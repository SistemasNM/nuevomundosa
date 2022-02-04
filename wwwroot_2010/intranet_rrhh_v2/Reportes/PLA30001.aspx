<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30001.aspx.vb" Inherits="intranet_rrhh.PLA30001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30001</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
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
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 32px"
				cellspacing="2" cellpadding="2" width="100%" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Movimiento 
						de personal</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table2" style="WIDTH: 248px; HEIGHT: 16px" cellspacing="2" cellpadding="2" width="248"
							border="0">
							<tr>
								<td class="etiqueta" style="WIDTH: 88px">Fecha</td>
								<td><asp:textbox id="txtFechaInicial" runat="server" Height="18px" Width="120px" Font-Names="Verdana"
										Font-Size="XX-Small" CssClass="input"></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, Form1.txtFechaInicial, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
										src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="HEIGHT: 16px" cellspacing="2" cellpadding="2"
							width="100%" border="0">
							<tr>
								<td align="right">&nbsp;
									<asp:button id="btnVer" runat="server" CssClass="boton" Text="Ver reporte"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
