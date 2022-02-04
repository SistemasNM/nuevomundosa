Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_ProgramaD

        Public Usuario As String
        Public codigo_programa As String
        Public codigo_tipo_maquina As String
        Public Ne_real As Double
        Public kilos_producir As Double
        Public cantidad_maquinas As Integer

        Function Add()
            Dim bd As New NM_Consulta(4)

            If codigo_programa <> "" And codigo_tipo_maquina <> "" And Ne_real <> 0 Then
                Dim sql = "INSERT INTO NM_ProgramaD " & _
                    "(codigo_programa, codigo_tipo_maquina, " & _
                    "Ne_real, kilos_producir, cantidad_maquinas, usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_programa & "', '" & _
                    codigo_tipo_maquina & "', " & _
                    Ne_real & ", " & _
                    kilos_producir & ", " & _
                    cantidad_maquinas & ", '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update()
            Dim bd As New NM_Consulta(4)

            If codigo_programa <> "" And codigo_tipo_maquina <> "" And Ne_real <> 0 Then
                Dim sql = "UPDATE NM_ProgramaD " & _
                    "SET kilos_producir = " & kilos_producir & ", " & _
                    "cantidad_maquinas = " & cantidad_maquinas & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_programa = '" & codigo_programa & "' " & _
                    "AND codigo_tipo_maquina = '" & codigo_tipo_maquina & "' " & _
                    "AND Ne_real = " & Ne_real
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Function Delete(ByVal codigoPrograma As String, ByVal codigoTipoMaquina As String, ByVal NeReal As Double)
            Dim bd As New NM_Consulta(4)

            If codigoPrograma <> "" And codigoTipoMaquina <> "" And NeReal <> 0 Then
                Dim sql = "DELETE FROM NM_ProgramaD " & _
                    "WHERE codigo_programa = '" & codigoPrograma & "' " & _
                    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
                    "AND Ne_real = " & NeReal
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Function

        Function List(ByVal codigoPrograma As String) As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_ProgramaD WHERE codigo_programa = '" & codigoPrograma & "'"
            Return bd.Query(sql)
        End Function

    End Class

End Namespace