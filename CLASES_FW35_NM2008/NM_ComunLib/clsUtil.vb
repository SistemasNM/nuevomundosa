Imports System.Xml
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Text
Imports NM.AccesoDatos
Imports Microsoft.Win32

Public Class clsUtil


#Region "VARIABLES"

    Private _objConexion As AccesoDatosSQLServer

#End Region
#Region " Definicion de Metodos "
    Function GeneraXml(ByVal dtDatos As DataTable) As String
        Dim xmlDOM As New XmlDocument
        Dim nodo, nodoChild As XmlElement
        With xmlDOM
            .Load(New StringReader("<root></root>"))
            For i As Integer = 0 To dtDatos.Rows.Count - 1
                nodo = .CreateElement(dtDatos.TableName)
                For j As Integer = 0 To dtDatos.Columns.Count - 1
                    If Not IsDBNull(dtDatos.Rows(i)(j)) Then
                        nodoChild = .CreateElement(dtDatos.Columns(j).ColumnName)
                        nodoChild.InnerText = Trim(CType(dtDatos.Rows(i)(j), String))
                        nodo.AppendChild(nodoChild)
                    End If
                Next j
                .DocumentElement.AppendChild(nodo)
            Next i

            Return EncodeXML(.OuterXml())
        End With
    End Function

    Function EncodeXML_Ant(ByVal Texto As String) As String
        Dim trama() As String = {"°", "*", "Ø", "¹", "¨", "´", "¹", "í", "á", "ú", "ó", "é", "&", "Ñ", "ñ", UCase("í"), UCase("á"), UCase("ú"), UCase("ó"), UCase("é"), "&"}
        Dim objAscii As Encoding = Encoding.ASCII
        For Each car As String In trama
            Dim encodedBytes As [Byte]() = objAscii.GetBytes(car)
            Texto = Replace(Texto, car, "&#" & encodedBytes(0).ToString & ";")
        Next
        Return Texto
    End Function

    Function EncodeXML(ByVal Texto As String) As String
        Dim tramaCar() As String = {"—", "¡", "¢", "£", "¤", "¥", "¦", "§", "¨", "©", "ª", "«", "¬", "®", "¯", "°", "±", "²", "³", _
                                    "´", "µ", "¶", "·", "¸", "¹", "º", "»", "¼", "½", "¾", "¿", "À", "Á", "Â", "Ã", "Ä", "Å", "Æ", "Ç", _
                                    "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", "×", "Ø", "Ù", "Ú", "Û", _
                                    "Ü", "Ý", "Þ", "ß", "à", "á", "â", "ã", "ä", "å", "æ", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", _
                                    "ð", "ñ", "ò", "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü", "ý", "þ", "ÿ"}

        Dim tramaVal() As String = {"151", "161", "162", "163", "164", "165", "166", "167", "168", "169", "170", "171", "172", "174", "175", "176", "177", "178", "179", _
                                    "180", "181", "182", "183", "184", "185", "186", "187", "188", "189", "190", "191", "192", "193", "194", "195", "196", "197", "198", "199", _
                                    "200", "201", "202", "203", "204", "205", "206", "207", "208", "209", "210", "211", "212", "213", "214", "215", "216", "217", "218", "219", _
                                    "220", "221", "222", "223", "224", "225", "226", "227", "228", "229", "230", "231", "232", "233", "234", "235", "236", "237", "238", "239", _
                                    "240", "241", "242", "243", "244", "245", "246", "247", "248", "249", "250", "251", "252", "253", "254", "255"}
        Dim iPos As Integer
        Dim sVal As String

        For Each car As String In tramaCar
            sVal = tramaVal(iPos)
            Texto = Replace(Texto, car, "&#" & sVal & ";")
            iPos = iPos + 1
        Next
        Return Texto
    End Function


    Function ReemplazarTexto(ByVal Texto As String) As String
        Dim trama() As String = {"°", "*", "Ø", "¹", "¨", "í", "i", "á", "a", "ú", "u", "ó", "o", "é", "e", "Ñ", "N", "ñ", "n", UCase("í"), "I", UCase("á"), "A", UCase("ú"), "U", UCase("ó"), "O", UCase("é"), "E"}
        Dim objAscii As Encoding = Encoding.ASCII
        Dim i As Integer
        Dim lstrTexto1 As String = ""
        Dim lstrTexto2 As String = ""

        For i = 0 To ((UBound(trama, 1) - 1) / 2)
            lstrTexto1 = trama(2 * i)
            lstrTexto2 = trama((2 * i) + 1)
            Texto = Replace(Texto, lstrTexto1, lstrTexto2)
        Next
        Return Texto
    End Function
    Public Function DatosAlerta_Obtener(ByVal strCodigoTabla As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        Dim objParametros() = {"var_CodigoTabla", strCodigoTabla}
        Try
            Return _objConexion.ObtenerDataTable("usp_FCO_Parametros_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function DatosTablas_Obtener(ByVal strCodigoTabla As String) As DataTable
        _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim objParametros() = {"var_CodigoTabla", strCodigoTabla}
        Try
            Return _objConexion.ObtenerDataTable("usp_ADM_Tablas_Obtener", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Padding(ByVal chrCaracter As String, ByVal intVeces As Int16)
        Dim strRetorno As String = ""
        For x As Int16 = 1 To intVeces
            strRetorno = strRetorno & chrCaracter
        Next
        Return strRetorno
    End Function

    Public Function GetFile(ByVal Ruta As String) As String
        ' Obtener el formato del detalle
        Dim formato As String
        Dim archivoLectura As Integer = FreeFile()
        FileOpen(archivoLectura, Ruta, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
        Do While Not EOF(archivoLectura)
            formato += LineInput(archivoLectura) & vbCrLf
        Loop
        FileClose(archivoLectura)
        Return formato
    End Function

    Public Sub PutFile(ByVal Ruta As String, ByVal strContenido As String)
        Dim ptrArchivo As Integer = FreeFile()
        'If File.Exists(Ruta) = False Then
        '    File.Create(Ruta)
        'End If
        FileOpen(ptrArchivo, Ruta, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
        Print(ptrArchivo, strContenido)
        FileClose(ptrArchivo)
    End Sub

    Public Function ClaveRegistro_Obtener(ByVal strClave As String, ByVal strCampo As String) As String
        Try
            Dim strRetorno As String = CType(Registry.LocalMachine.OpenSubKey("Software\NuevoMundo\" & strClave).GetValue(strCampo), String)
            Return strRetorno
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

End Class
