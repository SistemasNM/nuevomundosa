Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_TipoInventario
        Public Codigo_Tipo As String
        Public Descripcion_Tipo As String
        Public Usuario As String

        Sub New()
            codigo_tipo = ""
            descripcion_tipo = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New nm_consulta(4)
            Try
                sql = "Insert into NM_TipoInventario(codigo_tipo, descripcion_tipo," & _
                "usuario_creacion, fecha_creacion) values('" & Codigo_Tipo & _
                "','" & Descripcion_Tipo & "','" & Usuario & "',getdate())"
                Return objconn.execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New nm_consulta(4)
            Try
                sql = "Update NM_TipoInventario set descripcion_tipo='" & _
                Descripcion_Tipo & "', usuario_modificacion='" & Usuario & _
                "', fecha_modificacion=getdate() where codigo_tipo='" & Codigo_Tipo & "' "
                Return objconn.execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoTipo As String) As Boolean
            Dim sql As String, objConn As New nm_consulta(4)
            Try
                sql = "Delete from NM_TipoInventario where codigo_tipo='" & _
                sCodigoTipo & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As datatable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            sql = "Select * from  NM_TipoInventario "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoTipo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            Try
                sql = "Select * from NM_TipoInventario where codigo_tipo='" & sCodigoTipo & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoTipo As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow
            sql = "Select * from NM_TipoInventario where codigo_tipo='" & sCodigoTipo & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Codigo_Tipo = fila("codigo_Tipo")
                Descripcion_Tipo = fila("descripcion_tipo")
            Next
        End Sub

    End Class
End Namespace