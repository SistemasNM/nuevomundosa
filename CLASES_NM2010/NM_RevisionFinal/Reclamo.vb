Option Strict On

Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.io

Namespace NM.RevisionFinal
    Public Class Reclamo
        Implements IDisposable

        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function ListarCabecera(ByVal pReclamo As String) As DataTable
            Dim dtblDatosBusqueda As DataTable
            Try
                Dim objParametros() As Object = {"codigo_reclamo", pReclamo}
                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("SP_ListarReclamos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatosBusqueda
        End Function


        Public Function Listar(ByVal pReclamo As String) As DataTable
            Dim dtblDatosBusqueda As DataTable
            Try
                Dim objParametros() As Object = {"codigo_reclamo", pReclamo}
                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("SP_ListarDetalleReclamos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatosBusqueda
        End Function

        Public Function ObtenerNumeroReclamo() As String
            Dim pResultado As String
            Try
                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("ObtenerNumeroReclamo").ToString
            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function

        Public Function ObtenerArticulo(ByVal pArticulo As String) As String
            Dim pResultado As String
            Try
                Dim objParametros() As Object = {"codigo_articulo", pArticulo}
                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("SP_GET_ARTICULO_20", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function


        Public Sub Agregar(ByVal pReclamo As String, ByVal pFecha As DateTime, _
                           ByVal pCliente As String, ByVal pVendedor As String, _
                           ByVal pObservaciones As String, ByVal pConclusiones As String, _
                           ByVal pUsuario As String, ByVal dtDatos As DataTable, _
                           ByRef strReclamo As String)
            Dim pXml As String
            pXml = GeneraXml(dtDatos)
            Dim objParametros() As Object = {"codigo_reclamo", pReclamo, _
                                             "fecha_reclamo", pFecha, _
                                             "codigo_cliente", pCliente, _
                                             "codigo_vendedor", pVendedor, _
                                             "observaciones", pObservaciones, _
                                             "conclusiones", pConclusiones, _
                                             "usuario_creacion", pUsuario, _
                                             "xml", pXml}
            Try
                strReclamo = CType(m_sqlDtAccOfiPlan.ObtenerValor("SP_RegistrarReclamos", objParametros), String)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function ObtenerVendedores() As DataTable
            Try
                'Dim objParametros As Object() = {"codigo_reclamo", strCodigoReclamo}

                Return m_sqlDtAccOfiPlan.ObtenerDataTable("SP_ListarVendedores")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtenerMotivos() As DataTable
            Try
                'Dim objParametros As Object() = {"codigo_reclamo", strCodigoReclamo}

                Return m_sqlDtAccOfiPlan.ObtenerDataTable("SP_NM_ListarMotivos")
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ObtenerClientesReclamo() As DataTable
            Try
                'Dim objParametros As Object() = {"codigo_reclamo", strCodigoReclamo}

                Return m_sqlDtAccOfiPlan.ObtenerDataTable("SP_REVFIN_OBTENER_CLIENTES_RECLAMO")
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function ObtenerArticulosDeClientes(ByVal strCodigoCliente As String) As DataTable
            Try
                Dim objParametros As Object() = {"VC_CLIENTE", strCodigoCliente}

                Return m_sqlDtAccOfiPlan.ObtenerDataTable("SP_ListaArticulosDeCliente", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function AgregarParoMaquina(ByVal pFechaEntrega As String, ByVal dtDatos As DataTable) As Boolean
            Dim pXml As String
            pXml = GeneraXml(dtDatos)
            Dim objParametros() As Object = {"xml", pXml, "fecha_entrega", Convert.ToDateTime(pFechaEntrega)}
            Try
                m_sqlDtAccOfiPlan.EjecutarComando("UP_GrabarParosProduccion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'Agregado Juan Cucho inicio
        Public Function ObtenerListadoDefectoxInformeCalidadRegistrado(Optional ByVal intReclamo As Int32 = 0, Optional ByVal intSecuencia As Int32 = 0) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo, "pint_Secuencia", intSecuencia}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DEFECTOS_X_INFORME_CALIDAD_RECLAMO_REGISTRADO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerCorrelativoDocumentosReclamo(ByVal intFila As Int32) As String
            Try
                Dim objParametros As Object() = {"pint_Fila", intFila}
                Return m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_OBTENER_CORRELATIVO_DOCUMENTOS_RECLAMO", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarCorrelativoGuiaTransitoReclamo(ByVal intFila As Int32) As Boolean
            Try
                Dim objParametros As Object() = {"pint_Fila", intFila}
                m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_ACTUALIZAR_CORRELATIVO_GUIA_TRANSITO_RECLAMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'Agregado Juan Cucho fin
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

        'Public Function ObtenerArticulosDeClientes(ByVal strCodigoArticulo As String, ByVal strDescripcionArticulo As String) As DataTable
        Public Function ObtenerArticulos(ByVal strCodigoCliente As String, ByVal strCodigoArticulo As String, ByVal strDescripcionArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvar_CodigoArticulo", strCodigoArticulo,
                                                "pvar_DescripcionArticulo", strDescripcionArticulo}

                'Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_BUSCAR_ARTICULOS_CLIENTE", objParametros)
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DATOS_ARTICULO_POR_CODIGO_POR_DESC", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        '*****************************************************************************************************
        'Objetivo   : generar numero correlativo reclamo
        'Autor      : Juan Cucho Antunez
        'Creado     : 24/01/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function GenerarNumeroCorrelativoReclamo() As String
            Try
                Dim objParametros As Object() = {}
                Return CStr(m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_GENERAR_CORRELATIVO_RECLAMO", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '*****************************************************************************************************
        'Objetivo   : registra reclamo almacen 2das
        'Autor      : Juan Cucho Antunez
        'Creado     : 25/01/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function registrarReclamoAlmacen2das(ByVal int_NumeroReclamo As String, _
                                                 ByVal strNumeroTipoDocumento As String) As Integer
            Dim larrParams() As Object = {"pint_reclamo", int_NumeroReclamo, _
                                            "pvar_NumeroTipoDocumento", strNumeroTipoDocumento
                                          }

            Try
                Return m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRA_RECLAMO_ALMACEN_2DAS", larrParams)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace