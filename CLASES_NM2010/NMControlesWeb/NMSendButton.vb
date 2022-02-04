Imports System
Imports System.ComponentModel
Imports System.Web.UI

<DefaultProperty("TextoEnviando"), ToolboxData("<{0}:NMSendButton runat=server ></{0}:NMSendButton>")> Public Class NMSendButton
    Inherits System.Web.UI.WebControls.Button

    Private mstrTextoEnviando As String = ""

    Public Sub New()
        'MyBase.CausesValidation = False
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        'Creando el panel donde va el botón principal
        writer.Write("<div id='div1_" + MyBase.ID + "' style='display: inline'>")
        writer.AddAttribute("onClick", "EnviarBoton('" + MyBase.ID + "');")
        MyBase.Render(writer)
        writer.Write("</div>")
        'Creando el panel y el boton secundario de envio
        writer.Write("<div id='div2_" + MyBase.ID + "' style='display: none'>")
        writer.Write("<input disabled type=submit value='" + IIf(mstrTextoEnviando = "", "Enviando...", mstrTextoEnviando) + "' />")
        writer.Write("</div>")
    End Sub

    <Bindable(True), Category("Appearance"), DefaultValue(""), Description("Texto al hacer el postback.")> Public Property TextoEnviando() As String
        Get
            Return mstrTextoEnviando
        End Get
        Set(ByVal Value As String)
            mstrTextoEnviando = Value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        If Not Me.Page.IsClientScriptBlockRegistered("Global") Then
            Me.Page.RegisterClientScriptBlock("Global", "<script type ='text/javascript' src = ' /NuevoMundo/JavaScripts/Global.js'></script>")
        End If
        MyBase.OnPreRender(e)
    End Sub
End Class
