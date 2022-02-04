Imports System.Data.DataTable
Imports CostosLib
Imports NM.AccesoDatos

Public Class clsEstudioTela
#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
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

    Function setearParametros(ByVal ObjclsEstudioTela As clsEstudioTela) As Object
        Dim objParametros() As Object = {"p_vch_accion", ObjclsEstudioTela.Accion, _
                                         "p_vch_cod_crudo", ObjclsEstudioTela.CodCrudo, _
                                         "p_vch_cod_acabado", ObjclsEstudioTela.CodAcabado, _
                                         "p_vch_id_requer", ObjclsEstudioTela.IdRequer, _
                                         "p_vch_dtm_fec_gen", ObjclsEstudioTela.IdEstado, _
                                         "p_vch_cod_art_estudio", ObjclsEstudioTela.CodArtEstudio, _
                                         "p_vch_id_estado", ObjclsEstudioTela.IdEstado, _
                                         "p_vch_cod_op", ObjclsEstudioTela.CodOp, _
                                         "p_vch_cod_art", ObjclsEstudioTela.CodArt, _
                                         "p_vch_desc_art", ObjclsEstudioTela.DescArt, _
                                         "p_vch_color", ObjclsEstudioTela.Color, _
                                         "p_vch_mca_art", ObjclsEstudioTela.McaArt, _
                                         "p_vch_num_requi", ObjclsEstudioTela.NumRequi, _
                                         "p_vch_usuario", ObjclsEstudioTela.Usuario}
        Return objParametros
    End Function

    Function CRUDEstudioTela(ByVal ObjclsEstudioTela As clsEstudioTela) As DataTable
        Try
            Dim objParametros() As Object = setearParametros(ObjclsEstudioTela)
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Return _objConexion.ObtenerDataTable("USP_ATE_UTB_DET_ART_REQUER_ATELA_MANTENEDOR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
