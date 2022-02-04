Imports NM.AccesoDatos
Namespace Maestros
    Public Class Color
        Private mintIntensidad As Double
        Private mintAngulo As Double
        Private mintCroma As Double
        Private mstrDescripcion As String
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mstrUsuario As String

        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub

#Region "   Propiedades"
        Public Property Intensidad() As Double
            Get
                Intensidad = mintIntensidad
            End Get
            Set(ByVal Value As Double)
                mintIntensidad = Value
            End Set
        End Property
        Public Property Angulo() As Double
            Get
                Angulo = mintAngulo
            End Get
            Set(ByVal Value As Double)
                mintAngulo = Value
            End Set
        End Property
        Public Property Croma() As Double
            Get
                Croma = mintCroma
            End Get
            Set(ByVal Value As Double)
                mintCroma = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Descripcion = mstrDescripcion
            End Get
            Set(ByVal Value As String)
                mstrDescripcion = Value
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
        Public Function Listar_Parametros_Colores() As DataTable
            Dim dtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                dtDatos = lobjTinto.ObtenerDataTable("USP_LOG_LISTAR_PARAMETROS_COLORES")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtDatos
        End Function
        Public Function Buscar_Colores(ByVal pIntensidad As Double, ByVal pAngulo As Double, ByVal pCroma As Double, ByVal pDescripcion As String) As DataTable
            Dim dtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"NUM_INTENSIDAD", pIntensidad, "NUM_ANGULO", pAngulo, "NUM_CROMA", pCroma, "Var_Descripcion", pDescripcion}
            Try
                dtDatos = lobjTinto.ObtenerDataTable("USP_BUSCAR_COLORES", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtDatos
        End Function
        Public Function Insertar_Colores(ByVal pIntensidad As Double, ByVal pAngulo As Double, ByVal pCroma As Double, ByVal pDescripcion As String, ByVal pUsuario As String) As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"num_Intensidad", pIntensidad, "Num_Angulo", pAngulo, "Num_Croma", pCroma, "Var_Descripcion", pDescripcion, "Var_Usuario", pUsuario, "Var_Mensaje", ""}
            Try
                Return CType(lobjTinto.ObtenerDataTable("USP_LOG_INSERTAR_COLOR", objParametros), DataTable)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Actualizar_Colores(ByVal pIntensidad As Double, ByVal pAngulo As Double, ByVal pCroma As Double, ByVal pDescripcion As String, ByVal pUsuario As String, ByVal intIdColor As Integer) As Boolean
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"num_Intensidad", pIntensidad, "Num_Angulo", pAngulo, "Num_Croma", pCroma, "Var_Descripcion", pDescripcion, "Var_Usuario", pUsuario, "Num_id_Color", intIdColor}
            Try
                lobjTinto.EjecutarComando("USP_LOG_ACTUALIZAR_COLOR", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function
        Public Function Eliminar_Color(ByVal pUsuario As String, ByVal strCodigoColor As String) As Boolean
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"Var_Usuario", pUsuario, "VAR_CODIGO_COLOR", strCodigoColor}
            Try
                lobjTinto.EjecutarComando("USP_LOG_ELIMINAR_COLOR", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function
        Public Function Generar_Codigo_Color(ByVal pIntensidad As Double, ByVal pAngulo As Double, ByVal pCroma As Double) As DataTable
            Dim dtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"num_valor_Intensidad", pIntensidad, "num_valor_Angulo", pAngulo, "num_valor_Croma", pCroma}
            Try
                dtDatos = lobjTinto.ObtenerDataTable("USP_LOG_PARAMETROS_COLORES", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtDatos
        End Function
#End Region
    End Class
End Namespace