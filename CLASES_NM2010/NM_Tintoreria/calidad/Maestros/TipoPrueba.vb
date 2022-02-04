Imports NM.AccesoDatos

Namespace Calidad.Maestros
    Public Class TipoPrueba

#Region "VARIABLES"
        Private _strCodigoPrueba As String
        Private _intRevisionPrueba As Int16
        Private _strNombrePrueba As String
        Private _strEstado As String
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES PUBLICAS"
        Public Property CodigoPrueba() As String
            Get
                Return _strCodigoPrueba
            End Get
            Set(ByVal Value As String)
                _strCodigoPrueba = Value
            End Set
        End Property
        Public Property NombrePrueba() As String
            Get
                Return _strNombrePrueba
            End Get
            Set(ByVal Value As String)
                _strNombrePrueba = Value
            End Set
        End Property

        Public Property RevisionPrueba() As Int16
            Get
                Return _intRevisionPrueba
            End Get
            Set(ByVal Value As Int16)
                _intRevisionPrueba = Value
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

        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
#End Region

#Region "CONSTRUCTORES"

#End Region

#Region "METODOS y FUNCIONES"

        Public Function Agregar() As Boolean
            Try
                Dim objParametros() As String = {"var_NombrePrueba", _strNombrePrueba, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_TipoPrueba_Insertar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Modificar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, _
                "var_NombrePrueba", _strNombrePrueba, "var_Estado", _strEstado, _
                "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_TipoPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Eliminar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, _
                "var_NombrePrueba", _strNombrePrueba, "var_Estado", "ELI", _
                "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_TipoPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Listar() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                Return _objConexion.ObtenerDataTable("usp_CAL_TipoPrueba_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Obtener() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_CAL_TipoPrueba_Obtener", objParametros)
                For Each dtrItem As DataRow In dtbDatos.Rows
                    _strCodigoPrueba = dtrItem("Codigo_Prueba")
                    _intRevisionPrueba = dtrItem("Revision_Prueba")
                    _strEstado = dtrItem("Estado")
                    _strNombrePrueba = dtrItem("Nombre_Prueba")
                Next
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class
End Namespace

