Imports NM.AccesoDatos
Namespace Maestros
    Public Class Operacion
#Region "   Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mbolReceta As Boolean
        Private mbolTenido As Boolean
        Private mstrUsuario As String
#End Region
#Region "   Constructor"
        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub
        Sub New(ByVal strUsuario As String, ByVal strCodigo As String)
            mstrUsuario = strUsuario
            mstrCodigo = strCodigo
        End Sub
#End Region
#Region "   Propiedades"
        Public Property RequiereReceta() As Boolean
            Get
                RequiereReceta = mbolReceta
            End Get
            Set(ByVal Value As Boolean)
                mbolReceta = Value
            End Set
        End Property
        Public Property EsTennido() As Boolean
            Get
                EsTennido = mbolTenido
            End Get
            Set(ByVal Value As Boolean)
                mbolTenido = Value
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

#End Region
#Region "   Metodos"
        Public Function ListarXMaquina(ByVal strCodigoMaquina As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objParametros As Object() = {"var_CodigoMaquina", strCodigoMaquina}
                ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_Operaciones_ListarXMaquina", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtDatos
        End Function

        Public Function Obtener(ByVal strCodigoOperacion As String, ByVal strNombreOperacion As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                Dim objParametros As Object() = {"var_CodigoOperacion", strCodigoOperacion, _
                "var_NombreOperacion", strNombreOperacion}
                ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_Operaciones_Obtener", objParametros)
                If ldtDatos.Rows.Count = 1 Then
                    Me.mstrCodigo = ldtDatos.Rows(0)("codigo_operacion")
                    Me.mstrNombre = ldtDatos.Rows(0)("descripcion_operacion")
                    Me.mbolReceta = ldtDatos.Rows(0)("flag_receta")
                    Me.mbolTenido = ldtDatos.Rows(0)("flag_tenido")
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return ldtDatos
        End Function
#End Region
    End Class
End Namespace