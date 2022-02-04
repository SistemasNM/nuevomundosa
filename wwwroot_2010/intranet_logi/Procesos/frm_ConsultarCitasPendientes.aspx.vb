Imports NM_General

Public Class frm_ConsultarCitasPendientes
    Inherits System.Web.UI.Page

    Private Sub frm_ConsultarCitasPendientes_Init(sender As Object, e As System.EventArgs) Handles Me.Init        
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
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            wdpFecIni.Text = Date.Now.Date
            Call BuscarCitaxFecha()
            pnlDetalleCita.Visible = False
            pnlListadoCitasPend.Visible = True
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
            dtListaCitas = objExtranet.ObtenerListaCitasLogistica(dtmFechaCita)
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

    Private Sub AtenderCita(ByVal intIDCita As Integer)
        Dim objExtranet As New NM_Extranet
        Dim strUsuario As String
        Dim intResult As Integer

        Try
            strUsuario = Session("@USUARIO")
            intResult = objExtranet.AtenderCitaLogistica(intIDCita, strUsuario)

            pnlDetalleCita.Visible = False
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
            pnlDetalleCita.Visible = True

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


    End Sub

    Private Sub grvCitasLogistica_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvCitasLogistica.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnAtender As Button
            Dim lblEstado As Label
            'Dim intIdCita As Integer

            'intIdCita = Convert.ToInt32(grvCitasLogistica.DataKeys(e.Row.RowIndex).Value)
            lblEstado = e.Row.FindControl("lblEstado")
            btnAtender = e.Row.FindControl("btnAtender")

            If lblEstado.Text = "ACT" Then
                btnAtender.Visible = True
                lblEstado.Visible = False
            Else                
                btnAtender.Visible = False
                lblEstado.Visible = True
                If lblEstado.Text = "ATENDIDO" Then
                    lblEstado.ForeColor = Drawing.Color.Green
                End If
            End If
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
        strCitaSeleccionada = grvCitasLogistica.DataKeys(row.RowIndex).Value

        Call ConsultaDetalleCita(strCitaSeleccionada)
        

    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        pnlListadoCitasPend.Visible = True
        pnlDetalleCita.Visible = False
    End Sub


    Protected Sub btnAtender2_Click(sender As Object, e As EventArgs) Handles btnAtender2.Click
        Dim intIDCita As Integer
        intIDCita = CInt(lblNumCita.Text)
        Call AtenderCita(intIDCita)
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        Dim intIDCita As Integer
        intIDCita = CInt(lblNumCita.Text)
        Call AnularCita(intIDCita)
    End Sub
End Class