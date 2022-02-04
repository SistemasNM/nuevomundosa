Public Class PLA30006
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPlanilla As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnVer As System.Web.UI.WebControls.Button
    Protected WithEvents Txtano As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbtRepBol As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtRepRes As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Txtanno As System.Web.UI.WebControls.TextBox

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
        'Put user code to initialize the page here
    End Sub

    Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click
        Dim lstrURL As String
        If rbtRepBol.Checked = True Then
      lstrURL = "../CrystalReports/_AdelantoVacBoleta.asp?strEmpresa=" + Session("@EMPRESA") + "&strAnno=" + Txtanno.Text + "&strMes=" + ddlMes.SelectedValue + "&strPlan=" + ddlPlanilla.SelectedValue + "&strProc=01"
      ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    ElseIf rbtRepRes.Checked = True Then
      lstrURL = "../CrystalReports/_AdelantoVacResumen.asp?strEmpresa=" + Session("@EMPRESA") + "&strAnno=" + Txtanno.Text + "&strMes=" + ddlMes.SelectedValue + "&strPlan=" + ddlPlanilla.SelectedValue + "&strProc=01"
      ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
        End If

    End Sub


   
End Class
