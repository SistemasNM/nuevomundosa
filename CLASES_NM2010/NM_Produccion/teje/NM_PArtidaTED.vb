Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    'clase que hereda de NM_PartidaengomadoTED
    Public Class NM_PArtidaTED
        Inherits NM_PArtidaEngomadoYTED

        Private objUtil As New NM_General.Util

        Private _objConn As AccesoDatosSQLServer

        Public Sub New()
            _objConn = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Public Sub EliminarTemporalPlegadoresReserva()
            Try
                Dim strSQL = "DELETE FROM UTB_PartidasPlegadoresReservaTemporal"
                BD.Execute(strSQL)
            Catch 'ex As Exception
                Throw
            End Try
        End Sub

        Public Function cargarPlegadoresReservaxPartida(ByVal pcodPartidaEngomado As String, ByVal pCodUrdimbre As String, ByVal pCodTipo As String, ByVal pCodPlanta As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "ptin_tipolista", "3", _
                "pvch_parametro1", pcodPartidaEngomado, _
                "pvch_parametro2", pCodUrdimbre, _
                "pvch_parametro3", pCodTipo, _
                "pvch_parametro4", pCodPlanta}
                pdtb_datos = _objConn.ObtenerDataTable("usp_tej_partidaengomadoyted_listar_v2", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

        Public Function restaurarRegistroPlegadores(ByVal pCodTelar As String, ByVal pcodPartidaEngomado As String, ByVal pcodPlegador As String, ByVal pcodPiezaIni As String, ByVal pcodOrden As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "p_codigo_telar", pCodTelar, _
                "p_codigo_partida", pcodPartidaEngomado, _
                "p_codigo_plegador", pcodPlegador, _
                "p_codigo_pieza_ini", pcodPiezaIni, _
                "p_codigo_orden", pcodOrden}
                pdtb_datos = _objConn.ObtenerDataTable("USP_TEJ_RESTAURAR_REGISTRO_PLEGADORES", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

        Public Function asociarOPXPlegadores(ByVal pcodPartidaEngomado As String, ByVal pcodPlegador As String, ByVal pcodPiezaIni As String, ByVal pcodOrden As String, ByVal pCodUsuario As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "p_codigo_partida", pcodPartidaEngomado, _
                "p_codigo_plegador", pcodPlegador, _
                "p_codigo_pieza_ini", pcodPiezaIni, _
                "p_codigo_orden", pcodOrden, _
                "p_usuario", pCodUsuario}
                pdtb_datos = _objConn.ObtenerDataTable("USP_TEJ_INSERTAR_OP_PLEGADORES_RESERVA", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

        Public Function asociarOPXPlegadores_2(ByVal pCodTelar As String, ByVal pcodPartidaEngomado As String, ByVal pcodPiezaIni As String, ByVal pcodPiezaFin As String, ByVal pcodPlegador As String, ByVal pIntLongitudV As Integer, ByVal pcodOrden As String, ByVal pCodArticulo As String, ByVal pCodPlanta As String, ByVal pCodUsuario As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "p_codigo_telar", pCodTelar, _
                "p_codigo_partida", pcodPartidaEngomado, _
                "p_codigo_plegador", pcodPlegador, _
                "p_codigo_pieza_ini", pcodPiezaIni, _
                "p_codigo_pieza_fin", pcodPiezaFin, _
                "intLongitudV", pIntLongitudV, _
                "p_codigo_orden", pcodOrden, _
                "p_codigo_articulo", pCodArticulo, _
                "p_codigo_planta", pCodPlanta, _
                "p_usuario", pCodUsuario}
                pdtb_datos = _objConn.ObtenerDataTable("USP_TEJ_INSERTAR_OP_PLEGADORES_RESERVA_v2", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

        ' se modifico el query de busqueda para que devolviera a los codigos ted con las ultimas revisiones
        Public Function GetCodigosTED(ByVal pCod_sub_Part_Urd) As DataTable
            Dim db As New NM_Consulta
            Dim objDT As New DataTable
            Dim strql As String = "Select TE.codigo_ted, TE.revision_ted " & _
                "FROM NM_MA_TED TE join NM_PartidaUrdido PU " & _
                "ON TE.codigo_urdimbre = PU.codigo_urdimbre " & _
                "Join NM_ParticionPartidas PP " & _
                "ON PU.codigo_partida_urdido = PP.codigo_partida_urdido " & _
                "where PP.codigo_sub_partida_urdido = '" & pCod_sub_Part_Urd & "'"
            Try
                objDT = db.Query(strql)
            Catch ex As Exception
                objDT = Nothing
                Throw New Exception(ex.Message)
            End Try
            Return objDT
            db = Nothing
            objDT.Dispose()
        End Function

        Public Overrides Function loadDetalleCalidad(ByVal codPartidaEngomado As String) As DataTable
            MyBase.loadDetalleCalidad(codPartidaEngomado)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim strConsultaDetalleCalidad As String
            'sql = "SELECT pM.codigo_plegador, PM.Ocurrencias_madeja, pM.NHR_madeja, pM.otros, " & _
            '" pM.observaciones, vw.calificacion " & _
            '" FROM NM_PartidaEngomadoMCalidad pM " & _
            '" JOIN VW_NMPartidaEngomadoCalificacion vw " & _
            '" ON pM.codigo_partida_engomadoted = vw.codigo_partida_engomadoted " & _
            '" AND pM.codigo_plegador = vw.codigo_plegador " & _
            '" JOIN NM_PartidaEngomadoDProduccion PEDP " & _
            '" ON pM.codigo_plegador = PEDP.codigo_plegador " & _
            '" and pM.codigo_partida_engomadoted = PEDP.codigo_partida_engomadoted " & _
            '" where pM.codigo_partida_engomadoted='" & codPartidaEngomado & "' " & _
            '" order by PEDP.fecha_creacion "
            'objDT = objGen.Query(sql)
            Try
                Dim objParametros() As Object = {"vch_codPartidaEngomado", codPartidaEngomado}
                objDT = _objConn.ObtenerDataTable("USP_OBTENER_DETALLE_CALIDAD_PARTIDA_ENGOMADO", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
            Return objDT
        End Function

        'busca la partida ted que esta asociada a una partida de urdido
        Overrides Sub SeekXPartidaUrdido(ByVal pcodPartidaUrdido As String)
            MyBase.SeekXPartidaUrdido(pcodPartidaUrdido)
            dtCalidad = loadDetalleCalidad(codPartidaEngomado)
        End Sub

        'busca la partida de ted que corresponde al codigo de partida
        Overrides Sub Seek(ByVal pcodPartidaEngomado As String)
            MyBase.Seek(pcodPartidaEngomado)
            dtCalidad = loadDetalleCalidad(pcodPartidaEngomado)
        End Sub

        Public Overloads Sub insertar(ByVal pcodPartida As String, ByVal codPartUrdido As String, ByVal pHorInicio As String, ByVal pHoraFin As String, ByVal codformTED As String, _
        ByVal pMaquina As String, ByVal pcolor As String, ByVal pfecEngomado As Date, ByVal pmetroEngomados As Double, ByVal pDesperdicio As Double, ByVal revisionformTEd As Integer, ByVal pUserCrea As String, ByVal pfecCrea As Date)
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            codMaquina = pMaquina
            codPartidaEngomado = GeneraCodigoPartida()
            strsql = "INSERT INTO NM_PartidaEngomadoYTED (" & _
            " codigo_partida_engomadoted, codigo_partida_urdido," & _
            " hora_inicio,codigo_TED,codigo_maquina,fecha_engomado," & _
            " hora_fin, total_engomado,desperdicio,revision_ted," & _
            " tipo,estado,usuario_creacion,fecha_creacion) " & _
            " values ('" & codPartidaEngomado & "','" & _
            codPartUrdido & "','" & pHorInicio & "','" & codformTED & _
            "','" & pMaquina & "','" & objUtil.FormatFecha(pfecEngomado) & _
            "','" & pHoraFin & "'," & pmetroEngomados & "," & _
            pDesperdicio & "," & revisionformTEd & ",1,'1','" & _
            pUserCrea & "','" & objUtil.FormatFecha(pfecCrea) & "')"
            objDT = objGen.Query(strsql)
            InicializarDatosReceta(codformTED, revisionformTEd, codPartidaEngomado, 1) 'Pretratamiento
            InicializarDatosReceta(codformTED, revisionformTEd, codPartidaEngomado, 2) 'Tenido
            InicializarDatosReceta(codformTED, revisionformTEd, codPartidaEngomado, 3) 'engomado
        End Sub

        Public Overloads Sub Actualizar(ByVal pfecha_inicio As Date, ByVal pHoraInicio As String, ByVal pFechafin As Date, ByVal pcodTED As String, _
        ByVal pRevTED As String, ByVal pMaquina As String, ByVal pHoraFinal As String, _
        ByVal MetTotales As Double, ByVal MetDesperdic As Double, ByVal pColor As String)
            Dim strsql As String
            Dim objGen As New NM_Consulta
            strsql = "UPDATE NM_PartidaEngomadoYTED set " & _
            "hora_inicio = '" & pHoraInicio & "', " & _
            "fecha_engomado = '" & objUtil.FormatFecha(pfecha_inicio) & "', " & _
            "fecha_fin = '" & objUtil.FormatFecha(pFechafin) & "', " & _
            "codigo_ted = '" & pcodTED & "', " & _
            "revision_ted='" & pRevTED & "', " & _
            "codigo_maquina ='" & pMaquina & "'," & _
            "hora_fin= '" & pHoraFinal & "'," & _
            "total_engomado = " & MetTotales & "," & _
            "desperdicio= " & MetDesperdic & "," & _
            "color = '" & pColor & "'" & _
            " where codigo_partida_engomadoted='" & codPartidaEngomado & "'"
            Try
                If Not objGen.Query(strsql) Is Nothing Then
                    Throw New Exception("No existe registro con el codigo de partida ingresado")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#Region "Recetas de insumos quimicos"
        Private Sub InicializarDatosReceta(ByVal pcodTed As String, ByVal srevTEd As Integer, ByVal pCodigo_partida_engomadoted As String, ByVal pCodfase As Integer)
            Dim objDatosReceta As New NM_DatosRecetaTED
            'objDatosReceta.codPartEngomadoTED = pCodigo_partida_engomadoted
            'objDatosReceta.codFase = pCodfase
            'Se debe enviar el codigo TED para que busque el codigo de receta respectiva
            'objDatosReceta.codReceta = objDatosReceta.GetReceta(pcodTed, srevTEd, pCodfase)
            '   Throw New Exception(pCodigo_partida_engomadoted & "," & pcodTed & "," & srevTEd & "," & pCodfase)
            objDatosReceta.InsertarDatosReceta(pCodigo_partida_engomadoted, pcodTed, srevTEd, pCodfase)
            'objDatosReceta.InsertarDatosReceta(pCodigo_partida_engomadoted, pcodTed, srevTEd)
            'objDatosReceta.Insertar()
        End Sub
#End Region

#Region " Produccion "
        Public Overrides Sub AgregarProduccion(ByVal codPartidEngoProduct As String, ByVal plegador As String, ByVal lado As String, _
                ByVal longitud As Integer, ByVal cantPiezas As Integer, ByVal Operario As String, _
                ByVal ocurrencia As String, ByVal pCodSupervisor As String, _
                ByVal pfechaInicio As String, ByVal pfechaFinal As String, ByVal pCodigoMaquina As String, ByVal pUsuario As String, ByVal pObsvPlegador As String)

            Dim objCalidad As New NM_PartidaEngomadoMCalidad
            Dim objRotFiletas As New NM_RoturasFiletaTED
            MyBase.AgregarProduccion(codPartidEngoProduct, plegador, lado, longitud, cantPiezas, Operario, ocurrencia, pCodSupervisor, pfechaInicio, pfechaFinal, pCodigoMaquina, pUsuario, pObsvPlegador)
            objCalidad.Codigo_partida_engomado = codPartidEngoProduct
            objCalidad.Codigo_plegador = plegador
            objRotFiletas.codigo_partida_engomadoted = codPartidEngoProduct
            objRotFiletas.Codigo_plegador = plegador
            Try
                objCalidad.Insertar()
                objCalidad.InicializarRoturas()
                ' Se inserta una fileta con los datos vacios
                objRotFiletas.insert()
                ' Calcula la calificacion
                codPartidaEngomado = codPartidEngoProduct
                CalcularCalificacion(plegador)

            Catch ex As Exception
                Throw New Exception("Error al insertar NM_PartidaEngomadoMCalidad: " & ex.Message)
            End Try
            dtProduccion = loadDetalleProduccion(codPartidEngoProduct)
            dtCalidad = loadDetalleCalidad(codPartidEngoProduct)
        End Sub

        Public Function EliminarProduccionTED(ByVal codPartidEngoProduct As String, ByVal plegador As String) As String
            Dim PartEngomProduccion As New NM_PartidaEngomadoDProduccion
            Dim objPECorrelativo As New NM_PartidaEngomadoCorrelativo
            Dim objCalidad As New NM_PartidaEngomadoMCalidad
            Dim objDetCalidad As New NM_PartidaEngomadoDCalidad
            Dim objRotFiletas As New NM_RoturasFiletaTED
            Dim strResultado As String = ""

            Try
                If codPartidEngoProduct <> "" AndAlso plegador <> "" Then
                    ' AndAlso objPECorrelativo.Exist(codPartidEngoProduct, plegador) = False Then

                    'Se elimina las roturas que corresponde a al codigo de partida ted y el plegador
                    objRotFiletas.codigo_partida_engomadoted = codPartidEngoProduct
                    objRotFiletas.Codigo_plegador = plegador
                    If objRotFiletas.delete() = True Then
                        'Se elimina el maestro de partida de calidad para una partida de engomado y plegador
                        If objCalidad.Eliminar(codPartidEngoProduct, plegador) = True Then
                            ' Se elimina produccion
                            strResultado = EliminarProduccion(codPartidEngoProduct, plegador)
                        End If
                    End If
                    dtCalidad = loadDetalleCalidad(codPartidEngoProduct)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            PartEngomProduccion = Nothing
            Return strResultado
        End Function

#End Region

        Public Sub CalcularCalificacionTED(ByVal codPlegador As String)
            Dim objRotFiletas As New NM_RoturasFiletaTED
            ' Halla el total de defectos para los cabezales
            Dim result As Double = Calificacion(TotalDefectos(codPlegador), LongitudPlegador(codPlegador))
            ' Halla el total de defectos para las filetas
            objRotFiletas.getRoturas(codPartidaEngomado, codPlegador)
            result = result + objRotFiletas.GetTotal()
            ActualizarCalificacion(codPlegador, result)
            objRotFiletas = Nothing

        End Sub

        'Private Sub GenerarRegistroPiezas(ByVal pcantPiezas As Integer, ByVal pcodigoPartidaEngomado As String, ByVal plegador As String, ByVal pUserCrea As String, ByVal fecCrea As Date)  ' Se registra las piezas
        '    Dim objProduc As New NM_PartidaEngomadoDProduccion()
        '    objProduc.GenerarPiezas(pcantPiezas, 1, pcodigoPartidaEngomado, plegador, pUserCrea, fecCrea)
        '    objProduc = Nothing
        'End Sub

        Public Function listall() As DataTable
            Dim db As New NM_Consulta
            Return db.Query("SELECT * FROM NM_PArtidaEngomadoYTED where tipo = 1")
        End Function

        'funcion que lista las partidas que deben cerrarse para que pueda aperturarse otra
        'para el caso de engomado crudo solo es 1, solicitado x JCALDERON
        Public Function ListaPartidasxCerrar(ByRef pdtb_lista As DataTable, ByVal pint_tipoconsulta As Int16) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                Dim objParametros() As Object = {"pchr_tipopartida", "TED", _
                                                 "ptin_tipoconsulta", pint_tipoconsulta}
                pdtb_lista = _objConn.ObtenerDataTable("usp_tej_listapartidasxcerrar", objParametros)
                lbln_fncestado = True
            Catch Ex As Exception
                Throw Ex
            End Try
            Return lbln_fncestado
        End Function


        Public Function ubicacionPlegadores(ByVal pAccion As String, ByVal pZona As String, ByVal pUbic As String, ByVal pPartida As String, ByVal pPlegador As String, ByVal pEstado As String, ByVal pUsuario As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "p_vch_accion", pAccion, _
                "p_vch_zona", pZona, _
                "p_vch_ubic", pUbic, _
                "p_vch_partida", pPartida, _
                "p_vch_plegador", pPlegador, _
                "p_vch_estado", pEstado, _
                "p_vch_usuario", pUsuario}
                pdtb_datos = _objConn.ObtenerDataTable("USP_TEJ_ASOCIAR_UBICACION_PLEGADORES", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

        Public Function liberarManualPlegadores(ByVal pvchCodigoPlegadores As String, ByVal pvchUsuario As String, ByVal pvchValidacion As String) As DataTable
            Dim lbln_estado As Boolean = False
            Dim pdtb_datos As DataTable
            Try
                Dim lobjParametros() As Object = { _
                "pvchCodigosPlegadores", pvchCodigoPlegadores, _
                "pvchUsuario", pvchUsuario, _
                "pvchValidacion", pvchValidacion}
                pdtb_datos = _objConn.ObtenerDataTable("usp_tej_liberar_plegadores_manual", lobjParametros)
                lbln_estado = True
            Catch ex As Exception
                lbln_estado = False

            Finally
            End Try
            Return pdtb_datos

        End Function

    End Class

End Namespace