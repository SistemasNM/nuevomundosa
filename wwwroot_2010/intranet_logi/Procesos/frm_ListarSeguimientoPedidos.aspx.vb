Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ListarSeguimientoPedidos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgSeguimiento As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnCodigo As System.Web.UI.HtmlControls.HtmlInputHidden
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
        '    Response.Redirect("/intranet/finsesion_popup.htm", True)
        'End If
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            If (Not Request.Item("intCodigoPedido") Is Nothing) Then hdnCodigo.Value = Request.Item("intCodigoPedido")
            fnc_ListarSeguimientoPedido()
        End If
    End Sub

  Private Sub fnc_ListarSeguimientoPedido()
    Dim objPedido As New Logistica.clsPedidos
    Dim ldtbSeguimiento As New DataTable
    Try
      objPedido.NumPedido = hdnCodigo.Value
      objPedido.fnc_ListarSeguimientoPedido(ldtbSeguimiento)
      dgSeguimiento.DataSource = ldtbSeguimiento
      dgSeguimiento.DataBind()
    Catch ex As Exception
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó listar los seguimientos correctamente.');</script>")
    End Try
  End Sub

End Class
