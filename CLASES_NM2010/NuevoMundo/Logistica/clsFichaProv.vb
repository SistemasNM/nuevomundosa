Imports NM.AccesoDatos
Imports System.IO
Public Class clsFichaProv
#Region "-- Variables --"

  Dim mstrError As String
  Dim mstrOrdenServicio As String = ""
  Dim mstrCodigoEmpresa As String = ""
  Dim mstrCodigoProveedor As String = ""
  Dim mstrCodigoSolicitante As String = ""
  Dim mstrTipoServicio As String = ""
  Dim mstrFechaInicio As String = ""
  Dim mstrFechaFin As String = ""
  Dim mstrTiempoOfertado As String = ""
  Dim mstrTiempoReal As String = ""
  Dim mstrExperiencia As String = ""
  Dim mstrConformidad As String = ""
  Dim mstrObservaciones As String = ""
  Dim mstrUsuario As String = ""
  Dim mstrEstadoServicio As String = ""
  Dim mstrNumeroOrdenServicio As String = ""
    Dim mstrPonderadoTotal As Decimal
    Dim mstrResultado As String = ""
    Dim mstrEstado As String = ""
    Dim mstrItem As String = ""
    Dim mstrFlgObser As String = ""
#End Region

#Region "-- Propiedades --"

  Public ReadOnly Property clsError() As String
    Get
      Return mstrError
    End Get
  End Property

  Public Property CodigoEmpresa() As String
    Get
      CodigoEmpresa = mstrCodigoEmpresa
    End Get
    Set(ByVal sCad As String)
      mstrCodigoEmpresa = sCad
    End Set
  End Property

  Public Property NumeroOrdenServicio() As String
    Get
      NumeroOrdenServicio = mstrOrdenServicio
    End Get
    Set(ByVal sCad As String)
      mstrOrdenServicio = sCad
    End Set
  End Property

  Public Property CodigoProveedor() As String
    Get
      CodigoProveedor = mstrCodigoProveedor
    End Get
    Set(ByVal strCad As String)
      mstrCodigoProveedor = strCad
    End Set
  End Property

  Public Property CodigoSolicitante() As String
    Get
      CodigoSolicitante = mstrCodigoSolicitante
    End Get
    Set(ByVal strCad As String)
      mstrCodigoSolicitante = strCad
    End Set
  End Property

  Public Property TipoServicio() As String
    Get
      TipoServicio = mstrTipoServicio
    End Get
    Set(ByVal strCad As String)
      mstrTipoServicio = strCad
    End Set
  End Property

  Public Property FechaInicio() As String
    Get
      FechaInicio = mstrFechaInicio
    End Get
    Set(ByVal strCad As String)
      mstrFechaInicio = strCad
    End Set
  End Property

  Public Property FechaFin() As String
    Get
      FechaFin = mstrFechaFin
    End Get
    Set(ByVal strCad As String)
      mstrFechaFin = strCad
    End Set
  End Property

  Public Property TiempoOfertado() As String
    Get
      TiempoOfertado = mstrTiempoOfertado
    End Get
    Set(ByVal strCad As String)
      mstrTiempoOfertado = strCad
    End Set
  End Property

  Public Property TiempoReal() As String
    Get
      TiempoReal = mstrTiempoReal
    End Get
    Set(ByVal strCad As String)
      mstrTiempoReal = strCad
    End Set
  End Property

  Public Property Experiencia() As String
    Get
      Experiencia = mstrExperiencia
    End Get
    Set(ByVal strCad As String)
      mstrExperiencia = strCad
    End Set
  End Property

  Public Property Conformidad() As String
    Get
      Conformidad = mstrConformidad
    End Get
    Set(ByVal strCad As String)
      mstrConformidad = strCad
    End Set
  End Property

  Public Property Observaciones() As String
    Get
      Observaciones = mstrObservaciones
    End Get
    Set(ByVal strCad As String)
      mstrObservaciones = strCad
    End Set
  End Property

  Public Property Usuario() As String
    Get
      Usuario = mstrUsuario
    End Get
    Set(ByVal strCad As String)
      mstrUsuario = strCad
    End Set
  End Property

  Public Property EstadoServicio() As String
    Get
      EstadoServicio = mstrEstadoServicio
    End Get
    Set(ByVal strCad As String)
      mstrEstadoServicio = strCad
    End Set
    End Property
    Public Property FlgLevantarObservaciones() As String
        Get
            FlgLevantarObservaciones = mstrFlgObser
        End Get
        Set(value As String)
            mstrFlgObser = value
        End Set
    End Property

#End Region

#Region "-- Propiedades --"
  Public Function MostrarOrdenServicio(ByRef pDT As DataSet) As DataSet
    '*******************************************************************************************
    'Creado por:	  Darwin Ccorahua Livon
    'Fecha     :      21-07-2011
    'Proposito :      Muestra el detalle de la orden de Servicio a Calificar por el responsable.
    '*******************************************************************************************
    Dim blnRpta As Boolean
    Dim Conexion As AccesoDatosSQLServer
    Dim objParametro() As Object = {"chr_Empresa", mstrCodigoEmpresa, _
                                    "var_OrdenCompraServicio", NumeroOrdenServicio}
    Try
      mstrError = ""
      blnRpta = True
      Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      pDT = Conexion.ObtenerDataSet("usp_LOG_OCOS_Buscar", objParametro)
    Catch ex As Exception
      blnRpta = False
      mstrError = ex.Message
    Finally
      Conexion = Nothing
    End Try
    Return pDT
  End Function

  Public Function MostrarOrdenServicio_Detalle(ByRef objDT As DataSet) As DataSet
    '*******************************************************************************************
    'Creado por:	  Darwin Ccorahua Livon
    'Fecha     :      21-07-2011
    'Proposito :      Muestra el detalle de la orden de Servicio a Calificar por el responsable.
    '*******************************************************************************************
    Dim blnRpta As Boolean
    Dim Conexion As AccesoDatosSQLServer
    Dim objParametro() As Object = {"chr_Empresa", mstrCodigoEmpresa, _
                                    "var_OrdenCompraServicio", NumeroOrdenServicio}
    Try
      mstrError = ""
      blnRpta = True
      Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      objDT = Conexion.ObtenerDataSet("USP_LOG_DETALLEOS", objParametro)
    Catch ex As Exception
      blnRpta = False
      mstrError = ex.Message
    Finally
      Conexion = Nothing
    End Try
    Return objDT
  End Function
    Public Function MostrarOrdenServicio_Detalle_Conformidad(ByRef objDT As DataSet) As DataSet
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Muestra el detalle de la orden de Servicio a Calificar por el responsable.
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"chr_Empresa", mstrCodigoEmpresa, _
                                        "var_OrdenCompraServicio", NumeroOrdenServicio, _
                                        "vch_Item", mstrItem}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            objDT = Conexion.ObtenerDataSet("USP_LOG_DETALLEOS_CONFORMIDAD", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return objDT
    End Function
    Public Function MostrarOrdenServicio_Por_Conformidad(ByRef objDt As DataSet) As DataSet
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Muestra el detalle de la orden de Servicio a Calificar por el responsable.
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"chr_Empresa", mstrCodigoEmpresa, _
                                        "vch_NumConformidad", Conformidad}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            objDT = Conexion.ObtenerDataSet("USP_LOG_DETALLEOS_CONFORMIDAD_APROBACION", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return objDT
    End Function
    Public Function MostrarOrdenesServicio(ByRef pDT As DataTable) As DataTable

        '*******************************************************************************************
        'Creado por: Darwin Ccorahua Livon
        'Fecha     : 21-07-2011
        'Proposito : Muestra el detalle de la orden de Servicio a Calificar por el responsable.
        '*******************************************************************************************
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"chr_Empresa", mstrCodigoEmpresa, _
                                        "var_CodigoProveedor", mstrCodigoProveedor, _
                                        "var_FechaIni", mstrFechaInicio, _
                                        "var_FechaFin", mstrFechaFin, _
                                        "chr_estado", mstrEstadoServicio, _
                                        "vch_NumeroOrden", mstrOrdenServicio}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pDT = Conexion.ObtenerDataTable("USP_LOG_LISTAR_OS", objParametro)
        Catch ex As Exception
            Throw ex
        Finally
            Conexion = Nothing
        End Try
        Return pDT
    End Function

  Public Function MostrarOpcionesSeleccion(ByRef objDTable As DataTable, ByRef strNumeroOpcion As String) As DataTable
    '*******************************************************************************************
    'Creado por:	  Darwin Ccorahua Livon
    'Fecha     :      21-07-2011
    'Proposito :      Muestra el detalle de la orden de Servicio a Calificar por el responsable.
    '*******************************************************************************************
    Dim blnRpta As Boolean
    Dim Conexion As AccesoDatosSQLServer
    Dim objParametro() As Object = {"chr_CodigoTabla", strNumeroOpcion}
    Try
      mstrError = ""
      Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
      objDTable = Conexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar ", objParametro)
      blnRpta = True
    Catch ex As Exception
      blnRpta = False
      mstrError = ex.Message
    Finally
      Conexion = Nothing
    End Try
    Return objDTable

  End Function

  Public Function Insertar_FichaProveedor(ByVal pdtDetalle As DataTable) As Boolean
    '*******************************************************************************************
    'Creado por:	  Darwin Ccorahua Livon
    'Fecha     :      05-09-2011
    'Proposito :      Permite registrar la ficha de evaluacion del Proveedor
    '*******************************************************************************************

    Dim blnRpta As Boolean, clsUtilitario As New NM_General.Util
    Dim Conexion As AccesoDatosSQLServer
    Dim objParametro() As Object = {"pvch_OrdenServicio", mstrOrdenServicio, _
                                    "pvch_CodigoProveedor", mstrCodigoProveedor, _
                                    "pvch_CodigoSolicitante", mstrCodigoSolicitante, _
                                    "pchr_TipoServicio", mstrTipoServicio, _
                                    "pdtm_FechaInicio", mstrFechaInicio, _
                                    "pdtm_FechaFinal", mstrFechaFin, _
                                    "pint_TiempoReal", mstrTiempoReal, _
                                    "pint_TiempoOfertado", mstrTiempoOfertado, _
                                    "pvch_Experiencia", mstrExperiencia, _
                                    "pvch_Conformidad", mstrConformidad, _
                                    "pvch_Observaciones", mstrObservaciones, _
                                    "pvch_UsuarioCreacion", mstrUsuario, _
                                    "pchr_EstadoServicio", mstrEstadoServicio, _
                                    "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
    Try
      mstrError = ""
      blnRpta = True
      Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Conexion.EjecutarComando("USP_LOG_GESTIONSEOT_GUARDAR", objParametro)
    Catch ex As Exception
      blnRpta = False
      mstrError = ex.Message
    Finally
      Conexion = Nothing
    End Try
    Return blnRpta
  End Function

  Public Function ObtenerEsquema(ByRef pdtDetalle As DataTable) As Boolean
    '*******************************************************************************************
    'Creado por:	  Darwin Ccorahua
    'Fecha     :      11-09-2011
    'Proposito :      Devuelve el esquema de servicio para generar un xml
    '*******************************************************************************************
    Dim blnRpta As Boolean
    Dim Conexion As AccesoDatosSQLServer
    Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    pdtDetalle = Conexion.ObtenerDataTable("USP_LOG_ESQUEMASERVICIO")
    Try
      blnRpta = True
    Catch ex As Exception
      blnRpta = False
    Finally
    End Try
    Return blnRpta
  End Function

  Public Function fncSolicitarAprobacionOS(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                         ByVal strNumeroDocumento As String, ByVal strFecha As String, _
                                         ByVal strObservacion As String, ByVal strEstadoSoli As String, _
                                         ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, _
                                         ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, _
                                         ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, _
                                         ByVal strFechaModi As String, Optional ByRef pdtCorreos As DataTable = Nothing) As Boolean
    Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
    Dim lblnOk As Boolean
    Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                              "CO_APRO", strCodAprobacion, _
                              "NU_DOCU", strNumeroDocumento, _
                              "FE_DOCU", strFecha, _
                              "OB_0001", strObservacion, _
                              "ST_SOLI", strEstadoSoli, _
                              "FE_STAT_SOLI", strFechaSolicitud, _
                              "TI_AUXI_EMPR", strTipoAuxiliar, _
                              "CO_AUXI_EMPR", strCodigoAuxiliar, _
                              "CO_USUA_CREA", strUsuario, _
                              "FE_USUA_CREA", strFechaCreacion, _
                              "CO_USUA_MODI", strUsuarioModi, _
                              "FE_USUA_MODI", strFechaModi}
    Try
      lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
      pdtCorreos = lobjCon.ObtenerDataTable("USP_SEG_SOLICITAAPROBACION_OS", Params)
      lblnOk = True
    Catch ex As Exception
      lblnOk = False
    Finally
      lobjCon = Nothing
    End Try
    Return lblnOk
  End Function

    Public Function fncSolicitarAprobacionOS_Conformidad(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                         ByVal strNumeroDocumento As String, ByVal strFecha As String, _
                                         ByVal strObservacion As String, ByVal strEstadoSoli As String, _
                                         ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, _
                                         ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, _
                                         ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, _
                                         ByVal strFechaModi As String, ByVal strNroConformidad As String, _
                                         ByVal strItem As String, Optional ByRef pdtCorreos As DataTable = Nothing) As Boolean
        '*******************************************************************************************
        'Creado por: David Gamarra Paredes
        'Fecha     : 17-05-2017
        'Proposito : registra solicitud por aprobar
        '*******************************************************************************************
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lblnOk As Boolean
        Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                                  "CO_APRO", strCodAprobacion, _
                                  "NU_DOCU", strNumeroDocumento, _
                                  "FE_DOCU", strFecha, _
                                  "OB_0001", strObservacion, _
                                  "ST_SOLI", strEstadoSoli, _
                                  "FE_STAT_SOLI", strFechaSolicitud, _
                                  "TI_AUXI_EMPR", strTipoAuxiliar, _
                                  "CO_AUXI_EMPR", strCodigoAuxiliar, _
                                  "CO_USUA_CREA", strUsuario, _
                                  "FE_USUA_CREA", strFechaCreacion, _
                                  "CO_USUA_MODI", strUsuarioModi, _
                                  "FE_USUA_MODI", strFechaModi, _
                                  "NU_CONFOR", strNroConformidad, _
                                  "CO_ITEM", strItem}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            pdtCorreos = lobjCon.ObtenerDataTable("USP_SEG_SOLICITAAPROBACION_OS_CONFORMIDAD", Params)
            lblnOk = True
        Catch ex As Exception
            lblnOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return lblnOk
    End Function
  ' --- Aprobacion/Anulacion de Pedidos
    Public Function fncOSCambiaEstado(ByVal strTipo As String, ByVal strNumeroOS As String, _
                                ByVal strUsuario As String) As DataTable
        Dim strActualizarOS As String = ""
        Dim pdtCorreos As DataTable = Nothing
        Try
            ' Anula o culmina pedido 
            If strTipo = "1" Or strTipo = "3" Then
                strActualizarOS = "USP_SEG_EVALUACIONOS_ANULAR"
            End If
            If strTipo = "2" Then
                strActualizarOS = "USP_SEG_EVALUACIONOS_APROBAR"
            End If
            Dim objParametros As Object() = {"chr_tipo", strTipo,
                                             "vch_OrdenServicio", strNumeroOS,
                                             "CO_USUA_MODI", strUsuario}
            pdtCorreos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis).ObtenerDataTable(strActualizarOS, objParametros)
        Catch ex As Exception
            Throw
        End Try
        Return pdtCorreos
    End Function
    Public Function fncOSCambiaEstadoConformidad(ByVal strTipo As String, ByVal strNroConformidad As String, _
                              ByVal strUsuario As String) As DataTable
        Dim strActualizarOS As String = ""
        Dim pdtCorreos As DataTable = Nothing
        Try
            ' Anula o culmina pedido 
            If strTipo = "1" Or strTipo = "3" Then
                strActualizarOS = "USP_SEG_EVALUACIONOS_ANULAR_CONFORMIDAD"
            End If
            If strTipo = "2" Then
                strActualizarOS = "USP_SEG_EVALUACIONOS_APROBAR_CONFORMIDAD"
            End If
            Dim objParametros As Object() = {"chr_tipo", strTipo,
                                             "vch_NroConformidad", strNroConformidad,
                                             "CO_USUA_MODI", strUsuario}
            pdtCorreos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis).ObtenerDataTable(strActualizarOS, objParametros)
        Catch ex As Exception
            Throw
        End Try
        Return pdtCorreos
    End Function
#End Region

#Region "Propiedades Conformidad"
    Public Property PonderadoTotal() As Decimal
        Get
            PonderadoTotal = mstrPonderadoTotal
        End Get
        Set(value As Decimal)
            mstrPonderadoTotal = value
        End Set
    End Property
    Public Property Resultado() As String
        Get
            Resultado = mstrResultado
        End Get
        Set(value As String)
            mstrResultado = value
        End Set
    End Property
    Public Property Estado() As String
        Get
            Estado = mstrEstado
        End Get
        Set(value As String)
            mstrEstado = value
        End Set
    End Property
    Public Property Item() As String
        Get
            Item = mstrItem
        End Get
        Set(value As String)
            mstrItem = value
        End Set
    End Property
#End Region
#Region "Conformidad"

    Public Function ObtenerListadoOrdenesServicio(ByRef pDT As DataTable) As DataTable

        '*******************************************************************************************
        'Creado por: Luis Alanoca J.
        'Fecha     : 07-04-2017
        'Proposito : Muestra el listado de las ordenes de servicio
        '*******************************************************************************************
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"chr_Empresa", mstrCodigoEmpresa, _
                                        "var_CodigoProveedor", mstrCodigoProveedor, _
                                        "var_FechaIni", mstrFechaInicio, _
                                        "var_FechaFin", mstrFechaFin, _
                                        "chr_estado", mstrEstadoServicio, _
                                        "vch_NumeroOrden", mstrOrdenServicio}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pDT = Conexion.ObtenerDataTable("USP_LOG_LISTAR_OS_CONFORMIDADES", objParametro)
        Catch ex As Exception
            Throw ex
        Finally
            Conexion = Nothing
        End Try
        Return pDT
    End Function
    Public Function Insertar_FichaProveedor_Conformidad(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Permite registrar la ficha de evaluacion del Proveedor
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim clsUtilitario As New NM_General.Util
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_OrdenServicio", mstrOrdenServicio, _
                                        "vch_CodigoProveedor", mstrCodigoProveedor, _
                                        "vch_CodigoSolicitante", mstrCodigoSolicitante, _
                                        "vch_Item", mstrItem, _
                                        "chr_TipoServicio", mstrTipoServicio, _
                                        "chr_Estado", mstrEstado, _
                                        "chr_EstadoServicio", mstrEstadoServicio, _
                                        "vch_Resultado", mstrResultado, _
                                        "chr_FlgLevObser", mstrFlgObser, _
                                        "num_PonderadoTotal", mstrPonderadoTotal, _
                                        "vch_Observaciones", mstrObservaciones, _
                                        "pdtm_FechaInicio", mstrFechaInicio, _
                                        "pdtm_FechaFinal", mstrFechaFin, _
                                        "vch_UsuarioCreacion", mstrUsuario, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Conexion.ObtenerDataTable("USP_LOG_GESTIONSEOTCAB_GUARDAR_CONFORMIDADES", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ActualizarEstadoConformidad(ByVal mstrOrdenServicio As String, ByVal mstrItem As String, ByVal mstrTxtObser As String, ByVal mstrEstado As String, ByVal mstrUsuario As String) As Boolean
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Actualiza las observaciones dee la evaluacion de la OS
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim clsUtilitario As New NM_General.Util
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_OrdenServicio", mstrOrdenServicio, _
                                        "vch_Item", mstrItem, _
                                        "vch_Observaciones", mstrTxtObser, _
                                        "chr_Estado", mstrEstado, _
                                        "vch_UsuarioModificacion", mstrUsuario
                                       }
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Conexion.ObtenerDataTable("USP_LOG_GESTIONSEOTCAB_ACTUALIZAR", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
    Public Function fncObtenerConformidad(ByVal NroOrden As String, ByVal strItem As String) As String
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Obtiene datos de la conformidad
        '*******************************************************************************************
        Dim OrdenServicio As String

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_OrdenServicio", NroOrden, _
                                        "vch_Item", strItem}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            OrdenServicio = Conexion.ObtenerDataTable("USP_LOG_OBTENER_DATOS_CONFORMIDAD", objParametro).Rows(0).Item(0).ToString
        Catch ex As Exception
            Throw ex
        Finally
            Conexion = Nothing
        End Try
        Return OrdenServicio
    End Function
    Public Function ActualizarObservaciones(ByVal strNroOrden As String, ByVal strItem As String, ByVal strUsuario As String) As Boolean
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Actualiza las observaciones de la conformidad para que pueda enviar solicitud por aprobar.
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim clsUtilitario As New NM_General.Util
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_OrdenServicio", strNroOrden, _
                                        "vch_Item", strItem, _
                                        "vch_UsuarioModificacion", strUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Conexion.EjecutarComando("USP_LOG_ACTUALIZAR_FLGOBSER_CONFORMIDAD", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
    Public Function CargarCorreoProveedores(ByVal strNroOrden As String, ByVal strUsuario As String) As DataTable
        '*******************************************************************************************
        'Creado por:	  David Gamarra Paredes
        'Fecha     :      15-05-2017
        'Proposito :      Obtiene los correos de los proveedores para enviar el resultado de la conformidad.
        '*******************************************************************************************
        Dim pdt As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_OrdenServicio", strNroOrden, _
                                        "vch_Usuario", strUsuario}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdt = Conexion.ObtenerDataTable("USP_SEG_LISTA_CORREO_PROVEEDOR_EVALUACION", objParametro)
        Catch ex As Exception
            Throw ex
        Finally
            Conexion = Nothing
        End Try
        Return pDT
    End Function
    Public Function ValidarConformidad(ByVal strNroOrden As String, ByVal strItem As String) As DataTable
        Dim pdt As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_OrdenServicio", strNroOrden, _
                                        "vch_Item", strItem}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdt = Conexion.ObtenerDataTable("UPS_VALIDAR_CONFORMIDAD_REALIZADA", objParametro)
        Catch ex As Exception
            Throw ex
        Finally
            Conexion = Nothing
        End Try
        Return pdt
    End Function
    'CONFORMIDADES VERSION_2 DAVID 25/02/2019 - INI
    Public Function ListarItemorMonto(ByVal strOrden As String, ByVal strItem As String) As DataTable
        Dim pdt As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"VCH_ORDEN", strOrden, _
                                        "VCH_ITEM", strItem}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdt = Conexion.ObtenerDataTable("USP_LISTAR_ITEM_POR_MONTO", objParametro)
        Catch ex As Exception
            Throw ex
        Finally
            Conexion = Nothing
        End Try
        Return pdt
    End Function
    Public Function ActualizarMontoDetallePorMonto(ByVal strOS As String, ByVal strItem As String, ByVal strSecu As String, ByVal numMonto As Double) As DataTable
        Dim clsUtilitario As New NM_General.Util
        Dim Conexion As AccesoDatosSQLServer
        Dim resul As String
        Dim dt As DataTable

        Dim objParametro() As Object = {"vch_OrdenServicio", strOS, _
                                        "vch_Item", strItem, _
                                        "vch_Secu", strSecu, _
                                        "num_Monto", numMonto}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dt = Conexion.ObtenerDataTable("USP_ACTUALIZAR_MONTO_POR_ITEM", objParametro)
        Catch ex As Exception
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return dt
    End Function
    'CONFORMIDADES VERSION_2 DAVID 25/02/2019 - FIN
#End Region
End Class
