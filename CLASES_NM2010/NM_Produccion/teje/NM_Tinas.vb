Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Tinas
        Public CodigoTED As String
        Public RevisionTED As Integer
        Public Numero As Integer
        Public Tipo As Integer
        Public PresionRodillo As Double
        Public Tension As Double
        Public Temperatura As Integer
        Public Usuario As String
        Public Debug As String

        Dim objGen As New NM_Consulta()
        Dim objTED As New NM_TED()

        Sub New()
            CodigoTED = ""
            RevisionTED = 0
            Numero = 0
            Tipo = 0
            PresionRodillo = 0
            Tension = 0
            Temperatura = 0
        End Sub

        Public Function Add() As Boolean
            Dim sql As String
            Try
                If CodigoTED <> "" AndAlso Val(Numero) >= 0 AndAlso Val(Tipo) >= 0 Then
                    sql = "Insert into NM_Tinas (" & _
                    "codigo_ted,revision_ted, numero, codigo_tipo ,presion_rodillo" & _
                    ",tension,temperatura, usuario_creacion, fecha_creacion) " & _
                    " values('" & CodigoTED & "'," & RevisionTED & "," & Numero & "," & Tipo & "," & PresionRodillo & "," & _
                    Tension & "," & Temperatura & ",'" & Usuario & "',getdate())"
                    objGen.Execute(sql)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal sCodigoTED As String, ByVal NumeroTina As Integer, _
        ByVal nRevisionTED As Integer) As Boolean
            Dim sql As String, codErr As Integer = 0
            Try
                If sCodigoTED <> "" AndAlso NumeroTina <> "" Then
                    sql = "Delete from NM_Tinas where codigo_ted = '" & sCodigoTED & _
                    "' and numero=" & NumeroTina & " and revision_ted=" & nRevisionTED
                    Return objGen.Execute(sql)
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
                If CodigoTED <> "" AndAlso Val(Numero) >= 0 AndAlso Val(Tipo) >= 0 Then
                    sql = "Update NM_Tinas set codigo_tipo = " & Tipo & ",presion_rodillo = " & _
                    PresionRodillo & ",	tension = " & Tension & ",temperatura = " & _
                    Temperatura & ", usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                    "where codigo_ted ='" & CodigoTED & "' and " & " numero = " & _
                    Numero & " and revision_ted=" & RevisionTED
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function AddReserva(ByVal sCodigoTed As String, ByVal nRevision As Integer) As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Insert into NM_Tinas (codigo_ted,numero, presion_rodillo, tension, temperatura," & _
            "usuario_creacion, fecha_creacion, usuario_modificacion," & _
            " fecha_modificacion, codigo_tipo, revision_ted ) " & _
            "select codigo_ted,numero, presion_rodillo, tension, temperatura," & _
            "usuario_creacion, fecha_creacion, usuario_modificacion," & _
            " fecha_modificacion, codigo_tipo, " & nRevision + 1 & _
            " from NM_Tinas where codigo_ted='" & sCodigoTed & "' and revision_ted=" & nRevision
            Return objConn.Execute(sql)
        End Function

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_Tinas "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal sCodTED As String, ByVal nRevisionTED As Integer) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * from NM_Tinas where codigo_TED='" & sCodTED & "' " & _
            " and revision_ted=" & nRevisionTED
            objDT = objGen.Query(sql)
            Return objDT
        End Function


        Public Function Obtener(ByVal sCodigoTED As String, ByVal Numero As Integer, _
        ByVal nRevisionTED As Integer) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_Tinas where codigo_ted ='" & sCodigoTED & "' and numero=" & Numero & _
            " and revision_ted = " & nRevisionTED
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Exist(ByVal sCodigoTED As String, ByVal Numero As Integer, _
        ByVal nRevisionTED As Integer) As Boolean
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_Tinas where codigo_ted ='" & sCodigoTED & "' and numero=" & Numero & _
            " and revision_ted = " & nRevisionTED
            objDT = objGen.Query(sql)
            return (objDT.Rows.Count > 0)
        End Function

    End Class

End Namespace