<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PruebaCorreo.aspx.vb" Inherits="intranet_logi.PruebaCorreo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" />
        <asp:Button ID="Button1" runat="server" Text="comprueba Correo Log" />
        <asp:Button ID="Button2" runat="server" Text="Enviar Smtp" />
        <br />
        <br />
        <asp:TextBox ID="txtUsuario" runat="server" Text="alertas@nuevomundosa.com"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtPass" runat="server" Text="PASS"></asp:TextBox>
        <br />                
        <asp:TextBox ID="txtServidor" runat="server" Text="SERVIDOR"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtPuerto" runat="server" Text="PUERTO"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtSubject" runat="server" Text="SUBJECT"></asp:TextBox>
        <asp:RadioButtonList ID="rdbSSL" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1">Enable SSL</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">Disable SSL</asp:ListItem>            
        </asp:RadioButtonList>
        <p style="float:left;">Attachment:</p>
        <asp:RadioButtonList ID="rdbAttachment" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1">Si</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>            
        </asp:RadioButtonList>        
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Text="TO" Width="248px" ></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Text="COPY" Width="300px" ></asp:TextBox>
    </div>
    </form>
</body>
</html>
