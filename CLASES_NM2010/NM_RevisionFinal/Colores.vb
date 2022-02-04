Option Strict On

Imports System.Xml
Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class Colores
        Implements IDisposable

        Private m_accDtsSQLServer As AccesoDatosSQLServer
        Private m_dtaccRevFin As AccesoDatosSQLServer
        Private m_dtaccOfiLogi As AccesoDatosSQLServer
        Private m_dtaccNmProd4 As AccesoDatosSQLServer


        Sub New()
            m_accDtsSQLServer = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_dtaccRevFin = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            m_dtaccOfiLogi = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

        End Sub

        Public Function ObtenerColor(ByVal strCodigo As String, ByVal strCodigoColor As String, ByVal strDescripcionColor As String) As DataTable
            Dim dtblColor As DataTable
            Try
                Dim objParametros As Object() = {"codigocolor", strCodigoColor, "desColor", strDescripcionColor}
                dtblColor = m_accDtsSQLServer.ObtenerDataTable("usp_NM_Mostrar_Colores1", objParametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtblColor
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace

