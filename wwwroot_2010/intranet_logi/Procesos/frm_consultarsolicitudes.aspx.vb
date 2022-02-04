Public Class frm_consultarsolicitudes
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents TxtCodigoCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNombreCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents lblMsg As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            'Session("@USUARIO") = "EPOMA"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.txtFechaIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
            Me.txtFechaFin.Text = Now.ToString("dd/MM/yyyy")

            '20120910 EPM Readonly
            txtNombreCliente.Attributes.Add("readonly", "readonly")
            txtFechaIni.Attributes.Add("readonly", "readonly")
            txtFechaFin.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Function fValidaFiltros() As Boolean

        Dim validation As Boolean
        Dim mensaje = ""
        validation = True

        Me.lblMsg.Text = ""

        lblMsg.ForeColor = Drawing.Color.Red

        If Me.txtFechaIni.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Inicial de Busqueda ! <br>"
            validation = False
        End If

        If Me.txtFechaFin.Text.Length.Equals(0) Then
            mensaje += "Ingrese Fecha Final de Busqueda ! <br>"
            validation = False
        End If
        If Not IsDate(Me.txtFechaIni.Text) Then
            mensaje += "La Fecha Inicial de Busqueda es incorrecta !<br>"
            validation = False
        End If
        If Not IsDate(Me.txtFechaFin.Text) Then
            mensaje += "La Fecha Final de Busqueda es incorrecta !<br>"
            validation = False
        End If
        If IsDate(Me.txtFechaIni.Text) And IsDate(Me.txtFechaFin.Text) Then
            If CDate(Me.txtFechaIni.Text) > CDate(Me.txtFechaFin.Text) Then
                mensaje += "La Fecha Inicial de Busqueda no puede ser mayor a la Fecha Final de Busqueda !<br>"
                validation = False
            End If
        End If

        If Not validation Then
            Me.lblMsg.Text = mensaje
        End If

        Return validation
    End Function

#Region "-- Metodos --"
    Public Sub prc_listar()
        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim pDT As New DataTable
        lobjMuestrasTela.ConsultarSolicitudMuestra(pDT, Me.TxtCodigoCliente.Text, Me.txtFechaIni.Text, Me.txtFechaFin.Text)
        dtgLista.DataSource = pDT
        dtgLista.DataBind()
    End Sub
#End Region

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If Not fValidaFiltros() Then Exit Sub
        prc_listar()
    'ClientScript.RegisterStartupScript(Me.[GetType](),"alerta", "<script language=javascript>alert('Datos Grabados Correctamente.!');</script>")
    End Sub

    Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnSeleccion As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnSeleccion"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnSeleccion.Attributes.Add("onClick", "btnSeleccion_Onclick('" & drvDatos("NROSOL") & "')")
        End If
    End Sub
End Class
