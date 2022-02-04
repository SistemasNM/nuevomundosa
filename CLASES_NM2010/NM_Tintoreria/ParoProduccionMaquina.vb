Imports NM.AccesoDatos


Namespace NM.Tintoreria


Public Class ParoProduccionMaquina

#Region " Declaración de Variables Miembro "
    Private adSQL As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
    Sub New()
        adSQL = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    End Sub

#End Region

        'Private objUtil As New NM_Produccion.NM_Util.NM_Util

#Region "metodo de arturo"

		Public Function listarParos(ByVal codigo_maquina As String) As DataTable
			Try
				Dim objParametros() As Object = {"codigo_maquina", codigo_maquina}
				Return adSQL.ObtenerDataTable("pr_NM_ParoProduccionMaquina", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Function listarParos(ByVal codigo_maquina As String, ByVal strFecha As String) As DataTable
			Try
				Dim objParametros() As Object = {"codigo_maquina", codigo_maquina, "fecha", strFecha}
				Return adSQL.ObtenerDataTable("pr_NM_ParoProduccionMaquina_Fecha", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Sub Agregar(ByVal Codigo_Maquina As String, ByVal codigo_Etapa As String, ByVal codigo_Paro As String, _
		   ByVal codigo_tipoparoproduccion As String, ByVal fecha_Inicio As String, ByVal fecha_fin As String, ByVal hora_inicio As String, ByVal hora_fin As String, _
		   ByVal codigo_operario As String, ByVal codigo_supervisor As String, ByVal usuario_creacion As String, ByVal fecha_creacion As String)
			Try

				' If fecha_modificacion = "null" Then
				'fecha_modificacion = DBNull.Value
				'End If
				'If usuario_modificacion = "null" Then
				'usuario_modificacion = DBNull.Value
				'End If
				Dim objParametros() As Object = {"codigo_maquina", Codigo_Maquina, _
				   "codigo_Etapa", codigo_Etapa, _
				   "codigo_Paro", codigo_Paro, _
				   "codigo_tipoparoproduccion", codigo_tipoparoproduccion, _
				   "fecha_Inicio", CType(fecha_Inicio, String), _
				   "fecha_fin", CType(fecha_fin, String), _
				   "hora_inicio", hora_inicio, _
				   "hora_fin", hora_fin, _
				   "codigo_operario", codigo_operario, _
				   "codigo_supervisor", codigo_supervisor, _
				   "usuario_creacion", usuario_creacion, _
				   "fecha_creacion", CType(fecha_creacion, String)}

				adSQL.EjecutarComando("pr_NM_ParoProduccionMaquina_Insert", objParametros)

			Catch ex As Exception
				Throw ex
			End Try
		End Sub

		Public Sub EliminarByFecha(ByVal Codigo_Maquina As String, ByVal Fecha As String)
			Try
				Dim objParametros() As Object = {"codigo_maquina", Codigo_Maquina, "Fecha", Fecha}

				adSQL.EjecutarComando("SP_NM_ParoProduccionMaquinaFecha_Delete", objParametros)

			Catch ex As Exception
				Throw ex
			End Try
		End Sub

		Public Function Exists(ByVal Codigo_Maquina As String) As Boolean
			Try
				Dim objParametros() As Object = {"codigo_maquina", Codigo_Maquina}

				Dim drResult As SqlClient.SqlDataReader = adSQL.ObtenerDataReader("pr_NM_ParoProduccionMaquina", objParametros)
				Return drResult.HasRows
			Catch ex As Exception
				Throw ex
			End Try
		End Function

		Public Function Existe_Fecha_Maquina(ByVal Codigo_Maquina As String, ByVal Fecha As String) As Boolean
			Try
				Dim objParametros() As Object = {"codigo_maquina", Codigo_Maquina, "Fecha", Fecha}

				Dim drResult As SqlClient.SqlDataReader = adSQL.ObtenerDataReader("pr_NM_ParoProduccionMaquina_Fecha", objParametros)
				Return drResult.HasRows
			Catch ex As Exception
				Throw ex
			End Try
		End Function


#End Region

End Class

End Namespace