Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejSalidaHiloTejeduria
#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodigoUrdimbre As String = ""
    Dim mintRevisionUrdimbre As Integer
    Dim mstrCodigoPieza As String = ""
#End Region

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    '---------------------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 10-02-2012
    'Proposito : Registra la Salida de hilos a Almacen de tejeduria
    '            esta informacion es usada en la Poderacion de Costos
    '----------------------------------------------------------------
    Public Function fncSalidaHiloImportar(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_SalidaHiloFamilia_Importar", objParametros)
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
