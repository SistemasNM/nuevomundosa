Imports NM.AccesoDatos

Public Class NM_Logistica
#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
#End Region

    Function ListarHilos(ByVal strCodigo As String, ByVal strNombre As String)
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_CodigoHilo", strCodigo, "var_NombreHilo", strNombre}
            Return _objConexion.ObtenerDataTable("usp_LOG_Hilos_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
