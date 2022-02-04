Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class TipoOperacion
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
            Dim pdtTipoOperacion As DataTable

            Try
                pdtTipoOperacion = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_TipoOperacion_OP_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtTipoOperacion
        End Function

        Public Function ListarCaracteristicas(ByVal pArticulo As String) As DataTable
            Dim pdtCaracteristicas As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}

            Try
                pdtCaracteristicas = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_OrdenProduccion_ObtenerPedidos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtCaracteristicas
        End Function

        Public Function Descripcion(ByVal pTipoOperacion As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_tipooperacion", pTipoOperacion}
            Try

                pResultado = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_TipoOperacion_OP_SelectId", objParametros).Rows(0)("descripcion_tipooperacion").ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    End Class
End Namespace