Imports NuevoMundo.Generales
Imports NM.AccesoDatos
Public Class NMAuditoria
    Public Function Insertar_Politicas( _
            ByVal strNombreDocumento As String, ByVal strNombreDocumentoSistema As String, _
            ByVal strComentarios As String, ByVal strArea As String, _
            ByVal strRuta As String, ByVal strEstado As Integer, _
            ByVal strFechaPublic As String, ByVal StrUsuario As String) As Boolean
        Try
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Dim objParametros() As Object = {"var_DocumentoNombre", strNombreDocumento, "var_DocumentoNombreSistema", strNombreDocumentoSistema, _
            "var_Comentario", strComentarios, "chr_AreaPublicacion", strArea, _
            "var_Ruta", strRuta, "int_Estado", strEstado, "dtm_FechaPublicacion", strFechaPublic, _
            "var_UsuarioCreacion", StrUsuario}
            Return (lobjCon.EjecutarComando("usp_qry_Inserta_Potiticas", objParametros) > 0)
            lobjCon = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Listar_Politicas(ByVal strArea As String, ByVal strAnno As String) As DataTable
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Dim larrParams() As Object = {"chr_AreaPublicacion", strArea, "chr_Anno", strAnno}
        Try
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            ldtRes = lobjCon.ObtenerDataTable("usp_qry_listar_politicas", larrParams)
            Return ldtRes
        Catch ex As Exception
            Throw ex
        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
    End Function
    Public Function Eliminar_Politicas(ByVal strCodigoPolitica As Integer) As Boolean
        Try
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            lobjCon = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
            Dim objParametros() As Object = {"int_ID", strCodigoPolitica}
            Return (lobjCon.EjecutarComando("usp_qry_Elimina_politicas", objParametros) > 0)
            lobjCon = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
