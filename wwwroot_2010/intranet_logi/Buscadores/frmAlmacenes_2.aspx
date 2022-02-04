<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAlmacenes_2.aspx.vb" Inherits="intranet_logi.frmAlmacenes_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Busqueda de almacenes</title>
 <base target="_self"/>
 <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
 <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
 <meta name="vs_defaultClientScript" content="JavaScript"/>
 <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 
 <script language="javascript" type="text/javascript">
     returnValue = "";

     function btnEscoger_Onclick(Codigo, Nombre) {
         returnValue = Codigo + ":" + Nombre;
         window.close();
     }

     function Cerrar() {
         window.close();
     }

 </script>
</head>
<body>
 <form id="frmAlmacen" method="post" runat="server">
  <table cellspacing="0" cellpadding="0" style="width:400px">
   <tr>
	<td class="Cabecera" style="width:450px; height:30px">Busqueda de almacenes</td>
   </tr>
   <tr>
	<td>
	 <table cellspacing="0" cellpadding="0" width="400px" border="0">
      <tr>
	   <td style="width:100px" class="Etiqueta">Codigo</td>
       <td style="width:300px"><asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox></td>
     </tr>
	 <tr>
	  <td style="width:100px" class="Etiqueta">Nombre</td>
      <td style="width:300px"><asp:TextBox id="txtNombre" runat="server" CssClass="input"></asp:TextBox></td>
     </tr>
	 <tr>
	  <td style="width:400px;" colspan="2" align="right">
		<asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="Boton"></asp:Button>
        &nbsp;<input id="btnCerrar" onclick="Cerrar()" type="button" value="Cerrar" name="btnCerrar" class="Boton"/>
      </td>
	 </tr>
	</table>
   </td>
  </tr>
  <tr>
   <td>
	<div id="divDatos">
     <asp:DataGrid id="dgDatos" runat="server" width="400px" AutoGenerateColumns="False" EnableViewState="False">
     <FooterStyle CssClass="GridFooter"></FooterStyle>
	 <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	 <ItemStyle CssClass="GridItem"></ItemStyle>
	 <HeaderStyle CssClass="GridHeader"></HeaderStyle>
	 
     <Columns>
	  <asp:TemplateColumn>
       <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
	   <ItemTemplate>
        <input runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." class="Boton"  style="WIDTH: 20px; HEIGHT: 20px"/>
       </ItemTemplate>
	   </asp:TemplateColumn>
       
       <asp:BoundColumn DataField="co_alma" HeaderText="Codigo">
		<HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundColumn>

       <asp:BoundColumn DataField="des_alma" HeaderText="Nombre">
		<HeaderStyle HorizontalAlign="Center" Width="350px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundColumn>

	  </Columns>
	 </asp:DataGrid>
    </div>
   </td>
  </tr>
 </table>
</form>
</body>
</html>
