Imports NuevoMundo

Public Class frm_SecuenciaAprobacionOrdenes
    Inherits System.Web.UI.Page

    Dim strTipo As String = ""
    Dim strCodGrupo As String = ""
    Dim strDesGrupo As String = ""
    Dim strCodGrupoElegido As String

    Dim mstr_Copia As String
    Dim mstr_Para As String = ""
    Dim strSituacion As String

    Private Sub frm_SecuenciaAprobacionOrdenes_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ECASTILL"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnNumero.Value = Request.Item("pstrNumeroDoc")
        strTipo = Request.Item("pstrTipo")
        Consulta(strTipo, strCodGrupo, strDesGrupo)
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        strCodGrupo = Trim(txtCodigo.Text)
        strDesGrupo = Trim(txtNombre.Text)
        Consulta(strTipo, strCodGrupo, strDesGrupo)
    End Sub

    Private Sub dgDatos_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDatos.ItemCommand
        Dim objlblGrupo As Label = CType(e.Item.FindControl("lblCodGrupo"), Label)
        Select Case e.CommandName
            Case "Escoger"
                strCodGrupoElegido = objlblGrupo.Text
                RegistraAprobacionOrden()
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Se aprobo la OC/OS y envio al grupo.');</script>")
        End Select
    End Sub

    Private Sub dgDatos_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As Button = CType(e.Item.FindControl("btnEscoger"), Button)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger('" & drvDatos("CodigoGrupo") & "', '" & drvDatos("DescripcionGrupo") & "')")
        End If
    End Sub

#Region "Metodos"

    Public Sub Consulta(strTipo As String, ByVal strCodGrupo As String, ByVal strDesGrupo As String)
        Dim objOrden As New Logistica.OrdenCompra
        Dim ldtbDatos As New DataTable
        ldtbDatos = Nothing
        Try
            ldtbDatos = objOrden.ListaGrupoAprobacion(strTipo, strCodGrupo, strDesGrupo)
            If Not ldtbDatos Is Nothing Then
                dgDatos.DataSource = ldtbDatos
                dgDatos.DataBind()
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error: No es posible mostrar la lista de grupos " & ex.ToString
        End Try
    End Sub

    Public Sub RegistraAprobacionOrden()
        Dim objOrden As New NuevoMundo.Logistica.OrdenCompra
        Dim Empresa As String = ""
        Dim Usuario As String = ""
        Dim NumeroOrden As String = ""
        Dim Grupo As String = ""

        Try
            Empresa = Session("@EMPRESA")
            Usuario = Session("@USUARIO")
            Grupo = strCodGrupoElegido
            NumeroOrden = hdnNumero.Value

            'aprobamos orden compra
            objOrden.RegistraAprobacionOrden(Empresa, NumeroOrden, Usuario, Grupo)
            'enviamos email interno
            EnvioCorreo()
            'enviamos email al proveedor

        Catch ex As Exception
            lblMensaje.Text = "Ha ocurrido un error en la aprobacio " & ex.ToString
        End Try

    End Sub

    ' Envio de email
    Public Sub EnvioCorreo()
        Dim ldtbCorreosCopia As New DataTable
        CorreoCopia()
        CorreoPara()
        'EnviarCorreoInterno(hdnNumero.Value)
        EnviarCorreoInterno_V2(hdnNumero.Value)
    End Sub

    ' lista de correos de Para
    Public Sub CorreoPara()
        Dim ldtbCorreosPara As New DataTable
        Dim objOrden As New NuevoMundo.Logistica.OrdenCompra
        Dim Empresa As String
        Dim Usuario As String
        Dim NumeroOrden As String
        Try
            Empresa = Session("@EMPRESA")
            Usuario = Session("@USUARIO")
            NumeroOrden = hdnNumero.Value

            ldtbCorreosPara = objOrden.ListaCorreosPara(Empresa, Usuario, NumeroOrden)
            If (Not ldtbCorreosPara Is Nothing) AndAlso ldtbCorreosPara.Rows.Count > 0 Then
                For lint_fila = 0 To ldtbCorreosPara.Rows.Count - 1
                    'mstr_Para = ldtbCorreosPara.Rows(lint_fila).Item("UsuarioCorreo")
                    If mstr_Para.Trim.Length = 0 Then
                        mstr_Para = ldtbCorreosPara.Rows(lint_fila).Item("UsuarioCorreo")
                    Else
                        mstr_Para = mstr_Para + ";" + ldtbCorreosPara.Rows(lint_fila).Item("UsuarioCorreo")
                    End If
                    'mstr_Para = mstr_Para + ldtbCorreosPara.Rows(lint_fila).Item("UsuarioCorreo") + ";"
                Next
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error: Verificar lista de correos (TO). Comuniquese con sistemas." + ex.ToString
        End Try

    End Sub

    ' lista de correos de copia
    Public Sub CorreoCopia()
        Dim ldtbCorreosCopia As New DataTable
        Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"chr_CodigoTabla", 33}

        ldtbCorreosCopia = Nothing
        Try
            lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
            ldtbCorreosCopia = lobjConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", lstrParametros)

            If (Not ldtbCorreosCopia Is Nothing) AndAlso ldtbCorreosCopia.Rows.Count > 0 Then
                For lint_fila = 0 To ldtbCorreosCopia.Rows.Count - 1
                    mstr_Copia = ldtbCorreosCopia.Rows(lint_fila).Item("vch_email")
                Next
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error: Verificar lista de correos (CC). Comuniquese con sistemas." + ex.ToString
        End Try
        
    End Sub

    ' Envio de email a los involucrados
    Private Sub EnviarCorreoInterno_V2(ByVal pstrDocumento As String)
        Dim pobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrCopias As String = ""
        Dim lstrUsuTemp As String = ""
        Dim strProveedor As String = ""
        Dim strRucProveedor As String = ""
        Dim strCopia As String = ""
        Dim listaPara As String = ""
        Dim lstr_usuariotmp As String = ""
        Dim lintTipoMensaje As Integer = 0
        Dim i As Integer = 0
        Dim ldtbDatosOC As New DataTable

        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String

        ldtbDatosOC = Nothing

        Try
            ldtbDatosOC = pobjOrdenCompra.fnc_Listar(1, hdnNumero.Value)

            If ldtbDatosOC.Rows.Count > 0 Then
                strRucProveedor = ldtbDatosOC.Rows(0).Item("co_prov").ToString()
                strProveedor = ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString()
                strSituacion = ldtbDatosOC.Rows(0).Item("ti_situ")
            End If
            ldtbDatosOC = Nothing

            Dim mstr_correocuerpo1 As String = ""

            If strSituacion = "ENV" Then
                mstr_correocuerpo1 =
                 "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                  "La OC/OS: <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + pstrDocumento + "</FONT></B>" + " requiere de su aprobación" + _
                  "<BR>" + _
                  "Provvedor: <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + strRucProveedor + " - " + strProveedor + "." + "</FONT></B>" + _
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
            Else
                mstr_correocuerpo1 =
                 "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                  "Se ha autorizado la OC/OS: <B><FONT style='BACKGROUND-COLOR: #8DE806'>" + pstrDocumento + "</FONT></B>" + _
                  "<BR>" + _
                  "Provvedor: <B><FONT style='BACKGROUND-COLOR:#8DE806'>" + strRucProveedor + " - " + strProveedor + "." + "</FONT></B>" + _
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
            End If

            lstrMailTO = IIf(mstr_Para.Trim = "", "sistemas@nuevomundosa.com", mstr_Para.Trim)
            lstrMailCC = mstr_Copia.Trim

            lstrMailSubject = "Aprobacion de Ordenes de Compra/Servicio"
            lstrMailBody = mstr_correocuerpo1

            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody)
            
        Catch ex As Exception
            lblMensaje.Text = ex.ToString
        End Try


    End Sub

    ' Envio de email a los involucrados
    Private Sub EnviarCorreoInterno(ByVal pstrDocumento As String)
        Dim pobjOrdenCompra As New NuevoMundo.Logistica.OrdenCompra
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrCopias As String = ""
        Dim lstrUsuTemp As String = ""
        Dim strProveedor As String = ""
        Dim strRucProveedor As String = ""
        Dim strCopia As String = ""
        Dim listaPara As String = ""
        Dim lstr_usuariotmp As String = ""

        Dim lintTipoMensaje As Integer = 0
        Dim i As Integer = 0

        Dim ldtbDatosOC As New DataTable

        ldtbDatosOC = Nothing
        Try
            ldtbDatosOC = pobjOrdenCompra.fnc_Listar(1, hdnNumero.Value)

            If ldtbDatosOC.Rows.Count > 0 Then
                strRucProveedor = ldtbDatosOC.Rows(0).Item("co_prov").ToString()
                strProveedor = ldtbDatosOC.Rows(0).Item("de_razo_soci").ToString()
                strSituacion = ldtbDatosOC.Rows(0).Item("ti_situ")
            End If
            ldtbDatosOC = Nothing

            Dim mailMsg As System.Net.Mail.MailMessage
            Dim mstr_correocuerpo1 As String = ""

            If strSituacion = "ENV" Then
                mstr_correocuerpo1 =
                 "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                  "La OC/OS: <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + pstrDocumento + "</FONT></B>" + " requiere de su aprobación" + _
                  "<BR>" + _
                  "Provvedor: <B><FONT style='BACKGROUND-COLOR: #ffff66'>" + strRucProveedor + " - " + strProveedor + "." + "</FONT></B>" + _
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
            Else
                mstr_correocuerpo1 =
                 "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
                  "Se ha autorizado la OC/OS: <B><FONT style='BACKGROUND-COLOR: #8DE806'>" + pstrDocumento + "</FONT></B>" + _
                  "<BR>" + _
                  "Provvedor: <B><FONT style='BACKGROUND-COLOR:#8DE806'>" + strRucProveedor + " - " + strProveedor + "." + "</FONT></B>" + _
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
            End If

            mailMsg = New System.Net.Mail.MailMessage()
            'Para
            Dim lstrTo_arreglo() As String = mstr_Para.Split(";")
            For lintIndice = 0 To lstrTo_arreglo.Length - 1
                If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            Next
            'Copia
            Dim lstrCC_arreglo() As String = mstr_Copia.Split(";")
            For lintIndice = 0 To lstrCC_arreglo.Length - 1
                If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
            Next


            Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
            Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
            Dim userCredential As New System.Net.NetworkCredential(user, password)

            With mailMsg
                '.From = New System.Net.Mail.MailAddress("O/C - O/S<aprobaciones@nuevomundosa.com>")
                .From = New System.Net.Mail.MailAddress(user)
                .Subject = "Aprobacion de Ordenes de Compra/Servicio"
                .Body = mstr_correocuerpo1
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
        Catch ex As Exception
            lblMensaje.Text = ex.ToString
        End Try
        

    End Sub

    ' Envio de email a los involucrados
#End Region

End Class