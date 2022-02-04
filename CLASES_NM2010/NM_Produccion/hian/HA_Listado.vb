Imports NM.AccesoDatos

Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia
  Public Class HA_Listado

    Function ListMezcla(ByVal strGrupo As String) As DataTable
      Dim dt As New DataTable()
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"codigo_grupo", strGrupo}

      Try
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
        dt = Conexion.ObtenerDataTable("usp_HA_CompMezcla_listar", objParametro)
      Catch ex As Exception
        Throw ex
      Finally
        Conexion = Nothing
      End Try

      Return dt

    End Function

  End Class
End Namespace
