Imports NM.AccesoDatos
Imports NM_General


Public Class Cuestionario
#Region " Declaracion de Variables Miembro "
    Private m_sqlCuestionario As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlCuestionario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region

    Public Function ListarPreguntas(ByVal int_idpuesto As Int32) As DataTable
        Try
            'Dim objParametros As Object() = {"int_idpuesto", int_idpuesto}
            'Return m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_PREGUNTAS", objParametros)
            Return m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_PREGUNTAS")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPreguntasContestadasCab(ByVal Evaluacion As Integer, ByVal Evaluado As Int32, ByVal Evaluador As Int32, ByVal Relacion As Integer) As DataTable
        Try
            Dim dtblDatos As DataTable
            Try
                Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                                 "int_IdEvaluado", Evaluado, _
                                                 "int_IdEvaluador", Evaluador, _
                                                 "int_IdRelacion", Relacion}
                dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_RECUPERAENCUESTA_CAB", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ListarPreguntasContestadas(ByVal Evaluacion As Integer, ByVal Evaluado As Int32, ByVal Evaluador As Int32, ByVal Relacion As Integer) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluado", Evaluado, _
                                             "int_IdEvaluador", Evaluador, _
                                             "int_IdRelacion", Relacion}
            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_RECUPERAENCUESTA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function InsertarEncuestaXML(ByVal dtEncuesta As DataTable) As Integer
        Dim cantidad As Integer
        Try
            Dim cadenaXML As String
            Dim clsUtil As New Util
            cadenaXML = clsUtil.GeneraXml(dtEncuesta)
            Dim objParametros As Object() = {"vch_EncuestaXML", cadenaXML}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_INSERTA_ENCUESTA_DETALLE", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function

    Public Function ActualizarEncuestaXML(ByVal Evaluacion As Integer, ByVal Evaluado As Integer, ByVal Evaluador As Integer, _
                                          ByVal Relacion As Integer, ByVal dtEncuesta As DataTable, ByVal Usuario As String) As Integer
        Dim cantidad As Integer
        Try
            Dim cadenaXML As String
            Dim clsUtil As New Util
            cadenaXML = clsUtil.GeneraXml(dtEncuesta)
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluado", Evaluado, _
                                             "int_IdEvaluador", Evaluador, _
                                             "int_IdRelacion", Relacion, _
                                             "vch_EncuestaXML", cadenaXML, _
                                             "vch_Usuario", Usuario}

            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_ENCUESTA_DETALLE", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function

    ''' <summary>
    ''' Nuevo Metodo (LUIS_AJ)
    ''' </summary>
    ''' <param name="Evaluacion"></param>
    ''' <param name="Evaluado"></param>
    ''' <param name="Evaluador"></param>
    ''' <param name="Relacion"></param>
    ''' <param name="dtEncuesta"></param>
    ''' <param name="Usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActualizarEncuestaXML_V2(ByVal dtEncuesta As DataTable, ByVal Estado As String,
                                             ByVal Situacion As String, ByVal Usuario As String) As Integer
        Dim cantidad As Integer
        Try
            Dim cadenaXML As String
            Dim clsUtil As New Util
            cadenaXML = clsUtil.GeneraXml(dtEncuesta)
            Dim objParametros As Object() = {"vch_EncuestaXML", cadenaXML,
                                             "chr_Estado", Estado,
                                             "chr_Situacion", Situacion,
                                             "vch_Usuario", Usuario}

            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_EVALUACION_EDO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function

    Public Function ActualizarEncuestaXML_V4(ByVal dtEncuesta As DataTable, ByVal Estado As String,
                                             ByVal Situacion As String, ByVal Fortalezas As String,
                                             ByVal Mejoras As String, ByVal Usuario As String) As Integer
        Dim cantidad As Integer
        Try
            Dim cadenaXML As String
            Dim clsUtil As New Util
            cadenaXML = clsUtil.GeneraXml(dtEncuesta)
            Dim objParametros As Object() = {"vch_EncuestaXML", cadenaXML,
                                             "chr_Estado", Estado,
                                             "chr_Situacion", Situacion,
                                             "vch_Fortalezas", Fortalezas,
                                             "vch_Mejoras", Mejoras,
                                             "vch_Usuario", Usuario}

            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_EVALUACION_EDO_V4", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function

    Public Function ActualizarEstadoEncuesta(ByVal idEstado As Int32, ByVal idEvaluado As Int32, ByVal idEvaluador As Int32) As Integer
        Dim cantidad As Integer
        Try
            Dim objParametros As Object() = {"int_IdEstado", idEstado, "int_IdEvaluado", idEvaluado, "int_IdEvaluador", idEvaluador}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_ESTADO_ENCUESTA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function
    Public Function RecuperaIdEncuesta(ByVal idEvaluado As Int32, ByVal idEvaluador As Int32) As Integer
        Dim dt As New DataTable
        Dim idEncuesta As Int32
        Try
            Dim objParametros As Object() = {"int_IdEvaluado", idEvaluado, "int_IdEvaluador", idEvaluador}
            dt = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_RECUPERAIDENCUESTA", objParametros)
            Dim row As DataRow
            row = dt.Rows(0)
            If row("idEncuesta") Is System.DBNull.Value Then
                idEncuesta = 0
            Else
                idEncuesta = Convert.ToInt32(row("idEncuesta"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return idEncuesta
    End Function
    Public Function GeneraIdEncuesta(ByVal idEvaluador As Int32, ByVal idEvaluado As Int32) As Integer
        Dim dt As New DataTable
        Dim idEncuestaGenerado As Integer
        Try
            Dim objParametros As Object() = {"int_IdEvaluador", idEvaluador, "int_IdEvaluado", idEvaluado}
            dt = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHHH_GENERAID", objParametros)
            Dim row As DataRow
            row = dt.Rows(0)
            idEncuestaGenerado = Convert.ToInt32(row("idgenerado"))
        Catch ex As Exception
            Throw ex
        End Try
        Return idEncuestaGenerado
    End Function

    Public Function ActualizarEncuestaCab(ByVal Evaluacion As Integer, ByVal Evaluado As Integer, ByVal Evaluador As Integer, ByVal Relacion As Integer, ByVal Estado As String, ByVal Situacion As String, ByVal Fortalezas As String, ByVal Mejoras As String, ByVal Usuario As String) As Integer
        Dim cantidad As Integer
        Dim dt As New DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluado", Evaluado, _
                                             "int_IdEvaluador", Evaluador, _
                                             "int_IdRelacion", Relacion, _
                                             "chr_Estado", Estado, _
                                             "chr_Situacion", Situacion, _
                                             "vch_Fortalezas", Fortalezas, _
                                             "vch_Mejoras", Mejoras, _
                                             "vch_Usuario", Usuario}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_ENCUESTA_CABECERA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function


    Public Function ListarDivisionCompetencias() As DataTable
        Try
            Return m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_DIVISION_COMPETENCIA")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListarCompetencias(ByVal int_IdDivision As Integer) As DataTable
        Dim dtCompetencias As DataTable
        Try
            Dim objParametros As Object() = {"int_IdDivision", int_IdDivision}
            dtCompetencias = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_COMPETENCIA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtCompetencias
    End Function
    Public Function ListarCompetenciasTodas() As DataTable
        Dim dtCompetencias As DataTable
        Try
            dtCompetencias = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_COMPETENCIA_TODAS")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtCompetencias
    End Function

    Public Function ListarCompetenciasXpUESTO(ByVal int_IdPuesto As Integer) As DataTable
        Dim dtCompetencias As DataTable
        Try
            Dim objParametros As Object() = {"int_idpuesto", int_IdPuesto}
            dtCompetencias = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_COMPETENCIA_ACTIVASXPUESTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtCompetencias
    End Function

    Public Function ListarCompetenciasInactivasXpuesto(ByVal int_IdPuesto As Integer) As DataTable
        Dim dtCompetencias As DataTable
        Try
            Dim objParametros As Object() = {"int_idpuesto", int_IdPuesto}
            dtCompetencias = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_COMPETENCIA_INACTIVASXPUESTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtCompetencias
    End Function

    Public Function ListarPreguntasMantenimiento(ByVal int_IdCompetencia As Integer) As DataTable
        Dim dtPreguntas As DataTable
        Try
            Dim objParametros As Object() = {"int_IdCompetencia", int_IdCompetencia}
            dtPreguntas = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_PREGUNTAS_MANT", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtPreguntas
    End Function
    Public Function InsertarPregunta(ByVal int_IdCompetencia As Integer, ByVal vch_preguntas As String) As Integer
        Dim cantidad As Integer
        Dim dt As New DataTable
        Try
            Dim objParametros As Object() = {"int_IdCompetencia", int_IdCompetencia, "vch_preguntas", vch_preguntas}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_INSERTAR_PREGUNTAS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function

    Public Function EliminarPregunta(ByVal int_IdCuestionario As Integer) As Integer
        Dim cantidad As Integer
        Dim dt As New DataTable
        Try
            Dim objParametros As Object() = {"int_IdCuestionario", int_IdCuestionario}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ELIMINAR_PREGUNTAS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function
    Public Function ActualizarPregunta(ByVal int_IdCuestionario As Integer, ByVal vch_Preguntas As String) As Integer
        Dim cantidad As Integer
        Dim dt As New DataTable
        Try
            Dim objParametros As Object() = {"int_IdCuestionario", int_IdCuestionario, "vch_Preguntas", vch_Preguntas}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZAR_PREGUNTAS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function
    Public Function InsertarCompetenciaxPuesto(ByVal intPuesto As Int32, ByVal int_IdCompetencia As Int32) As Integer
        Dim cantidad As Integer
        Try
            Dim objParametros As Object() = {"int_Puesto", intPuesto, "int_IdCompetencia", int_IdCompetencia}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_INSERTA_COMPETENCIAXPUESTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function
    Public Function eLIMINARCompetenciaxPuesto(ByVal intPuesto As Int32, ByVal int_IdCompetencia As Int32) As Integer
        Dim cantidad As Integer
        Try
            Dim objParametros As Object() = {"int_Puesto", intPuesto, "int_IdCompetencia", int_IdCompetencia}
            cantidad = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ELIMINA_COMPETENCIAXPUESTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return cantidad
    End Function

    Public Function ObtenerListarBusquedaPreguntas(ByVal idDivision As Int32, ByVal idCompetencias As Int32, ByVal idPuesto As Int32, ByVal idEstado As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_IdDivision", idDivision, _
                                             "pint_IdCompetencia", idCompetencias, _
                                             "pint_IdPuesto", idPuesto, _
                                             "pvch_IdEstado", idEstado}

            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_LISTAR_BUSQUEDA_PREGUNTAS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ObtenerCodigoPreguntaNuevoRegistro() As Integer
        Dim intResult As Integer

        Try
            intResult = m_sqlCuestionario.ObtenerValor("USP_NM_RRHH_OBTENER_CODIGO_PREGUNTA_NUEVO")
        Catch ex As Exception
            Throw ex
        End Try
        Return intResult

    End Function
    'BuscarDatosPregunta

    Public Function BuscarDatosPregunta(ByVal intCodigo As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"pint_Codigo", intCodigo}
            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_BUSCAR_DATOS_PREGUNTA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ActualizarDatosPregunta(ByVal intCodPregunta As Int32, ByVal intCompetencia As Int32, ByVal intPuesto As Int32, ByVal strPregunta As String, ByVal dblPesoDecimal As Double, ByVal strEstado As String, ByVal strUsuModificacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdCuestionario", intCodPregunta, _
                                             "pint_IdCompetencia", intCompetencia, _
                                             "pint_IdPuesto", intPuesto, _
                                             "pvch_Preguntas", strPregunta, _
                                             "pdbl_PesoDecimal", dblPesoDecimal, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuModificacion", strUsuModificacion}

            intResult = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_ACTUALIZA_DATOS_PREGUNTA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function RegistrarDatosPregunta(ByVal intCompetencia As Int32, ByVal intPuesto As Int32, ByVal strPregunta As String, ByVal dblPesoDecimal As Double, ByVal strEstado As String, ByVal strUsuCreacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdCompetencia", intCompetencia, _
                                             "pint_IdPuesto", intPuesto, _
                                             "pvch_Preguntas", strPregunta, _
                                             "pdbl_PesoDecimal", dblPesoDecimal, _
                                             "pvch_Estado", strEstado, _
                                             "pvch_UsuCreacion", strUsuCreacion}

            intResult = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_REGISTRAR_DATOS_PREGUNTA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function GenerarCopiaPreguntas(ByVal intPuestoOrigen As Int32, ByVal intPuestoDestino As Int32, ByVal strUsuCreacion As String) As Integer
        Dim intResult As Integer

        Try
            Dim objParametros As Object() = {"pint_IdPuestoOrigen", intPuestoOrigen, _
                                             "pint_IdPuestoDestino", intPuestoDestino, _
                                             "pvch_UsuCreacion", strUsuCreacion}

            intResult = m_sqlCuestionario.EjecutarComando("USP_NM_RRHH_GENERA_COPIA_PREGUNTAS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return intResult
    End Function

    Public Function ListarPreguntasContestadas_v4(ByVal Evaluacion As Integer, ByVal Evaluado As Int32, ByVal Evaluador As Int32, ByVal Relacion As Integer) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluado", Evaluado, _
                                             "int_IdEvaluador", Evaluador, _
                                             "int_IdRelacion", Relacion}
            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_RECUPERAENCUESTA_V4", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function


End Class
