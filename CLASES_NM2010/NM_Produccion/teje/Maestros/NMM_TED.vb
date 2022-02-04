Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NMM_TED
        Private obj As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Private _objConexion As AccesoDatosSQLServer
        Friend objGen As New NM_Consulta
        Public objEngomadora As New NM_Engomadora
        Public objUrdimbre As New NMM_Urdimbre
        Public CodigoTED As String
        Public RevisionTED As Integer
        Public CodigoUrdimbre As String
        Public CodigoArea As String
        Public TemperaturaBano As Double
        Public TemperaturaCoccion As Double
        Public TiempoCoccion As Double
        Public Humedad As Single
        Public PresionExprimido As Double
        Public TemperaturaBatea As Double
        Public PickupSeco As Double
        Public Regulacion As Integer
        Public CampoDivisor As Integer
        Public FuerzaPlanchado As Double
        Public HumedadHilo As Double
        Public Velocidad As Double
        Public TensionSobreAlimentacion As Double
        Public TensionPlegado As Double
        Public FuerzaCompensador As Double
        Public EstirajeProduccion As Double
        Public PresionAlimentacion As Double
        Public CodigoMaquina As String
        Public ConsumoPostEncerado As Double
        Public PickupTenido As Double
        Public dtFormulacion As New DataTable
        Public Usuario As String

        Sub New(ByVal sCodigoTED As String)
            CodigoTED = sCodigoTED
            Seek(sCodigoTED)
            Dim objFormu As New NMM_Formulacion
            dtFormulacion = objFormu.List(sCodigoTED, "ENGTED", True)
        End Sub

        Sub New()
            CodigoTED = ""
            RevisionTED = 0
            CodigoUrdimbre = ""
            CodigoArea = ""
            TemperaturaBano = 0
            TemperaturaCoccion = 0
            TiempoCoccion = 0
            PresionExprimido = 0
            TemperaturaBatea = 0
            PickupSeco = 0
            Regulacion = 0
            CampoDivisor = 0
            FuerzaPlanchado = 0
            HumedadHilo = 0
            Velocidad = 0
            TensionSobreAlimentacion = 0
            TensionPlegado = 0
            FuerzaCompensador = 0
            EstirajeProduccion = 0
            PresionAlimentacion = 0
            CodigoMaquina = ""
            ConsumoPostEncerado = 0
            PickupTenido = 0
            Usuario = ""
        End Sub

        Public Function Add( _
                                ByVal v_codigo_maquina As String, ByVal v_codigo_ted As String, ByVal v_revision_ted As Integer, _
                                ByVal v_codigo_urdimbre As String, ByVal v_temperatura_bano As Double, ByVal v_temperatura_coccion As Double, _
                                ByVal v_tiempo_coccion As Double, ByVal v_humedad As Integer, ByVal v_presion_exprimido As Double, _
                                ByVal v_temperatura_batea As Double, ByVal v_pickup_seco As Double, ByVal v_regulacion_desenrrollamiento As Integer, _
                                ByVal v_campo_divisor As Double, ByVal v_fuerza_planchado As Double, ByVal v_humedad_hilo As Integer, _
                                ByVal v_velocidad As Double, ByVal v_Vel_Marcha_Lenta As Double, ByVal v_Vel_Marcha_Lenta_Asignado As Double, _
                                ByVal v_Vel_Marcha_Rapida As Double, ByVal v_Vel_Marcha_Rapida_Asignada As Double, ByVal v_tension_sobrealimentacion As Double, _
                                ByVal v_tension_plegado As Double, ByVal v_fuerza_compensador As Double, ByVal v_estiraje_produccion As Double, _
                                ByVal v_presion_alimentacion As Double, ByVal v_consumo_post_encerado As Integer, ByVal v_pickup_tenido As Double, _
                                ByVal v_usuario_creacion As String, ByVal v_usuario_modificacion As String, ByVal strRevisionUrdimbre As Integer, ByVal v_codigo_area As String, ByVal strFlag As Integer) As Boolean
            Try
                Dim Parametros() As Object = {"codigo_maquina", v_codigo_maquina, _
                                     "codigo_ted", v_codigo_ted, "revision_ted", v_revision_ted, "codigo_urdimbre", v_codigo_urdimbre, _
                                     "temperatura_bano", v_temperatura_bano, "temperatura_coccion", v_temperatura_coccion, "tiempo_coccion", v_tiempo_coccion, _
                                     "humedad", v_humedad, "presion_exprimido", v_presion_exprimido, "temperatura_batea", v_temperatura_batea, _
                                     "pickup_seco", v_pickup_seco, "regulacion_desenrrollamiento", v_regulacion_desenrrollamiento, _
                                     "campo_divisor", v_campo_divisor, "fuerza_planchado", v_fuerza_planchado, "humedad_hilo", v_humedad_hilo, _
                                     "velocidad", v_velocidad, "Vel_Marcha_Lenta", v_Vel_Marcha_Lenta, "Vel_Marcha_Lenta_Asignado", v_Vel_Marcha_Lenta_Asignado, _
                                     "Vel_Marcha_Rapida", v_Vel_Marcha_Rapida, "Vel_Marcha_Rapida_Asignada", v_Vel_Marcha_Rapida_Asignada, _
                                     "tension_sobrealimentacion", v_tension_sobrealimentacion, "tension_plegado", v_tension_plegado, _
                                     "fuerza_compensador", v_fuerza_compensador, "estiraje_produccion", v_estiraje_produccion, "presion_alimentacion", v_presion_alimentacion, _
                                     "consumo_post_encerado", v_consumo_post_encerado, "pickup_tenido", v_pickup_tenido, "usuario_creacion", v_usuario_creacion, _
                                     "usuario_modificacion", v_usuario_modificacion, "revision_urdimbre", strRevisionUrdimbre, "codigo_area", v_codigo_area, "flagestado", strFlag}
                obj.EjecutarComando("USP_TEJ_NM_MA_TED_INSERTAR", Parametros)
                Return True
            Catch ex As Exception
                Return False
                Throw ex
            End Try
            
        End Function

        Public Function Delete(ByVal sCodigoTED As String) As Boolean
            Dim sql As String
            Try
                If sCodigoTED <> "" Then
                    sql = "Delete from NM_MA_TED where codigo_ted = '" & sCodigoTED & "' "
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function DeleteAll(ByVal sCodigoTED As String) As Boolean
            Dim sql As String
            Try
                If sCodigoTED <> "" Then
                    sql = "Delete from NM_MA_Formulacion where codigo_ted='" & sCodigoTED & "' "
                    objGen.Execute(sql)

                    sql = "Delete from NM_MA_Tinas where codigo_ted='" & sCodigoTED & "' "
                    objGen.Execute(sql)

                    sql = "Delete from NM_MA_TED where codigo_ted = '" & sCodigoTED & "' "
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim sql As String
            Try
                If CodigoTED <> "" AndAlso CodigoUrdimbre <> "" Then
                    If objUrdimbre.Exist(CodigoUrdimbre) = True Then
                        sql = "Update NM_MA_TED set codigo_urdimbre = '" & CodigoUrdimbre & "', " & _
                       "codigo_area = '" & CodigoArea & " ', temperatura_bano = " & TemperaturaBano & _
                       ", temperatura_coccion = " & TemperaturaCoccion & ", tiempo_coccion = " & TiempoCoccion & "," & _
                       "presion_exprimido = " & PresionExprimido & ", temperatura_batea = " & TemperaturaBatea & _
                       ", pickup_seco = " & PickupSeco & ", regulacion_desenrrollamiento = " & Regulacion & _
                       ", campo_divisor = " & CampoDivisor & ", fuerza_planchado = " & FuerzaPlanchado & ", " & _
                       "humedad_hilo = " & HumedadHilo & ",velocidad = " & Velocidad & ", tension_sobrealimentacion = " & TensionSobreAlimentacion & _
                       ", tension_plegado = " & TensionPlegado & ", fuerza_compensador = " & FuerzaCompensador & _
                       ", estiraje_produccion = " & EstirajeProduccion & ", presion_alimentacion = " & PresionAlimentacion & ", " & _
                       "codigo_maquina = '" & CodigoMaquina & "', consumo_post_encerado = " & ConsumoPostEncerado & ", " & _
                       "pickup_tenido = " & PickupTenido & ", revision_TED = revision_TED + 1, " & _
                       " usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                       " where codigo_ted='" & CodigoTED & "' "
                        Return objGen.Execute(sql)
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function List() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_MA_TED"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function List(ByVal v_Codigo_Urdimbre As String) As DataTable
            Dim objDT As New DataTable
            Try
                Dim Params() As Object = {"codigo_urdimbre", v_Codigo_Urdimbre}
                objDT = obj.ObtenerDataTable("USP_TEJ_NM_MA_TED_GET_URDIMBRE", Params)
                Return objDT
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsumoDenim_Listar(ByVal strFechaInicio As String, ByVal strFechaFinal As String, ByVal strOpcion As String) As DataSet
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim Params() As Object = {"FechaINI", strFechaInicio, "FechaFIN", strFechaFinal, "Var_Opcion", strOpcion}
                Return _objConexion.ObtenerDataSet("USP_PRO_InsumosArticulos_Listar", Params)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Seek(ByVal v_Codigo_Ted As String) As DataTable
            Dim objDT As New DataTable
            Try
                Dim Params() As Object = {"codigo_ted", v_Codigo_Ted}
                objDT = obj.ObtenerDataTable("USP_TEJ_NM_MA_TED_GET_TED", Params)
                Return objDT
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Actualiza_Urdimbre( _
                                ByVal v_codigo_maquina As String, ByVal v_codigo_ted As String, ByVal v_revision_ted As Integer, _
                                ByVal v_codigo_urdimbre As String, ByVal v_temperatura_bano As Double, ByVal v_temperatura_coccion As Double, _
                                ByVal v_tiempo_coccion As Double, ByVal v_humedad As Integer, ByVal v_presion_exprimido As Double, _
                                ByVal v_temperatura_batea As Double, ByVal v_pickup_seco As Double, ByVal v_regulacion_desenrrollamiento As Integer, _
                                ByVal v_campo_divisor As Double, ByVal v_fuerza_planchado As Double, ByVal v_humedad_hilo As Integer, _
                                ByVal v_velocidad As Double, ByVal v_Vel_Marcha_Lenta As Double, ByVal v_Vel_Marcha_Lenta_Asignado As Double, _
                                ByVal v_Vel_Marcha_Rapida As Double, ByVal v_Vel_Marcha_Rapida_Asignada As Double, ByVal v_tension_sobrealimentacion As Double, _
                                ByVal v_tension_plegado As Double, ByVal v_fuerza_compensador As Double, ByVal v_estiraje_produccion As Double, _
                                ByVal v_presion_alimentacion As Double, ByVal v_consumo_post_encerado As Integer, ByVal v_pickup_tenido As Double, _
                                ByVal v_usuario_creacion As String, ByVal v_usuario_modificacion As String, ByVal v_revision_urdimbre As Integer, _
                                ByVal v_codigo_area As String, ByVal v_flagestado As Integer) As Boolean
            Try
                Dim Parametros() As Object = {"codigo_maquina", v_codigo_maquina, _
                                     "codigo_ted", v_codigo_ted, "revision_ted", v_revision_ted, "codigo_urdimbre", v_codigo_urdimbre, _
                                     "temperatura_bano", v_temperatura_bano, "temperatura_coccion", v_temperatura_coccion, "tiempo_coccion", v_tiempo_coccion, _
                                     "humedad", v_humedad, "presion_exprimido", v_presion_exprimido, "temperatura_batea", v_temperatura_batea, _
                                     "pickup_seco", v_pickup_seco, "regulacion_desenrrollamiento", v_regulacion_desenrrollamiento, _
                                     "campo_divisor", v_campo_divisor, "fuerza_planchado", v_fuerza_planchado, "humedad_hilo", v_humedad_hilo, _
                                     "velocidad", v_velocidad, "Vel_Marcha_Lenta", v_Vel_Marcha_Lenta, "Vel_Marcha_Lenta_Asignado", v_Vel_Marcha_Lenta_Asignado, _
                                     "Vel_Marcha_Rapida", v_Vel_Marcha_Rapida, "Vel_Marcha_Rapida_Asignada", v_Vel_Marcha_Rapida_Asignada, _
                                     "tension_sobrealimentacion", v_tension_sobrealimentacion, "tension_plegado", v_tension_plegado, _
                                     "fuerza_compensador", v_fuerza_compensador, "estiraje_produccion", v_estiraje_produccion, "presion_alimentacion", v_presion_alimentacion, _
                                     "consumo_post_encerado", v_consumo_post_encerado, "pickup_tenido", v_pickup_tenido, "usuario_creacion", v_usuario_creacion, _
                                     "usuario_modificacion", v_usuario_modificacion, "revision_urdimbre", v_revision_urdimbre, "codigo_area", v_codigo_area, "flagestado", v_flagestado}
                obj.EjecutarComando("USP_TEJ_NM_TED_UPDATE", Parametros)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Exist(ByVal Codigo As String) As Boolean
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_MA_TED where codigo_ted = '" & Codigo & "' "
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function
    End Class
End Namespace