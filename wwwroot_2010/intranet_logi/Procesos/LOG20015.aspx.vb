Partial Public Class LOG20015
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

  End Sub

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
    Ajax.Utility.RegisterTypeForAjax(GetType(LOG20012))
    If Not Page.IsPostBack Then
      Call prc_listaralmacenes()
      DataGrid1.DataSource = Nothing
      DataGrid1.DataBind()
    End If
    'txtAreaSolicitanteNombre.Text = Me.hdnAreaSolicitante.Value
  End Sub

  Private Sub btnbuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
    Call Buscar()
  End Sub

  Private Sub DataGrid1_CancelCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
    DataGrid1.EditItemIndex = -1
    DataGrid1.ShowFooter = True
    Call CargarGrilla()
  End Sub

  Private Sub DataGrid1_EditCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    DataGrid1.EditItemIndex = e.Item.ItemIndex
    DataGrid1.ShowFooter = False
    Call CargarGrilla()
    'Session.Add("Nuevo", False)
  End Sub

  Private Sub DataGrid1_UpdateCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
    DataGrid1.EditItemIndex = -1
    DataGrid1.ShowFooter = True
    Call CargarGrilla()
  End Sub

  Private Sub DataGrid1_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
    Dim lstr_mensaje As String = ""
    Try

      Select Case e.CommandName
        Case "Add"
          Dim txtcodarticulof As TextBox = CType(e.Item.FindControl("txtcodarticulof"), TextBox)
          Dim txtcodalmacenf As TextBox = CType(e.Item.FindControl("txtcodalmtransff"), TextBox)
          Dim txtcodbarra1f As TextBox = CType(e.Item.FindControl("txtcodbarra1f"), TextBox)
          Dim txtcodbarra2f As TextBox = CType(e.Item.FindControl("txtcodbarra2f"), TextBox)
          Dim txtcodbarra3f As TextBox = CType(e.Item.FindControl("txtcodbarra3f"), TextBox)
          Dim txtcodbarra4f As TextBox = CType(e.Item.FindControl("txtcodbarra4f"), TextBox)
          'insertar
          If fnc_guardar(1, txtcodarticulof.Text.Trim, txtcodalmacenf.Text.Trim, txtcodbarra1f.Text.Trim, txtcodbarra2f.Text.Trim, txtcodbarra3f.Text.Trim, txtcodbarra4f.Text.Trim) = True Then
            Call Buscar()
          End If
          txtcodarticulof = Nothing
          txtcodalmacenf = Nothing

        Case "Update"
          Dim txtcodarticuloe As TextBox = CType(e.Item.FindControl("txtcodarticuloe"), TextBox)
          Dim txtcodalmacene As TextBox = CType(e.Item.FindControl("txtcodalmtransfe"), TextBox)
          Dim txtcodbarra1e As TextBox = CType(e.Item.FindControl("txtcodbarra1e"), TextBox)
          Dim txtcodbarra2e As TextBox = CType(e.Item.FindControl("txtcodbarra2e"), TextBox)
          Dim txtcodbarra3e As TextBox = CType(e.Item.FindControl("txtcodbarra3e"), TextBox)
          Dim txtcodbarra4e As TextBox = CType(e.Item.FindControl("txtcodbarra4e"), TextBox)
          'actualizar
          If fnc_guardar(2, txtcodarticuloe.Text.Trim, txtcodalmacene.Text.Trim, txtcodbarra1e.Text.Trim, txtcodbarra2e.Text.Trim, txtcodbarra3e.Text.Trim, txtcodbarra4e.Text.Trim) = True Then
            Call Buscar()
          End If
          txtcodarticuloe = Nothing
          txtcodalmacene = Nothing

      End Select

    Catch ex As Exception
      lstr_mensaje = ex.Message.Replace("'", "-")
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" & lstr_mensaje & "');</script>")
    End Try
  End Sub

  Private Sub DataGrid1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    Select Case e.Item.ItemType
      Case ListItemType.Footer
        Dim btnarticulof As HtmlInputButton = CType(e.Item.FindControl("btnarticulof"), HtmlInputButton)
        Dim txtcodalmtransff As TextBox = CType(e.Item.FindControl("txtcodalmtransff"), TextBox)

        txtcodalmtransff.Attributes.Add("onBlur", "txtcodalm_onBlur('" & e.Item.ClientID.ToString & "','f');")
        btnarticulof.Attributes.Add("onclick", "BuscarArticulo('" & e.Item.ClientID.ToString & "','f')")

        btnarticulof = Nothing
        txtcodalmtransff = Nothing

      Case ListItemType.EditItem
        Dim btnarticuloe As HtmlInputButton = CType(e.Item.FindControl("btnarticuloe"), HtmlInputButton)
        Dim txtcodalmtransfe As TextBox = CType(e.Item.FindControl("txtcodalmtransfe"), TextBox)

        txtcodalmtransfe.Attributes.Add("onBlur", "txtcodalm_onBlur('" & e.Item.ClientID.ToString & "','e');")
        btnarticuloe.Attributes.Add("onclick", "BuscarArticulo('" & e.Item.ClientID.ToString & "','e')")

        btnarticuloe = Nothing
        txtcodalmtransfe = Nothing

      Case ListItemType.AlternatingItem, ListItemType.Item
        Dim btnprint As ImageButton = CType(e.Item.FindControl("btnPrint"), ImageButton)
        Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)

        btnprint.Attributes.Add("onClick", "javascript:return ImprimirCodBar('" & ldrvItem("vch_codarticulo").ToString & "','" & ldrvItem("de_item").ToString.Replace("'", "") & "','" & ldrvItem("vch_codigobarra1").ToString & "','" & ldrvItem("vch_codigobarra2").ToString & "','" & ldrvItem("vch_codigobarra3").ToString & "','" & ldrvItem("vch_codigobarra4").ToString & "');")
        'btnprint.Attributes.Add("onClick", "alert('ingreso');")

        btnprint = Nothing

    End Select
  End Sub

#End Region

#Region "-- Metodos --"

  Private Function Buscar()
    Dim lobj_articulos As New NuevoMundo.clsArticulo
    Dim ldtb_datos As DataTable
    If lobj_articulos.Listar_DatosExtension(ldtb_datos, 1, IIf(ddlalmtransf.SelectedValue = "-- TODOS --", "000", ddlalmtransf.SelectedValue), txtcodarticulo.Text.Trim, txtdesarticulo.Text.Trim, "") = True Then
      Session("LOG20012_datos") = ldtb_datos
    Else
      Session("LOG20012_datos") = Nothing
    End If
    'Session("LOG20012_datos") = ldtb_datos

    ldtb_datos = Nothing
    lobj_articulos = Nothing

    Call CargarGrilla()

  End Function

  Private Function CargarGrilla()
    Try
      DataGrid1.DataSource = CType(Session("LOG20012_datos"), DataTable)
      DataGrid1.DataBind()
    Catch ex As Exception

    End Try
  End Function

  Private Sub prc_listaralmacenes()
    Dim lobj_almacen As New NuevoMundo.clsAlmacen
    Dim ldtb_datos As DataTable
    If lobj_almacen.Listar(ldtb_datos, "4", "", "") = True Then
      ddlalmtransf.DataSource = ldtb_datos
      ddlalmtransf.DataTextField = "de_alma"
      ddlalmtransf.DataValueField = "co_alma"
      ddlalmtransf.DataBind()
      ddlalmtransf.Items.Insert(0, "-- TODOS --")
    End If

    ldtb_datos = Nothing
    lobj_almacen = Nothing
  End Sub

  Private Function fnc_guardar(ByVal ptin_accion As Int16, ByVal pstr_articulo As String, ByVal pstr_almacen As String, ByVal pstr_codbarra1 As String, ByVal pstr_codbarra2 As String, ByVal pstr_codbarra3 As String, ByVal pstr_codbarra4 As String) As Boolean
    Dim lobj_articulo As NuevoMundo.clsArticulo
    Dim lbln_resultado As Boolean = False, lstr_mensaje As String
    Try

      If pstr_articulo.Length > 0 Then

        lobj_articulo = New NuevoMundo.clsArticulo

        lobj_articulo.CodArticulo = pstr_articulo
        lobj_articulo.AlmTransfAutomatica = pstr_almacen
        lobj_articulo.Codigo_Barra1 = pstr_codbarra1
        lobj_articulo.Codigo_Barra2 = pstr_codbarra2
        lobj_articulo.Codigo_Barra3 = pstr_codbarra3
        lobj_articulo.Codigo_Barra4 = pstr_codbarra4
        lobj_articulo.Usuario = Session("@USUARIO")

        lobj_articulo.Guardar_DatosExtension(ptin_accion)
        lbln_resultado = True
      End If

    Catch ex As Exception
      lstr_mensaje = ex.Message.Replace("'", "-")
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" & lstr_mensaje & "');</script>")
    End Try

    lobj_articulo = Nothing
    Return lbln_resultado
  End Function

  <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
  Public Function fnc_BuscarAlmacen(ByVal pstrAlmacen As String) As DataTable
    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
      Response.Redirect("/intranet/finsesion.htm")
    End If
    '-----------------------------------------------------------------------
    '--FINAL: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    Dim lobjAlmacen As New NuevoMundo.clsAlmacen
    Dim lstrError As String = "", ldtbRetorno As DataTable
    Try
      lobjAlmacen.Listar(ldtbRetorno, "1", pstrAlmacen, "")
    Catch ex As Exception

    End Try
    lobjAlmacen = Nothing

    Return ldtbRetorno
  End Function

#End Region

End Class

