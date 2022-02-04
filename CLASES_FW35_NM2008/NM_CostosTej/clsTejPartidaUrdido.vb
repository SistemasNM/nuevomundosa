Imports System.Data
Imports System.Data.OleDb
Imports NM.AccesoDatos

Public Class clsTejPartidaUrdido
#Region "-- Variables --"

    Protected Friend mConexion As AccesoDatosSQLServer
    Protected Friend mstrError As String

    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""

#End Region
#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property



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


    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property

#End Region

    ' Importacion de partida de urdido 
    Public Function PartidaUrdidoImportar() As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", clsTejEstadoProceso.Modulo.Mod_CostosHiloTela, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosProdUrdido, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PartidasUrdido_Importar", objParametro)
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
