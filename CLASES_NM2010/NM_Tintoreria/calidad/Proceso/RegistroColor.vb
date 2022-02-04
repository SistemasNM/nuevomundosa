Option Strict On
Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class RegistroColor


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

#Region "Registrar datos de color"


    Public Function Registrar(ByVal codigo_ficha As String, _
                                  ByVal codigo_registro As Integer, _
                                  ByVal valor_Dh As Decimal, _
                                  ByVal valor_DE As Decimal, _
                                  ByVal valor_CMC As Decimal, _
                                  ByVal observacion As String, _
                                  ByVal estado_aprobacion As String, _
                                  ByVal flg_validar As String, _
                                  ByVal usuario_creacion As String, _
                                  ByVal secuencia_operacion As String, _
                                  ByVal secuencia_laboratorio As String, _
                                  ByVal cod_maq_laboratorio As String) As String


      Dim parametros As Object() = {"codigo_ficha", codigo_ficha, _
                                    "codigo_registro", codigo_registro, _
                                    "valor_Dh", valor_Dh, _
                                    "valor_DE", valor_DE, _
                                    "valor_CMC", valor_CMC, _
                                    "observacion", observacion, _
                                    "estado_aprobacion", estado_aprobacion, _
                                    "flg_validar", flg_validar, _
                                    "usuario_creacion", usuario_creacion, _
                                    "secuencia_operacion", secuencia_operacion, _
                                    "secuencia_laboratorio", secuencia_laboratorio, _
                                    "cod_maq_laboratorio", cod_maq_laboratorio}

      Try
        Dim strCadena As String
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("USP_NM_RegColor_Act", parametros), String)

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
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("USP_NM_RegColor_Anular", parametros), String)

        If strCadena <> "" Then
          Return strCadena
        Else
          Return ""
        End If

      Catch ex As Exception
        Throw ex
      End Try

    End Function


    Public Function ObtenerPruebaStandares(ByVal codigo_Articulo_largo As String, ByVal Codigo_Prueba As String, ByVal Accion As String, ByVal Revision_Dato As Integer) As DataTable
      Dim dtPrueba As New DataTable
      Dim parametros() As Object = {"Codigo_Dato", codigo_Articulo_largo, "Codigo_Prueba", Codigo_Prueba, "Accion", Accion, "Revision_Dato", Revision_Dato}
      Try
        dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_Estandares_Obtener", parametros)

      Catch ex As Exception
        Throw ex
      End Try
      Return dtPrueba
    End Function


    Public Function ObtenerRegColor(ByVal codigo_ficha As String) As DataTable
      Dim parametros() As Object = {"codigo_ficha", codigo_ficha}
      Try
        Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("USP_NM_RegColor_Sel", parametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function



#End Region

  End Class

End Namespace
