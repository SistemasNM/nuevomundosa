Imports NM.AccesoDatos

Public Class clsArticulo

#Region "Variables"
    Private mstr_Usuario As String
    Private mobj_Conexion As AccesoDatosSQLServer

    Private mstr_codarticulo As String = ""
    Private mstr_almtransfauto As String = ""

    Private mstr_codbarra1 As String = ""
    Private mstr_codbarra2 As String = ""
    Private mstr_codbarra3 As String = ""
    Private mstr_codbarra4 As String = ""
#End Region

#Region "-- Propiedades --"

    Public Property Usuario() As String
        Get
            Return mstr_Usuario
        End Get
        Set(ByVal Value As String)
            mstr_Usuario = Value
        End Set
    End Property

    Public Property CodArticulo() As String
        Get
            Return mstr_codarticulo
        End Get
        Set(ByVal Value As String)
            mstr_codarticulo = Value
        End Set
    End Property

    Public Property AlmTransfAutomatica() As String
        Get
            Return mstr_almtransfauto
        End Get
        Set(ByVal Value As String)
            mstr_almtransfauto = Value
        End Set
    End Property

    Public Property Codigo_Barra1() As String
        Get
            Return mstr_codbarra1
        End Get
        Set(ByVal Value As String)
            mstr_codbarra1 = Value
        End Set
    End Property

    Public Property Codigo_Barra2() As String
        Get
            Return mstr_codbarra2
        End Get
        Set(ByVal Value As String)
            mstr_codbarra2 = Value
        End Set
    End Property

    Public Property Codigo_Barra3() As String
        Get
            Return mstr_codbarra3
        End Get
        Set(ByVal Value As String)
            mstr_codbarra3 = Value
        End Set
    End Property

    Public Property Codigo_Barra4() As String
        Get
            Return mstr_codbarra4
        End Get
        Set(ByVal Value As String)
            mstr_codbarra4 = Value
        End Set
    End Property

#End Region

#Region "-- Metodos --"

    Public Function Listar_Caracteristicas(ByRef pdtb_lista As DataTable, ByVal pint_tipolista As Int16, ByVal pstr_tipoarticulo As String, ByVal pstr_rubro As String, ByVal pstr_familia As String, ByVal pstr_subfamilia As String) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipolista", pint_tipolista, _
            "pvch_tipoart", pstr_tipoarticulo, _
            "pvch_rubro", pstr_rubro, _
            "pvch_familia", pstr_familia, _
            "pvch_subfamilia", pstr_subfamilia}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("usp_log_articulocaracteristicas_lista", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Listar_DatosExtension(ByRef pdtb_lista As DataTable, ByVal pint_tipolista As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String) As Boolean
        'proceso: lista los datos de extension del tmitem
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipolista", pint_tipolista, _
            "pvch_param1", pstr_param1, _
            "pvch_param2", pstr_param2, _
            "pvch_param3", pstr_param3, _
            "pvch_param4", pstr_param4}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("usp_log_itemextension_listar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Guardar_DatosExtension(ByVal pint_accion As Int16) As Boolean
        'proceso: guarda los datos de extension del tmitem
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_accion", pint_accion, _
            "pvch_codarticulo", mstr_codarticulo, _
            "pvch_almtransfauto", mstr_almtransfauto, _
            "pvch_codbarra1", mstr_codbarra1, _
            "pvch_codbarra2", mstr_codbarra2, _
            "pvch_codbarra3", mstr_codbarra3, _
            "pvch_codbarra4", mstr_codbarra4, _
            "pvch_usumodificacion", mstr_Usuario}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("usp_log_itemextension_guardar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

#End Region


End Class
