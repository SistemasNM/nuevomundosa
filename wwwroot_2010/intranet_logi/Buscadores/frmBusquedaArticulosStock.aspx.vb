Public Class frmBusquedaArticulosStock
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNombre As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents dgDatos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnAlmacen As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnTipoArticulo As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ''-----------------------------------------------------------------------
        ''--INICIO: VERIFICAR LA SESION
        ''-----------------------------------------------------------------------
        ''20120904 EPM Valida que la session este vacio o nula
        'If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then

        '    If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
        '        Dim objRequest As New BLITZ_LOCK.clsRequest
        '        Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
        '        objRequest = Nothing
        '    End If

        '    Session("@EMPRESA") = "01"
        '    'Session("@USUARIO") = "EPOMA"

        '    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
        '        Response.Redirect("../../intranet/finsesion.htm")
        '    End If
        'End If
        ''-----------------------------------------------------------------------
        ''--FINAL: VERIFICAR LA SESION
        ''-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ("" & Request("pstrAlmacen")) <> "" Then hdnAlmacen.Value = Request("pstrAlmacen")
        If ("" & Request("pstrTipo")) <> "" Then hdnTipoArticulo.Value = Trim(Request("pstrTipo")) Else hdnTipoArticulo.Value = "1"
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Dim lobj As OFISIS.OFILOGI.Articulos
        Dim ldtbDatos As DataTable
        If hdnAlmacen.Value.Trim.Length = 0 Then
            Exit Sub
        End If
        If Me.txtNombre.Text.Trim.Length = 0 And Me.txtCodigo.Text.Trim.Length = 0 Then
            Exit Sub
        End If
        lobj = New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        ldtbDatos = lobj.Listar_ArticulosxTipoLista(hdnTipoArticulo.Value, hdnAlmacen.Value, Me.txtCodigo.Text, Me.txtNombre.Text)
        dgDatos.DataSource = ldtbDatos
        dgDatos.DataBind()
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim strParametros = "'" & drvDatos("co_item") & "', '" & drvDatos("de_item") & "','" & drvDatos("co_unme") & "','" & drvDatos("ca_actu").ToString & "'"
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick(" + strParametros + ")")
        End If
    End Sub

End Class
