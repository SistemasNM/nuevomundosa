Imports System.Web.UI.WebControls
Imports System.Web.ui
#Region "   Cabecera Personalizada"
Public Class ITCabecera
    Implements ITemplate

    Private mstrID As String
    Private mstrDataField As String
    Private mstrHeaderText As String

    Sub New(ByVal ID As String, ByVal strDataField As String, ByVal strHeaderText As String)
        mstrID = ID
        mstrDataField = strDataField
        mstrHeaderText = strHeaderText
    End Sub

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
        Dim lobjtableRow As TableRow
        Dim lobjTableCell As TableCell
        Dim lobjImagen As System.Web.UI.WebControls.Image

        Dim ldtTabla As New Table
        ldtTabla.ID = mstrDataField + mstrID
        lobjtableRow = New TableRow
        lobjTableCell = New TableCell
        lobjImagen = New System.Web.UI.WebControls.Image
        lobjImagen.ImageUrl = "../images/Bloqueado.png"
        lobjTableCell.Controls.Add(lobjImagen)
        lobjtableRow.Cells.Add(lobjTableCell)
        lobjTableCell = New TableCell
        lobjTableCell.Width = New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
        lobjTableCell.Text = mstrHeaderText
        lobjTableCell.HorizontalAlign = HorizontalAlign.Right
        lobjTableCell.CssClass = "GridHeader"
        lobjtableRow.Cells.Add(lobjTableCell)
        ldtTabla.Rows.Add(lobjtableRow)

        container.Controls.Add(ldtTabla)
    End Sub
End Class
#End Region
#Region "   Item Personalizado"
Public Class ITItem
    Implements ITemplate

    Private mstrID As String
    Private mstrDataField As String
    Private mstrHeaderText As String
    Private WithEvents mobjLabel As Label

    Sub New(ByVal ID As String, ByVal strDataField As String, ByVal strHeaderText As String)
        mstrID = ID
        mstrDataField = strDataField
        mstrHeaderText = strHeaderText
    End Sub

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
        Dim lobjtableRow As TableRow
        Dim lobjTableCell As TableCell
        Dim lobjImagen As System.Web.UI.WebControls.Image
        mobjLabel = New Label
        mobjLabel.CssClass = "inputnumber"
        mobjLabel.BackColor = System.Drawing.Color.Transparent
        mobjLabel.Width = System.Web.UI.WebControls.Unit.Percentage(100)
        mobjLabel.ID = "txt" + mstrDataField + mstrID
        container.Controls.Add(mobjLabel)
    End Sub

    Private Sub lobjLabel_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles mobjLabel.DataBinding
        Dim lobjLabel As Label = CType(sender, Label)
        Dim lobjContainer As DataGridItem = CType(lobjLabel.NamingContainer, DataGridItem)
        lobjLabel.ID = lobjContainer.ClientID + "_" + lobjLabel.ID
        lobjLabel.Text = DataBinder.Eval(lobjContainer.DataItem, mstrDataField)
    End Sub
End Class
#End Region
