Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NMM_Trama
        Public CodigoArticulo As String
        Public CodigoHilo As String
        Public NumeroHilos As Double
        Public Tipo As String
        Public PesoMetroCuadrado As Double
        Public PesoMetroLineal As Double
        Public Usuario As String

        Sub New()
            CodigoArticulo = ""
            CodigoHilo = ""
            NumeroHilos = 0
            tipo = ""
            PesoMetroCuadrado = 0
            PesoMetroLineal = 0
        End Sub

        Public Function Add() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If Not CodigoArticulo = "" And Not CodigoHilo = "" Then
                    Dim strSQL = "INSERT INTO NM_MA_Trama " & _
                        "(codigo_articulo, codigo_hilo, " & _
                        "numero_hilos, tipo, peso_metro_cuadrado, " & _
                        "peso_metro_lineal, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & CodigoArticulo & "', '" & _
                        CodigoHilo & "', " & NumeroHilos & ",'" & _
                        Tipo & "'," & PesoMetroCuadrado & "," & _
                        PesoMetroLineal & ",'" & Usuario & "'," & _
                        "GetDate())"
                    Return objConn.Execute(strSQL)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If Not CodigoArticulo = "" And Not CodigoHilo = "" Then
                    Dim strSQL = "UPDATE NM_MA_Trama " & _
                        "SET numero_hilos = " & NumeroHilos & ", " & _
                        "tipo = '" & Tipo & "', " & _
                        "peso_metro_cuadrado = " & PesoMetroCuadrado & ", " & _
                        "peso_metro_lineal = " & PesoMetroLineal & ", " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_articulo = '" & CodigoArticulo & "' " & _
                        "AND codigo_hilo = '" & CodigoHilo & "'"
                    Return objConn.Execute(strSQL)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If Not CodigoArticulo = "" And Not CodigoHilo = "" Then
                    Dim strSQL = "DELETE FROM NM_MA_Trama " & _
                        "WHERE codigo_articulo = '" & CodigoArticulo & "' " & _
                        "AND codigo_hilo = '" & CodigoHilo & "'"
                    Return objConn.Execute(strSQL)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function List(ByVal pCodigoArticulo As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_MA_Trama WHERE codigo_articulo = '" & pCodigoArticulo & "' "
            Dim dt As New DataTable
            dt = objConn.Query(strSQL)
        End Function

        Function LoadDT(ByVal pCodigoArticulo As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_MA_Trama " & _
                "WHERE codigo_articulo = '" & pCodigoArticulo & "' "
            Return objConn.Query(strSQL)
        End Function

        Public Sub Seek(ByVal pCodigoArticulo As String, ByVal pCodigoHilo As String)
            Dim sql As String
            Dim objDT As New DataTable, BD As New NM_Consulta
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_MA_Trama " & _
                    "WHERE codigo_articulo = '" & pCodigoArticulo & "' " & _
                    "AND codigo_hilo = '" & pCodigoHilo & "'"
            objDT = BD.Query(sql)

            For Each objDR In objDT.Rows
                CodigoArticulo = objDR("codigo_articulo")
                CodigoHilo = objDR("codigo_hilo")
                If IsDBNull(objDR("tipo")) = False Then Tipo = objDR("tipo")
                NumeroHilos = objDR("numero_hilos")
                PesoMetroCuadrado = objDR("peso_metro_cuadrado")
                PesoMetroLineal = objDR("peso_metro_lineal")
            Next
        End Sub

        Function CopyData(ByVal pCodigoArticulo As String, ByVal pUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_Trama (codigo_articulo, revision_articulo, " & _
                " codigo_hilo, tipo, numero_hilos, peso_metro_cuadrado, peso_metro_lineal, " & _
                " usuario_creacion, fecha_creacion) " & _
                " (select A.codigo_articulo, A.revision_articulo, codigo_hilo, tipo, " & _
                " numero_hilos, peso_metro_cuadrado, peso_metro_lineal, '" & pUsuario & _
                "', getdate() from NM_MA_Trama T, NM_MA_Articulo A " & _
                " where T.codigo_articulo = A.codigo_articulo " & _
                " and T.codigo_articulo = '" & pCodigoArticulo & "')"
                Return objConn.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class

End Namespace