Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria
    Public Class NM_Fase
        Public CodigoFase As String
        Public DescripcionFase As String
        Public Unidades As String
        Public Usuario As String
        Private _objConexion As AccesoDatosSQLServer

        Dim objGen As New NM_Consulta

        Sub New()
            CodigoFase = ""
            DescripcionFase = ""
            Unidades = ""
        End Sub

        Public Function Add() As Boolean
            Dim sql As String
            Try
                If CodigoFase <> "" AndAlso DescripcionFase <> "" Then
                    sql = "Insert into NM_Fase (" & _
                    "codigo_fase, descripcion_fase, usuario_creacion, fecha_creacion) " & _
                    " values('" & CodigoFase & "','" & DescripcionFase & "','" & _
                    Usuario & ",getdate())"
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Delete(ByVal Codigo As String) As Boolean
            Dim sql As String
            Try
                If Codigo <> "" Then
                    sql = "Delete from NM_Fase where codigo_fase = '" & Codigo & "' "
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Update() As Boolean
            Dim sql As String
            Try
                If CodigoFase <> "" AndAlso DescripcionFase <> "" Then
                    sql = "Update NM_fase descripcion_fase = '" & DescripcionFase & "', " & _
                    "usuario_modificacion = '" & Usuario & "', fecha_modificacion=getdate() " & _
                    "where codigo_fase = '" & CodigoFase & "'"
                    Return objGen.Execute(sql)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function List() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_PTJ_FasePretejido_Listar")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Seek(ByVal Codigo As String)
            Dim sql As String, objDT As New DataTable, fila As DataRow
            sql = "Select * " & _
            " from NM_Fase where codigo_fase ='" & Codigo & "' "
            objDT = objGen.Query(sql)
            For Each fila In objDT.Rows
                Me.CodigoFase = fila("codigo_fase")
                Me.DescripcionFase = fila("descripcion_fase")
                Me.Unidades = fila("unidades")
            Next
        End Sub

        Public Function Exist(ByVal Codigo As String) As Boolean
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Fase where codigo_fase ='" & Codigo & "' "
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

    End Class

End Namespace