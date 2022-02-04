Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_GrupoSustituto

        Public Usuario As String
        Public codigo_sustituto As String
        Public codigo_tipo_reemplazo As String
        Public codigo_grupo As String
        Public fecha_inicio As String
        Public fecha_fin As String
        Public turno As String

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_grupo <> "" Then
                Dim sql = "INSERT INTO NM_GrupoSustituto " & _
                    "(codigo_sustituto, codigo_tipo_reemplazo, " & _
                    "codigo_grupo, fecha_inicio, fecha_fin, " & _
                    "turno, usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_sustituto & "', '" & _
                    codigo_tipo_reemplazo & "', '" & _
                    codigo_grupo & "', '" & _
                    fecha_inicio & "', '" & _
                    fecha_fin & "', '" & _
                    turno & "', '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_grupo <> "" Then
                Dim sql = "UPDATE NM_GrupoSustituto " & _
                    "SET codigo_tipo_reemplazo = '" & codigo_tipo_reemplazo & "', " & _
                    "codigo_grupo = '" & codigo_grupo & "', " & _
                    "fecha_inicio = '" & fecha_inicio & "', " & _
                    "fecha_fin = '" & fecha_fin & "', " & _
                    "turno = '" & turno & "', " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_sustituto = '" & codigo_sustituto & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoSustituto As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_GrupoSustituto where codigo_sustituto = '" & codigoSustituto & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_sustituto = objDR("codigo_sustituto")
                codigo_tipo_reemplazo = objDR("codigo_tipo_reemplazo")
                codigo_grupo = objDR("codigo_grupo")
                fecha_inicio = objDR("fecha_inicio")
                fecha_fin = objDR("fecha_fin")
                turno = objDR("turno")
            Next

            'Dim programaD As New NM_ProgramaD()
            'Detalle = programaD.List(codigoPrograma)
        End Sub

        Public Sub SeekXGrupo(ByVal codigoGrupo As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_GrupoSustituto where codigo_grupo = '" & codigoGrupo & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_sustituto = objDR("codigo_sustituto")
                codigo_tipo_reemplazo = objDR("codigo_tipo_reemplazo")
                codigo_grupo = objDR("codigo_grupo")
                fecha_inicio = objDR("fecha_inicio")
                fecha_fin = objDR("fecha_fin")
                turno = objDR("turno")
            Next
        End Sub

        Function List()
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_GrupoSustituto "
            Return bd.Query(sql)
        End Function

    End Class


End Namespace
