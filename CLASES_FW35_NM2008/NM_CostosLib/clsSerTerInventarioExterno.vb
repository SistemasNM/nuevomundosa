Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsSerTerInventarioExterno


#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mintPeriodoAno As Integer = 0
    Dim mintPeriodoMes As Int16 = 0
    Dim mstrEmpresa As String = ""
    Dim mstrEstado As String = ""
    Dim mstrUsuario As String = ""

    Dim mintSecuencia As Integer = 0
    Dim mstrCodigoServicio As String = ""
    Dim mstrCodigoHiloO As String = ""
    Dim mstrCodigoHiloD As String = ""
    Dim mdblProdEnvKg As Double = 0
    Dim mdblProdRecKg As Double = 0
    Dim mdblInvFinKg As Double = 0

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

    Public Property CodigoServicio() As String
        Get
            CodigoServicio = mstrCodigoServicio
        End Get
        Set(ByVal strCad As String)
            mstrCodigoServicio = strCad
        End Set
    End Property

    Public Property CodigoHiloO() As String
        Get
            CodigoHiloO = mstrCodigoHiloO
        End Get
        Set(ByVal strCad As String)
            mstrCodigoHiloO = strCad
        End Set
    End Property

    Public Property CodigoHiloD() As String
        Get
            CodigoHiloD = mstrCodigoHiloD
        End Get
        Set(ByVal strCad As String)
            mstrCodigoHiloD = strCad
        End Set
    End Property

    Public Property Secuencia() As Integer
        Get
            Secuencia = mintSecuencia
        End Get
        Set(ByVal intVal As Integer)
            mintSecuencia = intVal
        End Set
    End Property

    Public Property ProdEnvKg() As Double
        Get
            ProdEnvKg = mdblProdEnvKg
        End Get
        Set(ByVal intVal As Double)
            mdblProdEnvKg = intVal
        End Set
    End Property

    Public Property ProdRecKg() As Double
        Get
            ProdRecKg = mdblProdRecKg
        End Get
        Set(ByVal intVal As Double)
            mdblProdRecKg = intVal
        End Set
    End Property

    Public Property InvFinKg() As Double
        Get
            InvFinKg = mdblInvFinKg
        End Get
        Set(ByVal intVal As Double)
            mdblInvFinKg = intVal
        End Set
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================
    '*******************************************************************************************
    'Creado por:	  Edwin Poma
    'Fecha     :      12-08-2010
    'Proposito :      Definir los metodos para la transferencia de de cinta carda de linea anillos a OE
    '*******************************************************************************************

    '======== Metodos de Consulta ========

    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      12-08-2010
        'Proposito :      retorna la lista de datos x periodo
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
            pDT = Conexion.ObtenerDataSet("usp_cos_serterinvexterno_listar", objParametro)

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
        'Fecha     :      12-08-2010
        'Proposito :      retorna la lista de datos x periodo, pero no proceso la informacion
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
            pDT = Conexion.ObtenerDataSet("usp_cos_serterinvexterno_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function LimpiarDatos() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.06.11
        'Proposito :      limpia los datos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 21, _
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

    Public Function CerrarDatos() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      11-06-2010
        'Proposito :      Cierra el mes 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 21, _
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

    Public Function AbrirDatos() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      11-06-2010
        'Proposito :      Abrir el mes 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "ptin_tipodatoprod", 21, _
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

    'Public Function GuardarDatos(ByVal pdtDetalle As DataTable) As Boolean
    '    '*******************************************************************************************
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      2010.03.23
    '    'Proposito :      Guarda los datos de las velocidades de hilos x máquina
    '    '*******************************************************************************************

    '    Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
    '    Dim Conexion As AccesoDatosSQLServer
    '    pdtDetalle.TableName = "lista"
    '    Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
    '                                    "pint_periodoano", mintPeriodoAno, _
    '                                    "ptin_periodomes", mintPeriodoMes, _
    '                                    "ptin_codigolinea", mintLinea, _
    '                                    "pvch_usuario", mstrUsuario, _
    '                                    "pnvc_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
    '    clsUtilitario = Nothing
    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
    '        Conexion.EjecutarComando("usp_cos_hilovelocidad_guardar", objParametro)
    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally
    '        Conexion = Nothing
    '    End Try

    '    Return blnRpta

    'End Function

    Public Function Insertar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.08.12
        'Proposito :      inserta los datos del inventario externo(servicio de terceros)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pint_Secuencia", mintSecuencia, _
                                        "pchr_CodigoServicio", mstrCodigoServicio, _
                                        "pvch_CodigoHiloO", mstrCodigoHiloO, _
                                        "pvch_CodigoHiloD", mstrCodigoHiloD, _
                                        "pnum_ProdEnvKg", mdblProdEnvKg, _
                                        "pnum_ProdRecKg", mdblProdRecKg, _
                                        "pnum_InvFinKg", mdblInvFinKg, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_serterinvexterno_actualizar", objParametro)

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
        'Fecha     :      2010.08.12
        'Proposito :      actualiza el inventario externo(servicio de terceros)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pint_Secuencia", mintSecuencia, _
                                        "pchr_CodigoServicio", mstrCodigoServicio, _
                                        "pvch_CodigoHiloO", mstrCodigoHiloO, _
                                        "pvch_CodigoHiloD", mstrCodigoHiloD, _
                                        "pnum_ProdEnvKg", mdblProdEnvKg, _
                                        "pnum_ProdRecKg", mdblProdRecKg, _
                                        "pnum_InvFinKg", mdblInvFinKg, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_serterinvexterno_actualizar", objParametro)
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
        'Fecha     :      2010.08.12
        'Proposito :      elimina el inventario externo (servicios de terceros)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 3, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoano", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pint_Secuencia", mintSecuencia, _
                                        "pchr_CodigoServicio", mstrCodigoServicio, _
                                        "pvch_CodigoHiloO", mstrCodigoHiloO, _
                                        "pvch_CodigoHiloD", mstrCodigoHiloD, _
                                        "pnum_ProdEnvKg", mdblProdEnvKg, _
                                        "pnum_ProdRecKg", mdblProdRecKg, _
                                        "pnum_InvFinKg", mdblInvFinKg, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_serterinvexterno_actualizar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    'Public Function ObtenerEsquemas(ByRef pdtDetalle As DataTable) As Boolean
    '    '*******************************************************************************************
    '    'Creado por:	  Edwin Poma
    '    'Fecha     :      11-06-2010
    '    'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
    '    '*******************************************************************************************
    '    Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas

    '    pdtDetalle.TableName = "lista"

    '    blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.GuardarTransfCardaRingOE)
    '    If blnRpta = True Then
    '        pdtDetalle = pDT.Tables(0)
    '    End If

    '    'tin_CodigoConcepto	    as [c1],
    '    'num_TransferenciaKg	as [c2]

    '    Try
    '        mstrError = ""
    '        blnRpta = True
    '    Catch ex As Exception
    '        blnRpta = False
    '        mstrError = ex.Message
    '    Finally

    '    End Try

    '    Return blnRpta

    'End Function

End Class
