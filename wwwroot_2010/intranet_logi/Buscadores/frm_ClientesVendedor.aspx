<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ClientesVendedor.aspx.vb" Inherits="intranet_logi.frm_ClientesVendedor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>frm_ClientesVendedor</title>
		<base target="_self" />
		<meta name="GENERATOR"  content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE"  content="Visual Basic .NET 7.1" />
		<meta name="vs_defaultClientScript"  content="JavaScript" />
		<meta name="vs_targetSchema"  content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="javascript">
		    returnValue = "";
		    function btnEscoger_Onclick(Codigo, Nombre, CodVendedor, NomVendedor) {
		        returnValue = Codigo + ":" + Nombre + ":" + CodVendedor + ":" + NomVendedor;
		        window.close();
		    }

		    function Cerrar() {
		        window.close();
		    }
		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table width="640" border="0" cellspacing="2" cellpadding="1" style="WIDTH: 640px; HEIGHT: 385px">
				<tr>
					<td class="Cabecera">Busqueda de Clientes&nbsp;x Vendedor
					</td>
				</tr>
				<tr>
					<td><table width="100%" border="0" cellspacing="2" cellpadding="1">
							<tr>
								<td width="80" class="Titulo">Código</td>
								<td width="300">
									<asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox></td>
							</tr>
							<tr>
								<td width="80" class="Titulo">Nombre</td>
								<td width="300">
									<asp:TextBox id="txtNombre" runat="server" CssClass="input" Width="216px"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="2" class="Footer">
									<asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="boton"></asp:Button>&nbsp;<input  class="boton" id="btnCerrar" type="button" value="Cerrar" onclick="Cerrar()" name="btnCerrar" />&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="260" valign="top"><div  id="divDatos" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px">
							<asp:DataGrid id="dgDatos" runat="server" AutoGenerateColumns="False" Width="624px" Height="128px">
								<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<input  class="input" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CO_CLIE" HeaderText="C&#243;digo"></asp:BoundColumn>
									<asp:BoundColumn DataField="NO_CLIE" HeaderText="Descripci&#243;n Cliente"></asp:BoundColumn>
									<asp:BoundColumn DataField="CO_VEND" HeaderText="C&#243;digo"></asp:BoundColumn>
									<asp:BoundColumn DataField="NO_VEND" HeaderText="Descripci&#243;n Vendedor"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
