Imports System
Public Partial Class frm_ListadoPrecostos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Call sMostrarReporte
    End Sub

    
    Private Sub sMostrarReporte()
        '*****************************************************************************************************
        'Objetivo   : Muestra el reporte que esta en el servidor
        'Autor      : CPT
        'Creado     : 00/00/0000
        'Modificado : 00/00/0000
        '*****************************************************************************************************

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String
        Dim strEstado As String


        If optPendiente.Checked = True Then
            strEstado = "SOL"
        Else
            strEstado = "APR"
        End If


        'strPath = "%2fPRECOSTOS%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        'txtUrl.Text = strURL

        'strURL = strURL + "precosto_listado"
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloPreCostos")
        strURL = strURL + strPath + "pto_listado"

        If TxtFechaIni.Text <> "" Then
            strURL = strURL + "&dtm_FechaIni=" + Format(CDate(TxtFechaIni.Text), "yyyyMMdd")
        Else
            strURL = strURL + "&dtm_FechaIni=."
        End If

        If TxtFechaFin.Text <> "" Then
            strURL = strURL + "&dtm_FechaFin=" + Format(CDate(TxtFechaFin.Text), "yyyyMMdd")
        Else
            strURL = strURL + "&dtm_FechaFin=."
        End If

        strURL = strURL + "&chr_Estado=" + strEstado

        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
    


End Class