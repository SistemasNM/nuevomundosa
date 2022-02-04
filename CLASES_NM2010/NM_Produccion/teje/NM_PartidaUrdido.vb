Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaUrdido

        Implements IDisposable

        Public Codigo_Partida_urdido As String = ""
        Public MetrosPartida As Double
        Public FechaInicio As Date
        Public FechaFinal As Date
        Public CodigoMaquina As String = ""
        Public PesoPartida As Double
        Public CodigoUrdimbre As String = ""
        Public RevisionUrdimbre As Integer
        Public RoturasStandar As Double
        Public RoturasMillon As Double
        Public RoturasMillonTotal As Double
        Public CantidadSubPartidas As Double
        Public Usuario As String = ""
        Public TipoPartida As String = ""

        Public dtPUrdidoProduccion As New DataTable
        Public dtDetalle As DataTable
        Public dtPUrdidoCalidad As New DataTable
        Public Debug As String
        Public mstrMaq As String = ""
        Public Estado As String = ""

        Private mstrError As String
        Private mintAno As Integer 'Año
        Private mintMes As Int16 'Mes

        Private objUtil As New NM_General.Util

#Region "-- Propiedades --"

        Public Property Ano_Partida()
            Get
                Ano_Partida = mintAno
            End Get
            Set(ByVal Value)
                mintAno = Value
            End Set
        End Property

        Public Property Mes_Partida()
            Get
                Mes_Partida = mintMes
            End Get
            Set(ByVal Value)
                mintMes = Value
            End Set
        End Property

        Public Property Cod_Maquina() As String
            Get
                Cod_Maquina = mstrMaq
            End Get
            Set(ByVal Value As String)
                mstrMaq = Value
            End Set
        End Property

        Public ReadOnly Property Error_Mensaje() As String
            Get
                Return mstrError
            End Get
        End Property

#End Region

#Region " Declaracion de Variables Miembro "

        Private m_sqlDtAccPartidaUrdido As AccesoDatosSQLServer
        Private m_sqlDtAccPartidaUrdidoProd As AccesoDatosSQLServer
        Private proceso As String
        Private operacion As String
        Private articulo As String
        Private _objConexion As AccesoDatosSQLServer

#End Region

#Region " Definicion de Constructores "
        Sub New(ByVal p As String, ByVal o As String, ByVal a As String)
            If p = "" Then
                p = "%"
            End If
            If o = "" Then
                o = "%"
            End If
            If a = "" Then
                a = "%"
            End If
            proceso = p
            operacion = o
            articulo = a
            m_sqlDtAccPartidaUrdido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            m_sqlDtAccPartidaUrdidoProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub
#End Region

#Region "IMPRESION ORDEN DE URDIDO "
        Function ImpresionPartidaUrdido(ByVal strCodigoPartida As String) As DataSet
            Try
                Dim objParametros() As Object = {"p_var_CodigoPartida", strCodigoPartida}
                Return m_sqlDtAccPartidaUrdidoProd.ObtenerDataSet("usp_qry_ListarDatosUrdido", objParametros)
            Catch ex As Exception
            End Try
        End Function
#End Region

        Sub New()
            Codigo_Partida_urdido = ""
            MetrosPartida = 0
            FechaInicio = Date.Today.Date
            FechaFinal = Date.Today.Date
            CodigoMaquina = ""
            PesoPartida = 0
            CodigoUrdimbre = ""
            RevisionUrdimbre = 0
            RoturasMillon = 0
            RoturasMillonTotal = 0
            CantidadSubPartidas = 0
            TipoPartida = 0
            m_sqlDtAccPartidaUrdido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            m_sqlDtAccPartidaUrdidoProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Sub New(ByVal CodigoPartida As String)
            Dim objDetProduccion As New NM_PartidaUrdidoProduccion
            Dim objDetallePartida As New NM_PartidaUrdidoDet
            m_sqlDtAccPartidaUrdido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            m_sqlDtAccPartidaUrdidoProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            'CAMBIO DG - NUEVA CONSULTA PARTIDA URDIDO - 20211011 - INI
            'Seek(CodigoPartida)
            Seek_V2(CodigoPartida)
            'CAMBIO DG - NUEVA CONSULTA PARTIDA URDIDO - 20211011 - FIN
            dtPUrdidoProduccion = objDetProduccion.Lista(CodigoPartida, True)
            dtDetalle = objDetallePartida.List(CodigoPartida, True)
        End Sub

        Public Function GeneraCodigo()
            Dim Retorno As String = ""

            Try
                Dim sql As String, objConn As New NM_Consulta, dtCorre As New DataTable, rowCorre As DataRow
                Dim Correlativo As String = "00", IniMaq As String
                Dim objConn2 As New NM_Consulta, dtMaq As New DataTable, rowMaq As DataRow
                Dim Mes As String, objParticion As New NM_ParticionPartidas, dtParticion As New DataTable

                'Obteniendo el maximo  correlativo por año/mes/maquina
                sql = "select  top 1 left(codigo_partida_urdido,2) as Maximo,codigo_partida_urdido " & _
                " from NM_PARTIDAURDIDO	WHERE Year(fecha_inicio) = " & _
                Year(FechaInicio) & " and month(fecha_inicio)=" & Month(FechaInicio) & _
                " and codigo_maquina = '" & Trim(CodigoMaquina) & "' " & _
                " group by codigo_partida_urdido order by left(codigo_partida_urdido,2) desc "
                Correlativo = "1"
                dtCorre = objConn.Query(sql)
                For Each rowCorre In dtCorre.Rows
                    dtParticion = objParticion.Lista(rowCorre.Item("codigo_partida_urdido"))
                    Correlativo = (Val(rowCorre.Item("maximo")) + dtParticion.Rows.Count).ToString
                Next

                'Obteniendo maquina
                sql = "Select Left(nombre_corto, 2) as Nombre" & _
                " from NM_MAQUINA WHERE codigo_maquina = '" & _
                Trim(CodigoMaquina) & "'"

                dtMaq = objConn2.Query(sql)
                For Each rowMaq In dtMaq.Rows
                    IniMaq = rowMaq.Item("Nombre")
                    If IniMaq = "" Then IniMaq = "--"
                Next

                Select Case Month(FechaInicio)
                    Case 1
                        Mes = "EN"
                    Case 2
                        Mes = "FE"
                    Case 3
                        Mes = "MA"
                    Case 4
                        Mes = "AB"
                    Case 5
                        Mes = "MY"
                    Case 6
                        Mes = "JN"
                    Case 7
                        Mes = "JL"
                    Case 8
                        Mes = "AG"
                    Case 9
                        Mes = "SE"
                    Case 10
                        Mes = "OC"
                    Case 11
                        Mes = "NO"
                    Case 12
                        Mes = "DI"
                End Select

                If Val(Correlativo) < 10 Then Correlativo = "0" & Correlativo
                Retorno = Correlativo & Mes & IniMaq & Right(Year(FechaInicio).ToString, 2)

                Return Retorno
            Catch ex As Exception
                Retorno = ""
            End Try
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function CodigoPartidaUrdido_Obtener(ByVal strCodigoMaquina As String, ByVal strFechaInicio As String) As String
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, "var_FechaInicio", strFechaInicio}
                Return _objConexion.ObtenerValor("usp_PTJ_CodigoPartidaUrdido_Obtener_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Exist(ByVal CodigoPartida As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim oj As New NM_Consulta
            Dim dtPartida As New DataTable, rowPartida As DataRow
            sql = "Select * from NM_PARTIDAURDIDO " & _
            " where codigo_partida_urdido = '" & CodigoPartida & "' "
            dtPartida = objConn.Query(sql)
            If dtPartida.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub Seek(ByVal sCodigoPartida As String)
            Dim strSql As String = ""
            Dim objConn As New NM_Consulta
            Dim dtPartida As New DataTable
            Dim rowPartida As DataRow
            ' CAMBIO DG - ACTUALIZAMOS LA CONSULTA POR DT -  20211011 - INI
            'strSql = "Select * from NM_PARTIDAURDIDO  where codigo_partida_urdido = '" & sCodigoPartida & "' "
            'dtPartida = objConn.Query(strSql)
            Dim lobjParametros() As Object = {"var_CodSubPartidaUrdido", sCodigoPartida}
            dtPartida = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("USP_OBTENER_DATOS_DE_SUB_PARTIDA_URDIDO", lobjParametros)
            ' CAMBIO DG - ACTUALIZAMOS LA CONSULTA POR DT -  20211011 - FIN

            For Each rowPartida In dtPartida.Rows
                Codigo_Partida_urdido = rowPartida.Item("codigo_partida_urdido")
                MetrosPartida = rowPartida.Item("metros_partida")
                PesoPartida = rowPartida.Item("peso_partida")
                CodigoUrdimbre = rowPartida.Item("codigo_urdimbre")
                RevisionUrdimbre = rowPartida.Item("revision_urdimbre")
                RoturasStandar = rowPartida.Item("roturas_standar")
                RoturasMillon = rowPartida.Item("roturas_millon")
                RoturasMillonTotal = rowPartida.Item("roturas_millontotal")
                CantidadSubPartidas = rowPartida.Item("cantidad_subpartidas")
                Estado = IIf(rowPartida.Item("estado") = "1", "ABI", "CER")
                If IsDBNull(rowPartida.Item("tipo_partida")) Then
                    TipoPartida = ""
                Else
                    TipoPartida = rowPartida.Item("tipo_partida").ToString
                End If

            Next

        End Sub
        ' CAMBIO DG - AGREGAMOS BUSQUEDA POR PARTIDA URDIDO -  20211011 - INI
        Public Sub Seek_V2(ByVal sCodigoPartida As String)
            Dim strSql As String = ""
            Dim objConn As New NM_Consulta
            Dim dtPartida As New DataTable
            Dim rowPartida As DataRow
            ' CAMBIO DG - ACTUALIZAMOS LA CONSULTA POR DT -  20211011 - INI
            'strSql = "Select * from NM_PARTIDAURDIDO  where codigo_partida_urdido = '" & sCodigoPartida & "' "
            'dtPartida = objConn.Query(strSql)
            Dim lobjParametros() As Object = {"var_CodPartidaUrdido", sCodigoPartida}
            dtPartida = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("USP_OBTENER_DATOS_PARTIDA_URDIDO", lobjParametros)
            ' CAMBIO DG - ACTUALIZAMOS LA CONSULTA POR DT -  20211011 - FIN

            For Each rowPartida In dtPartida.Rows
                Codigo_Partida_urdido = rowPartida.Item("codigo_partida_urdido")
                MetrosPartida = rowPartida.Item("metros_partida")
                PesoPartida = rowPartida.Item("peso_partida")
                CodigoUrdimbre = rowPartida.Item("codigo_urdimbre")
                RevisionUrdimbre = rowPartida.Item("revision_urdimbre")
                RoturasStandar = rowPartida.Item("roturas_standar")
                RoturasMillon = rowPartida.Item("roturas_millon")
                RoturasMillonTotal = rowPartida.Item("roturas_millontotal")
                CantidadSubPartidas = rowPartida.Item("cantidad_subpartidas")
                Estado = IIf(rowPartida.Item("estado") = "1", "ABI", "CER")
                If IsDBNull(rowPartida.Item("tipo_partida")) Then
                    TipoPartida = ""
                Else
                    TipoPartida = rowPartida.Item("tipo_partida").ToString
                End If

            Next

        End Sub
        ' CAMBIO DG -  AGREGAMOS BUSQUEDA POR PARTIDA URDIDO  -  20211011 - FIN
        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------

        Public Function Insert() As Boolean
            Dim lblnGraboCorrectamente As Boolean = False
            Dim ldtbResultado As DataTable
            ldtbResultado = Nothing

            Dim lobjParametros() As Object = { _
            "pvch_codigo_partida_urdido", Codigo_Partida_urdido, _
            "pnum_metros_partida", MetrosPartida, _
            "pnum_peso_partida", PesoPartida, _
            "pvch_codigo_urdimbre", CodigoUrdimbre, _
            "pint_revision_urdimbre", RevisionUrdimbre, _
            "pnum_roturas_standar", Replace(RoturasStandar, ",", "."), _
            "pnum_roturas_millon", Replace(RoturasMillon, ",", "."), _
            "pnum_roturas_millontotal", Replace(RoturasMillonTotal, ",", "."), _
            "pint_cantidad_subpartidas", CantidadSubPartidas, _
            "pvch_usuario_creacion", Usuario, _
            "pchrTipoPartida", TipoPartida}

            ldtbResultado = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_urdidopartida_insertar_2", lobjParametros)
            If ldtbResultado.Rows.Count > 0 Then
                If Mid(ldtbResultado.Rows(0).Item("ESTADO"), 1, 5) = "Exito" Then
                    lblnGraboCorrectamente = True
                Else
                    lblnGraboCorrectamente = False
                End If
            End If
            ldtbResultado = Nothing
            Return lblnGraboCorrectamente
        End Function

        Public Function Update() As Boolean
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Dim lobjParametros() As Object = { _
            "pvch_codigo_partida_urdido", Codigo_Partida_urdido, _
            "pnum_metros_partida", MetrosPartida, _
            "pnum_peso_partida", PesoPartida, _
            "pvch_codigo_urdimbre", CodigoUrdimbre, _
            "pint_revision_urdimbre", RevisionUrdimbre, _
            "pnum_roturas_standar", Replace(RoturasStandar, ",", "."), _
            "pnum_roturas_millon", Replace(RoturasMillon, ",", "."), _
            "pnum_roturas_millontotal", Replace(RoturasMillonTotal, ",", "."), _
            "pint_cantidad_subpartidas", CantidadSubPartidas, _
            "pvch_usuario_modificacion", Usuario}

            ldtbResultado = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_urdidopartida_actualizar", lobjParametros)
            If ldtbResultado.Rows.Count > 0 Then
                If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                    lblnGraboCorrectamente = True
                End If
            End If
            ldtbResultado = Nothing
            Return lblnGraboCorrectamente

        End Function

        Public Function GetPesoPartida() As Double
            Dim sql As String, DT As New DataTable, fila As DataRow
            Dim peso As Double = 0, objConn As New NM_Consulta
            sql = "select sum(numero_armadas * numero_conos_armada * peso_util_hilo) as peso " & _
            " from NM_PartidaUrdidoDet where codigo_partida_urdido = '" & Codigo_Partida_urdido & "' "
            DT = objConn.Query(sql)
            For Each fila In DT.Rows
                peso = fila.Item("peso")
            Next
            Return peso
        End Function
        'CAMBIO DG - ACTUALIZANDO FORMULA DE PESO PARTIDA - 20211026 - INI
        Public Function GetPesoPartida_V2(ByVal strPartidaUrdido As String) As Double
            'Dim sql As String, DT As New DataTable, fila As DataRow
            'Dim peso As Double = 0, objConn As New NM_Consulta
            'sql = "select sum(numero_armadas * numero_conos_armada * peso_util_hilo) as peso " & _
            '" from NM_PartidaUrdidoDet where codigo_partida_urdido = '" & Codigo_Partida_urdido & "' "
            'DT = objConn.Query(sql)
            'For Each fila In DT.Rows
            '    peso = fila.Item("peso")
            'Next
            'Return peso2
            Dim ldtbDatos As New DataTable
            Dim lobjParametros As Object() = {"pvch_CodPartida", strPartidaUrdido}

            Try
                ldtbDatos = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("USP_CALCULAR_PESO_PARTIDA", lobjParametros)
                Return Double.Parse(ldtbDatos.Rows(0).Item("Peso_Partida").ToString())
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO DG - ACTUALIZANDO FORMULA DE PESO PARTIDA - 20211026 - FIN

        Public Function GetLongitudPartida(ByVal sCodigoPartida As String) As Double
            Dim sql As String, DT As New DataTable, fila As DataRow
            Dim Longitud As Double = 0, objConn As New NM_Consulta
            sql = "select case when sum(metrosxrollo) is null then 0 " & _
            " else sum(metrosxrollo) end as suma " & _
            " from NM_PartidaUrdidoDet " & _
            " where codigo_partida_urdido = '" & sCodigoPartida & "' "
            Debug = sql
            DT = objConn.Query(sql)
            For Each fila In DT.Rows
                Longitud = fila.Item("suma")
            Next
            Return Longitud
        End Function

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            sql = "SELECT * FROM NM_PartidaUrdido"
            Return objConn.Query(sql)
        End Function

        Public Function Listar() As DataTable
            Try
                Dim dtblDatos As New DataTable
                dtblDatos = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_qry_ListarPartidasUrdidoAbiertas")
                Return dtblDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Listar(ByVal pintTipoLista As Int16) As DataTable
            'pintTipoLista:1 = Lista para Busqueda de Partidas urdidos, solo disponibles
            Dim ldtbDatos As New DataTable
            Dim lobjParametros As Object() = { _
            "pint_TipoConsulta", pintTipoLista, _
            "pint_Ano", mintAno, _
            "pint_Mes", mintMes, _
            "pCod_maq", mstrMaq}

            Try
                ldtbDatos = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_urdido_listar", lobjParametros)
                Return ldtbDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetRoturas(ByVal sCodigoPartida As String) As Double
            Dim sql As String, objConn As New NM_Consulta
            Dim DT As New DataTable, fila As DataRow, Retorno As Double = 0

            sql = " select case when sum(D.valor_detalle_calidad) is null then 0 " & _
            " else sum(D.valor_detalle_calidad) end as total  " & _
            " from NM_PartidaUrdidoDCalidad D " & _
            " where codigo_partida_urdido = '" & sCodigoPartida & "' " & _
            " and codigo_detalle_calidad<12 "

            DT = objConn.Query(sql)
            For Each fila In DT.Rows
                Retorno = fila.Item("total")
            Next

            Return (Retorno / MetrosPartida)
        End Function

        Function GetPartidasDisponibles() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            sql = "Select distinct PP.codigo_partida_urdido " & _
            " from NM_ParticionPartidas PP " & _
            " where PP.codigo_sub_partida_urdido " & _
            " NOT IN  ( " & _
            " SELECT pet.codigo_sub_partida_urdido FROM NM_ParticionPartidas pp JOIN NM_PArtidaEngomadoYTED pet " & _
            " ON pp.codigo_sub_partida_urdido = pet.codigo_sub_partida_urdido)"
            Return objConn.Query(sql)
        End Function

        Function GetSubPartidas(ByVal sCodigoPartida As String) As DataTable
            Dim sql As String
            Dim objConn As New NM_Consulta

            sql = "select codigo_sub_partida_urdido " & _
            " from NM_ParticionPartidas " & _
            " where codigo_partida_urdido = '" & sCodigoPartida & "' "
            Debug = sql
            Return objConn.Query(sql)
        End Function

        'Funcion que obtiene el porc de bonificacion segun tabla de bonif teniendo como referencia el metraje engomado por la TED
        Public Function GetBonifUTED(ByVal pint_tipolista As Int16, ByVal pstr_fechaini As String, ByVal pstr_fechafin As String, _
        ByVal pint_periodoano As Integer, ByVal pint_periodomes As Int16) As DataTable
            'pint_tipolista:
            '1-calcula el total
            '2-obtiene los datos guardados
            Try
                Dim dtblDatos As New DataTable
                Dim lobjParametros As Object() = { _
                      "p_tin_TipoLista", pint_tipolista, _
                      "p_var_FechaInicio", pstr_fechaini, _
                      "p_var_FechaFinal", pstr_fechafin, _
                      "p_int_PeriodoAno", pint_periodoano, _
                      "p_int_PeriodoMes", pint_periodomes _
                      }

                dtblDatos = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_ptj_porcbonificacionuted_obtener", lobjParametros)
                Return dtblDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'Funcion que guarda el porc de bonificacion segun tabla de bonif teniendo como referencia el metraje engomado por la TED
        Public Function SetBonifUTED(ByVal pint_accion As Int16, ByVal pstr_fechaini As String, ByVal pstr_fechafin As String, ByVal pint_periodoano As Integer, _
        ByVal pint_periodomes As Int16, ByVal pdbl_metraje As Double, ByVal pdbl_porcbonif As Double) As Boolean
            'pint_accion: 1-guardar registro nuevo,2-actualizar a ofisis,3-rechazar
            Dim lbln_resultado As Boolean = False
            Try
                mstrError = ""

                Dim lobjParametros As Object() = { _
                      "p_tin_Accion", pint_accion, _
                      "p_var_FechaInicio", pstr_fechaini, _
                      "p_var_FechaFinal", pstr_fechafin, _
                      "p_int_PeriodoAno", pint_periodoano, _
                      "p_int_PeriodoMes", pint_periodomes, _
                      "p_num_Metraje", pdbl_metraje, _
                      "p_num_PorcBonif", pdbl_porcbonif, _
                      "p_var_Usuario", Usuario _
                      }
                m_sqlDtAccPartidaUrdidoProd.EjecutarComando("usp_ptj_porcbonificacionuted_guardar", lobjParametros)
                lbln_resultado = True

            Catch ex As Exception
                lbln_resultado = False
                mstrError = ex.Message
            End Try

            Return lbln_resultado
        End Function

        'funcion que lista las partidas que deben cerrarse para que pueda aperturarse otra
        'para el caso de engomado crudo solo es 1, solicitado x JCALDERON
        Public Function ListaPartidasxCerrar(ByRef pdtb_lista As DataTable, ByVal pint_tipoconsulta As Int16) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                Dim objParametros() As Object = {"pchr_tipopartida", "URD", _
                                                 "ptin_tipoconsulta", pint_tipoconsulta}
                pdtb_lista = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_tej_listapartidasxcerrar", objParametros)
                lbln_fncestado = True
            Catch Ex As Exception
                Throw Ex
            End Try
            Return lbln_fncestado
        End Function

        Public Function PartidaUrdidoCerrar(ByVal pPartida As String, ByRef pdtb_lista As DataTable) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                Dim objParametros() As Object = {"pcodigo_partida", pPartida}
                pdtb_lista = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("usp_PreTej_PartidaUrdido_ValidaCierre", objParametros)
                lbln_fncestado = True
            Catch Ex As Exception
                Throw Ex
            End Try
            Return lbln_fncestado
        End Function

        ''' <summary>
        ''' LUIS_AJ (20210810)
        ''' </summary>
        ''' <param name="pintTipoLista"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ListarDocumentosAsociados(ByVal pstrPartidaUrdido As String) As DataTable
            Dim ldtbDatos As New DataTable
            Dim objParametros() As Object = {"pvch_CodPartidaUrdido", pstrPartidaUrdido}
            Try
                ldtbDatos = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("USP_TEJ_OBTENER_VALES_SALIDA_X_PARTIDA_URDIDO", objParametros)
                Return ldtbDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' LUIS_AJ (20210810)
        ''' </summary>
        ''' <param name="pintTipoLista"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function EliminarDocumentosAsociados(ByVal pstrPartidaUrdido As String, ByVal pstrNumDocumento As String) As Integer

            Dim objParametros() As Object = {"pvch_CodPartidaUrdido", pstrPartidaUrdido,
                                             "pvch_NumDocumento", pstrNumDocumento}
            Try
                Return m_sqlDtAccPartidaUrdidoProd.EjecutarComando("USP_TEJ_ELIMINAR_VALES_SALIDA_X_PARTIDA_URDIDO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' LUIS_AJ (20210810)
        ''' </summary>
        ''' <param name="pintTipoLista"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsertarDocumentosAsociados(ByVal pstrPartidaUrdido As String, ByVal pstrNumDocumento As String, ByVal pstrCodUsuario As String) As Integer

            Dim objParametros() As Object = {"pvch_CodPartidaUrdido", pstrPartidaUrdido,
                                             "pvch_NumDocumento", pstrNumDocumento,
                                             "pvch_CodUsuario", pstrCodUsuario}
            Try
                Return m_sqlDtAccPartidaUrdidoProd.EjecutarComando("USP_TEJ_INSERTAR_VALES_SALIDA_X_PARTIDA_URDIDO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' LUIS_AJ (20210810)
        ''' </summary>
        ''' <param name="pintTipoLista"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ListarPedidosHilosPendientesSinAsociar() As DataTable
            Dim ldtbDatos As New DataTable
            Try
                ldtbDatos = m_sqlDtAccPartidaUrdidoProd.ObtenerDataTable("USP_TEJ_BUSCAR_PEDIDOS_HILOS_PENDIENTES")
                Return ldtbDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
        End Sub

        'CAMBIO DG - NUEVO MODULO DE PARTICIONES- INI
        Public Function ObtenerDatosPartidaUrdido(ByVal strPartida As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"vch_CodPartidaUrdido", strPartida}

                Return _objConexion.ObtenerDataSet("USP_OBTENER_DATO_PARTIDA_URDIDO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO DG - NUEVO MODULO DE PARTICIONES- FIN
    End Class
End Namespace