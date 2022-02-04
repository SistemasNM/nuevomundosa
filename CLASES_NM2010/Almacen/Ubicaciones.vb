Imports NM.AccesoDatos
Imports NM_General
Imports System.Text

Public Class Ubicaciones
    Implements IDisposable

#Region " Declaracion de Variables Miembro "
    Private _objConnexion As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
    Sub New()
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    End Sub
#End Region

#Region " Funciones "
    Public Function BuscarUbicacionesArticulo(ByVal pstrRollo As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo}
            Return _objConnexion.ObtenerDataSet("USP_ALM_UBICACIONES_OBTENER_LISTADISPONIBLES", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidarRolloUbicaciones(ByVal pstrRollo As String) As String
        Try            
            Dim objParametros As Object() = {"pvch_CodigoRollo", pstrRollo}

            Return CType(_objConnexion.ObtenerValor("USP_ALM_UBICACIONES_VALIDAR_ROLLO", objParametros), String).Trim

        Catch ex As Exception
            Return "ERROR: " + ex.Message
        End Try

    End Function


    Public Function ObtenerDatosRollo(ByVal pstrRollo As String, ByVal pstrValida_Articulo As String,
                                      ByVal pstrValida_Color As String,
                                      ByVal pstrValida_Presentacion As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo,
                                             "pvch_Valida_Articulo", pstrValida_Articulo,
                                             "pvch_Valida_Color", pstrValida_Color,
                                             "pvch_Valida_Presentacion", pstrValida_Presentacion}

            Return _objConnexion.ObtenerDataTable("USP_ALM_UBICACIONES_OBTENER_DATOS_ROLLO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function IngresarRollosUbicacion(ByVal pstrCodUbicacion As String,
                                     ByVal pstrUsuario As String,
                                     ByVal pDTListaRollos As DataTable) As Integer
        Try
            Dim objUtil As New Util
            'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
            pDTListaRollos.TableName = "ROLLOS"
            Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(pDTListaRollos))
            ' Dim strXMLData As string = objUtil.GeneraXml(pDTListaPedido)

            Dim objParametros As Object() = {"pvch_CodUbicacion", pstrCodUbicacion, _
                                             "pvch_Usuario", pstrUsuario, _
                                             "pvch_ListaRollosXML", strListaRollosXML.ToString}

            ' Return _objConnexion.EjecutarComando("USP_ALM_UBICACIONES_VINCULAR_ROLLOS_UBICACION", objParametros)
            Return _objConnexion.EjecutarComando("USP_ALM_UBICACIONES_INGRESAR_ROLLOS_UBICACION", objParametros)


        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ObtenerDatosUbicacion(ByVal pstrCodigoUbicacion As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_CodigoUbicacion", pstrCodigoUbicacion}

            Return _objConnexion.ObtenerDataTable("USP_ALM_UBICACIONES_OBTENER_DATOS_UBICACION", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerDatosRolloUbicacion(ByVal pstrRollo As String,
                                               ByVal pstrValida_Articulo As String,
                                               ByVal pstrValida_Color As String,
                                               ByVal pstrValida_Presentacion As String,
                                               ByVal pstrValida_Ubicacion As String,
                                               ByVal pstrValida_Almacen As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo,
                                             "pvch_Valida_Articulo", pstrValida_Articulo,
                                             "pvch_Valida_Color", pstrValida_Color,
                                             "pvch_Valida_Presentacion", pstrValida_Presentacion,
                                             "pvch_Valida_Ubicacion", pstrValida_Ubicacion,
                                             "pvch_Valida_Almacen", pstrValida_Almacen}

            Return _objConnexion.ObtenerDataTable("USP_ALM_UBICACIONES_OBTENER_DATOS_ROLLO_UBIC", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function MoverRollosUbicacion(ByVal pstrCodUbicacion As String,
                                     ByVal pstrUsuario As String,
                                     ByVal pDTListaRollos As DataTable) As Integer
        Try
            Dim objUtil As New Util
            'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer
            pDTListaRollos.TableName = "ROLLOS"
            Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(pDTListaRollos))
            ' Dim strXMLData As string = objUtil.GeneraXml(pDTListaPedido)

            Dim objParametros As Object() = {"pvch_CodUbicacion", pstrCodUbicacion, _
                                             "pvch_Usuario", pstrUsuario, _
                                             "pvch_ListaRollosXML", strListaRollosXML.ToString}

            Return _objConnexion.EjecutarComando("USP_ALM_UBICACIONES_MOVER_ROLLOS_UBICACION", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarUbicacionesArticuloTransferencia(ByVal pstrRollo As String, ByVal pstrCodAlmacenOrigen As String, ByVal pstrCodAlmacenDestino As String, ByVal pstrNumeroPedido As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo,
                                             "pvch_CodAlmacenOrigen", pstrCodAlmacenOrigen,
                                             "pvch_CodAlmacenDestino", pstrCodAlmacenDestino,
                                             "pvch_NumeroPedido", pstrNumeroPedido}
            Return _objConnexion.ObtenerDataSet("USP_ALM_UBICACIONES_OBTENER_LISTADISPONIBLES_TRASNF", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerListaUbicaionesMultiArti() As DataTable
        Try

            Return _objConnexion.ObtenerDataTable("usp_qry_listaubicaciones")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerUbicacionesDisponiblesporDatosNdvAlmacen(ByVal pstrRollo As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo}
            Return _objConnexion.ObtenerDataSet("USP_ALM_UBICACIONES_OBTENER_LISTADISPONIBLES_DATOS_ROLLO_NDV", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerUbicacionesDisponiblesDevolucion() As DataTable
        Try
            Return _objConnexion.ObtenerDataTable("USP_ALM_UBICACIONES_OBTENER_UBICACIONES_DISPONIBLES_DEVOLUCION")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener ubicaciones disponibles por reingreso
    'Autor      : Juan Cucho Antunez
    'Creado     : 02/08/2016
    '*****************************************************************************************************
    Public Function ObtenerUbicacionesListaDisponiblesReingreso(ByVal pstrRollo As String) As DataSet
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo}
            Return _objConnexion.ObtenerDataSet("USP_ALM_OBTENER_UBICACIONES_LISTADISPONIBLES_REINGRESO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener datos de rollo por reingreso
    'Autor      : Juan Cucho Antunez
    'Creado     : 03/08/2016
    '*****************************************************************************************************
    Public Function ObtenerDatosRolloReingreso(ByVal pstrRollo As String, ByVal pstrValida_Articulo As String,
                                  ByVal pstrValida_Color As String,
                                  ByVal pstrValida_Presentacion As String) As DataTable
        Try
            Dim objparametros() As Object = {"pvch_CodigoRollo", pstrRollo,
                                             "pvch_Valida_Articulo", pstrValida_Articulo,
                                             "pvch_Valida_Color", pstrValida_Color,
                                             "pvch_Valida_Presentacion", pstrValida_Presentacion}

            Return _objConnexion.ObtenerDataTable("USP_ALM_OBTENER_DATOS_ROLLO_REINGRESO", objparametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : generar nota devolucion por reingreso
    'Autor      : Juan Cucho Antunez
    'Creado     : 03/08/2016
    '*****************************************************************************************************
    Function GenerarNdvReingresoTelaAlmacen(ByVal pstrNumeroGuia As String,
                                            ByVal pdtmFechaDevolucion As DateTime,
                                            ByVal pstrCodigoDestino As String,
                                            ByVal pstrMotivoDevolucion As String,
                                            ByVal pstrCodigoMotivo As String,
                                            ByVal pstrDescripcionMotivo As String,
                                            ByVal pstrCodUbicacion As String,
                                            ByVal pstrCodigoUsuario As String,
                                            ByVal pDTListaRollos As DataTable) As DataTable
        Try
            _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.RevisionFinal)
            Dim objUtil As New Util
            pDTListaRollos.TableName = "Devoluciones"
            Dim strListaRollosXML As New StringBuilder(objUtil.GeneraXml(pDTListaRollos))
            Dim objParametros As Object() = {"pvch_NumeroGuia", pstrNumeroGuia, _
                                             "pdtm_FechaDevolucion", pdtmFechaDevolucion, _
                                             "pvch_CodigoDestino", pstrCodigoDestino, _
                                             "pvch_MotivoDevolucion", pstrMotivoDevolucion, _
                                             "pvch_CodigoMotivo", pstrCodigoMotivo, _
                                             "pvch_DescripcionMotivo", pstrDescripcionMotivo, _
                                             "pvch_CodUbicacion", pstrCodUbicacion, _
                                             "pvch_CodigoUsuario", pstrCodigoUsuario, _
                                             "pvch_ListaRollosXML", strListaRollosXML.ToString}
            Return _objConnexion.ObtenerDataTable("USP_RVF_GENERAR_NDV_REINGRESO_TELA_ALMACEN", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener ubicacion por codigo
    'Autor      : Juan Cucho Antunez
    'Creado     : 30/01/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ObtenerCodigoXUbicacion(ByVal pStrCoUbicAlma As String) As DataTable
        Dim dtblUbicacion As DataTable
        Dim objParametros() As Object = {"pvar_co_ubic_alma", pStrCoUbicAlma}
        Try
            dtblUbicacion = _objConnexion.ObtenerDataTable("USP_LOG_OBTENER_UBICACION_X_CODIGO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblUbicacion
    End Function
    '*****************************************************************************************************
    'Objetivo   : Actualizar datos de ubicacion por codigo
    'Autor      : Juan Cucho Antunez
    'Creado     : 01/02/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ActualizarDatosUbicacionXCodigo(ByVal pStrCoAlma As String, _
                                                    ByVal pStrCoUbicAlma As String, _
                                                    ByVal pIntCapacidadUbicacion As Integer, _
                                                    ByVal pStrQuitarValidacion As String, _
                                                    ByVal pIntEstado As Integer, _
                                                    ByVal pStrUsuario As String) As Integer
        Dim int_CantidadFilasAfectadas As Integer = 0
        Dim objParametros() As Object = {"pvar_co_alma", pStrCoAlma,
                                         "pvar_co_ubic_alma", pStrCoUbicAlma,
                                         "pint_capacidad_ubic", pIntCapacidadUbicacion,
                                         "pvar_quitar_validacion", pStrQuitarValidacion,
                                         "pint_estado", pIntEstado,
                                         "pvar_usuario", pStrUsuario}
        Try
            int_CantidadFilasAfectadas = (_objConnexion.EjecutarComando("USP_LOG_ACTUALIZAR_DATOS_UBICACION_X_CODIGO", objParametros))
        Catch ex As Exception
            Throw ex
            int_CantidadFilasAfectadas = 0
        End Try
        Return int_CantidadFilasAfectadas
    End Function
#End Region
#Region "LUIS"
    Public Function MUESTRAFICHASARTICULOS() As DataTable
        Try

            Return _objConnexion.ObtenerDataTable("TMP_MUESTRAFICHASARTICULOS_LUIS")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "Dispose"
    Public Sub Dispose() Implements System.IDisposable.Dispose
        _objConnexion.Dispose()
    End Sub
#End Region

End Class
