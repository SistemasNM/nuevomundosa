Option Strict On
Imports NM.AccesoDatos
Namespace NM_Tintoreria.Proceso
    Public Class PruebaTelaPreparada

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

#Region "Transacciones de la Tela Preperada"
    Public Function Insertar_V2(ByVal codigo_ficha As String, _
                     ByVal hifrofilidad As String, _
                     ByVal goma As String, _
                     ByVal ph As String, _
                     ByVal diferencia_oco As String, _
                     ByVal grado_blanco As String, _
                     ByVal columna_agua As String, _
                     ByVal usuario As String, ByVal aprobacion As String, _
                     ByVal codigo_maquina_laboratorio As String, _
                     ByVal codigo_maquina_procedencia As String, _
                     ByVal secuencia_laboratorio As String, _
                     ByVal secuencia_operacion As String, _
                     ByVal revision_dato As Integer, _
                     ByVal pstrAccion As String, _
                     ByVal pintNumero_Prueba As Integer, _
                     ByVal Codigo_Dato As String, _
                     ByVal psObservacion As String) As String

      Dim parametros As Object() = {"codigo_ficha", codigo_ficha, _
                                    "hifrofilidad", IIf(hifrofilidad = "", DBNull.Value, hifrofilidad), _
                                    "goma", IIf(goma = "", DBNull.Value, goma), _
                                    "ph", IIf(ph = "", DBNull.Value, ph), _
                                    "grado_blanco", IIf(grado_blanco = "", DBNull.Value, grado_blanco), _
                                    "usuario", usuario, "aprobacion", aprobacion, _
                                    "codigo_maquina_laboratorio", codigo_maquina_laboratorio, _
                                    "codigo_maquina_procedencia", codigo_maquina_procedencia, _
                                    "secuencia_laboratorio", secuencia_laboratorio, _
                                    "secuencia_operacion", secuencia_operacion, _
                                    "revision_dato", revision_dato, _
                                    "Accion", pstrAccion, _
                                    "numero_prueba", pintNumero_Prueba, _
                                    "Codigo_Dato", Codigo_Dato, _
                                    "Observacion", psObservacion}
      Try
        Dim strCadena As String
        strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("usp_CAL_TelaPreparada_Proceso", parametros), String)

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

    Public Function ObtenerPruebasFicha(ByVal numeroFicha As String, ByVal NumeroPrueba As Integer) As DataTable
      Dim parametros() As Object = {"codigo_ficha", numeroFicha, "", NumeroPrueba}
      Try
        Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_ObtenerPruebaTelaPreparadaXFicha", parametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function
    Public Function ObtenerPruebasFichaPersonalizada(ByVal NumeroFicha As String, ByVal NumeroPrueba As Integer) As DataTable
      Dim parametros() As Object = {"codigo_ficha", NumeroFicha, "Numero_Prueba", NumeroPrueba}
      Try
        Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_CAL_TelaPreparada_Obtener", parametros)
      Catch ex As Exception
        Throw ex
      End Try
    End Function
#End Region

    End Class

End Namespace
