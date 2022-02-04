Imports NuevoMundo
Imports System.Data.SqlClient

Public Class frm_CotizacionOrdenServicio
    Inherits System.Web.UI.Page

    Dim strNumDoc As String = ""
    Dim intNumReq As Integer = 0

    Private Sub frm_CotizacionOrdenServicio_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not Page.IsPostBack Then
            LimpiarControles()
            txtSerie.Attributes.Add("onBlur", "FormatearBusqDoc(1);")
            txtNumeroDocumento.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
        End If
    End Sub

#Region "Controles"

    Protected Sub ibtConsultar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtConsultar.Click
        ListarRequisiciones()
    End Sub

    Protected Sub lnkConsultar_Click(sender As Object, e As EventArgs) Handles lnkConsultar.Click
        ListarRequisiciones()
    End Sub

    Protected Sub lnkCotizar_Click(sender As Object, e As EventArgs) Handles lnkCotizar.Click
        GuardarCotizacionMasivo()
    End Sub

    Protected Sub ibtCotizar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibtCotizar.Click
        GuardarCotizacionMasivo()
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        ListarRequisiciones()
    End Sub
#End Region

#Region "Metodos"

    ' LimpiarControles
    Private Sub LimpiarControles()
        txtFechaIni.Text = Date.Now.ToString("dd/MM/yyyy")
        txtFechaFin.Text = Date.Now.ToString("dd/MM/yyyy")
        txtCentroCosto.Text = ""
        txtDesCentroCosto.Text = hndDesCentroCosto.Value
        txtSolicitante.Text = ""
        txtDesSolicitante.Text = hndDesSolicitante.Value
        txtSerie.Text = ""
        txtNumeroDocumento.Text = ""
        lblMensaje.Text = ""
        lblNumReq.Text = ""
        pnlListado.Visible = True
        pnlDetalle.Visible = False
        drgListaCotizacion.DataSource = Nothing
        drgDetalle.DataSource = Nothing
    End Sub

    ' ListarRequisisciones
    Private Sub ListarRequisiciones()
        Dim dtbRequisiscion As New DataTable
        Dim objRequisiscion As New clsRequisicion
        Dim FechaInicio As String = ""
        Dim FechaFin As String = ""
        Dim CentroCosto As String = ""
        Dim Solicitante As String = ""
        Dim Estado As String = ""

        dtbRequisiscion = Nothing
        lblMensaje.Text = ""
        lblNumReq.Text = ""
        drgListaCotizacion.DataSource = Nothing
        drgListaCotizacion.Visible = False
        drgDetalle.DataSource = Nothing
        drgDetalle.Visible = False
        Try
            ' Validamos paremetros elegidos
            If ValidarParametrosConsulta() = True Then
                FechaInicio = Mid(txtFechaIni.Text, 7, 4) & Mid(txtFechaIni.Text, 4, 2) & Mid(txtFechaIni.Text, 1, 2)
                FechaFin = Mid(txtFechaFin.Text, 7, 4) & Mid(txtFechaFin.Text, 4, 2) & Mid(txtFechaFin.Text, 1, 2)
                CentroCosto = Trim(txtCentroCosto.Text)
                Solicitante = Trim(txtSolicitante.Text)

                If Trim(txtSerie.Text).Length > 0 And Trim(txtNumeroDocumento.Text).Length > 0 Then
                    objRequisiscion.NumeroDocumento = Trim(txtSerie.Text) + "-" + Trim(txtNumeroDocumento.Text)
                Else
                    objRequisiscion.NumeroDocumento = ""
                End If
                Estado = ddlEstados.SelectedValue

                objRequisiscion.fnc_ListarRequisiciones(dtbRequisiscion, FechaInicio, FechaFin, CentroCosto, Solicitante, Estado)
                If Not dtbRequisiscion Is Nothing Then intNumReq = dtbRequisiscion.Rows.Count
                If intNumReq > 0 Then
                    drgListaCotizacion.DataSource = dtbRequisiscion
                    drgListaCotizacion.DataBind()
                    drgListaCotizacion.Visible = True
                    lblNumReq.Text = "Numero de requisiciones:" + intNumReq.ToString
                    pnlListado.Visible = True
                    pnlDetalle.Visible = False
                Else
                    drgListaCotizacion.DataSource = Nothing
                    drgListaCotizacion.Visible = False
                    pnlListado.Visible = True
                    pnlDetalle.Visible = False
                    lblMensaje.Text = "No existe documentos a cotizar para la consulta."
                End If
            Else
                lblMensaje.Text = "Error: Verifique parametros para la consulta."
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error: Problema al realizar la consulta, veriquese datos. " & ex.Message.ToString
        End Try

    End Sub

    ' Validar Parametrso
    Public Function ValidarParametrosConsulta() As Boolean
        Dim blnValida As Boolean = True
        If (Trim(txtFechaIni.Text).Length > 0 And Trim(txtFechaFin.Text).Length = 0) _
            Or (Trim(txtFechaIni.Text).Length = 0 And Trim(txtFechaFin.Text).Length > 0) Then
            lblMensaje.Text = "Debe ingresar correctamente las fechas"
            blnValida = False
            Return blnValida
            Exit Function
        End If

        If txtFechaIni.Text.Length > 0 And txtFechaFin.Text.Length > 0 Then
            If CDate(txtFechaIni.Text) > CDate(txtFechaFin.Text) Then
                lblMensaje.Text = "La fecha Inicial debe ser meyor o igual a la Fecha final"
                blnValida = False
                Return blnValida
                Exit Function
            End If
        End If

        If (txtCentroCosto.Text.Length > 0 And txtCentroCosto.Text.Length < 7) Or (txtCentroCosto.Text.Length > 0 And Trim(txtDesCentroCosto.Text).Length = 0) Then
            lblMensaje.Text = "Debe elegir correctamente el Centro de Costo origen."
            blnValida = False
            Return blnValida
            Exit Function
        End If

        If (txtSolicitante.Text.Length > 0 And txtSolicitante.Text.Length < 6) Or (txtSolicitante.Text.Length > 0 And Trim(txtDesSolicitante.Text).Length = 0) Then
            lblMensaje.Text = "Debe elegir un trabajador valido."
            blnValida = False
            Return blnValida
            Exit Function
        End If
        Return blnValida
    End Function

    ' Guardar Cotizacion Masivo
    Private Sub GuardarCotizacionMasivo()
        Dim objCotizacion As New clsCotizacion
        Dim strTipoDocumento As String = ""
        Dim strNumeroDocumento As String = ""
        Dim lstr_Documentos() As String
        Dim intSecuencia As Integer = 0

        Try
            If Trim(lblNumReq.Text).Length > 0 Then
                intNumReq = Integer.Parse(Mid(Trim(lblNumReq.Text), 25, Trim(lblNumReq.Text).Length))
            Else
                intNumReq = 0
            End If

            If intNumReq = 0 Then
                lblMensaje.Text = "Consulte documentos por cotizar previamente."
            Else
                strTipoDocumento = "RQS"
                lstr_Documentos = Strings.Split(hdnaprobarmasivo.Value, ",")

                If Trim(lstr_Documentos(0)).Length > 0 Then

                    ' Agregamos cotizacion en masivo
                    For lint_fila = 0 To lstr_Documentos.Length - 2
                        strNumeroDocumento = lstr_Documentos(lint_fila)
                        objCotizacion.TipoDocumento = strTipoDocumento
                        objCotizacion.NumeroDocumento = strNumeroDocumento
                        objCotizacion.UsuarioCotizacion = Session("@USUARIO")

                        If objCotizacion.fnc_InsertarCotizacionMasivo > 0 Then
                            lblMensaje.Text = "Se realizo la cotizacion del documento."
                        End If

                        lstr_Documentos(lint_fila) = ""
                    Next

                    ' Listamos
                    ListarRequisiciones()

                Else
                    lblMensaje.Text = "Error: Eliga un documento a cotizar de forma masiva."
                End If
            End If

        Catch ex As Exception
            lblMensaje.Text = "Error al cotizar el documento." & ex.Message
        End Try

    End Sub

    ' Guardar Cotizacion Masivo
    Private Sub GuardarCotizacion(strNumero, strSecuencia)
        Dim objCotizacion As New clsCotizacion
        Dim strTipoDocumento As String = ""

        Try
            strTipoDocumento = "RQS"
            objCotizacion.TipoDocumento = strTipoDocumento
            objCotizacion.NumeroDocumento = strNumero
            objCotizacion.Secuencia = strSecuencia
            objCotizacion.UsuarioCotizacion = Session("@USUARIO")
            objCotizacion.CodigoProveedor = ""
            objCotizacion.Monto = 0
            objCotizacion.Observaciones = ""
            
            ' Agregamos cotizacion
            If objCotizacion.fnc_InsertarCotizacion > 0 Then
                lblMensaje.Text = "Se realizo la cotizacion del documento."

                ' Listamos detalle de req.
                ListarDetalleReq(strNumero)

            End If
        Catch ex As Exception
            lblMensaje.Text = "Error al cotizar el documento. " & ex.Message
        End Try

    End Sub

    ' Listar reporte detalle
    Private Sub ListarDetalle(strNumeoRequisicion As String)
        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        strPath = "%2fNM_Reportes%2f"
        strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath

        strURL = strURL + "logistica_DetalleRequisiciones"
        strURL = strURL + "&vch_NumeroDocumento=" + strNumeoRequisicion

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

    ' Ver panel con el detalle
    Public Sub ListarDetalleReq(strNumero As String)
        Dim objReq As New clsRequisicion
        Dim dbtReqDet As New DataTable
        Dim strEstado As String = ""
        dbtReqDet = Nothing
        txtNumero.Text = strNumero
        Try
            If strNumero.Length > 0 Then
                strEstado = ddlEstados.SelectedValue
                objReq.fnc_ListarDetalleRequisicion(dbtReqDet, strEstado, strNumero)
                If Not dbtReqDet Is Nothing Then
                    If dbtReqDet.Rows.Count > 0 Then
                        drgDetalle.DataSource = dbtReqDet
                        drgDetalle.DataBind()
                        drgDetalle.Visible = True
                        pnlDetalle.Visible = True
                        pnlListado.Visible = False
                    Else
                        drgDetalle.DataSource = Nothing
                        drgDetalle.Visible = False
                        lblMensaje.Text = "No existen item(s) del documento por cotizar."
                    End If
                End If
            End If
        Catch ex As Exception
            lblMensaje.Text = "Error al consultar detalle de documento. " + ex.Message
        End Try

    End Sub

#End Region
    
#Region "Grillas"

    ' Listado - ItemCommand
    Private Sub drgListaCotizacion_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles drgListaCotizacion.ItemCommand
        Dim objlblNumDoc As Label = CType(e.Item.FindControl("lblNumDoc"), Label)
        Select Case e.CommandName
            Case "VerDet"
                ListarDetalleReq(objlblNumDoc.Text)
        End Select

    End Sub

    'Listado - ItemDataBound
    Private Sub drgListaCotizacion_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles drgListaCotizacion.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim lobjCotizar As CheckBox = CType(e.Item.FindControl("chkCotMasivo"), CheckBox)
                Dim btnVerAdjuntos As ImageButton = CType(e.Item.FindControl("ibtVerAdj"), ImageButton)
                Dim objlblNumDoc As Label = CType(e.Item.FindControl("lblNumDoc"), Label)
                Dim objlblNumAdj As Label = CType(e.Item.FindControl("lblNumAdj"), Label)

                ' check de cotizar
                If ddlEstados.SelectedValue = "COT" Then
                    lobjCotizar.Visible = False
                End If
                ' boton adjuntar
                If objlblNumAdj.Text = "0" Then
                    btnVerAdjuntos.Visible = False
                End If

                strNumDoc = Trim(objlblNumDoc.Text)
                btnVerAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos('" + strNumDoc + "')")
                lobjCotizar.Attributes.Add("onClick", "fnc_aprobarmasivo(this,'" + strNumDoc + "')")

                lobjCotizar = Nothing
                ldrvDatos = Nothing
                btnVerAdjuntos = Nothing
        End Select
    End Sub

    ' detalle - ItemCommand 

    Private Sub drgDetalle_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles drgDetalle.ItemCommand
        Dim strNum As String = ""
        Dim strSec As String = ""
        Dim objSec As Label = CType(e.Item.FindControl("lblSec"), Label)

        strNum = txtNumero.Text
        strSec = objSec.Text

        Select Case e.CommandName
            Case "Cotizar"
                ' Guardamos cotizacion
                GuardarCotizacion(strNum, strSec)
        End Select
    End Sub

    ' detalle - ItemDataBound
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Private Sub drgDetalle_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles drgDetalle.ItemDataBound
        Dim strSecuencia As String = ""
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim btnAdjuntar As ImageButton = CType(e.Item.FindControl("ibtAdjDet"), ImageButton)
                Dim objSec As Label = CType(e.Item.FindControl("lblSec"), Label)

                Dim objbtnCotDet As ImageButton = CType(e.Item.FindControl("ibtCotDet"), ImageButton)
                Dim objlblCot As Label = CType(e.Item.FindControl("lblCot"), Label)

                ' check de cotizar
                If objlblCot.Text = "COT" Then
                    objbtnCotDet.Visible = False
                End If

                strNumDoc = txtNumero.Text
                strSecuencia = objSec.Text
                btnAdjuntar.Attributes.Add("onclick", "javascript:return fnc_AdjuntarDocs('" + strNumDoc + "', '" + "" + strSecuencia + "')")
                objSec = Nothing
                btnAdjuntar = Nothing
        End Select
    End Sub
#End Region

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        ListarDetalleReq(txtNumero.Text)
    End Sub
End Class