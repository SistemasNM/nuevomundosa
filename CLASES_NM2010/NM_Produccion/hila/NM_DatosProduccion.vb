Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia

    Public Class NM_DatosProduccion

        Public codigo_produccion As String
        Public fecha As Date
        Public turno As Integer
        Public usuario As String
        Public DT As New DataTable() 'Tabla que almacena el detalle de produccion
        Private DB As NM_Consulta
        Private objUtil As New NM_General.Util

        Public Function Add() As Boolean
            If codigo_produccion <> "" Then
                DB = New NM_Consulta(4)
                Dim strsql As String
                strsql = "INSERT INTO NM_DatosProduccion " & _
                "(codigo_produccion, fecha, turno, usuario_creacion, " & _
                "fecha_creacion) values(" & _
                "'" & codigo_produccion & "'," & _
                "'" & objUtil.FormatFechaHora(fecha) & "'," & turno & "," & _
                "'" & usuario & "',GetDate())"
                Return DB.Execute(strsql)
            Else
                Return False
            End If
        End Function

        Public Function Update() As Boolean
            If codigo_produccion <> "" Then
                DB = New NM_Consulta(4)
                Dim strsql As String
                strsql = "UPDATE NM_DatosProduccion SET " & _
                "codigo_produccion = '" & codigo_produccion & "'," & _
                "fecha = '" & objUtil.FormatFechaHora(fecha) & "'," & _
                "turno = " & turno & "," & _
                "usuario_modificacion = '" & usuario & "'," & _
                "fecha_modificacion = getdate()" & _
                " where codigo_produccion = '" & codigo_produccion & "'"
                Return DB.Execute(strsql)
                DB = Nothing
            Else
                Return False
            End If
        End Function

        Public Function Delete(ByVal pcodigo_produccion As String) As Boolean
            If codigo_produccion <> "" Then
                Dim strsql As String
                DB = New NM_Consulta(4)
                strsql = "DELETE FROM NM_DatosProduccion where codigo_produccion = '" & pcodigo_produccion & "'"
                Return DB.Execute(strsql)
            Else
                Return False
            End If
        End Function

        Public Function list() As DataTable
            DB = New NM_Consulta(4)
            Return DB.getData("NM_DatosProduccion")
        End Function

        Public Sub Seek(ByVal pcodigo_produccion As String)
            Dim strsql As String
            Dim Tabla As New DataTable()
            Dim fila As DataRow
            strsql = "SELECT * FROM NM_DatosProduccion WHERE codigo_produccion = '" & pcodigo_produccion & "'"
            DB = New NM_Consulta(4)
            Tabla = DB.Query(strsql)
            For Each fila In Tabla.Rows
                If Not IsDBNull(fila("codigo_produccion")) Then codigo_produccion = fila("codigo_produccion")
                If Not IsDBNull(fila("fecha")) Then fecha = fila("fecha")
                If Not IsDBNull(fila("turno")) Then turno = fila("turno")
            Next
            Dim datosProduccionD As New NM_DatosProduccionD()
            DT = datosProduccionD.list(pcodigo_produccion)
        End Sub

        Public Function exist(ByVal pcodigo_produccion As String) As Boolean
            DB = New NM_Consulta(4)
            Dim tabla As New DataTable()
            Dim strsql As String = "SELECT * FROM NM_DatosProduccion Where codigo_produccion = '" & pcodigo_produccion & "'"
            tabla = DB.Query(strsql)
            return (tabla.Rows.Count > 0)
        End Function

        Function ObtenerCodigo() As String
            Dim bd As New NM_Consulta(4)
            Dim prefijo As String = Date.Today.Year

            Dim sql = "Select MAX(Right(codigo_produccion, 6)) " & _
                "FROM NM_DatosProduccion " & _
                "WHERE LEFT(codigo_produccion,4) = " & prefijo
            Dim dt As DataTable = bd.Query(sql)

            If dt.Rows.Count > 0 And Not IsDBNull(dt.Rows(0).Item(0)) Then
                Dim correlativo As Integer = dt.Rows(0).Item(0) + 1

                ' completar ceros
                Return prefijo & Format(correlativo, "000000")
            Else
                Return prefijo & "000001"
            End If
        End Function

  

    End Class

End Namespace