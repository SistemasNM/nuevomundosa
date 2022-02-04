Imports NM_General

Public Class frm_ConsultarCitasPendientes_2
    Inherits System.Web.UI.Page

    Private Sub frm_ConsultarCitasPendientes_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        ' Session("@USUARIO") = "AAMPUERP"


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
        If Not Page.IsPostBack Then
            Ajax.Utility.RegisterTypeForAjax(GetType(frm_ConsultarCitasPendientes_2))
            wdpFecIni.Text = Date.Now.Date
            Call BuscarCitaxFecha()
            CargarListadoAyudantes(ddlAyudante1)
            CargarListadoAyudantes(ddlAyudante2)
            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True
            txtAcepta.Text = Session("@USUARIO")
        End If
    End Sub



    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        BuscarCitaxFecha()
    End Sub


    Private Sub BuscarCitaxFecha()
        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable
        Dim dtmFechaCita As Date

        Try
            dtmFechaCita = wdpFecIni.Text
            'dtListaCitas = objExtranet.ObtenerListaCitasLogistica(dtmFechaCita)
            dtListaCitas = objExtranet.ObtenerListaCitasLogistica_v2(dtmFechaCita)
            grvCitasLogistica.DataSource = dtListaCitas
            grvCitasLogistica.DataBind()
            lblContador.Text = grvCitasLogistica.Rows.Count
            lblMsg.Text = ""
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Carga Listado de Ayudantes
    ''' LUIS_AJ - 20210225
    ''' </summary>
    Sub CargarListadoAyudantes(ByRef ddlAyudante As DropDownList)

        Dim dtListado As New DataTable
        Dim objVentas As New Almacen.Ventas

        Try
            objVentas.ObtenerListadoAyudantes(Session("@USUARIO"), dtListado)

            ddlAyudante.DataTextField = "vch_NomTrabajador"
            ddlAyudante.DataValueField = "vch_CodTrabajador"
            ddlAyudante.DataSource = dtListado
            ddlAyudante.DataBind()
            ddlAyudante.Items.Insert(0, New ListItem("SIN AYUDANTE", "0"))

        Catch ex As Exception
        Finally
            objVentas = Nothing
            dtListado = Nothing
        End Try

    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Sub BuscarCitaxFecha_2()
        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable
        Dim dtmFechaCita As Date

        Try
            dtmFechaCita = Date.Now.Date
            dtListaCitas = objExtranet.ObtenerListaCitasLogistica(dtmFechaCita)
            grvCitasLogistica.DataSource = dtListaCitas
            grvCitasLogistica.DataBind()
        Catch ex As Exception
            Throw ex
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try

    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Sub actualizarEstadosCitas_Atendiendo(ByVal strEstado As String, ByVal strIdCita As String, ByVal strUsuario As String)

        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable

        Try
            'dtmFechaCita = wdpFecIni.Text
            dtListaCitas = objExtranet.actualizarEstadosCitasProveedores(strEstado, strIdCita, strUsuario)
            'Call BuscarCitaxFecha()
            'pnlDetalleCita.Visible = False
            'pnlListadoCitasPend.Visible = True
        Catch ex As Exception
            Throw ex
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try
    End Sub

    Private Sub actualizarEstadosCitas(ByVal strEstado As String,
                                       ByVal strIdCita As String)
        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable

        Try
            'dtmFechaCita = wdpFecIni.Text
            dtListaCitas = objExtranet.actualizarEstadosCitasProveedores(strEstado, strIdCita, Session("@USUARIO").ToString())
            lblMsg.Text = ""
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try

    End Sub

    Private Sub actualizarEstadosCitasClientesTela(ByVal strEstado As String,
                                                   ByVal strIdCita As String,
                                                   Optional ByVal strCodAyudante1 As String = "0",
                                                   Optional ByVal strCodAyudante2 As String = "0")
        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable

        Try
            'dtmFechaCita = wdpFecIni.Text
            dtListaCitas = objExtranet.actualizarEstadosCitasProveedores(strEstado, strIdCita, Session("@USUARIO").ToString(), strCodAyudante1, strCodAyudante2)

            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True
            Call BuscarCitaxFecha()

            lblMsg.Text = "Exito, Se atendió la cita correctamente."

        Catch ex As Exception
            Throw ex
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try

    End Sub

    Private Sub AtenderCita(ByVal intIDCita As Integer)
        Dim objExtranet As New NM_Extranet
        Dim strUsuario As String
        Dim intResult As Integer

        Try
            strUsuario = Session("@USUARIO")
            intResult = objExtranet.AtenderCitaLogistica(intIDCita, strUsuario)

            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True
            Call BuscarCitaxFecha()

            lblMsg.Text = "Exito, Se atendió la cita correctamente."
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objExtranet = Nothing
        End Try

    End Sub

    Private Sub AnularCita(ByVal intIDCita As Integer)
        Dim objExtranet As New NM_Extranet
        Dim strUsuario As String
        Dim intResult As Integer

        Try
            strUsuario = Session("@USUARIO")
            intResult = objExtranet.AnularCitaLogistica(intIDCita, strUsuario)

            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True
            Call BuscarCitaxFecha()

            lblMsg.Text = "Exito, Se anuló la cita correctamente."
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objExtranet = Nothing
        End Try

    End Sub

    Private Sub ConsultaDetalleCita(ByVal intIDCita As Integer)

        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable

        Try
            dtListaCitas = objExtranet.ObtenerDetalleCitaLogistica(intIDCita)
            If dtListaCitas.Rows.Count > 0 Then
                With dtListaCitas.Rows(0)
                    lblNumCita.Text = .Item("CODIGO_CITA")
                    lblFechaEnt.Text = .Item("FECHA_CITA")
                    lblHoraEnt.Text = .Item("HORA_CITA")
                    lblProveedor.Text = .Item("CODIGO_PROVEEDOR") & " - " & .Item("NOMBRE_PROVEEDOR")
                    lblEstadoCita.Text = .Item("ESTADO_CITA")
                    If .Item("ESTADO_CITA") = "ACTIVO" Then
                        btnAtender2.Enabled = True
                        btnAnular.Enabled = True
                        lblEstadoCita.ForeColor = Drawing.Color.Black
                    Else
                        btnAtender2.Enabled = False
                        btnAnular.Enabled = False
                        Select Case lblEstadoCita.Text
                            Case "ATENDIDO" : lblEstadoCita.ForeColor = Drawing.Color.Green
                            Case "NO ASISTIO" : lblEstadoCita.ForeColor = Drawing.Color.Red
                            Case Else : lblEstadoCita.ForeColor = Drawing.Color.Black
                        End Select
                    End If
                End With

                grvDetalleCita.DataSource = dtListaCitas
                grvDetalleCita.DataBind()

            End If

            pnlListadoCitasPend.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlDetalleCita.Visible = True

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try

    End Sub

    Private Sub ConsultaDetalleCitaClienteTela(ByVal intIDCita As Integer)

        Dim objExtranet As New NM_Extranet
        Dim dtListaCitas As DataTable

        Try
            dtListaCitas = objExtranet.ObtenerDetalleCitaClienteTela(intIDCita)
            If dtListaCitas.Rows.Count > 0 Then
                With dtListaCitas.Rows(0)
                    lblNumCitaCliente.Text = .Item("CODIGO_CITA")
                    lblFechaEntCliente.Text = .Item("FECHA_CITA")
                    lblHoraEntCliente.Text = .Item("HORA_CITA")
                    lblCliente.Text = .Item("CODIGO_CLIENTE") & " - " & .Item("NOMBRE_CLIENTE")
                    lblEstaoCitaCliente.Text = .Item("ESTADO_CITA")
                    If .Item("ESTADO_CITA") = "ATENDIENDO" Then
                        btnAtenderCitaCliente.Enabled = True
                        'btnAnular.Enabled = True
                        lblEstaoCitaCliente.ForeColor = Drawing.Color.Black
                    Else
                        btnAtenderCitaCliente.Enabled = False
                        'btnAnular.Enabled = False
                        Select Case lblEstaoCitaCliente.Text
                            Case "ATENDIDO" : lblEstadoCita.ForeColor = Drawing.Color.Green
                            Case "NO ASISTIO" : lblEstadoCita.ForeColor = Drawing.Color.Red
                            Case Else : lblEstadoCita.ForeColor = Drawing.Color.Black
                        End Select
                    End If
                End With

                grvDetalleCitaClienteTela.DataSource = dtListaCitas
                grvDetalleCitaClienteTela.DataBind()

            End If

            pnlListadoCitasPend.Visible = False
            pnlDetalleCitaTela.Visible = True
            pnlDetalleCita.Visible = False

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objExtranet = Nothing
            dtListaCitas = Nothing
        End Try

    End Sub

    Private Sub grvCitasLogistica_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvCitasLogistica.RowCommand
        If e.CommandName = "Atender" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Call AtenderCita(index)
        End If

        If e.CommandName = "Ingresar" Then

            actualizarEstadosCitas("ING", e.CommandArgument)
            Call BuscarCitaxFecha()
            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True

        End If

        If e.CommandName = "Atendiendo" Then

            Call BuscarCitaxFecha()
            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True

            '    'lblMsg.Text = "ATN"
            '    'actualizarEstadosCitas("ATN", e.CommandArgument)

            '    'Response.Redirect("http://servnmprb/enlacenm_movil/logistica/procesos/frm_ingarticulos_busqueda.aspx?Usuario='AAMPUERP'")

        End If

        If e.CommandName = "Terminar" Then

            actualizarEstadosCitas("ATE", e.CommandArgument)

            Call BuscarCitaxFecha()
            pnlDetalleCita.Visible = False
            pnlDetalleCitaTela.Visible = False
            pnlListadoCitasPend.Visible = True

        End If

    End Sub

    Private Sub grvCitasLogistica_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvCitasLogistica.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnAtender As Button
            Dim lblEstado As Label
            Dim imgIngresar As ImageButton
            Dim imgAtendiendo As ImageButton
            Dim imgSeleccionar As ImageButton
            Dim lblTipAtencionC As Label
            Dim lblMontaCargaFlag As Label
            Dim imgMontaCargaFlag As ImageButton
            Dim lblTipAtencion As Label
            Dim imgCierre As ImageButton
            Dim lblTipoCita As Label
            'Dim intIdCita As Integer

            'intIdCita = Convert.ToInt32(grvCitasLogistica.DataKeys(e.Row.RowIndex).Value)
            lblEstado = e.Row.FindControl("lblEstado")
            btnAtender = e.Row.FindControl("btnAtender")
            imgIngresar = e.Row.FindControl("btnIngresar")
            imgAtendiendo = e.Row.FindControl("btnAtendiendo")
            imgSeleccionar = e.Row.FindControl("btnSelect")
            lblTipAtencionC = e.Row.FindControl("lblTipAtencion")
            lblMontaCargaFlag = e.Row.FindControl("lblMontaCargaFlag")
            imgMontaCargaFlag = e.Row.FindControl("imgMontaCargaFlag")
            lblTipAtencion = e.Row.FindControl("lblTipAtencion")
            imgCierre = e.Row.FindControl("btnCierreAtencion")            
            lblTipoCita = e.Row.FindControl("lblTipoCita")

            If lblTipAtencionC.Text.Equals("CLIENTE") Then
                imgSeleccionar.Visible = False
            Else
                imgSeleccionar.Visible = True
            End If

            If lblMontaCargaFlag.Text.Equals("S") Then
                imgMontaCargaFlag.ImageUrl = "~/images/montacargas.png"
                lblMontaCargaFlag.Visible = False
            Else
                imgMontaCargaFlag.Visible = False                
            End If

            If lblEstado.Text = "ACT" Then
                btnAtender.Visible = False
                imgIngresar.Visible = True
                imgAtendiendo.Visible = False
                imgCierre.Visible = False
                lblEstado.Text = "PENDIENTE"
                lblEstado.ForeColor = Drawing.Color.Red
            End If

            If lblEstado.Text = "ING" Then
                btnAtender.Visible = False
                imgIngresar.Visible = False
                imgAtendiendo.Visible = True
                imgCierre.Visible = False
                lblEstado.Text = "INGRESANDO"
                lblEstado.ForeColor = Drawing.Color.Red
            End If

            If lblEstado.Text = "ATN" Then
                btnAtender.Visible = False
                imgIngresar.Visible = False
                If lblTipAtencion.Text.Equals("CLIENTE") Then
                    imgAtendiendo.Visible = False
                    If lblTipoCita.Text.Equals("Entrega de Tela") Then
                        imgCierre.Visible = False
                        imgSeleccionar.Visible = True
                        imgSeleccionar.ImageUrl = "~/images/Yes.gif"
                        imgSeleccionar.ToolTip = "Finalizar Cita Cliente Tela"
                    Else
                        imgCierre.Visible = True
                    End If

                    
                End If
                If lblTipAtencion.Text.Equals("PROVEEDOR") Then
                    imgAtendiendo.Visible = True
                    imgCierre.Visible = False
                End If
                lblEstado.Text = "ATENDIENDO"
                lblEstado.ForeColor = Drawing.Color.Green
            End If

            If lblEstado.Text = "ANU" Then
                btnAtender.Visible = False
                imgIngresar.Visible = False
                imgAtendiendo.Visible = False
                imgCierre.Visible = False
                lblEstado.Text = "ANULADO"
                lblEstado.ForeColor = Drawing.Color.Red
            End If

            If lblEstado.Text = "ATENDIDO" Then
                btnAtender.Visible = False
                imgIngresar.Visible = False
                imgAtendiendo.Visible = False
                lblEstado.Text = "ATENDIDO"
                imgCierre.Visible = False
                lblEstado.ForeColor = Drawing.Color.Green
            End If

            If lblEstado.Text = "NO ASISTIO" Then
                btnAtender.Visible = False
                imgIngresar.Visible = False
                imgAtendiendo.Visible = False
                imgCierre.Visible = False
                lblEstado.Text = "NO ASISTIO"
                lblEstado.ForeColor = Drawing.Color.Red
            End If



            'If lblEstado.Text = "ACT" Then
            '    btnAtender.Visible = True
            '    lblEstado.Visible = False
            'Else
            '    btnAtender.Visible = False
            '    lblEstado.Visible = True
            '    If lblEstado.Text = "ATENDIDO" Then
            '        lblEstado.ForeColor = Drawing.Color.Green
            '    End If
            'End If
        End If
    End Sub

    Private Sub grvCitasLogistica_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvCitasLogistica.RowCreated
        Dim btnSelect As ImageButton

        btnSelect = e.Row.Cells(0).FindControl("btnSelect")

        If Not btnSelect Is Nothing Then
            ScriptManager1.RegisterAsyncPostBackControl(btnSelect)
        End If
    End Sub

    Private Sub grvCitasLogistica_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles grvCitasLogistica.SelectedIndexChanged
        Dim row As GridViewRow = grvCitasLogistica.SelectedRow
        Dim strCitaSeleccionada As String
        Dim strTipoCita As String
        Dim strTipoAtencion As String

        'strCitaSeleccionada = grvCitasLogistica.DataKeys(row.RowIndex).Value
        strCitaSeleccionada = grvCitasLogistica.DataKeys(row.RowIndex).Values(0).ToString()
        strTipoCita = grvCitasLogistica.DataKeys(row.RowIndex).Values(1).ToString()
        strTipoAtencion = grvCitasLogistica.DataKeys(row.RowIndex).Values(2).ToString()

        If strTipoAtencion = "CLIENTE" And strTipoCita = "Entrega de Tela" Then
            Call ConsultaDetalleCitaClienteTela(strCitaSeleccionada)
        Else
            Call ConsultaDetalleCita(strCitaSeleccionada)
        End If
        


    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Call RegresarListado()
    End Sub

    Private Sub RegresarListado()
        pnlListadoCitasPend.Visible = True
        pnlDetalleCita.Visible = False
        pnlDetalleCitaTela.Visible = False
    End Sub

    Private Sub AnularCita()
        Dim intIDCita As Integer
        intIDCita = CInt(lblNumCita.Text)
        Call AnularCita(intIDCita)
    End Sub


    Protected Sub btnAtender2_Click(sender As Object, e As EventArgs) Handles btnAtender2.Click
        Dim intIDCita As Integer
        intIDCita = CInt(lblNumCita.Text)
        Call AtenderCita(intIDCita)
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        Call AnularCita()
    End Sub

    Protected Sub btnRegresarCliente_Click(sender As Object, e As EventArgs) Handles btnRegresarCliente.Click
        Call RegresarListado()
    End Sub

    Protected Sub btnAnularCitaCliente_Click(sender As Object, e As EventArgs) Handles btnAnularCitaCliente.Click
        Call AnularCita()
    End Sub

    Protected Sub btnAtenderCitaCliente_Click(sender As Object, e As EventArgs) Handles btnAtenderCitaCliente.Click
        Dim intIDCita As Integer
        Dim strCodAyudante1 As String
        Dim strCodAyudante2 As String

        
        Try
            intIDCita = CInt(lblNumCitaCliente.Text)
            strCodAyudante1 = ddlAyudante1.SelectedValue.ToString
            strCodAyudante2 = ddlAyudante2.SelectedValue.ToString

            If (ddlAyudante1.SelectedValue = ddlAyudante2.SelectedValue) And ddlAyudante1.SelectedValue <> "0" Then
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", "alert('Debe seleccionar diferentes ayudantes.')", True)
                ddlAyudante1.Focus()
                Exit Sub
            End If

            Call actualizarEstadosCitasClientesTela("ATE", intIDCita, strCodAyudante1, strCodAyudante2)

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try


    End Sub
End Class