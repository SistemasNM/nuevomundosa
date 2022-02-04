Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaUrdidoProduccion
        Public CodigoPartida As String
        Public CodigoHilo As String
        Public Carrete As String
        Public HilosCarrete As Integer
        Public CodOperario As String
        Public CodSupervisor As String
        Public Roturas As Double
        Public RoturasMillon As Double
        Public Usuario As String
        Public Debug As String
        Public CodigoMaquina As String
        Public FechaInicio As String
        Public FechaFinal As String
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer

        Sub New()
            CodigoPartida = ""
            CodigoHilo = ""
            Carrete = ""
            HilosCarrete = 0
            CodOperario = ""
            CodSupervisor = ""
            Roturas = 0
            RoturasMillon = 0
            CodigoMaquina = ""
            FechaInicio = ""
            FechaFinal = ""
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function Lista(ByVal pstrCodigoPartidaUrdido As String, ByVal dgFormat As Boolean) As DataTable
            Dim dtDetalle As New DataTable
            CodigoPartida = pstrCodigoPartidaUrdido
            Dim lobjParametros() As Object = {"pvch_CodigoPartida", pstrCodigoPartidaUrdido.Trim}
            dtDetalle = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdido_listarxpartida_2", lobjParametros)
            Return dtDetalle
        End Function

        Public Function Lista() As DataTable
            Dim objCon As New NM_Consulta, sql As String
            Dim dtDetalle As New DataTable
            sql = "Select * " & _
            " from NM_PartidaUrdidoDProduccion " & _
            "where codigo_partida_urdido = '" & CodigoPartida & "' "
            dtDetalle = objCon.Query(sql)
            Return dtDetalle
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------

        Public Function Insert() As String
            Dim lstrGraboCorrectamente As String = "", ldtbResultado As DataTable
            Dim ltmp1 As String
            If FechaInicio.Trim.Length >= 10 Then
                ltmp1 = Left(FechaInicio, 10)
                FechaInicio = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(FechaInicio, 11, Len(FechaInicio) - 10)
            End If
            If FechaFinal.Trim.Length >= 10 Then
                ltmp1 = Left(FechaFinal, 10)
                FechaFinal = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(FechaFinal, 11, Len(FechaFinal) - 10)
            End If

            Dim lobjParametros() As Object = { _
            "pvch_Codigo_hilo", CodigoHilo, _
            "pvch_Codigo_partida_urdido", CodigoPartida, _
            "pvch_codigo_maquina", CodigoMaquina, _
            "pchr_carrete", Carrete, _
            "pint_hilos_carrete", HilosCarrete, _
            "pvch_operario", CodOperario, _
            "pvch_supervisor", CodSupervisor, _
            "pvch_usuario_creacion", Me.Usuario, _
            "pnum_roturas", Roturas, _
            "pnum_roturas_millon", RoturasMillon, _
            "pfecha_inicio", FechaInicio, _
            "pfecha_final", FechaFinal}

            ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdido_insertar_2", lobjParametros)
            If ldtbResultado.Rows.Count > 0 Then
                lstrGraboCorrectamente = CType(ldtbResultado.Rows(0).Item("ESTADO"), String)
            End If
            ldtbResultado = Nothing
            Return lstrGraboCorrectamente
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function Update() As Boolean
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Dim ltmp1 As String

            If FechaInicio.Trim.Length >= 10 Then
                ltmp1 = Left(FechaInicio, 10)
                FechaInicio = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(FechaInicio, 11, Len(FechaInicio) - 10)
            End If

            If FechaFinal.Trim.Length >= 10 Then
                ltmp1 = Left(FechaFinal, 10)
                FechaFinal = Right(ltmp1, 4) & Mid(ltmp1, 4, 2) & Left(ltmp1, 2) & Mid(FechaFinal, 11, Len(FechaFinal) - 10)
            End If

            Dim lobjParametros() As Object = { _
            "pvch_Codigo_hilo", CodigoHilo, _
            "pvch_Codigo_partida_urdido", CodigoPartida, _
            "pvch_codigo_maquina", CodigoMaquina, _
            "pchr_carrete", Carrete, _
            "pint_hilos_carrete", HilosCarrete, _
            "pvch_operario", CodOperario, _
            "pvch_supervisor", CodSupervisor, _
            "pvch_usuario_modificacion", Me.Usuario, _
            "pnum_roturas", Replace(Roturas, ",", "."), _
            "pnum_roturas_millon", Replace(RoturasMillon, ",", "."), _
            "pfecha_inicio", FechaInicio, _
            "pfecha_final", FechaFinal}

            ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdido_actualizar_2", lobjParametros)
            If ldtbResultado.Rows.Count > 0 Then
                If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                    lblnGraboCorrectamente = True
                End If
            End If
            ldtbResultado = Nothing
            Return lblnGraboCorrectamente
        End Function

        Public Sub Seek(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal sCodigoHilo As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim DT As New DataTable, fila As DataRow
            sql = ""
            sql = sql + " Select * "
            sql = sql + " from NM_PartidaUrdidoDProduccion where codigo_partida_urdido='" & sCodigoPartida & "' "
            sql = sql + " and carrete ='" & sCarrete & "' and codigo_hilo ='" & sCodigoHilo & "' "
            DT = objConn.Query(sql)
            For Each fila In DT.Rows
                Me.CodigoPartida = fila.Item("codigo_partida_urdido")
                Me.CodigoMaquina = fila.Item("codigo_maquina")
                CodigoHilo = fila.Item("codigo_hilo")
                Me.Carrete = fila.Item("carrete")
                Me.CodOperario = fila.Item("operario")
                CodSupervisor = fila.Item("supervisor")
                Me.HilosCarrete = fila.Item("hilos_carrete")
                If IsDBNull(fila.Item("roturas")) = False Then Me.Roturas = fila.Item("roturas")
                If IsDBNull(fila.Item("roturas_millon")) = False Then Me.RoturasMillon = fila.Item("roturas_millon")
            Next
        End Sub

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function Delete(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal sCodigoHilo As String) As Boolean
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Dim lobjParametros() As Object = {"pvch_Codigo_hilo", sCodigoHilo, _
            "pvch_Codigo_partida_urdido", sCodigoPartida, _
            "pchr_carrete", sCarrete}
            Try
                ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdido_eliminar_2", lobjParametros)
                If ldtbResultado.Rows.Count > 0 Then
                    If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                        lblnGraboCorrectamente = True
                    End If
                End If
            Catch ex As Exception
            Finally
                ldtbResultado = Nothing
            End Try
            Return lblnGraboCorrectamente
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function Actualiza_Fechas_PU(ByVal sCodigoPartida As String, ByVal sCodigoMaquina As String) As Boolean
            Dim lblnGraboCorrectamente As Boolean = False
            Dim ldtbResultado As DataTable
            ldtbResultado = Nothing

            Dim lobjParametros() As Object = {"pvch_Codigo_partida_urdido", sCodigoPartida, "pvch_codigo_maquina", sCodigoMaquina}
            Try
                ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdido_actualizar_fechas", lobjParametros)
                If ldtbResultado.Rows.Count > 0 Then
                    If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                        lblnGraboCorrectamente = True
                    End If
                End If
            Catch ex As Exception
            Finally
                ldtbResultado = Nothing
            End Try
            Return lblnGraboCorrectamente
        End Function

        Public Function Delete(ByVal CodigoPartida As String, ByVal sCodigoHilo As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String, codErr As Integer = 0
            Try
                If CodigoPartida <> "" AndAlso sCodigoHilo <> "" Then
                    sql = "Delete from NM_PartidaUrdidoDProduccion " & _
                    " where codigo_partida_urdido = '" & CodigoPartida & _
                    "' and codigo_hilo='" & _
                    sCodigoHilo & "' "
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetRoturas(ByVal sCodigoPartida As String, _
        ByVal sCarrete As String, ByVal sCodigoHilo As String) As Double
            Dim sql As String, objConn As New NM_Consulta
            Dim Dato1 As Double = 0, objMCalidad As New NM_PartidaUrdidoMCalidad
            Dim DT As New DataTable, fila As DataRow

            sql = "select (RoturaEmpalmeOE + RoturaEmpalmeConera + RoturaEmpalmeContinua+HiloDebil " & _
            "+ Enganche + Reserva + Pelusa + CambioCogollos + HilosCruzados + " & _
            " BobinaDanada + HiloRoto + HiloPicado + Champa + HiloArrastre " & _
            "+ otros_defectos) as total from NM_PartidaUrdidoMCalidad " & _
            " WHERE codigo_partida_urdido = '" & sCodigoPartida & "' " & _
            " and codigo_hilo = '" & sCodigoHilo & "'" & _
            " and carrete = '" & sCarrete & "' "
            DT = objConn.Query(sql)
            For Each fila In DT.Rows
                If IsDBNull(fila.Item("total")) = False Then Dato1 = fila.Item("total")
            Next
            Return Dato1
        End Function

        Public Function GetRoturasMillon(ByVal sCodigoPartida As String, _
        ByVal sCarrete As String, ByVal sCodigoHilo As String) As Double
            Dim sql As String, objConn As New NM_Consulta
            Dim objPart As New NM_PartidaUrdido(sCodigoPartida)
            Dim metros As Double
            Dim dRoturas As Double
            metros = objPart.MetrosPartida
            dRoturas = GetRoturas(sCodigoPartida, sCarrete, sCodigoHilo)
            Debug = dRoturas & "/(" & metros & "/1000)) / (" & HilosCarrete & "/ 1000)"
            RoturasMillon = (dRoturas / (metros / 1000)) / (HilosCarrete / 1000)
            Return RoturasMillon
        End Function

    End Class

End Namespace