Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frmAlmacenes_2
    Inherits System.Web.UI.Page

    Dim strCodAlmacen, strDesAlmacen As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtbDatos As New DataTable
        ldtbDatos = Nothing
        Try
            ldtbDatos = fncListarAlmacenes()
            dgDatos.DataSource = ldtbDatos
            dgDatos.DataBind()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function fncListarAlmacenes() As DataTable
        Try
            Return New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet("usp_LOG_DiasStockxArticulosFiltros").Tables(1)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("co_alma") & "', '" & drvDatos("des_alma") & "')")
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub
End Class