Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_TestHilo
        Public Usuario As String
        Public codigo_hilo As String
        Public fecha As Date
        Public codigo_maquina_uster As String
        Public codigo_maquina As String
        Public codigo_testdato As String
        Public codigo_linea As String
        Public estandar As Double
        Public limite_control1 As Double
        Public limite_control2 As Double
        Public limite_aviso1 As Double
        Public limite_aviso2 As Double
        'Private objUtil As New NM_General.Util
        Private objUtil As New NM_General.Util

        Function Add()
            Dim bd As New NM_Consulta(4)

            If codigo_hilo <> "" Then
                Dim sql = "INSERT INTO NM_TestHilo " & _
                    "(codigo_hilo, fecha, codigo_maquina_uster, " & _
                    "codigo_maquina, codigo_testdato, codigo_linea, " & _
                    "estandar, limite_control1, limite_control2, " & _
                    "limite_aviso1, limite_aviso2, " & _
                    "usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_hilo & "', '" & _
                    objUtil.FormatFechaHora(fecha) & "', '" & _
                    codigo_maquina_uster & "', '" & _
                    codigo_maquina & "', '" & _
                    codigo_testdato & "', '" & _
                    codigo_linea & "', " & _
                    estandar & ", " & _
                    limite_control1 & ", " & _
                    limite_control2 & ", " & _
                    limite_aviso1 & ", " & _
                    limite_aviso2 & ", '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update()
            Dim bd As New NM_Consulta(4)

            If codigo_hilo <> "" And codigo_testdato <> "" Then
                Dim sql = "UPDATE NM_TestHilo " & _
                    "SET estandar = " & estandar & ", " & _
                    "limite_control1 = " & limite_control1 & ", " & _
                    "limite_control2 = " & limite_control2 & ", " & _
                    "limite_aviso1 = " & limite_aviso1 & ", " & _
                    "limite_aviso2 = " & limite_aviso2 & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_testdato = '" & codigo_testdato & "' " & _
                    "AND codigo_hilo = '" & codigo_hilo & "' "

                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Function Delete(ByVal codigo_hilo As String, ByVal codigo_testdato As String)
            Dim bd As New NM_Consulta(4)

            If codigo_hilo <> "" And codigo_testdato <> "" Then
                Dim sql = "DELETE FROM NM_TestHilo " & _
                    "WHERE codigo_testdato = '" & codigo_testdato & "' " & _
                    "AND codigo_hilo = '" & codigo_hilo & "' "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Function

        Function List() As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_TestHilo"
            Return bd.Query(sql)
        End Function

        Function List(ByVal codigo_hilo As String) As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT *,isnull(usuario_modificacion, '') as usuario_mod, convert(varchar(10), fecha_modificacion,103) as fecha_mod FROM NM_TestHilo ti " & _
                "JOIN NM_TestDato td ON ti.codigo_testdato = td.codigo_testdato " & _
                "WHERE ti.codigo_hilo = '" & codigo_hilo & "' "

            Return bd.Query(sql)
        End Function

    End Class

End Namespace