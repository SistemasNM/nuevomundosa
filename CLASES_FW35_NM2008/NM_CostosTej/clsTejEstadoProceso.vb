Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejEstadoProceso

#Region "Definicion de datos Enumerados"

    ' DatGen = DATOS GENERALES
    ' HilTel = COSTOS DE HILO EN TELA
    ' CostIQ = COSTOS DE INSUMOS QUIMICOS
    ' CosPre = COSTOS DE MO - GIF - PRE-TEJIDO
    ' CosTel = COSTOS DE MO - GIF - TELARES
    ' CosRev = COSTOS DE MO - GIF - REVISION DE CRUDOS
    ' DisTej = DISTRIBUCION DE COSTOS DE TEJEDURIA
    ' AlmTel = ALMACEN DE TELA CRUDA
    ' ImpFic = IMPORTACION PARA FICHAS DE COSTO

    Public Enum Proceso
        ' --- DATOS GENERALES
        DatGen_KardexAlmTela = 1        ' KARDEX EN ALMACÉN DE TELA CRUDA
        DatGen_DatosProdEngomado = 2    ' DATOS DE PRODUCCIÓN DE ENGOMADO

        ' --- COSTOS DE HILO EN TELA
        HilTel_DatosHilos = 3            ' DATOS DE HILOS
        HilTel_DatosUrdimbre = 4         ' DATOS DE URDIMBRE
        HilTel_HojaTrabajo = 201         ' HOJA DE TRABAJO
        HilTel_DatosArticulo = 5         ' DATOS DE ARTÍCULOS
        HilTel_DatosProdUrdido = 6       ' DATOS DE PRODUCCION DE URDIDO POR ARMADA
        HilTel_MermaHilos = 7            ' MERMA DE HILOS
        HilTel_ProduccionEntregada = 8   ' PRODUCCION ENTREGADA VALORIZADA
        HilTel_ParticionPartidas = 9     ' PARTICIÓN DE PARTIDAS
        HilTel_EstirajePartidas = 10     ' ESTIRAJES POR PARTIDA
        HilTel_PonderacionCostos = 13    ' PONDERACIÓN DE COSTOS DE INGRESO
        HilTel_ArticuloPlegador = 16     ' INVENTARIO DE ARTICULO PLEGADOR

        ' --- COSTOS DE INSUMOS QUIMICOS
        CostIQ_IQAlma = 301              ' CONSUMO IQ ALMACEN ENGOMADO CRUDO-TED
        CostIQ_EngCrudo = 32             ' CONSUMO DE INSUMOS Y AUXILIARES ENGOMADO CRUDO
        CostIQ_EngTED = 33               ' CONSUMO DE INSUMOS Y AUXILIARES ENGOMADO TED

        ' --- COSTOS DE MO - GIF - PRE-TEJIDO
        CosPre_DisUrdidoCrudo = 36       ' % DISTRIBUCIÓN DE URDIDO CRUDO
        CosPre_DisUrdidoTED = 37         ' % DISTRIBUCIÓN DE URDIDO TED
        CosPre_DisEngomadoCrudo = 38     ' % DISTRIBUCIÓN DE ENGOMADO CRUDO
        CosPre_DisEngomadoTED = 39       ' % DISTRIBUCIÓN DE ENGOMADO TED
        CosPre_DisFactoresGifMo = 40       ' FACTORES DE GIF-MO DE PRE-TEJIDO 

        ' --- COSTOS DE MO - GIF - TELARES
        CosTel_Telares = 54              ' TELARES
        CosTel_ArticuloTelar = 51       ' ARTICULO TELAR
        CosTel_DisHorasTrabajadas = 52  ' DISTRIBUCIÓN DE HORAS TRABAJADAS
        CosTel_PorcentajeAvance = 53    ' PORCENTAJES DE AVANCE TEJEDURIA

        ' --- COSTOS DE MO - GIF - REVISION DE CRUDOS
        CosRev_MetrosRevisados = 60     ' METROS REVISADOS

        ' --- DISTRIBUCION DE COSTOS DE TEJEDURIA
        DisTej_CostosPreTejido = 49     ' COSTOS DE PRE-TEJIDO DENIM
        DisTej_CostosProduccion = 61    ' COSTO DE PRODUCCIÓN TEJEDURÍA

        ' --- ALMACEN DE TELA CRUDA
        AlmTel_ValorTelaTerminada = 64  ' VALORIZACION DE TELA TERMINADA

        ' --- IMPORTACION PARA FICHAS DE COSTO
        ImpFic_CostosDiasTrabajados = 72 ' DIAS TRABAJADOS
        ImpFic_CostosUrdido = 73         ' IMPORTACION DE COSTOS DE URDIDO
        ImpFic_CostosEngomado = 74       ' IMPORTACION DE COSTOS DE ENGOMADO
        ImpFic_CostosTelares = 75        ' IMPORTACION DE COSTOS DE TELARES

    End Enum

    Enum Modulo
        Mod_DatosGenerales = 100       ' DATOS GENERALES
        Mod_CostosHiloTela = 200       ' COSTOS DE HILO EN TELA
        Mod_CostosIQ = 300             ' COSTOS DE INSUMOS QUIMICOS
        Mod_CostosMOGifPreTejido = 400 ' COSTOS DE MO - GIF - PRE-TEJIDO
        Mod_CostosMOGifTelares = 500   ' COSTOS DE MO - GIF - TELARES
        Mod_CostosMOGifRevCrudos = 600 ' COSTOS DE MO - GIF - REVISION DE CRUDOS
        Mod_DisCostosTejeduria = 700   ' DISTRIBUCION DE COSTOS DE TEJEDURIA
        Mod_AlmacenTelaCruda = 800     ' ALMACEN DE TELA CRUDA
        Mod_ImportacionFicha = 900     ' IMPORTACION PARA FICHAS DE COSTO
    End Enum
#End Region

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrUsuario As String = ""

    Dim mnumPeriodo As Double = 0
    Dim mstrEmpresa As String = ""
    Dim mintTipProc As Integer = -1
    Dim mintTipModulo As Integer = -1

#End Region

    '================================= Definición de Propiedades =================================

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

    Public Property TipoProceso() As Proceso
        Get
            TipoProceso = mintTipProc
        End Get
        Set(ByVal numVal As Proceso)
            mintTipProc = numVal
        End Set
    End Property

    Public Property TipoModulo() As Modulo
        Get
            TipoModulo = mintTipModulo
        End Get
        Set(ByVal numVal As Modulo)
            mintTipModulo = numVal
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

#End Region

    '============================ Definicion de metodos Modulo (Cab) =============================

    Public Function PeriodoModuloIniciar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      13.09.2011
        'Proposito :      Rpermite realizar la apertura del mes
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_EstadoProcesoCab_Iniciar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function PeriodoModuloListar(ByRef objDS As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      13.09.2011
        'Proposito :      retorna la lista de datos x periodo
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", mintTipModulo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            objDS = Conexion.ObtenerDataSet("usp_costej_EstadoProcesoCab_Listar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function PeriodoModuloCerrar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      13.09.2011
        'Proposito :      Cierra el mes 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim tblValida As New DataTable
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", mintTipModulo, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            tblValida = Conexion.ObtenerDataTable("usp_costej_EstadoProcesoCab_Cerrar", objParametro)
            If tblValida.Rows.Count > 0 Then
                blnRpta = False
            Else
                blnRpta = True
            End If

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function PeriodoModuloAbrir() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      13.09.2011
        'Proposito :      Abrir el mes 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", mintTipModulo, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_EstadoProcesoCab_Abrir", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    '=========================== Definicion de metodos Proceso (Det) ============================

    Public Function PeriodoProcesoCerrar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      18-08-2011
        'Proposito :      Cierra el mes 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "ptin_Proceso", mintTipProc, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_EstadoProceso_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function PeriodoProcesoAbrir() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      18-08-2011
        'Proposito :      Abrir el mes 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "ptin_Proceso", mintTipProc, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_EstadoProceso_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function PeriodoProcesoListar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de datos x periodo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "ptin_proceso", mintTipProc}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_EstadoProceso_Listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function


    Public Function PeriodoListaPendientes(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de procesos pendientes de cierre x periodo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", mintTipModulo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_EstadoProcesoCab_ListaPend", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function VerificaProceso(ByVal strEmpresa As String, ByVal numPeriodo As Double, ByVal intProceso As Integer) As Boolean
        '---------------------------------------------------------------
        'Creado por:	  atorres
        'Fecha     :      15-08-2011
        'Proposito :      Verifica si esta importados el proceso previo
        '---------------------------------------------------------------

        Dim blnRpta As Boolean
        Dim ldtbProceso As DataTable
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", strEmpresa, _
                                        "pnum_periodo", numPeriodo, _
                                        "ptin_proceso", intProceso}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            ldtbProceso = Conexion.ObtenerDataTable("usp_costej_EstadoProceso_Listar", objParametro)
            If Not ldtbProceso Is Nothing And ldtbProceso.Rows.Count > 0 Then
                blnRpta = True
            Else
                blnRpta = False
            End If
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Consulta subprocesos de un modulo
    Public Function PeriodoProcesoConsultar(ByRef objDS As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "pint_Modulo", mintTipModulo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            objDS = Conexion.ObtenerDataSet("usp_costej_EstadoProceso_Consulta", objParametro)
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
