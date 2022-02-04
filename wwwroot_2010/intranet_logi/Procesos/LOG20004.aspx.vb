Imports System
Imports System.IO
Imports NuevoMundo

Public Class LOG20004
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents imgDesde As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents imgHasta As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents hdn1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtHasta As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents rbtTodos As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtArticulos As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtServicios As System.Web.UI.WebControls.RadioButton
    Protected WithEvents lblSolicitada As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesde As System.Web.UI.WebControls.TextBox
    Protected WithEvents cbxHasta As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbxDesde As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtSolicitador As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSolicitador As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents Posicionador1 As NM.Posicionador.Posicionador
    Protected WithEvents cblRequisicionesEstados As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents txtProveedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnProveedor As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblProveedor As System.Web.UI.WebControls.Label
    Protected WithEvents hdnSolicitador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnProveedor As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnOrdenCompra As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents rbtdocreq As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtdococm As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtdocserie As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtdocnumero As System.Web.UI.WebControls.TextBox

    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
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
            'Session("@USUARIO") = "AAMPUERP"

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
        Ajax.Utility.RegisterTypeForAjax(GetType(LOG20004))
        If Not IsPostBack Then
            Inicializar()
            'Me.btnSolicitador.Visible = False
            txtSolicitador.Attributes.Add("onBlur", "BuscarUsuario();")
            txtProveedor.Attributes.Add("onBlur", "BuscarProveedor();")
            txtdocserie.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
            txtdocnumero.Attributes.Add("onBlur", "FormatearBusqDoc(2);")

            '20120906 EPM Readonly
            txtDesde.Attributes.Add("readonly", "readonly")
            txtHasta.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Sub cbxDesde_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxDesde.CheckedChanged
        If cbxDesde.Checked Then
            txtDesde.Enabled = True
            Me.imgDesde.Disabled = False
        Else
            txtDesde.Enabled = False
            Me.imgDesde.Disabled = True
        End If
    End Sub

    Private Sub cbxHasta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxHasta.CheckedChanged
        If cbxHasta.Checked Then
            txtHasta.Enabled = True
            Me.imgHasta.Disabled = False
        Else
            txtHasta.Enabled = False
            Me.imgHasta.Disabled = True
        End If

    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Listar()
    End Sub

    Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        Dim libtBloquear As ImageButton
        Dim libtDesbloquear As ImageButton
        Dim libtEnviarOCM As ImageButton
        Dim ldrvRow As DataRowView

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                ldrvRow = CType(e.Item.DataItem, DataRowView)
                libtBloquear = CType(e.Item.FindControl("ibtBloquear"), ImageButton)
                libtDesbloquear = CType(e.Item.FindControl("ibtDesbloquear"), ImageButton)
                libtEnviarOCM = CType(e.Item.FindControl("ibtenviarcorreo"), ImageButton)
                Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
                If ldrvRow("ti_situ") = "APR" Then
                    libtEnviarOCM.Visible = True
                    libtEnviarOCM.Attributes.Add("onClick", "javascript:return EnviarOC_onClick('" & ldrvRow("var_OrdenCompra") & "')")
                End If
                lobjBoton.Attributes.Add("onClick", "MostrarDetalle('" & ldrvRow("var_Documento") & "','" & ldrvRow("var_OrdenCompra") & "')")
                'If ldrvRow("var_OrdenCompra") = "" Then
                '  'lobjBoton.Attributes.Add("onClick", "MostrarRequisicion('" + ldrvRow("var_Documento") + "','" + ldrvRow("var_Estado") + "')")
                '  lobjBoton.Attributes.Add("onClick", "MostrarDetalle('" + ldrvRow("var_Documento") + "')")
                'Else
                '  If ldrvRow("var_Documento") = "" Then
                '    lobjBoton.Attributes.Add("onClick", "MostrarOrdenImpresion('" + ldrvRow("var_OrdenCompra") + "')")
                '  Else
                '    lobjBoton.Attributes.Add("onClick", "MostrarDetalle('" + ldrvRow("var_Documento") + "')")
                '  End If
                'End If
                lobjBoton = Nothing
                If ldrvRow("int_Bloqueado") = 0 Then
                    libtBloquear.Visible = True
                    libtDesbloquear.Visible = False
                Else
                    libtBloquear.Visible = False
                    libtDesbloquear.Visible = True
                End If
                If UCase(ldrvRow("int_Estado")) <> 4 Then
                    libtBloquear.Visible = False
                    libtDesbloquear.Visible = False
                End If
        End Select
    End Sub

    Private Sub dtgLista_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgLista.ItemCommand
        Dim lobjAprobacion As OFISIS.OFILOGI.Requisiciones
        Dim lstrDocumento As String = ""
        Dim strEstado As String
        Dim ldbtAprobacion As New DataTable
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones
        Try
            Select Case UCase(e.CommandName)
                Case "ENVIAROC"
                    lstrDocumento = hdnOrdenCompra.Value
                    lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
                    ldbtAprobacion = lobjOCOS.Verifica_OC_Proveedor(lstrDocumento)
                    strEstado = ldbtAprobacion.Rows(0).Item("st_apro").ToString
                    If strEstado = "APR" Then
                        ' Enviamos la OC por email al proveedor
                        Call prc_EnviarOC_Proveedor_V2(lstrDocumento)
                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script language='javascript'>alert('No es posible enviar este documento al Proveedor, falta Aprobar.');</script>")
                    End If
                Case "BLOQUEAR"
                    lobjAprobacion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
                    lobjAprobacion.Codigo = dtgLista.Items(e.Item.ItemIndex).Cells(6).Text
                    If lobjAprobacion.CotizacionIniciar() Then
                        Listar()
                    End If
                Case "DESBLOQUEAR"
                    lobjAprobacion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
                    lobjAprobacion.Codigo = dtgLista.Items(e.Item.ItemIndex).Cells(6).Text
                    If lobjAprobacion.CotizacionAnular() Then
                        Listar()
                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script language='javascript'>alert('La requisición -- " & lobjAprobacion.Codigo & "  -- no existe o ya tiene una O/C relacionada.');</script>")
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "-- Metodos --"

    Private Function ArmarSetFiltros() As String
        Dim ldtRes As DataTable
        Dim ldrFila As DataRow
        Dim lstrRes As String = ""
        Dim i As Integer
        Dim lobjUtil As New NuevoMundo.Generales.Objetos

        ldtRes = New DataTable("Filtros")
        ldtRes.Columns.Add("var_Codigo", GetType(String))
        ldtRes.Columns.Add("int_Seleccion", GetType(Integer))
        For i = 0 To cblRequisicionesEstados.Items.Count - 1
            ldrFila = ldtRes.NewRow
            ldrFila("var_Codigo") = cblRequisicionesEstados.Items(i).Value
            ldrFila("int_Seleccion") = IIf(cblRequisicionesEstados.Items(i).Selected, 1, 0)
            ldtRes.Rows.Add(ldrFila)
            ldrFila = Nothing
        Next i
        lobjUtil(ldtRes).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrRes)
        Return lstrRes
    End Function

    Private Sub Listar()
        Dim lobjAprobacion As OFISIS.OFILOGI.Requisiciones
        Dim lstrDesde As String, lstrHasta As String
        Dim lintTipo As Integer, lstrObservaciones As String

        If rbtTodos.Checked Then lintTipo = 0
        If rbtArticulos.Checked Then lintTipo = 1
        If rbtServicios.Checked Then lintTipo = 2
        'verificar si es busqueda directa
        If (txtdocserie.Text.Trim & "" & txtdocnumero.Text.Trim).Length > 0 Then
            lintTipo = 3
            If (txtdocserie.Text.Trim & "" & txtdocnumero.Text.Trim).Length < 10 Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('La busqueda del documento no es correcto : " & (txtdocserie.Text.Trim & "" & txtdocnumero.Text.Trim) & "');</script>")
                Exit Sub
            End If
        End If

        If cbxDesde.Checked Then
            lstrDesde = Format(CDate(txtDesde.Text), "yyyyMMdd")
        Else
            'lstrDesde = "00000000"
            lstrDesde = Format(CDate(txtDesde.Text), "yyyyMMdd")
        End If
        If cbxHasta.Checked Then
            lstrHasta = Format(CDate(txtHasta.Text), "yyyyMMdd")
        Else
            'lstrHasta = "99999999"
            lstrHasta = Format(CDate(txtHasta.Text), "yyyyMMdd")
        End If

        '-- formatear documento directo
        'If (txtdocserie.Text.Trim.Length > 0) Then
        '    txtdocserie.Text = Right("0000" + txtdocserie.Text, 4)
        'Else
        '    txtdocserie.Text = ""
        'End If

        'If (txtdocnumero.Text.Trim.Length > 0) Then
        '    txtdocnumero.Text = Right("0000000000" + txtdocnumero.Text, 10)
        'Else
        '    txtdocnumero.Text = ""
        'End If

        If lintTipo = 3 Then 'si es busqueda directa envia el tipo y num de documento
            lstrObservaciones = IIf(rbtdococm.Checked = True, "OCM", "REQ") & txtdocserie.Text & "-" & txtdocnumero.Text
        Else 'sino observaciones del cuadro de texto
            lstrObservaciones = txtObservaciones.Text.Trim
        End If

        '-- formatear documento directo
        lobjAprobacion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        'lobjAprobacion.Listar(OFISIS.OFILOGI.Requisiciones.enuTiposLista.Seguimiento, "", lstrDesde, lstrHasta, txtSolicitador.Text, lintTipo, ArmarSetFiltros, lstrObservaciones, txtProveedor.Text)
        lobjAprobacion.Listar(OFISIS.OFILOGI.Requisiciones.enuTiposLista.Seguimiento, "", lstrDesde, lstrHasta, txtSolicitador.Text, lintTipo, ArmarSetFiltros, lstrObservaciones, txtProveedor.Text)
        dtgLista.DataSource = lobjAprobacion.SetDatos.Tables(0)
        dtgLista.DataBind()
        If lobjAprobacion.SetDatos.Tables(0).Rows.Count > 0 Then
            lblTotal.Text = Format(lobjAprobacion.SetDatos.Tables(0).Compute("SUM(int_ImporteSoles)", ""), "##,##00.00")
        End If
        'lblTotal.Text = IIf(IsDBNull(lobjAprobacion.SetDatos.Tables(0).Compute("SUM(int_ImporteSoles)", "")), "0", Format(lobjAprobacion.SetDatos.Tables(0).Compute("SUM(int_ImporteSoles)", ""), "##,##00.00"))
        lobjAprobacion = Nothing
    End Sub

    Private Sub Inicializar()
        Dim lobjAprobacion As OFISIS.OFILOGI.Requisiciones
        Dim i As Integer
        Dim lobjUtil As NuevoMundo.Generales.RutinasGlobales.Varios
        lobjAprobacion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjAprobacion.Estados.Listar()
        cblRequisicionesEstados.DataSource = lobjAprobacion.Estados.SetDatos.Tables(0)
        cblRequisicionesEstados.DataTextField = "var_Descripcion"
        cblRequisicionesEstados.DataValueField = "var_Codigo"
        cblRequisicionesEstados.DataBind()
        For i = 0 To lobjAprobacion.Estados.SetDatos.Tables(0).Rows.Count - 1
            If lobjAprobacion.Estados.SetDatos.Tables(0).Rows(i)("int_Seleccion") = 1 Then
                cblRequisicionesEstados.Items(i).Selected = True
            Else
                cblRequisicionesEstados.Items(i).Selected = False
            End If
        Next i
        lobjAprobacion = Nothing
        cbxDesde.Checked = False
        cbxHasta.Checked = True
        lobjUtil = New NuevoMundo.Generales.RutinasGlobales.Varios
        txtDesde.Text = Format(lobjUtil.PrimerDiaMes(Now.Year, Now.Month), "dd/MM/yyyy")
        txtHasta.Text = Format(lobjUtil.UltimoDiaMes(Now.Year, Now.Month), "dd/MM/yyyy")
        lobjUtil = Nothing
        'ddlEstado.SelectedValue = "000"
        cbxDesde_CheckedChanged(Nothing, Nothing)
        cbxHasta_CheckedChanged(Nothing, Nothing)
    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function fnc_VerificarOC_paraEnvio(ByVal pstrOrdenCompra As String) As DataTable
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim ldtbDatos As DataTable
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion_popup.htm")
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        Try
            ldtbDatos = lobjOrdenCompra.fnc_Listar(1, pstrOrdenCompra)
            Session("datosoc_001") = ldtbDatos
            Return ldtbDatos
        Catch ex As Exception
        Finally
        End Try
    End Function

    Public Sub prc_EnviarOC_Proveedor_V2(ByVal pstrDocumento As String)
        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String
        Dim lstrRutaFile_SQL As String
        Dim lstrFile_SQL As String

        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra, ldtbDatosOC As DataTable
        Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable

        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = ""        
        Dim lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim lstrEmailDestino As String = "", lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
        Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
        Dim lstrError As String = ""
        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        ldtbDatosOC = CType(Session("datosoc_001"), DataTable)
        lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        lstrNumeroOC = pstrDocumento
        lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        'hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        lstrRutaFile_SQL = ldtbRuta.Rows(0).Item("OCO_RUTADOCS_GUARDAR_SQL").ToString

        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"
        lstrFile_SQL = lstrNombreFile & ".pdf"

        'obtener ruta donde guardar
        '-----------------------------------------------------------------------------------------
        '--FINAL: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: CONVERTIR A PDF
        '-----------------------------------------------------------------------------------------
        Try
            lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing
            lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrMailTO = IIf(lstrEmailDestino.Trim = "", "sistemas@nuevomundosa.com", lstrEmailDestino.Trim)
            lstrMailCC = ""
            lstrMailBCC = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_CCO").ToString()

            lstrMailSubject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC

            lstrMailBody = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
                            "<P><FONT face='Verdana' size='2'><STRONG>" + ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString + "</STRONG></FONT></P>" + _
                            "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la adjunta solicitud de compra <STRONG>" + _
                            "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
                            "<BR><BR>" + _
                            "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
                            "<BR>" + _
                            "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
                            "<BR>" + _
                            "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
                            "<BR>" + _
                            "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
                            "<P><FONT size='2'></FONT></P>" + _
                            "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
                            "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
                            "Por favor no responder este correo.<BR>" + _
                            "-------------------------------------------------------------------------------</FONT></P>"


            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody, lstrRutaFile_SQL, lstrFile_SQL)

        Catch ex As Exception
            lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
            'lstrError = ex.Message
        Finally
            objCorreo = Nothing
        End Try

        If lstrError.Length > 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
            Exit Sub
        End If
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('La orden de servicio -- " & lstrNumeroOC & " -- ha sido enviado al proveedor.');</script>")
        '-----------------------------------------------------------------------------------------
        '--FINAL: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: ACTUALIZAR METADATA DE ENVIO EN OC
        '-----------------------------------------------------------------------------------------
        Try
            lstrUsuario = IIf(IsNothing(Session.Item("@USUARIO")), "", Session.Item("@USUARIO"))
            ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
            '-----------------------------------------------------------------------------------------
            '--FINAL: ACTUALIZAR METADATA DE ENVIO EN OC
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
            If File.Exists(lstrRutaFile) Then
                File.Delete(lstrRutaFile)
            End If
            '-----------------------------------------------------------------------------------------
            '--FINAL: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
        Catch ex As Exception

        Finally
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing

        End Try
    End Sub


    Public Sub prc_EnviarOC_Proveedor(ByVal pstrDocumento As String)
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra, ldtbDatosOC As DataTable
        Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable

        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim lstrEmailDestino As String = "", lstrCuerpoMensaje As String = ""
        Dim lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
        Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
        Dim lstrError As String = ""
        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        ldtbDatosOC = CType(Session("datosoc_001"), DataTable)
        lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString
        '-----------------------------------------------------------------------------------------
        '--FINAL: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        lstrNumeroOC = pstrDocumento
        lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
        'hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"
        'obtener ruta donde guardar
        '-----------------------------------------------------------------------------------------
        '--FINAL: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: CONVERTIR A PDF
        '-----------------------------------------------------------------------------------------
        Try
            lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
            lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
            lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
            lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
            lobjUtil = Nothing
            lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
            lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
            lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
            lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
            lobjOpcDisco.DiskFileName = lstrRutaFile
            lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
            lrptOrdenCompra.Export()
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing
            '-----------------------------------------------------------------------------------------
            '--FINAL: CONVERTIR A PDF
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
            '-----------------------------------------------------------------------------------------

            lstrCuerpoMensaje = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
        "<P><FONT face='Verdana' size='2'><STRONG>" + ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString + "</STRONG></FONT></P>" + _
        "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la adjunta solicitud de compra <STRONG>" + _
        "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
        "<BR><BR>" + _
        "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
        "<BR>" + _
        "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
        "<BR>" + _
        "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
        "<BR>" + _
        "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
        "<P><FONT size='2'></FONT></P>" + _
        "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
        "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
        "Por favor no responder este correo.<BR>" + _
        "-------------------------------------------------------------------------------</FONT></P>"

            Dim mailMsg As System.Net.Mail.MailMessage
            mailMsg = New System.Net.Mail.MailMessage()
            Dim lobjAdjunto As System.Net.Mail.Attachment

            '20121005 EPM Configurar arreglo para el To
            Dim lstrTo_arreglo() As String = lstrEmailDestino.Split(";")
            For lintIndice = 0 To lstrTo_arreglo.Length - 1
                If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            Next
            'Si no hay destinatario que lo envie a sistemas
            If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")


            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With mailMsg
                '.From = New System.Net.Mail.MailAddress("Horas Extras<aprobaciones@nuevomundosa.com>")
                .From = New System.Net.Mail.MailAddress(user)
                .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
                .Body = lstrCuerpoMensaje
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
                lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
                .Attachments.Add(lobjAdjunto)
            End With

            Dim Servidor As New System.Net.Mail.SmtpClient
            Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                Servidor.Credentials = userCredential
            End If

            Servidor.Send(mailMsg)
            Servidor = Nothing
            lobjAdjunto = Nothing


        Catch ex As Exception
            lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
        End Try
        If lstrError.Length > 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
            Exit Sub
        End If
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('La orden de servicio -- " & lstrNumeroOC & " -- ha sido enviado al proveedor.');</script>")
        '-----------------------------------------------------------------------------------------
        '--FINAL: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '--INICIO: ACTUALIZAR METADATA DE ENVIO EN OC
        '-----------------------------------------------------------------------------------------
        Try
            lstrUsuario = IIf(IsNothing(Session.Item("@USUARIO")), "", Session.Item("@USUARIO"))
            ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
            '-----------------------------------------------------------------------------------------
            '--FINAL: ACTUALIZAR METADATA DE ENVIO EN OC
            '-----------------------------------------------------------------------------------------
            '-----------------------------------------------------------------------------------------
            '--INICIO: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
            If File.Exists(lstrRutaFile) Then
                File.Delete(lstrRutaFile)
            End If
            '-----------------------------------------------------------------------------------------
            '--FINAL: ELIMINAR ARCHIVO PDF
            '-----------------------------------------------------------------------------------------
        Catch ex As Exception

        Finally
            lrptOrdenCompra = Nothing
            lobjOpcDisco = Nothing

        End Try
    End Sub

#End Region

End Class

