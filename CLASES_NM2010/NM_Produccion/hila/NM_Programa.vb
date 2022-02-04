Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_Programa

        Public Usuario As String
        Public codigo_programa As String
        Public fecha_inicio As Date
        Public fecha_fin As Date
        Public Detalle As DataTable
        Private objUtil As New NM_General.Util

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_programa <> "" Then
                Dim sql = "INSERT INTO NM_Programa " & _
                    "(codigo_programa, fecha_inicio, " & _
                    "fecha_fin, usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_programa & "', '" & _
                    fecha_inicio & "', '" & _
                    fecha_fin & "', '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_programa <> "" Then
                Dim sql = "UPDATE NM_Programa " & _
                    "SET fecha_inicio = " & fecha_inicio & ", " & _
                    "fecha_fin = " & fecha_fin & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_programa = '" & codigo_programa & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoPrograma As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_Programa where codigo_programa = '" & codigoPrograma & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_programa = objDR("codigo_programa")
                fecha_inicio = objDR("fecha_inicio")
                fecha_fin = objDR("fecha_fin")
            Next

            Dim programaD As New NM_ProgramaD()
            Detalle = programaD.List(codigoPrograma)
        End Sub

        Function Exist(ByVal codigoPrograma As String) As Boolean
            Dim objGen As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "Select * from NM_Programa where codigo_programa = '" & codigoPrograma & "'"
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function ObtenerCodigo() As String
            Dim bd As New NM_Consulta(4)
            Dim prefijo As String = Date.Today.Year

            Dim sql = "Select MAX(Right(codigo_programa, 6)) " & _
                "FROM NM_Programa " & _
                "WHERE LEFT(codigo_programa,4) = " & prefijo
            Dim dt As DataTable = bd.Query(sql)

            If dt.Rows.Count > 0 And Not IsDBNull(dt.Rows(0).Item(0)) Then
                Dim correlativo As Integer = dt.Rows(0).Item(0) + 1

                ' completar ceros
                Return prefijo & Format(correlativo, "000000")
            Else
                Return prefijo & "000001"
            End If
        End Function

    End Class

End Namespace