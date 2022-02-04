
Imports System.Data.SqlClient
Imports NuevoMundo
Imports System.Data
Imports System.IO
Public Class frm_AnulaciondeDocumentos
    Inherits System.Web.UI.Page



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoReq As System.Web.UI.WebControls.TextBox
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
        Session("@EMPRESA") = "01"
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
        If Not IsPostBack Then
            txtSerie.Attributes.Add("onBlur", "FormatearNumero(1);")
            txtNumero.Attributes.Add("onBlur", "FormatearNumero(2);")
        Else
            'sListadoServicios()
        End If
    End Sub

    Private Sub MostrarCabecera()
        Dim strTipoDoc As String = ddlTipoDoc.SelectedValue
        Dim strDocumento As String = txtSerie.Text + "-" + txtNumero.Text

        Dim lobjDocumentos As New clsAlmacen
        Dim dtbRegistro As DataTable
        lobjDocumentos.obtenerDatosDocumento(dtbRegistro, strTipoDoc, strDocumento)
        If dtbRegistro.Rows.Count > 0 Then
            For Each dtrRegistro As DataRow In dtbRegistro.Rows
                txtFecha.Text = dtrRegistro("FE_EMIS")
                txtAuxiliar.Text = dtrRegistro("NO_AUXI")
                txtEstado.Text = dtrRegistro("TI_SITU")
            Next
        Else
            lblMensaje.Text = "El Dosumento no existe o no esta permitido anular!!!"
        End If
    End Sub

    Private Sub Mostrardetalle()
        Dim strTipoDoc As String = ddlTipoDoc.SelectedValue
        Dim strDocumento As String = txtSerie.Text + "-" + txtNumero.Text
        Dim lobjAlmacen As New clsAlmacen
        dgDetalle.DataSource = lobjAlmacen.ObtenerDetalleDocumento(strTipoDoc, strDocumento)
        dgDetalle.DataBind()
    End Sub
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        lblMensaje.Text = ""
        ' LimpiarObjetos()
        MostrarCabecera()
        Mostrardetalle()
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        Dim strTipoDoc As String = ddlTipoDoc.SelectedValue
        Dim strDocumento As String = txtSerie.Text + "-" + txtNumero.Text
        Dim ldtbResultado As DataTable
        Dim lobjDocumentos As New clsAlmacen
        ldtbResultado = lobjDocumentos.AnularDocumento(Session("@EMPRESA"), strTipoDoc, strDocumento, Session("@USUARIO"))
        If Not ldtbResultado Is Nothing Then
            If (ldtbResultado.Rows(0).Item("error").ToString.Length > 0) Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + ldtbResultado.Rows(0).Item("error").ToString + "');</script>")
                Exit Sub
            End If
        End If
        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + strTipoDoc + "  " + strDocumento + " Se anulo correctamente" + "');</script>")
        Call LimpiarObjetos()
        ldtbResultado = Nothing
    End Sub
    Private Sub LimpiarObjetos()
        txtFecha.Text = ""
        txtAuxiliar.Text = ""
        txtEstado.Text = ""
        txtSerie.Text = ""
        txtNumero.Text = ""
        dgDetalle.Visible = False
    End Sub
    
End Class