Public Class LOG_0001_2
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            If Request.QueryString("strCtaGasto") <> "" Then
                strCtaGasto = Request.QueryString("strCtaGasto")
            Else
                strCtaGasto = Request.Form("strCtaGasto")
            End If
            Me.wCuentaGasto.value = strCtaGasto
        End If
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("CO_AUXI_EMPR") & "', '" & drvDatos("NO_AUXI") & "')")
        End If
    End Sub

    Private Sub BindGrid()
        Dim lobjPres As OFISIS.OFISEGU.Auxiliares

        lobjPres = New OFISIS.OFISEGU.Auxiliares(Session("@EMPRESA"), Session("@USUARIO"))
        Dim ldtbDatos As DataTable
        lobjPres.Listar(ldtbDatos, txtCodigo.Text, txtNombre.Text, Me.wCuentaGasto.Value)
        dgDatos.DataSource = ldtbDatos
        dgDatos.DataBind()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BindGrid()
    End Sub
End Class