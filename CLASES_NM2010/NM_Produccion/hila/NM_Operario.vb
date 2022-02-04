Imports NM_General.NM_BaseDatos
Namespace NM_Hilanderia

    Public Class NM_Operario
        Public codigo_operario As String
        Public codigo_grupo As String
        Public nombre_operario As String
        Public usuario As String
        Public tipo_operario As String
        Public DB As NM_Consulta

        Public Function Add() As Boolean
            If codigo_operario <> "" Then
                DB = New NM_Consulta(4)
                Dim strsql As String
                strsql = "INSERT INTO NM_Operario values("
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("'" & codigo_operario & "',")
                commandString.Append("'" & codigo_grupo & "',")
                commandString.Append("'" & nombre_operario & "',")
                commandString.Append("'" & tipo_operario & "',")
                commandString.Append("'" & usuario & "',")
                commandString.Append("GetDate(),")
                commandString.Append("'" & usuario & "',")
                commandString.Append("GetDate())")
                Return CBool(DB.Execute(commandString.ToString))
            Else
                Return False
            End If
        End Function

        Public Function Update()
            If codigo_operario <> "" Then
                DB = New NM_Consulta(4)
                Dim strsql As String
                strsql = "UPDATE NM_Operario SET "
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("nombre_operario = '" & nombre_operario & "',")
                commandString.Append("tipo_operario = '" & tipo_operario & "',")
                commandString.Append("usuario_modificacion = '" & usuario & "',")
                commandString.Append("fecha_modificacion = getdate()")
                commandString.Append(" where codigo_operario = '" & codigo_operario & "'")
                commandString.Append(" AND codigo_grupo = '" & codigo_grupo & "'")
                Return DB.Execute(commandString.ToString)
                DB = Nothing
            Else
                Return False
            End If
        End Function

        Public Function delete(ByVal pcodigo_grupo As String, ByVal pcodigo_operario As String) As Boolean
            If pcodigo_operario <> "" Then
                Dim strsql As String
                DB = New NM_Consulta(4)
                strsql = "DELETE FROM NM_Operario where codigo_operario = '" & pcodigo_operario & "' " & _
                "AND codigo_grupo = '" & pcodigo_grupo & "'"
                Return DB.Execute(strsql)
            Else
                Return False
            End If
        End Function

        Public Function listXTipo(ByVal pCodGrupo As String, ByVal pTipo As String) As DataTable
            DB = New NM_Consulta(4)
            Dim sql As String = "SELECT * FROM NM_Operario where codigo_grupo='" & pCodGrupo & "' and tipo_operario ='" & pTipo & "'"
            'Throw New Exception(sql)
            Return DB.Query(sql)
        End Function

        'Public Function list()
        '    Dim bd As New NM_Consulta(4)
        '    Dim sql = "SELECT * FROM NM_Operario where "
        '    Return bd.Query(sql)
        'End Function

        Public Function allList(ByVal pTipoGrupo As String, Optional ByVal turno As Integer = 0)
            Dim bd As New NM_Consulta(4)
            Dim strsql As String = "SELECT * FROM NM_Operario O join NM_GrupoOperario GOp "
            strsql = strsql & "ON O.codigo_grupo = GOp.codigo_grupo "
            strsql = strsql & "where O.Tipo_operario = 'N' and GOp.Tipo = '" & pTipoGrupo & "' "
            If turno > 0 Then
                strsql = strsql & "and GOp.turno = " & turno
            End If
            Return bd.Query(strsql)
        End Function

        Public Function allListReemplazo(ByVal pTipoGrupo As String, Optional ByVal turno As Integer = 0)
            Dim bd As New NM_Consulta(4)
            Dim strsql As String = "SELECT * FROM NM_Operario O join NM_GrupoOperario GOp "
            strsql = strsql & "ON O.codigo_grupo = GOp.codigo_grupo "
            strsql = strsql & "where GOp.Tipo = '" & pTipoGrupo & "' and tipo_operario = 'C' "
            If turno > 0 Then
                strsql = strsql & " and GOp.turno = " & turno
            End If
            Return bd.Query(strsql)
        End Function

        Public Function list(ByVal pCodGrupo As String) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_Operario where codigo_grupo='" & pCodGrupo & "'"
            Return bd.Query(sql)
        End Function

        Public Sub seek(ByVal pcodigo_operario As String)
            DB = New NM_Consulta(4)
            Dim strsql As String
            Dim tabla As New DataTable
            strsql = "SELECT * FROM NM_Operario Where codigo_operario = '" & pcodigo_operario & "'"
            tabla = DB.Query(strsql)
            Dim fila As DataRow
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_operario")) Then codigo_operario = fila("codigo_operario")
                If Not IsDBNull(fila("codigo_grupo")) Then codigo_grupo = fila("codigo_grupo")
                If Not IsDBNull(fila("nombre_operario")) Then nombre_operario = fila("nombre_operario")
            Next
            tabla.Dispose()
            DB = Nothing
        End Sub

        Public Function exist(ByVal pcodigo_operario As String) As Boolean
            DB = New NM_Consulta(4)
            Dim tabla As New DataTable
            Dim strsql As String = "SELECT * FROM NM_Operario Where codigo_operario = '" & pcodigo_operario & "'"
            tabla = DB.Query(strsql)
            If tabla.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


    End Class
End Namespace