<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_EnvioCorreoOrdenes.aspx.vb" Inherits="intranet_logi.frm_EnvioCorreoOrdenes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enviar correo electronico</title>
    <base target="_self"/>
    <link href="../css/sytle.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
    
    <script language="javascript" type="text/javascript">
        function fnc_Cerrar() {
            window.close();
        }
    </script>
</head>
<body>
 <center>
  <form id="frm_EnvioEmail" runat="server">
   <div class="pnlModal" style="width:500px">
    <table cellpadding="0" cellspacing="0" width="500px">
     <tr>
      <td style="width:500px" align="right" colspan="2" id="Descripcion">Envio de correo electronico</td>
     </tr>
     <tr>
      <td style="width:500px" align="right" colspan="2"></td>
     </tr>
     <tr>
      <td style="width:100px" align="left" class="Etiqueta">Asunto:</td>
      <td style="width:400px" align="left"><asp:TextBox ID="txtAsunto" runat="server" Text="" CssClass="input" Font-Size = "9px" Width="400px" Font-Bold="true"></asp:TextBox></td>
     </tr>
     <tr>
      <td style="width:100px" align="left" class="Etiqueta">Para:</td>
      <td style="width:400px" align="left"><asp:TextBox ID="txtPara" runat="server" Text="" CssClass="input" Font-Size = "9px" Width="400px"></asp:TextBox></td>
     </tr>
     <tr>
      <td style="width:100px" align="left" class="Etiqueta">CC:</td>
      <td style="width:400px" align="left"><asp:TextBox ID="txtCopia" runat="server" Text="" CssClass="input" Font-Size = "9px" Width="400px"></asp:TextBox></td>
     </tr>
     <tr>
      <td style="width:100px" align="left" class="Etiqueta">Comentarios:</td>
      <td style="width:400px" align="left"><asp:TextBox ID="txtComentario" runat="server" 
              Text="" CssClass="input" Font-Size = "10px" Width="400px" TextMode="MultiLine" 
              Height="60px"></asp:TextBox></td>
     </tr>
     <tr>
      <td style="width:100px" align="left"></td>
      <td style="width:400px" align="left"><asp:Label ID="lblMensaje" Text="" runat="server" CssClass="mensaje" Width="400px"></asp:Label></td>
     </tr>
     <tr>
      <td style="width:100px" align="left"></td>
      <td style="width:400px" align="right">
        <input id="hdnCodigoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
        <input id="hndComprador" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hndComprador" runat="server"/>
        <input id="hndFechaOC" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hndFechaOC" runat="server"/>
        <input id="hndProveedor" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hndProveedor" runat="server"/>
        <input id="hndCorreoPara" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hndCorreoPara" runat="server"/>
        <input id="hndCorreoCopia" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hndCorreoCopia" runat="server"/>
       <asp:Button ID="btnenviar" runat="server" Text="Enviar" CssClass="btnAzul"/>
       <asp:Button ID="btnCerrar" runat="server" Text="Salir" CssClass="btnAzul"/>
      </td>
     </tr>
    </table>
   </div>
  </form>
 </center>
</body>
</html>
