Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_Supervisores
    Inherits System.Web.UI.Page

    ' inicio de pagina
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

    ' carga de pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    ' obtener supervisores
    Private Sub prc_ObtenerSupervisor()
        Dim lobjSupervisor As New clsSupervisor
        Dim ldtbSupervisor As DataTable
        ldtbSupervisor = Nothing
        lblError.Text = ""
        hdnAccion.Value = "4"
        Try
            If ValidaDatos() = True Then
                lobjSupervisor.CodigoGerencia = ""
                lobjSupervisor.CenCosGer = Trim(txtCodGer.Text)
                lobjSupervisor.CodigoJefatura = ""
                lobjSupervisor.CenCosJef = Trim(txtCodJef.Text)
                lobjSupervisor.CodigoSupervisor = ""
                lobjSupervisor.CenCosSup = ""
                lobjSupervisor.CodigoEmpleadoSup = ""
                ldtbSupervisor = lobjSupervisor.fncSupervisorListar(Session("@EMPRESA"), ldtbSupervisor)
                If Not ldtbSupervisor Is Nothing Then
                    If ldtbSupervisor.Rows.Count > 0 Then
                        dgSupervisores.DataSource = ldtbSupervisor
                        dgSupervisores.DataBind()
                        dgSupervisores.Visible = True
                        lblItems.Text = "Numero de Items: " + ldtbSupervisor.Rows.Count.ToString
                    Else
                        lblItems.Text = "No existen datos para la consulta."
                        dgSupervisores.DataSource = Nothing
                        dgSupervisores.DataBind()
                    End If
                Else
                    lblError.Text = "No existen gerencias a consultar"
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al consultar gerencias. " + ex.Message.ToString
        End Try

        ldtbSupervisor = Nothing
        lobjSupervisor = Nothing

    End Sub

    ' actualizamos datos del supervisor
    Private Sub prc_ActualizaSupervisor()
        Dim lobjSupervisor As New clsSupervisor
        Dim ldtbSupervisor As New DataTable
        ldtbSupervisor = Nothing
        lblError.Text = ""

        Try
            If ValidaDatos() = True Then

                lobjSupervisor.CodigoGerencia = Trim(hdnCodigoGerencia.Value)
                lobjSupervisor.CodigoJefatura = Trim(hdnCodigoJefatura.Value)
                lobjSupervisor.CodigoSupervisor = Trim(hdnCodigoSupervisor.Value)
                lobjSupervisor.CenCosSup = Trim(txtCodCenCos.Text)
                lobjSupervisor.CodigoEmpleadoSup = Trim(txtCodEmp.Text)
                lobjSupervisor.fncSupervisorActualizar(Session("@EMPRESA"), Session("@USUARIO"), ldtbSupervisor)
                If Not ldtbSupervisor Is Nothing Then
                    If Mid(ldtbSupervisor.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                        prc_ObtenerSupervisor()
                        lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                    Else
                        lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
        ldtbSupervisor = Nothing
        lobjSupervisor = Nothing

    End Sub

    ' eliminamos la supervisor
    Private Sub prc_EliminarSupervisor(strCodCencos As String, strCodEmpelado As String)
        Dim lobjSupervisor As New clsSupervisor
        Dim ldtbSupervisor As DataTable
        ldtbSupervisor = Nothing
        lblError.Text = ""

        Try
            lobjSupervisor.CodigoGerencia = Trim(hdnCodigoGerencia.Value)
            lobjSupervisor.CodigoJefatura = Trim(hdnCodigoJefatura.Value)
            lobjSupervisor.CodigoSupervisor = Trim(hdnCodigoSupervisor.Value)
            lobjSupervisor.CenCosSup = strCodCencos
            lobjSupervisor.CodigoEmpleadoSup = strCodEmpelado
            lobjSupervisor.EstadoSup = "ANU"

            lobjSupervisor.fncSupervisorEstado(Session("@USUARIO"), ldtbSupervisor)
            If Not ldtbSupervisor Is Nothing Then
                If Mid(ldtbSupervisor.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                    prc_ObtenerSupervisor()
                    lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                Else
                    lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
        ldtbSupervisor = Nothing
        lobjSupervisor = Nothing
    End Sub

    ' nuevo supervisor
    Private Sub prc_NuevoSupervisor()
        Dim lobjSupervisor As New clsSupervisor
        Dim ldtbSupervisor As New DataTable
        ldtbSupervisor = Nothing
        lblError.Text = ""

        Try
            If ValidaDatos() = True Then
                lobjSupervisor.CenCosGer = Trim(txtCodGer.Text)
                lobjSupervisor.CenCosJef = Trim(txtCodJef.Text)
                lobjSupervisor.CenCosSup = Trim(txtCodCenCos.Text)
                lobjSupervisor.CodigoEmpleadoSup = Trim(txtCodEmp.Text)
                lobjSupervisor.fncSupervisorInsertar(Session("@EMPRESA"), Session("@USUARIO"), ldtbSupervisor)
                If Not ldtbSupervisor Is Nothing Then
                    If Mid(ldtbSupervisor.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                        prc_ObtenerSupervisor()
                        lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                    Else
                        lblError.Text = ldtbSupervisor.Rows(0).Item(0)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
        ldtbSupervisor = Nothing
        lobjSupervisor = Nothing
    End Sub

    ' validacion de datos
    Public Function ValidaDatos() As Boolean
        Dim blnValida As Boolean = True
        Try
            ' validamos al consultar
            If hdnAccion.Value = "4" Then
                If Trim(txtCodGer.Text).Length = 0 Then
                    lblError.Text = "Error, debe elegir una gerencia para el supervisor."
                    blnValida = False
                    Return blnValida
                    Exit Function
                End If
                If Trim(txtCodJef.Text).Length = 0 Then
                    lblError.Text = "Error, debe elegir una jefatura para el supervisor."
                    blnValida = False
                    Return blnValida
                    Exit Function
                End If
            End If
            ' validamos al guardar o editar
            If hdnAccion.Value = "1" Or hdnAccion.Value = "2" Then
                If Trim(txtCodGer.Text).Length = 0 Then
                    lblError.Text = "Error, debe elegir una gerencia para el supervisor."
                    blnValida = False
                    Return blnValida
                    Exit Function
                End If
                If Trim(txtCodJef.Text).Length = 0 Then
                    lblError.Text = "Error, debe elegir una jefatura para el supervisor."
                    blnValida = False
                    Return blnValida
                    Exit Function
                End If
                If Trim(txtCodCenCos.Text).Length = 0 Then
                    lblError.Text = "Error, debe elegir un centro de costos para la supervision."
                    blnValida = False
                    Return blnValida
                    Exit Function
                End If
                If Trim(txtCodEmp.Text).Length = 0 Then
                    lblError.Text = "Error, debe elegir un personal como supervisor."
                    blnValida = False
                    Return blnValida
                    Exit Function
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error, validando los datos para el supervisor"
            blnValida = False
        End Try
        Return blnValida
    End Function

    ' boton consultar
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        prc_ObtenerSupervisor()
    End Sub

    ' boton grabar
    Protected Sub btnGrabar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGrabar.Click
        Select Case hdnAccion.Value
            Case "1"
                prc_NuevoSupervisor()
                txtCodCenCos.Focus()
            Case "2"
                prc_ActualizaSupervisor()
                txtCodEmp.Focus()
        End Select
        txtCodCenCos.Text = ""
        txtCodEmp.Text = ""
        pnlEditar.Visible = False
    End Sub

    ' boton nuevo
    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        txtCodCenCos.Enabled = True
        btnCentroCosto.Visible = True
        txtCodCenCos.Focus()
        pnlEditar.Visible = True
        hdnAccion.Value = "1"
    End Sub

    ' boton cerrar
    Protected Sub btnCerrar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerrar.Click
        txtCodCenCos.Text = ""
        txtDesCenCos.Text = ""
        txtCodEmp.Text = ""
        txtDesEmp.Text = ""
        pnlEditar.Visible = False
    End Sub

    ' grilla
    Private Sub dgSupervisores_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSupervisores.ItemCommand
        Dim strCodCencos As String = ""
        Dim strCodEmpelado As String = ""
        Try
            Select Case e.CommandName
                Case "Editar"
                    hdnCodigoGerencia.Value = CType(e.Item.FindControl("lblCodGerencia"), Label).Text
                    hdnCodigoJefatura.Value = CType(e.Item.FindControl("lblCodJefatura"), Label).Text
                    hdnCodigoSupervisor.Value = CType(e.Item.FindControl("lblCodSupervisor"), Label).Text

                    txtCodCenCos.Text = CType(e.Item.FindControl("lblCodCenCos"), Label).Text
                    txtDesCenCos.Text = CType(e.Item.FindControl("lblDesCenCos"), Label).Text
                    txtCodCenCos.Enabled = False

                    txtCodEmp.Text = CType(e.Item.FindControl("lblCodEmp"), Label).Text
                    txtDesEmp.Text = CType(e.Item.FindControl("lblDesEmp"), Label).Text
                    btnCentroCosto.Visible = False

                    txtCodEmp.Focus()
                    pnlEditar.Visible = True
                    hdnAccion.Value = "2"

                Case "Eliminar"
                    hdnCodigoGerencia.Value = CType(e.Item.FindControl("lblCodGerencia"), Label).Text
                    hdnCodigoJefatura.Value = CType(e.Item.FindControl("lblCodJefatura"), Label).Text
                    hdnCodigoSupervisor.Value = CType(e.Item.FindControl("lblCodSupervisor"), Label).Text
                    strCodCencos = CType(e.Item.FindControl("lblCodCenCos"), Label).Text
                    strCodEmpelado = CType(e.Item.FindControl("lblCodEmp"), Label).Text
                    prc_EliminarSupervisor(strCodCencos, strCodEmpelado)
                    hdnAccion.Value = "3"
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class