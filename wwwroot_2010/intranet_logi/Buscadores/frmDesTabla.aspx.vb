Imports System.Data
Imports NM_General
Public Class frmDesTabla
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitCodigo1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblTit2Descri As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescri As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOpc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegSel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTitCodigo2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitDescri2 As System.Web.UI.WebControls.Label
    Protected WithEvents grdData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSQL As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFiltro As System.Web.UI.WebControls.TextBox

    
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        
        InitializeComponent()
    End Sub

#End Region
    Dim mintFila As Integer = 0

    Private Sub InicilizaDatos()
        Dim clsTabla As New clsTabla

        clsTabla.NombreTabla = Request.QueryString("sTabla")
        clsTabla.InicializaTabla()

        txtFiltro.Text = Request.QueryString("sFiltro")

        lblTitulo.Text = clsTabla.Titulo

        lblTitCodigo1.Text = "Código :"
        lblTit2Descri.Text = "Descripción :"

        lblTitCodigo2.Text = clsTabla.TituloColumnaCodigo
        lblTitDescri2.Text = clsTabla.TituloColumnaDescri

        txtSQL.Text = clsTabla.SentenciaSQL

        clsTabla = Nothing

    End Sub


    Private Sub CargaLista()
        Dim clsTabla As New clsTabla
        Dim dtDatos As New DataTable
        Dim sSQL As String

        Dim sFiltro As String

        mintFila = 0

        sSQL = txtSQL.Text
        sFiltro = txtFiltro.Text

        If txtFiltro.Text <> "" Then
            If InStr(sSQL.ToUpper, "WHERE") > 0 Then
                sSQL = sSQL & " AND  " & sFiltro
            Else
                sSQL = sSQL & " WHERE  " & sFiltro
            End If

        End If

        If txtCodigo.Text <> "" Then
            sSQL = "SELECT * FROM (" & sSQL & ")  Tabla WHERE Codigo  LIKE '%" & txtCodigo.Text & "%'"
        End If

        If txtDescri.Text <> "" Then
            sSQL = "SELECT * FROM (" & sSQL & ")  Tabla WHERE Descri  LIKE '%" & txtDescri.Text & "%'"
        End If

        clsTabla.SentenciaSQL = sSQL


        If clsTabla.CargaLista(dtDatos) = True Then
            txtReg.Text = dtDatos.Rows.Count.ToString
            grdData.DataSource = dtDatos
            grdData.DataBind()
        End If

        dtDatos = Nothing
        clsTabla = Nothing

    End Sub

    Private Sub grdData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdData.ItemDataBound
        e.Item.Attributes.Add("onclick", "fSelFila(this," + mintFila.ToString + ")")
        mintFila = mintFila + 1
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        CargaLista()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            InicilizaDatos()
        End If
    End Sub

End Class