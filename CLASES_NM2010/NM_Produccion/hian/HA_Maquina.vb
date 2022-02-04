Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia

    Public Class HA_Maquina

        Public Usuario As String
        Public codigo_maquina As String
        Public Nombre As String
        Public tipo_maquina As String

        Sub New()
            codigo_maquina = ""
            Nombre = ""
            tipo_maquina = ""
        End Sub

        Sub New(ByVal codigoMaquina As String)
            Seek(codigoMaquina)
        End Sub

        Function Add() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_maquina <> "" Then
                Dim sql = "INSERT INTO HA_Maquina " & _
                    "(codigo_maquina, descripcion_maquina, tipo_maquina, usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_maquina & "', '" & _
                    Nombre & "', '" & _
                    Usuario & ", GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update() As Boolean
            Dim bd As New NM_Consulta(4)

            If codigo_maquina <> "" Then
                Dim sql = "UPDATE HA_Maquina " & _
                    "SET descripcion_maquina = '" & Nombre & "', " & _
                    "tipo_maquina = '" & tipo_maquina & "', " & _
                     "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_maquina = '" & codigo_maquina & " "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Public Sub Seek(ByVal codigoMaquina As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * from HA_Maquina where codigo_maquina = '" & codigoMaquina & "'"
            objDT = bd.Query(sql)
            For Each objDR In objDT.Rows
                If Not IsDBNull(objDR("codigo_maquina")) Then codigo_maquina = objDR("codigo_maquina")
                If Not IsDBNull(objDR("descripcion_maquina")) Then Nombre = objDR("descripcion_maquina")
                If Not IsDBNull(objDR("tipo_maquina")) Then tipo_maquina = objDR("tipo_maquina")
            Next
        End Sub

        Function Exist(ByVal codigoMaquina As String) As Boolean
            Dim objGen As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "Select * from HA_Maquina where codigo_maquina = '" & codigoMaquina & "'"
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function Delete(ByVal sCodigoMaquina As String) As Boolean
            Dim objGen As New NM_Consulta(4)
            Dim sql As String
            Try
                sql = "Delete from HA_Maquina where codigo_maquina = '" & sCodigoMaquina & "'"
                Return objGen.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM HA_Maquina"
            Return bd.Query(sql)
        End Function

        Function List(ByVal TipoMaquina As String) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM HA_Maquina where tipo_maquina = '" & TipoMaquina & "'"
            Return bd.Query(sql)
        End Function

    End Class
End Namespace
