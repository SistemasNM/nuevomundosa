Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Security.Permissions
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

<DefaultProperty("Fecha"), ToolboxData("<{0}:NMDatePicker runat=server Requerido='true' RutaImagenes='/NuevoMundo/Imagenes' RutaScript='/NuevoMundo/JavaScripts/jsCalendario.js' ></{0}:NMDatePicker>")> Public Class NMDatePicker
    Inherits System.Web.UI.WebControls.WebControl

    Private lstrRutaImagenes As String
    Private lstrRutaScript As String
    Private mbooRequerido As Boolean = True

    Private txtFecha As New System.Web.UI.WebControls.TextBox
    Private imgImagen As New HtmlControls.HtmlImage
    'Private divError As New HtmlControls.HtmlGenericControl
    Private cvaValidador As New System.Web.UI.WebControls.CustomValidator
    Private rfvRequerido As New System.Web.UI.WebControls.RequiredFieldValidator


    Protected Overrides Sub CreateChildControls()
        txtFecha.Columns = 14
        txtFecha.ReadOnly = False
        txtFecha.ID = MyBase.ID + "_Texto"
        Controls.Add(txtFecha)
        Controls.Add(New LiteralControl("&nbsp;"))
        imgImagen.Src = "/NuevoMundo/Imagenes/JSCalendario/Calendario.gif"
        Controls.Add(imgImagen)
        Controls.Add(New LiteralControl("&nbsp;"))
        'divError.Attributes.Add("Style", "VISIBILITY:hidden;Width:30px")
        'divError.Attributes.Add("Class", "Error")
        'divError.ID = MyBase.ClientID + "_Error"
        'divError.InnerHtml = ""
        cvaValidador.ControlToValidate = txtFecha.ClientID
        cvaValidador.ClientValidationFunction = "CustomValid"
        cvaValidador.ErrorMessage = "Fecha incorrecta"
        cvaValidador.Text = "!"
        cvaValidador.Font.Size = System.Web.UI.WebControls.FontUnit.XSmall
        cvaValidador.ForeColor = Color.Red
        Controls.Add(cvaValidador)
        If mbooRequerido Then
            rfvRequerido.ControlToValidate = txtFecha.ClientID
            rfvRequerido.ErrorMessage = "Debe de ingresar una fecha."
            rfvRequerido.Text = "!"
            rfvRequerido.Font.Size = System.Web.UI.WebControls.FontUnit.XSmall
            rfvRequerido.ForeColor = Color.Red
            Controls.Add(rfvRequerido)
        End If
    End Sub
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        'Page.Response.Clear()
        'CreateChildControls()
        EnsureChildControls()
        MyBase.Render(writer)
    End Sub

    <Bindable(True), Category("Appearance"), DefaultValue("/NuevoMundo/Imagenes"), Description("Ruta de la carpeta de imágenes.")> Public Property RutaImagenes() As String
        Get
            Return lstrRutaImagenes
        End Get
        Set(ByVal Value As String)
            lstrRutaImagenes = Value
        End Set
    End Property
    <Bindable(True), Category("Appearance"), DefaultValue("/NuevoMundo/JavaScripts/jsCalendario.js"), Description("Ruta del javascript del calendario.")> Public Property RutaScript() As String
        Get
            Return lstrRutaScript
        End Get
        Set(ByVal Value As String)
            lstrRutaScript = Value
        End Set
    End Property
    <Bindable(True), Category("Appearance"), DefaultValue(""), Description("Fecha.")> Public Property Fecha() As String
        Get
            EnsureChildControls()
            Return txtFecha.Text
        End Get
        Set(ByVal Value As String)
            EnsureChildControls()
            imgImagen.Src = "/NuevoMundo/Imagenes/JSCalendario/Calendario.gif"
            txtFecha.Text = Value
        End Set
    End Property
    <Bindable(True), Category("Appearance"), DefaultValue(""), Description("CssClass.")> Public Overrides Property CssClass() As String
        Get
            Return txtFecha.CssClass
        End Get
        Set(ByVal Value As String)
            txtFecha.CssClass = Value
            imgImagen.Src = "/NuevoMundo/Imagenes/JSCalendario/Calendario.gif"
            MyBase.CssClass = ""
        End Set
    End Property
    <Bindable(True), Category("Appearance"), DefaultValue("true"), Description("CssClass.")> Public Property Requerido() As Boolean
        Get
            Return mbooRequerido
        End Get
        Set(ByVal Value As Boolean)
            mbooRequerido = Value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        If Not Me.Page.IsClientScriptBlockRegistered("jsCalendario") Then
            Me.Page.RegisterClientScriptBlock("jsCalendario", "<script type ='text/javascript' src = ' /NuevoMundo/JavaScripts/jsCalendario.js'></script>")
        End If
        If Not Me.Page.IsClientScriptBlockRegistered("Validaciones") Then
            Me.Page.RegisterClientScriptBlock("Validaciones", "<script type ='text/javascript' src = ' /NuevoMundo/JavaScripts/Validaciones.js'></script>")
        End If
        imgImagen.Attributes.Add("onClick", "javascript:popUpCalendar(this,document.getElementById('" + txtFecha.ClientID + "'),'dd/mm/yyyy');")
        txtFecha.Attributes.Add("onBlur", "javascript:ValidarFechaObj('" + MyBase.ClientID + "_Texto');")
    End Sub

    Protected Overrides Function SaveViewState() As Object
        EnsureChildControls()
        Dim Estado(3) As Object
        Dim lobjBase As Object = MyBase.SaveViewState

        Estado(0) = lobjBase
        Estado(1) = txtFecha.Text
        Estado(2) = imgImagen.Src
        Return Estado
    End Function
    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        Dim Estado() As Object = savedState
        MyBase.LoadViewState(Estado(0))
        EnsureChildControls()
        txtFecha.Text = Estado(1).ToString
        imgImagen.Src = Estado(2).ToString
    End Sub
End Class
