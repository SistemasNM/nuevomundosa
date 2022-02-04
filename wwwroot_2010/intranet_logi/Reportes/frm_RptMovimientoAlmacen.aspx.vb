Public Partial Class frm_RptMovimientoAlmacen
    Inherits System.Web.UI.Page

  Private Sub frm_RptMovimientoAlmacen_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ATORRESC"

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
    If Not IsPostBack Then
      TxtFechaIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
      TxtFechaFin.Text = Now.ToString("dd/MM/yyyy")
      txtSeriePedidoAlmacen.Text = "0003"
      txtNumDocAlmacen.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
      txtNumPedidoAlmacen.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
    End If
  End Sub

  Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
    sMostrarReporte()
  End Sub

  Private Sub sMostrarReporte()
    Dim strURL As String = ""
    Dim strPath As String = ""
    Dim strScript As String = ""

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath + "log_movimiento_almacen"
        'CAMBIO DG FIN
    txtUrl.Text = strURL

        'strURL = strURL + "logistica_MovimientoAlmacen"
    strURL = strURL + "&dtmFechaInicio=" + Format(CDate(TxtFechaIni.Text), "yyyyMMdd")
    strURL = strURL + "&dtmFechaFinal=" + Format(CDate(TxtFechaFin.Text), "yyyyMMdd")
    strURL = strURL + "&vch_TipoDocumento=" + ddlEstado.SelectedItem.Value
    strURL = strURL + "&vch_NumDocAlmacen=" + Trim(txtNumDocAlmacen.Text)
    strURL = strURL + "&vch_NumPedidoAlmacen=" + Trim(txtSeriePedidoAlmacen.Text) + "-" + Trim(txtNumPedidoAlmacen.Text)
    strURL = strURL + "&rc:Command=Render"
    strURL = strURL + "&rc:Toolbar=true"

    strScript = "fMostrarReporte('" & strURL & "');"
    ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

  End Sub

End Class