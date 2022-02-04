Public Class frmConsPresupuestos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
  Protected WithEvents grdData As System.Web.UI.WebControls.DataGrid
  Protected WithEvents frmData As System.Web.UI.HtmlControls.HtmlForm
  Protected WithEvents cboAnno As System.Web.UI.WebControls.DropDownList
  Protected WithEvents cboMes As System.Web.UI.WebControls.DropDownList
  Protected WithEvents txtCentroCostoCodigo As System.Web.UI.WebControls.TextBox
  Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
  Protected WithEvents txtCentroCostoNombre As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
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
        If Not Page.IsPostBack Then
            '20120918 EPM - Readonly
            txtCentroCostoCodigo.Attributes.Add("readonly", "readonly")
            txtCentroCostoNombre.Attributes.Add("readonly", "readonly")
        End If

    btnBuscar.Attributes.Add("onClick", "javascript:return fValidarBus();")
  End Sub

  Private Sub rCargarConsulta()
    Dim lobjPre As OFISIS.OFIPRES.Presupuesto
    Dim objDtb As DataTable
    Dim sCCosto As String
    Dim sNumAno As String
    Dim sNumMes As String

    sCCosto = txtCentroCostoCodigo.Text
    sNumAno = cboAnno.SelectedValue
    sNumMes = cboMes.SelectedValue


    lobjPre = New OFISIS.OFIPRES.Presupuesto
    objDtb = lobjPre.ListaGastoMensual(Session("@EMPRESA"), "1", sNumAno, sNumMes, "K", sCCosto, "SOL", "R", Session("@USUARIO"))

    grdData.DataSource = objDtb
    grdData.DataBind()

  End Sub

  Private Sub grdData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdData.ItemDataBound
    Select Case e.Item.ItemType
      Case ListItemType.AlternatingItem, ListItemType.Item

        Dim sCuenta As String
        Dim sCCosto As String
        Dim sNumAno As String
        Dim sNumMes As String

        sCuenta = CType(e.Item.DataItem("CO_CNTA"), String)
        sCCosto = txtCentroCostoCodigo.Text
        sNumAno = cboAnno.SelectedValue
        sNumMes = cboMes.SelectedValue

        If Len(sCuenta) = 7 Then
          CType(e.Item.FindControl("btnVer"), ImageButton).Attributes.Add("onClick", "javascript:return VerReporte('" + sCuenta + "','" + sCCosto + "','" + sNumAno + "','" + sNumMes + "')")
        Else
          CType(e.Item.FindControl("btnVer"), ImageButton).Visible = False
        End If


    End Select

  End Sub

  Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
    rCargarConsulta()
  End Sub
End Class
