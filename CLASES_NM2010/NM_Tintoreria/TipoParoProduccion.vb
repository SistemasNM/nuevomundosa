Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class TipoParoProduccion
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_TipoParoProduccion_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Descripcion(ByVal pCodigo As String) As String
            Dim strDescripcion As String

            Dim objParametros() As Object = {"codigo_tipoparoproduccion", pCodigo}

            Try

                strDescripcion = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_TipoParoProduccion_SelectID", objParametros).Rows(0)("descripcion_tipoparoproduccion").ToString
            Catch ex As Exception
                strDescripcion = String.Empty
            End Try

            Return strDescripcion
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    End Class
End Namespace