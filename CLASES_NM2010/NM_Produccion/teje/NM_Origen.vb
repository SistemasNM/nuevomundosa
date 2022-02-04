Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Origen

        Public Codigo_origen As String
        Public Descripcion As String
        Public Usuario_Creacion As String
        Public Fecha_Creacion As DateTime
        Public Usuario_Modificacion As String
        Public Fecha_Modificacion As DateTime

        Private objUtil As New NM_General.Util

        Public Sub New()
            Codigo_origen = ""
            Descripcion = ""
            Usuario_Creacion = ""
            Fecha_Creacion = Nothing
            Usuario_Modificacion = ""
            Fecha_Modificacion = Nothing
        End Sub

        Public Function exist(ByVal pCodigo_Origen As String) As Boolean
            Dim sql As New System.Text.StringBuilder, objConn As New NM_Consulta
            sql.Append("Select * from NM_Origen ")
            sql.Append("where codigo_origen='" & pCodigo_Origen & "' ")

            Dim dt As New DataTable
            dt = objConn.Query(sql.ToString)
            If dt.Rows.Count > 0 Then
                Codigo_origen = dt.Rows(0)("codigo_origen")
                Descripcion = dt.Rows(0)("descripcion")
                Return True
            Else
                Return False
            End If

        End Function

        Public Sub Seek(ByVal pCodigo_Origen As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_Origen where codigo_origen='" & pCodigo_Origen & "'"
            dt = objConn.Query(sql)
            If dt.Rows.Count > 0 Then
                Codigo_origen = dt.Rows(0)("codigo_origen")
                Descripcion = dt.Rows(0)("descripcion")
                If IsDBNull(dt.Rows(0)("usuario_creacion")) Then Usuario_Creacion = "" Else Usuario_Creacion = dt.Rows(0)("usuario_creacion")
                If IsDBNull(dt.Rows(0)("fecha_creacion")) Then Fecha_Creacion = Nothing Else Fecha_Creacion = dt.Rows(0)("fecha_creacion")
                If IsDBNull(dt.Rows(0)("usuario_modificacion")) Then Usuario_Modificacion = "" Else Usuario_Modificacion = dt.Rows(0)("usuario_modificacion")
                If IsDBNull(dt.Rows(0)("fecha_creacion")) Then Fecha_Creacion = Nothing Else Fecha_Modificacion = dt.Rows(0)("fecha_modificacion")
            Else
                Codigo_origen = ""
                Descripcion = ""
                Usuario_Creacion = ""
                Fecha_Creacion = Nothing
                Usuario_Modificacion = ""
                Fecha_Modificacion = Nothing
            End If
        End Sub

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_Origen "
            dt = objConn.Query(sql)
            Return dt
        End Function
    End Class
End Namespace