
Partial Public Class LOG20014

#Region " Web Form Designer Generated Code "

    Inherits System.Web.UI.Page


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


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
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then

            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            'Session("@USUARIO") = "EPOMA"
            Session("@EMPRESA") = "01"

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
        'Ajax.Utility.RegisterTypeForAjax(GetType(PLA20013))

        If Not Page.IsPostBack Then
            Call prc_UltimoAbierto()
            'btnGrabar.Attributes.Add("onClick", "javascript:return fnc_ValidarRequisicion();")
        End If
        'HDN1-->
        'HDN2-->
        'HDN3-->
        'HDN4-->
        'HDN5-->
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        'Dim ldtTabla As DataTable
        'Dim lobjHESol As HorasExtras
        Try
            lblMensaje.ForeColor = lblMensaje.ForeColor.Blue
            lblMensaje.Text = ""

            Select Case e.CommandName
                Case "Update"

                    'lobjHESol = New HorasExtras

                    'Dim lblCodigoI As TextBox = CType(e.Item.FindControl("txtcantinv"), TextBox)

                    'lobjHESol.Codigo = lblCodigoI.Text
                    'lobjHESol.Usuario = Session("@USUARIO")

                    'ldtTabla = fnc_EsquemaReconocer()

                    'If lobjHESol.fnc_Guardar(6, ldtTabla) = True Then
          '    'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('Se actualizo correctamente.');</script>")
          '    Call fnc_Consultar(False)
          'Else
          '    lblMensaje.ForeColor = lblMensaje.ForeColor.Red
          '    lblMensaje.Text = lobjHESol.MensajeError
          'End If

          'ldtTabla = Nothing
          'lobjHESol = Nothing

      End Select

    Catch ex As Exception

    End Try
  End Sub

  Private Sub DataGrid1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

    Select Case e.Item.ItemType

      Case ListItemType.AlternatingItem, ListItemType.Item
        'Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
        'Dim btnUpdate As ImageButton = CType(e.Item.FindControl("btnUpdate"), ImageButton)

        'Dim lblCodigo As Label = CType(e.Item.FindControl("lblCodigoI"), Label)
        'Dim hdnCodigo As HtmlInputHidden = CType(e.Item.FindControl("hdnCodigoI"), HtmlInputHidden)


        'Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
        'Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)

        'lobjBoton.Attributes.Add("onClick", "fnc_mostrardetalle('" & ldrvItem("int_codigo") & "')")
        'lobjBoton = Nothing

        'hdnCodigo.Value = ldrvItem("int_codigo")

        'btnUpdate.Attributes.Add("onclick", "javascript:return fnc_Update('" & e.Item.ClientID.ToString & "')")
        'btnDelete.Attributes.Add("onclick", "javascript:return fnc_Eliminar('" & e.Item.ClientID.ToString & "')")
    End Select
  End Sub

  Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
    lblMensaje.Text = ""
    Call fnc_Consultar()
  End Sub

#End Region

#Region "-- Metodos --"

  'Function fnc_EsquemaReconocer() As DataTable
  '    Dim ldtdRes As DataTable
  '    ldtdRes = New DataTable
  '    ldtdRes.Columns.Add("p1", GetType(Integer)) 'int_codigo
  '    ldtdRes.Columns.Add("p2", GetType(String)) 'vch_concepto, de tipo de hora extra para el módulo de supervisores
  '    ldtdRes.Columns.Add("p3", GetType(String)) 'vch_codtrabajador
  '    ldtdRes.Columns.Add("p4", GetType(String)) 'chr_inicio
  '    ldtdRes.Columns.Add("p5", GetType(Integer)) 'int_duracion
  '    ldtdRes.Columns.Add("p6", GetType(String)) 'vch_observacion
  '    ldtdRes.Columns.Add("p7", GetType(String)) 'chr_fecha_sol
  '    ldtdRes.Columns.Add("p8", GetType(String)) 'chr_rango_marca
  '    Return ldtdRes
  'End Function

  Function fnc_Consultar() As String
    Dim lobj_planilla As New NuevoMundo.clsArticulo
    Dim ldts_datos As New DataSet

    Try

      If lobj_planilla.Listar_InventarioDiario(ldts_datos, 1, txtcodinv.Text, "", "", "") = True Then
        DataGrid1.DataSource = ldts_datos.Tables(0)
        DataGrid1.DataBind()

        If ldts_datos.Tables(1).Rows.Count > 0 Then
          lblEstado.Text = ldts_datos.Tables(1).Rows(0)("chr_estado").ToString
        End If
      End If
      ldts_datos = Nothing
      lobj_planilla = Nothing

      lblMensaje.ForeColor = Drawing.Color.DarkBlue
      lblMensaje.Text = ""

    Catch ex As Exception
      'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
      lblMensaje.ForeColor = Drawing.Color.Red
      lblMensaje.Text = ex.Message.Replace("'", "")
    Finally
    End Try

    Return ""
  End Function

  Sub prc_UltimoAbierto()
    Dim lobj_planilla As New NuevoMundo.clsArticulo
    Dim ldts_datos As New DataSet

    Try

      If lobj_planilla.Listar_InventarioDiario(ldts_datos, 2, "", "", "", "") = True Then
        If ldts_datos.Tables(0).Rows.Count > 0 Then
          txtcodinv.Text = ldts_datos.Tables(0).Rows(0)("chr_codigoinv").ToString
        End If
      End If
      ldts_datos = Nothing
      lobj_planilla = Nothing

      lblMensaje.ForeColor = Drawing.Color.DarkBlue
      lblMensaje.Text = ""

    Catch ex As Exception
      'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
      lblMensaje.ForeColor = Drawing.Color.Red
      lblMensaje.Text = ex.Message.Replace("'", "")
    Finally
    End Try
  End Sub

  Function fnc_Nuevo() As String
    Dim lobj_planilla As New NuevoMundo.clsArticulo

    Try

      If lobj_planilla.Guardar_InventarioDiario("NUE", "", "", "", "", Session("@USUARIO")) = True Then
        Call prc_UltimoAbierto()
        Call btnConsultar_Click(Nothing, Nothing)

        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se creo satisfactoriamente el inventario.')</script>")
      End If

      lblMensaje.ForeColor = Drawing.Color.DarkBlue
      lblMensaje.Text = ""

    Catch ex As Exception
      'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
      lblMensaje.ForeColor = Drawing.Color.Red
      lblMensaje.Text = ex.Message.Replace("'", "")
    Finally
    End Try

    Return ""
  End Function

  Function fnc_Guardar() As String
    Dim lobj_planilla As New NuevoMundo.clsArticulo
    Dim lobj_util As New NM_General.Util, ldtb_datos As New DataTable
    Dim ldtr_dato As DataGridItem, ldr_fila As DataRow
    Try

      ldtb_datos.Columns.Add("c1", GetType(String))
      ldtb_datos.Columns.Add("c2", GetType(Double))

      For Each ldtr_dato In DataGrid1.Items
        ldr_fila = ldtb_datos.NewRow()
        ldr_fila("c1") = ldtr_dato.Cells(2).Text
        ldr_fila("c2") = IIf(IsNumeric(CType(ldtr_dato.FindControl("txtcantinv"), TextBox).Text) = True, CType(ldtr_dato.FindControl("txtcantinv"), TextBox).Text, 0)
        ldtb_datos.Rows.Add(ldr_fila)
      Next
      ldtb_datos.TableName = "lista"
      If lobj_planilla.Guardar_InventarioDiario("GUA", txtcodinv.Text, "", "", lobj_util.GeneraXml(ldtb_datos), Session("@USUARIO")) = True Then
        lblMensaje.ForeColor = Drawing.Color.DarkBlue
        lblMensaje.Text = "Datos guardados satisfactoriamente."
      End If

    Catch ex As Exception
      'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
      lblMensaje.ForeColor = Drawing.Color.Red
      lblMensaje.Text = ex.Message.Replace("'", "")
    Finally
    End Try

    lobj_util = Nothing
    Return ""
  End Function

  Function fnc_Cerrar() As String
    Dim lobj_planilla As New NuevoMundo.clsArticulo

    Try

      If lobj_planilla.Guardar_InventarioDiario("CER", txtcodinv.Text, "", "", "", Session("@USUARIO")) = True Then
        lblMensaje.ForeColor = Drawing.Color.DarkBlue
        lblMensaje.Text = "Se cerro satisfactoriamente el inventario."
      End If

    Catch ex As Exception
      'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
      lblMensaje.ForeColor = Drawing.Color.Red
      lblMensaje.Text = ex.Message.Replace("'", "")
    Finally
    End Try

    Return ""
  End Function

#End Region

  Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNuevo.Click
    Call fnc_Nuevo()
  End Sub

  Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
    fnc_Guardar()
  End Sub

  Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCerrar.Click
    Call fnc_Cerrar()
  End Sub

  Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImprimir.Click
    Dim lstrURL As String
    lstrURL = "LOG20014_E.aspx?pstrcodinv=" + txtcodinv.Text
    'ClientScript.RegisterStartupScript(Me.[GetType](),"reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    ScriptManager.RegisterStartupScript(Page, GetType(Page), "reporte", "popUp('" & lstrURL & "');", True)

  End Sub
End Class

