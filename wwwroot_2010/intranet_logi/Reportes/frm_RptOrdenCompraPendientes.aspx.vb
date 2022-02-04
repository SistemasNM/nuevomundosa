Public Class frm_RptOrdenCompraPendientes
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "DGAMARRA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        If Not fValidaFiltros() Then Exit Sub
        sMostrarReporte()
    End Sub
    Private Function fValidaFiltros() As Boolean

        Dim validation As Boolean
        Dim mensaje = ""
        validation = True

        Me.lblMensaje.Text = ""

        lblMensaje.ForeColor = Drawing.Color.Red

        If txtFechaIniOC.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Inicial de Busqueda ! <br>"
            validation = False
        End If

        If txtFechaFinOC.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Final de Busqueda ! <br>"
            validation = False
        End If
        If Not IsDate(Me.txtFechaIniOC.Text) Then
            mensaje += "La Fecha Inicial de Busqueda es incorrecta !<br>"
            validation = False
        End If
        If Not IsDate(Me.txtFechaFinOC.Text) Then
            mensaje += "La Fecha Final de Busqueda es incorrecta !<br>"
            validation = False
        End If
        If IsDate(Me.txtFechaIniOC.Text) And IsDate(Me.txtFechaFinOC.Text) Then
            If CDate(Me.txtFechaIniOC.Text) > CDate(Me.txtFechaFinOC.Text) Then
                mensaje += "La Fecha Inicial de Busqueda no puede ser mayor a la Fecha Final de Busqueda !<br>"
                validation = False
            End If
        End If




        If Not validation Then
            Me.lblMensaje.Text = mensaje
        End If

        Return validation

    End Function
    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strFe_Inic As String
        Dim strFe_Fina As String
        strFe_Inic = Right(txtFechaIniOC.Text, 4) + Mid(txtFechaIniOC.Text, 4, 2) + Left(txtFechaIniOC.Text, 2)
        strFe_Fina = Right(txtFechaFinOC.Text, 4) + Mid(txtFechaFinOC.Text, 4, 2) + Left(txtFechaFinOC.Text, 2)
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

        strURL = strURL + strPath + "log_ordenes_compra_pendientes"
        strURL = strURL & "&vch_fechaini=" & txtFechaIniOC.Text
        strURL = strURL & "&vch_fechafin=" & txtFechaFinOC.Text
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub
End Class