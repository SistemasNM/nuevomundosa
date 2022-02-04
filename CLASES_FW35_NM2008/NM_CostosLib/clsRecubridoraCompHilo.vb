Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsRecubridoraCompHilo

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrEmpresa As String = ""
    Dim mintPeriodoAno As Integer = 0
    Dim mintPeriodoMes As Int16 = 0
    Dim mstrUsuario As String = ""

    Dim mstrTitulo As String = ""
    Dim mstrCodigoHilo As String = ""
    Dim mintSpandexDenier As Integer = 0
    Dim mstrCodigoSpandex As String = ""
    Dim mdblSpandex40DT As Double = 0
    Dim mdblSpandex70DT As Double = 0
    Dim mdblSpandex105D As Double = 0
    Dim mdblSpandex140D As Double = 0
    Dim mdblPolyester As Double = 0
    Dim mdblPolyesterCrudo As Double = 0
    Dim mdblPolyesterNegro As Double = 0
    Dim mdblPolyesterColor As Double = 0

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

    Public Property Titulo() As String
        Get
            Titulo = mstrTitulo
        End Get
        Set(ByVal strCad As String)
            mstrTitulo = strCad
        End Set
    End Property

    Public Property CodigoHilo() As String
        Get
            CodigoHilo = mstrCodigoHilo
        End Get
        Set(ByVal strCad As String)
            mstrCodigoHilo = strCad
        End Set
    End Property

    Public Property SpandexDenier() As Integer
        Get
            SpandexDenier = mintSpandexDenier
        End Get
        Set(ByVal intVal As Integer)
            mintSpandexDenier = intVal
        End Set
    End Property

    Public Property CodigoSpandex() As String
        Get
            CodigoSpandex = mstrCodigoSpandex
        End Get
        Set(ByVal strCad As String)
            mstrCodigoSpandex = strCad
        End Set
    End Property

    Public Property Spandex40DT() As Double
        Get
            Spandex40DT = mdblSpandex40DT
        End Get
        Set(ByVal intVal As Double)
            mdblSpandex40DT = intVal
        End Set
    End Property

    Public Property Spandex70DT() As Double
        Get
            Spandex70DT = mdblSpandex70DT
        End Get
        Set(ByVal intVal As Double)
            mdblSpandex70DT = intVal
        End Set
    End Property

    Public Property Spandex105D() As Double
        Get
            Spandex105D = mdblSpandex105D
        End Get
        Set(ByVal intVal As Double)
            mdblSpandex105D = intVal
        End Set
    End Property

    Public Property Spandex140D() As Double
        Get
            Spandex140D = mdblSpandex140D
        End Get
        Set(ByVal intVal As Double)
            mdblSpandex140D = intVal
        End Set
    End Property

    Public Property Polyester() As Double
        Get
            Polyester = mdblPolyester
        End Get
        Set(ByVal intVal As Double)
            mdblPolyester = intVal
        End Set
    End Property

    Public Property PolyesterCrudo() As Double
        Get
            PolyesterCrudo = mdblPolyesterCrudo
        End Get
        Set(ByVal intVal As Double)
            mdblPolyesterCrudo = intVal
        End Set
    End Property

    Public Property PolyesterNegro() As Double
        Get
            PolyesterNegro = mdblPolyesterNegro
        End Get
        Set(ByVal intVal As Double)
            mdblPolyesterNegro = intVal
        End Set
    End Property

    Public Property PolyesterColor() As Double
        Get
            PolyesterColor = mdblPolyesterColor
        End Get
        Set(ByVal intVal As Double)
            mdblPolyesterColor = intVal
        End Set
    End Property

#End Region

#Region "-- Metodos --"

    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.23
        'Proposito :      retorna 2 consultas del composición de hilo de la recubridora lista de cabecera, lista de detalle
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
            pDT = Conexion.ObtenerDataSet("usp_cos_recubridoracomphilo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarDatosSinProcesar(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.23
        'Proposito :      retorna 2 consultas del composición de hilo de la recubridora lista de cabecera, lista de detalle
        '*******************************************************************************************

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
            pDT = Conexion.ObtenerDataSet("usp_cos_recubridoracomphilo_listar", objParametro)

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
        'Fecha     :      2010.03.23
        'Proposito :      Guarda los datos de composición de hilos de la recubridora
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pnvc_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_recubridoracomphilo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Insertar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.04.07
        'Proposito :      inserta los datos de la composicion de los hilos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_Titulo", mstrTitulo, _
                                        "pint_SpandexDenier", mintSpandexDenier, _
                                        "pvch_CodigoSpandex", mstrCodigoSpandex, _
                                        "pnum_Spandex40DT", mdblSpandex40DT, _
                                        "pnum_Spandex70DT", mdblSpandex70DT, _
                                        "pnum_Spandex105D", mdblSpandex105D, _
                                        "pnum_Spandex140D", mdblSpandex140D, _
                                        "pnum_Polyester", mdblPolyester, _
                                        "pnum_PolyesterCrudo", mdblPolyesterCrudo, _
                                        "pnum_PolyesterNegro", mdblPolyesterNegro, _
                                        "pnum_PolyesterColor", mdblPolyesterColor, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_recubridoracomphilo_actualizar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Modificar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.04.07
        'Proposito :      modifica los datos de la composicion de los hilos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_Titulo", mstrTitulo, _
                                        "pint_SpandexDenier", mintSpandexDenier, _
                                        "pvch_CodigoSpandex", mstrCodigoSpandex, _
                                        "pnum_Spandex40DT", mdblSpandex40DT, _
                                        "pnum_Spandex70DT", mdblSpandex70DT, _
                                        "pnum_Spandex105D", mdblSpandex105D, _
                                        "pnum_Spandex140D", mdblSpandex140D, _
                                        "pnum_Polyester", mdblPolyester, _
                                        "pnum_PolyesterCrudo", mdblPolyesterCrudo, _
                                        "pnum_PolyesterNegro", mdblPolyesterNegro, _
                                        "pnum_PolyesterColor", mdblPolyesterColor, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_recubridoracomphilo_actualizar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function Eliminar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.03.23
        'Proposito :      elimina los datos de la composicion de los hilos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 3, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_Titulo", mstrTitulo, _
                                        "pint_SpandexDenier", mintSpandexDenier, _
                                        "pvch_CodigoSpandex", mstrCodigoSpandex, _
                                        "pnum_Spandex40DT", mdblSpandex40DT, _
                                        "pnum_Spandex70DT", mdblSpandex70DT, _
                                        "pnum_Spandex105D", mdblSpandex105D, _
                                        "pnum_Spandex140D", mdblSpandex140D, _
                                        "pnum_Polyester", mdblPolyester, _
                                        "pnum_PolyesterCrudo", mdblPolyesterCrudo, _
                                        "pnum_PolyesterNegro", mdblPolyesterNegro, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_recubridoracomphilo_actualizar", objParametro)
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
        'Fecha     :      2010.03.23
        'Proposito :      Ejecuta el proceso de cerrar los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 7, _
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
        'Fecha     :      2010.03.23
        'Proposito :      Ejecuta el proceso de abrir los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 7, _
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
        'Fecha     :      2010.03.23
        'Proposito :      Ejecuta el proceso de limpiar los datos
        '************************************************  
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 7, _
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

End Class
