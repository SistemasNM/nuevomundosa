Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_PartidaEngomadoDCalidad
        Dim BD As New NM_Consulta()
        Public Usuario As String

        Public Sub Insertar(ByVal codigoPartidaEngomadoTED As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String, ByVal codigoDetalleCalidad As Integer, ByVal valorDetalleCalidad As Integer)
            Dim sql As String
            Try
                If Not (codigoPartidaEngomadoTED Is Nothing AndAlso codigoPlegador Is Nothing AndAlso codigoMaestroCalidad Is Nothing AndAlso codigoDetalleCalidad = 0) Then
                    sql = "INSERT INTO NM_PartidaEngomadoDCalidad " & _
                        "(codigo_partida_engomadoted, codigo_plegador, codigo_maestro_calidad, " & _
                        "codigo_detalle_calidad, valor_detalle_calidad, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        codigoPartidaEngomadoTED & "','" & _
                        codigoPlegador & "','" & _
                        codigoMaestroCalidad & "'," & _
                        codigoDetalleCalidad & "," & _
                        valorDetalleCalidad & ",'" & _
                        Usuario & "'," & _
                        "GetDate())"
                    BD.Execute(sql)
                Else
                    Throw New Exception("No se puede insertar porque falta codigo_partida_engomadoted.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' Muestra, para cada plegador, el total de cada criterio de calidad. Devuelve la Vista completa de Calidad
        Public Function Listar(ByVal codigoPartidaEngomado As String) As DataTable
            Dim sql = "EXEC SP_NMCalidadPartidaEngomado '" & codigoPartidaEngomado & "'"
            Return BD.Query(sql)
        End Function

        ' Muestra el detalle de un criterio de calidad
        Public Function Listar(ByVal codigoPartidaEngomado As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String) As DataTable
            Dim sql = "SELECT c.codigo_detalle_calidad, c.descripcion_detalle_calidad, pec.valor_detalle_calidad " & _
                "FROM NM_DCalidad c " & _
                "LEFT JOIN NM_PartidaEngomadoDCalidad pec " & _
                "ON pec.codigo_maestro_calidad = c.codigo_maestro_calidad " & _
                "AND pec.codigo_detalle_calidad = c.codigo_detalle_calidad " & _
                "AND pec.codigo_partida_engomadoted ='" & codigoPartidaEngomado & "' " & _
                "AND pec.codigo_plegador ='" & codigoPlegador & "' " & _
                "WHERE c.codigo_maestro_calidad ='" & codigoMaestroCalidad & "'"
            Return BD.Query(sql)
        End Function

        Public Function LoadDT(ByVal codigoPartidaEngomado As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String) As DataTable
            Dim sql = "SELECT c.codigo_detalle_calidad, c.descripcion_detalle_calidad, pec.valor_detalle_calidad " & _
                "FROM NM_DCalidad c " & _
                "LEFT JOIN NM_PartidaEngomadoDCalidad pec " & _
                "ON pec.codigo_maestro_calidad = c.codigo_maestro_calidad " & _
                "AND pec.codigo_detalle_calidad = c.codigo_detalle_calidad " & _
                "AND pec.codigo_partida_engomadoted ='" & codigoPartidaEngomado & "' " & _
                "AND pec.codigo_plegador ='" & codigoPlegador & "' " & _
                "WHERE c.codigo_maestro_calidad ='" & codigoMaestroCalidad & "'"
            Return BD.Query(sql)
        End Function

        Public Sub Actualizar(ByVal codigoPartidaEngomadoTED As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String, ByVal codigoDetalleCalidad As Integer, ByVal valorDetalleCalidad As Integer)
            Try
                If codigoPartidaEngomadoTED <> "" And codigoPlegador <> "" And codigoMaestroCalidad <> "" And codigoDetalleCalidad > 0 Then
                    Dim sql = "UPDATE NM_PartidaEngomadoDCalidad " & _
                        "SET valor_detalle_calidad = " & valorDetalleCalidad & ", " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = " & Date.Today & " " & _
                        "WHERE codigo_partida_engomadoted = '" & codigoPartidaEngomadoTED & "' " & _
                        "AND codigo_plegador = '" & codigoPlegador & "' " & _
                        "AND codigo_maestro_calidad = '" & codigoMaestroCalidad & "' " & _
                        "AND codigo_detalle_calidad = " & codigoDetalleCalidad & ""
                    BD.Execute(sql)
                Else
                    Throw New Exception("No se puede actualizar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Exist(ByVal codigoPartidaEngomadoTED As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String, ByVal codigoDetalleCalidad As Integer) As Boolean
            Dim objConn As New NM_Consulta, dtCal As New DataTable
            Dim sql As String
            Try
                If codigoPartidaEngomadoTED <> "" And codigoPlegador <> "" And codigoMaestroCalidad <> "" And codigoDetalleCalidad > 0 Then
                    sql = "Select * from NM_PartidaEngomadoDCalidad " & _
                    "WHERE codigo_partida_engomadoted = '" & codigoPartidaEngomadoTED & "' " & _
                    "AND codigo_plegador = '" & codigoPlegador & "' " & _
                    "AND codigo_maestro_calidad = '" & codigoMaestroCalidad & "' " & _
                    "AND codigo_detalle_calidad = " & codigoDetalleCalidad & ""
                    dtCal = objConn.Query(sql)
                    Return (dtCal.Rows.Count > 0)
                Else
                    Return False
                    Throw New Exception("No se puede actualizar porque el código es inválido.")
                End If
            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Public Sub Eliminar(ByVal codigoPartidaEngomadoTED As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String, ByVal codigoDetalleCalidad As Integer)
            Try
                If Not codigoPlegador = "" Then
                    Dim sql = "DELETE FROM NM_PartidaEngomadoDCalidad " & _
                        "WHERE codigo_partida_engomadoted = '" & codigoPartidaEngomadoTED & "' " & _
                        "AND codigo_plegador = '" & codigoPlegador & "' " & _
                        "AND codigo_maestro_calidad = '" & codigoMaestroCalidad & "' " & _
                        "AND codigo_detalle_calidad = " & codigoDetalleCalidad & ""
                    BD.Execute(sql)
                Else
                    Throw New Exception("No se puede eliminar porque el código no es válido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Eliminar(ByVal codigoPartidaEngomadoTED As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String)
            Try
                If Not codigoPlegador = "" Then
                    Dim sql = "DELETE FROM NM_PartidaEngomadoDCalidad " & _
                        "WHERE codigo_partida_engomadoted = '" & codigoPartidaEngomadoTED & "' " & _
                        "AND codigo_plegador = '" & codigoPlegador & "' " & _
                        "AND codigo_maestro_calidad = '" & codigoMaestroCalidad & "'"
                    BD.Execute(sql)
                Else
                    Throw New Exception("No se puede eliminar porque el código no es válido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

End Namespace