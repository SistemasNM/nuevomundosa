Imports NM.AccesoDatos

Public Class clsAlmacen

#Region "Variables"
  Private mstr_Usuario As String
  Private mobj_Conexion As AccesoDatosSQLServer
#End Region

#Region "-- Metodos --"

    Public Function ListarMaquinas(ByRef pdtb_lista As DataTable) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = {}

        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("USP_MAQUINAS_TIN_TEJ_LISTAR", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function ListarUbicaciones(ByRef pdtb_lista As DataTable, ByVal pstr_Empresa As String, ByVal pstr_fechaInicio As String, ByVal pstr_fechaFinal As String) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "CO_EMPR", pstr_Empresa, _
            "FE_INIC", pstr_fechaInicio, _
            "FE_FINA", pstr_fechaFinal}

        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("USP_ATEL_MVTO_UBICACIONES", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Listar(ByRef pdtb_lista As DataTable, ByVal pstr_tipo As String, ByVal pstr_coalma As String, ByVal pstr_dealma As String) As Boolean
        'proceso: lista tipo de articulo, rubros, familia, subfamilia
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "chr_tipo", pstr_tipo, _
            "co_alma", pstr_coalma, _
            "de_alma", pstr_dealma}

        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_lista = mobj_Conexion.ObtenerDataTable("usp_qry_ConsultarAlmacenes", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function Procesar_TransfAutomatica(ByVal pint_tipo As Int16, ByVal pstr_param1 As String, ByVal pstr_param2 As String) As Boolean
        'proceso: procesa la transferencia automatica de almacen principal a los otros almacenes
        '       solo para los articulos que han sido asignados en una lista
        ' Modificado por Alexandeer Torres
        ' Junio 2013
        ' Se agrego uni med alt, y cant alt al detalle de la trasferencia.
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "ptin_tipo", pint_tipo, _
            "pvch_param1", pstr_param1, _
            "pvch_param2", pstr_param2}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("usp_log_transalmacenes_procesar_2", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function
    Public Function Generar_Inventario_AlmacenTelas(ByVal strEmpresa As String) As Boolean
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = { _
            "CO_EMPR", strEmpresa}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            mobj_Conexion.EjecutarComando("usp_log_Generar_Inventario_AlmacenTelas", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function

    Public Function obtenerDatosCabecera(ByRef pdtb_Registros As DataTable, ByVal strCodigoUbicacion As String) As Boolean
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = {"CO_UBIC_ALMA", strCodigoUbicacion}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_Registros = mobj_Conexion.ObtenerDataTable("USP_LOG_INVENTARIO_LISTA_CABECERA", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function
    Public Function obtenerDatosDetalle(ByVal strCodigoUbicacion As String) As DataTable
        Dim objParametros() As Object = {"CO_UBIC_ALMA", strCodigoUbicacion}
        mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Return mobj_Conexion.ObtenerDataTable("USP_LOG_INVENTARIO_LISTA_DETALLE", objParametros)
    End Function
    Public Function ObtenerAlmacenes() As DataTable
        mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Return mobj_Conexion.ObtenerDataTable("USP_LOG_INVENTARIO_LISTA_ALMACENES")
    End Function
    Public Function ObtenerFechas(ByVal strCodigoAlmacen As String) As DataTable
        Dim objParametros() As Object = {"CO_ALMA", strCodigoAlmacen}
        mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Return mobj_Conexion.ObtenerDataTable("USP_LOG_INVENTARIO_LISTA_FECHAS", objParametros)
    End Function
    Public Function actualizarFechaExpiracionOrCo(ByVal strTipoDocumento As String, strFechaExpiOrCo As String) As DataTable
        Dim objParametros() As Object = {"NU_DOCU", strTipoDocumento, "FE_EXPI_REQI", strFechaExpiOrCo}
        mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Return mobj_Conexion.ObtenerDataTable("usp_log_actu_fecha_expiracion_orco", objParametros)
    End Function
    Public Sub EliminarRegistroInventario(ByVal strCodigoUbicacion As String, ByVal strNumeroItem As String)
        Dim mobj_Conexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Dim parametros As Object() = {"CO_UBIC_ALMA", strCodigoUbicacion, "NU_SECU", strNumeroItem}
        mobj_Conexion.EjecutarComando("USP_LOG_INVENTARIO_ELIMINA_REGISTRO", parametros)
    End Sub
    Public Function ObtenerAnioVigenteStock() As DataTable
        mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Return mobj_Conexion.ObtenerDataTable("USP_LOG_TCDOCU_ALMA_LISTAR_ANIO_STOCK")
    End Function
    Public Function obtenerDatosDocumento(ByRef pdtb_Registros As DataTable, ByVal strTipoDocumento As String, strCodigoDocumento As String) As Boolean
        Dim lbln_estadof As Boolean = False
        Dim objParametros() As Object = {"TI_DOCU", strTipoDocumento, "NU_DOCU", strCodigoDocumento}
        Try
            mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            pdtb_Registros = mobj_Conexion.ObtenerDataTable("USP_LOG_DOCUMENTOS_CABECERA_LISTAR", objParametros)
            lbln_estadof = True
        Catch ex As Exception
            lbln_estadof = False
            Throw ex
        End Try
        mobj_Conexion = Nothing
        Return lbln_estadof
    End Function
    Public Function ObtenerDetalleDocumento(ByVal strTipoDocumento As String, strCodigoDocumento As String) As DataTable
        Dim objParametros() As Object = {"TI_DOCU", strTipoDocumento, "NU_DOCU", strCodigoDocumento}
        mobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Return mobj_Conexion.ObtenerDataTable("USP_LOG_DOCUMENTOS_DETALLE_LISTAR", objParametros)
    End Function

    Public Function AnularDocumento(ByVal pstrEmpresa As String, ByVal pstrTipo As String, ByVal pstrNumero As String, ByVal pstrUsuario As String) As DataTable
        Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As Object = { _
        "pvch_empresa", pstrEmpresa, _
        "pvch_tipoDoc", pstrTipo, _
        "pvch_NroDoc", pstrNumero, _
        "pvch_usuario", pstrUsuario _
        }
        Try
            lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Return lobjConexion.ObtenerDataTable("USP_LOG_DOCUMENTOS_ANULAR", lstrParametros)
        Catch ex As Exception
            Throw ex
        Finally
            lobjConexion = Nothing
        End Try
    End Function
#End Region

    Function ListarMaquinas(ldtb_datos As DataTable, p2 As String) As Boolean
        Throw New NotImplementedException
    End Function


End Class
