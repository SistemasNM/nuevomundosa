Option Explicit On
Imports System.Data
Imports NM.AccesoDatos


Public Class clsRecubridoraInvValorizado

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrEmpresa As String = ""
    Dim mintPeriodoAno As Integer = 0
    Dim mintPeriodoMes As Int16 = 0
    Dim mstrUsuario As String = ""

#End Region

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

    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property

#End Region

#Region "-- Metodos --"

    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.04.08
        'Proposito :      retorna 2 consultas del inventario de proceso de la recubridora
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_recubridorainvvalorizado_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    'Public Function ListarDatosSinProcesar(ByRef pDT As DataSet) As Boolean
    '    '*******************************************************************************************
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      2010.03.23
    '    'Proposito :      retorna 2 consultas del composición de hilo de la recubridora lista de cabecera, lista de detalle
    '    '*******************************************************************************************

    '    Dim blnRpta As Boolean
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim objParametro() As Object = {"ptin_tipolista", 3, _
    '                                    "pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoano", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes}
    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        pDT = Conexion.ObtenerDataSet("usp_cos_recubridoracomphilo_listar", objParametro)

    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try

    '    Return blnRpta
    'End Function

    Public Function GuardarDatos(ByVal pdtDetalle1 As DataTable, ByVal pdtDetalle2 As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.04.08
        'Proposito :      Guarda los datos del inventario del proceso de la recubridora
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle1.TableName = "lista1"
        pdtDetalle2.TableName = "lista2"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pnvc_detalle1", clsUtilitario.GeneraXml(pdtDetalle1), _
                                        "pnvc_detalle2", clsUtilitario.GeneraXml(pdtDetalle2)}

        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_recubridorainvvalorizado_guardar", objParametro)
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
        'Fecha     :      2010.04.08
        'Proposito :      Ejecuta el proceso de cerrar los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 11, _
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
        'Fecha     :      2010.04.08
        'Proposito :      Ejecuta el proceso de abrir los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 11, _
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

    Public Function LimpiarDatos() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.04.08
        'Proposito :      Ejecuta el proceso de limpiar los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 11, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccion_limpiar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ObtenerEsquemas(ByRef pdtDetallePT As DataTable, ByRef pdtDetalleMP As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.04.08
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetallePT.TableName = "lista1"
        pdtDetalleMP.TableName = "lista2"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.RecuInvValorizado)
        If blnRpta = True Then
            pdtDetallePT = pDT.Tables(0)
            pdtDetalleMP = pDT.Tables(1)
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

#End Region

End Class
