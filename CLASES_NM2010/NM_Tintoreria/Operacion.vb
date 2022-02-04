Option Strict Off

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Operacion
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
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Operacion_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Listar(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"var_CodigoOperacion", strCodigo, "var_NombreOperacion", strNombre}
            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_Operaciones_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Exist(ByVal pCodigoOperacion As String) As Boolean
            Dim dtblDatos As DataTable

            Try
                Dim objParametros As Object() = {"codigo_operacion", pCodigoOperacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Operacion_SelectId", objParametros)
                Return dtblDatos.Rows.Count > 0

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function Descripcion(ByVal pstrOperacion As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_operacion", pstrOperacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Operacion_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("descripcion_operacion").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function Maquina(ByVal pstrOperacion As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_operacion", pstrOperacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Operacion_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("codiqo_maquina").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function

        Public Function Agregar(ByVal pOperacion As String, ByVal pDescOperacion As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_operacion", pOperacion, "descripcion_operacion", pDescOperacion, "usuario_Creacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_Insert", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function


        Public Function Modificar(ByVal pOperacion As String, ByVal pDescOperacion As String, ByVal pUsuario As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_operacion", pOperacion, "descripcion_operacion", pDescOperacion, "usuario_modificacion", pUsuario}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_Update", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function Eliminar(ByVal pOperacion As String) As Boolean
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_operacion", pOperacion}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_Delete", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try

            Return bResultado

        End Function

        Public Function EliminarMaquinaRevision(ByVal pCodigoMaquina As String, ByVal pNumeroRevision As Integer)
            Dim bResultado As Boolean = False

            Try
                Dim objParametros As Object() = {"codigo_maquina", pCodigoMaquina, "numero_revision", pNumeroRevision}

                m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Operacion_DeleteMaquinaRevision", objParametros)
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