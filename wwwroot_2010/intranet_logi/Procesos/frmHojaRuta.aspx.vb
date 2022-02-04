Imports NM_General
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class frmHojaRuta
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        If Request("TipoPrueba") <> "" Then
            Session("TipoPrueba") = Request("TipoPrueba")
            Session("CodGenFormato") = Request("cod")
            lbltipoprueba.Text = IIf(Request("TipoPrueba") = "PRE", "Prueba Preliminar", "Prueba Industrial")
            Dim usuario As String = Session("@USUARIO")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(ibtDescargarfichas)
        scriptManager.RegisterPostBackControl(ibtDescargarcarta)
        scriptManager.RegisterPostBackControl(ibtDescargarcertificados)
        scriptManager.RegisterPostBackControl(ibtDescargardocumento)

        If Not IsPostBack Then

            fnc_ObtenerPruebaPreliminar(Session("CodGenFormato"))
            fn_ObtenerExisteFormato(Convert.ToInt32(Session("CodGenFormato")))

            fn_CargarFirmas()
            fnc_ObtenerHilanderia()
            fnc_ObenerLaboratorioHilnaderia()
            fnc_ObtenerPretejido()
            fnc_ObtenerTejeduria()
            fnc_ObtenerTintoreria()
            fnc_ObtenerLaboratorioFisico()
            fnc_ObtenerRevisionFinal()

        End If

    End Sub
    Protected Sub gvFirmasDireccion_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFirmasDireccion.RowCommand
        If e.CommandName = "Agregar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            Dim gvr As GridViewRow
            Dim lddlFirmas As DropDownList

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            lddlFirmas = CType(gvFirmasDireccion.FooterRow.FindControl("ddlFirmasDir"), DropDownList)

            If lddlFirmas.SelectedValue.ToString().Equals("0") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un responsable.');</script>")
                Exit Sub
            End If

            For Each lobjRow As GridViewRow In gvFirmasDireccion.Rows
                Dim ltxtValor As New TextBox
                ltxtValor = CType(gvFirmasDireccion.Rows(lobjRow.RowIndex).FindControl("txtCampoFirmaDir"), TextBox)

                If ltxtValor.Text.Equals(lddlFirmas.SelectedValue) Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Esta firma ya fue seleccionada.');</script>")
                    Exit Sub
                End If
            Next
            If (Session("CodGenFormato") <> Nothing) Then
                ldtResponse = lobjGerencia.fn_AgregarFirmas_HRuta(Convert.ToInt32(Convert.ToInt32(Session("CodGenFormato").ToString())),
                                                            2,
                                                            lddlFirmas.SelectedValue.ToString(),
                                                            Session("@USUARIO").ToString(), False)

                gvFirmasDireccion.DataSource = ldtResponse
                gvFirmasDireccion.DataBind()
                fn_ObtenerExisteFormato(Convert.ToInt32(Session("CodGenFormato")))
            End If
        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            If (Session("CodGenFormato") <> Nothing) Then
                ldtResponse = lobjGerencia.fn_EliminarFirma_HRuta(Convert.ToInt32(Convert.ToInt32(Session("CodGenFormato").ToString())),
                                                            2,
                                                            e.CommandArgument, False)

                gvFirmasDireccion.DataSource = ldtResponse
                gvFirmasDireccion.DataBind()
                fn_ObtenerExisteFormato(Convert.ToInt32(Session("CodGenFormato")))
            End If

        End If
    End Sub

    Protected Sub gvFirmasCalidad_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFirmasCalidad.RowCommand
        If e.CommandName = "Agregar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable
            Dim gvr As GridViewRow
            Dim lddlFirmas As DropDownList

            gvr = CType(e.CommandSource, ImageButton).NamingContainer

            lddlFirmas = CType(gvFirmasCalidad.FooterRow.FindControl("ddlFirmasCal"), DropDownList)

            If lddlFirmas.SelectedValue.ToString().Equals("0") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un responsable.');</script>")
                Exit Sub
            End If

            For Each lobjRow As GridViewRow In gvFirmasCalidad.Rows
                Dim ltxtValor As New TextBox
                ltxtValor = CType(gvFirmasCalidad.Rows(lobjRow.RowIndex).FindControl("txtCampoFirmaCal"), TextBox)

                If ltxtValor.Text.Equals(lddlFirmas.SelectedValue) Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Esta firma ya fue seleccionada.');</script>")
                    Exit Sub
                End If
            Next
            If (Session("CodGenFormato") <> Nothing) Then
                ldtResponse = lobjGerencia.fn_AgregarFirmas_HRuta(Convert.ToInt32(Convert.ToInt32(Session("CodGenFormato").ToString())),
                                                            2,
                                                            lddlFirmas.SelectedValue.ToString(),
                                                            Session("@USUARIO").ToString(), True)

                gvFirmasCalidad.DataSource = ldtResponse
                gvFirmasCalidad.DataBind()

            End If
        End If

        If e.CommandName = "Eliminar" Then

            Dim lobjGerencia As New clsGerencia
            Dim ldtResponse As DataTable

            If (Session("CodGenFormato") <> Nothing) Then
                ldtResponse = lobjGerencia.fn_EliminarFirma_HRuta(Convert.ToInt32(Convert.ToInt32(Session("CodGenFormato").ToString())),
                                                            2,
                                                            e.CommandArgument, True)

                gvFirmasCalidad.DataSource = ldtResponse
                gvFirmasCalidad.DataBind()
            End If

        End If
        fn_ObtenerExisteFormato(Convert.ToInt32(Session("CodGenFormato")))
    End Sub
    Protected Sub imgbtnVolver_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnVolver.Click
        Response.Redirect("frmListadoNuevoInsumo.aspx?pidCodGenFormato=" + Session("CodGenFormato"))
    End Sub
    Protected Sub btnPretejeduriaR_Click(sender As Object, e As EventArgs) Handles btnPretejeduriaR.Click
        If (btnPretejeduriaR.BackColor = Drawing.Color.Orange) Then
            btnPretejeduriaR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnPretejeduriaR.BackColor = Drawing.Color.Orange
        End If
    End Sub
    Protected Sub btnHilanderiaR_Click(sender As Object, e As EventArgs) Handles btnHilanderiaR.Click
        If (btnHilanderiaR.BackColor = Drawing.Color.Orange) Then
            btnHilanderiaR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnHilanderiaR.BackColor = Drawing.Color.Orange
        End If
    End Sub
    Protected Sub btnTintoreriaR_Click(sender As Object, e As EventArgs) Handles btnTintoreriaR.Click
        If (btnTintoreriaR.BackColor = Drawing.Color.Orange) Then
            btnTintoreriaR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnTintoreriaR.BackColor = Drawing.Color.Orange
        End If
    End Sub
    Protected Sub btnRevisionFinalR_Click(sender As Object, e As EventArgs) Handles btnRevisionFinalR.Click
        If (btnRevisionFinalR.BackColor = Drawing.Color.Orange) Then
            btnRevisionFinalR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnRevisionFinalR.BackColor = Drawing.Color.Orange
        End If


    End Sub
    Protected Sub btnTejeduriaR_Click(sender As Object, e As EventArgs) Handles btnTejeduriaR.Click
        If (btnTejeduriaR.BackColor = Drawing.Color.Orange) Then
            btnTejeduriaR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnTejeduriaR.BackColor = Drawing.Color.Orange
        End If

    End Sub
    Protected Sub btnLaboratorioHIlanderiaR_Click(sender As Object, e As EventArgs) Handles btnLaboratorioHIlanderiaR.Click
        If (btnLaboratorioHIlanderiaR.BackColor = Drawing.Color.Orange) Then
            btnLaboratorioHIlanderiaR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnLaboratorioHIlanderiaR.BackColor = Drawing.Color.Orange
        End If

    End Sub
    Protected Sub btnLaboratorioFisicoR_Click(sender As Object, e As EventArgs) Handles btnLaboratorioFisicoR.Click
        If (btnLaboratorioFisicoR.BackColor = Drawing.Color.Orange) Then
            btnLaboratorioFisicoR.BackColor = System.Drawing.ColorTranslator.FromHtml("#336699")
        Else
            btnLaboratorioFisicoR.BackColor = Drawing.Color.Orange
        End If

    End Sub
    Sub fn_ObtenerExisteFormato(ByVal pintCodGenFor As Int32)
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim ldsResponse_2 As DataSet
        Dim lintContadorP1 = 0
        Dim lintContadorP2 = 0

        ldsResponse = lobjGerencia.fn_ObtenerUsuarioFirma(pintCodGenFor,
                                                       2, True)
        ldsResponse_2 = lobjGerencia.fn_ObtenerUsuarioFirma(pintCodGenFor,
                                                     2, False)

        Session("ldtResponse") = ldsResponse.Tables(0)

        gvFirmasCalidad.DataSource = ldsResponse.Tables(0)
        gvFirmasCalidad.DataBind()

        gvFirmasDireccion.DataSource = ldsResponse_2.Tables(0)
        gvFirmasDireccion.DataBind()

        If ldsResponse.Tables(0).Rows.Count > 0 Then
            ddlFirmasCal.Visible = False
            btnAgregarCal.Visible = False
        Else
            ddlFirmasCal.Visible = True
            btnAgregarCal.Visible = True
        End If


        If ldsResponse_2.Tables(0).Rows.Count > 0 Then
            ddlFirmasDir.Visible = False
            btnAgregarDir.Visible = False
        Else
            ddlFirmasDir.Visible = True
            btnAgregarDir.Visible = True
        End If

    End Sub

    Public Function fn_CargarFirmas() As DataTable

        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        Dim ldtResponse As DataTable

        ldsResponse = lobjGerencia.fn_CargarListas("")

        ldtResponse = ldsResponse.Tables(1)
        ddlFirmasCal.DataSource = ldtResponse
        ddlFirmasCal.DataBind()
        ddlFirmasDir.DataSource = ldtResponse
        ddlFirmasDir.DataBind()
        Return ldtResponse
    End Function

    Protected Sub btnGuardarProcesos_Click(sender As Object, e As EventArgs) Handles btnGuardarProcesos.Click
        Dim lobjGerencia As New clsGerencia
        Dim procHilanderia As Boolean
        Dim procPretejeduria As Boolean
        Dim procTejeduria As Boolean
        Dim procTintoreria As Boolean
        Dim procRevisionFinal As Boolean
        Dim procLaboratorioHilanderia As Boolean
        Dim procLaboratorioFisico As Boolean

        If (btnRevisionFinalR.BackColor = Drawing.Color.Orange) Then
            procRevisionFinal = True
        Else
            procRevisionFinal = False
        End If
        If (btnTintoreriaR.BackColor = Drawing.Color.Orange) Then
            procTintoreria = True
        Else
            procTintoreria = False
        End If
        If (btnHilanderiaR.BackColor = Drawing.Color.Orange) Then
            procHilanderia = True
        Else
            procHilanderia = False
        End If
        If (btnTejeduriaR.BackColor = Drawing.Color.Orange) Then
            procTejeduria = True
        Else
            procTejeduria = False
        End If
        If (btnPretejeduriaR.BackColor = Drawing.Color.Orange) Then
            procPretejeduria = True
        Else
            procPretejeduria = False
        End If

        If (btnLaboratorioFisicoR.BackColor = Drawing.Color.Orange) Then
            procLaboratorioFisico = True
        Else
            procLaboratorioFisico = False
        End If

        If (btnLaboratorioHIlanderiaR.BackColor = Drawing.Color.Orange) Then
            procLaboratorioHilanderia = True
        Else
            procLaboratorioHilanderia = False
        End If
        lobjGerencia.fn_GuardarProcesosHojaRuta(txtNroPreliminar2.Text,
                                                Session("CodGenFormato"),
                                                2,
                                                Session("TipoPrueba").ToString(),
                                                procHilanderia,
                                                procPretejeduria,
                                                procTejeduria,
                                                procTintoreria,
                                                procRevisionFinal,
                                                procLaboratorioHilanderia,
                                                procLaboratorioFisico,
                                                txtFechaIngreso.Text,
                                                txtItem.Text,
                                                txtConclusionFinal.Text
                                                )
    End Sub
    Protected Sub btnAgregarCal_Click(sender As Object, e As EventArgs) Handles btnAgregarCal.Click
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable


        If ddlFirmasCal.SelectedValue.ToString().Equals("0") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un responsable.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvFirmasCalidad.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvFirmasCalidad.Rows(lobjRow.RowIndex).FindControl("txtCampoFirmaCal"), TextBox)

            If ltxtValor.Text.Equals(ddlFirmasCal.SelectedValue) Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Esta firma ya fue seleccionada.');</script>")
                Exit Sub
            End If
        Next
        If (Session("CodGenFormato") <> Nothing) Then
            ldtResponse = lobjGerencia.fn_AgregarFirmas_HRuta(Convert.ToInt32(Convert.ToInt32(Session("CodGenFormato").ToString())),
                                                        2,
                                                        ddlFirmasCal.SelectedValue.ToString(),
                                                        Session("@USUARIO").ToString(), True)

            gvFirmasCalidad.DataSource = ldtResponse
            gvFirmasCalidad.DataBind()
        End If
        fn_ObtenerExisteFormato(Convert.ToInt32(Session("CodGenFormato")))

    End Sub

    Protected Sub btnAgregarDir_Click(sender As Object, e As EventArgs) Handles btnAgregarDir.Click
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable


        If ddlFirmasDir.SelectedValue.ToString().Equals("0") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione un responsable.');</script>")
            Exit Sub
        End If

        For Each lobjRow As GridViewRow In gvFirmasDireccion.Rows
            Dim ltxtValor As New TextBox
            ltxtValor = CType(gvFirmasDireccion.Rows(lobjRow.RowIndex).FindControl("txtCampoFirmaCal"), TextBox)

            If ltxtValor.Text.Equals(ddlFirmasDir.SelectedValue) Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Esta firma ya fue seleccionada.');</script>")
                Exit Sub
            End If
        Next
        If (Session("CodGenFormato") <> Nothing) Then
            ldtResponse = lobjGerencia.fn_AgregarFirmas_HRuta(Convert.ToInt32(Convert.ToInt32(Session("CodGenFormato").ToString())),
                                                        2,
                                                        ddlFirmasDir.SelectedValue.ToString(),
                                                        Session("@USUARIO").ToString(), False)

            gvFirmasDireccion.DataSource = ldtResponse
            gvFirmasDireccion.DataBind()
        End If
        fn_ObtenerExisteFormato(Convert.ToInt32(Session("CodGenFormato")))
    End Sub

    Protected Sub btnHilanderia_Click(sender As Object, e As EventArgs) Handles btnHilanderia.Click
        Dim dtHilanderia As DataTable = Session("ldtHilanderia")
        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)
                Session("ldtHilanderia") = dtHilanderia

            End If
        End If
        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then
                fnc_ActualizarHilandria()
            Else
                fnc_InsertarHilandria()
            End If
        Else
            fnc_InsertarHilandria()
        End If

    End Sub
    Protected Sub btnLaboratorioHilanderia_Click(sender As Object, e As EventArgs) Handles btnLaboratorioHilanderia.Click
        Dim dtLaboratorioHilnaderia As DataTable = Session("LaboratorioHilnaderia")
        If Not dtLaboratorioHilnaderia Is Nothing Then
            If dtLaboratorioHilnaderia.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtLaboratorioHilnaderia = lobjGerencia.fn_ObtenerLaboratorioHIlanderia(txtNroPreliminar2.Text)
                Session("LaboratorioHilnaderia") = dtLaboratorioHilnaderia

            End If
        End If
        If Not dtLaboratorioHilnaderia Is Nothing Then
            If dtLaboratorioHilnaderia.Rows.Count > 0 Then
                fnc_ActualizarLaboratotioHilanderia()
            Else
                fnc_InsertarLaboratotioHilanderia()
            End If
        Else
            fnc_InsertarLaboratotioHilanderia()
        End If
    End Sub
    Protected Sub btn_Pretejido_Click(sender As Object, e As EventArgs) Handles btn_Pretejido.Click
        Dim dtPretejido As DataTable = Session("ldtPretejido")

        If Not dtPretejido Is Nothing Then
            If dtPretejido.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtPretejido = lobjGerencia.fn_ObtenerLPretejido(txtNroPreliminar2.Text)
                Session("ldtPretejido") = dtPretejido

            End If
        End If
        If Not dtPretejido Is Nothing Then
            If dtPretejido.Rows.Count > 0 Then
                fnc_ActualizarPretejido()
            Else
                fnc_InsertarPretejido()
            End If
        Else
            fnc_InsertarPretejido()
        End If

    End Sub
    Protected Sub btnTejeduria_Click(sender As Object, e As EventArgs) Handles btnTejeduria.Click
        Dim dtTejeduria As DataTable = Session("ldtTejeduria")
        If Not dtTejeduria Is Nothing Then
            If dtTejeduria.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtTejeduria = lobjGerencia.fn_ObtenerLTejeduria(txtNroPreliminar2.Text, Session("TipoPrueba").ToString())
                Session("ldtTejeduria") = dtTejeduria

            End If
        End If
        If Not dtTejeduria Is Nothing Then
            If dtTejeduria.Rows.Count > 0 Then
                fnc_ActaulizarTejeduria()
            Else
                fnc_InsertarTejeduria()
            End If
        Else
            fnc_InsertarTejeduria()
        End If

    End Sub
    Protected Sub btnTintoreria_Click(sender As Object, e As EventArgs) Handles btnTintoreria.Click

        Dim dtTintoreria As DataTable = Session("ldtTintoreria")
        If Not dtTintoreria Is Nothing Then
            If dtTintoreria.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtTintoreria = lobjGerencia.fn_ObtenerTinteroria(txtNroPreliminar2.Text, Session("TipoPrueba").ToString())
                Session("ldtTintoreria") = dtTintoreria

            End If
        End If
        If Not dtTintoreria Is Nothing Then
            If dtTintoreria.Rows.Count > 0 Then
                fnc_ActualizarTintoreria()
            Else
                fnc_InsertarTintoreria()
            End If
        Else
            fnc_InsertarTintoreria()
        End If
    End Sub
    Protected Sub btnLaboratorioFisico_Click(sender As Object, e As EventArgs) Handles btnLaboratorioFisico.Click

        Dim dtLaboratorioFisico As DataTable = Session("ldtLaboratorioFisico")
        If Not dtLaboratorioFisico Is Nothing Then
            If dtLaboratorioFisico.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtLaboratorioFisico = lobjGerencia.fn_ObtenerLaboratorioFisico(txtNroPreliminar2.Text)
                Session("ldtLaboratorioFisico") = dtLaboratorioFisico

            End If
        End If
        If Not dtLaboratorioFisico Is Nothing Then
            If dtLaboratorioFisico.Rows.Count > 0 Then
                fnc_ActualizarLaboratorioFisico()
            Else
                fnc_InsertarLaboratorioFisico()
            End If
        Else
            fnc_InsertarLaboratorioFisico()
        End If
    End Sub
    Protected Sub btnRevisionFinal_Click(sender As Object, e As EventArgs) Handles btnRevisionFinal.Click
        Dim dtRevisionFinal As DataTable = Session("ldtRevisionFinal")
        If Not dtRevisionFinal Is Nothing Then
            If dtRevisionFinal.Rows.Count = 0 Then
                Dim lobjGerencia As New clsGerencia

                dtRevisionFinal = lobjGerencia.fn_ObtenerLRevisionFinal(txtNroPreliminar2.Text)
                Session("ldtRevisionFinal") = dtRevisionFinal

            End If
        End If
        If Not dtRevisionFinal Is Nothing Then
            If dtRevisionFinal.Rows.Count > 0 Then
                fnc_ActualizarRevisionFinal()
            Else
                fnc_InsertarRevisionFinal()
            End If
        Else
            fnc_InsertarRevisionFinal()
        End If

    End Sub
    Protected Sub btnGuardadoFinal_Click(sender As Object, e As EventArgs) Handles btnGuardadoFinal.Click


        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarrPruebaPreliminar(Session("CodGenFormato"), 2, txtConclusionFinal.Text, Session("TipoPrueba").ToString(), txtNroPreliminar2.Text, txtFechaCalidad.Text, txtFechaDireccion.Text)

    End Sub
    Protected Sub btnTerminoHilanderia_Click(sender As Object, e As EventArgs) Handles btnTerminoHilanderia.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btnHilanderia.Visible = Not (btnHilanderia.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btnHilanderia.Visible, "HILANDERIA")
            End If
        End If

    End Sub
    Protected Sub btnTerminoLaboratorioHilanderia_Click(sender As Object, e As EventArgs) Handles btnTerminoLaboratorioHilanderia.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btnLaboratorioHilanderia.Visible = Not (btnLaboratorioHilanderia.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btnLaboratorioHilanderia.Visible, "LHILANDERIA")
            End If
        End If

    End Sub
    Protected Sub btnTerminoPretejido_Click(sender As Object, e As EventArgs) Handles btnTerminoPretejido.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btn_Pretejido.Visible = Not (btn_Pretejido.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btn_Pretejido.Visible, "PRETEJIDO")
            End If
        End If

    End Sub
    Protected Sub btnTerminoTejeduria_Click(sender As Object, e As EventArgs) Handles btnTerminoTejeduria.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btnTejeduria.Visible = Not (btnTejeduria.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btnTejeduria.Visible, "TEJEDURIA")
            End If
        End If

    End Sub
    Protected Sub btnTerminarTintoreria_Click(sender As Object, e As EventArgs) Handles btnTerminarTintoreria.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btnTintoreria.Visible = Not (btnTintoreria.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btnTintoreria.Visible, "TINTORERIA")
            End If
        End If

    End Sub
    Protected Sub btnTerminanoLaboratorioFisico_Click(sender As Object, e As EventArgs) Handles btnTerminanoLaboratorioFisico.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btnLaboratorioFisico.Visible = Not (btnLaboratorioFisico.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btnLaboratorioFisico.Visible, "LFISICO")
            End If
        End If

    End Sub
    Protected Sub btnTerminoRevisionFinal_Click(sender As Object, e As EventArgs) Handles btnTerminoRevisionFinal.Click

        Dim lobjGerencia As New clsGerencia
        dtHilanderia = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)

        If Not dtHilanderia Is Nothing Then
            If dtHilanderia.Rows.Count > 0 Then

                btnRevisionFinal.Visible = Not (btnRevisionFinal.Visible)
                lobjGerencia.fn_ActualizarBloquearDesbloquearProcesosHojaRuta(txtNroPreliminar2.Text,
                                                  Session("CodGenFormato"),
                                                  2,
                                                  Session("TipoPrueba").ToString(), btnRevisionFinal.Visible, "REVISIONFINAL")
            End If
        End If

    End Sub
    Sub fnc_ObtenerPruebaPreliminar(ByVal codFormato As String)
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataSet
        ldsResponse = lobjGerencia.fn_ObtenerPruebaPreliminar(codFormato, 2, Session("TipoPrueba"), Session("@USUARIO"))
        Session("ldtResponse") = ldsResponse.Tables(0)
        If Not Session("@USUARIO") Is Nothing Then


            If (ldsResponse.Tables(0).Rows.Count > 0) Then

                txtPruebaNro.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NROPRELIMINAR").ToString()
                txtNroPreliminar2.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_NROPRELIMINAR").ToString()
                txtDescProdcuto.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_DET_PROD").ToString()
                txtLote.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_LOTE").ToString()
                txtPlanta.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_PLANTA").ToString()
                txtPaisProcedencia.Text = ldsResponse.Tables(0).Rows(0).Item("NO_PAIS").ToString()
                txtPlanta.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_PLANTA").ToString()
                txtProveedor.Text = ldsResponse.Tables(0).Rows(0).Item("NO_CORT_PROV").ToString()
                txtLote.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_LOTE").ToString()
                txtCodProveedor.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_COD_PROV").ToString()
                txtCantidadIngresada.Text = ldsResponse.Tables(0).Rows(0).Item("INT_CANTIDAD").ToString()
                txtObjCambio.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_OBJ_CAMB").ToString()
                txtFichaEspecificacionTecnica.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEFICHA").ToString()
                txtFichaCertificadoCalidad.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMECERTIFICADO").ToString()
                txtFichaCartaCompromiso.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMECARTA").ToString()
                txtFichadocumento.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_FILENAMEDOCUMENTO").ToString()
                If (Not ldsResponse.Tables(0).Rows(0).Item("CONCLUSION_FINAL") Is Nothing) Then
                    txtConclusionFinal.Text = ldsResponse.Tables(0).Rows(0).Item("CONCLUSION_FINAL").ToString()
                End If
                txtItem.Text = ldsResponse.Tables(0).Rows(0).Item("VCH_ITEM").ToString()
                txtFechaIngreso.Text = ldsResponse.Tables(0).Rows(0).Item("DAT_FECHA_INGRESO").ToString()

                txtFechaCalidad.Text = ldsResponse.Tables(0).Rows(0).Item("DAT_FechaCalidad").ToString()
                txtFechaDireccion.Text = ldsResponse.Tables(0).Rows(0).Item("DAT_FechaDireccion").ToString()

                If (Not ldsResponse.Tables(0).Rows(0).Item("HILANDERIA") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("HILANDERIA").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("HILANDERIA").ToString())) Then
                            btnHilanderiaR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("PRETEJEDURIA") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("PRETEJEDURIA").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("PRETEJEDURIA").ToString())) Then
                            btnPretejeduriaR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If

                If (Not ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA").ToString())) Then
                            btnTejeduriaR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If

                If (Not ldsResponse.Tables(0).Rows(0).Item("TINTORERIA") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("TINTORERIA").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("TINTORERIA").ToString())) Then
                            btnTintoreriaR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If

                If (Not ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_HILANDERIA") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_HILANDERIA").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_HILANDERIA").ToString())) Then
                            btnLaboratorioHIlanderiaR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If

                If (Not ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_FISICO") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_FISICO").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_FISICO").ToString())) Then
                            btnLaboratorioFisicoR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If

                If (Not ldsResponse.Tables(0).Rows(0).Item("REVISION_FINAL") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("REVISION_FINAL").ToString() <> "" Then
                        If (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("REVISION_FINAL").ToString())) Then
                            btnRevisionFinalR.BackColor = Drawing.Color.Orange
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("HILANDERIA_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("HILANDERIA_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("HILANDERIA_BLOQ").ToString())) Then
                            btnHilanderia.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA_BLOQ").ToString())) Then
                            btnTejeduria.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("PRETEJEDURIA_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("PRETEJEDURIA_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("PRETEJEDURIA_BLOQ").ToString())) Then
                            btn_Pretejido.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("TEJEDURIA_BLOQ").ToString())) Then
                            btnTejeduria.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("TINTORERIA_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("TINTORERIA_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("TINTORERIA_BLOQ").ToString())) Then
                            btnTintoreria.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_HILANDERIA_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_HILANDERIA_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_HILANDERIA_BLOQ").ToString())) Then
                            btnLaboratorioHilanderia.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_FISICO_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_FISICO_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("LABORATORIO_FISICO_BLOQ").ToString())) Then
                            btnLaboratorioFisico.Visible = False
                        End If
                    End If
                End If
                If (Not ldsResponse.Tables(0).Rows(0).Item("REVISION_FINAL_BLOQ") Is Nothing) Then
                    If ldsResponse.Tables(0).Rows(0).Item("REVISION_FINAL_BLOQ").ToString() <> "" Then
                        If Not (Convert.ToBoolean(ldsResponse.Tables(0).Rows(0).Item("REVISION_FINAL_BLOQ").ToString())) Then
                            btnRevisionFinal.Visible = False
                        End If
                    End If
                End If

            End If
            If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                    If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("REVISION FINAL")) Then ''SISTEMNAS
                        tablaHilanderia.Visible = False
                        TablaTejeduria.Visible = False
                        TablaPretejido.Visible = False
                        TablaTintoreria.Visible = False
                        TablaRevisionFinal.Visible = True
                        TablaLaboratorioFisico.Visible = False
                        TablaLaboratorioHIlanderia.Visible = False
                        btnGuardarProcesos.Visible = False
                        TablaConclusiones.Visible = False
                        btnGuardadoFinal.Visible = False
                        btnTerminoRevisionFinal.Visible = False
                        imgbtnVolver.Visible = False
                        txtFechaCalidad.Visible = False
                        txtFechaDireccion.Visible = False
                        ddlFirmasCal.Visible = False
                        ddlFirmasDir.Visible = False
                        gvFirmasCalidad.Visible = False
                        gvFirmasDireccion.Visible = False
                        im1.Visible = False
                        im2.Visible = False
                    End If
                End If
            End If
            If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                    If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("DIRECCION TECNICA DE HILANDERIA")) Then
                        tablaHilanderia.Visible = True
                        TablaTejeduria.Visible = False
                        TablaPretejido.Visible = False
                        TablaTintoreria.Visible = False
                        TablaRevisionFinal.Visible = False
                        TablaLaboratorioFisico.Visible = False
                        TablaLaboratorioHIlanderia.Visible = False
                        btnGuardarProcesos.Visible = False
                        TablaConclusiones.Visible = False
                        btnGuardadoFinal.Visible = False
                        btnTerminoHilanderia.Visible = False
                        imgbtnVolver.Visible = False
                        txtFechaCalidad.Visible = False
                        txtFechaDireccion.Visible = False
                        ddlFirmasCal.Visible = False
                        ddlFirmasDir.Visible = False
                        gvFirmasCalidad.Visible = False
                        gvFirmasDireccion.Visible = False
                        im1.Visible = False
                        im2.Visible = False

                    End If
                End If
            End If
            If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                    If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("DIRECCION TECNICA DE TINTORERIA")) Then
                        tablaHilanderia.Visible = False
                        TablaTejeduria.Visible = False
                        TablaPretejido.Visible = False
                        TablaTintoreria.Visible = True
                        TablaRevisionFinal.Visible = False
                        TablaLaboratorioFisico.Visible = False
                        TablaLaboratorioHIlanderia.Visible = False
                        btnGuardarProcesos.Visible = False
                        TablaConclusiones.Visible = False
                        btnGuardadoFinal.Visible = False
                        btnTerminarTintoreria.Visible = False
                        imgbtnVolver.Visible = False
                        txtFechaCalidad.Visible = False
                        txtFechaDireccion.Visible = False
                        ddlFirmasCal.Visible = False
                        ddlFirmasDir.Visible = False
                        gvFirmasCalidad.Visible = False
                        gvFirmasDireccion.Visible = False
                        im1.Visible = False
                        im2.Visible = False
                    End If
                End If
            End If
            If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                    If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("PRETEJIDO DENIM")) Then
                        tablaHilanderia.Visible = False
                        TablaTejeduria.Visible = False
                        TablaPretejido.Visible = True
                        TablaTintoreria.Visible = False
                        TablaRevisionFinal.Visible = False
                        TablaLaboratorioFisico.Visible = False
                        TablaLaboratorioHIlanderia.Visible = False
                        btnGuardarProcesos.Visible = False
                        TablaConclusiones.Visible = False
                        btnGuardadoFinal.Visible = False
                        btnTerminoPretejido.Visible = False
                        imgbtnVolver.Visible = False
                        txtFechaCalidad.Visible = False
                        txtFechaDireccion.Visible = False
                        ddlFirmasCal.Visible = False
                        ddlFirmasDir.Visible = False
                        gvFirmasCalidad.Visible = False
                        gvFirmasDireccion.Visible = False
                        im1.Visible = False
                        im2.Visible = False
                    End If
                End If
            End If
            If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                    If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("DIRECCION TECNICA DE TEJEDURIA")) Then
                        tablaHilanderia.Visible = False
                        TablaTejeduria.Visible = True
                        TablaPretejido.Visible = False
                        TablaTintoreria.Visible = False
                        TablaRevisionFinal.Visible = False
                        TablaLaboratorioFisico.Visible = False
                        TablaLaboratorioHIlanderia.Visible = False
                        btnGuardarProcesos.Visible = False
                        TablaConclusiones.Visible = False
                        btnGuardadoFinal.Visible = False
                        btnTerminoTejeduria.Visible = False
                        imgbtnVolver.Visible = False
                        txtFechaCalidad.Visible = False
                        txtFechaDireccion.Visible = False
                        ddlFirmasCal.Visible = False
                        ddlFirmasDir.Visible = False
                        gvFirmasCalidad.Visible = False
                        gvFirmasDireccion.Visible = False
                        im1.Visible = False
                        im2.Visible = False
                    End If
                End If
            End If
            If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                    If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("DIRECCION TECNICA DE CALIDAD") Or ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("SISTEMAS")) Then
                        tablaHilanderia.Visible = True
                        TablaTejeduria.Visible = True
                        TablaPretejido.Visible = True
                        TablaTintoreria.Visible = True
                        TablaRevisionFinal.Visible = True
                        TablaLaboratorioFisico.Visible = True
                        TablaLaboratorioHIlanderia.Visible = True
                        btnGuardarProcesos.Visible = True
                        TablaConclusiones.Visible = True
                        btnGuardadoFinal.Visible = True
                        imgbtnVolver.Visible = True
                        txtFechaCalidad.Visible = True
                        txtFechaDireccion.Visible = True
                        ddlFirmasCal.Visible = True
                        ddlFirmasDir.Visible = True
                        gvFirmasCalidad.Visible = True
                        gvFirmasDireccion.Visible = True
                        im1.Visible = True
                        im2.Visible = True
                    End If
                End If
            End If
        Else
            tabla1.Visible = False
            tabla2.Visible = False
            tabla3.Visible = False
            TablaPrincipal.Visible = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Habra este enlace en Internet Explore. Antes de ello debe de ingresar sus credenciales en el intranet de Nuevo Mundo, para acceder con su usuario logueado.');</script>")
        End If

    End Sub
    Sub fnc_InsertarTejeduria()
        Dim lobjGerencia As New clsGerencia

        lobjGerencia.fn_InsertarTejeduria(Session("TipoPrueba"), txtPruebaNro.Text,
        txtFechaProduccionTeje.Text,
        txtTejedorTeje.Text,
        txtMecanicoTeje.Text,
        txtFechaDesmonteTeje.Text,
        txtAnalistaTeje.Text,
        txtPartidaTeje.Text,
        txtPlegadorTeje.Text,
        txtPiezaTeje.Text,
        txtTelarTeje.Text,
        txtPlantaTeje.Text,
        txtTituloTeje.Text,
        txtMetroTeje.Text,
        txtProcedenciaTeje.Text,
        txtVELOCIDADTELARRPMTeje.Text,
        txtObsVELOCIDADTELARRPMTeje.Text,
        txtANCHOPEINERPMTEje.Text,
        txtObsANCHOPEINERPMTEje.Text,
        txtOtrosTeje.Text,
        txtObsOtrosTeje.Text,
        txtCANTIDADROTURASTRAMATURNOTeje.Text,
        txtObsCANTIDADROTURASTRAMATURNOTeje.Text,
        txtCANTIDADROTURASURDIMBRETEje.Text,
        txtObsCANTIDADROTURASURDIMBRETEje.Text,
        txtPRUEBABOILOFFTEje.Text,
        txtObsPRUEBABOILOFFTEje.Text,
        txtANCHOROLLOTEje.Text,
        txObstANCHOROLLOTEje.Text,
        txtEFICIENCIATeje.Text,
        txtObsEFICIENCIATeje.Text,
        txtOtrosTeje2.Text,
        txtObsOtrosTeje2.Text)

    End Sub

    Sub fnc_ActaulizarTejeduria()
        Dim lobjGerencia As New clsGerencia

        lobjGerencia.fn_ActualizarTejeduria(Session("TipoPrueba"), txtPruebaNro.Text,
        txtFechaProduccionTeje.Text,
        txtTejedorTeje.Text,
        txtMecanicoTeje.Text,
        txtFechaDesmonteTeje.Text,
        txtAnalistaTeje.Text,
        txtPartidaTeje.Text,
        txtPlegadorTeje.Text,
        txtPiezaTeje.Text,
        txtTelarTeje.Text,
        txtPlantaTeje.Text,
        txtTituloTeje.Text,
        txtMetroTeje.Text,
        txtProcedenciaTeje.Text,
        txtVELOCIDADTELARRPMTeje.Text,
        txtObsVELOCIDADTELARRPMTeje.Text,
        txtANCHOPEINERPMTEje.Text,
        txtObsANCHOPEINERPMTEje.Text,
        txtOtrosTeje.Text,
        txtObsOtrosTeje.Text,
        txtCANTIDADROTURASTRAMATURNOTeje.Text,
        txtObsCANTIDADROTURASTRAMATURNOTeje.Text,
        txtCANTIDADROTURASURDIMBRETEje.Text,
        txtObsCANTIDADROTURASURDIMBRETEje.Text,
        txtPRUEBABOILOFFTEje.Text,
        txtObsPRUEBABOILOFFTEje.Text,
        txtANCHOROLLOTEje.Text,
        txObstANCHOROLLOTEje.Text,
        txtEFICIENCIATeje.Text,
        txtObsEFICIENCIATeje.Text,
        txtOtrosTeje2.Text,
        txtObsOtrosTeje2.Text)


    End Sub

    Sub fnc_ObtenerTejeduria()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataTable
        ldsResponse = lobjGerencia.fn_ObtenerLTejeduria(txtNroPreliminar2.Text, Session("TipoPrueba").ToString())

        Session("ldtTejeduria") = ldsResponse
        If ldsResponse.Rows.Count > 0 Then
            txtFechaProduccionTeje.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_1").ToString()
            txtTejedorTeje.Text = ldsResponse.Rows(0).Item("TEJEDOR_1").ToString()
            txtMecanicoTeje.Text = ldsResponse.Rows(0).Item("MECANICO_1").ToString()
            txtFechaDesmonteTeje.Text = ldsResponse.Rows(0).Item("FECHA_DESMONTE_1").ToString()
            txtAnalistaTeje.Text = ldsResponse.Rows(0).Item("ANALISTA_1").ToString()
            txtPartidaTeje.Text = ldsResponse.Rows(0).Item("PARTIDA_1").ToString()
            txtPlegadorTeje.Text = ldsResponse.Rows(0).Item("PLEGADOR_1").ToString()
            txtPiezaTeje.Text = ldsResponse.Rows(0).Item("PIEZA_1").ToString()
            txtTelarTeje.Text = ldsResponse.Rows(0).Item("TELAR_1").ToString()
            txtPlantaTeje.Text = ldsResponse.Rows(0).Item("PLANTA_1").ToString()
            txtTituloTeje.Text = ldsResponse.Rows(0).Item("TITULO_1").ToString()
            txtMetroTeje.Text = ldsResponse.Rows(0).Item("METROS_1").ToString()
            txtProcedenciaTeje.Text = ldsResponse.Rows(0).Item("PROCEDENCIA_1").ToString()
            txtVELOCIDADTELARRPMTeje.Text = ldsResponse.Rows(0).Item("VELOCIDAD_TELAR_1").ToString()
            txtObsVELOCIDADTELARRPMTeje.Text = ldsResponse.Rows(0).Item("VELOCIDAD_TELAR_OBS_1").ToString()
            txtANCHOPEINERPMTEje.Text = ldsResponse.Rows(0).Item("ANCHO_PEINE_1").ToString()
            txtObsANCHOPEINERPMTEje.Text = ldsResponse.Rows(0).Item("ANCHO_PEINE_OBS_1").ToString()
            txtOtrosTeje.Text = ldsResponse.Rows(0).Item("OTROS_1").ToString()
            txtObsOtrosTeje.Text = ldsResponse.Rows(0).Item("OTROS_OBS_1").ToString()
            txtCANTIDADROTURASTRAMATURNOTeje.Text = ldsResponse.Rows(0).Item("CANTIDAD_ROTURAS_TRAMA_TURNO_11").ToString()
            txtObsCANTIDADROTURASTRAMATURNOTeje.Text = ldsResponse.Rows(0).Item("CANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11").ToString()
            txtCANTIDADROTURASURDIMBRETEje.Text = ldsResponse.Rows(0).Item("CANTIDAD_ROTURAS_URDIMBRE_TURNO_11").ToString()
            txtObsCANTIDADROTURASURDIMBRETEje.Text = ldsResponse.Rows(0).Item("CANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11").ToString()
            txtPRUEBABOILOFFTEje.Text = ldsResponse.Rows(0).Item("PRUEBA_BOIL_OFF_11").ToString()
            txtObsPRUEBABOILOFFTEje.Text = ldsResponse.Rows(0).Item("PRUEBA_BOIL_OFF_OBS_11").ToString()
            txtANCHOROLLOTEje.Text = ldsResponse.Rows(0).Item("ANCHO_ROLLO_11").ToString()
            txObstANCHOROLLOTEje.Text = ldsResponse.Rows(0).Item("ANCHO_ROLLO_OBS_11").ToString()
            txtEFICIENCIATeje.Text = ldsResponse.Rows(0).Item("EFICIENCIA_11").ToString()
            txtObsEFICIENCIATeje.Text = ldsResponse.Rows(0).Item("EFICIENCIA_OBS_11").ToString()
            txtOtrosTeje2.Text = ldsResponse.Rows(0).Item("OTROS_11").ToString()
            txtObsOtrosTeje2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_11").ToString()
        End If
    End Sub
    Sub fnc_InsertarHilandria()
        Dim lobjGerencia As New clsGerencia

        lobjGerencia.fn_InsertarHilanderia(txtPruebaNro.Text,
                                                         txtFechaProduccionHila.Text,
                                                          txtMaquinaHila.Text,
                                                          txtSupervisorHila.Text,
                                                          txtOperarioHila.Text,
                                                          txtTurnoHila.Text,
                                                          txtLineaHila.Text,
                                                          txtProcesoHila.Text,
                                                          txtMaterialHila.Text,
                                                          txtFechaProduccionBobi.Text,
                                                          txtMaquinaBobi.Text,
                                                          txtSupervisorBobi.Text,
                                                          txtOperarioBobi.Text,
                                                          txtTurnoBobi.Text,
                                                          txtLineaBobi.Text,
                                                          txtProcesoBobi.Text,
                                                          txtMaterialBobi.Text,
                                                          txtFechaProduccionRecu.Text,
                                                          txtMaquinaRecu.Text,
                                                          txtSupervisorRecu.Text,
                                                          txtOperarioRecu.Text,
                                                          txtTurnoRecu.Text,
                                                          txtLineaRecu.Text,
                                                          txtProcesoRecu.Text,
                                                          txtMaterialRecu.Text,
                                                          txtContinuaHila.Text,
                                                          txtObContinuaHila.Text,
                                                          txtTorsionHilaHila.Text,
                                                          txtObsTorsionHila.Text,
                                                          txtEstirajeHila.Text,
                                                          txtObsEstirajeHila.Text,
                                                          txtVelocidadHila.Text,
                                                          txtObsVelocidadHila.Text,
                                                          txtCursoresHila.Text,
                                                          txtObsCursoresHila.Text,
                                                          txtClipHila.Text,
                                                          txtObsClipHila.Text,
                                                          txtHumedadHila.Text,
                                                          txtObsHumedadHila.Text,
                                                          txtTemperaturaHila.Text,
                                                          txtObsTemperaturaHila.Text,
                                                          txtOtrosHila.Text,
                                                          txtObsOtrosHila.Text,
                                                          txtOpenRieterHila2.Text,
                                                          txtObsOpenRieterHila2.Text,
                                                          txtTorsionHila2.Text,
                                                          txtObsTorsionHila2.Text,
                                                          txtTensionHila2.Text,
                                                          txtObsTensionHila2.Text,
                                                          txtEstirajeHila2.Text,
                                                          tstObsEstirajeHila2.Text,
                                                          txtVelocidadRotorHila2.Text,
                                                          txtObsVelocidadRotorHila2.Text,
                                                          txtVelocidadDisgregadorHila2.Text,
                                                          txtObsVelocidadDisgregadorHila2.Text,
                                                          txtHumedadRelativaSalaHila2.Text,
                                                          txtObsHumedadRelativaSalaHila2.Text,
                                                          txtTemperaturaSalaHila2.Text,
                                                          txtObsTemperaturaSalaHila2.Text,
                                                          txtOtrosHila2.Text,
                                                          txtObsOtrosHila2.Text,
                                                          txtCONERABobinado.Text,
                                                          txtObsCONERABobinado.Text,
                                                          txtTENSIONBobinado.Text,
                                                          txtObsTENSIONBobinado.Text,
                                                          txtLONGITUDBobinado.Text,
                                                          txtObsLONGITUDBobinado.Text,
                                                          txtVELOCIDADBobinado.Text,
                                                          txtObsVELOCIDADBobinado.Text,
                                                          txtPRESIÓNMARCOBobinado.Text,
                                                          txtObsPRESIÓNMARCOBobinado.Text,
                                                          TextBtxtHUMEDADRELATIVABobinado.Text,
                                                          txtObsBtxtHUMEDADRELATIVABobinado.Text,
                                                          txtTemperaturaSalaBObinado.Text,
                                                          txtObsTemperaturaSalaBObinado.Text,
                                                          txtOtrosBobinado.Text,
                                                          txtObsOtrosBobinado.Text,
                                                          txtRecubridoraRecubierto.Text,
                                                          txtObsRecubridoraRecubierto.Text,
                                                          txtVelocidadRecubridoraRecubierto.Text,
                                                          txtObsVelocidadRecubridoraRecubierto.Text,
                                                          txtRecetaRecubierto.Text,
                                                          txtObsRecetaRecubierto.Text,
                                                          txtOtrosRecubierto.Text,
                                                          txtObsOtrosRecubierto.Text
                                                        )

    End Sub
    Sub fnc_ActualizarHilandria()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarHilanderia(txtPruebaNro.Text,
                                                         txtFechaProduccionHila.Text,
                                                          txtMaquinaHila.Text,
                                                          txtSupervisorHila.Text,
                                                          txtOperarioHila.Text,
                                                          txtTurnoHila.Text,
                                                          txtLineaHila.Text,
                                                          txtProcesoHila.Text,
                                                          txtMaterialHila.Text,
                                                          txtFechaProduccionBobi.Text,
                                                          txtMaquinaBobi.Text,
                                                          txtSupervisorBobi.Text,
                                                          txtOperarioBobi.Text,
                                                          txtTurnoBobi.Text,
                                                          txtLineaBobi.Text,
                                                          txtProcesoBobi.Text,
                                                          txtMaterialBobi.Text,
                                                          txtFechaProduccionRecu.Text,
                                                          txtMaquinaRecu.Text,
                                                          txtSupervisorRecu.Text,
                                                          txtOperarioRecu.Text,
                                                          txtTurnoRecu.Text,
                                                          txtLineaRecu.Text,
                                                          txtProcesoRecu.Text,
                                                          txtMaterialRecu.Text,
                                                          txtContinuaHila.Text,
                                                          txtObContinuaHila.Text,
                                                          txtTorsionHilaHila.Text,
                                                          txtObsTorsionHila.Text,
                                                          txtEstirajeHila.Text,
                                                          txtObsEstirajeHila.Text,
                                                          txtVelocidadHila.Text,
                                                          txtObsVelocidadHila.Text,
                                                          txtCursoresHila.Text,
                                                          txtObsCursoresHila.Text,
                                                          txtClipHila.Text,
                                                          txtObsClipHila.Text,
                                                          txtHumedadHila.Text,
                                                          txtObsHumedadHila.Text,
                                                          txtTemperaturaHila.Text,
                                                          txtObsTemperaturaHila.Text,
                                                          txtOtrosHila.Text,
                                                          txtObsOtrosHila.Text,
                                                          txtOpenRieterHila2.Text,
                                                          txtObsOpenRieterHila2.Text,
                                                          txtTorsionHila2.Text,
                                                          txtObsTorsionHila2.Text,
                                                          txtTensionHila2.Text,
                                                          txtObsTensionHila2.Text,
                                                          txtEstirajeHila2.Text,
                                                          tstObsEstirajeHila2.Text,
                                                          txtVelocidadRotorHila2.Text,
                                                          txtObsVelocidadRotorHila2.Text,
                                                          txtVelocidadDisgregadorHila2.Text,
                                                          txtObsVelocidadDisgregadorHila2.Text,
                                                          txtHumedadRelativaSalaHila2.Text,
                                                          txtObsHumedadRelativaSalaHila2.Text,
                                                          txtTemperaturaSalaHila2.Text,
                                                          txtObsTemperaturaSalaHila2.Text,
                                                          txtOtrosHila2.Text,
                                                          txtObsOtrosHila2.Text,
                                                          txtCONERABobinado.Text,
                                                          txtObsCONERABobinado.Text,
                                                          txtTENSIONBobinado.Text,
                                                          txtObsTENSIONBobinado.Text,
                                                          txtLONGITUDBobinado.Text,
                                                          txtObsLONGITUDBobinado.Text,
                                                          txtVELOCIDADBobinado.Text,
                                                          txtObsVELOCIDADBobinado.Text,
                                                          txtPRESIÓNMARCOBobinado.Text,
                                                          txtObsPRESIÓNMARCOBobinado.Text,
                                                          TextBtxtHUMEDADRELATIVABobinado.Text,
                                                          txtObsBtxtHUMEDADRELATIVABobinado.Text,
                                                          txtTemperaturaSalaBObinado.Text,
                                                          txtObsTemperaturaSalaBObinado.Text,
                                                          txtOtrosBobinado.Text,
                                                          txtObsOtrosBobinado.Text,
                                                          txtRecubridoraRecubierto.Text,
                                                          txtObsRecubridoraRecubierto.Text,
                                                          txtVelocidadRecubridoraRecubierto.Text,
                                                          txtObsVelocidadRecubridoraRecubierto.Text,
                                                          txtRecetaRecubierto.Text,
                                                          txtObsRecetaRecubierto.Text,
                                                          txtOtrosRecubierto.Text,
                                                          txtObsOtrosRecubierto.Text
                                                        )

    End Sub
    Sub fnc_ObtenerHilanderia()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataTable
        ldsResponse = lobjGerencia.fn_ObtenerHilanderia(txtNroPreliminar2.Text)
        Session("ldtHilanderia") = ldsResponse
        If ldsResponse.Rows.Count > 0 Then


            txtFechaProduccionHila.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_1").ToString()
            txtMaquinaHila.Text = ldsResponse.Rows(0).Item("MAQUINA_1").ToString()
            txtSupervisorHila.Text = ldsResponse.Rows(0).Item("SUPERVISOR_1").ToString()
            txtOperarioHila.Text = ldsResponse.Rows(0).Item("OPERARIO_1").ToString()
            txtTurnoHila.Text = ldsResponse.Rows(0).Item("TURNO_1").ToString()
            txtLineaHila.Text = ldsResponse.Rows(0).Item("LINEA_1").ToString()
            txtProcesoHila.Text = ldsResponse.Rows(0).Item("PROCESO_1").ToString()
            txtMaterialHila.Text = ldsResponse.Rows(0).Item("MATERIAL_1").ToString()

            txtFechaProduccionBobi.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_2").ToString()
            txtMaquinaBobi.Text = ldsResponse.Rows(0).Item("MAQUINA_2").ToString()
            txtSupervisorBobi.Text = ldsResponse.Rows(0).Item("SUPERVISOR_2").ToString()
            txtOperarioBobi.Text = ldsResponse.Rows(0).Item("OPERARIO_2").ToString()
            txtTurnoBobi.Text = ldsResponse.Rows(0).Item("TURNO_2").ToString()
            txtLineaBobi.Text = ldsResponse.Rows(0).Item("LINEA_2").ToString()
            txtProcesoBobi.Text = ldsResponse.Rows(0).Item("PROCESO_2").ToString()
            txtMaterialBobi.Text = ldsResponse.Rows(0).Item("MATERIAL_2").ToString()

            txtFechaProduccionRecu.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_3").ToString()
            txtMaquinaRecu.Text = ldsResponse.Rows(0).Item("MAQUINA_3").ToString()
            txtSupervisorRecu.Text = ldsResponse.Rows(0).Item("SUPERVISOR_3").ToString()
            txtOperarioRecu.Text = ldsResponse.Rows(0).Item("OPERARIO_3").ToString()
            txtTurnoRecu.Text = ldsResponse.Rows(0).Item("TURNO_3").ToString()
            txtLineaRecu.Text = ldsResponse.Rows(0).Item("LINEA_3").ToString()
            txtProcesoRecu.Text = ldsResponse.Rows(0).Item("PROCESO_3").ToString()
            txtMaterialRecu.Text = ldsResponse.Rows(0).Item("MATERIAL_3").ToString()

            txtContinuaHila.Text = ldsResponse.Rows(0).Item("CONTINUA_VALOR_1").ToString()
            txtObContinuaHila.Text = ldsResponse.Rows(0).Item("CONTINUA_OBS_1").ToString()
            txtTorsionHilaHila.Text = ldsResponse.Rows(0).Item("TORSION_VALOR_1").ToString()
            txtObsTorsionHila.Text = ldsResponse.Rows(0).Item("TORSION_OBS_1").ToString()
            txtEstirajeHila.Text = ldsResponse.Rows(0).Item("ESTIRAJE_VALOR_1").ToString()
            txtObsEstirajeHila.Text = ldsResponse.Rows(0).Item("ESTIRAJE_OBS_1").ToString()
            txtVelocidadHila.Text = ldsResponse.Rows(0).Item("VELOCIDAD_VALOR_1").ToString()
            txtObsVelocidadHila.Text = ldsResponse.Rows(0).Item("VELOCIDAD_OBS_1").ToString()
            txtCursoresHila.Text = ldsResponse.Rows(0).Item("CURSORES_VALOR_1").ToString()
            txtObsCursoresHila.Text = ldsResponse.Rows(0).Item("CURSORES_OBS_1").ToString()
            txtClipHila.Text = ldsResponse.Rows(0).Item("CLIPS_VALOR_1").ToString()
            txtObsClipHila.Text = ldsResponse.Rows(0).Item("CLIPS_OBS_1").ToString()
            txtHumedadHila.Text = ldsResponse.Rows(0).Item("HUMEDAD_RELATIVA_VALOR_1").ToString()
            txtObsHumedadHila.Text = ldsResponse.Rows(0).Item("HUMEDAD_RELATIVA_OBS_1").ToString()
            txtTemperaturaHila.Text = ldsResponse.Rows(0).Item("TEMPERATURA_SALA_VALOR_1").ToString()
            txtObsTemperaturaHila.Text = ldsResponse.Rows(0).Item("TEMPERATURA_SALA_OBS_1").ToString()
            txtOtrosHila.Text = ldsResponse.Rows(0).Item("OTROS_VALOR_1").ToString()
            txtObsOtrosHila.Text = ldsResponse.Rows(0).Item("OTROS_OBS_1").ToString()

            txtOpenRieterHila2.Text = ldsResponse.Rows(0).Item("OPEN_RIETER_11").ToString()
            txtObsOpenRieterHila2.Text = ldsResponse.Rows(0).Item("OPEN_RIETER_OBS_11").ToString()
            txtTorsionHila2.Text = ldsResponse.Rows(0).Item("TORSION_VALOR_11").ToString()
            txtObsTorsionHila2.Text = ldsResponse.Rows(0).Item("TORSION_OBS_11").ToString()
            txtTensionHila2.Text = ldsResponse.Rows(0).Item("TENSION_VALOR_11").ToString()
            txtObsTensionHila2.Text = ldsResponse.Rows(0).Item("TENSION_OBS_11").ToString()
            txtEstirajeHila2.Text = ldsResponse.Rows(0).Item("ESTIRAJE_VALOR_11").ToString()
            tstObsEstirajeHila2.Text = ldsResponse.Rows(0).Item("ESTIRAJE_OBS_11").ToString()
            txtVelocidadRotorHila2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_ROTOR_VALOR_11").ToString()
            txtObsVelocidadRotorHila2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_ROTOR_OBS_11").ToString()

            txtVelocidadDisgregadorHila2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_DISGREGADOR_VALOR_11").ToString()
            txtObsVelocidadDisgregadorHila2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_DISGREGADOR_VALOR_OBS_11").ToString()

            txtHumedadRelativaSalaHila2.Text = ldsResponse.Rows(0).Item("HUMEDAD_RELATIVA_VALOR_11").ToString()
            txtObsHumedadRelativaSalaHila2.Text = ldsResponse.Rows(0).Item("HUMEDAD_RELATIVA_OBS_11").ToString()
            txtTemperaturaSalaHila2.Text = ldsResponse.Rows(0).Item("TEMPERATURA_SALA_VALOR_11").ToString()
            txtObsTemperaturaSalaHila2.Text = ldsResponse.Rows(0).Item("TEMPERATURA_SALA_OBS_11").ToString()
            txtOtrosHila2.Text = ldsResponse.Rows(0).Item("OTROS_VALOR_11").ToString()
            txtObsOtrosHila2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_11").ToString()
            txtCONERABobinado.Text = ldsResponse.Rows(0).Item("CONERA_VALOR_2").ToString()
            txtObsCONERABobinado.Text = ldsResponse.Rows(0).Item("CONERA_OBS_2").ToString()
            txtTENSIONBobinado.Text = ldsResponse.Rows(0).Item("TENSION_VALOR_2").ToString()
            txtObsTENSIONBobinado.Text = ldsResponse.Rows(0).Item("TENSION_OBS_2").ToString()
            txtLONGITUDBobinado.Text = ldsResponse.Rows(0).Item("LONGITUD_VALOR_2").ToString()
            txtObsLONGITUDBobinado.Text = ldsResponse.Rows(0).Item("LONGITUD_OBS_2").ToString()
            txtVELOCIDADBobinado.Text = ldsResponse.Rows(0).Item("VELOCIDAD_VALOR_2").ToString()
            txtObsVELOCIDADBobinado.Text = ldsResponse.Rows(0).Item("VELOCIDAD_OBS_2").ToString()
            txtPRESIÓNMARCOBobinado.Text = ldsResponse.Rows(0).Item("PRESION_MARCO_PORTABOBINA_VALOR_2").ToString()
            txtObsPRESIÓNMARCOBobinado.Text = ldsResponse.Rows(0).Item("PRESION_MARCO_PORTABOBINA_OBS_2").ToString()
            TextBtxtHUMEDADRELATIVABobinado.Text = ldsResponse.Rows(0).Item("HUMEDAD_RELATIVA_VALOR_2").ToString()
            txtObsBtxtHUMEDADRELATIVABobinado.Text = ldsResponse.Rows(0).Item("HUMEDAD_RELATIVA_OBS_2").ToString()
            txtTemperaturaSalaBObinado.Text = ldsResponse.Rows(0).Item("TEMPERATURA_SALA_VALOR_2").ToString()
            txtObsTemperaturaSalaBObinado.Text = ldsResponse.Rows(0).Item("TEMPERATURA_SALA_OBS_2").ToString()
            txtOtrosBobinado.Text = ldsResponse.Rows(0).Item("OTROS_VALOR_2").ToString()
            txtObsOtrosBobinado.Text = ldsResponse.Rows(0).Item("OTROS_OBS_2").ToString()

            txtRecubridoraRecubierto.Text = ldsResponse.Rows(0).Item("RECUBRIDORA_VALOR_3").ToString()
            txtObsRecubridoraRecubierto.Text = ldsResponse.Rows(0).Item("RECUBRIDORA_OBS_3").ToString()
            txtVelocidadRecubridoraRecubierto.Text = ldsResponse.Rows(0).Item("VELOCIDAD_RECUBRIDORA_VALOR_3").ToString()
            txtObsVelocidadRecubridoraRecubierto.Text = ldsResponse.Rows(0).Item("VELOCIDAD_RECUBRIDORA_OBS_3").ToString()
            txtRecetaRecubierto.Text = ldsResponse.Rows(0).Item("RECETA_VALOR_3").ToString()
            txtObsRecetaRecubierto.Text = ldsResponse.Rows(0).Item("RECETA_OBS_3").ToString()
            txtOtrosRecubierto.Text = ldsResponse.Rows(0).Item("OTROS_VALOR_3").ToString()
            txtObsOtrosRecubierto.Text = ldsResponse.Rows(0).Item("OTROS_OBS_3").ToString()

            txtRecubridoraRecubierto2.Text = ldsResponse.Rows(0).Item("RECUBRIDORA_VALOR_3").ToString()
            txtObsRecubridoraRecubierto2.Text = ldsResponse.Rows(0).Item("RECUBRIDORA_OBS_3").ToString()
            txtVelocidadRecubridoraRecubierto2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_RECUBRIDORA_VALOR_3").ToString()
            txtObsVelocidadRecubridoraRecubierto2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_RECUBRIDORA_OBS_3").ToString()
            txtRecetaRecubierto2.Text = ldsResponse.Rows(0).Item("RECETA_VALOR_3").ToString()
            txtObsRecetaRecubierto2.Text = ldsResponse.Rows(0).Item("RECETA_OBS_3").ToString()
            txtOtrosRecubierto2.Text = ldsResponse.Rows(0).Item("OTROS_VALOR_3").ToString()
            txtObsOtrosRecubierto2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_3").ToString()
        End If
    End Sub
    Sub fnc_InsertarLaboratotioHilanderia()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_InsertarLaboratorioHIlanderia(txtPruebaNro.Text,
        txtFechaEvaluacionHilande.Text,
        txtLaboratoristaHilande.Text,
        txtTurnoHilande.Text,
        txtLineaHilande.Text,
        txtProcesoHilande.Text,
        txtMaquinaHilande.Text,
        txtMaterialHilande.Text,
        txtFINURAFIBRAHilande.Text,
        txRESISTENCIAFIBRAHilande.Text,
        txtLONGITUDFIBRAHilande.Text,
        txtUNIFORMIDADFIBRAHilande.Text,
        txtTÍTULOCINTAHILOHilande.Text,
        txtCvTÍTULOHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAHILOHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAPDHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAPGHilande.Text,
        txtPRUEBAUNIFORMIDADCINTANEPSHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAHHilande.Text,
        txtRESISTENCIAHILOHilande.Text,
        txtRESISTENCIAMÍNIMAHILOHilande.Text,
        txtRESISTENCIALHILOHilande.Text,
        txtELONGACIÓNLHILOHilande.Text,
        txtTORSIÓNHILOHilande.Text,
        txtROTURASHUSOsHORAHilande.Text,
        txtRESISTENCIAEMPALMEHilande.Text,
        txtPesoConoHilande.Text,
        txtDUREZACONOHiland.Text,
        txtOtrosHilande.Text,
         txtObsFINURAFIBRAHilande.Text,
        txObsRESISTENCIAFIBRAHilande.Text,
        txtObsLONGITUDFIBRAHilande.Text,
        txtObsUNIFORMIDADFIBRAHilande.Text,
        txtObsTÍTULOCINTAHILOHilande.Text,
        txtObsCvTÍTULOHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAHILOHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAPDHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAPGHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTANEPSHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAHHilande.Text,
        txtObsRESISTENCIAHILOHilande.Text,
        txtObsRESISTENCIAMÍNIMAHILOHilande.Text,
        txtObsRESISTENCIALHILOHilande.Text,
        txtObsELONGACIÓNLHILOHilande.Text,
        txtObsTORSIÓNHILOHilande.Text,
        txtObsROTURASHUSOsHORAHilande.Text,
        txtObsRESISTENCIAEMPALMEHilande.Text,
        txtObsPesoConoHilande.Text,
        txtObsDUREZACONOHiland.Text,
        txtObsOtrosHilande.Text)

    End Sub
    Sub fnc_ActualizarLaboratotioHilanderia()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarLaboratorioHIlanderia(
        txtPruebaNro.Text,
        txtFechaEvaluacionHilande.Text,
        txtLaboratoristaHilande.Text,
        txtTurnoHilande.Text,
        txtLineaHilande.Text,
        txtProcesoHilande.Text,
        txtMaquinaHilande.Text,
        txtMaterialHilande.Text,
        txtFINURAFIBRAHilande.Text,
        txRESISTENCIAFIBRAHilande.Text,
        txtLONGITUDFIBRAHilande.Text,
        txtUNIFORMIDADFIBRAHilande.Text,
        txtTÍTULOCINTAHILOHilande.Text,
        txtCvTÍTULOHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAHILOHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAPDHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAPGHilande.Text,
        txtPRUEBAUNIFORMIDADCINTANEPSHilande.Text,
        txtPRUEBAUNIFORMIDADCINTAHHilande.Text,
        txtRESISTENCIAHILOHilande.Text,
        txtRESISTENCIAMÍNIMAHILOHilande.Text,
        txtRESISTENCIALHILOHilande.Text,
        txtELONGACIÓNLHILOHilande.Text,
        txtTORSIÓNHILOHilande.Text,
        txtROTURASHUSOsHORAHilande.Text,
        txtRESISTENCIAEMPALMEHilande.Text,
        txtPesoConoHilande.Text,
        txtDUREZACONOHiland.Text,
        txtOtrosHilande.Text,
         txtObsFINURAFIBRAHilande.Text,
        txObsRESISTENCIAFIBRAHilande.Text,
        txtObsLONGITUDFIBRAHilande.Text,
        txtObsUNIFORMIDADFIBRAHilande.Text,
        txtObsTÍTULOCINTAHILOHilande.Text,
        txtObsCvTÍTULOHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAHILOHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAPDHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAPGHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTANEPSHilande.Text,
        txtObsPRUEBAUNIFORMIDADCINTAHHilande.Text,
        txtObsRESISTENCIAHILOHilande.Text,
        txtObsRESISTENCIAMÍNIMAHILOHilande.Text,
        txtObsRESISTENCIALHILOHilande.Text,
        txtObsELONGACIÓNLHILOHilande.Text,
        txtObsTORSIÓNHILOHilande.Text,
        txtObsROTURASHUSOsHORAHilande.Text,
        txtObsRESISTENCIAEMPALMEHilande.Text,
        txtObsPesoConoHilande.Text,
        txtObsDUREZACONOHiland.Text,
        txtObsOtrosHilande.Text)
    End Sub
    Sub fnc_ObenerLaboratorioHilnaderia()
        Dim lobjGerencia As New clsGerencia
        Dim LaboratorioHilnaderia As DataTable
        LaboratorioHilnaderia = lobjGerencia.fn_ObtenerLaboratorioHIlanderia(txtNroPreliminar2.Text)
        Session("LaboratorioHilnaderia") = LaboratorioHilnaderia
        If LaboratorioHilnaderia.Rows.Count > 0 Then
            txtFechaEvaluacionHilande.Text = LaboratorioHilnaderia.Rows(0).Item("FECHA_PRODUCCION_1").ToString()
            txtLaboratoristaHilande.Text = LaboratorioHilnaderia.Rows(0).Item("LABORATORISTA_1").ToString()
            txtTurnoHilande.Text = LaboratorioHilnaderia.Rows(0).Item("TURNO_1").ToString()
            txtLineaHilande.Text = LaboratorioHilnaderia.Rows(0).Item("LINEA_1").ToString()
            txtProcesoHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PROCESO_1").ToString()
            txtMaquinaHilande.Text = LaboratorioHilnaderia.Rows(0).Item("MAQUINA_1").ToString()
            txtMaterialHilande.Text = LaboratorioHilnaderia.Rows(0).Item("MATERIAL_1").ToString()
            txtFINURAFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("FINURA_FIBRA_1").ToString()
            txRESISTENCIAFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_FIBRA_1").ToString()
            txtLONGITUDFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("LONGITUD_FIBRA_1").ToString()
            txtUNIFORMIDADFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("UNIFORMIDAD_FIBRA_1").ToString()
            txtTÍTULOCINTAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("TITULO_CINTA_HILO_1").ToString()
            txtCvTÍTULOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("Cv_TÍTULO_1").ToString()
            txtPRUEBAUNIFORMIDADCINTAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1").ToString()
            txtPRUEBAUNIFORMIDADCINTAPDHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1").ToString()
            txtPRUEBAUNIFORMIDADCINTAPGHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1").ToString()
            txtPRUEBAUNIFORMIDADCINTANEPSHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1").ToString()
            txtPRUEBAUNIFORMIDADCINTAHHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_H_1").ToString()
            txtRESISTENCIAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_HILO_RKM_1").ToString()
            txtRESISTENCIAMÍNIMAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_MINIMA_HILO_RKM_1").ToString()
            txtRESISTENCIALHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("Cv_RESISTENCIA_HILO_1").ToString()
            txtELONGACIÓNLHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("ELONGACION_HILO_1").ToString()
            txtTORSIÓNHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("TORSION_HILO_1").ToString()
            txtROTURASHUSOsHORAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("ROTURAS_1000_HUSOS_HORA_1").ToString()
            txtRESISTENCIAEMPALMEHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_EMPALME_1").ToString()
            txtPesoConoHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PESO_CONO_1").ToString()
            txtDUREZACONOHiland.Text = LaboratorioHilnaderia.Rows(0).Item("DUREZA_CONO_1").ToString()
            txtOtrosHilande.Text = LaboratorioHilnaderia.Rows(0).Item("OTROS_1").ToString()
            txtObsFINURAFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("FINURA_FIBRA_OBS_1").ToString()
            txObsRESISTENCIAFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_FIBRA_OBS_1").ToString()
            txtObsLONGITUDFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("LONGITUD_FIBRA_OBS_1").ToString()
            txtObsUNIFORMIDADFIBRAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("UNIFORMIDAD_FIBRA_OBS_1").ToString()
            txtObsTÍTULOCINTAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("TITULO_CINTA_HILO_OBS_1").ToString()
            txtObsCvTÍTULOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("Cv_TÍTULO_OBS_1").ToString()
            txtObsPRUEBAUNIFORMIDADCINTAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1").ToString()
            txtObsPRUEBAUNIFORMIDADCINTAPDHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1").ToString()
            txtObsPRUEBAUNIFORMIDADCINTAPGHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1").ToString()
            txtObsPRUEBAUNIFORMIDADCINTANEPSHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1").ToString()
            txtObsPRUEBAUNIFORMIDADCINTAHHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1").ToString()
            txtObsRESISTENCIAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_HILO_RKM_OBS_1").ToString()
            txtObsRESISTENCIAMÍNIMAHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_MINIMA_HILO_RKM_OBS_1").ToString()
            txtObsRESISTENCIALHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("Cv_RESISTENCIA_HILO_OBS_1").ToString()
            txtObsELONGACIÓNLHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("ELONGACION_HILO_OBS_1").ToString()
            txtObsTORSIÓNHILOHilande.Text = LaboratorioHilnaderia.Rows(0).Item("TORSION_HILO_OBS_1").ToString()
            txtObsROTURASHUSOsHORAHilande.Text = LaboratorioHilnaderia.Rows(0).Item("ROTURAS_OBS_1000_HUSOS_HORA_OBS_1").ToString()
            txtObsRESISTENCIAEMPALMEHilande.Text = LaboratorioHilnaderia.Rows(0).Item("RESISTENCIA_EMPALME_OBS_1").ToString()
            txtObsPesoConoHilande.Text = LaboratorioHilnaderia.Rows(0).Item("PESO_CONO_OBS_1").ToString()
            txtObsDUREZACONOHiland.Text = LaboratorioHilnaderia.Rows(0).Item("DUREZA_CONO_OBS_1").ToString()
            txtObsOtrosHilande.Text = LaboratorioHilnaderia.Rows(0).Item("OTROS_OBS_1").ToString()
        End If
    End Sub
    Sub fnc_InsertarPretejido()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_InsertarPretejido(
        txtPruebaNro.Text,
        txtFechaProduccionPrete.Text,
        txtMaquinaPrete.Text,
        txtSupervisorPrete.Text,
        txtMaquinistaPrete.Text,
        txtTurnoPrete.Text,
        txtPartidaPrete.Text,
        txtUrdimbrePrete.Text,
        txtAnalistaPrete.Text,
        txtVelocidadMaquinaPrete.Text,
        txtObsVelocidadMaquinaPrete.Text,
        txtTensionMaquinaPrete.Text,
        txtObsTensionMaquinaPrete.Text,
        txtOtrosPrete.Text,
        txtObsOtrosPrete.Text,
        txtRotulasMIllonPrete.Text,
        txtObsRotulasMIllonPrete.Text,
        txtTensionHiloPrete.Text,
        txtObsTensionHiloPrete.Text,
        txtOtrosPreteTenido.Text,
        txtObsOtrosPreteTenido.Text,
        txtFechaProduccionPrete2.Text,
        txtMaquinaPrete2.Text,
        txtSupervisorPrete2.Text,
        txtMaquinistaPrete2.Text,
        txtTurnoPrete2.Text,
        txtPartidaPrete2.Text,
        txtArticuloPrete2.Text,
        txtAnalistaPrete2.Text,
        txtVELOCIDADMÁQUINAPret.Text,
        txtObsVELOCIDADMÁQUINAPret.Text,
        txtTENSIÓNCABEZALPrete.Text,
        txtObsTENSIÓNCABEZALPrete.Text,
        txtTENSIÓNFILETAPrete.Text,
        txtObsTENSIÓNFILETAPrete.Text,
        txtPRESIONESRODILLOPrete.Text,
        txtObsPRESIONESRODILLOPrete.Text,
        txtDENSIDADSODAPrete.Text,
        txtObsDENSIDADSODAPrete.Text,
        txtTEMPERATURATINASPrete.Text,
        txtObsTEMPERATURATINASPrete.Text,
        txtCONCENTRACIONINDIGHIDROSULFITOPrete.Text,
        txtObsCONCENTRACIONINDIGHIDROSULFITOPrete.Text,
        txtOtrosPreteUrdido.Text,
        txtObsOtrosPreteUrdido.Text,
        txtROTURASKMPrete.Text,
        txtObsROTURASKMPrete.Text,
        txtVISCOSIDADGOMAPrete.Text,
        txtObsVISCOSIDADGOMAPrete.Text,
        txtOTROSPreteTenido2.Text,
        txtObsOTROSPreteTenido2.Text,
        txtCONCENTRACIONINDHOROSULFITOPrete.Text,
        txtObsCONCENTRACIONINDHOROSULFITOPrete.Text
        )
    End Sub
    Sub fnc_ActualizarPretejido()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarPretejido(
        txtPruebaNro.Text,
        txtFechaProduccionPrete.Text,
        txtMaquinaPrete.Text,
        txtSupervisorPrete.Text,
        txtMaquinistaPrete.Text,
        txtTurnoPrete.Text,
        txtPartidaPrete.Text,
        txtUrdimbrePrete.Text,
        txtAnalistaPrete.Text,
        txtVelocidadMaquinaPrete.Text,
        txtObsVelocidadMaquinaPrete.Text,
        txtTensionMaquinaPrete.Text,
        txtObsTensionMaquinaPrete.Text,
        txtOtrosPrete.Text,
        txtObsOtrosPrete.Text,
        txtRotulasMIllonPrete.Text,
        txtObsRotulasMIllonPrete.Text,
        txtTensionHiloPrete.Text,
        txtObsTensionHiloPrete.Text,
        txtOtrosPreteTenido.Text,
        txtObsOtrosPreteTenido.Text,
        txtFechaProduccionPrete2.Text,
        txtMaquinaPrete2.Text,
        txtSupervisorPrete2.Text,
        txtMaquinistaPrete2.Text,
        txtTurnoPrete2.Text,
        txtPartidaPrete2.Text,
        txtArticuloPrete2.Text,
        txtAnalistaPrete2.Text,
        txtVELOCIDADMÁQUINAPret.Text,
        txtObsVELOCIDADMÁQUINAPret.Text,
        txtTENSIÓNCABEZALPrete.Text,
        txtObsTENSIÓNCABEZALPrete.Text,
        txtTENSIÓNFILETAPrete.Text,
        txtObsTENSIÓNFILETAPrete.Text,
        txtPRESIONESRODILLOPrete.Text,
        txtObsPRESIONESRODILLOPrete.Text,
        txtDENSIDADSODAPrete.Text,
        txtObsDENSIDADSODAPrete.Text,
        txtTEMPERATURATINASPrete.Text,
        txtObsTEMPERATURATINASPrete.Text,
        txtCONCENTRACIONINDIGHIDROSULFITOPrete.Text,
        txtObsCONCENTRACIONINDIGHIDROSULFITOPrete.Text,
        txtOtrosPreteUrdido.Text,
        txtObsOtrosPreteUrdido.Text,
        txtROTURASKMPrete.Text,
        txtObsROTURASKMPrete.Text,
        txtVISCOSIDADGOMAPrete.Text,
        txtObsVISCOSIDADGOMAPrete.Text,
        txtOTROSPreteTenido2.Text,
        txtObsOTROSPreteTenido2.Text,
        txtCONCENTRACIONINDHOROSULFITOPrete.Text,
        txtObsCONCENTRACIONINDHOROSULFITOPrete.Text
        )
    End Sub
    Sub fnc_ObtenerPretejido()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataTable
        ldsResponse = lobjGerencia.fn_ObtenerLPretejido(txtNroPreliminar2.Text)

        Session("ldtPretejido") = ldsResponse
        If ldsResponse.Rows.Count > 0 Then
            txtFechaProduccionPrete.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_1").ToString()
            txtMaquinaPrete.Text = ldsResponse.Rows(0).Item("MAQUINA_1").ToString()
            txtSupervisorPrete.Text = ldsResponse.Rows(0).Item("SUPERVISOR_1").ToString()
            txtMaquinistaPrete.Text = ldsResponse.Rows(0).Item("MAQUINISTA_1").ToString()
            txtTurnoPrete.Text = ldsResponse.Rows(0).Item("TURNO_1").ToString()
            txtPartidaPrete.Text = ldsResponse.Rows(0).Item("PARTIDA_1").ToString()
            txtUrdimbrePrete.Text = ldsResponse.Rows(0).Item("URDIMBRE_1").ToString()
            txtAnalistaPrete.Text = ldsResponse.Rows(0).Item("ANALISTA_1").ToString()
            txtVelocidadMaquinaPrete.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MAQUINA_1").ToString()
            txtObsVelocidadMaquinaPrete.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MAQUINA_OBS_1").ToString()
            txtTensionMaquinaPrete.Text = ldsResponse.Rows(0).Item("TENSION_MAQUINA_1").ToString()
            txtObsTensionMaquinaPrete.Text = ldsResponse.Rows(0).Item("TENSION_MAQUINA_OBS_1").ToString()
            txtOtrosPrete.Text = ldsResponse.Rows(0).Item("OTROS_1").ToString()
            txtObsOtrosPrete.Text = ldsResponse.Rows(0).Item("OTROS_OBS_1").ToString()
            txtRotulasMIllonPrete.Text = ldsResponse.Rows(0).Item("ROTURAS_MILLÓN_11").ToString()
            txtObsRotulasMIllonPrete.Text = ldsResponse.Rows(0).Item("ROTURAS_MILLÓN_OBS_11").ToString()
            txtTensionHiloPrete.Text = ldsResponse.Rows(0).Item("TENSION_HILO_11").ToString()
            txtObsTensionHiloPrete.Text = ldsResponse.Rows(0).Item("TENSION_HILO_OBS_11").ToString()
            txtOtrosPreteTenido.Text = ldsResponse.Rows(0).Item("OTROS_11").ToString()
            txtObsOtrosPreteTenido.Text = ldsResponse.Rows(0).Item("OTROS_OBS_11").ToString()
            txtFechaProduccionPrete2.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_2").ToString()
            txtMaquinaPrete2.Text = ldsResponse.Rows(0).Item("MAQUINA_2").ToString()
            txtSupervisorPrete2.Text = ldsResponse.Rows(0).Item("SUPERVISOR_2").ToString()
            txtMaquinistaPrete2.Text = ldsResponse.Rows(0).Item("MAQUINISTA_2").ToString()
            txtTurnoPrete2.Text = ldsResponse.Rows(0).Item("TURNO_2").ToString()
            txtPartidaPrete2.Text = ldsResponse.Rows(0).Item("PARTIDA_2").ToString()
            txtArticuloPrete2.Text = ldsResponse.Rows(0).Item("ARTICULO_2").ToString()
            txtAnalistaPrete2.Text = ldsResponse.Rows(0).Item("ANALISTA_2").ToString()
            txtVELOCIDADMÁQUINAPret.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MAQUINA_2").ToString()
            txtObsVELOCIDADMÁQUINAPret.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MAQUINA_OBS_2").ToString()
            txtTENSIÓNCABEZALPrete.Text = ldsResponse.Rows(0).Item("TENSION_CABEZAL_2").ToString()
            txtObsTENSIÓNCABEZALPrete.Text = ldsResponse.Rows(0).Item("TENSION_CABEZAL_OBS_2").ToString()
            txtTENSIÓNFILETAPrete.Text = ldsResponse.Rows(0).Item("TENSIÓN_FILETA_2").ToString()
            txtObsTENSIÓNFILETAPrete.Text = ldsResponse.Rows(0).Item("TENSIÓN_FILETA_OBS_2").ToString()
            txtPRESIONESRODILLOPrete.Text = ldsResponse.Rows(0).Item("PRESIONES_RODILLO_2").ToString()
            txtObsPRESIONESRODILLOPrete.Text = ldsResponse.Rows(0).Item("PRESIONES_RODILLO_OBS_2").ToString()
            txtDENSIDADSODAPrete.Text = ldsResponse.Rows(0).Item("DENSIDAD_SODA_2").ToString()
            txtObsDENSIDADSODAPrete.Text = ldsResponse.Rows(0).Item("DENSIDAD_SODA_OBS_2").ToString()
            txtTEMPERATURATINASPrete.Text = ldsResponse.Rows(0).Item("TEMPERATURA__TINAS_TENIDO_2").ToString()
            txtObsTEMPERATURATINASPrete.Text = ldsResponse.Rows(0).Item("TEMPERATURA__TINAS_TENIDO_OBS_2").ToString()
            txtCONCENTRACIONINDIGHIDROSULFITOPrete.Text = ldsResponse.Rows(0).Item("CONCENTRACION_INDIGO_2").ToString()
            txtObsCONCENTRACIONINDIGHIDROSULFITOPrete.Text = ldsResponse.Rows(0).Item("CONCENTRACION_INDIGO_OBS_2").ToString()

            txtCONCENTRACIONINDHOROSULFITOPrete.Text = ldsResponse.Rows(0).Item("CONCENTRACION_HOROSULFITO_2").ToString()
            txtObsCONCENTRACIONINDHOROSULFITOPrete.Text = ldsResponse.Rows(0).Item("CONCENTRACION_HOROSULFITO_OBS_2").ToString()
            txtOtrosPreteUrdido.Text = ldsResponse.Rows(0).Item("OTROS_2").ToString()
            txtObsOtrosPreteUrdido.Text = ldsResponse.Rows(0).Item("OTROS_OBS_2").ToString()
            txtROTURASKMPrete.Text = ldsResponse.Rows(0).Item("ROTURAS_22").ToString()
            txtObsROTURASKMPrete.Text = ldsResponse.Rows(0).Item("ROTURAS_OBS_22").ToString()
            txtVISCOSIDADGOMAPrete.Text = ldsResponse.Rows(0).Item("VISCOSIDAD_GOMA_22").ToString()
            txtObsVISCOSIDADGOMAPrete.Text = ldsResponse.Rows(0).Item("VISCOSIDAD_GOMA_OBS_22").ToString()
            txtOTROSPreteTenido2.Text = ldsResponse.Rows(0).Item("OTROS_22").ToString()
            txtObsOTROSPreteTenido2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_22").ToString()
        End If
    End Sub

    Sub fnc_InsertarTintoreria()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_InsertarTinteroria(Session("TipoPrueba"),
            txtNroPreliminar2.Text,
        txtFechProduccionTinto.Text,
        txtMaquinaTinto.Text,
        txtSupervisorTinto.Text,
        txtMaquinistaTinto.Text,
        txtTurnoTinto.Text,
        txtArticuloTinto.Text,
        txtFichaTinto.Text,
        txtProcesoTinto.Text,
         txtMetrosTinto.Text,
        txtAnalistaTinto.Text,
        txtVELOCIDADMÁQUINAminTinto1.Text,
        txtObsVELOCIDADMÁQUINAminTinto1.Text,
        txtTEMPERATURACTinto1.Text,
        txtObsTEMPERATURACTinto1.Text,
        txtINTENSIDADLLAMAmbarTinto1.Text,
        txtObsINTENSIDADLLAMAmbarTinto1.Text,
        txtOtrosTinto1.Text,
        txtObsOtrosTinto1.Text,
        txtANCHOINGRESOcmTinto1.Text,
        txtObsANCHOINGRESOcmTinto1.Text,
        txtANCHOSALIDAcmTinto.Text,
        txtObsANCHOSALIDAcmTinto.Text,
        txtOtrosTinto11.Text,
        txtObsOtrosTinto11.Text,
        txtFechaProduccionTinto2.Text,
        txtMaquinaTinto2.Text,
        txtSupervisorTinto2.Text,
        txtMaquinistaTinto2.Text,
        txtTurnoTinto2.Text,
        txtArticuloTinto2.Text,
        txtFichaTinto2.Text,
        txtProcesoTinto2.Text,
         txtMetrosTinto2.Text,
        txtAnalistaTinto2.Text,
        txtVelocidadMaquinaTinto2.Text,
        txtObsVelocidadMaquinaTinto2.Text,
        txtTemperaturasCalderaTinto2.Text,
        txtObsTemperaturasCalderaTinto2.Text,
        txtOtrosTinto2.Text,
        txtObsOtrosTinto2.Text,
        txtAnchoIngresoTinto2.Text,
        txtObsAnchoIngresoTinto2.Text,
        txtAnchoSalidaTinto2.Text,
        txtObsAnchoSalidaTinto2.Text,
        txtOtrosTinto22.Text,
        txtObsOtrosTinto22.Text,
        txtFechaProduccionTinto3.Text,
        txtMaquinaTinto3.Text,
        txtSupervisorTinto3.Text,
        txtMaquinistaTinto3.Text,
        txtTurnoTinto3.Text,
        txtArticuloTinto3.Text,
        txtFichaTinto3.Text,
        txtProcesoTinto3.Text,
        txtMetrosTinto3.Text,
        txtAnalistaTinto3.Text,
        txtVelicidadMaquinaTinto3.Text,
        txtoBSVelicidadMaquinaTinto3.Text,
        txtTEMPERATURAmaQUINATinto3.Text,
        txtObsTEMPERATURAmaQUINATinto3.Text,
        txtTemperaturaCaldero3.Text,
        txtObsTemperaturaCaldero3.Text,
        txtVentiladoresTinto3.Text,
        txtObsVentiladoresTinto3.Text,
        txtAnchooSalidaTinto3.Text,
        txtObsAnchooSalidaTinto3.Text,
        txtExtractoresTinto3.Text,
        txtObsExtractoresTinto3.Text,
        txtOtrosTinto3.Text,
        txtObsOtrosTinto3.Text,
        txtAnchoIngresoTinto3.Text,
        txtObsAnchoIngresoTinto3.Text,
        txtAnchoSalidaTinto3.Text,
        txtObsAnchoSalidaTinto3.Text,
        txtOtrosTinto33.Text,
        txtObsOtrosTinto33.Text,
        txtFechaProduccionTinto4.Text,
        txtMaquinaTinto4.Text,
        txtSupervisorTinto4.Text,
        txtMAquinistaTinto4.Text,
        txtTurnoTinto4.Text,
        txtArticuloTinto4.Text,
        txtFichaTinto4.Text,
        txtProcesoTinto4.Text,
        txtMetrosTinto4.Text,
        txtAnalistaTinto4.Text,
        txtVelocidadMaquinaTinto4.Text,
        txtObsVelocidadMaquinaTinto4.Text,
        txtTemperaturaBandaTinto4.Text,
        txtObsTemperaturaBandaTinto4.Text,
        txtTemperaturaPalmerTinto4.Text,
        txtObsTemperaturaPalmerTinto4.Text,
        txtOtrosTinto4.Text,
        txtObsOtrosTinto4.Text,
        txtAnchoIngresoTinto4.Text,
        txtObsAnchoIngresoTinto4.Text,
        txtAnchoSalidaTinto4.Text,
        txtObsAnchoSalidaTinto4.Text,
        txtPruebaTendidoAnchoAcbTinto4.Text,
        txtObsPruebaTendidoAnchoAcbTinto4.Text,
        txtOtrosTinto44.Text,
        txtObsOtrosTinto44.Text
)
    End Sub

    Sub fnc_ActualizarTintoreria()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarTinteroria(Session("TipoPrueba"),
                                             txtNroPreliminar2.Text,
        txtFechProduccionTinto.Text,
        txtMaquinaTinto.Text,
        txtSupervisorTinto.Text,
        txtMaquinistaTinto.Text,
        txtTurnoTinto.Text,
        txtArticuloTinto.Text,
        txtFichaTinto.Text,
        txtProcesoTinto.Text,
         txtMetrosTinto.Text,
        txtAnalistaTinto.Text,
        txtVELOCIDADMÁQUINAminTinto1.Text,
        txtObsVELOCIDADMÁQUINAminTinto1.Text,
        txtTEMPERATURACTinto1.Text,
        txtObsTEMPERATURACTinto1.Text,
        txtINTENSIDADLLAMAmbarTinto1.Text,
        txtObsINTENSIDADLLAMAmbarTinto1.Text,
        txtOtrosTinto1.Text,
        txtObsOtrosTinto1.Text,
        txtANCHOINGRESOcmTinto1.Text,
        txtObsANCHOINGRESOcmTinto1.Text,
        txtANCHOSALIDAcmTinto.Text,
        txtObsANCHOSALIDAcmTinto.Text,
        txtOtrosTinto11.Text,
        txtObsOtrosTinto11.Text,
        txtFechaProduccionTinto2.Text,
        txtMaquinaTinto2.Text,
        txtSupervisorTinto2.Text,
        txtMaquinistaTinto2.Text,
        txtTurnoTinto2.Text,
        txtArticuloTinto2.Text,
        txtFichaTinto2.Text,
        txtProcesoTinto2.Text,
         txtMetrosTinto2.Text,
        txtAnalistaTinto2.Text,
        txtVelocidadMaquinaTinto2.Text,
        txtObsVelocidadMaquinaTinto2.Text,
        txtTemperaturasCalderaTinto2.Text,
        txtObsTemperaturasCalderaTinto2.Text,
        txtOtrosTinto2.Text,
        txtObsOtrosTinto2.Text,
        txtAnchoIngresoTinto2.Text,
        txtObsAnchoIngresoTinto2.Text,
        txtAnchoSalidaTinto2.Text,
        txtObsAnchoSalidaTinto2.Text,
        txtOtrosTinto22.Text,
        txtObsOtrosTinto22.Text,
        txtFechaProduccionTinto3.Text,
        txtMaquinaTinto3.Text,
        txtSupervisorTinto3.Text,
        txtMaquinistaTinto3.Text,
        txtTurnoTinto3.Text,
        txtArticuloTinto3.Text,
        txtFichaTinto3.Text,
        txtProcesoTinto3.Text,
        txtMetrosTinto3.Text,
        txtAnalistaTinto3.Text,
        txtVelicidadMaquinaTinto3.Text,
        txtoBSVelicidadMaquinaTinto3.Text,
        txtTEMPERATURAmaQUINATinto3.Text,
        txtObsTEMPERATURAmaQUINATinto3.Text,
        txtTemperaturaCaldero3.Text,
        txtObsTemperaturaCaldero3.Text,
        txtVentiladoresTinto3.Text,
        txtObsVentiladoresTinto3.Text,
        txtAnchooSalidaTinto3.Text,
        txtObsAnchooSalidaTinto3.Text,
        txtExtractoresTinto3.Text,
        txtObsExtractoresTinto3.Text,
        txtOtrosTinto3.Text,
        txtObsOtrosTinto3.Text,
        txtAnchoIngresoTinto3.Text,
        txtObsAnchoIngresoTinto3.Text,
        txtAnchoSalidaTinto3.Text,
        txtObsAnchoSalidaTinto3.Text,
        txtOtrosTinto33.Text,
        txtObsOtrosTinto33.Text,
        txtFechaProduccionTinto4.Text,
        txtMaquinaTinto4.Text,
        txtSupervisorTinto4.Text,
        txtMAquinistaTinto4.Text,
        txtTurnoTinto4.Text,
        txtArticuloTinto4.Text,
        txtFichaTinto4.Text,
        txtProcesoTinto4.Text,
        txtMetrosTinto4.Text,
        txtAnalistaTinto4.Text,
        txtVelocidadMaquinaTinto4.Text,
        txtObsVelocidadMaquinaTinto4.Text,
        txtTemperaturaBandaTinto4.Text,
        txtObsTemperaturaBandaTinto4.Text,
        txtTemperaturaPalmerTinto4.Text,
        txtObsTemperaturaPalmerTinto4.Text,
        txtOtrosTinto4.Text,
        txtObsOtrosTinto4.Text,
        txtAnchoIngresoTinto4.Text,
        txtObsAnchoIngresoTinto4.Text,
        txtAnchoSalidaTinto4.Text,
        txtObsAnchoSalidaTinto4.Text,
        txtPruebaTendidoAnchoAcbTinto4.Text,
        txtObsPruebaTendidoAnchoAcbTinto4.Text,
        txtOtrosTinto44.Text,
        txtObsOtrosTinto44.Text
)
    End Sub

    Sub fnc_ObtenerTintoreria()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataTable
        ldsResponse = lobjGerencia.fn_ObtenerTinteroria(txtNroPreliminar2.Text, Session("TipoPrueba").ToString())

        Session("ldtTintoreria") = ldsResponse
        If ldsResponse.Rows.Count > 0 Then
            txtFechProduccionTinto.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_1").ToString()
            txtMaquinaTinto.Text = ldsResponse.Rows(0).Item("MAQUINA_1").ToString()
            txtSupervisorTinto.Text = ldsResponse.Rows(0).Item("SUPERVISOR_1").ToString()
            txtMaquinistaTinto.Text = ldsResponse.Rows(0).Item("MAQUINISTA_1").ToString()
            txtTurnoTinto.Text = ldsResponse.Rows(0).Item("TURNO_1").ToString()
            txtArticuloTinto.Text = ldsResponse.Rows(0).Item("ARTICULO_1").ToString()
            txtFichaTinto.Text = ldsResponse.Rows(0).Item("FICHA_1").ToString()
            txtProcesoTinto.Text = ldsResponse.Rows(0).Item("PROCESO_1").ToString()
            txtMetrosTinto.Text = ldsResponse.Rows(0).Item("METROS_1").ToString()
            txtAnalistaTinto.Text = ldsResponse.Rows(0).Item("ANALISTA_1").ToString()
            txtVELOCIDADMÁQUINAminTinto1.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_1").ToString()
            txtObsVELOCIDADMÁQUINAminTinto1.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_OBS_1").ToString()
            txtTEMPERATURACTinto1.Text = ldsResponse.Rows(0).Item("TEMPERATURA_1").ToString()
            txtObsTEMPERATURACTinto1.Text = ldsResponse.Rows(0).Item("TEMPERATURA_OBS_1").ToString()
            txtINTENSIDADLLAMAmbarTinto1.Text = ldsResponse.Rows(0).Item("INTENSIDAD_LLAMA_1").ToString()
            txtObsINTENSIDADLLAMAmbarTinto1.Text = ldsResponse.Rows(0).Item("INTENSIDAD_LLAMA_OBS_1").ToString()
            txtOtrosTinto1.Text = ldsResponse.Rows(0).Item("OTROS_1").ToString()
            txtObsOtrosTinto1.Text = ldsResponse.Rows(0).Item("OTROS_OBS_1").ToString()
            txtANCHOINGRESOcmTinto1.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_11").ToString()
            txtObsANCHOINGRESOcmTinto1.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_OBS_11").ToString()
            txtANCHOSALIDAcmTinto.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_11").ToString()
            txtObsANCHOSALIDAcmTinto.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_OBS_11").ToString()
            txtOtrosTinto11.Text = ldsResponse.Rows(0).Item("OTROS_11").ToString()
            txtObsOtrosTinto11.Text = ldsResponse.Rows(0).Item("OTROS_OBS_11").ToString()
            txtFechaProduccionTinto2.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_2").ToString()
            txtMaquinaTinto2.Text = ldsResponse.Rows(0).Item("MAQUINA_2").ToString()
            txtSupervisorTinto2.Text = ldsResponse.Rows(0).Item("SUPERVISOR_2").ToString()
            txtMaquinistaTinto2.Text = ldsResponse.Rows(0).Item("MAQUINISTA_2").ToString()
            txtTurnoTinto2.Text = ldsResponse.Rows(0).Item("TURNO_2").ToString()
            txtArticuloTinto2.Text = ldsResponse.Rows(0).Item("ARTICULO_2").ToString()
            txtFichaTinto2.Text = ldsResponse.Rows(0).Item("FICHA_2").ToString()
            txtProcesoTinto2.Text = ldsResponse.Rows(0).Item("PROCESO_2").ToString()
            txtMetrosTinto2.Text = ldsResponse.Rows(0).Item("METROS_2").ToString()
            txtAnalistaTinto2.Text = ldsResponse.Rows(0).Item("ANALISTA_2").ToString()
            txtVelocidadMaquinaTinto2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_2").ToString()
            txtObsVelocidadMaquinaTinto2.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_OBS_2").ToString()
            txtTemperaturasCalderaTinto2.Text = ldsResponse.Rows(0).Item("TEMPERATURA_TINAS_2").ToString()
            txtObsTemperaturasCalderaTinto2.Text = ldsResponse.Rows(0).Item("TEMPERATURA_TINAS_OBS_2").ToString()
            txtOtrosTinto2.Text = ldsResponse.Rows(0).Item("OTROS_2").ToString()
            txtObsOtrosTinto2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_2").ToString()
            txtAnchoIngresoTinto2.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_22").ToString()
            txtObsAnchoIngresoTinto2.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_OBS_22").ToString()
            txtAnchoSalidaTinto2.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_22").ToString()
            txtObsAnchoSalidaTinto2.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_OBS_22").ToString()
            txtOtrosTinto22.Text = ldsResponse.Rows(0).Item("OTROS_22").ToString()
            txtObsOtrosTinto22.Text = ldsResponse.Rows(0).Item("OTROS_OBS_22").ToString()
            txtFechaProduccionTinto3.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_3").ToString()
            txtMaquinaTinto3.Text = ldsResponse.Rows(0).Item("MAQUINA_3").ToString()
            txtSupervisorTinto3.Text = ldsResponse.Rows(0).Item("SUPERVISOR_3").ToString()
            txtMaquinistaTinto3.Text = ldsResponse.Rows(0).Item("MAQUINISTA_3").ToString()
            txtTurnoTinto3.Text = ldsResponse.Rows(0).Item("TURNO_3").ToString()
            txtArticuloTinto3.Text = ldsResponse.Rows(0).Item("ARTICULO_3").ToString()
            txtFichaTinto3.Text = ldsResponse.Rows(0).Item("FICHA_3").ToString()
            txtProcesoTinto3.Text = ldsResponse.Rows(0).Item("PROCESO_3").ToString()
            txtMetrosTinto3.Text = ldsResponse.Rows(0).Item("METROS_3").ToString()
            txtAnalistaTinto3.Text = ldsResponse.Rows(0).Item("ANALISTA_3").ToString()
            txtVelicidadMaquinaTinto3.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_3").ToString()
            txtoBSVelicidadMaquinaTinto3.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_OBS_3").ToString()
            txtTEMPERATURAmaQUINATinto3.Text = ldsResponse.Rows(0).Item("TEMPERATURA_MAQUINAS_3").ToString()
            txtObsTEMPERATURAmaQUINATinto3.Text = ldsResponse.Rows(0).Item("TEMPERATURA_MAQUINAS_OBS_3").ToString()
            txtTemperaturaCaldero3.Text = ldsResponse.Rows(0).Item("TEMPERATURA_CALDERO_3").ToString()
            txtObsTemperaturaCaldero3.Text = ldsResponse.Rows(0).Item("TEMPERATURA_CALDERO_OBS_3").ToString()
            txtVentiladoresTinto3.Text = ldsResponse.Rows(0).Item("VENTILADORES_3").ToString()
            txtObsVentiladoresTinto3.Text = ldsResponse.Rows(0).Item("VENTILADORES_OBS_3").ToString()

            txtAnchooSalidaTinto3.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_m_3").ToString()
            txtObsAnchooSalidaTinto3.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_m_OBS_3").ToString()
            txtExtractoresTinto3.Text = ldsResponse.Rows(0).Item("EXTRACTORES_3").ToString()
            txtObsExtractoresTinto3.Text = ldsResponse.Rows(0).Item("EXTRACTORES_OBS_3").ToString()
            txtOtrosTinto3.Text = ldsResponse.Rows(0).Item("OTROS_3").ToString()
            txtObsOtrosTinto3.Text = ldsResponse.Rows(0).Item("OTROS_OBS_3").ToString()
            txtAnchoIngresoTinto3.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_33").ToString()
            txtObsAnchoIngresoTinto3.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_OBS_33").ToString()
            txtAnchoSalidaTinto3.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_33").ToString()
            txtObsAnchoSalidaTinto3.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_OBS_33").ToString()
            txtOtrosTinto33.Text = ldsResponse.Rows(0).Item("OTROS_33").ToString()
            txtObsOtrosTinto33.Text = ldsResponse.Rows(0).Item("OTROS_OBS_33").ToString()
            txtFechaProduccionTinto4.Text = ldsResponse.Rows(0).Item("FECHA_PRODUCCION_4").ToString()
            txtMaquinaTinto4.Text = ldsResponse.Rows(0).Item("MAQUINA_4").ToString()
            txtSupervisorTinto4.Text = ldsResponse.Rows(0).Item("SUPERVISOR_4").ToString()
            txtMAquinistaTinto4.Text = ldsResponse.Rows(0).Item("MAQUINISTA_4").ToString()
            txtTurnoTinto4.Text = ldsResponse.Rows(0).Item("TURNO_4").ToString()
            txtArticuloTinto4.Text = ldsResponse.Rows(0).Item("ARTICULO_4").ToString()
            txtFichaTinto4.Text = ldsResponse.Rows(0).Item("FICHA_4").ToString()
            txtProcesoTinto4.Text = ldsResponse.Rows(0).Item("PROCESO_4").ToString()
            txtMetrosTinto4.Text = ldsResponse.Rows(0).Item("METROS_4").ToString()
            txtAnalistaTinto4.Text = ldsResponse.Rows(0).Item("ANALISTA_4").ToString()
            txtVelocidadMaquinaTinto4.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_4").ToString()
            txtObsVelocidadMaquinaTinto4.Text = ldsResponse.Rows(0).Item("VELOCIDAD_MÁQUINA_OBS_4").ToString()
            txtTemperaturaBandaTinto4.Text = ldsResponse.Rows(0).Item("TEMPERATURA_BANDA_4").ToString()
            txtObsTemperaturaBandaTinto4.Text = ldsResponse.Rows(0).Item("TEMPERATURA_BANDA_OBS_4").ToString()
            txtTemperaturaPalmerTinto4.Text = ldsResponse.Rows(0).Item("TEMPERATURA_PALMER_4").ToString()
            txtObsTemperaturaPalmerTinto4.Text = ldsResponse.Rows(0).Item("TEMPERATURA_PALMER_OBS_4").ToString()
            txtOtrosTinto4.Text = ldsResponse.Rows(0).Item("OTROS_4").ToString()
            txtObsOtrosTinto4.Text = ldsResponse.Rows(0).Item("OTROS_OBS_4").ToString()
            txtAnchoIngresoTinto4.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_44").ToString()
            txtObsAnchoIngresoTinto4.Text = ldsResponse.Rows(0).Item("ANCHO_INGRESO_OBS_44").ToString()
            txtAnchoSalidaTinto4.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_44").ToString()
            txtObsAnchoSalidaTinto4.Text = ldsResponse.Rows(0).Item("ANCHO_SALIDA_OBS_44").ToString()
            txtPruebaTendidoAnchoAcbTinto4.Text = ldsResponse.Rows(0).Item("PRUEBA_TENDIDO_ANCHO_ACABADO_44").ToString()
            txtObsPruebaTendidoAnchoAcbTinto4.Text = ldsResponse.Rows(0).Item("PRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44").ToString()
            txtOtrosTinto44.Text = ldsResponse.Rows(0).Item("OTROS_44").ToString()
            txtObsOtrosTinto44.Text = ldsResponse.Rows(0).Item("OTROS_OBS_44").ToString()



        End If
    End Sub

    Sub fnc_InsertarLaboratorioFisico()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_InsertarLaboratorioFisico(
        txtNroPreliminar2.Text,
        txtFechaEvaluacionLaboFisico.Text,
        txtLaboratoristaLaboFisi.Text,
        txtTurnoLaboFisi.Text,
        txtArticuloLaboFisi.Text,
        txtFichaLaboFisi.Text,
        txtProcesoLaboFisi.Text,
        txtTipoAcabadoLaboFisi.Text,
        txtMetrosLaboFisi.Text,
        txtANCHOACABADOLaboFisi.Text,
        txtObsANCHOACABADOLaboFisi.Text,
        txtENCOGURDIMBRELaboFisi.Text,
        txtObsENCOGURDIMBRELaboFisi.Text,
        txtENCOGTRAMALaboFisi.Text,
        txtObsENCOGTRAMALaboFisi.Text,
        txtELONGACIÓNLaboFisi.Text,
        txtObsELONGACIÓNLaboFisi.Text,
        txtREVIRADODERECHOLaboFisi.Text,
        txtObsREVIRADODERECHOLaboFisi.Text,
        txtReviradoCentroLaboFisi.Text,
        txtObsReviradoCentroLaboFisi.Text,
        txtReviradoIzquierdoLaboFisi.Text,
        txtObsReviradoIzquierdoLaboFisi.Text,
        txtResistenciaUrdimbreLaboFisi.Text,
        txtObsResistenciaUrdimbreLaboFisi.Text,
        txtResistenciaTramaLaboFisi.Text,
        txtObsResistenciaTramaLaboFisi.Text,
        txtPesoLaboFisi.Text,
        txtObsPesoLaboFisi.Text,
        txtOtrosLaboFisi.Text,
        txObstOtrosLaboFisi.Text,
        txtRetroLycraLaboFisi.Text,
        txtObsRetroLycraLaboFisi.Text,
        txtPRUEBARETORNORECOVERYLaboFisi.Text,
        txtObsPRUEBARETORNORECOVERYLaboFisi.Text,
        txtSolidezHumedoLaboFisi.Text,
        txtSObsolidezHumedoLaboFisi.Text,
        txtSolidezSecoLaboFisi.Text,
        txtObsSolidezSecoLaboFisi.Text,
        txtSolidezLavadoTransferenciaLaboFisi.Text,
        txtObsSolidezLavadoTransferenciaLaboFisi.Text,
        txtSolidezLavadoLaboFisi.Text,
        txtObsObsSolidezLavadoLaboFisi.Text,
        txtDensidadTejdoLaboFisi.Text,
        txtObsDensidadTejdoLaboFisi.Text,
        txtOtrosLaboFisi2.Text,
        txtObsOtrosLaboFisi2.Text,
        txtDensidadHilosTejdoLaboFisi.Text,
        txtObsDensidadHilosTejdoLaboFisi.Text
)
    End Sub

    Sub fnc_ActualizarLaboratorioFisico()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarLaboratorioFisico(
        txtNroPreliminar2.Text,
        txtFechaEvaluacionLaboFisico.Text,
        txtLaboratoristaLaboFisi.Text,
        txtTurnoLaboFisi.Text,
        txtArticuloLaboFisi.Text,
        txtFichaLaboFisi.Text,
        txtProcesoLaboFisi.Text,
        txtTipoAcabadoLaboFisi.Text,
        txtMetrosLaboFisi.Text,
        txtANCHOACABADOLaboFisi.Text,
        txtObsANCHOACABADOLaboFisi.Text,
        txtENCOGURDIMBRELaboFisi.Text,
        txtObsENCOGURDIMBRELaboFisi.Text,
        txtENCOGTRAMALaboFisi.Text,
        txtObsENCOGTRAMALaboFisi.Text,
        txtELONGACIÓNLaboFisi.Text,
        txtObsELONGACIÓNLaboFisi.Text,
        txtREVIRADODERECHOLaboFisi.Text,
        txtObsREVIRADODERECHOLaboFisi.Text,
        txtReviradoCentroLaboFisi.Text,
        txtObsReviradoCentroLaboFisi.Text,
        txtReviradoIzquierdoLaboFisi.Text,
        txtObsReviradoIzquierdoLaboFisi.Text,
        txtResistenciaUrdimbreLaboFisi.Text,
        txtObsResistenciaUrdimbreLaboFisi.Text,
        txtResistenciaTramaLaboFisi.Text,
        txtObsResistenciaTramaLaboFisi.Text,
        txtPesoLaboFisi.Text,
        txtObsPesoLaboFisi.Text,
        txtOtrosLaboFisi.Text,
        txObstOtrosLaboFisi.Text,
        txtRetroLycraLaboFisi.Text,
        txtObsRetroLycraLaboFisi.Text,
        txtPRUEBARETORNORECOVERYLaboFisi.Text,
        txtObsPRUEBARETORNORECOVERYLaboFisi.Text,
        txtSolidezHumedoLaboFisi.Text,
        txtSObsolidezHumedoLaboFisi.Text,
        txtSolidezSecoLaboFisi.Text,
        txtObsSolidezSecoLaboFisi.Text,
        txtSolidezLavadoTransferenciaLaboFisi.Text,
        txtObsSolidezLavadoTransferenciaLaboFisi.Text,
        txtSolidezLavadoLaboFisi.Text,
        txtObsObsSolidezLavadoLaboFisi.Text,
        txtDensidadTejdoLaboFisi.Text,
        txtObsDensidadTejdoLaboFisi.Text,
        txtOtrosLaboFisi2.Text,
        txtObsOtrosLaboFisi2.Text,
         txtDensidadHilosTejdoLaboFisi.Text,
        txtObsDensidadHilosTejdoLaboFisi.Text
)
    End Sub

    Sub fnc_ObtenerLaboratorioFisico()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataTable
        ldsResponse = lobjGerencia.fn_ObtenerLaboratorioFisico(txtNroPreliminar2.Text)

        Session("ldtLaboratorioFisico") = ldsResponse
        If ldsResponse.Rows.Count > 0 Then
            txtNroPreliminar2.Text = ldsResponse.Rows(0).Item("PRUEBA_NRO").ToString()
            txtFechaEvaluacionLaboFisico.Text = ldsResponse.Rows(0).Item("FECHA_EVALUACION_1").ToString()
            txtLaboratoristaLaboFisi.Text = ldsResponse.Rows(0).Item("LABORATORISTA_1").ToString()
            txtTurnoLaboFisi.Text = ldsResponse.Rows(0).Item("TURNO_1").ToString()
            txtArticuloLaboFisi.Text = ldsResponse.Rows(0).Item("ARTICULO_1").ToString()
            txtFichaLaboFisi.Text = ldsResponse.Rows(0).Item("FICHA_1").ToString()
            txtProcesoLaboFisi.Text = ldsResponse.Rows(0).Item("PROCESO_1").ToString()
            txtTipoAcabadoLaboFisi.Text = ldsResponse.Rows(0).Item("TIPO_ACABADO_1").ToString()
            txtMetrosLaboFisi.Text = ldsResponse.Rows(0).Item("METROS_1").ToString()
            txtANCHOACABADOLaboFisi.Text = ldsResponse.Rows(0).Item("ANCHO_ACABADO_1").ToString()
            txtObsANCHOACABADOLaboFisi.Text = ldsResponse.Rows(0).Item("ANCHO_ACABADO_OBS_1").ToString()
            txtENCOGURDIMBRELaboFisi.Text = ldsResponse.Rows(0).Item("ENCOG_URDIMBRE_1").ToString()
            txtObsENCOGURDIMBRELaboFisi.Text = ldsResponse.Rows(0).Item("ENCOG_URDIMBRE_OBS_1").ToString()
            txtENCOGTRAMALaboFisi.Text = ldsResponse.Rows(0).Item("ENCOG_TRAMA_1").ToString()
            txtObsENCOGTRAMALaboFisi.Text = ldsResponse.Rows(0).Item("ENCOG_TRAMA_OBS_1").ToString()
            txtELONGACIÓNLaboFisi.Text = ldsResponse.Rows(0).Item("ELONGACION_1").ToString()
            txtObsELONGACIÓNLaboFisi.Text = ldsResponse.Rows(0).Item("ELONGACION_OBS_1").ToString()
            txtREVIRADODERECHOLaboFisi.Text = ldsResponse.Rows(0).Item("REVIRADO_DERECHO_1").ToString()
            txtObsREVIRADODERECHOLaboFisi.Text = ldsResponse.Rows(0).Item("REVIRADO_DERECHO_OBS_1").ToString()
            txtReviradoCentroLaboFisi.Text = ldsResponse.Rows(0).Item("REVIRADO_CENTRO_1").ToString()
            txtObsReviradoCentroLaboFisi.Text = ldsResponse.Rows(0).Item("REVIRADO_CENTRO_OBS_1").ToString()
            txtReviradoIzquierdoLaboFisi.Text = ldsResponse.Rows(0).Item("REVIRADO_IZQUIERDO_1").ToString()
            txtObsReviradoIzquierdoLaboFisi.Text = ldsResponse.Rows(0).Item("REVIRADO_IZQUIERDO_OBS_1").ToString()
            txtResistenciaUrdimbreLaboFisi.Text = ldsResponse.Rows(0).Item("RESISTENCIA_URDIMBRE_1").ToString()
            txtObsResistenciaUrdimbreLaboFisi.Text = ldsResponse.Rows(0).Item("RESISTENCIA_URDIMBRE_OBS_1").ToString()
            txtResistenciaTramaLaboFisi.Text = ldsResponse.Rows(0).Item("RESISTENCIA_TRAMA_1").ToString()
            txtObsResistenciaTramaLaboFisi.Text = ldsResponse.Rows(0).Item("RESISTENCIA_TRAMA_OBS_1").ToString()
            txtPesoLaboFisi.Text = ldsResponse.Rows(0).Item("PESO_1").ToString()
            txtObsPesoLaboFisi.Text = ldsResponse.Rows(0).Item("PESO_OBS_1").ToString()
            txtOtrosLaboFisi.Text = ldsResponse.Rows(0).Item("OTROS_1").ToString()
            txObstOtrosLaboFisi.Text = ldsResponse.Rows(0).Item("OTROS_OBS_1").ToString()
            txtRetroLycraLaboFisi.Text = ldsResponse.Rows(0).Item("RETIRO_LYCRA_11").ToString()
            txtObsRetroLycraLaboFisi.Text = ldsResponse.Rows(0).Item("ETIRO_LYCRA_OBS_11").ToString()
            txtPRUEBARETORNORECOVERYLaboFisi.Text = ldsResponse.Rows(0).Item("PRUEBA_RETORNO_11").ToString()
            txtObsPRUEBARETORNORECOVERYLaboFisi.Text = ldsResponse.Rows(0).Item("PRUEBA_RETORNO_OBS_11").ToString()
            txtSolidezHumedoLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_FROTE_HUMEDO_11").ToString()
            txtSObsolidezHumedoLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_FROTE_HUMEDO_OBS_11").ToString()
            txtSolidezSecoLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_FROTE_SECO_11").ToString()
            txtObsSolidezSecoLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_FROTE_SECO_OBS_11").ToString()
            txtSolidezLavadoTransferenciaLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11").ToString()
            txtObsSolidezLavadoTransferenciaLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11").ToString()
            txtSolidezLavadoLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_LAVADO_CAMBIO_COLOR_11").ToString()
            txtObsObsSolidezLavadoLaboFisi.Text = ldsResponse.Rows(0).Item("SOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11").ToString()
            txtDensidadTejdoLaboFisi.Text = ldsResponse.Rows(0).Item("DENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11").ToString()
            txtObsDensidadTejdoLaboFisi.Text = ldsResponse.Rows(0).Item("DENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11").ToString()
            txtOtrosLaboFisi2.Text = ldsResponse.Rows(0).Item("OTROS_11").ToString()
            txtObsOtrosLaboFisi2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_11").ToString()
            txtDensidadHilosTejdoLaboFisi.Text = ldsResponse.Rows(0).Item("DENSIDAD_TEJIDO_HILO_POR_PULGADAS_11").ToString()
            txtObsDensidadHilosTejdoLaboFisi.Text = ldsResponse.Rows(0).Item("DENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11").ToString()
        End If
    End Sub
    Sub fnc_InsertarRevisionFinal()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_InsertarRevisionFinal(txtNroPreliminar2.Text,
        txtFechaMapeadoRFinal.Text,
        txtRevisadorRfinal.Text,
        txtTurno.Text,
        txtArticuloRfinal.Text,
         txtFichaRFinal.Text,
        txtFechaCorteRFinal.Text,
        txtCortadorRFinal.Text,
        txtAnalistaRfinal.Text,
        txtMetrosCortadosRFinal.Text,
        txtObsMetrosCortadosRFinal.Text,
        txtMetrosSegundasRFinal.Text,
        txtObsMetrosSegundasRFinal.Text,
        txtPrincipalDefectoRFinal.Text,
        txtObsPrincipalDefectoRFinal.Text,
        txtOtrosRfinal.Text,
        txtObsOtrosRfinal.Text,
        txtPrincipalDefecto2RFinal.Text,
        txtObsPrincipalDefecto2RFinal.Text,
        txtPrinciaplDefecto3RFinal.Text,
        txtObsPrinciaplDefecto3RFinal.Text,
        txtOtrosRFinal2.Text,
        txtObsOtrosRFinal2.Text
        )
    End Sub
    Sub fnc_ActualizarRevisionFinal()
        Dim lobjGerencia As New clsGerencia
        lobjGerencia.fn_ActualizarRevisionFinal(
            txtNroPreliminar2.Text,
        txtFechaMapeadoRFinal.Text,
       txtRevisadorRfinal.Text,
       txtTurno.Text,
       txtArticuloRfinal.Text,
        txtFichaRFinal.Text,
       txtFechaCorteRFinal.Text,
       txtCortadorRFinal.Text,
       txtAnalistaRfinal.Text,
       txtMetrosCortadosRFinal.Text,
       txtObsMetrosCortadosRFinal.Text,
       txtMetrosSegundasRFinal.Text,
       txtObsMetrosSegundasRFinal.Text,
       txtPrincipalDefectoRFinal.Text,
       txtObsPrincipalDefectoRFinal.Text,
       txtOtrosRfinal.Text,
       txtObsOtrosRfinal.Text,
       txtPrincipalDefecto2RFinal.Text,
       txtObsPrincipalDefecto2RFinal.Text,
       txtPrinciaplDefecto3RFinal.Text,
       txtObsPrinciaplDefecto3RFinal.Text,
       txtOtrosRFinal2.Text,
       txtObsOtrosRFinal2.Text
        )
    End Sub
    Sub fnc_ObtenerRevisionFinal()
        Dim lobjGerencia As New clsGerencia
        Dim ldsResponse As DataTable
        ldsResponse = lobjGerencia.fn_ObtenerLRevisionFinal(txtNroPreliminar2.Text)

        Session("ldtRevisionFinal") = ldsResponse
        If ldsResponse.Rows.Count > 0 Then
            txtNroPreliminar2.Text = ldsResponse.Rows(0).Item("PRUEBA_NRO").ToString()
            txtFechaMapeadoRFinal.Text = ldsResponse.Rows(0).Item("FECHA_MAPEADO_1").ToString()
            txtRevisadorRfinal.Text = ldsResponse.Rows(0).Item("REVISADOR_1").ToString()
            txtTurno.Text = ldsResponse.Rows(0).Item("TURNO_1").ToString()
            txtArticuloRfinal.Text = ldsResponse.Rows(0).Item("ARTICULO_1").ToString()
            txtFichaRFinal.Text = ldsResponse.Rows(0).Item("FICHA_1").ToString()
            txtFechaCorteRFinal.Text = ldsResponse.Rows(0).Item("FECHA_CORTE_1").ToString()
            txtCortadorRFinal.Text = ldsResponse.Rows(0).Item("CORTADOR_1").ToString()
            txtAnalistaRfinal.Text = ldsResponse.Rows(0).Item("ANALISTA_1").ToString()
            txtMetrosCortadosRFinal.Text = ldsResponse.Rows(0).Item("METROS_CORTADOS_1").ToString()
            txtObsMetrosCortadosRFinal.Text = ldsResponse.Rows(0).Item("METROS_CORTADOS_OBS_1").ToString()
            txtMetrosSegundasRFinal.Text = ldsResponse.Rows(0).Item("METROS_SEGUNDAS_OBSERVADAS_1").ToString()
            txtObsMetrosSegundasRFinal.Text = ldsResponse.Rows(0).Item("METROS_SEGUNDAS_OBSERVADAS_OBS_1").ToString()
            txtPrincipalDefectoRFinal.Text = ldsResponse.Rows(0).Item("PRINCIPAL_DEFECTO_1_1").ToString()
            txtObsPrincipalDefectoRFinal.Text = ldsResponse.Rows(0).Item("PRINCIPAL_DEFECTO_1_OBS_1").ToString()
            txtOtrosRfinal.Text = ldsResponse.Rows(0).Item("OTROS_1").ToString()
            txtObsOtrosRfinal.Text = ldsResponse.Rows(0).Item("OTROS_OBS_1").ToString()
            txtPrincipalDefecto2RFinal.Text = ldsResponse.Rows(0).Item("PRINCIPAL_DEFECTO_2_11").ToString()
            txtObsPrincipalDefecto2RFinal.Text = ldsResponse.Rows(0).Item("PRINCIPAL_DEFECTO_2_OBS_11").ToString()
            txtPrinciaplDefecto3RFinal.Text = ldsResponse.Rows(0).Item("PRINCIPAL_DEFECTO_3_11").ToString()
            txtObsPrinciaplDefecto3RFinal.Text = ldsResponse.Rows(0).Item("PRINCIPAL_DEFECTO_3_OBS_11").ToString()
            txtOtrosRFinal2.Text = ldsResponse.Rows(0).Item("OTROS_11").ToString()
            txtObsOtrosRFinal2.Text = ldsResponse.Rows(0).Item("OTROS_OBS_11").ToString()


        End If
    End Sub
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
    Protected Sub ibtDescargardocumento_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDescargardocumento.Click
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



End Class