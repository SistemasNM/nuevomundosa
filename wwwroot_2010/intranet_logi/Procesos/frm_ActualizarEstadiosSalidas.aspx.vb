Imports NM_General
Public Class frm_ActualizarEstadiosSalidas
    Inherits System.Web.UI.Page

    Private Sub frm_ActualizarEstadiosSalidas_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
        If Not IsPostBack Then

        Else
            If hdnActualizar.Value = "SI" Then
                BuscarSalidas()
                hdnActualizar.Value = "NO"
            End If
            End If
    End Sub
    


    Private Sub BuscarSalidas()
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable
        dt = objLogistica.ObtenerSalidasEstados(txtFecIni.Text, txtFecFin.Text, txtSalida.Text)
        If dt.Rows.Count > 0 Then
            grdSalida.DataSource = dt
            grdSalida.DataBind()
        Else
            grdSalida.DataSource = Nothing
            grdSalida.DataBind()
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('No se encontraron.');</script>")
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Call BuscarSalidas()
    End Sub

    Private Sub grdSalida_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdSalida.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim drvSalida As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnBuscarEstadosalida As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnBuscarEstadosalida"), HtmlControls.HtmlInputButton)
            btnBuscarEstadosalida.Attributes.Add("onclick", "btnSeleccion_Onclick('" & drvSalida("SALIDA") & "','" & Session("@USUARIO") & "')")

        End If
    End Sub
End Class