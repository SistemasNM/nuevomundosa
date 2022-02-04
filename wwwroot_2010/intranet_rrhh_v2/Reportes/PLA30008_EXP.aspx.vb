Imports System.Drawing

Public Class PLA30008_EXP
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
        Response.ContentType = "application/vnd.ms-excel"
        Cabecera()
        Detalle(ListarDetalle(CInt(Request("strAnio")), CInt(Request("strMes")), Request("strEstado")))

        'tblmaestro.RenderControl(htmlWrite)
        'Response.Write(stringWrite.ToString)
        'Response.End()
    End Sub

    Private Function ListarDetalle(ByVal pintAnio As Integer, ByVal pintMes As Integer, ByVal pstrestado As String) As DataTable
        Dim ldtRes As New DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"CO_EMPR", Session("@EMPRESA"), _
                                        "NU_ANNO", CInt(pintAnio), _
                                        "NU_PERI", CInt(pintMes), _
                                        "TI_SITU", CStr(pstrestado)}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            ldtRes = lobjCon.ObtenerDataTable("USP_MAE_TRAB_LISTADATOS", lstrParametros)
        Catch ex As Exception
        Finally
            lobjCon = Nothing
        End Try
        Return ldtRes
    End Function

    Private Sub Cabecera()
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell

        ltrRow = New TableRow
        ltcCell = New TableCell : ltrRow.CssClass = "txtcabecera1" : ltcCell.HorizontalAlign = HorizontalAlign.Center : ltcCell.Text = "MAESTRO DE TRABAJADORES" : ltcCell.ColumnSpan = 28 : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        tblmaestro.Rows.Add(ltrRow)

        ltrRow = New TableRow
        ltcCell = New TableCell : ltcCell.Text = " Codigo " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Apellidos y Nombres " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Planilla " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Area. " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Seccion " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Puesto " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Sexo " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Fecha Nac. " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Direccion " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Pais " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Documento " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Fecha Ingr. " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Fecha Cese. " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Nro.Essalud " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Grado Instr. " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " AFP " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Nro.AFP " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Nro.EPS " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " CCosto " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Descripcion CCosto " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Cod.Seccion " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Cod.Sede " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Tipo Cont. " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Sindicato " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Cta.Sueldo " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Moneda CTS " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Banco CTS " : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
        ltcCell = New TableCell : ltcCell.Text = " Cuentas CTS " : ltrRow.Cells.Add(ltcCell) : ltrRow.CssClass = "txtcabecera1" : ltcCell = Nothing

        tblmaestro.Rows.Add(ltrRow)
    End Sub

    Private Sub Detalle(ByRef pdtRes As DataTable)
        Dim ltrRow As TableRow
        Dim ltcCell As TableCell
        Dim i As Integer
        For i = 0 To pdtRes.Rows.Count - 1
            ltrRow = New TableRow
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_TRAB") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NOMBRE") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_PLAN") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DE_AREA") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DE_SECC") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DE_PUES_TRAB") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("ST_SEXO") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("FE_NACI_TRAB") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DIRECCION") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NO_PAIS") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DOCUMENTO") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("FE_INGR_EMPR") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("Fecha_Cese") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NRO_ESSALUD") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_TIPO_INST") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("AFP") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NRO_SPP") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NRO_EPS") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_CENT_COST") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("DE_COSTO") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_SECC") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_SEDE") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_TIPO_CONT") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("SINDICATO") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NU_CNTA_SUEL") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("CO_MONE_CTSS") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NO_CORT_BANC") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            ltcCell = New TableCell : ltcCell.Text = pdtRes.Rows(i)("NU_CNTA_CTSS") : ltrRow.CssClass = "txttexto1" : ltrRow.Cells.Add(ltcCell) : ltcCell = Nothing
            tblmaestro.Rows.Add(ltrRow)
        Next
    End Sub


End Class

