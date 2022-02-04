Public Class PLA30014
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtPeriodoIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPeriodoFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrabajador As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents optresumen As System.Web.UI.WebControls.RadioButton
    Protected WithEvents optdetalle As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtpuescod As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtpuesdes As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtareacod As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtareades As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Eventos --"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    '20120904 EPM Valida que la session este vacio o nula
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
    InitializeComponent()

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'If Not IsPostBack Then

        'End If
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If fValidaPeriodo() = False Then Exit Sub

        sMostrarReporte()

    End Sub

#End Region

#Region "-- Metodos --"


    Private Sub sMostrarReporte()
        '*****************************************************************************************************
        'Objetivo   : Muestra el reporte que esta en el servidor
        'Autor      : EPM
        'Creado     : 00/00/0000
        'Modificado : 00/00/0000
        '*****************************************************************************************************

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        'strPath = "%2fNM_Reportes%2f"
        'strURL = ""
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer") & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")
        strURL = strURL + strPath

        If optdetalle.Checked = True Then
            'strURL = strURL + "planilla_sueldobruto_det"
            strURL = strURL + "pla_sueldobruto_det"
        ElseIf optresumen.Checked = True Then
            'strURL = strURL + "planilla_sueldobruto_res"
            strURL = strURL + "pla_sueldobruto_res"
        End If

        strURL = strURL + "&ptin_tipolista=" + IIf(optdetalle.Checked = True, "1", "2")
        strURL = strURL + "&pvch_area=" + txtareacod.Text
        strURL = strURL + "&pvch_puesto=" + txtpuescod.Text
        strURL = strURL + "&pnum_periodoini=" + txtPeriodoIni.Text
        strURL = strURL + "&pnum_periodofin=" + txtPeriodoFin.Text
        strURL = strURL + "&pvch_trabajador=" + txtTrabajador.Text

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"


        strScript = "<script>fMostrarReporte('" & strURL & "');</script>"
    ClientScript.RegisterStartupScript(Me.[GetType](), "ShowInfo", strScript)

  End Sub

  Private Function fValidaPeriodo() As Boolean

    '*****************************************************************************************************
    'Objetivo   : Valida que año y mes sean ingresado correctamente
    'Autor      : EPM
    'Creado     : 00/00/0000
    'Modificado : 00/00/0000
    '*****************************************************************************************************

    Dim lstr_mensaje As String
    lstr_mensaje = ""
    fValidaPeriodo = False

    If txtPeriodoIni.Text.Length <> 7 Then
      lstr_mensaje = "El periodo ingresado es incorrecto .. !"
    End If
    If txtPeriodoFin.Text.Length <> 7 Then
      lstr_mensaje = "El periodo ingresado es incorrecto .. !"
    End If
    If IsNumeric(Left(txtPeriodoIni.Text, 4)) = False Then
      lstr_mensaje = "El año ingresado es incorrecto .. !"
    End If
    If IsNumeric(Right(txtPeriodoIni.Text, 2)) = False Then
      lstr_mensaje = "El mes ingresado es incorrecto .. !"
    End If
    If IsNumeric(Left(txtPeriodoFin.Text, 4)) = False Then
      lstr_mensaje = "El año ingresado es incorrecto .. !"
    End If
    If IsNumeric(Right(txtPeriodoFin.Text, 2)) = False Then
      lstr_mensaje = "El mes ingresado es incorrecto .. !"
    End If

    'validar que no exceda los 24 meses

    If lstr_mensaje.Length > 0 Then
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" + lstr_mensaje + "');</script>")
      Exit Function
    End If

    fValidaPeriodo = True

  End Function

#End Region

End Class
