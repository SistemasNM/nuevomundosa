Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsPFC_Merma

    '================================ Definicion variables locales ================================
#Region "-- Variables --"

    Protected Friend mstrError As String = ""

    Protected Friend mstrEmpresa As String = ""
    Protected Friend mintPeriodoAno As Integer = 0
    Protected Friend mintPeriodoMes As Int16 = 0
    Protected Friend mstrUsuario As String = ""

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
        'Creado por:	  cpt
        'Fecha     :      06-12-2010
        'Proposito :      retorna un listado de las mermas importadas
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
            pDT = Conexion.ObtenerDataSet("usp_cos_resumenmerma_listar ", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    '===== Metodos de Actualizacion  =====

    Public Function ObtenerEsquemas(ByRef pdtData As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      02-12-2010
        'Proposito :      Devuelve el esquema de las mermas
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas

        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.PFCMerma)
        If blnRpta = True Then
            pdtData = pDT.Tables(0)
        End If

        Try
            mstrError = ""
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally

        End Try

        Return blnRpta

    End Function

    Public Function Guardar(ByVal pdtData As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      02-12-2010
        'Proposito :      Permite actualizar los reg de algodones no valido
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtData.TableName = "listaMerma"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtData)}

        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_resumenmerma_guardar", objParametro)
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
        'Fecha     :      06-12-2010
        'Proposito :      importar el resumen de las mermas de las linea (promedio de los ult 4 meses)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_resumenmerma_importar", objParametro)
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
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.12.07
        'Proposito :      Ejecuta el proceso de cerrar los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 24, _
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
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.12.07
        'Proposito :      Ejecuta el proceso de abrir los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 24, _
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
