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


    Public Sub GenerateFileINT(ByVal strCod_Lote As String, ByVal strNomFile As String)

        Dim clsObj As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        Dim clsDT As New DataTable

        ' Objetos de archivos
        Dim strFile As Stream
        Dim strWriter As StreamWriter
        Dim sFilePath As String = sRuta & "BANCO INTERBANK\" & strNomFile & ".txt"

        Dim strCtaDestino As String
        Dim strTipoProducto As String
        Dim strTipoMoneda As String
        Dim strTipoDocumento As String
        Dim StrProtDocumento As String
        Dim strCobroInteres As String
        Dim strNroDocu As String
        Dim strTipDocAcep As String
        Dim strNroDocAcep As String
        Dim strNomCliente1 As String
        Dim strNomCliente2 As String
        Dim strApePaterno As String
        Dim strApeMaterno As String
        Dim strAreaPostal As String
        Dim strImporteTotal As String
        Dim strFechaVenc As String
        Dim strNroCuota As String
        Dim strFechaCuota As String
        Dim strImporteCuota As String
        Dim strDirecAcep As String
        Dim strDistAcep As String
        Dim strProvAcep As String
        Dim strDptoAcep As String
        Dim intFila As Integer

        Dim lobjParametros() As Object = {"pCodigoPlanilla", strCod_Lote}
        Try
            sError = ""
            If File.Exists(sFilePath) = True Then
                File.Delete(sFilePath)
            End If
            clsDT = clsObj.ObtenerDataTable("usp_LetrasBancoINT_Lst", lobjParametros)
            strFile = File.OpenWrite(sFilePath)
            strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)
            For intFila = 0 To clsDT.Rows.Count - 1
                With clsDT.Rows(intFila)
                    strCtaDestino = CType(.Item("CtaDestino"), String)
                    strTipoProducto = CType(.Item("TipoProducto"), String)
                    strTipoMoneda = CType(.Item("TipoMoneda"), String)
                    strTipoDocumento = CType(.Item("TipoDocumento"), String)
                    StrProtDocumento = CType(.Item("ProtDocumento"), String)
                    strCobroInteres = CType(.Item("CobroInteres"), String)
                    strNroDocu = LSet(CType(.Item("NroDocu"), String), 10)
                    strTipDocAcep = CType(.Item("TipDocAcep"), String)
                    strNroDocAcep = LSet(CType(.Item("NroDocAcep"), String), 15)
                    strNomCliente1 = LSet(CType(.Item("NomCliente1"), String), 40)
                    strNomCliente2 = LSet(CType(.Item("NomCliente2"), String), 20)
                    strApePaterno = LSet(CType(.Item("ApePaterno"), String), 20)
                    strApeMaterno = LSet(CType(.Item("ApeMaterno"), String), 20)
                    strAreaPostal = LSet(CType(.Item("AreaPostal"), String), 40)
                    strImporteTotal = fsNumDecInt(CType(.Item("ImporteTotal"), String))
                    strFechaVenc = CType(.Item("FechaVenc"), String)
                    strNroCuota = LSet(CType(.Item("NroCuota"), String), 2)
                    strFechaCuota = LSet(CType(.Item("FechaCuota"), String), 10)
                    strImporteCuota = fsNumDecInt(CType(.Item("ImporteCuota"), String))
                    strDirecAcep = LSet(fsLeter(CType(.Item("DirecAcep"), String)), 70)
                    strDistAcep = LSet(fsLeter(CType(.Item("DistAcep"), String)), 40)
                    strProvAcep = LSet(fsLeter(CType(.Item("ProvAcep"), String)), 40)
                    strDptoAcep = LSet(fsLeter(CType(.Item("DptoAcep"), String)), 40)

                End With

                If intFila = 0 Then
                    strWriter.WriteLine("       " + strCtaDestino & _
                                strTipoProducto & _
                                strTipoMoneda & _
                                strTipoDocumento & _
                                StrProtDocumento & _
                                strCobroInteres)

                    strWriter.WriteLine(strNroDocu & _
                                       strTipDocAcep & _
                                       strNroDocAcep & _
                                       strNomCliente1 & _
                                       strNomCliente2 & _
                                       strApePaterno & _
                                       strApeMaterno & _
                                       strAreaPostal & _
                                       strImporteTotal & _
                                       strFechaVenc & _
                                       strNroCuota & _
                                       strFechaCuota & _
                                       strImporteCuota & _
                                       strDirecAcep & _
                                       strDistAcep & _
                                       strProvAcep & _
                                       strDptoAcep)
                Else
                    strWriter.WriteLine(strNroDocu & _
                                        strTipDocAcep & _
                                        strNroDocAcep & _
                                        strNomCliente1 & _
                                        strNomCliente2 & _
                                        strApePaterno & _
                                        strApeMaterno & _
                                        strAreaPostal & _
                                        strImporteTotal & _
                                        strFechaVenc & _
                                        strNroCuota & _
                                        strFechaCuota & _
                                        strImporteCuota & _
                                        strDirecAcep & _
                                        strDistAcep & _
                                        strProvAcep & _
                                        strDptoAcep)
                End If
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
    Public Sub GenerateFileBIF(ByVal strCod_Lote As String, ByVal strNomFile As String)

        Dim clsObj As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
        Dim clsDT As New DataTable

        ' Objetos de archivos
        Dim strFile As Stream
        Dim strWriter As StreamWriter
        'Dim sFilePath As String = sRuta & "BANCO BANBIF\" & strNomFile & ".txt"
        Dim sFilePath As String = sRuta & "BANCO CREDITO\" & strNomFile & ".txt"

        Dim NomCliente As String
        Dim strTipDocIde As String
        Dim strNumDocIde As String
        Dim StrTipoMoneda As String
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

            clsDT = clsObj.ObtenerDataTable("usp_LetrasBancoBIF_Lst", lobjParametros)

            strFile = File.OpenWrite(sFilePath)
            strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

            For intFila = 0 To clsDT.Rows.Count - 1


                With clsDT.Rows(intFila)
                    strTipDocIde = CType(.Item("TipDocIde"), String)
                    strNumDocIde = LSet(CType(.Item("NumDocIde"), String), 15)
                    NomCliente = LSet(fsLeter(CType(.Item("NombreAcep"), String)), 60)
                    strNroLetra = LSet(fsChar(CType(.Item("NroCedente"), String)), 16)
                    StrTipoMoneda = LSet(fsChar(CType(.Item("TipoMoneda"), String)), 3)
                    strImporte = fsNumDecBif(CType(.Item("Importe"), String))
                    strFechaVenc = CType(.Item("FechaVenc"), String)


                End With

                strWriter.WriteLine(strTipDocIde & _
                                    strNumDocIde & _
                                    NomCliente & _
                                    strNroLetra & _
                                    StrTipoMoneda & _
                                    strImporte & _
                                    strFechaVenc)

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
        sDato = Replace(sDato, "º", "o")
        sDato = Replace(sDato, "Ñ", "N")
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
    Private Function fsNumDecInt(ByVal sValor As String) As String
        Dim sCade As String = ""
        Dim Pos As Integer

        If InStr(1, CStr(sValor), ".") > 0 Then
            Pos = InStr(1, CStr(sValor), ".") - 1
            sCade = "0000000000000000" & Left(sValor, Pos) & Right(sValor, 2)
        End If

        fsNumDecInt = Right(sCade, 16)
    End Function
    Private Function fsNumDecBif(ByVal sValor As String) As String
        Dim sCade As String = ""
        Dim Pos As Integer

        If InStr(1, CStr(sValor), ".") > 0 Then
            Pos = InStr(1, CStr(sValor), ".") - 1
            sCade = "0000000000" & Left(sValor, Pos) & Right(sValor, 2)
        End If

        fsNumDecBif = Right(sCade, 10)
    End Function

  Private Function fsNumInt(ByVal sValor As Object, ByVal iLong As Integer) As String
    Dim sCade As String = ""
    Dim sForm As String = ""

    sForm = StrDup(iLong, "0")
    sCade = Format(sValor, sForm)

    fsNumInt = sCade
  End Function


End Class
