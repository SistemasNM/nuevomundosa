Imports NuevoMundo
Public Class frm_Reporte_Pedidos_Ventas
    Inherits System.Web.UI.Page

    Private Sub frm_Reporte_Pedidos_Ventas_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DGAMARRA"
        'Session("@USUARIO") = "OBLAS"

        '--INICIO: VERIFICAR LA SESION
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

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim objPedidos As New Logistica.clsPedidos
        Dim dts As DataSet
        txtNumero.Text = txtNumero.Text.PadLeft(10, "0")
        dts = objPedidos.BuscarPedidoCliente(ddserie.SelectedValue, txtNumero.Text)
        If dts.Tables(0).Rows.Count > 0 Then
            lblCliente.Text = dts.Tables(0).Rows(0).Item("NO_CLIE")
            'lblPorc.Text = Format(dts.Tables(0).Rows(0).Item("Porc_linea") * 100, "##.##")
            lblPorc.Text = dts.Tables(0).Rows(0).Item("Porc_linea").ToString() + " %"
            lblLinea.Text = Format(dts.Tables(0).Rows(0).Item("num_LineaCredito"), "##,##0.00")
            lblstatus.Text = dts.Tables(0).Rows(0).Item("estado_pedido")
            lblstatus.ForeColor = Drawing.Color.Black
            lblLugar.Text = dts.Tables(0).Rows(0).Item("DE_DIRE_DESP")
            lblFechaEntrega.Text = IIf(dts.Tables(0).Rows(0).Item("FE_ENTR").ToString() = "", "", dts.Tables(0).Rows(0).Item("FE_ENTR"))
            grdReporVenta.DataSource = dts.Tables(1)
            grdReporVenta.DataBind()
            grdSalidaPedido.DataSource = dts.Tables(2)
            grdSalidaPedido.DataBind()
        Else
            lblCliente.Text = ""
            lblPorc.Text = ""
            lblLinea.Text = ""
            lblstatus.Text = ""
            lblLugar.Text = ""
            lblFechaEntrega.Text = ""
            grdReporVenta.DataSource = Nothing
            grdReporVenta.DataBind()
            grdSalidaPedido.DataSource = Nothing
            grdSalidaPedido.DataBind()
        End If
       
    End Sub
End Class