Imports NuevoMundo
Imports System
Imports System.Data
Imports System.Web
Public Class frm_Listado_Pagos_OrdenServicio_V2
    Inherits System.Web.UI.Page
    Dim strOS As String
    Private Sub frm_Listado_Pagos_OrdenServicio_V2_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DGAMARRA"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtSerie.Text = "0002"
            btnBuscar.Attributes.Add("onclick", "FormatearBusqDoc(2);")
            txtSerie.Attributes.Add("onfocus", "javascript: this.select();")
            txtNumOrden.Attributes.Add("onblur", "FormatearBusqDoc(2);")
            txtNumOrden.Attributes.Add("onfocus", "javascript: this.select();")
        Else
            If txtNumOrden.Text <> "" And hdnPopUp.Value = "1" Then
                CargarGrilla(txtNumOrden.Text, Session("@EMPRESA"))
            End If
        End If
        hdnPopUp.Value = "0"
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        If txtNumOrden.Text Is Nothing Or txtNumOrden.Text = "" Then
            lblMsg.Text = "Debe Ingresar el número de orden de servicio para realizar la busqueda"
            CargarGrilla(Nothing, Nothing)
        Else
            CargarGrilla(txtNumOrden.Text, Session("@EMPRESA"))
        End If
    End Sub
    Protected Sub CargarGrilla(ByVal txtNumOrden As String, ByVal Empresa As String)
        strOS = txtSerie.Text + "-" + txtNumOrden
        If (txtNumOrden Is Nothing And Empresa Is Nothing) Then
            OSGrid.DataSource = Nothing
            OSGrid.DataBind()
            lblMsg.Text = "No hay datos encontrados"
            txtMontoT.Text = ""
            TextSum.Text = ""
            Session("dtDatos1") = Nothing
            'btnAceptar.Visible = True
        Else
            Dim lobjListarPagoOS As New clsEvaluar
            Dim dt As DataTable
            dt = lobjListarPagoOS.ListarPagoOS(txtSerie.Text + "-" + txtNumOrden, Empresa)
            If (dt.Rows.Count() > 0) Then
                OSGrid.DataSource = dt
                OSGrid.DataBind()

                txtMontoT.Text = Format(CType(dt.Rows(0).Item("Mont_Total"), Decimal), "#,###.00")
                Session("dtDatos1") = dt
                Session("NumOS") = txtSerie.Text + "-" + txtNumOrden
                lblMsg.Text = ""

                TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", String.Empty), "#,###.00")

            Else
                'btnAceptar.Visible = True
                OSGrid.DataSource = Nothing
                OSGrid.DataBind()
                lblMsg.Text = "No hay datos encontrados"
                txtMontoT.Text = ""
                TextSum.Text = ""
                Session("dtDatos1") = Nothing
            End If

        End If

    End Sub

    Private Sub OSGrid_DeleteCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.DeleteCommand
        Dim strItem As String
        strItem = CType(e.Item.FindControl("lblItem"), Label).Text
        Dim lobjOS As New clsEvaluar
        Dim dt As DataTable = lobjOS.EliminarMontoPorItem(txtSerie.Text + "-" + txtNumOrden.Text, strItem)
        CargarGrilla(txtNumOrden.Text, Session("@EMPRESA"))
    End Sub

    Private Sub OSGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.ItemCommand
        If e.CommandName = "AddNewRow" Then
            Dim txtMonto As TextBox = CType(e.Item.FindControl("txtMonto"), TextBox)
            If (txtMonto.Text Is Nothing Or txtMonto.Text = "") Then
                lblMsg.Text = "Ingrese un monto"
                Exit Sub
            Else
                Dim lobjOS As New clsEvaluar
                Dim resul As String = lobjOS.IngresarMonto(txtSerie.Text + "-" + txtNumOrden.Text, Double.Parse(txtMonto.Text), Session("@USUARIO"))
                If resul <> "" Then
                    lblMsg.Text = resul
                    ClientScript.RegisterStartupScript(Me.[GetType](), "MSG", "<script>alert('" & resul & "')</script>")
                Else
                    lblMsg.Text = ""
                End If
                CargarGrilla(txtNumOrden.Text, Session("@EMPRESA"))
            End If
        End If

    End Sub


    Private Sub OSGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles OSGrid.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
            Dim lnkDelete As LinkButton = CType(e.Item.FindControl("lnkDelete"), LinkButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            lnkEdit.Attributes.Add("onclick", "btnSeleccion_Edit('" & strOS & "','" & drvDatos("Item") & "','" & drvDatos("Monto") & "')")
            Dim drvPagos As DataRowView = CType(e.Item.DataItem, DataRowView)
            If (drvPagos("Estado").ToString() = "S") Then
                lnkEdit.Visible = False
                lnkDelete.Visible = False
            End If
            txtNumOrden.Attributes.Add("onBlur", "fValidaNum(this)")
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            Dim txtMonto As TextBox = CType(e.Item.FindControl("txtMonto"), TextBox)

            txtMonto.Attributes.Add("onBlur", "fValidaNum(this)")
            txtNumOrden.Attributes.Add("onBlur", "fValidaNum(this)")

        End If
    End Sub
End Class