Imports NuevoMundo
Imports System.Data
Imports System.IO
Public Class frm_ListadosIQSUNAT
    Inherits System.Web.UI.Page
    Dim strNumDoc As String = ""
    Dim intNumReq As Integer = 0

    Private Sub frm_ListadosIQSUNAT_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DARWIN"

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

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim strNumDoc As String = ""
            Dim strSecuencia As String = ""
            Me.TxtFechaPresentacionIni.Text = ""
            Me.TxtFechaPresentacionFin.Text = ""
            Me.cmdGrabar.Visible = False
            'Me.btnAdjDet.Attributes.Add("onclick", "javascript:return fnc_AdjuntarDocs()")
        End If
    End Sub

    Protected Sub cmdGrabar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdGrabar.Click
        If ValidarParametrosConsulta() = True Then
            Dim StrFechaIni As String = Mid(Me.TxtFechaPresentacionIni.Text, 7, 4) + Mid(Me.TxtFechaPresentacionIni.Text, 4, 2) + Mid(Me.TxtFechaPresentacionIni.Text, 1, 2)
            Dim StrFechaFin As String = Mid(Me.TxtFechaPresentacionFin.Text, 7, 4) + Mid(Me.TxtFechaPresentacionFin.Text, 4, 2) + Mid(Me.TxtFechaPresentacionFin.Text, 1, 2)
            Dim strTipoMovimiento As String = Me.cmbMovimiento.SelectedValue
            'objArticulosSUNAT.fnc_Guardar_ArticulosSUNAT(dtbArticulosSUNAT, Me.cmbAnno.SelectedValue.ToString, Me.cmbMes.SelectedValue.ToString, StrFecha, "", "", "", Session("@USUARIO"))
            Exportar_IQSUNAT(StrFechaIni, StrFechaFin, strTipoMovimiento)
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        If ValidarParametrosConsulta() = True Then
            Dim StrFechaIni As String = Mid(Me.TxtFechaPresentacionIni.Text, 7, 4) + Mid(Me.TxtFechaPresentacionIni.Text, 4, 2) + Mid(Me.TxtFechaPresentacionIni.Text, 1, 2)
            Dim StrFechaFin As String = Mid(Me.TxtFechaPresentacionFin.Text, 7, 4) + Mid(Me.TxtFechaPresentacionFin.Text, 4, 2) + Mid(Me.TxtFechaPresentacionFin.Text, 1, 2)
            Dim strTipoMovimiento As String = Me.cmbMovimiento.SelectedValue
            Listar_IQSUNAT(StrFechaIni, StrFechaFin, strTipoMovimiento)
        End If
    End Sub

#Region "Metodos-Funciones"

    ' Reporte
    Public Sub ListarDetalleIQ(ByVal strCodigoArticulo As String, ByVal strPeriodo As String)
        If strPeriodo.Length > 0 Then
            Dim strURL As String
            Dim strPath As String
            Dim strScript As String

            strPath = "%2fNM_Reportes%2f"
            strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath

            strURL = strURL + "logistica_DetalleItemIQSunat"
            strURL = strURL + "&var_Co_item=" + strCodigoArticulo
            strURL = strURL + "&var_Periodo=" + strPeriodo

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"

            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

        End If
    End Sub

    'Listar IQ-Sunat
    Private Sub Listar_IQSUNAT(ByVal strFechaIni As String, ByVal strFechaFin As String, ByVal strTipoMovimiento As String)
        Try
            Dim objArticulosSUNAT As New clsArticulo
            Dim dtbArticulosSUNAT As New DataTable
            dtgLista.DataSource = Nothing
            dtgLista.Visible = False
            objArticulosSUNAT.Listar_ArticulosSUNAT(dtbArticulosSUNAT, strFechaIni, strFechaFin, strTipoMovimiento)
            intNumReq = dtbArticulosSUNAT.Rows.Count
            If intNumReq > 0 Then
                If dtbArticulosSUNAT.Rows(0).Item("Tipo_Operacion").ToString.Length > 0 Then
                    Me.cmdGrabar.Visible = True
                Else
                    Me.cmdGrabar.Visible = False
                End If
                dtgLista.DataSource = dtbArticulosSUNAT
                dtgLista.DataBind()
                dtgLista.Visible = True
            Else
                Me.cmdGrabar.Visible = False
                dtgLista.DataSource = Nothing
                dtgLista.Visible = False
                lblMsg.Text = "No existe articulos en el periodo indicado...!"
            End If
        Catch ex As Exception
            lblMsg.Text = "Error: Problema al realizar la consulta, veriquese datos. " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Exportar_IQSUNAT(ByVal strFechaIni As String, ByVal strFechaFin As String, ByVal strTipoMovimiento As String)
        Try
            Dim objArticulosSUNAT As New clsArticulo
            Dim dtbArticulosSUNAT As New DataTable
            Dim strTexto As String
            dtgLista.DataSource = Nothing
            dtgLista.Visible = False
            objArticulosSUNAT.Listar_ArticulosSUNAT(dtbArticulosSUNAT, strFechaIni, strFechaFin, strTipoMovimiento)
            intNumReq = dtbArticulosSUNAT.Rows.Count
            
            Dim sRenglon As String = Nothing
            Dim strStreamW As Stream = Nothing
            Dim ContenidoArchivo As String = Nothing
            Dim PathArchivo As String

            Dim i As Integer

            Try
                Dim fic As String
                Dim strNombre As String
                If Me.cmbMovimiento.SelectedValue = "I" Then strNombre = "I" & Format(Today.Date, "ddMMyyyy") & ".txt" Else strNombre = "S" & Format(Today.Date, "ddMMyyyy") & ".txt"
                fic = Server.MapPath("~/procesos/TXT/" & "/" & strNombre) ' Se determina el nombre del archivo con la fecha actual
                Dim strStreamWriter As New System.IO.StreamWriter(fic, False, System.Text.Encoding.Default)
                If Me.cmbMovimiento.SelectedValue = "S" Then
                    For i = 0 To dtbArticulosSUNAT.Rows.Count - 1
                        strTexto = dtbArticulosSUNAT.Rows(0).Item("Tipo_Operacion").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("establecimiento").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_Transaccion").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Presentacion_producto").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("cantidad").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_Documento_Transaccion").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_Documento").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Fecha_Transaccion").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_Documento_Bien").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_Documento_relacionado").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Razon_Social").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("merma").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Ruc_Transportista").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_guia_remision").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_guia_remision").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("placa_vehiculo").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_Licencia").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("observaciones").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Codigo_Incidencia").ToString
                        strStreamWriter.WriteLine(strTexto)
                    Next
                Else
                    For i = 0 To dtbArticulosSUNAT.Rows.Count - 1
                        strTexto = dtbArticulosSUNAT.Rows(0).Item("Tipo_Operacion").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("establecimiento").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_Transaccion").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Presentacion_producto").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("cantidad").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_Documento_Transaccion").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_Documento").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Fecha_Transaccion").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_Documento_Bien").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_Documento_relacionado").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Razon_Social").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("merma").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Ruc_Transportista").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Tipo_guia_remision").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_guia_remision").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("placa_vehiculo").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("Numero_Licencia").ToString & "|" & dtbArticulosSUNAT.Rows(i).Item("observaciones").ToString
                        strTexto = strTexto & "|" & dtbArticulosSUNAT.Rows(i).Item("Codigo_Incidencia").ToString
                        strStreamWriter.WriteLine(strTexto)
                    Next
                End If
                strStreamWriter.Close()
                Response.ContentType = "text/plain"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & strNombre)
                Response.TransmitFile(Server.MapPath("~/procesos/TXT/" & strNombre))
                Response.End()
                Listar_IQSUNAT(strFechaIni, strFechaFin, strTipoMovimiento)
            Catch ex As Exception

            End Try
        Catch ex As Exception
            lblMsg.Text = "Error: Problema al realizar la consulta, veriquese datos. " & ex.Message.ToString
        End Try
    End Sub

    ' Valida parametros
    Public Function ValidarParametrosConsulta() As Boolean
        Dim blnValida As Boolean = True

        If Me.TxtFechaPresentacionIni.Text = "" Then
            Me.lblMsg.Text = "Ingrese fecha de inicio por favor...!"
            blnValida = False
            Return blnValida
            Exit Function
        End If

        If Me.TxtFechaPresentacionFin.Text = "" Then
            Me.lblMsg.Text = "ingrese la fecha final por favor...!"
            blnValida = False
            Return blnValida
            Exit Function
        End If
        If Me.cmbMovimiento.SelectedValue = "00" Then
            Me.lblMsg.Text = "Seleccione un tipo de movimiento por favor...!"
            blnValida = False
            Return blnValida
            Exit Function
        End If
        Return blnValida
    End Function
#End Region

#Region "Grilla"

    Private Sub dtgLista_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgLista.ItemCommand
        Dim strCodigoArticulo As String = ""
        Dim strPeriodo As String = ""
        Dim objlblCodigoArticulo As Label = CType(e.Item.FindControl("lblCodigoArticulo"), Label)
        strCodigoArticulo = objlblCodigoArticulo.Text.ToString
        'strPeriodo = Me.cmbAnno.SelectedValue.ToString + Me.cmbMes.SelectedValue.ToString
        Select Case e.CommandName
            Case "Seg"
                ListarDetalleIQ(strCodigoArticulo, strPeriodo)
        End Select
    End Sub
#End Region

End Class