Imports System.Data.DataTable
Imports NM_General
Imports NM.AccesoDatos

Public Class clsAnalisisTela
#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
    Dim objParametros() As Object
#End Region

#Region "Entidad Analisis Tela"

    Dim strAccion As String
    Dim strIdRequer As String
    Dim strReferencia As String
    Dim strSolicitante As String
    Dim strIdEstado As String
    Dim strObservacion1 As String
    Dim strDescripcionTecnica As String
    Dim strLigamento As String
    Dim strPeso As String
    Dim strOnza As String
    Dim strAncho As String
    Dim strTitUrd As String
    Dim strTitTrama As String
    Dim strMatUrd As String
    Dim strMatTrama As String
    Dim strHilos As String
    Dim strPasadas As String
    Dim strColor As String
    Dim strElongacion As String
    Dim strCostoRef As String
    Dim strObservacion2 As String
    Dim strURLMuestraTela As String
    Dim strUsuario As String
    Dim strMca1 As String
    Dim strMca2 As String
    Dim strMca3 As String
    Dim strFecDesde As String
    Dim strFecHasta As String
    Dim intRes As Integer


    Public Property Accion() As String
        Get
            Accion = strAccion
        End Get
        Set(ByVal strCad As String)
            strAccion = strCad
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

    Public Property Referencia() As String
        Get
            Referencia = strReferencia
        End Get
        Set(ByVal strCad As String)
            strReferencia = strCad
        End Set
    End Property

    Public Property Solicitante() As String
        Get
            Solicitante = strSolicitante
        End Get
        Set(ByVal strCad As String)
            strSolicitante = strCad
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

    Public Property Observacion1() As String
        Get
            Observacion1 = strObservacion1
        End Get
        Set(ByVal strCad As String)
            strObservacion1 = strCad
        End Set
    End Property

    Public Property DescripcionTecnica() As String
        Get
            DescripcionTecnica = strDescripcionTecnica
        End Get
        Set(ByVal strCad As String)
            strDescripcionTecnica = strCad
        End Set
    End Property

    Public Property Ligamento() As String
        Get
            Ligamento = strLigamento
        End Get
        Set(ByVal strCad As String)
            strLigamento = strCad
        End Set
    End Property

    Public Property Peso() As String
        Get
            Peso = strPeso
        End Get
        Set(ByVal strCad As String)
            strPeso = strCad
        End Set
    End Property

    Public Property Onza() As String
        Get
            Onza = strOnza
        End Get
        Set(ByVal strCad As String)
            strOnza = strCad
        End Set
    End Property

    Public Property Ancho() As String
        Get
            Ancho = strAncho
        End Get
        Set(ByVal strCad As String)
            strAncho = strCad
        End Set
    End Property

    Public Property TitUrd() As String
        Get
            TitUrd = strTitUrd
        End Get
        Set(ByVal strCad As String)
            strTitUrd = strCad
        End Set
    End Property

    Public Property TitTrama() As String
        Get
            TitTrama = strTitTrama
        End Get
        Set(ByVal strCad As String)
            strTitTrama = strCad
        End Set
    End Property

    Public Property MatUrd() As String
        Get
            MatUrd = strMatUrd
        End Get
        Set(ByVal strCad As String)
            strMatUrd = strCad
        End Set
    End Property

    Public Property MatTrama() As String
        Get
            MatTrama = strMatTrama
        End Get
        Set(ByVal strCad As String)
            strMatTrama = strCad
        End Set
    End Property

    Public Property Hilos() As String
        Get
            Hilos = strHilos
        End Get
        Set(ByVal strCad As String)
            strHilos = strCad
        End Set
    End Property

    Public Property Pasadas() As String
        Get
            Pasadas = strPasadas
        End Get
        Set(ByVal strCad As String)
            strPasadas = strCad
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

    Public Property Elongacion() As String
        Get
            Elongacion = strElongacion
        End Get
        Set(ByVal strCad As String)
            strElongacion = strCad
        End Set
    End Property

    Public Property CostoRef() As String
        Get
            CostoRef = strCostoRef
        End Get
        Set(ByVal strCad As String)
            strCostoRef = strCad
        End Set
    End Property

    Public Property Observacion2() As String
        Get
            Observacion2 = strObservacion2
        End Get
        Set(ByVal strCad As String)
            strObservacion2 = strCad
        End Set
    End Property

    Public Property URLMuestraTela() As String
        Get
            URLMuestraTela = strURLMuestraTela
        End Get
        Set(ByVal strCad As String)
            strURLMuestraTela = strCad
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

    Public Property Mca1() As String
        Get
            Mca1 = strMca1
        End Get
        Set(ByVal strCad As String)
            strMca1 = strCad
        End Set
    End Property

    Public Property Mca2() As String
        Get
            Mca2 = strMca2
        End Get
        Set(ByVal strCad As String)
            strMca2 = strCad
        End Set
    End Property

    Public Property Mca3() As String
        Get
            Mca3 = strMca3
        End Get
        Set(ByVal strCad As String)
            strMca3 = strCad
        End Set
    End Property

    Public Property FecDesde() As String
        Get
            FecDesde = strFecDesde
        End Get
        Set(ByVal strCad As String)
            strFecDesde = strCad
        End Set
    End Property

    Public Property FecHasta() As String
        Get
            FecHasta = strFecHasta
        End Get
        Set(ByVal strCad As String)
            strFecHasta = strCad
        End Set
    End Property

    Public Property Response() As Integer
        Get
            Response = intRes
        End Get
        Set(ByVal intCad As Integer)
            intRes = intCad
        End Set
    End Property

#End Region

    Sub setearParametros(ByVal ObjclsAnalisisTela As clsAnalisisTela)
        objParametros = {"p_vch_accion", ObjclsAnalisisTela.Accion,
                         "p_vch_id_requer", ObjclsAnalisisTela.IdRequer,
                         "p_vch_referencia", ObjclsAnalisisTela.Referencia,
                         "p_vch_solicitante", ObjclsAnalisisTela.Solicitante,
                         "p_vch_id_estado", ObjclsAnalisisTela.IdEstado,
                         "p_vch_observacion1", ObjclsAnalisisTela.Observacion1,
                         "p_vch_descrip_tec", ObjclsAnalisisTela.DescripcionTecnica,
                         "p_vch_ligamento", ObjclsAnalisisTela.Ligamento,
                         "p_num_peso", ObjclsAnalisisTela.Peso,
                         "p_num_onza", ObjclsAnalisisTela.Onza,
                         "p_num_ancho", ObjclsAnalisisTela.Ancho,
                         "p_vch_tit_urd", ObjclsAnalisisTela.TitUrd,
                         "p_vch_tit_trama", ObjclsAnalisisTela.TitTrama,
                         "p_vch_mat_urd", ObjclsAnalisisTela.MatUrd,
                         "p_vch_mat_trama", ObjclsAnalisisTela.MatTrama,
                         "p_int_hilos", ObjclsAnalisisTela.Hilos,
                         "p_int_pasadas", ObjclsAnalisisTela.Pasadas,
                         "p_vch_color", ObjclsAnalisisTela.Color,
                         "p_vch_elongacion", ObjclsAnalisisTela.Elongacion,
                         "p_num_costo_ref", ObjclsAnalisisTela.CostoRef,
                         "p_vch_observacion2", ObjclsAnalisisTela.Observacion2,
                         "p_vch_ruta_muestra_tela", ObjclsAnalisisTela.URLMuestraTela,
                         "p_vch_usuario", ObjclsAnalisisTela.Usuario,
                         "p_vch_mca1", ObjclsAnalisisTela.Mca1,
                         "p_vch_mca2", ObjclsAnalisisTela.Mca2,
                         "p_vch_mca3", ObjclsAnalisisTela.Mca3,
                         "p_vch_fecdesde", ObjclsAnalisisTela.FecDesde,
                         "p_vch_fechasta", ObjclsAnalisisTela.FecHasta,
                         "p_int_res", ObjclsAnalisisTela.Response}
    End Sub

    Function CRUDRequerimientos(ByVal ObjclsAnalisisTela As clsAnalisisTela) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            setearParametros(ObjclsAnalisisTela)
            Return _objConexion.ObtenerDataTable("USP_ATE_UTB_REQUER_ATELA_MANTENEDOR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_TablaParametro_Obtener(ByVal strCodigoTabla As String) As DataTable
        Try
            Dim objParametros As Object() = {"chr_CodigoTabla", strCodigoTabla}
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Return _objConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
