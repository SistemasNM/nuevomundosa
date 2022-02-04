Imports NM_General.NM_BaseDatos
Public Class NM_NMTelaMecanico
    Private DB As NM_Consulta

    Public Function insert(ByVal pCodigo_Mecanico As String, ByVal pCodigo_Telar As String, ByVal escuadra_mecanico As String, ByVal pusuario_creacion As String) As Integer
        DB = New NM_Consulta()
        If pCodigo_Mecanico <> "" AndAlso pCodigo_Telar <> "" Then
            Dim strsql As String
            strsql = "INSERT INTO NM_TelarMecanico values("
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("'" & pCodigo_Mecanico & "',")
            commandString.Append("'" & pCodigo_Telar & "',")
            commandString.Append("'" & escuadra_mecanico & "',")
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

    Public Function delete(ByVal pCodigo_Mecanico As String, ByVal pcodigo_telar As String) As Integer
        Dim strsql As String
        DB = New NM_Consulta()
        strsql = "DELETE FROM NM_TelarMecanico where Codigo_Mecanico ='" & pCodigo_Mecanico & "' and '" & " codigo_telar = '" & pcodigo_telar & "'"
        Try
            DB.Execute(strsql)
            Return 1
        Catch
            Return 0
        End Try
        DB = Nothing
    End Function

    Public Function list(ByVal pcodigo_mecanico As String, ByVal pcodigo_telar As String) As DataTable
        DB = New NM_Consulta()
        Dim strsql As String = "Select * from NM_TelarMecanico where codigo_mecanico ='" & pcodigo_mecanico & "' and codigo_telar = '" & pcodigo_telar & "'"
        Return DB.Query(strsql)
        DB = Nothing
    End Function

    Public Function list() As DataTable
        DB = New NM_Consulta()
        Return DB.getData("NM_TelarMecanico")
        DB = Nothing
    End Function

    'Public Sub seek(ByVal pcodigo_area As String)
    '    DB = New NM_Consulta()
    '    Dim strsql As String
    '    Dim tabla As New DataTable()
    '    strsql = "SELECT * FROM NM_AREA Where codigo_area = '" & pcodigo_area & "'"
    '    tabla = DB.Query(strsql)
    '    Dim fila As DataRow
    '    For Each fila In tabla.Rows
    '        If Not IsDBNull(fila("codigo_area")) Then codigo_area = fila("codigo_area")
    '        If Not IsDBNull(fila("nombre_area")) Then nombre_area = fila("nombre_area")
    '        If Not IsDBNull(fila("usuario_creacion")) Then usuario_creacion = fila("usuario_creacion")
    '        If Not IsDBNull(fila("fecha_creacion")) Then fecha_creacion = fila("fecha_creacion")
    '        If Not IsDBNull(fila("usuario_modificacion")) Then usuario_modificacion = fila("usuario_modificacion")
    '        If Not IsDBNull(fila("fecha_modificacion")) Then fecha_modificacion = fila("fecha_modificacion")
    '    Next
    '    DB = Nothing
    'End Sub
End Class

