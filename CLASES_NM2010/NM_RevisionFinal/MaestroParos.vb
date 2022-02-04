Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class MaestroParos
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_intCodigoParo As Integer
        Private m_strDescripcionParo As String
        'Private m_strCodigoSeccion As String

        Private m_strUsuarioCreacion As String
        Private m_dteFechaCreacion As Date
        Private m_strUsuarioModificacion As String
        Private m_dteFechaModificacion As Date
#End Region

#Region " Definción de constructores "
        Sub New()
            m_intCodigoParo = 0
            m_strDescripcionParo = String.Empty

            m_strUsuarioCreacion = String.Empty
            m_dteFechaCreacion = Now
            m_strUsuarioModificacion = String.Empty
            m_dteFechaModificacion = Now
        End Sub

        Sub New(ByVal intCodigoParo As Integer, ByVal strDescripcionParo As String)
            m_intCodigoParo = intCodigoParo
            m_strDescripcionParo = strDescripcionParo

            m_strUsuarioCreacion = String.Empty
            m_dteFechaCreacion = Now
            m_strUsuarioModificacion = String.Empty
            m_dteFechaModificacion = Now
        End Sub

#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerParos() As DataTable

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Return adSQL.ObtenerDataTable("UP_ObtenerParos")

        End Function

        Public Sub Insertar()

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"intCodigoParo", m_intCodigoParo, _
                                            "strDescripcionParo", m_strDescripcionParo, _
                                            "strUsuario", m_strUsuarioCreacion}

            adSQL.EjecutarComando("UP_InsertarMaestroParo", parametros)

        End Sub

        Public Sub Actualizar()

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"intCodigoParo", m_intCodigoParo, _
                                            "strDescripcionParo", m_strDescripcionParo, _
                                            "strUsuario", m_strUsuarioCreacion}

            adSQL.EjecutarComando("UP_ActualizarMaestroParo", parametros)

        End Sub

        Public Sub Eliminar(ByVal intCodigoParo As Integer)

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"intCodigoParo", intCodigoParo}

            adSQL.EjecutarComando("UP_EliminarMaestroParo", parametros)

        End Sub


        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub
#End Region

    End Class
End Namespace