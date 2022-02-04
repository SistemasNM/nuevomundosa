Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class PruebaPotencialStrecht
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

        Public Function ObtenerCodigo() As String

            Dim codigoPrueba As String

            codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaPotencialStrecht"))

            Return codigoPrueba

        End Function

        'Public Function Insertar(ByVal numeroPrueba As String, _
        '                    ByVal fechaPrueba As Date, _
        '                    ByVal codigoFicha As String, _
        '                    ByVal numeroPieza As String, _
        '                    ByVal ancho As String, _
        '                    ByVal anchoInicial As String, _
        '                    ByVal anchoLavado As Double, _
        '                    ByVal encogimientoTrama As Double, _
        '                    ByVal potencialStrecht As Double, _
        '                    ByVal usuario As String, _
        '                    ByVal strMaquinaProc As String, _
        '                    ByVal strSecuenciaLab As String, _
        '                    ByVal strSecuenciaOperacion As String, _
        '                    ByVal strCodigoMaquinaLaboratorio As String) As String

        '    Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
        '                                  "fechaPrueba", fechaPrueba, _
        '                                  "codigoFicha", codigoFicha, _
        '                                  "numeroPieza", numeroPieza, _
        '                                  "ancho", ancho, _
        '                                  "anchoInicial", anchoInicial, _
        '                                  "anchoLavado", anchoLavado, _
        '                                  "encogimientoTrama", encogimientoTrama, _
        '                                  "potencialStrecht", potencialStrecht, _
        '                                  "usuario", usuario, _
        '                                  "MAQUINA_PROCEDENCIA", strMaquinaProc, _
        '                                  "SECUENCIA_LABORATORIO", strSecuenciaLab, _
        '                                  "SECUENCIA_OPERACION", strSecuenciaOperacion, _
        '                                  "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio}

        '    Try
        '        Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaPotencialStrecht_2", parametros), String)

        '    Catch ex As Exception
        '        Return ""
        '    End Try

        'End Function

        Public Function Insertar(ByVal numeroPrueba As String, _
                            ByVal fechaPrueba As Date, _
                            ByVal codigoFicha As String, _
                            ByVal numeroPieza As String, _
                            ByVal ancho As String, _
                            ByVal anchoInicial As String, _
                            ByVal anchoLavado As Double, _
                            ByVal encogimientoTrama As Double, _
                            ByVal potencialStrecht As Double, _
                            ByVal usuario As String, _
                            ByVal strMaquinaProc As String, _
                            ByVal strSecuenciaLab As String, _
                            ByVal strSecuenciaOperacion As String, _
                            ByVal strCodigoMaquinaLaboratorio As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "numeroPieza", numeroPieza, _
                                          "ancho", ancho, _
                                          "anchoInicial", anchoInicial, _
                                          "anchoLavado", anchoLavado, _
                                          "encogimientoTrama", encogimientoTrama, _
                                          "potencialStrecht", potencialStrecht, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProc, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaPotencialStrecht_2", parametros), String)

            Catch ex As Exception
                Return ""
            End Try

        End Function

        Public Function Insertar_V2(ByVal codigoFicha As String, _
                            ByVal numeroPieza As String, _
                            ByVal ancho As String, _
                            ByVal anchoInicial As String, _
                            ByVal anchoLavado As Double, _
                            ByVal encogimientoTrama As Double, _
                            ByVal potencialStrecht As Double, _
                            ByVal usuario As String, _
                            ByVal strMaquinaProc As String, _
                            ByVal strSecuenciaLab As String, _
                            ByVal strSecuenciaOperacion As String, _
                            ByVal strCodigoMaquinaLaboratorio As String, ByVal aprobacion As String) As String

            Dim parametros As Object() = {"codigoFicha", codigoFicha, _
                                          "numeroPieza", numeroPieza, _
                                          "ancho", ancho, _
                                          "anchoInicial", anchoInicial, _
                                          "anchoLavado", anchoLavado, _
                                          "encogimientoTrama", encogimientoTrama, _
                                          "potencialStrecht", potencialStrecht, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProc, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio, "aprobacion", aprobacion}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaPotencialStrecht_V2", parametros), String)

            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"NUMERO_PRUEBA", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_PRUEBA_STRECHT_2", parametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtPrueba

        End Function

        Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroFicha", numeroFicha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaPotencialStrechtPorFicha", parametros)

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

        Public Function ObtenerPrueba_Potencial_Strecht() As DataTable

            Try


                Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_PRUEBAS_POT_STRECHT")

            Catch ex As Exception

            End Try

        End Function

        Function Obtener_ultima_Prueba() As String
            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("OBTIENE_ULTIMAPRUEBA_STRECHT"), String)

            Catch ex As Exception
                Return ""
            End Try

        End Function
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