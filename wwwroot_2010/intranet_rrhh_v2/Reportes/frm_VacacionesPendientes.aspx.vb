Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_VacacionesPendientes
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "lcaraved"

        'Verificar Sesion
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
        prcConsultarusuario(Session("@USUARIO"))
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        ListarVacaciones()
    End Sub

    ' Listar reporte detalle
    Private Sub ListarVacaciones()
        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        'strPath = "%2fNM_Reportes%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")
        strURL = strURL + strPath

        'strURL = strURL + "planilla_VacacionesPendientes"
        strURL = strURL + "pla_vacacionespendientes"
        strURL = strURL + "&vch_CodigoPlanilla=" + ""
        strURL = strURL + "&vch_CodigoTrabajador=" + Mid(lblUsuario.Text, 1, 5)
        strURL = strURL + "&vch_CodEmpGer=" + hdnCodEmpGer.Value
        strURL = strURL + "&vch_CenCosGer=" + txtCodGer.Text
        strURL = strURL + "&vch_CodEmpJef=" + hdnCodEmpJef.Value
        strURL = strURL + "&vch_CenCosJef=" + txtCodJef.Text
        strURL = strURL + "&vch_CodEmpSup=" + hdnCodEmpSup.Value
        strURL = strURL + "&vch_CenCosSup=" + txtCodSup.Text
        strURL = strURL + "&chr_Cesados=" + "ACT"
        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

    Public Function prcConsultarusuario(ByVal strUsuario As String) As DataTable
        Dim ldtbUsuario As New DataTable
        ldtbUsuario = Nothing
        Try
            Dim objParametros As Object() = {"COD_USU", strUsuario}
            ldtbUsuario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_PedidoConsultaUsuario", objParametros)
            If Not ldtbUsuario Is Nothing Then
                lblUsuario.Text = ldtbUsuario.Rows(0).Item("co_trab") + "-" + ldtbUsuario.Rows(0).Item("Nombres")
            Else
                lblError.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        Return ldtbUsuario
    End Function
End Class