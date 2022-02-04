Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

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

            Dim parametros As Object() = { _
                          "numeroPrueba", numeroPrueba, _
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
        Public Function Insertar_V2(ByVal numeroPrueba As Integer, _
                      ByVal fechaPrueba As Date, _
                      ByVal codigoFicha As String, _
                      ByVal secuenciaOperacion As String, _
                      ByVal codigoMaquina As String, _
                      ByVal velocidadMaquina As String, _
                      ByVal temperatura As String, _
                      ByVal anchoMuestra As String, _
                      ByVal tipoprueba_pasadas As String, _
                      ByVal encogimientoMaquina As String, _
                      ByVal encogimientoUrdimbre As String, _
                      ByVal encogimientoTrama As String, _
                      ByVal tipoRevision As String, _
                      ByVal nivelRevision As String, _
                      ByVal anchoLavadoCalculado As Double, _
                      ByVal anchoLavadoMedido As String, _
                      ByVal reviradoDerecho As String, _
                      ByVal reviradoCentro As String, _
                      ByVal reviradoIzquierdo As String, _
                      ByVal turno As String, _
                      ByVal usuario As String, _
                      ByVal maquina_procedencia As String, _
                      ByVal secuenciaLab As String, _
                      ByVal aprobacion As String, _
                      ByVal revision_dato As Integer, _
                      ByVal pstrAccion As String, _
                      ByVal pstrCODIDO_TIPO_REVISION As String, ByVal Codigo_Dato As String) As String

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                      "fechaPrueba", fechaPrueba, _
                      "codigoFicha", codigoFicha, _
                      "secuenciaOperacion", secuenciaOperacion, _
                      "codigoMaquina", codigoMaquina, _
                      "velocidadMaquina", IIf(velocidadMaquina = "", DBNull.Value, velocidadMaquina), _
                      "temperatura", IIf(temperatura = "", DBNull.Value, temperatura), _
                      "anchoMuestra", IIf(anchoMuestra = "", DBNull.Value, anchoMuestra), _
                      "tipoprueba_pasadas", tipoprueba_pasadas, _
                      "encogimientoMaquina", IIf(encogimientoMaquina = "", DBNull.Value, encogimientoMaquina), _
                      "encogimientoUrdimbre", IIf(encogimientoUrdimbre = "", DBNull.Value, encogimientoUrdimbre), _
                      "encogimientoTrama", encogimientoTrama, _
                      "tipoRevision", tipoRevision, _
                      "nivelRevision", nivelRevision, _
                      "anchoLavadoCalculado", anchoLavadoCalculado, _
                      "anchoLavadoMedido", IIf(anchoLavadoMedido = "", DBNull.Value, anchoLavadoMedido), _
                      "reviradoDerecho", reviradoDerecho, _
                      "reviradoCentro", reviradoCentro, _
                      "reviradoIzquierdo", reviradoIzquierdo, _
                      "turno", turno, _
                      "usuario", usuario, _
                      "MaqProcedencia", maquina_procedencia, _
                      "SecLaboratorio", secuenciaLab, _
                      "aprobacion", aprobacion, _
                      "Revision_Dato", revision_dato, _
                      "Accion", pstrAccion, _
                      "CODIDO_TIPO_REVISION", pstrCODIDO_TIPO_REVISION, _
                      "Codigo_Dato", Codigo_Dato}

            Try
                Dim strCadena As String
                strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("usp_CAL_Encogimiento_Proceso", parametros), String)

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

        Public Function ObtenerPruebaStandares(ByVal codigo_Articulo_largo As String, ByVal Codigo_Prueba As String, ByVal Accion As String, ByVal Revision_Dato As Integer) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"Codigo_Dato", codigo_Articulo_largo, "Codigo_Prueba", Codigo_Prueba, "Accion", Accion, "Revision_Dato", Revision_Dato}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_Estandares_Obtener", parametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function

        Public Function ObtenerAnchoStandar(ByVal codigo_Articulo As String) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"CodigoArticulo", codigo_Articulo}
            Try
                Dim _objConexion As AccesoDatosSQLServer
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dtPrueba = _objConexion.ObtenerDataTable("USP_AnchoEstandar_Obtener", parametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function

        Public Function ObtenerAnchoMuestra(ByVal codigo_Articulo As String) As Object
            Dim strAncho As Object
            Dim parametros() As Object = {"prmCodigoDato", codigo_Articulo}
            Try
                Dim _objConexion As AccesoDatosSQLServer
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                strAncho = _objConexion.ObtenerValor("usp_CAL_Articulo_Estandar_Obtener", parametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return strAncho
        End Function

        Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"numeroFicha", numeroFicha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_PruebaEncogimientoRevirado_Listar", parametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtPrueba

        End Function

        Public Function ObtenerPermisosPrueba(ByVal var_NumeroFicha As String, ByVal pstrNum_Prueba As String) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"numeroFicha", var_NumeroFicha, "Numero_Prueba", pstrNum_Prueba}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_Encogimiento_Obtener", parametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function
        '------------------------------------------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

        Public Function Listar(ByVal strCodigoFicha As String, ByVal strTipoPrueba As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            Try
                Dim objParametros() As String = {"var_codigo_ficha", strCodigoFicha, "Tipo_Prueba", strTipoPrueba}
                ldtDatos = lobjTinto.ObtenerDataTable("usp_CAL_PruebaEncogimientoRevirado_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtDatos
        End Function

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

#Region "Obtener Tipo Revision"
        Public Function ObtenerTipoRevision(ByVal pstrCodigoTabla As String) As DataTable
            Dim dtTiposTenido As DataTable
            Dim m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim Parametros() As Object = {"chr_CodigoTabla", pstrCodigoTabla}
                dtTiposTenido = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", Parametros)
                Return dtTiposTenido

            Catch ex As Exception

            End Try
        End Function
#End Region

        'REQSIS201700003 - DG - INI
#Region "OBTENER ULTIMA SECUENCIA DE OPERACION"
        Public Function ObtenerUltimaSecuenciaOperacionRegistrada(ByVal codigo_articulo As String, ByVal codigo_ficha As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objParametros() As String = {"var_codigo_articulo", codigo_articulo, "var_codigo_ficha", codigo_ficha}
                ldtDatos = lobjTinto.ObtenerDataTable("usp_Obtener_ultima_secuencia_operacion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtDatos
        End Function
#End Region
        'REQSIS201700003 - DG - FIN
    End Class

End Namespace