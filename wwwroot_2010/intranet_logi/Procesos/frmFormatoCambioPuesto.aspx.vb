Imports NM_General
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class frmFormatoCambioPuesto
    Inherits System.Web.UI.Page

    Dim gbolValidar As Boolean = True

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
        txtFecInicio.Enabled = False
        'ibtCalendar1.Style.Add("visibility", "hidden")
        txtNombres.Enabled = False
        txtCodigo.Enabled = False
        txtPuestoAct.Enabled = False
        txtPuestoProm.Enabled = False
        btnSolicitante.Style.Add("visibility", "hidden")
        txtArea.Enabled = False
        txtFecIni.Enabled = False
        ibtCalendar2.Style.Add("visibility", "hidden")
        txtSuper.Enabled = False
        btnJefe.Style.Add("visibility", "hidden")
        txtCompa.Enabled = False
        btnCompa.Style.Add("visibility", "hidden")
        txtTiempo.Enabled = False
        For Each lobjRow As GridViewRow In gvEval1.Rows
            Dim ltxtValor As New TextBox
            Dim dwlPuntaje As New DropDownList
            'ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)
            dwlPuntaje = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("dwlPuntaje"), DropDownList)
            dwlPuntaje.Enabled = False
            'ltxtValor.Enabled = False
        Next
        For Each lobjRow As GridViewRow In gvEval2.Rows
            Dim ltxtValor As New TextBox
            Dim dwlPuntaje As New DropDownList
            'ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
            dwlPuntaje = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("dwlPuntaje"), DropDownList)
            dwlPuntaje.Enabled = False
            'ltxtValor.Enabled = False
        Next
        txtOpinion.Enabled = False
        gvFirmas.Columns(3).Visible = False
        gvFirmas.FooterRow.Visible = False
    End Sub

    Sub fn_enviarCorreoFormatos(ByVal pstrRuta As String)

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_enviarCorreoFormatos("FORMATO CAMBIO DE PUESTO", pstrRuta)

    End Sub

    Sub fn_generarFormatoPDF(ByVal pintCodGenFor As Int32, ByVal pintCodFormato As Integer)
        Dim MyReportViewer As New ReportViewer
        Dim ReportServerCredentials As IReportServerCredentials
        ReportServerCredentials = New ReportViewerCredentials("nmsic", "Asmrp.159", "NUEVOMUNDOSA")

        File.Delete(Server.MapPath("~/Procesos/Formatos/PDF") + "/FormatoCambioPuesto_" + pintCodGenFor.ToString() + ".pdf")

        MyReportViewer.ProcessingMode = ProcessingMode.Remote
        MyReportViewer.ServerReport.ReportServerCredentials = ReportServerCredentials
        MyReportViewer.ServerReport.ReportServerUrl = New Uri("http://mundodesa02:8081/reportserver")
        MyReportViewer.ServerReport.ReportPath = "/Administrativo_NuevoMundo/Gerencia/gg_formato_cambio_puesto"
        'MyReportViewer.ServerReport.DisplayName = "gg_formato_cambio_proceso"
        MyReportViewer.ServerReport.Refresh()
        Dim reportParameterCollection(1) As ReportParameter
        reportParameterCollection(0) = New ReportParameter
        reportParameterCollection(0).Name = "pintCodGenerado"
        reportParameterCollection(0).Values.Add(pintCodGenFor)
        reportParameterCollection(1) = New ReportParameter
        reportParameterCollection(1).Name = "pintCodFormato"
        reportParameterCollection(1).Values.Add(5)
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

        Dim pdfPath As String = Server.MapPath("~/Procesos/Formatos/PDF") + "/FormatoCambioPuesto_" + pintCodGenFor.ToString() + "." + extension
        'Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
        'pdfFile.Write(bytes, 0, bytes.Length)
        'pdfFile.Close()

        System.IO.File.WriteAllBytes(pdfPath, bytes)

        'fn_enviarCorreoFormatos("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoCambioPuesto_" + pintCodGenFor.ToString() + ".pdf")
        Dim dt As DataTable = fn_enviarCorreoFormatos_V2("\\SERVNMPRB\c$\Inetpub\wwwroot\intranet_logi\Procesos\Formatos\PDF\FormatoCambioPuesto_" + pintCodGenFor.ToString() + ".pdf", pintCodGenFor, pintCodFormato)
        'Response.Flush()
        'Response.End()

        'Response.Redirect("frmListFormatoCambioProceso.aspx")

    End Sub
    Public Function fn_enviarCorreoFormatos_V2(ByVal pstrRuta As String, ByVal intCodFormulario As Int32, ByVal intCodFormato As Integer) As DataTable

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.enviarCorreoFormatos_V2("", pstrRuta, intCodFormulario, intCodFormato)
        Return ldtResponse
    End Function
    Sub fn_SolicitarAprobacion(ByVal pintCodGenFor As Int32)

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        If txtFecInicio.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese fecha.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFecInicio.Text.Trim())
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha válida.');</script>")
            Exit Sub
        End Try

        If txtNombres.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese nombres.');</script>")
            Exit Sub
        End If

        If txtCodigo.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese código.');</script>")
            Exit Sub
        End If

        If txtPuestoAct.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese puesto actual.');</script>")
            Exit Sub
        End If

        If txtPuestoProm.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese puesto al que se promociona.');</script>")
            Exit Sub
        End If

        If txtArea.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el área.');</script>")
            Exit Sub
        End If

        If txtFecIni.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese fecha de inicio.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFecIni.Text.Trim())
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha de inicio válida.');</script>")
            Exit Sub
        End Try

        If txtSuper.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese supervisor/jefe directo.');</script>")
            Exit Sub
        End If

        If txtCompa.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese trabajador asignado.');</script>")
            Exit Sub
        End If

        If txtTiempo.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese tiempo de servicio.');</script>")
            Exit Sub
        End If

        'For Each lobjRow As GridViewRow In gvEval1.Rows
        '    Dim ltxtValor As New TextBox
        '    Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

        '    Try
        '        Convert.ToInt32(ltxtValor.Text.Trim())
        '    Catch ex As Exception
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingres solo valores númericos.');</script>")
        '        Exit Sub
        '    End Try

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Complete puntuaciones de dominio principal de tareas.');</script>")
        '        Exit Sub
        '    End If
        'Next

        'For Each lobjRow As GridViewRow In gvEval2.Rows
        '    Dim ltxtValor As New TextBox
        '    Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    Try
        '        Convert.ToInt32(ltxtValor.Text.Trim())
        '    Catch ex As Exception
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingres solo valores númericos.');</script>")
        '        Exit Sub
        '    End Try

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Complete puntuaciones de dominio principal de tareas.');</script>")
        '        Exit Sub
        '    End If
        'Next

        If txtOpinion.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese opinión del responsable.');</script>")
            Exit Sub
        End If


        ldtResponse = lobjGerencia.fn_solicitarAprobacion("01",
                                                          pintCodGenFor,
                                                          5,
                                                          "SOL")

        Response.Redirect("frmListFormatoCambioPuesto.aspx")
    End Sub

    Sub fn_aprobarFormato(ByVal pintCodGenFor As Int32, ByVal pintCodFormato As Integer)
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        'ldtResponse = lobjGerencia.fn_solicitarAprobacion("01",
        '                                                  pintCodGenFor,
        '                                                  5,
        '                                                  "APR")
        ldtResponse = lobjGerencia.fn_aprobarFormatos("U",
                                                      pintCodGenFor,
                                                      5,
                                                      Session("@USUARIO").ToString())

        'fn_generarFormatoPDF(pintCodGenFor)

        If ldtResponse.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("200") Then
            'fn_ObtenerExisteFormato(pintCodGenFor)
            fn_generarFormatoPDF(pintCodGenFor, pintCodFormato)
            Response.Redirect("frmListFormatoCambioPuesto.aspx")
        ElseIf ldtResponse.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("300") Then
            'fn_ObtenerExisteFormato(pintCodGenFor)
            Response.Redirect("frmListFormatoCambioPuesto.aspx")
        End If

        'Response.Redirect("frmListFormatoCambioPuesto.aspx")

    End Sub

    Sub fn_anularFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        ldtResponse = lobjGerencia.fn_solicitarAprobacion("01",
                                                          pintCodGenFor,
                                                          5,
                                                          "ANU")

        Response.Redirect("frmListFormatoCambioPuesto.aspx")

    End Sub

    Sub fn_RegistrarParametros()

        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        For Each lobjRow As GridViewRow In gvEval1.Rows
            'Dim ltxtValor As New TextBox
            Dim ltxtCampo As New TextBox
            Dim ltxtIntOrden As New TextBox
            Dim dwlPuntaje As New DropDownList
            Dim lstrKeyName As String = ""
            'ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)
            ltxtCampo = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtCampo"), TextBox)
            ltxtIntOrden = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtIntOrden"), TextBox)
            dwlPuntaje = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("dwlPuntaje"), DropDownList)

            'lstrKeyName = gvEval1.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()

            'ldtResponse = lobjGerencia.fn_ModificarParamFormPuesto(Convert.ToInt32(lstrKeyName),
            '                                                 ltxtValor.Text.Trim().ToUpper(), hdfIdFormato.Value,
            '                                                 Session("@USUARIO").ToString())
            ldtResponse = lobjGerencia.fn_ModificarParamFormPuesto(ltxtIntOrden.Text,
                                                             ltxtCampo.Text.Trim().ToUpper(), dwlPuntaje.SelectedValue, hdfIdFormato.Value, "EVALUB1",
                                                             Session("@USUARIO").ToString())

        Next

        For Each lobjRow As GridViewRow In gvEval2.Rows
            'Dim ltxtValor As New TextBox
            Dim ltxtCampo As New TextBox
            Dim ltxtIntOrden As New TextBox
            Dim dwlPuntaje As New DropDownList
            Dim lstrKeyName As String = ""
            'ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
            ltxtCampo = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtCampo"), TextBox)
            ltxtIntOrden = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtIntOrden"), TextBox)
            'lstrKeyName = gvEval2.DataKeys(lobjRow.RowIndex)("INT_COD_GENPAR").ToString()
            dwlPuntaje = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("dwlPuntaje"), DropDownList)
            'ldtResponse = lobjGerencia.fn_ModificarParamFormPuesto(Convert.ToInt32(lstrKeyName),
            '                                                 ltxtValor.Text.Trim().ToUpper(), hdfIdFormato.Value,
            '                                                 Session("@USUARIO").ToString())
            ldtResponse = lobjGerencia.fn_ModificarParamFormPuesto(ltxtIntOrden.Text,
                                                             ltxtCampo.Text.Trim().ToUpper(), dwlPuntaje.SelectedValue, hdfIdFormato.Value, "EVALUB2",
                                                             Session("@USUARIO").ToString())

        Next

    End Sub

    Sub fn_ObtenerExisteFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim lintSuma As Int32 = 0
        Dim lintSuma_2 As Int32 = 0

        ldsResponse = lobjGerencia.fn_ObtenerFormato_5(pintCodGenFor,
                                                       5)

        hdfIdFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        txtNombres.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NOMBRE").ToString()
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC").ToString()
        txtCodigo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_TRAB").ToString()
        hdfPuestoA.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_PUEST_ACT").ToString()
        txtPuestoAct.Text = ldsResponse.Tables(0).Rows(0).Item("DE_PUES_TRAB_A").ToString()
        hdfCodPuesto.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_PUEST_PRO").ToString()
        txtPuestoProm.Text = ldsResponse.Tables(0).Rows(0).Item("DE_PUES_TRAB_P").ToString()
        txtFecIni.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_INI").ToString()
        hdfArea.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_AREA").ToString()
        txtArea.Text = ldsResponse.Tables(0).Rows(0).Item("DE_AREA").ToString()
        hdfCodJefe.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_SUPER").ToString()
        txtSuper.Text = ldsResponse.Tables(0).Rows(0).Item("NOM_SUP").ToString()
        hdfCodTrab.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_NOM_COMP").ToString()
        txtCompa.Text = ldsResponse.Tables(0).Rows(0).Item("NOM_COMP").ToString()
        txtTiempo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_TIEMPO_SERV").ToString()
        txtOpinion.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OPIN_RESP").ToString()

        gvEval1.DataSource = ldsResponse.Tables(1)
        gvEval1.DataBind()

        gvEval2.DataSource = ldsResponse.Tables(2)
        gvEval2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(3)
        gvFirmas.DataBind()

        For Each lobjRow As GridViewRow In gvEval1.Rows
            Dim ltxtValor As New TextBox
            Dim dwlPuntaje As New DropDownList
            dwlPuntaje = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("dwlPuntaje"), DropDownList)

            'ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)
            dwlPuntaje.SelectedValue = gvEval1.DataSource.Rows(lobjRow.RowIndex).Item("VCH_VAL_CAMPO") 'CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("VCH_VAL_CAMPO"), TextBox)


            If Not dwlPuntaje.SelectedValue.Trim().Equals("") Then
                lintSuma = lintSuma + Convert.ToInt32(dwlPuntaje.SelectedValue.Trim())
            End If
        Next
        txtValorTot1.InnerText = lintSuma.ToString()

     

        For Each lobjRow As GridViewRow In gvEval2.Rows
            Dim ltxtValor As New TextBox
            Dim dwlPuntaje As New DropDownList
            dwlPuntaje = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("dwlPuntaje"), DropDownList)
            'ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)
            'ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("VCH_VAL_CAMPO"), TextBox)
            dwlPuntaje.SelectedValue = gvEval2.DataSource.Rows(lobjRow.RowIndex).Item("VCH_VAL_CAMPO") 'CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("VCH_VAL_CAMPO"), TextBox)

            If Not dwlPuntaje.Text.Trim().Equals("") Then
                lintSuma_2 = lintSuma_2 + Convert.ToInt32(dwlPuntaje.SelectedValue.Trim())
            End If

            'If Not ltxtValor.Text.Trim().Equals("") Then
            '    lintSuma_2 = lintSuma_2 + Convert.ToInt32(ltxtValor.Text.Trim())
            'End If
        Next

        txtValorTot2.InnerText = lintSuma_2.ToString()

        If ldsResponse.Tables(0).Rows(0).Item("VCH_EST_FORMATO").ToString().Equals("ACT") Then
            'btnGuardar.Visible = True
            'btnSolicitar.Visible = True
            'btnAprobar.Visible = False
            'btnAnular.Visible = True
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
            'If Session("@USUARIO").ToString().Equals("AAMPUERP") And
            '    Session("@USUARIO").ToString().Equals("DARWIN") Then
            '    btnAprobar.Visible = True
            'End If
            btnAnular.Visible = False
            fn_bloquearControles()
            gvFirmas.Columns(2).Visible = True

            ldtResponseApro = lobjGerencia.fn_aprobarFormatos("B",
                                                              pintCodGenFor,
                                                              5,
                                                              Session("@USUARIO").ToString())

            If ldtResponseApro.Rows(0).Item("vchCodigoRespuesta").ToString().Equals("200") Then
                btnAprobar.Visible = True
            Else
                btnAprobar.Visible = False
            End If
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
    End Sub

    Sub fn_ObtenerParametrosFormulario()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim lintSuma As Int32 = 0
        Dim lintSuma_2 As Int32 = 0

        ldsResponse = lobjGerencia.fn_GeneraFormato_5("01",
                                                      0,
                                                      1,
                                                      Session("@USUARIO").ToString())

        hdfIdFormato.Value = ldsResponse.Tables(0).Rows(0).Item("INT_COD_GENFOR").ToString()
        txtNombres.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NOMBRE").ToString()
        txtFecInicio.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC").ToString()
        txtCodigo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_TRAB").ToString()
        hdfPuestoA.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_PUEST_ACT").ToString()
        txtPuestoAct.Text = ldsResponse.Tables(0).Rows(0).Item("DE_PUES_TRAB_A").ToString()
        hdfCodPuesto.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_PUEST_PRO").ToString()
        txtPuestoProm.Text = ldsResponse.Tables(0).Rows(0).Item("DE_PUES_TRAB_P").ToString()
        txtFecIni.Text = ldsResponse.Tables(0).Rows(0).Item("DT_FEC_INI").ToString()
        hdfArea.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_AREA").ToString()
        txtArea.Text = ldsResponse.Tables(0).Rows(0).Item("DE_AREA").ToString()
        hdfCodJefe.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_SUPER").ToString()
        txtSuper.Text = ldsResponse.Tables(0).Rows(0).Item("NOM_SUP").ToString()
        hdfCodTrab.Value = ldsResponse.Tables(0).Rows(0).Item("VCH_NOM_COMP").ToString()
        txtCompa.Text = ldsResponse.Tables(0).Rows(0).Item("NOM_COMP").ToString()
        txtTiempo.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_TIEMPO_SERV").ToString()
        txtOpinion.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OPIN_RESP").ToString()
        txtFecInicio.Text = Date.Now.ToString("dd/MM/yyyy")
        gvEval1.DataSource = Nothing 'ldsResponse.Tables(1)
        gvEval1.DataBind()

        gvEval2.DataSource = Nothing 'ldsResponse.Tables(2)
        gvEval2.DataBind()

        gvFirmas.DataSource = ldsResponse.Tables(3)
        gvFirmas.DataBind()

        For Each lobjRow As GridViewRow In gvEval1.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

            If Not ltxtValor.Text.Trim().Equals("") Then
                lintSuma = lintSuma + Convert.ToInt32(ltxtValor.Text.Trim())
            End If
        Next
        txtValorTot1.InnerText = lintSuma.ToString()

        For Each lobjRow As GridViewRow In gvEval2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            If Not ltxtValor.Text.Trim().Equals("") Then
                lintSuma_2 = lintSuma_2 + Convert.ToInt32(ltxtValor.Text.Trim())
            End If
        Next

        txtValorTot2.InnerText = lintSuma_2.ToString()

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
            'Session("@USUARIO") = "AAMPUERP"
            If Not Request("pidCodGenFormato") Is Nothing Then
                fn_ObtenerExisteFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
            Else
                fn_ObtenerParametrosFormulario()
            End If
        Else
            If hdnCargarNuevoPuesto.Value = "S" Then
                If hdfCodPuesto.Value <> "" Then
                    Dim lobjGerencia As New clsGerencia
                    Dim ldsResponse As DataSet
                    ldsResponse = lobjGerencia.fn_ObtenerFormatoPorArea(hdfCodPuesto.Value)

                    If ldsResponse.Tables(0).Rows.Count > 0 Then
                        gvEval1.DataSource = ldsResponse.Tables(0)
                        gvEval1.DataBind()

                        gvEval2.DataSource = ldsResponse.Tables(1)
                        gvEval2.DataBind()
                        hdnCargarNuevoPuesto.Value = ""
                    End If
                Else
                    gvEval1.DataSource = Nothing
                    gvEval1.DataBind()

                    gvEval2.DataSource = Nothing
                    gvEval2.DataBind()
                End If
            End If
        End If
    End Sub
    Public Function fn_CargarFirmas() As DataTable

        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim ldtResponse As DataTable

        ldsResponse = lobjGerencia.fn_CargarListas("")

        ldtResponse = ldsResponse.Tables(1)

        Return ldtResponse
    End Function
    Protected Sub txtValor_TextChanged(sender As Object, e As EventArgs)
        Dim lintSuma As Int32 = 0
        For Each lobjRow As GridViewRow In gvEval1.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

            If Not ltxtValor.Text.Trim().Equals("") Then
                lintSuma = lintSuma + Convert.ToInt32(ltxtValor.Text.Trim())
            End If
        Next
        txtValorTot1.InnerText = lintSuma.ToString()
    End Sub

    Protected Sub txtValor2_TextChanged(sender As Object, e As EventArgs)
        Dim lintSuma As Int32 = 0
        For Each lobjRow As GridViewRow In gvEval2.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

            If Not ltxtValor.Text.Trim().Equals("") Then
                lintSuma = lintSuma + Convert.ToInt32(ltxtValor.Text.Trim())
            End If
        Next

        txtValorTot2.InnerText = lintSuma.ToString()
    End Sub

    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        If Not txtCodigo.Text.Equals("") Then
            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_ObtenerDatosTrab(txtCodigo.Text)

            If ldtResponse.Rows.Count > 0 Then
                txtNombres.Text = ldtResponse.Rows(0).Item("NO_TRAB").ToString()
                txtPuestoAct.Text = ldtResponse.Rows(0).Item("DE_PUES_TRAB").ToString()
                hdfPuestoA.Value = ldtResponse.Rows(0).Item("CO_PUES_TRAB").ToString()
                txtArea.Text = ldtResponse.Rows(0).Item("DE_AREA").ToString()
                hdfArea.Value = ldtResponse.Rows(0).Item("CO_AREA").ToString()
            Else
                txtNombres.Text = ""
                txtPuestoAct.Text = ""
                hdfPuestoA.Value = ""
                txtArea.Text = ""
                hdfArea.Value = ""
            End If
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        If txtFecInicio.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese fecha.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFecInicio.Text.Trim())
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha válida.');</script>")
            Exit Sub
        End Try

        If txtNombres.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese nombres.');</script>")
            Exit Sub
        End If

        If txtCodigo.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese código.');</script>")
            Exit Sub
        End If

        If txtPuestoAct.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese puesto actual.');</script>")
            Exit Sub
        End If

        If txtPuestoProm.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese puesto al que se promociona.');</script>")
            Exit Sub
        End If

        If txtArea.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese el área.');</script>")
            Exit Sub
        End If

        If txtFecIni.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese fecha de inicio.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFecIni.Text.Trim())
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese una fecha de inicio válida.');</script>")
            Exit Sub
        End Try

        If txtSuper.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese supervisor/jefe directo.');</script>")
            Exit Sub
        End If

        If txtCompa.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese trabajador asignado.');</script>")
            Exit Sub
        End If

        If txtTiempo.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese tiempo de servicio.');</script>")
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

        'For Each lobjRow As GridViewRow In gvEval1.Rows
        '    Dim ltxtValor As New TextBox
        '    Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvEval1.Rows(lobjRow.RowIndex).FindControl("txtValor"), TextBox)

        '    Try
        '        Convert.ToInt32(ltxtValor.Text.Trim())
        '    Catch ex As Exception
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingres solo valores númericos.');</script>")
        '        Exit Sub
        '    End Try

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Complete puntuaciones de dominio principal de tareas.');</script>")
        '        Exit Sub
        '    End If
        'Next

        'For Each lobjRow As GridViewRow In gvEval2.Rows
        '    Dim ltxtValor As New TextBox
        '    Dim lbolValidar As Boolean = False
        '    ltxtValor = CType(gvEval2.Rows(lobjRow.RowIndex).FindControl("txtValor2"), TextBox)

        '    Try
        '        Convert.ToInt32(ltxtValor.Text.Trim())
        '    Catch ex As Exception
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingres solo valores númericos.');</script>")
        '        Exit Sub
        '    End Try

        '    If ltxtValor.Text.Trim().Equals("") Then
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Complete puntuaciones de dominio principal de tareas.');</script>")
        '        Exit Sub
        '    End If
        'Next

        If txtOpinion.Text.Trim().Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese opinión del responsable.');</script>")
            Exit Sub
        End If

        ldtResponse = lobjGerencia.fn_ModificarFormatoProt_5(Convert.ToInt32(hdfIdFormato.Value),
                                                             txtNombres.Text.ToUpper(),
                                                             Convert.ToDateTime(txtFecInicio.Text.Trim()).ToString("yyyyMMdd"),
                                                             txtCodigo.Text.Trim(),
                                                             hdfPuestoA.Value,
                                                             hdfCodPuesto.Value,
                                                             hdfArea.Value,
                                                             Convert.ToDateTime(txtFecIni.Text.Trim()).ToString("yyyyMMdd"),
                                                             hdfCodJefe.Value,
                                                             hdfCodTrab.Value,
                                                             txtTiempo.Text.Trim(),
                                                             txtOpinion.Text.Trim().ToUpper(),
                                                             Session("@USUARIO").ToString())

        fn_RegistrarParametros()
        'fn_ObtenerParametrosFormulario()
        fn_ObtenerExisteFormato(hdfIdFormato.Value)

        Response.Redirect("frmListFormatoCambioPuesto.aspx")
    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs) Handles btnSolicitar.Click
        Try
            fn_SolicitarAprobacion(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
        Catch ex As Exception
            'fn_SolicitarAprobacion(Convert.ToInt32(hdfIdFormato.Value))
        End Try
    End Sub

    Protected Sub btnAprobar_Click(sender As Object, e As EventArgs) Handles btnAprobar.Click
        fn_aprobarFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()), 5)
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        fn_anularFormato(Convert.ToInt32(Request("pidCodGenFormato").ToString()))
    End Sub

    Protected Sub imgbtnVolver_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVolver.Click
        Response.Redirect("frmListFormatoCambioPuesto.aspx")
    End Sub

#End Region

    Private Sub gvFirmas_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFirmas.RowCommand
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
                                                        5,
                                                        lddlFirmas.SelectedValue.ToString(),
                                                        Session("@USUARIO").ToString())

            gvFirmas.DataSource = ldtResponse
            gvFirmas.DataBind()

        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            ldtResponse = lobjGerencia.fn_EliminarFirma(Convert.ToInt32(hdfIdFormato.Value),
                                                        5,
                                                        e.CommandArgument)

            gvFirmas.DataSource = ldtResponse
            gvFirmas.DataBind()

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
End Class