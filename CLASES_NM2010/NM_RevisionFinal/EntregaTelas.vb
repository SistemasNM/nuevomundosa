Option Strict On

Imports NM.AccesoDatos

Namespace NM.RevisionFinal
    Public Class EntregaTelas
        Implements IDisposable

#Region " Declaración de Variables Miembro "
        Private m_strCodigoFicha As String
        Private m_strCodigoArticulo As String
        Private m_strCodigoArticuloCorto As String
        Private m_intRevisionArticulo As Integer
        Private m_strOrdenProduccion As String

        Private m_dteFechaEntrega As Date
        Private m_strHoraEntrega As String
        Private m_intMetraje As Double

        Private m_strUsuario As String
        Private m_Clasificacion As String
        Private m_Observacion As String
		Private m_sqlDtAccProduccion As AccesoDatosSQLServer
        Private m_sqlDtAccTint As AccesoDatosSQLServer
        Private m_sqlDtAccRvf As AccesoDatosSQLServer
#End Region

        Public Property Usuario() As String
            Get
                Return m_strUsuario
            End Get
            Set(ByVal Value As String)
                m_strUsuario = Value
            End Set
        End Property

        Public Property Clasificacion() As String
            Get
                Return m_Clasificacion
            End Get
            Set(ByVal Value As String)
                m_Clasificacion = Value
            End Set
        End Property
        Public Property Observacion() As String
            Get
                Return m_Observacion
            End Get
            Set(ByVal Value As String)
                m_Observacion = Value
            End Set
        End Property

#Region " Definción de constructores "
        Sub New()
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            m_sqlDtAccTint = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            m_sqlDtAccRvf = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
        End Sub

        Sub New(ByVal strCodigoFicha As String, ByVal strCodigoArticulo As String, _
          ByVal strCodigoArticuloCorto As String, ByVal strOrdenProduccion As String, _
          ByVal dteFechaEntrega As Date, ByVal strHoraEntrega As String, ByVal intMetraje As Double)
            m_strCodigoFicha = strCodigoFicha
            m_strCodigoArticulo = strCodigoArticulo
            m_strCodigoArticuloCorto = strCodigoArticuloCorto
            m_intRevisionArticulo = 0
            m_strOrdenProduccion = strOrdenProduccion
            m_dteFechaEntrega = dteFechaEntrega
            m_strHoraEntrega = strHoraEntrega
            m_intMetraje = intMetraje

            m_strUsuario = String.Empty
        End Sub
#End Region

#Region " Definición de Métodos "
        '------------------------------------------------------------------------------------------------
        Public Overloads Function ObtenerFichasPartidas(ByVal codigoFicha As String) As DataTable


        End Function

        Public Function Insertar() As Integer

            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"strCodigoFichaPartida", m_strCodigoFicha, _
                                            "strCodigoArticulo", m_strCodigoArticulo, _
                                            "strCodigoArticuloCorto", m_strCodigoArticuloCorto, _
                                            "intRevisionArticulo", m_intRevisionArticulo, _
                                            "strOrdenProduccion", m_strOrdenProduccion, _
                                            "dteFechaEntrega", m_dteFechaEntrega, _
                                            "strHoraEntrega", m_strHoraEntrega, _
                                            "intMetraje", m_intMetraje, _
                                            "strUsuario", m_strUsuario, _
                                            "varClasificacion", m_Clasificacion, _
                                            "varObservacion", m_Observacion}

            Return adSQL.EjecutarComando("UP_InsertarEntregaTelas", parametros)
        End Function
        Public Function Modificar_Estado_Ficha(ByVal strCodigoFicha As String, ByVal strClasificacion As String, ByVal strUsuario As String, ByVal strObservaciones As String) As Integer
            Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim parametros As Object() = {"CODIGO_FICHA", strCodigoFicha, _
                                            "Var_Clasificacion", strClasificacion, _
                                            "var_Usuario", strUsuario, _
                                            "var_Observaciones", strObservaciones}

            Return adSQL.EjecutarComando("USP_RVF_CAMBIAR_ESTADO_FICHA", parametros)
        End Function

    Public Function FinalizaCorte(ByVal strCodigoFicha As String, ByVal strUsuario As String) As Integer
      Dim adSQL As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
      Dim parametros As Object() = {"codigo_ficha_partida", strCodigoFicha, _
                                      "usuario_fincorte", strUsuario}

      Return adSQL.EjecutarComando("SP_FinalizaCorte_EntregaTela", parametros)
    End Function


    Public Sub Actualizar()

    End Sub

    Public Sub Eliminar()

    End Sub

    '------------------------------------------------------------------------------------------------

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub

#End Region
#Region "Validaciones de Articulos y Fichas Padres --- Giancarlo Vidal"

        Public Function ValidaAnchoAcabado(ByVal strCodigoArticulo As String) As Integer

            Try
                Dim objParametos() As Object = {"CODIGO_ARTICULO_LARGO", strCodigoArticulo}
                Return CType(m_sqlDtAccProduccion.ObtenerValor("SP_VALIDA_SI_TIENE_ANCHO_ACABADO", objParametos), Integer)
            Catch ex As Exception

            End Try

        End Function

        Public Function ValidaTransferenciaDeFichasPadres(ByVal strCodigoFicha As String) As Integer
            Try
                Dim objParametos() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return CType(m_sqlDtAccTint.ObtenerValor("SP_VALIDA_TRANFERENCIA_FICHAS_PADRES", objParametos), Integer)
            Catch ex As Exception
            End Try
        End Function
        Public Function ValidaAprobacionFicha(ByVal strCodigoFicha As String) As Integer
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return CType(m_sqlDtAccTint.ObtenerValor("SP_VALIDA_SI_FICHA_ESTA_APROBADA", objParametros), Integer)
            Catch ex As Exception
            End Try
        End Function
        Public Function ValidaTransferenciaFicha(ByVal strCodigoFicha As String) As Integer
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha}
                Return CType(m_sqlDtAccRvf.ObtenerValor("SP_VALIDA_SI_FICHA_ESTA_TRANSFERIDA", objParametros), Integer)
            Catch ex As Exception
            End Try
        End Function
#End Region
#Region "Valida si tiene Ruta"
        Public Function ValidaRuta(ByVal strCodigoFicha As String, ByVal strCodigoArticulo As String) As DataTable
            Try
                Dim objParametros() As Object = {"CODIGO_FICHA", strCodigoFicha, "CODIGO_ARTICULO", strCodigoArticulo}
                Return m_sqlDtAccTint.ObtenerDataTable("SP_VALIDA_RUTA_EN_TRANSFERENCIA", objParametros)
            Catch ex As Exception

            End Try
        End Function
#End Region
#Region "Valida Metraje de Articulos contra la Orden"
        Public Function ValidaMetrosArticulo(ByVal var_CodigoOrden As String, ByVal var_CodigoArticuloLargo As String, ByVal num_Metraje As Double) As Integer
            Try
                Dim objParametros As Object() = {"p_var_CodigoOrden", var_CodigoOrden, _
                                                 "p_var_CodigoArticuloLargo", var_CodigoArticuloLargo, _
                                                 "p_num_Metraje", num_Metraje}

                Return CType(m_sqlDtAccTint.ObtenerValor("usp_query_ValidaArticuloEnLaOrden", objParametros), Integer)

            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class
End Namespace