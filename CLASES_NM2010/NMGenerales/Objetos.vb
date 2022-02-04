Imports System
Imports System.IO
Imports System.Windows.Forms.Control
Imports System.Windows.Forms

Public Class Objetos
    Default Public ReadOnly Property Item(ByVal DataTable As DataTable) As cDataTableNM
        Get
            Item = New cDataTableNM(DataTable)
        End Get
    End Property
    Default Public ReadOnly Property Item(ByVal DataView As DataView) As cDataViewNM
        Get
            Item = New cDataViewNM(DataView)
        End Get
    End Property
    Default Public ReadOnly Property Item(ByVal DataGrid As DataGrid) As cDataGridNM
        Get
            Item = New cDataGridNM(DataGrid)
        End Get
    End Property
    Default Public ReadOnly Property Item(ByVal Control As System.Windows.Forms.Control) As cControl
        Get
            Item = New cControl(Control)
        End Get
    End Property
    Default Public ReadOnly Property Item(ByVal ControlWeb As System.Web.UI.WebControls.DropDownList) As cControl
        Get
            Item = New cControl(ControlWeb)
        End Get
    End Property
#Region "    Clases"
    Public Class cDataTableNM
#Region "       Variables"
        Private mdtDataTable As DataTable
#End Region
#Region "       Enumeraciones"
        Enum enuFormats
            [XML]
        End Enum
#End Region
#Region "       Constructor"
        Friend Sub New(ByRef pDataTable As DataTable)
            mdtDataTable = pDataTable
        End Sub
        Protected Overrides Sub Finalize()
            mdtDataTable = Nothing
            MyBase.Finalize()
        End Sub
#End Region
#Region "       Metodos"
        Public Overloads Function ToString(ByVal OutPutFormat As enuFormats, ByRef Output As String) As Boolean
            Select Case OutPutFormat
                Case enuFormats.XML
                    Output = CType(DataTableToXML(mdtDataTable), String)
                Case Else
                    Output = Nothing
            End Select
        End Function
        Private Function DataTableToXML(ByRef pDataTable As DataTable) As String
            Dim i As Integer
            Dim j As Integer
            Dim lstrHeader As String
            Dim lstrH1_I As String
            Dim lstrH1_E As String
            Dim lstrH2_I As String
            Dim lstrH2_E As String
            Dim lstrH3_I As String
            Dim lstrH3_E As String
      Dim lstrText As String
      'todo texto de XML devuelto debe estar codificado para que reemplaze caracteres especiales
      Dim lobjutilitario As New NM_General.Util
            lstrHeader = "<?xml version=" + """" + "1.0" + """" + " standalone=" + """" + "yes" + """" + "?>"
            lstrH1_I = UCase("<NewDataSet>")
            lstrH1_E = UCase("</NewDataSet>")

            lstrText = lstrHeader + lstrH1_I
            For i = 0 To pDataTable.Rows.Count - 1
                lstrH2_I = "<" + UCase(pDataTable.TableName) + ">"
                lstrH2_E = "</" + UCase(pDataTable.TableName) + ">"
                lstrText = lstrText + lstrH2_I
                For j = 0 To pDataTable.Columns.Count - 1
                    lstrH3_I = "<" + UCase(pDataTable.Columns(j).ColumnName) + ">"
                    lstrH3_E = "</" + UCase(pDataTable.Columns(j).ColumnName) + ">"
                    lstrText = lstrText + lstrH3_I
                    Try
                        If CType(pDataTable.Rows(i).Item(j), String) = "True" Then
                            lstrText = lstrText + "1"
                        ElseIf CType(pDataTable.Rows(i).Item(j), String) = "False" Then
                            lstrText = lstrText + "0"
                        Else
                            lstrText = lstrText + CType(pDataTable.Rows(i).Item(j), String)
                        End If
                    Catch ex As Exception
                        lstrText = lstrText + "Null"
                    End Try
                    lstrText = lstrText + lstrH3_E
                Next
                lstrText = lstrText + lstrH2_E
            Next i
            lstrText = lstrText + lstrH1_E
      Return lobjutilitario.EncodeXML(lstrText)
        End Function
#End Region
    End Class
    Public Class cDataViewNM
#Region "       Variables"
        Private mdvDataView As DataView
#End Region
#Region "       Constructor"
        Friend Sub New(ByRef pDataView As DataView)
            mdvDataView = pDataView
        End Sub
        Protected Overrides Sub Finalize()
            mdvDataView = Nothing
            MyBase.Finalize()
        End Sub
#End Region
#Region "       Metodos"
        Public Overloads Function ToDataTable(ByRef Output As DataTable) As Boolean
            Output = CType(DataTableToDataView(mdvDataView), DataTable)
            mdvDataView = Nothing
        End Function
        Private Function DataTableToDataView(ByRef pDataView As DataView) As DataTable
            Dim ldtRes As DataTable
            Dim i As Integer
            Dim j As Integer
            Dim ldrRow As DataRow

            ldtRes = pDataView.Table.Clone
            ldtRes.TableName = pDataView.Table.TableName
            For i = 0 To pDataView.Count - 1
                ldrRow = ldtRes.NewRow
                For j = 0 To pDataView.Table.Columns.Count - 1
                    ldrRow(j) = pDataView.Item(i).Item(j)
                Next j
                ldtRes.Rows.Add(ldrRow)
                ldrRow = Nothing
            Next i
            Return ldtRes
        End Function
#End Region
    End Class
    Public Class cDataGridNM
        Dim mdtgDataGrid As System.Windows.Forms.DataGrid

        Friend Sub New(ByRef DataGrid As System.Windows.Forms.DataGrid)
            mdtgDataGrid = DataGrid
        End Sub
        Protected Overrides Sub Finalize()
            mdtgDataGrid = Nothing
            MyBase.Finalize()
        End Sub

        Public Function Actions(Optional ByVal Insertable As Boolean = False, _
                    Optional ByVal Editable As Boolean = True, Optional ByVal Deletable As Boolean = False) As Boolean
            Dim lbooOk As Boolean
            Try
                Dim cm As System.Windows.Forms.CurrencyManager
                cm = CType(mdtgDataGrid.BindingContext(mdtgDataGrid.DataSource), System.Windows.Forms.CurrencyManager)
                Dim Dv As DataView = CType(cm.List, DataView)
                Dv.AllowEdit = Editable
                Dv.AllowNew = Insertable
                Dv.AllowDelete = Deletable
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                mdtgDataGrid = Nothing
            End Try
            Return lbooOk
        End Function
    End Class
    Public Class cControl
        Private mobjControl As Object
        Private mobjToolTip As globoTooltip
        Private mstrTipo As String = ""

        Friend Sub New(ByVal Control As System.Windows.Forms.Control)
            mobjControl = Control
            mstrTipo = "Combo"
        End Sub

        Friend Sub New(ByVal Control As System.Web.UI.WebControls.DropDownList)
            mobjControl = Control
            mstrTipo = "DropDownList"
        End Sub

        Public Sub ShowAlert(ByVal Title As String, ByVal Message As String)
            mobjToolTip = New globoTooltip
            With mobjToolTip
                .Style = globoTooltip.ttStyleEnum.TTBalloon
                .Centered = False
                .Icon = globoTooltip.ttIconType.TTIconInfo
                .Title = Title
                .TipText = Message
                .PopupOnDemand = False
                .CreateToolTip(mobjControl.Handle.ToInt32)
                .VisibleTime = 1
                .Show(0, 0, mobjControl.Handle.ToInt32)
                mobjControl = Nothing
            End With
        End Sub
        Public Sub DestroyAlert()
            mobjToolTip.Destroy()
        End Sub

        Public Sub FillMonths()
            Dim lcboCombo As System.Windows.Forms.ComboBox
            Dim lcboWebCombo As System.Web.UI.WebControls.DropDownList
            Dim ldtRes As DataTable
            Dim ldcColumn As DataColumn
            Dim ldrRow As DataRow
            Dim i As Integer

            ldtRes = New DataTable
            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "Codigo"
            ldtRes.Columns.Add(ldcColumn)
            ldcColumn = Nothing
            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "Nombre"
            ldtRes.Columns.Add(ldcColumn)
            ldcColumn = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "01"
            ldrRow("Nombre") = "ENERO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "02"
            ldrRow("Nombre") = "FEBRERO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "03"
            ldrRow("Nombre") = "MARZO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "04"
            ldrRow("Nombre") = "ABRIL"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "05"
            ldrRow("Nombre") = "MAYO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "06"
            ldrRow("Nombre") = "JUNIO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "07"
            ldrRow("Nombre") = "JULIO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "08"
            ldrRow("Nombre") = "AGOSTO"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "09"
            ldrRow("Nombre") = "SEPTIEMBRE"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "10"
            ldrRow("Nombre") = "OCTUBRE"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "11"
            ldrRow("Nombre") = "NOVIEMBRE"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "12"
            ldrRow("Nombre") = "DICIEMBRE"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing
            Select Case mstrTipo
                Case "Combo"
                    lcboCombo = CType(mobjControl, System.Windows.Forms.ComboBox)
                    With lcboCombo
                        .Items.Clear()
                        .DataSource = ldtRes
                        .DisplayMember = "Nombre"
                        .ValueMember = "Codigo"
                    End With
                    ldtRes = Nothing
                    lcboCombo = Nothing
                Case "DropDownList"
                    lcboWebCombo = CType(mobjControl, System.Web.UI.WebControls.DropDownList)
                    With lcboWebCombo
                        .Items.Clear()
                        .DataSource = ldtRes
                        .DataTextField = "Nombre"
                        .DataValueField = "Codigo"
                        .SelectedValue = Format(Month(Now), "00")
                    End With
                    ldtRes = Nothing
                    lcboWebCombo = Nothing
            End Select
        End Sub
        Public Sub FillYears()
            Dim lcboCombo As System.Windows.Forms.ComboBox
            Dim lcboWebCombo As System.Web.UI.WebControls.DropDownList
            Dim ldtRes As DataTable
            Dim ldcColumn As DataColumn
            Dim ldrRow As DataRow
            Dim i As Integer

            ldtRes = New DataTable
            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "Codigo"
            ldtRes.Columns.Add(ldcColumn)
            ldcColumn = Nothing
            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "Nombre"
            ldtRes.Columns.Add(ldcColumn)
            ldcColumn = Nothing

            For i = 2003 To Now.Year
                ldrRow = ldtRes.NewRow
                ldrRow("Codigo") = Format(i, "0000")
                ldrRow("Nombre") = Format(i, "0000")
                ldtRes.Rows.Add(ldrRow)
                ldrRow = Nothing
            Next i
            Select Case mstrTipo
                Case "Combo"
                    lcboCombo = CType(mobjControl, System.Windows.Forms.ComboBox)
                    With lcboCombo
                        .Items.Clear()
                        .DataSource = ldtRes
                        .DisplayMember = "Nombre"
                        .ValueMember = "Codigo"
                    End With
                    ldtRes = Nothing
                    lcboCombo = Nothing
                Case "DropDownList"
                    lcboWebCombo = CType(mobjControl, System.Web.UI.WebControls.DropDownList)
                    With lcboWebCombo
                        .Items.Clear()
                        .DataSource = ldtRes
                        .DataTextField = "Nombre"
                        .DataValueField = "Codigo"
                        .SelectedValue = Year(Now)
                    End With
                    ldtRes = Nothing
                    lcboWebCombo = Nothing
            End Select
        End Sub
        Public Sub FillStates()
            Dim lcboCombo As System.Windows.Forms.ComboBox
            Dim ldtRes As DataTable
            Dim ldcColumn As DataColumn
            Dim ldrRow As DataRow
            Dim i As Integer

            ldtRes = New DataTable
            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "Codigo"
            ldtRes.Columns.Add(ldcColumn)
            ldcColumn = Nothing
            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "Nombre"
            ldtRes.Columns.Add(ldcColumn)
            ldcColumn = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "0"
            ldrRow("Nombre") = "ELIMINADOS"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            ldrRow = ldtRes.NewRow
            ldrRow("Codigo") = "1"
            ldrRow("Nombre") = "ACTIVOS"
            ldtRes.Rows.Add(ldrRow)
            ldrRow = Nothing

            lcboCombo = CType(mobjControl, System.Windows.Forms.ComboBox)
            With lcboCombo
                .Items.Clear()
                .DataSource = ldtRes
                .DisplayMember = "Nombre"
                .ValueMember = "Codigo"
                .SelectedIndex = 1
            End With
            ldtRes = Nothing
            lcboCombo = Nothing
        End Sub
    End Class
#End Region
End Class
