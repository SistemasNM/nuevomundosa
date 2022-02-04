Option Strict On
Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

  Public Class PruebaAcabar

#Region " Declaración de Variables Miembro "

    Private m_strUsuario As String
    Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
    Private m_sqlDtAccTintoreria As AccesoDatosSQLServer

#End Region

#Region " Definción de constructores "
    Sub New()
      m_strUsuario = String.Empty
      m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
      m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
    End Sub

#End Region

#Region "Registrar datos de Aprobacion"

    Public Function Registrar(ByVal codigo_ficha As String, _
                              ByVal codigo_registro As String, _
                              ByVal secuencia_operacion As String, _
                              ByVal secuencia_laboratorio As String, _
                              ByVal cod_maq_procedencia As String, _
                              ByVal cod_maq_laboratorio As String, _
                              ByVal flg_apariencia As String, _
                              ByVal flg_centro_orillo As String, _
                              ByVal flg_degrade As String, _
                              ByVal flg_puntuacion As String, _
                              ByVal flg_pigmento As String, _
                              ByVal estado_aprobacion As String, _
                              ByVal observacion As String, _
                              ByVal usuario As String) As String


      Dim parametros As Object() = {"codigo_ficha", codigo_ficha, _
                                    "codigo_registro", codigo_registro, _
                                    "secuencia_operacion", secuencia_operacion, _
                                    "secuencia_laboratorio", secuencia_laboratorio, _
                                    "cod_maq_procedencia", cod_maq_procedencia, _
                                    "cod_maq_laboratorio", cod_maq_laboratorio, _
                                    "flg_apariencia", flg_apariencia, _
                                    "flg_centro_orillo", flg_centro_orillo, _
                                    "flg_degrade", flg_degrade, _
                                    "flg_puntuacion", flg_puntuacion, _
                                    "flg_pigmento", flg_pigmento, _
                                    "estado_aprobacion", estado_aprobacion, _
                                    "observacion", observacion, _
                                    "usuario", usuario}

      Try
        Dim strCadena As String
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("USP_NM_AprobacionAcabar_Act", parametros), String)

        If strCadena <> "" Then
          Return strCadena
        Else
          Return ""
        End If

      Catch ex As Exception
        Throw ex
      End Try

    End Function

    Public Function AnularReg(ByVal codigo_ficha As String, _
                              ByVal codigo_registro As Integer, _
                              ByVal usuario_creacion As String) As String


      Dim parametros As Object() = {"codigo_ficha", codigo_ficha, _
                                    "codigo_registro", codigo_registro, _
                                    "usuario_creacion", usuario_creacion}
      Try
        Dim strCadena As String
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("USP_NM_AprobacionAcabar_Anular", parametros), String)

        If strCadena <> "" Then
          Return strCadena
        Else
          Return ""
        End If

      Catch ex As Exception
        Throw ex
      End Try

    End Function

    Public Function ObtenerDatosPrueba(ByVal codigo_ficha As String) As DataTable
      Dim parametros() As Object = {"codigo_ficha", codigo_ficha}
      Try
        Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("USP_NM_AprobacionAcabar_Sel", parametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function


#End Region

  End Class

End Namespace
