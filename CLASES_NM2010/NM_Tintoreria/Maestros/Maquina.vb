Imports NM.AccesoDatos
Namespace Maestros
    Public Class Maquina
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mdblFactorCilindro As Double
        Private mstrUsuario As String
#Region "   Constructor"
        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub
#End Region

        Public Property FactorCilindro() As Double
            Get
                Return mdblFactorCilindro
            End Get
            Set(ByVal Value As Double)
                mdblFactorCilindro = Value
            End Set
        End Property
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property

#Region "   Metodos"
        Public Function Listar(ByVal strCodigo As String, ByVal strNombre As String, _
                        Optional ByVal booParaLotes As Boolean = False) As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim ldtDatos As DataTable
                Dim objParametros() As Object = {"var_CodigoMaquina", strCodigo, "var_NombreMaquina", strNombre, "sin_Flag", IIf(booParaLotes, 1, 0)}
                ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_Maquinas_Listar", objParametros)
                Return ldtDatos
            Catch ex As Exception
                Throw ex
            Finally
                lobjTinto = Nothing
            End Try
        End Function
#End Region
    End Class
End Namespace