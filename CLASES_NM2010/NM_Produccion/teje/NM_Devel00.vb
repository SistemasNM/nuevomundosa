Imports System.Data
Imports System.Data.SqlClient
Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia
    Public Class Class0

    End Class
End Namespace
Namespace NM_Tejeduria

    Public Class TPlegadorReserva
        '---Atributos
        Private objConsulta As New NM_Consulta()
        Public Fecha As Date
        Public NumPlegador As String
        Public Ubicacion As String
        Public usuario_creacion As String
        Public usuario_modificacion As String
        Private objUtil As New NM_General.Util

        Public Sub New()

        End Sub

        '----Metodos
        Public Sub insertar()
            Dim strsql As String = "INSERT INTO NM_PlegadoresReserva values(" & _
            "'" & objUtil.FormatFecha(Fecha) & "','" & NumPlegador & "','" & Ubicacion & "','" & usuario_creacion & "',GETDATE(),'" & usuario_creacion & "',GETDATE())"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub Eliminar(ByVal pItem As Integer)
            Dim strsql As String
            strsql = "DELETE FROM NM_PlegadoresReserva WHERE item =" & pItem
            objConsulta.Execute(strsql)
        End Sub
        Public Sub Eliminar(ByVal pNumPlegador As String)
            Dim strsql As String
            strsql = "DELETE FROM NM_PlegadoresReserva WHERE numero_plegador ='" & pNumPlegador & "'"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub Actualizar()
            Dim strsql As String
            strsql = "UPDATE NM_PlegadoresReserva SET Ubicacion ='" _
             & Ubicacion & "',Usuario_creacion='" & usuario_modificacion & "'" & _
             ",fecha_modificacion= GETDATE()" & _
             " where numero_plegador = '" & NumPlegador & "'"
            objConsulta.Execute(strsql)
        End Sub
        Public Function listar() As DataTable
            Return objConsulta.getData("NM_PlegadoresReserva")
        End Function

        Public Function getPlegadores(ByVal pfecha) As DataTable
            Dim strsql As String
            strsql = "select * from NM_plegador where codigo_plegador " & _
            " not in (select numero_plegador from NM_PlegadoresReserva where fecha = '" & objUtil.FormatFecha(pfecha) & "')"
            Return objConsulta.Query(strsql)
        End Function

        Public Function listar(ByVal fecha As Date) As DataTable
            Dim strsql As String
            strsql = "Select * from NM_PlegadoresReserva where fecha = '" & objUtil.FormatFecha(fecha) & "'"
            Return objConsulta.Query(strsql)
        End Function

    End Class

    Public Class ControlPlegadoresReserva
        Public objConsulta As New NM_Consulta
        Public Fecha As Date
        Public HoraInicio As String
        Public HoraFin As String
        Public Responsable As String
        Public Area As String
        Public centro_costo As String
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public plegadores As DataTable
        Public PlegadorReserva As TPlegadorReserva
        Private objUtil As New NM_General.Util

        Function PlegadoresReserva() As DataTable
            Return PlegadorReserva.listar
        End Function

        Function getPlegadores() As DataTable
            Return objConsulta.getData("NM_Plegador")
        End Function

        Public Sub New()

        End Sub

        Public Sub seek(ByVal sfecha As Date)
            Dim fila As DataRow
            Dim tabla As DataTable
            Dim sql As String = "SELECT * FROM NM_ControlPlegadoresReserva where Fecha='" & objUtil.FormatFecha(sfecha) & "'"
            Dim bd As New NM_Consulta
            tabla = BD.Query(sql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("persona_responsable")) Then Responsable = fila("persona_responsable")
                If Not IsDBNull(fila("centro_costo")) Then centro_costo = fila("centro_costo")
                If Not IsDBNull(Fecha = fila("Fecha")) Then Fecha = fila("Fecha")
                If Not IsDBNull(fila("hora_inicio")) Then HoraInicio = fila("hora_inicio")
                If Not IsDBNull(fila("hora_fin")) Then HoraFin = fila("hora_fin")
            Next
            Dim detalle As New TPlegadorReserva
            plegadores = detalle.listar(sfecha)
        End Sub

        Private Sub AgregarPlegadores_Reserva(ByVal Plegadores As DataTable)
            Dim i As Integer
            For i = 0 To Plegadores.Rows.Count - 1
                PlegadorReserva = New TPlegadorReserva
                PlegadorReserva.Fecha = Plegadores.Rows(i).Item("Fecha").ToString ' se debe convertir a datetime
                PlegadorReserva.NumPlegador = Plegadores.Rows(i).Item("numero_plegador").ToString
                PlegadorReserva.Ubicacion = Plegadores.Rows(i).Item("Ubicacion").ToString
                PlegadorReserva.insertar() ' inserta en la base de datos un nuevo plegador en reserva
            Next i

        End Sub

        Public Sub AgregarControlPlegadora(ByVal Plegadores As DataTable)
            Dim strsql As String
            Dim con As Integer
            Dim result As New DataTable
            'se le asigna la fecha en el momento que se registra el control
            Fecha = Date.Today
            strsql = "INSERT INTO NM_ControlPlegadoresReserva " & _
            "(Fecha, hora_inicio,hora_fin,persona_responsable,centro_costo,usuario_creacion,fecha_creacion) " & _
            "values ('" & objUtil.FormatFechaHora(Fecha) & "','" & HoraInicio & "','" & HoraFin & _
                    "','" & Responsable & "','" & centro_costo & "','" & usuario_creacion & "',GETDATE())"

            Try
                objConsulta.Execute(strsql)
                AgregarPlegadores_Reserva(Plegadores)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try


            ' End If
            ' se añade el detalle correspondiente al control de plegadores en reserva
            ' el detalle es pasado como un datable

        End Sub

    End Class

    Public Class TCInventarioCrudoDenimProceso
        Public fecha As String ' campo por el cual se enlaza al maestro
        Public centrocosto As String
        Public partida As String
        Public numPlegador As String
        Public numPlegadorEntregado As String
        Public longPlegadorEntregado As String
        Public usuarioCrea As String
        Public fecha_Creacion As DateTime
        Public usuarioMod As String
        Public fecha_Modifica As DateTime
        Private objConsulta As New NM_Consulta
        Private objUtil As New NM_General.Util

        Public Sub insert()
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCrudoDenimProceso " & _
            " (fecha, centro_costo, partida,numero_plegador_entregado,longitud_plegador_entregado)" & _
            "values ('" & objUtil.FormatFechaHora(fecha) & "', '" & centrocosto & "', '" & partida & "','" & numPlegadorEntregado & "','" & longPlegadorEntregado & "')"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub eliminar() 'Por definir
            Dim strsql As String
            strsql = "DELETE from NM_ControlInventarioCrudoDenimProceso " & _
            "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 and centro_costo = '" & centrocosto & "' "
            objConsulta.Execute(strsql)
        End Sub
        Public Function eliminar(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean 'Por definir
            Dim strsql As String, objConn As New NM_Consulta
            strsql = "DELETE from NM_ControlInventarioCrudoDenimProceso " & _
            "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            objConn.Execute(strsql)
        End Function

        Public Sub actualizar()
        End Sub

        Public Function listar() As DataTable
            Return objConsulta.getData("NM_ControlInventarioCrudoDenimProceso")
        End Function

    End Class

    Public Class TCInventarioCrudoDenimPartidas
        Public Fecha As Date
        Public CentroCosto As String
        Public CodigoPartida As String
        Private objConsulta As New NM_Consulta
        Private objUtil As New NM_General.Util

        Public Sub insertar()
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCrudoDenimPartidas " & _
            "(fecha, centro_costo, codigo_partida) values(convert(datetime, '" & _
            objUtil.FormatFecha(Fecha) & "'), '" & CentroCosto & "', '" & CodigoPartida & "')"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub eliminar() ' elimina un regisdtro dado el codigo de maquina
            Dim strsql As String
            strsql = "Delete from NM_ControlInventarioCrudoDenimPartidas " & _
            " where codigo_partida '" & CodigoPartida & "' and centro_costo '" & CentroCosto & "'"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub eliminar(ByVal pfecha As Date) ' elimina todos los registros que tengan la misma fecha
            Dim strsql As String
            strsql = "Delete from NM_ControlInventarioCrudoDenimPartidas " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 "
            objConsulta.Execute(strsql)
        End Sub

        Public Function eliminar(ByVal pfecha As Date, ByVal pCentroCosto As String) As Boolean ' elimina todos los registros que tengan la misma fecha
            Dim strsql As String
            strsql = "Delete from NM_ControlInventarioCrudoDenimPartidas " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            objConsulta.Execute(strsql)
        End Function

    End Class

    Public Class TCInventarioCrudoDenim  'clase que que maneja el control de inventarios en proceso d criuso y denim
        Public fecha As Date
        Public horaInicio As String ' se debe cambiar a datetime cuando en la BD se haga el cambio respesctivo
        Public horaFin As String
        Public responsable As String
        Public area As String
        Public centro_costo As String
        Public Detalle As New DataTable   'PArtidas de urdido para teñir /engomar
        Public objConsulta As New NM_Consulta
        Public DetallePartidaProceso As New DataTable   'PArtidas en proceso
        Private objUtil As New NM_General.Util

        Public Sub New()
            Detalle.Columns.Add("Fecha", Type.GetType("System.DateTime"))
            Detalle.Columns.Add("codigo_partida", Type.GetType("System.String"))
            With DetallePartidaProceso.Columns
                .Add("Fecha", Type.GetType("System.DateTime"))
                .Add("PartidaEngomado", Type.GetType("System.String"))
                .Add("PlegadorEntregado", Type.GetType("System.String"))
                .Add("Longitud", Type.GetType("System.String")) 'longitud plegador entregado
            End With
        End Sub
        'agrega un nueva fila al dataset que contiene partidas en proceso
        Public Sub AgregarPartidaProceso(ByVal PartidaEngomado As String, ByVal PlegadorEntregado As String, ByVal Longitud As String) ' agrega a un datatable
            Dim fila As DataRow
            fila = DetallePartidaProceso.NewRow()
            fila("PartidaEngomado") = PartidaEngomado
            fila("PlegadorEntregado") = PlegadorEntregado
            fila("Longitud") = Longitud
            DetallePartidaProceso.Rows.Add(fila)
        End Sub
        'agrega un nueva fila al dataset 
        Public Sub AgregarFilaDetalle(ByVal pCodigoPartida As String)
            Dim fila As DataRow
            fila = Detalle.NewRow()
            fila("codigo_partida") = pCodigoPartida
            Detalle.Rows.Add(fila)
        End Sub
        Public Sub RegistraControlInventario() ' registra en la tabla detalle: NM_ControlInventarioCrudoDenimPartidas
            Dim i As Integer
            Dim objDetalleControl As New TCInventarioCrudoDenimPartidas
            Dim objCInvProceso As New TCInventarioCrudoDenimProceso
            Try
                If Exist(fecha, centro_costo) = False Then
                    insertar()
                End If
                objDetalleControl.eliminar(fecha, centro_costo)
                For i = 0 To Detalle.Rows.Count - 1
                    objDetalleControl = New TCInventarioCrudoDenimPartidas
                    objDetalleControl.Fecha = fecha ' Se le asigan la misam fecha que posee el maestro
                    objDetalleControl.CodigoPartida = Detalle.Rows(i).Item("Codigo_Partida").ToString
                    objDetalleControl.CentroCosto = centro_costo
                    objDetalleControl.insertar() ' inserta en la base de datos un nuevo plegador en reserva
                Next i

                objCInvProceso.eliminar(fecha, centro_costo)

                For i = 0 To DetallePartidaProceso.Rows.Count - 1
                    objCInvProceso = New TCInventarioCrudoDenimProceso
                    With DetallePartidaProceso.Rows(i)
                        objCInvProceso.fecha = fecha
                        objCInvProceso.centrocosto = centro_costo
                        objCInvProceso.partida = .Item("partida").ToString  ' Se le asigan la misam fecha que posee el maestro
                        objCInvProceso.numPlegadorEntregado = .Item("numero_plegador_entregado").ToString
                        objCInvProceso.longPlegadorEntregado = .Item("longitud_plegador_entregado").ToString  ' inserta en la base de datos un nuevo plegador en reserva
                    End With
                    objCInvProceso.insert()
                Next i
            Catch
                Throw
            End Try
        End Sub
        Public Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dtInv As New DataTable
            sql = "Select * from NM_ControlInventarioCrudoDenim where " & _
            " DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 " & _
            " and centro_costo = '" & pCentroCosto & "' "
            dtInv = objConn.Query(sql)
            Return (dtInv.Rows.Count > 0)
        End Function
        Public Sub Seek(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dtInv As New DataTable, drInv As DataRow
            sql = "Select * from NM_ControlInventarioCrudoDenim where " & _
            " DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 " & _
            " and centro_costo = '" & pCentroCosto & "' "
            dtInv = objConn.Query(sql)
            For Each drInv In dtInv.Rows
                Me.centro_costo = drInv("centro_costo")
                Me.fecha = drInv("fecha")
                Me.responsable = drInv("persona_responsable")
            Next
        End Sub
        Private Sub insertar()
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCrudoDenim (fecha,hora_inicio,hora_fin,persona_responsable,centro_costo)" & _
            " values ('" & objUtil.FormatFechaHora(fecha) & "','" & horaInicio & "','" & horaFin & _
                               "','" & responsable & "','" & centro_costo & "')"
            objConsulta.Query(strsql)
        End Sub
        Public Sub eliminar()
            Dim sql As String, objConn As New NM_Consulta
            sql = "Select * from NM_ControlInventarioCrudoDenim where " & _
            " DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 " & _
            " and centro_costo = '" & centro_costo & "' "
        End Sub
        Public Sub actualizar()

        End Sub
        Public Function listar() As DataTable

        End Function
        ' obtiene todas las partidas de urdidos de a tabla NM_ControlInventarioCrudoDenimPartidas   
        Public Function getPartidasUrdidos() As DataTable
            Return objConsulta.getData("NM_PartidaUrdido")
        End Function
        'Obtiene
        Public Function getPartidasEngomado() As DataTable
            Return objConsulta.getData("NM_PartidaEngomadoYTED")
        End Function
        ' obtiene todos los registros de la tabla NM_ControlInventarioCrudoDenimPartidas asociadas al ContrlInventarioCrudoDenim  
        Public Function getDetalle() As DataTable ' obtiene todos los registros del control de inventario de crudo denim Partidas
            Dim strsql As String
            strsql = "Select fecha, codigo_partida from NM_ControlInventarioCrudoDenimPartidas "
            strsql = strsql & "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0"
            Return objConsulta.Query(strsql)
        End Function

        Public Function ListUrdido() As DataTable ' obtiene todos los registros del control de inventario de crudo denim Partidas
            Dim strsql As String
            strsql = "Select fecha, codigo_partida from NM_ControlInventarioCrudoDenimPartidas "
            strsql = strsql & "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 and centro_costo = '" & Me.centro_costo & "' "
            Return objConsulta.Query(strsql)
        End Function

        Public Function ListEngomado() As DataTable ' obtiene todos los registros del control de inventario de crudo denim Partidas
            Dim strsql As String
            strsql = "Select partida, numero_plegador_entregado, longitud_plegador_entregado " & _
            " from NM_ControlInventarioCrudoDenimProceso " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 and centro_costo = '" & centro_costo & "' "
            Return objConsulta.Query(strsql)
        End Function

        ' obtiene todos los registros de la tabla NM_ControlInventarioCrudoDenimProceso asociadas al ContrlInventarioCrudoDenim  
        Public Function getPartidasProceso() As DataTable ' obtiene todos los registros del control de inventario de crudo denim Partidas
            Dim strsql As String
            strsql = "Select partida, numero_plegador_entregado, longitud_plegador_entregado " & _
            " from NM_ControlInventarioCrudoDenimProceso " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(fecha) & "') = 0 "
            Return objConsulta.Query(strsql)
        End Function

    End Class

    '-----------------------DISTRIBUICIONMAQUINA---------------------------------
    Public Class TDistribucionMaquina
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta

        Public Sub New()

        End Sub

        Public Sub insertar(ByVal pCod_Activo As String, ByVal pCodigo_maquina As String, ByVal pPorcentaje As Integer)
            Dim strsql As String
            strsql = "insert into NM_DistribucionMaquina (codigo_activo,codigo_maquina"
            Dim values As String = ") values (" & "'" & pCod_Activo & "'" & ",'" & pCodigo_maquina & "'"
            Dim columns As String
            If pPorcentaje >= 0 Then
                columns = columns & ", porcentaje"
                values = values & "," & pPorcentaje
            End If
            strsql = strsql + columns + values & ")"
            If pCod_Activo <> "" And pCodigo_maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                    '  MsgBox(strsql)
                Catch ex As Exception
                    Throw New System.Exception("Error al insertar")
                End Try
            Else
                Throw New System.Exception("Error al insertar: Codigo activo vacio")
            End If
        End Sub

        Public Sub eliminar(ByVal pCod_Activo As String)
            Dim strsql As String = "Delete from NM_DistribucionMaquina where codigo_activo = '" + pCod_Activo + "'"

            If pCod_Activo <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    MsgBox(ex.Message())
                End Try
            Else
                Throw New System.Exception("Error al eliminar: codigo activo vacio")
            End If
        End Sub

        Public Sub actualizar(ByVal pCod_Activo As String, ByVal pCodigo_maquina As String, ByVal pPorcentaje As Integer)
            Dim campos As String
            Dim strsql As String = "UPDATE NM_DistribucionMaquina SET "
            If pCodigo_maquina <> "" Then
                campos = campos & " codigo_maquina = '" & pCodigo_maquina & "',"
            End If
            If pPorcentaje >= 0 Then
                campos = campos & " porcentaje = " & pPorcentaje & ","
            End If
            strsql = strsql & campos
            'MsgBox(strsql.Length)
            strsql = strsql.Remove((strsql.Length - 1), 1) & _
                     " where codigo_activo ='" & pCod_Activo & "'"
            If pCod_Activo <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message())
                End Try
            End If


        End Sub
        Function Listar() As DataTable
            Return objConsulta.getData("NM_DistribucionMaquina")
        End Function
    End Class


    '------------------------DISTRIBUCIONDEPRECIACION-----------------------
    Public Class DistribucionDepreciacion
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta
        Public Sub insertar()

        End Sub
        Public Sub eliminar()

        End Sub
        Public Sub actualizar()

        End Sub
        Function listar() As DataTable
            Return objConsulta.getData("NM_DistribucionDepreciacion")
        End Function

    End Class


    '-----------------------------DISTRIBUCION ENERGETICOS---------------------------
    Public Class DistribucionEnergeticos
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta
        Public Sub insertar(ByVal pcod_maquina As String, ByVal pKilowat As Double, ByVal pVelocidad As Double, _
        ByVal pCorriente As Double, ByVal pCorrienteVacio As Double, ByVal pvoltios As Double, ByVal pfactPotencia As Double)
            Dim strsql As String = "insert into NM_DistribucionEnergeticos (codigo_maquina"
            Dim values As String = ") values (" & "'" & pcod_maquina & "'"
            Dim columns As String
            If pKilowat >= 0 Then
                columns = columns & ", Kilowat"
                values = values & "," & pKilowat
            End If
            If pVelocidad >= 0 Then
                columns = columns & ", velocidad"
                values = values & "," & pVelocidad
            End If
            If pCorriente >= 0 Then
                columns = columns & ", corriente"
                values = values & "," & pCorriente
            End If
            If pCorrienteVacio >= 0 Then
                columns = columns & ", corriente_vacio"
                values = values & "," & pCorrienteVacio
            End If
            If pvoltios >= 0 Then
                columns = columns & ", voltios"
                values = values & "," & pvoltios
            End If
            If pfactPotencia >= 0 Then
                columns = columns & ", factor_potencia"
                values = values & "," & pfactPotencia
            End If
            strsql = strsql + columns + values & ")"

            If pcod_maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                End Try
            Else
                Throw New System.Exception("Error al insertar: codigo de maquina vacio")
            End If
        End Sub

        Public Sub eliminar(ByVal pCodMaquina As String)
            Dim strsql As String = "Delete from NM_DistribucionEnergeticos where codigo_maquina = '" + pCodMaquina + "'"
            If pCodMaquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message())
                End Try
            Else
                Throw New System.Exception("Error al eliminar")
            End If
        End Sub
        Public Sub Actualizar(ByVal pcod_maquina As String, ByVal pKilowat As Double, ByVal pVelocidad As Double, _
        ByVal pCorriente As Double, ByVal pCorrienteVacio As Double, ByVal pvoltios As Double, ByVal pfactPotencia As Double)
            Dim campos As String
            Dim strsql As String = "UPDATE NM_DistribucionEnergeticos SET "
            If pKilowat >= 0 Then
                campos = campos & " Kilowat = " & pKilowat & ","
            End If
            If pVelocidad >= 0 Then
                campos = campos & " velocidad = " & pVelocidad & ","
            End If
            If pCorriente >= 0 Then
                campos = campos & " corriente = " & pCorriente & ","
            End If
            If pCorrienteVacio >= 0 Then
                campos = campos & " corriente_vacio = " & pCorrienteVacio & ","
            End If
            If pvoltios >= 0 Then
                campos = campos & " voltios = " & pvoltios & ","
            End If
            If pfactPotencia >= 0 Then
                campos = campos & " factor_potencia = " & pfactPotencia & ","
            End If
            strsql = strsql & campos
            'MsgBox(strsql.Length)
            strsql = strsql.Remove((strsql.Length - 1), 1) & _
                     " where codigo_maquina ='" & pcod_maquina & "'"
            If pcod_maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message())
                End Try
            Else
                Throw New System.Exception("Error al actualizar")
            End If

        End Sub
        Function listar() As DataTable
            Return objConsulta.getData("NM_DistribucionEnergeticos")
        End Function

    End Class


    '-----------------------------DISTRIBUCION AGUA---------------------------
    Public Class DistribucionAgua
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta
        Public Sub New()

        End Sub

        Public Sub insertar(ByVal pCod_maquina As String, ByVal pconsumoITS As Double, _
                 ByVal pConsumoLIH As Double, ByVal pConsumoITM As Double)
            Dim strsql As String = "insert into NM_DistribucionAgua (codigo_maquina"
            Dim values As String = ") values (" & "'" & pCod_maquina & "'"
            Dim columns As String
            If pconsumoITS >= 0 Then
                columns = columns & ", consumo_lts"
                values = values & "," & pconsumoITS
            End If
            If pConsumoLIH >= 0 Then
                columns = columns & ", consumpo_lih"
                values = values & "," & pConsumoLIH
            End If
            If pConsumoITM >= 0 Then
                columns = columns & ", preparacion_ltm"
                values = values & "," & pConsumoITM
            End If
            strsql = strsql + columns + values & ")"
            If pCod_maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                End Try
            End If

        End Sub

        Public Sub eliminar(ByVal pCod_maquina As String)
            Dim strsql As String = "Delete from NM_DistribucionAgua where codigo_activo = '" + pCod_maquina + "'"
            objConsulta.Execute(strsql)
        End Sub

        Public Sub Actualizar(ByVal pCod_maquina As String, ByVal pconsumoITS As Double, _
                 ByVal pConsumoLIH As Double, ByVal pConsumoLTM As Double)
            Dim strsql As String = "UPDATE NM_DistribucionAgua" & _
                        " SET consumo_lts = " & pconsumoITS & _
                        ", consumpo_lih = " & pConsumoLIH & _
                        ", preparacion_ltm = " & pConsumoLTM & _
                        " WHERE codigo_maquina = '" + pCod_maquina + "'"
            objConsulta.Execute(strsql)
        End Sub

        Function listar() As DataTable
            Return objConsulta.getData("NM_DistribucionAgua")
        End Function

    End Class

    '-------------------------DISTRIBUCIONDIESELGAS-------------------------------------
    Public Class DistribucionDieselGas
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta

        Public Sub New()

        End Sub

        Public Sub insertar(ByVal pCod_maquina As String, ByVal pconsumoDS2 As Double, _
                ByVal pConsumoGAS As Double)
            Dim strsql As String = "insert into NM_DistribucionEnergeticos (codigo_maquina"
            Dim values As String = ") values (" & "'" & pCod_maquina & "'"
            Dim columns As String
            If pconsumoDS2 >= 0 Then
                columns = columns & ", Kilowat"
                values = values & "," & pconsumoDS2
            End If
            If pConsumoGAS >= 0 Then
                columns = columns & ", velocidad"
                values = values & "," & pConsumoGAS
            End If

            If pCod_maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                End Try
            Else
                Throw New System.Exception("Codigo maquina missing")
            End If
        End Sub

        Public Sub eliminar(ByVal pCodMaquina As String)
            Dim strsql As String = "Delete from NM_DistribucionEnergeticos where codigo_maquina = '" + pCodMaquina + "'"
            If pCodMaquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message())
                End Try
            Else
                Throw New System.Exception("Error al eliminar")
            End If
        End Sub

        Public Sub actualizar(ByVal pCod_maquina As String, ByVal pconsumoDS2 As Double, _
                ByVal pConsumoGAS As Double)
            Dim campos As String
            Dim strsql As String = "UPDATE NM_DistribucionEnergeticos SET "
            If pconsumoDS2 >= 0 Then
                campos = campos & " consumo_ds2 = " & pconsumoDS2 & ","
            End If
            If pConsumoGAS >= 0 Then
                campos = campos & " consumo_gas = " & pConsumoGAS & ","
            End If
            strsql = strsql & campos
            strsql = strsql.Remove((strsql.Length - 1), 1) & _
                 " where codigo_maquina ='" & pCod_maquina & "'"   '--Se elimina la ultima coma y se inserta el parametro de busqda
            If pCod_maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message())
                End Try
            Else
                Throw New System.Exception("Error al actualizar")
            End If
        End Sub

        Function listar() As DataTable
            Return objConsulta.getData("NM_DistribucionEnergeticos")
        End Function

    End Class


    '-------------------DISTRIBUCION AIRE-------------------------------------
    Public Class DistribucionAire
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta
        Public Sub New()

        End Sub

        Public Sub Actualizar(ByVal pCodigoMaquina As String, ByVal pNumHusos As Integer, ByVal pCaudal As Double)
            Dim strsql As String = "UPDATE NM_DistribucionAire " & _
                                    "SET numero_husos = " + pNumHusos & _
                                    ", caudal = " + pCaudal & _
                                    "WHERE codigo_activo = '" + pCodigoMaquina + ")"
            If (pCodigoMaquina <> "") Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                End Try
            End If

        End Sub
        Public Sub eliminar(ByVal pCodigo_Maquina As String)
            Dim strsql As String = "Delete from NM_DistribucionAire where codigo_activo = '" + pCodigo_Maquina + "'"
            If pCodigo_Maquina <> "" Then
                Try
                    objConsulta.Execute(strsql)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                End Try
            End If

        End Sub
        Public Sub Insertar(ByVal pCodigoMaquina As String, ByVal pNumHusos As Integer, ByVal pCaudal As Integer)
            Dim strsql As String = "Insert into NM_DistribucionAire values " & _
            "('" & pCodigoMaquina & "', " & pNumHusos & ", " & pCaudal & ")"
            If (pCodigoMaquina <> "") And (pNumHusos >= 0) Then
                objConsulta.Execute(strsql)
            End If

        End Sub
        Function listar() As DataTable
            Return objConsulta.getData("NM_DistribucionDieselGas")
        End Function


    End Class


    '----------------DISTRIBUCIONVAPOR--------------------------------
    Public Class DistribucionVapor

        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta

        Public Sub insertar(ByVal pCod_maquina As String, ByVal pConsumokgh As Integer, ByVal pConsumoPreparacion As Integer, ByVal pFactPreparacion As Integer)
            Dim strsql As String = "Insert into NM_DistribucionVapor values " & _
            "('" + pCod_maquina + "', " & pConsumokgh & ", " & pConsumoPreparacion & ", " & pFactPreparacion & ")"
            Try
                If pCod_maquina <> "" Then
                    objConsulta.Execute(strsql)
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try


        End Sub

        Public Sub eliminar(ByVal pCod_maquina As String)
            Dim strsql As String = "Delete from NM_DistribucionVapor where codigo_maquina ='" + pCod_maquina + "'"
            If pCod_maquina <> "" Then
                objConsulta.Execute(strsql)
            End If
        End Sub

        Public Sub actualizar(ByVal pCod_maquina As String, ByVal pConsumokgh As Integer, ByVal pConsumoPreparacion As Integer _
                              , ByVal pFactPreparacion As Integer)
            Dim strsql As String = "UPDATE NM_DistribucionVapor " & _
                                    "SET  consumo_kgh = " & pConsumokgh & _
                                    ", consumpo_preparacio= " & pConsumoPreparacion & _
                                    ", factor_preparacion = " & pFactPreparacion & _
                                    "WHERE  codigo_maquina = '" & pCod_maquina + "'"
            objConsulta.Execute(strsql)

        End Sub

        Function listar() As DataTable
            Return objConsulta.getData("NM_DistribucionVapor")
        End Function

    End Class


End Namespace
Namespace NM_Tintoreria
    Public Class Class0

    End Class
End Namespace