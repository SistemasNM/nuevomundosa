Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ListadoPedidosPorAtender
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub

    Protected WithEvents txtFechaIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecIniIns As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecFinIns As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumeroDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents lblSolicitante As System.Web.UI.WebControls.Label
    Protected WithEvents txtSolicitante As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgListaPedidos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label
    Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
    Protected WithEvents lblUsu As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkTipo As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddwTipo As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        '--Inicio: VERIFICAR LA SESION
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
        InitializeComponent()
    End Sub

#End Region

    Public mensaje As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(frm_ListadoPedidosPorAtender))
        If Not Page.IsPostBack Then
            txtSerie.Text = "0003"
            txtSerie.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
            txtNumeroDocumento.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
        End If
    End Sub


    
    ' validamos filtros
    Private Function fValidaFiltros() As Boolean
        Dim lblnValidation As Boolean = True
        lblMsg.Text = ""
        ' fecha pedido
        If txtFechaIni.Text.Length > 0 And txtFechaFin.Text.Length > 0 Then
            If Not IsDate(txtFechaIni.Text) Then
                lblMsg.Text = "La fecha inicial de busqueda es incorrecta."
                Return False
                Exit Function
            End If
            If Not IsDate(txtFechaFin.Text) Then
                lblMsg.Text = "La fecha final de busqueda es incorrecta."
                Return False
                Exit Function
            End If
            If IsDate(txtFechaIni.Text) And IsDate(txtFechaFin.Text) Then
                If CDate(txtFechaIni.Text) > CDate(txtFechaFin.Text) Then
                    lblMsg.Text = "La fecha inicial de busqueda no puede ser mayor a la fecha final de busqueda. !<br>"
                    Return False
                    Exit Function
                End If
            End If
        End If

        ' fecha de instalacion
        If txtFecIniIns.Text.Length > 0 And txtFecFinIns.Text.Length > 0 Then
            If Not IsDate(txtFecIniIns.Text) Then
                lblMsg.Text = "La fecha inicial de instalacion es incorrecta. !<br>"
                Return False
                Exit Function
            End If
            If Not IsDate(txtFecFinIns.Text) Then
                lblMsg.Text = "La fecha final de instalacion es incorrecta. !<br>"
                Return False
                Exit Function
            End If
            If IsDate(txtFecIniIns.Text) And IsDate(txtFecFinIns.Text) Then
                If CDate(txtFecIniIns.Text) > CDate(txtFecFinIns.Text) Then
                    lblMsg.Text = "La fecha inicial de instalacion no puede ser mayor a la fecha final de instalacion. !<br>"
                    Return False
                    Exit Function
                End If
            End If
        End If
        Return lblnValidation
    End Function

    ' boton buscar
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If fValidaFiltros() = True Then
                ListaPedidoPorAtender()
            End If
        Catch ex As Exception
            lblMsg.Text = "Ha ocurrido un error a listar pedidos por atender." + ex.Message
        End Try
    End Sub

    ' consulta pedidos
    Public Sub ListaPedidoPorAtender()
        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtDetalle As New DataTable

        Dim strTipo As String = ""
        Dim strSerie As String = ""
        Dim intCodPedido As Integer
        Dim strFechaIni As String = ""
        Dim strFechaFin As String = ""
        Dim strFecIniIns As String = ""
        Dim strFecFinIns As String = ""
        Dim strSolicitante As String = ""
        Dim strEstado As String = ""
        Dim strUsuario As String = ""

        ldtDetalle = Nothing
        Try
            strSerie = txtSerie.Text
            strFechaIni = Mid(txtFechaIni.Text, 7, 4) & Mid(txtFechaIni.Text, 4, 2) & Mid(txtFechaIni.Text, 1, 2)
            strFechaFin = Mid(txtFechaFin.Text, 7, 4) & Mid(txtFechaFin.Text, 4, 2) & Mid(txtFechaFin.Text, 1, 2)

            strFecIniIns = Mid(txtFecIniIns.Text, 7, 4) & Mid(txtFecIniIns.Text, 4, 2) & Mid(txtFecIniIns.Text, 1, 2)
            strFecFinIns = Mid(txtFecFinIns.Text, 7, 4) & Mid(txtFecFinIns.Text, 4, 2) & Mid(txtFecFinIns.Text, 1, 2)

            strSolicitante = txtSolicitante.Text
            strEstado = ddlEstado.SelectedValue()
            strUsuario = Session("@USUARIO")

            If Trim(txtNumeroDocumento.Text).Length > 0 Then
                intCodPedido = Integer.Parse(Trim(txtNumeroDocumento.Text))
            Else
                intCodPedido = 0
            End If

            ' -- Consultamos Pedidos por Atender
            ldtDetalle = objPedidos.fncPedidosConsulta(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, _
                                                        strSolicitante, strEstado, strUsuario, "", "Atender", strFecIniIns, strFecFinIns)
            If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                dgListaPedidos.DataSource = ldtDetalle
                dgListaPedidos.DataBind()
                dgListaPedidos.Visible = True
                lblItems.Text = "Numero de Pedidos: " + ldtDetalle.Rows.Count.ToString
                lblItems.Visible = True
            Else
                dgListaPedidos.DataSource = Nothing
                dgListaPedidos.Visible = False
                lblMsg.Text = "No existen pedidos pendientes en los parametros indicados"
                lblItems.Visible = False
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error en la consulta." + ex.ToString
        End Try

    End Sub

    Private Sub dgListaPedidos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListaPedidos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
            Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim strNumeroPedido As String = ""
            strNumeroPedido = ldrvDatos("nu_pedi")
            '---------------------------------------------------------------------------------------------
            'PEDIDOS EPPS - DG - 14/09/2018 - INI
            '---------------------------------------------------------------------------------------------
            'If Mid(strNumeroPedido, 1, 4) = "0003" Then
            '    lobjBoton.Attributes.Add("onClick", "VerAtenderPedido('" + ldrvDatos("nu_pedi") + "')")
            'Else
            '    lobjBoton.Attributes.Add("onClick", "VerAtenderHilo('" + ldrvDatos("nu_pedi") + "','" + ldrvDatos("co_alma") + "')")
            'End If
            If Mid(strNumeroPedido, 1, 4) = "0003" Then
                lobjBoton.Attributes.Add("onClick", "VerAtenderPedido('" + ldrvDatos("nu_pedi") + "')")
            ElseIf Mid(strNumeroPedido, 1, 4) = "0004" Then
                lobjBoton.Attributes.Add("onClick", "VerAtenderHilo('" + ldrvDatos("nu_pedi") + "','" + ldrvDatos("co_alma") + "')")
            Else
                lobjBoton.Attributes.Add("onClick", "VerAtenderPedidoEPPsOtros('" + ldrvDatos("nu_pedi") + "')")
            End If
            '---------------------------------------------------------------------------------------------
            'PEDIDOS EPPS - DG - 14/09/2018 - FIN
            '---------------------------------------------------------------------------------------------
        End If
    End Sub
    '---------------------------------------------------------------------------------------------
    'PEDIDOS EPPS - DG - 14/09/2018 - INI
    '---------------------------------------------------------------------------------------------
    'Protected Sub chkTipo_CheckedChanged(sender As Object, e As EventArgs) Handles chkTipo.CheckedChanged
    '    If chkTipo.Checked = False Then
    '        txtSerie.Text = "0003"
    '    Else
    '        txtSerie.Text = "0004"
    '    End If
    'End Sub

    Private Sub ddwTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddwTipo.SelectedIndexChanged
        If ddwTipo.SelectedValue = 1 Then
            txtSerie.Text = "0003"
        ElseIf ddwTipo.SelectedValue = 2 Then
            txtSerie.Text = "0004"
        Else
            txtSerie.Text = "0005"
        End If
    End Sub
    '---------------------------------------------------------------------------------------------
    'PEDIDOS EPPS - DG - 14/09/2018 - FIN
    '---------------------------------------------------------------------------------------------
End Class
