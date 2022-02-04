Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Mecanico
        Friend objGen As New NM_General.NM_BaseDatos.NM_Consulta()

        Public codigo_mecanico As String
        Public descripcion_mecanico As String
        Public dttelares As New DataTable()

        Public Function Add(ByVal Codigo As String, ByVal Nombre As String) As Integer
            Dim sql As String, codErr As Integer = 0
            Try
                If Codigo <> "" AndAlso Nombre <> "" Then
                    sql = "Insert into NM_Mecanico (" & _
                    "codigo_mecanico, descripcion_mecanico) values('" & Codigo & _
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
                    sql = "Delete from NM_Mecanico where codigo_mecanico = '" & Codigo & "'"
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

        Public Function Update(ByVal Codigo As String, ByVal Nombre As String) As String
            Dim sql As String, codErr As Integer = 0
            Try
                If Codigo <> "" AndAlso Nombre <> "" Then
                    sql = "Update NM_Mecanico set " & _
                    "codigo_mecanico = '" & Codigo & "', descripcion_mecanico = " & _
                    "'" & Nombre & "' where codigo_mecanico='" & Codigo & "'"
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
            sql = "Select codigo_mecanico, descripcion_mecanico " & _
            " from NM_Mecanico "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal Codigo As String) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select codigo_mecanico, descripcion_mecanico " & _
            " from NM_Mecanico where codigo_mecanico = '" & _
            Codigo & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function Exist(ByVal sCodigoMecanico As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable()
            sql = "Select * from NM_Mecanico where codigo_mecanico='" & sCodigoMecanico & "'"
            dt = objConn.Query(sql)
            Return (dt.Rows.Count > 0)
        End Function

        Public Sub seek(ByVal pcodigo_mecanico As String)
            Dim tabla As DataTable
            Dim fila As DataRow
            Dim telMec As New NM_TelarMecanico()
            tabla = Obtener(pcodigo_mecanico)
            If tabla.Rows.Count > 0 Then
                For Each fila In tabla.Rows
                    If Not IsDBNull(fila("codigo_mecanico")) Then codigo_mecanico = fila("codigo_mecanico")
                    If Not IsDBNull(fila("descripcion_mecanico")) Then descripcion_mecanico = fila("descripcion_mecanico")
                    Exit For
                Next
                dttelares = telMec.Obtener(pcodigo_mecanico)
            End If
        End Sub
    End Class

End Namespace
