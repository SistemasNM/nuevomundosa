Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_DefectosSultzer
        Public CodigoDefecto As String
        Public DescripcionDefecto As String
        Public Tipo As String
        Public Usuario As String

        Public Sub New()
            CodigoDefecto = ""
            DescripcionDefecto = ""
            Tipo = ""
        End Sub

        Public Sub Seek(ByVal pCodigoDefecto As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dtDefecto As New DataTable
            Dim drDefecto As DataRow
            sql = "SELECT * FROM NM_DefectosSultzer " & _
            " WHERE codigo_defecto_sulzer = '" & pCodigoDefecto & "'"
            dtDefecto = objConn.Query(sql)
            For Each drDefecto In dtDefecto.Rows
                CodigoDefecto = drDefecto("codigo_defecto_sulzer")
                DescripcionDefecto = drDefecto("descripcion_defecto_sulzer")
                Tipo = drDefecto("tipo_defecto_sulzer")
            Next
        End Sub

        Function List(ByVal pTipo As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_DefectosSultzer where tipo_defecto_sulzer = '" & pTipo & "'"
            Return objConn.Query(strSQL)
        End Function

    End Class

End Namespace