Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PlantillaDespacho
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        Session("@EMPRESA") = "01"
        Session("@USUARIO") = "benito"

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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Ajax.Utility.RegisterTypeForAjax(GetType(frm_RegistrarValeAlmacen))
        If Not (Page.IsPostBack) Then
            'En el caso de Busqueda cargamos el pedido
            'btnAgregar.Attributes.Add("onClick", "javascript:return fnc_VerificarDatos();")
            'btnSolicitaAprobacion.Attributes.Add("onClick", "javascript:SolicitarAprobacion();")
            'btnVerSeguimiento.Attributes.Add("onClick", "javascript:btnSeguimiento_Onclick();")
            'btnBuscar.Attributes.Add("onClick", "javascript:return VerConsultaPedido();")
            'txtCantidad.Attributes.Add("onBlur", "javascript:return txtCantidad_onBlur();")
            'txtCantidad.Attributes.Add("onBlur", "javascript:txtCantidad_onBlur();")
        End If
    End Sub
End Class