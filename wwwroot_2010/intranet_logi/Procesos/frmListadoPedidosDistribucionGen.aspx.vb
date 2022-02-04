Imports NuevoMundo

Public Class frmListadoPedidosDistribucionGen
    Inherits System.Web.UI.Page

    Sub fnListadoPedidosDistribucion(ByVal pstrCodArt As String)
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtResponse As DataTable

        dtResponse = objPedidos.fnListadoArticulosDistribG(pstrCodArt)

        If dtResponse.Rows.Count = 0 Then
            btnAprobar.Visible = False
        End If

        grdListArtDistG.DataSource = dtResponse
        grdListArtDistG.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fnListadoPedidosDistribucion("")
        End If
    End Sub

    Protected Sub imgBEliminar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim imgBEliminar As ImageButton = CType(sender.FindControl("imgBEliminar"), ImageButton)
        Dim strDatos() As String = imgBEliminar.CommandArgument.Split("|")
        Dim strNuPedi As String = strDatos(0)
        Dim strCoItem As String = strDatos(1)

        Dim objPedidos As New Logistica.clsPedidos
        Dim dtResponse As DataTable

        dtResponse = objPedidos.fnEliminarPedArticulosDistribG(strNuPedi, strCoItem)

        fnListadoPedidosDistribucion("")

    End Sub
End Class