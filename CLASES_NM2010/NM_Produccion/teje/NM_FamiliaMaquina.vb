Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_FamiliaMaquina
        Friend objGen As New NM_General.NM_BaseDatos.NM_Consulta()

        Public Function Add(ByVal Codigo As String, ByVal Nombre As String) As Integer
            Dim sql As String, codErr As Integer = 0
            Try
                If Codigo <> "" AndAlso Nombre <> "" Then
                    sql = "Insert into NM_FamiliaMaquina (" & _
                    "codigo_familia, nombre_familia) values('" & Codigo & _
                    "','" & Nombre & "')"
                    objGen.Execute(sql)
                    codErr = 1
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
            Return codErr
        End Function

        Public Function Delete(ByVal Codigo As String) As Integer
            Dim sql As String, codErr As Integer = 0
            Try
                If Codigo <> "" Then
                    sql = "Delete from NM_FamiliaMaquina where codigo_familia='" & Codigo & "'"
                    objGen.Execute(sql)
                    codErr = 1
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Public Function Update(ByVal Codigo As String, ByVal Nombre As String) As Integer
            Dim sql As String, codErr As Integer = 0, retorno As String = ""
            Try
                If Codigo <> "" AndAlso Nombre <> "" Then
                    sql = "Update NM_FamiliaMaquina set " & _
                    "codigo_familia = '" & Codigo & "', nombre_familia = " & _
                    "'" & Nombre & "' where codigo_familia='" & Codigo & "' "
                    objGen.Execute(sql)
                    codErr = 1
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
            Return codErr
        End Function

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select codigo_familia, nombre_familia " & _
            " from NM_FamiliaMaquina "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal Codigo As String) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select codigo_familia, nombre_familia " & _
            " from NM_FamiliaMaquina where codigo_familia='" & _
            Codigo & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

    End Class

End Namespace