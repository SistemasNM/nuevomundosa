Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ConsultaPedidosRegistrados
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
  End Sub

  Protected WithEvents txtFechaIni As System.Web.UI.WebControls.TextBox
  Protected WithEvents ddlEstado As System.Web.UI.WebControls.DropDownList
  Protected WithEvents txtFechaFin As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtSolicitante As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblItems As System.Web.UI.WebControls.Label
  Protected WithEvents dgListaPedidos As System.Web.UI.WebControls.DataGrid
  Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
  Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
  Protected WithEvents lblDesSolicitante As System.Web.UI.WebControls.TextBox
  Protected WithEvents cboPrioridad As System.Web.UI.WebControls.DropDownList

  Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtNumeroPedido As System.Web.UI.WebControls.TextBox

  Private designerPlaceholderDeclaration As System.Object

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    Session("@GRUPO_CODIGO") = "3000"
    Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "jcucho"

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LimpiarControles()
            txtSerie.Text = "0003"
            txtNumeroPedido.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
        End If

    End Sub

    Private Sub LimpiarControles()
        txtNumeroPedido.Text = "0"
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        txtSolicitante.Text = ""
        txtSolicitante.Text = ""
        lblDesSolicitante.Text = ""
        lblItems.Text = ""
        lblMsg.Text = ""
    End Sub

    Private Function fncValidaFiltros() As Boolean
        Dim lblnValidation As Boolean = True
        lblMsg.Text = ""
        Try
            If (Trim(txtNumeroPedido.Text).ToString = "") Then
                txtNumeroPedido.Text = "0"
            End If
            If (Trim(txtSolicitante.Text).Length = 0 AndAlso (Trim(txtNumeroPedido.Text).Length = 0 OrElse Trim(txtNumeroPedido.Text).ToString = "0")) Then
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
                Else
                    lblMsg.Text = "For favor seleccione ingrese algun filtro"
                    lblnValidation = False
                    Return lblnValidation
                    Exit Function
                End If
            End If
            'If Trim(txtSolicitante.Text).Length = 0 Then
            '    lblMsg.Text = "Eliga al solicitante del pedido."
            '    lblnValidation = False
            'End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        Return lblnValidation
    End Function

  Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
    Try
            If fncValidaFiltros() = True Then
                ListaPedidoRegistrados()
            Else
                dgListaPedidos.DataSource = Nothing
                lblItems.Text = ""
                dgListaPedidos.DataBind()
            End If
    Catch ex As Exception
      lblMsg.Text = "Error al consultar datos " + ex.Message
    End Try
  End Sub

  Public Sub ListaPedidoRegistrados()
    Dim objPedidos As New Logistica.clsPedidos
    Dim ldtDetalle As New DataTable

    Dim strTipo As String = ""
    Dim strSerie As String = ""
    Dim intNumeroPedido As Integer = 0
    Dim strFechaIni As String = ""
    Dim strFechaFin As String = ""
    Dim strSolicitante As String = ""
    Dim strEstado As String = ""
    Dim strUsuario As String = ""
    Dim strPrioridad As String = ""

    ldtDetalle = Nothing
    strTipo = "0"
    strSerie = txtSerie.Text
    intNumeroPedido = IIf(Trim(txtNumeroPedido.Text).Length = 0, "0", Integer.Parse(Trim(txtNumeroPedido.Text)))
    strFechaIni = Mid(txtFechaIni.Text, 7, 4) + Mid(txtFechaIni.Text, 4, 2) + Mid(txtFechaIni.Text, 1, 2)
    strFechaFin = Mid(txtFechaFin.Text, 7, 4) + Mid(txtFechaFin.Text, 4, 2) + Mid(txtFechaFin.Text, 1, 2)
    strSolicitante = Trim(txtSolicitante.Text)
    strEstado = Trim(ddlEstado.SelectedValue())
    strUsuario = Session("@USUARIO")
    strPrioridad = IIf(cboPrioridad.SelectedValue = "TODOS", "", cboPrioridad.SelectedValue)

    Try
      ' Consultamos Pedidos por Registrados
      ldtDetalle = objPedidos.fncPedidosGeneral(strTipo, strSerie, intNumeroPedido, strFechaIni, strFechaFin, _
                                                  strSolicitante, strEstado, strUsuario, strPrioridad)
      If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
        dgListaPedidos.DataSource = ldtDetalle
        dgListaPedidos.DataBind()
        dgListaPedidos.Visible = True
        lblItems.Text = "Numero de Pedidos: " + ldtDetalle.Rows.Count.ToString
        lblItems.Visible = True
        lblMsg.Text = ""
      Else
        dgListaPedidos.DataSource = Nothing
        dgListaPedidos.Visible = False
        lblMsg.Text = "No existen pedidos en los parametros indicados"
        lblItems.Visible = False
      End If
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Private Sub dgListaPedidos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListaPedidos.ItemCommand
    Dim intNumPedido As Integer
    Select Case e.CommandName
      Case "Reporte"
        Dim olblNumeroPedido As Label = CType(e.Item.FindControl("lblNumeroPedido"), Label)
        intNumPedido = Integer.Parse(Mid(olblNumeroPedido.Text, 6, 10))
        fnc_VerReporte(intNumPedido)
    End Select
  End Sub

  ' Listar reporte detalle
  Private Sub fnc_VerReporte(intNumPedido As Integer)
    Dim strURL As String = ""
    Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strReporte As String = ""

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        'strURL = strURL + "logistica_ConsultaVale"
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strReporte = "log_consulta_vale"
        strURL = strURL + strPath + strReporte
        'CAMBIO DG FIN
    strURL = strURL + "&chrTipo=" & "1"
    strURL = strURL + "&intNumPedido=" & intNumPedido.ToString

    strURL = strURL + "&rc:Command=Render"
    strURL = strURL + "&rc:Toolbar=true"
    strScript = "fMostrarReporte('" & strURL & "');"
    ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

  End Sub
End Class
