Imports NM.AccesoDatos

Namespace Lotes.Extra

    Public Class LoteCalculo

#Region "Variables privadas"
        Private _strCodigoReceta As String
        Private _dblVolumenPartida As Double
        Private _dblVolumenTotal As Double
        Private _intTotalPartidas As Int16
        Private _strCodigoLote As String
        Private _strPartidasDespacho As Int16
        Private _objConexion As AccesoDatosSQLServer

#End Region

#Region "Propiedades"
        Public Property CodigoReceta() As String
            Get
                Return _strCodigoReceta
            End Get
            Set(ByVal Value As String)
                _strCodigoReceta = Value
            End Set
        End Property

        Public Property VolumenPartida() As Double
            Get
                Return _dblVolumenPartida
            End Get
            Set(ByVal Value As Double)
                _dblVolumenPartida = Value
            End Set
        End Property
        Public Property VolumenTotal() As Double
            Get
                Return _dblVolumenTotal
            End Get
            Set(ByVal Value As Double)
                _dblVolumenTotal = Value
            End Set
        End Property
        Public Property TotalPartidas() As Int16
            Get
                Return _intTotalPartidas
            End Get
            Set(ByVal Value As Int16)
                _intTotalPartidas = Value
            End Set
        End Property
        Public Property CodigoLote() As String
            Get
                Return _strCodigoLote
            End Get
            Set(ByVal Value As String)
                _strCodigoLote = Value
            End Set
        End Property
        Public Property PartidasDespacho() As Int16
            Get
                Return _strPartidasDespacho
            End Get
            Set(ByVal Value As Int16)
                _strPartidasDespacho = Value
            End Set
        End Property

#End Region

        Sub New()

        End Sub

        Public Function RecetasCalculo_Listar(ByVal dtbFichas As DataTable, _
        ByVal strCodigoOperacion As String, ByVal strCodigoLote As String) As DataTable
            Try
                Dim objUtil As New NM_General.Util
                Dim objParametros() As String = {"var_CodigoOperacion", strCodigoOperacion, _
                "var_CodigoLote", strCodigoLote, "ntx_Fichas", objUtil.GeneraXml(dtbFichas)}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return _objConexion.ObtenerDataTable("usp_TIN_LoteRecetaCalculo_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

End Namespace
