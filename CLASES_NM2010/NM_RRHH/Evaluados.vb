Imports NM.AccesoDatos
Public Class Evaluados

#Region "-- VARIABLES --"
    Private m_sqlCuestionario As AccesoDatosSQLServer

    Private _IdEvaluado As Integer
    Private _IdEvaluador As String
    Private _IdRelacion As String
    Private _Estado As String
    Private _Usuario As String
    Private _Datos As DataTable
    Private _MensajeError As String

#End Region

#Region "-- PROPIEDADES --"

    Public Property IdEvaluado() As Integer
        Get
            Return _IdEvaluado
        End Get
        Set(ByVal Value As Integer)
            _IdEvaluado = Value
        End Set
    End Property

    Public Property IdEvaluador() As Integer
        Get
            Return _IdEvaluador
        End Get
        Set(ByVal Value As Integer)
            _IdEvaluador = Value
        End Set
    End Property

    Public Property IdRelacion() As Integer
        Get
            Return _IdRelacion
        End Get
        Set(ByVal Value As Integer)
            _IdRelacion = Value
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

#End Region

#Region "-- CONSTRUCTORES --"
    Sub New()
        m_sqlCuestionario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region

    Public Function ListarEvaluador(ByVal TipoConsulta As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pchr_TipoConsulta", TipoConsulta, _
                                             "pint_idEvaluado", _IdEvaluado, _
                                             "pint_idEvaluador", _IdEvaluador, _
                                             "pint_idRelacion", _IdRelacion, _
                                             "pchr_Estado", _Estado}

            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTA_EVALUADORES", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function GuardarEvaluador(ByVal Accion As String) As Boolean

        Try
            Dim objParametros As Object() = {"pchr_Accion", Accion, _
                                             "pint_IdEvaluado", _IdEvaluado, _
                                             "pint_IdEvaluador", _IdEvaluador, _
                                             "pint_IdRelacion", _IdRelacion, _
                                             "pchr_Estado", _Estado, _
                                             "pvch_Usuario", _Usuario}
            m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_REGISTRAR_EVALUADOR", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarRelacion(ByVal TipoConsulta As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pchr_TipoConsulta", TipoConsulta}
            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_RELACION", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
End Class
