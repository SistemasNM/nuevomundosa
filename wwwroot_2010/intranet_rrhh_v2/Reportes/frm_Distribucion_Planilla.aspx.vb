Public Class frm_Distribucion_Planilla
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAnio As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPlanilla As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents Form2 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents btnexportar As System.Web.UI.WebControls.Button
    Protected WithEvents btnVer As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            Session("@USUARIO") = "BENITO"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
           If Not IsPostBack Then
            Marcar()
            End If
    End Sub
    Private Sub Marcar()
        Dim lstrmesactual As String
        lstrmesactual = Strings.Format(Month(Now), "00")
        ddlMes.SelectedItem.Selected = False
        ddlMes.Items.FindByValue(lstrmesactual).Selected = True
        txtAnio.Text = CStr(Year(Now))
    End Sub
    Protected Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click

        ListarDetalle(ddlPlanilla.SelectedValue, txtAnio.Text, ddlMes.SelectedValue)
    End Sub

    Private Sub ListarDetalle(txtPlanilla As String, txtanio As Integer, txtMes As Integer)
        Dim strURL As String
        Dim strPath As String
        Dim strScript As String



        'strPath = "%2fNM_Reportes%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")
        strURL = strURL + strPath

        Select Case txtPlanilla
            Case "EMP", "PRA"

                strURL = strURL + "pla_distribucion_planilla_EMP"
                strURL = strURL & "&CO_EMPR=" & Session("@EMPRESA")
                strURL = strURL & "&CO_PLAN=" & txtPlanilla
                strURL = strURL & "&NU_ANNO=" & txtanio
                strURL = strURL & "&NU_PERI=" & txtMes

            Case "OBM"


                strURL = strURL + "pla_distribucion_planilla_OBM"
                strURL = strURL & "&CO_EMPR=" & Session("@EMPRESA")
                strURL = strURL & "&CO_PLAN=" & txtPlanilla
                strURL = strURL & "&NU_ANNO=" & txtanio
                strURL = strURL & "&NU_PERI=" & txtMes

        End Select

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)


    End Sub




End Class
