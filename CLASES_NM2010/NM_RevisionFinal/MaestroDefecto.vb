Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class MaestroDefecto
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoDefecto As String
        Private m_strDescripcionDefecto As String
        Private m_strDescripcionArea As String
        Private m_strTi_Situ As String
        Private m_strUsuarioCreacion As String
        Private m_strCodigoMotivo As String
        Private m_strDescripcionMotivo As String
#End Region

#Region " Definción de constructores "
        Sub New()
            m_strCodigoDefecto = String.Empty
            m_strDescripcionDefecto = String.Empty
            m_strDescripcionArea = String.Empty
            m_strTi_Situ = String.Empty
            m_strUsuarioCreacion = String.Empty
            m_strCodigoMotivo = String.Empty
            m_strDescripcionMotivo = String.Empty
        End Sub

        Sub New(ByVal strCodigoDefecto As String, ByVal strDescripcionDefecto As String, ByVal strDescripcionArea As String, ByVal strTi_Situ As String, ByVal strUsuario As String, ByVal strCodigoMotivo As String, ByVal strDescripcionMotivo As String)
            m_strCodigoDefecto = strCodigoDefecto
            m_strDescripcionDefecto = strDescripcionDefecto
            m_strDescripcionArea = strDescripcionArea
            m_strTi_Situ = strTi_Situ
            m_strUsuarioCreacion = strUsuario
            m_strCodigoMotivo = strCodigoMotivo
            m_strDescripcionMotivo = strDescripcionMotivo
        End Sub
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerDefectos() As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Return adSQL.ObtenerDataTable("UP_ListarDefectos")

        End Function
  

        Public Sub Insertar()
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoDefecto", m_strCodigoDefecto, _
                                            "strDescripcionDefecto", m_strDescripcionDefecto, _
                                            "strDescripcionArea", m_strDescripcionArea, _
                                            "strTi_Situ", m_strTi_Situ, _
                                            "strUsuario", m_strUsuarioCreacion, _
                                            "strCodigoMotivo", m_strCodigoMotivo, _
                                            "strDescripcionMotivo", m_strDescripcionMotivo}
            adSQL.EjecutarComando("UP_InsertarMaestroDefecto", parametros)
        End Sub

        Public Sub Actualizar()

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoDefecto", m_strCodigoDefecto, _
                                            "strDescripcionDefecto", m_strDescripcionDefecto, _
                                            "strDescripcionArea", m_strDescripcionArea, _
                                            "strTi_Situ", m_strTi_Situ, _
                                            "strUsuario", m_strUsuarioCreacion, _
                                            "strCodigoMotivo", m_strCodigoMotivo, _
                                            "strDescripcionMotivo", m_strDescripcionMotivo}

            adSQL.EjecutarComando("UP_ActualizarMaestroDefecto", parametros)

        End Sub

        Public Sub Eliminar(ByVal strCodigoDefecto As String)

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoDefecto", strCodigoDefecto}

            adSQL.EjecutarComando("UP_EliminarMaestroDefecto", parametros)

        End Sub
        Public Function ObtenerDefectoCodigo(ByVal strCodigo_Defecto As String) As DataTable
            '*****************************************************************************************************
            'Objetivo   : Actualizar defecto por codigo
            'Autor      : Juan Cucho Antunez
            'Creado     : 08/03/2017
            'Modificado : //
            '*****************************************************************************************************
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"PCodigo_Defecto", strCodigo_Defecto}
            Return adSQL.ObtenerDataTable("USP_RVF_OBTENER_DEFECTO_CODIGO", parametros)
        End Function
        Public Function ObtenerMotivosDelMaestrosDefectos() As DataTable
            '*****************************************************************************************************
            'Objetivo   : Obtener motivos del maestros de defectos
            'Autor      : Juan Cucho Antunez
            'Creado     : 08/03/2017
            'Modificado : //
            '*****************************************************************************************************
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {}
            Return adSQL.ObtenerDataTable("USP_RVF_OBTENER_MOTIVOS_DEL_MAESTRO_DEFECTOS", parametros)
        End Function
        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub
#End Region

    End Class
End Namespace
