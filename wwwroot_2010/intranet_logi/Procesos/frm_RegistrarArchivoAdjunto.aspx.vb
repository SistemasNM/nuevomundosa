Imports NuevoMundo
Imports System.IO
'--------------------------------------------------------------------------------------------------------
'--- Autor:Alexander Torres Cardenas
'--- Fecha: Febreo 2013
'--- Cia Nuevo Mundo
'--- Listar y editar los adjuntos
'--------------------------------------------------------------------------------------------------------

Public Class frm_RegistrarArchivoAdjunto
    Inherits System.Web.UI.Page

    Dim dtbListaAdjuntos As DataTable = Nothing

#Region " Web Form Designer Generated Code "
    Dim javascript As String

    Private Property objlblNombreAdjunto As Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub

    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnCerrar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAgregar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAbrirAdjunto As System.Web.UI.WebControls.Button
    Protected WithEvents hdnCodigoCtc As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnListarTodos As System.Web.UI.WebControls.Button
    Protected WithEvents hdnAgregarArchivo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDestinoAbrir As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDestinoGuardar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnMantenimiento As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodigoInc As System.Web.UI.HtmlControls.HtmlInputHidden

    Private designerPlaceholderDeclaration As System.Object
#End Region

#Region "Eventos-Controles del formulario"

    'Pagina Iniit
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ATORRESC"
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            Response.Redirect("/intranet/finsesion_popup.htm", True)
        End If
        InitializeComponent()
    End Sub

    'Pagina Load
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not Page.IsPostBack Then

            Call prc_ObtenerRutaDestino()
            If Not Request.Item("pstrTipoDoc") Is Nothing Then hdnTipoDoc.Value = Request.Item("pstrTipoDoc").ToString
            If Not Request.Item("pstrNumeroDoc") Is Nothing Then hdnCodigoDoc.Value = Request.Item("pstrNumeroDoc").ToString
            If Not Request.Item("pstrSecuencia") Is Nothing Then hdnSecuencia.Value = Request.Item("pstrSecuencia").ToString
            If Not Request.Item("pstrMantenimiento") Is Nothing Then hdnMantenimiento.Value = Request.Item("pstrMantenimiento").ToString

            'hdnTipoDoc.Value = "RQS"
            'hdnCodigoDoc.Value = "0002-0000010701"
            'hdnSecuencia.Value = "1"
            'hdnMantenimiento.Value = "1"

            prc_ListarArchivoAdjunto()
            btnCerrar.Attributes.Add("Onclick", "javascript:fnc_Cerrar();")
            btnAgregar.Attributes.Add("Onclick", "javascript:fnc_RegistraDocsAdjuntos();")
        End If
    End Sub
#End Region

#Region "Botones"
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Call prc_ListarArchivoAdjunto()
    End Sub
#End Region

#Region "Grilla"
    'ItemCommand
    Private Sub dgAdjuntos_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAdjuntos.ItemCommand
        Dim objRequisicion As New clsRequisicion
        Dim lstrError As String = ""
        Dim lintAccion As Int16 = 0
        Dim lintCodigoAdjunto As Integer = 0

        Dim objlblNumeroDoc As New Label
        Dim objlblSecuencia As New Label
        Dim objlblCodigoAdjunto As New Label
        Dim strRoot As String = ""
        Dim strPath As String = ""
        Dim strNombreFile As String = ""
        Try
            Select Case e.CommandName
                Case "Eliminar"
                    objlblNumeroDoc = CType(e.Item.FindControl("lblNumeroDoc"), Label)
                    objlblSecuencia = CType(e.Item.FindControl("lblSecuencia"), Label)
                    objlblCodigoAdjunto = CType(e.Item.FindControl("lblCodigoAdjunto"), Label)

                    objlblNombreAdjunto = CType(e.Item.FindControl("lblNombreAdjunto"), Label)

                    objRequisicion.NumeroDocumento = objlblNumeroDoc.Text
                    objRequisicion.Secuencia = objlblSecuencia.Text
                    objRequisicion.CodigoArchivo = objlblCodigoAdjunto.Text

                    If objRequisicion.fnc_EliminaAdjuntosReq() <> 0 Then
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Mensaje : \n Se elimino el archivo adjuntos.');</script>")

                        'eliminamos fisicamente archivo
                        strRoot = hdnDestinoGuardar.Value + "/"
                        strNombreFile = objlblNombreAdjunto.Text
                        strPath = strRoot + strNombreFile
                        If File.Exists(strPath) = True Then
                            File.Delete(strPath)
                        End If

                    Else
                        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No se puede eliminar archivo adjunto.');</script>")
                    End If

                    objRequisicion = Nothing
            End Select
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n No Error al eliminar el archivo adjunto.');</script>")
        End Try
        Call prc_ListarArchivoAdjunto()
    End Sub

    'ItemCreated
    Private Sub dgAdjuntos_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAdjuntos.ItemCreated
        Dim dgAdjuntos As DataGrid
        dgAdjuntos = CType(e.Item.FindControl("dgAdjuntos"), DataGrid)
        If Not dgAdjuntos Is Nothing Then
            AddHandler dgAdjuntos.ItemDataBound, AddressOf dgAdjuntos_ItemDataBound
            AddHandler dgAdjuntos.ItemCommand, AddressOf dgAdjuntos_ItemCommand
        End If
    End Sub

    'ItemDataBound
    Private Sub dgAdjuntos_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAdjuntos.ItemDataBound
        Dim ImgIcono As New HyperLink
        Dim lblIcono As New Label
        Dim lblNombreAdjunto As New Label
        Dim objBotonEliminar As New ImageButton
        Dim ruta As String
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                lblNombreAdjunto = CType(e.Item.FindControl("lblNombreAdjunto"), Label)
                lblIcono = CType(e.Item.FindControl("lblTipoAdjunto"), Label)
                ImgIcono = CType(e.Item.FindControl("hlnAbrirAdjunto"), HyperLink)
                ImgIcono.ImageUrl = "../../intranet/imagenes/" + lblIcono.Text
                ruta = hdnDestinoAbrir.Value
                ImgIcono.NavigateUrl = "javascript:fnc_AbrirDocumento('" + ruta + "/" + e.Item.DataItem("ArchivoAdjunto").ToString + "','" + Strings.Left(e.Item.DataItem("ArchivoAdjunto").ToString, 19) + "');"

                objBotonEliminar = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)
                If hdnMantenimiento.Value = "1" Then
                    objBotonEliminar.Enabled = True
                Else
                    objBotonEliminar.Enabled = False
                End If

            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Error : \n Error al cargar datos de adjuntos.');</script>")
        End Try
    End Sub

#End Region

#Region "Metodos-Funciones"

    'Obtener Tipos de adjunto
    Private Sub prc_ObtenerRutaDestino()
        Dim lobjGeneral As New NuevoMundo.General, ldtbRuta As DataTable
        ldtbRuta = lobjGeneral.ufn_TablaParametro_Obtener("27")
        hdnDestinoAbrir.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_ABRIR").ToString
        hdnDestinoGuardar.Value = ldtbRuta.Rows(0).Item("CTC_RUTADOCS_GUARDAR").ToString
        ldtbRuta = Nothing
        lobjGeneral = Nothing
    End Sub

    'Lista los archivos adjuntos
    Private Sub prc_ListarArchivoAdjunto()
        Dim objRequisicion As New clsRequisicion
        Dim blnAdjunto As Boolean

        dtbListaAdjuntos = New DataTable
        dtbListaAdjuntos = Nothing

        Try
            objRequisicion.TipoDocumento = hdnTipoDoc.Value
            objRequisicion.NumeroDocumento = hdnCodigoDoc.Value
            objRequisicion.Secuencia = CStr(hdnSecuencia.Value)

            blnAdjunto = objRequisicion.fnc_ListarAdjuntosReq(dtbListaAdjuntos)

            If blnAdjunto = True And dtbListaAdjuntos.Rows.Count > 0 Then
                lblTipoDoc.Text = dtbListaAdjuntos.Rows(0).Item("Tipo")
                lblNumDoc.Text = dtbListaAdjuntos.Rows(0).Item("NumeroDoc")
                lblSecuencia.Text = dtbListaAdjuntos.Rows(0).Item("Secuencia").ToString
                lblDescripcionDoc.Text = dtbListaAdjuntos.Rows(0).Item("Servicio")

                If Len(Trim(dtbListaAdjuntos.Rows(0).Item("CodigoArchivo").ToString)) = 0 Then
                    dgAdjuntos.DataSource = Nothing
                    dgAdjuntos.Visible = False
                Else
                    dgAdjuntos.DataSource = dtbListaAdjuntos
                    dgAdjuntos.DataBind()
                    dgAdjuntos.Visible = True
                End If

                If hdnMantenimiento.Value = "1" Then
                    btnAgregar.Enabled = True
                Else
                    btnAgregar.Enabled = False
                End If

            Else
                dgAdjuntos.DataSource = Nothing
                lblMensaje.Text = "El documento no cuenta con archivos adjuntos."
            End If
            objRequisicion = Nothing
        Catch ex As Exception
            lblMensaje.Text = "Error: No es posible mostrar la lista de adjuntos." & ex.Message.ToString
        End Try
    End Sub

#End Region

End Class
