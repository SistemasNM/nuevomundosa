Option Explicit On
Imports System.Data
Imports NM.AccesoDatos


Public Class clsTejUrdimbre
#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrUsuario As String = ""

    Dim mnumPeriodo As Double = 0
    Dim mstrEmpresa As String = ""

    Dim mstrCodigoUrdimbre As String = ""
    Dim mintRevisionUrdimbre As Integer = 0
    Dim mintSecuencia As Integer = 0
    Dim mintTipo As Integer = 0
    Dim mstrCodigoHilo As String = ""
    Dim mstrCodigoHiloNuevo As String = ""
    Dim mdblCantidadHilos As Double = 0
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

    Public Property CodigoUrdimbre() As String
        Get
            CodigoUrdimbre = mstrCodigoUrdimbre
        End Get
        Set(ByVal sCad As String)
            mstrCodigoUrdimbre = sCad
        End Set
    End Property

    Public Property RevisionUrdimbre() As Integer
        Get
            RevisionUrdimbre = mintRevisionUrdimbre
        End Get
        Set(ByVal iVal As Integer)
            mintRevisionUrdimbre = iVal
        End Set
    End Property

    Public Property Secuencia() As Integer
        Get
            Secuencia = mintSecuencia
        End Get
        Set(ByVal iVal As Integer)
            mintSecuencia = iVal
        End Set
    End Property

    Public Property Tipo() As Integer
        Get
            Tipo = mintTipo
        End Get
        Set(ByVal iVal As Integer)
            mintTipo = iVal
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

    Public Property CodigoHiloNuevo() As String
        Get
            CodigoHiloNuevo = mstrCodigoHiloNuevo
        End Get
        Set(ByVal strCad As String)
            mstrCodigoHiloNuevo = strCad
        End Set
    End Property

    Public Property CantidadHilos() As Double
        Get
            CantidadHilos = mdblCantidadHilos
        End Get
        Set(ByVal numVal As Double)
            mdblCantidadHilos = numVal
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
        'Proposito :      retorna la lista de datos x periodo del urdimbre
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Codigoempresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosUrdimbre}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_Urdimbre_Consultar", objParametro)

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
        'Proposito :      modifica datos de la urdimbre
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_RevisionUrdimbre", mintRevisionUrdimbre, _
                                        "pint_Secuencia", mintSecuencia, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_CodigoNuevo", mstrCodigoHiloNuevo, _
                                        "pnum_CantidadHilos", mdblCantidadHilos, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Urdimbre_Actualizar", objParametro)

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
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosUrdimbre, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Urdimbre_Importar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    ' Elimina datos de urdimbre
    Public Function EliminarUrdimbre() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_RevisionUrdimbre", mintRevisionUrdimbre}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Urdimbre_Eliminar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Agregar datos de urdimbre
    Public Function AgregarUrdimbre() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_RevisionUrdimbre", mintRevisionUrdimbre, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Urdimbre_Agregar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Verifica existencia de urdimbre con su revision
    Public Function ListaUrdimbre(ByRef dstUrdimbre As DataSet) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_RevisionUrdimbre", mintRevisionUrdimbre}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstUrdimbre = Conexion.ObtenerDataSet("usp_costej_Urdimbre_Listar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function
End Class
