Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace Reportes

    Public Class NM_FichaPlegador
        Private codigo_partida As String
        Private codigo_proceso As String
        Private codigo_operario As String
        Private nombre_operario As String
        Private turno_operario As String
        Private fecha_partida As String
        Private color_urdimbre As String
        Private codigo_plegador_a As String
        Private codigo_plegador_b As String
        Private codigo_calificacion_a As String
        Private codigo_calificacion_b As String
        Private metros As String
        Private observacion_calidad As String
        Private ocurrencias As String
        Private partidas As DataTable
        Private hilos As DataTable
        Private piezasA As DataTable
        Private piezasB As DataTable
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer

#Region "New"
        Public Sub New()
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Public Sub New(ByVal txtCodigoPartida As String, ByVal txtplegador1 As String, ByVal txtplegador2 As String)
            partidas = ListaPartida(txtCodigoPartida, txtplegador1, txtplegador2)
            hilos = ListaHilosUrdimbre(txtCodigoPartida)
            piezasA = ListaPiezas(txtCodigoPartida, codigoPlegadorA)
            piezasB = ListaPiezas(txtCodigoPartida, codigoPlegadorB)
        End Sub

#End Region

#Region "-- Propiedades --"

        Public Property codigoPartida() As String
            Get
                codigo_partida = partidas.Rows(0)("codigo_partida_engomadoted")
                Return codigo_partida
            End Get
            Set(ByVal Value As String)
                codigo_partida = Value
            End Set
        End Property

        Public Property codigoProceso() As String
            Get
                codigo_proceso = ""
                codigo_proceso = partidas.Rows(0)("codigo_engomado")
                Return codigo_proceso
            End Get
            Set(ByVal Value As String)
                codigo_proceso = Value
            End Set
        End Property

        Public Property colorUrdimbre() As String
            Get
                color_urdimbre = ""
                If Not IsDBNull(partidas.Rows(0)("codigo_color_ted")) Then
                    color_urdimbre = partidas.Rows(0)("codigo_color_ted")
                End If
                Return color_urdimbre
            End Get
            Set(ByVal Value As String)
                color_urdimbre = Value
            End Set
        End Property

        Public Property ObservacionCalidad(ByVal strTipo As String) As String
            Get
                Dim iFila As Integer
                observacion_calidad = ""
                For iFila = 0 To partidas.Rows.Count - 1
                    If strTipo = partidas.Rows(iFila)("lado") Then
                        If Not IsDBNull(partidas.Rows(iFila)("observacionescalidad")) Then
                            observacion_calidad = partidas.Rows(iFila)("observacionescalidad")
                        End If
                    End If

                Next
                Return observacion_calidad
            End Get
            Set(ByVal Value As String)
                observacion_calidad = Value
            End Set
        End Property

        Public Property ocurrencia() As String
            Get
                Dim iFila As Integer
                ocurrencia = ""
                If Not IsDBNull(partidas.Rows(iFila)("ocurrecias")) Then
                    ocurrencia = partidas.Rows(iFila)("ocurrecias")
                End If
                Return ocurrencia
            End Get
            Set(ByVal Value As String)
                ocurrencia = Value
            End Set
        End Property

        Public Property codigoOperario() As String
            Get
                codigo_operario = ""
                If Not IsDBNull(partidas.Rows(0)("operario")) Then
                    codigo_operario = partidas.Rows(0)("operario")
                End If
                Return codigo_operario
            End Get
            Set(ByVal Value As String)
                codigo_operario = Value
            End Set
        End Property

        Public Property turno() As String
            Get
                Dim fecfin As Date
                turno_operario = ""

                If Not IsDBNull(partidas.Rows(0)("fecha")) Then
                    fecfin = partidas.Rows(0)("fecha")
                    If fecfin.Hour >= 7 And fecfin.Hour < 15 Then
                        turno_operario = "PRIMERO"
                    End If
                    If fecfin.Hour >= 15 And fecfin.Hour < 23 Then
                        turno_operario = "SEGUNDO"
                    End If
                    If (fecfin.Hour >= 23 And fecfin.Hour < 24) Or _
                    (fecfin.Hour >= 0 And fecfin.Hour < 7) Then
                        turno_operario = "TERCERO"
                    End If

                    codigo_operario = partidas.Rows(0)("operario")
                End If
                Return turno_operario
            End Get
            Set(ByVal Value As String)
                codigo_operario = Value
            End Set
        End Property

        Public Property nombreOperario() As String
            Get
                nombre_operario = ""
                If Not IsDBNull(partidas.Rows(0)("nombre")) Then
                    nombre_operario = partidas.Rows(0)("nombre")
                End If
                Return nombre_operario
            End Get
            Set(ByVal Value As String)
                nombre_operario = Value
            End Set
        End Property

        Public Property turnoOperario() As String
            Get
                turno_operario = ""
                Return turno_operario
            End Get
            Set(ByVal Value As String)
                turno_operario = Value
            End Set
        End Property

        Public Property fechaPartida() As String
            Get
                fecha_partida = ""
                If Not IsDBNull(partidas.Rows(0)("fecha_fin")) Then
                    fecha_partida = partidas.Rows(0)("fecha_fin")
                End If
                Return fecha_partida
            End Get
            Set(ByVal Value As String)
                fecha_partida = Value
            End Set
        End Property

        Public Property codigoPlegadorA() As String
            Get
                Dim row As DataRow
                codigo_plegador_a = ""
                If partidas.Rows.Count > 0 Then
                    If Not IsDBNull(partidas.Rows(0)("lado")) Then
                        For Each row In partidas.Rows
                            If "A".Equals(row("lado")) Then
                                codigo_plegador_a = row("codigo_plegador")
                            End If
                        Next
                    End If
                End If
                Return codigo_plegador_a
            End Get
            Set(ByVal Value As String)
                codigo_plegador_a = Value
            End Set
        End Property

        Public Property codigoPlegadorB() As String
            Get
                Dim row As DataRow
                codigo_plegador_b = ""
                If partidas.Rows.Count > 0 Then
                    If Not IsDBNull(partidas.Rows(0)("lado")) Then
                        For Each row In partidas.Rows
                            If "B".Equals(row("lado")) Then
                                codigo_plegador_b = row("codigo_plegador")
                            End If
                        Next
                    End If
                End If
                Return codigo_plegador_b
            End Get
            Set(ByVal Value As String)
                codigo_plegador_a = Value
            End Set
        End Property

        Public Property longitud() As String
            Get
                metros = ""
                If Not IsDBNull(partidas.Rows(0)("longitud")) Then
                    metros = partidas.Rows(0)("longitud")
                End If
                Return metros
            End Get
            Set(ByVal Value As String)
                metros = Value
            End Set
        End Property

        Public ReadOnly Property calificacionA() As String
            Get
                Dim row As DataRow
                codigo_calificacion_a = ""
                If Not IsDBNull(partidas.Rows(0)("calificacion")) Then
                    For Each row In partidas.Rows
                        If "A".Equals(row("lado")) Then
                            codigo_calificacion_a = row("calificacion")
                        End If
                    Next
                End If
                Return codigo_calificacion_a
            End Get
        End Property

        Public ReadOnly Property calificacionB() As String
            Get
                Dim row As DataRow
                codigo_calificacion_b = ""
                If Not IsDBNull(partidas.Rows(0)("calificacion")) Then
                    For Each row In partidas.Rows
                        If "B".Equals(row("lado")) Then
                            codigo_calificacion_b = row("calificacion")
                        End If
                    Next
                End If
                Return codigo_calificacion_b
            End Get
        End Property
#End Region

#Region "ListaPartida"
        Public Function ListaPartida(ByVal txtCodigoPartida As String, ByVal txtplegador1 As String, ByVal txtplegador2 As String) As DataTable
            Dim objProd4 As New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objDT As New DataTable
            Dim lstrParams() As String = {"var_CodigoPartida", txtCodigoPartida.Trim, _
                                            "var_CodigoPlegador", txtplegador1.Trim, "var_ParejaPlegador", txtplegador2.Trim}
            Try
                objDT = objProd4.ObtenerDataTable("usp_PRE_Partida_Listar_2", lstrParams)
            Catch ex As Exception

            Finally
                objProd4 = Nothing
            End Try
            Return objDT
        End Function
#End Region

#Region "ListaPiezas"

        Public Function ListaPiezas(ByVal txtCodigoPartida As String, ByVal txtplegador As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim objDT As New DataTable
            Dim sql As String
            sql = "SELECT PEC.correlativo, PEC.codigo_partida_engomadoted, " & _
            " PEC.codigo_plegador " & _
            " FROM   NM_PartidaEngomadoCorrelativo PEC" & _
            " WHERE  PEC.codigo_partida_engomadoted='" & txtCodigoPartida & "' " & _
            " AND PEC.codigo_plegador='" & txtplegador & "' " & _
            " ORDER BY PEC.correlativo "
            objDT = objGen.Query(sql)
            Return objDT

        End Function

#End Region

#Region "ListaHilosUrdimbre"

        Public Function ListaHilosUrdimbre(ByVal txtCodigoPartida As String) As DataTable
            Dim objGen As New NM_Consulta
            Dim objDT As New DataTable

            Dim sql As String

            sql = " SELECT distinct UD.codigo_hilo, UD.numero_hilos, " & _
            " PEYTED.codigo_partida_engomadoted, Hilos.de_item de_hilo" & _
            " FROM ((NM_PartidaEngomadoYTED PEYTED " & _
            " INNER JOIN NM_ParticionPartidas PP " & _
            " ON PEYTED.codigo_sub_partida_urdido= PP.codigo_sub_partida_urdido) " & _
            " INNER JOIN NM_PartidaUrdido PU " & _
            " ON PP.codigo_partida_urdido= PU.codigo_partida_urdido) " & _
            " INNER JOIN NM_UrdimbreDetalle UD " & _
            " ON PU.codigo_urdimbre = UD.codigo_urdimbre " & _
            " and PU.revision_urdimbre = UD.revision_urdimbre " & _
            " FULL OUTER JOIN OFILOGI.dbo.nm_hilos Hilos on co_item = UD.codigo_hilo" & _
            " COLLATE database_default " & _
            " WHERE  PEYTED.codigo_partida_engomadoted='" & txtCodigoPartida & "' "

            objDT = objGen.Query(sql)

            Return objDT

        End Function

#End Region

#Region "trslistaurdimbre"

        Public ReadOnly Property trslistaurdimbre() As String
            Get
                Dim str As String
                Dim objDR As DataRow
                str = "<table width=100% border=1 bordercolor=#ffffff cellspacing=0> " & _
                "<tr><td class=titulo>Código de Hilo de Urdimbre</td>" & _
                "<td class=titulo>Descripción de Hilo de&nbsp;Urdimbre</td>" & _
                "<td class=titulo>Total Hilos</td></tr>"
                str += ""
                For Each objDR In hilos.Rows()
                    str += "<tr><td>"
                    str += objDR("codigo_hilo")
                    str += "</td><td>"
                    str += objDR("de_hilo")
                    str += "</td><td>"
                    str += CStr(objDR("numero_hilos"))
                    str += "</td></tr>"
                Next
                str += "</table>"
                Return str
            End Get
        End Property

#End Region

#Region "trslistapieza"

        Public ReadOnly Property trslistapieza() As String
            Get
                Dim str As String
                Dim i As Integer
                str = "<table width=100% border=1 bordercolor=#ffffff cellspacing=0> " & _
                "<tr><td class=titulo>Fecha</td>" & _
                "<td class=titulo>Pieza</td>" & _
                "<td class=titulo>Turno</td>" & _
                "<td class=titulo>Fecha</td>" & _
                "<td class=titulo>Pieza</td>" & _
                "<td class=titulo>Turno</td></tr>"
                If piezasA.Rows.Count > 0 AndAlso piezasB.Rows.Count = 0 Then
                    For i = 0 To piezasA.Rows.Count() - 1
                        str += "<tr><td>&nbsp;</td><td>"
                        str += piezasA.Rows(i)("correlativo")
                        str += "</td><td>&nbsp;</td>"
                        str += "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"
                    Next
                ElseIf piezasB.Rows.Count > 0 AndAlso piezasA.Rows.Count = 0 Then
                    For i = 0 To piezasB.Rows.Count() - 1
                        str += "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>"
                        str += "<td>&nbsp;</td><td>"
                        str += piezasB.Rows(i)("correlativo")
                        str += "</td><td>&nbsp;</td></tr>"
                    Next
                ElseIf piezasB.Rows.Count > 0 AndAlso piezasA.Rows.Count > 0 Then
                    For i = 0 To piezasA.Rows.Count() - 1
                        str += "<tr><td>&nbsp;</td><td>"
                        str += piezasA.Rows(i)("correlativo")
                        str += "</td><td>&nbsp;</td>"
                        str += "<td>&nbsp;</td><td>"
                        str += piezasB.Rows(i)("correlativo")
                        str += "</td><td>&nbsp;</td></tr>"
                    Next
                End If
                str += "</table>"
                Return str
            End Get
        End Property

        Public ReadOnly Property trslistapiezaNew() As String
            Get
                Dim str As String
                Dim i As Integer
                str = "<table width=100% border=1 bordercolor=#ffffff cellspacing=0> " & _
                "<tr><td class=titulo>Motivo Corte</td>" & _
                "<td class=titulo>Metraje</td>" & _
                "<td class=titulo>Fecha</td>" & _
                "<td class=titulo>Pieza</td>" & _
                "<td class=titulo>Volante/Tur.</td></tr>"
                If piezasB.Rows.Count > 0 AndAlso piezasA.Rows.Count = 0 Then
                    For i = 0 To piezasB.Rows.Count() - 1
                        str += "<tr><td>&nbsp;</td><td>&nbsp;</td>"
                        str += "<td>&nbsp;</td><td>"
                        str += piezasB.Rows(i)("correlativo")
                        str += "</td><td>&nbsp;</td></tr>"
                    Next
                End If
                str += "</table>"
                Return str
            End Get
        End Property

#End Region

#Region "Formato de Impresion"
        Public Function ListarDatosFichaPlegador(ByVal strCodigoPartida As String, ByVal strCodigoPlegador As String) As DataSet
            Try
                Dim objParametros As Object() = {"p_var_CodigoPartidaEngomado", strCodigoPartida, _
                                                 "p_var_CodigoPLegador", strCodigoPlegador}
                Return m_sqlDtAccProduccion.ObtenerDataSet("usp_query_ListarDatosEngomado_2", objParametros)
            Catch ex As Exception

            End Try
        End Function
#End Region

#Region "LUIS_AJ"
        Public Function ListaDatosFichaPlegador(ByVal strCodigoPartidaTED As String, ByVal strPlegador As String) As DataTable

            Dim objProd4 As New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objDT As New DataTable
            Dim lstrParams() As String = {"pvch_PartidaTED", strCodigoPartidaTED.Trim,
                                          "pvch_CodPlegador", strPlegador.Trim}
            Try
                objDT = objProd4.ObtenerDataTable("USP_PRD_LISTAR_DATOS_FICHA_PLEGADOR", lstrParams)
            Catch ex As Exception

            Finally
                objProd4 = Nothing
            End Try
            Return objDT
        End Function
#End Region
#Region "Eliminar Ficha Plegador Estado"
        Public Function EliminarDatosFichaPlegador(ByVal strCodigoPartidaTED As String, ByVal strPlegador As String, ByVal strCodTelar As String, ByVal strcodTrabajador As String) As Integer

            Dim objProd4 As New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim intResult As Integer

            Dim lstrParams() As String = {"pvch_CodigoPartidaTED", strCodigoPartidaTED.Trim,
                                          "pvch_CodPlegador", strPlegador.Trim,
                                          "pvch_CodTelar", strCodTelar,
                                          "codTrabajador", strcodTrabajador}
            Try
                intResult = objProd4.EjecutarComando("USP_PRD_ELIMINAR_DATOS_FICHA_PLEGADOR", lstrParams)
                objProd4.Dispose()
            Catch ex As Exception

            Finally
                objProd4 = Nothing
            End Try
            Return intResult
        End Function
#End Region
    End Class

End Namespace