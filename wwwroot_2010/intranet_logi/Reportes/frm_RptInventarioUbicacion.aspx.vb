Public Class WebForm2
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
    End Sub

    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim strUbicacion As String
        Dim lstrURL As String = ""
        strUbicacion = Trim(txtUbicacion.Text)
        lstrURL = "../CrystalReports/rpt_InventarioRptos.asp?Var_Empresa=01" + "&Var_Ubicacion=" & strUbicacion
        ClientScript.RegisterStartupScript(Me.[GetType](), "reporte", "<script language=javascript>popUp('" & lstrURL & "');</script>")
    End Sub
End Class