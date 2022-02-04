Public Class frm_Vacaciones_Pendientes
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'txtFecha.Text = Date.Today.ToShortDateString
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim strFecha As String = ""
        'strFecha = Mid(txtFecha.Text, 7, 4) + Mid(txtFecha.Text, 4, 2) + Mid(txtFecha.Text, 1, 2)
        ListarDetalle(strFecha)
    End Sub

    ' reporte
    Private Sub ListarDetalle(strFecha As String)
        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        'strPath = "%2fNM_Reportes%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")
        strURL = strURL + strPath

        'strURL = strURL + "planilla_VacacionesFecha"
        strURL = strURL + "pla_Vacaciones_Pendientes"
        'strURL = strURL + "&vch_FechaFin=" + strFecha

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
End Class