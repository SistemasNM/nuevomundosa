Public Class PLA30011
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAnio As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnVer As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPlanilla As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCCosto As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnImprimir As System.Web.UI.WebControls.Button
    Protected WithEvents txtCorrelativo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodInicio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservacion As System.Web.UI.WebControls.TextBox

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
        ddlMes.SelectedItem.Selected = False
        ddlMes.Items.FindByValue(CStr(Month(Now))).Selected = True
        txtAnio.Text = CStr(Year(Now))
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
    Dim lstrFecha As String = ""
        Dim lstrURL As String
        Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
        lobjUtil = Nothing
    lstrURL = "../CrystalReports/Rpt_BoletadePago.asp?strEmpresa=01" + "&strPlanilla=" + ddlPlanilla.SelectedValue + "&strAnio=" + txtAnio.Text + "&strMes=" + ddlMes.SelectedValue + "&strCorrelativo=" + txtCorrelativo.Text + "&strCCosto=" + ddlCCosto.SelectedValue + "&strCodInicio=" + txtCodInicio.Text + "&strCodFin=" + txtCodFin.Text + "&strObservacion=" + txtObservacion.Text
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub
End Class
