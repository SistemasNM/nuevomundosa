Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_Trabajadores
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "atorresc"

        'Verificar Sesion
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

    End Sub

#Region "Metodos"

    ' listamos trabajadores
    Private Sub prcObtenerTrabajador()
        Dim lobjTrabajador As New clsTrabajador
        Dim ldtbTrabajador As New DataTable
        ldtbTrabajador = Nothing
        lblError.Text = ""
        Try
            'If fncValida() = True Then
            lobjTrabajador.CodigoGerencia = ""
            lobjTrabajador.CenCosGer = Trim(txtCodGer.Text)
            lobjTrabajador.CodigoJefatura = ""
            lobjTrabajador.CenCosJef = Trim(txtCodJef.Text)
            lobjTrabajador.CodigoSupervisor = ""
            lobjTrabajador.CenCosSup = Trim(txtCodSup.Text)
            lobjTrabajador.CodigoTrabajador = ""
            lobjTrabajador.CenCosTra = ""
            lobjTrabajador.Asignado = ddlEstado.SelectedValue
            ldtbTrabajador = lobjTrabajador.fncTrabajadorListar(Session("@EMPRESA"), ldtbTrabajador)
            If Not ldtbTrabajador Is Nothing Then
                If ldtbTrabajador.Rows.Count > 0 Then
                    dgTrabajador.DataSource = ldtbTrabajador
                    dgTrabajador.DataBind()
                    dgTrabajador.Visible = True
                    lblItems.Text = "Numero de Items: " + ldtbTrabajador.Rows.Count.ToString
                Else
                    lblItems.Text = "No existen datos para la consulta."
                    dgTrabajador.DataSource = Nothing
                    dgTrabajador.DataBind()
                End If
            Else
                lblError.Text = "No existen gerencias a consultar"
            End If
            'End If
        Catch ex As Exception
            lblError.Text = "Error al consultar gerencias. " + ex.Message.ToString
        End Try
    End Sub

    ' validamos parametros
    Private Function fncValida() As Boolean
        Dim lblnValida As Boolean = True
        lblError.Text = ""

        If Trim(txtCodGer.Text).Length = 0 Then
            lblError.Text = "Error, debe ingresar una gerencia para la consulta."
            lblnValida = False
            Return lblnValida
            Exit Function
        End If

        If Trim(txtCodJef.Text).Length = 0 Then
            lblError.Text = "Error, debe ingresar una jefatura de la gerencia para la consulta."
            lblnValida = False
            Return lblnValida
            Exit Function
        End If
        Return lblnValida
    End Function

    ' asignamos trabajador a supervisor
    Private Sub prcAsignarTrabajador()
        Dim objTrabajador As New clsTrabajador
        Dim dtbTrabajador As New DataTable
        Dim strCodigoSup As String = ""
        Dim strEmpleado As String = ""
        Dim lstr_Trabajadores() As String
        lblError.Text = ""
        dtbTrabajador = Nothing
        Try
            lstr_Trabajadores = Strings.Split(hdnaprobarmasivo.Value, ",")
            If Trim(lstr_Trabajadores(0)).Length > 0 Then

                objTrabajador.CenCosGer = Trim(txtCodGer.Text)
                objTrabajador.CenCosJef = Trim(txtCodJef.Text)
                strCodigoSup = ddlSupervisores.SelectedValue

                objTrabajador.CodigoGerencia = hdnCodigoGerencia.Value
                objTrabajador.CodigoJefatura = hdnCodigoJefatura.Value
                objTrabajador.CodigoSupervisor = hdnCodigoSupervisor.Value
                objTrabajador.CodigoTrabajador = hdnCodigoTrabajador.Value
                objTrabajador.CodigoEmpleadoTra = hdnCodigoEmpleado.Value

                If strCodigoSup.Length = 0 Then
                    lblError.Text = "Error, Debe elegir un supervisor."
                    Exit Sub
                Else
                    ' Agregamos trabajador en masivo
                    objTrabajador.CodigoSupervisor = strCodigoSup
                    For lint_fila = 0 To lstr_Trabajadores.Length - 2
                        strEmpleado = lstr_Trabajadores(lint_fila)
                        objTrabajador.CodigoEmpleadoTra = strEmpleado
                        If objTrabajador.fncTrabajadorAsignar(Session("@USUARIO"), dtbTrabajador) = True Then
                            lblError.Text = "Se asigno el trabajador al supervisor(es) elegido."
                        Else
                            lblError.Text = "No se pudo asignar el trabajador." + strEmpleado
                        End If
                        lstr_Trabajadores(lint_fila) = ""
                    Next
                    ' Listamos
                    prcObtenerTrabajador()
                End If
            Else
                lblError.Text = "Error: Eliga un trabajador a asignar."
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    ' eliminamos trabajador asignado
    Private Sub prcEliminarTrabajador()
        Dim lobjTrabajador As New clsTrabajador
        Dim ldtbTrabajador As New DataTable
        ldtbTrabajador = Nothing
        lblError.Text = ""
        Try
            lobjTrabajador.CodigoGerencia = hdnCodigoGerencia.Value
            lobjTrabajador.CodigoJefatura = hdnCodigoJefatura.Value
            lobjTrabajador.CodigoSupervisor = hdnCodigoSupervisor.Value
            lobjTrabajador.CodigoTrabajador = hdnCodigoTrabajador.Value
            lobjTrabajador.CodigoEmpleadoTra = hdnCodigoEmpleado.Value

            ldtbTrabajador = lobjTrabajador.fncTrabajadorEliminar(ldtbTrabajador)
            If Not ldtbTrabajador Is Nothing Then
                If Mid(ldtbTrabajador.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                    prcObtenerTrabajador()
                    lblError.Text = ldtbTrabajador.Rows(0).Item(0)
                Else
                    lblError.Text = ldtbTrabajador.Rows(0).Item(0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    ' llenamos el combo - supervisores asignados
    Private Function fncListaSupervisoresAsignados() As Boolean
        Dim lobjSupervisor As New clsSupervisor
        Dim ldtbSupervisor As New DataTable
        Dim lblnSupervisores As Boolean = False

        ldtbSupervisor = Nothing
        lblError.Text = ""
        Try
            lobjSupervisor.CenCosGer = Trim(txtCodGer.Text)
            lobjSupervisor.CenCosJef = Trim(txtCodJef.Text)
            lobjSupervisor.CenCosSup = Trim(txtCodSup.Text)

            ldtbSupervisor = lobjSupervisor.fncSupervisorAsignadosListar(ldtbSupervisor)
            If Not ldtbSupervisor Is Nothing Then
                If Mid(ldtbSupervisor.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                    ddlSupervisores.Items.Clear()
                    ddlSupervisores.DataSource = ldtbSupervisor
                    ddlSupervisores.DataValueField = "vch_CodigoSup"
                    ddlSupervisores.DataTextField = "Supervisor"
                    ddlSupervisores.DataBind()
                    ddlSupervisores.Items.Insert(0, New ListItem("Seleccione supervisor", ""))
                    lblnSupervisores = True
                Else
                    lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
        ldtbSupervisor = Nothing
        lobjSupervisor = Nothing
        Return lblnSupervisores
    End Function

#End Region

#Region "Controles"

    ' boton consultar
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        prcObtenerTrabajador()
    End Sub

    ' boton asignar
    Protected Sub btnAsignar_Click(sender As Object, e As EventArgs) Handles btnAsignar.Click
        If fncListaSupervisoresAsignados() = True Then
            txtCodCenCos.Text = txtCodSup.Text
            txtDesCenCos.Text = lblDesSup.Text
            txtCodCenCos.Enabled = False
            btnCentroCosto.Visible = False
        Else
            'txtCodCenCos.Text = ""
            'txtCodCenCos.Text = ""
            'txtCodCenCos.Enabled = True
            'btnCentroCosto.Visible = True
            'txtCodCenCos.Focus()
        End If
        pnlEditar.Visible = True
        hdnAccion.Value = "1"
    End Sub

    ' boton cerrar
    Protected Sub btnCerrar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerrar.Click
        txtCodCenCos.Text = ""
        txtDesCenCos.Text = ""
        ddlSupervisores.Items.Clear()
        ddlSupervisores.DataSource = Nothing
        ddlSupervisores.DataBind()
        pnlEditar.Visible = False
    End Sub

    ' boton grabar
    Protected Sub btnGrabar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGrabar.Click
        prcAsignarTrabajador()
    End Sub

    ' grilla
    Private Sub dgTrabajador_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTrabajador.ItemCommand
        Dim strCodCencos As String = ""
        Dim strCodEmpelado As String = ""
        Try
            Select Case e.CommandName
                Case "Eliminar"
                    hdnCodigoGerencia.Value = CType(e.Item.FindControl("lblCodGerencia"), Label).Text
                    hdnCodigoJefatura.Value = CType(e.Item.FindControl("lblCodJefatura"), Label).Text
                    hdnCodigoSupervisor.Value = CType(e.Item.FindControl("lblCodSupervisor"), Label).Text
                    hdnCodigoTrabajador.Value = CType(e.Item.FindControl("lblCodTrabajador"), Label).Text
                    hdnCodigoEmpleado.Value = CType(e.Item.FindControl("lblCodEmp"), Label).Text
                    ' eliminamos
                    prcEliminarTrabajador()
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub dgTrabajador_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTrabajador.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                Dim ldrvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim lobjAsignar As CheckBox = CType(e.Item.FindControl("chkSeleccion"), CheckBox)
                Dim lobjEliminar As ImageButton = CType(e.Item.FindControl("btnEliminarItem"), ImageButton)
                Dim objlblTra As Label = CType(e.Item.FindControl("lblCodTrabajador"), Label)
                Dim objlblEmpleado As Label = CType(e.Item.FindControl("lblCodEmp"), Label)

                Dim strCodTrab As String = ""
                Dim strEmp As String = ""

                strCodTrab = Trim(objlblTra.Text)
                If strCodTrab.Length > 0 Then
                    lobjAsignar.Visible = False
                    lobjEliminar.Visible = True
                Else
                    lobjAsignar.Visible = True
                    lobjEliminar.Visible = False
                End If
                strEmp = objlblEmpleado.Text
                lobjAsignar.Attributes.Add("onClick", "fnc_aprobarmasivo(this,'" + strEmp + "')")
        End Select
    End Sub
#End Region
End Class