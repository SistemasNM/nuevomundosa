Imports NM.AccesoDatos

Namespace Calidad.Maestros
    Public Class CriteriosPrueba

#Region "VARIABLES"
        Private _strCodigoPrueba As String
        Private _strCodigoCriterio As String
        Private _intPosicionInicial As Int16
        Private _intLongitud As Int16
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
        Public Property CodigoCriterio() As String
            Get
                Return _strCodigoCriterio
            End Get
            Set(ByVal Value As String)
                _strCodigoCriterio = Value
            End Set
        End Property
        Public Property PosicionInicial() As Int16
            Get
                Return _intPosicionInicial
            End Get
            Set(ByVal Value As Int16)
                _intPosicionInicial = Value
            End Set
        End Property
        Public Property Longitud() As Int16
            Get
                Return _intLongitud
            End Get
            Set(ByVal Value As Int16)
                _intLongitud = Value
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
                _objConexion.EjecutarComando("usp_CAL_CriteriosPrueba_Registrar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Agregar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, _
                "int_PosicionInicial", _intPosicionInicial, "int_Longitud", _intLongitud, _
                "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CriterioPrueba_Insertar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Modificar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_CodigoCriterio", _strCodigoCriterio, _
                "int_PosicionInicial", _intPosicionInicial, "int_Longitud", _intLongitud, _
                "var_Estado", _strEstado, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CriterioPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Eliminar() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_CodigoCriterio", _strCodigoCriterio, _
                "int_PosicionInicial", _intPosicionInicial, "int_Longitud", _intLongitud, _
                "var_Estado", "ELI", "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                _objConexion.EjecutarComando("usp_CAL_CriterioPrueba_Actualizar", objParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Listar() As DataTable
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                Return _objConexion.ObtenerDataTable("usp_CAL_CriterioPrueba_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Obtener() As Boolean
            Try
                Dim objParametros() As String = {"var_CodigoPrueba", _strCodigoPrueba, "var_CodigoCriterio", _strCodigoCriterio}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_CAL_CriterioPrueba_Obtener", objParametros)
                For Each dtrItem As DataRow In dtbDatos.Rows
                    _strCodigoPrueba = dtrItem("Codigo_Prueba")
                    _strCodigoCriterio = dtrItem("Codigo_Criterio")
                    _intPosicionInicial = dtrItem("Posicion_Inicial")
                    _intLongitud = dtrItem("Longitud")
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

