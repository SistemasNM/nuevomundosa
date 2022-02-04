Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class TipoTenido
        Implements IDisposable


#Region " Declaración de Variables Miembro "

        Private m_strUsuario As String

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
#End Region


#Region " Definción de constructores "
        Sub New()
            m_strUsuario = String.Empty
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
        End Sub

#End Region


#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function ObtenerTiposTenido() As DataTable

            Dim dtTiposTenido As DataTable

            dtTiposTenido = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerTiposTenido")

            Return dtTiposTenido

        End Function

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccCalidadTintoreria.Dispose()
        End Sub

#End Region

    End Class

End Namespace