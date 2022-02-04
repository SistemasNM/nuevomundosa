Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejCalcPiezasP
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

    '------------------------------------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 10-02-2012
    'Proposito : Esta clase realiza las importaciones internas de KGS DE PIEZAS P:
    '            hoja 9\CALC_PIEZASP (Cabecera)
    '------------------------------------------------------------------------------

    Public Function fncImportarCalcPiezasPCab(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_CALC_PIEZASP", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarCalcPiezasPDet(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_CALC_PIEZASP_Detalle", objParametros)
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
