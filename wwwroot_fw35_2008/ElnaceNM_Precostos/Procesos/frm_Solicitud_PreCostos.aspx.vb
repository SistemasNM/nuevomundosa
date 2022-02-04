Imports System 
Imports CostosLib
Imports OFISIS
Public Partial Class frm_Solicitud_PreCostos
    Inherits System.Web.UI.Page
    Dim mFila1 As Integer
    Dim mFila2 As Integer
    Dim strFechaIni, strFechaFin As String
    Dim strCodRespuesta As String = ""
    Dim strMsgResponse As String = ""

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            
        If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
            Dim objRequest As New BLITZ_LOCK.clsRequest
            Session("Usuario") = objRequest.Decripta(Request("Usuario"))
        End If

        Session("Usuario") = "aampuerp"

        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        If (Session("Usuario") Is Nothing) OrElse Session("Usuario") = "" Then
            Response.Redirect("/webproduccion/noaccess.htm", True)
        End If

        '-----------------------------------------------------------------------
        '--FINAL: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
    End Sub 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Session("Usuario") = "Darwin"
        Session("empresa") = "01"
        If Page.IsPostBack =False 
            strFechaIni = TxtFechaIni.Text
            strFechaFin = TxtFechaFin.Text
            sCargaRequisiciones(me.TxtNum_Requisicion.Text,strFechaIni,strFechaFin)
        End If
        btnAnular.Attributes.Add("OnClick", "return fAnular();")
    End Sub

    Protected Sub btnGraba_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGraba.Click
        If fValida() = False Then Exit Sub
        fActDataIns()
        Limpiar("CABECERA")
    End Sub

    Private Sub Limpiar(ByVal valor)
        '*****************************************************************************************************
        'Objetivo   : Limpia los obejtos de los Pre-Costos
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 2010.01.06
        'Modificado : 
        '*****************************************************************************************************
        Select Case valor
            Case "CABECERA"
                Me.TxtFecha.Text = ""
                Me.TxtNumerorequisicion.Text = ""
                Me.TxtObservaciones.Text = ""
            Case "DETALLE"
                Me.TxtArticuloCrudoCod.Text = ""
                TxtArticuloCrudoNom.Text = ""
                Me.TxtArticuloBase.Text = ""
                Me.TxtCod_Acabado.Text = ""
                Me.TxtDesc_Acabado.Text = ""
                Me.TxtColorCod.Text = ""
                Me.TxtColoranteCod.Text = ""
                Me.TxtColoranteDes.Text = ""
                Me.TxtObservacionDetalle_Articulo.Text = ""
        End Select
    End Sub

    Private Function fActDataIns() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Registra los datos de la cabecera de los Pre-Costos
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 2010.01.06
        'Modificado : 
        '*****************************************************************************************************
        Dim lobjPre_Costos As New CostosLib.clsRequisicionPreCosto
        Dim var_numRequisicion As String = ""
        fActDataIns = True
        
        lobjPre_Costos.Fecha=Mid(me.TxtFecha.Text,4,2) + "/" + Mid(me.TxtFecha.Text,1,2) + "/" + Right(me.TxtFecha.Text,4)
        lobjPre_Costos.Observaciones=Me.TxtObservaciones.Text 
        lobjPre_Costos.Estado=me.ddlEstado.SelectedValue 
        lobjPre_Costos.Usuario = Session("Usuario").ToString
        var_numRequisicion = lobjPre_Costos.RequisicionPrecosto_Insertar2
        'If lobjPre_Costos.RequisicionPrecosto_Insertar <> "" Then
        If var_numRequisicion <> "" Then
            If Not txtCodReq.Text.Trim.Equals("") Then
                vincularReqAnalisisTela(var_numRequisicion, txtCodReq.Text.Trim)
            End If
            sCargaRequisiciones("", "", "")
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Datos actualizado con éxito"
        Else
            fActDataIns = False
            lblMsg.Text = lobjPre_Costos.clsError
        End If
        lobjPre_Costos = Nothing
    End Function

    Private Function fValida() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Valida el ingreso de datos para la tabla Pre-Costos
        'Autor      : Dawin Ccorahua Livon
        'Creado     : 00/00/0000
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        fValida = False
        If TxtFecha.Text.Trim.Length = 0 Then
            lblMsg.Text = "Por favor ingresar la fecha de requisicion...!"
            Exit Function
        End If
        fValida = True
    End Function

    Private Sub sCargaRequisiciones(strNumeroreRequisicion,strFechaIni,StrFechaFin)
        '*****************************************************************************************************
        'Objetivo   : Muestra los datos de las requisiciones
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 22/06/2010
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        lblMsg.Text = ""

        panActualiza.Visible = False
        panListado.Visible = True
        Dim lobjPre_Costos As New clsRequisicionPreCosto
        Dim objDT As New DataTable
        Dim strNumeroRequisicion As String 
        strNumeroRequisicion=Me.TxtNum_Requisicion.Text 
        mFila1 = 0
                
        lobjPre_Costos.NumeroRequisicion=strNumeroRequisicion
        If lobjPre_Costos.RequisicionPrecosto_Listar(objDT,strFechaIni,StrFechaFin) = True Then
            lblReg.Text = objDT.Rows.Count
            grdData.DataSource = objDT
            grdData.DataBind()
        Else
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = lobjPre_Costos.clsError
        End If
        'txtRegSel.Text = ""
        objDT = Nothing
        lobjPre_Costos = Nothing
    End Sub

    Private Sub sAsignaVal(ByVal bSw As Boolean)
        '*****************************************************************************************************
        'Objetivo   : Asignar los valores a los contrles de entrada de dato
        'Autor      : CPT
        'Creado     : 00/00/0000
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        'Dim cls As New clsFuncDatos
        Dim iFila As Integer
        iFila = CInt(txtRegSel.Text)
        With grdData.Items(iFila)
            TxtNumerorequisicion.Text = CType(.Cells(0).Text, String)
            TxtFecha.Text = CType(.Cells(1).Text, String)
            TxtObservaciones.Text = CType(.Cells(2).Text, String)
            ddlEstado.SelectedValue = CType(.Cells(3).Text, String)
        End With
        'If lblEstado.Text = "ANU" Then
        '    btnActivar.Visible = True
        'End If
    End Sub

    Private Sub vincularReqAnalisisTela(ByVal strNumRequi As String, ByVal strCodRequer As String)
        Dim dtResponse As New DataTable
        Dim objclsEstudioTela As New clsEstudioTela

        Try

            objclsEstudioTela.Accion = "V"
            objclsEstudioTela.CodCrudo = ""
            objclsEstudioTela.CodAcabado = ""
            objclsEstudioTela.IdRequer = strCodRequer
            objclsEstudioTela.FecGen = ""
            objclsEstudioTela.CodArtEstudio = ""
            objclsEstudioTela.IdEstado = ""
            objclsEstudioTela.CodOp = ""
            objclsEstudioTela.CodArt = ""
            objclsEstudioTela.DescArt = ""
            objclsEstudioTela.Color = ""
            objclsEstudioTela.McaArt = ""
            objclsEstudioTela.NumRequi = strNumRequi
            objclsEstudioTela.Usuario = "AAMPUERO"

            dtResponse = objclsEstudioTela.CRUDEstudioTela(objclsEstudioTela)

            Me.lblMsg.Text = dtResponse.Rows(0).Item("MensajeRespuesta").ToString()
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString() + " Enviar pantalla de error a sistemas."
        Finally
            dtResponse.Dispose()
            objclsEstudioTela = Nothing
        End Try
    End Sub

    Sub validarArticulosAsociadosEstudio(ByVal strNumRequi As String)
        Dim dtResponse As New DataTable
        Dim objclsEstudioTela As New clsEstudioTela

        Try

            objclsEstudioTela.Accion = "S"
            objclsEstudioTela.CodCrudo = ""
            objclsEstudioTela.CodAcabado = ""
            objclsEstudioTela.IdRequer = ""
            objclsEstudioTela.FecGen = ""
            objclsEstudioTela.CodArtEstudio = ""
            objclsEstudioTela.IdEstado = ""
            objclsEstudioTela.CodOp = ""
            objclsEstudioTela.CodArt = ""
            objclsEstudioTela.DescArt = ""
            objclsEstudioTela.Color = ""
            objclsEstudioTela.McaArt = ""
            objclsEstudioTela.NumRequi = strNumRequi
            objclsEstudioTela.Usuario = "AAMPUERO"

            dtResponse = objclsEstudioTela.CRUDEstudioTela(objclsEstudioTela)

            strCodRespuesta = dtResponse.Rows(0).Item("Codigo_Respuesta").ToString()
            strMsgResponse = dtResponse.Rows(0).Item("MensajeRespuesta").ToString()
        Catch ex As Exception
            strCodRespuesta = "300"
            strMsgResponse = ex.Message.ToString() + " Enviar pantalla de error a sistemas."
        Finally
            dtResponse.Dispose()
            objclsEstudioTela = Nothing
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuevo.Click
        txtOpc.Text = "NUE"
        lblMsg.Text = ""
        panListado.Visible = False
        panActualiza.Visible = True
        'sAsignaVal(False)
    End Sub
   
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancelar.Click
        lblMsg.Text = ""
        panActualiza.Visible = False
        panListado.Visible = True
        txtRegSel.Text = ""
    End Sub


    Protected Sub btnNuevoO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuevoO.Click
        txtOpcO.Text = "NUE"
        lblMsgO.Text = ""
        panListadoO.Visible = False
        panActualizaO.Visible = True
        'sAsignaValOperacion(False)
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEditar.Click
        txtOpc.Text = "EDI"
        lblMsg.Text = ""
        panListado.Visible = False
        panActualiza.Visible = True
    End Sub

    Protected Sub btnCancelarO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancelarO.Click
        lblMsgO.Text = ""
        panActualizaO.Visible = False
        panListadoO.Visible = True
        txtRegSelO.Text = ""
    End Sub

    Private Sub grdData_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdData.ItemCommand
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Select Case e.CommandName
                Case "DETALLE"
                    TxtRequisicion_Detalle.Text = e.Item.Cells(0).Text
                    TxtFecha_Detalle.Text = e.Item.Cells(1).Text
                    Me.TxtObservaciones_Detalle.Text = e.Item.Cells(2).Text
                    Me.lblNroRequisicion.Text = e.Item.Cells(0).Text
                    Me.LblFecha.Text = e.Item.Cells(1).Text
                    'cambiar de pagina
                    sCargaDetalle_Requisiciones()
                    'tabpage10.Visible=False 
                    'tabpage21.Visible=True 

                    'panListado.Visible=False 
                    'panListadoO.Visible=True 

                Case "ESTADO"
                    validarArticulosAsociadosEstudio(e.Item.Cells(0).Text)
                    If strCodRespuesta.Equals("100") Or strCodRespuesta.Equals("300") Then
                        MsgBox(strMsgResponse)
                    ElseIf strCodRespuesta.Equals("200") Then
                        MsgBox(strMsgResponse) 'sSolicitaAprobacion_Requisicion(e.Item.Cells(0).Text)
                    End If
            End Select
            'tabpage21.Focus()
        End If
    End Sub

    Private Sub grdData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdData.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onclick", "fSelFila(this," + mFila1.ToString + ")")
            mFila1 = mFila1 + 1
            Dim ldrvItem As DataRowView = CType(e.Item.DataItem, DataRowView)
            Select Case e.Item.ItemType
                Case ListItemType.AlternatingItem, ListItemType.Item
                    If CInt(ldrvItem("Detalle")) = 0 Or ldrvItem("chr_Estado") <> "ACT" Then
                        Dim btn1 As Button = CType(e.Item.FindControl("btnEditar"), Button)
                        btn1.Visible = False
                    Else
                        Dim btn2 As Button = CType(e.Item.FindControl("btnEditar"), Button)
                        btn2.Attributes.Add("OnClick", "javascript: return fConfirma('Actualizar los cambios ?');")
                    End If
            End Select
        End If
    End Sub

    Protected Sub btnDetalle_Click(ByVal sender As Object, ByVal e As EventArgs)
        txtOpc.Text = "EDI"
        lblMsg.Text = ""
        panListado.Visible = False
        panActualiza.Visible = True
        sAsignaVal(True)
    End Sub

    Protected Sub btnGrabarO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGrabarO.Click
        If fValida_Detalle() = False Then Exit Sub
         fActDataIns_Detalle 
         Limpiar("DETALLE")
    End Sub

    Private Function fValida_Detalle() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Valida el ingreso de datos para la tabla Pre-Costos
        'Autor      : Dawin Ccorahua Livon
        'Creado     : 13/09/2010
        'Modificado : 00/00/0000
        '*****************************************************************************************************

        fValida_Detalle = False
        If Me.TxtArticuloCrudoCod.Text.Trim.Length =0 Then
            lblMsg.Text = "Por favor ingresar el articulo crudo...!"
            Exit Function
        End If
        
        If TxtArticuloBase.Text.Trim.Length = 0 Then
            lblMsg.Text = "Por favor ingresar el articulo base...!"
            Exit Function
        End If
         

        If Me.TxtDesc_Acabado.Text.Trim.Length = 0 Then
            lblMsg.Text = "Por favor ingresar el acabado del articulo...!"
            Exit Function
        End If

        If TxtColorCod.Text.Trim.Length = 0 Then
            lblMsg.Text = "Por favor ingresar el color ...!"
            Exit Function
        End If
        fValida_Detalle = True
    End Function

    Private Function fActDataIns_Detalle() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Registra los datos de la cabecera de los Pre-Costos
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 2010.01.06
        'Modificado : 
        '*****************************************************************************************************
        Dim lobjPre_Costos As new CostosLib.clsRequisicionPreCosto 
        fActDataIns_Detalle = True
        
        lobjPre_Costos.NumeroDetalle_Requisicion=lblNroRequisicion.Text 
        lobjPre_Costos.ArticuloCrudo=me.TxtArticuloCrudoCod.Text 
        lobjPre_Costos.ArticuloBase=Me.TxtArticuloBase.Text 
        
        lobjPre_Costos.Acabado=Me.TxtCod_Acabado.Text 
        lobjPre_Costos.Color=Me.TxtColorCod.Text 
        lobjPre_Costos.Colorante=Me.TxtColoranteCod.Text  
        lobjPre_Costos.Estado_Detalle="ACT"
        lobjPre_Costos.Observacion_Detalle=Me.TxtObservacionDetalle_Articulo.Text 
        lobjPre_Costos.Usuario_Detalle=Session("Usuario").ToString
        if lobjPre_Costos.Detalle_RequisicionPrecosto_Insertar<>"" Then
            sCargaDetalle_Requisiciones()
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsgO.Text = "Datos actualizado con éxito"
        Else
            fActDataIns_Detalle = False
            lblMsgO.Text = lobjPre_Costos.clsError
        End If
        lobjPre_Costos= Nothing
    End Function
    
    Private Sub sCargaDetalle_Requisiciones()
        '*****************************************************************************************************
        'Objetivo   : Muestra los datos de los detalle de cada requisicion
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 13/09/2010
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        lblMsgO.Text = ""
        panActualizaO.Visible = False
        panListadoO.Visible = True
        Dim lobjPre_Costos As New clsRequisicionPreCosto
        Dim objDT As New DataTable
        Dim strNumeroRequisicion As String 
        strNumeroRequisicion=""
        strNumeroRequisicion=me.lblNroRequisicion.Text
        mFila2 = 0
        lobjPre_Costos.NumeroDetalle_Requisicion= strNumeroRequisicion
        If lobjPre_Costos.Detalle_RequisicionPrecosto_Listar(objDT) = True Then
            lblRegO.Text = objDT.Rows.Count
            grdDataO.DataSource = objDT
            grdDataO.DataBind()
        Else
            lblMsgO.ForeColor = Drawing.Color.Red
            lblMsgO.Text = lobjPre_Costos.clsError
        End If
        objDT = Nothing
        lobjPre_Costos = Nothing
    End Sub

    Private Sub sSolicitaAprobacion_Requisicion(Numero_Requisicion)
        lblMsg.Text=Numero_Requisicion
        If Numero_Requisicion<>"" Then
 
            Dim lobjPre_Costos As New clsRequisicionPreCosto
            Dim ldtDocumentosAprobar, ldtArticulos As DataTable
            Dim ldstResultados As DataSet
            Dim strNumeroDocumento, lstrAsunto, lstrMensaje As String
            lobjPre_Costos.NumeroDetalle_Requisicion = Numero_Requisicion
            lobjPre_Costos.Detalle_RequisicionPrecosto_Listar(ldtDocumentosAprobar)
            Dim lobjAprobaciones As New OFISIS.OFISEGU.Aprobaciones(Session("empresa"), Session("Usuario"))
            For i = 0 To ldtDocumentosAprobar.Rows.Count - 1
                strNumeroDocumento = ldtDocumentosAprobar.Rows(i).Item("int_NumeroSecuencia").ToString

                If InStr(ldtDocumentosAprobar.Rows(i).Item("Linea").ToString, "DENIM") = 1 Then
                    lobjAprobaciones.Generar_AprobacionPreCostos(Numero_Requisicion, 0, Session("empresa"), "067", ldtDocumentosAprobar.Rows(i).Item("int_NumeroSecuencia").ToString, Format(Now.Date, "dd/MM/yyyy"), "", "PRO", Format(Now.Date, "dd/MM/yyyy"), "K", "", Session("Usuario"), "", Session("Usuario"), "", "SOL", "", ldstResultados)
                Else
                    lobjAprobaciones.Generar_AprobacionPreCostos(Numero_Requisicion, 0, Session("empresa"), "068", ldtDocumentosAprobar.Rows(i).Item("int_NumeroSecuencia").ToString, Format(Now.Date, "dd/MM/yyyy"), "", "PRO", Format(Now.Date, "dd/MM/yyyy"), "K", "", Session("Usuario"), "", Session("Usuario"), "", "SOL", "", ldstResultados)
                End If
            Next
            lobjPre_Costos.NumeroDetalle_Requisicion = Numero_Requisicion
            lobjPre_Costos.Detalle_RequisicionPrecosto_Listar(ldtArticulos)
            lstrAsunto = "Requisicion de pre-costo numero : " + Numero_Requisicion
            lstrMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE INGRESO UNA NUEVA SOLICITUD DE PRE-COSTOS POR FAVOR GENERAR...!: <FONT style='BACKGROUND-COLOR: #ffff66'>"
            lstrMensaje = lstrMensaje + "<STRONG>" & Numero_Requisicion & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR><BR>"
            For i = 0 To ldtArticulos.Rows.Count - 1

                lstrMensaje = lstrMensaje + "<STRONG>Numero Solicitud de Validación : " & ldtDocumentosAprobar.Rows(i).Item("int_NumeroSecuencia").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>Articulo Crudo : " & ldtArticulos.Rows(i).Item("var_ArticuloCrudo").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>Articulo Base  : " & ldtArticulos.Rows(i).Item("var_ArticuloBase").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>Acabado        : " & ldtArticulos.Rows(i).Item("var_Acabado").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>Color          : " & ldtArticulos.Rows(i).Item("var_Color").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>Colorante      : " & ldtArticulos.Rows(i).Item("var_Colorante").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>Observaciones  : " & ldtArticulos.Rows(i).Item("var_Observaciones").ToString & "</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR>" + _
                "<STRONG>------------------------------------------------------------------------</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;<BR><BR>"

            Next i

            lstrMensaje = lstrMensaje + "</STRONG></FONT></FONT></P><BR>" + _
            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>Solicitado por el usuario :&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>" & Session("Usuario") & "</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
            "<A title='http://servnmprb/intrasolution/index.asp' href='http://servnmprb/intrasolution/index.asp'>" + _
            "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
            "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>--------------------------------------------------------------------------------------------------<BR>" + _
            "Este correo ha sido generado automáticamente por el módulo de aprobaciones.<BR>" + _
            "Por favor no responder este correo.<BR>" + _
            "Departamento de Sistemas<BR>" + _
            "Cía. Industrial Nuevo Mundo S.A.<BR>" + _
            "--------------------------------------------------------------------------------------------------</P>"
            ModMensaje.fnc_EnviarMensaje("", "0026", lstrAsunto, lstrMensaje)
            lobjAprobaciones = Nothing
            ldtDocumentosAprobar = Nothing
            lobjPre_Costos = Nothing
            ldtArticulos = Nothing

        End If
    End Sub
        
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        strFechaIni= Right(me.TxtFechaIni.Text,4)+Mid(me.TxtFechaIni.Text,4,2)+Mid(me.TxtFechaIni.Text,1,2)
        strFechaFin= Right(me.TxtFechaFin.Text,4)+Mid(me.TxtFechaFin.Text,4,2)+Mid(me.TxtFechaFin.Text,1,2)
        sCargaRequisiciones(me.TxtNum_Requisicion.Text,strFechaIni,strFechaFin )
    End Sub

     
    Protected Sub btnAnular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnular.Click
        '*****************************************************************************************************
        'Objetivo   : Elimina un pre-costo logicamente en la base de datos
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 14/09/2010
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        Dim iFila As Integer
        Dim strCodigoRequisicion As String
        Dim lobjPre_Costos As New clsRequisicionPreCosto
        iFila = CInt(txtRegSel.Text) + 1
        With grdData.Items(iFila)
            strCodigoRequisicion = CType(.Cells(0).Text, String)
        End With
        lobjPre_Costos.NumeroRequisicion = strCodigoRequisicion
        lobjPre_Costos.Usuario = Session("Usuario").ToString
        If lobjPre_Costos.RequisicionPrecosto_Eliminar() = True Then
            strFechaIni = Right(Me.TxtFechaIni.Text, 4) + Mid(Me.TxtFechaIni.Text, 4, 2) + Mid(Me.TxtFechaIni.Text, 1, 2)
            strFechaFin = Right(Me.TxtFechaFin.Text, 4) + Mid(Me.TxtFechaFin.Text, 4, 2) + Mid(Me.TxtFechaFin.Text, 1, 2)
            sCargaRequisiciones(Me.TxtNum_Requisicion.Text, strFechaIni, strFechaFin)
            lblMsg.ForeColor = Drawing.Color.Blue
        Else
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = lobjPre_Costos.clsError
        End If
        lblMsg.ForeColor = Drawing.Color.Red
        lblMsgO.Text = "Datos actualizado con éxito"
        lobjPre_Costos = Nothing
    End Sub

End Class