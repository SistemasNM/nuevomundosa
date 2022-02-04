Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

  Public Class ProgramacionOrden

#Region " Declaración de Variables Miembro "
    Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
    Sub New()
      m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    End Sub

#End Region

    Public Function ListarOrdenes(ByVal sOrden As String, ByVal sTipoColo As String, ByVal sColor As String, ByVal scliente As String) As DataTable
      Try

        Dim objParametros() As Object = {"codigo_orden", sOrden, _
                                        "tipocolorante", sTipoColo, _
                                        "color", sColor, _
                                        "cliente", scliente}


        Return m_sqlDtAccTintoreria.ObtenerDataTable("stp_OrdenesPendientes_Lista", objParametros)


      Catch ex As Exception
        Throw ex
      End Try

    End Function

    Public Function ListarOrdenesAtender(ByVal sOrden As String, ByVal sTipoColo As String, ByVal sColor As String, ByVal pcliente As String) As DataTable
      Try

        Dim objParametros() As Object = {"codigo_orden", sOrden, _
                                        "tipocolorant", sTipoColo, _
                                        "color", sColor, _
                                        "cliente", pcliente}

        Return m_sqlDtAccTintoreria.ObtenerDataTable("stp_NM_Atender_Ordenes_Listar", objParametros)


      Catch ex As Exception
        Throw ex
      End Try

    End Function


    Public Function ListarOrdSolicitado(ByVal pFecha_Desde As String, ByVal pFecha_Hasta As String, ByVal pcodigo_orden As String, ByVal pestado_solic As String) As DataTable
      Try

        Dim objParametros() As Object = {"Fecha_Desde", pFecha_Desde, _
                                        "Fecha_Hasta", pFecha_Hasta, _
                                        "codigo_orden", pcodigo_orden, _
                                        "estado_solic", pestado_solic}


        Return m_sqlDtAccTintoreria.ObtenerDataTable("stp_NM_Solicitud_Ordenes_Listar", objParametros)


      Catch ex As Exception
        Throw ex
      End Try

    End Function



    Public Function GrabarOrden(ByVal pcodigo_orden As String, _
                                ByVal pcodigo_articulo_largo As String, _
                                ByVal pcodigo_articulo_corto As String, _
                                ByVal pfecha_despacho As String, _
                                ByVal pfecha_entrega As String, _
                                ByVal pobservacion As String, _
                                ByVal pusuario_solicitado As String, _
                                ByVal pmetraje_solicitado As Decimal, _
                                ByVal porden_prioridad As Integer) As Integer
      Try

        Dim objParametros() As Object = {"codigo_orden", pcodigo_orden, _
                                        "codigo_articulo_largo", pcodigo_articulo_largo, _
                                        "codigo_articulo_corto", pcodigo_articulo_corto, _
                                        "fecha_despacho", pfecha_despacho, _
                                        "fecha_entrega", pfecha_entrega, _
                                        "observacion", pobservacion, _
                                        "usuario_solicitado", pusuario_solicitado, _
                                        "metraje_solicitado", pmetraje_solicitado, _
                                        "orden_prioridad", porden_prioridad}

        GrabarOrden = m_sqlDtAccTintoreria.EjecutarComando("stp_NM_Solicitud_Ordenes_Ins", objParametros)

      Catch ex As Exception
        Throw ex
      End Try

    End Function



    Public Function ActualizarOrden(ByVal pcodigo_solicitado As Integer, _
                                    ByVal pcodigo_orden As String, _
                                    ByVal pcodigo_articulo_largo As String, _
                                    ByVal pusuario_atendido As String) As Integer
      Try

        Dim objParametros() As Object = {"codigo_solicitado", pcodigo_solicitado, _
                                          "codigo_orden", pcodigo_orden, _
                                        "codigo_articulo_largo", pcodigo_articulo_largo, _
                                        "usuario_atendido", pusuario_atendido}

        ActualizarOrden = m_sqlDtAccTintoreria.EjecutarComando("stp_NM_Solicitud_Ordenes_Act", objParametros)

      Catch ex As Exception
        Throw ex
      End Try

    End Function



  End Class

End Namespace
