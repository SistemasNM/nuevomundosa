﻿Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejIQEngCrudo

#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodPartidaEngomado As String = ""
    Dim mstrCodInsumo As String = ""
    Dim mdblKgFinal As Double
#End Region

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    '-------------------- Definicion de Metodos/Funciones  --------------------

    Public Function fncListarIQEngCrudo(ByRef dstIQEngCrudo As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, _
                                       ByVal mstrTipo As String) As Boolean
        '-----------------------------------------------------
        'Creado por: Alexander Torres Cardenas
        'Fecha     : 02-12-2011
        'Proposito : Retorna IQ y auxiliares del Eng Crudo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "chr_Tipo", mstrTipo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstIQEngCrudo = Conexion.ObtenerDataSet("usp_costej_ConsumoIQEngCrudo_Consultar", objParametros)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function


    Public Function fncImportarIQEngCrudo(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                          ByVal chr_Tipo As String, ByVal mstrUsuario As String) As Boolean
        '--------------------------------------------------------------
        'Creado por: Alexander Torres Cardenas
        'Fecha     : 03-12-2011
        'Proposito : Importar los IQ y auxiliares de Engomado crudo
        '--------------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "chr_Tipo", chr_Tipo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.CostIQ_EngCrudo, _
                                         "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ConsumoIQEngCrudo_Importar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncActualizarIQEngCrudo(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                 ByVal mstrPartidaEngomado As String, ByVal mstrCodigoIQ As String, _
                                 ByVal mstrTipo As String, ByVal dblKgFinal As Double, ByVal strUsuario As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      23-08-2011
        'Proposito :      Actualizar datos de la Pieza
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_PartidaEngomado", mstrPartidaEngomado, _
                                        "pvch_Item", mstrCodigoIQ, _
                                        "pchr_Tipo", mstrTipo, _
                                        "pnum_KgFinal", dblKgFinal, _
                                        "pvch_Usuario", strUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_ConsumoIQEngCrudo_Actualizar", objParametro)
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
