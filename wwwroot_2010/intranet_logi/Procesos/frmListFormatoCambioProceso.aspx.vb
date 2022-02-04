Imports NM_General
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class frmListFormatoCambioProceso
    Inherits System.Web.UI.Page

#Region "Funciones"

    Sub fn_ObtenerTrabajadoresSPL2()
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_obtenerTrabajadoresSPL2("")

        For Each lobjRow As DataRow In ldtResponse.Rows

            fn_generarFormatoPDF(lobjRow.Item("CO_TRAB"),
                                 lobjRow.Item("NO_TRAB"),
                                 lobjRow.Item("DE_PUES_TRAB"),
                                 lobjRow.Item("NU_DOCU_IDEN"),
                                 lobjRow.Item("HORAS_COMPENSADAS"),
                                 lobjRow.Item("HORAS_PENDIENTES"))

        Next
    End Sub

    Sub fn_ObtenerTrabajadoresSPL()
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_obtenerTrabajadoresSPL("")

        For Each lobjRow As DataRow In ldtResponse.Rows

            fn_generarFormatoPDF(lobjRow.Item("CO_TRAB"),
                                 lobjRow.Item("NO_TRAB"),
                                 lobjRow.Item("DE_PUES_TRAB"),
                                 "", "", "")

        Next
    End Sub

    Sub fn_ObtenerTrabajadoresCTS()
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_obtenerTrabajadoresCTS("")

        For Each lobjRow As DataRow In ldtResponse.Rows

            fn_generarFormatoPDFCTS(lobjRow.Item("VCH_CO_TRAB"),
                                    lobjRow.Item("VCH_BAN_TRAB"),
                                    lobjRow.Item("VCH_NO_TRAB"),
                                    lobjRow.Item("VCH_NU_DNI"),
                                    Convert.ToDouble(lobjRow.Item("NUM_SU_TRAB")))

        Next
    End Sub

    Sub fn_generarFormatoPDFCTS(ByVal pvchCodTra As String,
                                ByVal pvchNomBanco As String,
                                ByVal pvchNomTra As String,
                                ByVal pvchNumDni As String,
                                ByVal pnumSuelTra As Double)
        Dim MyReportViewer As New ReportViewer
        'Dim ReportServerCredentials As IReportServerCredentials
        'ReportServerCredentials = New ReportViewerCredentials("nmsic", "Asmrp.159", "NUEVOMUNDOSA")

        File.Delete(Server.MapPath("~/Procesos/Formatos/CTS") + "/" + pvchCodTra + ".pdf")

        MyReportViewer.ProcessingMode = ProcessingMode.Remote
        'MyReportViewer.ServerReport.ReportServerCredentials = ReportServerCredentials
        MyReportViewer.ServerReport.ReportServerUrl = New Uri("http://mundodesa02:8081/reportserver")
        MyReportViewer.ServerReport.ReportPath = "/Administrativo_NuevoMundo/Gerencia/gg_formato_retiro_cts"
        'MyReportViewer.ServerReport.DisplayName = "gg_formato_cambio_proceso"
        MyReportViewer.ServerReport.Refresh()
        Dim reportParameterCollection(3) As ReportParameter
        reportParameterCollection(0) = New ReportParameter
        reportParameterCollection(0).Name = "vchNomBanco"
        reportParameterCollection(0).Values.Add(pvchNomBanco)
        reportParameterCollection(1) = New ReportParameter
        reportParameterCollection(1).Name = "vchNomTrab"
        reportParameterCollection(1).Values.Add(pvchNomTra)
        reportParameterCollection(2) = New ReportParameter
        reportParameterCollection(2).Name = "vchNumDni"
        reportParameterCollection(2).Values.Add(pvchNumDni)
        reportParameterCollection(3) = New ReportParameter
        reportParameterCollection(3).Name = "vchSuelTrab"
        reportParameterCollection(3).Values.Add(pnumSuelTra)
        MyReportViewer.ServerReport.SetParameters(reportParameterCollection)
        Dim warnings() As Warning
        Dim streamids() As String
        Dim mimeType, encoding, extension, deviceInfo As String
        deviceInfo = "True"

        Dim bytes() As Byte
        bytes = MyReportViewer.ServerReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

        'Response.Buffer = True
        'Response.Clear()
        'Response.ContentType = mimeType
        'Response.AddHeader("Content-Disposition", "attachment; filename=" + extension)
        'Response.AddHeader("content-disposition", "inline; filename=myfile." + extension)
        'Response.BinaryWrite(bytes)

        Dim pdfPath As String = Server.MapPath("~/Procesos/Formatos/CTS") + "/" + pvchCodTra + ".pdf"
        'Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
        'pdfFile.Write(bytes, 0, bytes.Length)
        'pdfFile.Close()

        System.IO.File.WriteAllBytes(pdfPath, bytes)

        'fn_enviarCorreoFormatos("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoCambioProceso_" + pintCodGenFor.ToString() + ".pdf")
        'Response.Flush()
        'Response.End()

        'Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub

    Sub fn_generarFormatoPDF(ByVal pvchCodTra As String,
                             ByVal pvchNomTra As String,
                             ByVal pvchPuesTra As String,
                             ByVal pvchDocuTra As String,
                             ByVal pvchHorasComp As String,
                             ByVal pvchHorasPend As String)

        Dim MyReportViewer As New ReportViewer
        'Dim ReportServerCredentials As IReportServerCredentials
        'ReportServerCredentials = New ReportViewerCredentials("nmsic", "Asmrp.159", "NUEVOMUNDOSA")

        File.Delete(Server.MapPath("~/Procesos/Formatos/SPL2") + "/" + pvchCodTra + ".pdf")

        MyReportViewer.ProcessingMode = ProcessingMode.Remote
        'MyReportViewer.ServerReport.ReportServerCredentials = ReportServerCredentials
        MyReportViewer.ServerReport.ReportServerUrl = New Uri("http://mundodesa02:8081/reportserver")
        MyReportViewer.ServerReport.ReportPath = "/Administrativo_NuevoMundo/Gerencia/gg_formato_dias_x_compensar" 'gg_formato_reduc_sueldo"
        'MyReportViewer.ServerReport.DisplayName = "gg_formato_cambio_proceso"
        MyReportViewer.ServerReport.Refresh()
        Dim reportParameterCollection(3) As ReportParameter
        reportParameterCollection(0) = New ReportParameter
        reportParameterCollection(0).Name = "nombre"
        reportParameterCollection(0).Values.Add(pvchNomTra)
        reportParameterCollection(1) = New ReportParameter
        reportParameterCollection(1).Name = "dni"
        reportParameterCollection(1).Values.Add(pvchPuesTra)

        reportParameterCollection(2) = New ReportParameter
        reportParameterCollection(2).Name = "compensado"
        reportParameterCollection(2).Values.Add(pvchHorasComp)

        reportParameterCollection(3) = New ReportParameter
        reportParameterCollection(3).Name = "pendiente"
        reportParameterCollection(3).Values.Add(pvchHorasPend)

        MyReportViewer.ServerReport.SetParameters(reportParameterCollection)
        Dim warnings() As Warning
        Dim streamids() As String
        Dim mimeType, encoding, extension, deviceInfo As String
        deviceInfo = "True"

        Dim bytes() As Byte
        bytes = MyReportViewer.ServerReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

        'Response.Buffer = True
        'Response.Clear()
        'Response.ContentType = mimeType
        'Response.AddHeader("Content-Disposition", "attachment; filename=" + extension)
        'Response.AddHeader("content-disposition", "inline; filename=myfile." + extension)
        'Response.BinaryWrite(bytes)

        Dim pdfPath As String = Server.MapPath("~/Procesos/Formatos/SPL2") + "/" + pvchCodTra + ".pdf"
        'Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
        'pdfFile.Write(bytes, 0, bytes.Length)
        'pdfFile.Close()

        System.IO.File.WriteAllBytes(pdfPath, bytes)

        'fn_enviarCorreoFormatos("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoCambioProceso_" + pintCodGenFor.ToString() + ".pdf")
        'Response.Flush()
        'Response.End()

        'Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub

    Sub fn_listarFormatosCambioProceso()
        Dim lstrCodArea As String = ""
        Dim lstrCodMaquina As String = ""
        Dim lstrCodResp As String = ""
        Dim lstrFechaIni As String = ""
        Dim lstrFechaFin As String = ""
        Dim lstrCodSoli As String = ""
        Dim lstrEstado As String = ""
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        If Not txtFechaIni.Text.Trim().Equals("") Then
            If txtFechaFin.Text.Trim().Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha fin.');</script>")
                Exit Sub
            End If
        End If

        If Not txtFechaFin.Text.Trim().Equals("") Then
            If txtFechaIni.Text.Trim().Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha inicio.');</script>")
                Exit Sub
            End If
        End If


        lstrCodArea = txtArea.Text.Trim().ToUpper()
        lstrCodMaquina = txtMaquina.Text.Trim().ToUpper()
        lstrCodResp = txtResponsable.Text.Trim().ToUpper()
        If txtFechaIni.Text.Trim().Equals("") Then
            lstrFechaIni = ""
        Else
            lstrFechaIni = Convert.ToDateTime(txtFechaIni.Text.Trim()).ToString("yyyyMMdd")
        End If

        If txtFechaFin.Text.Trim().Equals("") Then
            lstrFechaFin = ""
        Else
            lstrFechaFin = Convert.ToDateTime(txtFechaFin.Text.Trim()).ToString("yyyyMMdd")
        End If
        lstrCodSoli = txtSolicitante.Text.Trim().ToUpper()
        lstrEstado = ddlEstado.SelectedValue().ToString()

        ldtResponse = lobjGerencia.fn_listarFormatoCP(lstrCodArea,
                                                      lstrCodMaquina,
                                                      lstrCodResp,
                                                      lstrCodSoli,
                                                      lstrEstado,
                                                      lstrFechaIni,
                                                      lstrFechaFin)

        If ldtResponse.Rows.Count > 0 Then
            gvFormatoCP.DataSource = ldtResponse
            gvFormatoCP.DataBind()
        Else
            gvFormatoCP.DataSource = Nothing
            gvFormatoCP.DataBind()
        End If

    End Sub

#End Region

#Region "Métodos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "JRUIZS"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                    Dim objRequest As New BLITZ_LOCK.clsRequest
                    Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
                End If

                If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                    Response.Redirect("../../intranet/finsesion.htm")
                End If
            End If

            fn_listarFormatosCambioProceso()
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        fn_listarFormatosCambioProceso()
    End Sub

#End Region

    Protected Sub gvFormatoCP_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gvFormatoCP.ItemCommand
        If e.CommandName.ToString().Equals("Visualizar") Then
            Response.Redirect("frmListadoCambioProceso.aspx?pidCodGenFormato=" + e.CommandArgument)
        End If

        If e.CommandName.ToString().Equals("VerPDF") Then
            Dim strURL As String = ""
            Dim strPath As String = ""
            Dim strScript As String = ""
            Dim strCodGenerado As String
            Dim strCodFormato As String

            strCodGenerado = e.CommandArgument
            strCodFormato = "1"

            If strCodGenerado.Length > 0 Then
                'CAMBIO DG INI
                'strPath = "%2fNM_Reportes%2f"
                'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
                'strURL = strURL + "logistica_InventarioDiario"
                strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
                strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloGerencia")
                strURL = strURL + strPath + "gg_formato_cambio_proceso"
                'CAMBIO DG FIN
                strURL = strURL + "&pintCodGenerado=" + strCodGenerado
                strURL = strURL + "&pintCodFormato=" + strCodFormato

                strURL = strURL + "&rc:Command=Render"
                strURL = strURL + "&rc:Toolbar=true"

                strScript = "fMostrarReporte('" & strURL & "');"
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
            Else
                'lblMensaje.Text = "Debe ingresar codigo de inventario para la consulta."
            End If
        End If
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Response.Redirect("frmListadoCambioProceso.aspx")
    End Sub

    'Protected Sub btnGenero_Click(sender As Object, e As EventArgs) Handles btnGenero.Click
    '    fn_ObtenerTrabajadoresSPL2()
    'End Sub
End Class