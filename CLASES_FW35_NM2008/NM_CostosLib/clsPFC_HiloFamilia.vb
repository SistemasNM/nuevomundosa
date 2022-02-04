Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsPFC_HiloFamilia


    '================================ Definicion variables locales ================================
#Region "-- Variables --"

    Protected Friend mstrError As String = ""

    Protected Friend mstrEmpresa As String = ""
    Protected Friend mstrCodigoHilo As String = ""
    Protected Friend mstrNombreHilo As String = ""
    Protected Friend mstrCodigoFamilia As String = ""
    Protected Friend mstrNombreFamilia As String = ""

    Protected Friend mstrUsuario As String = ""



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

    Public Property CodigoHilo() As String
        Get
            CodigoHilo = mstrCodigoHilo
        End Get
        Set(ByVal strVal As String)
            mstrCodigoHilo = strVal
        End Set
    End Property

    Public Property NombreHilo() As String
        Get
            NombreHilo = mstrNombreHilo
        End Get
        Set(ByVal strVal As String)
            mstrNombreHilo = strVal
        End Set
    End Property

    Public Property CodigoFamilia() As String
        Get
            CodigoFamilia = mstrCodigoFamilia
        End Get
        Set(ByVal strVal As String)
            mstrCodigoFamilia = strVal
        End Set
    End Property

    Public Property NombreFamilia() As String
        Get
            NombreFamilia = mstrNombreFamilia
        End Get
        Set(ByVal strVal As String)
            mstrNombreFamilia = strVal
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
#Region "-- Metodos --"

    Public Function Listar(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      06-12-2010
        'Proposito :      retorna un listado de los hilos con su familia
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_NombreHilo", mstrNombreHilo, _
                                        "pvch_CodigoFamilia", mstrCodigoFamilia, _
                                        "pvch_NombreFamilia", mstrNombreFamilia}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_hilofamilia_listar ", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    '===== Metodos de Actualizacion  =====

    Public Function Guardar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      02-12-2010
        'Proposito :      Permite actualizar datos del registro
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer


        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_CodigoFamilia", mstrCodigoFamilia, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_hilofamilia_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function Anular() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      02-12-2010
        'Proposito :      Permite anular un reg 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer


        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_hilofamilia_Anular", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function



    Public Function Activar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      02-12-2010
        'Proposito :      Permite anular un reg 
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer


        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_hilofamilia_Activar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function importar() As Boolean
        '*******************************************************************************************
        'Creado por:	  cpt
        'Fecha     :      06-12-2010
        'Proposito :      importar los hilos con su familia
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_hilofamilia_importar", objParametro)
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
