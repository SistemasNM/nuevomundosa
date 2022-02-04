Imports NM.AccesoDatos
Public Class Puesto

#Region " Declaracion de Variables Miembro "
    Private m_sqlEDO As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlEDO = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region


    'Obtiene Lista de Tipo de Evaluacion
    Public Function ObtenerTiposDeEvaluacion() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_TIPOEVALUACION")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function


    Public Function ListarPuestosxEvaluacion(ByVal TipoConsulta As String, ByVal IdEvaluacion As Int32) As DataTable

        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pchr_TipoConsulta", TipoConsulta, _
                                             "pint_IdEvaluacion", IdEvaluacion}


            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_PUESTOSXEVALUACION", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ObtenerListarBusquedaPuestos(ByVal IdTipoEvaluacion As Int32, ByVal IdEstado As String) As DataTable

        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_IdTipoEvaluacion", IdTipoEvaluacion, _
                                             "pvch_IdEstado", IdEstado}

            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_LISTAR_BUSQUEDA_PUESTOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function


    Public Function BuscarDatosPuesto(ByVal intCodigo As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_Codigo", intCodigo}
            dtblDatos = m_sqlEDO.ObtenerDataTable("USP_NM_RRHH_BUSCAR_DATOS_PUESTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ActualizarDatosPuesto(ByVal intCodPuesto As Int32, ByVal strDescripcion As String, ByVal intTipoEvaluacion As Int32, ByVal strInfoAdicional As String, ByVal strEstado As String, ByVal strUsuModificacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdPuesto", intCodPuesto, _
                                             "pvch_DesPuesto", strDescripcion, _
                                             "pint_IdTipoEvaluacion", intTipoEvaluacion, _
                                             "pvch_CampoAdicional", strInfoAdicional, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuModificacion", strUsuModificacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_ACTUALIZA_DATOS_PUESTO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function RegistrarDatosPuesto(ByVal strDescripcion As String, ByVal intTipoEvaluacion As Int32, ByVal strInfoAdicional As String, ByVal strEstado As String, ByVal intPuestoCopia As Int32, ByVal strUsuCreacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pvch_DesPuesto", strDescripcion, _
                                             "pint_IdTipoEvaluacion", intTipoEvaluacion, _
                                             "pvch_CampoAdicional", strInfoAdicional, _
                                             "pvch_Estado", strEstado, _
                                             "pint_IdPuestoCopia", intPuestoCopia, _
                                             "pvch_UsuCreacion", strUsuCreacion}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_REGISTRAR_DATOS_PUESTO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function RegistrarPuestoxEvaluacion(ByVal strAccion As String, ByVal intIdEvaluacion As Int32, ByVal intIdPuesto As Int32, ByVal strUsuario As String) As Boolean
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pchr_Accion", strAccion, _
                                             "pint_IdEvaluacion", intIdEvaluacion, _
                                             "pint_IdPuesto", intIdPuesto, _
                                             "pvch_Usuario", strUsuario}

            intResult = m_sqlEDO.EjecutarComando("USP_NM_RRHH_REGISTRAR_PUESTOSXEVALUACION", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function


    Public Function ObtenerCodigoPuestoNuevoRegistro() As Integer
        Dim intResult As Integer

        Try
            intResult = m_sqlEDO.ObtenerValor("USP_NM_RRHH_OBTENER_CODIGO_PUESTO_NUEVO")
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function


End Class
