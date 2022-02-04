Imports System.Data.SqlClient
Imports NuevoMundo
Imports System.Data
Imports System.IO
Public Class frm_InventarioOtros
    Inherits System.Web.UI.Page



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoReq As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSituacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCtaGasto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescCtaGasto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlmacen As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNuSecu As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnSolicitar As System.Web.UI.WebControls.Button
    Protected WithEvents BtnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnular As System.Web.UI.WebControls.Button
    Protected WithEvents LblSecuenciaS As System.Web.UI.WebControls.Label

#End Region


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "BENITO"

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


        'BindParos()
    End Sub


    Protected Sub txtCodigoUbicacion_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoUbicacion.TextChanged
        MostrarCabecera()
        Mostrardetalle(txtCodigoUbicacion.Text)
    End Sub
    Private Sub MostrarCabecera()
        Dim strCodigoUbicacion As String = txtCodigoUbicacion.Text

        Dim lobjAlmacen As New clsAlmacen
        Dim dtbRegistro As DataTable
        lobjAlmacen.obtenerDatosCabecera(dtbRegistro, strCodigoUbicacion)
        If dtbRegistro.Rows.Count > 0 Then
            For Each dtrRegistro As DataRow In dtbRegistro.Rows
                txtInventariador.Text = dtrRegistro("NO_USUA_INVE")
                txtFecha.Text = dtrRegistro("FE_TOMA_INVE")
                txtAlmacen.Text = dtrRegistro("DE_ALMA")
            Next


        Else
            lblMensaje.Text = "El registro no existe."
        End If
    End Sub

    Private Sub Mostrardetalle(strCodigoUbicacion)
        Dim lobjAlmacen As New clsAlmacen
        dgDetalle.DataSource = lobjAlmacen.obtenerDatosDetalle(strCodigoUbicacion)
        dgDetalle.DataBind()
    End Sub

    Private Sub dgDetalle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetalle.ItemCommand
        If e.CommandName = "Eliminar" Then
            Dim lblNuSecu As Label = e.Item.FindControl("LblSecuenciaS")

            ' Eliminar el elemento seleccioando
            Dim lobjAlmacen As New clsAlmacen
            lobjAlmacen.EliminarRegistroInventario(txtCodigoUbicacion.Text, lblNuSecu.Text)

            ' Si esta en modo Edición
            If dgDetalle.EditItemIndex <> -1 Then
                dgDetalle.EditItemIndex = -1
                dgDetalle.ShowFooter = True
            End If

            Mostrardetalle(txtCodigoUbicacion.Text)
        End If
    End Sub
End Class