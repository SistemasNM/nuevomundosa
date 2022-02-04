Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos


Namespace NM_Tejeduria

    Public Class NM_ParejaPlegador
        Private _objConexion As AccesoDatosSQLServer
        Public CodigoPartida As String
        Public CodigoPlegador As String
        Public CodigoLado As String
        Public CodigoPareja As String
        Public Debug As String
        Public Usuario As String

        Sub New()
            CodigoPartida = ""
            CodigoPlegador = ""
            CodigoLado = ""
            CodigoPareja = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_ParejaPlegador(codigo_partida_engomadoted, " & _
                "codigo_plegador, codigo_lado, codigo_plegador_pareja, usuario_creacion," & _
                "fecha_creacion) values('" & Trim(CodigoPartida) & "','" & Trim(CodigoPlegador) & "','" & _
                Trim(CodigoLado) & "','" & Trim(CodigoPareja) & "','" & Usuario & "',getdate())"
                Debug = sql
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_ParejaPlegador set codigo_plegador_pareja='" & _
                Trim(CodigoPareja) & "', usuario_modificacion='" & Usuario & "', " & _
                "fecha_modificacion=getdate() where codigo_partida_engomadoted='" & _
                CodigoPartida & "' and codigo_plegador='" & Trim(CodigoPlegador) & "' and " & _
                "codigo_lado='" & Trim(CodigoLado) & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Function Delete(ByVal sCodigoPartida As String, ByVal sCodigoPlegador As String, ByVal sCodigoLado As String) As Boolean
            Dim sql As String
            Dim objConn As New NM_Consulta
            Try
                sql = "Delete from NM_ParejaPlegador where codigo_partida_engomadoted='" & _
                sCodigoPartida & "' and codigo_plegador='" & sCodigoPlegador & "' and " & _
                "codigo_lado LIKE '%" & sCodigoLado & "%' "
                objConn.Execute(sql)
                Return True
            Catch
                Return False
            End Try
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Function EliminarParejaPlegador(ByVal sCodigoPartida As String, ByVal sCodigoPlegador As String) As DataTable
            Dim Conexion As AccesoDatosSQLServer
            Dim dtbResultado As DataTable
            dtbResultado = Nothing
            Try
                Dim objParametro() As Object = {"codigo_partida_engomadoted", sCodigoPartida, _
                                                "codigo_plegador", sCodigoPlegador}

                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dtbResultado = Conexion.ObtenerDataTable("usp_NM_PartidaEngomadoDProduccion_EliminaEmparejado", objParametro)
                Return dtbResultado
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function DeletePareja(ByVal sCodigoPartida As String, ByVal sCodigoPlegador As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_ParejaPlegador where codigo_partida_engomadoted='" & _
                sCodigoPartida & "' and codigo_plegador_pareja='" & sCodigoPlegador & "' "
                Debug = sql
                objConn.Execute(sql)
                Return True
            Catch
                Return False
            End Try

        End Function

        Function Exist(ByVal sCodigoPartida As String, ByVal sCodigoPlegador As String, _
         ByVal sCodigoLado As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            Try
                sql = "Select * from NM_ParejaPlegador where codigo_partida_engomadoted='" & _
                sCodigoPartida & "' and codigo_plegador='" & sCodigoPlegador & "' and " & _
                "codigo_lado='" & sCodigoLado & "' "
                dt = objConn.Query(sql)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch
                Return False
            End Try

        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim DT As New DataTable

            sql = "Select * from NM_ParejaPlegador"
            DT = objConn.Query(sql)
            Return DT
        End Function

        Function Obtener(ByVal strCodigoPartida As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As String = {"var_PartidaEngomado", strCodigoPartida}
                Return _objConexion.ObtenerDataTable("usp_PTJ_PartidaEngomadoParejaPlegador_Obtener_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal sCodigoPartida As String, ByVal bParaGrid As Boolean) As DataTable
            Dim sql As String = ""
            Dim objConn As New NM_Consulta
            Dim DT As New DataTable
            sql = "select E.codigo_partida_engomadoted,E.longitud, E.codigo_plegador, E.lado," & _
            " P.codigo_plegador_pareja, E.cantidad_piezas, codigo_pieza_ini, codigo_pieza_fin from NM_PartidaEngomadoDProduccion E," & _
            " NM_ParejaPlegador P where E.codigo_partida_engomadoted *= P.codigo_partida_engomadoted " & _
            " and E.codigo_plegador *= P.codigo_plegador " & _
            " and E.codigo_partida_engomadoted='" & sCodigoPartida & "' " & _
            " order by E.fecha_creacion "
            DT = objConn.Query(sql)
            Return DT
        End Function

        Function Seek(ByVal sCodigoPartida As String, ByVal sCodPlegador As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim DT As New DataTable
            sql = "select E.codigo_partida_engomadoted,E.codigo_plegador, E.lado," & _
            " P.codigo_plegador_pareja, E.cantidad_piezas, P.codigo_plegador_pareja " & _
            " from  NM_PartidaEngomadoDProduccion E," & _
            " NM_ParejaPlegador P where E.codigo_partida_engomadoted = P.codigo_partida_engomadoted " & _
            " and E.codigo_plegador = P.codigo_plegador " & _
            " and E.codigo_partida_engomadoted='" & sCodigoPartida & "' " & _
            " and P.codigo_plegador='" & sCodPlegador & "' "
            DT = objConn.Query(sql)
            Return DT
        End Function

        Function Seek(ByVal sCodigoPartida As String, ByVal sCodPlegador As String, ByVal Busqueda As Boolean)
            Dim sql As String, objConn As New NM_Consulta
            Dim DT As New DataTable, fila As DataRow
            sql = "select E.codigo_partida_engomadoted,E.codigo_plegador, E.lado," & _
            " P.codigo_plegador_pareja, E.cantidad_piezas, P.codigo_plegador_pareja " & _
            " from  NM_PartidaEngomadoDProduccion E," & _
            " NM_ParejaPlegador P where E.codigo_partida_engomadoted = P.codigo_partida_engomadoted " & _
            " and E.codigo_plegador = P.codigo_plegador " & _
            " and E.codigo_partida_engomadoted='" & sCodigoPartida & "' " & _
            " and P.codigo_plegador='" & sCodPlegador & "' "
            DT = objConn.Query(sql)
            For Each fila In DT.Rows
                CodigoPartida = fila("codigo_partida_engomadoted")
                CodigoPlegador = fila("codigo_plegador")
                CodigoLado = fila("lado")
                CodigoPareja = fila("codigo_plegador_pareja")
            Next

        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Noviembre 2014
        'Modificado: Implementar IQ en partidas.
        '-----------------------------------------------------------
        Function EliminarPiezasPlegador(ByVal sCodigoPartida As String, ByVal sCodigoPlegador As String, _
                                        ByVal sUsuario As String) As DataTable
            Dim Conexion As AccesoDatosSQLServer
            Dim dtbResultado As DataTable
            dtbResultado = Nothing
            Try
                Dim objParametro() As Object = {"vch_CodigoPartidaEngomado", sCodigoPartida, _
                                                "vch_CodigoPlegador", sCodigoPlegador,
                                                "vch_Usuario", sUsuario
                                               }
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dtbResultado = Conexion.ObtenerDataTable("usp_nm_PartidaEngomadoDProduccion_EliminaPiezasPlegador", objParametro)
                Return dtbResultado
            Catch ex As Exception
                Throw ex
            End Try

        End Function

    End Class

End Namespace