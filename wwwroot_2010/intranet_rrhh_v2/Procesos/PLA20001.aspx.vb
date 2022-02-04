Public Class PLA20001
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgCuentas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnExcluir As System.Web.UI.WebControls.Button
    Protected WithEvents btnIncluir As System.Web.UI.WebControls.Button

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
            CargarGrilla()
        End If
    End Sub

    Private Sub CargarGrilla()
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Empresa", Session("@EMPRESA")}
        Dim ldtRes As DataTable

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            ldtRes = lobjCon.ObtenerDataTable("USP_CON_CUENTASCONTABLES_LISTARCUENTASCAJARRHH", lstrParametros)
            dtgCuentas.DataSource = ldtRes
            dtgCuentas.DataBind()
            If ldtRes.Rows(0)("chr_Flag") = "S" Then
                btnIncluir.Enabled = False
                btnExcluir.Enabled = True
            ElseIf ldtRes.Rows(0)("chr_Flag") = "N" Then
                btnExcluir.Enabled = False
                btnIncluir.Enabled = True
            Else
                btnExcluir.Enabled = True
                btnIncluir.Enabled = True
            End If
        Catch ex As Exception

        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
    End Sub

    Private Sub ModificarCuentas(ByVal pstrTipo As String)
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Empresa", Session("@EMPRESA"), "chr_Tipo", pstrTipo, "var_Usuario", Session("@USUARIO")}
        Dim ldtRes As DataTable

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            ldtRes = lobjCon.ObtenerDataTable("USP_CON_CUENTASCONTABLES_MODIFICARCUENTASCAJARRHH", lstrParametros)
            dtgCuentas.DataSource = ldtRes
            dtgCuentas.DataBind()
            If ldtRes.Rows(0)("chr_Flag") = "S" Then
                btnIncluir.Enabled = False
                btnExcluir.Enabled = True
            ElseIf ldtRes.Rows(0)("chr_Flag") = "N" Then
                btnExcluir.Enabled = False
                btnIncluir.Enabled = True
            Else
                btnExcluir.Enabled = True
                btnIncluir.Enabled = True
            End If
        Catch ex As Exception

        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
    End Sub

    Private Sub btnIncluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncluir.Click
        ModificarCuentas("S")
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        ModificarCuentas("N")
    End Sub
End Class
