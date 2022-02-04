Imports NM_General
Public Class frmActualizarSalida
    Inherits System.Web.UI.Page
    Dim strSalida As String
    Dim strUsuario As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strSalida = Request.QueryString("Salida")
        strUsuario = Request.QueryString("Usuario")
        btnRuta.Attributes.Add("onClick", "btnCerrar_Onclick()")
        btnPunto.Attributes.Add("onClick", "btnCerrar_Onclick()")
        btnAtender.Attributes.Add("onClick", "btnCerrar_Onclick()")
        'btnAceptar.Attributes.Add("onClick", "btnCerrar_Onclick()")
        btnAceptar.Attributes.Add("onClick", "javascript:return fnc_Validar();")
        txtSalida.Text = strSalida
        imgRuta.Visible = False
        imgPunto.Visible = False
        imgAtendido.Visible = False
        imgFinal.Visible = False
        imgProgram.Visible = False
        msgerror.Text = ""
        msgerror.Visible = False
        CargarEstados()
    End Sub
    Private Sub CargarEstados()
        Dim objLogistica As New NM_Logistica
        Dim dt As DataTable
        dt = objLogistica.ObtenerSalidasEstadosPorSalida(strSalida)
        If dt.Rows(0).Item("Estado").ToString = "PROGRAMADO" Then
            HabilitarBotonImagen("1", "", "", "", "")
            lblFechaPro.Text = dt.Rows(0).Item("FECHA_PROGRAMADO").ToString
            Piso.Visible = False
        ElseIf dt.Rows(0).Item("ESTADO").ToString = "EN RUTA" Then
            HabilitarBotonImagen("1", "1", "", "", "")
            lblFechaPro.Text = dt.Rows(0).Item("FECHA_PROGRAMADO").ToString
            lblFechaRut.Text = dt.Rows(0).Item("FECHA_RUTA").ToString
            Piso.Visible = False
        ElseIf dt.Rows(0).Item("ESTADO").ToString = "EN PUNTO" Then
            HabilitarBotonImagen("1", "1", "1", "", "")
            lblFechaPro.Text = dt.Rows(0).Item("FECHA_PROGRAMADO").ToString
            lblFechaRut.Text = dt.Rows(0).Item("FECHA_RUTA").ToString
            lblFechaPun.Text = dt.Rows(0).Item("FECHA_PUNTO").ToString
            Piso.Visible = False
        ElseIf dt.Rows(0).Item("ESTADO").ToString = "ATENDIDO" Then
            HabilitarBotonImagen("1", "1", "1", "1", "")
            lblFechaPro.Text = dt.Rows(0).Item("FECHA_PROGRAMADO").ToString
            lblFechaRut.Text = dt.Rows(0).Item("FECHA_RUTA").ToString
            lblFechaPun.Text = dt.Rows(0).Item("FECHA_PUNTO").ToString
            lblFechaAten.Text = dt.Rows(0).Item("FECHA_ATENDIDO").ToString
            Piso.Visible = False
        ElseIf dt.Rows(0).Item("ESTADO").ToString = "FINALIZADO" Then
            HabilitarBotonImagen("1", "1", "1", "1", "1")
            lblFechaPro.Text = dt.Rows(0).Item("FECHA_PROGRAMADO").ToString
            lblFechaRut.Text = dt.Rows(0).Item("FECHA_RUTA").ToString
            lblFechaPun.Text = dt.Rows(0).Item("FECHA_PUNTO").ToString
            lblFechaAten.Text = dt.Rows(0).Item("FECHA_ATENDIDO").ToString
            lblFechaFin.Text = dt.Rows(0).Item("FECHA_FINALIZADO").ToString
            Piso.Visible = False
        End If
    End Sub
    Private Sub HabilitarBotonImagen(programa, ruta, punto, atender, final)
        If programa = "1" And ruta = "" And punto = "" And atender = "" And final = "" Then
            imgProgram.Visible = True
            imgRuta.Visible = False
            imgPunto.Visible = False
            imgAtendido.Visible = False
            imgFinal.Visible = False

            pnActPro.Visible = False
            pnFechaPro.Visible = True

            pnActRuta.Visible = False
            pnFechaRut.Visible = False

            pnActPun.Visible = False
            pnFechaPun.Visible = False

            pnActAten.Visible = False
            pnFechaAten.Visible = False

            pnActFin.Visible = False
            pnFechaFin.Visible = False

            btnPrograma.Enabled = False
            btnRuta.Enabled = False
            btnPunto.Enabled = False
            btnAtender.Enabled = False
            btnFinal.Enabled = False
        ElseIf programa = "1" And ruta = "1" And punto = "" And atender = "" And final = "" Then
            imgProgram.Visible = True
            imgRuta.Visible = True
            imgPunto.Visible = False
            imgAtendido.Visible = False
            imgFinal.Visible = False

            pnActPro.Visible = False
            pnFechaPro.Visible = True

            pnActRuta.Visible = False
            pnFechaRut.Visible = True

            pnActPun.Visible = True
            pnFechaPun.Visible = False

            pnActAten.Visible = False
            pnFechaAten.Visible = False

            pnActFin.Visible = False
            pnFechaFin.Visible = False

            btnPrograma.Enabled = False
            btnRuta.Enabled = False
            btnPunto.Enabled = True
            btnAtender.Enabled = False
            btnFinal.Enabled = False
        ElseIf programa = "1" And ruta = "1" And punto = "1" And atender = "" And final = "" Then
            imgProgram.Visible = True
            imgRuta.Visible = True
            imgPunto.Visible = True
            imgAtendido.Visible = False
            imgFinal.Visible = False

            pnActPro.Visible = False
            pnFechaPro.Visible = True

            pnActRuta.Visible = False
            pnFechaRut.Visible = True

            pnActPun.Visible = False
            pnFechaPun.Visible = True

            pnActAten.Visible = True
            pnFechaAten.Visible = False

            pnActFin.Visible = False
            pnFechaFin.Visible = False

            btnPrograma.Enabled = False
            btnRuta.Enabled = False
            btnPunto.Enabled = False
            btnAtender.Enabled = True
            btnFinal.Enabled = False
        ElseIf programa = "1" And ruta = "1" And punto = "1" And atender = "1" And final = "" Then
            imgProgram.Visible = True
            imgRuta.Visible = True
            imgPunto.Visible = True
            imgAtendido.Visible = True
            imgFinal.Visible = False

            pnActPro.Visible = False
            pnFechaPro.Visible = True

            pnActRuta.Visible = False
            pnFechaRut.Visible = True

            pnActPun.Visible = False
            pnFechaPun.Visible = True

            pnActAten.Visible = False
            pnFechaAten.Visible = True

            pnActFin.Visible = True
            pnFechaFin.Visible = False

            btnPrograma.Enabled = False
            btnRuta.Enabled = False
            btnPunto.Enabled = False
            btnAtender.Enabled = False
            btnFinal.Enabled = True
        ElseIf programa = "1" And ruta = "1" And punto = "1" And atender = "1" And final = "1" Then
            imgProgram.Visible = True
            imgRuta.Visible = True
            imgPunto.Visible = True
            imgAtendido.Visible = True
            imgFinal.Visible = True

            pnActPro.Visible = False
            pnFechaPro.Visible = True

            pnActRuta.Visible = False
            pnFechaRut.Visible = True

            pnActPun.Visible = False
            pnFechaPun.Visible = True

            pnActAten.Visible = False
            pnFechaAten.Visible = True

            pnActFin.Visible = False
            pnFechaFin.Visible = True

            btnPrograma.Enabled = False
            btnRuta.Enabled = False
            btnPunto.Enabled = False
            btnAtender.Enabled = False
            btnFinal.Enabled = False
        End If
    End Sub

    Private Sub btnPunto_Click(sender As Object, e As System.EventArgs) Handles btnPunto.Click
        Dim objLogistica As New NM_Logistica
        objLogistica.ActualizarEstadiosSalida(txtSalida.Text.Trim, "P", strUsuario, 0)
    End Sub
    Private Sub btnAtender_Click(sender As Object, e As System.EventArgs) Handles btnAtender.Click
        Dim objLogistica As New NM_Logistica
        objLogistica.ActualizarEstadiosSalida(txtSalida.Text.Trim, "A", strUsuario, 0)
    End Sub
    Private Sub btnFinal_Click(sender As Object, e As System.EventArgs) Handles btnFinal.Click
        Piso.Visible = True
    End Sub
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        If dwlpiso.SelectedValue = "" Then
            msgerror.Text = "Debe elegir en que piso finalizo el despacho"
            msgerror.Visible = True
            Exit Sub
        Else
            msgerror.Text = ""
            msgerror.Visible = False
        End If

        Piso.Visible = False

        Dim objLogistica As New NM_Logistica
        objLogistica.ActualizarEstadiosSalida(txtSalida.Text.Trim, "F", strUsuario, dwlpiso.SelectedValue)

    End Sub
End Class