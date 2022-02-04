Public Class frm_MovimientoxUbicacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("@EMPRESA") = "01"
        ' Session("@USUARIO") = "BENITO"

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
    Private Function ValidarFecha() As Boolean
        If (txtFecha_Inicio.Text = "") Then
            Me.txtFecha_Inicio.Focus()
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Debe elegir una fecha de inicio');</script>")
            Return False
        End If

        If (txtFecha_Final.Text = "") Then
            Me.txtFecha_Final.Focus()
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Debe elegir fecha fin');</script>")
            Return False
        End If

        Dim FechIni As Date = CType(txtFecha_Inicio.Text, Date)
        Dim FechFin As Date = CType(txtFecha_Final.Text, Date)
        Dim result As Integer = DateTime.Compare(FechIni, FechFin)

        If (result > 0) Then
            Me.txtFecha_Inicio.Focus()
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('La fecha de inicio no debe ser mayor a la fecha Fin');</script>")
            Return False
        End If

        Return True
    End Function

    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strFechaIni As String
        Dim strFechaFin As String
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strFechaIni = Format(CDate(Me.txtFecha_Inicio.Text), "yyyyMMdd")
        strFechaFin = Format(CDate(Me.txtFecha_Final.Text), "yyyyMMdd")
        If ValidarFecha() = True Then
            strURL = strURL + strPath + "log_MovimientoUbicacion"

            strURL = strURL + "&CO_EMPR=" + Session("@EMPRESA")
            strURL = strURL + "&FE_INIC=" + strFechaIni
            strURL = strURL + "&FE_FINA=" + strFechaFin
            strURL = strURL + "&CO_UBIC=" + ddlubicaciones.SelectedValue

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        ListaUbicaciones()
    End Sub

    Private Sub ListaUbicaciones()
        strFechaIni = Format(CDate(Me.txtFecha_Inicio.Text), "yyyyMMdd")
        strFechaFin = Format(CDate(Me.txtFecha_Final.Text), "yyyyMMdd")
        Dim lobj_ubicaciones As New NuevoMundo.clsAlmacen
        Dim ldtb_datos As DataTable = Nothing
        If lobj_ubicaciones.ListarUbicaciones(ldtb_datos, Session("@EMPRESA"), strFechaIni, strFechaFin) = True Then
            ddlubicaciones.DataSource = ldtb_datos
            ddlubicaciones.DataTextField = "CO_UBIC"
            ddlubicaciones.DataValueField = "CO_UBIC"
            ddlubicaciones.DataBind()
            'ddlubicaciones.Items.RemoveAt(0)
            'ddlubicaciones.Items.Insert(0, "-- TODOS --")
        End If

        ldtb_datos = Nothing
        lobj_ubicaciones = Nothing
    End Sub
End Class

