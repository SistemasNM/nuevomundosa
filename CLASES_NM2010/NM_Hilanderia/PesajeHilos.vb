Imports NM.AccesoDatos
Imports NM_General
Imports System.Text

Public Class PesajeHilos
    Implements IDisposable


#Region " Declaracion de Variables Miembro "
    Private m_sqlHilanderia As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlHilanderia = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
    End Sub
#End Region

#Region " Definicion de Metodos "

    Public Function Obtener_Lista_Proveedor_Hilos(ByVal strTipoProceso As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_TipoProceso", strTipoProceso}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_BUSCAR_PROVEEDOR_HILOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function Obtener_Lista_Titulos_Hilos() As DataTable
        Dim dtblDatos As DataTable

        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_BUSCAR_TITULO_HILOS")
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Lista_Descripcion_Hilos(ByVal strTitulo As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_Titulo", strTitulo}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_BUSCAR_DESCRIPCION_HILOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function

    Public Function Obtener_Tipo_Hilo(ByVal strCodigoHilo As String) As String
        Dim strResult As String

        Dim objParametros() As Object = {"pstr_CodigoHilo", strCodigoHilo}
        Try
            strResult = m_sqlHilanderia.ObtenerValor("USP_HIL_BALANZA_OBTENER_TIPOHILO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function Obtener_Lista_Lotes_Activos(ByVal strTipoPesaje As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_TipoPesaje", strTipoPesaje}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_BUSCAR_LOTES_ACTIVOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Lista_Parametros_Tara(ByVal strTipoTara As String, ByVal strSubTipoTara As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_TipoTara", strTipoTara,
                                             "pvch_SubTipoTara", strSubTipoTara}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_BUSCAR_PARAMETROS_TARA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Valor_Parametro_Tara(ByVal strCodigoParametro As String) As Double
        Dim strResult As Double

        Dim objParametros() As Object = {"pstr_CodigoParametro", strCodigoParametro}
        Try
            strResult = m_sqlHilanderia.ObtenerValor("USP_HIL_BALANZA_OBTENER_VALOR_PARAMETRO_TARA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function


    Public Function RegistrarDatosPesajeHilos(ByVal strTipo_Pesaje As String,
                                             ByVal strRUC_Empresa As String,
                                             ByVal strTitulo_Hilo As String,
                                             ByVal strCodigo_Articulo As String,
                                             ByVal strTipo_Hilo As String,
                                             ByVal strCodigo_Maquina As String,
                                             ByVal strFecha_Produccion As String,
                                             ByVal strHora_Produccion As String,
                                             ByVal strLote As String,
                                             ByVal strCodigo_Paleta As String,
                                             ByVal dblPeso_Paleta As Double,
                                             ByVal strCodigo_Bobina As String,
                                             ByVal intCantidad_Bobina As Integer,
                                             ByVal dblPeso_Bobina As Double,
                                             ByVal strCodigoPlancha As String,
                                             ByVal intCantidad_Plancha As Integer,
                                             ByVal dblPeso_Plancha As Double,
                                             ByVal dblTotal_PesoBruto As Double,
                                             ByVal dblTotal_PesoTara As Double,
                                             ByVal strUsuario As String,
                                             ByVal strCodBalanza As String,
                                             ByVal strObservaciones As String,
                                             ByVal pDTListaDetalle As DataTable) As Integer

        Dim strResult As Integer
        Dim objUtil As New Util
        'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
        pDTListaDetalle.TableName = "DETALLE"

        Dim strListaDetalleXML As New StringBuilder(objUtil.GeneraXml(pDTListaDetalle))
        Dim objParametros() As Object = {"pstr_Tipo_Pesaje", strTipo_Pesaje,
                                         "pstr_RUC_Empresa", strRUC_Empresa,
                                         "pstr_Titulo_Hilo", strTitulo_Hilo,
                                         "pstr_Codigo_Articulo", strCodigo_Articulo,
                                         "pstr_Tipo_Hilo", strTipo_Hilo,
                                         "pstr_Codigo_Maquina", strCodigo_Maquina,
                                         "pstr_Fecha_Produccion", strFecha_Produccion,
                                         "pstr_Hora_Produccion", strHora_Produccion,
                                         "pstr_Lote", strLote,
                                         "pstr_Codigo_Paleta", strCodigo_Paleta,
                                         "pnum_Peso_Paleta", dblPeso_Paleta,
                                         "pstr_Codigo_Bobina", strCodigo_Bobina,
                                         "pnum_Cantidad_Bobina", intCantidad_Bobina,
                                         "pnum_Peso_Bobina", dblPeso_Bobina,
                                         "pstr_CodigoPlancha", strCodigoPlancha,
                                         "pnum_Cantidad_Plancha", intCantidad_Plancha,
                                         "pnum_Peso_Plancha", dblPeso_Plancha,
                                         "pnum_Total_PesoBruto", dblTotal_PesoBruto,
                                         "pnum_Total_PesoTara", dblTotal_PesoTara,
                                         "pstr_Usuario", strUsuario,
                                         "pstr_CodBalanza", strCodBalanza,
                                         "pstr_Observaciones", strObservaciones,
                                         "pvch_ListaDetalleXML", strListaDetalleXML.ToString}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_REGISTRA_DATOS_PESAJE_HILOS_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function Obtener_Lista_Registros_Pesaje_Hilos(ByVal strEstado As String, ByVal strCodBalanza As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_Estado", strEstado,
                                             "pvch_CodBalanza", strCodBalanza}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_LISTAR_REGISTROS_PESAJE_HILOS_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Datos_Parihuela_Etiqueta(ByVal intCodigoParihuela As Integer) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pint_IDParihuela", intCodigoParihuela}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_OBTENER_DATOS_PARIHUELA_ETIQUETA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function Obtener_Lista_Maquinas_Activas(ByVal strCodLinea As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_CodigoLinea", strCodLinea}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_LISTAR_MAQUINAS_ACTIVAS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Eliminar_Parihuela_Pendiente(ByVal strCodigoParihuela As String, ByVal strUsuarioSupervisor As String) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pvch_CodigoParihuela", strCodigoParihuela,
                                             "pvch_UsuarioSupervisor", strUsuarioSupervisor}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_ELIMINAR_PARIHUELA_PENDIENTE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function

    Public Function Actualizar_Parihuela_Inventario(ByVal intCodigoParihuela As Integer) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pint_CodigoParihuela", intCodigoParihuela}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_ACTUALIZA_PARIHUELA_INVENTARIO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function

    Public Function Obtener_Datos_Documento_Ingreso(ByVal strTipoDocu As String, ByVal strNumeroDocu As String, ByVal strCodigoAlmacen As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros() As Object = {"pvch_TipoDocumento", strTipoDocu,
                                             "pvch_NumeroDocumento", strNumeroDocu,
                                             "pvch_CodigoAlmacen", strCodigoAlmacen}

            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_LISTAR_DATOS_DOCUMENTO_INGRESO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Totales_Documento_Ingreso(ByVal strTipoDocu As String, ByVal strNumeroDocu As String, ByVal strCodigoItem As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros() As Object = {"pvch_TipoDocumento", strTipoDocu,
                                             "pvch_NumeroDocumento", strNumeroDocu,
                                             "pvch_CodigoItem", strCodigoItem}

            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BALANZA_LISTAR_TOTALES_DOCUMENTO_INGRESO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function RegistrarDatosParihuelaHilos(ByVal strTipo_Pesaje As String,
                                                 ByVal strRUC_Empresa As String,
                                                 ByVal strCodigo_Articulo As String,
                                                 ByVal strTipo_Hilo As String,
                                                 ByVal strFecha_Produccion As String,
                                                 ByVal strHora_Produccion As String,
                                                 ByVal strLote As String,
                                                 ByVal intTotalConos_Disponible As Integer,
                                                 ByVal dblTotalKilos_Disponible As Double,
                                                 ByVal strUsuario As String,
                                                 ByVal strCodBalanza As String,
                                                 ByVal strObservaciones As String,
                                                 ByVal strTipoDocu_Refe As String,
                                                 ByVal strNumDocu_Refe As String) As Integer

        Dim strResult As Integer
        Dim objUtil As New Util
        'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer

        Dim objParametros() As Object = {"pstr_Tipo_Pesaje", strTipo_Pesaje,
                                         "pstr_RUC_Empresa", strRUC_Empresa,
                                         "pstr_Codigo_Articulo", strCodigo_Articulo,
                                         "pstr_Tipo_Hilo", strTipo_Hilo,
                                         "pstr_Fecha_Produccion", strFecha_Produccion,
                                         "pstr_Hora_Produccion", strHora_Produccion,
                                         "pstr_Lote", strLote,
                                         "pnum_TotalConos_Disponible", intTotalConos_Disponible,
                                         "pnum_TotalKilos_Disponible", dblTotalKilos_Disponible,
                                         "pstr_Usuario", strUsuario,
                                         "pstr_CodBalanza", strCodBalanza,
                                         "pstr_Observaciones", strObservaciones,
                                         "pstr_TipoDocu_Refe", strTipoDocu_Refe,
                                         "pstr_NumDocu_Refe", strNumDocu_Refe}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_REGISTRA_DATOS_PARIHUELA_HILOS_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function Eliminar_Parihuela_Terceros(ByVal strCodigoParihuela As String, ByVal strUsuarioSupervisor As String) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pvch_CodigoParihuela", strCodigoParihuela,
                                             "pvch_UsuarioSupervisor", strUsuarioSupervisor}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_ELIMINAR_PARIHUELA_TERCEROS_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function

    Public Function Verificar_Parihuela_Reimpresion(ByVal intCodigoParihuela As Integer) As String
        Dim strResult As String

        Try
            Dim objParametros() As Object = {"pint_IDParihuela", intCodigoParihuela}
            strResult = m_sqlHilanderia.ObtenerValor("USP_HIL_BALANZA_VERIFICAR_PARIHUELA_REIMPRESION", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult

    End Function

    Public Function Obtener_Lista_Articulos_Reclasificacion(ByVal strTipoLista As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_TipoLista", strTipoLista}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_OBTENER_LISTA_ARTICULOS_RECLASIFICACION", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

        Public Function GenerarDocumentoTransferenciaAlmacen(ByVal strAlmacenOrigen As String,
                                                         ByVal strAlmacenDestino As String,
                                                         ByVal strObservaciones As String,
                                                         ByVal strCodigoUsuario As String,
                                                         ByVal pdtListaParihuelas As DataTable) As DataTable

        Dim objUtil As New Util
        Dim lobjLOG As NM.AccesoDatos.AccesoDatosSQLServer
        Dim dtResult As DataTable

        Try
            Dim strDatosParihuelasXML As New StringBuilder(objUtil.GeneraXml(pdtListaParihuelas))
            Dim objParametros() As Object = {"pvch_AlmacenOrigen", strAlmacenOrigen,
                                             "pvch_AlmacenDestino", strAlmacenDestino,
                                             "pvch_Observaciones", strObservaciones,
                                             "pvch_CodigoUsuario", strCodigoUsuario,
                                             "pvch_DatosParihuelasXML", strDatosParihuelasXML.ToString}

            lobjLOG = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            dtResult = lobjLOG.ObtenerDataTable("USP_LOG_GENERAR_TRANSFERENCIA_ALMACEN_HILOS", objParametros)

            Return dtResult
        Catch ex As Exception
            Throw ex
        Finally
            lobjLOG = Nothing
            objUtil = Nothing
            dtResult = Nothing
        End Try
    End Function


    Public Function AnularDespachoParihuela(ByVal strCodigoParihuela As String, ByVal strTipoDocumento As String, ByVal strNumeroDocumento As String, ByVal intNumeroSecuencia As Integer, ByVal strCodigoUsuario As String) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pvch_CodigoParihuela", strCodigoParihuela,
                                             "pvch_TipoDocumento", strTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento,
                                             "pint_NumeroSecuencia", intNumeroSecuencia,
                                             "pvch_CodigoUsuario", strCodigoUsuario}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_ANULAR_DOCUMENTO_DESPACHO_PARIHUELA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function
#End Region

    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlHilanderia.Dispose()
    End Sub
End Class
