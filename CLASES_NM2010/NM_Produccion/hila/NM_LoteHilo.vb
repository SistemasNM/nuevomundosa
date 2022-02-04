Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia
    Public Class NM_LoteHilo
        Public codigo_lotehilo As Integer
        Public lotehilo As String
        Public fecha As Date
        Public codigo_hilo As String
        Public usuario As String
        Private objUtil As New NM_General.Util

        Public Function insert() As Boolean
            Dim DB As New NM_Consulta(4)
            Dim strsql As String
            strsql = "INSERT INTO NM_LoteHilo values("
            Dim commandString As New System.Text.StringBuilder
            commandString.Append(strsql)
            commandString.Append("'" & objUtil.FormatFecha(fecha) & "',")
            commandString.Append("'" & lotehilo & "',")
            commandString.Append("'" & codigo_hilo & "',")
            commandString.Append("'" & usuario & "',")
            commandString.Append("GetDate(),")
            commandString.Append("'" & usuario & "',")
            commandString.Append("GetDate())")

            Return DB.Execute(commandString.ToString)
        End Function

        Public Function update() As Boolean
            Dim DB As New NM_Consulta(4)
            Dim strsql As String
            strsql = "UPDATE NM_LoteHilo SET " & _
            "fecha = '" & objUtil.FormatFecha(fecha) & "'," & _
            "codigo_hilo = '" & codigo_hilo & "'," & _
            "usuario_modificacion = '" & usuario & "'," & _
            "fecha_modificacion = getdate()" & _
            " where codigo_lotehilo = " & codigo_lotehilo
            Return DB.Execute(strsql)
        End Function

        Public Function delete(ByVal pcodigo_lotehilo As String) As Integer
            Dim strsql As String
            Dim DB As New NM_Consulta(4)
            strsql = "DELETE FROM NM_LoteHilo where codigo_lotehilo = '" & pcodigo_lotehilo & "'"
            Return DB.Execute(strsql)
        End Function

        Public Function List() As DataTable
            Dim DB As New NM_Consulta(4)
            Return DB.Query("Select * from NM_LoteHilo order by fecha_creacion")
        End Function

        Public Function List(ByVal pFecha As Date) As DataTable
            Dim DB As New NM_Consulta(4)
            Dim sql As String
            sql = "Select * from NM_LoteHilo " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 order by fecha_creacion"
            Return DB.Query(sql)
        End Function

    End Class
End Namespace


