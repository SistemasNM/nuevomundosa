Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Revisor
        Implements IDisposable

        Private m_sqlDtAccRevFin As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function ObtenerDatosBusqueda() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                dtblDatosBusqueda = m_sqlDtAccRevFin.ObtenerDataTable("ObtenerRevisoresBusqueda")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Function ObtenerDatosPorCodigo(ByVal strCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_revisor", strCodigo}

                Return m_sqlDtAccRevFin.ObtenerDataTable("UP_ObtenerRevisoresPorCodigo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccRevFin.Dispose()
        End Sub
    End Class
End Namespace