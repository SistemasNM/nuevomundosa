Option Strict On
Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso


    Public Class RegistroBlanco


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

#Region "Registrar color blanco"


    Public Function Registrar(ByVal codigo_ficha As String, _
                                  ByVal codigo_registro As Integer, _
                                  ByVal valor_Ph As Decimal, _
                                  ByVal flg_GradoAmarillo As String, _
                                  ByVal valor_GB As Decimal, _
                                  ByVal observacion As String, _
                                  ByVal estado_aprobacion As String, _
                                  ByVal flg_validar As String, _
                                  ByVal usuario_creacion As String) As String


      Dim parametros As Object() = {"codigo_ficha", codigo_ficha, _
                                    "codigo_registro", codigo_registro, _
                                    "valor_Ph", valor_Ph, _
                                    "flg_GradoAmarillo", flg_GradoAmarillo, _
                                    "valor_GB", valor_GB, _
                                    "observacion", observacion, _
                                    "estado_aprobacion", estado_aprobacion, _
                                    "flg_validar", flg_validar, _
                                    "usuario_creacion", usuario_creacion}
      Try
        Dim strCadena As String
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("USP_NM_RegBlanco_Act", parametros), String)

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
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("USP_NM_RegBlanco_Anular", parametros), String)

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



    Public Function ObtenerRegBlanco(ByVal codigo_ficha As String) As DataTable
      Dim parametros() As Object = {"codigo_ficha", codigo_ficha}
      Try
        Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("USP_NM_RegBlanco_Sel", parametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function



#End Region

    End Class
End Namespace
