Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejSolesTelarPlanta
    Dim mstrError As String

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    '-----------------------------------------------------
    'Creado por:	  Alexander Torres Cardenas
    'Fecha     :      05-10-2012
    'Proposito :      importar soles por telar y planta
    '-----------------------------------------------------

    Public Function fncActualizarSolesPlanta(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_TelarPlantaSoles_Actualiza", objParametros)
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
