Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejKardexTelaCruda

#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodigoArticulo As String = ""
    Dim mintRevisionArticulo As Integer
    Dim mstrCodigoHilo As String = ""
#End Region

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    '-------------------- Definicion de Metodos/Funciones  --------------------

    Public Function fncListarKardex(ByRef dstkardex As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, ByVal mstrArticulo As String, _
                                       ByVal mstrPieza As String, ByVal mstrTipoConsulta As String, _
                                       ByVal intFilaDesde As Integer, ByVal intFilaHasta As Integer) As Boolean
        '-----------------------------------------------------
        'Creado por: Alexander Torres Cardenas
        'Fecha     : 23-08-2011
        'Proposito : Retorna Kardex de tela cruda por periodo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pvch_CodigoArticulo", mstrArticulo, _
                                         "pvch_CodigoPieza", mstrPieza, _
                                         "pchr_TipoConsulta", mstrTipoConsulta, _
                                         "pint_FilaDesde", intFilaDesde, _
                                         "pint_FilaHasta", intFilaHasta, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.DatGen_KardexAlmTela}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstkardex = Conexion.ObtenerDataSet("usp_costej_KardexTelaCruda_Consultar", objParametros)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarkardex(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        '--------------------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      23-08-2011
        'Proposito :      Importar los kardex de Tela Cruda po periodo
        '--------------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.DatGen_KardexAlmTela, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_KardexTelaCruda_Importar", objParametros)
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
