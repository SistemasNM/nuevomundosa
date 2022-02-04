Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaEngomadoDProduccion
        Dim BD As New NM_Consulta()
        Dim Usuario As String
        Public codPartEngTED As String
        Public plegador As String
        Public longitud As Integer
        Public lado As String
        Public cantPiezas As Integer
        Public operario As String
        Public calificacion As Double
        Public ocurrencias As String
        Public usuarioCrea As String
        Public fechaCrea As Date
        Public usuarioMod As String
        Public codigo_supervisor As String
        Public fechaMod As Date
        Public fechainicio As String
        Public fechafinal As String
        Public codMaquina As String
        Public observ_plegador As String

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Sub Insertar(ByVal pcodigoPartidaEngomado As String, ByVal pplegador As String, ByVal plado As String, _
              ByVal plongitud As Integer, ByVal pcantidadPiezas As Integer, ByVal poperario As String, _
              ByVal pocurrencias As String, ByVal pcodSupervisor As String, _
              ByVal pfechaInicio As String, ByVal pfechaFinal As String, ByVal pCodMaquina As String, ByVal pUsuario As String, ByVal pObsvPlegador As String)
            Dim ltmp1 As String = ""
            Dim Conexion As AccesoDatosSQLServer
            Try
                If pfechaInicio.Trim.Length >= 10 Then
                    ltmp1 = Left(pfechaInicio, 10)
                    pfechaInicio = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(pfechaInicio, 11, Len(pfechaInicio) - 10)
                End If
                If pfechaFinal.Trim.Length >= 10 Then
                    ltmp1 = Left(pfechaFinal, 10)
                    pfechaFinal = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(pfechaFinal, 11, Len(pfechaFinal) - 10)
                End If
                Dim objParametro() As Object = {"codigo_partida_engomadoted", pcodigoPartidaEngomado, _
                                                "codigo_plegador", pplegador, _
                                                "lado", plado, _
                                                "longitud", plongitud, _
                                                "cantidad_piezas", pcantidadPiezas, _
                                                "operario", poperario, _
                                                "calificacion", calificacion, _
                                                "ocurrencias", pocurrencias, _
                                                "usuario_creacion", pUsuario, _
                                                "codigo_supervisor", pcodSupervisor, _
                                                "fecha_inicio", pfechaInicio, _
                                                "fecha_final", pfechaFinal, _
                                                "codigo_maquina", pCodMaquina,
                                                "observ_plegador", pObsvPlegador}

                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Conexion.EjecutarComando("usp_NM_PartidaEngomadoDProduccion_Insertar_2", objParametro)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Noviembre 2014
        'Modificado: Implementar IQ TED
        '-----------------------------------------------------------
        Public Function eliminar(ByVal pCodPartEngomadTED As String, ByVal plegador As String) As String
            Dim strSQL As String = ""
            Dim strResultado As String = ""
            Dim Conexion As AccesoDatosSQLServer
            Try
                strSQL = "usp_nm_partidaengomadodproduccion_eliminarplegador"
                If pCodPartEngomadTED <> "" And plegador <> "" Then
                    Dim objParametro() As Object = {"vch_CodigoPartidaEngomado", _
                                                    pCodPartEngomadTED, "vch_CodigoPlegador", plegador}
                    Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                    strResultado = Conexion.ObtenerDataTable(strSQL, objParametro).Rows(0)("Resultado")

                Else
                    Throw New Exception("Imposible eliminar porque el codigo de partida de engomado ted no es valido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return strResultado
        End Function

        Public Function Exist(ByVal pCodigoPartida As String, ByVal pCodigoPlegador As String) As Boolean
            Dim strSQL As String, dtPlegadores As New DataTable
            Dim objConn As New NM_Consulta
            Try
                If pCodigoPartida <> "" AndAlso pCodigoPlegador <> "" Then
                    strSQL = "Select * FROM NM_PartidaEngomadoDProduccion " & _
                    " where codigo_partida_engomadoted ='" & pCodigoPartida & _
                    "' and codigo_plegador = '" & pCodigoPlegador & "'"
                    dtPlegadores = objConn.Query(strSQL)
                    Return (dtPlegadores.Rows.Count > 0)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function CalcularCalificaion() As Double
            Return 0.0
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        'Fecha: Noviembre 2014
        'Modificado: IQ TED
        '-----------------------------------------------------------
        Public Function Actualizar() As String
            Dim ltmp1 As String = ""
            Dim strResultado As String = ""

            Dim Conexion As AccesoDatosSQLServer
            If fechainicio.Trim.Length >= 10 Then
                ltmp1 = Left(fechainicio, 10)
                fechainicio = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(fechainicio, 11, Len(fechainicio) - 10)
            End If
            If fechafinal.Trim.Length >= 10 Then
                ltmp1 = Left(fechafinal, 10)
                fechafinal = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(fechafinal, 11, Len(fechafinal) - 10)
            End If
            Try
                Dim objParametro() As Object = {"codigo_partida_engomadoted", codPartEngTED, _
                                                "codigo_plegador", plegador, _
                                                "lado", lado, _
                                                "longitud", longitud, _
                                                "cantidad_piezas", cantPiezas, _
                                                "operario", operario, _
                                                "ocurrencias", ocurrencias, _
                                                "codigo_supervisor", codigo_supervisor, _
                                                "fecha_inicio", fechainicio, _
                                                "fecha_final", fechafinal, _
                                                "codigo_maquina", codMaquina, _
                                                "usuario_modificacion", usuarioMod,
                                                "observ_plegador", observ_plegador}

                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                strResultado = Conexion.ObtenerDataTable("usp_NM_PartidaEngomadoDProduccion_Actualizar_2", objParametro).Rows(0)("Resultado")
            Catch ex As Exception
                Throw ex
            End Try
            Return strResultado

        End Function

        Function List(ByVal sCodigoPartida As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
            sql = "Select * from NM_PartidaEngomadoDProduccion " & _
            " where codigo_partida_engomadoted='" & sCodigoPartida & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function
        '----------------REQSIS201700007 - DG - INI - 18/12/2017-------------
        Function ListbyArticulo(ByVal sCodigoArticulo As String) As DataTable

            'Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
            'sql = "Select distinct EP.codigo_plegador, E.codigo_partida_engomadoted, " & _
            '" (EP.codigo_plegador + ' // ' + E.codigo_partida_engomadoted) as nombre " & _
            '" from NM_PartidaEngomadoyTED E, NM_ParticionPartidas P,  NM_PartidaEngomadoCorrelativo PEC, " & _
            '" NM_PartidaUrdido U, NM_TelaUrdimbre T , NM_PartidaEngomadoDProduccion EP " & _
            '" where E.codigo_sub_partida_urdido = P.codigo_sub_partida_urdido " & _
            '" and P.codigo_partida_urdido = U.codigo_partida_urdido " & _
            '" and E.codigo_partida_engomadoted = EP.codigo_partida_engomadoted " & _
            '" and T.codigo_urdimbre = U.codigo_urdimbre " & _
            '" and E.codigo_partida_engomadoted = PEC.codigo_partida_engomadoted " & _
            '" and EP.codigo_partida_engomadoted = PEC.codigo_partida_engomadoted " & _
            '" and EP.codigo_plegador = PEC.codigo_plegador " & _
            '" and T.codigo_articulo = '" & sCodigoArticulo & "' " & _
            '" and PEC.correlativo not in (select codigo_pieza from NM_Pieza) " & _
            '" order by EP.codigo_plegador, E.codigo_partida_engomadoted "
            'dt = objConn.Query(sql)
            'Return dt
            Dim dt As DataTable
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pvch_CodigoArticulo", sCodigoArticulo}
            Try
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dt = Conexion.ObtenerDataTable("usp_Listar_Partidas_Por_Articulo", objParametro)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Function ListbyArticuloRolloCortado(ByVal sCodigoArticulo As String) As DataTable
            Dim dt As DataTable
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pvch_CodigoArticulo", sCodigoArticulo}
            Try
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dt = Conexion.ObtenerDataTable("usp_listar_partidas_por_articulo_rollo_cortado", objParametro)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        '----------------REQSIS201700007 -DG - FIN - 18/12/2017-------------


        Function ListbyArticulo(ByVal sCodigoArticulo As String, ByVal bIs90 As Boolean) As DataTable
            Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
            Dim fila As DataRow, dtCheck As New DataTable, dt2 As New DataTable
            dt = ListbyArticulo(sCodigoArticulo)
            dt2 = dt.Clone
            For Each fila In dt.Rows
                sql = "Select * from NM_plegadorMontado " & _
                " where codigo_plegador = '" & fila("codigo_plegador") & "' " & _
                " and codigo_partida_engomadoted = '" & fila("codigo_partida_engomadoted") & "' "
                dtCheck = objConn.Query(sql)
                If dtCheck.Rows.Count = 0 Then
                    dt2.ImportRow(fila)
                End If
            Next
            Return dt2
        End Function
    End Class
End Namespace
