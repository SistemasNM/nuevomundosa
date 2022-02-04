Public Class frm_RegistrarDespachoMuestras
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnRegresar As System.Web.UI.WebControls.Button
    Protected WithEvents dtgListaValidacion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmbAlmacen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtObservacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTipoSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblNroSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblFecSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblSituSol As System.Web.UI.WebControls.Label
    Protected WithEvents lblCliente As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodCli As System.Web.UI.WebControls.Label
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodTipoSol As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipoSolicitud As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipoSol As System.Web.UI.WebControls.DropDownList
    Protected Friend cmbTipoPagoMuestra As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlDocumento As System.Web.UI.WebControls.DropDownList
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    'Private WithEvents cmbTipoPagoMuestra As System.Web.UI.WebControls.DropDownList
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Call prc_listar()
            btnGrabar.Attributes.Add("onClick", "javascript:return funValidarGrabar();")

            '20120911 EPM Readonly
            txtObservacion.Attributes.Add("readonly", "readonly")

        End If
    End Sub

#Region "-- Metodos --"
    Public Sub prc_listar()
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim pDT As New DataTable
        Dim pDT_Header As New DataTable
        Dim pDT_Valid As New DataTable
        Dim vrTipoSol As String
        Dim vrcodAlmacen As String
        Dim vrcodTipoPago As String
        Try
            lobjMuestrasTela.NumeroSolicitud = Request.QueryString("Codigo").ToString()

            'ini : cargamos la cabecera de la Solicitud
            lobjMuestrasTela.ListarDetalleSolicitudMuestraHeader(pDT_Header)
            'Me.txtCodTipoSol.Text = pDT_Header.Rows(0)("chr_tipomuestra").ToString()
            'Me.lblTipoSol.Text = pDT_Header.Rows(0)("TipoSol").ToString()
            vrTipoSol = pDT_Header.Rows(0)("chr_tipomuestra").ToString().Trim()
            Me.ddlTipoSol.SelectedValue = vrTipoSol
            Me.ddlTipoSol.Enabled = False
            vrcodAlmacen = pDT_Header.Rows(0)("chr_CodigoAlmacen").ToString().Trim()
            Me.cmbAlmacen.SelectedValue = vrcodAlmacen
            Me.cmbAlmacen.Enabled = False
            Me.lblNroSol.Text = pDT_Header.Rows(0)("var_numerosolicitud").ToString()
            Me.lblFecSol.Text = pDT_Header.Rows(0)("FecSolicitud").ToString()
            Me.lblSituSol.Text = pDT_Header.Rows(0)("SituSol").ToString()
            Me.lblCliente.Text = pDT_Header.Rows(0)("cliente").ToString()
            Me.lblCodCli.Text = pDT_Header.Rows(0)("codcli").ToString()
            Me.txtObservacion.Text = pDT_Header.Rows(0)("var_observaciones").ToString()
            'fini : cargamos la cabecera de la Solicitud
            vrcodTipoPago = pDT_Header.Rows(0)("var_TipoPago").ToString().Trim()
            Me.cmbTipoPagoMuestra.SelectedValue = vrcodTipoPago
            Me.cmbTipoPagoMuestra.Enabled = False
            If CInt(vrTipoSol) = 5 Then
                dtgLista.Columns(2).HeaderText = "Cen  "
                dtgLista.Columns(3).Visible = True
                dtgLista.Columns(4).Visible = False
            ElseIf CInt("0" & vrTipoSol) <> 1 Then
                dtgLista.Columns(2).HeaderText = "Unidades"
                dtgLista.Columns(3).Visible = True
                dtgLista.Columns(4).Visible = False
            Else
                dtgLista.Columns(3).Visible = False
            End If


            lobjMuestrasTela.ListarDetalleSolicitudMuestra(pDT)
            dtgLista.DataSource = pDT
            dtgLista.DataBind()

        Catch ex As Exception
            Response.Redirect("/intranet/finsesion_popup.htm")
        Finally
            lobjMuestrasTela = Nothing
            pDT_Header.Dispose() : pDT_Header = Nothing
            pDT.Dispose() : pDT = Nothing
            pDT_Valid.Dispose() : pDT_Valid = Nothing
        End Try
    End Sub
#End Region

    Private Sub btnRegresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        'Response.Redirect("frm_ListadoDespachoMuestras.aspx")
        Response.Redirect("frm_ListadoDespachoMuestras.aspx")
    End Sub

    Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim txtlote As HtmlControls.HtmlInputText = CType(e.Item.FindControl("txtLote"), HtmlControls.HtmlInputText)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            txtlote.Attributes.Add("onblur", "javascript: txtLote_Onblur('" & drvDatos("codart") & "','" & drvDatos("cant_sol") & "','" & txtlote.ClientID & "')")
            Dim vrTipoSol As String
            vrTipoSol = Me.ddlTipoSol.SelectedValue.Trim()

            If CInt("0" & vrTipoSol) <> 1 Then
                Dim txtLoteValid As HtmlControls.HtmlInputText = CType(e.Item.FindControl("txtLoteValid"), HtmlControls.HtmlInputText)
                txtLoteValid.Value = drvDatos("res_eval")
            End If

        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim vrResEjec As String()
        Dim vrRes_NU_DOC As String = ""
        Dim vrResProc As Boolean = False
        Dim vrInc As Integer = 0
        Dim vrCodArt As String = ""
        Dim vrCantSol As String = ""
        Dim vrCodLote As String = ""
        Dim vrCodCli As String = Me.lblCodCli.Text

        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim pDT_DETALLE As New DataTable

        If cmbTipoPagoMuestra.SelectedValue = "2" Then 'sin factura GRR
            pDT_DETALLE.TableName = "Detalle"
            pDT_DETALLE.Columns.Add("var_ISNU_DOCU", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_ISCO_ITEM", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_INCA_DOCU_MOVI", System.Type.GetType("System.Decimal"))
            pDT_DETALLE.Columns.Add("var_ISCO_AUXI_EMPR", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_ISCO_LOTE", System.Type.GetType("System.String"))

        ElseIf cmbTipoPagoMuestra.SelectedValue = "1" Then 'con factura GUR
            pDT_DETALLE.TableName = "Detalle"
            pDT_DETALLE.Columns.Add("var_ISNU_DOCU", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_ISCO_ITEM", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_INCA_DOCU_MOVI", System.Type.GetType("System.Decimal"))
            pDT_DETALLE.Columns.Add("var_ISCO_AUXI_EMPR", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_ISCO_LOTE", System.Type.GetType("System.String"))
            pDT_DETALLE.Columns.Add("var_ISNU_PRECIO", System.Type.GetType("System.Decimal"))
        End If

       
        Try
            If cmbTipoPagoMuestra.SelectedValue = "2" Then

                For vrInc = 0 To dtgLista.Items.Count - 1
                    vrCodArt = ""
                    vrCantSol = "0"
                    vrCodLote = ""

                    vrCodArt = CType(CType(CType(dtgLista.Items(vrInc), System.Web.UI.WebControls.DataGridItem).Controls(0), System.Web.UI.Control), System.Web.UI.WebControls.TableCell).Text
                    vrCantSol = CType(CType(CType(dtgLista.Items(vrInc), System.Web.UI.WebControls.DataGridItem).Controls(2), System.Web.UI.Control), System.Web.UI.WebControls.TableCell).Text
                    vrCodLote = CType(dtgLista.Items(vrInc).FindControl("txtLote"), HtmlControls.HtmlInputText).Value

                    Dim dr As DataRow = pDT_DETALLE.NewRow()
                    dr(0) = ""
                    dr(1) = vrCodArt
                    dr(2) = vrCantSol
                    dr(3) = vrCodCli
                    dr(4) = vrCodLote
                    pDT_DETALLE.Rows.Add(dr)
                Next

            ElseIf cmbTipoPagoMuestra.SelectedValue = "1" Then

                For vrInc = 0 To dtgLista.Items.Count - 1
                    vrCodArt = ""
                    vrCantSol = "0"
                    vrCodLote = ""
                    vrNumPrecio = "0"

                    vrCodArt = CType(CType(CType(dtgLista.Items(vrInc), System.Web.UI.WebControls.DataGridItem).Controls(0), System.Web.UI.Control), System.Web.UI.WebControls.TableCell).Text
                    vrCantSol = CType(CType(CType(dtgLista.Items(vrInc), System.Web.UI.WebControls.DataGridItem).Controls(2), System.Web.UI.Control), System.Web.UI.WebControls.TableCell).Text
                    vrCodLote = CType(dtgLista.Items(vrInc).FindControl("txtLote"), HtmlControls.HtmlInputText).Value
                    vrNumPrecio = CType(CType(CType(dtgLista.Items(vrInc), System.Web.UI.WebControls.DataGridItem).Controls(5), System.Web.UI.Control), System.Web.UI.WebControls.TableCell).Text

                    Dim dr As DataRow = pDT_DETALLE.NewRow()
                    dr(0) = ""
                    dr(1) = vrCodArt
                    dr(2) = vrCantSol
                    dr(3) = vrCodCli
                    dr(4) = vrCodLote
                    dr(5) = vrNumPrecio
                    pDT_DETALLE.Rows.Add(dr)
                Next
            End If


            'If Me.ddlTipoSol.SelectedValue = 5 Then
            '    vrRes_NU_DOC = lobjMuestrasTela.FunGrabarSalidaMuestraGUR("", vrCodCli, pDT_DETALLE, lblNroSol.Text)
            'Else
            '    vrRes_NU_DOC = lobjMuestrasTela.FunGrabarSalidaMuestra("", vrCodCli, pDT_DETALLE, lblNroSol.Text)
            'End If
            'CAMBIO DG GENERAR PEDIDO GUIA - INI
            ' vrRes_NU_DOC = lobjMuestrasTela.FunGrabarSalidaMuestra("", vrCodCli, pDT_DETALLE, lblNroSol.Text)
            If cmbTipoPagoMuestra.SelectedValue = "2" Then
                vrRes_NU_DOC = lobjMuestrasTela.FunGrabarSalidaMuestra("", vrCodCli, pDT_DETALLE, lblNroSol.Text)
            ElseIf cmbTipoPagoMuestra.SelectedValue = "1" Then
                vrRes_NU_DOC = lobjMuestrasTela.FunGrabarSalidaMuestra_GUR("", vrCodCli, pDT_DETALLE, lblNroSol.Text)
            End If
            'CAMBIO DG GENERAR PEDIDO GUIA - FIN

            If Me.ddlTipoSol.SelectedValue = 5 Then
                Call EnviarCorreoDespachoEtiquetas(lblNroSol.Text, vrRes_NU_DOC)
            End If

        Catch ex As Exception
            Response.Redirect("/intranet/finsesion_popup.htm")
        Finally
            lobjMuestrasTela = Nothing
            pDT_DETALLE.Dispose()
            pDT_DETALLE = Nothing

            If vrRes_NU_DOC = "" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('Ocurrieron Errores al Grabar.!');</script>")
            Else
                Response.Redirect("frm_ListadoDespachoMuestras.aspx?prMSG=S&NUD=" & vrRes_NU_DOC.ToString())
                'Response.Redirect("/intranet/Opciones/Logistica/Procesos/frm_ListadoDespachoMuestras.aspx?prMSG=S")
            End If
        End Try
    End Sub

    Private Sub EnviarCorreoDespachoEtiquetas(ByRef txtNumero As String, ByRef txtGuia As String)
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

            lstrPara = "cloayza@nuevomundosa.com"
            lstrCopia = System.Web.Configuration.WebConfigurationManager.AppSettings("CopiaCorreoEtiquetas").ToString()

            'If lstrCopia.Trim.Length = 0 Then
            '    lstrCopia = "rcastro@nuevomundosa.com" '"sistemas@nuevomundosa.com"
            'Else
            '    lstrCopia = lstrCopia + ";" + "AlmacenPrincipal@nuevomundosa.com"
            'End If
            lstrCuerpoMensaje = ""
            lstrTitulo = ""
            ' With pdtCorreos.Rows(0)
            'lstrTitulo = "Req. " + .Item("NumeroRequisicion") + " por aprobar."
            lstrTitulo = "DESPACHO DE ETIQUETAS"
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
            lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ATENDIDO UNA SOLICITUD DE " + _
                                "ETIQUETAS &nbsp; : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                "<STRONG>" & txtNumero & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                "</STRONG></FONT></FONT></P>" + _
                                "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>GUIA DE REMISION:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & txtGuia & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Session("@USUARIO") & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                 "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>OBSERVACIONES: &nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Me.txtObservacion.Text & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
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
                '.From = New System.Net.Mail.MailAddress("Solicitudes<solicitudes@nuevomundosa.com>")
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


            'End With
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudo enviar el correo electronico.');</script>")
        End Try
    End Sub

End Class
