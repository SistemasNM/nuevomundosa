Public Class PLA30001
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtFechaInicial As System.Web.UI.WebControls.TextBox
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        'Put user code to initialize the page here
        If Not IsPostBack Then
            txtFechaInicial.Attributes.Add("readonly", "readonly")
            LlenarFechas()
        End If
    End Sub

  Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click

    Dim lstrFecha As String
    Dim lstrURL As String
    Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
    lstrFecha = Format(lobjUtil.TextoAFecha(txtFechaInicial.Text, NuevoMundo.Generales.enuTiposFormatoCadena.ddMMyyyy_Slash), "yyyyMMdd")
    lobjUtil = Nothing
    lstrURL = "../CrystalReports/_MovimientosPersonal.asp?strFecha=" + lstrFecha + "&strEmpresa=" + Session("@EMPRESA")
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")

  End Sub

    Private Sub LlenarFechas()
        txtFechaInicial.Text = Format(Now, "dd") + "/" + Format(Now, "MM") + "/" + Format(Now, "yyyy")
    End Sub
End Class
