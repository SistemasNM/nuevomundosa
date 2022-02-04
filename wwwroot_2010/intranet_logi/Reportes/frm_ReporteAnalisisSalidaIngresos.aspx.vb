Public Class frm_ReporteAnalisisSalidaIngresos
    Inherits System.Web.UI.Page

    Private Sub frm_ReporteAnalisisSalidaIngresos_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnIngreso_Click(sender As Object, e As EventArgs) Handles btnIngreso.Click
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
            strURL = strURL + strPath + "log_analisis_ingreso_por_tono"

            strURL = strURL + "&FECINI=" + strFechaIni
            strURL = strURL + "&FECFIN=" + strFechaFin
            strURL = strURL + "&ARTI=" + txtArticulo.Text

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If

    End Sub

    Protected Sub btnSalida_Click(sender As Object, e As EventArgs) Handles btnSalida.Click
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
            strURL = strURL + strPath + "log_analisis_salida_por_tono"

            strURL = strURL + "&FECINI=" + strFechaIni
            strURL = strURL + "&FECFIN=" + strFechaFin
            strURL = strURL + "&ARTI=" + txtArticulo.Text

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If
    End Sub

    Protected Sub btnCliente_Click(sender As Object, e As EventArgs) Handles btnCliente.Click

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
            strURL = strURL + strPath + "log_analisis_cliente"

            strURL = strURL + "&FECINI=" + strFechaIni
            strURL = strURL + "&FECFIN=" + strFechaFin
            strURL = strURL + "&ARTI=" + txtArticulo.Text

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If

    End Sub

    Protected Sub btnResumen_Click(sender As Object, e As EventArgs) Handles btnResumen.Click
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
            strURL = strURL + strPath + "log_analisis_ingresos_y_salidas"

            strURL = strURL + "&FECINI=" + strFechaIni
            strURL = strURL + "&FECFIN=" + strFechaFin
            strURL = strURL + "&ARTI=" + txtArticulo.Text

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If
    End Sub
End Class