Imports NuevoMundo.Planillas

Public Class PLA20011

#Region " Web Form Designer Generated Code "

    Inherits System.Web.UI.Page
    Protected WithEvents hdnCodigo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN5 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgSeguimiento As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtdesarea As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdestrabajador As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfechasol As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtestado As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdestipo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txthorassol As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtcodctc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdesctc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtobservacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txthoraapr As System.Web.UI.WebControls.TextBox
    Protected WithEvents txthorarec As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfecharec As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtusuariorec As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfechaini As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfechafin As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnbuscarmarca As System.Web.UI.WebControls.Button
    Protected WithEvents txtfecharei As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtusuariorei As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtestadorei As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtmarcacionrec As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnldatosestadisticos As System.Web.UI.WebControls.Panel
    Protected WithEvents hdnarticuloestadistica As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnposicionpanel As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtreduracion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtrecinicio As System.Web.UI.WebControls.TextBox
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image
    Protected WithEvents dgmarcaciones As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlrecTipo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnreconocer As System.Web.UI.WebControls.Button
    Protected WithEvents btningresorec As System.Web.UI.WebControls.Button
    Protected WithEvents btnocultarrec As System.Web.UI.WebControls.Button
    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label
    Protected WithEvents btnrefrescar As System.Web.UI.WebControls.Button
    Protected WithEvents txtinicioaprox As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnreintegro As System.Web.UI.WebControls.Button

    Private mstr_fecha As String
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

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Readonly
            Call sub_IniJavaScript()

            Dim lstr_Codigo As String = ""
            lstr_Codigo = Request("pstrCodigo")

            If lstr_Codigo <> "" Then
                HDN1.Value = lstr_Codigo '"1" 'pstrCodigo int_codigo
                Call fnc_llenartipo()
                Call fnc_Consultar()
            End If

            btnreconocer.Attributes.Add("onClick", "javascript:return fnc_validarreconocer();")
            btnreintegro.Attributes.Add("onClick", "javascript:return fnc_validarreintegro();")
            btnbuscarmarca.Attributes.Add("onClick", "javascript:return fnc_buscarmarca();")
            btningresorec.Attributes.Add("onClick", "javascript:return fnc_mostrarreco();")
            btnocultarrec.Attributes.Add("onClick", "javascript:return fnc_ocultarreco();")

            txtrecinicio.Attributes.Add("onBlur", "javascript:return txtrecinicio_onBlur();")

            ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")


        End If
    'txtAreaSolicitanteNombre.Text = Me.hdnAreaSolicitante.Value
    'HDN1-->codigo de solicitud int_codigo
    'HDN2-->codigo del trabajador
    'HDN3-->
    'HDN4-->
    'HDN5-->
  End Sub

  Private Sub sub_IniJavaScript()

        txtfechaini.Attributes.Add("readonly", "readonly")
        txtfechafin.Attributes.Add("readonly", "readonly")
        txtdesarea.Attributes.Add("readonly", "readonly")

        txtrecinicio.Attributes.Add("readonly", "readonly")
        txtreduracion.Attributes.Add("readonly", "readonly")


        txtdestrabajador.Attributes.Add("readonly", "readonly")
        txtfechasol.Attributes.Add("readonly", "readonly")
        txtestado.Attributes.Add("readonly", "readonly")
        txtdestipo.Attributes.Add("readonly", "readonly")
        txthorassol.Attributes.Add("readonly", "readonly")
        txtinicioaprox.Attributes.Add("readonly", "readonly")
        txtobservacion.Attributes.Add("readonly", "readonly")
        txtcodctc.Attributes.Add("readonly", "readonly")
        txtdesctc.Attributes.Add("readonly", "readonly")
        txthoraapr.Attributes.Add("readonly", "readonly")
        txthorarec.Attributes.Add("readonly", "readonly")
        txtfecharec.Attributes.Add("readonly", "readonly")
        txtusuariorec.Attributes.Add("readonly", "readonly")
        txtmarcacionrec.Attributes.Add("readonly", "readonly")
    txtfecharei.Attributes.Add("readonly", "readonly")
    txtusuariorei.Attributes.Add("readonly", "readonly")
    txtestadorei.Attributes.Add("readonly", "readonly")

  End Sub


  Private Sub btnreconocer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreconocer.Click
    If CInt(txtreduracion.Text) > 0 Then
      Call fnc_Reconocer()
    End If
  End Sub

  Private Sub btnreintegro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreintegro.Click
    Call fnc_EnviarReintegro()
  End Sub

  Private Sub btnbuscarmarca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscarmarca.Click
    Call fnc_BuscarMarcacion()
  End Sub

  Private Sub dgmarcaciones_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgmarcaciones.ItemDataBound
    Select Case e.Item.ItemType

      Case ListItemType.AlternatingItem, ListItemType.Item

        Dim lblfechaI As Label = CType(e.Item.FindControl("lblfechaI"), Label)

        Dim hdnCodigo As HtmlInputHidden = CType(e.Item.FindControl("hdnCodigoI"), HtmlInputHidden)
        Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)

        If mstr_fecha <> ldrvItem("chr_fecha") Then
          mstr_fecha = ldrvItem("chr_fecha")
          lblfechaI.Text = mstr_fecha
        End If


    End Select
  End Sub

  Private Sub btnocultarrec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnocultarrec.Click
    ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")
  End Sub

  Private Sub btnrefrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefrescar.Click
    Call fnc_Consultar()
    ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")

  End Sub

#End Region

#Region "-- Metodos --"

  Function fnc_BuscarMarcacion()
    Dim lobj_planilla As New HorasExtras, dtsDatos As DataSet, lstr_inicio As String, lstr_final As String

    lstr_inicio = Right(txtfechaini.Text, 4) + Mid(txtfechaini.Text, 4, 2) + Left(txtfechaini.Text, 2)
    lstr_final = Right(txtfechafin.Text, 4) + Mid(txtfechafin.Text, 4, 2) + Left(txtfechafin.Text, 2)

    Try
      If lobj_planilla.fnc_Listar(dtsDatos, 5, "", lstr_inicio, lstr_final, "", HDN2.Value) = True Then
        mstr_fecha = ""
        dgmarcaciones.DataSource = CType(dtsDatos.Tables(0), DataTable)
        dgmarcaciones.DataBind()
      End If
    Catch ex As Exception

    End Try


    lobj_planilla = Nothing
  End Function

  Function fnc_EnviarReintegro()
    Dim lobj_planilla As New HorasExtras, dtsDatos As DataSet
    Dim ldtb_datos As DataTable

    lobj_planilla.Codigo = HDN1.Value
    lobj_planilla.Usuario = Session("@USUARIO")

    ldtb_datos = fnc_EsquemaReconocer()

    'en caso de reconocer correctamente
    If lobj_planilla.fnc_Guardar(5, ldtb_datos) = True Then
      lblMensaje.Text = ""
      Call fnc_Consultar()
      ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")
    Else
      lblMensaje.Text = lobj_planilla.MensajeError
    End If

    lobj_planilla = Nothing
  End Function

  Function fnc_Reconocer()
    Dim lobj_planilla As New HorasExtras
    Dim ldtb_datos As DataTable, ldrRow As DataRow
    Dim lstr_marca As String

    Dim ldtm_fecha As Date

    ldtb_datos = fnc_EsquemaReconocer()


    ldtm_fecha = ldtm_fecha.AddYears(2011)
    ldtm_fecha = ldtm_fecha.AddMonths(12)
    ldtm_fecha = ldtm_fecha.AddDays(1)

    ldtm_fecha = ldtm_fecha.AddHours(CDbl(Left(txtrecinicio.Text, 2)) + CInt(txtreduracion.Text))
    ldtm_fecha = ldtm_fecha.AddMinutes(CDbl(Right(txtrecinicio.Text, 2)))

    lstr_marca = txtrecinicio.Text.Replace(":", "") + Right("00" + ldtm_fecha.Hour.ToString(), 2) + Right("00" + ldtm_fecha.Minute.ToString(), 2)

    ldrRow = ldtb_datos.NewRow

    ldrRow("p1") = HDN1.Value 'int_codigo
    ldrRow("p2") = ddlrecTipo.SelectedValue 'vch_concepto
    ldrRow("p3") = HDN2.Value 'vch_codtrabajador
    ldrRow("p4") = txtrecinicio.Text 'chr_inicio
    ldrRow("p5") = txtreduracion.Text 'int_duracion
    ldrRow("p6") = txtobservacion.Text 'vch_observacion
    ldrRow("p7") = Right(txtfechasol.Text, 4) + Mid(txtfechasol.Text, 4, 2) + Left(txtfechasol.Text, 2) 'chr_fecha_sol YYYYMMDD
    ldrRow("p8") = lstr_marca 'chr_rango_marca hhmmhhmm

    ldtb_datos.Rows.Add(ldrRow)
    ldrRow = Nothing

    lobj_planilla.Codigo = HDN1.Value
    lobj_planilla.Usuario = Session("@USUARIO")
    'en caso de reconocer correctamente
    If lobj_planilla.fnc_Guardar(4, ldtb_datos) = True Then
      lblMensaje.Text = ""
      ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")
      Call fnc_Consultar()
    Else
      lblMensaje.Text = lobj_planilla.MensajeError
    End If


    lobj_planilla = Nothing
  End Function

  Function fnc_llenartipo()
    Dim lobj_planilla As New HorasExtras, dtsDatos As DataSet
    Try
      If lobj_planilla.fnc_Listar(dtsDatos, 4, "", "", "", "", "") = True Then
        ddlrecTipo.DataSource = CType(dtsDatos.Tables(0), DataTable)
        ddlrecTipo.DataTextField = "vchDescripcion"
        ddlrecTipo.DataValueField = "vchPkConcepto"
        ddlrecTipo.DataBind()
      End If
    Catch ex As Exception

    End Try

    lobj_planilla = Nothing
  End Function

  Function fnc_Consultar()
    Dim lobj_planilla As New HorasExtras
    Dim ldts_datos As DataSet, ldtb_segui As DataTable, lstr_marca As String

    Call LimpiarObjetos()

    If HDN1.Value.Trim = "" Then
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No existe número de solicitud.');</script>")
      Exit Function
    End If


    Try
      'detalle de registro
      If lobj_planilla.fnc_Listar(ldts_datos, 3, HDN1.Value, "", "", "", "") = True Then
        ldtb_segui = CType(ldts_datos.Tables(0), DataTable)

        If ldtb_segui.Rows.Count > 0 Then

          txtdesarea.Text = ldtb_segui.Rows(0).Item("vch_desarea")
          txtdestrabajador.Text = ldtb_segui.Rows(0).Item("vch_destrabajador")
          txtfechasol.Text = ldtb_segui.Rows(0).Item("chr_fecha_sol")
          txtestado.Text = ldtb_segui.Rows(0).Item("chr_estado")
          txtdestipo.Text = ldtb_segui.Rows(0).Item("chr_destarea")
          txtobservacion.Text = ldtb_segui.Rows(0).Item("vch_observacion")
          txtcodctc.Text = ldtb_segui.Rows(0).Item("vch_codctc")
          txtdesctc.Text = ldtb_segui.Rows(0).Item("vch_desctc")
          txthorassol.Text = ldtb_segui.Rows(0).Item("tin_horext_sol")
          txthoraapr.Text = ldtb_segui.Rows(0).Item("tin_horext_apr")
          txthorarec.Text = ldtb_segui.Rows(0).Item("tin_horext_rec")

          txtinicioaprox.Text = ldtb_segui.Rows(0).Item("vch_inicioaprox")

          txtfecharec.Text = ldtb_segui.Rows(0).Item("chr_fecha_rec")
          txtusuariorec.Text = ldtb_segui.Rows(0).Item("vch_usuario_rec")

          lstr_marca = Trim(ldtb_segui.Rows(0).Item("chr_rango_marca"))
          If lstr_marca <> "" Then
            txtmarcacionrec.Text = Left(lstr_marca, 2) + ":" + Mid(lstr_marca, 3, 2) + " - " + Mid(lstr_marca, 5, 2) + ":" + Right(lstr_marca, 2)
          Else
            txtmarcacionrec.Text = ""
          End If

          txtfecharei.Text = ldtb_segui.Rows(0).Item("chr_fecha_rei")
          txtusuariorei.Text = ldtb_segui.Rows(0).Item("vch_usuario_rei")
          txtestadorei.Text = ldtb_segui.Rows(0).Item("chr_estado_rei")

          HDN2.Value = ldtb_segui.Rows(0).Item("vch_codtrabajador")

          'detalle de seguimiento
          lobj_planilla.CodigoSol = ldtb_segui.Rows(0).Item("chr_codigo_sol")


          'habilitar controles en caso estado APR
          If txtestado.Text = "APR" Then
                        txtreduracion.Attributes.Remove("readonly")
                        txtrecinicio.Attributes.Remove("readonly")
            btningresorec.Enabled = True
            btnreconocer.Enabled = True
          Else
                        txtreduracion.Attributes.Add("readonly", "readonly")
                        txtrecinicio.Attributes.Add("readonly", "readonly")
            btningresorec.Enabled = False
            btnreconocer.Enabled = False
          End If

          If txtestado.Text = "REC" And txtestadorei.Text.Trim = "" Then
            btnreintegro.Enabled = True
          Else
            btnreintegro.Enabled = False
          End If

          lobj_planilla.SoliSecuSol = 0
          If lobj_planilla.fnc_ListarSeguimientoAprob(1, ldtb_segui) = "" Then
            dgSeguimiento.DataSource = ldtb_segui
            dgSeguimiento.DataBind()
          End If
        End If

      End If

      'datos del area

    Catch ex As Exception
      ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
    Finally

    End Try

    'ldts_datos = Nothing
    'lobj_planilla = Nothing
  End Function

  
  Function LimpiarObjetos()

    txtdesarea.Text = ""
    txtdestrabajador.Text = ""
    txtfechasol.Text = ""
    txtestado.Text = ""
    txtdestipo.Text = ""
    txtobservacion.Text = ""
    txthorassol.Text = ""
    txtinicioaprox.Text = ""
    txtcodctc.Text = ""
    txtdesctc.Text = ""
    txthoraapr.Text = ""
    txthorarec.Text = ""
    txtfecharec.Text = ""
    txtusuariorec.Text = ""
    txtmarcacionrec.Text = ""
    txtfecharei.Text = ""
    txtestadorei.Text = ""
    txtusuariorei.Text = ""

    btnreconocer.Enabled = False
    btnreintegro.Enabled = False

    txtreduracion.Text = ""
    txtrecinicio.Text = ""

  End Function

  Function fnc_EsquemaReconocer() As DataTable
    Dim ldtdRes As DataTable
    ldtdRes = New DataTable
    ldtdRes.Columns.Add("p1", GetType(Integer)) 'int_codigo
    ldtdRes.Columns.Add("p2", GetType(String)) 'vch_concepto, de tipo de hora extra para el módulo de supervisores
    ldtdRes.Columns.Add("p3", GetType(String)) 'vch_codtrabajador
    ldtdRes.Columns.Add("p4", GetType(String)) 'chr_inicio
    ldtdRes.Columns.Add("p5", GetType(Integer)) 'int_duracion
    ldtdRes.Columns.Add("p6", GetType(String)) 'vch_observacion
    ldtdRes.Columns.Add("p7", GetType(String)) 'chr_fecha_sol
    ldtdRes.Columns.Add("p8", GetType(String)) 'chr_rango_marca
    Return ldtdRes
  End Function

#End Region

End Class
