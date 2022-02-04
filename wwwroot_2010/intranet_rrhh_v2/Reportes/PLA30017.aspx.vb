Public Class PLA30017
    Inherits System.Web.UI.Page

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If
            Session("@EMPRESA") = "01"
            ' Session("@USUARIO") = "ATORRESC"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If

        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------

        InitializeComponent()
    End Sub


#Region "-- Metodos --"
    Private Sub sMostrarReporte()
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")


        strURL = strURL + strPath + "pla_formulario_2_obm"
        'strURL = strURL + "&CO_EMPR=" + Session("@EMPRESA")
        strURL = strURL + "&NU_ANNO=" + Me.txtAnio.Text
        strURL = strURL + "&NU_MESE=" + ddlMes.SelectedValue
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

#End Region

    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        sMostrarReporte()
    End Sub



End Class
