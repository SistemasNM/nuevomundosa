Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Proceso
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
            Dim pdtProcesos As DataTable

            Try
                pdtProcesos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Proceso_OP_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return pdtProcesos
        End Function

        Public Function Descripcion(ByVal pProceso As String) As String
            Dim pResultado As String

            Dim objParametros() As Object = {"codigo_proceso", pProceso}
            Try

                pResultado = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Proceso_OP_SelectId", objParametros).Rows(0)("descripcion_proceso").ToString
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