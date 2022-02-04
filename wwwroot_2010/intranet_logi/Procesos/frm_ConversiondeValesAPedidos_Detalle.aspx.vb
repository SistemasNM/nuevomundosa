Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ConversiondeValeAPedidos_Detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblUsu As System.Web.UI.WebControls.Label
    'Protected WithEvents lblUsuario As System.Web.UI.WebControls.Label
    Protected WithEvents rdbVale As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdbCTC As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtEstado As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNumeroPedido As System.Web.UI.WebControls.Label
    Protected WithEvents txtSeriePedido As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumeroPedido As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodSolicitante As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDesSolicitante As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaPedido As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaAprobacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaAtencion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCentroCostos As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDesCentroCostos As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboAlmacen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPstoInicial As System.Web.UI.WebControls.Label
    Protected WithEvents lblPstoUtilizado As System.Web.UI.WebControls.Label
    Protected WithEvents lblPstoDisponible As System.Web.UI.WebControls.Label
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblContadorPalabras As System.Web.UI.WebControls.Label
    Protected WithEvents txtAcepta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSituacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCanX As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesCuentaGasto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboCuentaGastos As System.Web.UI.WebControls.DropDownList
    Protected WithEvents imgBuscarCtaGastos As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtOrdenServicio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCantidad As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAgregar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblDesArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblUniMedida As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStockArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPrecioArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesServicio As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlArticulo As System.Web.UI.WebControls.Panel
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label
    Protected WithEvents dgDetallePedido As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblMonto As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPedido As System.Web.UI.WebControls.Label
    Protected WithEvents btnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents btnSolicitaAprobacion As System.Web.UI.WebControls.Button
    Protected WithEvents lblMsgError As System.Web.UI.WebControls.Label
    Protected WithEvents btnSalir As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtValeMantto As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGuardar As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@GRUPO_CODIGO") = "3000"
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "EPOMA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        InitializeComponent()
    End Sub
#End Region
    Dim strSeriePedido As String = "0003"
    Dim strEmpresa As String = "01"
    Dim strUnidad As String = "001"
    Dim strNumeroPedido As String = ""
    Dim strCodigoArticulo, strSecuencia As String
    Dim lstrErrorDatos, lstrErrorDuplicado, lstrPstoDisponible, lstrErrorValorCantidad, lstrErrorDuplicadoCTC As String
    Dim strFlag As String = "0"
    Dim strCodCtaGasto, strDesCtaGasto As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not (Page.IsPostBack) Then
            '20120918 EPM Readonly
            txtFechaPedido.Attributes.Add("readonly", "readonly")
            txtFechaAprobacion.Attributes.Add("readonly", "readonly")


            prcLimpiaControlesCabecera()
            txtValeMantto.Text = Request("NroVale")
            btnSolicitaAprobacion.Enabled = False
            CargaVales(txtValeMantto.Text)
            'lblUsuario.Text = Session("@USUARIO")
            txtSituacion.Text = strFlag
            btnSolicitaAprobacion.Enabled = False
            fncGeneraNumeroPedido()

        Else
            btnSolicitaAprobacion.Attributes.Add("onClick", "javascript:SolicitarAprobacion();")
            txtCantidad.Attributes.Add("onBlur", "javascript:txtCantidad_onBlur();")
        End If
    End Sub

    'Manejo de Controles
#Region "Manejo de Controles"
    ' --- Limpia todos Controles de Cabecera
    Private Sub prcLimpiaControlesCabecera()
        ' 1
        txtEstado.Text = ""
        txtSeriePedido.Text = ""
        txtNumeroPedido.Text = ""
        txtCodSolicitante.Text = ""
        lblDesSolicitante.Text = ""
        txtCodCentroCostos.Text = ""
        lblDesCentroCostos.Text = ""
        cboAlmacen.Items.Clear()
        txtObservaciones.Text = ""

        ' 2
        txtFechaPedido.Text = ""
        txtFechaAprobacion.Text = ""
        txtFechaAtencion.Text = ""
        lblPstoInicial.Text = Strings.Format("{0,0.0000}", 0)
        lblPstoUtilizado.Text = Strings.Format("{0,0.0000}", 0)
        lblPstoDisponible.Text = Strings.Format("{0,0.0000}", 0)
    End Sub

    ' --- Limpia Controles del Detalle
    Private Sub prcLimpiaControlesDetalle()
        txtCodArticulo.Text = ""
        lblDesArticulo.Text = ""
        lblUniMedida.Text = ""
        lblPrecioArticulo.Text = ""
        lblStockArticulo.Text = ""
        'txtOrdenServicio.Text = ""
        'txtDesServicio.Text = ""
        txtDesCuentaGasto.Text = ""
        txtCantidad.Text = Strings.Format("{0,0.00}", 0)
    End Sub
    ' --- Habilitra Controles Cabecera de Pedido
    Private Sub prcHabilitaControlesCabecera(ByVal lblnEstado As Boolean)
        txtSeriePedido.Enabled = False
        txtNumeroPedido.Enabled = False
        txtEstado.Enabled = False

        txtCodSolicitante.Enabled = lblnEstado
        txtCodCentroCostos.Enabled = lblnEstado
        cboAlmacen.Enabled = lblnEstado
        'txtObservaciones.Enabled = lblnEstado
    End Sub

    ' --- Habilitra Controles Detalle de Pedido
    Private Sub prcHabilitaControlesDetalle(ByVal lblnEstado As Boolean)
        lblItems.Visible = lblnEstado
        dgDetallePedido.Visible = lblnEstado
        lblMonto.Visible = lblnEstado
        lblTotalPedido.Visible = lblnEstado
    End Sub

    ' --- Habilita Controles de detalle del Pedido
    Private Sub prcHabilitaDatosArticulo(ByVal lblnEstado As Boolean)
        'pnlArticulo.Visible = lblnEstado
        txtCodArticulo.Enabled = lblnEstado
        lblDesArticulo.Visible = lblnEstado
        lblUniMedida.Visible = lblnEstado
        lblStockArticulo.Visible = lblnEstado
        lblPrecioArticulo.Visible = lblnEstado


        cboCuentaGastos.Enabled = lblnEstado
        imgBuscarCtaGastos.Enabled = lblnEstado
        txtOrdenServicio.Enabled = lblnEstado
        txtCantidad.Enabled = lblnEstado
        btnAgregar.Enabled = lblnEstado
    End Sub

    ' --- Habilita Botones de Accion
    Private Sub prcHabilataBotonesAccion(ByVal ActNuevo As Boolean, ByVal ActBuscar As Boolean, _
                                ByVal ActSolictud As Boolean, ByVal ActSeguimiento As Boolean, _
                                ByVal ActAnular As Boolean, ByVal ActAgregar As Boolean)
        btnNuevo.Enabled = ActNuevo
        btnSolicitaAprobacion.Enabled = ActSolictud
        btnAgregar.Enabled = ActAgregar
    End Sub

#End Region

    '  Botones de Acciones del Formulario
#Region "Botones"
    ' --- Boton: Nuevo Pedido
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Response.Redirect("frm_ConversiondeValesAPedidos.aspx", True)
    End Sub

    ' --- Cbo: Consultamos Presupuesto
    Private Sub cboCuentaGastos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCuentaGastos.SelectedIndexChanged
        Dim ldblPstoDisponible As Double
        ldblPstoDisponible = fncConsultaPstoDisponible("1")
        If ldblPstoDisponible <= 0 Then
            lblError.Text = "Observacion: No Existe Presupuesto para realizar Pedidos"
            lblError.Visible = True
        Else
            lblError.Text = ""
            lblError.Visible = False
        End If
    End Sub

    ' --- Boton: Agregar Item al Detalle de Pedido
    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregar.Click
        lstrErrorDatos = ""
        lblError.Text = ""
        lblError.Visible = False
        Call ValidaDatosDetallePedido()
        If lstrErrorDatos.Length > 0 Then
            lblError.Text = lstrErrorDatos
            lblError.Visible = True
            Exit Sub
        Else
            If ValidaActivoXCtaGasto().Length > 0 Or ValidaDuplicidadCTC().Length > 0 Then
                lblError.Text = lstrErrorDatos
                lblError.Visible = True
                Exit Sub
            Else
                'validamos la existencia del activo
                If ValidaActivoCTC().Length > 0 Then
                    lblError.Text = lstrErrorDatos
                    lblError.Visible = True
                    Exit Sub
                Else
                    ' Validamos Presupuesto
                    ValidaPresupuesto()
                    lblError.Text = lstrPstoDisponible
                    lblError.Visible = True
                    ' Validamos Articulos duplicados en Pedido
                    If Trim(ValidaDuplicidadItem()).Length > 0 Then
                        lblError.Text = lstrErrorDuplicado
                        lblError.Visible = True
                        Exit Sub
                    Else
                        strFlag = txtSituacion.Text
                        Select Case strFlag
                            Case "0"
                                'Nuevo Pedido: Grabamos Cabecera y Detalle
                                GuardarPedido()
                                txtSituacion.Text = "1"
                            Case "1"
                                'Agregarmos Items al Pedido
                                prcActulizaDetallePedido(strFlag, "")
                                txtSituacion.Text = "1"
                            Case "2"
                                ' Actualizamos Items al Pedido
                                ActualizaritemPedido()
                                txtSituacion.Text = "1"
                        End Select
                    End If
                    lblError.Text = ""
                    lblError.Visible = False
                    prcLimpiaControlesDetalle()
                End If
            End If
        End If
    End Sub

    ' Boton: Solicitamos Aprobacion de Pedidos
    Private Sub btnSolicitaAprobacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitaAprobacion.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim strNumeroPedido As String, ldtCorreos As DataTable
        Try
            strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
            If txtAcepta.Text <> "0" And txtAcepta.Text.Length > 0 Then
                If objPedido.fncSolicitarAprobacionPedido(Session("@EMPRESA"), _
                                                    txtAcepta.Text, _
                                                    strNumeroPedido, _
                                                    txtFechaPedido.Text, "", _
                                                    "PRO", _
                                                    txtFechaPedido.Text, _
                                                    "K", txtCodCentroCostos.Text, _
                                                    Session("@USUARIO"), "", _
                                                    Session("@USUARIO"), "", ldtCorreos) Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('El Pedido ha sido enviada para su respectiva Aprobaci�n.');</script>")
                    ' prcHabilitaDatosArticulo(False)
                    prcHabilitaControlesCabecera(False)
                    'prcHabilitaControlesDetalle(True)
                    'prcHabilitaDatosArticulo(False)
                    'prcHabilataBotonesAccion(True, True, False, True, True, False)
                    txtEstado.Text = "POR APROBAR"
                    EnviarEmail(ldtCorreos)
                    txtAcepta.Text = "0"
                End If
            Else
                lblError.Text = "Debe Elegir un tipo de Aprobacion"
                lblError.Visible = False
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al Aprobar, comuniquese con Sistemas."
        End Try
    End Sub

    ' Boton: Guardamos el Pedido
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        GuardarPedido()
    End Sub

    Private Sub btnBuscar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtValeMantto.Text.Length > 0 Then
            CargaVales(txtValeMantto.Text)
        End If
    End Sub

#End Region

    ' Procedimientos
#Region "Procedimentos"
    ' --- Prodedimiento: Nuevo Pedido
    Public Sub prnNuevoPedido()
        txtSituacion.Text = "0"
        prcLimpiaControlesCabecera()
        prcLimpiaControlesDetalle()
        prcHabilitaControlesCabecera(True)
        prcHabilitaControlesDetalle(False)
        prcHabilitaDatosArticulo(True)
        prcHabilataBotonesAccion(False, False, False, False, False, True)

        prcValoresIniciales(False)
        txtOrdenServicio.Text = ""
        txtDesCuentaGasto.Text = ""

        cboCuentaGastos.Items.Clear()

        'Conusltamos Usuario
        Dim lstrUsuario As String
        lstrUsuario = Session("@USUARIO")
        prcConsultarusuario(lstrUsuario)

        ' Conusltamos Almacenes
        prcListaAlmacen()

    End Sub

    ' Colocamos valores iniciales para un Nuevo Pedido
    Private Sub prcValoresIniciales(ByVal lblnEstado As Boolean)
        strFlag = txtSituacion.Text
        If strFlag = "0" Then
            txtSeriePedido.Text = strSeriePedido
            txtFechaPedido.Text = Now.Today.ToShortDateString
            txtEstado.Text = NombreEstado("ACTIVO")
            lblPstoInicial.Text = Strings.Format("{0,0.0000}", 0)
            lblPstoUtilizado.Text = Strings.Format("{0,0.0000}", 0)
            lblPstoDisponible.Text = Strings.Format("{0,0.0000}", 0)
            lblTotalPedido.Text = "0.00"
        End If
    End Sub

    ' --- Nombre de Estado de Pedido
    Private Function NombreEstado(ByVal strEstado As String) As String
        Dim strNomEstado As String
        Select Case strEstado
            Case "POR APROBAR"
                strNomEstado = "POR APROBAR"
            Case "ACTIVO"
                strNomEstado = "ACTIVO"
            Case "APROBADO"
                strNomEstado = "APROBADO"
            Case "ANULADO"
                strNomEstado = "ANULADO"
            Case "ATENDIDO"
                strNomEstado = "ATENDIDO"
        End Select
        Return strNomEstado
    End Function

    ' --- Consultamos los Almacenes
    Private Sub prcListaAlmacen()
        cboAlmacen.Items.Clear()
        cboAlmacen.Enabled = True
        cboAlmacen.DataSource = fncListarAlmacenes()
        cboAlmacen.DataValueField = "co_alma"
        cboAlmacen.DataTextField = "de_alma"
        cboAlmacen.DataBind()
        cboAlmacen.Items.Insert(0, New ListItem("Seleccione Almacen", ""))
    End Sub

    ' --- Consultamos Cuentas de Gastos
    Private Sub prcConsultaCuentagastos()
        If txtCodCentroCostos.Text.Length > 0 Then
            cboCuentaGastos.Items.Clear()
            Dim ldtCtaGastos As DataTable
            ldtCtaGastos = fncListarCtaGastos()
            If Not ldtCtaGastos Is Nothing And ldtCtaGastos.Rows.Count > 0 Then
                cboCuentaGastos.Enabled = True
                cboCuentaGastos.DataSource = ldtCtaGastos
                cboCuentaGastos.DataValueField = "co_cnta_gast"
                cboCuentaGastos.DataTextField = "de_unid_dest"
                cboCuentaGastos.DataBind()
                cboCuentaGastos.Items.Insert(0, New ListItem("Seleccione Cta Gastos", ""))
                lblError.Text = ""
                lblError.Visible = False
            Else
                cboCuentaGastos.Enabled = True
                lblError.Text = "No Existen Cuentas de Gastos para el Centro de Costos elegido"
                lblError.Visible = True
            End If
        Else
            cboCuentaGastos.Enabled = True
            lblError.Text = "Debe Ingresar el Centro de Costos"
            lblError.Visible = True
        End If
    End Sub

#End Region

    ' Acceso a datos
#Region "Acceso a datos"

    ' --- Consultamos Usuario
    Public Function prcConsultarusuario(ByVal strUsuario As String) As DataTable
        Dim ldtbUsuario As DataTable
        Try
            Dim objParametros As Object() = {"COD_USU", strUsuario}
            ldtbUsuario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_PedidoConsultaUsuario", objParametros)
            If Not ldtbUsuario Is Nothing Then
                txtCodSolicitante.Text = ldtbUsuario.Rows(0).Item("co_trab")
                lblDesSolicitante.Text = ldtbUsuario.Rows(0).Item("Nombres")
            Else
                txtCodSolicitante.Text = ""
                lblDesSolicitante.Text = ""
                lblError.Text = "Debe elegir un solicitante para el Vale"
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error a consultar usuario, comuniquese con Sistemas."
            Return Nothing
        End Try
    End Function
    ' --- Consultamos Almacenes
    Public Function fncListarAlmacenes() As DataTable
        Try
            Return New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet("usp_LOG_DiasStockxArticulosFiltros").Tables(1)
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error a consultar almacenes, comuniquese con Sistemas."
            Return Nothing
        End Try
    End Function

    ' --- Consultamos Cuentas de Gastos
    Public Function fncListarCtaGastos() As DataTable
        Dim strCodCentroCostos As String = ""
        Try
            strCodCentroCostos = txtCodCentroCostos.Text
            Dim objParametros As Object() = {"co_auxi_empr", strCodCentroCostos}
            Return New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_ConsultaCtaGastos", objParametros)
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error a consultar cuenta de gasto, comuniquese con Sistemas."
            Return Nothing
        End Try
    End Function
    ' --- Consultamos Presupuesto Disponible
    Public Function fncConsultaPstoDisponible(ByVal strTipo As String) As Double
        Dim ldblPstoInicial As Double = 0
        Dim ldblPstoUtilizado As Double = 0
        Dim ldblPstoDisponible As Double = 0

        Dim lstrCentroCostos As String
        Dim lstrCuentaGastos As String
        Dim lstrAnno As String
        Dim lintMes As Integer
        Dim ldblTotalPedido As Double

        Dim objPedido As Logistica.clsPedidos
        Dim ldtbPsto As DataTable

        lstrCentroCostos = txtCodCentroCostos.Text
        If strNumeroPedido.Length > 0 Then
            lstrCuentaGastos = Mid(Trim(cboCuentaGastos.SelectedValue), 1, 6)
            strTipo = "2"
        Else
            lstrCuentaGastos = cboCuentaGastos.SelectedValue
        End If

        lstrAnno = Mid(txtFechaPedido.Text, 7, 4)
        lintMes = Integer.Parse(Mid(txtFechaPedido.Text, 4, 2))
        ldblTotalPedido = CDbl(lblTotalPedido.Text)
        objPedido = New Logistica.clsPedidos
        Try
            ldtbPsto = New DataTable
            ldtbPsto = objPedido.fncConsultaPresupuesto(strTipo, lstrCentroCostos, lstrCuentaGastos, lstrAnno, lintMes, ldblTotalPedido)
            Dim i As Integer
            If Not ldtbPsto Is Nothing And ldtbPsto.Rows.Count > 0 Then
                ldblPstoInicial = ldtbPsto.Rows(0).Item("psto_inic")
                ldblPstoUtilizado = ldtbPsto.Rows(0).Item("psto_util")
                ldblPstoDisponible = ldtbPsto.Rows(0).Item("psto_disp")
            End If
            lblPstoInicial.Text = String.Format("{0:0.000}", ldblPstoInicial)
            lblPstoUtilizado.Text = String.Format("{0:0.000}", ldblPstoUtilizado)
            lblPstoDisponible.Text = String.Format("{0:0.000}", ldblPstoDisponible)
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al consultar presupuesto disponible, comuiquese con Sistemas."
        End Try
        Return ldblPstoDisponible
    End Function

    ' --- Prodedimiento: Guardar Pedido
    Private Sub GuardarPedido()
        Dim dtbDetalle As DataTable = CType(Session("Detalle"), DataTable)
        Dim i As Integer
        Dim intNumItems As Integer
        Dim dblTotalPedido As Double
        Dim objPedidos As Logistica.clsPedidos
        Dim strNumPedido As String
        Dim strCodUnidad As String
        Dim strCodAlmacen As String
        Dim strFecha As String
        Dim strObservacion As String
        Dim strCodDestino As String
        Dim strCodAuxiliar As String
        Dim strCodSolicitante As String
        Dim strCodUsuario As String
        Dim strCodVale As String
        Dim ldtDetalle As DataTable ' = CType(Session("Detalle"), DataTable)

        strNumPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
        strCodUnidad = strUnidad
        strCodAlmacen = cboAlmacen.SelectedValue
        strFecha = txtFechaPedido.Text
        strObservacion = txtObservaciones.Text
        strCodAuxiliar = txtCodCentroCostos.Text
        strCodSolicitante = txtCodSolicitante.Text
        strCodAlmacen = cboAlmacen.SelectedValue
        strCodUsuario = Session("@USUARIO")
        strCodVale = txtValeMantto.Text

        Try
            objPedidos = New Logistica.clsPedidos

            ldtDetalle = objPedidos.fncGuardaPedidoVale(Session("@EMPRESA"), strNumPedido, strCodUnidad, strCodAlmacen, _
                                strFecha, strObservacion, strCodAuxiliar, strCodSolicitante, strCodUsuario, strCodVale, dtbDetalle)
            If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                intNumItems = ldtDetalle.Rows.Count
                If intNumItems > 0 Then
                    'txtNumeroPedido.Text = ldtDetalle.Rows(i).Item("var_NumeroPedido")
                    lblError.Text = "Se Genero el Pedido: " + strNumPedido
                    lblError.Visible = True
                    btnSolicitaAprobacion.Enabled = True

                End If
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al guardar el vale, comuniquese con Sistemas." 'ex.Message '
        End Try
    End Sub

    'Private Sub GuardarPedido()
    '    Dim i As Integer
    '    Dim intNumItems As Integer
    '    Dim dblTotalPedido As Double
    '    Dim objPedidos As Logistica.clsPedidos

    '    Dim chrTipo As String
    '    Dim strNumPedido As String
    '    Dim strNumItem As String
    '    Dim dblCantidad As Double
    '    Dim strCodAuxiliar As String
    '    Dim strCodDestino As String
    '    Dim strCodOrden As String
    '    Dim strCodSolicitante As String
    '    Dim strCodAlmacen As String
    '    Dim strObservacion As String
    '    Dim strCodUsuario As String
    '    Dim ldtDetalle As DataTable

    '    chrTipo = "1"
    '    strNumPedido = fncGeneraNumeroPedido()
    '    strNumItem = txtCodArticulo.Text
    '    dblCantidad = CDbl(Trim(txtCantidad.Text))
    '    strCodAuxiliar = txtCodCentroCostos.Text
    '    strCodDestino = cboCuentaGastos.SelectedValue
    '    strCodOrden = txtOrdenServicio.Text
    '    strCodSolicitante = txtCodSolicitante.Text
    '    strCodAlmacen = cboAlmacen.SelectedValue
    '    strObservacion = txtObservaciones.Text
    '    strCodUsuario = Session("@USUARIO")
    '    Try
    '        objPedidos = New Logistica.clsPedidos
    '        strFlag = txtSituacion.Text
    '        If strFlag = "0" Then
    '            ldtDetalle = objPedidos.fncGuardaPedidoVale(chrTipo, strNumPedido, strNumItem, dblCantidad, _
    '                                strCodAuxiliar, strCodDestino, strCodOrden, strCodUsuario, _
    '                                strCodAlmacen, strObservacion, strCodSolicitante)
    '            If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
    '                intNumItems = ldtDetalle.Rows.Count
    '                If intNumItems > 0 Then
    '                    dgDetallePedido.DataSource = ldtDetalle
    '                    dgDetallePedido.DataBind()
    '                    dgDetallePedido.Visible = True

    '                    'Calculamos Monto del Pedido
    '                    For i = 0 To intNumItems - 1
    '                        dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
    '                    Next

    '                    lblItems.Text = "Numero de Items:" + intNumItems.ToString
    '                    lblItems.Visible = True

    '                    lblMonto.Visible = True
    '                    lblTotalPedido.Text = dblTotalPedido.ToString()
    '                    lblTotalPedido.Visible = True

    '                    txtNumeroPedido.Text = Mid(strNumPedido, 6, 10)
    '                    lblError.Text = "Se Genero el Pedido: " + strNumPedido
    '                    lblError.Visible = True

    '                    prcHabilitaControlesCabecera(False)
    '                    prcLimpiaControlesDetalle()
    '                    prcHabilataBotonesAccion(True, True, True, False, True, True)

    '                    ' Consultamos Presupuestos
    '                    fncConsultaPstoDisponible("1")
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblMsgError.Text = "Ha ocurrido un error al guardar el vale, comuniquese con Sistemas."
    '    End Try

    'End Sub

    ' --- Funcion que genera un Nuevo Numero de Pedido
    Public Function fncGeneraNumeroPedido() As String
        Dim objPedido As Logistica.clsPedidos
        Dim strNumeroPedido As String
        Dim strSerie As String

        strSerie = strSeriePedido

        objPedido = New Logistica.clsPedidos
        strNumeroPedido = objPedido.fncGeneraNumeroPedido(strSerie)
        txtNumeroPedido.Text = strNumeroPedido
        txtSeriePedido.Text = Mid(strNumeroPedido, 1, 4)
        txtNumeroPedido.Text = Mid(strNumeroPedido, 6, 10)
        Return strNumeroPedido
    End Function

    ' --- Agrega/Modifica/Elimina el Item al detalle en la tabla Original
    Private Sub prcActulizaDetallePedido(ByVal chrTipo As String, ByVal strCodProducto As String)
        Dim i As Integer
        Dim intNumItems As Integer
        Dim dblTotalPedido As Double = 0

        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtDetalle = New DataTable

        Dim strNumPedido As String
        Dim strNumItem As String
        Dim dblCantidad As Double
        Dim strCodAuxiliar As String
        Dim strCodDestino As String
        Dim strCodOrden As String
        Dim strCodUsuario As String
        Dim strCodSolicitante As String
        Dim strCodAlmacen As String
        Dim strObs As String

        strNumPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
        If Trim(strCodProducto.Length) > 0 Then
            strNumItem = strCodProducto
        Else
            strNumItem = txtCodArticulo.Text
        End If
        dblCantidad = CDbl(Trim(txtCantidad.Text))
        strCodAuxiliar = txtCodCentroCostos.Text

        strCodDestino = Trim(cboCuentaGastos.SelectedValue)
        If strCodDestino.Length = 0 Then
            strCodDestino = Mid(Trim(txtDesCuentaGasto.Text), 1, 7)
        End If

        strCodOrden = txtOrdenServicio.Text
        strCodUsuario = Session("@USUARIO")
        strCodSolicitante = txtCodSolicitante.Text
        strCodAlmacen = cboAlmacen.SelectedValue
        strObs = txtObservaciones.Text
        If Trim(txtAcepta.Text) = "" Or Trim(txtAcepta.Text).Length = 0 Then
            strSecuencia = "0"
        Else
            strSecuencia = Trim(txtAcepta.Text)
        End If
        ' Actualizamos en Pedido Existente
        Try
            ldtDetalle = objPedidos.prcRegistraDetallePedido(chrTipo, strNumPedido, strNumItem, _
                            dblCantidad, strCodAuxiliar, strCodDestino, strCodOrden, strCodUsuario, _
                            strCodAlmacen, strObs, strCodSolicitante, strSecuencia, "", "", "")
            If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                intNumItems = ldtDetalle.Rows.Count
                If intNumItems > 0 Then
                    dgDetallePedido.DataSource = ldtDetalle
                    dgDetallePedido.DataBind()
                    dgDetallePedido.Visible = True
                    lblItems.Text = "Numero de Items:" + intNumItems.ToString
                    lblItems.Visible = True
                    For i = 0 To intNumItems - 1
                        dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
                    Next
                    lblMonto.Visible = True
                    lblTotalPedido.Text = dblTotalPedido.ToString()
                    lblTotalPedido.Visible = True

                    prcLimpiaControlesDetalle()
                    cboAlmacen.Enabled = False
                    prcHabilataBotonesAccion(True, True, True, False, True, True)
                    lblError.Text = ""
                    lblError.Visible = False
                End If
            Else
                intNumItems = 0
                dgDetallePedido.DataSource = Nothing
                dgDetallePedido.Visible = True
                lblItems.Text = "Numero de Items:" + intNumItems.ToString
                lblItems.Visible = True
                lblTotalPedido.Text = "0.00"
                lblMonto.Visible = True
            End If
            fncConsultaPstoDisponible("1")
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al actualizar vale, comuniquese con Sistemas."
        End Try
    End Sub

    ' Elimina un Registro del Pedido
    Private Sub prcEliminaRegistroPedido(ByVal strCodProducto As String)
        Dim stMensaje As String
        Dim intNumItems As Integer

        strFlag = txtSituacion.Text
        lblError.Text = ""
        lblError.Visible = False

        If strCodProducto.Length > 0 Then
            lblError.Text = "Se Elimino el Articulo: " + strCodProducto + " del Pedido"

            ' Actualza en el detalle
            prcActulizaDetallePedido(strFlag, strCodProducto)
            fncConsultaPstoDisponible("1")
            prcLimpiaControlesDetalle()
            prcHabilitaDatosArticulo(True)
            lblError.Visible = True
            txtSituacion.Text = "1"
        Else
            lblError.Text = "Error: Debe Elegir el Articulo a Eliminar de la Lista"
            lblError.Visible = True
        End If
    End Sub


    ' Actualiza el articulo
    Private Sub ActualizaritemPedido()
        Dim ldblNewCantidad As Double
        Dim stMensaje As String
        Dim strCodProducto As String
        lblError.Text = ""
        lblError.Visible = False
        strFlag = txtSituacion.Text
        ldblNewCantidad = CDbl(txtCantidad.Text)
        strCodProducto = txtCodArticulo.Text
        If strCodProducto.Length > 0 Then
            If ldblNewCantidad > 0 Then
                lblError.Text = "Se Guardaron los cambios en Articulo: " + strCodProducto
                prcActulizaDetallePedido(strFlag, "")
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(True)
                lblError.Visible = True
            Else
                lblError.Text = "Error: Debe Ingresar una Cantidad Valida, Mayor a Cero"
                lblError.Visible = True
            End If
        Else
            lblError.Text = "Error: Debe Elegir un Registro de la Lista"
            lblError.Visible = True
        End If
    End Sub

    Private Sub prcAnulaPedido()
        Dim objPedido As Logistica.clsPedidos
        Dim ldtbPsto As DataTable
        Dim strNumeroPedido As String
        Dim strUsuarioModi As String
        Dim stMensaje As String

        lblError.Text = ""
        lblError.Visible = False

        strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
        strUsuarioModi = Session("@USUARIO")
        objPedido = New Logistica.clsPedidos

        'stMensaje = "�Esta Seguro de Anular: " + strNumeroPedido + " el Pedido Actual?"
        'btnAnular.Attributes.Add("onClick", "javascript:Confirmacion('" + stMensaje + "')")
        lblError.Text = "El Pedido ha sido Anulado"
        Try
            ldtbPsto = New DataTable
            ldtbPsto = objPedido.fncPedidoCambiaEstado("1", strNumeroPedido, strUsuarioModi, Nothing)
            lblError.Visible = True
            Dim i As Integer
            If Not ldtbPsto Is Nothing And ldtbPsto.Rows.Count > 0 Then
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(True)
                'prcLimpiaControlesCabecera()
                'prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, False, False, False)
                lblPstoInicial.Text = ldtbPsto.Rows(0).Item("psto_inic").ToString
                lblPstoUtilizado.Text = ldtbPsto.Rows(0).Item("psto_util").ToString
                lblPstoDisponible.Text = ldtbPsto.Rows(0).Item("psto_disp").ToString
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al anular un vale comuiquese con Sistemas."
        End Try
    End Sub

#End Region
    ' validacion de datos
    ' Manejo de la Grilla
#Region "Grilla"
    Private Sub dgDetallePedido_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetallePedido.ItemCommand
        lblError.Text = ""
        lblError.Visible = False
        prcConsultaCuentagastos()

        Dim stMensaje As String
        Dim lobjSecuencia As Label = CType(e.Item.FindControl("lblSecuencia"), Label)
        Dim lobjCodigo As Label = CType(e.Item.FindControl("lblCodigo"), Label)
        Dim lobjCantidad As Label = CType(e.Item.FindControl("lblCantidad"), Label)
        Dim lobjDescripcion As Label = CType(e.Item.FindControl("lblDescripcion"), Label)
        Dim lobjUnidadMedidad As Label = CType(e.Item.FindControl("lblUnidaMedida"), Label)
        Dim lobjPrecio As Label = CType(e.Item.FindControl("lblPrecio"), Label)

        Dim lobjActivo As Label = CType(e.Item.FindControl("lblActivoFijo"), Label)
        Dim lobjDesActivo As Label = CType(e.Item.FindControl("lblDesActivo"), Label)
        Dim lobjCodCtaGasto As Label = CType(e.Item.FindControl("lblCtagasto"), Label)
        Dim lobjDesCtaGasto As Label = CType(e.Item.FindControl("lblDesCtaGasto"), Label)

        Dim lobjBotonEliminar As ImageButton = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)
        If txtEstado.Text = "ACTIVO" Then
            Select Case e.CommandName
                Case "Editar"
                    Dim i As Integer = 0
                    txtSituacion.Text = "2"
                    txtCodArticulo.Text = lobjCodigo.Text
                    txtCantidad.Text = Strings.Format("{0,0.000}", lobjCantidad.Text)
                    txtOrdenServicio.Text = Trim(lobjActivo.Text)
                    txtDesServicio.Text = Trim(lobjDesActivo.Text)
                    lblDesArticulo.Text = "Desc: " + lobjDescripcion.Text
                    lblUniMedida.Text = "U.M.: " + lobjUnidadMedidad.Text
                    lblPrecioArticulo.Text = "Prec. " + lobjPrecio.Text
                    strCodCtaGasto = lobjCodCtaGasto.Text
                    strDesCtaGasto = lobjDesCtaGasto.Text
                    txtDesCuentaGasto.Text = strDesCtaGasto
                    txtAcepta.Text = lobjSecuencia.Text

                    For i = 0 To cboCuentaGastos.Items.Count - 1
                        If Trim(cboCuentaGastos.Items.Item(i).Text) = Trim(lobjDesCtaGasto.Text) Then
                            cboCuentaGastos.Items.RemoveAt(i)
                            cboCuentaGastos.Items.Insert(0, New ListItem(strDesCtaGasto, strCodCtaGasto))
                            Exit For
                        End If
                    Next
                    prcHabilitaDatosArticulo(True)
                    txtCodArticulo.Enabled = False
            End Select
        Else
            lblError.Text = "Error: Verifique la Situacion, No es posible Modificar el Pedido"
            lblError.Visible = True
        End If
    End Sub

#End Region
#Region "Validaciones para Ingreso"
    ' Validacion de CTC duplicado
    Private Function ValidaDuplicidadCTC() As String
        If txtSituacion.Text = "1" Then
            Dim objPedidos As New Logistica.clsPedidos
            Dim ldtDuplicadosCTC = New DataTable
            Dim strNumPedido As String
            Dim strActivo As String
            strNumPedido = Trim(txtSeriePedido.Text + "-" + txtNumeroPedido.Text)
            strActivo = Trim(txtOrdenServicio.Text)
            Try
                If rdbCTC.Checked = True And strActivo.Length > 0 Then
                    ldtDuplicadosCTC = objPedidos.fncValidaDuplicadoCTC(strNumPedido, strActivo)
                    If ldtDuplicadosCTC.Rows(0).Item("Validacion").ToString = "1" Then
                        lstrErrorDatos = "Verifique por favor, No puede existir mas de un CTC en el Vale Actual"
                    Else
                        lstrErrorDatos = ""
                    End If
                End If
            Catch ex As Exception
                lblMsgError.Text = "Ha ocurrido un error a validar duplicidad de CTC, comuniquese con Sistemas."
            End Try
        End If
        Return lstrErrorDatos
    End Function

    ' Validamos Datos Ingresados
    Public Function ValidaDatosDetallePedido() As String
        Dim ldblPrecio As Double = 0
        lblMsgError.Text = ""
        lblMsgError.Visible = False
        lstrErrorDatos = ""
        Try
            If Mid(cboAlmacen.SelectedItem.ToString, 1, 10) = "Seleccione" Or cboAlmacen.Items.Count = 0 Then
                lstrErrorDatos = "Por favor eliga un Almacen"
                Exit Function
            Else
                If Trim(txtCodArticulo.Text).Length = 0 Then
                    lstrErrorDatos = "Debe elegir un articulo"
                    Exit Function
                Else
                    ' Activo
                    If Mid(Trim(txtOrdenServicio.Text), 1, 1) <> "9" And cboCuentaGastos.Items.Count = 0 And rdbVale.Checked = True Then
                        lstrErrorDatos = "Debe elegir una cuenta de gastos para el pedido."
                        Exit Function
                    Else
                        If Mid(Trim(txtOrdenServicio.Text), 1, 1) <> "9" And cboCuentaGastos.Items.Count > 0 _
                        And Trim(cboCuentaGastos.SelectedValue).Length = 0 Then
                            lstrErrorDatos = "Error: Debe elegir una cuenta de gastos para el pedido."
                            Exit Function
                        Else
                            If Trim(txtCantidad.Text).Length = 0 Or Trim(txtCantidad.Text) = "" Then
                                txtCantidad.Text = "0.00"
                                lstrErrorDatos = "Error: Debe ingresar un activo para el pedido."
                                Exit Function
                            Else
                                'Consultamos precio
                                lstrErrorDatos = ""
                                ldblPrecio = fncConsultaPrecio()
                            End If
                        End If
                    End If
                End If
            End If
            lblPrecioArticulo.Text = Strings.Format("{0,0.00}", ldblPrecio)
        Catch ex As Exception
            lstrErrorDatos = "Error: verifique los datos elegidos"
        End Try
        Return lstrErrorDatos
    End Function

    ' Validamos Stock del Articulo
    Private Function ValidaStockArticulo() As String
        lstrErrorValorCantidad = ""
        Dim dblCantidad As Double = 0
        Dim dblStock As Double = 0

        If CDbl(Trim(txtCantidad.Text)) > 0 Then
            dblCantidad = CDbl(txtCantidad.Text)
            dblStock = CDbl(Trim(Mid(lblStockArticulo.Text, 6, 10)))
            If dblCantidad > dblStock Then
                lstrErrorValorCantidad = "No Existe Stock en Almacen para el Articulo Solicitado"
            End If
        End If
        Return lstrErrorValorCantidad
    End Function

    ' Validamos Presupuesto para la Cuenta de Gastos
    Private Function ValidaPresupuesto() As String
        Dim ldblPstoDisponible As Double = 0
        Dim ldblSubTotalPedido As Double = 0
        Dim ldblPrecioItem As Double = 0
        Dim strCodItems As String

        lstrPstoDisponible = ""
        ldblPstoDisponible = Double.Parse(lblPstoDisponible.Text)
        If lblPrecioArticulo.Text = "" Or Len(Trim(lblPrecioArticulo.Text)) = 0 Then
            lblPrecioArticulo.Text = "0"
        End If
        ldblPrecioItem = Double.Parse(lblPrecioArticulo.Text)
        ldblSubTotalPedido = Double.Parse(txtCantidad.Text) * CDbl(ldblPrecioItem)

        If ldblPrecioItem > 0 Then
            If ldblSubTotalPedido > ldblPstoDisponible Then
                lstrPstoDisponible = "El Monto del Pedido Excede el Presupuesto Disponible"
            End If
        Else
            lstrPstoDisponible = "Error: Verifique el Precio del Articulo a Solicitar"
        End If
        Return lstrPstoDisponible
    End Function

    ' Validamos Articulos duplicados en el detalle del Pedido
    Private Function ValidaDuplicidadItem() As String
        If txtSituacion.Text = "1" Then
            lstrErrorDuplicado = ""
            Dim objPedidos As New Logistica.clsPedidos
            Dim ldtDuplicados = New DataTable

            Dim chrTipo As String
            Dim strNumPedido As String
            Dim strNumItem As String
            Dim strCtagasto As String
            Dim strActivo As String
            chrTipo = "1"
            strNumPedido = Trim(txtSeriePedido.Text + "-" + txtNumeroPedido.Text)
            strNumItem = txtCodArticulo.Text
            strCtagasto = cboCuentaGastos.SelectedValue
            strActivo = txtOrdenServicio.Text
            Try
                ldtDuplicados = objPedidos.fncValidaDuplicadosDetalle(chrTipo, strNumPedido, strNumItem, _
                                                        strCtagasto, strActivo)
                If Not ldtDuplicados Is Nothing And ldtDuplicados.Rows.Count > 0 Then
                    lstrErrorDuplicado = "Verifique por favor, el articulo a solicitar ya existe en el Vale Actual"
                End If
            Catch ex As Exception
                lblMsgError.Text = "Ha ocurrido un error al verificar duplicidad en detalle,  comuniquese con Sistemas."
            End Try
            Return lstrErrorDuplicado
        End If
    End Function

    ' --- Validamos Precio de Producto
    Public Function fncConsultaPrecio() As Double
        Dim objPedido As Logistica.clsPedidos
        Dim ldblPrecioItem As Double = 0
        Dim strCodItem As String

        strCodItem = Trim(txtCodArticulo.Text)
        objPedido = New Logistica.clsPedidos
        ldblPrecioItem = objPedido.fncConsultaPrecioItem(strCodItem)
        Return ldblPrecioItem
    End Function

    ' --- Validamos Activos para cuentas de gasto especificas
    Public Function ValidaActivoXCtaGasto() As String
        Dim objPedido As New Logistica.clsPedidos
        Dim strCtagasto As String
        If rdbVale.Checked = True Then
            If cboCuentaGastos.Items.Count > 0 Then
                strCtagasto = cboCuentaGastos.SelectedValue()
                If objPedido.fnc_VerificaCtaGasto(strCtagasto) = False And (Trim(txtOrdenServicio.Text)).Length = 0 Then
                    lstrErrorDatos = "Error: Debe elegir un activo para la cuenta de gasto seleccionada"
                Else
                    lstrErrorDatos = ""
                End If
            End If
        End If
        Return lstrErrorDatos
    End Function

    ' --- Validamos existencia de ActivoCTC
    Public Function ValidaActivoCTC() As String
        Dim objPedido As New Logistica.clsPedidos
        Dim strActivo As String
        strActivo = Trim(txtOrdenServicio.Text)
        If strActivo.Length > 0 Then
            If objPedido.fnc_VerificaActivoCTC(strActivo) = False Then
                lstrErrorDatos = ""
            Else
                lstrErrorDatos = "Error: Debe elegir un activo/ctc valido"
            End If
        End If
        Return lstrErrorDatos
    End Function

#End Region


    ' Carga de Vales de Mantenimiento
#Region "Carga de Vales"
    ' --- Metodo: Para Cargar el Vale
    Private Sub CargaVales(ByVal NumVale As String)
        Dim objVales As Logistica.clsPedidos
        Dim ldtDetalle As DataTable
        Dim StrCodVale As String
        Dim strSerie As String = strSeriePedido
        Dim strSolicitante As String = ""
        Dim strEstado As String = ""
        Dim dblTotalPedido As Double
        Dim i, intNumItems As Integer
        Dim dtbPedido As DataTable

        StrCodVale = NumVale
        Try
            ' -- Consultamos Cabecera de Vales
            objVales = New Logistica.clsPedidos
            dtbPedido = objVales.fncConsultaVales(strEmpresa, StrCodVale)
            If Not objVales Is Nothing Then
                Dim strAlmacen, strCuentaGastos As String

                ' -- Habilitamos controles segun estado
                'ManejoEstados(dtbPedido.Rows(0).Item("ti_situ"))

                txtSeriePedido.Text = Mid(dtbPedido.Rows(0).Item("nu_pedi"), 1, 4)
                txtNumeroPedido.Text = Mid(dtbPedido.Rows(0).Item("nu_pedi"), 6, 10)
                txtCodSolicitante.Text = dtbPedido.Rows(0).Item("CodSolicitante")
                lblDesSolicitante.Text = dtbPedido.Rows(0).Item("NomSolicitante")
                txtCodCentroCostos.Text = dtbPedido.Rows(0).Item("CodCentroCostos")
                lblDesCentroCostos.Text = dtbPedido.Rows(0).Item("DesCentroCostos")

                cboAlmacen.Items.Clear()
                strAlmacen = Trim(dtbPedido.Rows(0).Item("co_alma") + " - " + dtbPedido.Rows(0).Item("de_alma"))
                cboAlmacen.Items.Insert(0, New ListItem(strAlmacen, dtbPedido.Rows(0).Item("co_alma")))
                cboAlmacen.Enabled = False
                cboAlmacen.Enabled = False
                txtFechaPedido.Text = dtbPedido.Rows(0).Item("fe_pedi")
                txtFechaAprobacion.Text = dtbPedido.Rows(0).Item("fe_apro")
                txtFechaAtencion.Text = dtbPedido.Rows(0).Item("fe_aten")
                txtObservaciones.Text = dtbPedido.Rows(0).Item("de_obse")
                txtEstado.Text = NombreEstado(dtbPedido.Rows(0).Item("ti_situ"))

                ' -- Consultamos Detalle de Vales
                ldtDetalle = New DataTable
                ldtDetalle = objVales.fncConsultaDetalleVales(strEmpresa, StrCodVale)
                dgDetallePedido.DataSource = ldtDetalle
                dgDetallePedido.DataBind()
                Session("Detalle") = ldtDetalle
                dgDetallePedido.Visible = True
                intNumItems = ldtDetalle.Rows.Count.ToString
                lblItems.Text = "Numero de Items: " + ldtDetalle.Rows.Count.ToString
                lblItems.Visible = True
                For i = 0 To intNumItems - 1
                    dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
                Next
                lblMonto.Visible = True
                lblTotalPedido.Text = dblTotalPedido.ToString()
                lblTotalPedido.Visible = True
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al cargar el vale, comuniquese con Sistemas."
        End Try
    End Sub
#End Region
    ' --- Manejo de Estados
    Private Sub ManejoEstados(ByVal strEstado As String)
        Dim lstrMensajeEstado As String
        Select Case strEstado
            Case "ACTIVO1"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(True)
                prcHabilitaDatosArticulo(True)
                prcHabilataBotonesAccion(True, True, True, False, True, True)
                txtSeriePedido.Enabled = False
                txtNumeroPedido.Enabled = False
                txtSituacion.Text = "1"
            Case "POR APROBAR"
                lstrMensajeEstado = "Se ha enviado una Solicitud de Aprobacion para este Pedido, No puede ser Modificado"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, True, False)
                txtSeriePedido.Enabled = False
                txtNumeroPedido.Enabled = False
                cboCuentaGastos.Enabled = False
                cboAlmacen.Enabled = False
                txtSituacion.Text = "6"
            Case "ACTIVO"
                lstrMensajeEstado = "Este Pedido ya ha sido Aprobado, No puede ser Modificado"
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & lstrMensajeEstado & ".');</script>")
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, False, False)
                txtSituacion.Text = "7"
            Case "ANULADO"
                lstrMensajeEstado = "Este Pedido esta Anulado, No puede ser Modificado"
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & lstrMensajeEstado & ".');</script>")
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, False, False, False)
                txtSituacion.Text = "8"
            Case "ATENDIDO"
                lstrMensajeEstado = "Este Pedido ha atendido, No puede ser Modificado"
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & lstrMensajeEstado & ".');</script>")
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, False, False, False)
                txtSituacion.Text = "9"
        End Select
    End Sub

    ' Envio de Correo Electronico
#Region "Envio de Email"

  Private Sub EnviarEmail(ByRef pdtCorreos As DataTable)
    Dim i As Integer
    Dim mailMsg As System.Net.Mail.MailMessage

    Dim lstrCuerpoMensaje As String = ""
    Dim lstrTitulo As String
    Dim lstrPara As String = ""
    Dim lstrCopia As String = ""
    strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
    Try
      For i = 0 To pdtCorreos.Rows.Count - 1
        If lstrPara.Trim.Length = 0 Then
          lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
        Else
          lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
        End If
        lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
      Next i
      If lstrCopia.Trim.Length = 0 Then
        lstrCopia = ""
      Else
        lstrCopia = lstrCopia
      End If
      With pdtCorreos.Rows(0)
        lstrTitulo = "[Intranet] VALE DE ALMACEN POR APROBAR."
        lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                            "APROBACION PARA EL VALE DE ALMACEN &nbsp;: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                            "<STRONG>" & strNumeroPedido & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                            "</STRONG></FONT></FONT></P>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                            "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                            "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                            "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                            "Este correo ha sido generado autom�ticamente por el m�dulo de aprobaciones.<BR>" + _
                            "Por favor no responder este correo.<BR>" + _
                            "Departamento de Sistemas<BR>" + _
                            "C�a. Industrial Nuevo Mundo S.A.<BR>" + _
                            "-------------------------------------------------------------------------------</P>"
        mailMsg = New System.Net.Mail.MailMessage()


        'Configurar arreglo para el TO
        Dim lstrTo_arreglo() As String = lstrPara.Split(";")
        For lintIndice = 0 To lstrTo_arreglo.Length - 1
          If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
        Next

        'Si no hay destinatario que lo envie a sistemas
        If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

        'Configurar arreglo para el CC
        Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
        For lintIndice = 0 To lstrCC_arreglo.Length - 1
          If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
        Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)

        With mailMsg
                    ' .From = New System.Net.Mail.MailAddress("VALE DE ALMACEN POR APROBAR<aprobaciones@nuevomundosa.com>")
                    .From = New System.Net.Mail.MailAddress(user)
          .Subject = lstrTitulo
          .Body = lstrCuerpoMensaje
          .Priority = System.Net.Mail.MailPriority.High
          .IsBodyHtml = True
        End With

        Dim Servidor As New System.Net.Mail.SmtpClient
                Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
                Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
                Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
                If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                    Servidor.Credentials = userCredential
                End If
        Servidor.Send(mailMsg)
        Servidor = Nothing


        Dim strDestinatarios As String
        strDestinatarios = "Se Comunico via email a: " + lstrPara + " Con Copia a: " + lstrCopia
        lblError.Text = strDestinatarios
        lblError.Visible = True
      End With
    Catch ex As Exception
      lblError.Text = ""
      lblError.Visible = False
      lblMsgError.Text = "Ha ocurrido un error al enviar email, comuniquese con Sistemas."
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pud� enviar el correo electronico.');</script>")
    End Try
  End Sub
#End Region

    Private Sub rdbCTC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCTC.CheckedChanged
        If rdbCTC.Checked = True Then
            rdbVale.Checked = False
            txtCodCentroCostos.Text = ""
            lblDesCentroCostos.Text = ""
            txtCodCentroCostos.Enabled = False
        End If
    End Sub

    Private Sub rdbVale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVale.CheckedChanged
        If rdbVale.Checked = True Then
            rdbCTC.Checked = False
            txtCodCentroCostos.Text = ""
            lblDesCentroCostos.Text = ""
            txtCodCentroCostos.Enabled = True
        End If
    End Sub


End Class