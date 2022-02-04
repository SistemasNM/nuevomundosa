Imports OFISIS.OFILOGI

Public Class frm_Registromuestras
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlTipo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNumero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSituacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmbAlmacen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtObservacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnSolicitar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnular As System.Web.UI.WebControls.Button
    Protected WithEvents BtnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents hdnCodigo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAprobacion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAreaSolicitante As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDNArticulo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDNUnidadMedida As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDNCtaGasto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HdnDescServicio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
    Protected WithEvents TxtCodigoCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNombreCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblReg As System.Web.UI.WebControls.Label
    Protected WithEvents hdnIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents cmbTipoPagoMuestra As System.Web.UI.WebControls.DropDownList
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Public Const vrCTE_MSG_GRAB = "Se Grabo la cabecera de la Solicitud...<br>Ingrese los articulos del detalle."
    Public Const vrCTE_MSG_ANU = "Se Anulo la Solicitud "
    Public Const vrCTE_MSG_INS_DET = "Ingreso Articulo al Detalle con exito..."
    Public Const vrCTE_MSG_DEL_DET = "Articulo eliminado del Detalle con exito..."

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION0¿
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "DGAMARRA"

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Dim strNumeroSolicitud = "" & Request.QueryString("pstrNumeroSolicitud")

            Me.ddlTipo.SelectedValue = "0"

            Me.hdnIdentificador.Value = System.Guid.NewGuid().ToString()
            HttpContext.Current.Session(Me.hdnIdentificador.Value + "_NUMERO") = ""
            HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION") = ""
            HttpContext.Current.Session(Me.hdnIdentificador.Value + "_TIPOSOL") = ""

            'strNumeroSolicitud = "2010-0000000012"
            If strNumeroSolicitud = "" Then
                Me.txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
                Me.btnGrabar.Visible = True
                Me.btnAnular.Visible = False
                Me.BtnNuevo.Visible = False
                Me.btnSolicitar.Visible = False
                Me.DataGrid1.EnableViewState = True
                Me.DataGrid1.Visible = True
                Session.Add("Nuevo", True)
                btnSolicitar.Attributes.Add("onClick", "javascript:return SolicitarAprobacion();")
                btnGrabar.Attributes.Add("onClick", "javascript:return fnc_ValidarSolicitudMuestras();")
                TxtCodigoCliente.Attributes.Add("onBlur", "BuscarCentroCosto()")
            Else

                Call sCargaSolicitudMuestras(strNumeroSolicitud)

                HttpContext.Current.Session(Me.hdnIdentificador.Value + "_TIPOSOL") = Me.ddlTipo.SelectedValue

            End If
            btnAnular.Attributes.Add("onClick", "javascript:return fnc_ConfirmarAnulacion();")

            '20120906 EPM
            txtNumero.Attributes.Add("readonly", "readonly")
            txtFecha.Attributes.Add("readonly", "readonly")
            txtSituacion.Attributes.Add("readonly", "readonly")
            txtNombreCliente.Attributes.Add("readonly", "readonly")

        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'If fValida() = False Then Exit Sub
        If cmbTipoPagoMuestra.SelectedValue = "0" Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Debe ingresar Tipo de Pago Muestra');</script>")
            Exit Sub
        End If
        Dim vrFlag As Boolean = False

        If Trim("" & HttpContext.Current.Session(Me.hdnIdentificador.Value + "_NUMERO")) = "" Then
            Call fActDataIns()
            vrFlag = True
            HttpContext.Current.Session(Me.hdnIdentificador.Value + "_NUMERO") = Me.txtNumero.Text
            HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION") = Me.txtSituacion.Text
            HttpContext.Current.Session(Me.hdnIdentificador.Value + "_TIPOSOL") = ddlTipo.SelectedValue
        Else
            Me.txtNumero.Text = Trim("" & HttpContext.Current.Session(Me.hdnIdentificador.Value + "_NUMERO"))
            Me.txtSituacion.Text = Trim("" & HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION"))
            Me.ddlTipo.SelectedValue = Trim("" & HttpContext.Current.Session(Me.hdnIdentificador.Value + "_TIPOSOL"))
        End If

        Me.BtnNuevo.Visible = True
        Me.btnAnular.Visible = True
        Me.DataGrid1.EnableViewState = True
        Me.DataGrid1.Visible = True
        Me.btnGrabar.Visible = False

        Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

        If vrFlag = True Then Me.lblMsg.Text = vrCTE_MSG_GRAB

    End Sub
    Private Sub sCargaSolicitudMuestras(ByVal pstrSolicitud As String)
        Dim lobMuestras As New OFISIS.OFILOGI.Muestras_Telas
        Dim objDataSet As New DataSet
        lobMuestras.NumeroSolicitud = pstrSolicitud
        'CAMBIO DG AGREGAR PRECIO - INI
        'Call lobMuestras.Detalle_SolicitudMuestras_Mostrar(objDataSet)
        Call lobMuestras.Detalle_SolicitudMuestras_Mostrar_V2(objDataSet)
        'CAMBIO DG AGREGAR PRECIO - FIN
        With objDataSet.Tables(0)
            Me.txtNumero.Text = .Rows(0)("Var_NumeroSolicitud")
            Me.cmbAlmacen.SelectedValue = .Rows(0)("chr_CodigoAlmacen")
            Me.ddlTipo.SelectedValue = .Rows(0)("TipoMuestra")
            Me.txtObservacion.Text = .Rows(0)("var_Observaciones")
            Me.txtFecha.Text = .Rows(0)("Fecha")
            Me.TxtCodigoCliente.Text = .Rows(0)("var_CodigoCliente")
            Me.txtNombreCliente.Text = .Rows(0)("NO_CLIE")
            Me.txtSituacion.Text = .Rows(0)("chr_EstadoDocumento")
            Me.cmbTipoPagoMuestra.SelectedValue = .Rows(0)("var_TipoPago")
            Me.cmbTipoPagoMuestra.Enabled = False
            Me.btnGrabar.Visible = False

            If .Rows(0)("chr_EstadoDocumento") = "INI" Then
                Me.btnSolicitar.Visible = True
            Else
                Me.btnSolicitar.Visible = False
            End If


            If .Rows(0)("chr_EstadoDocumento") = "ATE" Then
                Me.btnAnular.Visible = False
            End If

        End With
        DataGrid1.DataSource = objDataSet.Tables(1)
        DataGrid1.DataBind()
        lblReg.Text = objDataSet.Tables(1).Rows.Count

        If objDataSet.Tables(1).Rows.Count = 0 Then
            btnSolicitar.Visible = False
        End If

        lobMuestras = Nothing
    End Sub
    Private Function fActDataIns() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Registra los datos de la cabecera de la Solicitud de Muestras
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 21-09-2010
        'Modificado : 
        '*****************************************************************************************************
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim strNumeroSolictud As String
        fActDataIns = True
        lobjMuestrasTela.FechaSolicitud = Mid(Me.txtFecha.Text, 4, 2) + "/" + Mid(Me.txtFecha.Text, 1, 2) + "/" + Right(Me.txtFecha.Text, 4)
        lobjMuestrasTela.CodigoCliente = Me.TxtCodigoCliente.Text
        lobjMuestrasTela.TipoMuestra = Me.ddlTipo.SelectedValue
        lobjMuestrasTela.CodigoAlmacen = Me.cmbAlmacen.SelectedValue
        lobjMuestrasTela.EstadoSolicitud = "ACT"
        If ddlTipo.SelectedValue = 5 Then
            lobjMuestrasTela.EstadoDocumento = "APR"
        Else : lobjMuestrasTela.EstadoDocumento = "INI"
        End If
        lobjMuestrasTela.Observaciones = Me.txtObservacion.Text
        lobjMuestrasTela.Usuario = Session("@USUARIO")

        lobjMuestrasTela.TipoPagoMuestra = cmbTipoPagoMuestra.SelectedValue
        
        strNumeroSolictud = lobjMuestrasTela.SolicitudMuestras_Insertar


        If strNumeroSolictud <> "" Then
            Me.txtNumero.Text = strNumeroSolictud
            lblMsg.ForeColor = Drawing.Color.Red
            'lblMsg.Text = "Datos actualizado con éxito"
            lblMsg.Text = vrCTE_MSG_GRAB
        Else
            fActDataIns = False
            lblMsg.Text = lobjMuestrasTela.clsError
        End If
        Me.txtSituacion.Text = "ACT"
        lobjMuestrasTela = Nothing
    End Function
    Private Function fActDataDel() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Registra los datos de la cabecera de la Solicitud de Muestras
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 21-09-2010
        'Modificado : 
        '*****************************************************************************************************
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim strNumeroSolictud As String
        fActDataDel = True
        lobjMuestrasTela.Usuario = Session("@USUARIO")
        lobjMuestrasTela.NumeroSolicitud = Me.txtNumero.Text

        Call lobjMuestrasTela.SolicitudMuestras_Eliminar()

        Me.txtSituacion.Text = "ANU"
        lblMsg.ForeColor = Drawing.Color.Red
        'lblMsg.Text = "Numero de Solicitud fue eliminado con éxito....!"
        lblMsg.Text = vrCTE_MSG_ANU & Me.txtNumero.Text
        lobjMuestrasTela = Nothing
    End Function

    Private Sub BtnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNuevo.Click
        Response.Redirect("frm_Registromuestras.aspx")
        'LimpiarObjetos()
    End Sub

    Function LimpiarObjetos()
        Me.ddlTipo.SelectedValue = "0"
        Me.txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
        Me.TxtCodigoCliente.Text = ""
        Me.txtNombreCliente.Text = ""
        Me.txtObservacion.Text = ""
        Me.txtNumero.Text = ""
        Me.txtSituacion.Text = ""
        Me.lblMsg.Text = ""
        Me.lblReg.Text = ""
        Me.btnGrabar.Visible = True
        Me.btnSolicitar.Visible = False
        Me.btnAnular.Visible = False
        Me.DataGrid1.Visible = False

        Me.hdnIdentificador.Value = System.Guid.NewGuid().ToString()
        HttpContext.Current.Session(Me.hdnIdentificador.Value + "_NUMERO") = ""
        HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION") = ""
        HttpContext.Current.Session(Me.hdnIdentificador.Value + "_TIPOSOL") = ""

    End Function

    Private Sub sCargaDetalleSolicitudMuestras(ByVal strNumeroMuestra As String)
        '*****************************************************************************************************
        'Objetivo   : Muestra el detalle del documento de solicitud de muestras
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 29/06/2010
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        lblMsg.Text = ""
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim objDT As New DataTable
        lobjMuestrasTela.NumeroSolicitud = strNumeroMuestra
        'mFila1 = 0
        'CAMBIO DG - INI
        'If lobjMuestrasTela.Detalle_SolicitudMuestras_Listar(objDT) = True Then
        If lobjMuestrasTela.Detalle_SolicitudMuestras_Listar_v2(objDT) = True Then
            'CAMBIO DG - FIN
            'lblReg.Text = objDT.Rows.Count
            Me.DataGrid1.Visible = True
            Me.DataGrid1.EnableViewState = True
            Me.DataGrid1.DataSource = objDT
            Me.DataGrid1.DataBind()
            Me.cmbTipoPagoMuestra.SelectedValue = cmbTipoPagoMuestra.SelectedValue
            lblReg.Text = objDT.Rows.Count

            If objDT.Rows.Count = 0 Then
                btnSolicitar.Visible = False
            End If
        Else
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = lobjMuestrasTela.clsError
        End If
        objDT = Nothing
        lobjMuestrasTela = Nothing
    End Sub
  Private Sub DataGrid1_ItemDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem Or ListItemType.Item
                If cmbTipoPagoMuestra.SelectedValue = "1" Then
                    'e.Item.Cells(4).Visible = True
                    DataGrid1.Columns(4).Visible = True
                Else
                    'e.Item.Cells(4).Visible = False
                    DataGrid1.Columns(4).Visible = False

                End If

            Case ListItemType.Footer
                Dim txtCantidad As TextBox = CType(e.Item.FindControl("txtCantidadF"), TextBox)
                Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloF"), TextBox)
                Dim btnarticulo As HtmlInputButton = CType(e.Item.FindControl("btnArticuloF"), HtmlInputButton)
                Dim txtPrecioF As TextBox = CType(e.Item.FindControl("txtPrecioF"), TextBox)

                btnarticulo.Attributes.Add("onclick", "BuscarArticulo('" & e.Item.ClientID.ToString & "','F')")
                txtCodArticulo.Attributes.Add("readonly", "readonly")
                txtCantidad.Attributes.Add("onBlur", "txtCantidad_onBlur('" & e.Item.ClientID.ToString & "','F');")
                txtCantidad.Attributes.Add("onBlur", "txtCantidad_onBlur('" & e.Item.ClientID.ToString & "','F');")
                If cmbTipoPagoMuestra.SelectedValue = "1" Then
                    'e.Item.Cells(4).Visible = True
                    DataGrid1.Columns(4).Visible = True
                    txtPrecioF.Attributes.Add("onblur", "fvalidarnumero(this)")
                Else
                    'e.Item.Cells(4).Visible = False
                    DataGrid1.Columns(4).Visible = False
                End If

            Case ListItemType.EditItem
                Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloE"), TextBox)
                Dim btnarticulo As HtmlInputButton = CType(e.Item.FindControl("btnArticuloE"), HtmlInputButton)
                Dim txtCantidad As TextBox = CType(e.Item.FindControl("txtCantidadE"), TextBox)
                Dim txtPrecioE As TextBox = CType(e.Item.FindControl("txtPrecioE"), TextBox)
                txtCodArticulo.Attributes.Add("readonly", "readonly")
                btnarticulo.Attributes.Add("onclick", "BuscarArticulo('" & e.Item.ClientID.ToString & "','E')")
                txtCantidad.Attributes.Add("onBlur", "txtCantidad_onBlur('" & e.Item.ClientID.ToString & "','E');")
                If cmbTipoPagoMuestra.SelectedValue = "1" Then
                    ' e.Item.Cells(4).Visible = True
                    DataGrid1.Columns(4).Visible = True
                    txtPrecioE.Attributes.Add("onblur", "fvalidarnumero(this)")
                Else
                    'e.Item.Cells(4).Visible = False
                    DataGrid1.Columns(4).Visible = False
                End If

            Case ListItemType.AlternatingItem, ListItemType.Item
        End Select
  End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim ldtTableDetaReqi As DataTable, ldrRow As DataRow
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas

        Dim vrFlag As Boolean = False

        Select Case e.CommandName
            Case "Add"

                vrFlag = False

                If CType(lblReg.Text, Integer) = 6 Then Exit Sub

                Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloF"), TextBox)
                Dim lblUnidad As Label = CType(e.Item.FindControl("lblUnidadMF"), Label)
                Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadF"), TextBox)
                'CAMBIO DG AGREGAR PRECIO - INI
                Dim txtPrecio As TextBox = CType(e.Item.FindControl("txtPrecioF"), TextBox)
                'CAMBIO DG AGREGAR PRECIO - FIN
                lobjMuestrasTela.NumeroSolicitud_Detalle = Me.txtNumero.Text
                lobjMuestrasTela.Codigo_Articulo = txtCodArticulo.Text
                lobjMuestrasTela.Cantidad_Solicitada = txtCantidad.Text
                'CAMBIO DG AGREGAR PRECIO - INI
                If cmbTipoPagoMuestra.SelectedValue <> "1" Then
                    lobjMuestrasTela.Precio = 0
                Else
                    lobjMuestrasTela.Precio = txtPrecio.Text
                End If

                Dim objDT As New DataTable
                lobjMuestrasTela.NumeroSolicitud = Me.txtNumero.Text
                objDT = lobjMuestrasTela.ListarSolicitudMuestraHeader(lobjMuestrasTela.NumeroSolicitud)

                Me.cmbTipoPagoMuestra.SelectedValue = objDT.Rows(0)("var_TipoPago").ToString()

                If cmbTipoPagoMuestra.SelectedValue = "0" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Debe ingresar Tipo de Pago de Muestra');</script>")
                    Exit Sub
                ElseIf cmbTipoPagoMuestra.SelectedValue = "1" Then
                    If txtPrecio.Text = 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Requiere Ingresar el precio!');</script>")
                        Exit Sub
                    End If
                End If
                'CAMBIO DG AGREGAR PRECIO - FIN

                If Me.ddlTipo.SelectedValue = "0" Then Me.ddlTipo.SelectedValue = HttpContext.Current.Session(Me.hdnIdentificador.Value + "_TIPOSOL")

                If Me.ddlTipo.SelectedValue = "1" Then
                    lobjMuestrasTela.Unidad_Medida = "MTS"
                ElseIf Me.ddlTipo.SelectedValue = "5" Then
                    lobjMuestrasTela.Unidad_Medida = "CEN"
                Else : lobjMuestrasTela.Unidad_Medida = "UNI"
                End If
                lobjMuestrasTela.Cantidad_Aprobada = 0
                lobjMuestrasTela.Estado_Detalle = "ACT"
                lobjMuestrasTela.Usuario_Detalle = Session("@USUARIO")

                If Me.txtSituacion.Text = "INI" Or Me.txtSituacion.Text = "ACT" Then

                    'CAMBIO DG AGREGAR PRECIO - INI
                    'If lobjMuestrasTela.Detalle_SolicitudMuestras_Insertar <> "" Then
                    If lobjMuestrasTela.Detalle_SolicitudMuestras_Insertar_V2 <> "" Then
                        'CAMBIO DG AGREGAR PRECIO - FIN

                        lblMsg.ForeColor = Drawing.Color.Red
                        'lblMsg.Text = "Detalle de Solicitud ingresado con exito....!"
                        vrFlag = True

                        If Me.ddlTipo.SelectedValue = "1" Then Me.ddlTipo.SelectedValue = "1"

                    End If
                    Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

                    If ddlTipo.SelectedValue = "5" Then
                        Call EnviarCorreoSolicitudEtiquetas(Me.txtNumero.Text)
                    End If

                    If vrFlag = True Then lblMsg.Text = vrCTE_MSG_INS_DET

                Else
                    lblMsg.ForeColor = Drawing.Color.Red
                    If Me.txtSituacion.Text = "SOL" Then lblMsg.Text = "No puede agregar articulos cuando la Solicitud ya fue Solicitada."
                    If Me.txtSituacion.Text = "ANU" Then lblMsg.Text = "No puede agregar articulos cuando la Solicitud ya fue Anulada."
                    If Me.txtSituacion.Text = "ATE" Then lblMsg.Text = "No puede agregar articulos cuando la Solicitud ya fue Atendida."
                End If


                Me.btnSolicitar.Visible = True
                Me.btnAnular.Visible = True
                Me.BtnNuevo.Visible = True

            Case "Update"

                vrFlag = False

                Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloE"), TextBox)
                Dim lblUnidadM As Label = CType(e.Item.FindControl("lblUnidadME"), Label)
                Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadE"), TextBox)

                Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

                Me.btnGrabar.Visible = True

            Case "Delete"

                vrFlag = False

                Dim lblSecuencia As Label = CType(e.Item.FindControl("lblNumeroSecuencia"), Label)
                lobjMuestrasTela.NumeroSolicitud_Detalle = Me.txtNumero.Text
                lobjMuestrasTela.NumeroSecuencia = lblSecuencia.Text
                lobjMuestrasTela.Usuario_Detalle = Session("@USUARIO")

                If lobjMuestrasTela.Detalle_SolicitudMuestras_Eliminar = True Then
                    lblMsg.ForeColor = Drawing.Color.Red
                    'lblMsg.Text = "El registro fue eliminado exitosamente....!"
                    vrFlag = True
                End If

                Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

                If vrFlag = True Then lblMsg.Text = vrCTE_MSG_DEL_DET

        End Select
        lobjMuestrasTela = Nothing
    End Sub

    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = e.Item.ItemIndex
        DataGrid1.ShowFooter = False

        Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

    End Sub

    Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        DataGrid1.EditItemIndex = -1
        DataGrid1.ShowFooter = True

        Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

    End Sub

    Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        DataGrid1.ShowFooter = True

        Call sCargaDetalleSolicitudMuestras(Me.txtNumero.Text)

    End Sub

    Private Sub btnSolicitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitar.Click
        Dim objAprobaciones As New OFISIS.OFISEGU.Aprobaciones(Session("@EMPRESA"), Session("@USUARIO"))
        Dim strNumero As String, ldtCorreos As DataTable
        strNumero = Me.txtNumero.Text
        If Me.hdnAprobacion.Value = "" Then
      ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('Debe de indicar un tipo de aprobación.');</script>")
        Else
            If cmbTipoPagoMuestra.SelectedValue = "1" Then 'PARA FACTURAR
                If objAprobaciones.Generar_AprobacionMuestras(Session("@EMPRESA"), "115", strNumero, Me.txtFecha.Text, "", "PRO", Me.txtFecha.Text, "K", "", Session("@USUARIO"), "", Session("@USUARIO"), "", ldtCorreos) Then
                    'ClientScript.RegisterStartupScript(Me.[GetType](),"AlertaAprobacion", "<script language=javascript>alert('La Solicitud ha sido enviada para su respectiva aprobación.');</script>")

                    lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "La Solicitud ha sido enviada para su respectiva aprobación."

                    Call EnviarCorreo(ldtCorreos)

                    Me.btnSolicitar.Visible = False
                    Me.btnGrabar.Visible = False


                    HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION") = "SOL"
                    Me.txtSituacion.Text = HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION")
                End If
            ElseIf cmbTipoPagoMuestra.SelectedValue = "2" Then 'PARA CLIENTE SIN FACTURA
                If objAprobaciones.Generar_AprobacionMuestras(Session("@EMPRESA"), "070", strNumero, Me.txtFecha.Text, "", "PRO", Me.txtFecha.Text, "K", "", Session("@USUARIO"), "", Session("@USUARIO"), "", ldtCorreos) Then
                    'ClientScript.RegisterStartupScript(Me.[GetType](),"AlertaAprobacion", "<script language=javascript>alert('La Solicitud ha sido enviada para su respectiva aprobación.');</script>")

                    lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "La Solicitud ha sido enviada para su respectiva aprobación."

                    Call EnviarCorreo(ldtCorreos)

                    Me.btnSolicitar.Visible = False
                    Me.btnGrabar.Visible = False


                    HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION") = "SOL"
                    Me.txtSituacion.Text = HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION")
                End If
            End If
            'If objAprobaciones.Generar_AprobacionMuestras(Session("@EMPRESA"), "070", strNumero, Me.txtFecha.Text, "", "PRO", Me.txtFecha.Text, "K", "", Session("@USUARIO"), "", Session("@USUARIO"), "", ldtCorreos) Then
            '    'ClientScript.RegisterStartupScript(Me.[GetType](),"AlertaAprobacion", "<script language=javascript>alert('La Solicitud ha sido enviada para su respectiva aprobación.');</script>")

            '    lblMsg.ForeColor = Drawing.Color.Red
            '    Me.lblMsg.Text = "La Solicitud ha sido enviada para su respectiva aprobación."

            '    Call EnviarCorreo(ldtCorreos)

            '    Me.btnSolicitar.Visible = False
            '    Me.btnGrabar.Visible = False


            '    HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION") = "SOL"
            '    Me.txtSituacion.Text = HttpContext.Current.Session(Me.hdnIdentificador.Value + "_SITUACION")
            'End If
    End If
    Me.btnSolicitar.Visible = False
  End Sub

  Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable)
    Dim i As Integer, lstrCuerpoMensaje As String = "", lstrTitulo As String
    Dim lstrPara As String = "", lstrCopia As String = ""
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
        lstrCopia = "" '"sistemas@nuevomundosa.com"
      Else
        lstrCopia = lstrCopia '+ ";" + "sistemas@nuevomundosa.com"
      End If
      lstrCuerpoMensaje = ""
      lstrTitulo = ""
      With pdtCorreos.Rows(0)
        'lstrTitulo = "Req. " + .Item("NumeroRequisicion") + " por aprobar."
        lstrTitulo = "SOLICITUD DE MUESTRAS POR APROBAR."
        'lstrCuerpoMensaje = "La requisición '" + _
        '                    .Item("NumeroRequisicion") + _
        '                    "' ha sido enviada al módulo de aprobaciones por " + .Item("Creador") + " y requiere de su aprobación." + vbCrLf + vbCrLf _
        '                    + "-------------------------------------------------------------------------------" + vbCrLf _
        '                    + "| Este correo ha sido generado automáticamente por el módulo de aprobaciones. |" + vbCrLf _
        '                    + "| Por favor no responder este correo.                                         |" + vbCrLf _
        '                    + "|                                                                             |" + vbCrLf _
        '                    + "| Departamento de Sistemas                                                    |" + vbCrLf _
        '                    + "| C.I.A Industrial Nuevo Mundo S.A.                                           |" + vbCrLf _
        '                    + "-------------------------------------------------------------------------------"
        lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                            "APROBACION PARA LA SIGUIENTE&nbsp;MUESTRA : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                            "<STRONG>" & .Item("NumeroSolicitud") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                            "</STRONG></FONT></FONT></P>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                            "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                            "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                            "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                            "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                            "Por favor no responder este correo.<BR>" + _
                            "Departamento de Sistemas<BR>" + _
                            "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                            "-------------------------------------------------------------------------------</P>"
        Dim mailMsg As System.Net.Mail.MailMessage
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
                    '.From = New System.Net.Mail.MailAddress("Requisiciones<aprobaciones@nuevomundosa.com>")
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


            End With
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudo enviar el correo electronico.');</script>")
        End Try
    End Sub

    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        fActDataDel()
        Me.btnSolicitar.Visible = False
    End Sub

    Protected Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged
        Dim ttipo As Integer

        ttipo = Me.ddlTipo.SelectedValue
        Select Case ttipo
            Case Is = 5
                cmbAlmacen.SelectedIndex = "001"
            Case Else
                cmbAlmacen.SelectedValue = "013"

        End Select

    End Sub

    Private Sub EnviarCorreoSolicitudEtiquetas(ByRef txtNumero As String)
        Dim i As Integer, lstrCuerpoMensaje As String = "", lstrTitulo As String
        Dim lstrPara As String = "", lstrCopia As String = ""
        Try

            'For i = 0 To pdtCorreos.Rows.Count - 1
            '    If lstrPara.Trim.Length = 0 Then
            '        lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
            '    Else
            '        lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
            '    End If
            '    lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
            'Next i

            lstrPara = "AlmacenPrincipal@nuevomundosa.com"
            'lstrPara = "mvasquez@nuevomundosa.com"
            If lstrCopia.Trim.Length = 0 Then
                lstrCopia = "RMejia@nuevomundosa.com" '"sistemas@nuevomundosa.com"
            Else
                lstrCopia = lstrCopia '+ ";" + "sistemas@nuevomundosa.com"
            End If
            lstrCuerpoMensaje = ""
            lstrTitulo = ""
            'With pdtCorreos.Rows(0)
            'lstrTitulo = "Req. " + .Item("NumeroRequisicion") + " por aprobar."
            lstrTitulo = "SOLICITUD DE ETIQUETAS"
            'lstrCuerpoMensaje = "La requisición '" + _
            '                    .Item("NumeroRequisicion") + _
            '                    "' ha sido enviada al módulo de aprobaciones por " + .Item("Creador") + " y requiere de su aprobación." + vbCrLf + vbCrLf _
            '                    + "-------------------------------------------------------------------------------" + vbCrLf _
            '                    + "| Este correo ha sido generado automáticamente por el módulo de aprobaciones. |" + vbCrLf _
            '                    + "| Por favor no responder este correo.                                         |" + vbCrLf _
            '                    + "|                                                                             |" + vbCrLf _
            '                    + "| Departamento de Sistemas                                                    |" + vbCrLf _
            '                    + "| C.I.A Industrial Nuevo Mundo S.A.                                           |" + vbCrLf _
            '                    + "-------------------------------------------------------------------------------"
            lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                                "ETIQUETAS &nbsp; : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                "<STRONG>" & txtNumero & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                "</STRONG></FONT></FONT></P>" + _
                                "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Session("@USUARIO") & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>OBSERVACIONES :&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Me.txtObservacion.Text & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                                "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                "Este correo ha sido generado automáticamente por el módulo de solicitudes.<BR>" + _
                                "Por favor no responder este correo.<BR>" + _
                                "Departamento de Sistemas<BR>" + _
                                "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                "-------------------------------------------------------------------------------</P>"
            Dim mailMsg As System.Net.Mail.MailMessage
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
                '.From = New System.Net.Mail.MailAddress("Requisiciones<solicitudes@nuevomundosa.com>")
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


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudo enviar el correo electronico.');</script>")
        End Try
    End Sub


    'Protected Sub cmbTipoPagoMuestra_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoPagoMuestra.SelectedIndexChanged
    '    DataGrid1.Columns([X].Visible = False)
    'End Sub
End Class
