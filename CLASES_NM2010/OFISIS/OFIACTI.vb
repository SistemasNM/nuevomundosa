Imports NuevoMundo.Generales
Imports NM.AccesoDatos

Public Class OFIACTI

#Region "-- Variables --"

  Private mstrCodigoActivo As String
  Private mstrDescripcActivo As String
  Private mobjConexionActivoFijo As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ActivoFijoOfisis)
#End Region

#Region "-- Propiedades --"

  Public Property CodigoActivo() As String
    Get
      CodigoActivo = mstrCodigoActivo
    End Get
    Set(ByVal Value As String)
      mstrCodigoActivo = Value
    End Set
  End Property

  Public Property DescripcActivo() As String
    Get
      DescripcActivo = mstrDescripcActivo
    End Get
    Set(ByVal Value As String)
      mstrDescripcActivo = Value
    End Set
  End Property

#End Region

#Region "-- Metodos --"

  Public Function fnc_Buscar(ByVal pintTipoBusqueda As Int16, ByRef pdtbBusqueda As DataTable) As String
    Dim lstrError As String = "", ldtbResultado As DataTable
    Dim larrParams() As Object = { _
    "ptin_tipolista", pintTipoBusqueda, _
    "pvch_co_acti_fijo", mstrCodigoActivo, _
    "pvch_de_acti_fijo", mstrDescripcActivo _
    }
    Try
      ldtbResultado = mobjConexionActivoFijo.ObtenerDataTable("usp_act_activofijo_busqueda", larrParams)
      pdtbBusqueda = ldtbResultado
    Catch ex As Exception
      lstrError = ex.Message
    Finally
    End Try
    Return lstrError
    ldtbResultado = Nothing
  End Function

#End Region

End Class