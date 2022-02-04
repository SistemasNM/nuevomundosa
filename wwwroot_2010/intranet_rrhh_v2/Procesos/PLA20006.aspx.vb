Public Class PLA20006
    Inherits System.Web.UI.Page
    'Private objX As New OFISIS.OFIPLAN.CTS("01", "BENITO")
    'Private objX As New OFISIS.OFIPLAN.CTS(Session("@EMPRESA"), Session("@USUARIO"))
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtfechaini As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfechafin As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPlanilla As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCalcular As System.Web.UI.WebControls.Button
    Protected WithEvents txtano As System.Web.UI.WebControls.TextBox

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
    If Page.IsPostBack = False Then
      txtfechaini.Attributes.Add("readonly", "readonly")
      txtfechafin.Attributes.Add("readonly", "readonly")
    End If

  End Sub

    Private Sub btnCalcular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalcular.Click
        Dim lobjVac As OFISIS.OFIPLAN.Vacaciones
        Dim lstrFechaIni As String
        Dim lstrFechaFin As String
        Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
        lobjVac = New OFISIS.OFIPLAN.Vacaciones(Session("@EMPRESA"), Session("@USUARIO"))
        lstrFechaIni = Format(lobjUtil.TextoAFecha(txtfechaini.Text, NuevoMundo.Generales.enuTiposFormatoCadena.ddMMyyyy_Slash), "yyyyMMdd")
        lstrFechaFin = Format(lobjUtil.TextoAFecha(txtfechafin.Text, NuevoMundo.Generales.enuTiposFormatoCadena.ddMMyyyy_Slash), "yyyyMMdd")
        If lobjVac.CalculaAdelantoVacaciones(txtano.Text, ddlMes.SelectedValue, ddlPlanilla.SelectedValue, lstrFechaIni, lstrFechaFin) Then
            'If lobjCTS.CancelaPeriodo(xAnno, xMes, xMoneda, xBanco) Then
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('Se cálculo satisfactriamente.');</Script>")

    Else
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('No se realizo la operacion');</Script>")
        End If
    End Sub


End Class
