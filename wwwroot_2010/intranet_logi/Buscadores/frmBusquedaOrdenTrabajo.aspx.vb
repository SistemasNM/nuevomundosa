Imports NM_General

Public Class frmBusquedaOrdenTrabajo
    Inherits System.Web.UI.Page

    Private Sub frmBusquedaOrdenTrabajo_Init(sender As Object, e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("Param1") = Request.QueryString("Param1")
            Call LlenarComboCentroCostos()
            Call BuscarConsulta()
        End If
        lblMensajeError.Text = ""
    End Sub


    Private Sub LlenarComboCentroCostos()
        Dim objLogistica As New NM_Logistica
        Dim dtbDatos As DataTable
        Try
            dtbDatos = objLogistica.ufn_Listado_CentroCostos_OrdenTrabajo(ViewState("Param1").ToString)

            If (dtbDatos.Rows.Count > 0) Then
                ddlCentroCosto.DataTextField = "DESCRIPCION"
                ddlCentroCosto.DataValueField = "CODIGO"
                ddlCentroCosto.DataSource = dtbDatos
                ddlCentroCosto.DataBind()
            End If

        Catch ex As Exception
            lblMensajeError.Text = ex.Message
        Finally
            objLogistica = Nothing
            dtbDatos = Nothing
        End Try
    End Sub

    Private Sub BuscarConsulta()
        Dim objLogistica As New NM_Logistica
        Dim dtbDatos As DataTable
        Dim strCodigo As String
        Dim strDescripcion As String
        Dim strCentroCostos As String

        Try
            strCodigo = txtCodigo.Text
            strDescripcion = txtDescripcion.Text
            strCentroCostos = ddlCentroCosto.SelectedValue.Trim
            dtbDatos = Nothing

            dtbDatos = objLogistica.ufn_BusquedaOrdenTrabajo_V2(ViewState("Param1").ToString, strCentroCostos, strCodigo, strDescripcion, ddlAno.SelectedValue)

            If (dtbDatos.Rows.Count > 0) Then                
                dgGrillaEstandar.DataSource = dtbDatos
                dgGrillaEstandar.DataBind()
            Else
                dgGrillaEstandar.DataSource = Nothing
                dgGrillaEstandar.DataBind()
                lblMensajeError.Text = "No se encontraron datos"
            End If

        Catch ex As Exception
            lblMensajeError.Text = ex.Message
        Finally
            objLogistica = Nothing
            dtbDatos = Nothing
        End Try

    End Sub

#Region "Eventos"

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Call BuscarConsulta()
    End Sub

    Private Sub dgGrillaEstandar_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgGrillaEstandar.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("CODIGO") & "', '" & Replace(drvDatos("DESCRIPCION"), "'", "\'").ToString & "')")
        End If
    End Sub




#End Region

    Private Sub ddlCentroCosto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCentroCosto.SelectedIndexChanged
        Call BuscarConsulta()
    End Sub

    Private Sub ddlAno_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAno.SelectedIndexChanged
        Call BuscarConsulta()
    End Sub
End Class