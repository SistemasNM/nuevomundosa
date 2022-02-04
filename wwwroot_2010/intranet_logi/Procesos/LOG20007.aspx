<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20007.aspx.vb" Inherits="intranet_logi.LOG20007"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Historial</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="dtgDetalle" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				AutoGenerateColumns="False" Width="448px">
				<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
				<ItemStyle CssClass="griditem"></ItemStyle>
				<HeaderStyle CssClass="gridheader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="var_Estado" HeaderText="Estado"></asp:BoundColumn>
					<asp:BoundColumn DataField="var_Usuario" HeaderText="Usuario"></asp:BoundColumn>
					<asp:BoundColumn DataField="var_Fecha" HeaderText="Fecha"></asp:BoundColumn>
				</Columns>
			</asp:datagrid>
		</form>
	</body>
</html>
