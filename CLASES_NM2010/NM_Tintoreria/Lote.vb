Imports NM.AccesoDatos

Namespace Tintoreria
    Public Class Lote
#Region "   Variables"
        Private mstrCodigo As String
        Private mintFase As Integer
        Private mstrCodigoMaquina As String
        Private mstrCodigoOperacion As String
        Private mdblDensidad As Double
        Private mdblMetros As Double
        Private mdblPickUp As Double
        Private mdblVelocidad As Double
        Private mdatFechaCreacion As DateTime
        Private mstrNombreMaquina As String
        Private mstrNombreOperacion As String
        Private mintEstado As Integer
        Private mintActivo As Integer
        Private mstrUsuario As String
        Private mdtFichas As DataTable
        Private mobjCalculo As Object
        Private mdsSet As DataSet
#End Region
#Region "   Constructor"
        Sub New(ByVal strUsuario)
            mstrUsuario = strUsuario
        End Sub
        Sub New(ByVal strUsuario, ByVal strCodigoLote)
            mstrUsuario = strUsuario
            mstrCodigo = strCodigoLote
        End Sub
#End Region
#Region "   Enumeraciones"
        Enum enuFases
            [Pretratamiento] = 1
            [Acabado] = 2
            [Tenido] = 3
        End Enum
        Enum enuTiposCalculo
            [Thermosol] = 1
            [Acabado] = 2
            [Lavado] = 3
            [PadSteam] = 4
            [TMDB] = 5
        End Enum
        Enum enuTipoEsquemas
            [Fichas_x_Lote] = 1
            [Subprocesos] = 2
            [Recetas] = 3
            [Partidas] = 4
            [Consumos] = 5
            [Tinas] = 6
            [BanioInicial] = 7
        End Enum
#End Region
#Region "   Propiedades"
        Public Property CodigoLote() As String
            Get
                CodigoLote = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Fase() As enuFases
            Get
                Fase = mintFase
            End Get
            Set(ByVal Value As enuFases)
                mintFase = Value
            End Set
        End Property
        Public Property CodigoMaquina() As String
            Get
                CodigoMaquina = mstrCodigoMaquina
            End Get
            Set(ByVal Value As String)
                mstrCodigoMaquina = Value
            End Set
        End Property
        Public ReadOnly Property NombreMaquina() As String
            Get
                NombreMaquina = mstrNombreMaquina
            End Get
        End Property
        Public Property CodigoOperacion() As String
            Get
                CodigoOperacion = mstrCodigoOperacion
            End Get
            Set(ByVal Value As String)
                mstrCodigoOperacion = Value
            End Set
        End Property
        Public ReadOnly Property NombreOperacion() As String
            Get
                NombreOperacion = mstrNombreOperacion
            End Get
        End Property
        Public Property Densidad() As Double
            Get
                Densidad = mdblDensidad
            End Get
            Set(ByVal Value As Double)
                mdblDensidad = Value
            End Set
        End Property
        Public Property Metros() As Double
            Get
                Metros = mdblMetros
            End Get
            Set(ByVal Value As Double)
                mdblMetros = Value
            End Set
        End Property
        Public Property PickUp() As Double
            Get
                PickUp = mdblPickUp
            End Get
            Set(ByVal Value As Double)
                mdblPickUp = Value
            End Set
        End Property
        Public Property Velocidad() As Double
            Get
                Velocidad = mdblVelocidad
            End Get
            Set(ByVal Value As Double)
                mdblVelocidad = Value
            End Set
        End Property
        Public ReadOnly Property FechaCreacion() As DateTime
            Get
                FechaCreacion = mdatFechaCreacion
            End Get
        End Property
        Public ReadOnly Property Estado() As Integer
            Get
                Estado = mintEstado
            End Get
        End Property
        Public ReadOnly Property Activo() As Integer
            Get
                Activo = mintActivo
            End Get
        End Property
        Public Property Usuario() As String
            Get
                Usuario = mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property
        Public Property Fichas() As DataTable
            Get
                Fichas = mdtFichas
            End Get
            Set(ByVal Value As DataTable)
                Value.TableName = "Fichas"
                mdtFichas = Value
            End Set
        End Property
        Public ReadOnly Property Esquema(ByVal intTipo As enuTipoEsquemas) As DataTable
            Get
                Select Case intTipo
                    Case enuTipoEsquemas.Fichas_x_Lote
                        Esquema = EsquemaFichasLote()
                    Case enuTipoEsquemas.Partidas
                        Esquema = EsquemaPartidas()
                    Case enuTipoEsquemas.Recetas
                        Esquema = EsquemaRecetas()
                    Case enuTipoEsquemas.Subprocesos
                        Esquema = EsquemaSubprocesos()
                    Case enuTipoEsquemas.Consumos
                        Esquema = EsquemaConsumos()
                    Case enuTipoEsquemas.Tinas
                        Esquema = EsquemaTinas()
                    Case enuTipoEsquemas.BanioInicial
                        Esquema = EsquemaBanioInicial()
                    Case Else
                        Esquema = Nothing
                End Select
            End Get
        End Property

        Public ReadOnly Property SetDatos() As DataSet
            Get
                SetDatos = mdsSet
            End Get
        End Property
#End Region
#Region "   Metodos"
        Public Function Limpiar()
            mstrCodigo = ""
            mintFase = enuFases.Pretratamiento
            mstrCodigoMaquina = ""
            mstrCodigoOperacion = ""
            mdblDensidad = 0
            mdblMetros = 0
            mdblPickUp = 0
            mdblVelocidad = 0
            mdtFichas = EsquemaFichasLote()

            mdsSet = Nothing
        End Function
#Region "       Esquemas"
        Private Function EsquemaFichasLote() As DataTable
            Dim ldtFichas As DataTable
            ldtFichas = New DataTable("Fichas")
            ldtFichas.Columns.Add("codigo_lote", GetType(String))
            ldtFichas.Columns.Add("codigo_ficha", GetType(String))
            ldtFichas.Columns.Add("tipo", GetType(String))
            ldtFichas.Columns.Add("secuencia", GetType(Integer))
            ldtFichas.Columns.Add("peso", GetType(Double))
            ldtFichas.Columns.Add("metros", GetType(Double))
            ldtFichas.Columns.Add("velocidad", GetType(Double))
            ldtFichas.Columns.Add("pickup", GetType(Double))
            Return ldtFichas
        End Function
        Private Function EsquemaSubprocesos() As DataTable
            Dim ldtSubprocesos As DataTable
            ldtSubprocesos = New DataTable("Subprocesos")
            ldtSubprocesos.Columns.Add("codigo_subproceso", GetType(String))
            ldtSubprocesos.Columns.Add("revision_subproceso", GetType(Integer))
            ldtSubprocesos.Columns.Add("descripcion_subproceso", GetType(String))
            Return ldtSubprocesos
        End Function
        Private Function EsquemaRecetas() As DataTable
            Dim ldtRecetas As DataTable
            ldtRecetas = New DataTable("Recetas")
            ldtRecetas.Columns.Add("codigo_subproceso", GetType(String))
            ldtRecetas.Columns.Add("codigo_receta", GetType(String))
            ldtRecetas.Columns.Add("revision_receta", GetType(Integer))
            ldtRecetas.Columns.Add("descripcion_receta", GetType(String))
            Return ldtRecetas
        End Function
        Private Function EsquemaPartidas() As DataTable
            Dim ldtPartidas As DataTable
            ldtPartidas = New DataTable("Partidas")
            ldtPartidas.Columns.Add("secuencia_partida", GetType(Integer))
            ldtPartidas.Columns.Add("tipo_aplicacion", GetType(Integer))
            ldtPartidas.Columns.Add("estado", GetType(Integer))
            ldtPartidas.Columns.Add("volumen", GetType(Integer))
            Return ldtPartidas
        End Function
        Private Function EsquemaConsumos() As DataTable
            Dim ldtConsumos As DataTable
            ldtConsumos = New DataTable("Consumos")
            ldtConsumos.Columns.Add("codigo_receta", GetType(String))
            ldtConsumos.Columns.Add("codigo_insumo", GetType(String))
            ldtConsumos.Columns.Add("nombre_insumo", GetType(String))
            ldtConsumos.Columns.Add("concentracion_gr_lt", GetType(Double))
            ldtConsumos.Columns.Add("orden1", GetType(String))
            ldtConsumos.Columns.Add("orden2", GetType(String))
            Return ldtConsumos
        End Function
        Private Function EsquemaTinas() As DataTable
            Dim ldtRes As DataTable
            ldtRes = New DataTable("Tinas")
            ldtRes.Columns.Add("tina", GetType(String))
            ldtRes.Columns.Add("volumen", GetType(Double))
            ldtRes.Columns.Add("temperatura", GetType(Double))
            Return ldtRes
        End Function
        Private Function EsquemaBanioInicial() As DataTable
            Dim ldtRes As DataTable
            ldtRes = New DataTable("BanioInicial")
            ldtRes.Columns.Add("codigo_receta", GetType(String))
            ldtRes.Columns.Add("codigo_insumo", GetType(String))
            ldtRes.Columns.Add("nombre_insumo", GetType(String))
            ldtRes.Columns.Add("concentracion_gr_lt", GetType(Double))
            ldtRes.Columns.Add("tipo", GetType(Integer))
            ldtRes.Columns.Add("estado", GetType(Integer))
            ldtRes.Columns.Add("orden1", GetType(String))
            ldtRes.Columns.Add("orden2", GetType(String))
            ldtRes.Columns.Add("secuencia_tina", GetType(Integer))
            ldtRes.Columns.Add("tina", GetType(String))
            ldtRes.Columns.Add("consumo", GetType(Double))
            ldtRes.Columns.Add("total", GetType(Double))
            ldtRes.Columns.Add("concentracion_base", GetType(Double))
            Return ldtRes
        End Function
#End Region
#Region "       Grabar"
        Public Function GrabarLote() As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "sin_Fase", mintFase, _
                                                    "var_CodigoMaquina", mstrCodigoMaquina, _
                                                    "var_CodigoOperacion", mstrCodigoOperacion, _
                                                    "num_Densidad", mdblDensidad, _
                                                    "num_Metros", mdblMetros, _
                                                    "num_PickUp", mdblPickUp, _
                                                    "num_Velocidad", mdblVelocidad, _
                                                    "ntx_Fichas", lobjUtil.GeneraXml(mdtFichas), _
                                                    "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_Lote_GrabarLote", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("codigo_lote")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function GrabarConsumo(ByVal intTipoCalculo As enuTiposCalculo, ByRef dsDatos As DataSet) As Boolean
            Select Case intTipoCalculo
                Case enuTiposCalculo.TMDB
                    GrabarConsumoTMDB(dsDatos)
                Case Else
                    Return False
            End Select
        End Function
        Public Function GrabarConsumo(ByVal intTipoCalculo As enuTiposCalculo, ByRef dtDatosCalculo As Object, ByVal dtDatosPartidas As DataTable, ByVal objDatosConsumo As Object) As Boolean
            Select Case intTipoCalculo
                Case enuTiposCalculo.Thermosol
                    Return GrabarConsumoThermosol(CType(dtDatosCalculo, DataTable), dtDatosPartidas, CType(objDatosConsumo, DataTable))
                Case enuTiposCalculo.Acabado
                    Return GrabarConsumoAcabado(CType(dtDatosCalculo, DataTable), dtDatosPartidas, CType(objDatosConsumo, DataTable))
                Case enuTiposCalculo.Lavado
                    Return GrabarConsumoLavado(CType(dtDatosCalculo, DataSet), dtDatosPartidas, CType(objDatosConsumo, DataSet))
                Case enuTiposCalculo.PadSteam
                    Return GrabarConsumoPadSteam(dtDatosPartidas, CType(dtDatosCalculo, DataSet).Tables(0), CType(dtDatosCalculo, DataSet).Tables(1), CType(dtDatosCalculo, DataSet).Tables(2), CType(objDatosConsumo, DataTable))
                Case enuTiposCalculo.TMDB

                Case Else
                    Return False
            End Select
        End Function
        Private Function GrabarConsumoThermosol(ByRef dtDatosCalculo As DataTable, ByVal dtDatosPartidas As DataTable, ByVal dtDatosConsumo As DataTable) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                dtDatosCalculo.TableName = "DatosCalculo"
                dtDatosPartidas.TableName = "DatosPartidas"
                dtDatosConsumo.TableName = "DatosConsumo"
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "ntx_Calculo", lobjUtil.GeneraXml(dtDatosCalculo), _
                                                    "ntx_Partidas", lobjUtil.GeneraXml(dtDatosPartidas), _
                                                    "ntx_Consumo", lobjUtil.GeneraXml(dtDatosConsumo), _
                                                    "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_Lote_GrabarConsumoLoteThermosol", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("codigo_lote")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Private Function GrabarConsumoAcabado(ByRef dtDatosCalculo As DataTable, ByVal dtDatosPartidas As DataTable, ByVal dtDatosConsumo As DataTable) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                dtDatosCalculo.TableName = "DatosCalculo"
                dtDatosPartidas.TableName = "DatosPartidas"
                dtDatosConsumo.TableName = "DatosConsumo"
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "ntx_Calculo", lobjUtil.GeneraXml(dtDatosCalculo), _
                                                    "ntx_Partidas", lobjUtil.GeneraXml(dtDatosPartidas), _
                                                    "ntx_Consumo", lobjUtil.GeneraXml(dtDatosConsumo), _
                                                    "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_Lote_GrabarConsumoLoteAcabado", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("codigo_lote")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Private Function GrabarConsumoLavado(ByRef dsDatosCalculo As DataSet, ByVal dtDatosPartidas As DataTable, ByVal dsDatosConsumo As DataSet) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                dsDatosCalculo.Tables(0).TableName = "DatosCalculo"
                dsDatosCalculo.Tables(1).TableName = "DatosTinas"
                dtDatosPartidas.TableName = "DatosPartidas"
                dsDatosConsumo.Tables(0).TableName = "DatosConsumoBanioInicial"
                dsDatosConsumo.Tables(1).TableName = "DatosConsumoRefuerzo"
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "ntx_Calculo", lobjUtil.GeneraXml(dsDatosCalculo.Tables(0)), _
                                                    "ntx_Tinas", lobjUtil.GeneraXml(dsDatosCalculo.Tables(1)), _
                                                    "ntx_Partidas", lobjUtil.GeneraXml(dtDatosPartidas), _
                                                    "ntx_BanioInicial", lobjUtil.GeneraXml(dsDatosConsumo.Tables(0)), _
                                                    "ntx_Refuerzo", lobjUtil.GeneraXml(dsDatosConsumo.Tables(1)), _
                                                    "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_Lote_GrabarConsumoLoteLavado", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("codigo_lote")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Private Function GrabarConsumoPadSteam(ByVal dtbLotePartida As DataTable, _
                                                ByVal dtbLoteCalculo As DataTable, _
                                                ByVal dtbLoteCalculoPadSteam As DataTable, _
                                                ByVal dtbLoteCalculoTinas As DataTable, _
                                                ByVal dtbLotePartidaIQ As DataTable) As Boolean
            Dim objUtil As New NM_General.Util
            Dim lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim strXML_LotePartida As String = objUtil.GeneraXml(dtbLotePartida)
                Dim strXML_LoteCalculo As String = objUtil.GeneraXml(dtbLoteCalculo)
                Dim strXML_LoteCalculoPadSteam As String = objUtil.GeneraXml(dtbLoteCalculoPadSteam)
                Dim strXML_LoteCalculoTinas As String = objUtil.GeneraXml(dtbLoteCalculoTinas)
                Dim strXML_LotePartidaIQ As String = objUtil.GeneraXml(dtbLotePartidaIQ)
                Dim objParametros() As Object = {"var_CodigoLote", mstrCodigo, "var_XMLLotePartida", strXML_LotePartida, _
                "var_XMLLotePartidaIQ", strXML_LotePartidaIQ, "var_XMLLoteCalculo", strXML_LoteCalculo, _
                "var_XMLLoteTinas", strXML_LoteCalculoTinas, "var_XMLLotePadSteam", strXML_LoteCalculoPadSteam, _
                "var_Usuario", mstrUsuario}
                Return (lobjTinto.ObtenerValor("usp_TIN_Lote_GrabarConsumoLotePadSteam", objParametros) <> "")
            Catch ex As Exception
                Return False
            Finally
                lobjTinto.Dispose()
                objUtil = Nothing
            End Try
        End Function
        Private Function GrabarConsumoTMDB(ByRef dsDatos As DataSet) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "ntx_Calculo", lobjUtil.GeneraXml(dsDatos.Tables("DatosCalculo")), _
                                                    "ntx_DatosTMDB", lobjUtil.GeneraXml(dsDatos.Tables("DatosTMDB")), _
                                                    "ntx_Formulacion", lobjUtil.GeneraXml(dsDatos.Tables("Formulacion")), _
                                                    "ntx_RecetasAsignadas", lobjUtil.GeneraXml(dsDatos.Tables("RecetasAsignadas")), _
                                                    "ntx_Partidas", lobjUtil.GeneraXml(dsDatos.Tables("Partidas")), _
                                                    "ntx_Consumos", lobjUtil.GeneraXml(dsDatos.Tables("Consumos")), _
                                                    "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_Lote_GrabarConsumoLoteTMDB", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("codigo_lote")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
#End Region
#Region "       Cálculo"
        Public Function CalcularConsumo(ByRef dtConsumo As DataTable, ByRef dtPartidas As DataTable, ByVal dblVolumenPartida As Double, ByVal dblVolumenTotal As Double, ByVal intTipoAplicacion As Integer, Optional ByVal intColumnasFijas As Integer = 8) As Boolean
            Dim ldvPartidasBloqueadas As DataView
            Dim lintNroPartidasDespachadas As Integer = 0
            Dim ldblVolumenBloqueado As Double = 0
            Dim ldblVolumenEfectivo As Double
            Dim lintColumnasAdicionales As Integer = 0
            Dim ldblVolumenResidual As Double = 0
            Dim i As Integer
            Dim j As Integer
            Dim lbooOk As Boolean = False
            Dim ldrFila As DataRow
            Dim lstrFiltro As String

            lstrFiltro = "estado=0 and tipo_aplicacion=" + CStr(intTipoAplicacion)
            If dblVolumenPartida = 0 Then
                dblVolumenPartida = dblVolumenTotal
            End If

            ldvPartidasBloqueadas = New DataView(dtPartidas, lstrFiltro, "secuencia_partida", DataViewRowState.CurrentRows)
            If ldvPartidasBloqueadas.Count > 0 Then
                lintNroPartidasDespachadas = ldvPartidasBloqueadas.Item(ldvPartidasBloqueadas.Count - 1).Item("secuencia_partida")
            End If
            For i = 0 To ldvPartidasBloqueadas.Count - 1
                ldblVolumenBloqueado = ldblVolumenBloqueado + ldvPartidasBloqueadas.Item(i).Item("volumen")
            Next i
            ldblVolumenEfectivo = dblVolumenTotal - ldblVolumenBloqueado
            If dblVolumenPartida = ldblVolumenEfectivo Then
                lintColumnasAdicionales = 1
            ElseIf dblVolumenPartida > ldblVolumenEfectivo Then
                lintColumnasAdicionales = 1
            Else
                If ldblVolumenEfectivo / dblVolumenPartida > CInt(ldblVolumenEfectivo / dblVolumenPartida) Then
                    lintColumnasAdicionales = CInt(ldblVolumenEfectivo / dblVolumenPartida) + 1
                Else
                    lintColumnasAdicionales = CInt(ldblVolumenEfectivo / dblVolumenPartida)
                End If
            End If
            If (dblVolumenTotal - (ldblVolumenBloqueado + (lintColumnasAdicionales * dblVolumenPartida))) < 0 Then
                ldblVolumenResidual = dblVolumenPartida + (dblVolumenTotal - (ldblVolumenBloqueado + (lintColumnasAdicionales * dblVolumenPartida)))
            Else
                ldblVolumenResidual = dblVolumenPartida - (dblVolumenTotal - (ldblVolumenBloqueado + (lintColumnasAdicionales * dblVolumenPartida)))
            End If
            If ldblVolumenResidual = 0 Then ldblVolumenResidual = dblVolumenPartida
            For i = dtConsumo.Columns.Count - 1 To lintNroPartidasDespachadas + intColumnasFijas Step -1
                dtConsumo.Columns.Remove(dtConsumo.Columns(i))
            Next i
            For i = 1 To lintColumnasAdicionales
                If i <> lintColumnasAdicionales Then
                    dtConsumo.Columns.Add(CStr(dblVolumenPartida) + "-" + CStr(lintNroPartidasDespachadas + i), GetType(Double))
                Else
                    dtConsumo.Columns.Add(CStr(ldblVolumenResidual) + "-" + CStr(lintNroPartidasDespachadas + i), GetType(Double))
                End If
            Next i
            dtConsumo.Columns.Add("Total", GetType(String))
            For i = 0 To dtConsumo.Rows.Count - 1
                For j = lintNroPartidasDespachadas + intColumnasFijas To dtConsumo.Columns.Count - 2
                    If j <> dtConsumo.Columns.Count - 2 Then
                        If dtConsumo.Rows(i).Item("estado") = 1 Then
                            dtConsumo.Rows(i).Item(j) = dtConsumo.Rows(i).Item("concentracion_gr_lt") * dblVolumenPartida / 1000
                        Else
                            dtConsumo.Rows(i).Item(j) = 0
                        End If
                    Else
                        If dtConsumo.Rows(i).Item("estado") = 1 Then
                            dtConsumo.Rows(i).Item(j) = dtConsumo.Rows(i).Item("concentracion_gr_lt") * ldblVolumenResidual / 1000
                        Else
                            dtConsumo.Rows(i).Item(j) = 0
                        End If
                    End If
                Next j
                dtConsumo.Rows(i).Item("Total") = 0
                For j = intColumnasFijas To dtConsumo.Columns.Count - 2
                    dtConsumo.Rows(i).Item("Total") = dtConsumo.Rows(i).Item("Total") + dtConsumo.Rows(i).Item(j)
                Next j
            Next i
            For i = dtPartidas.Rows.Count - 1 To 0 Step -1
                If dtPartidas.Rows(i).Item("secuencia_partida") > lintNroPartidasDespachadas Then
                    If dtPartidas.Rows(i).Item("tipo_aplicacion") = intTipoAplicacion Then
                        dtPartidas.Rows.Remove(dtPartidas.Rows(i))
                    End If
                End If
            Next i
            For i = 1 To lintColumnasAdicionales
                ldrFila = dtPartidas.NewRow
                ldrFila("secuencia_partida") = ldvPartidasBloqueadas.Count + i
                ldrFila("estado") = 1
                ldrFila("tipo_aplicacion") = intTipoAplicacion
                If i = lintColumnasAdicionales Then
                    ldrFila("volumen") = ldblVolumenResidual
                Else
                    ldrFila("volumen") = dblVolumenPartida
                End If
                dtPartidas.Rows.Add(ldrFila)
                ldrFila = Nothing
            Next i
            lbooOk = True
            Return lbooOk
        End Function
        Public Function CalcularConsumoBI(ByRef dtConsumo As DataTable, ByRef dtPartidas As DataTable, ByVal arrTinas() As Double, Optional ByVal intColumnasFijas As Integer = 8) As Boolean
            Dim ldtConsumoActual As DataTable
            Dim ldtConsumoBase As DataTable
            Dim ldvIQ As DataView
            Dim lstrFiltro As String = ""
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            Dim ldrFila As DataRow
            Dim lbooOk As Boolean = False

            ldtConsumoActual = dtConsumo.Copy
            ldtConsumoBase = dtConsumo.Clone
            If New DataView(dtPartidas, "tipo_aplicacion=0 and estado=0", "", DataViewRowState.CurrentRows).Count > 0 Then
                lbooOk = True
                Return lbooOk
                Exit Function
            End If
            For i = 0 To ldtConsumoActual.Rows.Count - 1
                lstrFiltro = "codigo_insumo='" + ldtConsumoActual.Rows(i).Item("codigo_insumo") + "'"
                ldvIQ = New DataView(ldtConsumoBase, lstrFiltro, "", DataViewRowState.CurrentRows)
                If ldvIQ.Count = 0 Then
                    ldrFila = ldtConsumoBase.NewRow
                    ldrFila.Item("codigo_receta") = ldtConsumoActual.Rows(i).Item("codigo_receta")
                    ldrFila.Item("codigo_insumo") = ldtConsumoActual.Rows(i).Item("codigo_insumo")
                    ldrFila.Item("nombre_insumo") = ldtConsumoActual.Rows(i).Item("nombre_insumo")
                    If ldtConsumoActual.Rows(i).Item("tipo") = 0 Then
                        ldrFila.Item("concentracion_gr_lt") = ldtConsumoActual.Rows(i).Item("concentracion_base") / ((UBound(arrTinas, 1) + 1) / 2)
                    Else
                        ldrFila.Item("concentracion_gr_lt") = ldtConsumoActual.Rows(i).Item("concentracion_base")
                    End If
                    ldrFila.Item("tipo") = ldtConsumoActual.Rows(i).Item("tipo")
                    ldrFila.Item("estado") = ldtConsumoActual.Rows(i).Item("estado")
                    ldrFila.Item("orden1") = ldtConsumoActual.Rows(i).Item("orden1")
                    ldrFila.Item("orden2") = ldtConsumoActual.Rows(i).Item("orden2")
                    ldrFila.Item("secuencia_tina") = 0
                    ldrFila.Item("tina") = ""
                    ldrFila.Item("consumo") = 0
                    ldrFila.Item("total") = 0
                    ldrFila.Item("concentracion_base") = ldtConsumoActual.Rows(i).Item("concentracion_base")
                    ldtConsumoBase.Rows.Add(ldrFila)
                End If
            Next i
            ldtConsumoActual = ldtConsumoBase.Clone
            For i = 0 To ldtConsumoBase.Rows.Count - 1
                For j = 0 To (UBound(arrTinas, 1) - 1) / 2
                    ldrFila = ldtConsumoActual.NewRow

                    ldrFila.Item("codigo_receta") = ldtConsumoBase.Rows(i).Item("codigo_receta")
                    ldrFila.Item("codigo_insumo") = ldtConsumoBase.Rows(i).Item("codigo_insumo")
                    ldrFila.Item("nombre_insumo") = ldtConsumoBase.Rows(i).Item("nombre_insumo")
                    If ldtConsumoBase.Rows(i).Item("tipo") = 0 Then
                        ldrFila.Item("concentracion_gr_lt") = ldtConsumoBase.Rows(i).Item("concentracion_base") / ((UBound(arrTinas, 1) + 1) / 2)
                    Else
                        ldrFila.Item("concentracion_gr_lt") = ldtConsumoBase.Rows(i).Item("concentracion_base")
                    End If
                    ldrFila.Item("tipo") = ldtConsumoBase.Rows(i).Item("tipo")
                    ldrFila.Item("estado") = ldtConsumoBase.Rows(i).Item("estado")
                    ldrFila.Item("orden1") = ldtConsumoBase.Rows(i).Item("orden1")
                    ldrFila.Item("orden2") = ldtConsumoBase.Rows(i).Item("orden2")
                    ldrFila.Item("secuencia_tina") = arrTinas(2 * j)
                    ldrFila.Item("tina") = "TINA " + Convert.ToString(CInt(arrTinas(2 * j)))
                    ldrFila.Item("consumo") = arrTinas((2 * j) + 1) * ldrFila.Item("concentracion_gr_lt") / 1000
                    ldrFila.Item("total") = 0
                    ldrFila.Item("concentracion_base") = ldtConsumoBase.Rows(i).Item("concentracion_base")
                    ldtConsumoActual.Rows.Add(ldrFila)
                Next j
            Next
            lstrFiltro = "tipo_aplicacion=0"
            ldvIQ = New DataView(dtPartidas, lstrFiltro, "", DataViewRowState.CurrentRows)
            If ldvIQ.Count = 0 Then
                ldrFila = dtPartidas.NewRow
                ldrFila.Item("secuencia_partida") = 1
                ldrFila.Item("tipo_aplicacion") = 0
                ldrFila.Item("estado") = 1
                ldrFila.Item("volumen") = 0
                dtPartidas.Rows.Add(ldrFila)
                ldrFila = Nothing
            End If
            dtConsumo = ldtConsumoActual
            lbooOk = True
            Return lbooOk
        End Function
        Public Function VolumenTotal(ByVal dblVolumenFichas As Double, ByVal dblVolumenArtesa As Double, ByVal dblVolumenRefuerzo As Double, ByVal dblVolumenRecuperado As Double, ByVal dblFactor As Double) As Double
            Dim ldblVolumenBruto As Double
            Dim ldblVolumenCalculado As Double
            ldblVolumenBruto = dblVolumenFichas + dblVolumenArtesa + dblVolumenRefuerzo - dblVolumenRecuperado
            If (ldblVolumenBruto / dblFactor) > CInt((ldblVolumenBruto / dblFactor)) Then
                ldblVolumenCalculado = (CInt((ldblVolumenBruto / dblFactor)) + 1) * dblFactor
            Else
                ldblVolumenCalculado = (CInt((ldblVolumenBruto / dblFactor))) * dblFactor
            End If
            Return ldblVolumenCalculado
        End Function
#End Region
        Public Function ValidarStock(ByRef pdtConsumo As DataTable, ByVal pintColumnas As Integer, Optional ByRef pdtErrores As DataTable = Nothing) As Boolean
            Dim lbooOk As Boolean
            Dim lobjCon As AccesoDatosSQLServer
            Dim lobjUtil As New NM_General.Util
            Dim lstrXML As String
            Dim ldtRes As DataTable

            pdtConsumo.TableName = "Detalle"
            lstrXML = lobjUtil.GeneraXml(pdtConsumo)
            lobjUtil = Nothing
            Dim lstrParametros() As String = {"ntx_Consumo", lstrXML, _
                            "sin_Columnas", pintColumnas}
            Try
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldtRes = lobjCon.ObtenerDataTable("usp_TIN_Lote_VerificarStock", lstrParametros)
                If ldtRes.Rows.Count > 0 Then
                    pdtErrores = ldtRes
                    lbooOk = False
                Else
                    lbooOk = True
                End If
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        'Public Function Errores_toString() As String
        '    Dim lstrErrores As String
        '    Dim i As Integer
        '    lstrErrores = ""
        '    If mcolErrores.Count > 0 Then
        '        For i = 1 To mcolErrores.Count
        '            If lstrErrores.Length = 0 Then
        '                lstrErrores = CStr(mcolErrores.Item(i))
        '            Else
        '                lstrErrores = lstrErrores & "/n " & CStr(mcolErrores.Item(i))
        '            End If
        '        Next i
        '    End If
        '    Return lstrErrores
        'End Function

        Public Function ListarFichasLote(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String, ByVal intTipo As Integer, ByVal intFase As Integer, ByVal strColor As String) As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Try
                Dim lstrParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "sin_CodigoEtapa", intFase, _
                                "sin_Tipo", intTipo, _
                                "var_Color", strColor}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_ListarFichasLotes", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mdsSet = Nothing
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function BuscarLote() As Boolean
            Dim lbooOk As Boolean = False
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarLote", lstrParametros)
                With mdsSet.Tables
                    With .Item(0).Rows(0)
                        mintFase = .Item("tipo")
                        mstrCodigoMaquina = .Item("codigo_maquina")
                        mstrNombreMaquina = .Item("nombre_maquina")
                        mstrCodigoOperacion = .Item("codigo_operacion")
                        mstrNombreOperacion = .Item("descripcion_operacion")
                        mdblDensidad = .Item("densidad")
                        mdblMetros = .Item("metros")
                        mdblVelocidad = .Item("velocidad")
                        mdblPickUp = .Item("pickup")
                        mdatFechaCreacion = .Item("fecha_creacion")
                        mintEstado = .Item("estado")
                        mintActivo = .Item("activo")
                    End With
                    Fichas = .Item(1)
                End With
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mstrCodigoMaquina = ""
                mstrNombreMaquina = ""
                mstrCodigoOperacion = ""
                mstrNombreOperacion = ""
                mdblDensidad = 0
                mdblMetros = 0
                mdblVelocidad = 0
                mdblPickUp = 0
                mintEstado = 0
                mintActivo = 1
                mdatFechaCreacion = Now
                Fichas = EsquemaFichasLote()
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function BuscarLote(ByVal intTipoCalculo As enuTiposCalculo) As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Dim lintNroTablas As Integer
            Dim lstrTablas() As String
            Dim i As Integer
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Select Case intTipoCalculo
                    Case enuTiposCalculo.Thermosol
                        ReDim lstrTablas(5)
                        lstrTablas(0) = "Cabecera"
                        lstrTablas(1) = "Subprocesos"
                        lstrTablas(2) = "Recetas"
                        lstrTablas(3) = "Calculo"
                        lstrTablas(4) = "Partidas"
                        lstrTablas(5) = "Consumo"
                        mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarThermosol", lstrParametros)
                    Case enuTiposCalculo.Acabado
                        ReDim lstrTablas(5)
                        lstrTablas(0) = "Cabecera"
                        lstrTablas(1) = "Subprocesos"
                        lstrTablas(2) = "Recetas"
                        lstrTablas(3) = "Calculo"
                        lstrTablas(4) = "Partidas"
                        lstrTablas(5) = "Consumo"
                        mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarAcabado", lstrParametros)
                    Case enuTiposCalculo.Lavado
                        ReDim lstrTablas(7)
                        lstrTablas(0) = "Cabecera"
                        lstrTablas(1) = "Subprocesos"
                        lstrTablas(2) = "Recetas"
                        lstrTablas(3) = "Calculo"
                        lstrTablas(4) = "Partidas"
                        lstrTablas(5) = "Tinas"
                        lstrTablas(6) = "Consumo"
                        lstrTablas(7) = "ConsumoRefuerzo1"
                        mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarLavado", lstrParametros)
                    Case enuTiposCalculo.PadSteam
                        ReDim lstrTablas(10)
                        lstrTablas(0) = "Cabecera"
                        lstrTablas(1) = "Subprocesos"
                        lstrTablas(2) = "Formulacion"
                        lstrTablas(3) = "Calculo"
                        lstrTablas(4) = "Tinas"
                        lstrTablas(5) = "Partidas"
                        lstrTablas(6) = "InsumosRecetas"
                        lstrTablas(7) = "InsumosRefuerzo"
                        lstrTablas(8) = "BanioInicial"
                        lstrTablas(9) = "Fichas"
                        lstrTablas(10) = "TinaXInsumo"
                        mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarPadSteam", lstrParametros)
                    Case enuTiposCalculo.TMDB
                        ReDim lstrTablas(10)
                        lstrTablas(0) = "Cabecera"
                        lstrTablas(1) = "Calculo"
                        lstrTablas(2) = "CalculoTMDB"
                        lstrTablas(3) = "Fichas"
                        lstrTablas(4) = "SubProcesos"
                        lstrTablas(5) = "RecetasAgrupadas"
                        lstrTablas(6) = "RecetasSimples"
                        lstrTablas(7) = "RecetasAsignadas"
                        lstrTablas(8) = "Formulacion"
                        lstrTablas(9) = "Partidas"
                        lstrTablas(10) = "Consumos"
                        mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarTMDB", lstrParametros)
                End Select
                If IsNothing(mdsSet) OrElse mdsSet.Tables.Count <> UBound(lstrTablas, 1) + 1 Then
                    lbooOk = False
                    mdsSet = Nothing
                Else
                    lbooOk = True
                    For i = 0 To UBound(lstrTablas, 1)
                        mdsSet.Tables(i).TableName = lstrTablas(i)
                    Next i
                    Select Case intTipoCalculo
                        Case enuTiposCalculo.TMDB
                            FormatearSet_TMDB(mdsSet)
                        Case enuTiposCalculo.PadSteam
                            FormatearSet_PadSteam(mdsSet)
                    End Select
                End If
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Private Sub FormatearSet_PadSteam(ByRef dsSetDatos As DataSet)
            Dim ldtPartidasOxidacion As DataTable
            Dim ldtPartidasReduccion As DataTable
            Dim larrKey() As DataColumn

            ReDim larrKey(0)
            larrKey(0) = dsSetDatos.Tables("Tinas").Columns("secuencia_tina")
            dsSetDatos.Tables("Tinas").PrimaryKey = larrKey

            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("InsumosRecetas").Columns("codigo_receta")
            larrKey(1) = dsSetDatos.Tables("InsumosRecetas").Columns("codigo_insumo")
            dsSetDatos.Tables("InsumosRecetas").PrimaryKey = larrKey

            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("InsumosRefuerzo").Columns("codigo_receta")
            larrKey(1) = dsSetDatos.Tables("InsumosRefuerzo").Columns("codigo_insumo")
            dsSetDatos.Tables("InsumosRefuerzo").PrimaryKey = larrKey

            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("BanioInicial").Columns("secuencia_tina")
            larrKey(1) = dsSetDatos.Tables("BanioInicial").Columns("codigo_insumo")
            dsSetDatos.Tables("BanioInicial").PrimaryKey = larrKey

            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("Fichas").Columns("codigo_lote")
            larrKey(1) = dsSetDatos.Tables("Fichas").Columns("codigo_ficha")
            dsSetDatos.Tables("Fichas").PrimaryKey = larrKey

            ldtPartidasReduccion = dsSetDatos.Tables("Partidas").Clone
            ldtPartidasOxidacion = dsSetDatos.Tables("Partidas").Clone
            For Each dtrItem As DataRow In dsSetDatos.Tables(5).Select("tipo_aplicacion=0")
                ldtPartidasReduccion.LoadDataRow(dtrItem.ItemArray, True)
            Next
            ldtPartidasReduccion.TableName = "PartidasReduccion"
            ldtPartidasReduccion.AcceptChanges()
            For Each dtrItem As DataRow In dsSetDatos.Tables(5).Select("tipo_aplicacion=1")
                ldtPartidasOxidacion.LoadDataRow(dtrItem.ItemArray, True)
            Next
            ldtPartidasOxidacion.TableName = "PartidasOxidacion"
            ldtPartidasOxidacion.AcceptChanges()
            dsSetDatos.Tables.Add(ldtPartidasOxidacion)
            dsSetDatos.Tables.Add(ldtPartidasReduccion)
        End Sub
        Private Sub FormatearSet_TMDB(ByRef dsSetDatos As DataSet)
            Dim larrKey() As DataColumn
            'Llave de subprocesos
            ReDim larrKey(0)
            larrKey(0) = dsSetDatos.Tables("SubProcesos").Columns("codigo_subproceso")
            dsSetDatos.Tables("SubProcesos").PrimaryKey = larrKey
            'Llave de recetas agrupadas
            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("RecetasAgrupadas").Columns("codigo_subproceso")
            larrKey(1) = dsSetDatos.Tables("RecetasAgrupadas").Columns("codigo_receta")
            dsSetDatos.Tables("RecetasAgrupadas").PrimaryKey = larrKey
            'Llave de recetas
            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("RecetasSimples").Columns("codigo_receta_grupo")
            larrKey(1) = dsSetDatos.Tables("RecetasSimples").Columns("codigo_receta")
            dsSetDatos.Tables("RecetasSimples").PrimaryKey = larrKey
            'Llave de recetas asignadas
            ReDim larrKey(0)
            larrKey(0) = dsSetDatos.Tables("RecetasAsignadas").Columns("codigo_receta")
            dsSetDatos.Tables("RecetasAsignadas").PrimaryKey = larrKey
            'Llave de formulaciones
            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("Formulacion").Columns("codigo_receta")
            larrKey(1) = dsSetDatos.Tables("Formulacion").Columns("codigo_insumo")
            dsSetDatos.Tables("Formulacion").PrimaryKey = larrKey
            'Llave de partidas
            ReDim larrKey(1)
            larrKey(0) = dsSetDatos.Tables("Partidas").Columns("tipo_aplicacion")
            larrKey(1) = dsSetDatos.Tables("Partidas").Columns("secuencia_partida")
            dsSetDatos.Tables("Partidas").PrimaryKey = larrKey
            'Llave de consumos
            ReDim larrKey(3)
            larrKey(0) = dsSetDatos.Tables("Consumos").Columns("tipo_aplicacion")
            larrKey(1) = dsSetDatos.Tables("Consumos").Columns("secuencia_partida")
            larrKey(2) = dsSetDatos.Tables("Consumos").Columns("secuencia_tina")
            larrKey(3) = dsSetDatos.Tables("Consumos").Columns("codigo_insumo")
            dsSetDatos.Tables("Consumos").PrimaryKey = larrKey

            'Creando estructura de consumos
            Dim ldrRecetaAsignada As DataRow
            Dim lstrZona As String
            Dim lstrReceta As String
            Dim lstrTipo As String
            Dim lintTipoAplicacion
            Dim ldtConsumo As DataTable
            Dim ldrConsumo As DataRow
            Dim ldrFilaNueva As DataRow
            Dim ldrPartida As DataRow
            Dim lstrNombreColumna As String
            Dim lbooAddTotal As Boolean
            Dim lstrKey(1) As String

            For Each ldrRecetaAsignada In dsSetDatos.Tables("RecetasAsignadas").Select("", "tipo_aplicacion")
                With ldrRecetaAsignada
                    lstrZona = .Item("codigo_zona")
                    lstrReceta = .Item("codigo_receta")
                    lstrTipo = CStr(.Item("tipo"))
                    lintTipoAplicacion = .Item("tipo_aplicacion")
                End With
                ldtConsumo = New DataTable(lstrZona + lstrReceta + lstrTipo)
                With ldtConsumo.Columns
                    .Add("codigo_receta", GetType(String))
                    .Add("codigo_insumo", GetType(String))
                    .Add("nombre_insumo", GetType(String))
                    .Add("concentracion", GetType(Double))
                    .Add("orden1", GetType(Integer))
                    .Add("orden2", GetType(String))
                    ReDim larrKey(1)
                    larrKey(0) = .Item("codigo_receta")
                    larrKey(1) = .Item("codigo_insumo")
                End With
                ldtConsumo.PrimaryKey = larrKey
                For Each ldrConsumo In dsSetDatos.Tables("Consumos").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "orden1 , orden2")
                    ldrFilaNueva = ldtConsumo.NewRow
                    With ldrFilaNueva
                        .Item("codigo_receta") = lstrReceta
                        .Item("codigo_insumo") = ldrConsumo.Item("codigo_insumo")
                        .Item("nombre_insumo") = ldrConsumo.Item("nombre_insumo")
                        .Item("concentracion") = ldrConsumo.Item("concentracion")
                        .Item("orden1") = ldrConsumo.Item("orden1")
                        .Item("orden2") = ldrConsumo.Item("orden2")
                    End With
                    ldtConsumo.LoadDataRow(ldrFilaNueva.ItemArray, True)
                Next ldrConsumo
                Select Case lstrTipo
                    Case "0"
                        ldtConsumo.Columns.Add("consumo_50", GetType(Double))
                        ldtConsumo.Columns.Add("consumo_100", GetType(Double))
                        ldtConsumo.Columns.Add("total", GetType(Double))
                    Case "1"
                        lbooAddTotal = False
                        ldtConsumo.Columns.Add("concentracion_inicial", GetType(Double))
                        ldtConsumo.Columns.Add("consumo_inicial", GetType(Double))
                        ldtConsumo.Columns.Add("concentracion_refuerzo", GetType(Double))
                        For Each ldrPartida In dsSetDatos.Tables("Partidas").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "secuencia_partida")
                            lstrNombreColumna = CStr(CInt(ldrPartida.Item("volumen"))) + "-" + CStr(CInt(ldrPartida.Item("secuencia_partida")))
                            ldtConsumo.Columns.Add(lstrNombreColumna, GetType(Double))
                            If ldrPartida.Item("estado") = 0 Then
                                ldtConsumo.Columns(lstrNombreColumna).ReadOnly = True
                            End If
                            lbooAddTotal = True
                        Next ldrPartida
                        If lbooAddTotal Then
                            ldtConsumo.Columns.Add("total", GetType(Double))
                        End If
                        For Each ldrConsumo In dsSetDatos.Tables("Consumos").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "secuencia_partida , secuencia_tina")

                        Next ldrConsumo
                    Case "2"
                        lbooAddTotal = False
                        If dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("volumen_tanque_1") > 0 Then
                            ldtConsumo.Columns.Add("tanque1", GetType(Double))
                        End If
                        If dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("volumen_tanque_2") > 0 Then
                            ldtConsumo.Columns.Add("tanque2", GetType(Double))
                        End If
                        ldtConsumo.Columns.Add("concentracion_refuerzo", GetType(Double))
                        For Each ldrPartida In dsSetDatos.Tables("Partidas").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "secuencia_partida")
                            lstrNombreColumna = CStr(CInt(ldrPartida.Item("volumen"))) + "-" + CStr(CInt(ldrPartida.Item("secuencia_partida")))
                            ldtConsumo.Columns.Add(lstrNombreColumna, GetType(Double))
                            If ldrPartida.Item("estado") = 0 Then
                                If ldrPartida.Item("secuencia_partida") = 1 AndAlso dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("volumen_tanque_1") > 0 Then
                                    ldtConsumo.Columns("tanque1").ReadOnly = True
                                End If
                                If ldrPartida.Item("secuencia_partida") = 1 AndAlso dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("volumen_tanque_2") > 0 Then
                                    ldtConsumo.Columns("tanque2").ReadOnly = True
                                End If
                                ldtConsumo.Columns(lstrNombreColumna).ReadOnly = True
                            End If
                            lbooAddTotal = True
                        Next ldrPartida
                        If lbooAddTotal Then
                            ldtConsumo.Columns.Add("total", GetType(Double))
                        End If
                End Select

                Select Case lstrTipo
                    Case "0"
                        For Each ldrConsumo In dsSetDatos.Tables("Consumos").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "orden1 , orden2")
                            lstrKey(0) = lstrReceta
                            lstrKey(1) = ldrConsumo.Item("codigo_insumo")
                            ldrFilaNueva = ldtConsumo.Rows.Find(lstrKey)
                            ldrFilaNueva.BeginEdit()
                            If ldrFilaNueva.Item("codigo_insumo") = "030101010054" Then
                                ldrFilaNueva.Item("consumo_100") = ldrConsumo.Item("cantidad")
                                ldrFilaNueva.Item("consumo_50") = ldrConsumo.Item("cantidad") / 0.7665
                                ldrFilaNueva.Item("total") = ldrConsumo.Item("cantidad")
                            Else
                                ldrFilaNueva.Item("consumo_100") = ldrConsumo.Item("cantidad")
                                ldrFilaNueva.Item("consumo_50") = ldrConsumo.Item("cantidad")
                                ldrFilaNueva.Item("total") = ldrConsumo.Item("cantidad")
                            End If
                            ldrFilaNueva.AcceptChanges()
                        Next
                    Case "1"
                        For Each ldrConsumo In dsSetDatos.Tables("Consumos").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "orden1 , orden2")
                            lstrKey(0) = lstrReceta
                            lstrKey(1) = ldrConsumo.Item("codigo_insumo")
                            ldrFilaNueva = ldtConsumo.Rows.Find(lstrKey)
                            ldrFilaNueva.BeginEdit()
                            ldrFilaNueva.Item("concentracion_inicial") = (ldrConsumo.Item("concentracion") * 100) / _
                                    ((dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("pickup_final") _
                                    - dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("pickup_inicial")) + _
                                    (dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("pickup_inicial") * 0.8))
                            If ldrConsumo.Item("secuencia_tina") = 1 Then
                                ldrFilaNueva.Item("consumo_inicial") = ldrConsumo.Item("cantidad")
                            Else
                                lstrNombreColumna = CStr(CInt(ldrConsumo.Item("volumen"))) + "-" + CStr(CInt(ldrConsumo.Item("secuencia_partida")))
                                ldrFilaNueva.Item(lstrNombreColumna) = ldrConsumo.Item("cantidad")
                            End If
                            If IsDBNull(ldrFilaNueva.Item("total")) Then ldrFilaNueva.Item("total") = 0
                            ldrFilaNueva.Item("total") = ldrFilaNueva.Item("total") + ldrConsumo.Item("cantidad")
                            ldrFilaNueva.AcceptChanges()
                        Next
                    Case "2"
                        For Each ldrConsumo In dsSetDatos.Tables("Consumos").Select("tipo_aplicacion=" + CStr(lintTipoAplicacion), "orden1 , orden2")
                            lstrKey(0) = lstrReceta
                            lstrKey(1) = ldrConsumo.Item("codigo_insumo")
                            ldrFilaNueva = ldtConsumo.Rows.Find(lstrKey)
                            ldrFilaNueva.BeginEdit()
                            ldrFilaNueva.Item("concentracion_refuerzo") = (ldrConsumo.Item("concentracion") * 100) * _
                                    ((dsSetDatos.Tables("CalculoTMDB").Select("codigo_zona='" + lstrZona + "'", "")(0).Item("factor_refuerzo")))
                            If ldrConsumo.Item("secuencia_tina") = 1 Then
                                ldrFilaNueva.Item("tanque1") = ldrConsumo.Item("cantidad")
                            ElseIf ldrConsumo.Item("secuencia_tina") = 2 Then
                                ldrFilaNueva.Item("tanque2") = ldrConsumo.Item("cantidad")
                            Else
                                lstrNombreColumna = CStr(CInt(ldrConsumo.Item("volumen"))) + "-" + CStr(CInt(ldrConsumo.Item("secuencia_partida")))
                                ldrFilaNueva.Item(lstrNombreColumna) = ldrConsumo.Item("cantidad")
                            End If
                            If IsDBNull(ldrFilaNueva.Item("total")) Then ldrFilaNueva.Item("total") = 0
                            ldrFilaNueva.Item("total") = ldrFilaNueva.Item("total") + ldrConsumo.Item("cantidad")
                            ldrFilaNueva.AcceptChanges()
                        Next
                End Select
                ldtConsumo.TableName = "@" + ldtConsumo.TableName
                dsSetDatos.Tables.Add(ldtConsumo)
            Next ldrRecetaAsignada
        End Sub
        Public Function ListarLotes(ByVal ParamArray Flag() As String) As Boolean
            Dim lobjLote As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False

            Try
                lobjLote = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                mdsSet = lobjLote.ObtenerDataSet("usp_TIN_Lote_ListarLotes", Flag)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjLote = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function EliminarLote() As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                "var_Usuario", mstrUsuario}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lobjTinto.ObtenerDataTable("usp_TIN_Lote_EliminarLote", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = True
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function ReporteCalculo() As Boolean
            Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo}
            Dim lobjCon As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            mdsSet = lobjCon.ObtenerDataSet("usp_TIN_Lote_Reporte", lstrParametros)
            With mdsSet
                .Tables(0).TableName = "GENERALES"
                .Tables(1).TableName = "FICHAS"
                .Tables(2).TableName = "APLICACIONES"
                .Tables(3).TableName = "INSUMOS_QUIMICOS"
                .Tables(4).TableName = "CONSUMOS"
                .Tables(5).TableName = "PARTIDAS"
                .Tables(6).TableName = "TINAS"
                .Tables(7).TableName = "AUDITORIA"
            End With
            lobjCon = Nothing
        End Function
        Public Function CambiarMaquina() As Boolean
            Dim lbooOk As Boolean = False
            Dim larrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                            "var_CodigoMaquina", mstrCodigoMaquina, _
                                            "var_Usuario", mstrUsuario}
            Dim lobjCon As AccesoDatosSQLServer
            Try
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lobjCon.EjecutarComando("usp_TIN_Lote_CambioMaquina", larrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
#End Region

#Region "Metodos de Luis_AJ"
        Public Function CambiarMaquinaLoteCerrado() As String
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrMensaje As String
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                "var_CodigoMaquina", mstrCodigoMaquina, _
                                                "var_Usuario", mstrUsuario}

                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lobjTinto.EjecutarComando("usp_TIN_Lote_CambioMaquinaLoteCerrado", lstrParametros)
                lstrMensaje = ""
            Catch ex As Exception
                lstrMensaje = ex.Message
            Finally
                lobjTinto = Nothing
            End Try

            Return lstrMensaje

        End Function
#End Region
    End Class
End Namespace
