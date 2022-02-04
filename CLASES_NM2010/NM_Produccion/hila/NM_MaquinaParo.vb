Imports NM_General.NM_BaseDatos
Namespace NM_Hilanderia
    Public Class NM_MaquinaParo
        Public codigo_maquina_paro As String
        Public codigo_paro_produccionD As String
        Public codigo_linea As String
        Public codigo_maquina As String
        Public revision_maquina As Integer
        Public codigo_tipo_maquina As String
        Public fecha_inicio As Date
        Public fecha_fin As Date
        Public hora_inicio As String
        Public hora_fin As String
        Public usuario As String
        Public descripcion_paro_produccion As String
        Public DB As NM_Consulta
        'Private objUtil As New NM_General.Util
        Private objUtil As New NM_General.Util

        Public Function add() As Boolean
            'If codigo_maquina_paro <> "" Then
            DB = New NM_Consulta(4)
            Dim strsql As String
            strsql = "INSERT INTO NM_MaquinaParo values("
            Dim commandString As New System.Text.StringBuilder
            commandString.Append(strsql)
            Try
                'commandString.Append("'" & codigo_maquina_paro & "',")
                commandString.Append("'" & codigo_linea & "',")
                commandString.Append("'" & codigo_tipo_maquina & "',")
                commandString.Append("'" & codigo_paro_produccionD & "',")
                commandString.Append("'" & codigo_maquina & "',")
                commandString.Append(revision_maquina & ",")
                commandString.Append("'" & objUtil.FormatFecha(fecha_inicio) & "',")
                commandString.Append("'" & objUtil.FormatFecha(fecha_fin) & "',")
                commandString.Append("'" & hora_inicio & "',")
                commandString.Append("'" & hora_fin & "',")
                commandString.Append("'" & usuario & "',")
                commandString.Append("GetDate(),")
                commandString.Append("'" & usuario & "',")
                commandString.Append("GetDate())")
            Catch
                Throw
            End Try
            Return CBool(DB.Execute(commandString.ToString))

            '   Else
            '       Return False
            '  End If
        End Function

        Public Function update() As Boolean

            DB = New NM_Consulta(4)
            Dim strsql As String
            strsql = "UPDATE NM_MaquinaParo SET "
            Dim commandString As New System.Text.StringBuilder
            commandString.Append(strsql)
            commandString.Append("codigo_linea = '" & codigo_linea & "',")
            commandString.Append("codigo_tipo_maquina = '" & codigo_tipo_maquina & "',")
            commandString.Append("codigo_paro_produccionD = '" & codigo_paro_produccionD & "',")
            commandString.Append("codigo_maquina = '" & codigo_maquina & "',")
            'commandString.Append("fecha_inicio = '" & objUtil.FormatFecha(fecha_inicio) & "',")
            commandString.Append("fecha_fin = '" & objUtil.FormatFecha(fecha_fin) & "',")
            commandString.Append("hora_inicio = '" & hora_inicio & "',")
            commandString.Append("hora_fin = '" & hora_fin & "',")
            commandString.Append("usuario_modificacion = '" & usuario & "',")
            commandString.Append("fecha_modificacion = getdate()")
            commandString.Append(" where codigo_maquina_paro = " & codigo_maquina_paro)

            Return DB.Execute(commandString.ToString)
            DB = Nothing

        End Function

        Public Function delete(ByVal pcodigo_maquina_paro As Integer) As Boolean
            If codigo_maquina_paro >= 0 Then
                Dim strsql As String
                DB = New NM_Consulta(4)
                strsql = "DELETE FROM NM_MaquinaParo where codigo_maquina_paro = " & pcodigo_maquina_paro
                Return CBool(DB.Execute(strsql))
            Else
                Return False
            End If
        End Function

        'Public Function list() As DataTable
        '    DB = New NM_Consulta(4)
        '    Return DB.Query("SELECT * FROM NM_MaquinaParo Order by fecha_modificacion")
        'End Function

        Public Sub seek(ByVal pcodigo_maquina_paro As String)
            Dim strsql As String
            Dim Tabla As New DataTable
            DB = New NM_Consulta(4)
            Dim fila As DataRow
            strsql = "SELECT MP.codigo_maquina_paro,MP.codigo_linea,MP.codigo_tipo_maquina," & _
            " MP.codigo_paro_produccionD, PP.descripcion_paro_produccion, MP.codigo_maquina," & _
            " MP.fecha_inicio,MP.fecha_fin,MP.hora_inicio,MP.hora_fin" & _
            " FROM NM_MaquinaParo MP join NM_ParosProduccionD PP" & _
            " ON MP.codigo_linea = PP.codigo_linea and" & _
            " MP.codigo_tipo_maquina =PP.codigo_tipo_maquina and" & _
            " MP.codigo_paro_produccionD= PP.codigo_paro_produccionD " & _
            " where MP.codigo_maquina_paro=" & pcodigo_maquina_paro & "'"
            Tabla = DB.Query(strsql)
            For Each fila In Tabla.Rows
                If Not IsDBNull(fila("codigo_maquina_paro")) Then codigo_maquina_paro = codigo_maquina_paro = fila("codigo_maquina_paro")
                If Not IsDBNull(fila("codigo_linea")) Then codigo_linea = fila("codigo_linea")
                If Not IsDBNull(fila("codigo_tipo_maquina")) Then codigo_tipo_maquina = fila("codigo_tipo_maquina")
                If Not IsDBNull(fila("codigo_paro_produccionD")) Then codigo_paro_produccionD = fila("codigo_paro_produccionD")
                If Not IsDBNull(fila("descripcion_paro_produccion")) Then descripcion_paro_produccion = fila("descripcion_paro_produccion")
                If Not IsDBNull(fila("codigo_maquina")) Then codigo_maquina = fila("codigo_maquina")
                If Not IsDBNull(fila("fecha_inicio")) Then fecha_inicio = fila("fecha_inicio")
                If Not IsDBNull(fila("fecha_fin")) Then fecha_fin = fila("fecha_fin")
                If Not IsDBNull(fila("hora_inicio")) Then hora_inicio = fila("hora_inicio")
                If Not IsDBNull(fila("hora_fin")) Then hora_fin = fila("hora_fin")
            Next
        End Sub

        Public Function list(ByVal pfecha As Date) As DataTable
            DB = New NM_Consulta(4)
            Dim strsql As String
            strsql = strsql & "SELECT MP.codigo_maquina_paro,MP.codigo_linea,MP.codigo_tipo_maquina,"
            strsql = strsql & " MP.codigo_paro_produccionD, PP.descripcion_paro_produccion, MP.codigo_maquina,"
            strsql = strsql & " MP.fecha_inicio,MP.fecha_fin,MP.hora_inicio,MP.hora_fin"
            strsql = strsql & " FROM NM_MaquinaParo MP join NM_ParosProduccionD PP"
            strsql = strsql & " ON MP.codigo_linea = PP.codigo_linea and"
            strsql = strsql & " MP.codigo_tipo_maquina =PP.codigo_tipo_maquina and"
            strsql = strsql & " MP.codigo_paro_produccionD= PP.codigo_paro_produccionD "
            strsql = strsql & "WHERE fecha_inicio = '" & objUtil.FormatFecha(pfecha) & "' Order by MP.usuario_modificacion"

            Return DB.Query(strsql)
        End Function
        Public Function list() As DataTable
            DB = New NM_Consulta(4)
            Dim strsql As String
            strsql = strsql & "SELECT MP.codigo_maquina_paro,MP.codigo_linea,MP.codigo_tipo_maquina,"
            strsql = strsql & " MP.codigo_paro_produccionD, PP.descripcion_paro_produccion, MP.codigo_maquina,"
            strsql = strsql & " MP.fecha_inicio,MP.fecha_fin,MP.hora_inicio,MP.hora_fin"
            strsql = strsql & " FROM NM_MaquinaParo MP join NM_ParosProduccionD PP"
            strsql = strsql & " ON MP.codigo_linea = PP.codigo_linea and"
            strsql = strsql & " MP.codigo_tipo_maquina =PP.codigo_tipo_maquina and"
            strsql = strsql & " MP.codigo_paro_produccionD= PP.codigo_paro_produccionD "
            Return DB.Query(strsql)
        End Function

        Public Function exist(ByVal pcodigo_maquina_paro As String) As Boolean
            DB = New NM_Consulta
            Dim tabla As New DataTable
            Dim strsql As String = "SELECT * FROM NM_MaquinaParo Where codigo_maquina_paro = '" & pcodigo_maquina_paro & "'"
            tabla = DB.Query(strsql)
            If tabla.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace