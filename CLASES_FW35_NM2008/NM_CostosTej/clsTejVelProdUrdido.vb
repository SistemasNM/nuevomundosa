Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

'--------------------------------------------------------
'Creado por:	  Alexander Torres Cardenas
'Fecha     :      19-10-2011
'Proposito :      ABC de la Tabla: UTB_NM_VelProdUrdido
'--------------------------------------------------------
Public Class clsTejVelProdUrdido
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrPartidaUrdido As String
    Dim mnumMinMetros As Double
    Dim mstrTipo As String
    Dim mstrError As String
    Dim mstrUsuario As String


    Public Property PartidaUrdido() As String
        Get
            Return mstrPartidaUrdido
        End Get
        Set(ByVal value As String)
            mstrPartidaUrdido = value
        End Set
    End Property

    Public Property MinMetros() As Double
        Get
            Return mnumMinMetros
        End Get
        Set(ByVal value As Double)
            mnumMinMetros = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return mstrTipo
        End Get
        Set(ByVal value As String)
            mstrTipo = value
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Function fncListarVelProdUrdido(ByRef dstVelProdUrdido As DataSet, _
                                           ByVal mstrEmpresa As String, _
                                           ByVal mnumPeriodo As Double, _
                                           ByVal mstrTipoConsulta As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pchr_Tipo", mstrTipoConsulta}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstVelProdUrdido = Conexion.ObtenerDataSet("usp_costej_PorcDistribucionUrdidoCrudoTED", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncModificarVelProdUrdido(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                       ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_PartidaUrdido", mstrPartidaUrdido, _
                                        "pnum_MinMetros", mnumMinMetros, _
                                        "pchr_Tipo", mstrTipo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_VelProdUrdido_Actualizar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarVelProdUrdido(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                       ByVal mstrTipo As String, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Tipo", mstrTipo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_VelProdUrdido_Importar", objParametro)
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
