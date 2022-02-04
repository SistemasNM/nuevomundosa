Imports NuevoMundo

Public Class frm_SeguimientoDocLog
    Inherits System.Web.UI.Page
    Dim strMensaje As String = ""

    Dim TipoDoc As String = ""
    Dim TipoCom As String = ""
    Dim FechaInicio As String = ""
    Dim FechaFin As String = ""
    Dim CentroCosto As String = ""
    Dim Solicitante As String = ""
    Dim Proveedor As String = ""
    Dim Activo_CTC As String = ""
    Dim Estado As String = ""
    Dim Importacion As String = ""

    Private Sub frm_SeguimientoDocLog_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ATORRESC"

        If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
            Dim objRequest As New BLITZ_LOCK.clsRequest
            Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
        End If

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("../../intranet/finsesion.htm")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtSerie.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
            txtNumeroDocumento.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
            LimpiaControles()
        End If
    End Sub

    Protected Sub lnkConsultar_Click(sender As Object, e As EventArgs) Handles lnkConsultar.Click
        Consulta()
    End Sub

    Protected Sub ibtConsultar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtConsultar.Click
        Consulta()
    End Sub

    Protected Sub lnkExportar_Click(sender As Object, e As EventArgs) Handles lnkExportar.Click
        AsignaVariables()
        ListarExportar()
    End Sub

    Private Sub rdlDocumentos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdlDocumentos.SelectedIndexChanged
        If rdlDocumentos.Items.Item(0).Selected = True Then
            ddlTipo.Enabled = False
            chkImportacion.Checked = False
            chkImportacion.Enabled = False
            txtCodSolicitante.Enabled = True
            txtDesSolicitante.Text = ""
        Else
            ddlTipo.Enabled = True
            chkImportacion.Checked = False
            chkImportacion.Enabled = True
            txtCodSolicitante.Enabled = False
        End If
    End Sub

#Region "Metodos-Funciones"
    ' Limpia controles
    Public Sub LimpiaControles()
        rdlDocumentos.Items.Item(0).Selected = False
        rdlDocumentos.Items.Item(1).Selected = False
        txtFechaIni.Text = Date.Now.ToString("dd/MM/yyyy")
        txtFechaFin.Text = Date.Now.ToString("dd/MM/yyyy")
        txtCodCentroCosto.Text = ""
        txtDesCentroCosto.Text = ""
        txtCodSolicitante.Text = ""
        txtDesSolicitante.Text = ""
        txtCodProveedor.Text = ""
        txtDesProveedor.Text = ""
        txtCodActivo.Text = ""
        txtDesActivo.Text = ""
        txtSerie.Text = ""
        txtNumeroDocumento.Text = ""
        chkImportacion.Checked = False
    End Sub

    ' Consulta OC/OS, Req segun parametros
    Public Sub Consulta()
        Dim dtbRequisiscion As New DataTable
        Dim objRequisiscion As New clsRequisicion
        Dim intNumDoc As Integer = 0

        dtbRequisiscion = Nothing
        lblMensaje.Text = ""
        lblNumDoc.Text = ""
        drgListado.DataSource = Nothing
        drgListado.Visible = False

        Try
            'ValidarParametrosConsulta()
            If strMensaje.Length = 0 Then
                'variables
                AsignaVariables()
                If txtNumeroDocumento.Text.Length > 0 Then
                    objRequisiscion.NumeroDocumento = txtSerie.Text & "-" & txtNumeroDocumento.Text
                Else
                    objRequisiscion.NumeroDocumento = ""
                End If
                'consulta
                objRequisiscion.fnc_ListarSegCabecera(dtbRequisiscion, TipoDoc, TipoCom, FechaInicio, FechaFin, CentroCosto, _
                                                      Solicitante, Proveedor, Activo_CTC, Importacion, Estado)

                intNumDoc = dtbRequisiscion.Rows.Count
                If intNumDoc > 0 Then
                    drgListado.DataSource = dtbRequisiscion
                    drgListado.DataBind()
                    drgListado.Visible = True
                Else
                    drgListado.DataSource = Nothing
                    drgListado.Visible = False
                    lblMensaje.Text = "No existe datos para la consulta realizada."
                End If
                lblNumDoc.Text = "# Doc. encontrados: " & intNumDoc.ToString
            Else
                lblMensaje.Text = strMensaje
            End If
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try

    End Sub

    ' Validar Parametrso
    Public Function ValidarParametrosConsulta() As String
        If rdlDocumentos.Items(0).Selected = False And rdlDocumentos.Items(1).Selected = False Then
            strMensaje = "Debe elegir un tipo de documento a consultar."
            Return strMensaje
            Exit Function
        End If
        If (Trim(txtFechaIni.Text).Length > 0 And Trim(txtFechaFin.Text).Length = 0) _
            Or (Trim(txtFechaIni.Text).Length = 0 And Trim(txtFechaFin.Text).Length > 0) Then
            strMensaje = "Debe ingresar correctamente las fechas."
            Return strMensaje
            Exit Function
        End If
        If txtFechaIni.Text.Length > 0 And txtFechaFin.Text.Length > 0 Then
            If CDate(txtFechaIni.Text) > CDate(txtFechaFin.Text) Then
                strMensaje = "La fecha inicial debe ser meyor o igual a la fecha final."
                Return strMensaje
                Exit Function
            End If
        End If
        If txtDesCentroCosto.Text.Length > 0 And txtDesCentroCosto.Text.Length = 0 Then
            strMensaje = "Eliga un centro de costo valido."
            Return strMensaje
            Exit Function
        End If
        If txtCodProveedor.Text.Length > 0 And txtDesProveedor.Text.Length = 0 Then
            strMensaje = "Eliga un proveedor valido."
            Return strMensaje
            Exit Function
        End If
        If txtCodActivo.Text.Length > 0 And txtDesActivo.Text.Length = 0 Then
            strMensaje = "Eliga un Activo/CTC valido."
            Return strMensaje
            Exit Function
        End If
        If Trim(txtFechaIni.Text).Length = 0 And Trim(txtFechaFin.Text).Length = 0 _
            And Trim(txtCodCentroCosto.Text).Length = 0 _
            And Trim(txtCodProveedor.Text).Length = 0 _
            And Trim(txtNumeroDocumento.Text).Length = 0 _
            And Trim(txtCodActivo.Text).Length = 0 _
            And chkImportacion.Checked = False Then
            strMensaje = "Ingrese parametros para la consulta."
            Return strMensaje
            Exit Function
        End If
        Return strMensaje
    End Function

    ' Listar seguimiento
    Public Sub ListarDetalleSeg(strNumero As String)

        If strNumero.Length > 0 Then

            Dim strURL As String
            Dim strPath As String
            Dim strScript As String
            Dim strReporte As String

            'CAMBIO DG INI
            'strPath = "%2fNM_Reportes%2f"
            'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
            'strURL = strURL + "logistica_SeguimientoDocDet"
            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
            strReporte = "log_seguimientoDocDet"
            strURL = strURL + strPath + strReporte
            'CAMBIO DG FIN
            strURL = strURL + "&vch_NumeroDocumento=" + strNumero

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"

            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If

    End Sub

    'Listar reporte detalle de la requisicion
    Public Sub ListarDetReq(strRequisicion As String)
        If strRequisicion.Length > 0 Then

            Dim strURL As String
            Dim strPath As String
            Dim strScript As String
            Dim strReporte As String
            'CAMBIO DG INI
            'strPath = "%2fNM_Reportes%2f"
            'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
            'strURL = strURL + "logistica_DetalleRequisiciones"
            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
            strReporte = "log_detalle_requisiciones"
            strURL = strURL + strPath + strReporte
            'CAMBIO DG FIN


            strURL = strURL + "&chr_Estado=" + ""
            strURL = strURL + "&vch_NumeroDocumento=" + strRequisicion

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"

            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
        End If

    End Sub

    'Listar reporte de exportacion
    Public Sub ListarExportar()
        Dim strURL As String
        Dim strPath As String
        Dim strScript As String
        Dim strReporte As String
        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        'strURL = strURL + "logistica_SeguimientoDocExportar"
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strReporte = "log_seguimientoDocExportar"
        strURL = strURL + strPath + strReporte
        'CAMBIO DG FIN


        strURL = strURL + "&TipDoc=" + TipoDoc
        strURL = strURL + "&TipCom=" + TipoCom
        strURL = strURL + "&FecIni=" + FechaInicio
        strURL = strURL + "&FecFin=" + FechaFin
        strURL = strURL + "&CenCos=" + CentroCosto
        strURL = strURL + "&Solicita=" + Solicitante
        strURL = strURL + "&Proveedor=" + Proveedor
        strURL = strURL + "&ActCtc=" + Activo_CTC
        strURL = strURL + "&Importa=" + Importacion

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub

    ' Ver reporte Requisicion
    Public Sub VerRq(strNumeroRq As String)
        Dim lstrURL, strScript As String
        lstrURL = "../CrystalReports/_Seguridad.asp?strEmpresa=" + Session("@EMPRESA") + "&strRequisicion=" + strNumeroRq + "&strUsuario=" + Session("@USUARIO")
        strScript = "fMostrarReporte('" & lstrURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub

    ' Ver reporte Orden de compra
    Public Sub VerOC(strNumeroOC As String)
        Dim lstrURL, strScript As String
        lstrURL = "../CrystalReports/_Logistica.asp?strFormulario=LOG20006&strOC=" + strNumeroOC
        strScript = "fMostrarReporte('" & lstrURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub

#End Region

#Region "Grilla"

    Private Sub drgListado_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles drgListado.ItemCommand
        Dim strDocumento As String
        Dim strRequisicion As String

        strDocumento = ""
        strRequisicion = ""

        Dim objlblNumReq As Label = CType(e.Item.FindControl("lblReq"), Label)
        Dim objlblNumDoc As Label = CType(e.Item.FindControl("lblNumDoc"), Label)

        If rdlDocumentos.Items.Item(0).Selected = True Then
            strDocumento = "REQ" & objlblNumReq.Text
            strRequisicion = objlblNumReq.Text
        Else
            strDocumento = "OCO" & objlblNumDoc.Text
            strRequisicion = objlblNumReq.Text
        End If

        Select Case e.CommandName
            Case "Seg"
                ListarDetalleSeg(strDocumento)
            Case "Req"
                ListarDetReq(strRequisicion)
            Case "VerOC"
                VerOC(objlblNumDoc.Text)
            Case "VerRq"
                VerRq(objlblNumReq.Text)
        End Select
    End Sub

    Private Sub drgListado_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles drgListado.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)

                ' Cotizacion
                Dim lblCotozacion As Label = CType(e.Item.FindControl("lblCotizacion"), Label)
                Dim btnCotizacion As ImageButton = CType(e.Item.FindControl("btnCotizacion"), ImageButton)
                Select Case Trim(lblCotozacion.Text).Length
                    Case Is > 0
                        btnCotizacion.ImageUrl = "../images/check.jpg"
                        btnCotizacion.Visible = True
                End Select

                ' AprobadorA
                Dim lblSituacionA As Label = CType(e.Item.FindControl("lblSituacionA"), Label)
                Dim btnAprobadoA As ImageButton = CType(e.Item.FindControl("btnSituacionA"), ImageButton)
                Select Case Trim(lblSituacionA.Text)
                    Case "APR"
                        btnAprobadoA.ImageUrl = "../images/check.jpg"
                        btnAprobadoA.Visible = True
                    Case "SOL"
                        btnAprobadoA.ImageUrl = "../images/no.gif"
                        btnAprobadoA.Visible = True
                    Case ""
                        btnAprobadoA.ImageUrl = ""
                        btnAprobadoA.Visible = False
                End Select

                ' AprobadorB
                Dim lblSituacionB As Label = CType(e.Item.FindControl("lblSituacionB"), Label)
                Dim btnAprobadoB As ImageButton = CType(e.Item.FindControl("btnSituacionB"), ImageButton)
                Select Case Trim(lblSituacionB.Text)
                    Case "APR"
                        btnAprobadoB.ImageUrl = "../images/check.jpg"
                        btnAprobadoB.Visible = True
                    Case "SOL"
                        btnAprobadoB.ImageUrl = "../images/no.gif"
                        btnAprobadoB.Visible = True
                    Case ""
                        btnAprobadoB.ImageUrl = ""
                        btnAprobadoB.Visible = False
                End Select

                ' AprobadorB
                Dim lblSituacionC As Label = CType(e.Item.FindControl("lblSituacionC"), Label)
                Dim btnAprobadoC As ImageButton = CType(e.Item.FindControl("btnSituacionC"), ImageButton)
                Select Case Trim(lblSituacionC.Text)
                    Case "APR"
                        btnAprobadoC.ImageUrl = "../images/check.jpg"
                        btnAprobadoC.Visible = True
                    Case "SOL"
                        btnAprobadoC.ImageUrl = "../images/no.gif"
                        btnAprobadoC.Visible = True
                    Case ""
                        btnAprobadoC.ImageUrl = ""
                        btnAprobadoC.Visible = False
                End Select

                ' Ingreso
                Dim lblIngreso As Label = CType(e.Item.FindControl("lblIngreso"), Label)
                Dim btnIngreso As ImageButton = CType(e.Item.FindControl("btnIngreso"), ImageButton)
                Dim NumeroIngresos As Integer
                NumeroIngresos = CInt(Trim(lblIngreso.Text))
                Select Case NumeroIngresos
                    Case Is > 0
                        btnIngreso.ImageUrl = "../images/check.jpg"
                        btnIngreso.Visible = True
                End Select

                ' Conformidad
                Dim lblConformidad As Label = CType(e.Item.FindControl("lblConformidad"), Label)
                Dim btnConformidad As ImageButton = CType(e.Item.FindControl("btnConformidad"), ImageButton)
                Select Case Trim(lblConformidad.Text)
                    Case "APR"
                        btnConformidad.ImageUrl = "../images/check.jpg"
                        btnConformidad.Visible = True
                    Case "SOL"
                        btnConformidad.ImageUrl = "../images/no.gif"
                        btnConformidad.Visible = True
                    Case ""
                        btnConformidad.ImageUrl = ""
                        btnConformidad.Visible = False
                End Select

                ' Ver orden de compra
                Dim lblOrden As Label = CType(e.Item.FindControl("lblNumDoc"), Label)
                Dim btnVerOC As ImageButton = CType(e.Item.FindControl("ibtVerOC"), ImageButton)
                If Trim(lblOrden.Text).Length = 0 Then
                    btnVerOC.Visible = False
                End If

                ' Ver requisiscion
                Dim lblReq As Label = CType(e.Item.FindControl("lblReq"), Label)
                Dim btnVerRq As ImageButton = CType(e.Item.FindControl("ibtVerRq"), ImageButton)
                Dim btnDetRq As ImageButton = CType(e.Item.FindControl("ibtDetReq"), ImageButton)

                If Trim(lblReq.Text).Length = 0 Then
                    btnVerRq.Visible = False
                    btnDetRq.Visible = False
                End If

        End Select
    End Sub
#End Region

    Public Sub AsignaVariables()
        If rdlDocumentos.Items.Item(0).Selected = True Then TipoDoc = "REQ"
        If rdlDocumentos.Items.Item(1).Selected = True Then TipoDoc = "OCO"
        TipoCom = ddlTipo.SelectedValue
        FechaInicio = Mid(txtFechaIni.Text, 7, 4) & Mid(txtFechaIni.Text, 4, 2) & Mid(txtFechaIni.Text, 1, 2)
        FechaFin = Mid(txtFechaFin.Text, 7, 4) & Mid(txtFechaFin.Text, 4, 2) & Mid(txtFechaFin.Text, 1, 2)
        CentroCosto = Trim(txtCodCentroCosto.Text)
        Solicitante = Trim(txtCodSolicitante.Text)
        Proveedor = Trim(txtCodProveedor.Text)
        Activo_CTC = Trim(txtCodActivo.Text)

        If chkImportacion.Checked = True Then
            Importacion = "S"
        Else
            Importacion = "N"
        End If
    End Sub
End Class