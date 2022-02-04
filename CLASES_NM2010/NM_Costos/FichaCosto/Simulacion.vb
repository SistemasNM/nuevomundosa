Imports NM.AccesoDatos

Namespace FichaCosto
    Public Class Simulacion
#Region "VARIABLES"

    Private _objConexion As AccesoDatosSQLServer
    Private _strCodigoArticuloLargo As String
    Private _strCodigoArticuloCorto As String
    Private _dblTipoCambio As Double
    Private _strCodigoUrdimbre As String
    Private _strCodigoEngomado As String
    Private _strCodigoTelar As String
    Private _strUsuario As String
    Private _dblVariacionDimensional As Double
    Private _dstSchema As New DataSet
    Private _dblCostoEstampado As Double
#End Region

#Region "PROPIEDADES"
    Public Property CodigoArticuloLargo() As String
      Get
        CodigoArticuloLargo = _strCodigoArticuloLargo
      End Get
      Set(ByVal Value As String)
        _strCodigoArticuloLargo = Value
      End Set
    End Property

    Public Property CodigoArticuloCorto() As String
      Get
        CodigoArticuloCorto = _strCodigoArticuloCorto
      End Get
      Set(ByVal Value As String)
        _strCodigoArticuloCorto = Value
      End Set
    End Property

    Public Property TipoCambio() As Double
      Get
        TipoCambio = _dblTipoCambio
      End Get
      Set(ByVal Value As Double)
        _dblTipoCambio = Value
      End Set
    End Property

    Public Property CodigoUrdimbre() As String
      Get
        CodigoUrdimbre = _strCodigoUrdimbre
      End Get
      Set(ByVal Value As String)
        _strCodigoUrdimbre = Value
      End Set
    End Property

    Public Property CodigoEngomado() As String
      Get
        CodigoEngomado = _strCodigoEngomado
      End Get
      Set(ByVal Value As String)
        _strCodigoEngomado = Value
      End Set
    End Property

    Public Property VariacionDimensional() As Double
      Get
        VariacionDimensional = _dblVariacionDimensional
      End Get
      Set(ByVal Value As Double)
        _dblVariacionDimensional = Value
      End Set
    End Property

    Public Property DataSchema() As DataSet
      Get
        DataSchema = _dstSchema
      End Get
      Set(ByVal Value As DataSet)
        _dstSchema = Value
      End Set
    End Property

    Public Property Usuario() As String
      Get
        Usuario = Me._strUsuario
      End Get
      Set(ByVal Value As String)
        _strUsuario = Value
      End Set
    End Property

    Public Property CostoEstampado() As Double
      Get
        CostoEstampado = Me._strUsuario
      End Get
      Set(ByVal Value As Double)
        _dblCostoEstampado = Value
      End Set
    End Property

#End Region

#Region "CONSTRUCTORES"
    Sub New()
      _strCodigoArticuloLargo = ""
      _strCodigoArticuloCorto = ""
      _dblTipoCambio = 0
      _strCodigoUrdimbre = ""
      _strCodigoEngomado = ""
      _strCodigoTelar = ""
      _strUsuario = ""
      _dblVariacionDimensional = 0
    End Sub

#End Region

#Region "PROCEDIMIENTOS Y FUNCIONES"

    Public Function ListarArticuloCorto() As DataTable
      Try
        Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_CodigoArticuloLargo", Me._strCodigoArticuloLargo}

        Return _objConexion.ObtenerDataTable("usp_FCO_ListarArticulosProduccion", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

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
      dtbDatos.Columns.Add("int_NumeroTelas", GetType(Int32))
      dtbDatos.Columns.Add("num_AnchoPeine", GetType(Double))
      dtbDatos.Columns.Add("var_Ligamento", GetType(String))
      dtbDatos.Columns.Add("var_DescArticulo1", GetType(String))
      dtbDatos.Columns.Add("var_DescArticulo2", GetType(String))
      dtbDatos.Columns.Add("var_Glosa", GetType(String))
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
      dtbDatos.Columns.Add("int_NumeroHilos", GetType(Int16))
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

      '--------------------------------------------
      ' Modificado: Costo Promedio Telar
      ' Alexander Torres Cardenas
      ' Mayo 2013
      ' COSTO DE TELAR / COSTO PROMEDIO DE TELARES
      '--------------------------------------------
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
        Public Function CreateSchema_V2() As DataSet
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
            dtbDatos.Columns.Add("int_NumeroTelas", GetType(Int32))
            dtbDatos.Columns.Add("num_AnchoPeine", GetType(Double))
            dtbDatos.Columns.Add("var_Ligamento", GetType(String))
            dtbDatos.Columns.Add("var_DescArticulo1", GetType(String))
            dtbDatos.Columns.Add("var_DescArticulo2", GetType(String))
            dtbDatos.Columns.Add("var_Glosa", GetType(String))
            dtbDatos.Columns.Add("num_AnchoEstandar", GetType(Double))
            dtbDatos.Columns.Add("num_PesoOnzas", GetType(Double))
            dtbDatos.Columns.Add("int_linea", GetType(Int16))
            dtbDatos.Columns.Add("int_fami_crudo", GetType(Int16))
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
            dtbDatos.Columns.Add("int_NumeroHilos", GetType(Int16))
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

            '--------------------------------------------
            ' Modificado: Costo Promedio Telar
            ' Alexander Torres Cardenas
            ' Mayo 2013
            ' COSTO DE TELAR / COSTO PROMEDIO DE TELARES
            '--------------------------------------------
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

            'SOFISTICACION
            dtbDatos = New DataTable
            dtbDatos.Columns.Add("var_CodigoArticulo", GetType(String))
            dtbDatos.Columns.Add("num_moda", GetType(String))
            dtbDatos.Columns.Add("num_competi", GetType(String))
            dtbDatos.Columns.Add("num_tiempo", GetType(String))
            dtbDatos.Columns.Add("num_total", GetType(String))
            ReDim dtcPrimary(0)
            dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticulo")
            dtbDatos.PrimaryKey = dtcPrimary
            dtbDatos.TableName = "Sofisticacion"
            _dstSchema.Tables.Add(dtbDatos)

            Return _dstSchema

        End Function
    Public Function InicializaDataSet(ByVal dstDatos As DataSet) As DataSet
      Try
        Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_CodigoArticuloLargo", Me._strCodigoArticuloLargo, _
        "var_CodigoArticuloCorto", Me._strCodigoArticuloCorto, "var_Usuario", Me._strUsuario, _
        "var_CodigoUrdimbre", Me._strCodigoUrdimbre, "var_CodigoEngomado", Me._strCodigoEngomado}

        _dstSchema = _objConexion.ObtenerDataSet("usp_FCO_DatosMaestros_Obtener", objParametros)

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
          dstDatos.Tables("Receta").LoadDataRow(dtrNuevo.ItemArray, True)
        Next

        'CARGANDO RUTA TINTORERIA
        For Each dtrDatos As DataRow In _dstSchema.Tables(6).Rows
          Dim dtrNuevo As DataRow = dstDatos.Tables("RutaTintoreria").NewRow
          dtrNuevo("var_CodigoArticulo") = dtrDatos("var_CodigoArticuloLargo")
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

        dstDatos = CargarInsumosQuimicos(dstDatos)
        dstDatos = CargarCostoHilo(dstDatos)
        dstDatos = CargarCostoUrdido(dstDatos)
        dstDatos = CargarCostoEngomado(dstDatos)
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
                Dim objParametros() = {"var_CodigoArticuloLargo", Me._strCodigoArticuloLargo, _
                "var_CodigoArticuloCorto", Me._strCodigoArticuloCorto, "var_Usuario", Me._strUsuario, _
                "var_CodigoUrdimbre", Me._strCodigoUrdimbre, "var_CodigoEngomado", Me._strCodigoEngomado}

                _dstSchema = _objConexion.ObtenerDataSet("usp_FCO_DatosMaestros_Obtener_V2", objParametros)

                'CARGANDO ARTICULO
                For Each dtrDatos As DataRow In _dstSchema.Tables(0).Rows
                    Dim dtrNuevo As DataRow = dstDatos.Tables("Articulo").NewRow
                    dtrNuevo("var_CodigoUrdimbre") = dtrDatos("var_CodigoUrdimbre")
                    Me._strCodigoUrdimbre = dtrDatos("var_CodigoUrdimbre")
                    dtrNuevo("int_linea") = dtrDatos("int_linea")
                    dtrNuevo("int_fami_crudo") = dtrDatos("int_fami_crudo")

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
                    dstDatos.Tables("Receta").LoadDataRow(dtrNuevo.ItemArray, True)
                Next

                'CARGANDO RUTA TINTORERIA
                For Each dtrDatos As DataRow In _dstSchema.Tables(6).Rows
                    Dim dtrNuevo As DataRow = dstDatos.Tables("RutaTintoreria").NewRow
                    dtrNuevo("var_CodigoArticulo") = dtrDatos("var_CodigoArticuloLargo")
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

                For Each dtrDatos As DataRow In _dstSchema.Tables(9).Rows
                    Dim dtrNuevo As DataRow = dstDatos.Tables("Sofisticacion").NewRow
                    dtrNuevo("var_CodigoArticulo") = dtrDatos("var_CodigoArticulo")
                    dtrNuevo("num_moda") = dtrDatos("num_moda")
                    dtrNuevo("num_competi") = dtrDatos("num_competi")
                    dtrNuevo("num_tiempo") = dtrDatos("num_tiempo")
                    dtrNuevo("num_total") = dtrDatos("num_total")
                    dstDatos.Tables("Sofisticacion").LoadDataRow(dtrNuevo.ItemArray, True)
                Next
                
                dstDatos = CargarInsumosQuimicos(dstDatos)
                dstDatos = CargarCostoHilo(dstDatos)
                dstDatos = CargarCostoUrdido(dstDatos)
                dstDatos = CargarCostoEngomado(dstDatos)
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

          If dtrDatos("num_VariacionDimensional") Is Nothing OrElse CType(dtrDatos("num_VariacionDimensional"), Double) = 0 Then
            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Variacion Dimensional" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
          End If

          If dtrDatos("num_VelocidadTeorica") Is Nothing OrElse IsDBNull(dtrDatos("num_VelocidadTeorica")) = True OrElse CType(dtrDatos("num_VelocidadTeorica"), Double) <= 0 Then
            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Velocidad Teorica" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
          End If

          If dtrDatos("num_GastoOperativo") Is Nothing OrElse IsDBNull(dtrDatos("num_GastoOperativo")) = True OrElse CType(dtrDatos("num_GastoOperativo"), Double) <= 0 Then
            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta % Gastos Operativos" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
          End If

          If dtrDatos("num_PorcentajeCompensacion") Is Nothing OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse CType(dtrDatos("num_PorcentajeCompensacion"), Double) < 0 Then
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

        'SI NO ES ARTICULO CRUDO VERIFICAR EN TINTORERIA
        If _strCodigoArticuloLargo.Substring(9, 1) <> 0 AndAlso _strCodigoArticuloLargo.Substring(7, 1) <> 5 Then
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

        End If

        If dstDatos.Tables("CostoHilo").Rows.Count <= 0 Then
          dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_HILO" : dtrItem("var_Descripcion") = "No existen costos de hilo" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
        Else
          For Each dtrDatos As DataRow In dstDatos.Tables("CostoHilo").Rows
            If dtrDatos("num_CostoProm") = 0 Then
              dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_HILO" : dtrItem("var_Descripcion") = "El costo del hilo " & dtrDatos("var_CodigoHilo") & " es incorrecto" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If
          Next
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
        If _strCodigoArticuloLargo.Substring(9, 1) <> 0 AndAlso _strCodigoArticuloLargo.Substring(7, 1) <> 5 Then
          If dstDatos.Tables("CostoTinto").Rows.Count <= 0 Then
            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_TINTORERIA" : dtrItem("var_Descripcion") = "No existe Costo Hora/Maquina en Tintoreria" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
          End If
        End If

        If dstDatos.Tables("CostoRevFin").Rows.Count <= 0 Then
          dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_REVISION" : dtrItem("var_Descripcion") = "No existe Costo de Revision Final" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
        End If


        'Validacion de costo de estampado

        If _dblCostoEstampado = -1 Then
          dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_TINTORERIA" : dtrItem("var_Descripcion") = "Ingresar el costo de estampado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
        End If

        'Validando la formulacion
        If _strCodigoArticuloLargo <> "" Then

          Dim lobjEstudio As NM_Costos.Estudio
          Dim ldtFormulacion As DataTable
          Dim dstTabDatos As DataSet

          lobjEstudio = New NM_Costos.Estudio("sa")
          lobjEstudio.Equivalencia = _strCodigoArticuloLargo
                    lobjEstudio.ObtenerFormulacion()
          ldtFormulacion = lobjEstudio.SetDatos.Tables(0)

          If ldtFormulacion.Rows.Count > 0 Then

            If IsDBNull(dstDatos.Tables("Receta").Compute("Sum(num_Concentracion)", "var_CodigoReceta='" & Trim(ldtFormulacion.Rows(0)("var_CodigoReceta").ToString) & "'")) = True Then
              dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "La receta de la formulacion a cambiado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
            End If
          Else
            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "La receta de la formulacion a cambiado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
          End If

        End If



        Return dtbRetorno
      Catch ex As Exception
        Throw ex
      End Try
    End Function
        Public Function VerificaPreliminar_V2(ByVal dstDatos As DataSet) As DataTable
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

                    If dtrDatos("num_VariacionDimensional") Is Nothing OrElse CType(dtrDatos("num_VariacionDimensional"), Double) = 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Variacion Dimensional" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_VelocidadTeorica") Is Nothing OrElse IsDBNull(dtrDatos("num_VelocidadTeorica")) = True OrElse CType(dtrDatos("num_VelocidadTeorica"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Velocidad Teorica" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_GastoOperativo") Is Nothing OrElse IsDBNull(dtrDatos("num_GastoOperativo")) = True OrElse CType(dtrDatos("num_GastoOperativo"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta % Gastos Operativos" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    'If dtrDatos("num_PorcentajeCompensacion") Is Nothing OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse CType(dtrDatos("num_PorcentajeCompensacion"), Double) < 0 Then
                    '    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Porcentaje Compensacion por Calidad" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    'End If

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

                'SI NO ES ARTICULO CRUDO VERIFICAR EN TINTORERIA
                If _strCodigoArticuloLargo.Substring(9, 1) <> 0 AndAlso _strCodigoArticuloLargo.Substring(7, 1) <> 5 Then
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

                End If

                If dstDatos.Tables("CostoHilo").Rows.Count <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_HILO" : dtrItem("var_Descripcion") = "No existen costos de hilo" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                Else
                    For Each dtrDatos As DataRow In dstDatos.Tables("CostoHilo").Rows
                        If dtrDatos("num_CostoProm") = 0 Then
                            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_HILO" : dtrItem("var_Descripcion") = "El costo del hilo " & dtrDatos("var_CodigoHilo") & " es incorrecto" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                        End If
                    Next
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
                If _strCodigoArticuloLargo.Substring(9, 1) <> 0 AndAlso _strCodigoArticuloLargo.Substring(7, 1) <> 5 Then
                    If dstDatos.Tables("CostoTinto").Rows.Count <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_TINTORERIA" : dtrItem("var_Descripcion") = "No existe Costo Hora/Maquina en Tintoreria" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                End If

                If dstDatos.Tables("CostoRevFin").Rows.Count <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_REVISION" : dtrItem("var_Descripcion") = "No existe Costo de Revision Final" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If


                'Validacion de costo de estampado

                If _dblCostoEstampado = -1 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "COSTO_TINTORERIA" : dtrItem("var_Descripcion") = "Ingresar el costo de estampado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                'Validando la formulacion
                If _strCodigoArticuloLargo <> "" Then

                    Dim lobjEstudio As NM_Costos.Estudio
                    Dim ldtFormulacion As DataTable
                    Dim dstTabDatos As DataSet

                    lobjEstudio = New NM_Costos.Estudio("sa")
                    lobjEstudio.Equivalencia = _strCodigoArticuloLargo
                    lobjEstudio.ObtenerFormulacion_V2()
                    ldtFormulacion = lobjEstudio.SetDatos.Tables(0)

                    If ldtFormulacion.Rows.Count > 0 Then

                        If IsDBNull(dstDatos.Tables("Receta").Compute("Sum(num_Concentracion)", "var_CodigoReceta='" & Trim(ldtFormulacion.Rows(0)("var_CodigoReceta").ToString) & "'")) = True Then
                            dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "La receta de la formulacion a cambiado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                        End If
                    Else
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "La receta de la formulacion a cambiado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                End If



                Return dtbRetorno
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    Public Function CargarCostos(ByVal dstDatos As DataSet, ByVal strTipo As String) As DataSet
      If strTipo = "INSUMOS" Then
        Return CargarInsumosQuimicos(dstDatos)
      ElseIf strTipo = "HILOS" Then
        Return CargarCostoHilo(dstDatos)
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

    Private Function CargarInsumosQuimicos(ByVal dstDatos As DataSet) As DataSet
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
          If IsDBNull(dtrdatos("var_CodigoIQ1")) = False AndAlso dtrdatos("var_CodigoIQ1") <> "" Then
            strDatosXML = strDatosXML & "<INSUMOS>"
            strDatosXML = strDatosXML & "<var_CodigoReceta>SR</var_CodigoReceta>"
            strDatosXML = strDatosXML & "<var_CodigoInsumo>" & dtrDatos("var_CodigoIQ1") & "</var_CodigoInsumo>"
            strDatosXML = strDatosXML & "<var_DescripcionInsumo>" & dtrDatos("var_NombreIQ1") & "</var_DescripcionInsumo>"
            strDatosXML = strDatosXML & "</INSUMOS>"
          End If
          If IsDBNull(dtrdatos("var_CodigoIQ2")) = False AndAlso dtrdatos("var_CodigoIQ2") <> "" Then
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
        Dim objParametros() As Object = {"var_DatosXML", strDatosXML}
        Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoInsumoQuimico_Obtener", objParametros)
        Dim dtcIndice(0) As DataColumn
        dtcIndice(0) = dtbDatos.Columns("var_CodigoInsumo")
        dtbDatos.PrimaryKey = dtcIndice

        'AGREGANDO LOS COSTOS DE IQ
        For Each dtrDatos As DataRow In dtbDatos.Rows
          If dstDatos.Tables("CostoInsumo").Rows.Find(dtrDatos("var_CodigoInsumo")) Is Nothing Then
            dstDatos.Tables("CostoInsumo").LoadDataRow(dtrDatos.ItemArray, True)
          End If
        Next
        dstDatos.Tables("CostoInsumo").AcceptChanges()
        'RETIRANDO IQ NO USADOS
        For Each dtrDatos As DataRow In dstDatos.Tables("CostoInsumo").Rows
          If dtbDatos.Rows.Find(dtrDatos("var_CodigoInsumo")) Is Nothing Then
            dstDatos.Tables("CostoInsumo").Rows.Find(dtrDatos("var_CodigoInsumo")).Delete()
          End If
        Next
        dstDatos.Tables("CostoInsumo").AcceptChanges()
        Return dstDatos
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Private Function CargarCostoHilo(ByVal dstDatos As DataSet) As DataSet
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
        Dim objUtil As New NM_General.Util
        strDatosXML = objUtil.EncodeXML(strDatosXML)

        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As Object = {"var_DatosXML", strDatosXML}
        Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoHilo_Obtener", objParametros)
        Dim dtcIndice(0) As DataColumn
        dtcIndice(0) = dtbDatos.Columns("var_CodigoHilo")
        dtbDatos.PrimaryKey = dtcIndice

        'AGREGANDO LOS COSTOS DE HILO
        For Each dtrDatos As DataRow In dtbDatos.Rows
          If dstDatos.Tables("CostoHilo").Rows.Find(dtrDatos("var_CodigoHilo")) Is Nothing Then
            dstDatos.Tables("CostoHilo").LoadDataRow(dtrDatos.ItemArray, True)
          End If
        Next

        'RETIRANDO HILOS NO USADOS
        For Each dtrDatos As DataRow In dstDatos.Tables("CostoHilo").Rows
          If dtbDatos.Rows.Find(dtrDatos("var_CodigoHilo")) Is Nothing Then
            dstDatos.Tables("CostoHilo").Rows.Find(dtrDatos("var_CodigoHilo")).Delete()
          End If
        Next

        Return dstDatos
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Private Function CargarCostoUrdido(ByVal dstDatos As DataSet) As DataSet
      Try

        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As Object = {"var_CodigoUrdimbre", _strCodigoArticuloCorto}
        Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoUrdido_Obtener", objParametros)
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
        Dim objParametros() As Object = {"var_CodigoEngomado", Me._strCodigoEngomado}
        Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_FCO_CostoEngomado_Obtener_2", objParametros)
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
        Dim objUtil As New NM_General.Util
        strDatosXML = objUtil.EncodeXML(strDatosXML)

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

    Public Function fnc_costototalfichaproduccion(ByVal pstr_codigoarticulolargo As String) As Double
      Try
        Dim ldbl_totalfichaproduccion As Double, ldtb_datos As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As Object = {"var_codigoarticulolargo", pstr_codigoarticulolargo}
        ldtb_datos = _objConexion.ObtenerDataTable("usp_qry_precosto_totalfichaprod", objParametros)

        ldbl_totalfichaproduccion = ldtb_datos.Rows(0)("num_costofichaproduccion")

        Return ldbl_totalfichaproduccion
      Catch ex As Exception
        Return 0
      End Try
    End Function

    Public Function GeneraFicha(ByVal dstDatos As DataSet) As DataSet
      Dim dstFicha As DataSet = CreateSchemaFicha()
      dstFicha = BuildFicha(dstDatos, dstFicha)
      Return dstFicha
    End Function
        Public Function GeneraFicha_V2(ByVal dstDatos As DataSet) As DataSet
            Dim dstFicha As DataSet = CreateSchemaFicha()
            dstFicha = BuildFicha_V2(dstDatos, dstFicha)
            Return dstFicha
        End Function

    Private Function CreateSchemaFicha() As DataSet
      Dim dtbDatos As DataTable
      Dim dtcPrimary(0) As DataColumn

      _dstSchema = New DataSet
      'CABECERA
      dtbDatos = New DataTable
      dtbDatos.Columns.Add("var_CodigoArticuloLargo", GetType(String))
      dtbDatos.Columns.Add("var_CodigoFicha", GetType(String))
      dtbDatos.Columns.Add("var_CodigoArticuloCorto", GetType(String))
      dtbDatos.Columns.Add("num_TipoCambio", GetType(Double))
      dtbDatos.Columns.Add("num_TipoCambioSugerido", GetType(Double))
      dtbDatos.Columns.Add("num_GastoOperativo", GetType(Double))
      dtbDatos.Columns.Add("num_CostoCalidad", GetType(Double))
      dtbDatos.Columns.Add("mon_CostoCrudo", GetType(Double))
      dtbDatos.Columns.Add("mon_CostoAcabado", GetType(Double))
      dtbDatos.Columns.Add("num_PrecioReal", GetType(Double))
      dtbDatos.Columns.Add("num_PrecioVenta", GetType(Double))
      dtbDatos.Columns.Add("num_PorcentajeCompensacion", GetType(Double))
      dtbDatos.Columns.Add("var_DescArticulo1", GetType(String))
      dtbDatos.Columns.Add("var_DescArticulo2", GetType(String))
      dtbDatos.Columns.Add("chr_TipoFicha", GetType(String))
      dtbDatos.Columns.Add("var_Glosa", GetType(String))
      ReDim dtcPrimary(1)
      dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticuloLargo")
      dtcPrimary(1) = dtbDatos.Columns("var_CodigoFicha")
      dtbDatos.PrimaryKey = dtcPrimary
      dtbDatos.TableName = "FichaCabecera"
      _dstSchema.Tables.Add(dtbDatos)

      '------------------------------------------------------------------------
      ' DETALLE
      ' Modificado: Guardar tipo de calculo de telares (N: Normal, P: Promedio)
      ' Mayo 2013
      ' Alexander Torres Cardenas
      '------------------------------------------------------------------------
      dtbDatos = New DataTable
      dtbDatos.Columns.Add("var_CodigoFicha", GetType(String))
      dtbDatos.Columns.Add("chr_CodigoEtapa", GetType(String))
      dtbDatos.Columns.Add("var_CodigoArticuloLargo", GetType(String))
      dtbDatos.Columns.Add("var_TipoDato", GetType(String))
      dtbDatos.Columns.Add("int_Secuencia", GetType(Int32))
      dtbDatos.Columns.Add("var_Dato1", GetType(String))
      dtbDatos.Columns.Add("var_Dato2", GetType(String))
      dtbDatos.Columns.Add("var_Dato3", GetType(String))
      dtbDatos.Columns.Add("var_Dato4", GetType(String))
      dtbDatos.Columns.Add("var_Dato5", GetType(String))
      dtbDatos.Columns.Add("var_Dato6", GetType(String))
      dtbDatos.Columns.Add("var_Dato7", GetType(String))
      dtbDatos.Columns.Add("var_Dato8", GetType(String))
      dtbDatos.Columns.Add("num_MateriaPrima", GetType(Double))
      dtbDatos.Columns.Add("num_InsumoQuimico", GetType(Double))
      dtbDatos.Columns.Add("num_OtroGasto", GetType(Double))
      dtbDatos.Columns.Add("num_FijoVariable", GetType(Double))
      dtbDatos.Columns.Add("num_FijoNoVariable", GetType(Double))
      dtbDatos.Columns.Add("num_ManoObraFija", GetType(Double))
      dtbDatos.Columns.Add("num_ManoObraVariable", GetType(Double))
      dtbDatos.Columns.Add("var_TipoCalculo", GetType(String))

      ReDim dtcPrimary(5)
      dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticuloLargo")
      dtcPrimary(1) = dtbDatos.Columns("var_CodigoFicha")
      dtcPrimary(2) = dtbDatos.Columns("chr_CodigoEtapa")
      dtcPrimary(3) = dtbDatos.Columns("var_TipoDato")
      dtcPrimary(4) = dtbDatos.Columns("int_Secuencia")
      dtbDatos.PrimaryKey = dtcPrimary
      dtbDatos.TableName = "FichaDetalle"
      _dstSchema.Tables.Add(dtbDatos)

      'RESUMEN
      dtbDatos = New DataTable
      dtbDatos.Columns.Add("chr_CodigoEtapa", GetType(String))
      dtbDatos.Columns.Add("var_CodigoFicha", GetType(String))
      dtbDatos.Columns.Add("var_CodigoArticuloLargo", GetType(String))
      dtbDatos.Columns.Add("var_NombreResumen", GetType(String))
      dtbDatos.Columns.Add("num_MateriaPrima", GetType(Double))
      dtbDatos.Columns.Add("num_InsumoQuimico", GetType(Double))
      dtbDatos.Columns.Add("num_OtroGasto", GetType(Double))
      dtbDatos.Columns.Add("num_FijoVariable", GetType(Double))
      dtbDatos.Columns.Add("num_FijoNoVariable", GetType(Double))
      dtbDatos.Columns.Add("num_Porcentaje", GetType(Double))
      dtbDatos.Columns.Add("num_ManoObraVariable", GetType(Double))
      dtbDatos.Columns.Add("num_ManoObraFija", GetType(Double))

      ReDim dtcPrimary(3)
      dtcPrimary(0) = dtbDatos.Columns("var_CodigoArticuloLargo")
      dtcPrimary(1) = dtbDatos.Columns("var_CodigoFicha")
      dtcPrimary(2) = dtbDatos.Columns("chr_CodigoEtapa")
      dtbDatos.PrimaryKey = dtcPrimary
      dtbDatos.TableName = "FichaResumen"
      _dstSchema.Tables.Add(dtbDatos)
      'OTROS
      dtbDatos = New DataTable
      dtbDatos.Columns.Add("ANCHO_ESTANDAR", GetType(Double))
      dtbDatos.Columns.Add("ONZAS", GetType(Double))
      dtbDatos.TableName = "FichaAdicional"
      _dstSchema.Tables.Add(dtbDatos)

      Return _dstSchema
    End Function
        Private Function BuildFicha_V2(ByVal dstDatos As DataSet, ByVal dstFicha As DataSet) As DataSet

            Dim dtrFicha As DataRow, dtrAuxiliar As DataRow, dtrArticulo As DataRow, dtrEngomado As DataRow
            Dim dblPesoUrdimbre As Double = 0
            Dim dblCalcPesoUrdimbre As Double = 0
            '******************************************************************
            'CABECERA
            '******************************************************************
            dtrArticulo = dstDatos.Tables("Articulo").Rows(0)
            dtrEngomado = dstDatos.Tables("Engomado").Rows(0)
            dtrFicha = dstFicha.Tables("FichaCabecera").NewRow
            dtrFicha("var_CodigoArticuloLargo") = ""
            dtrFicha("var_CodigoFicha") = ""
            dtrFicha("var_CodigoArticuloCorto") = ""

            dtrFicha("num_TipoCambio") = dtrArticulo("num_TipoCambio")
            dtrFicha("num_TipoCambioSugerido") = dtrArticulo("num_TipoCambio")
            dtrFicha("num_PorcentajeCompensacion") = 0 'dtrArticulo("num_PorcentajeCompensacion") / 100.0
            dtrFicha("num_GastoOperativo") = dtrArticulo("num_GastoOperativo")
            dtrFicha("num_CostoCalidad") = 0 'dtrArticulo("num_PorcentajeCompensacion")
            dtrFicha("mon_CostoCrudo") = 0
            dtrFicha("mon_CostoAcabado") = 0
            dtrFicha("num_PrecioReal") = 0
            dtrFicha("num_PrecioVenta") = 0
            dtrFicha("var_DescArticulo1") = dtrArticulo("var_DescArticulo1")
            dtrFicha("var_DescArticulo2") = dtrArticulo("var_DescArticulo2")
            dtrFicha("chr_TipoFicha") = "S"
            dtrFicha("var_Glosa") = dtrArticulo("var_Glosa")
            dstFicha.Tables("FichaCabecera").LoadDataRow(dtrFicha.ItemArray, True)
            dstFicha.Tables("FichaCabecera").AcceptChanges() '

            '******************************************************************
            'DETALLE - HILO URDIMBRE
            '******************************************************************
            For Each dtrDato As DataRow In dstDatos.Tables("Urdimbre").Rows
                dtrAuxiliar = dstDatos.Tables("CostoHilo").Rows.Find(dtrDato("var_CodigoHilo"))

                dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
                dtrFicha("var_CodigoFicha") = ""
                dtrFicha("chr_CodigoEtapa") = "001"
                dtrFicha("var_CodigoArticuloLargo") = ""
                dtrFicha("var_TipoDato") = "URDIMBRE"
                dtrFicha("int_Secuencia") = dtrDato("int_Secuencia")
                dtrFicha("var_Dato1") = dtrDato("var_CodigoHilo")
                dtrFicha("var_Dato2") = dtrAuxiliar("num_CostoProm")
                dtrFicha("var_Dato3") = dtrDato("num_HilosPulgada")
                dtrFicha("var_Dato4") = dtrDato("int_NumeroHilos")
                dtrFicha("var_Dato5") = dtrDato("num_GramosMetro")
                dtrFicha("var_Dato6") = dtrArticulo("num_EncogimientoUrdimbre")
                dtrFicha("var_Dato7") = dtrDato("var_DescripcionHilo")
                dtrFicha("var_Dato8") = ""
                dtrFicha("num_MateriaPrima") = (dtrAuxiliar("num_CostoMP") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000.0 * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_InsumoQuimico") = 0
                dtrFicha("num_OtroGasto") = (dtrAuxiliar("num_CostoOTR") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_FijoVariable") = (dtrAuxiliar("num_CostoFV") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_FijoNoVariable") = (dtrAuxiliar("num_CostoCI") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_ManoObraFija") = (dtrAuxiliar("num_CostoMOF") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_ManoObraVariable") = (dtrAuxiliar("num_CostoMOV") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
                dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)

                dblPesoUrdimbre = dblPesoUrdimbre + (0.59 * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000)

            Next

            '******************************************************************
            'DETALLE - HILO TRAMA
            '******************************************************************
            For Each dtrDato As DataRow In dstDatos.Tables("Trama").Rows
                dtrAuxiliar = dstDatos.Tables("CostoHilo").Rows.Find(dtrDato("var_CodigoHilo"))

                dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
                dtrFicha("var_CodigoFicha") = ""
                dtrFicha("chr_CodigoEtapa") = "001"
                dtrFicha("var_CodigoArticuloLargo") = ""
                dtrFicha("var_TipoDato") = "TRAMA"
                dtrFicha("int_Secuencia") = dtrDato("int_Secuencia")
                dtrFicha("var_Dato1") = dtrDato("var_CodigoHilo")
                dtrFicha("var_Dato2") = dtrAuxiliar("num_CostoProm")
                dtrFicha("var_Dato3") = dtrDato("num_AnchoCrudo")
                dtrFicha("var_Dato4") = dtrDato("int_NumeroHilos")
                dtrFicha("var_Dato5") = dtrDato("num_GramosMetro")
                dtrFicha("var_Dato6") = dtrArticulo("num_EncogimientoTrama")
                dtrFicha("var_Dato7") = dtrDato("var_DescripcionHilo")
                dtrFicha("var_Dato8") = ""
                dtrFicha("num_MateriaPrima") = (dtrAuxiliar("num_CostoMP") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_InsumoQuimico") = 0
                dtrFicha("num_OtroGasto") = (dtrAuxiliar("num_CostoOTR") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_FijoVariable") = (dtrAuxiliar("num_CostoFV") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_FijoNoVariable") = (dtrAuxiliar("num_CostoCI") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_ManoObraFija") = (dtrAuxiliar("num_CostoMOF") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
                dtrFicha("num_ManoObraVariable") = (dtrAuxiliar("num_CostoMOV") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
                dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
            Next

            '******************************************************************
            'COSTO DE URDIDO
            '******************************************************************
            For Each dtrDato As DataRow In dstDatos.Tables("CostoUrdido").Rows
                dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
                dtrFicha("var_CodigoFicha") = ""
                dtrFicha("chr_CodigoEtapa") = "002"
                dtrFicha("var_CodigoArticuloLargo") = ""
                dtrFicha("var_TipoDato") = "URDIDO"
                dtrFicha("int_Secuencia") = 0
                dtrFicha("var_Dato1") = dtrArticulo("num_Velocidad1")
                dtrFicha("var_Dato2") = dtrArticulo("num_Velocidad2")
                dtrFicha("var_Dato3") = dtrArticulo("num_Velocidad3")
                dtrFicha("var_Dato4") = dtrArticulo("num_Velocidad4")
                dtrFicha("var_Dato5") = ""
                dtrFicha("var_Dato6") = ""
                dtrFicha("var_Dato7") = ""
                dtrFicha("var_Dato8") = ""
                dtrFicha("num_MateriaPrima") = 0
                dtrFicha("num_InsumoQuimico") = 0
                dtrFicha("num_OtroGasto") = dtrDato("num_CostoOTR")
                dtrFicha("num_FijoVariable") = dtrDato("num_CostoFV")
                dtrFicha("num_FijoNoVariable") = dtrDato("num_CostoCI")
                dtrFicha("num_ManoObraFija") = dtrDato("num_CostoMOF")
                dtrFicha("num_ManoObraVariable") = dtrDato("num_CostoMOV")
                dtrFicha("var_TipoCalculo") = dtrDato("var_TipoCalculo")
                dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
            Next

            '******************************************************************
            'COSTO DE ENGOMADO
            '******************************************************************
            For Each dtrFormulacion As DataRow In dstDatos.Tables("Formulacion").Rows
                Dim num_SolLitroReceta As Double = 0
                Dim num_ConcentracionTotal As Double = dstDatos.Tables("Receta").Compute("Sum(num_Concentracion)", "var_CodigoReceta='" & Trim(dtrFormulacion("var_CodigoReceta")) & "'")
                Dim num_CostoReceta As Double
                Dim num_EncogimientoUrdimbre As Double = dtrArticulo("num_EncogimientoUrdimbre")
                Dim num_Estiraje As Double = dtrEngomado("num_Estiraje")

                'CALCULANDO EL SOL/LITRO
                For Each dtrReceta As DataRow In dstDatos.Tables("Receta").Select("var_CodigoReceta='" & Trim(dtrFormulacion("var_CodigoReceta")) & "'")
                    dtrAuxiliar = dstDatos.Tables("CostoInsumo").Rows.Find(dtrReceta("var_CodigoInsumo"))
                    If dtrEngomado("var_CodigoTipo") = "ENGCRU" Then
                        num_SolLitroReceta = num_SolLitroReceta + (dtrAuxiliar("num_CostoPromedio") * dtrReceta("num_PorcentajeGramosLitro"))
                    ElseIf dtrEngomado("var_CodigoTipo") = "ENGTED" Then
                        num_SolLitroReceta = num_SolLitroReceta + (dtrReceta("num_Concentracion") * dtrAuxiliar("num_CostoPromedio") / 1000)
                    End If
                Next

                'CALCULANDO EL COSTO DE RECETA
                If dtrEngomado("var_CodigoTipo") <> "ENGCRU" Then
                    If dtrFormulacion("var_CodigoFase") = 2 Then
                        num_CostoReceta = (num_SolLitroReceta * dtrFormulacion("num_Dosificacion") / dtrEngomado("num_Velocidad")) / (num_Estiraje) / (1 - num_EncogimientoUrdimbre)
                    Else
                        num_CostoReceta = (num_SolLitroReceta * dtrFormulacion("num_Dosificacion")) / (num_Estiraje) / (1 - num_EncogimientoUrdimbre)
                    End If
                Else
                    dblCalcPesoUrdimbre = dblPesoUrdimbre * (dtrFormulacion("num_Pickup") / 100)
                    num_CostoReceta = num_SolLitroReceta * (dblCalcPesoUrdimbre / ((1 - num_EncogimientoUrdimbre) * num_Estiraje))

                End If

                '-------------------------------------------
                'INICIO: CALCULANDO COSTO DE IQs ADICIONALES
                '-------------------------------------------
                Dim num_CostoIQ1 As Double = 0, num_CostoIQ2 As Double = 0

                If IsDBNull(dtrFormulacion("var_CodigoIQ1")) = False AndAlso dtrFormulacion("var_CodigoIQ1") <> "" Then
                    dtrAuxiliar = dstDatos.Tables("CostoInsumo").Rows.Find(dtrFormulacion("var_CodigoIQ1"))

                    If Trim(UCase(dtrFormulacion("var_UnidadIQ1"))) = Trim(UCase("gr/min")) Then
                        num_CostoIQ1 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ1") / dtrEngomado("num_Velocidad"))) / 1000 * dtrAuxiliar("num_CostoPromedio")
                    ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ1"))) = Trim(UCase("cc/min")) Then
                        num_CostoIQ1 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ1") / dtrEngomado("num_Velocidad")) * 1.53 / 2) / 1000 * dtrAuxiliar("num_CostoPromedio")
                    ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ1"))) = Trim(UCase("gr/mt")) Then
                        num_CostoIQ1 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * dtrFormulacion("num_DosisIQ1")) / 1000 * dtrAuxiliar("num_CostoPromedio")
                    End If
                End If

                If IsDBNull(dtrFormulacion("var_CodigoIQ2")) = False AndAlso dtrFormulacion("var_CodigoIQ2") <> "" Then
                    dtrAuxiliar = dstDatos.Tables("CostoInsumo").Rows.Find(dtrFormulacion("var_CodigoIQ2"))

                    If Trim(UCase(dtrFormulacion("var_UnidadIQ2"))) = Trim(UCase("gr/min")) Then
                        num_CostoIQ2 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ2") / dtrEngomado("num_Velocidad"))) / 1000 * dtrAuxiliar("num_CostoPromedio")
                    ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ2"))) = Trim(UCase("cc/min")) Then
                        num_CostoIQ2 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ2") / dtrEngomado("num_Velocidad")) * 1.53 / 2) / 1000 * dtrAuxiliar("num_CostoPromedio")
                    ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ2"))) = Trim(UCase("gr/mt")) Then
                        num_CostoIQ2 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * dtrFormulacion("num_DosisIQ2")) / 1000 * dtrAuxiliar("num_CostoPromedio")
                    End If
                End If
                '-----------------------------------------
                'FINAL: CALCULANDO COSTO DE IQs ADICIONALES
                '-----------------------------------------

                '-------------------------------------------
                'AGREGANDO LOS DATOS DE IQ
                '-------------------------------------------
                dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
                dtrFicha("var_CodigoFicha") = ""
                dtrFicha("chr_CodigoEtapa") = "002"
                dtrFicha("var_CodigoArticuloLargo") = ""
                If dtrFormulacion("var_CodigoFase") = 1 Then
                    dtrFicha("var_TipoDato") = "PRE-TRATAMIENTO"
                ElseIf dtrFormulacion("var_CodigoFase") = 2 Then
                    dtrFicha("var_TipoDato") = "TEÑIDO"
                ElseIf dtrFormulacion("var_CodigoFase") = 3 Then
                    dtrFicha("var_TipoDato") = "ENGOMADO"
                End If
                Dim strWhere As String = "var_TipoDato='" & dtrFicha("var_TipoDato") & "' " & _
                " and chr_CodigoEtapa='002' and int_Secuencia = " & dtrFormulacion("var_CodigoFase")
                Dim dblCostoRecetaEtapa As Double = 0
                If IsDBNull(dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", strWhere)) = False Then
                    dblCostoRecetaEtapa = CType(dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", strWhere), Double)
                End If

                dtrFicha("int_Secuencia") = dtrFormulacion("var_CodigoFase")
                dtrFicha("var_Dato1") = dtrEngomado("num_Velocidad")
                dtrFicha("var_Dato2") = dtrEngomado("num_Estiraje")
                dtrFicha("var_Dato3") = dtrFormulacion("var_CodigoFase")
                dtrFicha("var_Dato4") = dtrEngomado("var_CodigoEngomado")
                dtrFicha("var_Dato5") = ""
                dtrFicha("var_Dato6") = ""
                dtrFicha("var_Dato7") = ""
                dtrFicha("var_Dato8") = ""
                dtrFicha("num_MateriaPrima") = 0
                dtrFicha("num_InsumoQuimico") = dblCostoRecetaEtapa + num_CostoReceta + num_CostoIQ1 + num_CostoIQ2
                dtrFicha("num_OtroGasto") = 0
                dtrFicha("num_FijoVariable") = 0
                dtrFicha("num_FijoNoVariable") = 0
                dtrFicha("num_ManoObraFija") = 0
                dtrFicha("num_ManoObraVariable") = 0
                dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
                dstFicha.Tables("FichaDetalle").AcceptChanges()
            Next
            'OBTENIENDO EL COSTO DE ENGOMADO
            dtrAuxiliar = dstDatos.Tables("CostoEngomado").Rows(0)

            'AGREGANDO LOS DATOS DE IQ
            dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
            dtrFicha("var_CodigoFicha") = ""
            dtrFicha("chr_CodigoEtapa") = "002"
            dtrFicha("var_CodigoArticuloLargo") = ""
            dtrFicha("var_TipoDato") = ""
            dtrFicha("int_Secuencia") = 4
            dtrFicha("var_Dato1") = dtrEngomado("num_Velocidad")
            dtrFicha("var_Dato2") = dtrEngomado("num_Estiraje")
            dtrFicha("var_Dato3") = 9
            dtrFicha("var_Dato4") = dtrEngomado("var_CodigoEngomado")
            dtrFicha("var_Dato5") = ""
            dtrFicha("var_Dato6") = ""
            dtrFicha("var_Dato7") = ""
            dtrFicha("var_Dato8") = ""
            dtrFicha("num_MateriaPrima") = 0
            dtrFicha("num_InsumoQuimico") = 0
            dtrFicha("num_OtroGasto") = dtrAuxiliar("num_CostoOTR")
            dtrFicha("num_FijoVariable") = dtrAuxiliar("num_CostoFV")
            dtrFicha("num_FijoNoVariable") = dtrAuxiliar("num_CostoCI")
            dtrFicha("num_ManoObraFija") = dtrAuxiliar("num_CostoMOF")
            dtrFicha("num_ManoObraVariable") = dtrAuxiliar("num_CostoMOV")
            dtrFicha("var_TipoCalculo") = dtrAuxiliar("var_TipoCalculo")
            dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)

            '--------------------------------------------------------------------------
            ' COSTO DE TELARES
            ' Modificado: Guardar tipo de calculo de telares (E: Estandar, P: Promedio)
            ' Mayo 2013
            ' Alexander Torres Cardenas
            '--------------------------------------------------------------------------
            For Each dtrDato As DataRow In dstDatos.Tables("CostoTelar").Rows
                dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
                dtrFicha("var_CodigoFicha") = ""
                dtrFicha("chr_CodigoEtapa") = "003"
                dtrFicha("var_CodigoArticuloLargo") = ""
                dtrFicha("var_TipoDato") = ""
                dtrFicha("int_Secuencia") = 0
                dtrFicha("var_Dato1") = dtrArticulo("var_TipoMaquina")
                dtrFicha("var_Dato2") = dtrArticulo("int_NumeroTelas")
                dtrFicha("var_Dato3") = dtrArticulo("num_VelocidadTeorica")
                dtrFicha("var_Dato4") = dtrArticulo("int_AnchoCrudo")
                dtrFicha("var_Dato5") = dtrArticulo("num_AnchoPeine")
                dtrFicha("var_Dato6") = dtrArticulo("num_EficienciaReal")
                dtrFicha("var_Dato7") = dtrArticulo("var_Ligamento")
                dtrFicha("var_Dato8") = dstDatos.Tables("Urdimbre").Compute("Sum(num_GramosMetro)", "") + dstDatos.Tables("Trama").Compute("Sum(num_GramosMetro)", "")
                dtrFicha("num_MateriaPrima") = 0
                dtrFicha("num_InsumoQuimico") = 0
                dtrFicha("num_OtroGasto") = dtrDato("num_CostoOTR")
                dtrFicha("num_FijoVariable") = dtrDato("num_CostoFV")
                dtrFicha("num_FijoNoVariable") = dtrDato("num_CostoCI")
                dtrFicha("num_ManoObraFija") = dtrDato("num_CostoMOF")
                dtrFicha("num_ManoObraVariable") = dtrDato("num_CostoMOV")
                dtrFicha("var_TipoCalculo") = dtrDato("var_TipoCalculo")
                dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
            Next

            '******************************************************************
            'COSTO TINTORERIA
            '******************************************************************
            Dim num_FactorTintoreria As Double = (100.0 - dtrArticulo("num_VariacionDimensional")) / 100.0
            For Each dtrRuta As DataRow In dstDatos.Tables("RutaTintoreria").Rows
                Dim Factor As Double = 1
                If dtrRuta("var_CodigoMaquina") <> "000782" Then
                    Factor = num_FactorTintoreria
                End If
                dtrAuxiliar = dstDatos.Tables("CostoTinto").Rows.Find(dtrRuta("var_CodigoMaquina"))
                Dim num_CostoReceta As Double = 0
                If IsDBNull(dtrRuta("var_CodigoReceta")) = False AndAlso dtrRuta("var_CodigoReceta") <> "" Then
                    Dim num_Pickup As Double = 0, num_Peso As Double = 0

                    For Each dtrPickup As DataRow In dstDatos.Tables("PickupTintoreria").Select("int_Secuencia=" & dtrRuta("int_Secuencia"), "")
                        num_Pickup = dtrPickup("num_Pickup")
                        num_Peso = dtrPickup("num_Peso")
                        Dim CostoBruto As Double = 0
                        For Each dtrInsumo As DataRow In dstDatos.Tables("RecetaInsumoTinto").Select("var_CodigoReceta='" & dtrPickup("var_CodigoReceta") & "'")
                            Dim dtrCostoIQ = dstDatos.Tables("CostoInsumo").Rows.Find(dtrInsumo("var_CodigoInsumo"))
                            CostoBruto = CostoBruto + (dtrCostoIQ("num_Factor") * dtrCostoIQ("num_CostoPromedio") * dtrInsumo("num_Concentracion") / 1000.0)
                        Next
                        num_CostoReceta = num_CostoReceta + (CostoBruto * num_Pickup / 100) * num_Peso * dtrRuta("int_Pases")
                    Next
                End If

                dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
                dtrFicha("var_CodigoFicha") = ""
                dtrFicha("chr_CodigoEtapa") = "004"
                dtrFicha("var_CodigoArticuloLargo") = ""
                dtrFicha("var_TipoDato") = dtrRuta("var_DescripcionEtapa")
                dtrFicha("int_Secuencia") = dtrRuta("int_Secuencia")
                dtrFicha("var_Dato1") = dtrRuta("int_Secuencia")
                If IsDBNull(dtrRuta("var_CodigoReceta")) = False Then
                    dtrFicha("var_Dato2") = dtrRuta("var_CodigoReceta")
                Else
                    dtrFicha("var_Dato2") = ""
                End If
                dtrFicha("var_Dato3") = dtrArticulo("num_VariacionDimensional")
                dtrFicha("var_Dato4") = dtrRuta("var_CodigoMaquina")
                dtrFicha("var_Dato5") = dtrRuta("var_CodigoOperacion")
                dtrFicha("var_Dato6") = dtrRuta("var_DescripcionOperacion")
                dtrFicha("var_Dato7") = dtrRuta("num_VelocidadMaquina")
                dtrFicha("var_Dato8") = dtrRuta("int_Pases")
                dtrFicha("num_MateriaPrima") = 0

                If dtrRuta("var_CodigoMaquina") = "000071" OrElse dtrRuta("var_CodigoMaquina") = "006801" Then
                    num_CostoReceta = _dblCostoEstampado
                End If

                dtrFicha("num_InsumoQuimico") = num_CostoReceta

                If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
                    dtrFicha("num_OtroGasto") = (dtrAuxiliar("num_CostoOTR") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
                Else
                    dtrFicha("num_OtroGasto") = 0
                End If

                If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
                    dtrFicha("num_FijoVariable") = (dtrAuxiliar("num_CostoFV") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
                Else
                    dtrFicha("num_FijoVariable") = 0
                End If

                If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
                    dtrFicha("num_FijoNoVariable") = (dtrAuxiliar("num_CostoCI") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
                Else
                    dtrFicha("num_FijoNoVariable") = 0
                End If

                If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
                    dtrFicha("num_ManoObraFija") = (dtrAuxiliar("num_CostoMOF") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
                Else
                    dtrFicha("num_ManoObraFija") = 0
                End If

                If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
                    dtrFicha("num_ManoObraVariable") = (dtrAuxiliar("num_CostoMOV") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
                Else
                    dtrFicha("num_ManoObraVariable") = 0
                End If
                dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)

            Next
            dstFicha.Tables("FichaDetalle").AcceptChanges()

            Dim dtrDatos As DataRow

            dtrDatos = dstFicha.Tables("FichaResumen").NewRow
            dtrDatos("var_CodigoArticuloLargo") = ""
            dtrDatos("var_CodigoFicha") = ""
            dtrDatos("chr_CodigoEtapa") = "001"
            dtrDatos("var_NombreResumen") = "COSTO HILO"
            dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
            dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
            dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)


            dtrDatos = dstFicha.Tables("FichaResumen").NewRow
            dtrDatos("var_CodigoArticuloLargo") = ""
            dtrDatos("var_CodigoFicha") = ""
            dtrDatos("chr_CodigoEtapa") = "002"
            dtrDatos("var_NombreResumen") = "COSTO PRETEJIDO"
            dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
            dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
            dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)


            dtrDatos = dstFicha.Tables("FichaResumen").NewRow
            dtrDatos("var_CodigoArticuloLargo") = ""
            dtrDatos("var_CodigoFicha") = ""
            dtrDatos("chr_CodigoEtapa") = "003"
            dtrDatos("var_NombreResumen") = "COSTO TELARES"
            dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
            dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
            dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)

            dtrDatos = dstFicha.Tables("FichaResumen").NewRow
            dtrDatos("var_CodigoArticuloLargo") = ""
            dtrDatos("var_CodigoFicha") = ""
            dtrDatos("chr_CodigoEtapa") = "004"
            dtrDatos("var_NombreResumen") = "COSTO TINTORERIA"
            dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='004'")
            dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='004'")
            dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='004'")
            dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='004'")
            dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='004'")
            dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='004'")
            dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='004'")
            dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
            dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)


            For Each dtrRevFin As DataRow In dstDatos.Tables("CostoRevFin").Rows
                dtrDatos = dstFicha.Tables("FichaResumen").NewRow
                dtrDatos("var_CodigoArticuloLargo") = ""
                dtrDatos("var_CodigoFicha") = ""
                dtrDatos("chr_CodigoEtapa") = "005"
                dtrDatos("var_NombreResumen") = "COSTO REVISION FINAL"
                dtrDatos("num_MateriaPrima") = 0
                dtrDatos("num_InsumoQuimico") = 0
                dtrDatos("num_OtroGasto") = dtrRevFin("num_CostoOTR")
                dtrDatos("num_FijoVariable") = dtrRevFin("num_CostoFV")
                dtrDatos("num_FijoNoVariable") = dtrRevFin("num_CostoCI")
                dtrDatos("num_ManoObraVariable") = dtrRevFin("num_CostoMOV")
                dtrDatos("num_ManoObraFija") = dtrRevFin("num_CostoMOF")
                dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
                dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)
            Next
            dstFicha.Tables("FichaResumen").AcceptChanges()

            Dim dtrAdicional As DataRow = dstFicha.Tables("FichaAdicional").NewRow
            dtrAdicional("ANCHO_ESTANDAR") = dtrArticulo("num_AnchoEstandar")
            dtrAdicional("ONZAS") = dtrArticulo("num_PesoOnzas")
            dstFicha.Tables("FichaAdicional").LoadDataRow(dtrAdicional.ItemArray, True)
            dstFicha.Tables("FichaAdicional").AcceptChanges()

            Return dstFicha
        End Function

    Private Function BuildFicha(ByVal dstDatos As DataSet, ByVal dstFicha As DataSet) As DataSet

      Dim dtrFicha As DataRow, dtrAuxiliar As DataRow, dtrArticulo As DataRow, dtrEngomado As DataRow
      Dim dblPesoUrdimbre As Double = 0
      Dim dblCalcPesoUrdimbre As Double = 0
      '******************************************************************
      'CABECERA
      '******************************************************************
      dtrArticulo = dstDatos.Tables("Articulo").Rows(0)
      dtrEngomado = dstDatos.Tables("Engomado").Rows(0)
      dtrFicha = dstFicha.Tables("FichaCabecera").NewRow
      dtrFicha("var_CodigoArticuloLargo") = ""
      dtrFicha("var_CodigoFicha") = ""
      dtrFicha("var_CodigoArticuloCorto") = ""

      dtrFicha("num_TipoCambio") = dtrArticulo("num_TipoCambio")
      dtrFicha("num_TipoCambioSugerido") = dtrArticulo("num_TipoCambio")
      dtrFicha("num_PorcentajeCompensacion") = dtrArticulo("num_PorcentajeCompensacion") / 100.0
      dtrFicha("num_GastoOperativo") = dtrArticulo("num_GastoOperativo")
      dtrFicha("num_CostoCalidad") = dtrArticulo("num_PorcentajeCompensacion")
      dtrFicha("mon_CostoCrudo") = 0
      dtrFicha("mon_CostoAcabado") = 0
      dtrFicha("num_PrecioReal") = 0
      dtrFicha("num_PrecioVenta") = 0
      dtrFicha("var_DescArticulo1") = dtrArticulo("var_DescArticulo1")
      dtrFicha("var_DescArticulo2") = dtrArticulo("var_DescArticulo2")
      dtrFicha("chr_TipoFicha") = "S"
      dtrFicha("var_Glosa") = dtrArticulo("var_Glosa")
      dstFicha.Tables("FichaCabecera").LoadDataRow(dtrFicha.ItemArray, True)
      dstFicha.Tables("FichaCabecera").AcceptChanges() '

      '******************************************************************
      'DETALLE - HILO URDIMBRE
      '******************************************************************
      For Each dtrDato As DataRow In dstDatos.Tables("Urdimbre").Rows
        dtrAuxiliar = dstDatos.Tables("CostoHilo").Rows.Find(dtrDato("var_CodigoHilo"))

        dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
        dtrFicha("var_CodigoFicha") = ""
        dtrFicha("chr_CodigoEtapa") = "001"
        dtrFicha("var_CodigoArticuloLargo") = ""
        dtrFicha("var_TipoDato") = "URDIMBRE"
        dtrFicha("int_Secuencia") = dtrDato("int_Secuencia")
        dtrFicha("var_Dato1") = dtrDato("var_CodigoHilo")
        dtrFicha("var_Dato2") = dtrAuxiliar("num_CostoProm")
        dtrFicha("var_Dato3") = dtrDato("num_HilosPulgada")
        dtrFicha("var_Dato4") = dtrDato("int_NumeroHilos")
        dtrFicha("var_Dato5") = dtrDato("num_GramosMetro")
        dtrFicha("var_Dato6") = dtrArticulo("num_EncogimientoUrdimbre")
        dtrFicha("var_Dato7") = dtrDato("var_DescripcionHilo")
        dtrFicha("var_Dato8") = ""
        dtrFicha("num_MateriaPrima") = (dtrAuxiliar("num_CostoMP") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000.0 * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_InsumoQuimico") = 0
        dtrFicha("num_OtroGasto") = (dtrAuxiliar("num_CostoOTR") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_FijoVariable") = (dtrAuxiliar("num_CostoFV") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_FijoNoVariable") = (dtrAuxiliar("num_CostoCI") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_ManoObraFija") = (dtrAuxiliar("num_CostoMOF") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_ManoObraVariable") = (dtrAuxiliar("num_CostoMOV") * 0.59 * dtrDato("int_NumeroHilos") * (1 + dtrArticulo("num_EncogimientoUrdimbre"))) / (dtrEngomado("num_Estiraje") * dtrDato("num_TituloReal") * 1000 * (1 - dtrArticulo("num_MermaTejeduria")))
        dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)

        dblPesoUrdimbre = dblPesoUrdimbre + (0.59 * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000)

      Next

      '******************************************************************
      'DETALLE - HILO TRAMA
      '******************************************************************
      For Each dtrDato As DataRow In dstDatos.Tables("Trama").Rows
        dtrAuxiliar = dstDatos.Tables("CostoHilo").Rows.Find(dtrDato("var_CodigoHilo"))

        dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
        dtrFicha("var_CodigoFicha") = ""
        dtrFicha("chr_CodigoEtapa") = "001"
        dtrFicha("var_CodigoArticuloLargo") = ""
        dtrFicha("var_TipoDato") = "TRAMA"
        dtrFicha("int_Secuencia") = dtrDato("int_Secuencia")
        dtrFicha("var_Dato1") = dtrDato("var_CodigoHilo")
        dtrFicha("var_Dato2") = dtrAuxiliar("num_CostoProm")
        dtrFicha("var_Dato3") = dtrDato("num_AnchoCrudo")
        dtrFicha("var_Dato4") = dtrDato("int_NumeroHilos")
        dtrFicha("var_Dato5") = dtrDato("num_GramosMetro")
        dtrFicha("var_Dato6") = dtrArticulo("num_EncogimientoTrama")
        dtrFicha("var_Dato7") = dtrDato("var_DescripcionHilo")
        dtrFicha("var_Dato8") = ""
        dtrFicha("num_MateriaPrima") = (dtrAuxiliar("num_CostoMP") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_InsumoQuimico") = 0
        dtrFicha("num_OtroGasto") = (dtrAuxiliar("num_CostoOTR") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_FijoVariable") = (dtrAuxiliar("num_CostoFV") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_FijoNoVariable") = (dtrAuxiliar("num_CostoCI") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_ManoObraFija") = (dtrAuxiliar("num_CostoMOF") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
        dtrFicha("num_ManoObraVariable") = (dtrAuxiliar("num_CostoMOV") * 0.59 * dtrDato("num_AnchoCrudo") * dtrDato("int_NumeroHilos")) / (dtrDato("num_TituloReal") * 1000 * 2.54 * (1 - dtrArticulo("num_EncogimientoTrama")) * (1 - dtrArticulo("num_MermaTejeduria")))
        dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
      Next

      '******************************************************************
      'COSTO DE URDIDO
      '******************************************************************
      For Each dtrDato As DataRow In dstDatos.Tables("CostoUrdido").Rows
        dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
        dtrFicha("var_CodigoFicha") = ""
        dtrFicha("chr_CodigoEtapa") = "002"
        dtrFicha("var_CodigoArticuloLargo") = ""
        dtrFicha("var_TipoDato") = "URDIDO"
        dtrFicha("int_Secuencia") = 0
        dtrFicha("var_Dato1") = dtrArticulo("num_Velocidad1")
        dtrFicha("var_Dato2") = dtrArticulo("num_Velocidad2")
        dtrFicha("var_Dato3") = dtrArticulo("num_Velocidad3")
        dtrFicha("var_Dato4") = dtrArticulo("num_Velocidad4")
        dtrFicha("var_Dato5") = ""
        dtrFicha("var_Dato6") = ""
        dtrFicha("var_Dato7") = ""
        dtrFicha("var_Dato8") = ""
        dtrFicha("num_MateriaPrima") = 0
        dtrFicha("num_InsumoQuimico") = 0
        dtrFicha("num_OtroGasto") = dtrDato("num_CostoOTR")
        dtrFicha("num_FijoVariable") = dtrDato("num_CostoFV")
        dtrFicha("num_FijoNoVariable") = dtrDato("num_CostoCI")
        dtrFicha("num_ManoObraFija") = dtrDato("num_CostoMOF")
        dtrFicha("num_ManoObraVariable") = dtrDato("num_CostoMOV")
        dtrFicha("var_TipoCalculo") = dtrDato("var_TipoCalculo")
        dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
      Next

      '******************************************************************
      'COSTO DE ENGOMADO
      '******************************************************************
      For Each dtrFormulacion As DataRow In dstDatos.Tables("Formulacion").Rows
        Dim num_SolLitroReceta As Double = 0
        Dim num_ConcentracionTotal As Double = dstDatos.Tables("Receta").Compute("Sum(num_Concentracion)", "var_CodigoReceta='" & Trim(dtrFormulacion("var_CodigoReceta")) & "'")
        Dim num_CostoReceta As Double
        Dim num_EncogimientoUrdimbre As Double = dtrArticulo("num_EncogimientoUrdimbre")
        Dim num_Estiraje As Double = dtrEngomado("num_Estiraje")

        'CALCULANDO EL SOL/LITRO
        For Each dtrReceta As DataRow In dstDatos.Tables("Receta").Select("var_CodigoReceta='" & Trim(dtrFormulacion("var_CodigoReceta")) & "'")
          dtrAuxiliar = dstDatos.Tables("CostoInsumo").Rows.Find(dtrReceta("var_CodigoInsumo"))
          If dtrEngomado("var_CodigoTipo") = "ENGCRU" Then
            num_SolLitroReceta = num_SolLitroReceta + (dtrAuxiliar("num_CostoPromedio") * dtrReceta("num_PorcentajeGramosLitro"))
          ElseIf dtrEngomado("var_CodigoTipo") = "ENGTED" Then
            num_SolLitroReceta = num_SolLitroReceta + (dtrReceta("num_Concentracion") * dtrAuxiliar("num_CostoPromedio") / 1000)
          End If
        Next

        'CALCULANDO EL COSTO DE RECETA
        If dtrEngomado("var_CodigoTipo") <> "ENGCRU" Then
          If dtrFormulacion("var_CodigoFase") = 2 Then
            num_CostoReceta = (num_SolLitroReceta * dtrFormulacion("num_Dosificacion") / dtrEngomado("num_Velocidad")) / (num_Estiraje) / (1 - num_EncogimientoUrdimbre)
          Else
            num_CostoReceta = (num_SolLitroReceta * dtrFormulacion("num_Dosificacion")) / (num_Estiraje) / (1 - num_EncogimientoUrdimbre)
          End If
        Else
          dblCalcPesoUrdimbre = dblPesoUrdimbre * (dtrFormulacion("num_Pickup") / 100)
          num_CostoReceta = num_SolLitroReceta * (dblCalcPesoUrdimbre / ((1 - num_EncogimientoUrdimbre) * num_Estiraje))

        End If

        '-------------------------------------------
        'INICIO: CALCULANDO COSTO DE IQs ADICIONALES
        '-------------------------------------------
        Dim num_CostoIQ1 As Double = 0, num_CostoIQ2 As Double = 0

        If IsDBNull(dtrFormulacion("var_CodigoIQ1")) = False AndAlso dtrFormulacion("var_CodigoIQ1") <> "" Then
          dtrAuxiliar = dstDatos.Tables("CostoInsumo").Rows.Find(dtrFormulacion("var_CodigoIQ1"))

          If Trim(UCase(dtrFormulacion("var_UnidadIQ1"))) = Trim(UCase("gr/min")) Then
            num_CostoIQ1 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ1") / dtrEngomado("num_Velocidad"))) / 1000 * dtrAuxiliar("num_CostoPromedio")
          ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ1"))) = Trim(UCase("cc/min")) Then
            num_CostoIQ1 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ1") / dtrEngomado("num_Velocidad")) * 1.53 / 2) / 1000 * dtrAuxiliar("num_CostoPromedio")
          ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ1"))) = Trim(UCase("gr/mt")) Then
            num_CostoIQ1 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * dtrFormulacion("num_DosisIQ1")) / 1000 * dtrAuxiliar("num_CostoPromedio")
          End If
        End If

        If IsDBNull(dtrFormulacion("var_CodigoIQ2")) = False AndAlso dtrFormulacion("var_CodigoIQ2") <> "" Then
          dtrAuxiliar = dstDatos.Tables("CostoInsumo").Rows.Find(dtrFormulacion("var_CodigoIQ2"))

          If Trim(UCase(dtrFormulacion("var_UnidadIQ2"))) = Trim(UCase("gr/min")) Then
            num_CostoIQ2 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ2") / dtrEngomado("num_Velocidad"))) / 1000 * dtrAuxiliar("num_CostoPromedio")
          ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ2"))) = Trim(UCase("cc/min")) Then
            num_CostoIQ2 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * (dtrFormulacion("num_DosisIQ2") / dtrEngomado("num_Velocidad")) * 1.53 / 2) / 1000 * dtrAuxiliar("num_CostoPromedio")
          ElseIf Trim(UCase(dtrFormulacion("var_UnidadIQ2"))) = Trim(UCase("gr/mt")) Then
            num_CostoIQ2 = ((1 / (1 - num_EncogimientoUrdimbre) / num_Estiraje) * dtrFormulacion("num_DosisIQ2")) / 1000 * dtrAuxiliar("num_CostoPromedio")
          End If
        End If
        '-----------------------------------------
        'FINAL: CALCULANDO COSTO DE IQs ADICIONALES
        '-----------------------------------------

        '-------------------------------------------
        'AGREGANDO LOS DATOS DE IQ
        '-------------------------------------------
        dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
        dtrFicha("var_CodigoFicha") = ""
        dtrFicha("chr_CodigoEtapa") = "002"
        dtrFicha("var_CodigoArticuloLargo") = ""
        If dtrFormulacion("var_CodigoFase") = 1 Then
          dtrFicha("var_TipoDato") = "PRE-TRATAMIENTO"
        ElseIf dtrFormulacion("var_CodigoFase") = 2 Then
          dtrFicha("var_TipoDato") = "TEÑIDO"
        ElseIf dtrFormulacion("var_CodigoFase") = 3 Then
          dtrFicha("var_TipoDato") = "ENGOMADO"
        End If
        Dim strWhere As String = "var_TipoDato='" & dtrFicha("var_TipoDato") & "' " & _
        " and chr_CodigoEtapa='002' and int_Secuencia = " & dtrFormulacion("var_CodigoFase")
        Dim dblCostoRecetaEtapa As Double = 0
        If IsDBNull(dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", strWhere)) = False Then
          dblCostoRecetaEtapa = CType(dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", strWhere), Double)
        End If

        dtrFicha("int_Secuencia") = dtrFormulacion("var_CodigoFase")
        dtrFicha("var_Dato1") = dtrEngomado("num_Velocidad")
        dtrFicha("var_Dato2") = dtrEngomado("num_Estiraje")
        dtrFicha("var_Dato3") = dtrFormulacion("var_CodigoFase")
        dtrFicha("var_Dato4") = dtrEngomado("var_CodigoEngomado")
        dtrFicha("var_Dato5") = ""
        dtrFicha("var_Dato6") = ""
        dtrFicha("var_Dato7") = ""
        dtrFicha("var_Dato8") = ""
        dtrFicha("num_MateriaPrima") = 0
        dtrFicha("num_InsumoQuimico") = dblCostoRecetaEtapa + num_CostoReceta + num_CostoIQ1 + num_CostoIQ2
        dtrFicha("num_OtroGasto") = 0
        dtrFicha("num_FijoVariable") = 0
        dtrFicha("num_FijoNoVariable") = 0
        dtrFicha("num_ManoObraFija") = 0
        dtrFicha("num_ManoObraVariable") = 0
        dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
        dstFicha.Tables("FichaDetalle").AcceptChanges()
      Next
      'OBTENIENDO EL COSTO DE ENGOMADO
      dtrAuxiliar = dstDatos.Tables("CostoEngomado").Rows(0)

      'AGREGANDO LOS DATOS DE IQ
      dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
      dtrFicha("var_CodigoFicha") = ""
      dtrFicha("chr_CodigoEtapa") = "002"
      dtrFicha("var_CodigoArticuloLargo") = ""
      dtrFicha("var_TipoDato") = ""
      dtrFicha("int_Secuencia") = 4
      dtrFicha("var_Dato1") = dtrEngomado("num_Velocidad")
      dtrFicha("var_Dato2") = dtrEngomado("num_Estiraje")
      dtrFicha("var_Dato3") = 9
      dtrFicha("var_Dato4") = dtrEngomado("var_CodigoEngomado")
      dtrFicha("var_Dato5") = ""
      dtrFicha("var_Dato6") = ""
      dtrFicha("var_Dato7") = ""
      dtrFicha("var_Dato8") = ""
      dtrFicha("num_MateriaPrima") = 0
      dtrFicha("num_InsumoQuimico") = 0
      dtrFicha("num_OtroGasto") = dtrAuxiliar("num_CostoOTR")
      dtrFicha("num_FijoVariable") = dtrAuxiliar("num_CostoFV")
      dtrFicha("num_FijoNoVariable") = dtrAuxiliar("num_CostoCI")
      dtrFicha("num_ManoObraFija") = dtrAuxiliar("num_CostoMOF")
      dtrFicha("num_ManoObraVariable") = dtrAuxiliar("num_CostoMOV")
      dtrFicha("var_TipoCalculo") = dtrAuxiliar("var_TipoCalculo")
      dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)

      '--------------------------------------------------------------------------
      ' COSTO DE TELARES
      ' Modificado: Guardar tipo de calculo de telares (E: Estandar, P: Promedio)
      ' Mayo 2013
      ' Alexander Torres Cardenas
      '--------------------------------------------------------------------------
      For Each dtrDato As DataRow In dstDatos.Tables("CostoTelar").Rows
        dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
        dtrFicha("var_CodigoFicha") = ""
        dtrFicha("chr_CodigoEtapa") = "003"
        dtrFicha("var_CodigoArticuloLargo") = ""
        dtrFicha("var_TipoDato") = ""
        dtrFicha("int_Secuencia") = 0
        dtrFicha("var_Dato1") = dtrArticulo("var_TipoMaquina")
        dtrFicha("var_Dato2") = dtrArticulo("int_NumeroTelas")
        dtrFicha("var_Dato3") = dtrArticulo("num_VelocidadTeorica")
        dtrFicha("var_Dato4") = dtrArticulo("int_AnchoCrudo")
        dtrFicha("var_Dato5") = dtrArticulo("num_AnchoPeine")
        dtrFicha("var_Dato6") = dtrArticulo("num_EficienciaReal")
        dtrFicha("var_Dato7") = dtrArticulo("var_Ligamento")
        dtrFicha("var_Dato8") = dstDatos.Tables("Urdimbre").Compute("Sum(num_GramosMetro)", "") + dstDatos.Tables("Trama").Compute("Sum(num_GramosMetro)", "")
        dtrFicha("num_MateriaPrima") = 0
        dtrFicha("num_InsumoQuimico") = 0
        dtrFicha("num_OtroGasto") = dtrDato("num_CostoOTR")
        dtrFicha("num_FijoVariable") = dtrDato("num_CostoFV")
        dtrFicha("num_FijoNoVariable") = dtrDato("num_CostoCI")
        dtrFicha("num_ManoObraFija") = dtrDato("num_CostoMOF")
        dtrFicha("num_ManoObraVariable") = dtrDato("num_CostoMOV")
        dtrFicha("var_TipoCalculo") = dtrDato("var_TipoCalculo")
        dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)
      Next

      '******************************************************************
      'COSTO TINTORERIA
      '******************************************************************
      Dim num_FactorTintoreria As Double = (100.0 - dtrArticulo("num_VariacionDimensional")) / 100.0
      For Each dtrRuta As DataRow In dstDatos.Tables("RutaTintoreria").Rows
        Dim Factor As Double = 1
        If dtrRuta("var_CodigoMaquina") <> "000782" Then
          Factor = num_FactorTintoreria
        End If
        dtrAuxiliar = dstDatos.Tables("CostoTinto").Rows.Find(dtrRuta("var_CodigoMaquina"))
        Dim num_CostoReceta As Double = 0
        If IsDBNull(dtrRuta("var_CodigoReceta")) = False AndAlso dtrRuta("var_CodigoReceta") <> "" Then
          Dim num_Pickup As Double = 0, num_Peso As Double = 0

          For Each dtrPickup As DataRow In dstDatos.Tables("PickupTintoreria").Select("int_Secuencia=" & dtrRuta("int_Secuencia"), "")
            num_Pickup = dtrPickup("num_Pickup")
            num_Peso = dtrPickup("num_Peso")
            Dim CostoBruto As Double = 0
            For Each dtrInsumo As DataRow In dstDatos.Tables("RecetaInsumoTinto").Select("var_CodigoReceta='" & dtrPickup("var_CodigoReceta") & "'")
              Dim dtrCostoIQ = dstDatos.Tables("CostoInsumo").Rows.Find(dtrInsumo("var_CodigoInsumo"))
              CostoBruto = CostoBruto + (dtrCostoIQ("num_Factor") * dtrCostoIQ("num_CostoPromedio") * dtrInsumo("num_Concentracion") / 1000.0)
            Next
            num_costoreceta = num_costoreceta + (CostoBruto * num_Pickup / 100) * num_Peso * dtrRuta("int_Pases")
          Next
        End If

        dtrFicha = dstFicha.Tables("FichaDetalle").NewRow
        dtrFicha("var_CodigoFicha") = ""
        dtrFicha("chr_CodigoEtapa") = "004"
        dtrFicha("var_CodigoArticuloLargo") = ""
        dtrFicha("var_TipoDato") = dtrRuta("var_DescripcionEtapa")
        dtrFicha("int_Secuencia") = dtrRuta("int_Secuencia")
        dtrFicha("var_Dato1") = dtrRuta("int_Secuencia")
        If IsDBNull(dtrRuta("var_CodigoReceta")) = False Then
          dtrFicha("var_Dato2") = dtrRuta("var_CodigoReceta")
        Else
          dtrFicha("var_Dato2") = ""
        End If
        dtrFicha("var_Dato3") = dtrArticulo("num_VariacionDimensional")
        dtrFicha("var_Dato4") = dtrRuta("var_CodigoMaquina")
        dtrFicha("var_Dato5") = dtrRuta("var_CodigoOperacion")
        dtrFicha("var_Dato6") = dtrRuta("var_DescripcionOperacion")
        dtrFicha("var_Dato7") = dtrRuta("num_VelocidadMaquina")
        dtrFicha("var_Dato8") = dtrRuta("int_Pases")
        dtrFicha("num_MateriaPrima") = 0

                If dtrRuta("var_CodigoMaquina") = "000071" OrElse dtrRuta("var_CodigoMaquina") = "006801" Then
                    num_CostoReceta = _dblCostoEstampado
                End If

        dtrFicha("num_InsumoQuimico") = num_CostoReceta

        If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
          dtrFicha("num_OtroGasto") = (dtrAuxiliar("num_CostoOTR") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
        Else
          dtrFicha("num_OtroGasto") = 0
        End If

        If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
          dtrFicha("num_FijoVariable") = (dtrAuxiliar("num_CostoFV") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
        Else
          dtrFicha("num_FijoVariable") = 0
        End If

        If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
          dtrFicha("num_FijoNoVariable") = (dtrAuxiliar("num_CostoCI") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
        Else
          dtrFicha("num_FijoNoVariable") = 0
        End If

        If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
          dtrFicha("num_ManoObraFija") = (dtrAuxiliar("num_CostoMOF") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
        Else
          dtrFicha("num_ManoObraFija") = 0
        End If

        If IsDBNull(dtrRuta("num_VelocidadMaquina")) = False AndAlso dtrRuta("num_VelocidadMaquina") > 0 Then
          dtrFicha("num_ManoObraVariable") = (dtrAuxiliar("num_CostoMOV") / dtrRuta("num_VelocidadMaquina") / 60) * dtrRuta("int_Pases") / Factor
        Else
          dtrFicha("num_ManoObraVariable") = 0
        End If
        dstFicha.Tables("FichaDetalle").LoadDataRow(dtrFicha.ItemArray, True)

      Next
      dstFicha.Tables("FichaDetalle").AcceptChanges()

      Dim dtrDatos As DataRow

      dtrDatos = dstFicha.Tables("FichaResumen").NewRow
      dtrDatos("var_CodigoArticuloLargo") = ""
      dtrDatos("var_CodigoFicha") = ""
      dtrDatos("chr_CodigoEtapa") = "001"
      dtrDatos("var_NombreResumen") = "COSTO HILO"
      dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='001'") / num_FactorTintoreria
      dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
      dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)


      dtrDatos = dstFicha.Tables("FichaResumen").NewRow
      dtrDatos("var_CodigoArticuloLargo") = ""
      dtrDatos("var_CodigoFicha") = ""
      dtrDatos("chr_CodigoEtapa") = "002"
      dtrDatos("var_NombreResumen") = "COSTO PRETEJIDO"
      dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='002'") / num_FactorTintoreria
      dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
      dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)


      dtrDatos = dstFicha.Tables("FichaResumen").NewRow
      dtrDatos("var_CodigoArticuloLargo") = ""
      dtrDatos("var_CodigoFicha") = ""
      dtrDatos("chr_CodigoEtapa") = "003"
      dtrDatos("var_NombreResumen") = "COSTO TELARES"
      dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='003'") / num_FactorTintoreria
      dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
      dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)

      dtrDatos = dstFicha.Tables("FichaResumen").NewRow
      dtrDatos("var_CodigoArticuloLargo") = ""
      dtrDatos("var_CodigoFicha") = ""
      dtrDatos("chr_CodigoEtapa") = "004"
      dtrDatos("var_NombreResumen") = "COSTO TINTORERIA"
      dtrDatos("num_MateriaPrima") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_MateriaPrima)", "chr_CodigoEtapa='004'")
      dtrDatos("num_InsumoQuimico") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_InsumoQuimico)", "chr_CodigoEtapa='004'")
      dtrDatos("num_OtroGasto") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_OtroGasto)", "chr_CodigoEtapa='004'")
      dtrDatos("num_FijoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoVariable)", "chr_CodigoEtapa='004'")
      dtrDatos("num_FijoNoVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_FijoNoVariable)", "chr_CodigoEtapa='004'")
      dtrDatos("num_ManoObraVariable") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraVariable)", "chr_CodigoEtapa='004'")
      dtrDatos("num_ManoObraFija") = dstFicha.Tables("FichaDetalle").Compute("Sum(num_ManoObraFija)", "chr_CodigoEtapa='004'")
      dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
      dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)


      For Each dtrRevFin As DataRow In dstDatos.Tables("CostoRevFin").Rows
        dtrDatos = dstFicha.Tables("FichaResumen").NewRow
        dtrDatos("var_CodigoArticuloLargo") = ""
        dtrDatos("var_CodigoFicha") = ""
        dtrDatos("chr_CodigoEtapa") = "005"
        dtrDatos("var_NombreResumen") = "COSTO REVISION FINAL"
        dtrDatos("num_MateriaPrima") = 0
        dtrDatos("num_InsumoQuimico") = 0
        dtrDatos("num_OtroGasto") = dtrRevFin("num_CostoOTR")
        dtrDatos("num_FijoVariable") = dtrRevFin("num_CostoFV")
        dtrDatos("num_FijoNoVariable") = dtrRevFin("num_CostoCI")
        dtrDatos("num_ManoObraVariable") = dtrRevFin("num_CostoMOV")
        dtrDatos("num_ManoObraFija") = dtrRevFin("num_CostoMOF")
        dtrDatos("num_Porcentaje") = dtrArticulo("num_GastoOperativo")
        dstFicha.Tables("FichaResumen").LoadDataRow(dtrDatos.ItemArray, True)
      Next
      dstFicha.Tables("FichaResumen").AcceptChanges()

      Dim dtrAdicional As DataRow = dstFicha.Tables("FichaAdicional").NewRow
      dtrAdicional("ANCHO_ESTANDAR") = dtrArticulo("num_AnchoEstandar")
      dtrAdicional("ONZAS") = dtrArticulo("num_PesoOnzas")
      dstFicha.Tables("FichaAdicional").LoadDataRow(dtrAdicional.ItemArray, True)
      dstFicha.Tables("FichaAdicional").AcceptChanges()

      Return dstFicha
    End Function

    Public Function GrabarFicha(ByVal strXMLCabecera As String, ByVal strXMLDetalle As String, ByVal strXMLResumen As String) As DataTable
      Try
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As Object = {"var_XMLCabecera", strXMLCabecera, "var_XMLDetalle", strXMLDetalle, "var_XMLResumen", strXMLResumen, "var_Usuario", Me._strUsuario}
        Dim dtbRetorno As DataTable = _objConexion.ObtenerDataTable("usp_FCO_FichaSimulada_Grabar_3", objParametros)
        Return dtbRetorno
      Catch ex As Exception
        Throw ex
      End Try

        End Function
        Public Function GrabarFicha_V2(ByVal strXMLCabecera As String, ByVal strXMLDetalle As String, ByVal strXMLResumen As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim objParametros() As Object = {"var_XMLCabecera", strXMLCabecera, "var_XMLDetalle", strXMLDetalle, "var_XMLResumen", strXMLResumen, "var_Usuario", Me._strUsuario}
                Dim dtbRetorno As DataTable = _objConexion.ObtenerDataTable("usp_FCO_FichaSimulada_Grabar_3_V2", objParametros)
                Return dtbRetorno
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ListarFichas_V2(ByVal strCodigoArticulo As String, Optional ByVal strTipoFicha As String = "S") As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_TipoFicha", strTipoFicha}
                Return _objConexion.ObtenerDataTable("usp_FCO_FichaSimmulada_Listar_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ListarFichas(ByVal strCodigoArticulo As String, Optional ByVal strTipoFicha As String = "S") As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_TipoFicha", strTipoFicha}
                Return _objConexion.ObtenerDataTable("usp_FCO_FichaSimmulada_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    Public Function FichaSimuladaEliminar(ByVal strCodigoArticulo As String, ByVal strCodigoFicha As String) As Boolean
      Try
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_CodigoFicha", strCodigoFicha}
        _objConexion.EjecutarComando("usp_FCO_FichaSimmulada_Eliminar", objParametros)
        Return True
      Catch ex As Exception
        Throw ex
      End Try
        End Function
        'CAMBIO DG - INI
        Public Function FichaSimuladaEliminar_V2(ByVal strCodigoArticulo As String, ByVal strCodigoFicha As String) As Boolean
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_CodigoFicha", strCodigoFicha}
                _objConexion.EjecutarComando("usp_FCO_FichaSimmulada_Eliminar_V2", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO DG - FIN
    Public Function ListarRequisicionPrecostos(ByVal strNumeroRequisicion As String, ByVal strArticuloCrudo As String) As DataTable
      Try
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() As Object = {"var_NumeroRequisicion", strNumeroRequisicion, "var_ArticuloCrudo", strArticuloCrudo}
        Return _objConexion.ObtenerDataTable("USP_COS_REQUISICIONPRECOSTOS_LISTAR", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function
#End Region

  End Class

End Namespace

