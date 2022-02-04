Imports NM_General
Public Class frm_Consulta_Escolaridad_Prestamo
    Inherits System.Web.UI.Page

    Private Sub frm_Consulta_Escolaridad_Prestamo_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DGAMARRA"

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If chkEscolaridad.Checked = True Then
                pnlEscolaridad.Visible = True
                pnlPrestamo.Visible = False
            Else
                pnlEscolaridad.Visible = False
                pnlPrestamo.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim objLogistica As New NM_Logistica
        Dim dt As New DataTable
        If chkEscolaridad.Checked = True Then
            pnlEscolaridad.Visible = True
            pnlPrestamo.Visible = False
            dt = objLogistica.ObtenerListadoPersonalEscolaridad(ddlAno.SelectedValue)
            grdEscolaridad.DataSource = dt
            grdEscolaridad.DataBind()
            
        Else
            pnlEscolaridad.Visible = False
            pnlPrestamo.Visible = True
            dt = objLogistica.ObtenerListadoPersonalPrestamo(ddlAno.SelectedValue)
            grdPrestamo.DataSource = dt
            grdPrestamo.DataBind()
            grdEscolaridad.DataSource = Nothing
            grdEscolaridad.DataBind()
        End If
    End Sub

    Protected Sub chkEscolaridad_CheckedChanged(sender As Object, e As EventArgs) Handles chkEscolaridad.CheckedChanged
        If chkEscolaridad.Checked = True Then
            chkPrestamo.Checked = False
            pnlEscolaridad.Visible = True
            pnlPrestamo.Visible = False
            grdPrestamo.DataSource = Nothing
            grdPrestamo.DataBind()
        Else
            chkPrestamo.Checked = True
            chkEscolaridad.Checked = False
            pnlEscolaridad.Visible = False
            pnlPrestamo.Visible = True
            grdEscolaridad.DataSource = Nothing
            grdEscolaridad.DataBind()
        End If
    End Sub

    Protected Sub chkPrestamo_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrestamo.CheckedChanged
        If chkPrestamo.Checked = True Then
            chkEscolaridad.Checked = False
            pnlEscolaridad.Visible = False
            pnlPrestamo.Visible = True
        Else
            chkPrestamo.Checked = False
            chkEscolaridad.Checked = True
            pnlEscolaridad.Visible = True
            pnlPrestamo.Visible = False
        End If
    End Sub

    Private Sub grdEscolaridad_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEscolaridad.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                
                Dim btnDetalle As ImageButton = CType(e.Row.FindControl("btnDetalle"), ImageButton)
                Dim ruta As String = "http://200.60.99.228/EnlaceNM_Extranet/Procesos/frmAsignacionEscolar.aspx?Usuario="
                ruta = ruta + e.Row.DataItem("Login") + "&IdFlg=" + "visitante"
                btnDetalle.Attributes.Add("onClick", "javascript:return showFormularioEscolaridad('" + ruta + "')")
            End If
        End If
    End Sub

    Private Sub grdPrestamo_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPrestamo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                
                Dim btnDetalle As ImageButton = CType(e.Row.FindControl("btnDetalle"), ImageButton)
                Dim ruta As String = "http://200.60.99.228/EnlaceNM_Extranet/Procesos/frmPrestamoEscolar.aspx?Usuario="
                ruta = ruta + e.Row.DataItem("Login") + "&IdFlg=" + "visitante"
                btnDetalle.Attributes.Add("onClick", "javascript:return showFormularioEscolaridad('" + ruta + "')")
            End If
        End If
    End Sub

    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        sMostrarReporte()
    End Sub
    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strPeriodo As String
        Dim strFlg As String = ""

        strPeriodo = ddlAno.SelectedValue
        If chkEscolaridad.Checked = True Then
            strFlg = "E"
        Else
            strFlg = "P"
        End If

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath + "log_escolaridad_prestamo"
        strURL = strURL & "&var_anio=" & strPeriodo
        strURL = strURL & "&var_flg=" & strFlg
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub
End Class