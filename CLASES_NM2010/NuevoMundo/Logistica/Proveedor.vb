Imports NM.AccesoDatos
Imports NM_General

Namespace Logistica

    Public Class Proveedor

#Region "VARIABLES"
        Private _objConexion As AccesoDatosSQLServer
        Private _strCodigoProveedor As String
        Private _strNombreProveedor As String
        Private _strEstadoFactoring As String
        Private _strUsuario As String

#End Region

#Region "PROPIEDADES PUBLICAS"

        Public Property CodigoProveedor() As String
            Get
                Return _strCodigoProveedor
            End Get
            Set(ByVal Value As String)
                _strCodigoProveedor = Value
            End Set
        End Property

        Public Property NombreProveedor() As String
            Get
                Return _strNombreProveedor
            End Get
            Set(ByVal Value As String)
                _strNombreProveedor = Value
            End Set
        End Property

        Public Property EstadoFactoring() As String
            Get
                Return _strEstadoFactoring
            End Get
            Set(ByVal Value As String)
                _strEstadoFactoring = Value
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
        Sub New()
            _strCodigoProveedor = ""
            _strNombreProveedor = ""
            _strEstadoFactoring = ""
            _strUsuario = ""
        End Sub
#End Region

#Region "FUNCIONES"
        Public Function ufn_ListarProveedores() As DataTable
            Try
                Return ufn_ObtenerProveedor("", "")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_ObtenerProveedor(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Dim objParametros() As Object = {"var_CodigoProveedor", strCodigo, "var_NombreProveedor", strNombre}
                Return _objConexion.ObtenerDataTable("usp_TES_Proveedores_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_ObtenerProveedorFactoring(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoProveedor", strCodigo, "var_NombreProveedor", strNombre}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_ProveedoresFactoring_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_ObtenerDocumentosFactoring(ByVal strCodigo As String, _
        ByVal strNombre As String, ByVal strMoneda As String, ByVal strTipo As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoProveedor", strCodigo, _
                "var_NombreProveedor", strNombre, "var_TipoDocumento", strTipo, "var_Moneda", strMoneda}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_DocumentosFactoring_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_RegistrarProveedorFactoring(ByVal dtbDatos As DataTable) As Boolean
            Try
                Dim objUtil As New Util
                dtbDatos.TableName = "Factoring"
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As Object = {"var_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                _objConexion.ObtenerDataTable("usp_TES_ProveedoresFactoring_Registrar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "METODOS"

#End Region
    End Class

End Namespace
