Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_TED
        Friend objConn As New NM_Consulta
        Public objEngomadora As New NM_Engomadora()
        Public objUrdimbre As New NM_Urdimbre()
        Public CodigoTED As String
        Public RevisionTED As Integer
        Public CodigoUrdimbre As String
        Public RevisionUrdimbre As Integer
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
        Public CodigoEngomadora As String
        Public ConsumoPostEncerado As Double
        Public PickupTenido As Double
        Public FlagEstado As Integer
        Public dtFormulacion As New DataTable()
        Public Usuario As String

        Sub New(ByVal sCodigoTED As String, ByVal nRevision As Integer, ByVal nEstado As Integer)
            CodigoTED = sCodigoTED
            Seek(sCodigoTED, nRevision, nEstado)
            Dim objFormu As New NM_Formulacion()
            dtFormulacion = objFormu.List(sCodigoTED, "ENGTED", True)
        End Sub

        Sub New()
            CodigoTED = ""
            RevisionTED = 0
            CodigoUrdimbre = ""
            RevisionUrdimbre = 0
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
            CodigoEngomadora = ""
            ConsumoPostEncerado = 0
            PickupTenido = 0
            Usuario = ""
        End Sub

        Public Function Add() As Boolean
            Dim sql As String
            Try
                If CodigoTED <> "" AndAlso CodigoUrdimbre <> "" Then
                    If objUrdimbre.Exist(CodigoUrdimbre) = True Then
                        sql = "Insert into NM_TED (" & _
                        "codigo_ted, revision_ted, flagestado, codigo_urdimbre, revision_urdimbre, " & _
                        "codigo_area, temperatura_bano, temperatura_coccion, tiempo_coccion," & _
                        "presion_exprimido, temperatura_batea, pickup_seco, " & _
                        "regulacion_desenrrollamiento, campo_divisor, fuerza_planchado, " & _
                        "humedad_hilo,velocidad, tension_sobrealimentacion, tension_plegado, " & _
                        "fuerza_compensador, estiraje_produccion, presion_alimentacion, " & _
                        "codigo_maquina, consumo_post_encerado, pickup_tenido, " & _
                        "usuario_creacion, fecha_creacion) values('" & CodigoTED & "'," & Val(RevisionTED) & _
                        "," & FlagEstado & ",'" & _
                        CodigoUrdimbre & "'," & RevisionUrdimbre & ", '" & CodigoArea & "'," & TemperaturaBano & "," & _
                        TemperaturaCoccion & "," & TiempoCoccion & "," & _
                        PresionExprimido & "," & TemperaturaBatea & "," & _
                        PickupSeco & "," & Regulacion & "," & CampoDivisor & ", " & _
                        FuerzaPlanchado & "," & HumedadHilo & "," & _
                        Velocidad & "," & TensionSobreAlimentacion & "," & _
                        TensionPlegado & "," & FuerzaCompensador & "," & _
                        EstirajeProduccion & "," & PresionAlimentacion & ",'" & _
                        CodigoEngomadora & "'," & ConsumoPostEncerado & "," & PickupTenido & ",'" & Usuario & "',getdate())"
                        Return objConn.Execute(sql)
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function GetRevision(ByVal pCodigoTED As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow, Rev As Integer = 0
            sql = "Select revision_ted from NM_MA_TED where codigo_ted='" & pCodigoTED & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Rev = fila("revision_ted")
            Next
            Return Rev
        End Function

        Function GetRevision(ByVal pCodigoTED As String, ByVal pFlag As Integer)
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow, Rev As Integer = 0
            sql = "Select revision_ted from NM_TED where codigo_ted='" & pCodigoTED & _
            "' and flagEstado = " & pFlag
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Rev = fila("revision_ted")
            Next
            Return Rev
        End Function

        Function SendHistory(ByVal pCodigoTed As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Update NM_TED set flagestado=0 where codigo_ted='" & _
            pCodigoTed & "' and flagestado=1"
            Return objConn.Execute(sql)
        End Function

        Public Function Delete(ByVal pCodigoTED As String, ByVal pRevision As Integer) As Boolean
            Dim sql As String
            Try
                If pCodigoTED <> "" Then
                    sql = "Delete from NM_TED where codigo_ted = '" & _
                    pCodigoTED & "' and revision_ted=" & pRevision
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function DeleteRevision(ByVal pCodigoTED As String) As Boolean
            Dim sql As String, rev As Integer = GetRevision(pCodigoTED, 2)
            Try
                If pCodigoTED <> "" Then
                    sql = "Delete from NM_Formulacion where codigo_ted='" & _
                    pCodigoTED & "' and revision_ted=" & rev
                    objConn.Execute(sql)

                    sql = "Delete from NM_Tinas where codigo_ted='" & _
                    pCodigoTED & "' and revision_ted=" & rev
                    objConn.Execute(sql)

                    sql = "Delete from NM_TED where codigo_ted = '" & _
                    pCodigoTED & "' and revision_ted=" & rev
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal pCodigoTED As String, ByVal pRevision As Integer, ByVal pEstado As Integer) As Boolean
            Dim sql As String
            Try
                If pCodigoTED <> "" Then
                    sql = "Delete from NM_TED where codigo_ted = '" & _
                    pCodigoTED & "' and revision_ted=" & pRevision & _
                    " and flagestado =" & pEstado
                    Return objConn.Execute(sql)
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
                        sql = "Update NM_TED set revision_urdimbre=" & RevisionUrdimbre & _
                        ",codigo_urdimbre = '" & CodigoUrdimbre & "', flagestado =" & FlagEstado & _
                        ",codigo_area = '" & CodigoArea & " ', temperatura_bano = " & TemperaturaBano & _
                        ", temperatura_coccion = " & TemperaturaCoccion & ", tiempo_coccion = " & TiempoCoccion & "," & _
                        "presion_exprimido = " & PresionExprimido & ", temperatura_batea = " & TemperaturaBatea & _
                        ", pickup_seco = " & PickupSeco & ", regulacion_desenrrollamiento = " & Regulacion & _
                        ", campo_divisor = " & CampoDivisor & ", fuerza_planchado = " & FuerzaPlanchado & ", " & _
                        "humedad_hilo = " & HumedadHilo & ",velocidad = " & Velocidad & ", tension_sobrealimentacion = " & TensionSobreAlimentacion & _
                        ", tension_plegado = " & TensionPlegado & ", fuerza_compensador = " & FuerzaCompensador & _
                        ", estiraje_produccion = " & EstirajeProduccion & ", presion_alimentacion = " & PresionAlimentacion & ", " & _
                        "codigo_maquina = '" & CodigoEngomadora & "', consumo_post_encerado = " & ConsumoPostEncerado & ", " & _
                        "pickup_tenido = " & PickupTenido & ", " & _
                        "usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                        " where codigo_ted='" & CodigoTED & "' and revision_ted = " & RevisionTED
                        Return objConn.Execute(sql)
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_TED where flagestado=1 order by codigo_ted"
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal pCodUrdimbre As String, ByVal pRevision As Integer, ByVal pEstado As Integer) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_TED where codigo_urdimbre='" & pCodUrdimbre & _
            "' and flagestado=" & pEstado & " and revision_urdimbre=" & pRevision
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal pCodUrdimbre As String, ByVal pEstado As Integer) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_TED where codigo_urdimbre='" & pCodUrdimbre & _
            "' and flagestado=" & pEstado & " "
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal Codigo As String, ByVal nRevision As Integer) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_TED where codigo_ted = '" & Codigo & "' and revision_ted=" & nRevision
            objDT = objConn.Query(sql)
            Return objDT

        End Function

        Sub Seek(ByVal Codigo As String, ByVal nRevision As Integer, ByVal nEstado As Integer)
            Dim sql As String, objDT As New DataTable
            Dim fila As DataRow
            sql = "Select * " & _
            " from NM_TED where codigo_ted = '" & Codigo & "' and revision_ted=" & _
            nRevision & " and flagestado =" & nEstado
            objDT = objConn.Query(sql)
            For Each fila In objDT.Rows
                If Not IsDBNull(fila.Item("revision_ted")) Then RevisionTED = fila.Item("revision_ted")
                If Not IsDBNull(fila.Item("codigo_urdimbre")) Then CodigoUrdimbre = fila.Item("codigo_urdimbre")
                If Not IsDBNull(fila.Item("revision_urdimbre")) Then RevisionUrdimbre = fila.Item("revision_urdimbre")
                If Not IsDBNull(fila.Item("codigo_area")) Then CodigoArea = fila.Item("codigo_area")
                If Not IsDBNull(fila.Item("temperatura_bano")) Then TemperaturaBano = fila.Item("temperatura_bano")
                If Not IsDBNull(fila.Item("temperatura_coccion")) Then TemperaturaCoccion = fila.Item("temperatura_coccion")
                If Not IsDBNull(fila.Item("tiempo_coccion")) Then TiempoCoccion = fila.Item("tiempo_coccion")
                If Not IsDBNull(fila.Item("presion_exprimido")) Then PresionExprimido = fila.Item("presion_exprimido")
                If Not IsDBNull(fila.Item("temperatura_batea")) Then TemperaturaBatea = fila.Item("temperatura_batea")
                If Not IsDBNull(fila.Item("pickup_seco")) Then PickupSeco = fila.Item("pickup_seco")
                If Not IsDBNull(fila.Item("regulacion_desenrrollamiento")) Then Regulacion = fila.Item("regulacion_desenrrollamiento")
                If Not IsDBNull(fila.Item("campo_divisor")) Then CampoDivisor = fila.Item("campo_divisor")
                If Not IsDBNull(fila.Item("fuerza_planchado")) Then FuerzaPlanchado = fila.Item("fuerza_planchado")
                If Not IsDBNull(fila.Item("humedad_hilo")) Then HumedadHilo = fila.Item("humedad_hilo")
                If Not IsDBNull(fila.Item("velocidad")) Then Velocidad = fila.Item("velocidad")
                If Not IsDBNull(fila.Item("tension_sobrealimentacion")) Then TensionSobreAlimentacion = fila.Item("tension_sobrealimentacion")
                If Not IsDBNull(fila.Item("tension_plegado")) Then TensionPlegado = fila.Item("tension_plegado")
                If Not IsDBNull(fila.Item("fuerza_compensador")) Then FuerzaCompensador = fila.Item("fuerza_compensador")
                If Not IsDBNull(fila.Item("estiraje_produccion")) Then EstirajeProduccion = fila.Item("estiraje_produccion")
                If Not IsDBNull(fila.Item("presion_alimentacion")) Then PresionAlimentacion = fila.Item("presion_alimentacion")
                If Not IsDBNull(fila.Item("codigo_maquina")) Then CodigoEngomadora = fila.Item("codigo_maquina")
                If Not IsDBNull(fila.Item("consumo_post_encerado")) Then ConsumoPostEncerado = fila.Item("consumo_post_encerado")
                If Not IsDBNull(fila.Item("pickup_tenido")) Then PickupTenido = fila.Item("pickup_tenido")
            Next
        End Sub

        Public Function AddReservaFormulacion(ByVal sCodigoTed As String, ByVal nRevision As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "INSERT INTO NM_Formulacion(codigo_formulacion, revision_formulacion, codigo_fase,  " & _
            "codigo_receta, revision_receta, dosificacion, usuario_creacion,  " & _
            "fecha_creacion, usuario_modificacion, fecha_modificacion, flagestado) " & _
            " SELECT codigo_formulacion, revision_formulacion+1, codigo_fase, " & _
            " codigo_receta, revision_receta, dosificacion, usuario_creacion, " & _
            " fecha_creacion, usuario_modificacion, fecha_modificacion, flagestado " & _
            " FROM NM_Formulacion " & _
            " where codigo_formulacion like '" & sCodigoTed & "' and revision_formulacion=" & _
            nRevision
            Return objConn.Execute(sql)
        End Function

        Sub Seek(ByVal Codigo As String, ByVal nEstado As Integer)
            Dim sql As String, objDT As New DataTable
            Dim fila As DataRow
            sql = "Select * " & _
            " from NM_TED where codigo_ted = '" & Codigo & "' and flagestado =" & nEstado
            objDT = objConn.Query(sql)
            For Each fila In objDT.Rows
                If Not IsDBNull(fila.Item("revision_ted")) Then RevisionTED = fila.Item("revision_ted")
                If Not IsDBNull(fila.Item("codigo_urdimbre")) Then CodigoUrdimbre = fila.Item("codigo_urdimbre")
                If Not IsDBNull(fila.Item("revision_urdimbre")) Then RevisionUrdimbre = fila.Item("revision_urdimbre")
                If Not IsDBNull(fila.Item("codigo_area")) Then CodigoArea = fila.Item("codigo_area")
                If Not IsDBNull(fila.Item("temperatura_bano")) Then TemperaturaBano = fila.Item("temperatura_bano")
                If Not IsDBNull(fila.Item("temperatura_coccion")) Then TemperaturaCoccion = fila.Item("temperatura_coccion")
                If Not IsDBNull(fila.Item("tiempo_coccion")) Then TiempoCoccion = fila.Item("tiempo_coccion")
                If Not IsDBNull(fila.Item("presion_exprimido")) Then PresionExprimido = fila.Item("presion_exprimido")
                If Not IsDBNull(fila.Item("temperatura_batea")) Then TemperaturaBatea = fila.Item("temperatura_batea")
                If Not IsDBNull(fila.Item("pickup_seco")) Then PickupSeco = fila.Item("pickup_seco")
                If Not IsDBNull(fila.Item("regulacion_desenrrollamiento")) Then Regulacion = fila.Item("regulacion_desenrrollamiento")
                If Not IsDBNull(fila.Item("campo_divisor")) Then CampoDivisor = fila.Item("campo_divisor")
                If Not IsDBNull(fila.Item("fuerza_planchado")) Then FuerzaPlanchado = fila.Item("fuerza_planchado")
                If Not IsDBNull(fila.Item("humedad_hilo")) Then HumedadHilo = fila.Item("humedad_hilo")
                If Not IsDBNull(fila.Item("velocidad")) Then Velocidad = fila.Item("velocidad")
                If Not IsDBNull(fila.Item("tension_sobrealimentacion")) Then TensionSobreAlimentacion = fila.Item("tension_sobrealimentacion")
                If Not IsDBNull(fila.Item("tension_plegado")) Then TensionPlegado = fila.Item("tension_plegado")
                If Not IsDBNull(fila.Item("fuerza_compensador")) Then FuerzaCompensador = fila.Item("fuerza_compensador")
                If Not IsDBNull(fila.Item("estiraje_produccion")) Then EstirajeProduccion = fila.Item("estiraje_produccion")
                If Not IsDBNull(fila.Item("presion_alimentacion")) Then PresionAlimentacion = fila.Item("presion_alimentacion")
                If Not IsDBNull(fila.Item("codigo_maquina")) Then CodigoEngomadora = fila.Item("codigo_maquina")
                If Not IsDBNull(fila.Item("consumo_post_encerado")) Then ConsumoPostEncerado = fila.Item("consumo_post_encerado")
                If Not IsDBNull(fila.Item("pickup_tenido")) Then PickupTenido = fila.Item("pickup_tenido")
            Next
        End Sub

        Public Function Exist(ByVal Codigo As String, ByVal nRevision As Integer) As Boolean
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_TED where codigo_ted = '" & Codigo & "' " & _
            " revision_ted=" & nRevision
            objDT = objConn.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

    End Class
End Namespace