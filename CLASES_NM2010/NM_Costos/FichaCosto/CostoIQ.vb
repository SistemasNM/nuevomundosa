Imports NM.AccesoDatos

Namespace FichaCosto.Mantenimiento

    Public Class CostoIQ
#Region "Variables"
        Private _objConexion As AccesoDatosSQLServer
        Private _strUsuario As String
        Private _intAnno As Int16
        Private _intMes As Int16
        Private _strCodigoIQ As String
        Private _dblCostoPromedio As Double
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
        Public Property CodigoIQ() As String
            Get
                Return _strCodigoIQ
            End Get
            Set(ByVal Value As String)
                _strCodigoIQ = Value
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

        Public Function GrabaCostoInsumoQuimico()
            Try
                Dim objParametros() As Object = {"sin_Anno", _intAnno, "sin_Mes", _intMes, _
                "var_Codigo", _strCodigoIQ, "num_Costo", _dblCostoPromedio, "var_Usuario", _strUsuario}
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                _objConexion.EjecutarComando("usp_COS_InsumoQuimico_GrabarVersion", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function ListarCostoInsumoQuimico() As DataTable
            Try
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Return _objConexion.ObtenerDataTable("usp_COS_InsumoQuimico_ListarCosto")
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

    End Class

End Namespace
