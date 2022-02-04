Option Strict On
Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.IO
Namespace NM.RevisionFinal
    Public Class ReclamoInformeCalidad
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
        Public Function ObtenerFormaDevolucionReclamo() As DataTable
            Dim dtblFormaDevolucionReclamo As DataTable
            Try
                dtblFormaDevolucionReclamo = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_FORMA_DEVOLUCION_RECLAMO")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblFormaDevolucionReclamo
        End Function
        Public Function ObtenerCondicionInformeCalidad() As DataTable
            Dim dtblFormaDevolucionReclamo As DataTable
            Try
                dtblFormaDevolucionReclamo = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_CONDICION_INFORME_CALIDAD")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblFormaDevolucionReclamo
        End Function
        Public Function ObtenerCausanteReclamo() As DataTable
            Dim dtblFormaDevolucionReclamo As DataTable
            Try
                dtblFormaDevolucionReclamo = m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_CAUSANTES_RECLAMO")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblFormaDevolucionReclamo
        End Function
        Public Function ObtenerCorrelativoReclamoInformeCalidad() As String
            Try
                Return CStr(m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_OBTENER_CORRELATIVO_RECLAMO_INFORME_CALIDAD"))
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ObtenerInformeCalidadxDetalleReclamo(ByVal intReclamo As Int32, ByVal intSecuencia As Int32) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo, "pint_Secuencia", intSecuencia}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_INFORME_CALIDAD_X_DETALLE_RECLAMO", objParametros)
                'Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_INFORME_CALIDAD_X_DETALLE_RECLAMO_TEST", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function RegistrarInformeCalidadReclamo(ByVal intReclamo As Int32,
                                                        ByVal intSecuencia As Int32,
                                                        ByVal intFormaDevolucionID As Int32,
                                                        ByVal intNumeroPrendas As Int32,
                                                        ByVal numMetrosJustificados As Decimal,
                                                        ByVal varObservacion As String,
                                                        ByVal varCondicion As String,
                                                        ByVal varConclusiones As String,
                                                        ByVal dtmFechaInformeCalidad As DateTime,
                                                        ByVal varUsuarioCreacion As String,
                                                        ByVal intEstadoInformeCalidadID As Int32) As String
            'Dim var_ListaDefectosXml As String
            'dtblDefectos.TableName = "Defectos"
            'var_ListaDefectosXml = GeneraXml(dtblDefectos)
            Dim pResultado As String
            Try
                Dim objParametros As Object() = {
                                                 "pint_Reclamo", intReclamo,
                                                 "pint_Secuencia", intSecuencia,
                                                 "pint_FormaDevolucionID", intFormaDevolucionID,
                                                 "pint_NumeroPrendas", intNumeroPrendas,
                                                 "pnum_MetrosJustificados", numMetrosJustificados,
                                                 "pvar_Observacion", varObservacion,
                                                 "pvar_Condicion", varCondicion,
                                                 "pvar_Conclusiones", varConclusiones,
                                                 "pdtm_FechaInformeCalidad", dtmFechaInformeCalidad,
                                                 "pvar_UsuarioCreacion", varUsuarioCreacion,
                                                 "pint_EstadoInformeCalidadID", intEstadoInformeCalidadID
                                                }
                'pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_REGISTRAR_INFORME_CALIDAD_RECLAMO_TEST", objParametros).ToString
                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_REGISTRAR_INFORME_CALIDAD_RECLAMO", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function
        Public Function ObtenerCausantesInformeCalidadReclamo(ByVal intInformeCalidadID As Int32) As DataTable
            Try
                Dim objParametros As Object() = {"pint_InformeCalidadID", intInformeCalidadID}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_CAUSANTES_INFORME_CALIDAD_RECLAMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarInformeCalidadReclamo(ByVal intNumeroInforme As Int32, ByVal intReclamo As Int32, ByVal intSecuencia As Int32,
                                                        ByVal intFormaDevolucionID As Int32, ByVal intNumeroPrendas As Int32,
                                                        ByVal numMetrosJustificados As Decimal,
                                                        ByVal varObservacion As String,
                                                        ByVal varCondicion As String,
                                                        ByVal varConclusiones As String,
                                                        ByVal dtmFechaInformeCalidad As DateTime, ByVal varUsuarioModificacion As String,
                                                        ByVal intEstadoInformeCalidadID As Int32) As String

            Dim pResultado As String
            Try
                Dim objParametros As Object() = {"pint_InformeCalidadID", intNumeroInforme,
                                                 "pint_Reclamo", intReclamo,
                                                 "pint_Secuencia", intSecuencia,
                                                 "pint_FormaDevolucionID", intFormaDevolucionID,
                                                 "pnum_MetrosJustificados", numMetrosJustificados,
                                                 "pint_NumeroPrendas", intNumeroPrendas,
                                                 "pvar_Observacion", varObservacion,
                                                 "pvar_Condicion", varCondicion,
                                                "pvar_Conclusiones", varConclusiones,
                                                "pdtm_FechaInformeCalidad", dtmFechaInformeCalidad,
                                                 "pvar_UsuarioModificacion", varUsuarioModificacion,
                                                "pint_EstadoInformeCalidadID", intEstadoInformeCalidadID}
                'pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_ACTUALIZAR_INFORME_CALIDAD_RECLAMO_TEST", objParametros).ToString
                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_ACTUALIZAR_INFORME_CALIDAD_RECLAMO", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function
        Public Function RegistrarDefectosInformeCalidad(ByVal intReclamo As Int32, ByVal intSecuencia As Int32,
                                                        ByVal intNumeroInforme As Int32,
                                                        ByVal varUsuarioCreacion As String,
                                                        ByVal pdtm_FechaRegistro As DateTime,
                                                        ByVal dtblListaDefectos As DataTable) As Boolean
            Dim var_ListaDefectosXml As String
            dtblListaDefectos.TableName = "Defectos"
            var_ListaDefectosXml = GeneraXml(dtblListaDefectos)
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo,
                                                 "pint_Secuencia", intSecuencia,
                                                 "pint_InformeCalidadID", intNumeroInforme,
                                                 "pvar_UsuarioRegistro", varUsuarioCreacion,
                                                 "pdtm_FechaRegistro", pdtm_FechaRegistro,
                                                 "pvar_ListaDefectosXml", var_ListaDefectosXml}
                'm_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRAR_DEFECTOS_INFORME_CALIDAD_TEST", objParametros).ToString()
                m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRAR_DEFECTOS_INFORME_CALIDAD", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerListadoDefectoxDetalleReclamo(ByVal intReclamo As Int32, ByVal intSecuencia As Int32) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo, "pint_Secuencia", intSecuencia}
                'Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DEFECTOS_X_DETALLE_RECLAMO_TEST", objParametros)
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_DEFECTOS_X_DETALLE_RECLAMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerDetalleReclamoReclamoValidacionExistencia(ByVal intReclamo As Int32, ByVal intSecuencia As Int32) As String
            Dim pResultado As String = "0"
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo,
                                                 "pint_Secuencia", intSecuencia}

                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_OBTENER_DETALLE_RECLAMO_VALIDACION_EXISTENCIA", objParametros).ToString()

            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function
        Public Function ObtenerValidacionInformesConcluidosReclamos(ByVal pintReclamo As Int32) As String
            Dim pResultado As String = "0"
            Try
                Dim objParametros As Object() = {"pint_reclamo", pintReclamo}

                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_OBTENER_VALIDACION_INFORMES_CONCLUIDOS_RECLAMOS", objParametros).ToString()

            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function
        Public Function ObtenerValidacionAdjuntoInformesCalidad(ByVal pintReclamo As Int32, ByVal pintSecuencia As Int32) As String
            Dim pResultado As String = "0"
            Try
                Dim objParametros As Object() = {"pint_reclamo", pintReclamo, "pint_secuencia", pintSecuencia}

                pResultado = m_sqlDtAccOfiPlan.ObtenerValor("USP_RVF_OBTENER_VALIDACION_ADJUNTO_INFORME_CALIDAD", objParametros).ToString()

            Catch ex As Exception
                Throw ex
            End Try
            Return pResultado
        End Function
        Public Function RegistrarFormaDevolucionInformeCalidad(ByVal intNumeroInforme As Int32,
                                                        ByVal varUsuarioCreacion As String,
                                                        ByVal pdtm_FechaRegistro As DateTime,
                                                        ByVal dtblListaFormaDevolucion As DataTable) As Boolean
            Dim var_FormaDevolucionXml As String
            dtblListaFormaDevolucion.TableName = "FormaDevolucion"
            var_FormaDevolucionXml = GeneraXml(dtblListaFormaDevolucion)
            Try
                Dim objParametros As Object() = {"pint_InformeCalidadID", intNumeroInforme,
                                                 "pvar_UsuarioRegistro", varUsuarioCreacion,
                                                 "pdtm_FechaRegistro", pdtm_FechaRegistro,
                                                 "pvar_FormaDevolucionXml", var_FormaDevolucionXml}
                m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRAR_DEFECTOS_INFORME_CALIDAD", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarFormaDevolucionInformeCalidad(ByVal intNumeroInforme As Int32,
                                                        ByVal strUsuarioModificacion As String,
                                                        ByVal dtmFechaInformeCalidad As DateTime,
                                                        ByVal dtmFechaModificacion As DateTime,
                                                        ByVal dtblListaFormaDevolucion As DataTable) As Boolean
            Dim var_FormaDevolucionXml As String
            dtblListaFormaDevolucion.TableName = "FormaDevolucion"
            var_FormaDevolucionXml = GeneraXml(dtblListaFormaDevolucion)
            Try
                Dim objParametros As Object() = {"pint_InformeCalidadID", intNumeroInforme,
                                                 "pvar_UsuarioModificacion", strUsuarioModificacion,
                                                 "pdtm_FechaInformeCalidad", dtmFechaInformeCalidad,
                                                 "pdtm_FechaModificacion", dtmFechaModificacion,
                                                 "pvar_FormaDevolucionXml", var_FormaDevolucionXml}
                m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_ACTUALIZAR_FORMA_DEVOLUCION_INFORME_CALIDAD", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerFormasDevolucionInformeCalidad(ByVal intInformeCalidadID As Int32) As DataTable
            Try
                Dim objParametros As Object() = {"pint_InformeCalidadID", intInformeCalidadID}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_FORMA_DEVOLUCION_INFORME_CALIDAD", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerFormasDevolucionxReclamo(ByVal intReclamo As Int32, ByVal intSecuencia As Int32) As DataTable
            Try
                Dim objParametros As Object() = {"pint_Reclamo", intReclamo,
                                                 "pint_Secuencia", intSecuencia
                                                }
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("USP_RVF_OBTENER_FORMA_DEVOLUCION_X_RECLAMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function AprobarInformeCalidadGerenciaComercialReclamo(ByVal DTInformesCalidadAprobados As DataTable) As Integer
            Dim var_InformesCalidadAprobados As String
            Dim int_FilasAfectadas As Integer = 0
            DTInformesCalidadAprobados.TableName = "InformesCalidadAprobados"
            var_InformesCalidadAprobados = GeneraXml(DTInformesCalidadAprobados)

            Try
                Dim objParametros As Object() = {"pvar_InformesCalidadAprobados", var_InformesCalidadAprobados}
                int_FilasAfectadas = m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_APROBAR_INFORME_CALIDAD_GERENCIA_COMERCIAL_RECLAMO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return int_FilasAfectadas
        End Function

        Public Function RegistrarMotivos(ByVal CodMotivo As String,
                                         ByVal CodEmpresa As String,
                                         ByVal TipoDocumento As String,
                                         ByVal NumDocumento As String,
                                         ByVal SecuenciaDocumento As Integer,
                                         ByVal Porcetaje As Decimal,
                                         ByVal CodigoRevisor As String,
                                         ByVal Fila As Integer,
                                         ByVal UsuarioRegistro As String,
                                         ByVal UsuarioModificacion As String,
                                         ByVal FechaRegistro As Date,
                                         ByVal FechaModificacion As Date,
                                         ByVal Estado As Char) As Boolean

            Try
                Dim objParametros As Object() = {"var_CodigoMotivo", CodMotivo,
                                                 "var_CodigoEmpresa", CodEmpresa,
                                                 "var_TipoDocumento", TipoDocumento,
                                                 "var_NumeroDocumento", NumDocumento,
                                                 "int_SecuenciaDocumento", SecuenciaDocumento,
                                                 "num_Porcentaje", Porcetaje,
                                                 "var_CodigoRevisor", CodigoRevisor,
                                                 "int_Fila", Fila,
                                                 "var_UsuarioRegistro", UsuarioRegistro,
                                                 "var_UsuarioModificacion", UsuarioModificacion,
                                                 "dtm_FechaRegistro", FechaRegistro,
                                                 "dtm_FechaModificacion", FechaModificacion,
                                                 "bit_Estado", Estado}
                m_sqlDtAccOfiPlan.EjecutarComando("USP_RVF_REGISTRAR_MOTIVO_INFORME_CALIDAD_RECLAMO", objParametros).ToString()

            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

End Namespace
