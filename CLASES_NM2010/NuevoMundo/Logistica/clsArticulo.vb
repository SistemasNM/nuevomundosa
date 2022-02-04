Imports NM.AccesoDatos

Public Class clsArticulo

#Region "Variables"
    Private mstr_Usuario As String
    Private mobj_Conexion As AccesoDatosSQLServer

    Private mstr_codarticulo As String = ""
    Private mstr_almtransfauto As String = ""

    Private mstr_codbarra1 As String = ""
    Private mstr_codbarra2 As String = ""
    Private mstr_codbarra3 As String = ""
    Private mstr_codbarra4 As String = ""
#End Region

#Region "-- Propiedades --"

    Public Property Usuario() As String
        Get
            Return mstr_Usuario
        End Get
        Set(ByVal Value As String)
            mstr_Usuario = Value
        End Set
    End Property

    Public Property CodArticulo() As String
        Get
            Return mstr_codarticulo
        End Get
        Set(ByVal Value As String)
            mstr_codarticulo = Value
        End Set
    End Property

    Public Property AlmTransfAutomatica() As String
        Get
            Return mstr_almtransfauto
        End Get
        Set(ByVal Value As String)
            mstr_almtransfauto = Value
        End Set
    End Property

    Public Property Codigo_Barra1() As String
        Get
            Return mstr_codbarra1
        End Get
        Set(ByVal Value As String)
            mstr_codbarra1 = Value
        End Set
    End Property

    Public Property Codigo_Barra2() As String
        Get
            Return mstr_codbarra2
        End Get
        Set(ByVal Value As String)
            mstr_codbarra2 = Value
        End Set
    End Property

    Public Property Codigo_Barra3() As String
        Get
            Return mstr_codbarra3
        End Get
        Set(ByVal Value As String)
            mstr_codbarra3 = Value
        End Set
    End Property

    Public Property Codigo_Barra4() As String
        Get
            Return mstr_codbarra4
        End Get
        Set(ByVal Value As String)
            mstr_codbarra4 = Value
        End Set
    End Property

#End Region

#Region "-- Metodos --"

    Public Function Listar_Caracteristicas(ByRef pdtb_lista As DataTable, ByVal pint_tipolista As Int16, ByVal pstr_tipoarticulo As String, ByVal pstr_rubro As String, ByVal pstr_familia As String, ByVal pstr_subfamilia As String) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipolista", pint_tipolista, _
            "pvch_tipoart", pstr_tipoarticulo, _
            "pvch_rubro", pstr_rubro, _
            "pvch_familia", pstr_familia, _
            "pvch_subfamilia", pstr_subfamilia}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("usp_log_articulocaracteristicas_lista", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Listar_DatosExtension(ByRef pdtb_lista As DataTable, ByVal pint_tipolista As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String) As Boolean
        'proceso: lista los datos de extension del tmitem
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipolista", pint_tipolista, _
            "pvch_param1", pstr_param1, _
            "pvch_param2", pstr_param2, _
            "pvch_param3", pstr_param3, _
            "pvch_param4", pstr_param4}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("usp_log_itemextension_listar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Guardar_DatosExtension(ByVal pint_accion As Int16) As Boolean
        'proceso: guarda los datos de extension del tmitem
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_accion", pint_accion, _
            "pvch_codarticulo", mstr_codarticulo, _
            "pvch_almtransfauto", mstr_almtransfauto, _
            "pvch_codbarra1", mstr_codbarra1, _
            "pvch_codbarra2", mstr_codbarra2, _
            "pvch_codbarra3", mstr_codbarra3, _
            "pvch_codbarra4", mstr_codbarra4, _
            "pvch_usumodificacion", mstr_Usuario}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("usp_log_itemextension_guardar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Listar_InventarioDiario(ByRef pdtb_lista As DataSet, ByVal pint_tipolista As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String) As Boolean
        'proceso: lista el inventario diario
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipolista", pint_tipolista, _
            "pvch_param1", pstr_param1, _
            "pvch_param2", pstr_param2, _
            "pvch_param3", pstr_param3, _
            "pvch_param4", pstr_param4}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            pdtb_lista = mobj_Conexion.ObtenerDataSet("usp_log_invdiariolog_listar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    '--------------------------------------------------------------
    ' Modificado: Alexander Torres Cardenas
    ' Añadir nuevos filtros al listado de inventario diario - ERI
    ' Julio 2013
    '--------------------------------------------------------------

    ' Lista inventario diario
    Public Sub InventarioDiario_Listado(ByRef dstInventario As DataSet, ByVal pint_TipoLista As Int16,
                                        ByVal vch_CodInventario As String, ByVal strEmpresa As String,
                                        ByVal strUnidad As String, ByVal strAlmacen As String)


        Dim objParametros() As Object = {"pint_TipoLista", pint_TipoLista, _
                                         "pvch_CodInventario", vch_CodInventario, _
                                         "pvch_Empresa", strEmpresa, _
                                         "pvch_Unidad", strUnidad, _
                                         "pvch_Almacen", strAlmacen}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            dstInventario = mobj_Conexion.ObtenerDataSet("usp_log_invdiariolog_listar_2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing
    End Sub

    ' Lista inventario diario 005
    Public Sub InventarioDiario_Listado_v2(ByRef dstInventario As DataSet, ByVal pint_TipoLista As Int16,
                                           ByVal vch_CodInventario As String, ByVal strEmpresa As String,
                                           ByVal strUnidad As String, ByVal strAlmacen As String)


        Dim objParametros() As Object = {"pint_TipoLista", pint_TipoLista, _
                                         "pvch_CodInventario", vch_CodInventario, _
                                         "pvch_Empresa", strEmpresa, _
                                         "pvch_Unidad", strUnidad, _
                                         "pvch_Almacen", strAlmacen}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            dstInventario = mobj_Conexion.ObtenerDataSet("usp_log_invdiariolog_listar_3", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing
    End Sub

    ' Genera inventario diario almacen - 005
    Public Function InventarioDiario_Generar_v2(ByRef dstInventario As DataSet, ByVal int_ItemsMasCaros As Int16, ByVal int_ItemsMenosCaros As Int16,
                                            ByVal int_ItemsMasRotacion As Int16, ByVal int_ItemsMenosRotacion As Int16, ByVal int_ItemsSinRotacion As Int16, _
                                            ByVal strUsuario As String, ByVal strAlmacen As String,
                                            ByVal strEmpresa As String, ByVal strUnidad As String,
                                            ByVal strFlagBusqueda As String, ByVal strCodArticulo As String,
                                            ByVal strUbicDesde As String, ByVal strUbicHasta As String) As Boolean
        Dim blnGenerar As Boolean = False
        Dim objParametros() As Object = {"pint_ItemsMasCaros", int_ItemsMasCaros, _
                                         "pint_ItemsMenosCaros", int_ItemsMenosCaros, _
                                         "pint_ItemsMasMov", int_ItemsMasRotacion, _
                                         "pint_ItemsMenosMov", int_ItemsMenosRotacion, _
                                         "pint_ItemsSinMov", int_ItemsSinRotacion, _
                                         "pvch_Usuario", strUsuario, _
                                         "pvch_Almacen", strAlmacen, _
                                         "pvch_Empresa", strEmpresa, _
                                         "pvch_Unidad", strUnidad, _
                                         "pvch_FlagBusqueda", strFlagBusqueda, _
                                         "pvch_CodArticulo", strCodArticulo, _
                                         "pvch_UbicDesde", strUbicDesde, _
                                         "pvch_UbicHasta", strUbicHasta}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            dstInventario = mobj_Conexion.ObtenerDataSet("usp_log_invdiariolog_obtener_v4", objParametros)
            blnGenerar = True
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return blnGenerar
    End Function


    ' Genera inventario diario
    Public Function InventarioDiario_Generar(ByRef dstInventario As DataSet, ByVal int_ItemsMasCaros As Int16, ByVal int_ItemsMenosCaros As Int16,
                                            ByVal int_ItemsMasRotacion As Int16, ByVal int_ItemsMenosRotacion As Int16, ByVal int_ItemsSinRotacion As Int16, _
                                            ByVal strUsuario As String, ByVal strAlmacen As String,
                                            ByVal strEmpresa As String, ByVal strUnidad As String) As Boolean
        Dim blnGenerar As Boolean = False
        Dim objParametros() As Object = {"pint_ItemsMasCaros", int_ItemsMasCaros, _
                                         "pint_ItemsMenosCaros", int_ItemsMenosCaros, _
                                         "pint_ItemsMasMov", int_ItemsMasRotacion, _
                                         "pint_ItemsMenosMov", int_ItemsMenosRotacion, _
                                         "pint_ItemsSinMov", int_ItemsSinRotacion, _
                                         "pvch_Usuario", strUsuario, _
                                         "pvch_Almacen", strAlmacen, _
                                         "pvch_Empresa", strEmpresa, _
                                         "pvch_Unidad", strUnidad}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            dstInventario = mobj_Conexion.ObtenerDataSet("usp_log_invdiariolog_obtener_v3", objParametros)
            blnGenerar = True
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return blnGenerar
    End Function

    ' Cerrar el inventario ERI
    Public Function InventarioDiario_Cerrar(ByVal strAccion As String, ByVal strCodInventario As String, ByVal strUsuario As String, _
                                            ByVal strEmpresa As String, ByVal strUnidad As String, ByVal strAlmacen As String) As Boolean
        Dim blnGuardar As Boolean = False

        Dim objParametros() As Object = {"pchr_Accion", strAccion, _
                                            "pvch_CodInventario", strCodInventario, _
                                            "pvch_Usuario", strUsuario, _
                                            "pvch_Empresa", strEmpresa, _
                                            "pvch_Unidad", strUnidad, _
                                            "pvch_Almacen", strAlmacen}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            mobj_Conexion.ObtenerDataSet("usp_log_invdiariolog_Estados", objParametros)
            blnGuardar = True
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return blnGuardar

    End Function

    ' Guardar el inventario ERI
    Public Function InventarioDiario_Guardar(ByVal strAccion As String, ByVal strCodInventario As String, _
                                            ByVal strDetalle As String, ByVal strUsuario As String, _
                                            ByVal strEmpresa As String, ByVal strUnidad As String, ByVal strAlmacen As String) As Boolean
        Dim blnGuardar As Boolean = False

        Dim objParametros() As Object = {"pchr_Accion", strAccion, _
                                            "pvch_CodInventario", strCodInventario, _
                                            "pvch_Detalle", strDetalle, _
                                            "pvch_Usuario", strUsuario, _
                                            "pvch_Empresa", strEmpresa, _
                                            "pvch_Unidad", strUnidad, _
                                            "pvch_Almacen", strAlmacen}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            mobj_Conexion.EjecutarComando("usp_log_invdiariolog_guardar_2", objParametros)
            blnGuardar = True
        Catch ex As Exception
            blnGuardar = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return blnGuardar
    End Function

    ' -----------------------------
    ' Fin de Cambios
    ' -----------------------------

    Public Function Guardar_InventarioDiario(ByVal pchr_accion As String, ByVal pstr_param1 As String, ByVal pstr_param2 As String, ByVal pstr_param3 As String, ByVal pstr_param4 As String, ByVal pstr_usuario As String) As Boolean
        'proceso: lista el inventario diario
        Dim lbln_estadof As Boolean = False

        Dim objParametros() As Object = { _
            "pchr_accion", pchr_accion, _
            "pvch_param1", pstr_param1, _
            "pvch_param2", pstr_param2, _
            "pvch_param3", pstr_param3, _
            "pvch_param4", pstr_param4, _
            "pvch_usuario", pstr_usuario}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)
            mobj_Conexion.EjecutarComando("usp_log_invdiariolog_guardar", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Listar_ArticulosSUNAT(ByRef pdtb_lista As DataTable, ByVal strFechaIni As String, ByVal strFechaFin As String, ByVal strTipoMovimiento As String) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "var_FechaInicio", strFechaIni, _
            "var_FechaFin", strFechaFin, _
            "chr_TipoMovi", strTipoMovimiento}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("USP_LOG_Listado_ArticulosFiscalizados_x_SUNAT", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function fnc_Guardar_ArticulosSUNAT(ByRef pdtb_lista As DataTable, ByVal strPeriodo As String, ByVal strMes As String, ByVal strFechaPresentacion As String, ByVal strNombreAdjunto As String, ByVal strRutaAdjunto As String, ByVal strObservaciones As String, ByVal strUsuario As String) As Boolean
        'proceso: Guarda la lista de Articulos
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "var_Periodo", strPeriodo, _
            "var_Mes", strMes, _
            "var_FechaPresentacion", strFechaPresentacion, _
            "var_NombreAdjunto", strNombreAdjunto, _
            "var_RutaAdjunto", strRutaAdjunto, _
            "var_Observaciones", strObservaciones, _
            "var_UsuarioCreacion", strUsuario}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("USP_LOG_Guardar_ArticulosSUNAT", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function fnc_GuardarFileIQ(ByVal strPeriodo As String, _
                                        ByVal pstrNombreArchivo As String, _
                                        ByVal pstrAliasArchivo As String, _
                                        ByVal pstrTipoArchivo As String, _
                                        ByVal pstrArchivodescrip As String) As String

        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = { _
            "pvch_Periodo", strPeriodo, _
            "pvch_NombreArchivo", pstrNombreArchivo, _
            "pvch_AliasArchivo", pstrAliasArchivo, _
            "pvch_TipoArchivo", pstrTipoArchivo, _
            "pvch_Descripcion", pstrArchivodescrip}
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("USP_LOG_GUARDARFILEIQ", lobjParametros)
        Catch ex As Exception
            lstrError = "Error: " & ex.Message
        End Try
        Return lstrError
    End Function

    Public Function fnc_ListarFileIQ(ByVal strPeriodo As Integer, ByRef pdtbLista As DataTable) As String
        Dim lstrError As String = ""
        Try
            Dim lobjParametros() As Object = {"pvch_Periodo", strPeriodo}
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtbLista = mobj_Conexion.ObtenerDataTable("USP_LOG_LISTARFILE_IQ", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrError
    End Function
#End Region

#Region "NUEVOS CAMBIOS ERI"
    ''' <summary>
    ''' Busca Lista de Inventarios ERI segun Filtro
    ''' </summary>
    ''' <param name="strCodigo"></param>
    ''' <param name="strEstado"></param>
    ''' <param name="strUnidad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ufn_BuscarListaERI(ByVal strCodigo As String, ByVal strEstado As String, ByVal strEmpresa As String, ByVal strUnidad As String) As DataTable
        Dim dtListaERI As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim objParametros() As Object = {"pvchCodigo", strCodigo,
                                                "pvchEstado", strEstado,
                                                "pvchEmpresa", strEmpresa,
                                                "pvchUnidad", strUnidad}

            dtListaERI = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_ListarERI", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return dtListaERI
    End Function


    Public Function obtenerRollosFaltantes(ByVal pstrCodigoERI As String, ByVal pstrCodigoRollo As String) As DataTable
        Dim dtListaERI As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim objParametros() As Object = {"pstrCodigoERI", pstrCodigoERI,
                                             "pstrCodigoRollo", pstrCodigoRollo}

            dtListaERI = mobj_Conexion.ObtenerDataTable("usp_log_obtenerRollosFaltantesERI", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return dtListaERI
    End Function

    '--------------------------------------------------------------
    ' Modificado: Luis Alanoca Jiménez
    ' Añadir nuevos filtros al listado de inventario diario - ERI
    ' 30/07/2014
    '-------------------------------------------------------------- 
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pstrEmpresa"></param>
    ''' <param name="pstrUnidad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InventarioDiario_Listado_Almacenes(ByVal pstrEmpresa As String, ByVal pstrUnidad As String) As DataTable
        Dim dtInventario As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pvchEmpresa", pstrEmpresa, _
                                                "pvchUnidad", pstrUnidad}

            dtInventario = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_Listar_Almacenes", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return dtInventario

    End Function

    Public Function InventarioDiario_Listado_Almacenes_v2(ByVal pstrEmpresa As String, ByVal pstrUnidad As String) As DataTable
        Dim dtInventario As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pvchEmpresa", pstrEmpresa, _
                                                "pvchUnidad", pstrUnidad}

            dtInventario = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_Listar_Almacenes_v2", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return dtInventario

    End Function

    Public Function InventarioDiario_Listado_Unidad(ByVal pstrEmpresa As String) As DataTable
        Dim dtUnidad As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pvchEmpresa", pstrEmpresa}

            dtUnidad = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_Listar_Unidad", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try

        mobj_Conexion = Nothing

        Return dtUnidad
    End Function


    Public Function InventarioDiario_Lista_ERI_Abiertos(ByVal pstrEmpresa As String, ByVal pstrUnidad As String) As DataTable
        Dim dtInventario As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pvchEmpresa", pstrEmpresa, _
                                                "pvchUnidad", pstrUnidad}

            dtInventario = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_Listar_ERI_Abiertos", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return dtInventario

    End Function

    Public Function InventarioDiario_Lista_ERI_Detalle(ByVal pstrCodigoIdERI As String) As DataTable
        Dim dtInventario As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pint_IdCodigoERI", pstrCodigoIdERI}

            dtInventario = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_Listar_ERI_Detalle", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return dtInventario

    End Function


    Public Function InventarioDiario_ConsultarItem(ByVal pstrCodEmpresa As String, ByVal pstrCodItem As String) As DataTable
        Dim dtInventario As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pvch_CodEmpresa", pstrCodEmpresa, _
                                              "pvch_CodItem", pstrCodItem}

            dtInventario = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_ConsultarItem", lobjParametros)

        Catch ex As Exception
            Throw ex
        End Try

        mobj_Conexion = Nothing

        Return dtInventario

    End Function

    Public Function InventarioDiario_ActualizaItem(ByVal pstrIdCodERI As String, ByVal pstrCodItem As String, ByVal pstrCantidad As String, ByVal pvch_Usuario As String) As Boolean

        Dim blnRespuesta As Boolean = False
        Dim intRspt As Integer

        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pint_IdCodigoERI", pstrIdCodERI, _
                                              "pvch_CodItem", pstrCodItem, _
                                              "pnum_Cantidad", pstrCantidad, _
                                              "pvch_Usuario", pvch_Usuario}

            intRspt = mobj_Conexion.EjecutarComando("usp_log_invdiariolog_Actualiza_Item", lobjParametros)
            blnRespuesta = True
        Catch ex As Exception
            Throw ex
        End Try

        mobj_Conexion = Nothing

        Return blnRespuesta

    End Function

    Public Function GeneraReconteo_ERI(ByVal pstrCodigoIdERI As String, ByVal pvch_Usuario As String) As DataTable
        Dim strNewCodigoERI As DataTable
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pint_IdCodigoERI", pstrCodigoIdERI, _
                                              "pvch_Usuario", pvch_Usuario}

            strNewCodigoERI = mobj_Conexion.ObtenerDataTable("usp_log_invdiariolog_GeneraReconteo_ERI", lobjParametros)
        Catch ex As Exception
            Throw ex
        End Try
        mobj_Conexion = Nothing

        Return strNewCodigoERI

    End Function

    Public Function CerrarERI(ByVal pstrCodigoIdERI As String, ByVal pvch_Usuario As String) As Boolean
        Dim blnRespuesta As Boolean = False
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMSmart)

            Dim lobjParametros() As Object = {"pint_IdCodigoERI", pstrCodigoIdERI, _
                                              "pvch_Usuario", pvch_Usuario}

            mobj_Conexion.EjecutarComando("usp_log_invdiariolog_Cerrar_ERI", lobjParametros)
            blnRespuesta = True
        Catch ex As Exception
            Throw ex
        End Try
        Return blnRespuesta
    End Function


#End Region

End Class
