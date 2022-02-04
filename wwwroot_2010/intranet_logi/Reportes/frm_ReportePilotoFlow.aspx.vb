Public Class frm_ReportePilotoFlow
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            'Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "LALANOCA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call CargaComboAnioPeriodo(Year(Now()))
            ddlAnhioPeriodo.SelectedValue = Year(Now.AddMonths(-1))
        End If        
    End Sub

#Region "Metodos"
    Private Sub CargaComboAnioPeriodo(ByVal intAnioActual As Integer)

        For i As Integer = 0 To 2
            ddlAnhioPeriodo.Items.Add((intAnioActual - i))
        Next i

    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        sMostrarReporte()
    End Sub


    Private Sub sMostrarReporte()

        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strPeriodo As String
        Dim strArticulo As String

        strPeriodo = ddlAnhioPeriodo.SelectedValue
        strArticulo = txtArticulo.Text

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath + "log_pilotoflow_ingresos_ventas"
        strURL = strURL & "&pint_Anno=" & strPeriodo
        strURL = strURL & "&vch_CodigoArticulo=" & strArticulo
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
    End Sub

#End Region
End Class