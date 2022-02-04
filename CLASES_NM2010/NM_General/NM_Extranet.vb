Imports NM.AccesoDatos
Imports System.Text

Public Class NM_Extranet
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private _objConnexion As AccesoDatosSQLServer
    Private _objConnexion_1 As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    End Sub
#End Region

#Region " Funciones "

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Horas Disponibles para la Cita - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function grabarDatosAdicionalesArticulos(ByVal pstrArticulo As String,
                                                    ByVal pstrFamilia As String,
                                                    ByVal pstrModa As String,
                                                    ByVal pstrCompetitividad As String,
                                                    ByVal pstrUsuario As String,
                                                    ByVal pstrLinea As String,
                                                    ByVal intRevision As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodigoArticulo", pstrArticulo,
                                             "pvchFlagFamCalidad", pstrFamilia,
                                             "pvchFlagModa", pstrModa,
                                             "pvchFlagCompetitividad", pstrCompetitividad,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchLinea", pstrLinea,
                                             "intRevision", intRevision}

            Return _objConnexion.ObtenerDataTable("usp_tej_insertar_datos_adicionales_articulo", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'CAMBIO DG - AJUSTE COMITE - INI
    Public Function GrabarDatosExtrasArticulo(ByVal pstrArticulo As String,
                                                                      ByVal intTipoUrdi As Integer,
                                                                      ByVal intTipoTra As Integer,
                                                                      ByVal intElong As Integer,
                                                                      ByVal pstrUsuario As String,
                                                                      ByVal intRevision As Integer) As Integer
        Try

            _objConnexion_1 = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objparametros() As Object = {"VCH_ART", pstrArticulo,
                                            "INT_CATEGORIA_URD", intTipoUrdi,
                                            "INT_CATEGORIA_TRA", intTipoTra,
                                            "INT_ELONG", intElong,
                                            "VCH_USR", pstrUsuario,
                                             "INT_REVISION", intRevision}

            Return _objConnexion_1.EjecutarComando("USP_ACTUALIZAR_DATOS_EXTRAS_ARTICULO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function obtenerDatosExtrasArticulos(ByVal pstrArticulo As String) As DataTable
        Try
            _objConnexion_1 = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objparametros() As Object = {"VCH_ART", pstrArticulo}

            Return _objConnexion_1.ObtenerDataTable("USP_LISTAR_DATOS_EXTRAS_ARTICULO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'CAMBIO DG - AJUSTE COMITE - FIN
    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Horas Disponibles para la Cita - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function obtenerDatosAdicionalesArticulos(ByVal pstrArticulo As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodigoArticulo", pstrArticulo}

            Return _objConnexion.ObtenerDataTable("usp_tej_obtener_datos_adicionales_articulo", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Horas Disponibles para la Cita - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerListaHorasCita_Disponibles(ByVal strFechaCita As Date) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_HORAS_DISPONIBLES_CITA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Horas Disponibles para la Cita - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerListaHorasCita_Disponibles_v2(ByVal strFechaCita As Date) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_HORAS_DISPONIBLES_CITA_V2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Horas Disponibles para la Cita - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerListaHorasCita_Disponibles_v3(ByVal strFechaCita As Date, ByVal strTipAte As String, ByVal strCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita,
                                             "pvch_FlagAte", strTipAte,
                                             "pvch_CodProv", strCodProv}

            'Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_HORAS_DISPONIBLES_CITA_V3", objparametros)
            Return _objConnexion.ObtenerDataTable("USP_LOG_GENERAR_HORARIOS_PROVEEDORES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerListaHorasCita_Disponibles_v4(ByVal strFechaCita As Date, ByVal strTipAte As String, ByVal strCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita,
                                             "pvch_FlagAte", strTipAte,
                                             "pvch_CodProv", strCodProv}

            'Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_HORAS_DISPONIBLES_CITA_V3", objparametros)
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_HORAS_DISPONIBLES_CITA_V4", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'REQSIS201900037 - DG - INI
    Public Function ObtenerListaHorasCita_Camiones(ByVal strFechaCita As Date, ByVal strCamion As String) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita,
                                             "pvch_CodCamion", strCamion}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_HORAS_DISPONIBLES_CITA_CAMIONES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'REQSIS201900037 - DG - FIN
    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Ordenes Pendientes Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerOrdenesPendientesProveedor_Cita(ByVal strCodigoProveedor As String) As DataTable
        Try
            Dim objParametros() As Object = {"pvch_CodProveedor", strCodigoProveedor}
            Return _objConnexion.ObtenerDataTable("USP_LOG_EXTRANET_ORDENES_PENDIENTE_PROVEEDOR_CITA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Detalle Orden Compra - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerSalidasDistribucion(ByVal intNumSalida As String) As DataTable
        Try
            Dim objParametros() As Object = {"pintNumSalida", intNumSalida}
            Return _objConnexion.ObtenerDataTable("USP_LOG_BUSCAR_SALIDA_DISTRIBUCION", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ObtenerPedidosDesperdicios(ByVal intNumSalida As String) As DataTable
        Try
            Dim objParametros() As Object = {"pvchNumPedido", intNumSalida}
            Return _objConnexion.ObtenerDataTable("USP_LOG_BUSCAR_PEDIDOS_DESPERDICIOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Detalle Orden Compra - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 26/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDetalleOrdenCompra(ByVal strOrdenCompra As String) As DataTable
        Try
            Dim objParametros() As Object = {"pvch_OrdenCompra", strOrdenCompra}
            Return _objConnexion.ObtenerDataTable("USP_LOG_EXTRANET_DETALLE_ORDEN_COMPRA_CITA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 02/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaLogistica(ByVal strCodigoProveedor As String,
                                         ByVal dtmFechaCita As Date,
                                         ByVal strHoraCita As String,
                                         ByVal strRangoCita As String,
                                         ByVal strUsuario As String,
                                         ByVal dtListaArticulosCita As DataTable) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_LOGISTICA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Proveedores - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaLogistica_v2(ByVal strCodigoProveedor As String,
                                            ByVal dtmFechaCita As Date,
                                            ByVal strHoraCita As String,
                                            ByVal strRangoCita As String,
                                            ByVal strUsuario As String,
                                            ByVal dtListaArticulosCita As DataTable,
                                            ByVal strFlagMontaCarga As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_LOGISTICA_v2", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Clientes - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaLogistica_v3(ByVal strCodigoProveedor As String,
                                            ByVal dtmFechaCita As Date,
                                            ByVal strHoraCita As String,
                                            ByVal strRangoCita As String,
                                            ByVal strUsuario As String,
                                            ByVal dtListaArticulosCita As DataTable,
                                            ByVal strFlagMontaCarga As String,
                                            ByVal strCamion As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga,
                                             "pvch_Camion", strCamion}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_LOGISTICA_v3", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Proveedores - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaLogistica_v4(ByVal strCodigoProveedor As String,
                                            ByVal dtmFechaCita As Date,
                                            ByVal strHoraCita As String,
                                            ByVal strRangoCita As String,
                                            ByVal strUsuario As String,
                                            ByVal dtListaArticulosCita As DataTable,
                                            ByVal strFlagMontaCarga As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_LOGISTICA_v4", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Clientes - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaLogistica_v5(ByVal strCodigoProveedor As String,
                                            ByVal dtmFechaCita As Date,
                                            ByVal strHoraCita As String,
                                            ByVal strRangoCita As String,
                                            ByVal strUsuario As String,
                                            ByVal dtListaArticulosCita As DataTable,
                                            ByVal strFlagMontaCarga As String,
                                            ByVal strCamion As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga,
                                             "pvch_Camion", strCamion}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_LOGISTICA_v5", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Proveedores - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaLogistica_v6(ByVal strCodigoProveedor As String,
                                            ByVal dtmFechaCita As Date,
                                            ByVal strHoraCita As String,
                                            ByVal strRangoCita As String,
                                            ByVal strUsuario As String,
                                            ByVal dtListaArticulosCita As DataTable,
                                            ByVal strFlagMontaCarga As String,
                                            ByVal strTipoDocu As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga,
                                             "pvch_TipoDocumento", strTipoDocu}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_LOGISTICA_SERVICIOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 07/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerListaCitasLogistica(ByVal strFechaCita As Date) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_CITAS_LOGISTICA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 07/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerListaCitasLogistica_v2(ByVal strFechaCita As Date) As DataTable
        Try
            Dim objparametros() As Object = {"pdtm_FechaCita", strFechaCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_LISTA_CITAS_LOGISTICA_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 09/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerDetalleCitaLogistica(ByVal intIDCita As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pint_IDCita", intIDCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_DETALLE_CITA_LOGISTICA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 09/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerDetalleCitaClienteTela(ByVal intIDCita As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pint_IDCita", intIDCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_DETALLE_CITA_CLIENTE_TELA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 09/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function AtenderCitaLogistica(ByVal intIDCita As Integer, ByVal strUsuario As String) As Integer
        Try
            Dim objparametros() As Object = {"pint_IDCita", intIDCita,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("USP_LOG_ATENDER_CITA_LOGISTICA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 27/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function AnularCitaLogistica(ByVal intIDCita As Integer, ByVal strUsuario As String) As Integer
        Try
            Dim objparametros() As Object = {"pint_IDCita", intIDCita,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("USP_LOG_ANULAR_CITA_LOGISTICA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 09/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerDatosCitaTicket(ByVal intIDCita As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pint_IDCita", intIDCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_DATOS_CITA_TICKET", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cita de Logistica - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 09/08/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerDatosCitaTicket_v2(ByVal intIDCita As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pint_IDCita", intIDCita}

            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_DATOS_CITA_TICKET_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'CAMBIO DG INI
    Public Function ListarCitasPendientes(ByVal StrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_Usuario", StrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_CITAS_PENDIENTES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarCitasPendientesxModulo(ByVal StrUsuario As String,
                                                 ByVal strCodCliente As String,
                                                 ByVal strTipModulo As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_Usuario", StrUsuario,
                                             "pvch_CodCliente", strCodCliente,
                                             "pvch_TipMod", strTipModulo}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_CITAS_PENDIENTES_X_MODULO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'CAMBIO DG INI
    Public Function ListarCitasPendientes_v2(ByVal StrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_Usuario", StrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_CITAS_PENDIENTES_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function AnularCitasClientes(ByVal intCodCita As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"p_intCodCita", intCodCita}
            Return _objConnexion.ObtenerDataTable("USP_LOG_ANULAR_CITAS_CLIENTES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'CAMBIO DG FIN

    Public Function actualizarEstadosCitasProveedores(ByVal strEstado As String,
                                                      ByVal strIdCita As String,
                                                      ByVal strUsuario As String,
                                                      Optional ByVal strCodAyudante1 As String = "0",
                                                      Optional ByVal strCodAyudante2 As String = "0") As DataTable
        Try
            Dim objparametros() As Object = {"pvch_Estado", strEstado,
                                             "pvch_IdCita", strIdCita,
                                             "pvch_Usuario", strUsuario,
                                             "pvch_CodAyudante1", strCodAyudante1,
                                             "pvch_CodAyudante2", strCodAyudante2}
            Return _objConnexion.ObtenerDataTable("USP_LOG_ACTUALIZAR_CITAS_PROVEEDORES_V2", objparametros)
            'Return _objConnexion.ObtenerDataTable("USP_LOG_ACTUALIZAR_CITAS_PROVEEDORES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function subirRequisitoArchivo(ByVal pstrNumRequi As String,
    Public Function subirRequisitoArchivo(ByVal pstrCodProv As String,
                                          ByVal pstrCodRubro As String,
                                          ByVal pintIDRequisito As Integer,
                                          ByVal pstrFlagCompromiso As String,
                                          ByVal pstrFileName As String,
                                          ByVal pintFileSize As Integer,
                                          ByVal pstrContentType As String,
                                          ByVal pstrFileExtension As String,
                                          ByVal pbyteContent As Byte(),
                                          ByVal pstrUsuario As String,
                                          ByVal pstrVigencia As String) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pchrFlagCompromiso", pstrFlagCompromiso,
                                             "pvchFileName", pstrFileName,
                                             "pintFileSize", pintFileSize,
                                             "pstrContentType", pstrContentType,
                                             "pstrFileExtension", pstrFileExtension,
                                             "pvbContent", pbyteContent,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchVigencia", pstrVigencia}
            Return _objConnexion.ObtenerDataTable("USP_LOG_CARGAR_ARCHIVOS_ADJUNTOS_REQUISITOS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function subirRequisitoArchivo_2(ByVal pstrCodProv As String,
                                          ByVal pstrCodRubro As String,
                                          ByVal pstrTipDoc As String,
                                          ByVal pstrNumDoc As String,
                                          ByVal pintIDRequisito As Integer,
                                          ByVal pstrFlagCompromiso As String,
                                          ByVal pstrFileName As String,
                                          ByVal pintFileSize As Integer,
                                          ByVal pstrContentType As String,
                                          ByVal pstrFileExtension As String,
                                          ByVal pbyteContent As Byte(),
                                          ByVal pstrUsuario As String,
                                          ByVal pstrVigencia As String) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pchrFlagCompromiso", pstrFlagCompromiso,
                                             "pvchFileName", pstrFileName,
                                             "pintFileSize", pintFileSize,
                                             "pstrContentType", pstrContentType,
                                             "pstrFileExtension", pstrFileExtension,
                                             "pvbContent", pbyteContent,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchVigencia", pstrVigencia}
            Return _objConnexion.ObtenerDataTable("USP_LOG_CARGAR_ARCHIVOS_ADJUNTOS_REQUISITOS_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function DescargarRequisitoArchivo(ByVal pstrNumRequi As String,
    Public Function DescargarRequisitoArchivo(ByVal pstrCodProv As String,
                                              ByVal pstrCodRubro As String,
                                              ByVal pintIDRequisito As Integer) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pintIDRequisito", pintIDRequisito}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_ARCHIVO_ADJUNTO_REQUISITO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DescargarRequisitoArchivo_v2(ByVal pstrCodProv As String,
                                              ByVal pstrCodRubro As String,
                                              ByVal pstrTipDoc As String,
                                              ByVal pstrNumDoc As String,
                                              ByVal pintIDRequisito As Integer) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pintIDRequisito", pintIDRequisito}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_ARCHIVO_ADJUNTO_REQUISITO_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DescargarRequisitoArchivo_v3(ByVal pstrCodProv As String,
                                                 ByVal pstrCodRubro As String,
                                                 ByVal pstrTipDoc As String,
                                                 ByVal pstrNumDoc As String,
                                                 ByVal pintIDRequisito As Integer,
                                                 ByVal pintIDCitaInduccion As Integer) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pintIDCitaInduccion", pintIDCitaInduccion}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_ARCHIVO_ADJUNTO_REQUISITO_v3", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '    Public Function limpiarRequisitoArchivo(ByVal pstrNumRequi As String,
    Public Function limpiarRequisitoArchivo(ByVal pstrCodProv As String,
                                            ByVal pstrCodRubro As String,
                                            ByVal pintIDRequisito As Integer) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pintIDRequisito", pintIDRequisito}
            Return _objConnexion.ObtenerDataTable("USP_LOG_LIMPIAR_ARCHIVO_ADJUNTO_REQUISITO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '    Public Function limpiarRequisitoArchivo(ByVal pstrNumRequi As String,
    Public Function limpiarRequisitoArchivo_3(ByVal pstrCodProv As String,
                                            ByVal pstrCodRubro As String,
                                            ByVal pstrTipDoc As String,
                                            ByVal pstrNumDoc As String,
                                            ByVal pintIDRequisito As Integer) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pintIDRequisito", pintIDRequisito}
            Return _objConnexion.ObtenerDataTable("USP_LOG_LIMPIAR_ARCHIVO_ADJUNTO_REQUISITO_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function limpiarRequisitoArchivo_4(ByVal pstrCodProv As String,
                                              ByVal pstrCodRubro As String,
                                              ByVal pstrTipDoc As String,
                                              ByVal pstrNumDoc As String,
                                              ByVal pintIDRequisito As Integer,
                                              ByVal pstrUsuario As String,
                                              ByVal pstrObservacion As String) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchObservacion", pstrObservacion}
            Return _objConnexion.ObtenerDataTable("USP_LOG_LIMPIAR_ARCHIVO_ADJUNTO_REQUISITO_4", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function limpiarRequisitoArchivo_2(ByVal pstrCodProv As String,
                                            ByVal pstrCodRubro As String,
                                            ByVal pintIDRequisito As Integer) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pintIDRequisito", pintIDRequisito}
            Return _objConnexion.ObtenerDataTable("USP_LOG_LIMPIAR_ARCHIVO_ADJUNTO_REQUISITO_2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function limpiarRequisitoArchivo_3(ByVal pstrCodProv As String,
                                              ByVal pstrCodRubro As String,
                                              ByVal pintIDRequisito As Integer,
                                              ByVal pstrUsuario As String,
                                              ByVal pstrObservacion As String) As DataTable
        Try
            'Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchObservacion", pstrObservacion}
            Return _objConnexion.ObtenerDataTable("USP_LOG_LIMPIAR_ARCHIVO_ADJUNTO_REQUISITO_3", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ingresarPostulanteServicio(ByVal pstrNumRequi As String,
                                               ByVal pstrCodProv As String,
                                               ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
                                             "pvchCodProv", pstrCodProv,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_LOG_INGRESAR_POSTULANTES_SERVICIO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function listarServiciosxRubro(ByVal pstrCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv}
            Return _objConnexion.ObtenerDataTable("usp_log_listar_servicios_x_rubro", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function listarServiciosxGenerar() As DataTable
        Try
            'Dim objparametros() As Object = {"pvch_OrdenCompra", ""}
            Return _objConnexion.ObtenerDataTable2("usp_log_obtener_servicios_pendientes_x_generar")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function obtenerCabeceraRequisicionServicio(ByVal pstrNumRequi As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumRequisicion", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_detalle_servicio_postulante", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerDetalleRequisicionServicio(ByVal pstrNumRequi As String,
                                                      ByVal pstrCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumRequisicion", pstrNumRequi,
                                             "pvchCodProv", pstrCodProv}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_detalle_servicio_requisicion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerRequisitosServicio(ByVal pstrCodProv As String, ByVal pstrNumRequi As String) As DataTable
        'Public Function obtenerRequisitosServicio(ByVal pstrCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv,
                                            "pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_requisitos_x_rubro", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function obtenerRequisitosServicio_v2(ByVal pstrCodProv As String, ByVal pstrNumRequi As String) As DataTable
    Public Function obtenerRequisitosServicio_v2(ByVal pstrCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_requisitos_x_rubro_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerRequisitosServicio_v3(ByVal pstrCodProv As String,
                                                 ByVal pstrNumRequi As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv,
                                             "pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_requisitos_x_rubro_v3", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function obtenerRequisitosServicio_v2(ByVal pstrCodProv As String, ByVal pstrNumRequi As String) As DataTable
    Public Function obtenerRequisitosServicio_v6(ByVal pstrCodProv As String,
                                                 ByVal pstrTipDoc As String,
                                                 ByVal pstrNumDoc As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv,
                                             "pvchTipDocumento", pstrTipDoc,
                                             "pvchNumeroDocumento", pstrNumDoc}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_requisitos_x_rubro_v6", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function obtenerTiposTrabajoServicio(ByVal pstrOperacion As String,
                                                ByVal pstrNumRequi As String,
                                                ByVal pintIDTipTrabajo As Integer,
                                                ByVal pintIDRequisito As Integer,
                                                ByVal pstrDescripcion As String,
                                                ByVal pstrEstado As String,
                                                ByVal pstrUsuario As String) As DataTable
        Dim lstrParametros() As Object = {"pvchOperacion", pstrOperacion,
                                          "pvchNumRequi", pstrNumRequi,
                                          "pintIDTipTrabajo", pintIDTipTrabajo,
                                          "pintIDRequisito", pintIDRequisito,
                                          "pvchDescripcion", pstrDescripcion,
                                          "pvchEstado", pstrEstado,
                                          "pvchUsuario", pstrUsuario}
        Try
            Return _objConnexion.ObtenerDataTable("USP_LOG_CRUD_TIP_TRABAJO_REQUISITO", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function asociarTipoTrabajo(ByVal pstrOperacion As String,
                                           ByVal pstrCodProv As String,
                                           ByVal pintIDTipTrabajo As Integer,
                                           ByVal pstrUsuario As String) As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Dim params() As Object = {"pvchOperacion", pstrOperacion,
                                  "pvchCodProv", pstrCodProv,
                                  "pintIDTipTrabajo", pintIDTipTrabajo,
                                  "pvchUsuario", pstrUsuario}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            ldtRes = lobjCon.ObtenerDataTable("USP_LOG_TIP_TRABAJO_PROVEEDORES_ASOCIAR", params)
            Return ldtRes
        Catch ex As Exception
            Throw ex
        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
    End Function

    Public Function generarCotizacionServicio(ByVal pBloqueSeccion As String, ByVal pintCotizacionServicio As Integer, ByVal pvchNumRequi As String, ByVal pvchCodProveedor As String,
                                              ByVal pvchFonoFax As String, ByVal pvchFecEjecTra As String, ByVal pvchAtencion As String, ByVal pvchArea As String, ByVal pvchFecEjecTer As String,
                                              ByVal pvchDescripcion01SecI As String, ByVal pvchZonaTrabajo01SecI As String, ByVal pnumCantidad01SecI As Double, ByVal pnumPrecioUni01SecI As Double,
                                              ByVal pnumPrecioTot01SecI As Double, ByVal pvchDescripcion02SecI As String, ByVal pvchZonaTrabajo02SecI As String, ByVal pnumCantidad02SecI As Double,
                                              ByVal pnumPrecioUni02SecI As Double, ByVal pnumPrecioTot02SecI As Double, ByVal pvchDescripcion03SecI As String, ByVal pvchZonaTrabajo03SecI As String,
                                              ByVal pnumCantidad03SecI As Double, ByVal pnumPrecioUni03SecI As Double, ByVal pnumPrecioTot03SecI As Double, ByVal pvchDescripcion04SecI As String,
                                              ByVal pvchZonaTrabajo04SecI As String, ByVal pnumCantidad04SecI As Double, ByVal pnumPrecioUni04SecI As Double, ByVal pnumPrecioTot04SecI As Double,
                                              ByVal pvchDescripcion05SecI As String, ByVal pvchZonaTrabajo05SecI As String, ByVal pnumCantidad05SecI As Double, ByVal pnumPrecioUni05SecI As Double,
                                              ByVal pnumPrecioTot05SecI As Double, ByVal pvchObservacionSecI As String, ByVal pnumPrecioTotSecI As Double, ByVal pnumMaterialSecII As Double,
                                              ByVal pnumManoObraSecII As Double, ByVal pnumEQyServSecII As Double, ByVal pnumGGyUtilSecII As Double, ByVal pnumPrecioTotSecII As Double,
                                              ByVal pvchValidezOfertaSecII As String, ByVal pvchPlazoDiasSecII As String, ByVal pvchFormaPago As String, ByVal pvchDescripcion01SecIIIA As String,
                                              ByVal pvchMarca01SecIIIA As String, ByVal pvchUnidadM01SecIIIA As String, ByVal pnumCantidad01SecIIIA As Double, ByVal pnumPrecioUni01SecIIIA As Double,
                                              ByVal pnumPrecioTot01SecIIIA As Double, ByVal pvchDescripcion02SecIIIA As String, ByVal pvchMarca02SecIIIA As String, ByVal pvchUnidadM02SecIIIA As String,
                                              ByVal pnumCantidad02SecIIIA As Double, ByVal pnumPrecioUni02SecIIIA As Double, ByVal pnumPrecioTot02SecIIIA As Double, ByVal pvchDescripcion03SecIIIA As String,
                                              ByVal pvchMarca03SecIIIA As String, ByVal pvchUnidadM03SecIIIA As String, ByVal pnumCantidad03SecIIIA As Double, ByVal pnumPrecioUni03SecIIIA As Double,
                                              ByVal pnumPrecioTot03SecIIIA As Double, ByVal pvchDescripcion04SecIIIA As String, ByVal pvchMarca04SecIIIA As String, ByVal pvchUnidadM04SecIIIA As String,
                                              ByVal pnumCantidad04SecIIIA As Double, ByVal pnumPrecioUni04SecIIIA As Double, ByVal pnumPrecioTot04SecIIIA As Double, ByVal pvchDescripcion05SecIIIA As String,
                                              ByVal pvchMarca05SecIIIA As String, ByVal pvchUnidadM05SecIIIA As String, ByVal pnumCantidad05SecIIIA As Double, ByVal pnumPrecioUni05SecIIIA As Double,
                                              ByVal pnumPrecioTot05SecIIIA As Double, ByVal pnumPrecioTotSecIIIA As Double, ByVal pvchObservacionSecIIIA As String, ByVal pvchEspecialidad01SecIIIB As String,
                                              ByVal pnumPersonas01SecIIIB As Double, ByVal pnumHoras01SecIIIB As Double, ByVal pnumPrecioUni01SecIIIB As Double, ByVal pnumPrecioTot01SecIIIB As Double,
                                              ByVal pvchEspecialidad02SecIIIB As String, ByVal pnumPersonas02SecIIIB As Double, ByVal pnumHoras02SecIIIB As Double, ByVal pnumPrecioUni02SecIIIB As Double,
                                              ByVal pnumPrecioTot02SecIIIB As Double, ByVal pvchEspecialidad03SecIIIB As String, ByVal pnumPersonas03SecIIIB As Double, ByVal pnumHoras03SecIIIB As Double,
                                              ByVal pnumPrecioUni03SecIIIB As Double, ByVal pnumPrecioTot03SecIIIB As Double, ByVal pvchEspecialidad04SecIIIB As String, ByVal pnumPersonas04SecIIIB As Double,
                                              ByVal pnumHoras04SecIIIB As Double, ByVal pnumPrecioUni04SecIIIB As Double, ByVal pnumPrecioTot04SecIIIB As Double, ByVal pvchEspecialidad05SecIIIB As String,
                                              ByVal pnumPersonas05SecIIIB As Double, ByVal pnumHoras05SecIIIB As Double, ByVal pnumPrecioUni05SecIIIB As Double, ByVal pnumPrecioTot05SecIIIB As Double,
                                              ByVal pvchEspecialidad06SecIIIB As String, ByVal pnumPersonas06SecIIIB As Double, ByVal pnumHoras06SecIIIB As Double, ByVal pnumPrecioUni06SecIIIB As Double,
                                              ByVal pnumPrecioTot06SecIIIB As Double, ByVal pvchEspecialidad07SecIIIB As String, ByVal pnumPersonas07SecIIIB As Double, ByVal pnumHoras07SecIIIB As Double,
                                              ByVal pnumPrecioUni07SecIIIB As Double, ByVal pnumPrecioTot07SecIIIB As Double, ByVal pvchEspecialidad08SecIIIB As String, ByVal pnumPersonas08SecIIIB As Double,
                                              ByVal pnumHoras08SecIIIB As Double, ByVal pnumPrecioUni08SecIIIB As Double, ByVal pnumPrecioTot08SecIIIB As Double, ByVal pnumPrecioTotSecIIIB As Double,
                                              ByVal pvchObservacionSecIIIB As String, ByVal pvchEquipos01SecIIIC As String, ByVal pnumEquipos01SecIIIC As Double, ByVal pnumPersonas01SecIIIC As Double,
                                              ByVal pnumHoras01SecIIIC As Double, ByVal pnumPrecioUni01SecIIIC As Double, ByVal pnumPrecioTot01SecIIIC As Double, ByVal pvchEquipos02SecIIIC As String,
                                              ByVal pnumEquipos02SecIIIC As Double, ByVal pnumPersonas02SecIIIC As Double, ByVal pnumHoras02SecIIIC As Double, ByVal pnumPrecioUni02SecIIIC As Double,
                                              ByVal pnumPrecioTot02SecIIIC As Double, ByVal pvchEquipos03SecIIIC As String, ByVal pnumEquipos03SecIIIC As Double, ByVal pnumPersonas03SecIIIC As Double,
                                              ByVal pnumHoras03SecIIIC As Double, ByVal pnumPrecioUni03SecIIIC As Double, ByVal pnumPrecioTot03SecIIIC As Double, ByVal pvchEquipos04SecIIIC As String,
                                              ByVal pnumEquipos04SecIIIC As Double, ByVal pnumPersonas04SecIIIC As Double, ByVal pnumHoras04SecIIIC As Double, ByVal pnumPrecioUni04SecIIIC As Double,
                                              ByVal pnumPrecioTot04SecIIIC As Double, ByVal pvchEquipos05SecIIIC As String, ByVal pnumEquipos05SecIIIC As Double, ByVal pnumPersonas05SecIIIC As Double,
                                              ByVal pnumHoras05SecIIIC As Double, ByVal pnumPrecioUni05SecIIIC As Double, ByVal pnumPrecioTot05SecIIIC As Double, ByVal pnumPrecioTotSecIIIC As Double,
                                              ByVal pvchObservacionSecIIIC As String, ByVal pvchUsuario As String, ByVal pintNumSecu As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pBloqueSeccion", pBloqueSeccion, _
                                            "pintCotizacionServicio", pintCotizacionServicio, _
                                            "pvchNumRequi", pvchNumRequi, _
                                            "pvchCodProveedor", pvchCodProveedor, _
                                            "pvchFonoFax", pvchFonoFax, _
                                            "pvchFecEjecTra", pvchFecEjecTra, _
                                            "pvchAtencion", pvchAtencion, _
                                            "pvchArea", pvchArea, _
                                            "pvchFecEjecTer", pvchFecEjecTer, _
                                            "pvchDescripcion01SecI", pvchDescripcion01SecI, _
                                            "pvchZonaTrabajo01SecI", pvchZonaTrabajo01SecI, _
                                            "pnumCantidad01SecI", pnumCantidad01SecI, _
                                            "pnumPrecioUni01SecI", pnumPrecioUni01SecI, _
                                            "pnumPrecioTot01SecI", pnumPrecioTot01SecI, _
                                            "pvchDescripcion02SecI", pvchDescripcion02SecI, _
                                            "pvchZonaTrabajo02SecI", pvchZonaTrabajo02SecI, _
                                            "pnumCantidad02SecI", pnumCantidad02SecI, _
                                            "pnumPrecioUni02SecI", pnumPrecioUni02SecI, _
                                            "pnumPrecioTot02SecI", pnumPrecioTot02SecI, _
                                            "pvchDescripcion03SecI", pvchDescripcion03SecI, _
                                            "pvchZonaTrabajo03SecI", pvchZonaTrabajo03SecI, _
                                            "pnumCantidad03SecI", pnumCantidad03SecI, _
                                            "pnumPrecioUni03SecI", pnumPrecioUni03SecI, _
                                            "pnumPrecioTot03SecI", pnumPrecioTot03SecI, _
                                            "pvchDescripcion04SecI", pvchDescripcion04SecI, _
                                            "pvchZonaTrabajo04SecI", pvchZonaTrabajo04SecI, _
                                            "pnumCantidad04SecI", pnumCantidad04SecI, _
                                            "pnumPrecioUni04SecI", pnumPrecioUni04SecI, _
                                            "pnumPrecioTot04SecI", pnumPrecioTot04SecI, _
                                            "pvchDescripcion05SecI", pvchDescripcion05SecI, _
                                            "pvchZonaTrabajo05SecI", pvchZonaTrabajo05SecI, _
                                            "pnumCantidad05SecI", pnumCantidad05SecI, _
                                            "pnumPrecioUni05SecI", pnumPrecioUni05SecI, _
                                            "pnumPrecioTot05SecI", pnumPrecioTot05SecI, _
                                            "pvchObservacionSecI", pvchObservacionSecI, _
                                            "pnumPrecioTotSecI", pnumPrecioTotSecI, _
                                            "pnumMaterialSecII", pnumMaterialSecII, _
                                            "pnumManoObraSecII", pnumManoObraSecII, _
                                            "pnumEQyServSecII", pnumEQyServSecII, _
                                            "pnumGGyUtilSecII", pnumGGyUtilSecII, _
                                            "pnumPrecioTotSecII", pnumPrecioTotSecII, _
                                            "pvchValidezOfertaSecII", pvchValidezOfertaSecII, _
                                            "pvchPlazoDiasSecII", pvchPlazoDiasSecII, _
                                            "pvchFormaPago", pvchFormaPago, _
                                            "pvchDescripcion01SecIIIA", pvchDescripcion01SecIIIA, _
                                            "pvchMarca01SecIIIA", pvchMarca01SecIIIA, _
                                            "pvchUnidadM01SecIIIA", pvchUnidadM01SecIIIA, _
                                            "pnumCantidad01SecIIIA", pnumCantidad01SecIIIA, _
                                            "pnumPrecioUni01SecIIIA", pnumPrecioUni01SecIIIA, _
                                            "pnumPrecioTot01SecIIIA", pnumPrecioTot01SecIIIA, _
                                            "pvchDescripcion02SecIIIA", pvchDescripcion02SecIIIA, _
                                            "pvchMarca02SecIIIA", pvchMarca02SecIIIA, _
                                            "pvchUnidadM02SecIIIA", pvchUnidadM02SecIIIA, _
                                            "pnumCantidad02SecIIIA", pnumCantidad02SecIIIA, _
                                            "pnumPrecioUni02SecIIIA", pnumPrecioUni02SecIIIA, _
                                            "pnumPrecioTot02SecIIIA", pnumPrecioTot02SecIIIA, _
                                            "pvchDescripcion03SecIIIA", pvchDescripcion03SecIIIA, _
                                            "pvchMarca03SecIIIA", pvchMarca03SecIIIA, _
                                            "pvchUnidadM03SecIIIA", pvchUnidadM03SecIIIA, _
                                            "pnumCantidad03SecIIIA", pnumCantidad03SecIIIA, _
                                            "pnumPrecioUni03SecIIIA", pnumPrecioUni03SecIIIA, _
                                            "pnumPrecioTot03SecIIIA", pnumPrecioTot03SecIIIA, _
                                            "pvchDescripcion04SecIIIA", pvchDescripcion04SecIIIA, _
                                            "pvchMarca04SecIIIA", pvchMarca04SecIIIA, _
                                            "pvchUnidadM04SecIIIA", pvchUnidadM04SecIIIA, _
                                            "pnumCantidad04SecIIIA", pnumCantidad04SecIIIA, _
                                            "pnumPrecioUni04SecIIIA", pnumPrecioUni04SecIIIA, _
                                            "pnumPrecioTot04SecIIIA", pnumPrecioTot04SecIIIA, _
                                            "pvchDescripcion05SecIIIA", pvchDescripcion05SecIIIA, _
                                            "pvchMarca05SecIIIA", pvchMarca05SecIIIA, _
                                            "pvchUnidadM05SecIIIA", pvchUnidadM05SecIIIA, _
                                            "pnumCantidad05SecIIIA", pnumCantidad05SecIIIA, _
                                            "pnumPrecioUni05SecIIIA", pnumPrecioUni05SecIIIA, _
                                            "pnumPrecioTot05SecIIIA", pnumPrecioTot05SecIIIA, _
                                            "pnumPrecioTotSecIIIA", pnumPrecioTotSecIIIA, _
                                            "pvchObservacionSecIIIA", pvchObservacionSecIIIA, _
                                            "pvchEspecialidad01SecIIIB", pvchEspecialidad01SecIIIB, _
                                            "pnumPersonas01SecIIIB", pnumHoras01SecIIIB, _
                                            "pnumHoras01SecIIIB", pnumHoras01SecIIIB, _
                                            "pnumPrecioUni01SecIIIB", pnumPrecioUni01SecIIIB, _
                                            "pnumPrecioTot01SecIIIB", pnumPrecioTot01SecIIIB, _
                                            "pvchEspecialidad02SecIIIB", pvchEspecialidad02SecIIIB, _
                                            "pnumPersonas02SecIIIB", pnumPersonas02SecIIIB, _
                                            "pnumHoras02SecIIIB", pnumHoras02SecIIIB, _
                                            "pnumPrecioUni02SecIIIB", pnumPrecioUni02SecIIIB, _
                                            "pnumPrecioTot02SecIIIB", pnumPrecioTot02SecIIIB, _
                                            "pvchEspecialidad03SecIIIB", pvchEspecialidad03SecIIIB, _
                                            "pnumPersonas03SecIIIB", pnumPersonas03SecIIIB, _
                                            "pnumHoras03SecIIIB", pnumHoras03SecIIIB, _
                                            "pnumPrecioUni03SecIIIB", pnumPrecioUni03SecIIIB, _
                                            "pnumPrecioTot03SecIIIB", pnumPrecioTot03SecIIIB, _
                                            "pvchEspecialidad04SecIIIB", pvchEspecialidad04SecIIIB, _
                                            "pnumPersonas04SecIIIB", pnumPersonas04SecIIIB, _
                                            "pnumHoras04SecIIIB", pnumHoras04SecIIIB, _
                                            "pnumPrecioUni04SecIIIB", pnumPrecioUni04SecIIIB, _
                                            "pnumPrecioTot04SecIIIB", pnumPrecioTot04SecIIIB, _
                                            "pvchEspecialidad05SecIIIB", pvchEspecialidad05SecIIIB, _
                                            "pnumPersonas05SecIIIB", pnumPersonas05SecIIIB, _
                                            "pnumHoras05SecIIIB", pnumHoras05SecIIIB, _
                                            "pnumPrecioUni05SecIIIB", pnumPrecioUni05SecIIIB, _
                                            "pnumPrecioTot05SecIIIB", pnumPrecioTot05SecIIIB, _
                                            "pvchEspecialidad06SecIIIB", pvchEspecialidad06SecIIIB, _
                                            "pnumPersonas06SecIIIB", pnumPersonas06SecIIIB, _
                                            "pnumHoras06SecIIIB", pnumHoras06SecIIIB, _
                                            "pnumPrecioUni06SecIIIB", pnumPrecioUni06SecIIIB, _
                                            "pnumPrecioTot06SecIIIB", pnumPrecioTot06SecIIIB, _
                                            "pvchEspecialidad07SecIIIB", pvchEspecialidad07SecIIIB, _
                                            "pnumPersonas07SecIIIB", pnumPersonas07SecIIIB, _
                                            "pnumHoras07SecIIIB", pnumHoras07SecIIIB, _
                                            "pnumPrecioUni07SecIIIB", pnumPrecioUni07SecIIIB, _
                                            "pnumPrecioTot07SecIIIB", pnumPrecioTot07SecIIIB, _
                                            "pvchEspecialidad08SecIIIB", pvchEspecialidad08SecIIIB, _
                                            "pnumPersonas08SecIIIB", pnumPersonas08SecIIIB, _
                                            "pnumHoras08SecIIIB", pnumHoras08SecIIIB, _
                                            "pnumPrecioUni08SecIIIB", pnumPrecioUni08SecIIIB, _
                                            "pnumPrecioTot08SecIIIB", pnumPrecioTot08SecIIIB, _
                                            "pnumPrecioTotSecIIIB", pnumPrecioTotSecIIIB, _
                                            "pvchObservacionSecIIIB", pvchObservacionSecIIIB, _
                                            "pvchEquipos01SecIIIC", pvchEquipos01SecIIIC, _
                                            "pnumEquipos01SecIIIC", pnumEquipos01SecIIIC, _
                                            "pnumPersonas01SecIIIC", pnumPersonas01SecIIIC, _
                                            "pnumHoras01SecIIIC", pnumHoras01SecIIIC, _
                                            "pnumPrecioUni01SecIIIC", pnumPrecioUni01SecIIIC, _
                                            "pnumPrecioTot01SecIIIC", pnumPrecioTot01SecIIIC, _
                                            "pvchEquipos02SecIIIC", pvchEquipos02SecIIIC, _
                                            "pnumEquipos02SecIIIC", pnumEquipos02SecIIIC, _
                                            "pnumPersonas02SecIIIC", pnumPersonas02SecIIIC, _
                                            "pnumHoras02SecIIIC", pnumHoras02SecIIIC, _
                                            "pnumPrecioUni02SecIIIC", pnumPrecioUni02SecIIIC, _
                                            "pnumPrecioTot02SecIIIC", pnumPrecioTot02SecIIIC, _
                                            "pvchEquipos03SecIIIC", pvchEquipos03SecIIIC, _
                                            "pnumEquipos03SecIIIC", pnumEquipos03SecIIIC, _
                                            "pnumPersonas03SecIIIC", pnumPersonas03SecIIIC, _
                                            "pnumHoras03SecIIIC", pnumHoras03SecIIIC, _
                                            "pnumPrecioUni03SecIIIC", pnumPrecioUni03SecIIIC, _
                                            "pnumPrecioTot03SecIIIC", pnumPrecioTot03SecIIIC, _
                                            "pvchEquipos04SecIIIC", pvchEquipos04SecIIIC, _
                                            "pnumEquipos04SecIIIC", pnumEquipos04SecIIIC, _
                                            "pnumPersonas04SecIIIC", pnumPersonas04SecIIIC, _
                                            "pnumHoras04SecIIIC", pnumHoras04SecIIIC, _
                                            "pnumPrecioUni04SecIIIC", pnumPrecioUni04SecIIIC, _
                                            "pnumPrecioTot04SecIIIC", pnumPrecioTot04SecIIIC, _
                                            "pvchEquipos05SecIIIC", pvchEquipos05SecIIIC, _
                                            "pnumEquipos05SecIIIC", pnumEquipos05SecIIIC, _
                                            "pnumPersonas05SecIIIC", pnumPersonas05SecIIIC, _
                                            "pnumHoras05SecIIIC", pnumHoras05SecIIIC, _
                                            "pnumPrecioUni05SecIIIC", pnumPrecioUni05SecIIIC, _
                                            "pnumPrecioTot05SecIIIC", pnumPrecioTot05SecIIIC, _
                                            "pnumPrecioTotSecIIIC", pnumPrecioTotSecIIIC, _
                                            "pvchObservacionSecIIIC", pvchObservacionSecIIIC, _
                                            "pvchUsuario", pvchUsuario,
                                            "pintNumSecu", pintNumSecu} ',
            '"pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("USP_LOG_ARCHIVO_COTIZACION_SERVICIO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function obtenerRequisitosServicio(ByVal pstrCodProv As String, ByVal pstrNumRequi As String) As DataTable
    Public Function obtenerDatosCotizacion(ByVal pstrValRef As String,
                                           ByVal pstrCodProveedor As String,
                                           ByVal pstrNumRequi As String,
                                           ByVal pintNumSecu As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pvchValRef", pstrValRef,
                                             "pvchCodProveedor", pstrCodProveedor,
                                             "pvchNumRequi", pstrNumRequi,
                                             "pintNumSecu", pintNumSecu} ',
            '"pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("USP_LOG_ARCHIVO_COTIZACION_SERVICIO_OBTENER", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerPostulantesServicio(ByVal pstrNumRequi As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi} ',
            '"pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("USP_LOG_PROCESAR_POSTULACIONES_SERVICIOS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function validarConstanciasProveedor(ByVal pstrCodProv As String,
                                                ByVal pintIDRequisito As Integer,
                                                ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pvchUsuario", pstrUsuario} ',
            '"pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("USP_LOG_VALIDAR_CONSTANCIAS_PROVEEDOR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function validarConstanciasProveedor_2(ByVal pstrCodProv As String,
                                                  ByVal pintIDRequisito As Integer,
                                                  ByVal pstrTipDoc As String,
                                                  ByVal pstrNumDoc As String,
                                                  ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProv", pstrCodProv,
                                             "pintIDRequisito", pintIDRequisito,
                                             "pvchTipDocumento", pstrTipDoc,
                                             "pvchNumeroDocumento", pstrNumDoc,
                                             "pvchUsuario", pstrUsuario} ',
            '"pvchNumRequi", pstrNumRequi}
            Return _objConnexion.ObtenerDataTable("USP_LOG_VALIDAR_CONSTANCIAS_PROVEEDOR_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function obtenerRequisitosServicio_v2(ByVal pstrCodProv As String, ByVal pstrNumRequi As String) As DataTable
    Public Function obtenerConfiguracionRequisitos(ByVal pstrTipBusqueda As String,
                                                   ByVal pstrCodRubro As String,
                                                   ByVal pstrNumRequi As String,
                                                   ByVal pintRequisito As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pvchTipBusqueda", pstrTipBusqueda,
                                             "pvchCodRubro", pstrCodRubro,
                                             "pvchNumRequi", pstrNumRequi,
                                             "pintRequisito", pintRequisito}
            Return _objConnexion.ObtenerDataTable("usp_obtener_valores_configuracion_servicios", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '
    Public Function obtenerDatosConfiguracionRequi(ByVal pstrTipBusqueda As String,
                                                   ByVal pstrNumRequi As String,
                                                   ByVal pintRequisito As Integer) As DataSet
        Try
            Dim objparametros() As Object = {"pvchTipBusqueda", pstrTipBusqueda,
                                             "pvchNumRequi", pstrNumRequi,
                                             "pintIDRequisito", pintRequisito}
            Return _objConnexion.ObtenerDataSet("usp_log_obtener_valores_configurados", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function obtenerProveedorTipTrabajo(ByVal pstrTipBusqueda As String,
                                               ByVal pstrParamBusqueda As String,
                                               ByVal pintIDTipTrabajo As Integer,
                                               ByVal pstrUsuario As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchTipBusqueda", pstrTipBusqueda,
                                             "pvchParamBusqueda", pstrParamBusqueda,
                                             "pintIDTipTrabajo", pintIDTipTrabajo,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataSet("usp_log_proveedor_tip_trabajo", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerDatosCitaInduccionPortal(ByVal pintIDInduccion As Int32) As DataSet
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion}
            Return _objConnexion.ObtenerDataSet("usp_log_obtener_datos_induccion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerDatosCitaInduccionPortal_v2(ByVal pintIDInduccion As Int32) As DataSet
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion}
            Return _objConnexion.ObtenerDataSet("usp_log_obtener_datos_induccion_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function obtenerPaseExpoDenim(ByVal pvchNombres As String,
                                         ByVal pvchApellidos As String,
                                         ByVal pvchNumeroDocumento As String,
                                         ByVal pvchEmail As String,
                                         ByVal pchrFlagLavanderia As String,
                                         ByVal pchrFlagDistribuidor As String,
                                         ByVal pchrFlagConfeccionista As String,
                                         ByVal pchrFlagDisenador As String,
                                         ByVal pchrFlagEstudiante As String,
                                         ByVal pvchOtros As String,
                                         ByVal pdtFecha As String,
                                         ByVal pvchHorario As String) As DataTable
        Try
            Dim objParametros() As Object = {"pvchNombres", pvchNombres,
                                             "pvchApellidos", pvchApellidos,
                                             "pvchNumeroDocumento", pvchNumeroDocumento,
                                             "pvchEmail", pvchEmail,
                                             "pchrFlagLavanderia", pchrFlagLavanderia,
                                             "pchrFlagDistribuidor", pchrFlagDistribuidor,
                                             "pchrFlagConfeccionista", pchrFlagConfeccionista,
                                             "pchrFlagDisenador", pchrFlagDisenador,
                                             "pchrFlagEstudiante", pchrFlagEstudiante,
                                             "pvchOtros", pvchOtros,
                                             "pdtFecha", pdtFecha,
                                             "pvchHorario", pvchHorario}
            Return _objConnexion.ObtenerDataTable("USP_VENTAS_EXPO_DENIM", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerPaseExpoDenim_2(ByVal pvchNombres As String,
                                         ByVal pvchApellidos As String,
                                         ByVal pvchNumeroDocumento As String,
                                         ByVal pvchEmail As String,
                                         ByVal pchrFlagLavanderia As String,
                                         ByVal pchrFlagDistribuidor As String,
                                         ByVal pchrFlagConfeccionista As String,
                                         ByVal pchrFlagDisenador As String,
                                         ByVal pchrFlagEstudiante As String,
                                         ByVal pvchOtros As String,
                                         ByVal pdtFecha As String,
                                         ByVal pvchHorario As String,
                                         ByVal pvchTelefono As String) As DataTable
        Try
            Dim objParametros() As Object = {"pvchNombres", pvchNombres,
                                             "pvchApellidos", pvchApellidos,
                                             "pvchNumeroDocumento", pvchNumeroDocumento,
                                             "pvchEmail", pvchEmail,
                                             "pchrFlagLavanderia", pchrFlagLavanderia,
                                             "pchrFlagDistribuidor", pchrFlagDistribuidor,
                                             "pchrFlagConfeccionista", pchrFlagConfeccionista,
                                             "pchrFlagDisenador", pchrFlagDisenador,
                                             "pchrFlagEstudiante", pchrFlagEstudiante,
                                             "pvchOtros", pvchOtros,
                                             "pdtFecha", pdtFecha,
                                             "pvchHorario", pvchHorario,
                                             "pvchTelefono", pvchTelefono}
            Return _objConnexion.ObtenerDataTable("USP_VENTAS_EXPO_DENIM_2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function generarOSPortalContratista(ByVal pstrNumRequi As String,
                                               ByVal pstrCodProveedor As String,
                                               ByVal pstrPagos As String,
                                               ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumRequi", pstrNumRequi,
                                             "pvchCodProveedor", pstrCodProveedor,
                                             "pvchPagos", pstrPagos,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_LOG_GENERA_OS_PORTAL_CONTRATISTA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function listarOrdenesAprobadas(ByVal pstrCodProveedor As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProveedor}
            Return _objConnexion.ObtenerDataTable("usp_log_registro_personas_servicios", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function operaciones_CRUD_personas_portal(ByVal pstrTipBusqueda As String,
                                                     ByVal pstCodProveedor As String,
                                                     ByVal pstrTipDocumento As String,
                                                     ByVal pstrNumeroDocumento As String,
                                                     ByVal pstrNombres As String,
                                                     ByVal pstrApellidos As String,
                                                     ByVal pstrNumeroMovil As String,
                                                     ByVal pstrEstado As String,
                                                     ByVal pstrUsuario As String,
                                                     ByVal pstrNombreArchivo As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchTipBusqueda", pstrTipBusqueda,
                                             "pvchCodProveedor", pstCodProveedor,
                                             "pvchTipDocumento", pstrTipDocumento,
                                             "pvchNumeroDocumento", pstrNumeroDocumento,
                                             "pvchNombres", pstrNombres,
                                             "pvchApellidos", pstrApellidos,
                                             "pvchNumeroMovil", pstrNumeroMovil,
                                             "pvchEstado", pstrEstado,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchNombreArchivo", pstrNombreArchivo}
            Return _objConnexion.ObtenerDataTable("usp_log_crud_personas_proveedor", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerPersonaProveedorGrilla(ByVal pstrOperacion As String,
                                                  ByVal pstrTipDoc As String,
                                                  ByVal pstrNumDoc As String,
                                                  ByVal pstrCodProveedor As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchOperacion", pstrOperacion,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pvchCodProveedor", pstrCodProveedor}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_personas_x_proveedor", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function generarCabeceraInduccionPortal(ByVal pstrNumOrdServ As String,
                                                   ByVal pstrFechaInduccion As String,
                                                   ByVal pstrHoraInduccion As String,
                                                   ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumOrdServ", pstrNumOrdServ,
                                             "pvchFechaInduccion", pstrFechaInduccion,
                                             "pvchHoraInduccion", pstrHoraInduccion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_genera_cita_induccion_cab", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function generarDetalleInduccionPortal(ByVal pintIDInduccion As Int32,
                                                  ByVal pstrCodProveedor As String,
                                                  ByVal pstrTipDoc As String,
                                                  ByVal pstrNumDoc As String,
                                                  ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchCodProveedor", pstrCodProveedor,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_genera_cita_induccion_det", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerCitasInduccionPortal(ByVal pstrNumOrdServ As String,
                                                ByVal pstrCodProveedor As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchOrdServ", pstrNumOrdServ,
                                             "pvchCodProveedor", pstrCodProveedor}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_cita_induccion_x_OS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function anularCitasInduccionPortal(ByVal pintIDInduccion As Int32) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion}
            Return _objConnexion.ObtenerDataTable("usp_log_anular_cita_induccion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerListaCitaInduccionxFecha(ByVal pvchFecha As String,
                                                    ByVal pvchEstado As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchFecha", pvchFecha,
                                             "pvchEstado", pvchEstado}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_cita_induccion_x_fecha", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ingresarPersonasCitaInduccion(ByVal pintIDInduccion As Int32,
                                                  ByVal pstrTipDoc As String,
                                                  ByVal pstrNumDoc As String,
                                                  ByVal pstrAsistio As String,
                                                  ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pvchAsistio", pstrAsistio,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_validar_ingreso_cita_induccion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function programarPersonasCitaInduccion(ByVal pintIDInduccion As Int32,
                                                  ByVal pstrTipDoc As String,
                                                  ByVal pstrNumDoc As String,
                                                  ByVal pstrTipInduccion As String,
                                                  ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pvchTipInduccion", pstrTipInduccion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_programar_personas_induccion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function calificarPersonasCitaInduccion(ByVal pintIDInduccion As Int32,
                                                   ByVal pstrTipDoc As String,
                                                   ByVal pstrNumDoc As String,
                                                   ByVal pdblNotam As Double,
                                                   ByVal pstrObservacion As String,
                                                   ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pnumNota", pdblNotam,
                                             "pvchObservacion", pstrObservacion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_calificar_personas_induccion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function calificarPersonasCitaInduccion_v2(ByVal pintIDInduccion As Int32,
                                                      ByVal pstrTipDoc As String,
                                                      ByVal pstrNumDoc As String,
                                                      ByVal pdblNotam As Double,
                                                      ByVal pstrObservacion As String,
                                                      ByVal pstrUltCalificacion As String,
                                                      ByVal pintUltIDTipInduccion As Integer,
                                                      ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc,
                                             "pnumNotaInduccion", pdblNotam,
                                             "pvchObservacion", pstrObservacion,
                                             "pvchUltCalificacion", pstrUltCalificacion,
                                             "pintUltIDTipInduccion", pintUltIDTipInduccion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_calificar_personas_induccion_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function terminarCalificarCitaInduccion(ByVal pintIDInduccion As Int32,
                                                   ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_finalizar_personas_induccion", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function terminarCalificarCitaInduccion_2(ByVal pintIDInduccion As Int32,
                                                   ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDInduccion", pintIDInduccion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_log_finalizar_personas_induccion_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function mostrarComentario(ByVal pintIDInduccion As Int32,
                                      ByVal pstrCodProv As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDRequisito", pintIDInduccion,
                                             "pvchCodProv", pstrCodProv}
            Return _objConnexion.ObtenerDataTable("usp_log_mostrar_comentario_rechazo", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function mostrarComentario_2(ByVal pintIDInduccion As Int32,
                                        ByVal pstrCodProv As String,
                                        ByVal pstrTipDoc As String,
                                        ByVal pstrNumDoc As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDRequisito", pintIDInduccion,
                                             "pvchCodProv", pstrCodProv,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc}
            Return _objConnexion.ObtenerDataTable("usp_log_mostrar_comentario_rechazo_2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerFechasDisponiblesPersona(ByVal pstrCodProv As String,
                                                    ByVal pstrTipDoc As String,
                                                    ByVal pstrNumDoc As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv,
                                             "pvchTipDocumento", pstrTipDoc,
                                             "pvchNumeroDocumento", pstrNumDoc}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_fechas_historial_persona_trabajos", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerHistorialCertificadosPersona(ByVal pstrCodProv As String,
                                                        ByVal pstrTipDoc As String,
                                                        ByVal pstrNumDoc As String,
                                                        ByVal pstrFechaBusqueda As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv,
                                             "pvchTipDocumento", pstrTipDoc,
                                             "pvchNumeroDocumento", pstrNumDoc,
                                             "pvchFechaBusqueda", pstrFechaBusqueda}
            Return _objConnexion.ObtenerDataTable("usp_log_obtener_requisitos_x_rubro_v5", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerDatosOrdenServicio(ByVal pstrOrdServ As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchOrdServ", pstrOrdServ}
            Return _objConnexion.ObtenerDataSet("usp_log_obtener_datos_orden_servicio", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerDatosPersonaxProve(ByVal pstrCodProv As String,
                                              ByVal pstrTipDoc As String,
                                              ByVal pstrNumDoc As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProv,
                                             "pvchTipDocumento", pstrTipDoc,
                                             "pvchNumeroDocumento", pstrNumDoc}
            Return _objConnexion.ObtenerDataTable("usp_log_obtenerDatosPersonasxProv", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function enviarVisitaTecnica(ByVal pstrNumRequi As String,
                                        ByVal pstrCodProv As String,
                                        ByVal pstrFecha As String,
                                        ByVal pstrHora As String,
                                        ByVal pstrMotivo As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_NumRequisicion", pstrNumRequi,
                                             "pvch_CodProv", pstrCodProv,
                                             "pvch_Fecha", pstrFecha,
                                             "pvch_Hora", pstrHora,
                                             "pvch_Motivo", pstrMotivo}
            Return _objConnexion.ObtenerDataTable("USP_LOG_ENVIO_REQUISICION_VISITA_TECNICA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function validarDisponibilidadPersonas(ByVal pstrOrdServ As String,
                                                  ByVal pstrCodProv As String,
                                                  ByVal pstrTipDoc As String,
                                                  ByVal pstrNumDoc As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchOrdServ", pstrOrdServ,
                                             "pvchCodProv", pstrCodProv,
                                             "pvchTipDoc", pstrTipDoc,
                                             "pvchNumDoc", pstrNumDoc}
            Return _objConnexion.ObtenerDataTable("USP_LOG_VALIDAR_PERSONAS_SERVICIOS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function actualizarPrecioBase2daCostos(ByVal pstrTipBusqueda As String,
                                                  ByVal pstrCodVersion As String,
                                                  ByVal pstrCodGrupo As String,
                                                  ByVal pdblValor1 As Double,
                                                  ByVal pdblValor2 As Double,
                                                  ByVal pdblValor3 As Double,
                                                  ByVal pstrUsuario As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchTipBusqueda", pstrTipBusqueda,
                                             "pvchCodVersion", pstrCodVersion,
                                             "pvchCodGrupo", pstrCodGrupo,
                                             "pnumValor1", pdblValor1,
                                             "pnumValor2", pdblValor2,
                                             "pnumValor3", pdblValor3,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataSet("USP_LOG_ACTUALIZAR_PRECIO_BASE2DA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function actualizarPrecioBase2daCostos_V2(ByVal pstrTipBusqueda As String,
                                                      ByVal pstrCodVersion As String,
                                                      ByVal pstrCodGrupo As String,
                                                      ByVal pdblValor1 As Double,
                                                      ByVal pdblValor2 As Double,
                                                      ByVal pdblValor3 As Double,
                                                      ByVal pstrLinea As String,
                                                      ByVal pstrUsuario As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchTipBusqueda", pstrTipBusqueda,
                                             "pvchCodVersion", pstrCodVersion,
                                             "pvchCodGrupo", pstrCodGrupo,
                                             "pnumValor1", pdblValor1,
                                             "pnumValor2", pdblValor2,
                                             "pnumValor3", pdblValor3,
                                             "pvchLinea", pstrLinea,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataSet("USP_LOG_ACTUALIZAR_PRECIO_BASE2DA_V2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function cargaAdjuntosProveedores(ByVal pstrOperacion As String,
                                             ByVal pintNumSecu As Int32,
                                             ByVal pstrCodProv As String,
                                             ByVal pstrNumRequi As String,
                                             ByVal pstrDescripArchivo As String,
                                             ByVal pstrArchivoNombre As String,
                                             ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchOperacion", pstrOperacion,
                                             "pintNumSecu", pintNumSecu,
                                             "pvchCodProveedor", pstrCodProv,
                                             "pvchNumRequisicion", pstrNumRequi,
                                             "pvchDescripArchivo", pstrDescripArchivo,
                                             "pvchArchivoNombre", pstrArchivoNombre,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_LOG_CARGA_ADJUNTOS_PROVEEDORES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function registrarAcuse(ByVal pintProceso As Int32,
                                   ByVal pintAnio As Int32,
                                   ByVal pintMes As Int32,
                                   ByVal pstrCoTrab As String,
                                   ByVal pstrRutaArchivo As String) As DataTable
        Try
            Dim objparametros() As Object = {"P_INT_PROCESO", pintProceso,
                                             "P_INT_ANIO", pintAnio,
                                             "P_INT_MES ", pintMes,
                                             "P_CO_TRAB", pstrCoTrab,
                                             "P_RUTA_ARCHIVO", pstrRutaArchivo}
            Return _objConnexion.ObtenerDataTable("USP_RRHH_REGITRAR_ACUSE_RECIBO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function registrarLecturaBoletas(ByVal pvchTip As String,
                                            ByVal pintAnio As Int32,
                                            ByVal pintMes As Int32,
                                            ByVal pvchCoTrab As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchTip", pvchTip,
                                             "pintAnio", pintAnio,
                                             "pintMes ", pintMes,
                                             "pvchCoTrab", pvchCoTrab}
            Return _objConnexion.ObtenerDataTable("usp_log_seguimiento_boleta_digital_lectura", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Clientes - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaCliente(ByVal strCodigoProveedor As String,
                                       ByVal dtmFechaCita As Date,
                                       ByVal strHoraCita As String,
                                       ByVal strRangoCita As String,
                                       ByVal strUsuario As String,
                                       ByVal dtListaArticulosCita As DataTable,
                                       ByVal strFlagMontaCarga As String,
                                       ByVal strCamion As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga,
                                             "pvch_Camion", strCamion}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_CLIENTE", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Cita para Logistica Clientes - Extranet (Modificado)
    'Autor      : Alessandro Ampuero Peña
    'Creado     : 13/03/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function GenerarCitaServLog(ByVal strCodigoProveedor As String,
                                       ByVal dtmFechaCita As Date,
                                       ByVal strHoraCita As String,
                                       ByVal strRangoCita As String,
                                       ByVal strUsuario As String,
                                       ByVal dtListaArticulosCita As DataTable,
                                       ByVal strFlagMontaCarga As String,
                                       ByVal strCamion As String) As String
        Dim objUtil As New Util

        Try
            dtListaArticulosCita.TableName = "Articulos"
            Dim strListaArticulosCitaXML As New StringBuilder(objUtil.GeneraXml(dtListaArticulosCita))
            Dim objParametros As Object() = {"pvch_CodigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaCita", dtmFechaCita,
                                             "pvch_HoraCita", strHoraCita,
                                             "pvch_RangoCita", strRangoCita,
                                             "pvch_Usuario", strUsuario,
                                             "pxml_ListaArticulosCita", strListaArticulosCitaXML.ToString,
                                             "pvch_flagMontacarga", strFlagMontaCarga,
                                             "pvch_Camion", strCamion}

            Return _objConnexion.ObtenerValor("USP_LOG_EXTRANET_GENERA_CITA_SL", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ufn_ObtenerSalidasFactura(ByVal pstrNumDocu As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumDocu", pstrNumDocu}
            Return _objConnexion.ObtenerDataTable("USP_LOG_BUSCAR_SALIDA_FACTURA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarCitasPendientes_v3(ByVal pstrNumDocu As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchNumFactura", pstrNumDocu}
            Return _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_CITAS_PENDIENTES_v3", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_listasServiciosLosgisticos(ByVal pstrTipOper As String,
                                                  ByVal pstrCodEmpr As String,
                                                  ByVal pstrCodOrig As String,
                                                  ByVal pstrCodDest As String,
                                                  ByVal pstrCodVTar As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchTipOper", pstrTipOper,
                                             "pvchCodEmpr", pstrCodEmpr,
                                             "pvchCodOrig", pstrCodOrig,
                                             "pvchCodDest", pstrCodDest,
                                             "pvchCodVTar", pstrCodVTar}
            Return _objConnexion.ObtenerDataTable("USP_LOG_SL_OPERACIONES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_listadoServiciosLogisticos(ByVal pstrCodProveedor As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodProveedor", pstrCodProveedor}
            Return _objConnexion.ObtenerDataTable("USP_LOG_SL_LISTADO_SERVICIOS_LOGISTICOS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_decriptarNumDocu(ByVal pstrNumFactura As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchParam", pstrNumFactura}
            Return _objConnexion.ObtenerDataTable("usp_log_decriptarParam", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ufn_ObtenerDetalleRequer_OS(ByVal strTipDocu As String,
                                                ByVal strNumDocu As String) As DataTable
        Try
            Dim objParametros() As Object = {"pvch_TipDocu", strTipDocu,
                                             "pvch_NumDocu", strNumDocu}
            Return _objConnexion.ObtenerDataTable("USP_LOG_EXTRANET_DETALLE_NUM_DOCU_CITA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CORONAVIRUS"

    Public Function obtenerPreguntasPaseIngreso() As DataTable
        Try
            Return _objConnexion.ObtenerDataTable("usp_log_obtenerPreguntasPaseIngreso")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function registrarPaseIngresoNM(ByVal strNombreCompleto As String, ByVal strEmail As String, ByVal strFechaInicioVisita As String, ByVal strFechaFinVisita As String, ByVal strPersonaVisita As String,
                                           ByVal strTipoDocumento As String, ByVal strDocumento As String, ByVal strRuc As String, ByVal strEmpresa As String, ByVal strUsuario As String, ByVal strObservaciones3 As String,
                                           ByVal strCodPregunta1 As String, ByVal strCodPregunta2 As String, ByVal strCodPregunta3 As String, ByVal strCodPregunta4 As String, ByVal strCodPregunta5 As String,
                                           ByVal strRespuesta1 As String, ByVal strRespuesta2 As String, ByVal strRespuesta3 As String, ByVal strRespuesta4 As String, ByVal strRespuesta5 As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_NombreCompleto", strNombreCompleto,
                                            "pvch_Email", strEmail,
                                            "pvch_FechaInicioVisita", strFechaInicioVisita,
                                            "pvch_FechaFinVisita", strFechaFinVisita,
                                            "pvch_PersonaVisita", strPersonaVisita,
                                            "pvch_TipoDocumento", strTipoDocumento,
                                            "pvch_Documento", strDocumento,
                                            "pvch_Ruc", strRuc,
                                            "pvch_Empresa", strEmpresa,
                                            "pvch_Usuario", strUsuario,
                                            "pvch_Observaciones3", strObservaciones3,
                                            "pvch_CodPregunta1", strCodPregunta1,
                                            "pvch_CodPregunta2 ", strCodPregunta2,
                                            "pvch_CodPregunta3", strCodPregunta3,
                                            "pvch_CodPregunta4 ", strCodPregunta4,
                                            "pvch_CodPregunta5", strCodPregunta5,
                                            "pvch_Respuesta1", strRespuesta1,
                                            "pvch_Respuesta2", strRespuesta2,
                                            "pvch_Respuesta3 ", strRespuesta3,
                                            "pvch_Respuesta4", strRespuesta4,
                                            "pvch_Respuesta5", strRespuesta5}

            Return _objConnexion.ObtenerDataTable("usp_log_registrarPaseIngresoNM", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerPaseIngresoNM(ByVal intIDPaseIngresoNM As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pintIDPaseIngresoNM", intIDPaseIngresoNM}

            Return _objConnexion.ObtenerDataTable("usp_log_obtenerPaseIngresoNM", objparametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function enviarCorreoPaseIngresoNM(ByVal intIDPaseIngresoNM As Integer, ByVal strEstado As String, ByVal strRutaDir As String, ByVal strFilePDF As String) As Integer
        Try
            Dim objparametros() As Object = {"pintIDPaseIngresoNM", intIDPaseIngresoNM,
                                             "pvch_Estado", strEstado,
                                             "pvch_RutaDir", strRutaDir,
                                             "pvch_FilePDF", strFilePDF}

            Return _objConnexion.EjecutarComando("usp_log_enviarConfirmacionPaseIngresoNM", objparametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerListadoPaseIngresoNM(ByVal strEstado As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_Estado", strEstado}

            Return _objConnexion.ObtenerDataTable("usp_log_obtenerListadoPaseIngresoNM", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function actualizarEnviarPaseIngresoNM(ByVal intIDPaseIngresoNM As Integer, ByVal strEstado As String, ByVal strRutaDir As String, ByVal strFilePDF As String, ByVal strUsuario As String) As Integer
        Try
            Dim objparametros() As Object = {"pintIDPaseIngresoNM", intIDPaseIngresoNM,
                                             "pvch_Estado", strEstado,
                                             "pvch_RutaDir", strRutaDir,
                                             "pvch_FilePDF", strFilePDF,
                                             "pvch_Usuario", strUsuario}

            Return _objConnexion.EjecutarComando("usp_log_actualizarEnviarPaseIngresoNM", objparametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region " Dispose "
    Public Sub Dispose() Implements System.IDisposable.Dispose
        _objConnexion.Dispose()
    End Sub
#End Region

End Class
