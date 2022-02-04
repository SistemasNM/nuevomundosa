Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsUnidadConsumoEEPlantaPrincipal
    Inherits clsUnidadConsumoEE

    Public Function ListarTodos(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE de planta principal
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 17, _
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
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR de los grupo de calculo (trabajadores y bombas de agua)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 10, _
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

    Public Function ListarxGrupoDistTrabajadores(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR del grupo de trabajadores
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 11, _
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

    Public Function ListarxGrupoDistBombasAgua(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR del grupo de distribución de bombas de agua
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 12, _
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

    Public Function ListarxGrupoTipoTelares(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR del grupo de tipo de telares
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 13, _
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

    Public Function ListarxGrupoMaqPreTejido(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR del grupo de maquinas de pre-tejido
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 14, _
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

    Public Function ListarxGrupoHorasEstandar(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR del grupo de maquinas de pre-tejido
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 15, _
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

    Public Function ListarxEmpleaVelocidad(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.11.30
        'Proposito :      retorna un listado de las unidades de consumo EE-PPR del grupo de maquinas de pre-tejido
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 16, _
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

    Public Function ListarxOrdenCalculo(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.30
        'Proposito :      retorna un listado de las unidades de consumo EE-Planta Principal por campo orden
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 6, _
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

    Public Function ObtenerEsquemaxOrdenCalculo(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.30
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDt As New DataSet, lclsEsquemas As New clsEsquemas

        pdtDetalle.TableName = "lista"
        Try

            blnRpta = lclsEsquemas.ObtenerEsquemas(pDt, clsEsquemas.enu_esquemas.GuardarOrdenCalculoEEPPR)
            If blnRpta = True Then
                pdtDetalle = pDt.Tables(0)
            End If

            pDt = Nothing
            'vch_CodigoUnidadConsumo		as [c1],
            'int_OrdenCalculo				as [c2],

            mstrError = ""
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally

        End Try

        Return blnRpta

    End Function

    Public Function GuardarxOrdenCalculo(ByVal pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      02-12-2009
        'Proposito :      modifica el orden de calculo de cada unidad de consumo de agua
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pDT.TableName = "lista"

        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pchr_codigoplanta", "PPR", _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pDT)}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumoee_guardar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function
End Class
