Public Class PLA30008
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnexportar As System.Web.UI.WebControls.Button
    Protected WithEvents ddlEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtanio As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label

    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    Session("@EMPRESA") = "01"
    Session("@USUARIO") = "ATORRESC"
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If
            ' Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "ATORRESC"

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

    Private Sub btnexportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportar.Click
        Dim lstrPeriodo As String
        Dim lstrEstado As String
        Dim lstrURL As String
        Dim lstrAnno As String
        lstrAnno = Trim(txtanio.Text)
        lstrPeriodo = ddlMes.SelectedValue
        lstrEstado = ddlEstado.SelectedValue
        If lstrAnno.Length = 0 Then
            lblMensaje.Text = "Ingrese periodo a consultar"
        Else
            lblMensaje.Text = ""
            lstrURL = "PLA30008_EXP.aspx?stranio=" + lstrAnno + "&strMes=" + ddlMes.SelectedValue + "&strEstado=" + ddlEstado.SelectedValue
            ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class
