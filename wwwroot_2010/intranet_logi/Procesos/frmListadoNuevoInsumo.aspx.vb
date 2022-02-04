Imports NM_General
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class frmListadoNuevoInsumo
    Inherits System.Web.UI.Page

    Dim gbolValidar As Boolean = True
    Private _hojaRuta As Boolean = False

#Region "Metodos"

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

    Sub fn_bloquearControles()
        'ddlResponsable.Enabled = False
        txtResponsable.Enabled = False

        txtFecInicio.Enabled = False
        'ibtCalendar1.Style.Add("visibility", "hidden")
        txtNomProv.Enabled = False
        btnSolicitante.Style.Add("visibility", "hidden")
        txtDetProc.Enabled = False
        txtProcProd.Enabled = False
        txtObjCambio.Enabled = False
        'For Each lobjRow As GridViewRow In gvBloque1.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)
        '    ltxtValor.Enabled = False
        'Next
        'gvBloque1.Columns(2).Visible = False
        'gvBloque1.FooterRow.Visible = False

        'CAMBIO JR - RIESGO ES TEXTO ,NO GRILLA  - INI
        'For Each lobjRow As GridViewRow In gvBloque2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvBloque2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
        '    ltxtValor.Enabled = False
        'Next
        'gvBloque2.Columns(2).Visible = False
        'gvBloque2.FooterRow.Visible = False
        txtConsRiesgo.Enabled = False
        'CAMBIO JR - RIESGO ES TEXTO ,NO GRILLA  - FIN
        'ibtAgregarProd.Visible = False
        'For Each lobjRow As GridViewRow In gvProduc1.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)
        '    ltxtValor.Enabled = False
        'Next
        'gvProduc1.Columns(2).Visible = False
        'gvProduc1.FooterRow.Visible = False
        'For Each lobjRow As GridViewRow In gvProduc2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
        '    ltxtValor.Enabled = False
        'Next
        'gvProduc2.Columns(2).Visible = False
        'gvProduc2.FooterRow.Visible = False
        'ibtMenos2.Visible = False
        'For Each lobjRow As GridViewRow In gvProduc3.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)
        '    ltxtValor.Enabled = False
        'Next
        'gvProduc3.Columns(2).Visible = False
        'gvProduc3.FooterRow.Visible = False
        'ibtMenos3.Visible = False
        txtDescConcl.Enabled = False
        txtFecConc.Enabled = False
        ibtCalendar2.Style.Add("visibility", "hidden")
        gvFirmas.Columns(3).Visible = False
        gvFirmas.FooterRow.Visible = False

        ddlFabricante.Enabled = False
        txtPlanta.Enabled = False
        ddlProcedenciaPais.Enabled = False
        txtCantidad.Enabled = False
        txtUNidadMedida.Enabled = False
        txtLote.Enabled = False
        txtFechaProduccion.Enabled = False
        txtUbicacionNUevoMaterial.Enabled = False

        txtCaracteristicasEmbalaje.Enabled = False
        txtCodigoMaterial.Enabled = False
        txtCaracteristicasRelevantesMaterial.Enabled = False

        txtObservacionesInsumos.Enabled = False
        fluAdjuntarCartaCompremiso.Enabled = False
        fluAdjuntarCertificadoCalidad.Enabled = False
        fluAdjuntarFicha.Enabled = False

    End Sub

    Sub fn_enviarCorreoFormatos(ByVal pstrRuta As String)

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_enviarCorreoFormatos("FORMATO NUEVO INSUMO", pstrRuta)

    End Sub
    Sub fn_ObetnerPaises()
        Dim lobjGerencia As New clsGerencia

        ddlProcedenciaPais.DataSource = lobjGerencia.fn_obtenerPaises()
        ddlProcedenciaPais.DataValueField = "CO_PAIS"
        ddlProcedenciaPais.DataTextField = "NO_PAIS"
        ddlProcedenciaPais.DataBind()

    End Sub
    Sub fn_ObtenerTablaMAestra()
        Dim lobjGerencia As New clsGerencia
        ddlFabricante.DataSource = lobjGerencia.fn_listarTablaMaestra("00", "")
        ddlFabricante.DataValueField = "COD_FABRICANTE"
        ddlFabricante.DataTextField = "NOM_FABRICANTE"
        ddlFabricante.DataBind()

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

        File.Delete(Server.MapPath("~/Procesos/Formatos/PDF") + "/FormatoNuevoInsumo" + pintCodGenFor.ToString() + ".pdf")

        MyReportViewer.ProcessingMode = ProcessingMode.Remote
        MyReportViewer.ServerReport.ReportServerCredentials = ReportServerCredentials
        MyReportViewer.ServerReport.ReportServerUrl = New Uri("http://mundodesa02:8081/reportserver")
        MyReportViewer.ServerReport.ReportPath = "/Administrativo_NuevoMundo/Gerencia/gg_formato_nuevo_insumo"
        'MyReportViewer.ServerReport.DisplayName = "gg_formato_cambio_proceso"
        MyReportViewer.ServerReport.Refresh()
        Dim reportParameterCollection(1) As ReportParameter
        reportParameterCollection(0) = New ReportParameter
        reportParameterCollection(0).Name = "pintCodGenFormato"
        reportParameterCollection(0).Values.Add(pintCodGenFor)
        reportParameterCollection(1) = New ReportParameter
        reportParameterCollection(1).Name = "pintCodFormato"
        reportParameterCollection(1).Values.Add(2)
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

        Dim pdfPath As String = Server.MapPath("~/Procesos/Formatos/PDF") + "/FormatoNuevoInsumo_" + pintCodGenFor.ToString() + "." + extension
        'Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
        'pdfFile.Write(bytes, 0, bytes.Length)
        'pdfFile.Close()

        System.IO.File.WriteAllBytes(pdfPath, bytes)

        'fn_enviarCorreoFormatos("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoNuevoInsumo_" + pintCodGenFor.ToString() + ".pdf")
        Dim dt As DataTable = fn_enviarCorreoFormatos_V2("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoNuevoInsumo_" + pintCodGenFor.ToString() + ".pdf", pintCodGenFor, pintCodFormato)
        'Response.Flush()
        'Response.End()

        'Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub

    Sub fn_aprobarFormato(ByVal pintCodGenFor As Int32, ByVal pintCodFormato As Integer)

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_aprobarFormatos("U",
                                                      pintCodGenFor,
                                                      2,
                                                      Session("@USUARIO").ToString())

        If ldtResponse.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("200") Then
            'fn_ObtenerExisteFormato(pintCodGenFor)
            fn_generarFormatoPDF(pintCodGenFor, pintCodFormato)
            Response.Redirect("frmListFormatoNuevoInsumo.aspx")
        ElseIf ldtResponse.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("300") Then
            'fn_ObtenerExisteFormato(pintCodGenFor)
            Response.Redirect("frmListFormatoNuevoInsumo.aspx")
        End If

        'Response.Redirect("frmListFormatoCambioProceso.aspx")
    End Sub

    Sub fn_anularFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_solicitarAprobacion("01",
                                                          pintCodGenFor,
                                                          2,
                                                          "ANU")

        Response.Redirect("frmListFormatoNuevoInsumo.aspx")

    End Sub

    Sub fn_SolicitarAprobacion(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        gbolValidar = True

        'CAMBIO JR - RESPONSABLE TEXTBOX - INI
        'If ddlResponsable.SelectedValue.Equals("0") Then
        '    gbolValidar = False
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Responsable.');</script>")
        '    Exit Sub
        'End If
        If txtResponsable.Text.Equals("0") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un Responsable.');</script>")
            Exit Sub
        End If
        'CAMBIO JR - RESPONSABLE TEXTBOX - FIN

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

        If hdfCodProv.Value.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el proveedor.');</script>")
            Exit Sub
        End If

        If txtDetProc.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese detalle del producto.');</script>")
            Exit Sub
        End If

        If txtProcProd.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese proceso del producto.');</script>")
            Exit Sub
        End If

        'For Each lobjRow As GridViewRow In gvBloque1.Rows
        '    Dim ltxtValor As New TextBox
        '    'Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        gbolValidar = False
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
        '        Exit Sub
        '    End If

        '    'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
        '    'Exit Sub

        'Next

        'CAMBIO JR  - CAMPO RIESGO ES TXT , NO GRILLA - INI
        'For Each lobjRow As GridViewRow In gvBloque2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvBloque2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        gbolValidar = False
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
        '        Exit Sub
        '    End If
        'Next
        If txtConsRiesgo.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese detalle de consideraciones.');</script>")
            Exit Sub
        End If
        'CAMBIO JR  - CAMPO RIESGO ES TXT , NO GRILLA - FIN

        'For Each lobjRow As GridViewRow In gvProduc1.Rows
        '    Dim ltxtValor As New TextBox
        '    'Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        gbolValidar = False
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
        '        Exit Sub
        '    End If

        '    'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
        '    'Exit Sub

        'Next

        'If hdfPanel2.Value.Equals("S") Then
        '    For Each lobjRow As GridViewRow In gvProduc2.Rows
        '        Dim ltxtValor As New TextBox
        '        'Dim lbolValidar As Boolean = False
        '        ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '        If ltxtValor.Text.Trim().Equals("") Then
        '            gbolValidar = False
        '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
        '            Exit Sub
        '        End If

        '        'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
        '        'Exit Sub

        '    Next
        'End If

        'If hdfPanel3.Value.Equals("S") Then
        '    For Each lobjRow As GridViewRow In gvProduc3.Rows
        '        Dim ltxtValor As New TextBox
        '        'Dim lbolValidar As Boolean = False
        '        ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

        '        If ltxtValor.Text.Trim().Equals("") Then
        '            gbolValidar = False
        '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
        '            Exit Sub
        '        End If

        '        'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
        '        'Exit Sub

        '    Next
        'End If

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
                                                          2,
                                                          "SOL")

        Response.Redirect("frmListFormatoNuevoInsumo.aspx")
    End Sub

    Sub fn_CargarListas()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet

        ldsResponse = lobjGerencia.fn_CargarListas("")


        'ddlArea.DataTextField = "DE_AREA"
        'ddlArea.DataValueField = "CO_AREA"
        'ddlArea.DataSource = ldsResponse.Tables(0)
        'ddlArea.DataBind()

        'ddlResponsable.DataTextField = "NO_USUA"
        'ddlResponsable.DataValueField = "var_Dato"
        'ddlResponsable.DataSource = ldsResponse.Tables(1)
        'ddlResponsable.DataBind()

    End Sub

    Public Function fn_CargarFirmas() As DataTable

        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim ldtResponse As DataTable

        ldsResponse = lobjGerencia.fn_CargarListas("")

        ldtResponse = ldsResponse.Tables(1)

        Return ldtResponse
    End Function

    Sub fn_ObtenerExisteFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim lintContadorP1 = 0
        Dim lintContadorP2 = 0

        ldsResponse = lobjGerencia.fn_ObtenerFormato_2(pintCodGenFor,
                                                       2)

        Session("ldtResponse") = ldsResponse.Tables(0)
        hdfIdFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        'ddlResponsable.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtResponsable.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC").ToString()
        txtNomProv.Text = ldsResponse.Tables(0).Rows(0).Item("NO_CORT_PROV").ToString()
        hdfCodProv.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_PROV").ToString()
        txtDetProc.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_PROD").ToString()
        txtProcProd.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_PROC_PROD").ToString()
        txtObjCambio.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBJ_CAMB").ToString()
        txtDescConcl.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CONC_FORM").ToString()
        txtFecConc.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_CONC").ToString()


        'gvBloque1.DataSource = ldsResponse.Tables(1)
        'gvBloque1.DataBind()

        'gvBloque2.DataSource = ldsResponse.Tables(2)
        'gvBloque2.DataBind()
        txtConsRiesgo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_CONS").ToString()

        'gvProduc1.DataSource = ldsResponse.Tables(2)
        'gvProduc1.DataBind()

        'gvProduc2.DataSource = ldsResponse.Tables(3)
        'gvProduc2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(5)
        gvFirmas.DataBind()

        'For Each lobjRow As GridViewRow In gvProduc2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        lintContadorP1 = lintContadorP1 + 1
        '    End If
        'Next

        'If lintContadorP1 = gvProduc2.Rows.Count Then
        '    'hdfPanel2.Value = ""
        '    pnlProduccion2.Style.Add("display", "none")
        'Else
        '    'hdfPanel2.Value = "S"
        '    pnlProduccion2.Style.Add("display", "block")
        'End If

        'gvProduc3.DataSource = ldsResponse.Tables(4)
        'gvProduc3.DataBind()

        'For Each lobjRow As GridViewRow In gvProduc3.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        lintContadorP2 = lintContadorP2 + 1
        '    End If
        'Next

        'If lintContadorP2 = gvProduc3.Rows.Count Then
        '    'hdfPanel3.Value = ""
        '    pnlProduccion3.Style.Add("display", "none")
        'Else
        '    'hdfPanel3.Value = "S"
        '    pnlProduccion3.Style.Add("display", "block")
        'End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_FORMATO").ToString().Equals("ACT") Then
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

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_FORMATO").ToString().Equals("SOL") Then
            btnGuardar.Visible = False
            btnSolicitar.Visible = False
            'btnAprobar.Visible = True
            btnAnular.Visible = False
            fn_bloquearControles()
            gvFirmas.Columns(2).Visible = True

            ldtResponseApro = lobjGerencia.fn_aprobarFormatos("B",
                                                              pintCodGenFor,
                                                              2,
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

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_FORMATO").ToString().Equals("ANU") Then
            btnGuardar.Visible = False
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
            fn_bloquearControles()
        End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_FORMATO").ToString().Equals("APR") Then
            btnGuardar.Visible = False
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
            fn_bloquearControles()
            gvFirmas.Columns(2).Visible = True
        End If
        lblPrueba.Text = IIf(ddlTipoPrueba.SelectedValue = "PRE", "PRUEBA PRELIMINAR N°", "PRUEBA INDUSTRIAL N°")
        txtNroPreliminar.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NROPRELIMINAR").ToString()
        ddlTipoPrueba.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString().ToUpper()
        ddlFabricante.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_FABRICANTE").ToString()
        txtPlanta.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_PLANTA").ToString()
        ddlProcedenciaPais.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("COD_PAIS").ToString()
        txtCantidad.Text = ldsResponse.Tables(0).Rows(0).Item("INT_CANTIDAD").ToString()
        txtUNidadMedida.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_UNIDAD_MEDIDA").ToString()
        txtLote.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_LOTE").ToString()
        txtFechaProduccion.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FECHA_PRODUCCION").ToString()
        txtUbicacionNUevoMaterial.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_UBICACION_NUEVO_MATERIAL").ToString()

        txtCaracteristicasEmbalaje.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CARACTERISTICAS_EMBALAJE").ToString()
        txtCodigoMaterial.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CODIGO_MATERIAL").ToString()
        txtCaracteristicasRelevantesMaterial.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CARACTERISTICAS_RELEVANTE_MATERIAL").ToString()
        'txtAprobado.Attributes.Add("disabled", "true")
        'txtDesaprobado.Attributes.Add("disabled", "true")

        txtAprobado.Text = IIf(ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString() = "Aprobado", ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString(), "")
        txtDesaprobado.Text = IIf(ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString() = "Desaprobado", ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString(), "")
        If txtAprobado.Text <> "" Then
            txtAprobado.BackColor = Drawing.Color.Yellow
            txtDesaprobado.BackColor = Drawing.Color.White
        Else
            txtDesaprobado.BackColor = Drawing.Color.Red
            txtAprobado.BackColor = Drawing.Color.White
        End If

        txtObservacionesInsumos.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBSERVACIONES_INSUMO").ToString()
        lblficha.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEFICHA").ToString()
        lblCertificado.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMECERTIFICADO").ToString()
        lblCarta.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMECARTA").ToString()
        lbldocumentoadicional.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEDOCUMENTO").ToString()
        'fluAdjuntarCartaCompremiso.va = False
        'fluAdjuntarCertificadoCalidad.Text = False
        'fluAdjuntarFicha.Enabled = False

    End Sub

    Sub fn_ObtenerParametrosFormulario()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim lintContadorP1 = 0
        Dim lintContadorP2 = 0

        ldsResponse = lobjGerencia.fn_GeneraFormato_2("01",
                                                      0,
                                                      1,
                                                      Session("@USUARIO").ToString())
        Session("ldtResponse") = ldsResponse.Tables(0)

        hdfIdFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        Session("CodGenFormato") = hdfIdFormato.Value
        'ddlResponsable.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtResponsable.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_RESP").ToString()
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC").ToString()
        txtNomProv.Text = ldsResponse.Tables(0).Rows(0).Item("NO_CORT_PROV").ToString()
        hdfCodProv.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_PROV").ToString()
        txtDetProc.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_PROD").ToString()
        txtProcProd.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_PROC_PROD").ToString()
        txtObjCambio.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBJ_CAMB").ToString()
        txtDescConcl.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CONC_FORM").ToString()
        txtFecConc.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_CONC").ToString()
        txtFecInicio.Text = Date.Now.ToString("dd/MM/yyyy")

        txtNroPreliminar.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NROPRELIMINAR").ToString()
        ddlTipoPrueba.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString()
        ddlFabricante.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_FABRICANTE").ToString()
        txtPlanta.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_PLANTA").ToString()
        ddlProcedenciaPais.SelectedValue = ldsResponse.Tables(0).Rows(0).Item("COD_PAIS").ToString()
        txtCantidad.Text = ldsResponse.Tables(0).Rows(0).Item("INT_CANTIDAD").ToString()
        txtUNidadMedida.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_UNIDAD_MEDIDA").ToString()
        txtLote.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_LOTE").ToString()
        txtFechaProduccion.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FECHA_PRODUCCION").ToString()
        txtUbicacionNUevoMaterial.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_UBICACION_NUEVO_MATERIAL").ToString()

        txtCaracteristicasEmbalaje.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CARACTERISTICAS_EMBALAJE").ToString()
        txtCodigoMaterial.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CODIGO_MATERIAL").ToString()
        txtCaracteristicasRelevantesMaterial.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_CARACTERISTICAS_RELEVANTE_MATERIAL").ToString()

        txtAprobado.Text = IIf(ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString() = "Aprobado", ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString(), "")
        txtDesaprobado.Text = IIf(ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString() = "Desaprobado", ldsResponse.Tables(0).Rows(0).Item("VCH_ESTADO_INSUMO").ToString(), "")
        If txtAprobado.Text <> "" Then
            txtAprobado.BackColor = Drawing.Color.Yellow
            txtDesaprobado.BackColor = Drawing.Color.White
        Else
            txtDesaprobado.BackColor = Drawing.Color.Red
            txtAprobado.BackColor = Drawing.Color.White
        End If
        txtObservacionesInsumos.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBSERVACIONES_INSUMO").ToString()
        lblficha.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEFICHA").ToString()
        lblCertificado.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMECERTIFICADO").ToString()
        lblCarta.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMECARTA").ToString()
        'gvBloque1.DataSource = ldsResponse.Tables(1)
        'gvBloque1.DataBind()

        'gvBloque2.DataSource = ldsResponse.Tables(2)
        'gvBloque2.DataBind()
        txtConsRiesgo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_CONS").ToString()

        'gvProduc1.DataSource = ldsResponse.Tables(2)
        'gvProduc1.DataBind()

        'gvProduc2.DataSource = ldsResponse.Tables(3)
        'gvProduc2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(5)
        gvFirmas.DataBind()

        'For Each lobjRow As GridViewRow In gvProduc2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        lintContadorP1 = lintContadorP1 + 1
        '    End If
        'Next

        'If lintContadorP1 = gvProduc2.Rows.Count Then
        '    'hdfPanel2.Value = ""
        '    pnlProduccion2.Style.Add("display", "none")
        'Else
        '    'hdfPanel2.Value = "S"
        '    pnlProduccion2.Style.Add("display", "block")
        'End If

        'gvProduc3.DataSource = ldsResponse.Tables(4)
        'gvProduc3.DataBind()

        'For Each lobjRow As GridViewRow In gvProduc3.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        lintContadorP2 = lintContadorP2 + 1
        '    End If
        'Next

        'If lintContadorP2 = gvProduc3.Rows.Count Then
        '    'hdfPanel3.Value = ""
        '    pnlProduccion3.Style.Add("display", "none")
        'Else
        '    'hdfPanel3.Value = "S"
        '    pnlProduccion3.Style.Add("display", "block")
        'End If

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_FORMATO").ToString().Equals("ACT") Then
            btnGuardar.Visible = True
            btnSolicitar.Visible = False
            btnAprobar.Visible = False
            btnAnular.Visible = False
        End If

    End Sub

#End Region

#Region "Eventos"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Session("@USUARIO") = "JRUIZS"
            fn_CargarListas()
            fn_ObetnerPaises()
            fn_ObtenerTablaMAestra()
            If Not Request("pidCodGenFormato") Is Nothing Then
                Session("CodGenFormato") = Request("pidCodGenFormato").ToString()
                fn_ObtenerExisteFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
                btnHojaRuta.Visible = True
            Else
                btnHojaRuta.Visible = False
                fn_ObtenerParametrosFormulario()
            End If
        End If
    End Sub

    'Protected Sub ibtAgregarProd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtAgregarProd.Click
    '    If hdfPanel2.Value.Equals("") Then
    '        hdfPanel2.Value = "S"
    '        pnlProduccion2.Style.Add("display", "block")
    '        'Dim lobjGerencia As New clsGerencia
    '        'Dim ldtResponse As DataTable
    '        'ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '        '                                               2,
    '        '                                               "PRODNI2",
    '        '                                               "",
    '        '                                               "",
    '        '                                               Session("@USUARIO").ToString())

    '        'gvProduc2.DataSource = ldtResponse
    '        'gvProduc2.DataBind()
    '        Exit Sub
    '    End If

    '    If hdfPanel3.Value.Equals("") Then
    '        hdfPanel3.Value = "S"
    '        pnlProduccion3.Style.Add("display", "block")
    '        ibtAgregarProd.Visible = False
    '        'Dim lobjGerencia As New clsGerencia
    '        'Dim ldtResponse As DataTable
    '        'ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '        '                                                2,
    '        '                                                "PRODNI3",
    '        '                                                "",
    '        '                                                "",
    '        '                                                Session("@USUARIO").ToString())

    '        'gvProduc3.DataSource = ldtResponse
    '        'gvProduc3.DataBind()
    '        Exit Sub
    '    End If
    'End Sub

    'Protected Sub ibtMenos2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtMenos2.Click
    '    Dim lobjGerencia As New clsGerencia
    '    Dim ldtResponse As DataTable

    '    'hdfPanel2.Value = ""
    '    'pnlProduccion2.Style.Add("display", "none")
    '    'For Each lobjRow As GridViewRow In gvProduc2.Rows
    '    '    Dim ltxtValor As New TextBox
    '    '    ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

    '    '    ltxtValor.Text = ""




    '    '    'ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
    '    '    '                                                 "",
    '    '    '                                                 Session("@USUARIO").ToString())
    '    '    ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdFormato.Value),
    '    '                                                Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
    '    '                                                "PRODNI2",
    '    '                                                2)










    '    '    gvProduc2.DataSource = ldtResponse
    '    '    gvProduc2.DataBind()
    '    'Next
    '    'ibtAgregarProd.Visible = True
    'End Sub

    'Protected Sub ibtMenos3_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtMenos3.Click
    '    Dim lobjGerencia As New clsGerencia
    '    Dim ldtResponse As DataTable

    '    'hdfPanel3.Value = ""
    '    pnlProduccion3.Style.Add("display", "none")
    '    For Each lobjRow As GridViewRow In gvProduc3.Rows
    '        Dim ltxtValor As New TextBox
    '        ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

    '        ltxtValor.Text = ""


    '        'ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
    '        '                                                 "",
    '        '                                                 Session("@USUARIO").ToString())
    '        ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdFormato.Value),
    '                                                    Convert.ToInt32(gvProduc3.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
    '                                                    "PRODNI3",
    '                                                    2)

    '        gvProduc3.DataSource = ldtResponse
    '        gvProduc3.DataBind()
    '    Next
    '    'ibtAgregarProd.Visible = True
    'End Sub

    Protected Sub btnHojaRuta_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHojaRuta.Click
        _hojaRuta = True
        btnGuardar_Click(sender, New EventArgs)
        If ddlTipoPrueba.SelectedValue.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el tipo de prueba.');</script>")
            Exit Sub
        Else
            Session("TipoPrueba") = ddlTipoPrueba.SelectedValue.ToString().Substring(0, 1)
            Response.Redirect("frmHojaRuta.aspx?TipoPrueba=" + ddlTipoPrueba.SelectedValue.ToString() + "&cod=" + Session("CodGenFormato"))
        End If

    End Sub

    'Protected Sub gvBloque1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvBloque1.RowCommand
    '    If e.CommandName = "Agregar" Then
    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable
    '        Dim gvr As GridViewRow
    '        'Dim ltxtCampoF As TextBox
    '        Dim ltxtValorF As TextBox

    '        gvr = CType(e.CommandSource, ImageButton).NamingContainer

    '        'ltxtCampoF = CType(gvBloque1.FooterRow.FindControl("txtCampoF"), TextBox)
    '        ltxtValorF = CType(gvBloque1.FooterRow.FindControl("txtValorF"), TextBox)

    '        'If ltxtCampoF.Text.Trim.Equals("") Then
    '        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
    '        '    Exit Sub
    '        'End If

    '        If ltxtValorF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
    '            Exit Sub
    '        End If

    '        'CAMBIO JR - SOLO UN CAMPO - INI
    '        'ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '        '                                                2,
    '        '                                                "BLQINFOPARAM1",
    '        '                                                ltxtCampoF.Text.Trim().ToUpper(),
    '        '                                                ltxtValorF.Text.Trim().ToUpper(),
    '        '                                                Session("@USUARIO").ToString())
    '        ldtResponse = lobjGerencia.fn_AgregarParametrosInformacion(Convert.ToInt32(hdfIdFormato.Value),
    '                                                        2,
    '                                                        "BLQINFOPARAM1",
    '                                                        ltxtValorF.Text.Trim().ToUpper(),
    '                                                        Session("@USUARIO").ToString())
    '        'CAMBIO JR - SOLO UN CAMPO - FIN
    '        gvBloque1.DataSource = ldtResponse
    '        gvBloque1.DataBind()
    '    End If

    '    If e.CommandName = "Eliminar" Then

    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable

    '        ldtResponse = lobjGerencia.fn_EliminarParamInformacion(Convert.ToInt32(hdfIdFormato.Value),
    '                                                    Convert.ToInt32(e.CommandArgument),
    '                                                    "BLQINFOPARAM1",
    '                                                    2)

    '        gvBloque1.DataSource = ldtResponse
    '        gvBloque1.DataBind()

    '    End If
    'End Sub

    'Protected Sub gvBloque2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvBloque2.RowCommand
    '    If e.CommandName = "Agregar" Then
    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable
    '        Dim gvr As GridViewRow
    '        Dim ltxtCampoF As TextBox
    '        Dim ltxtValorF As TextBox

    '        gvr = CType(e.CommandSource, ImageButton).NamingContainer

    '        ltxtCampoF = CType(gvBloque2.FooterRow.FindControl("txtCampoF"), TextBox)
    '        ltxtValorF = CType(gvBloque2.FooterRow.FindControl("txtValorF"), TextBox)

    '        If ltxtCampoF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
    '            Exit Sub
    '        End If

    '        If ltxtValorF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
    '            Exit Sub
    '        End If

    '        ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '                                                        2,
    '                                                        "BLQINFOPARAM2",
    '                                                        ltxtCampoF.Text.Trim().ToUpper(),
    '                                                        ltxtValorF.Text.Trim().ToUpper(),
    '                                                        Session("@USUARIO").ToString())

    '        gvBloque2.DataSource = ldtResponse
    '        gvBloque2.DataBind()
    '    End If

    '    If e.CommandName = "Eliminar" Then

    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable

    '        ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdFormato.Value),
    '                                                    Convert.ToInt32(e.CommandArgument),
    '                                                    "BLQINFOPARAM2",
    '                                                    2)

    '        gvBloque2.DataSource = ldtResponse
    '        gvBloque2.DataBind()

    '    End If
    'End Sub

    'Protected Sub gvProduc1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduc1.RowCommand
    '    If e.CommandName = "Agregar" Then
    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable
    '        Dim gvr As GridViewRow
    '        Dim ltxtCampoF As TextBox
    '        Dim ltxtValorF As TextBox

    '        gvr = CType(e.CommandSource, ImageButton).NamingContainer

    '        ltxtCampoF = CType(gvProduc1.FooterRow.FindControl("txtCampoF1"), TextBox)
    '        ltxtValorF = CType(gvProduc1.FooterRow.FindControl("txtValorF1"), TextBox)

    '        If ltxtCampoF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
    '            Exit Sub
    '        End If

    '        If ltxtValorF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
    '            Exit Sub
    '        End If

    '        ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '                                                        2,
    '                                                        "PRODNI1",
    '                                                        ltxtCampoF.Text.Trim().ToUpper(),
    '                                                        ltxtValorF.Text.Trim().ToUpper(),
    '                                                        Session("@USUARIO").ToString())

    '        gvProduc1.DataSource = ldtResponse
    '        gvProduc1.DataBind()
    '    End If

    '    If e.CommandName = "Eliminar" Then

    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable

    '        ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdFormato.Value),
    '                                                    Convert.ToInt32(e.CommandArgument),
    '                                                    "PRODNI1",
    '                                                    2)

    '        gvProduc1.DataSource = ldtResponse
    '        gvProduc1.DataBind()

    '    End If
    'End Sub

    'Protected Sub gvProduc2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduc2.RowCommand
    '    If e.CommandName = "Agregar" Then
    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable
    '        Dim gvr As GridViewRow
    '        Dim ltxtCampoF As TextBox
    '        Dim ltxtValorF As TextBox

    '        gvr = CType(e.CommandSource, ImageButton).NamingContainer

    '        ltxtCampoF = CType(gvProduc2.FooterRow.FindControl("txtCampoF2"), TextBox)
    '        ltxtValorF = CType(gvProduc2.FooterRow.FindControl("txtValorF2"), TextBox)

    '        If ltxtCampoF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
    '            Exit Sub
    '        End If

    '        If ltxtValorF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
    '            Exit Sub
    '        End If

    '        ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '                                                        2,
    '                                                        "PRODNI2",
    '                                                        ltxtCampoF.Text.Trim().ToUpper(),
    '                                                        ltxtValorF.Text.Trim().ToUpper(),
    '                                                        Session("@USUARIO").ToString())

    '        gvProduc2.DataSource = ldtResponse
    '        gvProduc2.DataBind()
    '    End If

    '    If e.CommandName = "Eliminar" Then

    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable

    '        ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdFormato.Value),
    '                                                    Convert.ToInt32(e.CommandArgument),
    '                                                    "PRODNI2",
    '                                                    2)

    '        gvProduc2.DataSource = ldtResponse
    '        gvProduc2.DataBind()

    '    End If
    'End Sub

    'Protected Sub gvProduc3_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduc3.RowCommand
    '    If e.CommandName = "Agregar" Then
    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable
    '        Dim gvr As GridViewRow
    '        Dim ltxtCampoF As TextBox
    '        Dim ltxtValorF As TextBox

    '        gvr = CType(e.CommandSource, ImageButton).NamingContainer

    '        ltxtCampoF = CType(gvProduc3.FooterRow.FindControl("txtCampoF3"), TextBox)
    '        ltxtValorF = CType(gvProduc3.FooterRow.FindControl("txtValorF3"), TextBox)

    '        If ltxtCampoF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el nombre del campo.');</script>")
    '            Exit Sub
    '        End If

    '        If ltxtValorF.Text.Trim.Equals("") Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el valor del campo.');</script>")
    '            Exit Sub
    '        End If

    '        ldtResponse = lobjGerencia.fn_AgregarParametros(Convert.ToInt32(hdfIdFormato.Value),
    '                                                        2,
    '                                                        "PRODNI3",
    '                                                        ltxtCampoF.Text.Trim().ToUpper(),
    '                                                        ltxtValorF.Text.Trim().ToUpper(),
    '                                                        Session("@USUARIO").ToString())

    '        gvProduc3.DataSource = ldtResponse
    '        gvProduc3.DataBind()
    '    End If

    '    If e.CommandName = "Eliminar" Then

    '        Dim lobjGerencia As New clsGerencia
    '        Dim ldtResponse As DataTable

    '        ldtResponse = lobjGerencia.fn_EliminarParam(Convert.ToInt32(hdfIdFormato.Value),
    '                                                    Convert.ToInt32(e.CommandArgument),
    '                                                    "PRODNI3",
    '                                                    2)

    '        gvProduc3.DataSource = ldtResponse
    '        gvProduc3.DataBind()

    '    End If
    'End Sub

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

            ldtResponse = lobjGerencia.fn_AgregarFirmas(Convert.ToInt32(hdfIdFormato.Value),
                                                        2,
                                                        lddlFirmas.SelectedValue.ToString(),
                                                        Session("@USUARIO").ToString())

            gvFirmas.DataSource = ldtResponse
            gvFirmas.DataBind()

        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_EliminarFirma(Convert.ToInt32(hdfIdFormato.Value),
                                                        2,
                                                        e.CommandArgument)

            gvFirmas.DataSource = ldtResponse
            gvFirmas.DataBind()

        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        gbolValidar = True

        'CAMBIO JR - RESPONSABLE EN TEXTBOX - INI
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
        'CAMBIO JR - RESPONSABLE EN TEXTBOX - FIN

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

        If hdfCodProv.Value.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el proveedor.');</script>")
            Exit Sub
        End If

        If txtDetProc.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese detalle del producto.');</script>")
            Exit Sub
        End If

        If txtProcProd.Text.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese proceso del producto.');</script>")
            Exit Sub
        End If

        'For Each lobjRow As GridViewRow In gvBloque1.Rows
        '    Dim ltxtValor As New TextBox
        '    'Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        gbolValidar = False
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
        '        Exit Sub
        '    End If

        '    'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
        '    'Exit Sub

        'Next

        'CAMBIO JR - RIESGO ES TXT , NO GRILLA - INI
        'For Each lobjRow As GridViewRow In gvBloque2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvBloque2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
        '    If ltxtValor.Text.Trim().Equals("") Then
        '        gbolValidar = False
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese parámetros relevantes a evaluar.');</script>")
        '        Exit Sub
        '    End If
        'Next





        'CAMBIO JR - RIESGO ES TXT , NO GRILLA - FIN

        'For Each lobjRow As GridViewRow In gvProduc1.Rows
        '    Dim ltxtValor As New TextBox
        '    'Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        gbolValidar = False
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
        '        Exit Sub
        '    End If

        '    'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 1.');</script>")
        '    'Exit Sub

        'Next

        'If hdfPanel2.Value.Equals("S") Then
        '    For Each lobjRow As GridViewRow In gvProduc2.Rows
        '        Dim ltxtValor As New TextBox
        '        'Dim lbolValidar As Boolean = False
        '        ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '        If ltxtValor.Text.Trim().Equals("") Then
        '            gbolValidar = False
        '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
        '            Exit Sub
        '        End If

        '        'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 2.');</script>")
        '        'Exit Sub

        '    Next
        'End If

        'If hdfPanel3.Value.Equals("S") Then
        '    For Each lobjRow As GridViewRow In gvProduc3.Rows
        '        Dim ltxtValor As New TextBox
        '        'Dim lbolValidar As Boolean = False
        '        ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

        '        If ltxtValor.Text.Trim().Equals("") Then
        '            gbolValidar = False
        '            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
        '            Exit Sub
        '        End If

        '        'ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese datos de producción 3.');</script>")
        '        'Exit Sub

        '    Next
        'End If

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



        If ddlTipoPrueba.SelectedValue.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el tipo de prueba.');</script>")
            Exit Sub
        End If


        If ddlFabricante.SelectedValue.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el fabricante.');</script>")
            Exit Sub
        End If

        If ddlProcedenciaPais.SelectedValue.Trim().Equals("") Then
            gbolValidar = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el país de procedencia.');</script>")
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

        'CAMBIO JR - CAMBIO DE RESPONSABLE TEXTBOX - INI
        'ldtResponse = lobjGerencia.fn_ModificarFormatoProt_2(Convert.ToInt32(hdfIdFormato.Value),
        '                                                     ddlResponsable.SelectedValue,
        '                                                     Convert.ToDateTime(txtFecInicio.Text.Trim()).ToString("yyyyMMdd"),
        '                                                     hdfCodProv.Value,
        '                                                     txtDetProc.Text.ToUpper(),
        '                                                     txtProcProd.Text.ToUpper(),
        '                                                     txtObjCambio.Text.ToUpper(),
        '                                                     Convert.ToDateTime(txtFecConc.Text.Trim()).ToString("yyyyMMdd"),
        '                                                     txtDescConcl.Text.ToUpper(),
        '                                                     Session("@USUARIO").ToString())


        Dim lhpfFileficha, lhpfFilecertificado, lhpfFilecarta, lhpfFiledocumento As HttpPostedFile
        Dim lbrFragmentarficha, lbrFragmentarcertificado, lbrFragmentarcarta, lbrFragmentardocumento As BinaryReader
        Dim lbytBufferficha, lbytBuffercertificado, lbytBuffercarta, lbytBufferdocumento As Byte()
        Dim lstrFileNameficha As String = ""
        Dim lstrFileNamecertificado As String = ""
        Dim lstrFileNamecarta As String = ""
        Dim lstrFileNamedocumento As String = ""
        Dim lintFileSizeficha As Integer = 0
        Dim lintFileSizecertificado As Integer = 0
        Dim lintFileSizecarta As Integer = 0
        Dim lintFileSizedocumento As Integer = 0
        Dim lstrContentTypeficha As String = ""
        Dim lstrContentTypecertificado As String = ""
        Dim lstrContentTypecarta As String = ""
        Dim lstrContentTypedocumento As String = ""
        Dim lstrFileExtensionficha As String = ""
        Dim lstrFileExtensioncertificado As String = ""
        Dim lstrFileExtensioncarta As String = ""
        Dim lstrFileExtensiondocumento As String = ""
        Dim lbyteContentficha, lbyteContentcertificado, lbyteContentcarta, lbyteContentdocumento As Byte()
        Dim lstrRutaficha As String = ""
        Dim lstrRutacertificado As String = ""
        Dim lstrRutacarta As String = ""
        Dim lstrRutadocumento As String = ""
        Dim larraySeparatorFileficha, larraySeparatorFilecertificado, larraySeparatorFilecarta, larraySeparatorFiledocumento As String()



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
        ''''---------------

        If fluAdjuntarCertificadoCalidad.HasFile Then
            lhpfFilecertificado = fluAdjuntarCertificadoCalidad.PostedFile
            lbrFragmentarcertificado = New BinaryReader(lhpfFilecertificado.InputStream)
            lbytBuffercertificado = lbrFragmentarcertificado.ReadBytes(lhpfFilecertificado.ContentLength)

            lstrFileNamecertificado = Path.GetFileName(lhpfFilecertificado.FileName)
            lintFileSizecertificado = lhpfFilecertificado.ContentLength
            lstrContentTypecertificado = lhpfFilecertificado.ContentType
            lstrFileExtensioncertificado = Path.GetExtension(lhpfFilecertificado.FileName)
            lbyteContentcertificado = lbytBuffercertificado

            larraySeparatorFilecertificado = Split(fluAdjuntarCertificadoCalidad.FileName, ".")

            lstrFileNamecertificado = larraySeparatorFilecertificado(0) + Path.GetExtension(fluAdjuntarCertificadoCalidad.FileName)

            lstrRutacertificado = Server.MapPath("~/Archivos/") + lstrFileNamecertificado

            fluAdjuntarCertificadoCalidad.SaveAs(lstrRutacertificado)
        End If
        '''''''''''''''''''''---------
        If fluAdjuntarCartaCompremiso.HasFile Then
            lhpfFilecarta = fluAdjuntarCartaCompremiso.PostedFile
            lbrFragmentarcarta = New BinaryReader(lhpfFilecarta.InputStream)
            lbytBuffercarta = lbrFragmentarcarta.ReadBytes(lhpfFilecarta.ContentLength)

            lstrFileNamecarta = Path.GetFileName(lhpfFilecarta.FileName)
            lintFileSizecarta = lhpfFilecarta.ContentLength
            lstrContentTypecarta = lhpfFilecarta.ContentType
            lstrFileExtensioncarta = Path.GetExtension(lhpfFilecarta.FileName)
            lbyteContentcarta = lbytBuffercarta

            larraySeparatorFilecarta = Split(fluAdjuntarCartaCompremiso.FileName, ".")

            lstrFileNamecarta = larraySeparatorFilecarta(0) + Path.GetExtension(fluAdjuntarCartaCompremiso.FileName)

            lstrRutacarta = Server.MapPath("~/Archivos/") + lstrFileNamecarta

            fluAdjuntarCartaCompremiso.SaveAs(lstrRutacarta)
        End If

        If fluAdjuntarDocAdicional.HasFile Then
            lhpfFiledocumento = fluAdjuntarDocAdicional.PostedFile
            lbrFragmentardocumento = New BinaryReader(lhpfFiledocumento.InputStream)
            lbytBufferdocumento = lbrFragmentardocumento.ReadBytes(lhpfFiledocumento.ContentLength)

            lstrFileNamedocumento = Path.GetFileName(lhpfFiledocumento.FileName)
            lintFileSizedocumento = lhpfFiledocumento.ContentLength
            lstrContentTypedocumento = lhpfFiledocumento.ContentType
            lstrFileExtensiondocumento = Path.GetExtension(lhpfFiledocumento.FileName)
            lbyteContentdocumento = lbytBufferdocumento

            larraySeparatorFiledocumento = Split(fluAdjuntarDocAdicional.FileName, ".")

            lstrFileNamedocumento = larraySeparatorFiledocumento(0) + Path.GetExtension(fluAdjuntarDocAdicional.FileName)

            lstrRutadocumento = Server.MapPath("~/Archivos/") + lstrFileNamedocumento

            fluAdjuntarDocAdicional.SaveAs(lstrRutadocumento)
        End If
        ''''''----
        '--------------------------------
        ldtResponse = lobjGerencia.fn_ModificarFormatoProt_2(Convert.ToInt32(hdfIdFormato.Value),
                                                             txtResponsable.Text,
                                                             txtFecInicio.Text.Trim(),
                                                             hdfCodProv.Value,
                                                             txtDetProc.Text.ToUpper(),
                                                             txtProcProd.Text.ToUpper(),
                                                             txtObjCambio.Text.ToUpper(),
                                                             txtConsRiesgo.Text.ToUpper(),
                                                             txtFecConc.Text.Trim(),
                                                             txtDescConcl.Text.ToUpper(),
                                                             Session("@USUARIO").ToString(),
                                                             txtFecInicio.Text.Trim(),
                                                             ddlTipoPrueba.SelectedValue.ToString(),
                                                             ddlFabricante.SelectedValue.ToString(),
                                                             txtPlanta.Text,
                                                             ddlProcedenciaPais.SelectedValue.ToString(),
                                                             txtUNidadMedida.Text,
                                                             Convert.ToDouble(txtCantidad.Text),
                                                             txtLote.Text,
                                                             txtFechaProduccion.Text,
                                                             txtUbicacionNUevoMaterial.Text,
                                                             txtCaracteristicasEmbalaje.Text,
                                                             txtCodigoMaterial.Text,
                                                             txtCaracteristicasRelevantesMaterial.Text,
                                                             IIf(txtAprobado.Text <> "", txtAprobado.Text, IIf(txtDesaprobado.Text <> "", txtDesaprobado.Text, "")),
                                                             txtObservacionesInsumos.Text,
                                                             txtNroPreliminar.Text,
                                                              lstrFileNameficha, lintFileSizeficha, lstrContentTypeficha, lstrFileExtensionficha,
                                                              lstrFileNamecertificado, lintFileSizecertificado, lstrContentTypecertificado, lstrFileExtensioncertificado,
                                                              lstrFileNamecarta, lintFileSizecarta, lstrContentTypecarta, lstrFileExtensioncarta,
                                                              lstrFileNamedocumento, lintFileSizedocumento, lstrContentTypedocumento, lstrFileExtensiondocumento)
        'CAMBIO JR - CAMBIO DE RESPONSABLE TEXTBOX - FIN

        Session("ldtResponse") = ldtResponse
        'For Each lobjRow As GridViewRow In gvBloque1.Rows
        '    Dim ltxtValor As New TextBox
        '    Dim lstrKeyName As String = ""
        '    ltxtValor = CType(gvBloque1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

        '    lstrKeyName = gvBloque1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()

        '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(lstrKeyName),
        '                                                     ltxtValor.Text.Trim().ToUpper(),
        '                                                     Session("@USUARIO").ToString())

        'Next

        'For Each lobjRow As GridViewRow In gvBloque2.Rows
        '    Dim ltxtValor As New TextBox
        '    Dim lstrKeyName As String = ""
        '    ltxtValor = CType(gvBloque2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    lstrKeyName = gvBloque2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()

        '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(lstrKeyName),
        '                                                     ltxtValor.Text.Trim().ToUpper(),
        '                                                     Session("@USUARIO").ToString())
        'Next

        'For Each lobjRow As GridViewRow In gvProduc1.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc1.Rows(lobjRow.RowIndex).FindControl("txtValor1"), TextBox)

        '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
        '                                                     ltxtValor.Text.Trim().ToUpper(),
        '                                                     Session("@USUARIO").ToString())

        'Next

        'For Each lobjRow As GridViewRow In gvProduc2.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
        '                                                     ltxtValor.Text.Trim().ToUpper(),
        '                                                     Session("@USUARIO").ToString())

        'Next

        'For Each lobjRow As GridViewRow In gvProduc3.Rows
        '    Dim ltxtValor As New TextBox
        '    ltxtValor = CType(gvProduc3.Rows(lobjRow.RowIndex).FindControl("txtValor3"), TextBox)

        '    ldtResponse = lobjGerencia.fn_ModificarParamForm(Convert.ToInt32(gvProduc3.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()),
        '                                                     ltxtValor.Text.Trim().ToUpper(),
        '                                                     Session("@USUARIO").ToString())

        'Next
        'If _hojaRuta = False Then
        '    Response.Redirect("frmListFormatoNuevoInsumo.aspx")
        'End If
        btnHojaRuta.Visible = True

    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        Try
            fn_SolicitarAprobacion(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
        Catch ex As Exception
            'fn_SolicitarAprobacion(Convert.ToInt32(hdfIdFormato.Value))
        End Try
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        fn_anularFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
    End Sub

    Protected Sub imgbtnVolver_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVolver.Click
        Response.Redirect("frmListFormatoNuevoInsumo.aspx")
    End Sub

    Protected Sub btnAprobar_Click(sender As Object, e As EventArgs) Handles btnAprobar.Click
        fn_aprobarFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()), 2)
    End Sub

#End Region

    'Private Sub gvBloque1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBloque1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
    '            Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
    '            Dim txtValor As TextBox = CType(e.Row.FindControl("txtValor"), TextBox)
    '            If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
    '                btnEliminar.Visible = False
    '                txtValor.Visible = False
    '            Else
    '                btnEliminar.Visible = True
    '                'txtValor.Enabled = True
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub gvProduc1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduc1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
    '            Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
    '            Dim txtValor1 As TextBox = CType(e.Row.FindControl("txtValor1"), TextBox)
    '            Dim txtCampo1 As TextBox = CType(e.Row.FindControl("txtCampo1"), TextBox)
    '            If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
    '                btnEliminar.Visible = False
    '                txtValor1.Visible = False
    '                txtCampo1.Visible = False
    '            Else
    '                btnEliminar.Visible = True
    '                'txtValor1.Enabled = True
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub gvProduc2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduc2.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
    '            Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
    '            Dim txtValor2 As TextBox = CType(e.Row.FindControl("txtValor2"), TextBox)
    '            Dim txtCampo2 As TextBox = CType(e.Row.FindControl("txtCampo2"), TextBox)
    '            If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
    '                btnEliminar.Visible = False
    '                txtValor2.Visible = False
    '                txtCampo2.Visible = False
    '            Else
    '                btnEliminar.Visible = True
    '                'txtValor2.Enabled = True
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub gvProduc3_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduc3.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
    '            Dim btnEliminar As ImageButton = CType(e.Row.FindControl("btnEliminar"), ImageButton)
    '            Dim txtValor3 As TextBox = CType(e.Row.FindControl("txtValor3"), TextBox)
    '            Dim txtCampo3 As TextBox = CType(e.Row.FindControl("txtCampo3"), TextBox)
    '            If DataBinder.Eval(e.Row.DataItem, "VCH_VAL_CAMPO").ToString() = "" Then
    '                btnEliminar.Visible = False
    '                txtValor3.Visible = False
    '                txtCampo3.Visible = False
    '            Else
    '                btnEliminar.Visible = True
    '                'txtValor3.Enabled = True
    '            End If
    '        End If
    '    End If
    'End Sub

    'Protected Sub gvProduc3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvProduc3.SelectedIndexChanged

    'End Sub
    Protected Sub ibtDescargarcarta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDescargarcarta.Click
        Dim ldtResponse As DataTable
        ldtResponse = Session("ldtResponse")
        If ldtResponse.Rows().Count > 0 Then
            If ldtResponse.Rows(0).Item("VCH_FILENAMECARTA").ToString() <> "" Then
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ldtResponse.Rows(0).Item("VCH_FILENAMECARTA").ToString())
                Response.TransmitFile(Server.MapPath("~/Archivos/") + ldtResponse.Rows(0).Item("VCH_FILENAMECARTA").ToString())
                Response.End()
            End If
        End If
    End Sub
    Protected Sub ibtDescargarficha_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDescargarfichas.Click
        Dim ldtResponse As DataTable
        ldtResponse = Session("ldtResponse")
        If ldtResponse.Rows().Count > 0 Then
            If ldtResponse.Rows(0).Item("VCH_FILENAMEFICHA").ToString() <> "" Then
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ldtResponse.Rows(0).Item("VCH_FILENAMEFICHA").ToString())
                Response.TransmitFile(Server.MapPath("~/Archivos/") + ldtResponse.Rows(0).Item("VCH_FILENAMEFICHA").ToString())
                Response.End()
            End If
        End If
    End Sub
    Protected Sub ibtDescargarcertificado_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDescargarcertificados.Click
        Dim ldtResponse As DataTable
        ldtResponse = Session("ldtResponse")
        If ldtResponse.Rows().Count > 0 Then
            If ldtResponse.Rows(0).Item("VCH_FILENAMECERTIFICADO").ToString() <> "" Then
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ldtResponse.Rows(0).Item("VCH_FILENAMECERTIFICADO").ToString())
                Response.TransmitFile(Server.MapPath("~/Archivos/") + ldtResponse.Rows(0).Item("VCH_FILENAMECERTIFICADO").ToString())
                Response.End()
            End If
        End If
    End Sub
    Protected Sub ibtDescargarDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDescargarDocumento.Click
        Dim ldtResponse As DataTable
        ldtResponse = Session("ldtResponse")
        If ldtResponse.Rows().Count > 0 Then
            If ldtResponse.Rows(0).Item("VCH_FILENAMEDOCUMENTO").ToString() <> "" Then
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ldtResponse.Rows(0).Item("VCH_FILENAMEDOCUMENTO").ToString())
                Response.TransmitFile(Server.MapPath("~/Archivos/") + ldtResponse.Rows(0).Item("VCH_FILENAMEDOCUMENTO").ToString())
                Response.End()
            End If
        End If
    End Sub
    Protected Sub gvFirmas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFirmas.SelectedIndexChanged


    End Sub
End Class


