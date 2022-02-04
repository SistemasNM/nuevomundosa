Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejTest

#Region "-- Variables --"



#End Region

    '================================= Definición de constructores ===============================

    '================================= Definición de Propiedades =================================

#Region "-- Propiedades --"


#End Region

    '=================================== Definicion de metodos  ==================================

    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de datos x periodo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", "01", _
                                        "pnum_periodo", 2011.02}

        Try

            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_test1", objParametro)

        Catch ex As Exception
            blnRpta = False

        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

End Class
