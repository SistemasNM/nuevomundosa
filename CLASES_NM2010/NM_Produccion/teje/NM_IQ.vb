Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Public Class NM_IQ
#Region "VARIABLES"
    Private m_objConnection As AccesoDatosSQLServer
    Private _strNombreInsumo As String
    Private _strCodigoInsumo As String
#End Region

#Region "PROPIEDADES PUBLICAS"
    Public Property NombreInsumo() As String
        Get
            Return _strNombreInsumo
        End Get
        Set(ByVal Value As String)
            _strNombreInsumo = Value
        End Set
    End Property

    Public Property CodigoInsumo() As String
        Get
            Return _strCodigoInsumo
        End Get
        Set(ByVal Value As String)
            _strCodigoInsumo = Value
        End Set
    End Property

#End Region

#Region "CONSTRUCTORES Y DESCRUCTORES"
    Sub New()
        m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    End Sub
#End Region

#Region "METODOS Y FUNCIONES"
    Public Function Obtener(ByVal strCodigo As String) As Boolean
        Try
            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_CodigoInsumo", strCodigo, "var_NombreInsumo", ""}
            Dim dtbDatos As DataTable = m_objConnection.ObtenerDataTable("usp_LOG_InsumosQuimicos_Obtener", objParametros)
            If dtbDatos.Rows.Count > 0 Then
                _strCodigoInsumo = dtbDatos.Rows(0)("var_CodigoInsumo")
                _strNombreInsumo = dtbDatos.Rows(0)("var_NombreInsumo")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Listar(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
        Try
            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_CodigoInsumo", strCodigo, "var_NombreInsumo", strNombre}
            Return m_objConnection.ObtenerDataTable("usp_LOG_InsumosQuimicos_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarInsumosQuimicos(ByVal tipo As String, ByVal FechaINI As String, ByVal FechaFIN As String, ByVal codigo_iq As String, ByVal descripcion As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"Tipo", tipo, "FechaINI", FechaINI, "FechaFIN", FechaFIN, "codigo_iq", codigo_iq, "descripcion", descripcion}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("SP_SEL_FiltrarInsumosQuimicos", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarPartida(ByVal tipo As String, ByVal FechaINI As String, ByVal FechaFIN As String, ByVal partida As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"Tipo", tipo, "FechaINI", FechaINI, "FechaFIN", FechaFIN, "partida", partida}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("SP_SEL_FiltrarPartidaIQ", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarUrdimbre(ByVal tipo As String, ByVal FechaINI As String, ByVal FechaFIN As String, ByVal urdimbre As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"Tipo", tipo, "FechaINI", FechaINI, "FechaFIN", FechaFIN, "urdimbre", urdimbre}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("SP_SEL_FiltrarUrdimbreIQ", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
#End Region

End Class
