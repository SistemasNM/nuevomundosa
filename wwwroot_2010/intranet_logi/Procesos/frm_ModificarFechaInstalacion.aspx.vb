Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient
Public Class frm_ModificarFechaInstalacion
    Inherits System.Web.UI.Page

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPedido As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSituacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSolicitante As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCentroCosto As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAlmacen As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblFecPedido As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecInstal As System.Web.UI.WebControls.TextBox

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@GRUPO_CODIGO") = "3000"
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "vvalenci"


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
    Dim strNumeroPedido As String
    Dim strTipo As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(frm_ModificarFechaInstalacion))
        If Not (Page.IsPostBack) Then
            If Not IsDBNull(Request.Item("intCodigoPedido")) Or Not (Request.Item("intCodigoPedido")) Is Nothing Then
                strNumeroPedido = Request.Item("intCodigoPedido")
                strTipo = Request.Item("intTipo")
                ActualizarPanel(strTipo)
                If strTipo = "1" Then
                    CargaPedido(strNumeroPedido)
                Else
                    CargarRequisicion(strNumeroPedido)
                End If
            End If
        End If
    End Sub
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function Actualizar(ByVal numPedido As String, ByVal strFecInstal As String) As Boolean
        Dim objPedidos As Logistica.clsPedidos
        objPedidos = New Logistica.clsPedidos
        Dim val As Boolean
        Try
            val = objPedidos.ActualizarPedidoFechaInstalacion(numPedido, strFecInstal)
        Catch ex As Exception
            val = False
        End Try
        Return val
    End Function
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function ActualizarReq(ByVal numRequi As String, ByVal strFecInstal As String, ByVal strFecFin As String) As Boolean
        Dim objPedidos As Logistica.clsPedidos
        objPedidos = New Logistica.clsPedidos
        Dim val As Boolean
        Try
            val = objPedidos.ActualizarRequiFechaInstalacion(numRequi, strFecInstal, strFecFin)
        Catch ex As Exception
            val = False
        End Try
        Return val
    End Function
    'Private Sub btnActualizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnActualizar.Click
    '    Dim objPedidos As Logistica.clsPedidos
    '    objPedidos = New Logistica.clsPedidos
    '    objPedidos.ActualizarPedidoFechaInstalacion("0003" + strNumeroPedido, txtFecInstal.Text, Session("@EMPRESA"))
    'End Sub
    Private Sub ActualizarPanel(ByVal strTipo As String)
        If strTipo = "1" Then
            pnlPedido.Visible = True
            pnlReque.Visible = False
        Else
            pnlPedido.Visible = False
            pnlReque.Visible = True
        End If
    End Sub
    Private Sub CargarRequisicion(ByVal strNumeroRequi As String)
        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        ldtablas = obj.Listar_reqi_Detalle(strNumeroPedido, Session("@EMPRESA"))
        If ldtablas.Tables(0).Rows.Count <> 0 Then
            txtReq.Text = Mid(ldtablas.Tables(0).Rows(0)("NU_REQI"), 1, 4) + "-" + Mid(ldtablas.Tables(0).Rows(0)("NU_REQI"), 6, 10)
            txtFec.Text = ldtablas.Tables(0).Rows(0)("FE_EMIS_REQI")
            txtFecIni.Text = ldtablas.Tables(0).Rows(0)("FE_TOPE_REQI")
            txtFecFin.Text = ldtablas.Tables(0).Rows(0)("FE_EXPI_REQI")
            txtAreaBenef.Text = ldtablas.Tables(0).Rows(0)("CO_JEFATURA_SOLI") + "-" + ldtablas.Tables(0).Rows(0)("NombreJefatura")
            txtRespo.Text = ldtablas.Tables(0).Rows(0)("CO_RESP") + "-" + ldtablas.Tables(0).Rows(0)("DE_RESP")
            txtAlma.Text = ldtablas.Tables(0).Rows(0)("CO_ALMA")
            txtObs.Text = ldtablas.Tables(0).Rows(0)("DE_OBSE_0001")
        End If
        'Dim lobjRequisicion As OFISIS.OFILOGI.Requisiciones
        'lobjRequisicion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        'lobjRequisicion.Codigo = strNumeroRequi
        'If lobjRequisicion.Consultar() Then
        '    With lobjRequisicion.SetDatos.Tables(0)
        '        txtReq.Text = .Rows(0)("NU_REQI")
        '        txtFecEmi.Text = Format(.Rows(0)("FE_EMIS_REQI"), "dd/MM/yyyy")
        '        txtComprador.Text = .Rows(0)("CO_COMP") + " - " + .Rows(0)("NO_COMP")
        '        txtCeCo.Text = .Rows(0)("CO_AUXI_EMPR") + " - " + .Rows(0)("NO_AUXI")
        '        txtUnidad.Text = .Rows(0)("CO_UNID") + " - " + .Rows(0)("DE_UNID")
        '        txtAlma.Text = .Rows(0)("CO_ALMA") + " - " + .Rows(0)("DE_ALMA")
        '        txtObs.Text = .Rows(0)("DE_OBSE_0001")
        '        txtFecIni.Text = Format(.Rows(0)("FE_TOPE_REQI"), "dd/MM/yyyy")
        '    End With
        'End If
    End Sub
    Private Sub CargaPedido(ByVal strNumeroPedido As String)
        Dim objPedidos As Logistica.clsPedidos
        Dim intCodPedido As Integer = strNumeroPedido
        Dim strSerie As String = "0003"
        Dim strTipo As String = "0"
        Dim strFechaIni As String = ""
        Dim strFechaFin As String = ""
        Dim strSolicitante As String = ""
        Dim strEstado As String = ""
        Dim dtbPedido As DataTable
        objPedidos = New Logistica.clsPedidos
        dtbPedido = objPedidos.fncConsultaPedidos(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
        If Not dtbPedido Is Nothing Then
            If dtbPedido.Rows().Count > 0 Then
                lblPedido.Text = dtbPedido.Rows(0).Item("nu_pedi")
                lblSituacion.Text = dtbPedido.Rows(0).Item("ti_situ")
                lblSolicitante.Text = dtbPedido.Rows(0).Item("CodSolicitante") + " - " + dtbPedido.Rows(0).Item("NomSolicitante")
                lblCentroCosto.Text = dtbPedido.Rows(0).Item("CodCentroCostos") + " - " + dtbPedido.Rows(0).Item("DesCentroCostos")
                lblAlmacen.Text = Trim(dtbPedido.Rows(0).Item("co_alma")) + " - " + dtbPedido.Rows(0).Item("de_alma")
                lblFecPedido.Text = dtbPedido.Rows(0).Item("fe_pedi")
                txtFecInstal.Text = dtbPedido.Rows(0).Item("FE_INSTAL")
            End If
        End If

    End Sub
End Class