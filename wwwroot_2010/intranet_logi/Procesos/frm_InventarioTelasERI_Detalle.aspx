<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_InventarioTelasERI_Detalle.aspx.vb" Inherits="intranet_logi.frm_InventarioTelasERI_Detalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Busqueda de Rollos</title>
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
	<td class="Cabecera" style="width:450px; height:30px">Rollos Faltantes</td>
   </tr>
   <tr>
	<td>
	 <table cellspacing="0" cellpadding="0" border="0" style="width: 667px">
      <tr>
	   <td style="width:100px" class="Etiqueta">Ubicacion</td>
       <td style="width:150px"><asp:TextBox id="txtCodigo" runat="server" CssClass="input" Enabled="false"></asp:TextBox></td>
       <td>
        <input style="background-color:red;width:90px;font-weight:bold;height:30px;color:White;" type="button" value="FALTANTE"/>
        <input style="background-color:blue;width:90px;font-weight:bold;height:30px;color:White;" type="button" value="SOBRANTE"/>
       </td>
     </tr>
     <tr>
        <td>&nbsp;</td>
     </tr>	 	 
	</table>
   </td>
  </tr>
  <tr>
   <td>
	<div id="divDatos">
     <asp:GridView id="dgDatos" runat="server" width="670px" AutoGenerateColumns="False" EnableViewState="False">
     <FooterStyle CssClass="GridFooter"></FooterStyle>
	 <AlternatingRowStyle CssClass="GridAlternateItem"></AlternatingRowStyle>
	 <RowStyle CssClass="GridItem"></RowStyle>
	 <HeaderStyle CssClass="GridHeader"></HeaderStyle>
	 
     <Columns>
       
       <asp:BoundField DataField="ubic_inv" HeaderText="Ubic. Inv.">
		<HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundField>

       <asp:BoundField DataField="codigo_rollo" HeaderText="Rollo">
		<HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundField>

       <asp:BoundField DataField="des_articulo" HeaderText="Articulo">
		<HeaderStyle HorizontalAlign="Center" Width="500px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundField>
        
       <asp:BoundField DataField="ca_actu" HeaderText="Mts. Sis." DataFormatString="{0:N2}">
		<HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundField>

       <asp:BoundField DataField="ca_inv" HeaderText="Mts. Inv." DataFormatString="{0:N2}">
		<HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundField>


       <asp:TemplateField HeaderText="Dif.">
        <ItemTemplate>
            <asp:Label ID="lblDif" runat="server" Text='<%#Container.DataItem("ca_dif")%>' Width="55px"></asp:Label>
        </ItemTemplate>
       </asp:TemplateField>

       <asp:BoundField DataField="ubic_sis" HeaderText="Ubic. Sis.">
		<HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Middle"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
	   </asp:BoundField>

	  </Columns>
	 </asp:GridView>
    </div>
   </td>
  </tr>
 </table>
</form>
</body>
</html>

