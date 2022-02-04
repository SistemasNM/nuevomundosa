Imports NM.AccesoDatos

Public Class Logeo


#Region " Declaracion de Variables Miembro "
    Private m_sqlCuestionario As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlCuestionario = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region
    Public Function ValidarUsuario(ByVal usuario As String, ByVal clave As String) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"vch_usuario", usuario, "vch_clave", clave}
            dtblDatos = m_sqlCuestionario.ObtenerDataTable("USP_NM_RRHH_VALIDA_USUARIO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
End Class

