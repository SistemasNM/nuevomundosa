Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class clsRequest
    Dim strMatriz As String = "%H)OU!@56C89-"

    Public Function Encripta(ByVal strFrase As String) As String
        Return GenerarMatriz(strFrase)
    End Function

    Public Function Decripta(ByVal strFrase As String) As String
        Return DevelarMatriz(strFrase)
    End Function

    Private Function GenerarMatriz(ByVal strFrase As String) As String
        Dim strRetorno As String = ""
        For Indice As Int16 = 0 To strFrase.Length - 1
            strRetorno = strRetorno & EncodeChar(strFrase.Chars(Indice))
        Next
        Return strRetorno
    End Function

    Private Function DevelarMatriz(ByVal strFrase As String) As String
        Dim strRetorno As String = ""
        For Indice As Int16 = 0 To strFrase.Length - 1 Step 2
            strRetorno = strRetorno & DecodeChar(strFrase.Chars(Indice), strFrase.Chars(Indice + 1))
        Next
        Return strRetorno
    End Function

    Private Function EncodeChar(ByVal strCaracter As String) As String
        Dim intCodigo As Int16 = Asc(strCaracter)
        Dim strX As String = Left(CStr(intCodigo), CStr(intCodigo).Length - 1)
        Dim strY As String = Right(CStr(intCodigo), 1)
        If CInt(strX) = 0 Then
            strX = 13
        End If
        If CInt(strY) = 0 Then
            strY = 13
        End If
        Return (strMatriz.Substring(CInt(strX) - 1, 1) & strMatriz.Substring(CInt(strY) - 1, 1))
    End Function

    Private Function DecodeChar(ByVal strX As String, ByVal strY As String) As String
        Dim intNumeralX As Int16 = InStr(strMatriz, strX)
        Dim intNumeralY As Int16 = InStr(strMatriz, strY)
        If intNumeralX = 13 Then
            intNumeralX = 0
        End If
        If intNumeralY = 13 Then
            intNumeralY = 0
        End If
        Dim strCaracter As String = Chr(CInt(intNumeralX.ToString & intNumeralY.ToString))
        Return (strCaracter)
    End Function

    Public Function Encrypt(ByVal clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Public Function Decrypt(ByVal cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        cipherText = cipherText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function
End Class
