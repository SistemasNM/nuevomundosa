<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG30007.aspx.vb" Inherits="intranet_logi.LOG30007"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG30007</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
			function popUp(strUrl) 
			{
				var intWidth = screen.width;
				var intHeight = screen.height;
				//window.open(strUrl);
				window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
			}	
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 56px"
				cellspacing="2" cellpadding="2" width="100%" border="0">
				<tr>
					<td class="Cabecera">&nbsp;Maestro de proveedores</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table3" style="WIDTH: 480px; HEIGHT: 8px" cellspacing="2" cellpadding="2" width="480"
							border="0">
							<tr>
								<td class="etiqueta" style="WIDTH: 86px">Flags</td>
								<td><asp:checkbox id="cbxRetenedor" runat="server" Text="Retenedor" CssClass="input"></asp:checkbox><asp:checkbox id="cbxBuenContribuyente" runat="server" Text="Buen contribuyente" CssClass="input"></asp:checkbox></td>
							</tr>
						</table>
						<table id="Table2" style="HEIGHT: 8px" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="botonera" align="right"><asp:button id="btnExportar" runat="server" Width="98px" Text="Exportar" CssClass="boton"></asp:button><asp:button id="btnReporte" runat="server" Width="98px" Text="Ver reporte" CssClass="boton"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
