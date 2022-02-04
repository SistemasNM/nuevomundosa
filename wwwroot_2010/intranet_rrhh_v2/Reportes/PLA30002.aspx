<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30002.aspx.vb" Inherits="intranet_rrhh.PLA30002"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30002</title>
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
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Resumen 
						por Área</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 472px; HEIGHT: 35px" cellspacing="1" cellpadding="1" width="472"
							border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">Año</td>
								<td style="WIDTH: 145px; HEIGHT: 11px"><asp:textbox id="txtAnio" runat="server" MaxLength="4" Height="20px" Width="53px" Font-Names="Verdana"
										Font-Size="XX-Small"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 31px; HEIGHT: 11px">Mes</td>
								<td style="HEIGHT: 11px"><asp:dropdownlist id="ddlMes" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="1">ENERO</asp:ListItem>
										<asp:ListItem Value="2">FEBRERO</asp:ListItem>
										<asp:ListItem Value="3">MARZO</asp:ListItem>
										<asp:ListItem Value="4">ABRIL</asp:ListItem>
										<asp:ListItem Value="5">MAYO</asp:ListItem>
										<asp:ListItem Value="6">JUNIO</asp:ListItem>
										<asp:ListItem Value="7">JULIO</asp:ListItem>
										<asp:ListItem Value="8">AGOSTO</asp:ListItem>
										<asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
										<asp:ListItem Value="10">OCTUBRE</asp:ListItem>
										<asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
										<asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px">Fecha</td>
								<td style="WIDTH: 145px"><asp:textbox id="txtfechamov" runat="server" Height="18px" 
                                        Width="120px" Font-Names="Verdana"
										Font-Size="XX-Small" CssClass="INPUT"></asp:textbox><img onclick="popUpCalendar(this, Form1.txtfechamov, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
										src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
								<td style="WIDTH: 31px"></td>
								<td></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="HEIGHT: 16px" cellspacing="2" cellpadding="2"
							width="100%" border="0">
							<tr>
								<td align="right"><asp:button id="btnVer" runat="server" CssClass="boton" Text="Ver reporte"></asp:button>
									<asp:button id="btnexportar" runat="server" CssClass="boton" Text="Exportar"></asp:button>&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
