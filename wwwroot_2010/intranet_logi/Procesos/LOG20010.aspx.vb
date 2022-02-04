Public Class LOG20010
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAlternativa1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlternativa2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlternativa3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPrecio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProveedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtArticuloNombre As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtArticuloCodigo As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            '20120906 EPM Readonly
            txtArticuloCodigo.Attributes.Add("readonly", "readonly")
            txtArticuloNombre.Attributes.Add("readonly", "readonly")
            txtPrecio.Attributes.Add("readonly", "readonly")
            txtProveedor.Attributes.Add("readonly", "readonly")
            txtAlternativa1.Attributes.Add("readonly", "readonly")
            txtAlternativa2.Attributes.Add("readonly", "readonly")
            txtAlternativa3.Attributes.Add("readonly", "readonly")
        End If

        Dim lstrOrden As String
        Dim lstrArticulo As String
        lstrOrden = Request("Orden")
        lstrArticulo = Request("Articulo")
        Buscar(lstrOrden, lstrArticulo)

       

    End Sub

    Private Sub Buscar(ByVal pstrOrden As String, ByVal pstrArticulo As String)
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Try
            Dim lstrParams() As String = {"var_Empresa", Session("@EMPRESA"), _
                                        "var_Numero", pstrOrden, _
                                        "var_Articulo", pstrArticulo}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            ldtRes = lobjCon.ObtenerDataTable("usp_LOG_Requisicion_DetalleAdjuntoXArticulo", lstrParams)
            If ldtRes.Rows.Count = 1 Then
                txtArticuloCodigo.Text = ldtRes.Rows(0)("ArticuloCodigo")
                txtArticuloNombre.Text = ldtRes.Rows(0)("ArticuloNombre")
                txtPrecio.Text = Format(ldtRes.Rows(0)("UltimaCompraPrecio"), "###,##0.00")
                txtProveedor.Text = ldtRes.Rows(0)("ProveedorNombre")
                txtAlternativa1.Text = ldtRes.Rows(0)("Alternativa1Nombre")
                txtAlternativa2.Text = ldtRes.Rows(0)("Alternativa2Nombre")
                txtAlternativa3.Text = ldtRes.Rows(0)("Alternativa3Nombre")
            Else

            End If
        Catch ex As Exception

        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
    End Sub

End Class
