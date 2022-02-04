Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_TestDato
        Public Usuario As String
        Public codigo_testdato As String
        Public descripcion_testdato As String

        Function List() As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_TestDato"
            Return bd.Query(sql)
        End Function

    End Class

End Namespace
