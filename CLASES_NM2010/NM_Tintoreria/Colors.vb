Option Strict On

Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria
  Public Class Colors
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
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_Color_Select")
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Listar_TipoConsulta(ByVal pintTipoConsulta As Int16, ByVal pstrCodigo As String, ByVal pstrDescripcion As String) As DataTable
      Dim dtblDatos As DataTable
      Dim lobjParametros() As Object = { _
      "ptin_tipoconsulta", pintTipoConsulta, _
      "pvch_codigocolor", pstrCodigo, _
      "pvch_descripcioncolor", pstrDescripcion _
      }
      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("usp_tin_color_listar", lobjParametros)
      Catch ex As Exception
        Throw ex
      End Try
      Return dtblDatos
    End Function

    Public Function ListarOFILOGI(ByVal pArticulo As String) As DataTable
      Dim dtblDatos As DataTable

      Dim objParametros() As Object = {"codigo_articulo", pArticulo}

      Try
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ColorOFILOGI_Select", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function Agregar(ByVal pColor As String, ByVal pDescColor As String, ByVal pCobertura As String, ByVal pUsuario As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_color", pColor, "descripcion_color", pDescColor, "cobertura", pCobertura, "usuario_creacion", pUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Color_Insert", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function Modificar(ByVal pColor As String, ByVal pDescColor As String, ByVal pCobertura As String, ByVal pUsuario As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_color", pColor, "descripcion_color", pDescColor, "cobertura", pCobertura, "usuario_modificacion", pUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Color_Update", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function Eliminar(ByVal pColor As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_color", pColor}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_Color_Delete", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function Descripcion(ByVal pColor As String) As String
      Dim dtblDatos As DataTable
      Dim strResultado As String

      Try
        Dim objParametros As Object() = {"codigo_color", pColor}

        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_ColorOFILOGI_Obtener", objParametros)
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