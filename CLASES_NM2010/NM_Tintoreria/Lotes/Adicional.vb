Imports NM.AccesoDatos
Namespace Lotes
    Public Class Adicional
        Private mstrUsuario As String

        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub

        Public Function Buscar(ByVal strCodigoLote As String) As DataSet
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoLote", strCodigoLote}
            Dim ldsSet As DataSet
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_AdicionalBuscar", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldsSet
        End Function
        Public Function Grabar(ByVal strLoteReferencia As String, ByVal strCodigoMotivo As String, _
                                ByVal strMaquina As String, ByVal strCC As String, _
                                ByVal dtFichas As DataTable, ByVal dtConsumos As DataTable) As String
            Dim lobjUtil As New NM_General.Util
            Dim lstrXMLFichas As String = lobjUtil.GeneraXml(dtFichas)
            Dim lstrXMLConsumos As String = lobjUtil.GeneraXml(dtConsumos)
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrRes As String
            Dim lstrParametros() As String = {"var_LoteReferencia", strLoteReferencia, "var_CodigoMotivo", strCodigoMotivo, _
            "var_CodigoMaquina", strMaquina, "var_CodigoCC", strCC, "ntx_XMLFichas", lstrXMLFichas, _
            "ntx_XMLConsumos", lstrXMLConsumos, "var_Usuario", mstrUsuario}
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lstrRes = lobjTinto.ObtenerValor("usp_TIN_LoteAdicional_Grabar", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return lstrRes
        End Function
        Public Function Actualizar(ByVal strCodigoLote As String, ByVal strLoteReferencia As String, _
                    ByVal strCodigoMotivo As String, ByVal strMaquina As String, ByVal strCC As String, _
                    ByVal dtFichas As DataTable, ByVal dtConsumos As DataTable) As String
            Dim lobjUtil As New NM_General.Util
            Dim lstrXMLFichas As String = lobjUtil.GeneraXml(dtFichas)
            Dim lstrXMLConsumos As String = lobjUtil.GeneraXml(dtConsumos)
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrRes As String
            Dim lstrParametros() As String = {"var_CodigoLote", strCodigoLote, _
                                            "var_CodigoMaquina", strMaquina, _
                                            "var_LoteReferencia", strLoteReferencia, _
                                            "var_CodigoMotivo", strCodigoMotivo, _
                                            "var_CodigoCC", strCC, _
                                            "ntx_XMLFichas", lstrXMLFichas, _
                                            "ntx_XMLConsumos", lstrXMLConsumos, _
                                            "var_Usuario", mstrUsuario}
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                lstrRes = lobjTinto.ObtenerValor("usp_TIN_Lote_AdicionalActualizar", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return lstrRes
        End Function

        Public Function ObtenerFichas(ByVal strCodigoLote As String, ByVal strCodigoOrden As String, ByVal strCodigoArticulo As String, ByVal pstrCodigoMaquina As String, ByVal pstrFlagDev As Integer) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoLote", strCodigoLote, _
            "var_CodigoOrden", strCodigoOrden, "var_CodigoArticulo", strCodigoArticulo, "var_codigomaquina", pstrCodigoMaquina, "var_flagdev", pstrFlagDev}
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("usp_tin_fichaslote_listar", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
  End Class
End Namespace