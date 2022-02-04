Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class OrdenProduccion
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private _strEstado As String
        Private _strCodigoOrden As String
        Private _strCodigoCliente As String
        Private _strPedido As String
        Private _strEstadoRechazo As String
        Private _strUsuario As String
        Private _strArticulo As String
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region "PROPIEDADES"
        Public Property Estado() As String
            Get
                Return _strEstado
            End Get
            Set(ByVal Value As String)
                _strEstado = Value
            End Set
        End Property
        Public Property CodigoOrden() As String
            Get
                Return _strCodigoOrden
            End Get
            Set(ByVal Value As String)
                _strCodigoOrden = Value
            End Set
        End Property
        Public Property Pedido() As String
            Get
                Return _strPedido
            End Get
            Set(ByVal Value As String)
                _strPedido = Value
            End Set
        End Property
        Public Property EstadoRechazo() As String
            Get
                Return _strEstadoRechazo
            End Get
            Set(ByVal Value As String)
                _strEstadoRechazo = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
        Public Property Articulo() As String
            Get
                Return _strArticulo
            End Get
            Set(ByVal Value As String)
                _strArticulo = Value
            End Set
        End Property
        Public Property CodigoCliente() As String
            Get
                Return _strCodigoCliente
            End Get
            Set(ByVal Value As String)
                _strCodigoCliente = Value
            End Set
        End Property
#End Region
#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function ObtenesOrdenesProduccion() As DataTable
            Dim pdtOrdenes As DataTable

            Try
                pdtOrdenes = m_sqlDtAccTintoreria.ObtenerDataTable("ObtenerOrdenesProduccion")
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtOrdenes
        End Function

        Public Function ListarPedidos() As DataTable
            Dim pdtPedidos As DataTable

            Try
                pdtPedidos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_ObtenerPedidos")
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtPedidos
        End Function

        Public Function ListarPedidosPorAprobar() As DataTable
            Dim pdtPedidos As DataTable

            Try
                pdtPedidos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_NoAprobados")
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtPedidos
        End Function

        Public Function ListarOrdenesNuevas(ByVal pFecha As String) As DataTable
            Dim pdtPedidos As DataTable

            Dim objParametros() As Object = {"fecha_generacion", pFecha}

            Try
                pdtPedidos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Consulta", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtPedidos
        End Function

        Public Function ObtenerAdicionales(ByVal pOrden As String, ByVal pArticulo As String) As DataTable
            Dim pdtAdicionales As DataTable

            Dim objParametros() As Object = {"CODIGO_ORDEN", pOrden, "CODIGO_ARTICULO", pArticulo}

            Try
                pdtAdicionales = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_ObtenerAdicionales", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtAdicionales
        End Function

        Public Function ObtenerDatosAdicionales(ByVal pOrden As String, ByVal pArticulo As String) As DataTable
            Dim dtbAdicionales As DataTable
            Dim objParametros() As Object = {"p_var_CodigoOrden", pOrden, "p_var_CodigoArticulo", pArticulo}
            Try
                dtbAdicionales = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerAdicionalesOrden", objParametros)
                Return dtbAdicionales
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosAdicionalesEnProceso(ByVal pOrden As String, ByVal pArticulo As String) As DataTable
            Dim dtbAdicionales As DataTable
            Dim objParametros() As Object = {"p_var_CodigoOrden", pOrden, "p_var_CodigoArticulo", pArticulo}
            Try
                dtbAdicionales = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerAdicionalesOrdenEnProceso", objParametros)
                Return dtbAdicionales
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ExisteProduccionPostTenido(ByVal strCodigoOrden As String, ByVal strCodigoArticulo As String) As Boolean
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As Object = {"var_CodigoOrden", strCodigoOrden, "var_CodigoArticulo", strCodigoArticulo}
            Try
                Dim int_Existencia As Int16 = CType(m_sqlDtAccTintoreria.ObtenerValor("usp_TIN_ArticuloPostTenido_Ruta", objParametros), Int16)
                Return (int_Existencia > 0)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosAdicionalesPedido(ByVal pArticulo As String) As DataTable
            Dim dtbAdicionales As DataTable
            Dim objParametros() As Object = {"p_var_CodigoArticulo", pArticulo}
            Try
                dtbAdicionales = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerExistenciaArticulo", objParametros)
                Return dtbAdicionales
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListarCaracteristicas(ByVal pArticulo As String) As DataTable
            Dim pdtCaracteristicas As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                pdtCaracteristicas = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_ObtenerCaracteristica", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtCaracteristicas
        End Function

        Function Listar(ByVal pOrden As String) As DataTable
            Dim pdtOrden As DataTable

            Dim objParametros() As Object = {"codigo_orden", pOrden}

            Try
                pdtOrden = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_SelectId", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtOrden
        End Function

        Function ObtenerAutogenerado() As String
            Dim pResultado As String

            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_OrdenProduccion_Secuencial").ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Function ObtenerMetraje(ByVal pArticulo As String) As String
            Dim pResultado As String

            Dim objParametros() As String = {"codigo_articulo", pArticulo}

            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_OrdenProduccion_ObtenerMetraje", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Function GrabarCabeceraDetalle(ByVal pdtDatos As DataTable, ByVal pdtDetalle As DataTable, ByRef pdtErrores As DataTable) As Boolean

            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util
            Dim pXmlCabecera As String
            Dim pXmlDetalle As String

            pXmlCabecera = objXml.GeneraXml(pdtDatos)
            pXmlDetalle = objXml.GeneraXml(pdtDetalle)

            Dim objParametros() As Object = {"xml_cabecera", pXmlCabecera, "xml_detalle", pXmlDetalle}

            Try
                pdtErrores = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_InsertXML", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True

        End Function

        Function GrabarCabeceraDetalleNuevo(ByVal pdtDatos As DataTable, ByVal pdtDetalle As DataTable) As Boolean

            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util
            Dim pXmlCabecera As String
            Dim pXmlDetalle As String

            pXmlCabecera = objXml.GeneraXml(pdtDatos)
            pXmlDetalle = objXml.GeneraXml(pdtDetalle)

            Dim objParametros() As Object = {"xml_cabecera", pXmlCabecera, "xml_detalle", pXmlDetalle}

            Try
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_OrdenProduccionNuevo_InsertXML", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True

        End Function

        Public Function ListarDetalle(ByVal pArticulo As String, ByVal pPedido As String) As DataTable
            Dim pdtDetalle As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "numero_pedido", pPedido}

            Try
                pdtDetalle = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Detalle_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtDetalle
        End Function

        Public Function ListarDetalleNuevo(ByVal pFecha As String) As DataTable
            Dim pdtDetalle As DataTable

            Dim objParametros() As Object = {"fecha_generacion", CType(pFecha, DateTime)}

            Try
                pdtDetalle = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Detalle_Consulta", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtDetalle
        End Function

        Public Function ObtenerDetalleModificacion(ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_articulo", strCodigoArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("UP_ObtenerDetalleOrdenPorArticulo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerPorAprobar() As DataTable
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Consulta_PorAprobar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerPorAprobar1() As DataTable
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Consulta_PorAprobar1")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerOrdenesPorAprobarNivel2() As DataTable
            Try
                Return ObtenerOrdenesPorEstado("A1", "SR")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerOrdenesPorAprobarNivel1(ByVal pstrRechazado As String) As DataTable
            Try
                Return ObtenerOrdenesPorEstado("SA", pstrRechazado)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerOrdenesPorEstado(ByVal pstrEstado As String, Optional ByVal pstrRechazado As String = "SR") As DataTable
            Try
                Dim objParametros() As Object = {"p_var_EstadoOrden", pstrEstado, "p_var_EstadoRechazo", pstrRechazado}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerOrdenPorEstado", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub CambiarEstado(ByVal strCodigoOrden As String, ByVal strEstado As String, ByVal strObservaciones As String, ByVal strEstadoAnterior As String)
            Try
                Dim objParametros As Object() = {"codigo_orden", strCodigoOrden, "estado_orden", strEstado, "observaciones_orden", strObservaciones, "rechazado", strEstadoAnterior}

                m_sqlDtAccTintoreria.EjecutarComando("UP_CambiarEstadoOrdenProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub CambiarEstado(ByVal pstrCodigoOrden As String, ByVal pstrEstado As String, _
        ByVal pstrObservaciones As String, ByVal pstrEstadoRechazado As String, ByVal pstrUsuario As String)
            Try
                Dim objParametros As Object() = {"p_var_CodigoOrden", pstrCodigoOrden, _
                "p_chr_EstadoOrden", pstrEstado, "p_var_Observacion", pstrObservaciones, _
                "p_var_EstadoRechazado", pstrEstadoRechazado, "p_var_Usuario", pstrUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("usp_upd_CambiarEstadoOrden", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub CambiarEstadoOFIVENT(ByVal strCodigoOrden As String, ByVal strEstado As String, _
        ByVal strObservaciones As String, ByVal strEstadoAnterior As String, ByVal strNumeroPedido As String, _
        ByVal strAprob1 As String, ByVal strAprob2 As String, ByVal strAprob3 As String, ByVal strAprob4 As String, _
        ByVal strUsuario As String)
            Try
                Dim objParametros As Object() = {"codigo_orden", strCodigoOrden, _
                   "estado_orden", strEstado, "observaciones_orden", strObservaciones, _
                   "rechazado", strEstadoAnterior, "numero_pedido", strNumeroPedido, _
                   "aprob1", strAprob1, "aprob2", strAprob2, "aprob3", strAprob3, _
                   "aprob4", strAprob4, "Usuario", strUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("UP_CambiarEstadoOrdenProduccionOFIVENT", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub
        Public Sub RecalculoOrden(ByVal strCodigoOrden As String)
            Try
                Dim objParametros As Object() = {"var_CodigoOrden", strCodigoOrden}

                m_sqlDtAccTintoreria.EjecutarComando("usp_TIN_RecalcularMovimientoOrden", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Sub metodito(ByVal a As Integer, ByVal b As Integer)

        End Sub
        Public Sub ActualizarOrdenProduccion(ByVal pstrCodigoOrden As String, _
        ByVal pstrCodigoArticulo As String, ByVal pstrCodigoTipoAcabado As String, _
        ByVal pstrCodigoCliente As String, ByVal pstrDataXML As String)
            Try
                Dim objParametros As Object() = {"p_var_CodigoOrden", pstrCodigoOrden, _
                "p_var_CodigoArticulo", pstrCodigoArticulo, "p_var_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                "p_var_CodigoCliente", pstrCodigoCliente, "p_txt_DetalleXML", pstrDataXML, _
                "p_var_EstadoOrden", _strEstado, "p_var_Usuario", _strUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("usp_upd_ActualizarOrdenProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub ActualizarOrdenProduccionEnProceso(ByVal pstrCodigoOrden As String, _
        ByVal pstrCodigoArticulo As String, ByVal pstrCodigoTipoAcabado As String, _
        ByVal pstrCodigoCliente As String, ByVal pstrDataXML As String, ByVal pstrUsuario As String)
            Try
                Dim objParametros As Object() = {"p_var_CodigoOrden", pstrCodigoOrden, _
                "p_var_CodigoArticulo", pstrCodigoArticulo, "p_var_CodigoTipoAcabado", pstrCodigoTipoAcabado, _
                "p_var_CodigoCliente", pstrCodigoCliente, "p_txt_DetalleXML", pstrDataXML, _
                "p_var_Usuario", pstrUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("usp_upd_ActualizarOrdenProduccion_EnProceso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub GrabarOrdenesProduccionBatch(ByVal usuario As String, ByVal dtblOrdenesProduccion As DataTable, ByVal dstDetallesOrdenesProduccion As DataSet)
            Try
                Dim objParametros As Object() = {"usuario", usuario, "xml_data", ObtenerXMLModificacion(dtblOrdenesProduccion, dstDetallesOrdenesProduccion)}
                m_sqlDtAccTintoreria.EjecutarComando("UP_ModificarOrdenProduccionXMLBatch", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub GrabarOrdenesProduccionBatch2(ByVal usuario As String, ByVal drowOrdenProduccion As DataRow, ByVal dtblDetallesOrdenProduccion As DataTable)
            Try
                Dim objParametros As Object() = {"usuario", usuario, "xml_data", ObtenerXMLModificacion2(drowOrdenProduccion, dtblDetallesOrdenProduccion)}
                m_sqlDtAccTintoreria.EjecutarComando("UP_ModificarOrdenProduccionXMLBatchV2", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub GrabarOrdenesProduccionBatchSOME2(ByVal usuario As String, ByVal drowOrdenProduccion As DataRow, ByVal dtblDetallesOrdenProduccion As DataTable)
            Try
                Dim objParametros As Object() = {"usuario", usuario, "xml_data", ObtenerXMLModificacion2(drowOrdenProduccion, dtblDetallesOrdenProduccion)}
                m_sqlDtAccTintoreria.EjecutarComando("UP_ModificarOrdenProduccionXMLBatch_SOME_V2", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Public Sub GrabarOrdenesProduccionBatch_Aprobadas(ByVal usuario As String, ByVal dtblOrdenesProduccion As DataTable, ByVal dstDetallesOrdenesProduccion As DataSet, ByVal strAprob1 As String, ByVal strAprob2 As String, ByVal strAprob3 As String, ByVal strAprob4 As String)
            Try
                Dim objParametros As Object() = {"xml_data", ObtenerXMLModificacion(dtblOrdenesProduccion, dstDetallesOrdenesProduccion), "aprob1", strAprob1, "aprob2", strAprob2, "aprob3", strAprob3, "aprob4", strAprob4, "usuario", usuario}
                m_sqlDtAccTintoreria.EjecutarComando("UP_ModificarOrdenProduccionXMLBatch_APROBADAS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function ObtenerXMLModificacion(ByVal dtblOrdenesProduccion As DataTable, ByVal dstDetallesOrdenesProduccion As DataSet) As String
            Dim xmlDoc As New XmlDocument
            Dim objXML As New NM_General.Util
            xmlDoc.LoadXml("<root></root>")

            For i As Integer = 0 To dtblOrdenesProduccion.Rows.Count - 1
                Dim xmlNodoOrdenProduccion As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ordenProduccion", [String].Empty)
                Dim xmlElementoOrdenProduccion As XmlElement

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_orden").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_orden").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_orden"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("numero_pedido").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("numero_pedido").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("numero_pedido"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("caracteristica_articulo").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("caracteristica_articulo").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("caracteristica_articulo"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_proceso").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_proceso").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_proceso"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_tipooperacion").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_tipooperacion").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_tipooperacion"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_orden_nuevo").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_orden_nuevo").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_orden_nuevo"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_cliente").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_cliente").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_cliente"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_acabado").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_acabado").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_acabado"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("codigo_articulo").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_articulo").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_articulo"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                xmlElementoOrdenProduccion = xmlDoc.CreateElement(dtblOrdenesProduccion.Columns("indicacion_envoltura").ColumnName)
                xmlElementoOrdenProduccion.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("indicacion_envoltura").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("indicacion_envoltura"))).ToString.Trim
                xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

                For j As Integer = 0 To dstDetallesOrdenesProduccion.Tables(i).Rows.Count - 1
                    Dim xmlNodoDetalle As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "detalleOrden", [String].Empty)
                    Dim xmlElememtoDetalle As XmlElement

                    xmlElememtoDetalle = xmlDoc.CreateElement("codigo_orden")
                    xmlElememtoDetalle.InnerText = IIf(dtblOrdenesProduccion.Rows(i)("codigo_orden_nuevo").Equals(Convert.DBNull), String.Empty, Convert.ToString(dtblOrdenesProduccion.Rows(i)("codigo_orden_nuevo"))).ToString.Trim
                    xmlNodoDetalle.AppendChild(xmlElememtoDetalle)

                    For c As Integer = 0 To dstDetallesOrdenesProduccion.Tables(i).Columns.Count - 1
                        xmlElememtoDetalle = xmlDoc.CreateElement(dstDetallesOrdenesProduccion.Tables(i).Columns(c).ColumnName)
                        xmlElememtoDetalle.InnerText = IIf(dstDetallesOrdenesProduccion.Tables(i).Rows(j)(c).Equals(Convert.DBNull), String.Empty, Convert.ToString(dstDetallesOrdenesProduccion.Tables(i).Rows(j)(c))).ToString.Trim
                        xmlNodoDetalle.AppendChild(xmlElememtoDetalle)
                    Next c

                    xmlNodoOrdenProduccion.AppendChild(xmlNodoDetalle)
                Next j

                xmlDoc.DocumentElement.AppendChild(xmlNodoOrdenProduccion)
            Next i

            Return objXML.EncodeXML(xmlDoc.OuterXml)
        End Function

        Public Function ObtenerXMLModificacion2(ByVal drowOrdenProduccion As DataRow, ByVal dtblDetallesOrdenProduccion As DataTable) As String
            Dim xmlDoc As New XmlDocument
            Dim objXML As New NM_General.Util

            xmlDoc.LoadXml("<root></root>")

            Dim xmlNodoOrdenProduccion As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "ordenProduccion", [String].Empty)
            Dim xmlElementoOrdenProduccion As XmlElement

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_orden")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_orden").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_orden"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("numero_pedido")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("numero_pedido").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("numero_pedido"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("caracteristica_articulo")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("caracteristica_articulo").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("caracteristica_articulo"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_proceso")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_proceso").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_proceso"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_tipooperacion")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_tipooperacion").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_tipooperacion"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_orden_nuevo")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_orden_nuevo").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_orden_nuevo"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_cliente")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_cliente").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_cliente"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_acabado")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_acabado").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_acabado"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("codigo_articulo")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("codigo_articulo").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_articulo"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)

            xmlElementoOrdenProduccion = xmlDoc.CreateElement("indicacion_envoltura")
            xmlElementoOrdenProduccion.InnerText = IIf(drowOrdenProduccion("indicacion_envoltura").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("indicacion_envoltura"))).ToString.Trim
            xmlNodoOrdenProduccion.AppendChild(xmlElementoOrdenProduccion)
            Dim drowDetalle As DataRow

            For Each drowDetalle In dtblDetallesOrdenProduccion.Rows
                Dim xmlNodoDetalle As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, "detalleOrden", [String].Empty)
                Dim xmlElememtoDetalle As XmlElement

                xmlElememtoDetalle = xmlDoc.CreateElement("codigo_orden")
                xmlElememtoDetalle.InnerText = IIf(drowOrdenProduccion("codigo_orden_nuevo").Equals(Convert.DBNull), String.Empty, Convert.ToString(drowOrdenProduccion("codigo_orden_nuevo"))).ToString.Trim
                xmlNodoDetalle.AppendChild(xmlElememtoDetalle)

                For c As Integer = 0 To dtblDetallesOrdenProduccion.Columns.Count - 1
                    xmlElememtoDetalle = xmlDoc.CreateElement(dtblDetallesOrdenProduccion.Columns(c).ColumnName)
                    xmlElememtoDetalle.InnerText = IIf(drowDetalle(c).Equals(Convert.DBNull), String.Empty, Convert.ToString(drowDetalle(c))).ToString.Trim
                    xmlNodoDetalle.AppendChild(xmlElememtoDetalle)
                Next c

                xmlNodoOrdenProduccion.AppendChild(xmlNodoDetalle)
            Next
            xmlDoc.DocumentElement.AppendChild(xmlNodoOrdenProduccion)

            Return objXML.EncodeXML(xmlDoc.OuterXml)
        End Function

        Public Function ObtenerDetalles(ByVal strCodigosOrdenes As String, ByVal intNumeroDetalles As Integer) As DataSet
            Try
                Dim objParametros As Object() = {"codigos_ordenes", strCodigosOrdenes, "numero_ordenes", intNumeroDetalles}
                Return m_sqlDtAccTintoreria.ObtenerDataSet("UP_ObtenerDetallesOrdelProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Modificar(ByVal dtblOrdenProduccion As DataTable)
            'Dim objXML As New generaXml
            Dim objXML As New NM_General.Util

            Try
                Dim objParametros As Object() = {"xml_data", objXML.GeneraXml(dtblOrdenProduccion)}
                m_sqlDtAccTintoreria.EjecutarComando("UP_ModificarOrdenProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub ModificarOrdenesN2(ByVal pstrDatos As String, ByVal pstrUsuario As String)
            Try
                Dim objParametros As Object() = {"p_var_DatosXML", pstrDatos, "p_var_Usuario", pstrUsuario}
                m_sqlDtAccTintoreria.EjecutarComando("usp_prc_ModificarOrdenProduccionBatch", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Public Function ObtenerPorFecha(ByVal strFecha As String, ByVal strFechaFin As String, ByVal strOrden As String, ByVal strArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"fecha_generacion", strFecha.Trim, "fecha_generacion_fin", strFechaFin.Trim, "codigo_orden", strOrden, "codigo_articulo", strArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Consulta_PorAprobar0", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerPorFechaAprobadas(ByVal strFecha As String, ByVal strFechaFin As String, ByVal strOrden As String, ByVal strArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"fecha_generacion", strFecha.Trim, "fecha_generacion_fin", strFechaFin.Trim, "codigo_orden", strOrden, "codigo_articulo", strArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_Consulta_vigentes", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerOrdenesEnProceso(ByVal strFecha As String, ByVal strFechaFin As String, ByVal strOrden As String, ByVal strArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"p_var_FechaInicio", strFecha.Trim, "p_var_FechaFinal", strFechaFin.Trim, "p_var_CodigoOrden", strOrden, "p_var_CodigoArticulo", strArticulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_TIN_ObtenerOrdenesEnProceso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function anular(ByVal strCodigoOrden As String, ByVal strObservaciones As String, ByVal strUsuario As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_orden", strCodigoOrden, "observaciones", strObservaciones, "codigo_usuario", strUsuario}
                Dim ldtbRetornar As DataTable
                ldtbRetornar = m_sqlDtAccTintoreria.ObtenerDataTable("up_AnularOrdenProduccion", objParametros)
                Return ldtbRetornar
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Validar(ByVal pProceso As String, ByVal pTipoOperacion As String) As String
            Try
                Dim objParametros As Object() = {"Proceso", pProceso, "TipoOperacion", pTipoOperacion}

                Return Convert.ToString(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Validacion_Obtener", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerOrdenDetalle(ByVal strCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_orden", strCodigo}

                Return m_sqlDtAccTintoreria.ObtenerDataTable("UP_ObtenerDetalleOrdenV2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerOrdenDetalle_V1(ByVal pstrCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"p_var_CodigoOrden", pstrCodigo}

                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerOrdenDetalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerOrdenDetalle_V2(ByVal pstrCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"p_var_CodigoOrden", pstrCodigo}

                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerOrdenEnProcesoDetalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'Guido ----------------------------------------------------------------------------
        Public Function ObtenerOrdenDetalleV3(ByVal strCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_orden", strCodigo}

                Return m_sqlDtAccTintoreria.ObtenerDataTable("UP_ObtenerDetalleOrdenV3", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function VerificaExistArticuloTinto(ByVal codigo_orden As String, ByVal codigo_articulo As String) As Boolean
            Try
                Dim objParametros As Object() = {"codigo_orden", codigo_orden, "codigo_articulo", codigo_articulo}

                If CStr(m_sqlDtAccTintoreria.ObtenerValor("spSEL_VerificaExistArticuloTinto", objParametros)).Equals("1") Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Sub spUDP_ModificarOrdenDetalle_conPROCTinto(ByVal codigo_orden As String, ByVal codigo_articulo_largo As String, ByVal metros_pedidos As String, ByVal precio As String, ByVal usuario As String)
            Try
                Dim objParametros As Object() = {"codigo_orden", codigo_orden, "codigo_articulo_largo", codigo_articulo_largo, "metros_pedidos", metros_pedidos, "precio", precio, "usuario", usuario}
                m_sqlDtAccTintoreria.EjecutarComando("spUDP_ModificarOrdenDetalle_conPROCTinto", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub spUDP_ModificarOrdenDetalle_sinPROCTinto(ByVal codigo_orden As String, ByVal codigo_articulo_anterior As String, ByVal codigo_articulo_largo As String, ByVal codigo_color As String, ByVal codigo_colorante As String, ByVal codigo_combinacion_real As String, ByVal codigo_combinacion As String, ByVal codigo_proceso_acabado As String, ByVal codigo_ligamiento_acabado As String, ByVal codigo_descripcion_acabado As String, ByVal metros_pedidos As String, ByVal metros_etiquetados As String, ByVal Calidad As String, ByVal precio As String, ByVal usuario As String)
            Try
                Dim objParametros As Object() = {"codigo_orden", codigo_orden, "codigo_articulo_anterior", codigo_articulo_anterior, "codigo_articulo_largo", codigo_articulo_largo, "codigo_color", codigo_color, "codigo_colorante", codigo_colorante, "codigo_combinacion_real", codigo_combinacion_real, "codigo_combinacion", codigo_combinacion, "codigo_proceso_acabado", codigo_proceso_acabado, "codigo_ligamiento_acabado", codigo_ligamiento_acabado, "codigo_descripcion_acabado", codigo_descripcion_acabado, "metros_pedidos", metros_pedidos, "metros_etiquetados", metros_etiquetados, "Calidad", Calidad, "precio", precio, "usuario", usuario}
                m_sqlDtAccTintoreria.EjecutarComando("spUDP_ModificarOrdenDetalle_sinPROCTinto", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        'Guido ----------------------------------------------------------------------------
        Public Function VerificaPedidoVenta(ByVal pCodigoPedidoVenta As String) As String
            Try
                Dim objParametros As Object() = {"pedido_venta", pCodigoPedidoVenta}

                Return Convert.ToString(m_sqlDtAccTintoreria.ObtenerValor("pr_NM_Verificar_PedidoVenta", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    Public Function ObtenerDatosFiltro() As DataTable
      Try
        Return m_sqlDtAccTintoreria.ObtenerDataTable("TNT_SP_REPORTE_PVOP")
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function fnc_listainformacionretrasosxop(ByVal pint_tipoconsulta As Int16, ByVal pstr_numeroop As String, ByVal pstr_codigoarticulo As String, ByVal pstr_codigocliente As String, ByVal pstr_prioridad As String, ByVal pstr_ordenar As String) As DataTable
      Dim lobjParametros() As Object = {"ptin_tipoconsulta", pint_tipoconsulta, "pvch_codigoorden", pstr_numeroop, "pvch_codigoarticulo", pstr_codigoarticulo, "pvch_codigocliente", pstr_codigocliente, "ptin_prioridad", pstr_prioridad, "ptin_ordernar", pstr_ordenar}
      Try
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_listainfretrazosxop", lobjParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function fnc_guardarinformacionretrasosxop(ByVal pint_accion As Int16, ByVal pstr_numeroop As String, ByVal pstr_codigoarticulo As String, ByVal pint_prioridad As Int16, ByVal pstr_fechasolic As String, ByVal pstr_fechaaprox As String, ByVal pstr_observacionvent As String, ByVal pstr_observacionprod As String, ByVal pstr_metadata As String) As DataTable
      Dim lobjParametros() As Object = {"ptin_accion", pint_accion, "pvch_codigoorden", pstr_numeroop, "pvch_codigoarticulo", pstr_codigoarticulo, "ptin_prioridad", pint_prioridad, "pvch_fechasoli", pstr_fechasolic, "pvch_fechaaprox", pstr_fechaaprox, "pvch_observacionvent", pstr_observacionvent, "pvch_observacionprod", pstr_observacionprod, "pvch_metadata", pstr_metadata}
      Try
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_guardarinfretrazosxop", lobjParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

#Region "Henry Ortiz"
    Public Function ObtenerResumenOrden(ByVal strCodigoOrden As String) As DataTable
      Dim objParametros() As Object = {"codigo_orden", strCodigoOrden}
      Return m_sqlDtAccTintoreria.ObtenerDataTable("PR_NM_REP_RESUMENORDEN", objParametros)
    End Function

    Public Function ObtenerOrdenProduccion(ByVal pstrCodigoOrden As String) As DataTable
      Dim objParametros() As Object = {"p_var_CodigoOrden", pstrCodigoOrden}
      Try
        Dim dtbDatos As DataTable = m_sqlDtAccTintoreria.ObtenerDataTable("usp_qry_ObtenerOrdenProduccion", objParametros)
        If dtbDatos.Rows.Count = 1 Then
          For Each dtrItem As DataRow In dtbDatos.Rows
            Me._strEstado = dtrItem("estado_orden")
            Me._strArticulo = dtrItem("codigo_articulo")
            Me._strEstadoRechazo = dtrItem("rechazado")
            Me._strCodigoCliente = dtrItem("codigo_cliente")
            Me._strCodigoOrden = dtrItem("codigo_orden")
            Me._strPedido = dtrItem("numero_pedido")
          Next
        End If
        Return dtbDatos
      Catch ex As Exception
        Throw ex
      Finally
        objParametros = Nothing
      End Try
    End Function

    Public Function ObtenerPedidosConvertirOP() As DataTable
      Try
        m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_VEN_PedidosAOrden_Listar")
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ValidarIntegridadPedido(ByVal strNumeroPedido As String) As DataTable
      Try
        Dim objParametros() As Object = {"var_CodigoPedido", strNumeroPedido}
        m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_VEN_PedidosAOrden_Verificar", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function TransformarPedidoOrden(ByVal dtbPedidos As DataTable, ByVal strUsuario As String) As DataTable
      Try
        Dim objUtil As New NM_General.Util
        dtbPedidos.TableName = "Pedidos"
        Dim strXMLPedido As String = objUtil.GeneraXml(dtbPedidos)
        Dim objParametros() As Object = {"var_XMLPedidos", strXMLPedido, "var_Usuario", strUsuario}
        m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim dtbResultado As DataTable = m_sqlDtAccTintoreria.ObtenerDataTable("usp_VEN_PedidosAOrden_Transformar", objParametros)
        If Not dtbResultado Is Nothing Then
          Return dtbResultado
        End If
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ObtenerPedidosDetalle(ByVal strNumeroPedido As String) As DataSet
      Try
        Dim objParametros() As Object = {"var_CodigoPedido", strNumeroPedido}
        m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Return m_sqlDtAccTintoreria.ObtenerDataSet("usp_VEN_ObtenerPedidoVenta", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function ObtenerPedidosStock(ByVal strCodigoCliente As String) As DataTable
      Try
        Dim objParametros() As Object = {"var_CodigoCliente", strCodigoCliente}
        m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_VEN_PedidoStock_Listar", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function AprobarPedidoStock(ByVal dtbPedidos As DataTable, ByVal strUsuario As String) As Boolean
      Try
        Dim objUtil As New NM_General.Util
        dtbPedidos.TableName = "Pedidos"
        Dim strXMLPedido As String = objUtil.GeneraXml(dtbPedidos)
        Dim objParametros() As Object = {"var_XMLPedidos", strXMLPedido, "var_Usuario", strUsuario}
        m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        m_sqlDtAccTintoreria.EjecutarComando("usp_VEN_PedidoStock_Aprobar", objParametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function
#End Region

  End Class

End Namespace