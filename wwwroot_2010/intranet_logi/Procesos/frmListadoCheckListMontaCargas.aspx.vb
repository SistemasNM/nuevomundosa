Imports NuevoMundo

Public Class frmListadoCheckListMontaCargas
    Inherits System.Web.UI.Page

#Region "Funciones"

    Private Sub filtrarListadoCheckList()

        Dim objPedido As New Logistica.clsPedidos
        Dim ldtResponse As DataTable

        If txtFechaIni.Text.Trim.Equals("") And Not txtFechaFin.Text.Trim.Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Debe ingresar la fecha inicio');</script>")
            Exit Sub
        End If

        If Not txtFechaIni.Text.Trim.Equals("") And txtFechaFin.Text.Trim.Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Debe ingresar la fecha fin.');</script>")
            Exit Sub
        End If

        Try
            Convert.ToDateTime(txtFechaIni.Text.Trim)
            Convert.ToDateTime(txtFechaFin.Text.Trim)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Debe ingresar fechas validas.');</script>")
            Exit Sub
        End Try


        Dim lvchAccion As String = "F"
        Dim lvchCodEmpl As String = txtSolicitante.Text.Trim + "%"
        Dim lIntTurno As Integer = 0
        Dim lvchFechaChk As String = Convert.ToDateTime(txtFechaIni.Text.Trim).ToString("yyyyMMdd")
        Dim lvchNumMaquina As String = IIf(ddlNumMaquina.SelectedValue.Equals("0"), "%", ddlEstado.SelectedValue + "%")
        Dim lvchHorometroE As String = Convert.ToDateTime(txtFechaFin.Text.Trim).ToString("yyyyMMdd")
        Dim lvchHorometroS As String = IIf(ddlEstado.SelectedValue.Equals("0"), "%", ddlEstado.SelectedValue + "%")
        Dim lvchMcaFlag1 As String = ""
        Dim lvchCantAnad1 As String = ""
        Dim lvchMcaFlag2 As String = ""
        Dim lvchCantAnad2 As String = ""
        Dim lvchMcaFlag3 As String = ""
        Dim lvchCantAnad3 As String = ""
        Dim lvchMcaFlag4 As String = ""
        Dim lvchCantAnad4 As String = ""
        Dim lvchMcaFlag5 As String = ""
        Dim lvchCantAnad5 As String = ""
        Dim lvchMcaFlag6 As String = ""
        Dim lvchMcaFlag7 As String = ""
        Dim lvchMcaFlag8 As String = ""
        Dim lvchMcaFlag9 As String = ""
        Dim lvchMcaFlag10 As String = ""
        Dim lvchObservaciones As String = ""
        Dim lvchUsuario As String = ""

        ldtResponse = objPedido.fncCheckListMontacargaCRUD(lvchAccion, lvchCodEmpl, lIntTurno, lvchFechaChk, lvchNumMaquina, lvchHorometroE, lvchHorometroS, lvchMcaFlag1, lvchCantAnad1,
                                                           lvchMcaFlag2, lvchCantAnad2, lvchMcaFlag3, lvchCantAnad3, lvchMcaFlag4, lvchCantAnad4, lvchMcaFlag5, lvchCantAnad5,
                                                           lvchMcaFlag6, lvchMcaFlag7, lvchMcaFlag8, lvchMcaFlag9, lvchMcaFlag10, lvchObservaciones, lvchUsuario)

        dgListaCheckList.DataSource = ldtResponse
        dgListaCheckList.DataBind()

    End Sub

    Private Sub cargarListadoCheckList()

        Dim objPedido As New Logistica.clsPedidos
        Dim ldtResponse As DataTable

        Dim lvchAccion As String = "L"
        Dim lvchCodEmpl As String = ""
        Dim lIntTurno As Integer = 0
        Dim lvchFechaChk As String = ""
        Dim lvchNumMaquina As String = ""
        Dim lvchHorometroE As String = ""
        Dim lvchHorometroS As String = ""
        Dim lvchMcaFlag1 As String = ""
        Dim lvchCantAnad1 As String = ""
        Dim lvchMcaFlag2 As String = ""
        Dim lvchCantAnad2 As String = ""
        Dim lvchMcaFlag3 As String = ""
        Dim lvchCantAnad3 As String = ""
        Dim lvchMcaFlag4 As String = ""
        Dim lvchCantAnad4 As String = ""
        Dim lvchMcaFlag5 As String = ""
        Dim lvchCantAnad5 As String = ""
        Dim lvchMcaFlag6 As String = ""
        Dim lvchMcaFlag7 As String = ""
        Dim lvchMcaFlag8 As String = ""
        Dim lvchMcaFlag9 As String = ""
        Dim lvchMcaFlag10 As String = ""
        Dim lvchObservaciones As String = ""
        Dim lvchUsuario As String = ""

        ldtResponse = objPedido.fncCheckListMontacargaCRUD(lvchAccion, lvchCodEmpl, lIntTurno, lvchFechaChk, lvchNumMaquina, lvchHorometroE, lvchHorometroS, lvchMcaFlag1, lvchCantAnad1,
                                                           lvchMcaFlag2, lvchCantAnad2, lvchMcaFlag3, lvchCantAnad3, lvchMcaFlag4, lvchCantAnad4, lvchMcaFlag5, lvchCantAnad5,
                                                           lvchMcaFlag6, lvchMcaFlag7, lvchMcaFlag8, lvchMcaFlag9, lvchMcaFlag10, lvchObservaciones, lvchUsuario)

        dgListaCheckList.DataSource = ldtResponse
        dgListaCheckList.DataBind()

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Session("@USUARIO") = "AAMPUERP"

            '--INICIO: VERIFICAR LA SESION
            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                    Dim objRequest As New BLITZ_LOCK.clsRequest
                    Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
                End If

                If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                    Response.Redirect("../../intranet/finsesion.htm")
                End If
            End If

            cargarListadoCheckList()

        End If

    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

        Response.Redirect("frm_CheckListMontacargas.aspx?Accion=INSERT")

    End Sub

    Protected Sub dgListaCheckList_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListaCheckList.ItemCommand
        Select Case e.CommandName
            Case "Editar"

                Response.Redirect("frm_CheckListMontacargas.aspx?Accion=UPDATE&idChk=" + e.CommandArgument)

        End Select
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        filtrarListadoCheckList()
    End Sub
End Class