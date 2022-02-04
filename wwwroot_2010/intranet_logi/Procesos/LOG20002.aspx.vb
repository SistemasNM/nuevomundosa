Public Class LOG20002
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDesde As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHasta As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents cbxDesde As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbxHasta As System.Web.UI.WebControls.CheckBox
    Protected WithEvents imgDesde As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents imgHasta As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents ddlEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdn1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents rbtTodos As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtArticulos As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtServicios As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtSolicitador As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSolicitada As System.Web.UI.WebControls.Label
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSolicitador As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents Posicionador1 As NM.Posicionador.Posicionador
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel3 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel4 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel5 As System.Web.UI.WebControls.Panel
    Protected WithEvents hdnusuencryptado As System.Web.UI.HtmlControls.HtmlInputHidden

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

        'Session("@USUARIO") = "IGOYBURU"
        'Session("@USUARIO") = "VVALENCI" '   "RZUNIGA" '"MLEUNG"  '"DARWIN"    '
        Session("@EMPRESA") = "01"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

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
            Call SetearFechas()

            'readonly
            txtDesde.Attributes.Add("readonly", "readonly")
            txtHasta.Attributes.Add("readonly", "readonly")

        End If
        'If Me.hdn1.Value = "1" Then
        '  Listar()
        '  Me.hdn1.Value = ""
        'End If
    End Sub

    Private Sub cbxDesde_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxDesde.CheckedChanged
        If cbxDesde.Checked Then
            txtDesde.Visible = True
            Me.imgDesde.Visible = True
        Else
            txtDesde.Visible = False
            Me.imgDesde.Visible = False
        End If
    End Sub

    Private Sub cbxHasta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxHasta.CheckedChanged
        If cbxHasta.Checked Then
            txtHasta.Visible = True
            Me.imgHasta.Visible = True
        Else
            txtHasta.Visible = False
            Me.imgHasta.Visible = False
        End If
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Call Listar()
    End Sub

    Private Sub DataGrid1_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If e.Item.ItemIndex <> -1 Then
            Dim btnBuscar As ImageButton
            btnBuscar = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
            If Not btnBuscar Is Nothing Then
                AddHandler btnBuscar.Click, AddressOf btnBuscar_Click
            End If
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call Listar()
    End Sub


    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
                Select Case ldrvVista("Tipo")
                    Case "VALE"
                        'lobjBoton.Attributes.Add("onClick", " BuscarPedido('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                        lobjBoton.Attributes.Add("onClick", "javascript:return BuscarPedido('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                    Case "O/S"
                        'lobjBoton.Attributes.Add("onClick", "BuscarOS('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                        lobjBoton.Attributes.Add("onClick", "javascript:return BuscarOS('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                    Case "CONFORMIDAD"
                        'Modificacion: Se agrega el caso de Conformidad de la OS 
                        'Modificado por David Gamarra Paredes
                        'Se agrega la opcion para aprobar la conformidad de la OS
                        lobjBoton.Attributes.Add("onClick", "javascript:return BuscarConformidad('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                    Case Else
                        'lobjBoton.Attributes.Add("onClick", "BuscarRequisicion('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                        lobjBoton.Attributes.Add("onClick", "javascript:return BuscarRequisicion('" + ldrvVista("NumeroRequisicion") + "','" + ldrvVista("DescripcionEstado") + "')")
                End Select
                lobjBoton = Nothing
                ldrvVista = Nothing
        End Select
    End Sub

#End Region

#Region "-- Metodos --"

    Private Sub Listar()
        Dim lobjAprobacion As OFISIS.OFILOGI.Requisiciones
        Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Conversion
        Dim lstrFechaDesde As String = Format(lobjUtil.TextoAFecha(txtDesde.Text), "yyyyMMdd")
        Dim lstrFechaHasta As String = Format(lobjUtil.TextoAFecha(txtHasta.Text), "yyyyMMdd")
        lobjUtil = Nothing
        lobjAprobacion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjAprobacion.Listar(OFISIS.OFILOGI.Requisiciones.enuTiposLista.Aprobaciones, ddlEstado.SelectedValue, IIf(cbxDesde.Checked, lstrFechaDesde, ""), IIf(cbxHasta.Checked, lstrFechaHasta, ""), txtSolicitador.Text, _
            IIf(rbtTodos.Checked, "T", IIf(rbtArticulos.Checked, "C", "S")), txtObservaciones.Text.Trim)
        DataGrid1.DataSource = lobjAprobacion.Tabla()
        DataGrid1.DataBind()
    End Sub

    Private Sub SetearFechas()
        Dim lobjUtil As New NuevoMundo.Generales.RutinasGlobales.Varios
        txtDesde.Text = Format(lobjUtil.PrimerDiaMes(Now.Year, Now.Month), "dd/MM/yyyy")
        txtHasta.Text = Format(lobjUtil.UltimoDiaMes(Now.Year, Now.Month), "dd/MM/yyyy")
        lobjUtil = Nothing
    End Sub

#End Region

End Class
