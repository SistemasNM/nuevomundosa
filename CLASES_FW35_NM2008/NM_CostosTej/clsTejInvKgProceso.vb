Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

'--------------------------------------------------------
'Creado por:	  Alexander Torres Cardenas
'Fecha     :      19-08-2011
'Proposito :      importar hilos en proceso de tejeduria
'--------------------------------------------------------

Public Class clsTejInvKgProceso
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Function fncImportarInvProceso(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_InvKgProceso_Importar", objParametros)
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
