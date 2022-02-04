Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Hilanderia
    Public Class NM_InventarioContinuas

        Public Codigo_Inventario As String
        Public Codigo_maquina As String
        Public revision_maquina As Integer
        Public Codigo_Materia_Prima As String
        Public Ne_Real As Double
        Public Pabilos_Reserva As Double
        Public Pabilos_Maquina As Double
        Public Avance_Pabilos As Double
        Public Canillas_Proceso As Double
        Public Avance_Canillas As Double
        Public Canillas_Terminadas As Double
        Public Usuario As String
        Public peso_canilla As String
        Public Codigo_Tacho As String
        Private objUtil As New NM_General.Util

        Sub New()
            Codigo_Inventario = ""
            Codigo_maquina = ""
            revision_maquina = 0
            Codigo_Materia_Prima = ""
            Ne_Real = 0
            Pabilos_Reserva = 0
            Pabilos_Maquina = 0
            Avance_Pabilos = 0
            Canillas_Proceso = 0
            Avance_Canillas = 0
            Canillas_Terminadas = 0
            Usuario = ""
            peso_canilla = ""
            Codigo_Tacho = ""
        End Sub

        Function Add() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", Codigo_Inventario, _
                                                  "codigo_maquina", Codigo_maquina, _
                                                  "revision_maquina", revision_maquina, _
                                                  "Ne_real", Ne_Real, _
                                                  "codigo_materia_prima", Codigo_Materia_Prima, _
                                                  "pabilos_reserva", Pabilos_Reserva, _
                                                  "pabilos_maquina", Pabilos_Maquina, _
                                                  "avance_pabilos", Avance_Pabilos, _
                                                  "canillas_proceso", Canillas_Proceso, _
                                                  "avance_canillas", Avance_Canillas, _
                                                  "canillas_terminadas", Canillas_Terminadas, _
                                                  "usuario", Usuario, _
                                                  "peso_canilla", peso_canilla, _
                                                  "codigo_tacho", Codigo_Tacho, _
                                                  "int_accion", 1}

                lobjConexion.EjecutarComando("usp_hil_InvContinuas_actualizar", lobjParametros)
                Add = True
            Catch ex As Exception
                Add = False
            End Try

        End Function

        Function Update() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", Codigo_Inventario, _
                                                  "codigo_maquina", Codigo_maquina, _
                                                  "revision_maquina", revision_maquina, _
                                                  "Ne_real", Ne_Real, _
                                                  "codigo_materia_prima", Codigo_Materia_Prima, _
                                                  "pabilos_reserva", Pabilos_Reserva, _
                                                  "pabilos_maquina", Pabilos_Maquina, _
                                                  "avance_pabilos", Avance_Pabilos, _
                                                  "canillas_proceso", Canillas_Proceso, _
                                                  "avance_canillas", Avance_Canillas, _
                                                  "canillas_terminadas", Canillas_Terminadas, _
                                                  "usuario", Usuario, _
                                                  "peso_canilla", peso_canilla, _
                                                  "codigo_tacho", Codigo_Tacho, _
                                                  "int_accion", 2}

                lobjConexion.EjecutarComando("usp_hil_InvContinuas_actualizar", lobjParametros)
                Update = True
            Catch
                Update = False
            End Try
        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal nNeReal As Double, ByVal pCodigoMateriaPrima As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "Ne_real", nNeReal, _
                                                  "codigo_materia_prima", pCodigoMateriaPrima}

                lobjConexion.EjecutarComando("usp_hil_InvContinuas_eliminar", lobjParametros)

                Delete = True
            Catch
                Delete = False
            End Try
        End Function

        Function List(ByVal Fecha As Date) As DataTable
            Try
                Dim objUtil As New NM_Produccion.NM_Util.NM_Util
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", Format(Fecha, "yyyyMMdd")}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvContinuas_listar", lobjParametros)
                Return dt
            Catch
            End Try
        End Function

        Function List() As DataTable
            Try
                Dim objUtil As New NM_Produccion.NM_Util.NM_Util
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", ""}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvContinuas_listar", lobjParametros)

                Return dt
            Catch
            End Try
        End Function

        Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal nNeReal As Double, ByVal pCodigoMateriaPrima As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "ne_real", nNeReal, _
                                                  "codigo_materia_prima", pCodigoMateriaPrima}

                dt = lobjConexion.ObtenerDataTable("usp_hil_InvContinuas_obtener", lobjParametros)

                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try

        End Function

        Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal nNeReal As Double, ByVal pCodigoMateriaPrima As String)

            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim dt As New DataTable
            Dim fila As DataRow

            Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                              "codigo_maquina", sCodigoMaquina, _
                                              "ne_real", nNeReal, _
                                              "codigo_materia_prima", pCodigoMateriaPrima}

            dt = lobjConexion.ObtenerDataTable("usp_hil_InvContinuas_obtener", lobjParametros)

            For Each fila In dt.Rows
                If IsDBNull(fila("pabilos_reserva")) = False Then Pabilos_Reserva = fila("pabilos_reserva")
                If IsDBNull(fila("pabilos_maquina")) = False Then Pabilos_Maquina = fila("pabilos_maquina")
                If IsDBNull(fila("avance_pabilos")) = False Then Avance_Pabilos = fila("avance_pabilos")
                If IsDBNull(fila("canillas_proceso")) = False Then Canillas_Proceso = fila("canillas_proceso")
                If IsDBNull(fila("avance_canillas")) = False Then Avance_Canillas = fila("avance_canillas")
                If IsDBNull(fila("canillas_terminadas")) = False Then Canillas_Terminadas = fila("canillas_terminadas")
                If IsDBNull(fila("codigo_materia_prima")) = False Then Codigo_Materia_Prima = fila("codigo_materia_prima")
                If IsDBNull(fila("peso_canilla")) = False Then peso_canilla = fila("peso_canilla")
                If IsDBNull(fila("codigo_tacho")) = False Then Codigo_Tacho = fila("Codigo_Tacho")
                Codigo_Inventario = sCodigoInventario
                Codigo_maquina = sCodigoMaquina
                Ne_Real = nNeReal
            Next
        End Sub

    End Class

End Namespace