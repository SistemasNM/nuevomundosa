Option Strict On
Imports System.Data
Imports NM_General
Imports NM.AccesoDatos
Namespace OFISIS.OFISEGU
    Public Class Tmusua
        Implements IDisposable
        Private m_sqlDtAccOfisegu As AccesoDatosSQLServer
        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfisegu.Dispose()
        End Sub
        Sub New()
            m_sqlDtAccOfisegu = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
        End Sub
        Public Function ObtenerDatosUsuarioxNombre(ByVal pstrCodUsu As String) As DataTable
            '*******************************************************************************************
            'Creado por:	Carlos Ponce Taype
            'Fecha     :    12-07-2016
            'Proposito :    Permite obtener datos del usuario por su nombre
            '*******************************************************************************************
            Dim dtblDatosUsuario As DataTable
            Dim objParametros() As Object = {"pco_usua", pstrCodUsu}
            Try
                dtblDatosUsuario = m_sqlDtAccOfisegu.ObtenerDataTable("USP_RVF_OBTENER_DATOS_USUARIO_X_NOMBRE", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatosUsuario
        End Function
    End Class
End Namespace

