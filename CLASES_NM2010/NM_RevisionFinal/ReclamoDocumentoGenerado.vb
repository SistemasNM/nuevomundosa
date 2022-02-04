Option Strict On
Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.IO
Namespace NM.RevisionFinal
    Public Class ReclamoDocumentoGenerado
        Implements IDisposable
        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer
        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
        End Sub
        Public Function GeneraXml(ByVal dtblDatos As DataTable) As String
            Dim xmlDocDatos As New XmlDocument
            Dim nodo, nodoChild As XmlElement
            Dim objXML As New NM_General.Util
            With xmlDocDatos
                .Load(New StringReader("<root></root>"))
                For i As Integer = 0 To dtblDatos.Rows.Count - 1
                    nodo = .CreateElement(dtblDatos.TableName)
                    For j As Integer = 0 To dtblDatos.Columns.Count - 1
                        'If Not IsDBNull(dtblDatos.Rows(i)(j)) Then
                        nodoChild = .CreateElement(dtblDatos.Columns(j).ColumnName.ToLower)
                        Try
                            nodoChild.InnerText = Trim(CType(dtblDatos.Rows(i)(j), String))
                        Catch ex As Exception
                            nodoChild.InnerText = String.Empty
                        End Try
                        nodo.AppendChild(nodoChild)
                        ' End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return objXML.EncodeXML(.OuterXml())

            End With
        End Function
        Public Function RegistrarReclamoDocumentoGenerado(ByVal strCodigoEmpresa As String,
                                                            ByVal strNumeroTipoDocumento As String,
                                                            ByVal strNumeroDocumento As String,
                                                            ByVal intReclamo As Int32,
                                                            ByVal strTipoDocumento As String,
                                                            ByVal strNumeroGuiaRemisionCliente As String,
                                                            ByVal numMetrajeTotalGuiaRemisionCliente As Decimal,
                                                            ByVal dtmFechaRegistro As DateTime,
                                                            ByVal strObservacionAlmacen As String,
                                                            ByVal strCodigoUbicacion As String,
                                                            ByVal strUsuarioCreacion As String,
                                                            ByVal dtblDatosRollosAbiertosCerrados As DataTable) As Integer
            Dim strDatosRollosAbiertosCerrados As String
            Dim intResult As Integer = 0
            Try
                strDatosRollosAbiertosCerrados = GeneraXml(dtblDatosRollosAbiertosCerrados)

                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                                 "pvar_NumeroTipoDocumento", strNumeroTipoDocumento,
                                                 "pvar_NumeroDocumento", strNumeroDocumento,
                                                 "pint_Reclamo", intReclamo,
                                                 "pvar_TipoDocumento", strTipoDocumento,
                                                 "pvar_NumeroGuiaRemisionCliente", strNumeroGuiaRemisionCliente,
                                                 "pnum_MetrajeTotalGuiaRemisionCliente", numMetrajeTotalGuiaRemisionCliente,
                                                 "pdtm_FechaRegistro", dtmFechaRegistro,
                                                 "pvar_ObservacionAlmacen", strObservacionAlmacen,
                                                 "pvar_CodigoUbicacion", strCodigoUbicacion,
                                                 "pvar_UsuarioCreacion", strUsuarioCreacion,
                                                 "pvch_DatosRollosAbiertosCerrados_XML", strDatosRollosAbiertosCerrados
                                                 }
                intResult = m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRAR_RECLAMO_DOCUMENTO_GENERADO", objParametros)
                Return (intResult)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerCorrelativoGuiaTransitoDocumentosReclamo(ByVal intTipoDocumentoGuiaRemision As Int32) As String
            Try
                Dim objParametros As Object() = {"pint_Fila", intTipoDocumentoGuiaRemision}
                Return m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_OBTENER_CORRELATIVO_GUIA_TRANSITO_DOCUMENTOS_RECLAMO", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDocumentoCabeceraRollosAbiertosPorAprobarAlmacen(ByVal intReclamo As Integer, ByVal bitEnvioRevisionFinal As Integer, ByVal strEstadoReclamo As String) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo, "pbit_EnvioRevisionFinal", bitEnvioRevisionFinal, "pchr_EstadoReclamo", strEstadoReclamo}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DOCUMENTO_CABECERA_ROLLOS_ABIERTOS_POR_APROBAR_ALMACEN", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDocumentoCabeceraRollosAbiertosEnviadosRevisionFinal(ByVal intReclamo As Integer) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DOCUMENTO_CABECERA_ROLLOS_ABIERTOS_ENVIADO_REVISION_FINAL", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarDocumentoGeneradoEnvioRevisionFinal(ByVal strCodigoEmpresa As String, _
                                                           ByVal strNumeroTipoDocumento As String, _
                                                           ByVal strNumeroDocumento As String, _
                                                           ByVal dtmFechaEnvioRevisionFinal As DateTime, _
                                                           ByVal bitEnvioRevisionFinal As Integer, _
                                                           ByVal strUsuarioModificacion As String, _
                                                           ByVal dtmFechaModificacion As DateTime) As Integer
            Dim int_CantidadFilasAfectadas As Integer = 0
            Try
                Dim larrParams() As Object = {"pvar_CodigoEmpresa", strCodigoEmpresa, _
                                                "pvar_NumeroTipoDocumento", strNumeroTipoDocumento, _
                                                "pvar_NumeroDocumento", strNumeroDocumento, _
                                                "pdtm_FechaEnvioRevisionFinal", dtmFechaEnvioRevisionFinal, _
                                                "pbit_EnvioRevisionFinal", bitEnvioRevisionFinal, _
                                                "pvar_usuarioModificacion", strUsuarioModificacion, _
                                                "pdtm_FechaModificacion", dtmFechaModificacion
                                             }
                int_CantidadFilasAfectadas = m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_ACTUALIZAR_DOCUMENTO_GENERADO_ENVIO_REVISIONFINAL", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_CantidadFilasAfectadas
        End Function
        Public Function ObtenerDocumentoDetalleGeneradoEnvioRevisionFinal(ByVal intReclamo As String) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DOCUMENTO_CABECERA_ROLLOS_ABIERTOS_ENVIADO_REVISION_FINAL", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDocumentoDetalleGeneradoEnvioRevisionFinalPorTicket(ByVal intReclamo As String, ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo, _
                                                 "pvar_codigo_articulo", strCodigoArticulo}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DOCUMENTO_DETALLE_GENERADO_ENVIO_REVISION_FINAL_D", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '*****************************************************************************************************
        'Objetivo   : Registrar reclamo documento generado 2das
        'Autor      : Juan Cucho Antunez
        'Creado     : 23/01/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function RegistrarReclamoDocumentoGenerado2das(ByVal strCodigoEmpresa As String,
                                                            ByVal strNumeroTipoDocumento As String,
                                                            ByVal strNumeroDocumento As String,
                                                            ByVal strTipoDocumento As String,
                                                            ByVal strNumeroGuiaRemisionCliente As String,
                                                            ByVal numMetrajeTotalGuiaRemisionCliente As Decimal,
                                                            ByVal dtmFechaRegistro As DateTime,
                                                            ByVal strObservacionAlmacen As String,
                                                            ByVal strCodigoUbicacion As String,
                                                            ByVal strUsuarioCreacion As String,
                                                            ByVal dtblDatosRollosAbiertosCerrados As DataTable,
                                                            ByVal strCodigoCliente As String,
                                                            ByVal strCodigoVendedor As String) As DataTable
            Dim strDatosRollosAbiertosCerrados As String
            Dim DataTable As DataTable
            Try
                strDatosRollosAbiertosCerrados = GeneraXml(dtblDatosRollosAbiertosCerrados)

                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                                 "pvar_NumeroTipoDocumento", strNumeroTipoDocumento,
                                                 "pvar_NumeroDocumento", strNumeroDocumento,
                                                 "pvar_TipoDocumento", strTipoDocumento,
                                                 "pvar_NumeroGuiaRemisionCliente", strNumeroGuiaRemisionCliente,
                                                 "pnum_MetrajeTotalGuiaRemisionCliente", numMetrajeTotalGuiaRemisionCliente,
                                                 "pdtm_FechaRegistro", dtmFechaRegistro,
                                                 "pvar_ObservacionAlmacen", strObservacionAlmacen,
                                                 "pvar_CodigoUbicacion", strCodigoUbicacion,
                                                 "pvar_UsuarioCreacion", strUsuarioCreacion,
                                                 "pvch_DatosRollosAbiertosCerrados_XML", strDatosRollosAbiertosCerrados,
                                                 "pvar_CodigoCliente", strCodigoCliente,
                                                 "pvar_CodigoVendedor", strCodigoVendedor
                                                 }
                DataTable = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_REGISTRAR_RECLAMO_DOCUMENTO_GENERADO_2DAS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return (DataTable)
        End Function
        '*****************************************************************************************************
        'Objetivo   : Consultar documento generado 2das para vincular a reclamo
        'Autor      : Juan Cucho Antunez
        'Creado     : 24/01/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function consultaNdvDocumentoGenerado2dasVincularReclamo(Optional ByVal strCodigoEmpresa As String = "01", _
                                                                        Optional ByVal strNumeroDocumento As String = "", _
                                                            Optional ByVal strNumeroTipoDocumento As String = "0001", _
                                                            Optional ByVal strTipoDocumento As String = "NDV"
                                                            ) As DataTable
            Try
                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                                 "pvar_NumeroDocumento", strNumeroDocumento,
                                                 "pvar_NumeroTipoDocumento", strNumeroTipoDocumento,
                                                 "pvar_TipoDocumento", strTipoDocumento
                                                 }
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_CONSULTA_NDV_DOCUMENTO_GENERADO_2DAS_VINCULAR_RECLAMO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '*****************************************************************************************************
        'Objetivo   : Actualizar numero reclamo documento generado 2das
        'Autor      : Juan Cucho Antunez
        'Creado     : 23/01/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function AsignarNumeroReclamoDocumentoGenerado2das(ByVal dtNdvVinculadoReclamo As DataTable) As String
            Dim strDatosNdvVinculadoReclamo As String
            Dim strNumeroReclamo As Integer = 0
            Try
                dtNdvVinculadoReclamo.TableName = "NdvAsignadosReclamo"
                strDatosNdvVinculadoReclamo = GeneraXml(dtNdvVinculadoReclamo)
                Dim objParametros As Object() = {"pvar_DatosNdvVinculadoReclamo_XML", strDatosNdvVinculadoReclamo}
                Return CStr(m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_ASIGNAR_NUMERO_RECLAMO_DOCUMENTO_GENERADO_2DAS", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '*****************************************************************************************************
        'Objetivo   : Obtener Documento Cabecera Rollos Abierto
        'Autor      : Juan Cucho Antunez
        'Creado     : 14/02/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ObtenerDocumentoCabeceraRollosAbierto(ByVal strCodigoEmpresa As String, _
                                                             ByVal strNumeroTipoDocumento As String, _
                                                             ByVal strNumeroDocumento As String) As DataTable
            Dim dtReturn As DataTable
            Try
                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                                 "pvar_NumeroTipoDocumento", strNumeroTipoDocumento,
                                                 "pvar_NumeroDocumento", strNumeroDocumento
                                                 }
                dtReturn = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DOCUMENTO_CABECERA_ROLLOS_ABIERTOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtReturn
        End Function
        '*****************************************************************************************************
        'Objetivo   : Obtener Documento Detalle Rollos Abierto
        'Autor      : Juan Cucho Antunez
        'Creado     : 14/02/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ObtenerDocumentoDetalleRollosAbierto(ByVal strCodigoEmpresa As String, _
                                                             ByVal strNumeroTipoDocumento As String, _
                                                             ByVal strNumeroDocumento As String) As DataTable
            Dim dtReturn As DataTable
            Try
                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                                 "pvar_NumeroTipoDocumento", strNumeroTipoDocumento,
                                                 "pvar_NumeroDocumento", strNumeroDocumento
                                                 }
                dtReturn = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DOCUMENTO_DETALLE_ROLLOS_ABIERTOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtReturn
        End Function
        '*****************************************************************************************************
        'Objetivo   : Actualizar articulo, metraje cliente y metraje estimado
        'Autor      : Juan Cucho Antunez
        'Creado     : 17/02/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ActualizarDatosTicketsClienteRollosAbierto(ByVal strCodigoEmpresa As String,
                                                        ByVal strTipoDocumento As String,
                                                        ByVal strNumeroDocumento As String,
                                                        ByVal strObservacionAlmacen As String,
                                                        ByVal strUsuarioModificacion As String,
                                                        ByVal dtmFechaModificacion As DateTime,
                                                        ByVal strEstado As String,
                                                        ByVal dtblDatosArticulosCliente As DataTable) As Integer
            Dim intTicketsActualizados As Integer = 0
            Dim strDatosArticulosCliente As String
            dtblDatosArticulosCliente.TableName = "Articulos"
            strDatosArticulosCliente = GeneraXml(dtblDatosArticulosCliente)
            Try
                Dim objParametros As Object() = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                                 "pvar_TipoDocumento", strTipoDocumento,
                                                 "pvar_NumeroDocumento", strNumeroDocumento,
                                                 "pvar_ObservacionAlmacen", strObservacionAlmacen,
                                                 "pvar_UsuarioModificacion", strUsuarioModificacion,
                                                 "pdtm_FechaModificacion", dtmFechaModificacion,
                                                 "pvar_Estado", strEstado,
                                                 "pvar_DatosArticulosCliente", strDatosArticulosCliente}
                intTicketsActualizados = m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_ACTUALIZAR_DATOS_TICKETS_CLIENTE_ROLLOS_ABIERTOS", objParametros)
            Catch ex As Exception
                Throw ex
                intTicketsActualizados = 0
            End Try
            Return intTicketsActualizados
        End Function
    End Class
End Namespace

