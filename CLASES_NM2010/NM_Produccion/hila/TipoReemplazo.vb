Imports NM_General.NM_BaseDatos

Namespace Hilanderia

    Public Class NM_TipoReemplazo

        Public Usuario As String
        Public codigo_tipo_reemplazo As String
        Public descripcion_reemplazo As String

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_tipo_reemplazo <> "" Then
                Dim sql = "INSERT INTO NM_TipoReemplazo " & _
                    "(codigo_tipo_reemplazo, descripcion_reemplazo, " & _
                    "usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_tipo_reemplazo & "', '" & _
                    descripcion_reemplazo & "', '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_tipo_reemplazo <> "" Then
                Dim sql = "UPDATE NM_TipoReemplazo " & _
                    "SET descripcion_reemplazo = '" & descripcion_reemplazo & "', " & _
                    "WHERE codigo_tipo_reemplazo = '" & codigo_tipo_reemplazo & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoTipoReemplazo As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_TipoReemplazo where codigo_tipo_reemplazo = '" & codigoTipoReemplazo & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_tipo_reemplazo = objDR("codigo_tipo_reemplazo")
                descripcion_reemplazo = objDR("descripcion_reemplazo")
            Next

        End Sub

        Function List()
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_TipoReemplazo "
            Return bd.Query(sql)
        End Function

    End Class

End Namespace