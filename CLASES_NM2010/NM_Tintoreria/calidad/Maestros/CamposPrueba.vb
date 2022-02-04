Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace Calidad.Maestros
    Public Class CamposPrueba

#Region "VARIABLES"
        Private _strCodigoPrueba As String
        Private _strCodigoCampo As String
        Private _strNombreCampo As String
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
        Public Property CodigoCampo() As String
            Get
                Return _strCodigoCampo
            End Get
            Set(ByVal Value As String)
                _strCodigoCampo = Value
            End Set
        End Property
        Public Property NombreCampo() As String
            Get
                Return _strNombreCampo
            End Get
            Set(ByVal Value As String)
                _strNombreCampo = Value
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
        Public Function Registrar(ByVal dtbDatos As DataTable) As Boolean
            Try
                Dim objUtil As New NM_General.Util
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, _
                "ntx_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CamposPrueba_Registrar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Agregar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, _
                "var_NombreCampo", _strNombreCampo, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CamposPrueba_Insertar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Modificar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_CodigoCampo", _strCodigoCampo, _
                "var_NombreCampo", _strNombreCampo, "var_Estado", _strEstado, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CamposPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Eliminar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_CodigoCampo", _strCodigoCampo, _
                "var_NombreCampo", _strNombreCampo, "var_Estado", "ELI", "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CamposPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Listar() As DataTable
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                Return _objConexion.ObtenerDataTable("usp_CAL_CamposPrueba_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Obtener() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_CodigoCampo", _strCodigoCampo}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_CAL_CamposPrueba_Obtener", objParametros)
                For Each dtrItem As DataRow In dtbDatos.Rows
                    _strCodigoPrueba = dtrItem("Codigo_Prueba")
                    _strCodigoCampo = dtrItem("Codigo_Campo")
                    _strNombreCampo = dtrItem("Nombre_Campo")
                    _strEstado = dtrItem("Estado")
                Next
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class
End Namespace

