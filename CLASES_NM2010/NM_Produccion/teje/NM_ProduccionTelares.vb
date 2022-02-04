Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
'Imports NM_Produccion.NM_Util.NM_Util

Namespace NM_Tejeduria

    Public Class NM_ProduccionTelares
        Dim BD As New NM_Consulta

        Public Usuario As String
        Public codigoTelar As String
        Public fecha As String
        Public turno As Integer
        Public tomaInicial As Double
        Public tomaFinal As Double
        Public numeroPieza As String
        Public colorBobina As String
        Public parosTrama As Double
        Public parosUrdimbre As Double
        Public intentosArranques As Integer
        Public tension_Kn As Double
        Public parosTrama_100k As Double
        Public parosUrdimbre_100k As Double
        Public revisionTelar As Integer
        Public blnUpdRevTelar As Boolean
        Private objUtil As New NM_General.Util
        Private mobjConexionProduccion As AccesoDatosSQLServer

        Sub New()
            codigoTelar = ""
            fecha = Date.Today
            turno = 0
            tomaInicial = 0
            tomaFinal = 0
            numeroPieza = ""
            colorBobina = ""
            parosTrama = 0
            parosUrdimbre = 0
            intentosArranques = 0
            tension_Kn = 0
            parosTrama_100k = 0
            parosUrdimbre_100k = 0
            blnUpdRevTelar = False
            mobjConexionProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Sub New(ByVal codigoTelar As String, ByVal fecha As String, ByVal turno As Integer)
            mobjConexionProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Seek(codigoTelar, fecha, turno)
        End Sub

        Public Sub Seek(ByVal codigoTelar As String, ByVal fecha As String, ByVal turno As Integer)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_ProduccionTelares " & _
            "WHERE codigo_telar='" & codigoTelar & "'," & _
            "AND fecha = '" & objUtil.FormatFecha(fecha) & "' " & _
            "AND turno = " & turno
            objDT = BD.Query(sql)
            ' falta!
        End Sub

        Function Exist(ByVal codigoTelar As String, ByVal fecha As String, ByVal turno As Integer) As Boolean
            Dim sql As String
            Dim objDT As New DataTable
            sql = "SELECT * FROM NM_ProduccionTelares " & _
            "WHERE codigo_telar='" & codigoTelar & "' " & _
            "AND DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 " & _
            "AND turno = " & turno
            objDT = BD.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function LoadDT(ByVal fecha As String, ByVal turno As Integer) As DataTable   'pt.toma_inicial, " & _
            Dim sql = "SELECT t.codigo_maquina, pt.fecha, pt.turno, 0 as toma_inicial, " & _
                "pt.toma_final, pt.numero_pieza, pt.color_bobina, c.Descripcion, " & _
                "pt.paros_trama, pt.paros_urdimbre, " & _
                "pt.paros_trama_100k, pt.paros_urdimbre_100k, " & _
                "t.codigo_tipo_maquina, " & _
                "pt.intentos_arranques, pt.tension_kn " & _
                "FROM NM_Telares t " & _
                "LEFT JOIN NM_ProduccionTelares pt " & _
                "ON t.codigo_maquina = pt.codigo_telar " & _
                "AND DATEDIFF(DD, pt.fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 AND pt.turno = " & turno & " " & _
                "LEFT JOIN NM_Color c " & _
                "ON pt.color_bobina = c.codigo_color " & _
                "WHERE t.flagestado = 1 " & _
                "order by t.codigo_maquina"
            Return BD.Query(sql)
        End Function

        'Function LoadDT(ByVal fecha As Date, ByVal turno As Integer) As DataTable   'pt.toma_inicial, " & _
        '    Dim sql = "SELECT t.codigo_telar, pt.fecha, pt.turno, pt2.toma_final as toma_inicial, " & _
        '        "pt.toma_final, pt.numero_pieza, pt.color_bobina, " & _
        '        "pt.paros_trama, pt.paros_urdimbre, " & _
        '        "pt.paros_trama_100k, pt.paros_urdimbre_100k " & _
        '        "FROM NM_Telares t " & _
        '        "LEFT JOIN NM_ProduccionTelares pt " & _
        '        "ON t.codigo_telar = pt.codigo_telar " & _
        '        "AND pt.fecha = '" & objUtil.FormatFecha(Fecha) & "' " & _
        '        "AND pt.turno =" & turno & " " & _
        '        "LEFT JOIN NM_ProduccionTelares pt2 " & _
        '        "ON pt.codigo_telar = pt2.codigo_telar " & _
        '        "AND pt2.fecha = (SELECT MAX (fecha) as fecha FROM NM_ProduccionTelares WHERE fecha > " & fecha & ") " & _
        '        "AND pt2.turno = (SELECT MAX (turno) as turno FROM NM_ProduccionTelares WHERE turno < " & turno & ") "
        '    Return BD.Query(sql)
        'End Function

        Function UltimaToma(ByVal fecha As String, ByVal turno As Integer, ByVal codigoTelar As String) As String
            Dim sql As String, dt As DataTable, valor As Double = 0
            Dim fila As DataRow
            objUtil.FormatFecha(fecha)
            ' Buscar la última toma realizada en días anteriores para el mismo turno
            sql = "declare @ultFecha datetime " & _
            "SET @ultFecha = (SELECT MAX (fecha) as fecha " & _
            " FROM NM_ProduccionTelares " & _
            " WHERE DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 AND codigo_telar = '" & codigoTelar & _
            "' AND turno = " & turno & ") " & _
            "SELECT toma_final as toma_inicial " & _
            "FROM NM_ProduccionTelares " & _
            "WHERE convert(datetime, fecha) = convert(datetime, @ultFecha) " & _
            "AND turno = " & turno & " " & _
            "AND codigo_telar = '" & codigoTelar & "' "
            dt = BD.Query(sql)
            For Each fila In dt.Rows
                If IsDBNull(fila("toma_inicial")) = False Then valor = fila("toma_inicial")
            Next
            Return valor
        End Function

        Function Listar() As DataTable
            Dim strSQL = "SELECT * FROM NM_ProduccionTelares"
            Return BD.Query(strSQL)
        End Function
        Function List(ByVal pFecha As String, ByVal pTurno As Integer) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dtRet As New DataTable
            sql = "select MT.codigo_maquina, PT.fecha, PT.turno, 0 as toma_inicial, " & _
            " PT.toma_final, PT.numero_pieza, PT.color_bobina, " & _
            " PT.paros_trama, PT.paros_urdimbre, PT.paros_trama_100k, " & _
            " PT.paros_urdimbre_100k, MT.codigo_tipo_maquina, " & _
            " PT.intentos_arranques, PT.tension_kn " & _
            " from NM_MA_Telares MT, NM_ProduccionTelares PT " & _
            " where PT.codigo_telar =* MT.codigo_maquina " & _
            " and DATEDIFF(DD, PT.fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and PT.turno =" & pTurno
            dtRet = objConn.Query(sql)
            Return dtRet
        End Function

        Public Sub Actualizar()
            Try
                If codigoTelar <> "" And turno > 0 Then
                    Dim strSQL As String
                    strSQL = "UPDATE NM_ProduccionTelares " & _
                            "SET toma_inicial = " & tomaInicial & ", "

                    If blnUpdRevTelar Then
                        strSQL = strSQL & "revision_telar = " & revisionTelar & ", "
                    End If

                    strSQL = strSQL & " toma_final = " & tomaFinal & ", " & _
                        "numero_pieza = '" & numeroPieza & "', " & _
                        "color_bobina = '" & colorBobina & "', " & _
                        "paros_trama = " & parosTrama & ", " & _
                        "paros_urdimbre = " & parosUrdimbre & ", " & _
                        "intentos_arranques = " & intentosArranques & ", " & _
                        "tension_kn = " & tension_Kn & ", " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_telar = '" & codigoTelar & "' " & _
                        "AND DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 " & _
                        "AND turno = " & turno
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede actualizar porque el código es inválido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Insertar() As Boolean
            Try
                If Not codigoTelar = "" Then
                    Dim strSQL = "INSERT INTO NM_ProduccionTelares " & _
                        "(codigo_telar, revision_telar, fecha, turno, " & _
                        "toma_inicial, toma_final, numero_pieza, color_bobina, " & _
                        "paros_trama, paros_urdimbre, paros_trama_100k, paros_urdimbre_100k, " & _
                        "intentos_arranques,tension_kn, usuario_creacion, fecha_creacion  ) " & _
                        "VALUES ('" & _
                        codigoTelar & "'," & revisionTelar & "," & "convert(datetime, '" & _
                        objUtil.FormatFecha(fecha) & "'), " & turno & ", " & _
                        tomaInicial & ", " & tomaFinal & ", '" & _
                        numeroPieza & "', '" & colorBobina & "', " & _
                        parosTrama & ", " & parosUrdimbre & ", " & _
                        parosTrama_100k & ", " & parosUrdimbre_100k & ", " & _
                        intentosArranques & ", " & tension_Kn & ", '" & _
                        Usuario & "', GetDate())"
                    Return BD.Execute(strSQL)
                Else
                    Return False
                    Throw New Exception("No se puede insertar porque el código es incorrecto.")
                End If
            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Public Function Eliminar(ByVal codigoTelar As String, ByVal fecha As String, ByVal turno As Integer) As Boolean

            Try
                Dim sql As String
                Dim objDT As New DataTable
                sql = "DELETE NM_ProduccionTelares " & _
                "WHERE codigo_telar='" & codigoTelar & "' " & _
                "AND DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 " & _
                "AND turno =" & turno
                'objDT = BD.Query(sql)
                BD.Execute(sql)
                Eliminar = True
            Catch ex As Exception
                Eliminar = False
                Throw ex
            End Try

        End Function


        Function ListAll(ByVal pFecha As String, ByVal pTurno As Integer) As DataSet

            Try
                'Dim util As New NM_Util.NM_Util

                Dim objParametros As Object() = {"Fecha", objUtil.convierteFecha(pFecha), "Turno", pTurno}
                ListAll = mobjConexionProduccion.ObtenerDataSet("NM_GET_PRODUCC_TELARES", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function fnc_ListarRutas(ByVal pintTipoConsulta As Int16) As DataTable
            Dim lstrError As String, ldtbResultados As DataTable
            Try
                Dim lobjParametros As Object() = { _
                "ptin_tipoconsulta", pintTipoConsulta, _
                "pvch_fechaproduccion", Strings.Right(fecha, 4) & Strings.Mid(fecha, 4, 2) & Strings.Left(fecha, 2)}
                ldtbResultados = mobjConexionProduccion.ObtenerDataTable("usp_tej_rutaregmet_listar", lobjParametros)
            Catch ex As Exception
            End Try
            Return ldtbResultados
        End Function
        'REQSIS201900041 - DG - INI
        Public Function ListarStockPeines(ByVal strPeine As String, ByVal strAncho As String) As DataTable
            Dim lstrError As String, ldtbResultados As DataTable
            Try
                Dim lobjParametros As Object() = {"vch_Peine", strPeine, "vch_Ancho", strAncho}
                ldtbResultados = mobjConexionProduccion.ObtenerDataTable("USP_CONSULTA_STOCK_PEINES_TEJEDURIA", lobjParametros)
            Catch ex As Exception
            End Try
            Return ldtbResultados
        End Function
        Public Function IngresarDatosTejeduria(ByVal strPeine As String, ByVal strAncho As String, ByVal strTipo As String, ByVal strStock As String, ByVal strEnTelar As String, ByVal strGuardado As String, ByVal strReparar As String, ByVal strCantDisp As String, ByVal strUbicacion As String, ByVal strCantTotal As String, ByVal strusuario As String) As Boolean
            Try
                Dim lobjParametros As Object() = {"vch_peine", strPeine, "vch_ancho", strAncho, "vch_tipo", strTipo, "vch_stock", strStock, "vch_telar", strEnTelar, "vch_guardado", strGuardado, "vch_reparar", strReparar, "vch_ubicacion", strUbicacion, "vch_cantdispo", strCantDisp, "vch_canttotal", strCantTotal, "vch_usuario", strusuario}
                mobjConexionProduccion.EjecutarComando("USP_INGRESAR_DATOS_STOCK_PEINES_TEJEDURIA", lobjParametros)
            Catch ex As Exception
            End Try
        End Function
        Public Function AnularDatosTejeduria(ByVal strPeine As String, ByVal strAncho As String, ByVal strTipo As String, ByVal strusuario As String) As Boolean
            Dim dt As DataTable
            Try
                Dim lobjParametros As Object() = {"vch_peine", strPeine, "vch_ancho", strAncho, "vch_tipo", strTipo, "vch_usuario", strusuario}
                mobjConexionProduccion.EjecutarComando("USP_ANULAR_DATOS_STOCK_PEINES_TEJEDURIA", lobjParametros)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function ActualizarDatosTejeduria(ByVal strPeine As String, ByVal strAncho As String, ByVal strTipo As String, ByVal strStock As String, ByVal strEnTelar As String, ByVal strGuardado As String, ByVal strReparar As String, ByVal strCantDisp As String, ByVal strUbicacion As String, ByVal strCantTotal As String, ByVal strusuario As String) As Boolean
            Try
                Dim lobjParametros As Object() = {"vch_peine", strPeine, "vch_ancho", strAncho, "vch_tipo", strTipo, "vch_stock", strStock, "vch_telar", strEnTelar, "vch_guardado", strGuardado, "vch_reparar", strReparar, "vch_ubicacion", strUbicacion, "vch_cantdispo", strCantDisp, "vch_canttotal", strCantTotal, "vch_usuario", strusuario}
                mobjConexionProduccion.EjecutarComando("USP_ACTUALIZAR_DATOS_STOCK_PEINES_TEJEDURIA", lobjParametros)
            Catch ex As Exception
            End Try
        End Function
        'REQSIS201900041 - DG - FIN
    End Class
    
End Namespace
