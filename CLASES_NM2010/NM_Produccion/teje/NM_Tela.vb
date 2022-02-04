Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_Tela
        Public usuario As String
        Private BD As New NM_Consulta()

        Public codigo_tela As String
        Public revision_tela As Integer
        Public tipo As String
        Public codigo_tipo_maquina As String
        Public ligamento As String
        Public numero_peine As Double
        Public ancho_peine As Double
        Public pasadas_pulgada As Double
        Public pasadas_centimetro As Double
        Public ancho_crudo As Double
        Public hilos_pulgada_tela As Double
        Public hilos_centimetro_tela As Double
        Public hilos_diente As Double
        Public encogimiento_urdimbre As Double
        Public encogimiento_trama As Double
        Public hilos_pulgada_peine As Double
        Public hilos_centimetro_peine As Double
        Public numero_cuadros As Integer
        Public eficiencia_teorica As Double
        Public eficiencia_real As Double
        Public coeficiente_densidad_urdido As Double
        Public coeficiente_densidad_trama As Double
        Public factor_cobertura_urdimbre As Double
        Public factor_cobertura_trama As Double
        Public puntos_ligadura As Integer
        Public cobertura_total As Double
        Public flagestado As Integer
        Public Trama As DataTable

        Sub New()
            codigo_tela = ""
            revision_tela = 0
        End Sub

        Public Sub Insertar()
            Try
                If Not codigo_tela = "" Then
                    Dim strSQL = "INSERT INTO NM_Tela " & _
                        "(codigo_articulo, revision_articulo, tipo, "
                    If Trim(UCase(codigo_tipo_maquina)) <> "SELECCIONAR" Then strSQL += "codigo_tipo_maquina,"
                    strSQL += "ligamento, " & _
                        "numero_peine, ancho_peine, pasadas_pulgada, pasadas_centimetro, " & _
                        "ancho_crudo, hilos_pulgada_tela, hilos_centimetro_tela, " & _
                        "hilos_diente, encogimiento_urdimbre, encogimiento_trama, " & _
                        "hilos_pulgada_peine, hilos_centimetro_peine, numero_cuadros, " & _
                        "eficiencia_teorica, eficiencia_real, " & _
                        "coeficiente_densidad_urdido, coeficiente_densidad_trama, " & _
                        "factor_cobertura_urdimbre, factor_cobertura_trama, " & _
                        "puntos_ligadura, cobertura_total, flagestado, " & _
                        "usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        codigo_tela & "', " & _
                        revision_tela & ", '" & _
                        tipo & "', '"
                        If Trim(UCase(codigo_tipo_maquina)) <> "SELECCIONAR" Then strSQL+= codigo_tipo_maquina & "', '" 
                    strSQL += ligamento & "', " & _
                        numero_peine & ", " & _
                        ancho_peine & ", " & _
                        pasadas_pulgada & ", " & _
                        pasadas_centimetro & ", " & _
                        ancho_crudo & ", " & _
                        hilos_pulgada_tela & ", " & _
                        hilos_centimetro_tela & ", " & _
                        hilos_diente & ", " & _
                        encogimiento_urdimbre & ", " & _
                        encogimiento_trama & ", " & _
                        hilos_pulgada_peine & ", " & _
                        hilos_centimetro_peine & ", " & _
                        numero_cuadros & ", " & _
                        eficiencia_teorica & ", " & _
                        eficiencia_real & ", " & _
                        coeficiente_densidad_urdido & ", " & _
                        coeficiente_densidad_trama & ", " & _
                        factor_cobertura_urdimbre & ", " & _
                        factor_cobertura_trama & ", " & _
                        puntos_ligadura & ", " & _
                        cobertura_total & ", " & _
                        flagestado & ", '" & _
                        usuario & "'," & _
                        "GetDate())"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es incorrecto.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Update()

            If codigo_tela <> "" Then
                Dim strSQL = "UPDATE NM_Tela " & _
                    "SET " & _
                    "tipo = '" & tipo & "', " & _
                    "codigo_tipo_maquina = '" & codigo_tipo_maquina & "', " & _
                    "ligamento = '" & ligamento & "', " & _
                    "numero_peine = " & numero_peine & ", " & _
                    "ancho_peine = " & ancho_peine & ", " & _
                    "pasadas_pulgada = " & pasadas_pulgada & ", " & _
                    "pasadas_centimetro = " & pasadas_centimetro & ", " & _
                    "ancho_crudo = " & ancho_crudo & ", " & _
                    "hilos_pulgada_tela = " & hilos_pulgada_tela & ", " & _
                    "hilos_centimetro_tela = " & hilos_centimetro_tela & ", " & _
                    "hilos_diente = " & hilos_diente & ", " & _
                    "encogimiento_urdimbre = " & encogimiento_urdimbre & ", " & _
                    "encogimiento_trama = " & encogimiento_trama & ", " & _
                    "hilos_pulgada_peine = " & hilos_pulgada_peine & ", " & _
                    "hilos_centimetro_peine = " & hilos_centimetro_peine & ", " & _
                    "numero_cuadros = " & numero_cuadros & ", " & _
                    "eficiencia_teorica = " & eficiencia_teorica & ", " & _
                    "eficiencia_real = " & eficiencia_real & ", " & _
                    "coeficiente_densidad_urdido = " & coeficiente_densidad_urdido & ", " & _
                    "coeficiente_densidad_trama = " & coeficiente_densidad_trama & ", " & _
                    "factor_cobertura_urdimbre = " & factor_cobertura_urdimbre & ", " & _
                    "factor_cobertura_trama = " & factor_cobertura_trama & ", " & _
                    "puntos_ligadura = " & puntos_ligadura & ", " & _
                    "cobertura_total = " & cobertura_total & ", " & _
                    "flagestado = " & flagestado & ", " & _
                    "usuario_modificacion = '" & usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_articulo = '" & codigo_tela & "' " & _
                    "AND revision_articulo = " & revision_tela
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede actualizar porque el código es incorrecto.")
            End If
        End Sub

        Public Function Exist(ByVal codigoTela As String, ByVal revisionTela As Integer) As Boolean
            Try
                Dim sql As String
                Dim objDT As New DataTable
                Dim objDR As DataRow
                sql = "SELECT * FROM NM_Tela WHERE codigo_articulo = '" & _
                codigoTela & "' AND revision_articulo = " & revisionTela
                objDT = BD.Query(sql)
                Return (objDT.Rows.Count > 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Sub Seek(ByVal codigoTela As String, ByVal revisionTela As Integer)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_Tela WHERE codigo_articulo = '" & codigoTela & "' AND revision_articulo = " & revisionTela
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_tela = objDR("codigo_articulo")
                revision_tela = objDR("revision_articulo")
                tipo = objDR("tipo")
                codigo_tipo_maquina = objDR("codigo_tipo_maquina")
                ligamento = objDR("ligamento")
                numero_peine = objDR("numero_peine")
                ancho_peine = objDR("ancho_peine")
                pasadas_pulgada = objDR("pasadas_pulgada")
                pasadas_centimetro = objDR("pasadas_centimetro")
                ancho_crudo = objDR("ancho_crudo")
                hilos_pulgada_tela = objDR("hilos_pulgada_tela")
                hilos_centimetro_tela = objDR("hilos_centimetro_tela")
                hilos_diente = objDR("hilos_diente")
                encogimiento_urdimbre = objDR("encogimiento_urdimbre")
                encogimiento_trama = objDR("encogimiento_trama")
                hilos_pulgada_peine = objDR("hilos_pulgada_peine")
                hilos_centimetro_peine = objDR("hilos_centimetro_peine")
                numero_cuadros = objDR("numero_cuadros")
                eficiencia_teorica = objDR("eficiencia_teorica")
                eficiencia_real = objDR("eficiencia_real")
                coeficiente_densidad_urdido = objDR("coeficiente_densidad_urdido")
                coeficiente_densidad_trama = objDR("coeficiente_densidad_trama")
                factor_cobertura_urdimbre = objDR("factor_cobertura_urdimbre")
                factor_cobertura_trama = objDR("factor_cobertura_trama")
                puntos_ligadura = objDR("puntos_ligadura")
                cobertura_total = objDR("cobertura_total")
                flagestado = objDR("flagestado")

                Dim objTrama As New NM_Trama
                Trama = objTrama.LoadDT(codigoTela, revisionTela)
            Next
        End Sub


        Public Sub Seek(ByVal codigoTela As String)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT top 1 * FROM NM_Tela " & _
            " WHERE codigo_articulo = '" & codigoTela & "' " & _
            " AND flagestado = 1 order by revision_articulo desc "
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_tela = objDR("codigo_articulo")
                revision_tela = objDR("revision_articulo")
                tipo = objDR("tipo")
                codigo_tipo_maquina = objDR("codigo_tipo_maquina")
                ligamento = objDR("ligamento")
                numero_peine = objDR("numero_peine")
                ancho_peine = objDR("ancho_peine")
                pasadas_pulgada = objDR("pasadas_pulgada")
                pasadas_centimetro = objDR("pasadas_centimetro")
                ancho_crudo = objDR("ancho_crudo")
                hilos_pulgada_tela = objDR("hilos_pulgada_tela")
                hilos_centimetro_tela = objDR("hilos_centimetro_tela")
                hilos_diente = objDR("hilos_diente")
                'encogimiento_urdimbre = objDR("encogimiento_urdimbre")
                encogimiento_trama = objDR("encogimiento_trama")
                hilos_pulgada_peine = objDR("hilos_pulgada_peine")
                hilos_centimetro_peine = objDR("hilos_centimetro_peine")
                numero_cuadros = objDR("numero_cuadros")
                eficiencia_teorica = objDR("eficiencia_teorica")
                eficiencia_real = objDR("eficiencia_real")
                coeficiente_densidad_urdido = objDR("coeficiente_densidad_urdido")
                coeficiente_densidad_trama = objDR("coeficiente_densidad_trama")
                factor_cobertura_urdimbre = objDR("factor_cobertura_urdimbre")
                factor_cobertura_trama = objDR("factor_cobertura_trama")
                puntos_ligadura = objDR("puntos_ligadura")
                cobertura_total = objDR("cobertura_total")
                flagestado = objDR("flagestado")

                Dim objTrama As New NM_Trama
                Trama = objTrama.LoadDT(codigoTela, revision_tela)
            Next
        End Sub

        Function Exist(ByVal codigoTela As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_Tela where codigo_articulo ='" & _
            codigoTela & "' and flagestado=1"
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function Lista() As DataTable
            Dim strSQL
            strSQL = "SELECT rtrim(codigo_articulo) as codigo_articulo, revision_articulo, " & _
            " tipo, codigo_tipo_maquina," & _
            " codigo_tipo_telar, codigo_urdimbre, revision_urdimbre, ligamento, " & _
            " numero_peine, ancho_peine, pasadas_pulgada, pasadas_centimetro, " & _
            " ancho_crudo, hilos_pulgada_tela, hilos_centimetro_tela, hilos_diente, " & _
            " encogimiento_urdimbre, encogimiento_trama, hilos_centimetro_peine, " & _
            " hilos_pulgada_peine, numero_cuadros, eficiencia_teorica, eficiencia_real, " & _
            " coeficiente_densidad_urdido, coeficiente_densidad_trama, factor_cobertura_urdimbre, " & _
            " factor_cobertura_trama, puntos_ligadura, cobertura_total" & _
            " FROM NM_MA_Tela "
            Return BD.Query(strSQL)
        End Function

        Function List(ByVal sCodTelar As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Exec ListByCambioArticulo '" & sCodTelar & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select codigo_articulo from NM_Tela "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodTelar As String, ByVal sCodArticulo As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Exec ListByCambioArticulo '" & sCodTelar & "', '" & sCodArticulo & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

#Region "JORGE ROMANI"
        'Fecha       : 06-12-2004
        'Autor       : Jorge Romaní
        'Descripción : Lista todos los tipos de tela
        Public Function obtenerListaTipoTelas() As DataTable
            Try
                Dim objSqlConnection As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return objSqlConnection.ObtenerDataTable("TEL_SP_LISTA_TIPO_TELA")
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        'Fecha       : 07-12-2004
        'Autor       : Jorge Romaní
        'Descripción : Actualiza los puntajes de la tabla tipo de tela
        Public Function actualizarTipoTela(ByVal objTipoTela As NM_TipoTela) As Integer
            Try
                Dim objSqlConnection As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = { _
                                     "K_VC_CODIGO", objTipoTela.getCodigo(), _
                                     "K_UN_MAX_PTS_M2_1RA", objTipoTela.getMaxPtsxM21ra(), _
                                     "K_UN_MAX_PTS_M2_2DA", objTipoTela.getMaxPtsxM22da(), _
                                     "K_UN_MAX_PTS_M2_OBS", objTipoTela.getMaxPtsxM2Obs(), _
                                     "K_VC_USUARIO", objTipoTela.getUsuarioModificacion() _
                                    }
                Return objSqlConnection.EjecutarComando("TEL_SP_PUNTOxMETRO_TIPO_TELA_UPD", objParametros)
            Catch ex As Exception
                Return -1
            End Try
        End Function
#End Region

    End Class


End Namespace