Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_Trama
        Public usuario As String
        Dim BD As New NM_Consulta()

        Public codigo_tela As String
        Public revision_tela As Integer
        Public codigo_hilo As String
        Public numero_hilos As Integer
        Public tipo As String
        Public peso_metro_cuadrado As Double
        Public peso_metro_lineal As Double

        Sub New()
            codigo_tela = ""
            revision_tela = 0
            codigo_hilo = ""
            numero_hilos = 0
            tipo = ""
            peso_metro_cuadrado = 0
            peso_metro_lineal = 0
        End Sub

        Public Sub Insert()
            If Not codigo_tela = "" And Not codigo_hilo = "" Then
                Dim strSQL = "INSERT INTO NM_Trama " & _
                    "(codigo_tela, revision_tela, codigo_hilo, " & _
                    "numero_hilos, tipo, peso_metro_cuadrado, " & _
                    "peso_metro_lineal, usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_tela & "', " & _
                    revision_tela & ", '" & _
                    codigo_hilo & "', " & _
                    numero_hilos & ",'" & _
                    tipo & "'," & _
                    peso_metro_cuadrado & "," & _
                    peso_metro_lineal & ",'" & _
                    usuario & "'," & _
                    "GetDate())"
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Sub

        Public Sub Update()
            If Not codigo_tela = "" And Not codigo_hilo = "" Then
                Dim strSQL = "UPDATE NM_Trama " & _
                    "SET numero_hilos = " & numero_hilos & ", " & _
                    "tipo = '" & tipo & "', " & _
                    "peso_metro_cuadrado = " & peso_metro_cuadrado & ", " & _
                    "peso_metro_lineal = " & peso_metro_lineal & ", " & _
                    "usuario_modificacion = '" & usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_tela = '" & codigo_tela & "' " & _
                    "AND revision_tela = " & revision_tela & " " & _
                    "AND codigo_hilo = '" & codigo_hilo & "'"

                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Sub

        Public Sub Delete()
            If Not codigo_tela = "" And Not codigo_hilo = "" Then
                Dim strSQL = "DELETE FROM NM_Trama " & _
                    "WHERE codigo_tela = '" & codigo_tela & "' " & _
                    "AND revision_tela = " & revision_tela & " " & _
                    "AND codigo_hilo = '" & codigo_hilo & "'"
                BD.Execute(strSQL)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Sub

        Function LoadDT(ByVal codigoTela As String, ByVal revision As Integer) As DataTable
            Dim strSQL = "SELECT * FROM NM_Trama WHERE codigo_tela = '" & codigoTela & "' AND revision_tela = " & revision
            Return BD.Query(strSQL)
        End Function

        Function LoadDT(ByVal codigoTela As String) As DataTable
            Dim strSQL = "SELECT * FROM NM_Tela t " & _
                "JOIN NM_Trama tr ON t.codigo_tela = tr.codigo_tela " & _
                "AND t.revision_tela = tr.revision_tela " & _
                "WHERE t.codigo_tela = '" & codigoTela & "' AND flagestado = 1"
            Return BD.Query(strSQL)
        End Function

        Function List(ByVal pCodigoPieza As String) As DataTable
            Dim sql As String
            sql = "select T.codigo_hilo, T.numero_hilos, T.peso_metro_lineal " & _
            " from NM_TRAMA T, NM_PIEZA P " & _
            " where T.codigo_articulo = P.codigo_articulo " & _
            " and T.revision_articulo = P.revision_articulo " & _
            " and P.codigo_pieza like '" & pCodigoPieza & "' "
        End Function

        Public Sub Seek(ByVal codigoTela As String, ByVal revisionTela As Integer, ByVal codigoHilo As String)
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_Trama " & _
                    "WHERE codigo_tela = '" & codigoTela & "' " & _
                    "AND revision_tela = " & revisionTela & " " & _
                    "AND codigo_hilo = '" & codigoHilo & "'"
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                codigo_tela = objDR("codigo_tela")
                revision_tela = objDR("revision_tela")
                codigo_hilo = objDR("codigo_hilo")
                If IsDBNull(objDR("tipo")) = False Then tipo = objDR("tipo")
                numero_hilos = objDR("numero_hilos")
                peso_metro_cuadrado = objDR("peso_metro_cuadrado")
                peso_metro_lineal = objDR("peso_metro_lineal")


            Next
        End Sub

    End Class


End Namespace