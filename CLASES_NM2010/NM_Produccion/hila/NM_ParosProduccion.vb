Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_ParosProduccion

        Public Usuario As String
        Public codigo_linea As String
        Public codigo_tipo_maquina As String
        Public revision As Integer
        Public Detalle As DataTable

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_linea <> "" And codigo_tipo_maquina <> "" Then
                Dim sql = "INSERT INTO NM_ParosProduccion " & _
                    "(codigo_linea, " & _
                    "codigo_tipo_maquina, revision, usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_linea & "', '" & _
                    codigo_tipo_maquina & "', " & _
                    revision & ", '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_linea <> "" And codigo_tipo_maquina <> "" Then
                Dim sql = "UPDATE NM_ParosProduccion " & _
                    "SET " & _
                    "revision = " & revision & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_linea = '" & codigo_linea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigo_tipo_maquina & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from NM_ParosProduccion where codigo_linea = '" & codigoLinea & "' " & _
            "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "'"
            objDT = bd.Query(sql)

            For Each objDR In objDT.Rows
                codigo_linea = objDR("codigo_linea")
                codigo_tipo_maquina = objDR("codigo_tipo_maquina")
                revision = objDR("revision")
            Next

            Dim paroProduccionD As New NM_ParosProduccionD()
            Detalle = paroProduccionD.List(codigoLinea, codigoTipoMaquina)
        End Sub

        Function Exist(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String)
            Dim objGen As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "SELECT * from NM_ParosProduccion where codigo_linea = '" & codigoLinea & "' " & _
            "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "'"

            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


    End Class


End Namespace
