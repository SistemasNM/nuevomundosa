Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_TipoCategoria

        Private conexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

        Public Function ListarTipo_por_Categoria(ByVal pCodigoCategoria As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros() As Object = {"codigo_categoria", pCodigoCategoria}
                dtblDatos = conexion.ObtenerDataTable("pr_NM_TipoCategoria_por_Tipo_Select", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function

    End Class
End Namespace