Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_TelarMecanico
        Friend objGen As New NM_General.NM_BaseDatos.NM_Consulta()
        Friend objMecanico As New NM_Mecanico()
        Friend objTelares As New NM_Telares()
        Public CodigoMecanico As String
        Public CodigoTelar As String
        Public EscuadraMecanico As String
        Public RevisionTelar As String
        Public usuario As String

        'Public Function Add(ByVal CodigoMecanico As String, ByVal CodigoTelar As String) As Integer
        '    Dim sql As String, codErr As Integer = 0
        '    Dim objTable1 As New DataTable(), objTable2 As New DataTable()
        '    Try
        '        If CodigoMecanico <> "" And CodigoTelar <> "" Then
        '            objTable1 = objTelares.Obtener(CodigoTelar)
        '            objTable2 = objMecanico.Obtener(CodigoMecanico)

        '            If objTable1.Rows.Count > 0 AndAlso objTable2.Rows.Count > 0 Then
        '                sql = "Insert into NM_TelarMecanico (" & _
        '                "codigo_mecanico, codigo_telar) values('" & CodigoMecanico & _
        '                "','" & CodigoTelar & "')"
        '                objGen.Execute(sql)
        '                codErr = 1
        '            Else
        '                codErr = 0
        '            End If
        '        Else
        '            codErr = 0
        '        End If
        '    Catch ex As Exception
        '        codErr = 0
        '    End Try
        '    Return codErr
        'End Function

        Public Function Add() As Boolean
            Dim sql As String
            Dim objTable1 As New DataTable(), objTable2 As New DataTable()
            Try

                If CodigoMecanico <> "" And CodigoTelar <> "" Then
                    objTable1 = objTelares.Obtener(CodigoTelar, RevisionTelar)
                    objTable2 = objMecanico.Obtener(CodigoMecanico)

                    If objTable1.Rows.Count > 0 AndAlso objTable2.Rows.Count > 0 Then
                        sql = "Insert into NM_TelarMecanico (" & _
                        "codigo_mecanico, codigo_telar, revision_telar, escuadra_mecanico,usuario_creacion,fecha_creacion) values('" & CodigoMecanico & _
                        "','" & CodigoTelar & "'," & RevisionTelar & ",'" & EscuadraMecanico & "','" & usuario & "', getdate() )"

                        Return objGen.Execute(sql)
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Function Delete(ByVal sCodigoMecanico As String, _
        ByVal sCodigoTelar As String, ByVal nRevision As Integer) As Boolean
            Dim sql As String
            Try
                If sCodigoMecanico <> "" AndAlso sCodigoTelar <> "" AndAlso nRevision >= 0 Then
                    sql = "Delete from NM_TelarMecanico where codigo_mecanico = '" & _
                    sCodigoMecanico & "' and codigo_telar='" & sCodigoTelar & _
                    "' and revision_telar=" & nRevision
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim sql As String, codErr As Integer = 0
            Dim objTable1 As New DataTable(), objTable2 As New DataTable()
            Try
                If CodigoMecanico <> "" AndAlso CodigoTelar <> "" Then
                    If objMecanico.Exist(CodigoMecanico) = True AndAlso _
                    objTelares.Exist(CodigoTelar, RevisionTelar) = True Then
                        sql = "Update NM_TelarMecanico set " & _
                        "codigo_mecanico = '" & CodigoMecanico & "' " & _
                        " where codigo_mecanico = '" & CodigoMecanico & _
                        " and revision_telar=" & RevisionTelar & _
                        " and codigo_telar='" & CodigoTelar & "' "
                        Return objGen.Execute(sql)
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

     
        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select codigo_mecanico, codigo_telar " & _
            " from NM_TelarMecanico "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal dgFormat As Boolean) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "select TM.codigo_telar, TM.codigo_mecanico," & _
            " M.descripcion_maquina , MEC.descripcion_mecanico " & _
            " from NM_TELARES T, NM_MAQUINA M, NM_TELARMECANICO TM, " & _
            " NM_MECANICO MEC " & _
            " where T.codigo_telar = M.codigo_maquina " & _
            " and TM.codigo_telar = M.codigo_maquina " & _
            " and TM.codigo_telar = T.codigo_telar " & _
            " and TM.revision_telar = T.revision_telar " & _
            " and MEC.codigo_mecanico = TM.codigo_mecanico "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal CodigoMecanico As String, Optional ByVal CodigoTelar As String = "") As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select codigo_mecanico, codigo_telar " & _
            " from NM_TelarMecanico where codigo_mecanico ='" & _
            CodigoMecanico & "' "
            If CodigoTelar <> "" Then
                sql = sql & " and codigo_telar = '" & CodigoTelar & "' "
                Throw New Exception(sql)
            End If

            objDT = objGen.Query(sql)
            Return objDT

        End Function

        Function getLastRevision(ByVal sCodigoTelar) As Integer
            Dim dt As New DataTable(), fila As DataRow
            Dim rev As Integer = 0, objConn As New NM_Consulta()
            Dim sql As String
            sql = "Select max(revision_telar) revision from NM_TelarMecanico where " & _
            "codigo_telar='" & sCodigoTelar & "'"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                If IsDBNull(fila("revision")) Then
                    rev = 0
                Else
                    rev = fila("revision")
                End If
            Next
            Return rev
            dt.Dispose()
            dt = Nothing
        End Function



        Public Sub New()

        End Sub
    End Class

End Namespace