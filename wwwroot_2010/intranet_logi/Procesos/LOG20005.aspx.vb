Imports System.IO
Imports System.Web.Mail

Public Class LOG20005
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents hdn1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblCantidad As System.Web.UI.WebControls.Label
    Protected WithEvents tblFil1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblFil2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblBotonera1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnaprobarmasivo As System.Web.UI.WebControls.Button
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnaprobarmasivo As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Variables --"
    Dim mstr_correopara1 As String, mstr_correopara2 As String
    Dim mstr_correocopia1 As String, mstr_correocopia2 As String
    Dim mstr_correocuerpo1 As String, mstr_correocuerpo2 As String
    Dim mstr_copia As String
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
            ' Session("@USUARIO") = "JMAYO"
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
        Me.tblFil1.Visible = False
        Me.tblFil2.Visible = False

        btnConsultar.Text = "Actualizar"
        If Me.hdn1.Value = "1" Then
            Me.hdn1.Value = ""
        End If
        If Not Page.IsPostBack Then
            btnConsultar_Click(Nothing, Nothing)
            hdnaprobarmasivo.Value = ""
        End If
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Call prc_listar()
    End Sub

    Private Sub dtgLista_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemCreated
        If e.Item.ItemIndex <> -1 Then
            Dim btnBuscar As ImageButton
            btnBuscar = CType(e.Item.FindControl("ibtBuscar"), ImageButton)
            If Not btnBuscar Is Nothing Then
                AddHandler btnBuscar.Click, AddressOf btnBuscar_Click
            End If
        End If
    End Sub

  Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Call prc_listar()
  End Sub

    Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtBuscar"), ImageButton)
                Dim lobjaprobar As CheckBox = CType(e.Item.FindControl("chkaprobari"), CheckBox)
                Dim btnVerAdjuntos As ImageButton = CType(e.Item.FindControl("ibtVerAdj"), ImageButton)
                ' boton adjuntar
                If ldrvVista("int_Adjunto") = 0 Then
                    btnVerAdjuntos.Visible = False
                End If

                btnVerAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntosOC('" + ldrvVista("var_OrdenCompra") + "')")                

                lobjBoton.Attributes.Add("onClick", "javascript:return VerDetalle('" + ldrvVista("var_OrdenCompra") + "')")
                lobjBoton = CType(e.Item.FindControl("ibtHistorial"), ImageButton)
                lobjBoton.Attributes.Add("onClick", "VerHistorial('" + ldrvVista("var_OrdenCompra") + "')")
                lobjaprobar.Attributes.Add("onClick", "fnc_aprobarmasivo(this,'" + ldrvVista("var_OrdenCompra") + "')")
                lobjBoton = Nothing
                lobjaprobar = Nothing
                ldrvVista = Nothing
        End Select
    End Sub

    Private Sub btnaprobarmasivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaprobarmasivo.Click
        Call prc_aprobarmasivo()
    End Sub

#End Region

#Region "-- Metodos --"

    Private Sub prc_listar()
        Try
            Dim lobjRequi As OFISIS.OFILOGI.Requisiciones
            lobjRequi = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
            lobjRequi.Listar(OFISIS.OFILOGI.Requisiciones.enuTiposLista.OCOS)
            If Not lobjRequi.SetDatos.Tables(0) Is Nothing Then
                dtgLista.DataSource = lobjRequi.SetDatos.Tables(0)
                dtgLista.DataBind()
                lblCantidad.Text = Format(dtgLista.Items.Count, "#,##0")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            EnviaCorreoError(ex.Message + "LOG2005-Listar OC/OS por aprobar")
        End Try
    End Sub

    Private Sub prc_aprobarmasivo()
        'maximo 50 ordenes, porque la variable del sp es de 800
        Dim lobjOCOS As OFISIS.OFILOGI.Requisiciones, lstr_ordenes() As String
        Dim strEstado As String, lint_fila As Integer
        Dim ldbtAprobacion As DataTable
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Try
            mstr_correopara1 = ""
            mstr_correopara2 = ""
            mstr_correocopia1 = ""
            mstr_correocopia2 = ""
            mstr_correocuerpo1 = ""
            mstr_correocuerpo2 = ""

            If hdnaprobarmasivo.Value.Length <= 0 Then
        ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No ha seleccionado ninguna O/C o O/S.')</script>")
      End If
      'separar con split y enviar a BD
      lstr_ordenes = Strings.Split(hdnaprobarmasivo.Value, ",")
      'bucle
      lobjOCOS = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))

      '=====================================================
      Dim ldtbCorreosCopia As DataTable, lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
      Dim lstrParametros() As String = {"chr_CodigoTabla", 33} 'tabla  maestra

      lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
      ldtbCorreosCopia = lobjConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", lstrParametros)

      lobjConexion = Nothing

      If (Not ldtbCorreosCopia Is Nothing) AndAlso ldtbCorreosCopia.Rows.Count > 0 Then
        For lint_fila = 0 To ldtbCorreosCopia.Rows.Count - 1
          If ldtbCorreosCopia.Rows(lint_fila).Item("vch_nombre") = "Logistica" Then
            mstr_copia = ldtbCorreosCopia.Rows(lint_fila).Item("vch_email")
            Exit For
          End If
        Next
      End If

      ldtbCorreosCopia = Nothing
      '=====================================================


      For lint_fila = 0 To lstr_ordenes.Length - 2
        lobjOCOS.Codigo = lstr_ordenes(lint_fila)
        If lobjOCOS.AprobarOCOS Then
          If lobjOCOS.SetDatos.Tables(0).Rows.Count > 0 Then
            'preparanos email informativo
            PrepararCorreo(lobjOCOS.SetDatos.Tables(0), lobjOCOS.Codigo, lobjOrdenCompra)
          End If
        End If
      Next
      'envio final de correo informativo
      lobjOrdenCompra = Nothing

      EnviarCorreo()

      Call prc_listar()

    Catch ex As Exception
      EnviaCorreoError(ex.ToString + "Envio de Email")
    End Try
  End Sub

  Private Sub EnviaCorreoError(ByVal strMensaje As String)
        Dim lstrCuerpoMensaje As String
        Dim mailMsg As System.Net.Mail.MailMessage

        lstrCuerpoMensaje = strMensaje
        mailMsg = New System.Net.Mail.MailMessage()

        Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
        Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
        Dim userCredential As New System.Net.NetworkCredential(user, password)

        With mailMsg
            '.From = New System.Net.Mail.MailAddress("<IntranetNM@nuevomundosa.com>")
            .From = New System.Net.Mail.MailAddress(user)
            .To.Add("sistemas@nuevomundosa.com")

            .Subject = "Ocurrio un Error - Log2005"
            .IsBodyHtml = True
            .Body = lstrCuerpoMensaje
            .Priority = System.Net.Mail.MailPriority.High
        End With
        '20120906: EPM()
        '    SmtpMail.SmtpServer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ServidorSMTP").ToString
        '        SmtpMail.Send(mailMsg)

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

    Private Sub PrepararCorreo(ByRef pdtCorreos As DataTable, ByVal pstrDocumento As String, ByRef pobjOrdenCompra As NuevoMundo.Logistica.OrdenCompra)
        Dim i As Integer, lstrCuerpoMensaje As String = ""
        'Dim lstrTitulo As String ', mailMsg As System.Net.Mail.MailMessage
        Dim lstrCopias As String = ""

        Dim ldtbDatosOC As DataTable, lstrUsuTemp As String, lintTipoMensaje As Integer
        Dim strProveedor, strRucProveedor As String
        Dim strCopia As String, listaPara As String
        Dim lstr_usuariotmp As String = ""

        ldtbDatosOC = pobjOrdenCompra.fnc_Listar(1, pstrDocumento)

        If ldtbDatosOC.Rows.Count > 0 Then
            strRucProveedor = ldtbDatosOC.Rows(0).Item("co_prov").ToString()
            strProveedor = ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString()
        End If

        strCopia = ""
        listaPara = ""
        lintTipoMensaje = 0
        lstrUsuTemp = ""

        For i = 0 To pdtCorreos.Rows.Count - 1
            With pdtCorreos.Rows(i)
                lintTipoMensaje = .Item("Tipo")
                listaPara = listaPara + .Item("UsuarioCorreo")
                lstr_usuariotmp = .Item("UsuarioNombre")

                If lintTipoMensaje = 1 Then
                    If .Item("UsuarioCorreo").Length > 0 And InStr(mstr_correopara1, .Item("UsuarioCorreo")) <= 0 And InStr(mstr_correocopia1, .Item("UsuarioCorreo")) <= 0 Then
                        mstr_correopara1 = mstr_correopara1 + .Item("UsuarioCorreo") & ";"
                    End If
                ElseIf lintTipoMensaje = 2 Then
                    If .Item("UsuarioCorreo").Length > 0 And InStr(mstr_correopara2, .Item("UsuarioCorreo")) <= 0 And InStr(mstr_correocopia2, .Item("UsuarioCorreo")) <= 0 Then
                        mstr_correopara2 = mstr_correopara2 + .Item("UsuarioCorreo") & ";"
                    End If
                End If

                lstrUsuTemp = .Item("Usuario")
            End With
        Next i

        ldtbDatosOC = Nothing

        'si no hay lista para sale del proceso enviarcorreos
        If listaPara.Length <= 5 Then Exit Sub

        If lintTipoMensaje = 1 Then

            '=============================================================================
            'se actualiza para agregar a jbaltazar y acaro desde tabla maestra de administrativo nuevo mundo
            Dim larrCopia() As String, lint_fila As Integer

            'If InStr(mstr_correopara1, "jbaltazar@nuevomundosa.com") <= 0 And InStr(mstr_correocopia1, "jbaltazar@nuevomundosa.com") <= 0 Then
            '    mstr_correocopia1 = mstr_correocopia1 + "jbaltazar@nuevomundosa.com;"
            'End If
            'If InStr(mstr_correopara1, "malfaro@nuevomundosa.com") <= 0 And InStr(mstr_correocopia1, "malfaro@nuevomundosa.com") <= 0 Then
            '    mstr_correocopia1 = mstr_correocopia1 + "malfaro@nuevomundosa.com;"
            'End If

            If mstr_copia.Length > 0 Then
                'pasar a array
                larrCopia = mstr_copia.Split(",")
                For lint_fila = 0 To larrCopia.Length - 1
                    If InStr(mstr_correopara1, larrCopia(lint_fila)) <= 0 And InStr(mstr_correocopia1, larrCopia(lint_fila)) <= 0 Then
                        mstr_correocopia1 = mstr_correocopia1 + larrCopia(lint_fila) + ";"
                    End If
                Next
            End If
            '=============================================================================

            'mstr_correocuerpo1 = mstr_correocuerpo1 + "La orden de " + IIf(Left(pstrDocumento, 4) = "0001", "compra", "servicio") + _
            '                    " Nro. " + pstrDocumento + _
            '                    " asignada al Proveedor: " + strRucProveedor + " " + strProveedor + " ha sido autorizada." + Strings.Chr(13) + Strings.Chr(13)

            mstr_correocuerpo1 = mstr_correocuerpo1 + _
                                "- <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + pstrDocumento + "</FONT></B>" + _
                                        " : " + strRucProveedor + " " + strProveedor + "." + _
                                "<BR>"
        ElseIf lintTipoMensaje = 2 Then

            'mstr_correocuerpo2 = mstr_correocuerpo2 + listaPara + Strings.Chr(13) + _
            '                    "La orden de " + IIf(Left(pstrDocumento, 4) = "0001", "compra", "servicio") + _
            '                    " Nro. : " + pstrDocumento + _
            '                    " asignada al Proveedor: " + strRucProveedor + " " + strProveedor + _
            '                    ", ha sido aprobada por : " + lstrUsuTemp + ", ahora necesita de su aprobación." + Strings.Chr(13) + Strings.Chr(13)

            mstr_correocuerpo2 = mstr_correocuerpo2 + _
                                lstr_usuariotmp + "<BR>" + _
                                "- <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + pstrDocumento + "</FONT></B>" + _
                                        " : " + strRucProveedor + " " + strProveedor + "." + _
                                "<BR>" + _
                                "<BR>"
        End If

    End Sub

    Private Sub EnviarCorreo()
        Dim mailMsg As System.Net.Mail.MailMessage

        'correos finales
        If mstr_correopara1.Length > 5 Then

            mstr_correocuerpo1 = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                                "Se han autorizado las siguientes ordenes :" + _
                                "<BR>" + _
                                "<BR>" + _
                                mstr_correocuerpo1 + _
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

            mailMsg = New System.Net.Mail.MailMessage()


            '20121005 EPM Configurar arreglo para el To
            Dim lstrTo_arreglo() As String = mstr_correopara1.Split(";")
            For lintIndice = 0 To lstrTo_arreglo.Length - 1
                If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            Next
            'Si no hay destinatario que lo envie a sistemas
            If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
            '20121005 EPM Configurar arreglo para el CC
            Dim lstrCC_arreglo() As String = mstr_correocopia1.Split(";")
            For lintIndice = 0 To lstrCC_arreglo.Length - 1
                If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
            Next

            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With mailMsg
                '.From = New System.Net.Mail.MailAddress("O/C - O/S<aprobaciones@nuevomundosa.com>")
                .From = New System.Net.Mail.MailAddress(user)
                '.To = mstr_correopara1.ToLower
                '.Cc = mstr_correocopia1.ToLower
                .Subject = "Ordenes de Compra/Servicio han sido autorizadas"
                .Body = mstr_correocuerpo1
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
            End With

            '20120906 EPM
            'SmtpMail.SmtpServer = System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorSMTP").ToString()
            'SmtpMail.Send(mailMsg)

            Dim Servidor As New System.Net.Mail.SmtpClient
            Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                Servidor.Credentials = userCredential
            End If
            Servidor.Send(mailMsg)
            Servidor = Nothing

        End If

        'correos intermedios
        If mstr_correopara2.Length > 5 Then

            mstr_correocuerpo2 = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                    "Las siguientes ordenes requieren de su aprobación :" + _
                    "<BR>" + _
                    "<BR>" + _
                    mstr_correocuerpo2 + _
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

            mailMsg = New System.Net.Mail.MailMessage()

            '20121005 EPM Configurar arreglo para el To
            Dim lstrTo_arreglo() As String = mstr_correopara2.Split(";")
            For lintIndice = 0 To lstrTo_arreglo.Length - 1
                If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            Next
            'Si no hay destinatario que lo envie a sistemas
            If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
            '20121005 EPM Configurar arreglo para el CC
            Dim lstrCC_arreglo() As String = mstr_correocopia2.Split(";")
            For lintIndice = 0 To lstrCC_arreglo.Length - 1
                If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
            Next



            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With mailMsg
                'From = New System.Net.Mail.MailAddress("O/C - O/S<aprobaciones@nuevomundosa.com>")
                .From = New System.Net.Mail.MailAddress(user)
                '.To = mstr_correopara2.ToLower
                '.Cc = mstr_correocopia2.ToLower
                .Subject = "Ordenes de Compra/Servicio necesitan su aprobación"
                .Body = mstr_correocuerpo2
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
            End With

            ''20120906 EPM
            'SmtpMail.SmtpServer = System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorSMTP").ToString()
            'SmtpMail.Send(mailMsg)
            Dim Servidor As New System.Net.Mail.SmtpClient
            Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                Servidor.Credentials = userCredential
            End If
            Servidor.Send(mailMsg)
            Servidor = Nothing
        End If

        mailMsg = Nothing

    End Sub

#End Region

    ' Envia email al proveeedor
#Region "EnviaEmailProveedor"
    Public Sub prc_EnviarOC_Proveedor(ByVal pstrDocumento As String)
        Dim lobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim ldtbDatosOC As DataTable
        Dim lrptOrdenCompra As OrdenCompra = New OrdenCompra, ldtbError As DataTable
        Dim lobjOpcDisco As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim lstrRutaFile As String = "", lstrNumeroOC As String = "", lstrNombreFile As String = ""
        Dim lstrEmailDestino As String = "", lobjmailMsg As System.Net.Mail.MailMessage, lstrCuerpoMensaje As String = ""
        Dim lobjAdjunto As System.Net.Mail.Attachment, lstrUsuario As String = ""
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        Dim lstrBDUsuario As String = "", lstrBDServidor As String = "", lstrBDPassword As String = ""
        Dim lobjUtil As New NM_General.Util, lstrBDBaseDato As String = ""
        Dim lstrError As String = ""
        '-----------------------------------------------------------------------------------------
        '--INICIO: OBTENER DATOS OC Y EMAIL DEL PROVEEDOR
        '-----------------------------------------------------------------------------------------
        ldtbDatosOC = lobjOrdenCompra.fnc_Listar(1, pstrDocumento)
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
        lstrRutaFile = ldtbRuta.Rows(0).Item("oco_rutadocs_guardar").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
        If lstrRutaFile.Length <= 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('No se ha establecido la ruta donde almacenar los documentos para las ordenes de servicio.');</script>")
            Exit Sub
        End If
        lstrRutaFile = lstrRutaFile & "/" & lstrNombreFile & ".pdf"

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
          "<P><FONT size='2'><FONT face='Verdana'>Sirvase atender la orden de compra:<STRONG>" + _
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

            lobjmailMsg = New System.Net.Mail.MailMessage()



            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With lobjmailMsg
                '.From = New System.Net.Mail.MailAddress("Nuevo Mundo - Compras<compras@nuevomundosa.com>")
                .From = New System.Net.Mail.MailAddress(user)
                .To.Add(lstrEmailDestino)
                .CC.Add("acaro@nuevomundosa.com")
                .Subject = "[Cía. Ind. Nuevo Mundo] - Orden de Compra: " + lstrNumeroOC
                .Body = lstrCuerpoMensaje
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
                lobjAdjunto = New System.Net.Mail.Attachment(lstrRutaFile)
                .Attachments.Add(lobjAdjunto)
                '.Headers.Add("Notificacion de Lectura", "ecastillo@nuevomundosa.com")
            End With

            'SmtpMail.SmtpServer = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("ServidorSMTP").ToString '"192.168.116.2"
            'SmtpMail.Send(lobjmailMsg)
            Dim Servidor As New System.Net.Mail.SmtpClient
            Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
            Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
            Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
            If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                Servidor.Credentials = userCredential
            End If
            Servidor.Send(lobjmailMsg)
            Servidor = Nothing


        Catch ex As Exception
            lstrError = "No se pudó enviar el e-mail con la orden de servicio.\n\nProbablemente el correo del proveedor tenga problemas, si el problema persite comuniquese con sistemas."
        End Try
        If lstrError.Length > 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('" & lstrError & "');</script>")
            Exit Sub
        End If

        'Mensaje de Confirmacion
        'ClientScript.RegisterStartupScript(Me.[GetType](),"Mensaje", "<script language=javascript>alert('La orden de servicio -- " & lstrNumeroOC & " -- ha sido enviado al proveedor.');</script>")

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
            lobjAdjunto = Nothing
        End Try
    End Sub

#End Region

End Class
