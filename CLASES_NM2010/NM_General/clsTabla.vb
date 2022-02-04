Imports System.Data

Imports NM.AccesoDatos
Public Class clsTabla
#Region "Variables"
    Private mstr_Usuario As String
    Private mobj_Conexion As AccesoDatosSQLServer
#End Region

    Dim mstrError As String

    Dim mstrCodigo_Tabla As String
    Dim mstrNombre_Tabla As String
    Dim mstrTitulo_Tabla As String
    Dim mstrNomCol_Codi As String
    Dim mstrNomCol_Desc As String
    Dim mintSizCol_Codi As Integer
    Dim mintSizCol_Desc As Integer
    Dim mstrTitCol_Codi As String
    Dim mstrTitCol_Desc As String
    Dim mstrSenten_SQL As String

    Dim mstrFiltro As String

    '================================= Definición de constructores ===============================

    '================================= Definición de Propiedades =================================


    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Filtro() As String
        Get
            Filtro = mstrFiltro
        End Get
        Set(ByVal strCad As String)
            mstrFiltro = strCad
        End Set
    End Property


    Public ReadOnly Property CodigoTabla() As String
        Get
            CodigoTabla = mstrCodigo_Tabla
        End Get
    End Property


    Public Property NombreTabla() As String
        Get
            NombreTabla = mstrNombre_Tabla
        End Get
        Set(ByVal strCad As String)
            mstrNombre_Tabla = strCad
        End Set
    End Property

    Public ReadOnly Property Titulo() As String
        Get
            Titulo = mstrTitulo_Tabla
        End Get
    End Property

    Public ReadOnly Property NombreColumnaCodigo() As String
        Get
            NombreColumnaCodigo = mstrNomCol_Codi
        End Get
    End Property


    Public ReadOnly Property nombreColumnaDescri() As String
        Get
            nombreColumnaDescri = mstrNomCol_Desc
        End Get
    End Property

    Public ReadOnly Property SizeColumnaCodigo() As Integer
        Get
            SizeColumnaCodigo = mintSizCol_Codi
        End Get
    End Property

    Public ReadOnly Property SizeColumnaDescri() As Integer
        Get
            SizeColumnaDescri = mintSizCol_Desc
        End Get
    End Property

    Public Property SentenciaSQL() As String
        Get
            SentenciaSQL = mstrSenten_SQL
        End Get

        Set(ByVal strCad As String)
            mstrSenten_SQL = strCad
        End Set

    End Property

    Public ReadOnly Property TituloColumnaCodigo() As String
        Get
            TituloColumnaCodigo = mstrTitCol_Codi
        End Get
    End Property

    Public ReadOnly Property TituloColumnaDescri() As String
        Get
            TituloColumnaDescri = mstrTitCol_Desc
        End Get
    End Property


    '=================================== Definicion de metodos  ==================================

    Public Sub InicializaTabla()
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Inicializa las variables datos  para las propiedades
        '*******************************************************************************************

        Dim objDT As DataTable
        Dim clsData As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
        Dim objParam() As Object = {"Nom_Tabla", mstrNombre_Tabla}

        objDT = clsData.ObtenerDataTable("UP_SEL_DesTablaSql", objParam)

        If Not (objDT Is Nothing) Then
            With objDT
                If .Rows.Count > 0 Then
                    mstrCodigo_Tabla = .Rows(0)("vchCodigo_Tabla")
                    mstrNombre_Tabla = .Rows(0)("vchNombre_Tabla")
                    mstrTitulo_Tabla = .Rows(0)("varTitulo_Tabla")
                    mstrNomCol_Codi = .Rows(0)("vchNomCol_Codi")
                    mstrNomCol_Desc = .Rows(0)("vchNomCol_Desc")
                    mintSizCol_Codi = .Rows(0)("intSizCol_Codi")
                    mintSizCol_Desc = .Rows(0)("intSizCol_Desc")
                    mstrTitCol_Codi = .Rows(0)("vchTitCol_Codi")
                    mstrTitCol_Desc = .Rows(0)("vchTitCol_Desc")
                    mstrSenten_SQL = .Rows(0)("vchSenten_SQL")

                End If
            End With
        End If

        objDT = Nothing
        clsData = Nothing

    End Sub

    Public Function CargaLista(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Retorna el listado de una sentecia SQL
        '*******************************************************************************************

        Dim clsData As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
        Dim blnRpta As Boolean


        Try
            'Dim objParam() As Object = {"vchSQL", sSQL}
            'pDT = clsData.ObtenerDataTable("UP_Ejecuta_DesTabla", objParam)

            pDT = clsData.ObtenerDataTable2(mstrSenten_SQL)

            blnRpta = True
        Catch ex As Exception
            mstrError = ex.Message
            blnRpta = False
        End Try

        clsData = Nothing

        Return blnRpta

    End Function

    Public Function ListarAños(ByRef pdtb_lista As DataTable) As DataTable
        Dim lbln_estadof As Boolean = False
        Dim objParametros As Object() = {}
        Dim dt As DataTable
        Try
            Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dt = accesoDatos.ObtenerDataTable("USP_OT_Annos_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return dt
    End Function

    Public Function ListarResponsables(ByVal NU_ANNO As String, ByVal USUARIO As String) As DataTable
        Dim lbln_estadof As Boolean = False
        Dim objParametros As Object = {"NU_ANNO", NU_ANNO, "NO_USUA", USUARIO}
        Dim dtbPrograma As DataTable
        Try
            Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dtbPrograma = accesoDatos.ObtenerDataTable("USP_OT_Responsables_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbPrograma
    End Function

    Public Function ListarMeses(ByVal NU_ANNO As String) As DataTable
        Dim lbln_estadof As Boolean = False
        Dim objParametros As Object() = {}
        Dim dtbPrograma As DataTable
        Try
            Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dtbPrograma = accesoDatos.ObtenerDataTable("USP_OT_Meses_Listar", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtbPrograma
    End Function
    Public Function ListarCcostos(ByVal NU_ANNO As String) As DataTable
        Dim lbln_estadof As Boolean = False
        Dim objParametros As Object = {"NU_ANNO", NU_ANNO}
        Dim dtbPrograma As DataTable
        Try
            Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dtbPrograma = accesoDatos.ObtenerDataTable("USP_OT_CCosto_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbPrograma
    End Function

    Public Function ReporteResponsableGastos(ByVal strEmpr As String, ByVal strAno As String, ByVal strMes As String, ByVal strRes As String) As DataTable
        Dim lbln_estadof As Boolean = False
        Dim objParametros As Object = {"CO_EMPR", strEmpr, "NU_ANNO", strAno, "NU_MES", strMes, "CO_RES", strRes}
        Dim dtbPrograma As DataTable
        Try
            Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            dtbPrograma = accesoDatos.ObtenerDataTable("USP_PRES_OT_LISTAR_POR_MES", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtbPrograma
    End Function

End Class
