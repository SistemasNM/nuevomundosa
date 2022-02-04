
Imports NM.AccesoDatos

Public Class FichaPartida
    'clase implementada por Arturo Luna

#Region " Declaracion de Variables Miembro "
    Private m_sqlDtAccTint As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlDtAccTint = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    End Sub
#End Region


    Public Function ObtenerTodasFichasHijas() As DataTable
        Try
            Return m_sqlDtAccTint.ObtenerDataTable("pr_NM_FichaPartida_ObtenerSoloHijas")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsertarFicha(ByVal codigo_ficha_partida As String, ByVal codigo_ficha As String, ByVal codigo_articulo As String, _
                 ByVal Codigo_Articulo_Corto As String, ByVal codigo_orden_produccion As String, ByVal metro_final As Double, ByVal Usuario_Creacion As String)
        Try
            Dim objParametros As Object() = {"codigo_ficha_partida", codigo_ficha_partida, "codigo_ficha", codigo_ficha, _
            "codigo_articulo", codigo_articulo, "Codigo_Articulo_Corto", Codigo_Articulo_Corto, _
            "codigo_orden_produccion", codigo_orden_produccion, "metro_final", metro_final, "usuario_creacion", Usuario_Creacion}
            m_sqlDtAccTint.EjecutarComando("pr_NM_FichaPartida_Insert", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function EsFichaPartida(ByVal codigo_ficha As String) As Boolean
        Try
            Dim objParametros As Object() = {"codigo_ficha", codigo_ficha}
            Dim drFichaPartida As SqlClient.SqlDataReader = m_sqlDtAccTint.ObtenerDataReader("pr_NM_FichaPartida_EsPartida", objParametros)
            If drFichaPartida.HasRows Then
                Return True  'no es partida porque esa ficha es padre
            Else
                Return False   'no es partida porque esa ficha es hija
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function Busca_CodigoArticulo_Corto(ByVal codigoLargo As String) As DataTable
        Try
            Dim objParametros As Object() = {"CodigoLargo", codigoLargo}
            Dim dtFichaPartida As DataTable
            Return m_sqlDtAccTint.ObtenerDataTable("pr_NM_Busca_CodigoArticulo_Corto", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function obtenerPiezasFicha(ByVal strNumFicha As String) As DataTable
        Dim objParametros As Object() = {"codigo_ficha", strNumFicha}
        Return m_sqlDtAccTint.ObtenerDataTable("SP_NM_OBTENER_PIEZAS_FICHA", objParametros)
    End Function

    Public Sub InsertarPiezaFicha(ByVal codigo_pieza As String, ByVal codigo_ficha_partida As String, _
                ByVal codigo_ficha As String, ByVal codigo_articulo As String, _
                ByVal Tipo As String, ByVal metraje As Double, ByVal Usuario As String)
        Try
            Dim objParametros As Object() = {"codigo_pieza", codigo_pieza, "codigo_ficha_partida", codigo_ficha_partida, _
                            "codigo_ficha", codigo_ficha, "Codigo_Articulo", codigo_articulo, _
                            "codigo_tipo", Tipo, "metraje", metraje, "usuario", Usuario}
            m_sqlDtAccTint.EjecutarComando("SP_NM_PIEZAPARTIDA_ADD", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#Region "Guido"

    Public Function ListaInventarioTinto(ByVal fecha As String) As DataTable
        Try
            Dim objParametros As Object() = {"FECHA", fecha}
            Dim dtFichaPartida As DataTable
            Return m_sqlDtAccTint.ObtenerDataTable("SP_NM_STOCK_TINTORERIA_INV_V2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub GuardarInventarioFichaFisica(ByVal codigo_ficha As String, ByVal metraje_fisico As String)
        Try
            Dim objParametros As Object() = {"codigo_ficha", codigo_ficha, "metraje_fisico", metraje_fisico}
            Dim dtFichaPartida As DataTable
            m_sqlDtAccTint.EjecutarComando("sp_nm_GuardarInventarioFichaFisica", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
	Public Function ListaMaquinasOperacionesPendientes() As DataTable
		Try

			Dim dtFichaPartida As DataTable
            Return m_sqlDtAccTint.ObtenerDataTable("usp_TIN_MaquinasOperacionPendiente")
		Catch ex As Exception
			Throw ex
		End Try
	End Function
#End Region


#Region "Luis Antezana"
	Public Sub ProcesoPreCierre(ByVal fecha As String, ByVal usuario As String)
		Try
			Dim objParametros As Object() = {"FECHA", fecha, "USUARIO", usuario}
			Dim dtFichaPartida As DataTable
			m_sqlDtAccTint.EjecutarComando("SP_NM_CIERRE_TINTO", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Sub

	Public Function ListaPreCierre(ByVal fecha As String) As DataTable
		Try
			Dim objParametros As Object() = {"FECHA", fecha}
			Dim dtFichaPartida As DataTable
			Return m_sqlDtAccTint.ObtenerDataTable("SP_NM_LISTA_CIERRE_TINTO", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Function

	Public Function HuboCierreMes(ByVal fecha As String) As Boolean
		Try
			Dim objParametros As Object() = {"fecha", fecha}
			Dim drFichaPartida As SqlClient.SqlDataReader = m_sqlDtAccTint.ObtenerDataReader("NM_EXISTE_CIERRE", objParametros)
			If drFichaPartida.HasRows Then
				Return True			  'no es partida porque esa ficha es padre
			Else
				Return False			   'no es partida porque esa ficha es hija
			End If
		Catch ex As Exception
			Throw ex
		End Try

	End Function

	Public Function HabilParaCierre(ByVal fecha As String) As Boolean
		Try
			Dim objParametros As Object() = {"fecha", fecha}
			Dim drFichaPartida As SqlClient.SqlDataReader = m_sqlDtAccTint.ObtenerDataReader("NM_HABIL_CERRAR", objParametros)
			If drFichaPartida.HasRows Then
				Return True			  'no es partida porque esa ficha es padre
			Else
				Return False			   'no es partida porque esa ficha es hija
			End If
		Catch ex As Exception
			Throw ex
		End Try

	End Function

	Public Function ExisteCierreMensualTinto(ByVal fecha As String) As Boolean
		Try
			Dim objParametros As Object() = {"fecha", fecha}
			Dim drFichaPartida As SqlClient.SqlDataReader = m_sqlDtAccTint.ObtenerDataReader("NM_EXISTE_CIERRE_MES", objParametros)
			If drFichaPartida.HasRows Then
				Return True			  'no es partida porque esa ficha es padre
			Else
				Return False			   'no es partida porque esa ficha es hija
			End If
		Catch ex As Exception
			Throw ex
		End Try

	End Function



	Public Sub UPDATE_CIERRE_METROS(ByVal fecha As String, ByVal ficha As String, ByVal articulo As String, ByVal usuario As String, ByVal metraje As Double)
		'    SP_NM_CIERRE_UPD_METRAJE() '24/06/2005', '302314-2', '26231831240000000005', 'ADMIN', 2500
		'  @FECHA VARCHAR(12),
		'  @CODIGO_FICHA VARCHAR(20),
		'  @CODIGO_ARTICULO_LARGO VARCHAR(30),
		'  @USUARIO VARCHAR(30),
		'  @METROS NUMERIC(16,2)
		Try
			Dim objParametros As Object() = {"FECHA", fecha, "CODIGO_FICHA", ficha, "CODIGO_ARTICULO_LARGO", articulo, "USUARIO", usuario, "METROS", metraje}
			Dim dtFichaPartida As DataTable
			m_sqlDtAccTint.EjecutarComando("SP_NM_CIERRE_UPD_METRAJE", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Sub

	Public Sub GeneraCierre(ByVal fecha As String, ByVal usuario As String)
		Try
			Dim objParametros As Object() = {"FECHA", fecha, "USUARIO", usuario}
			Dim dtFichaPartida As DataTable
			m_sqlDtAccTint.EjecutarComando("SP_NM_GENERA_CIERRE", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
	End Sub


#End Region


End Class
