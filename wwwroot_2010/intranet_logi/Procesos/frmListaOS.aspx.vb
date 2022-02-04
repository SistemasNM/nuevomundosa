Partial Public Class frmListaOS
  Inherits System.Web.UI.Page

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    Session("@EMPRESA") = "01"
    'Session("@USUARIO") = "ATORRESC"

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

  Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      wdpFecIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
      wdpFecFin.Text = Now.ToString("dd/MM/yyyy")
      txtNombreProveedor.Attributes.Add("readonly", "readonly")
      txtSerie.Text = "0002"
      txtNumOrden.Attributes.Add("onBlur", "FormatearBusqDoc(2);")
    Else
      sListadoServicios()
    End If
  End Sub

  Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
    If fValida() = True Then
      sListadoServicios()
    End If
  End Sub

  Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
      Dim btnSeleccion As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnSeleccion"), HtmlControls.HtmlInputButton)
      Dim btnReporte As ImageButton = CType(e.Item.FindControl("btnReporte"), ImageButton)
      Dim lstrURL As String

      Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
      lstrURL = "../CrystalReports/rpt_OrdenServicio.asp?vchCodigoEmpresa=01" & "&vchCodigoOS=" & drvDatos.Item(0).ToString

      btnSeleccion.Attributes.Add("onclick", "btnSeleccion_Onclick('" & drvDatos("var_Numero") & "')")
      btnReporte.Attributes.Add("onclick", "btnReporte_Onclick('" & lstrURL & "')")
    End If
  End Sub

#Region "Procedimientos"

  Private Function fValida() As Boolean
    '----------------------------------------------------------------------------------
    'Objetivo   : Valida el parametros
    'Autor      : Alexander Torres
    'Modificado : Junio 2013
    '----------------------------------------------------------------------------------
    Dim blnValida As Boolean = True

    If Trim(txtNumOrden.Text).Length = 0 Then
      If cmbOpcion.SelectedValue = "00" Or cmbOpcion.SelectedItem.Text = "Seleccionar estado" Then
        cmbOpcion.Focus()
        lblMsg.Text = "Error: Por favor seleccione un estado del documento."
        blnValida = False
      End If
      If wdpFecIni.Value > wdpFecFin.Value Then
        lblMsg.Text = "Error: Verifique las fechas para la consulta."
        blnValida = False
      End If
    End If
    Return blnValida

  End Function

  Private Sub sListadoServicios()
    '-------------------------------------------------------------------------
    'Objetivo   : Muestra listado de ordenes de Servicio para su calificaccion
    'Autor      : Darwin Ccorahua Livon
    'Creado     : 04/10/2011

    'Modificado : Junio 2013
    'Alexander Torres Cardenas
    'Se aumenta y validan filtros para la consultar
    '-------------------------------------------------------------------------
    lblMsg.Text = ""
    Dim lobjOrdenServicio As New NuevoMundo.clsFichaProv
    Dim objDT As New DataTable
    objDT = Nothing

    Try
      lobjOrdenServicio.CodigoEmpresa = Session("@EMPRESA")
      lobjOrdenServicio.FechaInicio = Right(wdpFecIni.Text, 4) + Mid(wdpFecIni.Text, 4, 2) + Mid(wdpFecIni.Text, 1, 2)
      lobjOrdenServicio.FechaFin = Right(wdpFecFin.Text, 4) + Mid(wdpFecFin.Text, 4, 2) + Mid(wdpFecFin.Text, 1, 2)
      lobjOrdenServicio.CodigoProveedor = Trim(TxtCodigoProveedor.Text)
      lobjOrdenServicio.EstadoServicio = cmbOpcion.SelectedValue.ToString
      If Trim(txtNumOrden.Text).Length = 0 Then
        lobjOrdenServicio.NumeroOrdenServicio = ""
      Else
        lobjOrdenServicio.NumeroOrdenServicio = txtSerie.Text + "-" + Trim(txtNumOrden.Text)
      End If

      lobjOrdenServicio.MostrarOrdenesServicio(objDT)
      If objDT.Rows.Count > 0 Then
        dtgLista.DataSource = objDT
        dtgLista.DataBind()
      Else
        dtgLista.DataSource = Nothing
        dtgLista.DataBind()
        lblMsg.Text = "No existen datos para la consulta."
      End If
      lblContador.Text = "Numero de O/S: " + objDT.Rows.Count.ToString
    Catch ex As Exception
      lblMsg.Text = ex.Message
    End Try

    objDT = Nothing
    lobjOrdenServicio = Nothing
  End Sub

#End Region

End Class