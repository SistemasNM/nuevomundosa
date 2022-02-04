Option Strict On
Imports System.Data
Imports NM_General
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Cliente
        Implements IDisposable
        Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer
        Sub New()
            m_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
        Public Function ObtenerMotivosReclamo() As DataTable
            Dim dtblMotivosReclamo As DataTable
            Try
                dtblMotivosReclamo = m_sqlDtAccRevisionFinal.ObtenerDataTable("UP_ObtenerMotivosReclamosCliente")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblMotivosReclamo
        End Function
        Public Function ObtenerMotivosComerciales() As DataTable
            Dim dtblMotivosReclamo As DataTable
            Try
                dtblMotivosReclamo = m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_RVF_MOSTRAR_MOTIVOS_COMERCIALES")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblMotivosReclamo
        End Function

        Public Function ObtenerDefectosCalidad() As DataTable
            Dim dtblMotivosReclamo As DataTable
            Try
                dtblMotivosReclamo = m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_ObtenerDefectosCalidad")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblMotivosReclamo
        End Function
        Public Function ObtenerListadoDefectosInformeCalidadxSeccion() As DataTable
            Dim dtblMotivosReclamo As DataTable
            Try
                dtblMotivosReclamo = m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_OBTENER_LISTADO_DEFECTO_INFORME_CALIDAD_X_SECCION")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblMotivosReclamo
        End Function
        Public Function ObtenerListadoDefectosInformeCalidad(ByVal pStrDescripcionDefecto As String, ByVal pStrCodigo_seccion As String) As DataTable
            Dim dtblMotivosReclamo As DataTable
            Dim objParametros() As Object = {"PDescripcion_defecto", pStrDescripcionDefecto, "PCodigo_seccion", pStrCodigo_seccion}
            Try
                dtblMotivosReclamo = m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_OBTENER_LISTADO_DEFECTO_INFORME_CALIDAD", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblMotivosReclamo
        End Function
        Public Function DescripcionMotivo(ByVal pMotivo As String) As String
            Dim pResultado As String
            '*****************************************************************************************************
            'Objetivo   : Mostrar el valor Seleccione en la grilla
            'Autor      : Jaime Víctor Durand Dorner.
            'Creado     : 18/04/2017
            '*****************************************************************************************************
            If pMotivo = "0" Then
                pMotivo = "68"
            End If
            Dim objParametros() As Object = {"codigo_motivo", pMotivo}
            Try
                pResultado = m_sqlDtAccRevisionFinal.ObtenerValor("UP_ObtenerMotivosPorCodigo", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function

        Public Function DescripcionMotivoComercial(ByVal StrvarchCodigo As String) As String
            Dim pResultado As String
            '*****************************************************************************************************
            'Objetivo   : Mostrar el valor Seleccione en la grilla
            'Autor      : Jaime Víctor Durand Dorner.
            'Creado     : 18/04/2017
            '*****************************************************************************************************        
            Dim objParametros() As Object = {"pvarchCodigo", StrvarchCodigo}
            Try
                pResultado = m_sqlDtAccRevisionFinal.ObtenerValor("USP_RVF_MOSTRAR_DETALLE_MOTIVOS_COMERCIALES", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function
        Public Function Obtener() As DataTable
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("UP_ObtenerClientes")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerId(ByVal pCliente As String) As String
            Dim objParametros() As Object = {"codigo_cliente", pCliente}
            Try
                Return Convert.ToString(m_sqlDtAccRevisionFinal.ObtenerValor("pr_NM_Cliente_ObtenerId", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevisionFinal.Dispose()
        End Sub
        Public Function Listado_Articulos_Cliente(ByVal strCliente As String, ByVal strArticulo As String, ByVal strColor As String) As String
            Dim objParametros() As Object = {"VC_CLIENTE", strCliente}
            Try
                Return Convert.ToString(m_sqlDtAccRevisionFinal.ObtenerValor("SP_ListaArticulosDeCliente", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function List(ByVal strCliente As String, ByVal strNumeroFactura As String, ByVal strNumeroGuia As String, ByVal strNumeroRollo As String) As DataTable
            Dim objParametros() As Object = {"VC_CLIENTE", strCliente, "NumeroFactura", strNumeroFactura, "NumeroGuia", strNumeroGuia, "NumeroRollo", strNumeroRollo}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_VEN_ARTICULOS_CLIENTE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Insertar_Reclamo_Cliente(ByVal dtm_FechaReclamo As String, _
                                                 ByVal var_CodigoCliente As String, _
                                                 ByVal var_CodigoVendedor As String, _
                                                 ByVal var_Observaciones As String, _
                                                 ByVal Var_Concluciones As String, _
                                                 ByVal var_usuarioCreacion As String, _
                                                 ByVal var_usuarioModificacion As String, _
                                                 ByVal strDatatatable As DataTable, _
                                                 ByVal chr_EstadoReclamo As String, _
                                                 ByVal int_Reclamo As Integer) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NM_General.Util
            Dim lstrXML As String
            strDatatatable.TableName = "DTRECLAMO"
            lstrXML = lobjUtil.GeneraXml(strDatatatable)
            Dim larrParams() As Object = {"dtm_FechaReclamo", dtm_FechaReclamo, _
                                            "var_CodigoCliente", var_CodigoCliente, _
                                            "var_CodigoVendedor", var_CodigoVendedor, _
                                            "var_Observaciones", var_Observaciones, _
                                            "Var_Concluciones", Var_Concluciones, _
                                            "var_usuarioCreacion", var_usuarioCreacion, _
                                            "var_usuarioModificacion", var_usuarioModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "chr_EstadoReclamo", chr_EstadoReclamo, _
                                            "id_Reclamo", int_Reclamo}

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Return lobjCon.ObtenerDataTable("USP_RVF_INSERTA_RECLAMO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            lobjCon = Nothing
        End Function

        'Modificado: Enviamos fecha de modificacion
        'Alexander Torres Cardenas
        '26/04/2012
        Public Function Actualizar_Reclamo_Cliente(ByVal int_NumeroReclamo As String, _
                                                 ByVal dtm_FechaReclamo As String, _
                                                 ByVal var_CodigoCliente As String, _
                                                 ByVal var_CodigoVendedor As String, _
                                                 ByVal var_Observaciones As String, _
                                                 ByVal Var_Concluciones As String, _
                                                 ByVal var_usuarioCreacion As String, _
                                                 ByVal var_usuarioModificacion As String, _
                                                 ByVal strDatatatable As DataTable, _
                                                 ByVal chr_EstadoReclamo As String, _
                                                 ByVal dtm_FechaRegistro As String) As Integer

            Dim lobjUtil As New NM_General.Util
            Dim lstrXML As String
            strDatatatable.TableName = "DTRECLAMO"
            lstrXML = lobjUtil.GeneraXml(strDatatatable)
            Dim larrParams() As Object = {"int_NumeroReclamo", int_NumeroReclamo, _
                                            "dtm_FechaReclamo", dtm_FechaReclamo, _
                                            "var_CodigoCliente", var_CodigoCliente, _
                                            "var_CodigoVendedor", var_CodigoVendedor, _
                                            "var_Observaciones", var_Observaciones, _
                                            "Var_Concluciones", Var_Concluciones, _
                                            "var_usuarioCreacion", var_usuarioCreacion, _
                                            "var_usuarioModificacion", var_usuarioModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "chr_EstadoReclamo", chr_EstadoReclamo, _
                                            "var_FechaModificacion", dtm_FechaRegistro}

            Try
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZA_RECLAMO_1", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Actualiza_Estado_Reclamo(ByVal strEstado As String, ByVal strNumeroreclamo As Integer) As Integer
            Try
                Dim larrParams() As Object = {"chr_EstadoReclamo", strEstado, _
                                             "int_Reclamo", strNumeroreclamo}
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ESTADO_RECLAMO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Actualiza_condicion_reclamo(ByVal strNumeroReclamo As Integer, ByVal strNumeSecuencia As Integer, ByVal strConclusiones As String, ByVal strMetros As Double, ByVal strSituacion As String, ByVal strUsuario As String) As Integer
            Try
                Dim larrParams() As Object = {"int_Reclamo", strNumeroReclamo, _
                                             "int_Secuencia", strNumeSecuencia, _
                                             "Var_Concluciones", strConclusiones, _
                                             "num_MetrosAceptados", strMetros, _
                                             "chr_Condicion", strSituacion, _
                                             "var_usuarioModificacion", strUsuario}
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_CONDICION_RECLAMO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Email_Reclamos(ByVal strValor As String) As DataTable
            Try
                Dim larrParams() As Object = {"strValor", strValor}
                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_RVF_EMAIL_RECLAMO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Lista_Reclamo(ByVal strNumeroReclamo As Integer) As DataSet
            Dim objParametros() As Object = {"Id_Reclamo", strNumeroReclamo}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("USP_RVF_MUESTRA_RECLAMO_DETALLE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Lista_Reclamo_2(ByVal VC_FECHA_INI As String, ByVal VC_FECHA_FIN As String, ByVal NU_MOTIVO As String, ByVal VC_CLIENTE As String, ByVal VC_ARTICULO As String, ByVal VC_ESTADORECLAMO As String, ByVal VC_TipoFechas As String) As DataSet

            Dim objParametros() As Object = { _
                                            "VC_FECHA_INI", VC_FECHA_INI, _
                                            "VC_FECHA_FIN", VC_FECHA_FIN, _
                                            "NU_MOTIVO", NU_MOTIVO, _
                                            "VC_CLIENTE", VC_CLIENTE, _
                                            "VC_ARTICULO", VC_ARTICULO, _
                                            "VC_ESTADORECLAMO", VC_ESTADORECLAMO, _
                                            "VC_TipoFechas", VC_TipoFechas}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("USP_RVF_LISTARECLAMOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_obtenerCabeceraReclamo(ByVal strNumeroReclamo As Integer) As DataTable
            Dim objParametros() As Object = {"Id_Reclamo", strNumeroReclamo}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_rvf_obtenerReclamoCabecera", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_obtenerDetalleReclamo(ByVal strNumeroReclamo As Integer) As DataTable
            Dim objParametros() As Object = {"Id_Reclamo", strNumeroReclamo}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("usp_rvf_obtenerReclamoDetalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'De aqui para adelante metodos cambiados para desarrollo
        Public Function ufn_Lista_Reclamo_D(ByVal strNumeroReclamo As Integer) As DataSet
            Dim objParametros() As Object = {"Id_Reclamo", strNumeroReclamo}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("USP_RVF_MUESTRA_RECLAMO_DETALLE_D", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_Actualizar_Reclamo_Cliente_D(ByVal int_NumeroReclamo As String, _
                                                 ByVal dtm_FechaReclamo As String, _
                                                 ByVal dtm_FechaVisitaTentativa As String, _
                                                 ByVal var_CodigoCliente As String, _
                                                 ByVal var_CodigoVendedor As String, _
                                                 ByVal var_NombreContacto As String, _
                                                 ByVal var_TelefonoContacto As String, _
                                                 ByVal var_DireccionVisita As String, _
                                                 ByVal var_Observaciones As String, _
                                                 ByVal var_usuarioModificacion As String, _
                                                 ByVal strDatatatable As DataTable, _
                                                 ByVal chr_EstadoReclamo As String, _
                                                 ByVal var_TipoReclamo As String) As Integer

            Dim lobjUtil As New NM_General.Util
            Dim lstrXML As String
            strDatatatable.TableName = "DTRECLAMO"
            lstrXML = lobjUtil.GeneraXml(strDatatatable)
            Dim larrParams() As Object = {"pint_NumeroReclamo", int_NumeroReclamo, _
                                            "pdtm_FechaReclamo", dtm_FechaReclamo, _
                                            "pdtm_FechaVisitaTentativa", dtm_FechaVisitaTentativa, _
                                            "pvar_CodigoCliente", var_CodigoCliente, _
                                            "pvar_CodigoVendedor", var_CodigoVendedor, _
                                            "pvar_NombreContacto", var_NombreContacto, _
                                            "pvar_TelefonoContacto", var_TelefonoContacto, _
                                            "pvar_DireccionVisita", var_DireccionVisita, _
                                            "pvar_Observaciones", var_Observaciones, _
                                            "pvar_usuarioModificacion", var_usuarioModificacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "pchr_EstadoReclamo", chr_EstadoReclamo, _
                                            "pvar_TipoReclamo", var_TipoReclamo
                                          }

            Try
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZA_RECLAMO_D_V3", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_Insertar_Reclamo_Cliente_D(ByVal dtm_FechaReclamo As String, _
                                                 ByVal dtm_FechaVisitaTentativa As String, _
                                                 ByVal var_CodigoCliente As String, _
                                                 ByVal var_CodigoVendedor As String, _
                                                 ByVal var_NombreContacto As String, _
                                                 ByVal var_TelefonoContacto As String, _
                                                 ByVal var_DireccionVisita As String, _
                                                 ByVal var_Observaciones As String, _
                                                 ByVal var_usuarioCreacion As String, _
                                                 ByVal strDatatatable As DataTable, _
                                                 ByVal chr_EstadoReclamo As String, _
                                                 ByVal var_TipoReclamo As String, _
                                                 ByVal int_Reclamo As Integer) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As New NM_General.Util
            Dim lstrXML As String
            strDatatatable.TableName = "DTRECLAMO"
            lstrXML = lobjUtil.GeneraXml(strDatatatable)
            Dim larrParams() As Object = {"pdtm_FechaReclamo", dtm_FechaReclamo, _
                                            "pdtm_FechaVisitaTentativa", dtm_FechaVisitaTentativa, _
                                            "pvar_CodigoCliente", var_CodigoCliente, _
                                            "pvar_CodigoVendedor", var_CodigoVendedor, _
                                            "pvar_NombreContacto", var_NombreContacto, _
                                            "pvar_TelefonoContacto", var_TelefonoContacto, _
                                            "pvar_DireccionVisita", var_DireccionVisita, _
                                            "pvar_Observaciones", var_Observaciones, _
                                            "pvar_usuarioCreacion", var_usuarioCreacion, _
                                            "P_NTX_XML", lstrXML, _
                                            "pchr_EstadoReclamo", chr_EstadoReclamo, _
                                            "pvar_TipoReclamo", var_TipoReclamo, _
                                            "pid_Reclamo", int_Reclamo}

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Return lobjCon.ObtenerDataTable("USP_RVF_INSERTA_RECLAMO_D_V3", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            lobjCon = Nothing
        End Function
    

        Public Function ufn_ActualizarDatosSolicitudVisitaCalidad(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal strEstadoEnvioCorreoSolicitudVisitaCalidad As String, ByVal dtmFechaEnvioCorreoSolicitudCalidad As DateTime) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pvar_EstadoEnvioCorreoSolicitudVisitaCalidad", strEstadoEnvioCorreoSolicitudVisitaCalidad, _
                                                "pdtm_FechaEnvioCorreoSolicitudVisitaCalidad", dtmFechaEnvioCorreoSolicitudCalidad _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_DATOS_SOLICITUD_VISITA_CALIDAD", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_AnularReclamo(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal strConcluciones As String, ByVal strUsuarioModificacion As String, ByVal dtmFechaModificacion As DateTime, ByVal pdtmFechaAnulacion As DateTime) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pvar_Concluciones", strConcluciones, _
                                                "pvar_usuarioModificacion", strUsuarioModificacion, _
                                                "pdtm_FechaModificacion", dtmFechaModificacion, _
                                                "pdtm_FechaAnulacion", pdtmFechaAnulacion _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ANULAR_RECLAMO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Lista_Reclamo_D(ByVal VC_FECHA_INI As String, ByVal VC_FECHA_FIN As String, ByVal NU_MOTIVO As String, ByVal VC_CLIENTE As String, ByVal VC_ARTICULO As String, ByVal VC_ESTADORECLAMO As String, ByVal VC_TipoFechas As String) As DataSet

            Dim objParametros() As Object = { _
                                            "VC_FECHA_INI", VC_FECHA_INI, _
                                            "VC_FECHA_FIN", VC_FECHA_FIN, _
                                            "NU_MOTIVO", NU_MOTIVO, _
                                            "VC_CLIENTE", VC_CLIENTE, _
                                            "VC_ARTICULO", VC_ARTICULO, _
                                            "VC_ESTADORECLAMO", VC_ESTADORECLAMO, _
                                            "VC_TipoFechas", VC_TipoFechas}
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("USP_RVF_LISTARECLAMOS_D", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarDatosAprobacionComecial(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal strEstadoEnvioCorreoSolicitudAprobacionComercial As String, ByVal dtmFechaEnvioCorreoSolicitudAprobacionComercial As DateTime) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pvar_EstadoEnvioCorreoSolicitudAprobacionComercial", strEstadoEnvioCorreoSolicitudAprobacionComercial, _
                                                "pdtm_FechaEnvioCorreoSolicitudAprobacionComercial", dtmFechaEnvioCorreoSolicitudAprobacionComercial _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_DATOS_SOLICITUD_APROBACION_COMERCIAL", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarReclamoDatosNotaDevolucion(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal pdtmFechaActualizacionAlmacen As DateTime) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pdtm_FechaActualizacionAlmacen", pdtmFechaActualizacionAlmacen _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_RECLAMO_DATOS_NOTA_DEVOLUCION", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarReclamoAprobacionGerenciaComercial(ByVal intNumeroreclamo As Integer, ByVal strEstadoReclamo As String, ByVal dtmFechaAprobacionGerenciaComercial As DateTime, ByVal strUsuarioAprobacionGerenciaComercial As String) As Integer
            Dim int_CantidadFilasAfectadas As Integer = 0
            Try
                Dim larrParams() As Object = {"pint_Reclamo", intNumeroreclamo, _
                                              "pvar_EstadoReclamo", strEstadoReclamo, _
                                              "pdtm_FechaAprobacionGerenciaComercial", dtmFechaAprobacionGerenciaComercial, _
                                               "pvar_usuarioAprobacionGerenciaComercial", strUsuarioAprobacionGerenciaComercial
                                             }
                int_CantidadFilasAfectadas = m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_RECLAMO_APROBACION_GERENCIA_COMERCIAL_V2", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_CantidadFilasAfectadas
        End Function
        Public Function ufn_ActualizarReclamoAprobacionGerenciaComercialDetalle(ByVal intNumeroreclamo As Integer, ByVal intSecuencia As Integer, ByVal strTipoSolucion As String, ByVal strMontoComentario As String, ByVal strUsuarioModificacion As String, ByVal dtmFechaModificacion As DateTime) As Integer
            Dim int_CantidadFilasAfectadas As Integer = 0
            Try
                Dim larrParams() As Object = {"pint_Reclamo", intNumeroreclamo, _
                                              "pint_Secuencia", intSecuencia, _
                                              "pvar_TipoSolucion", strTipoSolucion, _
                                              "pvar_MontoComentario", strMontoComentario, _
                                              "pvar_UsurioModificacion", strUsuarioModificacion, _
                                              "pdtm_FechaModificacion", dtmFechaModificacion _
                                             }
                int_CantidadFilasAfectadas = m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_RECLAMO_APROBACION_GERENCIA_COMERCIAL_DETALLE", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_CantidadFilasAfectadas
        End Function
        Public Function ufn_ActualizarReclamoDetalle(ByVal intNumeroreclamo As Integer, ByVal strEstado As String, ByVal strUsuarioModificacion As String, ByVal dtmFechaModificacion As DateTime) As Integer
            Dim int_CantidadFilasAfectadas As Integer = 0
            Try
                Dim larrParams() As Object = {"pint_Reclamo", intNumeroreclamo, _
                                              "pvar_Estado", strEstado, _
                                              "pvar_UsurioModificacion", strUsuarioModificacion, _
                                              "pdtm_FechaModificacion", dtmFechaModificacion _
                                             }
                int_CantidadFilasAfectadas = m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_RECLAMO_DETALLE_X_ARTICULO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_CantidadFilasAfectadas
        End Function
        Public Function ufn_ActualizarReclamoDatosEnvioRevisionFinal(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal pdtmFechaEnvioRevisionFinal As DateTime, ByVal strUsuarioModificacion As String) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pdtm_FechaEnvioRevisionFinal", pdtmFechaEnvioRevisionFinal, _
                                                "pvar_UsuarioModificacion", strUsuarioModificacion _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_RECLAMO_DATOS_ENVIO_REVISION_FINAL", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarEstadoReclamoDetalleDatos(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal strCodigoArticulo As String, ByVal pdtmFechaModificacion As DateTime, ByVal strUsuarioModificacion As String) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pvar_CodigoArticulo", strCodigoArticulo, _
                                                "pdtm_FechaModificacion", pdtmFechaModificacion, _
                                                "pvar_UsuarioModificacion", strUsuarioModificacion _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_ESTADO_RECLAMO_DETALLE_DATOS", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_Registrar_Rollos_ReclamoNdv(ByVal intNumeroReclamo As Integer, _
                                                 ByVal strnumeroNDV As String, _
                                                 ByVal dtb_DatosRollos As DataTable, _
                                                 ByVal strUsuario As String) As Integer

            Dim lobjUtil As New NM_General.Util
            Dim strDatosRollosXML As String

            strDatosRollosXML = lobjUtil.GeneraXml(dtb_DatosRollos)
            Dim larrParams() As Object = {"pint_Reclamo", intNumeroReclamo, _
                                            "pvar_numeroNDV", strnumeroNDV, _
                                            "pvch_DatosRollos_XML", strDatosRollosXML, _
                                            "pvar_Usuario", strUsuario}
            Try
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_REGISTRAR_ROLLOS_RECLAMO_NDV", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarEstadoReclamoDatos(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal pdtmFechaModificacion As DateTime, ByVal strUsuarioModificacion As String) As Integer
            Try
                Dim larrParams() As Object = {"pEstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pdtm_FechaModificacion", pdtmFechaModificacion, _
                                                "pvar_UsuarioModificacion", strUsuarioModificacion _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_ESTADO_RECLAMO_DATOS", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarEstadoReclamoClienteD(ByVal strEstado As String, ByVal strNumeroreclamo As Integer, ByVal pdtmFechaModificacion As DateTime, ByVal strUsuarioModificacion As String) As Integer
            '*****************************************************************************************************
            'Objetivo   : Actualizar estado de la tbl RECLAMO_CLIENTE_D
            'Autor      : Juan Cucho Antunez
            'Creado     : 07/02/2017
            'Modificado : //
            '*****************************************************************************************************
            Try
                Dim larrParams() As Object = {"pchr_EstadoReclamo", strEstado, _
                                                "pint_Reclamo", strNumeroreclamo, _
                                                "pdtm_FechaModificacion", pdtmFechaModificacion, _
                                                "pvar_UsuarioModificacion", strUsuarioModificacion _
                                             }
                Return m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_ESTADO_RECLAMO_CLIENTE_D", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ufn_ActualizarReclamoVariosAprobacionGerenciaComercial(ByVal intNumeroreclamo As Integer, ByVal strEstadoReclamo As String, ByVal dtmFechaAprobacionGerenciaComercial As DateTime, ByVal strUsuarioAprobacionGerenciaComercial As String, ByVal ComentarioGerenciaComercialVarios As String) As Integer
            '*****************************************************************************************************
            'Objetivo   : Actualizar aprobación reclamo varios en las tablas RECLAMO_CLIENTE_D y UTB_RECLAMO_CLIENTE_DETALLE_D
            'Autor      : Juan Cucho Antunez
            'Creado     : 09/02/2017
            'Modificado : //
            '*****************************************************************************************************
            Dim int_CantidadFilasAfectadas As Integer = 0
            Try
                Dim larrParams() As Object = {"pint_Reclamo", intNumeroreclamo, _
                                              "pvar_EstadoReclamo", strEstadoReclamo, _
                                              "pdtm_FechaAprobacionGerenciaComercial", dtmFechaAprobacionGerenciaComercial, _
                                              "pvar_usuarioAprobacionGerenciaComercial", strUsuarioAprobacionGerenciaComercial, _
                                              "pvar_ComentarioGerenciaComercialVarios", ComentarioGerenciaComercialVarios
                                             }
                int_CantidadFilasAfectadas = m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_RECLAMO_VARIOS_APROBACION_GERENCIA_COMERCIAL", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_CantidadFilasAfectadas
        End Function
        Public Function ufn_ObtenerDatosArticuloPorCodigo(ByVal strCodigoArticulo As String) As DataTable
            '*****************************************************************************************************
            'Objetivo   : Obtener datos de un articulo por su codigo 
            'Autor      : Juan Cucho Antunez
            'Creado     : 15/02/2017
            'Modificado : //
            '*****************************************************************************************************
            Dim dtReturn As DataTable
            Try
                Dim larrParams() As Object = {"pvar_CodigoArticulo", strCodigoArticulo}
                dtReturn = m_sqlDtAccRevisionFinal.ObtenerDataTable("USP_RVF_OBTENER_DATOS_ARTICULO_POR_CODIGO", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtReturn
        End Function
        Public Function ufn_ActualizarEstadoDetalleReclamoCliente(ByVal intNumeroreclamo As Integer, ByVal strEstadoReclamo As String, ByVal strObservaciones As String, ByVal dtmFechaModificacion As DateTime, ByVal strUsuarioModificacion As String, ByVal dtReclamo As DataTable) As Integer
            '*****************************************************************************************************
            'Objetivo   : Actualizar estado detalle reclamo cliente
            'Autor      : Juan Cucho Antunez
            'Creado     : 08/03/2017
            'Modificado : //
            '*****************************************************************************************************
            Dim int_CantidadFilasAfectadas As Integer = 0

            Try
                'Dim lobjUtil As New NM_General.Util
                'Dim lstrXML As String
                'strDatatatable.TableName = "DTRECLAMO"
                'lstrXML = lobjUtil.GeneraXml(strDatatatable)
                Dim lobjUtil As New NM_General.Util
                dtReclamo.TableName = "reclamos"
                Dim strReclamoXML As String = lobjUtil.GeneraXml(dtReclamo)
                Dim larrParams() As Object = {"pint_Reclamo", intNumeroreclamo, _
                                              "pchr_EstadoReclamo", strEstadoReclamo, _
                                              "pvar_Observaciones", strObservaciones, _
                                              "pdtm_FechaModificacion", dtmFechaModificacion, _
                                              "pvar_UsuarioModificacion", strUsuarioModificacion, _
                                              "pvar_ReclamoXML", strReclamoXML
                                             }
                int_CantidadFilasAfectadas = m_sqlDtAccRevisionFinal.EjecutarComando("USP_RVF_ACTUALIZAR_ESTADO_DETALLE_RECLAMO_CLIENTE", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_CantidadFilasAfectadas
        End Function
    End Class
End Namespace