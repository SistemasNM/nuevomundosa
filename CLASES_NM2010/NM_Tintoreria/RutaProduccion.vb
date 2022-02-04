Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria

    Public Class RutaProduccion
        Implements IDisposable

#Region " Declaración de Variables Miembro "

        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)

        End Sub

#End Region

#Region " Definición de Métodos "
        Public Function ObtenerSecuencias(ByVal codigoArticulo As String) As DataTable

            Dim dtSecuencias As DataTable

            Try
                Dim parametros As Object() = {"codigoArticulo", codigoArticulo}

                dtSecuencias = m_sqlDtAccTintoreria.ObtenerDataTable("UP_ObtenerSecuenciaPorArticulo", parametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtSecuencias

        End Function
        Public Function ObtenerOperacionPorSecuencia(ByVal codigoArticulo As String, ByVal codigoSecuencia As String) As String

            Dim dtSecuencias As DataTable

            Try
                Dim parametros As Object() = {"codigoArticulo", codigoArticulo, "codigoSecuencia", codigoSecuencia}

                dtSecuencias = m_sqlDtAccTintoreria.ObtenerDataTable("UP_ObtenerOperacionPorSecuencia", parametros)
            Catch ex As Exception
                Throw ex
            End Try

            If dtSecuencias.Rows.Count > 0 Then
                Return CStr(dtSecuencias.Rows(0).Item("descripcion_operacion"))
            End If



        End Function
        Public Function ObtenerRutaProduccion(ByVal codigo_articulo As String) As DataTable
            Try
                Dim parametros As Object() = {"codigo_Articulo", codigo_articulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_OBTENER_RUTAPRODUCCION_ARTICULO", parametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function listarRutaProduccion(ByVal codigo_articulo As String) As DataTable
            Try
                Dim parametros As Object() = {"codigo_Articulo", codigo_articulo}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_RutaProduccion_SelectArticulo", parametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Validar(ByVal pstrCodigoArticuloLargo As String, _
                            ByVal pstrCodigoArticuloCorto As String, _
                            Optional ByRef lstrErrores As String = "") As Boolean
            Dim lbooOk As Boolean = False
            Dim ldtErrores As DataTable
      Dim lobjCostos As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoArticuloLargo", pstrCodigoArticuloLargo, _
                                    "var_CodigoArticulo", pstrCodigoArticuloCorto}
            Try
        lobjCostos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtErrores = lobjCostos.ObtenerDataTable("usp_qry_VerificaRutaArticulo", lstrParametros)
                If ldtErrores.Rows.Count = 0 Then
                    lbooOk = True
                Else
                    lstrErrores = Replace(CStr(ldtErrores.Rows(0).Item(0)), ". El administrador del sistema se contactará con Ud. en cuanto los datos hayan sido completados", "")
                    If lstrErrores = "COMPLETO" Then
                        lbooOk = True
                        lstrErrores = ""
                    Else
                        lbooOk = False
                    End If
                End If
            Catch ex As Exception
                lbooOk = False
                ldtErrores = Nothing
            Finally
                lobjCostos = Nothing
            End Try
            Return lbooOk
        End Function
        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub
#Region "       METODOS PARA EL MÓDULO DE CAMBIO DE SUBPROCESOS EN LA RUTA"
        Public Function CargarEtapas() As DataTable
            Try
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_query_ListaEtapas")
            Catch ex As Exception

            End Try
        End Function
        Public Function ListarDatosArticulo(ByVal var_CodigoArticulo As String, ByVal var_CodigoEtapa As String, ByVal var_CodigoSubproceso As String) As DataTable
            Try
                Dim parametros As Object() = {"p_var_CodigoArticulo", var_CodigoArticulo, _
                                              "p_var_CodigoEtapa", var_CodigoEtapa, _
                                              "p_var_CodigoSubproceso", var_CodigoSubproceso}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_query_ListarDatosArticulosPorSubproceso", parametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function FiltraSubprocesosPorEtapas(ByVal var_CodigoEtapa As String) As DataTable
            Try
                Dim objParametros As Object() = {"p_var_CodigoEtapa", var_CodigoEtapa}
                Return m_sqlDtAccTintoreria.ObtenerDataTable("usp_query_FiltraSubprocesosPorEtapas", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ActualizarSubproceso(ByVal var_XMLArticulos As String, ByVal var_CodigoSubproceso As String, ByVal var_Usuario As String) As Integer
            Try
                Dim objParametros As Object() = {"p_text_XMLArticulos", var_XMLArticulos, _
                                                 "p_var_CodigoSubproceso", var_CodigoSubproceso, _
                                                 "p_var_Usuario", var_Usuario}
                Return CType(m_sqlDtAccTintoreria.ObtenerValor("usp_upd_ActualizarSubproceso", objParametros), Integer)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#End Region

    End Class
End Namespace