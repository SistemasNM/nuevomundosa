Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Combinacion
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

        Public Function ListarOFILOGI(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo.Trim}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_CombinacionOFILOGI_Select", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function ListarOFILOGI_real(ByVal pArticulo As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"codigo_articulo", pArticulo.Trim}

            Try
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_CombinacionOFILOGI_Select_real", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Descripcion(ByVal pCombinacion As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_color", pCombinacion}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Combinacion_SelectId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("descripcion_color").ToString
                Else
                    strResultado = String.Empty
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return strResultado
        End Function


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccTintoreria.Dispose()
        End Sub
#End Region

    End Class
End Namespace