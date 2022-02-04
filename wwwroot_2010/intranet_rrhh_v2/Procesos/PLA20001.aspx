<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20001.aspx.vb" Inherits="intranet_rrhh_v2.PLA20001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA20001</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
        
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 32px"
				cellspacing="2" cellpadding="2" width="100%" border="0">
				<tr>
					<td class="Cabecera">Modificación 
						de cuentas para caja chica</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table2" style="WIDTH: 691px; HEIGHT: 96px" cellspacing="2" cellpadding="2" width="691"
							border="0">
							<tr>
								<td>
									<asp:DataGrid id="dtgCuentas" runat="server" AutoGenerateColumns="False" Width="632px">
										<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem"></ItemStyle>
										<HeaderStyle CssClass="GridHeader"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="var_CuentaContable" HeaderText="Cuenta"></asp:BoundColumn>
											<asp:BoundColumn DataField="var_NombreCuenta" HeaderText="Nombre Cuenta"></asp:BoundColumn>
											<asp:BoundColumn DataField="var_LlevaDocumento" HeaderText="Lleva documento"></asp:BoundColumn>
											<asp:BoundColumn DataField="var_Documento" HeaderText="Tipo documento"></asp:BoundColumn>
										</Columns>
									</asp:DataGrid></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="HEIGHT: 16px" cellspacing="2" cellpadding="2"
							width="100%" border="0">
							<tr>
								<td align="right">
									<asp:button id="btnIncluir" runat="server" CssClass="boton" Text="Incluir CCH"></asp:button>&nbsp;
									<asp:button id="btnExcluir" runat="server" CssClass="boton" Text="Quitar doc."></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
