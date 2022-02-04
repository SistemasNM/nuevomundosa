Imports NM_General.NM_BaseDatos

Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_IQEngomadoYTED
        Implements IDisposable

        Public Codigo_insumo_quimico As String
        Public Codigo_partida_engomadoted As String
        Public Codigo_receta As String
        Public Codigo_fase As String
        Public Revision_receta As Integer
        Public Kilos As Double
        Public Usuario As String
        Public num_Item As Integer
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer

        Sub New()
            Codigo_insumo_quimico = ""
            Codigo_partida_engomadoted = ""
            Codigo_receta = ""
            Codigo_fase = ""
            Revision_receta = 0
            Kilos = 0
            num_Item = 0

            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_IQEngomadoYTED (" & _
                "codigo_insumo_quimico, codigo_partida_engomadoted," & _
                " codigo_receta, codigo_area, codigo_fase, num_Item, revision_receta, kilos, " & _
                " usuario_creacion, fecha_creacion) values('" & _
                Me.Codigo_insumo_quimico & "', '" & Me.Codigo_partida_engomadoted & _
                "', '" & Me.Codigo_receta & "', 'ENGTED', '" & Me.Codigo_fase & _
                "', " & Me.num_Item & "," & Me.Revision_receta & ", " & Me.Kilos & _
                ",'" & Usuario & "', getdate())"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Add2() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                Dim objparametros As Object() = {"p_var_codigo_partida_engomadoted", Me.Codigo_partida_engomadoted, _
                                                 "p_var_codigo_receta", Me.Codigo_receta, _
                                                 "p_num_revision_receta", Me.Revision_receta, _
                                                 "p_num_item", Me.num_Item, _
                                                 "p_num_codigo_fase", Me.Codigo_fase, _
                                                 "p_var_codigo_insumo_quimico", Me.Codigo_insumo_quimico, _
                                                 "p_num_kilos", Me.Kilos, _
                                                 "p_var_codigo_area", "ENGTED", _
                                                 "p_var_usuario", Usuario}

                m_sqlDtAccProduccion.EjecutarComando("usp_Ins_NM_IQEngomadoYTED", objparametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_IQEngomadoYTED set " & _
                " kilos = " & Kilos & ", " & _
                " usuario_modificacion='" & Usuario & "', " & _
                " fecha_modificacion = getdate() " & _
                " where codigo_insumo_quimico = '" & Me.Codigo_insumo_quimico & "' " & _
                " and codigo_partida_engomadoted ='" & Me.Codigo_partida_engomadoted & "' " & _
                " and codigo_receta = '" & Me.Codigo_receta & "' " & _
                " and revision_receta = " & Me.Revision_receta & _
                " and codigo_area = 'ENGTED' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update2() As Boolean
            Dim sql As String, objConn As New NM_Consulta

            Try
                Dim objparametros As Object() = {"p_var_codigo_partida_engomadoted", Me.Codigo_partida_engomadoted, _
                                                 "p_var_codigo_receta", Me.Codigo_receta, _
                                                 "p_num_revision_receta", Me.Revision_receta, _
                                                 "p_var_codigo_insumo_quimico", Me.Codigo_insumo_quimico, _
                                                 "p_num_kilos", Kilos, _
                                                 "p_var_usuario", Usuario}

                m_sqlDtAccProduccion.EjecutarComando("usp_Upd_NM_IQEngomadoYTED", objparametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function Delete(ByVal sCodigoIQ As String, ByVal sCodigoPartida As String, _
        ByVal sCodigoReceta As String, ByVal nRevReceta As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_IQEngomadoYTED " & _
                " where codigo_insumo_quimico = '" & sCodigoIQ & "' " & _
                " and codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                " and codigo_receta = '" & sCodigoReceta & "' " & _
                " and revision_receta = " & nRevReceta & _
                " and codigo_area = 'ENGTED' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete2(ByVal sCodigoIQ As String, ByVal sCodigoPartida As String, _
        ByVal sCodigoReceta As String, ByVal nRevReceta As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                Dim objparametros As Object() = {"p_var_codigo_partida_engomadoted", sCodigoPartida, _
                                                 "p_var_codigo_receta", sCodigoReceta, _
                                                 "p_num_revision_receta", nRevReceta, _
                                                 "p_var_codigo_insumo_quimico", sCodigoIQ}

                m_sqlDtAccProduccion.EjecutarComando("usp_Del_NM_IQEngomadoYTED", objparametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function List(ByVal sCodigoPartida As String, ByVal nCodFase As Integer) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_IQEngomadoYTED " & _
            " where codigo_partida_engomadoted='" & sCodigoPartida & _
            "' and codigo_fase = " & nCodFase & " "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List2(ByVal sCodigoPartida As String, ByVal nCodFase As Integer) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            'sql = "Select * from NM_IQEngomadoYTED " & _
            '" where codigo_partida_engomadoted='" & sCodigoPartida & _
            '"' and codigo_fase = " & nCodFase & " "
            'dt = objConn.Query(sql)
            'Return dt
            Try
                Dim objParametros() As Object = {"p_var_codigo_partida_engomadoted", sCodigoPartida, _
                                                 "p_num_codigo_fase", nCodFase}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("usp_qry_ObtenerIQTED", objParametros)

                Return dt

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_IQEngomadoYTED "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function AddReceta(ByVal sCodigoPartida As String, ByVal sCodigoReceta As String, _
        ByVal nRevReceta As Integer, ByVal nFase As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_IQEngomadoYTED (" & _
                " codigo_partida_engomadoted, codigo_receta, revision_receta, num_Item, codigo_area, " & _
                " codigo_insumo_quimico, kilos, codigo_fase, " & _
                " usuario_creacion, fecha_creacion)  (" & _
                " SELECT '" & sCodigoPartida & "', codigo_receta,revision_receta," & Me.num_Item & ",'ENGTED', " & _
                " codigo_insumo_quimico,0," & nFase & ",'" & Usuario & "',getdate() " & _
                " FROM NM_RecetaInsumoQuimico RIQ " & _
                " where codigo_receta = '" & sCodigoReceta & "' " & _
                " AND REVISION_RECETA=" & nRevReceta & ") "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function DeleteReceta(ByVal sCodigoPartida As String, ByVal sCodigoReceta As String, _
         ByVal nRevReceta As Integer, ByVal nFase As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_IQEngomadoYTED " & _
                " where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                " and codigo_receta = '" & sCodigoReceta & "' " & _
                " and revision_receta = " & nRevReceta
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function UpdateKilos(ByVal sCodigoPartida As String, ByVal sCodigoReceta As String, _
         ByVal nRevReceta As Integer, ByVal nCantidad As Double) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Update NM_IQEngomadoYTED set " & _
            " kilos = " & nCantidad & "/(select count(*) FROM NM_RecetaInsumoQuimico RIQ " & _
            " where codigo_receta = '" & sCodigoReceta & "' AND REVISION_RECETA= " & nRevReceta & ")" & _
            " where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
            " and codigo_receta = '" & sCodigoReceta & "' " & _
            " and revision_receta = " & nRevReceta & " "
            Return objConn.Execute(sql)
        End Function

        Function UpdateKilosPreTratamiento(ByVal sCodigoPartida As String, ByVal sCodigoReceta As String, _
         ByVal nRevReceta As Integer, ByVal nCantidad As Double) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Update NM_IQEngomadoYTED set " & _
            " kilos = (" & nCantidad & " * 1.53 /2) " & _
            " where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
            " and codigo_receta = '" & sCodigoReceta & "' " & _
            " and revision_receta = " & nRevReceta & " AND codigo_insumo_quimico = '030101010054' "
            Return objConn.Execute(sql)
        End Function

        Function UpdateKilosTenido(ByVal sCodigoPartida As String, ByVal sCodigoReceta As String, _
         ByVal nRevReceta As Integer, ByVal nCantidad As Double, ByVal nHidroSulfito As Double, ByVal nSoda100 As Double) As Boolean
            Dim sql As String, objConn As New NM_Consulta, objConn2 As New NM_Consulta
            Try
                'Calculando Soda Caustica
                'sql = "Update NM_IQEngomadoYTED set " & _
                '" kilos = convert(real,(" & " select avg((" & nSoda100 & "/velocidad)*(total_engomado* 1.53/2)/ 1000) " & _
                '" from NM_PartidaEngomadoYTED PT, NM_TED MT, NM_IQEngomadoyTED RT, NM_RecetaInsumoQuimico RIQ " & _
                '" where PT.codigo_ted = MT.codigo_ted " & _
                '" and PT.revision_ted = MT.revision_ted " & _
                '" and RT.codigo_partida_engomadoted = PT.codigo_partida_engomadoted " & _
                '" and RT.codigo_fase = 2 " & _
                '" and RIQ.codigo_receta = RT.codigo_receta " & _
                '" and RIQ.revision_receta = RT.revision_receta " & _
                '" and RIQ.codigo_insumoquimico = '030101010054' " & _
                '" and PT.codigo_partida_engomadoted = '" & sCodigoPartida & "' " & _
                '" and MT.flagestado = 1 and RT.codigo_receta = '" & sCodigoReceta & _
                '"' and RT.revision_receta=" & nRevReceta & "))" & _
                '" where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                '" and codigo_receta = '" & sCodigoReceta & "' " & _
                '" and revision_receta = " & nRevReceta & " AND codigo_insumo_quimico = '030101010054' "
                'objConn.Execute(sql)

                ''Calculando Hidrosulfito
                'sql = "Update NM_IQEngomadoYTED set " & _
                '" kilos = convert(real,(" & " select AVG((" & nHidroSulfito & ")/velocidad*total_engomado/1000) " & _
                '" from NM_PartidaEngomadoYTED PT, NM_TED MT, NM_IQEngomadoyTED RT, NM_RecetaInsumoQuimico RIQ " & _
                '" where PT.codigo_ted = MT.codigo_ted " & _
                '" and PT.revision_ted = MT.revision_ted " & _
                '" and RT.codigo_partida_engomadoted = PT.codigo_partida_engomadoted " & _
                '" and RT.codigo_fase = 2 " & _
                '" and RIQ.codigo_receta = RT.codigo_receta " & _
                '" and RIQ.revision_receta = RT.revision_receta " & _
                '" and RIQ.codigo_insumoquimico = '030101010037' " & _
                '" and PT.codigo_partida_engomadoted = '" & sCodigoPartida & "' " & _
                '" and MT.flagestado = 1 and RT.codigo_receta = '" & sCodigoReceta & _
                '"' and RT.revision_receta=" & nRevReceta & "))" & _
                '" where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                '" and codigo_receta = '" & sCodigoReceta & "' " & _
                '" and revision_receta = " & nRevReceta & " AND codigo_insumo_quimico = '030101010037' "
                'objConn.Execute(sql)

                'Calculando otros IQ
                '" and codigo_insumoquimico not in ('030101010037', '030101010054') " & _

                Dim dtIQT As New DataTable, fila As DataRow
                sql = "select AVG(" & nCantidad & " * concentracion/1000) as valor, codigo_insumo_quimico " & _
                " FROM NM_RecetaInsumoQuimico RIQ " & _
                " where codigo_receta = '" & sCodigoReceta & "' " & _
                " and revision_receta = " & nRevReceta & " and codigo_area = 'ENGTED' " & _
                " GROUP BY codigo_insumo_quimico "
                dtIQT = objConn.Query(sql)
                For Each fila In dtIQT.Rows
                    sql = "Update NM_IQEngomadoYTED set " & _
                                " kilos = convert(real, " & fila("valor") & ") " & _
                                " where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                                " and codigo_receta = '" & sCodigoReceta & "' " & _
                                " and revision_receta = " & nRevReceta & " AND codigo_insumo_quimico = '" & _
                                fila("codigo_insumo_quimico") & "' "
                    objConn2.Execute(sql)
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function UpdateKilosEngomado(ByVal sCodigoPartida As String, ByVal sCodigoReceta As String, _
         ByVal nRevReceta As Integer, ByVal nCantidad As Double, ByVal nLB330 As Double) As Boolean
            Dim sql, sql_2 As String, objConn As New NM_Consulta, objConn2 As New NM_Consulta, objConn3 As New NM_Consulta
            Try
                'Calculando LB330
                'sql = "Update NM_IQEngomadoYTED set " & _
                '" kilos = (" & " select avg(" & nLB330 & "*total_engomado/ 1000) " & _
                '" from NM_PartidaEngomadoYTED PT, NM_TED MT, NM_IQEngomadoyTED RT, NM_RecetaInsumoQuimico RIQ " & _
                '" where PT.codigo_ted = MT.codigo_ted " & _
                '" and PT.revision_ted = MT.revision_ted " & _
                '" and RT.codigo_partida_engomadoted = PT.codigo_partida_engomadoted " & _
                '" and RT.codigo_fase = 2 " & _
                '" and RIQ.codigo_receta = RT.codigo_receta " & _
                '" and RIQ.revision_receta = RT.revision_receta " & _
                '" and RIQ.codigo_insumoquimico = '030102330045' " & _
                '" and PT.codigo_partida_engomadoted = '" & sCodigoPartida & "' " & _
                '" and MT.flagestado = 1 and RT.codigo_receta = '" & sCodigoReceta & _
                '"' and RT.revision_receta=" & nRevReceta & ")" & _
                '" where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                '" and codigo_receta = '" & sCodigoReceta & "' " & _
                '" and revision_receta = " & nRevReceta & " AND codigo_insumo_quimico = '030102330045' "
                'objConn.Execute(sql)

                'Calculando otros IQ
                '" and codigo_insumoquimico not in ('030102330045') " & _
                Dim dtIQT As New DataTable, fila As DataRow
                sql = "select AVG(" & nCantidad & " * concentracion/1000) as valor, codigo_insumo_quimico " & _
                " FROM NM_RecetaInsumoQuimico RIQ " & _
                " where codigo_receta = '" & sCodigoReceta & "' " & _
                " and revision_receta = " & nRevReceta & " and codigo_area = 'ENGTED' " & _
                " GROUP BY codigo_insumo_quimico "
                dtIQT = objConn.Query(sql)
                For Each fila In dtIQT.Rows
                    sql = "Update NM_IQEngomadoYTED set " & _
                                " kilos = " & fila("valor") & " " & _
                                " where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                                " and codigo_receta = '" & sCodigoReceta & "' " & _
                                " and revision_receta = " & nRevReceta & " AND codigo_insumo_quimico = '" & _
                                fila("codigo_insumo_quimico") & "' "
                    objConn2.Execute(sql)                    
                Next
                'sql_2 = "Update NM_DatosRecetaTED set " & _
                '                " cantidad_iq1 = " & nLB330 & " " & _
                '                " where codigo_partida_engomadoted ='" & sCodigoPartida & "' " & _
                '                " and codigo_receta = '" & sCodigoReceta & "' " & _
                '                " and revision_receta = " & nRevReceta & " AND codigo_fase = 3 AND codigo_iq1 <> '' AND codigo_iq1 = '030102140011'"
                'objConn3.Execute(sql_2)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccProduccion.Dispose()
        End Sub

    End Class
End Namespace