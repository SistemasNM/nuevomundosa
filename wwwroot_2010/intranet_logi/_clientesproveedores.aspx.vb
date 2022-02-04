Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.Services
Imports System.Net
Imports System.IO

Public Class _clientesproveedores
    Inherits System.Web.UI.Page

    Dim oEnlaceService As New AccesoEnlace.AccesoEnlaceClient()
    'Dim gstrTipLogeo As String = ""
    'Dim gstrRuc As String = ""
    'Dim gstrUsuario As String = ""
    'Dim gstrPassword As String = ""
    'Dim gstrResponse As String = ""


    <System.Web.Services.WebMethod()>
    Public Function fLogin(ByVal sUsuario As String, ByVal sContrasena As String) As String
        Dim sUsuarioEnc As String = ""
        Dim sContrasenaEnc As String = ""
        Dim bResult As Boolean
        Dim sRetorno As String = ""

        Try
            bResult = oEnlaceService.LoginUsuario(sUsuario, sContrasena, sUsuarioEnc, sContrasenaEnc)

            If bResult Then
                sRetorno = "http://200.60.99.228/EnlaceNM_Extranet/menu_Extranet.aspx?param1=" & sUsuarioEnc & "&param2=" & sContrasenaEnc & "'"
            End If

        Catch
        End Try

        Return sRetorno
    End Function

    <System.Web.Services.WebMethod()>
    Public Function fLoginProveedor(ByVal sRuc As String, ByVal sUsuario As String, ByVal sContrasena As String) As String
        Dim sUsuarioEnc As String = ""
        Dim sContrasenaEnc As String = ""
        Dim bResult As Boolean
        Dim sRetorno As String = ""
        Dim strIp As String

        Try
            strIp = returnIp()

            If strIp = "200.60.99.228" Then
                bResult = oEnlaceService.LoginProveedor(sRuc, sUsuario, sContrasena, sUsuarioEnc, sContrasenaEnc)
            Else
                bResult = oEnlaceService.LoginProveedor(sRuc, sUsuario, sContrasena, sUsuarioEnc, sContrasenaEnc)
            End If

            If bResult Then
                sRetorno = "http://" & strIp & "/EnlaceNM_Extranet/frm_menu_proveedor.aspx?param1=" & sUsuarioEnc & "&param2=" & sContrasenaEnc & "'"
            End If

        Catch
        End Try

        Return sRetorno
    End Function

    Public Shared Function returnIp() As String
        Dim strIp As String

        Try
            strIp = "190.102.143.90"
            Dim strUrl As String = "http://" & strIp & "/EnlaceNM_Extranet/pong.html"
            Dim result As String
            Dim myReq As HttpWebRequest = CType(WebRequest.Create(strUrl), HttpWebRequest)
            myReq.Timeout = 5000
            myReq.ReadWriteTimeout = 5000
            myReq.GetResponse()
            Dim httpResponse = CType(myReq.GetResponse(), HttpWebResponse)

            Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                result = streamReader.ReadToEnd()
            End Using

            If result = "pong" Then
                strIp = "190.102.143.90"
            End If

        Catch ex As Exception
            strIp = "200.60.99.228"
        End Try

        Return strIp
    End Function

    'Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    If Not IsPostBack Then
    '        'Dim str As String = fLoginProveedor("20427481370", "SHURTAPE", "PVEXS23")
    '        gstrTipLogeo = Request.QueryString("vchTipLogeo")
    '        gstrRuc = Request.QueryString("vchRuc") '"20427481370"
    '        gstrUsuario = Request.QueryString("vchUser") '"SHURTAPE"
    '        gstrPassword = Request.QueryString("vchPass") '"PVEXS23"

    '        If gstrTipLogeo.Equals("P") Then
    '            gstrResponse = fLoginProveedor(gstrRuc, gstrUsuario, gstrPassword)
    '            If Not gstrResponse.Equals("") Then

    '            End If
    '        ElseIf gstrTipLogeo.Equals("C") Then
    '            If Not gstrResponse.Equals("") Then

    '            End If
    '            Response.Redirect(fLogin(gstrUsuario, gstrPassword))
    '        End If
    '    End If
    'End Sub
End Class