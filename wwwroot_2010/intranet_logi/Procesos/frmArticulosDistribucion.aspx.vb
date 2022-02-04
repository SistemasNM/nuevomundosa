Imports NuevoMundo

Public Class frmArticulosDistribucion
    Inherits System.Web.UI.Page

    Sub fn_cargarListas(ByVal pstrCodArt As String)

        Dim objPedidos As New Logistica.clsPedidos
        Dim dsResponse As DataSet

        dsResponse = objPedidos.fnBuscarArticulosDistribAsociados(pstrCodArt)

        lstLeft.DataValueField = "CODIGO"
        lstLeft.DataTextField = "VALUE"
        lstLeft.DataSource = dsResponse.Tables(0)
        lstLeft.DataBind()

        lstRight.DataValueField = "CODIGO"
        lstRight.DataTextField = "VALUE"
        lstRight.DataSource = dsResponse.Tables(1)
        lstRight.DataBind()


    End Sub

    Private Sub frmArticulosDistribucion_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        'Session("@USUARIO") = "OBLAS"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fn_cargarListas("W")
        End If
    End Sub

    Protected Sub LeftClick(ByVal sender As Object, ByVal e As EventArgs)
        'List will hold items to be removed.
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtResponse As DataTable
        Dim removedItems As List(Of ListItem) = New List(Of ListItem)

        'Loop and transfer the Items to Destination ListBox.
        For Each item As ListItem In lstRight.Items
            If item.Selected Then
                item.Selected = False
                'lstLeft.Items.Add(item)
                'removedItems.Add(item)
                dtResponse = objPedidos.fnAccionesArticulosDistribAsociados("E", txtArtSAsoc.Text.Trim(), item.Value, Session("@USUARIO").ToString())
            End If
        Next

        fn_cargarListas(txtArtSAsoc.Text.Trim())

        'Loop and remove the Items from the Source ListBox.
        'For Each item As ListItem In removedItems
        'lstRight.Items.Remove(item)
        'Next
    End Sub

    Protected Sub RightClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRight.Click
        'List will hold items to be removed.
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtResponse As DataTable
        Dim removedItems As List(Of ListItem) = New List(Of ListItem)

        'Loop and transfer the Items to Destination ListBox.
        For Each item As ListItem In lstLeft.Items
            If item.Selected Then
                item.Selected = False
                'lstRight.Items.Add(item)
                'removedItems.Add(item)
                dtResponse = objPedidos.fnAccionesArticulosDistribAsociados("I", txtArtSAsoc.Text.Trim(), item.Value, Session("@USUARIO").ToString())
            End If
        Next

        fn_cargarListas(txtArtSAsoc.Text.Trim())

        'Loop and remove the Items from the Source ListBox.
        'For Each item As ListItem In removedItems
        'lstLeft.Items.Remove(item)
        'Next
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        fn_cargarListas(txtArtSAsoc.Text.Trim())
    End Sub

    Private Sub lstRight_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstRight.SelectedIndexChanged
        Dim value As Object = lstRight.SelectedValue
        lblCodigoArticulo.Text = value.ToString()
    End Sub
End Class