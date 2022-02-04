Imports NM.AccesoDatos
Public Class Reportes
#Region " Declaracion de Variables Miembro "
    Private m_sqlreportes As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlreportes = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.NMEVALDESEMP)
    End Sub
#End Region


    Public Function ObtenerComentarios(ByVal Evaluacion As Integer, ByVal Evaluado As Int32) As DataTable
        Dim dtblDatos As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluado", Evaluado}
            dtblDatos = m_sqlreportes.ObtenerDataTable("USP_NM_RRHH_LISTAR_COMENTARIOS_REPORTE", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function ObtenerGerencias() As DataTable
        Try 
            Return m_sqlreportes.ObtenerDataTable("NM_RRHH_LISTAR_GERENCIAS")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ObtenerAreas(ByVal IdGerencia As Int32) As DataTable
        Dim dtblAreas As DataTable
        Try
            Dim objParametros As Object() = {"int_IdGerencia", IdGerencia}
            dtblAreas = m_sqlreportes.ObtenerDataTable("NM_RRHH_LISTAR_AREAS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblAreas
    End Function
    Public Function ObtenerPuesto() As DataTable
        Dim dtblAreas As DataTable
        Try
            'Dim objParametros As Object() = {"int_IdGerencia", IdGerencia, "int_IdArea", IdArea}
            dtblAreas = m_sqlreportes.ObtenerDataTable("NM_RRHH_LISTAR_PUESTOS")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblAreas
    End Function

    Public Function Obtenerusuarios(ByVal Evaluacion As Integer, ByVal Gerencia As Int32, ByVal Area As Int32, ByVal Usuario As String) As DataTable
        Dim dtblEvaluados As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdGerencia", Gerencia, _
                                             "int_IdArea", Area, _
                                             "vch_Usuario", Usuario}
            dtblEvaluados = m_sqlreportes.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUADOS_GERENCIA_AREA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblEvaluados
    End Function

    Public Function ObtenerEvaluadores(ByVal Evaluacion As Integer, ByVal Gerencia As Int32, ByVal Area As Int32) As DataTable
        Dim dtblAreas As DataTable
        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                              "int_IdGerencia", Gerencia, _
                                              "int_IdArea", Area}
            dtblAreas = m_sqlreportes.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUADORES_GERENCIA_AREA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblAreas
    End Function

    Public Function ObtenerEvaluadoresImprimir(ByVal Evaluacion As Integer, ByVal Evaluador As Integer, ByVal Usuario As String) As DataTable
        Dim dtblEvaluados As DataTable

        Try
            Dim objParametros As Object() = {"int_IdEvaluacion", Evaluacion, _
                                             "int_IdEvaluador", Evaluador, _
                                             "vch_Usuario", Usuario}

            dtblEvaluados = m_sqlreportes.ObtenerDataTable("USP_NM_RRHH_LISTAR_EVALUADORES_X_IMPRIMIR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblEvaluados
    End Function

    Public Function ObtenerCredencialesReportingService() As System.Net.NetworkCredential
        Dim creds As New System.Net.NetworkCredential
        'System.Net.CredentialCache.DefaultCredentials
        creds.Domain = "NUEVOMUNDOSA"
        creds.UserName = "nmsic"
        creds.Password = "Asmrp.159"

        Return creds

    End Function

End Class
