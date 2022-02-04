Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

	Public Class NM_MenuItem
		Public Codigo As String
		Public Descripcion As String
		Public Destino As String
		Public Tipo As String
		Public Nivel As String
		Public CodPadre As String
		Public usuario As String
		Public Debug As String
#Region "Variables"
		Private _objConnProduccion As AccesoDatosSQLServer
#End Region
		Sub New()
			Codigo = ""
			Descripcion = ""
			Destino = ""
			Tipo = ""
			Nivel = "0"
			CodPadre = ""
		End Sub

		Public Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta
			Try
				sql = "insert into NM_MenuItem (codigo_menu," & _
				"descripcion_menu,destino_menu,codigo_tipo, nivel, codigo_padre) " & _
				"values('" & Codigo & "', '" & Descripcion & "','" & _
				Destino & "','" & Tipo & "','" & Nivel & "','" & CodPadre & "')"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Update() As Boolean
			Dim sql As String, objConn As New NM_Consulta
			Try
				sql = "Update NM_MenuItem set destino_menu='" & _
				Destino & "',descripcion_menu='" & Descripcion & "', " & _
				"codigo_tipo='" & Tipo & "',nivel='" & Nivel & "'," & _
				"codigo_padre='" & CodPadre & "' where codigo_menu='" & Codigo & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Delete(ByVal sCodigo As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta
			Try
				sql = "Delete from NM_MenuItem where codigo_menu='" & _
				sCodigo & "'"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Sub Seek(ByVal sCodigo As String)
			Dim sql As String, objConn As New NM_Consulta, dtUsuario As New DataTable
			Dim fila As DataRow
			sql = "Select * from NM_MenuItem where codigo_menu ='" & _
			sCodigo & "' "
			dtUsuario = objConn.Query(sql)
			For Each fila In dtUsuario.Rows
				Codigo = fila.Item("codigo_menu")
				Descripcion = fila.Item("descripcion_menu")
				Destino = fila.Item("destino_menu")
				CodPadre = fila.Item("codigo_padre")
				Tipo = fila.Item("codigo_tipo")
				Nivel = fila.Item("nivel")
			Next
		End Sub

		Public Function List() As DataTable
			Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
			sql = "Select * from NM_MenuItem "
			dt = objConn.Query(sql)
			Return dt
		End Function

		Public Function List(ByVal bParaGrid As Boolean) As DataTable
			Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
			sql = "Select codigo_menu, descripcion_menu, destino_menu, M.codigo_tipo, " & _
			 " nivel, codigo_padre, descripcion_tipo, visible " & _
			 " from NM_MenuItem M, NM_MenuTipo T " & _
			 " where M.codigo_tipo = T.codigo_tipo "
			dt = objConn.Query(sql)
			Return dt
		End Function
		Public Function List(ByVal pstrCodigoTipo As String) As DataTable
			Try
				_objConnProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
				Dim objParametros() As Object = {"p_var_CodigoTipo", pstrCodigoTipo}
				Return _objConnProduccion.ObtenerDataTable("usp_qry_ListarMenuPorTipo", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
		End Function
		Public Function List(ByVal sCodTipo As String, ByVal sCodPadre As String) As DataTable
			Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
			sql = "Select * from NM_MenuItem where codigo_tipo='" & _
			sCodTipo & "' and codigo_padre ='" & sCodPadre & "' "
			dt = objConn.Query(sql)
			Return dt
		End Function

		Public Function ListNivel(ByVal sCodTipo As String) As DataTable
			Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
			sql = "Select distinct nivel from NM_MenuItem where codigo_tipo='" & sCodTipo & "' "
			dt = objConn.Query(sql)
			Return dt
		End Function


	End Class

End Namespace