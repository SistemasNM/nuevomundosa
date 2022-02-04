Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class Caracteristica
        Implements IDisposable

#Region " Declaración de Variables Miembro "

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
        Sub New()
            'm_strCodigoDefecto = String.Empty
            'm_strDescripcionDefecto = String.Empty
            'm_strUsuarioCreacion = String.Empty
            ''m_dteFechaCreacion = Now
            ''m_strUsuarioModificacion = String.Empty
            ''m_dteFechaModificacion = Now

            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
        End Sub

#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function ObtenerCarateristicas() As DataTable

            Dim dtCarateristicas As DataTable

            Try

                dtCarateristicas = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerCaracteristicas")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtCarateristicas

        End Function

        Public Function ObtenerParametroEstandar(ByVal codigoCaracteristica As String) As Double

            Dim parametroEstandar As Double
            Dim parametros As Object() = {"codigoCaracteristica", codigoCaracteristica}

            Try

                parametroEstandar = CDbl(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerParamStdCaracteristica", parametros))
            Catch ex As Exception
                Throw ex
            End Try

            Return parametroEstandar

        End Function
        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region


    End Class

End Namespace
