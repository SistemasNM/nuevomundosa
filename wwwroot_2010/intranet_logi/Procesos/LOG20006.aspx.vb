Imports System.IO

Public Class LOG20006
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnRechazar As System.Web.UI.WebControls.Button
    Protected WithEvents btnDesaprobar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAprobar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnular As System.Web.UI.WebControls.Button
    Protected WithEvents btnImprimir As System.Web.UI.WebControls.Button
    Protected WithEvents btnListaAdjuntos As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents tblCentroCosto As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtAlmacen As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtBase As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFOB As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFlete As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSeguro As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtSubTotal As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIGV As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotal As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnHistorial As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSeguimiento As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents txtMoneda As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProveedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescuento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCondicion As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCreacion As System.Web.UI.WebControls.Label
    Protected WithEvents pnldatosestadisticos As System.Web.UI.WebControls.Panel
    Protected WithEvents dgestconsumo As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents hdnarticuloestadistica As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgestcompras As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnposicionpanel As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txteststockactual As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtestconsumopromedio As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAnalisis As System.Web.UI.WebControls.Button
    Protected WithEvents txtObservaciones2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblMsgDesaprobacion As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents hdnNumReq As System.Web.UI.HtmlControls.HtmlInputHidden
#End Region

  Dim mstrToolTip As String = "<table id=Table5 cellspacing=2 cellpadding=2 width=384 border=0><tr><TD class=Etiqueta>Última Compra</TD></TR><tr><TD><table id=Table7 cellspacing=2 cellpadding=2 width=100% border=0><tr><TD class=Input ><FONT class=Input size=3>Precio</FONT></TD><TD></TD></TR><tr><TD class=Input >Proveedor</TD><TD></TD></TR></TABLE></TD></TR><tr><TD class=Etiqueta>Alternativas</TD></TR><tr><TD><table id=Table8 cellspacing=2 cellpadding=2 width=100% border=0><tr><TD class=Input><FONT class=Input size=3>1</FONT></TD><TD></TD></TR><tr><TD class=Input >2</TD><TD></TD></TR><tr><TD class=Input >3</TD><TD></TD></TR></TABLE></TD></TR></TABLE>"

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

            'Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "ECASTILL"

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
        Response.Cache.SetNoStore()
        Dim lstrNumero As String
        Dim lstrTipo As String

        lstrNumero = Request("strRequisicion")
        lstrTipo = Request("strTipo")
        hdnNumReq.Value = ""
        If Not IsPostBack Then
            Buscar(lstrNumero)
            BuscarHistoria(lstrNumero)
            btnHistorial.Attributes.Add("onClick", "Historial('" + lstrNumero + "');")
            btnHistorial.Visible = False
            txtDocumento.Attributes.Add("readonly", "readonly")
            txtAlmacen.Attributes.Add("readonly", "readonly")
            txtMoneda.Attributes.Add("readonly", "readonly")
            txtProveedor.Attributes.Add("readonly", "readonly")
            txtCondicion.Attributes.Add("readonly", "readonly")
            txtObservaciones.Attributes.Add("readonly", "readonly")

            Me.txtFOB.Attributes.Add("readonly", "readonly")
            Me.txtFlete.Attributes.Add("readonly", "readonly")
            Me.txtSeguro.Attributes.Add("readonly", "readonly")
            Me.txtBase.Attributes.Add("readonly", "readonly")

            txtSubTotal.Attributes.Add("readonly", "readonly")
            txtDescuento.Attributes.Add("readonly", "readonly")
            txtIGV.Attributes.Add("readonly", "readonly")
            txtTotal.Attributes.Add("readonly", "readonly")
            txteststockactual.Attributes.Add("readonly", "readonly")
            txtestconsumopromedio.Attributes.Add("readonly", "readonly")

            If hdnNumReq.Value.Length = 0 Then
                btnListaAdjuntos.Enabled = False
            End If
            btnListaAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos()")

        End If
        If lstrTipo = "Consulta" Then
            btnAprobar.Visible = False
            btnDesaprobar.Visible = False
        End If
        If hdnarticuloestadistica.Value.Trim.Length = 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")
        End If
        'Me.HyperLinkHtmlToolTip1.ToolTipHtmlText = "<table id=Table5 cellspacing=2 cellpadding=2 width=384 border=0><tr><TD class=Etiqueta>Última Compra</TD></TR><tr><TD><table id=Table7 cellspacing=2 cellpadding=2 width=100% border=0><tr><TD class=Input ><FONT class=Input size=3>Precio</FONT></TD><TD></TD></TR><tr><TD class=Input >Proveedor</TD><TD></TD></TR></TABLE></TD></TR><tr><TD class=Etiqueta>Alternativas</TD></TR><tr><TD><table id=Table8 cellspacing=2 cellpadding=2 width=100% border=0><tr><TD class=Input><FONT class=Input size=3>1</FONT></TD><TD></TD></TR><tr><TD class=Input >2</TD><TD></TD></TR><tr><TD class=Input >3</TD><TD></TD></TR></TABLE></TD></TR></TABLE>"
    End Sub

  Private Sub dtgDetalle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDetalle.ItemDataBound
    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
      Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
      If CType(e.Item.DataItem, DataRowView)(6) = 1 Then
        CType(e.Item.FindControl("ibtAdjuntos"), ImageButton).Visible = True
        CType(e.Item.FindControl("ibtAdjuntos"), ImageButton).ImageUrl = IIf(e.Item.ItemType = ListItemType.Item, "../../intranet/Imagenes/PaginasI.bmp", "../../intranet/Imagenes/PaginasAI.bmp")
        CType(e.Item.FindControl("ibtAdjuntos"), ImageButton).Attributes.Add("onClick", "VerAdjunto('" + txtDocumento.Text.Trim + "','" & CType(e.Item.DataItem, DataRowView)("var_ArticuloCodigo") & "');")
        'CType(e.Item.FindControl("ibtAdjunto"), NMControlesWeb.HyperLinkHtmlToolTip).ToolTipHtmlText = mstrToolTip
      Else
        CType(e.Item.FindControl("ibtAdjuntos"), ImageButton).Visible = False
      End If
      CType(e.Item.FindControl("ibtiestadisticas"), ImageButton).Attributes.Add("onClick", "fnc_estadisticosxarticulo('" & CType(e.Item.DataItem, DataRowView)("var_ArticuloCodigo").ToString & "');")
    End If
  End Sub

  Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
    ' Modificado: 26-04-2011
    ' Objetivo: Enviar email al proveedor, si es la ultima aprobacion
    ' Alexander Torres cardenas
    Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones
    Dim strEstado As String
    Dim ldbtAprobacion As New DataTable

    lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
    lobjOCOS.Codigo = txtDocumento.Text
    If lobjOCOS.AprobarOCOS Then
      If lobjOCOS.SetDatos.Tables(0).Rows.Count > 0 Then
        Try
          ' Enviamos email informativo
          EnviarCorreo(lobjOCOS.SetDatos.Tables(0), lobjOCOS.Codigo)
        Catch ex As Exception
          EnviaCorreoError(ex.ToString)
        End Try
      End If
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
    End If
  End Sub

  Private Sub EnviaCorreoError(ByVal strMensaje As String)
    Dim lstrCuerpoMensaje As String

    lstrCuerpoMensaje = strMensaje

    Dim mailMsg As System.Net.Mail.MailMessage
    mailMsg = New System.Net.Mail.MailMessage()
    mailMsg.To.Add("sistemas@nuevomundosa.com")

        Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
        Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
        Dim userCredential As New System.Net.NetworkCredential(user, password)

    With mailMsg
            '.From = New System.Net.Mail.MailAddress("<IntranetNM@nuevomundosa.com>")
            .From = New System.Net.Mail.MailAddress(user)
      .Subject = "Error al enviar email al Proveedor"
      .Body = lstrCuerpoMensaje
      .Priority = System.Net.Mail.MailPriority.High
      .IsBodyHtml = True
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


    End Sub


    Private Sub btnDesaprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesaprobar.Click
        ' Modificado para guardar Observacion de desaprobacion
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones
        Dim strObservaciones2 As String
        Dim lstrNumeroOC As String
        Dim ldtbCorreo As DataTable

        lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjOCOS.Codigo = txtDocumento.Text
        If txtObservaciones2.Text.Length = 0 Then
            strObservaciones2 = "Desaprobado por: " + Session("@USUARIO") + " Fecha: " + Now.ToShortDateString + " Motivo: " + Trim(txtObservaciones2.Text)
        Else
            strObservaciones2 = txtObservaciones2.Text + " Desaprobado por: " + Session("@USUARIO") + " Fecha: " + Now.ToShortDateString
        End If
        lstrNumeroOC = txtDocumento.Text
        Try
            If Trim(txtObservaciones2.Text).Length > 0 Then
                ldtbCorreo = New DataTable
                ldtbCorreo = lobjOCOS.DesaprobarOCOS(strObservaciones2)
                'Verificamos si es el ultimo paso
                If Not ldtbCorreo Is Nothing And ldtbCorreo.Rows.Count > 0 Then
                    Dim strEstado As String = ""
                    strEstado = ldtbCorreo.Rows(0).Item("ti_situ").ToString
                    If strEstado = "ACT" Then
                        'Enviamos email
                        lblMsgDesaprobacion.Visible = True
                        lblMsgDesaprobacion.Text = "Documento desaprobado"
                        EnviarEmailDesaprobacion(ldtbCorreo)
                    End If
                End If
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('El documento: " & lstrNumeroOC & " ha sido desaprobado.');</script>")
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('Por favor ingrese una observacion (en el campo otras observaciones) para la Desaprobacion.');</script>")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim lstrURL As String
        lstrURL = "../CrystalReports/_Logistica.asp?strFormulario=LOG20006&strOC=" + txtDocumento.Text.Trim
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub

    Private Sub dtgDetalle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDetalle.ItemCommand
        Dim lstrarticulo As String = ""
        Select Case e.CommandName
            Case "cmd_estadisticas" 'mostrar el panel con los datos de estadisticos del artículo
                lstrarticulo = hdnarticuloestadistica.Value
                If lstrarticulo.Trim.Length > 0 Then
                    Call prc_datosestadisticosxarticulo(lstrarticulo)
                End If
        End Select
    End Sub

#End Region

#Region "-- Metodos --"

    Private Sub Seguimiento(ByVal pstrNumero As String)
        Dim lobjCon As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Dim larrParams() = {"chr_Empresa", Session("@EMPRESA"), "var_OCOS", pstrNumero}
        dtgSeguimiento.DataSource = (lobjCon.ObtenerDataTable("usp_LOG_OCOS_Seguimiento", larrParams))
        dtgSeguimiento.DataBind()
        lobjCon = Nothing
    End Sub

    Private Sub Buscar(ByVal pstrCodigo As String)
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones

        lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjOCOS.Codigo = pstrCodigo
        If lobjOCOS.BuscarOCOS() Then
            With lobjOCOS.SetDatos.Tables(0)
                txtDocumento.Text = .Rows(0)("var_Numero")
                txtAlmacen.Text = .Rows(0)("var_AlmacenCodigo") + " - " + .Rows(0)("var_AlmacenNombre")
                txtObservaciones.Text = .Rows(0)("var_Observaciones")
                Me.txtBase.Text = Format(.Rows(0)("num_Base"), "#,##0.00")
                Me.txtFOB.Text = Format(.Rows(0)("IM_GAFO"), "#,##0.00")
                Me.txtFlete.Text = Format(.Rows(0)("IM_FLET"), "#,##0.00")
                Me.txtSeguro.Text = Format(.Rows(0)("IM_SEGU"), "#,##0.00")
                txtDescuento.Text = Format(.Rows(0)("num_Descuento"), "#,##0.00")
                txtSubTotal.Text = Format(.Rows(0)("num_SubTotal"), "#,##0.00")
                txtIGV.Text = Format(.Rows(0)("num_IGV"), "#,##0.00")
                txtTotal.Text = Format(.Rows(0)("num_Total"), "#,##0.00")
                txtMoneda.Text = .Rows(0)("DE_MONE")
                txtProveedor.Text = .Rows(0)("NO_CORT_PROV")
                txtCondicion.Text = .Rows(0)("DE_COND")
                txtObservaciones2.Text = .Rows(0)("var_Observaciones2")
                hdnNumReq.Value = .Rows(0)("NumReq")
            End With
            dtgDetalle.DataSource = lobjOCOS.SetDatos.Tables(1)
            dtgDetalle.DataBind()

            If Trim(txtObservaciones2.Text).Length > 0 Then
                lblMsgDesaprobacion.Visible = True
                lblMsgDesaprobacion.Text = "El documento ha sido previamente desaprobado"
            Else
                lblMsgDesaprobacion.Visible = False
                lblMsgDesaprobacion.Text = ""
            End If
        End If
        lobjOCOS = Nothing
    End Sub

    Private Sub BuscarHistoria(ByVal pstrNumero As String)
        Dim lobjCon As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Dim ldtRes As DataTable
        Dim larrParams() = {"var_Empresa", Session("@EMPRESA"), "var_Numero", pstrNumero}
        ldtRes = lobjCon.ObtenerDataTable("usp_LOG_Requisicion_SeguimientoCompletoListar2", larrParams)
        dtgSeguimiento.DataSource = ldtRes
        dtgSeguimiento.DataBind()
        lobjCon = Nothing
        lblCreacion.Text = "Creada el : " + ldtRes.Rows(0)("var_Fecha")
    End Sub

    Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable, ByVal pstrDocumento As String)
        Dim i As Integer, lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrCopias As String = ""

        'Modificado: 26-04-2011
        'Obtenemos datos del proveedor, copiamos al personal de logistica
        'Alexander Torres cardenas

        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim ldtbDatosOC As DataTable, lstrUsuTemp As String, lintTipoMensaje As Integer
        Dim strProveedor, strRucProveedor As String
        Dim strCopia As String, listaPara As String

        ldtbDatosOC = lobjOrdenCompra.fnc_Listar(1, pstrDocumento)

        If ldtbDatosOC.Rows.Count > 0 Then
            strRucProveedor = ldtbDatosOC.Rows(0).Item("co_prov").ToString()
            strProveedor = ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString()
        End If

        'EPOMA - 2011.09.02
        'Se cambia para que solo envia UN solo correo, ya no N correos(revisar sourcesafe)
        strCopia = ""
        listaPara = ""
        lintTipoMensaje = 0
        lstrUsuTemp = ""

        For i = 0 To pdtCorreos.Rows.Count - 1
            With pdtCorreos.Rows(i)
                lintTipoMensaje = .Item("Tipo")
                If InStr(listaPara, .Item("UsuarioCorreo")) <= 0 And InStr(strCopia, .Item("UsuarioCorreo")) <= 0 Then
                    listaPara = listaPara + .Item("UsuarioCorreo") & ";"
                End If
                lstrUsuTemp = .Item("Usuario")
            End With
        Next i

        'si no hay lista para sale del proceso enviarcorreos
        If listaPara.Length <= 5 Then Exit Sub

        If (lintTipoMensaje = 1) Then


            '=============================================================================
            'se actualiza para agregar a jbaltazar y acaro desde tabla maestra de administrativo nuevo mundo
            Dim ldtbCorreosCopia As DataTable, lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"chr_CodigoTabla", 33} 'tabla  maestra
            Dim lint_fila As Integer = 0
            Dim larrCopia() As String

            lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
            ldtbCorreosCopia = lobjConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", lstrParametros)

            lobjConexion = Nothing

            If (Not ldtbCorreosCopia Is Nothing) AndAlso ldtbCorreosCopia.Rows.Count > 0 Then
                For lint_fila = 0 To ldtbCorreosCopia.Rows.Count - 1
                    If ldtbCorreosCopia.Rows(lint_fila).Item("vch_nombre") = "Logistica" Then
                        lstrCopias = ldtbCorreosCopia.Rows(lint_fila).Item("vch_email")
                        Exit For
                    End If
                Next
            End If

            ldtbCorreosCopia = Nothing

            'If InStr(listaPara, "jbaltazar@nuevomundosa.com") <= 0 And InStr(strCopia, "jbaltazar@nuevomundosa.com") <= 0 Then
            '    strCopia = strCopia + "jbaltazar@nuevomundosa.com;"
            'End If
            'If InStr(listaPara, "malfaro@nuevomundosa.com") <= 0 And InStr(strCopia, "malfaro@nuevomundosa.com") <= 0 Then
            '    strCopia = strCopia + "malfaro@nuevomundosa.com;"
            'End If

            If lstrCopias.Length > 0 Then
                'pasar a array
                larrCopia = lstrCopias.Split(",")
                For lint_fila = 0 To larrCopia.Length - 1
                    If InStr(listaPara, larrCopia(lint_fila)) <= 0 And InStr(strCopia, larrCopia(lint_fila)) <= 0 Then
                        strCopia = strCopia + larrCopia(lint_fila) + ";"
                    End If
                Next
            End If
            '=============================================================================

            lstrTitulo = "Orden de " + IIf(Left(pstrDocumento, 4) = "0001", "compra", "servicio") + _
                         " Nro. " + pstrDocumento + " ha sido autorizada."


            lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                        "Se han autorizado las siguientes ordenes :" + _
                        "<BR>" + _
                        "<BR>" + _
                        "- <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + pstrDocumento + "</FONT></B>" + _
                                " : " + strRucProveedor + " " + strProveedor + "." + _
                        "<BR>" + _
                        "</P>" + _
                        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>" + _
                        "-------------------------------------------------------------------------------" + _
                        "<BR>" + _
                        "Este correo ha sido generado automáticamente por el módulo de aprobaciones." + _
                        "<BR>" + _
                        "Por favor no responder este correo." + _
                        "<BR>" + _
                        "Departamento de Sistemas" + _
                        "<BR>" + _
                        "Cia. Industrial Nuevo Mundo S.A." + _
                        "<BR>" + _
                        "-------------------------------------------------------------------------------" + _
                        "</P>"


            'lstrCuerpoMensaje = "La orden de " + IIf(Left(pstrDocumento, 4) = "0001", "compra", "servicio") + " Nro. " + pstrDocumento + _
            '                    " asignada al Proveedor: " + strRucProveedor + " " + strProveedor + " ha sido autorizada."

        Else

            lstrTitulo = "Orden de " + IIf(Left(pstrDocumento, 4) = "0001", "compra", "servicio") + _
                         " Nro. " + pstrDocumento + " necesita su aprobación."

            lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                        "Las siguientes ordenes requieren de su aprobación :" + _
                        "<BR>" + _
                        "<BR>" + _
                        "- <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + pstrDocumento + "</FONT></B>" + _
                                " : " + strRucProveedor + " " + strProveedor + "." + _
                        "<BR>" + _
                        "</P>" + _
                        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>" + _
                        "-------------------------------------------------------------------------------" + _
                        "<BR>" + _
                        "Este correo ha sido generado automáticamente por el módulo de aprobaciones." + _
                        "<BR>" + _
                        "Por favor no responder este correo." + _
                        "<BR>" + _
                        "Departamento de Sistemas" + _
                        "<BR>" + _
                        "Cia. Industrial Nuevo Mundo S.A." + _
                        "<BR>" + _
                        "-------------------------------------------------------------------------------" + _
                        "</P>"
            'lstrCuerpoMensaje = "La orden de " + IIf(Left(pstrDocumento, 4) = "0001", "compra", "servicio") + " Nro. : " + pstrDocumento + _
            '                    " asignada al Proveedor: " + strRucProveedor + " " + strProveedor + _
            '                    ", ha sido aprobada por : " + lstrUsuTemp + ", ahora necesita de su aprobación."
        End If


        Dim mailMsg As System.Net.Mail.MailMessage
        mailMsg = New System.Net.Mail.MailMessage()

        'Configurar arreglo para el TO
        Dim lstrTo_arreglo() As String = listaPara.Split(";")
        For lintIndice = 0 To lstrTo_arreglo.Length - 1
            If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
        Next

        'Si no hay destinatario que lo envie a sistemas
        If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

        'Configurar arreglo para el CC
        Dim lstrCC_arreglo() As String = strCopia.Split(";")
        For lintIndice = 0 To lstrCC_arreglo.Length - 1
            If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
        Next

        Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
        Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
        Dim userCredential As New System.Net.NetworkCredential(user, password)

        With mailMsg
            '.From = New System.Net.Mail.MailAddress("O/C - O/S<aprobaciones@nuevomundosa.com>")
            .From = New System.Net.Mail.MailAddress(user)
            .Subject = lstrTitulo
            .Body = lstrCuerpoMensaje
            .Priority = System.Net.Mail.MailPriority.High
            .IsBodyHtml = True
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



    End Sub

    Private Sub prc_datosestadisticosxarticulo(ByVal pstrarticulo As String)
        Dim ldtsdatos As DataSet, lobjarticulo As New OFISIS.OFILOGI.Articulos("01", Session("@USUARIO"))
        lobjarticulo.Codigo = pstrarticulo
        ldtsdatos = lobjarticulo.fnc_listarestadisticasxarticulo(1)
        lobjarticulo = Nothing
        dgestcompras.DataSource = ldtsdatos.Tables(0)
        dgestcompras.DataBind()
        dgestconsumo.DataSource = ldtsdatos.Tables(1)
        dgestconsumo.DataBind()
        txteststockactual.Text = CType(ldtsdatos.Tables(2).Rows(0).Item("num_stockactual"), Double)
        txtestconsumopromedio.Text = CType(ldtsdatos.Tables(2).Rows(0).Item("num_consumopromedio"), Double)

        ldtsdatos = Nothing

        ClientScript.RegisterStartupScript(Me.[GetType](), "ubicaropanel", "<script language=javascript>document.all['pnldatosestadisticos'].style.top=" & hdnposicionpanel.Value & "</script>")
    End Sub

#End Region

    Private Sub btnAnalisis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnalisis.Click
        fnc_VerReporteAnalisis()
    End Sub

    ' Funcion para Ver Reporte de Analisis detallado
    Private Function fnc_VerReporteAnalisis()
        Dim strNumOrdenCompra As String
        Dim lstrURL As String = ""
        strNumOrdenCompra = Trim(txtDocumento.Text)
        lstrURL = "../CrystalReports/rpt_AnalisisOC.asp?strNumOrdenCompra=" & strNumOrdenCompra
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Function

    ' Envio de Correo al Proveedor
#Region "EnviaEmailProveedor"
    'Public Sub prc_EnviarOC_Proveedor(ByVal pstrDocumento As String)
    '    Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
    '    Dim ldtbDatosOC As DataTable
    '    Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable
    '    Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
    '    Dim lobjFile As File, lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
    '    Dim lstrEmailDestino As String = "", lobjmailMsg As MailMessage, lstrCuerpoMensaje As String = ""
    '    Dim lobjAdjunto As System.Web.Mail.MailAttachment, lstrUsuario As String = ""
    '    Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
    '    Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
    '    Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
    '    Dim lstrError As String = ""

    '    '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
    '    ldtbDatosOC = lobjOrdenCompra.fnc_Listar(1, pstrDocumento)
    '    lstrEmailDestino = ldtbDatosOC.Rows(0).Item("prv_de_mail").ToString

    '    '--INICIO: GENERAR NOMBRE ARCHIVO Y OBTENER RUTA DONDE GUARDAR
    '    lstrNumeroOC = pstrDocumento
    '    lstrNombreFile = "oco_" & lstrNumeroOC & "_" & Strings.Format(Now(), "hhmmss")
    '    ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("28")
    '    lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
    '    ldtbRuta = Nothing
    '    lobjGeneral = Nothing
    '    If lstrRutaFile.Length <= 0 Then
    '        ClientScript.RegisterStartupScript(Me.[GetType](),"Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
    '        Exit Sub
    '    End If
    '    lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"

    '    '--INICIO: CONVERTIR A PDF
    '    Try
    '        lstrBDUsuario = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "User")
    '        lstrBDServidor = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Server")
    '        lstrBDPassword = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "Passwd")
    '        lstrBDBaseDato = lobjUtil.ClaveRegistro_Obtener("OFILOGI", "BD")
    '        lobjUtil = Nothing
    '        lrptOrdenCompra.SetParameterValue(0, lstrNumeroOC)
    '        lrptOrdenCompra.SetDatabaseLogon(lstrBDUsuario, lstrBDPassword, lstrBDServidor, lstrBDBaseDato)
    '        lrptOrdenCompra.ExportOptions.ExportDestinationType = CrystalDecisions.[Shared].ExportDestinationType.DiskFile
    '        lrptOrdenCompra.ExportOptions.ExportFormatType = CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat
    '        lobjOpcDisco.DiskFileName = lstrRutaFile
    '        lrptOrdenCompra.ExportOptions.DestinationOptions = lobjOpcDisco
    '        lrptOrdenCompra.Export()
    '        lrptOrdenCompra = Nothing
    '        lobjOpcDisco = Nothing

    '        '--INICIO: ATACHAR PDF Y ENVIAR EMAIL AL PROVEEDOR (FORMATEAR EMAIL)
    '        lstrCuerpoMensaje = "<P><FONT face='Verdana' size='2'>Estimados señores,</FONT></P> " + _
    '      "<P><FONT face='Verdana' size='2'><STRONG>" + ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString + "</STRONG></FONT></P>" + _
    '      "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la orden de compra:<STRONG>" + _
    '      "<FONT style='BACKGROUND-COLOR: #ffff33'>" + lstrNumeroOC + "</FONT></STRONG></FONT><FONT style='BACKGROUND-COLOR: #ffff33'></FONT>" + _
    '      "<BR><BR>" + _
    '      "<FONT face='Verdana'>Dpto. de Logística</FONT>" + _
    '      "<BR>" + _
    '      "<FONT face='Verdana'>Cía Industrial Nuevo Mundo</FONT>" + _
    '      "<BR>" + _
    '      "<FONT face='Verdana'>Telf. 415-4000 anexo 221</FONT>" + _
    '      "<BR>" + _
    '     "</FONT><A href='http://www.nuevomundosa.com'><FONT face='Verdana' size='2'>http://www.nuevomundosa.com</FONT></A></P>" + _
    '    "<P><FONT size='2'></FONT></P>" + _
    '    "<P><FONT face='Verdana' size='2'>-------------------------------------------------------------------------------<BR>" + _
    '      "Este correo ha sido generado automáticamente por el módulo de compras.<BR>" + _
    '      "Por favor no responder este correo.<BR>" + _
    '      "-------------------------------------------------------------------------------</FONT></P>"

    '        lobjmailMsg = New MailMessage
    '        With lobjmailMsg
    '            .From = "Nuevo Mundo - Compras<compras@nuevomundosa.com>"
    '            .To = lstrEmailDestino
    '            .Cc = "acaro@nuevomundosa.com"
    '            .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
    '            .Body = lstrCuerpoMensaje
    '            .Priority = MailPriority.High
    '            .BodyFormat = MailFormat.Html
    '            lobjAdjunto = New MailAttachment(lstrRutaFile)
    '            .Attachments.Add(lobjAdjunto)
    '            '.Headers.Add("Notificacion de Lectura", "ecastillo@nuevomundosa.com")
    '        End With
    '        SmtpMail.SmtpServer = ConfigurationSettings.AppSettings.Item("ServidorSMTP").ToString '"192.168.116.2"
    '        'SmtpMail.Send(lobjmailMsg)
    '    Catch ex As Exception
    '        lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
    '    End Try
    '    If lstrError.Length > 0 Then
    '        ClientScript.RegisterStartupScript(Me.[GetType](),"Mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
    '        Exit Sub
    '    End If

    '    'Mensaje de Confirmacion
    '    'ClientScript.RegisterStartupScript(Me.[GetType](),"Mensaje", "<script language=javascript>alert('La orden de servicio -- " & lstrNumeroOC & " -- ha sido enviado al proveedor.');</script>")

    '    '--INICIO: ACTUALIZAR METADATA DE ENVIO EN OC
    '    Try
    '        lstrUsuario = IIf(IsNothing(Session.Item("@USUARIO")), "", Session.Item("@USUARIO"))
    '        ldtbError = lobjOrdenCompra.fnc_ActualizarDatosEnvio(lstrNumeroOC, lstrUsuario, lstrEmailDestino)
    '        '-----------------------------------------------------------------------------------------
    '        '--INICIO: ELIMINAR ARCHIVO PDF
    '        '-----------------------------------------------------------------------------------------
    '        If lobjFile.Exists(lstrRutaFile) Then
    '            lobjFile.Delete(lstrRutaFile)
    '        End If
    '    Catch ex As Exception
    '    Finally
    '        lrptOrdenCompra = Nothing
    '        lobjOpcDisco = Nothing
    '        lobjAdjunto = Nothing
    '    End Try
    'End Sub
#End Region

    ' Envio de Correo Electronico
#Region "Envio de Email"
    Private Sub EnviarEmailDesaprobacion(ByVal ldtbCorreo As DataTable)
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""
        Dim strNumeroOC As String

        strNumeroOC = txtDocumento.Text
        Try
            lstrPara = ldtbCorreo.Rows(0).Item("de_dire_mail")
            If lstrPara.Length > 0 Then
                lstrCopia = "ecastillo@nuevomundosa.com"
                lstrTitulo = "[Intranet] OC/OS DESAPROBADA."
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA DESAPROBADO LA OC/OS " + _
                                        "<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & strNumeroOC & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG></FONT></FONT></P>" + _
                                        "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(ldtbCorreo.Rows(0).Item("co_usua_crea")) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                        "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb") + "/intrasolution/index.asp'>" + _
                                        "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                        "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                                        "Por favor no responder este correo.<BR>" + _
                                        "Departamento de Sistemas<BR>" + _
                                        "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                        "-------------------------------------------------------------------------------</P>"

                Dim mailMsg As System.Net.Mail.MailMessage
                mailMsg = New System.Net.Mail.MailMessage()

                'Configurar arreglo para el TO
                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next

                'Si no hay destinatario que lo envie a sistemas
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

                'Configurar arreglo para el CC
                Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
                For lintIndice = 0 To lstrCC_arreglo.Length - 1
                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)

                With mailMsg
                    '.From = New System.Net.Mail.MailAddress("OC/OS DESAPROBACION<aprobaciones@nuevomundosa.com>")
                    .From = New System.Net.Mail.MailAddress(user)
                    .Subject = lstrTitulo
                    .Body = lstrCuerpoMensaje
                    .Priority = System.Net.Mail.MailPriority.High
                    .IsBodyHtml = True
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

                Dim strDestinatarios As String
                strDestinatarios = "Se Comunico email informativo a: " + lstrPara
                lblError.Text = strDestinatarios
                lblError.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = ""
            lblError.Visible = False
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub
#End Region

    
End Class
