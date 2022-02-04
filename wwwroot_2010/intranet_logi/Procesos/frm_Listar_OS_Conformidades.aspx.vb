Public Class frm_Listar_OS_Conformidades
    Inherits System.Web.UI.Page

#Region "Eventos"

    Private Sub frm_Listar_OS_Conformidades_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
            wdpFecIni.Text = "01/" + Now.Month.ToString() + "/" + Now.Year.ToString()
            wdpFecFin.Text = Now.ToString("dd/MM/yyyy")
            txtNombreProveedor.Attributes.Add("readonly", "readonly")
            txtSerie.Text = "0002"
            txtNumOrden.Attributes.Add("onblur", "FormatearBusqDoc(2);")            
            btnBuscar.Attributes.Add("onclick", "FormatearBusqDoc(2);")
            txtCodigoProveedor.Attributes.Add("onfocus", "javascript: this.select();")
            txtSerie.Attributes.Add("onfocus", "javascript: this.select();")
            txtNumOrden.Attributes.Add("onfocus", "javascript: this.select();")
        Else
            'sListadoServicios()
        End If
    End Sub

    Private Sub dtgLista_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLista.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnSeleccion As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnSeleccion"), HtmlControls.HtmlInputButton)
            Dim btnReporte As ImageButton = CType(e.Item.FindControl("btnReporte"), ImageButton)
            Dim lstrURL As String



            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            'VALIDAR SI NO TIENE NINGUNA CONFORMIDAD PENDIENTE POR HACER.
            Dim objValidarConfor As New NuevoMundo.clsFichaProv

            If (drvDatos("flg").ToString = "N") Then
                Dim valor As DataTable
                Dim Flg As String
                valor = objValidarConfor.ValidarConformidad(drvDatos("vch_NumOrdenServicio"), drvDatos("vch_Item"))
                Flg = valor.Rows(0).Item("FLG").ToString()
                'lblMsg.Text = drvDatos("vch_NumOrdenServicio").ToString

                'lstrURL = "../CrystalReports/rpt_OrdenServicio.asp?vchCodigoEmpresa=01" & "&vchCodigoOS=" & drvDatos("vch_NumOrdenServicio").ToString
                lstrURL = "../CrystalReports/rpt_Conformidad.asp?vchCodigoEmpresa=01" & "&vchCodigoOS=" & drvDatos("vch_NumOrdenServicio").ToString & "&vchItem=" & drvDatos("vch_Item").ToString
                'btnSeleccion.Attributes.Add("onclick", "btnSeleccion_Onclick('" & drvDatos("vch_NumOrdenServicio") & "')")
                btnSeleccion.Attributes.Add("onclick", "btnSeleccion_Onclick('" & drvDatos("vch_NumOrdenServicio") & "','" & drvDatos("vch_Item") & "','" & Flg & "')")

                Dim bol As Boolean = True
                If drvDatos("vch_IdConformidad") = "" Then
                    bol = False
                Else
                    bol = True
                End If
                btnReporte.Attributes.Add("onclick", "popUp('" & lstrURL & "','" & bol & "','" & drvDatos("vch_NumOrdenServicio") & "')")
            Else
                lstrURL = "../CrystalReports/rpt_OrdenServicio.asp?vchCodigoEmpresa=01" & "&vchCodigoOS=" & drvDatos("vch_NumOrdenServicio").ToString
                btnSeleccion.Attributes.Add("onclick", "btnSeleccion_Onclick_Antiguo('" & drvDatos("vch_NumOrdenServicio") & "')")
                'btnReporte.Attributes.Add("onclick", "btnReporte_Onclick('" & lstrURL & "')")
                btnReporte.Attributes.Add("onclick", "popUp('" & lstrURL & "')")
            End If

        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        If fValida() = True Then
            sListadoServicios()
        End If
    End Sub

#End Region


#Region "Metodos"

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
            lobjOrdenServicio.CodigoProveedor = Trim(txtCodigoProveedor.Text)
            lobjOrdenServicio.EstadoServicio = cmbOpcion.SelectedValue.ToString
            If Trim(txtNumOrden.Text).Length = 0 Then
                lobjOrdenServicio.NumeroOrdenServicio = ""
            Else
                lobjOrdenServicio.NumeroOrdenServicio = txtSerie.Text + "-" + Right("0000000000" + txtNumOrden.Text.Trim, 10)
            End If

            lobjOrdenServicio.ObtenerListadoOrdenesServicio(objDT)
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
        Finally
            objDT = Nothing
            lobjOrdenServicio = Nothing
        End Try

        
    End Sub


#End Region


    'Private Sub dtgLista_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dtgLista.SelectedIndexChanged
    '    lblMsg.Text = "entro"
    'End Sub

End Class