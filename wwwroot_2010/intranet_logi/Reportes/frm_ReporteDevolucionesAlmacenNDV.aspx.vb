Public Class frm_ReporteDevolucionesAlmacenNDV
    Inherits System.Web.UI.Page

    Private Sub frm_ReporteDevolucionesAlmacenNDV_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
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

    End Sub

#Region "Metodos"
    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strFe_Inic As String
        Dim strFe_Fina As String

        Try
            strFe_Inic = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
            strFe_Fina = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)

            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRevisionFinal")

            strURL = strURL + strPath + "rfi_detalledevoluciones_almacen"
            strURL = strURL & "&pvch_FechaInicio=" & strFe_Inic
            strURL = strURL & "&pvch_FechaFin=" & strFe_Fina
            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
#End Region
#Region "Funciones"
    Private Function fnc_ValidaDatos() As Boolean

        If txtFechaIni.Text = "" Then
            lblMsg.Text = "Seleccione una fecha inicio"
            txtFechaIni.Focus()
            Return False
        End If

        If txtFechaIni.Text = "" Then
            lblMsg.Text = "Seleccione una fecha fin"
            txtFechaFin.Focus()
            Return False
        End If

        Return True
    End Function
#End Region

#Region "Eventos"
    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        lblMsg.Text = ""
        If fnc_ValidaDatos() Then
            sMostrarReporte()
        End If
    End Sub

#End Region

End Class