Imports NM.AccesoDatos

Namespace FichaCosto.Mantenimiento

    Public Class CostoHilo
#Region "Variables"
        Private _objConexion As AccesoDatosSQLServer
        Private _strUsuario As String
        Private _intAnno As Int16
        Private _intMes As Int16
        Private _strCodigoHilo As String
        Private _dblCostoPromedio As Double
        Private _dblCostoVariable As Double
        Private _dblCostoFijo As Double
        Private _dblOtrocosto As Double
        Private _dblManoObraFija As Double
        Private _dblManoObraVariable As Double
        Private _dblGastoFijoVariable As Double
        Private _dblGastoFijoNoVariable As Double
        Private _strEstado As String
        Private _intVersion As Int16
        Private _dblFactorFijoVariable As Double
#End Region

#Region "Propiedades"
        Public Property Anno() As Int16
            Get
                Return _intAnno
            End Get
            Set(ByVal Value As Int16)
                _intAnno = Value
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
        Public Property CodigoHilo() As String
            Get
                Return _strCodigoHilo
            End Get
            Set(ByVal Value As String)
                _strCodigoHilo = Value
            End Set
        End Property
        Public Property CostoPromedio() As Double
            Get
                Return _dblCostoPromedio
            End Get
            Set(ByVal Value As Double)
                _dblCostoPromedio = Value
            End Set
        End Property
        Public Property CostoVariable() As Double
            Get
                Return _dblCostoVariable
            End Get
            Set(ByVal Value As Double)
                _dblCostoVariable = Value
            End Set
        End Property
        Public Property CostoFijo() As Double
            Get
                Return _dblCostoFijo
            End Get
            Set(ByVal Value As Double)
                _dblCostoFijo = Value
            End Set
        End Property
        Public Property Otrocosto() As Double
            Get
                Return _dblOtrocosto
            End Get
            Set(ByVal Value As Double)
                _dblOtrocosto = Value
            End Set
        End Property
        Public Property ManoObraFija() As Double
            Get
                Return _dblManoObraFija
            End Get
            Set(ByVal Value As Double)
                _dblManoObraFija = Value
            End Set
        End Property
        Public Property ManoObraVariable() As Double
            Get
                Return _dblManoObraVariable
            End Get
            Set(ByVal Value As Double)
                _dblManoObraVariable = Value
            End Set
        End Property
        Public Property GastoFijoVariable() As Double
            Get
                Return _dblGastoFijoVariable
            End Get
            Set(ByVal Value As Double)
                _dblGastoFijoVariable = Value
            End Set
        End Property
        Public Property GastoFijoNoVariable() As Double
            Get
                Return _dblGastoFijoNoVariable
            End Get
            Set(ByVal Value As Double)
                _dblGastoFijoNoVariable = Value
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
        Public Property FactorFijoVariable() As Double
            Get
                Return _dblFactorFijoVariable
            End Get
            Set(ByVal Value As Double)
                _dblFactorFijoVariable = Value
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

        Public Function GrabaCostoProduccionHilo()
            Try
                Dim objParametros() As Object = {"sin_Anno", _intAnno, "sin_Mes", _intMes, _
                "var_CodigoHilo", _strCodigoHilo, "mon_CostoPromedio", _dblCostoPromedio, _
                "mon_CostoVariable", _dblCostoVariable, "mon_CostoFijo", _dblCostoFijo, _
                "mon_OtroCosto", _dblOtrocosto, "num_ManoObraFija", _dblManoObraFija, _
                "num_ManoObraVariable", _dblManoObraVariable, "num_GastoFijoVariable", _dblFactorFijoVariable, _
                "num_GastoFijoNoVariable", _dblGastoFijoNoVariable, "num_FactorFijoVariable", _dblFactorFijoVariable, _
                "var_Usuario", _strUsuario}
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                _objConexion.EjecutarComando("usp_COS_ProduccionHilo_GrabarVersion", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function ListarCostoProduccionHilo() As DataTable
            Try
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Return _objConexion.ObtenerDataTable("usp_COS_ProduccionHilo_ListarCosto")
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

    End Class

End Namespace
