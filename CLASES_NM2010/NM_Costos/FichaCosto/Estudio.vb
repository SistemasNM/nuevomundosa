Imports NM_General
Imports NM.AccesoDatos


Public Class Estudio

#Region "   Variables"

  Private mstrEquivalencia As String
  Private _strCodigoArticuloLargo As String
  Private _strCodigoArticuloCorto As String
  Private _strPrefijo As String
  Private _strGlosa As String
  Private mstrUsuario As String
  Private mdsRes As DataSet
  Private mbooOk As Boolean
  Private _objConexion As AccesoDatosSQLServer
  Private _dstSchema As New DataSet
  Private _dblTipoCambio As Double
  Private _strCodigoUrdimbre As String
  Private _strCodigoEngomado As String
  Private _strCodigoTelar As String
  Private _dblCostoEstampado As Double

  Private mstrNumeroRequisicion As String
  Private mstrDetalleRequisicion As Double



  Private _dblVariacionDimensional As Double

  Public gstrNroRequision As String
  Public gstrNroDetalle As String


#End Region

#Region "   Propiedades"
  Public Property Glosa() As String
    Get
      Return _strGlosa
    End Get
    Set(ByVal Value As String)
      _strGlosa = Value
    End Set
  End Property
  Public Property Prefijo() As String
    Get
      Return _strPrefijo
    End Get
    Set(ByVal Value As String)
      _strPrefijo = Value
    End Set
  End Property
  Public Property ArticuloLargo() As String
    Get
      Return _strCodigoArticuloLargo
    End Get
    Set(ByVal Value As String)
      _strCodigoArticuloLargo = Value
    End Set
  End Property

  Public Property ArticuloCorto() As String
    Get
      Return _strCodigoArticuloCorto
    End Get
    Set(ByVal Value As String)
      _strCodigoArticuloCorto = Value
    End Set
  End Property

  Public Property Equivalencia() As String
    Get
      Equivalencia = mstrEquivalencia
    End Get
    Set(ByVal Value As String)
      mstrEquivalencia = Value
    End Set
  End Property
  Public Property Usuario() As String
    Get
      Usuario = mstrUsuario
    End Get
    Set(ByVal Value As String)
      mstrUsuario = Value
    End Set
  End Property
'Adicionado por Darwin, esto es para agregar 2 campos en el maestro de Pre-Costos
    Public Property NumeroRequisicion() As String
    Get
      NumeroRequisicion = mstrNumeroRequisicion
    End Get
    Set(ByVal Value As String)
      mstrNumeroRequisicion = Value
    End Set
  End Property
 Public Property DetalleRequisicion() As Double
    Get
      DetalleRequisicion = mstrDetalleRequisicion
    End Get
    Set(ByVal Value As Double)
      mstrDetalleRequisicion = Value
    End Set
  End Property
'++++++++++++++++++++++++FIN++++++++++++++++++++++++++++++

  Public ReadOnly Property SetDatos() As DataSet
    Get
      SetDatos = mdsRes
    End Get
  End Property

  Public ReadOnly Property CostoEstampado() As Double
    Get
      CostoEstampado = _dblCostoEstampado
    End Get
  End Property





#End Region

#Region "   Constructor"
  Sub New(ByVal pstrUsuario As String)
    mstrUsuario = pstrUsuario
    mbooOk = False
    mstrEquivalencia = ""
    mdsRes = Nothing
  End Sub

  Sub New()
    mstrUsuario = ""
    mbooOk = False
    mstrEquivalencia = ""
    mdsRes = Nothing
  End Sub
#End Region

#Region "   Metodos"

    Public Function CreateSchema() As DataSet
        Dim dtbDatos As DataTable
        Dim dtcPrimary(0) As DataColumn


        _dstSchema = New DataSet
        'ARTICULO
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoUrdimbre", GetType(String))
        dtbDatos.Columns.Add("num_EncogimientoUrdimbre", GetType(Double))
        dtbDatos.Columns.Add("num_EncogimientoTrama", GetType(Double))
        dtbDatos.Columns.Add("int_AnchoCrudo", GetType(Int16))
        dtbDatos.Columns.Add("num_EficienciaTeorica", GetType(Double))
        dtbDatos.Columns.Add("num_EficienciaReal", GetType(Double))
        dtbDatos.Columns.Add("num_MermaTejeduria", GetType(Double))
        dtbDatos.Columns.Add("num_VariacionDimensional", GetType(Double))
        dtbDatos.Columns.Add("num_Velocidad1", GetType(Double))
        dtbDatos.Columns.Add("num_Velocidad2", GetType(Double))
        dtbDatos.Columns.Add("num_Velocidad3", GetType(Double))
        dtbDatos.Columns.Add("num_Velocidad4", GetType(Double))
        dtbDatos.Columns.Add("num_VelocidadTeorica", GetType(Double))
        dtbDatos.Columns.Add("num_GastoOperativo", GetType(Double))
        dtbDatos.Columns.Add("num_PorcentajeCompensacion", GetType(Double))
        dtbDatos.Columns.Add("num_TipoCambio", GetType(Double))
        dtbDatos.Columns.Add("var_TipoMaquina", GetType(String))
        dtbDatos.Columns.Add("var_NombreTipoMaquina", GetType(String))
        dtbDatos.Columns.Add("int_NumeroTelas", GetType(Int32))
        dtbDatos.Columns.Add("num_AnchoPeine", GetType(Double))
        dtbDatos.Columns.Add("var_Ligamento", GetType(String))
        dtbDatos.Columns.Add("var_DescArticulo1", GetType(String))
        dtbDatos.Columns.Add("var_DescArticulo2", GetType(String))
        dtbDatos.Columns.Add("var_Glosa", GetType(String))
        dtbDatos.Columns.Add("var_CodigoArticuloCorto", GetType(String))
        dtbDatos.Columns.Add("num_AnchoEstandar", GetType(Double))
        dtbDatos.Columns.Add("num_PesoOnzas", GetType(Double))
        dtbDatos.TableName = "Articulo"
        _dstSchema.Tables.Add(dtbDatos)

        'URDIMBRE
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("int_Secuencia", GetType(Int16))
        dtbDatos.Columns.Add("var_CodigoHilo", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionHilo", GetType(String))
        dtbDatos.Columns.Add("num_HilosPulgada", GetType(Double))
        dtbDatos.Columns.Add("int_NumeroHilos", GetType(Int16))
        dtbDatos.Columns.Add("num_TituloReal", GetType(Double))
        dtbDatos.Columns.Add("num_GramosMetro", GetType(Double))
        dtbDatos.Columns.Add("num_Velocidad", GetType(Double))
        dtbDatos.TableName = "Urdimbre"
        _dstSchema.Tables.Add(dtbDatos)

        'TRAMA
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("int_Secuencia", GetType(Int16))
        dtbDatos.Columns.Add("var_CodigoHilo", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionHilo", GetType(String))
        dtbDatos.Columns.Add("num_AnchoCrudo", GetType(Double))
        dtbDatos.Columns.Add("int_NumeroHilos", GetType(Double))
        dtbDatos.Columns.Add("num_TituloReal", GetType(Double))
        dtbDatos.Columns.Add("num_GramosMetro", GetType(Double))
        dtbDatos.TableName = "Trama"
        _dstSchema.Tables.Add(dtbDatos)

        'ENGOMADO (CRUDO/TED)
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoEngomado", GetType(String))
        dtbDatos.Columns.Add("var_CodigoTipo", GetType(String))
        dtbDatos.Columns.Add("num_Velocidad", GetType(Double))
        dtbDatos.Columns.Add("num_Estiraje", GetType(Double))
        dtbDatos.TableName = "Engomado"
        _dstSchema.Tables.Add(dtbDatos)

        'FORMULACION
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoEngomado", GetType(String))
        dtbDatos.Columns.Add("var_CodigoFase", GetType(String))
        dtbDatos.Columns.Add("var_NombreFase", GetType(String))
        dtbDatos.Columns.Add("var_CodigoReceta", GetType(String))
        dtbDatos.Columns.Add("num_Dosificacion", GetType(Double))
        dtbDatos.Columns.Add("num_Pickup", GetType(Double))
        dtbDatos.Columns.Add("var_NombreIQ1", GetType(String))
        dtbDatos.Columns.Add("var_NombreIQ2", GetType(String))
        dtbDatos.Columns.Add("var_CodigoIQ1", GetType(String))
        dtbDatos.Columns.Add("var_CodigoIQ2", GetType(String))
        dtbDatos.Columns.Add("num_DosisIQ1", GetType(Double))
        dtbDatos.Columns.Add("num_DosisIQ2", GetType(Double))
        dtbDatos.Columns.Add("var_UnidadIQ1", GetType(String))
        dtbDatos.Columns.Add("var_UnidadIQ2", GetType(String))
        dtbDatos.TableName = "Formulacion"
        _dstSchema.Tables.Add(dtbDatos)

        'RECETA
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoReceta", GetType(String))
        dtbDatos.Columns.Add("var_CodigoInsumo", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionInsumo", GetType(String))
        dtbDatos.Columns.Add("num_Concentracion", GetType(Double))
        dtbDatos.Columns.Add("num_PorcentajeGramosLitro", GetType(Double))
        ReDim dtcPrimary(1)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoReceta")
        dtcPrimary(1) = dtbDatos.Columns("var_CodigoInsumo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "Receta"
        _dstSchema.Tables.Add(dtbDatos)

        'RUTA TINTORERIA
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
        dtbDatos.Columns.Add("int_Secuencia", GetType(Int16))
        dtbDatos.Columns.Add("var_CodigoReceta", GetType(String))
        dtbDatos.Columns.Add("var_CodigoMaquina", GetType(String))
        dtbDatos.Columns.Add("var_CodigoOperacion", GetType(String))
        dtbDatos.Columns.Add("var_CodigoEtapa", GetType(String))
        dtbDatos.Columns.Add("num_VelocidadMaquina", GetType(Double))
        dtbDatos.Columns.Add("int_Pases", GetType(Int16))
        dtbDatos.Columns.Add("var_DescripcionEtapa", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionOperacion", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionMaquina", GetType(String))
        dtbDatos.TableName = "RutaTintoreria"
        _dstSchema.Tables.Add(dtbDatos)

        'PICKUP TINTORERIA
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("int_Secuencia", GetType(Int16))
        dtbDatos.Columns.Add("var_CodigoReceta", GetType(String))
        dtbDatos.Columns.Add("var_CodigoMaquina", GetType(String))
        dtbDatos.Columns.Add("var_CodigoOperacion", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionOperacion", GetType(String))
        dtbDatos.Columns.Add("num_Pickup", GetType(Double))
        dtbDatos.Columns.Add("num_Peso", GetType(Double))
        dtbDatos.TableName = "PickupTintoreria"
        ReDim dtcPrimary(1)
        dtcPrimary(0) = dtbDatos.Columns("int_Secuencia")
        dtcPrimary(1) = dtbDatos.Columns("var_CodigoOperacion")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "PickupTintoreria"
        _dstSchema.Tables.Add(dtbDatos)

        'RECETA - IQ EN RUTA TINTORERIA
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoReceta", GetType(String))
        dtbDatos.Columns.Add("var_CodigoInsumo", GetType(String))
        dtbDatos.Columns.Add("var_DescripcionInsumo", GetType(String))
        dtbDatos.Columns.Add("num_Concentracion", GetType(Double))
        ReDim dtcPrimary(1)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoReceta")
        dtcPrimary(1) = dtbDatos.Columns("var_CodigoInsumo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "RecetaInsumoTinto"
        _dstSchema.Tables.Add(dtbDatos)

        'COSTO INSUMO QUIMICO
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoInsumo", GetType(String))
        dtbDatos.Columns.Add("int_NumeroVersion", GetType(Integer))
        dtbDatos.Columns.Add("var_DescripcionInsumo", GetType(String))
        dtbDatos.Columns.Add("num_CostoPromedio", GetType(Double))
        dtbDatos.Columns.Add("num_Factor", GetType(Double))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoInsumo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoInsumo"
        _dstSchema.Tables.Add(dtbDatos)

        'COSTO DE HILO
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoHilo", GetType(String))
        dtbDatos.Columns.Add("int_VersionMP", GetType(Integer))
        dtbDatos.Columns.Add("int_VersionPrd", GetType(Integer))
        dtbDatos.Columns.Add("var_DescripcionHilo", GetType(String))
        dtbDatos.Columns.Add("num_CostoProm", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMP", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoHilo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoHilo"
        _dstSchema.Tables.Add(dtbDatos)

        'COSTO DE URDIDO
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        dtbDatos.Columns.Add("var_TipoCalculo", GetType(String))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticulo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoUrdido"
        _dstSchema.Tables.Add(dtbDatos)

        'COSTO DE ENGOMADO
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        dtbDatos.Columns.Add("var_TipoCalculo", GetType(String))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticulo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoEngomado"
        _dstSchema.Tables.Add(dtbDatos)

        'COSTO DE TELAR
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        dtbDatos.Columns.Add("var_TipoCalculo", GetType(String))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticulo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoTelar"
        _dstSchema.Tables.Add(dtbDatos)

        'Modificado: Alexander Torres Cardenas
        'Fecha: Mayo 2013
        'Objetivo: Costo de telares promedio calculado.

        'COSTO DE TELAR
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        dtbDatos.Columns.Add("var_TipoCalculo", GetType(String))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticulo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoTelarTemp"
        _dstSchema.Tables.Add(dtbDatos)


        'COSTO DE TINTORERIA
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoMaquina", GetType(String))
        dtbDatos.Columns.Add("var_NombreMaquina", GetType(String))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoMaquina")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoTinto"
        _dstSchema.Tables.Add(dtbDatos)

        'COSTO DE REVISION FINAL
        dtbDatos = New DataTable
        dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
        dtbDatos.Columns.Add("num_CostoMOV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoOTR", GetType(Double))
        dtbDatos.Columns.Add("num_CostoFV", GetType(Double))
        dtbDatos.Columns.Add("num_CostoMOF", GetType(Double))
        dtbDatos.Columns.Add("num_CostoCI", GetType(Double))
        ReDim dtcPrimary(0)
        dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticulo")
        dtbDatos.PrimaryKey = dtcPrimary
        dtbDatos.TableName = "CostoRevFin"
        _dstSchema.Tables.Add(dtbDatos)

        Return _dstSchema

    End Function

    Public Function DatosAlerta_Obtener(ByVal strCodigoTabla As String) As DataTable
        Dim objGeneral As New NM_General.Util
        Return objGeneral.DatosAlerta_Obtener(strCodigoTabla)
    End Function

    Public Function InicializaDataSet(ByVal dstDatos As DataSet) As DataSet
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() = {"var_CodigoEquivalencia", Me.mstrEquivalencia}

            _dstSchema = _objConexion.ObtenerDataSet("usp_FCO_DatosMaestrosEstudio_Obtener_2", objParametros)

            'CARGANDO ARTICULO
            For Each dtrDatos As DataRow In _dstSchema.Tables(0).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Articulo").NewRow
                dtrNuevo("var_CodigoUrdimbre") = dtrDatos("var_CodigoUrdimbre")
                Me._strCodigoUrdimbre = dtrDatos("var_CodigoUrdimbre")
                dtrNuevo("num_EncogimientoUrdimbre") = dtrDatos("num_EncogimientoUrdimbre")
                dtrNuevo("num_EncogimientoTrama") = dtrDatos("num_EncogimientoTrama")
                dtrNuevo("int_AnchoCrudo") = dtrDatos("int_AnchoCrudo")
                dtrNuevo("num_EficienciaTeorica") = dtrDatos("num_EficienciaTeorica")
                dtrNuevo("num_EficienciaReal") = dtrDatos("num_EficienciaReal")
                dtrNuevo("num_MermaTejeduria") = dtrDatos("num_MermaTejeduria")
                dtrNuevo("num_VariacionDimensional") = dtrDatos("num_VariacionDimensional")
                dtrNuevo("num_Velocidad1") = dtrDatos("num_Velocidad1")
                dtrNuevo("num_Velocidad2") = dtrDatos("num_Velocidad2")
                dtrNuevo("num_Velocidad3") = dtrDatos("num_Velocidad3")
                dtrNuevo("num_Velocidad4") = dtrDatos("num_Velocidad4")
                dtrNuevo("num_VelocidadTeorica") = dtrDatos("num_VelocidadTeorica")
                dtrNuevo("num_GastoOperativo") = dtrDatos("num_GastoOperativo")
                dtrNuevo("num_PorcentajeCompensacion") = dtrDatos("num_PorcentajeCompensacion")
                dtrNuevo("num_TipoCambio") = dtrDatos("num_TipoCambio")
                dtrNuevo("var_TipoMaquina") = dtrDatos("var_TipoMaquina")
                dtrNuevo("int_NumeroTelas") = dtrDatos("int_NumeroTelas")
                dtrNuevo("num_AnchoPeine") = dtrDatos("num_AnchoPeine")
                dtrNuevo("var_Ligamento") = dtrDatos("var_Ligamento")
                dtrNuevo("var_DescArticulo1") = dtrDatos("var_DescArticulo1")
                dtrNuevo("var_DescArticulo2") = dtrDatos("var_DescArticulo2")
                dtrNuevo("var_Glosa") = dtrDatos("var_Glosa")
                dtrNuevo("var_CodigoArticuloCorto") = dtrDatos("var_CodigoArticuloCorto")
                _strCodigoArticuloCorto = dtrDatos("var_CodigoArticuloCorto")
                dtrNuevo("num_AnchoEstandar") = dtrDatos("num_AnchoEstandar")
                dtrNuevo("num_PesoOnzas") = dtrDatos("num_PesoOnzas")
                dstDatos.Tables("Articulo").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO URDIMBRE
            For Each dtrDatos As DataRow In _dstSchema.Tables(1).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Urdimbre").NewRow
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencia")
                dtrNuevo("var_CodigoHilo") = dtrDatos("var_CodigoHilo")
                dtrNuevo("var_DescripcionHilo") = dtrDatos("var_DescripHilo")
                dtrNuevo("num_HilosPulgada") = dtrDatos("num_HilosPorPulgada")
                dtrNuevo("int_NumeroHilos") = dtrDatos("int_NumeroHilos")
                dtrNuevo("num_TituloReal") = dtrDatos("num_TituloReal")
                dtrNuevo("num_GramosMetro") = dtrDatos("num_GramosMetro")
                dtrNuevo("num_Velocidad") = dtrDatos("num_Velocidad")
                dstDatos.Tables("Urdimbre").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO TRAMA
            For Each dtrDatos As DataRow In _dstSchema.Tables(2).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Trama").NewRow
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencia")
                dtrNuevo("var_CodigoHilo") = dtrDatos("var_CodigoHilo")
                dtrNuevo("var_DescripcionHilo") = dtrDatos("var_DescripHilo")
                dtrNuevo("num_AnchoCrudo") = dtrDatos("int_AnchoCrudo")
                dtrNuevo("int_NumeroHilos") = dtrDatos("int_NumeroHilos")
                dtrNuevo("num_TituloReal") = dtrDatos("num_TituloReal")
                dtrNuevo("num_GramosMetro") = dtrDatos("num_GramosMetro")
                dstDatos.Tables("Trama").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO ENGOMADO
            For Each dtrDatos As DataRow In _dstSchema.Tables(3).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Engomado").NewRow
                dtrNuevo("var_CodigoEngomado") = dtrDatos("var_CodigoEngomado")
                Me._strCodigoEngomado = dtrDatos("var_CodigoEngomado")
                dtrNuevo("var_CodigoTipo") = dtrDatos("chr_Tipo")
                dtrNuevo("num_Estiraje") = dtrDatos("num_Estiraje")
                dtrNuevo("num_Velocidad") = dtrDatos("num_Velocidad")
                dstDatos.Tables("Engomado").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO FORMULACION
            For Each dtrDatos As DataRow In _dstSchema.Tables(4).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Formulacion").NewRow
                dtrNuevo("var_CodigoEngomado") = dtrDatos("var_CodigoEngomado")
                dtrNuevo("var_CodigoFase") = dtrDatos("var_CodigoFase")
                dtrNuevo("var_NombreFase") = dtrDatos("var_NombreFase")
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("num_Dosificacion") = dtrDatos("num_Dosificacion")
                dtrNuevo("num_Pickup") = dtrDatos("num_Pickup")
                dtrNuevo("var_NombreIQ1") = dtrDatos("var_NombreIQ1")
                dtrNuevo("var_NombreIQ2") = dtrDatos("var_NombreIQ2")
                dtrNuevo("var_CodigoIQ1") = dtrDatos("var_CodigoIQ1")
                dtrNuevo("var_CodigoIQ2") = dtrDatos("var_CodigoIQ2")
                dtrNuevo("num_DosisIQ1") = dtrDatos("num_DosisIQ1")
                dtrNuevo("num_DosisIQ2") = dtrDatos("num_DosisIQ2")
                dtrNuevo("var_UnidadIQ1") = dtrDatos("var_UnidadIQ1")
                dtrNuevo("var_UnidadIQ2") = dtrDatos("var_UnidadIQ2")
                dstDatos.Tables("Formulacion").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO IQ RECETA
            For Each dtrDatos As DataRow In _dstSchema.Tables(5).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Receta").NewRow
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoInsumo") = dtrDatos("var_CodigoInsumo")
                dtrNuevo("var_DescripcionInsumo") = dtrDatos("var_DescripcionInsumo")
                dtrNuevo("num_Concentracion") = dtrDatos("num_Concentracion")
                dtrNuevo("num_PorcentajeGramosLitro") = dtrDatos("num_PorcentajeGramosLitro")
                dstDatos.Tables("Receta").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO RUTA TINTORERIA
            For Each dtrDatos As DataRow In _dstSchema.Tables(6).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("RutaTintoreria").NewRow
                dtrNuevo("var_CodigoArticulo") = dtrDatos("var_CodigoArticuloLargo")
                _strCodigoArticuloLargo = dtrDatos("var_CodigoArticuloLargo")
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencial")
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoMaquina") = dtrDatos("var_CodigoMaquina")
                dtrNuevo("var_CodigoOperacion") = dtrDatos("var_CodigoOperacion")
                dtrNuevo("var_CodigoEtapa") = dtrDatos("var_CodigoEtapa")
                dtrNuevo("num_VelocidadMaquina") = dtrDatos("num_Velocidad")
                dtrNuevo("int_Pases") = dtrDatos("int_Pases")
                dtrNuevo("var_DescripcionEtapa") = dtrDatos("var_DescripEtapa")
                dtrNuevo("var_DescripcionOperacion") = dtrDatos("var_DescripOperacion")
                dtrNuevo("var_DescripcionMaquina") = dtrDatos("var_DescripMaquina")
                dstDatos.Tables("RutaTintoreria").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO IQ RECETA TINTORERIA
            For Each dtrDatos As DataRow In _dstSchema.Tables(7).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("RecetaInsumoTinto").NewRow
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoInsumo") = dtrDatos("var_CodigoInsumo")
                dtrNuevo("var_DescripcionInsumo") = dtrDatos("var_DescripcionInsumo")
                dtrNuevo("num_Concentracion") = dtrDatos("num_Concentracion")
                dstDatos.Tables("RecetaInsumoTinto").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO PICKUP TINTORERIA
            For Each dtrDatos As DataRow In _dstSchema.Tables(8).Select("", "int_Secuencia")
                Dim dtrNuevo As DataRow = dstDatos.Tables("PickupTintoreria").NewRow
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencia")
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoMaquina") = dtrDatos("var_CodigoMaquina")
                dtrNuevo("var_CodigoOperacion") = dtrDatos("var_CodigoOperacion")
                dtrNuevo("var_DescripcionOperacion") = dtrDatos("var_DescripcionOperacion")
                dtrNuevo("num_Pickup") = dtrDatos("num_Pickup")
                dtrNuevo("num_Peso") = dtrDatos("num_Peso")

                dstDatos.Tables("PickupTintoreria").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'Sacando los datos de estampado
            _dblCostoEstampado = _dstSchema.Tables(9).Rows(0).Item(0)

            'Sacando datos de la requisicion
            If IsDBNull(_dstSchema.Tables(9).Rows(0)(1)) = False Then
                gstrNroRequision = _dstSchema.Tables(9).Rows(0)(1)
                gstrNroDetalle = _dstSchema.Tables(9).Rows(0)(2)
            Else
                gstrNroRequision = ""
                gstrNroDetalle = ""
            End If

            dstDatos = CargarInsumosQuimicos(dstDatos, "Produccion")
            dstDatos = CargarCostoHilo(dstDatos, "Produccion")

            dstDatos = CargarCostoUrdido(dstDatos)
            dstDatos = CargarCostoEngomado(dstDatos)

            ' Modificado: Costo Promedio Telar
            ' Alexander Torres Cardenas
            ' Mayo 2013

            dstDatos = CargarCostoTelar(dstDatos)
            dstDatos = CargarCostoTinto(dstDatos)
            dstDatos = CargarCostoRevFin(dstDatos)

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function InicializaDataSet_V2(ByVal dstDatos As DataSet) As DataSet
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() = {"var_CodigoEquivalencia", Me.mstrEquivalencia}

            _dstSchema = _objConexion.ObtenerDataSet("usp_FCO_DatosMaestrosEstudio_Obtener_2_V2", objParametros)

            'CARGANDO ARTICULO
            For Each dtrDatos As DataRow In _dstSchema.Tables(0).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Articulo").NewRow
                dtrNuevo("var_CodigoUrdimbre") = dtrDatos("var_CodigoUrdimbre")
                Me._strCodigoUrdimbre = dtrDatos("var_CodigoUrdimbre")
                dtrNuevo("num_EncogimientoUrdimbre") = dtrDatos("num_EncogimientoUrdimbre")
                dtrNuevo("num_EncogimientoTrama") = dtrDatos("num_EncogimientoTrama")
                dtrNuevo("int_AnchoCrudo") = dtrDatos("int_AnchoCrudo")
                dtrNuevo("num_EficienciaTeorica") = dtrDatos("num_EficienciaTeorica")
                dtrNuevo("num_EficienciaReal") = dtrDatos("num_EficienciaReal")
                dtrNuevo("num_MermaTejeduria") = dtrDatos("num_MermaTejeduria")
                dtrNuevo("num_VariacionDimensional") = dtrDatos("num_VariacionDimensional")
                dtrNuevo("num_Velocidad1") = dtrDatos("num_Velocidad1")
                dtrNuevo("num_Velocidad2") = dtrDatos("num_Velocidad2")
                dtrNuevo("num_Velocidad3") = dtrDatos("num_Velocidad3")
                dtrNuevo("num_Velocidad4") = dtrDatos("num_Velocidad4")
                dtrNuevo("num_VelocidadTeorica") = dtrDatos("num_VelocidadTeorica")
                dtrNuevo("num_GastoOperativo") = dtrDatos("num_GastoOperativo")
                dtrNuevo("num_PorcentajeCompensacion") = dtrDatos("num_PorcentajeCompensacion")
                dtrNuevo("num_TipoCambio") = dtrDatos("num_TipoCambio")
                dtrNuevo("var_TipoMaquina") = dtrDatos("var_TipoMaquina")
                dtrNuevo("int_NumeroTelas") = dtrDatos("int_NumeroTelas")
                dtrNuevo("num_AnchoPeine") = dtrDatos("num_AnchoPeine")
                dtrNuevo("var_Ligamento") = dtrDatos("var_Ligamento")
                dtrNuevo("var_DescArticulo1") = dtrDatos("var_DescArticulo1")
                dtrNuevo("var_DescArticulo2") = dtrDatos("var_DescArticulo2")
                dtrNuevo("var_Glosa") = dtrDatos("var_Glosa")
                dtrNuevo("var_CodigoArticuloCorto") = dtrDatos("var_CodigoArticuloCorto")
                _strCodigoArticuloCorto = dtrDatos("var_CodigoArticuloCorto")
                dtrNuevo("num_AnchoEstandar") = dtrDatos("num_AnchoEstandar")
                dtrNuevo("num_PesoOnzas") = dtrDatos("num_PesoOnzas")
                dstDatos.Tables("Articulo").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO URDIMBRE
            For Each dtrDatos As DataRow In _dstSchema.Tables(1).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Urdimbre").NewRow
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencia")
                dtrNuevo("var_CodigoHilo") = dtrDatos("var_CodigoHilo")
                dtrNuevo("var_DescripcionHilo") = dtrDatos("var_DescripHilo")
                dtrNuevo("num_HilosPulgada") = dtrDatos("num_HilosPorPulgada")
                dtrNuevo("int_NumeroHilos") = dtrDatos("int_NumeroHilos")
                dtrNuevo("num_TituloReal") = dtrDatos("num_TituloReal")
                dtrNuevo("num_GramosMetro") = dtrDatos("num_GramosMetro")
                dtrNuevo("num_Velocidad") = dtrDatos("num_Velocidad")
                dstDatos.Tables("Urdimbre").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO TRAMA
            For Each dtrDatos As DataRow In _dstSchema.Tables(2).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Trama").NewRow
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencia")
                dtrNuevo("var_CodigoHilo") = dtrDatos("var_CodigoHilo")
                dtrNuevo("var_DescripcionHilo") = dtrDatos("var_DescripHilo")
                dtrNuevo("num_AnchoCrudo") = dtrDatos("int_AnchoCrudo")
                dtrNuevo("int_NumeroHilos") = dtrDatos("int_NumeroHilos")
                dtrNuevo("num_TituloReal") = dtrDatos("num_TituloReal")
                dtrNuevo("num_GramosMetro") = dtrDatos("num_GramosMetro")
                dstDatos.Tables("Trama").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO ENGOMADO
            For Each dtrDatos As DataRow In _dstSchema.Tables(3).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Engomado").NewRow
                dtrNuevo("var_CodigoEngomado") = dtrDatos("var_CodigoEngomado")
                Me._strCodigoEngomado = dtrDatos("var_CodigoEngomado")
                dtrNuevo("var_CodigoTipo") = dtrDatos("chr_Tipo")
                dtrNuevo("num_Estiraje") = dtrDatos("num_Estiraje")
                dtrNuevo("num_Velocidad") = dtrDatos("num_Velocidad")
                dstDatos.Tables("Engomado").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO FORMULACION
            For Each dtrDatos As DataRow In _dstSchema.Tables(4).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Formulacion").NewRow
                dtrNuevo("var_CodigoEngomado") = dtrDatos("var_CodigoEngomado")
                dtrNuevo("var_CodigoFase") = dtrDatos("var_CodigoFase")
                dtrNuevo("var_NombreFase") = dtrDatos("var_NombreFase")
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("num_Dosificacion") = dtrDatos("num_Dosificacion")
                dtrNuevo("num_Pickup") = dtrDatos("num_Pickup")
                dtrNuevo("var_NombreIQ1") = dtrDatos("var_NombreIQ1")
                dtrNuevo("var_NombreIQ2") = dtrDatos("var_NombreIQ2")
                dtrNuevo("var_CodigoIQ1") = dtrDatos("var_CodigoIQ1")
                dtrNuevo("var_CodigoIQ2") = dtrDatos("var_CodigoIQ2")
                dtrNuevo("num_DosisIQ1") = dtrDatos("num_DosisIQ1")
                dtrNuevo("num_DosisIQ2") = dtrDatos("num_DosisIQ2")
                dtrNuevo("var_UnidadIQ1") = dtrDatos("var_UnidadIQ1")
                dtrNuevo("var_UnidadIQ2") = dtrDatos("var_UnidadIQ2")
                dstDatos.Tables("Formulacion").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO IQ RECETA
            For Each dtrDatos As DataRow In _dstSchema.Tables(5).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("Receta").NewRow
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoInsumo") = dtrDatos("var_CodigoInsumo")
                dtrNuevo("var_DescripcionInsumo") = dtrDatos("var_DescripcionInsumo")
                dtrNuevo("num_Concentracion") = dtrDatos("num_Concentracion")
                dtrNuevo("num_PorcentajeGramosLitro") = dtrDatos("num_PorcentajeGramosLitro")
                dstDatos.Tables("Receta").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO RUTA TINTORERIA
            For Each dtrDatos As DataRow In _dstSchema.Tables(6).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("RutaTintoreria").NewRow
                dtrNuevo("var_CodigoArticulo") = dtrDatos("var_CodigoArticuloLargo")
                _strCodigoArticuloLargo = dtrDatos("var_CodigoArticuloLargo")
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencial")
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoMaquina") = dtrDatos("var_CodigoMaquina")
                dtrNuevo("var_CodigoOperacion") = dtrDatos("var_CodigoOperacion")
                dtrNuevo("var_CodigoEtapa") = dtrDatos("var_CodigoEtapa")
                dtrNuevo("num_VelocidadMaquina") = dtrDatos("num_Velocidad")
                dtrNuevo("int_Pases") = dtrDatos("int_Pases")
                dtrNuevo("var_DescripcionEtapa") = dtrDatos("var_DescripEtapa")
                dtrNuevo("var_DescripcionOperacion") = dtrDatos("var_DescripOperacion")
                dtrNuevo("var_DescripcionMaquina") = dtrDatos("var_DescripMaquina")
                dstDatos.Tables("RutaTintoreria").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO IQ RECETA TINTORERIA
            For Each dtrDatos As DataRow In _dstSchema.Tables(7).Rows
                Dim dtrNuevo As DataRow = dstDatos.Tables("RecetaInsumoTinto").NewRow
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoInsumo") = dtrDatos("var_CodigoInsumo")
                dtrNuevo("var_DescripcionInsumo") = dtrDatos("var_DescripcionInsumo")
                dtrNuevo("num_Concentracion") = dtrDatos("num_Concentracion")
                dstDatos.Tables("RecetaInsumoTinto").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'CARGANDO PICKUP TINTORERIA
            For Each dtrDatos As DataRow In _dstSchema.Tables(8).Select("", "int_Secuencia")
                Dim dtrNuevo As DataRow = dstDatos.Tables("PickupTintoreria").NewRow
                dtrNuevo("int_Secuencia") = dtrDatos("int_Secuencia")
                dtrNuevo("var_CodigoReceta") = dtrDatos("var_CodigoReceta")
                dtrNuevo("var_CodigoMaquina") = dtrDatos("var_CodigoMaquina")
                dtrNuevo("var_CodigoOperacion") = dtrDatos("var_CodigoOperacion")
                dtrNuevo("var_DescripcionOperacion") = dtrDatos("var_DescripcionOperacion")
                dtrNuevo("num_Pickup") = dtrDatos("num_Pickup")
                dtrNuevo("num_Peso") = dtrDatos("num_Peso")

                dstDatos.Tables("PickupTintoreria").LoadDataRow(dtrNuevo.ItemArray, True)
            Next

            'Sacando los datos de estampado
            _dblCostoEstampado = _dstSchema.Tables(9).Rows(0).Item(0)

            'Sacando datos de la requisicion
            If IsDBNull(_dstSchema.Tables(9).Rows(0)(1)) = False Then
                gstrNroRequision = _dstSchema.Tables(9).Rows(0)(1)
                gstrNroDetalle = _dstSchema.Tables(9).Rows(0)(2)
            Else
                gstrNroRequision = ""
                gstrNroDetalle = ""
            End If

            dstDatos = CargarInsumosQuimicos(dstDatos, "Produccion")
            dstDatos = CargarCostoHilo(dstDatos, "Produccion")

            dstDatos = CargarCostoUrdido(dstDatos)
            dstDatos = CargarCostoEngomado_V2(dstDatos)

            ' Modificado: Costo Promedio Telar
            ' Alexander Torres Cardenas
            ' Mayo 2013

            dstDatos = CargarCostoTelar(dstDatos)
            dstDatos = CargarCostoTinto(dstDatos)
            dstDatos = CargarCostoRevFin(dstDatos)

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function VerificaPreliminar(ByVal dstDatos As DataSet) As DataTable
        Try
            Dim dtbRetorno As New DataTable, dtrItem As DataRow
            dtbRetorno.Columns.Add("var_Modulo", GetType(String))
            dtbRetorno.Columns.Add("var_Descripcion", GetType(String))

            'CARGANDO ARTICULO
            For Each dtrDatos As DataRow In dstDatos.Tables("Articulo").Rows
                If dtrDatos("var_CodigoUrdimbre") = "" Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Codigo de Urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_EncogimientoUrdimbre") Is Nothing OrElse IsDBNull(dtrDatos("num_EncogimientoUrdimbre")) = True OrElse CType(dtrDatos("num_EncogimientoUrdimbre"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Encogimiento de Urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_EncogimientoTrama") Is Nothing OrElse IsDBNull(dtrDatos("num_EncogimientoTrama")) = True OrElse CType(dtrDatos("num_EncogimientoTrama"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Encogimiento de Trama" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("int_AnchoCrudo") Is Nothing OrElse IsDBNull(dtrDatos("int_AnchoCrudo")) = True OrElse CType(dtrDatos("int_AnchoCrudo"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Ancho Crudo" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_EficienciaTeorica") Is Nothing OrElse IsDBNull(dtrDatos("num_EficienciaTeorica")) = True OrElse CType(dtrDatos("num_EficienciaTeorica"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Eficiencia Teorica" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_EficienciaReal") Is Nothing OrElse IsDBNull(dtrDatos("num_EficienciaReal")) = True OrElse CType(dtrDatos("num_EficienciaReal"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Eficiencia Real" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_MermaTejeduria") Is Nothing OrElse IsDBNull(dtrDatos("num_MermaTejeduria")) = True OrElse CType(dtrDatos("num_MermaTejeduria"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Merma de Tejeduria" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_VariacionDimensional") Is Nothing OrElse CType(dtrDatos("num_VariacionDimensional"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Variacion Dimensional" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_VelocidadTeorica") Is Nothing OrElse IsDBNull(dtrDatos("num_VelocidadTeorica")) = True OrElse CType(dtrDatos("num_VelocidadTeorica"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Velocidad Teorica" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_GastoOperativo") Is Nothing OrElse IsDBNull(dtrDatos("num_GastoOperativo")) = True OrElse CType(dtrDatos("num_GastoOperativo"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta % Gastos Operativos" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_PorcentajeCompensacion") Is Nothing OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse CType(dtrDatos("num_PorcentajeCompensacion"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Porcentaje Compensacion por Calidad" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_TipoCambio") Is Nothing OrElse IsDBNull(dtrDatos("num_TipoCambio")) = True OrElse CType(dtrDatos("num_TipoCambio"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Tipo de Cambio" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("int_NumeroTelas") Is Nothing OrElse IsDBNull(dtrDatos("int_NumeroTelas")) = True OrElse CType(dtrDatos("int_NumeroTelas"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Numero de Telas" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_AnchoPeine") Is Nothing OrElse IsDBNull(dtrDatos("num_AnchoPeine")) = True OrElse CType(dtrDatos("num_AnchoPeine"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Ancho del peine" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                dtbRetorno.AcceptChanges()

            Next

            If dstDatos.Tables("Urdimbre").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "No existen hilos en urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            'CARGANDO URDIMBRE
            For Each dtrDatos As DataRow In dstDatos.Tables("Urdimbre").Rows
                If dtrDatos("num_HilosPulgada") Is Nothing OrElse IsDBNull(dtrDatos("num_HilosPulgada")) = True OrElse CType(dtrDatos("num_HilosPulgada"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Hilos/Pulgada en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("int_NumeroHilos") Is Nothing OrElse IsDBNull(dtrDatos("int_NumeroHilos")) = True OrElse CType(dtrDatos("int_NumeroHilos"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Numero de Hilos en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_TituloReal") Is Nothing OrElse IsDBNull(dtrDatos("num_TituloReal")) = True OrElse CType(dtrDatos("num_TituloReal"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Titulo real en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_GramosMetro") Is Nothing OrElse IsDBNull(dtrDatos("num_GramosMetro")) = True OrElse CType(dtrDatos("num_GramosMetro"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Gramos/Metro en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_Velocidad") Is Nothing OrElse IsDBNull(dtrDatos("num_Velocidad")) = True OrElse CType(dtrDatos("num_Velocidad"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Velocidad en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

            Next
            dtbRetorno.AcceptChanges()

            If dstDatos.Tables("Trama").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "No existen hilos en trama" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If
            'CARGANDO TRAMA
            For Each dtrDatos As DataRow In dstDatos.Tables("Trama").Rows

                If dtrDatos("num_AnchoCrudo") Is Nothing OrElse IsDBNull(dtrDatos("num_AnchoCrudo")) = True OrElse CType(dtrDatos("num_AnchoCrudo"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Ancho Crudo en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("int_NumeroHilos") Is Nothing OrElse IsDBNull(dtrDatos("int_NumeroHilos")) = True OrElse CType(dtrDatos("int_NumeroHilos"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Numero de Hilos en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_TituloReal") Is Nothing OrElse IsDBNull(dtrDatos("num_TituloReal")) = True OrElse CType(dtrDatos("num_TituloReal"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Titulo real en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_GramosMetro") Is Nothing OrElse IsDBNull(dtrDatos("num_GramosMetro")) = True OrElse CType(dtrDatos("num_GramosMetro"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Gramos/Metro en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            'CARGANDO ENGOMADO
            For Each dtrDatos As DataRow In dstDatos.Tables("Engomado").Rows
                If dtrDatos("var_CodigoEngomado") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoEngomado")) = True OrElse dtrDatos("var_CodigoEngomado") = "" Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Codigo de Engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("var_CodigoTipo") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoTipo")) = True OrElse dtrDatos("var_CodigoTipo") = "" Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Tipo de Engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_Estiraje") Is Nothing OrElse IsDBNull(dtrDatos("num_Estiraje")) = True OrElse CType(dtrDatos("num_Estiraje"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Estiraje " : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_Velocidad") Is Nothing OrElse IsDBNull(dtrDatos("num_Velocidad")) = True OrElse CType(dtrDatos("num_Velocidad"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Velocidad " : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            'CARGANDO FORMULACION
            If dstDatos.Tables("Formulacion").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "No existen fases de engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            For Each dtrDatos As DataRow In dstDatos.Tables("Formulacion").Rows
                If dtrDatos("var_CodigoReceta") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoReceta")) = True OrElse dtrDatos("var_CodigoReceta") = "" Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Receta en  " & dtrDatos("var_NombreFase") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_Dosificacion") Is Nothing OrElse IsDBNull(dtrDatos("num_Dosificacion")) = True OrElse CType(dtrDatos("num_Dosificacion"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Dosificacion en  " & dtrDatos("var_NombreFase") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_Pickup") Is Nothing OrElse IsDBNull(dtrDatos("num_Pickup")) = True OrElse CType(dtrDatos("num_Pickup"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Pickup en  " & dtrDatos("var_NombreFase") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            'CARGANDO IQ RECETA
            For Each dtrDatos As DataRow In dstDatos.Tables("Receta").Rows
                If dtrDatos("num_Concentracion") Is Nothing OrElse IsDBNull(dtrDatos("num_Concentracion")) = True OrElse CType(dtrDatos("num_Concentracion"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_PTJ" : dtrItem("var_Descripcion") = "Falta Concentracion en  " & dtrDatos("var_CodigoReceta") & "/" & dtrDatos("var_CodigoInsumo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            'CARGANDO RUTA TINTORERIA
            For Each dtrDatos As DataRow In dstDatos.Tables("RutaTintoreria").Rows
                If dtrDatos("var_CodigoMaquina") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoMaquina")) = True OrElse dtrDatos("var_CodigoMaquina") = "" Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RUTA_TINTO" : dtrItem("var_Descripcion") = "Falta Codigo de Maquina en #Sec " & dtrDatos("int_Secuencia") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("var_CodigoOperacion") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoOperacion")) = True OrElse dtrDatos("var_CodigoOperacion") = "" Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RUTA_TINTO" : dtrItem("var_Descripcion") = "Falta Codigo de Operacion en #Sec " & dtrDatos("int_Secuencia") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("int_Pases") Is Nothing OrElse IsDBNull(dtrDatos("int_Pases")) = True OrElse CType(dtrDatos("int_Pases"), Double) <= 0.0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RUTA_TINTO" : dtrItem("var_Descripcion") = "Falta Pases en #Sec " & dtrDatos("int_Secuencia") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            'CARGANDO IQ RECETA TINTORERIA
            For Each dtrDatos As DataRow In dstDatos.Tables("RecetaInsumoTinto").Rows
                If dtrDatos("num_Concentracion") Is Nothing OrElse IsDBNull(dtrDatos("num_Concentracion")) = True OrElse CType(dtrDatos("num_Concentracion"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_PTJ" : dtrItem("var_Descripcion") = "Falta Concentracion en  " & dtrDatos("var_CodigoReceta") & "/" & dtrDatos("var_CodigoInsumo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            'CARGANDO PICKUP TINTORERIA
            For Each dtrDatos As DataRow In dstDatos.Tables("PickupTintoreria").Select("var_CodigoReceta<>''")
                If dtrDatos("num_Pickup") Is Nothing OrElse IsDBNull(dtrDatos("num_Pickup")) = True OrElse CType(dtrDatos("num_Pickup"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_TIN" : dtrItem("var_Descripcion") = "Falta Pickup en  " & dtrDatos("var_CodigoOperacion") & "/" & dtrDatos("var_CodigoReceta") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                If dtrDatos("num_Peso") Is Nothing OrElse IsDBNull(dtrDatos("num_Peso")) = True OrElse CType(dtrDatos("num_Peso"), Double) <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_TIN" : dtrItem("var_Descripcion") = "Falta Peso en  " & dtrDatos("var_CodigoOperacion") & "/" & dtrDatos("var_CodigoReceta") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
            Next
            dtbRetorno.AcceptChanges()

            If dstDatos.Tables("CostoHilo").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_HILO" : dtrItem("var_Descripcion") = "No existen costos de hilo" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            If dstDatos.Tables("CostoUrdido").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_URDIDO" : dtrItem("var_Descripcion") = "No existen costos en urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            If dstDatos.Tables("CostoEngomado").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_ENGOMADO" : dtrItem("var_Descripcion") = "No existen costos en engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            If dstDatos.Tables("CostoTelar").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_TELAR" : dtrItem("var_Descripcion") = "No existen costos en Telares" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            If dstDatos.Tables("CostoInsumo").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_INSUMO" : dtrItem("var_Descripcion") = "No existen costos en Insumos Quimicos" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            If dstDatos.Tables("CostoTinto").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_TINTORERIA" : dtrItem("var_Descripcion") = "No existe Costo Hora/Maquina en Tintoreria" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            If dstDatos.Tables("CostoRevFin").Rows.Count <= 0 Then
                dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_REVISION" : dtrItem("var_Descripcion") = "No existe Costo de Revision Final" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If

            Return dtbRetorno
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CargarCostos(ByVal dstDatos As DataSet, ByVal strTipo As String, Optional ByVal strArea As String = "Gerencial") As DataSet
        If strTipo = "INSUMOS" Then
            Return CargarInsumosQuimicos(dstDatos, strArea)
        ElseIf strTipo = "HILOS" Then
            Return CargarCostoHilo(dstDatos, strArea)
        ElseIf strTipo = "URDIDO" Then
            Return CargarCostoUrdido(dstDatos)
        ElseIf strTipo = "ENGOMADO" Then
            Return CargarCostoEngomado(dstDatos)
        ElseIf strTipo = "TELAR" Then
            Return CargarCostoTelar(dstDatos)
        ElseIf strTipo = "TINTO" Then
            Return CargarCostoTinto(dstDatos)
        ElseIf strTipo = "REVFIN" Then
            Return CargarCostoRevFin(dstDatos)
        End If
    End Function

    Private Function CargarInsumosQuimicos(ByVal dstDatos As DataSet, Optional ByVal strArea As String = "Gerencial") As DataSet
        Try
            Dim strDatosXML As String = ""
            strDatosXML = "<root>"
            For Each dtrDatos As DataRow In dstDatos.Tables("Receta").Rows
                strDatosXML = strDatosXML & "<INSUMOS>"
                strDatosXML = strDatosXML & "<var_CodigoReceta>" & dtrDatos("var_CodigoReceta") & "</var_CodigoReceta>"
                strDatosXML = strDatosXML & "<var_CodigoInsumo>" & dtrDatos("var_CodigoInsumo") & "</var_CodigoInsumo>"
                strDatosXML = strDatosXML & "<var_DescripcionInsumo>" & dtrDatos("var_DescripcionInsumo") & "</var_DescripcionInsumo>"
                strDatosXML = strDatosXML & "</INSUMOS>"
            Next

            For Each dtrDatos As DataRow In dstDatos.Tables("RecetaInsumoTinto").Rows
                strDatosXML = strDatosXML & "<INSUMOS>"
                strDatosXML = strDatosXML & "<var_CodigoReceta>" & dtrDatos("var_CodigoReceta") & "</var_CodigoReceta>"
                strDatosXML = strDatosXML & "<var_CodigoInsumo>" & dtrDatos("var_CodigoInsumo") & "</var_CodigoInsumo>"
                strDatosXML = strDatosXML & "<var_DescripcionInsumo>" & dtrDatos("var_DescripcionInsumo") & "</var_DescripcionInsumo>"
                strDatosXML = strDatosXML & "</INSUMOS>"
            Next

            For Each dtrDatos As DataRow In dstDatos.Tables("Formulacion").Rows
                If IsDBNull(dtrDatos("var_CodigoIQ1")) = False AndAlso dtrDatos("var_CodigoIQ1") <> "" Then
                    strDatosXML = strDatosXML & "<INSUMOS>"
                    strDatosXML = strDatosXML & "<var_CodigoReceta>SR</var_CodigoReceta>"
                    strDatosXML = strDatosXML & "<var_CodigoInsumo>" & dtrDatos("var_CodigoIQ1") & "</var_CodigoInsumo>"
                    strDatosXML = strDatosXML & "<var_DescripcionInsumo>" & dtrDatos("var_NombreIQ1") & "</var_DescripcionInsumo>"
                    strDatosXML = strDatosXML & "</INSUMOS>"
                End If
                If IsDBNull(dtrDatos("var_CodigoIQ2")) = False AndAlso dtrDatos("var_CodigoIQ2") <> "" Then
                    strDatosXML = strDatosXML & "<INSUMOS>"
                    strDatosXML = strDatosXML & "<var_CodigoReceta>SR</var_CodigoReceta>"
                    strDatosXML = strDatosXML & "<var_CodigoInsumo>" & dtrDatos("var_CodigoIQ2") & "</var_CodigoInsumo>"
                    strDatosXML = strDatosXML & "<var_DescripcionInsumo>" & dtrDatos("var_NombreIQ2") & "</var_DescripcionInsumo>"
                    strDatosXML = strDatosXML & "</INSUMOS>"
                End If
            Next

            strDatosXML = strDatosXML & "</root>"
            Dim objUtil As New NM_General.Util
            strDatosXML = objUtil.EncodeXML(strDatosXML)

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_DatosXML", strDatosXML, "var_Area", strArea}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoInsumoQuimico_Obtener", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoInsumo")
            dtbDatos.PrimaryKey = dtcIndice

            dstDatos.Tables("CostoInsumo").Rows.Clear()
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoInsumo").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            'AGREGANDO LOS COSTOS DE IQ
            'For Each dtrDatos As DataRow In dtbDatos.Rows
            '    If dstDatos.Tables("CostoInsumo").Rows.Find(dtrDatos("var_CodigoInsumo")) Is Nothing Then
            '        dstDatos.Tables("CostoInsumo").LoadDataRow(dtrDatos.ItemArray, True)
            '    End If
            'Next

            'RETIRANDO IQ NO USADOS
            'For Each dtrDatos As DataRow In dstDatos.Tables("CostoInsumo").Rows
            '    If dtbDatos.Rows.Find(dtrDatos("var_CodigoInsumo")) Is Nothing Then
            '        dstDatos.Tables("CostoInsumo").Rows.Find(dtrDatos("var_CodigoInsumo")).Delete()
            '    End If
            'Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CargarCostoHilo(ByVal dstDatos As DataSet, Optional ByVal strArea As String = "Gerencial") As DataSet
        Try
            Dim strDatosXML As String = ""
            strDatosXML = "<root>"
            For Each dtrDatos As DataRow In dstDatos.Tables("Urdimbre").Rows
                strDatosXML = strDatosXML & "<HILOS>"
                strDatosXML = strDatosXML & "<var_CodigoHilo>" & dtrDatos("var_CodigoHilo") & "</var_CodigoHilo>"
                strDatosXML = strDatosXML & "<var_DescripcionHilo>" & dtrDatos("var_DescripcionHilo") & "</var_DescripcionHilo>"
                strDatosXML = strDatosXML & "</HILOS>"
            Next

            For Each dtrDatos As DataRow In dstDatos.Tables("Trama").Rows
                strDatosXML = strDatosXML & "<HILOS>"
                strDatosXML = strDatosXML & "<var_CodigoHilo>" & dtrDatos("var_CodigoHilo") & "</var_CodigoHilo>"
                strDatosXML = strDatosXML & "<var_DescripcionHilo>" & dtrDatos("var_DescripcionHilo") & "</var_DescripcionHilo>"
                strDatosXML = strDatosXML & "</HILOS>"
            Next
            strDatosXML = strDatosXML & "</root>"

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objUtil As New NM_General.Util
            Dim objParametros() As Object = {"var_DatosXML", objUtil.EncodeXML(strDatosXML), "var_Area", strArea}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoHilo_Obtener", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoHilo")
            dtbDatos.PrimaryKey = dtcIndice

            'AGREGANDO LOS COSTOS DE HILO
            dstDatos.Tables("CostoHilo").Rows.Clear()
            For Each dtrDatos As DataRow In dtbDatos.Rows
                'If dstDatos.Tables("CostoHilo").Rows.Find(dtrDatos("var_CodigoHilo")) Is Nothing Then
                dstDatos.Tables("CostoHilo").LoadDataRow(dtrDatos.ItemArray, True)
                'End If
            Next

            'RETIRANDO HILOS NO USADOS
            'For Each dtrDatos As DataRow In dstDatos.Tables("CostoHilo").Rows
            '    If dtbDatos.Rows.Find(dtrDatos("var_CodigoHilo")) Is Nothing Then
            '        dstDatos.Tables("CostoHilo").Rows.Find(dtrDatos("var_CodigoHilo")).Delete()
            '    End If
            'Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CargarCostoUrdido(ByVal dstDatos As DataSet) As DataSet
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_CodigoUrdimbre", _strCodigoArticuloCorto}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoUrdido_Obtener_2", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcIndice
            dstDatos.Tables("CostoUrdido").Rows.Clear()

            'AGREGANDO LOS COSTOS DE URDIDO
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoUrdido").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CargarCostoEngomado(ByVal dstDatos As DataSet) As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_CodigoEstudio", mstrEquivalencia}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoEngomadoCompleto_Obtener_2", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcIndice
            dstDatos.Tables("CostoEngomado").Rows.Clear()

            'AGREGANDO LOS COSTOS DE ENGOMADO
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoEngomado").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function CargarCostoEngomado_V2(ByVal dstDatos As DataSet) As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_CodigoEstudio", mstrEquivalencia}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoEngomadoCompleto_Obtener_2_V2", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcIndice
            dstDatos.Tables("CostoEngomado").Rows.Clear()

            'AGREGANDO LOS COSTOS DE ENGOMADO
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoEngomado").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CargarCostoTelar(ByVal dstDatos As DataSet) As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_CodigoArticulo", Left(_strCodigoArticuloLargo, 7)}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoTelar_Obtener_2", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcIndice
            dstDatos.Tables("CostoTelar").Rows.Clear()

            'AGREGANDO LOS COSTOS DE TELAR
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoTelar").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' Modificado: Obtenemos costo promedio de los telares
    ' Mayo 2013
    ' Alexandder Torres Cardenas
    ' 1
    Private Function CargarCostoPromedioTelar(ByVal dstDatos As DataSet) As DataSet
        Dim dtbDatos As New DataTable
        dtbDatos = Nothing

        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)

            Dim objParametros() As Object = {"var_CodigoEquivalencia", Me._strCodigoArticuloLargo}
            Dim dtcIndice(0) As DataColumn

            dtbDatos = _objConexion.ObtenerDataTable("usp_costos_CostoPromedioTelar_Listar_2", objParametros)
            dtcIndice(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcIndice
            dstDatos.Tables("CostoTelar").Rows.Clear()
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoTelar").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CargarCostoTinto(ByVal dstDatos As DataSet) As DataSet
        Try
            Dim strDatosXML As String = ""
            strDatosXML = "<root>"
            For Each dtrDatos As DataRow In dstDatos.Tables("RutaTintoreria").Rows
                strDatosXML = strDatosXML & "<MAQUINATINTO>"
                strDatosXML = strDatosXML & "<var_CodigoMaquina>" & dtrDatos("var_CodigoMaquina") & "</var_CodigoMaquina>"
                strDatosXML = strDatosXML & "<var_DescripcionMaquina>" & dtrDatos("var_DescripcionMaquina") & "</var_DescripcionMaquina>"
                strDatosXML = strDatosXML & "</MAQUINATINTO>"
            Next

            strDatosXML = strDatosXML & "</root>"

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_DatosXML", strDatosXML}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoHoraMaquinaTinto_Obtener", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoMaquina")
            dtbDatos.PrimaryKey = dtcIndice

            'AGREGANDO LOS COSTOS DE IQ
            For Each dtrDatos As DataRow In dtbDatos.Rows
                If dstDatos.Tables("CostoTinto").Rows.Find(dtrDatos("var_CodigoMaquina")) Is Nothing Then
                    dstDatos.Tables("CostoTinto").LoadDataRow(dtrDatos.ItemArray, True)
                End If
            Next

            'RETIRANDO IQ NO USADOS
            For Each dtrDatos As DataRow In dstDatos.Tables("CostoTinto").Rows
                If dtbDatos.Rows.Find(dtrDatos("var_CodigoMaquina")) Is Nothing Then
                    dstDatos.Tables("CostoTinto").Rows.Find(dtrDatos("var_CodigoMaquina")).Delete()
                End If
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CargarCostoRevFin(ByVal dstDatos As DataSet) As DataSet
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Dim objParametros() As Object = {"var_CodigoArticulo", Left(_strCodigoArticuloLargo, 7)}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoRevFin_Obtener", objParametros)
            Dim dtcIndice(0) As DataColumn
            dtcIndice(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcIndice
            dstDatos.Tables("CostoRevFin").Rows.Clear()

            'AGREGANDO LOS COSTOS DE TELAR
            For Each dtrDatos As DataRow In dtbDatos.Rows
                dstDatos.Tables("CostoRevFin").LoadDataRow(dtrDatos.ItemArray, True)
            Next

            Return dstDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerTrama() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Trama_Listar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerTrama_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Trama_Listar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerUrdimbre() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Urdimbre_Listar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerUrdimbre_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Urdimbre_Listar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerArticulo() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Articulo_Listar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerArticulo_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Articulo_Listar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerEngomado() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Engomado_Listar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerEngomado_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Engomado_Listar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function

    Public Function ObtenerCalculoEngomado() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoArticulo", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_DeterminarCostoEngomado_2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerCalculoEngomado_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoArticulo", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_DeterminarCostoEngomado_2_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerCalculoTelar() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoArticulo", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_DeterminarCostoTelar_2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerCalculoTelar_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoArticulo", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_DeterminarCostoTelar_2_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerCalculoUrdido() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoArticulo", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_DeterminarCostoUrdido_2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerCalculoUrdido_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoArticulo", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_DeterminarCostoUrdido_2_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function

    ' --------------------------------------------------------------------
    ' Modificado: Mayo 2013
    ' Alexander Torres
    ' Objetivo: Calculo promedio de telares
    ' 2
    ' --------------------------------------------------------------------
    Public Function ObtenerCalculoPromedioTelar() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_costos_CostoPromedioTelar_Listar_2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerCalculoPromedioTelar_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_costos_CostoPromedioTelar_Listar_2_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function

    Public Function ObtenerFormulacion() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Formulacion_Listar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function ObtenerFormulacion_V2() As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_Equivalencia", mstrEquivalencia}

        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            mdsRes = lobjCon.ObtenerDataSet("usp_FCO_Estudio_Formulacion_Listar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function

    Public Function GrabarTrama(ByRef mdtTrama As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String

        mdtTrama.TableName = "Trama"
        lobjUtil = New NM_General.Util
        lstrXML1 = lobjUtil.GeneraXml(mdtTrama)
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLTrama", UCase(lstrXML1), _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Trama_Grabar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarTrama_V2(ByRef mdtTrama As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String

        mdtTrama.TableName = "Trama"
        lobjUtil = New NM_General.Util
        lstrXML1 = lobjUtil.GeneraXml(mdtTrama)
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLTrama", UCase(lstrXML1), _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Trama_Grabar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarUrdimbre(ByRef mdtUrdimbre As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String

        mdtUrdimbre.TableName = "Urdimbre"
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtUrdimbre))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLUrdimbre", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Urdimbre_Grabar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk

    End Function
    Public Function GrabarUrdimbre_V2(ByRef mdtUrdimbre As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String

        mdtUrdimbre.TableName = "Urdimbre"
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtUrdimbre))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLUrdimbre", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Urdimbre_Grabar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk

    End Function

    Public Function GrabarDatosUrdimbre(ByVal sCodigoEquivalencia As String, ByVal sCodigoUrdimbre As String, ByVal sUsuario As String) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer


        Dim lstrParametros() As String = {"var_CodigoEquivalencia", sCodigoEquivalencia, _
                                            "var_CodigoUrdimbre", sCodigoUrdimbre, _
                                            "var_Usuario", sUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("USP_PreCos_ActUrdimbre", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk

    End Function
    Public Function GrabarDatosUrdimbre_V2(ByVal sCodigoEquivalencia As String, ByVal sCodigoUrdimbre As String, ByVal sUsuario As String) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer


        Dim lstrParametros() As String = {"var_CodigoEquivalencia", sCodigoEquivalencia, _
                                            "var_CodigoUrdimbre", sCodigoUrdimbre, _
                                            "var_Usuario", sUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("USP_PreCos_ActUrdimbre_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk

    End Function

    Public Function GrabarArticulo(ByRef mdtArticulo As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String

        mdtArticulo.TableName = "Articulo"
        lobjUtil = New NM_General.Util
        lstrXML1 = lobjUtil.GeneraXml(mdtArticulo)
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLArticulo", UCase(lstrXML1), _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Articulo_Grabar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk

    End Function
    Public Function GrabarArticulo_V2(ByRef mdtArticulo As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String

        mdtArticulo.TableName = "Articulo"
        lobjUtil = New NM_General.Util
        lstrXML1 = lobjUtil.GeneraXml(mdtArticulo)
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLArticulo", UCase(lstrXML1), _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Articulo_Grabar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk

    End Function
    Public Function GrabarEngomado(ByRef mdtEngomado As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtEngomado))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLEngomado", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Engomado_Grabar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarEngomado_V2(ByRef mdtEngomado As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtEngomado))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLEngomado", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Engomado_Grabar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarFormulacion(ByRef mdtFormulacion As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtFormulacion))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLFormulacion", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Formulacion_Grabar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarFormulacion_V2(ByRef mdtFormulacion As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtFormulacion))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLFormulacion", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_Formulacion_Grabar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarReceta(ByRef mdtRecetasPretejido As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtRecetasPretejido))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLRecetasPretejido", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_RecetaPretejido_Grabar", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function
    Public Function GrabarReceta_V2(ByRef mdtRecetasPretejido As DataTable) As Boolean
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lobjUtil As NM_General.Util
        Dim lstrXML1 As String
        lobjUtil = New NM_General.Util
        lstrXML1 = UCase(lobjUtil.GeneraXml(mdtRecetasPretejido))
        lobjUtil = Nothing
        Dim lstrParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, _
                                            "ntx_XMLRecetasPretejido", lstrXML1, _
                                            "var_Usuario", mstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            lobjCon.EjecutarComando("usp_FCO_Estudio_RecetaPretejido_Grabar_V2", lstrParametros)
            mbooOk = True
        Catch ex As Exception
            mbooOk = False
        Finally
            lobjCon = Nothing
        End Try
        Return mbooOk
    End Function

    Public Function RutaTintoreriaObtener() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_RutaProduccion_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RutaTintoreriaObtener_V2() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_RutaProduccion_Obtener_V2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RecetaTintoreriaObtener() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_RecetaTintoreria_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RecetaTintoreriaObtener_V2() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_RecetaTintoreria_Obtener_V2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PickupTintoreriaObtener() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_PickupTintoreria_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PickupTintoreriaObtener_V2() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_PickupTintoreria_Obtener_V2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DatosGeneralesObtener() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_DatosGenerales_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DatosGeneralesObtener_V2() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_DatosGenerales_Obtener_V2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DatosSofisticacion(ByVal strEquivalencia As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoEquivalencia", strEquivalencia}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_Sofisticacion_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GrabarDatosSofisticacion(ByVal strEquivalencia As String, ByVal numcompeti As String, ByVal nummoda As String, ByVal numtiempo As String, ByVal numsofi As String) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", strEquivalencia, "num_moda", nummoda, "num_competi", numcompeti, "num_tiempo", numtiempo, "num_sofi", numsofi}
        Try
            _objConexion.EjecutarComando("usp_FCO_Sofisticacion_Procesar", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function PickupTintoreriaProcesar(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_PickupTintoreria_Procesar", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function PickupTintoreriaProcesar_V2(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_PickupTintoreria_Procesar_V2", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function DatosGeneralesProcesar(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_DatosGenerales_Procesar", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function DatosGeneralesProcesar_V2(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_DatosGenerales_Procesar_V2", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function RutaTintoreriaProcesar(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_RutaTintoreria_Procesar", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function RutaTintoreriaProcesar_V2(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_RutaTintoreria_Procesar_V2", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function RecetaTintoreriaProcesar(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_RecetaTintoreria_Procesar", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function RecetaTintoreriaProcesar_V2(ByVal dtbDatos As DataTable) As Boolean
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim _objUtil As New NM_General.Util
        Dim objParametros() As String = {"var_CodigoEquivalencia", mstrEquivalencia, "var_Usuario", Me.mstrUsuario, "var_XMLData", _objUtil.GeneraXml(dtbDatos)}
        Try
            _objConexion.EjecutarComando("usp_FCO_RecetaTintoreria_Procesar_V2", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
            _objUtil = Nothing
        End Try
    End Function
    Public Function PrefijoListar() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_PrefijoFicha_Listar")
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function

    Public Function EstudioObtener(ByVal strCodigo As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_CodigoEquivalencia", strCodigo}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_EquivalenciaEstudio_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    Public Function EstudioObtener_V2(ByVal strCodigo As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_CodigoEquivalencia", strCodigo}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_EquivalenciaEstudio_Obtener_V2", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function

    Public Function EstudioObtener(ByVal strArticuloLargo As String, ByVal strArticuloCorto As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_ArticuloLargo", strArticuloLargo, "var_ArticuloCorto", strArticuloCorto}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_EquivalenciaEstudioxArticulo_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    'CAMBIO DG - COSTOS - INI
    Public Function EstudioObtener_V2(ByVal strArticuloLargo As String, ByVal strArticuloCorto As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_ArticuloLargo", strArticuloLargo, "var_ArticuloCorto", strArticuloCorto}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_EquivalenciaEstudioxArticulo_Obtener_V2", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    'CAMBIO DG - COSTOS - FIN
    Public Function EstudioProcesar() As String
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoArticuloLargo", Me._strCodigoArticuloLargo, _
        "var_CodigoArticuloCorto", Me._strCodigoArticuloCorto, _
        "var_Glosa", Me._strGlosa, "var_Usuario", Me.mstrUsuario, "var_NumeroRequisicion", mstrNumeroRequisicion, "var_DetalleRequisicion", mstrDetalleRequisicion}
        Try
            Return CType(_objConexion.ObtenerValor("usp_FCO_Estudio_Insertar", objParametros), String)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    'CAMBIO DG - COSTOS - INI
    Public Function EstudioProcesar_V2() As String
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As String = {"var_CodigoArticuloLargo", Me._strCodigoArticuloLargo, _
        "var_CodigoArticuloCorto", Me._strCodigoArticuloCorto, _
        "var_Glosa", Me._strGlosa, "var_Usuario", Me.mstrUsuario, "var_NumeroRequisicion", mstrNumeroRequisicion, "var_DetalleRequisicion", mstrDetalleRequisicion}
        Try
            Return CType(_objConexion.ObtenerValor("usp_FCO_Estudio_Insertar_V2", objParametros), String)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    Public Function ListarLinea() As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim dt As DataTable
        Try
            dt = _objConexion.ObtenerDataTable("USP_LISTAR_LINEA")
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    Public Function ListarFamiliaCalidad(ByVal intLinea As Integer) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim objParametros() As String = {"INT_LINEA", intLinea}
        Try
            Return _objConexion.ObtenerDataTable("USP_FAMILIA_CALIDAD_ARTICULO", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    Public Function ListarFamiliaAcabado(ByVal intLinea As Integer) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim objParametros() As String = {"INT_LINEA", intLinea}
        Try
            Return _objConexion.ObtenerDataTable("USP_FAMILIA_ACABADO_ARTICULO", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try
    End Function
    'CAMBIO DG - COSTOS - FIN
    Public Function GrupoTipoObtener(ByVal strArticuloLargo As String, ByVal strArticuloCorto As String) As String

        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)

        Dim objParametros() = {"var_ArticuloLargo", strArticuloLargo, "var_ArticuloCorto", strArticuloCorto}
        Dim objDT As New DataTable
        Dim strGrupo As String

        Try

            strGrupo = ""
            objDT = _objConexion.ObtenerDataTable("usp_ObtenerGrupoPorTipoArticulo", objParametros)

            If objDT.Rows.Count > 0 Then
                strGrupo = CType(objDT.Rows(0)(0), String)
            End If

            Return strGrupo

        Catch ex As Exception
            Throw ex
        Finally
            _objConexion = Nothing
        End Try

    End Function

#End Region

End Class
