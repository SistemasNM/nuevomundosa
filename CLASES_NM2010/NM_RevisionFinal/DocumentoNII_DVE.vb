Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class DocumentoNII_DVE
        Implements IDisposable


        Private m_sqlDtAccOfiPlan As AccesoDatosSQLServer
        Private m_sqlDtAccRevFin As AccesoDatosSQLServer

        Sub New()
            m_sqlDtAccOfiPlan = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            m_sqlDtAccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Public Function ObtenerDocumentos(ByVal strCodigoDoc As String, ByRef strUsuario As String) As DataTable
            Try
                Dim objParametros() As Object = {"codigo_doc", strCodigoDoc, _
                                                 "usuario", strUsuario}
                Return m_sqlDtAccOfiPlan.ObtenerDataTable("ObtenerDocumentosPorCodigo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccOfiPlan.Dispose()
            m_sqlDtAccRevFin.Dispose()
        End Sub

    End Class
End Namespace
