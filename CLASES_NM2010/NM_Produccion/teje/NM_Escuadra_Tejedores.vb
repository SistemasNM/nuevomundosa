Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Escuadras
        Public codigo_escuadra As String
        Public nombre_escuadra As String
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modicacion As String
        Protected Nombre_Tabla As String
        Private DB As New NM_Consulta()

        Sub New(ByVal pNombre_Tabla As String)
            Nombre_Tabla = pNombre_Tabla
        End Sub

        Public Function insertar(ByVal pcodigo_escuadra As String, ByVal pnombre_escuadra As String, ByVal pusuario_creacion As String) As Integer
            Dim strsql As String
            strsql = "INSERT INTO " & Nombre_Tabla & " values("
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("'" & pcodigo_escuadra & "',")
            commandString.Append("'" & pnombre_escuadra & "',")
            commandString.Append("'" & pusuario_creacion & "',")
            commandString.Append("GetDate(),")
            commandString.Append("NULL,")
            commandString.Append("GetDate())")
            Try
                DB.Execute(commandString.ToString)
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Function listar() As DataTable
            Try
                Return DB.getData(Nombre_Tabla)
            Catch
            End Try
        End Function

        Public Function Actualizar(ByVal pcodigo_escuadra As String, ByVal pnombre_escuadra As String, ByVal pusuario_modificacion As String) As Integer
            If pcodigo_escuadra <> "" Then
                Dim strsql As String
                strsql = "UPDATE " & Nombre_Tabla & " SET "
                Dim commandString As New System.Text.StringBuilder()
                commandString.Append(strsql)
                commandString.Append("nombre_escuadra = '" & pnombre_escuadra & "',")
                commandString.Append("usuario_modificacion = '" & pusuario_modificacion & "',")
                commandString.Append("fecha_modificacion = getdate()")
                commandString.Append(" where codigo_escuadra = '" & pcodigo_escuadra & "'")
                Try
                    DB.Execute(commandString.ToString)
                    Return 1
                Catch
                    Return 0
                End Try
            End If
        End Function

        Public Function delete(ByVal pcodigo_escuadra As String) As Integer
            Dim strsql As String
            strsql = "DELETE FROM " & Nombre_Tabla & " where codigo_escuadra = '" & pcodigo_escuadra & "'"
            Try
                DB.Execute(strsql)
                Return 1
            Catch
                Return 0
            End Try
        End Function
    End Class


    Public Class NM_Escuadra_Tejedores
        Inherits NM_Escuadras

        Sub New()
            MyBase.New("NM_Escuadra_Tejedores")
        End Sub

    End Class
End Namespace

