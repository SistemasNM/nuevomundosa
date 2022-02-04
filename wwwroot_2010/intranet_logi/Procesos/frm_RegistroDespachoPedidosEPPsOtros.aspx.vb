Imports NuevoMundo
Imports System.Drawing.Printing
Imports System.Drawing
Public Class frm_RegistroDespachoPedidosEPPsOtros
    Inherits System.Web.UI.Page

    Dim strVale_NumDocu As String
    Dim strVale_TipoDocu As String
    Dim strVale_CodAlm As String
    Dim strVale_CodEmp As String
#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtEstado As System.Web.UI.WebControls.Label
    Protected WithEvents txtSeriePedido As System.Web.UI.WebControls.Label
    Protected WithEvents txtNumeroPedido As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodSolicitante As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesSolicitante As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesCentroCostos As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodCentroCostos As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodAlmacen As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesAlmacen As System.Web.UI.WebControls.Label
    Protected WithEvents txtFechaPedido As System.Web.UI.WebControls.Label
    Protected WithEvents txtFechaAprobacion As System.Web.UI.WebControls.Label
    Protected WithEvents txtFechaDespacho As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCtaGasto As System.Web.UI.WebControls.TextBox

    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label

    Protected WithEvents txtCantidadPendiente As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCulminar As System.Web.UI.WebControls.Button
    Protected WithEvents btnDespachar As System.Web.UI.WebControls.Button
    Protected WithEvents btnSalir As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents imgFoto As System.Web.UI.WebControls.Image
    Protected WithEvents txtDesCargo As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesSeccion As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesArea As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodRecepciona As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDesRecepciona As System.Web.UI.WebControls.Label

    'Protected WithEvents imgBuscarCliente As System.Web.UI.WebControls.ImageButton

    Protected WithEvents txtDespachable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnVerVale As System.Web.UI.WebControls.Button
    Protected WithEvents btnVales As System.Web.UI.WebControls.Button
    'Protected WithEvents btnPreDespachar As System.Web.UI.WebControls.Button

    Protected WithEvents hdnCodEmpresa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodUnidad As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodAlmacen As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodReserva As System.Web.UI.HtmlControls.HtmlInputHidden


    Protected WithEvents btnImprimirEtiqueta As System.Web.UI.WebControls.ImageButton


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Session("@GRUPO_CODIGO") = "3000"
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "atorresc"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion_popup.htm", True)
        End If
        InitializeComponent()
    End Sub
#End Region
    Dim strCtaGasto As String = ""
    Dim strOrdenServicio As String = ""
    Dim strNumeroPedido As String = ""
    Dim strNumeroSolicitante As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFechaDespacho.Text = Format(Now, "dd/MM/yyyy")
        Dim strNumPedido As String = ""
        If Not (Page.IsPostBack) Then
            Try
                If (Not Request.Item("strNumeroPedido") Is Nothing) Then
                    Session("Detalle") = Nothing
                    strNumPedido = Request.Item("strNumeroPedido")
                    CargaPedido(strNumPedido)
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Else
            'If txtCodRecepciona.Text <> "" Then
            '    prcConsultaEmpleado(txtCodRecepciona.Text)
            'End If
        End If
        btnSalir.Attributes.Add("onClick", "javascript:fnc_Cerrar();")
        btnVales.Attributes.Add("onClick", "javascript:VerValesPedido('" + txtNumeroPedido.Text + "')")
        btnDespachar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion('Despachar');")
        'btnPreDespachar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion('PreDespachar');")
        btnCulminar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion('Culminar');")
    End Sub
    'Cargar el pedido
    Private Sub CargaPedido(ByVal strNumPedido As String)
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtbPedido As New DataTable
        Dim ldtDetalle As New DataTable

        Dim intCodPedido As Integer = 0
        Dim strSerie As String = "0005"
        Dim strTipo As String = "0"
        Dim strFechaIni As String = ""
        Dim strFechaFin As String = ""
        Dim strSolicitante As String = ""
        Dim strEstado As String = ""
        Dim dblTotalPedido As Double = 0
        Dim i As Integer = 0
        Dim intNumItems As Integer = 0

        dtbPedido = Nothing
        ldtDetalle = Nothing

        intCodPedido = Integer.Parse(Mid(strNumPedido, 6, 10))
        Try
            ' -- Consultamos Cabecera de Pedidos
            'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
            'dtbPedido = objPedidos.fncConsultaPedidos(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
            dtbPedido = objPedidos.fncConsultaPedidosEPPsOtros(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
            'ACTUALIZAR PARA PEDIDOS 0005 - DG - FIN
            If Not objPedidos Is Nothing Then
                ManejoEstados(dtbPedido.Rows(0).Item("ti_situ"))
                txtNumeroPedido.Text = Trim(Mid(dtbPedido.Rows(0).Item("nu_pedi"), 1, 4)) + "-" + Trim(Mid(dtbPedido.Rows(0).Item("nu_pedi"), 6, 10))
                txtDesAlmacen.Text = Trim(dtbPedido.Rows(0).Item("co_alma")) + "-" + Trim(dtbPedido.Rows(0).Item("de_alma"))
                txtDesSolicitante.Text = Trim(dtbPedido.Rows(0).Item("CodSolicitante")) + "-" + Trim(dtbPedido.Rows(0).Item("NomSolicitante"))
                'txtCodRecepciona.Text = Trim(dtbPedido.Rows(0).Item("CodSolicitante"))
                'lblDesRecepciona.Text = Trim(dtbPedido.Rows(0).Item("NomSolicitante"))
                txtDesCentroCostos.Text = Trim(dtbPedido.Rows(0).Item("CodCentroCostos")) + "-" + Trim(dtbPedido.Rows(0).Item("DesCentroCostos"))
                txtFechaPedido.Text = Trim(dtbPedido.Rows(0).Item("fe_pedi"))
                txtFechaAprobacion.Text = Trim(dtbPedido.Rows(0).Item("fe_apro"))
                txtObservaciones.Text = Trim(dtbPedido.Rows(0).Item("de_obse"))
                hdnCodEmpresa.Value = Trim(dtbPedido.Rows(0).Item("co_empr"))
                hdnCodUnidad.Value = Trim(dtbPedido.Rows(0).Item("co_unid"))
                hdnCodAlmacen.Value = Trim(dtbPedido.Rows(0).Item("co_alma"))
                hdnCodReserva.Value = Trim(dtbPedido.Rows(0).Item("vch_CodReserva"))
                hdnTipoPed.Value = Trim(dtbPedido.Rows(0).Item("TI_EPOT"))
                If hdnCodReserva.Value <> "" And dtbPedido.Rows(0).Item("ti_situ") = "PRE-DESPACHO" Then
                    btnImprimirEtiqueta.Visible = True
                    btnDespachar.Text = "Despachar Reserva"
                    btnDespachar.ToolTip = "Despachar Reserva Num: " + hdnCodReserva.Value
                    txtEstado.Text += " (Cod. Reserva: " + hdnCodReserva.Value + ")"
                Else
                    btnImprimirEtiqueta.Visible = False
                    btnDespachar.Text = "Despachar vale"
                    btnDespachar.ToolTip = "Despachar Pedido"
                End If

                ' -- Consultamos Solicitante
                'prcConsultaEmpleado(dtbPedido.Rows(0).Item("CodSolicitante"))

                ' -- Consultamos Detalle de Pedidos
                'ldtDetalle = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)
                ldtDetalle = objPedidos.fncConsultaDetallePedidoEPPsOtros("2", strSerie, intCodPedido)
                If Not ldtDetalle Is Nothing Then
                    Session("Detalle") = ldtDetalle
                    dgDetalle.DataSource = ldtDetalle
                    dgDetalle.DataBind()
                    dgDetalle.Visible = True
                End If
                'Session("Detalle") = dgDetalle
                intNumItems = ldtDetalle.Rows.Count.ToString
                lblItems.Text = "Numero de Articulos por Atender: " + ldtDetalle.Rows.Count.ToString
                lblItems.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error al cargar el vale." + ex.Message
        End Try
    End Sub

    ' Consulta Empleado
    Private Sub prcConsultaEmpleado(ByVal strCodSolicitante As String)
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtbEmpleados As New DataTable
        Dim Ruta As String = ""
        lblError.Text = ""
        dtbEmpleados = Nothing
        Try
            dtbEmpleados = objPedidos.fncPedidoConsultaEmpleado(strCodSolicitante)
            If Not dtbEmpleados Is Nothing Or dtbEmpleados.Rows.Count > 0 Then
                txtCodSolicitante.Text = dtbEmpleados.Rows(0).Item("co_trab")
                txtDesSolicitante.Text = dtbEmpleados.Rows(0).Item("Nombres")
                txtDesArea.Text = dtbEmpleados.Rows(0).Item("de_area")
                txtDesSeccion.Text = dtbEmpleados.Rows(0).Item("de_secc")
                txtDesCargo.Text = dtbEmpleados.Rows(0).Item("de_pues_trab")
                Ruta = "\\" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "\fotos$\" + txtCodSolicitante.Text + ".jpg"
                imgFoto.ImageUrl = Ruta
                If (dtbEmpleados.Rows(0).Item("fe_cese_trab")) = "Ceso" Then
                    lblError.Text = "Cambie por favor el empleado, ha cesado."
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al consultar el empleado." + ex.Message
            txtCodRecepciona.Text = ""
        End Try
    End Sub

    ' menejo de estado
    Private Sub ManejoEstados(ByVal strEstado As String)
        Select Case strEstado
            Case "APROBADO"
                txtEstado.Text = strEstado
                prcHabilitaControles(True)
                HabilitaBotonesAccion(True, True, True, True)
            Case "ATENDIDO"
                txtEstado.Text = strEstado
                prcHabilitaControles(False)
                HabilitaBotonesAccion(True, False, False, False)
                lblError.Text = "Vale de almacen ya ha sido atendido, no puede ser modificado"
            Case "CULMINADO"
                txtEstado.Text = strEstado
                lblError.Text = "Vale de almacen ha sido culminado, no puede ser modificado"
                prcHabilitaControles(False)
                HabilitaBotonesAccion(True, False, False, False)
            Case "PRE-DESPACHO"
                txtEstado.Text = strEstado
                lblError.Text = "Vale de almacen se encuentra en Pre-Despacho, no puede ser modificado"
                prcHabilitaControles(True)
                HabilitaBotonesAccion(True, False, True, False)
        End Select
    End Sub
    'Manejo de Estado de Vale para despacho
    Private Sub prcHabilitaControles(ByVal lblEstado As Boolean)
        txtFechaDespacho.Enabled = lblEstado
        txtCodRecepciona.Enabled = lblEstado
        'imgBuscarCliente.Enabled = lblEstado
        'txtObservaciones.Enabled = lblEstado
        dgDetalle.Enabled = lblEstado
    End Sub

    ' habilita botones
    Private Sub HabilitaBotonesAccion(ByVal blnVer As Boolean, ByVal blnCulminar As Boolean, ByVal blnDespachar As Boolean, ByVal blnPreDespacho As Boolean)
        btnVerVale.Enabled = blnVer
        btnCulminar.Enabled = blnCulminar
        btnDespachar.Enabled = blnDespachar
        'btnPreDespachar.Enabled = blnPreDespacho
    End Sub

#Region "Grilla"
    Private Sub dgDetalle_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetalle.ItemDataBound
        Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim ltxtDespachable As TextBox = CType(e.Item.FindControl("txtDespachable"), TextBox)
        Dim lbtnEditar As ImageButton = CType(e.Item.FindControl("btnGuardarItem"), ImageButton)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Try
                Select Case e.Item.ItemType
                    Case ListItemType.AlternatingItem, ListItemType.Item
                        Dim Stock As Double
                        Stock = CDbl(ldrvDatos("stock"))
                        Select Case e.Item.ItemType
                            Case ListItemType.AlternatingItem, ListItemType.Item
                                If ldrvDatos("ti_situ") <> "PRE" Then
                                    If CDbl(ldrvDatos("ca_pend")) > 0 And CDbl(ldrvDatos("stock")) <= 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Red
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "Pend./Insuf."
                                        ltxtDespachable.ReadOnly = True
                                    End If
                                    If CDbl(ldrvDatos("ca_pend")) = 0 And (CDbl(ldrvDatos("ca_apro")) - CDbl(ldrvDatos("ca_aten"))) = 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Blue
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "Atendido"
                                        ltxtDespachable.ReadOnly = True
                                    End If
                                    If CDbl(ldrvDatos("ca_pend")) > 0 And CDbl(ldrvDatos("stock")) > 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Green
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "Despachable"
                                        If hdnTipoPed.Value = 7 Then
                                            ltxtDespachable.ReadOnly = True
                                        Else
                                            ltxtDespachable.ReadOnly = False
                                        End If
                                    End If
                                    If CDbl(ldrvDatos("ca_pend")) = 0 And (CDbl(ldrvDatos("ca_apro")) - CDbl(ldrvDatos("ca_aten"))) > 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Red
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "No Atendido"
                                        ltxtDespachable.ReadOnly = False
                                    End If
                                Else
                                    CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.OrangeRed
                                    CType(e.Item.FindControl("lblsituacion"), Label).Text = "Reservado"
                                    ltxtDespachable.ReadOnly = True
                                End If
                                CType(e.Item.FindControl("txtDespachable"), TextBox).Attributes.Add("onkeydown", "return fSoloNumeros(event);")
                                CType(e.Item.FindControl("txtDespachable"), TextBox).Attributes.Add("onblur", "return VerificaNumero('" & CDbl(ldrvDatos("CA_DESP")) & "', this);")


                        End Select
                End Select
            Catch ex As Exception
                btnDespachar.Enabled = False
            End Try
        End If
    End Sub
#End Region

    Private Sub btnCulminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCulminar.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtbPedido As New DataTable
        Dim strNumeroPedido As String = ""
        Dim strUsuarioModi As String = ""
        Dim i As Integer = 0

        lblError.Text = ""

        strNumeroPedido = txtNumeroPedido.Text
        strUsuarioModi = Session("@USUARIO")
        Try
            ldtbPedido = New DataTable
            ldtbPedido = objPedido.fncPedidoCambiaEstado("3", strNumeroPedido, strUsuarioModi, Nothing)

            If Not ldtbPedido Is Nothing And ldtbPedido.Rows.Count > 0 Then
                btnDespachar.Enabled = False
                btnCulminar.Enabled = False
                txtEstado.Text = "CULMINADO"
                lblError.Text = "Vale fue culminado con exito"
            End If
        Catch ex As Exception
            lblError.Text = "Error al culminar vale." + ex.Message
        End Try
    End Sub
    ' boton despachar
    Private Sub btnDespachar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDespachar.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtbDetalle As New DataTable
        Dim dtbRetorno As New DataTable
        Dim strNumeroPedido As String = ""
        Dim strActivoFijo As String = ""
        Dim strUsuarioDespachador As String = ""
        Dim strUsuarioRecepcionador As String = ""
        Dim strCodAlmacen As String = ""
        Dim strCodCentroCostos As String = ""
        Dim intCodPedido As Integer = 0

        ldtbDetalle = Nothing
        dtbRetorno = Nothing

        strNumeroPedido = Trim(txtNumeroPedido.Text)
        strActivoFijo = ""
        'If txtCodRecepciona.Text.Length = 0 Then
        '    strUsuario = Trim(txtCodRecepciona.Text)
        'Else
        '    strUsuario = Session("@USUARIO")
        'End If
        If txtDesArea.Text = "" Then
            lblError.Text = "Debe cargar los datos de la persona"
            Exit Sub
        Else
            lblError.Text = ""
        End If
        strUsuarioDespachador = Session("@USUARIO")
        strUsuarioRecepcionador = Trim(txtCodRecepciona.Text)
        strCodAlmacen = Mid(Trim(txtDesAlmacen.Text), 1, 3)
        strCodCentroCostos = Mid(txtDesCentroCostos.Text, 1, 7)
        Try

            Dim resulVal As String = ""
            resulVal = fnc_ValidarDespachoRepetidoPorPedidoUsuario(Trim(txtNumeroPedido.Text), Trim(txtCodRecepciona.Text))
            If resulVal <> "" Then
                lblError.Text = resulVal
                Exit Sub
            End If

            'Guardamos datos
            'fncGuardarDetalle()
            Dim resultDet As String = fncGuardarDetalleEPPS()
            If resultDet <> "" Then
                lblError.Text = resultDet
                Exit Sub
            End If

            ' extraemos la data actualizada solo lo despachable (con stock y pendiente)
            ldtbDetalle = objPedido.fnc_ListarPedidoPorDespachar("1", strNumeroPedido, strUsuarioDespachador)
            If Not ldtbDetalle Is Nothing And ldtbDetalle.Rows.Count > 0 Then

                'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
                ' Guardamos el vale de salida
                'dtbRetorno = objPedido.fnc_PedidoValesDetalle_Grabar(strUsuario, strNumeroPedido)
                dtbRetorno = objPedido.fnc_PedidoValesDetalleEPPsOtros_Grabar(strUsuarioDespachador, strNumeroPedido, strUsuarioRecepcionador)

                'ACTUALIZAR PARA PEDIDOS 0005 - DG - FIN
                lblError.Text = dtbRetorno.Rows(0)("Resultado")

                If Mid(dtbRetorno.Rows(0)("Resultado"), 1, 5) <> "Error" Then

                    'Se imprimen los Voucher si existen vales generados (VS y VTC)
                    If dtbRetorno.Rows(0)("Resultado").ToString.Trim = "VALESGENERADOS" Then
                        For Each row As DataRow In dtbRetorno.Rows
                            strVale_NumDocu = row("NU_DOCU").ToString
                            strVale_TipoDocu = row("TI_DOCU").ToString
                            strVale_CodAlm = row("CO_ALMA").ToString
                            strVale_CodEmp = row("CO_EMPR").ToString
                            If chkbox.Checked = True Then
                                Call ImprimirValesGenerados()
                            End If

                        Next
                    End If
                    'If hdnCodReserva.Value <> "" Then
                    'CargaPedido(txtNumeroPedido.Text) 'Add 07/11/2014 (Luis_AJ)
                    'Else
                    ' Refrescamos la grilla
                    intCodPedido = Integer.Parse(Mid(txtNumeroPedido.Text, 6, 10))
                    'ldtbDetalle = objPedido.fncConsultaDetallePedido("2", Mid(txtNumeroPedido.Text, 1, 4), intCodPedido)
                    ldtbDetalle = objPedido.fncConsultaDetallePedidoEPPsOtros("2", Mid(txtNumeroPedido.Text, 1, 4), intCodPedido)

                    Dim result As String
                    result = objPedido.ActualizaEstadoPedidoEPPS(strUsuarioDespachador, txtNumeroPedido.Text)


                    Session("Detalle") = ldtbDetalle
                    dgDetalle.DataSource = ldtbDetalle
                    dgDetalle.DataBind()
                    dgDetalle.Visible = True
                    'btnDespachar.Enabled = False
                    txtCodRecepciona.Text = ""
                    imgFoto.ImageUrl = ""
                    txtDesArea.Text = ""
                    txtDesCargo.Text = ""
                    txtDesSeccion.Text = ""
                    btnDespachar.Enabled = True

                    If result <> "" Then
                        lblError.Text = "Hubo un problema al actualizar el estado del pedido."
                        Exit Sub
                    End If

                    'End If

                    lblError.Text = "Se atendio el pedido con exito. Consulte los vales generados. #vale " + strVale_NumDocu.ToString()

                Else
                    lblError.Text = "Error al despachar vale."
                End If
            Else
                lblError.Text = "Verique stock, no es posible registrar la salida."
            End If
        Catch ex As Exception
            lblError.Text = "Error al despachar el vale." + ex.Message
        End Try
    End Sub
    Private Sub ImprimirValesGenerados()
        Dim lobjDocumentoImp As New PrintDocument, lstrImpresora As String
        'Dim ldtbResultado As New DataTable

        Dim lobjLogistica As New Logistica.clsPedidos
        Dim strResult As Integer

        Try
            'OBTENER EL NOMBRE DE LA IMPRESORA CODIGO DE TABLA GENERAL ##
            If strVale_CodAlm = "002" Then 'Impresora Almacen
                lstrImpresora = "\\ALMPRODQUIM\POS_ALM_PQ" '"\\ALMPRINCIPAL\EPSON"
            Else
                lstrImpresora = "POS_ALM_LOG" ' "\\SERVNMPRB\POS_ALM_LOG" '
            End If

            lobjDocumentoImp.PrinterSettings.PrinterName = lstrImpresora
            lobjDocumentoImp.DefaultPageSettings.Landscape = False

            AddHandler lobjDocumentoImp.PrintPage, AddressOf lobjDocumentoImp_PrintThePage
            lobjDocumentoImp.Print()
            lobjDocumentoImp = Nothing

            strResult = lobjLogistica.ufn_ActualizaDocumentoImpreso(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)

        Catch ex As Exception
            Throw ex
            'ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & ex.Message & "');</script>")
        End Try
    End Sub
    Public Function fnc_ValidarDespachoRepetidoPorPedidoUsuario(ByVal NuPedido As String, ByVal CodEmpleado As String) As String
        Dim result As String = ""
        Dim ldtDetalle As New DataTable
        Dim objPedidos As New Logistica.clsPedidos
        Try
            result = objPedidos.ValidarDespachoRepetidoPorPedidoUsuario(NuPedido, CodEmpleado)
        Catch ex As Exception
            result = ex.Message
        End Try
        Return result
    End Function
    Private Sub lobjDocumentoImp_PrintThePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim lobjLogistica As New Logistica.clsPedidos
        Dim lblnImpresionOK As Boolean = False
        Dim dtDocumento As New DataTable

        Try
            'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
            'dtDocumento = lobjLogistica.ufn_BuscaDatosDocumentos(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)
            dtDocumento = lobjLogistica.ufn_BuscaDatosDocumentosEPPsOtros(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)
            'ACTUALIZAR PARA PEDIDOS 0005 - DG - FIN

            If dtDocumento.Rows.Count > 0 Then
                Dim lobjPencil As Pen = New Pen(System.Drawing.Color.Black, 0.5)
                Dim lobjFuente As Font
                Dim format As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                Dim FontName As String = "Courier New"
                '------------------------------
                'Dim strNombreDocu As String = ""
                Dim strAuxiliar As String = ""
                Dim strObservaciones As String = ""
                Dim intPosY As Integer
                Dim dblTotalCant As Double
                Dim dblTotalAlterno As Double
                Dim intTotalItem As Integer
                Dim strNumeroDocu As String
                Dim strUsuario_Crea As String
                Dim strDescripcion As String
                '------------------------------

                dtDocumento.TableName = "Documento"
                intPosY = 0
                '------------------------------------------------------------------------------
                'BEGIN: CONFIGURAR DISEÑO DOCUMENTO
                '------------------------------------------------------------------------------
                'Cabecera
                lobjFuente = New Font(FontName, 8, FontStyle.Bold)
                e.Graphics.DrawString("Cia. Ind. Nuevo Mundo S.A.", lobjFuente, Brushes.Black, 35, intPosY)
                'e.Graphics.DrawString("Pág. 0001", lobjFuente, Brushes.Black, 250, 10, format)

                'TITULO DOCUMENTO
                lobjFuente = New Font(FontName, 9, FontStyle.Regular)
                intPosY += 30
                If strVale_TipoDocu = "VS" Then
                    e.Graphics.DrawString("VALE DE SALIDA", lobjFuente, Brushes.Black, 70, intPosY)
                ElseIf strVale_TipoDocu = "VTC" Then
                    e.Graphics.DrawString("VALE DE TRABAJO EN CURSO", lobjFuente, Brushes.Black, 30, intPosY)
                ElseIf strVale_TipoDocu = "NI" Then
                    e.Graphics.DrawString("NOTA DE INGRESO POR COMPRA", lobjFuente, Brushes.Black, 25, intPosY)
                End If

                'NUMERO DOCUMENTO
                lobjFuente = New Font(FontName, 10, FontStyle.Regular)
                intPosY += 10
                strNumeroDocu = strVale_TipoDocu & " - " & dtDocumento.Rows(0).Item("DOCUMENTO")
                e.Graphics.DrawString(strNumeroDocu, lobjFuente, Brushes.Black, 100 - ((strNumeroDocu.Length / 2) * 6), intPosY)

                lobjFuente = New Font(FontName, 7, FontStyle.Regular)

                'FECHA
                intPosY += 30
                e.Graphics.DrawString("Fecha:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("FECHA_DOCUMENTO"), lobjFuente, Brushes.Black, 60, intPosY)

                'ALMACEN
                intPosY += 10
                e.Graphics.DrawString("Almacen:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(strVale_CodAlm, lobjFuente, Brushes.Black, 60, intPosY)

                'OPERACION
                e.Graphics.DrawString("Operación:", lobjFuente, Brushes.Black, 140, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("TIPO_OPERACION"), lobjFuente, Brushes.Black, 210, intPosY)

                intPosY += 10
                If strVale_TipoDocu = "VS" Or strVale_TipoDocu = "VTC" Then
                    'PEDIDO
                    e.Graphics.DrawString("Pedido:", lobjFuente, Brushes.Black, 0, intPosY)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("NUMERO_PEDIDO"), lobjFuente, Brushes.Black, 60, intPosY)

                    'COD AUXILIAR
                    intPosY += 10
                    e.Graphics.DrawString("Auxiliar:", lobjFuente, Brushes.Black, 0, intPosY)
                    strAuxiliar = dtDocumento.Rows(0).Item("NOMBRE_AUXILIAR").ToString.ToLower.Replace(vbCrLf, " ")
                    If strAuxiliar.Length > 0 Then
                        e.Graphics.DrawString(strAuxiliar.Substring(0, IIf(strAuxiliar.Length > 32, 31, strAuxiliar.Length)), lobjFuente, Brushes.Black, 60, intPosY)
                        If strAuxiliar.Length > 32 Then
                            intPosY += 10
                            e.Graphics.DrawString(strAuxiliar.Substring(31, IIf(strAuxiliar.Length > 63, 31, strAuxiliar.Length - 31)), lobjFuente, Brushes.Black, 60, intPosY)
                        End If
                    End If

                    'OBSERVACIONES
                    strObservaciones = dtDocumento.Rows(0).Item("OBSERVACION").ToString.ToLower.Replace(vbCrLf, " ")
                    If strObservaciones.Length > 0 Then
                        intPosY += 20
                        e.Graphics.DrawString("Obs:", lobjFuente, Brushes.Black, 0, intPosY)
                        e.Graphics.DrawString(strObservaciones.Substring(0, IIf(strObservaciones.Length > 38, 37, strObservaciones.Length)).Trim, lobjFuente, Brushes.Black, 25, intPosY)

                        If strObservaciones.Length > 38 Then
                            intPosY += 10
                            e.Graphics.DrawString(strObservaciones.Substring(37, IIf(strObservaciones.Length > 75, 37, strObservaciones.Length - 37)).Trim, lobjFuente, Brushes.Black, 25, intPosY)
                        End If

                        If strObservaciones.Length > 75 Then
                            intPosY += 10
                            e.Graphics.DrawString(strObservaciones.Substring(74, IIf(strObservaciones.Length > 112, 37, strObservaciones.Length - 74)).Trim, lobjFuente, Brushes.Black, 25, intPosY)
                        End If
                    End If

                ElseIf strVale_TipoDocu = "NI" Then

                    'PROVEEDOR
                    e.Graphics.DrawString("Proveedor:", lobjFuente, Brushes.Black, 0, intPosY)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("PROVEEDOR"), lobjFuente, Brushes.Black, 60, intPosY)

                    'GUIA PROVEEDOR
                    intPosY += 10
                    e.Graphics.DrawString("Guia Proveedor:", lobjFuente, Brushes.Black, 0, intPosY)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("GUIA_PROVEEDOR"), lobjFuente, Brushes.Black, 90, intPosY)

                    'ORDEN COMPRA
                    intPosY += 10
                    e.Graphics.DrawString("Orden Compra:", lobjFuente, Brushes.Black, 0, intPosY)
                    lobjFuente = New Font(FontName, 8, FontStyle.Regular)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("ORDEN_COMPRA"), lobjFuente, Brushes.Black, 90, intPosY)
                End If

                strUsuario_Crea = dtDocumento.Rows(0).Item("USUARIO_CREA")

                'ITEMS DETALLE
                lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                dblTotalCant = 0.0
                dblTotalAlterno = 0.0
                intTotalItem = 0
                intPosY += 20

                'LINEA INICIO DETALLE
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)
                intPosY += 10
                e.Graphics.DrawString("CODIGO", lobjFuente, Brushes.Black, 15, intPosY)
                e.Graphics.DrawString("CANTIDAD", lobjFuente, Brushes.Black, 190, intPosY, format)
                e.Graphics.DrawString("ALTERNA", lobjFuente, Brushes.Black, 250, intPosY, format)
                intPosY += 10
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)
                intPosY += 20

                For Each row As DataRow In dtDocumento.Rows
                    'Codigo Item
                    e.Graphics.DrawString(row("CODIGO_ITEM"), lobjFuente, Brushes.Black, 0, intPosY)

                    'Unidad
                    If row("CODIGO_ITEM").ToString.Length < 16 Then
                        e.Graphics.DrawString(row("UNIDAD"), lobjFuente, Brushes.Black, 95, intPosY)
                    End If

                    'Cantidad
                    e.Graphics.DrawString(row("CANTIDAD"), lobjFuente, Brushes.Black, 190, intPosY, format)

                    'Cantidad Alterna
                    e.Graphics.DrawString(row("CANTIDAD_ALTERNA"), lobjFuente, Brushes.Black, 250, intPosY, format)

                    'Descripcion Item 
                    intPosY += 8
                    lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                    strDescripcion = row("DESCRIPCION_ITEM").ToString.Trim
                    e.Graphics.DrawString(strDescripcion.Substring(0, IIf(strDescripcion.Length > 42, 41, strDescripcion.Length)).Trim, lobjFuente, Brushes.Black, 0, intPosY)
                    If strDescripcion.Length > 42 Then
                        intPosY += 8
                        e.Graphics.DrawString(strDescripcion.Substring(41, IIf(strDescripcion.Length > 82, 41, strDescripcion.Length - 41)).Trim, lobjFuente, Brushes.Black, 0, intPosY)
                    End If

                    'lobjFuente = New Font(FontName, 8, FontStyle.Regular)
                    If strVale_TipoDocu = "NI" Then
                        intPosY += 8
                        e.Graphics.DrawString("Ubicación: ", lobjFuente, Brushes.Black, 0, intPosY)
                        lobjFuente = New Font(FontName, 8, FontStyle.Regular)
                        e.Graphics.DrawString(row("UBICACION_ALMACEN"), lobjFuente, Brushes.Black, 80, intPosY)
                    End If

                    intPosY += 20
                    intTotalItem += 1
                    dblTotalCant += CDbl(row("CANTIDAD"))
                    dblTotalAlterno += CDbl(row("CANTIDAD_ALTERNA"))
                    lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                Next

                'LINEA FIN DETALLE
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)

                intPosY += 10
                If strVale_TipoDocu = "NI" Then
                    e.Graphics.DrawString("TOTAL GRAL:", lobjFuente, Brushes.Black, 40, intPosY)
                    e.Graphics.DrawString(FormatNumber(dblTotalCant, 2), lobjFuente, Brushes.Black, 190, intPosY, format)
                    e.Graphics.DrawString(FormatNumber(dblTotalAlterno, 2), lobjFuente, Brushes.Black, 250, intPosY, format)
                    intPosY += 10
                End If

                e.Graphics.DrawString("TOTAL ITEMS:", lobjFuente, Brushes.Black, 40, intPosY)
                e.Graphics.DrawString(intTotalItem, lobjFuente, Brushes.Black, 190, intPosY, format)

                'LINEA FIN SUBTOTAL
                intPosY += 15
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)

                'FIRMA (Solo Vale de Salida)
                If strVale_TipoDocu = "VS" Or strVale_TipoDocu = "VTC" Then
                    intPosY += 65
                    e.Graphics.DrawString("-------------------", lobjFuente, Brushes.Black, 120, intPosY)
                    intPosY += 10
                    e.Graphics.DrawString("RECIBIDO POR", lobjFuente, Brushes.Black, 140, intPosY)
                End If

                intPosY += 40
                'USUARIO
                e.Graphics.DrawString(strUsuario_Crea, lobjFuente, Brushes.Black, 0, intPosY)
                'FECHA IMPRESION
                e.Graphics.DrawString(Now(), lobjFuente, Brushes.Black, 105, intPosY)


                'FIN PAGINA
                e.HasMorePages = False

                'ELIMINAR OBJETOS
                lobjLogistica = Nothing
                'lobjLoteRollo = Nothing
                dtDocumento = Nothing
                'limgImagen = Nothing
            Else
                Throw New Exception("No se encontro el Documento")
            End If

        Catch ex As Exception
            e.Cancel = True
            Throw (ex)
        Finally
            e.Graphics.Dispose()
            e.HasMorePages = False
        End Try
    End Sub
    ' Funcion Guarda en masivo
    Public Sub fncGuardarDetalle()
        Dim i As Integer = 0
        Dim max As Integer = 0
        Dim intCodPedido As Integer = 0
        Dim strSerie As String = ""
        Dim strNumeroPedido As String = ""
        Dim strCodArticulo As String = ""
        Dim strCtaGasto As String = ""
        Dim strOrdenServicio As String = ""
        Dim strSecuencia As String = ""
        Dim strUsuario As String = ""
        Dim ldblNewCantidad As Double = 0
        Dim ldblNewPendiente As Double = 0
        Dim ldblStock As Double = 0
        Dim ldblDespachable As Double = 0
        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtDetalleOriginal As New DataTable
        Dim txtDespachable As TextBox
        Dim strTipo As String = ""
        Dim strCentroCostos As String

        Try
            ' extraemo la tabla con datos originales
            strSerie = "0005"
            intCodPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
            'ldtDetalleOriginal = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)
            ldtDetalleOriginal = objPedidos.fncConsultaDetallePedidoEPPsOtros("2", strSerie, intCodPedido)

            ' Guardamos los cambios en las filas editadas
            strNumeroPedido = Trim(txtNumeroPedido.Text)
            strUsuario = Session("@USUARIO")
            If Not ldtDetalleOriginal Is Nothing Then
                max = ldtDetalleOriginal.Rows.Count
                strTipo = "2"
                If max > 0 Then
                    For i = 0 To max - 1
                        ' datos originales
                        strSecuencia = ldtDetalleOriginal.Rows(i).Item("nu_secu").ToString
                        strCodArticulo = ldtDetalleOriginal.Rows(i).Item("co_item").ToString
                        ldblDespachable = ldtDetalleOriginal.Rows(i).Item("ca_desp")

                        txtDespachable = dgDetalle.Items(i).Cells(0).FindControl("txtDespachable")
                        ldblNewCantidad = Double.Parse(txtDespachable.Text)
                        ldblStock = ldtDetalleOriginal.Rows(i).Item("Stock")
                        ldblNewPendiente = ldtDetalleOriginal.Rows(i).Item("ca_pend")

                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("CtaGasto")) = True Then
                            strCtaGasto = ""
                        Else
                            strCtaGasto = ldtDetalleOriginal.Rows(i).Item("CtaGasto")
                        End If
                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("ActivoFijo")) = True Then
                            strOrdenServicio = ""
                        Else
                            strOrdenServicio = ldtDetalleOriginal.Rows(i).Item("ActivoFijo")
                        End If

                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("CentroCostos")) = True Then
                            strCentroCostos = ""
                        Else
                            strCentroCostos = ldtDetalleOriginal.Rows(i).Item("CentroCostos")
                        End If

                        prcActualizaDetalle(strNumeroPedido, strCodArticulo, ldblNewCantidad, ldblStock, _
                            ldblNewPendiente, strUsuario, strCtaGasto, strOrdenServicio, strCentroCostos)
                    Next
                End If
            Else
                lblError.Text = "Error no hay datos para edicion."
            End If
        Catch ex As Exception
            lblError.Text = "Error al guardar el detalle." + ex.Message
        End Try

    End Sub
    Public Function fncGuardarDetalleEPPS() As String
        Dim i As Integer = 0
        Dim max As Integer = 0
        Dim intCodPedido As Integer = 0
        Dim strSerie As String = ""
        Dim strNumeroPedido As String = ""
        Dim strCodArticulo As String = ""
        Dim strCtaGasto As String = ""
        Dim strOrdenServicio As String = ""
        Dim strSecuencia As String = ""
        Dim strUsuario As String = ""
        Dim ldblNewCantidad As Double = 0
        Dim ldblNewPendiente As Double = 0
        Dim ldblStock As Double = 0
        Dim ldblDespachable As Double = 0
        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtDetalleOriginal As New DataTable
        Dim txtDespachable As TextBox
        Dim strTipo As String = ""
        Dim result As String = ""
        Dim strCentroCostos As String

        Try
            ' extraemo la tabla con datos originales
            strSerie = "0005"
            intCodPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
            'ldtDetalleOriginal = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)
            'ldtDetalleOriginal = objPedidos.fncConsultaDetallePedidoEPPsOtros("2", strSerie, intCodPedido) 'ULTIMO
            ldtDetalleOriginal = objPedidos.fncConsultaDetallePedidoEPPsOtrosPorUsuario(Mid(txtNumeroPedido.Text, 1, 4), intCodPedido, Trim(txtCodRecepciona.Text))
            ' Guardamos los cambios en las filas editadas
            strNumeroPedido = Trim(txtNumeroPedido.Text)
            strUsuario = Session("@USUARIO")
            If Not ldtDetalleOriginal Is Nothing Then
                max = ldtDetalleOriginal.Rows.Count
                strTipo = "2"
                If max > 0 Then
                    For i = 0 To max - 1
                        ' datos originales
                        strSecuencia = ldtDetalleOriginal.Rows(i).Item("nu_secu").ToString
                        strCodArticulo = ldtDetalleOriginal.Rows(i).Item("co_item").ToString
                        ldblDespachable = ldtDetalleOriginal.Rows(i).Item("ca_desp")

                        txtDespachable = dgDetalle.Items(i).Cells(0).FindControl("txtDespachable")
                        ldblNewCantidad = Double.Parse(txtDespachable.Text)
                        ldblStock = ldtDetalleOriginal.Rows(i).Item("Stock")
                        ldblNewPendiente = ldtDetalleOriginal.Rows(i).Item("ca_pend")

                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("CtaGasto")) = True Then
                            strCtaGasto = ""
                        Else
                            strCtaGasto = ldtDetalleOriginal.Rows(i).Item("CtaGasto")
                        End If
                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("ActivoFijo")) = True Then
                            strOrdenServicio = ""
                        Else
                            strOrdenServicio = ldtDetalleOriginal.Rows(i).Item("ActivoFijo")
                        End If

                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("CentroCostos")) = True Then
                            strCentroCostos = ""
                        Else
                            strCentroCostos = ldtDetalleOriginal.Rows(i).Item("CentroCostos")
                        End If

                        'prcActualizaDetalle(strNumeroPedido, strCodArticulo, ldblNewCantidad, ldblStock, _
                        '    ldblNewPendiente, strUsuario, strCtaGasto, strOrdenServicio)
                        result = prcActualizaDetalleEPPS(strNumeroPedido, strCodArticulo, ldblNewCantidad, ldblStock, _
                            ldblNewPendiente, strUsuario, strCtaGasto, strOrdenServicio, strCentroCostos)
                    Next
                End If
            Else
                lblError.Text = "Error no hay datos para edicion."
                result = lblError.Text
            End If

        Catch ex As Exception
            lblError.Text = "Error al guardar el detalle." + ex.Message
            result = lblError.Text
        End Try
        Return result
    End Function
    ' Actualizamos la cantidad a despachar
    Private Sub prcActualizaDetalle(ByVal strNumPedido As String, ByVal strCodArticulo As String, ByVal ldblNewCantidad As Double, ByVal ldblStock As Double, _
                                ByVal ldblNewPendiente As Double, ByVal strUsuario As String, ByVal strCtaGasto As String, ByVal strOrdenServicio As String, _
                                ByVal strCentroCostos As String)
        Dim ldtDetalle As New DataTable
        Dim objPedidos As New Logistica.clsPedidos
        ldtDetalle = Nothing
        lblError.Text = ""
        Try
            ' Validamos que eliga un articulo a modificar
            If strCodArticulo.Length > 0 Then
                ' Validamos que ingrese cantidad
                If ldblNewCantidad >= 0 Then
                    ' Validamos que ingrese cantidad pendiente
                    If ldblNewCantidad <= ldblNewPendiente Then
                        ' Validamos que exista Stock
                        If ldblStock >= ldblNewCantidad Then
                            Try
                                ' Actualizamos en Pedido
                                ldtDetalle = objPedidos.fncActualizarCantidades("2", strNumPedido, strCodArticulo, _
                                                        ldblNewCantidad, strUsuario, strCtaGasto, strOrdenServicio, strCentroCostos)
                            Catch ex As Exception
                                lblError.Text = "Error al actualizar vale." + ex.Message
                            End Try
                        Else
                            lblError.Text = "Error debe Ingresar una cantidad menor o igual al stock"
                        End If
                    Else
                        lblError.Text = "Error la cantidad ingresada es mayor a lo pendiente por pespachar"
                    End If
                Else
                    lblError.Text = "Error debe ingresar una cantidad valida, mayor a cero"
                End If
            Else
                lblError.Text = "Error: debe elegir un registro a modificar de la lista"
            End If
        Catch ex As Exception
            lblError.Text = "Error al actualizar detalle de vale." + ex.Message
        End Try
    End Sub
    Private Function prcActualizaDetalleEPPS(ByVal strNumPedido As String, ByVal strCodArticulo As String, ByVal ldblNewCantidad As Double, ByVal ldblStock As Double, _
                               ByVal ldblNewPendiente As Double, ByVal strUsuario As String, ByVal strCtaGasto As String, ByVal strOrdenServicio As String, _
                                ByVal strCentroCostos As String) As String
        Dim ldtDetalle As New DataTable
        Dim objPedidos As New Logistica.clsPedidos
        ldtDetalle = Nothing
        lblError.Text = ""
        Dim result As String = ""
        Try
            ' Validamos que eliga un articulo a modificar
            If strCodArticulo.Length > 0 Then
                ' Validamos que ingrese cantidad
                If ldblNewCantidad >= 0 Then
                    ' Validamos que ingrese cantidad pendiente
                    If ldblNewCantidad <= ldblNewPendiente Then
                        ' Validamos que exista Stock
                        If ldblStock >= ldblNewCantidad Then
                            Try
                                ' Actualizamos en Pedido
                                ldtDetalle = objPedidos.fncActualizarCantidades("2", strNumPedido, strCodArticulo, _
                                                        ldblNewCantidad, strUsuario, strCtaGasto, strOrdenServicio, strCentroCostos)
                            Catch ex As Exception
                                lblError.Text = "Error al actualizar vale." + ex.Message
                                result = lblError.Text
                            End Try
                        Else
                            lblError.Text = "Error debe Ingresar una cantidad menor o igual al stock"
                            result = lblError.Text
                        End If
                    Else
                        lblError.Text = "Error la cantidad ingresada es mayor a lo pendiente por pespachar"
                        result = lblError.Text
                    End If
                Else
                    lblError.Text = "Error debe ingresar una cantidad valida, mayor a cero"
                    result = lblError.Text
                End If
            Else
                lblError.Text = "Error: debe elegir un registro a modificar de la lista"
                result = lblError.Text
            End If

        Catch ex As Exception
            lblError.Text = "Error al actualizar detalle de vale." + ex.Message
            result = lblError.Text
        End Try
        Return result
    End Function

    Protected Sub carga_Click(sender As Object, e As EventArgs) Handles carga.Click

        Dim objPedido As New Logistica.clsPedidos

        Try
            If txtCodRecepciona.Text <> "" Then
                prcConsultaEmpleado(txtCodRecepciona.Text)
                'CONSULTAR LOS PEDIDOS Y COLOCAR LA CANTIAD POR PERSONA

                intCodPedido = Integer.Parse(Mid(txtNumeroPedido.Text, 6, 10))

                Call ConsultaDetallePedidoEEPsotrosPorUsuario(txtNumeroPedido.Text, intCodPedido, txtCodRecepciona.Text)

            Else
                'ACA TENDREMOS QUE LEER DESDE LA BD DONDE SE GUARDA LOS REGISTROS, OBTENDREMOS EL ULTIMO EN HABER PUESTO SU HUELLA ES DECIR CON ESTADO SIN DESPACHAR 
                ', UNA VES DEPSACHADO SE LE CAMBIO DE ESTADO A ESTE REGISTRO DE HUELLA

                Dim codRecepciona As String

                codRecepciona = objPedido.ObtenerUltimaHuellaRegistrado(hdnTipoPed.Value)
                If (codRecepciona = "") Then
                    lblError.Text = "Error al consultar ultima huella registrada"
                    txtCodRecepciona.Text = ""
                    Exit Sub
                End If
                txtCodRecepciona.Text = codRecepciona
                prcConsultaEmpleado(codRecepciona)
                intCodPedido = Integer.Parse(Mid(txtNumeroPedido.Text, 6, 10))

                Call ConsultaDetallePedidoEEPsotrosPorUsuario(txtNumeroPedido.Text, intCodPedido, txtCodRecepciona.Text)


            End If
        Catch ex As Exception
            lblError.Text = "Sucedio un error al cargar los datos del pedido :" + ex.Message
            txtCodRecepciona.Text = ""
        End Try

    End Sub
    Protected Sub ConsultaDetallePedidoEEPsotrosPorUsuario(ByVal numeroPedido As String, ByVal intCodPedido As Integer, ByVal codRecepciona As String)
        Try
            Dim ldtbDetalle As New DataTable
            Dim objPedido As New Logistica.clsPedidos

            ldtbDetalle = objPedido.fncConsultaDetallePedidoEPPsOtrosPorUsuario(Mid(numeroPedido, 1, 4), intCodPedido, codRecepciona)
            If Not ldtbDetalle Is Nothing Then
                Session("Detalle") = ldtbDetalle
                dgDetalle.DataSource = ldtbDetalle
                dgDetalle.DataBind()
                dgDetalle.Visible = True
                intNumItems = ldtbDetalle.Rows.Count.ToString
                lblItems.Text = "Numero de Articulos por Atender: " + ldtbDetalle.Rows.Count.ToString
                lblItems.Visible = True
            End If
            btnDespachar.Enabled = True
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = "Error al consultar el detalle del pedido ." + ex.Message
            txtCodRecepciona.Text = ""
        End Try
    End Sub
End Class