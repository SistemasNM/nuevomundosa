Imports NM.AccesoDatos
Namespace Maestros
    Public Class InsumoQuimico
#Region "    Metodos"
        Public Function Listar() As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_InsumoQuimico_Listar")
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtDatos
        End Function

        Public Function Obtener(ByVal strCodigoIQ As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim lobjParametros() As String = {"var_CodigoInsumo", strCodigoIQ}
            Try
                ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_InsumoQuimico_Obtener", lobjParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtDatos
        End Function
#End Region
    End Class
End Namespace
