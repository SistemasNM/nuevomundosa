Public Class frm_RegistrarAprobacionOrdenesDet
    Inherits System.Web.UI.Page

    ' init de la pagina
    Private Sub frm_RegistrarAprobacionOrdenesDet_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ECASTILL"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub

    ' Load de la pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        Dim lstrNumero As String
        Dim lstrTipo As String

        lstrNumero = Request("NumeroDoc")
        lstrTipo = Request("strTipo")
        'lstrNumero = "0001-0000088023"
        'lstrTipo = "1"
        If Not IsPostBack Then
            Buscar(lstrNumero)
            txtDocumento.Attributes.Add("readonly", "readonly")
            txtAlmacen.Attributes.Add("readonly", "readonly")
            txtMoneda.Attributes.Add("readonly", "readonly")
            txtProveedor.Attributes.Add("readonly", "readonly")
            txtCondicion.Attributes.Add("readonly", "readonly")
            txtObservaciones.Attributes.Add("readonly", "readonly")
            txtSubTotal.Attributes.Add("readonly", "readonly")
            txtDescuento.Attributes.Add("readonly", "readonly")
            txtIGV.Attributes.Add("readonly", "readonly")
            txtTotal.Attributes.Add("readonly", "readonly")
            txtNumReq.Attributes.Add("readonly", "readonly")

            btnAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos()")
            btnAprobar.Attributes.Add("Onclick", "fnc_BuscarGrupo('" + txtDocumento.Text + "')")

        End If
        If lstrTipo = "Consulta" Then
            btnAprobar.Visible = False
            btnDesaprobar.Visible = False
        End If

        If hdnarticuloestadistica.Value.Trim.Length = 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")
        End If
    End Sub

    ' boton: Ver Reporte de Analisis detallado
    Protected Sub btnAnalisis_Click(sender As Object, e As EventArgs) Handles btnAnalisis.Click
        Dim strNumOrdenCompra As String
        Dim lstrURL As String = ""
        strNumOrdenCompra = Trim(txtDocumento.Text)
        lstrURL = "../CrystalReports/rpt_AnalisisOC.asp?strNumOrdenCompra=" & strNumOrdenCompra
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub

    ' boton: Ver Reporte de impresion de OC/OS
    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim lstrURL As String
        lstrURL = "../CrystalReports/_Logistica.asp?strFormulario=LOG20006&strOC=" + txtDocumento.Text.Trim
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub

    ' boton: desaprobar OC*OS
    Protected Sub btnDesaprobar_Click(sender As Object, e As EventArgs) Handles btnDesaprobar.Click
        DesAprobarOrden()
    End Sub


#Region "Procedimientos"
    ' Buscar orden de OC/OS
    Private Sub Buscar(ByVal pstrCodigo As String)
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones

        lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjOCOS.Codigo = pstrCodigo
        'mostramos datos de la cabecera
        If lobjOCOS.BuscarOCOS() Then
            With lobjOCOS.SetDatos.Tables(0)
                txtDocumento.Text = .Rows(0)("var_Numero")
                txtAlmacen.Text = .Rows(0)("var_AlmacenCodigo") + " - " + .Rows(0)("var_AlmacenNombre")
                txtObservaciones.Text = .Rows(0)("var_Observaciones")
                txtSubTotal.Text = Format(.Rows(0)("num_Base"), "#,##0.00")
                txtIGV.Text = Format(.Rows(0)("num_IGV"), "#,##0.00")
                txtTotal.Text = Format(.Rows(0)("num_Total"), "#,##0.00")
                txtMoneda.Text = .Rows(0)("DE_MONE")
                txtProveedor.Text = .Rows(0)("NO_CORT_PROV")
                txtDescuento.Text = Format(.Rows(0)("num_Descuento"), "#,##0.00")
                txtCondicion.Text = .Rows(0)("DE_COND")
                txtObservaciones2.Text = .Rows(0)("var_Observaciones2")
                txtNumReq.Text = .Rows(0)("NumReq")
                txtFecCrea.Text = .Rows(0)("fe_crea")
                txtFecIni.Text = .Rows(0)("fe_aten")
                txtFecFin.Text = .Rows(0)("fe_entr")
            End With

            'mostramos datos del detalle
            dtgDetalle.DataSource = lobjOCOS.SetDatos.Tables(1)
            dtgDetalle.DataBind()

            If Trim(txtObservaciones2.Text).Length > 0 Then
                lblMsgDesaprobacion.Visible = True
                lblMsgDesaprobacion.Text = "El documento ha sido previamente desaprobado"
            Else
                lblMsgDesaprobacion.Visible = False
                lblMsgDesaprobacion.Text = ""
            End If

            If Trim(txtNumReq.Text).Length = 0 Then
                btnAdjuntos.Enabled = True
            End If

            Seguimiento(Trim(txtNumReq.Text))
        End If
        lobjOCOS = Nothing
    End Sub

    ' Seguimineto de aprobaciones OC/OS
    Private Sub Seguimiento(ByVal pstrNumero As String)
        Dim lobjCon As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Dim strNumero As String = ""
        strNumero = "1" & pstrNumero
        Dim larrParams() = {"var_Empresa", Session("@EMPRESA"), _
                            "var_Numero", strNumero}
        dtgSeguimiento.DataSource = (lobjCon.ObtenerDataTable("usp_LOG_Requisicion_SeguimientoCompletoListar", larrParams))
        dtgSeguimiento.DataBind()
        lobjCon = Nothing
    End Sub

    ' Desaprobaciones OC/OS
    Private Sub DesAprobarOrden()
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones
        Dim strObservaciones2 As String
        Dim lstrNumeroOC As String
        Dim ldtbCorreo As DataTable

        lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjOCOS.Codigo = txtDocumento.Text
        If txtObservaciones2.Text.Length = 0 Then
            strObservaciones2 = "Desaprobado por: " + Session("@USUARIO") + " Fecha: " + Now.ToShortDateString
        Else
            strObservaciones2 = Trim(txtObservaciones2.Text) + " Desaprobado por: " + Session("@USUARIO") + " Fecha: " + Now.ToShortDateString
        End If
        lstrNumeroOC = txtDocumento.Text
        Try
            If Trim(txtObservaciones2.Text).Length > 0 Then
                ldtbCorreo = New DataTable
                ldtbCorreo = lobjOCOS.DesaprobarOCOS(strObservaciones2)
                'Verificamos si es el ultimo paso
                If Not ldtbCorreo Is Nothing And ldtbCorreo.Rows.Count > 0 Then
                    Dim strEstado As String = ""
                    strEstado = ldtbCorreo.Rows(0).Item("ti_situ").ToString
                    If strEstado = "ACT" Then
                        'Enviamos email
                        lblMsgDesaprobacion.Visible = True
                        lblMsgDesaprobacion.Text = "Documento desaprobado"
                        EnviarEmailDesaprobacion(ldtbCorreo)
                    End If
                End If
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('El documento: " & lstrNumeroOC & " ha sido desaprobado.');</script>")
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('Por favor ingrese una observacion (en el campo otras observaciones) para la Desaprobacion.');</script>")
            End If
        Catch ex As Exception
        End Try

    End Sub

    'Enviar correo de desaprobacion
    Private Sub EnviarEmailDesaprobacion(ByVal ldtbCorreo As DataTable)
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""
        Dim strNumeroOC As String

        strNumeroOC = txtDocumento.Text
        Try
            lstrPara = ldtbCorreo.Rows(0).Item("de_dire_mail")
            If lstrPara.Length > 0 Then
                lstrCopia = "ecastillo@nuevomundosa.com"
                lstrTitulo = "[Intranet] OC/OS DESAPROBADA."
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA DESAPROBADO LA OC/OS " + _
                                        "<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & strNumeroOC & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG></FONT></FONT></P>" + _
                                        "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(ldtbCorreo.Rows(0).Item("co_usua_crea")) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                        "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
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
                    '.From = New System.Net.Mail.MailAddress("OC/OS DESAPROBACION<aprobaciones@nuevomundosa.com>")
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
                strDestinatarios = "Se Comunico email informativo a: " + lstrPara
                lblError.Text = strDestinatarios
                lblError.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = ""
            lblError.Visible = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub

    ' estidistico de precios
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

#Region "Grillas"
    'dtgDetalle_ItemDataBound
    Private Sub dtgDetalle_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDetalle.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
            CType(e.Item.FindControl("ibtiestadisticas"), ImageButton).Attributes.Add("onClick", "fnc_estadisticosxarticulo('" & CType(e.Item.DataItem, DataRowView)("var_ArticuloCodigo").ToString & "');")
        End If

    End Sub

    'dtgDetalle_ItemCommand
    Private Sub dtgDetalle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDetalle.ItemCommand
        Dim lstrarticulo As String = ""
        'mostrar el panel con los datos de estadisticos del artículo
        Select Case e.CommandName
            Case "cmd_estadisticas"
                lstrarticulo = hdnarticuloestadistica.Value
                If lstrarticulo.Trim.Length > 0 Then
                    Call prc_datosestadisticosxarticulo(lstrarticulo)
                End If
        End Select
    End Sub

#End Region

End Class