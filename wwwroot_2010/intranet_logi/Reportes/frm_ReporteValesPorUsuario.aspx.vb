Public Class frm_ReporteValesPorUsuario
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
        If Not IsPostBack Then
            txtFechaIni.Text = Now.ToString("dd/MM/yyyy")
            txtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        If Not txtFechaIni.Text.Trim.Equals("") And Not txtFechaFin.Text.Trim.Equals("") Then
            If Not Convert.ToDateTime(txtFechaIni.Text.Trim) > Convert.ToDateTime(txtFechaFin.Text.Trim) Then
                sMostrarReporte()
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaError", "<script language=javascript>alert('La fecha fin no puede ser mayor a la fecha inicio.');</script>")
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "AlertaError", "<script language=javascript>alert('Debe seleccionar un rango de fechas');</script>")
        End If
    End Sub
    Private Sub sMostrarReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strFe_Inic As String
        Dim strFe_Fina As String

        strFe_Inic = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
        strFe_Fina = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath + "log_reporte_vales_por_usuario"
        strURL = strURL & "&FECH_INI=" + strFe_Inic
        strURL = strURL & "&FECHA_FIN=" + strFe_Fina
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
End Class