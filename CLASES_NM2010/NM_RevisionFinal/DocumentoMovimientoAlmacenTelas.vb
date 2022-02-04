Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.IO
Imports NM_General
Imports System.Text

Namespace NM.RevisionFinal
    Public Class DocumentoMovimientoAlmacenTelas
        Implements IDisposable
        Private m_sqlDtAccRevFin As AccesoDatosSQLServer
        Public Sub New()
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevFin.Dispose()
        End Sub
        Public Function ufn_RegistrarDocumentoMovimientoAlmacenTelas(ByVal strTipoOperacion As String,
                                                     ByVal strCodigoDocumento As String,
                                                     ByVal strTipoDocumento As String,
                                                     ByVal strFechaDocumento As String,
                                                     ByVal strCodigoDestino As String,
                                                     ByVal strCodigoMotivo As String,
                                                     ByVal strCodigoCliente As String,
                                                     ByVal strNombreCliente As String,
                                                     ByVal strObservaciones As String,
                                                     ByVal strCodigoUsuario As String,
                                                     ByVal dtDatos As DataTable) As DataTable
            Dim strDatosRollosXML As String
            Dim dtResult As DataTable

            Try

                strDatosRollosXML = GeneraXml(dtDatos)
                Dim objParametros() As Object = {"pvch_TipoOperacion", strTipoOperacion, _
                                                 "pint_CodigoDocumento", strCodigoDocumento, _
                                                 "pvch_TipoDocumento", strTipoDocumento, _
                                                 "pvch_FechaDocumento", strFechaDocumento, _
                                                 "pvch_CodigoDestino", strCodigoDestino, _
                                                 "pvch_CodigoMotivo", strCodigoMotivo, _
                                                 "pvch_CodigoCliente", strCodigoCliente, _
                                                 "pvch_NombreCliente", strNombreCliente, _
                                                 "pvch_Observaciones", strObservaciones, _
                                                 "pvch_CodigoUsuario", strCodigoUsuario, _
                                                 "pvch_DatosRollos_XML", strDatosRollosXML}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_REGISTRAR_DOCUMENTO_MOVIMIENTO_ALMACEN_TELAS", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try
        End Function
        Public Function ufn_ObtenerDatosRolloDocumentoMovimientoAlmacenTelas(ByVal strCodigoDocumento As String, ByVal strTipoDocumento As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvch_CodigoDocumento", strCodigoDocumento, _
                                                 "pvch_TipoDocumento", strTipoDocumento}
                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_OBTENER_DATOS_ROLLO_DOCUMENTO_MOVIMIENTO_ALMACEN_TELAS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function Eliminar_Rollo_Documento(ByVal strCodigoRollo As String, ByVal strTipoDocumento As String) As Integer
            Dim objParametros() As Object = {"codigo_rollo", strCodigoRollo, _
                                             "tipo_documento", strTipoDocumento}
            Try
                Return m_sqlDtAccRevFin.EjecutarComando("USP_RVF_ELIMINA_ROLLO_DEVOLUCION", objParametros)
            Catch ex As Exception
                Throw ex
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

        Public Function ufn_RegistrarDocumentoMovimientoAlmacenTelas_V2(ByVal strTipoOperacion As String,
                                                                        ByVal strCodigoDocumento As String,
                                                                        ByVal strTipoDocumento As String,
                                                                        ByVal strFechaDocumento As String,
                                                                        ByVal strCodigoDestino As String,
                                                                        ByVal strCodigoMotivo As String,
                                                                        ByVal strCodigoCliente As String,
                                                                        ByVal strNombreCliente As String,
                                                                        ByVal strObservaciones As String,
                                                                        ByVal strCodigoUsuario As String,
                                                                        ByVal pdtListaRollos As DataTable) As DataTable

            Dim objUtil As New Util
            Dim dtResult As DataTable

            Try
                Dim strDatosRollosXML As New StringBuilder(objUtil.GeneraXml(pdtListaRollos))                
                Dim objParametros() As Object = {"pvch_TipoOperacion", strTipoOperacion, _
                                                 "pint_CodigoDocumento", strCodigoDocumento, _
                                                 "pvch_TipoDocumento", strTipoDocumento, _
                                                 "pvch_FechaDocumento", strFechaDocumento, _
                                                 "pvch_CodigoDestino", strCodigoDestino, _
                                                 "pvch_CodigoMotivo", strCodigoMotivo, _
                                                 "pvch_CodigoCliente", strCodigoCliente, _
                                                 "pvch_NombreCliente", strNombreCliente, _
                                                 "pvch_Observaciones", strObservaciones, _
                                                 "pvch_CodigoUsuario", strCodigoUsuario, _
                                                 "pvch_DatosRollos_XML", strDatosRollosXML.ToString}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_REGISTRAR_DOCUMENTO_MOVIMIENTO_ALMACEN_TELAS_V2", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try
        End Function

        Public Function ufn_BuscarDatosTransferenciaTela_Etiqueta(ByVal strTipoDocumento As String,
                                                                   ByVal strCodigoDocumento As String) As DataTable

            Dim dtResult As DataTable

            Try
                Dim objParametros() As Object = {"pvch_TipoDocumento", strTipoDocumento,
                                                 "pvch_CodigoDocumento", strCodigoDocumento}

                dtResult = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_OBTENER_DATOS_TRANSFERENCIA_TELA_ETIQUETA", objParametros)
                Return dtResult
            Catch ex As Exception
                Throw ex
            Finally
                dtResult = Nothing
            End Try
        End Function

    End Class
End Namespace
