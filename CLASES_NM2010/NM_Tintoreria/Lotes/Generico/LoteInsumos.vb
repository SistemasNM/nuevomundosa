Imports NM.AccesoDatos

Namespace Lotes.Generico

  Public Class LoteInsumos

#Region "VARIABLES"
    Private mstrCodigoReceta As String
    Private _strNombreReceta As String
    Private mdblVolumenTotal As Double
    Private mdblVolumenPartida As Double
    Private mintNumeroPartidas As Int16
    Private mdtbInsumos As DataTable
    Private mdtbInsumosPartidas As DataTable
    Private mdtbCilindros As DataTable
    Private mstrUsuario As String
        'REQSIS201700031 - DG - INI
        Private mdblVolStockIni As Double
        Private mdblVolRequerido As Double
        Private mdblVolDespachado As Double
        Private mdblVolStockFin As Double
        Private mdblVolConsumo As Double
        Private mdblVolDevuleto As Double
        'REQSIS201700031 - DG - FIN
#End Region
#Region "PROPIEDADES"
    Public Property Insumos() As DataTable
      Get
        Return mdtbInsumos
      End Get
      Set(ByVal Value As DataTable)
        mdtbInsumos = Value
      End Set
    End Property
    Public Property InsumosPartidas() As DataTable
      Get
        Return mdtbInsumosPartidas
      End Get
      Set(ByVal Value As DataTable)
        mdtbInsumosPartidas = Value
      End Set
    End Property
    Public Property Usuario() As String
      Get
        Return mstrUsuario
      End Get
      Set(ByVal Value As String)
        mstrUsuario = Value
      End Set
    End Property
    Public Property NombreReceta() As String
      Get
        Return _strNombreReceta
      End Get
      Set(ByVal Value As String)
        _strNombreReceta = Value
      End Set
    End Property
    Public Property CodigoReceta() As String
      Get
        Return mstrCodigoReceta
      End Get
      Set(ByVal Value As String)
        mstrCodigoReceta = Value
      End Set
    End Property
    Public Property Cilindros() As DataTable
      Get
        Return mdtbCilindros
      End Get
      Set(ByVal Value As DataTable)
        mdtbCilindros = Value
      End Set
    End Property
    Public Property VolumenTotal() As Double
      Get
        If IsDBNull(mdblVolumenTotal) = True Then
          Return 0
        Else
          Return mdblVolumenTotal
        End If
      End Get
      Set(ByVal Value As Double)
        mdblVolumenTotal = Value
      End Set
    End Property
    Public Property VolumenPartida() As Double
      Get
        Return mdblVolumenPartida
      End Get
      Set(ByVal Value As Double)
        mdblVolumenPartida = Value
      End Set
    End Property
    Public Property NumeroPartidas() As Int16
      Get
        Return mintNumeroPartidas
      End Get
      Set(ByVal Value As Int16)
        mintNumeroPartidas = Value
      End Set
    End Property
        'REQSIS201700031 - DG - INI
        Public Property VolStockIni() As Double
            Get
                Return mdblVolStockIni
            End Get
            Set(ByVal Value As Double)
                mdblVolStockIni = Value
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
                Return mdblVolConsumo
            End Get
            Set(ByVal Value As Double)
                mdblVolConsumo = Value
            End Set
        End Property
        Public Property VolDevuleto() As Double
            Get
                Return mdblVolDevuleto
            End Get
            Set(ByVal Value As Double)
                mdblVolDevuleto = Value
            End Set
        End Property
        'REQSIS201700031 - DG - FIN
#End Region
#Region "CONSTRUCTORES"
    Sub New(ByVal strCodigoLote As String)
      mdtbInsumos = InsumosLote_Obtener(strCodigoLote, "")
      mdtbInsumosPartidas = EsquemaInsumosPartida()
      InsumosLotePartidas_Cargar(strCodigoLote)
    End Sub

    Sub New()
      mdtbInsumos = EsquemaInsumosLote()
      mdtbInsumosPartidas = EsquemaInsumosPartida()
    End Sub

#End Region
#Region "ESQUEMAS"
    Private Function EsquemaInsumosLote() As DataTable
      Dim ldtInsumos As DataTable
      ldtInsumos = New DataTable("Insumos")
      ldtInsumos.Columns.Add("CODIGO_FICHA", GetType(String))
      ldtInsumos.Columns.Add("CODIGO_INSUMO", GetType(String))
      ldtInsumos.Columns.Add("CODIGO_RECETA", GetType(String))
      ldtInsumos.Columns.Add("CODIGO_RECETA_COMPUESTA", GetType(String))
      ldtInsumos.Columns.Add("NOMBRE_INSUMO", GetType(String))
      ldtInsumos.Columns.Add("CONCENTRACION", GetType(Double))
      ldtInsumos.Columns.Add("CONSUMO", GetType(Double))
      ldtInsumos.Columns.Add("CONCENTRACION_REAL", GetType(Double))
      ldtInsumos.Columns.Add("CONSUMO_REAL", GetType(Double))
      ldtInsumos.Columns.Add("TIPO", GetType(Int16))
      ldtInsumos.Columns.Add("ESTADO_INSUMO", GetType(Int16))
      ldtInsumos.Columns.Add("ORDEN", GetType(Int16))
      Return ldtInsumos
    End Function
    Private Function EsquemaInsumosPartida() As DataTable
      Dim dtbInsumosPartida As New DataTable
      dtbInsumosPartida.Columns.Add("CODIGO_RECETA", GetType(String))
      dtbInsumosPartida.Columns.Add("CODIGO_INSUMO", GetType(String))
      dtbInsumosPartida.Columns.Add("PARTIDA", GetType(String))
      dtbInsumosPartida.Columns.Add("VOLUMEN_PARTIDA", GetType(Double))
      dtbInsumosPartida.Columns.Add("ESTADO_LOTE", GetType(Int16))
      dtbInsumosPartida.Columns.Add("ESTADO_PARTIDA", GetType(Int16))
      dtbInsumosPartida.Columns.Add("CONSUMO", GetType(String))
            dtbInsumosPartida.Columns.Add("CONSUMO_REAL", GetType(String))
            'REQSIS201700031 - DG - INI
            dtbInsumosPartida.Columns.Add("CANTIDAD_DESPACHADA", GetType(Double))
            dtbInsumosPartida.Columns.Add("CANTIDAD_CONSUMIDA", GetType(Double))
            'REQSIS201700031 - DG - FIN
      Return dtbInsumosPartida
    End Function
#End Region

    Public Function InsumosLote_Obtener(ByVal strCodigoLote As String, ByVal strCodigoReceta As String) As DataTable
      Dim objConexion As AccesoDatosSQLServer
      Dim objParametros() As String = {"var_CodigoReceta", strCodigoReceta, "var_CodigoLote", strCodigoLote}
      mdtbInsumos = EsquemaInsumosLote()
      Try
        objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim dtbDatos As DataTable = objConexion.ObtenerDataTable("usp_TIN_LoteInsumos_Obtener", objParametros)
        For Each dtrItem As DataRow In dtbDatos.Rows
          mstrCodigoReceta = dtrItem("var_CodigoRecetaCompuesta")
          _strNombreReceta = dtrItem("var_NombreReceta")
          Dim dtrNuevo As DataRow = mdtbInsumos.NewRow
          dtrNuevo("CODIGO_RECETA") = dtrItem("var_CodigoReceta")
          dtrNuevo("CODIGO_INSUMO") = dtrItem("var_CodigoInsumo")
          dtrNuevo("CODIGO_RECETA_COMPUESTA") = dtrItem("var_CodigoRecetaCompuesta")
          dtrNuevo("NOMBRE_INSUMO") = dtrItem("var_NombreInsumo")
          dtrNuevo("CONCENTRACION") = dtrItem("num_Concentracion")
          dtrNuevo("CONCENTRACION_REAL") = dtrItem("num_Concentracion_REAL")
          dtrNuevo("TIPO") = dtrItem("int_Tipo")
          dtrNuevo("ESTADO_INSUMO") = dtrItem("int_Estado")
          dtrNuevo("ORDEN") = dtrItem("Orden")
          mdtbInsumos.LoadDataRow(dtrNuevo.ItemArray, True)
        Next
        mdtbInsumos.AcceptChanges()
        Return mdtbInsumos
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Sub InsumosLotePartidas_Cargar(ByVal strCodigoLote As String)
      Dim objConexion As AccesoDatosSQLServer
      Dim objParametros() As String = {"var_CodigoLote", strCodigoLote}
      mdtbInsumosPartidas = EsquemaInsumosPartida()
      Try
        objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim dtsDatos As DataSet = objConexion.ObtenerDataSet("usp_TIN_LoteInsumoPartidas_Obtener", objParametros)
        Dim dtbCalculo As DataTable = dtsDatos.Tables(0)
        Dim dtbPartidas As DataTable = dtsDatos.Tables(1)
        If dtbCalculo.Rows.Count > 0 Then
          mdblVolumenPartida = dtbCalculo.Rows(0)("VOLUMEN_PARTIDA")
          mdblVolumenTotal = dtbCalculo.Rows(0)("VOLUMEN_TOTAL")
                    mintNumeroPartidas = dtbCalculo.Rows(0)("TOTAL_PARTIDA")
                    'REQSIS201700031 - DG - INI
                    mdblVolStockIni = dtbCalculo.Rows(0)("VOL_STOCK_INI")
                    mdblVolRequerido = dtbCalculo.Rows(0)("VOL_REQUERIDO")
                    mdblVolDespachado = dtbCalculo.Rows(0)("VOL_DESPACHADO")
                    mdblVolStockFin = dtbCalculo.Rows(0)("VOL_STOCK_FIN")
                    mdblVolConsumo = dtbCalculo.Rows(0)("VOL_CONSUMIDO")
                    mdblVolDevuleto = dtbCalculo.Rows(0)("VOL_DEVUELTO")
                    'REQSIS201700031 - DG - FIN
        End If

        For Each dtrItem As DataRow In dtbPartidas.Rows
          Dim dtrNuevo As DataRow = mdtbInsumosPartidas.NewRow
          dtrNuevo("CODIGO_RECETA") = dtrItem("CODIGO_RECETA")
          dtrNuevo("CODIGO_INSUMO") = dtrItem("CODIGO_INSUMO_QUIMICO")
          dtrNuevo("PARTIDA") = dtrItem("PARTIDA")
          dtrNuevo("VOLUMEN_PARTIDA") = dtrItem("VOLUMEN_PARTIDA")
          dtrNuevo("ESTADO_PARTIDA") = dtrItem("ESTADO_PARTIDA")
          dtrNuevo("ESTADO_LOTE") = dtrItem("ESTADO_LOTE")
          dtrNuevo("CONSUMO") = dtrItem("CONSUMO")
          dtrNuevo("CONSUMO_REAL") = dtrItem("CONSUMO_REAL")
                    'REQSIS201700031 - DG - INI
                    dtrNuevo("CANTIDAD_DESPACHADA") = dtrItem("CANTIDAD_DESPACHADA")
                    dtrNuevo("CANTIDAD_CONSUMIDA") = dtrItem("CANTIDAD_CONSUMIDA")
                    'REQSIS201700031 - DG - FIN
                    mdtbInsumosPartidas.LoadDataRow(dtrNuevo.ItemArray, True)
        Next
        mdtbInsumosPartidas.AcceptChanges()
      Catch ex As Exception
        Throw ex
      End Try
    End Sub
  End Class

End Namespace
