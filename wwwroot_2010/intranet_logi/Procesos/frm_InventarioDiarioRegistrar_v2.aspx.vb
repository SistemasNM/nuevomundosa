Public Class frm_InventarioDiarioRegistrar_v2
    Inherits System.Web.UI.Page

    Dim strMensaje As String = ""

    Private Sub frm_InventarioDiarioRegistrar_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If Session("@EMPRESA") = Nothing Then
            Session("@EMPRESA") = "01"
        End If

        'Session("@USUARIO") = "AAMPUERP"

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

    ' Load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call CargaUnidad()
            Call CargaAlmacenes()
            btnConsultar.Attributes.Add("onclick", "javascript: return ListarERI();")
            btnGenerar.Attributes.Add("onclick", "javascript: return ValidaGenerar();")
            HabilitaTextos(False)
            HabilitaBotones(False, False, False, False)
        End If

    End Sub

    ' Boton consultar
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim strCodigoInventario As String = ""
        Dim strTipo As String = ""
        lblMensaje.Text = ""

        Try
            strCodigoInventario = Trim(txtcodinv.Text)
            strTipo = "2"
            If strCodigoInventario.Length = 10 Then
                If ddlAlmacenes.SelectedValue.Equals("005") Then
                    Call ConsultaInventarioDiario_2(3, strCodigoInventario)
                    HabilitaTextos(True)
                    If lblEstado.Text = "ABIERTO" Then
                        HabilitaBotones(False, True, True, True)
                        btnGenerar.Enabled = False
                    Else
                        HabilitaBotones(False, False, False, True)
                        btnGuardar.Enabled = False
                        btnEstado.Enabled = False
                    End If
                Else
                    Call ConsultaInventarioDiario(strTipo, strCodigoInventario)
                    HabilitaTextos(True)
                    If lblEstado.Text = "ABIERTO" Then
                        HabilitaBotones(False, True, True, True)
                        btnGenerar.Enabled = False
                    Else
                        HabilitaBotones(False, False, False, True)
                        btnGuardar.Enabled = False
                        btnEstado.Enabled = False
                    End If
                End If
            Else
                lblMensaje.Text = "Error: Debe ingresar el codigo de inventario a consultar."
            End If
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try
    End Sub

    ' Boton Nuevo
    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        LimpiarControles()
        HabilitaTextos(False)
        HabilitaBotones(True, False, False, False)
        txtcodinv.Text = ""
        txtcodinv.Focus()
        rdbParamBusqueda.Visible = False
        lblInfoArt.Visible = False
        txtArtBusqueda.Text = ""
        txtArtBusqueda.Visible = False
        btnConsultarArt.Visible = False
        lblInfoDesde.Visible = False
        txtUbicDesde.Text = ""
        txtUbicDesde.Visible = False
        lblInfoHasta.Visible = False
        txtUbicHasta.Text = ""
        txtUbicHasta.Visible = False
    End Sub

    ' Boton genenar ERI
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim dstInventario As New DataSet

        Dim intMasCostoso As Integer = 0
        Dim intMenosCostoso As Integer = 0
        Dim intMasRotacion As Integer = 0
        Dim intMenosRotacion As Integer = 0
        Dim intSinRotacion As Integer = 0
        Dim strUsuario As String = ""
        Dim strCodigoInventario As String = ""
        Dim strEmpresa As String
        Dim strUnidad As String
        Dim strAlmacen As String
        Dim strFlagBusqueda As String
        Dim strCodArticulo As String
        Dim strUbicDesde As String
        Dim strUbicHasta As String

        dstInventario = Nothing
        lblMensaje.Text = ""
        strUsuario = Session("@USUARIO")
        strEmpresa = Session("@EMPRESA")

        Try
            ValidaParametros()
            If strMensaje.Length = 0 Then
                intMasCostoso = Integer.Parse(Trim(txtMasCostoso.Text))
                intMenosCostoso = Integer.Parse(Trim(txtMenosCostoso.Text))
                intMasRotacion = Integer.Parse(Trim(txtMasRotacion.Text))
                intMenosRotacion = Integer.Parse(Trim(txtMenosRotacion.Text))
                intSinRotacion = Integer.Parse(Trim(txtSinRotacion.Text))
                strAlmacen = ddlAlmacenes.SelectedValue.ToString
                strUnidad = ddlUnidad.SelectedValue.ToString
                strFlagBusqueda = rdbParamBusqueda.SelectedValue.ToString()
                strCodArticulo = txtArtBusqueda.Text.Trim
                strUbicDesde = txtUbicDesde.Text.Trim
                strUbicHasta = txtUbicHasta.Text.Trim


                If ddlAlmacenes.SelectedValue.ToString.Equals("005") Then
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '''''''''''''''''''''''''''''''''''''''''''''''''ALMACEN 005''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If objArticulo.InventarioDiario_Generar_v2(dstInventario, intMasCostoso, intMenosCostoso, intMasRotacion, _
                                                        intMenosRotacion, intSinRotacion, strUsuario, _
                                                        strAlmacen, strEmpresa, strUnidad, strFlagBusqueda, strCodArticulo, strUbicDesde, strUbicHasta) = True Then

                        If Not dstInventario Is Nothing And dstInventario.Tables(0).Rows.Count > 0 Then
                            'Consultamos nuevo inventario ERI
                            strCodigoInventario = dstInventario.Tables(0).Rows(0)("vch_CodigoInventario").ToString

                            If strCodigoInventario.Length = 10 Then
                                Call ConsultaInventarioDiario_2("3", strCodigoInventario)
                                HabilitaBotones(False, False, True, True)
                            End If

                            lblMensaje.Text = "Se genero el inventario:" + Trim(txtcodinv.Text)
                        Else
                            lblMensaje.Text = "No se puedo generar el inventario - ERI"
                        End If
                    End If

                Else
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '''''''''''''''''''''''''''''''''''''''''''TODOS LOS ALMACENES EXCEPTO EL 005'''''''''''''''''''''''''''''''''''''''''''''''''
                    If objArticulo.InventarioDiario_Generar(dstInventario, intMasCostoso, intMenosCostoso, intMasRotacion, _
                                                        intMenosRotacion, intSinRotacion, strUsuario, _
                                                        strAlmacen, strEmpresa, strUnidad) = True Then

                        If Not dstInventario Is Nothing And dstInventario.Tables(0).Rows.Count > 0 Then
                            'Consultamos nuevo inventario ERI
                            strCodigoInventario = dstInventario.Tables(0).Rows(0)("vch_CodigoInventario").ToString

                            If strCodigoInventario.Length = 10 Then
                                Call ConsultaInventarioDiario("2", strCodigoInventario)
                                HabilitaBotones(False, True, True, True)
                            End If

                            lblMensaje.Text = "Se genero el inventario:" + Trim(txtcodinv.Text)
                        Else
                            lblMensaje.Text = "No se puedo generar el inventario - ERI"
                        End If
                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                End If
            Else
                lblMensaje.Text = strMensaje
            End If


        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try

        dstInventario = Nothing
    End Sub

    ' Boton estado
    Protected Sub btnEstado_Click(sender As Object, e As EventArgs) Handles btnEstado.Click
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim strCodigoInventario As String = ""
        Dim strUsuario As String = ""
        Dim strEmpresa As String
        Dim strUnidad As String
        Dim strAlmacen As String

        strEmpresa = Session("@EMPRESA")
        strAlmacen = ddlAlmacenes.SelectedValue.ToString
        strUnidad = ddlUnidad.SelectedValue.ToString

        lblMensaje.Text = ""
        Try
            strUsuario = Session("@USUARIO")
            strCodigoInventario = Trim(txtcodinv.Text)
            If strCodigoInventario.Length = 10 Then
                If objArticulo.InventarioDiario_Cerrar("CER", strCodigoInventario, strUsuario, strEmpresa, strUnidad, strAlmacen) = True Then
                    Call ConsultaInventarioDiario_2(3, strCodigoInventario)
                    lblEstado.Text = "CERRADO"
                    HabilitaBotones(False, False, False, True)
                    btnGuardar.Enabled = False
                    btnEstado.Enabled = False
                Else
                    lblMensaje.Text = "Error: No se pudo cerrar el inventario actual."
                End If
            Else
                lblMensaje.Text = "Error: Debe ingresar el codigo de inventario a consultar."
            End If
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try

    End Sub

    ' Boton guardar
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim lobj_util As New NM_General.Util
        Dim ldtb_datos As New DataTable
        Dim ldtr_dato As DataGridItem
        Dim ldr_fila As DataRow
        Dim strCodigoInventario As String = ""
        Dim strTipo As String = ""
        Dim strUsuario As String = ""
        Dim strDetalle As String = ""

        Dim strEmpresa As String
        Dim strUnidad As String
        Dim strAlmacen As String

        strEmpresa = Session("@EMPRESA")
        strAlmacen = ddlAlmacenes.SelectedValue.ToString
        strUnidad = ddlUnidad.SelectedValue.ToString

        Try
            strCodigoInventario = Trim(txtcodinv.Text)
            strTipo = "GUA"
            strUsuario = Session("@USUARIO")
            ldtb_datos.Columns.Add("c1", GetType(String))
            ldtb_datos.Columns.Add("c2", GetType(String))
            ldtb_datos.Columns.Add("c3", GetType(Double))

            For Each ldtr_dato In dtgInventario.Items
                ldr_fila = ldtb_datos.NewRow()
                ldr_fila("c1") = ldtr_dato.Cells(2).Text
                ldr_fila("c2") = ldtr_dato.Cells(3).Text
                ldr_fila("c3") = IIf(IsNumeric(CType(ldtr_dato.FindControl("txtcantinv"), TextBox).Text) = True, CType(ldtr_dato.FindControl("txtcantinv"), TextBox).Text, 0)
                ldtb_datos.Rows.Add(ldr_fila)
            Next

            ldtb_datos.TableName = "lista"
            strDetalle = lobj_util.GeneraXml(ldtb_datos)
            If objArticulo.InventarioDiario_Guardar(strTipo, strCodigoInventario, strDetalle, strUsuario, strEmpresa, strUnidad, strAlmacen) = True Then
                lblMensaje.Text = "Datos guardados satisfactoriamente."
            Else
                lblMensaje.Text = "No se pudo guardar los datos, verifique la informacion."
            End If

        Catch ex As Exception
            lblMensaje.Text = ex.Message
        Finally
        End Try

        lobj_util = Nothing
    End Sub

    ' Boton exportar
    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim strCodigoInventario As String = ""
        Dim strTipo As String = ""
        Try
            strCodigoInventario = Trim(txtcodinv.Text)
            strTipo = "2"
            If strCodigoInventario.Length = 10 Then
                fnc_VerReportePendientes()
                If ddlAlmacenes.SelectedValue.Equals("005") Then
                    Call ConsultaInventarioDiario_2(3, strCodigoInventario)
                    HabilitaTextos(True)
                    If lblEstado.Text = "ABIERTO" Then
                        HabilitaBotones(False, True, True, True)
                        btnGenerar.Enabled = False
                    Else
                        HabilitaBotones(False, False, False, True)
                        btnGuardar.Enabled = False
                        btnEstado.Enabled = False
                    End If
                Else
                    Call ConsultaInventarioDiario(strTipo, strCodigoInventario)
                    HabilitaTextos(True)
                    If lblEstado.Text = "ABIERTO" Then
                        HabilitaBotones(False, True, True, True)
                        btnGenerar.Enabled = False
                    Else
                        HabilitaBotones(False, False, False, True)
                        btnGuardar.Enabled = False
                        btnEstado.Enabled = False
                    End If
                End If
            Else
                lblMensaje.Text = "Error: Verifique codigo de inventario a exportar."
            End If
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try
    End Sub
    'Comentado por falta de funcion //
    Private Sub CargaAlmacenes()

        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim dtAlmacenes As New DataTable
        Dim strEmpresa, strUnidad As String

        strEmpresa = Session("@EMPRESA")
        strUnidad = ddlUnidad.SelectedValue

        dtAlmacenes = objArticulo.InventarioDiario_Listado_Almacenes_v2(strEmpresa, strUnidad)
        ddlAlmacenes.DataSource = dtAlmacenes
        ddlAlmacenes.DataValueField = "CO_ALMA"
        ddlAlmacenes.DataTextField = "DE_ALMA"
        ddlAlmacenes.DataBind()

        objArticulo = Nothing

    End Sub


    Private Sub CargaUnidad()
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim dtUnidad As New DataTable
        Dim strEmpresa As String

        strEmpresa = Session("@EMPRESA")

        dtUnidad = objArticulo.InventarioDiario_Listado_Unidad(strEmpresa)
        ddlUnidad.DataSource = dtUnidad
        ddlUnidad.DataValueField = "CO_UNID"
        ddlUnidad.DataTextField = "DE_UNID"
        ddlUnidad.DataBind()

        objArticulo = Nothing

    End Sub

#Region "Procedimientos"

    ' Consulta un inventario 005
    Private Function ConsultaInventarioDiario_2(strTipo As String, strCodigoInventario As String) As Integer
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim dstInventario As New DataSet
        Dim intConsulta As Integer = 0
        Dim strEmpresa As String
        Dim strUnidad As String
        Dim strAlmacen As String

        strEmpresa = Session("@EMPRESA")
        strAlmacen = ddlAlmacenes.SelectedValue.ToString
        strUnidad = ddlUnidad.SelectedValue.ToString

        LimpiarControles()
        dstInventario = Nothing
        Try
            objArticulo.InventarioDiario_Listado_v2(dstInventario, strTipo, strCodigoInventario, strEmpresa, strUnidad, strAlmacen)
            If Not dstInventario Is Nothing Then
                intConsulta = dstInventario.Tables(0).Rows.Count()
                ' cabecera
                If Not dstInventario Is Nothing And intConsulta > 0 Then

                    ddlUnidad.SelectedValue = dstInventario.Tables(0).Rows(0)("vch_CodigoUnidad").ToString
                    ddlAlmacenes.SelectedValue = dstInventario.Tables(0).Rows(0)("vch_CodigoAlmacen").ToString
                    txtcodinv.Text = dstInventario.Tables(0).Rows(0)("vch_CodigoInventario").ToString
                    lblEstado.Text = dstInventario.Tables(0).Rows(0)("chr_EstadoInventario").ToString
                    txtMasCostoso.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMasCaros").ToString
                    txtMenosCostoso.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMenosCaros").ToString
                    txtMasRotacion.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMasMov").ToString
                    txtMenosRotacion.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMenosMov").ToString
                    txtSinRotacion.Text = dstInventario.Tables(0).Rows(0)("int_ItemsSinMov").ToString
                    rdbParamBusqueda.SelectedValue = dstInventario.Tables(0).Rows(0)("vch_FlabBusqueda").ToString
                    If rdbParamBusqueda.SelectedValue.Equals("ART") Then                        
                        lblInfoArt.Visible = True
                        txtArtBusqueda.Visible = True
                        btnConsultarArt.Visible = True
                        lblInfoDesde.Visible = False
                        txtUbicDesde.Text = ""
                        txtUbicDesde.Visible = False
                        lblInfoHasta.Visible = False
                        txtUbicHasta.Text = ""
                        txtUbicHasta.Visible = False
                        txtArtBusqueda.Text = dstInventario.Tables(0).Rows(0)("vch_CodigoArticulo").ToString
                    ElseIf rdbParamBusqueda.SelectedValue.Equals("UBI") Then
                        lblInfoArt.Visible = False
                        txtArtBusqueda.Visible = False
                        txtArtBusqueda.Text = ""
                        btnConsultarArt.Visible = False
                        lblInfoDesde.Visible = True
                        txtUbicDesde.Visible = True
                        lblInfoHasta.Visible = True
                        txtUbicHasta.Visible = True
                        txtUbicDesde.Text = dstInventario.Tables(0).Rows(0)("vch_UbicDesde").ToString
                        txtUbicHasta.Text = dstInventario.Tables(0).Rows(0)("vch_UbicHasta").ToString
                    End If

                    txtTotal.Text = (Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMasCaros")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMenosCaros")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMasMov")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMenosMov")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsSinMov"))).ToString

                    dtgInventario.DataSource = Nothing
                    dtgInventario.DataBind()

                    grdInventarioTelas.DataSource = Nothing
                    grdInventarioTelas.DataBind()

                    grdInventarioUbic.DataSource = Nothing
                    grdInventarioUbic.DataBind()
                End If

                ' detalle
                If Not dstInventario Is Nothing And strTipo = "3" Then
                    If dstInventario.Tables(1).Rows.Count > 0 Then
                        If rdbParamBusqueda.SelectedValue.Equals("ART") Then
                            grdInventarioTelas.DataSource = dstInventario.Tables(1)
                            grdInventarioTelas.DataBind()
                        ElseIf rdbParamBusqueda.SelectedValue.Equals("UBI") Then
                            grdInventarioUbic.DataSource = dstInventario.Tables(1)
                            grdInventarioUbic.DataBind()
                        End If

                        lblNumReq.Text = "Numero de items:" + dstInventario.Tables(1).Rows.Count.ToString
                    Else
                        dtgInventario.DataSource = Nothing
                        dtgInventario.DataBind()
                        grdInventarioTelas.DataSource = Nothing
                        grdInventarioTelas.DataBind()
                        grdInventarioUbic.DataSource = Nothing
                        grdInventarioUbic.DataBind()
                        lblNumReq.Text = "Numero de items:" + "0"
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
        End Try

        dtgInventario = Nothing
        grdInventarioTelas = Nothing
        grdInventarioUbic = Nothing

        objArticulo = Nothing

        Return intConsulta
    End Function

    ' Consulta un inventario
    Private Function ConsultaInventarioDiario(strTipo As String, strCodigoInventario As String) As Integer
        Dim objArticulo As New NuevoMundo.clsArticulo
        Dim dstInventario As New DataSet
        Dim intConsulta As Integer = 0
        Dim strEmpresa As String
        Dim strUnidad As String
        Dim strAlmacen As String

        strEmpresa = Session("@EMPRESA")
        strAlmacen = ddlAlmacenes.SelectedValue.ToString
        strUnidad = ddlUnidad.SelectedValue.ToString

        LimpiarControles()
        dstInventario = Nothing
        Try
            objArticulo.InventarioDiario_Listado(dstInventario, strTipo, strCodigoInventario, strEmpresa, strUnidad, strAlmacen)
            If Not dstInventario Is Nothing Then
                intConsulta = dstInventario.Tables(0).Rows.Count()
                ' cabecera
                If Not dstInventario Is Nothing And intConsulta > 0 Then

                    ddlUnidad.SelectedValue = dstInventario.Tables(0).Rows(0)("vch_CodigoUnidad").ToString
                    ddlAlmacenes.SelectedValue = dstInventario.Tables(0).Rows(0)("vch_CodigoAlmacen").ToString
                    txtcodinv.Text = dstInventario.Tables(0).Rows(0)("vch_CodigoInventario").ToString
                    lblEstado.Text = dstInventario.Tables(0).Rows(0)("chr_EstadoInventario").ToString
                    txtMasCostoso.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMasCaros").ToString
                    txtMenosCostoso.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMenosCaros").ToString
                    txtMasRotacion.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMasMov").ToString
                    txtMenosRotacion.Text = dstInventario.Tables(0).Rows(0)("int_ItemsMenosMov").ToString
                    txtSinRotacion.Text = dstInventario.Tables(0).Rows(0)("int_ItemsSinMov").ToString

                    rdbParamBusqueda.Visible = False
                    lblInfoArt.Visible = False
                    txtArtBusqueda.Visible = False
                    txtArtBusqueda.Text = ""
                    btnConsultarArt.Visible = False
                    lblInfoDesde.Visible = False
                    txtUbicDesde.Visible = False
                    txtUbicDesde.Text = ""
                    lblInfoHasta.Visible = False
                    txtUbicHasta.Visible = False
                    txtUbicHasta.Text = ""


                    txtTotal.Text = (Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMasCaros")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMenosCaros")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMasMov")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsMenosMov")) + _
                    Integer.Parse(dstInventario.Tables(0).Rows(0)("int_ItemsSinMov"))).ToString

                    dtgInventario.DataSource = Nothing
                    dtgInventario.DataBind()
                End If

                ' detalle
                If Not dstInventario Is Nothing And strTipo = "2" Then
                    If dstInventario.Tables(1).Rows.Count > 0 Then
                        dtgInventario.DataSource = dstInventario.Tables(1)
                        dtgInventario.DataBind()
                        lblNumReq.Text = "Numero de items:" + dstInventario.Tables(1).Rows.Count.ToString
                    Else
                        dtgInventario.DataSource = Nothing
                        dtgInventario.DataBind()
                        lblNumReq.Text = "Numero de items:" + "0"
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
        End Try

        dtgInventario = Nothing
        objArticulo = Nothing

        Return intConsulta
    End Function

    ' Valida parametros para registro
    Private Sub ValidaParametros()
        strMensaje = ""

        If Trim(txtMasCostoso.Text).Length = 0 Then
            txtMasCostoso.Text = "0"
        End If

        If Trim(txtMenosCostoso.Text).Length = 0 Then
            txtMenosCostoso.Text = "0"
        End If

        If Trim(txtMasRotacion.Text).Length = 0 Then
            txtMasRotacion.Text = "0"
        End If

        If Trim(txtMenosRotacion.Text).Length = 0 Then
            txtMenosRotacion.Text = "0"
        End If

        If Trim(txtSinRotacion.Text).Length = 0 Then
            txtSinRotacion.Text = "0"
        End If

        If Integer.TryParse(txtMasCostoso.Text, 0) = False Then
            strMensaje = "Numero de items para tipo Mas precio no permitido."
            Exit Sub
        End If

        If Integer.TryParse(txtMenosCostoso.Text, 0) = False Then
            strMensaje = "Numero de items para tipo Menos precio no permitido."
            Exit Sub
        End If

        If Integer.TryParse(txtMasRotacion.Text, 0) = False Then
            strMensaje = "Numero de items de Mayor rotacion no permitido."
            Exit Sub
        End If

        If Integer.TryParse(txtMenosRotacion.Text, 0) = False Then
            strMensaje = "Numero de items de menor rotacion no permitido."
            Exit Sub
        End If

        If Integer.TryParse(txtSinRotacion.Text, 0) = False Then
            strMensaje = "Numero de items sin rotacion no permitido."
            Exit Sub
        End If

        If Not ddlAlmacenes.SelectedValue.Equals("005") Then
            If CInt(txtMasCostoso.Text) + CInt(txtMenosCostoso.Text) + CInt(txtMasRotacion.Text) + CInt(txtMenosRotacion.Text) + CInt(txtSinRotacion.Text) = 0 Then
                strMensaje = "Para generar el Inventario debe ingresar al menos un item diferente de cero (0)."
                Exit Sub
            End If
        End If

        If ddlAlmacenes.SelectedValue.Equals("005") And rdbParamBusqueda.SelectedValue.Equals("ART") Then
            If Trim(txtArtBusqueda.Text).Length = 0 Then
                strMensaje = "Ingrese el artículo para generar el ERI."
                Exit Sub
            End If
        End If

        If ddlAlmacenes.SelectedValue.Equals("005") And rdbParamBusqueda.SelectedValue.Equals("UBI") Then
            If Trim(txtUbicDesde.Text).Length = 0 Then
                strMensaje = "Ingrese la ubicación de inicio."
                Exit Sub
            End If
            If Trim(txtUbicHasta.Text).Length = 0 Then
                strMensaje = "Ingrese la ubicación de hasta."
                Exit Sub
            End If
        End If
    End Sub

    ' Habilita textos
    Private Sub HabilitaTextos(swEstado As Boolean)
        txtMasCostoso.ReadOnly = swEstado
        txtMenosCostoso.ReadOnly = swEstado
        txtMasRotacion.ReadOnly = swEstado
        txtMenosRotacion.ReadOnly = swEstado
        txtSinRotacion.ReadOnly = swEstado
        'txtTotal.ReadOnly = swEstado
    End Sub

    'Habilita botones
    Private Sub HabilitaBotones(ByVal swGenerar As Boolean, ByVal swGuardar As Boolean, ByVal swEstado As Boolean, ByVal swExportar As Boolean)
        btnGenerar.Enabled = swGenerar
        btnGuardar.Enabled = swGuardar
        btnEstado.Enabled = swEstado
        btnExportar.Enabled = swExportar
    End Sub

    ' exportar reporte
    Private Sub fnc_VerReportePendientes()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strCodigoInventario As String = ""
        Dim strEmpresa As String
        Dim strUnidad As String
        Dim strAlmacen As String

        strEmpresa = Session("@EMPRESA")
        strUnidad = ddlUnidad.SelectedValue.ToString
        strAlmacen = ddlAlmacenes.SelectedValue.ToString

        lblMensaje.Text = ""
        strCodigoInventario = Trim(txtcodinv.Text)
        If strCodigoInventario.Length > 0 Then
            'CAMBIO DG INI
            'strPath = "%2fNM_Reportes%2f"
            'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
            'strURL = strURL + "logistica_InventarioDiario"
            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
            strURL = strURL + strPath + "log_inventario_diario"
            'CAMBIO DG FIN
            strURL = strURL + "&vch_CodInventario=" + strCodigoInventario
            strURL = strURL + "&vch_CodEmpresa=" + strEmpresa
            strURL = strURL + "&vch_CodUnidad=" + strUnidad
            strURL = strURL + "&vch_CodAlmacen=" + strAlmacen

            strURL = strURL + "&rc:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"

            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
        Else
            lblMensaje.Text = "Debe ingresar codigo de inventario para la consulta."
        End If

    End Sub

    'limpiar controles
    Private Sub LimpiarControles()
        txtcodinv.Text = ""
        ddlAlmacenes.SelectedIndex = -1
        ddlUnidad.SelectedIndex = -1
        lblEstado.Text = ""
        lblMensaje.Text = ""
        lblNumReq.Text = ""
        txtMasCostoso.Text = ""
        txtMenosCostoso.Text = ""
        txtMasRotacion.Text = ""
        txtMenosRotacion.Text = ""
        txtSinRotacion.Text = ""
        txtTotal.Text = ""
        dtgInventario.DataSource = Nothing
        dtgInventario.DataBind()
        grdInventarioTelas.DataSource = Nothing
        grdInventarioTelas.DataBind()
        grdInventarioUbic.DataSource = Nothing
        grdInventarioUbic.DataBind()
    End Sub

#End Region


    Dim strIDFilaAnterior As String = String.Empty
    Dim intTotalIndex As Integer = 1
    Dim dblStockSistema As Double = 0.0
    Dim dblStockInventariado As Double = 0.0

    Private Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Call CargaAlmacenes()
    End Sub


    Protected Sub grdInventarioTelas_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInventarioTelas.RowCreated

        Dim blFilaTotalItem As Boolean = False
        If (strIDFilaAnterior <> String.Empty) And (DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem") IsNot Nothing) Then
            If (strIDFilaAnterior <> DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem").ToString()) Then
                blFilaTotalItem = True
            End If
        End If

        If (strIDFilaAnterior <> String.Empty) AndAlso (DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem") Is Nothing) Then
            blFilaTotalItem = True
            intTotalIndex = 0
        End If

        If (strIDFilaAnterior = String.Empty) AndAlso (DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem") IsNot Nothing) Then

            Dim grdViewOrders As GridView = CType(sender, GridView)
            Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
            Dim cell As TableCell = New TableCell()

            cell.Text = "Art. : " + DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem").ToString() + " - " + DataBinder.Eval(e.Row.DataItem, "DE_ITEM").ToString()

            cell.ColumnSpan = 5
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.CssClass = "GroupHeaderStyle"
            cell.BackColor = Drawing.Color.Yellow
            cell.Font.Bold = True
            row.Cells.Add(cell)
            grdInventarioTelas.Controls(0).Controls.AddAt(e.Row.RowIndex + intTotalIndex, row)
            intTotalIndex += 1

        End If

        If blFilaTotalItem Then

            Dim grdViewOrders As GridView = CType(sender, GridView)
            Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
            Dim cell As TableCell = New TableCell()

            cell.Text = "Total Mts."
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.ColumnSpan = 2
            cell.Font.Bold = True
            cell.BackColor = Drawing.Color.Gray
            cell.CssClass = "SubTotalRowStyle"
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.Text = String.Format("{0:N2}", dblStockSistema)
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.Font.Bold = True
            cell.BackColor = Drawing.Color.Gray
            cell.CssClass = "SubTotalRowStyle"
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.Text = String.Format("{0:N2}", dblStockInventariado)
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.Font.Bold = True
            cell.BackColor = Drawing.Color.Gray
            cell.CssClass = "SubTotalRowStyle"
            row.Cells.Add(cell)

            grdInventarioTelas.Controls(0).Controls.AddAt(e.Row.RowIndex + intTotalIndex, row)
            intTotalIndex += 1

            If DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem") IsNot Nothing Then

                row = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
                cell = New TableCell()
                cell.Text = "Art. : " + DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem").ToString() + " - " + DataBinder.Eval(e.Row.DataItem, "DE_ITEM").ToString()
                cell.ColumnSpan = 5
                cell.HorizontalAlign = HorizontalAlign.Left
                cell.BackColor = Drawing.Color.Yellow
                cell.Font.Bold = True
                cell.CssClass = "GroupHeaderStyle"
                row.Cells.Add(cell)
                grdInventarioTelas.Controls(0).Controls.AddAt(e.Row.RowIndex + intTotalIndex, row)
                intTotalIndex += 1

            End If

            dblStockSistema = 0.0
            dblStockInventariado = 0.0

        End If

    End Sub

    Protected Sub grdInventarioTelas_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInventarioTelas.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            strIDFilaAnterior = DataBinder.Eval(e.Row.DataItem, "vch_CodigoItem").ToString()
            Dim ldblStockSistema As Double = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "num_StockInventario").ToString())
            Dim ldblStockInventariado As Double = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "num_CanInventario").ToString())
            Dim lblEstado As Label = CType(e.Row.FindControl("lblEstadoUbic"), Label)

            If lblEstado.Text.Equals("OK") Then
                'lblEstado.Text = ""
                lblEstado.Text = "OK"
                lblEstado.ForeColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.Green
            ElseIf lblEstado.Text.Equals("KO") Then
                'lblEstado.Text = ""
                lblEstado.Text = "FALTANTE"
                lblEstado.ForeColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.Red
            ElseIf lblEstado.Text.Equals("SO") Then
                'lblEstado.Text = ""
                lblEstado.Text = "SOBRANTE"
                lblEstado.ForeColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.Blue
            Else
                lblEstado.BackColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.White
            End If


            dblStockSistema += ldblStockSistema
            dblStockInventariado += ldblStockInventariado

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''")
                e.Row.Attributes.Add("style", "cursor:pointer;")

            End If

        End If


    End Sub

    Protected Sub rdbParamBusqueda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdbParamBusqueda.SelectedIndexChanged
        If rdbParamBusqueda.SelectedValue.Equals("UBI") Then
            lblInfoMasCos.Visible = False
            lblInfoMenosCos.Visible = False
            lblInfoMasRot.Visible = False
            lblInfoMenosRot.Visible = False
            lblInfoSinRot.Visible = False
            lblInfoTotal.Visible = False

            txtMasCostoso.Visible = False
            txtMenosCostoso.Visible = False
            txtMasRotacion.Visible = False
            txtMenosRotacion.Visible = False
            txtSinRotacion.Visible = False
            txtTotal.Visible = False


            lblInfoArt.Visible = False
            txtArtBusqueda.Visible = False
            btnConsultarArt.Visible = False

            lblInfoDesde.Visible = True
            txtUbicDesde.Visible = True
            lblInfoHasta.Visible = True
            txtUbicHasta.Visible = True
        Else
            lblInfoMasCos.Visible = True
            lblInfoMenosCos.Visible = True
            lblInfoMasRot.Visible = True
            lblInfoMenosRot.Visible = True
            lblInfoSinRot.Visible = True
            lblInfoTotal.Visible = True

            txtMasCostoso.Visible = True
            txtMenosCostoso.Visible = True
            txtMasRotacion.Visible = True
            txtMenosRotacion.Visible = True
            txtSinRotacion.Visible = True
            txtTotal.Visible = True

            lblInfoArt.Visible = True
            txtArtBusqueda.Visible = True
            btnConsultarArt.Visible = True

            lblInfoDesde.Visible = False
            txtUbicDesde.Visible = False
            lblInfoHasta.Visible = False
            txtUbicHasta.Visible = False
        End If
    End Sub




    Protected Sub ddlAlmacenes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAlmacenes.SelectedIndexChanged
        If ddlAlmacenes.SelectedValue.Equals("005") Then
            rdbParamBusqueda.Visible = True
            rdbParamBusqueda.SelectedValue = "ART"
            lblInfoMasCos.Visible = True
            lblInfoMenosCos.Visible = True
            lblInfoMasRot.Visible = True
            lblInfoMenosRot.Visible = True
            lblInfoSinRot.Visible = True
            lblInfoTotal.Visible = True

            txtMasCostoso.Visible = True
            txtMenosCostoso.Visible = True
            txtMasRotacion.Visible = True
            txtMenosRotacion.Visible = True
            txtSinRotacion.Visible = True
            txtTotal.Visible = True

            lblInfoArt.Visible = True
            txtArtBusqueda.Visible = True
            btnConsultarArt.Visible = True

            lblInfoDesde.Visible = False
            txtUbicDesde.Visible = False
            lblInfoHasta.Visible = False
            txtUbicHasta.Visible = False
        Else
            rdbParamBusqueda.Visible = False
            lblInfoMasCos.Visible = True
            lblInfoMenosCos.Visible = True
            lblInfoMasRot.Visible = True
            lblInfoMenosRot.Visible = True
            lblInfoSinRot.Visible = True
            lblInfoTotal.Visible = True

            txtMasCostoso.Visible = True
            txtMenosCostoso.Visible = True
            txtMasRotacion.Visible = True
            txtMenosRotacion.Visible = True
            txtSinRotacion.Visible = True
            txtTotal.Visible = True

            lblInfoArt.Visible = False
            txtArtBusqueda.Visible = False
            btnConsultarArt.Visible = False

            lblInfoDesde.Visible = False
            txtUbicDesde.Visible = False
            lblInfoHasta.Visible = False
            txtUbicHasta.Visible = False
        End If
    End Sub


    Dim strIDFilaAnterior_2 As String = String.Empty
    Dim intTotalIndex_2 As Integer = 1
    Dim dblStockSistema_2 As Double = 0.0
    Dim dblStockInventariado_2 As Double = 0.0

    Protected Sub grdInventarioUbic_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInventarioUbic.RowCreated
        Dim blFilaTotalItem As Boolean = False
        If (strIDFilaAnterior_2 <> String.Empty) And (DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion") IsNot Nothing) Then
            If (strIDFilaAnterior_2 <> DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString()) Then
                blFilaTotalItem = True
            End If
        End If

        If (strIDFilaAnterior_2 <> String.Empty) AndAlso (DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion") Is Nothing) Then
            blFilaTotalItem = True
            intTotalIndex_2 = 0
        End If

        If (strIDFilaAnterior_2 = String.Empty) AndAlso (DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion") IsNot Nothing) Then

            Dim grdViewOrders As GridView = CType(sender, GridView)
            Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
            Dim cell As TableCell = New TableCell()

            'cell.Text = "Ubicacion : <a href='#' onClick='window.open(frm_InventarioTelasERI_Detalle.aspx?vch_CodUbicacion=" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + ",'nombrePop-Up', 'width=380,height=500, top=85,left=50');'>" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "</a>"
            'cell.Text = "Ubicacion : <a href='frm_InventarioTelasERI_Detalle.aspx?vch_CodUbicacion=" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "' target='_blank' onclick='window.open(this.href, this.target, 'width=300,height=400'); return false;'>" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "</a>"
            cell.Text = "Ubicacion : <a onclick='window.showModalDialog(""frm_InventarioTelasERI_Detalle.aspx?vch_CodInventario=" + txtcodinv.Text.Trim + "&vch_CodUbicacion=" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + """, """", ""dialogheight:450px;dialogwidth:690px;center:yes;help:no;"");'>" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "</a>"

            cell.ColumnSpan = 5
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.CssClass = "GroupHeaderStyle"
            cell.BackColor = Drawing.Color.Yellow
            cell.Font.Bold = True
            row.Cells.Add(cell)

            grdInventarioUbic.Controls(0).Controls.AddAt(e.Row.RowIndex + intTotalIndex_2, row)
            intTotalIndex_2 += 1

        End If

        If blFilaTotalItem Then

            Dim grdViewOrders As GridView = CType(sender, GridView)
            Dim row As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
            Dim cell As TableCell = New TableCell()

            cell.Text = "Total Mts."
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.ColumnSpan = 2
            cell.Font.Bold = True
            cell.BackColor = Drawing.Color.Gray
            cell.CssClass = "SubTotalRowStyle"
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.Text = String.Format("{0:N2}", dblStockSistema_2)
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.Font.Bold = True
            cell.BackColor = Drawing.Color.Gray
            cell.CssClass = "SubTotalRowStyle"
            row.Cells.Add(cell)

            cell = New TableCell()
            cell.Text = String.Format("{0:N2}", dblStockInventariado_2)
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.Font.Bold = True
            cell.BackColor = Drawing.Color.Gray
            cell.CssClass = "SubTotalRowStyle"
            row.Cells.Add(cell)

            grdInventarioUbic.Controls(0).Controls.AddAt(e.Row.RowIndex + intTotalIndex_2, row)
            intTotalIndex_2 += 1

            If DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion") IsNot Nothing Then

                row = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
                cell = New TableCell()
                'cell.Text = "Ubicacion : <a href='#' onClick='window.open(frm_InventarioTelasERI_Detalle.aspx?vch_CodUbicacion=" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "','nombrePop-Up', 'width=380,height=500, top=85,left=50');'>" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "</a>"
                'cell.Text = "Ubicacion : <a href='frm_InventarioTelasERI_Detalle.aspx?vch_CodUbicacion=" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "' target='_blank' onclick='window.open(this.href, this.target, 'width=300,height=400'); return false;'>" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "</a>"
                cell.Text = "Ubicacion : <a onclick='window.showModalDialog(""frm_InventarioTelasERI_Detalle.aspx?vch_CodInventario=" + txtcodinv.Text.Trim + "&vch_CodUbicacion=" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + """, """", ""dialogheight:450px;dialogwidth:690px;center:yes;help:no;"");'>" + DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString() + "</a>"
                cell.ColumnSpan = 5
                cell.HorizontalAlign = HorizontalAlign.Left
                cell.BackColor = Drawing.Color.Yellow
                cell.Font.Bold = True
                cell.CssClass = "GroupHeaderStyle"
                row.Cells.Add(cell)

                grdInventarioUbic.Controls(0).Controls.AddAt(e.Row.RowIndex + intTotalIndex_2, row)
                intTotalIndex_2 += 1

            End If

            dblStockSistema_2 = 0.0
            dblStockInventariado_2 = 0.0

        End If
    End Sub

    Protected Sub grdInventarioUbic_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInventarioUbic.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            strIDFilaAnterior_2 = DataBinder.Eval(e.Row.DataItem, "vch_CodigoUbicacion").ToString()
            Dim ldblStockSistema As Double = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "num_StockInventario").ToString())
            Dim ldblStockInventariado As Double = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "num_CanInventario").ToString())
            Dim lblEstado As Label = CType(e.Row.FindControl("lblEstadoUbic"), Label)

            If lblEstado.Text.Equals("OK") Then
                'lblEstado.Text = ""
                lblEstado.Text = "OK"
                lblEstado.ForeColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.Green
            ElseIf lblEstado.Text.Equals("KO") Then
                'lblEstado.Text = ""
                lblEstado.Text = "FALTANTE"
                lblEstado.ForeColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.Red
            ElseIf lblEstado.Text.Equals("SO") Then
                'lblEstado.Text = ""
                lblEstado.Text = "SOBRANTE"
                lblEstado.ForeColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.Blue
            Else
                lblEstado.BackColor = Drawing.Color.White
                lblEstado.BackColor = Drawing.Color.White
            End If


            dblStockSistema_2 += ldblStockSistema
            dblStockInventariado_2 += ldblStockInventariado

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'")
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''")
                e.Row.Attributes.Add("style", "cursor:pointer;")

            End If

        End If
    End Sub
End Class