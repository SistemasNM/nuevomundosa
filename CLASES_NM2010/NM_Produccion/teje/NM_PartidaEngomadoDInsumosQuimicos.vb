Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_PartidaEngomadoDInsumosQuimicos

        Dim BD As New NM_Consulta()
        Public Usuario As String

        Public Sub Insertar(ByVal codigoPartidaEngomado As String, ByVal numeroPreparacion As Integer, ByVal codigoInsumo As String, ByVal cantidad As Double)
            Dim strSQL As String
            Try
                If Not (codigoPartidaEngomado Is Nothing And numeroPreparacion = 0 And codigoInsumo Is Nothing) Then
                    strSQL = "INSERT INTO NM_PartidaEngomadoDInsumosQuimicos " & _
                        "(codigo_partida_engomadoted, numero_preparacion, codigo_insumo_quimico, " & _
                        "cantidad, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        codigoPartidaEngomado & "'," & _
                        numeroPreparacion & ",'" & _
                        codigoInsumo & "'," & _
                        cantidad & ",'" & _
                        Usuario & "'," & _
                        "GetDate())"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque falta codigo_partida_engomadoted.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub Actualizar(ByVal codigoPartidaEngomado As String, ByVal codigoInsumo As String, ByVal numPreparacion As Integer, ByVal cantidad As Double)
            Try
                If codigoPartidaEngomado <> "" And codigoInsumo <> "" And numPreparacion <> 0 Then
                    Dim strSQL = "UPDATE NM_PartidaEngomadoDInsumosQuimicos " & _
                        "SET cantidad = " & cantidad & ", " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_partida_engomadoted = '" & codigoPartidaEngomado & "' " & _
                        "AND codigo_insumo_quimico = '" & codigoInsumo & "' " & _
                        "AND numero_preparacion = " & numPreparacion
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede actualizar porque el código no es válido.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class


End Namespace