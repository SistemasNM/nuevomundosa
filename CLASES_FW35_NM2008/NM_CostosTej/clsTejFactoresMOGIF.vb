Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejFactoresMOGIF
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""

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

    '-----------------------------------------------------
    'Creado por: Alexander Torres Cardenas
    'Fecha     : 01-06-2012
    'Proposito : Manejo de los factores de pretejido
    '-----------------------------------------------------

    ' Importar factores iniciales: todo en 1
    Public Function fncFactoresMOGIF_Inicializa() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo, "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_FactoresMOGIF_Inicializa", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Actualiza factores con los factores calculados
    Public Function fncFactoresMOGIF_Actualiza() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo, "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_FactoresMOGIF_Actualiza", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Consultar factores iniciales/calculados
    Public Function fncFactoresMOGIF_Listar(ByRef dstFactoresMOGIF As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstFactoresMOGIF = Conexion.ObtenerDataSet("usp_costej_FactoresMOGIF_Listar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Verifica factores
    Public Function fncFactoresMOGIF_Verifica(ByRef dstFactoresMOGIF As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstFactoresMOGIF = Conexion.ObtenerDataSet("usp_costej_FactoresMOGIF_Verifica", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Retorna el esquema de factores Gif/Mo
    Public Function ListarEsquemaFactores(ByRef dtbFactoresMOGIF As DataTable) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim dtsDetalle As DataSet
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dtsDetalle = Conexion.ObtenerDataSet("usp_CosTej_FactoresMOGIF_Esquema", objParametro)
            dtbFactoresMOGIF = dtsDetalle.Tables(0)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Permite actualizar los factores Gif/Mo
    Public Function GuardarDatosFactores(ByRef dtbFactoresMOGIF As DataTable) As Boolean
        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        dtbFactoresMOGIF.TableName = "lista"
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Usuario", mstrUsuario, "pnte_detalle", clsUtilitario.GeneraXml(dtbFactoresMOGIF)}
        clsUtilitario = Nothing
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_CosTej_FactoresMOGIF_Guardar", objParametro)
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
