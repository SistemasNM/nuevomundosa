Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria


Public Class NM_Ligamento
        Public usuario As String
        Private BD As New NM_Consulta()

        Public codigo_ligamento As String
        Public descripcion_ligamento As String

        Public Sub New()
            codigo_ligamento = ""
            descripcion_ligamento = ""

        End Sub

        Public Sub New(ByVal codigoLigamento As String)
            Seek(codigoLigamento)
        End Sub

        Public Sub Seek(ByVal codigoLigamento As String)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_Ligamento WHERE codigo_ligamento = '" & codigoLigamento & "'"
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_ligamento = objDR("codigo_ligamento")
                descripcion_ligamento = objDR("descripcion_ligamento")
            Next
        End Sub

        Function Lista() As DataTable
            Dim strSQL = "SELECT * FROM NM_Ligamento"
            Return BD.Query(strSQL)
        End Function

End Class


End Namespace