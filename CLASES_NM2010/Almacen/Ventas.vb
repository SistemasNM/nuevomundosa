Imports NM.AccesoDatos
Imports NM_General
Imports System.Text

Public Class Ventas
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private _objConnexion As AccesoDatosSQLServer
    Private N_Pedido As String
    Private C_Cliente As String
    Private D_Cliente As String
    Private F_Pedido As String
#End Region

#Region " Definicion de Constructores "
    Sub New()
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
    End Sub
#End Region


#Region "Metodos y Funciones"
    Public Function ufn_ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"p_var_Codigocliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
            Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ObtenerClientesxTipoConsulta(ByVal pintTipoConsulta As Int16, ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String, ByVal pstrCodigoVendedor As String, ByVal pstrNombreVendedor As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = { _
            "ptin_tipoconsulta", pintTipoConsulta, _
            "pvch_codigocliente", pstrCodigoCliente, _
            "pvch_nombrecliente", pstrNombreCliente, _
            "pvch_codigovendedor", pstrCodigoVendedor, _
            "pvch_nombrevendedor", pstrNombreVendedor}

      Return _objConnexion.ObtenerDataTable("usp_ven_clientes_listar", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExistPistoleteoPedido(ByVal PstrCia As String, ByVal PstrPedido As String) As Boolean
        Dim dtblDatos As DataTable
        Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
        dtblDatos = _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Pedidos", objparametros)
        If dtblDatos.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetDatospedido(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Dim dtblDatos As New DataTable
        Dim objParametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
        Try
            dtblDatos = _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Pedidos", objParametros)
            N_Pedido = dtblDatos.Rows(0).Item("Nu_Pedi")
            C_Cliente = dtblDatos.Rows(0).Item("Co_Clie")
            D_Cliente = dtblDatos.Rows(0).Item("No_Clie")
            F_Pedido = dtblDatos.Rows(0).Item("Fe_Pedi")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function RegistroPistoleteo(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Dim dtbResultado As DataTable
        dtbResultado = Nothing
        Try
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
            dtbResultado = _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Pedidos", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbResultado
    End Function

    '----------------------------------------
    'Modificado: Toma de inventario Nov 2014
    'Autor: Alexander Torres Cardenas
    'Nov 2014
    '----------------------------------------

    Public Function ConsultaPedidoCabecera(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Dim dtbResultado As DataTable
        dtbResultado = Nothing
        Try
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Pedidos_Cabecera", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbResultado
    End Function

    Public Function ConsultaArticulosFilas(ByVal PstrCia As String) As DataTable
        Dim dtbResultado As DataTable
        dtbResultado = Nothing
        Try
            Dim objparametros() As Object = {"var_Empresa", PstrCia}
            dtbResultado = _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Articulos_Filas", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbResultado
    End Function

    Public Function ConsultaArticulosFilas_v2(ByVal PstrCia As String) As DataTable
        Dim dtbResultado As DataTable
        dtbResultado = Nothing
        Try
            Dim objparametros() As Object = {"var_Empresa", PstrCia}
            dtbResultado = _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Articulos_Filas_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbResultado
    End Function



    Public Function ConsultaFilasPiezas(ByVal PstrCia As String, ByVal PstrFila As String) As DataTable
        Dim dtbResultado As DataTable
        dtbResultado = Nothing
        Try
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Fila", PstrFila}
            dtbResultado = _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Filas_Piezas", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbResultado
    End Function

    'cargamos las piezas desde una tabla tmp
    Public Function ConsultaFilasPiezas_2() As DataTable
        Dim dtbResultado As DataTable
        dtbResultado = Nothing
        Try
            dtbResultado = _objConnexion.ObtenerDataTable("usp_ven_consulta_filas_piezas_2")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbResultado
    End Function

    Public Sub GrabaRegistrosInventario(ByVal pstrPedido As String, _
                                         ByVal pstrInven As String, _
                                         ByVal pdtventas As DataTable)
        Dim objUtil As New Util
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""

        Try
            pdtventas.TableName = "rollos"
            strXMLData = objUtil.GeneraXml(pdtventas)
            Dim lstrParametros() As String = {"Var_Fila", pstrPedido, "Var_Inven", pstrInven, _
                                              "ntx_Detalle", strXMLData}
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            lobjVentas.EjecutarComando("Usp_Ven_InvTelas_Grabar", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'graba detalle de inventario
    Public Function GrabaDetalleInventario(ByVal pstrPedido As String, ByVal pstrInven As String, _
                                         ByVal pdtventas As DataTable) As DataTable
        Dim objUtil As New Util
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            pdtventas.TableName = "rollos"
            strXMLData = objUtil.GeneraXml(pdtventas)
            Dim lstrParametros() As String = {"Var_Fila", pstrPedido, "Var_Inven", pstrInven, "ntx_Detalle", strXMLData}
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("Usp_Ven_InvTelas_Grabar_2", lstrParametros)
        Catch ex As Exception
            Throw ex
            Return dbtResultado
        End Try
        Return dbtResultado
    End Function

    Public Function GrabaDetalleInventario_v3(ByVal pstrPedido As String, ByVal pstrInven As String, _
                                     ByVal pdtventas As DataTable) As DataTable
        Dim objUtil As New Util
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            pdtventas.TableName = "rollos"
            strXMLData = objUtil.GeneraXml(pdtventas)
            Dim lstrParametros() As String = {"Var_Fila", pstrPedido, "Var_Inven", pstrInven, "ntx_Detalle", strXMLData}
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("Usp_Ven_InvTelas_Grabar_v3", lstrParametros)
        Catch ex As Exception
            Throw ex
            Return dbtResultado
        End Try
        Return dbtResultado
    End Function



    'graba detalle de inventario
    Public Function GrabaDetalleInventario_2(ByVal pstrPedido As String, ByVal pstrInven As String, _
                                             ByVal pstrCodERI As String, ByVal pdtventas As DataTable) As DataTable
        Dim objUtil As New Util
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            pdtventas.TableName = "rollos"
            strXMLData = objUtil.GeneraXml(pdtventas)
            Dim lstrParametros() As String = {"Var_Fila", pstrPedido, "Var_Inven", pstrInven, "var_invenERI", pstrCodERI, "ntx_Detalle", strXMLData}
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("Usp_Ven_InvTelas_Grabar_3", lstrParametros)
        Catch ex As Exception
            Throw ex
            Return dbtResultado
        End Try
        Return dbtResultado
    End Function


    ''' <summary>
    ''' -- Agregado 20150922 (LUIS_AJ) --
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerListaUbicaciones() As DataTable
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("Usp_Ven_InvTelas_ObtenerUbicaciones")
        Catch ex As Exception
            Throw ex
        Finally
            lobjVentas = Nothing
        End Try
        Return dbtResultado
    End Function


    ''' <summary>
    ''' -- Agregado 20150922 (LUIS_AJ) --
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerListaInventariadores() As DataTable
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("Usp_Ven_InvTelas_ObtenerInventariadores")
        Catch ex As Exception
            Throw ex
        Finally
            lobjVentas = Nothing
        End Try
        Return dbtResultado
    End Function

    Public Function ObtenerListaInventariadores_2() As DataTable
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("Usp_Ven_InvTelas_ObtenerInventariadores_2")
        Catch ex As Exception
            Throw ex
        Finally
            lobjVentas = Nothing
        End Try
        Return dbtResultado
    End Function

    Public Function ObtenerListaInventariosERI() As DataTable
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("usp_ven_obtenerInventarioERI")
        Catch ex As Exception
            Throw ex
        Finally
            lobjVentas = Nothing
        End Try
        Return dbtResultado
    End Function


    '----------------------------------------
    'Fin modificacion
    '----------------------------------------

    Public Function ConsultaArticulosPedidos(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Articulos_Pedidos", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function ConsultaPedidoDetalle(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Pedidos_Detalle", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function ConsultaGuiaCabecera(ByVal PstrCia As String, ByVal PstrGuia As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Guia", PstrGuia}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Guia_Cabecera", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function ConsultaGuiaDetalle(ByVal PstrCia As String, ByVal PstrGuia As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Guia", PstrGuia}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Guia_Detalle", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function RegistroPistoleteoArt(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Articulos_Pedidos", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function ConsultaPedidosPiezas(ByVal PstrCia As String, ByVal PstrPedido As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"var_Empresa", PstrCia, "var_Pedido", PstrPedido}
            Return _objConnexion.ObtenerDataTable("usp_VEN_Consulta_Pedidos_Piezas", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function ConsultaPiezasPendientes() As DataTable
        Try
            Dim _objConnexionLogistica As NM.AccesoDatos.AccesoDatosSQLServer
            _objConnexionLogistica = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {}
            Return _objConnexionLogistica.ObtenerDataTable("USP_LOG_TRANSFERENCIA_ROLLOS_PENDIENTES", objparametros)
        Catch ex As Exception
        End Try
    End Function

    Public Function GrabaRegistros(ByVal pstrPedido As String, ByVal pdtventas As DataTable) As DataTable
        Dim objUtil As New Util
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        pdtventas.TableName = "rollos"
        Dim strXMLData As String = objUtil.GeneraXml(pdtventas)
        Dim lstrParametros() As String = {"Var_Pedido", pstrPedido, "ntx_Detalle", strXMLData}

        lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        lobjVentas.EjecutarComando("usp_VEN_TDPEDV_BULT_Grabar", lstrParametros)
    End Function

    Public Function GrabaDocumentos(ByVal pstrUsuario As String, ByVal pdtDocumento As DataTable) As DataTable
        Dim objUtil As New Util
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        pdtDocumento.TableName = "rollos"
        Dim strXMLData As String = objUtil.GeneraXml(pdtDocumento)
        Dim lstrParametros() As String = {"CO_USUA", pstrUsuario, "NXT_DETALLE", strXMLData}
        lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        lobjVentas.EjecutarComando("USP_LOG_TRANSFERENCIA_GRABAR", lstrParametros)
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener contactos reclamo 
    'Autor      : Juan Cucho Antunez
    'Creado     : 03/08/2016
    '*****************************************************************************************************
    Public Function ufn_ObtenerContactosReclamo(ByVal pstrNombreContacto As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"pvar_NombreContacto", pstrNombreContacto}
            Return _objConnexion.ObtenerDataTable("USP_RVF_OBTENER_CONTACTOS_RECLAMO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerCliente_Reclamos(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"pvch_CodigoCliente", pstrCodigoCliente,
                                             "pvch_NombreCliente", pstrNombreCliente}
            Return _objConnexion.ObtenerDataTable("USP_VEN_BUSCAR_CLIENTES_RECLAMOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener vendedores reclamo 
    'Autor      : Juan Cucho Antunez
    'Creado     : 19/01/2017
    '*****************************************************************************************************
    Public Function ufn_ObtenerVendedoresReclamo(ByVal pstrCodigoVendedor As String, ByVal pstrNombreVendedor As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"var_Codigo", pstrCodigoVendedor.Trim, _
                                             "var_Nombre", pstrNombreVendedor.Trim}
            Return _objConnexion.ObtenerDataTable("usp_RVF_Vendedores_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CACHINAS"
    Public Function ListarClientesCachina() As DataTable
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Return _objConnexion.ObtenerDataTable("usp_qry_ListarClientesCachina")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function VerificaRolloActivo(ByVal pCodigoRollo As String) As String
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros As Object() = {"pvch_CodigoRollo", pCodigoRollo}

            Return CType(_objConnexion.ObtenerValor("usp_qry_VerificaRolloActivo", objParametros), String).Trim
        Catch ex As Exception
            Return "ERROR: " + ex.Message
        End Try

    End Function

    Public Function ValidaRolloPermitidoMaestroCachinas(ByVal pstrRollo As String, ByVal pstrCliente As String) As DataTable
        Try
            ' _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"pstrRollo", pstrRollo, "pstrCliente", pstrCliente}
            Return _objConnexion.ObtenerDataTable("usp_qry_ValidaRolloMaestroCachina", objparametros)
        Catch ex As Exception
            'ValidaRolloPermitidoMaestroCachinas = Nothing
            Throw ex
        End Try
    End Function

    Function GeneraPedidoyGuiaCachina(ByVal strCodCliente As String, ByVal pFechaProceso As DateTime,
                                      ByVal pTipoTela As String, ByVal pintNumeroSalida As Integer,
                                      ByVal pUsuario As String, ByVal pDTListaPedido As DataTable) As DataTable
        Try
            Dim objUtil As New Util
            'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
            pDTListaPedido.TableName = "Pedido"
            Dim strListaPedidoXML As New StringBuilder(objUtil.GeneraXml(pDTListaPedido))
            ' Dim strXMLData As string = objUtil.GeneraXml(pDTListaPedido)

            Dim objParametros As Object() = {"pvch_CodCliente", strCodCliente, _
                                             "pvch_FechaProceso", pFechaProceso, _
                                             "pint_NumeroSalida", pintNumeroSalida, _
                                             "pvch_Usuario", pUsuario, _
                                             "pvch_TipoTela", pTipoTela, _
                                             "pvch_ListaPedidoXML", strListaPedidoXML.ToString}

            Return _objConnexion.ObtenerDataTable("usp_qry_GeneraPedidoyGuiaCachina_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarCorreosCachinas() As String

        Try
            Return CType(_objConnexion.ObtenerValor("usp_qry_BuscaCorreosCachina"), String).Trim
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function VerificaNumSalidaActiva(ByVal pNumeroSalida As Integer) As String
        Try
            '_objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros As Object() = {"pint_NumeroSalida", pNumeroSalida}

            Return CType(_objConnexion.ObtenerValor("usp_qry_VerificaNumeroSalidaActiva", objParametros), String).Trim

        Catch ex As Exception
            Return "ERROR: " + ex.Message
        End Try

    End Function
#End Region


#Region "ROLLOS"

    Public Function BuscarUbicacionesRollosTransito(ByVal pstrDocumento As String) As DataTable
        Try
            ' _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"pvch_Documento", pstrDocumento}
            Return _objConnexion.ObtenerDataTable("usp_qry_BuscarUbicacionesRollosTransito", objparametros)
        Catch ex As Exception
            'ValidaRolloPermitidoMaestroCachinas = Nothing
            Throw ex
        End Try
    End Function

#End Region

#Region "Guias de Remision"
  'INICIO: 20150605
  Public Sub ValidarRolloGuiaRemision(ByVal pstrEmpresa As String, ByVal pstrNroPedido As String, ByVal pstrCodRollo As String, ByVal pintNroSalida As Integer, ByRef _DtRollo As DataTable)
    Try
      Dim objparametros() As Object = {"vch_Empresa", pstrEmpresa, "vch_NroPedido", pstrNroPedido, "vch_CodRollo", pstrCodRollo, "int_NroSalida", pintNroSalida}
      _DtRollo = _objConnexion.ObtenerDataTable("Usp_Alm_GuiaRemision_ValidarRollo", objparametros)
    Catch ex As Exception
      Throw ex
    End Try
    End Sub
    Public Sub ValidarBolsaGuiaRemision(ByVal pstrEmpresa As String, ByVal pstrNroPedido As String, ByVal pstrCodRollo As String, ByVal pintNroSalida As Integer, ByRef _DtRollo As DataTable)
        Try
            Dim objparametros() As Object = {"vch_Empresa", pstrEmpresa, "vch_NroPedido", pstrNroPedido, "vch_CodRollo", pstrCodRollo, "int_NroSalida", pintNroSalida}
            _DtRollo = _objConnexion.ObtenerDataTable("USP_VEN_GuiaRemision_ValidarBolsa", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
  'FINAL: 20150605

  Public Sub ResumenPedido_Listar(ByVal pstrEmpresa As String, ByVal pstrNroPedido As String, ByRef _DtRollo As DataTable)
    Try
      Dim objparametros() As Object = {"vch_Empresa", pstrEmpresa, "vch_NroPedido", pstrNroPedido}
            _DtRollo = _objConnexion.ObtenerDataTable("Usp_Alm_ResumenPedido_Listar_V2", objparametros)
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Public Sub PedidoDeVentas_Buscar(ByVal pstrEmpresa As String, ByVal pstrTipoDoc As String, ByVal pstrNroPedido As String, ByRef _DtRollo As DataTable)
    Try
      Dim objparametros() As Object = {"vch_Empresa", pstrEmpresa, "vch_TipoDoc", pstrTipoDoc, "vch_NroPedido", pstrNroPedido}
      _DtRollo = _objConnexion.ObtenerDataTable("Usp_Alm_PedidosDeVenta_Buscar", objparametros)
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Public Sub GuiaRemision_Registrar(ByVal pstrEmpresa As String, ByVal pstr_NroPedido As String, _
                                    ByVal pstr_CodUbigeo As String, pstr_DirEntrega As String, _
                                    ByVal pstr_Usuario As String, ByRef pstr_RollosXML As String, _
                                    ByVal pstrFechaGuia As String, pint_NroSalida As Integer, ByRef dtResult As DataTable)
    Try
      Dim objparametros() As Object = {"pvch_CodEmpresa", pstrEmpresa, "pvch_NroPedido", _
                                       pstr_NroPedido, "pvch_CodUbigeo", pstr_CodUbigeo, "pvch_DirEntrega", pstr_DirEntrega, _
                                       "pvch_Usuario", pstr_Usuario, "pxml_RollosXML", pstr_RollosXML, "pvch_FecGuia", pstrFechaGuia, _
                                       "pint_NroSalida", pint_NroSalida}
      dtResult = _objConnexion.ObtenerDataTable("Usp_Alm_GenerarGuiaDePedido", objparametros)
    Catch ex As Exception
      Throw ex
    End Try
    End Sub
    Public Sub GuiaRemision_Registrar_Desperdicio(ByVal pstrEmpresa As String, ByVal pstr_NroPedido As String, _
                                  ByVal pstr_CodUbigeo As String, pstr_DirEntrega As String, _
                                  ByVal pstr_Usuario As String, ByRef pstr_RollosXML As String, _
                                  ByVal pstrFechaGuia As String, ByRef dtResult As DataTable)
        Try
            Dim objparametros() As Object = {"pvch_CodEmpresa", pstrEmpresa, "pvch_NroPedido", _
                                             pstr_NroPedido, "pvch_CodUbigeo", pstr_CodUbigeo, "pvch_DirEntrega", pstr_DirEntrega, _
                                             "pvch_Usuario", pstr_Usuario, "pxml_RollosXML", pstr_RollosXML, "pvch_FecGuia", pstrFechaGuia}

            dtResult = _objConnexion.ObtenerDataTable("Usp_Alm_GenerarGuiaDePedido_Desperdicios", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

  Public Sub DireccionCliente_Listar(ByVal pstrEmpresa As String, ByVal pstrCodCliente As String, ByRef dtResult As DataTable)
    Dim strSQL As String = ""
    strSQL = "Select TI_DIRE = CASE WHEN TI_DIRE = 'DES' THEN 'DE0' ELSE TI_DIRE END,DE_DIRE, CO_UBIC_GEOG From TDDIRE_CLIE WHERE	CO_EMPR = '" & pstrEmpresa & "' AND	CO_CLIE = '" & pstrCodCliente & "' ORDER BY 1 ASC "
    Try
      dtResult = _objConnexion.ObtenerDataTable2(strSQL)
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

    Public Sub ListaNumSalidaDespachoxPedido_Listar(ByVal pstr_NroPedido As String, ByRef dtResult As DataTable)
        Try
            Dim objparametros() As Object = {"pvch_NroPedido", pstr_NroPedido}

            dtResult = _objConnexion.ObtenerDataTable("Usp_Alm_NumSalidaDespachoxPedido_Listar", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ObtenerListadoAyudantes(ByVal pstrUsuario As String, ByRef dtResult As DataTable)
        Try
            Dim objparametros() As Object = {"pvch_Usuario", pstrUsuario}
            dtResult = _objConnexion.ObtenerDataTable("usp_vent_obtener_listado_ayudantes_almacen", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Dispose"
    Public Sub Dispose() Implements System.IDisposable.Dispose
        _objConnexion.Dispose()
    End Sub
#End Region



End Class
