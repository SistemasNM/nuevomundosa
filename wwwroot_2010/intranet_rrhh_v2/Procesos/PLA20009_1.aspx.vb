Public Class PLA20009_1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtinicioaprox As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtcodctc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdesctc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnguardar As System.Web.UI.WebControls.Button
    Protected WithEvents btnbuscarctc As System.Web.UI.WebControls.Button

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
    If Not Page.IsPostBack = True Then
      Dim lstr_Codigo As String = "", lstr_Inicio As String = ""
      lstr_Codigo = Request("pstrctc")
      lstr_Inicio = Request("pstrinicio")

      If lstr_Inicio.Trim <> "" Then
        txtinicioaprox.Text = lstr_Inicio.Trim
      End If

      If lstr_Codigo.Trim <> "" Then
        txtcodctc.Text = lstr_Codigo.Trim
        'buscar ctc
        Call btnbuscarctc_Click(Nothing, Nothing)
      End If

      txtinicioaprox.Attributes.Add("onBlur", "javascript:return txtinicioaprox_onBlur();")
      txtdesctc.Attributes.Add("readonly", "readonly")
    End If
  End Sub

    Private Sub btnguardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnguardar.Click
    ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1','" & txtinicioaprox.Text & "','" & txtcodctc.Text & "');</Script>")
    End Sub

    Private Sub btnbuscarctc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscarctc.Click

        If txtcodctc.Text.Trim <> "" Then

            Dim lobj_ctc As New NuevoMundo.Ctc, ldtb_lista As DataTable, lstr_mensaje As String = ""

            lobj_ctc.Ano = 0
            lobj_ctc.EstadoCodigo = ""
            lobj_ctc.Descripcion = ""
            lobj_ctc.Numero = txtcodctc.Text.Trim

            lstr_mensaje = lobj_ctc.fnc_Listar(4, ldtb_lista)

            If ldtb_lista.Rows.Count > 0 Then
                txtdesctc.Text = ldtb_lista.Rows(0)("vch_descripcion")
            End If

            ldtb_lista = Nothing
            lobj_ctc = Nothing

        End If

    End Sub

End Class
