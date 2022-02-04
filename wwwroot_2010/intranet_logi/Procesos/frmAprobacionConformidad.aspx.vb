Imports NuevoMundo
Imports System
Imports System.Data
Imports System.Web
Imports System.Web.Mail
Imports System.IO

Public Class frmAprobacionConformidad
    Inherits System.Web.UI.Page
    Dim strNumeroConformidad As String
    Dim strNumeroOrdenServicio As String

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DARWIN"

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
        If Page.IsPostBack = False Then
            strNumeroConformidad = Request.QueryString("strNumeroConformidad")
            Session("NroConformidad") = strNumeroConformidad
            CargarDatosCabecera(strNumeroConformidad)
            btnListaAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos()")
        End If
    End Sub
    Public Sub BindGridTM()
        grvEval.DataSource = CType(Session("dtDatos1"), DataTable)
        grvEval.DataBind()
    End Sub
    Public Sub CargarDatosCabecera(ByVal strNumeroConformidad As String)
        lblMsg.Text = ""
        Dim lobjOrdenServicio As New clsFichaProv
        Dim objDT As New DataSet
        Dim xData1, xData2 As New DataTable
        Dim tipoServicio As String
        lobjOrdenServicio.CodigoEmpresa = Session("@EMPRESA")
        lobjOrdenServicio.Conformidad = strNumeroConformidad
        Session("NroConformidad") = lobjOrdenServicio.Conformidad
        lobjOrdenServicio.MostrarOrdenServicio_Por_Conformidad(objDT)
        xData1 = objDT.Tables(0)
        xData2 = objDT.Tables(1)
        xData3 = objDT.Tables(2)

        With xData1.Rows(0)
            lblNroOrdenServicio.Text = .Item("var_Numero").ToString
            Me.lblNombreProveedor.Text = .Item("NO_CORT_PROV").ToString
            Me.lblRuc.Text = .Item("CO_PROV").ToString
            Me.lblFecha.Text = .Item("fe_emis").ToString
            Me.lblNombreContacto.Text = .Item("Contacto").ToString
            Me.lblTelefonoContacto.Text = .Item("NU_TLFN_CONC").ToString
            Me.TxtCodigoTrabajador.Text = .Item("vch_CodigoSolicitante").ToString
            Me.TxtNombreTrabajador.Text = .Item("no_usua").ToString
            Me.lblEmail.Text = .Item("DE_MAIL").ToString
            Me.txtNroRequisicion.Text = .Item("nu_reqi").ToString
            Me.TxtObservaciones.Text = .Item("vch_Observaciones").ToString
            If (.Item("dtm_fechaInicio").ToString = "" Or .Item("dtm_fechaInicio").ToString Is Nothing) Then
                Me.wdpFechaInicio.Text = Now.ToString("dd/MM/yyyy")
            Else
                Me.wdpFechaInicio.Text = .Item("dtm_fechaInicio").ToString
            End If
            If (.Item("dtm_FechaFinal").ToString = "" Or .Item("dtm_FechaFinal").ToString Is Nothing) Then
                Me.wdpFechaFin.Text = Now.ToString("dd/MM/yyyy")
            Else
                Me.wdpFechaFin.Text = .Item("dtm_FechaFinal").ToString
            End If
            Session("FlgObser") = .Item("FlgObser").ToString
            Session("Item") = .Item("ITEM").ToString
            Me.lblEstado.Text = .Item("chr_estado").ToString
            If .Item("chr_TipoServicio").ToString = "T" Then
                Me.rdTiposervicio1.Checked = True
                Me.rdTiposervicio2.Checked = False
            Else
                Me.rdTiposervicio2.Checked = True
                Me.rdTiposervicio1.Checked = False
            End If
            Me.lblObservaciones.Text = .Item("var_Observaciones").ToString
            Me.lblUsuario.Text = .Item("co_usua_modi").ToString
            tipoServicio = .Item("TIPO_SERVICIO")
            If (.Item("TIPO_SERVICIO") = "I") Then
                lblTipoServicio.Text = "SI"
            Else
                lblTipoServicio.Text = "NO"
            End If
        End With

        Dim lobjPreguntas As New clsEvaluar
        Session("dtDatos1") = lobjPreguntas.ListarPreguntas(tipoServicio)
        BindGridTM()

        If (xData2.Rows.Count() > 0) Then
            CargarDatosEvaluacion(xData2)
        End If
        If (xData3.Rows.Count() > 0) Then
            CargarItemOrdenServicio(xData3)
        End If
        objDT = Nothing
        lobjOrdenServicio = Nothing

    End Sub
    Public Sub CargarDatosEvaluacion(ByVal Dta As DataTable)
        Dim logObjDatosEval As New clsEvaluar
        Dim dt As DataTable
        Dim estadoEval As String
        dt = Dta
        If (dt.Rows.Count() > 0) Then
            estadoEval = dt.Rows(0).Item("ESTADO_CONFORMIDAD").ToString
            txtResultado.Text = dt.Rows(0).Item("RESULTADO")

            For i As Integer = 0 To grvEval.Rows.Count - 1
                Dim dwnResp As DropDownList
                dwnResp = grvEval.Rows(i).FindControl("dwnRespuesta")
                Session("dtEval") = dt

                Dim lobjPreguntas As New clsEvaluar

                dwnResp.DataSource = dt
                dwnResp.DataValueField = "VALUE"
                dwnResp.DataTextField = "RESPUESTA"
                dwnResp.DataBind()
                dwnResp.SelectedValue = dt.Rows(i).Item("VALUE")
            Next
            grvEval.Enabled = False
        End If

    End Sub
    Public Sub CargarItemOrdenServicio(ByVal Dta As DataTable)
        grvItem.DataSource = Dta
        grvItem.DataBind()
    End Sub
    Private Sub btnAprobar_Click(sender As Object, e As System.EventArgs) Handles btnAprobar.Click
        Try

            If Me.lblEmail.Text = "" Then
                lblMsg.Text = "Asegurese que el proveedor cuente con una cuenta de correo...!"
                Exit Sub
            Else
                prcActualizarEstado("2")
                lblEstado.Text = "SOLICITUD APROBADO"
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
            End If

            'prcActualizarEstado("2")
            lblEstado.Text = "SOLICITUD APROBADO"
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub prcActualizarEstado(ByVal strTipo As String)
        Dim strUsuario As String
        Dim ldtCorreos As New DataTable

        ldtCorreos = Nothing
        lblMsg.Text = ""
        lblMsg.Visible = False

        strNumeroOrdenServicio = Trim(lblNroOrdenServicio.Text)
        strUsuario = Session("@Usuario")

        Try

            Dim lobjOrdenServicio As New clsFichaProv
            ' Aprobamos, Anulamos O/S
            ldtCorreos = lobjOrdenServicio.fncOSCambiaEstadoConformidad(strTipo, Session("NroConformidad"), strUsuario)

            'prc_Enviar_ConformidadOS_V2(Me.lblNroOrdenServicio.Text)

            If strTipo = "2" Then
                If Not ldtCorreos Is Nothing And ldtCorreos.Rows.Count > 0 Then
                    prc_Enviar_ConformidadOS_V2(Me.lblNroOrdenServicio.Text)
                    'prc_Enviar_ConformidadOS(Me.lblNroOrdenServicio.Text)
                End If
            End If
            ' Envio de correo
            'If Not ldtCorreos Is Nothing And ldtCorreos.Rows.Count > 0 Then
            '    prc_Enviar_ConformidadOS(Me.lblNroOrdenServicio.Text)
            'Else
            '    If strTipo = "1" Then
            '        lblMsg.Text = "Verifique la cuenta de correo del solicitante."
            '    End If
            'End If

            lobjOrdenServicio = Nothing
            Me.btnAprobar.Visible = False
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    'Public Sub prc_Enviar_ConformidadOS_V2(ByVal pstrDocumento As String)

    '    Dim lstrEmpresa As String = Session("@EMPRESA")
    '    '--------------------------------------------CORREO CC----------------------------------
    '    Dim lobjAlertas As New clsFichaProv
    '    Dim objDTable As New DataTable
    '    objDTable = Nothing
    '    lstrEmailCopia = ""
    '    lobjAlertas.MostrarOpcionesSeleccion(objDTable, "33")
    '    If Not objDTable Is Nothing And objDTable.Rows.Count > 0 Then
    '        lstrEmailCopia = objDTable.Rows(0).Item("vch_email").ToString
    '        'lstrEmailCopia = "dgamarra@nuevomundosa.com"
    '    Else
    '        lstrEmailCopia = ""
    '    End If

    '    '-------------------------------------RUTA ARCHIVO---------------------------------
    '    Dim lobjGeneral As New NuevoMundo.General

    '    lstrNumeroOC = pstrDocumento

    '    ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
    '    lstrRutaFile = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR_SQL").ToString
    '    If Trim(lstrRutaFile).Length <= 0 Then
    '        lblMsg.Text = "Error: Verificar ruta o archivo a adjuntar de la OS"
    '        Exit Sub
    '    End If

    '    '---------------------------------NOMBRE ARCHIVO--------------------
    '    lstrNombreFile = "CONF_" & Session("NroConformidad").ToString & "_" & Strings.Format(Now(), "hhmmss") & ".pdf"
    '    'lstrNombreFile = "CONF_0000002332_122142.pdf"

    '    '-----------------------------------GENERAR DOCUMENTOS---------------------------------------
    '    Dim lobjUtil As New NM_General.Util
    '    Dim lrptOrdenServicio As rptFormatoConformidad = New rptFormatoConformidad
    '    Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions

    '    lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
    '    lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
    '    lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
    '    lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
    '    lobjUtil = Nothing
    '    lrptOrdenServicio.SetParameterValue(0, lstrEmpresa)
    '    lrptOrdenServicio.SetParameterValue(1, lstrNumeroOC)
    '    lrptOrdenServicio.SetParameterValue(2, Session("Item").ToString)
    '    lrptOrdenServicio.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
    '    lrptOrdenServicio.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    '    lrptOrdenServicio.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    '    lobjOpcDisco.DiskFileName = lstrRutaFile
    '    lrptOrdenServicio.ExportOptions.DestinationOptions = lobjOpcDisco
    '    lrptOrdenServicio.Export()
    '    lrptOrdenServicio.Close()
    '    lrptOrdenServicio.Dispose()
    '    lobjOpcDisco = Nothing

    '    '------------------------------------CUERPO--------------------------
    '    lstrCuerpoMensaje =
    '            "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
    '            "<P><FONT face='Verdana' size='2'><STRONG>" + Me.lblNombreProveedor.Text + "</STRONG></FONT></P>" + _
    '            "<P><FONT size='2'><FONT face='Verdana'>Por favor sirvase adjuntar el presente formato con su factura  de la Conformidad : <STRONG>" + _
    '            "<FONT style='BACKGROUND-COLOR: #ffff33'>" + Session("NroConformidad").ToString + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
    '            "<BR><BR>" + _
    '            "<P><FONT size='2'><FONT face='Verdana'>Perteneciente a la Orden de Servicio : <STRONG>" + _
    '            "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lblNroOrdenServicio.Text + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
    '            "<BR><BR>" + _
    '            "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
    '            "<BR>" + _
    '            "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
    '            "<BR>" + _
    '            "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
    '            "<BR>" + _
    '            "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
    '            "<P><FONT size='2'></FONT></P>" + _
    '            "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
    '            "Este correo ha sido generado automáticamente por el módulo de Cierre de Conformidades.<BR>" + _
    '            "Por favor no responder este correo.<BR>" + _
    '            "-------------------------------------------------------------------------------</FONT></P>"

    '    '--------------------------------------------CORREO TO----------------------------------
    '    lstrEmailTO = ""
    '    lstrEmailTO = lblEmail.Text

    '    '--------------------------------------------CORREO BCC----------------------------------
    '    lstrEmailBCC = ""
    '    lstrEmailBCC = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_CCO").ToString()

    '    '--------------------------------------------SUBJECT----------------------------------
    '    lstrSubject = ""
    '    lstrSubject = "[Cía. Ind. Nuevo Mundo] - Formato de conclusión de la Conformidad: " + Session("NroConformidad").ToString

    '    Dim lobjEnvio As New NuevoMundo.clsCorreo
    '    Dim boll As Boolean = lobjEnvio.ServicioEnvioCorreo("davico44.dg@gmail.com;sistemas@nuevomundosa.com", "luis.alanoca78@gmail.com", "luis_aj@hotmail.com", lstrSubject, lstrCuerpoMensaje, lstrRutaFile, lstrNombreFile)
    '    'Dim boll As Boolean = lobjEnvio.ServicioEnvioCorreo(lstrEmailTO, lstrEmailCopia, lstrEmailBCC, lstrSubject, lstrCuerpoMensaje, lstrRutaFile, lstrNombreFile)

    'End Sub
    Public Sub prc_Enviar_ConformidadOS(ByVal pstrDocumento As String)
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim ldtbDatosOC As New DataTable
        Dim lrptOrdenServicio As New rptFormatoConformidad '= New rptFormatoConformidad
        Dim ldtbError As DataTable
        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = ""
        Dim lstrNumeroOC As String = ""
        Dim lstrNombreFile As String = ""

        Dim lstrEmailDestino As String = ""
        Dim lstrEmailCopia As String = ""
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General
        Dim ldtbRuta As DataTable = Nothing
        Dim lstrBDUsuario As String = ""
        Dim lstrBDServidor As String = ""
        Dim lstrBDPassword As String = ""

        Dim lobjUtil As New NM_General.Util
        Dim lstrBDBaseDato As String = ""
        Dim lstrError As String = ""

        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS DE USUARIOS QUE RECIBEN LA ALERTA DE CIERRE DE O/S
        '-----------------------------------------------------------------------------------------
        Dim lobjAlertas As New clsFichaProv
        Dim objDTable As New DataTable
        objDTable = Nothing
        lstrEmailCopia = ""

        lobjAlertas.MostrarOpcionesSeleccion(objDTable, "33")
        If Not objDTable Is Nothing And objDTable.Rows.Count > 0 Then
            lstrEmailCopia = objDTable.Rows(0).Item("vch_email").ToString
            'lstrEmailCopia = "jcucho@nuevomundosa.com"
        Else
            lstrEmailCopia = ""
        End If

        lobjAlertas = Nothing
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------

        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        Dim lstrEmpresa As String = Session("@EMPRESA")
        lstrNumeroOC = pstrDocumento
        lstrNombreFile = "CONF_" & Session("NroConformidad").ToString & "_" & Strings.Format(Now(), "hhmmss") & ".pdf"
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        lstrRutaFile = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR").ToString
        If Trim(lstrRutaFile).Length <= 0 Then
            lblMsg.Text = "Error: Verificar ruta o archivo a adjuntar de la OS"
            Exit Sub
        End If

        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile

        ldtbRuta = Nothing
        lobjGeneral = Nothing
        '-----------------------------------------------------------------------------------------
        '--FINAL: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------

        '-----------------------------------------------------------------------------------------
        '--INICIO: CONVERTIR A PDF
        '-----------------------------------------------------------------------------------------
        Try

            If File.Exists(lstrRutaFile) = True Then
                File.Delete(lstrRutaFile)
            End If

            lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing
            lrptOrdenServicio.SetParameterValue(0, lstrEmpresa)
            lrptOrdenServicio.SetParameterValue(1, lstrNumeroOC)
            lrptOrdenServicio.SetParameterValue(2, Session("Item").ToString)
            lrptOrdenServicio.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenServicio.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            lrptOrdenServicio.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenServicio.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenServicio.Export()
            lrptOrdenServicio.Close()
            lrptOrdenServicio.Dispose()
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------

            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrCuerpoMensaje =
                "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
                "<P><FONT face='Verdana' size='2'><STRONG>" + Me.lblNombreProveedor.Text + "</STRONG></FONT></P>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Por favor sirvase adjuntar el presente formato con su factura  de la Conformidad : <STRONG>" + _
                "<FONT style='BACKGROUND-COLOR: #ffff33'>" + Session("NroConformidad").ToString + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
                "<BR><BR>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Perteneciente a la Orden de Servicio : <STRONG>" + _
                "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lblNroOrdenServicio.Text + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
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
                "Este correo ha sido generado automáticamente por el módulo de Cierre de Conformidades.<BR>" + _
                "Por favor no responder este correo.<BR>" + _
                "-------------------------------------------------------------------------------</FONT></P>"

            Dim ServidorSMTP As New System.Net.Mail.SmtpClient
            Dim lobjmailMsg As New System.Net.Mail.MailMessage()
            Dim lobjAdjunto As System.Net.Mail.Attachment
            Dim cd As System.Net.Mime.ContentDisposition

            If lstrNombreFile.Length > 0 Then
                lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
                cd = lobjAdjunto.ContentDisposition
                cd.FileName = lstrNombreFile
            End If

            Dim lstrCC_arreglo() As String = lstrEmailCopia.Split(";")
            For lintIndice = 0 To lstrCC_arreglo.Length - 1
                If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then
                    lobjmailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                End If
            Next

            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With lobjmailMsg
                '.From = New System.Net.Mail.MailAddress("Nuevo Mundo - Compras<compras@nuevomundosa.com>")
                .From = New System.Net.Mail.MailAddress(user)
                .To.Add(Trim(lblEmail.Text))
                '.To.Add("dgamarra@nuevomundosa.com")
                '.Subject = "[Cía. Ind. Nuevo Mundo] - Formato de conclusión de Orden de Servicio número: " + lstrNumeroOC
                .Subject = "[Cía. Ind. Nuevo Mundo] - Formato de conclusión de la Conformidad: " + Session("NroConformidad").ToString
                .Body = lstrCuerpoMensaje
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
                If lstrNombreFile.Length > 0 Then
                    .Attachments.Add(lobjAdjunto)
                End If
            End With

            'With ServidorSMTP
            '    .Port = 25
            '    .Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ServidorSMTP").ToString() '172.16.0.10
            '    .EnableSsl = False
            'End With

            'Dim ServidorSMTP As New System.Net.Mail.SmtpClient
            ServidorSMTP.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            ServidorSMTP.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            ServidorSMTP.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            ServidorSMTP.Credentials = userCredential

            ServidorSMTP.Send(lobjmailMsg)
            ServidorSMTP = Nothing
            lobjAdjunto.Dispose()
            'ServidorSMTP.Dispose()


            If File.Exists(lstrRutaFile) Then
                File.Delete(lstrRutaFile)
            End If

            ' ACTUALIZAR METADATA DE ENVIO EN OC
            lstrUsuario = Session("@Usuario")
            ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)

        Catch ex As Exception
            'lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
            lstrError = ex.Message
        End Try

        If Trim(lstrError).Length > 0 Then
            EnviaCorreoError(lstrError)
            Me.ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
        End If

        lrptOrdenServicio = Nothing
        lobjOpcDisco = Nothing

    End Sub
    Public Sub prc_Enviar_ConformidadOS_V2(ByVal pstrDocumento As String)
        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String
        Dim lstrRutaFile_SQL As String
        Dim lstrFile_SQL As String




        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim ldtbDatosOC As New DataTable
        Dim lrptOrdenServicio As New rptFormatoConformidad '= New rptFormatoConformidad
        Dim ldtbError As DataTable
        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = ""
        Dim lstrNumeroOC As String = ""
        Dim lstrNombreFile As String = ""

        Dim lstrEmailDestino As String = ""
        Dim lstrEmailCopia As String = ""
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General
        Dim ldtbRuta As DataTable = Nothing
        Dim lstrBDUsuario As String = ""
        Dim lstrBDServidor As String = ""
        Dim lstrBDPassword As String = ""

        Dim lobjUtil As New NM_General.Util
        Dim lstrBDBaseDato As String = ""
        Dim lstrError As String = ""

        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS DE USUARIOS QUE RECIBEN LA ALERTA DE CIERRE DE O/S
        '-----------------------------------------------------------------------------------------
        Dim lobjAlertas As New clsFichaProv
        Dim objDTable As New DataTable
        objDTable = Nothing
        lstrEmailCopia = ""

        lobjAlertas.MostrarOpcionesSeleccion(objDTable, "33")
        If Not objDTable Is Nothing And objDTable.Rows.Count > 0 Then
            lstrEmailCopia = objDTable.Rows(0).Item("vch_email").ToString
            'lstrEmailCopia = "jcucho@nuevomundosa.com"
        Else
            lstrEmailCopia = ""
        End If

        lobjAlertas = Nothing
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------

        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        Dim lstrEmpresa As String = Session("@EMPRESA")
        lstrNumeroOC = pstrDocumento
        lstrNombreFile = "CONF_" & Session("NroConformidad").ToString & "_" & Strings.Format(Now(), "hhmmss") & ".pdf"
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        lstrRutaFile = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR").ToString
        lstrRutaFile_SQL = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR_SQL").ToString
        If Trim(lstrRutaFile).Length <= 0 Then
            lblMsg.Text = "Error: Verificar ruta o archivo a adjuntar de la OS"
            Exit Sub
        End If

        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile
        lstrFile_SQL = lstrNombreFile

        ldtbRuta = Nothing
        lobjGeneral = Nothing
        '-----------------------------------------------------------------------------------------
        '--FINAL: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------

        '-----------------------------------------------------------------------------------------
        '--INICIO: CONVERTIR A PDF
        '-----------------------------------------------------------------------------------------
        Try

            If File.Exists(lstrRutaFile) = True Then
                File.Delete(lstrRutaFile)
            End If

            lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing
            lrptOrdenServicio.SetParameterValue(0, lstrEmpresa)
            lrptOrdenServicio.SetParameterValue(1, lstrNumeroOC)
            lrptOrdenServicio.SetParameterValue(2, Session("Item").ToString)
            lrptOrdenServicio.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenServicio.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            lrptOrdenServicio.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenServicio.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenServicio.Export()
            lrptOrdenServicio.Close()
            lrptOrdenServicio.Dispose()
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------

            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrMailTO = IIf(Trim(lblEmail.Text) = "", "sistemas@nuevomundosa.com", Trim(lblEmail.Text))
            lstrMailCC = lstrEmailCopia
            lstrMailBCC = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_CCO").ToString()

            lstrMailSubject = "[Cía. Ind. Nuevo Mundo] - Formato de conclusión de la Conformidad: " + Session("NroConformidad").ToString

            lstrMailBody =
                "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
                "<P><FONT face='Verdana' size='2'><STRONG>" + Me.lblNombreProveedor.Text + "</STRONG></FONT></P>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Por favor sirvase adjuntar el presente formato con su factura  de la Conformidad : <STRONG>" + _
                "<FONT style='BACKGROUND-COLOR: #ffff33'>" + Session("NroConformidad").ToString + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
                "<BR><BR>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Perteneciente a la Orden de Servicio : <STRONG>" + _
                "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lblNroOrdenServicio.Text + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
                "<BR><BR>" + _
                "<P><FONT size='2'><FONT face='Verdana'>No olvide ingresar su factura en el portal:<A href='http://200.60.99.228/EnlaceNM_Extranet/Procesos/frmLoginExtranet.aspx'><FONT face='Verdana' size='2'>http://200.60.99.228/EnlaceNM_Extranet/Procesos/frmLoginExtranet.aspx</FONT></A>" + _
                "<BR><BR>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Recordar por favor que NM se guía de la fecha del registro de factura en el portal para programar su pago, es importante que ésta sea registrada una vez tenga la conformidad del servicio y/o la nota de ingreso del producto. Gracias.:" + _
                "<BR><BR>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Si desea conocer su usuario y clave, comunicarse con: compras@nuevomundosa.com:" + _
                "<BR><BR>" + _
                "<P><FONT size='2'><FONT face='Verdana'>Para nuevos usuarios, descárgalo aquí:<A href='http://www.nuevomundosa.com/documento/Manual_Registro_de_Factura_V2.pdf'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com/documento/Manual_Registro_de_Factura_V2.pdf</FONT></A>" + _
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
                "Este correo ha sido generado automáticamente por el módulo de Cierre de Conformidades.<BR>" + _
                "Por favor no responder este correo.<BR>" + _
                "-------------------------------------------------------------------------------</FONT></P>"

            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody, lstrRutaFile_SQL, lstrFile_SQL)
            'objCorreo.ServicioEnvioCorreo("davico44.dg@gmail.com;sistemas@nuevomundosa.com", "luis.alanoca78@gmail.com", "luis_aj@hotmail.com", lstrMailSubject, lstrMailBody, lstrRutaFile_SQL, lstrFile_SQL)

            If File.Exists(lstrRutaFile) Then
                File.Delete(lstrRutaFile)
            End If

            ' ACTUALIZAR METADATA DE ENVIO EN OC
            lstrUsuario = Session("@Usuario")
            ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)

        Catch ex As Exception
            'lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
            lstrError = ex.Message
        End Try

        If Trim(lstrError).Length > 0 Then
            EnviaCorreoError(lstrError)
            Me.ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
        End If

        lrptOrdenServicio = Nothing
        lobjOpcDisco = Nothing

    End Sub
    Private Sub EnviaCorreoError(ByVal strMensaje As String)
        Dim lstrCuerpoMensaje As String = ""
        Dim mailMsg As System.Net.Mail.MailMessage

        lstrCuerpoMensaje = strMensaje
        mailMsg = New System.Net.Mail.MailMessage()

        Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
        Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
        Dim userCredential As New System.Net.NetworkCredential(user, password)

        With mailMsg
            '.From = New System.Net.Mail.MailAddress("<compras@nuevomundosa.com>")
            .From = New System.Net.Mail.MailAddress(user)
            .To.Add("sistemas@nuevomundosa.com")
            .Subject = "Ocurrio un error en envio de confirmacion"
            .IsBodyHtml = True
            .Body = lstrCuerpoMensaje
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

    End Sub

    Private Sub btnAnular_Click(sender As Object, e As System.EventArgs) Handles btnAnular.Click
        prcActualizarEstado("1")
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
    End Sub
End Class