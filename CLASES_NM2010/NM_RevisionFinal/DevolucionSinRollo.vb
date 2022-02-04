Option Strict On

Imports NM.AccesoDatos
Imports NuevoMundo.Tintoreria.NM.Tintoreria

Public Class DevolucionSinRollo
    Implements IDisposable

    Private m_sqlDtRevFin As AccesoDatosSQLServer

    Sub New()
        m_sqlDtRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
    End Sub

    Public Function ObtenerCodigoDevolucion() As Integer
        Try
            Return CType(m_sqlDtRevFin.ObtenerValor("UP_ObtCodDevolAlmSinRollo"), Integer)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerMotivosDevolucion() As DataTable
        Try
            Return m_sqlDtRevFin.ObtenerDataTable("UP_ObtenerMotivosDevolucion")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GenerarOrdenProduccion(ByVal strCodigoArticuloLargo As String, ByVal strCodigoArticulo As String, _
                                        ByVal dblMetros As Double) As DataTable
        Try
            Dim objParametros As Object() = {"codigo_articulo_largo", strCodigoArticuloLargo, _
                                            "codigo_articulo", strCodigoArticulo, _
                                            "metros", dblMetros}
            Return m_sqlDtRevFin.ObtenerDataTable("UP_GenerarOrdenProduccion", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Grabar(ByVal intCodigoDevolucion As Integer, ByVal dteFechaDevolucion As Date, _
                    ByVal strCodigoArticulo As String, ByVal strCodigoArticuloLargo As String, _
                    ByVal dblMetros As Double, ByVal strCodigoOrden As String, ByVal strCodigoFicha As String, _
                    ByVal strCodigoPieza As String, ByVal strCodigoMotivo As String, ByVal strObservaciones As String)
        Try
            Dim objParametros As Object() = {"codigo_devolucion", intCodigoDevolucion, _
                                            "fecha_devolucion", dteFechaDevolucion, _
                                            "codigo_articulo", strCodigoArticulo, _
                                            "codigo_articulo_largo", strCodigoArticuloLargo, _
                                            "metros", dblMetros, _
                                            "codigo_orden", strCodigoOrden, _
                                            "codigo_ficha", strCodigoFicha, _
                                            "codigo_pieza", strCodigoPieza, _
                                            "codigo_motivo", strCodigoMotivo, _
                                            "observaciones", strObservaciones}

            m_sqlDtRevFin.EjecutarComando("UP_GrabarDevolucionSinRollo", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Function ObtenerDevolucion(ByVal intCodigoDevolucion As Integer) As DataSet
    '    Try
    '        Dim objParametros As Object() = {"codigo_devolucion", intCodigoDevolucion}

    '        Return m_sqlDtRevFin.ObtenerDataSet("ObtenerDevolucionSinRollo", objParametros)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlDtRevFin.Dispose()
    End Sub
End Class