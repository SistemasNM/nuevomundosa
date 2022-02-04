Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Data.OleDb
Imports NM.AccesoDatos
Imports NM_General

Namespace Logistica
    Public Class clsPedidos
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

        ' --- Consulta el Detalle de Pedidos
        Public Function fncConsultaPedidos(ByVal strTipo As String, ByVal strSerie As String, _
                        ByVal intCodPedido As Integer, ByVal dtFecIni As String, ByVal dtFecFin As String, _
                        ByVal strSolicitante As String, ByVal strEstado As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbCabeceraPedido As DataTable
            Try
                ldtbCabeceraPedido = New DataTable
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido, _
                    "dtFecIni", dtFecIni, "dtFecFin", dtFecFin, "strSolicitante", strSolicitante, "strEstado", strEstado}
                strConsultaPedido = "usp_qry_PedidoConsultar"
                ldtbCabeceraPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try

            Return ldtbCabeceraPedido
        End Function

        ' --- Consulta el Detalle de Pedidos
        Public Function fncConsultaDetallePedido(ByVal strTipo As String, ByVal strSerie As String, ByVal intCodPedido As Integer) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbDetallePedido As DataTable
            Try
                ldtbDetallePedido = New DataTable
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido}
                strConsultaPedido = "usp_qry_ConsultarDetallePedido"
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
                                           ByVal strResponsable As String, ByVal strTipoConsulta As String) As DataTable
            Dim strConsultaPedido As String
            Dim ldtbListaPedido As DataTable
            Try
                ldtbListaPedido = New DataTable

                Select Case strTipoConsulta
                    Case "Consulta"
                        strConsultaPedido = "usp_qry_PedidoListaConsulta"
                        Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, _
                                                "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, _
                                                "strEstado", strEstado, "strUsuario", strUsuaario}
                        ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
                    Case "Aprobaciones"
                        strConsultaPedido = "usp_qry_PedidoListaAprobados"
                        Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, _
                                                "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, _
                                                "strEstado", strEstado, "strUsuario", strUsuaario, "strResponsable", strResponsable}
                        ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
                    Case "Atender"
                        strConsultaPedido = "usp_qry_PedidoListaAtender"
                        Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                         "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, _
                                                         "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, _
                                                         "strEstado", strEstado}
                        ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
                End Select

            Catch ex As Exception
                ldtbListaPedido = Nothing
            End Try

            Return ldtbListaPedido
        End Function

        ' --- Listado de Pedidos Registrados
        Public Function fncPedidosGeneral(ByVal strTipo As String, ByVal strSerie As String, _
                                           ByVal strFechaIni As String, ByVal strFechaFin As String, _
                                           ByVal strSolicitante As String, ByVal strEstado As String, _
                                           ByVal strUsuario As String) As DataTable
            Dim strConsultaPedidoGeneral As String
            Dim ldtbListaPedidoGeneral As DataTable
            Try
                ldtbListaPedidoGeneral = New DataTable
                strConsultaPedidoGeneral = "usp_qry_PedidoConsultaGeneral"
                Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, _
                                                "dtFecIni", strFechaIni, "dtFecFin", strFechaFin, _
                                                "strSolicitante", strSolicitante, "strEstado", strEstado, _
                                                "strUsuario", strUsuario}
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
                    ByVal strSecuencia As String) As DataTable

            Dim lstrListaDetallePedido As String
            Dim ldtbListaDetallePedido As DataTable
            Try
                ldtbListaDetallePedido = New DataTable
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, _
                                                "CO_ITEM", strNumItem, "CA_PEDI", dblCantidad, _
                                                "CO_AUXI_EMPR", strCodAuxiliar, "CO_DEST_FINA", strCodDestino, _
                                                "CO_ORDE_SERV", strCodOrden, "CO_USUA_CREA", strCodUsuario, _
                                                "CO_ALMA", strAlmacen, "DE_OBSE", strObservacion, _
                                                "CO_USUA_SOLI", strCodSolicitante, "strSec", strSecuencia}

                lstrListaDetallePedido = "usp_qry_PedidoActulizar"
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
                lstrDuplicadoDetallePedido = "usp_qry_ConsultaDuplicadoDetallePedidos"
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
                    ByVal strCodSolicitante As String) As DataTable

            Dim ldtlPedido As DataTable
            Dim strGuardaPedido As String
            Try
                Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, "CO_ITEM", strNumItem, _
                "CA_PEDI", dblCantidad, "CO_AUXI_EMPR", strCodAuxiliar, "CO_DEST_FINA", strCodDestino, _
                "CO_ORDE_SERV", strCodOrden, "CO_USUA_CREA", strCodUsuario, "CO_ALMA", strAlmacen, _
                "DE_OBSE", strObservacion, "CO_USUA_SOLI", strCodSolicitante}

                strGuardaPedido = "usp_qry_PedidoRegistra"
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
                Dim objParametros As Object() = {"chr_tipo", strTipo, "NU_PEDI", strNumPedido, "CO_USUA_MODI", strUsuario}
                pdtCorreos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strActualizarPedido, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return pdtCorreos
        End Function

        ' --- Actualizar Cantidades - Pedido
        Public Function fncActualizarCantidades(ByVal strTipo As String, ByVal strNumPedido As String, _
                                                ByVal strNumeroItem As String, ByVal dblCantidadAprobada As Double, _
                                                ByVal strUsuario As String, ByVal strCtaGasto As String, _
                                                ByVal strOrdenServicio As String) As DataTable
            Dim strActualizarCantidades As String
            Dim ldtbActualizarCantidades As DataTable
            Try
                ldtbActualizarCantidades = New DataTable
                Dim objParametros As Object() = {"chr_tipo", strTipo, "NU_PEDI", strNumPedido, _
                                                "CO_ITEM", strNumeroItem, "CA_APRO", dblCantidadAprobada, _
                                                "CO_USUA_MODI", strUsuario, "CO_DEST_FINA", strCtaGasto, _
                                                "CO_ORDEN_SERV", strOrdenServicio}
                strActualizarCantidades = "usp_qry_PedidoActualizaCantidades"
                ldtbActualizarCantidades = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strActualizarCantidades, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbActualizarCantidades
        End Function

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

        ' --- Listar Seguimiento
        Public Function fnc_ListarSeguimientoPedido(ByVal intCodigoPedido As Int16, ByRef ldtbSeguimiento As DataTable) As String
            Dim lstrError As String = ""
            Dim strConsultaSeguimiento As String
            Try
                strConsultaSeguimiento = "usp_qry_PedidoListarSeguimiento"
                Dim objParametros() As Object = {"intCodigoPedido", intCodigoPedido}
                ldtbSeguimiento = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaSeguimiento, objParametros)
            Catch ex As Exception
                lstrError = "Error : " & Chr(13) & ex.Message
            End Try
            Return lstrError
        End Function

        ' --- Listar Pedidos pendientes de despacho
        Public Function fnc_ListarPedidoPorDespachar(ByVal strTipo As String, ByVal strNumeroPedido As String, ByVal strUsuario As String) As DataTable
            Dim strConsultaporDespachar As String
            Dim ldtbPorDespachar As DataTable
            Try
                strConsultaporDespachar = "usp_qry_PedidoListaPorDespachar"
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
        Public Function fnc_PedidoValesDetalle_Grabar(ByVal strAlmacen As String, ByVal strVale As String, _
                                            ByVal strCodigoCC As String, ByVal strActivo As String, _
                                            ByVal strFecha As String, _
                                            ByVal strObservacion As String, ByVal strUsuario As String, _
                                            ByVal dtbDatos As DataTable, ByVal strNumeroPedido As String) As DataTable
            Dim m_sqlDtAccTintoreria As AccesoDatosSQLServer
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Try
                Dim objPedido As New Logistica.clsPedidos
                ' Generamos XML
                dtbDatos.TableName = "Detalle"
                Dim strXMLDatos As String = objPedido.GeneraXml(dtbDatos)

                Dim objParametros() As String = {"var_Almacen", strAlmacen, "var_NumeroVale", strVale, _
                                            "var_CentroCosto", strCodigoCC, "var_ActivoFijo", strActivo, _
                                            "var_Fecha", strFecha, "var_Observacion", strObservacion, _
                                            "var_Usuario", strUsuario, "var_XMLDatos", strXMLDatos, "strNumeroPedido", strNumeroPedido}

                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_PedidoValesGrabar", objParametros)

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
            Dim strDetalleVale As String
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
    End Class
End Namespace