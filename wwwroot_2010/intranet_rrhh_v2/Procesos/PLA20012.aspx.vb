Imports NuevoMundo

Public Class PLA20012
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
#End Region

#Region "-- Variables --"
    Dim mstr_correopara1 As String, mstr_correopara2 As String
    Dim mstr_correocopia1 As String, mstr_correocopia2 As String
    Dim mstr_correocuerpo1 As String, mstr_correocuerpo2 As String
    Dim mstr_copia As String
#End Region

#Region "-- Eventos --"
    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "ECastill"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here

        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DARWIN"

        If Not Page.IsPostBack Then
            btnConsultar.Text = "Actualizar"
            hdnaprobarmasivo.Value = ""
            btnConsultar_Click(Nothing, Nothing)
        End If

        If Me.hdn1.Value = "1" Then
            btnConsultar_Click(Nothing, Nothing)
            Me.hdn1.Value = ""
        End If
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Call prc_listar()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call prc_listar()
    End Sub

    Private Sub dtgLista_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemCreated
        If e.Item.ItemIndex <> -1 Then
            Dim btnBuscar As ImageButton
            btnBuscar = CType(e.Item.FindControl("ibtBuscar"), ImageButton)
            If Not btnBuscar Is Nothing Then
                AddHandler btnBuscar.Click, AddressOf btnBuscar_Click
            End If
        End If
    End Sub

    Private Sub dtgLista_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim ldrvVista As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtBuscar"), ImageButton)

                lobjBoton.Attributes.Add("onClick", "javascript:return VerDetalle('" + ldrvVista("chr_codigo_sol") + "')")
                lobjBoton = Nothing

                ldrvVista = Nothing
        End Select
    End Sub
#End Region

#Region "-- Metodos --"

    Private Sub prc_listar()
        Try
            Dim lobjHoraExtra As New NuevoMundo.Planillas.HorasExtras
            Dim ldtsDatos As DataSet

            If lobjHoraExtra.fnc_Listar(ldtsDatos, 6, "", "", "", "", Session("@USUARIO")) = True Then
                dtgLista.DataSource = ldtsDatos.Tables(0)
                dtgLista.DataBind()
                lblCantidad.Text = Format(dtgLista.Items.Count, "#,##0")
            End If

        Catch ex As Exception
            EnviaCorreoError(ex.Message + "PLA200012-Listar Horas Extras por aprobar")
        End Try
    End Sub

    Private Sub EnviaCorreoError(ByVal strMensaje As String)
        Try
            Dim objCorreo As New clsCorreo
            Dim lstrCuerpoMensaje As String
            Dim mailMsg As System.Net.Mail.MailMessage
            mailMsg = New System.Net.Mail.MailMessage()

            lstrCuerpoMensaje = strMensaje
            Dim lstrPara As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("CorreoCC").ToString()
            Dim lstrSubject As String = "Ocurrio un Error - Pla20012"
            objCorreo.ServicioEnvioCorreo(lstrPara, "", "", lstrSubject, lstrCuerpoMensaje)

        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class