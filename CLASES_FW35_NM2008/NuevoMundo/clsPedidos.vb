Imports System.Data
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Public Class clsPedidos

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

    ' --- Consulta la Cabecera de Pedidos
    Public Function fncConsultarPedido(ByVal strTipo As String, ByVal strSerie As String, ByVal intCodPedido As Integer, ByVal strFechaIni As String, ByVal strFechaFin As String, ByVal strSolicitante As String, ByVal strEstado As String) As clsPedidos
        Dim strConsultaPedido As String
        Dim objPedidoBE As New clsPedidos
        Try
            Dim drPedido As SqlDataReader
            Dim ldtbPedido As DataTable
            Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, "strEstado", strEstado}
            strConsultaPedido = "usp_qry_ConsultarPedido"
            drPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataReader("usp_qry_ConsultarPedido", objParametros)

            If drPedido.Read() Then
                objPedidoBE.SeriePedido = Mid(drPedido("nu_pedi"), 1, 4)
                objPedidoBE.NumPedido = Mid(drPedido("nu_pedi"), 6, 10)
                objPedidoBE.CodAlmacen = drPedido("co_alma")
                objPedidoBE.DesAlmacen = drPedido("de_alma")
                objPedidoBE.CodSolicitante = drPedido("CodSolicitante")
                objPedidoBE.DesSolicitante = drPedido("NomSolicitante")
                objPedidoBE.CodUsuarioSolicitante = drPedido("co_usua_crea")
                objPedidoBE.CodCentroCostos = drPedido("CodCentroCostos")
                objPedidoBE.DesCentroCostos = drPedido("DesCentroCostos")
                objPedidoBE.CodCuentaGastos = drPedido("CodCuentaGastos")
                objPedidoBE.DesCuentaGastos = drPedido("DesCtaGastos")
                objPedidoBE.CodOrdenServicio = drPedido("co_orde_serv")
                objPedidoBE.FecPedido = drPedido("fe_pedi")
                objPedidoBE.FecAprobacion = drPedido("fe_apro")
                objPedidoBE.FecAtencion = drPedido("fe_aten")
                objPedidoBE.EstadoPedido = drPedido("ti_situ")
                objPedidoBE.ObsPedido = drPedido("de_obse")
            Else
                objPedidoBE = Nothing
            End If
            drPedido.Close()
        Catch ex As Exception
            Throw
        End Try
        Return objPedidoBE
    End Function

    ' --- Consulta el Detalle de Pedidos
    Public Function fncDetallePedido(ByVal strSerie As String, ByVal intCodPedido As Integer) As DataTable
        Dim strConsultaPedido As String
        Dim ldtbDetallePedido As DataTable
        Try
            ldtbDetallePedido = New DataTable
            Dim objParametros As Object() = {"chrTipo", "1", "intNumPedido", intCodPedido, "vchSerie", strSerie}
            strConsultaPedido = "usp_qry_ConsultarDetallePedido"
            ldtbDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
        Catch ex As Exception
            Throw
        End Try

        Return ldtbDetallePedido
    End Function

    ' --- Listado de Pedidos a Atender/Aprobar
    Public Function fncListadoPedidos(ByVal strTipo As String, ByVal strSerie As String, ByVal intCodPedido As Integer, ByVal strFechaIni As String, ByVal strFechaFin As String, ByVal strSolicitante As String, ByVal strEstado As String) As DataTable
        Dim strConsultaPedido As String
        Dim ldtbListaPedido As DataTable
        Try
            ldtbListaPedido = New DataTable
            Dim objParametros As Object() = {"chrTipo", strTipo, "vchSerie", strSerie, "intNumPedido", intCodPedido, "dtFecIni", strFechaIni, "dtFecFin", strFechaFin, "strSolicitante", strSolicitante, "strEstado", strEstado}
            strConsultaPedido = "usp_qry_ConsultarPedido"
            ldtbListaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaPedido, objParametros)
        Catch ex As Exception
            ldtbListaPedido = Nothing
        End Try

        Return ldtbListaPedido
    End Function

    ' --- Registra Pedido en la tabla Temporal
    Public Function prcRegistraDetallePedidoTemporal(ByVal strStore As String, ByVal chrTipo As String, ByVal strNumPedido As String, ByVal strNumItem As String, ByVal dblCantidad As Double, ByVal strCodAuxiliar As String, ByVal strCodDestino As String, ByVal strCodOrden As String, ByVal strCodUsuario As String, ByVal dtFecPedido As String, ByVal strAlmacen As String, ByVal strObservacion As String, ByVal strCodSolicitante As String) As DataTable
        Dim lstrListaDetallePedido As String
        Dim ldtbListaDetallePedido As DataTable
        Try
            ldtbListaDetallePedido = New DataTable
            Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, "CO_ITEM", strNumItem, "CA_PEDI", dblCantidad, "CO_AUXI_EMPR", strCodAuxiliar, "CO_DEST_FINA", strCodDestino, "CO_ORDE_SERV", strCodOrden, "CO_USUA_CREA", strCodUsuario, "FE_USUA_CREA", dtFecPedido, "CO_ALMA", strAlmacen, "DE_OBSE", strObservacion, "CO_USUA_SOLI", strCodSolicitante}
            lstrListaDetallePedido = strStore
            ldtbListaDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrListaDetallePedido, objParametros)
        Catch ex As Exception
            ldtbListaDetallePedido = Nothing
            Throw
        End Try
        Return ldtbListaDetallePedido
    End Function

    ' --- Valida existencia del items en el Detalle (Temporal) 
    Public Function fncValidaDuplicadosPedidoTemporal(ByVal chrTipo As String, ByVal strNumPedido As String, ByVal strNumItem As String) As DataTable
        Dim lstrDuplicadoDetallePedido As String
        Dim ldtbDuplicadoDetallePedido As DataTable
        Try
            ldtbDuplicadoDetallePedido = New DataTable
            Dim objParametros As Object() = {"chr_Tipo", chrTipo, "NU_PEDI", strNumPedido, "CO_ITEM", strNumItem}
            lstrDuplicadoDetallePedido = "usp_qry_ConsultaDuplicadoDetallePedidos"
            ldtbDuplicadoDetallePedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(lstrDuplicadoDetallePedido, objParametros)
        Catch ex As Exception
            ldtbDuplicadoDetallePedido = Nothing
            Throw
        End Try
        Return ldtbDuplicadoDetallePedido
    End Function

    ' --- Guarda Pedidos en tablas Originales
    Public Function fncGuardaPedido(ByVal strNumPedido As String) As String
        Dim ldtlPedido As DataTable
        Dim strGuardaPedido As String
        Dim strGuardo As String = "0"
        Try
            Dim objParametros As Object() = {"NumPedido", strNumPedido}
            strGuardaPedido = "usp_qry_RegistraPedido"
            ldtlPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strGuardaPedido, objParametros)
            If ldtlPedido.Rows.Count > 0 Then
                strGuardo = ldtlPedido.Rows.Count.ToString()
            End If
        Catch ex As Exception
            Throw
            ldtlPedido = Nothing
        End Try
        Return strGuardo
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

    ' --- Anular Pedido
    Public Function fncAnularPedido(ByVal strTipo As String, ByVal strNumPedido As String, ByVal strUsuario As String) As DataTable
        Dim strAnulaPedido As String
        Dim ldtbAnulaPedido As DataTable
        Try
            ldtbAnulaPedido = New DataTable
            Dim objParametros As Object() = {"chr_tipo", strTipo, "NU_PEDI", strNumPedido, "CO_USUA_MODI", strUsuario}
            strAnulaPedido = "usp_qry_AnularPedido"
            ldtbAnulaPedido = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strAnulaPedido, objParametros)
        Catch ex As Exception
            Throw
        End Try
        Return ldtbAnulaPedido
    End Function

End Class
