Imports NM.AccesoDatos
Imports NM_General
Namespace Logistica

  Public Class OrdenCompra
    Private mobjConexion As AccesoDatosSQLServer

        Public Function fnc_Listar(ByVal pintTipoConsulta As Int16, ByVal pstrNumeroOC As String) As DataTable
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = { _
                "ptin_tipoconsulta", pintTipoConsulta, _
                "pvch_ordencompra", pstrNumeroOC _
                }
                Return mobjConexion.ObtenerDataTable("usp_log_ordencompra_listar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fnc_Listar2(ByVal pintTipoConsulta As Int16, ByVal pstrNumeroOC As String) As DataSet
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = { _
                "ptin_tipoconsulta", pintTipoConsulta, _
                "pvch_ordencompra", pstrNumeroOC _
                }
                Return mobjConexion.ObtenerDataSet("usp_log_ordencompra_listar", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fnc_ActualizarDatosEnvio(ByVal pstrNumeroOC As String, ByVal pstrUsuario As String, ByVal pstrEmailDestino As String) As DataTable
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = { _
                "pvch_ordencompra", pstrNumeroOC, _
                "pvch_usuario", pstrUsuario, _
                "pvch_cuenta", pstrEmailDestino _
                }
                Return mobjConexion.ObtenerDataTable("usp_log_ordencompra_actualizarenvio", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fnc_RegistroArticulos(ByVal pstrNumeroOC As String, ByVal pstrGuia As String, ByVal pstrFecha As String, ByVal pstrUsuario As String, ByVal pstrObs As String, ByVal pdtb_articulos As DataTable) As Boolean
            'creacion de Nota de Ingreso
            Dim clsUtilitario As New NM_General.Util
            Dim lbln_retornar As Boolean = False
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = { _
                "pvch_ordcom", pstrNumeroOC, _
                "pvch_guia", pstrGuia, _
                "pchr_fechadoc", pstrFecha, _
                "pvch_usuario", pstrUsuario, _
                "pvch_obs", pstrObs, _
                "pvch_xml", clsUtilitario.GeneraXml(pdtb_articulos) _
                }
                mobjConexion.EjecutarComando("usp_log_ordencompra_ingreso2", lobjParametros)
                lbln_retornar = True
            Catch ex As Exception
                lbln_retornar = False
                Throw ex
            End Try
            clsUtilitario = Nothing

            Return lbln_retornar
        End Function

    End Class

End Namespace
