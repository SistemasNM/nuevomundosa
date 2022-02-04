Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsRubroContable
    Inherits clsRubro

    '================================= Definición de constructores ===============================

    Sub New()
        mstrTipoRubro = "RC"
    End Sub

    '================================= Definición de constructores ===============================

    Public Function Insertar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Permite registrar un nuevo Rubro
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pint_codigorubro", mintCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pchr_tiporubro", mstrTipoRubro, _
                                        "pvch_descripcioncorta", mstrDescripcionCorta, _
                                        "pvch_descripcionlarga", mstrDescripcionLarga, _
                                        "pchr_estado", mstrEstado, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_rubro_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Listar(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      retorna un listado de Rubro
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pint_codigorubro", mintCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pchr_tiporubro", mstrTipoRubro, _
                                        "pvch_descripcioncorta", mstrDescripcionCorta, _
                                        "pvch_descripcionlarga", mstrDescripcionLarga, _
                                        "pchr_estado", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_rubro_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Sub Obtener()
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Obtiene los datos de un Rubro
        '*******************************************************************************************
        Dim Conexion As AccesoDatosSQLServer
        Dim dtbData As DataTable
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pint_codigorubro", mintCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pchr_tiporubro", mstrTipoRubro, _
                                        "pvch_descripcioncorta", "", _
                                        "pvch_descripcionlarga", "", _
                                        "pchr_estado", ""}

        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            dtbData = Conexion.ObtenerDataTable("usp_cos_rubro_listar", objParametro)
            If (Not (dtbData Is Nothing)) AndAlso dtbData.Rows.Count > 0 Then
                mintCodigo = dtbData.Rows(0).Item("int_codigorubro").ToString
                mstrDescripcionCorta = dtbData.Rows(0).Item("vch_descripcioncorta").ToString
                mstrDescripcionLarga = dtbData.Rows(0).Item("vch_descripcionlarga").ToString
                mstrTipoRubro = dtbData.Rows(0).Item("chr_tiporubro").ToString
                mstrEstado = dtbData.Rows(0).Item("chr_estado").ToString
                mstrUsuario = dtbData.Rows(0).Item("vch_UsuarioCreacion").ToString
            Else
                mintCodigo = 0
                mstrDescripcionCorta = ""
                mstrDescripcionLarga = ""
                mstrTipoRubro = ""
                mstrEstado = ""
                mstrUsuario = ""
            End If


        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub


    '===== Metodos de Actualizacion  =====

    Public Function Modificar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite modificar datos del Rubro
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 2, _
                                "pint_codigorubro", mintCodigo, _
                                "pchr_codigoempresa", mstrEmpresa, _
                                "pchr_tiporubro", mstrTipoRubro, _
                                "pvch_descripcioncorta", mstrDescripcionCorta, _
                                "pvch_descripcionlarga", mstrDescripcionLarga, _
                                "pchr_estado", mstrEstado, _
                                "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_rubro_guardar", objParametro)
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
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente un Rubro
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 3, _
                           "pint_codigorubro", mintCodigo, _
                           "pchr_codigoempresa", mstrEmpresa, _
                           "pchr_tiporubro", mstrTipoRubro, _
                           "pvch_descripcioncorta", "", _
                           "pvch_descripcionlarga", "", _
                           "pchr_estado", "", _
                           "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_rubro_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class
