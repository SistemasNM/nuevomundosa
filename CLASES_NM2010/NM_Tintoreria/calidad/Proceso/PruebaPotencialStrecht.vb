Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class PruebaPotencialStrecht
        'Inherits PruebaBase
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
                            ByVal anchoLavado As String, _
                            ByVal encogimientoTrama As String, _
                            ByVal potencialStrecht As String, _
                            ByVal usuario As String, _
                            ByVal strMaquinaProc As String, _
                            ByVal strSecuenciaLab As String, _
                            ByVal strSecuenciaOperacion As String, _
                            ByVal strCodigoMaquinaLaboratorio As String, _
                            ByVal aprobacion As Integer, _
                            ByVal pintNumeroPrueba As Integer, _
                            ByVal pstrAccion As String, ByVal strElongacion As String) As String

            Dim parametros As Object() = {"codigoFicha", IIf(codigoFicha = "", DBNull.Value, codigoFicha), _
                                          "numeroPieza", IIf(numeroPieza = "", DBNull.Value, numeroPieza), _
                                          "ancho", IIf(ancho = "", DBNull.Value, ancho), _
                                          "anchoInicial", IIf(anchoInicial = "", DBNull.Value, anchoInicial), _
                                          "anchoLavado", IIf(anchoLavado = "", DBNull.Value, anchoLavado), _
                                          "encogimientoTrama", IIf(encogimientoTrama = "", DBNull.Value, encogimientoTrama), _
                                          "potencialStrecht", IIf(potencialStrecht = "", DBNull.Value, potencialStrecht), _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", IIf(strMaquinaProc = "", DBNull.Value, strMaquinaProc), _
                                          "SECUENCIA_LABORATORIO", IIf(strSecuenciaLab = "", DBNull.Value, strSecuenciaLab), _
                                          "SECUENCIA_OPERACION", IIf(strSecuenciaOperacion = "", DBNull.Value, strSecuenciaOperacion), _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio, _
                                          "aprobacion", aprobacion, "numero_prueba", _
                                          pintNumeroPrueba, "Accion", pstrAccion, _
                                          "Elongacion", strElongacion}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("usp_CAL_Strecht_Proceso", parametros), String)

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

        Public Overloads Function ObtenerUltimaPrueba(ByVal pstrNumeroFicha As String, ByVal pstrNumeroPrueba As String) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"numeroFicha", pstrNumeroFicha, "Numero_Prueba", pstrNumeroPrueba}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_Strecht_Obtener", parametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function

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

#Region "Obtener Pruebas Estandares"
        Public Function ObtenerPruebaStandares(ByVal codigo_Articulo_largo As String, ByVal Codigo_Prueba As String, ByVal Accion As String, ByVal Revision_Dato As Integer) As DataTable
            Dim dtPrueba As New DataTable
            Dim parametros() As Object = {"Codigo_Dato", codigo_Articulo_largo, "Codigo_Prueba", Codigo_Prueba, "Accion", Accion, "Revision_Dato", Revision_Dato}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_TraerValorEstandarEncogimiento_V3", parametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function
#End Region

    End Class

End Namespace