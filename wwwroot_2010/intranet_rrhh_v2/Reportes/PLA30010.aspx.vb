Public Class PLA30010
    Inherits System.Web.UI.Page



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

    Dim lstrUsuarioIntrasolution As String = ""

    If (Not Request("pstrUsuarioIntrasolution") Is Nothing) Or Request("pstrUsuarioIntrasolution") <> "" Then
      lstrUsuarioIntrasolution = Request("pstrUsuarioIntrasolution")
    End If

    If lstrUsuarioIntrasolution.Trim.Length > 0 Then
      Session("@USUARIO") = lstrUsuarioIntrasolution
    Else
      Response.Redirect("../../intranet/finsesion.htm", True)
    End If

    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------

    If (Session("@USUARIO") Is Nothing) Or Session("@USUARIO") = "" Then
      Response.Redirect("../../intranet/finsesion.htm", True)
    End If

    '-----------------------------------------------------------------------
    '--FINAL: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    InitializeComponent()
  End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Redirect("PLA30010_Det.aspx")
    End Sub


End Class