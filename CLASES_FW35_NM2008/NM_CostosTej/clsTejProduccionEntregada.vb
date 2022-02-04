Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejProduccionEntregada
#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""
    Dim mdblFactorIF As Double

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

    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property FactorIF() As Double
        Get
            FactorIF = mdblFactorIF
        End Get
        Set(ByVal value As Double)
            mdblFactorIF = value
        End Set
    End Property
#End Region

    '-----------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 10-02-2012
    'Proposito : Manejo de la entidad produccion entregada
    '-----------------------------------------------------

    ' Consultar produccion entregada
    Public Function fncProduccionEntregadaListar(ByRef dstProduccionEntregada As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstProduccionEntregada = Conexion.ObtenerDataSet("usp_costej_ProduccionEntregada_Consultar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Importar produccion entregada
    Public Function fncProduccionEntregadaImportar() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ProduccionEntregada_Importar", objParametros)
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
