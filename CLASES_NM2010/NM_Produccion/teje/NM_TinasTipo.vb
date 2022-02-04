Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_TinasTipo
        Public Codigo As Integer
        Public Descripcion As String
        Public Usuario As String
        Public Debug As String

        Dim objGen As New NM_Consulta()

        Sub New()
            Codigo = 0
            Descripcion = ""
        End Sub

        Public Function Add() As Boolean
            Dim sql As String
            Try
                If Codigo <> "" AndAlso Descripcion <> "" Then
                    sql = "Insert into NM_TinasTipo (" & _
                    "codigo_tipo, descripcion_tipo, usuario_creacion, fecha_creacion) values('" & _
                    Codigo & "','" & Descripcion & "','" & Usuario & "',getdate())"
                    objGen.Execute(sql)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal Codigo As String) As Boolean
            Dim sql As String, codErr As Integer = 0
            Try
                If Codigo <> "" Then
                    sql = "Delete from NM_TinasTipo where codigo_tipo = '" & Codigo & "' "
                    objGen.Execute(sql)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim sql As String
            Try
                If Codigo <> "" AndAlso Descripcion <> "" Then
                    sql = "Update NM_TinasTipo codigo_tipo = '" & Codigo & "', descripcion_tipo = " & _
                    Descripcion & "', usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                    " where codigo_tipo =" & Codigo & " "
                    objGen.Execute(sql)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_TinasTipo "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal Codigo As String) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_TinasTipo where codigo_tipo ='" & Codigo & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Exist(ByVal Codigo As String) As Boolean
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_TinasTipo where codigo_tipo ='" & Codigo & "' "
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub Seek(ByVal Codigo As String)
            Dim sql As String, objDT As New DataTable
            sql = "Select * from NM_TinasTipo where codigo_tipo ='" & Codigo & "' "
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Me.Descripcion = objDT.Rows(0).Item("descripcion_tipo")
                Me.Codigo = objDT.Rows(0).Item("codigo_tipo")
            Else
                Me.Descripcion = ""
                Me.Codigo = ""
            End If
        End Sub
    End Class

End Namespace