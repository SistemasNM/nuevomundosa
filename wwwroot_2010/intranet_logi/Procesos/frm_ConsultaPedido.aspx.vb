Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ConsultaPedido
    Inherits System.Web.UI.Page

  Dim strSerie As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents lblUsu As System.Web.UI.WebControls.Label
    'Protected WithEvents lblUsuario As System.Web.UI.WebControls.Label
    Protected WithEvents dgListaPedidos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFechaIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSolicitante As System.Web.UI.WebControls.Label
    Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumeroPedido As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label
    Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSolicitante As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    Session("@GRUPO_CODIGO") = "3000"
    Session("@EMPRESA") = "01"
    'Session("@USUARIO") = "atorresc"

    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
      Response.Redirect("/intranet/finsesion_popup.htm", True)
    End If
    InitializeComponent()
  End Sub

#End Region

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Ajax.Utility.RegisterTypeForAjax(GetType(frm_RegistroPedido))
    If Not Page.IsPostBack Then
      LimpiarControles()
      txtSerie.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
      txtNumeroPedido.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
    End If
  End Sub

  Private Sub LimpiarControles()
    strSerie = "0003"
    txtSerie.Text = strSerie
    txtSerie.Enabled = False
    txtNumeroPedido.Text = "0"
    txtFechaIni.Text = ""
    txtFechaFin.Text = ""
    txtSolicitante.Text = ""
    txtSolicitante.Text = ""
    lblSolicitante.Text = ""
    lblItems.Text = ""
    lblMsg.Text = ""
  End Sub

  Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
    If fncValidaFiltros() = True Then
      fncConsultaPedidos()
    End If
  End Sub

  ' Consulta pedido
  Public Sub fncConsultaPedidos()
    Dim objPedidos As New Logistica.clsPedidos
    Dim ldblPedidos As New DataTable

    Dim strTipo As String = ""
    Dim strSerie As String = 0
    Dim intCodPedido As Integer = 0
    Dim strFechaIni As String = ""
    Dim strFechaFin As String = ""
    Dim strSolicitante As String = ""
    Dim strEstado As String = ""
    Dim strUsuario As String = ""

    ldblPedidos = Nothing

    Try
      strTipo = "0"
      strSerie = Trim(txtSerie.Text)
      If Trim(txtNumeroPedido.Text).Length > 0 Then
        intCodPedido = Integer.Parse(txtNumeroPedido.Text)
      Else
        intCodPedido = 0
      End If

      strFechaIni = Trim(txtFechaIni.Text)
      strFechaFin = Trim(txtFechaFin.Text)
      strSolicitante = Trim(txtSolicitante.Text)
      strEstado = ddlEstado.SelectedItem.Value
      strUsuario = Session("@USUARIO")

      ' Consultamos Pedidos
      ldblPedidos = objPedidos.fncPedidosConsulta(strTipo, strSerie, intCodPedido, _
                                                  strFechaIni, strFechaFin, strSolicitante, _
                                                  strEstado, strUsuario, "", "Consulta", "", "")
      If Not ldblPedidos Is Nothing Then
        If ldblPedidos.Rows.Count > 0 Then
          dgListaPedidos.DataSource = ldblPedidos
          dgListaPedidos.DataBind()
          dgListaPedidos.Visible = True
          lblItems.Text = "Numero de pedidos: " + ldblPedidos.Rows.Count.ToString
        Else
          dgListaPedidos.DataSource = Nothing
          dgListaPedidos.Visible = False
          lblMsg.Text = "No existen Pedidos en los parametros indicados."
          lblItems.Text = ""
        End If
      End If
    Catch ex As Exception
      lblMsg.Text = "Ha ocurrido un error al listar, comuniquese con sistemas" + ex.ToString
    End Try
  End Sub

  ' Validamos ingreso de datos
  Private Function fncValidaFiltros() As Boolean
    Dim lblnValidation As Boolean = True
    lblMsg.Text = ""
    Try
      If txtFechaIni.Text.Length > 0 And txtFechaFin.Text.Length > 0 Then
        If Not IsDate(txtFechaIni.Text) Then
          lblMsg.Text = "Error: la fecha inicial de busqueda es incorrecta."
          lblnValidation = False
          Return lblnValidation
          Exit Function
        End If

        If Not IsDate(txtFechaFin.Text) Then
          lblMsg.Text = "Error: la fecha final de busqueda es incorrecta."
          lblnValidation = False
          Return lblnValidation
          Exit Function
        End If

        If IsDate(txtFechaIni.Text) And IsDate(txtFechaFin.Text) Then
          If CDate(txtFechaIni.Text) > CDate(txtFechaFin.Text) Then
            lblMsg.Text = "La fecha inicial de busqueda no puede ser mayor a la fecha final de busqueda."
            lblnValidation = False
            Return lblnValidation
            Exit Function
          End If
        End If
      End If

    Catch ex As Exception
      lblMsg.Text = ex.Message
    End Try

    Return lblnValidation
  End Function

  Private Sub dgListaPedidos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListaPedidos.ItemDataBound
    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
      Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
      Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
      lobjBoton.Attributes.Add("onClick", "VerRegistroPedido('" + ldrvDatos("nu_pedi") + "')")
    End If
  End Sub
End Class
