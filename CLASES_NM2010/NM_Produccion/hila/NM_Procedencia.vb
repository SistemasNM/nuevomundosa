Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_Procedencia
        Public Codigo_Procedencia As String
        Public Descripcion_Procedencia As String
        Public Usuario As String

        Sub New()
            Codigo_Procedencia = ""
            Descripcion_Procedencia = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into NM_TipoInventario(codigo_Procedencia, descripcion_Procedencia," & _
                "usuario_creacion, fecha_creacion) values('" & Codigo_Procedencia & _
                "','" & Descripcion_Procedencia & "','" & Usuario & "',getdate())"
                Return objConn.execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update NM_Procedencia set descripcion_Procedencia = '" & _
                Descripcion_Procedencia & "', usuario_modificacion='" & Usuario & _
                "', fecha_modificacion=getdate() where codigo_Procedencia='" & Codigo_Procedencia & "' "
                Return objConn.execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoProcedencia As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from NM_Procedencia where codigo_Procedencia='" & _
                sCodigoProcedencia & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            sql = "Select * from  NM_Procedencia "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoProcedencia As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            Try
                sql = "Select * from NM_Procedencia where codigo_Procedencia = '" & sCodigoProcedencia & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoProcedencia As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow
            sql = "Select * from NM_Procedencia where codigo_Procedencia = '" & sCodigoProcedencia & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Codigo_Procedencia = fila("codigo_Procedencia")
                Descripcion_Procedencia = fila("descripcion_Procedencia")
            Next
        End Sub

    End Class
End Namespace