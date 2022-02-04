Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class PruebaResistencia
        Inherits PruebaBase
        Implements IDisposable


#Region " Declaración de Variables Miembro "

        Private m_strUsuario As String

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region


#Region " Definción de constructores "
        Sub New()
            m_strUsuario = String.Empty
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub

#End Region


#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function ObtenerCodigo() As String

            Dim codigoPrueba As String

            codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaResistencia"))

            Return codigoPrueba

        End Function

        Public Function Insertar(ByVal numeroPrueba As String, _
                            ByVal fechaPrueba As Date, _
                            ByVal codigoFicha As String, _
                            ByVal tipoTela As String, _
                            ByVal descripcionOtroTipo As String, _
                            ByVal urdimbreCrudoKgF As Double, _
                            ByVal tramaCrudoKgF As Double, _
                            ByVal resTraccionUrdimbreKgF As Double, _
                            ByVal resTraccionUrdimbreLbF As Double, _
                            ByVal resTraccionTramaKgF As Double, _
                            ByVal resTraccionTramaLbF As Double, _
                            ByVal resRasgoUrdimbreKgF As Double, _
                            ByVal resRasgoUrdimbreLbF As Double, _
                            ByVal resRasgoTramaKgF As Double, _
                            ByVal resRasgoTramaLbF As Double, _
                            ByVal resPilingUrdimbre As Double, _
                            ByVal resPilingTrama As Double, _
                            ByVal desHiloUrdimbre As Double, _
                            ByVal desHiloTrama As Double, _
                            ByVal desCosturaUrdimbre As Double, _
                            ByVal desCosturaTrama As Double, _
                            ByVal qtyHiloUrdimbre As Double, _
                            ByVal qtyHiloTrama As Double, _
                            ByVal qtyCosturaUrdimbre As Double, _
                            ByVal qtyCosturaTrama As Double, _
                            ByVal diagnostico As String, _
                            ByVal usuario As String) As String


            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "codigoTipoTela", tipoTela, _
                                          "descripcionOtroTipoTela", descripcionOtroTipo, _
                                          "kgFUrdimbreCrudo", urdimbreCrudoKgF, _
                                          "kgFTramaCrudo", tramaCrudoKgF, _
                                          "resTraccionUrdimbre_KgF", resTraccionUrdimbreKgF, _
                                          "resTraccionUrdimbre_LbF", resTraccionUrdimbreLbF, _
                                          "resistencia_traccion_trama_KgF", resTraccionTramaKgF, _
                                          "resistencia_traccion_trama_LbF", resTraccionTramaLbF, _
                                          "resitencia_rasgo_urdimbre_KgF", resRasgoUrdimbreKgF, _
                                          "resitencia_rasgo_urdimbre_LbF", resRasgoUrdimbreLbF, _
                                          "resitencia_rasgo_trama_KgF", resRasgoTramaKgF, _
                                          "resitencia_rasgo_trama_LbF", resRasgoTramaLbF, _
                                          "resitencia_piling_urdimbre", resPilingUrdimbre, _
                                          "resitencia_piling_trama", resPilingTrama, _
                                          "deslizamiento_hilo_urdimbre", desHiloUrdimbre, _
                                          "deslizamiento_hilo_trama", desHiloTrama, _
                                          "deslizamiento_costura_urdimbre", desCosturaUrdimbre, _
                                          "deslizamiento_costura_trama", desCosturaTrama, _
                                          "cantidad_hilo_urdimbre", qtyHiloUrdimbre, _
                                          "cantidad_hilo_trama", qtyHiloTrama, _
                                          "cantidad_costura_urdimbre", qtyCosturaUrdimbre, _
                                          "cantidad_costura_trama", qtyCosturaTrama, _
                                          "diagnostico", diagnostico, _
                                          "usuario", usuario}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaResistencia_3", parametros), String)


            Catch ex As Exception
                Return ""
            End Try

        End Function
        Public Function Insertar_V2(ByVal codigoFicha As String, _
                    ByVal tipoTela As String, _
                    ByVal descripcionOtroTipo As String, _
                    ByVal urdimbreCrudoKgF As String, _
                    ByVal tramaCrudoKgF As String, _
                    ByVal resTraccionUrdimbreKgF As String, _
                    ByVal resTraccionUrdimbreLbF As String, _
                    ByVal resTraccionTramaKgF As String, _
                    ByVal resTraccionTramaLbF As String, _
                    ByVal resRasgoUrdimbreKgF As String, _
                    ByVal resRasgoUrdimbreLbF As String, _
                    ByVal resRasgoTramaKgF As String, _
                    ByVal resRasgoTramaLbF As String, _
                    ByVal resPilingUrdimbre As String, _
                    ByVal resPilingTrama As String, _
                    ByVal desHiloUrdimbre As String, _
                    ByVal desHiloTrama As String, _
                    ByVal desCosturaUrdimbre As String, _
                    ByVal desCosturaTrama As String, _
                    ByVal qtyHiloUrdimbre As String, _
                    ByVal qtyHiloTrama As String, _
                    ByVal qtyCosturaUrdimbre As String, _
                    ByVal qtyCosturaTrama As String, _
                    ByVal diagnostico As String, _
                    ByVal usuario As String, ByVal aprobacion As String) As String


            Dim parametros As Object() = {"codigoFicha", codigoFicha, _
                                          "codigoTipoTela", tipoTela, _
                                          "descripcionOtroTipoTela", descripcionOtroTipo, _
                                          "kgFUrdimbreCrudo", urdimbreCrudoKgF, _
                                          "kgFTramaCrudo", tramaCrudoKgF, _
                                          "resTraccionUrdimbre_KgF", resTraccionUrdimbreKgF, _
                                          "resTraccionUrdimbre_LbF", resTraccionUrdimbreLbF, _
                                          "resistencia_traccion_trama_KgF", resTraccionTramaKgF, _
                                          "resistencia_traccion_trama_LbF", resTraccionTramaLbF, _
                                          "resitencia_rasgo_urdimbre_KgF", resRasgoUrdimbreKgF, _
                                          "resitencia_rasgo_urdimbre_LbF", resRasgoUrdimbreLbF, _
                                          "resitencia_rasgo_trama_KgF", resRasgoTramaKgF, _
                                          "resitencia_rasgo_trama_LbF", resRasgoTramaLbF, _
                                          "resitencia_piling_urdimbre", resPilingUrdimbre, _
                                          "resitencia_piling_trama", resPilingTrama, _
                                          "deslizamiento_hilo_urdimbre", desHiloUrdimbre, _
                                          "deslizamiento_hilo_trama", desHiloTrama, _
                                          "deslizamiento_costura_urdimbre", desCosturaUrdimbre, _
                                          "deslizamiento_costura_trama", desCosturaTrama, _
                                          "cantidad_hilo_urdimbre", qtyHiloUrdimbre, _
                                          "cantidad_hilo_trama", qtyHiloTrama, _
                                          "cantidad_costura_urdimbre", qtyCosturaUrdimbre, _
                                          "cantidad_costura_trama", qtyCosturaTrama, _
                                          "diagnostico", diagnostico, _
                                          "usuario", usuario, "aprobacion", aprobacion}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaResistencia_V2", parametros), String)


            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"NUMERO_PRUEBA", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaResistencia_2", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function
        Function BuscarPrueba(ByVal ficha As String, ByVal fecha As String) As DataSet

            Dim dtPrueba As DataSet

            Dim parametros() As Object = {"ficha", ficha, "fecha", fecha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataSet("sp_ListaPruebasGramaje", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function


        'GUIDOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        Function ListarPruebasResitencia() As DataTable

            Dim dtPrueba As DataTable
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_TODOS_PRUEBA_RESISTENCIA")

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function
        'Este metodo devuelve el codigo de articulo junto con su acabado segun número de ficha
        'UP_ObtenerPRUEBASOLIDEZDetalleArticulo_por_numFicha

        Function ObtenerAcabado(ByVal pnumeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"pNumeroFicha", pnumeroFicha}

            Try
                dtPrueba = m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerDetalleArticulo_por_numFicha", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function



        Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroFicha", numeroFicha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaResistenciaPorFicha", parametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtPrueba

        End Function

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccCalidadTintoreria.Dispose()
        End Sub

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
    End Class

End Namespace