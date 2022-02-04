Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frm_ReporteAnalisisConsumo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCodArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdbArticulo As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdbFile As System.Web.UI.WebControls.RadioButton
    Protected WithEvents pnlArticulo As System.Web.UI.WebControls.Panel
    Protected WithEvents lblDesArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblUniMedida As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStockArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPrecioArticulo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgDetalleConsumo As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnVerReporte As System.Web.UI.WebControls.Button
    Protected WithEvents chkMensual As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSemanal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents rbdProveedor As System.Web.UI.WebControls.RadioButton
    Protected WithEvents pnlFile As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSubir As System.Web.UI.WebControls.Button
    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblProveedor As System.Web.UI.WebControls.Label
    Protected WithEvents txtDesProveedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlProveedores As System.Web.UI.WebControls.Panel
    Protected WithEvents txtCodProveedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnVerProveedores As System.Web.UI.WebControls.Button

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

    Dim strCadena As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            HabilitaAccion(1)
        Else
        End If
    End Sub

    Private Sub HabilitaAccion(ByVal Accion As Integer)
        'Inicio
        If Accion = 1 Then
            pnlFile.Visible = False
            pnlArticulo.Visible = False
            pnlProveedores.Visible = False

            rdbArticulo.Checked = False
            rdbFile.Checked = False
            rbdProveedor.Checked = False

            txtCodArticulo.Text = ""
            lblDesArticulo.Text = ""
            lblUniMedida.Text = ""
            lblStockArticulo.Text = ""
            lblPrecioArticulo.Text = ""

            txtCodProveedor.Text = ""
            txtDesProveedor.Text = ""

            chkMensual.Checked = False
            chkSemanal.Checked = False
        End If
        'Por Articulo
        If Accion = 2 Then
            pnlFile.Visible = False
            pnlProveedores.Visible = False
            pnlArticulo.Visible = True

            rdbFile.Checked = False
            rbdProveedor.Checked = False

            txtCodArticulo.Text = ""
            lblDesArticulo.Text = ""
            lblUniMedida.Text = ""
            lblStockArticulo.Text = ""
            lblPrecioArticulo.Text = ""

            chkMensual.Checked = False
            chkSemanal.Checked = False
        End If
        'File
        If Accion = 3 Then
            pnlArticulo.Visible = False
            pnlProveedores.Visible = False
            pnlFile.Visible = True

            rdbArticulo.Checked = False
            rbdProveedor.Checked = False

            chkMensual.Checked = False
            chkSemanal.Checked = False
        End If
        'Proveedores
        If Accion = 4 Then
            pnlArticulo.Visible = False
            pnlFile.Visible = False
            pnlProveedores.Visible = True

            txtCodProveedor.Text = ""
            txtDesProveedor.Text = ""

            rdbArticulo.Checked = False
            rdbFile.Checked = False

            chkMensual.Checked = False
            chkSemanal.Checked = False
        End If

    End Sub

' Procedimiento usado para consultar reporte
    Private Sub Consulta(ByVal strCadena As String)
        Dim ldblPedidos As DataTable
        Dim objPedidos As Logistica.clsPedidos
        Dim strNumeroOrdenCompra As String

        objPedidos = New Logistica.clsPedidos
        ldblPedidos = New DataTable
        strNumeroOrdenCompra = strCadena

        ldblPedidos = objPedidos.fnc_ConsumoMensual(strNumeroOrdenCompra)
        If Not ldblPedidos Is Nothing Then
            If ldblPedidos.Rows.Count > 0 Then
                dgDetalleConsumo.DataSource = ldblPedidos
                dgDetalleConsumo.DataBind()
                dgDetalleConsumo.Visible = True
            End If
        End If
    End Sub

    'Funcion para Ver Reporte de Analisis detallado (Articulo - Mensual)
    Private Sub fnc_VerReporteAnalisisMensual()
        Dim strNumeroItem As String
        Dim lstrURL As String = ""
        strNumeroItem = "2" + Trim(txtCodArticulo.Text)
        lstrURL = "../CrystalReports/rpt_AnalisisOC.asp?strNumOrdenCompra=" & strNumeroItem
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
    End Sub

    'Funcion para Ver Reporte de Analisis detallado (Articulo - Semanal)
    Private Sub fnc_VerReporteAnalisisSemanal()
        Dim strNumeroItem As String
        Dim lstrURL As String = ""
        strNumeroItem = "2" + Trim(txtCodArticulo.Text)
        lstrURL = "../CrystalReports/rpt_AnalisisSemanal.asp?strNumeroItem=" & strNumeroItem
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
    End Sub

    'Funcion para Ver Reporte de Analisis detallado (Proveedor - Mensual)
    Private Sub btnVerProveedores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerProveedores.Click
        Dim strCodProveedor As String
        Dim lstrURL As String = ""
        If rbdProveedor.Checked = True Then
            If Trim(txtCodProveedor.Text) = "" Or Trim(txtCodProveedor.Text).Length = 0 Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Reporte", "<script language=javascript>alert('Debe Ingresar un codigo de proveedor.');</script>")
            Else
                If chkMensual.Checked = False And chkSemanal.Checked = False Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Reporte", "<script language=javascript>alert('Debe Ingresar elegir un tipo de reporte.');</script>")
                Else
                    If chkMensual.Checked = True Then
                        strCodProveedor = "4" + Trim(txtCodProveedor.Text)
                        lstrURL = "../CrystalReports/rpt_AnalisisOC.asp?strNumOrdenCompra=" & strCodProveedor
                        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
                    End If
                    If chkSemanal.Checked = True Then
                        strCodProveedor = "4" + Trim(txtCodProveedor.Text)
                        lstrURL = "../CrystalReports/rpt_AnalisisSemanal.asp?strNumeroItem=" & strCodProveedor
                        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
                    End If
                End If
            End If
        End If
    End Sub

    'Consulta por Producto
    Private Sub btnVerReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerReporte.Click
        If chkMensual.Checked = False And chkSemanal.Checked = False Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Reporte", "<script language=javascript>alert('Por favor eliga un tipo de Reporte (Mensual o Semanal).');</script>")
        Else
            If Trim(txtCodArticulo.Text) = "" Or Trim(txtCodArticulo.Text).Length = 0 Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Reporte", "<script language=javascript>alert('Debe Ingresar un codigo de articulo.');</script>")
            Else
                If chkMensual.Checked = True Then
                    fnc_VerReporteAnalisisMensual()
                End If
                If chkSemanal.Checked = True Then
                    fnc_VerReporteAnalisisSemanal()
                End If
            End If
        End If
    End Sub

    'Consulta por File
    Private Sub btnSubir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubir.Click
        Dim strFileName As String = "Articulos.xls"
        Dim objPedido As Logistica.clsPedidos
        Dim lstrURL As String = ""
        Dim dtbArticulos As New DataTable
        If chkMensual.Checked = False And chkSemanal.Checked = False Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Reporte", "<script language=javascript>alert('Por favor eliga un tipo de Reporte (Mensual o Semanal).');</script>")
        Else
            Try
                If File1.PostedFile.FileName <> "" Then
                    Dim strRoot As String = System.Web.Configuration.WebConfigurationManager.AppSettings("UploadPath").ToString()
                    Dim strPath As String = strRoot + strFileName
                    If File.Exists(strPath) = True Then
                        File.Delete(strPath)
                    End If
                    File1.PostedFile.SaveAs(strPath)
                    objPedido = New Logistica.clsPedidos
                    dtbArticulos = objPedido.fnc_ObtenerArticuloXLS(strPath)
                    If dtbArticulos.Rows.Count > 0 Then
                        strCadena = "3" + objPedido.fnc_Articulo_Grabar(dtbArticulos)
                        If chkMensual.Checked = True Then
                            lstrURL = "../CrystalReports/rpt_AnalisisOC.asp?strNumOrdenCompra=" & strCadena
                            ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
                        End If
                        If chkSemanal.Checked = True Then
                            lstrURL = "../CrystalReports/rpt_AnalisisSemanal.asp?strNumeroItem=" & strCadena
                            ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp1('" & lstrURL & "');</script>")
                        End If
                    End If
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "Archivo", "<script language=javascript>alert('Debe elegir un archivo.');</script>")
                End If
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Me.[GetType](), "Subir", "<script language=javascript>alert('Error: Verifique el file.');</script>")
            End Try
        End If
    End Sub

    Private Sub rdbArticulo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbArticulo.CheckedChanged
        If rdbArticulo.Checked = True Then
            HabilitaAccion(2)
        End If
    End Sub

    Private Sub rdbFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbFile.CheckedChanged
        If rdbFile.Checked = True Then
            HabilitaAccion(3)
        End If
    End Sub

    Private Sub rbdProveedor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbdProveedor.CheckedChanged
        If rbdProveedor.Checked = True Then
            HabilitaAccion(4)
        End If
    End Sub

    Private Sub chkMensual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMensual.CheckedChanged
        chkSemanal.Checked = False
    End Sub

    Private Sub chkSemanal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSemanal.CheckedChanged
        chkMensual.Checked = False
    End Sub
End Class
