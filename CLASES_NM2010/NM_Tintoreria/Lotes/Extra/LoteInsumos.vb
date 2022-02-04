Imports NM.AccesoDatos

Namespace Lotes.Extra

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

#End Region
#Region "CONSTRUCTORES"
    Sub New(ByVal strCodigoLote As String)
      mdtbInsumos = InsumosLote_Obtener(strCodigoLote)
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
      Return dtbInsumosPartida
    End Function
#End Region

    Public Function InsumosLote_Obtener(ByVal strCodigoLote As String) As DataTable
      Dim objConexion As AccesoDatosSQLServer
      Dim objParametros() As String = {"var_CodigoLote", strCodigoLote}
      mdtbInsumos = EsquemaInsumosLote()
      Try
        objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim dtbDatos As DataTable = objConexion.ObtenerDataTable("usp_TIN_LoteInsumosAdicional_Obtener", objParametros)
        For Each dtrItem As DataRow In dtbDatos.Rows
          mstrCodigoReceta = dtrItem("var_CodigoRecetaCompuesta")
          _strNombreReceta = dtrItem("var_NombreReceta")
          Dim dtrNuevo As DataRow = mdtbInsumos.NewRow
          dtrNuevo("CODIGO_RECETA") = dtrItem("var_CodigoReceta")
          dtrNuevo("CODIGO_INSUMO") = dtrItem("var_CodigoInsumo")
          dtrNuevo("CODIGO_RECETA_COMPUESTA") = dtrItem("var_CodigoRecetaCompuesta")
          dtrNuevo("NOMBRE_INSUMO") = dtrItem("var_NombreInsumo")
          dtrNuevo("CONCENTRACION") = dtrItem("num_Concentracion")
          dtrNuevo("CONCENTRACION_REAL") = dtrItem("num_Concentracion_real")
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
        Dim dtsDatos As DataSet = objConexion.ObtenerDataSet("usp_TIN_LoteInsumoPartidasAdicional_Obtener", objParametros)
        Dim dtbCalculo As DataTable = dtsDatos.Tables(0)
        Dim dtbPartidas As DataTable = dtsDatos.Tables(1)
        If dtbCalculo.Rows.Count > 0 Then
          mdblVolumenPartida = dtbCalculo.Rows(0)("VOLUMEN_PARTIDA")
          mintNumeroPartidas = dtbCalculo.Rows(0)("TOTAL_PARTIDA")
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
          dtrNuevo("CONSUMO") = dtrItem("CONSUMO_REAL")
          mdtbInsumosPartidas.LoadDataRow(dtrNuevo.ItemArray, True)
        Next
        mdtbInsumosPartidas.AcceptChanges()
      Catch ex As Exception
        Throw ex
      End Try
    End Sub
  End Class

End Namespace
