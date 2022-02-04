Imports NM.AccesoDatos
Imports NM_General
Imports System.Text
Public Class Logistica
#Region "Variables"

    Private _objConnexion As AccesoDatosSQLServer
    Private _strUsuario As String

    '-- variables para nsa

    Private _strError As String = ""

    Private _strEmpresa As String
    Private _strAlmacen As String
    Private _strCCosto As String
    Private _strNroSession As String
    Private _strCodigoRollo As String
    Private _strFecha As String




#End Region
#Region "Propiedades"



    Public Property Usuario() As String
        Get
            Usuario = _strUsuario
        End Get
        Set(ByVal Value As String)
            _strUsuario = Value
        End Set
    End Property


    Public Property clsError() As String
        Get
            clsError = _strError
        End Get
        Set(ByVal Value As String)
            _strError = Value
        End Set
    End Property

    Public Property Empresa() As String
        Get
            Empresa = _strEmpresa
        End Get
        Set(ByVal Value As String)
            _strEmpresa = Value
        End Set
    End Property

    Public Property Almacen() As String
        Get
            Almacen = _strAlmacen
        End Get
        Set(ByVal Value As String)
            _strAlmacen = Value
        End Set
    End Property

    Public Property CentroCosto() As String
        Get
            CentroCosto = _strCCosto
        End Get
        Set(ByVal Value As String)
            _strCCosto = Value
        End Set
    End Property

    Public Property NroSession() As String
        Get
            NroSession = _strNroSession
        End Get
        Set(ByVal Value As String)
            _strNroSession = Value
        End Set
    End Property

    Public Property Fecha() As String
        Get
            Fecha = _strFecha
        End Get
        Set(ByVal Value As String)
            _strFecha = Value
        End Set
    End Property

    Public Property CodigoRollo() As String
        Get
            CodigoRollo = _strCodigoRollo
        End Get
        Set(ByVal Value As String)
            _strCodigoRollo = Value
        End Set
    End Property


#End Region
#Region "Contructores"
    Sub New()

    End Sub
#End Region
#Region "Metodos y funciones"

  Public Function ufn_ObtenerRolloTrazabilidad(ByVal pvch_codigo_ficha_partida As String) As DataTable
    Try
      _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
      Dim objParametros() As Object = {"pvch_codigo_ficha_partida", pvch_codigo_ficha_partida}
      Return _objConnexion.ObtenerDataSet("usp_Rollo_Trazabilidad_Obtener", objParametros).Tables(0)
    Catch ex As Exception
      _strError = ex.Message
      Throw ex
    End Try
    End Function
    '------------------------- ADD 24/11/2015 (LUIS_AJ ) ------------------------------------------------
    Public Function ufn_obtenerRollosporverificar(ByVal pvch_CodigoRollo As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"pvch_CodigoRollo", pvch_CodigoRollo}
            'Return _objConnexion.ObtenerDataTable("usp_VerificacionRollos", objParametros)
            Return _objConnexion.ObtenerDataTable("usp_Almacen_Rollos_Transito_Validar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ProcesarReetiquetado(ByVal pvch_Usuario As String, ByVal pvch_CodigoRollo As String, ByVal pvch_codigo_ficha_partida As String, ByVal pvch_codigo_Ubicacion As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", pvch_Usuario, _
                                             "pvch_CodigoRollo", pvch_CodigoRollo,
                                             "pvch_codigo_ficha_partida", pvch_codigo_ficha_partida,
                                             "pvch_codigo_Ubicacion", pvch_codigo_Ubicacion}
            Return _objConnexion.ObtenerDataTable("usp_Almacen_Reclasificacion_Grabar_ubicacion", objParametros)
            'usp_Almacen_Reclasificacion_Grabar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ObtenerComentario(ByVal pstrCodigoArticulo As String, ByVal pstrAnchoArticulo As String, _
    ByVal pstrCodigoColor As String, ByVal pstrAcabado As String, ByVal pstrCalidad As String, ByVal pstrCodigoDiseno As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
            "p_var_AnchoArticulo", pstrAnchoArticulo, "p_var_CodigoColor", pstrCodigoColor, "p_var_DescripAcabado", pstrAcabado, _
            "p_var_Calidad", pstrCalidad, "p_var_CodigoDiseno", pstrCodigoDiseno}
            Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerComentario", objParametros)
        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function ufn_ProcesarComentario(ByVal pstrCodigoArticulo As String, ByVal pstrAnchoArticulo As String, _
    ByVal pstrDescripcion As String, ByVal pstrCodigoColor As String, _
    ByVal pstrAcabado As String, ByVal pstrCalidad As String, ByVal pstrCodigoDiseno As String, _
    ByVal pstrComentario As String) As Integer
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
            "p_var_AnchoArticulo", pstrAnchoArticulo, "p_var_DescripArticulo", pstrDescripcion, _
            "p_var_CodigoColor", pstrCodigoColor, "p_var_DescripAcabado", pstrAcabado, _
            "p_var_Calidad", pstrCalidad, "p_var_CodigoDiseno", pstrCodigoDiseno, _
            "p_var_Comentarios", pstrComentario, "p_var_Usuario", _strUsuario}
            Return _objConnexion.EjecutarComando("usp_prc_ProcesarComentario", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ObtenerRutaLoteAlmacen(ByVal pstrNumeroLote As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"p_var_NumeroLote", pstrNumeroLote}
            Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerRutaLoteAlmacen", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ufn_ObtenerNuevoComentario(ByVal pstrCodigoArticulo As String, _
                                               ByVal pstrCodigoColor As String, _
                                               ByVal pstrAcabado As String, _
                                               ByVal pstrCalidad As String, _
                                               ByVal pstrCodigoDiseno As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
                                             "p_var_CodigoColor", pstrCodigoColor, _
                                             "p_var_DescripAcabado", pstrAcabado, _
                                             "p_var_Calidad", pstrCalidad, _
                                             "p_var_CodigoDiseno", pstrCodigoDiseno}
            Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerComentarios", objParametros)
        Catch ex As Exception
            Throw ex

        End Try
    End Function
    'Funcion para grabar y actualizar los comentarios que se vayan a ingresar
    Public Function ufn_ProcesarNuevoComentario(ByVal pstrCodigoArticulo As String, _
                                            ByVal pstrDescripcion As String, _
                                            ByVal pstrCodigoColor As String, _
                                            ByVal pstrDescripColor As String, _
                                            ByVal pstrAcabado As String, _
                                            ByVal pstrCalidad As String, _
                                            ByVal pstrCodigoDiseno As String, _
                                            ByVal pstrComentario As String, _
                                            ByVal pstrUsuario As String) As Integer
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"p_var_CodigoArticulo", pstrCodigoArticulo, _
                                             "p_var_DescripArticulo", pstrDescripcion, _
                                             "p_var_CodigoColor", pstrCodigoColor, _
                                             "p_var_DescripColor", pstrDescripColor, _
                                             "p_var_Proceso", pstrAcabado, _
                                             "p_var_Calidad", pstrCalidad, _
                                             "p_var_CodigoDiseno", pstrCodigoDiseno, _
                                             "p_var_Comentarios", pstrComentario, _
                                             "p_var_Usuario", _strUsuario}
            Return _objConnexion.EjecutarComando("USP_LOG_NM_PROCESAR_COMENTARIO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ObtenerConsumoTeoricoVsStock_Articulo(ByVal strCodigoArticulo As String, _
    ByVal strCodigoColor As String, ByVal strCodigoColorante As String) As DataTable
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Try
            Dim objParametros() As Object = {"var_CodigoArticulo", strCodigoArticulo, _
                                             "@var_CodigoColor", strCodigoColor, _
                                             "var_CodigoColorante", strCodigoColorante}
            Return _objConnexion.ObtenerDataTable("usp_LOG_ConsumoTeoricovsStock_Articulo", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ufn_ObtenerConsumoTeoricoVsStock_Insumos() As DataTable
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        Try
            Return _objConnexion.ObtenerDataTable("usp_LOG_ConsumoTeoricovsStock_Insumo")
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ufn_ObtenerAlmacenNSA() As DataSet
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", _strUsuario, _
                                             "pvch_NroSession", _strNroSession}
            Return _objConnexion.ObtenerDataSet("usp_Almacen_NSA_Listar", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function

    Public Function ufn_ObtenerAlmacenTela() As DataSet
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", _strUsuario}

            Return _objConnexion.ObtenerDataSet("usp_Almacen_Tela_Listar", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function

    'Public Function ufn_ObtenerUbicaciones() As DataSet
    '    Try
    '        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    '        Dim objParametros() As Object = {"pvch_Almacen", _strAlmacen}

    '        Return _objConnexion.ObtenerDataSet("usp_LOG_Ubicaciones_Listar", objParametros)
    '    Catch ex As Exception
    '        _strError = ex.Message
    '    End Try
    'End Function

    Public Function ObtenerListaUbicaciones() As DataTable
        Dim _objConnexion As NM.AccesoDatos.AccesoDatosSQLServer
        Dim strXMLData As String = ""
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            _objConnexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dbtResultado = _objConnexion.ObtenerDataTable("usp_LOG_Ubicaciones_Listar")
        Catch ex As Exception
            Throw ex
        Finally
            _objConnexion = Nothing
        End Try
        Return dbtResultado
    End Function
    Public Function ufn_ObtenerAlmacenes() As DataSet
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            'Dim objParametros() As Object = {"pvch_Usuario", _strUsuario}

            Return _objConnexion.ObtenerDataSet("usp_LOG_almacenes_Listar")
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function
    Public Function ObtenerInventariadores() As DataTable
        Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        Dim dbtResultado As DataTable

        dbtResultado = Nothing
        Try
            lobjVentas = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dbtResultado = lobjVentas.ObtenerDataTable("usp_LOG_inventariadores_Listar")
        Catch ex As Exception
            Throw ex
        Finally
            lobjVentas = Nothing
        End Try
        Return dbtResultado
    End Function

    Public Function ufn_ObtenerItems(ByVal strEmpresa As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_Empresa", strEmpresa}

            Return _objConnexion.ObtenerDataTable("usp_LOG_Consulta_Items_Almacen", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function

    Public Function ufn_ObtenerItem(ByVal strEmpresa As String, ByVal strCodigoItem As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_Empresa", strEmpresa, "var_Item", strCodigoItem}

            Return _objConnexion.ObtenerDataTable("usp_LOG_Consulta_Item_Almacen", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function
    Public Function ufn_ObtenerItem(ByVal strEmpresa As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_Empresa", strEmpresa}

            Return _objConnexion.ObtenerDataTable("usp_LOG_Consulta_Items_Almacen", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function

    Public Function GrabaRegistrosInventarioOtros(ByVal strEmpresa As String, ByVal strResponsable As String, ByVal strAlmacen As String, ByVal strTipoInve As String, ByVal strUbicacion As String, ByVal dbtItems As DataTable) As Boolean

        Dim blnRespuesta As Boolean = False
        Try
            Dim objUtil As New Util
            _objConnexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dbtItems.TableName = "Items"
            Dim strXMLData As String = objUtil.GeneraXml(dbtItems)
            Dim lstrParametros() As String = {"Var_Empresa", strEmpresa, "Var_Almacen", strAlmacen, "Var_Responsable", strResponsable, "Var_TipoInv", strTipoInve, "Var_Ubicacion", strUbicacion, "ntx_Detalle", strXMLData}
            _objConnexion.EjecutarComando("Usp_LOG_InventarioOtros_Grabar", lstrParametros)
            blnRespuesta = True
        Catch ex As Exception

        End Try
        Return blnRespuesta
    End Function

    Public Function GrabaRegistrosERI(ByVal strEmpresa As String, ByVal strAlmacen As String, ByVal strCodigoERI As String, ByVal strCodigoItem As String, ByVal dblCantidad As Double) As Boolean
        Dim blnRespuesta As Boolean = False
        Try
            Dim objUtil As New Util
            _objConnexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lstrParametros() As String = {"Var_Empresa", strEmpresa, "Var_Almacen", strAlmacen, "Var_CodigoERI", strCodigoERI, "Var_CodigoItem", strCodigoItem, "Num_Cantidad", dblCantidad}
            _objConnexion.EjecutarComando("Usp_LOG_InventarioERI_Grabar", lstrParametros)
            blnRespuesta = True
        Catch ex As Exception
        End Try
        Return blnRespuesta
    End Function

    Public Function GeneraERI(ByVal strEmpresa As String, ByVal strAlmacen As String, ByVal strCodigoERI As String) As Boolean
        Dim blnRespuesta As Boolean = False
        Try
            Dim objUtil As New Util
            _objConnexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lstrParametros() As String = {"Var_Empresa", strEmpresa, "Var_Almacen", strAlmacen, "Var_CodigoERI", strCodigoERI}
            _objConnexion.EjecutarComando("Usp_LOG_InventarioERI_Generar", lstrParametros)
            blnRespuesta = True
        Catch ex As Exception
        End Try
        Return blnRespuesta
    End Function
    Public Function CerrarERI(ByVal strEmpresa As String, ByVal strCodigoERI As String) As Boolean
        Dim blnRespuesta As Boolean = False
        Try
            Dim objUtil As New Util
            _objConnexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lstrParametros() As String = {"Var_Empresa", strEmpresa, "Var_CodigoERI", strCodigoERI}
            _objConnexion.EjecutarComando("Usp_LOG_InventarioERI_Cerrar", lstrParametros)
            blnRespuesta = True
        Catch ex As Exception
        End Try
        Return blnRespuesta
    End Function

    Public Function ufn_InicializarAlmacenNSA() As Boolean
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", _strUsuario, _
                                             "pvch_NroSession", _strNroSession}
            _objConnexion.EjecutarComando("usp_Almacen_NSA_Limpiar", objParametros)
            ufn_InicializarAlmacenNSA = True

        Catch ex As Exception
            _strError = ex.Message
            ufn_InicializarAlmacenNSA = False

        End Try
    End Function


    Public Function ufn_AgregarAlmacenNSA() As Boolean
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", _strUsuario, _
                                             "pvch_NroSession", _strNroSession, _
                                             "pvch_CodigoRollo", _strCodigoRollo, _
                                             "pvch_Almacen", _strAlmacen}

            _objConnexion.EjecutarComando("usp_Almacen_NSA_Insertar_V2", objParametros)
            ufn_AgregarAlmacenNSA = True

        Catch ex As Exception
            _strError = ex.Message
            ufn_AgregarAlmacenNSA = False

        End Try
    End Function

    Public Function ufn_EliminarAlmNSA_Rollo() As Boolean
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", _strUsuario, _
                                             "pvch_NroSession", _strNroSession, _
                                             "pvch_CodigoRollo", _strCodigoRollo}

            _objConnexion.EjecutarComando("usp_Almacen_NSA_EliminarRollo", objParametros)
            ufn_EliminarAlmNSA_Rollo = True
        Catch ex As Exception
            _strError = ex.Message
            ufn_EliminarAlmNSA_Rollo = False

        End Try
    End Function


    Public Function ufn_GenerarAlmacenNSA() As DataSet
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pchr_CodigoEmpresa", _strEmpresa, _
                                             "pchr_FechaDoc", _strFecha, _
                                             "pvch_CodigoAlmacen", _strAlmacen, _
                                             "pvch_CodigoCCosto", _strCCosto, _
                                             "pvch_Usuario", _strUsuario, _
                                             "pvch_NroSession", _strNroSession}
            Return _objConnexion.ObtenerDataSet("usp_LOG_AlmaceNSA_Grabar", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function
    Public Function ConsultaERI_Cabecera(ByVal strEmpresa As String, ByVal StrAlmacen As String, ByVal StrERI As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"CO_EMPR", strEmpresa, "CO_ALMA", StrAlmacen, "CO_INVE", StrERI}

            Return _objConnexion.ObtenerDataTable("USP_LOG_InventarioERI_Consultar", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function

    Public Function ufn_BuscaDatosDocumentos(ByVal strNumDocumento As String, ByVal strTipoDocumento As String, ByVal strCodAlmacen As String, ByVal strCodEmpresa As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)


            Dim objParametros() As Object = {"pstr_NumDocumento", strNumDocumento,
                                             "pstr_TipoDocumento", strTipoDocumento,
                                             "pstr_CodAlmacen", strCodAlmacen,
                                             "pstr_CodEmpresa", strCodEmpresa}

            Return _objConnexion.ObtenerDataTable("usp_LOG_BuscarDatosDocumentos", objParametros)
        Catch ex As Exception
            _strError = ex.Message
        End Try
    End Function

    'Public Function ConsultartRollo(ByVal codartirollo As String) As DataTable
    '    Try
    '        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    '        Dim objParametros() As Object = {"CODIGO_ARTICULO", codartirollo}
    '        Return _objConnexion.ObtenerDataTable("usp_qry_codartirollo")
    '    Catch ex As Exception

    '        Throw ex
    '    End Try
    'End Function


#End Region
    Function IngresarRollosVerificados(ByVal pstrUsuario As String, ByVal pdtListaRollos As DataTable) As DataTable
        Dim objUtil As New Util
        Dim dtResult As DataTable

        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)

            'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
            pdtListaRollos.TableName = "ROLLOS"
            Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(pdtListaRollos))
            ' Dim strXMLData As string = objUtil.GeneraXml(pDTListaPedido)

            Dim objParametros As Object() = {"pvch_Usuario", pstrUsuario,
                                             "pvch_ListaRollosXML", strListaRollosXML.ToString}

            dtResult = _objConnexion.ObtenerDataTable("usp_Almacen_Rollos_Transito_Procesar", objParametros)

        Catch ex As Exception
            Throw ex
        Finally
            objUtil = Nothing
        End Try

        Return dtResult

    End Function

    Function VerificandoCantDocumentos() As DataTable



        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objUtil As New Util

            Return _objConnexion.ObtenerDataTable("")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ObtenerRolloPorCodigo_NDV(ByVal pstrCodigoRollo As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"codigo_rollo", pstrCodigoRollo}

            Return _objConnexion.ObtenerDataTable("usp_rvf_nmrollo_obtenerrolloporcodigo_ndv", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConnexion = Nothing
        End Try
    End Function

    Public Function ufn_ObtenerGuiaPorNumero(ByVal pstrNuDocu As String, ByVal pstrTiDocu As String, ByVal pstrCoAlma As String, ByVal pstrCoUnid As String, ByVal pstrCoEmpr As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_nu_docu", pstrNuDocu, _
                                             "pvch_ti_docu", pstrTiDocu, _
                                             "pvch_co_alma", pstrCoAlma, _
                                             "pvch_co_unid", pstrCoUnid, _
                                             "pvch_co_empr", pstrCoEmpr}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_GUIA_POR_NUMERO", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConnexion = Nothing
        End Try
    End Function
    Public Function ufn_ObtenerRollosPorGuia(ByVal pstrNuGuia As String) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_nu_guia", pstrNuGuia}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_ROLLOS_NUMERO_GUIA", objParametros)
        Catch ex As Exception
            Throw ex
        Finally
            _objConnexion = Nothing
        End Try
    End Function
End Class
