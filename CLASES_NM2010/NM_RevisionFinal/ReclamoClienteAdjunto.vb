Option Strict On
Imports System.Data
Imports NM.AccesoDatos
Imports System.Xml
Imports System.IO
Public Class ReclamoClienteAdjunto
#Region "Propiedades"
    Public Property TipoDocumento As String
    Public Property NumeroDocumento As String
    Public Property Secuencia As String
    Public Property CodigoArchivo As String
    Public Property NombreAdjunto As String
    Public Property AdjuntosJustiticados As String

    Public Property CodigoContenido As Integer
    Public Property TamanoAdjunto As String
    Public Property FechaAdjunto As Date

    Public Property NombreCortoAdjunto As String
    Public Property TipoAdjunto As String
    Public Property ObservacionAdjunto As String
    Public Property ValidoAdjunto As Integer
    Public Property UsuarioCreacion As String
#End Region
    Dim mobjConexionLogistica As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
    ' Agregar archivo adjunto
    Public Function fnc_InsertarAdjuntosReclamo() As Integer
        Dim int_Adjunta As Integer = 0
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDocumento", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia, _
                                              "pvch_NombreAdjunto", NombreAdjunto, _
                                              "pint_CodigoContenido", CodigoContenido, _
                                              "pvch_TamanoAdjunto", TamanoAdjunto, _
                                              "pdtm_FechaAdjunto", FechaAdjunto, _
                                              "pvch_NombreCortoAdjunto", NombreCortoAdjunto, _
                                              "pvch_ObservacionAdjunto", ObservacionAdjunto, _
                                              "pvch_UsuarioCreacion", UsuarioCreacion
                                              }

            mobjConexionLogistica.EjecutarComando("usp_qry_AgregarArchivoAdjunto_Reclamo", lobjParametros)
            int_Adjunta = 1
        Catch ex As Exception
            Throw ex
        End Try
        Return int_Adjunta
    End Function
    ' Listar archivos adjuntos Reclamo
    Public Function fnc_ListarAdjuntosReclamo(ByRef dtbListaAdjuntos As DataTable) As Boolean
        Dim blnAdjunta As Boolean
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDoc", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia}

            dtbListaAdjuntos = mobjConexionLogistica.ObtenerDataTable("usp_qry_ListarArchivoAdjunto_Reclamo", lobjParametros)
            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function
    ' Listar un informe de calidad por secuencia
    Public Function fnc_ListarAdjuntosInforme(ByRef dtbListaAdjuntos As DataTable) As Boolean
        Dim blnAdjunta As Boolean
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDoc", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia}

            dtbListaAdjuntos = mobjConexionLogistica.ObtenerDataTable("usp_qry_ListarArchivoAdjunto_Informe", lobjParametros)
            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function

    ' Listar todos los informes de calidad de un reclamo
    Public Function fnc_ListarArchivoAdjuntoAllInformeCalidadxReclamo(ByRef dtbListaAdjuntos As DataTable) As Boolean
        Dim blnAdjunta As Boolean
        Try
            Dim lobjParametros() As Object = {"pvch_TipoDoc", TipoDocumento, _
                                              "pvch_NumeroDocumento", NumeroDocumento, _
                                              "pint_Secuencia", Secuencia, _
                                              "pAdjuntosJustiticados", AdjuntosJustiticados}

            dtbListaAdjuntos = mobjConexionLogistica.ObtenerDataTable("usp_qry_ListarArchivoAdjuntoAllInformeCalidadxReclamo", lobjParametros)
            blnAdjunta = True
        Catch ex As Exception
            blnAdjunta = False
            Throw ex
        End Try
        Return blnAdjunta
    End Function
End Class
