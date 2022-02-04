Option Strict On

Imports NM.AccesoDatos

Namespace NM_Tintoreria.Proceso

    Public Class PruebaNumeroHilosPasadasPulg
        Inherits PruebaBase
        Implements IDisposable


#Region " Declaración de Variables Miembro "

        Private m_strUsuario As String

        Private m_sqlDtAccCalidadTintoreria As AccesoDatosSQLServer
#End Region


#Region " Definción de constructores "
        Sub New()
            m_strUsuario = String.Empty
            m_sqlDtAccCalidadTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
        End Sub

#End Region


#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------

        Public Function ObtenerCodigo() As String

            Dim codigoPrueba As String

            codigoPrueba = CStr(m_sqlDtAccCalidadTintoreria.ObtenerValor("UP_ObtenerCodigoPruebaNumeroHilos"))

            Return codigoPrueba

        End Function

        Public Sub Insertar(ByVal numeroPrueba As String, _
                            ByVal fechaPrueba As Date, _
                            ByVal codigoFicha As String, _
                            ByVal hilosPulg As Integer, _
                            ByVal pasadasPulg As Integer, _
                            ByVal usuario As String)

            Dim parametros As Object() = {"numeroPrueba", numeroPrueba, _
                                          "fechaPrueba", fechaPrueba, _
                                          "codigoFicha", codigoFicha, _
                                          "hilosPulgada", hilosPulg, _
                                          "pasadasPulgada", pasadasPulg, _
                                          "usuario", m_strUsuario}

            Try
                m_sqlDtAccCalidadTintoreria.EjecutarComando("UP_InsertarPruebaNumeroHilosPasadasPulg", parametros)

            Catch ex As Exception

            End Try

        End Sub

        Function ObtenerPrueba(ByVal numeroPrueba As Integer) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroPrueba", numeroPrueba}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaNumeroHilos", parametros)

            Catch ex As Exception

            End Try

            Return dtPrueba

        End Function

        Public Overrides Function ObtenerUltimaPrueba(ByVal numeroFicha As String) As DataTable

            Dim dtPrueba As DataTable

            Dim parametros() As Object = {"numeroFicha", numeroFicha}

            Try
                dtPrueba = m_sqlDtAccCalidadTintoreria.ObtenerDataTable("UP_ObtenerPruebaNumeroHilosPorFicha", parametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtPrueba

        End Function

        '------------------------------------------------------------------------------------------------

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccCalidadTintoreria.Dispose()
        End Sub

#End Region

    End Class

End Namespace