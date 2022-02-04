Imports NM.AccesoDatos

Public Class clsCotizacion

    Dim mobjConexionLogistica As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

    Public Property TipoDocumento As String
    Public Property NumeroDocumento As String
    Public Property Secuencia As String
    Public Property FechaCotizacion As Date
    Public Property UsuarioCotizacion As String
    Public Property CodigoProveedor As String
    Public Property Monto As String
    Public Property Observaciones As String

    ' Agregar cotizacion de un item/rqs
    Public Function fnc_InsertarCotizacion() As Integer
        Dim int_Adjunta As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDocumento", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia, _
                                              "pvch_UsuarioCotizacion", UsuarioCotizacion, _
                                              "pvch_CodigoProveedor", CodigoProveedor, _
                                              "pnum_Monto", Monto, _
                                              "pvch_Observaciones", Observaciones}
            mobjConexionLogistica.EjecutarComando("usp_qry_AgregarCotizacion", lobjParametros)
            int_Adjunta = 1
        Catch ex As Exception
            int_Adjunta = 0
            Throw ex
        End Try
        Return int_Adjunta
    End Function

    ' Agregar cotizacion masivo
    Public Function fnc_InsertarCotizacionMasivo() As Integer
        Dim int_Adjunta As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDocumento", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pvch_UsuarioCotizacion", UsuarioCotizacion}
            mobjConexionLogistica.EjecutarComando("usp_qry_AgregarCotizacionMasivo", lobjParametros)
            int_Adjunta = 1
        Catch ex As Exception
            int_Adjunta = 0
            Throw ex
        End Try
        Return int_Adjunta
    End Function

End Class
