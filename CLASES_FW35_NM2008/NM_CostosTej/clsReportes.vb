Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsReportes

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

#End Region

#Region "-- Metodos --"

    Public Function ListarBalance(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      10-01-2011
        'Proposito :      lista los balance - costos ponderados
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_Balance_Listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarInvValorizado(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      10-01-2011
        'Proposito :      lista los inventarios valorizados
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_InvValorizado_Listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistCostos1(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      10-01-2011
        'Proposito :      Lista la distribucion del costo parte 1
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_CosTej_DistCosto_Listar1", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistCostos2(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      10-01-2011
        'Proposito :      Lista la distribucion del costo parte 2
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_CosTej_DistCosto_Listar2", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistCostos2_7Art(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      10-01-2011
        'Proposito :      Lista la distribucion del costo parte 2
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_CosTej_DistCosto7Dig_Listar2", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function
#End Region

End Class
