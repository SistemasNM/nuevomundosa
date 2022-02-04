Imports System.Data
Imports ComunLib

Partial Public Class frmDesTabla
    Inherits System.Web.UI.Page

    Dim mintFila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            InicilizaDatos()
        End If

    End Sub

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
End Class