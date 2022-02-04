Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaUrdidoMCalidad
        Public CodigoPartida As String
        Public Carrete As String
        Public CodigoHilo As String
        Public RoturaEmpalmeOE As Double
        Public RoturaEmpalmeConera As Double
        Public RoturaEmpalmeContinua As Double
        Public HiloDebil As Double
        Public Enganche As Double
        Public Reserva As Double
        Public Pelusa As Double
        Public CambioCogollos As Double
        Public HilosCruzados As Double
        Public BobinaDanada As Double
        Public HiloRoto As Double
        Public HiloPicado As Double
        Public Champa As Double
        Public HiloArrastre As Double
        Public OtrosDefectos As Double
        Public Ocurrencias As String
        Public Usuario As String
        Public Debug As String

        Private objGen As New NM_Consulta
        Private m_sqlDtAccProduccion As AccesoDatosSQLServer

        Sub New()
            CodigoPartida = ""
            Carrete = ""
            RoturaEmpalmeOE = 0
            RoturaEmpalmeConera = 0
            RoturaEmpalmeContinua = 0
            HiloDebil = 0
            Enganche = 0
            Reserva = 0
            Pelusa = 0
            CambioCogollos = 0
            HilosCruzados = 0
            BobinaDanada = 0
            HiloRoto = 0
            HiloPicado = 0
            Champa = 0
            HiloArrastre = 0
            OtrosDefectos = 0
            Ocurrencias = ""
            CodigoHilo = ""
            Usuario = ""
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Sub Add()
            'Dim sql As String
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Dim lobjParametros() As Object = {"pvch_codigo_partida_urdido", CodigoPartida, _
            "pvch_carrete", Carrete, _
            "pvch_codigo_hilo", CodigoHilo, _
            "pnum_RoturaEmpalmeOE", RoturaEmpalmeOE, _
            "pnum_RoturaEmpalmeConera", RoturaEmpalmeConera, _
            "pnum_RoturaEmpalmeContinua", RoturaEmpalmeContinua, _
            "pnum_HiloDebil", HiloDebil, _
            "pnum_Enganche", Enganche, _
            "pnum_Reserva", Reserva, _
            "pnum_Pelusa", Pelusa, _
            "pnum_CambioCogollos", CambioCogollos, _
            "pnum_HilosCruzados", HilosCruzados, _
            "pnum_BobinaDanada", BobinaDanada, _
            "pnum_HiloRoto", HiloRoto, _
            "pnum_HiloPicado", HiloPicado, _
            "pnum_Champa", Champa, _
            "pnum_HiloArrastre", HiloArrastre, _
            "pnum_otros_defectos", OtrosDefectos, _
            "pvch_ocurrencias", Ocurrencias, _
            "usuario_creacion", Usuario}
            Try
                If CodigoPartida <> "" AndAlso Carrete <> "" Then
                    'sql = "Insert into NM_PartidaUrdidoMCalidad (" & _
                    '"codigo_partida_urdido, carrete, codigo_hilo, " & _
                    '"RoturaEmpalmeConera, RoturaEmpalmeContinua, " & _
                    '"HiloDebil, Enganche, Reserva, Pelusa, CambioCogollos, " & _
                    '"HilosCruzados, BobinaDanada, HiloRoto, HiloPicado, " & _
                    '"Champa, HiloArrastre, otros_defectos, " & _
                    '"ocurrencias, usuario_creacion, fecha_creacion) values('" & _
                    'CodigoPartida & "','" & Carrete & "','" & CodigoHilo & _
                    '"',0" & RoturaEmpalmeConera & ", 0" & RoturaEmpalmeContinua & ", 0" & _
                    'HiloDebil & ", 0" & Enganche & ", 0" & Reserva & ", 0" & Pelusa & _
                    '", 0" & CambioCogollos & ", 0" & _
                    'HilosCruzados & ", 0" & BobinaDanada & ", 0" & HiloRoto & _
                    '", 0" & HiloPicado & ", 0" & _
                    'Champa & ", 0" & HiloArrastre & "," & Val(OtrosDefectos) & _
                    '",'" & Ocurrencias & "','" & Usuario & "',getdate())"
                    'objGen.Execute(sql)
                    ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdidocalidad_insertar", lobjParametros)
                    If ldtbResultado.Rows.Count > 0 Then
                        If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                            lblnGraboCorrectamente = True
                        End If
                    End If
                End If
            Catch ex As Exception
            Finally
                ldtbResultado = Nothing
            End Try
        End Sub

        Sub Delete(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal sCodigoHilo As String)
            'Dim sql As String
            Dim lblnGraboCorrectamente As Boolean = False, ldtbResultado As DataTable
            Dim lobjParametros() As Object = {"pvch_Codigo_partida_urdido", sCodigoPartida, _
            "pvch_Carrete", sCarrete, _
            "pvch_Codigo_hilo", sCodigoHilo}
            Try
                If sCodigoPartida <> "" AndAlso sCarrete <> "" Then
                    'sql = "Delete from NM_PartidaUrdidoMCalidad where " & _
                    '" codigo_partida_urdido = '" & sCodigoPartida & _
                    '"' and carrete = '" & sCarrete & "' and " & _
                    '" codigo_hilo ='" & sCodigoHilo & "' "
                    'objGen.Execute(sql)
                    ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdidocalidad_eliminar", lobjParametros)
                    If ldtbResultado.Rows.Count > 0 Then
                        If ldtbResultado.Rows(0).Item("ESTADO") = 1 Then
                            lblnGraboCorrectamente = True
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Sub Delete(ByVal sCodigoPartida As String, ByVal sCodigoHilo As String)
            Dim sql As String
            Try
                If sCodigoPartida <> "" Then
                    sql = "Delete from NM_PartidaUrdidoMCalidad where " & _
                    " codigo_partida_urdido = '" & sCodigoPartida & _
                    "' and " & _
                    " codigo_hilo ='" & sCodigoHilo & "' "
                    objGen.Execute(sql)
                End If
            Catch ex As Exception

            End Try
        End Sub


        Sub Update()
            Dim sql As String
            Try
                If CodigoPartida <> "" AndAlso Carrete <> "" Then
                    sql = "Update NM_PartidaUrdidoMCalidad set " & _
                    "  RoturaEmpalmeOE = 0" & RoturaEmpalmeOE & _
                    ", RoturaEmpalmeConera = 0" & RoturaEmpalmeConera & _
                    ", RoturaEmpalmeContinua = 0" & RoturaEmpalmeContinua & _
                    ", HiloDebil = 0" & HiloDebil & _
                    ", Enganche = 0" & Enganche & _
                    ", Reserva = 0" & Reserva & _
                    ", Pelusa = 0" & Pelusa & _
                    ", CambioCogollos = 0" & CambioCogollos & _
                    ", HilosCruzados = 0" & HilosCruzados & _
                    ", BobinaDanada = 0" & BobinaDanada & _
                    ", HiloRoto = 0" & HiloRoto & _
                    ", HiloPicado = 0" & HiloPicado & _
                    ", Champa = 0" & Champa & _
                    ", HiloArrastre = 0" & HiloArrastre & _
                    ",otros_defectos = " & OtrosDefectos & ", ocurrencias = '" & _
                    Ocurrencias & "', usuario_modificacion='" & Usuario & _
                    "', fecha_modificacion = getdate() " & _
                    "where codigo_partida_urdido ='" & CodigoPartida & "' " & _
                    " and carrete = '" & Carrete & "' " & _
                    " and codigo_hilo = '" & CodigoHilo & "' "
                    objGen.Execute(sql)
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_PartidaUrdidoMCalidad "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function List(ByVal sCodigoPartida As String) As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "select C.* " & _
            " from NM_PartidaUrdidoMCalidad C, NM_PartidaUrdidoDProduccion P " & _
            " where C.codigo_partida_urdido = P.codigo_partida_urdido " & _
            " and C.carrete = P.carrete " & _
            " and C.codigo_hilo = P.codigo_hilo " & _
            " and C.codigo_partida_urdido = '" & sCodigoPartida & "' " & _
            " order by P.fecha_creacion "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Exist(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal sCodigoHilo As String) As Boolean
            Dim sql As String, objDT As New DataTable, fila As DataRow
            sql = "Select * " & _
            " from NM_PartidaUrdidoMCalidad where codigo_partida_urdido ='" & _
            sCodigoPartida & "' and carrete='" & sCarrete & "'  and " & _
            "codigo_hilo ='" & sCodigoHilo & "' "
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Sub Seek(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal sCodigoHilo As String)
            Dim sql As String, objDT As New DataTable, fila As DataRow
            sql = "Select * " & _
            " from NM_PartidaUrdidoMCalidad where codigo_partida_urdido ='" & _
            sCodigoPartida & "' and carrete='" & sCarrete & "' and " & _
            " codigo_hilo ='" & sCodigoHilo & "' "
            objDT = objGen.Query(sql)
            For Each fila In objDT.Rows
                CodigoPartida = fila.Item("codigo_partida_urdido")
                CodigoHilo = fila.Item("codigo_hilo")
                Carrete = fila.Item("carrete")
                RoturaEmpalmeOE = fila.Item("RoturaEmpalmeOE")
                RoturaEmpalmeConera = fila.Item("RoturaEmpalmeConera")
                RoturaEmpalmeContinua = fila.Item("RoturaEmpalmeContinua")
                HiloDebil = fila.Item("HiloDebil")
                Enganche = fila.Item("Enganche")
                Reserva = fila.Item("Reserva")
                Pelusa = fila.Item("Pelusa")
                CambioCogollos = fila.Item("CambioCogollos")
                HilosCruzados = fila.Item("HilosCruzados")
                BobinaDanada = fila.Item("BobinaDanada")
                HiloRoto = fila.Item("HiloRoto")
                HiloPicado = fila.Item("HiloPicado")
                Champa = fila.Item("Champa")
                HiloArrastre = fila.Item("HiloArrastre")
                OtrosDefectos = fila.Item("otros_defectos")
                Ocurrencias = fila.Item("ocurrencias")
            Next
        End Sub


    End Class

End Namespace