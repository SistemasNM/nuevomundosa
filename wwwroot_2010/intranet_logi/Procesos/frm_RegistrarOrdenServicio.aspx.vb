Public Class frm_RegistrarOrdenServicio
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
    Protected WithEvents hdnAprobacion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAreaSolicitante As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodigo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoReq As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSituacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAreaSolicitanteCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAreaSolicitanteNombre As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCtaGasto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescCtaGasto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlmacen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnSolicitar As System.Web.UI.WebControls.Button
    Protected WithEvents BtnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnular As System.Web.UI.WebControls.Button


    Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Eventos --"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '--INICIO: VERIFICAR LA SESION
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "LALANOCA"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(frm_RegistrarOrdenServicio))
        If Not Page.IsPostBack Then
            Dim obj As OFISIS.OFILOGI.Articulos
            Dim ldtable As New DataTable
            Dim LdtDetalle As DataTable

            Try
                obj = New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
                ldtable = obj.Listar_Almacenes(Session("@EMPRESA"))
                txtAlmacen.Text = ldtable.Rows(0).Item("co_alma").ToString() + "-" + ldtable.Rows(0).Item("de_alma").ToString()

                LdtDetalle = obj.EsquemaDetalle()
                Session("LOG00001_utbDetalle") = LdtDetalle
                txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
                txtFecIns.Text = Date.Now.ToString("dd/MM/yyyy")
                'CuentaGasto.Visible = True
                btnSolicitar.Visible = False
                btnSolicitarPRE.Visible = False
                btnGrabar.Visible = False
                btnAnular.Visible = False
                BtnNuevo.Visible = False
                btnListaAdjuntos.Visible = False
                Session.Add("Nuevo", True)
                Me.txtSerie.Text = "0002"

                Call CargarGrilla()
                Call CargarMotivosRequisicion()
                Call CargaConfiguracionJavascript()

            Catch ex As Exception
                lblMensaje.Text = ex.Message
            Finally
                obj = Nothing
                ldtable = Nothing
                LdtDetalle = Nothing
            End Try
            
        End If

    End Sub
    Private Sub CargaConfiguracionJavascript()
        btnSolicitar.Attributes.Add("onClick", "javascript:return SolicitarAprobacion();")
        btnSolicitarPRE.Attributes.Add("onClick", "javascript:return SolicitarAprobacion();")
        btnGrabar.Attributes.Add("onClick", "javascript:return fnc_ValidarRequisicion();")
        btnAnular.Attributes.Add("onClick", "javascript:return fnc_ConfirmarAnulacion();")
        btnListaAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos()")

        txtAreaSolicitanteCodigo.Attributes.Add("onBlur", "BuscarJefaturaSolicitante()")
        txtObservacion.Attributes.Add("onBlur", "txtObservaciones_onBlur();")

        txtSerie.Attributes.Add("readonly", "readonly")
        txtFecha.Attributes.Add("readonly", "readonly")
        txtFecIns.Attributes.Add("readonly", "readonly")
        txtSituacion.Attributes.Add("readonly", "readonly")
        txtAreaSolicitanteNombre.Attributes.Add("readonly", "readonly")
        'txtCtaGasto.Attributes.Add("readonly", "readonly") 'COMENTADO PARA PRUEBAS
        'txtDescCtaGasto.Attributes.Add("readonly", "readonly")
    End Sub
    Private Sub CargarMotivosRequisicion()
        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        Dim ldtable As New DataTable

        Try
            ldtable = obj.Listar_Opciones_Modulos("REQUISICIONES", "MOTIVO")

            ddlMotivoRequisicion.DataValueField = "VCH_CODIGO"
            ddlMotivoRequisicion.DataTextField = "VCH_DESCRIPCION"
            ddlMotivoRequisicion.DataSource = ldtable
            ddlMotivoRequisicion.Items.Add("SELECCIONAR MOTIVO")
            ddlMotivoRequisicion.DataBind()
            ddlMotivoRequisicion.Items.Insert(0, New ListItem("SELECCIONAR MOTIVO", "0"))

        Catch ex As Exception
            Throw ex
        Finally
            obj = Nothing
            ldtable = Nothing
        End Try

    End Sub
    Private Sub BtnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        Dim strOpcion As String
        Dim strCodigo As String = ""
        Dim Grilla As DataGrid
        Dim ldatable As DataTable
        Dim strNumeroReqi As String
        Dim strCodigoMotivo As String
        Dim strPresupuesto As String
        Dim TipoServicio As String
        Try
            If fValidaRequisicion() = False Then Exit Sub
            If txtTipoReq.Text = "SERVICIO" Then strOpcion = "S" Else strOpcion = "N"
            Select Case strOpcion
                Case "S"
                    Grilla = Me.DataGrid2
                    Session("Grilla") = Grilla
                    Session("NombreGrilla") = "DataGrid2"
            End Select

            'If Me.HDNCtaGasto.Value = "" Then
            '    If Me.DataGrid2.Items.Count > 0 Then
            '        Me.HDNCtaGasto.Value = Me.DataGrid2.Items(0).Cells("DE_OBSE").Text
            '    End If
            'End If

            'CAMBIO DG: SE INSERTARA TIPO DE SERVICIO EXTERNO O INTERNO
            If Me.rdTipoInterna.Checked = True Then
                TipoServicio = "I"
            Else
                TipoServicio = "E"
            End If
            'CAMBIO DG: SE INSERTARA TIPO DE SERVICIO EXTERNO O INTERNO

            'INICIO: CAMBIOS LUIS_AJ (20180112)
            If rdPresupuestado_SI.Checked = True Then
                strPresupuesto = "S"
            Else
                strPresupuesto = "N"
            End If
            strCodigoMotivo = ddlMotivoRequisicion.SelectedValue
            'FIN: CAMBIOS LUIS_AJ (20180112)

            If CType(Session("Nuevo"), Boolean) Then
                'ldatable = obj.Insertar_Requisicion(Session("@EMPRESA"), "001", "REQ", Me.txtSerie.Text, "", Mid(txtAlmacen.Text, 1, 3), Me.txtFecha.Text, Me.txtFecIns.Text, "N", "K", "", Me.txtObservacion.Text, Me.txtCtaGasto.Text, "ACT", strOpcion, "", "", "", "", "", Me.txtAreaSolicitanteCodigo.Text, 0, Session("@USUARIO"), "", Session("@USUARIO"), "", Session("LOG00001_utbDetalle"), "")
                ldatable = obj.Insertar_Requisicion(Session("@EMPRESA"), "001", "REQ", Me.txtSerie.Text, "", Mid(txtAlmacen.Text, 1, 3),
                                                    Me.txtFecha.Text, Me.txtFecIns.Text, "N", "K", "", Me.txtObservacion.Text, Me.HDNCtaGasto.Value,
                                                    "ACT", strOpcion, "", "", "", "", "", Me.txtAreaSolicitanteCodigo.Text, 0, Session("@USUARIO"), "",
                                                    Session("@USUARIO"), "", Session("LOG00001_utbDetalle"), "", TipoServicio, strPresupuesto, strCodigoMotivo)
                Session("LOG00001_utbDetalle") = Nothing
                If ldatable.Rows.Count > 0 Then strCodigo = ldatable.Rows(0)("NumeroDocumento")
                Call MuestraReqisicion(strCodigo)
                'btnSolicitar.Visible = True
                'btnSolicitarPRE.Visible = True
                'BtnNuevo.Visible = True
                Session.Add("Nuevo", False)
            Else
                strNumeroReqi = Me.txtSerie.Text + "-" + Me.txtNumero.Text
                'obj.Actualiza_Requisicion(Session("@EMPRESA"), "001", "REQ", strNumeroReqi, Mid(txtAlmacen.Text, 1, 3), Me.txtFecha.Text, "", Me.txtObservacion.Text, Me.txtCtaGasto.Text, "ACT", strOpcion, "", Me.txtAreaSolicitanteCodigo.Text, 0, Session("@USUARIO"))
                obj.Actualiza_Requisicion(Session("@EMPRESA"), "001", "REQ", strNumeroReqi, Mid(txtAlmacen.Text, 1, 3),
                                          Me.txtFecha.Text, "", Me.txtObservacion.Text, Me.HDNCtaGasto.Value,
                                          "ACT", strOpcion, hdnOrdServicio.Value, Me.txtAreaSolicitanteCodigo.Text, 0,
                                          Session("@USUARIO"), TipoServicio, strPresupuesto, strCodigoMotivo, Me.txtFecIns.Text)
                obj.Actualiza_DetalleRequi(Session("@EMPRESA"), strNumeroReqi, Session("LOG00001_utbDetalle"))
                ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('\n\r La requisición de servicio se guardo correctamente.')</script>")
                Session("LOG00001_utbDetalle") = Nothing
                Call MuestraReqisicion(strNumeroReqi)
                Session.Add("Nuevo", False)
            End If
            obj = Nothing
            ldatable = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('\n\r Error al registrar la requisición de servicio.');</script>")
        End Try
 
    End Sub

    Private Sub btnSolicitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitar.Click
        Dim obj As New OFISIS.OFISEGU.Aprobaciones(Session("@EMPRESA"), Session("@USUARIO"))
        Dim strNumero As String
        Dim ldtCorreos As New DataTable

        Try
            If Len(Trim(txtNumero.Text)) > 0 Then
                strNumero = Trim(txtSerie.Text) + "-" + Trim(txtNumero.Text)
                If hdnAprobacion.Value = "" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('\n\r Debe de indicar un tipo de aprobación.');</script>")
                Else
                    If obj.Generar_Aprobacion(Session("@EMPRESA"), hdnAprobacion.Value, strNumero, txtFecha.Text, "", "PRO", txtFecha.Text, "K", "", Session("@USUARIO"), "", Session("@USUARIO"), "", ldtCorreos) Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('La requisición ha sido enviada para su respectiva aprobación.');</script>")
                        Call EnviarCorreo(ldtCorreos)
                        Call EnviarCorreoComitePresupuesto(Session("@EMPRESA"), strNumero) ' LUIS_AJ (20180115)
                        Call LimpiarObjetos()
                        Call MuestraReqisicion(strNumero)
                    End If
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('Error No fue posible enviar la requisición para su respectiva aprobación.');</script>")
        End Try
                
    End Sub

    Private Sub EnviarCorreoComitePresupuesto(ByVal strEmpresa As String, ByVal strNumeroRequisicion As String)
        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        Dim intResult As Integer

        Try
            intResult = obj.GenerarCorreo_ComitePresupuesto(strEmpresa, strNumeroRequisicion)

        Catch ex As Exception
            lblMensaje.Text = ex.Message
        Finally
            obj = Nothing
        End Try


    End Sub

    Protected Sub btnSolicitarPRE_Click(sender As Object, e As EventArgs) Handles btnSolicitarPRE.Click
        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        'Dim obj As New OFISIS.OFISEGU.Aprobaciones(Session("@EMPRESA"), Session("@USUARIO"))
        Dim strNumero As String
        Dim ldtCorreos As New DataTable

        Try
            If Len(Trim(txtNumero.Text)) > 0 Then
                strNumero = Trim(txtSerie.Text) + "-" + Trim(txtNumero.Text)
                If hdnAprobacion.Value = "" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('\n\r Debe de indicar un tipo de aprobación.');</script>")
                Else
                    'If hdnAprobacion.Value = txtAreaSolicitanteCodigo.Text And hdnAprobacion.Value <> "049" Then ' Validacion diferentes aprobaciones, Excepto sistemas - LUIS_AJ (20180113)
                    '    ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('\n\r El tipo de aprobación no puede ser igual a la jefatura solicitante.');</script>")
                    'Else
                    If obj.Generar_PreAprobacion(Session("@EMPRESA"), hdnAprobacion.Value, strNumero, txtFecha.Text, "", "PRO", txtFecha.Text, txtAreaSolicitanteCodigo.Text, "K", "", Session("@USUARIO"), Session("@USUARIO"), ldtCorreos) Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaAprobacion", "<script language=javascript>alert('La requisición ha sido enviada para su respectiva aprobación.');</script>")
                        Call EnviarCorreo(ldtCorreos)
                        Call EnviarCorreoComitePresupuesto(Session("@EMPRESA"), strNumero)
                        Call LimpiarObjetos()
                        Call MuestraReqisicion(strNumero)
                    End If
                    'End If
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('Error No fue posible enviar la requisición para su respectiva aprobación. " & ex.Message & "');</script>")
        End Try
    End Sub

    Private Sub BtnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNuevo.Click
        Call LimpiarObjetos()
    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Private Sub DataGrid2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.ItemCommand
        Dim ldtTableDetaReqi As DataTable
        Dim ldrRow As DataRow
        ldtTableDetaReqi = Session("LOG00001_utbDetalle")
        Try
            Select Case e.CommandName
                Case "Add"
                    If txtSituacion.Text = "ACTIVO" Or txtSituacion.Text.Trim = "" Then
                        Dim LblSecuenciaS As Label = CType(e.Item.FindControl("LblSecuenciaSF"), Label)
                        Dim txtCodGrupo As TextBox = CType(e.Item.FindControl("TxtGrupoServF"), TextBox)
                        Dim txtTipoServicio As TextBox = CType(e.Item.FindControl("TxtTipoServF"), TextBox)
                        Dim txtDescServicio As TextBox = CType(e.Item.FindControl("TxtDesServicioF"), TextBox)
                        Dim txtCantidad As TextBox = CType(e.Item.FindControl("TxtCantidadSF"), TextBox)
                        Dim TxtCodigoCentroCosto As TextBox = CType(e.Item.FindControl("TxtCodigoCentroCosto_S_F"), TextBox)
                        Dim txtCodigoCuentaGastos As TextBox = CType(e.Item.FindControl("txtCodigoCuentaGastos_S_F"), TextBox)
                        Dim TxtCodOrdenServicio As TextBox = CType(e.Item.FindControl("TxtCodOrdenServicio_S_F"), TextBox)

                        If txtCodigoCuentaGastos.Text <> "" And HDNCtaGasto.Value = "" Then
                            HDNCtaGasto.Value = txtCodigoCuentaGastos.Text
                        End If
                        If txtCodGrupo.Text.Trim.Length = 0 Then
                            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar un grupo de servicio.');</Script>")
                            txtCodGrupo.Focus()
                            Exit Sub
                        ElseIf txtTipoServicio.Text.Trim.Length = 0 Then
                            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar un tipo de servicio.');</Script>")
                            txtTipoServicio.Focus()
                            Exit Sub
                        ElseIf TxtCodigoCentroCosto.Text.Trim.Length = 0 Then
                            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar un centro de Costo.');</Script>")
                            TxtCodigoCentroCosto.Focus()
                            Exit Sub
                        ElseIf TxtCodOrdenServicio.Text.Trim.Length = 0 And txtCodigoCuentaGastos.Text.Trim.Length = 0 Then
                            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar una Cuenta de Gastos.');</Script>")
                            txtCodigoCuentaGastos.Focus()
                            Exit Sub
                        ElseIf txtCantidad.Text.Trim.Length = 0 Then
                            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar una Cantidad');</Script>")
                            txtCantidad.Focus()
                            Exit Sub
                        ElseIf CDbl(txtCantidad.Text.Trim) <= 0 Then
                            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar una Cantidad mayor a Cero');</Script>")
                            txtCantidad.Focus()
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
                        ldrRow("CO_DEST_FINA") = txtCodigoCuentaGastos.Text 'Me.txtCtaGasto.Text
                        ldrRow("DE_UNID_DEST") = UCase(HDNDesCtaGasto.Value)
                        ldrRow("Ti_auxi_empr") = "K"
                        ldrRow("co_auxi_empr") = TxtCodigoCentroCosto.Text
                        ldrRow("no_auxi") = UCase(HDN1.Value)
                        ldrRow("DE_OBSE") = txtCodigoCuentaGastos.Text 'UCase(HDNCtaGasto.Value)
                        ldrRow("Co_orde_serv") = TxtCodOrdenServicio.Text
                        ldrRow("DE_ACTI") = UCase(HDN2.Value)
                        ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
                        ldrRow("TI_SITU") = "ACT"
                        ldrRow("Adjuntos") = 0
                        ldtTableDetaReqi.Rows.Add(ldrRow)
                        ldrRow = Nothing
                        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
                        CargarGrilla()
                        Me.btnGrabar.Visible = True
                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('\n\r No se puede Modificar la requisición porque se encuentra en situacion de " & txtSituacion.Text & ".');</script>")
                    End If
                Case "Editar"
                    If txtSituacion.Text = "ACTIVO" Or txtSituacion.Text.Trim = "" Then
                        DataGrid2.EditItemIndex = e.Item.ItemIndex
                        DataGrid2.ShowFooter = False
                        CargarGrilla()
                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('\n\r No se puede Modificar la requisición porque se encuentra en situacion de " & txtSituacion.Text & ".');</script>")
                    End If

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
                    Dim txtCodigoCuentaGastos As TextBox = CType(e.Item.FindControl("txtCodigoCuentaGastos_S_E"), TextBox)
                    Dim TxtCodOrdenServicio As TextBox = CType(e.Item.FindControl("TxtCodOrdenServicio_S_E"), TextBox)

                    If txtCodigoCuentaGastos.Text <> "" And HDNCtaGasto.Value = "" Then
                        HDNCtaGasto.Value = txtCodigoCuentaGastos.Text
                    End If
                    If txtCodGrupo.Text.Trim.Length = 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar un grupo de servicio.');</Script>")
                        txtCodGrupo.Focus()
                        Exit Sub
                    ElseIf txtTipoServicio.Text.Trim.Length = 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar un tipo de servicio.');</Script>")
                        txtTipoServicio.Focus()
                        Exit Sub
                    ElseIf TxtCodigoCentroCosto.Text.Trim.Length = 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar un centro de Costo.');</Script>")
                        TxtCodigoCentroCosto.Focus()
                        Exit Sub
                    ElseIf TxtCodOrdenServicio.Text.Trim.Length = 0 And txtCodigoCuentaGastos.Text.Trim.Length = 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar una Cuenta de Gastos.');</Script>")
                        txtCodigoCuentaGastos.Focus()
                        Exit Sub
                    ElseIf txtCantidad.Text.Trim.Length = 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar una Cantidad');</Script>")
                        txtCantidad.Focus()
                        Exit Sub
                    ElseIf CDbl(txtCantidad.Text.Trim) <= 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<Script language=javascript>alert('\n\r Debe de indicar una Cantidad mayor a Cero');</Script>")
                        txtCantidad.Focus()
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
                    ldrRow("CO_DEST_FINA") = txtCodigoCuentaGastos.Text 'Me.txtCtaGasto.Text
                    ldrRow("DE_UNID_DEST") = UCase(HDNDesCtaGasto.Value)
                    ldrRow("Ti_auxi_empr") = "K"
                    ldrRow("co_auxi_empr") = TxtCodigoCentroCosto.Text
                    ldrRow("no_auxi") = UCase(HDN1.Value)
                    ldrRow("DE_OBSE") = txtCodigoCuentaGastos.Text 'UCase(HDNCtaGasto.Value)
                    ldrRow("Co_orde_serv") = TxtCodOrdenServicio.Text
                    ldrRow("DE_ACTI") = UCase(HDN2.Value)
                    ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
                    ldrRow("TI_SITU") = "ACT"
                    ldrRow("Adjuntos") = 0
                    ldrRow.AcceptChanges()
                    Session("LOG00001_utbDetalle") = ldtTableDetaReqi
                    DataGrid2.EditItemIndex = -1
                    DataGrid2.ShowFooter = True
                    CargarGrilla()
                    Me.btnGrabar.Visible = True




                Case "Eliminar"
                    If txtSituacion.Text = "ACTIVO" Or txtSituacion.Text.Trim = "" Then
                        ldtTableDetaReqi = Session("LOG00001_utbDetalle")
                        ldtTableDetaReqi.Rows(e.Item.ItemIndex).Delete()
                        Session("LOG00001_utbDetalle") = ldtTableDetaReqi
                        ldtTableDetaReqi = Nothing
                        CargarGrilla()
                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('\n\r No se puede Modificar la requisición porque se encuentra en situacion de " & txtSituacion.Text & ".');</script>")
                    End If
            End Select
            ldtTableDetaReqi = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('\n\r Ha ocurrido un error al realizar la operacion.');</script>")
        End Try

    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Private Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim strSecuencia As String = ""
        Select Case e.Item.ItemType
            Case ListItemType.Footer
                Dim BtnGrupoSerF As HtmlInputButton = CType(e.Item.FindControl("BtnGrupoSerF"), HtmlInputButton)
                Dim BtnCentroCostoSF As HtmlInputButton = CType(e.Item.FindControl("BtnCentroCostoSF"), HtmlInputButton)
                Dim BtnCuentaGastosSF As HtmlInputButton = CType(e.Item.FindControl("BtnCuentaGastosSF"), HtmlInputButton)
                Dim BtnOrdenServF As HtmlInputButton = CType(e.Item.FindControl("BtnOrdenServF"), HtmlInputButton)
                Dim btnAdicionaS As ImageButton = CType(e.Item.FindControl("btnAdicionaS"), ImageButton)
                Dim txtCantidadS As TextBox = CType(e.Item.FindControl("txtCantidadSF"), TextBox)
                Dim txtDesServicioF As TextBox = CType(e.Item.FindControl("txtDesServicioF"), TextBox)
                Dim txtCodigoCentroCostoF As TextBox = CType(e.Item.FindControl("txtCodigoCentroCosto_S_F"), TextBox)

                txtDesServicioF.Attributes.Add("onBlur", "txtDesServicio_onBlur('" & e.Item.ClientID.ToString & "','F');")
                btnAdicionaS.Attributes.Add("onclick", "javascript:return fnc_Datagrid2_Validar('" & e.Item.ClientID.ToString & "','F')")
                txtCantidadS.Attributes.Add("onBlur", "txtCantidadS_onBlur('" & e.Item.ClientID.ToString & "','F');")
                BtnGrupoSerF.Attributes.Add("Onclick", "BuscarServicio('" & e.Item.ClientID.ToString & "')")
                BtnCentroCostoSF.Attributes.Add("onclick", "fnc_BuscarDatos('" & e.Item.ClientID.ToString & "','F','CentrodeCostos','')")
                BtnCuentaGastosSF.Attributes.Add("onclick", "fnc_BuscarDatos('" & e.Item.ClientID.ToString & "','F','CuentaGastos','S')")
                BtnOrdenServF.Attributes.Add("onclick", "BuscarOrdenServicio('" & e.Item.ClientID.ToString & "','F')")
                txtCodigoCentroCostoF.Attributes.Add("onBlur", "txtCodigoCentroCosto_onBlur('" & e.Item.ClientID.ToString & "','F');")

            Case ListItemType.EditItem
                Dim BtnGrupoSerE As HtmlInputButton = CType(e.Item.FindControl("BtnGrupoSerE"), HtmlInputButton)
                Dim BtnOrdenServE As HtmlInputButton = CType(e.Item.FindControl("BtnOrdenServE"), HtmlInputButton)
                Dim BtnCentroCostoSE As HtmlInputButton = CType(e.Item.FindControl("BtnCentroCostoSE"), HtmlInputButton)
                Dim BtnCuentaGastosSE As HtmlInputButton = CType(e.Item.FindControl("BtnCuentaGastosSE"), HtmlInputButton)
                Dim btnGuardarS As ImageButton = CType(e.Item.FindControl("btnGuardarS"), ImageButton)
                Dim txtCantidadSE As TextBox = CType(e.Item.FindControl("txtCantidadSE"), TextBox)
                Dim txtDesServicioE As TextBox = CType(e.Item.FindControl("txtDesServicioE"), TextBox)
                Dim txtCodigoCentroCostoE As TextBox = CType(e.Item.FindControl("txtCodigoCentroCosto_S_E"), TextBox)

                txtDesServicioE.Attributes.Add("onBlur", "txtDesServicio_onBlur('" & e.Item.ClientID.ToString & "','E');")
                btnGuardarS.Attributes.Add("onclick", "javascript:return fnc_Datagrid2_Validar('" & e.Item.ClientID.ToString & "','E')")
                txtCantidadSE.Attributes.Add("onBlur", "txtCantidadS_onBlur('" & e.Item.ClientID.ToString & "','E');")
                BtnGrupoSerE.Attributes.Add("onclick", "BuscarServicio('" & e.Item.ClientID.ToString & "')")
                BtnCentroCostoSE.Attributes.Add("onclick", "fnc_BuscarDatos('" & e.Item.ClientID.ToString & "','E','CentrodeCostos','')")
                BtnCuentaGastosSE.Attributes.Add("onclick", "fnc_BuscarDatos('" & e.Item.ClientID.ToString & "','E','CuentaGastos','S')")
                BtnOrdenServE.Attributes.Add("onclick", "BuscarOrdenServicio('" & e.Item.ClientID.ToString & "','E')")
                txtCodigoCentroCostoE.Attributes.Add("onBlur", "txtCodigoCentroCosto_onBlur('" & e.Item.ClientID.ToString & "','E');")

            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim btnAdjuntar As ImageButton = CType(e.Item.FindControl("btnAjuntarS"), ImageButton)
                If Len(Trim(txtNumero.Text)) = 0 Then
                    btnAdjuntar.Visible = False
                Else
                    Dim objlblSecuencia As Label = CType(e.Item.FindControl("LblSecuenciaS"), Label)
                    strSecuencia = objlblSecuencia.Text
                    btnAdjuntar.Visible = True
                    btnAdjuntar.Attributes.Add("onclick", "javascript:return fnc_AdjuntarDocs('" & strSecuencia & "')")
                End If
        End Select
    End Sub

    Private Sub txtNumero_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumero.TextChanged
        'btnGrabar.Visible = True
        'btnSolicitar.Visible = True
        'btnSolicitarPRE.Visible = True
        'BtnNuevo.Visible = True
        'btnListaAdjuntos.Visible = True
        Call MuestraReqisicion(txtSerie.Text + "-" + Right("0000000000" + txtNumero.Text, 10))
        Session.Add("Nuevo", False)
    End Sub

    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Call prc_AnularRequisicion()
    End Sub

#End Region

#Region "-- Metodos --"

    Private Sub MuestraReqisicion(ByVal strcodigo As String)
        Dim bSin_CTC As Boolean
        Dim ldtablas As DataSet
        Dim ldtable As DataTable

        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        Try
            ldtablas = obj.Listar_reqi_Detalle(strcodigo, Session("@EMPRESA"))

            ' Cabera de la Requisicion
            If ldtablas.Tables(0).Rows.Count <> 0 Then
                txtSerie.Text = Mid(ldtablas.Tables(0).Rows(0)("NU_REQI"), 1, 4)
                txtNumero.Text = Mid(ldtablas.Tables(0).Rows(0)("NU_REQI"), 6, 10)
                txtFecha.Text = ldtablas.Tables(0).Rows(0)("FE_EMIS_REQI")
                txtFecIns.Text = ldtablas.Tables(0).Rows(0)("FE_TOPE_REQI")
                hdnCCDestino.Value = ldtablas.Tables(0).Rows(0)("CO_AREA_SOLI")
                HDNCtaGasto.Value = ldtablas.Tables(0).Rows(0)("DE_OBSE_0002")
                HDNDesCtaGasto.Value = ldtablas.Tables(0).Rows(0)("DE_UNID_DEST")
                'txtCtaGasto.Text = ldtablas.Tables(0).Rows(0)("DE_OBSE_0002")
                'txtDescCtaGasto.Text = ldtablas.Tables(0).Rows(0)("DE_UNID_DEST")
                txtObservacion.Text = ldtablas.Tables(0).Rows(0)("DE_OBSE_0001")
                txtAreaSolicitanteCodigo.Text = ldtablas.Tables(0).Rows(0)("CO_JEFATURA_SOLI")
                txtAreaSolicitanteNombre.Text = ldtablas.Tables(0).Rows(0)("NombreJefatura")
                txtAlmacen.Text = ldtablas.Tables(0).Rows(0)("CO_ALMA")
                lblMensaje.Text = ldtablas.Tables(0).Rows(0)("MENSAJE")
                lblMotivoRechazo.Text = ldtablas.Tables(0).Rows(0)("DE_OBSE_0003")
                hdnOrdServicio.Value = ldtablas.Tables(0).Rows(0)("CO_ORDE_SERV")

                Select Case ldtablas.Tables(0).Rows(0)("TIP_SERV")
                    Case "I"
                        rdTipoInterna.Checked = True
                        rdTipoExterna.Checked = False
                    Case "E"
                        rdTipoInterna.Checked = False
                        rdTipoExterna.Checked = True
                    Case Else
                        rdTipoInterna.Checked = False
                        rdTipoExterna.Checked = False
                End Select

                Select Case ldtablas.Tables(0).Rows(0)("CHR_PRESUPUESTO")
                    Case "S"
                        rdPresupuestado_SI.Checked = True
                        rdPresupuestado_NO.Checked = False
                    Case "N"
                        rdPresupuestado_SI.Checked = False
                        rdPresupuestado_NO.Checked = True
                    Case Else
                        rdPresupuestado_SI.Checked = False
                        rdPresupuestado_NO.Checked = False
                End Select

                ddlMotivoRequisicion.SelectedValue = ldtablas.Tables(0).Rows(0)("CHR_COD_MOTIVO").ToString.Trim

                If hdnOrdServicio.Value = "" Then
                    bSin_CTC = True
                Else
                    bSin_CTC = IIf(hdnOrdServicio.Value.Substring(0, 1) <> "9", True, False)
                End If


                Select Case ldtablas.Tables(0).Rows(0)("TI_SITU")
                    Case "ACT"
                        txtSituacion.Text = "ACTIVO"
                        btnGrabar.Visible = True
                        btnAnular.Visible = True
                        BtnNuevo.Visible = True
                        btnSolicitar.Visible = Not bSin_CTC
                        btnSolicitarPRE.Visible = bSin_CTC
                        btnListaAdjuntos.Visible = False
                        DataGrid2.Columns(7).Visible = True
                    Case "ANU"
                        txtSituacion.Text = "ANULADO"
                        DataGrid2.Columns(7).Visible = False
                        btnGrabar.Visible = False
                        btnSolicitar.Visible = False
                        btnSolicitarPRE.Visible = False
                        btnAnular.Visible = False
                        BtnNuevo.Visible = True
                        btnListaAdjuntos.Visible = False
                    Case "ENV"
                        txtSituacion.Text = "ENVIADO"
                        DataGrid2.Columns(7).Visible = False
                        btnGrabar.Visible = False
                        btnSolicitar.Visible = False
                        btnSolicitarPRE.Visible = False
                        btnAnular.Visible = False
                        BtnNuevo.Visible = True
                        btnListaAdjuntos.Visible = True
                    Case "APR"
                        txtSituacion.Text = "APROBADO"
                        DataGrid2.Columns(7).Visible = False
                        btnGrabar.Visible = False
                        btnSolicitar.Visible = False
                        btnSolicitarPRE.Visible = False
                        btnAnular.Visible = False
                        BtnNuevo.Visible = True
                        btnListaAdjuntos.Visible = True
                End Select

                ' Detalle de la Requisicion
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
                    ldrRow("DE_OBSE") = dtrDatos("DE_OBSE")
                    ldrRow("Co_orde_serv") = dtrDatos("CO_ORDE_SERV")
                    ldrRow("DE_ACTI") = UCase(dtrDatos("DE_ACTI"))
                    ldrRow("CO_USUA_CREA") = UCase(Session("@USUARIO"))
                    ldrRow("TI_SITU") = "ACT"
                    ldrRow("Adjuntos") = dtrDatos("Adjuntos")
                    ldtable.Rows.Add(ldrRow)
                Next
                ldrRow = Nothing
                Session("LOG00001_utbDetalle") = ldtable
                Call CargarGrilla()
            Else
                txtNumero.Text = ""
                'btnSolicitar.Visible = False
                btnGrabar.Visible = False
                btnAnular.Visible = False
                BtnNuevo.Visible = True
                btnSolicitar.Visible = False
                btnSolicitarPRE.Visible = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('\n\r El numero de requisición ingresado no esta registrado...!');</script>")
                Call LimpiarObjetos()
            End If
            obj = Nothing
            ldtablas = Nothing
            ldtable = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('\n\r No se puede mostrar datos de la requisicion');</script>")
        End Try

    End Sub


    Private Sub CargarGrilla()
        Dim dtbDatos As DataTable = CType(Session("LOG00001_utbDetalle"), DataTable)
        If txtTipoReq.Text = "SERVICIO" Then
            DataGrid2.Visible = True
            DataGrid2.DataSource = dtbDatos
            DataGrid2.DataBind()
        End If
    End Sub

    Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable)

        Dim i As Integer
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""
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
            lstrCuerpoMensaje = ""
            lstrTitulo = ""
            With pdtCorreos.Rows(0)
                lstrTitulo = "Nueva requisición por aprobar."
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

                Dim mailMsg As System.Net.Mail.MailMessage
                mailMsg = New System.Net.Mail.MailMessage()

                'Comentado para pruebas
                '---------------------------
                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
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
                    '.To.Add("lalanoca@nuevomundosa.com")
                    '.CC.Add("dccorahua@nuevomundosa.com")
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

    Private Sub LimpiarObjetos()
        txtAreaSolicitanteCodigo.Text = ""
        txtAreaSolicitanteNombre.Text = ""
        hdnAreaSolicitante.Value = ""
        HDNCtaGasto.Value = ""
        HDNDesCtaGasto.Value = ""
        txtObservacion.Text = ""
        txtNumero.Text = ""
        txtSituacion.Text = ""
        'txtCtaGasto.Text = ""
        If txtTipoReq.Text = "SERVICIO" Then
            txtSerie.Text = "0002"
            'CuentaGasto.Visible = True
            DataGrid2.Visible = True
        End If

        txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
        txtFecIns.Text = Date.Now.ToString("dd/MM/yyyy")
        ddlMotivoRequisicion.SelectedIndex = -1
        rdPresupuestado_SI.Checked = False
        rdPresupuestado_NO.Checked = False
        rdTipoExterna.Checked = False
        rdTipoInterna.Checked = False


        DataGrid2.Columns(6).Visible = True
        BtnNuevo.Visible = True
        btnGrabar.Visible = False
        btnAnular.Visible = False
        btnSolicitar.Visible = False
        btnSolicitarPRE.Visible = False
        btnListaAdjuntos.Visible = False

        Dim LdtDetalle As DataTable, obj As OFISIS.OFILOGI.Articulos
        obj = New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        LdtDetalle = obj.EsquemaDetalle()
        Session("LOG00001_utbDetalle") = LdtDetalle
        Call CargarGrilla()
        Session.Add("Nuevo", True)
        obj = Nothing
        LdtDetalle = Nothing
    End Sub

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

    Private Function fValidaRequisicion() As Boolean
        Dim lobjRequisicion As New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        Dim lstrError As String = ""
        Dim ldtbRetorno As DataTable
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
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede realizar las validaciones.');</script>")
        Finally
            ldtbRetorno = Nothing
            lobjRequisicion = Nothing
        End Try
        Return bRpta
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function fnc_ValidarCodigos(ByVal pstrAreasolicitante As String, ByVal pstrCuentagasto As String, ByVal pstrGruposervicio As String, ByVal pstrTiposervicio As String, ByVal pstrCentrocosto As String, ByVal pstrOrdenservicio As String, ByVal pstrArticulo As String) As DataTable
        '--INICIO: VERIFICAR LA SESION
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("../../intranet/finsesion.htm")
        End If
        Dim lobjRequisicion As New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        Dim lstrError As String = "", ldtbRetorno As DataTable
        ldtbRetorno = lobjRequisicion.fnc_ValidarCodigos(pstrAreasolicitante, pstrCuentagasto, pstrGruposervicio, pstrTiposervicio, pstrCentrocosto, pstrOrdenservicio, pstrArticulo)
        lobjRequisicion = Nothing
        Return ldtbRetorno
    End Function

#End Region



End Class