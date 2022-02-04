Imports System.Data
Imports NM.AccesoDatos

Namespace NM.Tintoreria

  Public Class MaquinaOperacion

#Region " Declaracion de Variables Miembro "
    Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
      m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    End Sub
#End Region

#Region " Definicion de Metodos "

    Public Function ListarOperaciones_por_Maquina(ByVal pCodigoMaquina As String) As DataTable
      Dim dtblDatos As DataTable
      Try
        Dim objParametros As Object() = {"codigo_maquina", pCodigoMaquina}
        dtblDatos = m_sqlDtAccTintoreria.ObtenerDataTable("pr_NM_MaquinaOperacion_SelectId", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return dtblDatos
    End Function

    Public Function GetRevision(ByVal pCodigoMaquina As String) As Integer
      Dim Revision As Integer
      Try
        Dim objParametros As Object() = {"codigo_maquina", pCodigoMaquina}
        Revision = m_sqlDtAccTintoreria.ObtenerValor("pr_NM_MaquinaOperacion_GetRevision", objParametros)
      Catch ex As Exception
        Throw ex
      End Try

      Return Revision
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

    Public Function Agregar(ByVal pOperacion As String, ByVal pRevision As Integer, ByVal pDescOperacion As String, ByVal pCodigoMaquina As String, ByVal pUsuario As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"codigo_operacion", pOperacion, "revision_maquina", pRevision, "codigo_maquina", pCodigoMaquina, "usuario_creacion", pUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("pr_NM_MaquinaOperacion_Insert", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    '========================================================================================
    'Metodos para el registro de parametros de las maquinas
    '========================================================================================


    Public Function MaquinaParametro_Listar(ByVal strMaquina As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina}

        dtData = m_sqlDtAccTintoreria.ObtenerDataTable("usp_NM_MaquinaParametro_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function MaquinaParametro_Actuaizar(ByVal strMaquina As String, _
                                               ByVal strUsuario As String, _
                                               ByVal dtbData As DataTable) As Boolean
      'Dim lobjUtil As New generaXml
      Dim lobjUtil As New NM_General.Util
      Dim lblValor As Boolean = False
      Try
        dtbData.TableName = "Data"
        Dim objParametros() As Object = {"vch_CodigoMaquina", strMaquina, _
                                        "vch_Usuario", strUsuario, _
                                        "pnvc_detalle", lobjUtil.GeneraXml(dtbData)}

        m_sqlDtAccTintoreria.EjecutarComando("usp_NM_MaquinaParametro_Actualizar", objParametros)

        lblValor = True

      Catch ex As Exception
        lblValor = False
        Throw ex
      Finally
        lobjUtil = Nothing
      End Try
      Return lblValor
    End Function


    Public Function MaquinaProceso_Listar(ByVal strEmpresa As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"chr_Empresa", strEmpresa}

        dtData = m_sqlDtAccTintoreria.ObtenerDataTable("usp_NM_MaquinaProceso_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function



    Public Function RegistroArticParam_Listar(ByVal strArticulo As String, ByVal strMaq As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"vch_CodigoArticulo", strArticulo, "vch_CodigoMaquina", strMaq}

        dtData = m_sqlDtAccTintoreria.ObtenerDataTable("usp_NM_RegMaqParam_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function



    Public Function MaquinaArticulo_Listar(ByVal strArticulo As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"vch_CodigoArticulo", strArticulo}

        dtData = m_sqlDtAccTintoreria.ObtenerDataTable("usp_MaquinaArticulo_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function


    Public Function MaquinaParametro_Select(ByVal strCodigo As String, ByVal strNombre As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"vch_CodigoMaquina", strCodigo, "vch_DescriMaquina", strNombre}

        dtData = m_sqlDtAccTintoreria.ObtenerDataTable("usp_NM_MaqParamSelec_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function ArticuloMaquina_Agregar(ByVal strArticulo As String, ByVal strMaquina As String, ByVal strUsuario As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"vch_CodigoArticulo", strArticulo, "vch_CodigoMaquina", strMaquina, "vch_Usuario", strUsuario}

        m_sqlDtAccTintoreria.EjecutarComando("usp_NM_RegMaqParam_Insertar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function

    Public Function ArticuloMaquina_Eliminar(ByVal strArticulo As String, ByVal strMaquina As String) As Boolean
      Dim bResultado As Boolean = False

      Try
        Dim objParametros As Object() = {"vch_CodigoArticulo", strArticulo, "vch_CodigoMaquina", strMaquina}

        m_sqlDtAccTintoreria.EjecutarComando("usp_MaquinaArticulo_Eliminar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try

      Return bResultado

    End Function



    Public Function ArticuloParametro_Actuaizar(ByVal strArticulo As String, _
                                                ByVal strMaquina As String, _
                                                ByVal strUsuario As String, _
                                                ByVal dtbData As DataTable) As Boolean
      'Dim lobjUtil As New generaXml
      Dim lobjUtil As New NM_General.Util
      Dim lblValor As Boolean = False
      Try
        dtbData.TableName = "Data"
        Dim objParametros() As Object = {"vch_CodigoArticulo", strArticulo, _
                                         "vch_CodigoMaquina", strMaquina, _
                                        "vch_Usuario", strUsuario, _
                                        "pnvc_detalle", lobjUtil.GeneraXml(dtbData)}

        m_sqlDtAccTintoreria.EjecutarComando("usp_NM_RegMaqParam_Actualizar", objParametros)

        lblValor = True

      Catch ex As Exception
        lblValor = False
        Throw ex
      Finally
        lobjUtil = Nothing
      End Try
      Return lblValor
    End Function


#End Region

  End Class
End Namespace