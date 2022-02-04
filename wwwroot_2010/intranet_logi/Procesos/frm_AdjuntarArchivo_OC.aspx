<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_AdjuntarArchivo_OC.aspx.vb" Inherits="intranet_logi.frm_AdjuntarArchivo_OC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjuntar archivos</title>
 
    <base target="_self"/>
    <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />

  <style type="text/css">
      .pnlModal
      {
          background-color :#FFFFFF;
          border:solid 1px #000000;
          color:Black;
          font-family:Arial;
          font-size:10pt;	
          padding:3px 3px 3px 3px;
      }
      .pnlTitulo{
          background-color: #BCCAE0;;
          color:#333333;
          font-size:10pt;
          font-family:Arial;
          font-weight:bold;
          cursor:move;
          padding:3px 3px 3px 3px;
      }
  </style>
</head>
<body>
 <form id="form1" runat="server">
  <center>
   <%--Parte 1: Controles ocultos--%>
   <table style="WIDTH: 600px; height:20px" cellspacing="0" cellpadding="0" border="0">
    <tr>
	 <td style="WIDTH:600px; HEIGHT:20px" align="right">
      <input id="hdnTipoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
      <input id="hdnCodigoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
      <input id="hdnSecuencia" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnSecuencia" runat="server"/>
      <input id="hdnDestinoAbrir" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoAbrir" runat="server"/>
      <input id="hdnDestinoGuardar" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoGuardar" runat="server"/>
     </td>
    </tr>
    </table>

   <%--Parte 4: Adjuntar archivo--%>
   <asp:Panel ID="pnlCargaFile" runat="server" CssClass="pnlModal" Width="600px">
    <table style="WIDTH: 600px; height:40px" cellspacing="0" border="0" cellpadding = "0">
     <tr>
      <td style="WIDTH: 600px; height:10px" align="left" class="pnlTitulo" colspan = "2">Cargar archivos:</td>
     </tr>
     <tr>
      <td style="WIDTH: 600px; height:10px" align="left" colspan = "2"></td>
     </tr>
     <tr>
      <td align="left" style="WIDTH: 480px; height:20px">
       <input style="WIDTH: 450px; HEIGHT: 20px; margin-bottom: 0px;" id="File1" size="75" type="file" name="File1" class="Boton" runat="server" dir="ltr"/>
      </td>
      <td class="Etiqueta" style="WIDTH: 200px" valign="middle" align="right" >
       <asp:button ID="btnSubir" runat="server" Text = "Subir Archivo" CssClass = "Boton" Width = "120px" Height="20PX"></asp:button>
      </td>
     </tr>
     </table>

     <table style="WIDTH: 600px; height:40px" cellspacing="0" border="0" cellpadding = "0">
     <tr>
      <td class="Etiqueta" style="WIDTH: 400px" valign="middle" align = "left">Descripcion:</td>
      <td class="Etiqueta" style="WIDTH: 200px" valign="middle" align = "left">Categoria:</td>
     </tr>
     <tr>
	  <td class="Etiqueta" style="WIDTH: 400px" valign="middle" align = "left">
       <asp:textbox id="txtNombreCorto" runat="server" Width="380px" CssClass="input" MaxLength="50" Font-Size = "10px"></asp:textbox>
      </td>
      <td class="Etiqueta" style="WIDTH: 200px" valign="middle" align = "left">
       <asp:DropDownList id="ddlTipoContenido" runat="server" Width="198px" CssClass="input"></asp:DropDownList>
      </td>
     </tr>
     <tr>
      <td class="Etiqueta" style="WIDTH: 600px" valign="middle" align = "left" colspan="2">Observaciones:</td>
     </tr>
     <tr>
	  <td class="Etiqueta" style="WIDTH: 600px" valign="middle" align = "left" colspan="2">
       <asp:textbox id="txtObservacion" runat="server" Width="590px" CssClass="input" MaxLength="100" height="30px" TextMode="MultiLine" Font-Size="10px"></asp:textbox>
      </td>
     </tr>
     <tr>
	  <td style="WIDTH: 600px" valign="middle" align = "left" colspan="2">
       <asp:Label ID="lblMensaje" runat="server" Text = "" Width="600px" CssClass="error"></asp:Label>
      </td>
     </tr>
      
     </table>
   </asp:Panel>

  </center>
 </form>
</body>
</html>
