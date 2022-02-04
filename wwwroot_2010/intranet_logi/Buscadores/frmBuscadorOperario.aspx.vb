Public Class frmBuscadorOperario
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDatos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNombre As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim mstrTipo As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mstrTipo = Request("strTipo")
        If mstrTipo = String.Empty Then
            mstrTipo = "EMP"
        ElseIf mstrTipo = "" Then
            mstrTipo = "EMP"
        End If
    End Sub

    Function Titulo() As String
        If Request("strTipo") = String.Empty Then
            Return "Busqueda de empleados"
            Exit Function
        ElseIf Request("strTipo") = "" Then
            Return "Búsqueda de empleados"
            Exit Function
        ElseIf mstrTipo = "OBR" Then
            Return "Búsqueda de obreros"
            Exit Function
        End If
    End Function

    Private Sub dgDatos_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            Try
                btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("chr_CodigoTrabajador") & "', '" & Replace(drvDatos("var_NombreTrabajador"), "'", "\'").ToString & "', '" & drvDatos("CO_CENT_COST") & "', '" & drvDatos("NO_AUXI") & "')")
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub BindGrid()
        Dim lobjTrabajador As New OFISIS.OFIPLAN.Trabajador("01", Session("@USUARIO"))
        Dim dtbDatos As DataTable
        dtbDatos = Nothing
        lobjTrabajador.Listar(dtbDatos, mstrTipo, txtCodigo.Text, txtNombre.Text)
        dgDatos.DataSource = dtbDatos
        dgDatos.DataBind()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub

End Class