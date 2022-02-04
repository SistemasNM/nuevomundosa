Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_InventarioTelaProcesoD

        Dim BD As New NM_Consulta
        Public Fecha As Date
        Public codigo_telar As String
        Public revision_telar As Integer
        Public centro_costo As String
        Public codigo_partida As String
        Public codigo_plegador As String
        Public UltNumeroPieza_entregada As String
        Public codigo_hilo1 As String
        Public numero_conos1 As Integer
        Public codigo_hilo2 As String
        Public numero_conos2 As Integer
        Public codigo_hilo3 As String
        Public numero_conos3 As Integer
        Public pasadas_avance As Integer
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Public Function Insertar() As Boolean
            Dim strSQL As String, objConn As New NM_Consulta
		'faltaria agregar codigo de partida en la consulta
            Try
                strSQL = "INSERT INTO NM_InventarioTelaProcesoD " & _
                    "(fecha, centro_costo, codigo_maquina, revision_maquina, codigo_plegador, codigo_partida, " & _
                    " UltNumeroPieza_entregada, " & _
                    "codigo_hilo1, numero_conos1, codigo_hilo2, numero_conos2, " & _
                    "codigo_hilo3, numero_conos3, pasadas_avance, " & _
                    "usuario_modificacion, fecha_modificacion) " & _
                    "VALUES (convert(datetime,'" & _
                    objUtil.FormatFecha(Fecha) & "'),'" & centro_costo & "', '" & _
                    codigo_telar & "'," & revision_telar & ",'" & _
                    codigo_plegador & "','" & codigo_partida & "','" & UltNumeroPieza_entregada & "','" & _
                    codigo_hilo1 & "'," & numero_conos1 & ",'" & codigo_hilo2 & "'," & _
                    numero_conos2 & ",'" & codigo_hilo3 & "'," & numero_conos3 & "," & _
                    pasadas_avance & ",'" & Usuario & "'," & "GetDate())"
                Return objConn.Execute(strSQL)
            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Public Function Update() As Boolean
            Dim strSQL As String, objConn As New NM_Consulta
		'Faltaria agregar codigo de partida
            Try
                strSQL = "Update NM_InventarioTelaProcesoD set " & _
                " codigo_plegador = '" & codigo_plegador & "', " & _
                " codigo_partida = '" & codigo_partida & "', " & _
                " UltNumeroPieza_entregada = '" & UltNumeroPieza_entregada & "', " & _
                " codigo_hilo1 = '" & codigo_hilo1 & "', numero_conos1 = '" & numero_conos1 & "', " & _
                " codigo_hilo2 = '" & codigo_hilo2 & "', numero_conos2 = '" & numero_conos2 & "', " & _
                " codigo_hilo3 = '" & codigo_hilo3 & "', numero_conos3 = '" & numero_conos3 & "', " & _
                " pasadas_avance = '" & pasadas_avance & "', usuario_modificacion = '" & _
                Usuario & "', fecha_modificacion = getdate() " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and" & _
                " centro_costo = '" & centro_costo & "' and codigo_maquina = '" & _
                codigo_telar & "' and codigo_plegador = '" & codigo_plegador & "' "
                Return objConn.Execute(strSQL)
            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Function Listar() As DataTable
            Dim strSQL = "SELECT t.codigo_telar " & _
                "FROM NM_MA_Telares t " & _
                "LEFT JOIN NM_InventarioTelaProcesoD itp " & _
                "ON t.codigo_maquina = itp.codigo_maquina "
            Return BD.Query(strSQL)
        End Function

        Function Listar(ByVal pfecha As Date, ByVal pcentrocosto As String) As DataTable
            Dim SQL As String
            Dim objConn As New NM_Consulta
		'SQL = "SELECT t.codigo_maquina, t.revision_maquina, itp.centro_costo, itp.codigo_plegador, itp.codigo_partida, itp.UltNumeroPieza_entregada, " & _
            SQL = "SELECT t.codigo_maquina, t.revision_maquina, itp.centro_costo, itp.codigo_plegador, itp.UltNumeroPieza_entregada, " & _
                               "itp.codigo_hilo1, itp.numero_conos1, itp.codigo_hilo2, itp.numero_conos2, " & _
                               "itp.codigo_hilo3, itp.numero_conos3, itp.pasadas_avance, itp.codigo_partida " & _
                               "FROM NM_MA_Telares t " & _
                               " INNER JOIN NM_InventarioTelaProcesoD itp " & _
                               "  ON t.codigo_maquina = itp.codigo_maquina " & _
                               "AND DATEDIFF(DD, itp.fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0  and ITP.centro_costo = '" & _
                               pcentrocosto & "' "
            Return objConn.Query(SQL)
        End Function

        Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String, ByVal pCodigoTelar As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_InventarioTelaProcesoD " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 " & _
            "AND codigo_maquina = '" & pCodigoTelar & "' and centro_costo ='" & pCentroCosto & "' "
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String, ByVal pCodigoTelar As String, ByVal pCodigoPlegador As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_InventarioTelaProcesoD " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 " & _
            "AND codigo_maquina = '" & pCodigoTelar & "' and centro_costo ='" & _
            pCentroCosto & "' and codigo_plegador = '" & pCodigoPlegador & "' "
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Public Function Delete(ByVal pFecha As String, ByVal pCentroCosto As String, ByVal pCodigoMaquina As String, ByVal pPlegador As String, ByVal pUltNumPieza As String, ByVal pCodhilo1 As String, ByVal pCodhilo2 As String, ByVal pCodhilo3 As String)
            Dim objGen As New NM_Consulta, sql As New System.Text.StringBuilder
            Try
                sql.Append("delete from NM_InventarioTelaProcesoD ")
                sql.Append("where fecha='" & pFecha & "' and codigo_maquina='" & pCodigoMaquina & "' and ")
                sql.Append("codigo_plegador='" & pPlegador & "' and UltNumeroPieza_entregada='" & pUltNumPieza & "' and ")
                sql.Append("codigo_hilo1='" & pCodhilo1 & "' and codigo_hilo2='" & pCodhilo2 & "' and codigo_hilo3='" & pCodhilo3 & "' and centro_costo='" & pCentroCosto & "'")
                objGen.Execute(sql.ToString)
            Catch
                Return False
            End Try
        End Function

    End Class


End Namespace