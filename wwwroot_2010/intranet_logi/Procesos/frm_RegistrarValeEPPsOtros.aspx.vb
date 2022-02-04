Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RegistrarValeEPPsOtros
    Inherits System.Web.UI.Page


    Dim strSeriePedido As String = "0005"
    Dim strNumeroPedido As String = ""
    Dim strCodigoArticulo As String = ""
    Dim strSecuencia As String = ""
    Dim lstrErrorDatosCab As String = ""
    Dim lstrErrorDatosDet As String = ""
    Dim lstrErrorCtaGas As String = ""
    Dim lstrErrorCTC As String = ""
    Dim lstrErrorActivo As String = ""
    Dim lstrErrorPsto As String = ""
    Dim lstrErrorItemDuplicado As String = ""
    Dim lstrErrorGeneraNumero As String = ""
    Dim lstrErrorValorCantidad As String = ""

    Dim strFlag As String = "0"
    Dim strCodCtaGasto As String = ""
    Dim strDesCtaGasto As String = ""

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@GRUPO_CODIGO") = "3000"
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DGAMARRA"

        '--INICIO: VERIFICAR LA SESION
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

    ' load
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Ajax.Utility.RegisterTypeForAjax(GetType(frm_RegistrarValeAlmacen))
        Ajax.Utility.RegisterTypeForAjax(GetType(frm_RegistrarValeEPPsOtros))
        If Not (Page.IsPostBack) Then
            prcLimpiaControlesCabecera()
            prcLimpiaControlesDetalle()
            prcHabilitaControlesDetalle(False)
            prcHabilitaDatosArticulo(False)
            prcHabilataBotonesAccion(True, True, False, False, False, False, False)
            prnNuevoPedido()

            'En el caso de Busqueda cargamos el pedido
            strNumeroPedido = txtCodigo.Text
            If strNumeroPedido.Length > 0 Then
                CargaPedido(strNumeroPedido)
            End If
        End If
        'CONNTROL DE GESTION - DG  -INI
        CargarMontoOt()
        'CONNTROL DE GESTION - DG  -FIN
        txtCodOrdenTrabajo.Attributes.Add("readonly", "readonly")
        btnAgregar.Attributes.Add("onClick", "javascript:return fnc_VerificarDatos();")
        btnAceptar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarAproba('" + txtNumeroPedido.Text + "');")
        btnBuscar.Attributes.Add("onClick", "javascript:return VerConsultaPedido();")
    End Sub

    'Manejo de Controles
#Region "Manejo de Controles"

    ' --- Limpia todos Controles de Cabecera
    Private Sub prcLimpiaControlesCabecera()
        txtFechaPedido.Text = ""
        txtFechaAprobacion.Text = ""
        txtFechaAtencion.Text = ""
        txtPstoInicial.Text = Strings.Format("{0,0.00}", 0)
        txtPstoUtilizado.Text = Strings.Format("{0,0.00}", 0)
        txtPstoDisponible.Text = Strings.Format("{0,0.00}", 0)
        txtEstado.Text = ""
        txtSeriePedido.Text = ""
        txtNumeroPedido.Text = ""
        txtCodSolicitante.Text = ""
        lblDesSolicitante.Text = ""
        txtCodAlmacen.Text = ""
        lblDesAlmacen.Text = ""
        txtCodCentroCostos.Text = ""
        lblDesCentroCostos.Text = ""
        txtCodResponsableOT.Text = ""
        lblDesResponsableOT.Text = ""
        'txtFecInstal.Text = ""
        txtObservaciones.Text = ""
        cboTipo.SelectedValue = 0
    End Sub

    ' --- Limpia Controles del Detalle
    Private Sub prcLimpiaControlesDetalle()
        txtCodArticulo.Text = ""
        lblDesArticulo.Text = ""
        lblUniMedida.Text = ""
        lblPrecioArticulo.Text = ""
        lblStockArticulo.Text = ""
        txtCodCuentaGastos.Text = ""
        lblDesCuentaGasto.Text = ""
        lblDesServicio.Text = ""
        txtCodOrdenTrabajo.Text = ""
        lblDesOrdenTrabajo.Text = ""
        txtCantidad.Text = Strings.Format("{0,0.00}", 0)
    End Sub

    ' --- Habilitra Controles Cabecera de Pedido
    Private Sub prcHabilitaControlesCabecera(ByVal lblnEstado As Boolean)
        txtSeriePedido.Enabled = False
        txtNumeroPedido.Enabled = False
        txtEstado.Enabled = False
        txtCodSolicitante.Enabled = lblnEstado
        txtCodAlmacen.Enabled = lblnEstado
        txtCodCentroCostos.Enabled = lblnEstado
        cboTipo.Enabled = lblnEstado
        txtCodResponsableOT.Enabled = lblnEstado
        'txtFecInstal.Enabled = lblnEstado
        'cboPrioridad.Enabled = lblnEstado
        txtObservaciones.Enabled = lblnEstado
    End Sub

    ' --- Habilitra Controles Detalle de Pedido
    Private Sub prcHabilitaControlesDetalle(ByVal lblnEstado As Boolean)
        lblItems.Visible = lblnEstado
        dgDetallePedido.Visible = lblnEstado
        lblMonto.Visible = lblnEstado
        lblTotalPedido.Visible = lblnEstado
    End Sub

    ' --- Habilita Controles de detalle del Pedido
    Private Sub prcHabilitaDatosArticulo(ByVal lblnEstado As Boolean)
        pnlArticulo.Visible = lblnEstado
        txtCodArticulo.Enabled = lblnEstado
        lblDesArticulo.Visible = lblnEstado
        lblUniMedida.Visible = lblnEstado
        lblStockArticulo.Visible = lblnEstado
        lblPrecioArticulo.Visible = lblnEstado
        txtCodCuentaGastos.Enabled = lblnEstado
        'txtCodOrdenServicio.Enabled = lblnEstado
        txtCantidad.Enabled = lblnEstado
        btnAgregar.Enabled = lblnEstado
    End Sub

    ' --- Habilita Botones de Accion
    Private Sub prcHabilataBotonesAccion(ByVal ActNuevo As Boolean, ByVal ActBuscar As Boolean, _
                                ByVal ActSolictud As Boolean, ByVal ActSeguimiento As Boolean, _
                                ByVal ActAnular As Boolean, ByVal ActAgregar As Boolean, ByVal ActAceptar As Boolean)
        btnNuevo.Enabled = ActNuevo
        btnBuscar.EnableViewState = ActBuscar
        btnAnular.Enabled = ActAnular
        btnAgregar.Enabled = ActAgregar
        btnAceptar.Enabled = ActAceptar
    End Sub

#End Region

    '  Botones de Acciones del Formulario
#Region "Botones"
    ' --- Boton: Nuevo Pedido
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        prnNuevoPedido()
    End Sub

    ' --- Boton: Agregar Item al Detalle de Pedido
    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregar.Click

        ' validamos cabecera de pedido
        If ValidaDatoscabeceraPedido.Length > 0 Then
            lblError.Text = lstrErrorDatosCab
            Exit Sub
        End If

        ' validamos detalle de pedido
        If ValidaDatosDetallePedido().Length > 0 Then
            lblError.Text = lstrErrorDatosDet
            Exit Sub
        End If

        ' validamos Articulos duplicados en Pedido
        If Trim(ValidaDuplicidadItem()).Length > 0 Then
            lblError.Text = lstrErrorItemDuplicado
            Exit Sub
        End If

        ' validamos activo que nesecita cuenta de gasto especifica
        'If ValidaActivoXCtaGasto().Length > 0 Then
        '    lblError.Text = lstrErrorCtaGas + lstrErrorCTC
        '    Exit Sub
        'End If

        ' validamos la existencia del activo
        'If ValidaActivoCTC().Length > 0 Then
        '    lblError.Text = lstrErrorCTC
        '    Exit Sub
        'End If

        ' validamos duplicidad del activo
        'If ValidaDuplicidadCTC().Length > 0 Then
        '    lblError.Text = lstrErrorCtaGas + lstrErrorCTC
        '    Exit Sub
        'End If


        strFlag = txtSituacion.Text
        Select Case strFlag
            Case "0"
                'Nuevo Pedido: Grabamos Cabecera y Detalle
                GuardarPedido()
                txtSituacion.Text = "1"
            Case "1"
                'Agregarmos Items al Pedido
                prcActulizaDetallePedido(strFlag, "")
                txtSituacion.Text = "1"
            Case "2"
                ' Actualizamos Items al Pedido
                ActualizaritemPedido()
                txtSituacion.Text = "1"
        End Select
        prcLimpiaControlesDetalle()
    End Sub

    ' Boton: Anulamos Pedido
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        prcAnulaPedido()
    End Sub

    ' Boton: Buscar Pedido
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        If txtCodigo.Text.Length > 0 Then
            CargaPedido(txtCodigo.Text)
        End If
    End Sub

#End Region

    ' Procedimientos
#Region "Procedimentos"
    ' --- Prodedimiento: Nuevo Pedido
    Public Sub prnNuevoPedido()
        txtSituacion.Text = "0"
        prcLimpiaControlesCabecera()
        prcLimpiaControlesDetalle()
        prcHabilitaControlesCabecera(True)
        prcHabilitaControlesDetalle(False)
        prcHabilitaDatosArticulo(True)
        prcHabilataBotonesAccion(False, False, False, False, False, True, False)

        prcValoresIniciales(False)
        lblDesCuentaGasto.Text = ""
        lblError.Text = ""
        'Conusltamos Usuario
        Dim lstrUsuario As String
        lstrUsuario = Session("@USUARIO")
        prcConsultarusuario(lstrUsuario)

    End Sub

    ' Colocamos valores iniciales para un Nuevo Pedido
    Private Sub prcValoresIniciales(ByVal lblnEstado As Boolean)
        strFlag = txtSituacion.Text
        If strFlag = "0" Then
            txtSeriePedido.Text = strSeriePedido
            txtFechaPedido.Text = Date.Today.ToShortDateString
            txtEstado.Text = NombreEstado("ACTIVO")
            txtPstoInicial.Text = Strings.Format("{0,0.00}", 0)
            txtPstoUtilizado.Text = Strings.Format("{0,0.00}", 0)
            txtPstoDisponible.Text = Strings.Format("{0,0.00}", 0)
            lblTotalPedido.Text = "0.00"
        End If
    End Sub

    ' --- Nombre de Estado de Pedido
    Private Function NombreEstado(ByVal strEstado As String) As String
        Dim strNomEstado As String = ""
        Select Case strEstado
            Case "POR APROBAR"
                strNomEstado = "POR APROBAR"
            Case "ACTIVO"
                strNomEstado = "ACTIVO"
            Case "APROBADO"
                strNomEstado = "APROBADO"
            Case "ANULADO"
                strNomEstado = "ANULADO"
            Case "ATENDIDO"
                strNomEstado = "ATENDIDO"
        End Select
        Return strNomEstado
    End Function

#End Region

    ' Acceso a datos
#Region "Acceso a datos"

    ' --- Consultamos Usuario
    Public Function prcConsultarusuario(ByVal strUsuario As String) As DataTable
        Dim ldtbUsuario As DataTable
        ldtbUsuario = Nothing
        Try
            Dim objParametros As Object() = {"COD_USU", strUsuario}
            ldtbUsuario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_PedidoConsultaUsuario", objParametros)
            If Not ldtbUsuario Is Nothing Then
                txtCodSolicitante.Text = ldtbUsuario.Rows(0).Item("co_trab")
                lblDesSolicitante.Text = ldtbUsuario.Rows(0).Item("Nombres")
            Else
                txtCodSolicitante.Text = ""
                lblDesSolicitante.Text = ""
                ldtbUsuario = Nothing
                lblError.Text = "Debe elegir un solicitante para el Vale"
            End If
        Catch ex As Exception
            lblError.Text = "Error: Consultar de usuario." + ex.Message
            ldtbUsuario = Nothing
        End Try
        Return ldtbUsuario
    End Function

    ' --- Consultamos Presupuesto Disponible
    Public Function fncConsultaPstoDisponible(ByVal strTipo As String) As Double
        Dim ldblPstoInicial As Double = 0
        Dim ldblPstoUtilizado As Double = 0
        Dim ldblPstoDisponible As Double = 0

        Dim lstrCentroCostos As String
        Dim lstrCuentaGastos As String
        Dim lstrAnno As String
        Dim lintMes As Integer
        Dim ldblTotalPedido As Double

        Dim objPedido As Logistica.clsPedidos
        Dim ldtbPsto As DataTable

        lstrCentroCostos = txtCodCentroCostos.Text
        If strNumeroPedido.Length > 0 Then
            lstrCuentaGastos = txtCodCuentaGastos.Text
            strTipo = "2"
        Else
            lstrCuentaGastos = txtCodCuentaGastos.Text
        End If

        lstrAnno = Mid(txtFechaPedido.Text, 7, 4)
        lintMes = Integer.Parse(Mid(txtFechaPedido.Text, 4, 2))
        ldblTotalPedido = CDbl(lblTotalPedido.Text)
        objPedido = New Logistica.clsPedidos
        Try
            ldtbPsto = New DataTable
            ldtbPsto = objPedido.fncConsultaPresupuesto(strTipo, lstrCentroCostos, lstrCuentaGastos, lstrAnno, lintMes, ldblTotalPedido)
            Dim i As Integer
            If Not ldtbPsto Is Nothing And ldtbPsto.Rows.Count > 0 Then
                ldblPstoInicial = ldtbPsto.Rows(0).Item("psto_inic")
                ldblPstoUtilizado = ldtbPsto.Rows(0).Item("psto_util")
                ldblPstoDisponible = ldtbPsto.Rows(0).Item("psto_disp")
            End If
            txtPstoInicial.Text = String.Format("{0:0.000}", ldblPstoInicial)
            txtPstoUtilizado.Text = String.Format("{0:0.000}", ldblPstoUtilizado)
            txtPstoDisponible.Text = String.Format("{0:0.000}", ldblPstoDisponible)
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error al consultar presupuesto disponible, comuiquese con Sistemas."
        End Try
        Return ldblPstoDisponible
    End Function

    ' --- Guardar Pedido
    Private Sub GuardarPedido()
        Dim i As Integer
        Dim intNumItems As Integer = 0
        Dim dblTotalPedido As Double = 0
        Dim objPedidos As New Logistica.clsPedidos

        Dim chrTipo As String = ""
        Dim strNumPedido As String = ""
        Dim strNumItem As String = ""
        Dim dblCantidad As Double = 0
        Dim strCodAuxiliar As String = ""
        Dim strCodDestino As String = ""
        'Dim strCodOrden As String = ""
        Dim strCodSolicitante As String = ""
        Dim strCodAlmacen As String = ""
        Dim strObservacion As String = ""
        Dim strCodUsuario As String = ""
        Dim strTipPedido As String = ""
        Dim strFecnstal As String = ""
        Dim TipoVale As String
        Dim ldtDetalle As New DataTable

        Dim strCodResponsableOT As String
        Dim strCodOrdenTrabajo As String

        chrTipo = "1"
        strNumPedido = fncGeneraNumeroPedido()
        strNumItem = Trim(txtCodArticulo.Text)
        dblCantidad = Double.Parse(Trim(txtCantidad.Text))
        strCodAuxiliar = Trim(txtCodCentroCostos.Text)
        strCodDestino = Trim(txtCodCuentaGastos.Text)
        strCodSolicitante = Trim(txtCodSolicitante.Text)
        strCodAlmacen = Trim(txtCodAlmacen.Text)
        strObservacion = Trim(txtObservaciones.Text)
        strCodUsuario = Trim(Session("@USUARIO"))
        TipoVale = cboTipo.SelectedValue.ToString()

        strCodResponsableOT = Trim(txtCodResponsableOT.Text)
        strCodOrdenTrabajo = Trim(txtCodOrdenTrabajo.Text)

        'strTipPedido = Trim(cboPrioridad.SelectedValue)
        'strFecnstal = Mid(txtFecInstal.Text, 7, 4) & Mid(txtFecInstal.Text, 4, 2) & Mid(txtFecInstal.Text, 1, 2)

        Try
            strFlag = txtSituacion.Text
            If strFlag = "0" Then
                ldtDetalle = objPedidos.fncGuardaPedidoEPPsOtros(chrTipo, strNumPedido, strNumItem, dblCantidad, _
                                    strCodAuxiliar, strCodDestino, strCodUsuario, _
                                    strCodAlmacen, strObservacion, strCodSolicitante, strTipPedido, strFecnstal, "", "", TipoVale, strCodOrdenTrabajo, strCodResponsableOT)
                If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                    intNumItems = ldtDetalle.Rows.Count
                    If intNumItems > 0 Then
                        dgDetallePedido.DataSource = ldtDetalle
                        dgDetallePedido.DataBind()
                        dgDetallePedido.Visible = True

                        'Calculamos Monto del Pedido
                        For i = 0 To intNumItems - 1
                            dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
                        Next

                        lblItems.Text = "Numero de Items:" + intNumItems.ToString
                        lblItems.Visible = True

                        lblMonto.Visible = True
                        lblTotalPedido.Text = dblTotalPedido.ToString()
                        lblTotalPedido.Visible = True

                        txtNumeroPedido.Text = Mid(strNumPedido, 6, 10)
                        lblError.Text = "Se Genero el Pedido: " + strNumPedido
                        lblError.Visible = True

                        prcHabilitaControlesCabecera(False)
                        prcLimpiaControlesDetalle()
                        prcHabilataBotonesAccion(True, True, True, False, True, True, True)

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al registrar el vale." + ex.Message
        End Try

    End Sub

    ' --- Funcion que genera un Nuevo Numero de Pedido
    Public Function fncGeneraNumeroPedido() As String
        Dim objPedido As Logistica.clsPedidos
        Dim strNumeroPedido As String
        Dim strSerie As String
        strSerie = strSeriePedido
        objPedido = New Logistica.clsPedidos
        Try
            strNumeroPedido = objPedido.fncGeneraNumeroPedido(strSerie)
        Catch ex As Exception
            strNumeroPedido = "Error: Generar numero de vale." + ex.Message
        End Try
        Return strNumeroPedido
    End Function

    ' --- Agrega/Modifica/Elimina el Item al detalle en la tabla Original
    Private Sub prcActulizaDetallePedido(ByVal chrTipo As String, ByVal strCodProducto As String)
        Dim i As Integer
        Dim intNumItems As Integer = 0
        Dim dblTotalPedido As Double = 0

        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtDetalle As New DataTable

        Dim strNumPedido As String = ""
        Dim strNumItem As String = ""
        Dim dblCantidad As Double = 0
        Dim strCodAuxiliar As String = ""
        Dim strCodDestino As String = ""
        Dim strCodOrden As String = ""
        Dim strCodUsuario As String = ""
        Dim strCodSolicitante As String = ""
        Dim strCodAlmacen As String = ""
        Dim strObs As String = ""
        Dim strTipPedido As String = ""
        Dim strFecInstal As String = ""

        ldtDetalle = Nothing

        strNumPedido = Trim(txtSeriePedido.Text) + "-" + Trim(txtNumeroPedido.Text)
        If Trim(strCodProducto.Length) > 0 Then
            strNumItem = strCodProducto
        Else
            strNumItem = Trim(txtCodArticulo.Text)
        End If
        dblCantidad = Double.Parse(Trim(txtCantidad.Text))
        strCodAuxiliar = Trim(txtCodCentroCostos.Text)

        strCodDestino = Trim(txtCodCuentaGastos.Text)
        strCodUsuario = Session("@USUARIO")
        strCodSolicitante = txtCodSolicitante.Text
        strCodAlmacen = Trim(txtCodAlmacen.Text)
        strObs = Trim(txtObservaciones.Text)

        'strTipPedido = cboPrioridad.SelectedValue
        'strFecInstal = Mid(txtFecInstal.Text, 7, 4) & Mid(txtFecInstal.Text, 4, 2) & Mid(txtFecInstal.Text, 1, 2)

        strCodOrdenTrabajo = Trim(txtCodOrdenTrabajo.Text)

        If Trim(txtAcepta.Text) = "" Or Trim(txtAcepta.Text).Length = 0 Then
            strSecuencia = "0"
        Else
            strSecuencia = Trim(txtAcepta.Text)
        End If

        ' Actualizamos en Pedido Existente
        Try
            ldtDetalle = objPedidos.prcRegistraDetallePedidoEPPsOtros(chrTipo, strNumPedido, strNumItem, _
                            dblCantidad, strCodAuxiliar, strCodDestino, strCodUsuario, _
                            strCodAlmacen, strObs, strCodSolicitante, strSecuencia, strTipPedido, strFecInstal, "", strCodOrdenTrabajo)


            If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                intNumItems = ldtDetalle.Rows.Count
                If intNumItems > 0 Then
                    dgDetallePedido.DataSource = ldtDetalle
                    dgDetallePedido.DataBind()
                    dgDetallePedido.Visible = True
                    lblItems.Text = "Numero de Items:" + intNumItems.ToString
                    lblItems.Visible = True
                    For i = 0 To intNumItems - 1
                        dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
                    Next
                    lblMonto.Visible = True
                    lblTotalPedido.Text = dblTotalPedido.ToString()
                    lblTotalPedido.Visible = True

                    prcLimpiaControlesDetalle()
                    prcHabilataBotonesAccion(True, True, True, False, True, True, True)
                    lblError.Text = ""
                    lblError.Visible = False
                End If
            Else
                intNumItems = 0
                dgDetallePedido.DataSource = Nothing
                dgDetallePedido.Visible = True
                lblItems.Text = "Numero de Items:" + intNumItems.ToString
                lblItems.Visible = True
                lblTotalPedido.Text = "0.00"
                lblMonto.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error al actualizar vale. " + ex.Message
        End Try
    End Sub

    ' --- Elimina un Registro del Pedido
    Private Sub prcEliminaRegistroPedido(ByVal strCodProducto As String)
        Dim stMensaje As String = ""
        Dim intNumItems As Integer = 0

        strFlag = txtSituacion.Text
        lblError.Text = ""
        lblError.Visible = False

        Try
            If strCodProducto.Length > 0 Then
                lblError.Text = "Se Elimino el Articulo: " + strCodProducto + " del Pedido"

                ' Actualza en el detalle
                prcActulizaDetallePedido(strFlag, strCodProducto)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(True)
                lblError.Visible = True
                txtSituacion.Text = "1"
            Else
                lblError.Text = "Error: Debe Elegir el Articulo a Eliminar de la Lista"
                lblError.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error: Eliminacion de articulo de la lista." + ex.Message
        End Try

    End Sub

    ' --- Actualiza el articulo
    Private Sub ActualizaritemPedido()
        Dim ldblNewCantidad As Double = 0
        Dim stMensaje As String = ""
        Dim strCodProducto As String = ""

        lblError.Text = ""
        strFlag = txtSituacion.Text
        ldblNewCantidad = Double.Parse(Trim(txtCantidad.Text))
        strCodProducto = Trim(txtCodArticulo.Text)

        If strCodProducto.Length > 0 Then
            If ldblNewCantidad > 0 Then
                prcActulizaDetallePedido(strFlag, "")
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(True)
                lblError.Text = "Se Guardaron los cambios en articulo: " + strCodProducto
            Else
                lblError.Text = "Error, debe ingresar una cantidad valida, mayor a cero"
            End If
        Else
            lblError.Text = "Error, debe Elegir un registro de la lista"
        End If
    End Sub

    ' --- Anular un registro del pedido
    Private Sub prcAnulaPedido()
        Dim objPedido As Logistica.clsPedidos
        Dim ldtbPsto As DataTable
        Dim strNumeroPedido As String
        Dim strUsuarioModi As String
        Dim stMensaje As String = ""

        lblError.Text = ""
        lblError.Visible = False

        strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
        strUsuarioModi = Session("@USUARIO")
        objPedido = New Logistica.clsPedidos

        lblError.Text = "El Pedido ha sido Anulado"
        Try
            ldtbPsto = New DataTable
            ldtbPsto = objPedido.fncPedidoCambiaEstado("1", strNumeroPedido, strUsuarioModi, Nothing)
            lblError.Visible = True
            Dim i As Integer
            If Not ldtbPsto Is Nothing And ldtbPsto.Rows.Count > 0 Then
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(True)
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, False, False, False, False)
                txtPstoInicial.Text = ldtbPsto.Rows(0).Item("psto_inic").ToString
                txtPstoUtilizado.Text = ldtbPsto.Rows(0).Item("psto_util").ToString
                txtPstoDisponible.Text = ldtbPsto.Rows(0).Item("psto_disp").ToString
            End If
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error al anular un vale comuiquese con Sistemas."
        End Try
    End Sub

#End Region

#Region "Validaciones para Ingreso"

    ' --- Validamos cabecera de pedido
    Public Function ValidaDatoscabeceraPedido() As String
        lstrErrorDatosCab = ""
        Try
            'If Trim(txtCodAlmacen.Text).Length = 0 Or (Trim(txtCodAlmacen.Text).Length > 0 And Trim(lblDesAlmacen.Text).Length = 0) Then
            If Trim(txtCodAlmacen.Text).Length = 0 Then
                lstrErrorDatosCab = "Error debe eligir un almacen."
                Return lstrErrorDatosCab
                Exit Function
            End If

            If txtCodResponsableOT.Text.Trim.Length = 0 Then
                lstrErrorDatosCab = "Error debe eligir un responsable de la orden de trabajo."
                Return lstrErrorDatosCab
                Exit Function
            End If

            'If Trim(txtCodCentroCostos.Text).Length = 0 Or (Trim(txtCodCentroCostos.Text).Length > 0 And Trim(lblDesCentroCostos.Text).Length = 0) Then
            If Trim(txtCodCentroCostos.Text).Length = 0 Then
                lstrErrorDatosCab = "Error debe eligir un centro de costos."
                Return lstrErrorDatosCab
                Exit Function
            End If

            If cboTipo.SelectedValue = 0 Then
                lstrErrorDatosCab = "Error debe seleccionar un Tipo"
                Return lstrErrorDatosCab
                Exit Function
            End If

            'If cboPrioridad.SelectedValue = "" Then
            '    lstrErrorDatosCab = "Error debe elegir una prioridad."
            '    Return lstrErrorDatosCab
            '    Exit Function
            'End If

            'If Trim(txtFecInstal.Text).Length = 0 Then
            '    lstrErrorDatosCab = "Error debe ingresar una fecha de instalacion."
            '    Return lstrErrorDatosCab
            '    Exit Function
            'End If

            'If Trim(txtFecInstal.Text).Length > 0 Then
            '    If IsDate(txtFecInstal.Text) = False Then
            '        lstrErrorDatosCab = "Error ingrese correctamente la fecha instalacion."
            '        Return lstrErrorDatosCab
            '        Exit Function
            '    End If
            'End If

        Catch ex As Exception
            lblError.Text = "Error: Validando datos de cabecera de pedido."
        End Try
        Return lstrErrorDatosCab
    End Function

    ' --- Validamos detalle de pedido
    Public Function ValidaDatosDetallePedido() As String
        Dim ldblPrecio As Double = 0
        lblError.Text = ""
        lstrErrorDatosDet = ""
        Try
            ' articulo
            If Trim(txtCodArticulo.Text).Length = 0 Then
                lstrErrorDatosDet = "Debe elegir un articulo."
                Return lstrErrorDatosDet
                Exit Function
            Else
                'consultamos precio
                ldblPrecio = fncConsultaPrecio()
                lblPrecioArticulo.Text = Strings.Format("{0,0.00}", ldblPrecio)
            End If

            ''CONNTROL DE GESTION - DG  -INI
            ' Orden de Trabajo
            'If Trim(txtCodOrdenTrabajo.Text).Length = 0 Then
            '    lstrErrorDatosDet = "Error debe eligir una Orden de Trabajo."
            '    Return lstrErrorDatosDet
            '    Exit Function
            'End If
            If Trim(txtCodCuentaGastos.Text) <> "" Then
                Dim strResult As String = ValidarCuentaGastoOT(Trim(txtCodCuentaGastos.Text))
                If strResult <> "" And txtCodOrdenTrabajo.Text = "" Then
                    lstrErrorDatosDet = "Debe ingresar una Orden de Trabajo, para el uso de esta cuenta de gastos."
                    Return lstrErrorDatosDet
                    Exit Function
                End If
            End If
            'CONNTROL DE GESTION - DG  -FIN
            

        Catch ex As Exception
            lblError.Text = "Error: Validando datos del detalle de pedido."
        End Try
        Return lstrErrorDatosDet
    End Function

    ' --- Validamos Articulos duplicados en el detalle del Pedido
    Private Function ValidaDuplicidadItem() As String
        lstrErrorItemDuplicado = ""
        If txtSituacion.Text = "1" Then
            Dim objPedidos As New Logistica.clsPedidos
            Dim ldtDuplicados As New DataTable
            Dim chrTipo As String = ""
            Dim strNumPedido As String = ""
            Dim strNumItem As String = ""
            Dim strCtagasto As String = ""
            Dim strActivo As String = ""

            ldtDuplicados = Nothing

            chrTipo = "1"
            strNumPedido = Trim(txtSeriePedido.Text) + "-" + Trim(txtNumeroPedido.Text)
            strNumItem = Trim(txtCodArticulo.Text)
            strCtagasto = Trim(txtCodCuentaGastos.Text)
            Try
                ldtDuplicados = objPedidos.fncValidaDuplicadosDetalle(chrTipo, strNumPedido, strNumItem, _
                                                        strCtagasto, strActivo)
                If Not ldtDuplicados Is Nothing And ldtDuplicados.Rows.Count > 0 Then
                    lstrErrorItemDuplicado = "El articulo a solicitar ya existe en el vale actual."
                End If
            Catch ex As Exception
                lblError.Text = "Error al Validar duplicidad del item en detalle." + ex.Message
            End Try
        End If
        Return lstrErrorItemDuplicado
    End Function

    ' --- Validamos Activos para cuentas de gasto especificas
    'Public Function ValidaActivoXCtaGasto() As String
    '    Dim objPedido As New Logistica.clsPedidos
    '    Dim strCtagasto As String = ""
    '    lstrErrorCtaGas = ""
    '    Return lstrErrorCtaGas
    'End Function

    ' --- Validamos existencia de ActivoCTC
    'Public Function ValidaActivoCTC() As String
    '    Dim objPedido As New Logistica.clsPedidos
    '    Dim strActivo As String
    '    lstrErrorActivo = ""
    '    'strActivo = Trim(txtCodOrdenServicio.Text)
    '    If strActivo.Length > 0 Then
    '        Try
    '            If objPedido.fnc_VerificaActivoCTC(strActivo) = True Then
    '                lstrErrorActivo = "Error: Debe elegir un activo/ctc valido."
    '            End If
    '        Catch ex As Exception
    '            lstrErrorActivo = "Error al Validar activo/ctc." + ex.ToString
    '        End Try
    '    End If
    '    Return lstrErrorActivo
    'End Function

    ' --- Validacion de CTC duplicado
    'Private Function ValidaDuplicidadCTC() As String
    '    lstrErrorCTC = ""
    '    If txtSituacion.Text = "1" Then
    '        Dim objPedidos As New Logistica.clsPedidos
    '        Dim ldtDuplicadosCTC As New DataTable
    '        Dim strNumPedido As String = ""
    '        Dim strActivo As String = ""

    '        strNumPedido = Trim(txtSeriePedido.Text + "-" + txtNumeroPedido.Text)
    '        'strActivo = Trim(txtCodOrdenServicio.Text)
    '        Try
    '            'If rdbCTC.Checked = True And strActivo.Length > 0 Then
    '            '    ldtDuplicadosCTC = objPedidos.fncValidaDuplicadoCTC(strNumPedido, strActivo)
    '            '    If ldtDuplicadosCTC.Rows(0).Item("Validacion").ToString = "1" Then
    '            '        lstrErrorCTC = "No debe existir mas de un CTC en el vale actual."
    '            '    End If
    '            'End If
    '        Catch ex As Exception
    '            lblError.Text = "Error al Validar duplicidad de ctc. " + ex.Message
    '        End Try
    '    End If
    '    Return lstrErrorCTC
    'End Function

    ' --- Validamos Presupuesto para la Cuenta de Gastos
    Private Function ValidaPresupuesto() As String
        Dim ldblPstoDisponible As Double = 0
        Dim ldblSubTotalPedido As Double = 0
        Dim ldblPrecioItem As Double = 0
        Dim strCodItems As String = ""

        lstrErrorPsto = ""
        ldblPstoDisponible = Double.Parse(txtPstoDisponible.Text)
        If lblPrecioArticulo.Text = "" Or Len(Trim(lblPrecioArticulo.Text)) = 0 Then
            lblPrecioArticulo.Text = "0"
        End If
        ldblPrecioItem = Double.Parse(lblPrecioArticulo.Text)
        ldblSubTotalPedido = Double.Parse(txtCantidad.Text) * CDbl(ldblPrecioItem)

        If ldblPrecioItem > 0 Then
            If ldblSubTotalPedido > ldblPstoDisponible Then
                lstrErrorPsto = "El Monto del Pedido Excede el Presupuesto Disponible"
            End If
        End If
        Return lstrErrorPsto
    End Function

    ' --- Validamos Precio de Producto
    Public Function fncConsultaPrecio() As Double
        Dim objPedido As Logistica.clsPedidos
        Dim ldblPrecioItem As Double = 0
        Dim strCodItem As String

        strCodItem = Trim(txtCodArticulo.Text)
        objPedido = New Logistica.clsPedidos
        ldblPrecioItem = objPedido.fncConsultaPrecioItem(strCodItem)
        Return ldblPrecioItem
    End Function

    ' --- Validamos Stock del Articulo
    Private Function ValidaStockArticulo() As String
        lstrErrorValorCantidad = ""
        Dim dblCantidad As Double = 0
        Dim dblStock As Double = 0

        If Double.TryParse(Trim(txtCantidad.Text), 0) > 0 Then
            dblCantidad = Double.Parse(txtCantidad.Text)
            dblStock = CDbl(Trim(Mid(lblStockArticulo.Text, 6, 10)))
            If dblCantidad > dblStock Then
                lstrErrorValorCantidad = "No Existe Stock en Almacen para el Articulo Solicitado"
            End If
        End If
        Return lstrErrorValorCantidad
    End Function


#End Region

    ' Manejo de la Grilla
#Region "Grilla"

    Private Sub dgDetallePedido_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetallePedido.ItemCommand
        lblError.Text = ""
        lblError.Visible = False

        Dim stMensaje As String = ""
        Dim lobjSecuencia As Label = CType(e.Item.FindControl("lblSecuencia"), Label)
        Dim lobjCodigo As Label = CType(e.Item.FindControl("lblCodigo"), Label)
        Dim lobjCantidad As Label = CType(e.Item.FindControl("lblCantidad"), Label)
        Dim lobjDescripcion As Label = CType(e.Item.FindControl("lblDescripcion"), Label)
        Dim lobjUnidadMedidad As Label = CType(e.Item.FindControl("lblUnidaMedida"), Label)
        Dim lobjPrecio As Label = CType(e.Item.FindControl("lblPrecio"), Label)

        Dim lobjActivo As Label = CType(e.Item.FindControl("lblActivoFijo"), Label)
        'Dim lobjDesActivo As Label = CType(e.Item.FindControl("lblDesActivo"), Label)
        Dim lobjCodCtaGasto As Label = CType(e.Item.FindControl("lblCtagasto"), Label)
        Dim lobjDesCtaGasto As Label = CType(e.Item.FindControl("lblDesCtaGasto"), Label)

        Dim lobjOT As Label = CType(e.Item.FindControl("lblOrdenTrabajo"), Label)
        Dim lobDescOT As Label = CType(e.Item.FindControl("lblDesOrdenTrabajo"), Label)

        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtCuentaGasto As New DataTable
        Dim lobjBotonEliminar As ImageButton = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)
        If txtEstado.Text = "ACTIVO" Then
            Select Case e.CommandName
                Case "Editar"
                    Dim i As Integer = 0
                    txtSituacion.Text = "2"
                    txtCodArticulo.Text = lobjCodigo.Text
                    txtCantidad.Text = Strings.Format("{0,0.000}", lobjCantidad.Text)
                    'txtCodOrdenServicio.Text = Trim(lobjActivo.Text)
                    'lblDesServicio.Text = Trim(lobjDesActivo.Text)
                    lblDesArticulo.Text = "Desc: " + lobjDescripcion.Text
                    lblUniMedida.Text = "U.M.: " + lobjUnidadMedidad.Text
                    lblPrecioArticulo.Text = "Prec. " + lobjPrecio.Text

                    txtCodOrdenTrabajo.Text = lobjOT.Text
                    lblDesOrdenTrabajo.Text = lobDescOT.Text

                    txtCodCuentaGastos.Text = lobjCodCtaGasto.Text
                    'txtCodCuentaGastos.Text = ldtCuentaGasto.Rows(0).Item("co_acti_fijo").ToString()
                    lblDesCuentaGasto.Text = lobjDesCtaGasto.Text
                    txtAcepta.Text = lobjSecuencia.Text
                    prcHabilitaDatosArticulo(True)
                    txtCodArticulo.Enabled = False
                Case "Eliminar"
                    txtSituacion.Text = "3"
                    strCodCtaGasto = lobjCodCtaGasto.Text
                    'txtCodOrdenServicio.Text = Trim(lobjActivo.Text)
                    txtAcepta.Text = lobjSecuencia.Text
                    prcEliminaRegistroPedido(Trim(lobjCodigo.Text))
            End Select
        Else
            lblError.Text = "Error: Verifique la Situacion, No es posible Modificar el Pedido"
            lblError.Visible = True
        End If
    End Sub
#End Region

    ' Carga de Pedidos existentes.
#Region "Carga de Pedidos"
    ' --- Metodo: Para Cargar el Pedido
    Private Sub CargaPedido(ByVal NumPedido As String)
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtbPedido As New DataTable
        Dim ldtDetalle As New DataTable

        Dim strSerie As String = ""
        Dim intCodPedido As Integer = 0
        Dim strTipo As String = "0"
        Dim strFechaIni As String = ""
        Dim strFechaFin As String = ""
        Dim strSolicitante As String = ""
        Dim strEstado As String = ""
        Dim dblTotalPedido As Double = 0
        Dim i As Integer = 0
        Dim intNumItems As Integer = 0

        strSerie = strSeriePedido
        intCodPedido = Integer.Parse(Mid(NumPedido, 6, 10))
        Try
            ' -- Consultamos Cabecera de Pedidos
            dtbPedido = objPedidos.fncConsultaPedidosEPPsOtros(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
            If Not objPedidos Is Nothing Then

                ' -- Habilitamos controles segun estado
                ManejoEstados(dtbPedido.Rows(0).Item("ti_situ"))

                txtFechaPedido.Text = dtbPedido.Rows(0).Item("fe_pedi")
                txtFechaAprobacion.Text = dtbPedido.Rows(0).Item("fe_apro")
                txtFechaAtencion.Text = dtbPedido.Rows(0).Item("fe_aten")
                txtEstado.Text = NombreEstado(dtbPedido.Rows(0).Item("ti_situ"))
                txtSeriePedido.Text = Mid(dtbPedido.Rows(0).Item("nu_pedi"), 1, 4)
                txtNumeroPedido.Text = Mid(dtbPedido.Rows(0).Item("nu_pedi"), 6, 10)
                txtCodSolicitante.Text = dtbPedido.Rows(0).Item("CodSolicitante")
                lblDesSolicitante.Text = dtbPedido.Rows(0).Item("NomSolicitante")
                txtCodAlmacen.Text = dtbPedido.Rows(0).Item("co_alma")
                lblDesAlmacen.Text = dtbPedido.Rows(0).Item("de_alma")
                txtCodCentroCostos.Text = dtbPedido.Rows(0).Item("CodCentroCostos")
                lblDesCentroCostos.Text = dtbPedido.Rows(0).Item("DesCentroCostos")
                'cboPrioridad.SelectedValue = dtbPedido.Rows(0).Item("ti_pedido")
                'txtFecInstal.Text = dtbPedido.Rows(0).Item("fe_instal")
                txtObservaciones.Text = dtbPedido.Rows(0).Item("de_obse")
                cboTipo.SelectedValue = dtbPedido.Rows(0).Item("ti_epot")
                txtCodResponsableOT.Text = dtbPedido.Rows(0).Item("vch_CodResponsable")
                lblDesResponsableOT.Text = dtbPedido.Rows(0).Item("vch_DesResponsable")
                txtCodResponsableOT.Enabled = False
                ' -- Consultamos Detalle de Pedidos
                ldtDetalle = New DataTable
                ldtDetalle = objPedidos.fncConsultaDetallePedido("1", strSerie, intCodPedido)
                dgDetallePedido.DataSource = ldtDetalle
                dgDetallePedido.DataBind()
                dgDetallePedido.Visible = True
                intNumItems = ldtDetalle.Rows.Count.ToString
                lblItems.Text = "Numero de Items: " + ldtDetalle.Rows.Count.ToString
                lblItems.Visible = True
                For i = 0 To intNumItems - 1
                    dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
                Next
                lblMonto.Visible = True
                lblTotalPedido.Text = dblTotalPedido.ToString()
                lblTotalPedido.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error al consultar datos del vale." + ex.Message
        End Try
    End Sub

    ' --- Manejo de Estados
    Private Sub ManejoEstados(ByVal strEstado As String)
        Select Case strEstado
            Case "ACTIVO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(True)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(True)
                prcHabilataBotonesAccion(True, True, True, False, True, True, True)
                txtSeriePedido.Enabled = False
                txtNumeroPedido.Enabled = False
                cboTipo.Enabled = False
                txtSituacion.Text = "1"
                lblError.Text = "Pedido se encuentra activo."
            Case "POR APROBAR"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, True, False, True)
                txtSeriePedido.Enabled = False
                txtNumeroPedido.Enabled = False
                txtCodCuentaGastos.Enabled = False
                txtCodAlmacen.Enabled = False
                cboTipo.Enabled = False
                txtSituacion.Text = "6"
                lblError.Text = "Se ha enviado una Solicitud de aprobacion para este pedido, no puede ser modificado."
            Case "APROBADO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, False, False, False)
                txtSituacion.Text = "7"
                cboTipo.Enabled = False
                lblError.Text = "Este pedido ya ha sido aprobado, no puede ser modificado."
            Case "ANULADO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, False, False, False, False)
                txtSituacion.Text = "8"
                cboTipo.Enabled = False
                lblError.Text = "Este pedido esta anulado, no puede ser modificado."
            Case "ATENDIDO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, False, False, False)
                cboTipo.Enabled = False
                txtSituacion.Text = "9"
                lblError.Text = "Este pedido ha sido atendido, no puede ser modificado."
        End Select
    End Sub
#End Region

    ' Envio de Correo Electronico
    '#Region "Envio de Email"
    '    Private Sub EnviarEmail(ByRef pdtCorreos As DataTable)
    '        Dim i As Integer
    '        Dim lstrCuerpoMensaje As String = ""
    '        Dim lstrTitulo As String
    '        Dim lstrPara As String = ""
    '        Dim lstrCopia As String = ""
    '        strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
    '        Try
    '            For i = 0 To pdtCorreos.Rows.Count - 1
    '                If lstrPara.Trim.Length = 0 Then
    '                    lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
    '                Else
    '                    lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
    '                End If
    '                lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
    '            Next i
    '            If lstrCopia.Trim.Length = 0 Then
    '                lstrCopia = ""
    '            Else
    '                lstrCopia = lstrCopia
    '            End If
    '            With pdtCorreos.Rows(0)
    '                lstrTitulo = "[Intranet] VALE DE ALMACEN POR APROBAR."
    '                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
    '                                    "APROBACION PARA EL VALE DE ALMACEN &nbsp;: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
    '                                    "<STRONG>" & strNumeroPedido & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
    '                                    "</STRONG></FONT></FONT></P>" + _
    '                                    "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
    '                                    "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
    '                                    "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
    '                                    "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
    '                                    "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
    '                                    "Por favor no responder este correo.<BR>" + _
    '                                    "Departamento de Sistemas<BR>" + _
    '                                    "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
    '                                    "-------------------------------------------------------------------------------</P>"
    '                Dim mailMsg As System.Net.Mail.MailMessage
    '                mailMsg = New System.Net.Mail.MailMessage()

    '                'Configurar arreglo para el TO
    '                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
    '                For lintIndice = 0 To lstrTo_arreglo.Length - 1
    '                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
    '                Next

    '                'Si no hay destinatario que lo envie a sistemas
    '                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

    '                'Configurar arreglo para el CC
    '                Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
    '                For lintIndice = 0 To lstrCC_arreglo.Length - 1
    '                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
    '                Next

    '                With mailMsg
    '                    .From = New System.Net.Mail.MailAddress("VALE DE ALMACEN POR APROBAR<aprobaciones@nuevomundosa.com>")
    '                    .Subject = lstrTitulo
    '                    .Body = lstrCuerpoMensaje
    '                    .Priority = System.Net.Mail.MailPriority.High
    '                    .IsBodyHtml = True
    '                End With

    '                Dim Servidor As New System.Net.Mail.SmtpClient
    '                Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
    '                Servidor.Send(mailMsg)
    '                Servidor = Nothing


    '                Dim strDestinatarios As String
    '                strDestinatarios = "Se Comunico via email a: " + lstrPara + " Con Copia a: " + lstrCopia
    '                lblError.Text = strDestinatarios
    '                lblError.Visible = True
    '            End With
    '        Catch ex As Exception
    '            lblError.Text = ""
    '            lblError.Visible = False
    '            lblError.Text = "Ha ocurrido un error al enviar email, comuniquese con Sistemas."
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
    '        End Try
    '    End Sub
    '#End Region
    'CONNTROL DE GESTION - DG  -INI
    Protected Sub CargarMontoOt()
        If txtCodOrdenTrabajo.Text <> "" Then
            Dim objPedido As Logistica.clsPedidos
            objPedido = New Logistica.clsPedidos
            Dim strMonto As String
            strMonto = objPedido.fnc_ConsultaMontoOT(Year(Now), txtCodOrdenTrabajo.Text)
            txtMonto.Text = strMonto
        End If
    End Sub
    'CONNTROL DE GESTION - DG  -FIN
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        prcAprobarPedido()
    End Sub
    ' --- Aprobar un registro del pedido
    Private Sub prcAprobarPedido()
        Dim objPedido As Logistica.clsPedidos
        Dim ldtbPsto As DataTable
        Dim strNumeroPedido As String
        Dim strUsuarioModi As String
        Dim stMensaje As String = ""

        lblError.Text = ""
        lblError.Visible = False

        strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
        strUsuarioModi = Session("@USUARIO")
        objPedido = New Logistica.clsPedidos

        'lblError.Text = "El Pedido ha sido Aprobado"
        Try
            ldtbPsto = New DataTable
            ldtbPsto = objPedido.fncPedidoCambiaEstadoEPPsOtros(strNumeroPedido, strUsuarioModi)
            lblError.Visible = True
            Dim i As Integer
            If Not ldtbPsto Is Nothing And ldtbPsto.Rows.Count > 0 Then
                'prcHabilitaControlesCabecera(False)
                'prcHabilitaControlesDetalle(True)
                'prcHabilitaDatosArticulo(False)
                'prcHabilataBotonesAccion(True, True, False, False, False, False)
                If ldtbPsto.Rows(0).Item("TI_SITU").ToString = "APR" Then
                    lblError.Text = "El Pedido ha sido Aprobado, listo para despachar"
                End If

                prcHabilitaControlesCabecera(False)
                prcLimpiaControlesDetalle()
                prcHabilataBotonesAccion(True, True, True, False, True, True, True)
            End If
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error al Aprobar un vale comuiquese con Sistemas."
        End Try
    End Sub
    'CONNTROL DE GESTION - DG  -INI
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function ValidarCuentaGastoOT(ByVal strCuentaGasto As String) As String
        Dim strResult As String = ""

        Dim objPedido As Logistica.clsPedidos
        objPedido = New Logistica.clsPedidos

        strResult = objPedido.ValidarCuentaGastoOT(strCuentaGasto)

        Return strResult
    End Function
    'CONNTROL DE GESTION - DG  -FIN
End Class