Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_EstandaresTolerancia
        Public codigo_articulo As String
        Public codigo_estandar As String
        Public codigo_revision As Integer
        Public tolerancia As Integer
        Public Usuario_creacion As String
        Public Usuario_modificacion As String
        Private DB As NM_Consulta

        Public Function insert(ByVal pcodigo_articulo As String, ByVal pcodigo_estandar As String, _
             ByVal ptolerancia As Double, ByVal pUsuario_creacion As String) As Integer
            DB = New NM_Consulta()
            If pcodigo_articulo <> "" Then
                Dim strsql As String
                strsql = "INSERT INTO NM_EstandaresTolerancia values("
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("'" & pcodigo_articulo & "',")
                commandString.Append("'" & pcodigo_estandar & "',")
                commandString.Append(0 & ",")
                commandString.Append(ptolerancia & ",")
                commandString.Append("'" & pUsuario_creacion & "',")
                commandString.Append("GetDate(),")
                commandString.Append("'" & pUsuario_creacion & "',")
                commandString.Append("GetDate())")

                Try
                    DB.Execute(commandString.ToString)
                Catch
                End Try
            End If
            DB = Nothing
        End Function

        Private Function getLastRevision(ByVal codArt As String, ByVal pcodEstandar As String) As Integer
            DB = New NM_Consulta()
            Dim tabla As New DataTable()
            Dim fila As DataRow
            Dim rev As String
            tabla = DB.Query("SELECT * FROM NM_EstandaresTolerancia where codigo_articulo ='" & codArt & "' and codigo_estandar ='" & pcodEstandar & "'")
            For Each fila In tabla.Rows
                rev = fila("codigo_revision")
                Exit For
            Next
            Return CInt(rev)
        End Function

        Public Function update(ByVal pcodigo_articulo As String, ByVal pcodigo_estandar As String, _
             ByVal ptolerancia As Double, Optional ByVal pUsuario_modificacion As String = "dbo") As Integer
            DB = New NM_Consulta()
            If pcodigo_articulo <> "" Then
                Dim strsql As String
                strsql = "UPDATE NM_EstandaresTolerancia SET "
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("tolerancia  = " & ptolerancia & ",")
                commandString.Append("codigo_revision  = " & getLastRevision(pcodigo_articulo, pcodigo_estandar) + 1 & ",")
                commandString.Append("usuario_modificacion = '" & pUsuario_modificacion & "',")
                commandString.Append("fecha_modificacion = getdate()")
                commandString.Append(" where codigo_articulo = '" & pcodigo_articulo & "'")
                commandString.Append(" and codigo_estandar = '" & pcodigo_estandar & "'")
                Try
                    DB.Execute(commandString.ToString)
                    Return 1
                Catch
                    Throw
                    Return 0
                End Try
                DB = Nothing
            End If
        End Function

        'esta función busca la tela que correponde al codigo ingresado y sus tolernacias respectivas
        Public Function SeekXTela(ByVal pcodigo_Articulo As String) As DataTable
            Dim db As New NM_Consulta()
            Dim tela As New NM_Tela()
            Dim strsql As String
            Dim fila As DataRow
            Dim i As Integer
            Dim tabla As New DataTable()
            tela.Seek(pcodigo_Articulo)
            codigo_revision = tela.revision_tela ' se almacena en la variable revision
            strsql = "SELECT * FROM NM_EstandaresTolerancia where codigo_articulo ='" & pcodigo_Articulo & "'"
            tabla = db.Query(strsql)
            ' Si no tiene ningun dato ingresado en NM_EstandaresTolerancia, se crea unos nuevos inicailizados en cero
            If tabla.Rows.Count <= 0 Then
                For i = 1 To 9
                    insert(pcodigo_Articulo, ("EST00" & i), 0, Usuario_creacion)
                Next i
                ' strsql = "SELECT * FROM NM_EstandaresTolerancia where codigo_articulo ='" & pcodigo_Articulo & "'"
                Return db.Query(strsql)
            End If
            Return tabla
        End Function

    End Class
End Namespace
