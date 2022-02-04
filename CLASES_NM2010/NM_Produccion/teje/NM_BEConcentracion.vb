Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_BeConcentracion

        Public idBeConcentracion As Integer
        Public Be As Double
        Public Concentracion As Double
        Public usuario_creacion As String
        Public fecha_creacion As Date
        Public usuario_modificacion As String
        Public fecha_modificacion As Date
        Dim DB As New NM_Consulta()

        Public Sub New()
            Be = 0
            Concentracion = 0
            usuario_creacion = ""
            fecha_creacion = Date.Today
            usuario_modificacion = ""
            fecha_modificacion = Date.Today
        End Sub

        Public Function insert() As Integer
            Dim strsql As String
            strsql = "INSERT INTO NM_BeConcentracion values("
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append(Be & ",")
            commandString.Append(Concentracion & ",")
            commandString.Append("'" & usuario_creacion & "',")
            commandString.Append(fecha_creacion & ",")
            commandString.Append("'" & usuario_modificacion & "',")
            commandString.Append(fecha_modificacion & ")")
            Try
                DB.Execute(commandString.ToString)
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Function Listar() As DataTable
            Return DB.getData("NM_BeConcentracion")
        End Function

        Public Function actualizar(ByVal pId As Integer, ByVal pBe As Double, ByVal pConcentracion As Double, ByVal pusuario_modificacion As String, ByVal pfecha_modificacion As Date) As Integer
            Dim strsql As String
            strsql = "UPDATE NM_BeConcentracion SET "
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append(strsql)
            commandString.Append("Be =" & pBe & ",")
            commandString.Append("Concentracion =" & pConcentracion & ",")
            commandString.Append("usuario_modificacion = '" & pusuario_modificacion & "',")
            commandString.Append("fecha_modificacion = " & pfecha_modificacion)
            commandString.Append(" Where idBeConcentracion = " & pId)
            Try
                DB.Execute(commandString.ToString)
                Return 1
            Catch
                Return 0
            End Try
        End Function

        Public Function Delete(ByVal pId As Integer)
            Dim strsql As String
            strsql = "DELETE FROM NM_BeConcentracion Where idBeConcentracion = "
            strsql = strsql & pId
            Try
                DB.Execute(strsql)
                Return 1
            Catch
                Return 0
            End Try
        End Function

    End Class

End Namespace