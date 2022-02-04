Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria

    Public Class NM_RoturasFiletaTED
        Public codigo_partida_engomadoted As String
        Public Codigo_plegador As String
        Public Nro_Carrete As Integer
        Public HiloFlojo As Integer
        Public Madeja As Integer
        Public Zanja As Integer
        Public Zanja_anudado As Integer
        Public Hilo_Cruzado As Integer
        Public Cruzado_Anudado As Integer
        Public Champa As Integer
        Public Metraje As Integer
        Public Usuario_Creacion As String
        Public Fecha_creacion As Date
        Public Usuario_Modificacion As String
        Public Fecha_Modificacion As Date
        Public dtRoturas As New DataTable()
        Public Total As Integer = 0
        Public Sub New()
            codigo_partida_engomadoted = ""
            Codigo_plegador = ""
            Nro_Carrete = 0
            HiloFlojo = 0
            Madeja = 0
            Zanja = 0
            Zanja_anudado = 0
            Hilo_Cruzado = 0
            Cruzado_Anudado = 0
            Champa = 0
            Metraje = 0
            Usuario_Creacion = "dbo"
            Fecha_creacion = Date.Today
            Usuario_Modificacion = "dbo"
            Fecha_Modificacion = Date.Today
        End Sub

        Public Function insert() As Integer
            Dim DB As New NM_Consulta()
            Dim strsql As String
            strsql = "Insert into NM_RoturasFiletaTED (codigo_partida_engomadoted," & _
            " Codigo_plegador, Nro_Carrete, HiloFlojo,Madeja,Zanja,Zanja_anudado," & _
            " Hilo_Cruzado,Cruzado_Anudado,Champa, Metraje,Usuario_Creacion,Fecha_creacion,Usuario_Modificacion ,Fecha_Modificacion) values("
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("'" & codigo_partida_engomadoted & "',")    ' el codigo es autogenerado
            commandString.Append("'" & Codigo_plegador & "',")
            commandString.Append(Nro_Carrete & ",")
            commandString.Append(HiloFlojo & ",")
            commandString.Append(Madeja & ",")
            commandString.Append(Zanja & ",")
            commandString.Append(Zanja_anudado & ",")
            commandString.Append(Hilo_Cruzado & ",")
            commandString.Append(Cruzado_Anudado & ",")
            commandString.Append(Champa & ",")
            commandString.Append(Metraje & ",")
            commandString.Append("'" & Usuario_Creacion & "',")
            commandString.Append(Fecha_creacion & ",")
            commandString.Append("'" & Usuario_Modificacion & "',")
            commandString.Append(Fecha_Modificacion & ")")
            Try
                DB.Query(commandString.ToString)                ' devuelve las roturas correspondientes al plegador
                dtRoturas = getRoturas(codigo_partida_engomadoted, Codigo_plegador)
                Total = GetTotal()
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Function update() As Integer
            Dim DB As New NM_Consulta()
            Dim strsql As String
            strsql = "UPDATE NM_RoturasFiletaTED SET "
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("Nro_Carrete =" & Nro_Carrete & ",")
            commandString.Append("HiloFlojo= " & HiloFlojo & ",")
            commandString.Append("Madeja =" & Madeja & ",")
            commandString.Append("Zanja =" & Zanja & ",")
            commandString.Append("Zanja_anudado =" & Zanja_anudado & ",")
            commandString.Append("Hilo_Cruzado =" & Zanja_anudado & ",")
            commandString.Append("Cruzado_Anudado =" & Cruzado_Anudado & ",")
            commandString.Append("Champa =" & Champa & ",")
            commandString.Append("Usuario_Modificacion = '" & Usuario_Modificacion & "',")
            commandString.Append("Fecha_Modificacion = " & Fecha_Modificacion)
            commandString.Append(" where codigo_partida_engomadoted = '" & codigo_partida_engomadoted & "'")
            commandString.Append(" and Codigo_plegador = '" & Codigo_plegador & "'")
            commandString.Append(" and Nro_Carrete = " & Nro_Carrete)
            Try
                IsNothing(DB.Query(commandString.ToString))
                dtRoturas = getRoturas(codigo_partida_engomadoted, Codigo_plegador)
                Total = GetTotal()
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Function delete() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "DELETE FROM NM_RoturasFiletaTED WHERE"
            strsql = strsql & " codigo_partida_engomadoted = '" & codigo_partida_engomadoted & "' "
            strsql = strsql & "and  Codigo_plegador = '" & Codigo_plegador & "'"
            Try
                Return objConn.Execute(strsql)
                'dtRoturas = getRoturas(codigo_partida_engomadoted, Codigo_plegador)
                'Total = GetTotal()
                'Return 1
            Catch
                Return False
            End Try
        End Function

        Public Function delete(ByVal pNro_Carrete) As Integer
            Dim DB As New NM_Consulta
            Dim strsql As String
            strsql = "DELETE FROM NM_RoturasFiletaTED WHERE"
            strsql = strsql & " codigo_partida_engomadoted = '" & codigo_partida_engomadoted & "' "
            strsql = strsql & "and  Codigo_plegador = '" & Codigo_plegador & "'"
            strsql = strsql & " and  Nro_Carrete = " & pNro_Carrete
            Try
                IsNothing(DB.Query(strsql))
                dtRoturas = getRoturas(codigo_partida_engomadoted, Codigo_plegador)
                Total = GetTotal()
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Function getRoturas(ByVal codigo_partida_engomadoted As String, ByVal plegador As String) As DataTable
            Dim DB As New NM_Consulta
            Dim strsql As String
            strsql = "Select * from NM_RoturasFiletaTED where" & _
            " codigo_partida_engomadoted = '" & codigo_partida_engomadoted & "'" & _
            " and Codigo_plegador = '" & plegador & "'"
            dtRoturas = DB.Query(strsql)
            Return dtRoturas
        End Function
        'obtiene el total de roturas

        Public Function GetTotal() As Integer
            Dim suma As Integer = 0
            Dim fila As DataRow
            For Each fila In dtRoturas.Rows
                'fila(codigo_partida_engomadoted()
                'fila(Codigo_plegador()
                'fila(Nro_Carrete()
                If Not IsDBNull(fila("HiloFlojo")) Then suma = suma + fila("HiloFlojo")
                If Not IsDBNull(fila("Madeja")) Then suma = suma + fila("Madeja")
                If Not IsDBNull(fila("Zanja")) Then suma = suma + fila("Zanja")
                If Not IsDBNull(fila("Zanja_anudado")) Then suma = suma + fila("Zanja_anudado")
                If Not IsDBNull(fila("Hilo_Cruzado")) Then suma = suma + fila("Hilo_Cruzado")
                If Not IsDBNull(fila("Cruzado_Anudado")) Then suma = suma + fila("Cruzado_Anudado")
                If Not IsDBNull(fila("Champa")) Then suma = suma + fila("Champa")
                If Not IsDBNull(fila("Metraje")) Then suma = suma + fila("Metraje")
            Next
            Return suma
        End Function
        Public Function GetTotal(ByVal codigo_partida_engomadoted, ByVal Codigo_plegador) As Integer
            Dim suma As Integer = 0
            Dim fila As DataRow
            dtRoturas = getRoturas(codigo_partida_engomadoted, Codigo_plegador)
            For Each fila In dtRoturas.Rows
                'fila(codigo_partida_engomadoted()
                'fila(Codigo_plegador()
                'fila(Nro_Carrete()
                If Not IsDBNull(fila("HiloFlojo")) Then suma = suma + fila("HiloFlojo")
                If Not IsDBNull(fila("Madeja")) Then suma = suma + fila("Madeja")
                If Not IsDBNull(fila("Zanja")) Then suma = suma + fila("Zanja")
                If Not IsDBNull(fila("Zanja_anudado")) Then suma = suma + fila("Zanja_anudado")
                If Not IsDBNull(fila("Hilo_Cruzado")) Then suma = suma + fila("Hilo_Cruzado")
                If Not IsDBNull(fila("Cruzado_Anudado")) Then suma = suma + fila("Cruzado_Anudado")
                If Not IsDBNull(fila("Champa")) Then suma = suma + fila("Champa")
                If Not IsDBNull(fila("Metraje")) Then suma = suma + fila("Metraje")
            Next
            Return suma
        End Function

    End Class

End Namespace