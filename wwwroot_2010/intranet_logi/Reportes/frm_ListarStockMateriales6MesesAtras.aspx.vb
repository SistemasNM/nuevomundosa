Imports System.Data
Imports NuevoMundo
Public Class frm_ListarStockMateriales6MesesAtras
    Inherits System.Web.UI.Page

    Private Sub frm_ListarStockMateriales6MesesAtras_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        'Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "BENITO"
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
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
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        sMostrarReporte()
    End Sub

    Private Sub sMostrarReporte()

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String
        Dim strReporte As String

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strReporte = "Logistica_ListarStockMateriales6MesesAtras"
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strReporte = "log_listar_stock_materiales_6mesesatras"
        strURL = strURL + strPath
        'CAMBIO DG FIN
        txtUrl.Text = strURL
        strURL = strURL + strReporte
        strURL = strURL + "&vch_mes=" + Me.ddlMes.Text
        strURL = strURL + "&vch_anio=" + Me.ddlAnio.Text

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
    Private Sub CargarMesActual()
        ddlMes.SelectedValue = DateTime.Now.ToString("MM")
    End Sub
    Private Sub CargarAnioVigenteStock()
        Dim ldtbAnio As DataTable
        Dim objCls As New clsAlmacen
        ldtbAnio = Nothing
        lblMsg.Text = ""

        ddlAnio.Items.Clear()
        ddlAnio.DataSource = Nothing

        Try
            ldtbAnio = objCls.ObtenerAnioVigenteStock()

            If Not ldtbAnio Is Nothing Then
                If ldtbAnio.Rows.Count > 0 Then
                    ddlAnio.DataSource = ldtbAnio
                    ddlAnio.DataTextField = "DE_ANIO"
                    ddlAnio.DataValueField = "CO_ANIO"
                    ddlAnio.SelectedValue = DateTime.Now.ToString("yyyy")
                    ddlAnio.DataBind()
                End If
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objCls = Nothing
            ldtbAnio = Nothing
        End Try
    End Sub

    Private Sub frm_ListarStockMateriales6MesesAtras_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        CargarAnioVigenteStock()
        CargarMesActual()
        'End If
    End Sub
End Class