Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class PruebaSolidez
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

            codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaSolidez"))

            Return codigoPrueba

        End Function

        Public Function Insertar(ByVal numeroPrueba As String, _
                                    ByVal fechaPrueba As Date, _
                                    ByVal codigoFicha As String, _
                                    ByVal codigoTipoTenido As String, _
                                    ByVal numeroPase As String, _
                                    ByVal codigoMaquina As String, _
                                    ByVal codigoTipoSolidez As String, _
                                    ByVal codigoConformidad As String, _
                                    ByVal especificacion As String, _
                                    ByVal diagnostico As String, _
                                    ByVal usuario As String, _
                                    ByVal strMaquinaProc As String, _
                                    ByVal strSecuenciaLab As String, _
                                    ByVal strSecuenciaOperacion As String, _
                                    ByVal strCodigoMaquinaLaboratorio As String, ByVal escala_grises As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "codigoTipoTenido", codigoTipoTenido, _
                                          "numeroPase", numeroPase, _
                                          "codigoMaquina", codigoMaquina, _
                                          "codigoTipoSolidez", codigoTipoSolidez, _
                                          "codigoConformidad", codigoConformidad, _
                                          "especificacion", especificacion, _
                                          "diagnostico", diagnostico, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProc, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio, _
                                          "escala_grises", escala_grises}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaSolidez_2_V2", parametros), String)

            Catch ex As Exception
                Return "FALLA EN EL PROCESO"
            End Try

        End Function

        Public Function Insertar_V2(ByVal numeroPrueba As String, _
                                            ByVal codigoFicha As String, _
                                            ByVal codigoTipoTenido As String, _
                                            ByVal numeroPase As String, _
                                            ByVal codigoMaquina As String, _
                                            ByVal codigoTipoSolidez As String, _
                                            ByVal codigoConformidad As String, _
                                            ByVal especificacion As String, _
                                            ByVal diagnostico As String, _
                                            ByVal usuario As String, _
                                            ByVal strMaquinaProc As String, _
                                            ByVal strSecuenciaLab As String, _
                                            ByVal strSecuenciaOperacion As String, _
                                            ByVal strCodigoMaquinaLaboratorio As String, _
                                            ByVal lavado_ct As String, _
                                            ByVal lavado_mt As String, _
                                            ByVal frote_humedo As String, _
                                            ByVal frote_seco As String, _
                                            ByVal luz_ct As String, _
                                            ByVal aprobacion As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "codigoTipoTenido", codigoTipoTenido, _
                                          "numeroPase", numeroPase, _
                                          "codigoMaquina", codigoMaquina, _
                                          "codigoTipoSolidez", codigoTipoSolidez, _
                                          "codigoConformidad", codigoConformidad, _
                                          "especificacion", especificacion, _
                                          "diagnostico", diagnostico, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProc, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquinaLaboratorio, _
                                          "lavado_ct", lavado_ct, _
                                        "lavado_mt", lavado_mt, _
                                        "frote_humedo", frote_humedo, _
                                        "frote_seco", frote_seco, _
                                        "luz_ct", luz_ct, "aprobacion", aprobacion}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaSolidez_V2", parametros), String)

            Catch ex As Exception
                Return "FALLA EN EL PROCESO"
            End Try
        End Function

        'GUIDOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO

        Function ListaTodosPruebaSolidez() As DataTable

            Dim dtPrueba As DataTable


            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_TODOS_PRUEBA_SOLIDEZ")

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function
        '---------------------------------------------------------------------
        Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"NUMERO_PRUEBA", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaSolidez_2", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function

        Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroFicha", numeroFicha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaSolidezPorFicha", parametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtPrueba

        End Function

        Public Function ObtenerEstandaresPruebaSolidez(ByVal codigo_articulo As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"codigo_articulo_largo", codigo_articulo}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_TraerValorEstandarSolidez", parametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtPrueba

        End Function

        Function ObtenerDetalleFicha(ByVal pnumeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"pNumeroFicha", pnumeroFicha}

            Try
                dtPrueba = m_sqlDtAccProduccion.ObtenerDataTable("UP_ObtenerPRUEBASOLIDEZDetalleArticulo_por_numFicha", parametros)

            Catch ex As Exception

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