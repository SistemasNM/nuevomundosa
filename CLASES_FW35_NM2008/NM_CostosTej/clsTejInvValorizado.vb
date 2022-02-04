Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejInvValorizado
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrEmpresa As String
    Dim mnumPeriodo As String
    Dim mstrUsuario As String
    Dim mstrError As String

    Public Property Periodo() As Double
        Get
            Periodo = mnumPeriodo
        End Get
        Set(ByVal numVal As Double)
            mnumPeriodo = numVal
        End Set
    End Property

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Function fncImportarInvValorizado() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_InvValorizado_Procesar", objParametro)
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
