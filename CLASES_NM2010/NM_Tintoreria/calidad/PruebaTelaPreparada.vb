Option Strict On

Imports NM.AccesoDatos
Namespace NM.Tintoreria
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
        ByVal secuencia_operacion As String) As String

            Dim parametros As Object() = {"codigo_ficha", codigo_ficha, _
                                          "hifrofilidad", hifrofilidad, _
                                          "goma", goma, _
                                          "ph", ph, _
                                          "diferencia_oco", diferencia_oco, _
                                          "grado_blanco", grado_blanco, _
                                          "columna_agua", columna_agua, _
                                          "usuario", usuario, "aprobacion", aprobacion, _
                                          "codigo_maquina_laboratorio", codigo_maquina_laboratorio, _
                                          "codigo_maquina_procedencia", codigo_maquina_procedencia, _
                                          "secuencia_laboratorio", secuencia_laboratorio, _
                                          "secuencia_operacion", secuencia_operacion}


            Try
                Dim strCadena As String
                strCadena = CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("spINS_insertarPruebaTelaPrepaprada_V2", parametros), String)

                If strCadena <> "" Then
                    Return strCadena
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ObtenerPruebaStandares(ByVal codigo_Articulo_largo As String) As DataTable
            Dim dtPrueba As DataTable
            Dim parametros() As Object = {"codigo_articulo_largo", codigo_Articulo_largo}
            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_TraerValorEstandarTelaPreparada", parametros)

            Catch ex As Exception
                Throw ex
            End Try
            Return dtPrueba
        End Function

        Public Function ObtenerPruebasFicha(ByVal numeroFicha As String) As DataTable

            Dim parametros() As Object = {"codigo_ficha", numeroFicha}

            Try
                Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("spSEL_ObtenerPruebaTelaPreparadaXFicha", parametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ObtenerPruebasFichaPersonalizada(ByVal numeroFicha As String) As DataTable

            Dim parametros() As Object = {"codigo_ficha", numeroFicha}

            Try
                Return m_sqlDtAccCalidadTintoreria.ObtenerDataTable("usp_query_ListarDatosTelaPreparada", parametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
End Namespace
