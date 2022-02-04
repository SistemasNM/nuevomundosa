Imports NM.AccesoDatos
Namespace Lotes
    Public MustInherit Class MICalculo
        Implements IDisposable

#Region "   Variables"
        Public mstrUsuario As String
        Private mstrCodigo As String
        Private mintFase As Integer
        Private mobjMaquina As NuevoMundo.Tintoreria.Maestros.Maquina
        Private mobjOperacion As NuevoMundo.Tintoreria.Maestros.Operacion
        Private mdblDensidad As Double
        Private mdblMetros As Double
        Private mdblPickUp As Double
        Private mdblVelocidad As Double
        Private mdatFechaCreacion As DateTime
        Private mstrUsuarioCreacion As String
        Private mintEstado As Integer
        Private mintActivo As Integer
        Private mdtFichas As DataTable
        Private mdsSetDatos As DataSet
#End Region
#Region "   Enumeraciones"
        Enum enuFases
            [Pretratamiento] = 1
            [Acabado] = 2
            [Tenido] = 3
        End Enum
#End Region
#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
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
        Public Property Maquina() As NuevoMundo.Tintoreria.Maestros.Maquina
            Get
                Maquina = mobjMaquina
            End Get
            Set(ByVal Value As NuevoMundo.Tintoreria.Maestros.Maquina)
                mobjMaquina = Value
            End Set
        End Property
        Public Property Operacion() As NuevoMundo.Tintoreria.Maestros.Operacion
            Get
                Operacion = mobjOperacion
            End Get
            Set(ByVal Value As NuevoMundo.Tintoreria.Maestros.Operacion)
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
        Public Property SetDatos() As DataSet
            Get
                SetDatos = mdsSetDatos
            End Get
            Set(ByVal Value As DataSet)
                mdsSetDatos = Value
            End Set
        End Property
#End Region
#Region "   MustOverride"
        MustOverride Function Buscar() As Boolean
        MustOverride Function Grabar(ByVal dsSet As DataSet) As Boolean
        MustOverride Function SetearLlavesPrimarias() As Boolean
        MustOverride Function BloquearColumnas() As Boolean
#End Region
#Region "   Metodos públicos"
        Public Function VolumenLote(ByVal dblVolumenFichas As Double, ByVal dblVolumenArtesa As Double, ByVal dblVolumenRefuerzo As Double, ByVal dblVolumenRecuperado As Double, ByVal dblFactor As Double) As Double
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
        Public Function Orden1(ByVal strCodigo As String) As Integer 'Implements ICalculo.Orden1
            Dim lintOrden1 As Integer = 0
            If Left(strCodigo, 4) = "0302" Then
                lintOrden1 = 1
            Else
                lintOrden1 = 2
            End If
            If strCodigo = "030101010052" Then lintOrden1 = 3
            If strCodigo = "030101010054" Then lintOrden1 = 3
            Return lintOrden1
        End Function
        Public Function Orden2(ByVal strCodigo As String, ByVal strNombre As String) As String 'Implements ICalculo.Orden2
            Return strCodigo
        End Function
        Public Function CalcularConsumo(ByVal intTipoAplicacion As Integer, ByVal dblVolumenPartida As Double, ByVal dblVolumenLote As Double, ByRef dsSet As DataSet) As Boolean
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
            Dim lintColumnasFijas As Integer = 8

            lstrFiltro = "estado=0 and tipo_aplicacion=" + CStr(intTipoAplicacion)
            If dblVolumenPartida = 0 Then
                dblVolumenPartida = dblVolumenLote
            End If

            ldvPartidasBloqueadas = New DataView(dsSet.Tables("Partidas"), lstrFiltro, "secuencia_partida", DataViewRowState.CurrentRows)
            If ldvPartidasBloqueadas.Count > 0 Then
                lintNroPartidasDespachadas = ldvPartidasBloqueadas.Item(ldvPartidasBloqueadas.Count - 1).Item("secuencia_partida")
            End If
            For i = 0 To ldvPartidasBloqueadas.Count - 1
                ldblVolumenBloqueado = ldblVolumenBloqueado + ldvPartidasBloqueadas.Item(i).Item("volumen")
            Next i
            ldblVolumenEfectivo = dblVolumenLote - ldblVolumenBloqueado
            If dblVolumenPartida = ldblVolumenEfectivo Then
                lintColumnasAdicionales = 1
            ElseIf dblVolumenPartida > ldblVolumenEfectivo Then
                If ldblVolumenEfectivo = 0 Then
                    lintColumnasAdicionales = 0
                Else
                    lintColumnasAdicionales = 1
                End If
            Else
                If ldblVolumenEfectivo / dblVolumenPartida > CInt(ldblVolumenEfectivo / dblVolumenPartida) Then
                    lintColumnasAdicionales = CInt(ldblVolumenEfectivo / dblVolumenPartida) + 1
                Else
                    lintColumnasAdicionales = CInt(ldblVolumenEfectivo / dblVolumenPartida)
                End If
            End If
            If (dblVolumenLote - (ldblVolumenBloqueado + (lintColumnasAdicionales * dblVolumenPartida))) < 0 Then
                ldblVolumenResidual = dblVolumenPartida + (dblVolumenLote - (ldblVolumenBloqueado + (lintColumnasAdicionales * dblVolumenPartida)))
            Else
                ldblVolumenResidual = dblVolumenPartida - (dblVolumenLote - (ldblVolumenBloqueado + (lintColumnasAdicionales * dblVolumenPartida)))
            End If
            If ldblVolumenResidual = 0 Then ldblVolumenResidual = dblVolumenPartida
            For i = dsSet.Tables("Consumos").Columns.Count - 1 To lintNroPartidasDespachadas + lintColumnasFijas Step -1
                dsSet.Tables("Consumos").Columns.Remove(dsSet.Tables("Consumos").Columns(i))
            Next i
            For i = 1 To lintColumnasAdicionales
                If i <> lintColumnasAdicionales Then
                    dsSet.Tables("Consumos").Columns.Add(CStr(dblVolumenPartida) + "-" + CStr(lintNroPartidasDespachadas + i), GetType(Double))
                Else
                    dsSet.Tables("Consumos").Columns.Add(CStr(ldblVolumenResidual) + "-" + CStr(lintNroPartidasDespachadas + i), GetType(Double))
                End If
            Next i
            dsSet.Tables("Consumos").Columns.Add("Total", GetType(String))
            For i = 0 To dsSet.Tables("Consumos").Rows.Count - 1
                For j = lintNroPartidasDespachadas + lintColumnasFijas To dsSet.Tables("Consumos").Columns.Count - 2
                    If j <> dsSet.Tables("Consumos").Columns.Count - 2 Then
                        If dsSet.Tables("Consumos").Rows(i).Item("estado") = 1 Then
                            dsSet.Tables("Consumos").Rows(i).Item(j) = dsSet.Tables("Consumos").Rows(i).Item("concentracion_gr_lt") * dblVolumenPartida / 1000
                        Else
                            dsSet.Tables("Consumos").Rows(i).Item(j) = 0
                        End If
                    Else
                        If dsSet.Tables("Consumos").Rows(i).Item("estado") = 1 Then
                            dsSet.Tables("Consumos").Rows(i).Item(j) = dsSet.Tables("Consumos").Rows(i).Item("concentracion_gr_lt") * ldblVolumenResidual / 1000
                        Else
                            dsSet.Tables("Consumos").Rows(i).Item(j) = 0
                        End If
                    End If
                Next j
                dsSet.Tables("Consumos").Rows(i).Item("Total") = 0
                For j = lintColumnasFijas To dsSet.Tables("Consumos").Columns.Count - 2
                    dsSet.Tables("Consumos").Rows(i).Item("Total") = dsSet.Tables("Consumos").Rows(i).Item("Total") + dsSet.Tables("Consumos").Rows(i).Item(j)
                Next j
            Next i
            For i = dsSet.Tables("Partidas").Rows.Count - 1 To 0 Step -1
                If dsSet.Tables("Partidas").Rows(i).Item("secuencia_partida") > lintNroPartidasDespachadas Then
                    If dsSet.Tables("Partidas").Rows(i).Item("tipo_aplicacion") = intTipoAplicacion Then
                        dsSet.Tables("Partidas").Rows.Remove(dsSet.Tables("Partidas").Rows(i))
                    End If
                End If
            Next i
            For i = 1 To lintColumnasAdicionales
                ldrFila = dsSet.Tables("Partidas").NewRow
                ldrFila("secuencia_partida") = ldvPartidasBloqueadas.Count + i
                ldrFila("estado") = 1
                ldrFila("tipo_aplicacion") = intTipoAplicacion
                If i = lintColumnasAdicionales Then
                    ldrFila("volumen") = ldblVolumenResidual
                Else
                    ldrFila("volumen") = dblVolumenPartida
                End If
                dsSet.Tables("Partidas").Rows.Add(ldrFila)
                ldrFila = Nothing
            Next i
            lbooOk = True
            Return lbooOk
        End Function
        Public Function CalcularConsumoBI(ByVal arrTinas() As Double, ByRef dsSet As DataSet) As Boolean
            Dim ldtConsumoActual As DataTable
            Dim ldtConsumoBase As DataTable
            Dim ldvIQ As DataView
            Dim lstrFiltro As String = ""
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            Dim ldrFila As DataRow
            Dim lbooOk As Boolean = False

            ldtConsumoActual = dsSet.Tables("Consumos").Copy
            ldtConsumoBase = dsSet.Tables("Consumos").Clone
            If New DataView(dsSet.Tables("Partidas"), "tipo_aplicacion=0 and estado=0", "", DataViewRowState.CurrentRows).Count > 0 Then
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
            ldvIQ = New DataView(dsSet.Tables("Partidas"), lstrFiltro, "", DataViewRowState.CurrentRows)
            If ldvIQ.Count = 0 Then
                ldrFila = dsSet.Tables("Partidas").NewRow
                ldrFila.Item("secuencia_partida") = 1
                ldrFila.Item("tipo_aplicacion") = 0
                ldrFila.Item("estado") = 1
                ldrFila.Item("volumen") = 0
                dsSet.Tables("Partidas").Rows.Add(ldrFila)
                ldrFila = Nothing
            End If
            dsSet.Tables.Remove(dsSet.Tables("Consumos"))
            ldtConsumoActual.TableName = "Consumos"
            dsSet.Tables.Add(ldtConsumoActual)
            lbooOk = True
            Return lbooOk
        End Function
        Public Function EliminarCalculo() As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Try
                Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lobjTinto.EjecutarComando("usp_Lote_EliminarCalculo", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Reporte() As DataSet
            Dim ldsRes As DataSet
            Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo}
            Dim lobjCon As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            ldsRes = lobjCon.ObtenerDataSet("usp_TIN_Lote_Reporte", lstrParametros)
            With ldsRes
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
            Return ldsRes
        End Function
#Region "   Esquemas"
        Public Function EsquemaSubprocesos() As DataTable
            Dim ldtSubprocesos As DataTable
            ldtSubprocesos = New DataTable("Subprocesos")
            ldtSubprocesos.Columns.Add("codigo_subproceso", GetType(String))
            ldtSubprocesos.Columns.Add("revision_subproceso", GetType(Integer))
            ldtSubprocesos.Columns.Add("descripcion_subproceso", GetType(String))
            Return ldtSubprocesos
        End Function
        Public Function EsquemaRecetas() As DataTable
            Dim ldtRecetas As DataTable
            ldtRecetas = New DataTable("Recetas")
            ldtRecetas.Columns.Add("codigo_subproceso", GetType(String))
            ldtRecetas.Columns.Add("codigo_receta", GetType(String))
            ldtRecetas.Columns.Add("revision_receta", GetType(Integer))
            ldtRecetas.Columns.Add("descripcion_receta", GetType(String))
            Return ldtRecetas
        End Function
        Public Function EsquemaPartidas() As DataTable
            Dim ldtPartidas As DataTable
            ldtPartidas = New DataTable("Partidas")
            ldtPartidas.Columns.Add("secuencia_partida", GetType(Integer))
            ldtPartidas.Columns.Add("tipo_aplicacion", GetType(Integer))
            ldtPartidas.Columns.Add("estado", GetType(Integer))
            ldtPartidas.Columns.Add("volumen", GetType(Integer))
            Return ldtPartidas
        End Function
        Public Function EsquemaConsumos() As DataTable
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
        Public Function EsquemaConsumosBI() As DataTable
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
        Public Function EsquemaTinas() As DataTable
            Dim ldtRes As DataTable
            ldtRes = New DataTable("Tinas")
            ldtRes.Columns.Add("tina", GetType(String))
            ldtRes.Columns.Add("volumen", GetType(Double))
            ldtRes.Columns.Add("temperatura", GetType(Double))
            Return ldtRes
        End Function
#End Region
#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            mobjMaquina = Nothing
            mobjOperacion = Nothing
            mdtFichas = Nothing
            mdsSetDatos = Nothing
        End Sub
    End Class
    Friend Class CalculoGeneral
        Inherits MICalculo

        Public Overrides Function BloquearColumnas() As Boolean

        End Function

        Public Overrides Function Buscar() As Boolean

        End Function

        Public Overrides Function Grabar(ByVal dsSet As System.Data.DataSet) As Boolean

        End Function

        Public Overrides Function SetearLlavesPrimarias() As Boolean

        End Function
    End Class
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
        Private mintEstado As Integer
        Private mintActivo As Integer
        Private mstrUsuario As String
        Private mobjFichas As FichasXLote
        Private mdsSet As DataSet
        Private mobjCalculo As MICalculo
        Private mintPartidasDespachadas As Integer = 0
    Private mintPartidasPendientes As Integer = 0
    Private mstrFechaUltimoDespacho As String
#End Region

#Region "   Constructor"
    Sub New(ByVal strUsuario)
      mstrUsuario = strUsuario
      mobjMaquina = New Maestros.Maquina(strUsuario)
      mobjOperacion = New Maestros.Operacion(strUsuario)
      mobjFichas = New FichasXLote(mstrUsuario)
      mobjCalculo = New CalculoGeneral
    End Sub
    Sub New(ByVal strUsuario, ByVal strCodigoLote)
      mstrUsuario = strUsuario
      mstrCodigo = strCodigoLote
      mobjMaquina = New Maestros.Maquina(strUsuario)
      mobjOperacion = New Maestros.Operacion(strUsuario)
      mobjFichas = New FichasXLote(strUsuario, strCodigoLote)
      mobjCalculo = New CalculoGeneral
      mobjCalculo.Codigo = strCodigoLote
    End Sub
#End Region

#Region "   Enumeraciones"
    Enum enuFases
      [Pretratamiento] = 1
      [Acabado] = 2
      [Tenido] = 3
    End Enum
#End Region

#Region "   Propiedades"
    Public Property CodigoLote() As String
      Get
        CodigoLote = mstrCodigo
      End Get
      Set(ByVal Value As String)
        mstrCodigo = Value
        mobjCalculo.Codigo = Value
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
        Public Property Fichas() As FichasXLote
            Get
                Fichas = mobjFichas
            End Get
            Set(ByVal Value As FichasXLote)
                Value.Datos.TableName = "Fichas"
                mobjFichas = Value
            End Set
        End Property
        Public ReadOnly Property Calculo() As MICalculo
            Get
                Calculo = mobjCalculo
            End Get
        End Property
        Public ReadOnly Property SetDatos() As DataSet
            Get
                SetDatos = mdsSet
            End Get
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
#End Region

#Region "   Esquemas"
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
      mobjFichas.Datos = EsquemaFichasLote()

      mdsSet = Nothing
    End Function
    Public Function Grabar() As Boolean
      Dim lobjUtil As NM_General.Util
      Dim lobjTinto As AccesoDatosSQLServer
      Dim ldtRes As DataTable
      Dim lbooOk As Boolean = False
      Try
        lobjUtil = New NM_General.Util
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim lstrParametros() As String = {"var_CodigoLote", mstrCodigo, _
                                            "sin_Fase", mintFase, _
                                            "var_CodigoMaquina", mobjMaquina.Codigo, _
                                            "var_CodigoOperacion", mobjOperacion.Codigo, _
                                            "num_Densidad", mdblDensidad, _
                                            "num_Metros", mdblMetros, _
                                            "num_PickUp", mdblPickUp, _
                                            "num_Velocidad", mdblVelocidad, _
                                            "ntx_Fichas", lobjUtil.GeneraXml(mobjFichas.Datos), _
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
        mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_BuscarLote", lstrParametros)
        With mdsSet.Tables
          With .Item(0).Rows(0)
            mintFase = .Item("tipo")
            mobjMaquina.Codigo = .Item("codigo_maquina")
            mobjMaquina.Nombre = .Item("nombre_maquina")
            mobjOperacion.Codigo = .Item("codigo_operacion")
            mobjOperacion.Nombre = .Item("descripcion_operacion")
            mdblDensidad = .Item("densidad")
            mdblMetros = .Item("metros")
            mdblVelocidad = .Item("velocidad")
            mdblPickUp = .Item("pickup")
                        mdatFechaCreacion = .Item("fecha_creacion")
                        mstrUsuarioCreacion = .Item("usuario_creacion")
            mintEstado = .Item("estado")
            mintActivo = .Item("activo")
            mintPartidasPendientes = .Item("partidas_pendientes")
            mintPartidasDespachadas = .Item("partidas_despachadas")
            mstrFechaUltimoDespacho = .Item("ultimo_despacho")
          End With
          Fichas.Datos = .Item(1)
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
        Fichas.Datos = EsquemaFichasLote()
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
        'usp_TIN_LoteInsumos_Obtener

#End Region
  End Class
    Public Class FichasXLote
        Private mstrUsuario As String
        Private mstrCodigoLote As String
        Private mdtFichas As DataTable
        Private mdtCabecera As DataTable
        Protected Friend Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub
        Protected Friend Sub New(ByVal strUsuario As String, ByVal strCodigoLote As String)
            mstrUsuario = strUsuario
            mstrCodigoLote = strCodigoLote
        End Sub

        Public Property Datos() As DataTable
            Get
                Datos = mdtFichas
            End Get
            Set(ByVal Value As DataTable)
                Value.TableName = "Fichas"
                mdtFichas = Value
            End Set
        End Property
        Public Property Cabecera() As DataTable
            Get
                Cabecera = mdtCabecera
            End Get
            Set(ByVal Value As DataTable)
                mdtCabecera = Value
            End Set
        End Property
        Public Function Buscar(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String, _
                            ByVal intFase As Integer, ByVal strColor As String, _
                            ByVal strCodigoFicha As String) As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Try
                Dim lstrParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "sin_CodigoEtapa", intFase, _
                                "var_Color", strColor, _
                                "var_Ficha", strCodigoFicha}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                mdtFichas = lobjTinto.ObtenerDataTable("usp_TIN_Lote_BuscarFichaLote", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mdtFichas = Nothing
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Listar(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String, ByVal intTipo As Integer, ByVal intFase As Integer, ByVal strColor As String) As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lbooOk As Boolean = False
            Dim ldsSet As DataSet
            Try
                Dim lstrParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "sin_CodigoEtapa", intFase, _
                                "sin_Tipo", intTipo, _
                                "var_Color", strColor}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_ListarFichasLotes", lstrParametros)
                'ldsSet = lobjTinto.ObtenerDataSet("usp_TIN_LoteFichas_Listar", lstrParametros)
                mdtCabecera = ldsSet.Tables(0)
                mdtFichas = ldsSet.Tables(1)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mdtCabecera = Nothing
                mdtFichas = Nothing
            Finally
                lobjTinto = Nothing
            End Try
            Return lbooOk
        End Function

    End Class
End Namespace
