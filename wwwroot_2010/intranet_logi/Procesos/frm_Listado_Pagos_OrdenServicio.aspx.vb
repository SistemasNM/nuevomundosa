Imports NuevoMundo
Imports System
Imports System.Data
Imports System.Web
'*******************************************************************************************
'Creado por:	  David Gamarra Paredes
'Fecha     :      15-05-2017
'Proposito :      Lista los pagos de la OS.
'*******************************************************************************************
Public Class frm_Listado_Pagos_OrdenServicio
    Inherits System.Web.UI.Page
#Region "Eventos"
    Private Sub frm_Listado_Pagos_OrdenServicio_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
            txtSerie.Text = "0002"
            btnBuscar.Attributes.Add("onclick", "FormatearBusqDoc(2);")
            txtSerie.Attributes.Add("onfocus", "javascript: this.select();")
            txtNumOrden.Attributes.Add("onblur", "FormatearBusqDoc(2);")
            txtNumOrden.Attributes.Add("onfocus", "javascript: this.select();")

            'Else
            '    CargarGrilla(txtNumOrden.Text, Session("@EMPRESA"))
        End If

    End Sub
#End Region


    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        If txtNumOrden.Text Is Nothing Or txtNumOrden.Text = "" Then
            lblMsg.Text = "Debe Ingresar el número de orden de servicio para realizar la busqueda"
            CargarGrilla(Nothing, Nothing)
        Else
            CargarGrilla(txtNumOrden.Text, Session("@EMPRESA"))
        End If

    End Sub
    Protected Sub CargarGrilla(ByVal txtNumOrden As String, ByVal Empresa As String)

        If (txtNumOrden Is Nothing And Empresa Is Nothing) Then
            OSGrid.DataSource = Nothing
            OSGrid.DataBind()
            lblMsg.Text = "No hay datos encontrados"
            txtMontoT.Text = ""
            TextSum.Text = ""
            Session("dtDatos1") = Nothing
            btnAceptar.Visible = True
        Else
            Dim lobjListarPagoOS As New clsEvaluar
            Dim dt As DataTable
            dt = lobjListarPagoOS.ListarPagoOS(txtSerie.Text + "-" + txtNumOrden, Empresa)
            If (dt.Rows.Count() > 0) Then
                OSGrid.DataSource = dt
                OSGrid.DataBind()

                txtMontoT.Text = Format(CType(dt.Rows(0).Item("Mont_Total"), Decimal), "#,###.00")
                Session("dtDatos1") = dt
                Session("NumOS") = txtSerie.Text + "-" + txtNumOrden
                lblMsg.Text = ""

                'If dt.Rows(0).Item("Estado").ToString() = "S" Then
                '    OSGrid.EditItemIndex = -1
                '    OSGrid.ShowFooter = False
                '    OSGrid.Columns.Item(3).Visible = False
                'Else
                '    btnAceptar.Visible = True
                '    OSGrid.Columns.Item(2).Visible = True
                '    OSGrid.Columns.Item(3).Visible = True
                '    OSGrid.ShowFooter = True
                'End If


                TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", String.Empty), "#,###.00")

            Else
                btnAceptar.Visible = True
                OSGrid.DataSource = Nothing
                OSGrid.DataBind()
                lblMsg.Text = "No hay datos encontrados"
                txtMontoT.Text = ""
                TextSum.Text = ""
                Session("dtDatos1") = Nothing
            End If

        End If

    End Sub

    Private Sub OSGrid_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.CancelCommand
        OSGrid.EditItemIndex = -1
        OSGrid.ShowFooter = True
        OSGrid.DataSource = CType(Session("dtDatos1"), DataTable)
        OSGrid.DataBind()

        TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", String.Empty), "#,###.00")

    End Sub

    Private Sub OSGrid_DeleteCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.DeleteCommand

        Dim i As Integer = e.Item.ItemIndex
        'CType(Session("dtDatos1"), DataTable).Rows(i).Delete()
        CType(Session("dtDatos1"), DataTable).Rows().RemoveAt(i)
        OSGrid.EditItemIndex = -1
        OSGrid.ShowFooter = True
        OSGrid.DataSource = CType(Session("dtDatos1"), DataTable)
        OSGrid.DataBind()

        If (IIf(IsDBNull(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", "")), "0", CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", "")) = "0") Then
            TextSum.Text = IIf(IsDBNull(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", "")), "0", CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", ""))
        Else
            TextSum.Text = Format(IIf(IsDBNull(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", "")), "0", CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", "")), "#,###.00")
        End If

    End Sub

    Private Sub OSGrid_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.EditCommand
        OSGrid.EditItemIndex = e.Item.ItemIndex
        OSGrid.ShowFooter = False
        OSGrid.DataSource = CType(Session("dtDatos1"), DataTable)
        OSGrid.DataBind()

        TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", String.Empty), "#,###.00")

    End Sub

    Private Sub OSGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.ItemCommand
        If e.CommandName = "AddNewRow" Then
            Dim txtMonto As TextBox = CType(e.Item.FindControl("txtMonto"), TextBox)
            If (txtMonto.Text Is Nothing Or txtMonto.Text = "") Then
                lblMsg.Text = "Ingrese un monto"
                Exit Sub
            Else
                Dim i As Integer = e.Item.ItemIndex
                Dim dr As DataRow = CType(Session("dtDatos1"), DataTable).NewRow
                dr("Monto") = txtMonto.Text
                dr("Mont_Total") = txtMontoT.Text
                CType(Session("dtDatos1"), DataTable).Rows.Add(dr)
                OSGrid.DataSource = CType(Session("dtDatos1"), DataTable)
                OSGrid.DataBind()

                TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", String.Empty), "#,###.00")

            End If

        End If
    End Sub

    Private Sub OSGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles OSGrid.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim txtMonto As TextBox = CType(e.Item.FindControl("txtMonto"), TextBox)

            txtMonto.Attributes.Add("onBlur", "fValidaNum(this)")
            txtNumOrden.Attributes.Add("onBlur", "fValidaNum(this)")


        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim txtMonto As TextBox = CType(e.Item.FindControl("txtMontoedt"), TextBox)
            txtMonto.Attributes.Add("onBlur", "fValidaNum(this)")


        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
            Dim lnkDelete As LinkButton = CType(e.Item.FindControl("lnkDelete"), LinkButton)
            Dim drvPagos As DataRowView = CType(e.Item.DataItem, DataRowView)
            If (drvPagos("Estado").ToString() = "S") Then
                lnkEdit.Visible = False
                lnkDelete.Visible = False
                'OSGrid.ShowFooter = False
            End If
        End If
    End Sub

    Private Sub OSGrid_UpdateCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles OSGrid.UpdateCommand

        Dim txtMontoedt As TextBox = CType(e.Item.FindControl("txtMontoedt"), TextBox)
        Dim i As Integer = e.Item.ItemIndex
        CType(Session("dtDatos1"), DataTable).Rows(i).Item(0) = CType(txtMontoedt.Text, Decimal)
        CType(Session("dtDatos1"), DataTable).Rows(i).Item(1) = txtMontoT.Text
        OSGrid.EditItemIndex = -1
        OSGrid.ShowFooter = True
        OSGrid.DataSource = CType(Session("dtDatos1"), DataTable)
        OSGrid.DataBind()

        TextSum.Text = Format(CType(Session("dtDatos1"), DataTable).Compute("SUM(Monto)", String.Empty), "#,###.00")

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try
            If validarMontos() = True Then
                Dim lobjActualizarPagosOs As New clsEvaluar

                'If (CType(Session("dtDatos1"), DataTable).Select("Estado = 'S'").Count > 0) Then 'NO MODIFICARA EL DATATABLE A TODOS CON ESTADO N , YA QUE CUENTA CON UNA CONFORMIDAD
                '    lobjActualizarPagosOs.Update(Session("NumOS"), Session("@Usuario"), Session("@EMPRESA"), CType(Session("dtDatos1"), DataTable))
                '    lblMsg.Text = "Los datos fueron actualizados correctamente"
                'Else
                '    For i As Integer = 0 To CType(Session("dtDatos1"), DataTable).Rows.Count() - 1
                '        CType(Session("dtDatos1"), DataTable).Rows(i).Item(2) = "N"
                '        CType(Session("dtDatos1"), DataTable).Rows(i).Item(3) = i + 1
                '    Next
                '    lobjActualizarPagosOs.Update(Session("NumOS"), Session("@Usuario"), Session("@EMPRESA"), CType(Session("dtDatos1"), DataTable)) 'REGISTRAR REGISTRO NUEVOS
                '    lblMsg.Text = "Los datos fueron grabados correctamente"
                'End If

                For i As Integer = 0 To CType(Session("dtDatos1"), DataTable).Rows.Count() - 1
                    If CType(Session("dtDatos1"), DataTable).Rows(i).Item("Estado").ToString <> "S" Then
                        CType(Session("dtDatos1"), DataTable).Rows(i).Item(2) = "N"
                        CType(Session("dtDatos1"), DataTable).Rows(i).Item(3) = i + 1
                    End If
                Next

                lobjActualizarPagosOs.Update(Session("NumOS"), Session("@Usuario"), Session("@EMPRESA"), CType(Session("dtDatos1"), DataTable)) 'REGISTRAR REGISTRO NUEVOS
                lblMsg.Text = "Los datos fueron grabados correctamente"

            End If
        Catch ex As Exception
            lblMsg.Text = "Ocurrio un error para grabar los datos"
        End Try

    End Sub
    Public Function validarMontos() As Boolean
        Dim sumMonto As Decimal = 0
        If (Session("dtDatos1") Is Nothing) Then
            Return False
        Else
            For i As Integer = 0 To CType(Session("dtDatos1"), DataTable).Rows.Count() - 1
                sumMonto = sumMonto + CType((CType(Session("dtDatos1"), DataTable).Rows(i).Item(0).ToString()), Decimal)
            Next
            If sumMonto > CType(txtMontoT.Text, Decimal) Then
                lblMsg.Text = "La suma de los montos no puede ser mayor al monto total."
                Return False
                'ElseIf sumMonto < CType(txtMontoT.Text, Decimal) Then
                '   lblMsg.Text = "La suma de los montos no puede ser menor al monto total."
                '    Return False
                'YA NO VALIDARA QUE EL MONTO SEA MENOR AL TOTAL
            End If
        End If
        Return True
    End Function

End Class