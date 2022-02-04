'Imports NuevoMundo
Imports System.Web.Mail

Public Class frmPrueba
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
    '    Dim lstrCuerpoMensaje As String = ""
    '    lstrCuerpoMensaje =
    '          "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
    '          "<P><FONT face='Verdana' size='2'><STRONG>" + "#OS" + "</STRONG></FONT></P>" + _
    '          "<P><FONT size='2'><FONT face='Verdana'>Por favor sirvase adjuntar el presente formato con su factura  de la Orden de Servicio : <STRONG>" + _
    '          "<FONT style='BACKGROUND-COLOR: #ffff33'>" + "Nro" + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
    '          "<BR><BR>" + _
    '          "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
    '          "<BR>" + _
    '          "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
    '          "<BR>" + _
    '          "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
    '          "<BR>" + _
    '          "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
    '          "<P><FONT size='2'></FONT></P>" + _
    '          "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
    '          "Este correo ha sido generado automáticamente por el módulo de Cierre de Ordenes de Servicio.<BR>" + _
    '          "Por favor no responder este correo.<BR>" + _
    '          "-------------------------------------------------------------------------------</FONT></P>"
    '    Dim objCorreo As New clsCorreo
    '      objCorreo.MailBody = lstrCuerpoMensaje
    '      objCorreo.MailCC = "luis.alanoca78@gmail.com"
    '        objCorreo.MailTo = "luis_aj@hotmail.com;lalanoca@nuevomundosa.com"
    '      objCorreo.MailFrom = "Nuevo Mundo - Compras<compras@nuevomundosa.com>"
    '      objCorreo.MailSubject = "[Cía. Ind. Nuevo Mundo] - Formato de conclusión de Orden de Servicio número: "
    '      objCorreo.MailSMTP = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ServidorSMTP").ToString()
    '      If objCorreo.EnviarCorreos() = True Then
    '        Response.Write("Envio")
    '      Else
    '        Response.Write("No Envio")
    '      End If
    'End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Dim ServidorSMTP As New System.Net.Mail.SmtpClient
        Dim lobjmailMsg As New System.Net.Mail.MailMessage
        'Dim lobjAdjunto As System.Net.Mail.Attachment

        Dim lstrCuerpoMensaje As String = ""
        lstrCuerpoMensaje =
              "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
              "<P><FONT face='Verdana' size='2'><STRONG>" + "#OS" + "</STRONG></FONT></P>" + _
              "<P><FONT size='2'><FONT face='Verdana'>Por favor sirvase adjuntar el presente formato con su factura  de la Orden de Servicio : <STRONG>" + _
              "<FONT style='BACKGROUND-COLOR: #ffff33'>" + "Nro" + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
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
              "Este correo ha sido generado automáticamente por el módulo de Cierre de Ordenes de Servicio.<BR>" + _
              "Por favor no responder este correo.<BR>" + _
              "-------------------------------------------------------------------------------</FONT></P>"


        'lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)

        Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
        Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
        Dim userCredential As New System.Net.NetworkCredential(user, password)

        With lobjmailMsg
            '.From = New System.Net.Mail.MailAddress("Nuevo Mundo - Compras<compras@nuevomundosa.com>")
            .From = New System.Net.Mail.MailAddress(user)
            .To.Add("luis_aj@hotmail.com")
            .To.Add("lalanoca@nuevomundosa.com")
            .Subject = "[Cía. Ind. Nuevo Mundo] - Formato de conclusión de Orden de Servicio número: "
            .Body = lstrCuerpoMensaje
            .Priority = System.Net.Mail.MailPriority.High
            .IsBodyHtml = True
            '.Attachments.Add(lobjAdjunto)
        End With

        'With ServidorSMTP
        '    .Port = 25
        '    .Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ServidorSMTP").ToString() '172.16.0.10
        '    '.Credentials = New System.Net.NetworkCredential("usuario-smtp", "clave-smtp")
        '    .EnableSsl = False
        'End With

        ServidorSMTP.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
        ServidorSMTP.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
        ServidorSMTP.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
        ServidorSMTP.Credentials = userCredential

        ServidorSMTP.Send(lobjmailMsg)
        ServidorSMTP = Nothing


    End Sub
End Class