Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia

    Public Class HA_MateriaPrima
        Public Codigo_MateriaPrima As String
        Public Descripcion_MateriaPrima As String
        Public Codigo_tipo As String
        Public Usuario As String

        Sub New()
            Codigo_tipo = ""
            Descripcion_MateriaPrima = ""
            Codigo_MateriaPrima = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into HA_MateriaPrima(codigo_materia_prima, descripcion_materia_prima," & _
                "usuario_creacion, fecha_creacion) values('" & Codigo_MateriaPrima & _
                "','" & Descripcion_MateriaPrima & "','" & Usuario & "',getdate())"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update HA_MateriaPrima set descripcion_materia_prima='" & _
                Descripcion_MateriaPrima & "'," & _
                " usuario_modificacion='" & Usuario & _
                "', fecha_modificacion=getdate() where codigo_materia_prima='" & Codigo_MateriaPrima & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoMateria As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from HA_MateriaPrima where codigo_materia_prima='" & _
                sCodigoMateria & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            sql = "Select * from  HA_MateriaPrima "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal codigoTipo As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            sql = "Select * from  HA_MateriaPrima where codigo_tipo = '" & codigoTipo & "'"
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoMateria As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            Try
                sql = "Select * from HA_MateriaPrima where codigo_materia_prima='" & sCodigoMateria & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoMateria As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow
            sql = "Select * from HA_MateriaPrima where codigo_materia_prima='" & sCodigoMateria & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Codigo_tipo = fila("codigo_Tipo")
                Descripcion_MateriaPrima = fila("descripcion_materia_prima")
                Codigo_MateriaPrima = fila("codigo_materia_prima")
            Next
        End Sub

    End Class
End Namespace