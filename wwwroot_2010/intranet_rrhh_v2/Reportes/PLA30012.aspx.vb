Public Class PLA30012
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnFoto As System.Web.UI.WebControls.Button
    Protected WithEvents btnVer As System.Web.UI.WebControls.Button
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents btnBoleta As System.Web.UI.WebControls.Button
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Session("@USUARIO") = "BENITO"
        If Not IsPostBack Then
        End If
    End Sub
    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Dim lstrURL As String
        Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
        lobjUtil = Nothing
    lstrURL = "../CrystalReports/_CtaCteTrabajadores.asp?strEmpresa=01" + "&strCodigo=" + txtCodigo.Text
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
  End Sub

  Private Sub btnFoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFoto.Click
    Dim Ruta As String
    Ruta = "\\" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "\c$\Intrasolution\IS\Images\Fotos\" + txtCodigo.Text + ".jpg"
    Image1.ImageUrl = Ruta
  End Sub
  Private Sub btnBoleta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBoleta.Click
    Dim lstrURL As String
    lstrURL = "../CrystalReports/_BoletaPagoEmpresa.asp?strEmpresa=01" + "&strCodigo=" + txtCodigo.Text + "&strUsuario=" + Session("@Usuario")
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")

  End Sub
End Class
