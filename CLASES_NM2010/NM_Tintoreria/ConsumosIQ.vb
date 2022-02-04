Imports System.IO
Imports System.data.OleDb
Imports nm.AccesoDatos
Imports NM_General

Public Class ConsumosIQ
#Region "Variables"
    Private _strUsuario As String
    Private _objConexion As AccesoDatosSQLServer
#End Region
#Region "Propiedades"
    Public Property Usuario() As String
        Get
            Return _strUsuario
        End Get
        Set(ByVal Value As String)
            _strUsuario = Value
        End Set
    End Property
#End Region
    Public Function Calculo_Consumo_Resumido(ByVal var_FechaInicio As String, ByVal var_FechaFinal As String) As DataTable
        Dim objParametros() As Object = {"var_FechaInicio", var_FechaInicio, "var_FechaFinal", var_FechaFinal}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Return _objConexion.ObtenerDataTable("USP_TIN_CONSUMO_IQ_RESUMIDO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Calculo_Consumo_DetalladoFE(ByVal var_FechaInicio As String, ByVal var_FechaFinal As String) As DataTable
        Dim objParametros() As Object = {"var_FechaInicio", var_FechaInicio, "var_FechaFinal", var_FechaFinal}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Return _objConexion.ObtenerDataTable("USP_TIN_CONSUMO_IQ_DETALLADO2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Carga_Consumo_Detallado(ByVal strCodigoUsuario As String, _
    ByVal strFechaInicio As String, ByVal strFechaFinal As String) As Boolean
        Dim objParametros() As Object = {"var_Usuario", strCodigoUsuario, _
        "var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            _objConexion.EjecutarComando("usp_TIN_ConsumoInsumosTintoreriaRS_Obtener", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Calculo_Consumo_Detallado(ByVal strCodigoFicha As String, ByVal strFechaInicio As String, _
    ByVal strFechaFinal As String, ByVal strCodigoOrden As String, ByVal strCodigoArticulo As String, _
    ByVal strCodigoInsumo As String, ByVal strNombreInsumo As String, ByVal strNombreArticulo As String) As DataTable
        Dim objParametros() As Object = {"var_CodigoFicha", strCodigoFicha, "var_FechaInicio", strFechaInicio, _
        "var_FechaFinal", strFechaFinal, "var_CodigoOrden", strCodigoOrden, "var_CodigoArticulo", strCodigoArticulo, _
        "var_CodigoInsumo", strCodigoInsumo, "var_NombreInsumo", strNombreInsumo, "var_NombreArticulo", strNombreArticulo}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Return _objConexion.ObtenerDataTable("usp_TIN_ConsumovsTeoricoInsumos_Detalle", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Calculo_Consumo_Detallado(ByVal var_FechaInicio As String, ByVal var_FechaFinal As String, ByVal var_codigoinsumo As String) As DataTable
        Dim objParametros() As Object = {"var_FechaInicio", var_FechaInicio, "var_FechaFinal", var_FechaFinal, "var_CodigoInsumo", var_codigoinsumo}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Return _objConexion.ObtenerDataTable("USP_TIN_CONSUMO_IQ_DETALLADO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'REQSIS201700015 - DG - INI
    'Public Function Carga_Consumo_Detallado_IQ(ByVal strCodigoUsuario As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String, ByVal strEstado As String) As Boolean
    '    Dim objParametros() As Object = {"var_Usuario", strCodigoUsuario, "var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal, "var_Estado", strEstado}
    '    Try
    '        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    '        _objConexion.EjecutarComando("usp_TIN_ConsumoInsumosTintoreriaRS_Obtener_PRUEBADG", objParametros)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function Carga_Consumo_Detallado_IQ(ByVal strCodigoUsuario As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String, ByVal iAbierto As Integer, ByVal iCerrado As Integer, ByVal iAnulado As Integer, ByVal iDespachado As Integer) As Boolean
        Dim objParametros() As Object = {"var_Usuario", strCodigoUsuario, "var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal, "var_EstadoAbierto", iAbierto, "var_EstadoCerrado", iCerrado, "var_EstadoAnulado", iAnulado, "var_EstadoDespachado", iDespachado}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            _objConexion.EjecutarComando("usp_TIN_ConsumoInsumosTintoreriaRS_Obtener_Detallado", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Carga_ResumenIQ(ByVal strCodigoUsuario As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String) As Boolean
        Dim objParametros() As Object = {"var_Usuario", strCodigoUsuario, "var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            _objConexion.EjecutarComando("usp_TIN_ConsumoResumenInsumosTintoreria", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Carga_ValePorLoteIQ(ByVal strCodigoUsuario As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String) As Boolean
        Dim objParametros() As Object = {"var_Usuario", strCodigoUsuario, "var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            _objConexion.EjecutarComando("usp_TIN_ConsumoValePorLoteTintoreria", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Carga_ResumenIQ_PorIQ(ByVal strCodigoUsuario As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String) As Boolean
        Dim objParametros() As Object = {"var_Usuario", strCodigoUsuario, "var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            _objConexion.EjecutarComando("usp_TIN_ConsumoResumenInsumosTintoreria_IQ", objParametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'REQSIS201700015 - DG - FIN
End Class
