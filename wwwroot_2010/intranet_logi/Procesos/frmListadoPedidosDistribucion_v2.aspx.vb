Imports NM_General
Imports NuevoMundo

Public Class frmListadoPedidosDistribucion_v2
    Inherits System.Web.UI.Page

    'Private Shared _dsDistribucion As New DataSet("Distribucion")
    'Public Shared Property dsDistribucion() As DataSet
    '    Get
    '        Return HttpContext.Current.Session("dsDistribucion")
    '    End Get
    '    Set(ByVal value As DataSet)
    '        HttpContext.Current.Session("dsDistribucion") = value
    '    End Set
    'End Property
    'Shared lstPedAgregado As New List(Of clsSeleccion)
    'Public Shared Property PedAgregado() As List(Of clsSeleccion)
    '    Get
    '        Return HttpContext.Current.Session("PedAgregado")
    '    End Get
    '    Set(ByVal value As List(Of clsSeleccion))
    '        HttpContext.Current.Session("PedAgregado") = value
    '    End Set
    'End Property

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("Usuario") = "OBLAS"
        If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
            Dim objRequest As New BLITZ_LOCK.clsRequest
            Session("Usuario") = objRequest.Decripta(Request("Usuario"))
        End If
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("Usuario") Is Nothing) OrElse Session("Usuario") = "" Then
            Response.Redirect("../../webproduccion/noaccess.htm", True)
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                InicializarControles()
                CargarCodigoArticulos()
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Sub InicializarControles()
        '_dsDistribucion = dsDistribucion
        idMensaje.Visible = False
        idBotones.Visible = False
        idDatosGenerales.Visible = False
    End Sub

    Sub CargarCodigoArticulos()
        Dim dtConsultaItems = New DataTable
        Dim objPedidos As New Logistica.clsPedidos
        dtConsultaItems = objPedidos.fnListadoItemsModuloDistribucion()

        combobox.TextField = "CO_ITEM"
        combobox.ValueField = "CO_ITEM"
        combobox.DataSource = dtConsultaItems
        combobox.DataBind()
        combobox.AutoFilterQueryType = Infragistics.Web.UI.ListControls.AutoFilterQueryTypes.Contains
        combobox.CurrentValue = ""
    End Sub


    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            BuscarDistribucion()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub BuscarDistribucion()
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtResponse As New DataTable
        Try
            Dim strCodigoArt As String = combobox.CurrentValue

            


            dtResponse = objPedidos.fnListadoArticulosDistribV3_1(strCodigoArt)
            GridView1.DataSource = dtResponse
            GridView1.DataBind()

            If GridView1.Rows.Count > 0 Then
                'Mensaje  de Nombre de articulo y articulo
                lblCodigoArt.Text = dtResponse.Rows(0).ItemArray(3).ToString()
                'Cantidad de Stock vs Pedido
                lblStockvsPedido.Text = (objPedidos.fnStockVSPedidoDistrib(strCodigoArt)).Rows(0).ItemArray(2).ToString()
                idMensaje.Visible = False
                idBotones.Visible = True
                idDatosGenerales.Visible = True
            Else
                lblStock.Text = "No se ha encontrado pedido con este artículo."
                idMensaje.Visible = True
                idBotones.Visible = False
                idDatosGenerales.Visible = False
            End If

            '_dsDistribucion = New DataSet
            'objPedidos.fnListadoArticulosDistribV2(_dsDistribucion, strCodigoArt)

            'With _dsDistribucion
            '    .Tables(0).TableName = "Distribucion"
            '    .Tables(1).TableName = "Detalle"

            '    Dim ColsP() As DataColumn = New DataColumn() {.Tables("Distribucion").Columns("CO_CLIE"), .Tables("Distribucion").Columns("CO_ITEM")}
            '    Dim ColsD() As DataColumn = New DataColumn() {.Tables("Detalle").Columns("CO_CLIE"), .Tables("Detalle").Columns("NU_PEDI"), .Tables("Detalle").Columns("CO_ITEM")}
            '    Dim ColsDRel() As DataColumn = New DataColumn() {.Tables("Detalle").Columns("CO_CLIE"), .Tables("Detalle").Columns("CO_ITEM")}

            '    .Tables("Distribucion").PrimaryKey = ColsP
            '    .Tables("Detalle").PrimaryKey = ColsD
            '    .Relations.Add("DistribucionDetalle", ColsP, ColsDRel)
            'End With

            '_dsDistribucion.AcceptChanges()

            'dsDistribucion = _dsDistribucion

            'Me.igDtg_Distribucion.Bands(0).DataMember = "Detalle"
            'Me.igDtg_Distribucion.Bands(0).DataKeyFields = "CO_CLIE"

            'Me.igDtg_Distribucion.GridView.Rows.Clear()

            'Me.igDtg_Distribucion.DataSource = _dsDistribucion
            'Me.igDtg_Distribucion.DataBind()

            'If _dsDistribucion.Tables("Distribucion").Rows.Count >= 1 Then
            '    Me.igDtg_Distribucion.GridView.Rows(0).Expanded = True
            '    'Mensaje  de Nombre de articulo y articulo
            '    lblCodigoArt.Text = dsDistribucion.Tables("Distribucion").Rows(0).ItemArray(3).ToString()
            '    idMensaje.Visible = False
            '    idBotones.Visible = True
            '    idDatosGenerales.Visible = True
            'Else
            '    lblStock.Text = "No se ha encontrado pedido con este artículo."
            '    idMensaje.Visible = True
            '    idBotones.Visible = False
            '    idDatosGenerales.Visible = False
            'End If
            

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        ''Dim sbPedido As New StringBuilder
        ''sbPedido.Append("<root>")
        ' ''For Each rowP As DataRow In _dsDistribucion.Tables("Distribucion").Rows
        ' ''    Dim mtsRepartir = rowP("MTS_TOTAL_PROGRAMADOS")
        ' ''Next
        ''sbPedido.Append("</root>")
        'For index = 0 To Me.igDtg_Distribucion.Rows.Count - 1
        '    Dim mtsRepartir As String = Me.igDtg_Distribucion.Rows(index).Items(6).Text
        'Next
        'For Each rec As Infragistics.Web.UI.GridControls.GridRecord In igDtg_Distribucion.Rows
        '    Dim mts As String = rec.Items(0).Text
        'Next
    End Sub

    Protected Sub IbtnRefrescarSvsP_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles IbtnRefrescarSvsP.Click
        Dim strCodigoArt As String = combobox.CurrentValue
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtStockvsPedido As DataTable = objPedidos.fnStockVSPedidoDistrib(strCodigoArt)
        lblStockvsPedido.Text = dtStockvsPedido.Rows(0).ItemArray(2).ToString()
        BuscarDistribucion()
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim objPedidos As New Logistica.clsPedidos
        Dim strCodigoArt As String = combobox.CurrentValue
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim pub_id As String = GridView1.DataKeys(e.Row.RowIndex).Value.ToString()
            dtResponse = objPedidos.fnListadoArticulosDistribV3_2(strCodigoArt, pub_id)
            Dim pubTitle As GridView = DirectCast(e.Row.FindControl("GridView2"), GridView)
            pubTitle.DataSource = dtResponse
            pubTitle.DataBind()
        End If
    End Sub
End Class