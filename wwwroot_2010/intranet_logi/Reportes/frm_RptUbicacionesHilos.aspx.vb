Public Class frm_RptUbicacionesHilos
    Inherits System.Web.UI.Page

    Private Sub frm_RptUbicacionesHilos_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            'Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "LALANOCA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddl_Almacen.SelectedValue = "007"
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click

        sMostrarReporte()
    End Sub


    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strCodAlmacen As String
        strCodAlmacen = ddl_Almacen.SelectedValue.ToString

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloHilanderia")

        strURL = strURL + strPath + "hil_listado_ubicaciones_capacidad"
        strURL = strURL & "&pvch_Almacen=" & strCodAlmacen
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub






End Class
