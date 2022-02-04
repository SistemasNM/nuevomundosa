Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class PruebaGramaje
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

            codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaGramaje"))

            Return codigoPrueba

        End Function

        Public Function Insertar_V2(ByVal codigoFicha As String, _
                           ByVal codigoTelar As String, _
                           ByVal pesoCrudo_grm2 As Double, _
                           ByVal pesoCrudo_ozyd2 As Double, _
                           ByVal pesoAcabado_grm2 As Double, _
                           ByVal pesoAcabado_ozyd2 As Double, _
                           ByVal pesoLavado_grm2 As Double, _
                           ByVal pesoLavado_ozyd2 As Double, _
                           ByVal usuario As String, _
                           ByVal strMaquinaProcedencia As String, _
                           ByVal strSecuenciaLab As String, _
                           ByVal strSecuenciaOperacion As String, _
                           ByVal strCodigoMaquina As String, ByVal aprobacion As String) As String

            Dim parametros As Object() = {"codigoFicha", codigoFicha, _
                                          "codigoTelar", codigoTelar, _
                                          "pesoCrudo_grm2", pesoCrudo_grm2, _
                                          "pesoCrudo_ozyd2", pesoCrudo_ozyd2, _
                                          "pesoAcabado_grm2", pesoAcabado_grm2, _
                                          "pesoAcabado_ozyd2", pesoAcabado_ozyd2, _
                                          "pesoLavado_grm2", pesoLavado_grm2, _
                                          "pesoLavado_ozyd2", pesoLavado_ozyd2, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProcedencia, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquina, "aprobacion", aprobacion}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaGramaje_V2", parametros), String)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Insertar(ByVal numeroPrueba As String, _
                            ByVal fechaPrueba As Date, _
                            ByVal codigoFicha As String, _
                            ByVal codigoTelar As String, _
                            ByVal pesoCrudo_grm2 As Double, _
                            ByVal pesoCrudo_ozyd2 As Double, _
                            ByVal pesoAcabado_grm2 As Double, _
                            ByVal pesoAcabado_ozyd2 As Double, _
                            ByVal pesoLavado_grm2 As Double, _
                            ByVal pesoLavado_ozyd2 As Double, _
                            ByVal usuario As String, _
                            ByVal strMaquinaProcedencia As String, _
                            ByVal strSecuenciaLab As String, _
                            ByVal strSecuenciaOperacion As String, _
                            ByVal strCodigoMaquina As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "codigoTelar", codigoTelar, _
                                          "pesoCrudo_grm2", pesoCrudo_grm2, _
                                          "pesoCrudo_ozyd2", pesoCrudo_ozyd2, _
                                          "pesoAcabado_grm2", pesoAcabado_grm2, _
                                          "pesoAcabado_ozyd2", pesoAcabado_ozyd2, _
                                          "pesoLavado_grm2", pesoLavado_grm2, _
                                          "pesoLavado_ozyd2", pesoLavado_ozyd2, _
                                          "usuario", usuario, _
                                          "MAQUINA_PROCEDENCIA", strMaquinaProcedencia, _
                                          "SECUENCIA_LABORATORIO", strSecuenciaLab, _
                                          "SECUENCIA_OPERACION", strSecuenciaOperacion, _
                                          "CODIGO_MAQUINA_LABORATORIO", strCodigoMaquina}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarPruebaGramaje_2", parametros), String)

            Catch ex As Exception
                Return ""
            End Try

        End Function


        Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroPrueba", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaGramaje", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function

        Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroFicha", numeroFicha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaGramajePorFicha", parametros)

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

        'Guido

        Function ObtenerPruebaTotalDS(ByVal numeroPrueba As Integer) As DataSet

            Dim dtPrueba As DataSet

            Dim parametros() As Object = {"NUMERO_PRUEBA", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataSet("SP_LISTA_PRUEBA_GRAMAJE_2", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function
        'OTRO Modificado---- PAra buscar la prueba
        Function ObtenerPruebaTotalDS() As DataTable

            Dim dtPrueba As DataTable

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("SP_LISTA_TODOS_PRUEBA_GRAMAJE")

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function


        Function ObtenerPruebaDS(ByVal numeroPrueba As Integer) As DataSet

            Dim dtPrueba As DataSet

            Dim parametros() As Object = {"numeroPrueba", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataSet("UP_ObtenerPruebaGramaje", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

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
