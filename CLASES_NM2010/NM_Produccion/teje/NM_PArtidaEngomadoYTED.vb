Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PArtidaEngomadoYTED

#Region "VARIABLES"
        Private _objConexion As AccesoDatosSQLServer

#End Region

        Public codPartidaEngomado As String  'codigo_partida_engomadoted
        Public codPArtidaUrdido As String    'codigo_partida_urdido
        Public tipo As Integer               'tipo
        Public estado As String              'situacion
        Public fecEngomado As String         'fecha_engomado
        Public horaInicio As String          'hora_inicio
        Public horaFin As String             'hora_fin
        Public codUrdidoEngomado As String   'codigo_engomado
        Public fecha_fin As String           'fecha inicio
        Public codMaquina As String          'codigo_maquina
        Public totalEngomado As Integer      'total_engomado
        Public desperdicio As Integer        'desperdicio
        Public color As String               'color
        Public usuarioCrea As String         'usuario_creacion
        Public fechaCrea As Date             'fecha_creacion
        Public usuarioMod As String          'usuario_modificacion
        Public fechaMod As Date              'fecha_modificacion
        Public codTed As String              'codigo_ted
        Public revEngomado As String         'revision_engomado
        Public revTed As String              'revision_ted
        Public litrosInicio As Double        'litros_inicio
        Public litrosFinal As Double         'litros_final
        Public codigoPartidaOrigen As String
        Public desecResto As Boolean         'desechar_resto
        Public codReproceso As String        'codigo de reproceso
        Public Codigo_supervisor As String   'supervisor

        'CAMBIO DG - NUEVO CAMPO - 20210927 - INI
        Public periodo As String
        Public iniperiodo As String
        Public finperiodo As String
        'CAMBIO DG - NUEVO CAMPO - 20210927 - FIN

        Protected dtProduccion As New DataTable
        Protected dtCalidad As New DataTable
        Const COD_PENDIENTE = "1"   ' cuando la partida no ha sido finalizada
        Public BD As New NM_Consulta
        Private objUtil As New NM_General.Util
        Private objConnProd As NM.AccesoDatos.AccesoDatosSQLServer

        Property detalleProduccion() As DataTable
            Get
                Return dtProduccion
            End Get
            Set(ByVal Value As DataTable)
                dtProduccion = Value
            End Set
        End Property

        Property detalleCalidad() As DataTable
            Get
                Return dtCalidad
            End Get
            Set(ByVal Value As DataTable)
                dtCalidad = Value
            End Set
        End Property

        Sub New()
            codPartidaEngomado = ""    'codigo autogenerado
            codPArtidaUrdido = ""
            codigoPartidaOrigen = ""
            tipo = 0
            fecEngomado = Date.Today
            fecha_fin = Date.Today
            horaInicio = ""
            horaFin = ""
            codUrdidoEngomado = ""
            revEngomado = ""
            codTed = ""
            revTed = ""
            codMaquina = ""
            totalEngomado = 0
            desperdicio = 0
            color = ""
            usuarioCrea = ""
            Codigo_supervisor = ""
            usuarioMod = ""
            litrosInicio = 0
            litrosFinal = 0
            desecResto = False
            estado = COD_PENDIENTE
            codReproceso = ""
            'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - INI
            periodo = ""
            iniperiodo = ""
            finperiodo = ""
            'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - FIN
            objConnProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Public Function CerrarPartida(ByVal codPartidaEngomado As String) As String
            Dim lstrGraboCorrectamente As String = "", ldtbResultado As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvch_codigo_partida_engomadoted", codPartidaEngomado, _
                "pchr_usuario_modificacion", Me.usuarioMod.ToString}

                ldtbResultado = objConnProd.ObtenerDataTable("usp_tej_engomado_cerrar", lobjParametros)

                If ldtbResultado.Rows.Count > 0 Then
                    lstrGraboCorrectamente = CType(ldtbResultado.Rows(0).Item("ESTADO"), String)
                End If
            Catch ex As Exception
                lstrGraboCorrectamente = "No se pudó cerrar la partida correctamente."
            Finally
                ldtbResultado = Nothing
            End Try
            Return lstrGraboCorrectamente
        End Function

        Public Function GeneraCodigoPartida() As String
            Dim sql As String
            Dim DB As New NM_Consulta
            Dim tabla As New DataTable
            Dim IniMAq As String
            If codPArtidaUrdido <> "" Then
                sql = "Select Left(nombre_corto, 2) as Nombre" & _
                " from NM_MAQUINA WHERE codigo_maquina = '" & _
                Trim(codMaquina) & "'"
                tabla = DB.Query(sql)
                Dim fila As DataRow
                For Each fila In tabla.Rows
                    IniMAq = fila.Item("Nombre")
                    If IniMAq = "" Then IniMAq = "--"
                Next
                Dim old As String = Trim(codPArtidaUrdido.Chars(4) & codPArtidaUrdido.Chars(5))
                Dim codigo As String = codPArtidaUrdido.Replace(old, IniMAq)

                ' Cantidad de sub partidas registradas
                sql = "Select count (codigo_sub_partida_urdido) as cantidad " & _
                      "from NM_PartidaEngomadoYTED WHERE codigo_sub_partida_urdido LIKE '" & _
                      codPArtidaUrdido.Substring(0, 8) & "%'"
                tabla = DB.Query(sql)
                Dim cantidad As Integer
                For Each fila In tabla.Rows
                    cantidad = fila.Item("cantidad")
                Next
                Dim correlativo As String = CInt(codigo.Substring(0, 2)) + cantidad
                If Val(correlativo) < 10 Then correlativo = "0" & correlativo
                Return correlativo & codigo.Substring(2, 8)
            Else
                Return ""
            End If
            tabla.Dispose()
            DB = Nothing
        End Function

        Public Function CodigoPartidaEngomado_Obtener(ByVal strCodigoUrdido As String, ByVal strCodigoMaquina As String) As String
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim objParametros() As String = {"var_CodigoSubPartida", strCodigoUrdido, "var_CodigoMaquina", strCodigoMaquina}
                Return _objConexion.ObtenerValor("usp_PTJ_CodigoPartidaEngomado_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#Region " Metodos de manipulacion directa a la BD"
        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function insertar() As Boolean
            Dim lbln_estado As Boolean = False
            Try
                'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - INI
                'Dim lobjParametros() As Object = { _
                '"pvch_codpartidaengomado", codPartidaEngomado, _
                '"pvch_codpartidaurdido", codPArtidaUrdido, _
                '"pint_tipo", tipo, _
                '"pvch_fecengomado", objUtil.FormatFecha(fecEngomado), _
                '"pvch_fecfin", objUtil.FormatFecha(fecha_fin), _
                '"pvch_horainicio", horaInicio, _
                '"pvch_horafin", horaFin, _
                '"pvch_codurdidoengomado", codUrdidoEngomado, _
                '"pvch_codmaquina", codMaquina, _
                '"pint_totalengomado", totalEngomado, _
                '"pint_desperdicio", desperdicio, _
                '"pvch_color", color, _
                '"pvch_usuariocreacion", usuarioCrea, _
                '"pvch_usuariomodif", usuarioMod, _
                '"pvch_codted", codTed, _
                '"pvch_revengomado", revEngomado, _
                '"pvch_revted", revTed, _
                '"pnum_litrosinicio", litrosInicio, _
                '"pnum_litrosfinal", litrosFinal, _
                '"pvch_codpartidaorigen", codigoPartidaOrigen, _
                '"ptin_descresto", CInt(desecResto), _
                '"pvch_estado", estado}
                Dim lobjParametros() As Object = { _
                "pvch_codpartidaengomado", codPartidaEngomado, _
                "pvch_codpartidaurdido", codPArtidaUrdido, _
                "pint_tipo", tipo, _
                "pvch_fecengomado", objUtil.FormatFecha(fecEngomado), _
                "pvch_fecfin", objUtil.FormatFecha(fecha_fin), _
                "pvch_horainicio", horaInicio, _
                "pvch_horafin", horaFin, _
                "pvch_codurdidoengomado", codUrdidoEngomado, _
                "pvch_codmaquina", codMaquina, _
                "pint_totalengomado", totalEngomado, _
                "pint_desperdicio", desperdicio, _
                "pvch_color", color, _
                "pvch_usuariocreacion", usuarioCrea, _
                "pvch_usuariomodif", usuarioMod, _
                "pvch_codted", codTed, _
                "pvch_revengomado", revEngomado, _
                "pvch_revted", revTed, _
                "pnum_litrosinicio", litrosInicio, _
                "pnum_litrosfinal", litrosFinal, _
                "pvch_codpartidaorigen", codigoPartidaOrigen, _
                "ptin_descresto", CInt(desecResto), _
                "pvch_estado", estado, _
                "pvch_Periodo", periodo}
                'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - FIN
                objConnProd.EjecutarComando("usp_tej_partidaengomadoyted_insertar_2", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
            Finally
                objConnProd.Dispose()
                objConnProd = Nothing
            End Try
            Return lbln_estado
        End Function

        Public Function Actualizar() As Boolean
            Dim lbln_estado As Boolean = False
            Try
                'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - INI
                'Dim lobjParametros() As Object = { _
                '"pvch_codpartidaengomado", codPartidaEngomado, _
                '"pvch_fecengomado", objUtil.FormatFecha(fecEngomado), _
                '"pvch_fecfin", objUtil.FormatFecha(fecha_fin), _
                '"pvch_horainicio", horaInicio, _
                '"pvch_horafin", horaFin, _
                '"pint_totalengomado", totalEngomado, _
                '"pint_desperdicio", desperdicio, _
                '"pvch_color", color, _
                '"pvch_usuariomodif", usuarioMod, _
                '"pnum_litrosinicio", litrosInicio, _
                '"pnum_litrosfinal", litrosFinal, _
                '"pvch_codpartidaorigen", codigoPartidaOrigen, _
                '"ptin_descresto", CInt(desecResto), _
                '"pvch_estado", estado, _
                '"pvch_codigoreproceso", codReproceso}
                Dim lobjParametros() As Object = { _
                "pvch_codpartidaengomado", codPartidaEngomado, _
                "pvch_fecengomado", objUtil.FormatFecha(fecEngomado), _
                "pvch_fecfin", objUtil.FormatFecha(fecha_fin), _
                "pvch_horainicio", horaInicio, _
                "pvch_horafin", horaFin, _
                "pint_totalengomado", totalEngomado, _
                "pint_desperdicio", desperdicio, _
                "pvch_color", color, _
                "pvch_usuariomodif", usuarioMod, _
                "pnum_litrosinicio", litrosInicio, _
                "pnum_litrosfinal", litrosFinal, _
                "pvch_codpartidaorigen", codigoPartidaOrigen, _
                "ptin_descresto", CInt(desecResto), _
                "pvch_estado", estado, _
                "pvch_codigoreproceso", codReproceso, _
                "pvch_Periodo", periodo}
                'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - FIN
                objConnProd.EjecutarComando("usp_tej_partidaengomadoyted_actualizar", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
            Finally
                objConnProd.Dispose()
                objConnProd = Nothing
            End Try
            Return lbln_estado
        End Function

        Public Function listar() As DataTable
            Dim objGen As New NM_Consulta
            Return objGen.getData("NM_PartidaEngomadoYTED")
            objGen = Nothing
        End Function

        Function Buscar(ByVal codigoUrdido As String) As DataTable
            Dim strSQL = "SELECT * " & _
            "FROM NM_PartidaEngomadoYTED " & _
            "WHERE codigo_sub_partida_urdido ='" & codigoUrdido & "' "
            Return BD.Query(strSQL)
        End Function

        Public Function PartidasUrdidosDisponibles(ByVal TipoEngomado As Integer) As DataTable
            Dim strSQL = "(Select PP.codigo_sub_partida_urdido " & _
            "from NM_ParticionPartidas PP Join NM_PArtidaEngomadoYTED pet " & _
            "ON PP.codigo_sub_partida_urdido = pet.codigo_sub_partida_urdido " & _
            "WHERE pet.estado = '" & COD_PENDIENTE & "' AND pet.tipo = " & TipoEngomado & ")" & _
            " union " & _
            "(Select PP.codigo_sub_partida_urdido " & _
            "from NM_ParticionPartidas PP " & _
            "where PP.codigo_sub_partida_urdido " & _
            "NOT IN  (SELECT pet.codigo_sub_partida_urdido FROM " & _
            "NM_ParticionPartidas pp " & _
            "JOIN NM_PArtidaEngomadoYTED pet ON pp.codigo_sub_partida_urdido = pet.codigo_sub_partida_urdido))"
            Return BD.Query(strSQL)
        End Function

        Public Function PartidasUrdidosDisponibles() As DataTable
            Dim strSQL = "(Select PP.codigo_sub_partida_urdido " & _
            "from NM_ParticionPartidas PP Join NM_PArtidaEngomadoYTED pet " & _
            "ON PP.codigo_sub_partida_urdido = pet.codigo_sub_partida_urdido " & _
            "WHERE pet.estado = '" & COD_PENDIENTE & "')" & _
            " union " & _
            "(Select PP.codigo_sub_partida_urdido " & _
            "from NM_ParticionPartidas PP " & _
            "where PP.codigo_sub_partida_urdido " & _
            "NOT IN  (SELECT pet.codigo_sub_partida_urdido FROM " & _
            "NM_ParticionPartidas pp " & _
            "JOIN NM_PArtidaEngomadoYTED pet ON pp.codigo_sub_partida_urdido = pet.codigo_sub_partida_urdido))"
            Return BD.Query(strSQL)
        End Function

        '---------------------------------------------------------- 
        'Modificado: Se valida el color de engomado ted
        'Autor: Alexander Torres Cardenas
        'Mayo 2016
        '----------------------------------------------------------
        Public Function GetPartidaEngomadoTED(ByVal pcodPartidaUrdido As String) As String
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Dim objDR As DataRow
            Dim objDT As New DataTable
            strsql = "SELECT codigo_partida_engomadoted, tipo, fecha_inicio, fecha_fin, hora_inicio, hora_fin," & _
                     "codigo_supervisor, total_engomado, desperdicio, litros_inicio, litros_final, " & _
                     "codigo_partida_origen, desechar_resto, estado, " & _
                     "usuario_creacion, fecha_creacion, usuario_modificacion, fecha_modificacion, " & _
                     "codigo_sub_partida_urdido, codigo_maquina, " & _
                     "RTRIM(LTRIM(codigo_color_ted)) AS codigo_color_ted, " & _
                     "codigo_engomado, revision_engomado, codigo_ted, revision_ted, codigo_reproceso " & _
                     "FROM NM_PartidaEngomadoYTED where codigo_sub_partida_urdido ='" & _
                     pcodPartidaUrdido & "'" & ""
            objDT = objGen.Query(strsql)
            For Each objDR In objDT.Rows
                If Not IsDBNull(objDR("codigo_partida_engomadoted")) Then Return objDR("codigo_partida_engomadoted")
                Exit For
            Next
            Return ""
            objDT.Dispose()
            objGen = Nothing
        End Function

        Public Function getPartTEDCerradas() As DataTable
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Dim objDR As DataRow
            Dim objDT As New DataTable
            strsql = "SELECT * FROM NM_PartidaEngomadoYTED where tipo = 1 and estado = 0"
            objDT = objGen.Query(strsql)
            Return objDT
            objDT.Dispose()
            objGen = Nothing
        End Function

        '---------------------------------------------------------- 
        'Modificado: Se valida el color de engomado ted
        'Autor: Alexander Torres Cardenas
        'Mayo 2016
        '----------------------------------------------------------
        Overridable Sub Seek(ByVal pcodPartidaEngomado As String)
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 -  INI
            'strsql = "SELECT codigo_partida_engomadoted, tipo, fecha_inicio, fecha_fin, hora_inicio, hora_fin," & _
            '         "codigo_supervisor, total_engomado, desperdicio, litros_inicio, litros_final, " & _
            '         "codigo_partida_origen, desechar_resto, estado, " & _
            '         "usuario_creacion, fecha_creacion, usuario_modificacion, fecha_modificacion, " & _
            '         "codigo_sub_partida_urdido, codigo_maquina, " & _
            '         "RTRIM(LTRIM(codigo_color_ted)) AS codigo_color_ted, " & _
            '         "codigo_engomado, revision_engomado, codigo_ted, revision_ted, codigo_reproceso " & _
            '         "FROM NM_PartidaEngomadoYTED where codigo_partida_engomadoted ='" & _
            '  pcodPartidaEngomado & "'"
            'objDT = objGen.Query(strsql)
            'For Each objDR In objDT.Rows
            '    If Not IsDBNull(objDR("codigo_partida_engomadoted")) Then codPartidaEngomado = objDR("codigo_partida_engomadoted")
            '    If Not IsDBNull(objDR("codigo_sub_partida_urdido")) Then codPArtidaUrdido = objDR("codigo_sub_partida_urdido") 'fecha_engomado
            '    If Not IsDBNull(objDR("fecha_inicio")) Then fecEngomado = objDR("fecha_inicio") 'fecha_engomado
            '    If Not IsDBNull(objDR("hora_inicio")) Then horaInicio = objDR("hora_inicio") 'hora_inicio
            '    If Not IsDBNull(objDR("hora_fin")) Then horaFin = objDR("hora_fin") 'hora_fin
            '    If Not IsDBNull(objDR("codigo_ted")) Then codTed = objDR("codigo_ted") 'codigo_ted
            '    If Not IsDBNull(objDR("revision_ted")) Then revTed = objDR("revision_ted") 'revision_ted
            '    If Not IsDBNull(objDR("codigo_maquina")) Then codMaquina = objDR("codigo_maquina") 'codigo_maquina
            '    If Not IsDBNull(objDR("total_engomado")) Then totalEngomado = objDR("total_engomado") 'total_engomado
            '    If Not IsDBNull(objDR("desperdicio")) Then desperdicio = objDR("desperdicio") 'desperdicio
            '    If Not IsDBNull(objDR("codigo_engomado")) Then codUrdidoEngomado = objDR("codigo_engomado") 'desperdicio
            '    If Not IsDBNull(objDR("revision_engomado")) Then revEngomado = objDR("revision_engomado") 'desperdicio
            '    If Not IsDBNull(objDR("litros_inicio")) Then litrosInicio = objDR("litros_inicio") 'desperdicio
            '    If Not IsDBNull(objDR("litros_final")) Then litrosFinal = objDR("litros_final") 'desperdicio
            '    If Not IsDBNull(objDR("codigo_partida_origen")) Then codigoPartidaOrigen = objDR("codigo_partida_origen")
            '    If Not IsDBNull(objDR("desechar_resto")) Then desecResto = objDR("desechar_resto") 'desperdicio
            '    If Not IsDBNull(objDR("codigo_color_ted")) Then color = (objDR("codigo_color_ted")) 'color
            '    If Not IsDBNull(objDR("estado")) Then estado = (objDR("estado"))
            '    If Not IsDBNull(objDR("fecha_fin")) Then fecha_fin = (objDR("fecha_fin"))
            '    If Not IsDBNull(objDR("codigo_reproceso")) Then codReproceso = (objDR("codigo_reproceso")) 'codigo reproceso
            '    'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - INI
            '    If Not IsDBNull(objDR("periodo")) Then periodo = (objDR("periodo")) 'periodo
            '    'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - FIN
            'Next
            Dim pdtb_datos As New DataTable
            Try
                Dim lobjParametros() As Object = {"vch_CodEngomado", pcodPartidaEngomado}
                pdtb_datos = objConnProd.ObtenerDataTable("USP_OBTENER_DATOS_PARTIDA_ENGOMADO_TED", lobjParametros)
            Catch ex As Exception
                Throw ex
            Finally
            End Try
            'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 -  FIN

            For Each objDR In pdtb_datos.Rows
                If Not IsDBNull(objDR("codigo_partida_engomadoted")) Then codPartidaEngomado = objDR("codigo_partida_engomadoted")
                If Not IsDBNull(objDR("codigo_sub_partida_urdido")) Then codPArtidaUrdido = objDR("codigo_sub_partida_urdido") 'fecha_engomado
                If Not IsDBNull(objDR("fecha_inicio")) Then fecEngomado = objDR("fecha_inicio") 'fecha_engomado
                If Not IsDBNull(objDR("hora_inicio")) Then horaInicio = objDR("hora_inicio") 'hora_inicio
                If Not IsDBNull(objDR("hora_fin")) Then horaFin = objDR("hora_fin") 'hora_fin
                If Not IsDBNull(objDR("codigo_ted")) Then codTed = objDR("codigo_ted") 'codigo_ted
                If Not IsDBNull(objDR("revision_ted")) Then revTed = objDR("revision_ted") 'revision_ted
                If Not IsDBNull(objDR("codigo_maquina")) Then codMaquina = objDR("codigo_maquina") 'codigo_maquina
                If Not IsDBNull(objDR("total_engomado")) Then totalEngomado = objDR("total_engomado") 'total_engomado
                If Not IsDBNull(objDR("desperdicio")) Then desperdicio = objDR("desperdicio") 'desperdicio
                If Not IsDBNull(objDR("codigo_engomado")) Then codUrdidoEngomado = objDR("codigo_engomado") 'desperdicio
                If Not IsDBNull(objDR("revision_engomado")) Then revEngomado = objDR("revision_engomado") 'desperdicio
                If Not IsDBNull(objDR("litros_inicio")) Then litrosInicio = objDR("litros_inicio") 'desperdicio
                If Not IsDBNull(objDR("litros_final")) Then litrosFinal = objDR("litros_final") 'desperdicio
                If Not IsDBNull(objDR("codigo_partida_origen")) Then codigoPartidaOrigen = objDR("codigo_partida_origen")
                If Not IsDBNull(objDR("desechar_resto")) Then desecResto = objDR("desechar_resto") 'desperdicio
                If Not IsDBNull(objDR("codigo_color_ted")) Then color = (objDR("codigo_color_ted")) 'color
                If Not IsDBNull(objDR("estado")) Then estado = (objDR("estado"))
                If Not IsDBNull(objDR("fecha_fin")) Then fecha_fin = (objDR("fecha_fin"))
                If Not IsDBNull(objDR("codigo_reproceso")) Then codReproceso = (objDR("codigo_reproceso")) 'codigo reproceso
                'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - INI
                If Not IsDBNull(objDR("periodo")) Then periodo = (objDR("periodo")) 'periodo
                If Not IsDBNull(objDR("primerdia")) Then iniperiodo = (objDR("primerdia")) 'primerdia
                If Not IsDBNull(objDR("ultimodia")) Then finperiodo = (objDR("ultimodia")) 'ultimodia
                'CAMBIO DG - NUEVO CAMPO PERIODO - 20210927 - FIN
            Next
            dtProduccion = loadDetalleProduccion(codPartidaEngomado)
        End Sub

        Overridable Sub SeekXPartidaUrdido(ByVal pcodPartidaUrdido As String)
            Dim PartidaEngTED As String = GetPartidaEngomadoTED(pcodPartidaUrdido)
            Seek(PartidaEngTED)
        End Sub

        Function Exist(ByVal pCodPartida As String) As Boolean
            Dim sql As String, dtPartida As New DataTable
            Dim objConn As New NM_Consulta
            sql = "Select * from NM_PartidaEngomadoYTED " & _
            " where codigo_partida_engomadoted ='" & pCodPartida & "'"
            dtPartida = objConn.Query(sql)
            Return (dtPartida.Rows.Count > 0)
        End Function

#End Region

        Public Function fnc_partidasrelacionadas_guardar(ByVal pstr_accion As String, ByVal pstr_codigopartida As String, ByVal pstr_partidarelacionada As String, ByVal pnum_metros As Double, ByVal pstr_usuario As String, ByRef pstr_mensaje As String) As Boolean
            Dim lbln_estado As Boolean = False
            pstr_mensaje = ""
            Try
                Dim lobjParametros() As Object = { _
                "pchr_accion", pstr_accion, _
                "pvch_codigopartida", pstr_codigopartida, _
                "pvch_partidarelacionada", pstr_partidarelacionada, _
                "pnum_metros", pnum_metros, _
                "pvch_usuario", pstr_usuario}
                objConnProd.EjecutarComando("usp_pro_partengomaytedpartrela_guardar", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
                pstr_mensaje = ex.Message.Replace("'", " ")
            Finally
            End Try
            Return lbln_estado
        End Function

        Public Function fnc_partidasrelacionadas_listar(ByVal pint_tipolista As Integer, ByRef pdtb_datos As DataTable, ByVal pstr_parametro1 As String, ByVal pstr_parametro2 As String, ByVal pstr_parametro3 As String) As Boolean
            Dim lbln_estado As Boolean = False
            Try
                Dim lobjParametros() As Object = { _
                "ptin_tipolista", pint_tipolista, _
                "pvch_parametro1", pstr_parametro1, _
                "pvch_parametro2", pstr_parametro2, _
                "pvch_parametro3", pstr_parametro3}
                pdtb_datos = objConnProd.ObtenerDataTable("usp_pro_partengomaytedpartrela_listar", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False
            Finally
            End Try
            Return lbln_estado
        End Function


#Region " Metodos de manipulacion de los detalles: produccion y Calidad"

        Public Function loadDetalleProduccion(ByVal codPartidaEngomado As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "ptin_tipolista", "3", _
                "pvch_parametro1", codPartidaEngomado, _
                "pvch_parametro2", "", _
                "pvch_parametro3", "", _
                "pvch_parametro4", ""}
                pdtb_datos = objConnProd.ObtenerDataTable("usp_tej_partidaengomadoyted_listar", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

        'Carga el detalle de Calidad (abstracto)
        Public Overridable Function loadDetalleCalidad(ByVal codPartidaEngomado As String) As DataTable
        End Function

        'Metodo que llama a la clase NM_PartidaEngomadoDProduccion para agregar un registro en el detalle de produccion
        Public Overridable Sub AgregarProduccion(ByVal codPartidEngoProduct As String, ByVal plegador As String, ByVal lado As String, _
          ByVal longitud As Integer, ByVal cantPiezas As Integer, ByVal Operario As String, _
          ByVal ocurrencia As String, ByVal pCodSupervisor As String, _
          ByVal pfechaInicio As String, ByVal pfechaFinal As String, ByVal CodMaquina As String, ByVal pUsuario As String, ByVal pObsvPlegador As String)
            Dim PartEngomProduccion As New NM_PartidaEngomadoDProduccion
            Dim objCalidad As New NM_PartidaEngomadoMCalidad
            Try
                PartEngomProduccion.Insertar(codPartidEngoProduct, plegador, lado, longitud, cantPiezas, Operario, ocurrencia, pCodSupervisor, pfechaInicio, pfechaFinal, CodMaquina, pUsuario, pObsvPlegador)
            Catch ex As Exception
                Throw New Exception("Error al insertar en NM_PartidaEngomadoDProduccion: " & ex.Message)
            End Try
            dtProduccion = loadDetalleProduccion(codPartidEngoProduct)
        End Sub

        Public Function ActualizarProduccion(ByVal codigoPartidaEngomado As String, ByVal codigoPlegador As String, ByVal lado As String, _
            ByVal longitud As Integer, ByVal cantidadPiezas As Integer, ByVal operario As String, _
            ByVal ocurrencias As String, ByVal pCodSupervisor As String, _
            ByVal pfechaInicio As String, ByVal pfechaFinal As String, ByVal pCodMaquina As String, pUsuario As String, pObservPlegador As String) As String

            Dim PartEngomProduccion As New NM_PartidaEngomadoDProduccion
            Dim strResultado As String = ""

            PartEngomProduccion.codPartEngTED = codigoPartidaEngomado
            PartEngomProduccion.plegador = codigoPlegador
            PartEngomProduccion.lado = lado
            PartEngomProduccion.cantPiezas = cantidadPiezas
            PartEngomProduccion.operario = operario
            PartEngomProduccion.longitud = longitud
            PartEngomProduccion.codigo_supervisor = pCodSupervisor
            PartEngomProduccion.ocurrencias = ocurrencias
            PartEngomProduccion.fechainicio = pfechaInicio
            PartEngomProduccion.fechafinal = pfechaFinal
            PartEngomProduccion.codMaquina = pCodMaquina
            PartEngomProduccion.usuarioMod = pUsuario
            PartEngomProduccion.observ_plegador = pObservPlegador

            strResultado = PartEngomProduccion.Actualizar()
            dtProduccion = loadDetalleProduccion(codigoPartidaEngomado)

            Return strResultado

        End Function

        '---------------------------------------------------------- 
        'Modificado: Partida TED, piezas, plegadores
        'Autor: Alexander Torres Cardenas
        'Noviembre 2016
        '----------------------------------------------------------
        Public Function EliminarProduccion(ByVal codPartidEngoProduct As String, ByVal plegador As String) As String
            Dim PartEngomProduccion As New NM_PartidaEngomadoDProduccion
            Dim objCalidad As New NM_PartidaEngomadoMCalidad
            Dim strResultado As String = ""

            If codPartidEngoProduct <> "" And plegador <> "" Then
                strResultado = PartEngomProduccion.eliminar(codPartidEngoProduct, plegador)
                dtProduccion = loadDetalleProduccion(codPartidEngoProduct)
            End If
            Return strResultado
        End Function

        '---------------------------------------------------------- 
        'Modificado: IQ TED
        'Autor: Alexander Torres Cardenas
        'Diciembre 2016
        '----------------------------------------------------------

        ' consulta cabecera de consumo de iq
        Public Function ConsultarPEIQ_Cab(ByVal strCodigoPartidaEngomado As String,
                                          ByVal intFase As Integer) As DataTable
            Dim dtbPartidaengomadoIQCab As DataTable
            dtbPartidaengomadoIQCab = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "int_FaseEngomado", intFase}
                dtbPartidaengomadoIQCab = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_CabConsultar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQCab
        End Function

        ' consulta cabecera de consumo de iq 2
        Public Function ConsultarPEIQ_Cab_v2(ByVal strCodigoPartidaEngomado As String,
                                             ByVal intFase As Integer) As DataTable
            Dim dtbPartidaengomadoIQCab As DataTable
            dtbPartidaengomadoIQCab = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "int_FaseEngomado", intFase}
                dtbPartidaengomadoIQCab = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_CabConsultar_v2", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQCab
        End Function

        ' consulta detalle de consumo de iq
        Public Function ConsultarPEIQ_Det(ByVal strCodigoPartidaEngomado As String,
                                          ByVal intTurno As Integer,
                                          ByVal intFaseEngomado As Integer,
                                          ByRef dtbPartidaengomadoIQDet As DataTable) As DataTable
            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "int_Turno", intTurno,
                                                  "int_FaseEngomado", intFaseEngomado
                                                 }
                dtbPartidaengomadoIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_DetConsultar", lobjParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQDet
        End Function

        ' inserta cabecera de consumo de iq
        Public Function InsertarPEIQ_Cab(ByVal strCodigoPartidaEngomado As String,
                                         ByVal strFechaPartidaEngomado As String,
                                         ByVal dblMetrosPartidaEngomado As Double,
                                         ByVal strCodigoEngomado As String,
                                         ByVal intRevisionEngomado As Integer,
                                         ByVal dblVelocidadEngomado As Double,
                                         ByVal intFaseEngomado As Integer,
                                         ByVal dblVolumenInicial As Double,
                                         ByVal dblVolumenFinal As Double,
                                         ByVal dblVolumenPreparado As Double,
                                         ByVal strObservaciones As String,
                                         ByVal strUsuario As String,
                                         ByVal intTurnoConsumo As Integer) As DataTable
            Dim dtbPartidaengomadoIQCab As DataTable
            dtbPartidaengomadoIQCab = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "dtm_FechaPartidaEngomado", strFechaPartidaEngomado,
                                                  "num_MetrosPartidaEngomado", dblMetrosPartidaEngomado,
                                                  "vch_CodigoEngomado", strCodigoEngomado,
                                                  "int_RevisionEngomado", intRevisionEngomado,
                                                  "num_VelocidadEngomado", dblVelocidadEngomado,
                                                  "int_FaseEngomado", intFaseEngomado,
                                                  "num_VolumenInicial", dblVolumenInicial,
                                                  "num_VolumenFinal", dblVolumenFinal,
                                                  "num_VolumenPreparado", dblVolumenPreparado,
                                                  "vch_Observaciones", strObservaciones,
                                                  "vch_Usuario", strUsuario,
                                                  "int_TurnoConsumo", intTurnoConsumo
                                                 }
                dtbPartidaengomadoIQCab = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_CabInsertar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQCab
        End Function

        ' insertar detalle de consumo de iq
        Public Function InsertarPEIQ_Det(ByVal strCodigoPartidaEngomado As String,
                                         ByVal strCodigoEngomado As String,
                                         ByVal intRevisionEngomado As Integer,
                                         ByVal strCodigoOperario As String,
                                         ByVal intTurno As Integer,
                                         ByVal intFaseEngomado As Integer,
                                         ByVal strCodigoReceta As String,
                                         ByVal intRevisionReceta As Integer,
                                         ByVal strUsuario As String) As DataTable
            Dim dtbPartidaengomadoIQDet As DataTable
            dtbPartidaengomadoIQDet = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "vch_CodigoEngomado", strCodigoEngomado,
                                                  "int_RevisonEngomado", intRevisionEngomado,
                                                  "vch_CodigoOperario", strCodigoOperario,
                                                  "int_TurnoConsumo", intTurno,
                                                  "int_FaseEngomado", intFaseEngomado,
                                                  "vch_CodigoReceta", strCodigoReceta,
                                                  "int_RevisionReceta", intRevisionReceta,
                                                  "vch_Usuario", strUsuario
                                                 }
                dtbPartidaengomadoIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_DetInsertar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQDet
        End Function

        ' Cerrar consumo
        Public Function CerrarConsumoIQ(ByVal strCodigoPartidaEngomado As String) As DataTable
            Dim dtbPartidaengomadoIQDet As DataTable
            dtbPartidaengomadoIQDet = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado}
                dtbPartidaengomadoIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_Cerrar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQDet
        End Function

        ' Abrir consumo
        Public Function AbrirConsumoIQ(ByVal strCodigoPartidaEngomado As String) As DataTable
            Dim dtbPartidaengomadoIQDet As DataTable
            dtbPartidaengomadoIQDet = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado}
                dtbPartidaengomadoIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_Abrir", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQDet
        End Function

        ' actualizar detalle de consumo de iq
        Public Function ActualizarPEIQ_Det(ByVal strCodigoPartidaEngomado As String,
                                         ByVal strCodigoEngomado As String,
                                         ByVal intRevisionEngomado As Integer,
                                         ByVal intTurno As Integer,
                                         ByVal intFaseEngomado As Integer,
                                         ByVal strCodigoReceta As String,
                                         ByVal intRevisionReceta As Integer,
                                         ByVal strCodigoInsumoQuimico As String,
                                         ByVal intSecuenciaConsumo As Integer,
                                         ByVal strFechaConsumo As String,
                                         ByVal strCodigoOperario As String,
                                         ByVal dblCantidadConsumo As Double,
                                         ByVal dblVolumenFinal As Double,
                                         ByVal dblConsumoBe As Double,
                                         ByVal strCondicion As String,
                                         ByVal strUsuario As String,
                                         ByVal dblCantidadConsumoReal As Double) As DataTable
            Dim dtbPartidaengomadoIQDet As DataTable
            dtbPartidaengomadoIQDet = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "vch_CodigoEngomado", strCodigoEngomado,
                                                  "int_RevisonEngomado", intRevisionEngomado,
                                                  "int_TurnoConsumo", intTurno,
                                                  "int_FaseEngomado", intFaseEngomado,
                                                  "vch_CodigoReceta", strCodigoReceta,
                                                  "int_RevisionReceta", intRevisionReceta,
                                                  "vch_CodigoInsumoQuimico", strCodigoInsumoQuimico,
                                                  "int_SecuenciaConsumo", intSecuenciaConsumo,
                                                  "dtm_FechaConsumo", strFechaConsumo,
                                                  "vch_CodigoOperario", strCodigoOperario,
                                                  "num_CantidadConsumo", dblCantidadConsumo,
                                                  "num_VolumenFinal", dblVolumenFinal,
                                                  "num_ConsumoBe", dblConsumoBe,
                                                  "vch_Condicion", strCondicion,
                                                  "vch_Usuario", strUsuario,
                                                  "num_ConsumoReal", dblCantidadConsumoReal
                                                 }
                dtbPartidaengomadoIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_DetActualizar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQDet
        End Function


        ' eliminar detalle de consumo de iq
        Public Function EliminarPEIQ_Det(ByVal strCodigoPartidaEngomado As String,
                                         ByVal intTurno As Integer,
                                         ByVal intFaseEngomado As Integer,
                                         ByVal strCodigoReceta As String,
                                         ByVal intRevisionReceta As Integer,
                                         ByVal strCodigoInsumoQuimico As String,
                                         ByVal intSecuenciaConsumo As Integer) As DataTable
            Dim dtbPartidaengomadoIQDet As DataTable
            dtbPartidaengomadoIQDet = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoPartidaEngomado", strCodigoPartidaEngomado,
                                                  "int_TurnoConsumo", intTurno,
                                                  "int_FaseEngomado", intFaseEngomado,
                                                  "vch_CodigoReceta", strCodigoReceta,
                                                  "int_RevisionReceta", intRevisionReceta,
                                                  "vch_CodigoInsumoQuimico", strCodigoInsumoQuimico,
                                                  "int_SecuenciaConsumo", intSecuenciaConsumo
                                                 }
                dtbPartidaengomadoIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_DetEliminar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbPartidaengomadoIQDet
        End Function

        ' consulta iq por receta y fase
        Public Function ConsultarRecetaIQFase(ByVal strCodigoEngomado As String, _
                                              ByVal intRevisionEngomado As Integer, _
                                              ByVal intFaseEngomado As Integer) As DataTable
            Dim dtbRecetaIQDet As DataTable
            dtbRecetaIQDet = Nothing

            Try
                Dim lobjParametros() As Object = {"vch_CodigoEngomado", strCodigoEngomado,
                                                  "int_RevisionEngomado", intRevisionEngomado,
                                                  "int_FaseEngomado", intFaseEngomado
                                                 }
                dtbRecetaIQDet = objConnProd.ObtenerDataTable("usp_nm_PartidaEngomadoIQ_RecetaIQ", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbRecetaIQDet
        End Function

        '----------------------------------------------------------

        Public Sub AgregarCalidad(ByVal pcodigoPartidaEngomado As String, ByVal pcodPlegador As String, _
          ByVal numMadeja As Integer, ByVal NHR As Integer, ByVal otros As Integer, ByVal obs As String, _
          ByVal calif As Integer, ByVal userMod As String, ByVal paroEleMec As Integer)
            Dim fila As DataRow
            Dim objPartEngMCalidad As New NM_PartidaEngomadoMCalidad
            objPartEngMCalidad.Codigo_partida_engomado = pcodigoPartidaEngomado
            objPartEngMCalidad.Codigo_plegador = pcodPlegador
            'objPartEngMCalidad.Ocurrencias_madeja = ocurr
            objPartEngMCalidad.num_madeja = numMadeja
            objPartEngMCalidad.NHR_madeja = NHR
            objPartEngMCalidad.Otros = otros
            objPartEngMCalidad.Observaciones = obs
            objPartEngMCalidad.calificacion = calif
            objPartEngMCalidad.Usuario_Modificacion = userMod
            'AGREGA CAMPO PARA LA CANTIDAD DE PAROS - DG - INI
            objPartEngMCalidad.ParoMecElec = paroEleMec
            'AGREGA CAMPO PARA LA CANTIDAD DE PAROS - DG - FIN
            Try
                Dim objTed As New NM_PArtidaEngomadoYTED
                If objPartEngMCalidad.Actualizar() = True Then
                    objTed.codPartidaEngomado = pcodigoPartidaEngomado
                    objTed.CalcularCalificacion(pcodPlegador)
                End If
            Catch ex As Exception
                Throw New Exception("Error al insertar en NM_PartidaEngomadoMCalidad: " & ex.Message)
            End Try

        End Sub

#End Region

#Region "Metodos para cargar la data a ser mostrada en los dropdownlist"

        'metodo que devuelve los plegadores para ser mostrados en el grid de produccion
        Public Function getPlegadores() As DataTable
            Dim objGen As New NM_Consulta
            Return objGen.getData("NM_Plegador")
        End Function

        Public Function GetRevisionesTED(ByVal pCodigoTED As String) As DataTable
            Dim db As New NM_Consulta
            Dim objDT As New DataTable
            Dim strql As String
            Try
                If pCodigoTED <> "" Then
                    strql = "select revision_ted from NM_TEd where codigo_ted = '" & pCodigoTED & "'"
                    db.Query(strql)
                    objDT = db.Query(strql)
                Else
                    Throw New Exception("Codigo TED no valido")
                End If
            Catch ex As Exception
                objDT = Nothing
                Throw New Exception(ex.Message)
            End Try
            Return objDT
        End Function

        Public Function GetMaquinas() As DataTable
            Dim Tabla As New DataTable
            Dim Familia As New NM_Maquina
            Try
                Tabla = Familia.Lista("001001001123") ' obtiene todas las maquinas que pertencen a TED
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            Return Tabla
        End Function

        Function getPArtidasUrdidos() As DataTable
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Dim objDT As New DataTable
            strsql = "select * from NM_PartidaUrdido where codigo_partida_urdido like '%HA%'"
            Try
                objDT = objGen.Query(strsql)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            Return objDT
        End Function

#End Region

#Region " Métodos para el cálculo de la calificación de un Plegador "

        ' Calcula y registra la calificación para el Plegador indicado
        Public Sub CalcularCalificacion(ByVal codPlegador As String)
            Dim result As Double = Calificacion(TotalDefectos(codPlegador), LongitudPlegador(codPlegador))
            ActualizarCalificacion(codPlegador, result)
        End Sub

        ' Actualiza sólo el valor de calificación
        Protected Sub ActualizarCalificacion(ByVal codPlegador As String, ByVal calificacion As Double)
            Dim strSQL = "UPDATE NM_PartidaEngomadoMCalidad " & _
                "SET calificacion = " & calificacion & " " & _
                "WHERE codigo_partida_engomadoted = '" & codPartidaEngomado & "' " & _
                "AND codigo_plegador = '" & codPlegador & "'"
            BD.Execute(strSQL)
        End Sub

        ' Fórmula para Calcular la calificación de un Plegador
        Protected Function Calificacion(ByVal totalDefectos As Integer, ByVal longitudPlegador As Integer) As Double
            Dim longitudPlegadorKM As Double
            longitudPlegadorKM = longitudPlegador / 1000
            Return totalDefectos / longitudPlegadorKM
        End Function

        Protected Function LongitudPlegador(ByVal codPlegador As String) As Integer
            Dim dt As DataTable
            Dim strSQL = "SELECT longitud " & _
                        "FROM NM_PartidaEngomadoDProduccion " & _
                        "WHERE codigo_partida_engomadoted = '" & codPartidaEngomado & "' " & _
                        "AND codigo_plegador = '" & codPlegador & "'"
            dt = BD.Query(strSQL)
            Return dt.Rows(0).Item("longitud")
        End Function

        Protected Function TotalDefectos(ByVal codPlegador As String) As Integer
            Dim total As Integer
            Dim objParametros() As Object = {"CODIGO_PARTIDA", Me.codPartidaEngomado, "CODIGO_PLEGADOR", codPlegador}
            total = CType(objConnProd.ObtenerValor("SP_NM_TOTALDEFECTOS_PLEGADOR_PARTIDA_TED", objParametros), Integer)
            Return total
        End Function
#End Region

        Public Function GetPlegadoresPartida(ByVal sCodigoPartida As String) As DataTable
            Dim sql As String, dt As New DataTable, objConn As New NM_Consulta
            sql = "select P.codigo_plegador, P.tipo, P.peso,PP.lado, PP.cantidad_piezas " & _
            " from NM_PartidaEngomadoDProduccion PP, NM_PLEGADOR P " & _
            " where PP.codigo_plegador = P.codigo_plegador " & _
            " and codigo_partida_engomadoted='" & sCodigoPartida & "'" & _
            " order by PP.fecha_creacion "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function SeekPlegadorPartida(ByVal sCodigoPartida As String, ByVal sCodigoPlegador As String) As DataTable
            Dim sql As String, dt As New DataTable, objConn As New NM_Consulta
            sql = "select PP.longitud, P.codigo_plegador, P.tipo, P.peso,PP.lado, PP.cantidad_piezas " & _
            " from NM_PartidaEngomadoDProduccion PP, NM_PLEGADOR P " & _
            " where PP.codigo_plegador = P.codigo_plegador " & _
            " and codigo_partida_engomadoted='" & sCodigoPartida & "'" & _
            " and P.codigo_plegador='" & sCodigoPlegador & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function
    End Class

End Namespace
