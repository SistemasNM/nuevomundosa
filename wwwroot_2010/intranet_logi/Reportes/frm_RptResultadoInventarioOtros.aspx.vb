Imports System.Data
Imports System.Data.SqlClient
Imports NuevoMundo

Public Class frm_RptREsultadoInventarioOtros
    Inherits System.Web.UI.Page

    Private Sub frm_RptMovimientoAlmacen_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "BENITO"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarAlmacenes()
            CargarFechaInventario(ddlAlmacenes.SelectedValue)
        End If
    End Sub

   

    Private Sub sMostrarReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""

        'CAMBIO DG INI
        'strPath = "%2fNM_Reportes%2f"
        'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
        strURL = strURL + strPath + "log_inventario_otros"
        'CAMBIO DG FIN
        txtUrl.Text = strURL

        'strURL = strURL + "Logistica_InventarioOtros"
        strURL = strURL + "&CO_ALMA=" + ddlAlmacenes.SelectedValue
        strURL = strURL + "&FE_TOMA_INVE=" + Format(CDate(ddlFechas.SelectedValue), "yyyyMMdd")
        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"


        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
    Private Sub CargarAlmacenes()
        Dim ldtbAlmacenes As DataTable
        Dim objCls As New clsAlmacen
        ldtbAlmacenes = Nothing
        lblMsg.Text = ""

        ddlAlmacenes.Items.Clear()
        ddlAlmacenes.DataSource = Nothing

        Try
            ldtbAlmacenes = objCls.ObtenerAlmacenes()

            If Not ldtbAlmacenes Is Nothing Then
                If ldtbAlmacenes.Rows.Count > 0 Then
                    ddlAlmacenes.DataSource = ldtbAlmacenes
                    ddlAlmacenes.DataTextField = "de_alma"
                    ddlAlmacenes.DataValueField = "co_alma"
                    ddlAlmacenes.DataBind()
                End If
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objCls = Nothing
            ldtbAlmacenes = Nothing
        End Try
    End Sub
    Private Sub CargarFechaInventario(co_alma)
        Dim ldtbFinventario As DataTable
        Dim objCls As New clsAlmacen
        Dim strCodigoAlmacen As String = co_alma

        ldtbFinventario = Nothing
        lblMsg.Text = ""

        ddlFechas.Items.Clear()
        ddlFechas.DataSource = Nothing

        Try
            ldtbFinventario = objCls.ObtenerFechas(strCodigoAlmacen)

            If Not ldtbFinventario Is Nothing Then
                If ldtbFinventario.Rows.Count > 0 Then
                    ddlFechas.DataSource = ldtbFinventario
                    ddlFechas.DataTextField = "FE_TOMA_INVE"
                    ddlFechas.DataValueField = "FE_TOMA_INVE"
                    ddlFechas.DataBind()
                End If
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objCls = Nothing
            ldtbFinventario = Nothing
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        sMostrarReporte()
    End Sub

   
End Class