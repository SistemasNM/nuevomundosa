Public Class LOG20013

  Inherits System.Web.UI.Page

#Region "-- Eventos --"

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Init

    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    '20120904 EPM Valida que la session este vacio o nula
    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then

      If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
        Dim objRequest As New BLITZ_LOCK.clsRequest
        Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
        objRequest = Nothing
      End If

      Session("@EMPRESA") = "01"
      'Session("@USUARIO") = "EPOMA"

      If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
        Response.Redirect("/intranet/finsesion.htm")
      End If

    End If
    '-----------------------------------------------------------------------
    '--FINAL: VERIFICAR LA SESION
    '-----------------------------------------------------------------------

  End Sub

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not Page.IsPostBack Then
      Call prc_listaralmacenes()
    End If
  End Sub

  Private Sub btnbuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
    Call Buscar()
  End Sub

  Private Sub btnEjecutar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEjecutar.Click
    Call EjecutarTrasferencia(IIf(ddlalmtransf.SelectedValue = "-- TODOS --", "000", ddlalmtransf.SelectedValue))
  End Sub

#End Region

#Region "-- Metodos --"

  Private Function Buscar()
    Dim lobj_articulos As New NuevoMundo.clsArticulo
    Dim ldtb_datos As DataTable
    If lobj_articulos.Listar_DatosExtension(ldtb_datos, 2, IIf(ddlalmtransf.SelectedValue = "-- TODOS --", "000", ddlalmtransf.SelectedValue), "", "", "") = True Then
      DataGrid1.DataSource = ldtb_datos
    Else
      DataGrid1.DataSource = Nothing
    End If
    DataGrid1.DataBind()

    ldtb_datos = Nothing
    lobj_articulos = Nothing

  End Function


  Private Sub prc_listaralmacenes()
    Dim lobj_almacen As New NuevoMundo.clsAlmacen
    Dim ldtb_datos As DataTable
    If lobj_almacen.Listar(ldtb_datos, "4", "", "") = True Then
      ddlalmtransf.DataSource = ldtb_datos
      ddlalmtransf.DataTextField = "de_alma"
      ddlalmtransf.DataValueField = "co_alma"
      ddlalmtransf.DataBind()
      ddlalmtransf.Items.RemoveAt(0)
      ddlalmtransf.Items.Insert(0, "-- TODOS --")
    End If

    ldtb_datos = Nothing
    lobj_almacen = Nothing
  End Sub


  Private Sub EjecutarTrasferencia(ByVal pstr_coalma As String)
    Dim lobj_almacen As New NuevoMundo.clsAlmacen
    Dim lint_tipo As Int16, lstr_alma As String
    Try

      If ddlalmtransf.SelectedValue = "-- TODOS --" Then
        lint_tipo = 1
        lstr_alma = ""
      Else
        lint_tipo = 2
        lstr_alma = ddlalmtransf.SelectedValue
      End If

      If lobj_almacen.Procesar_TransfAutomatica(lint_tipo, lstr_alma, Session("@USUARIO")) = True Then
        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se realizo la transferencia.');</script>")
        Call Buscar()
      End If

    Catch ex As Exception
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
    End Try

    lobj_almacen = Nothing
  End Sub

#End Region


End Class

