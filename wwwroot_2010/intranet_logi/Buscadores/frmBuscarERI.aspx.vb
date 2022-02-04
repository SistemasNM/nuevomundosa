Public Class frmBuscarERI
    Inherits System.Web.UI.Page

    Dim strEmpresa As String
    Dim strUnidad As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strEmpresa = Session("@EMPRESA")
        strUnidad = Request.QueryString("pCodU")

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub

    Private Sub BindGrid()
        'Dim lobj As New NM_General.NM_Logistica
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim ldtbDatos As DataTable

        ldtbDatos = objArticulo.ufn_BuscarListaERI(Me.txtCodigo.Text, IIf(rdbAbierto.Checked, "ABI", "CER"), strEmpresa, strUnidad)
        dgDatos.DataSource = ldtbDatos
        dgDatos.DataBind()

        objArticulo = Nothing
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("vch_CodigoInventario") & "', '" & drvDatos("vch_CodigoAlmacen") & "')")
        End If
    End Sub
End Class