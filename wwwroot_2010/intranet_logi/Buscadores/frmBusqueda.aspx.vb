Imports NM_General

Public Class frmBusqueda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ViewState("TipoBusqueda") = Request.QueryString("TipoBusqueda")
            ViewState("Param1") = Request.QueryString("Param1")
            ViewState("Param2") = Request.QueryString("Param2")
            Call ConfigurarFormulario()
            Call BuscarConsulta()
        End If
        lblMensajeError.Text = ""
    End Sub

#Region "Metodos"
    Private Sub ConfigurarFormulario()
        Call DesactivarPaneles()
        'Configuracion segun tipo
        Select Case ViewState("TipoBusqueda").ToString
            Case "ArticuloDesperdicios"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Articulo de Desperdicios"
            Case "CentroCostosTejeduria"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Centro de Costos de Desperdicios - Tejeduria"
            Case "ProcesoOrigen"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Proceso Origen"            
            Case "CentrodeCostos"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Centro de Costos"
            Case "CuentaGastos"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Cuenta de Gastos"
            Case "DesperdiciosAlgodon"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Articulos de Desperdicios de Algodon"
            Case "OrdenTrabajo"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Ordenes de Trabajo"
            Case "Almacen"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Almacenes"
            Case "Responsable"
                pnl_GrillaEstandar.Visible = True
                lblTitulo.Text = "Busqueda de Responsables de O/T"
        End Select
        Me.Title = lblTitulo.Text
    End Sub

    Private Sub DesactivarPaneles()
        'Desactivar todos los paneles
        'pnl_GrillaArticulosDesperdicios.Visible = False
        pnl_GrillaEstandar.Visible = False
    End Sub


    Private Sub BindGrid_GrillaEstandar(ByVal strOpcionBusqueda As String)
        Dim objLogistica As New NM_Logistica        
        Dim dtbDatos As DataTable
        Dim strCodigo As String
        Dim strDescripcion As String

        Try
            strCodigo = txtCodigo.Text
            strDescripcion = txtDescripcion.Text
            dtbDatos = Nothing

            Select Case strOpcionBusqueda
                Case "ArticuloDesperdicios" : dtbDatos = objLogistica.ufn_BusquedaArticulosDesperdicios(strCodigo, strDescripcion)
                Case "CentroCostosTejeduria" : dtbDatos = objLogistica.ufn_BusquedaCentroCostosTejeduria(strCodigo, strDescripcion)
                Case "ProcesoOrigen" : dtbDatos = objLogistica.ufn_BusquedaProcesoOrigen(strCodigo, strDescripcion)
                Case "CentrodeCostos" : dtbDatos = objLogistica.ufn_BusquedaCentrodeCostos(strCodigo, strDescripcion)
                Case "CuentaGastos" : dtbDatos = objLogistica.ufn_BusquedaCuentaGastos(strCodigo, strDescripcion, ViewState("Param1").ToString, ViewState("Param2").ToString)                    
                Case "DesperdiciosAlgodon" : dtbDatos = objLogistica.ufn_BusquedaArticulosDesperdiciosAlgodon(strCodigo, strDescripcion)
                Case "OrdenTrabajo" : dtbDatos = objLogistica.ufn_BusquedaOrdenTrabajo(strCodigo, strDescripcion, ViewState("Param1").ToString, ViewState("Param2").ToString)
                Case "Almacen" : dtbDatos = objLogistica.ufn_BusquedaAlmacen(strCodigo, strDescripcion)
                Case "Responsable" : dtbDatos = objLogistica.ufn_BusquedaResponsableOT(strCodigo, strDescripcion)

            End Select

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
            objVentas = Nothing
            dtbDatos = Nothing
        End Try

    End Sub

    Private Sub BuscarConsulta()
        'If (txtCodigo.Text = "" And txtDescripcion.Text = "") Then
        '    If ViewState("TipoBusqueda").ToString = "ArticuloDesperdicios" Or ViewState("TipoBusqueda").ToString = "ProcesoOrigen" Then
        '        lblMensajeError.Text = "Debe Ingresar un valor para la Busqueda"
        '        Exit Sub
        '    End If
        'End If

        Call BindGrid_GrillaEstandar(ViewState("TipoBusqueda").ToString)
    End Sub

#End Region

#Region "Eventos"

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        
        Call BuscarConsulta()

    End Sub


    Private Sub dgGrillaEstandar_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgGrillaEstandar.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)            
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            If ViewState("TipoBusqueda").ToString = "DesperdiciosAlgodon" Then
                btnEscoger.Attributes.Add("onClick", "btnEscogerDesperdicios_Onclick('" & drvDatos("CODIGO") & "', '" & Replace(drvDatos("DESCRIPCION"), "'", "\'").ToString & "','" & drvDatos("STOCK") & "')")
            Else
                btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("CODIGO") & "', '" & Replace(drvDatos("DESCRIPCION"), "'", "\'").ToString & "')")

            End If

        End If
    End Sub


    Protected Sub dgGrillaArticulos_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgGrillaEstandar.PageIndexChanged
        dgGrillaEstandar.CurrentPageIndex = e.NewPageIndex
        'Configuracion segun tipo
        Call BindGrid_GrillaEstandar(ViewState("TipoBusqueda").ToString)        
        dgGrillaEstandar.DataBind()
    End Sub

    


#End Region


End Class