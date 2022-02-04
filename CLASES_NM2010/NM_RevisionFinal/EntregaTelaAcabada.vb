Option Strict On

Imports System.Data
Imports NM.RevisionFinal
Imports NM.AccesoDatos
Imports NuevoMundo.Tintoreria.NM.Tintoreria

Public Class EntregaTelaAcabada
    Implements IDisposable

    Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer

    Public Sub New()
        m_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
    End Sub

    Public Function ObtenerNuevaFichaPieza(ByVal strCodigoOrden As String) As DataTable
        Try
            Dim objParametros As Object() = {"codigo_orden", strCodigoOrden}

            Return m_sqlDtAccRevisionFinal.ObtenerDataTable("UP_ObtenerNuevaPiezaFichaEntregaTela", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '11-12-2004
    Public Function obtenerParametrosInicio() As DataSet
        Try
            Return m_sqlDtAccRevisionFinal.ObtenerDataSet("RVF_SP_ENTREGA_TELA_PARAMETROS")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '11-12-2004
    Public Function obtenerDatosMetrajeOrden(ByVal strCodigoOrden As String) As DataTable
        Try
            Dim objParametros As Object() = {"K_VC_CODIGO_ORDEN", strCodigoOrden}
            Return m_sqlDtAccRevisionFinal.ObtenerDataTable("RVF_SP_ENTREGA_TELA_METRAJE_ORDEN", objParametros)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function obtenerRollo(ByVal strRollo As String) As DataRow
        Return (New Rollo).ObtenerRollosPorCodigo(strRollo).Rows(0) '("metraje_total")
    End Function
    Public Sub Agregar(ByVal strRollo As String, ByVal strUsuario As String)

    End Sub
    Public Sub Grabar(ByVal strCodigoFicha As String, ByVal strCodigoPieza As String, _
                    ByVal dteFechaEntrega As Date, ByVal strOrdenProduccion As String, _
                    ByVal strCodigoArticulo As String, ByVal strCodigoArticuloLargo As String, _
                    ByVal strMetrosTotales As Double, ByVal dtblDetalle As DataTable)
        Try
            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util

            Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha, _
                                            "codigo_pieza", strCodigoPieza, _
                                            "fecha_entrega", dteFechaEntrega, _
                                            "orden_produccion", strOrdenProduccion, _
                                            "codigo_articulo", strCodigoArticulo, _
                                            "codigo_articulo_largo", strCodigoArticuloLargo, _
                                            "metros_totales", strMetrosTotales, _
                                            "xml_detalle", objXml.GeneraXml(dtblDetalle)}

            m_sqlDtAccRevisionFinal.EjecutarComando("UP_InsertarEntregaTelaAcabada", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlDtAccRevisionFinal.Dispose()
    End Sub
End Class