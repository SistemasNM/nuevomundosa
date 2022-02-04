Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos


Namespace NM_Hilanderia

    Public Class NM_InventarioConeras

        Public codigo_inventario As String
        Public codigo_maquina As String
        Public revision_maquina As Integer
        Public codigo_hilo As String
        Public numero_usos As Integer
        Public canillas_proceso As Double
        Public conos_proceso As Double
        Public conos_avance As Double
        Public canillas_llenas As Double
        Public Usuario As String
        Public peso_canilla As Double
        Public peso_cono As Double

        Private objUtil As New NM_General.Util

        Sub New()
            codigo_inventario = ""
            codigo_maquina = ""
            revision_maquina = 0
            codigo_hilo = ""
            numero_usos = 0
            canillas_proceso = 0
            conos_proceso = 0
            conos_avance = 0
            canillas_llenas = 0
            Usuario = ""

            peso_canilla = 0
            peso_cono = 0

        End Sub

        Function Add() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", codigo_inventario, _
                                                  "codigo_maquina", codigo_maquina, _
                                                  "revision_maquina", revision_maquina, _
                                                  "codigo_hilo", codigo_hilo, _
                                                  "numero_usos", numero_usos, _
                                                  "canillas_proceso", canillas_proceso, _
                                                  "conos_proceso", conos_proceso, _
                                                  "conos_avance", conos_avance, _
                                                  "canillas_llenas", canillas_llenas, _
                                                  "usuario", Usuario, _
                                                  "peso_canilla", peso_canilla, _
                                                  "peso_cono", peso_cono, _
                                                  "int_accion", 1}

                lobjConexion.EjecutarComando("usp_hil_InvConeras_actualizar", lobjParametros)
                Add = True
            Catch
                Add = False
            End Try
        End Function

        Function Update() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", codigo_inventario, _
                                                  "codigo_maquina", codigo_maquina, _
                                                  "revision_maquina", revision_maquina, _
                                                  "codigo_hilo", codigo_hilo, _
                                                  "numero_usos", numero_usos, _
                                                  "canillas_proceso", canillas_proceso, _
                                                  "conos_proceso", conos_proceso, _
                                                  "conos_avance", conos_avance, _
                                                  "canillas_llenas", canillas_llenas, _
                                                  "usuario", Usuario, _
                                                  "peso_canilla", peso_canilla, _
                                                  "peso_cono", peso_cono, _
                                                  "int_accion", 2}

                lobjConexion.EjecutarComando("usp_hil_InvConeras_actualizar", lobjParametros)
                Update = True
            Catch
                Update = False
            End Try
        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoHilo As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "codigo_hilo", sCodigoHilo}

                lobjConexion.EjecutarComando("usp_hil_InvConeras_eliminar", lobjParametros)
                Delete = True
            Catch
                Delete = False
            End Try
        End Function


        Function List(ByVal Fecha As Date, Optional ByVal pCentroCosto As String = "") As DataTable

            Try
                Dim objUtil As New NM_Produccion.NM_Util.NM_Util
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", Format(Fecha, "yyyyMMdd"), _
                                                  "codigo_centro_costo", pCentroCosto}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvConeras_listar", lobjParametros)
                Return dt
            Catch
            End Try

        End Function

        Function List() As DataTable

            Try
                Dim objUtil As New NM_Produccion.NM_Util.NM_Util
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", "", _
                                                  "codigo_centro_costo", ""}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvConeras_listar", lobjParametros)
                Return dt
            Catch
            End Try

        End Function


    End Class

End Namespace