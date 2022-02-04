Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_MaquinaOperario
        Public codigo_maquina_operario As String
        Public codigo_operario As String
        Public codigo_maquina As String
        Public revision_maquina As Integer
        Public turno As Integer
		Public fecha_inicio As String
		Public fecha_fin As String
        Public usuario As String
        Public DB As NM_Consulta
        Private objUtil As New NM_General.Util
		Private objConexion As AccesoDatosSQLServer


		Public Function add() As Boolean
			Try
				Dim objParametros() As Object = {"p_var_CodigoOperario", codigo_operario, "p_var_CodigoMaquina", codigo_maquina, _
				"p_int_RevisionMaquina", revision_maquina, "p_int_Turno", turno, _
				"p_var_Inicio", fecha_inicio, "p_var_Final", fecha_fin, _
				"p_var_Usuario", usuario}

				objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
				objConexion.EjecutarComando("usp_ins_MaquinaOperario", objParametros)
				add = True
			Catch Ex As Exception
				add = False
				Throw Ex
			Finally
				objConexion = Nothing
			End Try
		End Function

		Public Function update() As Boolean
			Try
				Dim objParametros() As Object = {"p_var_CodigoOperario", codigo_operario, "p_var_CodigoMaquina", codigo_maquina, _
				"p_int_RevisionMaquina", revision_maquina, "p_int_Turno", turno, _
				"p_var_Inicio", fecha_inicio, "p_var_Final", fecha_fin, _
				"p_var_Usuario", usuario}

				objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
				objConexion.EjecutarComando("usp_ins_MaquinaOperario", objParametros)
				update = True
			Catch Ex As Exception
				update = False
				Throw Ex
			Finally
				objConexion = Nothing
			End Try
		End Function

		Public Sub Seek(ByVal pcodigo_maquina_operario As String)
			Dim bd As New NM_Consulta(4)
			Dim sql As String
			Dim objDT As New DataTable
			Dim objDR As DataRow
			sql = "SELECT * from NM_MaquinaOperario where codigo_maquina_operario = '" & pcodigo_maquina_operario & "'"
			objDT = bd.Query(sql)
			For Each objDR In objDT.Rows
				codigo_maquina_operario = objDR("codigo_maquina_operario")
				codigo_operario = objDR("codigo_operario")
				codigo_maquina = objDR("codigo_maquina")
				turno = objDR("turno")
				fecha_inicio = objDR("fecha_inicio")
				fecha_fin = objDR("fecha_fin")
			Next
		End Sub

		Public Function delete(ByVal pcodigo_maquina_operario As String) As Boolean
			' If pcodigo_maquina_operario <> "" Then
			Dim strsql As String
			DB = New NM_Consulta(4)
			strsql = "DELETE FROM NM_MaquinaOperario where codigo_maquina_operario = '" & pcodigo_maquina_operario & "'"
			Return DB.Execute(strsql)
			'  Else
			'      Return False
			'  End If
		End Function

		Public Function list() As DataTable
			Dim bd As New NM_Consulta(4)
			Dim strsql As String = "SELECT MO.codigo_maquina_operario,MO.codigo_operario, " & _
			 "MO.codigo_maquina,MO.fecha_inicio,MO.fecha_fin,O.Nombre_Operario,MO.fecha_creacion  " & _
			 "FROM NM_MaquinaOperario MO join NM_Operario O " & _
			 "on MO.codigo_operario = O.codigo_Operario order by 7"
			Return bd.Query(strsql)
		End Function

		Public Function list(ByVal nTurno As Integer, ByVal str_FInicio As String, ByVal str_FFinal As String) As DataTable
			Try
				objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
				Dim objParametros() As Object = {"p_int_Turno", nTurno, "p_var_Inicio", str_FInicio, "p_var_Final", str_FFinal}
				Return objConexion.ObtenerDataTable("usp_qry_MaquinaOperario", objParametros)
			Catch Ex As Exception
				Throw Ex
			Finally
				objConexion = Nothing
			End Try
		End Function

		Public Function list(ByVal fechaInicio As Date) As DataTable
			Dim bd As New NM_Consulta(4)
			Dim strsql As String = "SELECT MO.codigo_maquina_operario,MO.codigo_operario, " & _
			 "MO.codigo_maquina,MO.fecha_inicio,MO.fecha_fin,O.Nombre_Operario,MO.fecha_creacion  " & _
			 "FROM NM_MaquinaOperario MO join NM_Operario O " & _
			 "on MO.codigo_operario = O.codigo_Operario "
			strsql = strsql & " where fecha_inicio ='" & objUtil.FormatFecha(fechaInicio) & "' "
			strsql = strsql & "order by 7"
			Return bd.Query(strsql)
		End Function

		Public Function exist(ByVal pfecha_inicio As Date, ByVal pfecha_fin As Date) As Boolean
			Dim bd As New NM_Consulta(4)
			Dim tabla As DataTable
			Dim strsql As String
			strsql = "SELECT * FROM NM_MaquinaOperario WHERE fecha_inicio = '" & objUtil.FormatFecha(pfecha_inicio) & "'"
			strsql = strsql & "and  fecha_fin ='" & objUtil.FormatFecha(pfecha_fin) & "'"
			tabla = bd.Query(strsql)
			If tabla.Rows.Count > 0 Then
				Return True
			End If
			Return False
		End Function


	End Class
End Namespace