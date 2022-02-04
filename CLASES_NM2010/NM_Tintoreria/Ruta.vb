Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Ruta
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function Listar(ByVal pArticulo As String, ByVal pCodigoSubproceso As String) As DataTable
            Dim dtDatos As DataTable
			Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pCodigoSubproceso}
			Try
				dtDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_RutaProduccion_Select", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
			Return dtDatos
        End Function

        Public Function ListarRutas_CodSubprocesos_byArticulo(ByVal pArticulo As String) As DataTable
            Dim dtDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}
            Try

                dtDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_RutaProduccion_CodSubprocesos_ByArticulo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtDatos
        End Function

        Public Function ListarSubprocesos(ByVal pArticulo As String) As DataTable
            Dim dtDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}
            Try

                dtDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_RutaProduccion_Subprocesos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtDatos
        End Function

        Public Function ObtenerNumeroRevision(ByVal pArticulo As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}
            Try
                pResultado = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_RutaProduccion_Revision", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Function ActualizarNumeroRevision(ByVal pArticulo As String, ByVal pUsuario As String) As Boolean
            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "usuario_modificacion", pUsuario}
            Try
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_RutaProduccion_Update", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function

        Public Function ListarNuevosDatos(ByVal pArticulo As String, ByVal pSubproceso As String) As DataTable
            Dim dtDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso}
            Try
                dtDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Subproceso_Detalle_PorSubProceso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtDatos
        End Function

        Public Function Agregar(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pRevisionSubproceso As Integer, ByVal pRevisionRuta As Integer, ByVal pUsuario As String) As Boolean

            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso, "revision_subproceso", pRevisionSubproceso, "revision_ruta", pRevisionRuta, "usuario_creacion", pUsuario}
            Try
                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_RutaProduccion_Insert", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function

        Public Function Eliminar(ByVal pArticulo As String, ByVal pSubproceso As String) As Boolean

            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso}

            Try

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_RutaProduccion_Delete", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
    End Function

    Public Function fnc_copiar(ByVal pstr_articuloofiorigen As String, ByVal pstr_articuloofidestino As String, ByVal pstr_usuario As String) As DataTable
      Dim ldtbretornar As DataTable
      Dim lobjparametros() As Object = {"pvch_articuloorigen", pstr_articuloofiorigen, "pvch_articulodestino", pstr_articuloofidestino, "pvch_usuario", pstr_usuario}
      Try
        ldtbretornar = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_rutaproduccion_copiar", lobjparametros)
      Catch ex As Exception
        Throw ex
      End Try
      Return ldtbretornar
    End Function

    Public Function ListarArticulosOfisisMaestroRutas() As DataTable
      Dim dtDatos As DataTable
      Try
        dtDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ArticulosOfisisMaestroRutas_Select")
      Catch ex As Exception
        Throw ex
      End Try

      Return dtDatos
    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
      m_sqlDtAccTintoreria.Dispose()
    End Sub
    Public Function Actualiza_Estado_Articulo(ByVal pArticulo As String, ByVal strEstado As String, ByVal strUsuario As String) As Boolean
      Dim objParametros() As Object = {"codigo_articulo", pArticulo, "estado_articulo", strEstado, "usuario_modificacion", strUsuario}
      Try
        m_sqlDtAccTintoreria.EjecutarComando("usp_TINT_Actualiza_EstadoArticulo", objParametros).ToString()
      Catch ex As Exception
        Throw ex
      End Try

      Return True
    End Function
#End Region
#Region "Valida si un articulo tiene Ruta --- Giancarlo Vidal "
        Public Function validaArticulo(ByVal strCodigoArticulo As String, ByVal strCodigoFicha As String) As Integer

            Try
                Dim objParametros() As Object = {"CODIGO_ARTICULO_LARGO", strCodigoArticulo, "CODIGO_FICHA", strCodigoFicha}
                Return CType(m_sqlDtAccTintoreria.ObtenerValor("SP_VALIDA_SI_EL_ARTICULO_TIENE_RUTA_V2", objParametros), Integer)
            Catch ex As Exception

            End Try
        End Function


#End Region
	End Class
End Namespace