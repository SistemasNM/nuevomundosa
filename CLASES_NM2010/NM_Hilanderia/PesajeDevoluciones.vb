Imports NM.AccesoDatos
Imports NM_General

Public Class PesajeDevoluciones
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

    Public Function Obtener_Lista_Descripcion_Maestro_Balanza(ByVal strProceso As String, ByVal strSubProceso As String, ByVal strTipo As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pvch_Proceso", strProceso,
                                         "pvch_SubProceso", strSubProceso,
                                         "pvch_Tipo", strTipo}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DEVOLUCIONES_LISTA_MAESTRO_BALANZA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function

    Public Function Obtener_Lista_Titulos_Hilos() As DataTable
        Dim dtblDatos As DataTable

        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DEVOLUCIONES_BUSCAR_TITULO_HILOS")
        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Lista_Descripcion_Hilos(ByVal strTitulo As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pstr_Titulo", strTitulo}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DEVOLUCIONES_BUSCAR_DESCRIPCION_HILOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function


    Public Function RegistrarDatosDevoluciones(ByVal strTipoDevolucion As String,
                                               ByVal strRUC_Empresa As String,
                                               ByVal strCodigoArticulo As String,
                                               ByVal strTipoHilo As String,
                                               ByVal strCodigo_Origen As String,
                                               ByVal strCodigo_Destino As String,
                                               ByVal strCodigo_Ubicacion As String,
                                               ByVal strObservaciones As String,
                                               ByVal strFechaDocumento As String,
                                               ByVal strCodigo_Paleta As String,
                                               ByVal dblPeso_Paleta As Double,
                                               ByVal strCodigo_Bobina As String,
                                               ByVal intCantidad_Bobina As Integer,
                                               ByVal dblPeso_Bobina As Double,
                                               ByVal strCodigo_CartonT As String,
                                               ByVal intCantidad_CartonT As Integer,
                                               ByVal dblPeso_CartonT As Double,
                                               ByVal dblTotal_PesoBruto As Double,
                                               ByVal dblTotal_PesoTara As Double,
                                               ByVal strUsuario As String,
                                               ByVal strCodBalanza As String,
                                               ByVal strCodigoAlmacen As String) As Integer


        Dim strResult As Integer

        Dim objParametros() As Object = {"pvch_TipoDevolucion", strTipoDevolucion,
                                         "pvch_RUC_Empresa", strRUC_Empresa,
                                         "pvch_Codigo_Articulo", strCodigoArticulo,
                                         "pvch_Tipo_Hilo", strTipoHilo,
                                         "pvch_Codigo_Origen", strCodigo_Origen,
                                         "pvch_Codigo_Destino", strCodigo_Destino,
                                         "pvch_Codigo_Ubicacion", strCodigo_Ubicacion,
                                         "pvch_Observaciones", strObservaciones,
                                         "pvch_Fecha_Documento", strFechaDocumento,
                                         "pvch_Codigo_Paleta", strCodigo_Paleta,
                                         "pnum_Peso_Paleta", dblPeso_Paleta,
                                         "pvch_Codigo_Bobina", strCodigo_Bobina,
                                         "pnum_Cantidad_Bobina", intCantidad_Bobina,
                                         "pnum_Peso_Bobina", dblPeso_Bobina,
                                         "pvch_Codigo_CartonT", strCodigo_CartonT,
                                         "pnum_Cantidad_CartonT", intCantidad_CartonT,
                                         "pnum_Peso_CartonT", dblPeso_CartonT,
                                         "pnum_Total_PesoBruto", dblTotal_PesoBruto,
                                         "pnum_Total_PesoTara", dblTotal_PesoTara,
                                         "pvch_Usuario", strUsuario,
                                         "pvch_CodBalanza", strCodBalanza,
                                         "pvch_CodigoAlmacen", strCodigoAlmacen
                                        }

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_DEVOLUCION_REGISTRAR_DATOS_BALANZA_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function



    Public Function Obtener_Lista_Registros_Pesaje_Devoluciones(ByVal strCodBalanza As String, ByVal strFechaDocumento As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_CodBalanza", strCodBalanza,
                                             "pvch_FechaDocumento", strFechaDocumento}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DEVOLUCIONES_LISTAR_PESAJES_ACTIVOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Eliminar_Pesaje_Devoluciones(ByVal strCodigoDevolucion As String, ByVal strUsuarioSupervisor As String) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pint_IdDevolucion", strCodigoDevolucion,
                                             "pvch_UsuarioSupervisor", strUsuarioSupervisor}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_DEVOLUCIONES_ELIMINAR_PESAJE_BALANZA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function


#End Region


    Public Sub Dispose() Implements System.IDisposable.Dispose
        m_sqlHilanderia.Dispose()
    End Sub
End Class
