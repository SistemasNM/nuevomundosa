Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia


  Public Class NM_Linea

    Public Usuario As String
    Public codigo_linea As String
        Public descripcion_linea As String

        Private _objConnexion As AccesoDatosSQLServer


    Function Add() As Boolean
      Dim bd As New NM_Consulta(4)

      If codigo_linea <> "" Then
        Dim sql = "INSERT INTO NM_Linea " & _
            "(codigo_linea, descripcion_linea, " & _
            "usuario_creacion, fecha_creacion) " & _
            "VALUES ('" & _
            codigo_linea & "', '" & _
            descripcion_linea & "', '" & _
            Usuario & "'," & _
            "GetDate())"
        Return bd.Execute(sql)
      Else
        Throw New Exception("No se puede insertar porque el código es incorrecto.")
      End If
    End Function

    Function Update() As Boolean
      Dim bd As New NM_Consulta(4)

      If codigo_linea <> "" Then
        Dim sql = "UPDATE NM_Linea " & _
            "SET descripcion_linea = '" & descripcion_linea & "', " & _
            "WHERE codigo_linea = '" & codigo_linea & "' "
        Return bd.Execute(sql)
      Else
        Throw New Exception("No se puede actualizar porque el código es inválido.")
      End If
    End Function

    Public Sub Seek(ByVal codigoLinea As String)
      Dim bd As New NM_Consulta(4)
      Dim sql As String
      Dim objDT As New DataTable
      Dim objDR As DataRow
      sql = "SELECT * from NM_Linea where codigo_linea = '" & codigoLinea & "'"
      objDT = bd.Query(sql)

      For Each objDR In objDT.Rows
        codigo_linea = objDR("codigo_linea")
        descripcion_linea = objDR("descripcion_linea")
      Next

    End Sub

    Function List()
      Dim bd As New NM_Consulta(4)

      Dim sql = "SELECT * FROM NM_Linea "
      Return bd.Query(sql)
    End Function


    Public Function ListMezcla() As DataTable
      Dim bd As New NM_Consulta(4)
      Dim sql = "execute usp_ListarMezcla"
      Return bd.EjecutarConsulta(sql)
    End Function

        Public Function List_V2() As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

                Return _objConnexion.ObtenerDataTable("USP_OBTENER_LINEA_MAQUINA")
            Catch ex As Exception
                Throw ex
            Finally
                _objConnexion = Nothing
            End Try
        End Function

  End Class

End Namespace