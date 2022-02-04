Public Class PLA30003
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
      'Session("@USUARIO") = "EPOMA"

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

  Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click
    Dim lstrURL As String
    Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
    lobjUtil = Nothing
    lstrURL = "../CrystalReports/_DetalleAportesSCTR.asp?strEmpresa=01" + " &strPlanilla=" + ddlPlanilla.SelectedValue + " &strAnio=" + txtAnio.Text + " &strMes=" + ddlMes.SelectedValue
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
  End Sub

  Private Sub btnexportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportar.Click
    Dim lstrURL As String
    lstrURL = "PLA30003_EXP.aspx?strEmpresa=01" + " &strPlanilla=" + ddlPlanilla.SelectedValue + " &strAnio=" + txtAnio.Text + " &strMes=" + ddlMes.SelectedValue
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")

  End Sub
End Class
