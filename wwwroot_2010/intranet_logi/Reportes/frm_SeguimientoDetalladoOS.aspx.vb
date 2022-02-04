Public Class frm_SeguimientoDetalladoOS
    Inherits System.Web.UI.Page

    Private Sub frm_SeguimientoDetalladoOS_Init(sender As Object, e As System.EventArgs) Handles Me.Init    
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
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
        If Not IsPostBack Then            
            txtFechaIni.Attributes.Add("readonly", "readonly")
            txtFechaFin.Attributes.Add("readonly", "readonly")
            txtFechaIniCont.Attributes.Add("readonly", "readonly")
            txtFechaFinCont.Attributes.Add("readonly", "readonly")
            LimpiarFormulario()
            CargarMotivosRequisicion()
            ActualizaPanelTipoReporte()            

        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        sMostrarReporte()
    End Sub

    Private Sub sMostrarReporte()
        Dim strURL As String = ""
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strEstado As String = ""
        Dim strPresupuesto As String = ""
        Dim strMotivo As String = ""
        Dim strFechaOS_Ini As String = ""
        Dim strFechaOS_Fin As String = ""
        Dim strFechaContab_Ini As String = ""
        Dim strFechaContab_Fin As String = ""
        Try
            strEstado = IIf(chkOrdenSinRuta.Checked, "ACT|", "") +
                        IIf(chkOrdenxAprobar.Checked, "ENV|", "") +
                        IIf(chkOrdenArpobada.Checked, "APR|", "")

            strPresupuesto = IIf(rdbSiPre.Checked, "S", IIf(rdbNoPre.Checked, "N", ""))
            strMotivo = ddlMotivoRequisicion.SelectedValue.ToString.Trim

            strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
            strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

            If rdbTipoReporteDeta.Checked Then
                strURL = strURL + strPath + "log_seguimiento_Detallado_OS"
                strURL = strURL & "&vch_FechaInicio=" + Format(CDate(txtFechaIni.Text), "yyyyMMdd")
                strURL = strURL & "&vch_FechaFin=" + Format(CDate(txtFechaFin.Text), "yyyyMMdd")
                strURL = strURL & "&vch_EstadoOrden=" & strEstado
                strURL = strURL & "&vch_CodigoProveedor=" & txtCodigoProveedor.Text
                strURL = strURL & "&vch_Motivo=" & strMotivo
                strURL = strURL & "&vch_Presupuestado=" & strPresupuesto
                strURL = strURL & "&vch_Grupo_Serv=" & txtGrupoServF.Text
                strURL = strURL & "&vch_Tipo_Serv=" & txtTipoServF.Text
            Else
                
                If txtFechaIniOS.Text <> "" Then strFechaOS_Ini = Format(CDate(txtFechaIniOS.Text), "yyyyMMdd")
                If txtFechaFinOS.Text <> "" Then strFechaOS_Fin = Format(CDate(txtFechaFinOS.Text), "yyyyMMdd")
                If txtFechaIniCont.Text <> "" Then strFechaContab_Ini = Format(CDate(txtFechaIniCont.Text), "yyyyMMdd")
                If txtFechaFinCont.Text <> "" Then strFechaContab_Fin = Format(CDate(txtFechaFinCont.Text), "yyyyMMdd")

                If strFechaOS_Ini = "" And strFechaOS_Fin = "" And strFechaContab_Ini = "" And strFechaContab_Fin = "" Then
                    Throw New Exception("Debe ingresar una fecha inicio y fin para el reporte.")
                Else

                    If (strFechaOS_Ini = "" Or strFechaOS_Fin = "") And (strFechaContab_Ini = "" And strFechaContab_Fin = "") Then
                        Throw New Exception("Debe ingresar una fecha inicio y fin de O/S para el reporte.")
                    ElseIf (strFechaContab_Ini = "" Or strFechaContab_Fin = "") And (strFechaOS_Ini = "" And strFechaOS_Fin = "") Then
                        Throw New Exception("Debe ingresar una fecha inicio y fin de Contabilización para el reporte.")
                    End If
                End If

                strURL = strURL + strPath + "log_seguimiento_Contabilizado_OS"
                strURL = strURL & "&vch_FechaOS_Ini=" + strFechaOS_Ini
                strURL = strURL & "&vch_FechaOS_Fin=" + strFechaOS_Fin
                strURL = strURL & "&vch_FechaContab_Ini=" + strFechaContab_Ini
                strURL = strURL & "&vch_FechaContab_Fin=" + strFechaContab_Fin


            End If

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
            lblMensaje.Text = ""
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try

    End Sub

    Private Sub CargarMotivosRequisicion()
        Dim obj As New OFISIS.OFILOGI.Articulos(Session("@EMPRESA"), Session("@USUARIO"))
        Dim ldtable As New DataTable

        Try
            ldtable = obj.Listar_Opciones_Modulos("REQUISICIONES", "MOTIVO")

            ddlMotivoRequisicion.DataValueField = "VCH_CODIGO"
            ddlMotivoRequisicion.DataTextField = "VCH_DESCRIPCION"
            ddlMotivoRequisicion.DataSource = ldtable
            ddlMotivoRequisicion.Items.Add("SELECCIONAR MOTIVO")
            ddlMotivoRequisicion.DataBind()
            ddlMotivoRequisicion.Items.Insert(0, New ListItem("TODOS", " "))

        Catch ex As Exception
            Throw ex
        Finally
            obj = Nothing
            ldtable = Nothing
        End Try

    End Sub

    Protected Sub rdbTipoReporteDeta_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTipoReporteDeta.CheckedChanged
        ActualizaPanelTipoReporte()

    End Sub

    Protected Sub rdbTipoReporteCont_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTipoReporteCont.CheckedChanged
        ActualizaPanelTipoReporte()
    End Sub

    Private Sub ActualizaPanelTipoReporte()
        If rdbTipoReporteDeta.Checked Then
            pnlDetallado.Visible = True
            pnlContabilizado.Visible = False
        Else
            pnlDetallado.Visible = False
            pnlContabilizado.Visible = True
        End If
        LimpiarFormulario()
    End Sub

    Private Sub LimpiarFormulario()
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        txtFechaIniOS.Text = ""
        txtFechaFinOS.Text = ""
        txtFechaIniCont.Text = ""
        txtFechaFinCont.Text = ""
        chkOrdenArpobada.Checked = False
        chkOrdenSinRuta.Checked = False
        chkOrdenxAprobar.Checked = False
        txtCodigoProveedor.Text = ""
        txtNombreProveedor.Text = ""
        ddlMotivoRequisicion.SelectedIndex = -1
        rdbTodosPre.Checked = True
        txtGrupoServF.Text = ""
        txtTipoServF.Text = ""
        txtDescr_Servicios.Text = ""

        txtFechaIni.Text = "01/" + IIf(Now.Month < 10, "0" + Now.Month.ToString(), Now.Month.ToString()) + "/" + Now.Year.ToString()
        txtFechaFin.Text = Now.ToString("dd/MM/yyyy")
        lblMensaje.Text = ""
    End Sub
End Class