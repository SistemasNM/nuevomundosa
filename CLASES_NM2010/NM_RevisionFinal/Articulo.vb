Option Strict On

Imports System.Xml
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Articulo
        Implements IDisposable

        Private m_accDtsSQLServer As AccesoDatosSQLServer
		Private m_dtaccRevFin As AccesoDatosSQLServer

		Sub New()
			m_accDtsSQLServer = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
			m_dtaccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
		End Sub

		Public Function ObtenerArticulosCombo() As DataTable
			Dim dtblArticulosCombo As DataTable

			Try
				dtblArticulosCombo = m_accDtsSQLServer.ObtenerDataTable("UP_ObtenerArticulosCombo")
			Catch ex As Exception
				Throw ex
			End Try

			Return dtblArticulosCombo
		End Function

		Public Function Descripcion(ByVal pArticulo As String) As String
			Dim pDescripcion As String

			Dim objParametros() As Object = {"codigo_articulo", pArticulo}

			Try
				pDescripcion = m_accDtsSQLServer.ObtenerValor("UP_ObtenerArticulosPorCodigo", objParametros).ToString
			Catch ex As Exception
				Throw ex
			End Try

			Return pDescripcion
		End Function

		Public Function ObtenerArticulosPorOrden(ByVal strCodigoOrden As String) As DataTable
			Try
				Dim objParametros As Object() = {"codigo_orden", strCodigoOrden}

                Return m_dtaccRevFin.ObtenerDataTable("UP_ObtenerArticulosPorOrden", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
    End Function

    Public Function ObtenerRangoAncho(ByVal strFicha As String) As DataTable
            Try

                Dim objParametros As Object() = {"codigo_ficha", strFicha}

                Return m_dtaccRevFin.ObtenerDataTable("usp_rvf_RangoAncho", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
    End Function



        Public Function ObtenerArticulosxLotizacion(ByVal strCodigoCliente As String, ByVal strCodigoArticulo As String, ByVal strDescripcionArticulo As String) As DataTable
            Dim dtArticuloxLotizacion As DataTable
            Try
                Dim objParametros As Object() = {"codigo_articulo", strCodigoArticulo, "desc_articulo", strDescripcionArticulo}
                dtArticuloxLotizacion = m_dtaccRevFin.ObtenerDataTable("usp_NM_Mostrar_Articulos", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtArticuloxLotizacion
        End Function


#Region "OBSERVACIONES - LUIS(20210414)"
        Public Function ObtenerListadoObservacionArticulo(ByVal strCodigoArticulo7Dig As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvch_CodigoArticulo7Dig", strCodigoArticulo7Dig}

                Return m_dtaccRevFin.ObtenerDataTable("USP_RVF_OBTENER_LISTADO_OBSERVACIONES_ARTICULOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function RegistrarObservacionArticulo(ByVal strCodigoArticulo As String, ByVal strObservacion As String, ByVal strUusario As String) As Integer
            Try
                Dim objParametros As Object() = {"pvch_CodigoArticulo", strCodigoArticulo,
                                                 "pvch_Observaciones", strObservacion,
                                                 "pvch_Usuario", strUusario}

                Return m_dtaccRevFin.EjecutarComando("USP_RVF_REGISTRAR_OBSERVACIONES_ARTICULOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ActualizarObservacionArticulo(ByVal strCodigoArticulo As String, ByVal strObservacion As String, ByVal strUusario As String) As Integer
            Try
                Dim objParametros As Object() = {"pvch_CodigoArticulo", strCodigoArticulo,
                                                 "pvch_Observaciones", strObservacion,
                                                 "pvch_Usuario", strUusario}

                Return m_dtaccRevFin.EjecutarComando("USP_RVF_ACTUALIZAR_OBSERVACIONES_ARTICULOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function EliminarObservacionArticulo(ByVal strCodigoArticulo As String, ByVal strUusario As String) As Integer
            Try
                Dim objParametros As Object() = {"pvch_CodigoArticulo", strCodigoArticulo,
                                                 "pvch_Usuario", strUusario}

                Return m_dtaccRevFin.EjecutarComando("USP_RVF_ELIMINAR_OBSERVACIONES_ARTICULOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_accDtsSQLServer.Dispose()
            m_dtaccRevFin.Dispose()
        End Sub
    End Class
End Namespace