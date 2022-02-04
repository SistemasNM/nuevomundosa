Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsUnidadConsumoEEPlantaHilanderia
    Inherits clsUnidadConsumoEE

    Public Function ListarTodos(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna un listado de las unidades de consumo EE de planta hilandería
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 19, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", mstrEstado}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarxGrupoCalculo(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna un listado de las unidades de consumo EE-PHI de los grupo de calculo (luwa e iluminación)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 20, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarxGrupoDistLuwas(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna un listado de las unidades de consumo EE-PHI de los grupo dist de luwas
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 21, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarxGrupoDistIluminacion(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna un listado de las unidades de consumo EE-PHI de los grupo dist de iluminación
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 22, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ListarxGrupoHoraMaquinaHilanderia(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.19
        'Proposito :      retorna un listado de las unidades de consumo EE-PHI de los grupo hora máquina hilandería
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 23, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class
