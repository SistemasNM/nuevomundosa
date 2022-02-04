Public Class LOG20003
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmision As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaTope As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCentroCosto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUnidad As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlmacen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComprador As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAprobar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnular As System.Web.UI.WebControls.Button
    Protected WithEvents lblTipo As System.Web.UI.WebControls.Label
    Protected WithEvents btnDesaprobar As System.Web.UI.WebControls.Button
    Protected WithEvents btnRechazar As System.Web.UI.WebControls.Button
    Protected WithEvents tblCentroCosto As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnImprimir As System.Web.UI.WebControls.Button
    Protected WithEvents lblEstado As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel3 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel4 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel5 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel6 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel7 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel8 As System.Web.UI.WebControls.Panel
    Protected WithEvents hdMotivo As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdAnular As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnEditarFecInstal As System.Web.UI.WebControls.ImageButton
    Protected WithEvents hdnFlg As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnReiniciar As System.Web.UI.WebControls.Button
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

    Private Enum enuTipoEnvioMail
        REQ_X_APROBAR = 1
        REQ_DESAPROBADA = 2
        REQ_RECHAZADA = 3
        REQ_REINICIO = 4
    End Enum


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        '--INICIO: VERIFICAR LA SESION
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "LALANOCA"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lstrRequisicion As String
        Dim lstrEstado As String
        Response.Cache.SetNoStore()
        lstrRequisicion = Request("strRequisicion")
        lstrEstado = Request("strEstado")
        'lstrRequisicion = "0002-0000010701"
        'lstrEstado = "ACT"
        Me.tblCentroCosto.Visible = False
        If Not IsPostBack Then
            lblEstado.Text = lstrEstado
            BuscarRequisicion(lstrRequisicion)

            txtDocumento.Attributes.Add("readonly", "readonly")
            txtEmision.Attributes.Add("readonly", "readonly")
            txtFechaTope.Attributes.Add("readonly", "readonly")
            txtComprador.Attributes.Add("readonly", "readonly")
            txtCentroCosto.Attributes.Add("readonly", "readonly")
            txtUnidad.Attributes.Add("readonly", "readonly")
            txtAlmacen.Attributes.Add("readonly", "readonly")
            txtObservaciones.Attributes.Add("readonly", "readonly")
            btnRechazar.Attributes.Add("onClick", "javascript:return Rechazar('');")
            'btnDesaprobar.Attributes.Add("onClick", "javascript:return Desaprobar();")
            'REQSIS201900029 - DG - INI
            btnEditarFecInstal.Attributes.Add("onClick", "javascript:btnEditarFecha_Onclick();")
            btnReiniciar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion();")
            'REQSIS201900029 - DG - FIN
        End If
        'REQSIS201900029 - DG - INI
        If hdnFlg.Value = "1" Then
            BuscarRequisicion(lstrRequisicion)
        End If
        'REQSIS201900029 - DG - FIN
    End Sub

    Private Function Rechazar() As Boolean
        Dim pdtCorreos As DataTable
        Dim lobjRequisicion As OFISIS.OFILOGI.Requisiciones
        Dim cMotivo As String
        Dim bAnular As String

        cMotivo = hdMotivo.Value
        bAnular = IIf(hdAnular.Value = "true", 1, 0)

        lobjRequisicion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjRequisicion.Codigo = txtDocumento.Text
        If lobjRequisicion.Rechazar(bAnular, cMotivo) Then
            If lobjRequisicion.SetDatos.Tables(0).Rows.Count > 0 Then
                If lobjRequisicion.SetDatos.Tables(0).Rows(0)("RES") = "OK" Then
                    If lobjRequisicion.SetDatos.Tables(1).Rows.Count > 0 Then
                        pdtCorreos = lobjRequisicion.SetDatos.Tables(1)
                        If pdtCorreos.Rows(0).Item("TIPO_RESULTADO") = "RECHAZADO" Then
                            EnviarCorreo_Requisicion(enuTipoEnvioMail.REQ_RECHAZADA, pdtCorreos)
                        End If
                    End If
                    Return True
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('" + lobjRequisicion.Tabla.Rows(0)("RES") + "');</Script>")
                    Return False
                End If
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('Error al rechazar.');</Script>")
                Return False
            End If
        End If
    End Function

    Private Function Desaprobar() As Boolean

        Dim lobjRequisicion As OFISIS.OFILOGI.Requisiciones
        lobjRequisicion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))

        lobjRequisicion.Codigo = txtDocumento.Text
        If lobjRequisicion.Desaprobar() Then
            If lobjRequisicion.Tabla.Rows.Count > 0 Then
                If lobjRequisicion.Tabla.Rows(0)("RES") = "OK" Then
                    Return True
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('" + lobjRequisicion.Tabla.Rows(0)("RES") + "');</Script>")
                    Return False
                End If
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('Error al desaprobar.');</Script>")
                Return False
            End If
        End If
    End Function

    Private Function Aprobar() As Boolean
        Dim lobjRequisicion As OFISIS.OFILOGI.Requisiciones
        Dim pdtCorreos As DataTable

        lobjRequisicion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjRequisicion.Codigo = txtDocumento.Text
        lobjRequisicion.Detalle = CType(Session("DETALLE_REQUISICION"), DataTable)

        Try
            If lobjRequisicion.Aprobar_2() Then
                If lobjRequisicion.SetDatos.Tables(0).Rows.Count > 0 Then
                    If lobjRequisicion.SetDatos.Tables(0).Rows(0)("RES") = "OK" Then
                        If lobjRequisicion.SetDatos.Tables(1).Rows.Count > 0 Then
                            pdtCorreos = lobjRequisicion.SetDatos.Tables(1)
                            If pdtCorreos.Rows(0).Item("TIPO_RESULTADO") = "PASO_NEXT" Then
                                EnviarCorreo_Requisicion(enuTipoEnvioMail.REQ_X_APROBAR, pdtCorreos)
                            ElseIf pdtCorreos.Rows(0).Item("TIPO_RESULTADO") = "PASO_LOGISTICA" Then
                                EnviarCorreo(pdtCorreos)
                            End If
                        End If
                        Return True
                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('" + lobjRequisicion.Tabla.Rows(0)("RES") + "');</Script>")
                        Return False
                    End If
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('Error al aprobar.');</Script>")
                    Return False
                End If
            End If
        Catch ex As Exception

        End Try

    End Function

    'Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable, pdtDatos As DataTable)
    '    Dim i As Integer
    '    Dim lstrCuerpoMensaje As String = ""
    '    Dim lstrTitulo As String = ""
    '    Dim lstrPara As String = ""
    '    Dim lstrCopia As String = "sistemas@nuevomundosa.com"

    '    If pdtDatos.Rows(0).Item("Tipo") <> 0 Then
    '        Exit Sub
    '    End If

    '    With pdtDatos.Rows(0)

    '        lstrTitulo = "Req. " + .Item("NumeroRequisicion") + " por convertir en órden de " + IIf(Left(.Item("NumeroRequisicion"), 4) = "0001", "compra", "servicio") + "."
    '        lstrCuerpoMensaje = "La requisición número : " + _
    '                            .Item("NumeroRequisicion") + _
    '                            ", ha sido aprobada por : " _
    '                            + .Item("Usuario") + _
    '                            ", esta pendiente de convertir en órden de " + _
    '                            IIf(Left(.Item("NumeroRequisicion"), 4) = "0001", "compra", "servicio") + "."
    '    End With


    '    For i = 0 To pdtCorreos.Rows.Count - 1
    '        If lstrPara.Trim.Length = 0 Then
    '            lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
    '        Else
    '            lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
    '        End If
    '    Next i

    '    Dim mailMsg As System.Net.Mail.MailMessage
    '    mailMsg = New System.Net.Mail.MailMessage()

    '    'Configurar arreglo para el TO
    '    Dim lstrTo_arreglo() As String = lstrPara.Split(";")
    '    For lintIndice = 0 To lstrTo_arreglo.Length - 1
    '        If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
    '    Next

    '    'Si no hay destinatario que lo envie a sistemas

    '    If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

    '    'Configurar arreglo para el CC
    '    Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
    '    For lintIndice = 0 To lstrCC_arreglo.Length - 1
    '        If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
    '    Next

    '    With mailMsg
    '        .From = New System.Net.Mail.MailAddress("Requisiciones<aprobaciones@nuevomundosa.com>")
    '        .Subject = lstrTitulo
    '        .Body = lstrCuerpoMensaje
    '        .Priority = System.Net.Mail.MailPriority.High
    '        .IsBodyHtml = True
    '    End With

    '    Dim Servidor As New System.Net.Mail.SmtpClient
    '    Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"

    '    Servidor.Send(mailMsg)
    '    Servidor = Nothing
    'End Sub

    Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable)

        Dim i As Integer
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""


        Try
            For i = 0 To pdtCorreos.Rows.Count - 1
                If lstrPara.Trim.Length = 0 Then
                    lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                Else
                    lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
                End If
                lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia") + ";" + pdtCorreos.Rows(i).Item("CorreoCopia2")
            Next i

            If lstrCopia.Trim.Length = 0 Then
                lstrCopia = ""
            Else
                lstrCopia = lstrCopia
            End If

            lstrCuerpoMensaje = ""
            lstrTitulo = ""
            With pdtCorreos.Rows(0)
                lstrTitulo = "Req. " + .Item("NumeroRequisicion") + " por convertir en órden de " + IIf(Left(.Item("NumeroRequisicion"), 4) = "0001", "COMPRA", "SERVICIO") + "."
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>LA REQUISICIÓN NÚMERO : <FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" + _
                                        .Item("NumeroRequisicion") + "</STRONG></FONT>, HA SIDO APROBADA POR : <FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" + _
                                        Strings.UCase(.Item("Creador").ToString) + "</STRONG></FONT>, ESTA PENDIENTE DE CONVERTIR EN ÓRDEN DE " + IIf(Left(.Item("NumeroRequisicion"), 4) = "0001", "COMPRA", "SERVICIO") + ".</P>" + _
                                        "<BR><BR><P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>" + _
                                        "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                                        "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                        "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                                        "Por favor no responder este correo.<BR>" + _
                                        "Departamento de Sistemas<BR>" + _
                                        "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                        "-------------------------------------------------------------------------------</P>"

                Dim mailMsg As System.Net.Mail.MailMessage
                mailMsg = New System.Net.Mail.MailMessage()

                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
                Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
                For lintIndice = 0 To lstrCC_arreglo.Length - 1
                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                Next


                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)

                With mailMsg
                    '.From = New System.Net.Mail.MailAddress("Requisiciones<aprobaciones@nuevomundosa.com>")
                    .From = New System.Net.Mail.MailAddress(user)
                    '.To.Add("lalanoca@nuevomundosa.com")
                    '.CC.Add("dccorahua@nuevomundosa.com")
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

            End With
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub

    Private Sub EnviarCorreo_Requisicion(ByVal enuTipoEnvioMail As enuTipoEnvioMail, ByRef pdtCorreos As DataTable)

        Dim i As Integer
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""
        Try
            'REQSIS201900029 - DG - INI
            If enuTipoEnvioMail = LOG20003.enuTipoEnvioMail.REQ_REINICIO Then
                For i = 0 To pdtCorreos.Rows.Count - 1
                    If lstrPara.Trim.Length = 0 Then
                        lstrPara = pdtCorreos.Rows(i).Item("CorreoCopia")
                    Else
                        lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("CorreoCopia")
                    End If
                    lstrCopia = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                Next i
                If lstrCopia.Trim.Length = 0 Then
                    lstrCopia = ""
                Else
                    lstrCopia = lstrCopia
                End If
            Else
                For i = 0 To pdtCorreos.Rows.Count - 1
                    If lstrPara.Trim.Length = 0 Then
                        lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                    Else
                        lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
                    End If
                    lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
                Next i

                If lstrCopia.Trim.Length = 0 Then
                    lstrCopia = ""
                Else
                    lstrCopia = lstrCopia
                End If
            End If

            'REQSIS201900029 - DG - FIN
            
            lstrCuerpoMensaje = ""
            lstrTitulo = ""
            With pdtCorreos.Rows(0)
                If enuTipoEnvioMail = LOG20003.enuTipoEnvioMail.REQ_X_APROBAR Then
                    lstrTitulo = "Nueva requisición por aprobar."
                    lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                                        "APROBACION PARA LA SIGUIENTE&nbsp;REQUISICION : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & .Item("NumeroRequisicion") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG></FONT></FONT></P>"

                ElseIf enuTipoEnvioMail = LOG20003.enuTipoEnvioMail.REQ_DESAPROBADA Then
                    lstrTitulo = "Requisición " & .Item("NumeroRequisicion") & " Desaprobada."
                    lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA DESAPROBADO LA SOLICITUD DE " + _
                                        "APROBACION DE LA REQUISICION : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & .Item("NumeroRequisicion") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG></FONT></FONT></P>" + _
                                        "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><STRONG>MOTIVO :</STRONG>&nbsp;&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & hdMotivo.Value.ToUpper & "</STRONG></FONT></P>"

                ElseIf enuTipoEnvioMail = LOG20003.enuTipoEnvioMail.REQ_RECHAZADA Then
                    lstrTitulo = "Requisición " & .Item("NumeroRequisicion") & " Rechazada."
                    lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA RECHAZADO LA SOLICITUD DE " + _
                                        "APROBACION DE LA REQUISICION : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & .Item("NumeroRequisicion") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG></FONT></FONT></P>" + _
                                        "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><STRONG>MOTIVO :</STRONG>&nbsp;&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & hdMotivo.Value.ToUpper & "</STRONG></FONT></P>"
                    'REQSIS201900029 - DG - INI
                ElseIf enuTipoEnvioMail = LOG20003.enuTipoEnvioMail.REQ_REINICIO Then
                    lstrTitulo = "Requisición " & .Item("NumeroRequisicion") & " Reiniciada."
                    lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA REINICIADO EL PROCESO DE SOLICITUD DE " + _
                                        "APROBACION DE LA REQUISICION : <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & .Item("NumeroRequisicion") & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                        "</STRONG></FONT></FONT></P>" + _
                                        "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><STRONG>MOTIVO :</STRONG>&nbsp;&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                        "<STRONG>" & " La fecha de Inicio no puede ser menor a la fecha actual, favor de modificar la fecha incio y fin".ToUpper & "</STRONG></FONT></P>"
                    'REQSIS201900029 - DG - FIN
                End If

                lstrCuerpoMensaje += "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                        "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                                        "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                        "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                                        "Por favor no responder este correo.<BR>" + _
                                        "Departamento de Sistemas<BR>" + _
                                        "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                        "-------------------------------------------------------------------------------</P>"

                Dim mailMsg As System.Net.Mail.MailMessage
                mailMsg = New System.Net.Mail.MailMessage()

                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
                Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
                For lintIndice = 0 To lstrCC_arreglo.Length - 1
                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)


                With mailMsg
                    '.From = New System.Net.Mail.MailAddress("Requisiciones<aprobaciones@nuevomundosa.com>")
                    .From = New System.Net.Mail.MailAddress(user)
                    '.To.Add("lalanoca@nuevomundosa.com")
                    '.CC.Add("dccorahua@nuevomundosa.com")
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

            End With
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub

    Private Function BuscarRequisicion(ByVal pstrRequisicion As String) As Boolean
        Dim lobjRequisicion As OFISIS.OFILOGI.Requisiciones
        Dim lintColumnaBotones As Integer = 9
        lobjRequisicion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        lobjRequisicion.Codigo = pstrRequisicion
        If lobjRequisicion.Consultar() Then
            With lobjRequisicion.SetDatos.Tables(0)
                txtDocumento.Text = .Rows(0)("NU_REQI")
                txtEmision.Text = Format(.Rows(0)("FE_EMIS_REQI"), "dd/MM/yyyy")
                txtFechaTope.Text = Format(.Rows(0)("FE_TOPE_REQI"), "dd/MM/yyyy")
                txtCentroCosto.Text = .Rows(0)("CO_AUXI_EMPR") + " - " + .Rows(0)("NO_AUXI")
                txtUnidad.Text = .Rows(0)("CO_UNID") + " - " + .Rows(0)("DE_UNID")
                txtAlmacen.Text = .Rows(0)("CO_ALMA") + " - " + .Rows(0)("DE_ALMA")
                txtComprador.Text = .Rows(0)("CO_COMP") + " - " + .Rows(0)("NO_COMP")
                txtObservaciones.Text = .Rows(0)("DE_OBSE_0001")
                If .Rows(0)("ST_SERV") = "S" Then
                    lblTipo.Text = "Servicio"
                    dtgDetalle.Columns(1).Visible = False
                    dtgDetalle.Columns(5).Visible = True
                    dtgDetalle.Columns(2).HeaderText = "Servicio"
                Else
                    lblTipo.Text = "Artículos"
                    dtgDetalle.Columns(1).Visible = True
                    dtgDetalle.Columns(5).Visible = False
                    dtgDetalle.Columns(2).HeaderText = ""
                End If
            End With
            Session("DETALLE_REQUISICION") = lobjRequisicion.SetDatos.Tables(1)
            BindGrid()
            With lobjRequisicion.SetDatos.Tables(2)
                If .Rows.Count = 1 Then
                    btnAnular.Visible = IIf(.Rows(0).Item("Anular") = 1, True, False)
                    btnAprobar.Visible = IIf(.Rows(0).Item("Aprobar") = 1, True, False)
                    btnRechazar.Visible = IIf(.Rows(0).Item("Rechazar") = 1, True, False)
                    btnDesaprobar.Visible = IIf(.Rows(0).Item("Desaprobar") = 1, True, False)
                    dtgDetalle.Columns(lintColumnaBotones).Visible = btnAprobar.Visible
                    'REQSIS201900029 - DG - INI
                    If lblEstado.Text = "REQUISICION POR PRE-APROBAR" Then
                        btnReiniciar.Visible = False
                    Else
                        btnReiniciar.Visible = True
                    End If
                    'REQSIS201900029 - DG - FIN
                Else
                    btnAnular.Visible = False
                    btnAprobar.Visible = False
                    btnRechazar.Visible = False
                    btnDesaprobar.Visible = False
                    'REQSIS201900029 - DG - INI
                    btnReiniciar.Visible = False
                    'REQSIS201900029 - DG - FIN
                    dtgDetalle.Columns(lintColumnaBotones).Visible = btnAprobar.Visible
                End If
            End With
            If lobjRequisicion.SetDatos.Tables(0).Rows(0)("ST_ESTA") = 1 Or _
                lobjRequisicion.SetDatos.Tables(0).Rows(0)("NU_ORCO") <> "" Then
                btnDesaprobar.Visible = False
                btnRechazar.Visible = False
                btnAnular.Visible = False
            End If
            If lobjRequisicion.SetDatos.Tables(0).Rows(0)("ST_ESTA") = 1 Then
                lblEstado.Text = Replace(lblEstado.Text, "(Cotizando)", "") + "(Cotizando)"
            End If
        End If

    End Function

    Private Sub BindGrid()
        dtgDetalle.DataSource = CType(Session("DETALLE_REQUISICION"), DataTable)
        dtgDetalle.DataBind()
    End Sub

    Private Sub dtgDetalle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDetalle.ItemCommand
        Dim ldtRes As DataTable = CType(Session("DETALLE_REQUISICION"), DataTable)
        Dim ltxtAprobado As TextBox
        Select Case UCase(e.CommandName)
            Case "EDITAR"
                dtgDetalle.EditItemIndex = e.Item.ItemIndex
                BindGrid()
            Case "CANCELAR"
                dtgDetalle.EditItemIndex = -1
                BindGrid()
            Case "ELIMINAR"
                ldtRes.Rows(e.Item.ItemIndex)("int_Estado") = 0
                BindGrid()
            Case "GRABAR"
                ltxtAprobado = CType(e.Item.FindControl("txtAprobadoE"), TextBox)
                If IsNumeric(ltxtAprobado.Text) Then
                    ldtRes.Rows(e.Item.ItemIndex)("var_Aprobado") = CStr(ltxtAprobado.Text)
                End If
                dtgDetalle.EditItemIndex = -1
                BindGrid()
            Case "REACTIVAR"
                ldtRes.Rows(e.Item.ItemIndex)("int_Estado") = 1
                BindGrid()
        End Select
        ldtRes = Nothing
    End Sub

    Private Sub dtgDetalle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDetalle.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim libtBoton1 As ImageButton = CType(e.Item.FindControl("ibtEliminar"), ImageButton)
                Dim libtBoton2 As ImageButton = CType(e.Item.FindControl("ibtReactivar"), ImageButton)
                Dim libtBoton3 As ImageButton = CType(e.Item.FindControl("ibtEditar"), ImageButton)
                If CInt(ldrvVista("int_Estado")) = 0 Then
                    e.Item.CssClass = "GridAlert"
                    libtBoton2.Visible = True
                    libtBoton1.Visible = False
                    libtBoton3.Visible = False
                Else
                    libtBoton2.Visible = False
                    libtBoton1.Visible = True
                    libtBoton3.Visible = True
                    libtBoton1.Attributes.Add("onClick", "ConfirmarEli();")
                End If

                Dim btnAdjuntar As ImageButton = CType(e.Item.FindControl("btnAjuntarS"), ImageButton)
                Dim objlblSecuencia As Label = CType(e.Item.FindControl("lblSecuencia"), Label)
                Dim strSecuencia As String = ""
                strSecuencia = objlblSecuencia.Text
                btnAdjuntar.Attributes.Add("onclick", "javascript:return fnc_AdjuntarDocs('" & " " & "')")

        End Select
    End Sub

    Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click

        'REQSIS201900029 - DG - INI
        If (DateTime.Compare(Now.ToShortDateString(), txtFechaTope.Text) > 0) Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('La fecha de instalación es menor a la fecha de aprobación. \n Debe modificar la fecha de instalación. \n O de lo contrario revertir el pedido para la modificación.');</script>")
        Else
            If Aprobar() Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Resultado('1','');</Script>")
            End If
        End If
        'If Aprobar() Then
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Resultado('1','');</Script>")
        'End If
        'REQSIS201900029 - DG - FIN
    End Sub

    Private Sub btnDesaprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesaprobar.Click

        If Desaprobar() Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Resultado('1','Se realizo la operacion con exito.');</Script>")
        End If

    End Sub

    Private Sub btnRechazar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRechazar.Click
        If hdMotivo.Value <> "" Then
            If Rechazar() Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Resultado('1','Se rechazo la Requisicion satisfactoriamente y se envio un correo al solicitante.');</Script>")
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>alert('Debe ingresar un Motivo para el Rechazo.');</Script>")
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim lstrURL As String
        lstrURL = "../CrystalReports/_Seguridad.asp?strEmpresa=" + Session("@EMPRESA") + "&strRequisicion=" + txtDocumento.Text.Trim + "&strUsuario=" + Session("@USUARIO")
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub

    'REQSIS201900029 - DG - INI
    Private Sub btnReiniciar_Click(sender As Object, e As System.EventArgs) Handles btnReiniciar.Click
        If Reiniciar() Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Resultado('1','Se realizo la operacion con exito.');</Script>")
        End If
    End Sub
    Private Function Reiniciar() As Boolean

        Dim lobjRequisicion As OFISIS.OFILOGI.Requisiciones
        lobjRequisicion = New OFISIS.OFILOGI.Requisiciones(Session("@EMPRESA"), Session("@USUARIO"))
        Try
            lobjRequisicion.Codigo = txtDocumento.Text
            Dim pdtCorreos As DataTable = lobjRequisicion.Reiniciar()
            'pdtCorreos = lobjRequisicion.Tabla
            EnviarCorreo_Requisicion(enuTipoEnvioMail.REQ_REINICIO, pdtCorreos)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'REQSIS201900029 - DG - FIN


End Class
