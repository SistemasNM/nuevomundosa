Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
    Public Class Maquina
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


    Public Function Listar(ByVal strCodigo As String, ByVal strNombre As String, Optional ByVal booParaLotes As Boolean = False) As DataTable
      Try
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"var_CodigoMaquina", strCodigo, "var_NombreMaquina", strNombre, "sin_Flag", IIf(booParaLotes, 1, 0)}
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_TIN_Maquinas_Obtener", objParametros)
        Return dtblDatos
      Catch ex As Exception
        Throw ex
      End Try
    End Function

    Public Function Listar() As DataTable
      Dim dtblDatos As DataTable

      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_Maquina")
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function


    Public Function ListarTipoMaquina(ByVal pCodigo As String, ByVal pDescri As String) As DataTable
      Try
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"pCodigo", pCodigo, "pDescri", pDescri}
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_ListarTipoMaquina", objParametros)
        Return dtblDatos
      Catch ex As Exception
        Throw ex
      End Try

    End Function

    Public Function Descripcion(ByVal pMaquina As String) As String
      Dim dtblDatos As DataTable
      Dim strResultado As String

      Try
        Dim objParametros As Object() = {"codigo_maquina", pMaquina}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_DatosProduccion_MaquinaId", objParametros)
        If Not dtblDatos.Rows.Count.Equals(0) Then
          strResultado = dtblDatos.Rows(0)("descripcion_maquina").ToString
        Else
          strResultado = String.Empty
        End If

      Catch ex As Exception
        Throw ex
      End Try

      Return strResultado
    End Function

    Public Function DescripcionTinto(ByVal pMaquina As String) As String
      Dim dtblDatos As DataTable
      Dim strResultado As String

      Try
        Dim objParametros As Object() = {"codigo_maquina", pMaquina}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_MaquinaTintoreria_SelectId", objParametros)
        If Not dtblDatos.Rows.Count.Equals(0) Then
          strResultado = dtblDatos.Rows(0)("descripcion_maquina").ToString
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

#Region "Henry"
		Public Function ObtenerMaquinaPorCodigo(ByVal pCodigo_Maquina As String) As DataTable
			Try
				Dim objParametros() As Object = {"codigo_maquina", pCodigo_Maquina}
				Return m_sqlDtAccTintoreria.ObtenerDataTable("SP_NM_ObtenerMaquinaPorCodigo", objParametros)
			Catch ex As Exception
				Throw ex
			End Try
		End Function

#End Region

#Region "Metodos de arturo"
		Public Function ListarMaquina() As DataTable
			Try
				Return m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ListarMaquinaTintoreria")
			Catch ex As Exception
				Throw ex
			End Try
		End Function
#End Region

	End Class
End Namespace