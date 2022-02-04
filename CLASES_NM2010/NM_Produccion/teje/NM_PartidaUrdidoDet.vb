Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

	Public Class NM_PartidaUrdidoDet
		Public Codigo_Partida_urdido As String = ""
        Public CodigoHilo As String
        Public CodigoHiloNuevo As String
        Public NumeroArmadas As Integer
        Public LoteHilo As String
        Public PesoPartida As Double
        Public CodigoUrdimbre As String
        Public RevisionUrdimbre As Integer
        Public NumeroConos As Integer
		Public MetrosxRollo As Double
        Public PesoUtilHilo As Double
        Public RoturasMillon As Double
        Public RoturasMillonTotal As Double
        '***    Agregado por Luis Antezana
        Public Fecha_Inicio As Date
        Public Hora_Inicio As String
        Public Fecha_Fin As Date
        Public Hora_Fin As String
        Public Velocidad As Double
        '***
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            Codigo_Partida_urdido = ""
            NumeroArmadas = 0
            CodigoHilo = ""
            LoteHilo = ""
            MetrosxRollo = 0
            PesoPartida = 0
            CodigoUrdimbre = ""
            RevisionUrdimbre = 0
            NumeroConos = 0
            PesoUtilHilo = 0
            RoturasMillon = 0
            RoturasMillonTotal = 0
            '*** Agregado por Luis Antezana
            'Fecha_Inicio = Date.Today
            Hora_Inicio = ""
            'Fecha_Fin = Date.Today
            Hora_Fin = ""
            Velocidad = 0
            '***
        End Sub

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Function Insert()
            Dim m_sqlDtAccTejeduria As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros As Object() = { _
                "codigo_partida_urdido", Me.Codigo_Partida_urdido, _
                "codigo_hilo", Me.CodigoHilo, _
                "numero_armadas", Me.NumeroArmadas, _
                "lote_hilo", Me.LoteHilo, _
                "numero_conos_armada", Me.NumeroConos, _
                "peso_util_hilo", Me.PesoUtilHilo, _
                "roturas_millon", Me.RoturasMillon, _
                "roturas_millon_total", Me.RoturasMillonTotal, _
                "metrosxrollo", Me.MetrosxRollo, _
                "codigo_urdimbre", Me.CodigoUrdimbre, _
                "revision_urdimbre", Me.RevisionUrdimbre, _
                "fecha_inicio", Me.Fecha_Inicio, _
                "hora_inicio", Me.Hora_Inicio, _
                "fecha_fin", Me.Fecha_Fin, _
                "hora_fin", Me.Hora_Fin, _
                "velocidad", Me.Velocidad, _
                "usuario", Me.Usuario}
            Return (m_sqlDtAccTejeduria.EjecutarComando("PR_NM_PartidaUrdidoDet_Add_2", objParametros) > 0)

        End Function

        Public Function Update()
            Dim m_sqlDtAccTejeduria As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros As Object() = { _
                "codigo_partida_urdido", Me.Codigo_Partida_urdido, _
                "codigo_hilo", Me.CodigoHilo, _
                "codigo_hilo_nuevo", Me.CodigoHiloNuevo, _
                "numero_armadas", Me.NumeroArmadas, _
                "lote_hilo", Me.LoteHilo, _
                "numero_conos_armada", Me.NumeroConos, _
                "peso_util_hilo", Me.PesoUtilHilo, _
                "roturas_millon", Me.RoturasMillon, _
                "roturas_millon_total", Me.RoturasMillonTotal, _
                "metrosxrollo", Me.MetrosxRollo, _
                "codigo_urdimbre", Me.CodigoUrdimbre, _
                "revision_urdimbre", Me.RevisionUrdimbre, _
                "fecha_inicio", Me.Fecha_Inicio, _
                "hora_inicio", Me.Hora_Inicio, _
                "fecha_fin", Me.Fecha_Fin, _
                "hora_fin", Me.Hora_Fin, _
                "velocidad", Me.Velocidad, _
                "usuario", Me.Usuario}

            Return (m_sqlDtAccTejeduria.EjecutarComando("PR_NM_PartidaUrdidoDet_Upd", objParametros) > 0)

        End Function

        Public Function Delete()
            Dim sql As String, objConn As New NM_Consulta
            sql = "Delete from NM_PartidaUrdidoDet " & _
            " where codigo_partida_urdido = '" & Codigo_Partida_urdido & "' and " & _
            " codigo_urdimbre='" & CodigoUrdimbre & "' and codigo_hilo='" & CodigoHilo & "' and " & _
            " revision_urdimbre=" & RevisionUrdimbre
            Try
                objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Sub Seek(ByVal sCodigoPartida As String, ByVal sCodigoUrdimbre As String, _
         ByVal sRevisionUrdimbre As Integer, ByVal sCodigoHilo As String)
            Dim sql As String, objConn As New NM_Consulta, fila As DataRow, dt As New DataTable
            Dim objPartidaUrdido As New NM_PartidaUrdido

            sql = "Select * from NM_PartidaUrdidoDet where codigo_partida_urdido = '" & _
            sCodigoPartida & "' and codigo_urdimbre='" & sCodigoUrdimbre & _
            "' and revision_urdimbre=" & sRevisionUrdimbre & " and codigo_hilo='" & sCodigoHilo & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Me.Codigo_Partida_urdido = sCodigoPartida
                Me.CodigoHilo = sCodigoHilo
                Me.CodigoUrdimbre = sCodigoUrdimbre
                Me.RevisionUrdimbre = sRevisionUrdimbre
                Me.LoteHilo = fila.Item("lote_hilo")
                Me.NumeroArmadas = fila.Item("numero_armadas")
                Me.NumeroConos = fila.Item("numero_conos_armada")
                Me.PesoUtilHilo = fila.Item("peso_util_hilo")
                If IsDBNull(fila.Item("roturas_millon")) = False Then Me.RoturasMillon = fila.Item("roturas_millon")
                If IsDBNull(fila.Item("roturas_millon_total")) = False Then Me.RoturasMillonTotal = fila.Item("roturas_millon_total")
                If IsDBNull(fila.Item("metrosxrollo")) = False Then Me.MetrosxRollo = fila.Item("metrosxrollo")
                '*** Agregado por Luis Antezana
                If IsDBNull(fila.Item("Fecha_Inicio")) Then
                    Me.Fecha_Inicio = objPartidaUrdido.FechaInicio
                Else
                    Me.Fecha_Inicio = fila.Item("Fecha_Inicio")
                End If
                If IsDBNull(fila.Item("Hora_Inicio")) Then
                    Me.Hora_Inicio = ""
                Else
                    Me.Hora_Inicio = fila.Item("Hora_Inicio")
                End If
                If IsDBNull(fila.Item("Fecha_Fin")) Then
                    Me.Fecha_Fin = objPartidaUrdido.FechaFinal
                Else
                    Me.Fecha_Fin = fila.Item("Fecha_Fin")
                End If
                If IsDBNull(fila.Item("Hora_Inicio")) Then
                    Me.Hora_Fin = ""
                Else
                    Me.Hora_Fin = fila.Item("Hora_Fin")
                End If
                If IsDBNull(fila.Item("Velocidad")) Then
                    Me.Velocidad = 0
                Else
                    Me.Velocidad = fila.Item("Velocidad")
                End If

                '***
            Next
        End Sub

    Public Function List() As DataTable
      Dim ldtbResultado As DataTable, m_sqlDtAccProduccion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      Dim lobjParametros() As Object = {"pvch_Codigo_partida_urdido", "", _
      "ptin_Tipo_consulta", 2}
      Try
        ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdidodet_listar", lobjParametros)
        Return ldtbResultado
      Catch ex As Exception
      Finally
        ldtbResultado = Nothing
        m_sqlDtAccProduccion = Nothing
      End Try
      'Dim sql As String, objConn As New NM_Consulta
      'sql = "SELECT * FROM NM_PartidaUrdidoDet"
      'Return objConn.Query(sql)
    End Function

    Public Function List(ByVal sCodigoPartida As String, ByVal bParaGrid As Boolean) As DataTable
      Dim ldtbResultado As DataTable, m_sqlDtAccProduccion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      Dim lobjParametros() As Object = {"pvch_Codigo_partida_urdido", sCodigoPartida, _
      "ptin_Tipo_consulta", 1}
      Try
        ldtbResultado = m_sqlDtAccProduccion.ObtenerDataTable("usp_tej_urdidodet_listar", lobjParametros)
        Return ldtbResultado
      Catch ex As Exception
      Finally
        ldtbResultado = Nothing
        m_sqlDtAccProduccion = Nothing
      End Try
      'Dim sql As String, objConn As New NM_Consulta, dt As New DataTable
      'sql = "select codigo_hilo, numero_armadas, lote_hilo, codigo_urdimbre, " & _
      '" revision_urdimbre, numero_conos_armada, peso_util_hilo, metrosxrollo, roturas_millon, roturas_millon_total, " & _
      '" fecha_inicio, hora_inicio, fecha_fin, hora_fin, velocidad " & _
      '" from NM_PartidaUrdidoDet " & _
      '" where codigo_partida_urdido = '" & sCodigoPartida & "'"
      'dt = objConn.Query(sql)
      'Return dt
    End Function

    Public Function GetMTSRollo(ByVal sCodigoPartida As String, ByVal sCodigoHilo As String) As Double
      Dim sql As String, DT As New DataTable, fila As DataRow
      Dim MetrosHilo As Double = 0, objConn As New NM_Consulta
      Dim dt2 As New DataTable, NumCarretes As Int16 = 0, retorno As Double = 0
      sql = "select sum(numero_armadas * (peso_util_hilo/0.59)* H.titulo*1000) as MetrosHilo " & _
      " from NM_PartidaUrdidoDet D, NM_THilo H " & _
      " where D.codigo_hilo = H.codigo_hilo and D.codigo_hilo='" & sCodigoHilo & "' " & _
      " and D.codigo_partida_urdido = '" & sCodigoPartida & "' "
      DT = objConn.Query(sql)
      For Each fila In DT.Rows
        MetrosHilo = fila.Item("MetrosHilo")
      Next

      sql = "select count(carrete) AS cuenta from NM_PartidaUrdidoDProduccion " & _
      " where codigo_hilo='" & sCodigoHilo & "' and codigo_partida_urdido='" & _
      sCodigoPartida & "' "
      dt2 = objConn.Query(sql)
      For Each fila In dt2.Rows
        NumCarretes = fila.Item("cuenta")
      Next

      If NumCarretes > 0 Then
        retorno = MetrosHilo / NumCarretes
      End If

      Return retorno

    End Function

    Function GetNumeroHilos(ByVal sCodigoHilo As String, ByVal sCodigoPartida As String) As Double
      Dim sql As String, objConn As New NM_Consulta
      Dim dt As New DataTable, fila As DataRow
      sql = "Select sum(hilos_carrete) as suma from NM_PartidaUrdidoDProduccion " & _
      " where codigo_partida_urdido = '" & sCodigoPartida & "' " & _
      " and codigo_hilo = '" & sCodigoHilo & "' "
      dt = objConn.Query(sql)
      If dt.Rows.Count > 0 Then
        fila = dt.Rows(0)
        If IsDBNull(fila("suma")) = False Then Return fila("suma") Else Return 0
      End If
      Return 0
    End Function

    Function GetRoturasMillon(ByVal bTotal As Boolean, ByVal sCodigoHilo As String, _
     ByVal sCodigoPartida As String) As Double
      Dim sql As String, objConn As New NM_Consulta
      Dim dt As New DataTable, fila As DataRow
      Dim dt2 As New DataTable, fila2 As DataRow
      Dim Suma As Double, Valor As Double = 0

            sql = "select sum(RoturaEmpalmeOE + RoturaEmpalmeConera + RoturaEmpalmeContinua+HiloDebil " & _
      "+ Enganche + Reserva + Pelusa + CambioCogollos + HilosCruzados + " & _
      " BobinaDanada + HiloRoto + HiloPicado + Champa + HiloArrastre "
      If bTotal = False Then sql = sql & "+ otros_defectos"
      sql = sql & ") as suma from NM_PartidaUrdidoMCalidad " & _
      " WHERE codigo_partida_urdido = '" & sCodigoPartida & "' " & _
      " and codigo_hilo = '" & sCodigoHilo & "'"
      ' and carrete = '" & '
      dt = objConn.Query(sql)
      Suma = 0
      For Each fila In dt.Rows
        If IsDBNull(fila("suma")) = False Then Suma = fila("suma")
      Next

      'sql = "select ((" & Suma & " / case when Sum(Metros_partida)=0 then 1 " & _
      '" else Sum(Metros_partida) end * " & _
      '" case when count(carrete)=0 then 1 else count(carrete) end)/ " & _
      '" case when sum(hilos_carrete)=0 then 1 else sum(hilos_carrete) end) as valor, " & _
      '" codigo_hilo " & _
      '" from NM_PartidaUrdido P, NM_PartidaUrdidoDProduccion PP " & _
      '" where P.codigo_partida_urdido = PP.codigo_partida_urdido " & _
      '" and codigo_hilo = '" & sCodigoHilo & "' " & _
      '" group by codigo_hilo "
      'dt2 = objConn.Query(sql)
      'For Each fila2 In dt.Rows
      '    Valor = fila2.Item("valor")
      'Next
      'Return Valor
      Return Suma
    End Function

    Public Sub ActualizarDetalleUrdido(ByVal pCodigoPartidaUrdido As String, ByVal pCodigoHiloAntiguo As String, _
           ByVal pCodigoHiloNuevo As String, ByVal pCodigoUrdimbre As String, ByVal pRevisionUrdimbre As Integer)

      Try
        Dim m_sqlDtAccTejeduria As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

        Dim objParametros As Object() = {"codigo_partida_urdido", pCodigoPartidaUrdido, _
            "codigo_hilo_antiguo", pCodigoHiloAntiguo, "codigo_hilo_nuevo", pCodigoHiloNuevo, _
            "codigo_urdimbre", pCodigoUrdimbre, "revision_urdimbre", pRevisionUrdimbre}

        m_sqlDtAccTejeduria.EjecutarComando("pr_PartidaUrdido_Update", objParametros)
        m_sqlDtAccTejeduria.Dispose()
      Catch ex As Exception
        Throw ex
      End Try
    End Sub

  End Class
End Namespace