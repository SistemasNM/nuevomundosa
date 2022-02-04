Option Strict On

Imports NM.AccesoDatos

Public Class Defecto
    Implements IDisposable

    Private m_sqlDtAccRevicionFinal As AccesoDatosSQLServer

    Sub New()
        m_sqlDtAccRevicionFinal = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
    End Sub

    Public Function ObtenerDatosBusqueda(ByVal pCodigoArticulo As String) As DataTable
        Dim objParametros() As Object = {"p_vch_CodigoArticulo", pCodigoArticulo}
        Dim dtblDatosBusqueda As DataTable
        Try
            dtblDatosBusqueda = m_sqlDtAccRevicionFinal.ObtenerDataTable("USP_RVF_DEFECTOSARTICULOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatosBusqueda
    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlDtAccRevicionFinal.Dispose()
    End Sub
End Class