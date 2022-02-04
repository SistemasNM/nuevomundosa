Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Security.Permissions
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class NMNumericText
    Inherits System.Web.UI.WebControls.TextBox

    Private mstrScript1 As String = "var tecla = window.event.keyCode;if (tecla < 48 || tecla > 57){window.event.keyCode=0;}"
    Private mstrScript2 As String = "var tecla = window.event.keyCode;if ((tecla < 48 || tecla > 57) && (tecla != 46) ){window.event.keyCode=0;}"
    Private mstrOnBlur As String = ""
    Private mintTipo As Integer = 0

    Enum enuTypeText
        [Integer] = 0
        [Double] = 1
    End Enum

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        If Not Me.Page.IsClientScriptBlockRegistered("jsValidaciones") Then
            Me.Page.RegisterClientScriptBlock("jsValidaciones", "<script type ='text/javascript' src = '/NuevoMundo/JavaScripts/Validaciones.js'></script>")
        End If
        If Not Me.Page.IsClientScriptBlockRegistered("jsTextBox") Then
            Me.Page.RegisterClientScriptBlock("jsTextBox", "<script type ='text/javascript' src = '/NuevoMundo/JavaScripts/jsTextBox.js'></script>")
        End If
        MyBase.Attributes.Add("onBlur", "PonerComas('" + MyBase.ClientID + "');" + mstrOnBlur)
        MyBase.Attributes.Add("onFocus", "QuitarComas('" + MyBase.ClientID + "')")
        Select Case mintTipo
            Case enuTypeText.Integer
                MyBase.Attributes.Add("onKeyPress", mstrScript1)
            Case enuTypeText.Double
                MyBase.Attributes.Add("onKeyPress", mstrScript2)
        End Select
    End Sub

    <Bindable(True), Category("Appearance"), DefaultValue("0"), Description("Tipo de caracteres permitidos.")> Public Property TypeText() As enuTypeText
        Get
            Return mintTipo
        End Get
        Set(ByVal Value As enuTypeText)
            mintTipo = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue(""), Description("")> Public Property onBlur() As String
        Get
            Return mstrOnBlur
        End Get
        Set(ByVal Value As String)
            mstrOnBlur = Value
        End Set
    End Property

    Protected Overrides Function SaveViewState() As Object
        EnsureChildControls()
        Dim Estado(3) As Object
        Dim lobjBase As Object = MyBase.SaveViewState

        Estado(0) = lobjBase
        Estado(1) = MyBase.Text
        Estado(2) = mintTipo
        Estado(3) = mstrOnBlur
        Return Estado
    End Function
    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        Dim Estado() As Object = savedState
        MyBase.LoadViewState(Estado(0))
        EnsureChildControls()
        MyBase.Text = Estado(1).ToString
        mintTipo = Estado(2).ToString
        mstrOnBlur = Estado(3).ToString
    End Sub
End Class
