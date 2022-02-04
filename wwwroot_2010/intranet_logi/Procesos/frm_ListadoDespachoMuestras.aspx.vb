Public Class frm_ListadoDespachoMuestras
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgLista As System.Web.UI.WebControls.DataGrid

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
            'Session("@USUARIO") = "DGAMARRA"

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Call prc_listar()
            If Trim("" & Request.QueryString("prMSG")) = "S" Then
        ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('Se creo la Guia " & Trim("" & Request.QueryString("NUD")) & " ');</script>")
            End If

        End If
    End Sub


#Region "-- Metodos --"
    Public Sub prc_listar()
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim pDT As New DataTable
        lobjMuestrasTela.Usuario = CType(Session("@USUARIO"), String)
        lobjMuestrasTela.ListarDespacho_SolicitudMuestrasPend(pDT)
        dtgLista.DataSource = pDT
        dtgLista.DataBind()
    End Sub
#End Region

    Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnSeleccion As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnSeleccion"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnSeleccion.Attributes.Add("onClick", "btnSeleccion_Onclick('" & drvDatos("var_NumeroSolicitud") & "')")
        End If
    End Sub

    
End Class
