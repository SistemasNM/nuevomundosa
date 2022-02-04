Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Personal
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
                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_Personal")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function

        Public Function Descripcion(ByVal pOperario As String) As String
            Dim dtblDatos As DataTable
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"codigo_operario", pOperario}

                dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_PersonalId", objParametros)
                If Not dtblDatos.Rows.Count.Equals(0) Then
                    strResultado = dtblDatos.Rows(0)("nombres_personal").ToString
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