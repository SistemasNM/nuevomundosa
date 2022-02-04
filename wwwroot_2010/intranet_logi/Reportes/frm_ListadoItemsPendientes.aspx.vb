Public Class frm_ListadoItemsPendientes
  Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents txtSerie As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumeroDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCentroCosto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesCentroCosto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodActivo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesActivo As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdlDocumentos As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents ddlPrioridad As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFecIns As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents chkStock As System.Web.UI.WebControls.CheckBox

    Private designerPlaceholderDeclaration As System.Object

    Dim strMensaje As String = ""

    ' Init
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "atorresc"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If

        InitializeComponent()
    End Sub

#End Region

  ' Load
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not Page.IsPostBack Then
            txtSerie.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
            txtNumeroDocumento.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
        End If
    End Sub

  ' boton Buscar
    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        Try
            lblError.Text = ""
            ValidarParametrosConsulta()
            If strMensaje.Length = 0 Then
                fnc_VerReportePendientes()
            Else
                lblError.Text = strMensaje
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Procedimientos"

  ' Validacion de parametros
    Public Sub ValidarParametrosConsulta()
        If rdlDocumentos.Items(0).Selected = False And rdlDocumentos.Items(1).Selected = False And rdlDocumentos.Items(2).Selected = False And rdlDocumentos.Items(3).Selected = False And rdlDocumentos.Items(4).Selected = False Then
            strMensaje = "Debe elegir un tipo de documento a consultar."
            Exit Sub
        End If
        If (Trim(txtFechaIni.Text).Length > 0 And Trim(txtFechaFin.Text).Length = 0) _
            Or (Trim(txtFechaIni.Text).Length = 0 And Trim(txtFechaFin.Text).Length > 0) Then
            strMensaje = "Debe ingresar correctamente las fechas."
            Exit Sub
        End If
        If Trim(txtFechaIni.Text).Length > 0 And Trim(txtFechaFin.Text).Length > 0 Then
            If CDate(txtFechaIni.Text) > CDate(txtFechaFin.Text) Then
                strMensaje = "La fecha inicial debe ser meyor o igual a la fecha final."
                Exit Sub
            End If
        End If
        If Trim(txtDesCentroCosto.Text).Length > 0 And Trim(txtDesCentroCosto.Text).Length = 0 Then
            strMensaje = "Eliga un centro de costo valido."
            Exit Sub
        End If
        If Trim(txtCodActivo.Text).Length > 0 And Trim(txtDesActivo.Text).Length = 0 Then
            strMensaje = "Eliga un Activo/CTC valido."
            Exit Sub
        End If
        If Trim(txtFechaIni.Text).Length = 0 And Trim(txtFechaFin.Text).Length = 0 _
            And Trim(txtCodCentroCosto.Text).Length = 0 _
            And Trim(txtNumeroDocumento.Text).Length = 0 _
            And Trim(txtCodActivo.Text).Length = 0 Then
            strMensaje = "Ingrese parametros para la consulta."
            Exit Sub
        End If

        If Trim(txtFecIns.Text).Length > 0 Then
            If Date.TryParse(txtFecIns.Text, CDate(txtFecIns.Text)) = False Then
                strMensaje = "Debe ingresar correctamente las fechas de instalacion."
                Exit Sub
            End If
        End If
    End Sub

  ' Reporte
    Private Sub fnc_VerReportePendientes()
        Dim strTipo As String = ""
        Dim strFechaInicial As String = ""
        Dim strFechaFinal As String = ""
        Dim strCentroCostos As String = ""
        Dim strActivoCTC As String = ""
        Dim strNumeroDocumento As String = ""
        Dim strTipoPedido As String = ""
        Dim strFechaInstalacion As String = ""
        Dim strStock As String = ""

        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strReporte As String = ""
        strTipo = rdlDocumentos.SelectedValue.ToString
        strFechaInicial = Format(CDate(txtFechaIni.Text), "yyyyMMdd")
        strFechaFinal = Format(CDate(txtFechaFin.Text), "yyyyMMdd")
        strCentroCostos = Trim(txtCodCentroCosto.Text)
        strActivoCTC = Trim(txtCodActivo.Text)
        strNumeroDocumento = Trim(txtNumeroDocumento.Text)
        strTipoPedido = Trim(ddlPrioridad.SelectedValue.ToString)
        If Trim(txtFecIns.Text).Length > 0 Then
            strFechaInstalacion = Format(CDate(txtFecIns.Text), "yyyyMMdd")
        Else
            strFechaInstalacion = ""
        End If
        If chkStock.Checked = True Then
            strStock = "1"
        Else
            strStock = "0"
        End If

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        'strURL = strURL + "logistica_DocumentosPendientesAtender"
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strReporte = "log_documentos_pendientes_atender"
        strURL = strURL + strPath + strReporte
        'CAMBIO DG FIN
        strURL = strURL + "&chrTipo=" + strTipo
        strURL = strURL + "&vch_FecIni=" + strFechaInicial
        strURL = strURL + "&vch_FecFin=" + strFechaFinal
        strURL = strURL + "&vch_CentroCosto=" + strCentroCostos
        strURL = strURL + "&vch_ActivoCTC=" + strActivoCTC
        strURL = strURL + "&vch_NumeroDoc=" + strNumeroDocumento
        strURL = strURL + "&chr_TipoPedido=" + strTipoPedido
        strURL = strURL + "&vch_FechaInstalacion=" + strFechaInstalacion
        strURL = strURL + "&chr_Stock=" + strStock

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub
#End Region

End Class
