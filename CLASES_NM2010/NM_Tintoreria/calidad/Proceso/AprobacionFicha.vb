Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class AprobacionFicha
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strUsuario As String
        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
        Private m_sqlDtAccPrensa As AccesoDatosSQLServer
        Private m_sqlDtAccTintoreria As AccesoDatosSQLServer
#End Region

#Region " Definción de constructores "
        Sub New()
            m_strUsuario = String.Empty
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
            m_sqlDtAccPrensa = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_sqlDtAccTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        End Sub

#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function Insertar(ByVal codigoFicha As String, _
                            ByVal codigoArticulo As String, _
                            ByVal codigoArticuloLargo As String, _
                            ByVal codigoOrden As String, _
                            ByVal estado As String, _
                            ByVal strUsuario As String) As String


            Dim parametros As Object() = {"codigoFicha", codigoFicha, _
                                        "codigo_articulo_corto", codigoArticulo, _
                                        "codigo_articulo", codigoArticuloLargo, _
                                        "codigo_orden", codigoOrden, _
                                        "estado", estado, _
                                        "usuario", strUsuario}

            Try
                Return CType(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_InsertarAprobacionFicha_2", parametros), String)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Function ValidaEstandares(ByVal strCodigoFicha As String, ByVal strCodigoArticulo As String) As Integer
            Try
                Dim objParametros As Object() = {"CODIGO_FICHA", strCodigoFicha, "CODIGO_ARTICULO_OFISIS", strCodigoArticulo}
                Return CType(m_sqlDtAccTintoreria.ObtenerValor("SP_VALIDA_APROBACION_FICHAS", objParametros), Integer)
            Catch ex As Exception

            End Try

        End Function
        Public Function ObtenerDatosPorCodigo(ByVal strCodigo As String) As DataTable
            Try
                Dim objParametros As Object() = {"codigo_ficha", strCodigo}

                Return m_sqlDtAccPrensa.ObtenerDataTable("UP_ObtenerDatosFichaPorCodigo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub

#End Region


    End Class

End Namespace


