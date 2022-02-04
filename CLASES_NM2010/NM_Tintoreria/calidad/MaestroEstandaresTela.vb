Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class MaestroEstandaresTela
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoArticulo As String
        Private m_intRevisionArticulo As Integer
        Private m_strCodigoCaracteristica As String
        Private m_dblParametroEstandar As Double
        Private m_intValorMinimo As Double
        Private m_intValorMaximo As Double

        Private m_strUsuario As String

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
        Sub New()
            'm_strCodigoDefecto = String.Empty
            'm_strDescripcionDefecto = String.Empty
            'm_strUsuarioCreacion = String.Empty
            ''m_dteFechaCreacion = Now
            ''m_strUsuarioModificacion = String.Empty
            ''m_dteFechaModificacion = Now

            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
        End Sub

        Sub New(ByVal strCodigoArticulo As String, _
                ByVal intRevisionArticulo As Integer, _
                ByVal strCodigoCaracteristica As String, _
                ByVal dblParametroEstandar As Double, _
                ByVal intValorMinimo As Double, _
                ByVal intValorMaximo As Double)

            m_strCodigoArticulo = strCodigoArticulo
            m_intRevisionArticulo = intRevisionArticulo
            m_strCodigoCaracteristica = strCodigoCaracteristica
            m_dblParametroEstandar = dblParametroEstandar
            m_intValorMinimo = intValorMinimo
            m_intValorMaximo = intValorMaximo
            m_strUsuario = String.Empty 'strUsuario

            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
        End Sub
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Sub Insertar()

            Dim parametros As Object() = {"codigoArticulo", m_strCodigoArticulo, _
                                          "revisionArticulo", m_intRevisionArticulo, _
                                          "codigoCaracteristica", m_strCodigoCaracteristica, _
                                          "parametro_estandar", m_dblParametroEstandar, _
                                          "valorMinimo", m_intValorMinimo, _
                                          "valorMaximo", m_intValorMaximo, _
                                          "usuario", m_strUsuario}

            Try
                m_sqlDtAccCalidadTintoreria.EjecutarComando("UP_InsertarMaestroEstandares", parametros)

            Catch ex As Exception

            End Try

        End Sub

        Public Sub Actualizar()

            Dim parametros As Object() = {"codigoArticulo", m_strCodigoArticulo, _
                                           "revisionArticulo", m_intRevisionArticulo, _
                                           "codigoCaracteristica", m_strCodigoCaracteristica, _
                                           "parametroEstandar", m_dblParametroEstandar, _
                                           "valorMinimo", m_intValorMinimo, _
                                           "valorMaximo", m_intValorMaximo, _
                                           "usuario", m_strUsuario}

            Try
                m_sqlDtAccCalidadTintoreria.EjecutarComando("UP_ActualizarMaestroEstandares", parametros)

            Catch ex As Exception

            End Try

        End Sub

        Public Sub Eliminar(ByVal codigoArticulo As String, _
                            ByVal revisionArticulo As Integer, _
                            ByVal codigoCaracteristica As String)

            Dim parametros As Object() = {"codigoArticulo", codigoArticulo, _
                                          "revisionArticulo", revisionArticulo, _
                                          "codigoCaracteristica", codigoCaracteristica}

            Try
                m_sqlDtAccCalidadTintoreria.EjecutarComando("UP_EliminarMaestroEstandar", parametros)

            Catch ex As Exception

            End Try

        End Sub

        Public Function ObtenerEstandaresTela(ByVal codigoArticulo As String, _
                                              ByVal revisionArticulo As Integer) As DataTable

            Dim dtEstandaresTela As DataTable

            Try
                Dim parametros As Object() = {"codigoArticulo", codigoArticulo, _
                                              "revisionArticulo", revisionArticulo}

                dtEstandaresTela = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerEstandaresTela", parametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtEstandaresTela

        End Function

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region

#Region "MAESTRO DE TELA ACABADA"
		Public Function CargarTipoDePruebas() As DataTable
			Try
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_CARGAR_PRUEBAS_CALIDAD")
			Catch ex As Exception

			End Try
		End Function
		Public Function CargarTipoDeLigamentos() As DataTable
			Try
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_CARGAR_TIPO_LIGAMENTOS")
			Catch ex As Exception

			End Try
		End Function
		Public Function CargarTipoDeAcabado() As DataTable
			Try
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_CARGAR_LISTA_ACABADOS")
			Catch ex As Exception

			End Try
		End Function
		Public Function FiltraEstandaresPorTipoDePrueba(ByVal intTipoPrueba As Integer) As DataTable
			Try
				Dim objParametros() As Object = {"CODIGO_PRUEBA", intTipoPrueba}
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_CARGA_TIPO_DE_ESTANDARES", objParametros)
			Catch ex As Exception

			End Try
		End Function
		Public Function FiltraEstandaresPorSolidez(ByVal intTipoPrueba As Integer) As DataTable
			Try
				Dim objParametros() As Object = {"CODIGO_PRUEBA", intTipoPrueba}
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_CARGAR_DATOS_SOLIDEZ", objParametros)
			Catch ex As Exception

			End Try
		End Function

		Public Function CargarDatosEstandares(ByVal intTipoPrueba As Integer, ByVal strCodigoLigamento As String, ByVal strCodigoAcabado As String) As DataTable
			Try
                Dim objParametros() As Object = {"int_CodigoTipoPrueba", intTipoPrueba, "var_Ligamento", strCodigoLigamento, "var_Acabado", strCodigoAcabado}
                Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_TIN_DatosEstandar_Listar", objParametros)
			Catch ex As Exception

			End Try
		End Function
		Public Function ObtieneCombinaciones() As DataTable
			Try
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_COMBINACION_ESTANDARES")
			Catch ex As Exception

			End Try
		End Function
		Public Function ObtieneDescripcionAcabados() As DataTable
			Try
				Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_CARGAR_DESC_ACABADOS")
			Catch ex As Exception

			End Try
		End Function
		Public Sub CrearEquivalencia(ByVal intTipoArticulo As Integer, ByVal data1 As String, ByVal data2 As String, ByVal intTipoPrueba As Integer)
			Try
				Dim objParametros() As Object = {"COD_TIPO_ARTICULO", intTipoArticulo, "DATA1", data1, "DATA2", data2, "COD_TIPO_PRUEBA", intTipoPrueba}
				m_sqlDtAccCalidadTintoreria.EjecutarComando("SP_CREAR_ARTICULO_EQUIVALENTE", objParametros)
			Catch ex As Exception

			End Try

		End Sub
		Public Sub InsertarNuevoEstandar(ByVal codTipoPrueba As Integer, ByVal Data1 As String, ByVal data2 As String)
			Try
				Dim objParametros() As Object = {"COD_TIPO_PRUEBA", codTipoPrueba, "DATA1", Data1, "DATA2", data2}
				m_sqlDtAccCalidadTintoreria.EjecutarComando("INSERTAR_ESTANDAR_NUEVO", objParametros)
			Catch ex As Exception

			End Try
		End Sub

		Public Function ActualizarEstandares(ByVal codTipoPrueba As Integer, ByVal strDescCampo As String, ByVal ValorMinimo As Double, ByVal ValorMaximo As Double, ByVal Estandar As Double, ByVal Data1 As String, ByVal Data2 As String) As Integer
			Try
				Dim objParametros() As Object = {"COD_TIPO_PRUEBA", codTipoPrueba, _
												 "DESC_CAMPO", strDescCampo, _
												 "VALOR_MINIMO", ValorMinimo, _
												 "VALOR_MAXIMO", ValorMaximo, _
												 "ESTANDAR", Estandar, _
												 "DATA1", Data1, _
												 "DATA2", Data2}

				Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("SP_ACTUALIZAR_VALOR_ESTANDAR", objParametros), Integer)

			Catch ex As Exception

			End Try
		End Function
		Public Function EliminarEstandares(ByVal codTipoPrueba As Integer, ByVal strDescCampo As String, ByVal Data1 As String, ByVal Data2 As String) As Integer
			Try
				Dim objParametros() As Object = {"COD_TIPO_PRUEBA", codTipoPrueba, _
												 "DESC_CAMPO", strDescCampo, _
												 "DATA1", Data1, _
												 "DATA2", Data2}
				Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("SP_ELIMINAR_ESTANDAR", objParametros), Integer)
			Catch ex As Exception

			End Try
		End Function
        Public Function InsertarEstandares(ByVal CodigoTipoPrueba As Integer, ByVal CodigoCampo As String, ByVal ValorMinimo As Double, ByVal ValorMaximo As Double, ByVal ValorEstandar As Double, ByVal Data1 As String, ByVal Data2 As String) As Integer
            Try
                Dim objParametros() As Object = {"int_CodigoTipoPrueba", CodigoTipoPrueba, _
                         "int_CodigoCampo", CodigoCampo, _
                         "num_ValorMinimo", ValorMinimo, _
                         "num_ValorMaximo", ValorMaximo, _
                         "num_ValorEstandar", ValorEstandar, _
                         "var_Data1", Data1, _
                         "var_Data2", Data2}
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("usp_TIN_DatosEstandar_Insertar", objParametros), Integer)

            Catch ex As Exception

            End Try
        End Function
#End Region
	End Class

End Namespace
