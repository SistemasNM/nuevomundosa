Imports NuevoMundo
Public Class frm_Status_Pedido_Por_Salida
    Inherits System.Web.UI.Page

    Private Sub frm_Status_Pedido_Por_Salida_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DRIOS" '"DGAMARRA"

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
        If Not IsPostBack Then
            CargarListadoAyudantes(ddlAyudante1)
            CargarListadoAyudantes(ddlAyudante2)
            CargarListadoAyudantes(ddlAyudante3)
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim objPedidos As New Logistica.clsPedidos
        Dim dt As DataTable
        dt = objPedidos.BuscarDatosSalida(txtSalida.Text)
        If dt.Rows.Count > 0 Then
            dgGuia.DataSource = dt
            dgGuia.DataBind()
            'grdReporVenta.DataSource = dt
            'grdReporVenta.DataBind()
            'ddlEstado.SelectedValue = dt.Rows(0).Item("vch_Estado").ToString
        Else
            ' grdReporVenta.DataSource = Nothing
            'grdReporVenta.DataBind()
            'ddlEstado.SelectedValue = ""
            dgGuia.DataSource = Nothing
            dgGuia.DataBind()
        End If
    End Sub

    'Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
    '    Dim objPedidos As New Logistica.clsPedidos
    '    Dim dt As DataTable
    '    objPedidos.GrabarStatusSalida(txtSalida.Text, ddlEstado.SelectedValue, Session("@USUARIO"))

    '    dt = objPedidos.BuscarDatosSalida(txtSalida.Text)
    '    If dt.Rows.Count > 0 Then
    '        'grdReporVenta.DataSource = dt
    '        'grdReporVenta.DataBind()
    '        'ddlEstado.SelectedValue = dt.Rows(0).Item("vch_Estado").ToString
    '    Else
    '        'grdReporVenta.DataSource = Nothing
    '        'grdReporVenta.DataBind()
    '        'ddlEstado.SelectedValue = ""
    '    End If
    '    'USP_GRABAR_STATUS_SALIDA
    'End Sub

    Private Sub dgGuia_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgGuia.ItemCommand
        If e.CommandName = "Grabar" Then
            Dim lblNroPedido As Label = CType(e.Item.FindControl("lblNroPedido"), Label)
            Dim lblNroGuia As Label = CType(e.Item.FindControl("lblNroGuia"), Label)
            Dim ddlEstado As DropDownList = CType(e.Item.FindControl("ddlEstado"), DropDownList)
            Dim lblSalida As Label = CType(e.Item.FindControl("lblSalida"), Label)
            Dim objPedidos As New Logistica.clsPedidos
            Dim strCodAyudante1 As String = ddlAyudante1.SelectedValue.ToString
            Dim strCodAyudante2 As String = ddlAyudante2.SelectedValue.ToString
            Dim strCodAyudante3 As String = ddlAyudante3.SelectedValue.ToString

            objPedidos.GrabarStatusGuiaSalida(lblSalida.Text, lblNroPedido.Text, lblNroGuia.Text, ddlEstado.SelectedValue, Session("@USUARIO"), strCodAyudante1, strCodAyudante2, strCodAyudante3)
            ScriptManager.RegisterClientScriptBlock(TryCast(source, Control), Me.GetType(), "alert", "alert('Grabado..!')", True)

            Dim dt As DataTable
            dt = objPedidos.BuscarDatosSalida(txtSalida.Text)
            If dt.Rows.Count > 0 Then
                dgGuia.DataSource = dt
                dgGuia.DataBind()
            Else
                dgGuia.DataSource = Nothing
                dgGuia.DataBind()
            End If
        End If
    End Sub

    Protected Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        Dim objPedidos As New Logistica.clsPedidos
        Dim dt As New DataTable
        Dim strCodAyudante1 As String = ddlAyudante1.SelectedValue.ToString
        Dim strCodAyudante2 As String = ddlAyudante2.SelectedValue.ToString
        Dim strCodAyudante3 As String = ddlAyudante3.SelectedValue.ToString
        Try
            If (ddlAyudante1.SelectedValue = ddlAyudante2.SelectedValue) And ddlAyudante1.SelectedValue <> "0" Then
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", "alert('Debe seleccionar diferentes ayudantes.')", True)
                ddlAyudante1.Focus()
                Exit Sub
            End If

            If (ddlAyudante2.SelectedValue = ddlAyudante3.SelectedValue) And ddlAyudante2.SelectedValue <> "0" Then
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", "alert('Debe seleccionar diferentes ayudantes.')", True)
                ddlAyudante1.Focus()
                Exit Sub
            End If

            If (ddlAyudante1.SelectedValue = ddlAyudante3.SelectedValue) And ddlAyudante1.SelectedValue <> "0" Then
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", "alert('Debe seleccionar diferentes ayudantes.')", True)
                ddlAyudante1.Focus()
                Exit Sub
            End If

            dt = objPedidos.BuscarDatosSalida(txtSalida.Text)
            If dt.Rows.Count() > 0 Then
                For index As Integer = 0 To dt.Rows.Count - 1
                    objPedidos.GrabarStatusGuiaSalida(txtSalida.Text, dt.Rows(index).Item("vch_NroPedido").ToString(), dt.Rows(index).Item("vch_NroGuia").ToString(), "ENT", Session("@USUARIO"), strCodAyudante1, strCodAyudante2, strCodAyudante3)
                Next
                dt = objPedidos.BuscarDatosSalida(txtSalida.Text)
                If dt.Rows.Count > 0 Then
                    dgGuia.DataSource = dt
                    dgGuia.DataBind()
                Else
                    dgGuia.DataSource = Nothing
                    dgGuia.DataBind()
                End If
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", "alert('Se Actualizó todo a salida entregada!')", True)
            End If

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", "alert('" & ex.Message.ToString & "')", True)
        Finally
            objPedidos = Nothing
            dt = Nothing
        End Try

        
    End Sub

    ''' <summary>
    ''' Carga Listado de Ayudantes
    ''' LUIS_AJ - 20210225
    ''' </summary>
    Sub CargarListadoAyudantes(ByRef ddlAyudante As DropDownList)

        Dim dtListado As New DataTable
        Dim objVentas As New Almacen.Ventas

        Try
            objVentas.ObtenerListadoAyudantes(Session("@USUARIO"), dtListado)

            ddlAyudante.DataTextField = "vch_NomTrabajador"
            ddlAyudante.DataValueField = "vch_CodTrabajador"
            ddlAyudante.DataSource = dtListado
            ddlAyudante.DataBind()
            ddlAyudante.Items.Insert(0, New ListItem("SIN AYUDANTE", "0"))

        Catch ex As Exception
        Finally
            objVentas = Nothing
            dtListado = Nothing
        End Try

    End Sub


End Class