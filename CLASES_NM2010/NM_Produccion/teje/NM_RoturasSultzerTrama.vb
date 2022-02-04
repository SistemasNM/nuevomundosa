Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

Public Class NM_RoturasSultzerTrama

        Public CodigoPieza As String
        Public CodigoDefecto As String
        Public Roturas As Double
        Public Usuario As String

        Sub New()
            CodigoPieza = ""
            CodigoDefecto = ""
            Roturas = 0
        End Sub

        Sub New(ByVal pCodigoPieza As String)
            Seek(pCodigoPieza)
        End Sub

        Public Sub Seek(ByVal pCodigoPieza As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dtTrama As New DataTable
            Dim drTrama As DataRow
            sql = "SELECT * from NM_RoturasSultzerTrama where codigo_pieza ='" & pCodigoPieza & "'"
            dtTrama = objconn.Query(sql)
            For Each drTrama In dtTrama.Rows
                CodigoPieza = drTrama("codigo_pieza")
                CodigoDefecto = drTrama("codigo_defecto_sulzer")
                Roturas = drTrama("roturas")
            Next
        End Sub

        Function Listar() As DataTable
            Dim objConn As New NM_Consulta
            Dim strSQL = "SELECT * FROM NM_RoturasSultzerTrama "
            Return objConn.Query(strSQL)
        End Function

        Public Function Add() As Boolean
            Try
                Dim objConn As New NM_Consulta

                If CodigoPieza <> "" AndAlso CodigoDefecto <> "" Then
                    Dim sql = "INSERT INTO NM_RoturasSultzerTrama " & _
                        "(codigo_pieza, codigo_defecto_sulzer, roturas, usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & CodigoPieza & "', '" & CodigoDefecto & "', " & Roturas & ",'" & _
                        Usuario & "'," & "GetDate())"
                    Return objConn.Execute(sql)
                Else
                    Return False
            End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If CodigoPieza <> "" AndAlso CodigoDefecto <> "" Then
                    Dim sql = "UPDATE NM_RoturasSultzerTrama " & _
                        "SET " & _
                        "roturas = " & Roturas & ", " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_pieza = '" & CodigoPieza & "' " & _
                        "AND codigo_defecto_sulzer = '" & CodigoDefecto & "'"
                    Return (objConn.Execute(sql))
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Delete(ByVal pCodigoPieza As String, ByVal pCodigoDefecto As String) As Boolean
            Dim objConn As New NM_Consulta
            Try
                If pCodigoPieza <> "" AndAlso pCodigoDefecto <> "" Then
                    Dim sql = "DELETE FROM NM_RoturasSultzerTrama " & _
                        "WHERE codigo_pieza = '" & pCodigoPieza & "' " & _
                        "AND codigo_defecto_sulzer = '" & pCodigoDefecto & "'"
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Exist(ByVal pCodigoPieza As String, ByVal pCodigoDefecto As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim dtTrama As New DataTable
            Try
                If pCodigoPieza <> "" AndAlso pCodigoDefecto <> "" Then
                    Dim sql = "Select * FROM NM_RoturasSultzerTrama " & _
                        "WHERE codigo_pieza = '" & pCodigoPieza & "' " & _
                        "AND codigo_defecto_sulzer = '" & pCodigoDefecto & "'"
                    dtTrama = objConn.Query(sql)
                    Return (dtTrama.Rows.Count > 0)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal pCodigoPieza As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim sql = "SELECT RST.*, DS.descripcion_defecto_sulzer " & _
            " FROM NM_RoturasSultzerTrama RST, NM_DefectosSultzer DS " & _
            " WHERE RST.codigo_defecto_sulzer = DS.codigo_defecto_sulzer " & _
            " and RST.codigo_pieza = '" & pCodigoPieza & "'"
            Return objConn.Query(sql)
        End Function

End Class

End Namespace