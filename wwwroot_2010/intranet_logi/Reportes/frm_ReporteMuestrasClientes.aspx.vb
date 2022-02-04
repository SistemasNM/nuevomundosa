Public Class frm_ReporteMuestrasClientes
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtFechaIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlVendedor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCodigoCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdbTipo As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents colTitulo As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents rowPeriodo As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblerror As System.Web.UI.WebControls.Label

    

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            Session("@USUARIO") = "Darwin"

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Session("@EMPRESA") = "01"
            LlenarVendedores()
            'Accesos()
        End If
    End Sub
    Private Function LlenarVendedores() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Dim larrParams() As Object
        Dim litmItem As ListItem
        Dim lbooOk As Boolean

        ReDim larrParams(1)
        larrParams(0) = "P_CO_EMPR"
        larrParams(1) = Session("@EMPRESA")
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
            ldtRes = lobjCon.ObtenerDataTable("usp_qry_VendedoresListar", larrParams)
            ddlVendedor.DataSource = ldtRes
            ddlVendedor.DataValueField = "Codigo"
            ddlVendedor.DataTextField = "Nombre"
            ddlVendedor.DataBind()
            litmItem = New ListItem("TODOS LOS VENDEDORES", "-1")
            ddlVendedor.Items.Insert(0, litmItem)
            ddlVendedor.SelectedItem.Selected = False
            litmItem.Selected = True
            litmItem = Nothing
            lbooOk = True
        Catch ex As Exception
            lbooOk = False
        Finally
            ldtRes = Nothing
            litmItem = Nothing
            lobjCon = Nothing
        End Try
        Return lbooOk
    End Function
    Private Sub Accesos()
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
        Try
            Dim lstrParametros() As String = {"P_USUARIO", Session("@USUARIO"), "var_Formulario", "VEN30003_E"}
            ldtRes = lobjCon.ObtenerDataTable("usp_qry_OpcionesMenuListar", lstrParametros)
            If ldtRes.Rows.Count = 1 Then
                'btnExportar.Visible = True
            Else
                'btnExportar.Visible = False
            End If
        Catch ex As Exception
            'btnExportar.Visible = False
        End Try

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click


        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String = ""

        Dim lstrURL As String
        Dim strFechaIni As String
        Dim strFechaFin As String
        Dim StrCodigoVendedor As String
        Dim StrCodigoCliente As String

        If txtFechaIni.Text <> "" And txtFechaIni.Text.Length = 10 Then
            strFechaIni = Right(txtFechaIni.Text, 4) + txtFechaIni.Text.Substring(3, 2) + Left(txtFechaIni.Text, 2)
        Else
            lblerror.Text = "Ingrese la fecha de Inicio válida."
            txtFechaIni.Focus()
            Exit Sub
        End If

        If txtFechaFin.Text <> "" And txtFechaFin.Text.Length = 10 Then
            strFechaFin = Right(txtFechaFin.Text, 4) + txtFechaFin.Text.Substring(3, 2) + Left(txtFechaFin.Text, 2)
        Else
            lblerror.Text = "Ingrese la fecha Fin válida."
            txtFechaFin.Focus()
            Exit Sub
        End If

        StrCodigoVendedor = IIf(ddlVendedor.SelectedValue = -1, "99", ddlVendedor.SelectedValue)
        StrCodigoCliente = IIf(txtCodigoCliente.Text = "", "99999999999", txtCodigoCliente.Text)

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

        strURL = strURL + strPath + "log_despacho_muestras"

        strURL = strURL & "&CO_VEND=" & StrCodigoVendedor
        strURL = strURL & "&CO_CLIE=" & StrCodigoCliente
        strURL = strURL & "&FE_INIC=" & strFechaIni
        strURL = strURL & "&FE_FINA=" & strFechaFin
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        'lstrURL = "../CrystalReports/_Muestras.asp?strEmpresa=" + Session("@EMPRESA") + "&strFechaIni=" + strFechaIni + "&strFechaFin=" + strFechaFin + "&strCodigoVendedor=" + StrCodigoVendedor + "&strCodigoCliente=" + StrCodigoCliente + "&strTipo=" + CStr(3)
        'ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub
End Class
