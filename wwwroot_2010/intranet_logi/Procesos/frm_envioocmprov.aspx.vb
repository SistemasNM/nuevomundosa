
Imports System
Imports System.Data
Imports System.Web
'Imports System.Net.Mail
Imports System.IO
Imports NuevoMundo

Public Class frm_envioocmprov
    Inherits System.Web.UI.Page

    Dim lstrBDServidor As String = "", lstrBDPassword As String = "", lstrBDUsuario As String = ""
    Dim lstrBDBaseDato As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write("LOAD")
        If Not Page.IsPostBack Then
            Call fnc_EnviarOCMaProveedor()
        End If
    End Sub

    Private Function fnc_EnviarOCMaProveedor() As Boolean
        'listar las OCM que tengan flag de no envio al proveedor e enviarlas
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim ldtbDatosOC As DataTable, ldtrDato As DataRow
        Dim ldtbError As DataTable, ldtbRuta As DataTable
        Dim lobjGeneral As New NuevoMundo.General, lstrRuta As String = ""
        Dim lobjUtil As New NM_General.Util
        Dim lstrRutaFile_SQL As String
        Try

            lstrBDUsuario = "ENLACE08" 'lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDServidor = "SERVBD02\NMUNDO02" 'lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDPassword = "ENLACE08" 'lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = "OFILOGI" 'lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing

            ldtbDatosOC = lobjOrdenCompra.fnc_Listar(2, "")

            ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")

            lstrRuta = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
            lstrRutaFile_SQL = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR_SQL").ToString
            'lstrRutaFile_SQL = System.Configuration.ConfigurationManager.AppSettings.Item("RUTA_OCO_SQL").ToString()
            'lstrRuta = "C:\inetpub\wwwroot\docs"
            '''''''''''''''''
            Response.Write(lstrRuta + " - " + lstrRutaFile_SQL + "<br />")
            ldtbRuta = Nothing
            lobjGeneral = Nothing

            For Each ldtrDato In ldtbDatosOC.Rows
                Response.Write(ldtrDato.Item("prv_de_mail").ToString + "<br />")
                If prc_EnviarOC_Proveedor( _
                                        ldtrDato.Item("nu_orco").ToString, _
                                        ldtrDato.Item("prv_de_mail").ToString, _
                                        ldtrDato.Item("de_razo_soci").ToString, _
                                        lstrRuta, lstrRutaFile_SQL) = True Then

                    ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio( _
                                        ldtrDato.Item("nu_orco").ToString(), _
                                        "NMUNDO", _
                                        ldtrDato.Item("prv_de_mail").ToString)
                End If
            Next

        Catch ex As Exception
            Response.Write("ERROR:" + ex.Message + "<br />")
        Finally
            ldtrDato = Nothing
            ldtbDatosOC = Nothing
            ldtbError = Nothing
            lobjOrdenCompra = Nothing
        End Try
    End Function

    Public Function prc_EnviarOC_Proveedor(ByVal pstrDocumento As String, ByVal pstrEmail As String, ByVal pstrRazonSocial As String, ByVal pstrRuta As String, ByVal pstrRutaFile_SQL As String) As Boolean
        Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, lbtnResultado As Boolean = False
        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim mailMsg As System.Net.Mail.MailMessage, lstrCuerpoMensaje As String = ""
        'Dim lobjAdjunto As System.Net.Mail.Attachment
        'Dim lobjAdjunto As System.Net.Mail.Attachment

        ' Dim lstrError As String = "", lobjFile As File

        'Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()

        'Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
        'Dim userCredential As New System.Net.NetworkCredential(user, password)



        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String

        Dim lstrFile_SQL As String

        Try
            '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
            lstrNumeroOC = pstrDocumento
            lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
            lstrRutaFile = pstrRuta & "/" & lstrNombreFile & ".pdf"
            lstrFile_SQL = lstrNombreFile & ".pdf"

            '--INICIO: CONVERTIR A PDF
            lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing

            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            lstrCuerpoMensaje = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
          "<P><FONT face='Verdana' size='2'><STRONG>" + pstrRazonSocial + "</STRONG></FONT></P>" + _
          "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la orden de compra:<STRONG>" + _
          "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
          "<BR><BR>" + _
          "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
          "<BR>" + _
          "<FONT face='Verdana'>Cía. Industrial Nuevo Mundo</FONT>" + _
          "<BR>" + _
          "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
          "<BR>" + _
         "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
        "<P><FONT size='2'></FONT></P>" + _
        "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
          "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
          "Por favor no responder este correo.<BR>" + _
          "-------------------------------------------------------------------------------</FONT></P>"

            'mailMsg = New System.Net.Mail.MailMessage()

            'mailMsg.To.Add(pstrEmail)

            'mailMsg.To.Add("sistemas@nuevomundosa.com")

            'mailMsg.CC.Add(System.Configuration.ConfigurationManager.AppSettings.Item("Correo_CC").ToString)

            'mailMsg.Bcc.Add(System.Configuration.ConfigurationManager.AppSettings.Item("Correo_CCO").ToString)


            'lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)

            lstrMailTO = pstrEmail
            lstrMailCC = System.Configuration.ConfigurationManager.AppSettings.Item("Correo_CC").ToString
            lstrMailBCC = System.Configuration.ConfigurationManager.AppSettings.Item("Correo_CCO").ToString
            lstrMailSubject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
            lstrMailBody = lstrCuerpoMensaje
            lstrRutaFile_SQL = pstrRutaFile_SQL



            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody, lstrRutaFile_SQL, lstrFile_SQL)

            'With mailMsg
            '    .From = New System.Net.Mail.MailAddress(user)
            '    .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
            '    .Body = lstrCuerpoMensaje
            '    .Priority = System.Net.Mail.MailPriority.High
            '    .IsBodyHtml = True
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


            'Dim mailMsg As System.Net.Mail.MailMessage
            'mailMsg = New System.Net.Mail.MailMessage()

            'Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            'Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            'Dim userCredential As New System.Net.NetworkCredential(user, password)

            'With mailMsg
            '    .From = New System.Net.Mail.MailAddress(user)
            '    .To.Add(pstrEmail)
            '    .CC.Add(System.Configuration.ConfigurationManager.AppSettings.Item("Correo_CC").ToString)
            '    .Bcc.Add(System.Configuration.ConfigurationManager.AppSettings.Item("Correo_CCO").ToString)
            '    .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
            '    .Body = lstrCuerpoMensaje
            '    .Priority = System.Net.Mail.MailPriority.High
            '    .IsBodyHtml = True
            '    lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
            '    .Attachments.Add(lobjAdjunto)
            'End With


            'SmtpMail.SmtpServer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString()
            'SmtpMail.Send(lobjmailMsg)

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


            lbtnResultado = True
        Catch ex As Exception
            lbtnResultado = False
            Response.Write(ex.Message)
        Finally
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            lobjAdjunto = Nothing
            'eliminar archivo pdf
            'If lobjFile.Exists(lstrRutaFile) Then
            '    lobjFile.Delete(lstrRutaFile)
            'End If
        End Try
        Return lbtnResultado
    End Function
End Class