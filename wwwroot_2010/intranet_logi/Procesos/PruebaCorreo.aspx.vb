Imports System.IO
Imports System.Net.Mail

Public Class PruebaCorreo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        prc_EnviarOC_Proveedor()
    End Sub

    Public Sub prc_EnviarOC_Proveedor()
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra 'ldtbDatosOC As DataTable
        'Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable
        'Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
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
        'ldtbDatosOC = CType(Session("datosoc_001"), DataTable)
        'lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        lstrNumeroOC = "0001-0000092850"

        lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        'hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"
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
            'lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            'lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            'lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            'lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            'lobjOpcDisco.DiskFileName = lstrRutaFile
            'lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            'lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrCuerpoMensaje = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
          "<P><FONT face='Verdana' size='2'><STRONG>" + "PRUEBA.COM" + "</STRONG></FONT></P>" + _
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

            Dim mailMsg As System.Net.Mail.MailMessage
            mailMsg = New System.Net.Mail.MailMessage()
            Dim lobjAdjunto As System.Net.Mail.Attachment

            '20121005 EPM Configurar arreglo para el To
            'Dim lstrTo_arreglo() As String = lstrEmailDestino.Split(";")
            'For lintIndice = 0 To lstrTo_arreglo.Length - 1
            '    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            'Next

            ''Si no hay destinatario que lo envie a sistemas
            'If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With mailMsg
                .From = New System.Net.Mail.MailAddress(user)
                .To.Add(TextBox1.Text)
                .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
                .Body = lstrCuerpoMensaje
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
                'lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
                '.Attachments.Add(lobjAdjunto)
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
            lobjAdjunto = Nothing

        Catch ex As Exception
            'lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
            lstrError = ex.Message
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
            'ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
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

    Public Sub prc_EnviarOC_Proveedor_2()
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra 'ldtbDatosOC As DataTable
        'Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable
        'Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim lstrEmailDestino As String = "", lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
        Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
        Dim lstrError As String = ""
        Dim fileAttachment As Attachment

        'Dim lobjFile As File

        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        'ldtbDatosOC = CType(Session("datosoc_001"), DataTable)
        'lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        lstrNumeroOC = "0001--"

        lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        'hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"
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
            'lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            'lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            'lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            'lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            'lobjOpcDisco.DiskFileName = lstrRutaFile
            'lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            'lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrCuerpoMensaje = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
          "<P><FONT face='Verdana' size='2'><STRONG>" + "PRUEBA.COM" + "</STRONG></FONT></P>" + _
          "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la adjunta solicitud de compra <STRONG>" + _
          "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
          "<BR><BR>" + _
          "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
          "<BR>" + _
          "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
          "<BR>" + _
          "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
          "<input style='font-weight:normal;font-size: medium;font-family: IDAutomationHC39M;' value='*00000012'/>" + _
          "<BR>" + _
         "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
        "<P><FONT size='2'></FONT></P>" + _
        "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
          "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
          "Por favor no responder este correo.<BR>" + _
          "-------------------------------------------------------------------------------</FONT></P>"

            Dim mailMsg As System.Net.Mail.MailMessage
            mailMsg = New System.Net.Mail.MailMessage()
            Dim lobjAdjunto As System.Net.Mail.Attachment

            '20121005 EPM Configurar arreglo para el To
            'Dim lstrTo_arreglo() As String = lstrEmailDestino.Split(";")
            'For lintIndice = 0 To lstrTo_arreglo.Length - 1
            '    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            'Next

            ''Si no hay destinatario que lo envie a sistemas
            'If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            'Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            'Dim userCredential As New System.Net.NetworkCredential(user, password)

            fileAttachment = New Attachment(Server.MapPath("../Procesos/PDF/Curso-Aspnet.pdf"))

            With mailMsg
                .From = New System.Net.Mail.MailAddress(IIf(txtUsuario.Text.Trim = "", user, txtUsuario.Text.Trim))

                .To.Add(TextBox1.Text.Trim)
                If Not TextBox2.Text.Trim.Equals("COPY") And Not TextBox2.Text.Trim.Equals("") Then
                    .CC.Add(TextBox2.Text.Trim)
                End If
                '.Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
                .Subject = txtSubject.Text.Trim
                .Body = lstrCuerpoMensaje
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
                'lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
                If rdbAttachment.SelectedValue.Equals("1") Then
                    .Attachments.Add(fileAttachment)
                End If
            End With

            Dim Servidor As New System.Net.Mail.SmtpClient
            Servidor.Host = txtServidor.Text.Trim 'System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            Try
                Convert.ToInt32(txtPuerto.Text.Trim)
                Servidor.Port = Convert.ToInt32(txtPuerto.Text.Trim) 'Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('Ingrese un # Puerto correcto.');</script>")
            End Try
            If rdbSSL.SelectedValue.Equals("1") Then
                Servidor.EnableSsl = True

                If Not txtUsuario.Text.Trim.Equals("alertas@nuevomundosa.com") And Not txtUsuario.Text.Trim.Equals("") Then
                    Dim user_2 As String = txtUsuario.Text.Trim 'System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                    Dim password As String = txtPass.Text.Trim 'System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                    Dim userCredential As New System.Net.NetworkCredential(user_2, password)
                    Servidor.Credentials = userCredential
                End If
            Else
                Servidor.EnableSsl = False
            End If
            Servidor.Send(mailMsg)
            Servidor = Nothing
            lobjAdjunto = Nothing

        Catch ex As Exception
            'lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
            lstrError = ex.Message
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
            'ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        EnviarCorreo()

    End Sub


    Private Sub EnviarCorreo()
        Dim i As Integer
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrCopias As String = ""

        'Modificado: 26-04-2011
        'Obtenemos datos del proveedor, copiamos al personal de logistica
        'Alexander Torres cardenas

        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim lstrUsuTemp As String
        Dim lintTipoMensaje As Integer
        Dim strProveedor
        Dim strRucProveedor As String
        Dim strCopia As String
        Dim listaPara As String


        'EPOMA - 2011.09.02
        'Se cambia para que solo envia UN solo correo, ya no N correos(revisar sourcesafe)
        strCopia = ""
        listaPara = ""
        lintTipoMensaje = 0
        lstrUsuTemp = ""

        'For i = 0 To pdtCorreos.Rows.Count - 1
        '    With pdtCorreos.Rows(i)
        '        lintTipoMensaje = .Item("Tipo")
        '        If InStr(listaPara, .Item("UsuarioCorreo")) <= 0 And InStr(strCopia, .Item("UsuarioCorreo")) <= 0 Then
        '            listaPara = listaPara + .Item("UsuarioCorreo") & ";"
        '        End If
        '        lstrUsuTemp = .Item("Usuario")
        '    End With
        'Next i

        listaPara = "lalanoca@nuevomundosa.com"
        'si no hay lista para sale del proceso enviarcorreos
        If listaPara.Length <= 5 Then Exit Sub



        '=============================================================================
        'se actualiza para agregar a jbaltazar y acaro desde tabla maestra de administrativo nuevo mundo
        Dim ldtbCorreosCopia As DataTable, lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"chr_CodigoTabla", 33} 'tabla  maestra
        Dim lint_fila As Integer = 0
        Dim larrCopia() As String

        lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
        ldtbCorreosCopia = lobjConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", lstrParametros)

        lobjConexion = Nothing

        If (Not ldtbCorreosCopia Is Nothing) AndAlso ldtbCorreosCopia.Rows.Count > 0 Then
            For lint_fila = 0 To ldtbCorreosCopia.Rows.Count - 1
                If ldtbCorreosCopia.Rows(lint_fila).Item("vch_nombre") = "Logistica" Then
                    lstrCopias = ldtbCorreosCopia.Rows(lint_fila).Item("vch_email")
                    Exit For
                End If
            Next
        End If

        ldtbCorreosCopia = Nothing

        'If InStr(listaPara, "jbaltazar@nuevomundosa.com") <= 0 And InStr(strCopia, "jbaltazar@nuevomundosa.com") <= 0 Then
        '    strCopia = strCopia + "jbaltazar@nuevomundosa.com;"
        'End If
        'If InStr(listaPara, "malfaro@nuevomundosa.com") <= 0 And InStr(strCopia, "malfaro@nuevomundosa.com") <= 0 Then
        '    strCopia = strCopia + "malfaro@nuevomundosa.com;"
        'End If

        If lstrCopias.Length > 0 Then
            'pasar a array
            larrCopia = lstrCopias.Split(",")
            For lint_fila = 0 To larrCopia.Length - 1
                If InStr(listaPara, larrCopia(lint_fila)) <= 0 And InStr(strCopia, larrCopia(lint_fila)) <= 0 Then
                    strCopia = strCopia + larrCopia(lint_fila) + ";"
                End If
            Next
        End If
        '=============================================================================





        TextBox1.Text = listaPara
        TextBox2.Text = lstrCopias


    End Sub


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        prc_EnviarOC_Proveedor_2()
    End Sub
End Class