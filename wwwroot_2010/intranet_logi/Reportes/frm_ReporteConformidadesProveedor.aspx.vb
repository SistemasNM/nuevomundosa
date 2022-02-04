Public Class frm_ReporteConformidadesProveedor
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        If Page.IsPostBack = False Then

        End If
    End Sub

    Private Sub btnReporte1_Click(sender As Object, e As System.EventArgs) Handles btnReporte1.Click
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        
        If ValidarFecha() = True Then
            If txtCodigoProveedor.Text = "" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Debe elegir un proveedor');</script>")
                Exit Sub
            End If

            strURL = strURL + strPath + "log_reporte_os_promedio_por_proveedor"
            strURL = strURL + "&pvch_CodProv=" + Me.txtCodigoProveedor.Text
            strURL = strURL + "&pcvh_FechIni=" + Me.txtFecha_Inicio.Text
            strURL = strURL + "&pcvh_FechFin=" + Me.txtFecha_Final.Text
            strURL = strURL + "&pvch_CoEmpre=" + Session("@EMPRESA")
            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
        End If

        

    End Sub

    Private Sub btnReporte2_Click(sender As Object, e As System.EventArgs) Handles btnReporte2.Click
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

        If ValidarFecha() = True Then
            strURL = strURL + strPath + "log_reporte_conformidad_evaluacion"
            strURL = strURL + "&pvch_CodProv=" + IIf(Me.txtCodigoProveedor.Text = "", " ", Me.txtCodigoProveedor.Text)
            strURL = strURL + "&pcvh_FechIni=" + Me.txtFecha_Inicio.Text()
            strURL = strURL + "&pcvh_FechFin=" + Me.txtFecha_Final.Text
            strURL = strURL + "&pvch_CoEmpre=" + Session("@EMPRESA")
            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
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
End Class