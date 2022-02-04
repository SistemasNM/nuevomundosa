Public Class frmTiposAprobacion_2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents dgDatos As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    'Protected WithEvents txtNombre As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

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

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim lstrCodigo As String
        Dim lstrNombre As String
        lstrCodigo = Request("strCodigo")
        lstrNombre = Request("strNombre")
        txtCodigo.Text = lstrCodigo
        txtNombre.Text = lstrNombre
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("CO_APRO") & "', '" & drvDatos("DE_APRO") & "')")
        End If
    End Sub

    Private Sub BindGrid()
        Dim ldtRes As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Try
            Dim lstrParams() As String = {"var_Codigo", txtCodigo.Text.Trim, _
                                            "var_Nombre", txtNombre.Text.Trim}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            ldtRes = lobjCon.ObtenerDataTable("usp_LOG_Requisiciones_TiposAprobacionListar", lstrParams)
            dgDatos.DataSource = ldtRes
            dgDatos.DataBind()
        Catch ex As Exception

        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub

End Class