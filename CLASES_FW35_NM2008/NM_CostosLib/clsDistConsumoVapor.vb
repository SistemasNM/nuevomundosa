Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsDistConsumoVapor

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mintPeriodoAno As Integer = 0
    Dim mintPeriodoMes As Int16 = 0
    Dim mstrEmpresa As String = ""
    Dim mstrEstado As String = ""
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

#End Region

    '=================================== Definicion de metodos  ==================================
    '*******************************************************************************************
    'Creado por:	  Edwin Poma
    'Fecha     :      2010.01.21
    'Proposito :      Definir los metodos para el cálculo y distribucion del consumo de vapor
    '*******************************************************************************************

    '======== Metodos de Consulta ========

    Public Function ListarDatosxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.21
        'Proposito :      retorna 2 consultas del calculo de consumo de vapor lista de cabecera, lista de detalle
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_unidadconsumodistvaporxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDatosSinProcesarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.21
        'Proposito :      retorna 2 consultas del calculo de consumo de vapor lista de cabecera, lista de detalle
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_unidadconsumodistvaporxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function CerrarDistribucion() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Cierra el mes de calculo de consumo de agua
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_CodigoPlanta", "PPR", _
                                        "pchr_CodigoRecurso", "VA", _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_distconsumocentrocosto_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirDistribucion() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Cierra el mes de calculo de consumo de agua
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_CodigoPlanta", "PPR", _
                                        "pchr_CodigoRecurso", "VA", _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_distconsumocentrocosto_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function DistribuirConsumoGastos() As Boolean
        ''*******************************************************************************************
        ''Creado por:	  Edwin Poma
        ''Fecha     :      2010.01.19
        ''Proposito :      Procesa los porcentajes de distribución a ofisis
        ''*******************************************************************************************

        'Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        'Dim Conexion As AccesoDatosSQLServer

        'Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
        '                                "pint_periodoAno", mintPeriodoAno, _
        '                                "ptin_periodomes", mintPeriodoMes, _
        '                                "pchr_codigoplanta", mstrPlanta, _
        '                                "pvch_usuario", mstrUsuario}
        'Try
        '    mstrError = ""
        '    blnRpta = True
        '    Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
        '    'Conexion.EjecutarComando("usp_cos_distconsumocentrocosto_listar", objParametro)
        'Catch ex As Exception
        '    blnRpta = False
        '    mstrError = ex.Message
        'Finally
        '    Conexion = Nothing
        'End Try

        'Return blnRpta

    End Function

    Public Function ProcesarDistribucionVapor() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.22
        'Proposito :      Agrupa en C.C. los consumos de EE
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", "PPR", _
                                        "pchr_CodigoRecurso", "VA", _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_distconsumocentrocosto_procesar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarDistribucionVapor() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.22
        'Proposito :      limpia en C.C. los consumos de EE
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", "PPR", _
                                        "pchr_CodigoRecurso", "VA", _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_distconsumocentrocosto_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarDistribucionVaporxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.21
        'Proposito :      lista los porcentajes distribuidos en C.C.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_CodigoPlanta", "PPR", _
                                        "pchr_CodigoRecurso", "VA"}


        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_distconsumocentrocosto_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistribucionVaporxHistorico(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.11
        'Proposito :      lista los porcentajes de distribución de GLP x C.C., en un rango de 12 meses x columna y un promedio x C.C. al final 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_CodigoPlanta", "PPR", _
                                        "pchr_CodigoRecurso", "VA"}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_distconsumocentrocosto_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ObtenerEsquemas(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistConsumoVapor)
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

    Public Function CalcularConsumo(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Calcula los datos en base a los datos ingresados y a los datos de producción
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
            Conexion.EjecutarComando("usp_cos_unidadconsumodistvaporxperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarConsumo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de cierre
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodistvaporxperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirConsumo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodistvaporxperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarConsumo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de cierre del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodistvaporxperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class


