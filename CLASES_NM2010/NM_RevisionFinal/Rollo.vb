Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Rollo
        Implements IDisposable

        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer
        Private m_sqlDtAccRevFin As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerRollos(ByVal strCodigoLote As String) As DataTable
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros As Object() = {"codigo_lote", strCodigoLote}

            Return adSQL.ObtenerDataTable("UP_ObtenerRollos", objParametros)
        End Function

        Function ObtenerDatosFicha(ByVal strCodigoRollo As String) As DataTable
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim dtblPiezas As DataTable

            Try
                Dim objParametros As Object() = {"strCodigoRollo", strCodigoRollo}

                dtblPiezas = adSQL.ObtenerDataTable("UP_ObtenerDatosFicha", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblPiezas

        End Function

        '------------------------------------------------------------------------------------------------

        Public Function ObtenerRolloDetalle(ByVal pstrCodigoRollo As String) As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                Dim objParametros() As Object = {"p_var_CodigoRollo", pstrCodigoRollo}

                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("usp_qry_ObtenerRolloDetalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Function ObtenerRollosPorCodigo(ByVal pRollo As String) As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                Dim objParametros() As Object = {"codigo_rollo", pRollo}

                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("ObtenerRolloPorCodigo_Temp", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Function ObtenerPlanillaRollos_porCodigo(ByVal pRollo As String) As DataSet
            Dim ldtsDatosBusqueda As DataSet

            Try
                Dim objParametros() As Object = {"codigo_rollo", pRollo}

                ldtsDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataSet("ObtenerRolloPorCodigo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtsDatosBusqueda
        End Function

        Public Function ObtenerRollosPorCodigoV2(ByVal pRollo As String) As DataSet
            Try
                Dim objParametros() As Object = {"codigo_rollo", pRollo}
                Return m_sqlDtAccOfiPlan.ObtenerDataSet("ObtenerRolloPorCodigoV2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function KilogramosMetroLineal(ByVal pstrCodigoFicha As String) As Double
            Try
                Dim objParametros As Object() = {"VAR_FICHA", pstrCodigoFicha}

                Return Convert.ToDouble(m_sqlDtAccRevFin.ObtenerValor("USP_RVF_FICHAKGXMTLINEAL", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function KilogramosMetroLinealAcabado(ByVal pstrCodigoFicha As String) As Double
            'Obtiene el peso acabado desde la tabla Artículo Estandar
            'busqueda segun el código de ficha
            Try
                Dim objParametros As Object() = {"VAR_FICHA", pstrCodigoFicha}

                Return Convert.ToDouble(m_sqlDtAccRevFin.ObtenerValor("USP_RVF_FICHAKGXMTLINEALACABADO", objParametros))
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "Guido modificacion"

        Public Function ultimos_rollos_metrajeV2(ByVal ficha As String) As DataTable
            Try
                Dim objParametros As Object() = {"CODIGO_FICHA", ficha}
                Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_ULTIMOS_ROLLO_METRAJE_V2", objParametros)
                'Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_ULTIMOS_ROLLO_METRAJE_V2_TEMP", objParametros) ' Add (Luis_AJ)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GenerarRolloV2(ByVal strCodigoFicha As String, ByVal dblMetrajeInicial As Double, _
         ByVal dblMetrajeFinal As Double, ByVal dblAnchoMinimo As Double, _
         ByVal dblAnchoMaximo As Double, ByVal dblMetrosRetazoInicial As Double, _
         ByVal dblMetrosRetazoFinal As Double, ByVal dblRetazoInicial As Double, _
         ByVal dblRetazoFinal As Double, ByVal blnObservar As Boolean, ByVal strUsuario As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha, _
                  "metraje_inicial", dblMetrajeInicial, _
                  "metraje_final", dblMetrajeFinal, _
                  "ancho_minimo", dblAnchoMinimo, _
                  "ancho_maximo", dblAnchoMaximo, _
                  "metros_retazo_inicial", dblMetrosRetazoInicial, _
                  "metros_retazo_final", dblMetrosRetazoFinal, _
                  "retazo_inicial", dblRetazoInicial, _
                  "retazo_final", dblRetazoFinal, _
                  "observado", blnObservar, "usuario", strUsuario}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_GenerarRolloV2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function Clasificacion(ByVal pstrFicha As String) As String
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrRes As String
            Try
                Dim lstrParams() As String = {"varFicha", pstrFicha}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                lstrRes = CStr(lobjCon.ObtenerValor("usp_REV_Ficha_ObtenerClasificacion", lstrParams))
            Catch ex As Exception
                lstrRes = ""
            End Try
            Return lstrRes
        End Function

        Public Function ObtenerClasificacion(ByVal pstrFicha As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParams() As String = {"var_CodigoFicha", pstrFicha}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Return lobjCon.ObtenerDataTable("usp_REV_Obtener_DetalleClasificacion", lstrParams)
            Catch ex As Exception
            Finally
                lobjCon = Nothing
            End Try
        End Function
        Public Function ObtenerPlegadorObserTeje(ByVal pstrFicha As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParams() As String = {"CODIGO_FICHA", pstrFicha}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Return lobjCon.ObtenerDataTable("USP_OBTENER_PLEGADOR_OBSERVACION", lstrParams)
            Catch ex As Exception
            Finally
                lobjCon = Nothing
            End Try
        End Function
        Public Function ufn_GeneraRolloPlanillaCorte(ByVal strCodigoFicha As String, _
         ByVal strCodigoPlanilla As String, ByVal strCodigoPieza As String, _
         ByVal dblMetrajeInicial As Double, _
         ByVal dblMetrajeFinal As Double, ByVal dblAnchoMinimo As Double, _
         ByVal dblAnchoMaximo As Double, ByVal dblMetrosRetazoInicial As Double, _
         ByVal dblMetrosRetazoFinal As Double, ByVal dblInicioOtroRetazo As Double, _
         ByVal dblFinalOtroRetazo As Double, ByVal blnObservar As Boolean, ByVal intCortadora As Integer, _
         ByVal strUsuario As String, ByVal strPiezaUnida As Boolean, _
         Optional ByVal dblPeso As Double = 0, Optional ByVal dblPesoTeorico As Double = 0, _
         Optional ByVal strClasificacion As String = "", Optional ByVal strTipoDefecto As String = "", Optional ByVal strValorDefecto As String = "", Optional ByVal strAnchoReal As Double = 0) As DataTable
            Try
                m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim objParametros As Object() = {"var_CodigoPlanilla", strCodigoPlanilla, _
                "var_CodigoFicha", strCodigoFicha, _
                 "num_MetrajeInicial", dblMetrajeInicial, _
                 "num_MetrajeFinal", dblMetrajeFinal, _
                 "num_AnchoMinimo", dblAnchoMinimo, _
                 "num_AnchoMaximo", dblAnchoMaximo, _
                 "num_MetrosRetazoInicial", dblMetrosRetazoInicial, _
                 "num_MetrosRetazoFinal", dblMetrosRetazoFinal, _
                 "num_InicioRetazo", dblInicioOtroRetazo, _
                 "num_FinalRetazo", dblFinalOtroRetazo, _
                 "bit_Observado", blnObservar, _
                 "var_CodigoUsuario", strUsuario, _
                 "var_CodigoPieza", strCodigoPieza, _
                 "num_Peso", dblPeso, _
                 "num_PesoTeorico", dblPesoTeorico, _
                 "var_Clasificacion", strClasificacion, _
                 "var_CodigoDefecto", strTipoDefecto, _
                 "var_ValorDefecto", strValorDefecto, _
                 "num_AnchoReal", strAnchoReal, _
                 "bit_PiezaUnida", strPiezaUnida, _
                 "int_Cortadora", intCortadora}

                Return m_sqlDtAccRevFin.ObtenerDataTable("usp_ins_GeneraRolloPlanillaCorte", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccRevFin = Nothing
            End Try
        End Function

        Public Function ufn_GeneraRolloPlanillaCorte_2(ByVal strCodigoFicha As String, _
         ByVal strCodigoPlanilla As String, ByVal strCodigoPieza As String, _
         ByVal dblMetrajeInicial As Double, ByVal dblMetrajeFinal As Double, _
         ByVal dblAnchoMinimo As Double, ByVal dblAnchoMaximo As Double, _
         ByVal dblMetrosRetazoInicial As Double, ByVal dblMetrosRetazoFinal As Double, _
         ByVal dblInicioOtroRetazo As Double, ByVal dblFinalOtroRetazo As Double, _
         ByVal blnObservar As Boolean, ByVal intCortadora As Integer, _
         ByVal strUsuario As String, ByVal strPiezaUnida As Boolean, _
         Optional ByVal dblPeso As Double = 0, Optional ByVal dblPesoTeorico As Double = 0, _
         Optional ByVal strClasificacion As String = "", Optional ByVal strTipoDefecto As String = "", _
         Optional ByVal strValorDefecto As String = "", Optional ByVal strAnchoReal As Double = 0, _
         Optional ByVal strObservacion As String = "") As DataTable
            Try
                m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim objParametros As Object() = {"var_CodigoPlanilla", strCodigoPlanilla, _
                "var_CodigoFicha", strCodigoFicha, _
                 "num_MetrajeInicial", dblMetrajeInicial, _
                 "num_MetrajeFinal", dblMetrajeFinal, _
                 "num_AnchoMinimo", dblAnchoMinimo, _
                 "num_AnchoMaximo", dblAnchoMaximo, _
                 "num_MetrosRetazoInicial", dblMetrosRetazoInicial, _
                 "num_MetrosRetazoFinal", dblMetrosRetazoFinal, _
                 "num_InicioRetazo", dblInicioOtroRetazo, _
                 "num_FinalRetazo", dblFinalOtroRetazo, _
                 "bit_Observado", blnObservar, _
                 "var_CodigoUsuario", strUsuario, _
                 "var_CodigoPieza", strCodigoPieza, _
                 "num_Peso", dblPeso, _
                 "num_PesoTeorico", dblPesoTeorico, _
                 "var_Clasificacion", strClasificacion, _
                 "var_CodigoDefecto", strTipoDefecto, _
                 "var_ValorDefecto", strValorDefecto, _
                 "num_AnchoReal", strAnchoReal, _
                 "bit_PiezaUnida", strPiezaUnida, _
                 "var_Observacion", strObservacion, _
                 "int_Cortadora", intCortadora}

                Return m_sqlDtAccRevFin.ObtenerDataTable("usp_ins_GeneraRolloPlanillaCorte_3", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccRevFin = Nothing
            End Try
        End Function

        '---------------------------------------------------------------------------------------
        'Fecha: Nov 2016
        'Autor: Alexander Torres Cardenas
        'Modificacion: Se mejora la sp de nivelacion de tela
        '---------------------------------------------------------------------------------------
        Public Function ufn_NivelaPlanillaCorte(ByVal strCodigoFicha As String, _
         ByVal strCodigoPlanilla As String, ByVal dblMetrajeFinal As Double, ByVal dblAnchoMinimo As Double, _
         ByVal dblAnchoMaximo As Double, ByVal strUsuario As String) As DataTable
            Dim dstNivelar As DataSet
            Dim dtbNivelar As DataTable
            dtbNivelar = Nothing
            Try
                m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
                Dim objParametros As Object() = {"p_var_CodigoPlanilla", strCodigoPlanilla, _
                "p_var_CodigoFicha", strCodigoFicha, _
                  "p_num_MetrajeFinal", dblMetrajeFinal, _
                  "p_num_AnchoMinimo", dblAnchoMinimo, _
                  "p_num_AnchoMaximo", dblAnchoMaximo, _
                  "p_var_CodigoUsuario", strUsuario}
                'dstNivelar = m_sqlDtAccRevFin.ObtenerDataSet("usp_prc_NivelarPlanillaCorte", objParametros)
                dstNivelar = m_sqlDtAccRevFin.ObtenerDataSet("usp_revfin_PlanillaCorte_Nivelar", objParametros)
                dtbNivelar = dstNivelar.Tables(0)
            Catch ex As Exception
                Throw ex
            Finally
                m_sqlDtAccRevFin = Nothing
            End Try
            Return dtbNivelar
        End Function

        Public Function GenerarRollo(ByVal strCodigoFicha As String, ByVal dblMetrajeInicial As Double, _
         ByVal dblMetrajeFinal As Double, ByVal dblAnchoMinimo As Double, _
         ByVal dblAnchoMaximo As Double, ByVal dblMetrosRetazoInicial As Double, _
         ByVal dblMetrosRetazoFinal As Double, ByVal dblRetazoInicial As Double, _
         ByVal dblRetazoFinal As Double, ByVal blnObservar As Boolean, ByVal strUsuario As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoFicha, _
                  "metraje_inicial", dblMetrajeInicial, _
                  "metraje_final", dblMetrajeFinal, _
                  "ancho_minimo", dblAnchoMinimo, _
                  "ancho_maximo", dblAnchoMaximo, _
                  "metros_retazo_inicial", dblMetrosRetazoInicial, _
                  "metros_retazo_final", dblMetrosRetazoFinal, _
                  "retazo_inicial", dblRetazoInicial, _
                  "retazo_final", dblRetazoFinal, _
                  "observado", blnObservar, "usuario", strUsuario}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_GenerarRollo2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub GrabarGeneracionRollo()
            Try
                m_sqlDtAccRevFin.EjecutarComando("UP_GrabarGeneracionRollo")
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
            m_sqlDtAccRevFin.Dispose()
        End Sub

        Public Overloads Function EsRolloSinLote(ByVal intCodigoRollo As Integer) As Boolean
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"codigo_rollo", intCodigoRollo}

            Dim dtRollos As DataTable = adSQL.ObtenerDataTable("UP_ExisteRolloSinLote", objParametros)
            Return (dtRollos.Rows.Count = 0)
        End Function

        Public Overloads Function ObtenerRollosSinLote() As DataTable
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)

            Return adSQL.ObtenerDataTable("UP_ObtenerRollosSinLote")
        End Function

        Public Function ObtenerDatosEtiqueta(ByVal intCodigoRollo As Integer) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", intCodigoRollo}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosEtiqueta", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosReetiqueta(ByVal strCodigoRollo As String, ByVal strCodFicha As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo, "codigo_ficha", strCodFicha}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosReetiqueta", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerMetraje(ByVal intCodigoRollo As Integer) As Double
            Try
                Dim objParametros As Object() = {"codigo_rollo", intCodigoRollo}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("UP_ObtenerMetrajeRollo", objParametros), Double)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Anular(ByVal strCodigoRollo As String, ByVal strUsuario As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo, "usuario", strUsuario}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_AnularRollo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function AnularRolloEnCorte(ByVal strCodigoRollo As String, ByVal strUsuario As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo, "usuario", strUsuario}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_AnularRollo_PlanillaCorte", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ultimo_rollo_metraje(ByVal ficha As String) As DataTable
            Try
                Dim objParametros As Object() = {"CODIGO_FICHA", ficha}

                Return m_sqlDtAccRevFin.ObtenerDataTable("RVF_ULTIMO_ROLLO_METRAJE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtieneTipoTela_por_Rollo(ByVal pCodigoRollo As String) As String
            Try
                Dim objParametros As Object() = {"codigo_rollo", pCodigoRollo}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("pr_ObtieneTipoTelaPorRollo", objParametros), String)
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function

        Public Function VerificaPesoRolloValido(ByVal pCodigoRollo As String, ByVal pVerificaLote As Boolean) As String
            Try
                Dim objParametros As Object() = {"pvch_CodigoRollo", pCodigoRollo, _
                         "pvch_VerificaLote", pVerificaLote}

                Return CType(m_sqlDtAccRevFin.ObtenerValor("usp_RVF_VerificaPesoRollo", objParametros), String).Trim
            Catch ex As Exception
                Return "ERROR: " + ex.Message
            End Try

        End Function

        'Adicionado por Darwin Ccorahua
        Public Function ObtenerRollos_x_Ficha(ByVal pNumeroFicha As String, ByVal pEstadoTpp As String, ByRef dtData As DataTable) As Boolean
            Dim bResultado As Boolean = False
            Try
                Dim objParametros As Object() = {"pvch_NumeroFicha", pNumeroFicha, _
                         "pvch_EstadoTpp", pEstadoTpp}

                dtData = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_ROLLOS_X_FICHA", objParametros)
                bResultado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bResultado
        End Function

        Public Function ListarAreas_Defectos(ByRef dtData As DataTable) As Boolean
            Dim bResultado As Boolean = False
            Try
                dtData = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_AREAS_DEFECTOS")
                bResultado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bResultado
        End Function

        Public Function ListarDefectos(ByVal pSeccion As String, ByRef dtData As DataTable) As Boolean
            Dim bResultado As Boolean = False
            Try
                Dim objParametros As Object() = {"pvch_Seccion", pSeccion}
                dtData = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_LISTAR_DEFECTOS", objParametros)
                bResultado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bResultado
        End Function

        Public Function AsignarTpp_Rollos(ByVal pRollos As String, ByVal pEstadoTPP As String, ByVal pCodigoDefecto As String, ByVal pValorDefecto As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False
            Try
                Dim objParametros As Object() = {"pvch_NumeroRollo", pRollos, _
                         "pvch_EstadoTPP", pEstadoTPP, _
                         "pvch_CodigoDefecto", pCodigoDefecto, _
                         "pvch_ValorDefecto", pValorDefecto, _
                         "pvch_Usuario", pUsuario}
                m_sqlDtAccRevFin.EjecutarComando("USP_RVF_CLASIFICAROLLO", objParametros)
                bResultado = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bResultado
        End Function


#End Region

        '---------------------------------------------------------------------------------------
        'Fecha: Octubre 2015
        'Autor: Alexander Torres Cardenas
        'Modificacion: Se mejora la gui y sp para la revision de tela
        '---------------------------------------------------------------------------------------

        Public Function fnc_FichaPiezas_UltimoRollos(ByVal strCodigoficha As String) As DataTable
            Dim dtbRollos As New DataTable
            Dim m_sqlDtAccRevFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            dtbRollos = Nothing
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigoficha}
                dtbRollos = m_sqlDtAccRevFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_ObtenerUltimosRollos", objParametros)
                'dtbRollos = m_sqlDtAccRevFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_ObtenerUltimosRollos_OB_PRUEBA", objParametros)
                Return dtbRollos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fnc_CalulaPuntosRollo(ByVal strCodigoficha As String, _
                                              ByVal dblMetrajeInicial As Double, _
                                              ByVal dblMetrajeFinal As Double, _
                                              ByVal dblMetrosRetazoInicial As Double, _
                                              ByVal dblMetrosRetazoFinal As Double, _
                                              ByVal dblAnchoReal As Double) As DataTable
            Dim dtbCalculoPuntos As New DataTable
            Dim m_sqlDtAccRevFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            dtbCalculoPuntos = Nothing
            Try
                Dim objParametros As Object() = {"vch_CodigoFicha", strCodigoficha, _
                                                 "num_MetrajeInicial", dblMetrajeInicial, _
                                                 "num_MetrajeFinal", dblMetrajeFinal, _
                                                 "num_MetrosRetazoInicial", dblMetrosRetazoInicial, _
                                                 "num_MetrosRetazoFinal", dblMetrosRetazoFinal, _
                                                 "num_AnchoReal", dblAnchoReal}
                dtbCalculoPuntos = m_sqlDtAccRevFinal.ObtenerDataTable("usp_revfin_PlanillaCorte_CalculaPuntos", objParametros)
                Return dtbCalculoPuntos
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "Luis Antezana"
        Public Function EstaRolloTransferido(ByVal intCodigoRollo As Integer) As Boolean
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"codigo_rollo", intCodigoRollo}

            Dim dtRollos As DataTable = adSQL.ObtenerDataTable("EsRolloAnulable", objParametros)
            Return (dtRollos.Rows.Count = 0)
        End Function

        Public Function ExisteRolloActivo(ByVal intCodigoRollo As Integer) As Boolean
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"codigo_rollo", intCodigoRollo}

            Dim dtRollos As DataTable = adSQL.ObtenerDataTable("ExisteRollo", objParametros)
            Return (dtRollos.Rows.Count = 0)
        End Function
        Public Function ClasificacionRolloPrimera(ByVal intCodigoRollo As Integer) As Integer
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"pvchRollo", intCodigoRollo}

            Dim dtRollos As DataTable = adSQL.ObtenerDataTable("USP_RVF_OBTENER_CLASIFICACION_ROLLO", objParametros)
            If dtRollos.Rows(0).Item("RESULTADO").ToString = "0" Then
                Return 0 'ES DE OTRA CALIDAD
            Else
                Return 1 'ES DE 1ERA CALIDAD
            End If
        End Function
        'REQSIS201900025 - DG - INI
        Public Function ValidaEstadoTransferido(ByVal intCodigoRollo As Integer) As Integer
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"codigo_rollo", intCodigoRollo}

            Dim dtRollos As DataTable = adSQL.ObtenerDataTable("USP_VALIDA_ESTADO_TRANSITO_ROLLO", objParametros)
            If dtRollos.Rows.Count > 0 Then
                Return 0
            Else
                Return 1
            End If
            'Return (dtRollos.Rows.Count = 0)
        End Function
        'REQSIS201900025 - DG - FIN
#End Region

        Public Function RelotizarRollo(ByVal intCodigoRollo As Integer, ByVal strUsuario As String) As Boolean
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objParametros() As Object = {"pvchRollo", intCodigoRollo, "pvchUsuario", strUsuario}
            Dim lblnPuedeLotizar As Boolean
            Dim dtRollos As DataTable = adSQL.ObtenerDataTable("usp_rvf_rollos_puederelotizar", objParametros)
            lblnPuedeLotizar = (CType(dtRollos.Rows(0).Item("PUEDELOTIZAR").ToString, Integer) = 1)
            Return lblnPuedeLotizar
        End Function
        Public Function ObtenerDatosRolloPorCodigoDevolucion2da(ByVal pstrCodigoRollo As String, ByVal pstrCodigoCliente As String) As DataSet
            Try
                Dim objParametros() As Object = {"pstrCodigoRollo", pstrCodigoRollo, _
                                                 "pstrCodigoCliente", pstrCodigoCliente}
                Return m_sqlDtAccOfiPlan.ObtenerDataSet("USP_RVF_OBTENER_DATOS_ROLLO_POR_CODIGO_DEVOLUCION_2DA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function


#Region "LUIS ALANOCA"

        ''' <summary>
        '''---------------------------------------------------------------------------------------
        '''   Autor: Luis Alanoca
        '''   Modificacion: verifica datos rollos (validacion impresion etiquetas)
        '''   Fecha: 10/05/2017
        '''---------------------------------------------------------------------------------------
        ''' </summary>
        ''' <param name="strCodigoRollo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function fnc_VerificaDatosRollo(ByVal strCodigoRollo As String, ByVal strOpcion As String) As DataTable
            Dim dtbRollos As New DataTable
            'Dim m_sqlDtAccRevFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            'dtbRollos = Nothing
            Try
                Dim objParametros As Object() = {"pvch_CodigoRollo", strCodigoRollo,
                                                 "pvch_Opcion", strOpcion}

                dtbRollos = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_VERIFICA_DATOS_ROLLO", objParametros)
                Return dtbRollos
            Catch ex As Exception
                Throw ex
            Finally
                dtbRollos = Nothing
            End Try
        End Function


        ''' <summary>
        '''---------------------------------------------------------------------------------------
        '''   Autor: Luis Alanoca
        '''   Modificacion: Actualiza Contador de Etiquetas y estado de 2da etiqueta
        '''   Fecha: 11/05/2017
        '''---------------------------------------------------------------------------------------
        ''' </summary>
        ''' <param name="strCodigoRollo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ActualizaContadorEtiquetas(ByVal strCodigoRollo As String, ByVal strUsuario As String) As Integer
            Dim intResult As Integer
            Try
                Dim objParametros As Object() = {"pvch_CodigoRollo", strCodigoRollo,
                                                 "pvch_Usuario", strUsuario}
                intResult = m_sqlDtAccRevFin.EjecutarComando("USP_RVF_ACTUALIZAR_CONTADOR_IMPRESION_ROLLO", objParametros)
                Return intResult

            Catch ex As Exception
                Throw ex
            End Try

        End Function


        ''' <summary>
        '''---------------------------------------------------------------------------------------
        '''   Autor: Luis Alanoca
        '''   Modificacion: verifica datos rollos (validacion impresion etiquetas)
        '''   Fecha: 10/05/2017
        '''---------------------------------------------------------------------------------------
        ''' </summary>
        ''' <param name="strCodigoRollo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function fnc_VerificaDatosRolloCliente(ByVal strCodigoRollo As String, ByVal strOpcion As String, ByVal strCodigoCliente As String) As DataTable
            Dim dtbRollos As New DataTable
            'Dim m_sqlDtAccRevFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            'dtbRollos = Nothing
            Try
                Dim objParametros As Object() = {"pvch_CodigoRollo", strCodigoRollo,
                                                 "pvch_Opcion", strOpcion,
                                                 "pvch_CodigoCliente", strCodigoCliente}

                dtbRollos = m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_VERIFICA_DATOS_ROLLO_CLIENTE", objParametros)

                Return dtbRollos
            Catch ex As Exception
                Throw ex
            Finally
                dtbRollos = Nothing
            End Try
        End Function

#End Region
#Region "DAVID GAMARRA"
        Public Function CargarMotivosAnulacion(ByVal strPerfil As String) As DataTable
            Dim dtMotivo As New DataTable
            Dim m_sqlDtAccRevFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            dtMotivo = Nothing
            Try
                Dim objParametros As Object() = {"vch_Perfil", strPerfil}
                dtMotivo = m_sqlDtAccRevFinal.ObtenerDataTable("USP_RVF_MOTIVOS_ANULACION_POR_PERFIL", objParametros)
                Return dtMotivo
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function OtenerMetrajeParametrico() As Double
            Dim dt As New DataTable
            Dim m_sqlDtAccRevFinal As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            dt = Nothing
            Try
                dt = m_sqlDtAccRevFinal.ObtenerDataTable("USP_OBTENER_VALOR_METRAJE_PARAMETRICO_POR_ROLLO")
                Return CType(dt.Rows(0).Item(0), Double)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function Anular(ByVal strCodigoRollo As String, ByVal strUsuario As String, ByVal strIdMotivo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo, "usuario", strUsuario, "idMotivo", strIdMotivo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_AnularRollo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function AnularRolloEnCorte(ByVal strCodigoRollo As String, ByVal strUsuario As String, ByVal strIdMotivo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", strCodigoRollo, "usuario", strUsuario, "idMotivo", strIdMotivo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_AnularRollo_PlanillaCorte", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'CAMBIO PESO DG  - INI
        Public Function ValidaPrimerRolloYArticulo(ByVal strFicha As String, ByVal strArticulo As String) As DataTable
            Try
                Dim objParametros As Object() = {"vch_ficha", strFicha, "vch_articulo", strArticulo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_VALIDA_PRIMER_ROLLO_Y_ARTICULO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerCorreoEnvio() As DataTable
            Try
                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_OBTENER_CORREO_ENVIO_ARTICULO")
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'CAMBIO PESO DG  - FIN
#End Region
#Region "LUIS_AJ - VENTAS"
        Public Function ObtenerDatosEtiquetaCliente(ByVal intCodigoRollo As String, ByVal strCodCliente As String) As DataTable
            Try
                Dim objParametros As Object() = {"pvch_CodigoRollo", intCodigoRollo,
                                                 "pvch_CodigoCliente", strCodCliente}
                Return m_sqlDtAccRevFin.ObtenerDataTable("USP_RVF_OBTENERDATOS_ETIQUETA_CLIENTE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerDatosEtiquetaNM(ByVal intCodigoRollo As Integer) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_rollo", intCodigoRollo}
                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerDatosEtiqueta_NM", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace