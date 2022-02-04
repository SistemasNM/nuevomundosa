Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejDistHorasTrab

#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodigoEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String

#End Region

#Region "-- Propiedades --"


    Public Property Periodo() As Double
        Get
            Periodo = mnumPeriodo
        End Get
        Set(ByVal numVal As Double)
            mnumPeriodo = numVal
        End Set
    End Property


    Public Property CodigoEmpresa() As String
        Get
            CodigoEmpresa = mstrCodigoEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrCodigoEmpresa = sCad
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
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

    '-------------------- Definicion de Metodos/Funciones  --------------------


    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      03-04-2012
        'Proposito :      retorna la lista de distribucion de horas trabajadas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_CosTej_DistHorasTrab_Listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarEsquema(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      03-04-2012
        'Proposito :      retorna el esquema de distribucion de horas trabajadas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim dtsDet As DataSet
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dtsDet = Conexion.ObtenerDataSet("usp_CosTej_DistHorasTrab_Esquema", objParametro)
            pDT = dtsDet.Tables(0)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function


    Public Function ImportarDatos() As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-04-2012
        'Proposito :      importa la distribucion de horas trabajadas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_Empresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_DistHorasTrabajadas_Procesar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function GuardarDatos(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      04.04.2012
        'Proposito :      Permite actualizar los datos de la distribucion de horas trab.
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_CosTej_DistHorasTrab_Guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    ' Modificado
    ' Importamos Eficiencia por articulo por planta
    Public Function DistribucionHoras_Actualiza() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_DistHorasTrabajadas_ReProc", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Importamos Eficiencia por articulo por planta desde articulo
    Public Function DistribucionHoras_Eficiencia() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_DistHorasTrab_ActEfi", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Importamos Eficiencia por articulo por planta
    Public Function EficienciaTelarArticulo_Importar() As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrCodigoEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_EficienciaArticuloTelar_Importar", objParametro)
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
