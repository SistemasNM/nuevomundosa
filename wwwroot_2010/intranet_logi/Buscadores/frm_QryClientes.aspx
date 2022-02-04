<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_QryClientes.aspx.vb" Inherits="intranet_logi.frm_QryClientes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_Clientes</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
        <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
		    returnValue = "";
		    function btnEscoger_Onclick(Codigo, Nombre, Estado) {
		        returnValue = Codigo + ":" + Nombre + ":" + Estado;
		        //alert(returnValue);
		        window.close();
		    }

		    function Cerrar() {
		        window.close();
		    }
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="440" border="0" cellspacing="2" cellpadding="1">
				<tr>
					<td class="Cabecera">Busqueda de Clientes
					</td>
				</tr>
				<tr>
					<td><table width="100%" border="0" cellspacing="2" cellpadding="1">
							<tr>
								<td width="80" class="Etiqueta">Código</td>
								<td width="300">
									<asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox></td>
							</tr>
							<tr>
								<td width="80" class="Etiqueta">Nombre</td>
								<td width="300">
									<asp:TextBox id="txtNombre" runat="server" CssClass="input" Width="296px"></asp:TextBox></td>
							</tr>
							<tr>
								<td colspan="2" class="Footer">
									<asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="boton"></asp:Button>&nbsp;<INPUT class="boton" id="btnCerrar" type="button" value="Cerrar" onclick="Cerrar()" name="btnCerrar">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="260" valign="top"><div id="divDatos" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px">
							<asp:DataGrid id="dgDatos" runat="server" AutoGenerateColumns="False" Width="350px">
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<INPUT class="input" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="...">
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
</HTML>
