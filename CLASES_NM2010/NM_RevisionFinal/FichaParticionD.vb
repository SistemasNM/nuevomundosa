Option Strict On

Imports NM.AccesoDatos
Imports NuevoMundo.Tintoreria.NM.Tintoreria

Namespace NM.RevisionFinal
    Public Class FichaParticionD
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoFichaPartida As String
        Private m_strCodigoFicha As String
        Private m_strMetrosFinales As Double
        Private m_strUsuario As String
        'Private m_dteFechaCreacion As Date
        'Private m_strUsuarioModificacion As String
        'Private m_dteFechaModificacion As Date
#End Region

#Region " Definción de constructores "
        Sub New()
            m_strCodigoFichaPartida = String.Empty
            m_strCodigoFicha = String.Empty
            m_strMetrosFinales = 0
            m_strUsuario = String.Empty
            'm_dteFechaCreacion = Now
            'm_strUsuarioModificacion = String.Empty
            'm_dteFechaModificacion = Now
        End Sub

        Sub New(ByVal strCodigoFichaPartida As String, ByVal strCodigoFicha As String, ByVal strMetrosFinales As Integer)
            m_strCodigoFichaPartida = strCodigoFichaPartida
            m_strCodigoFicha = strCodigoFicha
            m_strMetrosFinales = strMetrosFinales

            m_strUsuario = String.Empty

            'm_dteFechaCreacion = Now
            'm_strUsuarioModificacion = String.Empty
            'm_dteFechaModificacion = Now
        End Sub
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerFichasPartidas(ByVal codigoFicha As String) As DataTable


        End Function

        Public Function ObtenerFicha(ByVal codigoFichaPartida As String) As DataSet
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoFichaPartida", m_strCodigoFichaPartida}

            Return adSQL.ObtenerDataSet("UP_ObtenerFicha", parametros)

        End Function

        Public Sub Insertar(ByVal dtblFichasPartidas As DataTable)
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            'Dim objXml As New generaXml
            Dim objXml As New NM_General.Util

            Dim parametros As Object() = {"xml_data", objXml.GeneraXml(dtblFichasPartidas)}
            adSQL.EjecutarComando("UP_InsertarFichaParticionD", parametros)
            adSQL.Dispose()
        End Sub

        Public Sub Actualizar()

        End Sub

        Public Sub Eliminar(ByVal strCodigoFichaPartida As String)

        End Sub

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region

    End Class


End Namespace