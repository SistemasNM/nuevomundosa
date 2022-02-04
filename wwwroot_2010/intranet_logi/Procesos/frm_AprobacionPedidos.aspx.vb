Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_AprobacionPedidos
  Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

  'This call is required by the Web Form Designer.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
  Protected WithEvents label1 As System.Web.UI.WebControls.Label
  Protected WithEvents lblUsuario As System.Web.UI.WebControls.Label
  Protected WithEvents lblItems As System.Web.UI.WebControls.Label
  Protected WithEvents lblTotalPedido As System.Web.UI.WebControls.Label
  Protected WithEvents dgDetallePedido As System.Web.UI.WebControls.DataGrid
  Protected WithEvents pnlArticulo As System.Web.UI.WebControls.Panel
  Protected WithEvents lblMsg As System.Web.UI.WebControls.Label
  Protected WithEvents lblTextoTotal As System.Web.UI.WebControls.Label
  Protected WithEvents btnVerSeguimiento As System.Web.UI.WebControls.Button
  Protected WithEvents btnAprobar As System.Web.UI.WebControls.Button
  Protected WithEvents btnAnular As System.Web.UI.WebControls.Button
  Protected WithEvents btnSalir As System.Web.UI.HtmlControls.HtmlInputButton
  Protected WithEvents lblError As System.Web.UI.WebControls.Label
  Protected WithEvents lblAlmacen As System.Web.UI.WebControls.Label
  Protected WithEvents lblSolicitante As System.Web.UI.WebControls.Label
  Protected WithEvents lblCentroCostos As System.Web.UI.WebControls.Label
  Protected WithEvents lblFechaPedido As System.Web.UI.WebControls.Label
  Protected WithEvents lblNumeroPedido As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblEstado As System.Web.UI.WebControls.Label
  Protected WithEvents lblNumPedido As System.Web.UI.WebControls.Label
  Protected WithEvents Label3 As System.Web.UI.WebControls.Label
  Protected WithEvents txtCanX As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtAcepta As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtSituacion As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblObservaciones As System.Web.UI.WebControls.Label
  Protected WithEvents txtCantidad As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblDesArticulo As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblPrecioArticulo As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblUniMedida As System.Web.UI.WebControls.TextBox
  Protected WithEvents lblCodArticulo As System.Web.UI.WebControls.TextBox
  Protected WithEvents btnActualizar As System.Web.UI.WebControls.ImageButton
  Protected WithEvents lblDesCantidad As System.Web.UI.WebControls.Label
  Protected WithEvents lblDesDes As System.Web.UI.WebControls.Label
  Protected WithEvents lblDesPrecio As System.Web.UI.WebControls.Label
  Protected WithEvents lblDesUM As System.Web.UI.WebControls.Label
  Protected WithEvents lblDesCodigo As System.Web.UI.WebControls.Label
  Protected WithEvents dgSeguimiento As System.Web.UI.WebControls.DataGrid
  Protected WithEvents txtCtaGasto As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtOrden As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtSecuencia As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtFecInstal As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtPrioridad As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblActivoCTC As System.Web.UI.WebControls.Label
    Protected WithEvents btnEditarFecInstal As System.Web.UI.WebControls.ImageButton
    Protected WithEvents hdnFlg As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnReiniciar As System.Web.UI.WebControls.Button
  'NOTE: The following placeholder declaration is required by the Web Form Designer.
  'Do not delete or move it.
  Private designerPlaceholderDeclaration As System.Object

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    Session("@GRUPO_CODIGO") = "3000"
    Session("@EMPRESA") = "01"
    'Session("@USUARIO") = "vvalenci"

    'If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
    '    Response.Redirect("/intranet/finsesion.htm", True)
    'End If

    InitializeComponent()
  End Sub
#End Region

  Dim lintNumeroPedido As Integer
  Dim strSeriePedido, strFlag, stMensaje As String
  Dim strNumeroPedido, strCodDestino, strCodOrden, strSecuencia As String
  Dim intNumItems As Integer

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If Not (Page.IsPostBack) Then
      ' Cargamos el pedido desde la venta de Consulta
      If Not IsDBNull(Request.Item("strNumeroPedido")) Or Not (Request.Item("strNumeroPedido")) Is Nothing Then
        strNumeroPedido = Request.Item("strNumeroPedido")
        'strNumeroPedido = "0003-0000051268"
        CargaPedido(strNumeroPedido)
        fnc_ListarSeguimientoPedido(strNumeroPedido)
      End If
        End If
        'REQSIS201900029 - DG - INI
        If hdnFlg.Value = "1" Then
            CargaPedido(Request.Item("strNumeroPedido"))
        End If
        btnEditarFecInstal.Attributes.Add("onClick", "javascript:btnEditarFecha_Onclick();")
        btnReiniciar.Attributes.Add("onClick", "javascript:return fnc_ConfirmarOperacion();")
        'REQSIS201900029 - DG - FIN
    lblUsuario.Text = Session("@USUARIO")
        btnVerSeguimiento.Attributes.Add("onClick", "javascript:btnSeguimiento_Onclick();")
    End Sub

  ' --- Carga de Pedido
#Region "Carga Pedido Aprobar"
  ' --- Metodo: Para Cargar el Pedido
  Private Sub CargaPedido(ByVal NumPedido As String)
    Dim objPedidos As Logistica.clsPedidos
    Dim ldtDetalle As DataTable
    Dim intCodPedido As Integer
    Dim strSerie As String = "0003"
    Dim strTipo As String = "0"
    Dim strFechaIni As String = ""
    Dim strFechaFin As String = ""
    Dim strSolicitante As String = ""
    Dim strEstado As String = ""
    Dim dblTotalPedido As Double
    Dim i As Integer
    Dim dtbPedido As DataTable

    intCodPedido = Integer.Parse(Mid(NumPedido, 6, 10))
    Try
      ' -- Consultamos Cabecera de Pedidos
      objPedidos = New Logistica.clsPedidos
      dtbPedido = objPedidos.fncConsultaPedidos(strTipo, strSerie, intCodPedido, strFechaIni, strFechaFin, strSolicitante, strEstado)
      If Not dtbPedido Is Nothing Then
        If dtbPedido.Rows().Count > 0 Then
          ManejoEstados(dtbPedido.Rows(0).Item("ti_situ"))
          lblNumPedido.Text = dtbPedido.Rows(0).Item("nu_pedi")
          lblNumeroPedido.Text = Mid(dtbPedido.Rows(0).Item("nu_pedi"), 6, 10)
          lblAlmacen.Text = Trim(dtbPedido.Rows(0).Item("co_alma")) + " - " + dtbPedido.Rows(0).Item("de_alma")
          lblSolicitante.Text = dtbPedido.Rows(0).Item("CodSolicitante") + " - " + dtbPedido.Rows(0).Item("NomSolicitante")
          lblCentroCostos.Text = dtbPedido.Rows(0).Item("CodCentroCostos") + " - " + dtbPedido.Rows(0).Item("DesCentroCostos")
          lblFechaPedido.Text = dtbPedido.Rows(0).Item("fe_pedi")
          lblEstado.Text = dtbPedido.Rows(0).Item("ti_situ")
          lblObservaciones.Text = dtbPedido.Rows(0).Item("de_obse")
          lblActivoCTC.Text = dtbPedido.Rows(0).Item("co_orde_serv") & "-" & dtbPedido.Rows(0).Item("de_acti")
          txtPrioridad.Text = dtbPedido.Rows(0).Item("TI_PEDIDO")
          txtFecInstal.Text = dtbPedido.Rows(0).Item("FE_INSTAL")

          ' -- Habilitamos controles segun estado
          ManejoEstados(objPedidos.EstadoPedido)

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
            If ldtDetalle.Rows(i).Item("CA_APRO") > 0 Then
              dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("CA_APRO") * ldtDetalle.Rows(i).Item("PE_ITEM")
            Else
              dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("CA_PEDI") * ldtDetalle.Rows(i).Item("PE_ITEM")
            End If
          Next
          Me.lblTextoTotal.Visible = True
          lblTotalPedido.Text = dblTotalPedido.ToString()
          lblTotalPedido.Visible = True
        End If
      End If
    Catch ex As Exception
      lblError.Text = "Error al cargar vale."
    End Try
  End Sub

#End Region

  ' --- Manejo de Estados
#Region "Metodos"
  Private Sub ManejoEstados(ByVal strEstado As String)
    Dim lstrMensajeEstado As String
    Select Case strEstado
      Case "ACTIVO"
        prcLimpiaControlesDetalle()
        prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(False, True, True, True, False, False)
        txtSituacion.Text = "5"
      Case "POR APROBAR"
        prcLimpiaControlesDetalle()
        prcHabilitaDatosArticulo(True)
                prcHabilataBotonesAccion(False, True, True, True, False, True)
        txtSituacion.Text = "6"
      Case "APROBADO"
        lstrMensajeEstado = "Este Pedido ya ha sido Aprobado, No puede ser Modificado"
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & lstrMensajeEstado & ".');</script>")
        prcLimpiaControlesDetalle()
        prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(False, False, False, False, False, False)
        txtSituacion.Text = "7"
      Case "ANULADO"
        lstrMensajeEstado = "Este Pedido esta Anulado, No puede ser Modificado"
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & lstrMensajeEstado & ".');</script>")
        prcLimpiaControlesDetalle()
        prcHabilitaDatosArticulo(False)
                prcHabilataBotonesAccion(False, False, False, False, False, False)
        txtSituacion.Text = "8"
    End Select
  End Sub

  ' --- habilita Controles
    Private Sub prcHabilataBotonesAccion(ByVal ActSeguimiento As Boolean, ByVal ActAprobar As Boolean, ByVal ActAnular As Boolean, _
                               ByVal ActActualizar As Boolean, ByVal ActEliminar As Boolean, ByVal ActReiniciar As Boolean)
        btnAprobar.Enabled = ActAprobar
        btnAnular.Enabled = ActAnular
        btnActualizar.Enabled = ActActualizar
        btnReiniciar.Enabled = ActReiniciar
    End Sub

  ' --- Nombre de Estado de Pedido
  Private Function NombreEstado(ByVal strEstado As String) As String
    Dim strNomEstado As String = ""
    Select Case strEstado
      Case "ACTIVO"
        strNomEstado = "ACTIVO"
      Case "APROBADO"
        strNomEstado = "APROBADO"
      Case "ANULADO"
        strNomEstado = "ANULADO"
    End Select
    Return strNomEstado
  End Function

  Private Sub prcHabilitaDatosArticulo(ByVal lblnEstado As Boolean)
    ' datos del Articulo
    pnlArticulo.Visible = lblnEstado
    'lblDesCodigo.Visible = lblnEstado
    lblCodArticulo.Visible = lblnEstado
    'lblDesUM.Visible = lblnEstado
    lblUniMedida.Visible = lblnEstado
    'lblDesPrecio.Visible = lblnEstado
    lblPrecioArticulo.Visible = lblnEstado
    'lblDesDes.Visible = lblnEstado
    lblDesArticulo.Visible = lblnEstado
    'lblDesCantidad.Visible = lblnEstado
    txtCantidad.Visible = lblnEstado
    btnActualizar.Visible = lblnEstado
    btnActualizar.Visible = lblnEstado
  End Sub

  ' --- Limpia Controles del Detalle
  Private Sub prcLimpiaControlesDetalle()
    lblCodArticulo.Text = ""
    lblUniMedida.Text = ""
    lblPrecioArticulo.Text = ""
    lblDesArticulo.Text = ""
    txtCantidad.Text = Strings.Format("{0,0.0000}", 0)
  End Sub

#End Region

  ' --- Acceso a datos
#Region "Acceso a datos"

  ' Funcion que nos consulta el solicitante del Pedido
  Public Function fConsultaEmailSolicitante(ByVal strNumeroPedido As String) As DataTable
    Dim objTabla As DataTable
    Dim strConsulta As String
    Try
      objTabla = New DataTable
      strConsulta = "usp_qry_ConsultaEmailPedido"
      Dim objParametros As Object() = {"strNumeroPedido", strNumeroPedido}

      objTabla = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsulta, objParametros)
    Catch ex As Exception
      lblError.Text = "Ha ocurrido un error a consultar email, comuniquese con Sistemas"
      lblError.Visible = True
      objTabla = Nothing
    End Try
    Return objTabla
  End Function

  ' Anulamos/Aprobamos el pedido 
  Private Sub prcActualizarEstado(ByVal strTipo As String)
    lblMsg.Text = ""
    lblMsg.Visible = False
    Dim strUsuario As String
    Dim strAccion As String
    Dim strNumPedido As String

    Dim objPedido As Logistica.clsPedidos
    Dim ldtbActualizaPedido As DataTable
    Dim ldtCorreos As DataTable

    stMensaje = ""
    strNumPedido = lblNumPedido.Text
    strUsuario = Session("@USUARIO")
    If strTipo = "1" Then
      strAccion = "ANULAR"
      lblEstado.Text = "ANULADO"
      stMensaje = "Se Anulo el Pedido: " + strNumPedido + "?"
      lblMsg.Text = "Pedido Anulado"

    End If
    If strTipo = "2" Then
      strAccion = "APROBAR"
      lblEstado.Text = "APROBADO"
      stMensaje = "Se Aprobo el Pedido: " + strNumPedido + "?"
            lblMsg.Text = "Pedido Aprobado"
        End If

        'REQSIS201900029 - DG - INI
        If strTipo = "4" Then
            strAccion = "ACTIVO"
            lblEstado.Text = "ACTIVO"
            stMensaje = "Se Reinicio el Pedido: " + strNumPedido + "?"
            lblMsg.Text = "Pedido Reiniciado"

        End If
        'REQSIS201900029 - DG -FIN
    Try
      objPedido = New Logistica.clsPedidos
      ' Aprobamos, Anulamos Pedido
      ldtbActualizaPedido = objPedido.fncPedidoCambiaEstado(strTipo, strNumPedido, strUsuario, ldtCorreos)
      ' Envio de correo, segun Accion APR, ANU
      If Not ldtbActualizaPedido Is Nothing And ldtbActualizaPedido.Rows.Count > 0 Then
        EnviarCorreosPedido(strTipo, ldtbActualizaPedido)
      End If
      ' Manejo de Controles
      prcHabilitaDatosArticulo(False)
            prcHabilataBotonesAccion(False, False, False, False, False, False)
      lblMsg.Visible = True
      ' Lista Detalle de Pedido Actuializado
      Dim ldtDetalle As DataTable
      Dim strSerie As String
      Dim intCodPedido As Integer

      ldtDetalle = New DataTable
      strSerie = Left(lblNumPedido.Text, 4)
      intCodPedido = Integer.Parse(Mid(lblNumPedido.Text, 6, 10))
      ldtDetalle = objPedido.fncConsultaDetallePedido("1", strSerie, intCodPedido)
      dgDetallePedido.DataSource = ldtDetalle
      dgDetallePedido.DataBind()
    Catch ex As Exception
      lblError.Text = "Ha ocurrido un error al anular o desaprobar vale, comuniquese con Sistemas"
    End Try
  End Sub

  ' Actualizamos la cantidad aprobada
  Private Sub prcActualizaDetalle()
    lblMsg.Text = ""
    lblMsg.Visible = False

    Dim strCodArticulo As String
    Dim ldblNewCantidad As Double
    Dim stMensaje As String = ""

    strCodArticulo = lblCodArticulo.Text
    ldblNewCantidad = CDbl(Trim(txtCantidad.Text))
    Try
      If strCodArticulo.Length > 0 Then
        If ldblNewCantidad > 0 Then
          prcActulizaDetallePedido("1")
          btnActualizar.Enabled = True
          prcLimpiaControlesDetalle()
          lblMsg.Text = "Articulo Modificado"
          lblMsg.Visible = True
        Else
          lblMsg.Text = "Error: Debe Ingresar una Cantidad Valida, Mayor a Cero"
          lblMsg.Visible = True
        End If
      Else
        lblMsg.Text = "Error: Debe Elegir un Registro a Modificar de la Lista"
        lblMsg.Visible = True
      End If
    Catch ex As Exception
      lblError.Text = "Ha ocurrido un error al actualizar cantidad, comuniquese con Sistemas"
    End Try
  End Sub

  ' --- Agrega/Modifica/Elimina el Item al detalle
  Private Sub prcActulizaDetallePedido(ByVal chrTipo As String)
    Dim i As Integer
    Dim dblTotalPedido As Double = 0

    Dim objPedidos As New Logistica.clsPedidos
    Dim ldtDetalle As New DataTable

    Dim strNumPedido As String
    Dim strNumItem As String
    Dim dblCantidad As Double
    Dim strUsuario As String

    strNumPedido = lblNumPedido.Text
    strNumItem = lblCodArticulo.Text
    dblCantidad = CDbl(Trim(txtCantidad.Text))
    strUsuario = Session("@USUARIO")
    strCodDestino = txtCtaGasto.Text
    strCodOrden = txtOrden.Text
    strFlag = txtSituacion.Text
    ' Actualizamos en Pedido Existente
    Try
      ldtDetalle = objPedidos.fncActualizarCantidades(chrTipo, strNumPedido, strNumItem, _
                                                      dblCantidad, strUsuario, strCodDestino, strCodOrden)
      If Not ldtDetalle Is Nothing Then
        dgDetallePedido.DataSource = ldtDetalle
        dgDetallePedido.DataBind()
        dgDetallePedido.Visible = True
        lblItems.Text = "Numero de Items: " + intNumItems.ToString
        lblItems.Visible = True
        'Calculamos Monto del Pedido
        For i = 0 To intNumItems - 1
          If ldtDetalle.Rows(i).Item("CA_APRO") > 0 Then
            dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("CA_APRO") * ldtDetalle.Rows(i).Item("PE_ITEM")
          Else
            dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("CA_PEDI") * ldtDetalle.Rows(i).Item("PE_ITEM")
          End If
        Next
        lblTextoTotal.Visible = True
        lblTotalPedido.Text = dblTotalPedido.ToString()
        lblTotalPedido.Visible = True
      Else
        lblMsg.Text = "No existe Registro para Mostrar"
        lblMsg.Visible = True
        dgDetallePedido.Visible = False
        lblItems.Visible = False
      End If
    Catch ex As Exception
      lblError.Text = "Ha ocurrido un error al actulizar vale, comuniquese con Sistemas"
    End Try
  End Sub

  ' Elimina un Articulo del Pedido
  Private Sub prcEliminaRegistroPedido(ByVal strCodProducto As String)
    Dim stMensaje As String = ""
    Dim intNumItems As Integer = 0

    strFlag = txtSituacion.Text
    lblMsg.Text = ""
    lblMsg.Visible = False

    If strCodProducto.Length > 0 Then
      lblMsg.Text = "Se Elimino el Articulo: " + strCodProducto + " del Pedido"
      ' Actualza en el detalle
      prcEliminaDetallePedido("3", strCodProducto)
      prcLimpiaControlesDetalle()
      prcHabilitaDatosArticulo(True)
      lblMsg.Visible = True
      txtSituacion.Text = "1"
    Else
      lblMsg.Text = "Error: Debe Elegir el Articulo a Eliminar de la Lista"
      lblMsg.Visible = True
    End If
  End Sub

  '--- Agrega/Modifica/Elimina el Item al detalle en la tabla Original
  Private Sub prcEliminaDetallePedido(ByVal chrTipo As String, ByVal strCodProducto As String)

    Dim i As Integer
    Dim intNumItems As Integer
    Dim dblTotalPedido As Double = 0

    Dim objPedidos As New Logistica.clsPedidos
    Dim ldtDetalle = New DataTable

    Dim strNumPedido As String
    Dim strNumItem As String
    Dim dblCantidad As Double
    Dim strCodAuxiliar As String
    Dim strCodUsuario As String
    Dim strCodSolicitante As String
    Dim strCodAlmacen As String
    Dim strObs As String

    Dim strTipPed As String
    Dim strFecInst As String

    strNumPedido = lblNumPedido.Text
    strNumItem = strCodProducto

    dblCantidad = CDbl(Trim(txtCantidad.Text))
    strCodAuxiliar = Left(Me.lblCentroCostos.Text, 7)
    strCodUsuario = Session("@USUARIO")
    strCodSolicitante = Left(lblSolicitante.Text, 5)
    strCodAlmacen = Left(lblAlmacen.Text, 3)
    strCodDestino = txtCtaGasto.Text
    strCodOrden = txtOrden.Text
    strSecuencia = txtSecuencia.Text
    strObs = lblObservaciones.Text

    strTipPed = txtPrioridad.Text
    strFecInst = txtFecInstal.Text

    ' Actualizamos en Pedido Existente
    Try
      ldtDetalle = objPedidos.prcRegistraDetallePedido(chrTipo, strNumPedido, strNumItem, _
                      dblCantidad, strCodAuxiliar, strCodDestino, strCodOrden, strCodUsuario, _
                      strCodAlmacen, strObs, strCodSolicitante, strSecuencia, strTipPed, strFecInst, "")
      If Not ldtDetalle Is Nothing And ldtDetalle.Rows.Count > 0 Then
        intNumItems = ldtDetalle.Rows.Count
        If intNumItems > 0 Then
          dgDetallePedido.DataSource = ldtDetalle
          dgDetallePedido.DataBind()
          dgDetallePedido.Visible = True
          lblItems.Text = "Numero de Items:" + intNumItems.ToString
          lblItems.Visible = True
          'Calculamos Monto del Pedido
          For i = 0 To intNumItems - 1
            dblTotalPedido = dblTotalPedido + ldtDetalle.Rows(i).Item("SubTotal")
          Next
          lblTextoTotal.Visible = True
          lblTotalPedido.Text = dblTotalPedido.ToString()
          lblTotalPedido.Visible = True
          lblMsg.Text = ""
          lblMsg.Visible = False
          prcLimpiaControlesDetalle()
                    prcHabilataBotonesAccion(False, True, True, True, False, True)
        End If
      Else
        intNumItems = 0
        dgDetallePedido.DataSource = Nothing
        dgDetallePedido.Visible = False
        lblItems.Text = "Numero de Items:" + intNumItems.ToString
        lblItems.Visible = True
        lblTotalPedido.Text = "0.00"
        lblTextoTotal.Visible = True
      End If
    Catch ex As Exception
      lblError.Text = "Ha ocurrido un error al editar vale, comuniquese con Sistemas"
    End Try
  End Sub
#End Region

#Region "Grilla"

  Private Sub dgDetallePedido_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetallePedido.ItemCommand
    If Me.lblEstado.Text <> "APROBADO" Then
      lblMsg.Text = ""
      lblMsg.Visible = False

      Dim stMensaje As String
      Dim lobjSecuencia As Label = CType(e.Item.FindControl("lblSecuencia"), Label)
      Dim lobjCodigoPedido As Label = CType(e.Item.FindControl("lblCodigo"), Label)
      Dim lobjDescripcion As Label = CType(e.Item.FindControl("lblDescripcion"), Label)
      Dim lobjUniMed As Label = CType(e.Item.FindControl("lblUniMed"), Label)
      Dim lobjPrecio As Label = CType(e.Item.FindControl("lblPrecio"), Label)
      Dim lobjCantidad As Label = CType(e.Item.FindControl("lblCantidad"), Label)
      Dim lobjCantidadAprobada As Label = CType(e.Item.FindControl("lblCantidadAprobada"), Label)
      Dim lobjActivo As Label = CType(e.Item.FindControl("lblActivoFijo"), Label)
      Dim lobjCtaGasto As Label = CType(e.Item.FindControl("lblCtagasto"), Label)

      Dim lobjBotonEditar As ImageButton = CType(e.Item.FindControl("btnEditarItem"), ImageButton)
      Dim lobjBotonEliminar As ImageButton = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)

      If strFlag <> "5" Then
        Select Case e.CommandName
          Case "Editar"
            Dim strCodItem As String
            Dim strCantidad As String
            Dim strDesItem As String
            Dim strUnidad As String
            Dim strPrecio As String

            strCodItem = lobjCodigoPedido.Text
            strDesItem = lobjDescripcion.Text
            strUnidad = lobjUniMed.Text
            strPrecio = lobjPrecio.Text
            txtCtaGasto.Text = lobjCtaGasto.Text
            txtOrden.Text = lobjActivo.Text
            txtSecuencia.Text = lobjSecuencia.Text

            If CDbl(lobjCantidadAprobada.Text) > 0 Then
              strCantidad = lobjCantidadAprobada.Text
            Else
              strCantidad = lobjCantidad.Text
            End If
            prcHabilitaDatosArticulo(True)
            lobjBotonEditar.Attributes.Add("onClick", "EditarItemPedido('" + strCodItem + "', '" + strCantidad + "', '" + strDesItem + "', '" + strUnidad + "', '" + strPrecio + "')")
            lobjBotonEditar.Enabled = True
          Case "Eliminar"
            txtCtaGasto.Text = lobjCtaGasto.Text
            txtOrden.Text = lobjActivo.Text
            txtSecuencia.Text = lobjSecuencia.Text
            prcEliminaRegistroPedido(Trim(lobjCodigoPedido.Text))
        End Select
      Else
        lblMsg.Text = "Error: Verifique el Estado. No es posible Modificar este Pedido"
        lblMsg.Visible = True
      End If
    Else
      dgDetallePedido.Enabled = True
      lblError.Text = "Error: Verifique la Situacion, No es posible Modificar el Pedido"
      lblError.Visible = True
    End If

  End Sub

  Private Sub dgDetallePedido_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetallePedido.ItemDataBound
  End Sub

#End Region

#Region "Envio de Email"
  '-- Envio de Email al ser Aprobar o Anular Pedido
  Private Sub EnviarCorreosPedido(ByVal strTipo As String, ByVal ldtbCorreos As DataTable)

    Dim i As Integer
    Dim strAccion As String
    'Dim strSolicitante As String
    Dim lstrError As String = ""
    Dim lstrCuerpoMensaje As String = ""
    Dim lstrTitulo As String = ""
    Dim lstrPara As String = ""
    Dim lstrCopia As String = ""

    Dim mailMsg As System.Net.Mail.MailMessage


    'Dim x As System.Net.Mail.MailMessage

    Dim ldtbSolicitante As DataTable
    Try
      ' Obtenemos los correos de la lista
      For i = 0 To ldtbCorreos.Rows.Count - 1
        If lstrPara.Trim.Length = 0 Then
          lstrPara = ldtbCorreos.Rows(i).Item("UsuarioCorreo")
        Else
          lstrPara = lstrPara + ";" + ldtbCorreos.Rows(i).Item("UsuarioCorreo")
        End If
      Next i
      If lstrCopia.Trim.Length = 0 Then
        lstrCopia = ""
      Else
        lstrCopia = lstrCopia
      End If
      strNumeroPedido = Trim(lblNumPedido.Text)
      ' Adicionamos a la lista de Correos el usuario que solicito el Pedido
      If strTipo = "1" Then
        strAccion = "ANULADO"
        ' Informamos  al Solicitante
        lstrPara = ""
        ldtbSolicitante = fConsultaEmailSolicitante(Trim(Me.lblNumPedido.Text))
        If Not ldtbSolicitante Is Nothing And ldtbSolicitante.Rows.Count() > 0 Then
          lstrPara = ldtbSolicitante.Rows(0).Item("vch_email").ToString
                End If
                'REQSIS201900029 - DG - INI
            ElseIf strTipo = "4" Then
                strAccion = "REINICIADO"
                For i = 0 To ldtbCorreos.Rows.Count - 1
                    If lstrPara.Trim.Length = 0 Then
                        lstrPara = ldtbCorreos.Rows(i).Item("CorreoCopia")
                    Else
                        lstrPara = lstrPara + ";" + ldtbCorreos.Rows(i).Item("CorreoCopia")
                    End If
                    lstrCopia = ldtbCorreos.Rows(i).Item("UsuarioCorreo")
                Next i
                If lstrCopia.Trim.Length = 0 Then
                    lstrCopia = ""
                Else
                    lstrCopia = lstrCopia
                End If
                'REQSIS201900029 - DG - FIN
            Else
                strAccion = "APROBADO"
                ' Copiamos al Solicitante
                ldtbSolicitante = fConsultaEmailSolicitante(Trim(Me.lblNumPedido.Text))
                If Not ldtbSolicitante Is Nothing And ldtbSolicitante.Rows.Count() > 0 Then
                    lstrCopia = ldtbSolicitante.Rows(0).Item("vch_email").ToString
                End If
      End If
      'Cuerpo del e-mail
      lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> " + _
      "EL USUARIO: " + lblUsuario.Text + " HA " + strAccion + " EL VALE DE ALMACEN #: " + lblNumPedido.Text + "<BR><BR>" + _
      "<B><FONT style='BACKGROUND-COLOR: #ffff66'>" + lblObservaciones.Text + "</FONT></B>" + _
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
      lstrTitulo = "[Intranet] VALE DE ALMACEN POR ATENDER: " + lblNumPedido.Text + " " + strAccion

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
                '.From = New System.Net.Mail.MailAddress("VALE DE ALMACEN POR ATENDER<aprobaciones@nuevomundosa.com>")
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
      lblError.Visible = False
      'lblError.Text = "Ha ocurrido un error al enviar email, comuniquese con Sistemas"
    End Try
  End Sub
#End Region

  Private Sub btnSeguimiento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        prcHabilataBotonesAccion(True, True, True, False, False, True)
  End Sub

  Private Sub btnAprobar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAprobar.Click

        'REQSIS201900029 - DG - INI
        If (DateTime.Compare(Now.ToShortDateString(), txtFecInstal.Text) > 0) Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('La fecha de instalación es menor a la fecha de aprobación. \n Debe modificar la fecha de instalación. \n O de lo contrario revertir el pedido para la modificación.');</script>")
        Else
            prcActualizarEstado("2")
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
        End If
        'prcActualizarEstado("2")
        'ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language =javaScript>Aprobar('1');</Script>")
        'REQSIS201900029 - DG - FIN
        
  End Sub

  Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
    prcActualizarEstado("1")
    strNumeroPedido = Trim(lblNumPedido.Text)
    fnc_ListarSeguimientoPedido(strNumeroPedido)
  End Sub

  Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnActualizar.Click
        prcActualizaDetalle()
  End Sub

  Private Sub fnc_ListarSeguimientoPedido(ByVal strNumeroPedido As String)
    Dim objPedido As New Logistica.clsPedidos
    Dim ldtbSeguimiento As New DataTable
    ldtbSeguimiento = Nothing
    Try
      objPedido.NumPedido = Integer.Parse(Mid(strNumeroPedido, 6, 10))
      objPedido.fnc_ListarSeguimientoPedido(ldtbSeguimiento)
      dgSeguimiento.DataSource = ldtbSeguimiento
      dgSeguimiento.DataBind()
    Catch ex As Exception
      lblError.Text = "Ha ocurrido un error a consultar seguimiento de vale, comuniquese con Sistemas"
    End Try
  End Sub
    'REQSIS201900029 - DG - INI
    Private Sub btnReiniciar_Click(sender As Object, e As System.EventArgs) Handles btnReiniciar.Click
        prcActualizarEstado("4")
    End Sub
    'REQSIS201900029 - DG - FIN
End Class
