Imports NuevoMundo
Imports System.IO

'--------------------------------------------------------------------------------------------------------
'--- Autor:Alexander Torres Cardenas
'--- Fecha: Febreo 2013
'--- Cia Nuevo Mundo
'--- Adjuntar file
'--------------------------------------------------------------------------------------------------------

Public Class frm_AdjuntarArchivo_OC
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ATORRESC"
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion_popup.htm", True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Request.Item("pstrTipoDoc") Is Nothing Then hdnTipoDoc.Value = Request.Item("pstrTipoDoc").ToString
            If Not Request.Item("pstrNumeroDoc") Is Nothing Then hdnCodigoDoc.Value = Request.Item("pstrNumeroDoc").ToString
            If Not Request.Item("pstrSecuencia") Is Nothing Then hdnSecuencia.Value = Request.Item("pstrSecuencia").ToString

            'hdnTipoDoc.Value = "RQS"
            'hdnCodigoDoc.Value = "0002-0000010701"
            'hdnSecuencia.Value = "1"

            Call prc_ObtenerTipoContenido()
            Call prc_ObtenerRutaDestino()
        End If

    End Sub

    'btnSubir_Click
    Protected Sub btnSubir_Click(sender As Object, e As EventArgs) Handles btnSubir.Click
        Dim objRequisiciones As New clsRequisicion
        Dim objCotizacion As New clsCotizacion

        Dim dtbListaAdjuntos As DataTable

        Dim intListado As Integer = 0
        Dim intMarca As Integer = 0
        Dim strTamano As String = ""
        Dim strFileName As String = ""
        Dim strNombreArchivoGuardado As String = ""
        Dim objFile As System.IO.FileInfo
        Dim blnSw As Boolean = False
        Dim intCotizado As Integer = 0
        Dim lintPunto As Integer = 0

        dtbListaAdjuntos = Nothing

        Dim lstrNombreArchivo As String
        Dim lstrtipodoc As String

        Try
            strFileName = File1.PostedFile.FileName
            objFile = New System.IO.FileInfo(strFileName)

            'Validamos datos
            If ValidaRegistro() = True Then

                'Validamos duplicidad
                objRequisiciones.TipoDocumento = hdnTipoDoc.Value
                objRequisiciones.NumeroDocumento = hdnCodigoDoc.Value
                objRequisiciones.Secuencia = CStr(hdnSecuencia.Value)
                objRequisiciones.fnc_ListarAdjuntos_OrdenCompra(dtbListaAdjuntos)
                intListado = dtbListaAdjuntos.Rows.Count()
                If intListado > 0 Then
                    For i = 0 To intListado - 1
                        intMarca = Strings.InStrRev(Trim(dtbListaAdjuntos.Rows(i).Item("Descripcion").ToString), "-")
                        If intMarca > 0 Then
                            strNombreArchivoGuardado = Strings.Mid(Trim(dtbListaAdjuntos.Rows(i).Item("Descripcion").ToString), 1, intMarca - 1)
                        Else
                            strNombreArchivoGuardado = Trim(dtbListaAdjuntos.Rows(i).Item("Descripcion").ToString)
                        End If

                        If strNombreArchivoGuardado = objFile.Name.ToString Then
                            lblMensaje.Text = "Error: El documento ya fue adjuntado previamente."
                            blnSw = True
                            Exit For
                        End If
                    Next
                End If

                ' guardar adjunto
                If blnSw = False Then
                    If File1.PostedFile.FileName <> "" Then
                        lintPunto = Strings.InStrRev(File1.PostedFile.FileName, ".")
                        If lintPunto > 0 Then
                            lstrtipodoc = Strings.Mid(File1.PostedFile.FileName, lintPunto + 1, Strings.Len(File1.PostedFile.FileName) - lintPunto)
                            lstrNombreArchivo = "OCM" & "_" & Strings.Format(Now(), "yyyyMMdd_hhmmss") & "." & lstrtipodoc

                            Dim strPath As String = hdnDestinoGuardar.Value & "\" & lstrNombreArchivo
                            strTamano = "0 KB"

                            If File.Exists(strPath) = True Then
                                lblMensaje.Text = "Error: Este documento ya fue esta adjuntado."
                                Exit Sub
                            End If
                            ' guardamos regsitro
                            File1.PostedFile.SaveAs(strPath)
                            GuardarAdjunto(strTamano, String.Format(Now(), "dd/mm/yyyy"), lstrNombreArchivo, objFile.Name)
                            ' cotizacion de rqs
                            objCotizacion.TipoDocumento = hdnTipoDoc.Value
                            objCotizacion.NumeroDocumento = hdnCodigoDoc.Value
                            objCotizacion.Secuencia = hdnSecuencia.Value
                            objCotizacion.UsuarioCotizacion = Session("@USUARIO")
                            objCotizacion.CodigoProveedor = ""
                            objCotizacion.Monto = 0
                            objCotizacion.Observaciones = ""
                            'intCotizado = objCotizacion.fnc_InsertarCotizacion()
                            'If intCotizado = 0 Then
                            '    lblMensaje.Text = "Error: Verificar cotizacion de documento."
                            'End If
                            lblMensaje.Text = "Se adjunto archivo correctamente."
                            'ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>window.opener.location.reload(); window.close();</script>")
                            'ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>window.opener.document.getElementById('btnActualizar').click(); window.close();</script>")
                        End If
                    Else
                        lblMensaje.Text = "Error: El archivo a adjuntar no es valido."
                    End If
                End If
            End If
        Catch ex As Exception
            lblMensaje.Text = "Problema al adjuntar el archivo." & ex.Message.ToString
        End Try

    End Sub

    'Validar datos
    Public Function ValidaRegistro() As Boolean
        Dim blnRpta As Boolean
        Dim strFileName As String
        strFileName = File1.PostedFile.FileName
        If Len(Trim(strFileName)) > 0 Then
            blnRpta = True
            If Mid(ddlTipoContenido.SelectedItem.Text, 1, 10) <> "Seleccione" Then
                blnRpta = True
            Else
                lblMensaje.Text = "Error: Debe elegir una categoria para el archivo a adjuntar."
                blnRpta = False
            End If
        Else
            lblMensaje.Text = "Debe elegir un archivo a adjuntar."
            blnRpta = False
        End If
        Return blnRpta
    End Function


    'Guardamos regsitro en tabla adjuntos
    Public Function GuardarAdjunto(strTamamo As String, strFecha As String, strNombreGenerado As String, strNombreOriginal As String) As Integer
        Dim objRequisiciones As New clsRequisicion
        Dim intResultado As Integer = 0
        Try
            objRequisiciones.TipoDocumento = hdnTipoDoc.Value
            objRequisiciones.NumeroDocumento = hdnCodigoDoc.Value
            objRequisiciones.Secuencia = hdnSecuencia.Value
            objRequisiciones.CodigoContenido = ddlTipoContenido.SelectedValue.ToString
            objRequisiciones.TamanoAdjunto = strTamamo
            objRequisiciones.FechaAdjunto = strFecha
            objRequisiciones.NombreAdjunto = strNombreGenerado

            If Trim(txtNombreCorto.Text).Length > 0 Then
                objRequisiciones.NombreCortoAdjunto = strNombreOriginal & "-" & Trim(txtNombreCorto.Text)
            Else
                objRequisiciones.NombreCortoAdjunto = strNombreOriginal
            End If

            objRequisiciones.ObservacionAdjunto = Trim(txtObservacion.Text)
            objRequisiciones.UsuarioCreacion = Session("@USUARIO").ToString
            intResultado = objRequisiciones.fnc_InsertarAdjuntosReq()
        Catch ex As Exception
            lblMensaje.Text = "Error: Problema en el registro de archivo adjunto." + ex.Message.ToString
        End Try
        Return intResultado
    End Function

    'Obtener Tipos de adjunto
    Private Sub prc_ObtenerTipoContenido()
        Dim lobjGeneral As New NuevoMundo.General
        Dim ldtbTipoContenido As DataTable
        Try
            ldtbTipoContenido = lobjGeneral.ufn_TablaParametro_Obtener("36")
            If ldtbTipoContenido.Rows.Count <> 0 Then
                ddlTipoContenido.DataSource = ldtbTipoContenido
                ddlTipoContenido.DataValueField = ldtbTipoContenido.Columns("vch_CodigoContenido").ColumnName.ToString()
                ddlTipoContenido.DataTextField = ldtbTipoContenido.Columns("vch_DescripcionContenido").ColumnName.ToString()
                ddlTipoContenido.DataBind()
                ddlTipoContenido.Items.Insert(0, New ListItem("Seleccione Tipo", ""))

            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede consultar los Tipo de contenidos.');</script>")
        End Try

        ldtbTipoContenido = Nothing
        lobjGeneral = Nothing

    End Sub

    'Obtener ruta Abrir/Grabar 
    Private Sub prc_ObtenerRutaDestino()
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("27")
        hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        hdnDestinoGuardar.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_GUARDAR").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
    End Sub

End Class