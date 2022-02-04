Imports NuevoMundo

Public Class frm_InventarioTelasERI_Detalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtCodigo.Text = Request.QueryString("vch_CodUbicacion")

            cargarRollosFaltantes(Request.QueryString("vch_CodInventario"), Request.QueryString("vch_CodUbicacion"))

        End If
    End Sub

    Sub cargarRollosFaltantes(ByVal strCodInventario As String, ByVal strCodigoUbic As String)

        Dim objArticulo As New clsArticulo
        Dim dtResponse As DataTable

        dtResponse = objArticulo.obtenerRollosFaltantes(strCodInventario, strCodigoUbic)


        dgDatos.DataSource = dtResponse
        dgDatos.DataBind()

    End Sub



    Protected Sub dgDatos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblEstado As String = DataBinder.Eval(e.Row.DataItem, "VCH_ESTADO").ToString()
            Dim lblCantDif As Label = CType(e.Row.FindControl("lblDif"), Label)


            lblCantDif.Text = Convert.ToDouble(lblCantDif.Text.Trim).ToString("##,##0.00")

            If lblEstado.Equals("FALTANTE") Then
                lblCantDif.BackColor = Drawing.Color.Red
                lblCantDif.ForeColor = Drawing.Color.White
                lblCantDif.Font.Bold = True
            ElseIf lblEstado.Equals("SOBRANTE") Then
                lblCantDif.BackColor = Drawing.Color.Blue
                lblCantDif.ForeColor = Drawing.Color.White
                lblCantDif.Font.Bold = True
            End If

        End If
    End Sub
End Class