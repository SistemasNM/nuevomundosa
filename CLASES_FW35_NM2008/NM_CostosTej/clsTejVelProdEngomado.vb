Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

'--------------------------------------------------------
'Creado por:	  Alexander Torres Cardenas
'Fecha     :      19-10-2011
'Proposito :      ABC de la Tabla: UTB_NM_VelProdEngomado
'--------------------------------------------------------

Public Class clsTejVelProdEngomado
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrPartidaEngomado As String
    Dim mnumMinMetros As Double
    Dim mstrTipo As String
    Dim mstrError As String
    Dim mstrUsuario As String

    Public Property PartidaEngomado() As String
        Get
            Return mstrPartidaEngomado
        End Get
        Set(ByVal value As String)
            mstrPartidaEngomado = value
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

    ' Lista Velocidades de Engomado
    Public Function fncListarVelProdEngomado(ByRef dstVelProdEngomado As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, ByVal mstrTipoConsulta As String) As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pchr_tipo", mstrTipoConsulta}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstVelProdEngomado = Conexion.ObtenerDataSet("usp_costej_PorcDistribucionEngCrudoTED", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Actualizacion Velocidades de Engomado
    Public Function fncModificarVelProdEng(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                           ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_PartidaEngomado", mstrPartidaEngomado, _
                                        "pnum_MinMetros", mnumMinMetros, _
                                        "pchr_Tipo", mstrTipo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_VelProEng_Actualizar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Importacion Velocidades de Engomado
    Public Function fncImportarVelProdEng(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                          ByVal mstrTipo As String, ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Tipo", mstrTipo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_VelProEngomado_Importar", objParametro)
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
