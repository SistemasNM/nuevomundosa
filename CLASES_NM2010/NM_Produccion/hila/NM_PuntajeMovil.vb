Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Hilanderia
    Public Class NM_PuntajeMovil
        Private _objConnexion As AccesoDatosSQLServer
        Sub New()
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        End Sub
        Public Function ufn_ObtenerPuntajeTablet(ByVal pstr_fecha_registro_produccion As String, ByVal pstr_turno_registro_produccion As String) As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"pvch_fecha_registro_produccion", pstr_fecha_registro_produccion,
                                                 "pvch_turno_registro_produccion", pstr_turno_registro_produccion}

                Return _objConnexion.ObtenerDataTable("USP_HIL_OBTENER_PUNTAJE_TABLET", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function
        Public Function ufn_ActualizarPuntajeTabletMaquina(ByVal pstr_codigo_maquina As String,
                                                           ByVal pnum_ne_nominal As Decimal,
                                                           ByVal pint_husos_maquina As Integer,
                                                           ByVal pstr_fecha_registro_produccion As String,
                                                           ByVal pstr_turno_registro_produccion As String,
                                                           ByVal pnum_puntaje_inicial As Decimal,
                                                           ByVal pnum_puntos_final As Decimal,
                                                           ByVal pstr_usuarioModificacion As String) As Integer
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros() As Object = {"pvch_codigo_maquina", pstr_codigo_maquina,
                                                 "pnum_ne_nominal", pnum_ne_nominal,
                                                 "pint_husos_maquina", pint_husos_maquina,
                                                 "pvch_fecha_registro_produccion", pstr_fecha_registro_produccion,
                                                 "pvch_turno_registro_produccion", pstr_turno_registro_produccion,
                                                 "pnum_puntaje_inicial", pnum_puntaje_inicial,
                                                 "pnum_puntos_final", pnum_puntos_final,
                                                 "pvar_usuarioModificacion", pstr_usuarioModificacion}

                Return _objConnexion.EjecutarComando("USP_HIL_ACTUALIZAR_PUNTAJE_TABLET_MAQUINA", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function
    End Class
End Namespace

