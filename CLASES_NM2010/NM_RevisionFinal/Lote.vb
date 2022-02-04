Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Lote
        Implements IDisposable

        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function ObtenerLotes() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try
                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("obtenerLotes")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
        End Sub
    End Class
End Namespace