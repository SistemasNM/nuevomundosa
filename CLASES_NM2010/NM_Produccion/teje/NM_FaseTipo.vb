Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_FaseTipo

        Public codigo_fase As String
        Public codigo_tipo As String
        Public descripcion As String

        Sub New()
            codigo_fase = ""
            codigo_tipo = ""
            descripcion = ""
        End Sub

        Function SeekTipos(ByVal codigo_fase As String) As DataTable
            Dim db As New NM_Consulta()
            Dim strQuery As String
            Dim dt As DataTable

            strQuery = "SELECT * from NM_FaseTipo where codigo_fase=" & codigo_fase

            dt = db.Query(strQuery)

            SeekTipos = dt

        End Function


    End Class

End Namespace