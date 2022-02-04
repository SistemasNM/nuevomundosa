Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejProdEngomado


#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String
    Dim mstrUsuario As String = ""

    Dim mnumPeriodo As Double = 0
    Dim mstrEmpresa As String = ""

    Dim mstrCodigoPartidaEngomado As String = ""
    Dim mstrCodigoEngomado As String = ""
    Dim mintRevisionEngomado As Integer = 0
    Dim mstrCodigoTED As String = ""
    Dim mintRevisionTED As Integer = 0
    Dim mstrCodigoUrdimbre As String = ""
    Dim mintRevisionUrdimbre As Integer = 0


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


    Public Property CodigoPartidaEngomado() As String
        Get
            CodigoPartidaEngomado = mstrCodigoPartidaEngomado

        End Get
        Set(ByVal strCad As String)
            mstrCodigoPartidaEngomado = strCad
        End Set
    End Property

    Public Property CodigoEngomado() As String
        Get
            CodigoEngomado = mstrCodigoEngomado
        End Get
        Set(ByVal strCad As String)
            mstrCodigoEngomado = strCad
        End Set
    End Property

    Public Property RevisionEngomado() As Integer
        Get
            RevisionEngomado = mintRevisionEngomado
        End Get
        Set(ByVal numVal As Integer)
            mintRevisionEngomado = numVal
        End Set
    End Property

    Public Property CodigoTED() As String
        Get
            CodigoTED = mstrCodigoTED
        End Get
        Set(ByVal strCad As String)
            mstrCodigoTED = strCad
        End Set
    End Property

    Public Property RevisionTED() As Integer
        Get
            RevisionTED = mintRevisionTED
        End Get
        Set(ByVal numVal As Integer)
            mintRevisionTED = numVal
        End Set
    End Property

    Public Property CodigoUrdimbre() As String
        Get
            CodigoUrdimbre = mstrCodigoUrdimbre
        End Get
        Set(ByVal strCad As String)
            mstrCodigoUrdimbre = strCad
        End Set
    End Property

    Public Property RevisionUrdimbre() As Integer
        Get
            RevisionUrdimbre = mintRevisionUrdimbre
        End Get
        Set(ByVal numVal As Integer)
            mintRevisionUrdimbre = numVal
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
        'Fecha     :      2011.08.26
        'Proposito :      retorna la lista de datos x periodo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.DatGen_DatosProdEngomado}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_PartidasEngomado_Consultar", objParametro)

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
        'Fecha     :      2011.08.26
        'Proposito :      modifica datos del hilo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pnum_periodo", mnumPeriodo, _
                                        "pvch_CodigoPartidaEngomado", mstrCodigoPartidaEngomado, _
                                        "pvch_CodigoEngomado", mstrCodigoEngomado, _
                                        "pint_RevisionEngomado", mintRevisionEngomado, _
                                        "pvch_CodigoTED", mstrCodigoTED, _
                                        "pint_RevisionTED", mintRevisionTED, _
                                        "pvch_CodigoUrdimbre", mstrCodigoUrdimbre, _
                                        "pint_RevisionUrdimbre", mintRevisionUrdimbre, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PartidasEngomado_Actualizar", objParametro)

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
        'Fecha     :      2011.08.26
        'Proposito :      registra un nuevo hilo con su familia
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Modulo", clsTejEstadoProceso.Modulo.Mod_DatosGenerales, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.DatGen_DatosProdEngomado, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PartidasEngomado_Importar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    ' Modificacion
    ' Verifica si se realizo la importacion de la produccion de engomado
    Public Function fncConsultarProdEngomado(ByRef dstProdEngomado As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double) As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstProdEngomado = Conexion.ObtenerDataSet("usp_costej_PartidasEngomado_Reporte", objParametros)
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
