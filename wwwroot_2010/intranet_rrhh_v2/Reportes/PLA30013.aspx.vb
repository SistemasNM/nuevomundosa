
Public Class PLA30013
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents txtPeriodoIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPeriodoFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrabajador As System.Web.UI.WebControls.TextBox
    Protected WithEvents optAdelantos As System.Web.UI.WebControls.RadioButton
    Protected WithEvents optPrestamos As System.Web.UI.WebControls.RadioButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

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

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
        End If
    End Sub

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
    Dim dFecIng As Date, dFecAct As Date, dFecStop As Date
    Dim aMes() As String = {"", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"}
    Dim strMes As String, iCon As Integer

        'strPath = "%2fNM_Reportes%2f"
        strMes = ""
        'strURL = ""
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer") & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")
        strURL = strURL + strPath

        If optAdelantos.Checked = True Then
            'strURL = strURL + "planilla_adelanto_lista"
            strURL = strURL + "pla_adelanto_lista"
        ElseIf optPrestamos.Checked = True Then
            'strURL = strURL + "planilla_prestamo_lista"
            strURL = strURL + "pla_prestamo_lista"
        End If

    strURL = strURL + "&pint_annoini=" + Left(txtPeriodoIni.Text, 4)
    strURL = strURL + "&pint_mesini=" + Right(txtPeriodoIni.Text, 2)
    strURL = strURL + "&pint_annofin=" + Left(txtPeriodoFin.Text, 4)
    strURL = strURL + "&pint_mesfin=" + Right(txtPeriodoFin.Text, 2)
    strURL = strURL + "&pvch_codtrab=" + txtTrabajador.Text

    dFecIng = CDate(txtPeriodoFin.Text.Replace(".", "-") & "-01")
    dFecStop = CDate(txtPeriodoIni.Text.Replace(".", "-") & "-01")
    For iCon = 0 To 12
      dFecAct = DateAdd(DateInterval.Month, -1 * iCon, dFecIng)
      If (dFecAct >= dFecStop) Then
        strMes = strMes & "&pM" & iCon.ToString & "=" & aMes(Month(dFecAct)).ToUpper & "-" & Format(dFecAct, "yy")
      Else
        strMes = strMes & "&pM" & iCon.ToString & "=" & "."
      End If
    Next iCon

    strURL = strURL + strMes

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

    If lstr_mensaje.Length > 0 Then
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" + lstr_mensaje + "');</script>")
      Exit Function
    End If

    fValidaPeriodo = True

  End Function

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        If fValidaPeriodo() = False Then Exit Sub

        sMostrarReporte()

    End Sub

End Class
