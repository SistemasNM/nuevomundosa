
Imports NM.AccesoDatos
Imports NM_General
Imports NuevoMundo.Generales
Imports System.Data
Imports System.IO
Imports Scripting
Imports System.Data.OleDb

Namespace Tesoreria
    Public Class Finanzas
        Private sRuta As String
        Private sError As String
#Region "Variables"
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer
#End Region

#Region "Propiedades"
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property
#End Region

#Region "METODOS Y FUNCIONES"

#Region "INTERFACE IMPORTACION DIETARIOS"
        Function GetDataFromFile(ByVal pstrCodigoBanco As String, _
            ByVal pstrTipoMovimiento As String, ByVal pstrRuta As String, _
            Optional ByVal pstrHoja As String = "") As DataTable
            Dim dtbDatos As New DataTable
            Try
                'Creando schema
                If System.IO.File.Exists(pstrRuta) AndAlso pstrTipoMovimiento <> "" AndAlso pstrCodigoBanco <> "" Then
                    'CAMPOS DE TRABAJO
                    dtbDatos.Columns.Add("var_IDMovimiento", GetType(Int16))
                    dtbDatos.Columns.Add("var_Tipo", GetType(String))
                    dtbDatos.Columns.Add("int_EstadoRegistro", GetType(Int16))
                    dtbDatos.Columns.Add("var_LetraBanco", GetType(String))
                    dtbDatos.Columns.Add("var_EstadoActual", GetType(String))
                    dtbDatos.Columns.Add("var_EstadoNuevo", GetType(String))
                    dtbDatos.Columns.Add("var_NombreCliente", GetType(String))
                    dtbDatos.Columns.Add("var_MonedaLetra", GetType(String))
                    dtbDatos.Columns.Add("num_Saldo", GetType(Double))
                    'CAMPOS DE FILTRO PARA EL ESQUEMA
                    dtbDatos.Columns.Add("var_CodigoEmpresa", GetType(String))
                    dtbDatos.Columns.Add("var_CodigoBanco", GetType(String))
                    'CAMPOS DEFINIDOS PARA EL PROCESO
                    dtbDatos.Columns.Add("var_NumeroUnico", GetType(String))
                    dtbDatos.Columns.Add("var_NumeroReferencia", GetType(String))
                    dtbDatos.Columns.Add("var_CodigoLetra", GetType(String))
                    dtbDatos.Columns.Add("var_NumeroRUC", GetType(String))
                    dtbDatos.Columns.Add("dtm_FechaVencimiento", GetType(String))
                    dtbDatos.Columns.Add("num_MontoImporte", GetType(Double))
                    dtbDatos.Columns.Add("var_Estado", GetType(String))
                    dtbDatos.Columns.Add("num_Interes", GetType(Double))
                    dtbDatos.Columns.Add("num_MontoProtesto", GetType(Double))
                    dtbDatos.Columns.Add("dtm_FechaProtesto", GetType(String))
                    dtbDatos.Columns.Add("num_MontoComision", GetType(Double))
                    dtbDatos.Columns.Add("num_MontoPortes", GetType(Double))
                    dtbDatos.Columns.Add("dtm_FechaMovimiento", GetType(String))
                    dtbDatos.Columns.Add("var_TipoGrupo", GetType(String))

                    Dim sin_IDPrimary As Int16 = 0
                    If pstrCodigoBanco = "08" Then
                        sin_IDPrimary = 1
                    End If

                    Dim dtcPrimary(sin_IDPrimary) As DataColumn
                    dtcPrimary(0) = dtbDatos.Columns("var_NumeroUnico")
                    If pstrCodigoBanco = "08" Then
                        dtcPrimary(1) = dtbDatos.Columns("var_NumeroReferencia")
                    End If
                    dtbDatos.PrimaryKey = dtcPrimary
                    Select Case pstrCodigoBanco
                        Case "01"
                            dtbDatos = GetDataBCP_01(pstrRuta, pstrCodigoBanco, dtbDatos)
                        Case "08"
                            dtbDatos = GetDataBIF_01(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                        Case "04"
                            dtbDatos = GetDataWIE(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                        Case "07"
                            dtbDatos = GetDataCiti(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos, pstrHoja)
                        Case "02"
                            dtbDatos = GetDataCONT(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                        Case "03"
                            dtbDatos = GetDataINT(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, pstrHoja, dtbDatos)
                        Case "06"
                            dtbDatos = GetDataSUD(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                        Case "15"
                            dtbDatos = GetDataSCO(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                        Case "16"
                            dtbDatos = GetDataHSBC(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                    End Select
                    dtbDatos.AcceptChanges()
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbDatos
        End Function

        Function GetDataINT(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal pstrHoja As String, ByVal dtbDatos As DataTable) As DataTable
            'Dim fp As StreamReader = New StreamReader(pstrRuta)
            Dim fs As FileSystemObject
            Dim ts As TextStream
            Dim dtbLetras As DataTable
            fs = New FileSystemObject
            ts = fs.OpenTextFile(pstrRuta, IOMode.ForReading, False)

            Dim strLinea As String
            Try
                strLinea = ts.ReadLine
                strLinea = ts.ReadLine
                Do While ts.AtEndOfStream = False
                    strLinea = ts.ReadLine
                    Dim arrDatos() As String = strLinea.Split(",")
                    Dim ldrRow As DataRow = dtbDatos.NewRow

                    'ldrRow("var_IDMovimiento") = arrDatos(3)
                    'ldrRow("var_Tipo") = "1"
                    ldrRow("int_EstadoRegistro") = "0"
                    ldrRow("var_LetraBanco") = arrDatos(6)
                    ldrRow("var_EstadoActual") = ""
                    ldrRow("var_EstadoNuevo") = ""
                    ldrRow("var_NombreCliente") = arrDatos(7)
                    ldrRow("num_Saldo") = "0"
                    'CAMPOS DE FILTRO PARA EL ESQUEMA
                    ldrRow("var_CodigoEmpresa") = "01"
                    ldrRow("var_CodigoBanco") = pstrCodigoBanco
                    'CAMPOS DEFINIDOS PARA EL PROCESO
                    ldrRow("var_NumeroUnico") = arrDatos(5)
                    ldrRow("var_NumeroReferencia") = ""
                    ldrRow("var_CodigoLetra") = ""
                    ldrRow("var_NumeroRUC") = Mid(arrDatos(27), 5)
                    ldrRow("dtm_FechaVencimiento") = arrDatos(8)
                    Select Case arrDatos(12)
                        Case "1"
                            ldrRow("num_MontoImporte") = arrDatos(13)
                        Case "2"
                            ldrRow("num_MontoImporte") = arrDatos(16)
                        Case "0"
                            If pstrTipoMovimiento = "COG" Then
                                ldrRow("num_MontoImporte") = arrDatos(13)
                            Else
                                ldrRow("num_MontoImporte") = arrDatos(21)
                            End If
                    End Select
                    ldrRow("var_Estado") = arrDatos(12)
                    Dim numInteresCompensatorio As Double = 0
                    Dim numInteresMoratorio As Double = 0
                    Dim numInteres As Double = 0
                    If Not arrDatos(19) Is Nothing AndAlso arrDatos(19) <> "" Then
                        numInteresMoratorio = arrDatos(19)
                    End If
                    If Not arrDatos(20) Is Nothing AndAlso arrDatos(20) <> "" Then
                        numInteresCompensatorio = arrDatos(20)
                    End If
                    If Not arrDatos(15) Is Nothing AndAlso arrDatos(15) <> "" Then
                        numInteres = arrDatos(15)
                    End If
                    ldrRow("num_Interes") = numInteres + numInteresMoratorio + numInteresCompensatorio
                    ldrRow("num_MontoProtesto") = 0
                    ldrRow("dtm_FechaProtesto") = ""
                    ldrRow("num_MontoComision") = arrDatos(22)
                    ldrRow("num_MontoPortes") = arrDatos(25)
                    ldrRow("dtm_FechaMovimiento") = arrDatos(4)
                    ldrRow("var_TipoGrupo") = "1"
                    Select Case arrDatos(12)
                        Case "0"
                            dtbLetras = ObtenerLetrasPeriodo(ldrRow("num_MontoImporte"), ldrRow("dtm_FechaVencimiento"), pstrCodigoBanco)
                        Case "1", "2"
                            dtbLetras = ObtenerLetrasIDBanco(ldrRow("var_NumeroReferencia"), ldrRow("var_NumeroUnico"), pstrCodigoBanco)
                    End Select
                    If dtbLetras.Rows.Count = 1 Then
                        ldrRow("var_CodigoLetra") = dtbLetras.Rows(0)("NU_LETR_CLIE")
                        ldrRow("var_NumeroRUC") = dtbLetras.Rows(0)("CO_CLIE")
                        ldrRow("var_NombreCliente") = dtbLetras.Rows(0)("NO_CLIE")
                        ldrRow("var_EstadoActual") = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                        ldrRow("var_MonedaLetra") = dtbLetras.Rows(0)("CO_MONE")
                        ldrRow("num_Saldo") = dtbLetras.Rows(0)("num_Saldo")
                        ldrRow("int_EstadoRegistro") = 0
                    Else
                        ldrRow("var_MonedaLetra") = ""
                        ldrRow("var_CodigoLetra") = ""
                        ldrRow("var_NumeroRUC") = Mid(arrDatos(27), 5)
                        ldrRow("var_EstadoActual") = ""
                        ldrRow("num_Saldo") = 0
                        If dtbLetras.Rows.Count = 0 Then
                            'SI NO SE ENCONTRO EL NUMERO UNICO
                            ldrRow("int_EstadoRegistro") = 2
                        ElseIf dtbLetras.Rows.Count > 1 Then
                            ldrRow("int_EstadoRegistro") = 1
                        End If
                    End If

                    dtbDatos.Rows.Add(ldrRow)
                Loop
                dtbDatos.AcceptChanges()
            Catch ex As Exception
                Throw ex
            Finally
                ts.Close()
                fs = Nothing
            End Try

            Return dtbDatos
        End Function

        Function GetDataCONT(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            'Dim fp As StreamReader = New StreamReader(pstrRuta)
            Dim fs As FileSystemObject
            Dim ts As TextStream
            Dim dtbLetras As DataTable
            fs = New FileSystemObject
            ts = fs.OpenTextFile(pstrRuta, IOMode.ForReading, False)
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 13, 4) & Mid(strFileName, 17, 2) & Mid(strFileName, 19, 2)
            Dim strTipoOperacion As String = Right(strFileName, 3)

            Dim strLinea As String
            Try
                'strLinea = ts.ReadLine
                Do While ts.AtEndOfStream = False
                    strLinea = ts.ReadLine
                    If (pstrTipoMovimiento = "DES" And InStr(strLinea.Substring(41, 31), "DESCONTADAS") > 0) _
                    OrElse (pstrTipoMovimiento = "COG" And InStr(strLinea.Substring(41, 31), "GARANTIA") > 0) Then
                        If strFecha = strLinea.Substring(170, 8) _
                                AndAlso ((strLinea.Substring(96, 3) = "VIG" And strTipoOperacion = "ING") _
                                OrElse (strLinea.Substring(96, 3) <> "VIG" And strTipoOperacion <> "ING")) Then
                            'Dim arrDatos() As String = strLinea.Split(",")
                            Dim ldrRow As DataRow = dtbDatos.NewRow

                            'ldrRow("var_IDMovimiento") = arrDatos(3)
                            'ldrRow("var_Tipo") = "1"
                            ldrRow("int_EstadoRegistro") = "0"
                            ldrRow("var_LetraBanco") = strLinea.Substring(190, 10)
                            ldrRow("var_EstadoActual") = ""
                            ldrRow("var_EstadoNuevo") = ""
                            ldrRow("var_NombreCliente") = strLinea.Substring(113, 35)
                            ldrRow("num_Saldo") = "0"
                            'CAMPOS DE FILTRO PARA EL ESQUEMA
                            ldrRow("var_CodigoEmpresa") = "01"
                            ldrRow("var_CodigoBanco") = pstrCodigoBanco
                            'CAMPOS DEFINIDOS PARA EL PROCESO
                            ldrRow("var_NumeroUnico") = strLinea.Substring(76, 10)
                            ldrRow("var_NumeroReferencia") = ""
                            ldrRow("var_CodigoLetra") = ""
                            ldrRow("var_NumeroRUC") = strLinea.Substring(179, 11)
                            ldrRow("dtm_FechaVencimiento") = strLinea.Substring(92, 2) & "/" & strLinea.Substring(90, 2) & "/" & strLinea.Substring(86, 4)
                            ldrRow("num_MontoImporte") = CDbl(strLinea.Substring(99, 14)) / 100.0
                            ldrRow("var_Estado") = strLinea.Substring(96, 3)
                            If pstrTipoMovimiento = "COG" And InStr(strLinea.Substring(41, 31), "GARANTIA") > 0 Then
                                ldrRow("num_Interes") = 0.0
                            Else
                                ldrRow("num_Interes") = CDbl(strLinea.Substring(234, strLinea.Length - 234)) / 100.0
                            End If
                            ldrRow("num_MontoProtesto") = 0
                            ldrRow("dtm_FechaProtesto") = ""
                            If pstrTipoMovimiento = "COG" And InStr(strLinea.Substring(41, 31), "GARANTIA") > 0 Then
                                ldrRow("num_MontoComision") = 0.0
                            Else
                                ldrRow("num_MontoComision") = CDbl(strLinea.Substring(220, 14)) / 100.0
                            End If
                            If pstrTipoMovimiento = "COG" And InStr(strLinea.Substring(41, 31), "GARANTIA") > 0 Then
                                ldrRow("num_MontoPortes") = 0.0
                            Else
                                ldrRow("num_MontoPortes") = CDbl(strLinea.Substring(206, 14)) / 100.0
                            End If
                            ldrRow("num_MontoPortes") = CDbl(strLinea.Substring(206, 14)) / 100.0
                            ldrRow("dtm_FechaMovimiento") = strLinea.Substring(176, 2) & "/" & strLinea.Substring(174, 2) & "/" & strLinea.Substring(170, 4) 'strLinea.Substring(170, 8)
                            ldrRow("var_TipoGrupo") = "1"
                            Select Case strLinea.Substring(96, 3)
                                Case "VIG"
                                    dtbLetras = ObtenerLetrasPeriodo(ldrRow("num_MontoImporte"), ldrRow("dtm_FechaVencimiento"), pstrCodigoBanco)
                                Case Else
                                    dtbLetras = ObtenerLetrasIDBanco(ldrRow("var_NumeroReferencia"), ldrRow("var_NumeroUnico"), pstrCodigoBanco)
                            End Select
                            If dtbLetras.Rows.Count = 1 Then
                                ldrRow("var_CodigoLetra") = dtbLetras.Rows(0)("NU_LETR_CLIE")
                                ldrRow("var_NumeroRUC") = dtbLetras.Rows(0)("CO_CLIE")
                                ldrRow("var_NombreCliente") = dtbLetras.Rows(0)("NO_CLIE")
                                ldrRow("var_EstadoActual") = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                                ldrRow("var_MonedaLetra") = dtbLetras.Rows(0)("CO_MONE")
                                ldrRow("num_Saldo") = dtbLetras.Rows(0)("num_Saldo")
                                ldrRow("int_EstadoRegistro") = 0
                            Else
                                ldrRow("var_MonedaLetra") = ""
                                ldrRow("var_CodigoLetra") = ""
                                ldrRow("var_NumeroRUC") = strLinea.Substring(179, 11)
                                ldrRow("var_EstadoActual") = ""
                                ldrRow("num_Saldo") = 0
                                If dtbLetras.Rows.Count = 0 Then
                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                    ldrRow("int_EstadoRegistro") = 2
                                ElseIf dtbLetras.Rows.Count > 1 Then
                                    ldrRow("int_EstadoRegistro") = 1
                                End If
                            End If

                            dtbDatos.Rows.Add(ldrRow)
                        End If
                    End If


                Loop
                dtbDatos.AcceptChanges()
            Catch ex As Exception
                Throw ex
            Finally
                ts.Close()
                fs = Nothing
            End Try

            Return dtbDatos
        End Function

        Function GetDataHSBC(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable, Optional ByVal pstrHoja As String = "") As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [LETRAS CITIBANK$]", xlconn)
            Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)

            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    Dim strFechaID As String = ""
                    If IsDBNull(xlReader.Item(0)) = False Then
                        If Right(pstrHoja, 3) = "ING" Then
                            Dim strFechaIni As String = "01/01/20" & CStr(xlReader.Item(1)).Substring(1, 2)
                            Dim strDiaAnno As String = CStr(xlReader.Item(1)).Substring(3, 3)
                            strFechaID = Format(DateAdd(DateInterval.Day, CInt(strDiaAnno) - 1, CDate(strFechaIni)), "dd/MM/yyyy")
                        End If
                        If (Right(pstrHoja, 3) = "PAG" AndAlso IsDBNull(xlReader.Item(9)) = False AndAlso xlReader.Item(9) = strFecha) OrElse _
                                                (Right(pstrHoja, 3) = "ING" AndAlso strFechaID = strFecha) Then
                            Dim strCodigoCliente As String = "", strNombreCliente As String = xlReader.Item(2), dblImporte As Double
                            Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0
                            Dim dblSaldo As Double = 0, strMoneda As String = ""
                            '-------------------------------------------------
                            '(INICIO) DATOS DEL MOVIMIENTO 
                            '-------------------------------------------------
                            dtrDatos = dtbDatos.NewRow
                            dtrDatos("var_CodigoEmpresa") = "01"
                            dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                            dtrDatos("var_NumeroUnico") = CStr(xlReader.Item(1))
                            dtrDatos("var_NumeroReferencia") = ""
                            dtrDatos("dtm_FechaVencimiento") = Format(xlReader.Item(8), "dd/MM/yyyy")
                            '-------------------------------------------------
                            '(FINAL) DATOS DEL MOVIMIENTO 
                            '-------------------------------------------------

                            '-------------------------------------------------
                            '(INICIO) UBICANDO LA LETRA
                            '-------------------------------------------------
                            dtbLetras = New DataTable
                            If Right(pstrHoja, 3) = "ING" Then
                                dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(12), "0.00"))
                                dtbLetras = ObtenerLetrasPeriodo(dtrDatos("num_MontoImporte"), dtrDatos("dtm_FechaVencimiento"), pstrCodigoBanco)
                            ElseIf Right(pstrHoja, 3) = "PAG" Then
                                dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(12), "0.00"))

                                If UCase(Trim(CStr(xlReader.Item(10)))) = "RENOVADA" Then
                                    Dim dblImporteRen As Double = 0
                                    Dim xlconnRen As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
                                    Dim strAuxiliar As String = Left(CStr(xlReader.Item(0)), 6)
                                    Dim strQuery As String = "select * from [LETRAS CITIBANK$]"
                                    Dim xlcommRen As New OleDbCommand(strQuery, xlconnRen)
                                    xlconnRen.Open()
                                    Dim xlReaderRen As OleDbDataReader = xlcommRen.ExecuteReader
                                    While xlReaderRen.Read
                                        If Left(CStr(xlReaderRen.Item(0)), 6) = strAuxiliar AndAlso UCase(Trim(CStr(xlReaderRen.Item(10)))) <> "RENOVADA" Then
                                            dblImporteRen = CDbl(Format(xlReaderRen.Item(12), "0.00"))
                                            Exit While
                                        End If
                                    End While
                                    xlReaderRen.Close()
                                    dtrDatos("num_MontoImporte") = Format(CDbl(Format(xlReader.Item(12), "0.00")) - dblImporteRen, "0.00")
                                End If
                                dtbLetras = ObtenerLetrasIDBanco(dtrDatos("var_NumeroReferencia").ToString, dtrDatos("var_NumeroUnico").ToString, pstrCodigoBanco)
                            End If

                            If dtbLetras.Rows.Count = 1 Then
                                strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
                                strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
                                strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
                                strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                                strMoneda = dtbLetras.Rows(0)("CO_MONE")
                                dblSaldo = dtbLetras.Rows(0)("num_Saldo")
                                intEstadoRegistro = 0
                            Else
                                strLetra = ""
                                strCodigoCliente = ""
                                strNombreCliente = xlReader.Item(5)
                                strEstadoLetra = ""
                                dblSaldo = 0
                                If Right(pstrHoja, 3) = "ING" Then
                                    If dtbLetras.Rows.Count = 0 Then
                                        'SI NO SE ENCONTRO EL NUMERO UNICO
                                        intEstadoRegistro = 2
                                    ElseIf dtbLetras.Rows.Count > 1 Then
                                        intEstadoRegistro = 1
                                    End If
                                    'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                ElseIf Right(pstrHoja, 3) = "PAG" Then
                                    If dtbLetras.Rows.Count = 0 Then
                                        'SI NO SE ENCONTRO EL NUMERO UNICO
                                        intEstadoRegistro = 2
                                    ElseIf dtbLetras.Rows.Count > 1 Then
                                        intEstadoRegistro = 3
                                    End If
                                    'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                End If
                            End If
                            dtrDatos("var_LetraBanco") = xlReader.Item(0)
                            dtrDatos("var_EstadoActual") = strEstadoLetra
                            dtrDatos("var_NombreCliente") = strNombreCliente
                            dtrDatos("var_NumeroRUC") = strCodigoCliente
                            dtrDatos("int_EstadoRegistro") = intEstadoRegistro
                            dtrDatos("num_Saldo") = dblSaldo
                            dtrDatos("var_CodigoLetra") = strLetra
                            dtrDatos("var_MonedaLetra") = strMoneda
                            dtrDatos("var_Estado") = xlReader.Item(10)
                            '-------------------------------------------------
                            '(FINAL) UBICANDO LA LETRA
                            '-------------------------------------------------
                            '-------------------------------------------------
                            '(INICIO) OTROS MONTOS
                            '-------------------------------------------------

                            dtrDatos("var_TipoGrupo") = ""
                            If Right(pstrHoja, 3) = "ING" Then
                                dtrDatos("num_Interes") = xlReader.Item(15)
                                dtrDatos("num_MontoComision") = 0
                                dtrDatos("num_MontoPortes") = 0
                                dtrDatos("dtm_FechaMovimiento") = xlReader.Item(9)
                                dtrDatos("num_MontoProtesto") = 0
                                dtrDatos("dtm_FechaProtesto") = ""
                            ElseIf Right(pstrHoja, 3) = "PAG" Then
                                dtrDatos("num_Interes") = xlReader.Item(15)
                                dtrDatos("num_MontoComision") = -1 * xlReader.Item(16)
                                If IsNumeric(xlReader.Item(18)) Then
                                    dtrDatos("num_MontoPortes") = xlReader.Item(18)
                                Else
                                    dtrDatos("num_MontoPortes") = 0.0
                                End If
                                dtrDatos("dtm_FechaMovimiento") = xlReader.Item(9)
                                dtrDatos("num_MontoProtesto") = CDbl(xlReader.Item(17)) + CDbl(xlReader.Item(18))
                                dtrDatos("dtm_FechaProtesto") = xlReader.Item(9)
                            End If
                            dtbDatos.Rows.Add(dtrDatos)
                        End If
                    End If
                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function


        Function GetDataSUD(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [BWS Virtual TeleBanking$]", xlconn)
            Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)


            pstrCodigoBanco = "15"

            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    'If InStr(xlReader.Item(10), "ITF") > 0 OrElse InStr(xlReader.Item(10), "I.T.F.") > 0 Then

                    'End If
                    If (xlReader.FieldCount <= 13 AndAlso xlReader.Item(10) = strFecha) _
                    OrElse (xlReader.FieldCount > 13 AndAlso (xlReader.Item(5) = strFecha OrElse Left(xlReader.Item(5), 6) & "20" & Right(xlReader.Item(5), 2) = strFecha)) Then
                        '    If IsNumeric(xlReader.Item(1)) Then
                        Dim strCodigoCliente As String = "", strNombreCliente As String = xlReader.Item(2), dblImporte As Double
                        Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0
                        Dim dblSaldo As Double = 0, strMoneda As String = ""
                        '-------------------------------------------------
                        '(INICIO) DATOS DEL MOVIMIENTO 
                        '-------------------------------------------------
                        dtrDatos = dtbDatos.NewRow
                        dtrDatos("var_CodigoEmpresa") = "01"
                        dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                        dtrDatos("var_NumeroUnico") = CInt(xlReader.Item(1))
                        dtrDatos("var_NumeroReferencia") = IIf(Left((xlReader.Item(0)), 4) = "BSA-", Replace((xlReader.Item(0)), "BSA-", ""), "")
                        Dim strFechaVencimiento() As String = Split(xlReader.Item(4), "/")
                        Dim strAuxiliarFecha As String
                        If strFechaVencimiento(2) < 100 Then
                            strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/20" & strFechaVencimiento(2)
                        Else
                            strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/" & strFechaVencimiento(2)
                        End If
                        dtrDatos("dtm_FechaVencimiento") = strAuxiliarFecha
                        '-------------------------------------------------
                        '(FINAL) DATOS DEL MOVIMIENTO 
                        '-------------------------------------------------

                        '-------------------------------------------------
                        '(INICIO) UBICANDO LA LETRA
                        '-------------------------------------------------
                        dtbLetras = New DataTable
                        If xlReader.FieldCount <= 13 Then
                            dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(6), "0.00"))
                            dtbLetras = ObtenerLetrasPeriodo(dtrDatos("num_MontoImporte"), dtrDatos("dtm_FechaVencimiento"), pstrCodigoBanco)
                        Else
                            dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(7), "0.00"))
                            dtbLetras = ObtenerLetrasIDBanco(dtrDatos("var_NumeroReferencia").ToString, dtrDatos("var_NumeroUnico").ToString, pstrCodigoBanco)
                            If dtbLetras.Rows.Count = 0 AndAlso (dtrDatos("var_NumeroReferencia") <> "") Then
                                dtbLetras = ObtenerLetrasIDBanco("", dtrDatos("var_NumeroReferencia").ToString, pstrCodigoBanco)
                                If dtbLetras.Rows.Count > 0 Then
                                    dtrDatos("var_NumeroUnico") = dtrDatos("var_NumeroReferencia")
                                    dtrDatos("var_NumeroReferencia") = ""
                                End If
                            End If
                        End If

                        If dtbLetras.Rows.Count = 1 Then
                            strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
                            strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
                            strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
                            strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                            strMoneda = dtbLetras.Rows(0)("CO_MONE")
                            dblSaldo = dtbLetras.Rows(0)("num_Saldo")
                            intEstadoRegistro = 0
                        Else
                            strLetra = ""
                            strCodigoCliente = ""
                            strNombreCliente = xlReader.Item(2)
                            strEstadoLetra = ""
                            dblSaldo = 0
                            If xlReader.FieldCount <= 13 Then
                                If dtbLetras.Rows.Count = 0 Then
                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                    intEstadoRegistro = 2
                                ElseIf dtbLetras.Rows.Count > 1 Then
                                    intEstadoRegistro = 1
                                End If
                                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                            ElseIf xlReader.FieldCount > 13 Then
                                If dtbLetras.Rows.Count = 0 Then
                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                    intEstadoRegistro = 2
                                ElseIf dtbLetras.Rows.Count > 1 Then
                                    intEstadoRegistro = 3
                                End If
                                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                            End If
                        End If
                        dtrDatos("var_LetraBanco") = xlReader.Item(0)
                        dtrDatos("var_EstadoActual") = strEstadoLetra
                        dtrDatos("var_NombreCliente") = strNombreCliente
                        dtrDatos("var_NumeroRUC") = strCodigoCliente
                        dtrDatos("int_EstadoRegistro") = intEstadoRegistro
                        dtrDatos("num_Saldo") = dblSaldo
                        dtrDatos("var_CodigoLetra") = strLetra
                        dtrDatos("var_MonedaLetra") = strMoneda
                        dtrDatos("var_Estado") = xlReader.Item(3) & "-" & Right(strFileName, 3)
                        '-------------------------------------------------
                        '(FINAL) UBICANDO LA LETRA
                        '-------------------------------------------------

                        '-------------------------------------------------
                        '(INICIO) OTROS MONTOS
                        '-------------------------------------------------

                        dtrDatos("var_TipoGrupo") = ""
                        If xlReader.FieldCount <= 13 Then
                            dtrDatos("num_Interes") = xlReader.Item(7) + xlReader.Item(8)
                            dtrDatos("num_MontoComision") = 0
                            dtrDatos("num_MontoPortes") = 0
                            dtrDatos("dtm_FechaMovimiento") = xlReader.Item(10)
                            dtrDatos("num_MontoProtesto") = 0
                            dtrDatos("dtm_FechaProtesto") = ""
                        Else
                            dtrDatos("num_Interes") = xlReader.Item(8) + xlReader.Item(9)
                            If xlReader.Item(10) < 0 Then
                                dtrDatos("num_MontoComision") = -1 * xlReader.Item(10)
                            Else
                                dtrDatos("num_MontoComision") = xlReader.Item(10)
                            End If
                            If IsNumeric(xlReader.Item(11)) Then
                                dtrDatos("num_MontoPortes") = xlReader.Item(11)
                            Else
                                dtrDatos("num_MontoPortes") = 0.0
                            End If
                            dtrDatos("dtm_FechaMovimiento") = xlReader.Item(5)
                            dtrDatos("num_MontoProtesto") = xlReader.Item(12)
                            dtrDatos("dtm_FechaProtesto") = xlReader.Item(5)
                        End If

                        dtbDatos.Rows.Add(dtrDatos)
                    End If

                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function

        Function GetDataSCOPAG(ByRef podrReader As OleDbDataReader, ByRef pdtDatos As DataTable, _
                                ByVal pstrCodigoBanco As String, ByVal pstrFileName As String)
            'mas de 14 columnas
            Dim lstrCodigoCliente As String = ""
            Dim lstrNombreCliente As String = podrReader.Item(2)
            Dim ldblImporte As Double
            Dim lstrLetra As String = ""
            Dim lstrEstadoLetra As String
            Dim lintEstadoRegistro As Int16 = 0
            Dim ldblSaldo As Double = 0
            Dim lstrMoneda As String = ""
            Dim ldrFila As DataRow
            Dim ldtLetras As DataTable
            '-------------------------------------------------
            '(INICIO) DATOS DEL MOVIMIENTO 
            '-------------------------------------------------
            ldrFila = pdtDatos.NewRow
            ldrFila("var_CodigoEmpresa") = "01"
            ldrFila("var_CodigoBanco") = pstrCodigoBanco
            ldrFila("var_NumeroUnico") = CInt(podrReader.Item(1))
            ldrFila("var_NumeroReferencia") = IIf(Left((podrReader.Item(0)), 4) = "BSA-", Replace((podrReader.Item(0)), "BSA-", ""), "")
            Dim strFechaVencimiento() As String = Split(podrReader.Item(4), "/")
            Dim strAuxiliarFecha As String
            If strFechaVencimiento(2) < 100 Then
                strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/20" & strFechaVencimiento(2)
            Else
                strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/" & strFechaVencimiento(2)
            End If
            ldrFila("dtm_FechaVencimiento") = strAuxiliarFecha
            '-------------------------------------------------
            '(FINAL) DATOS DEL MOVIMIENTO 
            '-------------------------------------------------

            '-------------------------------------------------
            '(INICIO) UBICANDO LA LETRA
            '-------------------------------------------------
            ldtLetras = New DataTable

            If InStr(podrReader.Item(10), "PROTESTADO") > 0 Then
                ldrFila("num_MontoImporte") = CDbl(Format(podrReader.Item(6), "0.00"))
            Else
                ldrFila("num_MontoImporte") = CDbl(Format(podrReader.Item(7), "0.00"))
            End If

            ldtLetras = ObtenerLetrasIDBanco(ldrFila("var_NumeroReferencia").ToString, ldrFila("var_NumeroUnico").ToString, pstrCodigoBanco)
            If ldtLetras.Rows.Count = 0 And Left((podrReader.Item(0)), 4) = "BSA-" Then
                ldtLetras = ObtenerLetrasIDBanco(ldrFila("var_NumeroUnico").ToString, ldrFila("var_NumeroReferencia").ToString, pstrCodigoBanco)
            End If

            If ldtLetras.Rows.Count = 1 Then
                lstrLetra = ldtLetras.Rows(0)("NU_LETR_CLIE")
                lstrCodigoCliente = ldtLetras.Rows(0)("CO_CLIE")
                lstrNombreCliente = ldtLetras.Rows(0)("NO_CLIE")
                lstrEstadoLetra = ldtLetras.Rows(0)("CO_ESTA_DOCU")
                lstrMoneda = ldtLetras.Rows(0)("CO_MONE")
                ldblSaldo = ldtLetras.Rows(0)("num_Saldo")
                lintEstadoRegistro = 0
            Else
                lstrLetra = ""
                lstrCodigoCliente = ""
                lstrNombreCliente = podrReader.Item(2)
                lstrEstadoLetra = ""
                ldblSaldo = 0
                If ldtLetras.Rows.Count = 0 Then
                    'SI NO SE ENCONTRO EL NUMERO UNICO
                    lintEstadoRegistro = 2
                ElseIf ldtLetras.Rows.Count > 1 Then
                    lintEstadoRegistro = 3
                End If
                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
            End If
            ldrFila("var_LetraBanco") = podrReader.Item(0)
            ldrFila("var_EstadoActual") = lstrEstadoLetra
            ldrFila("var_NombreCliente") = lstrNombreCliente
            ldrFila("var_NumeroRUC") = lstrCodigoCliente
            ldrFila("int_EstadoRegistro") = lintEstadoRegistro
            ldrFila("num_Saldo") = ldblSaldo
            ldrFila("var_CodigoLetra") = lstrLetra
            ldrFila("var_MonedaLetra") = lstrMoneda
            ldrFila("var_Estado") = podrReader.Item(3) & "-" & Right(pstrFileName, 3)
            '-------------------------------------------------
            '(FINAL) UBICANDO LA LETRA
            '-------------------------------------------------

            '-------------------------------------------------
            '(INICIO) OTROS MONTOS
            '-------------------------------------------------

            ldrFila("var_TipoGrupo") = ""
            ldrFila("num_Interes") = podrReader.Item(8) '+ podrReader.Item(9)
            ldrFila("num_MontoComision") = podrReader.Item(9)
            ldrFila("num_MontoPortes") = 0.0
            ldrFila("dtm_FechaMovimiento") = podrReader.Item(5)
            ldrFila("num_MontoProtesto") = 0 'podrReader.Item(13)
            ldrFila("dtm_FechaProtesto") = podrReader.Item(5)

            pdtDatos.Rows.Add(ldrFila)
        End Function
        Function GetDataSCOING(ByRef podrReader As OleDbDataReader, ByRef pdtDatos As DataTable, _
                                ByVal pstrCodigoBanco As String, ByVal pstrFileName As String)
            '14 columnas
            Dim lstrCodigoCliente As String = ""
            Dim lstrNombreCliente As String = podrReader.Item(2)
            Dim ldblImporte As Double
            Dim lstrLetra As String = ""
            Dim lstrEstadoLetra As String
            Dim lintEstadoRegistro As Int16 = 0
            Dim ldblSaldo As Double = 0
            Dim lstrMoneda As String = ""
            Dim ldrFila As DataRow
            Dim ldtLetras As DataTable
            '-------------------------------------------------
            '(INICIO) DATOS DEL MOVIMIENTO 
            '-------------------------------------------------
            ldrFila = pdtDatos.NewRow
            ldrFila("var_CodigoEmpresa") = "01"
            ldrFila("var_CodigoBanco") = pstrCodigoBanco
            ldrFila("var_NumeroUnico") = CInt(podrReader.Item(1))
            ldrFila("var_NumeroReferencia") = IIf(Left((podrReader.Item(0)), 4) = "BSA-", Replace((podrReader.Item(0)), "BSA-", ""), "")
            Dim strFechaVencimiento() As String = Split(podrReader.Item(4), "/")
            Dim strAuxiliarFecha As String
            If strFechaVencimiento(2) < 100 Then
                strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/20" & strFechaVencimiento(2)
            Else
                strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/" & strFechaVencimiento(2)
            End If
            ldrFila("dtm_FechaVencimiento") = strAuxiliarFecha
            '-------------------------------------------------
            '(FINAL) DATOS DEL MOVIMIENTO 
            '-------------------------------------------------

            '-------------------------------------------------
            '(INICIO) UBICANDO LA LETRA
            '-------------------------------------------------
            ldtLetras = New DataTable
            ldrFila("num_MontoImporte") = CDbl(Format(podrReader.Item(6), "0.00"))
            ldtLetras = ObtenerLetrasPeriodo(ldrFila("num_MontoImporte"), ldrFila("dtm_FechaVencimiento"), pstrCodigoBanco)
            If ldtLetras.Rows.Count = 0 And Left((podrReader.Item(0)), 4) = "BSA-" Then
                ldtLetras = ObtenerLetrasIDBanco(ldrFila("var_NumeroUnico").ToString, ldrFila("var_NumeroReferencia").ToString, pstrCodigoBanco)
            End If

            If ldtLetras.Rows.Count = 1 Then
                lstrLetra = ldtLetras.Rows(0)("NU_LETR_CLIE")
                lstrCodigoCliente = ldtLetras.Rows(0)("CO_CLIE")
                lstrNombreCliente = ldtLetras.Rows(0)("NO_CLIE")
                lstrEstadoLetra = ldtLetras.Rows(0)("CO_ESTA_DOCU")
                lstrMoneda = ldtLetras.Rows(0)("CO_MONE")
                ldblSaldo = ldtLetras.Rows(0)("num_Saldo")
                lintEstadoRegistro = 0
            Else
                lstrLetra = ""
                lstrCodigoCliente = ""
                lstrNombreCliente = podrReader.Item(2)
                lstrEstadoLetra = ""
                ldblSaldo = 0
                If ldtLetras.Rows.Count = 0 Then
                    'SI NO SE ENCONTRO EL NUMERO UNICO
                    lintEstadoRegistro = 2
                ElseIf ldtLetras.Rows.Count > 1 Then
                    lintEstadoRegistro = 1
                End If
                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO

            End If
            ldrFila("var_LetraBanco") = podrReader.Item(0)
            ldrFila("var_EstadoActual") = lstrEstadoLetra
            ldrFila("var_NombreCliente") = lstrNombreCliente
            ldrFila("var_NumeroRUC") = lstrCodigoCliente
            ldrFila("int_EstadoRegistro") = lintEstadoRegistro
            ldrFila("num_Saldo") = ldblSaldo
            ldrFila("var_CodigoLetra") = lstrLetra
            ldrFila("var_MonedaLetra") = lstrMoneda
            ldrFila("var_Estado") = podrReader.Item(3) & "-" & Right(pstrFileName, 3)
            '-------------------------------------------------
            '(FINAL) UBICANDO LA LETRA
            '-------------------------------------------------

            '-------------------------------------------------
            '(INICIO) OTROS MONTOS
            '-------------------------------------------------

            ldrFila("var_TipoGrupo") = ""
            If podrReader.FieldCount <= 14 Then
                ldrFila("num_Interes") = IIf(Trim(podrReader.Item(7)) = "", 0, podrReader.Item(7)) + IIf(Trim(podrReader.Item(8)) = "", 0, podrReader.Item(8))
                ldrFila("num_MontoComision") = 0
                ldrFila("num_MontoPortes") = 0
                ldrFila("dtm_FechaMovimiento") = podrReader.Item(11)
                ldrFila("num_MontoProtesto") = 0
                ldrFila("dtm_FechaProtesto") = ""
            Else
                ldrFila("num_Interes") = podrReader.Item(8) '+ podrReader.Item(9)
                If podrReader.Item(10) < 0 Then
                    ldrFila("num_MontoComision") = -1 * podrReader.Item(11)
                Else
                    ldrFila("num_MontoComision") = podrReader.Item(11)
                End If
                If IsNumeric(podrReader.Item(11)) Then
                    ldrFila("num_MontoPortes") = podrReader.Item(12)
                Else
                    ldrFila("num_MontoPortes") = 0.0
                End If
                ldrFila("dtm_FechaMovimiento") = podrReader.Item(5)
                ldrFila("num_MontoProtesto") = podrReader.Item(13)
                ldrFila("dtm_FechaProtesto") = podrReader.Item(5)
            End If

            pdtDatos.Rows.Add(ldrFila)
        End Function
        Function GetDataCiti(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable, Optional ByVal pstrHoja As String = "") As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [LETRAS CITIBANK$]", xlconn)
            Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)

            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    Dim strFechaID As String = ""
                    If IsDBNull(xlReader.Item(0)) = False Then
                        If Right(pstrHoja, 3) = "ING" Then
                            Dim strFechaIni As String = "01/01/20" & CStr(xlReader.Item(1)).Substring(1, 2)
                            Dim strDiaAnno As String = CStr(xlReader.Item(1)).Substring(3, 3)
                            strFechaID = Format(DateAdd(DateInterval.Day, CInt(strDiaAnno) - 1, CDate(strFechaIni)), "dd/MM/yyyy")
                        End If
                        If (Right(pstrHoja, 3) = "PAG" AndAlso IsDBNull(xlReader.Item(9)) = False AndAlso xlReader.Item(9) = strFecha) OrElse _
                                                (Right(pstrHoja, 3) = "ING" AndAlso strFechaID = strFecha) Then
                            Dim strCodigoCliente As String = "", strNombreCliente As String = xlReader.Item(2), dblImporte As Double
                            Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0
                            Dim dblSaldo As Double = 0, strMoneda As String = ""
                            '-------------------------------------------------
                            '(INICIO) DATOS DEL MOVIMIENTO 
                            '-------------------------------------------------
                            dtrDatos = dtbDatos.NewRow
                            dtrDatos("var_CodigoEmpresa") = "01"
                            dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                            dtrDatos("var_NumeroUnico") = CStr(xlReader.Item(1))
                            dtrDatos("var_NumeroReferencia") = ""
                            dtrDatos("dtm_FechaVencimiento") = Format(xlReader.Item(8), "dd/MM/yyyy")
                            '-------------------------------------------------
                            '(FINAL) DATOS DEL MOVIMIENTO 
                            '-------------------------------------------------

                            '-------------------------------------------------
                            '(INICIO) UBICANDO LA LETRA
                            '-------------------------------------------------
                            dtbLetras = New DataTable
                            If Right(pstrHoja, 3) = "ING" Then
                                dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(12), "0.00"))
                                dtbLetras = ObtenerLetrasPeriodo(dtrDatos("num_MontoImporte"), dtrDatos("dtm_FechaVencimiento"), pstrCodigoBanco)
                            ElseIf Right(pstrHoja, 3) = "PAG" Then
                                dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(12), "0.00"))

                                If UCase(Trim(CStr(xlReader.Item(10)))) = "RENOVADA" Then
                                    Dim dblImporteRen As Double = 0
                                    Dim xlconnRen As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
                                    Dim strAuxiliar As String = Left(CStr(xlReader.Item(0)), 6)
                                    Dim strQuery As String = "select * from [LETRAS CITIBANK$]"
                                    Dim xlcommRen As New OleDbCommand(strQuery, xlconnRen)
                                    xlconnRen.Open()
                                    Dim xlReaderRen As OleDbDataReader = xlcommRen.ExecuteReader
                                    While xlReaderRen.Read
                                        If Left(CStr(xlReaderRen.Item(0)), 6) = strAuxiliar AndAlso UCase(Trim(CStr(xlReaderRen.Item(10)))) <> "RENOVADA" Then
                                            dblImporteRen = CDbl(Format(xlReaderRen.Item(12), "0.00"))
                                            Exit While
                                        End If
                                    End While
                                    xlReaderRen.Close()
                                    dtrDatos("num_MontoImporte") = Format(CDbl(Format(xlReader.Item(12), "0.00")) - dblImporteRen, "0.00")
                                End If
                                dtbLetras = ObtenerLetrasIDBanco(dtrDatos("var_NumeroReferencia").ToString, dtrDatos("var_NumeroUnico").ToString, pstrCodigoBanco)
                            End If

                            If dtbLetras.Rows.Count = 1 Then
                                strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
                                strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
                                strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
                                strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                                strMoneda = dtbLetras.Rows(0)("CO_MONE")
                                dblSaldo = dtbLetras.Rows(0)("num_Saldo")
                                intEstadoRegistro = 0
                            Else
                                strLetra = ""
                                strCodigoCliente = ""
                                strNombreCliente = xlReader.Item(5)
                                strEstadoLetra = ""
                                dblSaldo = 0
                                If Right(pstrHoja, 3) = "ING" Then
                                    If dtbLetras.Rows.Count = 0 Then
                                        'SI NO SE ENCONTRO EL NUMERO UNICO
                                        intEstadoRegistro = 2
                                    ElseIf dtbLetras.Rows.Count > 1 Then
                                        intEstadoRegistro = 1
                                    End If
                                    'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                ElseIf Right(pstrHoja, 3) = "PAG" Then
                                    If dtbLetras.Rows.Count = 0 Then
                                        'SI NO SE ENCONTRO EL NUMERO UNICO
                                        intEstadoRegistro = 2
                                    ElseIf dtbLetras.Rows.Count > 1 Then
                                        intEstadoRegistro = 3
                                    End If
                                    'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                End If
                            End If
                            dtrDatos("var_LetraBanco") = xlReader.Item(0)
                            dtrDatos("var_EstadoActual") = strEstadoLetra
                            dtrDatos("var_NombreCliente") = strNombreCliente
                            dtrDatos("var_NumeroRUC") = strCodigoCliente
                            dtrDatos("int_EstadoRegistro") = intEstadoRegistro
                            dtrDatos("num_Saldo") = dblSaldo
                            dtrDatos("var_CodigoLetra") = strLetra
                            dtrDatos("var_MonedaLetra") = strMoneda
                            dtrDatos("var_Estado") = xlReader.Item(10)
                            '-------------------------------------------------
                            '(FINAL) UBICANDO LA LETRA
                            '-------------------------------------------------
                            '-------------------------------------------------
                            '(INICIO) OTROS MONTOS
                            '-------------------------------------------------

                            dtrDatos("var_TipoGrupo") = ""
                            If Right(pstrHoja, 3) = "ING" Then
                                dtrDatos("num_Interes") = xlReader.Item(15)
                                dtrDatos("num_MontoComision") = 0
                                dtrDatos("num_MontoPortes") = 0
                                dtrDatos("dtm_FechaMovimiento") = xlReader.Item(9)
                                dtrDatos("num_MontoProtesto") = 0
                                dtrDatos("dtm_FechaProtesto") = ""
                            ElseIf Right(pstrHoja, 3) = "PAG" Then
                                dtrDatos("num_Interes") = xlReader.Item(15)
                                dtrDatos("num_MontoComision") = -1 * xlReader.Item(16)
                                If IsNumeric(xlReader.Item(18)) Then
                                    dtrDatos("num_MontoPortes") = xlReader.Item(18)
                                Else
                                    dtrDatos("num_MontoPortes") = 0.0
                                End If
                                dtrDatos("dtm_FechaMovimiento") = xlReader.Item(9)
                                dtrDatos("num_MontoProtesto") = CDbl(xlReader.Item(17)) + CDbl(xlReader.Item(18))
                                dtrDatos("dtm_FechaProtesto") = xlReader.Item(9)
                            End If
                            dtbDatos.Rows.Add(dtrDatos)
                        End If
                    End If
                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function

        Function GetDataSCO(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [TeleBanking$]", xlconn)
            'Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)

            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    If xlReader.FieldCount <= 14 AndAlso _
                    ((pstrTipoMovimiento = "DES" And xlReader.Item(10) = strFecha) _
                    Or (pstrTipoMovimiento = "COG" And xlReader.Item(11) = strFecha)) Then
                        GetDataSCOING(xlReader, dtbDatos, pstrCodigoBanco, strFileName)
                    End If
                    If (xlReader.FieldCount > 14 AndAlso (xlReader.Item(5) = strFecha OrElse Left(xlReader.Item(5), 6) & "20" & Right(xlReader.Item(5), 2) = strFecha)) Then
                        GetDataSCOPAG(xlReader, dtbDatos, pstrCodigoBanco, strFileName)
                    End If
                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function

        Function GetDataWIE(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [BWS Virtual TeleBanking$]", xlconn)
            Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)

            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    'If InStr(xlReader.Item(10), "ITF") > 0 OrElse InStr(xlReader.Item(10), "I.T.F.") > 0 Then

                    'End If
                    If (xlReader.FieldCount <= 13 AndAlso xlReader.Item(10) = strFecha) _
                    OrElse (xlReader.FieldCount > 13 AndAlso (xlReader.Item(5) = strFecha OrElse Left(xlReader.Item(5), 6) & "20" & Right(xlReader.Item(5), 2) = strFecha)) Then
                        Dim strCodigoCliente As String = "", strNombreCliente As String = xlReader.Item(2), dblImporte As Double
                        Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0
                        Dim dblSaldo As Double = 0, strMoneda As String = ""
                        '-------------------------------------------------
                        '(INICIO) DATOS DEL MOVIMIENTO 
                        '-------------------------------------------------
                        dtrDatos = dtbDatos.NewRow
                        dtrDatos("var_CodigoEmpresa") = "01"
                        dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                        dtrDatos("var_NumeroUnico") = CInt(xlReader.Item(1))
                        dtrDatos("var_NumeroReferencia") = ""
                        Dim strFechaVencimiento() As String = Split(xlReader.Item(4), "/")
                        Dim strAuxiliarFecha As String
                        If strFechaVencimiento(2) < 100 Then
                            strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/20" & strFechaVencimiento(2)
                        Else
                            strAuxiliarFecha = strFechaVencimiento(0) & "/" & strFechaVencimiento(1) & "/" & strFechaVencimiento(2)
                        End If
                        dtrDatos("dtm_FechaVencimiento") = strAuxiliarFecha
                        '-------------------------------------------------
                        '(FINAL) DATOS DEL MOVIMIENTO 
                        '-------------------------------------------------

                        '-------------------------------------------------
                        '(INICIO) UBICANDO LA LETRA
                        '-------------------------------------------------
                        dtbLetras = New DataTable
                        If xlReader.FieldCount <= 13 Then
                            dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(6), "0.00"))
                            dtbLetras = ObtenerLetrasPeriodo(dtrDatos("num_MontoImporte"), dtrDatos("dtm_FechaVencimiento"), pstrCodigoBanco)
                        Else
                            dtrDatos("num_MontoImporte") = CDbl(Format(xlReader.Item(7), "0.00"))
                            dtbLetras = ObtenerLetrasIDBanco(dtrDatos("var_NumeroReferencia").ToString, dtrDatos("var_NumeroUnico").ToString, pstrCodigoBanco)
                        End If

                        If dtbLetras.Rows.Count = 1 Then
                            strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
                            strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
                            strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
                            strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                            strMoneda = dtbLetras.Rows(0)("CO_MONE")
                            dblSaldo = dtbLetras.Rows(0)("num_Saldo")
                            intEstadoRegistro = 0
                        Else
                            strLetra = ""
                            strCodigoCliente = ""
                            strNombreCliente = xlReader.Item(2)
                            strEstadoLetra = ""
                            dblSaldo = 0
                            If xlReader.FieldCount <= 13 Then
                                If dtbLetras.Rows.Count = 0 Then
                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                    intEstadoRegistro = 2
                                ElseIf dtbLetras.Rows.Count > 1 Then
                                    intEstadoRegistro = 1
                                End If
                                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                            ElseIf xlReader.FieldCount > 13 Then
                                If dtbLetras.Rows.Count = 0 Then
                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                    intEstadoRegistro = 2
                                ElseIf dtbLetras.Rows.Count > 1 Then
                                    intEstadoRegistro = 3
                                End If
                                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                            End If
                        End If
                        dtrDatos("var_LetraBanco") = xlReader.Item(0)
                        dtrDatos("var_EstadoActual") = strEstadoLetra
                        dtrDatos("var_NombreCliente") = strNombreCliente
                        dtrDatos("var_NumeroRUC") = strCodigoCliente
                        dtrDatos("int_EstadoRegistro") = intEstadoRegistro
                        dtrDatos("num_Saldo") = dblSaldo
                        dtrDatos("var_CodigoLetra") = strLetra
                        dtrDatos("var_MonedaLetra") = strMoneda
                        dtrDatos("var_Estado") = xlReader.Item(3) & "-" & Right(strFileName, 3)
                        '-------------------------------------------------
                        '(FINAL) UBICANDO LA LETRA
                        '-------------------------------------------------

                        '-------------------------------------------------
                        '(INICIO) OTROS MONTOS
                        '-------------------------------------------------

                        dtrDatos("var_TipoGrupo") = ""
                        If xlReader.FieldCount <= 13 Then
                            dtrDatos("num_Interes") = xlReader.Item(7) + xlReader.Item(8)
                            dtrDatos("num_MontoComision") = 0
                            dtrDatos("num_MontoPortes") = 0
                            dtrDatos("dtm_FechaMovimiento") = xlReader.Item(10)
                            dtrDatos("num_MontoProtesto") = 0
                            dtrDatos("dtm_FechaProtesto") = ""
                        Else
                            dtrDatos("num_Interes") = xlReader.Item(8) + xlReader.Item(9)
                            If xlReader.Item(10) < 0 Then
                                dtrDatos("num_MontoComision") = -1 * xlReader.Item(10)
                            Else
                                dtrDatos("num_MontoComision") = xlReader.Item(10)
                            End If
                            If IsNumeric(xlReader.Item(11)) Then
                                dtrDatos("num_MontoPortes") = xlReader.Item(11)
                            Else
                                dtrDatos("num_MontoPortes") = 0.0
                            End If
                            dtrDatos("dtm_FechaMovimiento") = xlReader.Item(5)
                            dtrDatos("num_MontoProtesto") = xlReader.Item(12)
                            dtrDatos("dtm_FechaProtesto") = xlReader.Item(5)
                        End If

                        dtbDatos.Rows.Add(dtrDatos)
                    End If

                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function

        Function GetDataBCP_01(ByVal pstrRuta As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            'Dim fp As StreamReader = New StreamReader(pstrRuta)
            Dim fs As FileSystemObject
            Dim ts As TextStream
            fs = New FileSystemObject
            ts = fs.OpenTextFile(pstrRuta, IOMode.ForReading, False)

            Dim strLinea As String
            Try
                'Creando schema
                strLinea = ts.ReadLine
                If Not (strLinea Is Nothing) Then
                    Dim dtrDatos As DataRow
                    Dim dtbLetras As DataTable
                    Dim dtbEsquema As DataTable = ObtenerEsquema(pstrCodigoBanco)

                    For Each dtrEsquema As DataRow In dtbEsquema.Rows
                        Dim arrDatos() As String = strLinea.Split(";")
                        Dim strItem As String, intContador As Int16, bolMatch As Boolean

                        For Each dtColumna As DataColumn In dtrEsquema.Table.Columns
                            intContador = 0
                            bolMatch = False
                            For Each strItem In arrdatos
                                If dtColumna.ColumnName.ToString <> "int_Fila" AndAlso dtrEsquema(dtColumna.ColumnName.ToString) = strItem.Substring(1, strItem.Length - 2).ToString Then
                                    dtrEsquema.BeginEdit()
                                    dtrEsquema(dtColumna.ColumnName) = intContador
                                    dtrEsquema.EndEdit()
                                    bolMatch = True
                                End If
                                intContador = intContador + 1

                            Next

                            If bolMatch = False Then
                                dtrEsquema.BeginEdit()
                                dtrEsquema(dtColumna.ColumnName) = -1
                                dtrEsquema.EndEdit()
                            End If
                        Next
                    Next
                    dtbEsquema.AcceptChanges()

                    Do While ts.AtEndOfStream = False
                        strLinea = ts.ReadLine
                        'BUSCANDO DATOS ADICIONALES
                        Dim arrDatos() As String = strLinea.Split(";")
                        dtrDatos = dtbDatos.NewRow
                        For Each dtrEsquema As DataRow In dtbEsquema.Rows
                            Dim strItem As String, intContador As Int16
                            For Each dtColumna As DataColumn In dtrEsquema.Table.Columns
                                Dim intIndice As Int16 = dtrEsquema(dtColumna.ColumnName.ToString)
                                If intIndice > -1 Then
                                    If dtColumna.ColumnName.ToString = "var_CodigoLetra" Then
                                        Dim strCodigoCliente As String = "", strNombreCliente As String = arrDatos(3).Substring(1, arrDatos(3).Length - 2)
                                        Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0, dblSaldo As Double = 0, strMoneda As String = ""

                                        dtbLetras = New DataTable
                                        If arrDatos(dtrEsquema("var_Estado")).Length >= 9 AndAlso arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) = "PENDIENTE" Then
                                            'If arrDatos(dtrEsquema("var_Estado")).Length >= 9 Then
                                            'If arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) = "PENDIENTE" Then
                                            dtbLetras = ObtenerLetrasPeriodo(arrDatos(dtrEsquema("num_MontoImporte")).Replace(Chr(34), ""), arrDatos(dtrEsquema("dtm_FechaVencimiento")).Replace(Chr(34), ""), pstrCodigoBanco)
                                        Else
                                            dtbLetras = ObtenerLetrasIDBanco(arrDatos(dtrEsquema("var_NumeroReferencia")).Substring(1, arrDatos(dtrEsquema("var_NumeroReferencia")).Length - 2), arrDatos(dtrEsquema("var_NumeroUnico")).Substring(1, arrDatos(dtrEsquema("var_NumeroUnico")).Length - 2), pstrCodigoBanco)
                                        End If

                                        If dtbLetras.Rows.Count = 1 Then
                                            strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
                                            strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
                                            strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
                                            strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                                            strMoneda = dtbLetras.Rows(0)("CO_MONE")
                                            dblSaldo = dtbLetras.Rows(0)("num_Saldo")
                                            intEstadoRegistro = 0
                                        Else
                                            strLetra = ""
                                            strCodigoCliente = ""
                                            strNombreCliente = arrDatos(3).Substring(1, arrDatos(3).Length - 2)
                                            strEstadoLetra = ""
                                            dblSaldo = 0
                                            If arrDatos(dtrEsquema("var_Estado")).Length >= 9 AndAlso arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) <> "PENDIENTE" Then
                                                If dtbLetras.Rows.Count = 0 Then
                                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                                    intEstadoRegistro = 2
                                                End If
                                            End If
                                            If arrDatos(dtrEsquema("var_Estado")).Length >= 9 AndAlso arrDatos(dtrEsquema("var_Estado")).Substring(1, 9) = "PENDIENTE" Then
                                                If dtbLetras.Rows.Count = 0 Then
                                                    'SI NO SE ENCONTRO EL NUMERO UNICO
                                                    intEstadoRegistro = 2
                                                ElseIf dtbLetras.Rows.Count > 1 Then
                                                    intEstadoRegistro = 1
                                                End If
                                                'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                            End If
                                        End If

                                        'var_LetraBanco
                                        dtrDatos("var_LetraBanco") = arrDatos(intIndice).Substring(1, arrDatos(intIndice).Length - 2)
                                        dtrDatos(dtColumna.ColumnName.ToString) = strLetra
                                        dtrDatos("var_EstadoActual") = strEstadoLetra
                                        dtrDatos("var_NombreCliente") = strNombreCliente
                                        dtrDatos("var_NumeroRUC") = strCodigoCliente
                                        dtrDatos("int_EstadoRegistro") = intEstadoRegistro
                                        dtrDatos("var_MonedaLetra") = strMoneda
                                        dtrDatos("num_Saldo") = dblSaldo
                                    Else
                                        If IsDBNull(dtrDatos(dtColumna.ColumnName.ToString)) = True OrElse dtrDatos(dtColumna.ColumnName.ToString) = "" Then
                                            dtrDatos(dtColumna.ColumnName.ToString) = arrDatos(intIndice).Substring(1, arrDatos(intIndice).Length - 2)
                                        End If
                                    End If
                                End If
                            Next
                        Next
                        dtbEsquema.AcceptChanges()

                        dtbDatos.Rows.Add(dtrDatos)
                    Loop    'Until ts.AtEndOfStream			 ' strLinea Is Nothing
                    dtbDatos.AcceptChanges()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                ts.Close()
                fs = Nothing
            End Try

            Return dtbDatos
        End Function

        Function GetDataBIF_01(ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            'Dim fp As StreamReader = New StreamReader(pstrRuta)
            Dim fs As FileSystemObject
            Dim ts As TextStream
            fs = New FileSystemObject
            ts = fs.OpenTextFile(pstrRuta, IOMode.ForReading, False)

            Try
                Dim strLinea As String
                'Creando schema
                strLinea = ts.ReadLine
                If Not (strLinea Is Nothing) Then
                    Dim dtrDatos As DataRow
                    Dim dtbLetras As DataTable

                    Do While ts.AtEndOfStream = False
                        strLinea = ts.ReadLine
                        If strLinea <> "" Then
                            'BUSCANDO DATOS
                            Dim arrDatos() As String = strLinea.Split(",")
                            dtrDatos = dtbDatos.NewRow

                            Dim strCodigoCliente As String = "", strNombreCliente As String = arrDatos(1), dblImporte As Double
                            Dim strLetra As String = "", strEstadoLetra As String, intEstadoRegistro As Int16 = 0
                            Dim dblSaldo As Double = 0, strMoneda As String = ""

                            If arrDatos(3) = "ING" Then
                                dblImporte = arrDatos(5)
                            Else
                                dblImporte = arrDatos(6)
                            End If

                            dtbLetras = New DataTable
                            If arrDatos(3).Length >= 3 AndAlso arrDatos(3) = "ING" Then
                                dtbLetras = ObtenerLetrasPeriodo(dblImporte, arrDatos(4), pstrCodigoBanco)
                            Else
                                dtbLetras = ObtenerLetrasIDBanco(arrDatos(0).ToString, arrDatos(2).ToString, pstrCodigoBanco)
                            End If

                            If dtbLetras.Rows.Count = 1 Then
                                strLetra = dtbLetras.Rows(0)("NU_LETR_CLIE")
                                strCodigoCliente = dtbLetras.Rows(0)("CO_CLIE")
                                strNombreCliente = dtbLetras.Rows(0)("NO_CLIE")
                                strEstadoLetra = dtbLetras.Rows(0)("CO_ESTA_DOCU")
                                strMoneda = dtbLetras.Rows(0)("CO_MONE")
                                dblSaldo = dtbLetras.Rows(0)("num_Saldo")
                                intEstadoRegistro = 0
                            Else
                                strLetra = ""
                                strCodigoCliente = ""
                                strNombreCliente = arrDatos(1)
                                strEstadoLetra = ""
                                dblSaldo = 0
                                If arrDatos(3).Length >= 3 AndAlso arrDatos(3) = "ING" Then
                                    If dtbLetras.Rows.Count = 0 Then
                                        'SI NO SE ENCONTRO EL NUMERO UNICO
                                        intEstadoRegistro = 2
                                    ElseIf dtbLetras.Rows.Count > 1 Then
                                        intEstadoRegistro = 1
                                    End If
                                    'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                End If
                                If arrDatos(3).Length >= 3 AndAlso arrDatos(3) <> "ING" Then
                                    If dtbLetras.Rows.Count = 0 Then
                                        'SI NO SE ENCONTRO EL NUMERO UNICO
                                        intEstadoRegistro = 2
                                    ElseIf dtbLetras.Rows.Count > 1 Then
                                        intEstadoRegistro = 3
                                    End If
                                    'SI LA LETRA TIENE VARIAS OPCIONES Y NO ES INGRESO
                                End If
                            End If

                            dtrDatos("var_LetraBanco") = arrDatos(2)
                            dtrDatos("var_EstadoActual") = strEstadoLetra
                            dtrDatos("var_NombreCliente") = strNombreCliente
                            dtrDatos("var_NumeroRUC") = strCodigoCliente
                            dtrDatos("int_EstadoRegistro") = intEstadoRegistro
                            dtrDatos("num_Saldo") = dblSaldo

                            dtrDatos("var_CodigoEmpresa") = "01"
                            dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                            dtrDatos("var_NumeroUnico") = arrDatos(2)
                            dtrDatos("var_NumeroReferencia") = arrDatos(0)
                            dtrDatos("var_CodigoLetra") = strLetra
                            dtrDatos("var_MonedaLetra") = strMoneda
                            dtrDatos("dtm_FechaVencimiento") = arrDatos(4)
                            If arrDatos(3) = "ING" Then
                                dtrDatos("num_MontoImporte") = arrDatos(5)
                            Else
                                dtrDatos("num_MontoImporte") = arrDatos(6)
                            End If
                            dtrDatos("var_Estado") = arrDatos(3)

                            If arrDatos(7) < 0 Then
                                If arrDatos(3) <> "ING" AndAlso (pstrTipoMovimiento = "COG" OrElse pstrTipoMovimiento = "COL") Then
                                    dtrDatos("num_Interes") = -1 * arrDatos(7)
                                ElseIf arrDatos(3) = "ING" AndAlso pstrTipoMovimiento = "DES" Then
                                    dtrDatos("num_Interes") = -1 * arrDatos(7)
                                Else
                                    dtrDatos("num_Interes") = 0
                                End If
                            Else
                                dtrDatos("num_Interes") = arrDatos(7)
                            End If

                            Dim dblMontoProtesto As Double = 0
                            If IsDBNull(arrDatos(11)) = False Then
                                dblMontoProtesto = dblMontoProtesto + IIf(arrDatos(11) < 0, -1 * arrDatos(11), arrDatos(11))
                            End If
                            If IsDBNull(arrDatos(9)) = False Then
                                dblMontoProtesto = dblMontoProtesto + IIf(arrDatos(9) < 0, -1 * arrDatos(9), arrDatos(9))
                            End If
                            If IsDBNull(arrDatos(8)) = False Then
                                dblMontoProtesto = dblMontoProtesto + IIf(arrDatos(8) < 0, -1 * arrDatos(8), arrDatos(8))
                            End If

                            dtrDatos("num_MontoProtesto") = dblMontoProtesto
                            dtrDatos("dtm_FechaProtesto") = ""
                            dtrDatos("num_MontoComision") = 0
                            dtrDatos("num_MontoPortes") = 0
                            dtrDatos("dtm_FechaMovimiento") = ""
                            dtrDatos("var_TipoGrupo") = ""

                            dtbDatos.Rows.Add(dtrDatos)
                        End If
                    Loop    'Until ts.AtEndOfStream			 ' strLinea Is Nothing
                    dtbDatos.AcceptChanges()
                End If
            Catch ex As Exception
                Throw ex
            Finally
                ts.Close()
                fs = Nothing
            End Try

            Return dtbDatos
        End Function

        Function ObtenerBancoPorAbreviatura(ByVal pstr_AbreviaBanco As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_AbreviaBanco", pstr_AbreviaBanco}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerBanco", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ListarBancos() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_qry_ListarBanco")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerEsquema(ByVal pstCodigoBanco As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstCodigoBanco}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerEsquemaArchivoBanco", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerTipoMovimientoBanco(ByVal pstrCodigoTipoMovimiento As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Dim objParametros() As Object = {"p_var_CodigoTipoMovimiento", pstrCodigoTipoMovimiento}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerTipoMovimiento", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerTeleProceso(ByVal pstrCodigoProceso As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoProceso", pstrCodigoProceso}
                Return _objConexion.ObtenerDataTable("usp_qry_BuscarArchivoProceso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function RegistrarMovimientoBanco(ByVal pstrCodigoBanco As String, _
        ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
        ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
        ByVal pstrDatos As String, ByVal pstrDietario As String, ByVal pstrUsuario As String) As DataSet
            Try
                Dim arrFecha() As String = pstrFechaOperacion.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
                "p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
                "p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, _
                "p_var_Dietario", pstrDietario, "p_var_Usuario", pstrUsuario}
                Return _objConexion.ObtenerDataSet("usp_qry_RegistrarMovimiento", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ProcesarDocumentos(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, ByVal pstrCodigoProceso As String, _
        ByVal pstrFechaOperacion As String, ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
         ByVal pstrDatos As String, ByVal pstrUsuario As String, ByVal pstrSinProcesar As String) As DataSet
            Try
                Dim arrFecha() As String = pstrFechaOperacion.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
                "p_var_FechaOperacion", strFecha, "p_var_CodigoProceso", pstrCodigoProceso, "p_var_CodigoOperacion", pstrCodigoOperacion, _
                "p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, "p_var_Usuario", pstrUsuario, "p_var_NoDatos", pstrSinProcesar}
                Return _objConexion.ObtenerDataSet("usp_prc_ProcesandoDocumentos", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function RegistrarContabilidad(ByVal pstrCodigoBanco As String, ByVal pstr_NumeroComprobante As String, _
         ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
         ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
         ByVal pstrDatos As String, ByVal pstrDietario As String, ByVal pstrUsuario As String) As DataSet
            Try
                Dim arrFecha() As String = pstrFechaOperacion.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
                "p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
                "p_var_NumeroComprobante", pstr_NumeroComprobante, "p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, _
                "p_var_Dietario", pstrDietario, "p_var_Usuario", pstrUsuario}
                Return _objConexion.ObtenerDataSet("usp_prc_RegistrarContabilidadIngreso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function RegistrarContabilidadCaja(ByVal pstrCodigoBanco As String, ByVal pstr_NumeroComprobante As String, _
         ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
         ByVal pstrCodigoOperacion As String, ByVal pstrResumen As String, _
         ByVal pstrDatos As String, ByVal pstrDietario As String, ByVal pstrUsuario As String) As DataSet
            Try
                Dim arrFecha() As String = pstrFechaOperacion.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
                "p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
                "p_var_NumeroComprobante", pstr_NumeroComprobante, "p_var_Resumen", pstrResumen, "p_var_Datos", pstrDatos, _
                "p_var_Dietario", pstrDietario, "p_var_Usuario", pstrUsuario}
                Return _objConexion.ObtenerDataSet("usp_prc_RegistrarContabilidadIngreso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function RegistrarContabilidadNCD(ByVal pstrCodigoBanco As String, _
        ByVal pstrCuentaBanco As String, ByVal pstrFechaOperacion As String, _
        ByVal pstrCodigoOperacion As String, ByVal pstrDatos As String, ByVal pstrUsuario As String) As DataSet
            Try
                Dim arrFecha() As String = pstrFechaOperacion.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)

                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CuentaBanco", pstrCuentaBanco, _
                "p_var_FechaOperacion", strFecha, "p_var_CodigoOperacion", pstrCodigoOperacion, _
                "p_var_Datos", pstrDatos, "p_var_Usuario", pstrUsuario}
                Return _objConexion.ObtenerDataSet("usp_prc_RegistrarContabilidadNCD", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ActualizarDocumento(ByVal pstrCodigoBanco As String, _
          ByVal pstrCodigoOperacion As String, ByVal pstrDatos As String, ByVal pstrUsuario As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CodigoOperacion", pstrCodigoOperacion, _
                  "p_var_Datos", pstrDatos, "p_var_Usuario", pstrUsuario}
                Return _objConexion.ObtenerDataTable("usp_qry_ActualizarDocumento", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerEstadoLetra(ByVal pstrCodigoBanco As String, ByVal pstrCodigoDietario As String, ByVal pstrEstadoEquivalente As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco, "p_var_CodigoDietario", _
                pstrCodigoDietario, "p_var_EstadoEquivalente", pstrEstadoEquivalente}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerEstadoLetra", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerBanco(ByVal pstrCodigoBanco As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerBancoPorCodigo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerOperacion(ByVal pstrCodigoOperacion As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Dim objParametros() As Object = {"p_var_CodigoOperacion", pstrCodigoOperacion}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerOperacionBancaria", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerOperacion(ByVal pstrCodigoOperacion As String, ByVal pstrNombreOperacion As String, Optional ByVal pstrTipo As String = "") As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Dim objParametros() As Object = {"p_var_CodigoOperacion", pstrCodigoOperacion, "p_var_NombreOperacion", pstrNombreOperacion, "p_var_Tipo", pstrTipo}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerOperacionBancaria", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerLetrasPeriodo(ByVal pdbl_Importe As Double, ByVal pstrFecha As String, ByVal pstrCodigoBanco As String) As DataTable
            Try
                Dim arrFecha() As String = pstrFecha.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_num_ImporteTotal", pdbl_Importe, "p_var_Fecha", strFecha, "p_var_CodigoBanco", pstrCodigoBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_BuscarLetraCliente", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerLetrasPeriodo(ByVal pstrNombreCliente As String, ByVal pdbl_Importe As Double, _
        ByVal pstrFecha As String, ByVal pstrCodigoBanco As String) As DataTable
            Try
                Dim arrFecha() As String = pstrFecha.Split("/")
                Dim strFecha As String = arrFecha(2) & arrFecha(1) & arrFecha(0)
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_NombreCliente", pstrNombreCliente, "p_num_ImporteTotal", pdbl_Importe, _
                "p_var_Fecha", strFecha, "p_var_CodigoBanco", pstrCodigoBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_ListarLetraCliente", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerLetrasIDBanco(ByVal pstrNumeroReferencia As String, ByVal pstrNumeroUnico As String, ByVal pstrCodigoBanco As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_NumeroReferencia", pstrNumeroReferencia, _
                "p_var_NumeroUnico", pstrNumeroUnico, "p_var_CodigoBanco", pstrCodigoBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_BuscarLetraIDBanco", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerLetrasPorCodigo(ByVal pstrCodigoLetra As String, ByVal pstrCodigoBanco As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoLetra", pstrCodigoLetra, "p_var_CodigoBanco", pstrCodigoBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_BuscarLetraPorCodigo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ListarCuentasBancos(ByVal strBanco As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Dim objParametros() As Object = {"p_var_CodigoBanco", strBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_ListarCuentaPorBanco", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerCuentaBanco(ByVal strBanco As String, ByVal pstr_TipoDietario As String, ByVal pstr_TipoMoneda As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoBanco", strBanco, "p_var_TipoDietario", _
                pstr_TipoDietario, "p_var_TipoMoneda", pstr_TipoMoneda}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerCuentaCorriente", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ObtenerCuentaBanco(ByVal strBanco As String, ByVal pstr_TipoMoneda As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoBanco", strBanco, "p_var_TipoMoneda", pstr_TipoMoneda}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerCuentaCorriente1", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ObtenerTipoOperacion(ByVal pstrCodigoBanco As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_CodigoBanco", pstrCodigoBanco}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerTipoOperacion", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                Dim objParametros() As Object = {"p_var_CodigoCliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
                Return _objConexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function AgruparMovimientos(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
        ByVal pstrTipoMovimiento As String, ByVal pdtbDatos As DataTable) As DataSet
            Dim dtbPrincipal As New DataTable
            'creando schema de la tabla
            dtbPrincipal.Columns.Add("sin_CodigoTipo", GetType(Int16))
            dtbPrincipal.Columns.Add("var_NombreTipo", GetType(String))
            dtbPrincipal.Columns.Add("var_Adicional", GetType(String))
            dtbPrincipal.Columns.Add("int_Secuencial", GetType(Int16))
            dtbPrincipal.Columns.Add("chr_TipoMovimiento", GetType(String))
            dtbPrincipal.Columns.Add("num_Importe", GetType(Double))
            dtbPrincipal.Columns.Add("var_Observacion", GetType(String))
            dtbPrincipal.Columns.Add("var_CodigoOperacion", GetType(String))
            dtbPrincipal.Columns.Add("var_NombreOperacion", GetType(String))
            Dim arrPrimary(1) As DataColumn
            '	arrPrimary(0) = dtbPrincipal.Columns("sin_CodigoTipo")
            arrPrimary(0) = dtbPrincipal.Columns("int_Secuencial")
            dtbPrincipal.PrimaryKey = arrPrimary
            Select Case pstrTipoMovimiento
                Case "DES"
                    Descuento(pstrCodigoBanco, pstrCuentaBanco, pdtbDatos, dtbPrincipal)
                Case "COG"
                    CobranzaGarantia(pstrCodigoBanco, pstrCuentaBanco, pdtbDatos, dtbPrincipal)
                Case "COL"
                    CobranzaLibre(pstrCodigoBanco, pstrCuentaBanco, pdtbDatos, dtbPrincipal)
            End Select
            Dim ds As New DataSet("Resumen")
            dtbPrincipal.TableName = "dtbResumen"
            ds.Tables.Add(dtbPrincipal)
            pdtbDatos.TableName = "dtbDatos"
            ds.Tables.Add(pdtbDatos)
            Return ds
        End Function

        Private Function Descuento(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
        ByRef pdtbDatos As DataTable, ByRef dtbPrincipal As DataTable) As DataSet
            Dim dtbTipo As DataTable = ObtenerTipoOperacion(pstrCodigoBanco)
            Dim dtrPrincipal As DataRow

            Try
                For Each dtrDatos As DataRow In pdtbDatos.Rows
                    For Each dtrTipo As DataRow In dtbTipo.Select("var_NombreOperacion='" & dtrDatos("var_Estado") & "'")
                        '----------------------------------------
                        'INGRESO
                        If dtrTipo("var_NombreTipo") = "INGRESO" Then
                            If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
                                dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
                                With dtrPrincipal
                                    .BeginEdit()
                                    .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                    .EndEdit()
                                    dtbPrincipal.LoadDataRow(.ItemArray, True)
                                End With
                            Else
                                dtrPrincipal = dtbPrincipal.NewRow
                                With dtrPrincipal
                                    .BeginEdit()
                                    .Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
                                    .Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
                                    .Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
                                    .Item("chr_TipoMovimiento") = "I"
                                    .Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                    .EndEdit()
                                    dtbPrincipal.LoadDataRow(.ItemArray, True)
                                End With
                            End If
                            dtrDatos.BeginEdit()
                            dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
                            dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
                            dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
                            dtrDatos.EndEdit()
                        End If
                        '----------------------------------------------------
                        'PAGO
                        If dtrTipo("var_NombreTipo") = "PAGO" Then
                            dtrPrincipal = dtbPrincipal.NewRow
                            With dtrPrincipal
                                .BeginEdit()
                                .Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
                                .Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
                                .Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
                                .Item("chr_TipoMovimiento") = "I"
                                .Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoImporte"))
                                .Item("var_Adicional") = dtrDatos("var_EstadoActual")

                                .EndEdit()
                                dtbPrincipal.LoadDataRow(.ItemArray, True)
                            End With

                            dtrDatos.BeginEdit()
                            dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
                            dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
                            dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
                            dtrDatos.EndEdit()
                        End If
                        '-----------------------------------------------------
                        If dtrTipo("var_NombreTipo") = "DEVOLUCION" AndAlso dtrTipo("var_TipoGrupo") = "PROTESTO" Then
                            If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
                                dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
                                With dtrPrincipal
                                    .BeginEdit()
                                    .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")))
                                    .EndEdit()
                                    dtbPrincipal.LoadDataRow(.ItemArray, True)
                                End With
                            Else
                                dtrPrincipal = dtbPrincipal.NewRow
                                With dtrPrincipal
                                    .BeginEdit()
                                    .Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
                                    .Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
                                    .Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
                                    .Item("chr_TipoMovimiento") = "I"
                                    .Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")))
                                    .Item("var_Adicional") = dtrDatos("var_EstadoActual")
                                    .EndEdit()
                                    dtbPrincipal.LoadDataRow(.ItemArray, True)
                                End With
                            End If
                            dtrDatos.BeginEdit()
                            dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
                            dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
                            dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
                            dtrDatos.EndEdit()
                        End If
                        '-----------------------------------------------------
                        If dtrTipo("var_NombreTipo") = "DEVOLUCION" AndAlso dtrTipo("var_TipoGrupo") = "DEVOLUCION" Then
                            If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
                                dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
                                With dtrPrincipal
                                    .BeginEdit()
                                    .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                    .EndEdit()
                                    dtbPrincipal.LoadDataRow(.ItemArray, True)
                                End With
                            Else
                                dtrPrincipal = dtbPrincipal.NewRow
                                With dtrPrincipal
                                    .BeginEdit()
                                    .Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
                                    .Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
                                    .Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
                                    .Item("chr_TipoMovimiento") = "I"
                                    .Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) - Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                    .EndEdit()
                                    dtbPrincipal.LoadDataRow(.ItemArray, True)
                                End With
                            End If
                            dtrDatos.BeginEdit()
                            dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
                            dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
                            dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
                            dtrDatos.EndEdit()
                        End If

                    Next
                Next
                pdtbDatos.AcceptChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function CobranzaGarantia(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
          ByRef pdtbDatos As DataTable, ByRef dtbPrincipal As DataTable) As DataSet
            Dim dtbTipo As DataTable = ObtenerTipoOperacion(pstrCodigoBanco)
            Dim dtrPrincipal As DataRow
            Try
                For Each dtrDatos As DataRow In pdtbDatos.Rows
                    For Each dtrTipo As DataRow In dtbTipo.Select("var_NombreOperacion='" & dtrDatos("var_Estado") & "'")
                        If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
                            dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
                            With dtrPrincipal
                                .BeginEdit()
                                If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
                                    If dtrDatos("var_EstadoNuevo") = "PRO" Then
                                        .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
                                    End If
                                Else
                                    .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                End If
                                .EndEdit()
                                dtbPrincipal.LoadDataRow(.ItemArray, True)
                            End With
                        Else
                            dtrPrincipal = dtbPrincipal.NewRow
                            With dtrPrincipal
                                .BeginEdit()
                                .Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
                                .Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
                                .Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
                                .Item("chr_TipoMovimiento") = "I"
                                If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
                                    If dtrDatos("var_EstadoNuevo") = "PRO" Then
                                        .Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
                                    Else
                                        .Item("num_Importe") = 0
                                    End If
                                Else
                                    If pstrCodigoBanco = "02" OrElse pstrCodigoBanco = "15" Then
                                        .Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes"))
                                    Else
                                        .Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                    End If
                                End If
                                .EndEdit()
                                dtbPrincipal.LoadDataRow(.ItemArray, True)
                            End With
                        End If
                        dtrDatos.BeginEdit()
                        dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
                        dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
                        dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
                        dtrDatos.EndEdit()
                    Next
                Next
                pdtbDatos.AcceptChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function CobranzaLibre(ByVal pstrCodigoBanco As String, ByVal pstrCuentaBanco As String, _
         ByRef pdtbDatos As DataTable, ByRef dtbPrincipal As DataTable) As DataSet
            Dim dtbTipo As DataTable = ObtenerTipoOperacion(pstrCodigoBanco)
            Dim dtrPrincipal As DataRow
            Try
                For Each dtrDatos As DataRow In pdtbDatos.Rows
                    For Each dtrTipo As DataRow In dtbTipo.Select("var_NombreOperacion='" & dtrDatos("var_Estado") & "'")
                        If dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'").Length > 0 Then
                            dtrPrincipal = dtbPrincipal.Select("var_NombreTipo='" & dtrTipo("var_NombreTipo") & "'")(0)
                            With dtrPrincipal
                                .BeginEdit()
                                If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
                                    If dtrDatos("var_EstadoNuevo") = "PRO" Then
                                        .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
                                    End If
                                Else
                                    .Item("num_Importe") = Convert.ToDouble(.Item("num_Importe")) + (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                End If
                                .EndEdit()
                                dtbPrincipal.LoadDataRow(.ItemArray, True)
                            End With
                        Else
                            dtrPrincipal = dtbPrincipal.NewRow
                            With dtrPrincipal
                                .BeginEdit()
                                .Item("sin_CodigoTipo") = dtrTipo("sin_CodigoTipo")
                                .Item("var_NombreTipo") = dtrTipo("var_NombreTipo")
                                .Item("int_Secuencial") = dtbPrincipal.Rows.Count + 1
                                .Item("chr_TipoMovimiento") = "I"
                                If dtrPrincipal("var_NombreTipo") = "DEVOLUCION" Then
                                    If dtrDatos("var_EstadoNuevo") = "PRO" Then
                                        .Item("num_Importe") = Convert.ToDouble(dtrDatos("num_MontoProtesto")) + Convert.ToDouble(dtrDatos("num_Interes")) + Convert.ToDouble(dtrDatos("num_MontoComision")) + Convert.ToDouble(dtrDatos("num_MontoPortes"))
                                    Else
                                        .Item("num_Importe") = 0
                                    End If
                                Else
                                    .Item("num_Importe") = (Convert.ToDouble(dtrDatos("num_MontoImporte")) + Convert.ToDouble(dtrDatos("num_Interes")) - Convert.ToDouble(dtrDatos("num_MontoComision")) - Convert.ToDouble(dtrDatos("num_MontoPortes")))
                                End If
                                .EndEdit()
                                dtbPrincipal.LoadDataRow(.ItemArray, True)
                            End With
                        End If
                        dtrDatos.BeginEdit()
                        dtrDatos.Item("var_Tipo") = dtrTipo("var_NombreTipo")
                        dtrDatos.Item("var_TipoGrupo") = dtrTipo("var_TipoGrupo")
                        dtrDatos.Item("var_IDMovimiento") = dtrPrincipal("int_Secuencial")
                        dtrDatos.EndEdit()
                    Next
                Next
                pdtbDatos.AcceptChanges()
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerSeguimientoLetra(ByVal pstrFechaInicio As String, ByVal pstrFechaFinal As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Dim objParametros() As Object = {"p_var_FechaInicio", pstrFechaInicio, "p_var_FechaFinal", pstrFechaFinal}
                Return _objConexion.ObtenerDataTable("usp_qry_ReporteLetrasSeguimiento", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function GetDataFormFile(ByVal pstrCodigoBanco As String, _
     ByVal pstrTipoMovimiento As String, ByVal pstrRuta As String, _
     Optional ByVal pstrHoja As String = "", Optional ByVal pstrEmpresa As String = "") As DataTable
            Dim dtbDatos As New DataTable
            Try
                'Creando schema
                If System.IO.File.Exists(pstrRuta) AndAlso pstrTipoMovimiento <> "" AndAlso pstrCodigoBanco <> "" Then
                    'CAMPOS DE TRABAJO
                    dtbDatos.Columns.Add("var_CodigoEmpresa", GetType(String))
                    dtbDatos.Columns.Add("var_CodigoBanco", GetType(String))
                    dtbDatos.Columns.Add("dtm_FechaMovimiento", GetType(String))
                    dtbDatos.Columns.Add("var_Descripcion", GetType(String))
                    dtbDatos.Columns.Add("var_NumeroReferencia", GetType(String))
                    dtbDatos.Columns.Add("num_MontoImporte", GetType(Double))
                    dtbDatos.Columns.Add("num_MontoSaldo", GetType(Double))
                    Select Case pstrCodigoBanco
                        Case "01"
                            Select Case pstrTipoMovimiento
                                Case "ITF"
                                    dtbDatos = GetDataITFBCP(pstrEmpresa, pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                                Case Else
                                    dtbDatos = GetDataBCP_01(pstrRuta, pstrCodigoBanco, dtbDatos)
                            End Select
                        Case "08"
                            Select Case pstrTipoMovimiento
                                Case "ITF"
                                    dtbDatos = GetDataITFBIF(pstrEmpresa, pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                            End Select
                        Case "04"
                            dtbDatos = GetDataWIE(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, dtbDatos)
                        Case "03"
                            dtbDatos = GetDataINT(pstrRuta, pstrTipoMovimiento, pstrCodigoBanco, pstrHoja, dtbDatos)
                    End Select
                    dtbDatos.AcceptChanges()
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbDatos
        End Function
        Function GetDataITFBIF(ByVal pstrEmpresa As String, ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [BIF$]", xlconn)
            Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)
            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    If InStr(xlReader.Item(0), "ITF") > 0 OrElse InStr(xlReader.Item(0), "I.T.F.") > 0 OrElse InStr(xlReader.Item(0), "I.T.F") Then
                        dtrDatos = dtbDatos.NewRow

                        dtrDatos("var_CodigoEmpresa") = pstrEmpresa
                        dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                        dtrDatos("dtm_FechaMovimiento") = Format(xlReader.Item(1), "dd/MM/yyyy")
                        dtrDatos("var_Descripcion") = xlReader.Item(0)
                        dtrDatos("var_NumeroReferencia") = xlReader.Item(2)
                        dtrDatos("num_MontoImporte") = Format(CDbl(xlReader.Item(3)), "0.00")
                        dtrDatos("num_MontoSaldo") = Format(CDbl(xlReader.Item(4)), "0.00")

                        dtbDatos.Rows.Add(dtrDatos)
                    End If

                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function
        Function GetDataITFBCP(ByVal pstrEmpresa As String, ByVal pstrRuta As String, ByVal pstrTipoMovimiento As String, ByVal pstrCodigoBanco As String, ByVal dtbDatos As DataTable) As DataTable
            Dim xlconn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & pstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim xlcomm As New OleDbCommand("select * from [BCP$]", xlconn)
            Dim dtrDatos As DataRow, dtbLetras As DataTable
            Dim strFileName As String = Path.GetFileNameWithoutExtension(pstrRuta)
            Dim strFecha As String = Mid(strFileName, 19, 2) & "/" & Mid(strFileName, 17, 2) & "/" & Mid(strFileName, 13, 4)
            Try
                xlconn.Open()
                Dim xlReader As OleDbDataReader = xlcomm.ExecuteReader
                While xlReader.Read
                    If InStr(xlReader.Item(2), "ITF") > 0 OrElse InStr(xlReader.Item(2), "I.T.F.") > 0 OrElse InStr(xlReader.Item(2), "I.T.F") Then
                        If CDbl(xlReader.Item(4)) - CDbl(xlReader.Item(3)) < 0 Then
                            dtrDatos = dtbDatos.NewRow

                            dtrDatos("var_CodigoEmpresa") = pstrEmpresa
                            dtrDatos("var_CodigoBanco") = pstrCodigoBanco
                            dtrDatos("dtm_FechaMovimiento") = Format(xlReader.Item(0), "dd/MM/yyyy")
                            dtrDatos("var_Descripcion") = xlReader.Item(2)
                            dtrDatos("var_NumeroReferencia") = xlReader.Item(7)
                            dtrDatos("num_MontoImporte") = Format(CDbl(xlReader.Item(4)) - CDbl(xlReader.Item(3)), "0.00")
                            dtrDatos("num_MontoSaldo") = Format(CDbl(xlReader.Item(5)), "0.00")

                            dtbDatos.Rows.Add(dtrDatos)
                        End If
                    End If

                End While
                dtbDatos.AcceptChanges()
                xlReader.Close()
                Return dtbDatos
            Catch ex As Exception
                Throw ex
            Finally
                xlconn.Close()
            End Try
        End Function

#End Region

#Region "ENVIO DE LETRAS A BANCOS"

        Function ObtenerLetrasNoAsignadasBanco(ByVal pstr_CodigoCliente As String, ByVal pstr_CodigoMoneda As String, _
        ByVal pstr_NombreCliente As String, ByVal pnum_Importe As Double, ByVal pstr_FechaVencimiento As String, _
        ByVal strEstado1 As String, ByVal strEstado2 As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoCliente", pstr_CodigoCliente, "var_NombreCliente", pstr_NombreCliente, _
                "var_FechaVencimiento", pstr_FechaVencimiento, "num_ImporteLetra", pnum_Importe, "var_TipoMoneda", pstr_CodigoMoneda, _
                "var_Estado1", strEstado1, "var_Estado2", strEstado2}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_TES_LetrasEnvioBanco_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerLetrasAsignadasBanco(ByVal pstr_CodigoBanco As String, ByVal pstr_FechaEnvio As String, ByVal pstr_CodigoMoneda As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoBanco", pstr_CodigoBanco, "var_FechaEnvio", pstr_FechaEnvio, "var_TipoMoneda", pstr_CodigoMoneda}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_TES_LetrasEnviadasBanco_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerLetrasAsignadasBanco_Resumen(ByVal pstr_CodigoBanco As String, ByVal pstr_FechaInicio As String, ByVal pstr_FechaFinal As String, ByVal pstr_CodigoMoneda As String) As DataTable
            Try
                Dim objParametros() As Object = {"var_CodigoBanco", pstr_CodigoBanco, _
                "var_FechaInicio", pstr_FechaInicio, "var_FechaFinal", pstr_FechaFinal, "var_TipoMoneda", pstr_CodigoMoneda}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_TES_ResumenLetrasBanco_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ProcesarPlanilla(ByVal pstr_CodigoPlanilla As String, ByVal pstr_CodigoBanco As String, _
        ByVal pstr_CodigoMoneda As String, ByVal pstr_TipoPago As String, ByVal pstr_CodigoCuenta As String, _
        ByVal pstr_FechaEnvio As String, ByVal pdtb_Datos As DataTable, ByVal pdtb_Restore As DataTable) As String
            Dim objUtil As New Util
            Dim str_XMLDatos As String = objUtil.EncodeXML(objUtil.GeneraXml(pdtb_Datos))
            Dim strXMLRestore As String = objUtil.EncodeXML(objUtil.GeneraXml(pdtb_Restore))

            Try
                Dim objParametros() As Object = {"var_CodigoPlanilla", pstr_CodigoPlanilla, "var_CodigoBanco", pstr_CodigoBanco, _
                "var_CodigoMoneda", pstr_CodigoMoneda, "var_TipoPago", pstr_TipoPago, "var_CodigoCuenta", pstr_CodigoCuenta, _
                "var_FechaEnvio", pstr_FechaEnvio, "var_XMLData", str_XMLDatos, "var_XMLRestore", strXMLRestore, _
                "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return CType(_objConexion.ObtenerValor("usp_TES_LetrasEnvioBanco_Procesar", objParametros), String)
            Catch ex As Exception
                Throw ex
                Return ""
            End Try
        End Function

        Function ConfirmarPlanilla(ByVal pstr_CodigoPlanilla As String, ByVal pdtb_Datos As DataTable) As String
            Dim objUtil As New Util
            Dim str_XMLDatos As String = objUtil.EncodeXML(objUtil.GeneraXml(pdtb_Datos))

            Try
                Dim objParametros() As Object = {"var_CodigoPlanilla", pstr_CodigoPlanilla, _
                "var_XMLData", str_XMLDatos, "var_Usuario", _strUsuario}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return CType(_objConexion.ObtenerValor("usp_TES_LetrasEnvioBancoConfirma_Procesar", objParametros), String)
            Catch ex As Exception
                Throw ex
                Return ""
            End Try
        End Function

        Function ListarPlanilla(ByVal pstr_CodigoPlanilla As String, ByVal pstr_CodigoBanco As String, _
            ByVal pstr_CodigoMoneda As String, ByVal pstr_TipoPago As String, ByVal pstr_FechaEnvio As String) As DataTable

            Try
                Dim objParametros() As Object = {"var_CodigoPlanilla", pstr_CodigoPlanilla, "var_CodigoBanco", pstr_CodigoBanco, _
                "var_CodigoMoneda", pstr_CodigoMoneda, "var_TipoPago", pstr_TipoPago, "var_FechaEnvio", pstr_FechaEnvio}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_TES_PlanillasBanco_Listar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerPlanilla(ByVal pstr_CodigoPlanilla As String) As DataSet
            Dim objParametros() As Object = {"var_CodigoPlanilla", pstr_CodigoPlanilla}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataSet("usp_TES_PlanillaBanco_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

        Public Function IGVVentas_Obtener(ByVal strFechaInicio As String, ByVal strFechaFinal As String) As DataTable
            Dim objParametros() As Object = {"var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_VentaIGVxTipoDocumento_Reporte", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function IGVCompras_Obtener(ByVal strFechaInicio As String, ByVal strFechaFinal As String) As DataTable
            Dim objParametros() As Object = {"var_FechaInicio", strFechaInicio, "var_FechaFinal", strFechaFinal}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_ComprasIGVxTipoDocumento_Reporte", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function LetrasVencidasClientes_Obtener(ByVal sin_Mes As Int16, ByVal sin_Anno As Int16) As DataTable
            Dim objParametros() As Object = {"var_Empresa", "01", "int_Anio", sin_Anno, "int_Mes", sin_Mes}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_LetrasVencidas_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function LetrasVencimientoClientes_Obtener(ByVal sin_Mes As Int16, ByVal sin_Anno As Int16) As DataTable
            'usp_TES_LetrasVencidas_Obtener
            Dim objParametros() As Object = {"var_Empresa", "01", "int_Anio", sin_Anno, "int_Mes", sin_Mes}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_VencimientoLetras_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function TasaInteresPorDescuento_Resumen(ByVal strMes As String, ByVal strAnno As String, ByVal strCodigoBanco As String, ByVal strCodigoMoneda As String) As DataSet
            Dim objParametros() As Object = {"var_Mes", strMes, "var_Anno", strAnno, "var_CodigoBanco", strCodigoBanco}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataSet("usp_TES_IngresoDescuento_Tasas", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function TasaInteresPorDescuento_Detalle(ByVal strMes As String, ByVal strAnno As String, ByVal strCodigoBanco As String, ByVal strCodigoMoneda As String) As DataTable
            Dim objParametros() As Object = {"var_Mes", strMes, "var_Anno", strAnno, "var_CodigoBanco", strCodigoBanco}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Return _objConexion.ObtenerDataTable("usp_TES_IngresoDescuento_Detalle", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ConciliacionLogisticaTesoreria_Obtener(ByVal strMes As String, ByVal strAnno As String) As DataTable
            Dim objParametros() As Object = {"var_Mes", strMes, "var_Anno", strAnno}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_ConciliacionLogisticaTesoreria_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "PAGO A PROVEEDORES"

        Public Function PagoProveedoresBBVADocumentosObtener(ByVal strBco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal strNCB As String) As DataTable
            Dim objParametros() As Object = {"var_Bnco", strBco, "var_Moneda", strMoneda, "var_Cuenta", strCuenta, "var_NCB", strNCB}
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_TES_PagoProveedoresBBVA_Generar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Function ObtenerCuentasBanco(ByVal strBanco As String, ByVal pstr_CodigoMoneda As String) As DataTable
            Try
                Dim objParametros() As Object = {"p_var_CodigoBanco", strBanco, "p_var_CodigoMoneda", pstr_CodigoMoneda}
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
                Return _objConexion.ObtenerDataTable("usp_qry_ListarCuentasPorBancoPorMoneda", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public WriteOnly Property sPath() As String
            Set(ByVal sCad As String)
                sRuta = sCad
            End Set
        End Property
        Public ReadOnly Property clsError() As String
            Get
                clsError = sError
            End Get
        End Property
        Public Sub GeneraFilePagoProveedoresBBVA(ByVal strBanco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal strDocumento As String, ByVal strFecha As String, ByVal strArchivo As String)
            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim clsDT As New DataTable
            ' Objetos de archivos
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            Dim strCtaCargo As String
            Dim strCodCtaBco As String
            Dim strCodMoneda As String
            Dim strTotImporte As String
            Dim strFechaProc As String
            Dim strNroReg As String
            Dim strTipoRegistro As String
            Dim strNomEnti As String
            Dim strDOI As String
            Dim strNroDOI As String
            Dim strTiAbono As String
            Dim strCtaAbono As String
            Dim strImporte As String
            Dim strRefe As String
            Dim strTiDocu As String
            Dim strNuDocu As String
            Dim strAboAgru As String
            Dim strIndAviso As String
            Dim strDeMail As String
            Dim strPerContact As String
            Dim strBlancosC As String = Space(68)
            Dim strBlancosD As String = Space(50)
            Dim intFila As Integer

            Dim lobjParametros() As Object = {"CO_BANC", strBanco, "NU_CTA", strCuenta, "NU_DOCU", strDocumento}
            Try
                sError = ""
                If System.IO.File.Exists(sFilePath) = True Then
                    System.IO.File.Delete(sFilePath)
                End If
                clsDT = lobjconexion.ObtenerDataTable("usp_TES_PagoProveedoresBBVA_Generar", lobjParametros)
                strFile = System.IO.File.OpenWrite(sFilePath)
                strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                For intFila = 0 To clsDT.Rows.Count - 1
                    With clsDT.Rows(intFila)
                        'Cabecera
                        If intFila = 0 Then
                          
                            strCtaCargo = CType(.Item("CTA_CARGO"), String)
                            strCodMoneda = LSet(fsChar(CType(.Item("CO_MONE"), String)), 3)
                            strTotImporte = CType(.Item("TO_CARGO"), String)
                            strFechaProc = CType(strFecha, String)
                            strRefe = LSet(fsChar(CType(.Item("DE_REFE"), String)), 26)
                            strNroReg = fsNumCad(CType(.Item("NU_REGI"), String))

                            strWriter.WriteLine("750" & _
                                                strCtaCargo & _
                                                strCodMoneda & _
                                                strTotImporte & _
                                                "A" & _
                                                strFechaProc & _
                                                strRefe & _
                                                strNroReg & _
                                                "N" & _
                                                 strBlancosC)

                        End If
                        'Detalle
                        
                        strDOI = CType(.Item("TIPO_DOI"), String)
                        strNroDOI = LSet(fsChar(CType(.Item("NRO_DOI"), String)), 12)
                        strTiAbono = CType(.Item("TI_ABON"), String)
                        strCtaAbono = CType(.Item("NU_CNTA_PROV"), String)
                        strNomEnti = LSet(fsLeter(CType(.Item("NO_ENTI"), String)), 40)
                        strImporte = fsNumDec(CType(.Item("TO_ABON"), String))
                        strTiDocu = CType(.Item("TIP_DOCU"), String)
                        strNuDocu = LSet(fsLeter(CType(.Item("NRO_DOCU"), String)), 12)
                        strAboAgru = CType(.Item("ABO_AGRU"), String)
                        strRefe = LSet(fsChar(CType(.Item("DE_REFE"), String)), 40)
                        strIndAviso = LSet(fsChar(CType(.Item("IND_AVISO"), String)), 1)
                        strDeMail = LSet(fsChar(CType(.Item("DE_MAIL"), String)), 50)
                        strPerContact = LSet(fsChar(CType(.Item("PER_CONTACT"), String)), 30)
                        strWriter.WriteLine("002" & _
                                            strDOI & _
                                            strNroDOI & _
                                            strTiAbono & _
                                            strCtaAbono & _
                                            strNomEnti & _
                                            strImporte & _
                                            strTiDocu & _
                                            strNuDocu & _
                                            strAboAgru & _
                                            strRefe & _
                                            strIndAviso & _
                                            strDeMail & _
                                            strPerContact & _
                                            strBlancosD)
                    End With

                Next
                strWriter.Close()
                strWriter = Nothing
                Exit Sub

            Catch ex As Exception
                sError = ex.Message

            End Try

        End Sub
        Public Sub GeneraFilePagoProveedoresInterBank(ByVal strBanco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal strDocumento As String, ByVal strFecha As String, ByVal strArchivo As String)
            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim clsDT As New DataTable
            ' Objetos de archivos
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            Dim strCodRegi As String
            Dim strCodBene As String
            Dim strTipOper As String
            Dim strNroDocu As String
            Dim strFecVenc As String
            Dim strMonOPer As String
            Dim strImpOper As String
            Dim strCtaBanc As String
            Dim strTipAbon As String
            Dim strTipProd As String
            Dim strCodMone As String
            Dim strCodOfic As String
            Dim strNroCta As String
            Dim strTipPers As String
            Dim strTipDoPr As String
            Dim strNroDoPr As String
            Dim strNomProv As String
            Dim strBlancosD As String = Space(8)
            Dim intFila As Integer

            Dim lobjParametros() As Object = {"CO_BANC", strBanco, "NU_CTA", strCuenta, "NU_DOCU", strDocumento}
            Try
                sError = ""
                If System.IO.File.Exists(sFilePath) = True Then
                    System.IO.File.Delete(sFilePath)
                End If
                clsDT = lobjconexion.ObtenerDataTable("usp_TES_PagoProveedoresInterBank_Generar", lobjParametros)
                strFile = System.IO.File.OpenWrite(sFilePath)
                strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                For intFila = 0 To clsDT.Rows.Count - 1
                    With clsDT.Rows(intFila)
                       
                        'Detalle
                        strCodRegi = CType(.Item("TI_REGI"), String)
                        strCodBene = LSet(fsLeter(CType(.Item("CO_ENTI"), String)), 20)
                        strTipOper = CType(.Item("TI_OPER"), String)
                        strNroDocu = LSet(fsLeter(CType(.Item("NRO_DOC"), String)), 20)
                        strFecVenc = LSet(fsLeter(CType(.Item("FE_VENC"), String)), 8)
                        strMonOPer = CType(.Item("CO_MONE_OPER"), String)
                        strImpOper = fsNumDec(CType(.Item("TOT_ABON"), String))
                        strCtaBanc = CType(.Item("IND_BANCO"), String)
                        strTipAbon = CType(.Item("TIPO_ABON"), String)
                        strTipProd = CType(.Item("TIPO_PROD"), String)
                        strCodMone = CType(.Item("CO_MONE"), String)
                        strCodOfic = CType(.Item("COD_OFI"), String)
                        strNroCta = LSet(fsChar(CType(.Item("NU_CNTA_PROV"), String)), 20)
                        strTipPers = CType(.Item("TIPO_PERS"), String)
                        strTipDoPr = CType(.Item("TIP_DOC_PRV"), String)
                        strNroDoPr = LSet(fsChar(CType(.Item("NRO_DOC_PRV"), String)), 15)
                        strNomProv = LSet(fsChar(CType(.Item("NO_ENTI"), String)), 60)
                        strWriter.WriteLine(strCodRegi & _
                                            strCodBene & _
                                            strTipOper & _
                                            strNroDocu & _
                                            strFecVenc & _
                                            strMonOPer & _
                                            strImpOper & _
                                            strCtaBanc & _
                                            strTipAbon & _
                                            strTipProd & _
                                            strCodMone & _
                                            strCodOfic & _
                                            strNroCta & _
                                            strTipPers & _
                                            strTipDoPr & _
                                            strNroDoPr & _
                                            strNomProv & _
                                            "000000000000000")
                    End With

                Next
                strWriter.Close()
                strWriter = Nothing
                Exit Sub

            Catch ex As Exception
                sError = ex.Message

            End Try
        End Sub

        Private Function fsNumDec(ByVal sValor As String) As String
            Dim sCade As String = ""
            Dim Pos As Integer
            If InStr(1, CStr(sValor), ".") > 0 Then
                Pos = InStr(1, CStr(sValor), ".") - 1
                sCade = "000000000000000" & Left(sValor, Pos) & Right(sValor, 2)
            End If
            fsNumDec = Right(sCade, 15)
        End Function
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
        Private Function fsNumCad(ByVal sValor As String) As String
            Dim sCade As String = ""
            sCade = "000000" & (sValor)
            fsNumCad = Right(sCade, 6)
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
#End Region

    End Class
End Namespace
