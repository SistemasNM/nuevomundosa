Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejConsumoAlmacen
#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
#End Region

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    '------------------------------------------------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 12-07-2012
    'Proposito : Esta clase obtiene el consumo de IQ del almacen 007
    '           Hoja 6\(CALC_IQCRUALMA, CALC_IQTEDALMA)
    '------------------------------------------------------------------------------------------

    Public Function fncImportarConsumoAlmacen(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                              ByVal vch_Tipo As String, ByVal vchUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "chr_tipo", vch_Tipo, _
                                         "pvch_Usuario", vchUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ConsumoIQAlma_Importar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncConsultarConsumoIQ(ByRef dstConsumoIQ As DataSet, ByVal mstrEmpresa As String, _
                                        ByVal mnumPeriodo As Double, ByVal mstrTipo As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pvch_Tipo", mstrTipo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstConsumoIQ = Conexion.ObtenerDataSet("usp_costej_ConsumoIQAlma_Consultar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
End Class
