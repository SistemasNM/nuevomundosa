Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_GrupoOperario

        Public Usuario As String
        Public codigo_grupo As String
        Public descripcion_grupo As String
        Public tipo As String
        Public fecha_inicio As Date
        Public fecha_fin As Date
        Public turno As Integer
        Private objUtil As New NM_General.Util

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util

            If codigo_grupo <> "" Then
                Dim sql = "INSERT INTO NM_GrupoOperario " & _
                    "(codigo_grupo, descripcion_grupo, " & _
                    "tipo,fecha_inicio,fecha_fin,turno,usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_grupo & "', '" & _
                    descripcion_grupo & "', '" & _
                    tipo & "', '" & _
                    objUtil.FormatFecha(fecha_inicio) & "', '" & _
                    objUtil.FormatFecha(fecha_fin) & "', " & _
                    turno & ", '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util
            If codigo_grupo <> "" Then
                Dim sql = "UPDATE NM_GrupoOperario " & _
                    "SET descripcion_grupo = '" & descripcion_grupo & "', " & _
                    "tipo = '" & tipo & "', " & _
                    "fecha_inicio = '" & objUtil.FormatFecha(fecha_inicio) & "', " & _
                    "fecha_fin = '" & objUtil.FormatFecha(fecha_fin) & "', " & _
                    "turno = " & turno & "," & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_grupo = '" & codigo_grupo & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Function Exist(ByVal codigoGrupo As String) As Boolean
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable
            sql = "SELECT * from NM_GrupoOperario where codigo_grupo = '" & codigoGrupo & "'"
            objDT = bd.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Public Sub Seek(ByVal codigoGrupo As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * from NM_GrupoOperario where codigo_grupo = '" & codigoGrupo & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_grupo = objDR("codigo_grupo")
                descripcion_grupo = objDR("descripcion_grupo")
                tipo = objDR("tipo")
            Next

            'Dim programaD As New NM_ProgramaD()
            'Detalle = programaD.List(codigoPrograma)
        End Sub

        Function List()
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_GrupoOperario"
            Return bd.Query(sql)
        End Function

        Function List(ByVal fTipo As Integer)
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_GrupoOperario where tipo =" & fTipo
            Return bd.Query(sql)
        End Function

        Function List(ByVal pTipo As String, ByVal pTurno As Int16)
            Dim objConn As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_GrupoOperario " & _
            "where tipo ='" & pTipo & "' and turno = " & pTurno
            Return objConn.Query(sql)
        End Function

        Function ListByTipo(ByVal pTipo As String)
            Dim objConn As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_GrupoOperario " & _
            "where tipo ='" & pTipo & "' "
            Return objConn.Query(sql)
        End Function

        Public Function list(ByVal pCodGrupo As String)
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_Operario where codigo_grupo='" & pCodGrupo & "'"
            ' Throw New Exception(sql)
            Return bd.Query(sql)
        End Function

    End Class

End Namespace
