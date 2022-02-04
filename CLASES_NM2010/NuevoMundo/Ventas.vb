Imports NM.AccesoDatos

Public Class Ventas
#Region "Variables"
	Private _objConnexion As AccesoDatosSQLServer
#End Region
#Region "Propiedades"

#End Region
#Region "Constructores"
	Sub New()

	End Sub
#End Region
#Region "Metodos y Funciones"
	Public Function ufn_ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
		Try
			_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
			Dim objParametros() As Object = {"p_var_Codigocliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
			Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
    End Function

    Public Function VentasAnualAClientes_Obtener(ByVal int_Anno As Int16) As DataTable
        Dim lstrParametros() As String = {"var_Empresa", "01", "int_Anio", CInt(int_Anno)}
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Return _objConnexion.ObtenerDataTable("USP_VEN_VentasMEXClienteXAnio", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerTipoDocumentoCliente() As DataTable
        Dim lstrParametros() As String = {}
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Return _objConnexion.ObtenerDataTable("USP_VEN_OBTENER_TIPO_DOCUMENTO_CLIENTE", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerDetalleDocumentoCliente(ByVal strCodigoEmpresa As String, ByVal strCodigoUnidad As String, ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String) As DataTable
        Dim lstrParametros() As String = {"pvar_coempr", strCodigoEmpresa, _
                                          "pvar_counid", strCodigoUnidad, _
                                          "pvar_tidocu", strTipoDocumento, _
                                          "pvar_nudocu", strNumeroDocumento}
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Return _objConnexion.ObtenerDataTable("USP_VEN_OBTENER_DETALLE_DOCUMENTO_CLIENTE", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'PRUEBA INICIO

    Public Function Obtener_Datos_Despacho_Pedido(ByVal pstrCodigoDespacho As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"var_NumeroPedido", pstrCodigoDespacho}
            Return _objConnexion.ObtenerDataTable("USP_VEN_LISTARDESPACHOPEDIDO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Obtener_Datos_Despacho_Cambio(ByVal pstrCodigoDespacho As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"var_NumeroPedido", pstrCodigoDespacho}
            Return _objConnexion.ObtenerDataTable("USP_VEN_LISTARDESPACHOCAMBIO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerPedidosPrestashop(ByVal pstrEmpresa As String, ByVal pstrFecha As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                             "ISFECHA", pstrFecha}
            Return _objConnexion.ObtenerDataTable("USP_BT_PEDIDOS_PENDIENTES_LISTAR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerPedidosPrestashopReimprimir(ByVal pstrEmpresa As String, ByVal pstrFecha As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                             "ISFECHA", pstrFecha}
            Return _objConnexion.ObtenerDataTable("USP_BT_PEDIDOS_LISTAR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ProcesarPedido(ByVal Pedido As String, ByVal Empresa As String, ByVal Usuario As String, ByVal strCodLugar As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", Empresa, _
                                            "ISNU_PEDI", Pedido, _
                                             "ISCO_USUA_MODI", Usuario, _
                                             "ISCO_LUGAR", strCodLugar}
            Return _objConnexion.ObtenerDataTable("USP_BT_GENERA_DOCUMENTOS_PRESTASHOP", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListarDocumentosFacturasYBoletas(ByVal pstrEmpresa As String, ByVal pstrFecha As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                            "ISFECHA", pstrFecha}
            Return _objConnexion.ObtenerDataTable("USP_LISTAR_DOCS_OCTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerDatosFactura(ByVal pstrEmpresa As String, ByVal pstrDocu As String) As DataSet
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"CO_EMPR", pstrEmpresa, _
                                            "NU_DOCU", pstrDocu}
            Return _objConnexion.ObtenerDataSet("USP_BT_GENERA_ARCHIVO_TEXTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerRuta_Boleta_Factura(ByVal pstrNuDocu As String, ByVal pstrDatatable As DataTable, ByVal pstrTipoarchivo As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim clsUtilitario As New NM_General.Util

            Dim objParametros() As Object = {"NU_DOCU", pstrNuDocu, _
                                             "TI_ARC", pstrTipoarchivo, _
                                            "XML_DATA", clsUtilitario.GeneraXml(pstrDatatable)}
            Return _objConnexion.ObtenerDataTable("USP_OBTENER_RUTA_ARCHIVO_DOCU_v2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActualizarDocumentoOCTO(ByVal pstrEmpresa As String, ByVal pstrNuDocu As String, ByVal pstrEstado As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)

            Dim objParametros() As Object = {"CO_EMPR", pstrEmpresa, _
                                            "NU_DOCU", pstrNuDocu, _
                                             "ESTADO", pstrEstado}

            Return _objConnexion.ObtenerDataTable("USP_ACTUALIZAR_ESTADO_DOCUMENTO_OCTO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerDatosPedidosCliente(ByVal strDocu As String, ByVal strEmpresa As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)

            Dim objParametros() As Object = {"NU_DOCU", strDocu, _
                                             "CO_EMPR", strEmpresa}

            Return _objConnexion.ObtenerDataTable("USP_OBTENER_DATOS_CLIENTE_POR_PEDIDO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function USP_VALIDAR_ARCHIVOS_PROCESADOS(ByVal pstrDatatable As DataTable) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim clsUtilitario As New NM_General.Util
            Dim objParametros() As Object = {"XML_DATA", clsUtilitario.GeneraXml(pstrDatatable)}

            Return _objConnexion.ObtenerDataTable("USP_VALIDAR_ARCHIVOS_PROCESADOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListarPedidosDespachadosYProcesadoSunat(ByVal pstrEmpresa As String, ByVal pstrFecha As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                            "ISFECHA", pstrFecha}
            Return _objConnexion.ObtenerDataTable("USP_LISTAR_PEDIDOS_DESPACHADOS_Y_PROCESADOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerDatosPedidoDespachoClientes(ByVal pstrNudocu As String, ByVal pstrEmpresa As String, ByVal pstrTipDev As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                            "ISNUDOCU", pstrNudocu, _
                                             "ISTIP", pstrTipDev}
            Return _objConnexion.ObtenerDataTable("USP_OBTENER_DATOS_DESPACHO_CLIENTE", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidarVSGeneradoPorOfisis(ByVal pstrEmpresa As String, ByVal pstrNuPedi As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                            "ISNUPEDI", pstrNuPedi}
            Return _objConnexion.ObtenerDataTable("USP_VALIDAR_VS_GENERADO_OFISIS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerProductosConStock(ByVal pstrEmpresa As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa}
            Return _objConnexion.ObtenerDataTable("USP_OBTENER_PRODUCTOS_CON_STOCK", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IngresoDetallePedido(ByVal pstrEmpresa As String, ByVal pstrNupedi As String, ByVal pstrCoItem As String, ByVal pstrCantidad As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                             "ISNUPEDI", pstrNupedi, _
                                             "ISCOITEM", pstrCoItem, _
                                             "ISCANTIDAD", pstrCantidad}
            Return _objConnexion.ObtenerDataTable("USP_INGRESO_DETALLE_PEDIDO_OCTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerDetallePorPedido(ByVal pstrNuPedi As String, ByVal pstrEmpresa As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", pstrEmpresa, _
                                             "ISNUPEDI", pstrNuPedi}
            Return _objConnexion.ObtenerDataTable("USP_OBTENER_DETALLE_PEDIDO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerDatosDocumentoAnulado(ByVal pstrEmpresa As String, ByVal pstrDocu As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"CO_EMPR", pstrEmpresa, _
                                             "NU_DOCU", pstrDocu}
            Return _objConnexion.ObtenerDataTable("USP_BT_GENERA_ARCHIVO_BAJA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AnularDocumento(ByVal pstrUsuario As String, ByVal pstrEmpresa As String, ByVal pstrCoUnid As String, ByVal pstrCoalma As String, ByVal pstrTidocu As String, ByVal pstrNudocu As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISUSUARIO", pstrUsuario, _
                                             "ISEMPRESA", pstrEmpresa, _
                                             "ISUNIDAD", pstrCoUnid, _
                                             "ISCOALMA", pstrCoalma, _
                                             "ISTIDOCU", pstrTidocu, _
                                             "ISNUDOCU", pstrNudocu}
            Return _objConnexion.ObtenerDataTable("USP_ANULAR_DOCUMENTO_OCTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerDocumentos(ByVal pstrEmpresa As String, ByVal pstrEstadoDocuSunat As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"CO_EMPR", pstrEmpresa, _
                                             "ESTADO_SUNAT", pstrEstadoDocuSunat}
            Return _objConnexion.ObtenerDataTable("USP_BT_OBTENER_DOCUMENTOS_POR_ESTADO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerDatosDocumentos(ByVal pstrEmpresa As String, ByVal pstrNuDocu As String) As DataSet
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"CO_EMPR", pstrEmpresa, _
                                        "NU_DOCU", pstrNuDocu}
        Return _objConnexion.ObtenerDataSet("USP_BT_GENERA_ARCHIVO_TEXTO_OCTO", objParametros)
    End Function
    Public Function ObtenerDatosDocumentosV2(ByVal pstrEmpresa As String, ByVal pstrNuDocu As String) As DataSet
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"CO_EMPR", pstrEmpresa, _
                                        "NU_DOCU", pstrNuDocu}
        Return _objConnexion.ObtenerDataSet("USP_BT_GENERA_ARCHIVO_TEXTO_OCTO_V3", objParametros)
    End Function
    'PRUEBA FIN
#End Region
End Class
