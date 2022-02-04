Imports NM.AccesoDatos
Public Class frm_RegistroAdjuntoIQ
    Inherits System.Web.UI.Page
    Protected WithEvents hdnCodigoPeriodoIQ As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnAgregar.Attributes.Add("Onclick", "javascript:return fnc_AdjuntarArchivo();")
            btnCerrar.Attributes.Add("Onclick", "javascript:fnc_Cerrar();")
            Call prc_ObtenerRutaDestino()

            hdnPeriodoIQ.Value = Request.Item("pstrAnno").ToString + Request.Item("pstrMes").ToString
            'hdnPeriodoIQ.Value = "201208"
            Call prc_CertificadoIQ()
        End If
    End Sub

    Private Sub prc_CertificadoIQ()
        Dim lobjIQ As NuevoMundo.clsArticulo
        Dim ldtsDatos As New DataTable
        Dim lstrError As String = ""
        ldtsDatos = Nothing
        Try
            lobjIQ = New NuevoMundo.clsArticulo
            lstrError = lobjIQ.fnc_ListarFileIQ(hdnPeriodoIQ.Value, ldtsDatos)
            lblPeriodoIQ.Text = ldtsDatos.Rows(0).Item("Periodo")

            If Not ldtsDatos Is Nothing Then
                If ldtsDatos.Rows.Count > 0 Then
                    dgArchivo.DataSource = ldtsDatos
                    dgArchivo.DataBind()
                Else
                    dgArchivo.DataSource = Nothing
                End If
            End If

            lobjIQ = Nothing
            ldtsDatos = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede mostrar la lista de adjuntos.');</script>")
        End Try

    End Sub

    Private Sub prc_ObtenerRutaDestino()
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("27")
        hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        hdnDestinoGuardar.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_GUARDAR").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If prc_GuardarArchivo() = True Then
            Call prc_ListarFilesIQ()
        End If
    End Sub

    Private Function prc_GuardarArchivo() As Boolean
        Dim lclsIQ As NuevoMundo.clsArticulo
        Dim lstrError As String = ""
        Dim sCodigo As String
        Dim sArchivo As String
        Dim sDescrip As String
        Dim sObserva As String
        Dim lstrArchivotipo As String
        Dim lintPunto As Integer = 0
        Dim blnExito As Boolean
        blnExito = True

        Try
            sCodigo = hdnPeriodoIQ.Value
            sArchivo = hdnArchivo.Value
            sDescrip = hdnDescri.Value
            sObserva = hdnObserva.Value

            lintPunto = Strings.InStrRev(sArchivo, ".")
            lstrArchivotipo = Strings.Mid(sArchivo, lintPunto + 1, Strings.Len(sArchivo) - lintPunto)

            lclsIQ = New NuevoMundo.clsArticulo

            lstrError = lclsIQ.fnc_GuardarFileIQ(sCodigo, sArchivo, sDescrip, lstrArchivotipo, sObserva)

            If lstrError.Length > 0 Then
                blnExito = False
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('" & lstrError & ".');</script>")
            End If

        Catch ex As Exception
            blnExito = False
        Finally
            lclsIQ = Nothing
        End Try
        Return blnExito
    End Function

    Private Sub prc_ListarFilesIQ()
        Dim lobjIQ As NuevoMundo.clsArticulo
        Dim ldtsDatos As DataTable = Nothing
        Dim lstrError As String = ""
        Try
            lobjIQ = New NuevoMundo.clsArticulo
            lstrError = lobjIQ.fnc_ListarFileIQ(hdnPeriodoIQ.Value, ldtsDatos)
            lblPeriodoIQ.Text = ldtsDatos.Rows(0).Item("Periodo")
            dgArchivo.DataSource = ldtsDatos
            dgArchivo.DataBind()
            lobjIQ = Nothing
            ldtsDatos = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede mostrar la lista de adjuntos.');</script>")
        End Try

    End Sub

    Private Sub dgArchivo_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgArchivo.ItemDataBound
        Dim ImgIcono As New ImageButton
        Dim lblIcono As New Label

        Dim ruta As String
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

                lblIcono = CType(e.Item.FindControl("lblTipoAdjunto"), Label)
                ImgIcono = CType(e.Item.FindControl("imgTipoArchivoI"), ImageButton)
                ImgIcono.ImageUrl = "../../intranet/imagenes/" + lblIcono.Text
                ruta = hdnDestinoAbrir.Value
                ImgIcono.Attributes.Add("Onclick", "javascript:fnc_AbrirDocumento('" + ruta + "/" + e.Item.DataItem("var_Certificado").ToString + "','" + Strings.Left(e.Item.DataItem("var_Certificado").ToString, 19) + "');")
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n Error al cargar datos de adjuntos.');</script>")
        End Try
    End Sub

End Class