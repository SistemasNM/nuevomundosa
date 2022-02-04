Imports OFISIS.OFILOGI
Public Class frm_AprobacionMuestras
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnaprobarmasivo As System.Web.UI.WebControls.Button
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents dtgLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblCantidad As System.Web.UI.WebControls.Label
    Protected WithEvents tblFil1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblFil2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblBotonera1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents hdn1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnaprobarmasivo As System.Web.UI.HtmlControls.HtmlInputHidden

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
      'Session("@USUARIO") = "CPONCE"

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
    Me.tblFil1.Visible = False
    Me.tblFil2.Visible = False

    btnConsultar.Text = "Actualizar"

    If Not Page.IsPostBack Then
      hdnaprobarmasivo.Value = ""
      btnConsultar_Click(Nothing, Nothing)
    End If
  End Sub

  Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
    Call prc_listar()
  End Sub
#Region "-- Metodos --"
  Public Sub prc_listar()
    Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
    Dim pDT As New DataTable
    lobjMuestrasTela.Usuario = CType(Session("@USUARIO"), String)
    lobjMuestrasTela.ListarAprobacion_SolicitudMuestras(pDT)
    dtgLista.DataSource = pDT
    dtgLista.DataBind()
    lblCantidad.Text = Format(dtgLista.Items.Count, "#,##0")
  End Sub

#End Region

  Private Sub dtgLista_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemCreated
    If e.Item.ItemIndex <> -1 Then
      Dim btnBuscar As ImageButton
      btnBuscar = CType(e.Item.FindControl("ibtBuscar"), ImageButton)
      If Not btnBuscar Is Nothing Then
        AddHandler btnBuscar.Click, AddressOf btnBuscar_Click
      End If
    End If

  End Sub

  Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Call prc_listar()
  End Sub

  Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
    Select Case e.Item.ItemType
      Case ListItemType.Item, ListItemType.AlternatingItem
        Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtBuscar"), ImageButton)
        Dim lobjaprobar As CheckBox = CType(e.Item.FindControl("chkaprobari"), CheckBox)
        lobjBoton.Attributes.Add("onClick", "javascript:return VerDetalle('" + ldrvVista("var_NumeroSolicitud") + "')")
        lobjBoton = CType(e.Item.FindControl("ibtHistorial"), ImageButton)
        lobjBoton.Attributes.Add("onClick", "VerHistorial('" + ldrvVista("var_NumeroSolicitud") + "')")
        lobjaprobar.Attributes.Add("onClick", "fnc_aprobarmasivo('" + e.Item.ClientID + "','" + ldrvVista("var_NumeroSolicitud") + "')")
        lobjBoton = Nothing
        lobjaprobar = Nothing
        ldrvVista = Nothing
    End Select
  End Sub

    
End Class
