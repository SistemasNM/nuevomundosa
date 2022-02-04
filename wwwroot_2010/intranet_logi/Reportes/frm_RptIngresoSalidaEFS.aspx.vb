Public Class frm_RptIngresoSalidaEFS
    Inherits System.Web.UI.Page

    Private Sub frm_RptIngresoSalidaEFS_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        'Session("@USUARIO") = "LALANOCA"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtFechaIni.Text = "01/" + IIf(Now.Month < 10, "0" + Now.Month.ToString(), Now.Month.ToString()) + "/" + Now.Year.ToString()
            txtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        If fnc_ValidaCampos() Then
            sMostrarReporte()
        End If

    End Sub

    Private Function fnc_ValidaCampos() As Boolean
        If rdlReportes.SelectedValue = 2 Or rdlReportes.SelectedValue = 3 Then
            If txtFechaIni.Text = "" Then
                lblMensaje.Text = "Debe seleccionar una fecha inicio."
                txtFechaIni.Focus()
                Return False
            End If
            If txtFechaFin.Text = "" Then
                lblMensaje.Text = "Debe seleccionar una fecha fin."
                txtFechaFin.Focus()
                Return False
            End If
            Return True
        Else
            Return True
        End If
    End Function

    Private Sub sMostrarReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath
        'CAMBIO DG FIN
        txtUrl.Text = strURL

        Select Case rdlReportes.SelectedValue
            Case 1 : strURL = strURL + "log_movimiento_efs_ingresos"
            Case 2 : strURL = strURL + "log_movimiento_efs_salidas"
            Case Else : strURL = ""
        End Select

        If txtFechaIni.Text <> "" Then
            strURL = strURL + "&dtm_FechaIni=" + Format(CDate(txtFechaIni.Text), "yyyyMMdd")
        Else
            strURL = strURL + "&dtm_FechaIni="
        End If

        If txtFechaFin.Text <> "" Then
            strURL = strURL + "&dtm_FechaFin=" + Format(CDate(txtFechaFin.Text), "yyyyMMdd")
        Else
            strURL = strURL + "&dtm_FechaFin="
        End If

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
End Class