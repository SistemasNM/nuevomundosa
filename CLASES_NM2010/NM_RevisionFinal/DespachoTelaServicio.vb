Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Public Class DespachoTelaServicio
    Implements IDisposable

    Private m_sqlDtRevFin As AccesoDatosSQLServer

    Public Sub New()
        m_sqlDtRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
    End Sub

    Public Function ObtenerFichaPiezaArticulo(ByVal strCodigoOrden As String) As DataTable
        Try
            Dim objParametros As Object() = {"codigo_orden", strCodigoOrden}

            Return m_sqlDtRevFin.ObtenerDataTable("UP_ObtenerNuevaPiezaDespachoTelaServicio", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Grabar(ByVal dteFechaDespacho As Date, ByVal strCodigoArticulo As String, _
                    ByVal strCodigoArticuloLargo As String, ByVal dblMetros As Double, _
                    ByVal strCodigoOrden As String, ByVal strCodigoFicha As String, _
                    ByVal strCodigoPieza As String)
        Try
            Dim objParametros As Object() = {"fecha_despacho", dteFechaDespacho, _
                                            "codigo_articulo", strCodigoArticulo, _
                                            "codigo_articulo_largo", strCodigoArticuloLargo, _
                                            "metros", dblMetros, _
                                            "codigo_orden", strCodigoOrden, _
                                            "codigo_ficha", strCodigoFicha, _
                                            "codigo_pieza", strCodigoPieza}
            m_sqlDtRevFin.EjecutarComando("UP_InsertarDespachoTelaServicio", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlDtRevFin.Dispose()
    End Sub
End Class