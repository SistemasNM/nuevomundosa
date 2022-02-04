Imports NuevoMundo.Planillas
Imports System.Drawing

Public Class PLA20013

#Region " Web Form Designer Generated Code "

    Inherits System.Web.UI.Page
    Protected WithEvents hdnCodigo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents HDN3 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN4 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HDN5 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid



    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

#End Region

#Region "-- Eventos --"

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
        Ajax.Utility.RegisterTypeForAjax(GetType(PLA20009))

        If Not Page.IsPostBack Then
            Call fnc_Consultar(True)
            'btnGrabar.Attributes.Add("onClick", "javascript:return fnc_ValidarRequisicion();")
        End If
        'txtAreaSolicitanteNombre.Text = Me.hdnAreaSolicitante.Value
        'HDN1-->
        'HDN2-->
        'HDN3-->
        'HDN4-->
        'HDN5-->
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim ldtTabla As DataTable
        Dim lobjHESol As HorasExtras
        Try
      lblMensaje.ForeColor = color.Blue
            lblMensaje.Text = ""

            Select Case e.CommandName
                Case "Update"

                    lobjHESol = New HorasExtras

                    Dim lblCodigoI As Label = CType(e.Item.FindControl("lblCodigoI"), Label)

                    lobjHESol.Codigo = lblCodigoI.Text
                    lobjHESol.Usuario = Session("@USUARIO")

                    ldtTabla = fnc_EsquemaReconocer()

                    If lobjHESol.fnc_Guardar(6, ldtTabla) = True Then
            'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('Se actualizo correctamente.');</script>")
            Call fnc_Consultar(False)
          Else
            lblMensaje.ForeColor = Color.Red
            lblMensaje.Text = lobjHESol.MensajeError
          End If

          ldtTabla = Nothing
          lobjHESol = Nothing

        Case "Delete"

          lobjHESol = New HorasExtras
          ldtTabla = fnc_EsquemaReconocer()

          Dim lblCodigoI As Label = CType(e.Item.FindControl("lblCodigoI"), Label)

          lobjHESol.Codigo = lblCodigoI.Text
          lobjHESol.Usuario = Session("@USUARIO")

          'eliminar
          If lobjHESol.fnc_Guardar(7, ldtTabla) = True Then
            'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('Se elimino la solicitud correctamente.');</script>")
            Call fnc_Consultar(False)
          Else
            'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + lobjHESol.MensajeError + "');</script>")
            lblMensaje.ForeColor = Color.Red
            lblMensaje.Text = lobjHESol.MensajeError
          End If

          ldtTabla = Nothing
          lobjHESol = Nothing
      End Select

    Catch ex As Exception

    End Try
  End Sub

  Private Sub DataGrid1_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

    Select Case e.Item.ItemType

      Case ListItemType.AlternatingItem, ListItemType.Item
        Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
        Dim btnUpdate As ImageButton = CType(e.Item.FindControl("btnUpdate"), ImageButton)

        Dim lblCodigo As Label = CType(e.Item.FindControl("lblCodigoI"), Label)
        Dim hdnCodigo As HtmlInputHidden = CType(e.Item.FindControl("hdnCodigoI"), HtmlInputHidden)


        Dim lobjBoton As ImageButton = CType(e.Item.FindControl("ibtConsultar"), ImageButton)
        Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)

        lobjBoton.Attributes.Add("onClick", "fnc_mostrardetalle('" & ldrvItem("int_codigo") & "')")
        lobjBoton = Nothing

        hdnCodigo.Value = ldrvItem("int_codigo")

        btnUpdate.Attributes.Add("onclick", "javascript:return fnc_Update('" & e.Item.ClientID.ToString & "')")
        btnDelete.Attributes.Add("onclick", "javascript:return fnc_Eliminar('" & e.Item.ClientID.ToString & "')")
    End Select
  End Sub

  Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
    lblMensaje.Text = ""
    Call fnc_Consultar(False)
  End Sub

#End Region

#Region "-- Metodos --"

  Function fnc_EsquemaReconocer() As DataTable
    Dim ldtdRes As DataTable
    ldtdRes = New DataTable
    ldtdRes.Columns.Add("p1", GetType(Integer)) 'int_codigo
    ldtdRes.Columns.Add("p2", GetType(String)) 'vch_concepto, de tipo de hora extra para el módulo de supervisores
    ldtdRes.Columns.Add("p3", GetType(String)) 'vch_codtrabajador
    ldtdRes.Columns.Add("p4", GetType(String)) 'chr_inicio
    ldtdRes.Columns.Add("p5", GetType(Integer)) 'int_duracion
    ldtdRes.Columns.Add("p6", GetType(String)) 'vch_observacion
    ldtdRes.Columns.Add("p7", GetType(String)) 'chr_fecha_sol
    ldtdRes.Columns.Add("p8", GetType(String)) 'chr_rango_marca
    Return ldtdRes
  End Function

  Function fnc_Consultar(ByVal pbln_esquema As Boolean)
    Dim lobj_planilla As New HorasExtras
    Dim ldts_datos As DataSet, lstr_fecha1 As String, lstr_fecha2 As String, lstr_situacion As String

    Try

      If lobj_planilla.fnc_Listar(ldts_datos, 7, "", "", "", "", "") = True Then
        DataGrid1.DataSource = ldts_datos.Tables(0)
        DataGrid1.DataBind()
      End If
      ldts_datos = Nothing
      lobj_planilla = Nothing


    Catch ex As Exception
      'ClientScript.RegisterStartupScript(Me.[GetType](),"mensaje", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>")
      lblMensaje.ForeColor = Color.Red
            lblMensaje.Text = ex.Message.Replace("'", "")
        Finally
        End Try
    End Function

  

#End Region

End Class

