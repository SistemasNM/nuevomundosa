Public Class LOG30008
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnVerReporte As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipoArt As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlRubro As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkconstocksinmov As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtcodarticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdesarticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbtfiltro1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtfiltro2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents btnbuscararticulo As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents rbtfiltro3 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents chkconstocksinmov_3 As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

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
            'Session("@USUARIO") = "DGAMARRA"

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

        If Not IsPostBack Then
            '20120918 EPM - Readonly
            txtcodarticulo.Attributes.Add("readonly", "readonly")
            txtdesarticulo.Attributes.Add("readonly", "readonly")

            'listar los tipos de articulo
            Call prc_listarcombos(1, "", "", "", "")
            'nota: para el caso de ecastillo se puede colocar los codigos x default
            ddlTipoArt.SelectedValue = "03"
            Call prc_listarcombos(2, "03", "", "", "")
            ddlRubro.SelectedValue = "02"
            Call prc_listarcombos(3, "03", "02", "", "")
        End If

    End Sub

    Private Sub ddlTipoArt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipoArt.SelectedIndexChanged
        Call prc_listarcombos(2, ddlTipoArt.SelectedValue, "", "", "")

    End Sub

    Private Sub ddlRubro_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlRubro.SelectedIndexChanged
        Call prc_listarcombos(3, ddlTipoArt.SelectedValue, ddlRubro.SelectedValue, "", "")
    End Sub

    Private Sub btnVerReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerReporte.Click
        If ddlFamilia.SelectedValue.Length > 0 Then
            Call sMostrarReporte()
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
        Dim strMes As String ', iCon As Integer

        'strPath = "%2flogistica%2f"
        strMes = ""
        strURL = ""
        'CAMBIO DG INI
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath
        'CAMBIO DG FIN

        If rbtfiltro3.Checked = True Then
            'strURL = strURL + "logistica_analisisconsumo2"
            strURL = strURL + "log_analisis_consumo2"
        Else
            'strURL = strURL + "logistica_analisisconsumo1"
            strURL = strURL + "log_analisis_consumo1"
        End If

        strURL = strURL + "&ptin_tipoconsulta=" & IIf(rbtfiltro1.Checked = True, 1, IIf(rbtfiltro2.Checked = True, 2, 3))
        strURL = strURL + "&pvch_param1=" + IIf(rbtfiltro1.Checked = True, ddlTipoArt.SelectedValue, IIf(rbtfiltro2.Checked = True, txtcodarticulo.Text, "00"))
        strURL = strURL + "&pvch_param2=" + IIf(rbtfiltro1.Checked = True, ddlRubro.SelectedValue, "00")
        strURL = strURL + "&pvch_param3=" + IIf(rbtfiltro1.Checked = True, ddlFamilia.SelectedValue, "00")
        strURL = strURL + "&pvch_param4=" + "00"
        strURL = strURL + "&pvch_param5=" + IIf(rbtfiltro1.Checked = True, IIf(chkconstocksinmov.Checked = True, "1", "0"), IIf(rbtfiltro3.Checked = True, IIf(chkconstocksinmov_3.Checked = True, "1", "0"), "0"))
        If rbtfiltro1.Checked = True Or rbtfiltro2.Checked = True Then
            strURL = strURL + "&pvch_tipoart=" + IIf(rbtfiltro1.Checked = True, ddlTipoArt.SelectedItem.Text.ToUpper, "--")
            strURL = strURL + "&pvch_rubro=" + IIf(rbtfiltro1.Checked = True, ddlRubro.SelectedItem.Text.ToUpper, "--")
            strURL = strURL + "&pvch_familia=" + IIf(rbtfiltro1.Checked = True, ddlFamilia.SelectedItem.Text.ToUpper, "--")
        End If
        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "<script>fMostrarReporte('" & strURL & "');</script>"
        ClientScript.RegisterStartupScript(Me.[GetType](), "ShowInfo", strScript)

    End Sub

    Private Sub prc_listarcombos(ByVal pint_tipolista As Int16, ByVal pstr_tipoarticulo As String, ByVal pstr_rubro As String, ByVal pstr_familia As String, ByVal pstr_subfamilia As String)
        Dim ldtb_datos As New DataTable, lbln_estado As Boolean = False
        Dim lobj_articulo As New NuevoMundo.clsArticulo

        lbln_estado = lobj_articulo.Listar_Caracteristicas(ldtb_datos, pint_tipolista, pstr_tipoarticulo, pstr_rubro, pstr_familia, pstr_subfamilia)
        lobj_articulo = Nothing

        If lbln_estado = False Then
            ldtb_datos = Nothing
            Exit Sub
        End If

        'tipo de articulo
        If pint_tipolista = 1 Then
            ddlFamilia.DataSource = Nothing
            ddlFamilia.DataBind()
            ddlRubro.DataSource = Nothing
            ddlRubro.DataBind()
            ddlTipoArt.DataSource = Nothing
            ddlTipoArt.DataBind()

            ddlTipoArt.DataSource = ldtb_datos
            ddlTipoArt.DataValueField = "ti_item"
            ddlTipoArt.DataTextField = "de_tipo_item"
            ddlTipoArt.DataBind()

            ddlTipoArt.Items.Insert(0, "-- Seleccionar --")
            ddlTipoArt.SelectedIndex = 0
        End If

        'rubro
        If pint_tipolista = 2 Then
            ddlFamilia.DataSource = Nothing
            ddlFamilia.DataBind()
            ddlRubro.DataSource = Nothing
            ddlRubro.DataBind()

            ddlRubro.DataSource = ldtb_datos
            ddlRubro.DataValueField = "co_rubr"
            ddlRubro.DataTextField = "de_rubr"
            ddlRubro.DataBind()

            ddlRubro.Items.Insert(0, "-- Seleccionar --")
            ddlRubro.SelectedIndex = 0
        End If

        'familia
        If pint_tipolista = 3 Then
            ddlFamilia.DataSource = Nothing
            ddlFamilia.DataBind()

            ddlFamilia.DataSource = ldtb_datos
            ddlFamilia.DataValueField = "co_fami"
            ddlFamilia.DataTextField = "de_fami"
            ddlFamilia.DataBind()

            ddlFamilia.Items.Insert(0, "-- Seleccionar --")
            ddlFamilia.SelectedIndex = 0
        End If

        ldtb_datos = Nothing

    End Sub

    Private Sub rbtfiltro1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtfiltro1.CheckedChanged
        Call bloquearcontrolesfiltro()
    End Sub

    Private Sub rbtfiltro2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtfiltro2.CheckedChanged
        Call bloquearcontrolesfiltro()
    End Sub

    Private Sub rbtfiltro3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtfiltro3.CheckedChanged
        Call bloquearcontrolesfiltro()
    End Sub

    Private Sub bloquearcontrolesfiltro()
        If rbtfiltro1.Checked = True Then
            btnbuscararticulo.Disabled = True
            txtcodarticulo.Enabled = False
            txtdesarticulo.Enabled = False
            '----
            ddlTipoArt.Enabled = True
            ddlRubro.Enabled = True
            ddlFamilia.Enabled = True
            chkconstocksinmov.Enabled = True
            '----
            chkconstocksinmov_3.Enabled = False

        ElseIf rbtfiltro2.Checked = True Then
            btnbuscararticulo.Disabled = False
            txtcodarticulo.Enabled = True
            txtdesarticulo.Enabled = True
            '----
            ddlTipoArt.Enabled = False
            ddlRubro.Enabled = False
            ddlFamilia.Enabled = False
            chkconstocksinmov.Enabled = False
            '----
            chkconstocksinmov_3.Enabled = False

        ElseIf rbtfiltro3.Checked = True Then
            btnbuscararticulo.Disabled = True
            txtcodarticulo.Enabled = False
            txtdesarticulo.Enabled = False
            '----
            ddlTipoArt.Enabled = False
            ddlRubro.Enabled = False
            ddlFamilia.Enabled = False
            chkconstocksinmov.Enabled = False
            '----
            chkconstocksinmov_3.Enabled = True

        End If
    End Sub

End Class
