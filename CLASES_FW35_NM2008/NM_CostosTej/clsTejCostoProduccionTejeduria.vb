Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejCostoProduccionTejeduria
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""

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
#End Region

    '-----------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 18-08-2011
    'Proposito : Maneja la entidad Producion de tejeduria
    '-----------------------------------------------------

    ' Importar costos de produccion de tejeduria
    Public Function fncCostoProdTejeduriaImportar() As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim blnRpta As Boolean = False

        Dim objParametros() As Object = {"pchr_codigoempresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo, "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_CostoProdTeje_Procesar ", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Consultar costos de produccion de tejeduria
    Public Function fncCostoProduccionConsultar(ByRef dstCostoProduccion As DataSet, ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstCostoProduccion = Conexion.ObtenerDataSet("usp_CosTej_DistCosto_Consultar", objParametros)
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
