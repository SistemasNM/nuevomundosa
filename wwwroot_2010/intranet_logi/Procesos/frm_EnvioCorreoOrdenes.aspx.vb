Imports NuevoMundo.Logistica
Imports NuevoMundo

Public Class frm_EnvioCorreoOrdenes
    Inherits System.Web.UI.Page

    Dim strNumeroOrden As String = ""
    Dim pdtCorreos As New DataTable
    Dim mstr_Para As String = ""
    Dim mstr_Copia As String = ""

    Private Sub frm_EnvioCorreoOrdenes_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "atorresc"
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack = True Then
            If Not Request.Item("pstrNumeroDoc") Is Nothing Then hdnCodigoDoc.Value = Request.Item("pstrNumeroDoc").ToString

            btnCerrar.Attributes.Add("Onclick", "javascript:fnc_Cerrar();")
            hdnCodigoDoc.Value = Request.Item("pstrNumeroDoc")
            'hdnCodigoDoc.Value = "0001-0000089171"
            ConsultarCorreos()
        End If

    End Sub

    Protected Sub btnenviar_Click(sender As Object, e As EventArgs) Handles btnenviar.Click
        ' EnviarEmail(pdtCorreos)
        Call EnviarEmail_V2()

    End Sub

#Region "Procediminetos"

    ' Consulta lista de involudrados
    Public Sub ConsultarCorreos()
        pdtCorreos = Nothing
        strNumeroOrden = hdnCodigoDoc.Value
        lblMensaje.Text = ""
        Try
            If strNumeroOrden.Length > 0 Then
                Dim objOrdenes As New NuevoMundo.Logistica.OrdenCompra
                pdtCorreos = objOrdenes.ListaUsuarioEmail(strNumeroOrden)
                If Not pdtCorreos Is Nothing Then
                    If Not pdtCorreos Is Nothing Then
                        For i = 0 To pdtCorreos.Rows.Count - 1
                            If mstr_Para.Trim.Length = 0 Then
                                mstr_Para = Trim(pdtCorreos.Rows(i).Item("vch_EmailPara"))
                            Else
                                mstr_Para = mstr_Para + ";" + Trim(pdtCorreos.Rows(i).Item("vch_EmailPara"))
                            End If
                            mstr_Copia = pdtCorreos.Rows(i).Item("vch_EmailCopia")
                            hndComprador.Value = Trim(pdtCorreos.Rows(i).Item("vch_UsuarioNombre"))
                            hndFechaOC.Value = Trim(pdtCorreos.Rows(i).Item("vch_FechaAprobacion"))
                            hndProveedor.Value = Trim(pdtCorreos.Rows(i).Item("vch_Proveedor"))
                            hndCorreoPara.Value = mstr_Para
                            hndCorreoCopia.Value = mstr_Copia
                        Next i
                    End If
                    txtAsunto.Text = "[Intranet] OC/OS POR APROBAR."
                    txtPara.Text = mstr_Para
                    txtCopia.Text = mstr_Copia
                End If
            End If
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try

    End Sub

    'Enviar correo electronico
    Private Sub EnviarEmail_V2()
        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String

        Try
            If txtComentario.Text.Length > 0 Then
                lstrMailTO = IIf(txtPara.Text.Trim = "", "sistemas@nuevomundosa.com", txtPara.Text.Trim)
                lstrMailCC = txtCopia.Text.Trim  'hndCorreoCopia.Value
                lstrMailSubject = txtAsunto.Text.Trim
                lstrMailBody = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA OBSERVACION PARA LA " + _
                           "APROBACION DE LA OC/OS &nbsp;: <FONT style='BACKGROUND-COLOR: #8DE806'>" + _
                           "<STRONG>" & hdnCodigoDoc.Value & "</STRONG><FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>&nbsp;" + _
                           "</STRONG></FONT></FONT></P>" + _
                           "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Observacion:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & Trim(txtComentario.Text) & "</STRONG></FONT>.</FONT><BR></FONT><BR>" + _
                           "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Proveedor&nbsp;&nbsp;&nbsp:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & hndProveedor.Value.ToUpper & "</STRONG></FONT>.</FONT></FONT>" + _
                           "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Comprador&nbsp:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & hndComprador.Value.ToUpper & "</STRONG></FONT>.</FONT></FONT>" + _
                           "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Fec. Doc.&nbsp;&nbsp;&nbsp;:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & hndFechaOC.Value.ToUpper & "</STRONG></FONT>.</FONT><BR></FONT><BR>" + _
                           "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> Comuniquese con el Area de Logistica. <BR><BR>" + _
                           "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                           "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                           "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                           "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                           "Por favor no responder este correo.<BR>" + _
                           "Departamento de Sistemas<BR>" + _
                           "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                           "-------------------------------------------------------------------------------</P>"


                If objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody) Then
                    lblMensaje.Text = "Se envio el correo electronico con la observación."
                End If
            Else
                Throw New Exception("Debe ingresar una observación/comentario para el correo electronico")
            End If

        Catch ex As Exception
            lblMensaje.Text = "Error al enviar email. " & ex.Message
            txtComentario.Focus()
        Finally

        End Try
    End Sub

    'Enviar correo electronico
    Private Sub EnviarEmail(ByRef pdtCorreos As DataTable)
        Dim lstrCuerpoMensaje As String = ""
        lblMensaje.Text = ""
        Try
            If txtComentario.Text.Length > 0 Then
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA OBSERVACION PARA LA " + _
                            "APROBACION DE LA OC/OS &nbsp;: <FONT style='BACKGROUND-COLOR: #8DE806'>" + _
                            "<STRONG>" & hdnCodigoDoc.Value & "</STRONG><FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>&nbsp;" + _
                            "</STRONG></FONT></FONT></P>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Observacion:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & Trim(txtComentario.Text) & "</STRONG></FONT>.</FONT><BR></FONT><BR>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Proveedor&nbsp;&nbsp;&nbsp:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & hndProveedor.Value.ToUpper & "</STRONG></FONT>.</FONT></FONT>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Comprador&nbsp:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & hndComprador.Value.ToUpper & "</STRONG></FONT>.</FONT></FONT>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffffff'><FONT style='BACKGROUND-COLOR: #ffffff'>Fec. Doc.&nbsp;&nbsp;&nbsp;:&nbsp;<FONT style='BACKGROUND-COLOR: #8DE806'><STRONG>" & hndFechaOC.Value.ToUpper & "</STRONG></FONT>.</FONT><BR></FONT><BR>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> Comuniquese con el Area de Logistica. <BR><BR>" + _
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
                Dim lstrTo_arreglo() As String = hndCorreoPara.Value.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next

                'Si no hay destinatario que lo envie a sistemas
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

                'Configurar arreglo para el CC
                Dim lstrCC_arreglo() As String = hndCorreoCopia.Value.Split(";")
                For lintIndice = 0 To lstrCC_arreglo.Length - 1
                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)

                With mailMsg
                    '.From = New System.Net.Mail.MailAddress("OC/OS POR APROBAR<aprobaciones@nuevomundosa.com>")
                    .From = New System.Net.Mail.MailAddress(user)
                    .Subject = txtAsunto.Text
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
                lblMensaje.Text = "Se envio el correo electronico con la observacion."
            Else
                lblMensaje.Text = "Debe ingresar una observacion/comentario para el correo electronico"
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error al enviar email. " & ex.Message
            txtComentario.Focus()
        End Try

    End Sub
#End Region
End Class