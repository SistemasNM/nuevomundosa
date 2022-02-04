Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Variacion
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

	Public Function ListarVariacionTelas() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Variacion_Select_Telas")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Listar() As DataTable
            Dim dtblDatos As DataTable

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Variacion_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Agregar(ByVal pArticulo As String, ByVal pVariacion As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_tela", pArticulo, "variacion", pVariacion, "usuario_creacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Variacion_Insert", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Modificar(ByVal pArticulo As String, ByVal pVariacion As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_tela", pArticulo, "variacion", pVariacion, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Variacion_Update", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Eliminar(ByVal pArticulo As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_tela", pArticulo}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Variacion_Delete", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    End Class
End Namespace