Imports System.Globalization
Public Class frm_PedidosEtiquetasPendientes
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "DGAMARRA"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

        End If
    End Sub

    'REPORTE PEDIDOS PENDIENTS
    Private Sub btnReporte1_Click(sender As Object, e As System.EventArgs) Handles btnReporte1.Click
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

        strURL = strURL + strPath + "log_reporte_pedidos_etiquetas_pendientes"
        strURL = strURL & "&rs:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub
    'REPORTE PEDIDOS DESPACHADOS
    Private Sub btnReporte2_Click(sender As Object, e As System.EventArgs) Handles btnReporte2.Click
        Dim strPath As String = ""
        Dim strScript As String = ""
        Dim strURL As String
        Dim strfechadesde As String
        Dim strfechahasta As String
        strfechadesde = ValidayConvierte_FechaCorrecta(txtFecha_Inicio, "Fecha Inicial")
        strfechahasta = ValidayConvierte_FechaCorrecta(txtFecha_Final, "Fecha Final")

        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloLogistica")

        If ValidarFecha() = True Then

            strURL = strURL + strPath + "log_reporte_pedidos_etiquetas_despachados"

            strURL = strURL + "&vchcoemp=" + Session("@EMPRESA")
            strURL = strURL + "&dtFecIni=" + strfechadesde
            strURL = strURL + "&dtFecFin=" + strfechahasta
            strURL = strURL + "&vchcocli=" + IIf(Me.txtCodigoCliente.Text = "", " ", Me.txtCodigoCliente.Text)

            strURL = strURL & "&rs:Command=Render"
            strURL = strURL + "&rc:Toolbar=true"
            strScript = "fMostrarReporte('" & strURL & "');"
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)
        End If
    End Sub
    Function ValidayConvierte_FechaCorrecta(ByRef objCajaTexto As TextBox, ByVal strTituloCampo As String) As String
        Dim dtmFecTrasIni As Date
        Dim strResult As String
        Dim strMensaje As String

        Try
            If Trim(objCajaTexto.Text) <> "" Then
                DateTime.TryParseExact(objCajaTexto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dtmFecTrasIni)
                strResult = dtmFecTrasIni.ToString("yyyyMMdd")
            Else
                strResult = ""
            End If

            If strResult = "00010101" Then
                objCajaTexto.Focus()
                strMensaje = "El campo " & strTituloCampo & " no contiene una fecha valida."
                Throw New Exception(strMensaje)
            End If
        Catch ex As Exception
            Throw ex
        End Try


        Return strResult

    End Function
    Private Function ValidarFecha() As Boolean
        If (txtFecha_Inicio.Text = "") Then
            Me.txtFecha_Inicio.Focus()
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Debe elegir una fecha de inicio');</script>")
            Return False
        End If

        If (txtFecha_Final.Text = "") Then
            Me.txtFecha_Final.Focus()
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('Debe elegir fecha fin');</script>")
            Return False
        End If

        Dim FechIni As Date = CType(txtFecha_Inicio.Text, Date)
        Dim FechFin As Date = CType(txtFecha_Final.Text, Date)
        Dim result As Integer = DateTime.Compare(FechIni, FechFin)

        If (result > 0) Then
            Me.txtFecha_Inicio.Focus()
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('La fecha de inicio no debe ser mayor a la fecha Fin');</script>")
            Return False
        End If

        Return True
    End Function


End Class