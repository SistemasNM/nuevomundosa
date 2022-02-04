Imports NM.AccesoDatos

Public Class clsCorreo

    Private _objConexion As AccesoDatosSQLServer

    Public Function ServicioEnvioCorreo(ByVal srtMailTo As String, _
                                        ByVal strMailCC As String, _
                                        ByVal strMailBCC As String, _
                                        ByVal strSubject As String, _
                                        ByVal strBody As String, _
                                        Optional ByVal strRuta As String = "", _
                                        Optional ByVal strArchivo As String = "", _
                                        Optional ByVal strBodyFormat As String = "HTML", _
                                        Optional ByVal strImportance As String = "NORMAL") As Boolean

        Dim blnRpta As Boolean        
        Dim objParametros() = {"vch_mailTO", srtMailTo, _
                               "vch_mailCC", strMailCC, _
                               "vch_mailBCC", strMailBCC, _
                               "vch_Subject", strSubject, _
                               "vch_Body", strBody, _
                               "vch_rutaAttachment", strRuta, _
                               "vch_NomArchivo", strArchivo, _
                               "vch_bodyformat", strBodyFormat, _
                               "vch_importance", strImportance}


        Try
            blnRpta = True
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            _objConexion.EjecutarComando("USP_SERVICIO_ENVIO_CORREO", objParametros)
        Catch ex As Exception
            blnRpta = False
            Throw ex
        Finally
            _objConexion = Nothing
        End Try

        Return blnRpta

    End Function



End Class
