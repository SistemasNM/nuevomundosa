Option Strict On

Imports NM.AccesoDatos

Namespace NM.Tintoreria
  Public Class ParoProduccion
    Implements IDisposable

#Region " Declaración de Variables Miembro "
    Private adSQL As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
    Sub New()
      adSQL = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    End Sub

#End Region

#Region " Definición de Métodos "

    Public Function listarParos() As DataTable
      Return adSQL.ObtenerDataTable("pr_NM_ParoProduccion_Tipo_Etapa_Select")
    End Function

    'Public Overloads Function ObtenerParos(ByVal pintTipoLista As Int16, ByVal pstrCodigoMaquina As String, ByVal pstrCodigoEtapa As String, ByVal pstrCodigoTipoParo As String) As DataTable
    '  Dim lobjParametros() As Object = { _
    '  "ptin_tipolista", pintTipoLista, _
    '  "pvch_codigo_maquina", pstrCodigoMaquina, _
    '  "pvch_codigo_etapa", pstrCodigoEtapa, _
    '  "pvch_codigo_tipoparo", pstrCodigoTipoParo _
    '  }
    '  Return adSQL.ObtenerDataTable("pr_NM_ParoProduccion_Etapa_tmp", lobjParametros)

    'End Function

    Public Overloads Function ObtenerParos(ByVal pstrCodigoEtapa As String) As DataTable
      Dim lobjParametros() As Object = { _
      "codigo_etapa", pstrCodigoEtapa _
      }
      Return adSQL.ObtenerDataTable("pr_NM_ParoProduccion_Etapa", lobjParametros)

    End Function

    Public Function Insertar(ByVal pParo As String, ByVal pEtapa As String, ByVal pTipoParo As String, ByVal pDescripcion As String, ByVal pUsuario As String) As Boolean

      Dim objParametros As Object() = {"codigo_paro", pParo, _
                                      "codigo_etapa", pEtapa, _
                                      "codigo_tipoparoproduccion", pTipoParo, _
                                      "descripcion_paro", pDescripcion, _
                                      "usuario_creacion", pUsuario}
      Try
        adSQL.EjecutarComando("pr_NM_ParoProduccion_Insert", objParametros)

      Catch ex As Exception
        Throw ex
      End Try

      Return True

    End Function

    Public Function Actualizar(ByVal pParo As String, ByVal pEtapa As String, ByVal pTipoParo As String, ByVal pDescripcion As String, ByVal pUsuario As String) As Boolean

      Dim objParametros As Object() = {"codigo_paro", pParo, _
                                      "codigo_etapa", pEtapa, _
                                      "codigo_tipoparoproduccion", pTipoParo, _
                                      "descripcion_paro", pDescripcion, _
                                      "usuario_modificacion", pUsuario}
      Try
        adSQL.EjecutarComando("pr_NM_ParoProduccion_Update", objParametros)

      Catch ex As Exception
        Throw ex
      End Try

      Return True

    End Function

    Public Function Eliminar(ByVal pParo As String, ByVal pEtapa As String, ByVal pTipoParo As String) As Boolean

      Dim objParametros As Object() = {"codigo_paro", pParo, _
                                      "codigo_etapa", pEtapa, _
                                      "codigo_tipoparoproduccion", pTipoParo}

      Try
        adSQL.EjecutarComando("pr_NM_ParoProduccion_Delete", objParametros)

      Catch ex As Exception
        Throw ex
      End Try

      Return True

    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
      adSQL.Dispose()
    End Sub
#End Region

  End Class
End Namespace