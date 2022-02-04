Imports Almacen

Public Class frm_ClientesVendedor
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents dgDatos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNombre As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Eventos --"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("CO_CLIE") & "', '" & Replace(drvDatos("NO_CLIE"), "'", "\'").ToString & "','" & drvDatos("CO_VEND") & "','" & Replace(drvDatos("NO_VEND"), "'", "\'").ToString & "')")
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If txtCodigo.Text <> "" Or txtNombre.Text <> "" Then
            BindGrid()
        End If
    End Sub

#End Region

#Region "-- Metodos --"

    Private Sub BindGrid()
        Dim objVentas As New Ventas
        Dim dtbDatos As DataTable = objVentas.ufn_ObtenerClientesxTipoConsulta(2, Me.txtCodigo.Text, Me.txtNombre.Text, "", "")
        dgDatos.DataSource = dtbDatos
        dgDatos.DataBind()
    End Sub

#End Region

End Class
