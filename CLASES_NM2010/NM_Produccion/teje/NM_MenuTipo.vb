Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

	Public Class NM_MenuTipo
		Public Codigo As String
        Public Descripcion As String
        Public Prioridad As Integer
		Public usuario As String
		Public Debug As String

		Sub New()
			Codigo = ""
            Descripcion = ""
            Prioridad = 0
		End Sub

		Public Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
                sql = "insert into NM_MenuTipo (codigo_tipo," & _
                "descripcion_tipo, prioridad) " & _
                "values('" & Codigo & "', '" & Descripcion & "'," & Prioridad & ")"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Update() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
                sql = "Update NM_MenuTipo set descripcion_tipo='" & _
                Descripcion & "', prioridad = " & Prioridad & "  where codigo_tipo='" & Codigo & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Delete(ByVal sCodigo As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Delete from NM_MenuTipo where codigo_tipo ='" & sCodigo & "'"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Sub Seek(ByVal sCodigo As String)
			Dim sql As String, objConn As New NM_Consulta(), dtUsuario As New DataTable()
			Dim fila As DataRow
			sql = "Select * from NM_MenuTipo where codigo_tipo ='" & sCodigo & "' "
			dtUsuario = objConn.Query(sql)
            For Each fila In dtUsuario.Rows
                Codigo = fila.Item("codigo_tipo")
                Descripcion = fila.Item("descripcion_tipo")
                Prioridad = fila.Item("prioridad")
            Next
		End Sub

		Public Function List() As DataTable
			Dim sql As String, objConn As New NM_Consulta(), dt As New DataTable()
			sql = "Select * from NM_MenuTipo "
			dt = objConn.Query(sql)
			Return dt
		End Function


	End Class

End Namespace