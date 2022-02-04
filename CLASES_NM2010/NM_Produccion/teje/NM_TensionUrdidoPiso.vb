Imports NM_General.NM_BaseDatos
Imports System.Math

Namespace NM_Tejeduria

    Public Class NM_TensionUrdidoPiso
        Private DB As NM_Consulta
        Private correlativo As Integer
        Private objUtil As New NM_General.Util

        Private Function getCorrelativo(ByVal fecha As Date, ByVal piso As String) As Integer
            DB = New NM_Consulta
            Dim strsql As String
            strsql = "SELECT max(correlativo) as correlativo FROM NM_TensionUrdidopiso " & _
            " WHERE DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(fecha) & "') = 0 and codigo_piso ='" & piso & "'"
            Dim tabla As DataTable
            Dim fila As DataRow
            tabla = DB.Query(strsql)
            For Each fila In tabla.Rows
                If IsDBNull(fila("correlativo")) Then Return 1
                Return CInt(fila("correlativo")) + 1
            Next
        End Function

        Public Function insert(ByVal pfecha_estudio As Date, ByVal pcodigo_piso As String, _
              ByVal hora As String, ByVal lado_a As Double, _
               ByVal lado_b As Double, Optional ByVal pusuario_creacion As String = "dbo") As Integer
            DB = New NM_Consulta
            Dim strsql As String
            strsql = "INSERT INTO NM_TensionUrdidopiso " & _
            " (fecha_estudio, codigo_piso, correlativo, hora, lado_a, lado_b, " & _
            " usuario_creacion, fecha_creacion) values(" & _
            "convert(datetime,'" & objUtil.FormatFecha(pfecha_estudio) & "')," & _
            "'" & pcodigo_piso & "'," & _
            getCorrelativo(pfecha_estudio, pcodigo_piso) & "," & _
            "'" & hora & "'," & lado_a & "," & _
            lado_b & ",'" & pusuario_creacion & "',GetDate())"
            Try
                Return DB.Execute(strsql)
            Catch
                Return False
            End Try
        End Function

        Public Function update(ByVal pfecha_estudio As Date, ByVal pcodigo_piso As String, _
        ByVal pcorrelativo As Integer, ByVal hora As String, ByVal plado_a As Double, _
               ByVal plado_b As Double, Optional ByVal pusuario_modificacion As String = "dbo") As Integer
            DB = New NM_Consulta
            Dim strsql As String
            strsql = "UPDATE NM_TensionUrdidoPiso SET " & _
            "lado_a = " & plado_a & "," & _
            "lado_b = " & plado_b & "," & _
            "usuario_modificacion = '" & pusuario_modificacion & "'," & _
            "fecha_modificacion = getdate()" & _
            " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha_estudio) & "') = 0 and " & _
            "codigo_piso = '" & pcodigo_piso & "' and " & _
            "correlativo = " & pcorrelativo
            Try
                Return DB.Execute(strsql)
            Catch
                Return False
            End Try
        End Function

        Public Function delete(ByVal pfecha As Date, Optional ByVal pcodigo_piso As String = "", Optional ByVal pCorrelativo As Integer = -1) As Integer
            Dim strsql As String
            DB = New NM_Consulta
            strsql = "DELETE FROM NM_TensionUrdidopiso " & _
            " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha) & "') = 0 "
            If pcodigo_piso <> "" Then
                strsql = strsql & " and codigo_piso = '" & pcodigo_piso & "'"
            End If
            If pCorrelativo <> -1 Then
                strsql = strsql & " and correlativo = " & pCorrelativo
            End If
            Try
                DB.Execute(strsql)
                Return 1
            Catch
                Return 0
            End Try
            DB = Nothing
        End Function

        Public Function getTensiones(ByVal pfecha As Date, ByVal piso As String) As DataTable
            Dim strsql As String
            Dim DB As New NM_Consulta
            Dim tabla As New DataTable
            strsql = "SELECT * FROM NM_TensionUrdidopiso " & _
            " WHERE DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha) & "') = 0 and codigo_piso ='" & piso & "' order by 1,2,3"
            Try
                tabla = DB.Query(strsql)
            Catch
                Throw
            End Try
            Return tabla
            ' DB = Nothing
        End Function

        Public Function getPromedio(ByVal pfecha As Date, ByVal piso As String) As Double
            DB = New NM_Consulta
            Dim tabla As New DataTable
            Dim fila As DataRow
            Dim strsql As String
            strsql = "SELECT ((sum(lado_a)+ sum(lado_b))/ (count(*)*2)) promedio FROM NM_TensionUrdidoPiso "
            strsql = strsql & " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha) & "') = 0 "
            strsql = strsql & " and codigo_piso = '" & piso & "'"
            tabla = DB.Query(strsql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("promedio")) Then
                    Return fila("promedio")
                End If
                Exit For
            Next
            Return 0
        End Function

        Public Function getDesviacion(ByVal pfecha As Date, ByVal piso As String) As Double
            DB = New NM_Consulta
            Dim tabla As New DataTable
            Dim fila As DataRow
            Dim suma As Double = 0
            Dim strsql As String = "exec SP_NM_GetDesviacion '" & _
            objUtil.FormatFecha(pfecha) & "'," & piso & " "
            tabla = DB.Query(strsql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("desv")) Then
                    suma = suma + fila("desv")
                End If
            Next
            suma = Sqrt(suma)
            Return suma
        End Function

        Public Function getMinMAx(ByVal pfecha As Date, ByVal piso As String, ByVal hallaElMin As Boolean) As Double
            DB = New NM_Consulta
            Dim tabla As New DataTable
            Dim fila As DataRow
            Dim func As String
            If hallaElMin Then
                func = "min"
            Else
                func = "max"
            End If
            Dim strsql As String = "SELECT  " & func & "(lado_a) A, " & func & "(lado_b) B " & _
                                   "FROM NM_TensionUrdidoPiso "
            strsql += " where DATEDIFF(DD, fecha_estudio, '" & objUtil.FormatFecha(pfecha) & "') = 0 "
            strsql += " and codigo_piso = '" & piso & "'"
            tabla = DB.Query(strsql)
            If hallaElMin Then
                For Each fila In tabla.Rows
                    If Not IsDBNull(fila("A")) And Not IsDBNull(fila("A")) Then
                        If (fila("A") <= fila("B")) Then
                            Return fila("A")
                        Else
                            Return fila("B")
                        End If
                    End If
                Next
            Else
                For Each fila In tabla.Rows
                    If Not IsDBNull(fila("A")) And Not IsDBNull(fila("A")) Then
                        If (fila("A") >= fila("B")) Then
                            Return fila("A")
                        Else
                            Return fila("B")
                        End If
                    End If
                Next
            End If
            Return 0
        End Function


    End Class
End Namespace
