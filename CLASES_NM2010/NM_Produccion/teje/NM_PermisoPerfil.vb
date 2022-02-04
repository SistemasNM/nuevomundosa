Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
	Public Class NM_PermisoPerfil
		Public codigo_perfil As String
		Public codigo_menu As String
		Public Orden As String
		Public usuario As String
		Public Debug As String
        Private m_sqlDtAccProd As AccesoDatosSQLServer
		Sub New()
			codigo_perfil = ""
			codigo_menu = ""
            Orden = ""
            m_sqlDtAccProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
		End Sub

		Public Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "insert into NM_PermisoPerfil (codigo_perfil," & _
				"codigo_menu, flagorden) values('" & codigo_perfil & _
				"', '" & codigo_menu & "','" & Orden & "')"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Delete(ByVal sCodigoPerfil As String, ByVal sCodigoMenu As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Delete from NM_PermisoPerfil where codigo_perfil='" & _
				sCodigoPerfil & "' and codigo_menu='" & sCodigoMenu & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Update() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Update NM_PermisoPerfil set flagOrden='" & Orden & _
				"' where codigo_perfil='" & codigo_perfil & "' and codigo_menu='" & _
				codigo_menu & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Exist(ByVal sCodigoPerfil As String, ByVal sCodigoMenu As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta(), dtUsuario As New DataTable()
			sql = "Select * from NM_PermisoPerfil where codigo_perfil='" & _
			sCodigoPerfil & "' and codigo_menu='" & sCodigoMenu & "' "
			dtUsuario = objConn.Query(sql)
			If dtUsuario.Rows.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		Public Function List() As DataTable
			Dim sql As String, objConn As New NM_Consulta(), dtUsuario As New DataTable()
			sql = "Select * from NM_PermisoPerfil "
			dtUsuario = objConn.Query(sql)
			Return dtUsuario
		End Function

		Public Function List(ByVal sCodigoPerfil As String) As DataTable
            Try
                Dim sql As String, objConn As New NM_Consulta, dtUsuario As New DataTable
                Dim objParameters As Object() = {"USUARIO", sCodigoPerfil}
                dtUsuario = m_sqlDtAccProd.ObtenerDataTable("NMprSEL_ListarOpcionesxUsuario", objParameters)
                'dtUsuario = objConn.getData
                '         sql = "Select M.codigo_tipo, " & _
                '         " CASE when M.nivel=0 then M.descripcion_menu end as DescMenu1, " & _
                '         " CASE when M.nivel=1 then M.descripcion_menu end as DescMenu2, " & _
                '         " CASE when M.nivel=2 then M.descripcion_menu end as DescMenu3, " & _
                '         " CASE when M.nivel=3 then M.descripcion_menu end as DescMenu4, " & _
                '         " M.destino_menu, M.codigo_tipo, M.nivel, " & _
                '         "M.codigo_menu, P.codigo_perfil, P.descripcion_perfil,PP.flagOrden,MT.descripcion_tipo " & _
                '         "from NM_MenuTipo MT, NM_MenuItem M, NM_PerfilUsuario P, NM_PermisoPerfil PP" & _
                '         " where P.codigo_perfil = PP.codigo_perfil and MT.codigo_tipo=M.codigo_tipo " & _
                '         " and PP.codigo_menu = M.codigo_menu  and P.codigo_perfil ='" & _
                '         sCodigoPerfil & "' order by M.codigo_menu,M.codigo_tipo, DescMenu1, DescMenu2,DescMenu3,DescMenu4,flagOrden "
                'dtUsuario = objConn.Query(sql)
                Return dtUsuario
            Catch ex As Exception
                Throw ex
            End Try
		End Function


        Public Function ListMaster(ByVal sCodigoPerfil As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(), dt As New DataTable()
            sql = "Select distinct M.codigo_tipo, MT.descripcion_tipo,MT.prioridad " & _
            " from NM_MenuTipo MT, NM_MenuItem M, NM_PerfilUsuario P, NM_PermisoPerfil PP" & _
            " where P.codigo_perfil = PP.codigo_perfil and MT.codigo_tipo=M.codigo_tipo " & _
            " and PP.codigo_menu = M.codigo_menu  and P.codigo_perfil =(" & _
            " Select codigo_perfil from NM_USUARIO Where codigo_usuario='" & _
            sCodigoPerfil & "') " & _
            " and MT.codigo_tipo<>'N' order by MT.prioridad "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function ListNivel(ByVal sCodigoPerfil As String, ByVal sCodigoPadre As String, ByVal sTipo As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(), dt As New DataTable()
            sql = "Select M.codigo_menu, M.descripcion_menu, M.destino_menu " & _
            " from NM_MenuTipo MT, NM_MenuItem M, NM_PerfilUsuario P, NM_PermisoPerfil PP" & _
            " where P.codigo_perfil = PP.codigo_perfil and MT.codigo_tipo=M.codigo_tipo " & _
            " and PP.codigo_menu = M.codigo_menu  and P.codigo_perfil =(" & _
            " Select codigo_perfil from NM_USUARIO Where codigo_usuario='" & _
            sCodigoPerfil & "') " & _
            " and M.codigo_padre='" & sCodigoPadre & "' and M.codigo_tipo='" & sTipo & "' order by PP.flagOrden "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function ListaTotalNiveles(ByVal sCodigoPerfil As String) As DataTable
            Try
                Dim dtListado As DataTable
                Dim objParameters As Object() = {"CODIGOPERFIL", sCodigoPerfil}
                dtListado = m_sqlDtAccProd.ObtenerDataTable("NMprSEL_ListarMenuxUsuario", objParameters)
                Return dtListado
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace