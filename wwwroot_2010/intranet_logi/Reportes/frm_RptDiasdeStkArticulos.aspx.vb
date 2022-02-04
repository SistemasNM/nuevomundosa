Imports System.Data

Partial Class frm_RptDiasdeStkArticulos
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

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "EPOMA"

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

            Dim oFiltrosRep As New NuevoMundo.clsReportesIndicadores()

            Try
                Me.cmbTipoArticulo.DataSource = oFiltrosRep.fListarTipoArticulos()
                Me.cmbTipoArticulo.DataValueField = "ti_item"
                Me.cmbTipoArticulo.DataTextField = "de_tipo_item"
                Me.cmbTipoArticulo.DataBind()
                Me.cmbTipoArticulo.Items.Insert(0, New ListItem("SELECCIONE", ""))

                Me.cmbAlmacen.DataSource = oFiltrosRep.fListarAlmacen()
                Me.cmbAlmacen.DataValueField = "co_alma"
                Me.cmbAlmacen.DataTextField = "de_alma"
                Me.cmbAlmacen.DataBind()
                Me.cmbAlmacen.Items.Insert(0, New ListItem("SELECCIONE", ""))

                Me.tr_TipoModComp.Visible = False
            Catch ex As Exception
                Throw ex
            Finally
                oFiltrosRep = Nothing
            End Try

        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        If Not fValidaFiltros() Then Exit Sub
        sMostrarReporte()
    End Sub

    Private Sub sMostrarReporte()

        Dim strURL As String
        Dim strPath As String
        Dim strScript As String
        strPath = "%2fLogistica%2f"

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServer") & strPath
        txtUrl.Text = strURL

        If Me.rbListModelos.SelectedItem.Value = 1 Then ' MODELO 1
            strURL = strURL + "ha_diasdestkporart"
            strURL = strURL + "&par_tipoart=" + Me.cmbTipoArticulo.SelectedValue
            strURL = strURL + "&par_co_alma=" + Me.cmbAlmacen.SelectedValue
        End If

        If Me.rbListModelos.SelectedItem.Value = 2 Then ' MODELO 2
            strURL = strURL + "ha_diasdestkporart_mod2"
            strURL = strURL + "&par_tipoart=" + Me.cmbTipoArticulo.SelectedValue
            strURL = strURL + "&par_co_alma=" + Me.cmbAlmacen.SelectedValue
            strURL = strURL + "&par_ti_comp_repo=" + Me.cmbTipoModComp.SelectedValue
        End If


        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"

        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

    Private Function fValidaFiltros() As Boolean

        Dim validation As Boolean
        Dim mensaje = ""
        validation = True

        Me.lblMsg.Text = ""

        lblMsg.ForeColor = Drawing.Color.Red

        If Me.cmbTipoArticulo.SelectedValue = "" Then
            mensaje += "Elija el Tipo de Artículo para la Busqueda ! <br>"
            validation = False
        End If

        If Me.cmbAlmacen.SelectedValue = "" Then
            mensaje += "Elija el Almacen para la Busqueda ! <br>"
            validation = False
        End If

        If Not validation Then
            Me.lblMsg.Text = mensaje
        End If

        Return validation

    End Function

    Protected Sub rbListModelos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbListModelos.SelectedIndexChanged
        If Me.rbListModelos.SelectedItem.Value = 2 Then
            Me.tr_TipoModComp.Visible = True
        Else
            Me.tr_TipoModComp.Visible = False
        End If
    End Sub
End Class
