Public Class frmIngresoSalidaDesperdicios
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "LALANOCA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtFechaIni.Text = "01/" + IIf(Now.Month < 10, "0" + Now.Month.ToString(), Now.Month.ToString()) + "/" + Now.Year.ToString()
            txtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        sMostrarReporte()
    End Sub

    Private Sub sMostrarReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strProceso As String

        strProceso = ddlProcesos.SelectedValue
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        If rdlReportes.SelectedValue = 1 Then
            strURL = strURL + strPath + "log_ingreso_desperdicio_detallado"
        End If
        If rdlReportes.SelectedValue = 2 Then
            strURL = strURL + strPath + "log_salida_desperdicio_detallado"
        End If
        strURL = strURL & "&vch_CodEmpresa=" & Session("@EMPRESA")
        strURL = strURL & "&dtm_FechaInicio=" + Format(CDate(txtFechaIni.Text), "yyyyMMdd")
        strURL = strURL & "&dtm_FecchaFin=" + Format(CDate(txtFechaFin.Text), "yyyyMMdd")
        strURL = strURL & "&vch_CodProceso=" & strProceso
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
End Class