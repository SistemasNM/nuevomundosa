Public Class LOG30007_E
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents tblExcel As System.Web.UI.WebControls.Table
    Protected stringWrite As New System.IO.StringWriter
    Protected htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim mstrRetenedor As String
    Dim mstrBuenContribuyente

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim ldtRes As DataTable
        mstrRetenedor = Request("strRetenedor")
        mstrBuenContribuyente = Request("strBuenContribuyente")
        If Listar(ldtRes) Then
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            CargarCabecera()
            CargarDetalle(ldtRes)
            tblExcel.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString)
            Response.End()
        End If
    End Sub

    Private Function CargarCabecera()
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell

        ltrRow = New TableRow
        ltrRow.CssClass = "cabecera"
        ltcCell = New TableCell : ltcCell.Text = "Código proveedor" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Nombre proveedor" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "R.U.C. " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Tipo clase" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Razón social" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Retención" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Buen contribuyente" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Usuario Creador" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Fecha Creación" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Usuario modificador" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = "Fecha modificación" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblExcel.Rows.Add(ltrRow)
    End Function

    Private Function CargarDetalle(ByRef pdtDet As DataTable)
        Dim i As Integer
        Dim j As Integer
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell

        For i = 0 To pdtDet.Rows.Count - 1
            ltrRow = New TableRow
            ltrRow.CssClass = "input"
            For j = 0 To pdtDet.Columns.Count - 1
                ltcCell = New TableCell : ltcCell.Text = pdtDet.Rows(i).Item(j) : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            Next j
            tblExcel.Rows.Add(ltrRow)
        Next i
    End Function

    Private Function Listar(ByRef pdtLista As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lbooOk As Boolean
        Dim lstrParametros() As String = {"chr_Empresa", Session("@EMPRESA"), _
                "var_Retencion", mstrRetenedor, _
                "var_BuenContribuyente", mstrBuenContribuyente}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtLista = lobjCon.ObtenerDataTable("USP_LOG_PROVEEDOR_LISTA", lstrParametros)
            lbooOk = True
        Catch ex As Exception
            pdtLista = Nothing
            lbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return lbooOk
    End Function
End Class
