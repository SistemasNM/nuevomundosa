Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejParametros
#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""
    Dim mvchProceso As String
    Dim mstrSubProceso As String
    Dim mintCodigoParametro As Integer
    Dim mdblValorParametro As Double
    Dim mstrDescripcion As String

#End Region

#Region "-- Propiedades --"

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property Periodo() As Double
        Get
            Periodo = mnumPeriodo
        End Get
        Set(ByVal numVal As Double)
            mnumPeriodo = numVal
        End Set
    End Property

    Public Property Proceso() As String
        Get
            Return mvchProceso
        End Get
        Set(ByVal value As String)
            mvchProceso = value
        End Set
    End Property

    Public Property SubProceso() As String
        Get
            Return mstrSubProceso
        End Get
        Set(ByVal value As String)
            mstrSubProceso = value
        End Set
    End Property

    Public Property CodigoParametro() As Integer
        Get
            Return mintCodigoParametro
        End Get
        Set(ByVal value As Integer)
            mintCodigoParametro = value
        End Set
    End Property

    Public Property ValorParametro() As Double
        Get
            Return mdblValorParametro
        End Get
        Set(ByVal value As Double)
            mdblValorParametro = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return mstrDescripcion
        End Get
        Set(ByVal value As String)
            mstrDescripcion = value
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

#End Region

#Region "-- Parametros --"
    Enum Parametros
        '-- Parametros
        Par_MermaIF = 100            ' MERMA FATOR DE INVENATRIO FINAL
        Par_MermaDesperdicio = 101   ' MERMA FACTOR DE DESPERDICIOS
        Par_MermaDiferenciaIF = 102  ' MERMA DIFERECIA DE INVENTARIO FINAL

        Par_MermaPorMerDes = 115     ' MERMA PORCENTAJE DESPERDICIO KGS.

        Par_ProdEntregada = 103      ' PROD ENT
        Par_MetrosEquivalentes = 104 ' METROS EQUIV
    End Enum
#End Region

    Public Function fncListarParametro(ByRef dstParametro As DataSet, ByVal intParametro As Integer) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "int_CodigoParametro", intParametro}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstParametro = Conexion.ObtenerDataSet("usp_costej_Parametro_Consultar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' insertar parametro de merma
    Public Function fncInsertarParametro() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Proceso", mvchProceso, _
                                        "pvch_SubProceso", mstrSubProceso, _
                                        "pint_CodigoParametro", mintCodigoParametro, _
                                        "pnum_ValorParametro", mdblValorParametro, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pvch_UsuarioCreacion", mstrUsuario}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Parametros_Agregar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncParametroProceso(ByRef dstParametro As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "vch_Proceso", mvchProceso}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstParametro = Conexion.ObtenerDataSet("usp_costej_ParametroProceso_Consultar", objParametro)
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
