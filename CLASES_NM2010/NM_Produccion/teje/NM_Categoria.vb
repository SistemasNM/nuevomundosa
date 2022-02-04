Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_Categoria

        Private conexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = conexion.ObtenerDataTable("pr_NM_Categoria_Select")
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function

    End Class
End Namespace

