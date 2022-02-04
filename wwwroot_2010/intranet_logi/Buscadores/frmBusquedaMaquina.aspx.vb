Imports NM_General

Public Class frmBusquedaMaquina
    Inherits System.Web.UI.Page

    Private Sub BuscarConsulta()
        Dim lobjGerencia As New clsGerencia
        Dim dtbDatos As DataTable
        Dim strCodigo As String
        Dim strDescripcion As String
        'Dim strCentroCostos As String

        Try
            strCodigo = txtCodigo.Text
            strDescripcion = txtDescripcion.Text
            'strCentroCostos = ddlCentroCosto.SelectedValue.Trim
            dtbDatos = Nothing

            dtbDatos = lobjGerencia.fn_CargarListarAdmi("MAQUINA", strCodigo, strDescripcion, "")

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuscarConsulta()
        End If
    End Sub

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

End Class