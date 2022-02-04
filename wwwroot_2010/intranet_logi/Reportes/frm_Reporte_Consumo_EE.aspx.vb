Public Class frm_REporte_Consumo_EE
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
            ' Session("@USUARIO") = "BENITO"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        lblMsg.Text = ""
        If fnc_ValidaDatos() Then
            sMostrarReporte()
        End If

    End Sub

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
        If Not rdbTipoDetallado.Checked Then
            If ddlMaquinas.SelectedIndex < 0 Then
                lblMsg.Text = "Seleccione una maquina"
                ddlMaquinas.Focus()
                Return False
            End If
        End If
        

        Return True
    End Function

    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strFe_Inic As String
        Dim strFe_Fina As String
        Dim strCo_Maq As String

        Try
            strCo_Maq = ddlMaquinas.SelectedValue

            strFe_Inic = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
            strFe_Fina = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)


            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

            If rdbTipoDetallado.Checked Then
                strURL = strURL + strPath + "log_consumo_medidores_EE"
            Else
                strURL = strURL + strPath + "log_consumo_EE"
                strURL = strURL & "&strCo_Maq=" & strCo_Maq
            End If

            strURL = strURL & "&strFe_Inic=" & strFe_Inic
            strURL = strURL & "&strFe_Fina=" & strFe_Fina
            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
        
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call prc_listarmaquinas()
        End If
    End Sub
    Private Sub prc_listarmaquinas()
        Dim lobj_maquinas As New NuevoMundo.clsAlmacen
        Dim ldtb_datos As DataTable = Nothing
        If lobj_maquinas.ListarMaquinas(ldtb_datos) = True Then
            ddlMaquinas.DataSource = ldtb_datos
            ddlMaquinas.DataTextField = "no_maq"
            ddlMaquinas.DataValueField = "co_maq"
            ddlMaquinas.DataBind()
            'ddlMaquinas.Items.RemoveAt(0)
            'ddlmaquinas.Items.Insert(0, "-- TODOS --")
        End If

        ldtb_datos = Nothing
        lobj_maquinas = Nothing
    End Sub

  
    Protected Sub rdbTipoDetallado_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTipoDetallado.CheckedChanged
        ddlMaquinas.Enabled = False
    End Sub

    Protected Sub rdbTipoResumido_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTipoResumido.CheckedChanged
        ddlMaquinas.Enabled = True
    End Sub
End Class