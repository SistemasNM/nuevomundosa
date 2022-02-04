Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsCorreo

    Public Property clsError As String = ""
    Public Property MailFrom As String = ""
    Public Property MailTo As String = ""
    Public Property MailCC As String = ""
    Public Property MailSubject As String = ""
    Public Property MailBody As String = ""
    Public Property MailSMTP As String = ""
    Public Property MailUser As String = ""
    Public Property MailPassword As String = ""
    Public Property MailSSL As Boolean
    Public Property MailPort As Integer
    Public Property MailCredenciales As String


    Public Function EnviarCorreos() As Boolean
        '*******************************************************************************************
        'Creado por:	Carlos Ponce Taype
        'Fecha     :    01-02-2013
        'Proposito :    Permite enviar correo segun los parametros
        '*******************************************************************************************

        Dim mailMsg As System.Net.Mail.MailMessage
        Dim blnRes As Boolean
        Dim lintIndice As Integer

        Try

            clsError = ""

            mailMsg = New System.Net.Mail.MailMessage()
            blnRes = True

            'Configurar arreglo para el TO
            Dim lstrTo_arreglo() As String = MailTo.Split(";")
            For lintIndice = 0 To lstrTo_arreglo.Length - 1
                If Strings.Trim(lstrTo_arreglo(lintIndice)).Length > 0 Then mailMsg.To.Add(lstrTo_arreglo(lintIndice))
            Next

            'Si no hay destinatario que lo envie a sistemas
            If mailMsg.To.Count <= 0 Then mailMsg.To.Add("sistemas@nuevomundosa.com")

            'Configurar arreglo para el CC
            Dim lstrCC_arreglo() As String = MailCC.Split(";")
            For lintIndice = 0 To lstrCC_arreglo.Length - 1
                If Strings.Trim(lstrCC_arreglo(lintIndice)).Length > 0 Then mailMsg.CC.Add(lstrCC_arreglo(lintIndice))
            Next

            With mailMsg
                .From = New System.Net.Mail.MailAddress(MailUser)
                .Subject = MailSubject
                .Body = MailBody
                .Priority = System.Net.Mail.MailPriority.High
                .IsBodyHtml = True
            End With

            Dim userCredential As New System.Net.NetworkCredential(MailUser, MailPassword)

            Dim Servidor As New System.Net.Mail.SmtpClient
            Servidor.Host = MailSMTP
            Servidor.Port = MailPort
            Servidor.EnableSsl = MailSSL
            If MailCredenciales = "1" Then
                Servidor.Credentials = userCredential
            End If


            Servidor.Send(mailMsg)
            Servidor = Nothing


        Catch ex As Exception
            clsError = ex.Message
            blnRes = False
        End Try

        Return blnRes

    End Function


  Public Function ListaCorreos(ByVal sUserGrupo As String, _
                               ByVal sSecuencia As String, _
                                 ByRef pDT As DataTable) As Boolean

    '*******************************************************************************************
    'Creado por:	Carlos Ponce Taype
    'Fecha     :    01/02/2013
    'Proposito :    Lista los correso segun grupo vendedor
    '*******************************************************************************************

    Dim blnRpta As Boolean
    Dim Conexion As AccesoDatosSQLServer
    Dim objParametro() As Object = {"vch_UserGrupo", sUserGrupo, "vch_Secuencia", sSecuencia}

    Try
      clsError = ""
      blnRpta = True
      Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
      pDT = Conexion.ObtenerDataTable("usp_venweb_Correos_Listar", objParametro)
    Catch ex As Exception
      blnRpta = False
      clsError = ex.Message
    Finally
      Conexion = Nothing
    End Try

    Return blnRpta

  End Function

    Public Function ServicioEnvioCorreo(ByVal srtMailTo As String, ByVal strMailCC As String, ByVal strMailBCC As String, ByVal strSubject As String,
                                        ByVal strBody As String, Optional strRuta As String = "", Optional strArchivo As String = "", Optional strBodyFormat As String = "HTML",
                                        Optional strImportance As String = "NORMAL") As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"vch_mailTO", srtMailTo,
                                        "vch_mailCC", strMailCC,
                                        "vch_mailBCC", strMailBCC,
                                        "vch_Subject", strSubject,
                                        "vch_Body", strBody,
                                        "vch_rutaAttachment", strRuta,
                                        "vch_NomArchivo", strArchivo,
                                        "vch_bodyformat", strBodyFormat,
                                        "vch_importance", strImportance}
        Try
            clsError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Conexion.EjecutarComando("USP_SERVICIO_ENVIO_CORREO", objParametro)
        Catch ex As Exception
            blnRpta = False
            clsError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function
End Class
