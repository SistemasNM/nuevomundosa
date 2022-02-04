Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class ConversiondeValesAPedidos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDatos As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Eventos --"
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            ListaValesPendientes()
        End If
    End Sub

    Private Sub ListaValesPendientes()
        Dim objVales As Logistica.clsPedidos
        Dim dtbVales As DataTable
        objVales = New Logistica.clsPedidos
        Try
            dtbVales = objVales.fncConsultaVales()
            If dtbVales.Rows.Count > 0 Then
                dgDatos.DataSource = dtbVales
                dgDatos.DataBind()
            End If
        Catch ex As Exception
            'lblMsgError.Text = ex.ToString
        End Try
    End Sub
    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            Dim btnSeleccion As HtmlInputButton = CType(e.Item.FindControl("btnSeleccion"), HtmlInputButton)
            Dim drvItem As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnSeleccion.Attributes.Add("onclick", "btnSeleccion_Onclick('" & drvItem("Id_Vale") & "')")
        End If
    End Sub
#End Region

End Class
