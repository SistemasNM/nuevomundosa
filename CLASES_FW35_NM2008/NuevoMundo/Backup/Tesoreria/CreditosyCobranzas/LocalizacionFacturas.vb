Imports NM.AccesoDatos

Namespace Tesoreria.CreditosyCobranzas
    Public Class LocalizacionFacturas

#Region "VARIABLES PRIVADAS"
        Private _objConexion As AccesoDatosSQLServer
        Private _strDocumento As String
        Private _strFechaGerencia As String
        Private _strFechaDespacho As String
        Private _strFechaConforme As String
        Private _strFechaRecogida As String
        Private _intCondicion As Int16
        Private _intStatus As Int16
        Private _strUsuario As String
#End Region

#Region "PROPIEDADES PUBLICAS"
        Public Property Documento() As String
            Get
                Return _strDocumento
            End Get
            Set(ByVal Value As String)
                _strDocumento = Value
            End Set
        End Property
        Public Property FechaGerencia() As String
            Get
                Return _strFechaGerencia
            End Get
            Set(ByVal Value As String)
                _strFechaGerencia = Value
            End Set
        End Property
        Public Property FechaDespacho() As String
            Get
                Return _strFechaDespacho
            End Get
            Set(ByVal Value As String)
                _strFechaDespacho = Value
            End Set
        End Property
        Public Property FechaConforme() As String
            Get
                Return _strFechaConforme
            End Get
            Set(ByVal Value As String)
                _strFechaConforme = Value
            End Set
        End Property
        Public Property FechaRecogida() As String
            Get
                Return _strFechaRecogida
            End Get
            Set(ByVal Value As String)
                _strFechaRecogida = Value
            End Set
        End Property
        Public Property Condicion() As Int16
            Get
                Return _intCondicion
            End Get
            Set(ByVal Value As Int16)
                _intCondicion = Value
            End Set
        End Property
        Public Property Status() As Int16
            Get
                Return _intStatus
            End Get
            Set(ByVal Value As Int16)
                _intStatus = Value
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
            _strUsuario = ""
        End Sub
#End Region

#Region "FUNCIONES"

        Public Function ufn_CondicionFactura_Obtener() As DataTable
            Try
                Dim objGeneral As New NuevoMundo.General
                Return objGeneral.ufn_TablaParametro_Obtener("15")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ufn_StatusFactura_Obtener() As DataTable
            Try
                Dim objGeneral As New NuevoMundo.General
                Return objGeneral.ufn_TablaParametro_Obtener("16")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ufn_LocalizacionFactura_Obtener(ByVal strDocumento As String, _
        ByVal strCliente As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String, _
        ByVal strCondicion As String, ByVal strStatus As String) As DataTable
            Dim objUtil As New NuevoMundo.General
            strFechaInicio = objUtil.ConvertirFecha(strFechaInicio)
            strFechaFinal = objUtil.ConvertirFecha(strFechaFinal)
            Try
                Dim lstrParams() As String = {"var_Documento", strDocumento.Trim, "var_RUC", strCliente.Trim, _
                                            "var_FechaIni", strFechaFinal, "var_FechaFin", strFechaFinal, _
                                            "int_Condicion", CInt(strCondicion), _
                                            "int_Status", CInt(strStatus)}
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_LocalizacionFacturas_Listar", lstrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_LocalizacionFacturaDetalle_Obtener(ByVal strDocumento As String) As DataSet
            Try
                Dim strParametros() As String = {"var_Documento", strDocumento.Trim}
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataSet("usp_TES_LocalizacionFacturas_Buscar", strParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "PROCEDIMIENTOS"

        Public Sub prc_LocalizacionFactura_Grabar()
            Dim objUtil As New General
            Try
                Dim strParametros() As String = {"var_empresa", "01", "var_Tipo", "FAC", _
                                            "var_Numero", _strDocumento, _
                                            "var_fechaGerencia", objUtil.ConvertirFecha(_strFechaGerencia), _
                                            "var_FechaDespacho", objUtil.ConvertirFecha(_strFechaDespacho), _
                                            "var_FechaConforme", objUtil.ConvertirFecha(_strFechaConforme), _
                                            "var_FechaRecogida", objUtil.ConvertirFecha(_strFechaRecogida), _
                                            "int_Condicion", _intCondicion, _
                                            "int_Status", _intStatus, _
                                            "var_Usuario", _strUsuario}
                _objConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                _objConexion.ObtenerDataSet("usp_TES_LocalizacionFacturas_Grabar", strParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
#End Region

    End Class
End Namespace

