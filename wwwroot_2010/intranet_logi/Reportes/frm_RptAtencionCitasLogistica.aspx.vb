Public Class frm_RptAtencionCitasLogistica
    Inherits System.Web.UI.Page

    Private Sub frm_RptAtencionCitasLogistica_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "AAMPUERP"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.TxtFechaIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
            Me.TxtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click

        If TxtFechaIni.Text = "" Then
            lblMsg.Text = "Debe seleccionar la Fecha de Inicio."
        ElseIf TxtFechaFin.Text = "" Then
            lblMsg.Text = "Debe seleccionar la Fecha Final."
        Else
            lblMsg.Text = ""
            Call sMostrarReporte()
        End If

    End Sub

    Private Sub sMostrarReporte()

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath
        'CAMBIO DG FIN
        txtUrl.Text = strURL

        'strURL = strURL + "log_atencion_citas_proveedor"

        strURL = strURL + "log_atencion_citas_proveedor_2"

        '    strURL = strURL + "&pvch_FechaInicio=" + Format(CDate(TxtFechaIni.Text), "yyyyMMdd")
        'strURL = strURL + "&pvch_FechaFinal=" + Format(CDate(TxtFechaFin.Text), "yyyyMMdd")

        strURL = strURL + "&p_vchFechaIni=" + Format(CDate(TxtFechaIni.Text), "yyyyMMdd")
        strURL = strURL + "&p_vchFechaFin=" + Format(CDate(TxtFechaFin.Text), "yyyyMMdd")

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"

            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

End Class