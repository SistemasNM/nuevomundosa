<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LOG20013.aspx.vb" Inherits="intranet_logi.LOG20013" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
	</head>
	<body>
		<form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellspacing="2"
				cellpadding="2" width="720" height="16" border="0">
				<TBODY>
					<tr>
						<td class="cabecera" style="HEIGHT: 22px">
							&nbsp;EJECUCION MANUAL DE TRANSFERENCIA&nbsp;A ALMACENES FISICOS</td>
					</tr>
					<tr>
						<td class="panel">
							<table style="WIDTH: 720px; HEIGHT: 16px">
					</tr>
			</table>
			<table style="WIDTH: 784px; HEIGHT: 40px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Almacén Transfe.&nbsp;:</td>
					<td style="WIDTH: 290px" width="290">&nbsp;
						<asp:DropDownList id="ddlalmtransf" runat="server" CssClass="input" Width="280px"></asp:DropDownList></td>
					<td style="WIDTH: 2px; HEIGHT: 8px" width="2">&nbsp;</td>
					<td style="HEIGHT: 8px" colspan="2">&nbsp;
						<asp:button id="btnbuscar" runat="server" CssClass="Boton" Text="Buscar"></asp:button>&nbsp;
						<asp:button id="btnEjecutar" runat="server" CssClass="Boton" Text="Ejecutar"></asp:button></td>
				</tr>
			</table>
			<table>
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" Width="608px" AutoGenerateColumns="False">
							<FooterStyle CssClass="GridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Articulo">
									<HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
									<HeaderTemplate>
										<table cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td class="GridHeader" width="100">
													<asp:Panel id="Panel34" runat="server" Width="120px">Código</asp:Panel></td>
												<td class="GridHeader" width="100%">
													<asp:Panel id="Panel38" runat="server" Width="120px">Descripción</asp:Panel></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table1" style="WIDTH: 434px; HEIGHT: 17px" height="17" cellspacing="1" cellpadding="1"
											width="434" border="0">
											<tr>
												<td width="100">
													<asp:Label id="lblcodarticuloi" runat="server" Width="100%" CssClass="input" text='<%#Container.DataItem("vch_codarticulo")%>'>
													</asp:Label></td>
												<td width="100%">
													<asp:Label id="lbldesarticuloi" runat="server" Width="297px" CssClass="input" text='<%#Container.DataItem("de_item")%>' Height="12px">
													</asp:Label></td>
											</tr>
										</table>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Centro Costo">
									<HeaderTemplate>
										<table id="Table30" style="WIDTH: 81px; HEIGHT: 28px" cellspacing="1" cellpadding="1" width="81"
											border="0">
											<tr>
												<td class="GridHeader" width="80">
													<asp:Panel id="Panel1" runat="server" Width="80px">Cantidad</asp:Panel>
												</td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table22" style="WIDTH: 81px; HEIGHT: 20px" cellspacing="1" cellpadding="1" width="97"
											border="0">
											<tr>
												<td width="80">
													<asp:Label id="lblcodalmtransfi" runat="server" CssClass="input" text='<%#Container.DataItem("num_cantidad")%>'>
													</asp:Label></td>
											</tr>
										</table>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
	    </form>
	</body>
</html>
