Imports System.Data
Imports System.Data.SqlClient
Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia
    Public Class Class0

    End Class
End Namespace
Namespace NM_Tejeduria
    Public Class TCInventarioCrudoDenimPartidas
        Public Fecha As Date
        Public Cod_maquina As String
        Private objConsulta As New NM_Consulta
        'Private objUtil As New NM_Produccion.NM_Util.NM_Util
        Private objUtil As New NM_General.Util

        Public Sub insertar()
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCrudoDenimPartidas (fecha,codigo_partida) values('" & _
            objUtil.FormatFechaHora(Fecha) & "','" & Cod_maquina & "')"
            objConsulta.Execute(strsql)
        End Sub
        Public Sub eliminar() ' elimina un regisdtro dado el codigo de maquina
            Dim strsql As String
            strsql = "Delete from NM_ControlInventarioCrudoDenimPartidas where codigo_partida" & _
                                 Cod_maquina
            objConsulta.Execute(strsql)
        End Sub
        Public Sub eliminar(ByVal pfecha As Date) ' elimina todos los registros que tengan la misma fecha
            Dim strsql As String
            strsql = "Delete from NM_ControlInventarioCrudoDenimPartidas where fecha = '" & objUtil.FormatFechaHora(Fecha) & "'"
            objConsulta.Execute(strsql)
        End Sub
    End Class

    Public Class TCInventarioCrudoDenim  'clase que que maneja el control de inventarios en proceso d criuso y denim
        Public fecha As Date
        Public horaInicio As String ' se debe cambiar a datetime cuando en la BD se haga el cambio respesctivo
        Public horaFin As String
        Public responsable As String
        Public area As String
        Public centro_costo As String
        Public Detalle As New DataTable() 'PArtidas de urdido para teñir /engomar
        Public objConsulta As New NM_Consulta()
        Public DetallePartidaProceso As New DataTable   'PArtidas en proceso
        'Private objUtil As New NM_Produccion.NM_Util.NM_Util
        Private objUtil As New NM_General.Util

        Public Sub New()
            Detalle.Columns.Add("Fecha", Type.GetType("System.DateTime"))
            Detalle.Columns.Add("Cod_Maquina", Type.GetType("System.String"))
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
            fila("Cod_Maquina") = pCodigoPartida
            Detalle.Rows.Add(fila)
        End Sub
        Public Sub RegistraControlInventario() ' registra en la tabla detalle: NM_ControlInventarioCrudoDenimPartidas
            Dim i As Integer
            Dim objDetalleControl As TCInventarioCrudoDenimPartidas
            Dim objCInvProceso As NM_TCInventarioCrudoDenimProceso
            Try
                insertar()
                For i = 0 To Detalle.Rows.Count - 1
                    objDetalleControl = New TCInventarioCrudoDenimPartidas()
                    objDetalleControl.Fecha = fecha ' Se le asigan la misam fecha que posee el maestro
                    objDetalleControl.Cod_maquina = Detalle.Rows(i).Item("Cod_Maquina").ToString
                    objDetalleControl.insertar() ' inserta en la base de datos un nuevo plegador en reserva
                Next i
                For i = 0 To DetallePartidaProceso.Rows.Count - 1
                    objCInvProceso = New NM_TCInventarioCrudoDenimProceso()
                    With DetallePartidaProceso.Rows(i)
                        objCInvProceso.fecha = fecha
                        objCInvProceso.partida = .Item("PartidaEngomado").ToString  ' Se le asigan la misam fecha que posee el maestro
                        objCInvProceso.numPlegadorEntregado = .Item("PlegadorEntregado").ToString
                        objCInvProceso.longPlegadorEntregado = .Item("Longitud").ToString  ' inserta en la base de datos un nuevo plegador en reserva
                    End With
                    objCInvProceso.insert()
                Next i
            Catch
                Throw
            End Try
        End Sub
        Private Sub insertar()
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCrudoDenim (fecha,hora_inicio,hora_fin,persona_responsable,centro_costo)" & _
            " values ('" & objUtil.FormatFechaHora(fecha) & "','" & horaInicio & "','" & horaFin & _
                               "','" & responsable & "','" & centro_costo & "')"
            objConsulta.Query(strsql)
        End Sub
        Public Sub eliminar()

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
            strsql = "Select * from NM_ControlInventarioCrudoDenimPartidas "
            strsql = strsql & "where fecha = '" & objUtil.FormatFechaHora(fecha) & "'"
            Return objConsulta.Query(strsql)
        End Function
        ' obtiene todos los registros de la tabla NM_ControlInventarioCrudoDenimProceso asociadas al ContrlInventarioCrudoDenim  
        Public Function getPartidasProceso() As DataTable ' obtiene todos los registros del control de inventario de crudo denim Partidas
            Dim strsql As String
            strsql = "Select * from NM_ControlInventarioCrudoDenimProceso"
            strsql = strsql & "where fecha = '" & objUtil.FormatFechaHora(fecha) & "'"
            Return objConsulta.Query(strsql)
        End Function

    End Class

    '-----------------------DISTRIBUICIONMAQUINA---------------------------------
    Public Class TDistribucionMaquina
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()

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
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()
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
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()
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
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()
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
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()

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
        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()
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

        Private objConsulta As New NM_General.NM_BaseDatos.NM_Consulta()

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