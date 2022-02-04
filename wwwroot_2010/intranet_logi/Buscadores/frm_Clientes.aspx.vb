Public Class frm_Clientes
    Inherits System.Web.UI.Page
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub
    'Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    'Protected WithEvents dgDatos As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtNombre As System.Web.UI.WebControls.TextBox

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("var_CodigoCliente") & "', '" & Replace(drvDatos("var_NombreCliente"), "'", "\'").ToString & "')")
        End If
    End Sub

    Private Sub BindGrid()
        Dim objVentas As New NM_General.NM_Logistica
        Dim dtbDatos As DataTable = objVentas.ufn_ObtenerCliente(Me.txtCodigo.Text, Me.txtNombre.Text)
        dgDatos.DataSource = dtbDatos
        dgDatos.DataBind()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If txtCodigo.Text <> "" Or txtNombre.Text <> "" Then
            BindGrid()
        End If
    End Sub

End Class