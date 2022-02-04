Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class PruebaEncogimientoRevirado
        Inherits PruebaBase
        Implements IDisposable


#Region " Declaración de Variables Miembro "

        Private m_strUsuario As String

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region


#Region " Definción de constructores "
        Sub New()
            m_strUsuario = String.Empty
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub

#End Region


#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function Insertar(ByVal numeroPrueba As String, _
                            ByVal fechaPrueba As Date, _
                            ByVal codigoFicha As String, _
                            ByVal secuenciaOperacion As String, _
                            ByVal codigoMaquina As String, _
                            ByVal velocidadMaquina As Double, _
                            ByVal temperatura As Double, _
                            ByVal anchoMuestra As Double, _
                            ByVal encogimientoMaquina As Double, _
                            ByVal encogimientoUrdimbre As Double, _
                            ByVal encogimientoTrama As Double, _
                            ByVal tipoRevision As String, _
                            ByVal nivelRevision As String, _
                            ByVal anchoLavadoCalculado As Double, _
                            ByVal anchoLavadoMedido As Double, _
                            ByVal reviradoDerecho As Double, _
                            ByVal reviradoCentro As Double, _
                            ByVal reviradoIzquierdo As Double, _
                            ByVal turno As String, _
                            ByVal usuario As String, _
                            ByVal maquina_procedencia As String, _
                            ByVal secuenciaLab As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "secuenciaOperacion", secuenciaOperacion, _
                                          "codigoMaquina", codigoMaquina, _
                                          "velocidadMaquina", velocidadMaquina, _
                                          "temperatura", temperatura, _
                                          "anchoMuestra", anchoMuestra, _
                                          "encogimientoMaquina", encogimientoMaquina, _
                                          "encogimientoUrdimbre", encogimientoUrdimbre, _
                                          "encogimientoTrama", encogimientoTrama, _
                                          "tipoRevision", tipoRevision, _
                                          "nivelRevision", nivelRevision, _
                                          "anchoLavadoCalculado", anchoLavadoCalculado, _
                                          "anchoLavadoMedido", anchoLavadoMedido, _
                                          "reviradoDerecho", reviradoDerecho, _
                                          "reviradoCentro", reviradoCentro, _
                                          "reviradoIzquierdo", reviradoIzquierdo, _
                                          "turno", turno, _
                                          "usuario", usuario, _
                                          "MaqProcedencia", maquina_procedencia, _
                                          "SecLaboratorio", secuenciaLab}

            Try
                Dim strCadena As String
                strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaEncogimientoRevirado_3", parametros), String)

                If strCadena <> "" Then
                    Return strCadena
                Else
                    Return ""
                End If

            Catch ex As Exception
                Return ""
            End Try

        End Function
        Public Function Insertar_V2(ByVal numeroPrueba As String, _
                      ByVal fechaPrueba As Date, _
                      ByVal codigoFicha As String, _
                      ByVal secuenciaOperacion As String, _
                      ByVal codigoMaquina As String, _
                      ByVal velocidadMaquina As Double, _
                      ByVal temperatura As Double, _
                      ByVal anchoMuestra As Double, _
                      ByVal encogimientoMaquina As Double, _
                      ByVal encogimientoUrdimbre As Double, _
                      ByVal encogimientoTrama As Double, _
                      ByVal tipoRevision As String, _
                      ByVal nivelRevision As String, _
                      ByVal anchoLavadoCalculado As Double, _
                      ByVal anchoLavadoMedido As Double, _
                      ByVal reviradoDerecho As Double, _
                      ByVal reviradoCentro As Double, _
                      ByVal reviradoIzquierdo As Double, _
                      ByVal turno As String, _
                      ByVal usuario As String, _
                      ByVal maquina_procedencia As String, _
                      ByVal secuenciaLab As String, ByVal aprobacion As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "secuenciaOperacion", secuenciaOperacion, _
                                          "codigoMaquina", codigoMaquina, _
                                          "velocidadMaquina", velocidadMaquina, _
                                          "temperatura", temperatura, _
                                          "anchoMuestra", anchoMuestra, _
                                          "encogimientoMaquina", encogimientoMaquina, _
                                          "encogimientoUrdimbre", encogimientoUrdimbre, _
                                          "encogimientoTrama", encogimientoTrama, _
                                          "tipoRevision", tipoRevision, _
                                          "nivelRevision", nivelRevision, _
                                          "anchoLavadoCalculado", anchoLavadoCalculado, _
                                          "anchoLavadoMedido", anchoLavadoMedido, _
                                          "reviradoDerecho", reviradoDerecho, _
                                          "reviradoCentro", reviradoCentro, _
                                          "reviradoIzquierdo", reviradoIzquierdo, _
                                          "turno", turno, _
                                          "usuario", usuario, _
                                          "MaqProcedencia", maquina_procedencia, _
                                          "SecLaboratorio", secuenciaLab, _
                                          "aprobacion", aprobacion}

            Try
                Dim strCadena As String
                strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaEncogimientoRevirado_V2", parametros), String)

                If strCadena <> "" Then
                    Return strCadena
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Function


		Function ContarFichasSinGramaje(ByVal pstr_CodigoArticulo As String, ByVal pstrCodigoFicha As String) As Int16

			Dim intNumeroFichas As Int16
			Dim objParametros() As Object = {"var_CodigoArticulo", pstr_CodigoArticulo, "var_CodigoFicha", pstrCodigoFicha}

			intNumeroFichas = Convert.ToInt16(m_sqlDtAccCalidadTintoreria.ObtenerValor("usp_qry_ContarFichasSinGramaje", objParametros))

			Return intNumeroFichas

		End Function

		Function ObtenerCodigo() As String

			Dim codigoPrueba As String

			codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaEncogimiento"))

			Return codigoPrueba

		End Function

		Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

			Dim dtPrueba As DataTable
			Dim parametros() As Object = {"numeroPrueba", numeroPrueba}

			Try
				dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaEncogimiento", parametros)

			Catch ex As Exception
				Throw ex
			End Try

			Return dtPrueba

		End Function

		Function ListarPrueba() As DataTable

			Dim dtPrueba As DataTable
			Try
				dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_BUSQUEDA_PRUEBAS_ENCOGIMIENTO")
			Catch ex As Exception
				Throw ex
			End Try
			Return dtPrueba
		End Function

		Public Function ObtenerPruebasFicha(ByVal numeroFicha As String) As DataSet
			Dim ds As DataSet
			Dim parametros() As Object = {"codigo_ficha", numeroFicha}

			Try
				ds = m_sqlDtAccCalidadTintoreria.ObtenerDataSet("spSEL_InformacionPruebasXFicha", parametros)

			Catch ex As Exception
				Throw ex
			End Try
			Return ds
		End Function




		Public Function ObtenerPruebaStandares(ByVal codigo_Articulo_largo As String) As DataTable

			Dim dtPrueba As DataTable
			Dim parametros() As Object = {"codigo_articulo_largo", codigo_Articulo_largo}

			Try
				dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_TraerValorEstandarEncogimiento", parametros)

			Catch ex As Exception
				Throw ex
			End Try

			Return dtPrueba

		End Function

		Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

			Dim dtPrueba As DataTable
			Dim parametros() As Object = {"numeroFicha", numeroFicha}

			Try
				dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaEncogimientoPorFicha", parametros)

			Catch ex As Exception
				Throw ex
			End Try

			Return dtPrueba

		End Function

		Public Function ObtenerPermisosPrueba(ByVal var_NumeroFicha As String) As DataTable
			Dim dtPrueba As DataTable
			Dim parametros() As Object = {"numeroFicha", var_NumeroFicha}

			Try
				dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_ListaPruebasEncogimiento", parametros)

			Catch ex As Exception
				Throw ex
			End Try

			Return dtPrueba
		End Function

		'------------------------------------------------------------------------------------------------
		Public Sub Dispose() Implements System.IDisposable.Dispose

		End Sub

#End Region

#Region "GIANCARLO VIDAL : OBTIENE PRUEBAS "

        Public Function ObtenerPrueba_Encogimiento_Revirado(ByVal strNumeroPrueba As Integer) As DataTable

            Try

                Dim Parametros() As Object = {"NUMERO_PRUEBA", strNumeroPrueba}
                Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_PRUEBAS_ENCOGIMIENTO_REVIRADO_3", Parametros)

            Catch ex As Exception

            End Try

        End Function

#End Region

#Region "OBTIENE LAS RUTA DE LA PRUEBA"

        Public Function ObtenerRuta(ByVal strCodigoArticulo As String) As DataTable

            Try

                Dim Parametros() As Object = {"codigo_articulo", strCodigoArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_OBTENER_RUTAPRODUCCION_ARTICULO_CALIDAD", Parametros)

            Catch ex As Exception

            End Try

        End Function

#End Region

#Region "OBTIENE EL ARTICULO LARGO DE OFISIS"

        Public Function ObtenerArticuloLargo(ByVal strCodigoArticulo As String) As String

            Try

                Dim Parametros() As Object = {"CODIGO_ARTICULO_OFISIS", strCodigoArticulo}
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("SP_OBTIENE_ARTICULO_LARGO", Parametros), String)

            Catch ex As Exception

            End Try

        End Function


#End Region

#Region "OBTIENE CODIGO DE OPERARIO"

        Public Function ObtenerOperario(ByVal strNombreUsuario As String) As String

            Try

                Dim Parametros() As Object = {"USUARIO", strNombreUsuario}
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("OBTIENE_CODIGOOPERARIO", Parametros), String)

            Catch ex As Exception

            End Try

        End Function

#End Region

        'REQSIS201700003 - DG - INI
#Region "OBTIENE LAS RUTAS DE LA PRUEBA CALIDAD"
        'Public Function ObtenerRutaCalidad(ByVal strCodigoArticulo As String, ByVal strCodigoFicha As String, ByVal strTipo As String) As DataTable

        '    Try

        '        Dim Parametros() As Object = {"codigo_articulo", strCodigoArticulo, "codigo_ficha", strCodigoFicha, "tipo", strTipo}
        '        Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_OBTENER_RUTAPRODUCCION_ARTICULO_CALIDAD_APROBACION", Parametros)

        '    Catch ex As Exception

        '    End Try

        'End Function
        Public Function ObtenerRutaCalidad(ByVal strCodigoArticulo As String, ByVal strCodigoFicha As String) As DataTable

            Try

                Dim Parametros() As Object = {"codigo_articulo", strCodigoArticulo, "codigo_ficha", strCodigoFicha}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_OBTENER_RUTAPRODUCCION_ARTICULO_CALIDAD_APROBACION", Parametros)

            Catch ex As Exception

            End Try

        End Function
#End Region
        'REQSIS201700003 - DG - FIN


    End Class

End Namespace