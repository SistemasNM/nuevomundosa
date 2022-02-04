Partial Public Class LOG20014_E

#Region " Web Form Designer Generated Code "

    Inherits System.Web.UI.Page

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents tblExcel As System.Web.UI.WebControls.Table
    Protected stringWrite As New System.IO.StringWriter
    Protected htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

    Dim mstrRetenedor As String
    Dim mstrBuenContribuyente

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim ldtRes As DataTable
        Dim lstr_codinv As String = ""

        lstr_codinv = Request("pstrcodinv")

        If Listar(ldtRes, lstr_codinv) Then
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            CargarCabecera(lstr_codinv)
            CargarDetalle(ldtRes)
            'tblExcel.RenderControl(htmlWrite)
            'Response.Write(stringWrite.ToString)
            'Response.End()
        End If
    End Sub

    Private Sub CargarCabecera(ByVal pstrCodInv As String)
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell

        ltrRow = New TableRow
        ltrRow.CssClass = "cabecera"
        ltcCell = New TableCell : ltcCell.Text = "Cod. Inv." : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pstrCodInv : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblExcel.Rows.Add(ltrRow)

        ltrRow = New TableRow
        ltrRow.CssClass = "cabecera"
        ltcCell = New TableCell : ltcCell.Text = "Tipo" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Codigo" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Descripcion" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "U.M." : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Stock" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Inventario" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblExcel.Rows.Add(ltrRow)
    End Sub

    Private Sub CargarDetalle(ByRef pdtDet As DataTable)
        Dim i As Integer
        Dim j As Integer
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell

        For i = 0 To pdtDet.Rows.Count - 1
            ltrRow = New TableRow
            ltrRow.CssClass = "input"

            ltcCell = New TableCell
            ltcCell.Text = CType(pdtDet.Rows(i).Item("de_tipo_item"), String)
            ltrRow.Cells.Add(ltcCell)
            ltcCell = Nothing

            ltcCell = New TableCell
            ltcCell.Text = "'" + CType(pdtDet.Rows(i).Item("vch_item"), String)
            ltrRow.Cells.Add(ltcCell)
            ltcCell = Nothing

            ltcCell = New TableCell
            ltcCell.Text = pdtDet.Rows(i).Item("de_item")
            ltrRow.Cells.Add(ltcCell)
            ltcCell = Nothing

            ltcCell = New TableCell
            ltcCell.Text = CType(pdtDet.Rows(i).Item("co_unme"), String)
            ltrRow.Cells.Add(ltcCell)
            ltcCell = Nothing

            ltcCell = New TableCell
            ltcCell.Text = Format(CType(pdtDet.Rows(i).Item("num_stock"), Double), "###,##0.00")
            ltrRow.Cells.Add(ltcCell)
            ltcCell = Nothing

            ltcCell = New TableCell
            ltcCell.Text = "        "
            ltrRow.Cells.Add(ltcCell)
            ltcCell = Nothing

            tblExcel.Rows.Add(ltrRow)
        Next i
    End Sub

    Private Function Listar(ByRef pdtLista As DataTable, ByVal pstr_codinv As String) As Boolean
        Dim lobj_planilla As New NuevoMundo.clsArticulo
        Dim ldts_datos As New DataSet
        Dim lbooOk As Boolean = False

        Try
            If lobj_planilla.Listar_InventarioDiario(ldts_datos, 1, pstr_codinv, "", "", "") = True Then
                pdtLista = ldts_datos.Tables(0)
                lbooOk = True
            End If
        Catch ex As Exception

        End Try

        ldts_datos = Nothing
        lobj_planilla = Nothing

        Return lbooOk
    End Function
End Class
