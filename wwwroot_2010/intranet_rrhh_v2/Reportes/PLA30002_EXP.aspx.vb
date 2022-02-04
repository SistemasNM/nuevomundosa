Imports System.Drawing

Public Class PLA30002_EXP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected stringWrite As New System.IO.StringWriter
    Protected htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
    Protected WithEvents tblresumen As System.Web.UI.WebControls.Table

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
        Response.ContentType = "application/vnd.ms-excel"
        Cabecera(CStr(Request("strAnio")), CStr(Request("strMes")), CStr(Right(Request("strFecha"), 2) + "/" + Mid(Request("strFecha"), 5, 2) + "/" + Left(Request("strFecha"), 4)))
        Detalle(ListarDetalle(CInt(Request("strAnio")), CInt(Request("strMes")), CStr(Request("strFecha"))))

        tblresumen.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString)
        Response.End()
    End Sub

    Private Function ListarDetalle(ByVal pintAnio As Integer, ByVal pintMes As Integer, ByVal pstrfecha As String) As DataTable
        Dim ldtRes As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        'Dim lstrParametros() As String = {"var_Empresa", Session("@EMPRESA"), _"int_Anio", CInt(pintAnio)}
        Dim lstrParametros() As String = {"CO_EMPR", Session("@EMPRESA"), _
                                        "NU_ANNO", CInt(pintAnio), _
                                        "NU_MES", CInt(pintMes), _
                                        "FE_PROC", CStr(pstrfecha)}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            ldtRes = lobjCon.ObtenerDataTable("MV_RESU_SECC_JM", lstrParametros)
        Catch ex As Exception
        Finally
            lobjCon = Nothing
        End Try
        Return ldtRes
    End Function

    Private Sub Cabecera(ByVal pstranio As String, ByVal pstrmes As String, ByVal pstrfecha As String)
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell
        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.HorizontalAlign = HorizontalAlign.Center : ltcCell.Text = "RESUMEN POR AREAS" : ltcCell.ColumnSpan = 11 : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblresumen.Rows.Add(ltrRow)
        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.HorizontalAlign = HorizontalAlign.Center : ltcCell.Text = "ANNO : " + CStr(pstranio) + "    MES : " + CStr(pstrmes) + "    MOV.PERSONAL AL: " + CDate(pstrfecha) : ltcCell.ColumnSpan = 11 : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblresumen.Rows.Add(ltrRow)
        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.HorizontalAlign = HorizontalAlign.Center : ltcCell.Text = " " : ltcCell.ColumnSpan = 11 : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblresumen.Rows.Add(ltrRow)
        ltrRow = New TableRow
        ltrRow.CssClass = "cabecera"
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Planilla " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Area " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Nro.Trab. " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Ingresos. " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Aportaciones " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Desc.Medico " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Nro.HE 50% " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " S/.HE 50% " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " Nro.HE 100% " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " S/.HE 100% " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.ForeColor = Color.White : ltcCell.BackColor = Color.Gray : ltcCell.Text = " S/. CTS " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing

        tblresumen.Rows.Add(ltrRow)
    End Sub

    Private Sub Detalle(ByRef pdtRes As DataTable)
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell
        Dim i As Integer
        For i = 0 To pdtRes.Rows.Count - 1
            ltrRow = New TableRow
            ltrRow.CssClass = "input"
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_PLAN") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DE_SECC") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TT_TRAB") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TS_INGR") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TS_APRT") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TS_DMED") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TH_HESI") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TS_SESI") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TH_HEDO") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TS_SEDO") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("TS_CTSS") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            tblresumen.Rows.Add(ltrRow)
        Next
        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.Text = "T O T A L E S: " : ltcCell.ColumnSpan = 2 : ltrRow.Cells.Add(ltcCell)
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TT_TRAB)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TS_INGR)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TS_APRT)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TS_DMED)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TH_HESI)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TS_SESI)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TH_HEDO)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TS_SEDO)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = pdtRes.Compute("Sum(TS_CTSS)", "") : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblresumen.Rows.Add(ltrRow)


    End Sub


End Class
