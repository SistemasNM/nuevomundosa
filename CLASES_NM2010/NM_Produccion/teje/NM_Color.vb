Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria

    Public Class NM_Color
        Public codigo As String
        Public descripcion As String
        Public usuario As String

        Public Function insert() As Integer
            Dim DB As New NM_Consulta()

            Dim strsql As String
            strsql = "INSERT INTO NM_Color values("
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("'" & descripcion & "',")
            commandString.Append("'" & usuario & "',")
            commandString.Append("GetDate(),")
            commandString.Append("'" & usuario & "',")
            commandString.Append("GetDate())")
            Try
                DB.Execute(commandString.ToString)
                Return 1
            Catch
                Return 0
            End Try
            DB = Nothing
        End Function

        Public Function list() As DataTable
            Dim DB As New NM_Consulta()
            'Return DB.Query("Select * from NM_Color")
            Return DB.Query("SELECT RTRIM(LTRIM(STR(codigo_color))) AS codigo_color, RTRIM(LTRIM(Descripcion)) AS Descripcion FROM NM_Color UNION SELECT RTRIM(LTRIM(codigo_color_ted)) AS codigo_color, RTRIM(LTRIM(descripcion_color_ted)) AS Descripcion FROM NM_ColorTED")
        End Function

     


        Public Sub Seek(ByVal pCodigo As String)
            Dim objConn As New NM_Consulta, sql As String
            Dim dtColor As New DataTable, drowColor As DataRow
            sql = "Select * from NM_Color where codigo_color ='" & pCodigo & "' "
            dtColor = objConn.Query(sql)
            For Each drowColor In dtColor.Rows
                codigo = drowColor("codigo_color")
                descripcion = drowColor("descripcion")
            Next
        End Sub

    End Class

End Namespace
