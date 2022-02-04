Option Explicit On
Imports System.Data
Imports NM.AccesoDatos


Public Class clsPFC_ProcCompMP


    '================================ Definicion variables locales ================================
#Region "-- Variables --"

    Dim mstrError As String = ""

    Dim mstrEmpresa As String = ""
    Dim mintPeriodoAno As Integer = 0
    Dim mintPeriodoMes As Int16 = 0

    Dim mstrCodigoHilo As String = ""
    Dim mdblPCUAlgodon As Double = 0
    Dim mdblGCUAlgodon As Double = 0
    Dim mdblCUSpandex As Double = 0
    Dim mdblCUPolyester As Double = 0
    Dim mdblCUHiloComp As Double = 0

    Dim mstrUsuario As String = ""

#End Region

    '================================= Definición de constructores ===============================


    '================================= Definición de Propiedades =================================
#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property PeriodoAno() As Integer
        Get
            PeriodoAno = mintPeriodoAno
        End Get
        Set(ByVal intVal As Integer)
            mintPeriodoAno = intVal
        End Set
    End Property

    Public Property PeriodoMes() As Int16
        Get
            PeriodoMes = mintPeriodoMes
        End Get
        Set(ByVal intVal As Int16)
            mintPeriodoMes = intVal
        End Set
    End Property

    Public Property CodigoHilo() As String
        Get
            CodigoHilo = mstrCodigoHilo
        End Get
        Set(ByVal strVal As String)
            mstrCodigoHilo = strVal
        End Set
    End Property

    Public Property PCUAlgodon() As Double
        Get
            PCUAlgodon = mdblPCUAlgodon
        End Get
        Set(ByVal dblVal As Double)
            mdblPCUAlgodon = dblVal
        End Set
    End Property

    Public Property GCUAlgodon() As Double
        Get
            GCUAlgodon = mdblGCUAlgodon
        End Get
        Set(ByVal dblVal As Double)
            mdblGCUAlgodon = dblVal
        End Set
    End Property


    Public Property CUSpandex() As Double
        Get
            CUSpandex = mdblCUSpandex
        End Get
        Set(ByVal dblVal As Double)
            mdblCUSpandex = dblVal
        End Set
    End Property

    Public Property CUPolyester() As Double
        Get
            CUPolyester = mdblCUPolyester
        End Get
        Set(ByVal dblVal As Double)
            mdblCUPolyester = dblVal
        End Set
    End Property

    Public Property CUHiloComp() As Double
        Get
            CUHiloComp = mdblCUHiloComp
        End Get
        Set(ByVal dblVal As Double)
            mdblCUHiloComp = dblVal
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

#End Region

    '=================================== Definicion de metodos  ==================================
#Region "-- Metodos --"

    Public Function Listar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  CPT
        'Fecha     :      13-04-2012
        'Proposito :      Lista los cu de la composicion de la mp
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_procostomp_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    
    Public Function Actualizar() As Boolean
        '*******************************************************************************************
        'Creado por:	  CPT
        'Fecha     :      16-04-2012
        'Proposito :      Actualiza los CU de las mp
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pnum_PCUAlgodon", mdblPCUAlgodon, _
                                        "pnum_GCUAlgodon", mdblGCUAlgodon, _
                                        "pnum_CUSpandex", mdblCUSpandex, _
                                        "pnum_CUPolyester", mdblCUPolyester, _
                                        "pnum_CUHiloComp", mdblCUHiloComp, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_procostomp_actualizar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function



    Public Function Importar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      2012-04-17
        'Proposito :      importa los para la composicion y costos de la MP
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pint_Tipo", 1}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_CompMariaPrima_procesar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodo() As Boolean
        '************************************************  
        'Creado por:	  cpt
        'Fecha     :      2012.04.17
        'Proposito :      Ejecuta el proceso de cerrar los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 28, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccion_cerrar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function AbrirPeriodo() As Boolean
        '************************************************  
        'Creado por:	  cpt
        'Fecha     :      2012.04.17
        'Proposito :      Ejecuta el proceso de abrir los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 28, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccion_abrir", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function


#End Region


End Class
