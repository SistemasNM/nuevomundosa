Public Class PLA30002
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnVer As System.Web.UI.WebControls.Button
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAnio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfechamov As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnexportar As System.Web.UI.WebControls.Button

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
        'Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "BENITO"
        If Not IsPostBack Then
            Marcar()
        End If
        txtfechamov.Attributes.Add("readonly", "readonly")
    End Sub
    Private Sub Marcar()
        ddlMes.SelectedItem.Selected = False
        ddlMes.Items.FindByValue(CStr(Month(Now))).Selected = True
        txtAnio.Text = CStr(Year(Now))
    End Sub
    Private Sub LlenarMeses()
        Dim litmItem As System.Web.UI.WebControls.ListItem
        With ddlMes
            litmItem = New ListItem
            .Items.Clear()
            litmItem.Value = 1
            litmItem.Value = "ENERO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 2
            litmItem.Value = "FEBRERO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 3
            litmItem.Value = "MARZO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 4
            litmItem.Value = "ABRIL"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 5
            litmItem.Value = "MAYO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 6
            litmItem.Value = "JUNIO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 7
            litmItem.Value = "JULIO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 8
            litmItem.Value = "AGOSTO"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 9
            litmItem.Value = "SEPTIEMBRE"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 10
            litmItem.Value = "OCTUBRE"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 11
            litmItem.Value = "NOVIEMBRE"
            litmItem.Selected = False
            .Items.Add(litmItem)
            litmItem.Value = 12
            litmItem.Value = "DICIEMBRE"
            litmItem.Selected = False
            .Items.Add(litmItem)
        End With
        
    End Sub


  Private Sub btnVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVer.Click
    Dim lstrFecha As String
    Dim lstrURL As String
    Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
    lstrFecha = Format(lobjUtil.TextoAFecha(txtfechamov.Text, NuevoMundo.Generales.enuTiposFormatoCadena.ddMMyyyy_Slash), "yyyyMMdd")
    lobjUtil = Nothing
    lstrURL = "../CrystalReports/_ResumenXArea.asp?strEmpresa=" + Session("@EMPRESA") + "&strAnio=" + txtAnio.Text + "&strMes=" + ddlMes.SelectedValue + "&strFecha=" + lstrFecha
    'lstrURL = "../CrystalReports/_ResumenXArea.asp?strEmpresa=01" + "&strAnio=" + txtAnio.Text + "&strMes= 10" + "&strFecha=" + lstrFecha
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    'lstrURL = "../CrystalReports/_ResumenXArea.asp?strEmpresa=01" + "&strAnio=2006" + "&strmes=2" + "&strFecha=20060402"
    'ClientScript.RegisterStartupScript(Me.[GetType](),"reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
  End Sub

  Private Sub LlenarFechas()
    txtfechamov.Text = Format(Now, "dd") + "/" + Format(Now, "MM") + "/" + Format(Now, "yyyy")
  End Sub
  Private Sub btnexportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportar.Click
    Dim lstrFecha As String
    Dim lstrURL As String
    Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
    lstrFecha = Format(lobjUtil.TextoAFecha(txtfechamov.Text, NuevoMundo.Generales.enuTiposFormatoCadena.ddMMyyyy_Slash), "yyyyMMdd")
    lobjUtil = Nothing
    lstrURL = "PLA30002_EXP.aspx?strAnio=" + txtAnio.Text + "&strMes=" + ddlMes.SelectedValue + "&strFecha=" + lstrFecha
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
  End Sub
End Class
