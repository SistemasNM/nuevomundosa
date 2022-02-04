Imports System.IO
Imports NuevoMundo

Public Class SIS200031
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlmacen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUnidad As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCentroCosto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComprador As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecCrea As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaTope As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaExpi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmision As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents tblCentroCosto As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tabpage21 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents tabpage31 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtBase As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFOB As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFlete As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSeguro As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDscto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotal As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIGV As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSubTotal As System.Web.UI.WebControls.TextBox
    Protected WithEvents Table8 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtDocumento2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlmacen2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservaciones2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents tabpage10 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Div1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents dtgAdjuntos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDetalleRequisicion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgDetalleOrden As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnProveedor1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnProveedor2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnProveedor3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnTipo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnObservaciones As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnImprimir As System.Web.UI.WebControls.Button
    Protected WithEvents btnImprimir2 As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSeguimiento As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnEnviarOC As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
#End Region

#Region "-- Variables --"

    Private mstrTipo As String = ""
    Protected WithEvents hdnproveedoremail As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtocmproveedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtocmmoneda As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnocmproveedorcorreo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtestconsumopromedio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txteststockactual As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgestconsumo As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgestcompras As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnldatosestadisticos As System.Web.UI.WebControls.Panel
    Protected WithEvents hdnarticuloestadistica As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnposicionpanel As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnAnalisis As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnalisisSemanal As System.Web.UI.WebControls.Button
    Protected WithEvents btnAdjuntarArchivo As System.Web.UI.WebControls.Button
    Protected WithEvents hdnCodigoDoc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDestinoAbrir As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDestinoGuardar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnActualizar As System.Web.UI.WebControls.Button


    Private mstrObservaciones As String = ""

#End Region

#Region "-- Eventos --"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        'Session("@USUARIO") = "Darwin"
        'Session("@EMPRESA") = "01"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"

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
        Dim strdocumento As String
        Ajax.Utility.RegisterTypeForAjax(GetType(SIS200031))
        Response.Cache.SetNoStore()
        Dim lstr_requisicion As String, lstr_ordencompra As String
        lstr_requisicion = Request("pstrrequisicion")
        'lstr_requisicion = ""
        lstr_ordencompra = Request("pstrordencompra")
        'lstr_ordencompra = "0001-0000091871"
        If Not IsPostBack Then
            Call prc_ObtenerRutaDestino()
            btnEnviarOC.Attributes.Add("onClick", "javascript:return btnEnviarOC_onClick();")
            Session("DETALLE_REQUISICION") = Nothing

            If Strings.Left(lstr_requisicion, 4) = "0002" Or Strings.Left(lstr_ordencompra, 4) = "0002" Then
                Me.hdnTipo.Value = "servicio"
            Else
                Me.hdnTipo.Value = "compra"
            End If
            Call BuscarRequisicion(lstr_requisicion, lstr_ordencompra)
            'Me.tabpage10.Visible = False

            

            If txtDocumento.Text.Trim <> "" Then
                strdocumento = "1" + txtDocumento.Text
            Else
                strdocumento = "2" + txtDocumento2.Text
            End If
            Me.tabpage10.Visible = True
            Call Buscar(strdocumento) 'lista seguimiento de aprobación de requerimiento
            'Else
            '  Me.tabpage10.Visible = False

            '20120609 EPM Readonly
            txtDocumento.Attributes.Add("readonly", "readonly")
            txtEmision.Attributes.Add("readonly", "readonly")
            txtFechaTope.Attributes.Add("readonly", "readonly")
            txtComprador.Attributes.Add("readonly", "readonly")
            txtCentroCosto.Attributes.Add("readonly", "readonly")
            txtUnidad.Attributes.Add("readonly", "readonly")
            txtAlmacen.Attributes.Add("readonly", "readonly")
            txtObservaciones.Attributes.Add("readonly", "readonly")
            txtDocumento2.Attributes.Add("readonly", "readonly")
            txtAlmacen2.Attributes.Add("readonly", "readonly")
            txtObservaciones2.Attributes.Add("readonly", "readonly")
            txtocmproveedor.Attributes.Add("readonly", "readonly")
            txtocmmoneda.Attributes.Add("readonly", "readonly")
            txteststockactual.Attributes.Add("readonly", "readonly")
            txtestconsumopromedio.Attributes.Add("readonly", "readonly")

            Me.txtBase.Attributes.Add("readonly", "readonly")
            Me.txtFlete.Attributes.Add("readonly", "readonly")
            Me.txtFOB.Attributes.Add("readonly", "readonly")
            Me.txtSeguro.Attributes.Add("readonly", "readonly")
            Me.txtDscto.Attributes.Add("readonly", "readonly")

            txtSubTotal.Attributes.Add("readonly", "readonly")
            txtIGV.Attributes.Add("readonly", "readonly")
            txtTotal.Attributes.Add("readonly", "readonly")

            'btnAdjuntarArchivo.Attributes.Add("Onclick", "javascript:fnc_RegistraDocsAdjuntos();document.getElementById('btnActualizar').click();")
            btnAdjuntarArchivo.Attributes.Add("Onclick", "javascript:return fnc_RegistraDocsAdjuntos();")

        End If

        If hdnarticuloestadistica.Value.Trim.Length = 0 Then
            'pnldatosestadisticos.Visible = False
            'ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.getElementById('pnldatosestadisticos').style.display='none';</script>")
        End If
        'If hdnTipo.Value = "compra" Then
        '    Call BindGridAdjuntoLista_OrdenCompra()
        'End If
    End Sub

    'Private Sub dtgAdjuntos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAdjuntos.ItemCommand
    '    Dim ldtRes As DataTable
    '    Dim ldrRow As DataRow
    '    Select Case e.CommandName
    '        Case "Editar"
    '            Me.hdnProveedor1.Value = ""
    '            Me.hdnProveedor2.Value = ""
    '            Me.hdnProveedor3.Value = ""
    '            dtgAdjuntos.EditItemIndex = e.Item.ItemIndex
    '            BindGridAdjunto()
    '        Case "Cancelar"
    '            Me.hdnProveedor1.Value = ""
    '            Me.hdnProveedor2.Value = ""
    '            Me.hdnProveedor3.Value = ""
    '            dtgAdjuntos.EditItemIndex = -1
    '            BindGridAdjunto()
    '        Case "Actualizar"
    '            ldtRes = CType(Session("DETALLE_ADJUNTO"), DataTable)
    '            ldrRow = ldtRes.Rows(e.Item.ItemIndex)
    '            ldrRow.BeginEdit()
    '            ldrRow("var_ProveedorCodigo1") = CType(e.Item.FindControl("txtProveedorCodigo1E"), TextBox).Text.Trim
    '            ldrRow("var_ProveedorNombre1") = Me.hdnProveedor1.Value.Trim
    '            ldrRow("var_Alternativa1") = IIf(ldrRow("var_ProveedorNombre1") <> "", ldrRow("var_ProveedorCodigo1") + " - " + ldrRow("var_ProveedorNombre1"), ldrRow("var_ProveedorCodigo1"))
    '            ldrRow("var_ProveedorCodigo2") = CType(e.Item.FindControl("txtProveedorCodigo2E"), TextBox).Text.Trim
    '            ldrRow("var_ProveedorNombre2") = Me.hdnProveedor2.Value.Trim
    '            ldrRow("var_Alternativa2") = IIf(ldrRow("var_ProveedorNombre2") <> "", ldrRow("var_ProveedorCodigo2") + " - " + ldrRow("var_ProveedorNombre2"), ldrRow("var_ProveedorCodigo2"))
    '            ldrRow("var_ProveedorCodigo3") = CType(e.Item.FindControl("txtProveedorCodigo3E"), TextBox).Text.Trim
    '            ldrRow("var_ProveedorNombre3") = Me.hdnProveedor3.Value.Trim
    '            ldrRow("var_Alternativa3") = IIf(ldrRow("var_ProveedorNombre3") <> "", ldrRow("var_ProveedorCodigo3") + " - " + ldrRow("var_ProveedorNombre3"), ldrRow("var_ProveedorCodigo3"))
    '            ldrRow.AcceptChanges()
    '            dtgAdjunto.EditItemIndex = -1
    '            BindGridAdjunto()
    '    End Select
    'End Sub

    'Private Sub dtgAdjunto_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAdjunto.ItemDataBound
    '  If e.Item.ItemType = ListItemType.EditItem Then
    '    CType(e.Item.FindControl("lblProveedorNombre1E"), Label).Text = Me.hdnProveedor1.Value
    '    CType(e.Item.FindControl("lblProveedorNombre2E"), Label).Text = Me.hdnProveedor2.Value
    '    CType(e.Item.FindControl("lblProveedorNombre3E"), Label).Text = Me.hdnProveedor3.Value
    '  End If
    'End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim lobjUtil As New NuevoMundo.Generales.Objetos
        Dim ldtRes As DataTable
        Dim lstrXML As String
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

        ldtRes = CType(Session("DETALLE_ADJUNTO"), DataTable)
        ldtRes.TableName = "ADJUNTO"
        lobjUtil(ldtRes).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
        Try
            Dim lstrParams() As String = {"var_Empresa", Session("@EMPRESA"), _
                                            "var_Numero", txtDocumento2.Text.Trim, _
                                            "ntx_Detalle", lstrXML, _
                                            "var_Usuario", Session("@USUARIO")}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            lobjCon.ObtenerDataTable("usp_LOG_Requisicion_DetalleAdjuntoGrabar", lstrParams)
            'BuscarRequisicion(txtDocumento2.Text.Trim)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Error", "<Script language=javascript>alert('Error al grabar.');</Script>")
        End Try
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim lstrURL As String
        lstrURL = "../CrystalReports/_Seguridad.asp?strEmpresa=" + Session("@EMPRESA") + "&strRequisicion=" + txtDocumento.Text.Trim + "&strUsuario=" + Session("@USUARIO")
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub

    Private Sub btnImprimir2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir2.Click
        Dim lstrURL As String
        lstrURL = "../CrystalReports/_Logistica.asp?strFormulario=LOG20006&strOC=" + txtDocumento2.Text.Trim
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub

    Private Sub btnEnviarOC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviarOC.Click
        ' Modificado
        ' Objetivo: Validar si aprobaciones estan concluidas para enviar email al proveedor
        ' Fecha: 28-04-2011

        Dim lobjAprobacion As OFISIS.OFILOGI.Requisiciones
        Dim lstrDocumento As String = ""
        Dim strEstado As String
        Dim ldbtAprobacion As New DataTable
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones
        Try
            ' Enviamos la OC por email al proveedor
            lstrDocumento = txtDocumento2.Text
            lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
            ldbtAprobacion = lobjOCOS.Verifica_OC_Proveedor(lstrDocumento)
            strEstado = ldbtAprobacion.Rows(0).Item("st_apro").ToString
            If strEstado = "APR" Then
                Call prc_EnviarOC_Proveedor()
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script language='javascript'>alert('No es posible enviar este documento al Proveedor, falta Aprobar.');</script>")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtgDetalleOrden_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDetalleOrden.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
            If Strings.Left(txtDocumento2.Text.Trim, 4) = "0001" Then 'solo las ocm de artículo
                CType(e.Item.FindControl("ibtiestadisticas"), ImageButton).Attributes.Add("onClick", "fnc_estadisticosxarticulo('" & CType(e.Item.DataItem, DataRowView)("var_ArticuloCodigo").ToString & "');")
            End If
        End If
    End Sub

    Private Sub dtgDetalleOrden_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDetalleOrden.ItemCommand
        Dim lstrarticulo As String = ""
        Select Case e.CommandName
            Case "cmd_estadisticas" 'mostrar el panel con los datos de estadisticos del artículo
                lstrarticulo = hdnarticuloestadistica.Value
                If lstrarticulo.Trim.Length > 0 Then
                    Call prc_datosestadisticosxarticulo(lstrarticulo)
                End If
        End Select
    End Sub

#End Region

#Region "-- Metodos --"

    Private Sub Buscar(ByVal pstrNumero As String)
        Dim lobjCon As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Dim larrParams() = {"var_Empresa", Session("@EMPRESA"), "var_Numero", pstrNumero}
        dtgSeguimiento.DataSource = (lobjCon.ObtenerDataTable("usp_LOG_Requisicion_SeguimientoCompletoListar", larrParams))
        dtgSeguimiento.DataBind()
        lobjCon = Nothing
    End Sub

    Public Function TipoRequisicion() As String
        Return "orden de " + Me.hdnTipo.Value
    End Function
    Public Function TipoOrden() As String
        Return Me.hdnTipo.Value
    End Function

    Private Function BuscarRequisicion(ByVal pstrRequisicion As String, ByVal pstrOrdenCompra As String) As Boolean
        Dim lobjRequisicion As NM.AccesoDatos.AccesoDatosSQLServer
        lobjRequisicion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Dim lstrParams() As String = {"pvch_empresa", Session("@EMPRESA"), _
                                    "pvch_requisicion", pstrRequisicion, "pvch_ordencompra", pstrOrdenCompra}
        Dim ldsRes As DataSet
        mstrTipo = ""
        ldsRes = lobjRequisicion.ObtenerDataSet("usp_log_requisicion_listarhistoria", lstrParams)
        If Not (ldsRes Is Nothing) Then
            '----------------------------------------------------------------------------------------
            '--inicio: datos de requisicion
            '----------------------------------------------------------------------------------------
            If pstrRequisicion.Trim.Length > 0 Then
                With ldsRes.Tables(0)
                    txtDocumento.Text = .Rows(0)("NU_REQI")
                    txtEmision.Text = Format(.Rows(0)("FE_EMIS_REQI"), "dd/MM/yyyy")
                    txtFechaTope.Text = Format(.Rows(0)("FE_TOPE_REQI"), "dd/MM/yyyy")
                    txtFechaExpi.Text = Format(.Rows(0)("FE_EXPI_REQI"), "dd/MM/yyyy")
                    txtCentroCosto.Text = IIf(CType(.Rows(0)("CO_AUXI_EMPR"), String).Trim = "", "", .Rows(0)("CO_AUXI_EMPR") + " - " + .Rows(0)("NO_AUXI"))
                    txtUnidad.Text = .Rows(0)("CO_UNID") + " - " + .Rows(0)("DE_UNID")
                    txtAlmacen.Text = .Rows(0)("CO_ALMA") + " - " + .Rows(0)("DE_ALMA")
                    txtComprador.Text = IIf(CType(.Rows(0)("CO_COMP"), String).Trim = "", "", .Rows(0)("CO_COMP") + " - " + .Rows(0)("NO_COMP"))
                    txtObservaciones.Text = .Rows(0)("DE_OBSE_0001")
                    If .Rows(0)("ST_SERV") = "S" Then
                        Me.hdnTipo.Value = "servicio"
                        dtgDetalleRequisicion.Columns(1).Visible = False
                        dtgDetalleRequisicion.Columns(5).Visible = True
                        dtgDetalleRequisicion.Columns(2).HeaderText = "Servicio"
                        dtgDetalleOrden.Columns(0).Visible = False
                        dtgDetalleOrden.Columns(1).HeaderText = "Servicio"
                        dtgDetalleOrden.Columns(2).Visible = False
                    Else
                        Me.hdnTipo.Value = "compra"
                        dtgDetalleRequisicion.Columns(1).Visible = True
                        dtgDetalleRequisicion.Columns(5).Visible = False
                        dtgDetalleRequisicion.Columns(2).HeaderText = ""
                        dtgDetalleOrden.Columns(0).Visible = True
                        dtgDetalleOrden.Columns(1).HeaderText = ""
                        dtgDetalleOrden.Columns(2).Visible = True
                    End If
                End With
                Session("DETALLE_REQUISICION") = ldsRes.Tables(1)
                Call BindGridRequisicion()
            End If
            '----------------------------------------------------------------------------------------
            '--final: datos de requisicion
            '----------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------
            '--inicio: datos de ocm
            '----------------------------------------------------------------------------------------
            If ldsRes.Tables(2).Rows.Count = 0 Then
                Me.tabpage21.Visible = False
                Me.tabpage31.Visible = False
            Else
                Me.tabpage21.Visible = True
                Me.tabpage31.Visible = True

                With ldsRes.Tables(2)
                    txtDocumento2.Text = .Rows(0)("var_Numero")
                    txtAlmacen2.Text = .Rows(0)("var_AlmacenCodigo") + " - " + .Rows(0)("var_AlmacenNombre")
                    txtObservaciones2.Text = .Rows(0)("var_Observaciones")
                    txtocmproveedor.Text = .Rows(0)("NO_CORT_PROV")
                    txtocmmoneda.Text = .Rows(0)("DE_MONE")
                    hdnocmproveedorcorreo.Value = .Rows(0)("DE_MAIL")
                    Me.txtBase.Text = Format(.Rows(0)("num_Base"), "#,##0.00")
                    Me.txtFOB.Text = Format(.Rows(0)("IM_GAFO"), "#,##0.00")
                    Me.txtFlete.Text = Format(.Rows(0)("IM_FLET"), "#,##0.00")
                    Me.txtSeguro.Text = Format(.Rows(0)("IM_SEGU"), "#,##0.00")
                    Me.txtDscto.Text = Format(.Rows(0)("Dscto"), "#,##0.00")
                    txtSubTotal.Text = Format(.Rows(0)("SubTotal"), "#,##0.00")
                    txtIGV.Text = Format(.Rows(0)("num_IGV"), "#,##0.00")
                    txtTotal.Text = Format(.Rows(0)("num_Total"), "#,##0.00")
                    txtFecCrea.Text = .Rows(0)("FE_CREA")
                    txtFecIni.Text = .Rows(0)("FE_ATEN")
                    txtFecFin.Text = .Rows(0)("FE_ENTR")
                End With
                Session("DETALLE_ORDEN") = ldsRes.Tables(3)
                Call BindGridOrden()
                '----------------------------------------------------------------------------------------
                '--final: datos de ocm
                '----------------------------------------------------------------------------------------
                If ldsRes.Tables(4).Rows.Count > 0 Then
                    'If ldsRes.Tables(4).Rows(0)("int_Grabado") = 0 Then
                    '    Me.hdnObservaciones.Value = "*** No se ha registrado información adjunta."
                    'Else
                    '    Me.hdnObservaciones.Value = ""
                    'End If
                    Me.hdnObservaciones.Value = ""
                Else
                    Me.hdnObservaciones.Value = "*** No se ha registrado información adjunta."
                End If
                Session("DETALLE_ADJUNTO") = ldsRes.Tables(4)
                hdnCodigoDoc.Value = pstrOrdenCompra
                Call BindGridAdjunto()
            End If
            If Me.hdnTipo.Value = "servicio" Then
                Me.tabpage31.Visible = False
            End If
        End If
    End Function

    Private Sub BindGridRequisicion()
        dtgDetalleRequisicion.DataSource = CType(Session("DETALLE_REQUISICION"), DataTable)
        dtgDetalleRequisicion.DataBind()
    End Sub

    Private Sub BindGridOrden()
        dtgDetalleOrden.DataSource = CType(Session("DETALLE_ORDEN"), DataTable)
        dtgDetalleOrden.DataBind()
    End Sub

    Private Sub BindGridAdjunto()

        dtgAdjuntos.DataSource = CType(Session("DETALLE_ADJUNTO"), DataTable)
        dtgAdjuntos.DataBind()
    End Sub

    Public Function Observaciones() As String
        Return Me.hdnObservaciones.Value
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function fnc_VerificarOC_paraEnvio(ByVal pstrOrdenCompra As String) As DataTable
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra, ldtbDatos As DataTable
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion_popup.htm")
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        Try
            ldtbDatos = lobjOrdenCompra.fnc_Listar(1, pstrOrdenCompra)
            Session("datosoc_001") = ldtbDatos
            Return ldtbDatos
        Catch ex As Exception
        Finally
        End Try
    End Function

    Public Sub prc_EnviarOC_Proveedor_V2()
        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String
        Dim lstrRutaFile_SQL As String
        Dim lstrFile_SQL As String

        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra, ldtbDatosOC As DataTable
        Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable
        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim lstrEmailDestino As String = "", lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
        Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
        Dim lstrError As String = ""

        'Dim lobjFile As File

        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        ldtbDatosOC = CType(Session("datosoc_001"), DataTable)
        lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        lstrNumeroOC = txtDocumento2.Text.Trim
        lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        'hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        lstrRutaFile_SQL = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR_SQL").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"
        lstrFile_SQL = lstrNombreFile & ".pdf"

        'obtener ruta donde guardar
        '-----------------------------------------------------------------------------------------
        '--FINAL: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: CONVERTIR A PDF
        '-----------------------------------------------------------------------------------------
        Try
            lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing
            lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrMailTO = IIf(lstrEmailDestino.Trim = "", "sistemas@nuevomundosa.com", lstrEmailDestino.Trim)
            lstrMailCC = ""
            lstrMailBCC = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_CCO").ToString()

            lstrMailSubject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC

            lstrMailBody = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
                            "<P><FONT face='Verdana' size='2'><STRONG>" + ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString + "</STRONG></FONT></P>" + _
                            "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la adjunta solicitud de compra <STRONG>" + _
                            "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
                            "<BR><BR>" + _
                            "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
                            "<BR>" + _
                            "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
                            "<BR>" + _
                            "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
                            "<BR>" + _
                            "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
                            "<P><FONT size='2'></FONT></P>" + _
                            "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
                            "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
                            "Por favor no responder este correo.<BR>" + _
                            "-------------------------------------------------------------------------------</FONT></P>"

            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody, lstrRutaFile_SQL, lstrFile_SQL)

            'Dim mailMsg As System.Net.Mail.MailMessage
            'mailMsg = New System.Net.Mail.MailMessage()
            'Dim lobjAdjunto As System.Net.Mail.Attachment

            ''20121005 EPM Configurar arreglo para el To
            'Dim lstrTo_arreglo() As String = lstrEmailDestino.Split(";")
            'For lintIndice = 0 To lstrTo_arreglo.Length - 1
            '    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            'Next
            ''Si no hay destinatario que lo envie a sistemas
            'If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

            'Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            'Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            'Dim userCredential As New System.Net.NetworkCredential(user, password)

            'With mailMsg
            '    '.From = New System.Net.Mail.MailAddress("Nuevo Mundo - Compras<compras@nuevomundosa.com>")
            '    .From = New System.Net.Mail.MailAddress(user)
            '    .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
            '    .Body = lstrCuerpoMensaje
            '    .Priority = System.Net.Mail.MailPriority.High
            '    .IsBodyHtml = True
            '    lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
            '    .Attachments.Add(lobjAdjunto)
            'End With

            'Dim Servidor As New System.Net.Mail.SmtpClient
            'Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            'Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            'Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            'If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
            '    Servidor.Credentials = userCredential
            'End If
            'Servidor.Send(mailMsg)
            'Servidor = Nothing
            'lobjAdjunto = Nothing

        Catch ex As Exception
            lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
        Finally
            objCorreo = Nothing
        End Try
        If lstrError.Length > 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
            Exit Sub
        End If
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('La orden de servicio -- " & lstrNumeroOC & " -- ha sido enviado al proveedor.');</script>")
        '-----------------------------------------------------------------------------------------
        '--FINAL: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: ACTUALIZAR METADATA DE ENVIO EN OC
        '-----------------------------------------------------------------------------------------
        Try
            lstrUsuario = IIf(IsNothing(Session.Item("@USUARIO")), "", Session.Item("@USUARIO"))
            ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
            '-----------------------------------------------------------------------------------------
            '--FINAL: ACTUALIZAR METADATA DE ENVIO EN OC
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
            'If lobjFile.Exists(lstrRutaFile) Then
            '  lobjFile.Delete(lstrRutaFile)
            'End If

            If File.Exists(lstrRutaFile) Then
                File.Delete(lstrRutaFile)
            End If

            '-----------------------------------------------------------------------------------------
            '--FINAL: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
        Catch ex As Exception

        Finally
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
        End Try
    End Sub


    Public Sub prc_EnviarOC_Proveedor()
        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String
        Dim lstrRutaFile_SQL As String
        Dim lstrFile_SQL As String

        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra, ldtbDatosOC As DataTable
        Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable
        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim lstrEmailDestino As String = "", lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
        Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
        Dim lstrError As String = ""

        'Dim lobjFile As File

        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        ldtbDatosOC = CType(Session("datosoc_001"), DataTable)
        lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        lstrNumeroOC = txtDocumento2.Text.Trim
        lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        'hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        lstrRutaFile_SQL = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR_SQL").ToString

        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"
        lstrFile_SQL = lstrNombreFile & ".pdf"

        'obtener ruta donde guardar
        '-----------------------------------------------------------------------------------------
        '--FINAL: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: CONVERTIR A PDF
        '-----------------------------------------------------------------------------------------
        Try
            lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing
            lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            '    lstrCuerpoMensaje = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
            '"<P><FONT face='Verdana' size='2'><STRONG>" + ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString + "</STRONG></FONT></P>" + _
            '"<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la adjunta solicitud de compra <STRONG>" + _
            '"<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
            '"<BR><BR>" + _
            '"<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
            '"<BR>" + _
            '"<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
            '"<BR>" + _
            '"<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
            '"<BR>" + _
            '"</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
            '"<P><FONT size='2'></FONT></P>" + _
            '"<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
            '"Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
            '"Por favor no responder este correo.<BR>" + _
            '"-------------------------------------------------------------------------------</FONT></P>"

            'Dim mailMsg As System.Net.Mail.MailMessage
            'mailMsg = New System.Net.Mail.MailMessage()
            'Dim lobjAdjunto As System.Net.Mail.Attachment

            ''20121005 EPM Configurar arreglo para el To
            'Dim lstrTo_arreglo() As String = lstrEmailDestino.Split(";")
            'For lintIndice = 0 To lstrTo_arreglo.Length - 1
            '    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            'Next
            ''Si no hay destinatario que lo envie a sistemas
            'If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

            'Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            'Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            'Dim userCredential As New System.Net.NetworkCredential(user, password)

            'With mailMsg
            '    '.From = New System.Net.Mail.MailAddress("Nuevo Mundo - Compras<compras@nuevomundosa.com>")
            '    .From = New System.Net.Mail.MailAddress(user)
            '    .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
            '    .Body = lstrCuerpoMensaje
            '    .Priority = System.Net.Mail.MailPriority.High
            '    .IsBodyHtml = True
            '    lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
            '    .Attachments.Add(lobjAdjunto)
            'End With

            'Dim Servidor As New System.Net.Mail.SmtpClient
            'Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            'Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            'Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            'If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
            '    Servidor.Credentials = userCredential
            'End If
            'Servidor.Send(mailMsg)
            'Servidor = Nothing
            'lobjAdjunto = Nothing

            lstrMailTO = IIf(lstrEmailDestino.Trim = "", "sistemas@nuevomundosa.com", lstrEmailDestino.Trim)
            lstrMailCC = ""
            lstrMailBCC = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_CCO").ToString()

            lstrMailSubject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC

            lstrMailBody = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
                            "<P><FONT face='Verdana' size='2'><STRONG>" + ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString + "</STRONG></FONT></P>" + _
                            "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la adjunta solicitud de compra <STRONG>" + _
                            "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
                            "<BR><BR>" + _
                            "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
                            "<BR>" + _
                            "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
                            "<BR>" + _
                            "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
                            "<BR>" + _
                            "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
                            "<P><FONT size='2'></FONT></P>" + _
                            "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
                            "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
                            "Por favor no responder este correo.<BR>" + _
                            "-------------------------------------------------------------------------------</FONT></P>"


            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody, lstrRutaFile_SQL, lstrFile_SQL)

        Catch ex As Exception
            lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
        End Try
        If lstrError.Length > 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
            Exit Sub
        End If
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('La orden de servicio -- " & lstrNumeroOC & " -- ha sido enviado al proveedor.');</script>")
        '-----------------------------------------------------------------------------------------
        '--FINAL: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: ACTUALIZAR METADATA DE ENVIO EN OC
        '-----------------------------------------------------------------------------------------
        Try
            lstrUsuario = IIf(IsNothing(Session.Item("@USUARIO")), "", Session.Item("@USUARIO"))
            ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
            '-----------------------------------------------------------------------------------------
            '--FINAL: ACTUALIZAR METADATA DE ENVIO EN OC
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
            'If lobjFile.Exists(lstrRutaFile) Then
            '  lobjFile.Delete(lstrRutaFile)
            'End If

            If File.Exists(lstrRutaFile) Then
                File.Delete(lstrRutaFile)
            End If

            '-----------------------------------------------------------------------------------------
            '--FINAL: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
        Catch ex As Exception

        Finally
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
        End Try
    End Sub

    Private Sub prc_datosestadisticosxarticulo(ByVal pstrarticulo As String)
        Dim ldtsdatos As DataSet, lobjarticulo As New OFISIS.OFILOGI.Articulos("01", Session("@USUARIO"))
        lobjarticulo.Codigo = pstrarticulo
        ldtsdatos = lobjarticulo.fnc_listarestadisticasxarticulo(1)
        lobjarticulo = Nothing
        dgestcompras.DataSource = ldtsdatos.Tables(0)
        dgestcompras.DataBind()
        dgestconsumo.DataSource = ldtsdatos.Tables(1)
        dgestconsumo.DataBind()
        txteststockactual.Text = CType(ldtsdatos.Tables(2).Rows(0).Item("num_stockactual"), Double)
        txtestconsumopromedio.Text = CType(ldtsdatos.Tables(2).Rows(0).Item("num_consumopromedio"), Double)

        ldtsdatos = Nothing

        ClientScript.RegisterStartupScript(Me.[GetType](), "ubicaropanel", "<script language=javascript>document.all['pnldatosestadisticos'].style.top=" & hdnposicionpanel.Value & "</script>")
    End Sub

#End Region

    Private Sub btnAnalisis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnalisis.Click
        fnc_VerReporteAnalisisMensual()
    End Sub

    ' Funcion para Ver Reporte de Analisis detallado
    Private Function fnc_VerReporteAnalisisMensual()
        Dim strNumOrdenCompra As String
        Dim lstrURL As String = ""
        strNumOrdenCompra = Trim(txtDocumento2.Text)
        lstrURL = "../CrystalReports/rpt_AnalisisOC.asp?strNumOrdenCompra=" & strNumOrdenCompra
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
    End Function

    ' Funcion para Ver Reporte de Analisis detallado Semanal
    Private Function fnc_VerReporteAnalisisSemanal()
        Dim strNumeroItem As String
        Dim lstrURL As String = ""
        strNumeroItem = "1" + Trim(txtDocumento2.Text)
        lstrURL = "../CrystalReports/rpt_AnalisisSemanal.asp?strNumeroItem=" & strNumeroItem
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
    End Function


    Private Sub btnAnalisisSemanal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnalisisSemanal.Click
        fnc_VerReporteAnalisisSemanal()
    End Sub

#Region "Grilla"
    'ItemCommand

    Private Sub dgAdjuntos_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAdjuntos.ItemCommand
        Dim objRequisicion As New clsRequisicion
        Dim lstrError As String = ""
        Dim lintAccion As Int16 = 0
        Dim lintCodigoAdjunto As Integer = 0

        Dim objlblNumeroDoc As New Label
        Dim objlblSecuencia As New Label
        Dim objlblCodigoAdjunto As New Label
        Dim strRoot As String = ""
        Dim strPath As String = ""
        Dim strNombreFile As String = ""
        Try
            Select Case e.CommandName
                Case "Eliminar"
                    objlblNumeroDoc = CType(e.Item.FindControl("lblNumeroDoc"), Label)
                    objlblSecuencia = CType(e.Item.FindControl("lblSecuencia"), Label)
                    objlblCodigoAdjunto = CType(e.Item.FindControl("lblCodigoAdjunto"), Label)

                    objlblNombreAdjunto = CType(e.Item.FindControl("lblNombreAdjunto"), Label)

                    objRequisicion.NumeroDocumento = objlblNumeroDoc.Text
                    objRequisicion.Secuencia = objlblSecuencia.Text
                    objRequisicion.CodigoArchivo = objlblCodigoAdjunto.Text

                    If objRequisicion.fnc_EliminaAdjuntosOCM() <> 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Mensaje : \n Se elimino el archivo adjuntos.');</script>")

                        'eliminamos fisicamente archivo
                        strRoot = hdnDestinoGuardar.Value + "/"
                        strNombreFile = objlblNombreAdjunto.Text
                        strPath = strRoot + strNombreFile
                        If File.Exists(strPath) = True Then
                            File.Delete(strPath)
                        End If

                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede eliminar archivo adjunto.');</script>")
                    End If
                    Call BindGridAdjuntoLista_OrdenCompra()

            End Select

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No Error al eliminar el archivo adjunto.');</script>")
        Finally
            objRequisicion = Nothing
        End Try
        'Call prc_ListarArchivoAdjunto()
    End Sub

    'ItemCreated
    Private Sub dgAdjuntos_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAdjuntos.ItemCreated
        Dim dgAdjuntos As DataGrid
        dgAdjuntos = CType(e.Item.FindControl("dgAdjuntos"), DataGrid)
        If Not dgAdjuntos Is Nothing Then
            AddHandler dgAdjuntos.ItemDataBound, AddressOf dtgAdjuntos_ItemDataBound
            AddHandler dgAdjuntos.ItemCommand, AddressOf dgAdjuntos_ItemCommand
        End If
    End Sub

    'ItemDataBound


    Private Sub dtgAdjuntos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAdjuntos.ItemDataBound
        Dim ImgIcono As New HyperLink
        Dim lblIcono As New Label
        Dim lblNombreAdjunto As New Label
        Dim objBotonEliminar As New ImageButton
        Dim ruta As String
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                lblNombreAdjunto = CType(e.Item.FindControl("lblNombreAdjunto"), Label)
                lblIcono = CType(e.Item.FindControl("lblTipoAdjunto"), Label)
                ImgIcono = CType(e.Item.FindControl("hlnAbrirAdjunto"), HyperLink)
                ImgIcono.ImageUrl = "../../intranet/imagenes/" + lblIcono.Text
                ruta = hdnDestinoAbrir.Value
                ImgIcono.NavigateUrl = "javascript:fnc_AbrirDocumento('" + ruta + "/" + e.Item.DataItem("ArchivoAdjunto").ToString + "','" + Strings.Left(e.Item.DataItem("ArchivoAdjunto").ToString, 19) + "');"

                'objBotonEliminar = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)
                'If hdnMantenimiento.Value = "1" Then
                '    objBotonEliminar.Enabled = True
                'Else
                '    objBotonEliminar.Enabled = False
                'End If

            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n Error al cargar datos de adjuntos.');</script>")
        End Try
    End Sub

#End Region

    'Obtener Tipos de adjunto
    Private Sub prc_ObtenerRutaDestino()
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("27")
        hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        hdnDestinoGuardar.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_GUARDAR").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
    End Sub


    Private Sub BindGridAdjuntoLista_OrdenCompra()
        Dim objRequisiciones As New clsRequisicion
        Dim dtbListaAdjuntos As New DataTable

        Try        
            objRequisiciones.NumeroDocumento = hdnCodigoDoc.Value
            objRequisiciones.fnc_ListarAdjuntos_OrdenCompra(dtbListaAdjuntos)

            Session("DETALLE_ADJUNTO") = dtbListaAdjuntos

            Call BindGridAdjunto()

        Catch ex As Exception
            Throw ex
        Finally
            objRequisiciones = Nothing
            dtbListaAdjuntos = Nothing
        End Try
        
    End Sub




    Private Sub btnAdjuntarArchivo_Click(sender As Object, e As System.EventArgs) Handles btnAdjuntarArchivo.Click
        If hdnTipo.Value = "compra" Then
            Call BindGridAdjuntoLista_OrdenCompra()
        End If
    End Sub
End Class
