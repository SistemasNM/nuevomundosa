Public Class frm_GeneraInventarioTelas
    Inherits System.Web.UI.Page


#Region "-- Eventos --"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Init

        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then

            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
                objRequest = Nothing
            End If

            Session("@EMPRESA") = "01"
            'Session("@USUARIO") = "BENITO"

            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("/intranet/finsesion.htm")
            End If

        End If
        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub btnEjecutar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEjecutar.Click
        Call EjecutarGeneracion()
    End Sub

#End Region

#Region "-- Metodos --"


    Private Sub EjecutarGeneracion()
        Dim lobj_almacen As New NuevoMundo.clsAlmacen
        Try

            If lobj_almacen.Generar_Inventario_AlmacenTelas(Session("@EMPRESA")) = True Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('Se realizo la generacion');</script>")
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
        End Try

        lobj_almacen = Nothing
    End Sub

#End Region


End Class

