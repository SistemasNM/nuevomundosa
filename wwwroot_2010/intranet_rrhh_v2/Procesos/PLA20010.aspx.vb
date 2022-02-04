Imports NuevoMundo.Planillas
Imports System.Drawing
Imports NuevoMundo

Public Class PLA20010

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
    Protected WithEvents lblDesArea As System.Web.UI.WebControls.Label
    Protected WithEvents dgSeguimiento As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnRechazar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAprobar As System.Web.UI.WebControls.Button
    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label
    Protected WithEvents txtobservacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid



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
            Dim lstr_CodigoSolicitud As String = ""
            lstr_CodigoSolicitud = Request("pstrCodigoSolicitud")

            HDN1.Value = lstr_CodigoSolicitud '"2011-0010" 'pstrCodigoSolicitud
            Call fnc_Consultar()

            btnAprobar.Attributes.Add("onClick", "javascript:return fnc_datosgrilla();")
            'btnGrabar.Attributes.Add("onClick", "javascript:return fnc_ValidarRequisicion();")
            'btnAnular.Attributes.Add("onClick", "javascript:return fnc_ConfirmarAnulacion();")
            HDN4.Value = "DataGrid1_"
        End If
        'txtAreaSolicitanteNombre.Text = Me.hdnAreaSolicitante.Value
        'HDN1-->codigo de solicitud
        'HDN2-->instancia SI,NO--> esta en el ultimo proceso y puede modificar las horas de aprobacion
        'HDN3-->
        'HDN4-->codigo de clientID del datagrid 
        'HDN5-->
    End Sub

    Private Sub DataGrid1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        Select Case e.Item.ItemType

            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)

                Dim lblCtcI As Label = CType(e.Item.FindControl("lblCtcI"), Label)

                If lblCtcI.Text.Trim <> "" Then
                    lblCtcI.Attributes.Add("onmouseover", "toolTip('" & ldrvItem("vch_desctc") & "',this)")
                End If

                Dim txtHESolE As TextBox = CType(e.Item.FindControl("txtHESolI"), TextBox)
                Dim lblHESolE As Label = CType(e.Item.FindControl("lblHESolI"), Label)

                Dim txtHEAprE As TextBox = CType(e.Item.FindControl("txtHEAprI"), TextBox)
                Dim lblHEAprE As Label = CType(e.Item.FindControl("lblHEAprI"), Label)

                If HDN2.Value = "SI" Then 'ES ULTIMO APROBACION
                    lblHESolE.Visible = True
                    txtHEAprE.Visible = True
                    txtHEAprE.Attributes.Add("onBlur", "txtHEApr_onBlur('" & e.Item.ClientID.ToString & "','I'," & ldrvItem("tin_horext_sol") & "," & e.Item.ItemIndex.ToString() & ");")
                Else
                    lblHEAprE.Visible = True
                    txtHESolE.Visible = True
                    txtHESolE.Attributes.Add("onBlur", "txtHESol_onBlur('" & e.Item.ClientID.ToString & "','I'," & ldrvItem("tin_horext_sol") & "," & e.Item.ItemIndex.ToString() & ");")
                End If

                Dim hdnCodigo As HtmlInputHidden = CType(e.Item.FindControl("hdnCodigoI"), HtmlInputHidden)


                hdnCodigo.Value = ldrvItem("int_codigo")
        End Select
    End Sub

    Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click

        Dim lobjHorasExtras As HorasExtras
        Dim strEstado As String, ldtsDatos As New DataSet
        Dim ldbtAprobacion As New DataTable, n As Integer, ldtrFila As DataRow
        Dim lstrCodigos() As String, lstrCantidades() As String
        strEstado = ""


        lstrCodigos = HDN3.Value.Split("|")
        lstrCantidades = HDN5.Value.Split("|")

        lobjHorasExtras = New HorasExtras
        lobjHorasExtras.CodigoSol = HDN1.Value

        ldbtAprobacion = fnc_EsquemaAprobacion()

        For n = 0 To lstrCodigos.Length - 2
            ldtrFila = ldbtAprobacion.NewRow
            ldtrFila("p1") = lstrCodigos(n) 'codigo
            ldtrFila("p2") = lstrCantidades(n) 'cantidad
            ldbtAprobacion.Rows.Add(ldtrFila)
            ldtrFila = Nothing
        Next

        lblMensaje.Text = ""
        lobjHorasExtras.Usuario = Session("@USUARIO")
        If lobjHorasExtras.fnc_Aprobar("APR", ldbtAprobacion, ldtsDatos) = True Then

            If ldtsDatos.Tables(0).Rows.Count > 0 Then

                Try
                    'Enviamos email informativo
                    Call EnviarCorreo(ldtsDatos.Tables(0), "APR")
                Catch ex As Exception
                    'EnviaCorreoError(ex.ToString)
                End Try
            End If
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
        Else
            lblMensaje.ForeColor = Color.Red
            lblMensaje.Text = lobjHorasExtras.MensajeError
        End If
    End Sub

    Private Sub btnRechazar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRechazar.Click
        Dim lobjHorasExtras As HorasExtras
        Dim strEstado As String, ldtsDatos As DataSet
        Dim ldbtAprobacion As New DataTable, n As Integer, ldtrFila As DataRow
        Dim lstrCodigos() As String, lstrCantidades() As String


        lobjHorasExtras = New HorasExtras
        lobjHorasExtras.CodigoSol = HDN1.Value

        ldbtAprobacion = fnc_EsquemaAprobacion()

        lblMensaje.Text = ""
        lobjHorasExtras.Usuario = Session("@USUARIO")
        If lobjHorasExtras.fnc_Desaprobar("REC", txtobservacion.Text, ldtsDatos) = True Then

            If ldtsDatos.Tables(0).Rows.Count > 0 Then

                Try
                    'Enviamos email informativo
                    Call EnviarCorreo(ldtsDatos.Tables(0), "REC")
                Catch ex As Exception
                    'EnviaCorreoError(ex.ToString)
                End Try
            End If
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
        Else
            lblMensaje.ForeColor = Color.Red
            lblMensaje.Text = lobjHorasExtras.MensajeError
        End If
    End Sub

#End Region

#Region "-- Metodos --"

    Function fnc_EsquemaAprobacion() As DataTable
        Dim ldtdRes As DataTable
        ldtdRes = New DataTable
        ldtdRes.Columns.Add("p1", GetType(Integer)) 'int_codigo
        ldtdRes.Columns.Add("p2", GetType(Integer)) 'cantidad horas
        Return ldtdRes
    End Function

    Function fnc_Consultar()
        Dim lobj_planilla As New HorasExtras
        Dim ldts_datos As DataSet, ldtb_segui As DataTable

        If HDN1.Value.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No existe número de solicitud.');</script>")
            Exit Function
        End If

        Try
            'detalle de seguimiento
            lobj_planilla.CodigoSol = HDN1.Value.Trim
            lobj_planilla.SoliSecuSol = 0
            If lobj_planilla.fnc_ListarSeguimientoAprob(1, ldtb_segui) = "" Then
                dgSeguimiento.DataSource = ldtb_segui
                dgSeguimiento.DataBind()
            End If

            'detalle de registro
            If lobj_planilla.fnc_Listar(ldts_datos, 2, HDN1.Value, "", "", "", "") = True Then
                ldtb_segui = CType(ldts_datos.Tables(0), DataTable)

                If ldtb_segui.Rows.Count > 0 Then
                    lblDesArea.Text = ldtb_segui.Rows(0).Item("vch_desarea")
                    HDN2.Value = ldtb_segui.Rows(0).Item("vch_esultimo") 'SI,NO
                End If



                DataGrid1.DataSource = ldtb_segui
                DataGrid1.DataBind()
            End If

            'datos del area

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
        Finally

        End Try

        ldts_datos = Nothing
        lobj_planilla = Nothing
    End Function

    Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable, ByVal pstrAccion As String)
        Dim i As Integer, lstrCuerpoMensaje As String = "", lstrTitulo As String
        Dim lstrPara As String = "", lstrCopia As String = ""
        Dim objCorreo As New clsCorreo
        Try
            For i = 0 To pdtCorreos.Rows.Count - 1
                If lstrPara.Trim.Length = 0 Then
                    lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                Else
                    lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
                End If
                'lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
            Next i

            lstrCuerpoMensaje = ""
            lstrTitulo = ""

            With pdtCorreos.Rows(0)
                'lstrTitulo = "Req. " + .Item("NumeroRequisicion") + " por aprobar."
                If (pstrAccion = "APR") Then
                    lstrTitulo = "Solicitud de Horas Extras por aprobar"
                Else 'desaprobar
                    lstrTitulo = "Solicitud de Horas Extras desaprobadas"
                End If


                If (pstrAccion = "APR") Then
                    If .Item("Tipo") = 1 Then 'final
                        lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA APROBADO " + _
                    "LA SOLICITUD DE HORAS EXTRAS : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                    "<STRONG>" & .Item("Numero") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                    "</STRONG></FONT></FONT></P>" + _
                    "<BR><BR></FONT><BR>" + _
                    "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
                    "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                    "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                    "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                    "Por favor no responder este correo.<BR>" + _
                    "Departamento de Sistemas<BR>" + _
                    "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                    "-------------------------------------------------------------------------------</P>"
                    Else 'sgte en aprobar
                        lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                                            "APROBACION PARA LA SIGUIENTE&nbsp;SOLICITUD DE HORAS EXTRAS : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                            "<STRONG>" & .Item("Numero") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                            "</STRONG></FONT></FONT></P>" + _
                                            "<BR><BR></FONT><BR>" + _
                                            "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
                                            "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                            "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                            "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                                            "Por favor no responder este correo.<BR>" + _
                                            "Departamento de Sistemas<BR>" + _
                                            "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                            "-------------------------------------------------------------------------------</P>"
                    End If
                Else 'desaprobadas
                    lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA DESAPROBADO UNA SOLICITUD DE " + _
                                        " HORAS EXTRAS : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & .Item("Numero") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG><BR><BR>POR EL USUARIO :<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & Session("@USUARIO") & "</STRONG></FONT><BR><BR>MOTIVO DE DESAPROBACION :<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & txtobservacion.Text & "</STRONG></FONT></FONT></FONT></P>" + _
                                        "<BR><BR></FONT><BR>" + _
                                        "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
                                        "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                        "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                                        "Por favor no responder este correo.<BR>" + _
                                        "Departamento de Sistemas<BR>" + _
                                        "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                        "-------------------------------------------------------------------------------</P>"
                End If

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

    Function LimpiarObjetos()
        'Me.txtAreaSolicitanteCodigo.Text = ""
        'Me.txtAreaSolicitanteNombre.Text = ""
        'Me.hdnAreaSolicitante.Value = ""
        'Me.txtObservacion.Text = ""
        'Me.txtNumero.Text = ""
        'Me.txtSituacion.Text = ""
        ''If Me.RdArticulo.Checked = True Then
        'If ddlTipo.SelectedValue = "ART" Then
        '    Me.txtSerie.Text = "0001"
        '    Me.CuentaGasto.Visible = False
        '    Me.CheckStock.Checked = False
        '    Me.Stock.Visible = True
        '    'Me.RdArticulo.Checked = True
        '    ddlTipo.SelectedValue = "ART"
        '    Me.DataGrid1.Visible = True
        '    Me.DataGrid2.Visible = False
        'Else
        '    Me.txtSerie.Text = "0002"
        '    Me.CuentaGasto.Visible = True
        '    Me.Stock.Visible = False
        '    'Me.RdServicio.Checked = True
        '    ddlTipo.SelectedValue = "SER"
        '    Me.DataGrid1.Visible = False
        '    Me.DataGrid2.Visible = True
        'End If
        'DataGrid1.Columns(6).Visible = True
        'DataGrid2.Columns(6).Visible = True
        'Me.BtnNuevo.Visible = True
        'Me.btnGrabar.Visible = False
        'Me.btnAnular.Visible = False
        'Me.btnSolicitar.Visible = False

        'Dim LdtDetalle As DataTable, obj As OFISIS.OFILOGI.Articulos
        'obj = New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        'LdtDetalle = obj.EsquemaDetalle()
        'Session("LOG00001_utbDetalle") = LdtDetalle
        'Call CargarGrilla()
        'Session.Add("Nuevo", True)
        'obj = Nothing
        'LdtDetalle = Nothing
    End Function

#End Region


End Class

