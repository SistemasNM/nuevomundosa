Imports NM.AccesoDatos

Public Class IngresoHilos
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private m_sqlHilanderia As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        m_sqlHilanderia = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
    End Sub
#End Region

#Region " Definicion de Metodos "


    Public Function BuscarParihuelaTransito(ByVal strCodigoParihuela As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_CodigoParihuela", strCodigoParihuela}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_INGRESO_BUSCAR_PARIHUELA_TRANSITO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function ObtenerListaUbicacionesHilanderia(Optional ByVal strCodigoAlmacen As String = "007") As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pvch_CodigoAlmacen", strCodigoAlmacen}
        Try

            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_INGRESO_OBTENER_UBICACIONES_HILANDERIA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Registrar_IngresoParihuela_Stock(ByVal intIDParihuela As Integer,
                                                     ByVal strTipoPesaje As String,
                                                     ByVal strCodigoUbicacion As String,
                                                     ByVal strCodigoUsuario As String) As Integer

        Dim strResult As Integer

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pvch_TipoPesaje", strTipoPesaje,
                                         "pvch_CodigoUbicacion", strCodigoUbicacion,
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_REGISTRA_INGRESO_PARIHUELA_STOCK", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function RegistrarRecojoParihuelaHilo(ByVal intIDParihuela As Integer,
                                                 ByVal strTipoPesaje As String,                                                 
                                                 ByVal strCodigoUsuario As String) As Integer

        Dim strResult As Integer

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pvch_TipoPesaje", strTipoPesaje,                                         
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_REGISTRA_RECOJO_PARIHUELA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function RegistrarStockUbicacionParihuelaHilo(ByVal intIDParihuela As Integer,
                                                         ByVal strCodigoUbicacion As String,
                                                         ByVal strCodigoUsuario As String) As Integer

        Dim strResult As Integer

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pvch_CodigoUbicacion", strCodigoUbicacion,
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_REGISTRA_STOCK_UBICACION_PARIHUELA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function



    Public Function BuscarUbicacionesDisponibles(ByVal strCodigoParihuela As String) As DataSet
        Dim dtblDatos As DataSet

        Dim objParametros() As Object = {"pvch_CodigoParihuela", strCodigoParihuela}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataSet("USP_HIL_BUSCAR_UBICACION_DISPONIBLE_PARIHUELA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function

    Public Function BuscarReUbicacionesDisponibles(ByVal strCodigoParihuela As String) As DataSet
        Dim dtblDatos As DataSet

        Dim objParametros() As Object = {"pvch_CodigoParihuela", strCodigoParihuela}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataSet("USP_HIL_BUSCAR_REUBICACION_DISPONIBLE_PARIHUELA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function

    Public Function ProcesarReUbicacionParihuelaHilo(ByVal intIDParihuela As Integer,
                                                     ByVal strCodigoUbicacion As String,
                                                     ByVal strCodigoUsuario As String) As Integer

        Dim strResult As Integer

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pvch_CodigoUbicacion", strCodigoUbicacion,
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_BALANZA_PROCESAR_REUBICACION_PARIHUELA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function BuscarParihuelaStock(ByVal strCodigoParihuela As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_CodigoParihuela", strCodigoParihuela}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_INGRESO_BUSCAR_PARIHUELA_STOCK", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Registrar_Division_Parihuela(ByVal intIDParihuela As Integer, ByVal intConosParihuela_Hija As Integer, ByVal dblKilosParihuela_Hija As Double, ByVal strCodigoUsuario As String) As String

        Dim strResult As String

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pint_ConosParihuelaHija", intConosParihuela_Hija,
                                         "pdbl_KilosParihuelaHija", dblKilosParihuela_Hija,
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.ObtenerValor("USP_HIL_REGISTRA_DIVISION_PARIHUELA_UBICACION", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return strResult

    End Function

    Public Function BuscarParihuela_PreDespacho(ByVal strCodigoParihuela As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_CodigoParihuela", strCodigoParihuela}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_INGRESO_BUSCAR_PARIHUELA_PREDESPACHO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function AnularParihuela_PreDespacho(ByVal intCodigoParihuela As Integer, ByVal strCodUsuario As String) As Integer
        Dim intResultado As Integer

        Dim objParametros() As Object = {"pint_CodigoParihuela", intCodigoParihuela,
                                         "pvch_CodigoUsuario", strCodUsuario}
        Try
            intResultado = m_sqlHilanderia.EjecutarComando("USP_HIL_ANULAR_PARIHUELA_PREDESPACHO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return intResultado

    End Function

    Public Function fnc_ObtenerListaMotivosSalida() As DataTable
        Dim dtblDatos As DataTable

        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_OBTENER_MOTIVOS_SALIDAS_PARIHUELA")
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function GenerarDocumentoSalidaHilos_NSA(ByVal intIDParihuela As Integer, ByVal strMotivoSalida As String, ByVal strDestinoHilo As String, ByVal strCodigoUsuario As String) As String

        Dim strResult As String

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pvch_CodigoMotivo", strMotivoSalida,
                                         "pvch_CodigoDestino", strDestinoHilo,
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.ObtenerValor("USP_HIL_GENERA_DOCUMENTO_SALIDA_HILOS_NSA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return strResult

    End Function

    Public Function Obtener_Lista_Titulos_Hilos() As DataTable
        Dim dtblDatos As DataTable

        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_RECLASIFICACION_BUSCAR_TITULO_HILOS")
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function Obtener_Lista_Descripcion_Hilos(ByVal strTitulo As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"pvch_Titulo", strTitulo}

        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_RECLASIFICACION_BUSCAR_DESCRIPCION_HILOS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Registrar_Reclasificacion_Parihuela(ByVal intIDParihuela As Integer, ByVal intConosParihuela_Hija As Integer, ByVal strCodigoArticulo As String, ByVal strCodigoUsuario As String) As String

        Dim strResult As String

        Dim objParametros() As Object = {"pint_IDParihuela", intIDParihuela,
                                         "pint_ConosParihuela", intConosParihuela_Hija,
                                         "pvch_CodigoArticulo", strCodigoArticulo,
                                         "pvch_CodigoUsuario", strCodigoUsuario}

        Try
            strResult = m_sqlHilanderia.ObtenerValor("USP_HIL_REGISTRA_RECLASIFICACION_HILO_PARIHUELA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return strResult

    End Function


    Public Function BuscarParihuelaDespachada(ByVal strCodigoParihuela As String) As DataSet
        Dim dsDatos As DataSet

        Dim objParametros() As Object = {"pstr_CodigoParihuela", strCodigoParihuela}
        Try
            dsDatos = m_sqlHilanderia.ObtenerDataSet("USP_HIL_BUSCAR_DATOS_PARIHUELA_DESPACHADA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dsDatos

    End Function

    Public Function BuscarDatosParihuelaStock(ByVal strCodigoParihuela As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_CodigoParihuela", strCodigoParihuela}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_BUSCAR_DATOS_PARIHUELA_STOCK", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

#End Region

    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlHilanderia.Dispose()
    End Sub
End Class
