Imports NM.AccesoDatos

Public Class General
#Region "VARIABLES PRIVADAS"
    Private _objConexion As AccesoDatosSQLServer
#End Region


    Public Function ConvertirFecha(ByVal pstrFecha As String) As String
        Try
            Dim larrPartes() As String
            larrPartes = Split(pstrFecha, "/")
            Return CStr(larrPartes(2) + larrPartes(1) + larrPartes(0))
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function ufn_TablaParametro_Obtener(ByVal strCodigoTabla As String) As DataTable
        Try
            Dim objParametros As Object() = {"chr_CodigoTabla", strCodigoTabla}
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Return _objConexion.ObtenerDataTable("usp_ADM_TablaMaestraColumnaDato_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
