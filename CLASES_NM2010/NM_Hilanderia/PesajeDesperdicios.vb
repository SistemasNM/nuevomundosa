Imports NM.AccesoDatos
Imports NM_General

Public Class PesajeDesperdicios
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

    Public Function Obtener_Lista_Descripcion_Proceso(ByVal strUsuario As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pvch_Usuario", strUsuario}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_BUSCAR_LISTA_PROCESO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function


    Public Function Obtener_Lista_Descripcion_SubTipo(ByVal strCodProceso As String, ByVal strTipo_Auxi As String, ByVal strUsuario As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pvch_CodProceso", strCodProceso,
                                         "pvch_Tipo_Auxi", strTipo_Auxi,
                                         "pvch_Usuario", strUsuario}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_BUSCAR_LISTA_SUBTIPO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function

    Public Function Obtener_Lista_Descripcion_Destino(ByVal strTipo_Auxi As String, ByVal strCentro_Costo As String, ByVal strUsuario As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pvch_Tipo_Auxi", strTipo_Auxi,
                                         "pvch_Centro_Costo", strCentro_Costo,
                                         "pvch_Usuario", strUsuario}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_BUSCAR_LISTA_DESTINO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function

    Public Function Obtener_Lista_Descripcion_Articulo(ByVal strProceso As String, ByVal strCentroCosto As String) As DataTable
        Dim dtblDatos As DataTable

        Dim objParametros() As Object = {"pvch_Proceso", strProceso,
                                         "pvch_CentroCosto", strCentroCosto}
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_BUSCAR_LISTA_ARTICULO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos
    End Function


    Public Function RegistrarDatosDesperdicios(ByVal strProceso As String,
                                               ByVal strTipo As String,
                                               ByVal strSubTipo As String,
                                               ByVal strDestino As String,
                                               ByVal strArticulo As String,
                                               ByVal strObservacion As String,
                                               ByVal strFechaDocumento As String,
                                               ByVal dblTotalKilos As Double,
                                               ByVal strUsuario As String,
                                               ByVal strCodBalanza As String,
                                               ByVal strCodigo_Tara As String,
                                               ByVal dblPeso_Tara As Double) As Integer

        Dim strResult As Integer
        Dim objUtil As New Util
        'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer

        Dim objParametros() As Object = {"pvch_Proceso", strProceso,
                                         "pvch_Tipo", strTipo,
                                         "pvch_SubTipo", strSubTipo,
                                         "pvch_Destino", strDestino,
                                         "pvch_Articulo", strArticulo,
                                         "pvch_Observacion", strObservacion,
                                         "pvch_FechaDocumento", strFechaDocumento,
                                         "pnum_TotalKilos", dblTotalKilos,
                                         "pvch_Usuario", strUsuario,
                                         "pvch_CodBalanza", strCodBalanza,
                                         "pvch_Codigo_Tara", strCodigo_Tara,
                                         "pnum_Peso_Tara", dblPeso_Tara}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_DESPERDICIOS_REGISTRAR_DATOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function RegistrarDatosDesperdicios_V2(ByVal strProceso As String,
                                                  ByVal strTipo As String,
                                                  ByVal strSubTipo As String,
                                                  ByVal strDestino As String,
                                                  ByVal strArticulo As String,
                                                  ByVal strObservacion As String,
                                                  ByVal strFechaDocumento As String,
                                                  ByVal dblTotalKilos As Double,
                                                  ByVal strUsuario As String,
                                                  ByVal strCodBalanza As String,
                                                  ByVal dblPesoTara As Double,
                                                  ByVal dblPesoBruto As Double,
                                                  ByVal strCodContenedor As String,
                                                  ByVal dblPesoContenedor As Double,
                                                  ByVal strCodBobina As String,
                                                  ByVal intCantBobina As Integer,
                                                  ByVal strCodMotivo As String) As Integer

        Dim strResult As Integer
        Dim objUtil As New Util
        'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer

        Dim objParametros() As Object = {"pvch_Proceso", strProceso,
                                         "pvch_Tipo", strTipo,
                                         "pvch_SubTipo", strSubTipo,
                                         "pvch_Destino", strDestino,
                                         "pvch_Articulo", strArticulo,
                                         "pvch_Observacion", strObservacion,
                                         "pvch_FechaDocumento", strFechaDocumento,
                                         "pnum_TotalKilos", dblTotalKilos,
                                         "pvch_Usuario", strUsuario,
                                         "pvch_CodBalanza", strCodBalanza,
                                         "pnum_Peso_Tara", dblPesoTara,
                                         "pnum_Peso_Bruto", dblPesoBruto,
                                         "pvch_CodContenedor", strCodContenedor,
                                         "pnum_PesoContenedor", dblPesoContenedor,
                                         "pvch_CodBobina", strCodBobina,
                                         "pnum_CantBobina", intCantBobina,
                                         "pvch_CodMotivo", strCodMotivo}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_DESPERDICIOS_REGISTRAR_DATOS_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function


    Public Function Obtener_Lista_Registros_Pesaje_Desperdicios(ByVal strCodProceso As String, ByVal strCodBalanza As String, ByVal strFechaDocumento As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_CodProceso", strCodProceso,
                                             "pvch_CodBalanza", strCodBalanza,
                                             "pvch_FechaDocumento", strFechaDocumento}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_LISTAR_PESAJES_DESPERDICIOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function


    Public Function Obtener_Datos_Desperdicio_Etiqueta(ByVal intCodigoDesperdicio As Integer) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"int_IdDesperdicio", intCodigoDesperdicio}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_OBTENER_DATOS_ETIQUETA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Eliminar_Pesaje_Desperdicio(ByVal strCodigoParihuela As String, ByVal strUsuarioSupervisor As String) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pvch_CodigoParihuela", strCodigoParihuela,
                                             "pvch_UsuarioSupervisor", strUsuarioSupervisor}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_DESPERDICIOS_ELIMINAR_PESAJE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function


    Public Function Obtener_Lista_Registros_Pesaje_Reclasificados(ByVal strCodBalanza As String) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pvch_CodBalanza", strCodBalanza}

            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_LISTAR_PESAJES_RECLASIFICADOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function RegistrarDatosReclasificacion(ByVal strArticuloOrigen As String,
                                                  ByVal strArticuloDestino As String,
                                                  ByVal strObservacion As String,
                                                  ByVal dblTotalKilos As Double,
                                                  ByVal dblPesoTara As Double,
                                                  ByVal dblPesoBruto As Double,
                                                  ByVal strCodContenedor As String,
                                                  ByVal dblPesoContenedor As Double,
                                                  ByVal strCodBobina As String,
                                                  ByVal intCantBobina As Integer,
                                                  ByVal strUsuario As String,
                                                  ByVal strCodBalanza As String) As Integer

        Dim strResult As Integer
        Dim objUtil As New Util
        'Dim lobjVentas As NM.AccesoDatos.AccesoDatosSQLServer

        Dim objParametros() As Object = {"pvch_ArticuloOrigen", strArticuloOrigen,
                                         "pvch_ArticuloDestino", strArticuloDestino,
                                         "pvch_Observacion", strObservacion,
                                         "pnum_TotalKilos", dblTotalKilos,
                                         "pnum_Peso_Tara", dblPesoTara,
                                         "pnum_Peso_Bruto", dblPesoBruto,
                                         "pvch_CodContenedor", strCodContenedor,
                                         "pnum_PesoContenedor", dblPesoContenedor,
                                         "pvch_CodBobina", strCodBobina,
                                         "pnum_CantBobina", intCantBobina,
                                         "pvch_Usuario", strUsuario,
                                         "pvch_CodBalanza", strCodBalanza}

        Try
            strResult = m_sqlHilanderia.EjecutarComando("USP_HIL_RECLASIFICACION_REGISTRAR_DATOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return strResult
    End Function

    Public Function Eliminar_Pesaje_Reclasificacion(ByVal strIdReclasificacion As String, ByVal strUsuarioSupervisor As String) As Integer
        Dim intResult As Integer
        Try
            Dim objParametros() As Object = {"pvch_IdReclasificacion", strIdReclasificacion,
                                             "pvch_UsuarioSupervisor", strUsuarioSupervisor}
            intResult = m_sqlHilanderia.EjecutarComando("USP_HIL_RECLASIFICACION_ELIMINAR_PESAJE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return intResult

    End Function

    Public Function Obtener_Datos_Reclasificacion_Etiqueta(ByVal intCodigoReclasificacion As Integer) As DataTable
        Dim dtblDatos As DataTable

        Try
            Dim objParametros() As Object = {"pint_IdReclasificacion", intCodigoReclasificacion}
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_RECLASIFICACION_OBTENER_DATOS_ETIQUETA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try

        Return dtblDatos

    End Function

    Public Function Obtener_Lista_CargaInicial_Fardos() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_CARGAINICIAL_DESPERDICIO")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function

    Public Function Obtener_Lista_Descripcion_Motivo() As DataTable
        Dim dtblDatos As DataTable

        Try
            dtblDatos = m_sqlHilanderia.ObtenerDataTable("USP_HIL_DESPERDICIOS_BUSCAR_LISTA_MOTIVO")
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