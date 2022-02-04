Option Explicit On
Imports System.Data
Imports NM.AccesoDatos


Public Class clsDistConsumoEEGrupo

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
    'Fecha     :      15-12-2009
    'Proposito :      Definir los metodos para el cálculo y distribucion del consumo de EE en grupos de cálculo, algunos los grupos se graban como consumo de soles utb_distconsumogrupoee y otros solo graban las horas netas utb_unidadconsumohorasefectivas, de ambos su llave principal es la unidad de consumo
    '                 9-bombas de agua, 8-trabajadores, 10-tipo telares, 11- maquinas pre-tejido
    '*******************************************************************************************

    '======== Metodos de Consulta ========

    '========================================================================================================================
    '=== INICIO: GRUPO DE CALCULO DE LOS GRUPOS PPR Y PHI 
    '========================================================================================================================

#Region "-- Grupo de cálculo --"

    Public Function ListarCalculoxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      retorna la lista de datos x periodo del grupo por trabajadores(3,4,5), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion por trabajadores -->"ListarDistribucionTrabajadoresxPeriodo"
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 7, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarCalculoxPeriodoSinProcesar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      retorna la lista de datos x periodo del grupo por trabajadores(3,4,5), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion por trabajadores -->"ListarDistribucionTrabajadoresxPeriodo"
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 8, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirCalculoLuwa(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Ejecuta el proceso de calculo y  del grupo por luwa(15), se calcula y se guarda los datos 
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalle.TableName = "lista"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 15, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function DistribuirCalculoIluminacion(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Ejecuta el proceso de calculo y  del grupo por iluminación(16), se calcula y se guarda los datos 
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalle.TableName = "lista"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 15, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarCalculo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de trabajadores tin_grupocalculo=8
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 0, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoCalculo() As Boolean
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
                                        "ptin_grupocalculo", 0, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoCalculo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 0, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemasLuwa(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistCalculoEExGLuwa)
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

    Public Function ListarCalculoPHIxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      retorna la lista de datos x periodo del grupo luwa(15) e iluminación(16), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion por luwa e iluminacion
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 9, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarCalculoPHIxPeriodoSinProcesar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      retorna la lista de datos x periodo del grupo luwa(15) e iluminación(16), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion por luwa e iluminacion
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 10, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function


    Public Function LimpiarCalculoPHI() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Limpia los datos de los grupos de calculo 15,16
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 15, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoCalculoPHI() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Ejecuta el proceso de cierre del grupo por trabajadores(15,16)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 15, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoCalculoPHI() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo de cálculo 15,16
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 15, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemasIluminacion(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistCalculoEExGIluminacion)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE CALCULO DE LOS GRUPOS PPR Y PHI 
    '========================================================================================================================

    '========================================================================================================================
    '=== INICIO: GRUPO DE TRABAJADORES 
    '========================================================================================================================

#Region "-- Grupo Trabajadores --"

    Public Function ListarCalculoTrabajadoresxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      retorna la lista de datos x periodo del grupo por trabajadores(3,4,5), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion por trabajadores -->"ListarDistribucionTrabajadoresxPeriodo"
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
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    

    Public Function ListarCalculoTrabajadoresxPeriodoSinProcesar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      retorna la lista de datos x periodo del grupo por trabajadores(3,4,5), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion por trabajadores -->"ListarDistribucionTrabajadoresxPeriodo"
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 5, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirCalculoTrabajadores(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de calculo y distribución del grupo por trabajadores(3,4,5), se calcula y se guarda los datos 
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalle.TableName = "lista"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 0, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarDistribucionTrabajadoresxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.10
        'Proposito :      Este metodo trae los datos distribuidos por cada C.C. (unidad de consumo), retorna dos listas de datos x periodo del grupo de trabajadores(8) las cuales son lista de distribucion x C.C. y lista de calculos por grupo de trabajadores (3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 3, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirConsumoTrabajadores() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de trabajadores tin_grupocalculo=8
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 8, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_procesar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarDistribucionTrabajadores() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de trabajadores tin_grupocalculo=8
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 8, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionTrabajadores() As Boolean
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
                                        "ptin_grupocalculo", 8, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionTrabajadores() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 8, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function ObtenerEsquemasTrabajadores(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistConsumoEExGTrabajadores)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE TRABAJADORES 
    '========================================================================================================================

    '========================================================================================================================
    '=== INICIO: GRUPO DE BOMBAS DE AGUA 
    '========================================================================================================================

#Region "-- Grupo Bombas de Agua --"

    Public Function ListarCalculoBombasAguaxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      retorna la lista de datos x periodo del grupo por bombas de agua(6,7), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion de bombas de agua
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
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarCalculoBombasAguaxPeriodoSinProcesar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      retorna la lista de datos x periodo del grupo por bombas de agua(6,7), para que se realize el cálculo, esto es requisito para que de aca tome los totales para la distribucion de bombas de agua
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 6, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirCalculoBombasAgua(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de calculo y distribución del grupo por bombas de agua(6,7), se calcula y se guarda los datos 
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalle.TableName = "lista"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 0, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarDistribucionBombasAguaxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.10
        'Proposito :      Este metodo trae los datos distribuidos por cada C.C. (unidad de consumo), retorna dos listas de datos x periodo del grupo de bombas de agua(9) las cuales son lista de distribucion x C.C. y lista de calculos por grupo de bombas de agua (6,7)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 4, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function LimpiarDistribucionBombasAgua() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de trabajadores tin_grupocalculo=8
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 9, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function DistribuirConsumoBombasAgua() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de bombas de agua tin_grupocalculo=9
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 9, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_procesar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionBombasAgua() As Boolean
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
                                        "ptin_grupocalculo", 9, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionBombasAgua() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 9, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemasBombasAgua(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistConsumoEExGBombaAgua)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE BOMBAS DE AGUA 
    '========================================================================================================================
    '========================================================================================================================
    '=== INICIO: GRUPO DE TIPO DE TELARES
    '========================================================================================================================

#Region "-- Grupo Tipo de Telares --"

    Public Function ListarDistribucionTiposTelarxPeriodo(ByRef pDS As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.19
        'Proposito :      Este metodo trae las horas de consumo de ee distribuidas por cada tipo de telar segun su ubicacion en planta (tipo telar =unidad de consumo), retorna dos listas de datos x periodo del grupo de tipo telar (10) las cuales son: lista de eficiencia por planta y lista de horas netas por tipos de telar y por planta 
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
            pDS = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirHorasTiposTelar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de trabajadores tin_grupocalculo=8
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 10, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_procesar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarDistribucionTiposTelar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de consumo de ee para el grupo de trabajadores tin_grupocalculo=8
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 10, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionTiposTelar() As Boolean
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
                                        "ptin_grupocalculo", 10, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionTiposTelar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 10, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarTiposTelar(ByRef pDS As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.19
        'Proposito :      Este metodo trae los tipos de telar
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pvch_codigo_tipo_maquina", "", _
                                        "pvch_familia_maquina", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            pDS = Conexion.ObtenerDataTable("usp_tej_maquinatipo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE TIPO DE TELARES
    '========================================================================================================================

    '========================================================================================================================
    '=== INICIO: GRUPO DE MAQUINAS DE PRE-TEJIDO
    '========================================================================================================================

#Region "-- Maquinas de PreTejido --"

    Public Function ListarDistribucionMaqPreTejidoxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.19
        'Proposito :      Este metodo trae las horas de consumo de ee distribuidas para cada maquina de pretejido, el grupo es el 11-Maquinas de Pre-tejido, si no existe datos crea los datos en base al mes anterior.
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
            pDT = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistribucionMaqPreTejidoSinProcesarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.19
        'Proposito :      Este metodo trae las horas de consumo de ee distribuidas para cada maquina de pretejido, el grupo es el 11-Maquinas de Pre-tejido, si no existe datos crea los datos en base al mes anterior.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 4, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirHorasMaqPreTejido(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de distribución de horas netas para el grupo de maquinas de pre-tejido
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 11, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarDistribucionMaqPreTejido() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de limpiar la distribución de horas netas para el grupo de maquinas de pre-tejido
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 11, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionMaqPreTejido() As Boolean
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
                                        "ptin_grupocalculo", 11, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionMaqPreTejido() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 11, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function ObtenerEsquemasMaqPreTejido(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistHorasxGMaqPretejido)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE MAQUINAS DE PRE-TEJIDO
    '========================================================================================================================

    '========================================================================================================================
    '=== INICIO: GRUPO DE HORAS ESTANDAR
    '========================================================================================================================

#Region "-- Horas Estandar --"

    Public Function ListarHorasEstandarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.19
        'Proposito :      Este metodo trae las horas de consumo de ee por grupo de horas estandar, el grupo es el 12-horas estandar tambien 3,4,5,6,7, si no existe datos crea los datos en base al mes anterior.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 3, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarHorasEstandarSinProcesarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.19
        'Proposito :      Este metodo trae las horas de consumo de ee por grupo de horas estandar, el grupo es el 12-horas estandar tambien 3,4,5,6,7, si no existe datos crea los datos en base al mes anterior.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 5, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function GuardarHorasEstandar(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de guardar las horas estandar para el grupo de horas estandar
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 12, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarHorasEstandar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de limpiar la distribución de horas estandar del grupo de horas estandar tin_grupocalculo=12 y 3,4,5,6,7
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 12, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionHorasEstandar() As Boolean
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
                                        "ptin_grupocalculo", 12, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionHorasEstandar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 12, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemasHorasEstandar(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.GuardarHorasxGHorasEstandar)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE HORAS ESTANDAR
    '========================================================================================================================

    '========================================================================================================================
    '=== INICIO: GRUPO DE LUWAS 
    '========================================================================================================================

#Region "-- Grupo de Luwas --"

    Public Function ListarDistribucionLuwasxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.08
        'Proposito :      Este metodo trae 4 listas(cabecera, maestro de porcentajes, consumo fijo, consumo variable)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 11, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistribucionLuwasSinProcesarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.08
        'Proposito :      Este metodo trae 4 listas(cabecera, maestro de porcentajes, consumo fijo, consumo variable)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 12, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirConsumoLuwas(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Ejecuta el proceso de calculo y  del grupo por luwa(15), se calcula y se guarda los datos 
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalle.TableName = "lista"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 17, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function LimpiarDistribucionLuwas() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Limpia los porcentajes y los datos distribuidos
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 17, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionLuwas() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de cierre del grupo de luwas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 17, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionLuwas() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo luwas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 17, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function ObtenerEsquemasDistribucionLuwas(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistConsumoEExGLuwa)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE LUWAS 
    '========================================================================================================================

    '========================================================================================================================
    '=== INICIO: GRUPO DE ILUMINACION 
    '========================================================================================================================

#Region "-- Grupo de Iluminación --"

    Public Function ListarDistribucionIluminacionxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.08
        'Proposito :      Este metodo trae 4 listas(cabecera, sin línea de producción, ring, open end)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 13, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDistribucionIluminacionSinProcesarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.08
        'Proposito :      Este metodo trae 4 listas(cabecera, maestro de porcentajes, consumo fijo, consumo variable)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 14, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumogrupodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function DistribuirConsumoIluminacion(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Ejecuta el proceso de calculo y  del grupo por iluminacion(18), se calcula y se guarda los datos 
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalle.TableName = "lista"

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 18, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function LimpiarDistribucionIluminacion() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Limpia los datos distribuidos
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 18, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoDistribucionIluminacion() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de cierre del grupo de iluminacion
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 18, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoDistribucionIluminacion() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo luwas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 18, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumogrupodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function ObtenerEsquemasDistribucionIluminacion(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.07
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistConsumoEExGIluminacion)
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

#End Region

    '========================================================================================================================
    '=== FINAL: GRUPO DE ILUMINACION 
    '========================================================================================================================


#Region "-- Horas Maquina Hilandería --"

    Public Function ListarHorasMaquinaPHIxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.15
        'Proposito :      Este metodo trae las horas de máquinas de hilandería, si no existe datos crea los datos en base al mes anterior.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 6, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarHorasMaquinaPHISinProcesarxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.15
        'Proposito :      Este metodo trae las horas de máquinas de hilandería, si no existe datos no crea nada. solo lista
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 7, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_consumohorasefecxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function GuardarHorasMaquinaPHI(ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.15
        'Proposito :      Ejecuta el proceso de guardar las horas máquina de hilandería.
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "ptin_grupocalculo", 19, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarHorasMaquinaPHI() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.15
        'Proposito :      Ejecuta el proceso de limpiar las  horas máquina de hilandería.
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 19, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarPeriodoHorasMaquinaPHI() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.15
        'Proposito :      Ejecuta el proceso de cierre de horas máquina de hilandería.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 19, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirPeriodoHorasMaquinaPHI() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir de horas máquina de hilandería.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_grupocalculo", 19, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_consumohorasefecxperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemasHorasMaquinaPHI(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.GuardarHorasxGHorasMaquinaHil)
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

#End Region

End Class
