Public Class frmBuscarJefatura
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim lobj As New NM_General.NM_Logistica
        Dim ldtbDatos As DataTable

        ldtbDatos = lobj.ufn_BuscarJefaturaSolicitante(Me.txtCodigo.Text, Me.txtNombre.Text)
        dgDatos.DataSource = ldtbDatos
        dgDatos.DataBind()
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("CO_APRO") & "', '" & drvDatos("DE_APRO") & "')")
        End If
    End Sub
End Class