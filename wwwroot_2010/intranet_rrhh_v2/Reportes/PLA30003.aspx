<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30003.aspx.vb" Inherits="intranet_rrhh.PLA30003"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30003</title>
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
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 32px"
				cellspacing="2" cellpadding="2" width="100%" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Detalle 
						de Aportes SCTR</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 472px; HEIGHT: 35px" cellspacing="1" cellpadding="1" width="472"
							border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">Año</td>
								<td style="WIDTH: 145px; HEIGHT: 11px"><asp:textbox id="txtAnio" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="53px"
										Height="20px" MaxLength="4"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 31px; HEIGHT: 11px">Mes</td>
								<td style="HEIGHT: 11px"><asp:dropdownlist id="ddlMes" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="106px"
										Height="16px">
										<asp:ListItem Value="01">ENERO</asp:ListItem>
										<asp:ListItem Value="02">FEBRERO</asp:ListItem>
										<asp:ListItem Value="03">MARZO</asp:ListItem>
										<asp:ListItem Value="04">ABRIL</asp:ListItem>
										<asp:ListItem Value="05">MAYO</asp:ListItem>
										<asp:ListItem Value="06">JUNIO</asp:ListItem>
										<asp:ListItem Value="07">JULIO</asp:ListItem>
										<asp:ListItem Value="08">AGOSTO</asp:ListItem>
										<asp:ListItem Value="09">SEPTIEMBRE</asp:ListItem>
										<asp:ListItem Value="10">OCTUBRE</asp:ListItem>
										<asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
										<asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px">Planilla</td>
								<td style="WIDTH: 145px"><asp:dropdownlist id="ddlPlanilla" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="106px"
										Height="16px">
										<asp:ListItem Value="EMP">EMPLEADOS</asp:ListItem>
										<asp:ListItem Value="OBS">OBREROS SEM.</asp:ListItem>
										<asp:ListItem Value="OBM">OBREROS MES</asp:ListItem>
									</asp:dropdownlist></td>
								<td style="WIDTH: 31px"></td>
								<td></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="HEIGHT: 16px" cellspacing="2" cellpadding="2"
							width="100%" border="0">
							<tr>
								<td align="right"><asp:button id="btnVer" runat="server" Text="Ver reporte" CssClass="boton"></asp:button><asp:button id="btnexportar" runat="server" Text="Exportar" CssClass="boton"></asp:button>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			&nbsp;
		</form>
	</body>
</html>
