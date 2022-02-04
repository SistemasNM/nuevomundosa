Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

  Public Class NM_DatosArticuloProduccion

#Region "-- Variables --"
    Private mobj_conexionproduccion As AccesoDatosSQLServer
    Private mstr_fechaproduccion As String 'YYYYMMDD
    Private mint_id As Integer
    Private mstr_telar As String
    Private mstr_articulocrudo As String
    Private mnum_anchocrudo As Double
    Private mnum_hilospulg As Double
    Private mnum_pasadaspulg As Double
    Private mnum_grmt2 As Double
    Private mnum_grml As Double
    Private mnum_ligamento As Double
    Private mstr_ligamento As String
    Private mnum_encogurdimbre As Double
    Private mnum_encogtrama As Double
    Private mstr_observacion As String
    Private mstr_usuario As String
#End Region

#Region "-- Propiedades --"
    Public Property fechaproduccion() As String
      Get
        Return mstr_fechaproduccion
      End Get
      Set(ByVal Value As String)
        mstr_fechaproduccion = Value
      End Set
    End Property

    Public Property id() As Integer
      Get
        Return mint_id
      End Get
      Set(ByVal Value As Integer)
        mint_id = Value
      End Set
    End Property

    Public Property telar() As String
      Get
        Return mstr_telar
      End Get
      Set(ByVal Value As String)
        mstr_telar = Value
      End Set
    End Property

    Public Property articulocrudo() As String
      Get
        Return mstr_articulocrudo
      End Get
      Set(ByVal Value As String)
        mstr_articulocrudo = Value
      End Set
    End Property

    Public Property anchocrudo() As Double
      Get
        Return mnum_anchocrudo
      End Get
      Set(ByVal Value As Double)
        mnum_anchocrudo = Value
      End Set
    End Property

    Public Property hilospulg() As Double
      Get
        Return mnum_hilospulg
      End Get
      Set(ByVal Value As Double)
        mnum_hilospulg = Value
      End Set
    End Property

    Public Property pasadaspulg() As Double
      Get
        Return mnum_pasadaspulg
      End Get
      Set(ByVal Value As Double)
        mnum_pasadaspulg = Value
      End Set
    End Property

    Public Property grmt2() As Double
      Get
        Return mnum_grmt2
      End Get
      Set(ByVal Value As Double)
        mnum_grmt2 = Value
      End Set
    End Property

    Public Property grml() As Double
      Get
        Return mnum_grml
      End Get
      Set(ByVal Value As Double)
        mnum_grml = Value
      End Set
    End Property

    Public Property ligamento() As Double
      Get
        Return mnum_ligamento
      End Get
      Set(ByVal Value As Double)
        mnum_ligamento = Value
      End Set
    End Property

    Public Property ligamento_desc() As String
      Get
        Return mstr_ligamento
      End Get
      Set(ByVal strValue As String)
        mstr_ligamento = strValue
      End Set
    End Property

    Public Property encogurdimbre() As Double
      Get
        Return mnum_encogurdimbre
      End Get
      Set(ByVal Value As Double)
        mnum_encogurdimbre = Value
      End Set
    End Property

    Public Property encogtrama() As Double
      Get
        Return mnum_encogtrama
      End Get
      Set(ByVal Value As Double)
        mnum_encogtrama = Value
      End Set
    End Property

    Public Property observacion() As String
      Get
        Return mstr_observacion
      End Get
      Set(ByVal Value As String)
        mstr_observacion = Value
      End Set
    End Property

    Public Property usuario() As String
      Get
        Return mstr_usuario
      End Get
      Set(ByVal Value As String)
        mstr_usuario = Value
      End Set
    End Property
#End Region

#Region "-- Eventos --"
    Sub New()
      mobj_conexionproduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    End Sub
#End Region

#Region "-- Metodos --"

    Public Function fnc_consultar(ByVal pint_tipoconsulta As Int16) As DataTable
      Try
        Dim ldtbretornar As DataTable
        Dim lobjparametros() As Object = {"ptin_tipoconsulta", pint_tipoconsulta, _
        "pvch_fechaproduccion", IIf(mstr_fechaproduccion Is Nothing, "", mstr_fechaproduccion), _
        "pvch_telar", IIf(mstr_telar Is Nothing, "", mstr_telar), _
        "pvch_articulocrudo", IIf(mstr_articulocrudo Is Nothing, "", mstr_articulocrudo)}
        ldtbretornar = mobj_conexionproduccion.ObtenerDataTable("usp_tej_datosartproduccion_consultar", lobjparametros)
        Return ldtbretornar
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function fnc_guardar(ByVal pint_accion As Int16) As DataTable
      Try
        Dim ldtbretornar As DataTable
        Dim lobjparametros() As Object = {"ptin_accion", pint_accion, _
        "pint_id", mint_id, _
        "pvch_fechaproduccion", mstr_fechaproduccion, _
        "pvch_telar", mstr_telar, _
        "pvch_articulocrudo", mstr_articulocrudo, _
        "pnum_anchocrudo", mnum_anchocrudo, _
        "pnum_hilospulg", mnum_hilospulg, _
        "pnum_pasadaspulg", mnum_pasadaspulg, _
        "pnum_grmt2", mnum_grmt2, _
        "pnum_grml", mnum_grml, _
        "pnum_ligamento", mnum_ligamento, _
        "pvch_ligamento", mstr_ligamento, _
        "pnum_encogurdimbre", mnum_encogurdimbre, _
        "pnum_encogtrama", mnum_encogtrama, _
        "pvch_observacion", mstr_observacion, _
        "pvch_usuario", mstr_usuario}
        ldtbretornar = mobj_conexionproduccion.ObtenerDataTable("usp_tej_datosartproduccion_guardar", lobjparametros)
        Return ldtbretornar
      Catch ex As Exception
        Throw ex
      End Try
    End Function
#End Region

  End Class

End Namespace