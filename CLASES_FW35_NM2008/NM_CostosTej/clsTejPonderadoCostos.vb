Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

'-----------------------------------------------------
'Creado por: Alexander Torres Cardenas
'Fecha     : 07-05-2012
'Proposito : Generar reporte de ponderaciion de cosotos
'-----------------------------------------------------

Public Class clsTejPonderadoCostos

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrUsuario As String = ""

    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0

#End Region

#Region "-- Propiedades --"
    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property Periodo() As Double
        Get
            Periodo = mnumPeriodo
        End Get
        Set(ByVal numVal As Double)
            mnumPeriodo = numVal
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

#End Region

    Public Function PonderadoCostosImportar(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Balance_Procesar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
End Class
