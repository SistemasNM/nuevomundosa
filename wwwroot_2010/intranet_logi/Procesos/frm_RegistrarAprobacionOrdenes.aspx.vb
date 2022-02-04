Imports System.Data
Imports System.Data.SqlClient
Imports NuevoMundo.Logistica
Imports OFISIS

Public Class frm_RegistrarAprobacionOrdenes
    Inherits System.Web.UI.Page

    Dim intSec As Integer = 0

    Private Sub frm_RegistrarAprobacionOrdenes_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ECASTILL"

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
        If Not Page.IsPostBack Then
        End If
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As System.EventArgs) Handles btnConsultar.Click
        ConsultarOrdenes()
    End Sub

#Region "Metodos-Funciones"
    Public Sub ConsultarOrdenes()
        Dim dtbOrdenes As DataTable
        Dim objOrdenes As New NuevoMundo.Logistica.OrdenCompra
        Dim strEmpresa As String
        Dim strUsuario As String
        Dim intContador As Integer = 0
        dtbOrdenes = Nothing
        strEmpresa = Session("@EMPRESA")
        strUsuario = Session("@USUARIO")
        Try
            dtbOrdenes = objOrdenes.ListaOrdenesAprobar()
            If Not dtbOrdenes Is Nothing Then
                intContador = dtbOrdenes.Rows.Count
                If intContador > 0 Then
                    dtgLista.DataSource = dtbOrdenes
                    dtgLista.DataBind()
                    lblContador.Text = "Numero de ordenes: " + intContador.ToString
                Else
                    dtgLista.DataSource = Nothing
                    lblContador.Text = "Numero de ordenes: " + intContador.ToString
                End If
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error al lista las OC/OS " & ex.Message
        End Try
    End Sub
#End Region

#Region "Grilla"
    Private Sub dtgLista_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgLista.ItemCommand
        Dim objtxtGrupo As TextBox = CType(e.Item.FindControl("txtGrupo"), TextBox)
        Select Case e.CommandName
            Case "Aprobar"
                ConsultarOrdenes()
            Case "Detalle"
                ConsultarOrdenes()
        End Select

    End Sub

    Private Sub dtgLista_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        Dim strNumOC As String = ""
        Dim strNumRq As String = ""
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
                'Dim lobjCotizar As CheckBox = CType(e.Item.FindControl("chkAprobar"), CheckBox)
                Dim btnVerAdjuntos As ImageButton = CType(e.Item.FindControl("ibtVerAdj"), ImageButton)
                Dim btnEnviaCorreo As ImageButton = CType(e.Item.FindControl("lblEmail"), ImageButton)
                Dim btnGrupo As Button = CType(e.Item.FindControl("btnGrupo"), Button)
                Dim btnDetalle As ImageButton = CType(e.Item.FindControl("ibtDetalle"), ImageButton)
                Dim objlblNumOC As Label = CType(e.Item.FindControl("lblNumDoc"), Label)
                Dim objlblNumRq As Label = CType(e.Item.FindControl("lblNumRq"), Label)
                Dim objlblNumAdj As Label = CType(e.Item.FindControl("lblNumAdj"), Label)
                Dim objlblTipo As Label = CType(e.Item.FindControl("lblTipo"), Label)

                ' boton adjuntar
                If objlblNumAdj.Text = "0" Then
                    btnVerAdjuntos.Visible = False
                End If

                strNumOC = Trim(objlblNumOC.Text)
                strNumRq = Trim(objlblNumRq.Text)

                If objlblTipo.Text = "OC" Then
                    btnVerAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntosOC('" + strNumOC + "')")
                Else
                    btnVerAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos('" + strNumRq + "')")
                End If

                btnEnviaCorreo.Attributes.Add("onclick", "javascript:return fnc_EnviaCorreo('" + strNumOC + "')")
                btnDetalle.Attributes.Add("onClick", "javascript:return VerDetalle('" + strNumOC + "')")
                btnGrupo.Attributes.Add("Onclick", "fnc_BuscarGrupo('" + intSec.ToString + "', '" + "" + Trim(objlblTipo.Text) + "', '" + "" + strNumOC + "')")
                'lobjCotizar.Attributes.Add("onClick", "fnc_aprobarmasivo(this,'" + strNumOC + "')")
                intSec = intSec + 1

                'lobjCotizar = Nothing
                btnVerAdjuntos = Nothing
                btnEnviaCorreo = Nothing
                btnDetalle = Nothing
                ldrvDatos = Nothing

        End Select
    End Sub
#End Region
    
End Class