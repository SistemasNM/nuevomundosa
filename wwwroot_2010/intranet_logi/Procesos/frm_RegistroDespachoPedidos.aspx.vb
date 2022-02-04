Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Drawing.Printing
Imports System.Drawing

Public Class frm_RegistraDespachoPedidos
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

    Protected WithEvents imgBuscarCliente As System.Web.UI.WebControls.ImageButton

    Protected WithEvents txtDespachable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnVerVale As System.Web.UI.WebControls.Button
    Protected WithEvents btnVales As System.Web.UI.WebControls.Button
    Protected WithEvents btnPreDespachar As System.Web.UI.WebControls.Button

    Protected WithEvents hdnCodEmpresa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodUnidad As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodAlmacen As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodReserva As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents btnImprimirEtiqueta As System.Web.UI.WebControls.ImageButton

    Protected WithEvents btncarga As System.Web.UI.WebControls.Button
    Protected WithEvents hdnTipoPed As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents chkbox As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Session("@GRUPO_CODIGO") = "3000"
        'Session("@EMPRESA") = "01"
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
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
        End If
        btnSalir.Attributes.Add("onClick", "javascript:fnc_Cerrar();")
        btnVales.Attributes.Add("onClick", "javascript:VerValesPedido('" + txtNumeroPedido.Text + "')")
        btnDespachar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion('Despachar');")
        btnPreDespachar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion('PreDespachar');")
        btnCulminar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion('Culminar');")

    End Sub

#Region "Pedido"

    ' Cargar el pedido
    Private Sub CargaPedido(ByVal strNumPedido As String)
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtbPedido As New DataTable
        Dim ldtDetalle As New DataTable

        Dim intCodPedido As Integer = 0
        Dim strSerie As String = "0003"
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
            dtbPedido = objPedidos.fncConsultaPedidos(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
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
                ldtDetalle = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)
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
        Dim strOrdenTrabajo As String

        Try
            ' extraemo la tabla con datos originales
            strSerie = "0003"
            intCodPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
            ldtDetalleOriginal = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)

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

                        If IsDBNull(ldtDetalleOriginal.Rows(i).Item("OrdenTrabajo")) = True Then
                            strOrdenTrabajo = ""
                        Else
                            strOrdenTrabajo = ldtDetalleOriginal.Rows(i).Item("OrdenTrabajo")
                        End If


                        prcActualizaDetalle(strNumeroPedido, strCodArticulo, ldblNewCantidad, ldblStock, _
                            ldblNewPendiente, strUsuario, strCtaGasto, strOrdenServicio, strCentroCostos, strOrdenTrabajo)
                    Next
                End If
            Else
                lblError.Text = "Error no hay datos para edicion."
            End If
        Catch ex As Exception
            lblError.Text = "Error al guardar el detalle." + ex.Message
        End Try

    End Sub

    ' Actualizamos la cantidad a despachar
    Private Sub prcActualizaDetalle(ByVal strNumPedido As String, ByVal strCodArticulo As String, ByVal ldblNewCantidad As Double, ByVal ldblStock As Double, _
                                ByVal ldblNewPendiente As Double, ByVal strUsuario As String, ByVal strCtaGasto As String, ByVal strOrdenServicio As String, _
                                ByVal strCentroCostos As String, ByVal strOrdenTrabajo As String)
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
                                                        ldblNewCantidad, strUsuario, strCtaGasto, strOrdenServicio, _
                                                        strCentroCostos, strOrdenTrabajo)
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

#End Region

#Region "Metodos-Funciones"

    ' Listar reporte detalle
    Private Sub fnc_VerReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim intNumPedido As Integer = 0
        Dim strReporte As String = ""
        Try
            intNumPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
            'CAMBIO DG INI
            'strPath = "%2fNM_Reportes%2f"
            'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
            'strURL = strURL + "logistica_ConsultaVale"
            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
            strReporte = "log_consulta_vale"
            strURL = strURL + strPath + strReporte
            'CAMBIO DG FIN
            strURL = strURL + "&chrTipo=" & "0"
            strURL = strURL + "&intNumPedido=" & intNumPedido.ToString

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
        Catch ex As Exception
            lblError.Text = "Error la mostrar el reporte." + ex.Message
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
        End Try
    End Sub

    'Manejo de Estado de Vale para despacho
    Private Sub prcHabilitaControles(ByVal lblEstado As Boolean)
        txtFechaDespacho.Enabled = lblEstado
        txtCodRecepciona.Enabled = lblEstado
        'MOFICICACION HUELLERO - INI
        'imgBuscarCliente.Enabled = lblEstado
        'MOFICICACION HUELLERO - INI
        txtObservaciones.Enabled = lblEstado
        dgDetalle.Enabled = lblEstado
    End Sub

    ' habilita botones
    Private Sub HabilitaBotonesAccion(ByVal blnVer As Boolean, ByVal blnCulminar As Boolean, ByVal blnDespachar As Boolean, ByVal blnPreDespacho As Boolean)
        btnVerVale.Enabled = blnVer
        btnCulminar.Enabled = blnCulminar
        btnDespachar.Enabled = blnDespachar
        btnPreDespachar.Enabled = blnPreDespacho
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
#End Region

#Region "Botones"

    ' boton ver reporte
    Private Sub btnVerVale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerVale.Click
        fnc_VerReporte()
    End Sub
    ' boton buscar cliente
    Private Sub imgBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarCliente.Click
        Dim CodSolicitante As String = ""
        CodSolicitante = Trim(txtCodRecepciona.Text)
        Try
            If CodSolicitante <> Trim(txtCodSolicitante.Text) Then
                prcConsultaEmpleado(CodSolicitante)
            Else
                lblError.Text = "Debe elegir otro empleado para despacho"
            End If
        Catch ex As Exception
            lblError.Text = "Error a buscar el cliente." + ex.Message
        End Try

    End Sub

    ' boton despachar
    Private Sub btnDespachar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDespachar.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtbDetalle As New DataTable
        Dim dtbRetorno As New DataTable
        Dim strNumeroPedido As String = ""
        Dim strActivoFijo As String = ""
        Dim strUsuario As String = ""
        Dim strCodAlmacen As String = ""
        Dim strCodCentroCostos As String = ""
        Dim intCodPedido As Integer = 0
        Dim strUsuarioRecepcionador As String = ""
        ldtbDetalle = Nothing
        dtbRetorno = Nothing

        strNumeroPedido = Trim(txtNumeroPedido.Text)
        strActivoFijo = ""
        'If txtCodRecepciona.Text.Length = 0 Then
        '    strUsuario = Trim(txtCodRecepciona.Text)
        'Else
        '    strUsuario = Session("@USUARIO")
        'End If
        strUsuario = Session("@USUARIO")
        If txtDesArea.Text = "" Then
            lblError.Text = "Debe cargar los datos de la persona"
            Exit Sub
        Else
            lblError.Text = ""
        End If
        strCodAlmacen = Mid(Trim(txtDesAlmacen.Text), 1, 3)
        strCodCentroCostos = Mid(txtDesCentroCostos.Text, 1, 7)
        strUsuarioRecepcionador = Trim(txtCodRecepciona.Text)
        Try
            'Guardamos datos
            fncGuardarDetalle()

            ' extraemos la data actualizada solo lo despachable (con stock y pendiente)
            ldtbDetalle = objPedido.fnc_ListarPedidoPorDespachar("1", strNumeroPedido, strUsuario)
            If Not ldtbDetalle Is Nothing And ldtbDetalle.Rows.Count > 0 Then
                ' Guardamos el vale de salida
                dtbRetorno = objPedido.fnc_PedidoValesDetalle_Grabar(strUsuario, strNumeroPedido, strUsuarioRecepcionador)

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
                    If hdnCodReserva.Value <> "" Then
                        CargaPedido(txtNumeroPedido.Text) 'Add 07/11/2014 (Luis_AJ)
                        'CAMBIO  DG - INI
                        Dim result As String
                        result = objPedido.ActualizaEstadoPedido(strUsuario, txtNumeroPedido.Text)
                        'CAMBIO DG - FIN
                    Else
                        ' Refrescamos la grilla
                        intCodPedido = Integer.Parse(Mid(txtNumeroPedido.Text, 6, 10))
                        ldtbDetalle = objPedido.fncConsultaDetallePedido("2", Mid(txtNumeroPedido.Text, 1, 4), intCodPedido)

                        'CAMBIO  DG - INI
                        Dim result As String
                        result = objPedido.ActualizaEstadoPedido(strUsuario, txtNumeroPedido.Text)
                        'CAMBIO DG - FIN

                        Session("Detalle") = ldtbDetalle
                        dgDetalle.DataSource = ldtbDetalle
                        dgDetalle.DataBind()
                        dgDetalle.Visible = True
                        btnDespachar.Enabled = False
                    End If

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

    ' boton culminar vale
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

    'boton Pre-despacho
    Protected Sub btnPreDespachar_Click(sender As Object, e As EventArgs) Handles btnPreDespachar.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim strNumPedido As String
        Dim strCodEmpresa As String
        Dim strCodUnidad As String
        Dim strCodAlmacen As String
        Dim strUsuario As String
        Dim txtDespachable As TextBox
        Dim lblSecuencia As Label
        Dim lblCodigoItem As Label
        Dim lblCentroCostos As Label
        Dim lblCtagasto As Label
        Dim lblActivoFijo As Label
        Dim strResult As String

        Dim dtbDetReserva As New DataTable
        dtbDetReserva.Columns.Add("NUMERO_SECUENCIA", GetType(Int16))
        dtbDetReserva.Columns.Add("CODIGO_ITEM", GetType(String))
        dtbDetReserva.Columns.Add("CANTIDAD_RESERVA", GetType(Double))
        dtbDetReserva.Columns.Add("COD_DESTINO_FINAL", GetType(String))
        dtbDetReserva.Columns.Add("COD_CENTRO_COSTOS", GetType(String))
        dtbDetReserva.Columns.Add("COD_ORDEN_SERVICIO", GetType(String))

        Try
            'Obtenemos los valores para la cabecera de la reserva
            strNumPedido = Trim(txtNumeroPedido.Text)
            strCodEmpresa = hdnCodEmpresa.Value
            strCodUnidad = hdnCodUnidad.Value
            strCodAlmacen = hdnCodAlmacen.Value
            strUsuario = Session("@USUARIO")

            'Obtenemos los valores para el detalle de la reserva
            For i As Integer = 0 To dgDetalle.Items.Count - 1
                lblSecuencia = dgDetalle.Items(i).Cells(0).FindControl("lblSecuencia")
                lblCodigoItem = dgDetalle.Items(i).Cells(0).FindControl("lblCodigo")
                txtDespachable = dgDetalle.Items(i).Cells(0).FindControl("txtDespachable")
                lblCtagasto = dgDetalle.Items(i).Cells(0).FindControl("lblCtagasto")
                lblCentroCostos = dgDetalle.Items(i).Cells(0).FindControl("lblCentroCostos")
                lblActivoFijo = dgDetalle.Items(i).Cells(0).FindControl("lblActivoFijo")

                Dim dtrNuevo As DataRow = dtbDetReserva.NewRow
                dtrNuevo("NUMERO_SECUENCIA") = lblSecuencia.Text
                dtrNuevo("CODIGO_ITEM") = lblCodigoItem.Text
                dtrNuevo("CANTIDAD_RESERVA") = txtDespachable.Text
                dtrNuevo("COD_DESTINO_FINAL") = IIf(IsDBNull(lblCtagasto.Text), "", lblCtagasto.Text)
                dtrNuevo("COD_CENTRO_COSTOS") = IIf(IsDBNull(lblCentroCostos.Text), "", lblCentroCostos.Text)
                dtrNuevo("COD_ORDEN_SERVICIO") = IIf(IsDBNull(lblActivoFijo.Text), "", lblActivoFijo.Text)
                
                dtbDetReserva.LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            strResult = objPedido.ufn_GrabarReservaPreDespacho(strNumPedido, strCodEmpresa, strCodUnidad, strCodAlmacen, strUsuario, dtbDetReserva)

            If strResult <> "" Then
                hdnCodReserva.Value = strResult
                ImprimirCodigoPreDespachoGenerado()
                ImprimirComandaPreDespacho()
                CargaPedido(strNumPedido)
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
            'ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & ex.Message & "');</script>")
        End Try



    End Sub

#End Region

#Region "Grilla"

    Private Sub dgDetalle_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetalle.CancelCommand
        dgDetalle.EditItemIndex = -1
        strNumPedido = Request.Item("strNumeroPedido")
        CargaPedido(strNumPedido)
    End Sub

    Private Sub dgDetalle_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetalle.EditCommand
        dgDetalle.EditItemIndex = e.Item.ItemIndex
        strNumPedido = Request.Item("strNumeroPedido")
        CargaPedido(strNumPedido)
    End Sub



    Private Sub dgDetalle_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetalle.ItemDataBound
        Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim ltxtDespachable As TextBox = CType(e.Item.FindControl("txtDespachable"), TextBox)
        Dim lbtnEditar As ImageButton = CType(e.Item.FindControl("btnGuardarItem"), ImageButton)
        Dim EditarItem As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
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
                                        EditarItem.Visible = False
                                    End If
                                    '-----CAMBIO DG - INI-----------
                                    'If CDbl(ldrvDatos("ca_pend")) > 0 And CDbl(ldrvDatos("stock")) > 0 Then
                                    '    CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Green
                                    '    CType(e.Item.FindControl("lblsituacion"), Label).Text = "Despachable"
                                    '    ltxtDespachable.ReadOnly = False
                                    'End If
                                    If CDbl(ldrvDatos("ca_pend")) > 0 And CDbl(ldrvDatos("stock")) > 0 And CDbl(ldrvDatos("ca_disponible")) > 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Green
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "Despachable"
                                        ltxtDespachable.ReadOnly = False
                                    End If
                                    If CDbl(ldrvDatos("ca_pend")) > 0 And CDbl(ldrvDatos("stock")) > 0 And CDbl(ldrvDatos("ca_disponible")) = 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Red
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "Pend./Insuf."
                                        ltxtDespachable.ReadOnly = True
                                    End If
                                    '-----CAMBIO DG - FIN-----------
                                    If CDbl(ldrvDatos("ca_pend")) = 0 And (CDbl(ldrvDatos("ca_apro")) - CDbl(ldrvDatos("ca_aten"))) > 0 Then
                                        CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.Red
                                        CType(e.Item.FindControl("lblsituacion"), Label).Text = "No Atendido"
                                        ltxtDespachable.ReadOnly = False
                                    End If
                                Else
                                    CType(e.Item.FindControl("lblsituacion"), Label).ForeColor = System.Drawing.Color.OrangeRed
                                    CType(e.Item.FindControl("lblsituacion"), Label).Text = "Reservado"
                                    ltxtDespachable.ReadOnly = True
                                    EditarItem.Visible = False
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

    Private Sub lobjDocumentoImp_PrintThePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim lobjLogistica As New Logistica.clsPedidos
        Dim lblnImpresionOK As Boolean = False
        Dim dtDocumento As New DataTable

        Try
            dtDocumento = lobjLogistica.ufn_BuscaDatosDocumentos(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)

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

    Public Function ImprimirCodigoPreDespachoGenerado() As Boolean
        Dim lobjEtiqueta_PreDespacho As New PrintDocument
        Dim lstrImpresora As String

        Try
            lstrImpresora = "ETI_LOGISTICA" '"doPDF v5" '' 
            lobjEtiqueta_PreDespacho.PrinterSettings.PrinterName = lstrImpresora
            lobjEtiqueta_PreDespacho.PrinterSettings.Copies = 2
            lobjEtiqueta_PreDespacho.DefaultPageSettings.Landscape = False

            AddHandler lobjEtiqueta_PreDespacho.PrintPage, AddressOf lobjEtiqueta_PreDespacho_PrintThePage
            lobjEtiqueta_PreDespacho.Print()
            lobjEtiqueta_PreDespacho = Nothing

        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function

    Private Sub lobjEtiqueta_PreDespacho_PrintThePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim lobjPedido As New Logistica.clsPedidos
        Dim lblnImpresionOK As Boolean = False
        Dim ldtDocumento As New DataTable
        Dim lstrCodReserva As String
        Dim lstrCodigoBarra As String

        lstrCodReserva = hdnCodReserva.Value.ToString

        Try
            ldtDocumento = lobjPedido.ufn_BuscarDatosEtiquetaPreDespacho(lstrCodReserva)

            Dim lobjPencil As Drawing.Pen = New Drawing.Pen(System.Drawing.Color.Black, 0.5)
            Dim lobjFuente As Drawing.Font = New Drawing.Font("Arial", 9, Drawing.FontStyle.Regular)
            Dim intLongitud As Integer
            e.HasMorePages = False
            e.PageSettings.Landscape = False

            '------------------------------------------------------------------------------
            'BEGIN: CONFIGURAR DATOS FIJOS DE LA ETIQUETA
            '------------------------------------------------------------------------------

            lobjFuente = New Drawing.Font("Arial", 10, Drawing.FontStyle.Regular)
            e.Graphics.DrawString("PEDIDO: " + ldtDocumento.Rows(0).Item("vch_NumPedido").ToString, lobjFuente, Drawing.Brushes.Black, 40, 15)

            lobjFuente = New Drawing.Font("Arial", 8, Drawing.FontStyle.Regular)
            e.Graphics.DrawString("FECHA: " + ldtDocumento.Rows(0).Item("dtm_FechaReserva").ToString, lobjFuente, Drawing.Brushes.Black, 40, 29)

            lobjFuente = New Drawing.Font("Arial", 10, Drawing.FontStyle.Regular)
            e.Graphics.DrawString("TOTAL", lobjFuente, Drawing.Brushes.Black, 225, 42)

            lobjFuente = New Drawing.Font("Arial", 13, Drawing.FontStyle.Regular)
            intLongitud = ldtDocumento.Rows(0).Item("int_TotalItem").ToString.Length
            e.Graphics.DrawString(ldtDocumento.Rows(0).Item("int_TotalItem").ToString, lobjFuente, Drawing.Brushes.Black, 244 - (intLongitud * 4), 62)

            'CODIGO DE BARRA EN BARRAS

            lobjFuente = New Drawing.Font("IDAutomationHC39M", 11, Drawing.FontStyle.Regular)

            lstrCodigoBarra = "*" + ldtDocumento.Rows(0).Item("vch_CodReserva").ToString.Trim + "*"
            e.Graphics.DrawString(lstrCodigoBarra, lobjFuente, Drawing.Brushes.Black, 40, 42)

            e.Graphics.DrawEllipse(Pens.Black, 215, 30, 65, 65)

            '------------------------------------------------------------------------------
            'END: CONFIGURAR DATOS FIJOS DE LA ETIQUETA
            '------------------------------------------------------------------------------
        Catch ex As Exception
            e.Cancel = True
            Throw ex
        Finally
            lobjPedido = Nothing
            e.Graphics.Dispose()
            e.HasMorePages = False
        End Try
    End Sub

    Private Sub ImprimirComandaPreDespacho()
        Dim lobjComanda_PreDespacho As New PrintDocument, lstrImpresora As String
        'Dim ldtbResultado As New DataTable
        Dim lobjPedido As New NuevoMundo.Logistica.clsPedidos

        Try
            lstrImpresora = "POS_ALM_LOG" '    '"doPDF v5" ' "\\SERVNMPRB\POS_ALM_LOG" '
            lobjComanda_PreDespacho.PrinterSettings.PrinterName = lstrImpresora
            lobjComanda_PreDespacho.PrinterSettings.Copies = 1
            lobjComanda_PreDespacho.DefaultPageSettings.Landscape = False

            AddHandler lobjComanda_PreDespacho.PrintPage, AddressOf lobjComanda_PreDespacho_PrintThePage
            lobjComanda_PreDespacho.Print()
            lobjComanda_PreDespacho = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lobjComanda_PreDespacho_PrintThePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim lobjPedido As New NuevoMundo.Logistica.clsPedidos
        Dim lblnImpresionOK As Boolean = False
        Dim dtDocumento As New DataTable
        Dim lstrCodReserva As String

        Try
            lstrCodReserva = hdnCodReserva.Value.ToString
            dtDocumento = lobjPedido.ufn_BuscarDetalleComanda_ConsultaPocket(lstrCodReserva)

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
                Dim strUsuario_Crea As String
                Dim strDescripcion As String
                '------------------------------

                dtDocumento.TableName = "Documento"
                intPosY = 0
                '------------------------------------------------------------------------------
                'BEGIN: CONFIGURAR DISEÑO DOCUMENTO
                '------------------------------------------------------------------------------

                'Logo PreDespacho
                intPosY += 5
                e.Graphics.DrawEllipse(Pens.Black, 108, intPosY, 45, 45)
                'e.Graphics.DrawRectangle(Pens.Black, 120, intPosY, 30, 30)
                lobjFuente = New Font(FontName, 30, FontStyle.Bold)
                e.Graphics.DrawString("C", lobjFuente, Brushes.Black, 110, intPosY)
                intPosY += 55

                'Cabecera
                lobjFuente = New Font(FontName, 8, FontStyle.Bold)
                e.Graphics.DrawString("Cia. Ind. Nuevo Mundo S.A.", lobjFuente, Brushes.Black, 35, intPosY)
                'e.Graphics.DrawString("Pág. 0001", lobjFuente, Brushes.Black, 250, 10, format)

                'TITULO DOCUMENTO
                lobjFuente = New Font(FontName, 9, FontStyle.Regular)
                intPosY += 30
                e.Graphics.DrawString("COMANDA PRE-DESPACHO", lobjFuente, Brushes.Black, 35, intPosY)

                lobjFuente = New Font(FontName, 8, FontStyle.Regular)

                'CODIGO RESERVA
                intPosY += 20
                e.Graphics.DrawString("Cod. Reserva:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("CODIGO_RESERVA"), lobjFuente, Brushes.Black, 100, intPosY)

                'FECHA
                intPosY += 10
                e.Graphics.DrawString("Fecha:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("FECHA_CREACION"), lobjFuente, Brushes.Black, 100, intPosY)

                'PEDIDO
                intPosY += 10
                e.Graphics.DrawString("Cod. Pedido:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("NUMERO_PEDIDO"), lobjFuente, Brushes.Black, 100, intPosY)

                'OBSERVACIONES
                lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                strObservaciones = dtDocumento.Rows(0).Item("OBSERVACIONES").ToString.ToLower.Replace(vbCrLf, " ")
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


                strUsuario_Crea = dtDocumento.Rows(0).Item("USUARIO_CREACION")

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
                e.Graphics.DrawString("CANTIDAD", lobjFuente, Brushes.Black, 220, intPosY, format)
                intPosY += 10
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)
                intPosY += 20

                For Each row As DataRow In dtDocumento.Rows
                    'Codigo Item
                    e.Graphics.DrawString(row("CODIGO_ARTICULO"), lobjFuente, Brushes.Black, 0, intPosY)

                    'Cantidad
                    e.Graphics.DrawString(row("CANTIDAD_RESERVADA"), lobjFuente, Brushes.Black, 220, intPosY, format)

                    'Descripcion Item 
                    intPosY += 8
                    lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                    strDescripcion = row("DESCRIPCION_ARTICULO").ToString.Trim
                    e.Graphics.DrawString(strDescripcion.Substring(0, IIf(strDescripcion.Length > 42, 41, strDescripcion.Length)).Trim, lobjFuente, Brushes.Black, 0, intPosY)
                    If strDescripcion.Length > 42 Then
                        intPosY += 8
                        e.Graphics.DrawString(strDescripcion.Substring(41, IIf(strDescripcion.Length > 82, 41, strDescripcion.Length - 41)).Trim, lobjFuente, Brushes.Black, 0, intPosY)
                    End If

                    intPosY += 20
                    intTotalItem += 1
                    dblTotalCant += CDbl(row("CANTIDAD_RESERVADA"))
                    lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                Next

                'LINEA FIN DETALLE
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)

                intPosY += 10
                e.Graphics.DrawString("TOTAL GRAL:", lobjFuente, Brushes.Black, 40, intPosY)
                e.Graphics.DrawString(FormatNumber(dblTotalCant, 2), lobjFuente, Brushes.Black, 190, intPosY, format)

                intPosY += 10
                e.Graphics.DrawString("TOTAL ITEMS:", lobjFuente, Brushes.Black, 40, intPosY)
                e.Graphics.DrawString(intTotalItem, lobjFuente, Brushes.Black, 190, intPosY, format)

                'LINEA FIN SUBTOTAL
                intPosY += 15
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)

                'FIRMA 
                intPosY += 65
                e.Graphics.DrawString("-------------------", lobjFuente, Brushes.Black, 120, intPosY)
                intPosY += 10
                e.Graphics.DrawString("RECIBIDO POR", lobjFuente, Brushes.Black, 140, intPosY)


                intPosY += 40
                'USUARIO
                e.Graphics.DrawString(strUsuario_Crea, lobjFuente, Brushes.Black, 0, intPosY)
                'FECHA IMPRESION
                e.Graphics.DrawString(Now(), lobjFuente, Brushes.Black, 105, intPosY)


                'FIN PAGINA
                e.HasMorePages = False

                'ELIMINAR OBJETOS
                lobjPedido = Nothing
                'lobjLoteRollo = Nothing
                dtDocumento = Nothing
                'limgImagen = Nothing
            Else
                Throw New Exception("No se encontro ningun registro para esta reserva.")
            End If

        Catch ex As Exception
            e.Cancel = True
            Throw (ex)
        Finally
            e.Graphics.Dispose()
            e.HasMorePages = False
        End Try
    End Sub

#End Region


    Private Sub btnImprimirEtiqueta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnImprimirEtiqueta.Click
        Dim strCodReserva As String

        strCodReserva = hdnCodReserva.Value.Trim
        Try
            If strCodReserva <> "" Then
                ImprimirCodigoPreDespachoGenerado()
                ImprimirComandaPreDespacho()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
    End Sub
    Protected Sub btncarga_Click(sender As Object, e As EventArgs) Handles btncarga.Click

        Dim objPedido As New Logistica.clsPedidos

        Try
            If txtCodRecepciona.Text <> "" Then
                prcConsultaEmpleado(txtCodRecepciona.Text)
                'CONSULTAR LOS PEDIDOS Y COLOCAR LA CANTIAD POR PERSONA

                intCodPedido = Integer.Parse(Mid(txtNumeroPedido.Text, 6, 10))


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


            End If
        Catch ex As Exception
            lblError.Text = "Sucedio un error al cargar los datos del pedido :" + ex.Message
            txtCodRecepciona.Text = ""
        End Try

    End Sub

    Private Sub dgDetalle_UpdateCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetalle.UpdateCommand
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtbPedido As New DataTable

        Dim NumSecu As String = CType(e.Item.FindControl("lblSecuencia_Edit"), Label).Text
        Dim Cantidad As String = CType(e.Item.FindControl("txtCantidad_Edi"), TextBox).Text
        Dim CantidadAtendida As String = CType(e.Item.FindControl("lblCantidadAtendida_Edit"), Label).Text
        Dim CantidadAprobadaOriginal As String = CType(e.Item.FindControl("txtCantidad_Edi_hiden"), HiddenField).Value

        If CType(Cantidad, Integer) < CType(CantidadAtendida, Integer) Then
            lblError.Text = "La modificación de la cantidad aprobada no puede ser menor a la cantidad atendida"
            Exit Sub
        End If

        If CType(Cantidad, Integer) > CType(CantidadAprobadaOriginal, Integer) Then
            lblError.Text = "La modificación de la cantidad aprobada no puede ser mayor a la cantidad aprobada original"
            Exit Sub
        End If


        Dim dt As DataTable = objPedidos.ActualizarCantidadAprobada(txtNumeroPedido.Text, NumSecu, Cantidad)
        dgDetalle.EditItemIndex = -1
        strNumPedido = Request.Item("strNumeroPedido")
        CargaPedido(strNumPedido)
    End Sub
End Class
