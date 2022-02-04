Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal

    Public Class DevolucionAlmacenD
        '        Implements IDisposable

        '#Region " Declaración de Variables Miembro "
        '        Private m_strCodigoDevolucion As String
        '        Private m_strCodigoRollo As String
        '        Private m_strCodigoDefecto As String
        '        Private m_strUsuarioCreacion As String
        '        'Private m_dteFechaCreacion As Date
        '        'Private m_strUsuarioModificacion As String
        '        'Private m_dteFechaModificacion As Date
        '#End Region

        '#Region " Definción de constructores "
        '        Sub New()
        '            'm_strCodigoDefecto = String.Empty
        '            'm_strDescripcionDefecto = String.Empty
        '            'm_strUsuarioCreacion = String.Empty
        '            ''m_dteFechaCreacion = Now
        '            ''m_strUsuarioModificacion = String.Empty
        '            ''m_dteFechaModificacion = Now
        '        End Sub

        '        Sub New(ByVal strCodigoDevolucion As String, ByVal strCodigoRollo As String, ByVal strCodigoDefecto As String)
        '            m_strCodigoDevolucion = strCodigoDevolucion
        '            m_strCodigoRollo = strCodigoRollo
        '            m_strCodigoDefecto = strCodigoDefecto

        '            m_strUsuarioCreacion = String.Empty
        '            'm_dteFechaCreacion = Now
        '            'm_strUsuarioModificacion = String.Empty
        '            'm_dteFechaModificacion = Now
        '        End Sub
        '#End Region

        '#Region " Definición de Métodos "
        '        '------------------------------------------------------------------------------------------------
        '        Public Overloads Function ObtenerDevoluciones(ByVal strCodigoDevolucion As String) As DataTable

        '            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '            Dim parametros As Object() = {"strCodigoDevolucionA", strCodigoDevolucion}

        '            Return adSQL.ObtenerDataTable("UP_ListarDevoluciones", parametros)

        '        End Function

        '        Public Sub Insertar()

        '            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '            Dim parametros As Object() = {"strCodigoDevolucion", m_strCodigoDevolucion, _
        '                                            "strCodigoRollo", m_strCodigoRollo, _
        '                                            "strCodigoDefecto", m_strCodigoDefecto, _
        '                                            "strUsuario", m_strUsuarioCreacion}

        '            adSQL.EjecutarComando("UP_InsertarDevolucionAlmacenD", parametros)

        '        End Sub

        '        Public Sub Actualizar()

        '            'Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '            'Dim parametros As Object() = {"strCodigoDefecto", m_strCodigoDefecto, _
        '            '                                "strDescripcionDefecto", m_strDescripcionDefecto, _
        '            '                                "strUsuario", m_strUsuarioCreacion}

        '            'adSQL.EjecutarComando("UP_ActualizarMaestroDefecto", parametros)

        '        End Sub

        '        Public Sub Eliminar(ByVal strCodigoDevolucion As String, ByVal strCodigoRollo As String)

        '            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        '            Dim parametros As Object() = {"strCodigoDevolucion", strCodigoDevolucion, _
        '                                            "strCodigoRollo", strCodigoRollo}

        '            adSQL.EjecutarComando("UP_EliminarMaestroDefecto", parametros)

        '        End Sub


        '        '------------------------------------------------------------------------------------------------

        '        Public Sub Dispose() Implements System.IDisposable.Dispose

        '        End Sub
        '#End Region

    End Class

End Namespace