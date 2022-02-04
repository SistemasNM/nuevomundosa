Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejInvUrdimbreTelares
    Dim mstrError As String

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Function fncInvUrdimbreTelares_Importar(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_InvUrdimbreEnTelares_Importar", objParametros)
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
