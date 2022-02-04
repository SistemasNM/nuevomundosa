Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_TipoMaquina

        Public Usuario As String
        Public codigo_tipo_maquina As String
        Public descripcion_tipo_maquina As String
        Public numero_maquinas As Integer
        Private _objConnexion As AccesoDatosSQLServer
        Sub New()
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub
        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_tipo_maquina <> "" Then
                Dim sql = "INSERT INTO NM_Maquina " & _
                    "(codigo_tipo_maquina, descripcion_tipo_maquina, " & _
                    "numero_maquinas, " & _
                    "usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_tipo_maquina & "', '" & _
                    descripcion_tipo_maquina & "', " & _
                    numero_maquinas & ", '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_tipo_maquina <> "" Then
                Dim sql = "UPDATE NM_Maquina " & _
                    "SET " & _
                    "descripcion_tipo_maquina = '" & descripcion_tipo_maquina & "', " & _
                    "numero_maquinas = " & numero_maquinas & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_tipo_maquina = '" & codigo_tipo_maquina & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoTipoMaquina As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_TipoMaquina where codigo_tipo_maquina = '" & codigoTipoMaquina & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_tipo_maquina = objDR("codigo_tipo_maquina")
                descripcion_tipo_maquina = objDR("descripcion_tipo_maquina")
                numero_maquinas = objDR("numero_maquinas")
            Next
        End Sub

        Function KgHora(ByVal codTipoMaquina As String) As Double
            Dim bd As New NM_Consulta(4)
            Dim dt As DataTable
            Dim fila As DataRow

            Dim sql = "SELECT AVG(kilos_hora) FROM NM_Maquina WHERE codigo_tipo_maquina = '" & codTipoMaquina & "'"
            dt = bd.Query(sql)

            For Each fila In dt.Rows
                If Not IsDBNull(fila(0)) Then
                    Return fila(0)
                Else
                    Return 0
                End If
            Next
        End Function

        Function List()
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_TipoMaquina "
            Return bd.Query(sql)
        End Function

        Public Function ufn_BusquedaHilosTitulos(ByVal strCodigo As String, ByVal strDesc As String) As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"COD_HILO", strCodigo,
                                                 "DES_HILO", strDesc}

                Return _objConnexion.ObtenerDataTable("USP_OBTENER_HILO_TITULOS", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function

        Public Function List_V2() As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

                Return _objConnexion.ObtenerDataTable("USP_OBTENER_TIPO_MAQUINA")
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function
    End Class

End Namespace