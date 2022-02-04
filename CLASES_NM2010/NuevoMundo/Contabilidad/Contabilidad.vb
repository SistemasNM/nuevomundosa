Imports NM.AccesoDatos

Public Class Contabilidad

#Region "-- Variables --"

    Private _strUsuario As String
    Private _intAnno As Int16
    Private _intMes As Int16
    Private _objConexion As AccesoDatosSQLServer

#End Region

#Region "-- Propiedades --"

  Public Property Usuario() As String
    Get
      Return _strUsuario
    End Get
    Set(ByVal Value As String)
      _strUsuario = Value
    End Set
  End Property

  Public Property Anno() As Int16
    Get
      Return _intAnno
    End Get
    Set(ByVal Value As Int16)
      _intAnno = Value
    End Set
  End Property

  Public Property Mes() As Int16
    Get
      Return _intMes
    End Get
    Set(ByVal Value As Int16)
      _intMes = Value
    End Set
  End Property

#End Region

#Region "-- Constructores --"
  Sub New()

  End Sub
#End Region


#Region "-- Metodos --"

  Public Function ProformasProveedores_Corregir() As DataTable
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
      Dim objParametros() As Object = {"NU_ANNO", _intAnno, "NU_MESE", _intMes}
      Return _objConexion.ObtenerDataTable("NM_PROFORMAS_PROVEEDORES", objParametros)
    Catch ex As Exception
      Throw ex
    End Try
  End Function

    Public Function fnc_generarpivotcontatesoproveedores(ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByVal pstr_contcuentainicio As String, ByVal pstr_contcuentafinal As String, ByVal pbln_noconsiderar999 As Boolean, ByVal pbln_noconsiderarsaldosiguales As Boolean, ByVal pint_cantmesesxvencer As Int16, ByVal pstr_tesofechafinal As String, ByVal pbln_excepcionarcdr As Boolean) As Boolean
        Dim lbln_resultado As Boolean = False
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim lobj_parametros() As Object = {"ptin_contmes", pint_contmes, "pint_contano", pint_contano, "pvch_contcuentainicio", pstr_contcuentainicio, "pstr_contcuentafinal", pstr_contcuentafinal, "ptin_noconsiderar999", IIf(pbln_noconsiderar999, 1, 0), "ptin_noconsiderarsaldosiguales", IIf(pbln_noconsiderarsaldosiguales, 1, 0), "ptin_cantmesesxvencer", pint_cantmesesxvencer, "pvch_tesofechafinal", pstr_tesofechafinal, "ptin_excepcionarcdr", IIf(pbln_excepcionarcdr, 1, 0)}

            _objConexion.EjecutarComando("usp_tes_contatesoproveedores", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

    Public Function fnc_existencias_procesar(ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByVal pint_procmesanterior As Int16) As Boolean
        Dim lbln_resultado As Boolean = False
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobj_parametros() As Object = {"pint_anno", pint_contano, _
                                                "pint_mes", pint_contmes, _
                                                "ptin_procmesant", pint_procmesanterior}

            _objConexion.EjecutarComando("usp_con_existencias_proceso", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

    Public Function fnc_existencias_listar(ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByRef pdtbdatos As DataTable) As Boolean
        Dim lbln_resultado As Boolean = False
        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lobj_parametros() As Object = {"ptin_tipolista", "1", _
                                                "pint_anno", pint_contano, _
                                                "pint_mes", pint_contmes}

            pdtbdatos = _objConexion.ObtenerDataTable("utb_conta_existencias1_lista", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

    Public Function fnc_tipocambio_listar(ByVal pint_tipolista As Int16, ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByRef pdtbdatos As DataTable) As Boolean
        Dim lbln_resultado As Boolean = False

        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            Dim lobj_parametros() As Object = {"ptin_tipolista", pint_tipolista, _
                                                "pint_anno", pint_contano, _
                                                "ptin_mes", pint_contmes}

            pdtbdatos = _objConexion.ObtenerDataTable("usp_seg_tipocambio_listar", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function

    Public Function fnc_pdbexportador_listar(ByVal pint_tipolista As Int16, ByVal pint_contmes As Int16, ByVal pint_contano As Integer, ByRef pdtbdatos As DataTable) As Boolean
        Dim lbln_resultado As Boolean = False

        Try

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobj_parametros() As Object = {"ptin_tipolista", pint_tipolista, _
                                                "pint_anno", pint_contano, _
                                                "ptin_mes", pint_contmes}

            pdtbdatos = _objConexion.ObtenerDataTable("usp_con_pdbexportador_listar", lobj_parametros)
            lbln_resultado = True
            Return lbln_resultado
        Catch ex As Exception
            Throw ex
            lbln_resultado = False
        Finally
        End Try
        Return lbln_resultado
    End Function
    Public Overloads Function ObtenerPeriodos() As DataTable

        Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
        Return adSQL.ObtenerDataTable("usp_con_CierreContable_listar")

    End Function
    Public Function InsertarPeriodo(ByVal pstrEmpresa As String, ByVal pintAnno As Integer, _
                                   ByVal pintMes As Integer, ByVal pstrEstado As String, _
                                   ByVal pstrUsuario As String) As String

        Dim lstrError As String = "", ldtbResultado As DataTable

        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = { _
            "CO_EMPR", pstrEmpresa, _
            "NU_ANNO", pintAnno, _
            "NU_MESE", pintMes, _
            "CO_ESTA", pstrEstado, _
            "CO_USUA", pstrUsuario}

            ldtbResultado = _objConexion.ObtenerDataTable("usp_con_CierreContable_Insertar", lobjParametros)

        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    Public Function ActualizarPeriodo(ByVal pstrEmpresa As String, ByVal pintAnno As Integer, _
                                    ByVal pintMes As Integer, ByVal pstrEstado As String, _
                                    ByVal pstrUsuario As String) As String

        Dim lstrError As String = "", ldtbResultado As DataTable

        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = { _
            "CO_EMPR", pstrEmpresa, _
            "NU_ANNO", pintAnno, _
            "NU_MESE", pintMes, _
            "CO_ESTA", pstrEstado, _
            "CO_USUA", pstrUsuario}

            ldtbResultado = _objConexion.ObtenerDataTable("usp_con_CierreContable_Actualizar", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function
    Public Function EliminarPeriodo(ByVal pstrEmpresa As String, ByVal pintAnno As Integer, _
                                    ByVal pintMes As Integer) As String

        Dim lstrError As String = "", ldtbResultado As DataTable

        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = { _
            "CO_EMPR", pstrEmpresa, _
            "NU_ANNO", pintAnno, _
            "NU_MESE", pintMes}

            ldtbResultado = _objConexion.ObtenerDataTable("usp_con_CierreContable_Eliminar", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        Finally
            ldtbResultado = Nothing
        End Try
        Return lstrError
    End Function

    'CAMBIO DG - INI
    Public Function ListarNumeroContabilidad(ByVal codigo As Integer) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_LISTAR_CONTABILIDAD_VOUCHER", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarCodigoOperacion(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_LISTAR_CODIGO_OPERACION", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarCodigoCuenta(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_CUENTA_EMPRESA", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarTipoAuxiliar(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_AUXILIAR_EMPRESA", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarCodigoAuxiliar(ByVal codigo As String, ByVal dato As String, ByVal dato2 As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato, "TI_AUXI", dato2}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_COD_AUXILIAR", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarCodigoDocumento(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_CODIGO_DOCUMENTO", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarTipoCargoAbono(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_CARGA_ABONO", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarTipoMoneda(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_MONEDA", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarCodigoOS(ByVal codigo As String, ByVal dato As String) As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"NU_CONT", codigo, "DATO", dato}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_COD_OS", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ListarTipoCambio() As DataTable
        Dim lstrError As String = "", ldtbResultado As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {}

            ldtbResultado = _objConexion.ObtenerDataTable("USP_OBTENER_TIPO_CAMBIO", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function ObtenerUltimoCorrelativo(ByVal oper As String, ByVal anio As Integer, ByVal mes As Integer) As String
        Dim lstrError As String = "", lstrCorr As String
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"OPER", oper, "ANIO", anio, "MES", mes}

            lstrCorr = _objConexion.ObtenerDataTable("SP_OBTENER_ULTIMO_CORRELATIVO", lobjParametros).Rows(0).Item("RESULT").ToString()
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrCorr
    End Function
    Public Function CagarCuentaDatos(ByVal cuenta As String) As DataSet
        Dim lstrError As String = "", ldtbResultado As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"CUENTA", cuenta}

            ldtbResultado = _objConexion.ObtenerDataSet("USP_OBTENER_DATOS_CUENTA", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return ldtbResultado
    End Function
    Public Function CargarValorTipoCambio(ByVal TipoCambio As String) As Decimal
        Dim lstrError As String = "", lstrCorr As Decimal
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"DATO", TipoCambio}

            lstrCorr = Decimal.Parse(_objConexion.ObtenerDataTable("USP_OBTENER_VALOR_TIPO_CAMBIO", lobjParametros).Rows(0).Item("TI_CAM").ToString())
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lstrCorr
    End Function
    Public Function ObtenerDatosVoucher(ByVal codOpe As String, ByVal anio As Integer, ByVal mes As Integer, ByVal voucher As String) As DataSet
        Dim lstrError As String = "", dts As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"COD_OPERA", codOpe, "ANIO", anio, "MES", mes, "VOUCHER", voucher}

            dts = _objConexion.ObtenerDataSet("SP_OBTENER_DATOS_VOUCHER", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return dts
    End Function
    Public Function ObtenerVoucherSecuencia(ByVal codOpe As String, ByVal voucher As String, ByVal anio As Integer, ByVal mes As Integer, ByVal secu As Integer) As DataSet
        Dim lstrError As String = "", dts As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"COD_OPERA", codOpe, "VOUCHER", voucher, "ANIO", anio, "MES", mes, "SECU", secu}

            dts = _objConexion.ObtenerDataSet("USP_OBTENER_VOUCHER_POR_SECU", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return dts
    End Function
    Public Function ObtenerMonedaContabilidad(ByVal strEmpresa As String, ByVal strMoneda As String, ByVal intcontEm As Integer, ByVal strTipoCambio As String, ByVal decFactorCam As Decimal, ByVal strFecha As String, ByVal decImporte As Decimal, ByVal decSalida As Decimal) As Decimal
        Dim lstrError As String = "", resul As Decimal
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"ISCO_EMPR", strEmpresa, "ISCO_MONE_ORIG", strMoneda, "INNU_CNTB_EMPR", intcontEm, "ISTI_CAMB", strTipoCambio, _
                                              "INFA_CAMB", decFactorCam, "IDFE_CAMB", strFecha, "INIM_ORIG", decImporte}

            resul = Decimal.Parse(_objConexion.ObtenerDataTable("USP_OBTENER_MONEDA_CONTABILIDAD", lobjParametros).Rows(0).Item("RESULT").ToString())
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return resul
    End Function
    Public Function GrabarVoucher(ByVal strEmpresa As String, strCoUnid As String, ByVal intcontEm As Integer, ByVal strOpera As String, ByVal intAnio As String, ByVal intPeriodo As String, ByVal strVoucher As String, _
                                  ByVal intSecu As Integer, ByVal strFeDocu As String, ByVal strCuenta As String, ByVal strGene As String, ByVal strAuxiliar As String, ByVal strCodAuxiliar As String, ByVal strTipDocu As String, _
                                  ByVal strDocu As String, ByVal strFeDocuEmi As String, ByVal strAbono As String, ByVal strMoneda As String, ByVal strTipCam As String, ByVal douFacCam As Double, ByVal douImpOri As Double, _
                                  ByVal strOS As String, ByVal strGlosa As String, ByVal strTipDocRef As String, ByVal strDocRefe As String, ByVal strNoAuxi As String, ByVal strFePro As String, ByVal strFeChe As String, _
                                  ByVal strMovCon As String, ByVal strFecVencDoc As String, ByVal strFeDocuRefe As String, ByVal douMonRet As Double, ByVal douMonCont As Double, ByVal strTipDocDet As String, ByVal strDocDet As String, ByVal srtFecDocuRet As String, ByVal strUsuario As String) As DataTable
        Dim lstrError As String = "", dts As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"ISCO_EMPR", strEmpresa, "ISCO_UNID_CNTB", strCoUnid, "INNU_CNTB_EMPR", intcontEm, "ISCO_OPRC_CNTB", strOpera, "INNU_ANNO", intAnio, "INNU_MESE", intPeriodo, _
                                              "ISNU_ASTO", strVoucher, "INNU_SECU", intSecu, "IDFE_ASTO_CNTB", strFeDocu, "ISCO_CNTA_EMPR", strCuenta, "ISST_GENE", strGene, "ISTI_AUXI_EMPR", strAuxiliar, "ISCO_AUXI_EMPR", strCodAuxiliar, _
                                              "ISTI_DOCU", strTipDocu, "ISNU_DOCU", strDocu, "IDFE_DOCU", strFeDocuEmi, "ISTI_OPER", strAbono, "ISCO_MONE", strMoneda, "ISTI_CAMB", strTipCam, "INFA_CAMB", douFacCam, _
                                              "INIM_MVTO_ORIG", douImpOri, "ISCO_ORDE_SERV", strOS, "ISDE_GLOS", strGlosa, "ISTI_DOCU_REFE", strTipDocRef, "ISNU_DOCU_REFE", strDocRefe, "ISNO_GIRA", strNoAuxi, "IDFE_PROG_CHEQ", strFePro, _
                                              "IDFE_ENTR_CHEQ", strFeChe, "ISSI_MVTO_CNTB", strMovCon, "IDFE_DOCU_VENC", strFecVencDoc, "IDFE_DOCU_REFE", strFeDocuRefe, "INIM_MVTO_RETE", douMonRet, "INIM_MVTO_CNTB", douMonCont, "ISTI_DOCU_DETR", strTipDocDet, _
                                              "ISNU_DOCU_DETR", strDocDet, "IDFE_DOCU_DETR", srtFecDocuRet, "ISCO_USUA", strUsuario}
            dts = _objConexion.ObtenerDataTable("SP_TXMVTO_CNTB_I01_VOUCHER", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return dts
    End Function
    Public Function ModificarVoucher(ByVal strEmpresa As String, ByVal intcontEm As Integer, strCoUnid As String, ByVal strOpera As String, ByVal intAnio As String, ByVal intPeriodo As String, ByVal strVoucher As String, _
                                      ByVal intSecu As Integer, ByVal strFeDocu As String, ByVal strCuenta As String, ByVal strAuxiliar As String, ByVal strCodAuxiliar As String, ByVal strTipDocu As String, _
                                      ByVal strDocu As String, ByVal strFeDocuEmi As String, ByVal strFecVencDoc As String, ByVal strAbono As String, ByVal strMoneda As String, ByVal strTipCam As String, ByVal douFacCam As Double, _
                                      ByVal douImpOri As Double, ByVal strOS As String, ByVal strGlosa As String, ByVal strTipDocRef As String, ByVal strDocRefe As String, ByVal strFeDocuRefe As String, ByVal strNoAuxi As String, _
                                      ByVal douMonRet As Double, ByVal douMonCont As Double, ByVal strTipDocDet As String, ByVal strDocDet As String, ByVal srtFecDocuRet As String, ByVal strUsuario As String) As Boolean
        Dim lstrError As String = "", dts As DataTable, lbln_resultado As Boolean = False
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"ISCO_EMPR", strEmpresa, "INNU_CNTB_EMPR", intcontEm, "ISCO_UNID_CNTB", strCoUnid, "ISCO_OPRC_CNTB", strOpera, "INNU_ANNO", intAnio, "INNU_MESE", intPeriodo, _
                                             "ISNU_ASTO", strVoucher, "INNU_SECU", intSecu, "IDFE_ASTO_CNTB", strFeDocu, "ISCO_CNTA_EMPR", strCuenta, "ISTI_AUXI_EMPR", strAuxiliar, "ISCO_AUXI_EMPR", strCodAuxiliar, _
                                             "ISTI_DOCU", strTipDocu, "ISNU_DOCU", strDocu, "IDFE_DOCU", strFeDocuEmi, "IDFE_DOCU_VENC", strFecVencDoc, "ISTI_OPER", strAbono, "ISCO_MONE", strMoneda, "ISTI_CAMB", strTipCam, "INFA_CAMB", douFacCam, _
                                             "INIM_MVTO_ORIG", douImpOri, "ISCO_ORDE_SERV", strOS, "ISDE_GLOS", strGlosa, "ISTI_DOCU_REFE", strTipDocRef, "ISNU_DOCU_REFE", strDocRefe, "IDFE_DOCU_REFE", strFeDocuRefe, "ISNO_GIRA", strNoAuxi, _
                                             "INIM_MVTO_RETE", douMonRet, "INIM_MVTO_CNTB", douMonCont, "ISTI_DOCU_DETR", strTipDocDet, "ISNU_DOCU_DETR", strDocDet, "IDFE_DOCU_DETR", srtFecDocuRet, "ISCO_USUA", strUsuario}
            'dts = _objConexion.ObtenerDataTable("SP_TXMVTO_CNTB_U01_VOUCHER", lobjParametros)
            _objConexion.EjecutarComando("SP_TXMVTO_CNTB_U01_VOUCHER", lobjParametros)
            lbln_resultado = True
        Catch ex As Exception
            lbln_resultado = False
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lbln_resultado
    End Function
    Public Function EliminarVoucher(ByVal strEmpresa As String, ByVal intcontEm As Integer, strCoUnid As String, ByVal strOpera As String, ByVal intAnio As String, ByVal intPeriodo As String, ByVal strVoucher As String, _
                                     ByVal intSecu As Integer, ByVal strTodo As String) As Boolean
        Dim lstrError As String = "", dts As DataTable, lbln_resultado As Boolean = False
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"ISCO_EMPR", strEmpresa, "INNU_CNTB_EMPR", intcontEm, "ISCO_UNID_CNTB", strCoUnid, "ISCO_OPRC_CNTB", strOpera, "INNU_ANNO", intAnio, "INNU_MESE", intPeriodo, "ISNU_ASTO", strVoucher, _
                                              "INNU_SECU", intSecu, "ISST_TODO", strTodo}
            'dts = _objConexion.ObtenerDataTable("SP_TXMVTO_CNTB_D01", lobjParametros)
            _objConexion.EjecutarComando("SP_TXMVTO_CNTB_D01", lobjParametros)
            lbln_resultado = True
        Catch ex As Exception
            lbln_resultado = False
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return lbln_resultado
    End Function
    Public Function ActualizarOrdenTrabajo(ByVal strEmpresa As String, ByVal strVoucher As String, ByVal strOpera As String, ByVal intAnio As String, ByVal intPeriodo As String, ByVal intSecu As Integer, ByVal strOT As String, ByVal strUsuario As String) As DataTable
        Dim lstrError As String = "", dts As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"ISCO_EMPR", strEmpresa, "ISNU_ASTO", strVoucher, "ISCO_OPRC_CNTB", strOpera, "INNU_ANNO", intAnio, "INNU_MESE", intPeriodo, "INNU_SECU", intSecu, "INNU_OT", strOT, "ISCO_USUA", strUsuario}
            dts = _objConexion.ObtenerDataTable("USP_ACTUALIZAR_VOUCHER", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return dts
    End Function
    Public Function ObtenerDatosSecuenciVoucher(ByVal codOpe As String, ByVal anio As Integer, ByVal mes As Integer, ByVal voucher As String) As DataTable
        Dim lstrError As String = "", dt As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim lobjParametros() As Object = {"COD_OPERA", codOpe, "ANIO", anio, "MES", mes, "VOUCHER", voucher}

            dt = _objConexion.ObtenerDataTable("USP_OBTENER_DATOS_SECUENCIAS_VOUCHER", lobjParametros)
        Catch ex As Exception
            lstrError = "Error : " & Chr(13) & ex.Message
        End Try
        Return dt
    End Function
    'CAMBIO DG - FIN
    Public Function ActualizaOTVoucher(ByVal strCodigoEmpresa As String, _
                                                    ByVal strCodigoOperacion As String, _
                                                    ByVal StrAnio As String, _
                                                    ByVal strMes As String, _
                                                    ByVal strNroAsiento As String,
                                                    ByVal strOT As String,
                                                    ByVal strCoUsuaModi As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim objParametros() As Object = {"CO_EMPR", strCodigoEmpresa,
                                            "CO_OPRC", strCodigoOperacion,
                                            "NU_ANNO", StrAnio,
                                            "NU_MESE", strMes,
                                             "NU_ASTO", strNroAsiento,
                                             "NU_ORTR", strOT,
                                            "CO_USUA_MODI", strCoUsuaModi
                                            }

            Return _objConexion.EjecutarComando("USP_CON_ACTUALIZA_OT_VOUCHER", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
