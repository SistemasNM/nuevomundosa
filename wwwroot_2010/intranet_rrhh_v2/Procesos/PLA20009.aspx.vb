Imports NuevoMundo.Planillas
Imports NuevoMundo

Public Class PLA20009

#Region " Web Form Designer Generated Code "
    Inherits System.Web.UI.Page

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
#End Region

#Region "-- Eventos --"
    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20220126 OB Valida que la session este vacio o nula
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "OBLAS"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(PLA20009))
        If Not Page.IsPostBack Then
            Call fnc_Consultar(True)
            btnSolicitar.Attributes.Add("onClick", "javascript:return SolicitarAprobacion();")
            HDN4.Value = "DataGrid1_"
            txtfechaini.Attributes.Add("readonly", "readonly")
            txtfechafin.Attributes.Add("readonly", "readonly")
        End If

    End Sub

    Private Sub btnSolicitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitar.Click
        Call fnc_SolicitarAprobacion()
    End Sub

    Private Sub DataGrid1_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        DataGrid1.ShowFooter = True
        Call CargarGrilla()
    End Sub

    Private Sub DataGrid1_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = e.Item.ItemIndex
        DataGrid1.ShowFooter = False
        Call CargarGrilla()
        Session.Add("Nuevo", False)
    End Sub

    Private Sub DataGrid1_UpdateCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        DataGrid1.EditItemIndex = -1
        DataGrid1.ShowFooter = True
        Call CargarGrilla()
    End Sub

    Private Sub DataGrid1_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim ldtTabla As DataTable, ldrRow As DataRow
        Dim lobjHESol As HorasExtras
        Try
            lblMensaje.ForeColor = Drawing.Color.Blue
            lblMensaje.Text = ""

            Select Case e.CommandName
                Case "Add"
                    lobjHESol = New HorasExtras
                    ldtTabla = fnc_EsquemaSolicitud()

                    Dim ddlTareaF As DropDownList = CType(e.Item.FindControl("ddlTareaF"), DropDownList)
                    Dim TxtCtcF As TextBox = CType(e.Item.FindControl("TxtCtcF"), TextBox)
                    Dim txtFechaF As TextBox = CType(e.Item.FindControl("txtFechaF"), TextBox)
                    Dim txtTrabajadorF As TextBox = CType(e.Item.FindControl("txtTrabajadorF"), TextBox)
                    Dim txtHESolF As TextBox = CType(e.Item.FindControl("txtHESolF"), TextBox)
                    Dim txtObservacionF As TextBox = CType(e.Item.FindControl("txtObservacionF"), TextBox)

                    ldrRow = ldtTabla.NewRow

                    ldrRow("p1") = 0 'int_codigo
                    ldrRow("p2") = "" 'chr_estado
                    ldrRow("p3") = HDN1.Value 'vch_codigoarea
                    ldrRow("p4") = Session("@USUARIO") 'vch_solicitante
                    ldrRow("p5") = Right(txtFechaF.Text, 4) + Mid(txtFechaF.Text, 4, 2) + Left(txtFechaF.Text, 2) 'dtm_fecha_sol YYYYMMDD
                    ldrRow("p6") = txtTrabajadorF.Text 'vch_codtrabajador
                    ldrRow("p7") = ddlTareaF.SelectedValue 'chr_tarea
                    ldrRow("p8") = TxtCtcF.Text 'vch_codctc
                    ldrRow("p9") = txtHESolF.Text 'tin_horext_sol
                    ldrRow("p10") = "" 'vch_motivo
                    ldrRow("p11") = txtObservacionF.Text 'vch_observacion
                    ldrRow("p12") = HDN6.Value 'vch_inicioaprox

                    ldtTabla.Rows.Add(ldrRow)
                    ldrRow = Nothing

                    lobjHESol.Codigo = 0
                    lobjHESol.Usuario = Session("@USUARIO")

                    If lobjHESol.fnc_Guardar(1, ldtTabla) = True Then
                        HDN2.Value = ddlTareaF.SelectedValue + "|" + TxtCtcF.Text + "|" + txtFechaF.Text + "|" + txtTrabajadorF.Text + "|" + txtHESolF.Text + "|" + txtObservacionF.Text
                        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se guardo la solicitud correctamente.');</script>")
                        Call fnc_Consultar(False)
                    Else
                        'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + lobjHESol.MensajeError + "');</script>")
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = lobjHESol.MensajeError
                    End If

                    ldtTabla = Nothing
                    lobjHESol = Nothing
                Case "Update"

                    lobjHESol = New HorasExtras
                    ldtTabla = fnc_EsquemaSolicitud()

                    Dim lblCodigoE As Label = CType(e.Item.FindControl("lblCodigoE"), Label)
                    Dim ddlTareaE As DropDownList = CType(e.Item.FindControl("ddlTareaE"), DropDownList)
                    Dim TxtCtcE As TextBox = CType(e.Item.FindControl("TxtCtcE"), TextBox)
                    Dim txtFechaE As TextBox = CType(e.Item.FindControl("txtFechaE"), TextBox)
                    Dim txtTrabajadorE As TextBox = CType(e.Item.FindControl("txtTrabajadorE"), TextBox)
                    Dim txtHESolE As TextBox = CType(e.Item.FindControl("txtHESolE"), TextBox)
                    Dim txtObservacionE As TextBox = CType(e.Item.FindControl("txtObservacionE"), TextBox)

                    ldrRow = ldtTabla.NewRow

                    ldrRow("p1") = lblCodigoE.Text
                    ldrRow("p2") = "" 'chr_estado
                    ldrRow("p3") = HDN1.Value 'vch_codigoarea
                    ldrRow("p4") = Session("@USUARIO") 'vch_solicitante
                    ldrRow("p5") = Right(txtFechaE.Text, 4) + Mid(txtFechaE.Text, 4, 2) + Left(txtFechaE.Text, 2) 'dtm_fecha_sol YYYYMMDD
                    ldrRow("p6") = txtTrabajadorE.Text 'vch_codtrabajador
                    ldrRow("p7") = ddlTareaE.SelectedValue 'chr_tarea
                    ldrRow("p8") = TxtCtcE.Text 'vch_codctc
                    ldrRow("p9") = txtHESolE.Text 'tin_horext_sol
                    ldrRow("p10") = "" 'vch_motivo
                    ldrRow("p11") = txtObservacionE.Text 'vch_observacion
                    ldrRow("p12") = HDN6.Value 'vch_inicioaprox

                    ldtTabla.Rows.Add(ldrRow)
                    ldrRow = Nothing

                    lobjHESol.Codigo = lblCodigoE.Text
                    lobjHESol.Usuario = Session("@USUARIO")

                    If lobjHESol.fnc_Guardar(2, ldtTabla) = True Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se guardo la solicitud correctamente.');</script>")
                        Call fnc_Consultar(False)
                    Else
                        'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + lobjHESol.MensajeError + "');</script>")
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = lobjHESol.MensajeError
                    End If

                    ldtTabla = Nothing
                    lobjHESol = Nothing

                Case "Delete"

                    lobjHESol = New HorasExtras
                    ldtTabla = fnc_EsquemaSolicitud()

                    Dim lblCodigoI As Label = CType(e.Item.FindControl("lblCodigoI"), Label)

                    lobjHESol.Codigo = lblCodigoI.Text
                    lobjHESol.Usuario = Session("@USUARIO")

                    'eliminar
                    If lobjHESol.fnc_Guardar(3, ldtTabla) = True Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se elimino la solicitud correctamente.');</script>")
                        Call fnc_Consultar(False)
                    Else
                        'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + lobjHESol.MensajeError + "');</script>")
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = lobjHESol.MensajeError
                    End If

                    ldtTabla = Nothing
                    lobjHESol = Nothing
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Footer
                Dim btnBuscarTrabF As HtmlInputButton = CType(e.Item.FindControl("btnBuscarTrabF"), HtmlInputButton)
                Dim btnAdd As ImageButton = CType(e.Item.FindControl("btnAdd"), ImageButton)

                Dim txtHESolF As TextBox = CType(e.Item.FindControl("txtHESolF"), TextBox)
                Dim txtTrabajadorF As TextBox = CType(e.Item.FindControl("txtTrabajadorF"), TextBox)
                Dim ddlTareaF As DropDownList = CType(e.Item.FindControl("ddlTareaF"), DropDownList)
                Dim txtObservacionF As TextBox = CType(e.Item.FindControl("txtObservacionF"), TextBox)


                txtTrabajadorF.Attributes.Add("onBlur", "txtTrabajador_onBlur('" & e.Item.ClientID.ToString & "','F');")
                ddlTareaF.Attributes.Add("onFocus", "ddlTarea_onFocus('" & e.Item.ClientID.ToString & "','F');")
                txtHESolF.Attributes.Add("onBlur", "txtHESol_onBlur('" & e.Item.ClientID.ToString & "','F');")
                txtObservacionF.Attributes.Add("onBlur", "txtObservacion_onBlur('" & e.Item.ClientID.ToString & "','F');")
                btnBuscarTrabF.Attributes.Add("onclick", "BuscarTrabajador('" & e.Item.ClientID.ToString & "','F')")
                btnAdd.Attributes.Add("onclick", "javascript:return fnc_Datagrid1_Validar('" & e.Item.ClientID.ToString & "','F')")


            Case ListItemType.EditItem

                Dim btnBuscarTrabE As HtmlInputButton = CType(e.Item.FindControl("btnBuscarTrabE"), HtmlInputButton)
                Dim btnUpdate As ImageButton = CType(e.Item.FindControl("btnUpdate"), ImageButton)
                Dim txtHESolE As TextBox = CType(e.Item.FindControl("txtHESolE"), TextBox)
                Dim txtObservacionE As TextBox = CType(e.Item.FindControl("txtObservacionE"), TextBox)
                Dim txtTrabajadorE As TextBox = CType(e.Item.FindControl("txtTrabajadorE"), TextBox)


                txtTrabajadorE.Attributes.Add("onBlur", "txtTrabajador_onBlur('" & e.Item.ClientID.ToString & "','E');")
                txtHESolE.Attributes.Add("onBlur", "txtHESol_onBlur('" & e.Item.ClientID.ToString & "','E');")
                txtObservacionE.Attributes.Add("onBlur", "txtObservacion_onBlur('" & e.Item.ClientID.ToString & "','E');")
                btnBuscarTrabE.Attributes.Add("onclick", "BuscarTrabajador('" & e.Item.ClientID.ToString & "','E')")
                btnUpdate.Attributes.Add("onclick", "javascript:return fnc_Datagrid1_Validar('" & e.Item.ClientID.ToString & "','E')")

            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                Dim lblCodigo As Label = CType(e.Item.FindControl("lblCodigoI"), Label)
                Dim hdnCodigo As HtmlInputHidden = CType(e.Item.FindControl("hdnCodigoI"), HtmlInputHidden)


                Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
                Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)

                lobjBoton.Attributes.Add("onClick", "fnc_mostrardetalle('" & ldrvItem("int_codigo") & "')")
                lobjBoton = Nothing

                hdnCodigo.Value = ldrvItem("int_codigo")
                btnDelete.Attributes.Add("onclick", "javascript:return fnc_Eliminar('" & e.Item.ClientID.ToString & "')")
        End Select
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        lblMensaje.Text = ""
        Call fnc_Consultar(False)
    End Sub
#End Region

#Region "-- Metodos --"

    Function fnc_SolicitarAprobacion()
        Dim larr_codigos() As String, lint_fila As Integer, ldtTabla As DataTable, ldrRow As DataRow
        Dim lobjHE As HorasExtras, ldtsResultado As New DataSet

        lblMensaje.ForeColor = Drawing.Color.Blue
        lblMensaje.Text = ""

        'validar que existan seleccionados
        If HDN5.Value.Length > 1 Then
            larr_codigos = HDN5.Value.Split("|")
            ldtTabla = fnc_EsquemaAprobacion()

            For lint_fila = 0 To larr_codigos.Length - 1
                If larr_codigos(lint_fila) <> "" Then
                    ldrRow = ldtTabla.NewRow
                    ldrRow("p1") = larr_codigos(lint_fila) 'int_codigo
                    ldtTabla.Rows.Add(ldrRow)
                End If
            Next
            ldrRow = Nothing
            Try
                If ldtTabla.Rows.Count > 0 Then
                    lobjHE = New HorasExtras

                    lobjHE.Codigo = 0
                    lobjHE.Usuario = Session("@USUARIO")

                    If lobjHE.fnc_SolicitarAprobacion(1, ldtTabla, ldtsResultado) = True Then
                        'enviar correo para aprobacion
                        EnviarCorreo(ldtsResultado.Tables(0))
                        Call fnc_Consultar(False)
                        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se envío a aprobar satisfactoriamente.');</script>")
                    Else
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = lobjHE.MensajeError
                    End If
                    lobjHE = Nothing
                End If
            Catch ex As Exception
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = lobjHE.MensajeError
            End Try
        End If
    End Function

    Function fnc_EsquemaSolicitud() As DataTable
        Dim ldtdRes As DataTable
        ldtdRes = New DataTable
        ldtdRes.Columns.Add("p1", GetType(Integer)) 'int_codigo
        ldtdRes.Columns.Add("p2", GetType(String)) 'chr_estado
        ldtdRes.Columns.Add("p3", GetType(String)) 'vch_codigoarea
        ldtdRes.Columns.Add("p4", GetType(String)) 'vch_solicitante
        ldtdRes.Columns.Add("p5", GetType(String)) 'dtm_fecha_sol
        ldtdRes.Columns.Add("p6", GetType(String)) 'vch_codtrabajador
        ldtdRes.Columns.Add("p7", GetType(String)) 'chr_tarea
        ldtdRes.Columns.Add("p8", GetType(String)) 'vch_codctc
        ldtdRes.Columns.Add("p9", GetType(Integer)) 'tin_horext_sol
        ldtdRes.Columns.Add("p10", GetType(String)) 'vch_motivo
        ldtdRes.Columns.Add("p11", GetType(String)) 'vch_observacion
        ldtdRes.Columns.Add("p12", GetType(String)) 'vch_inicioaprox
        Return ldtdRes
    End Function

    Function fnc_EsquemaAprobacion() As DataTable
        Dim ldtdRes As DataTable
        ldtdRes = New DataTable
        ldtdRes.Columns.Add("p1", GetType(Integer)) 'int_codigo
        Return ldtdRes
    End Function

    Sub fnc_Consultar(ByVal pbln_esquema As Boolean)
        Dim lobj_planilla As New HorasExtras
        Dim ldts_datos As New DataSet, lstr_fecha1 As String, lstr_fecha2 As String, lstr_situacion As String = ""

        Try
            'trae el esquema solo se utiliza al cargar los datos y carga datos de area
            If pbln_esquema = True Then
                txtArea.Text = ""
                HDN1.Value = ""

                If lobj_planilla.fnc_Listar(ldts_datos, 0, "", "", "", "", Session("@USUARIO")) = True Then
                    Session("PLA20009") = ldts_datos.Tables(0)
                    txtArea.Text = ldts_datos.Tables(1).Rows(0)("vch_desarea")
                    HDN1.Value = ldts_datos.Tables(1).Rows(0)("vch_codarea")
                End If

            Else 'consulta los datos segun parametros
                lstr_fecha1 = IIf(chkdesde.Checked = True, Right(txtfechaini.Text, 4) + Mid(txtfechaini.Text, 4, 2) + Left(txtfechaini.Text, 2), "")
                lstr_fecha2 = IIf(chkhasta.Checked = True, Right(txtfechafin.Text, 4) + Mid(txtfechafin.Text, 4, 2) + Left(txtfechafin.Text, 2), "")

                If chkSitACT.Checked = True Then lstr_situacion = lstr_situacion + "ACT,"
                If chkSitENV.Checked = True Then lstr_situacion = lstr_situacion + "ENV,"
                If chkSitAPR.Checked = True Then lstr_situacion = lstr_situacion + "APR,"
                If chkSitREC.Checked = True Then lstr_situacion = lstr_situacion + "REC,"
                If chkSitANU.Checked = True Then lstr_situacion = lstr_situacion + "ANU,"

                If lobj_planilla.fnc_Listar(ldts_datos, 1, HDN1.Value, lstr_fecha1, lstr_fecha2, lstr_situacion, "") = True Then
                    Session("PLA20009") = ldts_datos.Tables(0)
                End If
                ldts_datos = Nothing
                lobj_planilla = Nothing
            End If

            Call CargarGrilla()

        Catch ex As Exception
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = ex.Message.Replace("'", "")
        Finally
        End Try
    End Sub

    Sub CargarGrilla()
        Dim dtbDatos As DataTable = CType(Session("PLA20009"), DataTable)
        DataGrid1.DataSource = dtbDatos
        DataGrid1.DataBind()
    End Sub

    Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable)
        Dim i As Integer, lstrCuerpoMensaje As String = "", lstrTitulo As String
        Dim mailMsg As System.Net.Mail.MailMessage, lstrPara As String = "", lstrCopia As String = ""
        Try
            Dim objCorreo As New clsCorreo

            For i = 0 To pdtCorreos.Rows.Count - 1
                If lstrPara.Trim.Length = 0 Then
                    lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                Else
                    lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
                End If
            Next i

            lstrCuerpoMensaje = ""
            lstrTitulo = ""
            With pdtCorreos.Rows(0)
                lstrTitulo = "Solicitud de Horas Extras por aprobar"
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                "APROBACION PARA LA SIGUIENTE&nbsp;SOLICITUD DE HORAS EXTRAS : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                "<STRONG>" & .Item("Numero") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                "</STRONG></FONT></FONT></P>" + _
                "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
                "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                "Por favor no responder este correo.<BR>" + _
                "Departamento de Sistemas<BR>" + _
                "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                "-------------------------------------------------------------------------------</P>"

                mailMsg = New System.Net.Mail.MailMessage()

                'Configurar arreglo para el TO
                Dim lstrTo_arreglo() As String = lstrPara.Split(";")

                'Si no hay destinatario que lo envie a sistemas
                If lstrTo_arreglo.Count <= 0 Then lstrCopia = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("CorreoCC").ToString()

                If String.IsNullOrEmpty(lstrCopia) Then
                    For lintIndice = 0 To lstrTo_arreglo.Length - 1
                        objCorreo.ServicioEnvioCorreo(lstrTo_arreglo(lintIndice), lstrCopia, "", lstrTitulo, lstrCuerpoMensaje)
                    Next
                Else
                    objCorreo.ServicioEnvioCorreo(lstrCopia, "", "", lstrTitulo, lstrCuerpoMensaje)
                End If
            End With
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function fnc_DatosTrabajador(ByVal pstrCodigo As String) As DataTable
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion.htm")
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        Dim lobjTrabajador As New OFISIS.OFIPLAN.Trabajador("01", pstrCodigo)
        Dim lstrError As String = "", ldtbRetorno As New DataTable

        Try
            lobjTrabajador.Listar(ldtbRetorno, "", pstrCodigo, "")
        Catch ex As Exception

        End Try

        lobjTrabajador = Nothing
        Return ldtbRetorno
    End Function

#End Region
End Class