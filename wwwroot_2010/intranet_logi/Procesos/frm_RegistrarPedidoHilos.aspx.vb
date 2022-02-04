Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RegistrarPedidoHilos
    Inherits System.Web.UI.Page
    Dim strSeriePedido As String = "0004"
    Dim strCodigoAlmacen As String = ""
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

    Private Sub frm_RegistrarPedidoHilos_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@GRUPO_CODIGO") = "3000"
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "AAMPUERP"

        '--INICIO: VERIFICAR LA SESION
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@USUARIO") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            prnNuevoPedido()
        End If

        btnSolicitaAprobacion.Attributes.Add("onClick", "javascript:SolicitarAprobacion();")
        txtNumeroPedido.Attributes.Add("onBlur", "FormatearBusqDoc(2);")

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        'En el caso de Busqueda cargamos el pedido
        strNumeroPedido = txtNumeroPedido.Text
        If strNumeroPedido.Length > 0 Then
            CargaPedido(strNumeroPedido)
        Else
            lblError.Text = "Error: Debe ingresar un numero de pedido a consultar."
        End If
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAgregar.Click
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

        ' validamos la existencia del activo
        If ValidaActivoCTC().Length > 0 Then
            lblError.Text = lstrErrorCTC
            Exit Sub
        End If

        strFlag = txtSituacion.Text
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
                'Actualizamos Items al Pedido
                ActualizaritemPedido()
                txtSituacion.Text = "1"
        End Select

        prcLimpiaControlesDetalle()
    End Sub


#Region "Metodos"
    ' --- Limpia todos Controles de Cabecera
    Private Sub prcLimpiaControlesCabecera()
        txtFechaPedido.Text = ""
        txtFechaAprobacion.Text = ""
        txtFechaAtencion.Text = ""
        txtEstado.Text = ""
        txtSeriePedido.Text = ""
        txtNumeroPedido.Text = ""
        txtCodSolicitante.Text = ""
        lblDesSolicitante.Text = ""
        txtCodAlmacen.Text = ""
        lblDesAlmacen.Text = ""
        'txtCodCentroCostos.Text = ""
        lblDesCentroCostos.Text = ""
        txtFecInstal.Text = ""
        txtObservaciones.Text = ""
    End Sub

    ' --- Limpia Controles del Detalle
    Private Sub prcLimpiaControlesDetalle()
        txtCodArticulo.Text = ""
        lblDesArticulo.Text = ""
        lblUniMedida.Text = ""
        lblTitulo.Text = ""
        lblPeso.Text = ""
        lblStock.Text = ""
        txtCodOrdenServicio.Text = ""
        lblDesServicio.Text = ""
        txtObsDet.Text = ""
        txtCanConos.Text = Strings.Format("{0,0.00}", 0)
    End Sub

    ' --- Habilitra Controles Cabecera de Pedido
    Private Sub prcHabilitaControlesCabecera(ByVal lblnEstado As Boolean)
        txtSeriePedido.Enabled = False
        'txtNumeroPedido.Enabled = False
        txtEstado.Enabled = False
        txtCodSolicitante.Enabled = lblnEstado
        txtCodAlmacen.Enabled = lblnEstado
        'CodCentroCostos.Enabled = lblnEstado
        txtFecInstal.Enabled = lblnEstado
        cboPrioridad.Enabled = lblnEstado
        ddlTurno.Enabled = lblnEstado
        txtObservaciones.Enabled = lblnEstado
        ddlLugarEntrega.Enabled = lblnEstado
    End Sub

    ' --- Habilitra Controles Detalle de Pedido
    Private Sub prcHabilitaControlesDetalle(ByVal lblnEstado As Boolean)
        lblItems.Visible = lblnEstado
        dgDetallePedido.Visible = lblnEstado
    End Sub

    ' --- Habilita Controles de detalle del Pedido
    Private Sub prcHabilitaDatosArticulo(ByVal lblnEstado As Boolean)
        pnlArticulo.Visible = lblnEstado
        txtCodArticulo.Enabled = lblnEstado
        lblDesArticulo.Visible = lblnEstado
        lblUniMedida.Visible = lblnEstado
        lblTitulo.Visible = lblnEstado
        lblPeso.Visible = lblnEstado
        txtCodOrdenServicio.Enabled = lblnEstado
        txtCanConos.Enabled = lblnEstado
        btnAgregar.Enabled = lblnEstado
    End Sub

    ' --- Habilita Botones de Accion
    Private Sub prcHabilataBotonesAccion(ByVal ActNuevo As Boolean, ByVal ActBuscar As Boolean, _
                                ByVal ActSolicitud As Boolean, ByVal ActVisualizar As Boolean, _
                                ByVal ActAnular As Boolean, ByVal ActAgregar As Boolean)
        btnNuevo.Enabled = ActNuevo
        btnBuscar.Enabled = ActBuscar
        btnVisualizar.Enabled = ActVisualizar
        btnSolicitaAprobacion.Enabled = ActSolicitud
        btnAnular.Enabled = ActAnular
        btnAgregar.Enabled = ActAgregar
    End Sub

    ' --- Nuevo pedido de hilos
    Public Sub prnNuevoPedido()
        txtSituacion.Text = "0"
        prcLimpiaControlesCabecera()
        prcLimpiaControlesDetalle()
        prcHabilitaControlesCabecera(True)
        prcHabilitaControlesDetalle(False)
        prcHabilitaDatosArticulo(True)
        prcHabilataBotonesAccion(False, True, False, True, False, True)

        prcValoresIniciales(False)

        'Conusltamos Usuario
        prcConsultarusuario()

        'Consultamos almacen
        ConsultaAlmacen()
    End Sub

    ' --- Valores iniciales
    Private Sub prcValoresIniciales(ByVal lblnEstado As Boolean)
        strFlag = txtSituacion.Text
        If strFlag = "0" Then
            txtSeriePedido.Text = strSeriePedido
            txtFechaPedido.Text = Date.Today.ToShortDateString
            txtFecInstal.Text = Date.Today.ToShortDateString
            txtEstado.Text = NombreEstado("ACTIVO")
            txtNumeroPedido.Focus()
        End If
    End Sub

    ' --- Estado de pedido
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
            Case "CULMINADO"
                strNomEstado = "CULMINADO"
        End Select
        Return strNomEstado
    End Function

#End Region

#Region "Datos"

    Private Sub prcAnulaPedido()
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtbPsto As New DataTable
        Dim strNumeroPedido As String
        Dim strUsuarioModi As String
        Dim stMensaje As String = ""

        lblError.Text = ""
        'lblError.Visible = False

        Try
            strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
            strUsuarioModi = Session("@USUARIO")
            'objPedido = New Logistica.clsPedidos
            'ldtbPsto = New DataTable

            ldtbPsto = objPedido.fncPedidoCambiaEstado_v2("1", strNumeroPedido, strUsuarioModi, Nothing)
            'lblError.Visible = True
            'Dim i As Integer
            'If ldtbPsto.Rows(0).Item("Codigo_Respuesta").ToString().Equals("100") Then
            '    lblError.Text = "Error: Este pedido no puede ser Anulado porque tiene vales de salida generados."
            '    Exit Sub
            'End If
            'If ldtbPsto.Rows(0).Item("Codigo_Respuesta").ToString().Equals("110") Then
            '    lblError.Text = "Error: Este pedido no puede ser Anulado porque tiene pre-despachos de parihuelas."
            '    Exit Sub
            'End If
            prcHabilitaControlesCabecera(False)
            prcHabilitaControlesDetalle(True)
            prcHabilitaDatosArticulo(False)
            prcHabilataBotonesAccion(True, True, True, False, False, False)
            btnSolicitaAprobacion.Enabled = False
            txtEstado.Text = "ANULADO"
            'txtPstoInicial.Text = ldtbPsto.Rows(0).Item("psto_inic").ToString
            'txtPstoUtilizado.Text = ldtbPsto.Rows(0).Item("psto_util").ToString
            'txtPstoDisponible.Text = ldtbPsto.Rows(0).Item("psto_disp").ToString
            'End If
            lblError.Text = "El Pedido ha sido Anulado"
        Catch ex As Exception
            lblError.Text = "Error al Anular vale: " + ex.Message
        Finally
            objPedido = Nothing
            ldtbPsto = Nothing
        End Try
    End Sub

    Public Function fn_ConsultarVales(ByVal strNuPedi As String) As Boolean
        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtbPedidos As New DataTable
        Dim lblValida As Boolean = False

        ldtbPedidos = objPedidos.fnConsultarValesPedidos(strNuPedi)

        If ldtbPedidos.Rows.Count > 0 Then
            lblValida = True
        End If

        Return lblValida
    End Function

    ' --- Consultamos Usuario
    Public Function prcConsultarusuario() As DataTable
        Dim lstrUsuario As String
        Dim ldtbUsuario As New DataTable

        lstrUsuario = Session("@USUARIO")
        ldtbUsuario = Nothing
        Try
            Dim objParametros As Object() = {"COD_USU", lstrUsuario}
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

    ' --- Guardamos pedido de hilos
    Private Sub GuardarPedido()
        Dim i As Integer
        Dim intNumItems As Integer = 0
        Dim dblTotalPedido As Double = 0
        Dim objPedidos As New Logistica.clsPedidos
        ' Dim ddlCentroCosto As ListBox
        Dim chrTipo As String = ""
        Dim strNumPedido As String = ""
        Dim strNumItem As String = ""
        Dim dblCantidad As Double = 0
        Dim strCodAuxiliar As String = ""
        Dim strCodDestino As String = ""
        Dim strCodOrden As String = ""
        Dim strCodSolicitante As String = ""
        Dim strCodAlmacen As String = ""
        Dim strObservacion As String = ""
        Dim strCodUsuario As String = ""
        Dim strTipPedido As String = ""
        Dim strFecnstal As String = ""
        Dim strTurno As String = ""
        Dim strLugarEntrega As String = ""
        Dim strObsDet As String = ""
        Dim ldtDetalle As New DataTable

        chrTipo = "1"
        strNumPedido = fncGeneraNumeroPedido()
        strNumItem = Trim(txtCodArticulo.Text)
        dblCantidad = Integer.Parse(Trim(txtCanConos.Text))
        strCodAuxiliar = ddlCentroCosto.SelectedValue 'Trim(txtCodCentroCostos.Text)
        strCodDestino = ""
        strCodOrden = Trim(txtCodOrdenServicio.Text)
        strCodSolicitante = Trim(txtCodSolicitante.Text)
        strCodAlmacen = Trim(txtCodAlmacen.Text)
        strObservacion = Trim(txtObservaciones.Text)
        strCodUsuario = Trim(Session("@USUARIO"))

        strTipPedido = Trim(cboPrioridad.SelectedValue)
        strFecnstal = Mid(txtFecInstal.Text, 7, 4) & Mid(txtFecInstal.Text, 4, 2) & Mid(txtFecInstal.Text, 1, 2)
        strObsDet = Trim(txtObsDet.Text)
        strTurno = ddlTurno.SelectedValue.ToString
        strLugarEntrega = ddlLugarEntrega.SelectedValue.ToString
        Try
            strFlag = txtSituacion.Text
            If strFlag = "0" Then
                ldtDetalle = objPedidos.fncGuardaPedido_v2(chrTipo, strNumPedido, strNumItem, dblCantidad, _
                            strCodAuxiliar, strCodDestino, strCodOrden, strCodUsuario, _
                            strCodAlmacen, strObservacion, strCodSolicitante, strTipPedido, strFecnstal, _
                            strObsDet, strTurno, strLugarEntrega)

                If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                    intNumItems = ldtDetalle.Rows.Count
                    If intNumItems > 0 Then
                        dgDetallePedido.DataSource = ldtDetalle
                        dgDetallePedido.DataBind()
                        dgDetallePedido.Visible = True

                        lblItems.Text = "Numero de Items:" + intNumItems.ToString
                        lblItems.Visible = True

                        txtNumeroPedido.Text = Mid(strNumPedido, 6, 10)
                        lblError.Text = "Se Genero el Pedido: " + strNumPedido
                        lblError.Visible = True

                        prcHabilitaControlesCabecera(False)
                        prcLimpiaControlesDetalle()
                        prcHabilataBotonesAccion(True, True, True, True, True, True)

                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al registrar el vale." + ex.Message
        End Try
    End Sub

    ' --- Genera un Nuevo Numero de Pedido
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

    ' --- Funcion que genera un Nuevo Numero de Pedido
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

        dtbPedido = Nothing
        ldtDetalle = Nothing

        strSerie = strSeriePedido
        intCodPedido = Integer.Parse(Mid(NumPedido, 6, 10))
        Try
            ' -- Consultamos Cabecera de Pedidos
            dtbPedido = objPedidos.fncConsultaPedidos(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
            If Not dtbPedido Is Nothing And dtbPedido.Rows.Count > 0 Then

                ' -- Habilitamos controles segun estado
                ManejoEstados(dtbPedido.Rows(0).Item("ti_situ"), dtbPedido.Rows(0).Item("nu_pedi"))

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
                ddlCentroCosto.SelectedValue = dtbPedido.Rows(0).Item("CodCentroCostos")
                lblDesCentroCostos.Text = dtbPedido.Rows(0).Item("DesCentroCostos")
                'cboPrioridad.SelectedValue = dtbPedido.Rows(0).Item("ti_pedido")
                txtFecInstal.Text = dtbPedido.Rows(0).Item("fe_instal")
                ddlTurno.SelectedValue = dtbPedido.Rows(0).Item("Turno")
                ddlLugarEntrega.SelectedValue = dtbPedido.Rows(0).Item("VCH_LUG_ENTREGA")
                txtObservaciones.Text = dtbPedido.Rows(0).Item("de_obse")

                ' -- Consultamos Detalle de Pedidos
                ldtDetalle = New DataTable
                ldtDetalle = objPedidos.fncConsultaDetallePedido("1", strSerie, intCodPedido)
                dgDetallePedido.DataSource = ldtDetalle
                dgDetallePedido.DataBind()
                dgDetallePedido.Visible = True
                intNumItems = ldtDetalle.Rows.Count.ToString
                lblItems.Text = "Numero de Items: " + ldtDetalle.Rows.Count.ToString
                lblItems.Visible = True

                lblTitulo_Cantidad.Text = IIf(dtbPedido.Rows(0).Item("co_alma") = "015", "Cant. Kilos:", "Cant. Conos:")
            Else
                lblError.Text = "El documento consultado no existe."
            End If
        Catch ex As Exception
            lblError.Text = "Error al consultar datos del vale." + ex.Message
        End Try
    End Sub

    ' --- Manejo de Estados
    Private Sub ManejoEstados(ByVal strEstado As String,
                              ByVal strNuPedi As String)
        Select Case strEstado
            Case "ACTIVO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(True)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(True)
                prcHabilataBotonesAccion(True, True, True, True, True, True)
                txtSeriePedido.Enabled = False
                txtNumeroPedido.Enabled = False
                txtSituacion.Text = "1"
                lblError.Text = "Pedido se encuentra activo."
            Case "POR APROBAR"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, True, True, True, False)
                txtSeriePedido.Enabled = False
                txtNumeroPedido.Enabled = False
                txtCodAlmacen.Enabled = False
                txtSituacion.Text = "6"
                lblError.Text = "Se ha enviado una Solicitud de aprobacion para este pedido, no puede ser modificado."
            Case "APROBADO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, False, False)
                txtSituacion.Text = "7"
                lblError.Text = "Este pedido ya ha sido aprobado, no puede ser modificado."
                If Not fn_ConsultarVales(strNuPedi) Then
                    btnAnular.Enabled = True
                End If
            Case "ANULADO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, False, False)
                txtSituacion.Text = "8"
                lblError.Text = "Este pedido esta anulado, no puede ser modificado."
            Case "ATENDIDO"
                prcHabilitaControlesCabecera(False)
                prcHabilitaControlesDetalle(False)
                prcLimpiaControlesDetalle()
                prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(True, True, False, True, False, False)
                txtSituacion.Text = "9"
                lblError.Text = "Este pedido ha sido atendido, no puede ser modificado."
        End Select
    End Sub

    ' --- Consultamos almacen
    Private Sub ConsultaAlmacen()
        Dim dtbAlmacen As New DataTable
        Dim objAlmacen As New clsAlmacen
        dtbAlmacen = Nothing
        strCodigoAlmacen = "007"

        Try
            If objAlmacen.Listar(dtbAlmacen, "1", strCodigoAlmacen, "") = True Then
                If dtbAlmacen.Rows.Count > 0 Then
                    txtCodAlmacen.Text = dtbAlmacen.Rows(0).Item("co_alma").ToString
                    lblDesAlmacen.Text = dtbAlmacen.Rows(0).Item("de_alma").ToString
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    ' --- Agrega/Modifica/Elimina el Item al detalle en la tabla Original
    Private Sub prcActulizaDetallePedido(ByVal chrTipo As String, ByVal strCodProducto As String)
        Dim intNumItems As Integer = 0
        Dim dblTotalPedido As Double = 0
        Dim objPedidos As New Logistica.clsPedidos
        Dim ldtDetalle As DataTable

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
        Dim strObsDet As String = ""

        ldtDetalle = Nothing

        strNumPedido = Trim(txtSeriePedido.Text) + "-" + Trim(txtNumeroPedido.Text)
        If Trim(strCodProducto.Length) > 0 Then
            strNumItem = strCodProducto
        Else
            strNumItem = Trim(txtCodArticulo.Text)
        End If
        dblCantidad = Double.Parse(Trim(txtCanConos.Text))
        strCodAuxiliar = Trim(ddlCentroCosto.SelectedValue)
        strCodDestino = ""
        strCodOrden = Trim(txtCodOrdenServicio.Text)
        strCodUsuario = Session("@USUARIO")
        strCodSolicitante = Trim(txtCodSolicitante.Text)
        strCodAlmacen = Trim(txtCodAlmacen.Text)
        strObs = Trim(txtObservaciones.Text)

        strTipPedido = Trim(cboPrioridad.SelectedValue)
        strFecInstal = Trim(txtFecInstal.Text)

        If Trim(txtAcepta.Text) = "" Or Trim(txtAcepta.Text).Length = 0 Then
            strSecuencia = "0"
        Else
            strSecuencia = Trim(txtAcepta.Text)
        End If
        strObsDet = Trim(txtObsDet.Text)
        Try
            ldtDetalle = objPedidos.prcRegistraDetallePedido(chrTipo, strNumPedido, strNumItem, _
                dblCantidad, strCodAuxiliar, strCodDestino, strCodOrden, strCodUsuario, _
                strCodAlmacen, strObs, strCodSolicitante, strSecuencia, strTipPedido, strFecInstal, strObsDet)
            If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
                intNumItems = ldtDetalle.Rows.Count
                If intNumItems > 0 Then
                    dgDetallePedido.DataSource = ldtDetalle
                    dgDetallePedido.DataBind()
                    dgDetallePedido.Visible = True
                    lblItems.Text = "Numero de Items:" + intNumItems.ToString
                    lblItems.Visible = True
                    prcLimpiaControlesDetalle()
                    prcHabilataBotonesAccion(True, True, True, True, True, True)
                    lblError.Text = ""
                    lblError.Visible = False
                End If
            Else
                intNumItems = 0
                dgDetallePedido.DataSource = Nothing
                dgDetallePedido.Visible = True
                lblItems.Text = "Numero de Items:" + intNumItems.ToString
                lblItems.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error al actualizar vale, comuniquese con Sistemas."
        End Try
    End Sub

    ' --- Actualiza el articulo
    Private Sub ActualizaritemPedido()
        Dim ldblNewCantidad As Double = 0
        Dim stMensaje As String = ""
        Dim strCodProducto As String = ""

        lblError.Text = ""
        strFlag = txtSituacion.Text
        ldblNewCantidad = Double.Parse(Trim(txtCanConos.Text))
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

#End Region

#Region "Validaciones"
    ' validamos datos de cabecera 
    Public Function ValidaDatoscabeceraPedido() As String
        lstrErrorDatosCab = ""
        Try
            If Trim(txtCodAlmacen.Text).Length = 0 Or (Trim(txtCodAlmacen.Text).Length > 0 And Trim(lblDesAlmacen.Text).Length = 0) Then
                lstrErrorDatosCab = "Error debe eligir un almacen."
                Return lstrErrorDatosCab
                Exit Function
            End If

            If ddlLugarEntrega.SelectedValue.Equals("0") Then
                lstrErrorDatosCab = "Seleccione el lugar de entrega."
                Return lstrErrorDatosCab
                Exit Function
            End If

        Catch ex As Exception
            lblError.Text = "Error: Validando datos de cabecera de pedido."
        End Try
        Return lstrErrorDatosCab
    End Function

    ' validamos detalle de pedido
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
            End If
            If Trim(txtCanConos.Text).Length = 0 Or Integer.Parse(Trim(txtCanConos.Text)) <= 0 Then
                lstrErrorDatosDet = "Debe ingresar una cantidad valida para el pedido."
                Return lstrErrorDatosDet
                Exit Function
            End If

        Catch ex As Exception
            lblError.Text = "Error: Verifique datos para el articulo del pedido."
        End Try
        Return lstrErrorDatosDet
    End Function

    ' validamos Articulos duplicados en el detalle del Pedido
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
            strCtagasto = ""
            strActivo = Trim(txtCodOrdenServicio.Text)
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

    ' validamos existencia de ActivoCTC
    Public Function ValidaActivoCTC() As String
        Dim objPedido As New Logistica.clsPedidos
        Dim strActivo As String
        lstrErrorActivo = ""
        strActivo = Trim(txtCodOrdenServicio.Text)
        If strActivo.Length > 0 Then
            Try
                If objPedido.fnc_VerificaActivoCTC(strActivo) = True Then
                    lstrErrorActivo = "Error: Debe elegir un activo/ctc valido."
                End If
            Catch ex As Exception
                lstrErrorActivo = "Error al Validar activo/ctc." + ex.ToString
            End Try
        End If
        Return lstrErrorActivo
    End Function

#End Region
    '--- boton visusalizar
    Protected Sub btnVisualizar_Click(sender As Object, e As EventArgs) Handles btnVisualizar.Click
        If txtNumeroPedido.Text.Length > 0 Then
            fnc_VerReporte()
        Else
            lblError.Text = "Error: Debe ingresar un numero de pedido a visualizar."
        End If
    End Sub

    '--- Listar reporte detalle
    Private Sub fnc_VerReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim intNumPedido As Integer = 0
        Try
            intNumPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))

            'CAMBIO DG INI
            'strPath = "%2fNM_Reportes%2f"
            'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
            'strURL = strURL + "logistica_ValePedidoHilo"
            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
            strURL = strURL + strPath
            strURL = strURL + "log_vale_pedido_hilo"
            'CAMBIO DG FIN
            strURL = strURL + "&chrTipo=" & "0"
            strURL = strURL + "&vchSerie=" & Mid(txtNumeroPedido.Text, 1, 4)
            strURL = strURL + "&intNumPedido=" & intNumPedido.ToString

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
        Catch ex As Exception
            lblError.Text = "Error la mostrar el reporte." + ex.Message
        End Try
    End Sub

    '--- grilla
    Private Sub dgDetallePedido_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetallePedido.ItemCommand
        lblError.Text = ""
        lblError.Visible = False
        Dim stMensaje As String = ""
        Dim lobjSecuencia As Label = CType(e.Item.FindControl("lblSecuencia"), Label)
        Dim lobjCodigo As Label = CType(e.Item.FindControl("lblCodigo"), Label)
        Dim lobjCantidad As Label = CType(e.Item.FindControl("lblCanConos"), Label)
        Dim lobjDescripcion As Label = CType(e.Item.FindControl("lblDescripcion"), Label)
        Dim lobjActivo As Label = CType(e.Item.FindControl("lblActivoFijo"), Label)
        Dim lobjDesActivo As Label = CType(e.Item.FindControl("lblDesActivo"), Label)

        Dim lobjBotonEliminar As ImageButton = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)
        If txtEstado.Text = "ACTIVO" Then
            Select Case e.CommandName
                Case "Editar"
                    txtSituacion.Text = "2"
                    txtCodArticulo.Text = lobjCodigo.Text
                    txtCanConos.Text = Strings.Format("{0,0.000}", lobjCantidad.Text)
                    txtCodOrdenServicio.Text = Trim(lobjActivo.Text)
                    lblDesServicio.Text = Trim(lobjDesActivo.Text)
                    lblDesArticulo.Text = lobjDescripcion.Text
                    txtAcepta.Text = lobjSecuencia.Text
                    prcHabilitaDatosArticulo(True)
                    txtCodArticulo.Enabled = False
                Case "Eliminar"
                    txtSituacion.Text = "3"
                    txtCodOrdenServicio.Text = Trim(lobjActivo.Text)
                    txtAcepta.Text = lobjSecuencia.Text
            End Select
        Else
            lblError.Text = "Error: Verifique la Situacion, No es posible Modificar el Pedido"
        End If
    End Sub

    '--- boton aprobar
    Protected Sub btnSolicitaAprobacion_Click(sender As Object, e As EventArgs) Handles btnSolicitaAprobacion.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtCorreos As New DataTable
        ldtCorreos = Nothing
        Try
            strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
            If txtAcepta.Text <> "0" And txtAcepta.Text.Length > 0 Then
                If objPedido.fncSolicitarAprobacionPedidoHilos(Session("@EMPRESA"), Trim(txtAcepta.Text), strNumeroPedido, _
                                            "FIN", "K", ddlCentroCosto.SelectedValue, _
                                            Session("@USUARIO"), ldtCorreos) Then
                    prcHabilitaDatosArticulo(False)
                    prcHabilitaControlesCabecera(False)
                    prcHabilitaControlesDetalle(True)
                    prcHabilitaDatosArticulo(False)
                    prcHabilataBotonesAccion(True, True, False, True, True, False)
                    txtEstado.Text = "APROBADO"
                    lblError.Text = "El pedido de hilo(s) fue aprobado con exito."

                    ' Enviamos email
                    EnviarCorreosPedido(ldtCorreos)
                    txtAcepta.Text = "0"
                End If
            Else
                lblError.Text = "Error: Debe Elegir un tipo de Aprobacion."
            End If
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error al Aprobar este documento." + ex.Message
        End Try
    End Sub

    '--- Envio de email
    Private Sub EnviarCorreosPedido(ByVal ldtbCorreos As DataTable)
        Dim i As Integer
        Dim strCodAlmacen As String
        Dim lstrError As String = ""
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String = ""
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""

        Dim mailMsg As System.Net.Mail.MailMessage
        Dim ldtbSolicitante As New DataTable
        Try
            ' Obtenemos los correos de la lista
            For i = 0 To ldtbCorreos.Rows.Count - 1
                If lstrPara.Trim.Length = 0 Then
                    lstrPara = ldtbCorreos.Rows(i).Item("email")
                Else
                    lstrPara = lstrPara + ";" + ldtbCorreos.Rows(i).Item("email")
                End If
            Next i
            If lstrCopia.Trim.Length = 0 Then
                lstrCopia = ""
            Else
                lstrCopia = lstrCopia
            End If
            strNumeroPedido = txtSeriePedido.Text + "-" + txtNumeroPedido.Text
            strCodAlmacen = txtCodAlmacen.Text


            'Cuerpo del e-mail
            lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
            "EL USUARIO: " + lblDesSolicitante.Text + " HA " + _
            "<B><FONT style='BACKGROUND-COLOR:#8DE806''>" + "APROBADO" + "</FONT></B>" + _
            " EL VALE DE ALMACEN #: " + strNumeroPedido + "<BR><BR>" + _
            "<B><FONT style='BACKGROUND-COLOR:#8DE806''>" + txtObservaciones.Text + "</FONT></B>" + _
            "<BR><BR><BR>" + _
            "<a href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>ACCESO DIRECTO AL SISTEMA INTRANET</a>" + _
            "<BR>" + _
            "</P>" + _
            "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>" + _
            "-------------------------------------------------------------------------------" + _
            "<BR>" + _
            "Este correo ha sido generado automáticamente por el módulo de aprobaciones." + _
            "<BR>" + _
            "Por favor no responder este correo." + _
            "<BR>" + _
            "Departamento de Sistemas" + _
            "<BR>" + _
            "Cia. Industrial Nuevo Mundo S.A." + _
            "<BR>" + _
            "-------------------------------------------------------------------------------" + _
            "</P>"
            lstrTitulo = "[Intranet] VALE DE ALMACEN(" + IIf(strCodAlmacen = "015", "DESPERDICIOS", "HILOS") + ") POR ATENDER: " + strNumeroPedido + " " + "APROBADO"

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
                '.From = New System.Net.Mail.MailAddress("VALE DE ALMACEN(" + IIf(strCodAlmacen = "015", "DESPERDICIOS", "HILOS") + ") POR ATENDER<aprobaciones@nuevomundosa.com>")
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
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    '--- boton nuevo
    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        prnNuevoPedido()
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        prcAnulaPedido()
    End Sub
End Class