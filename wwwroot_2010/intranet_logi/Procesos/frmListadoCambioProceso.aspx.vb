Imports NM_General
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class frmListadoCambioProceso
    Inherits System.Web.UI.Page

    Dim gbolValidar As Boolean = True

#Region "Funciones"

    Public Class ReportViewerCredentials
        Implements IReportServerCredentials
        Private _userName As String
        Private _password As String
        Private _domain As String


        Public Sub New(ByVal userName As String, ByVal password As String, ByVal domain As String)
            _userName = userName
            _password = password
            _domain = domain
        End Sub


        Public ReadOnly Property ImpersonationUser() As System.Security.Principal.WindowsIdentity Implements Microsoft.Reporting.WebForms.IReportServerCredentials.ImpersonationUser
            Get
                Return Nothing
            End Get
        End Property


        Public ReadOnly Property NetworkCredentials() As System.Net.ICredentials Implements Microsoft.Reporting.WebForms.IReportServerCredentials.NetworkCredentials
            Get
                Return New Net.NetworkCredential(_userName, _password, _domain)
            End Get
        End Property


        Public Function GetFormsCredentials(ByRef authCookie As System.Net.Cookie, ByRef userName As String, ByRef password As String, ByRef authority As String) As Boolean Implements Microsoft.Reporting.WebForms.IReportServerCredentials.GetFormsCredentials
            userName = _userName
            password = _password
            authority = _domain
            Return Nothing
        End Function


    End Class

    Sub fn_enviarCorreoFormatos(ByVal pstrRuta As String)

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_enviarCorreoFormatos("FORMATO CAMBIO DE PROCESO", pstrRuta)

    End Sub
    Public Function fn_enviarCorreoFormatos_V2(ByVal pstrRuta As String, ByVal intCodFormulario As Int32, ByVal intCodFormato As Integer) As DataTable

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.enviarCorreoFormatos_V2("", pstrRuta, intCodFormulario, intCodFormato)
        Return ldtResponse
    End Function

    Sub fn_generarFormatoPDF(ByVal pintCodGenFor As Int32, ByVal pintCodFormato As Integer)
        Dim MyReportViewer As New ReportViewer
        Dim ReportServerCredentials As IReportServerCredentials
        ReportServerCredentials = New ReportViewerCredentials("nmsic", "Asmrp.159", "NUEVOMUNDOSA")

        File.Delete(Server.MapPath("~/Procesos/Formatos/PDF") + "/FormatoCambioProceso_" + pintCodGenFor.ToString() + ".pdf")

        MyReportViewer.ProcessingMode = ProcessingMode.Remote
        MyReportViewer.ServerReport.ReportServerCredentials = ReportServerCredentials
        MyReportViewer.ServerReport.ReportServerUrl = New Uri("http://mundodesa02:8081/reportserver")
        MyReportViewer.ServerReport.ReportPath = "/Administrativo_NuevoMundo/Gerencia/gg_formato_cambio_proceso"
        'MyReportViewer.ServerReport.DisplayName = "gg_formato_cambio_proceso"
        MyReportViewer.ServerReport.Refresh()
        Dim reportParameterCollection(1) As ReportParameter
        reportParameterCollection(0) = New ReportParameter
        reportParameterCollection(0).Name = "pintCodGenerado"
        reportParameterCollection(0).Values.Add(pintCodGenFor)
        reportParameterCollection(1) = New ReportParameter
        reportParameterCollection(1).Name = "pintCodFormato"
        reportParameterCollection(1).Values.Add(1)
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

        Dim pdfPath As String = Server.MapPath("~/Procesos/Formatos/PDF") + "/FormatoCambioProceso_" + pintCodGenFor.ToString() + "." + extension
        'Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
        'pdfFile.Write(bytes, 0, bytes.Length)
        'pdfFile.Close()

        System.IO.File.WriteAllBytes(pdfPath, bytes)
        'fn_enviarCorreoFormatos("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoCambioProceso_" + pintCodGenFor.ToString() + ".pdf")
        Dim dt As DataTable = fn_enviarCorreoFormatos_V2("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoCambioProceso_" + pintCodGenFor.ToString() + ".pdf", pintCodGenFor, pintCodFormato)
        'Response.Flush()
        'Response.End()

        'Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub

    Sub fn_aprobarFormato(ByVal pintCodGenFor As Int32, ByVal pintCodFormato As Integer)

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_aprobarFormatos("U",
                                                      pintCodGenFor,
                                                      1,
                                                      Session("@USUARIO").ToString())

        If ldtResponse.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("200") Then
            'fn_ObtenerExisteFormato(pintCodGenFor)
            fn_generarFormatoPDF(pintCodGenFor, pintCodFormato)
            Response.Redirect("frmListFormatoCambioProceso.aspx")
        ElseIf ldtResponse.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("300") Then
            'fn_ObtenerExisteFormato(pintCodGenFor)
            Response.Redirect("frmListFormatoCambioProceso.aspx")
        End If

        'Response.Redirect("frmListFormatoCambioProceso.aspx")
    End Sub

    Sub fn_bloquearControles()
        ddlArea.Enabled = False
        txtMaquina.Enabled = False
        'btnSolicitante.Style.Add("visibility", "hidden")
        'CAMBIO DG - RESPONSABLE - INI
        'ddlResponsable.Enabled = False
        txtResponsable.Enabled = False
        'CAMBIO DG - RESPONSABLE - FIN
        txtFecInicio.Enabled = False
        'ibtnCalendar1.Style.Add("visibility", "hidden")
        txtObjCambio.Enabled = False
        For Each lobjRow As GridViewRow In gvBloque1.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)
            ltxtValor.Enabled = False
        Next
        gvBloque1.Columns(1).Visible = False
        gvBloque1.FooterRow.Visible = False
        txtConsRiesgo.Enabled = False
        ibtAgregarProd.Visible = False
        For Each lobjRow As GridViewRow In gvProduc1.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)
            ltxtValor.Enabled = False
            
        Next
        gvProduc1.Columns(2).Visible = False
        gvProduc1.FooterRow.Visible = False
        For Each lobjRow As GridViewRow In gvProduc2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
            ltxtValor.Enabled = False
        Next
        gvProduc2.Columns(2).Visible = False
        gvProduc2.FooterRow.Visible = False
        ibtMenos2.Visible = False
        For Each lobjRow As GridViewRow In gvProduc3.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)
            ltxtValor.Enabled = False
        Next
        gvProduc3.Columns(2).Visible = False
        gvProduc3.FooterRow.Visible = False
        ibtMenos3.Visible = False
        txtFecConc.Enabled = False
        ibtCalendar2.Style.Add("visibility", "hidden")
        txtDescConcl.Enabled = False
        gvFirmas.Columns(3).Visible = False
        gvFirmas.FooterRow.Visible = False
    End Sub

    Sub fn_anularFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_solicitarAprobacion("01",
                                                          pintCodGenFor,
                                                          1,
                                                          "ANU")

        Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub

    Sub fn_SolicitarAprobacion(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        gbolValidar = True

        If ddlArea.SelectedValue.Equals("0") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Área.');</script>")
            Exit Sub
        End If
        'CAMBIO DG - OBTENER RESPONSABLE - INI
        'If ddlResponsable.SelectedValue.Equals("0") Then
        '    gbolValidar = False
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Responsable.');</script>")
        '    Exit Sub
        'End If
        If txtResponsable.Text.Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Responsable.');</script>")
            Exit Sub
        End If
        'CAMBIO DG - OBTENER RESPONSABLE - INI
        If txtFecInicio.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha de inicio.');</script>")
            Exit Sub
        End If

        'If Convert.ToDateTime(txtFecInicio.Text.Trim()) <= Date.Now Then
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha de inicio..');</script>")
        '    Exit Sub
        'End If
        Try
            Convert.ToDateTime(txtFecInicio.Text.Trim())
        Catch ex As Exception
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha de inicio válida.');</script>")
            Exit Sub
        End Try

        If txtObjCambio.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese objetivo del cambio.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvBloque1.Rows
            Dim ltxtValor As New TextBox
            'Dim lbolValidar As Boolean = False
            ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                gbolValidar = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
                Exit Sub
            End If

            'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
            'Exit Sub

        Next

        If txtConsRiesgo.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese detalle de consideraciones.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvProduc1.Rows
            Dim ltxtValor As New TextBox
            'Dim lbolValidar As Boolean = False
            ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                gbolValidar = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
                Exit Sub
            End If

            'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
            'Exit Sub

        Next

        If hdfPanel2.Value.Equals("S") Then
            For Each lobjRow As GridViewRow In gvProduc2.Rows
                Dim ltxtValor As New TextBox
                'Dim lbolValidar As Boolean = False
                ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

                If ltxtValor.Text.Trim().Equals("") Then
                    gbolValidar = False
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
                    Exit Sub
                End If

                'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
                'Exit Sub

            Next
        End If

        If hdfPanel3.Value.Equals("S") Then
            For Each lobjRow As GridViewRow In gvProduc3.Rows
                Dim ltxtValor As New TextBox
                'Dim lbolValidar As Boolean = False
                ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

                If ltxtValor.Text.Trim().Equals("") Then
                    gbolValidar = False
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
                    Exit Sub
                End If

                'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
                'Exit Sub

            Next
        End If

        If txtFecConc.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha de conclusiones.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFecConc.Text.Trim())
        Catch ex As Exception
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha de conclusiones válida.');</script>")
            Exit Sub
        End Try

        If txtDescConcl.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la conclusión.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvFirmas.Rows
            Dim ltxtValor As New TextBox
            'Dim lbolValidar As Boolean = False
            ltxtValor = CType(gvFirmas.Rows(lobjRow.RowIndex).FindControl("txtCampoFirma"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                gbolValidar = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese firmas.');</script>")
                Exit Sub
            End If

            'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese firmas.');</script>")
            'Exit Sub

        Next

        ldtResponse = lobjGerencia.fn_solicitarAprobacion("01",
                                                          pintCodGenFor,
                                                          1,
                                                          "SOL")

        Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub

    Sub fn_ObtenerExisteFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim ldtResponseApro As DataTable
        Dim lintContadorP1 = 0
        Dim lintContadorP2 = 0

        ldsResponse = lobjGerencia.fn_ObtieneFormato("01",
                                                     pintCodGenFor,
                                                     1,
                                                     1)
        Session("ldtResponse") = ldsResponse.Tables(0)
        hdfIdGenFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        'hdfCodMaquina.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_MAQU").ToString()
        txtMaquina.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NOM_MAQUINA").ToString()
        ddlArea.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_AREA").ToString()
        'CAMBIO DG - OBTENER RESPONSABLE - INI
        'ddlResponsable.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtResponsable.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        'CAMBIO DG - OBTENER RESPONSABLE - FIN
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_INI").ToString()
        txtObjCambio.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBJ_CAMB").ToString()
        txtConsRiesgo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_CONS").ToString()
        txtFecConc.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_CONC").ToString()
        txtDescConcl.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CONCLUS").ToString()
        lblficha.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEINFORME").ToString()
        gvBloque1.DataSource = ldsResponse.Tables(1)
        gvBloque1.DataBind()

        gvProduc1.DataSource = ldsResponse.Tables(2)
        gvProduc1.DataBind()


        gvProduc2.DataSource = ldsResponse.Tables(3)
        gvProduc2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(5)
        gvFirmas.DataBind()

        For Each lobjRow As GridViewRow In gvProduc2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                lintContadorP1 = lintContadorP1 + 1
            End If
        Next

        If lintContadorP1 = gvProduc2.Rows.Count Then
            hdfPanel2.Value = ""
            pnlProduccion2.Style.Add("display", "none")
        Else
            hdfPanel2.Value = "S"
            pnlProduccion2.Style.Add("display", "block")
        End If

        gvProduc3.DataSource = ldsResponse.Tables(4)
        gvProduc3.DataBind()

        For Each lobjRow As GridViewRow In gvProduc3.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                lintContadorP2 = lintContadorP2 + 1
            End If
        Next

        If lintContadorP2 = gvProduc3.Rows.Count Then
            hdfPanel3.Value = ""
            pnlProduccion3.Style.Add("display", "none")
        Else
            hdfPanel3.Value = "S"
            pnlProduccion3.Style.Add("display", "block")
        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_GEN_FORMATO").ToString().Equals("ACT") Then

            If ldsResponse.Tables(0).Rows(0).Item("VCH_USU_CREA").ToString() = Session("@USUARIO").ToString() Then
                btnSolicitar.Visible = True
                btnGuardar.Visible = True
                btnAnular.Visible = True
            Else
                btnSolicitar.Visible = False
                btnGuardar.Visible = False
                btnAnular.Visible = False
            End If
            btnAprobar.Visible = False

        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_GEN_FORMATO").ToString().Equals("SOL") Then
            btnGuardar.Visible = False
            btnSolicitar.Visible = False
            'btnAprobar.Visible = True
            btnAnular.Visible = False
            fn_bloquearControles()
            gvFirmas.Columns(2).Visible = True

            ldtResponseApro = lobjGerencia.fn_aprobarFormatos("B",
                                                              pintCodGenFor,
                                                              1,
                                                              Session("@USUARIO").ToString())

            If ldtResponseApro.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("200") Then
                btnAprobar.Visible = True
            Else
                btnAprobar.Visible = False
            End If

            'If Session("@USUARIO").ToString().Equals("AAMPUERP") Or
            '    Session("@USUARIO").ToString().Equals("DARWIN") Then
            '    btnAprobar.Visible = True
            'End If

        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_GEN_FORMATO").ToString().Equals("ANU") Then
            btnGuardar.Visible = False
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
            fn_bloquearControles()
        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_GEN_FORMATO").ToString().Equals("APR") Then
            btnGuardar.Visible = False
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
            fn_bloquearControles()
            gvFirmas.Columns(2).Visible = True
        End If

    End Sub

    Sub fn_CargarListas()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet

        ldsResponse = lobjGerencia.fn_CargarListas("")


        ddlArea.DataTextField = "DE_AREA"
        ddlArea.DataValueField = "CO_AREA"
        ddlArea.DataSource = ldsResponse.Tables(0)
        ddlArea.DataBind()

        'CAMBIO DG - RESPONSABLE POR BUSQUEDA - INI 
        'ddlResponsable.DataTextField = "NO_USUA"
        'ddlResponsable.DataValueField = "var_Dato"
        'ddlResponsable.DataSource = ldsResponse.Tables(1)
        'ddlResponsable.DataBind()
        'CAMBIO DG -RESPONSABLE POR BUSQUEDA - FIN
    End Sub

    Public Function fn_CargarFirmas() As DataTable

        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim ldtResponse As DataTable

        ldsResponse = lobjGerencia.fn_CargarListas("")

        ldtResponse = ldsResponse.Tables(1)

        Return ldtResponse
    End Function
    Sub fn_ObtenerParametrosFormulario_Nuevo()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim lintContadorP1 = 0
        Dim lintContadorP2 = 0

        ldsResponse = lobjGerencia.fn_GeneraFormato("01",
                                                    0,
                                                    1,
                                                    Session("@USUARIO").ToString())

        hdfIdGenFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        'hdfCodMaquina.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_MAQU").ToString()
        txtMaquina.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NOM_MAQUINA").ToString()
        ddlArea.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_AREA").ToString()
        'CAMBIO DG  - OBTENGO NOMBRE DEL RESPONSABLE - INI
        'ddlResponsable.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtResponsable.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        'CAMBIO DG  - OBTENGO NOMBRE DEL RESPONSABLE - FIN
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_INI").ToString()
        txtObjCambio.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBJ_CAMB").ToString()
        txtConsRiesgo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_CONS").ToString()
        txtFecConc.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_CONC").ToString()
        txtDescConcl.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CONCLUS").ToString()
        txtFecInicio.Text = Date.Now.ToString("dd/MM/yyyy")
        lblficha.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEINFORME").ToString()
        gvBloque1.DataSource = ldsResponse.Tables(1)
        gvBloque1.DataBind()

        gvProduc1.DataSource = ldsResponse.Tables(2)
        gvProduc1.DataBind()


        gvProduc2.DataSource = ldsResponse.Tables(3)
        gvProduc2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(5)
        gvFirmas.DataBind()

        For Each lobjRow As GridViewRow In gvProduc2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                lintContadorP1 = lintContadorP1 + 1
            End If
        Next

        If lintContadorP1 = gvProduc2.Rows.Count Then
            hdfPanel2.Value = ""
            pnlProduccion2.Style.Add("display", "none")
        Else
            hdfPanel2.Value = "S"
            pnlProduccion2.Style.Add("display", "block")
        End If

        gvProduc3.DataSource = ldsResponse.Tables(4)
        gvProduc3.DataBind()

        For Each lobjRow As GridViewRow In gvProduc3.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                lintContadorP2 = lintContadorP2 + 1
            End If
        Next

        If lintContadorP2 = gvProduc3.Rows.Count Then
            hdfPanel3.Value = ""
            pnlProduccion3.Style.Add("display", "none")
        Else
            hdfPanel3.Value = "S"
            pnlProduccion3.Style.Add("display", "block")
        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_GEN_FORMATO").ToString().Equals("ACT") Then
            btnGuardar.Visible = True
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
        End If

    End Sub
    Sub fn_ObtenerParametrosFormulario()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim lintContadorP1 = 0
        Dim lintContadorP2 = 0

        ldsResponse = lobjGerencia.fn_GeneraFormato("01",
                                                    1,
                                                    1,
                                                    Session("@USUARIO").ToString())

        hdfIdGenFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        'hdfCodMaquina.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_MAQU").ToString()
        txtMaquina.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NOM_MAQUINA").ToString()
        ddlArea.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_AREA").ToString()
        'CAMBIO DG - OBTENGO RESPONSABLE - INI
        'ddlResponsable.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtResponsable.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        'CAMBIO DG - OBTENGO RESPONSABLE - FIN
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_INI").ToString()
        txtObjCambio.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBJ_CAMB").ToString()
        txtConsRiesgo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_CONS").ToString()
        txtFecConc.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_CONC").ToString()
        txtDescConcl.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CONCLUS").ToString()

        gvBloque1.DataSource = ldsResponse.Tables(1)
        gvBloque1.DataBind()

        gvProduc1.DataSource = ldsResponse.Tables(2)
        gvProduc1.DataBind()


        gvProduc2.DataSource = ldsResponse.Tables(3)
        gvProduc2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(5)
        gvFirmas.DataBind()

        For Each lobjRow As GridViewRow In gvProduc2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                lintContadorP1 = lintContadorP1 + 1
            End If
        Next

        If lintContadorP1 = gvProduc2.Rows.Count Then
            hdfPanel2.Value = ""
            pnlProduccion2.Style.Add("display", "none")
        Else
            hdfPanel2.Value = "S"
            pnlProduccion2.Style.Add("display", "block")
        End If

        gvProduc3.DataSource = ldsResponse.Tables(4)
        gvProduc3.DataBind()

        For Each lobjRow As GridViewRow In gvProduc3.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                lintContadorP2 = lintContadorP2 + 1
            End If
        Next

        If lintContadorP2 = gvProduc3.Rows.Count Then
            hdfPanel3.Value = ""
            pnlProduccion3.Style.Add("display", "none")
        Else
            hdfPanel3.Value = "S"
            pnlProduccion3.Style.Add("display", "block")
        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_GEN_FORMATO").ToString().Equals("ACT") Then
            btnGuardar.Visible = True
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
        End If

    End Sub

    Sub fn_RegistrarParametros()

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        gbolValidar = True

        If ddlArea.SelectedValue.Equals("0") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Área.');</script>")
            Exit Sub
        End If

        'CAMBIO DG - VALIDA RESPONSABLE - INI
        'If ddlResponsable.SelectedValue.Equals("0") Then
        '    gbolValidar = False
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Responsable.');</script>")
        '    Exit Sub
        'End If
        If txtResponsable.Text.Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Responsable.');</script>")
            Exit Sub
        End If
        'CAMBIO DG - VALIDA RESPONSABLE - FIN

        If txtFecInicio.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha de inicio.');</script>")
            Exit Sub
        End If

        'If Convert.ToDateTime(txtFecInicio.Text.Trim()) <= Date.Now Then
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha de inicio..');</script>")
        '    Exit Sub
        'End If
        Try
            Convert.ToDateTime(txtFecInicio.Text.Trim())
        Catch ex As Exception
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha de inicio válida.');</script>")
            Exit Sub
        End Try

        If txtObjCambio.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese objetivo del cambio.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvBloque1.Rows
            Dim ltxtValor As New TextBox
            'Dim lbolValidar As Boolean = False
            ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                gbolValidar = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
                Exit Sub
            End If

            'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
            'Exit Sub

        Next

        If txtConsRiesgo.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese detalle de consideraciones.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvProduc1.Rows
            Dim ltxtValor As New TextBox
            'Dim lbolValidar As Boolean = False
            ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                gbolValidar = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
                Exit Sub
            End If

            'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
            'Exit Sub

        Next

        If hdfPanel2.Value.Equals("S") Then
            For Each lobjRow As GridViewRow In gvProduc2.Rows
                Dim ltxtValor As New TextBox
                'Dim lbolValidar As Boolean = False
                ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

                If ltxtValor.Text.Trim().Equals("") Then
                    gbolValidar = False
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
                    Exit Sub
                End If

                'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
                'Exit Sub

            Next
        End If

        If hdfPanel3.Value.Equals("S") Then
            For Each lobjRow As GridViewRow In gvProduc3.Rows
                Dim ltxtValor As New TextBox
                'Dim lbolValidar As Boolean = False
                ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

                If ltxtValor.Text.Trim().Equals("") Then
                    gbolValidar = False
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
                    Exit Sub
                End If

                'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
                'Exit Sub

            Next
        End If

        If txtFecConc.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha de conclusiones.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFecConc.Text.Trim())
        Catch ex As Exception
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha de conclusiones válida.');</script>")
            Exit Sub
        End Try

        If txtDescConcl.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese objetivo del cambio.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvFirmas.Rows
            Dim ltxtValor As New TextBox
            'Dim lbolValidar As Boolean = False
            ltxtValor = CType(gvFirmas.Rows(lobjRow.RowIndex).FindControl("txtCampoFirma"), TextBox)

            If ltxtValor.Text.Trim().Equals("") Then
                gbolValidar = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese firmas.');</script>")
                Exit Sub
            End If

            'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese firmas.');</script>")
            'Exit Sub

        Next


        Dim lstrFileNameficha As String = ""
        Dim lintFileSizeficha As Integer = 0
        Dim lstrContentTypeficha As String = ""
        Dim lstrFileExtensionficha As String = ""
        Dim larraySeparatorFileficha As String()

        If fluAdjuntarFicha.HasFile Then

            lhpfFileficha = fluAdjuntarFicha.PostedFile
            lbrFragmentarficha = New BinaryReader(lhpfFileficha.InputStream)
            lbytBufferficha = lbrFragmentarficha.ReadBytes(lhpfFileficha.ContentLength)
            lstrFileNameficha = Path.GetFileName(lhpfFileficha.FileName)
            lintFileSizeficha = lhpfFileficha.ContentLength
            lstrContentTypeficha = lhpfFileficha.ContentType
            lstrFileExtensionficha = Path.GetExtension(lhpfFileficha.FileName)
            lbyteContentficha = lbytBufferficha

            larraySeparatorFileficha = Split(fluAdjuntarFicha.FileName, ".")

            lstrFileNameficha = larraySeparatorFileficha(0) + Path.GetExtension(fluAdjuntarFicha.FileName)

            lstrRutaficha = Server.MapPath("~/Archivos/") + lstrFileNameficha

            fluAdjuntarFicha.SaveAs(lstrRutaficha)
        End If

        'CAMBIO DG - CAMBIO RESPOSABLE - INI
        'ldtResponse = lobjGerencia.fn_ModificarFormatoProt(Convert.ToInt32(hdfIdGenFormato.Value),
        '                                                   ddlArea.SelectedValue,
        '                                                   hdfCodMaquina.Value,
        '                                                   ddlResponsable.SelectedValue,
        '                                                   Convert.ToDateTime(txtFecInicio.Text.Trim()).ToString("yyyyMMdd"),
        '                                                   txtObjCambio.Text.ToUpper(),
        '                                                   txtConsRiesgo.Text.ToUpper(),
        '                                                   Convert.ToDateTime(txtFecConc.Text.Trim()).ToString("yyyyMMdd"),
        '                                                   txtDescConcl.Text.ToUpper(),
        '                                                   Session("@USUARIO").ToString())
        ldtResponse = lobjGerencia.fn_ModificarFormatoProt(Convert.ToInt32(hdfIdGenFormato.Value),
                                                           ddlArea.SelectedValue,
                                                           txtMaquina.Text,
                                                           txtResponsable.Text,
                                                           Convert.ToDateTime(txtFecInicio.Text.Trim()).ToString("yyyyMMdd"),
                                                           txtObjCambio.Text.ToUpper(),
                                                           txtConsRiesgo.Text.ToUpper(),
                                                           Convert.ToDateTime(txtFecConc.Text.Trim()).ToString("yyyyMMdd"),
                                                           txtDescConcl.Text.ToUpper(),
                                                           Session("@USUARIO").ToString(),
                                                             lstrFileNameficha, lintFileSizeficha, lstrContentTypeficha, lstrFileExtensionficha)
        'CAMBIO DG - CAMBIO RESPOSABLE - FIN

        Session("ldtResponse") = ldtResponse

        For Each lobjRow As GridViewRow In gvBloque1.Rows
            Dim ltxtValor As New TextBox
            Dim lstrKeyName As String = ""
            ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

            lstrKeyName = gvBloque1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()

            ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(lstrKeyName),
                                                             ltxtValor.Text.Trim().ToUpper(),
                                                             Session("@USUARIO").ToString())

        Next

        For Each lobjRow As GridViewRow In gvProduc1.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

            ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
                                                             ltxtValor.Text.Trim().ToUpper(),
                                                             Session("@USUARIO").ToString())

        Next

        For Each lobjRow As GridViewRow In gvProduc2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
                                                             ltxtValor.Text.Trim().ToUpper(),
                                                             Session("@USUARIO").ToString())

        Next

        For Each lobjRow As GridViewRow In gvProduc3.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

            ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc3.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
                                                             ltxtValor.Text.Trim().ToUpper(),
                                                             Session("@USUARIO").ToString())

        Next

    End Sub

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Session("@USUARIO") = "JMARTINE"
            fn_CargarListas()
            If Not Request("pidCodGenFormato") Is Nothing Then
                fn_ObtenerExisteFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
            Else
                fn_ObtenerParametrosFormulario_Nuevo()
            End If
        End If
    End Sub

    Protected Sub ibtAgregarProd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtAgregarProd.Click
        If hdfPanel2.Value.Equals("") Then
            hdfPanel2.Value = "S"
            pnlProduccion2.Style.Add("display", "block")
            'Dim lobjGerencia As New clsGerencia
            'Dim ldtResponse As DataTable
            'ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdGenFormato.Value),
            '                                                1,
            '                                                "PRODCP2",
            '                                                "",
            '                                                "",
            '                                                Session("@USUARIO").ToString())

            'gvProduc2.DataSource = ldtResponse
            'gvProduc2.DataBind()
            Exit Sub
        End If

        If hdfPanel3.Value.Equals("") Then
            hdfPanel3.Value = "S"
            pnlProduccion3.Style.Add("display", "block")

            ibtAgregarProd.Visible = False
            'Dim lobjGerencia As New clsGerencia
            'Dim ldtResponse As DataTable
            'ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdGenFormato.Value),
            '                                                1,
            '                                                "PRODCP3",
            '                                                "",
            '                                                "",
            '                                                Session("@USUARIO").ToString())

            'gvProduc3.DataSource = ldtResponse
            'gvProduc3.DataBind()
            Exit Sub
        End If
    End Sub

    Protected Sub ibtMenos2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtMenos2.Click
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        hdfPanel2.Value = ""
        pnlProduccion2.Style.Add("display", "none")
        For Each lobjRow As GridViewRow In gvProduc2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            ltxtValor.Text = ""

            'ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
            '                                                 "",
            '                                                 Session("@USUARIO").ToString())


            ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
                                                        "PRODCP2",
                                                        1)

            gvProduc2.DataSource = ldtResponse
            gvProduc2.DataBind()

        Next
        ibtAgregarProd.Visible = True
    End Sub

    Protected Sub ibtMenos3_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtMenos3.Click
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        hdfPanel3.Value = ""
        pnlProduccion3.Style.Add("display", "none")
        For Each lobjRow As GridViewRow In gvProduc3.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

            ltxtValor.Text = ""

            'ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
            '                                                 "",
            '                                                 Session("@USUARIO").ToString())

            ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        Convert.ToInt32(gvProduc3.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
                                                        "PRODCP3",
                                                        1)

            gvProduc3.DataSource = ldtResponse
            gvProduc3.DataBind()
        Next
        ibtAgregarProd.Visible = True
    End Sub

  

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
      

        fn_RegistrarParametros()
        If gbolValidar Then
            fn_CargarListas()
            If Not Request("pidCodGenFormato") Is Nothing Then
                fn_ObtenerExisteFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
                btnSolicitar.Visible = True
            Else
                'If hdfIdGenFormato.Value <> "" And Not hdfIdGenFormato.Value.Equals("") Then
                '    fn_ObtenerExisteFormato(Convert.ToInt32(hdfIdGenFormato.Value))
                'Else
                '    fn_ObtenerParametrosFormulario()
                'End If
                Response.Redirect("frmListFormatoCambioProceso.aspx")
            End If
        End If
    End Sub

    Protected Sub gvBloque1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvBloque1.RowCommand
        If e.CommandName = "Agregar" Then
            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            'Dim ldtResponse As DataSet
            Dim gvr As GridViewRow
            Dim ltxtCampoF As TextBox
            Dim ltxtValorF As TextBox

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            ltxtCampoF = CType(gvBloque1.FooterRow.FindControl("txtCampoF"), TextBox)
            ltxtValorF = CType(gvBloque1.FooterRow.FindControl("txtValorF"), TextBox)

            'If ltxtCampoF.Text.Trim.Equals("") Then
            '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
            '    Exit Sub
            'End If

            If ltxtValorF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
                Exit Sub
            End If

            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - INI'
            'For Each lobjRow As GridViewRow In gvBloque1.Rows
            '    Dim ltxtValor As New TextBox
            '    Dim lstrKeyName As String = ""
            '    ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

            '    lstrKeyName = gvBloque1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()

            '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(lstrKeyName),
            '                                                     ltxtValor.Text.Trim().ToUpper(),
            '                                                     Session("@USUARIO").ToString())

            'Next
            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - FIN'

            'CAMBIO DG - NO VA CAMPO VALOR - INI
            'ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdGenFormato.Value),
            '                                                1,
            '                                                "BLQINFOPARAMCP",
            '                                                ltxtCampoF.Text.Trim().ToUpper(),
            '                                                ltxtValorF.Text.Trim().ToUpper(),
            '                                                Session("@USUARIO").ToString())
            ldtResponse = lobjGerencia.fn_AgregarParametrosInformacion(Convert.ToInt32(hdfIdGenFormato.Value),
                                                            1,
                                                            "BLQINFOPARAMCP",
                                                            ltxtValorF.Text.Trim().ToUpper(),
                                                            Session("@USUARIO").ToString())
            'CAMBIO DG - NO VA CAMPO VALOR - FIN
            gvBloque1.DataSource = ldtResponse
            gvBloque1.DataBind()
            'gvBloque1.DataSource = ldtResponse.Tables(0)
            'gvBloque1.DataBind()
            'If ldtResponse.Tables(1).Rows.Count > 0 Then
            '    gvProduc1.DataSource = ldtResponse.Tables(1)
            '    gvProduc1.DataBind()
            'End If
            'If ldtResponse.Tables(2).Rows.Count > 0 Then
            '    gvProduc2.DataSource = ldtResponse.Tables(2)
            '    gvProduc2.DataBind()
            'End If
            'If ldtResponse.Tables(3).Rows.Count > 0 Then
            '    gvProduc3.DataSource = ldtResponse.Tables(3)
            '    gvProduc3.DataBind()
            'End If
        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            'CAMBIO DG  - NO VA CAMPO VALOR - INI
            'ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdGenFormato.Value),
            '                                            Convert.ToInt32(e.CommandArgument),
            '                                            "BLQINFOPARAMCP",
            '                                            1)
            ldtResponse = lobjGerencia.fn_EliminarParamInformacion(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        Convert.ToInt32(e.CommandArgument),
                                                        "BLQINFOPARAMCP",
                                                        1)
            'CAMBIO DG - NO VA CAMPO VALOR - INI
            gvBloque1.DataSource = ldtResponse
            gvBloque1.DataBind()

        End If
    End Sub

    Protected Sub gvProduc1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduc1.RowCommand
        If e.CommandName = "Agregar1" Then
            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            Dim gvr As GridViewRow
            Dim ltxtCampoF As TextBox
            Dim ltxtValorF As TextBox

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            ltxtCampoF = CType(gvProduc1.FooterRow.FindControl("txtCampoF1"), TextBox)
            ltxtValorF = CType(gvProduc1.FooterRow.FindControl("txtValorF1"), TextBox)

            If ltxtCampoF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
                Exit Sub
            End If

            If ltxtValorF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
                Exit Sub
            End If

            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - INI'
            'For Each lobjRow As GridViewRow In gvProduc1.Rows
            '    Dim ltxtValor As New TextBox
            '    ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

            '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
            '                                                     ltxtValor.Text.Trim().ToUpper(),
            '                                                     Session("@USUARIO").ToString())

            'Next
            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - FIN'

            ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdGenFormato.Value),
                                                            1,
                                                            "PRODCP1",
                                                            ltxtCampoF.Text.Trim().ToUpper(),
                                                            ltxtValorF.Text.Trim().ToUpper(),
                                                            Session("@USUARIO").ToString())

            gvProduc1.DataSource = ldtResponse
            gvProduc1.DataBind()
        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        Convert.ToInt32(e.CommandArgument),
                                                        "PRODCP1",
                                                        1)

            gvProduc1.DataSource = ldtResponse
            gvProduc1.DataBind()

        End If
    End Sub

    Protected Sub gvProduc2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduc2.RowCommand
        If e.CommandName = "Agregar" Then
            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            Dim gvr As GridViewRow
            Dim ltxtCampoF As TextBox
            Dim ltxtValorF As TextBox

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            ltxtCampoF = CType(gvProduc2.FooterRow.FindControl("txtCampoF2"), TextBox)
            ltxtValorF = CType(gvProduc2.FooterRow.FindControl("txtValorF2"), TextBox)

            If ltxtCampoF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
                Exit Sub
            End If

            If ltxtValorF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
                Exit Sub
            End If


            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - INI'
            'For Each lobjRow As GridViewRow In gvProduc2.Rows
            '    Dim ltxtValor As New TextBox
            '    ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
            '                                                     ltxtValor.Text.Trim().ToUpper(),
            '                                                     Session("@USUARIO").ToString())

            'Next
            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - FIN'
            ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdGenFormato.Value),
                                                            1,
                                                            "PRODCP2",
                                                            ltxtCampoF.Text.Trim().ToUpper(),
                                                            ltxtValorF.Text.Trim().ToUpper(),
                                                            Session("@USUARIO").ToString())

            gvProduc2.DataSource = ldtResponse
            gvProduc2.DataBind()
        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        Convert.ToInt32(e.CommandArgument),
                                                        "PRODCP2",
                                                        1)

            gvProduc2.DataSource = ldtResponse
            gvProduc2.DataBind()

        End If
    End Sub

    Protected Sub gvProduc3_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduc3.RowCommand
        If e.CommandName = "Agregar" Then
            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            Dim gvr As GridViewRow
            Dim ltxtCampoF As TextBox
            Dim ltxtValorF As TextBox

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            ltxtCampoF = CType(gvProduc3.FooterRow.FindControl("txtCampoF3"), TextBox)
            ltxtValorF = CType(gvProduc3.FooterRow.FindControl("txtValorF3"), TextBox)

            If ltxtCampoF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
                Exit Sub
            End If

            If ltxtValorF.Text.Trim.Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
                Exit Sub
            End If

            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - INI 
            'For Each lobjRow As GridViewRow In gvProduc3.Rows
            '    Dim ltxtValor As New TextBox
            '    ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

            '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc3.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
            '                                                     ltxtValor.Text.Trim().ToUpper(),
            '                                                     Session("@USUARIO").ToString())

            'Next
            'CAMBIO DG - GRABAR ANTES DE CREAR OTRA PREGUNTA  - FIN
            ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdGenFormato.Value),
                                                            1,
                                                            "PRODCP3",
                                                            ltxtCampoF.Text.Trim().ToUpper(),
                                                            ltxtValorF.Text.Trim().ToUpper(),
                                                            Session("@USUARIO").ToString())

            gvProduc3.DataSource = ldtResponse
            gvProduc3.DataBind()
        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        Convert.ToInt32(e.CommandArgument),
                                                        "PRODCP3",
                                                        1)

            gvProduc3.DataSource = ldtResponse
            gvProduc3.DataBind()

        End If
    End Sub

    Protected Sub gvFirmas_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFirmas.RowCommand
        If e.CommandName = "Agregar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            Dim gvr As GridViewRow
            Dim lddlFirmas As DropDownList

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            lddlFirmas = CType(gvFirmas.FooterRow.FindControl("ddlFirmas"), DropDownList)

            If lddlFirmas.SelectedValue.ToString().Equals("0") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un responsable.');</script>")
                Exit Sub
            End If

            For Each lobjRow As GridViewRow In gvFirmas.Rows
                Dim ltxtValor As New TextBox
                ltxtValor = CType(gvFirmas.Rows(lobjRow.RowIndex).FindControl("txtCampoFirma"), TextBox)

                If ltxtValor.Text.Equals(lddlFirmas.SelectedValue) Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Esta firma ya fue seleccionada.');</script>")
                    Exit Sub
                End If
            Next

            ldtResponse = lobjGerencia.fn_AgregarFirmas(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        1,
                                                        lddlFirmas.SelectedValue.ToString(),
                                                        Session("@USUARIO").ToString())

            gvFirmas.DataSource = ldtResponse
            gvFirmas.DataBind()

        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_EliminarFirma(Convert.ToInt32(hdfIdGenFormato.Value),
                                                        1,
                                                        e.CommandArgument)

            gvFirmas.DataSource = ldtResponse
            gvFirmas.DataBind()

        End If
    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        Try
            fn_SolicitarAprobacion(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
        Catch ex As Exception
            'fn_SolicitarAprobacion(Convert.ToInt32(hdfIdGenFormato.Value))
        End Try
    End Sub

    Protected Sub imgbtnVolver_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVolver.Click
        Response.Redirect("frmListFormatoCambioProceso.aspx")
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        fn_anularFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
    End Sub

    Protected Sub btnAprobar_Click(sender As Object, e As EventArgs) Handles btnAprobar.Click
        fn_aprobarFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()), 1)
    End Sub

    'Protected Sub btnpurb_Click(sender As Object, e As EventArgs) Handles btnpurb.Click
    '    fn_generarFormatoPDF(4)
    'End Sub

#End Region

    Private Sub gvBloque1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBloque1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
                Dim txtValor As TextBox = CType(e.Row.FindControl("txtValor"), TextBox)
                If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
                    btnEliminar.Visible = False
                    txtValor.Visible = False
                Else
                    btnEliminar.Visible = True
                    'txtValor.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub gvProduc1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduc1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
                Dim txtValor1 As TextBox = CType(e.Row.FindControl("txtValor1"), TextBox)
                Dim txtCampo1 As TextBox = CType(e.Row.FindControl("txtCampo1"), TextBox)
                If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
                    btnEliminar.Visible = False
                    txtValor1.Visible = False
                    txtCampo1.Visible = False
                Else
                    btnEliminar.Visible = True
                    'txtValor1.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub gvProduc2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduc2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
                Dim txtValor2 As TextBox = CType(e.Row.FindControl("txtValor2"), TextBox)
                Dim txtCampo2 As TextBox = CType(e.Row.FindControl("txtCampo2"), TextBox)
                If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
                    btnEliminar.Visible = False
                    txtValor2.Visible = False
                    txtCampo2.Visible = False
                Else
                    btnEliminar.Visible = True
                    'txtValor2.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub gvProduc3_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduc3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
                Dim txtValor3 As TextBox = CType(e.Row.FindControl("txtValor3"), TextBox)
                Dim txtCampo3 As TextBox = CType(e.Row.FindControl("txtCampo3"), TextBox)
                If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
                    btnEliminar.Visible = False
                    txtValor3.Visible = False
                    txtCampo3.Visible = False
                Else
                    btnEliminar.Visible = True
                    'txtValor3.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub gvFirmas_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFirmas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
                Dim txtCampoFirma As TextBox = CType(e.Row.FindControl("txtCampoFirma"), TextBox)
                Dim txtCampoFFirma As TextBox = CType(e.Row.FindControl("txtCampoFirma"), TextBox)
                Dim lblEstadoFirma As Label = CType(e.Row.FindControl("lblEstadoFirma"), Label)
                If DataBinder.Eval(e.Row.DataItem, "VCH_USU_FIRMA").ToString() = "" Then
                    btnEliminar.Visible = False
                    txtCampoFirma.Visible = False
                    txtCampoFFirma.Visible = False
                    lblEstadoFirma.Visible = False
                Else
                    btnEliminar.Visible = True
                    'txtValor3.Enabled = True
                End If
            End If
        End If
    End Sub
    Protected Sub ibtDescargarficha_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDescargarfichas.Click
        Dim ldtResponse As DataTable
        ldtResponse = Session("ldtResponse")
        If ldtResponse.Rows().Count > 0 Then
            If ldtResponse.Rows(0).Item("VCH_FILENAMEINFORME").ToString() <> "" Then
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ldtResponse.Rows(0).Item("VCH_FILENAMEINFORME").ToString())
                Response.TransmitFile(Server.MapPath("~/Archivos/") + ldtResponse.Rows(0).Item("VCH_FILENAMEINFORME").ToString())
                Response.End()
            End If
        End If
    End Sub
End Class