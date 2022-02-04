Imports NM.AccesoDatos
Imports System.Text

Public Class clsGerencia
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private _objConnexion As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    End Sub
#End Region

#Region " Funciones "

    Public Function fn_enviarCorreoFormatos(ByVal pstrAsunto As String,
                                            ByVal pstrRuta As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchAsunto", pstrAsunto,
                                             "pvchRuta", pstrRuta}
            Return _objConnexion.ObtenerDataTable("USP_GG_ENVIAR_CORREO_FORMATO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function enviarCorreoFormatos_V2(ByVal pstrAsunto As String, ByVal pstrRuta As String, ByVal intCodFormulario As Int32, ByVal intCodFormato As Integer) As DataTable
        Try
            Dim objparametros() As Object = {"pvchAsunto", pstrAsunto,
                                             "pvchRuta", pstrRuta,
                                             "pintCodGenerado", intCodFormulario,
                                             "pintCodFormato", intCodFormato}
            Return _objConnexion.ObtenerDataTable("USP_GG_ENVIAR_CORREO_FORMATO_V2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function fn_aprobarFormatos(ByVal pstrTipOper As String,
                                       ByVal pintCodGenFor As Int32,
                                       ByVal pintCodFormato As Int32,
                                       ByVal pvchUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchTipOper", pstrTipOper,
                                             "pintCodGenForm", pintCodGenFor,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchUsuario", pvchUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_APROBAR_FORMATO_FIRMAS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_solicitarAprobacion(ByVal pstrCodEmpr As String,
                                           ByVal pintCodGenFor As Int32,
                                           ByVal pintCodFormato As Int32,
                                           ByVal pvchEstFormato As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodEmpr", pstrCodEmpr,
                                             "pintCodGenerado", pintCodGenFor,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchEstFormato", pvchEstFormato}
            Return _objConnexion.ObtenerDataTable("USP_GG_ESTADOS_FORMATOS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ObtieneFormato(ByVal pstrCodEmpr As String,
                                      ByVal pintCodGenFor As Int32,
                                      ByVal pintCodFormato As Int32,
                                      ByVal pintCodVersion As Int32) As DataSet
        Try
            Dim objparametros() As Object = {"pvchCodEmpr", pstrCodEmpr,
                                             "pintCodGenerado", pintCodGenFor,
                                             "pintCodFormato", pintCodFormato,
                                             "pintCodVersion ", pintCodVersion}
            Return _objConnexion.ObtenerDataSet("USP_GG_OBTENER_FORMATO_PROTOCOLO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_GeneraFormato(ByVal pstrCodEmpr As String,
                                     ByVal pintCodFormato As Int32,
                                     ByVal pintCodVersion As Int32,
                                     ByVal pstrUsuario As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchCodEmpr", pstrCodEmpr,
                                             "pintCodFormato", pintCodFormato,
                                             "pintCodVersion ", pintCodVersion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataSet("USP_GG_GENERA_FORMATO_PROTOCOLO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function fn_ListarFormatoCP(ByVal pstrCodEmpr As String,
    '                                   ByVal pintCodFormato As Int32,
    '                                   ByVal pintCodVersion As Int32) As DataSet
    '    Try
    '        Dim objparametros() As Object = {"pvchCodEmpr", pstrCodEmpr,
    '                                         "pintCodFormato", pintCodFormato,
    '                                         "pintCodVersion ", pintCodVersion}
    '        Return _objConnexion.ObtenerDataSet("USP_GG_LISTA_FORMATO_CAMBIOPROCESO", objparametros)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function fn_ModificarFormatoProt(ByVal pintCodGenForm As Int32,
                                              ByVal pstrCodArea As String,
                                              ByVal pstrCodMaqu As String,
                                              ByVal pstrCodResp As String,
                                              ByVal pstrFecIni As String,
                                              ByVal pstrObjCamb As String,
                                              ByVal pstrDetCons As String,
                                              ByVal pstrFecConc As String,
                                              ByVal pstrTxtConc As String,
                                              ByVal pstrUsuario As String,
                                                ByVal strFilenameficha As String,
                                                ByVal intFileSizeficha As Integer,
                                                ByVal strConteTypeficha As String,
                                                ByVal strFileExtensionficha As String
                                              ) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pvchCodArea", pstrCodArea,
                                             "pvchCodMaqu ", pstrCodMaqu,
                                             "pvchCodResp", pstrCodResp,
                                             "pvchFecIni", pstrFecIni,
                                             "pvchObjCamb", pstrObjCamb,
                                             "pvchDetCons", pstrDetCons,
                                             "pvchFecConc", pstrFecConc,
                                             "pvchTxtConc", pstrTxtConc,
                                             "pvchUsuario", pstrUsuario,
                                             "pvchFilenameficha", strFilenameficha,
                                             "intFileSizeficha", intFileSizeficha,
                                             "pvchConteTypeficha", strConteTypeficha,
                                             "pvchFileExtensionficha", strFileExtensionficha}
            Return _objConnexion.ObtenerDataTable("USP_GG_MODIFICAR_FORMATO_PROTOCOLO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function fn_ModificarFormatoProt_2(ByVal pintCodGenForm As Int32,
                                              ByVal pstrCodResp As String,
                                              ByVal pstrFecIni As String,
                                              ByVal pstrCodProv As String,
                                              ByVal pstrDetProd As String,
                                              ByVal pstrProcProd As String,
                                              ByVal pstrObjCamb As String,
                                              ByVal pstrDetCons As String,
                                              ByVal pstrFecConc As String,
                                              ByVal pstrTxtConc As String,
                                              ByVal pstrUsuario As String,
                                              ByVal pdatfecha_creacion_prueba As String,
                                              ByVal pstrcod_tipoprueba As String,
                                              ByVal pstrcod_fabricante As String,
                                              ByVal pstrplanta As String,
                                              ByVal pstrpais As String,
                                              ByVal pstrunidad_medida As String,
                                              ByVal pdoucantidad As Double,
                                              ByVal pstrlote As String,
                                              ByVal pstrfecha_produccion As String,
                                              ByVal pstrubicacion_nuevo_material As String,
                                              ByVal pstrcaracteristicas_embalaje As String,
                                              ByVal pstrcodigo_material As String,
                                              ByVal pstrcaracteristicas_relevante_material As String,
                                              ByVal pstrestado_insumo As String,
                                              ByVal pstrobservaciones_insumo As String,
                                              ByVal pstrnro_preliminar As String,
                                              ByVal strFilenameficha As String,
                                              ByVal intFileSizeficha As Integer,
                                              ByVal strConteTypeficha As String,
                                              ByVal strFileExtensionficha As String,
                                              ByVal strFilenamecertificado As String,
                                              ByVal intFileSizecertificado As Integer,
                                              ByVal strConteTypecertificado As String,
                                              ByVal strFileExtensioncertificado As String,
                                              ByVal strFilenamecarta As String,
                                              ByVal intFileSizecarta As Integer,
                                              ByVal strConteTypecarta As String,
                                              ByVal strFileExtensioncarta As String,
                                               ByVal strFilenamedocumento As String,
                                              ByVal intFileSizedocumento As Integer,
                                              ByVal strConteTypedocumento As String,
                                              ByVal strFileExtensiondocumento As String
                                             ) As DataTable
        Try

            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pvchCodResp", pstrCodResp,
                                             "pvchFecIni ", Convert.ToDateTime(pstrFecIni).ToString("yyyyMMdd"),
                                             "pvchCodProv", pstrCodProv,
                                             "pvchDetProd", pstrDetProd,
                                             "pvchProcProd", pstrProcProd,
                                             "pvchObjCamb", pstrObjCamb,
                                             "pvchDetCons", pstrDetCons,
                                             "pvchFecConc", Convert.ToDateTime(pstrFecConc).ToString("yyyyMMdd"),
                                             "pvchTxtConc", pstrTxtConc,
                                             "pvchUsuario", pstrUsuario,
                                             "pdt_fecha_creacion_prueba", Convert.ToDateTime(pdatfecha_creacion_prueba).ToString("yyyyMMdd"),
                                             "pvch_cod_tipoprueba", pstrcod_tipoprueba,
                                             "pvch_cod_fabricante", pstrcod_fabricante,
                                             "pvch_planta", pstrplanta,
                                             "pcod_pais", pstrpais,
                                             "pvch_unidad_medida", pstrunidad_medida,
                                             "pint_cantidad", pdoucantidad,
                                             "pvch_lote", pstrlote,
                                             "pvch_fecha_produccion", pstrfecha_produccion,
                                             "pvch_ubicacion_nuevo_material", pstrubicacion_nuevo_material,
                                             "pvch_caracteristicas_embalaje", pstrcaracteristicas_embalaje,
                                             "pvch_codigo_material", pstrcodigo_material,
                                             "pvch_caracteristicas_relevante_material", pstrcaracteristicas_relevante_material,
                                             "pvch_estado_insumo", pstrestado_insumo,
                                             "pvch_observaciones_insumo", pstrobservaciones_insumo,
                                              "pvch_nro_preliminar", pstrnro_preliminar,
                                             "vch_FileNameficha", strFilenameficha, "int_FileSizeficha", intFileSizeficha, "vch_ContentTypeficha", strConteTypeficha, "vch_FileExtensionficha", strFileExtensionficha,
                                             "vch_FileNamecertificado", strFilenamecertificado, "int_FileSizecertificado", intFileSizecertificado, "vch_ContentTypecertificado", strConteTypecertificado, "vch_FileExtensioncertificado", strFileExtensioncertificado,
                                             "vch_FileNamecarta", strFilenamecarta, "int_FileSizecarta", intFileSizecarta, "vch_ContentTypecarta", strConteTypecarta, "vch_FileExtensioncarta", strFileExtensioncarta,
                                              "vch_FileNamedocumento", strFilenamedocumento, "int_FileSizedocumento", intFileSizedocumento, "vch_ContentTypedocumento", strConteTypedocumento, "vch_FileExtensiondocumento", strFileExtensiondocumento
                                             }
            Return _objConnexion.ObtenerDataTable("USP_GG_MODIFICAR_FORMATO_PROTOCOLO_222", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ModificarParamForm(ByVal pintCodGenParam As Int32,
                                          ByVal pstrValCampo As String,
                                          ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenParam", pintCodGenParam,
                                             "pvchValCampo", pstrValCampo,
                                             "pvchUsuario ", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_MODIFICAR_FORMATO_PARAMETROS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_ModificarParamFormPuesto(ByVal pintCodGenParam As Int32,
                                          ByVal pstrValCampo As String, ByVal pintPuntaje As Integer, ByVal pintCodGenFor As Integer, ByVal pstrBloqu As String,
                                          ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenParam", pintCodGenParam,
                                             "pvchValCampo", pstrValCampo, "pintPuntaje", pintPuntaje, "pintCodGenFor", pintCodGenFor, "pvchBloque", pstrBloqu,
                                             "pvchUsuario ", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_MODIFICAR_FORMATO_PARAMETROS_PUESTO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_AgregarParametros(ByVal pintCodGenForm As Int32,
                                         ByVal pintCodFormato As Int32,
                                         ByVal pstrCodBloque As String,
                                         ByVal pstrCodCampo As String,
                                         ByVal pstrValCampo As String,
                                         ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchCodBloque ", pstrCodBloque,
                                             "pvchCodCampo", pstrCodCampo,
                                             "pvchValCampo", pstrValCampo,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_AGREGAR_FORMATO_PARAMETROS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_AgregarParametrosInformacion(ByVal pintCodGenForm As Int32,
                                        ByVal pintCodFormato As Int32,
                                        ByVal pstrCodBloque As String,
                                        ByVal pstrValCampo As String,
                                        ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchCodBloque ", pstrCodBloque,
                                             "pvchValCampo", pstrValCampo,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_AGREGAR_FORMATO_PARAMETROS_INFO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function fn_AgregarParametrosInformacion(ByVal pintCodGenForm As Int32,
    '                                    ByVal pintCodFormato As Int32,
    '                                    ByVal pstrCodBloque As String,
    '                                    ByVal pstrValCampo As String,
    '                                    ByVal pstrPanel2 As String,
    '                                    ByVal pstrPanel3 As String,
    '                                    ByVal pstrUsuario As String) As DataSet
    '    Try
    '        Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
    '                                         "pintCodFormato", pintCodFormato,
    '                                         "pvchCodBloque ", pstrCodBloque,
    '                                         "pvchValCampo", pstrValCampo,
    '                                         "pvchPanel2", pstrPanel2,
    '                                         "pvchPanel3", pstrPanel3,
    '                                         "pvchUsuario", pstrUsuario}
    '        Return _objConnexion.ObtenerDataSet("USP_GG_AGREGAR_FORMATO_PARAMETROS_INFO", objparametros)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function fn_CargarListas(ByVal pstrAux As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchAux", pstrAux}
            Return _objConnexion.ObtenerDataSet("USP_GG_LISTAR_DATOS_FORMATO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_AgregarFirmas(ByVal pintCodGenForm As Int32,
                                     ByVal pintCodFormato As Int32,
                                     ByVal pstrValCampo As String,
                                     ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchValCampo", pstrValCampo,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_AGREGAR_FORMATO_FIRMAS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_AgregarFirmas_HRuta(ByVal pintCodGenForm As Int32,
                                    ByVal pintCodFormato As Int32,
                                    ByVal pstrValCampo As String,
                                    ByVal pstrUsuario As String,
                                    ByVal pbitProcesoHRuta As Boolean) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchValCampo", pstrValCampo,
                                             "pvchUsuario", pstrUsuario,
                                             "pbitProcesoHRuta", pbitProcesoHRuta}
            Return _objConnexion.ObtenerDataTable("USP_GG_AGREGAR_FORMATO_FIRMAS_HRUTA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function fn_EliminarFirma(ByVal pintCodGenForm As Int32,
                                     ByVal pintCodFormato As Int32,
                                     ByVal pstrValCampo As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchValCampo", pstrValCampo}
            Return _objConnexion.ObtenerDataTable("USP_GG_ELIMINAR_FORMATO_FIRMAS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_EliminarFirma_HRuta(ByVal pintCodGenForm As Int32,
                                    ByVal pintCodFormato As Int32,
                                    ByVal pstrValCampo As String,
                                     ByVal pbitProcesoHRuta As Boolean) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchValCampo", pstrValCampo,
                                              "pbitProcesoHRuta", pbitProcesoHRuta}
            Return _objConnexion.ObtenerDataTable("USP_GG_ELIMINAR_FORMATO_FIRMAS_HRUTA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_EliminarParam(ByVal pintCodGenForm As Int32,
                                     ByVal pintCodGenParam As Int32,
                                     ByVal pstrCodBloque As String,
                                     ByVal pintCodFormato As Int32) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodGenParam", pintCodGenParam,
                                             "pvchCodBloque", pstrCodBloque,
                                             "pintCodFormato", pintCodFormato}
            Return _objConnexion.ObtenerDataTable("USP_GG_ELIMINAR_FORMATO_PARAMETROS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_EliminarParamInformacion(ByVal pintCodGenForm As Int32,
                                    ByVal pintCodGenParam As Int32,
                                    ByVal pstrCodBloque As String,
                                    ByVal pintCodFormato As Int32) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pintCodGenParam", pintCodGenParam,
                                             "pvchCodBloque", pstrCodBloque,
                                             "pintCodFormato", pintCodFormato}
            Return _objConnexion.ObtenerDataTable("USP_GG_ELIMINAR_FORMATO_PARAMETROS_INFO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_GeneraFormato_5(ByVal pstrCodEmpr As String,
                                       ByVal pintCodFormato As Int32,
                                       ByVal pintCodVersion As Int32,
                                       ByVal pstrUsuario As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchCodEmpr", pstrCodEmpr,
                                             "pintCodFormato", pintCodFormato,
                                             "pintCodVersion ", pintCodVersion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataSet("USP_GG_GENERA_FORMATO_PROTOCOLO_5", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_ObtenerFormatoPorArea(ByVal strCodPuesto As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvch_Cod_Puesto", strCodPuesto}

            Return _objConnexion.ObtenerDataSet("USP_GG_OBTENER_FORMATO_PUESTO_PROMOCIONA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ObtenerFormato_5(ByVal pintCodGenFormato As Int32,
                                        ByVal pintCodFormato As Int32) As DataSet
        Try
            Dim objparametros() As Object = {"pintCodGenerado", pintCodGenFormato,
                                             "pintCodFormato", pintCodFormato}
            Return _objConnexion.ObtenerDataSet("USP_GG_OBTENER_FORMATO_PROTOCOLO_5", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ObtenerFormato_2(ByVal pintCodGenFormato As Int32,
                                        ByVal pintCodFormato As Int32) As DataSet
        Try
            Dim objparametros() As Object = {"pintCodGenFormato", pintCodGenFormato,
                                             "pintCodFormato", pintCodFormato}
            Return _objConnexion.ObtenerDataSet("USP_GG_OBTENER_FORMATO_PROTOCOLO_222", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_ObtenerUsuarioFirma(ByVal pintCodGenFormato As Int32,
                                       ByVal pintCodFormato As Int32,
                                       ByVal pbitHRuta As Boolean) As DataSet
        Try
            Dim objparametros() As Object = {"pintCodGenFormato", pintCodGenFormato,
                                             "pintCodFormato", pintCodFormato,
                                             "pbitprocesoHRuta", pbitHRuta}
            Return _objConnexion.ObtenerDataSet("USP_GG_OBTENER_USUARIO_FIRMA_HRUTA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ObtenerTipoPrueba(ByVal pintCodGenerado As Int32, ByVal pintCodFormato As Int32) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenerado", pintCodGenerado,
                                            "pintCodFormato", pintCodFormato}
            Return _objConnexion.ObtenerDataTable("USP_CALIDAD_OBTENER_TIPOPRUEBA", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function fn_ObtenerPruebaPreliminar(ByVal pintCodGenFormato As Int32,
                                        ByVal pintCodFormato As Int32, ByVal pstrTipoPrueba As String,
                                        ByVal COD_USUA As String) As DataSet
        Try
            Dim objparametros() As Object = {"pintCodGenFormato", pintCodGenFormato,
                                             "pintCodFormato", pintCodFormato,
                                             "pstrTipoPrueba", pstrTipoPrueba,
                                             "COD_USUA", COD_USUA}
            Return _objConnexion.ObtenerDataSet("USP_GG_OBTENER_PRUEBA_PROTOCOLO_2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_ActualizarrPruebaPreliminar(ByVal pintCodGenFormato As Int32,
                                       ByVal pintCodFormato As Int32, ByVal pvchConclusionesFianl As String,
                                       ByVal pstrTipoPrueba As String, ByVal pstrPRUEBA_NRO As String,
                                       ByVal pstrFechaCalidad As String, ByVal pstrFechaDireccion As String) As Integer
        Try
            

            If pstrFechaCalidad = "" Then
                pstrFechaCalidad = Nothing
            Else
                pstrFechaCalidad = Convert.ToDateTime(pstrFechaCalidad).ToString("yyyyMMdd")
            End If
            If pstrFechaDireccion = "" Then
                pstrFechaDireccion = Nothing
            Else
                pstrFechaDireccion = Convert.ToDateTime(pstrFechaDireccion).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pintCodGenFormato", pintCodGenFormato,
                                             "pintCodFormato", pintCodFormato,
                                             "pvchConclusionesFianl", pvchConclusionesFianl,
                                             "pstrTipoPrueba", pstrTipoPrueba,
                                             "pstrPRUEBA_NRO", pstrPRUEBA_NRO,
                                             "pdatFechaCalidad", pstrFechaCalidad,
                                             "pdatFechaDireccion", pstrFechaDireccion}
        
            Return _objConnexion.EjecutarComando("USP_GG_ACTUALIZAR_PRUEBA_PROTOCOLO_2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_CargarListarAdmi(ByVal pstrTipLista As String,
                                        ByVal pstrCodigo As String,
                                        ByVal pstrDescrip As String,
                                        ByVal pstrCodArea As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchTipLista", pstrTipLista,
                                             "pvchCodigo", pstrCodigo,
                                             "pvchDescrip", pstrDescrip,
                                             "pvchCodArea", pstrCodArea}
            Return _objConnexion.ObtenerDataTable("USP_GG_BUSQUEDA_LISTAS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function fn_ObtenerDatosTrab(ByVal pstrCodTrab As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodTrab", pstrCodTrab}
            Return _objConnexion.ObtenerDataTable("USP_GG_OBTENER_DATOS_TRABAJADOR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ModificarFormatoProt_5(ByVal pintCodGenForm As Int32,
                                              ByVal pstrNombreTrab As String,
                                              ByVal pstrFec As String,
                                              ByVal pstrCodTrab As String,
                                              ByVal pstrPuestAct As String,
                                              ByVal pstrPuestProm As String,
                                              ByVal pstrCodArea As String,
                                              ByVal pstrFecIni As String,
                                              ByVal pstrCodSup As String,
                                              ByVal pstrNomComp As String,
                                              ByVal pstrTiempServ As String,
                                              ByVal pstrOpinServ As String,
                                              ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pintCodGenForm", pintCodGenForm,
                                             "pvchNombreTrab", pstrNombreTrab,
                                             "pvchFec ", pstrFec,
                                             "pvchCodTrab", pstrCodTrab,
                                             "pvchPuestAct", pstrPuestAct,
                                             "pvchPuestProm", pstrPuestProm,
                                             "pvchCodArea", pstrCodArea,
                                             "pvchFecIni", pstrFecIni,
                                             "pvchCodSup", pstrCodSup,
                                             "pvchNomComp", pstrNomComp,
                                             "pvchTiempServ", pstrTiempServ,
                                             "pvchOpinServ", pstrOpinServ,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("USP_GG_MODIFICAR_FORMATO_PROTOCOLO_5", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function fn_GeneraFormato_2(ByVal pstrCodEmpr As String,
                                       ByVal pintCodFormato As Int32,
                                       ByVal pintCodVersion As Int32,
                                       ByVal pstrUsuario As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvchCodEmpr", pstrCodEmpr,
                                             "pintCodFormato", pintCodFormato,
                                             "pintCodVersion ", pintCodVersion,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataSet("USP_GG_GENERA_FORMATO_PROTOCOLO_222", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_listarFormatoCP(ByVal pstrCodArea As String,
                                       ByVal pstrCodMaqui As String,
                                       ByVal pstrCodRespo As String,
                                       ByVal pstrSolicit As String,
                                       ByVal pstrEstado As String,
                                       ByVal pstrFecIni As String,
                                       ByVal pstrFecFin As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodArea", pstrCodArea,
                                             "pvchCodMaqui", pstrCodMaqui,
                                             "pvchCodRespo", pstrCodRespo,
                                             "pvchSolicit", pstrSolicit,
                                             "pvchEstado", pstrEstado,
                                             "pvchFecIni", pstrFecIni,
                                             "pvchFecFin", pstrFecFin}
            Return _objConnexion.ObtenerDataTable("USP_GG_LISTAR_FORMATO_CAMBIO_PROCESO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_listarFormatoCPT(ByVal pstrCodArea As String,
                                        ByVal pstrCodTrab As String,
                                        ByVal pstrEstado As String,
                                        ByVal pstrFecIni As String,
                                        ByVal pstrFecFin As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodArea", pstrCodArea,
                                             "pvchCodTrab", pstrCodTrab,
                                             "pvchEstado", pstrEstado,
                                             "pvchFecIni", pstrFecIni,
                                             "pvchFecFin", pstrFecFin}
            Return _objConnexion.ObtenerDataTable("USP_GG_LISTAR_FORMATO_CAMBIO_PUESTO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_listarFormatoCNI(ByVal pstrCodResp As String,
                                        ByVal pstrCodProv As String,
                                        ByVal pstrEstado As String,
                                        ByVal pstrFecIni As String,
                                        ByVal pstrFecFin As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchCodResp", pstrCodResp,
                                             "pvchCodProv", pstrCodProv,
                                             "pvchEstado", pstrEstado,
                                             "pvchFecIni", pstrFecIni,
                                             "pvchFecFin", pstrFecFin}
            Return _objConnexion.ObtenerDataTable("USP_GG_LISTAR_FORMATO_NUEVO_INSUMO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_listarTablaMaestra(ByVal strCodigoTabla As String,
                                      ByVal strWhere As String) As DataTable
        Try
            Dim objparametros() As Object = {"chr_CodigoTabla", strCodigoTabla,
                                             "vch_Where", strWhere}

            Return _objConnexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDatoWhere_Listar", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_obtenerTrabajadoresSPL(ByVal pstrAux As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchAux", pstrAux}
            Return _objConnexion.ObtenerDataTable("USP_PROD_OTBENER_TRABAJADORES_SPL", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_obtenerTrabajadoresPRS(ByVal pstrAux As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchAux", pstrAux}
            Return _objConnexion.ObtenerDataTable("USP_PROD_OTBENER_TRABAJADORES_PRS", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_obtenerTrabajadoresSPL2(ByVal pstrAux As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchAux", pstrAux}
            Return _objConnexion.ObtenerDataTable("USP_PROD_OTBENER_TRABAJADORES_SPL_v2", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_obtenerTrabajadoresCTS(ByVal pstrAux As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchAux", pstrAux}
            Return _objConnexion.ObtenerDataTable("usp_gg_generar_trab_cts_banco", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_configuracionPLCliente(ByVal pstrOperacion As String,
                                              ByVal pstrCodProv As String,
                                              ByVal pstrFlagPL As String,
                                              ByVal pstrEmail As String,
                                              ByVal pstrUsuario As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchOperacion", pstrOperacion,
                                             "pvchCodProv", pstrCodProv,
                                             "pvchFlagPL", pstrFlagPL,
                                             "pvchEmail", pstrEmail,
                                             "pvchUsuario", pstrUsuario}
            Return _objConnexion.ObtenerDataTable("usp_vent_config_pl_cliente", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_obtenerPaises() As DataTable
        Try

            Return _objConnexion.ObtenerDataTable("USP_TTPAIS_OBTENER_PAISES")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region " Dispose "
    Public Sub Dispose() Implements System.IDisposable.Dispose
        _objConnexion.Dispose()
    End Sub
#End Region



#Region "Hoja de Ruta"

    Public Function fn_InsertarHilanderia(ByVal pstrPRUEBA_NRO As String,
                                          ByVal pstrFECHA_PRODUCCION_1 As String,
                                          ByVal pstrMAQUINA_1 As String,
                                          ByVal pstrSUPERVISOR_1 As String,
                                          ByVal pstrOPERARIO_1 As String,
                                          ByVal pstrTURNO_1 As String,
                                          ByVal pstrLINEA_1 As String,
                                          ByVal pstrPROCESO_1 As String,
                                          ByVal pstrMATERIAL_1 As String,
                                          ByVal pstrFECHA_PRODUCCION_2 As String,
                                          ByVal pstrMAQUINA_2 As String,
                                          ByVal pstrSUPERVISOR_2 As String,
                                          ByVal pstrOPERARIO_2 As String,
                                          ByVal pstrTURNO_2 As String,
                                          ByVal pstrLINEA_2 As String,
                                          ByVal pstrPROCESO_2 As String,
                                          ByVal pstrMATERIAL_2 As String,
                                          ByVal pstrFECHA_PRODUCCION_3 As String,
                                          ByVal pstrMAQUINA_3 As String,
                                          ByVal pstrSUPERVISOR_3 As String,
                                          ByVal pstrOPERARIO_3 As String,
                                          ByVal pstrTURNO_3 As String,
                                          ByVal pstrLINEA_3 As String,
                                          ByVal pstrPROCESO_3 As String,
                                          ByVal pstrMATERIAL_3 As String,
                                          ByVal pstrCONTINUA_VALOR_1 As String,
                                          ByVal pstrCONTINUA_OBS_1 As String,
                                          ByVal pstrTORSION_VALOR_1 As String,
                                          ByVal pstrTORSION_OBS_1 As String,
                                          ByVal pstrESTIRAJE_VALOR_1 As String,
                                          ByVal pstrESTIRAJE_OBS_1 As String,
                                          ByVal pstrVELOCIDAD_VALOR_1 As String,
                                          ByVal pstrVELOCIDAD_OBS_1 As String,
                                          ByVal pstrCURSORES_VALOR_1 As String,
                                          ByVal pstrCURSORES_OBS_1 As String,
                                          ByVal pstrCLIPS_VALOR_1 As String,
                                          ByVal pstrCLIPS_OBS_1 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_VALOR_1 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_OBS_1 As String,
                                          ByVal pstrTEMPERATURA_SALA_VALOR_1 As String,
                                          ByVal pstrTEMPERATURA_SALA_OBS_1 As String,
                                          ByVal pstrOTROS_VALOR_1 As String,
                                          ByVal pstrOTROS_OBS_1 As String,
                                          ByVal pstrOPEN_RIETER_11 As String,
                                          ByVal pstrOPEN_RIETER_OBS_11 As String,
                                          ByVal pstrTORSION_VALOR_11 As String,
                                          ByVal pstrTORSION_OBS_11 As String,
                                          ByVal pstrTENSION_VALOR_11 As String,
                                          ByVal pstrTENSION_OBS_11 As String,
                                          ByVal pstrESTIRAJE_VALOR_11 As String,
                                          ByVal pstrESTIRAJE_OBS_11 As String,
                                          ByVal pstrVELOCIDAD_ROTOR_VALOR_11 As String,
                                          ByVal pstrVELOCIDAD_ROTOR_OBS_11 As String,
                                          ByVal pstrVELOCIDAD_DISGREGADOR_VALOR_11 As String,
                                          ByVal pstrVELOCIDAD_DISGREGADOR_VALOR_OBS_11 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_VALOR_11 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_OBS_11 As String,
                                          ByVal pstrTEMPERATURA_SALA_VALOR_11 As String,
                                          ByVal pstrTEMPERATURA_SALA_OBS_11 As String,
                                          ByVal pstrOTROS_VALOR_11 As String,
                                          ByVal pstrOTROS_OBS_11 As String,
                                          ByVal pstrCONERA_VALOR_2 As String,
                                          ByVal pstrCONERA_OBS_2 As String,
                                          ByVal pstrTENSION_VALOR_2 As String,
                                          ByVal pstrTENSION_OBS_2 As String,
                                          ByVal pstrLONGITUD_VALOR_2 As String,
                                          ByVal pstrLONGITUD_OBS_2 As String,
                                          ByVal pstrVELOCIDAD_VALOR_2 As String,
                                          ByVal pstrVELOCIDAD_OBS_2 As String,
                                          ByVal pstrPRESION_MARCO_PORTABOBINA_VALOR_2 As String,
                                          ByVal pstrPRESION_MARCO_PORTABOBINA_OBS_2 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_VALOR_2 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_OBS_2 As String,
                                          ByVal pstrTEMPERATURA_SALA_VALOR_2 As String,
                                          ByVal pstrTEMPERATURA_SALA_OBS_2 As String,
                                          ByVal pstrOTROS_VALOR_2 As String,
                                          ByVal pstrOTROS_OBS_2 As String,
                                          ByVal pstrRECUBRIDORA_VALOR_3 As String,
                                          ByVal pstrRECUBRIDORA_OBS_3 As String,
                                          ByVal pstrVELOCIDAD_RECUBRIDORA_VALOR_3 As String,
                                          ByVal pstrVELOCIDAD_RECUBRIDORA_OBS_3 As String,
                                          ByVal pstrRECETA_VALOR_3 As String,
                                          ByVal pstrRECETA_OBS_3 As String,
                                          ByVal pstrOTROS_VALOR_3 As String,
                                          ByVal pstrOTROS_OBS_3 As String
                                          ) As Integer
        Try

            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If

            If pstrFECHA_PRODUCCION_2 = "" Then
                pstrFECHA_PRODUCCION_2 = Nothing
            Else
                pstrFECHA_PRODUCCION_2 = Convert.ToDateTime(pstrFECHA_PRODUCCION_2).ToString("yyyyMMdd")
            End If

            If pstrFECHA_PRODUCCION_3 = "" Then
                pstrFECHA_PRODUCCION_3 = Nothing
            Else
                pstrFECHA_PRODUCCION_3 = Convert.ToDateTime(pstrFECHA_PRODUCCION_3).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                              "pvchFECHA_PRODUCCION_1", pstrFECHA_PRODUCCION_1,
                                              "pvchMAQUINA_1", pstrMAQUINA_1,
                                              "pvchSUPERVISOR_1", pstrSUPERVISOR_1,
                                              "pvchOPERARIO_1", pstrOPERARIO_1,
                                              "pvchTURNO_1", pstrTURNO_1,
                                              "pvchLINEA_1", pstrLINEA_1,
                                              "pvchPROCESO_1", pstrPROCESO_1,
                                              "pvchMATERIAL_1", pstrMATERIAL_1,
                                              "pvchFECHA_PRODUCCION_2", pstrFECHA_PRODUCCION_2,
                                              "pvchMAQUINA_2", pstrMAQUINA_2,
                                              "pvchSUPERVISOR_2", pstrSUPERVISOR_2,
                                              "pvchOPERARIO_2", pstrOPERARIO_2,
                                              "pvchTURNO_2", pstrTURNO_2,
                                              "pvchLINEA_2", pstrLINEA_2,
                                              "pvchPROCESO_2", pstrPROCESO_2,
                                              "pvchMATERIAL_2", pstrMATERIAL_2,
                                              "pvchFECHA_PRODUCCION_3", pstrFECHA_PRODUCCION_3,
                                              "pvchMAQUINA_3", pstrMAQUINA_3,
                                              "pvchSUPERVISOR_3", pstrSUPERVISOR_3,
                                              "pvchOPERARIO_3", pstrOPERARIO_3,
                                              "pvchTURNO_3", pstrTURNO_3,
                                              "pvchLINEA_3", pstrLINEA_3,
                                              "pvchPROCESO_3", pstrPROCESO_3,
                                              "pvchMATERIAL_3", pstrMATERIAL_3,
                                              "pvchCONTINUA_VALOR_1", pstrCONTINUA_VALOR_1,
                                              "pvchCONTINUA_OBS_1", pstrCONTINUA_OBS_1,
                                              "pvchTORSION_VALOR_1", pstrTORSION_VALOR_1,
                                              "pvchTORSION_OBS_1", pstrTORSION_OBS_1,
                                              "pvchESTIRAJE_VALOR_1", pstrESTIRAJE_VALOR_1,
                                              "pvchESTIRAJE_OBS_1", pstrESTIRAJE_OBS_1,
                                              "pvchVELOCIDAD_VALOR_1", pstrVELOCIDAD_VALOR_1,
                                              "pvchVELOCIDAD_OBS_1", pstrVELOCIDAD_OBS_1,
                                              "pvchCURSORES_VALOR_1", pstrCURSORES_VALOR_1,
                                              "pvchCURSORES_OBS_1", pstrCURSORES_OBS_1,
                                              "pvchCLIPS_VALOR_1", pstrCLIPS_VALOR_1,
                                              "pvchCLIPS_OBS_1", pstrCLIPS_OBS_1,
                                              "pvchHUMEDAD_RELATIVA_VALOR_1", pstrHUMEDAD_RELATIVA_VALOR_1,
                                              "pvchHUMEDAD_RELATIVA_OBS_1", pstrHUMEDAD_RELATIVA_OBS_1,
                                              "pvchTEMPERATURA_SALA_VALOR_1", pstrTEMPERATURA_SALA_VALOR_1,
                                              "pvchTEMPERATURA_SALA_OBS_1", pstrTEMPERATURA_SALA_OBS_1,
                                              "pvchOTROS_VALOR_1", pstrOTROS_VALOR_1,
                                              "pvchOTROS_OBS_1", pstrOTROS_OBS_1,
                                              "pvchOPEN_RIETER_11", pstrOPEN_RIETER_11,
                                              "pvchOPEN_RIETER_OBS_11", pstrOPEN_RIETER_OBS_11,
                                              "pvchTORSION_VALOR_11", pstrTORSION_VALOR_11,
                                              "pvchTORSION_OBS_11", pstrTORSION_OBS_11,
                                              "pvchTENSION_VALOR_11", pstrTENSION_VALOR_11,
                                              "pvchTENSION_OBS_11", pstrTENSION_OBS_11,
                                              "pvchESTIRAJE_VALOR_11", pstrESTIRAJE_VALOR_11,
                                              "pvchESTIRAJE_OBS_11", pstrESTIRAJE_OBS_11,
                                              "pvchVELOCIDAD_ROTOR_VALOR_11", pstrVELOCIDAD_ROTOR_VALOR_11,
                                              "pvchVELOCIDAD_ROTOR_OBS_11", pstrVELOCIDAD_ROTOR_OBS_11,
                                              "pvchVELOCIDAD_DISGREGADOR_VALOR_11", pstrVELOCIDAD_DISGREGADOR_VALOR_11,
                                              "pvchVELOCIDAD_DISGREGADOR_VALOR_OBS_11", pstrVELOCIDAD_DISGREGADOR_VALOR_OBS_11,
                                              "pvchHUMEDAD_RELATIVA_VALOR_11", pstrHUMEDAD_RELATIVA_VALOR_11,
                                              "pvchHUMEDAD_RELATIVA_OBS_11", pstrHUMEDAD_RELATIVA_OBS_11,
                                              "pvchTEMPERATURA_SALA_VALOR_11", pstrTEMPERATURA_SALA_VALOR_11,
                                              "pvchTEMPERATURA_SALA_OBS_11", pstrTEMPERATURA_SALA_OBS_11,
                                              "pvchOTROS_VALOR_11", pstrOTROS_VALOR_11,
                                              "pvchOTROS_OBS_11", pstrOTROS_OBS_11,
                                              "pvchCONERA_VALOR_2", pstrCONERA_VALOR_2,
                                              "pvchCONERA_OBS_2", pstrCONERA_OBS_2,
                                              "pvchTENSION_VALOR_2", pstrTENSION_VALOR_2,
                                              "pvchTENSION_OBS_2", pstrTENSION_OBS_2,
                                              "pvchLONGITUD_VALOR_2", pstrLONGITUD_VALOR_2,
                                              "pvchLONGITUD_OBS_2", pstrLONGITUD_OBS_2,
                                              "pvchVELOCIDAD_VALOR_2", pstrVELOCIDAD_VALOR_2,
                                              "pvchVELOCIDAD_OBS_2", pstrVELOCIDAD_OBS_2,
                                              "pvchPRESION_MARCO_PORTABOBINA_VALOR_2", pstrPRESION_MARCO_PORTABOBINA_VALOR_2,
                                              "pvchPRESION_MARCO_PORTABOBINA_OBS_2", pstrPRESION_MARCO_PORTABOBINA_OBS_2,
                                              "pvchHUMEDAD_RELATIVA_VALOR_2", pstrHUMEDAD_RELATIVA_VALOR_2,
                                              "pvchHUMEDAD_RELATIVA_OBS_2", pstrHUMEDAD_RELATIVA_OBS_2,
                                              "pvchTEMPERATURA_SALA_VALOR_2", pstrTEMPERATURA_SALA_VALOR_2,
                                              "pvchTEMPERATURA_SALA_OBS_2", pstrTEMPERATURA_SALA_OBS_2,
                                              "pvchOTROS_VALOR_2", pstrOTROS_VALOR_2,
                                              "pvchOTROS_OBS_2", pstrOTROS_OBS_2,
                                              "pvchRECUBRIDORA_VALOR_3", pstrRECUBRIDORA_VALOR_3,
                                              "pvchRECUBRIDORA_OBS_3", pstrRECUBRIDORA_OBS_3,
                                              "pvchVELOCIDAD_RECUBRIDORA_VALOR_3", pstrVELOCIDAD_RECUBRIDORA_VALOR_3,
                                              "pvchVELOCIDAD_RECUBRIDORA_OBS_3", pstrVELOCIDAD_RECUBRIDORA_OBS_3,
                                              "pvchRECETA_VALOR_3", pstrRECETA_VALOR_3,
                                              "pvchRECETA_OBS_3", pstrRECETA_OBS_3,
                                              "pvchOTROS_VALOR_3", pstrOTROS_VALOR_3,
                                              "pvchOTROS_OBS_3", pstrOTROS_OBS_3}

            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_HILANDERIA_INSERTAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_ActualizarHilanderia(ByVal pstrPRUEBA_NRO As String,
                                          ByVal pstrFECHA_PRODUCCION_1 As String,
                                          ByVal pstrMAQUINA_1 As String,
                                          ByVal pstrSUPERVISOR_1 As String,
                                          ByVal pstrOPERARIO_1 As String,
                                          ByVal pstrTURNO_1 As String,
                                          ByVal pstrLINEA_1 As String,
                                          ByVal pstrPROCESO_1 As String,
                                          ByVal pstrMATERIAL_1 As String,
                                          ByVal pstrFECHA_PRODUCCION_2 As String,
                                          ByVal pstrMAQUINA_2 As String,
                                          ByVal pstrSUPERVISOR_2 As String,
                                          ByVal pstrOPERARIO_2 As String,
                                          ByVal pstrTURNO_2 As String,
                                          ByVal pstrLINEA_2 As String,
                                          ByVal pstrPROCESO_2 As String,
                                          ByVal pstrMATERIAL_2 As String,
                                          ByVal pstrFECHA_PRODUCCION_3 As String,
                                          ByVal pstrMAQUINA_3 As String,
                                          ByVal pstrSUPERVISOR_3 As String,
                                          ByVal pstrOPERARIO_3 As String,
                                          ByVal pstrTURNO_3 As String,
                                          ByVal pstrLINEA_3 As String,
                                          ByVal pstrPROCESO_3 As String,
                                          ByVal pstrMATERIAL_3 As String,
                                          ByVal pstrCONTINUA_VALOR_1 As String,
                                          ByVal pstrCONTINUA_OBS_1 As String,
                                          ByVal pstrTORSION_VALOR_1 As String,
                                          ByVal pstrTORSION_OBS_1 As String,
                                          ByVal pstrESTIRAJE_VALOR_1 As String,
                                          ByVal pstrESTIRAJE_OBS_1 As String,
                                          ByVal pstrVELOCIDAD_VALOR_1 As String,
                                          ByVal pstrVELOCIDAD_OBS_1 As String,
                                          ByVal pstrCURSORES_VALOR_1 As String,
                                          ByVal pstrCURSORES_OBS_1 As String,
                                          ByVal pstrCLIPS_VALOR_1 As String,
                                          ByVal pstrCLIPS_OBS_1 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_VALOR_1 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_OBS_1 As String,
                                          ByVal pstrTEMPERATURA_SALA_VALOR_1 As String,
                                          ByVal pstrTEMPERATURA_SALA_OBS_1 As String,
                                          ByVal pstrOTROS_VALOR_1 As String,
                                          ByVal pstrOTROS_OBS_1 As String,
                                          ByVal pstrOPEN_RIETER_11 As String,
                                          ByVal pstrOPEN_RIETER_OBS_11 As String,
                                          ByVal pstrTORSION_VALOR_11 As String,
                                          ByVal pstrTORSION_OBS_11 As String,
                                          ByVal pstrTENSION_VALOR_11 As String,
                                          ByVal pstrTENSION_OBS_11 As String,
                                          ByVal pstrESTIRAJE_VALOR_11 As String,
                                          ByVal pstrESTIRAJE_OBS_11 As String,
                                          ByVal pstrVELOCIDAD_ROTOR_VALOR_11 As String,
                                          ByVal pstrVELOCIDAD_ROTOR_OBS_11 As String,
                                          ByVal pstrVELOCIDAD_DISGREGADOR_VALOR_11 As String,
                                          ByVal pstrVELOCIDAD_DISGREGADOR_VALOR_OBS_11 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_VALOR_11 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_OBS_11 As String,
                                          ByVal pstrTEMPERATURA_SALA_VALOR_11 As String,
                                          ByVal pstrTEMPERATURA_SALA_OBS_11 As String,
                                          ByVal pstrOTROS_VALOR_11 As String,
                                          ByVal pstrOTROS_OBS_11 As String,
                                          ByVal pstrCONERA_VALOR_2 As String,
                                          ByVal pstrCONERA_OBS_2 As String,
                                          ByVal pstrTENSION_VALOR_2 As String,
                                          ByVal pstrTENSION_OBS_2 As String,
                                          ByVal pstrLONGITUD_VALOR_2 As String,
                                          ByVal pstrLONGITUD_OBS_2 As String,
                                          ByVal pstrVELOCIDAD_VALOR_2 As String,
                                          ByVal pstrVELOCIDAD_OBS_2 As String,
                                          ByVal pstrPRESION_MARCO_PORTABOBINA_VALOR_2 As String,
                                          ByVal pstrPRESION_MARCO_PORTABOBINA_OBS_2 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_VALOR_2 As String,
                                          ByVal pstrHUMEDAD_RELATIVA_OBS_2 As String,
                                          ByVal pstrTEMPERATURA_SALA_VALOR_2 As String,
                                          ByVal pstrTEMPERATURA_SALA_OBS_2 As String,
                                          ByVal pstrOTROS_VALOR_2 As String,
                                          ByVal pstrOTROS_OBS_2 As String,
                                          ByVal pstrRECUBRIDORA_VALOR_3 As String,
                                          ByVal pstrRECUBRIDORA_OBS_3 As String,
                                          ByVal pstrVELOCIDAD_RECUBRIDORA_VALOR_3 As String,
                                          ByVal pstrVELOCIDAD_RECUBRIDORA_OBS_3 As String,
                                          ByVal pstrRECETA_VALOR_3 As String,
                                          ByVal pstrRECETA_OBS_3 As String,
                                          ByVal pstrOTROS_VALOR_3 As String,
                                          ByVal pstrOTROS_OBS_3 As String
                                          ) As Integer
        Try


            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_PRODUCCION_2 = "" Then
                pstrFECHA_PRODUCCION_2 = Nothing
            Else
                pstrFECHA_PRODUCCION_2 = Convert.ToDateTime(pstrFECHA_PRODUCCION_2).ToString("yyyyMMdd")
            End If
            If pstrFECHA_PRODUCCION_3 = "" Then
                pstrFECHA_PRODUCCION_3 = Nothing
            Else
                pstrFECHA_PRODUCCION_3 = Convert.ToDateTime(pstrFECHA_PRODUCCION_3).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                              "pdatFECHA_PRODUCCION_1", pstrFECHA_PRODUCCION_1,
                                              "pvchMAQUINA_1", pstrMAQUINA_1,
                                              "pvchSUPERVISOR_1", pstrSUPERVISOR_1,
                                              "pvchOPERARIO_1", pstrOPERARIO_1,
                                              "pvchTURNO_1", pstrTURNO_1,
                                              "pvchLINEA_1", pstrLINEA_1,
                                              "pvchPROCESO_1", pstrPROCESO_1,
                                              "pvchMATERIAL_1", pstrMATERIAL_1,
                                              "pdatFECHA_PRODUCCION_2", pstrFECHA_PRODUCCION_2,
                                              "pvchMAQUINA_2", pstrMAQUINA_2,
                                              "pvchSUPERVISOR_2", pstrSUPERVISOR_2,
                                              "pvchOPERARIO_2", pstrOPERARIO_2,
                                              "pvchTURNO_2", pstrTURNO_2,
                                              "pvchLINEA_2", pstrLINEA_2,
                                              "pvchPROCESO_2", pstrPROCESO_2,
                                              "pvchMATERIAL_2", pstrMATERIAL_2,
                                              "pdatFECHA_PRODUCCION_3", pstrFECHA_PRODUCCION_3,
                                              "pvchMAQUINA_3", pstrMAQUINA_3,
                                              "pvchSUPERVISOR_3", pstrSUPERVISOR_3,
                                              "pvchOPERARIO_3", pstrOPERARIO_3,
                                              "pvchTURNO_3", pstrTURNO_3,
                                              "pvchLINEA_3", pstrLINEA_3,
                                              "pvchPROCESO_3", pstrPROCESO_3,
                                              "pvchMATERIAL_3", pstrMATERIAL_3,
                                              "pvchCONTINUA_VALOR_1", pstrCONTINUA_VALOR_1,
                                              "pvchCONTINUA_OBS_1", pstrCONTINUA_OBS_1,
                                              "pvchTORSION_VALOR_1", pstrTORSION_VALOR_1,
                                              "pvchTORSION_OBS_1", pstrTORSION_OBS_1,
                                              "pvchESTIRAJE_VALOR_1", pstrESTIRAJE_VALOR_1,
                                              "pvchESTIRAJE_OBS_1", pstrESTIRAJE_OBS_1,
                                              "pvchVELOCIDAD_VALOR_1", pstrVELOCIDAD_VALOR_1,
                                              "pvchVELOCIDAD_OBS_1", pstrVELOCIDAD_OBS_1,
                                              "pvchCURSORES_VALOR_1", pstrCURSORES_VALOR_1,
                                              "pvchCURSORES_OBS_1", pstrCURSORES_OBS_1,
                                              "pvchCLIPS_VALOR_1", pstrCLIPS_VALOR_1,
                                              "pvchCLIPS_OBS_1", pstrCLIPS_OBS_1,
                                              "pvchHUMEDAD_RELATIVA_VALOR_1", pstrHUMEDAD_RELATIVA_VALOR_1,
                                              "pvchHUMEDAD_RELATIVA_OBS_1", pstrHUMEDAD_RELATIVA_OBS_1,
                                              "pvchTEMPERATURA_SALA_VALOR_1", pstrTEMPERATURA_SALA_VALOR_1,
                                              "pvchTEMPERATURA_SALA_OBS_1", pstrTEMPERATURA_SALA_OBS_1,
                                              "pvchOTROS_VALOR_1", pstrOTROS_VALOR_1,
                                              "pvchOTROS_OBS_1", pstrOTROS_OBS_1,
                                              "pvchOPEN_RIETER_11", pstrOPEN_RIETER_11,
                                              "pvchOPEN_RIETER_OBS_11", pstrOPEN_RIETER_OBS_11,
                                              "pvchTORSION_VALOR_11", pstrTORSION_VALOR_11,
                                              "pvchTORSION_OBS_11", pstrTORSION_OBS_11,
                                              "pvchTENSION_VALOR_11", pstrTENSION_VALOR_11,
                                              "pvchTENSION_OBS_11", pstrTENSION_OBS_11,
                                              "pvchESTIRAJE_VALOR_11", pstrESTIRAJE_VALOR_11,
                                              "pvchESTIRAJE_OBS_11", pstrESTIRAJE_OBS_11,
                                              "pvchVELOCIDAD_ROTOR_VALOR_11", pstrVELOCIDAD_ROTOR_VALOR_11,
                                              "pvchVELOCIDAD_ROTOR_OBS_11", pstrVELOCIDAD_ROTOR_OBS_11,
                                              "pvchVELOCIDAD_DISGREGADOR_VALOR_11", pstrVELOCIDAD_DISGREGADOR_VALOR_11,
                                              "pvchVELOCIDAD_DISGREGADOR_VALOR_OBS_11", pstrVELOCIDAD_DISGREGADOR_VALOR_OBS_11,
                                              "pvchHUMEDAD_RELATIVA_VALOR_11", pstrHUMEDAD_RELATIVA_VALOR_11,
                                              "pvchHUMEDAD_RELATIVA_OBS_11", pstrHUMEDAD_RELATIVA_OBS_11,
                                              "pvchTEMPERATURA_SALA_VALOR_11", pstrTEMPERATURA_SALA_VALOR_11,
                                              "pvchTEMPERATURA_SALA_OBS_11", pstrTEMPERATURA_SALA_OBS_11,
                                              "pvchOTROS_VALOR_11", pstrOTROS_VALOR_11,
                                              "pvchOTROS_OBS_11", pstrOTROS_OBS_11,
                                              "pvchCONERA_VALOR_2", pstrCONERA_VALOR_2,
                                              "pvchCONERA_OBS_2", pstrCONERA_OBS_2,
                                              "pvchTENSION_VALOR_2", pstrTENSION_VALOR_2,
                                              "pvchTENSION_OBS_2", pstrTENSION_OBS_2,
                                              "pvchLONGITUD_VALOR_2", pstrLONGITUD_VALOR_2,
                                              "pvchLONGITUD_OBS_2", pstrLONGITUD_OBS_2,
                                              "pvchVELOCIDAD_VALOR_2", pstrVELOCIDAD_VALOR_2,
                                              "pvchVELOCIDAD_OBS_2", pstrVELOCIDAD_OBS_2,
                                              "pvchPRESION_MARCO_PORTABOBINA_VALOR_2", pstrPRESION_MARCO_PORTABOBINA_VALOR_2,
                                              "pvchPRESION_MARCO_PORTABOBINA_OBS_2", pstrPRESION_MARCO_PORTABOBINA_OBS_2,
                                              "pvchHUMEDAD_RELATIVA_VALOR_2", pstrHUMEDAD_RELATIVA_VALOR_2,
                                              "pvchHUMEDAD_RELATIVA_OBS_2", pstrHUMEDAD_RELATIVA_OBS_2,
                                              "pvchTEMPERATURA_SALA_VALOR_2", pstrTEMPERATURA_SALA_VALOR_2,
                                              "pvchTEMPERATURA_SALA_OBS_2", pstrTEMPERATURA_SALA_OBS_2,
                                              "pvchOTROS_VALOR_2", pstrOTROS_VALOR_2,
                                              "pvchOTROS_OBS_2", pstrOTROS_OBS_2,
                                              "pvchRECUBRIDORA_VALOR_3", pstrRECUBRIDORA_VALOR_3,
                                              "pvchRECUBRIDORA_OBS_3", pstrRECUBRIDORA_OBS_3,
                                              "pvchVELOCIDAD_RECUBRIDORA_VALOR_3", pstrVELOCIDAD_RECUBRIDORA_VALOR_3,
                                              "pvchVELOCIDAD_RECUBRIDORA_OBS_3", pstrVELOCIDAD_RECUBRIDORA_OBS_3,
                                              "pvchRECETA_VALOR_3", pstrRECETA_VALOR_3,
                                              "pvchRECETA_OBS_3", pstrRECETA_OBS_3,
                                              "pvchOTROS_VALOR_3", pstrOTROS_VALOR_3,
                                              "pvchOTROS_OBS_3", pstrOTROS_OBS_3}

            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_HILANDERIA_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ObtenerHilanderia(ByVal pstrPRUEBA_NRO As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_HILANDERIA_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fn_InsertarLaboratorioFisico(ByVal pstrPRUEBA_NRO As String,
                                                 ByVal pdatFECHA_EVALUACION_1 As String,
                                                 ByVal pstrLABORATORISTA_1 As String,
                                                 ByVal pstrTURNO_1 As String,
                                                 ByVal pstrARTICULO_1 As String,
                                                 ByVal pstrFICHA_1 As String,
                                                 ByVal pstrPROCESO_1 As String,
                                                 ByVal pstrTIPO_ACABADO_1 As String,
                                                 ByVal pstrMETROS_1 As String,
                                                 ByVal pstrANCHO_ACABADO_1 As String,
                                                 ByVal pstrANCHO_ACABADO_OBS_1 As String,
                                                 ByVal pstrENCOG_URDIMBRE_1 As String,
                                                 ByVal pstrENCOG_URDIMBRE_OBS_1 As String,
                                                 ByVal pstrENCOG_TRAMA_1 As String,
                                                 ByVal pstrENCOG_TRAMA_OBS_1 As String,
                                                 ByVal pstrELONGACION_1 As String,
                                                 ByVal pstrELONGACION_OBS_1 As String,
                                                 ByVal pstrREVIRADO_DERECHO_1 As String,
                                                 ByVal pstrREVIRADO_DERECHO_OBS_1 As String,
                                                 ByVal pstrREVIRADO_CENTRO_1 As String,
                                                 ByVal pstrREVIRADO_CENTRO_OBS_1 As String,
                                                 ByVal pstrREVIRADO_IZQUIERDO_1 As String,
                                                 ByVal pstrREVIRADO_IZQUIERDO_OBS_1 As String,
                                                 ByVal pstrRESISTENCIA_URDIMBRE_1 As String,
                                                 ByVal pstrRESISTENCIA_URDIMBRE_OBS_1 As String,
                                                 ByVal pstrRESISTENCIA_TRAMA_1 As String,
                                                 ByVal pstrRESISTENCIA_TRAMA_OBS_1 As String,
                                                 ByVal pstrPESO_1 As String,
                                                 ByVal pstrPESO_OBS_1 As String,
                                                 ByVal pstrOTROS_1 As String,
                                                 ByVal pstrOTROS_OBS_1 As String,
                                                 ByVal pstrRETIRO_LYCRA_11 As String,
                                                 ByVal pstrETIRO_LYCRA_OBS_11 As String,
                                                 ByVal pstrPRUEBA_RETORNO_11 As String,
                                                 ByVal pstrPRUEBA_RETORNO_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_HUMEDO_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_HUMEDO_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_SECO_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_SECO_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11 As String,
                                                 ByVal pstrOTROS_11 As String,
                                                 ByVal pstrOTROS_OBS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11 As String
                                                 ) As Integer
        Try
            If pdatFECHA_EVALUACION_1 = "" Then
                pdatFECHA_EVALUACION_1 = Nothing
            Else
                pdatFECHA_EVALUACION_1 = Convert.ToDateTime(pdatFECHA_EVALUACION_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                            "pdatFECHA_EVALUACION_1", pdatFECHA_EVALUACION_1,
                                            "pvchLABORATORISTA_1", pstrLABORATORISTA_1,
                                            "pvchTURNO_1", pstrTURNO_1,
                                            "pvchARTICULO_1", pstrARTICULO_1,
                                            "pvchFICHA_1", pstrFICHA_1,
                                            "pvchPROCESO_1", pstrPROCESO_1,
                                            "pvchTIPO_ACABADO_1", pstrTIPO_ACABADO_1,
                                            "pvchMETROS_1", pstrMETROS_1,
                                            "pvchANCHO_ACABADO_1", pstrANCHO_ACABADO_1,
                                            "pvchANCHO_ACABADO_OBS_1", pstrANCHO_ACABADO_OBS_1,
                                            "pvchENCOG_URDIMBRE_1", pstrENCOG_URDIMBRE_1,
                                            "pvchENCOG_URDIMBRE_OBS_1", pstrENCOG_URDIMBRE_OBS_1,
                                            "pvchENCOG_TRAMA_1", pstrENCOG_TRAMA_1,
                                            "pvchENCOG_TRAMA_OBS_1", pstrENCOG_TRAMA_OBS_1,
                                            "pvchELONGACION_1", pstrELONGACION_1,
                                            "pvchELONGACION_OBS_1", pstrELONGACION_OBS_1,
                                            "pvchREVIRADO_DERECHO_1", pstrREVIRADO_DERECHO_1,
                                            "pvchREVIRADO_DERECHO_OBS_1", pstrREVIRADO_DERECHO_OBS_1,
                                            "pvchREVIRADO_CENTRO_1", pstrREVIRADO_CENTRO_1,
                                            "pvchREVIRADO_CENTRO_OBS_1", pstrREVIRADO_CENTRO_OBS_1,
                                            "pvchREVIRADO_IZQUIERDO_1", pstrREVIRADO_IZQUIERDO_1,
                                            "pvchREVIRADO_IZQUIERDO_OBS_1", pstrREVIRADO_IZQUIERDO_OBS_1,
                                            "pvchRESISTENCIA_URDIMBRE_1", pstrRESISTENCIA_URDIMBRE_1,
                                            "pvchRESISTENCIA_URDIMBRE_OBS_1", pstrRESISTENCIA_URDIMBRE_OBS_1,
                                            "pvchRESISTENCIA_TRAMA_1", pstrRESISTENCIA_TRAMA_1,
                                            "pvchRESISTENCIA_TRAMA_OBS_1", pstrRESISTENCIA_TRAMA_OBS_1,
                                            "pvchPESO_1", pstrPESO_1,
                                            "pvchPESO_OBS_1", pstrPESO_OBS_1,
                                            "pvchOTROS_1", pstrOTROS_1,
                                            "pvchOTROS_OBS_1", pstrOTROS_OBS_1,
                                            "pvchRETIRO_LYCRA_11", pstrRETIRO_LYCRA_11,
                                            "pvchETIRO_LYCRA_OBS_11", pstrETIRO_LYCRA_OBS_11,
                                            "pvchPRUEBA_RETORNO_11", pstrPRUEBA_RETORNO_11,
                                            "pvchPRUEBA_RETORNO_OBS_11", pstrPRUEBA_RETORNO_OBS_11,
                                            "pvchSOLIDEZ_FROTE_HUMEDO_11", pstrSOLIDEZ_FROTE_HUMEDO_11,
                                            "pvchSOLIDEZ_FROTE_HUMEDO_OBS_11", pstrSOLIDEZ_FROTE_HUMEDO_OBS_11,
                                            "pvchSOLIDEZ_FROTE_SECO_11", pstrSOLIDEZ_FROTE_SECO_11,
                                            "pvchSOLIDEZ_FROTE_SECO_OBS_11", pstrSOLIDEZ_FROTE_SECO_OBS_11,
                                            "pvchSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11", pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11,
                                            "pvchSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11", pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11,
                                            "pvchSOLIDEZ_LAVADO_CAMBIO_COLOR_11", pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_11,
                                            "pvchSOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11", pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11", pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11", pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11,
                                            "pvchOTROS_11", pstrOTROS_11,
                                            "pvchOTROS_OBS_11", pstrOTROS_OBS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_POR_PULGADAS_11", pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11", pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_LABORATORIO_FISICO_INSERTAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ActualizarLaboratorioFisico(ByVal pstrPRUEBA_NRO As String,
                                                 ByVal pdatFECHA_EVALUACION_1 As String,
                                                 ByVal pstrLABORATORISTA_1 As String,
                                                 ByVal pstrTURNO_1 As String,
                                                 ByVal pstrARTICULO_1 As String,
                                                 ByVal pstrFICHA_1 As String,
                                                 ByVal pstrPROCESO_1 As String,
                                                 ByVal pstrTIPO_ACABADO_1 As String,
                                                 ByVal pstrMETROS_1 As String,
                                                 ByVal pstrANCHO_ACABADO_1 As String,
                                                 ByVal pstrANCHO_ACABADO_OBS_1 As String,
                                                 ByVal pstrENCOG_URDIMBRE_1 As String,
                                                 ByVal pstrENCOG_URDIMBRE_OBS_1 As String,
                                                 ByVal pstrENCOG_TRAMA_1 As String,
                                                 ByVal pstrENCOG_TRAMA_OBS_1 As String,
                                                 ByVal pstrELONGACION_1 As String,
                                                 ByVal pstrELONGACION_OBS_1 As String,
                                                 ByVal pstrREVIRADO_DERECHO_1 As String,
                                                 ByVal pstrREVIRADO_DERECHO_OBS_1 As String,
                                                 ByVal pstrREVIRADO_CENTRO_1 As String,
                                                 ByVal pstrREVIRADO_CENTRO_OBS_1 As String,
                                                 ByVal pstrREVIRADO_IZQUIERDO_1 As String,
                                                 ByVal pstrREVIRADO_IZQUIERDO_OBS_1 As String,
                                                 ByVal pstrRESISTENCIA_URDIMBRE_1 As String,
                                                 ByVal pstrRESISTENCIA_URDIMBRE_OBS_1 As String,
                                                 ByVal pstrRESISTENCIA_TRAMA_1 As String,
                                                 ByVal pstrRESISTENCIA_TRAMA_OBS_1 As String,
                                                 ByVal pstrPESO_1 As String,
                                                 ByVal pstrPESO_OBS_1 As String,
                                                 ByVal pstrOTROS_1 As String,
                                                 ByVal pstrOTROS_OBS_1 As String,
                                                 ByVal pstrRETIRO_LYCRA_11 As String,
                                                 ByVal pstrETIRO_LYCRA_OBS_11 As String,
                                                 ByVal pstrPRUEBA_RETORNO_11 As String,
                                                 ByVal pstrPRUEBA_RETORNO_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_HUMEDO_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_HUMEDO_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_SECO_11 As String,
                                                 ByVal pstrSOLIDEZ_FROTE_SECO_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_11 As String,
                                                 ByVal pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11 As String,
                                                 ByVal pstrOTROS_11 As String,
                                                 ByVal pstrOTROS_OBS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_11 As String,
                                                 ByVal pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11 As String
                                                 ) As Integer
        Try
            If pdatFECHA_EVALUACION_1 = "" Then
                pdatFECHA_EVALUACION_1 = Nothing
            Else
                pdatFECHA_EVALUACION_1 = Convert.ToDateTime(pdatFECHA_EVALUACION_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                            "pdatFECHA_EVALUACION_1", pdatFECHA_EVALUACION_1,
                                            "pvchLABORATORISTA_1", pstrLABORATORISTA_1,
                                            "pvchTURNO_1", pstrTURNO_1,
                                            "pvchARTICULO_1", pstrARTICULO_1,
                                            "pvchFICHA_1", pstrFICHA_1,
                                            "pvchPROCESO_1", pstrPROCESO_1,
                                            "pvchTIPO_ACABADO_1", pstrTIPO_ACABADO_1,
                                            "pvchMETROS_1", pstrMETROS_1,
                                            "pvchANCHO_ACABADO_1", pstrANCHO_ACABADO_1,
                                            "pvchANCHO_ACABADO_OBS_1", pstrANCHO_ACABADO_OBS_1,
                                            "pvchENCOG_URDIMBRE_1", pstrENCOG_URDIMBRE_1,
                                            "pvchENCOG_URDIMBRE_OBS_1", pstrENCOG_URDIMBRE_OBS_1,
                                            "pvchENCOG_TRAMA_1", pstrENCOG_TRAMA_1,
                                            "pvchENCOG_TRAMA_OBS_1", pstrENCOG_TRAMA_OBS_1,
                                            "pvchELONGACION_1", pstrELONGACION_1,
                                            "pvchELONGACION_OBS_1", pstrELONGACION_OBS_1,
                                            "pvchREVIRADO_DERECHO_1", pstrREVIRADO_DERECHO_1,
                                            "pvchREVIRADO_DERECHO_OBS_1", pstrREVIRADO_DERECHO_OBS_1,
                                            "pvchREVIRADO_CENTRO_1", pstrREVIRADO_CENTRO_1,
                                            "pvchREVIRADO_CENTRO_OBS_1", pstrREVIRADO_CENTRO_OBS_1,
                                            "pvchREVIRADO_IZQUIERDO_1", pstrREVIRADO_IZQUIERDO_1,
                                            "pvchREVIRADO_IZQUIERDO_OBS_1", pstrREVIRADO_IZQUIERDO_OBS_1,
                                            "pvchRESISTENCIA_URDIMBRE_1", pstrRESISTENCIA_URDIMBRE_1,
                                            "pvchRESISTENCIA_URDIMBRE_OBS_1", pstrRESISTENCIA_URDIMBRE_OBS_1,
                                            "pvchRESISTENCIA_TRAMA_1", pstrRESISTENCIA_TRAMA_1,
                                            "pvchRESISTENCIA_TRAMA_OBS_1", pstrRESISTENCIA_TRAMA_OBS_1,
                                            "pvchPESO_1", pstrPESO_1,
                                            "pvchPESO_OBS_1", pstrPESO_OBS_1,
                                            "pvchOTROS_1", pstrOTROS_1,
                                            "pvchOTROS_OBS_1", pstrOTROS_OBS_1,
                                            "pvchRETIRO_LYCRA_11", pstrRETIRO_LYCRA_11,
                                            "pvchETIRO_LYCRA_OBS_11", pstrETIRO_LYCRA_OBS_11,
                                            "pvchPRUEBA_RETORNO_11", pstrPRUEBA_RETORNO_11,
                                            "pvchPRUEBA_RETORNO_OBS_11", pstrPRUEBA_RETORNO_OBS_11,
                                            "pvchSOLIDEZ_FROTE_HUMEDO_11", pstrSOLIDEZ_FROTE_HUMEDO_11,
                                            "pvchSOLIDEZ_FROTE_HUMEDO_OBS_11", pstrSOLIDEZ_FROTE_HUMEDO_OBS_11,
                                            "pvchSOLIDEZ_FROTE_SECO_11", pstrSOLIDEZ_FROTE_SECO_11,
                                            "pvchSOLIDEZ_FROTE_SECO_OBS_11", pstrSOLIDEZ_FROTE_SECO_OBS_11,
                                            "pvchSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11", pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_11,
                                            "pvchSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11", pstrSOLIDEZ_LAVADO_TRANSFERENCIA_COLOR_OBS_11,
                                            "pvchSOLIDEZ_LAVADO_CAMBIO_COLOR_11", pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_11,
                                            "pvchSOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11", pstrSOLIDEZ_LAVADO_CAMBIO_COLOR_OBS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11", pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11", pstrDENSIDAD_TEJIDO_HILO_PASADAS_POR_PULGADAS_OBS_11,
                                            "pvchOTROS_11", pstrOTROS_11,
                                            "pvchOTROS_OBS_11", pstrOTROS_OBS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_POR_PULGADAS_11", pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_11,
                                            "pvchDENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11", pstrDENSIDAD_TEJIDO_HILO_POR_PULGADAS_OBS_11
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_LABORATORIO_FISICO_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ObtenerLaboratorioFisico(ByVal pstrPRUEBA_NRO As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_LABORATORIO_FISICO_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_InsertarLaboratorioHIlanderia(ByVal pstrPRUEBA_NRO As String,
                                                     ByVal pstrFECHA_PRODUCCION_1 As String,
                                                     ByVal pstrLABORATORISTA_1 As String,
                                                     ByVal pstrTURNO_1 As String,
                                                     ByVal pstrLINEA_1 As String,
                                                     ByVal pstrPROCESO_1 As String,
                                                     ByVal pstrMAQUINA_1 As String,
                                                     ByVal pstrMATERIAL_1 As String,
                                                     ByVal pstrFINURA_FIBRA_1 As String,
                                                     ByVal pstrRESISTENCIA_FIBRA_1 As String,
                                                     ByVal pstrLONGITUD_FIBRA_1 As String,
                                                     ByVal pstrUNIFORMIDAD_FIBRA_1 As String,
                                                     ByVal pstrTITULO_CINTA_HILO_1 As String,
                                                     ByVal pstrCv_TÍTULO_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_1 As String,
                                                     ByVal pstrRESISTENCIA_HILO_RKM_1 As String,
                                                     ByVal pstrRESISTENCIA_MINIMA_HILO_RKM_1 As String,
                                                     ByVal pstrCv_RESISTENCIA_HILO_1 As String,
                                                     ByVal pstrELONGACION_HILO_1 As String,
                                                     ByVal pstrTORSION_HILO_1 As String,
                                                     ByVal pstrROTURAS_1000_HUSOS_HORA_1 As String,
                                                     ByVal pstrRESISTENCIA_EMPALME_1 As String,
                                                     ByVal pstrPESO_CONO_1 As String,
                                                     ByVal pstrDUREZA_CONO_1 As String,
                                                     ByVal pstrOTROS_1 As String,
                                                     ByVal pstrFINURA_FIBRA_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_FIBRA_OBS_1 As String,
                                                     ByVal pstrLONGITUD_FIBRA_OBS_1 As String,
                                                     ByVal pstrUNIFORMIDAD_FIBRA_OBS_1 As String,
                                                     ByVal pstrTITULO_CINTA_HILO_OBS_1 As String,
                                                     ByVal pstrCv_TÍTULO_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_HILO_RKM_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_MINIMA_HILO_RKM_OBS_1 As String,
                                                     ByVal pstrCv_RESISTENCIA_HILO_OBS_1 As String,
                                                     ByVal pstrELONGACION_HILO_OBS_1 As String,
                                                     ByVal pstrTORSION_HILO_OBS_1 As String,
                                                     ByVal pstrROTURAS_OBS_1000_HUSOS_HORA_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_EMPALME_OBS_1 As String,
                                                     ByVal pstrPESO_CONO_OBS_1 As String,
                                                     ByVal pstrDUREZA_CONO_OBS_1 As String,
                                                     ByVal pstrOTROS_OBS_1 As String
                                                     ) As Integer
        Try
            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                            "pvchFECHA_PRODUCCION_1", pstrFECHA_PRODUCCION_1,
                                            "pvchLABORATORISTA_1", pstrLABORATORISTA_1,
                                            "pvchTURNO_1", pstrTURNO_1,
                                            "pvchLINEA_1", pstrLINEA_1,
                                            "pvchPROCESO_1", pstrPROCESO_1,
                                            "pvchMAQUINA_1", pstrMAQUINA_1,
                                            "pvchMATERIAL_1", pstrMATERIAL_1,
                                            "pvchFINURA_FIBRA_1", pstrFINURA_FIBRA_1,
                                            "pvchRESISTENCIA_FIBRA_1", pstrRESISTENCIA_FIBRA_1,
                                            "pvchLONGITUD_FIBRA_1", pstrLONGITUD_FIBRA_1,
                                            "pvchUNIFORMIDAD_FIBRA_1", pstrUNIFORMIDAD_FIBRA_1,
                                            "pvchTITULO_CINTA_HILO_1", pstrTITULO_CINTA_HILO_1,
                                            "pvchCv_TÍTULO_1", pstrCv_TÍTULO_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_H_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_1,
                                            "pvchRESISTENCIA_HILO_RKM_1", pstrRESISTENCIA_HILO_RKM_1,
                                            "pvchRESISTENCIA_MINIMA_HILO_RKM_1", pstrRESISTENCIA_MINIMA_HILO_RKM_1,
                                            "pvchCv_RESISTENCIA_HILO_1", pstrCv_RESISTENCIA_HILO_1,
                                            "pvchELONGACION_HILO_1", pstrELONGACION_HILO_1,
                                            "pvchTORSION_HILO_1", pstrTORSION_HILO_1,
                                            "pvchROTURAS_1000_HUSOS_HORA_1", pstrROTURAS_1000_HUSOS_HORA_1,
                                            "pvchRESISTENCIA_EMPALME_1", pstrRESISTENCIA_EMPALME_1,
                                            "pvchPESO_CONO_1", pstrPESO_CONO_1,
                                            "pvchDUREZA_CONO_1", pstrDUREZA_CONO_1,
                                            "pvchOTROS_1", pstrOTROS_1,
                                            "pvchFINURA_FIBRA_OBS_1", pstrFINURA_FIBRA_OBS_1,
                                            "pvchRESISTENCIA_FIBRA_OBS_1", pstrRESISTENCIA_FIBRA_OBS_1,
                                            "pvchLONGITUD_FIBRA_OBS_1", pstrLONGITUD_FIBRA_OBS_1,
                                            "pvchUNIFORMIDAD_FIBRA_OBS_1", pstrUNIFORMIDAD_FIBRA_OBS_1,
                                            "pvchTITULO_CINTA_HILO_OBS_1", pstrTITULO_CINTA_HILO_OBS_1,
                                            "pvchCv_TÍTULO_OBS_1", pstrCv_TÍTULO_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1,
                                            "pvchRESISTENCIA_HILO_RKM_OBS_1", pstrRESISTENCIA_HILO_RKM_OBS_1,
                                            "pvchRESISTENCIA_MINIMA_HILO_RKM_OBS_1", pstrRESISTENCIA_MINIMA_HILO_RKM_OBS_1,
                                            "pvchCv_RESISTENCIA_HILO_OBS_1", pstrCv_RESISTENCIA_HILO_OBS_1,
                                            "pvchELONGACION_HILO_OBS_1", pstrELONGACION_HILO_OBS_1,
                                            "pvchTORSION_HILO_OBS_1", pstrTORSION_HILO_OBS_1,
                                            "pvchROTURAS_OBS_1000_HUSOS_HORA_OBS_1", pstrROTURAS_OBS_1000_HUSOS_HORA_OBS_1,
                                            "pvchRESISTENCIA_EMPALME_OBS_1", pstrRESISTENCIA_EMPALME_OBS_1,
                                            "pvchPESO_CONO_OBS_1", pstrPESO_CONO_OBS_1,
                                            "pvchDUREZA_CONO_OBS_1", pstrDUREZA_CONO_OBS_1,
                                            "pvchOTROS_OBS_1", pstrOTROS_OBS_1
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_LABORATORIO_HILANDERIA_INSERTAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ActualizarLaboratorioHIlanderia(ByVal pstrPRUEBA_NRO As String,
                                                     ByVal pstrFECHA_PRODUCCION_1 As String,
                                                     ByVal pstrLABORATORISTA_1 As String,
                                                     ByVal pstrMAQUINA_1 As String,
                                                     ByVal pstrTURNO_1 As String,
                                                     ByVal pstrLINEA_1 As String,
                                                     ByVal pstrPROCESO_1 As String,
                                                     ByVal pstrMATERIAL_1 As String,
                                                     ByVal pstrFINURA_FIBRA_1 As String,
                                                     ByVal pstrRESISTENCIA_FIBRA_1 As String,
                                                     ByVal pstrLONGITUD_FIBRA_1 As String,
                                                     ByVal pstrUNIFORMIDAD_FIBRA_1 As String,
                                                     ByVal pstrTITULO_CINTA_HILO_1 As String,
                                                     ByVal pstrCv_TÍTULO_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_1 As String,
                                                     ByVal pstrRESISTENCIA_HILO_RKM_1 As String,
                                                     ByVal pstrRESISTENCIA_MINIMA_HILO_RKM_1 As String,
                                                     ByVal pstrCv_RESISTENCIA_HILO_1 As String,
                                                     ByVal pstrELONGACION_HILO_1 As String,
                                                     ByVal pstrTORSION_HILO_1 As String,
                                                     ByVal pstrROTURAS_1000_HUSOS_HORA_1 As String,
                                                     ByVal pstrRESISTENCIA_EMPALME_1 As String,
                                                     ByVal pstrPESO_CONO_1 As String,
                                                     ByVal pstrDUREZA_CONO_1 As String,
                                                     ByVal pstrOTROS_1 As String,
                                                     ByVal pstrFINURA_FIBRA_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_FIBRA_OBS_1 As String,
                                                     ByVal pstrLONGITUD_FIBRA_OBS_1 As String,
                                                     ByVal pstrUNIFORMIDAD_FIBRA_OBS_1 As String,
                                                     ByVal pstrTITULO_CINTA_HILO_OBS_1 As String,
                                                     ByVal pstrCv_TÍTULO_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1 As String,
                                                     ByVal pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_HILO_RKM_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_MINIMA_HILO_RKM_OBS_1 As String,
                                                     ByVal pstrCv_RESISTENCIA_HILO_OBS_1 As String,
                                                     ByVal pstrELONGACION_HILO_OBS_1 As String,
                                                     ByVal pstrTORSION_HILO_OBS_1 As String,
                                                     ByVal pstrROTURAS_OBS_1000_HUSOS_HORA_OBS_1 As String,
                                                     ByVal pstrRESISTENCIA_EMPALME_OBS_1 As String,
                                                     ByVal pstrPESO_CONO_OBS_1 As String,
                                                     ByVal pstrDUREZA_CONO_OBS_1 As String,
                                                     ByVal pstrOTROS_OBS_1 As String
                                                     ) As Integer
        Try
            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                            "pdatFECHA_PRODUCCION_1", pstrFECHA_PRODUCCION_1,
                                            "pvchLABORATORISTA_1", pstrLABORATORISTA_1,
                                            "pvchTURNO_1", pstrTURNO_1,
                                            "pvchLINEA_1", pstrLINEA_1,
                                            "pvchPROCESO_1", pstrPROCESO_1,
                                            "pvchMAQUINA_1", pstrMAQUINA_1,
                                            "pvchMATERIAL_1", pstrMATERIAL_1,
                                            "pvchFINURA_FIBRA_1", pstrFINURA_FIBRA_1,
                                            "pvchRESISTENCIA_FIBRA_1", pstrRESISTENCIA_FIBRA_1,
                                            "pvchLONGITUD_FIBRA_1", pstrLONGITUD_FIBRA_1,
                                            "pvchUNIFORMIDAD_FIBRA_1", pstrUNIFORMIDAD_FIBRA_1,
                                            "pvchTITULO_CINTA_HILO_1", pstrTITULO_CINTA_HILO_1,
                                            "pvchCv_TÍTULO_1", pstrCv_TÍTULO_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_H_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_1,
                                            "pvchRESISTENCIA_HILO_RKM_1", pstrRESISTENCIA_HILO_RKM_1,
                                            "pvchRESISTENCIA_MINIMA_HILO_RKM_1", pstrRESISTENCIA_MINIMA_HILO_RKM_1,
                                            "pvchCv_RESISTENCIA_HILO_1", pstrCv_RESISTENCIA_HILO_1,
                                            "pvchELONGACION_HILO_1", pstrELONGACION_HILO_1,
                                            "pvchTORSION_HILO_1", pstrTORSION_HILO_1,
                                            "pvchROTURAS_1000_HUSOS_HORA_1", pstrROTURAS_1000_HUSOS_HORA_1,
                                            "pvchRESISTENCIA_EMPALME_1", pstrRESISTENCIA_EMPALME_1,
                                            "pvchPESO_CONO_1", pstrPESO_CONO_1,
                                            "pvchDUREZA_CONO_1", pstrDUREZA_CONO_1,
                                            "pvchOTROS_1", pstrOTROS_1,
                                            "pvchFINURA_FIBRA_OBS_1", pstrFINURA_FIBRA_OBS_1,
                                            "pvchRESISTENCIA_FIBRA_OBS_1", pstrRESISTENCIA_FIBRA_OBS_1,
                                            "pvchLONGITUD_FIBRA_OBS_1", pstrLONGITUD_FIBRA_OBS_1,
                                            "pvchUNIFORMIDAD_FIBRA_OBS_1", pstrUNIFORMIDAD_FIBRA_OBS_1,
                                            "pvchTITULO_CINTA_HILO_OBS_1", pstrTITULO_CINTA_HILO_OBS_1,
                                            "pvchCv_TÍTULO_OBS_1", pstrCv_TÍTULO_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_cvM1_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PD_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_PG_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_NEPS_OBS_1,
                                            "pvchPRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1", pstrPRUEBA_UNIFORMIDAD_CINTA_HILO_H_OBS_1,
                                            "pvchRESISTENCIA_HILO_RKM_OBS_1", pstrRESISTENCIA_HILO_RKM_OBS_1,
                                            "pvchRESISTENCIA_MINIMA_HILO_RKM_OBS_1", pstrRESISTENCIA_MINIMA_HILO_RKM_OBS_1,
                                            "pvchCv_RESISTENCIA_HILO_OBS_1", pstrCv_RESISTENCIA_HILO_OBS_1,
                                            "pvchELONGACION_HILO_OBS_1", pstrELONGACION_HILO_OBS_1,
                                            "pvchTORSION_HILO_OBS_1", pstrTORSION_HILO_OBS_1,
                                            "pvchROTURAS_OBS_1000_HUSOS_HORA_OBS_1", pstrROTURAS_OBS_1000_HUSOS_HORA_OBS_1,
                                            "pvchRESISTENCIA_EMPALME_OBS_1", pstrRESISTENCIA_EMPALME_OBS_1,
                                            "pvchPESO_CONO_OBS_1", pstrPESO_CONO_OBS_1,
                                            "pvchDUREZA_CONO_OBS_1", pstrDUREZA_CONO_OBS_1,
                                            "pvchOTROS_OBS_1", pstrOTROS_OBS_1
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_LABORATORIO_HILANDERIA_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ObtenerLaboratorioHIlanderia(ByVal pstrPRUEBA_NRO As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_LABORATORIO_HILANDERIA_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function fn_InsertarPretejido(ByVal pstrPRUEBA_NRO As String,
                                        ByVal pstrFECHA_PRODUCCION_1 As String,
                                        ByVal pstrMAQUINA_1 As String,
                                        ByVal pstrSUPERVISOR_1 As String,
                                        ByVal pstrMAQUINISTA_1 As String,
                                        ByVal pstrTURNO_1 As String,
                                        ByVal pstrPARTIDA_1 As String,
                                        ByVal pstrURDIMBRE_1 As String,
                                        ByVal pstrANALISTA_1 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_1 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_OBS_1 As String,
                                        ByVal pstrTENSION_MAQUINA_1 As String,
                                        ByVal pstrTENSION_MAQUINA_OBS_1 As String,
                                        ByVal pstrOTROS_1 As String,
                                        ByVal pstrOTROS_OBS_1 As String,
                                        ByVal pstrROTURAS_MILLÓN_11 As String,
                                        ByVal pstrROTURAS_MILLÓN_OBS_11 As String,
                                        ByVal pstrTENSION_HILO_11 As String,
                                        ByVal pstrTENSION_HILO_OBS_11 As String,
                                        ByVal pstrOTROS_11 As String,
                                        ByVal pstrOTROS_OBS_11 As String,
                                        ByVal pstrFECHA_PRODUCCION_2 As String,
                                        ByVal pstrMAQUINA_2 As String,
                                        ByVal pstrSUPERVISOR_2 As String,
                                        ByVal pstrMAQUINISTA_2 As String,
                                        ByVal pstrTURNO_2 As String,
                                        ByVal pstrPARTIDA_2 As String,
                                        ByVal pstrARTICULO_2 As String,
                                        ByVal pstrANALISTA_2 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_2 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_OBS_2 As String,
                                        ByVal pstrTENSION_CABEZAL_2 As String,
                                        ByVal pstrTENSION_CABEZAL_OBS_2 As String,
                                        ByVal pstrTENSIÓN_FILETA_2 As String,
                                        ByVal pstrTENSIÓN_FILETA_OBS_2 As String,
                                        ByVal pstrPRESIONES_RODILLO_2 As String,
                                        ByVal pstrPRESIONES_RODILLO_OBS_2 As String,
                                        ByVal pstrDENSIDAD_SODA_2 As String,
                                        ByVal pstrDENSIDAD_SODA_OBS_2 As String,
                                        ByVal pstrTEMPERATURA__TINAS_TENIDO_2 As String,
                                        ByVal pstrTEMPERATURA__TINAS_TENIDO_OBS_2 As String,
                                        ByVal pstrCONCENTRACION_INDIGO_2 As String,
                                        ByVal pstrCONCENTRACION_INDIGO_OBS_2 As String,
                                        ByVal pstrOTROS_2 As String,
                                        ByVal pstrOTROS_OBS_2 As String,
                                        ByVal pstrROTURAS_22 As String,
                                        ByVal pstrROTURAS_OBS_22 As String,
                                        ByVal pstrVISCOSIDAD_GOMA_22 As String,
                                        ByVal pstrVISCOSIDAD_GOMA_OBS_22 As String,
                                        ByVal pstrOTROS_22 As String,
                                        ByVal pstrOTROS_OBS_22 As String,
                                         ByVal pstrCONCENTRACION_HOROSULFITO_2 As String,
                                        ByVal pstrCONCENTRACION_HOROSULFITO_OBS_2 As String) As Integer
        Try
            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_PRODUCCION_2 = "" Then
                pstrFECHA_PRODUCCION_2 = Nothing
            Else
                pstrFECHA_PRODUCCION_2 = Convert.ToDateTime(pstrFECHA_PRODUCCION_2).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                            "pdatFECHA_PRODUCCION_1 ", pstrFECHA_PRODUCCION_1,
                                            "pvchMAQUINA_1 ", pstrMAQUINA_1,
                                            "pvchSUPERVISOR_1 ", pstrSUPERVISOR_1,
                                            "pvchMAQUINISTA_1 ", pstrMAQUINISTA_1,
                                            "pvchTURNO_1 ", pstrTURNO_1,
                                            "pvchPARTIDA_1 ", pstrPARTIDA_1,
                                            "pvchURDIMBRE_1 ", pstrURDIMBRE_1,
                                            "pvchANALISTA_1 ", pstrANALISTA_1,
                                            "pvchVELOCIDAD_MAQUINA_1 ", pstrVELOCIDAD_MAQUINA_1,
                                            "pvchVELOCIDAD_MAQUINA_OBS_1 ", pstrVELOCIDAD_MAQUINA_OBS_1,
                                            "pvchTENSION_MAQUINA_1 ", pstrTENSION_MAQUINA_1,
                                            "pvchTENSION_MAQUINA_OBS_1 ", pstrTENSION_MAQUINA_OBS_1,
                                            "pvchOTROS_1 ", pstrOTROS_1,
                                            "pvchOTROS_OBS_1 ", pstrOTROS_OBS_1,
                                            "pvchROTURAS_MILLÓN_11 ", pstrROTURAS_MILLÓN_11,
                                            "pvchROTURAS_MILLÓN_OBS_11 ", pstrROTURAS_MILLÓN_OBS_11,
                                            "pvchTENSION_HILO_11 ", pstrTENSION_HILO_11,
                                            "pvchTENSION_HILO_OBS_11 ", pstrTENSION_HILO_OBS_11,
                                            "pvchOTROS_11 ", pstrOTROS_11,
                                            "pvchOTROS_OBS_11 ", pstrOTROS_OBS_11,
                                            "pdatFECHA_PRODUCCION_2 ", pstrFECHA_PRODUCCION_2,
                                            "pvchMAQUINA_2 ", pstrMAQUINA_2,
                                            "pvchSUPERVISOR_2 ", pstrSUPERVISOR_2,
                                            "pvchMAQUINISTA_2 ", pstrMAQUINISTA_2,
                                            "pvchTURNO_2 ", pstrTURNO_2,
                                            "pvchPARTIDA_2 ", pstrPARTIDA_2,
                                            "pvchARTICULO_2 ", pstrARTICULO_2,
                                            "pvchANALISTA_2 ", pstrANALISTA_2,
                                            "pvchVELOCIDAD_MAQUINA_2 ", pstrVELOCIDAD_MAQUINA_2,
                                            "pvchVELOCIDAD_MAQUINA_OBS_2 ", pstrVELOCIDAD_MAQUINA_OBS_2,
                                            "pvchTENSION_CABEZAL_2 ", pstrTENSION_CABEZAL_2,
                                            "pvchTENSION_CABEZAL_OBS_2 ", pstrTENSION_CABEZAL_OBS_2,
                                            "pvchTENSIÓN_FILETA_2 ", pstrTENSIÓN_FILETA_2,
                                            "pvchTENSIÓN_FILETA_OBS_2 ", pstrTENSIÓN_FILETA_OBS_2,
                                            "pvchPRESIONES_RODILLO_2 ", pstrPRESIONES_RODILLO_2,
                                            "pvchPRESIONES_RODILLO_OBS_2 ", pstrPRESIONES_RODILLO_OBS_2,
                                            "pvchDENSIDAD_SODA_2 ", pstrDENSIDAD_SODA_2,
                                            "pvchDENSIDAD_SODA_OBS_2 ", pstrDENSIDAD_SODA_OBS_2,
                                            "pvchTEMPERATURA__TINAS_TENIDO_2 ", pstrTEMPERATURA__TINAS_TENIDO_2,
                                            "pvchTEMPERATURA__TINAS_TENIDO_OBS_2 ", pstrTEMPERATURA__TINAS_TENIDO_OBS_2,
                                            "pvchCONCENTRACION_INDIGO_2 ", pstrCONCENTRACION_INDIGO_2,
                                            "pvchCONCENTRACION_INDIGO_OBS_2 ", pstrCONCENTRACION_INDIGO_OBS_2,
                                            "pvchOTROS_2 ", pstrOTROS_2,
                                            "pvchOTROS_OBS_2 ", pstrOTROS_OBS_2,
                                            "pvchROTURAS_22 ", pstrROTURAS_22,
                                            "pvchROTURAS_OBS_22 ", pstrROTURAS_OBS_22,
                                            "pvchVISCOSIDAD_GOMA_22 ", pstrVISCOSIDAD_GOMA_22,
                                            "pvchVISCOSIDAD_GOMA_OBS_22 ", pstrVISCOSIDAD_GOMA_OBS_22,
                                            "pvchOTROS_22 ", pstrOTROS_22,
                                            "pvchOTROS_OBS_22 ", pstrOTROS_OBS_22,
                                             "pvchCONCENTRACION_HOROSULFITO_2", pstrCONCENTRACION_HOROSULFITO_2,
                                             "pvchCONCENTRACION_HOROSULFITO_OBS_2", pstrCONCENTRACION_HOROSULFITO_OBS_2
                                            }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_PRETEJIDO_INSERTAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ActualizarPretejido(ByVal pstrPRUEBA_NRO As String,
                                        ByVal pstrFECHA_PRODUCCION_1 As String,
                                        ByVal pstrMAQUINA_1 As String,
                                        ByVal pstrSUPERVISOR_1 As String,
                                        ByVal pstrMAQUINISTA_1 As String,
                                        ByVal pstrTURNO_1 As String,
                                        ByVal pstrPARTIDA_1 As String,
                                        ByVal pstrURDIMBRE_1 As String,
                                        ByVal pstrANALISTA_1 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_1 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_OBS_1 As String,
                                        ByVal pstrTENSION_MAQUINA_1 As String,
                                        ByVal pstrTENSION_MAQUINA_OBS_1 As String,
                                        ByVal pstrOTROS_1 As String,
                                        ByVal pstrOTROS_OBS_1 As String,
                                        ByVal pstrROTURAS_MILLÓN_11 As String,
                                        ByVal pstrROTURAS_MILLÓN_OBS_11 As String,
                                        ByVal pstrTENSION_HILO_11 As String,
                                        ByVal pstrTENSION_HILO_OBS_11 As String,
                                        ByVal pstrOTROS_11 As String,
                                        ByVal pstrOTROS_OBS_11 As String,
                                        ByVal pstrFECHA_PRODUCCION_2 As String,
                                        ByVal pstrMAQUINA_2 As String,
                                        ByVal pstrSUPERVISOR_2 As String,
                                        ByVal pstrMAQUINISTA_2 As String,
                                        ByVal pstrTURNO_2 As String,
                                        ByVal pstrPARTIDA_2 As String,
                                        ByVal pstrARTICULO_2 As String,
                                        ByVal pstrANALISTA_2 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_2 As String,
                                        ByVal pstrVELOCIDAD_MAQUINA_OBS_2 As String,
                                        ByVal pstrTENSION_CABEZAL_2 As String,
                                        ByVal pstrTENSION_CABEZAL_OBS_2 As String,
                                        ByVal pstrTENSIÓN_FILETA_2 As String,
                                        ByVal pstrTENSIÓN_FILETA_OBS_2 As String,
                                        ByVal pstrPRESIONES_RODILLO_2 As String,
                                        ByVal pstrPRESIONES_RODILLO_OBS_2 As String,
                                        ByVal pstrDENSIDAD_SODA_2 As String,
                                        ByVal pstrDENSIDAD_SODA_OBS_2 As String,
                                        ByVal pstrTEMPERATURA__TINAS_TENIDO_2 As String,
                                        ByVal pstrTEMPERATURA__TINAS_TENIDO_OBS_2 As String,
                                        ByVal pstrCONCENTRACION_INDIGO_2 As String,
                                        ByVal pstrCONCENTRACION_INDIGO_OBS_2 As String,
                                        ByVal pstrOTROS_2 As String,
                                        ByVal pstrOTROS_OBS_2 As String,
                                        ByVal pstrROTURAS_22 As String,
                                        ByVal pstrROTURAS_OBS_22 As String,
                                        ByVal pstrVISCOSIDAD_GOMA_22 As String,
                                        ByVal pstrVISCOSIDAD_GOMA_OBS_22 As String,
                                        ByVal pstrOTROS_22 As String,
                                        ByVal pstrOTROS_OBS_22 As String,
                                        ByVal pstrCONCENTRACION_HOROSULFITO_2 As String,
                                        ByVal pstrCONCENTRACION_HOROSULFITO_OBS_2 As String) As Integer
        Try
            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_PRODUCCION_2 = "" Then
                pstrFECHA_PRODUCCION_2 = Nothing
            Else
                pstrFECHA_PRODUCCION_2 = Convert.ToDateTime(pstrFECHA_PRODUCCION_2).ToString("yyyyMMdd")

            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                            "pdatFECHA_PRODUCCION_1 ", pstrFECHA_PRODUCCION_1,
                                            "pvchMAQUINA_1 ", pstrMAQUINA_1,
                                            "pvchSUPERVISOR_1 ", pstrSUPERVISOR_1,
                                            "pvchMAQUINISTA_1 ", pstrMAQUINISTA_1,
                                            "pvchTURNO_1 ", pstrTURNO_1,
                                            "pvchPARTIDA_1 ", pstrPARTIDA_1,
                                            "pvchURDIMBRE_1 ", pstrURDIMBRE_1,
                                            "pvchANALISTA_1 ", pstrANALISTA_1,
                                            "pvchVELOCIDAD_MAQUINA_1 ", pstrVELOCIDAD_MAQUINA_1,
                                            "pvchVELOCIDAD_MAQUINA_OBS_1 ", pstrVELOCIDAD_MAQUINA_OBS_1,
                                            "pvchTENSION_MAQUINA_1 ", pstrTENSION_MAQUINA_1,
                                            "pvchTENSION_MAQUINA_OBS_1 ", pstrTENSION_MAQUINA_OBS_1,
                                            "pvchOTROS_1 ", pstrOTROS_1,
                                            "pvchOTROS_OBS_1 ", pstrOTROS_OBS_1,
                                            "pvchROTURAS_MILLÓN_11 ", pstrROTURAS_MILLÓN_11,
                                            "pvchROTURAS_MILLÓN_OBS_11 ", pstrROTURAS_MILLÓN_OBS_11,
                                            "pvchTENSION_HILO_11 ", pstrTENSION_HILO_11,
                                            "pvchTENSION_HILO_OBS_11 ", pstrTENSION_HILO_OBS_11,
                                            "pvchOTROS_11 ", pstrOTROS_11,
                                            "pvchOTROS_OBS_11 ", pstrOTROS_OBS_11,
                                            "pdatFECHA_PRODUCCION_2 ", pstrFECHA_PRODUCCION_2,
                                            "pvchMAQUINA_2 ", pstrMAQUINA_2,
                                            "pvchSUPERVISOR_2 ", pstrSUPERVISOR_2,
                                            "pvchMAQUINISTA_2 ", pstrMAQUINISTA_2,
                                            "pvchTURNO_2 ", pstrTURNO_2,
                                            "pvchPARTIDA_2 ", pstrPARTIDA_2,
                                            "pvchARTICULO_2 ", pstrARTICULO_2,
                                            "pvchANALISTA_2 ", pstrANALISTA_2,
                                            "pvchVELOCIDAD_MAQUINA_2 ", pstrVELOCIDAD_MAQUINA_2,
                                            "pvchVELOCIDAD_MAQUINA_OBS_2 ", pstrVELOCIDAD_MAQUINA_OBS_2,
                                            "pvchTENSION_CABEZAL_2 ", pstrTENSION_CABEZAL_2,
                                            "pvchTENSION_CABEZAL_OBS_2 ", pstrTENSION_CABEZAL_OBS_2,
                                            "pvchTENSIÓN_FILETA_2 ", pstrTENSIÓN_FILETA_2,
                                            "pvchTENSIÓN_FILETA_OBS_2 ", pstrTENSIÓN_FILETA_OBS_2,
                                            "pvchPRESIONES_RODILLO_2 ", pstrPRESIONES_RODILLO_2,
                                            "pvchPRESIONES_RODILLO_OBS_2 ", pstrPRESIONES_RODILLO_OBS_2,
                                            "pvchDENSIDAD_SODA_2 ", pstrDENSIDAD_SODA_2,
                                            "pvchDENSIDAD_SODA_OBS_2 ", pstrDENSIDAD_SODA_OBS_2,
                                            "pvchTEMPERATURA__TINAS_TENIDO_2 ", pstrTEMPERATURA__TINAS_TENIDO_2,
                                            "pvchTEMPERATURA__TINAS_TENIDO_OBS_2 ", pstrTEMPERATURA__TINAS_TENIDO_OBS_2,
                                            "pvchCONCENTRACION_INDIGO_2 ", pstrCONCENTRACION_INDIGO_2,
                                            "pvchCONCENTRACION_INDIGO_OBS_2 ", pstrCONCENTRACION_INDIGO_OBS_2,
                                            "pvchOTROS_2 ", pstrOTROS_2,
                                            "pvchOTROS_OBS_2 ", pstrOTROS_OBS_2,
                                            "pvchROTURAS_22 ", pstrROTURAS_22,
                                            "pvchROTURAS_OBS_22 ", pstrROTURAS_OBS_22,
                                            "pvchVISCOSIDAD_GOMA_22 ", pstrVISCOSIDAD_GOMA_22,
                                            "pvchVISCOSIDAD_GOMA_OBS_22 ", pstrVISCOSIDAD_GOMA_OBS_22,
                                            "pvchOTROS_22 ", pstrOTROS_22,
                                            "pvchOTROS_OBS_22 ", pstrOTROS_OBS_22,
                                               "pvchCONCENTRACION_HOROSULFITO_2", pstrCONCENTRACION_HOROSULFITO_2,
                                             "pvchCONCENTRACION_HOROSULFITO_OBS_2", pstrCONCENTRACION_HOROSULFITO_OBS_2
                                            }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_PRETEJIDO_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ObtenerLPretejido(ByVal pstrPRUEBA_NRO As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_PRETEJIDO_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function




    Public Function fn_InsertarRevisionFinal(
                                             ByVal pstrPRUEBA_NRO As String,
                                             ByVal pstrFECHA_MAPEADO_1 As String,
                                             ByVal pstrREVISADOR_1 As String,
                                             ByVal pstrTURNO_1 As String,
                                             ByVal pstrARTICULO_1 As String,
                                             ByVal pstrFICHA_1 As String,
                                             ByVal pstrFECHA_CORTE_1 As String,
                                             ByVal pstrCORTADOR_1 As String,
                                             ByVal pstrANALISTA_1 As String,
                                             ByVal pstrMETROS_CORTADOS_1 As String,
                                             ByVal pstrMETROS_CORTADOS_OBS_1 As String,
                                             ByVal pstrMETROS_SEGUNDAS_OBSERVADAS_1 As String,
                                             ByVal pstrMETROS_SEGUNDAS_OBSERVADAS_OBS_1 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_1_1 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_1_OBS_1 As String,
                                             ByVal pstrOTROS_1 As String,
                                             ByVal pstrOTROS_OBS_1 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_2_11 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_2_OBS_11 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_3_11 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_3_OBS_11 As String,
                                             ByVal pstrOTROS_11 As String,
                                             ByVal pstrOTROS_OBS_11 As String
                                             ) As Integer
        Try
            If pstrFECHA_MAPEADO_1 = "" Then
                pstrFECHA_MAPEADO_1 = Nothing
            Else
                pstrFECHA_MAPEADO_1 = Convert.ToDateTime(pstrFECHA_MAPEADO_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_CORTE_1 = "" Then
                pstrFECHA_CORTE_1 = Nothing
            Else
                pstrFECHA_CORTE_1 = Convert.ToDateTime(pstrFECHA_CORTE_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                             "pvchFECHA_MAPEADO_1", pstrFECHA_MAPEADO_1,
                                             "pvchREVISADOR_1", pstrREVISADOR_1,
                                             "pvchTURNO_1", pstrTURNO_1,
                                             "pvchARTICULO_1", pstrARTICULO_1,
                                             "pvchFICHA_1", pstrFICHA_1,
                                             "pvchFECHA_CORTE_1", pstrFECHA_CORTE_1,
                                             "pvchCORTADOR_1", pstrCORTADOR_1,
                                             "pvchANALISTA_1", pstrANALISTA_1,
                                             "pvchMETROS_CORTADOS_1", pstrMETROS_CORTADOS_1,
                                             "pvchMETROS_CORTADOS_OBS_1", pstrMETROS_CORTADOS_OBS_1,
                                             "pvchMETROS_SEGUNDAS_OBSERVADAS_1", pstrMETROS_SEGUNDAS_OBSERVADAS_1,
                                             "pvchMETROS_SEGUNDAS_OBSERVADAS_OBS_1", pstrMETROS_SEGUNDAS_OBSERVADAS_OBS_1,
                                             "pvchPRINCIPAL_DEFECTO_1_1", pstrPRINCIPAL_DEFECTO_1_1,
                                             "pvchPRINCIPAL_DEFECTO_1_OBS_1", pstrPRINCIPAL_DEFECTO_1_OBS_1,
                                             "pvchOTROS_1", pstrOTROS_1,
                                             "pvchOTROS_OBS_1", pstrOTROS_OBS_1,
                                             "pvchPRINCIPAL_DEFECTO_2_11", pstrPRINCIPAL_DEFECTO_2_11,
                                             "pvchPRINCIPAL_DEFECTO_2_OBS_11", pstrPRINCIPAL_DEFECTO_2_OBS_11,
                                             "pvchPRINCIPAL_DEFECTO_3_11", pstrPRINCIPAL_DEFECTO_3_11,
                                             "pvchPRINCIPAL_DEFECTO_3_OBS_11", pstrPRINCIPAL_DEFECTO_3_OBS_11,
                                             "pvchOTROS_11", pstrOTROS_11,
                                             "pvchOTROS_OBS_11", pstrOTROS_OBS_11
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_REVISION_FINAL_INSERTAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ActualizarRevisionFinal(ByVal pstrPRUEBA_NRO As String,
                                             ByVal pstrFECHA_MAPEADO_1 As String,
                                             ByVal pstrREVISADOR_1 As String,
                                             ByVal pstrTURNO_1 As String,
                                             ByVal pstrARTICULO_1 As String,
                                             ByVal pstrFICHA_1 As String,
                                             ByVal pstrFECHA_CORTE_1 As String,
                                             ByVal pstrCORTADOR_1 As String,
                                             ByVal pstrANALISTA_1 As String,
                                             ByVal pstrMETROS_CORTADOS_1 As String,
                                             ByVal pstrMETROS_CORTADOS_OBS_1 As String,
                                             ByVal pstrMETROS_SEGUNDAS_OBSERVADAS_1 As String,
                                             ByVal pstrMETROS_SEGUNDAS_OBSERVADAS_OBS_1 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_1_1 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_1_OBS_1 As String,
                                             ByVal pstrOTROS_1 As String,
                                             ByVal pstrOTROS_OBS_1 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_2_11 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_2_OBS_11 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_3_11 As String,
                                             ByVal pstrPRINCIPAL_DEFECTO_3_OBS_11 As String,
                                             ByVal pstrOTROS_11 As String,
                                             ByVal pstrOTROS_OBS_11 As String
                                             ) As Integer
        Try
            If pstrFECHA_MAPEADO_1 = "" Then
                pstrFECHA_MAPEADO_1 = Nothing
            Else
                pstrFECHA_MAPEADO_1 = Convert.ToDateTime(pstrFECHA_MAPEADO_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_CORTE_1 = "" Then
                pstrFECHA_CORTE_1 = Nothing
            Else
                pstrFECHA_CORTE_1 = Convert.ToDateTime(pstrFECHA_CORTE_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                             "pdteFECHA_MAPEADO_1", pstrFECHA_MAPEADO_1,
                                             "pvchREVISADOR_1", pstrREVISADOR_1,
                                             "pvchTURNO_1", pstrTURNO_1,
                                             "pvchARTICULO_1", pstrARTICULO_1,
                                             "pvchFICHA_1", pstrFICHA_1,
                                             "pdteFECHA_CORTE_1", pstrFECHA_CORTE_1,
                                             "pvchCORTADOR_1", pstrCORTADOR_1,
                                             "pvchANALISTA_1", pstrANALISTA_1,
                                             "pvchMETROS_CORTADOS_1", pstrMETROS_CORTADOS_1,
                                             "pvchMETROS_CORTADOS_OBS_1", pstrMETROS_CORTADOS_OBS_1,
                                             "pvchMETROS_SEGUNDAS_OBSERVADAS_1", pstrMETROS_SEGUNDAS_OBSERVADAS_1,
                                             "pvchMETROS_SEGUNDAS_OBSERVADAS_OBS_1", pstrMETROS_SEGUNDAS_OBSERVADAS_OBS_1,
                                             "pvchPRINCIPAL_DEFECTO_1_1", pstrPRINCIPAL_DEFECTO_1_1,
                                             "pvchPRINCIPAL_DEFECTO_1_OBS_1", pstrPRINCIPAL_DEFECTO_1_OBS_1,
                                             "pvchOTROS_1", pstrOTROS_1,
                                             "pvchOTROS_OBS_1", pstrOTROS_OBS_1,
                                             "pvchPRINCIPAL_DEFECTO_2_11", pstrPRINCIPAL_DEFECTO_2_11,
                                             "pvchPRINCIPAL_DEFECTO_2_OBS_11", pstrPRINCIPAL_DEFECTO_2_OBS_11,
                                             "pvchPRINCIPAL_DEFECTO_3_11", pstrPRINCIPAL_DEFECTO_3_11,
                                             "pvchPRINCIPAL_DEFECTO_3_OBS_11", pstrPRINCIPAL_DEFECTO_3_OBS_11,
                                             "pvchOTROS_11", pstrOTROS_11,
                                             "pvchOTROS_OBS_11", pstrOTROS_OBS_11
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_REVISION_FINAL_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ObtenerLRevisionFinal(ByVal pstrPRUEBA_NRO As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_REVISION_FINAL_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_InsertarTejeduria(ByVal pstrTipoPrueba As String,
                                         ByVal pstrPRUEBA_NRO As String,
                                         ByVal pstrFECHA_PRODUCCION_1 As String,
                                         ByVal pstrTEJEDOR_1 As String,
                                         ByVal pstrMECANICO_1 As String,
                                         ByVal pstrFECHA_DESMONTE_1 As String,
                                         ByVal pstrANALISTA_1 As String,
                                         ByVal pstrPARTIDA_1 As String,
                                         ByVal pstrPLEGADOR_1 As String,
                                         ByVal pstrPIEZA_1 As String,
                                         ByVal pstrTELAR_1 As String,
                                         ByVal pstrPLANTA_1 As String,
                                         ByVal pstrTITULO_1 As String,
                                         ByVal pstrMETROS_1 As String,
                                         ByVal pstrPROCEDENCIA_1 As String,
                                         ByVal pstrVELOCIDAD_TELAR_1 As String,
                                         ByVal pstrVELOCIDAD_TELAR_OBS_1 As String,
                                         ByVal pstrANCHO_PEINE_1 As String,
                                         ByVal pstrANCHO_PEINE_OBS_1 As String,
                                         ByVal pstrOTROS_1 As String,
                                         ByVal pstrOTROS_OBS_1 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_TRAMA_TURNO_11 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_11 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11 As String,
                                         ByVal pstrPRUEBA_BOIL_OFF_11 As String,
                                         ByVal pstrPRUEBA_BOIL_OFF_OBS_11 As String,
                                         ByVal pstrANCHO_ROLLO_11 As String,
                                         ByVal pstrANCHO_ROLLO_OBS_11 As String,
                                         ByVal pstrEFICIENCIA_11 As String,
                                         ByVal pstrEFICIENCIA_OBS_11 As String,
                                         ByVal pstrOTROS_11 As String,
                                         ByVal pstrOTROS_OBS_11 As String
                                         ) As Integer
        Try
            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_DESMONTE_1 = "" Then
                pstrFECHA_DESMONTE_1 = Nothing
            Else
                pstrFECHA_DESMONTE_1 = Convert.ToDateTime(pstrFECHA_DESMONTE_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchTipoPrueba", pstrTipoPrueba,
                "pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                             "pvchFECHA_PRODUCCION_1", pstrFECHA_PRODUCCION_1,
                                             "pvchTEJEDOR_1 ", pstrTEJEDOR_1,
                                             "pvchMECANICO_1 ", pstrMECANICO_1,
                                             "pvchFECHA_DESMONTE_1", pstrFECHA_DESMONTE_1,
                                             "pvchANALISTA_1 ", pstrANALISTA_1,
                                             "pvchPARTIDA_1 ", pstrPARTIDA_1,
                                             "pvchPLEGADOR_1 ", pstrPLEGADOR_1,
                                             "pvchPIEZA_1 ", pstrPIEZA_1,
                                             "pvchTELAR_1 ", pstrTELAR_1,
                                             "pvchPLANTA_1 ", pstrPLANTA_1,
                                             "pvchTITULO_1 ", pstrTITULO_1,
                                             "pvchMETROS_1 ", pstrMETROS_1,
                                             "pvchPROCEDENCIA_1 ", pstrPROCEDENCIA_1,
                                             "pvchVELOCIDAD_TELAR_1 ", pstrVELOCIDAD_TELAR_1,
                                             "pvchVELOCIDAD_TELAR_OBS_1 ", pstrVELOCIDAD_TELAR_OBS_1,
                                             "pvchANCHO_PEINE_1 ", pstrANCHO_PEINE_1,
                                             "pvchANCHO_PEINE_OBS_1 ", pstrANCHO_PEINE_OBS_1,
                                             "pvchOTROS_1 ", pstrOTROS_1,
                                             "pvchOTROS_OBS_1 ", pstrOTROS_OBS_1,
                                             "pvchCANTIDAD_ROTURAS_TRAMA_TURNO_11 ", pstrCANTIDAD_ROTURAS_TRAMA_TURNO_11,
                                             "pvchCANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11 ", pstrCANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11,
                                             "pvchCANTIDAD_ROTURAS_URDIMBRE_TURNO_11 ", pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_11,
                                             "pvchCANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11 ", pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11,
                                             "pvchPRUEBA_BOIL_OFF_11 ", pstrPRUEBA_BOIL_OFF_11,
                                             "pvchPRUEBA_BOIL_OFF_OBS_11 ", pstrPRUEBA_BOIL_OFF_OBS_11,
                                             "pvchANCHO_ROLLO_11 ", pstrANCHO_ROLLO_11,
                                             "pvchANCHO_ROLLO_OBS_11 ", pstrANCHO_ROLLO_OBS_11,
                                             "pvchEFICIENCIA_11 ", pstrEFICIENCIA_11,
                                             "pvchEFICIENCIA_OBS_11 ", pstrEFICIENCIA_OBS_11,
                                             "pvchOTROS_11 ", pstrOTROS_11,
                                             "pvchOTROS_OBS_11", pstrOTROS_OBS_11
                                             }
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_TEJEDURIA_INSERTAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ActualizarTejeduria(ByVal pstrTipoPrueba As String,
                                          ByVal pstrPRUEBA_NRO As String,
                                         ByVal pstrFECHA_PRODUCCION_1 As String,
                                         ByVal pstrTEJEDOR_1 As String,
                                         ByVal pstrMECANICO_1 As String,
                                         ByVal pstrFECHA_DESMONTE_1 As String,
                                         ByVal pstrANALISTA_1 As String,
                                         ByVal pstrPARTIDA_1 As String,
                                         ByVal pstrPLEGADOR_1 As String,
                                         ByVal pstrPIEZA_1 As String,
                                         ByVal pstrTELAR_1 As String,
                                         ByVal pstrPLANTA_1 As String,
                                         ByVal pstrTITULO_1 As String,
                                         ByVal pstrMETROS_1 As String,
                                         ByVal pstrPROCEDENCIA_1 As String,
                                         ByVal pstrVELOCIDAD_TELAR_1 As String,
                                         ByVal pstrVELOCIDAD_TELAR_OBS_1 As String,
                                         ByVal pstrANCHO_PEINE_1 As String,
                                         ByVal pstrANCHO_PEINE_OBS_1 As String,
                                         ByVal pstrOTROS_1 As String,
                                         ByVal pstrOTROS_OBS_1 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_TRAMA_TURNO_11 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_11 As String,
                                         ByVal pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11 As String,
                                         ByVal pstrPRUEBA_BOIL_OFF_11 As String,
                                         ByVal pstrPRUEBA_BOIL_OFF_OBS_11 As String,
                                         ByVal pstrANCHO_ROLLO_11 As String,
                                         ByVal pstrANCHO_ROLLO_OBS_11 As String,
                                         ByVal pstrEFICIENCIA_11 As String,
                                         ByVal pstrEFICIENCIA_OBS_11 As String,
                                         ByVal pstrOTROS_11 As String,
                                         ByVal pstrOTROS_OBS_11 As String
                                         ) As Integer
        Try
            If pstrFECHA_PRODUCCION_1 = "" Then
                pstrFECHA_PRODUCCION_1 = Nothing
            Else
                pstrFECHA_PRODUCCION_1 = Convert.ToDateTime(pstrFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If pstrFECHA_DESMONTE_1 = "" Then
                pstrFECHA_DESMONTE_1 = Nothing
            Else
                pstrFECHA_DESMONTE_1 = Convert.ToDateTime(pstrFECHA_DESMONTE_1).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchTipoPrueba", pstrTipoPrueba,
                "pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                             "pvchFECHA_PRODUCCION_1", pstrFECHA_PRODUCCION_1,
                                             "pvchTEJEDOR_1 ", pstrTEJEDOR_1,
                                             "pvchMECANICO_1 ", pstrMECANICO_1,
                                             "pvchFECHA_DESMONTE_1", pstrFECHA_DESMONTE_1,
                                             "pvchANALISTA_1 ", pstrANALISTA_1,
                                             "pvchPARTIDA_1 ", pstrPARTIDA_1,
                                             "pvchPLEGADOR_1 ", pstrPLEGADOR_1,
                                             "pvchPIEZA_1 ", pstrPIEZA_1,
                                             "pvchTELAR_1 ", pstrTELAR_1,
                                             "pvchPLANTA_1 ", pstrPLANTA_1,
                                             "pvchTITULO_1 ", pstrTITULO_1,
                                             "pvchMETROS_1 ", pstrMETROS_1,
                                             "pvchPROCEDENCIA_1 ", pstrPROCEDENCIA_1,
                                             "pvchVELOCIDAD_TELAR_1 ", pstrVELOCIDAD_TELAR_1,
                                             "pvchVELOCIDAD_TELAR_OBS_1 ", pstrVELOCIDAD_TELAR_OBS_1,
                                             "pvchANCHO_PEINE_1 ", pstrANCHO_PEINE_1,
                                             "pvchANCHO_PEINE_OBS_1 ", pstrANCHO_PEINE_OBS_1,
                                             "pvchOTROS_1 ", pstrOTROS_1,
                                             "pvchOTROS_OBS_1 ", pstrOTROS_OBS_1,
                                             "pvchCANTIDAD_ROTURAS_TRAMA_TURNO_11 ", pstrCANTIDAD_ROTURAS_TRAMA_TURNO_11,
                                             "pvchCANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11 ", pstrCANTIDAD_ROTURAS_TRAMA_TURNO_OBS_11,
                                             "pvchCANTIDAD_ROTURAS_URDIMBRE_TURNO_11 ", pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_11,
                                             "pvchCANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11 ", pstrCANTIDAD_ROTURAS_URDIMBRE_TURNO_OBS_11,
                                             "pvchPRUEBA_BOIL_OFF_11 ", pstrPRUEBA_BOIL_OFF_11,
                                             "pvchPRUEBA_BOIL_OFF_OBS_11 ", pstrPRUEBA_BOIL_OFF_OBS_11,
                                             "pvchANCHO_ROLLO_11 ", pstrANCHO_ROLLO_11,
                                             "pvchANCHO_ROLLO_OBS_11 ", pstrANCHO_ROLLO_OBS_11,
                                             "pvchEFICIENCIA_11 ", pstrEFICIENCIA_11,
                                             "pvchEFICIENCIA_OBS_11 ", pstrEFICIENCIA_OBS_11,
                                             "pvchOTROS_11 ", pstrOTROS_11,
                                             "pvchOTROS_OBS_11", pstrOTROS_OBS_11}
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_TEJEDURIA_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ObtenerLTejeduria(ByVal pstrPRUEBA_NRO As String, ByVal pvchTipoPrueba As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                              "pvchTipoPrueba", pvchTipoPrueba}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_TEJEDURIA_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function




    Public Function fn_InsertarTinteroria(ByVal pstrTipoPrueba As String,
                                         ByVal pstrPRUEBA_NRO As String,
                                          ByVal pdteFECHA_PRODUCCION_1 As String,
                                          ByVal pstrMAQUINA_1 As String,
                                          ByVal pstrSUPERVISOR_1 As String,
                                          ByVal pstrMAQUINISTA_1 As String,
                                          ByVal pstrTURNO_1 As String,
                                          ByVal pstrARTICULO_1 As String,
                                          ByVal pstrFICHA_1 As String,
                                          ByVal pstrPROCESO_1 As String,
                                          ByVal pstrMETROS_1 As String,
                                          ByVal pstrANALISTA_1 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_1 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_1 As String,
                                          ByVal pstrTEMPERATURA_1 As String,
                                          ByVal pstrTEMPERATURA_OBS_1 As String,
                                          ByVal pstrINTENSIDAD_LLAMA_1 As String,
                                          ByVal pstrINTENSIDAD_LLAMA_OBS_1 As String,
                                          ByVal pstrOTROS_1 As String,
                                          ByVal pstrOTROS_OBS_1 As String,
                                          ByVal pstrANCHO_INGRESO_11 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_11 As String,
                                          ByVal pstrANCHO_SALIDA_11 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_11 As String,
                                          ByVal pstrOTROS_11 As String,
                                          ByVal pstrOTROS_OBS_11 As String,
                                          ByVal pdteFECHA_PRODUCCION_2 As String,
                                          ByVal pstrMAQUINA_2 As String,
                                          ByVal pstrSUPERVISOR_2 As String,
                                          ByVal pstrMAQUINISTA_2 As String,
                                          ByVal pstrTURNO_2 As String,
                                          ByVal pstrARTICULO_2 As String,
                                          ByVal pstrFICHA_2 As String,
                                          ByVal pstrPROCESO_2 As String,
                                          ByVal pstrMETROS_2 As String,
                                          ByVal pstrANALISTA_2 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_2 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_2 As String,
                                          ByVal pstrTEMPERATURA_TINAS_2 As String,
                                          ByVal pstrTEMPERATURA_TINAS_OBS_2 As String,
                                          ByVal pstrOTROS_2 As String,
                                          ByVal pstrOTROS_OBS_2 As String,
                                          ByVal pstrANCHO_INGRESO_22 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_22 As String,
                                          ByVal pstrANCHO_SALIDA_22 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_22 As String,
                                          ByVal pstrOTROS_22 As String,
                                          ByVal pstrOTROS_OBS_22 As String,
                                          ByVal pdteFECHA_PRODUCCION_3 As String,
                                          ByVal pstrMAQUINA_3 As String,
                                          ByVal pstrSUPERVISOR_3 As String,
                                          ByVal pstrMAQUINISTA_3 As String,
                                          ByVal pstrTURNO_3 As String,
                                          ByVal pstrARTICULO_3 As String,
                                          ByVal pstrFICHA_3 As String,
                                          ByVal pstrPROCESO_3 As String,
                                          ByVal pstrMETROS_3 As String,
                                          ByVal pstrANALISTA_3 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_3 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_3 As String,
                                          ByVal pstrTEMPERATURA_MAQUINAS_3 As String,
                                          ByVal pstrTEMPERATURA_MAQUINAS_OBS_3 As String,
                                          ByVal pstrTEMPERATURA_CALDERO_3 As String,
                                          ByVal pstrTEMPERATURA_CALDERO_OBS_3 As String,
                                          ByVal pstrVENTILADORES_3 As String,
                                          ByVal pstrVENTILADORES_OBS_3 As String,
                                          ByVal pstrANCHO_SALIDA_m_3 As String,
                                          ByVal pstrANCHO_SALIDA_m_OBS_3 As String,
                                          ByVal pstrEXTRACTORES_3 As String,
                                          ByVal pstrEXTRACTORES_OBS_3 As String,
                                          ByVal pstrOTROS_3 As String,
                                          ByVal pstrOTROS_OBS_3 As String,
                                          ByVal pstrANCHO_INGRESO_33 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_33 As String,
                                          ByVal pstrANCHO_SALIDA_33 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_33 As String,
                                          ByVal pstrOTROS_33 As String,
                                          ByVal pstrOTROS_OBS_33 As String,
                                          ByVal pdteFECHA_PRODUCCION_4 As String,
                                          ByVal pstrMAQUINA_4 As String,
                                          ByVal pstrSUPERVISOR_4 As String,
                                          ByVal pstrMAQUINISTA_4 As String,
                                          ByVal pstrTURNO_4 As String,
                                          ByVal pstrARTICULO_4 As String,
                                          ByVal pstrFICHA_4 As String,
                                          ByVal pstrPROCESO_4 As String,
                                          ByVal pstrMETROS_4 As String,
                                          ByVal pstrANALISTA_4 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_4 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_4 As String,
                                          ByVal pstrTEMPERATURA_BANDA_4 As String,
                                          ByVal pstrTEMPERATURA_BANDA_OBS_4 As String,
                                          ByVal pstrTEMPERATURA_PALMER_4 As String,
                                          ByVal pstrTEMPERATURA_PALMER_OBS_4 As String,
                                          ByVal pstrOTROS_4 As String,
                                          ByVal pstrOTROS_OBS_4 As String,
                                          ByVal pstrANCHO_INGRESO_44 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_44 As String,
                                          ByVal pstrANCHO_SALIDA_44 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_44 As String,
                                          ByVal pstrPRUEBA_TENDIDO_ANCHO_ACABADO_44 As String,
                                          ByVal pstrPRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44 As String,
                                          ByVal pstrOTROS_44 As String,
                                          ByVal pstrOTROS_OBS_44 As String
                                          ) As Integer
        Try
            If (pdteFECHA_PRODUCCION_1 = "") Then
                pdteFECHA_PRODUCCION_1 = Nothing
            Else
                pdteFECHA_PRODUCCION_1 = Convert.ToDateTime(pdteFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If (pdteFECHA_PRODUCCION_2 = "") Then
                pdteFECHA_PRODUCCION_2 = Nothing
            Else
                pdteFECHA_PRODUCCION_2 = Convert.ToDateTime(pdteFECHA_PRODUCCION_2).ToString("yyyyMMdd")
            End If
            If (pdteFECHA_PRODUCCION_3 = "") Then
                pdteFECHA_PRODUCCION_3 = Nothing
            Else
                pdteFECHA_PRODUCCION_3 = Convert.ToDateTime(pdteFECHA_PRODUCCION_3).ToString("yyyyMMdd")
            End If
            If (pdteFECHA_PRODUCCION_4 = "") Then
                pdteFECHA_PRODUCCION_4 = Nothing
            Else
                pdteFECHA_PRODUCCION_4 = Convert.ToDateTime(pdteFECHA_PRODUCCION_4).ToString("yyyyMMdd")
            End If

            Dim objparametros() As Object = {"pvchTipoPrueba", pstrTipoPrueba,
                                             "pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                              "pdteFECHA_PRODUCCION_1", pdteFECHA_PRODUCCION_1,
                                             "pvchMAQUINA_1", pstrMAQUINA_1,
                                             "pvchSUPERVISOR_1", pstrSUPERVISOR_1,
                                             "pvchMAQUINISTA_1 ", pstrMAQUINISTA_1,
                                             "pvchTURNO_1 ", pstrTURNO_1,
                                             "pvchARTICULO_1 ", pstrARTICULO_1,
                                             "pvchFICHA_1 ", pstrFICHA_1,
                                             "pvchPROCESO_1 ", pstrPROCESO_1,
                                             "pvchMETROS_1 ", pstrMETROS_1,
                                             "pvchANALISTA_1 ", pstrANALISTA_1,
                                             "pvchVELOCIDAD_MÁQUINA_1 ", pstrVELOCIDAD_MÁQUINA_1,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_1 ", pstrVELOCIDAD_MÁQUINA_OBS_1,
                                             "pvchTEMPERATURA_1 ", pstrTEMPERATURA_1,
                                             "pvchTEMPERATURA_OBS_1 ", pstrTEMPERATURA_OBS_1,
                                             "pvchINTENSIDAD_LLAMA_1 ", pstrINTENSIDAD_LLAMA_1,
                                             "pvchINTENSIDAD_LLAMA_OBS_1 ", pstrINTENSIDAD_LLAMA_OBS_1,
                                             "pvchOTROS_1 ", pstrOTROS_1,
                                             "pvchOTROS_OBS_1 ", pstrOTROS_OBS_1,
                                             "pvchANCHO_INGRESO_11 ", pstrANCHO_INGRESO_11,
                                             "pvchANCHO_INGRESO_OBS_11 ", pstrANCHO_INGRESO_OBS_11,
                                             "pvchANCHO_SALIDA_11 ", pstrANCHO_SALIDA_11,
                                             "pvchANCHO_SALIDA_OBS_11 ", pstrANCHO_SALIDA_OBS_11,
                                             "pvchOTROS_11 ", pstrOTROS_11,
                                             "pvchOTROS_OBS_11 ", pstrOTROS_OBS_11,
                                             "pdteFECHA_PRODUCCION_2", pdteFECHA_PRODUCCION_2,
                                             "pvchMAQUINA_2", pstrMAQUINA_2,
                                             "pvchSUPERVISOR_2", pstrSUPERVISOR_2,
                                             "pvchMAQUINISTA_2 ", pstrMAQUINISTA_2,
                                             "pvchTURNO_2 ", pstrTURNO_2,
                                             "pvchARTICULO_2 ", pstrARTICULO_2,
                                             "pvchFICHA_2 ", pstrFICHA_2,
                                             "pvchPROCESO_2 ", pstrPROCESO_2,
                                             "pvchMETROS_2 ", pstrMETROS_2,
                                             "pvchANALISTA_2 ", pstrANALISTA_2,
                                             "pvchVELOCIDAD_MÁQUINA_2 ", pstrVELOCIDAD_MÁQUINA_2,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_2 ", pstrVELOCIDAD_MÁQUINA_OBS_2,
                                             "pvchTEMPERATURA_TINAS_2 ", pstrTEMPERATURA_TINAS_2,
                                             "pvchTEMPERATURA_TINAS_OBS_2 ", pstrTEMPERATURA_TINAS_OBS_2,
                                             "pvchOTROS_2 ", pstrOTROS_2,
                                             "pvchOTROS_OBS_2 ", pstrOTROS_OBS_2,
                                             "pvchANCHO_INGRESO_22 ", pstrANCHO_INGRESO_22,
                                             "pvchANCHO_INGRESO_OBS_22 ", pstrANCHO_INGRESO_OBS_22,
                                             "pvchANCHO_SALIDA_22 ", pstrANCHO_SALIDA_22,
                                             "pvchANCHO_SALIDA_OBS_22 ", pstrANCHO_SALIDA_OBS_22,
                                             "pvchOTROS_22 ", pstrOTROS_22,
                                             "pvchOTROS_OBS_22 ", pstrOTROS_OBS_22,
                                             "pdteFECHA_PRODUCCION_3", pdteFECHA_PRODUCCION_3,
                                             "pvchMAQUINA_3", pstrMAQUINA_3,
                                             "pvchSUPERVISOR_3", pstrSUPERVISOR_3,
                                             "pvchMAQUINISTA_3 ", pstrMAQUINISTA_3,
                                             "pvchTURNO_3 ", pstrTURNO_3,
                                             "pvchARTICULO_3 ", pstrARTICULO_3,
                                             "pvchFICHA_3 ", pstrFICHA_3,
                                             "pvchPROCESO_3 ", pstrPROCESO_3,
                                             "pvchMETROS_3 ", pstrMETROS_3,
                                             "pvchANALISTA_3 ", pstrANALISTA_3,
                                             "pvchVELOCIDAD_MÁQUINA_3 ", pstrVELOCIDAD_MÁQUINA_3,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_3 ", pstrVELOCIDAD_MÁQUINA_OBS_3,
                                             "pvchTEMPERATURA_MAQUINAS_3 ", pstrTEMPERATURA_MAQUINAS_3,
                                             "pvchTEMPERATURA_MAQUINAS_OBS_3 ", pstrTEMPERATURA_MAQUINAS_OBS_3,
                                             "pvchTEMPERATURA_CALDERO_3 ", pstrTEMPERATURA_CALDERO_3,
                                             "pvchTEMPERATURA_CALDERO_OBS_3 ", pstrTEMPERATURA_CALDERO_OBS_3,
                                             "pvchVENTILADORES_3 ", pstrVENTILADORES_3,
                                             "pvchVENTILADORES_OBS_3 ", pstrVENTILADORES_OBS_3,
                                             "pvchANCHO_SALIDA_m_3", pstrANCHO_SALIDA_m_3,
                                             "pvchANCHO_SALIDA_m_OBS_3", pstrANCHO_SALIDA_m_OBS_3,
                                             "pvchEXTRACTORES_3 ", pstrEXTRACTORES_3,
                                             "pvchEXTRACTORES_OBS_3 ", pstrEXTRACTORES_OBS_3,
                                             "pvchOTROS_3 ", pstrOTROS_3,
                                             "pvchOTROS_OBS_3 ", pstrOTROS_OBS_3,
                                             "pvchANCHO_INGRESO_33 ", pstrANCHO_INGRESO_33,
                                             "pvchANCHO_INGRESO_OBS_33 ", pstrANCHO_INGRESO_OBS_33,
                                             "pvchANCHO_SALIDA_33 ", pstrANCHO_SALIDA_33,
                                             "pvchANCHO_SALIDA_OBS_33 ", pstrANCHO_SALIDA_OBS_33,
                                             "pvchOTROS_33 ", pstrOTROS_33,
                                             "pvchOTROS_OBS_33 ", pstrOTROS_OBS_33,
                                             "pdteFECHA_PRODUCCION_4", pdteFECHA_PRODUCCION_4,
                                             "pvchMAQUINA_4", pstrMAQUINA_4,
                                             "pvchSUPERVISOR_4", pstrSUPERVISOR_4,
                                             "pvchMAQUINISTA_4 ", pstrMAQUINISTA_4,
                                             "pvchTURNO_4 ", pstrTURNO_4,
                                             "pvchARTICULO_4 ", pstrARTICULO_4,
                                             "pvchFICHA_4 ", pstrFICHA_4,
                                             "pvchPROCESO_4 ", pstrPROCESO_4,
                                             "pvchMETROS_4 ", pstrMETROS_4,
                                             "pvchANALISTA_4 ", pstrANALISTA_4,
                                             "pvchVELOCIDAD_MÁQUINA_4 ", pstrVELOCIDAD_MÁQUINA_4,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_4 ", pstrVELOCIDAD_MÁQUINA_OBS_4,
                                             "pvchTEMPERATURA_BANDA_4 ", pstrTEMPERATURA_BANDA_4,
                                             "pvchTEMPERATURA_BANDA_OBS_4 ", pstrTEMPERATURA_BANDA_OBS_4,
                                             "pvchTEMPERATURA_PALMER_4 ", pstrTEMPERATURA_PALMER_4,
                                             "pvchTEMPERATURA_PALMER_OBS_4 ", pstrTEMPERATURA_PALMER_OBS_4,
                                             "pvchOTROS_4 ", pstrOTROS_4,
                                             "pvchOTROS_OBS_4 ", pstrOTROS_OBS_4,
                                             "pvchANCHO_INGRESO_44 ", pstrANCHO_INGRESO_44,
                                             "pvchANCHO_INGRESO_OBS_44 ", pstrANCHO_INGRESO_OBS_44,
                                             "pvchANCHO_SALIDA_44 ", pstrANCHO_SALIDA_44,
                                             "pvchANCHO_SALIDA_OBS_44 ", pstrANCHO_SALIDA_OBS_44,
                                             "pvchPRUEBA_TENDIDO_ANCHO_ACABADO_44 ", pstrPRUEBA_TENDIDO_ANCHO_ACABADO_44,
                                             "pvchPRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44 ", pstrPRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44,
                                             "pvchOTROS_44 ", pstrOTROS_44,
                                             "pvchOTROS_OBS_44", pstrOTROS_OBS_44}
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_TINTORERIA_INSERTAR", objparametros)

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ActualizarTinteroria(ByVal pstrTipoPrueba As String,
                                           ByVal pstrPRUEBA_NRO As String,
                                           ByVal pdteFECHA_PRODUCCION_1 As String,
                                          ByVal pstrMAQUINA_1 As String,
                                          ByVal pstrSUPERVISOR_1 As String,
                                          ByVal pstrMAQUINISTA_1 As String,
                                          ByVal pstrTURNO_1 As String,
                                          ByVal pstrARTICULO_1 As String,
                                          ByVal pstrFICHA_1 As String,
                                          ByVal pstrPROCESO_1 As String,
                                          ByVal pstrMETROS_1 As String,
                                          ByVal pstrANALISTA_1 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_1 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_1 As String,
                                          ByVal pstrTEMPERATURA_1 As String,
                                          ByVal pstrTEMPERATURA_OBS_1 As String,
                                          ByVal pstrINTENSIDAD_LLAMA_1 As String,
                                          ByVal pstrINTENSIDAD_LLAMA_OBS_1 As String,
                                          ByVal pstrOTROS_1 As String,
                                          ByVal pstrOTROS_OBS_1 As String,
                                          ByVal pstrANCHO_INGRESO_11 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_11 As String,
                                          ByVal pstrANCHO_SALIDA_11 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_11 As String,
                                          ByVal pstrOTROS_11 As String,
                                          ByVal pstrOTROS_OBS_11 As String,
                                          ByVal pdteFECHA_PRODUCCION_2 As String,
                                          ByVal pstrMAQUINA_2 As String,
                                          ByVal pstrSUPERVISOR_2 As String,
                                          ByVal pstrMAQUINISTA_2 As String,
                                          ByVal pstrTURNO_2 As String,
                                          ByVal pstrARTICULO_2 As String,
                                          ByVal pstrFICHA_2 As String,
                                          ByVal pstrPROCESO_2 As String,
                                          ByVal pstrMETROS_2 As String,
                                          ByVal pstrANALISTA_2 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_2 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_2 As String,
                                          ByVal pstrTEMPERATURA_TINAS_2 As String,
                                          ByVal pstrTEMPERATURA_TINAS_OBS_2 As String,
                                          ByVal pstrOTROS_2 As String,
                                          ByVal pstrOTROS_OBS_2 As String,
                                          ByVal pstrANCHO_INGRESO_22 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_22 As String,
                                          ByVal pstrANCHO_SALIDA_22 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_22 As String,
                                          ByVal pstrOTROS_22 As String,
                                          ByVal pstrOTROS_OBS_22 As String,
                                          ByVal pdteFECHA_PRODUCCION_3 As String,
                                          ByVal pstrMAQUINA_3 As String,
                                          ByVal pstrSUPERVISOR_3 As String,
                                          ByVal pstrMAQUINISTA_3 As String,
                                          ByVal pstrTURNO_3 As String,
                                          ByVal pstrARTICULO_3 As String,
                                          ByVal pstrFICHA_3 As String,
                                          ByVal pstrPROCESO_3 As String,
                                          ByVal pstrMETROS_3 As String,
                                          ByVal pstrANALISTA_3 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_3 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_3 As String,
                                          ByVal pstrTEMPERATURA_MAQUINAS_3 As String,
                                          ByVal pstrTEMPERATURA_MAQUINAS_OBS_3 As String,
                                          ByVal pstrTEMPERATURA_CALDERO_3 As String,
                                          ByVal pstrTEMPERATURA_CALDERO_OBS_3 As String,
                                          ByVal pstrVENTILADORES_3 As String,
                                          ByVal pstrVENTILADORES_OBS_3 As String,
                                           ByVal pstrANCHO_SALIDA_m_3 As String,
                                          ByVal pstrANCHO_SALIDA_m_OBS_3 As String,
                                          ByVal pstrEXTRACTORES_3 As String,
                                          ByVal pstrEXTRACTORES_OBS_3 As String,
                                          ByVal pstrOTROS_3 As String,
                                          ByVal pstrOTROS_OBS_3 As String,
                                          ByVal pstrANCHO_INGRESO_33 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_33 As String,
                                          ByVal pstrANCHO_SALIDA_33 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_33 As String,
                                          ByVal pstrOTROS_33 As String,
                                          ByVal pstrOTROS_OBS_33 As String,
                                          ByVal pdteFECHA_PRODUCCION_4 As String,
                                          ByVal pstrMAQUINA_4 As String,
                                          ByVal pstrSUPERVISOR_4 As String,
                                          ByVal pstrMAQUINISTA_4 As String,
                                          ByVal pstrTURNO_4 As String,
                                          ByVal pstrARTICULO_4 As String,
                                          ByVal pstrFICHA_4 As String,
                                          ByVal pstrPROCESO_4 As String,
                                          ByVal pstrMETROS_4 As String,
                                          ByVal pstrANALISTA_4 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_4 As String,
                                          ByVal pstrVELOCIDAD_MÁQUINA_OBS_4 As String,
                                          ByVal pstrTEMPERATURA_BANDA_4 As String,
                                          ByVal pstrTEMPERATURA_BANDA_OBS_4 As String,
                                          ByVal pstrTEMPERATURA_PALMER_4 As String,
                                          ByVal pstrTEMPERATURA_PALMER_OBS_4 As String,
                                          ByVal pstrOTROS_4 As String,
                                          ByVal pstrOTROS_OBS_4 As String,
                                          ByVal pstrANCHO_INGRESO_44 As String,
                                          ByVal pstrANCHO_INGRESO_OBS_44 As String,
                                          ByVal pstrANCHO_SALIDA_44 As String,
                                          ByVal pstrANCHO_SALIDA_OBS_44 As String,
                                          ByVal pstrPRUEBA_TENDIDO_ANCHO_ACABADO_44 As String,
                                          ByVal pstrPRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44 As String,
                                          ByVal pstrOTROS_44 As String,
                                          ByVal pstrOTROS_OBS_44 As String
                                          ) As Integer
        Try
            If (pdteFECHA_PRODUCCION_1 = "") Then
                pdteFECHA_PRODUCCION_1 = Nothing
            Else
                pdteFECHA_PRODUCCION_1 = Convert.ToDateTime(pdteFECHA_PRODUCCION_1).ToString("yyyyMMdd")
            End If
            If (pdteFECHA_PRODUCCION_2 = "") Then
                pdteFECHA_PRODUCCION_2 = Nothing
            Else
                pdteFECHA_PRODUCCION_2 = Convert.ToDateTime(pdteFECHA_PRODUCCION_2).ToString("yyyyMMdd")
            End If
            If (pdteFECHA_PRODUCCION_3 = "") Then
                pdteFECHA_PRODUCCION_3 = Nothing
            Else
                pdteFECHA_PRODUCCION_3 = Convert.ToDateTime(pdteFECHA_PRODUCCION_3).ToString("yyyyMMdd")
            End If
            If (pdteFECHA_PRODUCCION_4 = "") Then
                pdteFECHA_PRODUCCION_4 = Nothing
            Else
                pdteFECHA_PRODUCCION_4 = Convert.ToDateTime(pdteFECHA_PRODUCCION_4).ToString("yyyyMMdd")
            End If
            Dim objparametros() As Object = {"pvchTipoPrueba", pstrTipoPrueba,
                                             "pvchPRUEBA_NRO ", pstrPRUEBA_NRO,
                                             "pdteFECHA_PRODUCCION_1", pdteFECHA_PRODUCCION_1,
                                             "pvchMAQUINA_1", pstrMAQUINA_1,
                                             "pvchSUPERVISOR_1", pstrSUPERVISOR_1,
                                             "pvchMAQUINISTA_1 ", pstrMAQUINISTA_1,
                                             "pvchTURNO_1 ", pstrTURNO_1,
                                             "pvchARTICULO_1 ", pstrARTICULO_1,
                                             "pvchFICHA_1 ", pstrFICHA_1,
                                             "pvchPROCESO_1 ", pstrPROCESO_1,
                                             "pvchMETROS_1 ", pstrMETROS_1,
                                             "pvchANALISTA_1 ", pstrANALISTA_1,
                                             "pvchVELOCIDAD_MÁQUINA_1 ", pstrVELOCIDAD_MÁQUINA_1,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_1 ", pstrVELOCIDAD_MÁQUINA_OBS_1,
                                             "pvchTEMPERATURA_1 ", pstrTEMPERATURA_1,
                                             "pvchTEMPERATURA_OBS_1 ", pstrTEMPERATURA_OBS_1,
                                             "pvchINTENSIDAD_LLAMA_1 ", pstrINTENSIDAD_LLAMA_1,
                                             "pvchINTENSIDAD_LLAMA_OBS_1 ", pstrINTENSIDAD_LLAMA_OBS_1,
                                             "pvchOTROS_1 ", pstrOTROS_1,
                                             "pvchOTROS_OBS_1 ", pstrOTROS_OBS_1,
                                             "pvchANCHO_INGRESO_11 ", pstrANCHO_INGRESO_11,
                                             "pvchANCHO_INGRESO_OBS_11 ", pstrANCHO_INGRESO_OBS_11,
                                             "pvchANCHO_SALIDA_11 ", pstrANCHO_SALIDA_11,
                                             "pvchANCHO_SALIDA_OBS_11 ", pstrANCHO_SALIDA_OBS_11,
                                             "pvchOTROS_11 ", pstrOTROS_11,
                                             "pvchOTROS_OBS_11 ", pstrOTROS_OBS_11,
                                             "pdteFECHA_PRODUCCION_2", pdteFECHA_PRODUCCION_2,
                                             "pvchMAQUINA_2", pstrMAQUINA_2,
                                             "pvchSUPERVISOR_2", pstrSUPERVISOR_2,
                                             "pvchMAQUINISTA_2 ", pstrMAQUINISTA_2,
                                             "pvchTURNO_2 ", pstrTURNO_2,
                                             "pvchARTICULO_2 ", pstrARTICULO_2,
                                             "pvchFICHA_2 ", pstrFICHA_2,
                                             "pvchPROCESO_2 ", pstrPROCESO_2,
                                             "pvchMETROS_2 ", pstrMETROS_2,
                                             "pvchANALISTA_2 ", pstrANALISTA_2,
                                             "pvchVELOCIDAD_MÁQUINA_2 ", pstrVELOCIDAD_MÁQUINA_2,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_2 ", pstrVELOCIDAD_MÁQUINA_OBS_2,
                                             "pvchTEMPERATURA_TINAS_2 ", pstrTEMPERATURA_TINAS_2,
                                             "pvchTEMPERATURA_TINAS_OBS_2 ", pstrTEMPERATURA_TINAS_OBS_2,
                                             "pvchOTROS_2 ", pstrOTROS_2,
                                             "pvchOTROS_OBS_2 ", pstrOTROS_OBS_2,
                                             "pvchANCHO_INGRESO_22 ", pstrANCHO_INGRESO_22,
                                             "pvchANCHO_INGRESO_OBS_22 ", pstrANCHO_INGRESO_OBS_22,
                                             "pvchANCHO_SALIDA_22 ", pstrANCHO_SALIDA_22,
                                             "pvchANCHO_SALIDA_OBS_22 ", pstrANCHO_SALIDA_OBS_22,
                                             "pvchOTROS_22 ", pstrOTROS_22,
                                             "pvchOTROS_OBS_22 ", pstrOTROS_OBS_22,
                                             "pdteFECHA_PRODUCCION_3", pdteFECHA_PRODUCCION_3,
                                             "pvchMAQUINA_3", pstrMAQUINA_3,
                                             "pvchSUPERVISOR_3", pstrSUPERVISOR_3,
                                             "pvchMAQUINISTA_3 ", pstrMAQUINISTA_3,
                                             "pvchTURNO_3 ", pstrTURNO_3,
                                             "pvchARTICULO_3 ", pstrARTICULO_3,
                                             "pvchFICHA_3 ", pstrFICHA_3,
                                             "pvchPROCESO_3 ", pstrPROCESO_3,
                                             "pvchMETROS_3 ", pstrMETROS_3,
                                             "pvchANALISTA_3 ", pstrANALISTA_3,
                                             "pvchVELOCIDAD_MÁQUINA_3 ", pstrVELOCIDAD_MÁQUINA_3,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_3 ", pstrVELOCIDAD_MÁQUINA_OBS_3,
                                             "pvchTEMPERATURA_MAQUINAS_3 ", pstrTEMPERATURA_MAQUINAS_3,
                                             "pvchTEMPERATURA_MAQUINAS_OBS_3 ", pstrTEMPERATURA_MAQUINAS_OBS_3,
                                             "pvchTEMPERATURA_CALDERO_3 ", pstrTEMPERATURA_CALDERO_3,
                                             "pvchTEMPERATURA_CALDERO_OBS_3 ", pstrTEMPERATURA_CALDERO_OBS_3,
                                             "pvchVENTILADORES_3 ", pstrVENTILADORES_3,
                                             "pvchVENTILADORES_OBS_3 ", pstrVENTILADORES_OBS_3,
                                              "pvchANCHO_SALIDA_m_3", pstrANCHO_SALIDA_m_3,
                                             "pvchANCHO_SALIDA_m_OBS_3", pstrANCHO_SALIDA_m_OBS_3,
                                             "pvchEXTRACTORES_3 ", pstrEXTRACTORES_3,
                                             "pvchEXTRACTORES_OBS_3 ", pstrEXTRACTORES_OBS_3,
                                             "pvchOTROS_3 ", pstrOTROS_3,
                                             "pvchOTROS_OBS_3 ", pstrOTROS_OBS_3,
                                             "pvchANCHO_INGRESO_33 ", pstrANCHO_INGRESO_33,
                                             "pvchANCHO_INGRESO_OBS_33 ", pstrANCHO_INGRESO_OBS_33,
                                             "pvchANCHO_SALIDA_33 ", pstrANCHO_SALIDA_33,
                                             "pvchANCHO_SALIDA_OBS_33 ", pstrANCHO_SALIDA_OBS_33,
                                             "pvchOTROS_33 ", pstrOTROS_33,
                                             "pvchOTROS_OBS_33 ", pstrOTROS_OBS_33,
                                             "pdteFECHA_PRODUCCION_4", pdteFECHA_PRODUCCION_4,
                                             "pvchMAQUINA_4", pstrMAQUINA_4,
                                             "pvchSUPERVISOR_4", pstrSUPERVISOR_4,
                                             "pvchMAQUINISTA_4 ", pstrMAQUINISTA_4,
                                             "pvchTURNO_4 ", pstrTURNO_4,
                                             "pvchARTICULO_4 ", pstrARTICULO_4,
                                             "pvchFICHA_4 ", pstrFICHA_4,
                                             "pvchPROCESO_4 ", pstrPROCESO_4,
                                             "pvchMETROS_4 ", pstrMETROS_4,
                                             "pvchANALISTA_4 ", pstrANALISTA_4,
                                             "pvchVELOCIDAD_MÁQUINA_4 ", pstrVELOCIDAD_MÁQUINA_4,
                                             "pvchVELOCIDAD_MÁQUINA_OBS_4 ", pstrVELOCIDAD_MÁQUINA_OBS_4,
                                             "pvchTEMPERATURA_BANDA_4 ", pstrTEMPERATURA_BANDA_4,
                                             "pvchTEMPERATURA_BANDA_OBS_4 ", pstrTEMPERATURA_BANDA_OBS_4,
                                             "pvchTEMPERATURA_PALMER_4 ", pstrTEMPERATURA_PALMER_4,
                                             "pvchTEMPERATURA_PALMER_OBS_4 ", pstrTEMPERATURA_PALMER_OBS_4,
                                             "pvchOTROS_4 ", pstrOTROS_4,
                                             "pvchOTROS_OBS_4 ", pstrOTROS_OBS_4,
                                             "pvchANCHO_INGRESO_44 ", pstrANCHO_INGRESO_44,
                                             "pvchANCHO_INGRESO_OBS_44 ", pstrANCHO_INGRESO_OBS_44,
                                             "pvchANCHO_SALIDA_44 ", pstrANCHO_SALIDA_44,
                                             "pvchANCHO_SALIDA_OBS_44 ", pstrANCHO_SALIDA_OBS_44,
                                             "pvchPRUEBA_TENDIDO_ANCHO_ACABADO_44 ", pstrPRUEBA_TENDIDO_ANCHO_ACABADO_44,
                                             "pvchPRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44 ", pstrPRUEBA_TENDIDO_ANCHO_ACABADO_OBS_44,
                                             "pvchOTROS_44 ", pstrOTROS_44,
                                             "pvchOTROS_OBS_44", pstrOTROS_OBS_44}
            Return _objConnexion.EjecutarComando("USP_CAL_HRUTA_TINTORERIA_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function fn_ObtenerTinteroria(ByVal pstrPRUEBA_NRO As String, ByVal pvchTipoPrueba As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvchPRUEBA_NRO", pstrPRUEBA_NRO,
                                             "pvchTipoPrueba", pvchTipoPrueba}
            Return _objConnexion.ObtenerDataTable("USP_CAL_HRUTA_TINTORERIA_SELECCIONAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_GuardarProcesosHojaRuta(ByVal pstrPRUEBA_NRO As String,
                                               ByVal INT_COD_GENFOR As Integer,
                                               ByVal INT_COD_FORMATO As Integer,
                                               ByVal VCH_TIPO_PRUEBA As String,
                                               ByVal pstrHilanderia As Boolean,
                                               ByVal pstrPretejeduria As Boolean,
                                               ByVal pstrTejeduria As Boolean,
                                               ByVal pstrTintoreria As Boolean,
                                               ByVal pstrRevisionFinal As Boolean,
                                               ByVal pstrLaboratorioHilanderia As Boolean,
                                               ByVal pstrLaboratorioFisico As Boolean,
                                               ByVal pstrFechaIngreso As String,
                                               ByVal pstrItem As String,
                                               ByVal conclusion_final As String
                                           ) As Integer
        Try
            Dim fecha As Date = Nothing
            If (pstrFechaIngreso = "") Then
                fecha = Nothing
            Else
                fecha = Convert.ToDateTime(pstrFechaIngreso)
            End If

            Dim objparametros() As Object = {"VCH_NROPRELIMINAR", pstrPRUEBA_NRO,
                                             "INT_COD_GENFOR", INT_COD_GENFOR,
                                             "INT_COD_FORMATO", INT_COD_FORMATO,
                                             "VCH_TIPO_PRUEBA", VCH_TIPO_PRUEBA,
                                             "pvchHilanderia", pstrHilanderia,
                                             "pvchPretejeduria", pstrPretejeduria,
                                             "pvchTejeduria", pstrTejeduria,
                                             "pvchTintoreria", pstrTintoreria,
                                             "pvchRevisionFinal", pstrRevisionFinal,
                                             "pvchLaboratorioHilanderia", pstrLaboratorioHilanderia,
                                             "pvchLaboratorioFisico", pstrLaboratorioFisico,
                                             "VCH_ITEM", pstrItem,
                                             "DAT_FECHA_INGRESO", fecha,
                                             "conclusion_final", conclusion_final
                                             }
            _objConnexion.EjecutarComando("USP_CAL_HRUTA_TIPOPRUEBA_FORMATOGENERADO_2_INSERTAR_ACTUALIZAR", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fn_ActualizarBloquearDesbloquearProcesosHojaRuta(ByVal pstrPRUEBA_NRO As String,
                                             ByVal INT_COD_GENFOR As Integer,
                                             ByVal INT_COD_FORMATO As Integer,
                                             ByVal VCH_TIPO_PRUEBA As String,
                                             ByVal BOL_PROCESO As Boolean,
                                             ByVal PROCESO As String
                                         ) As Integer
        Try

            Dim objparametros() As Object = {"VCH_NROPRELIMINAR", pstrPRUEBA_NRO,
                                             "INT_COD_GENFOR", INT_COD_GENFOR,
                                             "INT_COD_FORMATO", INT_COD_FORMATO,
                                             "VCH_TIPO_PRUEBA", VCH_TIPO_PRUEBA,
                                             "BOL_PROCESO", BOL_PROCESO,
                                             "PROCESO", PROCESO
                                             }
            _objConnexion.EjecutarComando("USP_CAL_HRUTA_BLOQUEARDESBLOQUEAR_PROCESO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
