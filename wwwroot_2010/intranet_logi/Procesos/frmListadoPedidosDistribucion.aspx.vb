Imports NuevoMundo

Public Class frmListadoPedidosDistribucion
    Inherits System.Web.UI.Page

    Sub fnListadoPedidosDistribucion(ByVal pstrCodArt As String)
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtResponse As DataTable

        dtResponse = objPedidos.fnListadoArticulosDistrib(pstrCodArt)
        Session("dtDistDetalle") = dtResponse
        grvListArtDist.DataSource = dtResponse
        grvListArtDist.DataBind()

    End Sub

    Private Sub frmListadoPedidosDistribucion_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        'Session("@USUARIO") = "OBLAS"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.DistTotal.Visible = True
            Me.lblTituloCodigoArt.Visible = False
            Me.IbtnRefrescarSvsP.Visible = False
            Me.lblTituloStockvsPedido.Visible = False
            Me.lblTotalDistribSinProg.Visible = False
            Me.lblTituloTotalDistribSinProg.Visible = False
            Me.BtnEnviar.Visible = False
            Me.BtnConcluir.Visible = False
            Me.BtnLimpiarProgramados.Visible = False
            Me.EsMostrar.Visible = False
            Me.BtnLimpiarProgramados.Enabled = False
            Dim dtConsultaItems = New DataTable
            Dim objPedidos As New Logistica.clsPedidos
            dtConsultaItems = objPedidos.fnListadoItemsModuloDistribucion()

            combobox.TextField = "CO_ITEM"
            combobox.ValueField = "CO_ITEM"
            combobox.DataSource = dtConsultaItems
            combobox.DataBind()
            combobox.AutoFilterQueryType = Infragistics.Web.UI.ListControls.AutoFilterQueryTypes.Contains
            combobox.CurrentValue = ""

        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim strCodigoArt As String = combobox.CurrentValue
        fnListadoPedidosDistribucion(strCodigoArt)
        'fnListadoPedidosDistribucion(txtArtSAsoc.Text.Trim())
        Me.lblCantidad.Text = grvListArtDist.Rows.Count

        If grvListArtDist.Rows.Count > 0 Then
            Me.BtnEnviar.Visible = True
            Me.BtnConcluir.Visible = True
            Me.BtnLimpiarProgramados.Visible = True
            Me.EsMostrar.Visible = True
            Me.DistTotal.Visible = True
            Me.lblTituloCodigoArt.Visible = True
            Me.lblTituloStockvsPedido.Visible = True
            Me.lblTituloTotalDistribSinProg.Visible = True
            Me.lblTotalDistribSinProg.Visible = True
            Me.IbtnRefrescarSvsP.Visible = True
            lblStock.Visible = False
            lblCodigoArt.Visible = True
            lblCodigoArt.Text = CType(grvListArtDist.Rows(0).FindControl("lblArticulo"), Label).Text
            Dim objPedidos As New Logistica.clsPedidos
            Dim dtStockvsPedido As DataTable = objPedidos.fnStockVSPedidoDistrib(strCodigoArt)
            lblStockvsPedido.Text = dtStockvsPedido.Rows(0).ItemArray(2).ToString()
            Dim dtTotalDistribSinProg As DataTable = objPedidos.fnSumaTotalPedidoDistrib_SinProgramacion(strCodigoArt)
            lblTotalDistribSinProg.Text = dtTotalDistribSinProg.Rows(0).ItemArray(0).ToString()
        End If
        If grvListArtDist.Rows.Count = 0 Then
            Me.EsMostrar.Visible = False
            Me.DistTotal.Visible = False
            Me.lblTituloCodigoArt.Visible = False
            lblStock.Visible = True
            lblCodigoArt.Visible = False
            BtnEnviar.Visible = False
            BtnConcluir.Visible = False
            BtnLimpiarProgramados.Visible = False
            lblStock.Text = "No se ha encontrado pedido con este artículo."
        End If

        lblPedidoConcluido.Visible = False
        DivSinConcluir.Visible = False
    End Sub



    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        'Para validar mts disponibles
        Dim sumaDisponible As Double = 0
        Dim valorDisponible As Double

        'Para usar el codigo de Articulo
        Dim strCoItem As String
        strCoItem = String.Empty

        If grvListArtDist.Rows().Count > 0 Then
            Dim datos As Label
            datos = CType(grvListArtDist.Rows(0).FindControl("lblDatos"), Label)
            Dim sDatos() As String = datos.Text.Split("|")
            valorDisponible = Convert.ToDouble(sDatos(3))
        End If

        Dim totalMtsR As Double

        For i = 0 To grvListArtDist.Rows.Count - 1
            Dim mtsRepartido As TextBox = CType(grvListArtDist.Rows(i).FindControl("txtMtsRepartir"), TextBox)
            Dim dblMtsR As Double = 0
            If Not String.IsNullOrEmpty(mtsRepartido.Text) Then
                dblMtsR = Convert.ToDouble(mtsRepartido.Text.Trim())
                totalMtsR = dblMtsR + totalMtsR
            Else
                dblMtsR = 0.0
                totalMtsR = dblMtsR + totalMtsR
            End If
        Next

        If totalMtsR = 0 Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", "alert('No ha ingresa metros por distribuir.');", True)
            Exit Sub
        End If

        For i = 0 To grvListArtDist.Rows.Count - 1
            Dim datos As Label
            Dim mtsRepartido As TextBox
            datos = CType(grvListArtDist.Rows(i).FindControl("lblDatos"), Label)
            Dim strDatos() As String = datos.Text.Split("|")
            Dim strNuPedi As String = strDatos(0)
            strCoItem = strDatos(1)
            Dim mtspen As Double = Convert.ToDouble(CType(grvListArtDist.Rows(i).FindControl("lblCaPend"), Label).Text)
            Dim mtsdisp As Double = Convert.ToDouble(strDatos(3))
            Dim mtspedido As Double = Convert.ToDouble(strDatos(4))
            mtsRepartido = CType(grvListArtDist.Rows(i).FindControl("txtMtsRepartir"), TextBox)

            Dim dblMtsR As Double
            If Not String.IsNullOrEmpty(mtsRepartido.Text) Then
                dblMtsR = Convert.ToDouble(mtsRepartido.Text.Trim())
            Else
                dblMtsR = 0.0
            End If

            If mtspen < 0 Then
                mtspen = 0
            End If

            If dblMtsR > mtspen Then
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", "alert('Debe ingresar una cantidad menor a la pendiente.');", True)
                Exit Sub
            End If
        Next

        For i = 0 To grvListArtDist.Rows.Count - 1
            Dim mtsRepartido As TextBox = CType(grvListArtDist.Rows(i).FindControl("txtMtsRepartir"), TextBox)
            Dim datos As Label = CType(grvListArtDist.Rows(i).FindControl("lblDatos"), Label)
            Dim strDatos() As String = datos.Text.Split("|")
            Dim mtsdisp As Double = Convert.ToDouble(strDatos(3))
            Dim dblMtsR As Double
            If Not String.IsNullOrEmpty(mtsRepartido.Text.Trim()) Then
                dblMtsR = Convert.ToDouble(mtsRepartido.Text.Trim())
            Else
                dblMtsR = 0.0
            End If

            sumaDisponible = sumaDisponible + dblMtsR
        Next

        If sumaDisponible > valorDisponible Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", "alert('Ha excedido los metros Disponibles.');", True)
            Exit Sub
        End If

        'Crear ArraysList (Para mensaje de clientes con línea de crédito)
        Dim lisLineCreditoTmp As New ArrayList()
        For i = 0 To grvListArtDist.Rows.Count - 1
            Dim datos As Label
            Dim mtsRepartido As TextBox
            datos = CType(grvListArtDist.Rows(i).FindControl("lblDatos"), Label)
            Dim strDatos() As String = datos.Text.Split("|")
            Dim strNuPedi As String = strDatos(0)
            strCoItem = strDatos(1)
            Dim mtspen As Double = Convert.ToDouble(CType(grvListArtDist.Rows(i).FindControl("lblCaPend"), Label).Text)
            Dim mtsdisp As Double = Convert.ToDouble(strDatos(3))
            Dim mtspedido As Double = Convert.ToDouble(strDatos(4))
            Dim idDistribucion As Integer = strDatos(5)
            Dim coCliente As String = strDatos(6)
            mtsRepartido = CType(grvListArtDist.Rows(i).FindControl("txtMtsRepartir"), TextBox)

            Dim dblMtsR As Double
            If Not String.IsNullOrEmpty(mtsRepartido.Text.Trim()) Then
                dblMtsR = Convert.ToDouble(mtsRepartido.Text.Trim())
            Else
                dblMtsR = 0.0
            End If

            Dim mtsdist As Double = Convert.ToDouble(CType(grvListArtDist.Rows(i).FindControl("lblCaDistribuidos"), Label).Text)

            'REGISTRO DE ATENTIDIDOS
            Dim mtsAtendidos As Double = Convert.ToDouble(CType(grvListArtDist.Rows(i).FindControl("lblCaAten"), Label).Text) + dblMtsR

            Dim objPedidos As New Logistica.clsPedidos
            Dim dtResponse As DataTable
            'Dim dtResponseStock As DataTable
            'Dim dtResponseLineCredito As DataTable

            Dim mtsPendientes As Double

            mtsPendientes = mtspedido - ((mtspedido - mtspen) + dblMtsR)

            Dim esTotal As Boolean = False
            If dblMtsR <> 0 Then
                esTotal = True
            End If

            If mtsdist = 0 Then
                dtResponse = objPedidos.fnGuardarPedidoDistribucion(strNuPedi, strCoItem, dblMtsR, Session("@USUARIO"), mtspedido, mtsPendientes, mtsAtendidos, dblMtsR, esTotal, idDistribucion)
            Else
                If dblMtsR <> mtsdist Then
                    If dblMtsR <> 0 Then
                        dtResponse = objPedidos.fnGuardarPedidoDistribucion(strNuPedi, strCoItem, dblMtsR, Session("@USUARIO"), mtspedido, mtsPendientes, mtsAtendidos, dblMtsR, esTotal, idDistribucion)
                    Else
                        dtResponse = objPedidos.fnGuardarPedidoDistribucion(strNuPedi, strCoItem, dblMtsR, Session("@USUARIO"), mtspedido, mtsPendientes, mtsAtendidos, mtsdist, esTotal, idDistribucion)
                    End If
                Else
                    dtResponse = objPedidos.fnGuardarPedidoDistribucion(strNuPedi, strCoItem, dblMtsR, Session("@USUARIO"), mtspedido, mtsPendientes, mtsAtendidos, mtsdist, esTotal, idDistribucion)
                End If
            End If

        Next
        'Datos para enviar correo
        Dim descpArticulo As String = CType(grvListArtDist.Rows(0).FindControl("lblArticulo"), Label).Text
        Call EnviarEmailLogistica_V2(strCoItem, descpArticulo, sumaDisponible)
        'Call EnviarEmailLogistica_V2("15601761510100000005", descpArticulo, 2786)


        Dim strCodigoArt As String = combobox.CurrentValue
        fnListadoPedidosDistribucion(strCodigoArt)

        If grvListArtDist.Rows.Count = 0 Then
            Me.EsMostrar.Visible = False
        End If

        Me.DistTotal.Visible = False
        Me.lblTituloCodigoArt.Visible = False
    End Sub

    'Enviar correo electronico a Logistica
    Private Sub EnviarEmailLogistica_V2(ByVal strCodItem As String, ByVal strDescArticulo As String, ByVal mtsDistribuidos As Double)
        Dim objCorreo As New clsCorreo
        Dim lstrMailTO As String
        Dim lstrMailCC As String
        Dim lstrMailBCC As String = ""
        Dim lstrMailSubject As String
        Dim lstrMailBody As String = ""

        Try
            lstrMailTO = System.Web.Configuration.WebConfigurationManager.AppSettings("CopiaCorreoModuloDistribucion").ToString()
            lstrMailCC = System.Web.Configuration.WebConfigurationManager.AppSettings("CopiaCorreoModuloDistribucionCC").ToString()
            lstrMailBCC = System.Web.Configuration.WebConfigurationManager.AppSettings("CopiaCorreoModuloDistribucionBCC").ToString()
            lstrMailSubject = strCodItem + " - Se ha generado una Distribución"
            lstrMailBody &= "<P style='FONT-SIZE: 13px; FONT-FAMILY: Verdana'>Estimad@s,&nbsp;<BR>Se ha generado una distribución.<BR></P>"
            lstrMailBody &= "<P style='FONT-SIZE: 13px; FONT-FAMILY: Verdana'>Para el artículo: " + strDescArticulo + "." + "</P>"
            lstrMailBody &= "</br>"
            lstrMailBody &= "<table border='1' style='FONT-SIZE: 13px; FONT-FAMILY: Verdana;'>"
            lstrMailBody &= "<TR>"
            lstrMailBody &= "<TH style='text-align: left;'>Número de Pedido</TH>"
            lstrMailBody &= "<TH style='text-align: left;'>Razón Social</TH>"
            lstrMailBody &= "<TH>Metros Distribuidos</TH>"
            lstrMailBody &= "</TR>"
            For i = 0 To grvListArtDist.Rows.Count - 1
                Dim mtsRepartido As String = CType(grvListArtDist.Rows(i).FindControl("txtMtsRepartir"), TextBox).Text
                Dim no_cliente As String = CType(grvListArtDist.Rows(i).FindControl("lblNoClie"), Label).Text
                Dim nu_pedido As String = CType(grvListArtDist.Rows(i).FindControl("lblNumeroPedido"), Label).Text
                Dim mtsRepar As Double
                If Not String.IsNullOrEmpty(mtsRepartido) Then
                    mtsRepar = Convert.ToDouble(mtsRepartido)
                Else
                    mtsRepar = 0
                    mtsRepartido = "0"
                End If

                If mtsRepar > 0 Then
                    lstrMailBody &= "<TR>"
                    lstrMailBody &= "<TD>" + nu_pedido + "</TD>"
                    lstrMailBody &= "<TD>" + no_cliente + "</TD>"
                    lstrMailBody &= "<TD  style='text-align: right; background:yellow;'>" + mtsRepartido + "mts." + "</TD>"
                    lstrMailBody &= "</TR>"
                End If
            Next
            lstrMailBody &= "</table>"
            lstrMailBody &= "<P>Suma total distribuido: " + mtsDistribuidos.ToString + ".</P>"
            lstrMailBody &= "</br>"
            lstrMailBody &= "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'> Comuniquese con el Area de Ventas. <BR><BR>"
            lstrMailBody &= "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>"
            lstrMailBody &= "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>"
            lstrMailBody &= "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>"
            lstrMailBody &= "Este correo ha sido generado automáticamente.<BR>"
            lstrMailBody &= "Por favor no responder este correo.<BR>"
            lstrMailBody &= "Departamento de Sistemas<BR>"
            lstrMailBody &= "Cía. Industrial Nuevo Mundo S.A.<BR>"
            lstrMailBody &= "-------------------------------------------------------------------------------</P>"


            objCorreo.ServicioEnvioCorreo(lstrMailTO, lstrMailCC, lstrMailBCC, lstrMailSubject, lstrMailBody)

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub grvListArtDist_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvListArtDist.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txt As TextBox = e.Row.FindControl("txtMtsRepartir")
            txt.Attributes.Add("onBlur", "JvfunonBlur();")
        End If

    End Sub

    Protected Sub BtnConcluir_Click(sender As Object, e As EventArgs) Handles BtnConcluir.Click
        Dim objPedidos As New Logistica.clsPedidos
        'Para la info de pdidos concluidos
        Dim ccantidad As Integer
        Dim lisPedidoConcluidoTmp As New ArrayList()
        Dim lisPedidoSinConcluidoTmp As New ArrayList()

        Dim conChecked As Integer = 0

        For i = 0 To grvListArtDist.Rows.Count - 1
            Dim mtsRepartido As TextBox = CType(grvListArtDist.Rows(i).FindControl("txtMtsRepartir"), TextBox)
            Dim dblMtsR As Double = Convert.ToDouble(mtsRepartido.Text.Trim())
            Dim numero_pedido As String = CType(grvListArtDist.Rows(i).FindControl("lblNumeroPedido"), Label).Text
            Dim cantidad_pendiente As String = CType(grvListArtDist.Rows(i).FindControl("lblCaPend"), Label).Text

            Dim check As CheckBox = CType(grvListArtDist.Rows(i).FindControl("chkSeleccion"), CheckBox)
            If check.Checked Then
                If Not String.IsNullOrEmpty(cantidad_pendiente) Then
                    'If (Convert.ToDouble(cantidad_pendiente) - dblMtsR) <= 400 Then
                    dtRespuestaConcluir = objPedidos.fnConcluirPedidoDistrib(numero_pedido) 'numero_pedido
                    If dtRespuestaConcluir.Rows(0).ItemArray(0).ToString() = "OK" Then
                        lisPedidoConcluidoTmp.Add(numero_pedido)
                        ccantidad = ccantidad + 1
                        conChecked = conChecked + 1
                    Else
                        lisPedidoSinConcluidoTmp.Add(dtRespuestaConcluir.Rows(0).ItemArray(0).ToString())
                    End If
                    'End If
                End If
            End If
        Next

        If conChecked = 0 Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", "alert('No ha marcado check al pedido que desea concluir o anular.');", True)
            Exit Sub
        End If


        Dim lisPedidoConcluido() As Object = lisPedidoConcluidoTmp.ToArray()
        If Not String.IsNullOrEmpty(String.Join(", ", lisPedidoConcluido.Distinct())) Then
            lblPedidoConcluido.Text = "Se ha concluido " + ccantidad.ToString + " pedido(S) - " + String.Join(", ", lisPedidoConcluido.Distinct()) + "."
            Me.lblPedidoConcluido.Visible = True
        Else
            Me.lblPedidoConcluido.Visible = False
        End If

        Dim lisPedidoSinConcluido() As Object = lisPedidoSinConcluidoTmp.ToArray()
        If Not String.IsNullOrEmpty(String.Join(", ", lisPedidoSinConcluido.Distinct())) Then
            lblSinConcluir.Text = "No se ha concluido lo(S) Pedido(s) - " + String.Join(", ", lisPedidoSinConcluido.Distinct()) + " por que tiene otros artículos enlazados, se necesita concluirlo de manera manual."
            lblSinConcluir.Visible = True
        Else
            lblSinConcluir.Visible = False
        End If
        Dim strCodigoArt As String = combobox.CurrentValue
        fnListadoPedidosDistribucion(strCodigoArt)
    End Sub

    Protected Sub BtnLimpiarProgramados_Click(sender As Object, e As EventArgs) Handles BtnLimpiarProgramados.Click
        Dim strCodigoArt As String = combobox.CurrentValue
        Dim objPedidos As New Logistica.clsPedidos

        objPedidos.fnLimpiarPedidoDistrib(strCodigoArt)

        fnListadoPedidosDistribucion(strCodigoArt)
    End Sub

    Protected Sub IbtnRefrescarSvsP_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles IbtnRefrescarSvsP.Click
        Dim strCodigoArt As String = combobox.CurrentValue
        Dim objPedidos As New Logistica.clsPedidos
        Dim dtStockvsPedido As DataTable = objPedidos.fnStockVSPedidoDistrib(strCodigoArt)
        lblStockvsPedido.Text = dtStockvsPedido.Rows(0).ItemArray(2).ToString()

        fnListadoPedidosDistribucion(strCodigoArt)

        Me.BtnLimpiarProgramados.Enabled = True
    End Sub
End Class