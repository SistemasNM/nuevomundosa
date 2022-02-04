Option Strict On

Imports System.IO
Imports System.Data
Imports NM.AccesoDatos


Public Class LoteArchivo

  Private sError As String
  Private sRuta As String
  Private bCreate As Boolean


  '==========================   Definicion de propiedades  ================================

  Public ReadOnly Property clsError() As String
    Get
      clsError = sError
    End Get
  End Property

  ' Ruta donde se graba el archivo
  Public WriteOnly Property sPath() As String

    Set(ByVal sCad As String)
      sRuta = sCad
    End Set

  End Property


  Public Sub GenerateFileBSC(ByVal strCod_Lote As String, ByVal strNomFile As String)

    Dim clsObj As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
    Dim clsDT As New DataTable

    ' Objetos de archivos
    Dim strFile As Stream
    Dim strWriter As StreamWriter
    Dim sFilePath As String = sRuta & "BANCO SCOTIABANK\" & strNomFile & ".txt"

    Dim strTipoLetra As String
    Dim strTipoMoneda As String
    Dim strCuentaBT As String
    Dim strTipoInters As String
    Dim strTasaClient As String
    Dim strFechaVenc As String
    Dim strImporte As String
    Dim strNroCedente As String
    Dim strNombreAcep As String
    Dim strTipoVia As String
    Dim strDirecAcep As String
    Dim strNroDirecc As String
    Dim strNomCiudad As String
    Dim strTipDocIde As String
    Dim strNumDocIde As String
    Dim strTipoAval As String
    Dim strProtesto As String
    Dim intFila As Integer

    Dim lobjParametros() As Object = {"pCodigoPlanilla", strCod_Lote}


    Try

      sError = ""

      If File.Exists(sFilePath) = True Then
        File.Delete(sFilePath)
      End If

      clsDT = clsObj.ObtenerDataTable("usp_LetrasBanco_Lst", lobjParametros)

      strFile = File.OpenWrite(sFilePath)
      strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

      For intFila = 0 To clsDT.Rows.Count - 1


        With clsDT.Rows(intFila)
          strTipoLetra = CType(.Item("TipoLetra"), String)
          strTipoMoneda = CType(.Item("TipoMoneda"), String)
          strCuentaBT = CType(.Item("CuentaBT"), String)
          strTipoInters = CType(.Item("TipoInters"), String)
          strTasaClient = CType(.Item("TasaClient"), String)
          strFechaVenc = CType(.Item("FechaVenc"), String)
          strImporte = fsNumDec(CType(.Item("Importe"), String))
          strNroCedente = LSet(fsLeter(CType(.Item("NroCedente"), String)), 14)
          strNombreAcep = LSet(CType(.Item("NombreAcep"), String), 30)
          strTipoVia = CType(.Item("TipoVia"), String)
          strDirecAcep = LSet(fsLeter(CType(.Item("DirecAcep"), String)), 30)
          strNroDirecc = CType(.Item("NroDirecc"), String)
          strNomCiudad = LSet(CType(.Item("NomCiudad"), String), 30)
          strTipDocIde = CType(.Item("TipDocIde"), String)
          strNumDocIde = LSet(CType(.Item("NumDocIde"), String), 12)
          strTipoAval = CType(.Item("TipoAval"), String)
          strProtesto = CType(.Item("Protesto"), String)
        End With

        strWriter.WriteLine(strTipoLetra & _
                            strTipoMoneda & _
                            strCuentaBT & _
                            strTipoInters & _
                            strTasaClient & _
                            strFechaVenc & _
                            strImporte & _
                            strNroCedente & _
                            strNombreAcep & _
                            strTipoVia & _
                            strDirecAcep & _
                            strNroDirecc & _
                            strNomCiudad & _
                            strTipDocIde & _
                            strNumDocIde & _
                            strTipoAval & _
                            strProtesto)

      Next


      strWriter.Close()
      strWriter = Nothing

      Exit Sub

    Catch ex As Exception
      sError = ex.Message

    End Try


  End Sub

  Public Sub GenerateFileBCP(ByVal strCod_Lote As String, ByVal strNomFile As String)

    Dim clsObj As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
    Dim clsDT As New DataTable

    ' Objetos de archivos
    Dim strFile As Stream
    Dim strWriter As StreamWriter
    Dim sFilePath As String = sRuta & "BANCO CREDITO\" & strNomFile & ".txt"

    Dim NomCliente As String
    Dim ApePaterno As String
    Dim ApeMaterno As String
    Dim strTipDocIde As String
    Dim strNumDocIde As String
    Dim strNroLetra As String
    Dim strFechaVenc As String
    Dim strImporte As String

    Dim intFila As Integer

    Dim lobjParametros() As Object = {"pCodigoPlanilla", strCod_Lote}


    Try

      sError = ""

      If File.Exists(sFilePath) = True Then
        File.Delete(sFilePath)
      End If

      clsDT = clsObj.ObtenerDataTable("usp_LetrasBancoBCP_Lst", lobjParametros)

      strFile = File.OpenWrite(sFilePath)
      strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

      For intFila = 0 To clsDT.Rows.Count - 1


        With clsDT.Rows(intFila)

          If CType(.Item("TI_NATU_JURI"), String) = "J" Then
            NomCliente = LSet(fsLeter(CType(.Item("NombreAcep"), String)), 72)
            ApePaterno = LSet("", 24)
            ApeMaterno = LSet("", 24)
          Else
            NomCliente = LSet(fsLeter(CType(.Item("NomCliente"), String)), 72)
            ApePaterno = LSet(fsLeter(CType(.Item("ApePaterno"), String)), 24)
            ApeMaterno = LSet(fsLeter(CType(.Item("ApeMaterno"), String)), 24)
          End If

          strTipDocIde = CType(.Item("TipDocIde"), String)

          strNumDocIde = LSet(CType(.Item("NumDocIde"), String), 15)
          strNroLetra = LSet(fsChar(CType(.Item("NroCedente"), String)), 12)
          strFechaVenc = CType(.Item("FechaVenc"), String)
          strFechaVenc = Right(strFechaVenc, 2) & Mid(strFechaVenc, 5, 2) & Mid(strFechaVenc, 3, 2)
          strImporte = RSet(CType(.Item("Importe"), String), 14)

        End With

        strWriter.WriteLine(NomCliente & _
                            ApePaterno & _
                            ApeMaterno & _
                            strTipDocIde & _
                            strNumDocIde & _
                            strNroLetra & _
                            strFechaVenc & _
                            strImporte)

      Next


      strWriter.Close()
      strWriter = Nothing

      Exit Sub

    Catch ex As Exception
      sError = ex.Message

    End Try

  End Sub


  Private Function fsLeter(ByVal sValor As Object) As String
    Dim sCade As String = ""
    Dim sDato As String = ""

    If (CType(sValor, String) = "") Then
      fsLeter = ""
      Exit Function
    End If

    sCade = UCase(CStr(sValor))

    sDato = Replace(sCade, "Ñ", "N")
    sDato = Replace(sDato, "Á", "A")
    sDato = Replace(sDato, "É", "E")
    sDato = Replace(sDato, "Í", "I")
    sDato = Replace(sDato, "Ó", "O")
    sDato = Replace(sDato, "Ú", "U")

    sDato = Replace(sDato, "À", "A")
    sDato = Replace(sDato, "È", "E")
    sDato = Replace(sDato, "Ì", "I")
    sDato = Replace(sDato, "Ò", "O")
    sDato = Replace(sDato, "Ú", "U")
    sDato = Replace(sDato, "´´", "")
    sDato = Replace(sDato, "´", "'")


    fsLeter = CStr(sDato)
  End Function


  Private Function fsChar(ByVal sValor As Object) As String
    Dim sCade As String = ""
    Dim sDato As String = ""

    If (CType(sValor, String) = "") Then
      fsChar = ""
      Exit Function
    End If

    sCade = UCase(CStr(sValor))

    sDato = Replace(sCade, "-", "")

    fsChar = CStr(sDato)
  End Function

  Private Function fsNumDec(ByVal sValor As String) As String
    Dim sCade As String = ""
    Dim Pos As Integer

    If InStr(1, CStr(sValor), ".") > 0 Then
      Pos = InStr(1, CStr(sValor), ".") - 1
      sCade = "000000000000000" & Left(sValor, Pos) & Right(sValor, 2)
    End If

    fsNumDec = Right(sCade, 15)
  End Function

  Private Function fsNumInt(ByVal sValor As Object, ByVal iLong As Integer) As String
    Dim sCade As String = ""
    Dim sForm As String = ""

    sForm = StrDup(iLong, "0")
    sCade = Format(sValor, sForm)

    fsNumInt = sCade
  End Function


End Class
