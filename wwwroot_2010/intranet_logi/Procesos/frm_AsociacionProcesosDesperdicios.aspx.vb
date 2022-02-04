Imports NM_General

Public Class frm_AsociacionProcesosDesperdicios
    Inherits System.Web.UI.Page

    Private Sub frm_AsociacionProcesosDesperdicios_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        ' Session("@USUARIO") = "LALANOCA"

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
            Call InicializaControles()
            Call CambiarPanel("LOAD")            
        End If

    End Sub


#Region "Metodos"

    Private Sub InicializaControles()
        txtCodigoArticulo.Attributes.Add("readonly", "readonly")
        txtDescripcionArticulo.Attributes.Add("readonly", "readonly")
        txtCentroCostoTejeduria.Attributes.Add("readonly", "readonly")
        txtDescripcionCentroCosto.Attributes.Add("readonly", "readonly")
        txtProcesoOrigen.Attributes.Add("readonly", "readonly")
        txtDescripcionProceso.Attributes.Add("readonly", "readonly")

        btnGrabar.Attributes.Add("onclick", "javascript:return fnc_ValidaFormulario();")
    End Sub

    Private Sub CargarListadoAsociacionProcesos()

        Dim objLogistica As New NM_Logistica
        Dim dtListado As DataTable


        Try
            dtListado = objLogistica.ufn_Listado_Procesos_Desperdicios_Teje()
            grvAsociacionProcesos.DataSource = dtListado
            grvAsociacionProcesos.DataBind()

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objLogistica = Nothing
            dtListado = Nothing
        End Try

        
    End Sub

    Private Sub CambiarPanel(ByVal strTipoPanel As String)

        Call LimpiarControles()

        If strTipoPanel = "LOAD" Then
            pnlListadoProcesos.Visible = True
            pnlRegistroProceso.Visible = False
            Call CargarListadoAsociacionProcesos()
        ElseIf strTipoPanel = "NEW" Then
            lblTituloDetalle.Text = "Registro"
            pnlListadoProcesos.Visible = False
            pnlRegistroProceso.Visible = True
        ElseIf strTipoPanel = "UPD" Then
            lblTituloDetalle.Text = "Modificación"
            pnlListadoProcesos.Visible = False
            pnlRegistroProceso.Visible = True
        End If
    End Sub

    Private Sub LimpiarControles()

        txtCodigoArticulo.Text = ""
        txtDescripcionArticulo.Text = ""
        txtCentroCostoTejeduria.Text = ""
        txtDescripcionCentroCosto.Text = ""
        txtProcesoOrigen.Text = ""
        txtDescripcionProceso.Text = ""

        grvAsociacionProcesos.DataSource = Nothing
        grvAsociacionProcesos.DataBind()

        lblMsg.Text = ""
        lblMensajeIngreso.Text = ""
    End Sub


    Private Sub EliminarAsociacionProcesosDesperdicios(ByVal CODIGO_ARTICULO As String, ByVal CODIGO_CENTROCOSTO As String, ByVal CODIGO_PROCESOORIGEN As String)
        Dim objLogistica As New NM_Logistica
        Dim intResultado As Integer
        Dim strUsuario As String

        Try
            strUsuario = Session("@USUARIO")
            intResultado = objLogistica.ufn_EliminarAsociacionProcesoTejeduria(CODIGO_ARTICULO, CODIGO_CENTROCOSTO, CODIGO_PROCESOORIGEN, strUsuario)

            If intResultado > 0 Then
                lblMsg.Text = "Se eliminó la asociación satisfactoriamente."
            End If

            Call CargarListadoAsociacionProcesos()

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub ModificarAsociacionProcesosDesperdicios(ByVal CODIGO_ARTICULO As String, ByVal DESCRIP_ART As String,
                                                        ByVal CODIGO_CENTROCOSTO As String, ByVal DESCRIP_CC As String,
                                                        ByVal CODIGO_PROCESOORIGEN As String, ByVal DESCRIP_PROCESOORIGEN As String)
        hdnOpcion.Value = "UPD"
        Call CambiarPanel(hdnOpcion.Value)
        txtCodigoArticulo.Text = CODIGO_ARTICULO
        txtCentroCostoTejeduria.Text = CODIGO_CENTROCOSTO
        txtProcesoOrigen.Text = CODIGO_PROCESOORIGEN

        hdnCodigoArticuloORI.Value = CODIGO_ARTICULO
        hdnCentroCostoTejeduriaORI.Value = CODIGO_CENTROCOSTO
        hdnProcesoOrigenORI.Value = CODIGO_PROCESOORIGEN

        txtDescripcionArticulo.Text = DESCRIP_ART
        txtDescripcionCentroCosto.Text = DESCRIP_CC
        txtDescripcionProceso.Text = DESCRIP_PROCESOORIGEN

    End Sub



#End Region

#Region "Eventos"
    Protected Sub btnNuevoProcesoD_Click(sender As Object, e As EventArgs) Handles btnNuevoProcesoD.Click
        hdnOpcion.Value = "NEW"
        Call CambiarPanel(hdnOpcion.Value)
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Call CambiarPanel("LOAD")
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Dim strArticuloDesperdicio As String
        Dim strCentroCostoTeje As String
        Dim strProcesoOrigen As String
        Dim strArticuloDesperdicioORI As String
        Dim strCentroCostoTejeORI As String
        Dim strProcesoOrigenORI As String
        Dim strOpcion As String
        Dim strUsuario As String
        Dim objLogistica As New NM_Logistica
        Dim intResultado As Integer


        Try
            strArticuloDesperdicio = txtCodigoArticulo.Text
            strCentroCostoTeje = txtCentroCostoTejeduria.Text
            strProcesoOrigen = txtProcesoOrigen.Text

            strArticuloDesperdicioORI = hdnCodigoArticuloORI.Value
            strCentroCostoTejeORI = hdnCentroCostoTejeduriaORI.Value
            strProcesoOrigenORI = hdnProcesoOrigenORI.Value
            strOpcion = hdnOpcion.Value
            strUsuario = Session("@USUARIO")

            If strArticuloDesperdicio = "" Then
                txtCodigoArticulo.Focus()
                Throw New Exception("Debe elegir un Articulo de Desperdicio.")
            End If
            If strCentroCostoTeje = "" Then
                txtCentroCostoTejeduria.Focus()
                Throw New Exception("Debe elegir un Centro de Costo de Tejeduria.")
            End If
            If strProcesoOrigen = "" Then
                txtProcesoOrigen.Focus()
                Throw New Exception("Debe elegir un Proceso Origen.")
            End If

            intResultado = objLogistica.ufn_RegistrarAsociacionProcesoTejeduria(strArticuloDesperdicio,
                                                                                strCentroCostoTeje,
                                                                                strProcesoOrigen,
                                                                                strArticuloDesperdicioORI,
                                                                                strCentroCostoTejeORI,
                                                                                strProcesoOrigenORI,
                                                                                strOpcion,
                                                                                strUsuario)

            If intResultado > 0 Then
                lblMsg.Text = "Se grabaron los datos satisfactoriamente."
            End If

            Call CambiarPanel("LOAD")

        Catch ex As Exception
            lblMensajeIngreso.Text = ex.Message
        Finally
            objLogistica = Nothing
        End Try

    End Sub

    Private Sub grvAsociacionProcesos_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvAsociacionProcesos.RowCommand
        Dim CODIGO_ARTICULO As String
        Dim CODIGO_CENTROCOSTO As String
        Dim CODIGO_PROCESOORIGEN As String

        Dim gvr As GridViewRow

        Try
            gvr = CType(e.CommandSource, ImageButton).NamingContainer
            CODIGO_ARTICULO = grvAsociacionProcesos.DataKeys(gvr.RowIndex)("CODIGO_ARTICULO")
            CODIGO_CENTROCOSTO = grvAsociacionProcesos.DataKeys(gvr.RowIndex)("CODIGO_CENTROCOSTO")
            CODIGO_PROCESOORIGEN = grvAsociacionProcesos.DataKeys(gvr.RowIndex)("CODIGO_PROCESOORIGEN")

            If e.CommandName = "SELECCIONAR" Then
                lblDescripcionArt = grvAsociacionProcesos.Rows(gvr.RowIndex).FindControl("lblDescripcionArt")
                lblDescripcionCC = grvAsociacionProcesos.Rows(gvr.RowIndex).FindControl("lblDescripcionCC")
                lblDescripcionProc = grvAsociacionProcesos.Rows(gvr.RowIndex).FindControl("lblDescripcionProc")
                Call ModificarAsociacionProcesosDesperdicios(CODIGO_ARTICULO, lblDescripcionArt.Text, CODIGO_CENTROCOSTO, lblDescripcionCC.Text, CODIGO_PROCESOORIGEN, lblDescripcionProc.Text)

            End If

            If e.CommandName = "ELIMINAR" Then
                Call EliminarAsociacionProcesosDesperdicios(CODIGO_ARTICULO, CODIGO_CENTROCOSTO, CODIGO_PROCESOORIGEN)

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub grvAsociacionProcesos_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAsociacionProcesos.RowCreated
        Dim btnSelect As ImageButton
        Dim btnModificar As ImageButton
        btnSelect = e.Row.Cells(0).FindControl("btnSelect")
        If Not btnSelect Is Nothing Then
            ScriptManager1.RegisterAsyncPostBackControl(btnSelect)
        End If

        btnModificar = e.Row.Cells(0).FindControl("btnModificar")
        If Not btnModificar Is Nothing Then
            ScriptManager1.RegisterAsyncPostBackControl(btnModificar)
        End If
    End Sub


#End Region

End Class