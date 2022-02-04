'Option Strict On

Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.io

Namespace NM.RevisionFinal
    Public Class Entrega
        Implements IDisposable

        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function ObtenerNumeroEntrega() As DataTable
            Try
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("ObtenerNumeroEntrega")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListarDetalle(ByVal pEntrega As String) As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                Dim objParametros() As Object = {"codigo_entrega", pEntrega}
                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("ObtenerEntregaDetalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function
        'Fecha: 30-11-2004
        Public Function obtenerRollo(ByVal strRollo As String) As DataRow
            Return (New Rollo).ObtenerRollosPorCodigo(strRollo).Rows(0) '("metraje_total")
        End Function
        'Fecha: 30-11-2004
        Public Function verificaLotexArticulo(ByVal strArticulo As String, ByVal strLote As String) As String()
            Dim dtbError As DataTable
            Try
                Dim objParametros() As Object = {"K_VC_CO_ITEM", strArticulo, "K_VC_CO_LOTE", strLote}
                dtbError = m_sqlDtAccOfiPlan.ObtenerDataTable("RVF_SP_VERIFICA_LOTExARTICULO", objParametros)

                Dim intError As Integer, strError As String

                If Not dtbError Is Nothing Then
                    intError = IIf(IsDBNull(dtbError.Rows(0).Item("IN_ERROR")), -1, CInt(dtbError.Rows(0).Item("IN_ERROR")))
                    strError = IIf(IsDBNull(dtbError.Rows(0).Item("VC_ERROR")), "", dtbError.Rows(0).Item("VC_ERROR"))
                End If

                Dim retorno As String() = {CStr(intError), strError}

                Return retorno
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'Fecha: 30-11-2004
        Public Function validaEntregaTelaAlmacen(ByVal strArticulo As String, ByVal strLote As String) As String()
            Dim dtbError As DataTable
            Try
                Dim objParametros() As Object = {"K_VC_CO_ITEM", strArticulo, "K_VC_CO_LOTE", strLote}
                dtbError = m_sqlDtAccOfiPlan.ObtenerDataTable("RVF_SP_VALIDA_ENTREGA_TELA_ALMACEN", objParametros)

                Dim intError As Integer, strError As String

                If Not dtbError Is Nothing Then
                    intError = IIf(IsDBNull(dtbError.Rows(0).Item("IN_NRO_ERROR")), -1, CInt(dtbError.Rows(0).Item("IN_NRO_ERROR")))
                    strError = IIf(IsDBNull(dtbError.Rows(0).Item("VC_DES_ERROR")), "", dtbError.Rows(0).Item("VC_DES_ERROR"))
                End If

                Dim retorno As String() = {CStr(intError), strError}

                Return retorno

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'Autor       : Jorge Romaní
        'Descripción : Obtiene los datos de una entrega de rev. fin. a almacen.
        'Fecha       : 27-12-2004
        Public Function obtenerDatosEntregaAlmacen(ByVal strCodigoEntrega As String) As DataTable
            Dim objParametros() As Object = {"K_VC_CODIGO_ENTREGA", strCodigoEntrega}
            Try
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("RVF_SP_ENTREGA_ALMACEN", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function obtenerReporteEntregaAlmacen(ByVal strCodigoEntrega As String) As DataTable
            Dim objParametros() As Object = {"K_VC_CODIGO_ENTREGA", strCodigoEntrega, "K_CH_ORDEN", 1}
            Try
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("RVF_SP_FICHA_ENTREGA_V2", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function obtenerReporteTransferenciaAlmacen(ByVal strCodigoEntrega As String) As DataTable
            Dim objParametros() As Object = {"K_VC_CODIGO_ENTREGA", strCodigoEntrega, "K_CH_ORDEN", 1}
            Try
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_IMPRESION_TRANSFERENCIA_ALMACEN", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        'Autor       : Jorge Romaní
        'Descripción : Obtiene número de entrega correlativo disponible para insertar.
        'Fecha       : 28-12-2004
        Public Function obtenerNumeroEntregaNuevo() As String
            Dim strNuevoCodigo As String = ""
            Try
                Dim dtbNumEntrega As DataTable = m_sqlDtAccOfiPlan.ObtenerDataTable("ObtenerNumeroEntrega")
                If Not dtbNumEntrega Is Nothing Then
                    If dtbNumEntrega.Rows.Count > 0 Then
                        If Not IsDBNull(dtbNumEntrega.Rows(0).Item("codigo_entrega")) Then
                            strNuevoCodigo = CStr(dtbNumEntrega.Rows(0).Item("codigo_entrega"))
                        End If
                    End If
                End If
                Return strNuevoCodigo
            Catch ex As Exception
                Return strNuevoCodigo
            End Try
        End Function
        Public Function RegistrarEntrega(ByVal strOperacion As String, ByVal strEntrega As String, ByVal strFecha As String, ByVal strCodigoOrden As String, ByVal strCodigoDestino As String, ByVal strCodigoEstado As String, ByVal strUsuario As String, ByVal dtDatos As DataTable, ByVal dtblDatosOfisis As DataTable) As String()
            Dim strXml, strXmlOfisis As String
            strXml = GeneraXml(dtDatos)
            strXmlOfisis = GeneraXml(dtblDatosOfisis)

            Dim objParametros() As Object = {"p_var_TipoOperacion", strOperacion, _
               "p_var_CodigoEntrega", strEntrega, _
               "p_var_FechaEntrega", strFecha, _
               "p_var_CodigoOrden", strCodigoOrden, _
               "p_var_CodigoDestino", strCodigoDestino, _
               "p_var_CodigoEstado", strCodigoEstado, _
               "p_var_CodigoUsuario", strUsuario, _
               "p_var_DatosXML", strXml, _
               "xml_ofisis", strXmlOfisis}

            Try
                Dim dtbResultado As DataTable = m_sqlDtAccOfiPlan.ObtenerDataTable("usp_prc_TransferenciaTelaAlmacen", objParametros)
                If Not dtbResultado Is Nothing Then
                    Dim strNroError As String = ""
                    Dim strDesError As String = ""
                    If Not IsDBNull(dtbResultado.Rows(0).Item("IN_ERROR")) Then strNroError = CType(dtbResultado.Rows(0).Item("IN_ERROR"), String)
                    If Not IsDBNull(dtbResultado.Rows(0).Item("VC_ERROR")) Then strDesError = CType(dtbResultado.Rows(0).Item("VC_ERROR"), String)

                    Return New String() {strNroError, strDesError}
                Else
                    Return New String() {"-1", "Error de ejecución del proceso."}
                End If
            Catch ex As Exception
                Return New String() {"-1", "Error de ejecución del proceso.<br>" & ex.ToString}
            End Try
        End Function
        Public Function GenerarInventario(ByVal Nu_Anno As Integer, ByVal Nu_Mese As Integer)
            Dim objParametros() As Object = {"NU_ANNO", Nu_Anno, "NU_MESE", Nu_Mese}
            Try
                m_sqlDtAccOfiPlan.EjecutarComando("USP_STOCK_MENSUAL_GENERAR", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ExisteCierreMensual(ByVal Nu_Anno As Integer, ByVal Nu_Mese As Integer) As DataTable
            Dim objParametros() As Object = {"NU_ANNO", Nu_Anno, "NU_MESE", Nu_Mese}
            Dim EntregaDato As DataTable
            Try
                EntregaDato = m_sqlDtAccOfiPlan.ObtenerDataTable("NM_EXISTE_CIERRE_MES", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return EntregaDato
        End Function
        Public Sub GeneraCierre(ByVal Nu_Anno As Integer, ByVal Nu_Mese As Integer, usuario As String)
            Try
                Dim objParametros As Object() = {"NU_ANNO", Nu_Anno, "NU_MESE", Nu_Mese, "USUARIO", usuario}
                m_sqlDtAccOfiPlan.EjecutarComando("SP_NM_GENERA_CIERRE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function ObtenerInventario(ByVal IntAnno As Integer, ByVal IntMese As Integer) As DataTable
            Try
                Dim objParametros() As Object = {"NU_ANNO", IntAnno, "NU_MESE", IntMese}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_STOCK_MENSUAL_OBTENER", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Registrar_Entrega_Almacen(ByVal strOperacion As String, ByVal strEntrega As String, ByVal strFecha As String, ByVal strCodigoOrden As String, ByVal strCodigoDestino As String, ByVal strCodigoEstado As String, ByVal strUsuario As String, ByVal dtDatos As DataTable, ByVal dtblDatosOfisis As DataTable) As String()
            Dim strXml, strXmlOfisis As String
            strXml = GeneraXml(dtDatos)
            strXmlOfisis = GeneraXml(dtblDatosOfisis)
            Dim objParametros() As Object = {"p_var_TipoOperacion", strOperacion, _
               "p_var_CodigoEntrega", strEntrega, _
               "p_var_FechaEntrega", strFecha, _
               "p_var_CodigoOrden", strCodigoOrden, _
               "p_var_CodigoDestino", strCodigoDestino, _
               "p_var_CodigoEstado", strCodigoEstado, _
               "p_var_CodigoUsuario", strUsuario, _
               "p_var_DatosXML", strXml, _
               "xml_ofisis", strXmlOfisis}
            Try
                Dim dtbResultado As DataTable = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_TRANSFERENCIA_TELA_ALMACEN", objParametros)
                If Not dtbResultado Is Nothing Then
                    Dim strNroError As String = ""
                    Dim strDesError As String = ""
                    If Not IsDBNull(dtbResultado.Rows(0).Item("IN_ERROR")) Then strNroError = CType(dtbResultado.Rows(0).Item("IN_ERROR"), String)
                    If Not IsDBNull(dtbResultado.Rows(0).Item("VC_ERROR")) Then strDesError = CType(dtbResultado.Rows(0).Item("VC_ERROR"), String)

                    Return New String() {strNroError, strDesError}
                Else
                    Return New String() {"-1", "Error de ejecución del proceso."}
                End If
            Catch ex As Exception
                Return New String() {"-1", "Error de ejecución del proceso.<br>" & ex.ToString}
            End Try
        End Function
        Public Function Registrar_DetalleEntrega(ByVal strNumeroDocumento As String, ByVal strCodigoOrden As String, ByVal strFecha As String, ByVal strUsuario As String, ByVal strTipoDocumento As String, _
                                                ByVal strCodigoAlmacen As String, ByVal strCodigoUnidad As String, ByVal strCodigoEmpresa As String, _
                                                ByVal strCodigoOperacion As String, ByVal dtDatos As DataTable, _
                                                ByVal strTipoAuxiliar As String, ByVal strCodigoAuxiliar As String, _
                                                ByVal strNombreAuxiliar As String) As String()
            Dim strXml, strXmlOfisis As String
            strXml = GeneraXml(dtDatos)
            'strXmlOfisis = GeneraXml(dtblDatosOfisis)

            Dim objParametros() As Object = {"p_var_NumDocumento", strNumeroDocumento, "p_var_CodigoOrden", strCodigoOrden, "p_dtm_FechaDocumento", strFecha, "p_var_CodigoUsuario", strUsuario, _
                "p_chr_TipoDocumento", strTipoDocumento, _
                "p_chr_CodigoAlmacen", strCodigoAlmacen, _
                "p_chr_CodigoUnidad", strCodigoUnidad, _
                "p_chr_CodigoEmpresa", strCodigoEmpresa, _
                "p_chr_TipoOperacion", strCodigoOperacion, _
                "p_txt_DatosXML", strXml, _
                "p_var_TipoAuxiliar", strTipoAuxiliar, _
                "p_var_CodigoAuxiliar", strCodigoAuxiliar, _
                "p_var_NombreAuxiliar", strNombreAuxiliar}

            Try
                Dim dtbResultado As DataTable = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_REGISTRA_DETALLE_ALMACEN", objParametros)
                If Not dtbResultado Is Nothing Then
                    Dim strNroError As String = ""
                    Dim strDesError As String = ""
                    If Not IsDBNull(dtbResultado.Rows(0).Item("IN_ERROR")) Then strNroError = CType(dtbResultado.Rows(0).Item("IN_ERROR"), String)
                    If Not IsDBNull(dtbResultado.Rows(0).Item("VC_ERROR")) Then strDesError = CType(dtbResultado.Rows(0).Item("VC_ERROR"), String)

                    Return New String() {strNroError, strDesError}
                Else
                    Return New String() {"-1", "Error de ejecución del proceso."}
                End If
            Catch ex As Exception
                Return New String() {"-1", "Error de ejecución del proceso.<br>" & ex.ToString}
            End Try
        End Function
        Public Function Elimnar_Rollo_Transferencia(ByVal strCodigoRollo As String)
            Dim objParametros() As Object = {"codigo_rollo", strCodigoRollo}
            Try
                m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_ELIMINA_ROLLO_TRANSFERENCIA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function registrarV2_2(ByVal strOperacion As String, ByVal strEntrega As String, ByVal datFecha As DateTime, ByVal strCodigoOrden As String, ByVal strCodigoDestino As String, ByVal strCodigoEstado As String, ByVal strUsuario As String, ByVal dtDatos As DataTable, ByVal dtblDatosOfisis As DataTable, ByVal metros1 As String, ByVal metros2 As String, ByVal metros3 As String) As String()
            Dim strXml, strXmlOfisis As String
            strXml = GeneraXml(dtDatos)
            strXmlOfisis = GeneraXml(dtblDatosOfisis)

            Dim objParametros() As Object = {"K_VC_OPERACION", strOperacion, _
               "K_VC_CODIGO_ENTREGA", strEntrega, _
               "K_DT_FECHA_ENTREGA", datFecha, _
               "K_VC_CODIGO_ORDEN", strCodigoOrden, _
               "K_VC_CODIGO_DESTINO", strCodigoDestino, _
               "K_VC_CODIGO_ESTADO", strCodigoEstado, _
               "K_VC_USUARIO_CREACION", strUsuario, _
               "xml", strXml, _
               "xml_ofisis", strXmlOfisis, _
               "metraje1", metros1, _
               "metraje2", metros2, _
               "metraje3", metros3}

            Try
                Dim dtbResultado As DataTable = m_sqlDtAccOfiPlan.ObtenerDataTable("UP_GrabarTelaAcabadaV2_2", objParametros)
                If Not dtbResultado Is Nothing Then
                    Dim strNroError As String = ""
                    Dim strDesError As String = ""
                    If Not IsDBNull(dtbResultado.Rows(0).Item("IN_ERROR")) Then strNroError = CType(dtbResultado.Rows(0).Item("IN_ERROR"), String)
                    If Not IsDBNull(dtbResultado.Rows(0).Item("VC_ERROR")) Then strDesError = CType(dtbResultado.Rows(0).Item("VC_ERROR"), String)

                    Return New String() {strNroError, strDesError}
                Else
                    Return New String() {"-1", "Error de ejecución del proceso."}
                End If
            Catch ex As Exception
                Return New String() {"-1", "Error de ejecución del proceso.<br>" & ex.ToString}
            End Try
        End Function

        Public Function Agregar(ByVal pEntrega As String, ByVal pFecha As DateTime, ByVal pUsuario As String, ByVal dtDatos As DataTable, ByVal dtblDatosOfisis As DataTable) As Boolean
            Dim pXml As String
            pXml = GeneraXml(dtDatos)

            Dim objParametros() As Object = {"xml", pXml, "codigo_entrega", pEntrega, "fecha_entrega", pFecha, "usuario_creacion", pUsuario, "xml_ofisis", GeneraXml(dtblDatosOfisis)}

            Try
                m_sqlDtAccOfiPlan.EjecutarComando("UP_GrabarTelaAcabada", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosRollosOFISIS(ByVal strCodigoRollo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("UP_ObtenerDatosRolloOfisis", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosRollosOFISIS_2(ByVal strCodigoRollo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("UP_ObtenerDatosRolloOfisis_2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
        End Sub

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
    End Class
End Namespace