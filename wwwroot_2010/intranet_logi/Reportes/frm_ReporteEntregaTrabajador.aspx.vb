Public Class frm_ReporteEntregaTrabajador
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
            'Session("@USUARIO") = "DGAMARRA"

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
            txtFechaIni.Text = Now.ToString("dd/MM/yyyy")
            txtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        sMostrarReporte()
    End Sub
    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strSerie As String = ""

        If rdbSerie3.Checked = True Then
            strSerie = "0003"
        Else
            strSerie = "0005"
        End If

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath + "log_reporte_entrega_trabajador"
        strURL = strURL & "&VAR_FECH_INI=" & txtFechaIni.Text
        strURL = strURL & "&VAR_FECH_FIN=" & txtFechaFin.Text
        strURL = strURL & "&VAR_TIPO=" & strSerie
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub

    Private Sub rdbSerie3_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbSerie3.CheckedChanged
        If rdbSerie3.Checked = True Then
            rdbSerie5.Checked = False
        End If
    End Sub

    Private Sub rdbSerie5_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbSerie5.CheckedChanged
        If rdbSerie5.Checked = True Then
            rdbSerie3.Checked = False
        End If
    End Sub
End Class