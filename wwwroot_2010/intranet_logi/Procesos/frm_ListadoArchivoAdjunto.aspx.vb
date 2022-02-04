Imports NuevoMundo

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.Item("pstrTipoDoc") Is Nothing Then hdnTipoDoc.Value = Request.Item("pstrTipoDoc").ToString
        If Not Request.Item("pstrNumeroDoc") Is Nothing Then hdnCodigoDoc.Value = Request.Item("pstrNumeroDoc").ToString

        'hdnTipoDoc.Value = "RQS"
        'hdnCodigoDoc.Value = "0002-0000010756"

        btnCerrar.Attributes.Add("Onclick", "javascript:fnc_Cerrar();")

        Call prc_ObtenerRutaDestino()
        Call prc_ListarArchivoAdjunto()
    End Sub

    ' Lista los archivos adjuntos
    Private Sub prc_ListarArchivoAdjunto()
        Dim objRequisicion As New clsRequisicion
        Dim blnAdjunto As Boolean
        Dim dtbListaAdjuntos As New DataTable

        dtbListaAdjuntos = New DataTable
        dtbListaAdjuntos = Nothing

        Try
            objRequisicion.TipoDocumento = hdnTipoDoc.Value
            objRequisicion.NumeroDocumento = hdnCodigoDoc.Value
            objRequisicion.Secuencia = ""

            If hdnTipoDoc.Value = "OCM" Then
                blnAdjunto = objRequisicion.fnc_ListarAdjuntos_OrdenCompra(dtbListaAdjuntos)
            Else
                blnAdjunto = objRequisicion.fnc_ListarAdjuntosReq(dtbListaAdjuntos)
            End If

            If blnAdjunto = True And dtbListaAdjuntos.Rows.Count > 0 Then
                lblTipoDoc.Text = dtbListaAdjuntos.Rows(0).Item("Tipo")
                lblNumDoc.Text = dtbListaAdjuntos.Rows(0).Item("NumeroDoc")
                lblDescripcionDoc.Text = dtbListaAdjuntos.Rows(0).Item("Servicio")

                If Len(Trim(dtbListaAdjuntos.Rows(0).Item("CodigoArchivo").ToString)) = 0 Then
                    dgAdjuntos.DataSource = Nothing
                    dgAdjuntos.Visible = False
                Else
                    dgAdjuntos.DataSource = dtbListaAdjuntos
                    dgAdjuntos.DataBind()
                    dgAdjuntos.Visible = True
                End If
            Else
                dgAdjuntos.DataSource = Nothing
                ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('\n\r El documento no cuenta con archivos adjuntos.');</script>")
            End If
            objRequisicion = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('\n\r No se puede mostrar la lista de adjuntos.');</script>")
        End Try
    End Sub

    Private Sub dgAdjuntos_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAdjuntos.ItemDataBound
        Dim ImgIcono As New HyperLink
        Dim lblIcono As New Label
        Dim lblNombreAdjunto As New Label
        Dim ruta As String
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                lblIcono = CType(e.Item.FindControl("lblTipoAdjunto"), Label)
                ImgIcono = CType(e.Item.FindControl("hlnAbrirAdjunto"), HyperLink)
                ImgIcono.ImageUrl = "../../intranet/imagenes/" + lblIcono.Text
                ruta = hdnDestinoAbrir.Value
                ImgIcono.NavigateUrl = "javascript:fnc_AbrirDocumento('" & ruta & "/" & e.Item.DataItem("ArchivoAdjunto").ToString & "','" & Strings.Left(e.Item.DataItem("ArchivoAdjunto").ToString, 19) & "');"
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n Error al cargar datos de adjuntos.');</script>")
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

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click

    End Sub
End Class