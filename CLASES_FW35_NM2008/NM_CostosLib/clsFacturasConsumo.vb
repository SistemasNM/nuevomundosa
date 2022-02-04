Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsFacturasConsumo

#Region "-- Variables --"

    Protected Friend mstrError As String = ""
    Protected Friend mstrEmpresa As String = ""
    Protected Friend mintPeriodoAno As Integer = 0
    Protected Friend mintPeriodoMes As Int16 = 0
    Protected Friend mintTipoProduc As Integer = 0
    Protected Friend mdblMontoFact As Double = 0
    Protected Friend mdblIgv As Double = 0
    Protected Friend mdblValImp As Double = 0


    Protected Friend mstrUsuario As String = ""


#End Region

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

    Public Property TipoDatoProd() As Integer
        Get
            TipoDatoProd = mintTipoProduc
        End Get
        Set(ByVal intVal As Integer)
            mintTipoProduc = intVal
        End Set
    End Property

    Public Property MontoFactura() As String
        Get
            MontoFactura = mdblMontoFact
        End Get

        Set(ByVal sCad As String)
            mdblMontoFact = sCad
        End Set
    End Property

    Public Property IGV() As Double
        Get
            IGV = mdblIgv
        End Get

        Set(ByVal dblVal As Double)
            mdblIgv = dblVal
        End Set
    End Property

    Public Property ValorImponible() As Double
        Get
            ValorImponible = mdblValImp
        End Get

        Set(ByVal dblVal As Double)
            mdblValImp = dblVal
        End Set
    End Property


    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get

        Set(ByVal sCad As String)
            mstrUsuario = sCad
        End Set
    End Property

#End Region

    Public Function ListarTipoConsumo(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo agua por campo orden
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "ptin_TipoDatoProd", mintTipoProduc}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_facturasconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function GuardarFacturasMontos() As Boolean
        '*******************************************************************************************
        'Creado por:	Edwin Poma
        'Fecha     :    24-05-2010
        'Proposito :    guarda los datos de las facturas o consumos para sus distribución, para el
        'caso de consumo de GLP el campo montosoles es totalPPR e igv es totalRRHH su tipodatopro=18
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "ptin_TipoDatoProd", mintTipoProduc, _
                                        "pnum_MontoSoles", mdblMontoFact, _
                                        "pnum_Igv", mdblIgv, _
                                        "pnum_ValorImponible", mdblValImp, _
                                        "pvch_Usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_facturasconsumo_gudar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    
End Class
