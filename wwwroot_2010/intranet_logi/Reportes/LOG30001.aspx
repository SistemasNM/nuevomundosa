<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG30001.aspx.vb" Inherits="intranet_logi.LOG30001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG30001</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
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
			<table id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 96px"
				cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
				<tr>
					<td class="Cabecera">Listado de Insumos</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table1" style="WIDTH: 480px; FONT-FAMILY: Verdana; HEIGHT: 29px" cellspacing="2" cellpadding="2" width="480" border="0">
							<tr>
								<td class="etiqueta" style="WIDTH: 6px; HEIGHT: 13px">Insumo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td style="WIDTH: 82px; HEIGHT: 13px">
									<asp:textbox id="txtInsumo" runat="server" Height="20px" Width="512px"></asp:textbox></td>
							</tr>
						</table>
						<table id="Table3" style="HEIGHT: 8px" cellspacing="2" cellpadding="2" width="100%" border="0"
							class="botonera">
							<tr>
								<td align="right">
									<asp:button id="btnVisualizar" runat="server" Height="24px" Width="105px" Font-Bold="True" Text="Ver reporte"
										CssClass="boton"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>