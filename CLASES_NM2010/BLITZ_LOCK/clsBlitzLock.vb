Public Class BlitzLock

  'Public Function Encripta(ByVal strFrase As String)
  '  Return GenerarMatriz(strFrase)
  'End Function

  Public Function Encripta(ByVal pstrTexto As String) As String
    Dim lstrTextoEncriptado As String = ""
    Dim i As Integer
    For i = 1 To pstrTexto.Length
      lstrTextoEncriptado = lstrTextoEncriptado + Chr(Asc(Mid(pstrTexto, i, 1)) + (i + 9))
    Next
    Return lstrTextoEncriptado
  End Function

      Private Function GenerarMatriz(ByVal strFrase As String) As String
        Dim strRetorno As String = ""
        For Indice As Int16 = 0 To strFrase.Length - 1
          strRetorno = strRetorno & EncodeChar(strFrase.Chars(Indice))
        Next
        Return strRetorno
      End Function

  Private Function EncodeChar(ByVal strCaracter As String) As String
    Dim intCodigo As Int16 = Asc(strCaracter)
    Dim dblFormula As Double = Math.Pow((2 * Math.Sqrt(3) / 3.0) * intCodigo, 2) + 2 * ((2 * Math.Sqrt(3) / 3.0) * intCodigo) * ((Math.Sqrt(3) / 3.0) * intCodigo) + Math.Pow((Math.Sqrt(3) / 3.0) * intCodigo, 2)
    Dim strMatriz As String = Format(dblFormula, "000000")
    Dim intFactor1 As Int16 = strMatriz.Substring(Len(strMatriz) - 2, 2)
    Dim intFactor2 As Int16 = strMatriz.Substring(Len(strMatriz) - 4, 2)
    Dim intFactor3 As Int16 = Left(strMatriz, Len(strMatriz) - 4)
    Return (Chr(intFactor1 + 35) & Chr(intFactor2 + 35) & Chr(intFactor3 + 35))
  End Function

End Class
