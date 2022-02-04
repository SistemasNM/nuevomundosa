Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Estandares
        Public codigo_estandar As String
        Public descripcion_estandar As String
        Private valor_estandar As Double
        Public usuario_creacion As String
        Public fecha_creacion As Date
        Public usuario_modificacion As String
        Public fecha_modificacion As Date
        Private DB As NM_Consulta

        Property valor(ByVal pcodigo_estandar As String) As Double
            Get
                DB = New NM_Consulta()
                Dim strSql As String
                Dim tabla As New DataTable()
                Dim fila As DataRow
                strSql = "Select * from NM_estandares where codigo_estandar = '" & pcodigo_estandar & "'"
                tabla = DB.Query(strSql)
                Try
                    For Each fila In tabla.Rows
                        valor_estandar = CDbl(fila("valor_estandar"))
                        Exit For
                    Next
                    Return valor_estandar
                Catch
                End Try
            End Get
            Set(ByVal Value As Double)
                If IsNumeric(Value) Then
                    Dim strsql As String = "UPDATE NM_estandares SET valor_estandar =" & Value & _
                                    " where codigo_estandar = '" & pcodigo_estandar & "'"
                    Try
                        DB.Execute(strsql)
                    Catch
                    End Try
                    valor_estandar = Value
                End If
            End Set
        End Property

        Public Sub Seek(ByVal pcodigo_estandar As String)
            DB = New NM_Consulta()
            Dim strSql As String
            Dim tabla As New DataTable()
            Dim fila As DataRow
            strSql = "Select * from NM_estandares where codigo_estandar = '" & pcodigo_estandar & "'"
            tabla = DB.Query(strSql)
            For Each fila In tabla.Rows
                codigo_estandar = fila("codigo_estandar")
                descripcion_estandar = fila("descripcion_estandar")
                valor_estandar = fila("valor_estandar")
                Exit For
            Next
        End Sub

        Public Function Seek() As DataTable
            DB = New NM_Consulta()
            Return DB.getData("NM_estandares")
        End Function

        


    End Class

End Namespace
