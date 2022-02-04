Option Explicit On
Imports System.Data
Imports NM.AccesoDatos


Public Class clsTejHilo

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrUsuario As String = ""

    Dim mnumPeriodo As Double = 0
    Dim mstrEmpresa As String = ""
    Dim mstrCodigoHilo As String = ""
    Dim mstrCodigoFamilia As String = ""
    Dim mstrTipoHilo As String = ""
    Dim mnumTituloReal As Double = 0
    Dim mnumPesoHilo As Double = 0


#End Region

    '================================= Definición de constructores ===============================

    '================================= Definición de Propiedades =================================

#Region "-- Propiedades --"



    Public Property Periodo() As Double
        Get
            Periodo = mnumPeriodo
        End Get
        Set(ByVal numVal As Double)
            mnumPeriodo = numVal
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


    Public Property CodigoHilo() As String
        Get
            CodigoHilo = mstrCodigoHilo
        End Get
        Set(ByVal strCad As String)
            mstrCodigoHilo = strCad
        End Set
    End Property

    Public Property CodigoFamilia() As String
        Get
            CodigoFamilia = mstrCodigoFamilia
        End Get
        Set(ByVal strCad As String)
            mstrCodigoFamilia = strCad
        End Set
    End Property

    Public Property TipoHilo() As String
        Get
            TipoHilo = mstrTipoHilo
        End Get
        Set(ByVal strCad As String)
            mstrTipoHilo = strCad
        End Set
    End Property

    Public Property TituloReal() As Double
        Get
            TituloReal = mnumTituloReal
        End Get
        Set(ByVal numVal As Double)
            mnumTituloReal = numVal
        End Set
    End Property

    Public Property PesoHilo() As Double
        Get
            PesoHilo = mnumPesoHilo
        End Get
        Set(ByVal numVal As Double)
            mnumPesoHilo = numVal
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

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================

    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de datos x periodo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoFamilia", mstrCodigoFamilia, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosHilos}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_PesoCono_Listar", objParametro)

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
        'Creado por:	  cponce
        'Fecha     :      2011.08.04
        'Proposito :      registra un nuevo hilo con su familia
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_TipoHilo", mstrTipoHilo, _
                                        "pnum_TituloReal", mnumTituloReal, _
                                        "pnum_PesoHilo", mnumPesoHilo, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PesoCono_Agregar", objParametro)

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
        'Creado por:	  cponce
        'Fecha     :      2011.08.04
        'Proposito :      modifica datos del hilo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "pvch_CodigoFamilia", mstrCodigoFamilia, _
                                        "pnum_TituloReal", mnumTituloReal, _
                                        "pnum_PesoHilo", mnumPesoHilo, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PesoCono_Actualizar", objParametro)

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
        'Creado por:	  cponce
        'Fecha     :      2011.08.04
        'Proposito :      modifica datos del hilo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "pvch_CodigoHilo", mstrCodigoHilo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PesoCono_Eliminar", objParametro)

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
        'Creado por:	  cponce
        'Fecha     :      2011.08.04
        'Proposito :      registra un nuevo hilo con su familia
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", clsTejEstadoProceso.Modulo.Mod_CostosHiloTela, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosHilos, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PesoCono_Importar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarValidacion(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de los hilos validados (hilos : inventario , en transito)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_PesoCono_Validar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function


    Public Function ListarUrdimbreValidacion(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de los hilos validados (hilos : urdimbre)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_Urdimbre_Validar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ConsultaFamiliaHilo(ByRef dstFamilia As DataSet, ByVal mstrHilo As String) As Boolean
        '*********************************************************
        'Creado por:	  atorres
        'Fecha     :      14-12-2011
        'Proposito :      retorna la peso y titulo de la familia
        '*********************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Hilo", mstrHilo}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstFamilia = Conexion.ObtenerDataSet("usp_costej_PesoCono_Familia", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function NumeroHilosModificados(ByRef dstFamilia As DataSet, ByVal mstrFamilia As String) As Boolean
        '*********************************************************
        'Creado por:	  atorres
        'Fecha     :      14-12-2011
        'Proposito :      retorna numero de hilos modificados
        '*********************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Familia", mstrFamilia}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstFamilia = Conexion.ObtenerDataSet("usp_costej_PesoCono_HilosActualizados", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

End Class
