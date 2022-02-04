Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejDiasTrabajados

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mintDiasUrdCrudo As Integer
    Dim mintDiasEngCrudo As Integer
    Dim mintDiasUrdTed As Integer
    Dim mintDiasEngTed As Integer

#End Region

#Region "-- Propiedades --"

    Public Property DiasUrdCrudo() As Integer
        Get
            Return mintDiasUrdCrudo
        End Get
        Set(ByVal value As Integer)
            mintDiasUrdCrudo = value
        End Set
    End Property

    Public Property DiasEngCrudo() As Integer
        Get
            Return mintDiasEngCrudo
        End Get
        Set(ByVal value As Integer)
            mintDiasEngCrudo = value
        End Set
    End Property

    Public Property DiasUrdTed() As Integer
        Get
            Return mintDiasUrdTed
        End Get
        Set(ByVal value As Integer)
            mintDiasUrdTed = value
        End Set
    End Property

    Public Property DiasEngTed() As Integer
        Get
            Return mintDiasEngTed
        End Get
        Set(ByVal value As Integer)
            mintDiasEngTed = value
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    Public Function fncListarDiasTrabajados(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByRef dstDiasTrabajados As DataSet) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      18-06-2012
        'Proposito :      Retorna listado de dias segun tipo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.ImpFic_CostosDiasTrabajados}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstDiasTrabajados = Conexion.ObtenerDataSet("usp_costej_DiasTrabajados_Consultar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarDiasTrabajados(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal strUsuario As String) As Boolean
        '----------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      18-06-2012
        'Proposito :      graba dias segun tipo
        '----------------------------------------------

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_DiasUrdCru", mintDiasUrdCrudo, _
                                         "pint_DiasEngCru", mintDiasEngCrudo, _
                                         "pint_DiasUrdTed", mintDiasUrdTed, _
                                         "pint_DiasEngTed", mintDiasEngTed, _
                                         "pvch_Usuario", strUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.ObtenerDataSet("usp_costej_DiasTrabajados_Importar", objParametros)
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
