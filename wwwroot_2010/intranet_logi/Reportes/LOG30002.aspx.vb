Imports NuevoMundo

Public Class LOG30002
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnResumido As System.Web.UI.WebControls.Button
    Protected WithEvents btnExportar As System.Web.UI.WebControls.Button
    Protected WithEvents btnVisualizar As System.Web.UI.WebControls.Button
    Protected WithEvents txtAreaCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCentroCostoCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaInicial As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFinal As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAreaNombre As System.Web.UI.WebControls.Label
    Protected WithEvents lblCentroCostoNombre As System.Web.UI.WebControls.Label
    Protected WithEvents HDN1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN2 As System.Web.UI.HtmlControls.HtmlInputHidden

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
        'Introducir aquí el código de usuario para inicializar la página
        If Not IsPostBack Then
            txtAreaCodigo.Text = ""
            txtCentroCostoCodigo.Text = ""
            LlenarFechas()
            txtAreaCodigo.Attributes.Add("OnChange", "BlanquearArea();")
            txtCentroCostoCodigo.Attributes.Add("OnChange", "BlanquearCC();")
        End If
        lblAreaNombre.Text = Me.HDN1.Value
        lblCentroCostoNombre.Text = Me.HDN2.Value
    End Sub
    Private Sub btnVisualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizar.Click
        Dim lstrFechaDesde As String
        Dim lstrFechaHasta As String
        Dim objGeneral As New NuevoMundo.General
        Dim lstrURL As String

        If txtFechaInicial.Text = "" Then
            lstrFechaDesde = Format(Now, "yyyyMMdd")
        Else
            lstrFechaDesde = objGeneral.ConvertirFecha(txtFechaInicial.Text)
        End If

        If txtFechaFinal.Text = "" Then
            lstrFechaHasta = Format(Now, "yyyyMMdd")
        Else

            lstrFechaHasta = objGeneral.ConvertirFecha(txtFechaFinal.Text)
        End If

        lstrURL = "../CrystalReports/_SalidasAlmacen.asp?strFechaInicio=" + lstrFechaDesde + "&strFechaFin=" + lstrFechaHasta + "&strArea=" + txtAreaCodigo.Text.Trim + "&strCentroCosto=" + txtCentroCostoCodigo.Text.Trim
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub
    Private Sub btnExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim lstrFechaDesde As String
        Dim lstrFechaHasta As String
        Dim objGeneral As New NuevoMundo.General
        Dim lstrArea As String

        If txtFechaInicial.Text = "" Then
            lstrFechaDesde = Format(Now, "yyyyMMdd")
        Else
            lstrFechaDesde = Format(objGeneral.ConvertirFecha(txtFechaInicial.Text), "yyyyMMdd")
        End If

        If txtFechaFinal.Text = "" Then
            lstrFechaHasta = Format(Now, "yyyyMMdd")
        Else
            lstrFechaHasta = Format(objGeneral.ConvertirFecha(txtFechaFinal.Text), "yyyyMMdd")
        End If

        lstrArea = txtAreaCodigo.Text.Trim

        'EXEC(SP_SALIDAS_ALMACEN) '20040101','20040131','9','903'


        '20120918 EPM - Se comenta no existe la ruta
        'Response.Redirect("http://servnm01/reporteador/reporte.asp?rn=salidas_almacene.rpt&sp=SP_SALIDAS_ALMACEN&bd=OFILOGI&srv=[SERVBD02\NMUNDO02]&NumPar=1111&P1=" + lstrFechaDesde + "&P2=" + lstrFechaHasta + "&P3=9&P4=" + lstrArea)

    End Sub
    Private Sub btnResumido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResumido.Click
        Dim lstrFechaDesde As String
        Dim lstrFechaHasta As String
        Dim objGeneral As New NuevoMundo.General
        Dim lstrArea As String

        If txtFechaInicial.Text = "" Then
            lstrFechaDesde = Format(Now, "yyyyMMdd")
        Else
            lstrFechaDesde = Format(objGeneral.ConvertirFecha(txtFechaInicial.Text), "yyyyMMdd")
        End If

        If txtFechaFinal.Text = "" Then
            lstrFechaHasta = Format(Now, "yyyyMMdd")
        Else
            lstrFechaHasta = Format(objGeneral.ConvertirFecha(txtFechaFinal.Text), "yyyyMMdd")
        End If

        lstrArea = txtAreaCodigo.Text.Trim

        '20120918 EPM - Se comenta no existe la ruta
        'Response.Redirect("http://servnm01/reporteador/reporte.asp?rn=salidas_almacend.rpt&sp=SP_SALIDAS_ALMACEN&bd=OFILOGI&srv=[SERVBD02\NMUNDO02]&NumPar=1111&P1=" + lstrFechaDesde + "&P2=" + lstrFechaHasta + "&P3=D&P4=" + lstrArea)

    End Sub
    Private Sub LlenarFechas()
        Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Varios
        If txtFechaInicial.Text = "" And txtFechaFinal.Text = "" Then
            txtFechaInicial.Text = Format(lobjUtil.PrimerDiaMes, "dd/MM/yyyy")
            txtFechaFinal.Text = Format(lobjUtil.UltimoDiaMes, "dd/MM/yyyy")
        End If
        lobjUtil = Nothing
    End Sub


    Private Sub txtAreaCodigo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAreaCodigo.TextChanged
    'txtAreaCodigo.Text = "sdf"
    End Sub
End Class
