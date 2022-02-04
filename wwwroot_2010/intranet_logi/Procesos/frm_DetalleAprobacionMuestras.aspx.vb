
Public Class frm_DetalleAprobacionMuestras
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtObservaciones As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtTotal As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAnular As System.Web.UI.WebControls.Button
    Protected WithEvents btnAprobar As System.Web.UI.WebControls.Button
    Protected WithEvents btnDesaprobar As System.Web.UI.WebControls.Button
    Protected WithEvents btnRechazar As System.Web.UI.WebControls.Button
    Protected WithEvents tblCentroCosto As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtAlmacen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVendedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCreacion As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents dtgSeguimiento As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ''Session("@USUARIO") = "DARWIN"
        ''Session("@EMPRESA") = "01"
        ''-----------------------------------------------------------------------
        ''--INICIO: VERIFICAR LA SESION
        ''-----------------------------------------------------------------------
        'If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
        '    Response.Redirect("/intranet/finsesion.htm")
        'End If
        ''-----------------------------------------------------------------------
        ''--FINAL: VERIFICAR LA SESION
        ''-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

#End Region

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'Put user code to initialize the page here
    Response.Cache.SetNoStore()
    Dim lstrNumero As String
    Dim lstrTipo As String
    'Session("@EMPRESA") = "01"
    'Session("usuario") = "OARIAS"
    lstrNumero = Request("strNumeroSolicitud")
    lstrTipo = Request("strTipo")
    If Not IsPostBack Then
      'btnAprobar.Attributes.Add("onClick", "ConfirmarEli();")
      Mostrar_Detalle(lstrNumero)
      BuscarHistoria(lstrNumero)

      '20120910 EPM Readonly
      txtDocumento.Attributes.Add("readonly", "readonly")
      TxtFecha.Attributes.Add("readonly", "readonly")
      txtCliente.Attributes.Add("readonly", "readonly")
      txtVendedor.Attributes.Add("readonly", "readonly")
      txtAlmacen.Attributes.Add("readonly", "readonly")
      txtObservaciones.Attributes.Add("readonly", "readonly")
      txtTotal.Attributes.Add("readonly", "readonly")

    End If
    If lstrTipo = "Consulta" Then
      btnAprobar.Visible = False
      btnDesaprobar.Visible = False
    End If
    'If hdnarticuloestadistica.Value.Trim.Length = 0 Then
    '    ClientScript.RegisterStartupScript(Me.[GetType](),"estadisticasnovisible", "<script>document.all['pnldatosestadisticos'].style.display='none';</script>")
    'End If
  End Sub

  Private Sub BuscarHistoria(ByVal pstrNumero As String)
    Dim lobjCon As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    Dim ldtRes As DataTable
    Dim larrParams() = {"pchr_Empresa", Session("@EMPRESA"), "pVar_NumeroSolicitud", pstrNumero}
    ldtRes = lobjCon.ObtenerDataTable("USP_LOG_SOLICITUDMUESTRAS_SEGUIMIENTO", larrParams)
    dtgSeguimiento.DataSource = ldtRes
    dtgSeguimiento.DataBind()
    lobjCon = Nothing
    lblCreacion.Text = "Creada el : " + ldtRes.Rows(0)("var_Fecha")
  End Sub
  Private Sub Mostrar_Detalle(ByVal pstrSolicitud As String)
    Dim lobMuestras As New OFISIS.OFILOGI.Muestras_Telas
    Dim objDataSet As DataSet
    lobMuestras.NumeroSolicitud = pstrSolicitud
    lobMuestras.Detalle_SolicitudMuestras_Mostrar(objDataSet)
    With objDataSet.Tables(0)
      txtDocumento.Text = .Rows(0)("Var_NumeroSolicitud")
      txtAlmacen.Text = "013-Almacen Muestras" '.Rows(0)("var_AlmacenCodigo") + " - " + .Rows(0)("var_AlmacenNombre")
      txtObservaciones.Text = .Rows(0)("var_Observaciones")
      Me.TxtFecha.Text = .Rows(0)("Fecha")
      'txtSubTotal.Text = Format(.Rows(0)("num_Base"), "#,##0.00")
      'txtIGV.Text = Format(.Rows(0)("num_IGV"), "#,##0.00")
      'txtTotal.Text = Format(.Rows(0)("num_Total"), "#,##0.00")
      Me.txtCliente.Text = .Rows(0)("NO_CLIE")
      Me.txtVendedor.Text = .Rows(0)("Vendedor")
      'txtDescuento.Text = Format(.Rows(0)("num_Descuento"), "#,##0.00")
    End With
    dtgDetalle.DataSource = objDataSet.Tables(1)
    dtgDetalle.DataBind()
    lobMuestras = Nothing
  End Sub

  Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
    Dim lobjAprobaciones As New OFISIS.OFISEGU.Aprobaciones(Session("@EMPRESA"), Session("@USUARIO"))
    If Me.txtDocumento.Text <> "" Then
      If lobjAprobaciones.Aprobar_SolicitudMuestras(Session("@USUARIO"), txtDocumento.Text) = True Then
        Try
          EnviarCorreo(lobjAprobaciones.SetDatos.Tables(0))
        Catch ex As Exception

        End Try
      End If
    End If
    ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
  End Sub
  Private Sub EnviarCorreo(ByRef pdtCorreos As DataTable)

    Dim i As Integer
    Dim lstrCuerpoMensaje As String = ""
    Dim lstrTitulo As String = ""
    Dim lstrPara As String = ""
    Dim lstrCopia As String = ""


    For i = 0 To pdtCorreos.Rows.Count - 1
      If lstrPara.Trim.Length = 0 Then
        lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
      Else
        lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
      End If
      lstrTitulo = "Numero  de Solicitud " + pdtCorreos.Rows(i).Item("NumeroSolicitud") + " ha sido autorizada."
      lstrCuerpoMensaje = "La solicitud de muestra de " + pdtCorreos.Rows(i).Item("NumeroSolicitud") + " ha sido autorizada."
    Next i

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
            '.From = New System.Net.Mail.MailAddress("Horas Extras<aprobaciones@nuevomundosa.com>")
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

    Private Sub btnDesaprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesaprobar.Click

    End Sub

    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click

    End Sub
End Class
