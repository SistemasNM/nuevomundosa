Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaEngomadoCorrelativo
        Public correlativo As String
        Public codigo_partida_engomadoted As String
        Public plegador As String
        Public usuario_creacion As String
        Public fecha_creacion As Date
        Public usuario_modificacion As String
        Public fecha_modificacion As Date
        Public debug As String
        Private Const TED = 1
        Private Const ENGOMADO = 3
        Private DB As New NM_Consulta()

        Private m_sqlDtProduccion As AccesoDatosSQLServer

        Sub New()
            correlativo = ""
            codigo_partida_engomadoted = ""
            plegador = ""
            usuario_creacion = ""
            fecha_creacion = Date.Today
            usuario_modificacion = ""
            fecha_modificacion = Date.Today
        End Sub

        Private Function generaCorrelativo(ByVal TipoPartida As Integer) As String
            Dim result As String
            Dim i As Integer

            Dim ceros As String = ""
            Dim agno As String = CInt(Date.Today.Year())
            Dim num As Integer = GetLastNumero(TipoPartida, agno)
            Dim l As Integer = CStr(num).Length
            If l <= 6 Then
                For i = 1 To 6 - l
                    ceros = ceros & "0"
                Next i
                result = agno.Chars(2) & agno.Chars(3) & ceros & CStr(num)
                If TipoPartida = TED Then
                    Return "D" & result
                End If
                If TipoPartida = ENGOMADO Then
                    Return "C" & result
                End If
            Else
                Throw New Exception("Se llegó al límite de piezas")
            End If
        End Function

        Private Function generaCorrelativo(ByVal TipoPartida As Integer, ByVal Anno As Integer) As String
            Dim result As String
            Dim i As Integer

            Dim ceros As String = ""
            Dim agno As String = CInt(Anno)
            Dim num As Integer = GetLastNumero(TipoPartida, Anno)
            Dim l As Integer = CStr(num).Length
            If l <= 6 Then
                For i = 1 To 6 - l
                    ceros = ceros & "0"
                Next i
                result = agno.Chars(2) & agno.Chars(3) & ceros & CStr(num)
                If TipoPartida = TED Then
                    Return "D" & result
                End If
                If TipoPartida = ENGOMADO Then
                    Return "C" & result
                End If
            Else
                Throw New Exception("Se llegó al límite de piezas")
            End If
        End Function

        Public Function GetLastNumero(ByVal pTipo As String, ByVal Anno As String) As Integer
            Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"TIPO", pTipo, "ANNO", Anno}
            Return CType(objADSql.ObtenerValor("PR_NM_GET_ENGOMADO_CORRELATIVO_PIEZA", objParametros), Integer)
        End Function

        Public Function insertar() As Integer
            Dim strsql As String
            strsql = "INSERT INTO NM_PartidaEngomadoCorrelativo " & _
            " (correlativo,codigo_partida_engomadoted, codigo_plegador, " & _
            " usuario_creacion, fecha_creacion) values(" & _
            "'" & correlativo & "','" & codigo_partida_engomadoted & "'," & _
            "'" & plegador & "','" & usuario_creacion & "',GetDate())"
            debug = strsql
            Try
                DB.Execute(strsql)
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Sub delete(ByVal pcorrelativo)
            Dim strsql As String
            strsql = "DELETE FROM NM_PartidaEngomadoCorrelativo where correlativo = '" & pcorrelativo & "'"
            DB.Execute(strsql)
        End Sub

        Public Sub delete(ByVal pCodigo_partida_engomadoted As String, ByVal pcodigo_plegador As String)
            Dim strsql As String
            strsql = "DELETE FROM NM_PartidaEngomadoCorrelativo where Codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and "
            strsql = strsql & "codigo_plegador ='" & pcodigo_plegador & "'"
            DB.Execute(strsql)
        End Sub

        Public Function DeleteByPartida(ByVal pCodigo_partida_engomadoted As String)
            Dim strsql As String
            strsql = "DELETE FROM NM_PartidaEngomadoCorrelativo " & _
            " where Codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' "
            Return DB.Execute(strsql)
        End Function

        Public Sub SeekPieza(ByVal pcorrelativo As String)
            Dim tabla As New DataTable
            Dim fila As DataRow
            Dim strsql As String
            strsql = "SELECT * FROM NM_PartidaEngomadoCorrelativo where correlativo = '" & pcorrelativo & "'"
            tabla = DB.Query(strsql)
            For Each fila In tabla.Rows
                correlativo = fila("correlativo")
                codigo_partida_engomadoted = fila("codigo_partida_engomadoted")
                plegador = fila("plegador")
                usuario_creacion = fila("usuario_creacion")
                fecha_creacion = fila("fecha_creacion")
                usuario_modificacion = fila("usuario_modificacion")
                fecha_modificacion = fila("fecha_modificacion")
            Next
        End Sub

        Public Function listar() As DataTable
            Dim db As New NM_Consulta
            Return db.getData("NM_PartidaEngomadoCorrelativo")
        End Function

        Function List(ByVal sCodigoPartida As String, ByVal sCodPlegador As String) As DataTable
            Dim sql As String, dt As New DataTable, objConn As New NM_Consulta
            sql = "select * from NM_PartidaEngomadoCorrelativo " & _
            " where codigo_plegador='" & sCodPlegador & "' " & _
            " and codigo_partida_engomadoted = '" & sCodigoPartida & "' " & _
            " order by correlativo "
            dt = objConn.Query(sql)
            Return dt
        End Function

        'Descripción    : Trae las piezas por partida y plegador y otra condición dada por el campo tipo.
        'Autor          : Jorge Romaní
        'Fecha Creación : 28-10-2004
        Function ListaPiezas(ByVal sCodigoPartida As String, ByVal sCodPlegador As String, ByVal sTipo As String) As DataTable
            Try
                Dim objADSql As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"K_VC_CODIGO_PARTIDA", sCodigoPartida, "K_VC_CODIGO_PLEGADOR", sCodPlegador, "K_VC_TIPO", sTipo}

                Dim dtbEntidad As New DataTable

                dtbEntidad = objADSql.ObtenerDataTable("TEL_SP_PIEZAS_ENGOMADO", objParametros)

                Return dtbEntidad
            Catch ex As Exception

            End Try
        End Function

        Function ListByArticulo(ByVal sCodigoArticulo As String, ByVal sCodPlegador As String) As DataTable
            Dim sql As String, dt As New DataTable, objConn As New NM_Consulta
            sql = "select EC.*  " & _
            " from NM_PartidaEngomadoyTED E, NM_ParticionPartidas P,  " & _
            " NM_PartidaUrdido U, NM_Tela T , NM_PartidaEngomadoCorrelativo EC " & _
            " where E.codigo_partida_urdido = P.codigo_sub_partida_urdido " & _
            " and P.codigo_partida_urdido = U.codigo_partida_urdido  " & _
            " and E.codigo_partida_engomadoted = EC.codigo_partida_engomadoted " & _
            " and T.codigo_urdimbre = U.codigo_urdimbre  " & _
            " and T.codigo_tela='" & Trim(sCodigoArticulo) & "' " & _
            " and EC.codigo_plegador='" & sCodPlegador & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function validarNumPieza(ByVal pCorrelativo As String) As Boolean

            Dim tabla As DataTable
            Dim strsql As String
            strsql = "SELECT * FROM NM_PartidaEngomadoCorrelativo WHERE correlativo ='" & pCorrelativo & "'"
            tabla = DB.Query(strsql)
            If tabla.Rows.Count >= 1 Then
                validarNumPieza = True
            Else
                validarNumPieza = False
            End If

        End Function

        Public Function Exist(ByVal pCodigo_partida_engomadoted As String, ByVal pcodigo_plegador As String) As Boolean
            Dim strsql As String, dtCorr As New DataTable
            strsql = "Select * FROM NM_PartidaEngomadoCorrelativo " & _
            " where Codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & _
            "' and codigo_plegador ='" & pcodigo_plegador & "'"
            dtCorr = DB.Query(strsql)
            Return (dtCorr.Rows.Count > 0)
        End Function

        Public Function ActualizarPiezaIniFin(ByVal pCodigo_partida_engomadoted As String) As Boolean
            Try
                Dim strsql As String
                strsql = " execute usp_tej_ActualizarPieza '" & pCodigo_partida_engomadoted & "'"
                DB.Execute(strsql)
                ActualizarPiezaIniFin = True
            Catch ex As Exception
                ActualizarPiezaIniFin = False
            End Try
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function fGenerarPiezas(ByVal strPartidaEngomadoted As String, ByVal strUsuario As String) As Boolean
            Try
                m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim oParametros() As Object = {"vch_CodEngomadoted", strPartidaEngomadoted, _
                                                "vch_Usuario", strUsuario}
                m_sqlDtProduccion.EjecutarComando("usp_tej_CreacionPiezas_2", oParametros)
                fGenerarPiezas = True

            Catch ex As Exception
                fGenerarPiezas = False
            End Try

        End Function

        'REQSIS201700007 - DG - INI - 18/12/2017
        Public Function ObtenerCantidadPiezasPorPartidaPlegador(ByVal strPartida As String, ByVal strPlegador As String) As DataTable
            Dim dt As DataTable
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pvch_Partida", strPartida, "pvch_Plegador", strPlegador}
            Try
                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dt = Conexion.ObtenerDataTable("usp_Obtener_Cantidad_Piezas_Por_Partida_Plegador", objParametro)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        'REQSIS201700007 - DG - FIN - 18/12/2017
    End Class
End Namespace