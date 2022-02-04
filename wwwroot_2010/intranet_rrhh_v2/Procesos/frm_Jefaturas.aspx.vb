Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_Jefaturas
    Inherits System.Web.UI.Page

    ' Init
    Private Sub frm_Jefaturas_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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

    ' load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    ' Obtener jefaturas
    Private Sub prc_ObtenerJefaturas()
        Dim lobjJefatura As New clsJefatura
        Dim ldtbJefatura As DataTable
        ldtbJefatura = Nothing
        lblError.Text = ""
        hndAccion.Value = "3"
        Try
            If fncValida() = True Then
                lobjJefatura.CodigoGerencia = ""
                lobjJefatura.CenCosGer = Trim(txtCodGer.Text)
                lobjJefatura.CodigoJef = ""
                lobjJefatura.CenCosJef = ""

                ldtbJefatura = lobjJefatura.fncJefaturaListar(Session("@EMPRESA"), ldtbJefatura)
                If Not ldtbJefatura Is Nothing Then
                    If ldtbJefatura.Rows.Count > 0 Then
                        dgJefaturas.DataSource = ldtbJefatura
                        dgJefaturas.DataBind()
                        dgJefaturas.Visible = True
                        lblItems.Text = "Numero de Items: " + ldtbJefatura.Rows.Count.ToString
                    Else
                        dgJefaturas.DataSource = Nothing
                        dgJefaturas.DataBind()
                        lblItems.Text = ""
                    End If
                Else
                    lblError.Text = "No existen jefaturas a consultar"
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al consultar jefaturas. " + ex.Message.ToString
        End Try

        ldtbJefatura = Nothing
        lobjJefatura = Nothing

    End Sub

    ' Insertar jefaturas
    Private Sub prc_InsertarJefaturas()
        Dim lobjJefatura As New clsJefatura
        Dim ldtbJefatura As DataTable
        ldtbJefatura = Nothing
        lblError.Text = ""

        Try
            If fncValida() = True Then
                lobjJefatura.CodigoGerencia = hdnCodigoGerencia.Value
                lobjJefatura.CenCosGer = Trim(txtCodGer.Text)
                lobjJefatura.CenCosJef = Trim(txtCodCenCos.Text)
                lobjJefatura.CodigoEmpleadoJef = Trim(txtCodEmp.Text)

                ldtbJefatura = lobjJefatura.fncJefaturaInsertar(Session("@EMPRESA"), Session("@USUARIO"), ldtbJefatura)
                If Not ldtbJefatura Is Nothing Then
                    If Not ldtbJefatura Is Nothing Then
                        If Mid(ldtbJefatura.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                            prc_ObtenerJefaturas()
                            lblError.Text = ldtbJefatura.Rows(0).Item(0)
                        Else
                            lblError.Text = ldtbJefatura.Rows(0).Item(0)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al insertar jefatura. " + ex.Message.ToString
        End Try

        ldtbJefatura = Nothing
        lobjJefatura = Nothing

    End Sub

    ' Actualizar jefaturas
    Private Sub prc_ActualizaJefatura()
        Dim lobjJefatura As New clsJefatura
        Dim ldtbJefatura As DataTable
        ldtbJefatura = Nothing
        lblError.Text = ""
        Try
            If fncValida() = True Then
                lobjJefatura.CodigoGerencia = hdnCodigoGerencia.Value
                lobjJefatura.CenCosGer = Trim(txtCodGer.Text)
                lobjJefatura.CodigoJef = hdnCodigoJefatura.Value
                lobjJefatura.CenCosJef = Trim(txtCodCenCos.Text)
                lobjJefatura.CodigoEmpleadoJef = Trim(txtCodEmp.Text)

                ldtbJefatura = lobjJefatura.fncJefaturaActualizar(Session("@EMPRESA"), Session("@USUARIO"), ldtbJefatura)
                If Not ldtbJefatura Is Nothing Then
                    If Mid(ldtbJefatura.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                        prc_ObtenerJefaturas()
                        lblError.Text = ldtbJefatura.Rows(0).Item(0)
                    Else
                        lblError.Text = ldtbJefatura.Rows(0).Item(0)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al actualizar jefatura. " + ex.Message.ToString
        End Try

        ldtbJefatura = Nothing
        lobjJefatura = Nothing

    End Sub

    ' Validar datos
    Private Function fncValida() As Boolean
        Dim lblnValida As Boolean = True
        lblError.Text = ""

        If Trim(txtCodGer.Text).Length = 0 And (hndAccion.Value = "1" Or hndAccion.Value = "3") Then
            lblError.Text = "Error, debe ingresar un centro de costos para la gerencia."
            lblnValida = False
            Return lblnValida
            Exit Function
        End If

        If hndAccion.Value = "2" Then
            If Trim(txtCodEmp.Text).Length = 0 Then
                lblError.Text = "Error, debe ingresar un responsable para la jefatura."
                lblnValida = False
                Return lblnValida
                Exit Function
            End If
        End If

        Return lblnValida
    End Function

    ' grilla
    Private Sub dgJefaturas_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgJefaturas.ItemCommand
        Dim strCodCencos As String = ""
        Dim strCodEmpelado As String = ""
        Try
            Select Case e.CommandName
                Case "Editar"
                    hdnCodigoGerencia.Value = CType(e.Item.FindControl("lblCodGerencia"), Label).Text
                    hdnCodigoJefatura.Value = CType(e.Item.FindControl("lblCodJefatura"), Label).Text
                    txtCodCenCos.Text = CType(e.Item.FindControl("lblCodCenCos"), Label).Text
                    txtDesCenCos.Text = CType(e.Item.FindControl("lblDesCenCos"), Label).Text
                    txtCodCenCos.Enabled = False
                    btnCentroCosto.Visible = False

                    txtCodEmp.Text = CType(e.Item.FindControl("lblCodEmp"), Label).Text
                    txtDesEmp.Text = CType(e.Item.FindControl("lblDesEmp"), Label).Text
                    txtCodEmp.Focus()
                    pnlEditar.Visible = True
                    hndAccion.Value = "2"
                Case "Eliminar"
                    hdnCodigoGerencia.Value = CType(e.Item.FindControl("lblCodGerencia"), Label).Text
                    hdnCodigoJefatura.Value = CType(e.Item.FindControl("lblCodJefatura"), Label).Text
                    strCodCencos = CType(e.Item.FindControl("lblCodCenCos"), Label).Text
                    strCodEmpelado = CType(e.Item.FindControl("lblCodEmp"), Label).Text
                    prc_EliminarJefatura(strCodCencos, strCodEmpelado)
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    ' eliminamos la supervisor
    Private Sub prc_EliminarJefatura(strCodCencos As String, strCodEmpelado As String)
        Dim lobjJefatura As New clsJefatura
        Dim ldtbJefatura As DataTable
        ldtbJefatura = Nothing
        lblError.Text = ""
        Try
            lobjJefatura.CodigoGerencia = Trim(hdnCodigoGerencia.Value)
            lobjJefatura.CodigoJef = Trim(hdnCodigoJefatura.Value)
            lobjJefatura.CenCosJef = strCodCencos
            lobjJefatura.CodigoEmpleadoJef = strCodEmpelado
            lobjJefatura.EstadoJef = "ANU"

            lobjJefatura.fncJefaturaEstado(Session("@USUARIO"), ldtbJefatura)
            If Not ldtbJefatura Is Nothing Then
                If Mid(ldtbJefatura.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                    prc_ObtenerJefaturas()
                    lblError.Text = ldtbJefatura.Rows(0).Item(0)
                Else
                    lblError.Text = ldtbJefatura.Rows(0).Item(0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al cambiar estado. " + ex.Message.ToString
        End Try
        ldtbJefatura = Nothing
        lobjJefatura = Nothing
    End Sub

    ' boton consultar
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        prc_ObtenerJefaturas()
    End Sub

    ' boton nuevo
    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        txtCodCenCos.Enabled = True
        btnCentroCosto.Visible = True
        txtCodCenCos.Focus()
        pnlEditar.Visible = True
        hndAccion.Value = "1"
    End Sub

    ' boton grabar
    Protected Sub btnGrabar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGrabar.Click
        Select Case hndAccion.Value
            Case "1"
                prc_InsertarJefaturas()
            Case "2"
                prc_ActualizaJefatura()
        End Select

        txtCodCenCos.Text = ""
        txtDesCenCos.Text = ""
        txtCodEmp.Text = ""
        txtDesEmp.Text = ""
        pnlEditar.Visible = False
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerrar.Click
        txtCodCenCos.Text = ""
        txtDesCenCos.Text = ""
        txtCodEmp.Text = ""
        txtDesEmp.Text = ""
        pnlEditar.Visible = False
    End Sub
End Class