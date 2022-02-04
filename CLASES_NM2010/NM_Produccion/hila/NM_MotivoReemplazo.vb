Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_MotivoReemplazo

        Public Usuario As String
        Public codigo_motivo As String
        Public descripcion_motivo As String

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_motivo <> "" Then
                Dim sql = "INSERT INTO NM_MotivoReemplazo " & _
                    "(codigo_motivo, descripcion_motivo, " & _
                    "usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_motivo & "', '" & _
                    descripcion_motivo & "', '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_motivo <> "" Then
                Dim sql = "UPDATE NM_MotivoReemplazo " & _
                    "SET descripcion_motivo = '" & descripcion_motivo & "', " & _
                    "WHERE codigo_motivo = '" & codigo_motivo & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoMotivo As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_MotivoReemplazo where codigo_motivo = '" & codigoMotivo & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_motivo = objDR("codigo_motivo")
                descripcion_motivo = objDR("descripcion_motivo")
            Next

        End Sub

        Function List()
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_MotivoReemplazo "
            Return bd.Query(sql)
        End Function

    End Class

End Namespace