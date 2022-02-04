Imports NM_General

Public Class frmListFormatoCambioPuesto
    Inherits System.Web.UI.Page

#Region "Funciones"

    Sub fn_listarFormatosCambioPuesto()
        Dim lstrCodArea As String = ""
        Dim lstrCodTrab As String = ""
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
        lstrCodTrab = txtResponsable.Text.Trim().ToUpper()

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

        lstrEstado = ddlEstado.SelectedValue().ToString()

        ldtResponse = lobjGerencia.fn_listarFormatoCPT(lstrCodArea,
                                                      lstrCodTrab,
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "DGAMARRA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                    Dim objRequest As New BLITZ_LOCK.clsRequest
                    Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
                End If

                If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                    Response.Redirect("../../intranet/finsesion.htm")
                End If
            End If

            fn_listarFormatosCambioPuesto()
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        fn_listarFormatosCambioPuesto()
    End Sub

    Protected Sub gvFormatoCP_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gvFormatoCP.ItemCommand
        If e.CommandName.ToString().Equals("Visualizar") Then
            Response.Redirect("frmFormatoCambioPuesto.aspx?pidCodGenFormato=" + e.CommandArgument)
        End If

        If e.CommandName.ToString().Equals("VerPDF") Then
            Dim strURL As String = ""
            Dim strPath As String = ""
            Dim strScript As String = ""
            Dim strCodGenerado As String
            Dim strCodFormato As String

            strCodGenerado = e.CommandArgument
            strCodFormato = "5"

            If strCodGenerado.Length > 0 Then
                'CAMBIO DG INI
                'strPath = "%2fNM_Reportes%2f"
                'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
                'strURL = strURL + "logistica_InventarioDiario"
                strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
                strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloGerencia")
                strURL = strURL + strPath + "gg_formato_cambio_puesto"
                'CAMBIO DG FIN
                strURL = strURL + "&pintCodFormato=" + strCodFormato
                strURL = strURL + "&pintCodGenerado=" + strCodGenerado

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
        Response.Redirect("frmFormatoCambioPuesto.aspx")
    End Sub
End Class