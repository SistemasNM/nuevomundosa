<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30012.aspx.vb" Inherits="intranet_rrhh.PLA30012"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30012</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript">
			function popUp(strUrl) 
			{
				var intWidth = screen.width;
				var intHeight = screen.height;
				//window.open(strUrl);
				window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
			}	
		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 971px; POSITION: absolute; TOP: 8px; HEIGHT: 80px"
				cellspacing="2" cellpadding="2" width="971" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Estado 
						de Cta - Trabajadores
					</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 40px; HEIGHT: 26px" cellspacing="1" cellpadding="1" width="112"
							border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 44px; HEIGHT: 11px">&nbsp;Codigo :</td>
								<td style="WIDTH: 90px; HEIGHT: 11px"><asp:textbox id="txtCodigo" runat="server" MaxLength="5" Font-Size="XX-Small" Font-Names="Verdana"
										Width="56px" Height="20px"></asp:textbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="WIDTH: 958px; HEIGHT: 32px" cellspacing="2"
							cellpadding="2" width="958" border="0">
							<tr>
								<td style="WIDTH: 839px" align="right">
									<asp:button id="btnConsultar" runat="server" CssClass="boton" Text="Consultar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;<asp:button id="btnBoleta" runat="server" Text="Boleta" CssClass="boton"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;<asp:button id="btnFoto" runat="server" Text="Ver Foto" CssClass="boton"></asp:button></td>
								<td align="right"><asp:button id="btnVer" runat="server" Text="Salir" CssClass="boton"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:Image id="Image1" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 120px" runat="server"
				Width="150px" Height="200px"></asp:Image></form>
	</body>
</html>
