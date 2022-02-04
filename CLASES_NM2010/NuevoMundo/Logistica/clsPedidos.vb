Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Data.OleDb
Imports NM.AccesoDatos
Imports NM_General

Namespace Logistica
    Public Class clsPedidos


#Region "Variables"
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "Propiedades"

        Private _SeriePedido As String
        Private _NumPedido As String
        Private _CodAlmacen As String
        Private _DesAlmacen As String
        Private _CodSolicitante As String
        Private _DesSolicitante As String
        Private _CodUsuarioSolicitante As String
        Private _CodCentroCostos As String
        Private _DesCentroCostos As String
        Private _CodCuentaGastos As String
        Private _DesCuentaGastos As String
        Private _CodOrdenServicio As String
        Private _DesOrdenServicio As String
        Private _FecPedido As String
        Private _FecAprobacion As String
        Private _FecAtencion As String
        Private _EstadoPedido As String
        Private _ObsPedido As String

        Public Property SeriePedido() As String
            Get
                Return _SeriePedido
            End Get
            Set(ByVal Value As String)
                _SeriePedido = Value
            End Set
        End Property

        Public Property NumPedido() As String
            Get
                Return _NumPedido
            End Get
            Set(ByVal value As String)
                _NumPedido = value
            End Set
        End Property

        Public Property CodAlmacen() As String
            Get
                Return _CodAlmacen
            End Get
            Set(ByVal value As String)
                _CodAlmacen = value
            End Set
        End Property

        Public Property DesAlmacen() As String
            Get
                Return _DesAlmacen
            End Get
            Set(ByVal value As String)
                _DesAlmacen = value
            End Set
        End Property

        Public Property CodSolicitante() As String
            Get
                Return _CodSolicitante
            End Get
            Set(ByVal Value As String)
                _CodSolicitante = Value
            End Set
        End Property

        Public Property DesSolicitante() As String
            Get
                Return _DesSolicitante
            End Get
            Set(ByVal Value As String)
                _DesSolicitante = Value
            End Set
        End Property

        Public Property CodUsuarioSolicitante() As String
            Get
                Return _CodUsuarioSolicitante
            End Get
            Set(ByVal Value As String)
                _CodUsuarioSolicitante = Value
            End Set
        End Property

        Public Property CodCentroCostos() As String
            Get
                Return _CodCentroCostos
            End Get
            Set(ByVal Value As String)
                _CodCentroCostos = Value
            End Set
        End Property

        Public Property DesCentroCostos() As String
            Get
                Return _DesCentroCostos
            End Get
            Set(ByVal Value As String)
                _DesCentroCostos = Value
            End Set
        End Property

        Public Property CodCuentaGastos() As String
            Get
                Return _CodCuentaGastos
            End Get
            Set(ByVal Value As String)
                _CodCuentaGastos = Value
            End Set
        End Property

        Public Property DesCuentaGastos() As String
            Get
                Return _DesCuentaGastos
            End Get
            Set(ByVal Value As String)
                _DesCuentaGastos = Value
            End Set
        End Property

        Public Property CodOrdenServicio() As String
            Get
                Return _CodOrdenServicio
            End Get
            Set(ByVal Value As String)
                _CodOrdenServicio = Value
            End Set
        End Property

        Public Property DesOrdenServicio() As String
            Get
                Return _DesOrdenServicio
            End Get
            Set(ByVal Value As String)
                _DesOrdenServicio = Value
            End Set
        End Property


        Public Property FecPedido() As String
            Get
                Return _FecPedido
            End Get
            Set(ByVal value As String)
                _FecPedido = value
            End Set
        End Property

        Public Property FecAprobacion() As String
            Get
                Return _FecAprobacion
            End Get
            Set(ByVal value As String)
                _FecAprobacion = value
            End Set
        End Property

        Public Property FecAtencion() As String
            Get
                Return _FecAtencion
            End Get
            Set(ByVal value As String)
                _FecAtencion = value
            End Set
        End Property

        Public Property EstadoPedido() As String
            Get
                Return _EstadoPedido
            End Get
            Set(ByVal value As String)
                _EstadoPedido = value
            End Set
        End Property

        Public Property ObsPedido() As String
            Get
                Return _ObsPedido
            End Get
            Set(ByVal value As String)
                _ObsPedido = value
            End Set
        End Property
#End Region

        Public Function fnEliminarPedArticulosDistribG(ByVal p_strNuPedi As String,
                                                       ByVal p_strCoItem As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"NU_PEDI", p_strNuPedi,
                                                 "CO_ITEM", p_strCoItem}
                strConsultaPedido = "USP_LOG_ELIMINAR_DISTRIBUCION_GEN"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fnListadoArticulosDistribG(ByVal p_strAux As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"pvchAux", p_strAux}
                strConsultaPedido = "USP_LOG_LISTADO_PEDI_DISTRIBUCION_G"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Validar si hay stock para distribucion
        Public Function fnValidarStockDistribucion(ByVal p_strCoItem As String) As DataTable
            Dim strConsultaStrock As String
            Dim ldtbCabeceraStrock As DataTable
            Try
                ldtbCabeceraStrock = New DataTable
                Dim objParametros As Object() = {"CO_ITEM", p_strCoItem}
                strConsultaStrock = "USP_VALIDAR_STOCK_DISTRIBUCION"
                ldtbCabeceraStrock = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaStrock, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraStrock
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fnGuardarPedidoDistribucion(ByVal p_strNuPedi As String,
                                                    ByVal p_strCoItem As String,
                                                    ByVal p_dblMtsR As Double,
                                                    ByVal p_strUsuario As String,
                                                    ByVal p_strMtsPedido As Double,
                                                    ByVal p_strMtsPediente As Double,
                                                    ByVal p_strMtsAtendido As Double,
                                                    ByVal p_strMtsDistribuido As Double,
                                                    ByVal p_esTotal As Boolean,
                                                    ByVal p_idDistribucion As Integer) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"NU_PEDI", p_strNuPedi,
                                                 "CO_ITEM", p_strCoItem,
                                                 "MTS_REP", p_dblMtsR,
                                                 "COD_USUA", p_strUsuario,
                                                 "MTS_PEDIDO", p_strMtsPedido,
                                                 "MTS_PENDIENTE", p_strMtsPediente,
                                                 "MTS_ATENDIDO", p_strMtsAtendido,
                                                 "MTS_DISTRIBUIDO", p_strMtsDistribuido,
                                                 "ES_TOTAL", p_esTotal,
                                                 "ID_DISTRIBUCION", p_idDistribucion}
                strConsultaPedido = "USP_LOG_GRABAR_PEDIDO_DISTRIBUCION_MODI"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta de clientes si / no línea de crédito
        Public Function fnListarLineaCreditoPorCliente(ByVal p_strCoCliente As String) As DataTable
            Dim strConsultaLineaCredito As String
            Dim ldtbCabeceraLineaCredito As DataTable
            Try
                ldtbCabeceraLineaCredito = New DataTable
                Dim objParametros As Object() = {"vch_CodCliente", p_strCoCliente}
                strConsultaLineaCredito = "USP_LISTAR_LINEA_CREDITO_POR_CLIENTE"
                ldtbCabeceraLineaCredito = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis).ObtenerDataTable(strConsultaLineaCredito, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbCabeceraLineaCredito
        End Function

        ' --- Suma Total Pedido de distribucion sin programacion
        Public Function fnSumaTotalPedidoDistrib_SinProgramacion(ByVal p_strCodArt As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try

                Dim objParametros As Object() = {"CO_ITEM", p_strCodArt}
                strConsultaPedido = "USP_SUMA_TOTAL_PEDIDO_DISTRIBUCION_SINPROGRAMADO"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbCabeceraPedido
        End Function

        ' --- Stock vs Pedido de distribucion
        Public Function fnStockVSPedidoDistrib(ByVal p_strCodArt As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try

                Dim objParametros As Object() = {"CO_ITEM", p_strCodArt}
                strConsultaPedido = "USP_STOCK_VS_PEDI_MODULO_DISTRIBUCION"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbCabeceraPedido
        End Function
        ' --- Limpiar el Pedido de distribucion segun programacion
        Public Function fnLimpiarPedidoDistrib(ByVal p_strCodArt As String) As Boolean
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataSet
            Try

                Dim objParametros As Object() = {"CO_ITEM", p_strCodArt}
                strConsultaPedido = "USP_Limpiar_PedidoDistribucion_UPD"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet(strConsultaPedido, objParametros)
                Return True
            Catch ex As Exception
                Throw
            End Try
        End Function
        ' --- Consulta el Pedido de distribucion
        Public Function fnConcluirPedidoDistrib(ByVal numeroPedido As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try

                Dim objParametros As Object() = {"codigo_pedido", numeroPedido}
                strConsultaPedido = "USP_ConcluirPedido_Distribucion_UPD"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fnListadoArticulosDistrib(ByVal p_strCodArt As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"pvchCodArt", p_strCodArt}
                strConsultaPedido = "USP_LOG_LISTADO_PEDI_DISTRIBUCION"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ''PRUEBAAAA
        Public Function fnListadoArticulosDistribV3_1(ByVal p_strCodArt As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"pvchCodArt", p_strCodArt}
                strConsultaPedido = "USP_LOG_LISTADO_PEDI_DISTRIBUCION_V3_1"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function
        Public Function fnListadoArticulosDistribV3_2(ByVal p_strCodArt As String, ByVal p_strCodClie As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"pvchCodArt", p_strCodArt,
                                                 "pvchCodClie", p_strCodClie}
                strConsultaPedido = "USP_LOG_LISTADO_PEDI_DISTRIBUCION_V3_2"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function
        ''SADASDA

        ' --- Consulta el Detalle de Pedidos Volumen 2 OB
        Sub fnListadoArticulosDistribV2(ByRef dsDistribucion As DataSet, ByVal p_strCodArt As String)
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"pvchCodArt", p_strCodArt}
                dsDistribucion = _objConexion.ObtenerDataSet("USP_LOG_LISTADO_PEDI_DISTRIBUCION_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' --- Consulta lista de items para modulo de distribucion
        Public Function fnListadoItemsModuloDistribucion() As DataTable
            Dim strConsulta As String
            Dim ldtbItems As DataTable
            Try
                ldtbItems = New DataTable
                strConsulta = "USP_LISTA_ITEMS_MODULO_DISTRIBUCION_LIST"
                ldtbItems = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsulta)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbItems
        End Function

        ' --- Consulta lista de items para asociar de distribucion
        Public Function fnListadoItemsAsociarDistribucion() As DataTable
            Dim strConsulta As String
            Dim ldtbItems As DataTable
            Try
                ldtbItems = New DataTable
                strConsulta = "USP_LISTAR_ITEMS_ASOCIAR_DISTRIBUCION_LIST"
                ldtbItems = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsulta)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbItems
        End Function

        ' --- Consulta la programacion de pedidos de Distribucion
        Public Function fnListadoArticulosDistribPorProgramar(ByVal p_strNumPedido As String, ByVal p_strCodArt As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"NU_PEDIDO", p_strNumPedido,
                                                 "CO_ITEM", p_strCodArt}
                strConsultaPedido = "USP_HISTORIO_LISTADO_PORPROGRAMAR_DISTRIBUCION"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fnBuscarArticulosDistribAsociados(ByVal pstrCodArt As String) As DataSet
            Dim strConsultaPedido As String
            Dim ldsCabeceraPedido As DataSet
            Try
                ldsCabeceraPedido = New DataSet
                Dim objParametros As Object() = {"p_vchCodArt", pstrCodArt}
                strConsultaPedido = "USP_LOG_ASOCIAR_ARTI_DIST"
                ldsCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldsCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fnAccionesArticulosDistribAsociados(ByVal p_strAccion As String,
                                                            ByVal p_strCodArtB As String,
                                                            ByVal p_strCodArt As String,
                                                            ByVal p_usuario As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"p_vchAccion", p_strAccion,
                                                 "p_vchCodArtB", p_strCodArtB,
                                                 "p_vchCodArt", p_strCodArt,
                                                 "p_usuario", p_usuario}
                strConsultaPedido = "USP_LOG_GRABAR_ASOCIAR_ARTI_DIST"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fncConsultaVales(ByVal strEmpresa As String, ByVal strVale As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"Co_Empr", strEmpresa, "Nro_Vale", strVale}
                strConsultaPedido = "usp_qry_ValesConsultar"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fncConsultaDetalleVales(ByVal strEmpresa As String, ByVal strVale As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbDetallePedido As DataTable
            Try
                ldtbDetallePedido = New DataTable
                Dim objParametros As Object() = {"Co_Empr", strEmpresa, "Nro_Vale", strVale}
                strConsultaPedido = "usp_qry_ConsultarDetalleVales"
                ldtbDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbDetallePedido
        End Function

        ' --- Consulta el cabecera de pedidos
        Public Function fncConsultaPedidos(ByVal strTipo As String, ByVal strSerie As String, _
                        ByVal intCodPedido As Integer, ByVal dtFecIni As String, ByVal dtFecFin As String, _
                        ByVal strSolicitante As String, ByVal strEstado As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido, _
                    "dtFecIni", dtFecIni, "dtFecFin", dtFecFin, "strSolicitante", strSolicitante, "strEstado", strEstado}
                strConsultaPedido = "usp_qry_PedidoAlmacen_Consultar"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        Public Function fnConsultarValesPedidos(ByVal strNuPedi As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbDetallePedido As DataTable
            Try
                ldtbDetallePedido = New DataTable
                Dim objParametros As Object() = {"NumPedi", strNuPedi}
                'strConsultaPedido = "usp_qry_DetallePedidoAlmacen_Consultar"
                strConsultaPedido = "usp_log_consultarvalesPedidos"

                ldtbDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbDetallePedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fncConsultaDetallePedido(ByVal strTipo As String, ByVal strSerie As String, ByVal intCodPedido As Integer) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbDetallePedido As DataTable
            Try
                ldtbDetallePedido = New DataTable
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido}
                'strConsultaPedido = "usp_qry_DetallePedidoAlmacen_Consultar"
                strConsultaPedido = "usp_qry_DetallePedidoAlmacen_Consultar"

                ldtbDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbDetallePedido
        End Function

        ' --- Listado de Pedidos a Atender/Aprobar
        Public Function fncPedidosConsulta(ByVal strTipo As String, ByVal strSerie As String, _
                                           ByVal intCodPedido As Integer, ByVal strFechaIni As String, _
                                           ByVal strFechaFin As String, ByVal strSolicitante As String, _
                                           ByVal strEstado As String, ByVal strUsuaario As String, _
                                           ByVal strResponsable As String, ByVal strTipoConsulta As String, _
                                           ByVal strFecIniIns As String, ByVal strFecFinIns As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbListaPedido As DataTable
            Try
                ldtbListaPedido = New DataTable

                Select Case strTipoConsulta
                    Case "Consulta"
                        strConsultaPedido = "usp_qry_PedidoListaConsulta_2"
                        'strConsultaPedido = "usp_qry_PedidoListaConsulta"
                        Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, _
                                                "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, _
                                                "strEstado", strEstado, "strUsuario", strUsuaario}
                        ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
                    Case "Aprobaciones"
                        strConsultaPedido = "usp_qry_PedidoListaAprobados_2"
                        Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, _
                                                "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, _
                                                "strEstado", strEstado, "strUsuario", strUsuaario, "strResponsable", strResponsable}
                        ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
                    Case "Atender"
                        strConsultaPedido = "usp_qry_PedidoListaAtender_2"
                        Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                         "intNumPedido", intCodPedido, _
                                                         "dtFecIni", strFechaIni, "dtFecFin", strFechaFin, _
                                                         "strSolicitante", strSolicitante, _
                                                         "strEstado", strEstado,
                                                         "dtFecIniIns", strFecIniIns, "dtFecFinIns", strFecFinIns}
                        ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
                End Select

            Catch ex As Exception
                ldtbListaPedido = Nothing
            End Try

            Return ldtbListaPedido
        End Function

        ' --- Listado de Pedidos Registrados
        Public Function fncPedidosGeneral(ByVal strTipo As String, ByVal strSerie As String, intNumeroPedido As Integer, _
                                           ByVal strFechaIni As String, ByVal strFechaFin As String, _
                                           ByVal strSolicitante As String, ByVal strEstado As String, _
                                           ByVal strUsuario As String, ByVal strTipPedi As String) As DataTable

            Dim strConsultaPedidoGeneral As String
            Dim ldtbListaPedidoGeneral As DataTable
            Try
                ldtbListaPedidoGeneral = New DataTable
                strConsultaPedidoGeneral = "usp_qry_PedidoConsultaGeneral_2"
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intNumeroPedido, _
                                                "dtFecIni", strFechaIni, "dtFecFin", strFechaFin, _
                                                "strSolicitante", strSolicitante, "strEstado", strEstado, _
                                                "strUsuario", strUsuario, _
                                                "strTipPedi", strTipPedi}
                ldtbListaPedidoGeneral = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedidoGeneral, objParametros)
            Catch ex As Exception
                ldtbListaPedidoGeneral = Nothing
            End Try
            Return ldtbListaPedidoGeneral
        End Function

        ' --- Registra Items en el detalle de pedido
        Public Function prcRegistraDetallePedido(ByVal chrTipo As String, ByVal strNumPedido As String, _
                    ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, _
                    ByVal strCodDestino As String, ByVal strCodOrden As String, ByVal strCodUsuario As String, _
                    ByVal strAlmacen As String, ByVal strObservacion As String, ByVal strCodSolicitante As String, _
                    ByVal strSecuencia As String, _
                    ByVal strTipPedid As String, ByVal strFecInstal As String, _
                    ByVal str_obsdet As String) As DataTable

            Dim lstrListaDetallePedido As String
            Dim ldtbListaDetallePedido As DataTable
            Try
                ldtbListaDetallePedido = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "nu_pedi", strNumPedido, _
                                                "co_item", strNumItem, "ca_pedi", dblCantidad, _
                                                "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                                                "co_orde_serv", strCodOrden, "co_usua_crea", strCodUsuario, _
                                                "co_alma", strAlmacen, "de_obse", strObservacion, _
                                                "co_usua_soli", strCodSolicitante, "strSec", strSecuencia, _
                                                "ti_pedido", strTipPedid, "fe_instal", strFecInstal, _
                                                "de_obse_001", str_obsdet
                                                }
                lstrListaDetallePedido = "usp_qry_PedidoAlmacen_Actulizar"
                ldtbListaDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrListaDetallePedido, objParametros)
            Catch ex As Exception
                ldtbListaDetallePedido = Nothing
                Throw
            End Try
            Return ldtbListaDetallePedido
        End Function

        ' --- Registra Items en el detalle de pedido
        Public Function prcRegistraDetallePedido_V2(ByVal chrTipo As String, ByVal strNumPedido As String, _
                    ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, _
                    ByVal strCodDestino As String, ByVal strCodOrden As String, ByVal strCodUsuario As String, _
                    ByVal strAlmacen As String, ByVal strObservacion As String, ByVal strCodSolicitante As String, _
                    ByVal strSecuencia As String, _
                    ByVal strTipPedid As String, ByVal strFecInstal As String, _
                    ByVal str_obsdet As String, ByVal strOrdenTrabajo As String) As DataTable

            Dim lstrListaDetallePedido As String
            Dim ldtbListaDetallePedido As DataTable
            Try
                ldtbListaDetallePedido = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "nu_pedi", strNumPedido, _
                                                "co_item", strNumItem, "ca_pedi", dblCantidad, _
                                                "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                                                "co_orde_serv", strCodOrden, "co_usua_crea", strCodUsuario, _
                                                "co_alma", strAlmacen, "de_obse", strObservacion, _
                                                "co_usua_soli", strCodSolicitante, "strSec", strSecuencia, _
                                                "ti_pedido", strTipPedid, "fe_instal", strFecInstal, _
                                                "de_obse_001", str_obsdet, "pvch_OrdenTrabajo", strOrdenTrabajo
                                                }
                lstrListaDetallePedido = "usp_qry_PedidoAlmacen_Actulizar_V2"
                ldtbListaDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrListaDetallePedido, objParametros)
            Catch ex As Exception
                ldtbListaDetallePedido = Nothing
                Throw
            End Try
            Return ldtbListaDetallePedido
        End Function

        ' --- Verifica existencia del items en el Detalle 
        Public Function fncValidaDuplicadosDetalle(ByVal chrTipo As String, ByVal strNumPedido As String, _
                                                   ByVal strNumItem As String, ByVal strCtagasto As String, _
                                                   ByVal strActivo As String) As DataTable
            Dim lstrDuplicadoDetallePedido As String
            Dim ldtbDuplicadoDetallePedido As DataTable
            Try
                ldtbDuplicadoDetallePedido = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, "CO_ITEM", strNumItem, _
                                                "CO_DEST_FINA", strCtagasto, "CO_ORDE_SERV", strActivo}
                lstrDuplicadoDetallePedido = "usp_qry_ConsultaDuplicadoDetallePedidos_2"
                ldtbDuplicadoDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrDuplicadoDetallePedido, objParametros)
            Catch ex As Exception
                ldtbDuplicadoDetallePedido = Nothing
                Throw
            End Try
            Return ldtbDuplicadoDetallePedido
        End Function

        '' --- Verifica existencia del items en el Detalle 
        'Public Function fncValidaDuplicadosDetalle_V2(ByVal chrTipo As String, ByVal strNumPedido As String, _
        '                                              ByVal strNumItem As String, ByVal strCtagasto As String, _
        '                                              ByVal strActivo As String, ByVal strCentroCostos As String, _
        '                                              ByVal strOrdenTrabajo As String) As DataTable
        '    Dim lstrDuplicadoDetallePedido As String
        '    Dim ldtbDuplicadoDetallePedido As DataTable
        '    Try
        '        ldtbDuplicadoDetallePedido = New DataTable
        '        Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, "CO_ITEM", strNumItem, _
        '                                        "CO_DEST_FINA", strCtagasto, "CO_ORDE_SERV", strActivo, _
        '                                         "pvch_CentroCostos", strCentroCostos, "pvch_OrdenTrabajo", strOrdenTrabajo}
        '        lstrDuplicadoDetallePedido = "usp_qry_ConsultaDuplicadoDetallePedidos_3"
        '        ldtbDuplicadoDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrDuplicadoDetallePedido, objParametros)
        '    Catch ex As Exception
        '        ldtbDuplicadoDetallePedido = Nothing
        '        Throw
        '    End Try
        '    Return ldtbDuplicadoDetallePedido
        'End Function

        ' --- Verifica existencia del items en el Detalle 
        Public Function fncValidaDuplicadosDetalle_V3(ByVal chrTipo As String, ByVal strNumPedido As String, _
                                                      ByVal strNumItem As String, ByVal strCtagasto As String, _
                                                      ByVal strActivo As String, ByVal strCentroCostos As String, _
                                                      ByVal strOrdenTrabajo As String, ByVal strSecuencia As String) As DataTable
            Dim lstrDuplicadoDetallePedido As String
            Dim ldtbDuplicadoDetallePedido As DataTable
            Try
                ldtbDuplicadoDetallePedido = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, "CO_ITEM", strNumItem, _
                                                "CO_DEST_FINA", strCtagasto, "CO_ORDE_SERV", strActivo, _
                                                 "pvch_CentroCostos", strCentroCostos, "pvch_OrdenTrabajo", strOrdenTrabajo, _
                                                 "pvch_Secuencia", strSecuencia}
                lstrDuplicadoDetallePedido = "usp_qry_ConsultaDuplicadoDetallePedidos_V3"
                ldtbDuplicadoDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrDuplicadoDetallePedido, objParametros)
            Catch ex As Exception
                ldtbDuplicadoDetallePedido = Nothing
                Throw
            End Try
            Return ldtbDuplicadoDetallePedido
        End Function

        ' --- Verifica existencia del ctc duplicados
        Public Function fncValidaDuplicadoCTC(ByVal strNumPedido As String, ByVal strActivo As String) As DataTable
            Dim lstrDuplicadoCTC As String
            Dim ldtbDuplicadoCTC As DataTable
            Try
                ldtbDuplicadoCTC = New DataTable
                Dim objParametros As Object() = {"strNumeroPedido", strNumPedido, "strCodServicio", strActivo}
                lstrDuplicadoCTC = "usp_qry_ValidaPedidoCTC"
                ldtbDuplicadoCTC = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrDuplicadoCTC, objParametros)
            Catch ex As Exception
                ldtbDuplicadoCTC = Nothing
                Throw
            End Try
            Return ldtbDuplicadoCTC
        End Function

        ' --- Guarda Pedidos en tablas Originales
        Public Function fncGuardaPedido(ByVal chrTipo As String, ByVal strNumPedido As String, _
                    ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, _
                    ByVal strCodDestino As String, ByVal strCodOrden As String, ByVal strCodUsuario As String, _
                    ByVal strAlmacen As String, ByVal strObservacion As String, _
                    ByVal strCodSolicitante As String, ByVal strTiPedido As String, ByVal strFecInstal As String,
                    ByVal strObsDet As String, ByVal strTurno As String) As DataTable

            Dim ldtlPedido As DataTable
            Dim strGuardaPedido As String
            Try
                Dim objParametros As Object() = {"chr_tipo", chrTipo, "nu_pedi", strNumPedido, "co_item", strNumItem, _
                "ca_pedi", dblCantidad, "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                "co_orde_serv", strCodOrden, "co_usua_crea", strCodUsuario, "co_alma", strAlmacen, _
                "de_obse", strObservacion, "co_usua_soli", strCodSolicitante, _
                "ti_pedido", strTiPedido, "fe_instal", strFecInstal, _
                "de_obse_001", strObsDet, "turno_ped", strTurno}

                strGuardaPedido = "usp_qry_PedidoAlmacen_Registra"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGuardaPedido, objParametros)
            Catch ex As Exception
                Throw
                ldtlPedido = Nothing
            End Try
            Return ldtlPedido
        End Function

        ' --- Guarda Pedidos en tablas Originales
        Public Function fncGuardaPedido_v2(ByVal chrTipo As String, ByVal strNumPedido As String, _
                    ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, _
                    ByVal strCodDestino As String, ByVal strCodOrden As String, ByVal strCodUsuario As String, _
                    ByVal strAlmacen As String, ByVal strObservacion As String, _
                    ByVal strCodSolicitante As String, ByVal strTiPedido As String, ByVal strFecInstal As String,
                    ByVal strObsDet As String, ByVal strTurno As String, ByVal strLugarEntrega As String) As DataTable

            Dim ldtlPedido As DataTable
            Dim strGuardaPedido As String
            Try
                Dim objParametros As Object() = {"chr_tipo", chrTipo, "nu_pedi", strNumPedido, "co_item", strNumItem, _
                "ca_pedi", dblCantidad, "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                "co_orde_serv", strCodOrden, "co_usua_crea", strCodUsuario, "co_alma", strAlmacen, _
                "de_obse", strObservacion, "co_usua_soli", strCodSolicitante, _
                "ti_pedido", strTiPedido, "fe_instal", strFecInstal, _
                "de_obse_001", strObsDet, "turno_ped", strTurno, "lugar_entrega", strLugarEntrega}

                strGuardaPedido = "usp_qry_PedidoAlmacen_Registra_v3"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGuardaPedido, objParametros)
            Catch ex As Exception
                Throw
                ldtlPedido = Nothing
            End Try
            Return ldtlPedido
        End Function

        ' --- Guarda Pedidos en tablas Originales
        Public Function fncGuardaPedido_V2(ByVal chrTipo As String, ByVal strNumPedido As String, _
                    ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, _
                    ByVal strCodDestino As String, ByVal strCodOrden As String, ByVal strCodUsuario As String, _
                    ByVal strAlmacen As String, ByVal strObservacion As String, _
                    ByVal strCodSolicitante As String, ByVal strTiPedido As String, ByVal strFecInstal As String, _
                    ByVal strObsDet As String, ByVal strTurno As String, _
                    ByVal strCodAuxiliarDet As String, ByVal strCodOrdenTrabajo As String, _
                    ByVal strCodResponsableOT As String) As DataTable

            Dim ldtlPedido As DataTable
            Dim strGuardaPedido As String
            Try
                Dim objParametros As Object() = {"chr_tipo", chrTipo, "nu_pedi", strNumPedido, "co_item", strNumItem, _
                "ca_pedi", dblCantidad, "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                "co_orde_serv", strCodOrden, "co_usua_crea", strCodUsuario, "co_alma", strAlmacen, _
                "de_obse", strObservacion, "co_usua_soli", strCodSolicitante, _
                "ti_pedido", strTiPedido, "fe_instal", strFecInstal, _
                "de_obse_001", strObsDet, "turno_ped", strTurno, _
                "pvch_CodAuxiliarDet", strCodAuxiliarDet, "pvch_CodOrdenTrabajo", strCodOrdenTrabajo, _
                "pvch_CodResponsableOT", strCodResponsableOT}

                strGuardaPedido = "usp_qry_PedidoAlmacen_Registra_V2"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGuardaPedido, objParametros)
            Catch ex As Exception
                Throw
                ldtlPedido = Nothing
            End Try
            Return ldtlPedido
        End Function

        ' --- Graba Pedidos desde Vales
        Public Function fncGuardaPedidoVale(ByVal strEmpresa As String, ByVal strNumpedido As String, _
                         ByVal strCodUnidad As String, ByVal strCodAlmacen As String, ByVal strFecha As String, _
                         ByVal strObservacion As String, ByVal strCodAuxiliar As String, ByVal strCodSolicitante As String, _
                         ByVal strCodUsuario As String, ByVal strCodVale As String, ByVal ldtDetalle As DataTable) As DataTable
            Dim ldtlPedido As DataTable
            Dim strGuardaPedidoVale As String
            Try
                Dim objUtil As New NM_General.Util
                ldtDetalle.TableName = "Detalle"
                Dim strXMLDetalle As String = objUtil.GeneraXml(ldtDetalle)

                Dim Objparametros As Object() = {"var_Empresa", strEmpresa, "var_Pedido", strNumpedido, "var_Unidad", strCodUnidad, _
                "var_Almacen", strCodAlmacen, "var_Fecha", strFecha, "var_Observacion", strObservacion, "var_Auxiliar", strCodAuxiliar, _
                "var_Solicitante", strCodSolicitante, "var_Usuario", strCodUsuario, "var_Vale", strCodVale, "var_XMLDatos", strXMLDetalle}
                strGuardaPedidoVale = "usp_qry_PedidoValeRegistra"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGuardaPedidoVale, Objparametros)
            Catch ex As Exception
                Throw
                ldtlPedido = Nothing
            End Try
            Return ldtlPedido
        End Function

        ' --- Genera Nuemero de Pedido Nuevo
        Public Function fncGeneraNumeroPedido(ByVal strSerie As String) As String

            Dim ldtlPedido As DataTable
            Dim strGeneraPedido As String
            Dim strNumeroPedido As String = 0
            Try
                Dim objParametros As Object() = {"vchSerie", strSerie}
                strGeneraPedido = "usp_qry_ConsultarNumeroPedido"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGeneraPedido, objParametros)
                If ldtlPedido.Rows.Count > 0 Then
                    strNumeroPedido = ldtlPedido.Rows(0).Item("NumeroPedido")
                End If
            Catch ex As Exception
                Throw
            End Try
            Return strNumeroPedido
        End Function

        ' --- Genera Numero de Solicitud Nuevo
        Public Function fncGeneraNumeroSolicitud() As String

            Dim ldtlSolicitud As DataTable
            Dim strGeneraSolicitud As String
            Dim strNumeroSolicitud As String = 0
            Try
                Dim objParametros As Object() = {}
                strGeneraSolicitud = "usp_qry_ConsultarNumeroSolicitud"
                ldtlSolicitud = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGeneraSolicitud, objParametros)
                If ldtlSolicitud.Rows.Count > 0 Then
                    strNumeroSolicitud = ldtlSolicitud.Rows(0).Item("NumeroSolicitud")
                End If
            Catch ex As Exception
                Throw
            End Try
            Return strNumeroSolicitud
        End Function

        ' --- Consulta ultimo precio de compra de un articulo
        Public Function fncConsultaPrecioItem(ByVal strCodItem As String) As Double

            Dim ldtlPedido As DataTable
            Dim ldblPrecioItem As String = 0
            Dim strConsultaPrecio As String
            Try
                Dim objParametros As Object() = {"CO_ITEM", strCodItem}
                strConsultaPrecio = "usp_qry_ConsultaPrecioArticulo"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPrecio, objParametros)
                If ldtlPedido.Rows.Count > 0 Then
                    ldblPrecioItem = ldtlPedido.Rows(0).Item("UltimoPrecioCompra")
                End If
            Catch ex As Exception
                Throw
            End Try
            Return ldblPrecioItem
        End Function

        ' --- Consulta Presupuesto
        Public Function fncConsultaPresupuesto(ByVal strTipo As String, ByVal lstrCentroCostos As String, ByVal lstrCuentaGastos As String, ByVal lstrAnno As String, ByVal lintMes As Integer, ByVal ldblTotalPedido As Double) As DataTable
            Dim strConsultaPresupuesto As String
            Dim ldtbPresupuesto As DataTable
            Try
                ldtbPresupuesto = New DataTable
                Dim objParametros As Object() = {"chr_tipo", strTipo, "co_auxi_empr", lstrCentroCostos, "cod_dest_fina", lstrCuentaGastos, "anno", lstrAnno, "mes", lintMes, "TotalPedido", ldblTotalPedido}
                strConsultaPresupuesto = "usp_qry_ConsultarPresupuesto"
                ldtbPresupuesto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPresupuesto, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbPresupuesto
        End Function

        ' --- Aprobacion/Anulacion de Pedidos
        Public Function fncPedidoCambiaEstado(ByVal strTipo As String, ByVal strNumPedido As String, _
                                    ByVal strUsuario As String, ByVal pdtCorreos As DataTable) As DataTable
            Dim strActualizarPedido As String
            Try
                ' Anula o culmina pedido 
                If strTipo = "1" Or strTipo = "3" Then
                    strActualizarPedido = "usp_qry_PedidoAnular"
                End If
                If strTipo = "2" Then
                    strActualizarPedido = "usp_qry_PedidoAprobacion"
                End If
                'REQSIS201900029 - DG - INI
                If strTipo = "4" Then
                    strActualizarPedido = "usp_qry_PedidoReinicio"
                End If
                'REQSIS201900029 - DG - FIN
                Dim objParametros As Object() = {"chr_tipo", strTipo, "NU_PEDI", strNumPedido, "CO_USUA_MODI", strUsuario}
                pdtCorreos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strActualizarPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return pdtCorreos
        End Function

        ' --- Aprobacion/Anulacion de Pedidos
        Public Function fncPedidoCambiaEstado_v2(ByVal strTipo As String, ByVal strNumPedido As String, _
                                    ByVal strUsuario As String, ByVal pdtCorreos As DataTable) As DataTable
            Dim strActualizarPedido As String
            Try
                ' Anula o culmina pedido 
                If strTipo = "1" Or strTipo = "3" Then
                    strActualizarPedido = "usp_qry_PedidoAnular_v2"
                End If
                If strTipo = "2" Then
                    strActualizarPedido = "usp_qry_PedidoAprobacion"
                End If
                'REQSIS201900029 - DG - INI
                If strTipo = "4" Then
                    strActualizarPedido = "usp_qry_PedidoReinicio"
                End If
                'REQSIS201900029 - DG - FIN
                Dim objParametros As Object() = {"chr_tipo", strTipo, "NU_PEDI", strNumPedido, "CO_USUA_MODI", strUsuario}
                pdtCorreos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strActualizarPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return pdtCorreos
        End Function

        ' --- Actualizar Cantidades - Pedido
        Public Function fncActualizarCantidades(ByVal strTipo As String, ByVal strNumPedido As String,
                                                ByVal strNumeroItem As String, ByVal dblCantidadAprobada As Double,
                                                ByVal strUsuario As String, ByVal strCtaGasto As String, ByVal strOrdenServicio As String,
                                                Optional ByVal strCentroCostos As String = "", Optional ByVal strOrdenTrabajo As String = "") As DataTable
            Dim strActualizarCantidades As String
            Dim ldtbActualizarCantidades As DataTable
            Try
                ldtbActualizarCantidades = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", strTipo, "nu_pedi", strNumPedido, _
                                                "co_item", strNumeroItem, "ca_reqi", dblCantidadAprobada, _
                                                "co_usua_modi", strUsuario, "co_dest_fina", strCtaGasto, _
                                                "co_orde_serv", strOrdenServicio, "co_auxi_empr", strCentroCostos, _
                                                 "NU_ORTR", strOrdenTrabajo}

                strActualizarCantidades = "usp_qry_PedidoActualizaCantidades_5"
                ldtbActualizarCantidades = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strActualizarCantidades, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbActualizarCantidades
        End Function

        ' --- Actualizar Cantidades - Pedido
        Public Function fncActualizarCantidadesHilo(ByVal strTipo As String, ByVal strNumPedido As String, _
                                                ByVal strNumeroItem As String, _
                                                ByVal dblCantidadAprobada As Double, ByVal dblCantidadAprobadaAlterna As Double, _
                                                ByVal strUsuario As String, ByVal strCtaGasto As String, _
                                                ByVal strOrdenServicio As String) As DataTable
            Dim strActualizarCantidades As String
            Dim ldtbActualizarCantidades As DataTable
            Try
                ldtbActualizarCantidades = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", strTipo, "nu_pedi", strNumPedido, _
                                                "co_item", strNumeroItem, "ca_reqi", dblCantidadAprobada, _
                                                "ca_reqi_alte", dblCantidadAprobadaAlterna, _
                                                "co_usua_modi", strUsuario, "co_dest_fina", strCtaGasto, _
                                                "co_orde_serv", strOrdenServicio}
                strActualizarCantidades = "usp_qry_PedidoAlmacenActualizaCantidades"
                ldtbActualizarCantidades = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strActualizarCantidades, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbActualizarCantidades
        End Function

        ' Aprobacion de pedido de almacen
        Public Function fncSolicitarAprobacionPedido(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                           ByVal strNumeroDocumento As String, ByVal strFecha As String, _
                                           ByVal strObservacion As String, ByVal strEstadoSoli As String, _
                                           ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, _
                                           ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, _
                                           ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, _
                                           ByVal strFechaModi As String, Optional ByRef pdtCorreos As DataTable = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lblnOk As Boolean
            Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                                      "CO_APRO", strCodAprobacion, _
                                      "NU_DOCU", strNumeroDocumento, _
                                      "FE_DOCU", strFecha, _
                                      "OB_0001", strObservacion, _
                                      "ST_SOLI", strEstadoSoli, _
                                      "FE_STAT_SOLI", strFechaSolicitud, _
                                      "TI_AUXI_EMPR", strTipoAuxiliar, _
                                      "CO_AUXI_EMPR", strCodigoAuxiliar, _
                                      "CO_USUA_CREA", strUsuario, _
                                      "FE_USUA_CREA", strFechaCreacion, _
                                      "CO_USUA_MODI", strUsuarioModi, _
                                      "FE_USUA_MODI", strFechaModi}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pdtCorreos = lobjCon.ObtenerDataTable("usp_qry_Insertar_Soli_Aprobacion_Pedido", Params)
                lblnOk = True
            Catch ex As Exception
                lblnOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lblnOk
        End Function

        ' Aprobacion de pedido de hilos
        Public Function fncSolicitarAprobacionPedidoHilos(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                           ByVal strNumeroDocumento As String,
                                           ByVal strEstadoSoli As String, _
                                           ByVal strTipoAuxiliar As String, _
                                           ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, _
                                           ByRef pdtCorreos As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lblnOk As Boolean
            Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                                      "CO_APRO", strCodAprobacion, _
                                      "NU_DOCU", strNumeroDocumento, _
                                      "ST_SOLI", strEstadoSoli, _
                                      "TI_AUXI_EMPR", strTipoAuxiliar, _
                                      "CO_AUXI_EMPR", strCodigoAuxiliar, _
                                      "CO_USUA_CREA", strUsuario}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pdtCorreos = lobjCon.ObtenerDataTable("usp_qry_PedidoAlmacen_Aprobar", Params)
                lblnOk = True
            Catch ex As Exception
                lblnOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lblnOk
        End Function

        ' --- Consulta Empelado Solicito - Pedido
        Public Function fncPedidoConsultaEmpleado(ByVal strCodSolicitante As String) As DataTable
            Dim strConsultaEmpleado As String
            Dim ldtbEmpleado As DataTable
            Try
                ldtbEmpleado = New DataTable
                Dim objParametros As Object() = {"co_trab", strCodSolicitante}
                strConsultaEmpleado = "usp_qry_PedidoConsultaEmpleado"
                ldtbEmpleado = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaEmpleado, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbEmpleado
        End Function

        Public Function fncValidarStockAlmacenPedido(ByVal strNumPedido As String) As DataTable
            Dim ldtbValida As DataTable
            Try
                ldtbValida = New DataTable
                Dim objParametros As Object() = {"Num_Pedido", strNumPedido}
                ldtbValida = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_ValidarArticulosxPedidos", objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbValida
        End Function

        Public Function fncValidarTipoItemVale(ByVal pAccion As String, ByVal pNumeroPedido As String, ByVal pTipItem As String) As DataTable
            Dim ldtbValida As DataTable
            Try
                ldtbValida = New DataTable
                Dim objParametros As Object() = {"p_vch_accion", pAccion,
                                                 "p_vch_nu_pedi", pNumeroPedido,
                                                 "p_vch_tip_item", pTipItem}
                ldtbValida = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_obtener_tipovalealmacen", objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbValida
        End Function

        Public Function fncValidarItemsExportacionVale(ByVal strNumPedido As String) As DataTable
            Dim ldtbValida As DataTable
            Try
                ldtbValida = New DataTable
                Dim objParametros As Object() = {"Num_Pedido", strNumPedido}
                ldtbValida = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_ValidarArticulosImportacionVale_v2", objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbValida
        End Function

        Public Function fncActualizarEstadoFechaValeAlma(ByVal strNumPedido As String, ByVal strPrioridad As String, ByVal strFecInstal As String) As DataTable
            Dim ldtbValida As DataTable
            Try
                ldtbValida = New DataTable
                Dim objParametros As Object() = {"num_pedido", strNumPedido,
                                                 "ti_pedido", strPrioridad,
                                                 "fec_instal", strFecInstal}
                ldtbValida = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_actualizarFechaEstadoValeAlma", objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbValida
        End Function

        ''' Funciones Montacarga 
        Public Function fncCheckListMontacargaCRUD(ByVal strAccion As String, ByVal strCodEmpl As String, ByVal intTurno As String, ByVal strFechaChkList As String, ByVal strNumMaquina As String,
                                                   ByVal strHorometroE As String, ByVal strHorometroS As String, ByVal strMcaFlag1 As String, ByVal strCantAnad1 As String, ByVal strMcaFlag2 As String,
                                                   ByVal strCantAnad2 As String, ByVal strMcaFlag3 As String, ByVal strCantAnad3 As String, ByVal strMcaFlag4 As String, ByVal strCantAnad4 As String,
                                                   ByVal strMcaFlag5 As String, ByVal strCantAnad5 As String, ByVal strMcaFlag6 As String, ByVal strMcaFlag7 As String, ByVal strMcaFlag8 As String,
                                                   ByVal strMcaFlag9 As String, ByVal strMcaFlag10 As String, ByVal strObservaciones As String, ByVal strUsuario As String) As DataTable
            Dim ldtbValida As DataTable
            Try
                ldtbValida = New DataTable
                Dim objParametros As Object() = {"p_vch_accion", strAccion,
                                                 "p_vch_cod_empl", strCodEmpl,
                                                 "p_int_turno", intTurno,
                                                 "p_vch_fecha_chk", strFechaChkList,
                                                 "p_vch_num_maquina", strNumMaquina,
                                                 "p_vch_horometro_e", strHorometroE,
                                                 "p_vch_horometro_s", strHorometroS,
                                                 "p_vch_mca_flag1", strMcaFlag1,
                                                 "p_vch_cant_anad1", strCantAnad1,
                                                 "p_vch_mca_flag2", strMcaFlag2,
                                                 "p_vch_cant_anad2", strCantAnad2,
                                                 "p_vch_mca_flag3", strMcaFlag3,
                                                 "p_vch_cant_anad3", strCantAnad3,
                                                 "p_vch_mca_flag4", strMcaFlag4,
                                                 "p_vch_cant_anad4", strCantAnad4,
                                                 "p_vch_mca_flag5", strMcaFlag5,
                                                 "p_vch_cant_anad5", strCantAnad5,
                                                 "p_vch_mca_flag6", strMcaFlag6,
                                                 "p_vch_mca_flag7", strMcaFlag7,
                                                 "p_vch_mca_flag8", strMcaFlag8,
                                                 "p_vch_mca_flag9", strMcaFlag9,
                                                 "p_vch_mca_flag10", strMcaFlag10,
                                                 "p_vch_observaciones", strObservaciones,
                                                 "p_vch_usuario", strUsuario}
                ldtbValida = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("USP_LOG_CHECK_LIST_MONTACARGA_MANTENEDOR", objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbValida
        End Function


        ' --- Listar Seguimiento
        Public Sub fnc_ListarSeguimientoPedido(ByRef ldtbSeguimiento As DataTable)
            Dim strConsultaSeguimiento As String
            Dim intCodigoPedido As Integer = 0
            intCodigoPedido = Me._NumPedido

            Try
                strConsultaSeguimiento = "usp_qry_PedidoListarSeguimiento"
                Dim objParametros() As Object = {"intCodigoPedido", intCodigoPedido}
                ldtbSeguimiento = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaSeguimiento, objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' --- Listar Pedidos pendientes de despacho
        Public Function fnc_ListarPedidoPorDespachar(ByVal strTipo As String, ByVal strNumeroPedido As String, ByVal strUsuario As String) As DataTable
            Dim strConsultaporDespachar As String
            Dim ldtbPorDespachar As DataTable
            Try
                strConsultaporDespachar = "usp_qry_PedidoListaPorDespachar_2"
                Dim objParametros() As Object = {"strTipo", strTipo, _
                                        "strNumeroPedido", strNumeroPedido, "strUsuario", strUsuario}
                ldtbPorDespachar = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaporDespachar, objParametros)
            Catch ex As Exception
                ldtbPorDespachar = Nothing
                Throw
            End Try
            Return ldtbPorDespachar
        End Function

        ' --- Listar Pedidos con Saldos por Atender
        Public Function fnc_ListarPedidoPorSaldos(ByVal strTipo As String) As DataTable
            Dim strConsultaporSaldos As String
            Dim ldtbPorSaldos As DataTable
            Try
                strConsultaporSaldos = "usp_qry_ConsultaPedidoSaldos"
                Dim objParametros() As Object = {"strTipo", strTipo}
                ldtbPorSaldos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaporSaldos, objParametros)
            Catch ex As Exception
                ldtbPorSaldos = Nothing
                Throw
            End Try
            Return ldtbPorSaldos
        End Function

        ' Funcion Graba vale de Salida
        Public Function fnc_PedidoValesDetalle_Grabar(ByVal strUsuario As String, ByVal strNumeroPedido As String, Optional ByVal strUsuarioRecepcionador As String = "") As DataTable
            Dim strDespachoPedidos As String = ""
            Dim m_sqlDtAccTintoreria As AccesoDatosSQLServer
            If Mid(strNumeroPedido, 1, 4) = "0003" Then
                'strDespachoPedidos = "usp_qry_PedidoValesAlmacenGrabar"
                strDespachoPedidos = "usp_qry_PedidoValesAlmacenGrabar_V2"
            Else
                strDespachoPedidos = "usp_qry_PedidoValesGrabar"
            End If
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                If Mid(strNumeroPedido, 1, 4) = "0003" Then
                    Dim objParametros() As String = {"var_Usuario", strUsuario, "vch_NumeroPedido", strNumeroPedido, "var_UsuarioRecep", strUsuarioRecepcionador}
                    Return m_sqlDtAccTintoreria.ObtenerDataTable(strDespachoPedidos, objParametros)
                Else
                    Dim objParametros() As String = {"var_Usuario", strUsuario, "vch_NumeroPedido", strNumeroPedido}
                    Return m_sqlDtAccTintoreria.ObtenerDataTable(strDespachoPedidos, objParametros)
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' Funcion Graba vale de Salida
        Public Function fnc_PedidoValesDesperdicios_Grabar(ByVal strUsuario As String, ByVal strNumeroPedido As String) As DataTable
            Dim m_sqlDtAccTintoreria As AccesoDatosSQLServer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As String = {"var_Usuario", strUsuario, "vch_NumeroPedido", strNumeroPedido}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_PedidoValesDesperdicios_Grabar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' Funcion Graba vale de Salida
        Public Function fnc_PedidoValesHilo_Grabar(ByVal strUsuario As String, ByVal strNumeroPedido As String) As DataTable
            Dim m_sqlDtAccTintoreria As AccesoDatosSQLServer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As String = {"var_Usuario", strUsuario, "vch_NumeroPedido", strNumeroPedido}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' Funcion Graba Solicitud OT
        Public Function fncGuardaSolicitudOT(ByVal strNumSoli As String, ByVal strFecha As String, _
                                            ByVal strArea As String, ByVal strMaquina As String, _
                                            ByVal strSolicitante As String, ByVal strObservacion As String) As DataTable
            Dim m_sqlDtAccTintoreria As AccesoDatosSQLServer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objPedido As New Logistica.clsPedidos
                Dim objParametros() As String = {"Var_NumeroSoli", strNumSoli, "var_Fecha", strFecha, _
                            "var_Area", strArea, "var_Maquina", strMaquina, "var_Soli", strSolicitante, _
                            "var_Observacion", strObservacion}

                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_SolicitudOTGrabar", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' --- Consulta los vales de pedido
        Public Function fncConsultaValesPedido(ByVal strNumeroPedido As String) As DataTable
            Dim strConsultaVales As String
            Dim ldtbVales As DataTable
            Try
                ldtbVales = New DataTable
                Dim objParametros As Object() = {"strNumeroPedido", strNumeroPedido}
                strConsultaVales = "usp_qry_PedidoListaVales"
                ldtbVales = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaVales, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbVales
        End Function

        ' --- Consulta Vales de Mantenimiento Pendiente de Conversio a Pedidos
        Public Function fncConsultaVales() As DataTable
            Dim strConsultaVales As String
            Dim ldtbVales As DataTable
            Try
                ldtbVales = New DataTable
                strConsultaVales = "usp_MTO_ValesMntoC_Listar"
                ldtbVales = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaVales)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtbVales
        End Function

        ' --- Consulta detalle de los vales de pedido
        Public Function fnc_ValeDetalle(ByVal strNumeroVale As String) As DataTable
            Dim strDetalleVale As String = ""
            Dim ldtbDetalleVale As DataTable
            Try
                ldtbDetalleVale = New DataTable
                Dim objParametros As Object() = {"strNumeroVale", strNumeroVale}
                strNumeroVale = "usp_qry_PedidoListaValeDetalle"
                ldtbDetalleVale = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strNumeroVale, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbDetalleVale
        End Function

        ' --- Consulta activos fijos por centro de costos
        Public Function fnc_ListaActivosFijos(ByVal strCodActivo As String, ByVal strDesActivo As String, ByVal strCentroCostos As String, ByVal strTipo As String) As DataTable
            Dim strConsultaActivos As String
            Dim ldtbActivos As DataTable
            Try
                ldtbActivos = New DataTable
                Dim objParametros As Object() = {"co_acti_fijo", strCodActivo, "de_Acti", strDesActivo, _
                                                "centro_costos", strCentroCostos, "Tipo", strTipo}
                strConsultaActivos = "usp_qry_ListaActivoFijos"
                ldtbActivos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaActivos, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbActivos
        End Function

        ' --- Consulta de consumo mensual
        Public Function fnc_ConsumoMensual(ByVal strNumeroOrdenCompra As String) As DataTable
            Dim strConsultaOC As String
            Dim ldtbDetalleConsumo As DataTable
            Try
                ldtbDetalleConsumo = New DataTable
                Dim objParametros As Object() = {"strNumeroOrdenCompra", strNumeroOrdenCompra}
                strConsultaOC = "usp_LOG_OrdenCompraReporte"
                ldtbDetalleConsumo = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaOC, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbDetalleConsumo
        End Function

        ' --- Verifica las cuenta de gasto
        Public Function fnc_VerificaCtaGasto(ByVal strCtaGasto As String) As Boolean
            Dim strConsultaCta As String
            Dim ldtbCtaGasto As DataTable
            Dim lblnCtaGasto As Boolean = False
            Try
                ldtbCtaGasto = New DataTable
                Dim objParametros As Object() = {"strCtaGasto", strCtaGasto}
                strConsultaCta = "usp_qry_VerificaCtaGastos"
                ldtbCtaGasto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaCta, objParametros)
                If ldtbCtaGasto.Rows().Count = 0 Or ldtbCtaGasto Is Nothing Then
                    lblnCtaGasto = True
                Else
                    lblnCtaGasto = False
                End If
            Catch ex As Exception
                lblnCtaGasto = False
            End Try

            Return lblnCtaGasto
        End Function

        ' --- Verifica los activos/ctc
        Public Function fnc_VerificaActivoCTC(ByVal strActivo As String) As Boolean
            Dim strConsultaActivo As String
            Dim ldtbActivo As DataTable
            Dim lblnActivo As Boolean = False
            Try
                ldtbActivo = New DataTable
                Dim objParametros As Object() = {"strActivo", strActivo}
                strConsultaActivo = "usp_qry_VerificaActivos"
                ldtbActivo = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaActivo, objParametros)
                If ldtbActivo.Rows().Count = 0 Or ldtbActivo Is Nothing Then
                    lblnActivo = True
                Else
                    lblnActivo = False
                End If
            Catch ex As Exception
                lblnActivo = False
            End Try
            Return lblnActivo
        End Function

        ' --- Verifica Orden de Trabajo
        Public Function fnc_VerificaOrdenTrabajo(ByVal strResponsable As String, ByVal strOrdenTrabajo As String) As Boolean
            Dim strConsultaActivo As String
            Dim ldtbPedido As DataTable
            Dim lblnPedido As Boolean = False
            Try
                ldtbPedido = New DataTable
                Dim objParametros As Object() = {"pvch_Responsable", strResponsable,
                                                 "pvch_OrdenTrabajo", strOrdenTrabajo}

                strConsultaActivo = "usp_qry_VerificaOrdenTrabajo"
                ldtbPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaActivo, objParametros)
                If ldtbPedido.Rows().Count = 0 Or ldtbPedido Is Nothing Then
                    lblnPedido = False
                Else
                    lblnPedido = True
                End If
            Catch ex As Exception
                lblnPedido = False
            End Try
            Return lblnPedido
        End Function

        ' --- Consulta hilos
        Public Function fncConsultarHilos(ByVal strCodigoItem As String, ByVal strDescripcionItem As String, ByVal strStock As String, ByVal strCodAlmacen As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbHilos As New DataTable
            ldtbHilos = Nothing
            Try

                Dim objParametros As Object() = {"vch_CodigoItem", strCodigoItem, "vch_DescripcionItem", strDescripcionItem, "chr_Stock", strStock, "vch_CodigoAlmacen", strCodAlmacen}
                strConsultaPedido = "usp_qry_Hilos_2"
                ldtbHilos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbHilos
        End Function

        '-------------------Inicio---------------------
        '--Descripcion: Despacho via pocket
        '--Fecha: Abril 2016
        '--Autor: Alexander Torres Cardenas
        '----------------------------------------

        ' consultamos pedidos de hilos pendientes - pocket
        Public Function fncConsultarPedidosHilosPendientes(ByVal strCodigoEmpresa As String, ByVal strCodigoPedidoHilo As String, Optional ByVal strCodigoAlmacen As String = "007") As DataTable
            Dim strConsultaPedidoHilo As String
            Dim ldtbPedHilos As New DataTable
            ldtbPedHilos = Nothing
            Try

                Dim objParametros As Object() = {"chr_CodigoEmpresa", strCodigoEmpresa, _
                                                 "vch_CodigoPedido", strCodigoPedidoHilo, _
                                                 "vch_CodigoAlmacen", strCodigoAlmacen}
                strConsultaPedidoHilo = "usp_qry_PedidoHiloPendientes_Pocket"
                ldtbPedHilos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedidoHilo, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbPedHilos
        End Function

        ' consultamos pedidos de hilos pendientes - pocket
        Public Function fncConsultarPedidoHiloStock(ByVal strCodigoPedidoHilo As String, ByVal strCodigoArticulo As String, ByVal _Version As String) As DataTable
            Dim strConsultaPedidoHilos As String
            Dim ldtbPedHilosStock As New DataTable
            ldtbPedHilosStock = Nothing
            Try

                Dim objParametros As Object() = {"vch_CodigoPedido", strCodigoPedidoHilo, _
                                                 "vch_CodigoArticulo", strCodigoArticulo}
                If _Version = 2 Then
                    strConsultaPedidoHilos = "usp_qry_ArticuloUbicacionStock_Pocket_V3"
                Else
                    strConsultaPedidoHilos = "usp_qry_ArticuloUbicacionStock_Pocket"
                End If

                ldtbPedHilosStock = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedidoHilos, objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtbPedHilosStock
        End Function

        ' consultamos datos de parihuela - pocket
        Public Function fncConsultarParihuela(ByVal strCodigoParihuela As String) As DataTable
            Dim strConsultaParihuela As String
            Dim ldtbParihuela As New DataTable
            ldtbParihuela = Nothing
            Try
                Dim objParametros As Object() = {"int_CodigoPariehuela", strCodigoParihuela}
                strConsultaParihuela = "usp_qry_ConsultarParihuela_Pocket"
                ldtbParihuela = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaParihuela, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbParihuela
        End Function

        ' validacion de la(s) parihuelas - pocket
        Public Function fncValidarParihuela(ByVal strCodigoParihuela As String, _
                                            ByVal strNumeroPedido As String, _
                                            ByVal strCodigoArticulo As String, _
                                            ByVal strCodigoUbicacion As String, _
                                            ByVal intConosElegidos As Integer, _
                                            Optional ByVal intVersion As Integer = 0 _
                                            ) As DataTable
            Dim strConsultaParihuela As String
            Dim ldtbParihuela As New DataTable
            ldtbParihuela = Nothing
            Try
                Dim objParametros As Object() = {"int_CodigoPariehuela", strCodigoParihuela, _
                                                 "vch_CodigoUbicacion", strCodigoUbicacion, _
                                                 "vch_NumeroPedido", strNumeroPedido, _
                                                 "vch_CodigoArticulo", strCodigoArticulo, _
                                                 "num_ConosElegidos", intConosElegidos
                                                 }
                If intVersion Then
                    strConsultaParihuela = "usp_qry_ValidarParihuela_Pocket_V2"
                Else
                    strConsultaParihuela = "usp_qry_ValidarParihuela_Pocket"
                End If

                ldtbParihuela = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaParihuela, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbParihuela
        End Function

        ' despacho de hilo - pocket
        Public Function fncDespachaParihuelahilos(ByVal strCodigoPedido As String, _
                                           ByVal strCodigoHilo As String, _
                                           ByVal strParihuelas As String, _
                                           ByVal strUsuario As String) As DataTable
            Dim strDespachoParihuelaHilo As String
            Dim ldtbParihuela As New DataTable
            ldtbParihuela = Nothing
            Try
                Dim objParametros As Object() = {"vch_CodigoPedido", strCodigoPedido, _
                                                 "vch_Codigohilo", strCodigoHilo, _
                                                 "vch_Parihuelas", strParihuelas, _
                                                 "vch_Usuario", strUsuario}
                strDespachoParihuelaHilo = "usp_qry_DespacharParihuelasHilo_Pocket_V2"
                'strDespachoParihuelaHilo = "usp_qry_DespacharParihuelasHilo_Pocket"
                ldtbParihuela = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strDespachoParihuelaHilo, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbParihuela
        End Function

        ' pre-despacho de hilo - pocket
        Public Function fnc_PreDespacho_ParihuelaHilos(ByVal strCodigoPedido As String, _
                                           ByVal strCodigoHilo As String, _
                                           ByVal strParihuelas As String, _
                                           ByVal strUsuario As String) As Integer
            Dim strDespachoParihuelaHilo As String
            Dim lintParihuela As Integer

            Try
                Dim objParametros As Object() = {"vch_CodigoPedido", strCodigoPedido, _
                                                 "vch_Codigohilo", strCodigoHilo, _
                                                 "vch_Parihuelas", strParihuelas, _
                                                 "vch_Usuario", strUsuario}
                strDespachoParihuelaHilo = "usp_qry_PreDespacho_ParihuelasHilo_Pocket"
                lintParihuela = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).EjecutarComando(strDespachoParihuelaHilo, objParametros)

            Catch ex As Exception
                Throw
            End Try

            Return lintParihuela
        End Function

        Public Function fnc_DespacharParihuela_PreDespacho(ByVal intCodigoParihuela As Integer, ByVal strCodUsuario As String) As DataTable
            Dim dtblDatos As DataTable
            Dim strDespachoParihuelaHilo As String
            Dim objParametros() As Object = {"pint_CodigoParihuela", intCodigoParihuela,
                                             "pvch_CodigoUsuario", strCodUsuario}
            Try
                strDespachoParihuelaHilo = "USP_HIL_DESPACHAR_PARIHUELA_PREDESPACHO"
                dtblDatos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strDespachoParihuelaHilo, objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos

        End Function

        ' consulta para imprimir vale de salida de hilos - pocket
        Public Function fnc_ConsultaValeSalidaHilo(ByVal strNumDocumento As String, _
                                                   ByVal strTipoDocumento As String, _
                                                   ByVal strCodAlmacen As String, _
                                                   ByVal strCodEmpresa As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtbValeSalidaHilo As DataTable
            dtbValeSalidaHilo = Nothing
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros() As Object = {"pstr_NumDocumento", strNumDocumento,
                                                 "pstr_TipoDocumento", strTipoDocumento,
                                                 "pstr_CodAlmacen", strCodAlmacen,
                                                 "pstr_CodEmpresa", strCodEmpresa}

                dtbValeSalidaHilo = sqlCon.ObtenerDataTable("usp_LOG_BuscarDatosDocumentos_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbValeSalidaHilo
        End Function

        ' culminar pedido de hilo - pocket
        Public Function fnc_CulminarPedidoHilo(ByVal strCodEmpresa As String, _
                                               ByVal strCodigoPedido As String, _
                                               ByVal strUsuario As String) As DataTable
            Dim dtbCulminarPedidoHilo As DataTable
            dtbCulminarPedidoHilo = Nothing
            Try
                Dim objParametros() As Object = {"chr_CodigoEmpresa", strCodEmpresa,
                                                "vch_CodigoPedido", strCodigoPedido,
                                                "vch_Usuario", strUsuario}
                dtbCulminarPedidoHilo = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("usp_qry_PedidoHiloCulminar_Pocket", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbCulminarPedidoHilo
        End Function

        '----------------------------------------
        '----Fin de despacho de hilos - pocket---
        '----------------------------------------

        '-------------------Inicio----------------------------
        '--Descripcion: Despacho Servicio Retorcido via pocket
        '--Fecha: Noviembre 2016
        '--Autor: Luis Alanoca J.
        '-----------------------------------------------------
        Public Function fnc_ObtenerListaProveedores_ServicioHilo() As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtbProveedoresServicio As DataTable
            dtbProveedoresServicio = Nothing
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dtbProveedoresServicio = sqlCon.ObtenerDataTable("usp_LOG_ListarProveedores_ServicioHilo")

            Catch ex As Exception
                Throw ex
            Finally
                sqlCon = Nothing
            End Try

            Return dtbProveedoresServicio

        End Function

        Public Function fnc_ConsultarOrdenServicioRetorcidos_Pendientes(ByVal strCodigoEmpresa As String,
                                                                        ByVal strCodigoProveedor As String,
                                                                        ByVal strOrden_Servicio As String) As DataTable
            Dim strConsultaPedidoHilo As String
            Dim ldtbPedHilos As New DataTable
            ldtbPedHilos = Nothing
            Try
                Dim objParametros As Object() = {"vch_Codigo_Empresa", strCodigoEmpresa,
                                                 "vch_Codigo_Proveedor", strCodigoProveedor,
                                                 "vch_Orden_Servicio", strOrden_Servicio}
                strConsultaPedidoHilo = "usp_LOG_ListarOrdenServicioRetorcido_Pendientes"
                ldtbPedHilos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedidoHilo, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbPedHilos
        End Function

        ' validacion de la(s) parihuelas - pocket
        Public Function fnc_ValidarParihuelaRetorcidos(ByVal strCodigoParihuela As String,
                                                       ByVal intConosElegidos As Integer) As DataTable
            Dim strConsultaParihuela As String
            Dim ldtbParihuela As New DataTable
            ldtbParihuela = Nothing
            Try
                Dim objParametros As Object() = {"int_CodigoPariehuela", strCodigoParihuela, _
                                                 "num_ConosElegidos", intConosElegidos
                                                 }
                strConsultaParihuela = "usp_LOG_ValidaParihuelaRetorcido_Pocket"
                ldtbParihuela = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaParihuela, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbParihuela
        End Function

        ' despacho de hilo  servicio retorcido - pocket
        Public Function fnc_DespachaParihuelaHilosRetorcidos(ByVal strParihuelas As String,
                                                             ByVal strProveedor As String,
                                                             ByVal strUsuario As String,
                                                             ByVal strCodigoAlmacen As String) As DataTable
            Dim strDespachoParihuelaHilo As String
            Dim ldtbParihuela As New DataTable
            ldtbParihuela = Nothing
            Try
                Dim objParametros As Object() = {"vch_Parihuelas", strParihuelas,
                                                 "vch_Proveedor", strProveedor,
                                                 "vch_Usuario", strUsuario,
                                                 "vch_CodigoAlmacen", strCodigoAlmacen}
                'strDespachoParihuelaHilo = "usp_LOG_DespacharParihuelasHiloRetorcido_Pocket"
                strDespachoParihuelaHilo = "usp_LOG_DespacharParihuelasHiloRetorcido_Pocket_V2"
                ldtbParihuela = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strDespachoParihuelaHilo, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbParihuela
        End Function

        ' consulta para imprimir vale de salida de hilos - pocket
        Public Function fnc_ConsultaValeSalidaHiloRetorcido(ByVal strNumDocumento As String, _
                                                   ByVal strTipoDocumento As String, _
                                                   ByVal strCodAlmacen As String, _
                                                   ByVal strCodEmpresa As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtbValeSalidaHilo As DataTable
            dtbValeSalidaHilo = Nothing
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros() As Object = {"pstr_NumDocumento", strNumDocumento,
                                                 "pstr_TipoDocumento", strTipoDocumento,
                                                 "pstr_CodAlmacen", strCodAlmacen,
                                                 "pstr_CodEmpresa", strCodEmpresa}

                dtbValeSalidaHilo = sqlCon.ObtenerDataTable("usp_LOG_BuscarDatosDocumentos_Retorcidos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbValeSalidaHilo
        End Function

        Public Function fnc_ConsultarArticulosxProveedor_MateriaPrima(ByVal strCodigoEmpresa As String,
                                                                        ByVal strCodigoProveedor As String) As DataTable
            Dim strConsultaPedidoHilo As String
            Dim ldtbPedHilos As New DataTable
            ldtbPedHilos = Nothing
            Try
                Dim objParametros As Object() = {"vch_CodigoEmpresa", strCodigoEmpresa,
                                                 "vch_CodigoProveedor", strCodigoProveedor}
                strConsultaPedidoHilo = "usp_LOG_MPR_ListarArticulos_x_Proveedor"
                ldtbPedHilos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedidoHilo, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbPedHilos
        End Function

        Public Function fnc_ConsultarDatosDespacho_MateriaPrima(ByVal strCodigoEmpresa As String,
                                                                ByVal strCodArticulo As String,
                                                                ByVal strCodigoProveedor As String) As DataTable
            Dim strConsultaPedidoHilo As String
            Dim ldtbPedHilos As New DataTable
            ldtbPedHilos = Nothing
            Try
                Dim objParametros As Object() = {"vch_CodigoEmpresa", strCodigoEmpresa,
                                                 "vch_CodigoArticulo", strCodArticulo,
                                                 "vch_CodigoProveedor", strCodigoProveedor}
                strConsultaPedidoHilo = "usp_LOG_MPR_ListarDatos_MateriaPrima"

                ldtbPedHilos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedidoHilo, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbPedHilos
        End Function

        Public Function fnc_ObtenerObservaciones_PedidoHilos(ByVal strNumeroPedido As String) As String
            Dim strConsultaPedidoHilo As String
            Dim strResult As String

            Try
                Dim objParametros As Object() = {"vch_NumeroPedido", strNumeroPedido}

                strConsultaPedidoHilo = "usp_LOG_HILOS_ObtenerObservaciones_PedidoHilo"

                strResult = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerValor(strConsultaPedidoHilo, objParametros)

            Catch ex As Exception
                Throw
            End Try

            Return strResult


        End Function


        Public Function fnc_ObtenerConosReservados_PedidoHilos(ByVal strNumeroPedido As String, ByVal strCodigoArticulo As String) As String
            Dim strConsultaPedidoHilo As String
            Dim strResult As String

            Try
                Dim objParametros As Object() = {"pvch_NumeroPedido", strNumeroPedido,
                                                 "pvch_CodigoArticulo", strCodigoArticulo}

                strConsultaPedidoHilo = "usp_LOG_HILOS_ObtenerConosReservados_PedidoHilo"

                strResult = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerValor(strConsultaPedidoHilo, objParametros)

            Catch ex As Exception
                Throw
            End Try

            Return strResult


        End Function

        '----------------------------------------------------
        '----Fin de Despacho Servicio Retorcido via pocket---
        '----------------------------------------------------

        ' Cargamos a data del XLS
        Public Function fnc_ObtenerArticuloXLS(ByVal mstrRuta As String) As DataTable
            'CADENA HASTA EXCEL 2003
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            'Dim lobjCon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [Hoja1$]", lobjCon)
            Dim ldtArticulo As DataTable = EsquemaArticulo()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        ldrFila = ldtArticulo.NewRow()
                        With ldrFila
                            .Item(0) = xlReader.Item(0)
                            'Guardamos en tabla

                        End With
                        ldtArticulo.Rows.Add(ldrFila)
                        ldrFila = Nothing
                    End If
                End While
                xlReader.Close()
                Return ldtArticulo
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        'Esquema de la tabla Articulo a cargar
        Private Function EsquemaArticulo() As DataTable
            Dim ldtArticulo As DataTable = New DataTable("Articulo")
            With ldtArticulo.Columns
                .Add("vch_CodigoItem", GetType(String))
            End With
            Return ldtArticulo
        End Function

        'Exportamos a SQL datos mediante XML
        Public Function fnc_Articulo_Grabar(ByVal dtbArticulos As DataTable) As String
            Dim sqlCon As AccesoDatosSQLServer
            Dim objPedido As New Logistica.clsPedidos

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dtbArticulos.TableName = "Articulo"
                Dim strXMLArticulo As String = objPedido.GeneraXml(dtbArticulos)
                Dim objParametros() As String = {"strNumeroOrdenCompra", strXMLArticulo}
                Dim objTabla As New DataTable
                sqlCon.ObtenerDataTable("usp_LOG_OrdenCompraReporte", objParametros)
                Return strXMLArticulo
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' --- Funcion Genera Trama XML
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

        ''' <summary>
        ''' Busca la informacion del Documento para la impresion del Voucher
        ''' </summary>
        ''' <param name="strNumDocumento"></param>
        ''' <param name="strTipoDocumento"></param>
        ''' <param name="strCodAlmacen"></param>
        ''' <param name="strCodEmpresa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ufn_BuscaDatosDocumentos(ByVal strNumDocumento As String, ByVal strTipoDocumento As String, ByVal strCodAlmacen As String, ByVal strCodEmpresa As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Dim objParametros() As Object = {"pstr_NumDocumento", strNumDocumento,
                                                 "pstr_TipoDocumento", strTipoDocumento,
                                                 "pstr_CodAlmacen", strCodAlmacen,
                                                 "pstr_CodEmpresa", strCodEmpresa}

                Return sqlCon.ObtenerDataTable("usp_LOG_BuscarDatosDocumentos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Actualiza el estado al documento que se imprimio
        ''' </summary>
        ''' <param name="strNumDocumento"></param>
        ''' <param name="strTipoDocumento"></param>
        ''' <param name="strCodAlmacen"></param>
        ''' <param name="strCodEmpresa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ufn_ActualizaDocumentoImpreso(ByVal strNumDocumento As String, ByVal strTipoDocumento As String, ByVal strCodAlmacen As String, ByVal strCodEmpresa As String) As Integer
            Dim sqlCon As AccesoDatosSQLServer
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Dim objParametros() As Object = {"pstr_NumDocumento", strNumDocumento,
                                                 "pstr_TipoDocumento", strTipoDocumento,
                                                 "pstr_CodAlmacen", strCodAlmacen,
                                                 "pstr_CodEmpresa", strCodEmpresa}

                Return sqlCon.EjecutarComando("usp_LOG_ActualizaDocumentoImpreso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '---------------------------------------------------------------------------------------------
        'PEDIDOS EPPS - DG - 14/09/2018 - INI
        '---------------------------------------------------------------------------------------------
        'fncGuardaPedidoEPPsOtros
        Public Function fncGuardaPedidoEPPsOtros(ByVal chrTipo As String, ByVal strNumPedido As String, ByVal strNumItem As String, ByVal dblCantidad As Double, _
                                                 ByVal strCodAuxiliar As String, ByVal strCodDestino As String, ByVal strCodUsuario As String, _
                                                 ByVal strAlmacen As String, ByVal strObservacion As String, ByVal strCodSolicitante As String, ByVal strTiPedido As String, _
                                                 ByVal strFecInstal As String, ByVal strObsDet As String, ByVal strTurno As String, ByVal strTipoEpps As String, ByVal strCodOrdenTrabajo As String, _
                                                    ByVal strCodResponsableOT As String) As DataTable

            Dim ldtlPedido As DataTable
            Dim strGuardaPedido As String
            Try
                Dim objParametros As Object() = {"chr_tipo", chrTipo, "nu_pedi", strNumPedido, "co_item", strNumItem, _
                "ca_pedi", dblCantidad, "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                 "co_usua_crea", strCodUsuario, "co_alma", strAlmacen, _
                "de_obse", strObservacion, "co_usua_soli", strCodSolicitante, _
                "ti_pedido", strTiPedido, "fe_instal", strFecInstal, _
                "de_obse_001", strObsDet, "turno_ped", strTurno, "ti_epot", strTipoEpps, "pvch_CodOrdenTrabajo", strCodOrdenTrabajo, _
                "pvch_CodResponsableOT", strCodResponsableOT}

                strGuardaPedido = "usp_qry_PedidoAlmacen_Registra_EPPsOtros"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGuardaPedido, objParametros)
            Catch ex As Exception
                Throw
                ldtlPedido = Nothing
            End Try
            Return ldtlPedido
        End Function

        Public Function fncPedidoCambiaEstadoEPPsOtros(ByVal strNumPedido As String, ByVal strUsuario As String) As DataTable
            Dim ldtlPedido As DataTable
            Dim strAprobarPedido As String
            Try
                Dim objParametros As Object() = {"NU_PEDI", strNumPedido, "CO_USUA_MODI", strUsuario}
                strAprobarPedido = "usp_qry_PedidoAprobacion_EPPsOtros"
                ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strAprobarPedido, objParametros)
            Catch ex As Exception
                Throw
                ldtlPedido = Nothing
            End Try
            Return ldtlPedido
        End Function
        Public Function prcRegistraDetallePedidoEPPsOtros(ByVal chrTipo As String, ByVal strNumPedido As String, _
                    ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, _
                    ByVal strCodDestino As String, ByVal strCodUsuario As String, _
                    ByVal strAlmacen As String, ByVal strObservacion As String, ByVal strCodSolicitante As String, _
                    ByVal strSecuencia As String, _
                    ByVal strTipPedid As String, ByVal strFecInstal As String, _
                    ByVal str_obsdet As String, ByVal strOrdenTrabajo As String) As DataTable
            Dim lstrListaDetallePedido As String
            Dim ldtbListaDetallePedido As DataTable
            Try
                ldtbListaDetallePedido = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "nu_pedi", strNumPedido, _
                                                "co_item", strNumItem, "ca_pedi", dblCantidad, _
                                                "co_auxi_empr", strCodAuxiliar, "co_dest_fina", strCodDestino, _
                                                "co_usua_crea", strCodUsuario, _
                                                "co_alma", strAlmacen, "de_obse", strObservacion, _
                                                "co_usua_soli", strCodSolicitante, "strSec", strSecuencia, _
                                                "ti_pedido", strTipPedid, "fe_instal", strFecInstal, _
                                                "de_obse_001", str_obsdet, "pvch_OrdenTrabajo", strOrdenTrabajo
                                                }
                lstrListaDetallePedido = "usp_qry_PedidoAlmacen_Actulizar_EPPsOtros"
                ldtbListaDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrListaDetallePedido, objParametros)
            Catch ex As Exception
                ldtbListaDetallePedido = Nothing
                Throw
            End Try
            Return ldtbListaDetallePedido
        End Function
        ' --- Consulta el cabecera de pedidos
        Public Function fncConsultaPedidosEPPsOtros(ByVal strTipo As String, ByVal strSerie As String, _
                        ByVal intCodPedido As Integer, ByVal dtFecIni As String, ByVal dtFecFin As String, _
                        ByVal strSolicitante As String, ByVal strEstado As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido, _
                    "dtFecIni", dtFecIni, "dtFecFin", dtFecFin, "strSolicitante", strSolicitante, "strEstado", strEstado}
                strConsultaPedido = "usp_qry_PedidoAlmacen_Consultar_EPPSOTROS"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function
        ' Funcion Graba vale de Salida
        Public Function fnc_PedidoValesDetalleEPPsOtros_Grabar(ByVal strUsuarioDespachador As String, ByVal strNumeroPedido As String, ByVal strUsuarioRecepcionador As String) As DataTable
            Dim strDespachoPedidos As String = ""
            Dim m_sqlDtAccTintoreria As AccesoDatosSQLServer

            strDespachoPedidos = "usp_qry_PedidoValesAlmacenGrabarEPPsOtros"

            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objParametros() As String = {"var_UsuarioDesp", strUsuarioDespachador, "vch_NumeroPedido", strNumeroPedido, "var_UsuarioRecep", strUsuarioRecepcionador}
                Return m_sqlDtAccTintoreria.ObtenerDataTable(strDespachoPedidos, objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        ' --- Consulta el Detalle de Pedidos
        Public Function fncConsultaDetallePedidoEPPsOtros(ByVal strTipo As String, ByVal strSerie As String, ByVal intCodPedido As Integer) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbDetallePedido As DataTable
            Try
                ldtbDetallePedido = New DataTable
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido}
                'strConsultaPedido = "usp_qry_DetallePedidoAlmacen_Consultar"
                strConsultaPedido = "usp_qry_DetallePedidoAlmacen_ConsultarEPPsOtros"

                ldtbDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbDetallePedido
        End Function
        Public Function ufn_BuscaDatosDocumentosEPPsOtros(ByVal strNumDocumento As String, ByVal strTipoDocumento As String, ByVal strCodAlmacen As String, ByVal strCodEmpresa As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Dim objParametros() As Object = {"pstr_NumDocumento", strNumDocumento,
                                                 "pstr_TipoDocumento", strTipoDocumento,
                                                 "pstr_CodAlmacen", strCodAlmacen,
                                                 "pstr_CodEmpresa", strCodEmpresa}

                Return sqlCon.ObtenerDataTable("usp_LOG_BuscarDatosDocumentosEPPsOtros", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function fncConsultaDetallePedidoEPPsOtrosPorUsuario(ByVal strSerie As String, ByVal intCodPedido As Integer, ByVal strUsuarioRecep As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim ldtbDetallePedido As DataTable
            Dim strConsultaPedido As String
            Try
                ldtbDetallePedido = New DataTable
                Dim objParametros As Object() = {"intNumPedido", intCodPedido, "vchSerie", strSerie, "vch_UsuarioRecep", strUsuarioRecep}

                strConsultaPedido = "usp_qry_DetallePedidoAlmacen_ConsultarEPPsOtros_x_Usuario"

                ldtbDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtbDetallePedido
        End Function
        Public Function ObtenerUltimaHuellaRegistrado(ByVal strTipoPed As String) As String
            Dim codRecepcion As String
            Dim strConsultaUltimaHuella As String
            Try
                Dim objParametros As Object() = {"vchTipoPed", strTipoPed}
                strConsultaUltimaHuella = "OBTENER_ULTIMA_HUELLA_REGISTRO_POR_TIPO_PEDIDO"
                codRecepcion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis).ObtenerDataTable(strConsultaUltimaHuella, objParametros).Rows(0).Item("CodigoEmpleado")

            Catch ex As Exception
                Throw ex
            End Try
            Return codRecepcion
        End Function
        Public Function ActualizaEstadoPedidoEPPS(ByVal strUsuario As String, ByVal strNumPedido As String) As String
            Dim codRecepcion As String
            Dim strConsultaActEstadoPedido As String
            Try
                Dim objParametros As Object() = {"var_Usuario", strUsuario, "vch_NumeroPedido", strNumPedido}
                strConsultaActEstadoPedido = "usp_qry_PedidoActualizaDespachoEpps"
                codRecepcion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaActEstadoPedido, objParametros).Rows(0).Item("Resultado")
            Catch ex As Exception
                Throw ex
            End Try
            Return codRecepcion
        End Function
        Public Function ActualizaEstadoPedido(ByVal strUsuario As String, ByVal strNumPedido As String) As String
            Dim codRecepcion As String = ""
            Dim strConsultaActEstadoPedido As String
            Dim mobjConexion As AccesoDatosSQLServer
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"var_Usuario", strUsuario, "vch_NumeroPedido", strNumPedido}
                strConsultaActEstadoPedido = "usp_qry_PedidoActualizaDespacho"
                'codRecepcion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaActEstadoPedido, objParametros).Rows(0).Item("Resultado")
                mobjConexion.EjecutarComando(strConsultaActEstadoPedido, objParametros)
            Catch ex As Exception
                codRecepcion = "error"
            End Try
            Return codRecepcion
        End Function
        Public Function ValidarDespachoRepetidoPorPedidoUsuario(ByVal strNuPedido As String, ByVal strCodEmpleado As String) As String
            Dim codRecepcion As String
            Dim strConsultaValidacion As String
            Try
                Dim objParametros As Object() = {"NU_PEDI", strNuPedido, "CO_EMPL", strCodEmpleado}
                strConsultaValidacion = "USP_VALIDAR_REPETICION_DESPACHO_POR_PEDIDO"
                codRecepcion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaValidacion, objParametros).Rows(0).Item("RESULTADO")
            Catch ex As Exception
                Throw ex
            End Try
            Return codRecepcion
        End Function
        '---------------------------------------------------------------------------------------------
        'PEDIDOS EPPS - DG - 14/09/2018 - FIN
        '---------------------------------------------------------------------------------------------
        'REQSIS201900029 - DG - INI
        Public Function ActualizarPedidoFechaInstalacion(ByVal strNuPedido As String, ByVal strFechInstal As String, Optional ByVal strEmpr As String = "01") As Boolean
            Dim lobjCon As Boolean = True
            Dim Params() As Object = {"CO_EMPR", strEmpr, _
                                      "NU_PEDI", strNuPedido, _
                                      "FE_IN", strFechInstal}
            Try
                Dim objparametros() As Object = {"CO_EMPR", strEmpr, "NU_PEDI", strNuPedido, "FE_IN", strFechInstal}
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).EjecutarComando("USP_ACTUALIZAR_PEDIDO_FECHA_INSTALACION", objparametros)
            Catch ex As Exception
                lobjCon = False
            End Try
            Return lobjCon
        End Function
        Public Function ActualizarRequiFechaInstalacion(ByVal strNuReq As String, ByVal strFechInstal As String, ByVal strFecFin As String, Optional ByVal strEmpr As String = "01") As Boolean
            Dim lobjCon As Boolean = True
            Dim Params() As Object = {"CO_EMPR", strEmpr, _
                                      "NU_REQ", strNuReq, _
                                      "FE_IN", strFechInstal, _
                                      "FE_FI", strFecFin}
            Try
                Dim objparametros() As Object = {"CO_EMPR", strEmpr, "NU_REQ", strNuReq, "FE_IN", strFechInstal, "FE_FI", strFecFin}
                lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).EjecutarComando("USP_ACTUALIZAR_PEDIDO_FECHA_INSTALACION_REQUISICION", objparametros)
            Catch ex As Exception
                lobjCon = False
            End Try
            Return lobjCon
        End Function
        Public Function fnc_ConsultaMontoOT(ByVal strAnno As String, ByVal strOT As String) As String
            Dim strResult As String = ""
            Dim strConsultaValidacion As String
            Dim Params() As Object = {"CO_ANNO", strAnno, _
                                      "CO_OT", strOT}
            Try
                Dim objparametros() As Object = {"CO_ANNO", strAnno, "CO_OT", strOT}
                strConsultaValidacion = "USP_DIF_PRES_GAST"
                strResult = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaValidacion, objparametros).Rows(0).Item("RESULTADO")
            Catch ex As Exception
                strResult = "error"
            End Try
            Return strResult
        End Function
        Public Function ValidarCuentaGastoOT(ByVal strCtGasto As String) As String
            Dim strResult As String = ""
            Dim strConsultaValidacion As String
            Dim Params() As Object = {"CO_GASTO", strCtGasto}
            Try
                Dim objparametros() As Object = {"CO_GASTO", strCtGasto}
                strConsultaValidacion = "USP_VALIDAR_CTA_GASTO_CON_OT"
                strResult = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis).ObtenerDataTable(strConsultaValidacion, objparametros).Rows(0).Item("RESULTADO")
            Catch ex As Exception
                strResult = "error"
            End Try
            Return strResult
        End Function
        'REQSIS201900029 -DG - FIN
        'NUEVA FUNCION PARA CONSULTAR PEDIDOS CLIENTES - DG- INI
        Public Function BuscarPedidoCliente(ByVal strSerie As String, ByVal strNumero As String) As DataSet
            Dim sqlCon As AccesoDatosSQLServer
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)

                Dim objParametros() As Object = {"pstr_Serie", strSerie,
                                                 "pstr_Numero", strNumero}

                Return sqlCon.ObtenerDataSet("USP_LISTAR_PEDIDO_STATUS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function BuscarDatosSalida(ByVal intsalida As Integer) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Dim objParametros() As Object = {"pint_NroSalida", intsalida}

                'Return sqlCon.ObtenerDataTable("USP_OBTENER_DATOS_SALIDA", objParametros)
                Return sqlCon.ObtenerDataTable("USP_LISTAR_GUIA_DATOS_SALIDA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GrabarStatusSalida(ByVal intSalida As Integer, ByVal strEstado As String, ByVal strUsuario As String) As Boolean
            Dim lobjCon As Boolean = True
            Dim sqlCon As AccesoDatosSQLServer
            sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim Params() As Object = {"INT_SALIDA", intSalida, _
                                      "VCH_STATUS", strEstado, _
                                      "VCH_USUARIO", strUsuario}
            Try
                lobjCon = sqlCon.EjecutarComando("USP_GRABAR_STATUS_SALIDA", Params)
            Catch ex As Exception
                lobjCon = False
            End Try
            Return lobjCon
        End Function
        Public Function GrabarStatusGuiaSalida(ByVal intSalida As Integer, ByVal strNroPedido As String, ByVal strGuia As String, ByVal strEstado As String,
                                               Optional ByVal strUsuario As String = "", Optional ByVal strCodAyudante1 As String = "0", Optional ByVal strCodAyudante2 As String = "0",
                                               Optional ByVal strCodAyudante3 As String = "0") As Boolean
            Dim lobjCon As Boolean = True
            Dim sqlCon As AccesoDatosSQLServer
            sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim Params() As Object = {"pint_NroSalida", intSalida, _
                                      "pvch_NroPedido", strNroPedido, _
                                      "pvch_NroGuia", strGuia, _
                                      "pvch_EstadoGuia", strEstado, _
                                      "pvch_Usuario", strUsuario, _
                                      "pvch_CodAyudante1", strCodAyudante1, _
                                      "pvch_CodAyudante2", strCodAyudante2, _
                                      "pvch_CodAyudante3", strCodAyudante3}
            Try
                lobjCon = sqlCon.EjecutarComando("Usp_ActualizarEstadoGuias_V3", Params)
                'lobjCon = sqlCon.EjecutarComando("Usp_ActualizarEstadoGuias_V2", Params)
            Catch ex As Exception
                lobjCon = False
            End Try
            Return lobjCon
        End Function
        'NUEVA FUNCION PARA CONSULTAR PEDIDOS CLIENTES - DG- FIN
        'PEDIDO CONTRA ENTREGA  -DG - INI
        Public Function ObtenerPedidoCE(ByVal strGuia As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)

                Dim objParametros() As Object = {"VAR_GUIA", strGuia}

                Return sqlCon.ObtenerDataTable("USP_LISTAR_PEDIDOS_CE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function AprobarDespachoPedidoCE(ByVal strGuia As String, ByVal strUsuario As String) As Boolean
            Dim lobjCon As Boolean = True
            Dim sqlCon As AccesoDatosSQLServer
            sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim Params() As Object = {"VAR_GUIA", strGuia, "VAR_USUARIO", strUsuario}
            Try
                lobjCon = sqlCon.EjecutarComando("USP_APROBAR_PEDIDO_CE", Params)
            Catch ex As Exception
                lobjCon = False
            End Try
            Return lobjCon
        End Function
        Public Function DesAprobarDespachoPedidoCE(ByVal strGuia As String, ByVal strUsuario As String, ByVal strMotivo As String) As Boolean
            Dim lobjCon As Boolean = True
            Dim sqlCon As AccesoDatosSQLServer
            sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim Params() As Object = {"VAR_GUIA", strGuia, "VAR_USUARIO", strUsuario, "VAR_MOTIVO", strMotivo}
            Try
                lobjCon = sqlCon.EjecutarComando("USP_DESAPROBAR_PEDIDO_CE", Params)
            Catch ex As Exception
                lobjCon = False
            End Try
            Return lobjCon
        End Function
        'PEDIDO CONTRA ENTREGA - DG - FIN
#Region "PRE-DESPACHO"
        Function ufn_GrabarReservaPreDespacho(ByVal strNumPedido As String, ByVal strCodEmpresa As String,
                                            ByVal strCodUnidad As String, ByVal strCodAlmacen As String,
                                            ByVal strUsuario As String, ByVal dtDetReserva As DataTable) As String


            Dim sqlCon As AccesoDatosSQLServer
            Dim strResult As String
            'Dim objPedido As New Logistica.clsPedidos

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dtDetReserva.TableName = "RESERVA"
                Dim strXMLDetReserva As String = GeneraXml(dtDetReserva)
                Dim objParametros As Object() = {"vch_NumPedido", strNumPedido,
                                                 "vch_CodEmpresa", strCodEmpresa,
                                                 "vch_CodUnidad", strCodUnidad,
                                                 "vch_CodAlmacen", strCodAlmacen,
                                                 "vch_Usuario", strUsuario,
                                                 "xml_DetReserva", strXMLDetReserva}
                'Dim objTabla As New DataTable
                strResult = sqlCon.ObtenerValor("usp_LOG_PREDESPACHO_GrabaReserva", objParametros)
                ' retornamos el codido de la reserva.
                Return strResult
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function ufn_BuscarDatosEtiquetaPreDespacho(ByVal strCodReserva As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtResult As DataTable

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"vch_CodReserva", strCodReserva}

                dtResult = sqlCon.ObtenerDataTable("usp_LOG_PREDESPACHO_ObtenerDatosEtiqueta", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ufn_BuscarDetallePedido_ConsultaPocket(ByVal strNumSerie As String, ByVal strNumPedido As Integer) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtResult As DataTable

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"vch_Serie", strNumSerie,
                                                 "int_NumPedido", strNumPedido}

                dtResult = sqlCon.ObtenerDataTable("usp_LOG_PREDESPACHO_ConsultaDetPedido_Pocket", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ufn_GrabarReservaPreDespacho_Pocket(ByVal strSerie As String, ByVal intpedido As Integer, ByVal strUsuario As String, ByVal dtDetReserva As DataTable) As String

            Dim sqlCon As AccesoDatosSQLServer
            Dim strResult As String
            'Dim objPedido As New Logistica.clsPedidos

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                dtDetReserva.TableName = "RESERVA"
                Dim strXMLDetReserva As String = GeneraXml(dtDetReserva)
                Dim objParametros As Object() = {"vch_Serie", strSerie,
                                                 "int_NumPedido", intpedido,
                                                 "vch_Usuario", strUsuario,
                                                 "xml_DetReserva", strXMLDetReserva}
                'Dim objTabla As New DataTable
                strResult = sqlCon.ObtenerValor("usp_LOG_PREDESPACHO_GrabaReserva_Pocket", objParametros)
                ' retornamos el codido de la reserva.
                Return strResult
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Function ufn_BuscarDetalleComanda_ConsultaPocket(ByVal strCodReserva As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtResult As DataTable

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"pvch_CodReserva", strCodReserva}

                dtResult = sqlCon.ObtenerDataTable("usp_LOG_PREDESPACHO_BuscarDatosComanda", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ufn_BuscarDetalleReserva_ConsultaPocket(ByVal strCodReserva As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtResult As DataTable

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"vch_CodigoReserva", strCodReserva}

                dtResult = sqlCon.ObtenerDataTable("usp_LOG_PREDESPACHO_ConsultaDetReserva_Pocket", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ufn_GenerarDespachoReserva_Pocket(ByVal strCodReserva As String, ByVal strNumPedido As String, ByVal strUsuario As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtResult As DataTable

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"vch_CodigoReserva", strCodReserva,
                                                 "vch_NumeroPedido", strNumPedido,
                                                 "vch_CodUsuario", strUsuario}

                dtResult = sqlCon.ObtenerDataTable("usp_LOG_PREDESPACHO_GenerarDespachoReserva_Pocket", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ActualizarCantidadAprobada(ByVal strPedi As String, ByVal strSecu As String, ByVal strCantidad As String) As DataTable
            Dim sqlCon As AccesoDatosSQLServer
            Dim dtResult As DataTable

            Try
                sqlCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim objParametros As Object() = {"vch_nupedi", strPedi, "vch_secu", strSecu, "vch_cantidad", strCantidad}

                dtResult = sqlCon.ObtenerDataTable("USP_ACTUALIZAR_CANTIDAD_PEDIDO_POR_ITEM", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "CHEKLIST-APILADORES"
        ''' Funciones APILADORES 
        Public Function fncCheckListApiladoresCRUD(ByVal strAccion As String, ByVal strCodEmpl As String, ByVal intTurno As String, ByVal strFechaChkList As String,
                                                   ByVal strNumMaquina As String, ByVal strHorometroE As String, ByVal strHorometroS As String,
                                                   ByVal strMcaFlag1 As String, ByVal strMcaFlag2 As String, ByVal strMcaFlag3 As String,
                                                   ByVal strMcaFlag4 As String, ByVal strMcaFlag5 As String, ByVal strMcaFlag6 As String,
                                                   ByVal strMcaFlag7 As String, ByVal strMcaFlag8 As String, ByVal strMcaFlag9 As String,
                                                   ByVal strMcaFlag10 As String, ByVal strMcaFlag11 As String, ByVal strMcaFlag12 As String, ByVal strMcaFlag13 As String,
                                                   ByVal strObservaciones1 As String, ByVal strObservaciones2 As String, ByVal strObservaciones3 As String, ByVal strUsuario As String) As DataTable
            Dim ldtbValida As DataTable
            Try
                ldtbValida = New DataTable
                Dim objParametros As Object() = {"pvch_accion", strAccion,
                                                 "pvch_cod_empl", strCodEmpl,
                                                 "pint_turno", intTurno,
                                                 "pvch_fecha_chk", strFechaChkList,
                                                 "pvch_num_maquina", strNumMaquina,
                                                 "pvch_horometro_e", strHorometroE,
                                                 "pvch_horometro_s", strHorometroS,
                                                 "pvch_mca_flag1", strMcaFlag1,
                                                 "pvch_mca_flag2", strMcaFlag2,
                                                 "pvch_mca_flag3", strMcaFlag3,
                                                 "pvch_mca_flag4", strMcaFlag4,
                                                 "pvch_mca_flag5", strMcaFlag5,
                                                 "pvch_mca_flag6", strMcaFlag6,
                                                 "pvch_mca_flag7", strMcaFlag7,
                                                 "pvch_mca_flag8", strMcaFlag8,
                                                 "pvch_mca_flag9", strMcaFlag9,
                                                 "pvch_mca_flag10", strMcaFlag10,
                                                 "pvch_mca_flag11", strMcaFlag11,
                                                 "pvch_mca_flag12", strMcaFlag12,
                                                 "pvch_mca_flag13", strMcaFlag13,
                                                 "pvch_observaciones1", strObservaciones1,
                                                 "pvch_observaciones2", strObservaciones2,
                                                 "pvch_observaciones3", strObservaciones3,
                                                 "pvch_usuario", strUsuario}
                ldtbValida = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable("USP_LOG_CHECK_LIST_APILADORES_MANTENEDOR", objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbValida
        End Function

#End Region
    End Class
End Namespace