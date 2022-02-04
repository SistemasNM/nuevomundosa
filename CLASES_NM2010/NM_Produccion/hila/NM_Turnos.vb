Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

    Public Class NM_Turnos

        Private _objConnexion As AccesoDatosSQLServer
        Sub New()
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub


        Public Function ufn_ObtenerTurnosxFecha(ByVal strAnhio As String) As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"pint_Anhio", strAnhio}

                Return _objConnexion.ObtenerDataTable("USP_HIL_OBTENER_TURNOS_X_FECHAS", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function


        Public Function ufn_RegistrarTurnosxFecha(ByVal strAnhio As Integer, ByVal strFechaInicio As Date, ByVal strFechaFin As Date, ByVal strHorasTurno As Integer, ByVal strUsuario As String) As Boolean

            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

                Dim objParametros() As Object = {"pint_Anhio", strAnhio,
                                                 "pvch_FechaInicio", strFechaInicio,
                                                 "pvch_FechaFin", strFechaFin,
                                                 "pint_HorasTurno", strHorasTurno,
                                                 "pvch_CodUsuario", strUsuario}

                Return _objConnexion.EjecutarComando("USP_HIL_REGISTRAR_TURNOS_X_FECHAS", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try

        End Function


        Public Function ufn_ActualizarTurnosxFecha(ByVal intIDTurno As Integer, ByVal strFechaInicio As Date, ByVal strFechaFin As Date, ByVal strHorasTurno As Integer, ByVal strUsuario As String) As Boolean

            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

                Dim objParametros() As Object = {"pint_IDTurno", intIDTurno,
                                                 "pvch_FechaInicio", strFechaInicio,
                                                 "pvch_FechaFin", strFechaFin,
                                                 "pint_HorasTurno", strHorasTurno,
                                                 "pvch_CodUsuario", strUsuario}

                Return _objConnexion.EjecutarComando("USP_HIL_ACTUALIZAR_TURNOS_X_FECHAS", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try

        End Function

        Public Function ufn_EliminarTurnosxFecha(ByVal intIDTurno As Integer, ByVal strUsuario As String) As Boolean

            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

                Dim objParametros() As Object = {"pint_IDTurno", intIDTurno,
                                                 "pvch_CodUsuario", strUsuario}

                Return _objConnexion.EjecutarComando("USP_HIL_ELIMINAR_TURNOS_X_FECHAS", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try

        End Function


        Public Function ufn_ObtenerStatusMaquina(ByVal strFecha As String) As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"pdtm_Fecha", strFecha}

                Return _objConnexion.ObtenerDataTable("USP_HIL_OBTENER_STATUS_MAQUINA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function

        Public Function ufn_ActualizarStatusMaquina(ByVal strCodigoMaquina As String, ByVal strFecha As Date, ByVal strValor1 As String, ByVal strValor2 As String, ByVal strValor3 As String, ByVal strUsuario As String) As Boolean

            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

                Dim objParametros() As Object = {"pvch_CodigoMaquina", strCodigoMaquina,
                                                 "pvch_Fecha", strFecha,
                                                 "pvch_Valor1", strValor1,
                                                 "pvch_Valor2", strValor2,
                                                 "pvch_Valor3", strValor3,
                                                 "pvch_CodUsuario", strUsuario}

                Return _objConnexion.EjecutarComando("USP_HIL_ACTUALIZAR_STATUS_MAQUINA", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try

        End Function

    End Class
End Namespace

