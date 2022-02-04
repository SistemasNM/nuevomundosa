Option Strict On

Imports NM.AccesoDatos
Imports System.Xml
Imports System.IO


Namespace NM.RevisionFinal
    Public Class DevolucionAlmacen
        Implements IDisposable

        Private m_sqlDtAccRevFin As AccesoDatosSQLServer

        Public Sub New()
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function Grabar(ByVal dteFechaDevolucion As Date, ByVal strObservaciones As String, ByVal dtblDetalle As DataTable, ByVal dtblOfisis As DataTable, ByVal strAlmacenDestino As String, ByVal strCodigoCliente As String, ByVal strNombreCliente As String, ByVal strMotivoDevolucion As String, ByVal strUsuario As String) As String
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util
                Dim objParametros As Object() = {"fecha_devolucion", dteFechaDevolucion, "observaciones", strObservaciones, "xml_data", objXml.GeneraXml(dtblDetalle), "xml_ofisis", objXml.GeneraXml(dtblOfisis), "ALMACEN_DESTINO", strAlmacenDestino, "CODIGO_CLIE", strCodigoCliente, "NOMBRE_CLIE", strNombreCliente, "motivo_devolucion", strMotivoDevolucion, "USUARIO", strUsuario}
                Dim dtbResultado As DataTable = m_sqlDtAccRevFin.ObtenerDataTable("UP_InsertarDevolucionAlmacen", objParametros)
                Dim strCodigoDevolucion As String = ""

                If Not dtbResultado Is Nothing Then
                    strCodigoDevolucion = CStr((dtbResultado.Rows(0).Item(0)))
                End If

                Return strCodigoDevolucion

            Catch ex As Exception
                Return ""
            End Try
        End Function
        Public Function ActualizarDetalle(ByVal strCodigoDevoluciona As String, ByVal dtblDetalleAlmacen As DataTable, ByVal dtblOfisis As DataTable, ByVal strAlmacen As String, ByVal strCodigoCliente As String, ByVal strNombreCliente As String, ByVal strUsuario As String, ByVal strObs As String, ByVal strFecha As String) As String
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util

                Dim objParametros As Object() = {"codigo_devoluciona", strCodigoDevoluciona, "xml_data", objXml.GeneraXml(dtblDetalleAlmacen), "xml_ofisis", objXml.GeneraXml(dtblOfisis), "ALMACEN_DESTINO", strAlmacen, "CODIGO_CLIE", strCodigoCliente, "NOMBRE_CLIE", strNombreCliente, "USUARIO", strUsuario, "OBSERVACIONES", strObs, "fecha_devolucion", strFecha}

                m_sqlDtAccRevFin.EjecutarComando("UP_ActualizarDetalleAlmacen", objParametros)

            Catch ex As Exception

            End Try
        End Function
        Public Function ValidaSiExisteCodigo(ByVal strCodigoDevolucion As String) As String
            Try
                Dim objParametros As Object() = {"codigo_devoluciona", strCodigoDevolucion}
                Dim dtbResultado As DataTable = m_sqlDtAccRevFin.ObtenerDataTable("SP_VALIDA_CODIGO", objParametros)
                Dim strCodigoDevoluciona As String = CStr((dtbResultado.Rows(0).Item(0)))
                Return strCodigoDevoluciona
            Catch ex As Exception
                Throw ex
                Return ""
            End Try
        End Function
        Public Function ValidaSiExisteEnLote(ByVal strCodigoRollo As String) As DataTable
            Try
                Dim objParametros As Object() = {"CODIGO_ROLLO", strCodigoRollo}
                Dim dtbLotes As DataTable = m_sqlDtAccRevFin.ObtenerDataTable("NM_REVFIN_VALIDA_ROLLO_LOTIZADO", objParametros)
                Return dtbLotes
            Catch ex As Exception
                Throw ex

            End Try

        End Function

        Public Function ObtenerDatosRollosOFISIS(ByVal strCodigoRollo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosRolloOfisis", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosRollosOFISIS_2(ByVal strCodigoRollo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosRolloOfisis_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosDevolucion(ByVal strCodigoDevolucion As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_devoluciona", strCodigoDevolucion}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_REVFIN_ACT_FRDA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerAlmacenDestino() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_SP_OBTIENE_ALMACEN_DESTINO")
            Catch ex As Exception

            End Try

        End Function
        Public Function ObtenerMotivoDevolucion() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_SP_OBTIENE_MOTIVO_DEVOLUCION")
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ObtenerCliente(ByVal strCodigoCliente As String) As DataTable

            Try
                Dim objParametros As Object() = {"CO_CLIE", strCodigoCliente}
                Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_SP_OBTENER_DATOS_CLIENTE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtenerRollosDespachados(ByVal strCodigoCliente As String, ByVal strCodigoRollo As String) As DataTable
            Try
                Dim objParametros As Object() = {"CO_CLIE", strCodigoCliente, "codigo_rollo", strCodigoRollo}
                Return m_sqlDtAccRevFin.ObtenerDataTable("REVFIN_SP_OBTENER_ROLLO_CLIENTE", objParametros)
            Catch ex As Exception
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevFin.Dispose()
        End Sub
        Public Function ObtenerDatosClientePorDevolucion(ByVal strCodigoDevolucion As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_devoluciona", strCodigoDevolucion}
                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_REVFIN_OBTIENE_DATOSCLIENTE_POR_DEVOLUCION", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerClientes() As DataTable
            Try
                'Dim objParametros As Object() = {"CO_CLIE", strCodigoCliente, "NO_CLIE", strNombreCliente}

                Return m_sqlDtAccRevFin.ObtenerDataTable("SP_REVFIN_OBTENER_CLIENTES")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

#Region "DEVOLUCION ALMACEN ACTUALIZADO"
        Public Function ObtenerDatosDevolucionAlmacen(ByVal strCodigoDevolucion As String) As DataTable
            Try
                Dim objParametros As Object() = {"CODIGO_DEVOLUCIONA", strCodigoDevolucion}
                Return m_sqlDtAccRevFin.ObtenerDataTable("REP_DEVOLUCIONES_ALMACEN", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function fnc_ObtenerDatosDevolucionAlmacen(ByVal strCodigoDevolucion As String) As DataTable
            Try
                Dim objParametros As Object() = {"VCH_CODIGO_DEVOLUCION", strCodigoDevolucion}
                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_DEVOLUCIONES_ALMACEN", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtenerNuevoCorrelativo(Optional ByVal strTipoDocumento As String = "NDV", Optional ByVal strNumeroSerie As String = "0001", Optional ByVal strCodigoEmpresa As String = "01") As String

            Try
                Dim objParametros As Object() = {"vch_CO_EMPR", strCodigoEmpresa,
                                                 "vch_TI_DOCU", strTipoDocumento,
                                                 "vch_NU_SERI", strNumeroSerie}

                Return CStr(m_sqlDtAccRevFin.ObtenerValor("USP_RVF_OBTENER_NUEVO_CORRELATIVO", objParametros))

            Catch ex As Exception
                Throw ex
            End Try

        End Function




        Public Function GrabarV2(ByVal dteFechaDevolucion As Date, ByVal strObservaciones As String, ByVal dtblDetalle As DataTable, ByVal dtblOfisis As DataTable, ByVal strAlmacenDestino As String, ByVal strCodigoCliente As String, ByVal strNombreCliente As String, ByVal strMotivoDevolucion As String, ByVal strUsuario As String) As String
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util
                Dim objParametros As Object() = {"fecha_devolucion", dteFechaDevolucion, "observaciones", strObservaciones, "xml_data", objXml.GeneraXml(dtblDetalle), "xml_ofisis", objXml.GeneraXml(dtblOfisis), "ALMACEN_DESTINO", strAlmacenDestino, "CODIGO_CLIE", strCodigoCliente, "NOMBRE_CLIE", strNombreCliente, "MOTIVO_DEVOLUCION", strMotivoDevolucion, "USUARIO", strUsuario}
                Dim dtbResultado As DataTable = m_sqlDtAccRevFin.ObtenerDataTable("UP_InsertarDevolucionAlmacen_v2", objParametros)
                Dim strCodigoDevolucion As String = ""

                If Not dtbResultado Is Nothing Then
                    strCodigoDevolucion = CStr((dtbResultado.Rows(0).Item(0)))
                End If

                Return strCodigoDevolucion

            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Function GrabarV2_2(ByVal dteFechaDevolucion As Date, ByVal strObservaciones As String, ByVal dtblDetalle As DataTable, ByVal dtblOfisis As DataTable, ByVal strAlmacenDestino As String, ByVal strCodigoCliente As String, ByVal strNombreCliente As String, ByVal strMotivoDevolucion As String, ByVal strUsuario As String) As String
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util
                Dim objParametros As Object() = {"fecha_devolucion", dteFechaDevolucion, "observaciones", strObservaciones, "xml_data", objXml.GeneraXml(dtblDetalle), "xml_ofisis", objXml.GeneraXml(dtblOfisis), "ALMACEN_DESTINO", strAlmacenDestino, "CODIGO_CLIE", strCodigoCliente, "NOMBRE_CLIE", strNombreCliente, "motivo_devolucion", strMotivoDevolucion, "USUARIO", strUsuario}
                Dim dtbResultado As DataTable = m_sqlDtAccRevFin.ObtenerDataTable("UP_InsertarDevolucionAlmacen_v2_2", objParametros)
                Dim strCodigoDevolucion As String = ""

                If Not dtbResultado Is Nothing Then
                    strCodigoDevolucion = CStr((dtbResultado.Rows(0).Item(0)))
                End If

                Return strCodigoDevolucion

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActualizarDetalleV2(ByVal strCodigoDevoluciona As String, ByVal dtblDetalleAlmacen As DataTable, ByVal dtblOfisis As DataTable, ByVal strAlmacen As String, ByVal strCodigoCliente As String, ByVal strNombreCliente As String, ByVal strUsuario As String, ByVal strObs As String, ByVal strFecha As String) As String
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util

                Dim objParametros As Object() = {"CODIGO_DEVOLUCIONA", strCodigoDevoluciona, "xml_data", objXml.GeneraXml(dtblDetalleAlmacen), "xml_ofisis", objXml.GeneraXml(dtblOfisis), "ALMACEN_DESTINO", strAlmacen, "CODIGO_CLIE", strCodigoCliente, "NOMBRE_CLIE", strNombreCliente, "USUARIO", strUsuario, "OBSERVACIONES", strObs, "fecha_devolucion", strFecha}

                m_sqlDtAccRevFin.EjecutarComando("UP_ActualizarDetalleAlmacenV2", objParametros)

            Catch ex As Exception

            End Try
        End Function

        Public Function ActualizarDetalleV2_2(ByVal strCodigoDevoluciona As String, ByVal dtblDetalleAlmacen As DataTable, ByVal dtblOfisis As DataTable, ByVal strAlmacen As String, ByVal strCodigoCliente As String, ByVal strNombreCliente As String, ByVal strmotivodevolucion As String, ByVal strUsuario As String, ByVal strObs As String, ByVal strFecha As String) As String
            Try
                'Dim objXml As New generaXml
                Dim objXml As New NM_General.Util

                Dim objParametros As Object() = {"CODIGO_DEVOLUCIONA", strCodigoDevoluciona, "xml_data", objXml.GeneraXml(dtblDetalleAlmacen), "xml_ofisis", objXml.GeneraXml(dtblOfisis), "ALMACEN_DESTINO", strAlmacen, "CODIGO_CLIE", strCodigoCliente, "NOMBRE_CLIE", strNombreCliente, "motivo_devolucion", strmotivodevolucion, "USUARIO", strUsuario, "OBSERVACIONES", strObs, "fecha_devolucion", strFecha}

                m_sqlDtAccRevFin.EjecutarComando("UP_ActualizarDetalleAlmacenV2_2", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "LUIS_AJ (20151109)"

        Public Function Registrar_Devolucion_Almacen(ByVal strOperacion As String,
                                                     ByVal strNumeroDevolucion As String,
                                                     ByVal strFechaDevolucion As String,
                                                     ByVal strCodigoDestino As String,
                                                     ByVal strMotivoDevolucion As String,
                                                     ByVal strCodigoCliente As String,
                                                     ByVal strNombreCliente As String, 
                                                     ByVal strObservaciones As String,
                                                     ByVal strUsuario As String,
                                                     ByVal dtDatos As DataTable,
                                                     ByVal dtblDatosOfisis As DataTable) As DataTable
            Dim strDatosRollosXML As String
            Dim strDatosOfisisXML As String
            Dim dtResult As DataTable

            Try

                strDatosRollosXML = GeneraXml(dtDatos)
                strDatosOfisisXML = GeneraXml(dtblDatosOfisis)

                Dim objParametros() As Object = {"pvch_TipoOperacion", strOperacion, _
                                                 "pvch_CodigoDevolucion", strNumeroDevolucion, _
                                                 "pvch_FechaDevolucion", strFechaDevolucion, _
                                                 "pvch_CodigoDestino", strCodigoDestino, _
                                                 "pvch_MotivoDevolucion", strMotivoDevolucion, _
                                                 "pvch_CodigoCliente", strCodigoCliente, _
                                                 "pvch_NombreCliente", strNombreCliente, _
                                                 "pvch_Observaciones", strObservaciones, _
                                                 "pvch_CodigoUsuario", strUsuario, _
                                                 "pvch_DatosRollos_XML", strDatosRollosXML, _
                                                 "pvch_DatosOfisis_XML", strDatosOfisisXML}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_DEVOLUCION_TELA_ALMACEN_GRABAR", objParametros)
                Return dtResult

            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try

        End Function

        Public Function GeneraXml(ByVal dtblDatos As DataTable) As String
            Dim xmlDocDatos As New XmlDocument
            Dim nodo, nodoChild As XmlElement
            Dim objUtil As New NM_General.Util
            With xmlDocDatos
                .Load(New StringReader("<root></root>"))
                For i As Integer = 0 To dtblDatos.Rows.Count - 1
                    nodo = .CreateElement(dtblDatos.TableName)
                    For j As Integer = 0 To dtblDatos.Columns.Count - 1
                        If Not IsDBNull(dtblDatos.Rows(i)(j)) Then
                            nodoChild = .CreateElement(dtblDatos.Columns(j).ColumnName)
                            nodoChild.InnerText = Trim(CType(dtblDatos.Rows(i)(j), String))
                            nodo.AppendChild(nodoChild)
                        End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return (objUtil.EncodeXML(.OuterXml()))
            End With
        End Function

        Public Function Eliminar_Rollo_Devolucion(ByVal strCodigoRollo As String) As Integer
            Dim objParametros() As Object = {"codigo_rollo", strCodigoRollo}
            Try
                Return m_sqlDtAccRevFin.EjecutarComando("USP_RVF_ELIMINA_ROLLO_DEVOLUCION", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
        '*****************************************************************************************************
        'Objetivo   : Generar nota de devolucion
        'Autor      : Juan Cucho Antunez
        'Creado     : //
        'Modificado : //
        '*****************************************************************************************************
        Public Function Registrar_Devolucion_Almacen_Ndv(ByVal strOperacion As String,
                                                     ByVal strNumeroDevolucion As String,
                                                     ByVal strFechaDevolucion As String,
                                                     ByVal strCodigoDestino As String,
                                                     ByVal strMotivoDevolucion As String,
                                                     ByVal strCodigoCliente As String,
                                                     ByVal strNombreCliente As String,
                                                     ByVal strObservaciones As String,
                                                     ByVal strUsuario As String,
                                                     ByVal strUbicacion As String,
                                                     ByVal dtDatos As DataTable,
                                                     ByVal dtblDatosOfisis As DataTable) As DataTable
            Dim strDatosRollosXML As String
            Dim strDatosOfisisXML As String
            Dim dtResult As DataTable

            Try

                strDatosRollosXML = GeneraXml(dtDatos)
                strDatosOfisisXML = GeneraXml(dtblDatosOfisis)

                Dim objParametros() As Object = {"pvch_TipoOperacion", strOperacion, _
                                                 "pvch_CodigoDevolucion", strNumeroDevolucion, _
                                                 "pvch_FechaDevolucion", strFechaDevolucion, _
                                                 "pvch_CodigoDestino", strCodigoDestino, _
                                                 "pvch_MotivoDevolucion", strMotivoDevolucion, _
                                                 "pvch_CodigoCliente", strCodigoCliente, _
                                                 "pvch_NombreCliente", strNombreCliente, _
                                                 "pvch_Observaciones", strObservaciones, _
                                                 "pvch_CodigoUsuario", strUsuario, _
                                                 "pvch_CodUbicacion", strUbicacion, _
                                                 "pvch_DatosRollos_XML", strDatosRollosXML, _
                                                 "pvch_DatosOfisis_XML", strDatosOfisisXML}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_DEVOLUCION_TELA_ALMACEN_GRABAR_NDV", objParametros)
                Return dtResult

            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try

        End Function
        '*****************************************************************************************************
        'Objetivo   : Generar nota de devolucion 2das
        'Autor      : Juan Cucho Antunez
        'Creado     : 20/01/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function Registrar_Devolucion_Almacen_Ndv_2das(ByVal strOperacion As String,
                                                     ByVal strNumeroDevolucion As String,
                                                     ByVal strFechaDevolucion As String,
                                                     ByVal strCodigoDestino As String,
                                                     ByVal strMotivoDevolucion As String,
                                                     ByVal strCodigoCliente As String,
                                                     ByVal strNombreCliente As String,
                                                     ByVal strObservaciones As String,
                                                     ByVal strUsuario As String,
                                                     ByVal strUbicacion As String,
                                                     ByVal dtDatos As DataTable) As DataTable
            Dim strDatosRollosXML As String
            Dim dtResult As DataTable
            Try
                strDatosRollosXML = GeneraXml(dtDatos)
                Dim objParametros() As Object = {"pvch_TipoOperacion", strOperacion, _
                                                 "pvch_CodigoDevolucion", strNumeroDevolucion, _
                                                 "pvch_FechaDevolucion", strFechaDevolucion, _
                                                 "pvch_CodigoDestino", strCodigoDestino, _
                                                 "pvch_MotivoDevolucion", strMotivoDevolucion, _
                                                 "pvch_CodigoCliente", strCodigoCliente, _
                                                 "pvch_NombreCliente", strNombreCliente, _
                                                 "pvch_Observaciones", strObservaciones, _
                                                 "pvch_CodigoUsuario", strUsuario, _
                                                 "pvch_CodUbicacion", strUbicacion, _
                                                 "pvch_DatosRollos_XML", strDatosRollosXML}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_DEVOLUCION_TELA_ALMACEN_GRABAR_NDV_2DAS", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try
        End Function
        '*****************************************************************************************************
        'Objetivo   : Generar ndv y nsa
        'Autor      : Juan Cucho Antunez
        'Creado     : 08/03/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function Generar_Ndv_Nsa(ByVal strCodigoEmpresa As String,
                                        ByVal strOperacion As String,
                                        ByVal strNumeroDevolucion As String,
                                        ByVal strFechaDevolucion As String,
                                        ByVal strCodigoAlmacenDestino As String,
                                        ByVal strMotivoDevolucion As String,
                                        ByVal strCodigoCliente As String,
                                        ByVal strNombreCliente As String,
                                        ByVal strObservaciones As String,
                                        ByVal strUsuario As String,
                                        ByVal strUbicacion As String,
                                        ByVal strCodigoCCosto As String,
                                        ByVal dtDatos As DataTable) As DataTable
            Dim strDatosRollosXML As String
            Dim dtResult As DataTable
            Try
                strDatosRollosXML = GeneraXml(dtDatos)
                Dim objParametros() As Object = {"pchr_CodigoEmpresa", strCodigoEmpresa, _
                                                 "pvch_TipoOperacion", strOperacion, _
                                                 "pvch_CodigoDevolucion", strNumeroDevolucion, _
                                                 "pvch_FechaDevolucion", strFechaDevolucion, _
                                                 "pvch_CodigoAlmacenDestino", strCodigoAlmacenDestino, _
                                                 "pvch_MotivoDevolucion", strMotivoDevolucion, _
                                                 "pvch_CodigoCliente", strCodigoCliente, _
                                                 "pvch_NombreCliente", strNombreCliente, _
                                                 "pvch_Observaciones", strObservaciones, _
                                                 "pvch_CodigoUsuario", strUsuario, _
                                                 "pvch_CodUbicacion", strUbicacion, _
                                                 "pvch_CodigoCCosto", strCodigoCCosto, _
                                                 "pvch_DatosRollos_XML", strDatosRollosXML}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_GENERAR_NDV_Y_NSA", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try
        End Function
        '*****************************************************************************************************
        'Objetivo   : Generar reclamo de la devolucion de revision final
        'Autor      : Juan Cucho Antunez
        'Creado     : 08/03/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function GenerarReclamoDevolucionRvf(ByVal strCodigoEmpresa As String,
                                        ByVal strNumeroTipoDocumento As String,
                                        ByVal strNumeroDocumento As String,
                                        ByVal strNumeroNotaSalida As String,
                                        ByVal strUsuarioCreacion As String,
                                        ByVal dtDatosRollos As DataTable) As DataTable
            Dim strDatosRollosXML As String
            Dim dtResult As DataTable
            Try
                strDatosRollosXML = GeneraXml(dtDatosRollos)
                Dim objParametros() As Object = {"pvar_CodigoEmpresa", strCodigoEmpresa, _
                                                 "pvar_NumeroTipoDocumento", strNumeroTipoDocumento, _
                                                 "pvar_NumeroDocumento", strNumeroDocumento, _
                                                 "pvar_NumeroNotaSalida", strNumeroNotaSalida, _
                                                 "pvar_UsuarioCreacion", strUsuarioCreacion,
                                                 "pvch_DatosRollos_XML", strDatosRollosXML}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_GENERAR_RECLAMO_DEVOLUCION_RVF", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try
        End Function
    End Class
End Namespace