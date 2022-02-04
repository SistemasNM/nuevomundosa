Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsDatosProduccion

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrEmpresa As String = ""
    Dim mintPeriodoAno As Integer = 0
    Dim mintPeriodoMes As Int16 = 0
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

    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal sCad As String)
            mstrUsuario = sCad
        End Set
    End Property

#End Region

#Region "-- Metodos --"

#Region "-- Trabajadores x Centro de Costo --"

    Public Function ProcesarTrabajadorxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los trabajadores por centro de costo
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pint_PeriodoAno", mintPeriodoAno, _
                                        "pint_PeriodoMes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_centrocostoxtrabajador_actualizar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ListarTrabajadorxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.26
        'Proposito :      Ejecuta el proceso de importar los datos de eficiencia por planta de los telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_centrocostoxtrabajador_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarTrabajadorxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 4, _
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

    Public Function AbrirTrabajadorxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 4, _
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

    Public Function LimpiarTrabajadorxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de limpiar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 4, _
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

#End Region

#Region "-- Producción de Tintorería --"

    Public Function ProcesarProdTintoreriaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de producción de tintorería
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes, _
                                        "Co_Usu", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproducciontintoreria_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ListarProdTintoreriaxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de eficiencia por planta de los telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproducciontintoreria_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarProdTintoreriaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 1, _
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

    Public Function AbrirProdTintoreriaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 1, _
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

    Public Function LimpiarProdTintoreriaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de limpiar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 1, _
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

#End Region

#Region "-- Eficiencia de Telares --"

    Public Function ProcesarEficienciaTelaresxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de eficiencia por planta de los telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes, _
                                        "Co_Usua", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccionEficiencia_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarEficienciaTelaresxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de eficiencia por planta de los telares, 3-eficiencia de telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 3, _
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

    Public Function AbrirEficienciaTelaresxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de eficiencia por planta de los telares, 3-eficiencia de telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 3, _
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

    Public Function LimpiarEficienciaTelaresxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de eficiencia por planta de los telares, 3-eficiencia de telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 3, _
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

    Public Function ListarEficienciaTelaresxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de eficiencia por planta de los telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproduccionEficiencia_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

#End Region

#Region "-- Producción de Hilandería --"

    Public Function ProcesarProdHilanderiaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de produccion de hilanderia
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes, _
                                        "Co_Usua", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccionHilanderia_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ListarProdHilanderiaxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de eficiencia por planta de los telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproduccionHilanderia_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarProdHilanderiaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de eficiencia por planta de los telares, 3-eficiencia de telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 5, _
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

    Public Function AbrirProdHilanderiaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de eficiencia por planta de los telares, 3-eficiencia de telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 5, _
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

    Public Function LimpiarProdHilanderiaxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de eficiencia por planta de los telares, 3-eficiencia de telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 5, _
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

#End Region

#Region "-- Producción de Tejeduría(Urdido y Engomado) --"

    Public Function ProcesarProdUrdEngxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de produccion de hilanderia
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes, _
                                        "Co_Usua", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccionUrdEng_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function


    Public Function ListarProdUrdEngxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de eficiencia por planta de los telares
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproduccionUrdEng_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarProdUrdEngxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 2, _
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

    Public Function AbrirProdUrdEngxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 2, _
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

    Public Function LimpiarProdUrdEngxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de limpiar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 2, _
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

#End Region

#Region "-- Gastos de Contabilidad --"

    Public Function ProcesarGastosContabilidad() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar los datos de produccion de hilanderia
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproducciongastos_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ListarGastosContabilidad(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Lista los datos importados de gastos de contabilidad
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 3, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproducciongastos_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarGastosContabilidad() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproducciongastos_cerrar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function AbrirGastosContabilidad() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de tejeduría(urdido y engomado)
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproducciongastos_abrir", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function LimpiarGastosContabilidad() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de limpiar los datos de gastos de contabilidad
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproducciongastos_limpiar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

#End Region

#Region "-- Gastos de Planillas --"

    Public Function ProcesarGastosPlanilla() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.16
        'Proposito :      Ejecuta el proceso de importar los gastos de planilla
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"Co_Empr", mstrEmpresa, _
                                        "Nu_Anno", mintPeriodoAno, _
                                        "Nu_Mes", mintPeriodoMes, _
                                        "Co_Usua", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_distconcumoCentroCostoMO_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    'Public Function ListarGastosContabilidad(ByRef pDT As DataSet) As Boolean
    '    '************************************************  
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      2009.12.03
    '    'Proposito :      Lista los datos importados de gastos de contabilidad
    '    '************************************************  
    '    Dim blnRpta As Boolean
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoano", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes}
    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        pDT = Conexion.ObtenerDataSet("usp_cos_datosproducciongastos_listar", objParametro)

    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return blnRpta
    'End Function

    'Public Function CerrarGastosContabilidad() As Boolean
    '    '************************************************  
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      2009.12.03
    '    'Proposito :      Ejecuta el proceso de cerrar los datos de tejeduría(urdido y engomado)
    '    '************************************************  
    '    Dim blnRpta As Boolean
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoano", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes, _
    '                                    "pvch_usuario", mstrUsuario}

    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        Conexion.EjecutarComando("usp_cos_datosproducciongastos_cerrar", objParametro)

    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return blnRpta
    'End Function

    'Public Function AbrirGastosContabilidad() As Boolean
    '    '************************************************  
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      2009.12.03
    '    'Proposito :      Ejecuta el proceso de abrir los datos de tejeduría(urdido y engomado)
    '    '************************************************  
    '    Dim blnRpta As Boolean
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoano", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes, _
    '                                    "pvch_usuario", mstrUsuario}

    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        Conexion.EjecutarComando("usp_cos_datosproducciongastos_abrir", objParametro)

    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return blnRpta
    'End Function

    'Public Function LimpiarGastosContabilidad() As Boolean
    '    '************************************************  
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      2009.12.03
    '    'Proposito :      Ejecuta el proceso de limpiar los datos de gastos de contabilidad
    '    '************************************************  
    '    Dim blnRpta As Boolean
    '    Dim Conexion As AccesoDatosSQLServer
    '    Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoano", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes, _
    '                                    "pvch_usuario", mstrUsuario}

    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        Conexion.EjecutarComando("usp_cos_datosproducciongastos_limpiar", objParametro)

    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try
    '    Return blnRpta
    'End Function

#End Region

#Region "-- Ingreso de Hilos al almacén de hilos --"

    Public Function ProcesarProdIngHilosxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.24
        'Proposito :      Ejecuta el proceso de importar los hilos ingresados a almacén
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccioningresohilo_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ListarProdIngHilosxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.24
        'Proposito :      Ejecuta el proceso de importar los ingresos de hilos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproduccioningresohilo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarProdIngHilosxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de cerrar los datos de ingreso de hilos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 8, _
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

    Public Function AbrirProdIngHilosxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de abrir los datos de ingreso de hilos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 8, _
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

    Public Function LimpiarProdIngHilosxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de limpiar los datos ingreso de hilos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 8, _
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

#End Region

#Region "-- Consumo de Materia Prima --"

    Public Function ProcesarProdConsumoMPxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.24
        'Proposito :      Ejecuta el proceso de importar el consumo de MP
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_datosproduccionconsumomp_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function ListarProdConsumoMPxPeriodo(ByRef pDT As DataSet) As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.24
        'Proposito :      Ejecuta el proceso de importar el consumo de MP
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_datosproduccionconsumomp_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function CerrarProdConsumoMPxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar el consumo de MP
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 9, _
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

    Public Function AbrirProdConsumoMPxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de importar el consumo de MP
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 9, _
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

    Public Function LimpiarProdConsumoMPxPeriodo() As Boolean
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.03
        'Proposito :      Ejecuta el proceso de limpiar la importación del consumo de MP
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 9, _
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

#End Region

    Public Function ListarTablasCargadas(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Lista las tablas maestras con la ultima cargar realizada
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_DatosProduccion_Listar", objParametro)

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
