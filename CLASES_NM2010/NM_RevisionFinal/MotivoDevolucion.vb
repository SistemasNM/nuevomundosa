Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class MotivoDevolucion
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoMotivo As String
        Private m_strDescripcionMotivo As String
        Private m_strTi_Situ As String
        Private m_strUsuarioCreacion As String
        'Private m_sqlDtAccRevisionFinal As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
        Sub New()
            m_strCodigoMotivo = String.Empty
            m_strDescripcionMotivo = String.Empty
            m_strTi_Situ = String.Empty
            m_strUsuarioCreacion = String.Empty
            'm_sqlDtAccRevisionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub
        Sub New(ByVal strCodigoMotivo As String, ByVal strDescripcionMotivo As String, ByVal strTi_Situ As String, ByVal strUsuario As String)
            m_strCodigoMotivo = strCodigoMotivo
            m_strDescripcionMotivo = strDescripcionMotivo
            m_strTi_Situ = strTi_Situ
            m_strUsuarioCreacion = strUsuario
        End Sub
#End Region
#Region " Definición de Métodos "
        Public Overloads Function Listar() As DataTable
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Return adSQL.ObtenerDataTable("USP_RVF_LISTAR_MOTIVOS_DEFECTO")
        End Function
        Public Sub Insertar()
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strDescripcionMotivo", m_strDescripcionMotivo, _
                                            "strTi_Situ", m_strTi_Situ}
            adSQL.EjecutarComando("USP_RVF_INSERTAR_MOTIVOS_DEFECTO", parametros)
        End Sub
        Public Sub Actualizar()
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoMotivo", m_strCodigoMotivo, _
                                            "strDescripcionMotivo", m_strDescripcionMotivo, _
                                            "strTi_Situ", m_strTi_Situ}
            adSQL.EjecutarComando("USP_RVF_ACTUALIZAR_MOTIVOS_DEFECTO", parametros)
        End Sub
        Public Sub Eliminar(ByVal strCodigoMotivo As String)
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoMotivo", strCodigoMotivo}
            adSQL.EjecutarComando("USP_RVF_ELIMINAR_MOTIVOS_DEFECTO", parametros)
        End Sub

        Public Function Buscar(ByVal strCodigoMotivo As String, ByVal strDescripcionMotivo As String) As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Try
                Dim parametros As Object() = {"CODIGO_DEFECTO", strCodigoMotivo,
                                          "descripcion_defecto", strDescripcionMotivo}
                Return adSQL.ObtenerDataTable("USP_RVF_BUSCAR_DEFECTO", parametros)
            Catch ex As Exception
                Throw ex
            End Try
            
        End Function

        Public Function BuscarReingreso(ByVal strCodigoMotivo As String, ByVal strDescripcionMotivo As String) As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Try
                Dim parametros As Object() = {"CODIGO_DEFECTO", strCodigoMotivo,
                                              "descripcion_defecto", strDescripcionMotivo}
                Return adSQL.ObtenerDataTable("USP_RVF_BUSCAR_REINGRESO", parametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function





        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub
#End Region

       
    End Class

End Namespace