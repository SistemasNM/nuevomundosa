Imports NM.AccesoDatos

Public Class Evaluacion

#Region "-- CONEXION --"
    Private m_conexion As AccesoDatosSQLServer
#End Region

#Region "-- VARIABLES --"

    Private _IdEvaluacion As Integer
    Private _InicioProgram As String 'YYYYMMDD
    Private _FinalProgram As String 'YYYYMMDD
    Private _Descripcion As String
    Private _Observacion As String
    Private _Estado As String
    Private _Usuario As String
    Private _Datos As DataTable
    Private _MensajeError As String
    Private _Anhio_Periodo As String

#End Region

#Region "-- PROPIEDADES --"

    Public Property IdEvaluacion() As Integer
        Get
            Return _IdEvaluacion
        End Get
        Set(ByVal Value As Integer)
            _IdEvaluacion = Value
        End Set
    End Property

    Public Property InicioProgram() As String
        Get
            Return _InicioProgram
        End Get
        Set(ByVal Value As String)
            _InicioProgram = Value
        End Set
    End Property

    Public Property FinalProgram() As String
        Get
            Return _FinalProgram
        End Get
        Set(ByVal Value As String)
            _FinalProgram = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property Observacion() As String
        Get
            Return _Observacion
        End Get
        Set(ByVal Value As String)
            _Observacion = Value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return _Estado
        End Get
        Set(ByVal Value As String)
            _Estado = Value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return _Usuario
        End Get
        Set(ByVal Value As String)
            _Usuario = Value
        End Set
    End Property

    Public Property MensajeError() As String
        Get
            Return _MensajeError
        End Get
        Set(ByVal Value As String)
            _MensajeError = Value
        End Set
    End Property

    Public ReadOnly Property Datos() As DataTable
        Get
            Return _Datos
        End Get
    End Property

    Public Property Anhio_Periodo() As String
        Get
            Return _Anhio_Periodo
        End Get
        Set(ByVal Value As String)
            _Anhio_Periodo = Value
        End Set
    End Property

#End Region

    Sub New()
        m_conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub

#Region "-- METODOS --"
    Public Function ListarEvaluacionesActivas() As DataTable
        Try
            Return m_conexion.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUACIONES_ACTIVAS")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Obtiene evaluaciones por evaluador
    Public Function ListarEvaluaciones(ByVal pstrTipoConsulta As String, ByVal idEvaluado As Int32, ByVal idEvaluador As Int32, ByVal idRelacion As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"chr_TipoConsulta", pstrTipoConsulta, _
                                             "int_IdEvaluacion", _IdEvaluacion, _
                                             "int_IdEvaluado", idEvaluado, _
                                             "int_IdEvaluador", idEvaluador, _
                                             "int_IdRelacion", idRelacion, _
                                             "vch_Evaluador", _Usuario, _
                                             "chr_Estado", _Estado}

            dtblDatos = m_conexion.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUACIONES", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    'Obtiene los porcentajes x evaluacion x puesto
    Public Function ListarPorcentajesxEvaluacion(ByVal pstrTipoConsulta As String, ByVal pintPuesto As Integer) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pchr_TipoConsulta", pstrTipoConsulta, _
                                             "pint_IdEvaluacion", _IdEvaluacion, _
                                             "pint_IdPuesto", pintPuesto}
            dtblDatos = m_conexion.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUACIONPORCENTAJE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    'Registra los porcentajes x evaluacion x puesto
    Public Function RegistrarPorcentajesxEvaluacion(ByVal pstrAccion As String, ByVal pintPuesto As Integer, ByVal pintDivision As Integer, ByVal pdblPorcentaje As Double) As Boolean
        Try
            Dim objParametros As Object() = {"pchr_Accion", pstrAccion, _
                                             "pint_IdEvaluacion", _IdEvaluacion, _
                                             "pint_IdPuesto", pintPuesto, _
                                             "pint_IdDivision", pintDivision, _
                                             "pnum_Porcentaje", pdblPorcentaje}
            m_conexion.EjecutarComando("USP_NM_RRHH_REGISTRAR_EVALUACIONPORCENTAJE", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerarEvaluaciones() As Boolean

        Dim objParametros As Object() = {"IdEvaluacion", _IdEvaluacion, _
                                         "Usuario", _Usuario, _
                                         "Mensaje", _MensajeError}
        Try
            m_conexion.EjecutarComando("USP_NM_RRHH_EJECUTA_EVALUACION", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EliminarEvaluaciones() As Boolean

        Dim objParametros As Object() = {"IdEvaluacion", _IdEvaluacion, _
                                         "Usuario", _Usuario, _
                                         "Mensaje", _MensajeError}
        Try
            m_conexion.EjecutarComando("USP_NM_RRHH_ELIMINAR_EVALUACION", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerEvaluacion() As Boolean

        Dim objParametros As Object() = {"int_IdEvaluacion", _IdEvaluacion}

        Try
            _Datos = m_conexion.ObtenerDataTable("USP_NM_RRHH_OBTENER_EVALUACION", objParametros)
            If _Datos.Rows.Count > 0 Then
                _InicioProgram = _Datos.Rows(0)("vch_InicioProgram").ToString()
                _FinalProgram = _Datos.Rows(0)("vch_FinalProgram").ToString()
                _Descripcion = _Datos.Rows(0)("vch_Descripcion").ToString()
                _Observacion = _Datos.Rows(0)("vch_Observacion").ToString()
                _Estado = _Datos.Rows(0)("chr_Estado").ToString()
                _Anhio_Periodo = _Datos.Rows(0)("int_Anhio_Evaluacion").ToString
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GuardarEvaluacion(ByVal pstrAccion As String) As Boolean

        Dim objParametros As Object() = {"chr_Accion", pstrAccion, _
                                         "int_IdEvaluacion", _IdEvaluacion, _
                                         "chr_InicioProgram", _InicioProgram, _
                                         "chr_FinalProgram", _FinalProgram, _
                                         "vch_Descripcion", _Descripcion, _
                                         "vch_Observacion", _Observacion, _
                                         "chr_Estado", _Estado, _
                                         "vch_Usuario", _Usuario, _
                                         "int_Anhio", _Anhio_Periodo}
        Dim dt As DataTable
        Try

            dt = m_conexion.ObtenerDataTable("USP_NM_RRHH_REGISTRAR_EVALUACION", objParametros)
            _IdEvaluacion = dt.Rows(0)("int_IdEvaluacion")

            Return True
         
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarEvaluacionesxPuestoCantidades() As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", _IdEvaluacion}
            Return m_conexion.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUACIONESXPUESTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CambiarEvaluador(ByVal idEvaluado As Int32, ByVal idEvaluador As Int32, ByVal idRelacion As Int32, ByVal idEvaluadorNuevo As Int32, ByVal idRelacionNuevo As Int32) As Boolean

        Dim objParametros As Object() = {"int_IdEvaluacion", _IdEvaluacion, _
                                         "int_IdEvaluado", idEvaluado, _
                                         "int_IdEvaluador", idEvaluador, _
                                         "int_IdRelacion", idRelacion, _
                                         "chr_Estado", _Estado, _
                                         "int_IdEvaluadorNuevo", idEvaluadorNuevo, _
                                         "int_IdRelacionNuevo", idRelacionNuevo, _
                                         "vch_Usuario", _Usuario}

        Try

            m_conexion.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_ENCUESTA_EVALUADOR", objParametros)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Registra los porcentajes x evaluacion x puesto
    Public Function RegistrarPorcentajesxEvaluacion_Masivo() As Boolean
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", _IdEvaluacion}
            m_conexion.EjecutarComando("USP_NM_RRHH_REGISTRAR_EVALUACIONPORCENTAJE_MASIVO", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
