Public Class frm_RptPedidoDistribucion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.TxtFechaIni.Text = Now.ToString("dd/MM/yyyy")
            Me.TxtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        End If
    End Sub

    Protected Sub btnVerReporte_Click(sender As Object, e As EventArgs) Handles btnVerReporte.Click
        If Not fValidaFiltros() Then Exit Sub
        sMostrarReporte()
    End Sub
    Private Sub sMostrarReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strReporte As String = ""
        Dim strFe_Inic As String
        Dim strFe_Fina As String
        Dim strCodigoCorto As String


        If TxtFechaIni.Text.Count < 10 Then
            strFe_Inic = Right(TxtFechaIni.Text, 4) + Mid(TxtFechaIni.Text, 3, 2) + "0" + Left(TxtFechaIni.Text, 1)
        Else
            strFe_Inic = Right(TxtFechaIni.Text, 4) + Mid(TxtFechaIni.Text, 4, 2) + Left(TxtFechaIni.Text, 2)
        End If

        If TxtFechaFin.Text.Count < 10 Then
            strFe_Fina = Right(TxtFechaFin.Text, 4) + Mid(TxtFechaFin.Text, 3, 2) + "0" + Left(TxtFechaFin.Text, 1)
        Else
            strFe_Fina = Right(TxtFechaFin.Text, 4) + Mid(TxtFechaFin.Text, 4, 2) + Left(TxtFechaFin.Text, 2)
        End If

        strCodigoCorto = txtCodigoCorto.Text.ToString

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloVentas")
        strReporte = "ven_reporte_pedidos_distribucion"
        strURL = strURL + strPath + strReporte
        strURL = strURL & "&ARTICULO=" & strCodigoCorto
        strURL = strURL & "&FE_INICIO=" & strFe_Inic
        strURL = strURL & "&FE_FIN=" & strFe_Fina
        strURL = strURL & "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub

    Private Function fValidaFiltros() As Boolean

        Dim validation As Boolean
        Dim mensaje = ""
        validation = True

        Me.lblMsg.Text = ""

        lblMsg.ForeColor = Drawing.Color.Red

        If Me.txtCodigoCorto.Text.Length.Equals(0) Then
            mensaje += "Ingrese 4 Digitos del Item de Busqueda ! <br>"
            validation = False
        End If

        If Me.TxtFechaIni.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Inicial de Busqueda ! <br>"
            validation = False
        End If

        If Me.TxtFechaFin.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Final de Busqueda ! <br>"
            validation = False
        End If
        If Not IsDate(Me.TxtFechaIni.Text) Then
            mensaje += "La Fecha Inicial de Busqueda es incorrecta !<br>"
            validation = False
        End If
        If Not IsDate(Me.TxtFechaFin.Text) Then
            mensaje += "La Fecha Final de Busqueda es incorrecta !<br>"
            validation = False
        End If
        If IsDate(Me.TxtFechaIni.Text) And IsDate(Me.TxtFechaFin.Text) Then
            If CDate(Me.TxtFechaIni.Text) > CDate(Me.TxtFechaFin.Text) Then
                mensaje += "La Fecha Inicial de Busqueda no puede ser mayor a la Fecha Final de Busqueda !<br>"
                validation = False
            End If
        End If
        If Not validation Then
            Me.lblMsg.Text = mensaje
        End If

        Return validation

    End Function
End Class