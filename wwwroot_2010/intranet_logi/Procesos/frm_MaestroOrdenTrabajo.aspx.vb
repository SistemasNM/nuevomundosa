Imports NM_General

Public Class frm_MaestroOrdenTrabajo
    Inherits System.Web.UI.Page

    Private Sub frm_MaestroOrdenTrabajo_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
        Ajax.Utility.RegisterTypeForAjax(GetType(frm_MaestroOrdenTrabajo))

        If Not IsPostBack Then
            'txtCodOT.Attributes.Add("readonly", "readonly")
            'txtCodRespo.Attributes.Add("readonly", "readonly")
            'ActualizaPanelModificarOT()
            CargarAnio()
        End If
    End Sub
    'Private Sub ActualizaPanelModificarOT()
    '    If rdbModificar.Checked Then
    '        pnlModificar.Visible = True
    '        pnlAgregar.Visible = False
    '    Else
    '        pnlModificar.Visible = False
    '        pnlAgregar.Visible = True
    '    End If
    'End Sub


    Private Sub CargarAnio()
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable
        dt = objLogistica.ListarAnioOT()
        ddlAno.DataSource = dt
        ddlAno.DataTextField = "ANIO"
        ddlAno.DataValueField = "ANIO"
        ddlAno.DataBind()
        ddlAno.Dispose()
    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable

        'If txtCodOT.Text = "" Then
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('Debe Ingresar Orden de Trabajo');</script>")
        '    Exit Sub
        'End If

        dt = objLogistica.ObtenerDatosOrdenTrabajo(txtCodOT.Text, ddlResponsableF.SelectedValue, ddlAno.SelectedValue)
        If dt.Rows.Count > 0 Then
            dgOT.DataSource = dt
            dgOT.DataBind()

        Else
            dgOT.DataSource = Nothing
            dgOT.DataBind()
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('No se encontraron.');</script>")
        End If

    End Sub

    Private Sub CargarDatosOT()
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable

        dt = objLogistica.ObtenerDatosOrdenTrabajo(txtCodOT.Text, ddlResponsableF.SelectedValue, ddlAno.SelectedValue)
        If dt.Rows.Count > 0 Then
            dgOT.DataSource = dt
            dgOT.DataBind()

        Else
            dgOT.DataSource = Nothing
            dgOT.DataBind()
        End If
    End Sub

    Private Sub dgOT_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOT.CancelCommand
        dgOT.EditItemIndex = -1
        dgOT.ShowFooter = True
        CargarDatosOT()
    End Sub

    Private Sub dgOT_DeleteCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOT.DeleteCommand
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable
        dgOT.ShowFooter = True
        'Dim Num_OT As Integer = CType(CType(e.Item.FindControl("txtlIdEdi"), TextBox).Text, Integer)
        Dim Num_OT As String = CType(e.Item.FindControl("txtlId"), Label).Text
        dt = objLogistica.EliminarDatosOrdenTrabajo(Num_OT)
        CargarDatosOT()
    End Sub

    Private Sub dgOT_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOT.EditCommand
        dgOT.EditItemIndex = e.Item.ItemIndex
        dgOT.ShowFooter = False
        CargarDatosOT()
    End Sub

    Private Sub dgOT_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOT.ItemCommand
        If e.CommandName = "AddNewRow" Then

            Dim objLogistica As New NM_Logistica
            Dim dt As DataTable

            'If ddlResponsableF.SelectedValue = "" Then
            '    ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('Para Agregar Nueva OT debe seleccionar Responable');</script>")
            '    Exit Sub
            'End If



            Dim OT As String = CType(e.Item.FindControl("txtOTFoot"), TextBox).Text

            If OT = "" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('Para Agregar Nueva OT debe ingresar Numero de OT');</script>")
                Exit Sub
            End If

            Dim Costo As String = CType(e.Item.FindControl("txtCeCoFoot"), TextBox).Text
            Dim Gasto As String = CType(e.Item.FindControl("txtCuGaFo"), TextBox).Text
            Dim Obser As String = CType(e.Item.FindControl("txtObsFoot"), TextBox).Text
            Dim Imp1 As Decimal = IIf(CType(e.Item.FindControl("txtImp1Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp1Foot"), TextBox).Text)
            Dim Imp2 As Decimal = IIf(CType(e.Item.FindControl("txtImp2Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp2Foot"), TextBox).Text)
            Dim Imp3 As Decimal = IIf(CType(e.Item.FindControl("txtImp3Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp3Foot"), TextBox).Text)
            Dim Imp4 As Decimal = IIf(CType(e.Item.FindControl("txtImp4Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp4Foot"), TextBox).Text)
            Dim Imp5 As Decimal = IIf(CType(e.Item.FindControl("txtImp5Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp5Foot"), TextBox).Text)
            Dim Imp6 As Decimal = IIf(CType(e.Item.FindControl("txtImp6Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp6Foot"), TextBox).Text)
            Dim Imp7 As Decimal = IIf(CType(e.Item.FindControl("txtImp7Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp7Foot"), TextBox).Text)
            Dim Imp8 As Decimal = IIf(CType(e.Item.FindControl("txtImp8Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp8Foot"), TextBox).Text)
            Dim Imp9 As Decimal = IIf(CType(e.Item.FindControl("txtImp9Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp9Foot"), TextBox).Text)
            Dim Imp10 As Decimal = IIf(CType(e.Item.FindControl("txtImp10Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp10Foot"), TextBox).Text)
            Dim Imp11 As Decimal = IIf(CType(e.Item.FindControl("txtImp11Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp11Foot"), TextBox).Text)
            Dim Imp12 As Decimal = IIf(CType(e.Item.FindControl("txtImp12Foot"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp12Foot"), TextBox).Text)
            Dim DescOT As String = CType(e.Item.FindControl("txtDescOTFoot"), TextBox).Text
            Dim valueResp As String = CType(e.Item.FindControl("ddlResponsableFooter"), DropDownList).SelectedValue

            If Convert.ToString(Imp1) = "" Then
                Imp1 = 0.0
            End If
            If Convert.ToString(Imp2) = "" Then
                Imp2 = 0.0
            End If
            If Convert.ToString(Imp3) = "" Then
                Imp3 = 0.0
            End If
            If Convert.ToString(Imp4) = "" Then
                Imp4 = 0.0
            End If
            If Convert.ToString(Imp5) = "" Then
                Imp5 = 0.0
            End If
            If Convert.ToString(Imp6) = "" Then
                Imp6 = 0.0
            End If
            If Convert.ToString(Imp7) = "" Then
                Imp7 = 0.0
            End If
            If Convert.ToString(Imp8) = "" Then
                Imp8 = 0.0
            End If
            If Convert.ToString(Imp9) = "" Then
                Imp9 = 0.0
            End If
            If Convert.ToString(Imp10) = "" Then
                Imp10 = 0.0
            End If
            If Convert.ToString(Imp11) = "" Then
                Imp11 = 0.0
            End If
            If Convert.ToString(Imp12) = "" Then
                Imp12 = 0.0
            End If

            dt = objLogistica.AgregarDatosOrdenTrabajo(OT, valueResp, DescOT, Costo, Gasto, Obser, Imp1, Imp2, Imp3, Imp4, Imp5, Imp6, Imp7, Imp8, Imp9, Imp10, Imp11, Imp12, Session("@USUARIO"))
            CargarDatosOT()
            If dt.Rows(0).Item("RESULTADO") <> "OK" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alerta", "<script language=javascript>alert('EL NUMERO DE OT YA EXISTE');</script>")
            End If
        End If
    End Sub

    Private Sub dgOT_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgOT.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("txtlId"), Label).Attributes.Add("style", "display:none;")
            Dim Estado As String = CType(e.Item.FindControl("lblEstado"), Label).Text
            If Estado = "ANU" Then
                e.Item.Cells(0).ForeColor = Drawing.Color.Red
                e.Item.Cells(0).BorderColor = Drawing.Color.Black
                e.Item.Cells(1).ForeColor = Drawing.Color.Red
                e.Item.Cells(1).BorderColor = Drawing.Color.Black
                e.Item.Cells(2).ForeColor = Drawing.Color.Red
                e.Item.Cells(2).BorderColor = Drawing.Color.Black
                e.Item.Cells(3).ForeColor = Drawing.Color.Red
                e.Item.Cells(3).BorderColor = Drawing.Color.Black
                e.Item.Cells(4).ForeColor = Drawing.Color.Red
                e.Item.Cells(4).BorderColor = Drawing.Color.Black
                e.Item.Cells(5).ForeColor = Drawing.Color.Red
                e.Item.Cells(5).BorderColor = Drawing.Color.Black
                e.Item.Cells(6).ForeColor = Drawing.Color.Red
                e.Item.Cells(6).BorderColor = Drawing.Color.Black
                e.Item.Cells(7).ForeColor = Drawing.Color.Red
                e.Item.Cells(7).BorderColor = Drawing.Color.Black
                e.Item.Cells(8).ForeColor = Drawing.Color.Red
                e.Item.Cells(8).BorderColor = Drawing.Color.Black
                e.Item.Cells(9).ForeColor = Drawing.Color.Red
                e.Item.Cells(9).BorderColor = Drawing.Color.Black
                e.Item.Cells(10).ForeColor = Drawing.Color.Red
                e.Item.Cells(10).BorderColor = Drawing.Color.Black
                e.Item.Cells(11).ForeColor = Drawing.Color.Red
                e.Item.Cells(11).BorderColor = Drawing.Color.Black
                e.Item.Cells(12).ForeColor = Drawing.Color.Red
                e.Item.Cells(12).BorderColor = Drawing.Color.Black
                e.Item.Cells(13).ForeColor = Drawing.Color.Red
                e.Item.Cells(13).BorderColor = Drawing.Color.Black
                e.Item.Cells(14).ForeColor = Drawing.Color.Red
                e.Item.Cells(14).BorderColor = Drawing.Color.Black
                e.Item.Cells(15).ForeColor = Drawing.Color.Red
                e.Item.Cells(15).BorderColor = Drawing.Color.Black
                e.Item.Cells(16).ForeColor = Drawing.Color.Red
                e.Item.Cells(16).BorderColor = Drawing.Color.Black
                e.Item.Cells(17).ForeColor = Drawing.Color.Red
                e.Item.Cells(17).BorderColor = Drawing.Color.Black
                e.Item.Cells(18).ForeColor = Drawing.Color.Red
                e.Item.Cells(18).BorderColor = Drawing.Color.Black
                e.Item.Cells(19).ForeColor = Drawing.Color.Red
                e.Item.Cells(19).BorderColor = Drawing.Color.Black
                e.Item.Cells(20).ForeColor = Drawing.Color.Red
                e.Item.Cells(20).BorderColor = Drawing.Color.Black
                e.Item.Cells(21).ForeColor = Drawing.Color.Red
                e.Item.Cells(21).BorderColor = Drawing.Color.Black
                e.Item.Cells(22).ForeColor = Drawing.Color.Red
                e.Item.Cells(22).BorderColor = Drawing.Color.Black
            End If
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            CType(e.Item.FindControl("txtlIdEdi"), TextBox).Attributes.Add("style", "display:none;")
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            CType(e.Item.FindControl("txtlIdFot"), TextBox).Attributes.Add("style", "display:none;")
            Dim OT As TextBox = CType(e.Item.FindControl("txtOTFoot"), TextBox)
            OT.Attributes.Add("onBlur", "fValidaRespo(this ,'" & e.Item.ClientID & "_')")
            OT.Attributes.Add("onBlur", "fValidaDescOT(this ,'" & e.Item.ClientID & "_')")
            CType(e.Item.FindControl("ddlResponsableFooter"), DropDownList).Attributes.Add("readonly", "readonly")
            'CType(e.Item.FindControl("ddlResponsableFooter"), DropDownList).Attributes.Add("disabled", "true")
        End If

    End Sub

    Private Sub dgOT_UpdateCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOT.UpdateCommand
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable
        dgOT.ShowFooter = True
        Dim Num_OT As String = CType(e.Item.FindControl("txtlIdEdi"), TextBox).Text
        Dim DescOT As String = CType(e.Item.FindControl("lblDescOTEdit"), TextBox).Text
        Dim Costo As String = CType(e.Item.FindControl("txtCeCo"), TextBox).Text
        Dim Gasto As String = CType(e.Item.FindControl("txtCuGa"), TextBox).Text
        Dim Obser As String = CType(e.Item.FindControl("txtObs"), TextBox).Text
        Dim Imp1 As Decimal = IIf(CType(e.Item.FindControl("txtImp1"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp1"), TextBox).Text)
        Dim Imp2 As Decimal = IIf(CType(e.Item.FindControl("txtImp2"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp2"), TextBox).Text)
        Dim Imp3 As Decimal = IIf(CType(e.Item.FindControl("txtImp3"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp3"), TextBox).Text)
        Dim Imp4 As Decimal = IIf(CType(e.Item.FindControl("txtImp4"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp4"), TextBox).Text)
        Dim Imp5 As Decimal = IIf(CType(e.Item.FindControl("txtImp5"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp5"), TextBox).Text)
        Dim Imp6 As Decimal = IIf(CType(e.Item.FindControl("txtImp6"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp6"), TextBox).Text)
        Dim Imp7 As Decimal = IIf(CType(e.Item.FindControl("txtImp7"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp7"), TextBox).Text)
        Dim Imp8 As Decimal = IIf(CType(e.Item.FindControl("txtImp8"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp8"), TextBox).Text)
        Dim Imp9 As Decimal = IIf(CType(e.Item.FindControl("txtImp9"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp9"), TextBox).Text)
        Dim Imp10 As Decimal = IIf(CType(e.Item.FindControl("txtImp10"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp10"), TextBox).Text)
        Dim Imp11 As Decimal = IIf(CType(e.Item.FindControl("txtImp11"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp11"), TextBox).Text)
        Dim Imp12 As Decimal = IIf(CType(e.Item.FindControl("txtImp12"), TextBox).Text = "", 0, CType(e.Item.FindControl("txtImp12"), TextBox).Text)


        If Convert.ToString(Imp1) = "" Then
            Imp1 = 0.0
        End If
        If Convert.ToString(Imp2) = "" Then
            Imp2 = 0.0
        End If
        If Convert.ToString(Imp3) = "" Then
            Imp3 = 0.0
        End If
        If Convert.ToString(Imp4) = "" Then
            Imp4 = 0.0
        End If
        If Convert.ToString(Imp5) = "" Then
            Imp5 = 0.0
        End If
        If Convert.ToString(Imp6) = "" Then
            Imp6 = 0.0
        End If
        If Convert.ToString(Imp7) = "" Then
            Imp7 = 0.0
        End If
        If Convert.ToString(Imp8) = "" Then
            Imp8 = 0.0
        End If
        If Convert.ToString(Imp9) = "" Then
            Imp9 = 0.0
        End If
        If Convert.ToString(Imp10) = "" Then
            Imp10 = 0.0
        End If
        If Convert.ToString(Imp11) = "" Then
            Imp11 = 0.0
        End If
        If Convert.ToString(Imp12) = "" Then
            Imp12 = 0.0
        End If

        dt = objLogistica.ActualizarDatosOrdenTrabajo(Num_OT, DescOT, Costo, Gasto, Obser, Imp1, Imp2, Imp3, Imp4, Imp5, Imp6, Imp7, Imp8, Imp9, Imp10, Imp11, Imp12, Session("@USUARIO"))
        dgOT.EditItemIndex = -1
        CargarDatosOT()
    End Sub


    'Private Sub rbnAgregar_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnAgregar.CheckedChanged
    '    rdbModificar.Checked = False
    '    ActualizaPanelModificarOT()
    'End Sub

    'Private Sub rdbModificar_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbModificar.CheckedChanged
    '    rbnAgregar.Checked = False
    '    ActualizaPanelModificarOT()
    'End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function ValidarOT(ByVal strCodigoOT As String) As String
        Dim objLogistica As New NM_Logistica
        Dim dato As String
        dato = objLogistica.ValidarDescripcionOT(strCodigoOT)
        Return dato
    End Function
End Class