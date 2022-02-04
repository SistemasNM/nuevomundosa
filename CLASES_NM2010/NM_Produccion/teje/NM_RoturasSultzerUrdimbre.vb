Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
    Public Class NM_RoturasSultzerUrdimbre
        Public CodigoPieza As String
        Public CodigoDefecto As String
        Public Del As Double
        Public Med As Double
        Public Det As Double
        Public Ori As Double
        Public Usuario As String

        Public Function Add() As Boolean
            Try
                If Not CodigoPieza = "" And Not CodigoDefecto = "" Then
                    Dim objConn As New NM_Consulta
                    Dim sql = "INSERT INTO NM_RoturasSultzerUrdimbre " & _
                        "(codigo_pieza, codigo_defecto_sulzer, " & _
                        "del, med, det, ori, " & _
                        "usuario_creacion, fecha_creacion) " & _
                        "VALUES ('" & _
                        CodigoPieza & "', '" & CodigoDefecto & "', " & _
                        Del & "," & Med & "," & Det & "," & Ori & ",'" & _
                        Usuario & "', GetDate())"
                    Return objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function List(ByVal pCodigoPieza As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim sql = "SELECT RSU.*, DS.descripcion_defecto_sulzer " & _
            " FROM NM_RoturasSultzerUrdimbre RSU, NM_DefectosSultzer DS " & _
            " WHERE RSU.codigo_defecto_sulzer = DS.codigo_defecto_sulzer " & _
            " and RSU.codigo_pieza = '" & pCodigoPieza & "'"
            Return objConn.Query(sql)
        End Function

        Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If CodigoPieza <> "" AndAlso CodigoDefecto <> "" Then
                    Dim sql = "UPDATE NM_RoturasSultzerUrdimbre " & _
                        "SET " & _
                        "del = '" & Del & "', " & _
                        "med = '" & Med & "', " & _
                        "det = '" & Det & "', " & _
                        "ori = '" & Ori & "', " & _
                        "usuario_modificacion = '" & Usuario & "', " & _
                        "fecha_modificacion = GetDate() " & _
                        "WHERE codigo_pieza = '" & CodigoPieza & "' " & _
                        "AND codigo_defecto_sulzer = '" & CodigoDefecto & "'"
                    Return objConn.Execute(sql)
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
                    Dim sql = "DELETE FROM NM_RoturasSultzerUrdimbre " & _
                        "WHERE codigo_pieza = '" & pCodigoPieza & "' " & _
                        "AND codigo_defecto_sulzer = '" & pCodigoDefecto & "'"
                    objConn.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function Exist(ByVal pCodigoPieza As String, ByVal pCodigoDefecto As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim dtUrdimbre As New DataTable
            Try
                If pCodigoPieza <> "" AndAlso pCodigoDefecto <> "" Then
                    Dim sql = "Select * FROM NM_RoturasSultzerUrdimbre " & _
                        "WHERE codigo_pieza = '" & pCodigoPieza & "' " & _
                        "AND codigo_defecto_sulzer = '" & pCodigoDefecto & "'"
                    dtUrdimbre = objConn.Query(sql)
                    Return (dtUrdimbre.Rows.Count > 0)
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

End Namespace