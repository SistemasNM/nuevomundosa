Imports NM.AccesoDatos
Imports System.Data
Imports NM_General
Imports System.Text

Namespace NM_Tejeduria
    Public Class NM_IngresoSalidafichaReproceso

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTeje As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTeje = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub
#End Region

        Public Function genera_FichaReproceso() As String
            Return m_sqlDtAccTeje.ObtenerValor("NM_GENERA_FICHAREPROCESO") 'generar ficha
        End Function

        Public Function genera_PiezaReproceso() As String
            Return m_sqlDtAccTeje.ObtenerValor("NM_GENERA_PIEZAREPROCESO")
        End Function

        Public Sub ingresa(ByVal OrdenProduccion As String, ByVal Articulo As String, _
        ByVal Color As String, ByVal Combinacion As String, ByVal Disenio As String, ByVal Pieza As String, _
        ByVal Ficha As String, ByVal Metraje As Double, ByVal TipoD As String, ByVal NumeroD As String, _
        ByVal Aprobado As Byte, ByVal strCodigoArticuloLargo As String, ByVal Usuario As String)
            Dim objParametros As Object() = {"OrdenProduccion", OrdenProduccion, "Articulo", Articulo, "Color", Color, _
            "Combinacion", Combinacion, "Disenio", Disenio, "Pieza", Pieza, "Ficha", Ficha, _
            "Metraje", Metraje, "TipoD", TipoD, "NumeroD", NumeroD, "AP_Directa", Aprobado, _
            "CodArt_Largo", strCodigoArticuloLargo, "Usuario", Usuario}

            m_sqlDtAccTeje.EjecutarComando("NM_INGRESA_PIEZAREPROCESO", objParametros)
        End Sub

        Public Sub FichaPorReetiquetado(ByVal OrdenProduccion As String, ByVal Articulo As String, ByVal Color As String, ByVal Combinacion As String, ByVal Disenio As String, _
                                        ByVal Ficha As String, ByVal strCodigoArticuloLargo As String, ByVal Usuario As String, ByVal motivo As String, ByVal solicita As String)

            Dim objParametros As Object() = {"OrdenProduccion", OrdenProduccion, "Articulo", Articulo, "Color", Color, "Combinacion", Combinacion, _
                                             "Disenio", Disenio, "Ficha", Ficha, "CodArt_Largo", strCodigoArticuloLargo, "Usuario", Usuario, _
                                             "vch_motivo", motivo, "vch_solicita", solicita}

            m_sqlDtAccTeje.EjecutarComando("NM_INGRESA_FICHA_REETIQUETADO", objParametros)

        End Sub

        Public Sub FichaPorReetiquetado_Eliminar(ByVal strCodigoFicha As String)
            Try
                m_sqlDtAccTeje = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim objParametros As Object() = {"vch_Codigo_Ficha", strCodigoFicha}
                m_sqlDtAccTeje.EjecutarComando("Usp_NM_Ficha_Eliminar", objParametros)
            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Function Concluir_FichaReetiquetado(ByVal Ficha As String)

            Dim objParametros As Object() = {"cOpcion", "Upd", "codigo_ficha_partida", Ficha, "vch_motivo", "", "vch_solicita", ""}

            m_sqlDtAccTeje.EjecutarComando("NM_MA_FICHA_REETIQUETADO_REGISTRAR", objParametros)

        End Function

        Public Function Codigo_articuloCorto(ByVal articulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo", articulo}
                Return m_sqlDtAccTeje.ObtenerDataTable("NM_Codigo_articuloCorto", objParametros)

            Catch ex As Exception
                Dim s As String = ex.Message
            End Try
        End Function

        Public Function ObtieneDatosArticulo(ByVal articulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_articulo", articulo}
                Return m_sqlDtAccTeje.ObtenerDataTable("PR_NM_ObtenerDatosArticulo_30dg", objParametros)

            Catch ex As Exception
                Dim s As String = ex.Message
            End Try

        End Function
        Public Sub ingresa_D(ByVal OrdenProduccion As String, ByVal Articulo As String, _
        ByVal Color As String, ByVal Combinacion As String, ByVal Disenio As String, ByVal Pieza As String, _
        ByVal Ficha As String, ByVal Metraje As Double, ByVal TipoD As String, ByVal NumeroD As String, _
        ByVal Aprobado As Byte, ByVal strCodigoArticuloLargo As String, ByVal Usuario As String, ByVal numero_reclamo As String)
            Dim objParametros As Object() = {"OrdenProduccion", OrdenProduccion, "Articulo", Articulo, "Color", Color, _
            "Combinacion", Combinacion, "Disenio", Disenio, "Pieza", Pieza, "Ficha", Ficha, _
            "Metraje", Metraje, "TipoD", TipoD, "NumeroD", NumeroD, "AP_Directa", Aprobado, _
            "CodArt_Largo", strCodigoArticuloLargo, "Usuario", Usuario, "Numero_reclamo", numero_reclamo}

            m_sqlDtAccTeje.EjecutarComando("NM_INGRESA_PIEZAREPROCESO_D", objParametros)
            'm_sqlDtAccTeje.EjecutarComando("NM_INGRESA_PIEZAREPROCESO_D2", objParametros)
        End Sub

#Region "LUIS_AJ"

        Public Function ObtieneDatosFichaNueva(ByVal strOrdenProduccion As String) As DataSet
            Try
                Dim objParametros As Object() = {"pvch_OrdenProduccion", strOrdenProduccion}

                Return m_sqlDtAccTeje.ObtenerDataSet("USP_PROD_OBTENER_LISTA_DATOS_FICHA_NUEVA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtieneListaCodigoCorto(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvch_CodigoArticulo", strCodigoArticulo}

                Return m_sqlDtAccTeje.ObtenerDataTable("USP_PROD_OBTENER_LISTA_ARTICULO_CORTO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtieneListaRollosDocumentoNSA(ByVal strDocumentoNSA As String, ByVal strArticuloLargo As String, ByVal strArticuloCorto As String) As DataTable

            Try
                Dim objParametros As Object() = {"pvch_DocumentoNSA", strDocumentoNSA,
                                                 "pvch_ArticuloLargo", strArticuloLargo,
                                                 "pvch_ArticuloCorto", strArticuloCorto}

                Return m_sqlDtAccTeje.ObtenerDataTable("USP_PROD_OBTENER_LISTA_ROLLOS_DOCUMENTOS_NSA", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function GenerarFichaCinco_Rollos(ByVal strModulo As String,
                                                 ByVal strFichaGenerada As String,
                                                 ByVal strPieza As String,
                                                 ByVal strOrdenProduccion As String,
                                                 ByVal strTipoDocumento As String,
                                                 ByVal strColor As String,
                                                 ByVal strCombinacion As String,
                                                 ByVal strDisenio As String,
                                                 ByVal strArticuloLargo As String,
                                                 ByVal strArticuloCorto As String,
                                                 ByVal dblMetraje As Double,
                                                 ByVal strObservaciones As String,
                                                 ByVal strUsuario As String,
                                                 ByVal dtListaRollosFicha As DataTable) As String
            Dim objUtil As New Util

            Try
                dtListaRollosFicha.TableName = "RollosFicha"
                Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(dtListaRollosFicha))
                Dim objParametros As Object() = {"pvch_Modulo", strModulo,
                                                 "pvch_FichaGenerada", strFichaGenerada,
                                                 "pvch_Pieza", strPieza,
                                                 "pvch_OrdenProduccion", strOrdenProduccion,
                                                 "pvch_TipoDocumento", strTipoDocumento,
                                                 "pvch_Color", strColor,
                                                 "pvch_Combinacion", strCombinacion,
                                                 "pvch_Disenio", strDisenio,
                                                 "pvch_ArticuloLargo", strArticuloLargo,
                                                 "pvch_ArticuloCorto", strArticuloCorto,
                                                 "pnum_Metraje", dblMetraje,
                                                 "pvch_Observaciones", strObservaciones,
                                                 "pvch_Usuario", strUsuario,
                                                 "pxml_ListaRollosFicha", strListaRollosXML.ToString}

                Return m_sqlDtAccTeje.ObtenerValor("USP_PROD_GENERAR_FICHA_CINCO_REPROCESOS_ROLLOS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtieneDatosFichaReproceso(ByVal strFicha As String) As DataSet
            Try
                Dim objParametros As Object() = {"pvch_Ficha", strFicha}

                Return m_sqlDtAccTeje.ObtenerDataSet("USP_PROD_OBTENER_LISTA_DATOS_FICHA_REPROCESO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region

    End Class
End Namespace
