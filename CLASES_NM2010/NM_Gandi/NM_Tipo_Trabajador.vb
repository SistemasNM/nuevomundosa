Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi
    Public Class NM_Tipo_Trabajador
        Implements IDisposable

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccGandi As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccGandi = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Gandi)
        End Sub
#End Region

#Region " Definicion de Metodos "
        Public Function Listar(ByVal pPlanta As String) As DataTable
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"planta", pPlanta}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Tipo_Trabajador", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtblDatos
        End Function
#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub

    End Class
End Namespace
