Imports System.Web
Imports System.Web.SessionState
Imports System.Diagnostics
Imports System.Configuration

Module ModMensaje
    Public Function fnc_EnviarMensaje(ByVal pstrPara As String, ByVal pstrCodigoTabla As String, ByVal pstrAsunto As String, ByVal pstrMensaje As String) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma y CPT
        'Fecha     :      2010.05.03
        'Proposito :      Enviar correo
        '*******************************************************************************************
        Dim lstrMensaje As String = ""
        Dim lblnResultado As Boolean = False

        Try
            lstrMensaje = pstrMensaje

            If lstrMensaje.Trim <> "" Then

                Dim objUtil As New ComunLib.clsUtil
                Dim dtbAlerta As DataTable = objUtil.DatosAlerta_Obtener(pstrCodigoTabla) 'objUtil.DatosAlerta_Obtener("0023")
                If dtbAlerta.Rows.Count > 0 Then

                    '-- enviando el correo --

                    Dim mailMsg As New System.Net.Mail.MailMessage

                    mailMsg.From = New System.Net.Mail.MailAddress("Pre-Costos<EnlaceNM@nuevomundosa.com>")

                    Dim aTo() As String = Split(Trim(dtbAlerta.Rows(0)("PARA")), ";")
                    Dim aCC() As String = Split(Trim(dtbAlerta.Rows(0)("CC")), ";")
                    Dim aBcc() As String = Split(Trim(dtbAlerta.Rows(0)("BCC")), ";")

                    If aTo.Count > 0 Then
                        For i = 0 To aTo.Count - 1
                            If Trim(aTo(i)) <> "" Then
                                mailMsg.To.Add(Trim(aTo(i)))
                            End If
                        Next
                    End If

                    If aCC.Count > 0 Then
                        For i = 0 To aCC.Count - 1
                            If Trim(aCC(i)) <> "" Then
                                mailMsg.CC.Add(Trim(aCC(i)))
                            End If
                        Next
                    End If

                    If aBcc.Count > 0 Then
                        For i = 0 To aBcc.Count - 1
                            If Trim(aBcc(i)) <> "" Then
                                mailMsg.Bcc.Add(Trim(aBcc(i)))
                            End If
                        Next
                    End If


                    mailMsg.Subject = pstrAsunto 'dtbAlerta.Rows(0)("Subject") '"Error Ficha Costos"
                    mailMsg.Body = lstrMensaje
                    mailMsg.Priority = Net.Mail.MailPriority.High
                    mailMsg.IsBodyHtml = True

                    Dim smtp As New System.Net.Mail.SmtpClient
                    smtp.Host = "172.16.0.10"

                    'Si es necesario poner el usuario y password
                    'smtp.Credentials = New System.Net.NetworkCredential("", "")

                    smtp.Send(mailMsg)
                    smtp = Nothing
                    mailMsg = Nothing

                End If
            End If

            lblnResultado = True

        Catch ex As Exception
            lblnResultado = False
        End Try

        Return lblnResultado

    End Function

    Public Function miCorreo() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.05.03
        'Proposito :      Enviar correo

        '*******************************************************************************************

        Try

            Dim lstrMensaje As String

            lstrMensaje = "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'>SE HA CERRADO LA DISTRIBUCION " + _
            "DE AGUA DEL PERIODO: <FONT style='BACKGROUND-COLOR: #ffff66'>" + _
            "<STRONG>2010-01</STRONG><FONT style='BACKGROUND-COLOR: #ffffff'><STRONG>&nbsp;" + _
            "</STRONG></FONT></FONT></P>" + _
            "<P style='FONT-SIZE: 11px; FONT-FAMILY: Verdana'><FONT style='BACKGROUND-COLOR: #ffff66'><FONT style='BACKGROUND-COLOR: #ffffff'>APROBADO POR EL USUARIO:&nbsp;<FONT style='BACKGROUND-COLOR: #ffff66'><STRONG>cpt</STRONG></FONT>.</FONT><BR><BR></FONT><BR>" + _
       "<A title='http://servnmprb/intrasolution/index.asp' href='http://servnmprb/intrasolution/index.asp'>" + _
        "ACCESO DIRECTO AL SISTEMA INTRANET</A><BR></P>" + _
        "<P style='FONT-SIZE: 9px; FONT-FAMILY: Verdana'>-------------------------------------------------------------------------------<BR>" + _
       "Este correo ha sido generado automticamente por el mdulo de aprobaciones.<BR>" + _
       "Por favor no responder este correo.<BR>" + _
       "Departamento de Sistemas<BR>" + _
       "Ca. Industrial Nuevo Mundo S.A.<BR>" + _
       "-------------------------------------------------------------------------------</P>"



            Dim mail As New System.Net.Mail.MailMessage

            mail.To.Add("cponce@nuevomundosa.com")
            mail.From = New System.Net.Mail.MailAddress("FichaCostos@nuevomundosa.com")
            mail.Subject = "test"
            mail.IsBodyHtml = True
            mail.Body = lstrMensaje
            mail.Priority = Net.Mail.MailPriority.High

            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Host = "192.168.116.2"

            'smtp.Credentials = New System.Net.NetworkCredential("", "")

            smtp.Send(mail)

        Catch ex As Exception

        End Try


    End Function

End Module
