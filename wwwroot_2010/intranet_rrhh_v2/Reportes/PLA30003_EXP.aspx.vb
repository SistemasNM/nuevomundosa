Imports System.Drawing

Public Class PLA30003_EXP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected stringWrite As New System.IO.StringWriter
    Protected htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
    Protected WithEvents tblresumen As System.Web.UI.WebControls.Table
    Protected WithEvents tblmaestro As System.Web.UI.WebControls.Table

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '-----------------------------------------------------------------------
    '--INICIO: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    '20120904 EPM Valida que la session este vacio o nula
    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
      If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
        Dim objRequest As New BLITZ_LOCK.clsRequest
        Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
      End If

      Session("@EMPRESA") = "01"
      'Session("@USUARIO") = "EPOMA"

      If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
        Response.Redirect("../../intranet/finsesion.htm")
      End If
    End If
    '-----------------------------------------------------------------------
    '--FINAL: VERIFICAR LA SESION
    '-----------------------------------------------------------------------
    InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        'Put user code to initialize the page here
        Response.ContentType = "application/vnd.ms-excel"
        Cabecera(CStr(Request("strEmpresa")), CStr(Request("strPlanilla")), CInt(Request("strAnio")), CStr(Request("strMes")))
        Detalle(ListarDetalle(CStr(Request("strEmpresa")), CStr(Request("strPlanilla")), CInt(Request("strAnio")), CStr(Request("strMes"))))

        tblmaestro.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString)
        Response.End()
    End Sub
    Private Function ListarDetalle(ByVal pstrEmpresa As String, ByVal pstrPlanilla As String, ByVal pintAnio As Integer, ByVal pintMes As Integer) As DataTable
        Dim ldtRes As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        'Dim lstrParametros() As String = {"var_Empresa", Session("@EMPRESA"), _"int_Anio", CInt(pintAnio)}
        Dim lstrParametros() As String = {"CO_EMPR", CStr(pstrEmpresa), _
                                        "CO_PLAN", CStr(pstrPlanilla), _
                                        "NU_ANNO", CInt(pintAnio), _
                                        "NU_PERI", CInt(pintMes)}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            ldtRes = lobjCon.ObtenerDataTable("MV_DETA_SCTR2", lstrParametros)
        Catch ex As Exception
        Finally
            lobjCon = Nothing
        End Try
        Return ldtRes
    End Function
    Private Sub Cabecera(ByVal pstrEmpresa As String, ByVal pstrPLanilla As String, ByVal pstranio As Integer, ByVal pstrmes As String)
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell
        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.HorizontalAlign = HorizontalAlign.Center : ltcCell.Text = "DETALLE SCTR  " + CStr(pstranio) + " - " + CStr(pstrmes) + " " : ltcCell.ColumnSpan = 8 : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblmaestro.Rows.Add(ltrRow)
        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.HorizontalAlign = HorizontalAlign.Center : ltcCell.Text = " " : ltcCell.ColumnSpan = 8 : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblmaestro.Rows.Add(ltrRow)
        ltrRow = New TableRow
        ltrRow.CssClass = "cabecera"
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Planilla " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Codigo " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Nombres " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Apellido Paterno " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Apellido Materno " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Doc.Iden. " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Base Imponible " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Aporte SCTR " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblmaestro.Rows.Add(ltrRow)
    End Sub

    Private Sub Detalle(ByRef pdtRes As DataTable)
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell
        Dim i As Integer
        For i = 0 To pdtRes.Rows.Count - 1
            ltrRow = New TableRow
            ltrRow.CssClass = "input"
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_PLAN") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_TRAB") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NO_TRAB") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NO_APEL_PATE") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NO_APEL_MATE") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DOCU_IDEN") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("BASE_IMP") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("MONTO_SCT") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            tblmaestro.Rows.Add(ltrRow)
        Next
    End Sub
End Class
