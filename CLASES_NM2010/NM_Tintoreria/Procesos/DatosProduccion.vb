Imports NM.AccesoDatos
Public Class DatosProduccion
    'Private mstrCodigo As String = ""
    Private mstrCodigoArticuloCorto As String = ""
    Private mstrCodigoOP As String = ""
    Private mobjArticulo As Maestros.Articulo
    'Private mstrUsuario As String = ""
    Private mobjDatosProduccion As DatosProduccion
    Public Function Listar_Productividad_Tintoreria(ByVal strFechaIni As String, ByVal StrFechaFin As String, ByVal strNumeroFicha As String, ByVal strNumeroOrden As String, ByVal strArticulo As String, ByVal strAcabado As String, ByVal strDias As Byte, ByVal strTipo As String) As DataTable
        Dim lobjTinto As AccesoDatosSQLServer
        Dim mdsSet As DataTable
        Dim lbooOk As Boolean
        'mobjDatosProduccion = New DatosProduccion(mstrUsuario, mstrCodigo)
        Try
            lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim larrParametros() As String = {"Fecha_Inicio", strFechaIni, "Fecha_Fin", StrFechaFin, "Numero_Ficha", strNumeroFicha, "Orden_Produccion", strNumeroOrden, "Articulo", strArticulo, "Acabado", strAcabado, "Dias_Tintoreria", strDias, "Tipo", strTipo}
            Return (CType(lobjTinto.ObtenerDataTable("USP_TIN_INDICADOR_PRODUCCION_TINTORERIA", larrParametros), DataTable))
        Catch ex As Exception
            Throw ex
        Finally
            lobjTinto = Nothing
            mdsSet = Nothing
        End Try
    End Function
    Public Function ufn_ProduccionXFecha_Resumido(ByVal strCodigoMaquina As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String) As DataTable
    Dim lobjCon As AccesoDatosSQLServer
        Dim lstrParametros() As String = {"Codigo_Maq", CStr(strCodigoMaquina), _
                                        "FechaIni", CStr(strFechaInicio), _
                                        "FechaFin", CStr(strFechaFinal)}
        Try
      lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Return lobjCon.ObtenerDataTable("USP_TIN_ProduccionXFecha_Resumido", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ufn_ProduccionXFecha_Detallado(ByVal strCodigoMaquina As String, ByVal strFechaInicio As String, ByVal strFechaFinal As String) As DataTable
    Dim lobjCon As AccesoDatosSQLServer
        Dim lstrParametros() As String = {"Codigo_Maq", CStr(strCodigoMaquina), _
                                        "FechaIni", CStr(strFechaInicio), _
                                        "FechaFin", CStr(strFechaFinal)}
        Try
      lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Return lobjCon.ObtenerDataTable("USP_TIN_ProduccionXFecha_Detallado", lstrParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
