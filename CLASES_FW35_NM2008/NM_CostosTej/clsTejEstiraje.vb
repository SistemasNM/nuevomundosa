Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejEstiraje
    Dim mstrError As String

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    Public Function fncConsultarEstirajes(ByRef dstEstirajes As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double) As Boolean
        '-----------------------------------------------------------------------
        'Creado por: Alexander Torres Cardenas
        'Fecha     : 31-08-2011
        'Proposito : Verifica si se realizo el calculo del Estiraje del periodo
        '-----------------------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstEstirajes = Conexion.ObtenerDataSet("usp_costej_Estirajes_Consultar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarEstirajes(ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        '-----------------------------------------------------------------------
        'Creado por: Alexander Torres Cardenas
        'Fecha     : 31-08-2011
        'Proposito : importamos y calculamos del Estiraje del periodo
        '-----------------------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_EstirajePartidas, _
                                         "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Estirajes_Calcular", objParametros)
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
