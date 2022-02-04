Imports NuevoMundo

Public Class frm_CheckListMontacargas
    Inherits System.Web.UI.Page

#Region "Funciones"

    Private Sub registrarCheckListMontaCargaTurno()

        Dim objPedido As New Logistica.clsPedidos
        Dim ldtResponse As DataTable

        Dim lvchAccion As String = "I"
        Dim lvchCodEmpl As String = txtSolicitante.Text.Trim
        Dim lIntTurno As Integer = IIf(ddlTurno.SelectedValue.ToString().Equals("0"), 0, Convert.ToInt32(ddlTurno.SelectedValue.ToString()))

        If txtSolicitante.Text.Trim.Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese Operador.');</script>")
            Exit Sub

        End If

        If lIntTurno.Equals(0) Then

            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione Turno.');</script>")
            Exit Sub

        End If

        If ddlNumMaquina.SelectedValue.Equals("0") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione Número Máquina.');</script>")
            Exit Sub

        End If

        If txtHorometroE.Text.Trim.Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese valor de horometro.');</script>")
            Exit Sub

        End If

        Dim lvchFechaChk As String = Convert.ToDateTime(txtFechaIni.Text.Trim).ToString("yyyyMMdd")
        Dim lvchNumMaquina As String = ddlNumMaquina.SelectedValue.ToString()
        Dim lvchHorometroE As String = txtHorometroE.Text.Trim
        Dim lvchHorometroS As String = txtHorometroS.Text.Trim
        Dim lvchMcaFlag1 As String = ddlItem1.SelectedValue.ToString()
        Dim lvchCantAnad1 As String = ddlCantAnad1.SelectedValue.ToString()
        Dim lvchMcaFlag2 As String = ddlItem2.SelectedValue.ToString()
        Dim lvchCantAnad2 As String = ddlCantAnad2.SelectedValue.ToString()
        Dim lvchMcaFlag3 As String = ddlItem3.SelectedValue.ToString()
        Dim lvchCantAnad3 As String = ddlCantAnad3.SelectedValue.ToString()
        Dim lvchMcaFlag4 As String = ddlItem4.SelectedValue.ToString()
        Dim lvchCantAnad4 As String = ddlCanAnad4.SelectedValue.ToString()
        Dim lvchMcaFlag5 As String = ddlItem5.SelectedValue.ToString()
        Dim lvchCantAnad5 As String = ddlCanAnad5.SelectedValue.ToString()
        Dim lvchMcaFlag6 As String = ddlItem6.SelectedValue.ToString()
        Dim lvchMcaFlag7 As String = ddlItem7.SelectedValue.ToString()
        Dim lvchMcaFlag8 As String = ddlItem8.SelectedValue.ToString()
        Dim lvchMcaFlag9 As String = ddlItem9.SelectedValue.ToString()
        Dim lvchMcaFlag10 As String = ddlItem10.SelectedValue.ToString()
        Dim lvchObservaciones As String = txtObservacion_1.Text.Trim + "|" + txtObservacion_2.Text.Trim + "|" + txtObservacion_3.Text.Trim
        Dim lvchUsuario As String = Session("@USUARIO")

        ldtResponse = objPedido.fncCheckListMontacargaCRUD(lvchAccion, lvchCodEmpl, lIntTurno, lvchFechaChk, lvchNumMaquina, lvchHorometroE, lvchHorometroS, lvchMcaFlag1, lvchCantAnad1,
                                                           lvchMcaFlag2, lvchCantAnad2, lvchMcaFlag3, lvchCantAnad3, lvchMcaFlag4, lvchCantAnad4, lvchMcaFlag5, lvchCantAnad5,
                                                           lvchMcaFlag6, lvchMcaFlag7, lvchMcaFlag8, lvchMcaFlag9, lvchMcaFlag10, lvchObservaciones, lvchUsuario)

        If ldtResponse.Rows(0).Item("CodigoRespuesta").Equals("100") Then

            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('" + ldtResponse.Rows(0).Item("MensajeRespuesta") + "');</script>")
            Exit Sub

        End If

        If ldtResponse.Rows(0).Item("CodigoRespuesta").Equals("200") Then

            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Se grabó correctamente.');</script>")

        End If

        objPedido = Nothing
        ldtResponse = Nothing

    End Sub

    Private Sub cargarCheckList(ByVal strIdChk As String)

        Dim objPedido As New Logistica.clsPedidos
        Dim ldtResponse As DataTable

        Dim lvchAccion As String = "O"
        Dim lvchCodEmpl As String = ""
        Dim lIntTurno As Integer = Convert.ToInt32(strIdChk)

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

        If ldtResponse.Rows.Count > 0 Then

            txtSolicitante.Text = ldtResponse.Rows(0).Item("VCH_COD_EMPL")
            ddlTurno.SelectedValue = ldtResponse.Rows(0).Item("TURNO")
            txtFechaIni.Text = ldtResponse.Rows(0).Item("FECHA_CHK")

            ddlNumMaquina.SelectedValue = ldtResponse.Rows(0).Item("VCH_NUM_MAQUINA")
            txtHorometroE.Text = ldtResponse.Rows(0).Item("VCH_HONOMETRO_E")
            txtHorometroS.Text = ldtResponse.Rows(0).Item("VCH_HONOMETRO_S")

            ddlItem1.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG1")
            ddlCantAnad1.SelectedValue = ldtResponse.Rows(0).Item("VCH_CANT_ANAD1")
            ddlItem2.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG2")
            ddlCantAnad2.SelectedValue = ldtResponse.Rows(0).Item("VCH_CANT_ANAD2")
            ddlItem3.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG3")
            ddlCantAnad3.SelectedValue = ldtResponse.Rows(0).Item("VCH_CANT_ANAD3")
            ddlItem4.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG4")
            ddlCanAnad4.SelectedValue = ldtResponse.Rows(0).Item("VCH_CANT_ANAD4")
            ddlItem5.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG5")
            ddlCanAnad5.SelectedValue = ldtResponse.Rows(0).Item("VCH_CANT_ANAD5")
            ddlItem6.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG6")
            ddlItem7.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG7")
            ddlItem8.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG8")
            ddlItem9.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG9")
            ddlItem10.SelectedValue = ldtResponse.Rows(0).Item("VCH_MCA_FLAG10")

            txtObservacion_1.Text = ldtResponse.Rows(0).Item("VCH_OBSERVACIONES")

        End If

        objPedido = Nothing
        ldtResponse = Nothing

    End Sub

    Private Sub actualizarCheckListMontaCargaTurno()

        Dim objPedido As New Logistica.clsPedidos
        Dim ldtResponse As DataTable

        Dim lvchAccion As String = "U"
        Dim lvchCodEmpl As String = txtSolicitante.Text.Trim
        Dim lIntTurno As Integer = Convert.ToInt32(Request.QueryString("idChk"))

        If txtSolicitante.Text.Trim.Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese Operador.');</script>")
            Exit Sub

        End If

        If lIntTurno.Equals(0) Then

            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione Turno.');</script>")
            Exit Sub

        End If

        If ddlNumMaquina.SelectedValue.Equals("0") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Seleccione Número Máquina.');</script>")
            Exit Sub

        End If

        If txtHorometroE.Text.Trim.Equals("") Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Ingrese valor de horometro.');</script>")
            Exit Sub

        End If

        Dim lvchFechaChk As String = Convert.ToDateTime(txtFechaIni.Text.Trim).ToString("yyyyMMdd")
        Dim lvchNumMaquina As String = ddlNumMaquina.SelectedValue.ToString()
        Dim lvchHorometroE As String = txtHorometroE.Text.Trim
        Dim lvchHorometroS As String = txtHorometroS.Text.Trim
        Dim lvchMcaFlag1 As String = ddlItem1.SelectedValue.ToString()
        Dim lvchCantAnad1 As String = ddlCantAnad1.SelectedValue.ToString()
        Dim lvchMcaFlag2 As String = ddlItem2.SelectedValue.ToString()
        Dim lvchCantAnad2 As String = ddlCantAnad2.SelectedValue.ToString()
        Dim lvchMcaFlag3 As String = ddlItem3.SelectedValue.ToString()
        Dim lvchCantAnad3 As String = ddlCantAnad3.SelectedValue.ToString()
        Dim lvchMcaFlag4 As String = ddlItem4.SelectedValue.ToString()
        Dim lvchCantAnad4 As String = ddlCanAnad4.SelectedValue.ToString()
        Dim lvchMcaFlag5 As String = ddlItem5.SelectedValue.ToString()
        Dim lvchCantAnad5 As String = ddlCanAnad5.SelectedValue.ToString()
        Dim lvchMcaFlag6 As String = ddlItem6.SelectedValue.ToString()
        Dim lvchMcaFlag7 As String = ddlItem7.SelectedValue.ToString()
        Dim lvchMcaFlag8 As String = ddlItem8.SelectedValue.ToString()
        Dim lvchMcaFlag9 As String = ddlItem9.SelectedValue.ToString()
        Dim lvchMcaFlag10 As String = ddlItem10.SelectedValue.ToString()
        Dim lvchObservaciones As String = txtObservacion_1.Text.Trim + "|" + txtObservacion_2.Text.Trim + "|" + txtObservacion_3.Text.Trim
        Dim lvchUsuario As String = Session("@USUARIO")

        ldtResponse = objPedido.fncCheckListMontacargaCRUD(lvchAccion, lvchCodEmpl, lIntTurno, lvchFechaChk, lvchNumMaquina, lvchHorometroE, lvchHorometroS, lvchMcaFlag1, lvchCantAnad1,
                                                           lvchMcaFlag2, lvchCantAnad2, lvchMcaFlag3, lvchCantAnad3, lvchMcaFlag4, lvchCantAnad4, lvchMcaFlag5, lvchCantAnad5,
                                                           lvchMcaFlag6, lvchMcaFlag7, lvchMcaFlag8, lvchMcaFlag9, lvchMcaFlag10, lvchObservaciones, lvchUsuario)

        If ldtResponse.Rows(0).Item("CodigoRespuesta").Equals("200") Then

            ClientScript.RegisterStartupScript(Me.[GetType](), "Alerta", "<script language=javascript>alert('Se actualizo correctamente.');</script>")
            Exit Sub

        End If

        objPedido = Nothing
        ldtResponse = Nothing

    End Sub

    Private Sub bloquearControles()
        txtSolicitante.Enabled = False
        ddlTurno.Enabled = False
        txtFechaIni.Enabled = False
        ddlNumMaquina.Enabled = False
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

            If Request.QueryString("Accion").Equals("INSERT") Then
                txtFechaIni.Text = Date.Now.ToString("dd/MM/yyyy")
                txtFechaIni.Enabled = False
            End If

            If Request.QueryString("Accion").Equals("UPDATE") Then
                cargarCheckList(Request.QueryString("idChk"))
                bloquearControles()
            End If

        End If
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If Request.QueryString("Accion").Equals("INSERT") Then
            registrarCheckListMontaCargaTurno()
        End If
        If Request.QueryString("Accion").Equals("UPDATE") Then
            actualizarCheckListMontaCargaTurno()
        End If
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("frmListadoCheckListMontaCargas.aspx")
    End Sub
End Class