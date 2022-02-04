Imports System.Drawing.Printing

Partial Public Class LOG20012_P
    Inherits System.Web.UI.Page

    Dim mstr_codigobarra1 As String = ""
    Dim mstr_codigoart As String = ""
    Dim mstr_descripcion1 As String = ""
    Dim mstr_codigoum As String = ""
    Dim mstr_cantidad1 As String = "0"
    Dim mstr_ubicacion As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            txtcodarticulo.Text = Request("pcod")
            txtdesarticulo.Text = Request("pdes")
            txtUbicacion.Text = Request("pubi")
            txtcant.Text = "2"
            'habilitar los codigos de barra 
            If Request("pcb4").ToString <> "" Then
                ddlcodigobarra.Items.Insert(0, Request("pcb4").ToString)
            End If

            If Request("pcb3").ToString <> "" Then
                ddlcodigobarra.Items.Insert(0, Request("pcb3").ToString)
            End If

            If Request("pcb2").ToString <> "" Then
                ddlcodigobarra.Items.Insert(0, Request("pcb2").ToString)
            End If

            If Request("pcb1").ToString <> "" Then
                ddlcodigobarra.Items.Insert(0, Request("pcb1").ToString)
            End If

            If ddlcodigobarra.Items.Count <= 0 Then
                ddlcodigobarra.Items.Insert(0, txtcodarticulo.Text)
            End If

            '20120904 EPM se agrega los readonly
            txtcodarticulo.Attributes.Add("readonly", "readonly")
            txtdesarticulo.Attributes.Add("readonly", "readonly")
            txtUbicacion.Attributes.Add("readonly", "readonly")
        End If

    End Sub

    Protected Sub btnimprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnimprimir.Click
        Call fnc_imprimir()
    End Sub


    Public Function fnc_imprimir() As Boolean
        Dim lobjDocumentoImp As PrintDocument, lstrImpresora As String
        Dim lintCantidad As Integer = 0, lintfi As Integer
        Dim lstrCant As String

        'OBTENER EL NOMBRE DE LA IMPRESORA CODIGO DE TABLA GENERAL ##
        lstrImpresora = "ETI_LOGISTICA" '"doPDF v5" '"doPDF v7"

        'OBTENER CANTIDAD DE FILAS
        'lintCantidad = txtcant.Text 'dgarticulos.Items.Count

        Try

            If lstrImpresora.Trim.Length >= 0 Then 'And lintCantidad > 0 Then


                lstrCant = txtcant.Text

                mstr_codigoart = txtcodarticulo.Text
                mstr_codigobarra1 = ""
                mstr_descripcion1 = txtdesarticulo.Text
                mstr_codigoum = ""
                mstr_ubicacion = txtUbicacion.Text

                lobjDocumentoImp = New PrintDocument
                lobjDocumentoImp.PrinterSettings.PrinterName = lstrImpresora
                lobjDocumentoImp.PrinterSettings.Copies = CInt(lstrCant)

                AddHandler lobjDocumentoImp.PrintPage, AddressOf lobjDocumentoImp_PrintThePage
                lobjDocumentoImp.Print()
                lobjDocumentoImp = Nothing

            End If


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "alert('" & ex.Message.Replace("'", "") & "');")
        End Try
        Return True
    End Function

    Private Sub lobjDocumentoImp_PrintThePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Try
            Dim lblnImpresionOK As Boolean = False
            Dim lobjPencil As Drawing.Pen = New Drawing.Pen(System.Drawing.Color.Black, 0.5)
            Dim lobjFuente As Drawing.Font = New Drawing.Font("Arial", 9, Drawing.FontStyle.Regular)
            Dim lblnImprimirPU As Boolean = True, lstrDescripcion1 As String, lstrDescripcion2 As String, lstrCodigoBarra As String

            e.HasMorePages = False

            e.PageSettings.Landscape = True
            '------------------------------------------------------------------------------
            'BEGIN: VALIDACIONES
            '------------------------------------------------------------------------------
            'EXISTENCIA DEL ROLLO
            'If mstr_codigo1.Trim.Length <= 0 Then
            '    e.Cancel = True
            '    Exit Sub
            'End If
            '------------------------------------------------------------------------------
            'END: VALIDACIONES
            '------------------------------------------------------------------------------
            '------------------------------------------------------------------------------
            'BEGIN: CONFIGURAR DATOS FIJOS DE LA ETIQUETA
            '------------------------------------------------------------------------------
            'DESCRIPCION DE ARTICULO
            lobjFuente = New Drawing.Font("Arial", 7, Drawing.FontStyle.Regular)
            e.Graphics.DrawString(Left(mstr_descripcion1, 45), lobjFuente, Drawing.Brushes.Black, 40, 10)
            e.Graphics.DrawString(Mid(mstr_descripcion1, 46, 35) + " - " + mstr_codigoum + " - " + mstr_ubicacion + " - ", lobjFuente, Drawing.Brushes.Black, 40, 25)

            'CODIGO DE BARRA EN BARRAS
            lobjFuente = New Drawing.Font("IDAutomationHC39M", 10, Drawing.FontStyle.Regular)

            lstrCodigoBarra = "*" + IIf(mstr_codigobarra1.Trim.Length > 0, mstr_codigobarra1, mstr_codigoart) + "*"
            e.Graphics.DrawString(lstrCodigoBarra, lobjFuente, Drawing.Brushes.Black, 40, 45)

            'CODIGO DE BARRA EN FA
            'lobjFuente = New Font("Arial", 7, FontStyle.Regular)
            'e.Graphics.DrawString("12345678901234567890", lobjFuente, Brushes.Black, 10, 75)

            '------------------------------------------------------------------------------
            'END: CONFIGURAR DATOS FIJOS DE LA ETIQUETA
            '------------------------------------------------------------------------------
        Catch ex As Exception
            e.Cancel = True
        Finally
            e.Graphics.Dispose()
            e.HasMorePages = False
        End Try
    End Sub
End Class