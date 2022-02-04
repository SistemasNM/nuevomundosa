Imports NM.AccesoDatos

Namespace FichaCosto.Mantenimiento

  Public Class CostoEstampado

#Region "Variables"

    Private _objConexion As AccesoDatosSQLServer
    Private _strUsuario As String
    Private _intAnio As Int16
    Private _intMes As Int16
    Private _strAriculo As String
    Private _dblCostoTotal As Double
    Private _strEstado As String
    Private _intVersion As Int16
#End Region

#Region "Propiedades"
    Public Property Anio() As Int16
      Get
        Return _intAnio
      End Get
      Set(ByVal Value As Int16)
        _intAnio = Value
      End Set
    End Property
    Public Property Mes() As Int16
      Get
        Return _intMes
      End Get
      Set(ByVal Value As Int16)
        _intMes = Value
      End Set
    End Property
    Public Property Articulo() As String
      Get
        Return _strAriculo
      End Get
      Set(ByVal Value As String)
        _strAriculo = Value
      End Set
    End Property
    Public Property CostoTotal() As Double
      Get
        Return _dblCostoTotal
      End Get
      Set(ByVal Value As Double)
        _dblCostoTotal = Value
      End Set
    End Property
    Public Property Estado() As String
      Get
        Return _strEstado
      End Get
      Set(ByVal Value As String)
        _strEstado = Value
      End Set
    End Property

    Public Property Version() As Int16
      Get
        Return _intVersion
      End Get
      Set(ByVal Value As Int16)
        _intVersion = Value
      End Set
    End Property
    Public Property Usuario() As String
      Get
        Return _strUsuario
      End Get
      Set(ByVal Value As String)
        _strUsuario = Value
      End Set
    End Property
#End Region

    Public Function GrabaCostoEstampado()
      Try
        Dim objParametros() As Object = {"int_Anio", _intAnio, "int_Mes", _intMes, _
        "var_CodArticulo", _strAriculo, "num_CostoTotal", _dblCostoTotal, "var_Usuario", _strUsuario}
        _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        _objConexion.EjecutarComando("usp_COS_CostoEstampado_GrabarVersion", objParametros)
      Catch Ex As Exception
        Throw Ex
      End Try
    End Function

    Public Function ListarCostoEstampado(ByVal intAnio As Integer, ByVal intMes As Integer) As DataTable
      Try

        Dim objParametros() As Object = {"int_Anio", intAnio, "int_Mes", intMes}
        _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Return _objConexion.ObtenerDataTable("utb_CostosEstampado_Listar", objParametros)

      Catch Ex As Exception
        Throw Ex
      End Try
    End Function

  End Class
End Namespace
