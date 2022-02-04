Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsUnidadConsumoOperacion

    '============================== Definicion de variables interna ==============================

    Dim mstrError As String
    Dim mstrEmpresa As String
    Dim mintCodigoGrupo As Integer
    Dim mintRevisionGrupo As Integer
    Dim mstrDescripcion As String
    Dim mintTipoEmpleo As Integer
    Dim mstrUnidadConsumo As String
    Dim mstrOperacion As String
    Dim mstrEstado As String
    Dim mstrUsuario As String


    '================================= Definición de constructores ===============================

    Public Sub New()
        mstrError = ""
        mstrEmpresa = ""
        mintCodigoGrupo = 0
        mintRevisionGrupo = 0
        mstrDescripcion = ""
        mintTipoEmpleo = 0
        mstrUnidadConsumo = ""
        mstrOperacion = ""
        mstrEstado = ""
        mstrUsuario = ""
    End Sub

    '================================= Definición de Propiedades =================================

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal strCad As String)
            mstrEmpresa = strCad
        End Set
    End Property

    Public Property CodigoGrupo() As Integer
        Get
            CodigoGrupo = mintCodigoGrupo
        End Get
        Set(ByVal intVal As Integer)
            mintCodigoGrupo = intVal
        End Set
    End Property

    Public Property RevisionGrupo() As Integer
        Get
            RevisionGrupo = mintRevisionGrupo
        End Get
        Set(ByVal intVal As Integer)
            mintRevisionGrupo = intVal
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = mstrDescripcion
        End Get
        Set(ByVal strCad As String)
            mstrDescripcion = strCad
        End Set
    End Property

    Public Property TipoEmpleo() As Integer
        Get
            TipoEmpleo = mintTipoEmpleo
        End Get
        Set(ByVal intVal As Integer)
            mintTipoEmpleo = intVal
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

    Public Property UnidadConsumo() As String
        Get
            UnidadConsumo = mstrUnidadConsumo
        End Get
        Set(ByVal strCad As String)
            mstrUnidadConsumo = strCad
        End Set
    End Property

    Public Property Operacion() As String
        Get
            Operacion = mstrOperacion
        End Get
        Set(ByVal strCad As String)
            mstrOperacion = strCad
        End Set
    End Property

    '=================================== Definicion de metodos  ==================================

    Public Function ListarxGrupo(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      retorna un listado de las unidades de consumo y sus operaciones
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "ptin_grupoempleo", mintTipoEmpleo, _
                                        "pint_codigogrupo", mintCodigoGrupo, _
                                        "pint_revisiongrupo", mintRevisionGrupo, _
                                        "pvch_maquinaproduccion", "", _
                                        "pvch_desmaquinaproduccion", "", _
                                        "pvch_codigooperacion", "", _
                                        "pvch_descoperacion", ""}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumooperacion_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarxOperacion(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.06
        'Proposito :      retorna un listado de las unidades de consumo y sus operaciones
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "ptin_grupoempleo", mintTipoEmpleo, _
                                        "pint_codigogrupo", mintCodigoGrupo, _
                                        "pint_revisiongrupo", mintRevisionGrupo, _
                                        "pvch_maquinaproduccion", mstrUnidadConsumo, _
                                        "pvch_desmaquinaproduccion", "", _
                                        "pvch_codigooperacion", mstrOperacion, _
                                        "pvch_descoperacion", ""}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumooperacion_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarxBusqueda(ByRef pDT As DataTable, ByVal pstrcodmaquina As String, ByVal pstrdescmaquina As String, ByVal pstrcodoperacion As String, ByVal pstrdescoperacion As String) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      retorna un listado de las unidades de consumo y sus operaciones
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 3, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "ptin_grupoempleo", mintTipoEmpleo, _
                                        "pint_codigogrupo", mintCodigoGrupo, _
                                        "pint_revisiongrupo", mintRevisionGrupo, _
                                        "pvch_maquinaproduccion", pstrcodmaquina, _
                                        "pvch_desmaquinaproduccion", pstrdescmaquina, _
                                        "pvch_codigooperacion", pstrcodoperacion, _
                                        "pvch_descoperacion", pstrdescoperacion}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumooperacion_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function GuardarxGrupo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.06
        'Proposito :      Permite registrar un grupo agrupador de operaciones, solo la cabecera
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "ptin_accion", 1, _
                                        "ptin_GrupoEmpleo", mintTipoEmpleo, _
                                        "pint_CodigoGrupo", mintCodigoGrupo, _
                                        "pint_RevisionGrupo", mintRevisionGrupo, _
                                        "pvch_DescripcionGrupo", mstrDescripcion, _
                                        "pnte_detalle", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumooperacion_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function GuardarxOperacion(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.06
        'Proposito :      Permite registrar las operaciones de un grupo
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "ptin_accion", 2, _
                                        "ptin_GrupoEmpleo", mintTipoEmpleo, _
                                        "pint_CodigoGrupo", mintCodigoGrupo, _
                                        "pint_RevisionGrupo", mintRevisionGrupo, _
                                        "pvch_DescripcionGrupo", mstrDescripcion, _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumooperacion_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemas(ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas
        pdtDetalle.TableName = "lista"
        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.GuardarOperacionesUConsumo)
        If blnRpta = True Then
            pdtDetalle = pDT.Tables(0)
        End If

        Try
            mstrError = ""
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally

        End Try

        Return blnRpta

    End Function

    Public Function EliminarxGrupo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente un C.Costo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                "ptin_accion", 3, _
                                "ptin_GrupoEmpleo", mintTipoEmpleo, _
                                "pint_CodigoGrupo", mintCodigoGrupo, _
                                "pint_RevisionGrupo", mintRevisionGrupo, _
                                "pvch_DescripcionGrupo", mstrDescripcion, _
                                "pnte_detalle", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumooperacion_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ActivarxGrupo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente un C.Costo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                "ptin_accion", 4, _
                                "ptin_GrupoEmpleo", mintTipoEmpleo, _
                                "pint_CodigoGrupo", mintCodigoGrupo, _
                                "pint_RevisionGrupo", mintRevisionGrupo, _
                                "pvch_DescripcionGrupo", mstrDescripcion, _
                                "pnte_detalle", ""}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumooperacion_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class
