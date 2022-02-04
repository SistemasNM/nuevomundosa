Imports NuevoMundo.Generales
Imports NM.AccesoDatos
Imports System.Data.SqlClient

Imports System.Xml
Imports System.IO
Imports System.Text

Namespace OFILOGI
    Public Class Articulos
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_ArticulosListar"
        Private CONST_SP_BUSCAR = "usp_qry_ArticulosBuscar"
        Private Const CONST_NOMBRE_TABLA_ARTICULOS = "ARTICULOS"
#End Region

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mdtDetalle As DataTable
#End Region

#Region "    Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
        Public Property Detalle() As DataTable
            Get
                Detalle = mdtDetalle
            End Get
            Set(ByVal Value As DataTable)
                mdtDetalle = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "    Metodos"
        Public Function Listar(ByRef pLista As DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"P_CO_EMPR", Me.EmpresaCodigo}

            Me.LimpiarError()
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pLista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR, larrParams)
                pLista.TableName = CONST_NOMBRE_TABLA_ARTICULOS
                Me.Ok = True
            Catch ex As Exception
                pLista = Nothing
                Me.Ok = False
                'Me.Mensaje = New NuevoMundo.Generales.Clases.NMMensaje( _
                '                        "OFISIS:OFILOGI:Proveedores.Articulos", _
                '                        "", _
                '                        "Error al listar proveedores", _
                '                        ex.Message, _
                '                        Clases.NMMensaje.enuTiposMensajes.Error)
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"P_CO_EMPR", Me.EmpresaCodigo, "P_CO_ITEM", mstrCodigo}

            Me.LimpiarError()
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable(CONST_SP_BUSCAR, larrParams)
                mstrNombre = ldtRes.Rows(0).Item("NO_ITEM")
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
                'Me.Mensaje = New NuevoMundo.Generales.Clases.NMMensaje("OFISIS:OFILOGI:Articulos.Buscar", _
                '        "", _
                '        "Error al consultar proveedor", _
                '        ex.Message, _
                '    Clases.NMMensaje.enuTiposMensajes.Error)
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose

        End Sub
        Public Function Actualiza_DetalleRequi(ByVal strCodigoEmpresa As String, ByVal strNumeroRequi As String, ByVal strDatatable As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            Try
                strDatatable.TableName = "LOG00001_utbDetalle"
                lobjUtil(strDatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
                Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa, "Num_reqi", strNumeroRequi, "P_NTX_XML", lstrXML}
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.EjecutarComando("usp_qry_Actualiza_detalle_requi", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            lobjCon = Nothing
            lobjUtil = Nothing
        End Function

        Public Function Actualiza_DetalleRequi_V2(ByVal strCodigoEmpresa As String, ByVal strNumeroRequi As String, ByVal strDatatable As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            Try
                strDatatable.TableName = "LOG00001_utbDetalle"
                lobjUtil(strDatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
                Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa, "Num_reqi", strNumeroRequi, "P_NTX_XML", lstrXML}
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.EjecutarComando("usp_qry_Actualiza_detalle_requi_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            lobjCon = Nothing
            lobjUtil = Nothing
        End Function

        Public Function Listar_Almacenes(ByVal strCodigoEmpresa As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"co_empr", strCodigoEmpresa}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_listar_Almacenes", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function Listar_Activos(ByVal strCodigoActivo As String, ByVal strDescripcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"co_acti_fijo", strCodigoActivo, "de_Acti", strDescripcion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_ActivoFijos", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function Listar_Maquinas(ByVal strCodigoActivo As String, ByVal strDescripcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"Co_Maquina", strCodigoActivo, "De_Maquina", strDescripcion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_Maquinas_Mantto", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function Listar_Personal(ByVal strCodigoActivo As String, ByVal strDescripcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"Co_Pers", strCodigoActivo, "De_Pers", strDescripcion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_Personal_Mantto", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function Listar_CuentaGastos(ByVal strCodigoCtaGasto As String, ByVal strDescripcion As String, ByVal strCodigoAuxiliar As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"CO_DEST_FINA", strCodigoCtaGasto, _
                                            "DE_UNID_DEST", strDescripcion, _
                                            "var_CO_AUXI_EMPR", strCodigoAuxiliar}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_CuentasGastos", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function Listar_CuentaGastos(ByVal strCodigoCtaGasto As String, ByVal strDescripcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"CO_DEST_FINA", strCodigoCtaGasto, _
                                            "DE_UNID_DEST", strDescripcion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_CuentasGastos", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function

        Public Function Listar_Articulos(ByVal strCodigoItem As String, ByVal strDescripcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"co_item", strCodigoItem, "de_item", strDescripcion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_listado_Articulos", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function

        Public Function Listar_ArticulosxTipoLista(ByVal pintTipoLista As Int16, ByVal pstrCodigoAlmacen As String, ByVal pstrCodigoItem As String, ByVal pstrDescripcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer, ldtRes As DataTable
            Dim larrParams() As Object = { _
            "ptin_tipolista", pintTipoLista, _
            "pvch_co_alma", pstrCodigoAlmacen, _
            "pvch_co_item", pstrCodigoItem, _
            "pvch_de_item", pstrDescripcion _
            }
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_log_articulos_lista", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function

        Public Function Listar_GruposServicios() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_Tipo_Servicio")
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function Listar_Tipo_Servicios(ByVal strGrupoServicio As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim params() As Object = {"CO_GRUP_SERV", strGrupoServicio}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_Listar_Servicio", params)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function
        Public Function limpiarTiposTrabajo(ByVal pstrOperacion As String,
                                           ByVal pstrNumRequi As String,
                                           ByVal pintIDTipTrabajo As Integer,
                                           ByVal pstrUsuario As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim params() As Object = {"pvchOperacion", pstrOperacion,
                                      "pvchNumRequi", pstrNumRequi,
                                      "pintIDTipTrabajo", pintIDTipTrabajo,
                                      "pvchUsuario", pstrUsuario}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("USP_LOG_TIP_TRABAJO_REQUI", params)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function

        Public Function asociarTipoTrabajo(ByVal pstrOperacion As String,
                                           ByVal pstrNumRequi As String,
                                           ByVal pintIDTipTrabajo As Integer,
                                           ByVal pstrUsuario As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim params() As Object = {"pvchOperacion", pstrOperacion,
                                      "pvchNumRequi", pstrNumRequi,
                                      "pintIDTipTrabajo", pintIDTipTrabajo,
                                      "pvchUsuario", pstrUsuario}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("USP_LOG_TIP_TRABAJO_REQUI", params)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function

        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        ' Inserta requisicion
        Public Function Insertar_Requisicion(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strTieneNumeroSerie As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strFechaExpiracion As String, _
                                                ByVal strAtendido As String, _
                                                ByVal strTipoAuxliar As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strRequiConst As String, _
                                                ByVal strRequiIsnu As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strCodigoComprador As String, _
                                                ByVal strNombreComprador As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioCreacion As String, _
                                                ByVal strFechaCreacion As String, _
                                                ByVal strUsuarioModificacion As String, _
                                                ByVal strFechaModificacion As String, _
                                                ByVal strDatatatable As DataTable, _
                                                ByVal strNumeroRequiGenerado As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "") As DataTable

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            strDatatatable.TableName = "LOG00001_utbDetalle"
            lobjUtil(strDatatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_SERI_REQI", strTieneNumeroSerie, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "IDFE_TOPE_REQI", strFechaExpiracion, _
                                            "ISST_ATEN", strAtendido, _
                                            "ISTI_AUXI_EMPR", strTipoAuxliar, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISST_REQI_CONS", strRequiConst, _
                                            "ISNU_REQI_CONS", strRequiIsnu, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "ISCO_COMP", strCodigoComprador, _
                                            "ISNO_COMP", strNombreComprador, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_CREACION", strUsuarioCreacion, _
                                            "FECHA_CREACION", strFechaCreacion, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "FECHA_MODIFICACION", strFechaModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "OSNU_REQI", strNumeroRequiGenerado, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.ObtenerDataTable("USP_QRY_INSERTAR_TCREQI", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        ' Inserta requisicion
        Public Function Insertar_Requisicion_v2(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strTieneNumeroSerie As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strFechaExpiracion As String, _
                                                ByVal strAtendido As String, _
                                                ByVal strTipoAuxliar As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strRequiConst As String, _
                                                ByVal strRequiIsnu As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strCodigoComprador As String, _
                                                ByVal strNombreComprador As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioCreacion As String, _
                                                ByVal strFechaCreacion As String, _
                                                ByVal strUsuarioModificacion As String, _
                                                ByVal strFechaModificacion As String, _
                                                ByVal strDatatatable As DataTable, _
                                                ByVal strNumeroRequiGenerado As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "", _
                                                Optional ByVal strFechaConclusion As String = "") As DataTable

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            strDatatatable.TableName = "LOG00001_utbDetalle"
            lobjUtil(strDatatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_SERI_REQI", strTieneNumeroSerie, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "IDFE_TOPE_REQI", strFechaExpiracion, _
                                            "ISST_ATEN", strAtendido, _
                                            "ISTI_AUXI_EMPR", strTipoAuxliar, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISST_REQI_CONS", strRequiConst, _
                                            "ISNU_REQI_CONS", strRequiIsnu, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "ISCO_COMP", strCodigoComprador, _
                                            "ISNO_COMP", strNombreComprador, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_CREACION", strUsuarioCreacion, _
                                            "FECHA_CREACION", strFechaCreacion, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "FECHA_MODIFICACION", strFechaModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "OSNU_REQI", strNumeroRequiGenerado, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo, _
                                            "IDFE_EXPI_REQI", strFechaConclusion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.ObtenerDataTable("USP_QRY_INSERTAR_TCREQI_V2", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        ' Inserta requisicion
        Public Function Insertar_Requisicion_v3(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strTieneNumeroSerie As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strFechaExpiracion As String, _
                                                ByVal strAtendido As String, _
                                                ByVal strTipoAuxliar As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strRequiConst As String, _
                                                ByVal strRequiIsnu As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strCodigoComprador As String, _
                                                ByVal strNombreComprador As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioCreacion As String, _
                                                ByVal strFechaCreacion As String, _
                                                ByVal strUsuarioModificacion As String, _
                                                ByVal strFechaModificacion As String, _
                                                ByVal strDatatatable As DataTable, _
                                                ByVal strNumeroRequiGenerado As String, _
                                                ByVal strCodResponsableOT As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "", _
                                                Optional ByVal strFechaConclusion As String = "") As DataTable

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            strDatatatable.TableName = "LOG00001_utbDetalle"
            lobjUtil(strDatatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_SERI_REQI", strTieneNumeroSerie, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "IDFE_TOPE_REQI", strFechaExpiracion, _
                                            "ISST_ATEN", strAtendido, _
                                            "ISTI_AUXI_EMPR", strTipoAuxliar, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISST_REQI_CONS", strRequiConst, _
                                            "ISNU_REQI_CONS", strRequiIsnu, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "ISCO_COMP", strCodigoComprador, _
                                            "ISNO_COMP", strNombreComprador, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_CREACION", strUsuarioCreacion, _
                                            "FECHA_CREACION", strFechaCreacion, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "FECHA_MODIFICACION", strFechaModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "OSNU_REQI", strNumeroRequiGenerado, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo, _
                                            "IDFE_EXPI_REQI", strFechaConclusion, _
                                            "pvch_ResponsableOT", strCodResponsableOT}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.ObtenerDataTable("USP_QRY_INSERTAR_TCREQI_V3", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        ' Inserta requisicion
        Public Function Insertar_Requisicion_v4(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strTieneNumeroSerie As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strFechaExpiracion As String, _
                                                ByVal strAtendido As String, _
                                                ByVal strTipoAuxliar As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strRequiConst As String, _
                                                ByVal strRequiIsnu As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strCodigoComprador As String, _
                                                ByVal strNombreComprador As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioCreacion As String, _
                                                ByVal strFechaCreacion As String, _
                                                ByVal strUsuarioModificacion As String, _
                                                ByVal strFechaModificacion As String, _
                                                ByVal strDatatatable As DataTable, _
                                                ByVal strNumeroRequiGenerado As String, _
                                                ByVal strCodResponsableOT As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "", _
                                                Optional ByVal strFechaConclusion As String = "", _
                                                Optional ByVal strValRef As String = "", _
                                                Optional ByVal strCodPlanta As String = "", _
                                                Optional ByVal strCodZona As String = "", _
                                                Optional ByVal strDeTraContra As String = "") As DataTable

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            strDatatatable.TableName = "LOG00001_utbDetalle"
            lobjUtil(strDatatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_SERI_REQI", strTieneNumeroSerie, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "IDFE_TOPE_REQI", strFechaExpiracion, _
                                            "ISST_ATEN", strAtendido, _
                                            "ISTI_AUXI_EMPR", strTipoAuxliar, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISST_REQI_CONS", strRequiConst, _
                                            "ISNU_REQI_CONS", strRequiIsnu, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "ISCO_COMP", strCodigoComprador, _
                                            "ISNO_COMP", strNombreComprador, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_CREACION", strUsuarioCreacion, _
                                            "FECHA_CREACION", strFechaCreacion, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "FECHA_MODIFICACION", strFechaModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "OSNU_REQI", strNumeroRequiGenerado, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo, _
                                            "IDFE_EXPI_REQI", strFechaConclusion, _
                                            "pvch_ResponsableOT", strCodResponsableOT, _
                                            "pvch_ValRef", strValRef, _
                                            "pvch_CodPlanta", strCodPlanta, _
                                            "pvch_CodZona", strCodZona, _
                                            "pvch_DeTraContra", strDeTraContra}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.ObtenerDataTable("USP_QRY_INSERTAR_TCREQI_V4", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function
        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        Public Function Actualiza_Requisicion(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioModificacion As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "", _
                                                Optional ByVal strFechaTentativa As String = "") As Boolean

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo, _
                                            "VCH_FECHATENTATIVA ", strFechaTentativa}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.EjecutarComando("USP_QRY_ACTUALIZA_TCREQI", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function


        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        Public Function Actualiza_Requisicion_2(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioModificacion As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "", _
                                                Optional ByVal strFechaTentativa As String = "", _
                                                Optional ByVal strFechaConclusion As String = "") As Boolean

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo, _
                                            "VCH_FECHATENTATIVA ", strFechaTentativa, _
                                            "IDFE_EXPI_REQI ", strFechaConclusion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.EjecutarComando("USP_QRY_ACTUALIZA_TCREQI_V2", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        'CAMBIO DG : SE AGREGA EL CAMPO TIPO DE SERVICIO
        Public Function Actualiza_Requisicion_3(ByVal strEmpresa As String, _
                                                ByVal strUnidad As String, _
                                                ByVal strStiDocu As String, _
                                                ByVal strNumeroRequi As String, _
                                                ByVal strCodigoAlmacen As String, _
                                                ByVal strFechaEmision As String, _
                                                ByVal strCodigoAuxiliar As String, _
                                                ByVal strObservacion1 As String, _
                                                ByVal strObservacion2 As String, _
                                                ByVal strSituacion As String, _
                                                ByVal strServicio As String, _
                                                ByVal strOrdenServicio As String, _
                                                ByVal strArea As String, _
                                                ByVal strStock As Integer, _
                                                ByVal strUsuarioModificacion As String, _
                                                Optional ByVal strTipoServicio As String = "", _
                                                Optional ByVal strPresupuesto As String = "", _
                                                Optional ByVal strCodigoMotivo As String = "", _
                                                Optional ByVal strFechaTentativa As String = "", _
                                                Optional ByVal strFechaConclusion As String = "", _
                                                Optional ByVal strValRef As String = "", _
                                                Optional ByVal strCodPlanta As String = "", _
                                                Optional ByVal strCodZona As String = "", _
                                                Optional ByVal strDeTraContra As String = "") As Boolean

            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"ISCO_EMPR", strEmpresa, _
                                            "ISCO_UNID", strUnidad, _
                                            "ISTI_DOCU", strStiDocu, _
                                            "ISNU_REQI", strNumeroRequi, _
                                            "ISCO_ALMA", strCodigoAlmacen, _
                                            "IDFE_EMIS_REQI", strFechaEmision, _
                                            "ISCO_AUXI_EMPR", strCodigoAuxiliar, _
                                            "ISDE_OBSE_0001", strObservacion1, _
                                            "ISDE_OBSE_0002", strObservacion2, _
                                            "ISTI_SITU", strSituacion, _
                                            "ISST_SERV", strServicio, _
                                            "ISCO_ORDE_SERV", strOrdenServicio, _
                                            "CO_AREA_SOLI", strArea, _
                                            "ST_STOC", strStock, _
                                            "USUARIO_MODIFICACION", strUsuarioModificacion, _
                                            "TIPO_SERVICIO", strTipoServicio, _
                                            "CHR_PRESUPUESTO", strPresupuesto, _
                                            "CHR_CODIGO_MOTIVO", strCodigoMotivo, _
                                            "VCH_FECHATENTATIVA ", strFechaTentativa, _
                                            "IDFE_EXPI_REQI ", strFechaConclusion,
                                            "pvch_ValRef", strValRef,
                                            "pvch_CodPlanta", strCodPlanta,
                                            "pvch_CodZona", strCodZona,
                                            "pvch_DeTraContra", strDeTraContra}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.EjecutarComando("USP_QRY_ACTUALIZA_TCREQI_V3", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function


        Public Function Listar_reqi_Detalle(ByVal strNumeroDocumento As String, ByVal strcodigoEmpresa As String) As DataSet
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"nu_reqi", strNumeroDocumento, "co_empr", strcodigoEmpresa}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjCon.ObtenerDataSet("usp_qry_listar_detalleReqi", larrParams)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function EsquemaDetalle() As DataTable
            Dim ldtdRes As DataTable
            ldtdRes = New DataTable
            ldtdRes.Columns.Add("NU_REQI", GetType(String))
            ldtdRes.Columns.Add("NU_SECU", GetType(Integer))
            ldtdRes.Columns.Add("CO_ITEM", GetType(String))
            ldtdRes.Columns.Add("DE_ITEM", GetType(String))
            ldtdRes.Columns.Add("CO_UNME_COMP", GetType(String))
            ldtdRes.Columns.Add("CO_UNME", GetType(String))
            ldtdRes.Columns.Add("CA_SOLI", GetType(Double))
            ldtdRes.Columns.Add("CA_SOLI_ALMA", GetType(Double))
            ldtdRes.Columns.Add("CO_GRUP_SERV", GetType(String))
            ldtdRes.Columns.Add("CO_SERV", GetType(String))
            ldtdRes.Columns.Add("DE_SERV", GetType(String))
            ldtdRes.Columns.Add("CO_DEST_FINA", GetType(String))
            ldtdRes.Columns.Add("DE_UNID_DEST", GetType(String))
            ldtdRes.Columns.Add("TI_AUXI_EMPR", GetType(String))
            ldtdRes.Columns.Add("CO_AUXI_EMPR", GetType(String))
            ldtdRes.Columns.Add("NO_AUXI", GetType(String))
            ldtdRes.Columns.Add("DE_OBSE", GetType(String))
            ldtdRes.Columns.Add("CO_ORDE_SERV", GetType(String))
            ldtdRes.Columns.Add("DE_ACTI", GetType(String))
            ldtdRes.Columns.Add("CO_USUA_CREA", GetType(String))
            ldtdRes.Columns.Add("TI_SITU", GetType(String))
            ldtdRes.Columns.Add("Adjuntos", GetType(String))
            ldtdRes.Columns.Add("NU_ORTR", GetType(String))
            Return ldtdRes
        End Function

        Public Function Grabar() As Boolean
            mdtDetalle.TableName = "DETALLE"
        End Function

        Public Function fnc_listarestadisticasxarticulo(ByVal pinttipoconsulta As Int16) As DataSet
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjparametros() As Object = {"ptin_tipoconsulta", pinttipoconsulta, "pvch_codigoitem", mstrCodigo}
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjconexion.ObtenerDataSet("usp_log_articulo_estadisticos", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
        End Function

        Public Function Generar_PreAprobacion(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                            ByVal strNumeroDocumento As String, ByVal strFecha As String, _
                                            ByVal strObservacion As String, ByVal strEstadoSoli As String, _
                                            ByVal strFechaSolicitud As String, ByVal strJefaturaSoli As String, _
                                            ByVal strTipoAuxiliar As String, ByVal strCodigoAuxiliar As String, _
                                            ByVal strUsuario As String, ByVal strUsuarioModi As String, _
                                            Optional ByRef pdtCorreos As DataTable = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                                        "CO_APRO", strCodAprobacion, _
                                        "NU_DOCU", strNumeroDocumento, _
                                        "FE_DOCU", strFecha, _
                                        "OB_0001", strObservacion, _
                                        "ST_SOLI", strEstadoSoli, _
                                        "FE_STAT_SOLI", strFechaSolicitud, _
                                        "CO_JEFATURA_SOLI", strJefaturaSoli, _
                                        "TI_AUXI_EMPR", strTipoAuxiliar, _
                                        "CO_AUXI_EMPR", strCodigoAuxiliar, _
                                        "CO_USUA_CREA", strUsuario, _
                                        "CO_USUA_MODI", strUsuarioModi}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pdtCorreos = lobjCon.ObtenerDataTable("usp_LOG_Insertar_Soli_PreAprobacion_Reqi", Params)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Listar_Opciones_Modulos(ByVal strModulo As String, ByVal strOpcion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"vch_Modulo", strModulo,
                                          "vch_Opcion", strOpcion}

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("usp_qry_listar_opciones_modulos", larrParams)
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
        End Function

        Public Function GenerarCorreo_ComitePresupuesto(ByVal strEmpresa As String, ByVal strNumeroRequisicion As String) As Integer
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim intResult As Integer

            Dim larrParams() As Object = {"vch_Empresa", strEmpresa,
                                          "vch_NumRequisicion", strNumeroRequisicion}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                intResult = lobjCon.EjecutarComando("usp_LOG_Generar_Correo_Comite_Presupuesto", larrParams)
            Catch ex As Exception
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function registroVisitaTecnica(ByVal pstrOperacion As String,
                                              ByVal pstrNumRequi As String,
                                              ByVal pstrFecha As String,
                                              ByVal pstrHora As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim dt As DataTable = Nothing
            Try
                Dim lstrParametros() As String = {"pvchTipOperacion", pstrOperacion, _
                                                  "pvchNumRequi", pstrNumRequi, _
                                                  "pvchFecha", pstrFecha, _
                                                  "pvchHora", pstrHora}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dt = lobjCon.ObtenerDataTable("usp_log_registrar_horario_visita_tecnica", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return dt
        End Function
#End Region

    End Class

    Public Class Proveedores
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_ProveedoresListar"
        Private CONST_SP_BUSCAR = "usp_qry_ProveedoresBuscar"
        Private Const CONST_NOMBRE_TABLA_PROVEEDORES = "PROVEEDORES"
#End Region

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mstrRUC As String
#End Region

#Region "    Propiedades"
        Private Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
        Public Property RUC() As String
            Get
                RUC = mstrRUC
            End Get
            Set(ByVal Value As String)
                mstrRUC = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "    Metodos"
        Public Function Listar(ByRef pLista As DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"var_Empresa", Me.EmpresaCodigo, _
                                        "var_Codigo", Flags(0), _
                                        "var_Nombre", Flags(1)}

            Me.LimpiarError()
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pLista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR, larrParams)
                pLista.TableName = CONST_NOMBRE_TABLA_PROVEEDORES
                Me.Ok = True
            Catch ex As Exception
                pLista = Nothing
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"P_CO_EMPR", Me.EmpresaCodigo, "P_CO_PROV", mstrCodigo}

            Me.LimpiarError()
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable(CONST_SP_BUSCAR, larrParams)
                mstrNombre = ldtRes.Rows(0).Item("NO_CORT_PROV")
                mstrRUC = ldtRes.Rows(0).Item("NU_RUCS_PROV")
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
                'Me.Mensaje = New NuevoMundo.Generales.Clases.NMMensaje("OFISIS:OFILOGI:Proveedores.Buscar", _
                '        "", _
                '        "Error al consultar proveedor", _
                '        ex.Message, _
                '    Clases.NMMensaje.enuTiposMensajes.Error)
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose

        End Sub
#End Region

    End Class

    Public Class StockArticulo
#Region "Variables"
        Private _strCodigoCliente As String
        Private _strCodigoArticulo As String
        Private _dblTotalPedidoPrimera As Double
        Private _dblTotalPedidoP50 As Double
        Private _dblTotalPedidoSegunda As Double
        Private _dblTotalPedidoOB As Double
        Private _dblTotalGuiaPrimera As Double
        Private _dblTotalGuiaP50 As Double
        Private _dblTotalGuiaSegunda As Double
        Private _dblTotalGuiaOB As Double
        Private _dblTotalStockPrimera As Double
        Private _dblTotalStockP50 As Double
        Private _dblTotalStockSegunda As Double
        Private _dblTotalStockOB As Double
        Private _objConexion As AccesoDatosSQLServer

#End Region

#Region "Propiedades"
        Public Property CodigoCliente() As String
            Get
                Return _strCodigoCliente
            End Get
            Set(ByVal Value As String)
                _strCodigoCliente = Value
            End Set
        End Property
        Public Property CodigoArticulo() As String
            Get
                Return _strCodigoArticulo
            End Get
            Set(ByVal Value As String)
                _strCodigoArticulo = Value
            End Set
        End Property

        Public ReadOnly Property TotalPedidoPrimera() As Double
            Get
                Return _dblTotalPedidoPrimera
            End Get
        End Property

        Public ReadOnly Property TotalPedidoP50() As Double
            Get
                Return _dblTotalPedidoP50
            End Get
        End Property

        Public ReadOnly Property TotalPedidoSegunda() As Double
            Get
                Return _dblTotalPedidoSegunda
            End Get
        End Property

        Public ReadOnly Property TotalPedidoOB() As Double
            Get
                Return _dblTotalPedidoOB
            End Get
        End Property

        Public ReadOnly Property TotalGuiaPrimera() As Double
            Get
                Return _dblTotalGuiaPrimera
            End Get
        End Property

        Public ReadOnly Property TotalGuiaP50() As Double
            Get
                Return _dblTotalGuiaP50
            End Get
        End Property

        Public ReadOnly Property TotalGuiaSegunda() As Double
            Get
                Return _dblTotalGuiaSegunda
            End Get
        End Property

        Public ReadOnly Property TotalGuiaOB() As Double
            Get
                Return _dblTotalGuiaOB
            End Get
        End Property

        Public ReadOnly Property TotalStockPrimera() As Double
            Get
                Return _dblTotalStockPrimera
            End Get
        End Property

        Public ReadOnly Property TotalStockP50() As Double
            Get
                Return _dblTotalStockP50
            End Get
        End Property

        Public ReadOnly Property TotalStockSegunda() As Double
            Get
                Return _dblTotalStockSegunda
            End Get
        End Property

        Public ReadOnly Property TotalStockOB() As Double
            Get
                Return _dblTotalStockOB
            End Get
        End Property

#End Region

#Region "Constructores"
        Sub New()
            _strCodigoCliente = ""
            _strCodigoArticulo = ""
            _dblTotalPedidoPrimera = 0
            _dblTotalPedidoSegunda = 0
            _dblTotalPedidoOB = 0
            _dblTotalGuiaPrimera = 0
            _dblTotalGuiaSegunda = 0
            _dblTotalGuiaOB = 0
            _dblTotalStockPrimera = 0
            _dblTotalStockSegunda = 0
            _dblTotalStockOB = 0
        End Sub
#End Region

#Region "Metodos y Funciones"
        Function ObtenerDatos() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                Dim objParametros() As Object = {"p_var_CodigoCliente", _strCodigoCliente, "p_var_CodigoArticulo", _strCodigoArticulo}
                Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("usp_qry_ObtenerStockArticuloVsPedido", objParametros)
                If dtbDatos.Rows.Count > 0 Then
                    Me._dblTotalStockPrimera = dtbDatos.Compute("Sum(MetrosStockPrimera)", "")
                    Me._dblTotalStockP50 = dtbDatos.Compute("Sum(MetrosStockP50)", "")
                    Me._dblTotalStockSegunda = dtbDatos.Compute("Sum(MetrosStockSegunda)", "")
                    Me._dblTotalStockOB = dtbDatos.Compute("Sum(MetrosStockOB)", "")

                    Me._dblTotalPedidoPrimera = dtbDatos.Compute("Sum(MetrosPedidoPrimera)", "")
                    Me._dblTotalPedidoP50 = dtbDatos.Compute("Sum(MetrosPedidoP50)", "")
                    Me._dblTotalPedidoSegunda = dtbDatos.Compute("Sum(MetrosPedidoSegunda)", "")
                    Me._dblTotalPedidoOB = dtbDatos.Compute("Sum(MetrosPedidoOB)", "")

                    Me._dblTotalGuiaPrimera = dtbDatos.Compute("Sum(MetrosGuiaPrimera)", "")
                    Me._dblTotalGuiaP50 = dtbDatos.Compute("Sum(MetrosGuiaP50)", "")
                    Me._dblTotalGuiaSegunda = dtbDatos.Compute("Sum(MetrosGuiaSegunda)", "")
                    Me._dblTotalGuiaOB = dtbDatos.Compute("Sum(MetrosGuiaOB)", "")
                End If
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
    End Class

    Public Class Requisiciones
        Inherits Clases.General

#Region "   Variables"
        Private mstrCodigo As String = ""
        Private mdtDetalle As DataTable
        Private mobjEstados As cEstados
#End Region

#Region "   Enumeraciones"
        Public Enum enuTiposRequisicion
            [Articulos] = 0
            [Servicio] = 1
        End Enum
        Public Enum enuTiposLista
            [Aprobaciones] = 0
            [Seguimiento] = 1
            [OCOS] = 2
        End Enum
#End Region

#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Detalle() As DataTable
            Get
                Detalle = mdtDetalle
            End Get
            Set(ByVal Value As DataTable)
                mdtDetalle = Value
            End Set
        End Property
        Public Property Estados() As cEstados
            Get
                Estados = mobjEstados
            End Get
            Set(ByVal Value As cEstados)
                mobjEstados = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario

            Me.SP_LISTAR = "usp_LOG_Requisicion_Listar"
            mobjEstados = New cEstados(Me.EmpresaCodigo, Me.UsuarioBD)
        End Sub
#End Region

#Region "   Metodos"

        Public Function Actualizar() As Boolean

        End Function
        Public Function Consultar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                            "var_Requisicion", mstrCodigo, _
                                            "var_Usuario", Me.UsuarioBD}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_ConsultarV2", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Eliminar() As Boolean

        End Function
        Public Function Insertar() As Boolean

        End Function

        Public Function Listar(ByVal penuTipo As enuTiposLista, ByVal ParamArray pParametros() As String) As Boolean
            Select Case penuTipo
                Case enuTiposLista.Aprobaciones
                    ListarAprobacion(pParametros)
                Case enuTiposLista.Seguimiento
                    ListarSeguimiento_2(pParametros)
                Case enuTiposLista.OCOS
                    ListarOCOS(pParametros)
            End Select
        End Function

        Public Function fnc_ValidarCodigos(ByVal pstrAreasolicitante As String, ByVal pstrCuentagasto As String, ByVal pstrGruposervicio As String, ByVal pstrTiposervicio As String, ByVal pstrCentrocosto As String, ByVal pstrOrdenservicio As String, ByVal pstrArticulo As String) As DataTable
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer, ldtbRetornar As DataTable
            Try
                Dim lstrParametros() As String = { _
                "pvch_areasolicitante", pstrAreasolicitante, _
                "pvch_cuentagasto", pstrCuentagasto, _
                "pvch_gruposervicio", pstrGruposervicio, _
                "pvch_tiposervicio", pstrTiposervicio, _
                "pvch_centrocosto", pstrCentrocosto, _
                "pvch_ordenservicio", pstrOrdenservicio, _
                "pvch_articulo", pstrArticulo _
                }
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                ldtbRetornar = lobjConexion.ObtenerDataTable("usp_log_requisicion_validar", lstrParametros)
            Catch ex As Exception

            Finally
                lobjConexion = Nothing
            End Try
            Return ldtbRetornar
        End Function

        Public Function Rechazar(ByVal cAnular As String, ByVal cMotivo As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                                  "var_Requisicion", mstrCodigo, _
                                                  "var_Usuario", Me.UsuarioBD, _
                                                  "var_Anular", cAnular, _
                                                  "var_Motivo", cMotivo}
                'lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                'Me.Tabla = lobjCon.ObtenerDataTable("USP_SEG_REQUISICIONAPROBACION", lstrParametros)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_Rechazar", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Desaprobar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrXML As String
            Try
                Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                                    "var_Requisicion", mstrCodigo, _
                                                    "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.Tabla = lobjCon.ObtenerDataTable("usp_LOG_Requisicion_Desaprobar", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Aprobar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            lobjUtil(mdtDetalle).ToString(Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            lobjUtil = Nothing
            Try
                Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                                    "var_Requisicion", mstrCodigo, _
                                                    "ntx_XMLDetalle", lstrXML, _
                                                    "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_Aprobar", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Aprobar_2() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            lobjUtil(mdtDetalle).ToString(Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            lobjUtil = Nothing
            Try
                Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                                    "var_Requisicion", mstrCodigo, _
                                                    "ntx_XMLDetalle", lstrXML, _
                                                    "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_Aprobar_2", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function CotizacionIniciar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim larrParametros() = {"var_Empresa", Me.EmpresaCodigo, "var_NroRequisicion", mstrCodigo, "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_CotizacionIniciar", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function CotizacionAnular() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim larrParametros() = {"var_Empresa", Me.EmpresaCodigo, "var_NroRequisicion", mstrCodigo, "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_CotizacionAnular", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function BuscarOCOS() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim larrParametros() = {"chr_Empresa", Me.EmpresaCodigo, _
                                        "var_OrdenCompraServicio", mstrCodigo}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_OCOS_Buscar", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function AprobarOCOS() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim larrParametros() = {"var_Empresa", Me.EmpresaCodigo, _
                                        "var_Usuario", Me.UsuarioBD, _
                                        "var_OrdenCompraServicio", mstrCodigo}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_OrdenAprobar", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function DesaprobarOCOS(ByVal strObservacion As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldbtCorreos As DataTable
            Try
                Dim larrParametros() = {"chr_Empresa", Me.EmpresaCodigo, _
                                        "var_Usuario", Me.UsuarioBD, _
                                        "var_OrdenCompraServicio", mstrCodigo, _
                                        "var_Observacion", strObservacion}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldbtCorreos = lobjCon.ObtenerDataTable("usp_LOG_OCOS_Desaprobar", larrParametros)
            Catch ex As Exception
                ldbtCorreos = Nothing
            End Try
            Return ldbtCorreos
        End Function

        Private Function ListarOCOS(ByVal PParametros() As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim larrParametros() = {"chr_Empresa", Me.EmpresaCodigo, _
                                        "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Seguimiento_OCOS_V2", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            End Try
            Return Me.Ok
        End Function

        Private Function ListarAprobacion(ByVal pParametros() As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrEstado As String
                Dim lstrFechaInicio As String
                Dim lstrFechaFin As String
                Dim lstrSolicitador As String
                Dim lstrTipo As String
                Dim lstrObs As String
                If UBound(pParametros, 1) < 0 Then lstrEstado = "-1" Else lstrEstado = pParametros(0)
                If UBound(pParametros, 1) < 1 Then lstrFechaInicio = "" Else lstrFechaInicio = pParametros(1)
                If UBound(pParametros, 1) < 2 Then lstrFechaFin = "" Else lstrFechaFin = pParametros(2)
                If UBound(pParametros, 1) < 3 Then lstrSolicitador = "" Else lstrSolicitador = pParametros(3)
                If UBound(pParametros, 1) < 4 Then lstrTipo = "T" Else lstrTipo = pParametros(4)
                If UBound(pParametros, 1) < 5 Then lstrObs = "" Else lstrObs = pParametros(5)
                Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                                "var_Usuario", Me.UsuarioBD, _
                                                "var_FechaInicial", lstrFechaInicio, _
                                                "var_FechaFinal", lstrFechaFin, _
                                                "var_Solicitador", lstrSolicitador, _
                                                "var_Observaciones", lstrObs, _
                                                "var_TipoRequisicion", lstrTipo, _
                                                "var_EstadoRequisicion", lstrEstado}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Me.Tabla = lobjCon.ObtenerDataTable("usp_LOG_Requisicion_RequisicionesListar", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Tabla = Nothing
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Private Function ListarSeguimiento(ByVal PParametros() As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim lstrEstado As String
                Dim lstrFechaInicio As String
                Dim lstrFechaFin As String
                Dim lstrSolicitador As String
                Dim lintTipo As Integer
                Dim lstrXML As String
                Dim lstrObs As String
                Dim lstrProveedor As String
                If UBound(PParametros, 1) < 0 Then lstrEstado = "" Else lstrEstado = PParametros(0)
                If UBound(PParametros, 1) < 1 Then lstrFechaInicio = "" Else lstrFechaInicio = PParametros(1)
                If UBound(PParametros, 1) < 2 Then lstrFechaFin = "" Else lstrFechaFin = PParametros(2)
                If UBound(PParametros, 1) < 3 Then lstrSolicitador = "" Else lstrSolicitador = PParametros(3)
                If UBound(PParametros, 1) < 4 Then lintTipo = 0 Else lintTipo = PParametros(4)
                If UBound(PParametros, 1) < 5 Then lstrXML = "" Else lstrXML = PParametros(5)
                If UBound(PParametros, 1) < 6 Then lstrObs = "" Else lstrObs = PParametros(6)
                If UBound(PParametros, 1) < 7 Then lstrProveedor = "" Else lstrProveedor = PParametros(7)
                Dim larrParametros() = {"var_Empresa", Me.EmpresaCodigo, _
                                        "var_Usuario", Me.UsuarioBD, _
                                        "var_FechaInicio", lstrFechaInicio, _
                                        "var_FechaFin", lstrFechaFin, _
                                        "var_Solicitador", lstrSolicitador, _
                                        "int_Tipo", lintTipo, _
                                        "ntx_Filtros", lstrXML, _
                                        "var_Observaciones", lstrObs, _
                                        "var_Proveedor", lstrProveedor}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_SeguimientoListarV2", larrParametros)
                'Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_SeguimientoListarV2_1", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Private Function ListarSeguimiento_2(ByVal PParametros() As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Try
                Dim lstrEstado As String
                Dim lstrFechaInicio As String
                Dim lstrFechaFin As String
                Dim lstrSolicitador As String
                Dim lintTipo As Integer
                Dim lstrXML As String
                Dim lstrObs As String
                Dim lstrProveedor As String
                If UBound(PParametros, 1) < 0 Then lstrEstado = "" Else lstrEstado = PParametros(0)
                If UBound(PParametros, 1) < 1 Then lstrFechaInicio = "" Else lstrFechaInicio = PParametros(1)
                If UBound(PParametros, 1) < 2 Then lstrFechaFin = "" Else lstrFechaFin = PParametros(2)
                If UBound(PParametros, 1) < 3 Then lstrSolicitador = "" Else lstrSolicitador = PParametros(3)
                If UBound(PParametros, 1) < 4 Then lintTipo = 0 Else lintTipo = PParametros(4)
                If UBound(PParametros, 1) < 5 Then lstrXML = "" Else lstrXML = PParametros(5)
                If UBound(PParametros, 1) < 6 Then lstrObs = "" Else lstrObs = PParametros(6)
                If UBound(PParametros, 1) < 7 Then lstrProveedor = "" Else lstrProveedor = PParametros(7)
                Dim larrParametros() = {"var_Empresa", Me.EmpresaCodigo, _
                                        "var_Usuario", Me.UsuarioBD, _
                                        "var_FechaInicio", lstrFechaInicio, _
                                        "var_FechaFin", lstrFechaFin, _
                                        "var_Solicitador", lstrSolicitador, _
                                        "int_Tipo", lintTipo, _
                                        "ntx_Filtros", lstrXML, _
                                        "var_Observaciones", lstrObs, _
                                        "var_Proveedor", lstrProveedor}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_SeguimientoListarV3", larrParametros)
                'Me.SetDatos = lobjCon.ObtenerDataSet("usp_LOG_Requisicion_SeguimientoListarV2_1", larrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Anular_Requisicion(ByVal pstrEmpresa As String, ByVal pstrNumeroRequi As String, ByVal pstrUsuario As String) As DataTable
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As Object = { _
            "pvch_empresa", pstrEmpresa, _
            "pvch_requisicion", pstrNumeroRequi, _
            "pvch_usuario", pstrUsuario _
            }
            Try
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Return lobjConexion.ObtenerDataTable("usp_log_requisicion_anular", lstrParametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjConexion = Nothing
            End Try
        End Function

        Public Function obtenerTiposTrabajoServicio(ByVal pstrOperacion As String,
                                                    ByVal pstrNumRequi As String,
                                                    ByVal pintIDTipTrabajo As Integer,
                                                    ByVal pintIDRequisito As Integer,
                                                    ByVal pstrDescripcion As String,
                                                    ByVal pstrEstado As String,
                                                    ByVal pstrUsuario As String) As DataTable
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As Object = {"pvchOperacion", pstrOperacion,
                                              "pvchNumRequi", pstrNumRequi,
                                              "pintIDTipTrabajo", pintIDTipTrabajo,
                                              "pintIDRequisito", pintIDRequisito,
                                              "pvchDescripcion", pstrDescripcion,
                                              "pvchEstado", pstrEstado,
                                              "pvchUsuario", pstrUsuario}
            Dim ldtbAprobado As New DataTable
            Try
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtbAprobado = lobjConexion.ObtenerDataTable("USP_LOG_CRUD_TIP_TRABAJO_REQUISITO", lstrParametros)
            Catch ex As Exception
                ldtbAprobado = Nothing
            End Try
            Return ldtbAprobado
        End Function

        Public Function cargarTiposTrabajoServicio(ByVal pstrOperacion As String,
                                                   ByVal pstrNumRequi As String,
                                                   ByVal pintIDTipTrabajo As Integer,
                                                   ByVal pstrUsuario As String) As DataTable
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As Object = {"pvchOperacion", pstrOperacion,
                                              "pvchNumRequi", pstrNumRequi,
                                              "pintIDTipTrabajo", pintIDTipTrabajo,
                                              "pvchUsuario", pstrUsuario}
            Dim ldtbAprobado As New DataTable
            Try
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtbAprobado = lobjConexion.ObtenerDataTable("USP_LOG_TIP_TRABAJO_REQUI", lstrParametros)
            Catch ex As Exception
                ldtbAprobado = Nothing
            End Try
            Return ldtbAprobado
        End Function
        'Modificado:18-04-2011
        'Autor: Alexander Torres cardenas
        'Objetivo: Consulta si es el ultimo paso de aprobacion para enviar email al Proveedor

        Public Function Verifica_OC_Proveedor(ByVal strNumeroDocumento As String) As DataTable
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As Object = {"NumeroDocumento", strNumeroDocumento}
            Dim ldtbAprobado As New DataTable
            Try
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtbAprobado = lobjConexion.ObtenerDataTable("usp_LOG_OrdenCompraProveedor", lstrParametros)
            Catch ex As Exception
                ldtbAprobado = Nothing
            End Try
            Return ldtbAprobado
        End Function

        Public Function Lista_ValidacionCTC(ByVal strDatatable As DataTable) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXML As String
            Dim ldtRes As DataTable

            Try

                strDatatable.TableName = "LOG00001_utbDetalle"
                lobjUtil(strDatatable).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)

                Dim larrParams() As Object = {"P_NTX_XML", lstrXML}

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtRes = lobjCon.ObtenerDataTable("USP_VALIDA_EXCESO_CTC", larrParams)

                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally

                lobjCon = Nothing
                lobjUtil = Nothing

            End Try
        End Function
        'REQSIS201900029 - DG - INI
        Public Function Reiniciar() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrXML As String
            Dim dt As DataTable = Nothing
            Try
                Dim lstrParametros() As String = {"var_Empresa", Me.EmpresaCodigo, _
                                                    "var_Requisicion", mstrCodigo, _
                                                    "var_Usuario", Me.UsuarioBD}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dt = lobjCon.ObtenerDataTable("usp_reiniciar_proceso_requi", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return dt
        End Function
        'REQSIS201900029 - DG - FIN

        Public Function ObtenerAreas() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim dt As DataTable = Nothing
            Try
                Dim lstrParametros() As String = {"pvchTipBusqueda", "A", _
                                                  "pvchArea", ""}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dt = lobjCon.ObtenerDataTable("usp_log_obtener_zonas_trabajo", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return dt
        End Function

        Public Function ObtenerZonas(ByVal pstrPlanta As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim dt As DataTable = Nothing
            Try
                Dim lstrParametros() As String = {"pvchTipBusqueda", "Z", _
                                                  "pvchArea", pstrPlanta}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dt = lobjCon.ObtenerDataTable("usp_log_obtener_zonas_trabajo", lstrParametros)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return dt
        End Function

        
#End Region

#Region "   Clase Estados"
        Public Class cEstados
            Inherits Clases.General
            Implements Interfases.INM

            Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
                Me.EmpresaCodigo = pstrEmpresa
                Me.UsuarioBD = pstrUsuario

                Me.SP_LISTAR = "usp_LOG_AprobacionesFiltros_ListarV2"
            End Sub


            Public Function Actualizar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Actualizar

            End Function
            Public Function Consultar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Consultar

            End Function
            Public Function Eliminar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Eliminar

            End Function
            Public Function Insertar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Insertar

            End Function
            Public Function Listar(ByVal ParamArray pParametros() As String) As Boolean Implements NuevoMundo.Generales.Interfases.INM.Listar
                Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

                Try
                    Dim larrParametros() = {"var_Usuario", Me.UsuarioBD}
                    lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                    Me.SetDatos = lobjCon.ObtenerDataSet(Me.SP_LISTAR, larrParametros)
                    Me.Ok = True
                Catch ex As Exception
                    Me.Ok = False
                Finally
                    lobjCon = Nothing
                End Try
                Return Me.Ok
            End Function
        End Class
#End Region

    End Class

    Public Class Muestras_Telas
#Region "--Variables --"
        'Cabecera de Solictud de Muestras
        Protected Friend mstrError As String = ""
        Protected Friend mstrNumeroSolicitud As String = ""
        Protected Friend mstrFecha As String
        Protected Friend mstrCodigoCliente As String
        Protected Friend mstrTipoMuestra As String
        Protected Friend mstrCodigoAlmacen As String
        Protected Friend mstrEstadoSolicitud As String
        Protected Friend mstrEstadoDocumento As String
        Protected Friend mstrObservaciones As String
        Protected Friend mstrUsuario As String

        Protected Friend mstrNumeroSolicitud_Detalle As String
        Protected Friend mstrNumeroSecuencial As String
        Protected Friend mstrCodigo_Articulo As String
        Protected Friend mstrUnidad_Medida As String
        Protected Friend mstrCantidad_Solicitada As Double
        Protected Friend mstrCantidad_Aprobada As Double
        Protected Friend mstrEstado_Detalle As String
        Protected Friend mstrUsuario_Detalle As String
        'CAMBIO DG AGREGAR PRECIO - INI
        Protected Friend mstrPrecio As Decimal
        Protected Friend mstrTipoPagoMuestra As String
        'CAMBIO DG AGREGAR PRECIO - FIN
#End Region
#Region " Propiedades Cabecera UTB_SOLICITUDMUESTRAS"
        Public ReadOnly Property clsError() As String
            Get
                Return mstrError
            End Get
        End Property
        Public Property NumeroSolicitud() As String
            Get
                NumeroSolicitud = mstrNumeroSolicitud
            End Get
            Set(ByVal Value As String)
                mstrNumeroSolicitud = Value
            End Set
        End Property
        Public Property FechaSolicitud() As String
            Get
                NumeroSolicitud = mstrFecha
            End Get
            Set(ByVal Value As String)
                mstrFecha = Value
            End Set
        End Property
        Public Property CodigoCliente() As String
            Get
                CodigoCliente = mstrCodigoCliente
            End Get
            Set(ByVal Value As String)
                mstrCodigoCliente = Value
            End Set
        End Property
        Public Property TipoMuestra() As String
            Get
                TipoMuestra = mstrTipoMuestra
            End Get
            Set(ByVal Value As String)
                mstrTipoMuestra = Value
            End Set
        End Property
        Public Property CodigoAlmacen() As String
            Get
                CodigoAlmacen = mstrCodigoAlmacen
            End Get
            Set(ByVal Value As String)
                mstrCodigoAlmacen = Value
            End Set
        End Property
        Public Property EstadoSolicitud() As String
            Get
                EstadoSolicitud = mstrEstadoSolicitud
            End Get
            Set(ByVal Value As String)
                mstrEstadoSolicitud = Value
            End Set
        End Property
        Public Property TipoPagoMuestra() As String
            Get
                TipoPagoMuestra = mstrTipoPagoMuestra
            End Get
            Set(ByVal Value As String)
                mstrTipoPagoMuestra = Value
            End Set
        End Property
        Public Property EstadoDocumento() As String
            Get
                EstadoDocumento = mstrEstadoDocumento
            End Get
            Set(ByVal Value As String)
                mstrEstadoDocumento = Value
            End Set
        End Property
        Public Property Observaciones() As String
            Get
                Observaciones = mstrObservaciones
            End Get
            Set(ByVal Value As String)
                mstrObservaciones = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Usuario = mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property
#End Region

#Region " Propiedades Detalle UTB_DETALLESOLICITUDMUESTRAS"

        Public Property NumeroSolicitud_Detalle() As String
            Get
                NumeroSolicitud_Detalle = mstrNumeroSolicitud_Detalle
            End Get
            Set(ByVal Value As String)
                mstrNumeroSolicitud_Detalle = Value
            End Set
        End Property
        Public Property NumeroSecuencia() As String
            Get
                NumeroSecuencia = mstrNumeroSecuencial
            End Get
            Set(ByVal Value As String)
                mstrNumeroSecuencial = Value
            End Set
        End Property
        Public Property Codigo_Articulo() As String
            Get
                Codigo_Articulo = mstrCodigo_Articulo
            End Get
            Set(ByVal Value As String)
                mstrCodigo_Articulo = Value
            End Set
        End Property
        Public Property Unidad_Medida() As String
            Get
                Unidad_Medida = mstrUnidad_Medida
            End Get
            Set(ByVal Value As String)
                mstrUnidad_Medida = Value
            End Set
        End Property
        Public Property Cantidad_Solicitada() As Double
            Get
                Cantidad_Solicitada = mstrCantidad_Solicitada
            End Get
            Set(ByVal Value As Double)
                mstrCantidad_Solicitada = Value
            End Set
        End Property
        'CAMBIO DG AGREGAR PRECIO - INI
        Public Property Precio() As Decimal
            Get
                Precio = mstrPrecio
            End Get
            Set(ByVal Value As Decimal)
                mstrPrecio = Value
            End Set
        End Property
        'CAMBIO DG AGREGAR PRECIO - FIN
        Public Property Cantidad_Aprobada() As Double
            Get
                Cantidad_Aprobada = mstrCantidad_Aprobada
            End Get
            Set(ByVal Value As Double)
                mstrCantidad_Aprobada = Value
            End Set
        End Property
        Public Property Estado_Detalle() As String
            Get
                Estado_Detalle = mstrEstado_Detalle
            End Get
            Set(ByVal Value As String)
                mstrEstado_Detalle = Value
            End Set
        End Property
        Public Property Usuario_Detalle() As String
            Get
                Usuario_Detalle = mstrUsuario_Detalle
            End Get
            Set(ByVal Value As String)
                mstrUsuario_Detalle = Value
            End Set
        End Property
#End Region

#Region "-- Metodos --"
        Public Function SolicitudMuestras_Insertar() As String
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      21-09-2010
            'Proposito :      Permite registrar una solicItud de muestras de tela por parte de los vendedores para sus clientes
            '*******************************************************************************************
            Dim blnRpta As Boolean
            Dim strRequisicion As String
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pdtm_FechaSolictud", mstrFecha, _
                                            "pvar_CodigoCliente", mstrCodigoCliente, _
                                            "pchr_TipoMuestra", mstrTipoMuestra, _
                                            "pchr_CodigoAlmacen", mstrCodigoAlmacen, _
                                            "pchr_EstadoSolicitud", mstrEstadoSolicitud, _
                                            "pchr_EstadoDocumento", mstrEstadoDocumento, _
                                            "pvar_Observaciones", mstrObservaciones, _
                                            "pvar_TipoPagoMuestra", mstrTipoPagoMuestra, _
                                            "pvar_UsuarioCreacion", mstrUsuario, _
                                            "pvar_NumeroSolicitud", ""}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                strRequisicion = Conexion.ObtenerValor("USP_LOG_SOLICITUDMUESTRAS_INSERTAR", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return strRequisicion
        End Function
        Public Function Detalle_SolicitudMuestras_Listar(ByRef pDT As DataTable) As Boolean
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      22-06-2010
            'Proposito :      lista el detalle de una solictud de muestras
            '*******************************************************************************************

            Dim blnRpta As Boolean
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pVar_NumeroSolicitud", mstrNumeroSolicitud}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT = Conexion.ObtenerDataTable("USP_LOG_DETALLESOLICITUDMUESTRAS_LISTAR", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function
        Public Function Detalle_SolicitudMuestras_Listar_v2(ByRef pDT As DataTable) As Boolean
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      22-06-2010
            'Proposito :      lista el detalle de una solictud de muestras
            '*******************************************************************************************

            Dim blnRpta As Boolean
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pVar_NumeroSolicitud", mstrNumeroSolicitud}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT = Conexion.ObtenerDataTable("USP_LOG_DETALLESOLICITUDMUESTRAS_LISTAR_V2", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function

        Public Function Detalle_SolicitudMuestras_Insertar() As String
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      29-06-2010
            'Proposito :      Permite registrar el detalle de una solicitud de muestras
            '*******************************************************************************************
            Dim blnRpta As Boolean

            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pVar_NumeroSolicitud", mstrNumeroSolicitud_Detalle, _
                                            "pvar_CodigoArticulo", mstrCodigo_Articulo, _
                                            "pchr_UnidadMedida", mstrUnidad_Medida, _
                                            "pnum_CantidadSolicitud", mstrCantidad_Solicitada, _
                                            "pnum_CantidadAprobada", mstrCantidad_Aprobada, _
                                            "pchr_EstadoDetalle", mstrEstado_Detalle, _
                                            "pvar_UsuarioCreacion", mstrUsuario_Detalle}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Conexion.EjecutarComando("USP_LOG_DETALLESOLICITUDMUESTRAS_INSERTAR", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function
        'CAMBIO DG AGREGAR PRECIO - INI
        Public Function Detalle_SolicitudMuestras_Insertar_V2() As String
            Dim blnRpta As Boolean

            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pVar_NumeroSolicitud", mstrNumeroSolicitud_Detalle, _
                                            "pvar_CodigoArticulo", mstrCodigo_Articulo, _
                                            "pchr_UnidadMedida", mstrUnidad_Medida, _
                                            "pnum_CantidadSolicitud", mstrCantidad_Solicitada, _
                                            "pnum_CantidadAprobada", mstrCantidad_Aprobada, _
                                            "pnum_Precio", mstrPrecio, _
                                            "pchr_EstadoDetalle", mstrEstado_Detalle, _
                                            "pvar_UsuarioCreacion", mstrUsuario_Detalle}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Conexion.EjecutarComando("USP_LOG_DETALLESOLICITUDMUESTRAS_INSERTAR_V2", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function
        'CAMBIO DG AGREGAR PRECIO - FIN
        Public Function Detalle_SolicitudMuestras_Eliminar() As Boolean
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      14-09-2010
            'Proposito :      Elimina logicamente un detalle de una solicitud de muestras
            '*******************************************************************************************
            Dim blnRpta As Boolean
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pVar_NumeroSolicitud", mstrNumeroSolicitud_Detalle, _
                                            "pInt_Secuencia", mstrNumeroSecuencial, _
                                            "pvar_UsuarioModificacion", mstrUsuario_Detalle}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Conexion.EjecutarComando("USP_LOG_DETALLESOLICITUDMUESTRAS_ELIMINAR", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function
        Public Function SolicitudMuestras_Eliminar() As Boolean
            '*******************************************************************************************
            'Creado por:	  Darwin Ccorahua Livon
            'Fecha     :      10-11-2010
            'Proposito :      Elimina logicamente un Solicitud de muestras mientras las condiciones no sean atendidas
            '*******************************************************************************************
            Dim blnRpta As Boolean
            Dim Conexion As AccesoDatosSQLServer
            Dim objParametro() As Object = {"pVar_NumeroSolicitud", mstrNumeroSolicitud, _
                                            "pvar_UsuarioModificacion", mstrUsuario}
            Try
                mstrError = ""
                blnRpta = True
                Conexion = New AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Conexion.EjecutarComando("USP_LOG_SOLICITUDMUESTRAS_ELIMINAR", objParametro)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                Conexion = Nothing
            End Try
            Return blnRpta
        End Function
        Public Function ListarAprobacion_SolicitudMuestras(ByRef pDT As DataTable) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pchr_Empresa", "01", _
                                        "pvar_Usuario", mstrUsuario}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT = lobjCon.ObtenerDataTable("USP_LOG_SEGUIMIENTOMUESTRAS", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function
        Public Function Detalle_SolicitudMuestras_Mostrar(ByRef pDT As DataSet) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_NumeroSolicitud", mstrNumeroSolicitud}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT = lobjCon.ObtenerDataSet("USP_LOG_SOLICITUDMUESTRA_MOSTRAR", larrParametros)
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function
        'CAMBIO DG AGREGAR PRECIO - INI
        Public Function Detalle_SolicitudMuestras_Mostrar_V2(ByRef pDT As DataSet) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_NumeroSolicitud", mstrNumeroSolicitud}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT = lobjCon.ObtenerDataSet("USP_LOG_SOLICITUDMUESTRA_MOSTRAR_V2", larrParametros)
                blnRpta = True
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function
        'CAMBIO DG AGREGAR PRECIO - FIN
        Public Function ListarDespachos_SolicitudMuestras(ByRef pDT As DataTable) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pchr_Empresa", "01", _
                                        "pvar_Usuario", mstrUsuario}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT = lobjCon.ObtenerDataTable("USP_LOG_DESPACHOSMUESTRAS_LISTAR", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function ListarDespacho_SolicitudMuestrasPend(ByRef pDT As DataTable) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pchr_Empresa", "01", _
                                        "pvar_Usuario", mstrUsuario}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT = lobjCon.ObtenerDataTable("USP_LOG_DESPACHOMUESTRASPEND_LISTAR", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function ListarDetalleSolicitudMuestraHeader(ByRef pDT_Header As DataTable) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_numerosolicitud", NumeroSolicitud, _
                                        "pvar_tipoLista", "0"}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT_Header = lobjCon.ObtenerDataTable("USP_LOG_SOLMUESTRA_DET", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function
        '
        Public Function ListarSolicitudMuestraHeader(ByVal strNumSolicitud As String) As DataTable
            Dim dt As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_numerosolicitud", strNumSolicitud}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                dt = lobjCon.ObtenerDataTable("USP_OBTENER_DATOS_CABECERA_MUESTRA", larrParametros)
            Catch ex As Exception
                dt = Nothing
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
            Return dt
        End Function
        Public Function ListarDetalleSolicitudMuestra(ByRef pDT As DataTable) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_numerosolicitud", NumeroSolicitud, _
                                        "pvar_tipoLista", "1"}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT = lobjCon.ObtenerDataTable("USP_LOG_SOLMUESTRA_DET", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function ListarDetalleSolicitudMuestraValid(ByRef pDT_Valid As DataTable) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_numerosolicitud", NumeroSolicitud, _
                                        "pvar_tipoLista", "2"}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT_Valid = lobjCon.ObtenerDataTable("USP_LOG_SOLMUESTRA_DET", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function FunEvaluaArtLote(ByRef pDT_Valid As DataTable, _
                                    ByVal pvar_co_item As String, _
                                    ByVal pvar_co_lote As String) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_numerosolicitud", "", _
                                        "pvar_tipoLista", "3", _
                                        "pvar_co_item", pvar_co_item, _
                                        "pvar_co_lote", pvar_co_lote}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT_Valid = lobjCon.ObtenerDataTable("USP_LOG_SOLMUESTRA_DET", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function FunGrabarSalidaMuestra(ByVal pvar_ISNU_DOCU As String, _
                                                    ByVal pvar_ISCO_AUXI_EMPR As String, _
                                                    ByRef dtbDatos As DataTable, _
                                                    ByVal pvar_numerosolicitud As String) As String


            Dim strXMLDatos As String
            Dim vrResProc As String = ""
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim pDT_Res As New DataTable

            dtbDatos.TableName = "DETALLE"
            lobjUtil(dtbDatos).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, strXMLDatos)


            Try
                Dim larrParametros() = {"pvar_ISNU_DOCU", pvar_ISNU_DOCU, _
                                        "pvar_ISCO_AUXI_EMPR", pvar_ISCO_AUXI_EMPR, _
                                        "var_XMLDatos", strXMLDatos, _
                                        "pvar_numerosolicitud", pvar_numerosolicitud}

                Dim vrCadCone As String = NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT_Res = lobjCon.ObtenerDataTable("USP_LOG_GRABASALIDAMUESTRAS", larrParametros)
                vrResProc = pDT_Res.Rows(0)(0)

            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
                vrResProc = "-|-Errores"
            Finally
                lobjCon = Nothing
                pDT_Res.Dispose()
                pDT_Res = Nothing
            End Try

            Return vrResProc

        End Function
        '------------------------------------------------------------------------------------------------------------------
        'REQSIS201800043 - DG CAMBIO LAS MUESTRAS SE FACTURAN - INI
        '------------------------------------------------------------------------------------------------------------------
        Public Function FunGrabarSalidaMuestra_GUR(ByVal pvar_ISNU_DOCU As String, _
                                                            ByVal pvar_ISCO_AUXI_EMPR As String, _
                                                            ByRef dtbDatos As DataTable, _
                                                            ByVal pvar_numerosolicitud As String) As String


            Dim strXMLDatos As String
            Dim vrResProc As String = ""
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim pDT_Res As New DataTable

            dtbDatos.TableName = "DETALLE"
            lobjUtil(dtbDatos).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, strXMLDatos)


            Try
                Dim larrParametros() = {"pvar_ISNU_DOCU", pvar_ISNU_DOCU, _
                                        "pvar_ISCO_AUXI_EMPR", pvar_ISCO_AUXI_EMPR, _
                                        "var_XMLDatos", strXMLDatos, _
                                        "pvar_numerosolicitud", pvar_numerosolicitud}

                Dim vrCadCone As String = NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT_Res = lobjCon.ObtenerDataTable("USP_LOG_GRABASALIDAMUESTRAS_GUR", larrParametros)
                vrResProc = pDT_Res.Rows(0)(0)

            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
                vrResProc = "-|-Errores"
            Finally
                lobjCon = Nothing
                pDT_Res.Dispose()
                pDT_Res = Nothing
            End Try

            Return vrResProc

        End Function
        '------------------------------------------------------------------------------------------------------------------
        'REQSIS201800043 - DG CAMBIO LAS MUESTRAS SE FACTURAN - FIN
        '------------------------------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------------------------------
        'REQSIS201800043 - DG - INI
        '------------------------------------------------------------------------------------------------------------------
        Public Function FunGrabarSalidaMuestraGUR(ByVal pvar_ISNU_DOCU As String, ByVal pvar_ISCO_AUXI_EMPR As String, ByRef dtbDatos As DataTable, ByVal pvar_numerosolicitud As String) As String
            Dim strXMLDatos As String
            Dim vrResProc As String = ""
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim pDT_Res As New DataTable
            dtbDatos.TableName = "DETALLE"
            lobjUtil(dtbDatos).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, strXMLDatos)

            Try
                Dim larrParametros() = {"pvar_ISNU_DOCU", pvar_ISNU_DOCU, _
                                        "pvar_ISCO_AUXI_EMPR", pvar_ISCO_AUXI_EMPR, _
                                        "var_XMLDatos", strXMLDatos, _
                                        "pvar_numerosolicitud", pvar_numerosolicitud}

                Dim vrCadCone As String = NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                pDT_Res = lobjCon.ObtenerDataTable("USP_LOG_GRABASALIDAMUESTRAS_ETIQUETA", larrParametros)
                vrResProc = pDT_Res.Rows(0)(0)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
                vrResProc = "-|-Errores"
            Finally
                lobjCon = Nothing
                pDT_Res.Dispose()
                pDT_Res = Nothing
            End Try
            Return vrResProc
        End Function
        '------------------------------------------------------------------------------------------------------------------
        'REQSIS201800043 - DG - FIN
        '------------------------------------------------------------------------------------------------------------------
        Public Function ConsultarSolicitudMuestra(ByRef pDT As DataTable, _
                                            ByVal pvar_CO_AUXI_EMPR As String, _
                                            ByVal par_fecini As String, _
                                            ByVal par_fecfin As String) As Boolean
            Dim blnRpta As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim larrParametros() = {"pvar_CO_AUXI_EMPR", pvar_CO_AUXI_EMPR, _
                                        "par_fecini", par_fecini, _
                                        "par_fecfin", par_fecfin}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                pDT = lobjCon.ObtenerDataTable("USP_LOG_CONSULTARSOLMUESTRA_LISTAR", larrParametros)
            Catch ex As Exception
                blnRpta = False
                mstrError = ex.Message
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Function GeneraXml(ByVal dtDatos As DataTable) As String
            Dim xmlDOM As New XmlDocument
            Dim nodo, nodoChild As XmlElement
            With xmlDOM
                .Load(New StringReader("<root></root>"))
                For i As Integer = 0 To dtDatos.Rows.Count - 1
                    nodo = .CreateElement(dtDatos.TableName)
                    For j As Integer = 0 To dtDatos.Columns.Count - 1
                        If Not IsDBNull(dtDatos.Rows(i)(j)) Then
                            nodoChild = .CreateElement(dtDatos.Columns(j).ColumnName)
                            nodoChild.InnerText = Trim(CType(dtDatos.Rows(i)(j), String))
                            nodo.AppendChild(nodoChild)
                        End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return EncodeXML(.OuterXml())
            End With
        End Function


        Function EncodeXML(ByVal Texto As String) As String

            Texto = Replace(Texto, "&", "XYZ1", )

            Dim tramaCar() As String = {"—", "¡", "¢", "£", "¤", "¥", "¦", "§", "¨", "©", "ª", "«", "¬", "®", "¯", "°", "±", "²", "³", _
                                        "´", "µ", "¶", "·", "¸", "¹", "º", "»", "¼", "½", "¾", "¿", "À", "Á", "Â", "Ã", "Ä", "Å", "Æ", "Ç", _
                                        "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", "×", "Ø", "Ù", "Ú", "Û", _
                                        "Ü", "Ý", "Þ", "ß", "à", "á", "â", "ã", "ä", "å", "æ", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", _
                                        "ð", "ñ", "ò", "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü", "ý", "þ", "ÿ"}

            Dim tramaVal() As String = {"151", "161", "162", "163", "164", "165", "166", "167", "168", "169", "170", "171", "172", "174", "175", "176", "177", "178", "179", _
                                        "180", "181", "182", "183", "184", "185", "186", "187", "188", "189", "190", "191", "192", "193", "194", "195", "196", "197", "198", "199", _
                                        "200", "201", "202", "203", "204", "205", "206", "207", "208", "209", "210", "211", "212", "213", "214", "215", "216", "217", "218", "219", _
                                        "220", "221", "222", "223", "224", "225", "226", "227", "228", "229", "230", "231", "232", "233", "234", "235", "236", "237", "238", "239", _
                                        "240", "241", "242", "243", "244", "245", "246", "247", "248", "249", "250", "251", "252", "253", "254", "255"}
            Dim iPos As Integer
            Dim sVal As String

            For Each car As String In tramaCar
                sVal = tramaVal(iPos)
                Texto = Replace(Texto, car, "&#" & sVal & ";")
                iPos = iPos + 1
            Next

            Texto = Replace(Texto, "XYZ1", "&#38;")


            Return Texto
        End Function


        Function ReemplazarTexto(ByVal Texto As String) As String
            Dim trama() As String = {"°", "*", "Ø", "¹", "¨", "í", "i", "á", "a", "ú", "u", "ó", "o", "é", "e", "Ñ", "N", "ñ", "n", UCase("í"), "I", UCase("á"), "A", UCase("ú"), "U", UCase("ó"), "O", UCase("é"), "E"}
            Dim objAscii As Encoding = Encoding.ASCII
            Dim i As Integer
            Dim lstrTexto1 As String = ""
            Dim lstrTexto2 As String = ""

            For i = 0 To ((UBound(trama, 1) - 1) / 2)
                lstrTexto1 = trama(2 * i)
                lstrTexto2 = trama((2 * i) + 1)
                Texto = Replace(Texto, lstrTexto1, lstrTexto2)
            Next
            Return Texto
        End Function

#End Region

    End Class
End Namespace
