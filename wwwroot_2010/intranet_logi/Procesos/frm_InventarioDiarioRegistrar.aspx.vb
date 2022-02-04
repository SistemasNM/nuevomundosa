Public Class frm_InventarioDiarioRegistrar

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
                    Call ConsultaInventarioDiario(2, strCodigoInventario)
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
            If strCodigoInventario.Length = 10 Then
                fnc_VerReportePendientes()
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

        dtAlmacenes = objArticulo.InventarioDiario_Listado_Almacenes(strEmpresa, strUnidad)
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


        If CInt(txtMasCostoso.Text) + CInt(txtMenosCostoso.Text) + CInt(txtMasRotacion.Text) + CInt(txtMenosRotacion.Text) + CInt(txtSinRotacion.Text) = 0 Then
            strMensaje = "Para generar el Inventario debe ingresar al menos un item diferente de cero (0)."
            Exit Sub
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
    End Sub

#End Region

    Private Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Call CargaAlmacenes()
    End Sub

End Class