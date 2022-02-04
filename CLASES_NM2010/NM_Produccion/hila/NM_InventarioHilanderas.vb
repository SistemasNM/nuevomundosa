Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia
    Public Class NM_InventarioHilanderas

        Public Codigo_Inventario As String
        Public Codigo_maquina As String
        Public Codigo_Materia_Prima As String
        Public revision_maquina As Integer
        Public Codigo_hilo As String
        Public Tachos_Proceso As Double
        Public Avance_Proceso As Double
        Public Conos_Maquina As Double
        Public Avance_Maquina As Double
        Public Conos_Reserva As Double
        Public Usuario As String
        Public Peso_cono As Double

        Private objUtil As New NM_General.Util

        Sub New()
            Codigo_Inventario = ""
            Codigo_maquina = ""
            Codigo_Materia_Prima = ""
            revision_maquina = 0
            Codigo_hilo = 0
            Tachos_Proceso = 0
            Avance_Proceso = 0
            Conos_Maquina = 0
            Avance_Maquina = 0
            Conos_Reserva = 0
            Usuario = ""
            Peso_cono = 0
        End Sub

        Function Add() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario   ", Codigo_Inventario, _
                                                  "codigo_maquina      ", Codigo_maquina, _
                                                  "revision_maquina    ", revision_maquina, _
                                                  "codigo_hilo         ", Codigo_hilo, _
                                                  "codigo_materia_prima", Codigo_Materia_Prima, _
                                                  "tachos_proceso      ", Tachos_Proceso, _
                                                  "avance_proceso      ", Avance_Proceso, _
                                                  "conos_maquina       ", Conos_Maquina, _
                                                  "avance_maquina      ", Avance_Maquina, _
                                                  "conos_reserva       ", Conos_Reserva, _
                                                  "usuario             ", Usuario, _
                                                  "peso_cono           ", Peso_cono, _
                                                  "int_accion", 1}

                lobjConexion.EjecutarComando("usp_hil_InvHilanderas_actualizar", lobjParametros)
                Add = True
            Catch
                Add = False
            End Try
        End Function

        Function Update() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario   ", Codigo_Inventario, _
                                                  "codigo_maquina      ", Codigo_maquina, _
                                                  "revision_maquina    ", revision_maquina, _
                                                  "codigo_hilo         ", Codigo_hilo, _
                                                  "codigo_materia_prima", Codigo_Materia_Prima, _
                                                  "tachos_proceso      ", Tachos_Proceso, _
                                                  "avance_proceso      ", Avance_Proceso, _
                                                  "conos_maquina       ", Conos_Maquina, _
                                                  "avance_maquina      ", Avance_Maquina, _
                                                  "conos_reserva       ", Conos_Reserva, _
                                                  "usuario             ", Usuario, _
                                                  "peso_cono           ", Peso_cono, _
                                                  "int_accion", 2}

                lobjConexion.EjecutarComando("usp_hil_InvHilanderas_actualizar", lobjParametros)
                Update = True
            Catch ex As Exception
                Update = False
            End Try

        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoHilo As String, ByVal pCodMateriaPrima As String) As Boolean

            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "codigo_hilo", sCodigoHilo, _
                                                  "codigo_materia_prima", pCodMateriaPrima}

                lobjConexion.EjecutarComando("usp_hil_InvHilanderas_eliminar", lobjParametros)
                Delete = True
            Catch
                Delete = False
            End Try

        End Function

        Function List() As DataTable
            Try

                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", ""}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvHilanderas_listar", lobjParametros)
                Return dt
            Catch
            End Try
        End Function

        Function List(ByVal Fecha As Date) As DataTable
            Try

                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", Format(Fecha, "yyyyMMdd")}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvHilanderas_listar", lobjParametros)
                Return dt
            Catch
            End Try
        End Function

        Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoHilo As String, ByVal pCodMateriaPrima As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "codigo_hilo", sCodigoHilo, _
                                                  "codigo_materia_prima", pCodMateriaPrima}

                dt = lobjConexion.ObtenerDataTable("usp_hil_InvHilanderas_obtener", lobjParametros)

                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoHilo As String, ByVal pCodMateriaPrima As String)

            Dim dt As New DataTable, fila As DataRow
            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

            Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                              "codigo_maquina", sCodigoMaquina, _
                                              "codigo_hilo", sCodigoHilo, _
                                              "codigo_materia_prima", pCodMateriaPrima}

            dt = lobjConexion.ObtenerDataTable("usp_hil_InvHilanderas_obtener", lobjParametros)


            For Each fila In dt.Rows
                If IsDBNull(fila("tachos_proceso")) = False Then Tachos_Proceso = fila("tachos_proceso")
                If IsDBNull(fila("avance_proceso")) = False Then Avance_Proceso = fila("avance_proceso")
                If IsDBNull(fila("conos_maquina")) = False Then Conos_Maquina = fila("conos_maquina")
                If IsDBNull(fila("avance_maquina")) = False Then Avance_Maquina = fila("avance_maquina")
                If IsDBNull(fila("conos_reserva")) = False Then Conos_Reserva = fila("conos_reserva")
                If IsDBNull(fila("codigo_materia_prima")) = False Then Codigo_Materia_Prima = fila("codigo_materia_prima")
                If IsDBNull(fila("peso_cono")) = False Then Peso_cono = fila("peso_cono")
                Codigo_Inventario = sCodigoInventario
                Codigo_maquina = sCodigoMaquina
                Codigo_hilo = Codigo_hilo
            Next
        End Sub

    End Class

End Namespace