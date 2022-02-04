Imports System.Web.UI.WebControls
Public Class NMFieldText
    Inherits System.Web.UI.WebControls.WebControl

    Private txtCajaTexto As New System.Web.UI.WebControls.TextBox
    Private btnBoton As New System.Web.UI.HtmlControls.HtmlInputButton
    Private tblTabla As New System.Web.UI.WebControls.Table


    Protected Overrides Sub CreateChildControls()
        Dim ltrFila As TableRow
        Dim ltcCelda As TableCell

        txtCajaTexto.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(0)
        txtCajaTexto.Width = System.Web.UI.WebControls.Unit.Pixel(MyBase.Width.Value - 20)
        txtCajaTexto.Height = System.Web.UI.WebControls.Unit.Pixel(16)
        txtCajaTexto.CssClass = "Input"
        btnBoton.Style.Add("Width", "20px")
        btnBoton.Style.Add("Height", "16px")
        btnBoton.Value = "..."
        btnBoton.Attributes.Add("Class", "boton")
        txtCajaTexto.ID = MyBase.ID + "_Text"
        btnBoton.ID = MyBase.ID + "_Button"

        tblTabla.CellPadding = 0
        tblTabla.CellSpacing = 0
        tblTabla.BorderWidth = Unit.Pixel(0)

        ltrFila = New TableRow
        ltcCelda = New TableCell
        ltcCelda.Controls.Add(txtCajaTexto)
        ltcCelda.Style.Add("BORDER-LEFT", "thin inset")
        ltcCelda.Style.Add("BORDER-TOP", "thin inset")
        ltcCelda.Style.Add("BORDER-BOTTOM", "thin inset")
        ltcCelda.Style.Add("BACKGROUND-COLOR", "white")
        ltrFila.Cells.Add(ltcCelda)
        ltcCelda = New TableCell
        ltcCelda.Controls.Add(btnBoton)
        ltcCelda.Style.Add("BORDER-RIGHT", "thin inset")
        ltcCelda.Style.Add("BORDER-TOP", "thin inset")
        ltcCelda.Style.Add("BORDER-BOTTOM", "thin inset")
        ltcCelda.Style.Add("BACKGROUND-COLOR", "white")
        ltrFila.Cells.Add(ltcCelda)
        tblTabla.Rows.Add(ltrFila)

        Controls.Add(tblTabla)

    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        'Page.Response.Clear()
        'CreateChildControls()
        EnsureChildControls()
        MyBase.Render(writer)
    End Sub
End Class
