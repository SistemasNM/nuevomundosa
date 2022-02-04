Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
	Public Class NM_Perfil
		Public Codigo As String
		Public Descripcion As String
		Public Usuario As String
		Public dtPermisos As New DataTable()

		Sub New()
			Codigo = ""
			Descripcion = ""
		End Sub

		Sub New(ByVal sCodigo As String)
			Seek(sCodigo)

		End Sub

		Public Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "insert into NM_PerfilUsuario (codigo_perfil," & _
				"descripcion_perfil)values('" & Codigo & "', '" & Descripcion & "')"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Update() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Update NM_PerfilUsuario set descripcion_perfil='" & _
				Descripcion & "' where codigo_perfil='" & Codigo & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Delete(ByVal sCodigo As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Delete from NM_PerfilUsuario where codigo_perfil='" & sCodigo & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Sub Seek(ByVal scodigo As String)
			Dim sql As String, objConn As New NM_Consulta(), dtUsuario As New DataTable()
			Dim fila As DataRow
			sql = "Select * from NM_PerfilUsuario where codigo_perfil ='" & scodigo & "'"
			dtUsuario = objConn.Query(sql)
			For Each fila In dtUsuario.Rows
				Descripcion = fila.Item("descripcion_perfil")
			Next
		End Sub

		Public Function Exist(ByVal sCodigo As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta(), dtUsuario As New DataTable()
			sql = "Select * from NM_PerfilUsuario where codigo_perfil ='" & sCodigo & "'"
			dtUsuario = objConn.Query(sql)
			If dtUsuario.Rows.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		Public Function List() As DataTable
			Dim sql As String, objConn As New NM_Consulta(), dtUsuario As New DataTable()
			sql = "Select * from NM_PerfilUsuario "
			dtUsuario = objConn.Query(sql)
			Return dtUsuario
		End Function


	End Class

End Namespace