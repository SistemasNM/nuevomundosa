Option Strict On

Namespace NM_Tintoreria.Proceso

    Public MustInherit Class PruebaBase

        Public MustOverride Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

    End Class

End Namespace