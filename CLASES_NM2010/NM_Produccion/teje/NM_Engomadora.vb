Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Engomadora
        Friend objGen As New NM_General.NM_BaseDatos.NM_Consulta()
        Friend objMaquina As New NM_Maquina
        Public Codigo_maqina As String
        Public Nombre_maquina As String

        Public Function Add(ByVal CodigoEngomadora As String, ByVal DatosEngomadora As String)
            Dim sql As String, codErr As Integer = 0
            Dim objTable1 As New DataTable()
            Try
                If CodigoEngomadora <> "" And DatosEngomadora <> "" Then
                    objTable1 = objMaquina.Obtener(CodigoEngomadora)
                    If objTable1.Rows.Count > 0 Then
                        sql = "Insert into NM_Engomadora (" & _
                        "codigo_engomadora, datos_engomadora) values('" & CodigoEngomadora & _
                        "','" & DatosEngomadora & "')"
                        codErr = objGen.Execute(sql)
                    Else
                        codErr = 1
                    End If
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Public Function Delete(ByVal CodigoEngomadora As String)
            Dim sql As String, codErr As Integer = 0
            Try
                If CodigoEngomadora <> "" Then
                    sql = "Delete from NM_Engomadora where codigo_engomadora = '" & CodigoEngomadora & "'"
                    codErr = objGen.Execute(sql)
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Public Function Update(ByVal CodigoEngomadora As String, ByVal DatosEngomadora As String)
            Dim sql As String, codErr As Integer = 0
            Dim objTable1 As New DataTable()
            Try
                If CodigoEngomadora <> "" And DatosEngomadora Then
                    objTable1 = objMaquina.Obtener(CodigoEngomadora)
                    If objTable1.Rows.Count > 0 Then
                        sql = "Update NM_Engomadora set " & _
                        "codigo_engomadora = '" & CodigoEngomadora & "', datos_engomadora = '" & _
                        DatosEngomadora & "' where codigo_engomadora = '" & CodigoEngomadora & _
                        "'"
                        codErr = objGen.Execute(sql)
                    End If
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Public Function List() As DataTable
            Dim sql As String, objDT As New DataTable
            sql = "Select codigo_maquina, datos_engomadora " & _
            " from NM_Engomadora "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Sub Seek(ByVal sCodigoMaquina As String)
            Dim sql As String, objDT As New DataTable, fila As DataRow
            sql = "Select codigo_maquina, datos_engomadora " & _
            " from NM_Engomadora where codigo_maquina ='" & _
            sCodigoMaquina & "' "
            objDT = objGen.Query(sql)
            For Each fila In objDT.Rows
                Codigo_maqina = fila("codigo_maquina")
                Nombre_maquina = fila("datos_engomadora")
            Next
        End Sub
    End Class

End Namespace