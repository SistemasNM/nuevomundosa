Public Class frm_ReporteLotesxArticulo
    Inherits System.Web.UI.Page

    Private Sub frm_ReporteLotesxArticulo_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
            Me.TxtFechaIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
            Me.TxtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If
        btnBuscar.Attributes.Add("style", "cursor: pointer;")



    End Sub


    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        lblMsg.Text = ""

        If TxtFechaIni.Text = "" Then
            lblMsg.Text = "Debe seleccionar la Fecha de Inicio."
            TxtFechaIni.Focus()
        ElseIf TxtFechaFin.Text = "" Then
            lblMsg.Text = "Debe seleccionar la Fecha Final."
            TxtFechaFin.Focus()
        ElseIf txtArticulo.Text.Length <> 4 Then
            lblMsg.Text = "Debe ingresar un Articulo de 4 digitos."
            txtArticulo.Focus()
        Else

            Call sMostrarReporte()
        End If
    End Sub


    Private Sub sMostrarReporte()

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        'CAMBIO DG INI

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath
        txtUrl.Text = strURL

        strURL = strURL + "log_detalle_lotes_x_articulo"

        strURL = strURL + "&pvch_FechaIni=" + Format(CDate(TxtFechaIni.Text), "yyyyMMdd")
        strURL = strURL + "&pvch_FechaFin=" + Format(CDate(TxtFechaFin.Text), "yyyyMMdd")
        strURL = strURL + "&pvch_Articulo=" + txtArticulo.Text


        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
End Class