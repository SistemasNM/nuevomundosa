Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing


Public Class frm_ListadoValesPedido
    Inherits System.Web.UI.Page

    Dim strVale_NumDocu As String
    Dim strVale_TipoDocu As String
    Dim strVale_CodAlm As String
    Dim strVale_CodEmp As String
    'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
    Dim strVale_Serie As String
    'ACTUALIZAR PARA PEDIDOS 0005 - DG - FIN

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgListaVales As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblMsgError As System.Web.UI.WebControls.Label
    Protected WithEvents pnlDetalle As System.Web.UI.WebControls.Panel
    Protected WithEvents dgDetalleVale As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDetalle As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not (Page.IsPostBack) Then
            Try
                If (Not Request.Item("strNumeroPedido") Is Nothing) Then
                    CargarValesPedido(Request.Item("strNumeroPedido"))
                    pnlDetalle.Visible = False
                End If
            Catch ex As Exception
                lblMsgError.Text = "Ha ocurrido un error a listar vales, comuniquese con sistemas"
            End Try
        Else
        End If
    End Sub

    Private Sub CargarValesPedido(ByVal strNumeroPedido As String)
        If strNumeroPedido.Length > 0 Then
            Dim objPedido As Logistica.clsPedidos
            Dim dtbVales As DataTable
            objPedido = New Logistica.clsPedidos
            Try
                dtbVales = objPedido.fncConsultaValesPedido(strNumeroPedido)
                'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
                strVale_Serie = Left(strNumeroPedido, 4)
                'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
                If dtbVales.Rows.Count > 0 Then
                    dgListaVales.DataSource = dtbVales
                    dgListaVales.DataBind()
                Else
                    lblMsgError.Text = "No Registra Vales de Salida de Almacen"
                End If
            Catch ex As Exception
                lblMsgError.Text = ex.ToString
            End Try
        End If
    End Sub

    Private Sub dgListaVales_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListaVales.ItemCommand
        Dim lobjVale As Label = CType(e.Item.FindControl("lblVale"), Label)
        Dim lobjNumDocu As Label = CType(e.Item.FindControl("lblNumDocu"), Label)
        Dim lobjTipoDocu As Label = CType(e.Item.FindControl("lblTipoDocu"), Label)
        Dim lobjAlmacen As Label = CType(e.Item.FindControl("lblAlmacen"), Label)
        Dim lobjEmpresa As Label = CType(e.Item.FindControl("lblEmpresa"), Label)

        Select Case e.CommandName
            Case "Editar"
                lblMsgError.Text = ""
                lblMsgError.Visible = False
                Dim strNumeroVale As String
                Dim objPedido As Logistica.clsPedidos
                Dim dbtDetalleVale As DataTable
                strNumeroVale = Mid(lobjVale.Text, 4, 20)
                objPedido = New Logistica.clsPedidos
                Try
                    dbtDetalleVale = objPedido.fnc_ValeDetalle(strNumeroVale)
                    If dbtDetalleVale.Rows.Count > 0 Then
                        dgDetalleVale.DataSource = dbtDetalleVale
                        dgDetalleVale.DataBind()
                        pnlDetalle.Visible = True
                    Else
                        pnlDetalle.Visible = False
                        lblMsgError.Text = "No existen detalle para este Vale de Salida"
                        lblMsgError.Visible = True
                    End If
                Catch ex As Exception
                    lblMsgError.Text = ex.ToString
                End Try
            Case "Imprimir"
                strVale_NumDocu = lobjNumDocu.Text
                strVale_TipoDocu = lobjTipoDocu.Text
                strVale_CodAlm = lobjAlmacen.Text
                strVale_CodEmp = lobjEmpresa.Text

                Call ImprimirValesGenerados()
                If (Not Request.Item("strNumeroPedido") Is Nothing) Then
                    CargarValesPedido(Request.Item("strNumeroPedido"))
                    pnlDetalle.Visible = False
                End If
        End Select
    End Sub

    Private Sub ImprimirValesGenerados()
        Dim lobjDocumentoImp As New PrintDocument, lstrImpresora As String
        'Dim ldtbResultado As New DataTable
        Dim lobjLogistica As New Logistica.clsPedidos        
        Dim strResult As Integer

        Try
            'OBTENER EL NOMBRE DE LA IMPRESORA CODIGO DE TABLA GENERAL ##
            If strVale_CodAlm = "002" Then 'Impresora Almacen
                lstrImpresora = "\\ALMPRODQUIM\POS_ALM_PQ" '"\\ALMPRINCIPAL\EPSON"
            Else
                lstrImpresora = "POS_ALM_LOG" ' "\\SERVNMPRB\POS_ALM_LOG" 
            End If

            lobjDocumentoImp.PrinterSettings.PrinterName = lstrImpresora
            lobjDocumentoImp.DefaultPageSettings.Landscape = False

            AddHandler lobjDocumentoImp.PrintPage, AddressOf lobjDocumentoImp_PrintThePage
            lobjDocumentoImp.Print()
            lobjDocumentoImp = Nothing

            strResult = lobjLogistica.ufn_ActualizaDocumentoImpreso(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & ex.Message & "');</script>")

        End Try
    End Sub

    Private Sub lobjDocumentoImp_PrintThePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim lobjLogistica As New Logistica.clsPedidos
        Dim lblnImpresionOK As Boolean = False
        Dim dtDocumento As New DataTable

        Try
            'ACTUALIZAR PARA PEDIDOS 0005 - DG - INI
            'dtDocumento = lobjLogistica.ufn_BuscaDatosDocumentos(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)
            If strVale_Serie = "0005" Then
                dtDocumento = lobjLogistica.ufn_BuscaDatosDocumentosEPPsOtros(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)
            Else
                dtDocumento = lobjLogistica.ufn_BuscaDatosDocumentos(strVale_NumDocu, strVale_TipoDocu, strVale_CodAlm, strVale_CodEmp)
            End If
            'ACTUALIZAR PARA PEDIDOS 0005 - DG - FIN

            If dtDocumento.Rows.Count > 0 Then
                Dim lobjPencil As Pen = New Pen(System.Drawing.Color.Black, 0.5)
                Dim lobjFuente As Font
                Dim format As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
                Dim FontName As String = "Courier New"
                '------------------------------
                'Dim strNombreDocu As String = ""
                Dim strAuxiliar As String = ""
                Dim strObservaciones As String = ""
                Dim intPosY As Integer
                Dim dblTotalCant As Double
                Dim dblTotalAlterno As Double
                Dim intTotalItem As Integer
                Dim strNumeroDocu As String
                Dim strUsuario_Crea As String
                Dim strDescripcion As String
                '------------------------------

                dtDocumento.TableName = "Documento"
                intPosY = 0
                '------------------------------------------------------------------------------
                'BEGIN: CONFIGURAR DISEÑO DOCUMENTO
                '------------------------------------------------------------------------------
                'Cabecera
                lobjFuente = New Font(FontName, 8, FontStyle.Bold)
                e.Graphics.DrawString("Cia. Ind. Nuevo Mundo S.A.", lobjFuente, Brushes.Black, 35, intPosY)
                'e.Graphics.DrawString("Pág. 0001", lobjFuente, Brushes.Black, 250, 10, format)

                'TITULO DOCUMENTO
                lobjFuente = New Font(FontName, 9, FontStyle.Regular)
                intPosY += 30
                If strVale_TipoDocu = "VS" Then
                    e.Graphics.DrawString("VALE DE SALIDA", lobjFuente, Brushes.Black, 70, intPosY)
                ElseIf strVale_TipoDocu = "VTC" Then
                    e.Graphics.DrawString("VALE DE TRABAJO EN CURSO", lobjFuente, Brushes.Black, 30, intPosY)
                ElseIf strVale_TipoDocu = "NI" Then
                    e.Graphics.DrawString("NOTA DE INGRESO POR COMPRA", lobjFuente, Brushes.Black, 25, intPosY)
                ElseIf strVale_TipoDocu = "VSP" Then
                    e.Graphics.DrawString("VALE DE SALIDA DE PRODUCCION", lobjFuente, Brushes.Black, 20, intPosY)
                End If

                'NUMERO DOCUMENTO
                lobjFuente = New Font(FontName, 10, FontStyle.Regular)
                intPosY += 10
                strNumeroDocu = strVale_TipoDocu & " - " & dtDocumento.Rows(0).Item("DOCUMENTO")
                e.Graphics.DrawString(strNumeroDocu, lobjFuente, Brushes.Black, 100 - ((strNumeroDocu.Length / 2) * 6), intPosY)

                lobjFuente = New Font(FontName, 7, FontStyle.Regular)

                'FECHA
                intPosY += 30
                e.Graphics.DrawString("Fecha:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("FECHA_DOCUMENTO"), lobjFuente, Brushes.Black, 60, intPosY)

                'ALMACEN
                intPosY += 10
                e.Graphics.DrawString("Almacen:", lobjFuente, Brushes.Black, 0, intPosY)
                e.Graphics.DrawString(strVale_CodAlm, lobjFuente, Brushes.Black, 60, intPosY)

                'OPERACION
                e.Graphics.DrawString("Operación:", lobjFuente, Brushes.Black, 140, intPosY)
                e.Graphics.DrawString(dtDocumento.Rows(0).Item("TIPO_OPERACION"), lobjFuente, Brushes.Black, 210, intPosY)

                intPosY += 10
                If strVale_TipoDocu = "VS" Or strVale_TipoDocu = "VTC" Or strVale_TipoDocu = "VSP" Then
                    'PEDIDO
                    e.Graphics.DrawString("Pedido:", lobjFuente, Brushes.Black, 0, intPosY)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("NUMERO_PEDIDO"), lobjFuente, Brushes.Black, 60, intPosY)

                    'COD AUXILIAR
                    intPosY += 10
                    e.Graphics.DrawString("Auxiliar:", lobjFuente, Brushes.Black, 0, intPosY)
                    strAuxiliar = dtDocumento.Rows(0).Item("NOMBRE_AUXILIAR").ToString.ToLower.Replace(vbCrLf, " ")
                    If strAuxiliar.Length > 0 Then
                        e.Graphics.DrawString(strAuxiliar.Substring(0, IIf(strAuxiliar.Length > 32, 31, strAuxiliar.Length)), lobjFuente, Brushes.Black, 60, intPosY)
                        If strAuxiliar.Length > 32 Then
                            intPosY += 10
                            e.Graphics.DrawString(strAuxiliar.Substring(31, IIf(strAuxiliar.Length > 63, 31, strAuxiliar.Length - 31)), lobjFuente, Brushes.Black, 60, intPosY)
                        End If
                    End If

                    'OBSERVACIONES
                    strObservaciones = dtDocumento.Rows(0).Item("OBSERVACION").ToString.ToLower.Replace(vbCrLf, " ")
                    If strObservaciones.Length > 0 Then
                        intPosY += 20
                        e.Graphics.DrawString("Obs:", lobjFuente, Brushes.Black, 0, intPosY)
                        e.Graphics.DrawString(strObservaciones.Substring(0, IIf(strObservaciones.Length > 38, 37, strObservaciones.Length)).Trim, lobjFuente, Brushes.Black, 25, intPosY)

                        If strObservaciones.Length > 38 Then
                            intPosY += 10
                            e.Graphics.DrawString(strObservaciones.Substring(37, IIf(strObservaciones.Length > 75, 37, strObservaciones.Length - 37)).Trim, lobjFuente, Brushes.Black, 25, intPosY)
                        End If

                        If strObservaciones.Length > 75 Then
                            intPosY += 10
                            e.Graphics.DrawString(strObservaciones.Substring(74, IIf(strObservaciones.Length > 112, 37, strObservaciones.Length - 74)).Trim, lobjFuente, Brushes.Black, 25, intPosY)
                        End If
                    End If

                ElseIf strVale_TipoDocu = "NI" Then

                    'PROVEEDOR
                    e.Graphics.DrawString("Proveedor:", lobjFuente, Brushes.Black, 0, intPosY)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("PROVEEDOR"), lobjFuente, Brushes.Black, 60, intPosY)

                    'GUIA PROVEEDOR
                    intPosY += 10
                    e.Graphics.DrawString("Guia Proveedor:", lobjFuente, Brushes.Black, 0, intPosY)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("GUIA_PROVEEDOR"), lobjFuente, Brushes.Black, 90, intPosY)

                    'ORDEN COMPRA
                    intPosY += 10
                    e.Graphics.DrawString("Orden Compra:", lobjFuente, Brushes.Black, 0, intPosY)
                    lobjFuente = New Font(FontName, 8, FontStyle.Regular)
                    e.Graphics.DrawString(dtDocumento.Rows(0).Item("ORDEN_COMPRA"), lobjFuente, Brushes.Black, 90, intPosY)
                End If

                strUsuario_Crea = dtDocumento.Rows(0).Item("USUARIO_CREA")

                'ITEMS DETALLE
                lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                dblTotalCant = 0.0
                dblTotalAlterno = 0.0
                intTotalItem = 0
                intPosY += 20

                'LINEA INICIO DETALLE
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)
                intPosY += 10
                e.Graphics.DrawString("CODIGO", lobjFuente, Brushes.Black, 15, intPosY)
                e.Graphics.DrawString("CANTIDAD", lobjFuente, Brushes.Black, 190, intPosY, format)
                e.Graphics.DrawString("ALTERNA", lobjFuente, Brushes.Black, 250, intPosY, format)
                intPosY += 10
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)
                intPosY += 20

                For Each row As DataRow In dtDocumento.Rows
                    'Codigo Item
                    e.Graphics.DrawString(row("CODIGO_ITEM"), lobjFuente, Brushes.Black, 0, intPosY)

                    'Unidad
                    If row("CODIGO_ITEM").ToString.Length < 16 Then
                        e.Graphics.DrawString(row("UNIDAD"), lobjFuente, Brushes.Black, 95, intPosY)
                    End If

                    'Cantidad
                    e.Graphics.DrawString(row("CANTIDAD"), lobjFuente, Brushes.Black, 190, intPosY, format)

                    'Cantidad Alterna
                    e.Graphics.DrawString(row("CANTIDAD_ALTERNA"), lobjFuente, Brushes.Black, 250, intPosY, format)

                    'Descripcion Item 
                    intPosY += 8
                    lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                    strDescripcion = row("DESCRIPCION_ITEM").ToString.Trim
                    e.Graphics.DrawString(strDescripcion.Substring(0, IIf(strDescripcion.Length > 42, 41, strDescripcion.Length)).Trim, lobjFuente, Brushes.Black, 0, intPosY)
                    If strDescripcion.Length > 42 Then
                        intPosY += 8
                        e.Graphics.DrawString(strDescripcion.Substring(41, IIf(strDescripcion.Length > 82, 41, strDescripcion.Length - 41)).Trim, lobjFuente, Brushes.Black, 0, intPosY)
                    End If

                    'lobjFuente = New Font(FontName, 8, FontStyle.Regular)
                    If strVale_TipoDocu = "NI" Then
                        intPosY += 8
                        e.Graphics.DrawString("Ubicación: ", lobjFuente, Brushes.Black, 0, intPosY)
                        lobjFuente = New Font(FontName, 8, FontStyle.Regular)
                        e.Graphics.DrawString(row("UBICACION_ALMACEN"), lobjFuente, Brushes.Black, 80, intPosY)
                    End If

                    intPosY += 20
                    intTotalItem += 1
                    dblTotalCant += CDbl(row("CANTIDAD"))
                    dblTotalAlterno += CDbl(row("CANTIDAD_ALTERNA"))
                    lobjFuente = New Font(FontName, 7, FontStyle.Regular)
                Next

                'LINEA FIN DETALLE
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)

                intPosY += 10
                If strVale_TipoDocu = "NI" Then
                    e.Graphics.DrawString("TOTAL GRAL:", lobjFuente, Brushes.Black, 40, intPosY)
                    e.Graphics.DrawString(FormatNumber(dblTotalCant, 2), lobjFuente, Brushes.Black, 190, intPosY, format)
                    e.Graphics.DrawString(FormatNumber(dblTotalAlterno, 2), lobjFuente, Brushes.Black, 250, intPosY, format)
                    intPosY += 10
                End If

                e.Graphics.DrawString("TOTAL ITEMS:", lobjFuente, Brushes.Black, 40, intPosY)
                e.Graphics.DrawString(intTotalItem, lobjFuente, Brushes.Black, 190, intPosY, format)

                'LINEA FIN SUBTOTAL
                intPosY += 15
                e.Graphics.DrawString("------------------------------------------", lobjFuente, Brushes.Black, 0, intPosY)

                'FIRMA (Solo Vale de Salida)
                If strVale_TipoDocu = "VS" Or strVale_TipoDocu = "VTC" Then
                    intPosY += 65
                    e.Graphics.DrawString("-------------------", lobjFuente, Brushes.Black, 120, intPosY)
                    intPosY += 10
                    e.Graphics.DrawString("RECIBIDO POR", lobjFuente, Brushes.Black, 140, intPosY)
                End If

                intPosY += 40
                'USUARIO
                e.Graphics.DrawString(strUsuario_Crea, lobjFuente, Brushes.Black, 0, intPosY)
                'FECHA IMPRESION
                e.Graphics.DrawString(Now(), lobjFuente, Brushes.Black, 105, intPosY)


                'FIN PAGINA
                e.HasMorePages = False

                'ELIMINAR OBJETOS
                lobjLogistica = Nothing
                'lobjLoteRollo = Nothing
                dtDocumento = Nothing
                'limgImagen = Nothing
            Else
                Throw New Exception("No se encontro el Documento")
            End If

        Catch ex As Exception
            e.Cancel = True
            Throw (ex)
        Finally
            e.Graphics.Dispose()
            e.HasMorePages = False
        End Try
    End Sub
End Class
