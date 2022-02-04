Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_OrdenTrabajo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblUsu As System.Web.UI.WebControls.Label
    'Protected WithEvents lblUsuario As System.Web.UI.WebControls.Label
    Protected WithEvents lblMsgError As System.Web.UI.WebControls.Label
    Protected WithEvents lblNumeroPedido As System.Web.UI.WebControls.Label
    Protected WithEvents cboAreas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtMaquina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodSolicitante As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDesSolicitante As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents btnGuardar As System.Web.UI.WebControls.Button
    Protected WithEvents btnSalir As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblDesMaquina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents txtNumeroSoli As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

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

#End Region
    Dim strNumeroSoli As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not (Page.IsPostBack) Then
            txtFecha.Text = Format(Now, "dd/MM/yyyy")
            prcConsultaAreas()
            fncGeneraNumeroSolicitud()

        End If
    End Sub
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        GuardarSolicitud()
    End Sub

    Private Sub GuardarSolicitud()
        Dim intNumItems As Integer
        Dim objPedidos As Logistica.clsPedidos
        Dim strFecha As String
        Dim strArea As String
        Dim strMaquina As String
        Dim strSolicitante As String
        Dim strObservacion As String
        Dim strCodUsuario As String
        Dim pdtCorreos As DataTable

        strNumeroSoli = txtNumeroSoli.Text
        strFecha = txtFecha.Text
        strArea = cboAreas.SelectedValue
        strMaquina = txtMaquina.Text
        strSolicitante = txtCodSolicitante.Text
        strObservacion = txtObservaciones.Text
        strCodUsuario = Session("@USUARIO")
        Try
            objPedidos = New Logistica.clsPedidos
            pdtCorreos = objPedidos.fncGuardaSolicitudOT(strNumeroSoli, strFecha, strArea, strMaquina, strSolicitante, strObservacion)
            If Not pdtCorreos Is Nothing And pdtCorreos.Rows.Count > 0 Then
                EnviarEmail(pdtCorreos)
                intNumItems = pdtCorreos.Rows.Count
                If intNumItems > 0 Then
                    'strNumeroSoli.Text = ldtDetalle.Rows(i).Item("var_NumeroPedido")
                    lblError.Text = "Se Genero la Solicitu de OT N°: " + strNumeroSoli
                    lblError.Visible = True
                    btnGuardar.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error al guardar la Solicitud, comuniquese con Sistemas." 'ex.Message '
        End Try
    End Sub

    Public Sub prcConsultarusuario(ByVal strUsuario As String) 'As DataTable
        Dim ldtbUsuario As DataTable
        Try
            Dim objParametros As Object() = {"COD_USU", strUsuario}
            ldtbUsuario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_PedidoConsultaUsuario", objParametros)
            If Not ldtbUsuario Is Nothing Then
                txtCodSolicitante.Text = ldtbUsuario.Rows(0).Item("co_trab")
                lblDesSolicitante.Text = ldtbUsuario.Rows(0).Item("Nombres")
            Else
                txtCodSolicitante.Text = ""
                lblDesSolicitante.Text = ""
                lblError.Text = "Debe elegir un solicitante para el Vale"
            End If
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error a consultar usuario, comuniquese con Sistemas."
            'Return Nothing
        End Try
    End Sub
    ' --- Consultamos Areas
    Private Sub prcConsultaAreas()
        cboAreas.Items.Clear()
        cboAreas.Enabled = True
        cboAreas.DataSource = fncListarAreas()
        cboAreas.DataValueField = "Co_Item"
        cboAreas.DataTextField = "De_Item"
        cboAreas.DataBind()
        cboAreas.Items.Insert(0, New ListItem("Seleccione Area", ""))
    End Sub
    Private Sub cboArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        prcConsultaAreas()
    End Sub
    Public Function fncListarAreas() As DataTable
        Try
            Return New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet("Usp_Mantto_Areas_Consultar").Tables(0)
        Catch ex As Exception
            lblMsgError.Text = "Ha ocurrido un error a consultar las areas, comuniquese con Sistemas."
            Return Nothing
        End Try
    End Function
    ' --- Funcion que genera un Nuevo Numero de Solicitud
    Public Function fncGeneraNumeroSolicitud() As String
        Dim objPedido As Logistica.clsPedidos
        Dim strNumeroSoli As String
        objPedido = New Logistica.clsPedidos
        strNumeroSoli = objPedido.fncGeneraNumeroSolicitud()
        txtNumeroSoli.Text = strNumeroSoli
        Return strNumeroSoli
    End Function

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        txtNumeroSoli.Text = ""
        'txtFecha.Text = ""
        txtMaquina.Text = ""
        lblDesMaquina.Text = ""
        txtCodSolicitante.Text = ""
        lblDesSolicitante.Text = ""
        txtObservaciones.Text = ""
        lblMsgError.Text = ""
        lblError.Text = ""
        prcConsultaAreas()
        fncGeneraNumeroSolicitud()
        btnGuardar.Enabled = True
    End Sub
    ' Envio de Correo Electronico
#Region "Envio de Email"

  Private Sub EnviarEmail(ByRef pdtCorreos As DataTable)
    Dim i As Integer
    Dim lstrCuerpoMensaje As String = ""
    Dim lstrTitulo As String
    Dim lstrPara As String = ""
    Dim lstrCopia As String = ""
    strNumeroSoli = txtNumeroSoli.Text

    Try
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
      With pdtCorreos.Rows(0)
        lstrTitulo = "[Intranet] SOLICITUD DE ORDEN DE TRABAJO."
        lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                            "ORDEN DE TRABAJO &nbsp;: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                            "<STRONG>" & strNumeroSoli & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                            "</STRONG></FONT></FONT></P>" + _
                            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                            "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                            "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                            "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                            "Este correo ha sido generado automáticamente por el módulo de solicitud de OT.<BR>" + _
                            "Por favor no responder este correo.<BR>" + _
                            "Departamento de Sistemas<BR>" + _
                            "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                            "-------------------------------------------------------------------------------</P>"
        Dim mailMsg As System.Net.Mail.MailMessage
        mailMsg = New System.Net.Mail.MailMessage()

        '20121005 EPM Configurar arreglo para el To
        Dim lstrTo_arreglo() As String = lstrPara.Split(";")
        For lintIndice = 0 To lstrTo_arreglo.Length - 1
          If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
        Next
        'Si no hay destinatario que lo envie a sistemas
        If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")
        '20121005 EPM Configurar arreglo para el CC
        Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
        For lintIndice = 0 To lstrCC_arreglo.Length - 1
          If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
        Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)


        With mailMsg
                    '.From = New System.Net.Mail.MailAddress("SOLICITUD DE ORDEN DE TRABAJO<solicitudot@nuevomundosa.com>")
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
        strDestinatarios = "Se Comunico via email a: " + lstrPara + " Con Copia a: " + lstrCopia
        lblError.Text = strDestinatarios
        lblError.Visible = True
      End With
    Catch ex As Exception
      lblError.Text = ""
      lblError.Visible = False
      lblMsgError.Text = "Ha ocurrido un error al enviar email, comuniquese con Sistemas."
      ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
    End Try
  End Sub
#End Region
End Class

      