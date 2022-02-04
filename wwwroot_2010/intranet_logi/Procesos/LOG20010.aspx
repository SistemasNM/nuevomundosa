<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20010.aspx.vb" Inherits="intranet_logi.LOG20010"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20010</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 456px; POSITION: absolute; TOP: 0px; HEIGHT: 230px"
				cellspacing="2" cellpadding="2" width="456" border="0">
				<tr>
					<td>
						<table id="Table4" style="HEIGHT: 32px" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 64px">Articulo</td>
								<td>
									<asp:TextBox id="txtArticuloCodigo" runat="server" CssClass="Input" Width="216px"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 64px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<asp:TextBox id="txtArticuloNombre" runat="server" CssClass="Input" Width="100%"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="Cabecera">Última compra</td>
				</tr>
				<tr>
					<td>
						<table id="Table2" style="HEIGHT: 32px" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 64px">Precio</td>
								<td>
									<asp:TextBox id="txtPrecio" runat="server" CssClass="Input"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 64px">Proveedor&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<asp:TextBox id="txtProveedor" runat="server" CssClass="Input" Width="100%"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="Cabecera">Alternativas</td>
				</tr>
				<tr>
					<td>
						<table id="Table3" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 89px">1 
									:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<asp:TextBox id="txtAlternativa1" runat="server" CssClass="Input" Width="100%"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 89px">2 :</td>
								<td>
									<asp:TextBox id="txtAlternativa2" runat="server" CssClass="Input" Width="100%"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 89px">3:</td>
								<td>
									<asp:TextBox id="txtAlternativa3" runat="server" CssClass="Input" Width="100%"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			&nbsp;
		</form>
	</body>
</html>
