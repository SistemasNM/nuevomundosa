Imports NM.AccesoDatos
Imports NM_General
Imports System.Text

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

        'Public Function fnc_RegistroArticulos(ByVal pstrNumeroOC As String, ByVal pstrGuia As String, ByVal pstrFecha As String, ByVal pstrUsuario As String, ByVal pstrObs As String, ByVal pdtb_articulos As DataTable, ByRef pdtb_resu As DataTable) As Boolean
        '    'creacion de Nota de Ingreso
        '    Dim clsUtilitario As New NM_General.Util
        '    Dim lbln_retornar As Boolean = False
        '    Try
        '        mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        '        Dim lobjParametros() As Object = { _
        '        "pvch_ordcom", pstrNumeroOC, _
        '        "pvch_guia", pstrGuia, _
        '        "pchr_fechadoc", pstrFecha, _
        '        "pvch_usuario", pstrUsuario, _
        '        "pvch_obs", pstrObs, _
        '        "pvch_xml", clsUtilitario.GeneraXml(pdtb_articulos) _
        '        }
        '        pdtb_resu = mobjConexion.ObtenerDataTable("usp_log_ordencompra_ingreso2", lobjParametros)

        '        lbln_retornar = True
        '    Catch ex As Exception
        '        lbln_retornar = False
        '        Throw ex
        '    End Try
        '    clsUtilitario = Nothing

        '    Return lbln_retornar
        'End Function

        'Public Function fnc_RegistroArticulos_2(ByVal pstrNumeroOC As String, ByVal pstrGuia As String, ByVal pstrFecha As String, ByVal pstrUsuario As String, ByVal pstrObs As String, ByVal pdtb_articulos As DataTable, ByRef pdtb_resu As DataTable) As Boolean
        '    'creacion de Nota de Ingreso
        '    Dim clsUtilitario As New NM_General.Util
        '    Dim lbln_retornar As Boolean = False
        '    Try
        '        mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
        '        Dim lobjParametros() As Object = { _
        '        "pvch_ordcom", pstrNumeroOC, _
        '        "pvch_guia", pstrGuia, _
        '        "pchr_fechadoc", pstrFecha, _
        '        "pvch_usuario", pstrUsuario, _
        '        "pvch_obs", pstrObs, _
        '        "pvch_xml", clsUtilitario.GeneraXml(pdtb_articulos) _
        '        }
        '        pdtb_resu = mobjConexion.ObtenerDataTable("usp_log_ordencompra_ingreso3", lobjParametros)

        '        lbln_retornar = True
        '    Catch ex As Exception
        '        lbln_retornar = False
        '        Throw ex
        '    End Try
        '    clsUtilitario = Nothing

        '    Return lbln_retornar
        'End Function

        Public Function fnc_RegistroArticulos_V4(ByVal pstrTipoDocumento As String,
                                                 ByVal pstrNumeroOC As String,
                                                 ByVal pstrGuia As String,
                                                 ByVal pstrFecha As String,
                                                 ByVal pstrUsuario As String,
                                                 ByVal pstrObs As String,
                                                 ByVal pdtb_articulos As DataTable,
                                                 ByRef pdtb_resu As DataTable) As Boolean
            'creacion de documento de Ingreso
            Dim clsUtilitario As New NM_General.Util
            Dim lbln_retornar As Boolean = False
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = { _
                "pvch_tipodocu", pstrTipoDocumento, _
                "pvch_ordcom", pstrNumeroOC, _
                "pvch_guia", pstrGuia, _
                "pchr_fechadoc", pstrFecha, _
                "pvch_usuario", pstrUsuario, _
                "pvch_obs", pstrObs, _
                "pvch_xml", clsUtilitario.GeneraXml(pdtb_articulos) _
                }
                pdtb_resu = mobjConexion.ObtenerDataTable("usp_log_ordencompra_ingreso_v4", lobjParametros)

                lbln_retornar = True
            Catch ex As Exception
                lbln_retornar = False
                Throw ex
            End Try
            clsUtilitario = Nothing

            Return lbln_retornar
        End Function

        ' Listar ordenes de compra para la aprobacion de Log. (MODI: LUIS_AJ (20180612))
        Public Function ListaOrdenesAprobar() As DataTable
            Dim dbtListado As New DataTable
            dbtListado = Nothing
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = {"var_Usuario", ""}
                dbtListado = mobjConexion.ObtenerDataTable("usp_LOG_Seguimiento_OCOS_3", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dbtListado
        End Function

        ' Listar seguimiento de ordenes - Envio de email
        Public Function ListaUsuarioEmail(strNumeroDoc As String) As DataTable
            Dim dbtListaEmail As New DataTable
            dbtListaEmail = Nothing
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = {"vch_NumeroOrden", strNumeroDoc}
                dbtListaEmail = mobjConexion.ObtenerDataTable("usp_LOG_OrdenCompraSeguimiento", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dbtListaEmail
        End Function

        ' --- Consulta tipos de aprobacion
        Public Function ListaGrupoAprobacion(ByVal strTipo As String, ByVal strCodGrupo As String, ByVal strDesGrupo As String) As DataTable
            Dim strConsultaGrupo As String = ""
            Dim ldtbGrupos As New DataTable
            Try
                ldtbGrupos = Nothing
                strConsultaGrupo = "usp_qry_ListaGrupoAprobacion"

                Dim objParametros As Object() = {"TipoAprobacion", strTipo, "CodGrupoAprobacion", strCodGrupo, "DesGrupoAprobacion", strDesGrupo}
                ldtbGrupos = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataTable(strConsultaGrupo, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbGrupos
        End Function

        'Registra la aprobacion de OC/OS
        Public Sub RegistraAprobacionOrden(Empresa As String, NumeroOrden As String, Usuario As String, CodigoAprobacion As String)

            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = {"chr_Empresa", Empresa, _
                                                  "vch_NumeroOrden", NumeroOrden, _
                                                  "vch_Usuario", Usuario, _
                                                  "vch_CodAprobacion", CodigoAprobacion _
                }
                mobjConexion.EjecutarComando("usp_LOG_AprobarOrdenCompra_2", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' lista correos de los usuarios opcion (Para)
        Public Function ListaCorreosPara(ByVal Empresa As String, ByVal Usuario As String, _
                                             ByVal NumeroOrden As String) As DataTable
            Dim ldtbCorreoPara As New DataTable
            Dim strqryCorreoPara As String = ""
            Try
                ldtbCorreoPara = Nothing
                strqryCorreoPara = "usp_LOG_CorreoParaOrdenCompra"

                Dim objParametros As Object() = {"vch_Empresa", Empresa, _
                                                 "vch_Usuario", Usuario, _
                                                 "vch_NumeroOrden", NumeroOrden
                                                }
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                ldtbCorreoPara = mobjConexion.ObtenerDataTable(strqryCorreoPara, objParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbCorreoPara
        End Function

        'datos de citas proveedores'
        Public Function fncDatosCitasProveedores(strAccion As String, intIdCita As Int32, strNumOrdCompra As String) As DataTable
            Dim dbtListaEmail As New DataTable
            dbtListaEmail = Nothing
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
                Dim lobjParametros() As Object = {"p_vchAccion", strAccion,
                                                  "p_intIdCita", intIdCita,
                                                  "p_vchNumOrdCompra", strNumOrdCompra}
                dbtListaEmail = mobjConexion.ObtenerDataTable("usp_log_atencion_citas_proveedores", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dbtListaEmail
        End Function

#Region "Aprobaciones Facturas OS"

        '*****************************************************************************************************
        'Objetivo   : Obtener Facturas Pend Aprobacion - OS 
        'Autor      : Luis Alanoca J.
        'Creado     : 22/04/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ufn_ListarFacturasPendApro_OCOS(ByVal strCodEmpresa As String, ByVal strCodUsuario As String) As DataTable
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                Dim objParametros As Object() = {"pvch_CodEmpresa", strCodEmpresa,
                                                 "pvch_CodUsuario", strCodUsuario}
                Return mobjConexion.ObtenerDataTable("USP_LOG_LISTADO_FACTURAS_PEND_APROB_OCOS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        '*****************************************************************************************************
        'Objetivo   : Procesa Aprobacion de Facturas por gerencia de logistica 
        'Autor      : Luis Alanoca J.
        'Creado     : 24/04/2017
        'Modificado : //
        '*****************************************************************************************************
        Public Function ufn_AprobarFacturasSeleccionadas_OCOS(ByVal strCodUsuario As String, ByVal dtFacturasSeleccion As DataTable) As Integer
            Dim objUtil As New Util
            Try
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

                dtFacturasSeleccion.TableName = "FACTURAS"
                Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(dtFacturasSeleccion))

                Dim objParametros As Object() = {"pvch_CodUsuario", strCodUsuario,
                                                 "pvch_ListaFacturasXML", strListaRollosXML.ToString}

                Return mobjConexion.EjecutarComando("USP_LOG_GENERAR_APROBACION_FACTURAS_OCOS", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region

    End Class
End Namespace
