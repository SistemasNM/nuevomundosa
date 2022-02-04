<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Clientes.aspx.vb" Inherits="intranet_logi.frm_Clientes" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
		<title>frm_Clientes</title>
		<base target="_self" />
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="../css/Styles.css" rel="Stylesheet" type="text/css"/>
        <link href="../css/NM0001.css" type="text/css" rel="Stylesheet" />
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
    
    <form id="Form1" method="post" runat="server">
			<table width="440" border="0" cellspacing="2" cellpadding="1">
				<tr>
					<td class="Cabecera">Busqueda de Clientes
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
									<asp:TextBox id="txtNombre" runat="server" CssClass="input" Width="296px"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="2" class="Footer">
									<asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="boton"></asp:Button>&nbsp;<input class="boton" id="btnCerrar" type="button" value="Cerrar" onclick="Cerrar()" name="btnCerrar">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="260" valign="top"><div id="divDatos" style="OVERFLOW: auto; width: 100%; height: 250px">
							<asp:DataGrid id="dgDatos" runat="server" AutoGenerateColumns="False" Width="350px">
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<input class="input" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="var_CodigoCliente" HeaderText="C&#243;digo"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_NombreCliente" HeaderText="Nombre y Apellido / Raz&#243;n Social"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid>
						</div>
					</td>
				</tr>
			</table>
		</form>

</body>
</html>
