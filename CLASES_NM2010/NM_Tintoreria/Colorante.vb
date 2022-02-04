Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
  Public Class Colorante
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

    Public Function ListarOFILOGI2(ByVal pintTipoConsulta As Int16, ByVal pstrCodigo As String, ByVal pstrDescripcion As String) As DataTable
      Dim dtblDatos As DataTable
      Dim lobjParametros() As Object = { _
      "ptin_tipoconsulta", pintTipoConsulta, _
      "pvch_codigotipocol", pstrCodigo, _
      "pvch_descripciontipocol", pstrDescripcion _
      }
      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_tipocolorante_listar_tmp", lobjParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function ListarOFILOGI(ByVal pArticulo As String) As DataTable
      Dim dtblDatos As DataTable

      Dim objParametros() As Object = {"codigo_articulo", pArticulo}

      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ColoranteOFILOGI_Select", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Descripcion(ByVal pColorante As String) As String
      Dim dtblDatos As DataTable
      Dim strResultado As String

      Try
        Dim objParametros As Object() = {"codigo_colorante", pColorante}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Colorante_SelectId", objParametros)
        If Not dtblDatos.Rows.Count.Equals(0) Then
          strResultado = dtblDatos.Rows(0)("descripcion_colorante").ToString
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