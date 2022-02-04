Imports NM_General

Public Class frmListFormatoNuevoInsumo
    Inherits System.Web.UI.Page

#Region "Funciones"

    Sub fn_listarFormatosNuevoInsumo()
        Dim lstrCodResp As String = ""
        Dim lstrCodProv As String = ""
        Dim lstrEstado As String = ""
        Dim lobjGerencia As New clsGerencia
        Dim ldtResponse As DataTable

        If Not txtFechaIni.Text.Trim().Equals("") Then
            If txtFechaFin.Text.Trim().Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha fin.');</script>")
                Exit Sub
            End If
        End If

        If Not txtFechaFin.Text.Trim().Equals("") Then
            If txtFechaIni.Text.Trim().Equals("") Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese la fecha inicio.');</script>")
                Exit Sub
            End If
        End If


        lstrCodResp = txtResponsable.Text.Trim().ToUpper()
        lstrCodProv = txtCodProveedor.Text.Trim().ToUpper()

        If txtFechaIni.Text.Trim().Equals("") Then
            lstrFechaIni = ""
        Else
            lstrFechaIni = Convert.ToDateTime(txtFechaIni.Text.Trim()).ToString("yyyyMMdd")
        End If

        If txtFechaFin.Text.Trim().Equals("") Then
            lstrFechaFin = ""
        Else
            lstrFechaFin = Convert.ToDateTime(txtFechaFin.Text.Trim()).ToString("yyyyMMdd")
        End If

        lstrEstado = ddlEstado.SelectedValue().ToString()

        ldtResponse = lobjGerencia.fn_listarFormatoCNI(lstrCodResp,
                                                      lstrCodProv,
                                                      lstrEstado,
                                                      lstrFechaIni,
                                                      lstrFechaFin)

        If ldtResponse.Rows.Count > 0 Then
            gvFormatoCP.DataSource = ldtResponse
            gvFormatoCP.DataBind()
        Else
            gvFormatoCP.DataSource = Nothing

            gvFormatoCP.DataBind()
        End If
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "JRUIZS"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                    Dim objRequest As New BLITZ_LOCK.clsRequest
                    Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
                End If

                If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                    Response.Redirect("../../intranet/finsesion.htm")
                End If
            End If

            fn_listarFormatosNuevoInsumo()
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        fn_listarFormatosNuevoInsumo()
    End Sub
 
    Protected Sub gvFormatoCP_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gvFormatoCP.ItemCommand
        Dim tipoprueba As DataTable
        If e.CommandName.ToString().Equals("Visualizar") Then

            Dim lobjGerencia As New clsGerencia
            Dim ldsResponse As DataSet
            tipoprueba = lobjGerencia.fn_ObtenerTipoPrueba(e.CommandArgument, 2)
            If (tipoprueba.Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString() <> "") Then
                ldsResponse = lobjGerencia.fn_ObtenerPruebaPreliminar(e.CommandArgument, 2, tipoprueba.Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString().Substring(0, 1), Session("@USUARIO"))
                If (Not ldsResponse.Tables(1).Rows(0).Item("AREA") Is Nothing) Then
                    If ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() <> "" Then
                        If (ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = ("DIRECCION TECNICA DE CALIDAD") Or ldsResponse.Tables(1).Rows(0).Item("AREA").ToString() = "SISTEMAS") Then
                            Response.Redirect("frmListadoNuevoInsumo.aspx?pidCodGenFormato=" + e.CommandArgument)
                        Else
                            Response.Redirect("frmHojaRuta.aspx?TipoPrueba=" + tipoprueba.Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString().Substring(0, 1) + "&cod=" + e.CommandArgument)

                        End If
                    End If
                End If

            Else
                Response.Redirect("frmListadoNuevoInsumo.aspx?pidCodGenFormato=" + e.CommandArgument)
            End If

            


        End If

        If e.CommandName.ToString().Equals("VerPDF") Then
            Dim strURL As String = ""
            Dim strPath As String = ""
            Dim strScript As String = ""
            Dim strCodGenerado As String
            Dim strCodFormato As String

            strCodGenerado = e.CommandArgument
            strCodFormato = "2"

            If strCodGenerado.Length > 0 Then
                'CAMBIO DG INI
                'strPath = "%2fNM_Reportes%2f"
                'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
                'strURL = strURL + "logistica_InventarioDiario"
                strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
                strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloGerencia")
                strURL = strURL + strPath + "gg_formato_nuevo_insumo"
                'CAMBIO DG FIN
                strURL = strURL + "&pintCodGenFormato=" + strCodGenerado
                strURL = strURL + "&pintCodFormato=" + strCodFormato

                strURL = strURL + "&rc:Command=Render"
                strURL = strURL + "&rc:Toolbar=true"

                strScript = "fMostrarReporte('" & strURL & "');"
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
            Else
                'lblMensaje.Text = "Debe ingresar codigo de inventario para la consulta."
            End If
        End If

        If e.CommandName.ToString().Equals("VerPDFHojaRuta") Then
            Dim strURL As String = ""
            Dim strPath As String = ""
            Dim strScript As String = ""
            Dim strCodGenerado As String
            Dim strCodFormato As String

            strCodGenerado = e.CommandArgument
            strCodFormato = "2"
            Dim lobjGerencia As New clsGerencia

            tipoprueba = lobjGerencia.fn_ObtenerTipoPrueba(e.CommandArgument, 2)
           
                'strPath = "%2fNM_Reportes%2f"
                'strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer").ToString() & strPath
                'strURL = strURL + "logistica_InventarioDiario"
            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")
            strURL = strURL + strPath
            strURL = strURL + "log_hoja_ruta"
                'CAMBIO DG FIN
            strURL = strURL + "&pvchPRUEBA_NRO=" + tipoprueba.Rows(0).Item("VCH_NROPRELIMINAR").ToString()
            strURL = strURL + "&pvchTipoPrueba=" + tipoprueba.Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString().Substring(0, 1)
            strURL = strURL + "&pintCodGenFormato=" + strCodGenerado
            strURL = strURL + "&pintCodFormato=" + strCodFormato
            strURL = strURL + "&pstrTipoPrueba=" + tipoprueba.Rows(0).Item("VCH_COD_TIPOPRUEBA").ToString().Substring(0, 1)
            strURL = strURL + "&pintCodGenerado=" + strCodGenerado


                strURL = strURL + "&rc:Command=Render"
                strURL = strURL + "&rc:Toolbar=true"

                strScript = "fMostrarReporte('" & strURL & "');"
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
          
        End If
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Response.Redirect("frmListadoNuevoInsumo.aspx")
    End Sub
End Class