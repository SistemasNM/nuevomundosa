Public Class frm_AjaxValidRegistrarDespachoMuestras
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

    Public vrRes As String

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim vrCodArt As String = Trim("" & Request.QueryString("codart"))
        Dim vrCodLote As String = Trim("" & Request.QueryString("codlote"))
        Dim vrCantSol As String = Trim("" & Request.QueryString("cantsol"))


        If vrCodArt = "" Or vrCodLote = "" Then
            vrRes = "Datos Incorrectos."
            Exit Sub
        End If


        Dim lobjMuestrasTela As New OFISIS.OFILOGI.Muestras_Telas
        Dim pDT As New DataTable

        Try
            lobjMuestrasTela.FunEvaluaArtLote(pDT, vrCodArt, vrCodLote)

            If pDT Is Nothing Or pDT.Rows.Count = 0 Then
                vrRes = "Lote No existe."
            Else
                If CDec("0" & vrCantSol) > CDec("0" & pDT.Rows(0).Item("var_ca_docu_movi").ToString()) Then
                    vrRes = "Cant.Insuficiente (Disp = " & Format(pDT.Rows(0).Item("var_ca_docu_movi"), "#####0.00") & ")"
                Else
                    vrRes = "Todo OK."
                End If
            End If
        Catch ex As Exception
            Response.Redirect("/intranet/finsesion_popup.htm")
        Finally
            lobjMuestrasTela = Nothing
            pDT.Dispose() : pDT = Nothing
        End Try
    End Sub

End Class
