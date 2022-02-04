Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsDistribucionGastosRubro

    '================================= Definicion variables local =================================
    Dim mstrError As String

    '=================================== Definicion de metodos  ===================================

    Public Function Dist_CCSoporteCCProductivo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Distribuye los porcentajes entre los centro de costo soporte y productivo
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}


        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return True

    End Function

    Public Function Cons_DistConsumoVapor(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      consulta de distribucion de consumo de vapor entre los centros de costos
        '*******************************************************************************************
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try


    End Function


    Public Function Dist_Gastos() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Distribuye los gastos entre toda las unidades de consumo
        '*******************************************************************************************
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Function



    Public Function Cons_CtaGastosRubroGerencial(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Consulta para validar la cta de gatos y los rubros gerenciale
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Function


    Public Function Cons_CtaGastosRubroContable(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Consulta para validar la cta de gatos y los rubros contable
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
        Catch ex As Exception
            mstrError = ex.Message
        End Try


    End Function


    Public Function Dist_GastoEstampadoCordoroy() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      distribucion de gasto de Estampado y Cordoroy
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try


    End Function

    Public Function Cons_CCostoCtaGasto(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      consulta para validar los centro de costoy las cta de gastos
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try


    End Function




    Public Function Cons_AgrupacionTiposValido(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Consulta de agrupacion de tipos validos
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message

        End Try


    End Function

    Public Function Cons_CtaGastoMensual(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Consulta de cta de gasto mensual
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try


    End Function

    Public Function Cons_GastoRubroNivelCC(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Consulta de gasto de rubro por nivel
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"", ""}

        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Function

End Class
