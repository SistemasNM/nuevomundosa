Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Maquina
        Implements IDisposable

        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function Listar() As DataTable
            Dim dtblDatosBusqueda As DataTable

            Try

                dtblDatosBusqueda = m_sqlDtAccOfiPlan.ObtenerDataTable("ListarMaquinas")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatosBusqueda
        End Function

        Public Function Descripcion(ByVal pMaquina As String) As String
            Dim pDescripcion As String

            Dim objParametros() As Object = {"codigo_maquina", pMaquina}
            Try

                pDescripcion = m_sqlDtAccOfiPlan.ObtenerDataTable("ListarMaquinasPorCodigo", objParametros).Rows(0)("descripcion_maquina").ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pDescripcion
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
        End Sub
    End Class
End Namespace