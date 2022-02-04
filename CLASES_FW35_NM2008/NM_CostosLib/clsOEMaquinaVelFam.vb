Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsOEMaquinaVelFam


#Region "-- Variables --"

    Protected Friend mConexion As AccesoDatosSQLServer
    Protected Friend mstrError As String
    Protected Friend mintPeriodoAno As Integer = 0
    Protected Friend mintPeriodoMes As Int16 = 0
    Protected Friend mstrEmpresa As String = ""
    Protected Friend mstrEstado As String = ""
    Protected Friend mstrUsuario As String = ""
    Protected Friend mintHusosEstandar As Integer = 0


#End Region

    '================================= Definición de constructores ===============================

    '================================= Definición de Propiedades =================================

#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
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

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property Estado() As String
        Get
            Estado = mstrEstado
        End Get
        Set(ByVal strCad As String)
            mstrEstado = strCad
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

    Public Property HusosEstandar() As Integer
        Get
            HusosEstandar = mintHusosEstandar
        End Get
        Set(ByVal intVal As Integer)
            mintHusosEstandar = intVal
        End Set
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================
    '*******************************************************************************************
    'Creado por:	  Edwin Poma
    'Fecha     :      2014.09.11
    'Proposito :      importa la data de los maestro de maquinas
    '*******************************************************************************************

    '======== Metodos de Consulta ========

    Public Function ObtenerEsquemas(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  epm
        'Fecha     :      11-09-2014
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas

        pdtDetalle.TableName = "lista"

        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.MaqVelocidadFamilia)
        If blnRpta = True Then
            pdtDetalle = pDT.Tables(0)
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


    'Public Function ImportarDatos() As Boolean
    '    '*******************************************************************************************
    '    'Creado por:	  cponce
    '    'Fecha     :      2010.11.25
    '    'Proposito :      importa datos de las maquinas para carda, pabilo hilo (OE)
    '    '*******************************************************************************************

    '    Dim blnRpta As Boolean
    '    Dim Conexion As AccesoDatosSQLServer

    '    Dim objParametro() As Object = {"ptin_Linea", "OE", _
    '                                    "pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoAno", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes, _
    '                                    "pvch_usuario", mstrUsuario}
    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        Conexion.EjecutarComando("usp_cos_maqhilovelocidad_procesar", objParametro)
    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try

    '    Return blnRpta

    'End Function


    Public Function ListarImportacionMaquina(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Pma
        'Fecha     :      2014.09.11
        'Proposito :      Lista los datos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_Linea", "OE", _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_maqhilovelocidadfam_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function GuardarDatos(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      11-09-2014
        'Proposito :      Guarda los datos
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_maqhilovelocidadfam_guardar", objParametro)
            clsUtilitario = Nothing
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function GenerarDatosxMaquinas(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      11-09-2014
        'Proposito :      Guarda los datos
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"ptin_Linea", "OE", _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptex_maquinas", clsUtilitario.GeneraXml(pdtDetalle), _
                                        "pint_HusosEstandar", mintHusosEstandar, _
                                        "pvch_usuario", mstrUsuario}
        Try

            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_maqhilovelocidadfam_procesar", objParametro)
            clsUtilitario = Nothing
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function CerrarImportacionMaquina() As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      2010-11-25
        'Proposito :      Cierra el mes de calculo de lo importado 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 45, _
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

    Public Function AbrirImportacionMaquina() As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      10-06-2010
        'Proposito :      Cambia el estado ha abireto para poder importar los datos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 45, _
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





End Class
