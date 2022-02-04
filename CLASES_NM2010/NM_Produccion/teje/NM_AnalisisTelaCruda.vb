Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_AnalisisTelaCruda

        Dim BD As New NM_Consulta()
        Public Usuario As String

        Public numero_pieza As String
        Public codigo_articulo As String
        Public codigo_telar As String
        Public fecha As Date
        Public codigo_estandar As String
        Public valor As Integer
        Public estandares As DataTable
        Private objUtil As New NM_General.Util

        Sub Seek(ByVal numeroPieza As String)

            estandares = LoadDT(numeroPieza)
        End Sub

        Public Sub Insert()
            Try
                If Not numero_pieza = "" Then
                    Dim strSQL = "INSERT INTO NM_AnalisisTelaCruda " & _
                        "(fecha, numero_pieza, codigo_articulo, codigo_telar, codigo_estandar, " & _
                        "valor, " & _
                        "usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        objUtil.FormatFechaHora(fecha) & "', '" & _
                        numero_pieza & "', '" & _
                        codigo_articulo & "', '" & _
                        codigo_telar & "', '" & _
                        codigo_estandar & "', " & _
                        valor & ", '" & _
                        Usuario & "'," & _
                        "GetDate())"
                    BD.Execute(strSQL)
                Else
                    Throw New Exception("No se puede insertar porque el código es incorrecto.")
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Sub Update()
            If Not numero_pieza = "" Then
                Dim strSQL = "UPDATE NM_AnalisisTelaCruda " & _
                    "SET " & _
                    "fecha = '" & objUtil.FormatFechaHora(fecha) & "', " & _
                    "numero_pieza = '" & numero_pieza & "', " & _
                    "codigo_articulo = '" & codigo_articulo & "', " & _
                    "codigo_telar = '" & codigo_telar & "', " & _
                    "codigo_estandar = '" & codigo_estandar & "', " & _
                    "valor = " & valor & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE numero_pieza = '" & numero_pieza & "' " & _
                    "AND codigo_estandar = '" & codigo_estandar & "' "
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede actualizar porque el código es incorrecto.")
            End If

        End Sub

        Function List() As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql = "Select * from NM_AnalisisTelaCruda "
            Return objGen.Query(sql)
        End Function

        Function LoadDT(ByVal numeroPieza As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql = "Select * from NM_Estandares e LEFT JOIN NM_AnalisisTelaCruda atc " & _
                "ON e.codigo_estandar = atc.codigo_estandar " & _
                "AND numero_pieza = '" & numeroPieza & "'"
            Return objGen.Query(sql)
        End Function

        Sub Delete(ByVal numeroPieza As String, ByVal codigoEstandar As String)

            If numeroPieza <> "" And codigoEstandar <> "" Then
                Dim strSQL = "DELETE FROM NM_AnalisisTelaCruda " & _
                    "WHERE numero_pieza = '" & numeroPieza & "' " & _
                    "AND codigo_estandar = '" & codigoEstandar & "' "
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Sub

        Function Exist(ByVal numeroPieza As String, ByVal codigoEstandar As String)
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "SELECT * FROM NM_AnalisisTelaCruda " & _
                    "WHERE numero_pieza = '" & numero_pieza & "' " & _
                    "AND codigo_estandar = '" & codigo_estandar & "' "
            objDT = BD.Query(sql)

            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


    End Class

End Namespace