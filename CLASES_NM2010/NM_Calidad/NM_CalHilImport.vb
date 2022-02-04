Imports System.IO
Imports System.IO.FileSystemInfo
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Data.OleDb
Imports NM_Calidad
Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace NM_Calidad
    Public Class NM_CalHilImport
        Public directorio As String
        Sub New()
        End Sub
        Public Sub importar(ByVal ruta As String)
            Try
                'Carga lista de archivos en memoria
                Dim i, j As Int16
                Dim fso As DirectoryInfo

                Dim dirparfiles() As FileInfo
                Dim dirtblfiles() As FileInfo
                Dim file As FileInfo

                fso = New DirectoryInfo(ruta)

                dirparfiles = fso.GetFiles("*.PAR")
                dirtblfiles = fso.GetFiles("*.TBL")

                'Crea DataSet con archivos 
                Dim objDS As DataSet = New DataSet
                Dim DTParFiles, DTTblFiles As DataTable
                Dim DRParFiles, DRTblFiles As DataRow
                Dim registro, drtemp1 As DataRow

                'Agrega los archivos de parametros a la tabla par del DataSet 
                DTParFiles = New DataTable("par")
                DTParFiles.Columns.Add("extension")
                DTParFiles.Columns.Add("name")
                DTParFiles.Columns.Add("status")
                For Each file In dirparfiles
                    DRParFiles = DTParFiles.NewRow()
                    DRParFiles("extension") = file.Extension
                    DRParFiles("name") = file.Name
                    DRParFiles("status") = 0
                    DTParFiles.Rows.Add(DRParFiles)
                Next

                'Agrega los archivos de tablas a la tabla par del DataSet
                DTTblFiles = New DataTable("tbl")
                DTTblFiles.Columns.Add("extension")
                DTTblFiles.Columns.Add("name")
                DTTblFiles.Columns.Add("status")
                For Each file In dirtblfiles
                    DRTblFiles = DTTblFiles.NewRow()
                    DRTblFiles("extension") = file.Extension
                    DRTblFiles("name") = file.Name
                    DRTblFiles("status") = 0
                    DTTblFiles.Rows.Add(DRTblFiles)
                Next

                'Definicion de tablas de parametros 
                Dim seccion As String
                Dim DTPar As DataTable = New DataTable("par")
                DTPar.TableName = "par"
                Dim DRPar As DataRow
                DTPar.Columns.Add("file")
                DTPar.Columns.Add("seccion")
                DTPar.Columns.Add("codigo")
                DTPar.Columns.Add("descripcion")
                DTPar.Columns.Add("valor")

                'Le los archivos y los almacena en data
                Dim linea As String
                Dim arrlinea As String()
                Dim nametext As String()

                'Lee archivos de parametros
                For Each registro In DTParFiles.Rows
                    If registro("status") = 0 Then
                        linea = registro("name")
                        nametext = linea.Split(".")

                        FileOpen(1, ruta + "\\" + registro("name"), OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
                        Do While Not EOF(1)
                            linea = LineInput(1)
                            arrlinea = linea.Split(";")
                            If arrlinea(0) = "SECTION Labels" Then
                                seccion = "labels"
                            End If
                            If arrlinea(0) = "SECTION Settings" Then
                                seccion = "setings"
                            End If
                            If arrlinea(0) = "SECTION TableColumns" Then
                                seccion = "columns"
                            End If
                            If arrlinea(0) = "SECTION TableRows" Then
                                seccion = "rows"
                            End If

                            If arrlinea.Length > 1 Then

                                DRPar = DTPar.NewRow()
                                DRPar("file") = nametext(0)
                                DRPar("seccion") = seccion
                                DRPar("codigo") = arrlinea(0)
                                DRPar("descripcion") = arrlinea(1)
                                If arrlinea.Length > 4 Then
                                    If arrlinea(0).Trim() = "TIME" Then
                                        DRPar("valor") = arrlinea(7) + " " + arrlinea(8)
                                    Else
                                        DRPar("valor") = arrlinea(4)
                                    End If

                                Else
                                    DRPar("valor") = arrlinea(2)
                                End If
                                DTPar.Rows.Add(DRPar)

                            End If
                        Loop
                        FileClose(1)
                        registro("status") = 1

                    End If

                Next

                'Definicion de tabla de reporte
                Dim DTTbl As DataTable = New DataTable("pruebas")
                DTPar.TableName = "tbl"
                Dim DRTbl As DataRow

                DTTbl.Columns.Add("codigo", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("fecha", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("operario", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("articulo", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("material", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("torsion", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("titulo", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("codigohilo", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("maquina", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("descripcion", System.Type.GetType("System.String"))
                DTTbl.Columns.Add("estado", System.Type.GetType("System.String"))

                For Each drtemp1 In DTPar.Select("file='" + nametext(0) + "' AND seccion='columns'")
                    DTTbl.Columns.Add(drtemp1("descripcion"), System.Type.GetType("System.String"))
                Next

                For Each registro In DTTblFiles.Rows
                    If registro("status") = 0 Then

                        linea = registro("name")
                        nametext = linea.Split(".")
                        j = 1
                        If noDuplicate(nametext(0)) Then

                            FileOpen(1, ruta + "\\" + registro("name"), OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
                            Do While Not EOF(1)
                                linea = LineInput(1)
                                arrlinea = linea.Split(";")

                                If arrlinea.Length >= 5 Then

                                    DRTbl = DTTbl.NewRow()
                                    DRTbl("estado") = "1"
                                    DRTbl("codigo") = nametext(0)

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='TIME'").GetValue(0)
                                    DRTbl("fecha") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='LABORANT'").GetValue(0)
                                    DRTbl("operario") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='ARTICLE'").GetValue(0)
                                    DRTbl("articulo") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='MATCLASS'").GetValue(0)
                                    DRTbl("material") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='NOMTWIST'").GetValue(0)
                                    DRTbl("torsion") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='NOMCOUNT'").GetValue(0)
                                    DRTbl("titulo") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='COMMENT'").GetValue(0)
                                    DRTbl("codigohilo") = drtemp1("valor")

                                    drtemp1 = DTPar.Select("file='" + nametext(0) + "' AND codigo='MASCHNR'").GetValue(0)
                                    DRTbl("maquina") = drtemp1("valor")
                                    Try
                                        i = 0
                                        For Each drtemp1 In DTPar.Select("file='" + nametext(0) + "' AND seccion='columns'")
                                            DRTbl(drtemp1("descripcion")) = arrlinea(i)
                                            i += 1
                                        Next
                                    Catch ex As Exception
                                        Throw ex
                                    End Try
                                    If DTPar.Select("file='" + nametext(0) + "' AND seccion='rows' and codigo='" + j.ToString() + "'").Length <= 0 Then

                                        j += 1
                                        For Each drtemp1 In DTPar.Select("file='" + nametext(0) + "' AND seccion='rows' and codigo='" + j.ToString() + "'")
                                            DRTbl("descripcion") = drtemp1("descripcion")
                                        Next
                                        j += 1

                                    Else

                                        For Each drtemp1 In DTPar.Select("file='" + nametext(0) + "' AND seccion='rows' and codigo='" + j.ToString() + "'")
                                            DRTbl("descripcion") = drtemp1("descripcion")
                                        Next
                                        j += 1

                                    End If

                                    DTTbl.Rows.Add(DRTbl)

                                End If

                            Loop

                            FileClose(1)
                            registro("status") = 1

                        End If
                    End If

                Next

                objDS.Tables.Add(DTTbl)

                Dim pruebas As NM_Consulta = New NM_Consulta(2)

                pruebas.Insert(objDS, "pruebas")
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Function noDuplicate(ByVal txtFile As String) As Boolean
            Dim pruebas As NM_Consulta = New NM_Consulta(2)
            Dim sql As String
            Dim num As Int16
            sql = " select count(*) from pruebas "
            sql += " where codigo='" + txtFile + "'"
            sql += " and estado=1"
            Dim tQuery As DataTable = pruebas.Query(sql)
            If tQuery.Rows.Count > 0 Then
                num = CInt(tQuery.Rows(0)(0))
            Else
                num = 0
            End If
            If num > 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Function ObtenerCargaMezcla(ByVal strFecha As String) As String
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrPeriodo As String
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadHilanderia)
                Dim lstrParametros() As String = {"dtm_Fecha", strFecha}
                lstrPeriodo = lobjCon.ObtenerValor("USP_HIL_Mezcla_Algodon", lstrParametros)
                Return lstrPeriodo
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function
    End Class
    Public Class Importacion

        Public Enum enuTipoFuente
            [Excel] = 0
            [BaseDatos] = 1
        End Enum

#Region "   Variables"
        Private mstrRuta As String = "D:\Interfaz\Mezcla.xls"
        Private mintFecha As String
        Private mstrUsuario As String
        Private mstrError As String
#End Region
#Region "   Propiedades"
        Public Property Ruta() As String
            Get
                Ruta = mstrRuta
            End Get
            Set(ByVal Value As String)
                mstrRuta = Value
            End Set
        End Property
        Public Property Fecha() As String
            Get
                Fecha = mintFecha
            End Get
            Set(ByVal Value As String)
                mintFecha = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Usuario = mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property
        Public Property ErrorDesc() As String
            Get
                ErrorDesc = mstrError
            End Get
            Set(ByVal Value As String)
                mstrError = Value
            End Set
        End Property
#End Region
#Region "   Esquemas"
        Public Function EsquemaMezclaAlgodon() As DataTable
            Dim ldtRes As DataTable = New DataTable("MezclaAlgodon")
            With ldtRes.Columns
                .Add("var_Id", GetType(String))
                .Add("var_tipo", GetType(String))
                .Add("var_variedad", GetType(String))
                .Add("var_Grado", GetType(String))
                .Add("var_Hebra", GetType(String))
                .Add("num_FinuraMinimo", GetType(Integer))
                .Add("num_FinuraMaximo", GetType(Integer))
                .Add("num_ResistenciaMaximo", GetType(Integer))
                .Add("var_GradoMinimo", GetType(String))
                .Add("var_GradoMaximo", GetType(String))
                .Add("var_HebraPromedio", GetType(String))
                .Add("num_NumeroFardos", GetType(Integer))
                .Add("num_NumeroQQS", GetType(Integer))
                .Add("num_NumeroPorcentaje", GetType(Integer))
            End With
            Return ldtRes
        End Function
#End Region
#Region "   Obtener Data de XLS"
        Public Function ObtenerMezclasAlgodon(ByVal penuFuente As enuTipoFuente) As DataTable
            Try
                Select Case penuFuente
                    Case enuTipoFuente.Excel
                        Return ObtenerMezclasAlgodonXLS()
                    Case enuTipoFuente.BaseDatos
                End Select
            Catch ex As Exception
                Return EsquemaMezclaAlgodon()
            End Try
        End Function
        Private Function ObtenerMezclasAlgodonXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [Mezclas$]", lobjCon)
            Dim ldtMezclaAlgodon As DataTable = EsquemaMezclaAlgodon()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        'If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                        ldrFila = ldtMezclaAlgodon.NewRow()
                        With ldrFila
                            .Item(0) = xlReader.Item(0)
                            .Item(1) = xlReader.Item(1)
                            .Item(2) = xlReader.Item(2)
                            .Item(3) = xlReader.Item(3)
                            If IsDBNull(xlReader.Item(4)) Then .Item(4) = " " Else .Item(4) = xlReader.Item(4)
                            If IsDBNull(xlReader.Item(5)) Then .Item(5) = 0 Else .Item(5) = xlReader.Item(5)
                            If IsDBNull(xlReader.Item(6)) Then .Item(6) = 0 Else .Item(6) = xlReader.Item(6)
                            If IsDBNull(xlReader.Item(7)) Then .Item(7) = 0 Else .Item(7) = xlReader.Item(7)
                            If IsDBNull(xlReader.Item(8)) Then .Item(8) = 0 Else .Item(8) = xlReader.Item(8)
                            If IsDBNull(xlReader.Item(9)) Then .Item(9) = " " Else .Item(9) = xlReader.Item(9)
                            If IsDBNull(xlReader.Item(10)) Then .Item(10) = " " Else .Item(10) = xlReader.Item(10)
                            If IsDBNull(xlReader.Item(11)) Then .Item(11) = 0 Else .Item(11) = xlReader.Item(11)
                            If IsDBNull(xlReader.Item(12)) Then .Item(12) = 0 Else .Item(12) = xlReader.Item(12)
                            .Item(13) = xlReader.Item(13)
                        End With
                        ldtMezclaAlgodon.Rows.Add(ldrFila)
                        ldrFila = Nothing
                        'End If
                    End If
                End While
                xlReader.Close()
                Return ldtMezclaAlgodon
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function
        Public Function InsertarMezclaAlgodon(ByRef pdtMezcla As DataTable, ByVal strUsuario As String) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrProcedure As String = ""
            Dim lbooOk As Boolean = False
            Dim lobjUtil As NM_General.Util
            mstrError = ""
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadHilanderia)
                lobjUtil = New NM_General.Util
                Dim lstrParametros() As String = {"sin_Fecha", mintFecha, _
                                                "ntx_Mezcla", lobjUtil.GeneraXml(pdtMezcla), _
                                                "var_Usuario", "DARWIN"} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("USP_CAL_Insertar_Mezclas", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                mstrError = ex.Message
                lbooOk = False
            Finally
                lobjUtil = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function

#End Region

    End Class
End Namespace