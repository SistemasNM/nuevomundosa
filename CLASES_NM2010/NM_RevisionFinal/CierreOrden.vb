Option Strict On
Imports NM.AccesoDatos
Namespace NM.RevisionFinal
    Public Class CierreOrden
        Implements IDisposable
        Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer
        Sub New()
            m_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
        'area de funciones
        Public Function actualizarEstadoOrden(ByVal strOrden As String, ByVal strEstado As String, ByVal strUsuario As String) As String()

            Try
                Dim objParametros() As Object = {"K_VC_CODIGO_ORDEN", strOrden,
                                                 "K_VC_ESTADO_ORDEN", strEstado,
                                                 "K_VC_USUARIO", strUsuario}

                Dim dtbResultado As DataTable = m_sqlDtAccRevisionFinal.ObtenerDataTable("RVF_SP_ESTADO_ORDEN_PRODUCCION_UPD", objParametros)

                If dtbResultado.Rows.Count > 0 Then
                    If dtbResultado.Rows.Count > 1 Then
                        Dim strMensaje As New System.Text.StringBuilder
                        For Each row As DataRow In dtbResultado.Rows
                            strMensaje.Append(row.Item("VC_ERROR").ToString & "<br />")
                        Next

                        Return New String() {"1", strMensaje.ToString()}                    
                    Else
                        Return New String() {CStr(dtbResultado.Rows(0).Item("IN_ERROR")), CStr(dtbResultado.Rows(0).Item("VC_ERROR"))}
                    End If
                Else
                    Return New String() {"-1", "Error al actualizar los datos."}
                End If

            Catch ex As Exception
                Return New String() {"-1", "Error al actualizar los datos: " & ex.ToString}
            End Try
        End Function
        Public Function obtenerOrden(ByVal strOrden As String) As DataTable
            Try
                Dim objParametros() As Object = {"K_VC_CODIGO_ORDEN", strOrden}
                Return m_sqlDtAccRevisionFinal.ObtenerDataTable("RVF_SP_DESPACHO_ORDEN", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function obtenerDespachoOrden(ByVal strOrden As String) As DataSet
            Try
                Dim objParametros() As Object = {"K_VC_CODIGO_ORDEN", strOrden}
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("RVF_SP_DESPACHO_ORDEN", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function consultarDespachoOrden(ByVal strOrden As String) As DataSet
            Try
                Dim objParametros() As Object = {"K_VC_CODIGO_ORDEN", strOrden}
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("SP_NM_DESPACHO_ORDEN_CONSULTA_V3", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function consultarDespachoOrden2(ByVal strOrden As String) As DataSet
            Try
                Dim objParametros() As Object = {"K_VC_CODIGO_ORDEN", strOrden}
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("SP_NM_DESPACHO_ORDEN_CONSULTA_V4_2", objParametros)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function obtenerParametrosInicio() As DataSet
            Try
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("RVF_SP_CIERRE_ORDEN_PARAM_INI")
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function obtener_OrdenPendiente(ByVal orden As String) As DataTable
            Dim objParametros() As Object = {"OrdenProduccion", orden}

            Return m_sqlDtAccRevisionFinal.ObtenerDataTable("RVF_SP_OBTENER_ORDENPENDIENTE", objParametros)
        End Function

        Public Function obtener_Total_OrdenPendiente(ByVal orden As String) As DataTable
            Dim objParametros() As Object = {"CodigoOrden", orden}

            Return m_sqlDtAccRevisionFinal.ObtenerDataTable("RVF_SP_OBTENER_TOTAL_ORDENPENDIENTE", objParametros)

        End Function
        Public Sub actualiza_Estado_EntregaTelas(ByVal orden As String)
            Dim objParametros() As Object = {"CodigoOrden", orden}
            m_sqlDtAccRevisionFinal.EjecutarComando("RVF_SP_ACTUALIZA_ESTADO_ENTREGATELAS", objParametros)
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevisionFinal.Dispose()
        End Sub
        Public Function ObtenerDatosImpresion(ByVal strOrden As String) As DataSet
            Try
                Dim objParametros() As Object = {"p_var_CodigoOrden", strOrden}
                Return m_sqlDtAccRevisionFinal.ObtenerDataSet("usp_qry_ImpresionCierreOrdenes", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosOrden(ByVal strOrden As String) As DataSet
                Try
                    Dim objParametros() As Object = {"p_var_CodigoOrden", strOrden}
                    Return m_sqlDtAccRevisionFinal.ObtenerDataSet("usp_qry_ImpresionCierreOrdenes", objParametros)
                Catch ex As Exception
                    Throw ex
                End Try
    End Function


    Public Function FichasPrensaIni_Listar(ByVal strOrden As String) As DataSet
      Try
        Dim objParametros() As Object = {"OrdenProd", strOrden}
        Dim dts As New DataSet
        dts = m_sqlDtAccRevisionFinal.ObtenerDataSet("usp_RevFin_ListaFichaPrensa", objParametros)
        FichasPrensaIni_Listar = dts
      Catch ex As Exception
        Throw ex
      End Try


    End Function


    Public Function FichasPrensaIni_Guardar(ByVal sOrdenProd As String, ByVal sCodFicha As String, ByVal sCodArtic As String, ByVal dMtsTotal As Double) As Boolean
      Try
        Dim objParametros() As Object = {"OrdenProd", sOrdenProd, _
                                         "CodFicha", sCodFicha, _
                                         "CodArtic", sCodArtic, _
                                         "MtsTotal", dMtsTotal}
        Dim dts As New DataSet
        dts = m_sqlDtAccRevisionFinal.ObtenerDataSet("usp_RevFin_ActualizarFichaPrensa", objParametros)
        FichasPrensaIni_Guardar = True
      Catch ex As Exception
        Throw ex
        FichasPrensaIni_Guardar = False
      End Try

    End Function


  End Class
End Namespace
