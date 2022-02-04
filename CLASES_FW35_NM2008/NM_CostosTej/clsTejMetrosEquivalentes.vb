Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejMetrosEquivalentes
#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrUsuario As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrEmpresa As String = ""

    Dim mdblPeso As Double
#End Region

#Region "-- Propiedades --"
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

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Peso() As Double
        Get
            Peso = mdblPeso
        End Get
        Set(ByVal value As Double)
            mdblPeso = value
        End Set
    End Property
#End Region

    ' Consultar metros revisados
    Public Function fncListarMetrosRevisados(ByRef dstMetrosRevisados As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.CosRev_MetrosRevisados}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstMetrosRevisados = Conexion.ObtenerDataSet("usp_costej_MetrosRevisado_Consultar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Importar metros revisados
    Public Function fncImportarMetrosRevisados() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.CosRev_MetrosRevisados, _
                                        "pnum_Peso", mdblPeso, _
                                        "pvch_Usuario", mstrUsuario}
        mstrError = ""
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_MetrosRevisado_Importar", objParametro)
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
