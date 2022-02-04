Imports NuevoMundo
Imports System
Imports System.Data
Imports System.Web
Public Class ConformidadServicio
    Inherits System.Web.UI.Page
    Protected WithEvents downResultado As System.Web.UI.WebControls.DropDownList
    Dim StrCodigo As String
    Dim strItem As String
    Dim strNumeroPedido As String = ""

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ATORRESC"

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
        Me.btnSolicitaAprobacion.Attributes.Add("onClick", "javascript: return fdesTipoAprobacion(TxtAprobacion,TxtDescripcion);")
        If Page.IsPostBack = False Then
            StrCodigo = Request.QueryString("strNumeroOrdenServicio")
            Dim actObs As Boolean = True
            Session("actObs") = actObs
            strItem = Request.QueryString("strItem")
            Session("Item") = strItem

            CargarDatosCabecera(StrCodigo)
            imgCancelar.Attributes.Add("Onclick", "javascript:fnc_Cerrar();")
            btnAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos()")
        End If

    End Sub
    Public Sub BindGridTM()
        grvEval.DataSource = CType(Session("dtDatos1"), DataTable)
        grvEval.DataBind()
    End Sub

    Public Sub CargarDatosCabecera(ByVal strNumeroOrdenServicio As String)
        lblMsg.Text = ""
        Dim lobjOrdenServicio As New clsFichaProv
        Dim objDT As New DataSet
        Dim xData1, xData2, xData3 As New DataTable
        Dim tipoServicio As String
        lobjOrdenServicio.CodigoEmpresa = Session("@EMPRESA")
        lobjOrdenServicio.NumeroOrdenServicio = strNumeroOrdenServicio
        lobjOrdenServicio.Item = Session("Item")
        lobjOrdenServicio.MostrarOrdenServicio_Detalle_Conformidad(objDT)
        xData1 = objDT.Tables(0)
        xData2 = objDT.Tables(1)
        xData3 = objDT.Tables(2)

        With xData1.Rows(0)
            lblNroOrdeServicio.Text = .Item("var_Numero").ToString
            Me.lblNombreProveedor.Text = .Item("NO_CORT_PROV").ToString
            Me.lblRuc.Text = .Item("CO_PROV").ToString
            Me.lblFecha.Text = .Item("fe_emis").ToString
            Me.lblNombreContacto.Text = .Item("Contacto").ToString
            Me.lblTelefonoContacto.Text = .Item("NU_TLFN_CONC").ToString
            Me.TxtCodigoTrabajador.Text = .Item("vch_CodigoSolicitante").ToString
            Me.TxtNombreTrabajador.Text = .Item("no_usua").ToString
            Me.lblEmail.Text = .Item("DE_MAIL").ToString
            Me.txtNroRequisicion.Text = .Item("nu_reqi").ToString
            Me.TxtObservaciones.Text = .Item("vch_Observaciones").ToString
            Me.lblMonto.Text = .Item("MONTO").ToString
            If (.Item("dtm_fechaInicio").ToString = "" or .Item("dtm_fechaInicio").ToString is Nothing) Then
                Me.wdpFechaInicio.Text = Now.ToString("dd/MM/yyyy")
            Else
                Me.wdpFechaInicio.Text = .Item("dtm_fechaInicio").ToString
            End If
            If (.Item("dtm_FechaFinal").ToString = "" Or .Item("dtm_FechaFinal").ToString Is Nothing) Then
                Me.wdpFechaFin.Text = Now.ToString("dd/MM/yyyy")
            Else
                Me.wdpFechaFin.Text = .Item("dtm_FechaFinal").ToString
            End If
            Session("FlgObser") = .Item("FlgObser").ToString
            tipoServicio = .Item("TIPO_SERVICIO")
            If (.Item("TIPO_SERVICIO") = "I") Then
                lblTipoServicio.Text = "SI"
            Else
                lblTipoServicio.Text = "NO"
            End If
            Select Case .Item("chr_estado").ToString
                Case "SOL"
                    Me.imgGrabarFicha.Visible = False
                    Me.btnSolicitaAprobacion.Visible = False
                    btnObervar.Visible = False
                    TxtObservaciones.Enabled = False
                Case "APR"
                    Me.imgGrabarFicha.Visible = False
                    Me.btnSolicitaAprobacion.Visible = False
                    btnObervar.Visible = False
                    TxtObservaciones.Enabled = False
                Case "ACT"
                    If Session("FlgObser") = "1" Then
                        Me.imgGrabarFicha.Visible = False
                        Me.btnSolicitaAprobacion.Visible = False
                        btnObervar.Visible = True
                        TxtObservaciones.Enabled = False
                        lblMsg.Text = "Debe levantar observaciones"
                    Else
                        Me.imgGrabarFicha.Visible = False
                        Me.btnSolicitaAprobacion.Visible = True
                        btnObervar.Visible = False
                        TxtObservaciones.Enabled = False
                    End If
                Case "PEN"
                    lblMsg.Text = "Debe ingresar Observaciones"
                    Me.btnSolicitaAprobacion.Visible = False
                    Me.imgGrabarFicha.Visible = True
                    Me.btnObervar.Visible = False
                    Session("actObs") = False
                Case Else
                    Me.imgGrabarFicha.Visible = True
                    Me.btnSolicitaAprobacion.Visible = False
                    btnObervar.Visible = False

            End Select
            Me.lblEstado.Text = .Item("chr_estado").ToString
            If .Item("chr_TipoServicio").ToString = "T" Then
                Me.rdTiposervicio1.Checked = True
                Me.rdTiposervicio2.Checked = False
            Else
                Me.rdTiposervicio2.Checked = True
                Me.rdTiposervicio1.Checked = False
            End If
            Me.lblObservaciones.Text = .Item("var_Observaciones").ToString
            Me.lblUsuario.Text = .Item("co_usua_modi").ToString
        End With

        Dim lobjPreguntas As New clsEvaluar
        Session("dtDatos1") = lobjPreguntas.ListarPreguntas(tipoServicio)
        BindGridTM()

        If (xData2.Rows.Count() > 0) Then
            CargarDatosEvaluacion(xData2)
        End If
        If (xData3.Rows.Count() > 0) Then
            CargarItemOrdenServicio(xData3)
        End If
        objDT = Nothing
        lobjOrdenServicio = Nothing

    End Sub
    'Public Sub CargarDatosEvaluacion(ByVal strNumOS As String, ByVal strItem As String)
    Public Sub CargarDatosEvaluacion(ByVal Dta As DataTable)
        Dim logObjDatosEval As New clsEvaluar
        Dim dt As DataTable

        Dim estadoEval As String
        'dt = logObjDatosEval.Mostrar_DatosEvaluados(strNumOS, strItem)
        dt = Dta
        If (dt.Rows.Count() > 0) Then
            estadoEval = dt.Rows(0).Item("ESTADO_CONFORMIDAD").ToString
            txtResultado.Text = dt.Rows(0).Item("RESULTADO")

            For i As Integer = 0 To grvEval.Rows.Count - 1
                Dim dwnResp As DropDownList
                dwnResp = grvEval.Rows(i).FindControl("dwnRespuesta")
                Session("dtEval") = dt

                Dim lobjPreguntas As New clsEvaluar

                dwnResp.DataSource = dt
                dwnResp.DataValueField = "VALUE"
                dwnResp.DataTextField = "RESPUESTA"
                dwnResp.DataBind()
                dwnResp.SelectedValue = dt.Rows(i).Item("VALUE")
            Next


            'If estadoEval = "PEN" Then
            '    lblMsg.Text = "Debe ingresar Observaciones"
            '    Me.btnSolicitaAprobacion.Visible = False
            '    Me.imgGrabarFicha.Visible = True
            '    Session("actObs") = False

            'Else
            '    lblMsg.Text = ""
            '    Me.imgGrabarFicha.Visible = False
            '    TxtObservaciones.Enabled = False
            'End If
            grvEval.Enabled = False
        End If

    End Sub
    Public Sub CargarItemOrdenServicio(ByVal Dta As DataTable)
        grvItem.DataSource = Dta
        grvItem.DataBind()
    End Sub
    Protected Sub imgGrabarFicha_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGrabarFicha.Click
        If Session("actObs") = True Then
            If fValida() = False Then Exit Sub
            'If fResulEval() = False Then Exit Sub
            If fActDataIns() = False Then Exit Sub
        Else
            fActuEval()
        End If

    End Sub
    Private Sub fActuEval()
        Dim clsData As New clsFichaProv
        Dim dtCorreo As DataTable
        If Len(TxtObservaciones.Text) > 0 Then
            clsData.Estado = "ACT"
            clsData.Item = Session("Item")
            clsData.Usuario = Session("@Usuario")
            clsData.NumeroOrdenServicio = lblNroOrdeServicio.Text

            If clsData.ActualizarEstadoConformidad(clsData.NumeroOrdenServicio, clsData.Item, TxtObservaciones.Text, clsData.Estado, clsData.Usuario) = True Then
                'ENVIO DE CORREO DE RESULTADO DESAPROBADO
                If Session("FlgObser") = "1" Then 'si esta con observaciones por levantar
                    Dim IdConformidad As String
                    IdConformidad = clsData.fncObtenerConformidad(lblNroOrdeServicio.Text, Session("Item"))
                    dtCorreo = clsData.CargarCorreoProveedores(lblNroOrdeServicio.Text, Session("@Usuario"))
                    EnviarEmail_Proveedor(dtCorreo, IdConformidad, txtResultado.Text)
                    Me.btnSolicitaAprobacion.Visible = False
                    btnObervar.Visible = True
                    TxtObservaciones.Enabled = False
                    Me.imgGrabarFicha.Visible = False
                    lblMsg.ForeColor = Drawing.Color.Red
                    lblMsg.Text = "Debe levantar observaciones"
                Else
                    Me.btnSolicitaAprobacion.Visible = True
                    Me.imgGrabarFicha.Visible = False
                    TxtObservaciones.Enabled = False
                    lblMsg.ForeColor = Drawing.Color.Red
                    lblMsg.Text = "Datos actualizados con éxito"
                End If
            Else
                Me.btnSolicitaAprobacion.Visible = False
                Me.imgGrabarFicha.Visible = True
            End If
        Else
            lblMsg.Text = "Debe ingresar Observaciones"
            TxtObservaciones.Focus()
        End If
    End Sub
    Private Function fValida() As Boolean
        fValida = False

        If Me.wdpFechaInicio.Text = "" Then
            Me.wdpFechaInicio.Focus()
            lblMsg.Text = "Por favor seleccione la fecha de inicio del servicio.. !"
            Exit Function
        End If
        If wdpFechaFin.Text = "" Then
            Me.wdpFechaFin.Focus()
            lblMsg.Text = "Por favor seleccione la fecha final del servicio.. !"
            Exit Function
        End If
        If Me.TxtCodigoTrabajador.Text = "" Then
            Me.TxtCodigoTrabajador.Focus()
            Me.lblMsg.Text = "Por favor ingresa el usuario que solicita este servicio.. !"
            Exit Function
        End If
        Dim FechIni As Date = CType(wdpFechaInicio.Text, Date)
        Dim FechFin As Date = CType(wdpFechaFin.Text, Date)
        Dim result As Integer = DateTime.Compare(FechIni, FechFin)
        Dim fechEmis As Date = CType(lblFecha.Text, Date)
        Dim result2 As Integer = DateTime.Compare(fechEmis, FechIni)
        Dim result3 As Integer = DateTime.Compare(fechEmis, FechFin)
        If (result > 0) Then
            Me.wdpFechaInicio.Focus()
            lblMsg.Text = "La fecha de inicio no debe ser mayor a la fecha Fin del servicio.. !"
            Exit Function
        End If
        If (result2 > 0) Then
            Me.wdpFechaInicio.Focus()
            lblMsg.Text = "La fecha de inicio no debe ser menor a la fecha de emisión.. !"
            Exit Function
        End If
        If (result3 > 0) Then
            Me.wdpFechaFin.Focus()
            lblMsg.Text = "La fecha de Fin no debe ser menor a la fecha de emisión.. !"
            Exit Function
        End If
        For i As Integer = 0 To Me.grvEval.Rows.Count - 1

            Dim por As String = grvEval.DataKeys(i).Item("Porcentaje").ToString()

            Dim Pregunta As Label
            'Pregunta = e.Row.FindControl("Pregunta")
            Pregunta = grvEval.Rows(i).FindControl("lblPregunta")

            Dim NumeroSolicitud As DropDownList = Me.grvEval.Rows(i).FindControl("dwnRespuesta")
            Dim valor As String = NumeroSolicitud.SelectedValue
            If valor = "" Then
                NumeroSolicitud.Focus()
                lblMsg.Text = "Por Favor seleccionar una evaluación de servicio para la pregunta - " + Pregunta.Text
                Exit Function
            End If
        Next

        fValida = True
    End Function

    Private Function fActDataIns() As Boolean
        Dim clsData As New clsFichaProv
        Dim strEstado As String = ""
        fActDataIns = True
        Dim dtDet As New DataTable
        Dim drFila As DataRow

        If CType(Session("dtDatos1"), DataTable).Columns.Contains("IdEval") Then
            CType(Session("dtDatos1"), DataTable).Clear()
        Else
            CType(Session("dtDatos1"), DataTable).Clear()
            CType(Session("dtDatos1"), DataTable).Columns.Add("IdEval")
            CType(Session("dtDatos1"), DataTable).Columns.Add("Promedio")
            CType(Session("dtDatos1"), DataTable).Columns.Remove("Pregunta")
        End If

        Dim numProm As Decimal = 0

        For i = 0 To grvEval.Rows.Count - 1
            drFila = CType(Session("dtDatos1"), DataTable).NewRow()

            Dim IdPregunta As Label
            IdPregunta = CType(grvEval.Rows(i).FindControl("lblIdPregunta"), Label)

            Dim dwnResp As DropDownList
            dwnResp = CType(grvEval.Rows(i).FindControl("dwnRespuesta"), DropDownList)
            Dim Porcentaje As Label
            Porcentaje = CType(grvEval.Rows(i).FindControl("lblPorcentaje"), Label)

            drFila.Item("IdPregunta") = IdPregunta.Text
            drFila.Item("IdEval") = CType(dwnResp.Text, Integer)
            drFila.Item("Porcentaje") = CType(Porcentaje.Text, Integer)
            'drFila.Item("Promedio") = (CType(dwnResp.Text, Decimal) / 100) * CType(Porcentaje.Text, Integer)
            drFila.Item("Promedio") = (CType(dwnResp.Text, Decimal) / 100) * CType(Porcentaje.Text, Decimal)
            'numProm = numProm + (CType(dwnResp.Text, Decimal) / 100) * CType(Porcentaje.Text, Integer)
            numProm = numProm + (CType(dwnResp.Text, Decimal) / 100) * CType(Porcentaje.Text, Decimal)

            CType(Session("dtDatos1"), DataTable).Rows.Add(drFila)
            'dtDet.Rows.Add(drFila)

            'Session("dtDet") = dtDet
        Next
        'EVALUACIONES PONDERADO
        If numProm < 3 Then

            txtResultado.Text = "Muchas Observaciones"
            clsData.Resultado = txtResultado.Text
            clsData.FlgLevantarObservaciones = "1"
            Session("FlgObser") = clsData.FlgLevantarObservaciones
            If Len(TxtObservaciones.Text) > 0 Then
                lblMsg.Text = ""
                clsData.Estado = "ACT"
                btnObervar.Visible = True
            Else
                TxtObservaciones.Focus()
                lblMsg.Text = "Debe ingresar Observaciones"
                clsData.Estado = "PEN"
            End If

        ElseIf (3 <= numProm And numProm < 4) Then
            txtResultado.Text = "Con Observaciones"
            clsData.Resultado = txtResultado.Text
            clsData.FlgLevantarObservaciones = "0"
            If Len(TxtObservaciones.Text) > 0 Then
                lblMsg.Text = ""
                clsData.Estado = "ACT"
            Else
                TxtObservaciones.Focus()
                lblMsg.Text = "Debe ingresar Observaciones"
                clsData.Estado = "PEN"
            End If
        Else
            txtResultado.Text = "Aprobado"
            clsData.Resultado = txtResultado.Text
            clsData.FlgLevantarObservaciones = "0"
            lblMsg.Text = ""
            clsData.Estado = "ACT"
        End If
        clsData.FechaInicio = Right(Me.wdpFechaInicio.Text, 4) + Mid(Me.wdpFechaInicio.Text, 4, 2) + Mid(Me.wdpFechaInicio.Text, 1, 2)
        clsData.FechaFin = Right(Me.wdpFechaFin.Text, 4) + Mid(Me.wdpFechaFin.Text, 4, 2) + Mid(Me.wdpFechaFin.Text, 1, 2)
        clsData.PonderadoTotal = numProm
        clsData.NumeroOrdenServicio = lblNroOrdeServicio.Text
        clsData.CodigoProveedor = Me.lblRuc.Text
        clsData.CodigoSolicitante = Me.TxtCodigoTrabajador.Text
        clsData.Usuario = Session("@Usuario")
        clsData.Observaciones = TxtObservaciones.Text
        If Me.rdTiposervicio1.Checked = True Then
            clsData.TipoServicio = "T"
        Else
            clsData.TipoServicio = "S"
        End If
        clsData.Usuario = Session("@Usuario")
        clsData.Item = Session("Item")
        clsData.EstadoServicio = "ACT"
        Dim dtcorreo As DataTable
        dtcorreo = clsData.CargarCorreoProveedores(lblNroOrdeServicio.Text, Session("@Usuario"))
        If clsData.Insertar_FichaProveedor_Conformidad(CType(Session("dtDatos1"), DataTable)) = True Then
            grvEval.Enabled = False
            If clsData.Estado = "PEN" Then 'si esta en estado pendiente
                Me.btnSolicitaAprobacion.Visible = False
                Me.imgGrabarFicha.Visible = True
                TxtObservaciones.Enabled = True
                Session("actObs") = False
            Else
                'ENVIO DE CORREO DE RESULTADO DESAPROBADO
                If Session("FlgObser") = "1" Then
                    Dim IdConformidad As String
                    IdConformidad = clsData.fncObtenerConformidad(lblNroOrdeServicio.Text, Session("Item"))
                    EnviarEmail_Proveedor(dtcorreo, IdConformidad, txtResultado.Text)

                    btnSolicitaAprobacion.Visible = False
                    btnObervar.Visible = True
                    TxtObservaciones.Enabled = False
                    lblMsg.ForeColor = Drawing.Color.Red
                    lblMsg.Text = "Debe levantar observaciones"
                Else
                    'si esta en estado activo
                    Me.btnSolicitaAprobacion.Visible = True
                    Me.imgGrabarFicha.Visible = False
                    TxtObservaciones.Enabled = False
                    lblMsg.ForeColor = Drawing.Color.Red
                    lblMsg.Text = "Datos actualizados con éxito"
                End If

            End If
        Else
            fActDataIns = False
            lblMsg.Text = clsData.clsError
        End If
    End Function

    Protected Sub imgCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCancelar.Click
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script> history.back(1)</script>")
    End Sub

    Private Sub grvEval_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvEval.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            For i As Integer = 0 To grvEval.Rows.Count
                Dim dwnResp As DropDownList
                dwnResp = e.Row.FindControl("dwnRespuesta")

                Dim Pregunta As Label
                Pregunta = e.Row.FindControl("Pregunta")

                Dim lobjPreguntas As New clsEvaluar

                Dim vcr_Pregunta As String
                vcr_Pregunta = CType(Session("dtDatos1"), DataTable).Rows(i).Item("Pregunta").ToString()
                dwnResp.DataSource = lobjPreguntas.ListarRespuesta(vcr_Pregunta)
                dwnResp.DataValueField = "int_Puntaje"
                dwnResp.DataTextField = "Respuesta"
                dwnResp.DataBind()
                dwnResp.Items.Insert(0, New ListItem("--Seleccione--", ""))
            Next

        End If
    End Sub


    Private Sub btnSolicitaAprobacion_Click(sender As Object, e As System.EventArgs) Handles btnSolicitaAprobacion.Click
        Dim objAprobacion As New clsFichaProv
        Dim ldtCorreos As DataTable
        Dim IdConformidad As String

        Try
            Me.btnSolicitaAprobacion.Attributes.Add("onClick", "javascript:fdesTipoAprobacion(TxtAprobacion,TxtDescripcion);")

            IdConformidad = objAprobacion.fncObtenerConformidad(lblNroOrdeServicio.Text, Session("Item"))
            Dim txtaprob As String = TxtAprobacion.Text
            If objAprobacion.fncSolicitarAprobacionOS_Conformidad(Session("@EMPRESA"), Me.TxtAprobacion.Text, lblNroOrdeServicio.Text, "", "", "PRO", Date.Today.ToString(), "K", "", Session("@Usuario"), "", Session("@Usuario"), "", IdConformidad, Session("Item"), ldtCorreos) = True Then
                EnviarEmail(ldtCorreos, IdConformidad)
                Me.lblMsg.Text = "CONFORMIDAD POR APROBAR PARA SU FINALIZACION"
                Me.btnSolicitaAprobacion.Visible = False
            Else
                lblMsg.Text = "Seleccione un tipo de aprobacion por favor...!"
            End If
        Catch ex As Exception
            lblMsg.Text = "Ha ocurrido un error al Aprobar, comuniquese con Sistemas."
        End Try
    End Sub
#Region "Envio de Email"
   
    Private Sub EnviarEmail(ByRef pdtCorreos As DataTable, ByVal idConformidad As String)
        Dim i As Integer
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""
        strNumeroPedido = idConformidad
        Try
            For i = 0 To pdtCorreos.Rows.Count - 1
                If lstrPara.Trim.Length = 0 Then
                    lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                Else
                    lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
                End If
                lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
            Next i
            'lstrPara = "JCucho@nuevomundosa.com"
            'lstrCopia = "DGamarra@nuevomundosa.com"
            If lstrCopia.Trim.Length = 0 Then
                lstrCopia = ""
            Else
                lstrCopia = lstrCopia
            End If
            With pdtCorreos.Rows(0)
                lstrTitulo = "[Intranet] CONFORMIDAD POR APROBAR."
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO UNA SOLICITUD DE " + _
                                    "APROBACION PARA LA CONFORMIDAD &nbsp;: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                    "<STRONG>" & strNumeroPedido & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                    "</STRONG></FONT></FONT></P>" + _
                                    "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>PERTENECIENTE A LA ORDEN DE SERVICIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & lblNroOrdeServicio.Text & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                    "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
                                    "<A title='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp' href='http://" + System.Web.Configuration.WebConfigurationManager.AppSettings("ServidorWeb").ToString() + "/intrasolution/index.asp'>" + _
                                    "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
                                    "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                    "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
                                    "Por favor no responder este correo.<BR>" + _
                                    "Departamento de Sistemas<BR>" + _
                                    "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                    "-------------------------------------------------------------------------------</P>"
                Dim mailMsg As System.Net.Mail.MailMessage
                mailMsg = New System.Net.Mail.MailMessage()

                'Configurar arreglo para el TO
                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next

                'Si no hay destinatario que lo envie a sistemas
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

                'Configurar arreglo para el CC
                Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
                For lintIndice = 0 To lstrCC_arreglo.Length - 1
                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)

                With mailMsg
                    '.From = New System.Net.Mail.MailAddress("CONFORMIDAD POR APROBAR<aprobaciones@nuevomundosa.com>")
                    .From = New System.Net.Mail.MailAddress(user)
                    .Subject = lstrTitulo
                    .Body = lstrCuerpoMensaje
                    .Priority = System.Net.Mail.MailPriority.High
                    .IsBodyHtml = True
                End With

                Dim Servidor As New System.Net.Mail.SmtpClient
                Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
                Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
                Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
                If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                    Servidor.Credentials = userCredential
                End If
                Servidor.Send(mailMsg)
                Servidor = Nothing


                Dim strDestinatarios As String
                strDestinatarios = "Se Comunico via email a: " + lstrPara + " Con Copia a: " + lstrCopia
                lblError.Text = strDestinatarios
                lblError.Visible = True
            End With
        Catch ex As Exception
            lblError.Text = ""
            lblError.Visible = False
            lblError.Text = "Ha ocurrido un error al enviar email, comuniquese con Sistemas."
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub
    Private Sub EnviarEmail_Proveedor(ByRef pdtCorreos As DataTable, ByVal idConformidad As String, ByVal txtObservaciones As String)
        Dim i As Integer
        Dim lstrCuerpoMensaje As String = ""
        Dim lstrTitulo As String
        Dim lstrPara As String = ""
        Dim lstrCopia As String = ""
        strNumeroPedido = idConformidad
        Try
            For i = 0 To pdtCorreos.Rows.Count - 1
                If lstrPara.Trim.Length = 0 Then
                    lstrPara = pdtCorreos.Rows(i).Item("UsuarioCorreo")
                Else
                    lstrPara = lstrPara + ";" + pdtCorreos.Rows(i).Item("UsuarioCorreo")
                End If
                lstrCopia = pdtCorreos.Rows(i).Item("CorreoCopia")
            Next i
            'lstrPara = "JCucho@nuevomundosa.com"
            'lstrCopia = "DGamarra@nuevomundosa.com"
            If lstrCopia.Trim.Length = 0 Then
                lstrCopia = ""
            Else
                lstrCopia = lstrCopia
            End If
            With pdtCorreos.Rows(0)
                lstrTitulo = "[Intranet] LEVANTAR OBSERVACIONES DE CONFORMIDAD."
                lstrCuerpoMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA ENVIADO EL RESULTADO DE " + _
                                    "EVALUACION DE LA CONFORMIDAD &nbsp;: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                    "<STRONG>" & strNumeroPedido & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                    "</STRONG></FONT></FONT></P>" + _
                                    "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>PERTENECIENTE A LA ORDEN DE SERVICIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & lblNroOrdeServicio.Text & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR></P>" + _
                                    "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>ENVIADA POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Strings.UCase(.Item("Creador").ToString) & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR></P>" + _
                                    "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>EL RESULTADO DE LA EVALUACION DE LA CONFORMIDAD ES" + _
                                    "&nbsp;: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
                                    "<STRONG>" & Strings.UCase(txtObservaciones.ToString) & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
                                    "</STRONG></FONT></FONT></P>" + _
                                    "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
                                    "Este correo ha sido generado automáticamente por el módulo de conformidad.<BR>" + _
                                    "Por favor no responder este correo.<BR>" + _
                                    "Departamento de Sistemas<BR>" + _
                                    "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
                                    "-------------------------------------------------------------------------------</P>"
                Dim mailMsg As System.Net.Mail.MailMessage
                mailMsg = New System.Net.Mail.MailMessage()

                'Configurar arreglo para el TO
                Dim lstrTo_arreglo() As String = lstrPara.Split(";")
                For lintIndice = 0 To lstrTo_arreglo.Length - 1
                    If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
                Next

                'Si no hay destinatario que lo envie a sistemas
                If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

                'Configurar arreglo para el CC
                Dim lstrCC_arreglo() As String = lstrCopia.Split(";")
                For lintIndice = 0 To lstrCC_arreglo.Length - 1
                    If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
                Next

                Dim user As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Usuario").ToString()
                Dim password As String = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Password").ToString()
                Dim userCredential As New System.Net.NetworkCredential(user, password)

                With mailMsg
                    '.From = New System.Net.Mail.MailAddress("VALE DE ALMACEN POR APROBAR<aprobaciones@nuevomundosa.com>")
                    '.From = New System.Net.Mail.MailAddress("USUARIO EVALUACION <" + lstrCopia + ">")
                    .From = New System.Net.Mail.MailAddress(user)
                    .Subject = lstrTitulo
                    .Body = lstrCuerpoMensaje
                    .Priority = System.Net.Mail.MailPriority.High
                    .IsBodyHtml = True
                End With

                Dim Servidor As New System.Net.Mail.SmtpClient
                Servidor.Host = System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Servidor").ToString() '"192.168.116.2"
                Servidor.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Puerto").ToString())
                Servidor.EnableSsl = Convert.ToBoolean(Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_SSL").ToString()))
                If System.Web.Configuration.WebConfigurationManager.AppSettings.Item("Correo_Credenciales").ToString().Equals("1") Then
                    Servidor.Credentials = userCredential
                End If
                Servidor.Send(mailMsg)
                Servidor = Nothing


                Dim strDestinatarios As String
                strDestinatarios = "Se Comunico via email a: " + lstrPara + " Con Copia a: " + lstrCopia
                lblError.Text = strDestinatarios
                lblError.Visible = True
            End With
        Catch ex As Exception
            lblError.Text = ""
            lblError.Visible = False
            lblError.Text = "Ha ocurrido un error al enviar email, comuniquese con Sistemas."
            ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script>alert('No se pudó enviar el correo electronico.');</script>")
        End Try
    End Sub
#End Region

    Private Sub btnObervar_Click(sender As Object, e As System.EventArgs) Handles btnObervar.Click
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Si" Then
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Si Levatara Observaciones')", True)
            Dim clsData As New clsFichaProv
            If clsData.ActualizarObservaciones(lblNroOrdeServicio.Text, Session("Item"), Session("@USUARIO")) = True Then
                btnSolicitaAprobacion.Visible = True
                btnObervar.Visible = False
                imgGrabarFicha.Visible = False
                lblMsg.Text = ""
                lblError.Text = ""
            Else
                btnSolicitaAprobacion.Visible = False
                btnObervar.Visible = True
                lblError.Text = ""
                imgGrabarFicha.Visible = False
            End If
        End If


    End Sub
End Class