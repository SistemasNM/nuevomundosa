Public Class PLA30010_Det
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
  End Sub
    Protected WithEvents btnVer As System.Web.UI.WebControls.Button
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAnio As System.Web.UI.WebControls.TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------

    Session("@EMPRESA") = "01"
    'Session("@USUARIO") = "EPOMA"

    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
      Response.Redirect("../../intranet/finsesion.htm")
    End If

    '-----------------------------------------------------------------------
    '--FINAL: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    InitializeComponent()

  End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If Not Page.IsPostBack Then
      Marcar()
    End If
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


    Dim strChar As String = ""
    Dim lstrUsuarioIntrasolution As String = ""
    Dim lstrCadena As String = ""

    lstrUsuarioIntrasolution = Session("@Usuario")

    If lstrUsuarioIntrasolution.Trim.Length > 0 Then

      Dim sFecha As String = ""
      Dim sDate As String = ""
      sFecha = Format(Now, "yyyyMMdd HH:mm:ss")

      For idx = 1 To Len(sFecha)
        sDate = sDate & "*" & ((Asc(Mid(sFecha, idx, 1)) * 4) + 2)
      Next

      lstrCadena = lstrUsuarioIntrasolution & "@@@" & sDate
    Else
      Response.Redirect("../../intranet/finsesion.htm", True)
    End If


    Dim lstrURL As String
    Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
    lobjUtil = Nothing
    lstrURL = "../CrystalReports/_BoletaPago.asp?strAnio=" + txtAnio.Text + "&strMes=" + ddlMes.SelectedValue + "&strUsuario=" + lstrCadena
    ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")

  End Sub

End Class
