Imports NM.AccesoDatos
Imports System.IO

Namespace Lotes.Generico

    Public Class Lote

#Region "   Variables"
        Private mstrCodigo As String
        Private mintFase As Integer
        Private mobjMaquina As Maestros.Maquina
        Private mobjOperacion As Maestros.Operacion
        Private mdblDensidad As Double
        Private mdblMetros As Double
        Private mdblPickUp As Double
        Private mdblVelocidad As Double
        Private mdatFechaCreacion As DateTime
        Private mstrUsuarioCreacion As String
        Private mdatFechaModificacion As DateTime
        Private mstrUsuarioModificacion As String
        Private mintEstado As Integer
        Private mintActivo As Integer
        Private mstrUsuario As String
        Private mintPartidasDespachadas As Integer = 0
        Private mintPartidasPendientes As Integer = 0
        Private mstrFechaUltimoDespacho As String
        Private mstrCentroCosto As String
        Private mstrCodigoMotivo As String
        Private mstrLoteReferencia As String
        Private mdsSet As DataSet
        Private mobjFichas As LoteFichas
        Private mobjInsumos As LoteInsumos
        Private objCalculo As Generico.LoteCalculo
        Private mintCodigoBarras As Integer
        Private mblnEstadoIntercambio As Boolean
        Private mstrFichaIntercambio As String
        'REQSIS201700031 - DG - INI
        Private mdblVolSotckIni As Double
        Private mdblVolRequerido As Double
        Private mdblVolDespachado As Double
        Private mdblVolStockFin As Double
        Private mdblVolConsumido As Double
        Private mdblVolDev As Double
        'REQSIS201700031 - DG - FIN
#End Region

#Region "   Constructor"
        Sub New(ByVal strUsuario)
            mstrUsuario = strUsuario
            mobjMaquina = New Maestros.Maquina(strUsuario)
            mobjOperacion = New Maestros.Operacion(strUsuario)
            mobjFichas = New Lotes.Generico.LoteFichas
            objCalculo = New Generico.LoteCalculo

        End Sub
        Sub New(ByVal strUsuario, ByVal strCodigoLote)
            mstrUsuario = strUsuario
            mstrCodigo = strCodigoLote
            mobjMaquina = New Maestros.Maquina(strUsuario)
            mobjOperacion = New Maestros.Operacion(strUsuario)
            mobjFichas = New Lotes.Generico.LoteFichas
            objCalculo = New Generico.LoteCalculo
            'mobjFichas = New FichasXLote(strUsuario, strCodigoLote)
        End Sub
#End Region

#Region "   Enumeraciones"
        Enum enuFases
            [Pretratamiento] = 1
            [Tenido] = 2
            [Acabado] = 3
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
        Public Property Maquina() As Maestros.Maquina
            Get
                Maquina = mobjMaquina
            End Get
            Set(ByVal Value As Maestros.Maquina)
                mobjMaquina = Value
            End Set
        End Property
        Public Property Operacion() As Maestros.Operacion
            Get
                Operacion = mobjOperacion
            End Get
            Set(ByVal Value As Maestros.Operacion)
                mobjOperacion = Value
            End Set
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
        Public Property CentroCosto() As String
            Get
                CentroCosto = mstrCentroCosto
            End Get
            Set(ByVal Value As String)
                mstrCentroCosto = Value
            End Set
        End Property
        Public Property CodigoMotivo() As String
            Get
                CodigoMotivo = mstrCodigoMotivo
            End Get
            Set(ByVal Value As String)
                mstrCodigoMotivo = Value
            End Set
        End Property
        Public Property LoteReferencia() As String
            Get
                LoteReferencia = mstrLoteReferencia
            End Get
            Set(ByVal Value As String)
                mstrLoteReferencia = Value
            End Set
        End Property
        Public ReadOnly Property FechaCreacion() As DateTime
            Get
                FechaCreacion = mdatFechaCreacion
            End Get
        End Property
        Public ReadOnly Property UsuarioCreacion() As String
            Get
                UsuarioCreacion = mstrUsuarioCreacion
            End Get
        End Property
        Public ReadOnly Property FechaModificacion() As DateTime
            Get
                FechaModificacion = mdatFechaModificacion
            End Get
        End Property
        Public ReadOnly Property UsuarioModificacion() As String
            Get
                UsuarioModificacion = mstrUsuarioModificacion
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
        Public ReadOnly Property PartidasPendientes() As Integer
            Get
                PartidasPendientes = mintPartidasPendientes
            End Get
        End Property
        Public ReadOnly Property PartidasDespachadas() As Integer
            Get
                PartidasDespachadas = mintPartidasDespachadas
            End Get
        End Property
        Public ReadOnly Property FechaUltimoDespacho() As String
            Get
                FechaUltimoDespacho = mstrFechaUltimoDespacho
            End Get
        End Property
        Public Property Fichas() As LoteFichas
            Get
                Return mobjFichas
            End Get
            Set(ByVal Value As LoteFichas)
                mobjFichas = Value
            End Set
        End Property
        Public Property Insumos() As LoteInsumos
            Get
                Return mobjInsumos
            End Get
            Set(ByVal Value As LoteInsumos)
                mobjInsumos = Value
            End Set
        End Property
        Public Property Calculo() As LoteCalculo
            Get
                Return objCalculo
            End Get
            Set(ByVal Value As LoteCalculo)
                objCalculo = Value
            End Set
        End Property
        Public Property CodigoBarras() As Integer
            Get
                CodigoBarras = mintCodigoBarras
            End Get
            Set(ByVal Value As Integer)
                mintCodigoBarras = Value
            End Set
        End Property

        Public Property FichaIntercambio() As String
            Get
                FichaIntercambio = mstrFichaIntercambio
            End Get
            Set(ByVal Value As String)
                mstrFichaIntercambio = Value
            End Set
        End Property


        Public Property EstadoIntercambio() As Boolean
            Get
                Return mblnEstadoIntercambio
            End Get
            Set(ByVal value As Boolean)
                mblnEstadoIntercambio = value
            End Set
        End Property
        'REQSIS201700031 - DG - INI
        Public Property VolSotckIni() As Double
            Get
                Return mdblVolSotckIni
            End Get
            Set(ByVal Value As Double)
                mdblVolSotckIni = Value
            End Set
        End Property
        Public Property VolRequerido() As Double
            Get
                Return mdblVolRequerido
            End Get
            Set(ByVal Value As Double)
                mdblVolRequerido = Value
            End Set
        End Property
        Public Property VolDespachado() As Double
            Get
                Return mdblVolDespachado
            End Get
            Set(ByVal Value As Double)
                mdblVolDespachado = Value
            End Set
        End Property
        Public Property VolStockFin() As Double
            Get
                Return mdblVolStockFin
            End Get
            Set(ByVal Value As Double)
                mdblVolStockFin = Value
            End Set
        End Property
        Public Property VolConsumido() As Double
            Get
                Return mdblVolConsumido
            End Get
            Set(ByVal Value As Double)
                mdblVolConsumido = Value
            End Set
        End Property
        Public Property VolDev() As Double
            Get
                Return mdblVolDev
            End Get
            Set(ByVal Value As Double)
                mdblVolDev = Value
            End Set
        End Property
        'REQSIS201700031 - DG - FIN
#End Region

#Region "   Esquemas"
        'Private Function EsquemaFichasLote() As DataTable
        '    Dim ldtFichas As DataTable
        '    ldtFichas = New DataTable("Fichas")
        '    ldtFichas.Columns.Add("codigo_lote", GetType(String))
        '    ldtFichas.Columns.Add("codigo_ficha", GetType(String))
        '    ldtFichas.Columns.Add("tipo", GetType(String))
        '    ldtFichas.Columns.Add("secuencia", GetType(Integer))
        '    ldtFichas.Columns.Add("peso", GetType(Double))
        '    ldtFichas.Columns.Add("metros", GetType(Double))
        '    ldtFichas.Columns.Add("velocidad", GetType(Double))
        '    ldtFichas.Columns.Add("pickup", GetType(Double))
        '    ldtFichas.Columns.Add("codigo_color", GetType(String))
        '    ldtFichas.Columns.Add("codigo_motivo", GetType(String))
        '    ldtFichas.Columns.Add("secuencia_reproceso", GetType(String))
        '    ldtFichas.Columns.Add("codigo_tipo_colorante", GetType(String))
        '    Return ldtFichas
        'End Function
#End Region

#Region "   Metodos"
        Public Function Limpiar()
            mstrCodigo = ""
            mintFase = enuFases.Pretratamiento
            mobjMaquina = New Maestros.Maquina(mstrUsuario)
            mobjOperacion = New Maestros.Operacion(mstrUsuario)
            mdblDensidad = 0
            mdblMetros = 0
            mdblPickUp = 0
            mdblVelocidad = 0
            mdsSet = Nothing
        End Function
        Public Function Grabar(ByVal dtbFichas As DataTable, ByVal dtbInsumos As DataTable, ByVal dtbPartidas As DataTable) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                'REQSIS201700031 - DG - INI
                'Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                '                                    "sin_Fase", mintFase, _
                '                                    "var_CodigoMaquina", mobjMaquina.Codigo, _
                '                                    "var_CodigoOperacion", mobjOperacion.Codigo, _
                '                                    "num_Densidad", mdblDensidad, _
                '                                    "num_Metros", mdblMetros, _
                '                                    "num_PickUp", mdblPickUp, _
                '                                    "num_Velocidad", mdblVelocidad, _
                '                                    "var_CodigoReceta", Me.Calculo.CodigoReceta, _
                '                                    "num_VolPartida", Me.Calculo.VolumenPartida, _
                '                                    "num_NumPartida", Me.Calculo.TotalPartidas, _
                '                                    "ntx_Fichas", lobjUtil.GeneraXml(dtbFichas), _
                '                                    "ntx_Insumos", lobjUtil.GeneraXml(dtbInsumos), _
                '                                    "ntx_Partidas", lobjUtil.GeneraXml(dtbPartidas), _
                '                                    "var_Usuario", mstrUsuario}
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "sin_Fase", mintFase, _
                                                    "var_CodigoMaquina", mobjMaquina.Codigo, _
                                                    "var_CodigoOperacion", mobjOperacion.Codigo, _
                                                    "num_Densidad", mdblDensidad, _
                                                    "num_Metros", mdblMetros, _
                                                    "num_PickUp", mdblPickUp, _
                                                    "num_Velocidad", mdblVelocidad, _
                                                    "var_CodigoReceta", Me.Calculo.CodigoReceta, _
                                                    "num_VolPartida", Me.Calculo.VolumenPartida, _
                                                    "num_NumPartida", Me.Calculo.TotalPartidas, _
                                                    "ntx_Fichas", lobjUtil.GeneraXml(dtbFichas), _
                                                    "ntx_Insumos", lobjUtil.GeneraXml(dtbInsumos), _
                                                    "ntx_Partidas", lobjUtil.GeneraXml(dtbPartidas), _
                                                    "num_VolStockIni", mdblVolSotckIni, _
                                                    "num_VolRequerido", mdblVolRequerido, _
                                                    "num_VolDespachado", mdblVolDespachado, _
                                                    "num_VolStockFin", mdblVolStockFin, _
                                                    "num_VolConsumido", mdblVolConsumido, _
                                                    "var_Usuario", mstrUsuario}
                'REQSIS201700031 - DG - FIN
                'ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_LoteInsumos_Grabar", lstrParametros)
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_LoteInsumos_Grabar_General", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("CODIGO_LOTE")
                If mstrCodigo <> "" Then
                    lbooOk = True
                Else
                    lbooOk = False
                End If
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Cerrar() As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "var_Usuario", mstrUsuario}
                lobjTinto.EjecutarComando("usp_TIN_Lote_CerrarLote", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
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
        Public Function Buscar() As Boolean
            Dim lbooOk As Boolean = False
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_Obtener", lstrParametros)
                With mdsSet.Tables
                    With .Item(0).Rows(0)
                        mintFase = .Item("tipo")
                        mobjMaquina.Codigo = .Item("codigo_maquina")
                        mobjMaquina.Nombre = .Item("nombre_maquina")
                        mobjMaquina.FactorCilindro = .Item("FACTOR_CILINDRO")
                        mobjOperacion.Codigo = .Item("codigo_operacion")
                        mobjOperacion.Nombre = .Item("descripcion_operacion")
                        mdblDensidad = .Item("densidad")
                        mdblMetros = .Item("metros")
                        mdblVelocidad = .Item("velocidad")
                        mdblPickUp = .Item("pickup")
                        mdatFechaCreacion = .Item("fecha_creacion")
                        mstrUsuarioCreacion = .Item("usuario_creacion")
                        mdatFechaModificacion = .Item("fecha_Modificacion")
                        mstrUsuarioModificacion = .Item("usuario_Modificacion")
                        mintEstado = .Item("estado")
                        mintActivo = .Item("activo")
                        mintPartidasPendientes = .Item("partidas_pendientes")
                        mintPartidasDespachadas = .Item("partidas_despachadas")
                        mstrFechaUltimoDespacho = .Item("ultimo_despacho")
                        mintCodigoBarras = .Item("codigo_barras")
                        mblnEstadoIntercambio = .Item("EstadoIntercambioF")
                    End With
                    Fichas.Datos = .Item(1)
                    Insumos = New LoteInsumos(mstrCodigo)
                End With
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mobjMaquina = New Maestros.Maquina(mstrUsuario)
                mobjOperacion = New Maestros.Operacion(mstrUsuario)
                mdblDensidad = 0
                mdblMetros = 0
                mdblVelocidad = 0
                mdblPickUp = 0
                mintEstado = 0
                mintActivo = 1
                mdatFechaCreacion = Now
                Insumos = New LoteInsumos
                'Fichas.Datos = EsquemaFichasLote()
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Buscar_codigobarras(ByVal pdbl_codigobarras As Double) As Boolean
            Dim lbooOk As Boolean = False
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"pnum_codigobarras", pdbl_codigobarras}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                mdsSet = lobjTinto.ObtenerDataSet("usp_tin_lote_buscarcodigobarras", lstrParametros)
                With mdsSet.Tables
                    With .Item(0).Rows(0)
                        mintFase = .Item("tipo")
                        mstrCodigo = .Item("codigo_lote")
                        mobjMaquina.Codigo = .Item("codigo_maquina")
                        mobjMaquina.Nombre = .Item("nombre_maquina")
                        mobjMaquina.FactorCilindro = .Item("FACTOR_CILINDRO")
                        mobjOperacion.Codigo = .Item("codigo_operacion")
                        mobjOperacion.Nombre = .Item("descripcion_operacion")
                        mdblDensidad = .Item("densidad")
                        mdblMetros = .Item("metros")
                        mdblVelocidad = .Item("velocidad")
                        mdblPickUp = .Item("pickup")
                        mdatFechaCreacion = .Item("fecha_creacion")
                        mstrUsuarioCreacion = .Item("usuario_creacion")
                        mdatFechaModificacion = .Item("fecha_Modificacion")
                        mstrUsuarioModificacion = .Item("usuario_Modificacion")
                        mintEstado = .Item("estado")
                        mintActivo = .Item("activo")
                        mintPartidasPendientes = .Item("partidas_pendientes")
                        mintPartidasDespachadas = .Item("partidas_despachadas")
                        mstrFechaUltimoDespacho = .Item("ultimo_despacho")
                    End With
                    Fichas.Datos = .Item(1)
                    Insumos = New LoteInsumos(mstrCodigo)
                End With
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mobjMaquina = New Maestros.Maquina(mstrUsuario)
                mobjOperacion = New Maestros.Operacion(mstrUsuario)
                mdblDensidad = 0
                mdblMetros = 0
                mdblVelocidad = 0
                mdblPickUp = 0
                mintEstado = 0
                mintActivo = 1
                mdatFechaCreacion = Now
                Insumos = New LoteInsumos
                'Fichas.Datos = EsquemaFichasLote()
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Listar(ByVal ParamArray Flag() As String) As Boolean
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

        Public Function Eliminar() As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                "var_Usuario", mstrUsuario}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                'lobjTinto.ObtenerDataTable("usp_TIN_Lote_EliminarLote", lstrParametros)
                lobjTinto.EjecutarComando("usp_TIN_Lote_EliminarLote", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function CambiarMaquina() As Boolean
            Dim lbooOk As Boolean = False
            Dim larrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                            "var_CodigoMaquina", mobjMaquina.Codigo, _
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

        Public Function ActualizarParametros(ByVal pstrCodigoLote As String) As Boolean
            Dim lbooOk As Boolean = False
            Dim larrParametros() As String = {"var_CodigoLote", pstrCodigoLote, "var_Usuario", mstrUsuario}
            Dim lobjCon As AccesoDatosSQLServer
            Try
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lobjCon.EjecutarComando("usp_TIN_LotePickup_Actualizar", larrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function InsumosLote_Obtener(ByVal strCodigoLote As String, ByVal strCodigoReceta As String) As DataTable
            Dim objConexion As AccesoDatosSQLServer
            Dim objParametros() As String = {"var_CodigoReceta", strCodigoReceta, "var_CodigoLote", strCodigoLote}
            Try
                objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return objConexion.ObtenerDataTable("usp_TIN_LoteInsumos_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_ConsumoTeoricoLoteAlmacen_Detalle(ByVal strFechaInicio As String, ByVal strFechaFinal As String) As DataTable
            Dim objConexion As AccesoDatosSQLServer
            Dim objParametros() As String = {"var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
            Try
                objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return objConexion.ObtenerDataTable("usp_TIN_ConsumoInsumoVsTeorico_Reporte", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GrabarFichaIntercambio(ByVal dtbFichas As DataTable) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                  "var_FichaIntercambio", mstrFichaIntercambio, _
                                                  "ntx_Fichas", lobjUtil.GeneraXml(dtbFichas), _
                                                  "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_Lote_IntercambiarFicha", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("CODIGO_LOTE")
                If mstrCodigo <> "" Then
                    lbooOk = True
                Else
                    lbooOk = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        'REQSIS201700031 - DG - INI
        Public Function ObtenerDatosStock(ByVal strLote As String, ByVal strReceta As String, ByVal strMaquina As String, ByVal strOperacion As String) As DataTable
            Dim objConexion As AccesoDatosSQLServer
            Dim objParametros() As String = {"var_lote", strLote, "var_codigo_receta", strReceta, "var_CodigoMaquina", strMaquina, "var_CodigoOperacion", strOperacion}
            Try
                objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return objConexion.ObtenerDataTable("USP_OBTENER_STOCK_RECETA_MAQUINA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GrabarDevolucion() As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                                    "var_CodigoMaquina", mobjMaquina.Codigo, _
                                                    "var_CodigoOperacion", mobjOperacion.Codigo, _
                                                    "var_CodigoReceta", Me.Calculo.CodigoReceta, _
                                                    "var_VolumenDev", Me.mdblVolDev, _
                                                    "var_Usuario", mstrUsuario}
                ldtRes = lobjTinto.ObtenerDataTable("USP_GRABAR_DEVOLUCION", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("CODIGO_LOTE")
                If mstrCodigo <> "" Then
                    lbooOk = True
                Else
                    lbooOk = False
                End If
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function ActualizaLoteSinVolumenRequerido(ByVal sCodLote As String) As Boolean
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean = False
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", sCodLote}
                ldtRes = lobjTinto.ObtenerDataTable("USP_ACTUALIZA_LOTE_SIN_VOLUMEN_REQUERIDO", lstrParametros)
                mstrCodigo = ldtRes.Rows(0).Item("CODIGO_LOTE")
                If mstrCodigo <> "" Then
                    lbooOk = True
                Else
                    lbooOk = False
                End If
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function ValidarPartidasAbiertas(ByVal sCodLote As String) As Integer
            Dim lobjUtil As NM_General.Util
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim valor As Integer
            Try
                lobjUtil = New NM_General.Util
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_CodigoLote", sCodLote}
                ldtRes = lobjTinto.ObtenerDataTable("USP_VALIDA_PARTIDA_ABIERTA_POR_LOTE", lstrParametros)
                valor = Convert.ToInt32(ldtRes.Rows(0).Item("CANTIDAD").ToString())
            Catch ex As Exception
                Throw ex
            End Try
            Return valor
        End Function
        Public Function BuscarLotesAbiertos(ByVal sAnio As Integer, ByVal sMes As Integer, ByVal sUsuario As String) As DataSet
            Dim lobjTinto As AccesoDatosSQLServer
            Dim dt As DataSet
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_Anio", sAnio, "var_Mes", sMes, "vas_Usuario", sUsuario}
                dt = lobjTinto.ObtenerDataSet("USP_BUSCAR_LOTES_ABIERTOS_POR_PERIODO", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function ProcesarCierraLote(ByVal sAnio As Integer, ByVal sMes As Integer, ByVal sUsuario As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Dim dt As DataTable
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_Anio", sAnio, "var_Mes", sMes, "vas_Usuario", sUsuario}
                dt = lobjTinto.ObtenerDataTable("USP_PROCESAR_CIERRE_LOTE_TINTORERIA", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function ObtenerCierre(ByVal sAnio As Integer, ByVal sMes As Integer) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Dim dt As DataTable
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_Anio", sAnio, "var_Mes", sMes}
                dt = lobjTinto.ObtenerDataTable("USP_OBTENER_CIERRE_POR_ANIO_MES", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function BuscarFichaMetraje(ByVal sFicha As String) As String
            Dim lobjTinto As AccesoDatosSQLServer
            Dim metraje As String
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_ficha", sFicha}
                metraje = lobjTinto.ObtenerDataTable("USP_OBTENER_METRAJE_FICHA", lstrParametros).Rows(0).Item("METRAJE").ToString()
            Catch ex As Exception
                Throw ex
            End Try
            Return metraje
        End Function
        Public Function ValidarFicha(ByVal sFicha As String) As String
            Dim lobjTinto As AccesoDatosSQLServer
            Dim mensaje As String
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_ficha", sFicha}
                mensaje = lobjTinto.ObtenerDataTable("USP_VALIDAR_FICHA_TINTORERIA", lstrParametros).Rows(0).Item("MENSAJE").ToString()
            Catch ex As Exception
                Throw ex
            End Try
            Return mensaje

        End Function
        Public Function ActualizaMetrajeFicha(ByVal sFicha As String, ByVal sUsuario As String,ByVal sMetraje As Decimal) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Dim DT As DataTable
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Dim lstrParametros() As String = {"var_ficha", sFicha, "var_usuario", sUsuario, "var_metraje", sMetraje}
                DT = lobjTinto.ObtenerDataTable("USP_ACTUALIZAR_METRAJE_FICHA", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return DT
        End Function
        'REQSIS201700031 - DG - FIN
#End Region

    End Class
End Namespace


