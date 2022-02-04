Imports NM.AccesoDatos
Namespace NM.Tintoreria
    Public Class RegistroProduccionTablet
        Implements IDisposable
#Region "Propiedades generales"
        Private mstrCodigoMaquina As String
        Private mStrCodigoOperario As String
        Private mStrCodigoSupervisor As String
        Private mStrUsuarioCreacion As String
        Private mDateFechaCreacion As String
        Private mStrComentario As String

        Public Property Codigo_Maquina() As String
            Get
                Return mstrCodigoMaquina
            End Get
            Set(ByVal Value As String)
                mstrCodigoMaquina = Value
            End Set
        End Property
        Public Property Codigo_Operario() As String
            Get
                Return mStrCodigoOperario
            End Get
            Set(ByVal Value As String)
                mStrCodigoOperario = Value
            End Set
        End Property

        Public Property Codigo_Supervisor() As String
            Get
                Return mStrCodigoSupervisor
            End Get
            Set(ByVal Value As String)
                mStrCodigoSupervisor = Value
            End Set
        End Property
        Public Property Usuario_Creacion() As String
            Get
                Return mStrUsuarioCreacion
            End Get
            Set(ByVal Value As String)
                mStrUsuarioCreacion = Value
            End Set
        End Property
        Public Property FechaCreacion() As String
            Get
                Return mDateFechaCreacion
            End Get
            Set(ByVal Value As String)
                mDateFechaCreacion = Value
            End Set
        End Property
        Public Property Comentario() As String
            Get
                Return mStrComentario
            End Get
            Set(ByVal Value As String)
                mStrComentario = Value
            End Set
        End Property
#End Region
#Region "Propiedades para registro produccion"
        Private mstrFicha As String
        Private mstrOrdenProduccion As String

        Private mstrCodigoOperacion As String
        Private mstrCodigoArticuloLargo As String
        Private mstrCodigoArticuloOfisis As String
        Private mintSecuencial As Integer
        '***
        Private mdblVelocidad As Double
        Private mdblPases As Double
        Private mStrCodigoReceta As String
        Private mintRevisionReceta As Integer
        Private mdblMetrosEntrada As Double
        Private mdblMetrosSalida As Double
        Private mStrHoraInicio As String
        Private mStrHoraFin As String
        Private mDateFechaInicio As String
        Private mDateFechaFin As String
        Private mintSituacion As Int16
        '***
        Private mstrConexion As String
        Private mobjConexion As AccesoDatosSQLServer
        Private mstrMotivoReprocesoCodigo As String
        Private mDateFechaInicioReal As String
        Private mDateFechaFinReal As String
        '***
        Private mStrCodigoReproceso As String
        Private mStrTipoRegistroSecuencial As String
        Private mStrHoraInicioReal As String
        Private mdblVelocidadReal As Double
        Public Property MotivoReprocesoCodigo() As String
            Get
                MotivoReprocesoCodigo = mstrMotivoReprocesoCodigo
            End Get
            Set(ByVal Value As String)
                mstrMotivoReprocesoCodigo = Value
            End Set
        End Property

        Public Property Codigo_Ficha() As String
            Get
                Return mstrFicha
            End Get
            Set(ByVal Value As String)
                mstrFicha = Value
            End Set
        End Property

        Public Property Secuencial() As Integer
            Get
                Return mintSecuencial
            End Get
            Set(ByVal Value As Integer)
                mintSecuencial = Value
            End Set
        End Property

        Public Property Orden_Produccion() As String
            Get
                Return mstrOrdenProduccion
            End Get
            Set(ByVal Value As String)
                mstrOrdenProduccion = Value
            End Set
        End Property

        Public Property Codigo_Articulo_Largo() As String
            Get
                Return mstrCodigoArticuloLargo
            End Get
            Set(ByVal Value As String)
                mstrCodigoArticuloLargo = Value
            End Set
        End Property

        Public Property Codigo_Operacion() As String
            Get
                Return mstrCodigoOperacion
            End Get
            Set(ByVal Value As String)
                mstrCodigoOperacion = Value
            End Set
        End Property

        Public Property Velocidad() As Double
            Get
                Return mdblVelocidad
            End Get
            Set(ByVal Value As Double)
                mdblVelocidad = Value
            End Set
        End Property

        Public Property Pases() As Double
            Get
                Return mdblPases
            End Get
            Set(ByVal Value As Double)
                mdblPases = Value
            End Set
        End Property

        Public Property Codigo_Receta() As String
            Get
                Return mStrCodigoReceta
            End Get
            Set(ByVal Value As String)
                mStrCodigoReceta = Value
            End Set
        End Property

        Public Property Revision_Receta() As Integer
            Get
                Return mintRevisionReceta
            End Get
            Set(ByVal Value As Integer)
                mintRevisionReceta = Value
            End Set
        End Property

        Public Property Metros_Entrada() As Double
            Get
                Return mdblMetrosEntrada
            End Get
            Set(ByVal Value As Double)
                mdblMetrosEntrada = Value
            End Set
        End Property

        Public Property Metros_Salida() As Double
            Get
                Return mdblMetrosSalida
            End Get
            Set(ByVal Value As Double)
                mdblMetrosSalida = Value
            End Set
        End Property

        Public Property Hora_Inicio() As String
            Get
                Return mStrHoraInicio
            End Get
            Set(ByVal Value As String)
                mStrHoraInicio = Value
            End Set
        End Property

        Public Property Hora_Fin() As String
            Get
                Return mStrHoraFin
            End Get
            Set(ByVal Value As String)
                mStrHoraFin = Value
            End Set
        End Property

        Public Property Fecha_Inicio() As String
            Get
                Return mDateFechaInicio
            End Get
            Set(ByVal Value As String)
                mDateFechaInicio = Value
            End Set
        End Property

        Public Property Fecha_Fin() As String
            Get
                Return mDateFechaFin
            End Get
            Set(ByVal Value As String)
                mDateFechaFin = Value
            End Set
        End Property

        Public Property Codigo_Articulo_Ofisis() As String
            Get
                Return mstrCodigoArticuloOfisis
            End Get
            Set(ByVal Value As String)
                mstrCodigoArticuloOfisis = Value
            End Set
        End Property

        Public Property Situacion() As Int16
            Get
                Return mintSituacion
            End Get
            Set(ByVal Value As Int16)
                mintSituacion = Value
            End Set
        End Property

        Public Property Fecha_Inicio_Real() As String
            Get
                Return mDateFechaInicioReal
            End Get
            Set(ByVal Value As String)
                mDateFechaInicioReal = Value
            End Set
        End Property

        Public Property Fecha_Fin_Real() As String
            Get
                Return mDateFechaFinReal
            End Get
            Set(ByVal Value As String)
                mDateFechaFinReal = Value
            End Set
        End Property

        Public Property TipoRegistroSecuencial() As String
            Get
                TipoRegistroSecuencial = mStrTipoRegistroSecuencial
            End Get
            Set(ByVal Value As String)
                mStrTipoRegistroSecuencial = Value
            End Set
        End Property

        Public Property Hora_Inicio_Real() As String
            Get
                Return mStrHoraInicioReal
            End Get
            Set(ByVal Value As String)
                mStrHoraInicioReal = Value
            End Set
        End Property

        Public Property VelocidadReal() As Double
            Get
                Return mdblVelocidadReal
            End Get
            Set(ByVal Value As Double)
                mdblVelocidadReal = Value
            End Set
        End Property
#End Region
#Region "Propiedades para paro produccion"
        Private mStrCodigoEtapa As String
        Private mStrTipoParoProduccion As String
        Private mStrCodigoParo As String
        Public Property Codigo_Etapa() As String
            Get
                Return mStrCodigoEtapa
            End Get
            Set(ByVal Value As String)
                mStrCodigoEtapa = Value
            End Set
        End Property

        Public Property Tipo_Paro_Produccion() As String
            Get
                Return mStrTipoParoProduccion
            End Get
            Set(ByVal Value As String)
                mStrTipoParoProduccion = Value
            End Set
        End Property

        Public Property Codigo_Paro() As String
            Get
                Return mStrCodigoParo
            End Get
            Set(ByVal Value As String)
                mStrCodigoParo = Value
            End Set
        End Property
#End Region
#Region "Declaracion de Variables Miembro"
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region
#Region "Definicion de Constructores"
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region
#Region "Definicion de Metodos"
        Public Function ObtenerListadoMaquinasActivasTablet(Optional ByVal strCodigoMaquina As String = "") As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoMaquina", strCodigoMaquina}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_LISTADO_MAQUINAS_ACTIVAS_TABLET", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosPersonalRegProdTablet(ByVal strCodigoOperario As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoPersonal", strCodigoOperario}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_PERSONAL_REG_PROD_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosFichaTablet(ByVal strCodigoFicha As String, ByVal strCodigoMaquina As String) As DataSet
            Dim dtblDatos As DataSet
            Dim objParametros As Object() = {"pvch_CodigoFicha", strCodigoFicha,
                                             "pvch_CodigoMaquina", strCodigoMaquina}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataSet("USP_TIN_OBTENER_DATOS_FICHA_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerListadoRegistroProduccionTablet(ByVal strCodigoMaquina As String, ByVal dtmFechaRegistro As DateTime, ByVal strTurnoRegistroProduccion As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoMaquina", strCodigoMaquina,
                                             "pdtm_fechaRegistro", dtmFechaRegistro,
                                             "pvch_TurnoRegistroProduccion", strTurnoRegistroProduccion}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_LISTADO_REGISTRO_PRODUCCION_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosArticuloRutaTablet(ByVal strCodigoArticulo30 As String, ByVal strSecuencial As Integer) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvar_CodigoArticulo30", strCodigoArticulo30,
                                             "pint_Secuencial", strSecuencial}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_ARTICULO_RUTA_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerMetrosEntradaFicha(ByVal pstrFicha As String) As String
            Dim strMetrosEntrada As String = ""
            Try
                Dim objParametros() As Object = {"pvch_codigoficha", pstrFicha}
                strMetrosEntrada = m_sqlDtAccTintoreria.ObtenerValor("usp_tin_regprod_metros_entrada", objParametros).ToString
                Return strMetrosEntrada
            Catch ex As SqlClient.SqlException
                strMetrosEntrada = ""
                Throw ex
            End Try
            Return strMetrosEntrada
        End Function
        Public Function InsertarRegistroProduccionTablet(ByVal pstrUsuario As String, ByVal objRegistroProduccionTablet As RegistroProduccionTablet) As String
            Dim dtFicha As DataTable = New DataTable
            Dim lintRetorno As String = "0" '0=FALLO; 1=INSERT; 2=UPD; 3=DEL
            Dim lstrAccion As String = "", strNombreStoreProcedure As String = "USP_TIN_INSERTAR_REGISTRO_PRODUCCION_TABLET"

            Try
                If MotivoReprocesoCodigo Is Nothing Then MotivoReprocesoCodigo = ""
                Dim objParametros() As Object = {
                "CODIGO_FICHA", objRegistroProduccionTablet.Codigo_Ficha.Trim.ToString, _
                "SECUENCIAL", objRegistroProduccionTablet.Secuencial.ToString.Trim.ToString, _
                "CODIGO_ORDEN", objRegistroProduccionTablet.Orden_Produccion.Trim.ToString, _
                "CODIGO_ARTICULO", objRegistroProduccionTablet.Codigo_Articulo_Largo.Trim.ToString, _
                "CODIGO_MAQUINA", objRegistroProduccionTablet.Codigo_Maquina.Trim.ToString, _
                "CODIGO_OPERACION", objRegistroProduccionTablet.Codigo_Operacion.Trim.ToString, _
                "VELOCIDAD", objRegistroProduccionTablet.Velocidad.ToString, _
                "PASES", objRegistroProduccionTablet.Pases.ToString, _
                "CODIGO_RECETA", objRegistroProduccionTablet.Codigo_Receta.Trim.ToString, _
                "REVISION_RECETA", objRegistroProduccionTablet.Revision_Receta.ToString.ToString, _
                "METROS_ENTRADA", objRegistroProduccionTablet.Metros_Entrada.ToString.ToString, _
                "METROS_SALIDA", objRegistroProduccionTablet.Metros_Salida.ToString.ToString, _
                "HORA_INICIO", objRegistroProduccionTablet.Hora_Inicio.Trim.ToString, _
                "HORA_FIN", objRegistroProduccionTablet.Hora_Fin.Trim.ToString, _
                "FECHA_INICIO", objRegistroProduccionTablet.Fecha_Inicio.ToString, _
                "FECHA_FIN", objRegistroProduccionTablet.Fecha_Fin.ToString, _
                "CODIGO_OPERARIO", objRegistroProduccionTablet.Codigo_Operario.Trim.ToString, _
                "CODIGO_SUPERVISOR", objRegistroProduccionTablet.Codigo_Supervisor.Trim.ToString, _
                "USUARIO_CREACION", pstrUsuario, _
                "MOTIVO_REPROCESO", objRegistroProduccionTablet.MotivoReprocesoCodigo.Trim.ToString, _
                "SITUACION", objRegistroProduccionTablet.mintSituacion, _
                "COMENTARIO", objRegistroProduccionTablet.Comentario.ToString, _
                "FECHA_INICIO_REAL", objRegistroProduccionTablet.Fecha_Inicio_Real.ToString, _
                "FECHA_FIN_REAL", objRegistroProduccionTablet.Fecha_Fin_Real
                }

                'Si es registro produccion parcial
                If (objRegistroProduccionTablet.TipoRegistroSecuencial = "PARCIAL") Then
                    strNombreStoreProcedure = "USP_TIN_INSERTAR_REGISTRO_PRODUCCION_TABLET_PARCIAL"
                End If


                dtFicha = m_sqlDtAccTintoreria.ObtenerDataTable(strNombreStoreProcedure, objParametros)

                If dtFicha.Rows.Count > 0 Then
                    lstrAccion = CType(dtFicha.Rows(0).Item("ACCION"), String)
                End If

                If lstrAccion = "INS" Then
                    lintRetorno = "1"
                ElseIf lstrAccion = "UPD" Then
                    lintRetorno = "2"
                End If
                Return lintRetorno
            Catch ex As SqlClient.SqlException
                Throw ex
            Finally
            End Try
        End Function
        Public Function InsertarParoProduccionTablet(ByVal objRegistroproduccion As RegistroProduccionTablet) As String
            Dim dtParo As DataTable = New DataTable
            Dim lintRetorno As String = "0", lstrAccion As String = "", strNombreStoreProcedure As String = "USP_TIN_INSERTAR_PARO_PRODUCCION_TABLET"
            Try
                Dim objParametros() As Object = {"codigo_Maquina", objRegistroproduccion.Codigo_Maquina.Trim, "codigo_etapa", objRegistroproduccion.Codigo_Etapa.Trim, _
                                                "codigo_tipoparoproduccion", objRegistroproduccion.Tipo_Paro_Produccion.Trim, "codigo_paro", objRegistroproduccion.Codigo_Paro.Trim, _
                                                "fecha_inicio", objRegistroproduccion.Fecha_Inicio.Trim, "fecha_fin", objRegistroproduccion.Fecha_Fin.Trim,
                                                "hora_Inicio", objRegistroproduccion.Hora_Inicio.Trim, "hora_fin", objRegistroproduccion.Hora_Fin.Trim,
                                                "codigo_operario", objRegistroproduccion.Codigo_Operario.Trim, "codigo_supervisor", objRegistroproduccion.Codigo_Supervisor.Trim,
                                                 "usuario_creacion", objRegistroproduccion.Usuario_Creacion.Trim, "fecha_creacion", objRegistroproduccion.FechaCreacion.Trim,
                                                 "comentario", objRegistroproduccion.Comentario.Trim, "fecha_inicio_real", objRegistroproduccion.Fecha_Inicio_Real.Trim,
                                                 "fecha_fin_real", objRegistroproduccion.Fecha_Fin_Real.Trim}

                If (objRegistroproduccion.TipoRegistroSecuencial = "PARCIAL") Then
                    strNombreStoreProcedure = "USP_TIN_INSERTAR_PARO_PRODUCCION_PARCIAL_TABLET"
                End If
                dtParo = m_sqlDtAccTintoreria.ObtenerDataTable(strNombreStoreProcedure, objParametros)

                If Not dtParo Is Nothing Then
                    lstrAccion = CType(dtParo.Rows(0)("ACCION"), String)
                End If
                If lstrAccion = "INS" Then
                    lintRetorno = "1"
                ElseIf lstrAccion = "UPD" Then
                    lintRetorno = "2"
                End If
                Return lintRetorno
            Catch ex As SqlClient.SqlException
                Throw ex
            Finally
                dtParo = Nothing
            End Try
        End Function
        Public Function ObtenerMaestroParosProduccionTablet(ByVal strCodigoParo As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_codigoParo", strCodigoParo}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_MAESTRO_PAROS_PRODUCCION_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerCantidadLoteFichaTablet(ByVal strCodigoFicha As String) As String
            Dim strCantidadLotesFicha As String
            Dim objParametros As Object() = {"pvch_codigoFicha", strCodigoFicha}
            Try
                strCantidadLotesFicha = m_sqlDtAccTintoreria.ObtenerValor("USP_TIN_OBTENER_CANTIDAD_LOTE_FICHA_TABLET", objParametros).ToString
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return strCantidadLotesFicha
        End Function
        Public Function ObtenerComentarioRegistroProduccionTablet(ByVal strCodigoFicha As String, ByVal intSecuencial As Integer, ByVal strCodigoMaquina As String,
                                                                     ByVal strCodigoParo As String, ByVal strCodigoEtapa As String, ByVal strCodigoTipoParoProduccion As String,
                                                                     ByVal dtmFechaInicio As DateTime, ByVal strHoraInicio As String, ByVal strTipoRegistro As String) As String
            Dim strComentario As String
            Dim objParametros As Object() = {"pvch_codigoFicha", strCodigoFicha,
                                             "pint_Secuencial", intSecuencial,
                                             "pvch_CodigoMaquina", strCodigoMaquina,
                                             "pvch_CodigoParo", strCodigoParo,
                                             "pvch_CodigoEtapa", strCodigoEtapa,
                                             "pvch_CodigoTipoParoProduccion", strCodigoTipoParoProduccion,
                                             "pdtm_FechaInicio", dtmFechaInicio,
                                             "pvch_HoraInicio", strHoraInicio,
                                             "pvch_TipoRegistro ", strTipoRegistro}
            Try
                strComentario = m_sqlDtAccTintoreria.ObtenerValor("USP_TIN_OBTENER_COMENTARIO_REGISTRO_PRODUCCION_TABLET", objParametros).ToString
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return strComentario
        End Function
        Public Function RegistrarDatosTurnoRegistroProduccionTablet(ByVal dtmFechaRegistroProduccion As DateTime, ByVal strTurnoRegistroProduccion As String,
                                                                    ByVal strCodigoMaquina As String, ByVal strEstadoRegistroProduccion As String,
                                                                    ByVal strCodigoSupervisor As String, ByVal strUsuarioCreacion As String) As Integer
            Dim intResult As Integer = 0
            Try
                Dim objParametros() As Object = {"pdtm_FechaRegistroProduccion", dtmFechaRegistroProduccion,
                                                 "pchr_TurnoRegistroProduccion", strTurnoRegistroProduccion.Trim, _
                                                 "pvch_CodigoMaquina", strCodigoMaquina.Trim, _
                                                 "pint_EstadoRegistroProduccion", strEstadoRegistroProduccion.Trim, _
                                                 "pvch_CodigoSupervisor", strCodigoSupervisor.Trim, _
                                                 "pvch_UsuarioCreacion", strUsuarioCreacion.Trim}
                intResult = m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_REGISTRAR_DATOS_TURNO_REGISTRO_PRODUCCION_TABLET", objParametros)


                Return intResult
            Catch ex As SqlClient.SqlException
                Throw ex
            Finally
                intResult = Nothing
            End Try
        End Function
        Public Function ObtenerDatosUltimoRegistroProduccionAbiertoTurnoTablet(ByVal strCodigoMaquina As String, ByVal intEstadoRegistroProduccion As Int16) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"pvch_CodigoMaquina", strCodigoMaquina,
                                             "pint_EstadoRegistroProduccion", intEstadoRegistroProduccion}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_ULTIMO_REGISTRO_PRODUCCION_TURNO_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosUltimoRegistroProduccionTodosTurnoTablet(ByVal strCodigoMaquina As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"pvch_CodigoMaquina", strCodigoMaquina,
                                             "pint_EstadoRegistroProduccion", ""}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_ULTIMO_REGISTRO_PRODUCCION_TURNO_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosRegistroProduccionTurnoTablet(ByVal strFechaRegistroProduccion As String, ByVal intTurnoRegistroProduccion As Int16) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"pdtm_FechaRegistroProduccion", strFechaRegistroProduccion,
                                             "pchr_TurnoRegistroProduccion", intTurnoRegistroProduccion}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_REGISTRO_PRODUCCION_TURNO_TABLET")
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ActualizarEstadoTurnoRegistroProduccionTablet(ByVal dtmFechaRegistroProduccion As DateTime, ByVal strTurnoRegistroProduccion As String,
                                                       ByVal strCodigoMaquina As String, ByVal strEstadoRegistroProduccion As String,
                                                       ByVal strUsuarioModificacion As String) As Integer
            Dim intResult As Integer = 0
            Try
                Dim objParametros() As Object = {"pdtm_FechaRegistroProduccion", dtmFechaRegistroProduccion,
                                                 "pchr_TurnoRegistroProduccion", strTurnoRegistroProduccion.Trim, _
                                                 "pvch_CodigoMaquina", strCodigoMaquina.Trim, _
                                                 "pint_EstadoRegistroProduccion", strEstadoRegistroProduccion.Trim, _
                                                 "pvch_UsuarioModificacion", strUsuarioModificacion.Trim}
                intResult = m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_ACTUALIZAR_ESTADO_TURNO_REGISTRO_PRODUCCION_TABLET", objParametros)


                Return intResult
            Catch ex As SqlClient.SqlException
                Throw ex
            Finally
                intResult = Nothing
            End Try
        End Function
        Public Function ObtenerDatosProduccionParcialTablet(ByVal strCodigoMaquina As String, ByVal dtmFechaRegistroProduccion As DateTime, ByVal strTurnoRegistroProduccion As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoMaquina", strCodigoMaquina,
                                             "pdtm_FechaRegistroProduccion", dtmFechaRegistroProduccion,
                                             "pvch_TurnoRegistroProduccion", strTurnoRegistroProduccion}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_PRODUCCION_PARCIAL_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function fnc_ObtenerMaestroReprocesoProduccionTablet(ByVal strCodigoReproceso As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_codigoReproceso", strCodigoReproceso}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_MAESTRO_REPROCESO_PRODUCCION_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function fnc_ObtenerMaestroOperacionProduccionTablet(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoMaquina", strCodigoMaquina,
                                             "pvch_codigoOperacion", strCodigoOperacion}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_MAESTRO_OPERACION_PRODUCCION_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosFichaTabletReproceso(ByVal strCodigoFicha As String, ByVal strCodigoMaquina As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoFicha", strCodigoFicha,
                                             "pvch_CodigoMaquina", strCodigoMaquina}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_FICHA_TABLET_REPROCESO_V2", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function VerificarRegParcialFichaSecuenciaTablet(ByVal strCodigoFicha As String, ByVal intSecuencia As Integer) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_codigoFicha", strCodigoFicha,
                                             "pint_secuencial", intSecuencia}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_VERIFICAR_REG_PARCIAL_FICHA_SECUENCIA_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function ObtenerDatosRegParcialFichaSecuenciaTablet(ByVal strCodigoFicha As String, ByVal intSecuencia As Integer) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_codigoFicha", strCodigoFicha,
                                             "pint_secuencial", intSecuencia}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_DATOS_REG_PARCIAL_FICHA_SECUENCIA_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function CerrarRegParcialFichaSecuenciaTablet(ByVal strCodigoFicha As String, ByVal intSecuencia As Integer, ByVal decVelocidad As Decimal) As Integer
            Dim intRegistroAfectados As Integer = 0
            Dim objParametros As Object() = {"pvch_CodigoFicha", strCodigoFicha,
                                             "pint_Secuencial", intSecuencia,
                                             "pnum_Velocidad", decVelocidad}
            Try
                intRegistroAfectados = m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_CERRAR_REG_PARCIAL_FICHA_SECUENCIA_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return intRegistroAfectados
        End Function
        Public Function VerificarRegParcialParoTablet(ByVal objRegistroProduccionTablet As RegistroProduccionTablet) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoMaquina", objRegistroProduccionTablet.Codigo_Maquina,
                                             "pvch_CodigoParo", objRegistroProduccionTablet.Codigo_Paro,
                                             "pvch_CodigoEtapa", objRegistroProduccionTablet.Codigo_Etapa,
                                             "pvch_CodigoTipoParoProduccion", objRegistroProduccionTablet.Tipo_Paro_Produccion,
                                             "pdtm_FechaInicio", CDate(objRegistroProduccionTablet.Fecha_Inicio),
                                             "pvch_HoraInicio", objRegistroProduccionTablet.Hora_Inicio_Real}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_VERIFICAR_REG_PARCIAL_PARO_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function
        Public Function CerrarRegParcialParoTablet(ByVal objRegistroProduccionTablet As RegistroProduccionTablet) As Integer
            Dim intRegistroAfectados As Integer = 0
            Dim objParametros As Object() = {"pvch_CodigoMaquina", objRegistroProduccionTablet.Codigo_Maquina,
                                             "pvch_CodigoParo", objRegistroProduccionTablet.Codigo_Paro,
                                             "pvch_CodigoEtapa", objRegistroProduccionTablet.Codigo_Etapa,
                                             "pvch_CodigoTipoParoProduccion", objRegistroProduccionTablet.Tipo_Paro_Produccion,
                                             "pvch_FechaInicio", objRegistroProduccionTablet.Fecha_Inicio,
                                             "pvch_FechaFin", objRegistroProduccionTablet.Fecha_Fin,
                                             "pvch_HoraInicio", objRegistroProduccionTablet.Hora_Inicio_Real,
                                             "pvch_HoraFin", objRegistroProduccionTablet.Hora_Fin}
            Try
                intRegistroAfectados = m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_CERRAR_REG_PARCIAL_PARO_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return intRegistroAfectados
        End Function
        Public Function MantenerDatosProduccionTurnoTablet(ByVal strFechaRegistroProduccion As String, ByVal intTurnoRegistroProduccion As Integer, ByVal strCodigoMaquina As String, ByVal strCodigoUsuario As String) As Integer
            Dim intRegistroAfectados As Integer = 0
            Dim objParametros As Object() = {"pvch_FechaRegistroProduccion", strFechaRegistroProduccion,
                                             "pint_TurnoRegistroProduccion", intTurnoRegistroProduccion,
                                             "pvch_CodigoMaquina", strCodigoMaquina,
                                             "pvch_CodigoUsuario", strCodigoUsuario}
            Try
                intRegistroAfectados = m_sqlDtAccTintoreria.EjecutarComando("USP_TIN_MANTENER_DATOS_PRODUCCION_TURNO_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return intRegistroAfectados
        End Function
        Public Function ObtenerDatosFichaAPartirTablet(ByVal strCodigoFicha As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros As Object() = {"pvch_CodigoFicha", strCodigoFicha}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("USP_TINT_OBTENER_DATOS_FICHA_A_PARTIR_TABLET", objParametros)
            Catch ex As SqlClient.SqlException
                Throw ex
            End Try
            Return dtblDatos
        End Function

        Public Function ObtenerListaSecuenciaRuta(ByVal pstr_codigoficha As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvch_codigoficha", pstr_codigoficha}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("USP_TIN_OBTENER_DATOS_RUTA_SECUENCIA_FICHA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region
    End Class

End Namespace

