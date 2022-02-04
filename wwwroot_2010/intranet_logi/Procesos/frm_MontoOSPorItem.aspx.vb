Imports NuevoMundo
Imports System
Imports System.Data
Imports System.Web
Public Class frm_MontoOSPorItem
    Inherits System.Web.UI.Page
    Dim strOrdenServicio As String
    Dim strItem As String
    Dim numMonto As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("@EMPRESA") = "01"
        strOrdenServicio = Request.QueryString("strNumeroOrdenServicio")
        strItem = Request.QueryString("strItem")
        numMonto = Request.QueryString("strMonto")
        If Page.IsPostBack = False Then
            'strOrdenServicio = Request.QueryString("strNumeroOrdenServicio")
            'strItem = Request.QueryString("strItem")
            'numMonto = Request.QueryString("strMonto")
            CargarDatosCabecera(strOrdenServicio)
            CargarDetallePagosPorItem(strOrdenServicio, strItem)
        End If
    End Sub
    Public Sub CargarDatosCabecera(ByVal strNumeroOrdenServicio As String)
        Dim lobjOrdenServicio As New clsFichaProv
        Dim objDT As New DataSet
        Dim xData1, xData2, xData3 As New DataTable
        lobjOrdenServicio.CodigoEmpresa = Session("@EMPRESA")
        lobjOrdenServicio.NumeroOrdenServicio = strNumeroOrdenServicio
        lobjOrdenServicio.Item = strItem
        lobjOrdenServicio.MostrarOrdenServicio_Detalle_Conformidad(objDT)
        xData1 = objDT.Tables(0)

        With xData1.Rows(0)
            lblNroOrdeServicio.Text = .Item("var_Numero").ToString
            Me.lblNombreProveedor.Text = .Item("NO_CORT_PROV").ToString
            Me.lblRuc.Text = .Item("CO_PROV").ToString
            Me.lblFecha.Text = .Item("fe_emis").ToString
            Me.lblNombreContacto.Text = .Item("Contacto").ToString
            Me.lblTelefonoContacto.Text = .Item("NU_TLFN_CONC").ToString
            Me.lblEmail.Text = .Item("DE_MAIL").ToString
            Me.txtNroRequisicion.Text = .Item("nu_reqi").ToString
            Me.lblMonto.Text = numMonto
            Me.lblItem.Text = strItem

            Session("FlgObser") = .Item("FlgObser").ToString
            tipoServicio = .Item("TIPO_SERVICIO")
            If (.Item("TIPO_SERVICIO") = "I") Then
                lblTipoServicio.Text = "SI"
            Else
                lblTipoServicio.Text = "NO"
            End If

            Me.lblEstado.Text = .Item("chr_estado").ToString

            Me.lblObservaciones.Text = .Item("var_Observaciones").ToString
            Me.lblUsuario.Text = .Item("co_usua_modi").ToString

        End With


    End Sub
    Public Sub CargarDetallePagosPorItem(ByVal strOrdenServicio As String, ByVal strItem As String)
        Dim lobjOrdenServicio As New clsFichaProv
        Dim dt As DataTable = lobjOrdenServicio.ListarItemorMonto(strOrdenServicio, strItem)
        If dt.Rows.Count > 0 Then
            Session("dtDatos1") = dt
            grvItem.DataSource = dt
            grvItem.DataBind()
            TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(MONTO)", String.Empty), "#,###.00")
        End If
    End Sub


    Private Sub grvItem_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grvItem.EditCommand
        grvItem.EditItemIndex = e.Item.ItemIndex
        CargarDetallePagosPorItem(strOrdenServicio, strItem)
        CargarDatosCabecera(strOrdenServicio)
    End Sub

    Private Sub grvItem_UpdateCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grvItem.UpdateCommand
        Dim lobjOrdenServicio As New clsFichaProv
        Dim numMonto As Double
        Dim strSecu As String
        numMonto = Double.Parse(CType(e.Item.FindControl("txtMonto"), TextBox).Text)
        strSecu = CType(e.Item.FindControl("lblSecu"), Label).Text
        Dim resul As DataTable = lobjOrdenServicio.ActualizarMontoDetallePorMonto(strOrdenServicio, strItem, strSecu, numMonto)
        If resul.Rows(0).Item("RESULTADO").ToString() <> "" Then
            lblError.Text = resul.Rows(0).Item("RESULTADO").ToString()
            ClientScript.RegisterStartupScript(Me.[GetType](), "MSG", "<script>alert('" & resul.Rows(0).Item("RESULTADO").ToString() & "')</script>")
            Exit Sub
        Else
            lblError.Text = ""
            'ClientScript.RegisterStartupScript(Me.[GetType](), "MSG", "<script>alert('" & resul.Rows(0).Item("RESULTADO").ToString() & "')</script>")
        End If
        grvItem.EditItemIndex = -1
        CargarDetallePagosPorItem(strOrdenServicio, strItem)
        CargarDatosCabecera(strOrdenServicio)
    End Sub
    Private Sub grvItem_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grvItem.CancelCommand
        grvItem.EditItemIndex = -1
        CargarDetallePagosPorItem(strOrdenServicio, strItem)
        CargarDatosCabecera(strOrdenServicio)
    End Sub
End Class