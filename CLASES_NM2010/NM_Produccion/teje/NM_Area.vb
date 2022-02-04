Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria

    Public Class NM_Area
        Public codigo_area = ""
        Public nombre_area = ""
        Public usuario_creacion = ""
        Public fecha_creacion
        Public usuario_modificacion = ""
        Public fecha_modificacion
        Private DB As NM_Consulta
        Public Function insert(ByVal pcodigo_area As String, ByVal pnombre_area As String, ByVal pusuario_creacion As String) As Integer
            DB = New NM_Consulta()
            If pcodigo_area <> "" Then
                Dim strsql As String
                strsql = "INSERT INTO NM_Area values("
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("'" & pcodigo_area & "',")
                commandString.Append("'" & pnombre_area & "',")
                commandString.Append("'" & pusuario_creacion & "',")
                commandString.Append("GetDate(),")
                commandString.Append("'" & pusuario_creacion & "',")
                commandString.Append("GetDate())")
                Try
                    DB.Execute(commandString.ToString)
                    Return 1
                Catch
                    Return 0
                End Try
            End If
            DB = Nothing
        End Function

        Public Function update(ByVal pcodigo_area As String, ByVal pnombre_area As String, ByVal pusuario_modificacion As String) As Integer
            DB = New NM_Consulta()
            If pcodigo_area <> "" Then
                Dim strsql As String
                strsql = "UPDATE NM_Area SET "
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("nombre_area = '" & pnombre_area & "',")
                commandString.Append("usuario_modificacion = '" & pusuario_modificacion & "',")
                commandString.Append("fecha_modificacion = getdate()")
                commandString.Append(" where codigo_area = '" & pcodigo_area & "'")
                Try
                    DB.Execute(commandString.ToString)
                    Return 1
                Catch
                    Return 0
                End Try
                DB = Nothing
            End If
        End Function

        Public Function delete(ByVal pcodigo_area As String) As Integer
            Dim strsql As String
            DB = New NM_Consulta()
            strsql = "DELETE FROM NM_Area where codigo_area = '" & pcodigo_area & "'"
            Try
                DB.Execute(strsql)
                Return 1
            Catch
                Return 0
            End Try
            DB = Nothing
        End Function

        Public Function list() As DataTable
            DB = New NM_Consulta()
            Return DB.Query("Select * from NM_Area order by fecha_creacion")
            DB = Nothing
        End Function

        Public Sub seek(ByVal pcodigo_area As String)
            DB = New NM_Consulta()
            Dim strsql As String
            Dim tabla As New DataTable()
            strsql = "SELECT * FROM NM_AREA Where codigo_area = '" & pcodigo_area & "'"
            tabla = DB.Query(strsql)
            Dim fila As DataRow
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_area")) Then codigo_area = fila("codigo_area")
                If Not IsDBNull(fila("nombre_area")) Then nombre_area = fila("nombre_area")
                If Not IsDBNull(fila("usuario_creacion")) Then usuario_creacion = fila("usuario_creacion")
                If Not IsDBNull(fila("fecha_creacion")) Then fecha_creacion = fila("fecha_creacion")
                If Not IsDBNull(fila("usuario_modificacion")) Then usuario_modificacion = fila("usuario_modificacion")
                If Not IsDBNull(fila("fecha_modificacion")) Then fecha_modificacion = fila("fecha_modificacion")
            Next
            DB = Nothing
        End Sub

        Public Function getUsuarios(ByVal pcodigo_area As String) As DataTable
            DB = New NM_Consulta()
            Dim strsql As String = "SELECT * FROM NM_Usuario WHERE codigo_area ='" & pcodigo_area & "'"
            Return DB.Query(strsql)
            DB = Nothing
        End Function

    End Class
End Namespace

