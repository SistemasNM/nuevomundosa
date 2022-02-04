<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_SecuenciaAprobacionOrdenes.aspx.vb" Inherits="intranet_logi.frm_SecuenciaAprobacionOrdenes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grupo de aprobacion</title>
    <base target="_self"/>
    <link href="../css/sytle.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>

    <script language="javascript" type="text/javascript">
        returnValue = "";
        function btnEscoger(Codigo, Nombre) {
            returnValue = Codigo + ":" + Nombre;
            window.close();
        }

        function Cerrar() {
            window.close();
        }
		</script>
</head>
<body>
<center>
 <form id="frm_SecAprobLog" runat="server">
  <!-- Inicio: Cabecera --> 
  <div id="Cabecera">
   <table cellpadding="0" cellspacing="0" border="0" width="500px">
    <tr>
     <td class="Cabecera" width="500px" height="30px"> SECUENCIA DE APROBACION DE OC/OS</td>
    </tr>
   </table>
  </div>
  <!-- Fin: Cabecera --> 

  <!-- Inicio: Filtros --> 
  <div id="Filtros">
   <table cellpadding="0" cellspacing="0" border="0" width="500px">
    <tr>
     <td style="WIDTH:100px" align="left" class="Etiqueta">Código</td>
     <td width="300" align="left"><asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox></td>
    </tr>
	<tr>
	 <td style="WIDTH:100px" align="left" class="Etiqueta">Nombre</td>
     <td width="300" align="left"><asp:TextBox id="txtNombre" runat="server" CssClass="input"></asp:TextBox></td>
    </tr>
	<tr>
     <td class="Footer" colspan="2">
	  <asp:Button id="btnBuscar" runat="server" CssClass="Boton" Text="Buscar"></asp:Button>
      &nbsp;<input id="btnCerrar" onclick="Cerrar()" type="button" value="Cerrar" name="btnCerrar" class="Boton"/>
      </td>
      
    </tr>
    <tr>
     <td style="WIDTH:500px" align="left" colspan="2">
      <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
      <input style="WIDTH: 10px; HEIGHT: 10px" id="hdnNumero" size="1" type="hidden" name="hndNumero" runat="server"/>
     </td>
    </tr>
   </table>
  </div>
  <!-- fin: Filtros --> 

  <!-- Inicio: Grilla --> 
  <div id="Datos">
   <asp:DataGrid id="dgDatos" runat="server" Width="500px" AutoGenerateColumns="False" EnableViewState="False">
   <FooterStyle CssClass="GridFooter"></FooterStyle>
   <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
   <ItemStyle CssClass="GridItem"></ItemStyle>
   <HeaderStyle CssClass="GridHeader"></HeaderStyle>
   <Columns>
    
     <asp:TemplateColumn>
	  <ItemTemplate>
       <asp:Button ID="btnEscoger" runat="server" CssClass="Boton" Width="20px" Height="20px" Text="..."  CommandName="Escoger"/>
      </ItemTemplate>
     </asp:TemplateColumn>

	 <asp:TemplateColumn>
      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Font-Bold="true"></HeaderStyle>
	  <ItemTemplate>
       <asp:Label id="lblCodGrupo" runat="server" Font-Size="9px" Text='<%# DataBinder.Eval(Container, "DataItem.CodigoGrupo") %>' visible ="true"></asp:Label>
      </ItemTemplate>
	 </asp:TemplateColumn>

     <asp:BoundColumn DataField="DescripcionGrupo" HeaderText="Nombre" HeaderStyle-Font-Bold = "true">
      <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="400px"></ItemStyle>
     </asp:BoundColumn>

	</Columns>
   </asp:DataGrid>
  </div>
  <!-- fin: Grilla --> 
 </form>
</center>
</body>
</html>
