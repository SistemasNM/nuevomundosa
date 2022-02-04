Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsDistConsumoEEPlantaHilanderia
    Inherits clsDistConsumoEE

    Sub New()
        mstrPlanta = "PHI"
    End Sub

    Public Function ListarDatosxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna la lista de datos x periodo
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
            pDT = Conexion.ObtenerDataSet("usp_cos_unidadconsumodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDatosxPeriodoSinProcesar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna la lista de datos x periodo sin procesar
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
            pDT = Conexion.ObtenerDataSet("usp_cos_unidadconsumodisteexperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Overloads Function ProcesarDistribucionEE() As Boolean
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
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE", _
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

    Public Overloads Function LimpiarDistribucionEE() As Boolean
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
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE", _
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

    Public Overloads Function ListarDistribucionEExPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna la lista de los CC y sus % para su distribución a ofisis
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_CodigoPlanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE"}


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

    Public Function ListarDistribucionEExHistorico(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      lista los porcentajes de distribución de agua x C.C., en un rango de 12 meses x columna y un promedio x C.C. al final 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_CodigoPlanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE"}

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

    Public Function LimpiarDatosxPeriodo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      Limpia el cálculo de consumo de ee planta hilandería
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodisteexperiodo_limpiar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerDatosProduccion() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      Vuelve a traer los datos de produccion, despues debe ejecutar el boton cálcular
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodisteexperiodo_procesar2", objParametro)
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
        'Fecha     :      2010.01.19
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.DistConsumoEEPlantaHilanderia)
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
        'Fecha     :      2010.01.19
        'Proposito :      Calcula los datos en base a los datos ingresados y a los datos de producción
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodisteexperiodo_guardar", objParametro)
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
                                        "pchr_CodigoPlanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE", _
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
                                        "pchr_CodigoPlanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE", _
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

    Public Function DistribuirConsumoOfisis() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      Procesa los porcentajes de distribución a ofisis
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pchr_CodigoRecurso", "EE", _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_GeneraDocContable", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Overloads Function DistribuirConsumoGastos() As Boolean
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

    Public Function CerrarConsumo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Cierra el periodo de calculo de consumo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodisteexperiodo_cerrar", objParametro)
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
        'Fecha     :      2010.01.19
        'Proposito :      Abre el mes de calculo de consumo de ee
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrPlanta, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumodisteexperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class
