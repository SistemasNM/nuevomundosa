Imports NuevoMundo
Imports System
Imports System.Data
Imports System.Web

Public Class frmFichaProveedor
    Inherits System.Web.UI.Page
    Dim strNumeroOrdenServicio As String
    Dim StrNumeroOpcion As String

#Region "Procedimientos"
    'Estilo de la grilla
    Private Sub IniStyles()
        wdgDetalle.StyleSetPath = "../style"
        wdgDetalle.StyleSetName = "Infragistics"
    End Sub

    'Valida datos
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
        If Me.TxtTiempoOfertado.Text = "" Then
            Me.TxtTiempoReal.Focus()
            lblMsg.Text = "Por favor ingresar el tiempo ofertado del servicio.. !"
            Exit Function
        End If
        If Me.TxtTiempoReal.Text = "" Then
            Me.TxtTiempoReal.Focus()
            lblMsg.Text = "Por favor ingresar el tiempo real de duracion del servicio.. !"
            Exit Function
        End If
        If Me.TxtCodigoTrabajador.Text = "" Then
            Me.TxtCodigoTrabajador.Focus()
            Me.lblMsg.Text = "Por favor ingresa el usuario que solicita este servicio.. !"
            Exit Function
        End If
        If Me.cmbExperiencia.SelectedValue = "00" Then
            Me.cmbExperiencia.Focus()
            lblMsg.Text = "Por favor seleccione la opcion de experiencia de trabajo.. !"
            Exit Function
        End If
        If Me.cmbConformidad.SelectedValue = "00" Then
            Me.cmbConformidad.Focus()
            lblMsg.Text = "Por favor seleccione la opcion de conformidad de trabajo.. !"
            Exit Function
        End If
        fValida = True
    End Function


#End Region

    'Valida usuario
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

    'Carga la pagina
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Me.btnSolicitaAprobacion.Attributes.Add("onClick", "javascript: return fdesTipoAprobacion(TxtAprobacion,TxtDescripcion);")
    If Page.IsPostBack = False Then
            IniStyles()
            StrNumeroOpcion = "32"
            'strNumeroOrdenServicio = "0002-0000067104"
            strNumeroOrdenServicio = Request.QueryString("strNumeroOrdenServicio")
            sCargarOrdenServicio(strNumeroOrdenServicio)
            sfCargarCombos(StrNumeroOpcion)
            imgCancelar.Attributes.Add("Onclick", "javascript:fnc_Cerrar();")
            btnAdjuntos.Attributes.Add("onclick", "javascript:return fnc_ListarDocsAdjuntos()")
            imgGrabarFicha.Visible = False
            btnSolicitaAprobacion.Visible = False
    End If
  End Sub

    ' Boton Grabar
    Protected Sub imgGrabarFicha_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGrabarFicha.Click
        If fValida() = False Then Exit Sub
        If fActDataIns() = False Then Exit Sub
        Me.btnSolicitaAprobacion.Visible = True
    End Sub

    '****************************************************************************
    'Objetivo   : Registra los datos de la evaluacion de la ficha del proveedor.
    'Autor      : Darwin Ccorahua Livon
    'Creado     : 05/09/2011
    'Modificado : 00/00/0000
    '****************************************************************************
    Private Function fActDataIns() As Boolean
        Dim clsData As New clsFichaProv
        Dim dtDet As New DataTable
        Dim drFila As DataRow
        Dim strEstado As String = ""
        fActDataIns = True
        'T:=Todo costo
        'M:=Mano de Obra

        clsData.NumeroOrdenServicio = lblNroOrdeServicio.Text
        clsData.CodigoProveedor = Me.lblRuc.Text
        clsData.CodigoSolicitante = Me.TxtCodigoTrabajador.Text

        If Me.rdTiposervicio1.Checked = True Then
            clsData.TipoServicio = "T"
        Else
            clsData.TipoServicio = "S"
        End If

        clsData.FechaInicio = Right(Me.wdpFechaInicio.Text, 4) + Mid(Me.wdpFechaInicio.Text, 4, 2) + Mid(Me.wdpFechaInicio.Text, 1, 2)
        clsData.FechaFin = Right(Me.wdpFechaFin.Text, 4) + Mid(Me.wdpFechaFin.Text, 4, 2) + Mid(Me.wdpFechaFin.Text, 1, 2)
        clsData.TiempoReal = Integer.Parse(Trim(Me.TxtTiempoReal.Text))
        clsData.TiempoOfertado = Integer.Parse(Trim(Me.TxtTiempoOfertado.Text))
        clsData.Observaciones = Trim(Me.TxtObservaciones.Text)
        clsData.Experiencia = Me.cmbExperiencia.SelectedValue
        clsData.Conformidad = Me.cmbConformidad.SelectedValue
        clsData.Usuario = Session("@Usuario")
        clsData.ObtenerEsquema(dtDet)

        For i = 0 To wdgDetalle.Rows.Count - 1
            drFila = dtDet.NewRow()
            drFila.Item("vch_OrdenServicio") = lblNroOrdeServicio.Text
            drFila.Item("int_Secuencia") = CInt(wdgDetalle.Rows(i).Items(0).Value.ToString)
            drFila.Item("vch_Trabajo") = wdgDetalle.Rows(i).Items(3).Value.ToString
            drFila.Item("vch_Materiales") = wdgDetalle.Rows(i).Items(4).Value.ToString
            drFila.Item("vch_Calidad") = wdgDetalle.Rows(i).Items(5).Value.ToString
            drFila.Item("vch_Seguridad") = wdgDetalle.Rows(i).Items(6).Value.ToString
            If wdgDetalle.Rows(i).Items(3).Value.ToString <> "Seleccionar" And wdgDetalle.Rows(i).Items(4).Value.ToString <> "Seleccionar" And wdgDetalle.Rows(i).Items(4).Value.ToString <> "Seleccionar" And wdgDetalle.Rows(i).Items(5).Value.ToString <> "Seleccionar" And wdgDetalle.Rows(i).Items(6).Value.ToString <> "Seleccionar" Then
                strEstado = "OK"
            Else
                strEstado = "PEN"
            End If
            drFila.Item("chr_Estado") = strEstado
            dtDet.Rows.Add(drFila)
        Next i
        'CON:=Concluido todo el Servicio
        'PEN:=Servicio Parcial
        clsData.EstadoServicio = "ACT"
        If clsData.Insertar_FichaProveedor(dtDet) = True Then
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Datos actualizado con éxito"
        Else
            fActDataIns = False
            lblMsg.Text = clsData.clsError
        End If
        clsData = Nothing

    End Function

  Private Sub sCargarOrdenServicio(ByVal strNumeroOrdenServicio As String)
    '-----------------------------------------------
    'Objetivo   : Muestra los datos de la Orden de Servicio
    'Autor      : Darwin Ccorahua Livon
    'Creado     : 10/08/2011
    'Modificado : 00/00/0000
    '-----------------------------------------------

    lblMsg.Text = ""
    Dim lobjOrdenServicio As New clsFichaProv
    Dim objDT As New DataSet
    Dim xData1, xData2 As New DataTable
    lobjOrdenServicio.CodigoEmpresa = Session("@EMPRESA")
    lobjOrdenServicio.NumeroOrdenServicio = strNumeroOrdenServicio
    lobjOrdenServicio.MostrarOrdenServicio_Detalle(objDT)
    xData1 = objDT.Tables(0)
    xData2 = objDT.Tables(1)

    Me.wdpFechaInicio.Text = Now.ToString("dd/MM/yyyy")
    Me.wdpFechaFin.Text = Now.ToString("dd/MM/yyyy")

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
      Me.wdpFechaInicio.Text = .Item("dtm_fechaInicio").ToString
      Me.wdpFechaFin.Text = .Item("dtm_FechaFinal").ToString
      Me.TxtTiempoReal.Text = .Item("int_Tiemporeal").ToString

      Me.TxtTiempoOfertado.Text = .Item("TiempoOfertado").ToString

      Me.TxtObservaciones.Text = .Item("vch_Observaciones").ToString
      Me.cmbExperiencia.SelectedValue = .Item("vch_experiencia").ToString
      Me.cmbConformidad.SelectedValue = .Item("vch_Conformidad").ToString
      Select Case .Item("chr_estado").ToString
        Case "SOL"
          Me.imgGrabarFicha.Visible = False
          Me.btnSolicitaAprobacion.Visible = False
        Case "APR"
          Me.imgGrabarFicha.Visible = False
          Me.btnSolicitaAprobacion.Visible = False
        Case "ACT"
          Me.imgGrabarFicha.Visible = False
          Me.btnSolicitaAprobacion.Visible = True
        Case Else
          Me.imgGrabarFicha.Visible = True
          Me.btnSolicitaAprobacion.Visible = False
      End Select
      Me.lblEstado.Text = .Item("chr_estado").ToString
      Me.TxtTiempoOfertado.Text = .Item("TiempoOfertado").ToString
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

    Me.wdgDetalle.DataSource = xData2
    Me.wdgDetalle.DataBind()
    objDT = Nothing
    lobjOrdenServicio = Nothing

  End Sub

  Private Sub sfCargarCombos(ByVal strNumero As String)
    '-----------------------------------------------
    'Objetivo   : Muestra los opcines de los combos
    'Autor      : Darwin Ccorahua Livon
    'Creado     : 05/09/2011
    'Modificado : 00/00/0000
    '-----------------------------------------------
    Dim lobjOpcionesSeleccion As New clsFichaProv
    Dim objDT As New DataTable
    lobjOpcionesSeleccion.MostrarOpcionesSeleccion(objDT, strNumero)

    cmbTrabajo.EditorControl.DataSource = objDT
    cmbTrabajo.EditorControl.DataBind()

    cmbCalidad.EditorControl.DataSource = objDT
    cmbCalidad.EditorControl.DataBind()

    cmbNormasSeguridad.EditorControl.DataSource = objDT
    cmbNormasSeguridad.EditorControl.DataBind()

    cmbMateriales.EditorControl.DataSource = objDT
    cmbMateriales.EditorControl.DataBind()

    objDT = Nothing
    lobjOpcionesSeleccion = Nothing
  End Sub

  Protected Sub btnSolicitaAprobacion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSolicitaAprobacion.Click
    Dim objAprobacion As New clsFichaProv
    Dim ldtCorreos As DataTable
    Try
      Me.btnSolicitaAprobacion.Attributes.Add("onClick", "javascript:fdesTipoAprobacion(TxtAprobacion,TxtDescripcion);")
      If objAprobacion.fncSolicitarAprobacionOS(Session("@EMPRESA"), Me.TxtAprobacion.Text, lblNroOrdeServicio.Text, "", "", "PRO", Me.wdpFechaInicio.Text, "K", "", Session("@Usuario"), "", Session("@Usuario"), "", ldtCorreos) Then
        Me.lblMsg.Text = "ORDEN DE SERVICIO POR APROBAR PARA SU FINALIZACION"
      Else
        lblMsg.Text = "Seleccione un tipo de aprobacion por favor...!"
      End If
    Catch ex As Exception
      lblMsg.Text = "Ha ocurrido un error al Aprobar, comuniquese con Sistemas."
    End Try
  End Sub

    Protected Sub imgCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCancelar.Click
        ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script> history.back(1)</script>")
    End Sub
End Class
