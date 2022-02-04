Imports NM.AccesoDatos

Public Class Competencia

#Region " Declaracion de Variables Miembro "
    Private m_sqlEDO As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlEDO = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region

#Region "Funciones"

    'Obtiene Lista de Tipo de Evaluacion
    Public Function ObtenerDivisionCompetencia() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_DIVISIONCOMPETENCIA")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function


    Public Function ObtenerListarBusquedaCompetencias(ByVal IdDivision As Int32, ByVal IdEstado As String) As DataTable

        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_IdDivision", IdDivision, _
                                             "pvch_IdEstado", IdEstado}

            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_BUSQUEDA_COMPETENCIAS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function


    Public Function BuscarDatosCompetencia(ByVal intCodigo As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_Codigo", intCodigo}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_BUSCAR_DATOS_COMPETENCIA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ActualizarDatosCompetencia(ByVal intCodCompetencia As Int32, ByVal strDescripcion As String, ByVal intDivision As Int32, ByVal intPuntajeDeseado As Int32, ByVal strEstado As String, ByVal strUsuModificacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdCompetencia", intCodCompetencia, _
                                             "pvch_DesCompetencia", strDescripcion, _
                                             "pint_IdDivision", intDivision, _
                                             "pint_PuntajeDeseado", intPuntajeDeseado, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuModificacion", strUsuModificacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_ACTUALIZA_DATOS_COMPETENCIA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function RegistrarDatosCompetencia(ByVal strDescripcion As String, ByVal intDivision As Int32, ByVal intPuntajeDeseado As Int32, ByVal strEstado As String, ByVal strUsuCreacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pvch_DesCompetencia", strDescripcion, _
                                             "pint_IdDivision", intDivision, _
                                             "pint_PuntajeDeseado", intPuntajeDeseado, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuCreacion", strUsuCreacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_REGISTRAR_DATOS_COMPETENCIA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function ObtenerCodigoCompetenciaNuevoRegistro() As Integer
        Dim intResult As Integer

        Try
            intResult = m_sqlEDO.ObtenerValor("USP_NM_RRHH_OBTENER_CODIGO_COMPETENCIA_NUEVO")
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function

    Public Function ObtenerListarBusquedaNivelesCompetencia(ByVal IdDivision As Int32, ByVal IdCompetencia As String) As DataTable

        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_IdDivision", IdDivision,
                                             "pint_IdCompetencia", IdCompetencia}

            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_BUSQUEDA_NIVELES_COMPETENCIA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function BuscarDatosNivelCompetencia(ByVal int_IdCompetencia As Int32, ByVal int_IdNivel As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_IdCompetencia", int_IdCompetencia,
                                             "pint_IdNivel", int_IdNivel}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_BUSCAR_DATOS_NIVEL_COMPETENCIA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ActualizarDatosNivelCompetencia(ByVal intIdCompetencia As Int32, ByVal intNivel As Int32, ByVal strDescripcion As String, ByVal strUsuModificacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdCompetencia", intIdCompetencia, _
                                             "pint_IdNivel", intNivel, _
                                             "pvch_Descripcion_Nivel", strDescripcion, _
                                             "pvch_UsuModificacion", strUsuModificacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_ACTUALIZA_DATOS_NIVEL_COMPETENCIA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function ObtenerIdNivelCompetenciaNuevoRegistro(ByVal int_IdCompetencia As Int32) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdCompetencia", int_IdCompetencia}
            intResult = m_sqlEDO.ObtenerValor("USP_NM_RRHH_OBTENER_ID_NIVEL_COMPETENCIA_NUEVO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function

    Public Function RegistrarDatosNivelCompetencia(ByVal intIdCompetencia As Int32, ByVal strDescripcion As String, ByVal strUsuCreacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdCompetencia", intIdCompetencia, _
                                             "pvch_Descripcion_Nivel", strDescripcion, _
                                             "pvch_UsuCreacion", strUsuCreacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_REGISTRAR_DATOS_NIVEL_COMPETENCIA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function
#End Region

End Class
