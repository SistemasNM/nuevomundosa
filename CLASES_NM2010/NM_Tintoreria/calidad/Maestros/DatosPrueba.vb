Imports NM.AccesoDatos
Namespace Calidad.Maestros
    Public Class DatosPrueba
#Region "VARIABLES"
        Private _strCodigoPrueba As String
        Private _strCodigoCampo As String
        Private _strCodigoDato As String
        Private _intRevisionDato As Int16
        Private _dblValorMinimo As Double
        Private _dblValorMaximo As Double
        Private _dblValorEstandar As Double
        Private _strEstado As String
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
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
        Public Property CodigoDato() As String
            Get
                Return _strCodigoDato
            End Get
            Set(ByVal Value As String)
                _strCodigoDato = Value
            End Set
        End Property
        Public Property RevisionDato() As Int16
            Get
                Return _intRevisionDato
            End Get
            Set(ByVal Value As Int16)
                _intRevisionDato = Value
            End Set
        End Property
        Public Property ValorMinimo() As Double
            Get
                Return _dblValorMinimo
            End Get
            Set(ByVal Value As Double)
                _dblValorMinimo = Value
            End Set
        End Property
        Public Property ValorMaximo() As Double
            Get
                Return _dblValorMaximo
            End Get
            Set(ByVal Value As Double)
                _dblValorMaximo = Value
            End Set
        End Property
        Public Property ValorEstandar() As Double
            Get
                Return _dblValorEstandar
            End Get
            Set(ByVal Value As Double)
                _dblValorEstandar = Value
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

#Region "METODOS Y FUNCIONES"

        Public Function ufn_DatosPrueba_Obtener(ByVal strCodigoPrueba As String, ByVal strCodigoDato As String) As DataSet
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            Try
                Dim objParametros() As String = {"CODIGO_PRUEBA", strCodigoPrueba, "CODIGO_DATO", strCodigoDato}
                Return _objConexion.ObtenerDataSet("usp_CAL_DatosPrueba_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Registrar(ByVal dtbDatos As DataTable) As Boolean
            Try
                Dim objUtil As New NM_General.Util
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_Codigo_Dato", _strCodigoDato, _
                "ntx_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_DatosPrueba_Registrar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function Modificar(ByVal dtbDatos As DataTable) As Boolean
            Try
                Dim objUtil As New NM_General.Util
                Dim strXMLDatos As String = objUtil.GeneraXml(dtbDatos)
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, _
                "ntx_XMLDatos", strXMLDatos, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_DatosPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Listar(ByVal strCodigoPrueba As String, ByVal strCodigoDato As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            If strCodigoDato = "" Then
                strCodigoDato = "%"
            End If
            Try
                Dim objParametros() As String = {"var_Codigo_Prueba", strCodigoPrueba, "var_Codigo_Dato", strCodigoDato}
                ldtDatos = lobjTinto.ObtenerDataTable("usp_CAL_DatosPrueba_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtDatos
        End Function
#End Region

    End Class
End Namespace


