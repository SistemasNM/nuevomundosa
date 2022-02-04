Imports NM.AccesoDatos
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
    Public Function ValidaPedido(ByVal strPedido As String) As DataTable
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objparametros() As Object = {"Numero_pedido", strPedido}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("USP_VEN_VALIDA_PEDIDO", objparametros)
            Return dtbDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Correlativos_Documentos() As DataSet
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim dtbDatos As DataSet = _objConexion.ObtenerDataSet("USP_LOG_SERIES_TRANSFERENCIAS")
            Return dtbDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function Lectura_Rollo(ByVal strCo_alma As String, ByVal strCo_Lote As String) As DataTable
    '    Try
    '        Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    '        Dim objparametros() As Object = {"Nu_Pedi", "SIN_PEDIDO", _
    '                                         "Co_alma", strCo_alma, _
    '                                         "co_Lote", strCo_Lote}
    '        Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("USP_LOG_LECTURA_LOTES", objparametros)
    '        Return dtbDatos
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    ''' <summary>
    ''' OBSOLETO : REmplazado por Lectura_Rollo_Valida
    ''' (20151023 - LUIS_AJ)
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Lectura_Rollo(ByVal pstrNum_Pedido As String, ByVal pstrCo_alma As String, ByVal pstrCo_Lote As String) As DataTable
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"Nu_Pedi", pstrNum_Pedido, _
                                             "Co_alma", pstrCo_alma, _
                                             "co_Lote", pstrCo_Lote}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("USP_LOG_LECTURA_LOTES", objparametros)
            Return dtbDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Lectura_Rollo_Valida(ByVal pstrNum_Pedido As String, ByVal pstrCo_alma As String, ByVal pstrCo_Lote As String, ByVal pstrValidaArticulo As String, ByVal pstrValidaColor As String, ByVal pstrValida_Presentacion As String) As DataTable
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"Nu_Pedi", pstrNum_Pedido, _
                                             "Co_alma", pstrCo_alma, _
                                             "co_Lote", pstrCo_Lote, _
                                             "pvch_Valida_Articulo", pstrValidaArticulo, _
                                             "pvch_Valida_Color", pstrValidaColor, _
                                             "pvch_Valida_Presentacion", pstrValida_Presentacion}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("USP_LOG_LECTURA_LOTES_UBICACION", objparametros)
            Return dtbDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Eliminar_Rollo_Transferencia(ByVal StrCO_USUA_MODI As String, ByVal strCO_EMPR As String, ByVal strCO_ALMA As String, _
                                                ByVal strCO_UNID As String, ByVal strTI_DOCU As String, _
                                                ByVal strNU_DOCU As String, ByVal strNU_SECU As String, _
                                                ByVal strCO_ITEM As String, ByVal strNU_ORCO As String, _
                                                ByVal strTI_MOVI As String, ByVal strCO_EMPR_TRFR As String, _
                                                ByVal strCO_UNID_TRFR As String, ByVal strCO_ALMA_TRFR As String, _
                                                ByVal strTI_DOCU_TRFR As String, ByVal strNU_DOCU_TRFR As String, _
                                                ByVal strTI_MOVI_TRFR As String, ByVal strST_TRAN As String) As Boolean
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"ISCO_USUA_MODI", StrCO_USUA_MODI, _
                                             "ISCO_EMPR", strCO_EMPR, _
                                             "ISCO_ALMA", strCO_ALMA, _
                                             "ISCO_UNID", strCO_UNID, _
                                             "ISTI_DOCU", strTI_DOCU, _
                                             "ISNU_DOCU", strNU_DOCU, _
                                             "INNU_SECU", strNU_SECU, _
                                             "ISCO_ITEM", strCO_ITEM, _
                                             "ISNU_ORCO", strNU_ORCO, _
                                             "ISTI_MOVI", strTI_MOVI, _
                                             "ISCO_EMPR_TRFR", strCO_EMPR_TRFR, _
                                             "ISCO_UNID_TRFR", strCO_UNID_TRFR, _
                                             "ISCO_ALMA_TRFR", strCO_ALMA_TRFR, _
                                             "ISTI_DOCU_TRFR", strTI_DOCU_TRFR, _
                                             "ISNU_DOCU_TRFR", strNU_DOCU_TRFR, _
                                             "ISTI_MOVI_TRFR", strTI_MOVI_TRFR, _
                                             "ISST_TRAN", strST_TRAN}
            _objConexion.EjecutarComando("SP_TDDOCU_ALMA_D01", objparametros)
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Function Mostrar_Transferencia(ByVal strNumero_Documento As String, ByVal strCo_alma As String) As DataSet
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"Numero_Docuemento", strNumero_Documento, _
                                             "Co_alma", strCo_alma}
            Dim dtbDatos As DataSet = _objConexion.ObtenerDataSet("USP_LOG_MOSTRAR_TRANSFERENCIA", objparametros)
            Return dtbDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Adjuntar_Transferencia_Pedido(ByVal strNumero_Documento As String, ByVal strNumero_Pedido As String) As Boolean
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"Numero_Documento", strNumero_Documento, _
                                             "Numero_Pedido", strNumero_Pedido}
            _objConexion.EjecutarComando("USP_LOG_TRANSFERENCIAS_BULT_GRABAR", objparametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Mostrar_Listado_Transferencias(ByVal strNumero_Pedido As String) As DataTable
        Try
            Me._objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"Numero_pedido", strNumero_Pedido}
            Dim dtbDatos As DataTable = _objConexion.ObtenerDataTable("USO_LOG_MOSTRAR_TRANSFERENCIAS_PEDIDOS", objparametros)
            Return dtbDatos
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' OBSOLETO : Remplazado por Registra_Transferencia_Tela
    ''' (20151022 - LUIS_AJ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Registra_Transferencia(ByVal strCo_Empr As String, _
                                           ByVal strCo_Unid As String, _
                                           ByVal strCo_Alma As String, _
                                           ByVal strTi_docu As String, _
                                           ByVal strNu_Docu As String, _
                                           ByVal strTi_Oper As String, _
                                           ByVal strCo_Dest_Fina As String, _
                                           ByVal strNu_Serie_Docu As String, _
                                           ByVal dtFe_Docu As String, _
                                           ByVal strTi_Auxi As String, _
                                           ByVal strCo_Auxi As String, _
                                           ByVal strOrde_Serv As String, _
                                           ByVal strNo_Pers As String, _
                                           ByVal strNu_Ruc As String, _
                                           ByVal strTi_Dire As String, _
                                           ByVal strDe_Dire As String, _
                                           ByVal strDe_Cond As String, _
                                           ByVal strCo_Tran As String, _
                                           ByVal strDe_Nomb_Tran As String, _
                                           ByVal strDe_Dire_Tran As String, _
                                           ByVal strDe_Ruc_Tran As String, _
                                           ByVal strNum_Placa As String, _
                                           ByVal strNu_Orco As String, _
                                           ByVal strNu_Guia_Prov As String, _
                                           ByVal strNu_Orpr As String, _
                                           ByVal strNu_PEPR As String, _
                                           ByVal strNu_DEPR As String, _
                                           ByVal strNu_PAPR As String, _
                                           ByVal strNu_Reqi As String, _
                                           ByVal strNu_Ortr As String, _
                                           ByVal strNu_PEDI As String, _
                                           ByVal strTI_DOCU_REFE As String, _
                                           ByVal strNU_DOCU_REFE As String, _
                                           ByVal dtFE_DOCU_REFE As String, _
                                           ByVal strDE_OBSE_0001 As String, _
                                           ByVal strDE_OBSE_0002 As String, _
                                           ByVal strCO_EMPR_TRFR As String, _
                                           ByVal strCO_UNID_TRFR As String, _
                                           ByVal strCO_ALMA_TRFR As String, _
                                           ByVal strCO_EMPR_DEST As String, _
                                           ByVal strCO_UNID_DEST As String, _
                                           ByVal strCO_ALMA_DEST As String, _
                                           ByVal strTI_DOCU_TRFR As String, _
                                           ByVal strTI_OPER_TRFR As String, _
                                           ByVal strNU_SERI_TRFR As String, _
                                           ByVal strNU_DOCU_TRFR As String, _
                                           ByVal strST_TRAN As String, _
                                           ByVal strCO_CPTO_GURR As String, _
                                           ByVal strCO_COND As String, _
                                           ByVal strDE_CPTO As String, _
                                           ByVal numNU_DOCU As String, ByRef dtCod As DataSet) As Boolean
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"ISCO_EMPR", strCo_Empr, _
                                             "ISCO_UNID", strCo_Unid, _
                                             "ISCO_ALMA", strCo_Alma, _
                                             "ISTI_DOCU", strTi_docu, _
                                             "ISNU_DOCU", strNu_Docu, _
                                             "ISTI_OPER", strTi_Oper, _
                                             "ISCO_DEST_FINA", strCo_Dest_Fina, _
                                             "ISNU_SERI_DOCU", strNu_Serie_Docu, _
                                             "IDFE_DOCU", dtFe_Docu, _
                                             "ISTI_AUXI_EMPR", strTi_Auxi, _
                                             "ISCO_AUXI_EMPR", strCo_Auxi, _
                                             "ISCO_ORDE_SERV", strOrde_Serv, _
                                             "ISNO_PERS", strNo_Pers, _
                                             "ISNU_RUCS", strNu_Ruc, _
                                             "ISTI_DIRE", strTi_Dire, _
                                             "ISDE_DIRE", strDe_Dire, _
                                             "ISDE_COND", strDe_Cond, _
                                             "ISCO_TRAN", strCo_Tran, _
                                             "ISDE_NOMB_TRAN", strDe_Nomb_Tran, _
                                             "ISDE_DIRE_TRAN", strDe_Dire_Tran, _
                                             "ISNU_RUCS_TRAN", strDe_Ruc_Tran, _
                                             "ISNU_PLAC", strNum_Placa, _
                                             "ISNU_ORCO", strNu_Orco, _
                                             "ISNU_GUIA_PROV", strNu_Guia_Prov, _
                                             "ISNU_ORPR", strNu_Orpr, _
                                             "ISNU_PEPR", strNu_PEPR, _
                                             "ISNU_DEPR", strNu_DEPR, _
                                             "ISNU_PAPR", strNu_PAPR, _
                                             "ISNU_REQI", strNu_Reqi, _
                                             "ISNU_ORTR", strNu_Ortr, _
                                             "ISNU_PEDI", strNu_PEDI, _
                                             "ISTI_DOCU_REFE", strTI_DOCU_REFE, _
                                             "ISNU_DOCU_REFE", strNU_DOCU_REFE, _
                                             "IDFE_DOCU_REFE", dtFE_DOCU_REFE, _
                                             "ISDE_OBSE_0001", strDE_OBSE_0001, _
                                             "ISDE_OBSE_0002", strDE_OBSE_0002, _
                                             "ISCO_EMPR_TRFR", strCO_EMPR_TRFR, _
                                             "ISCO_UNID_TRFR", strCO_UNID_TRFR, _
                                             "ISCO_ALMA_TRFR", strCO_ALMA_TRFR, _
                                             "ISCO_EMPR_DEST", strCO_EMPR_DEST, _
                                             "ISCO_UNID_DEST", strCO_UNID_DEST, _
                                             "ISCO_ALMA_DEST", strCO_ALMA_DEST, _
                                             "ISTI_DOCU_TRFR", strTI_DOCU_TRFR, _
                                             "ISTI_OPER_TRFR", strTI_OPER_TRFR, _
                                             "ISNU_SERI_TRFR", strNU_SERI_TRFR, _
                                             "ISNU_DOCU_TRFR", strNU_DOCU_TRFR, _
                                             "ISST_TRAN", strST_TRAN, _
                                             "ISCO_CPTO_GURR", strCO_CPTO_GURR, _
                                             "ISCO_COND", strCO_COND, _
                                             "ISDE_CPTO", strDE_CPTO, _
                                             "OSNU_DOCU", numNU_DOCU}

            dtCod = Me._objConexion.ObtenerDataSet("USP_LOG_REGISTRA_TRANSFERENCIAS", objparametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Registra_Transferencia_Tela(ByVal strCo_Empr As String, _
                                           ByVal strCo_Unid As String, _
                                           ByVal strCo_Alma As String, _
                                           ByVal strTi_docu As String, _
                                           ByVal strNu_Docu As String, _
                                           ByVal strTi_Oper As String, _
                                           ByVal strCo_Dest_Fina As String, _
                                           ByVal strNu_Serie_Docu As String, _
                                           ByVal dtFe_Docu As String, _
                                           ByVal strTi_Auxi As String, _
                                           ByVal strCo_Auxi As String, _
                                           ByVal strOrde_Serv As String, _
                                           ByVal strNo_Pers As String, _
                                           ByVal strNu_Ruc As String, _
                                           ByVal strTi_Dire As String, _
                                           ByVal strDe_Dire As String, _
                                           ByVal strDe_Cond As String, _
                                           ByVal strCo_Tran As String, _
                                           ByVal strDe_Nomb_Tran As String, _
                                           ByVal strDe_Dire_Tran As String, _
                                           ByVal strDe_Ruc_Tran As String, _
                                           ByVal strNum_Placa As String, _
                                           ByVal strNu_Orco As String, _
                                           ByVal strNu_Guia_Prov As String, _
                                           ByVal strNu_Orpr As String, _
                                           ByVal strNu_PEPR As String, _
                                           ByVal strNu_DEPR As String, _
                                           ByVal strNu_PAPR As String, _
                                           ByVal strNu_Reqi As String, _
                                           ByVal strNu_Ortr As String, _
                                           ByVal strNu_PEDI As String, _
                                           ByVal strTI_DOCU_REFE As String, _
                                           ByVal strNU_DOCU_REFE As String, _
                                           ByVal dtFE_DOCU_REFE As String, _
                                           ByVal strDE_OBSE_0001 As String, _
                                           ByVal strDE_OBSE_0002 As String, _
                                           ByVal strCO_EMPR_TRFR As String, _
                                           ByVal strCO_UNID_TRFR As String, _
                                           ByVal strCO_ALMA_TRFR As String, _
                                           ByVal strCO_EMPR_DEST As String, _
                                           ByVal strCO_UNID_DEST As String, _
                                           ByVal strCO_ALMA_DEST As String, _
                                           ByVal strTI_DOCU_TRFR As String, _
                                           ByVal strTI_OPER_TRFR As String, _
                                           ByVal strNU_SERI_TRFR As String, _
                                           ByVal strNU_DOCU_TRFR As String, _
                                           ByVal strST_TRAN As String, _
                                           ByVal strCO_CPTO_GURR As String, _
                                           ByVal strCO_COND As String, _
                                           ByVal strDE_CPTO As String, _
                                           ByVal numNU_DOCU As String, _
                                           ByVal strUsuario As String, _
                                           ByRef dtCod As DataSet) As Boolean
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"ISCO_EMPR", strCo_Empr, _
                                             "ISCO_UNID", strCo_Unid, _
                                             "ISCO_ALMA", strCo_Alma, _
                                             "ISTI_DOCU", strTi_docu, _
                                             "ISNU_DOCU", strNu_Docu, _
                                             "ISTI_OPER", strTi_Oper, _
                                             "ISCO_DEST_FINA", strCo_Dest_Fina, _
                                             "ISNU_SERI_DOCU", strNu_Serie_Docu, _
                                             "IDFE_DOCU", dtFe_Docu, _
                                             "ISTI_AUXI_EMPR", strTi_Auxi, _
                                             "ISCO_AUXI_EMPR", strCo_Auxi, _
                                             "ISCO_ORDE_SERV", strOrde_Serv, _
                                             "ISNO_PERS", strNo_Pers, _
                                             "ISNU_RUCS", strNu_Ruc, _
                                             "ISTI_DIRE", strTi_Dire, _
                                             "ISDE_DIRE", strDe_Dire, _
                                             "ISDE_COND", strDe_Cond, _
                                             "ISCO_TRAN", strCo_Tran, _
                                             "ISDE_NOMB_TRAN", strDe_Nomb_Tran, _
                                             "ISDE_DIRE_TRAN", strDe_Dire_Tran, _
                                             "ISNU_RUCS_TRAN", strDe_Ruc_Tran, _
                                             "ISNU_PLAC", strNum_Placa, _
                                             "ISNU_ORCO", strNu_Orco, _
                                             "ISNU_GUIA_PROV", strNu_Guia_Prov, _
                                             "ISNU_ORPR", strNu_Orpr, _
                                             "ISNU_PEPR", strNu_PEPR, _
                                             "ISNU_DEPR", strNu_DEPR, _
                                             "ISNU_PAPR", strNu_PAPR, _
                                             "ISNU_REQI", strNu_Reqi, _
                                             "ISNU_ORTR", strNu_Ortr, _
                                             "ISNU_PEDI", strNu_PEDI, _
                                             "ISTI_DOCU_REFE", strTI_DOCU_REFE, _
                                             "ISNU_DOCU_REFE", strNU_DOCU_REFE, _
                                             "IDFE_DOCU_REFE", dtFE_DOCU_REFE, _
                                             "ISDE_OBSE_0001", strDE_OBSE_0001, _
                                             "ISDE_OBSE_0002", strDE_OBSE_0002, _
                                             "ISCO_EMPR_TRFR", strCO_EMPR_TRFR, _
                                             "ISCO_UNID_TRFR", strCO_UNID_TRFR, _
                                             "ISCO_ALMA_TRFR", strCO_ALMA_TRFR, _
                                             "ISCO_EMPR_DEST", strCO_EMPR_DEST, _
                                             "ISCO_UNID_DEST", strCO_UNID_DEST, _
                                             "ISCO_ALMA_DEST", strCO_ALMA_DEST, _
                                             "ISTI_DOCU_TRFR", strTI_DOCU_TRFR, _
                                             "ISTI_OPER_TRFR", strTI_OPER_TRFR, _
                                             "ISNU_SERI_TRFR", strNU_SERI_TRFR, _
                                             "ISNU_DOCU_TRFR", strNU_DOCU_TRFR, _
                                             "ISST_TRAN", strST_TRAN, _
                                             "ISCO_CPTO_GURR", strCO_CPTO_GURR, _
                                             "ISCO_COND", strCO_COND, _
                                             "ISDE_CPTO", strDE_CPTO, _
                                             "vch_Usuario", strUsuario, _
                                             "OSNU_DOCU", numNU_DOCU}

            dtCod = Me._objConexion.ObtenerDataSet("USP_LOG_REGISTRA_TRANSFERENCIAS_TELA", objparametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' OBSOLETO : Remplazado por Registra_Transferencia_Detalle_Ubicacion
    ''' (20151022 - LUIS_AJ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Registra_Transferencia_Detalle( _
                                           ByVal strCO_EMPR As String, _
                                           ByVal strCO_UNID As String, _
                                           ByVal strCo_Alma As String, _
                                           ByVal strTi_docu As String, _
                                           ByVal strNu_Docu As String, _
                                           ByVal strNU_SECU As String, _
                                           ByVal strTI_MOVI As String, _
                                           ByVal strCO_ITEM As String, _
                                           ByVal strCO_UNME_MOVI As String, _
                                           ByVal strCA_DOCU_MOVI As String, _
                                           ByVal strCO_UNME As String, _
                                           ByVal strCA_DOCU_ALMA As Double, _
                                           ByVal strCO_DEST_FINA As String, _
                                           ByVal strCO_EMPR_DEST As String, _
                                           ByVal strCO_UNID_DEST As String, _
                                           ByVal strCO_ALMA_DEST As String, _
                                           ByVal strCA_DOCU_DEST As Double, _
                                           ByVal strCO_DEFE As String, _
                                           ByVal strCA_DEFE As Double, _
                                           ByVal strDE_OBSE_0001 As String, _
                                           ByVal strDE_OBSE_0002 As String, _
                                           ByVal strCO_MONE As String, _
                                           ByVal strFA_CAMB As Double, _
                                           ByVal strFA_CONV_MONE As Double, _
                                           ByVal strCO_MONE_ALMA As String, _
                                           ByVal strCO_MONE_NACI As String, _
                                           ByVal strTI_AUXI_EMPR As String, _
                                           ByVal strCO_AUXI_EMPR As String, _
                                           ByVal strCO_ORDE_SERV As String, _
                                           ByVal strCO_LOTE As String, _
                                           ByVal strTI_MOVI_TRFR As String, _
                                           ByVal strCO_EMPR_TRFR As String, _
                                           ByVal strCO_UNID_TRFR As String, _
                                           ByVal strCO_ALMA_TRFR As String, _
                                           ByVal strTI_DOCU_TRFR As String, _
                                           ByVal strNU_DOCU_TRFR As String, _
                                           ByVal strFA_CONV As Double, _
                                           ByVal strST_UBIC As String, _
                                           ByVal strST_UBIC_TRFR As String, _
                                           ByVal strST_TRAN As String, _
                                           ByVal strNU_ORCO As String, _
                                           ByVal strNU_SECU_ORCO As String, _
                                           ByVal strNU_REQI As String, _
                                           ByVal strNU_SECU_REQI As String, _
                                           ByVal strNU_ORPR As String, _
                                           ByVal strCO_PROC As String, _
                                           ByVal strNU_SECU_ORPR As String, _
                                           ByVal strNU_PEPR As String, _
                                           ByVal strNU_SECU_PEPR As Double, _
                                           ByVal strNU_DEPR As String, _
                                           ByVal numNU_SECU_DEPR As Double, _
                                           ByVal strNU_PAPR As String, _
                                           ByVal numNU_SECU_PAPR As Double, _
                                           ByVal strTI_DOCU_REFE As String, _
                                           ByVal strNU_DOCU_REFE As String, _
                                           ByVal numINNU_SECU_CLIE As Double) As Boolean
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"ISCO_EMPR", strCO_EMPR, _
                                             "ISCO_UNID", strCO_UNID, _
                                             "ISCO_ALMA", strCo_Alma, _
                                             "ISTI_DOCU", strTi_docu, _
                                             "ISNU_DOCU", strNu_Docu, _
                                             "INNU_SECU", strNU_SECU, _
                                             "ISTI_MOVI", strTI_MOVI, _
                                             "ISCO_ITEM", strCO_ITEM, _
                                             "ISCO_UNME_MOVI", strCO_UNME_MOVI, _
                                             "INCA_DOCU_MOVI", strCA_DOCU_MOVI, _
                                             "ISCO_UNME", strCO_UNME, _
                                             "INCA_DOCU_ALMA", strCA_DOCU_ALMA, _
                                             "ISCO_DEST_FINA", strCO_DEST_FINA, _
                                             "ISCO_EMPR_DEST", strCO_EMPR_DEST, _
                                             "ISCO_UNID_DEST", strCO_UNID_DEST, _
                                             "ISCO_ALMA_DEST", strCO_ALMA_DEST, _
                                             "INCA_DOCU_DEST", strCA_DOCU_DEST, _
                                             "ISCO_DEFE", strCO_DEFE, _
                                             "INCA_DEFE", strCA_DEFE, _
                                             "ISDE_OBSE_0001", strDE_OBSE_0001, _
                                             "ISDE_OBSE_0002", strDE_OBSE_0002, _
                                             "ISCO_MONE", strCO_MONE, _
                                             "INFA_CAMB", strFA_CAMB, _
                                             "INFA_CONV_MONE", strFA_CONV_MONE, _
                                             "ISCO_MONE_ALMA", strCO_MONE_ALMA, _
                                             "ISCO_MONE_NACI", strCO_MONE_NACI, _
                                             "ISTI_AUXI_EMPR", strTI_AUXI_EMPR, _
                                             "ISCO_AUXI_EMPR", strCO_AUXI_EMPR, _
                                             "ISCO_ORDE_SERV", strCO_ORDE_SERV, _
                                             "ISCO_LOTE", strCO_LOTE, _
                                             "ISTI_MOVI_TRFR", strTI_MOVI_TRFR, _
                                             "ISCO_EMPR_TRFR", strCO_EMPR_TRFR, _
                                             "ISCO_UNID_TRFR", strCO_UNID_TRFR, _
                                             "ISCO_ALMA_TRFR", strCO_ALMA_TRFR, _
                                             "ISTI_DOCU_TRFR", strTI_DOCU_TRFR, _
                                             "ISNU_DOCU_TRFR", strNU_DOCU_TRFR, _
                                             "INFA_CONV", strFA_CONV, _
                                             "ISST_UBIC", strST_UBIC, _
                                             "ISST_UBIC_TRFR", strST_UBIC_TRFR, _
                                             "ISST_TRAN", strST_TRAN, _
                                             "ISNU_ORCO", strNU_ORCO, _
                                             "INNU_SECU_ORCO", strNU_SECU_ORCO, _
                                             "ISNU_REQI", strNU_REQI, _
                                             "INNU_SECU_REQI", strNU_SECU_REQI, _
                                             "ISNU_ORPR", strNU_ORPR, _
                                             "ISCO_PROC", strCO_PROC, _
                                             "INNU_SECU_ORPR", strNU_SECU_ORPR, _
                                             "ISNU_PEPR", strNU_PEPR, _
                                             "INNU_SECU_PEPR", strNU_SECU_PEPR, _
                                             "ISNU_DEPR", strNU_DEPR, _
                                             "INNU_SECU_DEPR", numNU_SECU_DEPR, _
                                             "ISNU_PAPR", strNU_PAPR, _
                                             "INNU_SECU_PAPR", numNU_SECU_PAPR, _
                                             "ISTI_DOCU_REFE", strTI_DOCU_REFE, _
                                             "ISNU_DOCU_REFE", strNU_DOCU_REFE, _
                                             "INNU_SECU_CLIE", numINNU_SECU_CLIE}

            Me._objConexion.EjecutarComando("USP_LOG_REGISTRA_TRANSFERENCIAS_DETALLE", objparametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Registra_Transferencia_Detalle_Ubicacion( _
                                           ByVal strCO_EMPR As String, _
                                           ByVal strCO_UNID As String, _
                                           ByVal strCo_Alma As String, _
                                           ByVal strTi_docu As String, _
                                           ByVal strNu_Docu As String, _
                                           ByVal strNU_SECU As String, _
                                           ByVal strTI_MOVI As String, _
                                           ByVal strCO_ITEM As String, _
                                           ByVal strCO_UNME_MOVI As String, _
                                           ByVal strCA_DOCU_MOVI As String, _
                                           ByVal strCO_UNME As String, _
                                           ByVal strCA_DOCU_ALMA As Double, _
                                           ByVal strCO_DEST_FINA As String, _
                                           ByVal strCO_EMPR_DEST As String, _
                                           ByVal strCO_UNID_DEST As String, _
                                           ByVal strCO_ALMA_DEST As String, _
                                           ByVal strCA_DOCU_DEST As Double, _
                                           ByVal strCO_DEFE As String, _
                                           ByVal strCA_DEFE As Double, _
                                           ByVal strDE_OBSE_0001 As String, _
                                           ByVal strDE_OBSE_0002 As String, _
                                           ByVal strCO_MONE As String, _
                                           ByVal strFA_CAMB As Double, _
                                           ByVal strFA_CONV_MONE As Double, _
                                           ByVal strCO_MONE_ALMA As String, _
                                           ByVal strCO_MONE_NACI As String, _
                                           ByVal strTI_AUXI_EMPR As String, _
                                           ByVal strCO_AUXI_EMPR As String, _
                                           ByVal strCO_ORDE_SERV As String, _
                                           ByVal strCO_LOTE As String, _
                                           ByVal strTI_MOVI_TRFR As String, _
                                           ByVal strCO_EMPR_TRFR As String, _
                                           ByVal strCO_UNID_TRFR As String, _
                                           ByVal strCO_ALMA_TRFR As String, _
                                           ByVal strTI_DOCU_TRFR As String, _
                                           ByVal strNU_DOCU_TRFR As String, _
                                           ByVal strFA_CONV As Double, _
                                           ByVal strST_UBIC As String, _
                                           ByVal strST_UBIC_TRFR As String, _
                                           ByVal strST_TRAN As String, _
                                           ByVal strNU_ORCO As String, _
                                           ByVal strNU_SECU_ORCO As String, _
                                           ByVal strNU_REQI As String, _
                                           ByVal strNU_SECU_REQI As String, _
                                           ByVal strNU_ORPR As String, _
                                           ByVal strCO_PROC As String, _
                                           ByVal strNU_SECU_ORPR As String, _
                                           ByVal strNU_PEPR As String, _
                                           ByVal strNU_SECU_PEPR As Double, _
                                           ByVal strNU_DEPR As String, _
                                           ByVal numNU_SECU_DEPR As Double, _
                                           ByVal strNU_PAPR As String, _
                                           ByVal numNU_SECU_PAPR As Double, _
                                           ByVal strTI_DOCU_REFE As String, _
                                           ByVal strNU_DOCU_REFE As String, _
                                           ByVal numINNU_SECU_CLIE As Double, _
                                           ByVal strCodigoUbicacion As String, _
                                           ByVal strUsuario As String) As Boolean
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objparametros() As Object = {"ISCO_EMPR", strCO_EMPR, _
                                             "ISCO_UNID", strCO_UNID, _
                                             "ISCO_ALMA", strCo_Alma, _
                                             "ISTI_DOCU", strTi_docu, _
                                             "ISNU_DOCU", strNu_Docu, _
                                             "INNU_SECU", strNU_SECU, _
                                             "ISTI_MOVI", strTI_MOVI, _
                                             "ISCO_ITEM", strCO_ITEM, _
                                             "ISCO_UNME_MOVI", strCO_UNME_MOVI, _
                                             "INCA_DOCU_MOVI", strCA_DOCU_MOVI, _
                                             "ISCO_UNME", strCO_UNME, _
                                             "INCA_DOCU_ALMA", strCA_DOCU_ALMA, _
                                             "ISCO_DEST_FINA", strCO_DEST_FINA, _
                                             "ISCO_EMPR_DEST", strCO_EMPR_DEST, _
                                             "ISCO_UNID_DEST", strCO_UNID_DEST, _
                                             "ISCO_ALMA_DEST", strCO_ALMA_DEST, _
                                             "INCA_DOCU_DEST", strCA_DOCU_DEST, _
                                             "ISCO_DEFE", strCO_DEFE, _
                                             "INCA_DEFE", strCA_DEFE, _
                                             "ISDE_OBSE_0001", strDE_OBSE_0001, _
                                             "ISDE_OBSE_0002", strDE_OBSE_0002, _
                                             "ISCO_MONE", strCO_MONE, _
                                             "INFA_CAMB", strFA_CAMB, _
                                             "INFA_CONV_MONE", strFA_CONV_MONE, _
                                             "ISCO_MONE_ALMA", strCO_MONE_ALMA, _
                                             "ISCO_MONE_NACI", strCO_MONE_NACI, _
                                             "ISTI_AUXI_EMPR", strTI_AUXI_EMPR, _
                                             "ISCO_AUXI_EMPR", strCO_AUXI_EMPR, _
                                             "ISCO_ORDE_SERV", strCO_ORDE_SERV, _
                                             "ISCO_LOTE", strCO_LOTE, _
                                             "ISTI_MOVI_TRFR", strTI_MOVI_TRFR, _
                                             "ISCO_EMPR_TRFR", strCO_EMPR_TRFR, _
                                             "ISCO_UNID_TRFR", strCO_UNID_TRFR, _
                                             "ISCO_ALMA_TRFR", strCO_ALMA_TRFR, _
                                             "ISTI_DOCU_TRFR", strTI_DOCU_TRFR, _
                                             "ISNU_DOCU_TRFR", strNU_DOCU_TRFR, _
                                             "INFA_CONV", strFA_CONV, _
                                             "ISST_UBIC", strST_UBIC, _
                                             "ISST_UBIC_TRFR", strST_UBIC_TRFR, _
                                             "ISST_TRAN", strST_TRAN, _
                                             "ISNU_ORCO", strNU_ORCO, _
                                             "INNU_SECU_ORCO", strNU_SECU_ORCO, _
                                             "ISNU_REQI", strNU_REQI, _
                                             "INNU_SECU_REQI", strNU_SECU_REQI, _
                                             "ISNU_ORPR", strNU_ORPR, _
                                             "ISCO_PROC", strCO_PROC, _
                                             "INNU_SECU_ORPR", strNU_SECU_ORPR, _
                                             "ISNU_PEPR", strNU_PEPR, _
                                             "INNU_SECU_PEPR", strNU_SECU_PEPR, _
                                             "ISNU_DEPR", strNU_DEPR, _
                                             "INNU_SECU_DEPR", numNU_SECU_DEPR, _
                                             "ISNU_PAPR", strNU_PAPR, _
                                             "INNU_SECU_PAPR", numNU_SECU_PAPR, _
                                             "ISTI_DOCU_REFE", strTI_DOCU_REFE, _
                                             "ISNU_DOCU_REFE", strNU_DOCU_REFE, _
                                             "INNU_SECU_CLIE", numINNU_SECU_CLIE, _
                                             "vch_CodigoUbicacion", strCodigoUbicacion, _
                                             "vch_Usuario", strUsuario}

            Me._objConexion.EjecutarComando("USP_LOG_REGISTRA_TRANSFERENCIAS_DETALLE_UBICACION", objparametros)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class
