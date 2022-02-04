Imports Microsoft.SqlServer.Dts.Runtime
Public Class EventListener
    Inherits DefaultEvents

    Public Overrides Function OnError(ByVal source As Microsoft.SqlServer.Dts.Runtime.DtsObject, _
      ByVal errorCode As Integer, ByVal subComponent As String, ByVal description As String, _
      ByVal helpFile As String, ByVal helpContext As Integer, _
      ByVal idofInterfaceWithError As String) As Boolean

        ' Add application–specific diagnostics here.
        Console.WriteLine("Error in {0}/{1} : {2}", source, subComponent, description)
        Return False

    End Function
End Class
