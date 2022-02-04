Imports OFISIS.OFILOGI.Articulos

Public Class LOG20001
  Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

  'This call is required by the Web Form Designer.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
  Protected WithEvents HDN1 As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HDN2 As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HDNArticulo As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HDNUnidadMedida As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HDNCtaGasto As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents DataGrid2 As System.Web.UI.WebControls.DataGrid
  Protected WithEvents HdnDescServicio As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents CuentaGasto As System.Web.UI.HtmlControls.HtmlTableRow
  Protected WithEvents Stock As System.Web.UI.HtmlControls.HtmlTableRow
  Protected WithEvents hdnAprobacion As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents hdnAreaSolicitante As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents hdnCodigo As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents ddlTipo As System.Web.UI.WebControls.DropDownList
  Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtNumero As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtSituacion As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtAreaSolicitanteCodigo As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtAreaSolicitanteNombre As System.Web.UI.WebControls.TextBox
  Protected WithEvents CheckStock As System.Web.UI.WebControls.CheckBox
  Protected WithEvents txtCtaGasto As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtDescCtaGasto As System.Web.UI.WebControls.TextBox
  Protected WithEvents cmbAlmacen As System.Web.UI.WebControls.DropDownList
  Protected WithEvents txtObservacion As System.Web.UI.WebControls.TextBox
  Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
  Protected WithEvents btnSolicitar As System.Web.UI.WebControls.Button
  Protected WithEvents BtnNuevo As System.Web.UI.WebControls.Button
  Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
  Protected WithEvents btnAnular As System.Web.UI.WebControls.Button

  'NOTE: The following placeholder declaration is required by the Web Form Designer.
  'Do not delete or move it.
  Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Eventos --"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ATORRESC"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(LOG20001))
        If Not Page.IsPostBack Then
            Dim obj As OFISIS.OFILOGI.Articulos, ldtable As New DataTable, LdtDetalle As DataTable

            obj = New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
            ldtable = obj.Listar_Almacenes(Session("@EMPRESA"))
            If ldtable.Rows.Count <> 0 Then
                Me.cmbAlmacen.DataSource = ldtable
                Me.cmbAlmacen.DataValueField = ldtable.Columns("co_alma").ColumnName.ToString()
                Me.cmbAlmacen.DataTextField = ldtable.Columns("de_alma").ColumnName.ToString()
                Me.cmbAlmacen.DataBind()
            End If
            LdtDetalle = obj.EsquemaDetalle()
            Session("LOG00001_utbDetalle") = LdtDetalle
            Me.txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
            Me.CuentaGasto.Visible = False
            Me.btnSolicitar.Visible = False
            Me.btnGrabar.Visible = False
            Me.btnAnular.Visible = False
            Me.BtnNuevo.Visible = False
            Me.CheckStock.Checked = False
            Me.Stock.Visible = True
            Me.DataGrid1.EnableViewState = True
            Me.DataGrid1.Visible = True
            Session.Add("Nuevo", True)
            Me.txtSerie.Text = "0001"
            Call CargarGrilla()
            btnSolicitar.Attributes.Add("onClick", "javascript:return SolicitarAprobacion();")
            btnGrabar.Attributes.Add("onClick", "javascript:return fnc_ValidarRequisicion();")
            btnAnular.Attributes.Add("onClick", "javascript:return fnc_ConfirmarAnulacion();")
            txtAreaSolicitanteCodigo.Attributes.Add("onBlur", "BuscarCentroCosto()")
            txtObservacion.Attributes.Add("onBlur", "txtObservaciones_onBlur();")

            'readonly
            txtSerie.Attributes.Add("readonly", "readonly")
            txtFecha.Attributes.Add("readonly", "readonly")
            txtSituacion.Attributes.Add("readonly", "readonly")
            txtAreaSolicitanteNombre.Attributes.Add("readonly", "readonly")
            txtCtaGasto.Attributes.Add("readonly", "readonly")
            txtDescCtaGasto.Attributes.Add("readonly", "readonly")
        End If
        'txtAreaSolicitanteNombre.Text = Me.hdnAreaSolicitante.Value
  End Sub

  Private Sub BtnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
    Dim strOpcion As String, strCodigo As String = "", strStock As Integer
    Dim Grilla As DataGrid, ldatable As DataTable, strNumeroReqi As String
    'If Me.RdArticulo.Checked Then strOpcion = "N" Else strOpcion = "S"

    If fValidaRequisicion() = False Then Exit Sub

    If ddlTipo.SelectedValue = "ART" Then strOpcion = "N" Else strOpcion = "S"
    Select Case strOpcion
      Case "N"
        Me.CheckStock.Enabled = False
      Case "S"
        Grilla = Me.DataGrid2
        Session("Grilla") = Grilla
        Session("NombreGrilla") = "DataGrid2"
    End Select
    If Me.CheckStock.Checked = True Then strStock = 1 Else strStock = 0
    If CType(Session("Nuevo"), Boolean) Then
      ldatable = obj.Insertar_Requisicion(Session("@EMPRESA"), "001", "REQ", Me.txtSerie.Text, "", CType(Me.cmbAlmacen.SelectedValue, String), Me.txtFecha.Text, Me.txtFecha.Text, "N", "K", "", Me.txtObservacion.Text, Me.txtCtaGasto.Text, "ACT", strOpcion, "", "", "", "", "", Me.txtAreaSolicitanteCodigo.Text, strStock, Session("@USUARIO"), "", Session("@USUARIO"), "", Session("LOG00001_utbDetalle"), "")
      Session("LOG00001_utbDetalle") = Nothing
      If ldatable.Rows.Count > 0 Then strCodigo = ldatable.Rows(0)("NumeroDocumento")
      Call MuestraReqisicion(strCodigo)
      Me.btnSolicitar.Visible = True
      Me.BtnNuevo.Visible = True
      Session.Add("Nuevo", False)
    Else
      strNumeroReqi = Me.txtSerie.Text + "-" + Me.txtNumero.Text
      obj.Actualiza_Requisicion(Session("@EMPRESA"), "001", "REQ", strNumeroReqi, CType(Me.cmbAlmacen.SelectedValue, String), Me.txtFecha.Text, "", Me.txtObservacion.Text, Me.txtCtaGasto.Text, "ACT", strOpcion, "", Me.txtAreaSolicitanteCodigo.Text, strStock, Session("@USUARIO"))
      obj.Actualiza_DetalleRequi(Session("@EMPRESA"), strNumeroReqi, Session("LOG00001_utbDetalle"))
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('La requisición se guardo correctamente.')</script>")
      Session("LOG00001_utbDetalle") = Nothing
      Call MuestraReqisicion(strNumeroReqi)
      Session.Add("Nuevo", False)
    End If
    obj = Nothing
    ldatable = Nothing
  End Sub

  Private Sub btnSolicitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitar.Click
    Dim obj As New OFISIS.OFISEGU.Aprobaciones(Session("@EMPRESA"), Session("@USUARIO"))
    Dim strNumero As String, ldtCorreos As DataTable
    strNumero = Me.txtSerie.Text + "-" + Me.txtNumero.Text
    If Me.hdnAprobacion.Value = "" Then
      ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('Debe de indicar un tipo de aprobación.');</script>")
    Else
      If obj.Generar_Aprobacion(Session("@EMPRESA"), Me.hdnAprobacion.Value, strNumero, Me.txtFecha.Text, "", "PRO", Me.txtFecha.Text, "K", "", Session("@USUARIO"), "", Session("@USUARIO"), "", ldtCorreos) Then
        ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('La requisición ha sido enviada para su respectiva aprobación.');</script>")
        Call EnviarCorreo(ldtCorreos)
        Call LimpiarObjetos()
        Call MuestraReqisicion(strNumero)
      End If
    End If
  End Sub

  Private Sub BtnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNuevo.Click
    Call LimpiarObjetos()
  End Sub

  Private Sub DataGrid1_CancelCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
    DataGrid1.EditItemIndex = -1
    DataGrid1.ShowFooter = True
    Call CargarGrilla()
  End Sub

  Private Sub DataGrid1_EditCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    DataGrid1.EditItemIndex = e.Item.ItemIndex
    DataGrid1.ShowFooter = False
    Call CargarGrilla()
    Session.Add("Nuevo", False)
  End Sub

  Private Sub DataGrid1_UpdateCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
    DataGrid1.EditItemIndex = -1
    DataGrid1.ShowFooter = True
    Call CargarGrilla()
  End Sub

  Private Sub DataGrid1_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
    Dim ldtTableDetaReqi As DataTable, ldrRow As DataRow
    Dim strTipoAuxiliar, strCodigoAuxiliar, strCodigoAuxiliarDesc As String
    Dim strOrdenservicio, strOrdenservicioDes As String
    Select Case e.CommandName
      Case "Add"
        Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloF"), TextBox)
        Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadF"), TextBox)
        Dim TxtCtaGastoS As TextBox = CType(e.Item.FindControl("TxtCtaGastoSF"), TextBox)
        Dim txtCentroCostoCodigo As TextBox = CType(e.Item.FindControl("TxtCodigoCentroCostoF"), TextBox)
        Dim TxtCodOrdenServicio As TextBox = CType(e.Item.FindControl("TxtCodOrdenServicioF"), TextBox)
        If Me.CheckStock.Checked = False Then strTipoAuxiliar = "K" Else strTipoAuxiliar = ""

        ldtTableDetaReqi = Session("LOG00001_utbDetalle")
        ldrRow = ldtTableDetaReqi.NewRow
        Dim Secuencia As Int16 = 0
        If IsDBNull(ldtTableDetaReqi.Compute("Max(nu_secu)", "")) = True Then
          Secuencia = 1
        Else
          Secuencia = ldtTableDetaReqi.Compute("Max(nu_secu)", "") + 1
        End If
        ldrRow("NU_REQI") = "1"
        ldrRow("NU_SECU") = Secuencia
        ldrRow("CO_ITEM") = UCase(txtCodArticulo.Text)
        ldrRow("DE_ITEM") = UCase(HDNArticulo.Value)
        ldrRow("CO_UNME_COMP") = UCase(Me.HDNUnidadMedida.Value)
        ldrRow("co_unme") = UCase(Me.HDNUnidadMedida.Value)
        ldrRow("CA_SOLI") = txtCantidad.Text
        ldrRow("ca_soli_alma") = txtCantidad.Text
        ldrRow("CO_GRUP_SERV") = ""
        ldrRow("CO_SERV") = ""
        ldrRow("DE_SERV") = ""
        ldrRow("CO_DEST_FINA") = TxtCtaGastoS.Text
        ldrRow("DE_UNID_DEST") = UCase(HDNCtaGasto.Value)
        ldrRow("Ti_auxi_empr") = UCase(strTipoAuxiliar)
        ldrRow("no_auxi") = UCase(HDN1.Value)
        ldrRow("co_auxi_empr") = UCase(txtCentroCostoCodigo.Text)
        ldrRow("Co_orde_serv") = UCase(TxtCodOrdenServicio.Text)
        ldrRow("DE_ACTI") = UCase(HDN2.Value)
        ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
        ldrRow("TI_SITU") = "ACT"
        ldtTableDetaReqi.Rows.Add(ldrRow)
        ldrRow = Nothing
        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
        ldtTableDetaReqi = Nothing
        CargarGrilla()
        Me.btnGrabar.Visible = True
      Case "Update"
        Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloE"), TextBox)
        Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadE"), TextBox)
        Dim TxtCtaGastoS As TextBox = CType(e.Item.FindControl("TxtCtaGastoSE"), TextBox)
        Dim txtCentroCostoCodigo As TextBox = CType(e.Item.FindControl("TxtCodigoCentroCostoE"), TextBox)
        Dim TxtCodOrdenServicio As TextBox = CType(e.Item.FindControl("TxtCodOrdenServicioE"), TextBox)
        If Me.CheckStock.Checked = False Then strTipoAuxiliar = "K" Else strTipoAuxiliar = ""
        ldtTableDetaReqi = Session("LOG00001_utbDetalle")
        ldrRow = ldtTableDetaReqi.Rows(e.Item.ItemIndex)
        ldrRow.BeginEdit()
        ldrRow("co_item") = UCase(txtCodArticulo.Text)
        ldrRow("DE_ITEM") = UCase(HDNArticulo.Value)
        ldrRow("CO_UNME_COMP") = UCase(Me.HDNUnidadMedida.Value)
        ldrRow("co_unme") = UCase(Me.HDNUnidadMedida.Value)
        ldrRow("CA_SOLI") = txtCantidad.Text
        ldrRow("ca_soli_alma") = txtCantidad.Text
        ldrRow("CO_GRUP_SERV") = ""
        ldrRow("CO_SERV") = ""
        ldrRow("DE_SERV") = ""
        ldrRow("CO_DEST_FINA") = TxtCtaGastoS.Text
        ldrRow("DE_UNID_DEST") = UCase(HDNCtaGasto.Value)
        ldrRow("Ti_auxi_empr") = UCase(strTipoAuxiliar)
        ldrRow("no_auxi") = UCase(HDN1.Value)
        ldrRow("co_auxi_empr") = UCase(txtCentroCostoCodigo.Text)
        ldrRow("Co_orde_serv") = TxtCodOrdenServicio.Text
        ldrRow("DE_ACTI") = UCase(HDN2.Value)
        ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
        ldrRow("TI_SITU") = "ACT"
        ldrRow.AcceptChanges()
        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
        ldtTableDetaReqi = Nothing
        CargarGrilla()
        Me.btnGrabar.Visible = True
      Case "Delete"
        ldtTableDetaReqi = Session("LOG00001_utbDetalle")
        ldtTableDetaReqi.Rows(e.Item.ItemIndex).Delete()
        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
        ldtTableDetaReqi = Nothing
        CargarGrilla()
    End Select
  End Sub

  Private Sub DataGrid1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    Select Case e.Item.ItemType
      Case ListItemType.Footer
        Dim btnarticulo As HtmlInputButton = CType(e.Item.FindControl("btnArticuloF"), HtmlInputButton)
        Dim btnCtaGastoF As HtmlInputButton = CType(e.Item.FindControl("btnCtaGastoF"), HtmlInputButton)
        Dim btnCentroCostoF As HtmlInputButton = CType(e.Item.FindControl("btnCentroCostoF"), HtmlInputButton)
        Dim BtnOrdenServicioF As HtmlInputButton = CType(e.Item.FindControl("BtnOrdenServicioF"), HtmlInputButton)
        Dim btnAdd As ImageButton = CType(e.Item.FindControl("btnAdd"), ImageButton)
        Dim txtCantidad As TextBox = CType(e.Item.FindControl("txtCantidadF"), TextBox)

        Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloF"), TextBox)

        txtCantidad.Attributes.Add("onBlur", "txtCantidad_onBlur('" & e.Item.ClientID.ToString & "','F');")
        btnarticulo.Attributes.Add("onclick", "BuscarArticulo('" & e.Item.ClientID.ToString & "','F')")
        btnCtaGastoF.Attributes.Add("onclick", "BuscarCtaGasto('" & e.Item.ClientID.ToString & "','F')")
        btnCentroCostoF.Attributes.Add("onclick", "ListarCentroCosto('" & e.Item.ClientID.ToString & "','F')")
        BtnOrdenServicioF.Attributes.Add("onclick", "BuscarOrdenServicio('" & e.Item.ClientID.ToString & "','F')")
        btnAdd.Attributes.Add("onclick", "javascript:return fnc_Datagrid1_Validar('" & e.Item.ClientID.ToString & "','F')")

        txtCodArticulo.Attributes.Add("readonly", "readonly")

      Case ListItemType.EditItem
        Dim btnarticulo As HtmlInputButton = CType(e.Item.FindControl("btnArticuloE"), HtmlInputButton)
        Dim btnCtaGastoE As HtmlInputButton = CType(e.Item.FindControl("btnCtaGastoE"), HtmlInputButton)
        Dim btnCentroCostoE As HtmlInputButton = CType(e.Item.FindControl("btnCentroCostoE"), HtmlInputButton)
        Dim BtnOrdenServicioE As HtmlInputButton = CType(e.Item.FindControl("BtnOrdenServicioE"), HtmlInputButton)
        Dim btnUpdate As ImageButton = CType(e.Item.FindControl("btnUpdate"), ImageButton)
        Dim txtCantidad As TextBox = CType(e.Item.FindControl("txtCantidadE"), TextBox)
        Dim txtCodArticulo As TextBox = CType(e.Item.FindControl("txtCodArticuloE"), TextBox)

        txtCantidad.Attributes.Add("onBlur", "txtCantidad_onBlur('" & e.Item.ClientID.ToString & "','E');")
        btnarticulo.Attributes.Add("onclick", "BuscarArticulo('" & e.Item.ClientID.ToString & "','E')")
        btnCtaGastoE.Attributes.Add("onclick", "BuscarCtaGasto('" & e.Item.ClientID.ToString & "','E')")
        btnCentroCostoE.Attributes.Add("onclick", "ListarCentroCosto('" & e.Item.ClientID.ToString & "','E')")
        BtnOrdenServicioE.Attributes.Add("onclick", "BuscarOrdenServicio('" & e.Item.ClientID.ToString & "','E')")
        btnUpdate.Attributes.Add("onclick", "javascript:return fnc_Datagrid1_Validar('" & e.Item.ClientID.ToString & "','E')")

        txtCodArticulo.Attributes.Add("readonly", "readonly")

      Case ListItemType.AlternatingItem, ListItemType.Item
        CType(e.Item.FindControl("lbtAdjuntosArticulo"), LinkButton).Text = "" + e.Item.DataItem("co_item").ToString + " - (0)" 'aqui se debe escribir los adjuntados

        '20120905 EPM Se comenta debido a que no se usa y el formulario de adjunto no se migra 2010.
        'CType(e.Item.FindControl("lbtAdjuntosArticulo"), LinkButton).Attributes.Add("onClick", "javascript:return fnc_AdjuntarDocs('" + hdnCodigo.Value + "','" + e.Item.DataItem("co_item").ToString + "')")
    End Select
  End Sub

  Private Sub DataGrid2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.ItemCommand
    Dim ldtTableDetaReqi As DataTable, ldrRow As DataRow
    ldtTableDetaReqi = Session("LOG00001_utbDetalle")
    Select Case e.CommandName
      Case "Add"
        Dim LblSecuenciaS As Label = CType(e.Item.FindControl("LblSecuenciaSF"), Label)
        Dim txtCodGrupo As TextBox = CType(e.Item.FindControl("TxtGrupoServF"), TextBox)
        Dim txtTipoServicio As TextBox = CType(e.Item.FindControl("TxtTipoServF"), TextBox)
        Dim txtDescServicio As TextBox = CType(e.Item.FindControl("TxtDesServicioF"), TextBox)
        Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadSF"), TextBox)
        Dim TxtCodigoCentroCosto As TextBox = CType(e.Item.FindControl("TxtCodigoCentroCosto_S_F"), TextBox)
        Dim TxtCodOrdenServicio As TextBox = CType(e.Item.FindControl("TxtCodOrdenServicio_S_F"), TextBox)
        If txtCodGrupo.Text.Trim.Length = 0 Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('Debe de indicar un grupo de servicio.');</Script>")
          Exit Sub
        ElseIf txtTipoServicio.Text.Trim.Length = 0 Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('Debe de indicar un tipo de servicio.');</Script>")
          Exit Sub
        End If
        ldrRow = ldtTableDetaReqi.NewRow
        Dim Secuencia As Int16 = 0
        If IsDBNull(ldtTableDetaReqi.Compute("Max(nu_secu)", "")) = True Then
          Secuencia = 1
        Else
          Secuencia = ldtTableDetaReqi.Compute("Max(nu_secu)", "") + 1
        End If
        ldrRow("NU_REQI") = "1"
        ldrRow("NU_SECU") = Secuencia
        ldrRow("CO_ITEM") = ""
        ldrRow("DE_ITEM") = UCase(txtDescServicio.Text)
        ldrRow("CO_UNME_COMP") = ""
        ldrRow("co_unme") = ""
        ldrRow("CA_SOLI") = txtCantidad.Text
        ldrRow("ca_soli_alma") = txtCantidad.Text
        ldrRow("CO_GRUP_SERV") = txtCodGrupo.Text
        ldrRow("CO_SERV") = txtTipoServicio.Text
        ldrRow("DE_SERV") = UCase(HdnDescServicio.Value)
        ldrRow("CO_DEST_FINA") = Me.txtCtaGasto.Text
        ldrRow("DE_UNID_DEST") = UCase(HDNCtaGasto.Value)
        ldrRow("Ti_auxi_empr") = "K"
        ldrRow("co_auxi_empr") = TxtCodigoCentroCosto.Text
        ldrRow("no_auxi") = UCase(HDN1.Value)
        ldrRow("Co_orde_serv") = TxtCodOrdenServicio.Text
        ldrRow("DE_ACTI") = UCase(HDN2.Value)
        ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
        ldrRow("TI_SITU") = "ACT"
        ldtTableDetaReqi.Rows.Add(ldrRow)
        ldrRow = Nothing
        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
        CargarGrilla()
        Me.btnGrabar.Visible = True
      Case "Editar"
        DataGrid2.EditItemIndex = e.Item.ItemIndex
        DataGrid2.ShowFooter = False
        CargarGrilla()
      Case "Cancel"
        DataGrid2.EditItemIndex = -1
        DataGrid2.ShowFooter = True
        CargarGrilla()
      Case "Grabar"
        Dim LblSecuenciaS As Label = CType(e.Item.FindControl("LblSecuenciaSE"), Label)
        Dim txtCodGrupo As TextBox = CType(e.Item.FindControl("TxtGrupoServE"), TextBox)
        Dim txtTipoServicio As TextBox = CType(e.Item.FindControl("TxtTipoServE"), TextBox)
        Dim txtDescServicio As TextBox = CType(e.Item.FindControl("TxtDesServicioE"), TextBox)
        Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadSE"), TextBox)
        Dim TxtCodigoCentroCosto As TextBox = CType(e.Item.FindControl("TxtCodigoCentroCosto_S_E"), TextBox)
        Dim TxtCodOrdenServicio As TextBox = CType(e.Item.FindControl("TxtCodOrdenServicio_S_E"), TextBox)
        If txtCodGrupo.Text.Trim.Length = 0 Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('Debe de indicar un grupo de servicio.');</Script>")
          Exit Sub
        ElseIf txtTipoServicio.Text.Trim.Length = 0 Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('Debe de indicar un tipo de servicio.');</Script>")
          Exit Sub
        End If
        ldrRow = ldtTableDetaReqi.Rows(e.Item.ItemIndex)
        ldrRow.BeginEdit()
        ldrRow("CO_ITEM") = ""
        ldrRow("DE_ITEM") = UCase(txtDescServicio.Text)
        ldrRow("CO_UNME_COMP") = ""
        ldrRow("co_unme") = ""
        ldrRow("CA_SOLI") = txtCantidad.Text
        ldrRow("ca_soli_alma") = txtCantidad.Text
        ldrRow("CO_GRUP_SERV") = txtCodGrupo.Text
        ldrRow("CO_SERV") = txtTipoServicio.Text
        ldrRow("DE_SERV") = UCase(HdnDescServicio.Value)
        ldrRow("CO_DEST_FINA") = Me.txtCtaGasto.Text
        ldrRow("DE_UNID_DEST") = UCase(HDNCtaGasto.Value)
        ldrRow("Ti_auxi_empr") = "K"
        ldrRow("co_auxi_empr") = TxtCodigoCentroCosto.Text
        ldrRow("no_auxi") = UCase(HDN1.Value)
        ldrRow("Co_orde_serv") = TxtCodOrdenServicio.Text
        ldrRow("DE_ACTI") = UCase(HDN2.Value)
        ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
        ldrRow("TI_SITU") = "ACT"
        ldrRow.AcceptChanges()
        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
        DataGrid2.EditItemIndex = -1
        DataGrid2.ShowFooter = True
        CargarGrilla()
        Me.btnGrabar.Visible = True
      Case "Eliminar"
        ldtTableDetaReqi = Session("LOG00001_utbDetalle")
        ldtTableDetaReqi.Rows(e.Item.ItemIndex).Delete()
        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
        ldtTableDetaReqi = Nothing
        CargarGrilla()
    End Select
    ldtTableDetaReqi = Nothing
  End Sub

  Private Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
    Select Case e.Item.ItemType
      Case ListItemType.Footer
        Dim BtnGrupoSerF As HtmlInputButton = CType(e.Item.FindControl("BtnGrupoSerF"), HtmlInputButton)
        Dim BtnCentroCostoSF As HtmlInputButton = CType(e.Item.FindControl("BtnCentroCostoSF"), HtmlInputButton)
        Dim BtnOrdenServF As HtmlInputButton = CType(e.Item.FindControl("BtnOrdenServF"), HtmlInputButton)
        Dim btnAdicionaS As ImageButton = CType(e.Item.FindControl("btnAdicionaS"), ImageButton)
        Dim txtCantidadS As TextBox = CType(e.Item.FindControl("txtCantidadSF"), TextBox)
        Dim txtDesServicioF As TextBox = CType(e.Item.FindControl("txtDesServicioF"), TextBox)

        txtDesServicioF.Attributes.Add("onBlur", "txtDesServicio_onBlur('" & e.Item.ClientID.ToString & "','F');")
        txtCantidadS.Attributes.Add("onBlur", "txtCantidadS_onBlur('" & e.Item.ClientID.ToString & "','F');")
        BtnGrupoSerF.Attributes.Add("Onclick", "BuscarServicio('" & e.Item.ClientID.ToString & "')")
        BtnOrdenServF.Attributes.Add("onclick", "BuscarOrdenServicio('" & e.Item.ClientID.ToString & "','F')")
        BtnCentroCostoSF.Attributes.Add("onclick", "ListarCentroCosto('" & e.Item.ClientID.ToString & "','F')")
        btnAdicionaS.Attributes.Add("onclick", "javascript:return fnc_Datagrid2_Validar('" & e.Item.ClientID.ToString & "','F')")
      Case ListItemType.EditItem
        Dim BtnGrupoSerE As HtmlInputButton = CType(e.Item.FindControl("BtnGrupoSerE"), HtmlInputButton)
        Dim BtnOrdenServE As HtmlInputButton = CType(e.Item.FindControl("BtnOrdenServE"), HtmlInputButton)
        Dim BtnCentroCostoSE As HtmlInputButton = CType(e.Item.FindControl("BtnCentroCostoSE"), HtmlInputButton)
        Dim btnGuardarS As ImageButton = CType(e.Item.FindControl("btnGuardarS"), ImageButton)
        Dim txtCantidadSE As TextBox = CType(e.Item.FindControl("txtCantidadSE"), TextBox)
        Dim txtDesServicioE As TextBox = CType(e.Item.FindControl("txtDesServicioE"), TextBox)

        txtDesServicioE.Attributes.Add("onBlur", "txtDesServicio_onBlur('" & e.Item.ClientID.ToString & "','E');")
        btnGuardarS.Attributes.Add("onclick", "javascript:return fnc_Datagrid2_Validar('" & e.Item.ClientID.ToString & "','E')")
        txtCantidadSE.Attributes.Add("onBlur", "txtCantidadS_onBlur('" & e.Item.ClientID.ToString & "','E');")
        BtnGrupoSerE.Attributes.Add("Onclick", "BuscarServicio('" & e.Item.ClientID.ToString & "')")
        BtnOrdenServE.Attributes.Add("onclick", "BuscarOrdenServicio('" & e.Item.ClientID.ToString & "','E')")
        BtnCentroCostoSE.Attributes.Add("onclick", "ListarCentroCosto('" & e.Item.ClientID.ToString & "','E')")
    End Select
  End Sub

  Private Sub ddlTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
    Call LimpiarObjetos()
  End Sub

  Private Sub txtNumero_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumero.TextChanged
    Me.btnGrabar.Visible = True
    Me.btnSolicitar.Visible = True
    Me.BtnNuevo.Visible = True
    Call MuestraReqisicion(txtSerie.Text + "-" + Right("0000000000" + txtNumero.Text, 10))
    Session.Add("Nuevo", False)
  End Sub

  Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
    Call prc_AnularRequisicion()
  End Sub

#End Region

#Region "-- Metodos --"

  Function MuestraReqisicion(ByVal strcodigo As String)
    Dim ldtablas As DataSet, ldtable As DataTable
    Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
    ldtablas = obj.Listar_reqi_Detalle(strcodigo, Session("@EMPRESA"))
    '----------------Cabera de la Requisicion---------------------------------------
    If ldtablas.Tables(0).Rows.Count <> 0 Then
      Me.txtSerie.Text = Mid(ldtablas.Tables(0).Rows(0)("NU_REQI"), 1, 4)
      Me.txtNumero.Text = Mid(ldtablas.Tables(0).Rows(0)("NU_REQI"), 6, 10)
      Me.txtFecha.Text = ldtablas.Tables(0).Rows(0)("FE_EMIS_REQI")
      Me.txtCtaGasto.Text = ldtablas.Tables(0).Rows(0)("DE_OBSE_0002")
      Me.txtDescCtaGasto.Text = ldtablas.Tables(0).Rows(0)("DE_UNID_DEST")
      Me.txtObservacion.Text = ldtablas.Tables(0).Rows(0)("DE_OBSE_0001")
      Me.txtAreaSolicitanteCodigo.Text = ldtablas.Tables(0).Rows(0)("CO_AREA_SOLI")
      Me.txtAreaSolicitanteNombre.Text = ldtablas.Tables(0).Rows(0)("NombreArea")
      Me.cmbAlmacen.SelectedValue = ldtablas.Tables(0).Rows(0)("CO_ALMA")
      If ldtablas.Tables(0).Rows(0)("ST_STOC") = 0 Then Me.CheckStock.Checked = False Else Me.CheckStock.Checked = True
      Select Case ldtablas.Tables(0).Rows(0)("TI_SITU")
        Case "ACT"
          Me.txtSituacion.Text = "ACTIVO"
          Me.btnGrabar.Visible = True
          Me.btnAnular.Visible = True
          Me.btnSolicitar.Visible = True
          DataGrid1.Columns(6).Visible = True
          DataGrid2.Columns(6).Visible = True
        Case "ANU"
          Me.txtSituacion.Text = "ANULADO"
          DataGrid1.Columns(6).Visible = False
          DataGrid2.Columns(6).Visible = False
          Me.btnGrabar.Visible = False
          Me.btnSolicitar.Visible = False
          Me.btnAnular.Visible = False
          Me.BtnNuevo.Visible = True
        Case "ENV"
          Me.txtSituacion.Text = "ENVIADO"
          DataGrid1.Columns(6).Visible = False
          DataGrid2.Columns(6).Visible = False
          Me.btnGrabar.Visible = False
          Me.btnSolicitar.Visible = False
          Me.btnAnular.Visible = False
          Me.BtnNuevo.Visible = True
        Case "APR"
          Me.txtSituacion.Text = "APROBADO"
          DataGrid1.Columns(6).Visible = False
          DataGrid2.Columns(6).Visible = False
          Me.btnGrabar.Visible = False
          Me.btnSolicitar.Visible = False
          Me.btnAnular.Visible = False
          Me.BtnNuevo.Visible = True
      End Select

      '---------------Detalle de la Requisicion-------------------------------------------
      ldtable = obj.EsquemaDetalle
      Dim ldrRow As DataRow
      For Each dtrDatos As DataRow In ldtablas.Tables(1).Rows
        ldrRow = ldtable.NewRow()
        ldrRow("NU_REQI") = strcodigo
        ldrRow("NU_SECU") = dtrDatos("nu_secu")
        ldrRow("CO_ITEM") = dtrDatos("co_item")
        ldrRow("DE_ITEM") = UCase(dtrDatos("de_item"))
        ldrRow("CO_UNME_COMP") = dtrDatos("co_unme")
        ldrRow("co_unme") = dtrDatos("co_unme")
        ldrRow("CA_SOLI") = dtrDatos("ca_soli")
        ldrRow("ca_soli_alma") = dtrDatos("ca_soli")
        ldrRow("CO_GRUP_SERV") = dtrDatos("CO_GRUP_SERV")
        ldrRow("CO_SERV") = dtrDatos("CO_SERV")
        ldrRow("DE_SERV") = UCase(dtrDatos("DE_SERV"))
        ldrRow("CO_DEST_FINA") = dtrDatos("CO_DEST_FINA")
        ldrRow("DE_UNID_DEST") = UCase(dtrDatos("DE_UNID_DEST"))
        ldrRow("Ti_auxi_empr") = "K"
        ldrRow("no_auxi") = UCase(dtrDatos("no_auxi"))
        ldrRow("co_auxi_empr") = dtrDatos("CO_AUXI_EMPR")
        ldrRow("Co_orde_serv") = dtrDatos("CO_ORDE_SERV")
        ldrRow("DE_ACTI") = UCase(dtrDatos("DE_ACTI"))
        ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
        ldrRow("TI_SITU") = "ACT"
        ldtable.Rows.Add(ldrRow)
      Next
      ldrRow = Nothing
      Session("LOG00001_utbDetalle") = ldtable
      Call CargarGrilla()
    Else
      Me.txtNumero.Text = ""
      Me.btnSolicitar.Visible = False
      Me.btnGrabar.Visible = False
      Me.btnAnular.Visible = False
      Me.BtnNuevo.Visible = True
      ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('El numero de requisición ingresado no esta registrado...!');</script>")
      Call LimpiarObjetos()
    End If
    obj = Nothing
    ldtablas = Nothing
    ldtable = Nothing
  End Function

  Function CargarGrilla()
    Dim dtbDatos As DataTable = CType(Session("LOG00001_utbDetalle"), DataTable)
    'If RdArticulo.Checked Then
    If ddlTipo.SelectedValue = "ART" Then
      DataGrid1.Visible = True
      DataGrid2.Visible = False
      DataGrid1.DataSource = dtbDatos
      DataGrid1.DataBind()
    Else
      DataGrid2.Visible = True
      DataGrid1.Visible = False
      DataGrid2.DataSource = dtbDatos
      DataGrid2.DataBind()
    End If
  End Function

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
        lstrTitulo = "Nueva requisición por aprobar."

        '20120904 EPM Se actualiza para solicite el AppSetting del FW 4.0
        lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
        "APROBACION PARA LA SIGUIENTE&nbsp;REQUISICION : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
        "<STRONG>" & .Item("NumeroRequisicion") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
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
        'lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
        '"APROBACION PARA LA SIGUIENTE&nbsp;REQUISICION : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
        '"<STRONG>" & .Item("NumeroRequisicion") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
        '"</STRONG></FONT></FONT></P>" + _
        '"<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
        '"<A title='http://" + ConfigurationSettings.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + ConfigurationSettings.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
        '"ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
        '"<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
        '"Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
        '"Por favor no responder este correo.<BR>" + _
        '"Departamento de Sistemas<BR>" + _
        '"Cía. Industrial Nuevo Mundo S.A.<BR>" + _
        '"-------------------------------------------------------------------------------</P>"

        Dim mailMsg As System.Net.Mail.MailMessage
        mailMsg = New System.Net.Mail.MailMessage()

        '20121005 EPM Configurar arreglo para el To
        Dim lstrTo_arreglo() As String = lstrPara.Split(";")
        For lintIndice = 0 To lstrTo_arreglo.Length - 1
          If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
        Next
        'Si no hay destinatario que lo envie a sistemas
        If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
        '20121005 EPM Configurar arreglo para el CC
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
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
    End Try
  End Sub

  Function LimpiarObjetos()
    Me.txtAreaSolicitanteCodigo.Text = ""
    Me.txtAreaSolicitanteNombre.Text = ""
    Me.hdnAreaSolicitante.Value = ""
    Me.txtObservacion.Text = ""
    Me.txtNumero.Text = ""
    Me.txtSituacion.Text = ""
    'If Me.RdArticulo.Checked = True Then
    If ddlTipo.SelectedValue = "ART" Then
      Me.txtSerie.Text = "0001"
      Me.CuentaGasto.Visible = False
      Me.CheckStock.Checked = False
      Me.Stock.Visible = True
      'Me.RdArticulo.Checked = True
      ddlTipo.SelectedValue = "ART"
      Me.DataGrid1.Visible = True
      Me.DataGrid2.Visible = False
    Else
      Me.txtSerie.Text = "0002"
      Me.CuentaGasto.Visible = True
      Me.Stock.Visible = False
      'Me.RdServicio.Checked = True
      ddlTipo.SelectedValue = "SER"
      Me.DataGrid1.Visible = False
      Me.DataGrid2.Visible = True
    End If
    DataGrid1.Columns(6).Visible = True
    DataGrid2.Columns(6).Visible = True
    Me.BtnNuevo.Visible = True
    Me.btnGrabar.Visible = False
    Me.btnAnular.Visible = False
    Me.btnSolicitar.Visible = False

    Dim LdtDetalle As DataTable, obj As OFISIS.OFILOGI.Articulos
    obj = New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
    LdtDetalle = obj.EsquemaDetalle()
    Session("LOG00001_utbDetalle") = LdtDetalle
    Call CargarGrilla()
    Session.Add("Nuevo", True)
    obj = Nothing
    LdtDetalle = Nothing
  End Function

  Private Sub prc_AnularRequisicion()
    Dim lobjRequisicion As New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
    Dim ldtbResultado As DataTable, lstrNumero As String
    lstrNumero = txtSerie.Text + "-" + txtNumero.Text
    ldtbResultado = lobjRequisicion.Anular_Requisicion(Session("@EMPRESA"), lstrNumero, Session("@USUARIO"))
    If Not ldtbResultado Is Nothing Then
      If (ldtbResultado.Rows(0).Item("error").ToString.Length > 0) Then
        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + ldtbResultado.Rows(0).Item("error").ToString + "');</script>")
        Exit Sub
      End If
    End If
    ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('La requisición se anulo correctamente.');</script>")
    Call LimpiarObjetos()
    Call MuestraReqisicion(lstrNumero)
    lobjRequisicion = Nothing
    ldtbResultado = Nothing
  End Sub

  'Valida que los CTC ingresados cumplan las condiciones
  Private Function fValidaRequisicion() As Boolean
    Dim lobjRequisicion As New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
    Dim lstrError As String = "", ldtbRetorno As DataTable
    Dim bRpta As Boolean

    Try

      ldtbRetorno = lobjRequisicion.Lista_ValidacionCTC(Session("LOG00001_utbDetalle"))

      bRpta = True

      If ldtbRetorno.Rows.Count > 0 Then

        For Each ldrRow As DataRow In ldtbRetorno.Rows
          If ldrRow("chr_flgCerrado") = "X" Then
            bRpta = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('El numero de orden de servicio " & ldrRow("vch_numero") & " ya se encuentra cerrado');</script>")
            Exit For
          End If

          If ldrRow("chr_flgCerrado") = "S" And ldrRow("vch_estado") = "APR" Then
            bRpta = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('El numero de orden de servicio " & ldrRow("vch_numero") & " se encuentra solicitado para su cierre');</script>")
            Exit For
          End If

          If ldrRow("vch_estado") <> "APR" Then
            bRpta = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('El numero de orden de servicio " & ldrRow("vch_numero") & " no se encuentra aprobado');</script>")
            Exit For
          End If

        Next

      End If

      fValidaRequisicion = bRpta

    Catch ex As Exception

      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede realizar las validaciones.');</script>")

    Finally

      ldtbRetorno = Nothing
      lobjRequisicion = Nothing

    End Try

  End Function

  <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
  Public Function fnc_ValidarCodigos(ByVal pstrAreasolicitante As String, ByVal pstrCuentagasto As String, ByVal pstrGruposervicio As String, ByVal pstrTiposervicio As String, ByVal pstrCentrocosto As String, ByVal pstrOrdenservicio As String, ByVal pstrArticulo As String) As DataTable
    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
      Response.Redirect("/intranet/finsesion.htm")
    End If
    '-----------------------------------------------------------------------
    '--FINAL: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    Dim lobjRequisicion As New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
    Dim lstrError As String = "", ldtbRetorno As DataTable
    ldtbRetorno = lobjRequisicion.fnc_ValidarCodigos(pstrAreasolicitante, pstrCuentagasto, pstrGruposervicio, pstrTiposervicio, pstrCentrocosto, pstrOrdenservicio, pstrArticulo)
    lobjRequisicion = Nothing
    Return ldtbRetorno
  End Function

#End Region

End Class
