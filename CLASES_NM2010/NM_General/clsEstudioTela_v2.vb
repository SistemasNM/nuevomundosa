Imports System.Data.DataTable
Imports NM_General
Imports NM.AccesoDatos

Public Class clsEstudioTela_v2
#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
    Dim objParametros() As Object
#End Region

#Region "Entidad Analisis Tela"

    Dim strAccion As String
    Dim strCodCrudo As String
    Dim strCodAcabado As String
    Dim strIdRequer As String
    Dim strFecGen As String
    Dim strCodArtEstudio As String
    Dim strIdEstado As String
    Dim strCodOp As String
    Dim strCodArt As String
    Dim strDescArt As String
    Dim strColor As String
    Dim strMcaArt As String
    Dim strNumRequi As String
    Dim strUsuario As String

    Public Property Accion() As String
        Get
            Accion = strAccion
        End Get
        Set(ByVal strCad As String)
            strAccion = strCad
        End Set
    End Property

    Public Property CodCrudo() As String
        Get
            CodCrudo = strCodCrudo
        End Get
        Set(ByVal strCad As String)
            strCodCrudo = strCad
        End Set
    End Property

    Public Property CodAcabado() As String
        Get
            CodAcabado = strCodAcabado
        End Get
        Set(ByVal strCad As String)
            strCodAcabado = strCad
        End Set
    End Property

    Public Property IdRequer() As String
        Get
            IdRequer = strIdRequer
        End Get
        Set(ByVal strCad As String)
            strIdRequer = strCad
        End Set
    End Property

    Public Property FecGen() As String
        Get
            FecGen = strFecGen
        End Get
        Set(ByVal strCad As String)
            strFecGen = strCad
        End Set
    End Property

    Public Property CodArtEstudio() As String
        Get
            CodArtEstudio = strCodArtEstudio
        End Get
        Set(ByVal strCad As String)
            strCodArtEstudio = strCad
        End Set
    End Property

    Public Property IdEstado() As String
        Get
            IdEstado = strIdEstado
        End Get
        Set(ByVal strCad As String)
            strIdEstado = strCad
        End Set
    End Property

    Public Property CodOp() As String
        Get
            CodOp = strCodOp
        End Get
        Set(ByVal strCad As String)
            strCodOp = strCad
        End Set
    End Property

    Public Property CodArt() As String
        Get
            CodArt = strCodArt
        End Get
        Set(ByVal strCad As String)
            strCodArt = strCad
        End Set
    End Property

    Public Property DescArt() As String
        Get
            DescArt = strDescArt
        End Get
        Set(ByVal strCad As String)
            strDescArt = strCad
        End Set
    End Property

    Public Property Color() As String
        Get
            Color = strColor
        End Get
        Set(ByVal strCad As String)
            strColor = strCad
        End Set
    End Property

    Public Property McaArt() As String
        Get
            McaArt = strMcaArt
        End Get
        Set(ByVal strCad As String)
            strMcaArt = strCad
        End Set
    End Property

    Public Property NumRequi() As String
        Get
            NumRequi = strNumRequi
        End Get
        Set(ByVal strCad As String)
            strNumRequi = strCad
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Usuario = strUsuario
        End Get
        Set(ByVal strCad As String)
            strUsuario = strCad
        End Set
    End Property

#End Region

    Sub setearParametros(ByVal ObjclsEstudioTela As clsEstudioTela_v2)
        objParametros = {"p_vch_accion", ObjclsEstudioTela.Accion,
                         "p_vch_cod_crudo", ObjclsEstudioTela.CodCrudo,
                         "p_vch_cod_acabado", ObjclsEstudioTela.CodAcabado,
                         "p_vch_id_requer", ObjclsEstudioTela.IdRequer,
                         "p_vch_dtm_fec_gen", ObjclsEstudioTela.FecGen,
                         "p_vch_cod_art_estudio", ObjclsEstudioTela.CodArtEstudio,
                         "p_vch_id_estado", ObjclsEstudioTela.IdEstado,
                         "p_vch_cod_op", ObjclsEstudioTela.CodOp,
                         "p_vch_cod_art", ObjclsEstudioTela.CodArt,
                         "p_vch_desc_art", ObjclsEstudioTela.DescArt,
                         "p_vch_color", ObjclsEstudioTela.Color,
                         "p_vch_mca_art", ObjclsEstudioTela.McaArt,
                         "p_vch_num_requi", ObjclsEstudioTela.NumRequi,
                         "p_vch_usuario", ObjclsEstudioTela.Usuario}
    End Sub

    Function CRUDEstudioTela(ByVal ObjclsEstudioTela As clsEstudioTela_v2) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            setearParametros(ObjclsEstudioTela)
            Return _objConexion.ObtenerDataTable("USP_ATE_UTB_DET_ART_REQUER_ATELA_MANTENEDOR_V2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
