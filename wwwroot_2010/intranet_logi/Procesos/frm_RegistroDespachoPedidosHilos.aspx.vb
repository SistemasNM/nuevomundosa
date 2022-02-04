Imports NM.AccesoDatos
Imports NuevoMundo

Public Class frm_RegistroDespachoPedidosHilos
    Inherits System.Web.UI.Page

    Private Sub frm_RegistroDespachoPedidosHilos_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@GRUPO_CODIGO") = "3000"
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "atorresc"
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion_popup.htm", True)
        End If
    End Sub

  ' load del formulario
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblFechaDespacho.Text = Format(Now, "dd/MM/yyyy")
        Dim strNumPedido As String = ""
        If Not (Page.IsPostBack) Then
            Try
                If (Not Request.Item("strNumeroPedido") Is Nothing) Then
                    strNumPedido = Request.Item("strNumeroPedido")
                    CargaPedido(strNumPedido)
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
        btnSalir.Attributes.Add("onClick", "javascript:fnc_Cerrar();")
        btnVales.Attributes.Add("onClick", "javascript:VerValesPedido('" + txtNumeroPedido.Text + "')")
    End Sub

  ' cargar el pedido
  Private Sub CargaPedido(ByVal strNumPedido As String)
    Dim objPedidos As New Logistica.clsPedidos
    Dim dtbPedido As New DataTable
    Dim ldtDetalle As New DataTable

    Dim intCodPedido As Integer = 0
    Dim strSerie As String = ""
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
    strSerie = Mid(strNumPedido, 1, 4)
    intCodPedido = Integer.Parse(Mid(strNumPedido, 6, 10))
    Try
      ' -- Consultamos Cabecera de Pedidos
      dtbPedido = objPedidos.fncConsultaPedidos(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
      If Not objPedidos Is Nothing Then
        ManejoEstados(dtbPedido.Rows(0).Item("ti_situ"))
        txtNumeroPedido.Text = Trim(Mid(dtbPedido.Rows(0).Item("nu_pedi"), 1, 4)) + "-" + Trim(Mid(dtbPedido.Rows(0).Item("nu_pedi"), 6, 10))
        txtDesAlmacen.Text = Trim(dtbPedido.Rows(0).Item("co_alma")) + "-" + Trim(dtbPedido.Rows(0).Item("de_alma"))
        txtDesSolicitante.Text = Trim(dtbPedido.Rows(0).Item("CodSolicitante")) + "-" + Trim(dtbPedido.Rows(0).Item("NomSolicitante"))
        lblDesRecepciona.Text = Trim(dtbPedido.Rows(0).Item("NomSolicitante"))
        txtDesCentroCostos.Text = Trim(dtbPedido.Rows(0).Item("CodCentroCostos")) + "-" + Trim(dtbPedido.Rows(0).Item("DesCentroCostos"))
        txtFechaPedido.Text = Trim(dtbPedido.Rows(0).Item("fe_pedi"))
        txtFechaAprobacion.Text = Trim(dtbPedido.Rows(0).Item("fe_apro"))
        txtObservaciones.Text = Trim(dtbPedido.Rows(0).Item("de_obse"))

        ' -- Consultamos Solicitante
        prcConsultaEmpleado(dtbPedido.Rows(0).Item("CodSolicitante"))

        ' -- Consultamos Detalle de Pedidos
        ldtDetalle = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)
        If Not ldtDetalle Is Nothing Then
          Session("Detalle") = ldtDetalle
          dgDetalle.DataSource = ldtDetalle
          dgDetalle.DataBind()
          dgDetalle.Visible = True
        End If
        'Session("Detalle") = dgDetalle
        intNumItems = ldtDetalle.Rows.Count.ToString
        lblItems.Text = "Numero de articulos por atender: " + ldtDetalle.Rows.Count.ToString
        lblItems.Visible = True
      End If
    Catch ex As Exception
      lblError.Text = "Error al cargar el vale." + ex.Message
    End Try
  End Sub

  ' Consulta Empleado
  Private Sub prcConsultaEmpleado(ByVal strCodSolicitante As String)
    Dim objPedidos As New Logistica.clsPedidos
    Dim dtbEmpleados As New DataTable
    Dim Ruta As String = ""
    'lblError.Text = ""
    dtbEmpleados = Nothing
    Try
      dtbEmpleados = objPedidos.fncPedidoConsultaEmpleado(strCodSolicitante)
      If Not dtbEmpleados Is Nothing Or dtbEmpleados.Rows.Count > 0 Then
        txtCodSolicitante.Text = dtbEmpleados.Rows(0).Item("co_trab")
        txtDesSolicitante.Text = dtbEmpleados.Rows(0).Item("Nombres")
        txtDesArea.Text = dtbEmpleados.Rows(0).Item("de_area")
        txtDesSeccion.Text = dtbEmpleados.Rows(0).Item("de_secc")
        txtDesCargo.Text = dtbEmpleados.Rows(0).Item("de_pues_trab")
        If (dtbEmpleados.Rows(0).Item("fe_cese_trab")) = "Ceso" Then
            lblError.Text = "Verifique al empleado solicitante, ha cesado."
        End If
      End If
    Catch ex As Exception
      'lblError.Text = "Error al consultar el empleado." + ex.Message
    End Try
  End Sub

  ' menejo de estado
  Private Sub ManejoEstados(ByVal strEstado As String)
    Select Case strEstado
      Case "APROBADO"
        txtEstado.Text = strEstado
        HabilitaBotonesAccion(True, True, True)
      Case "ATENDIDO"
        txtEstado.Text = strEstado
        HabilitaBotonesAccion(True, False, False)
        lblError.Text = "Vale de almacen ya ha sido atendido, no puede ser modificado"
      Case "CULMINADO"
        txtEstado.Text = strEstado
        lblError.Text = "Vale de almacen ha sido culminado, no puede ser modificado"
        HabilitaBotonesAccion(True, False, False)
    End Select
  End Sub

  ' habilita botones
  Private Sub HabilitaBotonesAccion(ByVal blnVer As Boolean, ByVal blnCulminar As Boolean, ByVal blnDespachar As Boolean)
        btnVerVale.Enabled = blnVer
        btnCulminar.Enabled = blnCulminar
        btnDespachar.Enabled = blnDespachar
    End Sub

  ' boton vista previa
  Protected Sub btnVerVale_Click(sender As Object, e As EventArgs) Handles btnVerVale.Click
    fnc_VerReporte()
  End Sub

  ' Listar reporte detalle
  Private Sub fnc_VerReporte()
    Dim strURL As String = ""
    Dim strPath As String = ""
    Dim strScript As String = ""
    Dim intNumPedido As Integer = 0
    Try
      'intNumPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
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
      strURL = strURL + "&intNumPedido=" & Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
      strURL = strURL + "&rc:Command=Render"
      strURL = strURL + "&rc:Toolbar=true"
      strScript = "fMostrarReporte('" & strURL & "');"
      ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    Catch ex As Exception
      lblError.Text = "Error la mostrar el reporte." + ex.Message
    End Try
  End Sub

  ' boton despachar
  Protected Sub btnDespachar_Click(sender As Object, e As EventArgs) Handles btnDespachar.Click
    Dim objPedido As New Logistica.clsPedidos
    Dim ldtbDetalle As New DataTable
    Dim dtbRetorno As New DataTable
    Dim strNumeroPedido As String = ""
    Dim strActivoFijo As String = ""
    Dim strUsuario As String = ""
    Dim strCodAlmacen As String = ""
    Dim strCodCentroCostos As String = ""
    Dim intCodPedido As Integer = 0

    ldtbDetalle = Nothing
    dtbRetorno = Nothing

    lblError.Text = "Procesando..."

    strNumeroPedido = Trim(txtNumeroPedido.Text)
    strActivoFijo = ""
    strUsuario = Session("@USUARIO")
    strCodAlmacen = Mid(Trim(txtDesAlmacen.Text), 1, 3)
    strCodCentroCostos = Mid(txtDesCentroCostos.Text, 1, 7)
    Try
      'Guardamos datos
      fncGuardarDetalle()

      ' extraemos la data actualizada solo lo despachable (con stock y pendiente)
      ldtbDetalle = objPedido.fnc_ListarPedidoPorDespachar("1", strNumeroPedido, strUsuario)
      If Not ldtbDetalle Is Nothing And ldtbDetalle.Rows.Count > 0 Then
        ' Guardamos el vale de salida
        dtbRetorno = objPedido.fnc_PedidoValesDetalle_Grabar(strUsuario, strNumeroPedido)

        If Mid(dtbRetorno.Rows(0)("Resultado"), 1, 5) <> "Error" Then

          ' Refrescamos la grilla
          intCodPedido = Integer.Parse(Mid(txtNumeroPedido.Text, 6, 10))
          ldtbDetalle = objPedido.fncConsultaDetallePedido("2", Mid(txtNumeroPedido.Text, 1, 4), intCodPedido)
          Session("Detalle") = ldtbDetalle
          dgDetalle.DataSource = ldtbDetalle
          dgDetalle.DataBind()
          dgDetalle.Visible = True

          'lblError.Text = dtbRetorno.Rows(0)("Resultado").ToString
          lblError.Text = "Se atendio el pedido con exito. Consulte los vales generados."
          btnDespachar.Enabled = False
        Else
          lblError.Text = "Error al despachar vale."
        End If
      Else
        lblError.Text = "Verique stock, no es posible registrar la salida."
      End If
    Catch ex As Exception
      lblError.Text = "Error al despachar el vale." + ex.Message
    End Try
End Sub

  ' Funcion Guarda en masivo
  Public Sub fncGuardarDetalle()
    Dim i As Integer = 0
    Dim max As Integer = 0
    Dim intCodPedido As Integer = 0
    Dim strSerie As String = ""
    Dim strNumeroPedido As String = ""
    Dim strCodArticulo As String = ""
    Dim strCtaGasto As String = ""
    Dim strOrdenServicio As String = ""
    Dim strSecuencia As String = ""
    Dim strUsuario As String = ""
    Dim ldblNewCantidad As Double = 0
    Dim ldblNewCantidadKgs As Double = 0
    Dim ldblNewPendiente As Double = 0
    Dim ldblStock As Double = 0
    Dim ldblDespachable As Double = 0

    Dim ldblDespachable_Alte As Double = 0

    Dim objPedidos As New Logistica.clsPedidos
    Dim ldtDetalleOriginal As New DataTable
    Dim txtDespachable As TextBox
    Dim txtDespachableKgs As TextBox
    Dim strTipo As String = ""

    Try
      ' extraemo la tabla con datos originales
      strSerie = Mid(txtNumeroPedido.Text, 1, 4)
      intCodPedido = Integer.Parse(Trim(Mid(txtNumeroPedido.Text, 6, 10)))
      ldtDetalleOriginal = objPedidos.fncConsultaDetallePedido("2", strSerie, intCodPedido)

      ' Guardamos los cambios en las filas editadas
      strNumeroPedido = Trim(txtNumeroPedido.Text)
      strUsuario = Session("@USUARIO")
      If Not ldtDetalleOriginal Is Nothing Then
        max = ldtDetalleOriginal.Rows.Count
        strTipo = "2"
        If max > 0 Then
          For i = 0 To max - 1
            ' datos originales
            strSecuencia = ldtDetalleOriginal.Rows(i).Item("nu_secu").ToString
            strCodArticulo = ldtDetalleOriginal.Rows(i).Item("co_item").ToString
            ldblDespachable = ldtDetalleOriginal.Rows(i).Item("ca_desp")
            ldblDespachable_Alte = ldtDetalleOriginal.Rows(i).Item("ca_reqi_alte")

            txtDespachable = dgDetalle.Items(i).Cells(0).FindControl("txtDespachable")
            txtDespachableKgs = dgDetalle.Items(i).Cells(0).FindControl("txtDespachableKgs")

            ldblNewCantidad = Double.Parse(txtDespachable.Text)
            ldblNewCantidadKgs = Double.Parse(txtDespachableKgs.Text)

            ldblStock = ldtDetalleOriginal.Rows(i).Item("Stock_alte")
            ldblNewPendiente = ldtDetalleOriginal.Rows(i).Item("ca_pend")

            If IsDBNull(ldtDetalleOriginal.Rows(i).Item("CtaGasto")) = True Then
              strCtaGasto = ""
            Else
              strCtaGasto = ldtDetalleOriginal.Rows(i).Item("CtaGasto")
            End If
            If IsDBNull(ldtDetalleOriginal.Rows(i).Item("ActivoFijo")) = True Then
              strOrdenServicio = ""
            Else
              strOrdenServicio = ldtDetalleOriginal.Rows(i).Item("ActivoFijo")
            End If

            prcActualizaDetalle(strNumeroPedido, strCodArticulo, ldblNewCantidad, ldblNewCantidadKgs, ldblStock, _
               ldblNewPendiente, strUsuario, strCtaGasto, strOrdenServicio)
          Next
        End If
      Else
        lblError.Text = "Error no hay datos para edicion."
      End If
    Catch ex As Exception
      lblError.Text = "Error al guardar el detalle." + ex.Message
    End Try
  End Sub

   ' Actualizamos la cantidad a despachar
  Private Sub prcActualizaDetalle(ByVal strNumPedido As String, ByVal strCodArticulo As String, ByVal ldblNewCantidad As Double, ldblNewCantidadKgs As Double, _
                                  ByVal ldblStock As Double, _
                                  ByVal ldblNewPendiente As Double, ByVal strUsuario As String, ByVal strCtaGasto As String, ByVal strOrdenServicio As String)
    Dim ldtDetalle As New DataTable
    Dim objPedidos As New Logistica.clsPedidos
    ldtDetalle = Nothing
    lblError.Text = ""
    Try
      ' Validamos que eliga un articulo a modificar
      If strCodArticulo.Length > 0 Then
        ' Validamos que ingrese cantidad
        If ldblNewCantidad >= 0 And ldblNewCantidadKgs >= 0 Then
          ' Validamos que ingrese cantidad pendiente
          If ldblNewCantidad <= ldblNewPendiente Then
            ' Validamos que exista Stock
            If ldblStock >= ldblNewCantidad Then
              Try
                ' Actualizamos en Pedido
                ldtDetalle = objPedidos.fncActualizarCantidadesHilo("2", strNumPedido, strCodArticulo, _
                                        ldblNewCantidad, ldblNewCantidadKgs, strUsuario, strCtaGasto, strOrdenServicio)
              Catch ex As Exception
                lblError.Text = "Error al actualizar vale." + ex.Message
              End Try
            Else
              lblError.Text = "Error debe Ingresar una cantidad menor o igual al stock"
            End If
          Else
            lblError.Text = "Error la cantidad ingresada es mayor a lo pendiente por despachar"
          End If
        Else
          lblError.Text = "Error debe ingresar una cantidad mayor a cero en Conos y kgs."
        End If
      Else
        lblError.Text = "Error: debe elegir un registro a modificar de la lista"
      End If
    Catch ex As Exception
      lblError.Text = "Error al actualizar detalle de vale." + ex.Message
    End Try
  End Sub

  ' boton culminar
  Protected Sub btnCulminar_Click(sender As Object, e As EventArgs) Handles btnCulminar.Click
        Dim objPedido As New Logistica.clsPedidos
        Dim ldtbPedido As New DataTable
        Dim strNumeroPedido As String = ""
        Dim strUsuarioModi As String = ""

        ldtbPedido = Nothing
        lblError.Text = ""

        strNumeroPedido = txtNumeroPedido.Text
        strUsuarioModi = Session("@USUARIO")
        Try
            ldtbPedido = objPedido.fncPedidoCambiaEstado("3", strNumeroPedido, strUsuarioModi, Nothing)
            If Not ldtbPedido Is Nothing And ldtbPedido.Rows.Count > 0 Then
                btnDespachar.Enabled = False
                btnCulminar.Enabled = False
                txtEstado.Text = "CULMINADO"
                lblError.Text = "Vale fue culminado con exito"
            End If
        Catch ex As Exception
            lblError.Text = "Error al culminar vale." + ex.Message
        End Try
    End Sub
End Class