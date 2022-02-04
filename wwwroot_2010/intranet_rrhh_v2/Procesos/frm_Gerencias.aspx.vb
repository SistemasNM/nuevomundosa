Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_Gerencias
  Inherits System.Web.UI.Page

    Private Sub frm_Gerencias_Init(sender As Object, e As System.EventArgs) Handles Me.Init
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
        txtCodEmp.Attributes.Add("onkeypress", "handleKeyPress(event)")
    End Sub

#Region "Metodos-Funciones"

    ' Obtener gerencias
    Private Sub prc_ObtenerGerencias()
        Dim lobjGerencias As New clsGerencia
        Dim ldtbGerencias As DataTable
        ldtbGerencias = Nothing
        lblError.Text = ""
        Try
            lobjGerencias.CodigoGerencia = ""
            lobjGerencias.CenCosGer = ""

            ldtbGerencias = lobjGerencias.fncGerenciasListar(Session("@EMPRESA"), ldtbGerencias)
            If Not ldtbGerencias Is Nothing Then
                If ldtbGerencias.Rows.Count > 0 Then
                    dgGerencias.DataSource = ldtbGerencias
                    dgGerencias.DataBind()
                    dgGerencias.Visible = True
                    lblItems.Text = "Numero de Items: " + ldtbGerencias.Rows.Count.ToString
                Else
                    dgGerencias.DataSource = Nothing
                    dgGerencias.DataBind()
                    lblItems.Text = ""
                End If
            Else
                lblError.Text = "No existen gerencias a consultar"
            End If
        Catch ex As Exception
            lblError.Text = "Error al consultar gerencias. " + ex.Message.ToString
        End Try

        ldtbGerencias = Nothing
        lobjGerencias = Nothing

    End Sub

    ' Guardar gerencias
    Private Sub ActualizaGerencia()
        Dim lobjGerencias As New clsGerencia
        Dim ldtbGerencias As DataTable
        ldtbGerencias = Nothing
        lblError.Text = ""

        Try
            If fncValida() Then
                lobjGerencias.CodigoGerencia = Trim(hdnCodigoGerencia.Value)
                lobjGerencias.CenCosGer = Trim(txtCodCenCos.Text)
                lobjGerencias.CodigoEmpleadoGer = Trim(txtCodEmp.Text)
                lobjGerencias.fncGerenciasActualizar(Session("@EMPRESA"), Session("@USUARIO"), ldtbGerencias)
                If Not ldtbGerencias Is Nothing Then
                    If Mid(ldtbGerencias.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                        prc_ObtenerGerencias()
                    End If
                    lblError.Text = ldtbGerencias.Rows(0).Item(0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al actualizar gerencia. " + ex.Message.ToString
        End Try
        ldtbGerencias = Nothing
        lobjGerencias = Nothing
    End Sub

    ' Insertar gerencias
    Private Sub InsertarGerencia()
        Dim lobjGerencias As New clsGerencia
        Dim ldtbGerencias As DataTable
        ldtbGerencias = Nothing
        lblError.Text = ""

        Try
            If fncValida() Then
                lobjGerencias.CenCosGer = Trim(txtCodCenCos.Text)
                lobjGerencias.CodigoEmpleadoGer = Trim(txtCodEmp.Text)
                lobjGerencias.fncGerenciasInsertar(Session("@EMPRESA"), Session("@USUARIO"), ldtbGerencias)
                If Not ldtbGerencias Is Nothing Then
                    If Mid(ldtbGerencias.Rows(0).Item(0).ToString, 1, 5) <> "Error" Then
                        prc_ObtenerGerencias()
                    End If
                    lblError.Text = ldtbGerencias.Rows(0).Item(0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error al registrar la gerencia. " + ex.Message.ToString
        End Try
        ldtbGerencias = Nothing
        lobjGerencias = Nothing
    End Sub

    ' Validar datos
    Private Function fncValida() As Boolean
        Dim lblnValida As Boolean = True
        lblError.Text = ""

        If Trim(txtCodCenCos.Text).Length = 0 Then
            lblError.Text = "Error, debe ingresar un centro de costos para la gerencia."
            lblnValida = False
            Return lblnValida
            Exit Function
        End If

        If Trim(txtCodEmp.Text).Length = 0 Then
            lblError.Text = "Error, debe ingresar un responsable para la gerencia."
            lblnValida = False
            Return lblnValida
            Exit Function
        End If
        Return lblnValida
    End Function

#End Region


  ' grilla
    Private Sub dgGerencias_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgGerencias.ItemCommand
        Try
            Select Case e.CommandName
                Case "Editar"
                    hdnCodigoGerencia.Value = CType(e.Item.FindControl("lblCodGerencia"), Label).Text
                    txtCodCenCos.Text = CType(e.Item.FindControl("lblCodCenCos"), Label).Text
                    txtNomCenCos.Text = CType(e.Item.FindControl("lblDesCenCos"), Label).Text
                    txtCodCenCos.Enabled = False
                    btnCentroCosto.Visible = False

                    txtCodEmp.Text = CType(e.Item.FindControl("lblCodEmp"), Label).Text
                    txtNomEmp.Text = CType(e.Item.FindControl("lblDesEmp"), Label).Text
                    txtCodEmp.Focus()
                    pnlEditar.Visible = True
                    hdnAccion.Value = "2"
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

  ' boton grabar
    Protected Sub btnGrabar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnGrabar.Click
        Select Case hdnAccion.Value
            Case "1"
                InsertarGerencia()
            Case "2"
                ActualizaGerencia()
        End Select

        txtCodCenCos.Text = ""
        txtNomCenCos.Text = ""
        txtCodEmp.Text = ""
        txtNomEmp.Text = ""
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

  ' boton consultar
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Call prc_ObtenerGerencias()
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerrar.Click
        txtCodCenCos.Text = ""
        txtNomCenCos.Text = ""
        txtCodEmp.Text = ""
        txtNomEmp.Text = ""
        pnlEditar.Visible = False
    End Sub
End Class