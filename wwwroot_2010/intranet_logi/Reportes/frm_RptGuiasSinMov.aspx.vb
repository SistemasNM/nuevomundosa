Public Partial Class frm_RptGuiasSinMov
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
            'Session("@USUARIO") = "EPOMA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        If Not fValidaFiltros() Then Exit Sub
        sMostrarReporte()
    End Sub


    Private Sub sMostrarReporte()

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String
        Dim strEstado As String

        strPath = "%2fLogistica%2f"

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        txtUrl.Text = strURL

        If Me.rbListModelos.SelectedItem.Value = 1 Then
            strEstado = "PEN"
        Else
            strEstado = " "
        End If

        strURL = strURL + "Rep_GuiasxRecoger"
        strURL = strURL + "&chr_Empresa=01"
        strURL = strURL + "&dtm_FechaInicio=" + Format(CDate(TxtFechaIni.Text), "yyyyMMdd")
        strURL = strURL + "&dtm_FechaFin=" + Format(CDate(TxtFechaFin.Text), "yyyyMMdd")
        strURL = strURL + "&chr_EstadoDocumento=" + strEstado


        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

    Private Function fValidaFiltros() As Boolean

        Dim validation As Boolean
        Dim mensaje = ""
        validation = True

        Me.lblMsg.Text = ""

        lblMsg.ForeColor = Drawing.Color.Red

        If Me.TxtFechaIni.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Inicial de Busqueda ! <br>"
            validation = False
        End If


        If Me.TxtFechaFin.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Final de Busqueda ! <br>"
            validation = False
        End If
        If Not IsDate(Me.TxtFechaIni.Text) Then
            mensaje += "La Fecha Inicial de Busqueda es incorrecta !<br>"
            validation = False
        End If

        If Not validation Then
            Me.lblMsg.Text = mensaje
        End If

        Return validation

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.TxtFechaIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
            Me.TxtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If

    End Sub

End Class
