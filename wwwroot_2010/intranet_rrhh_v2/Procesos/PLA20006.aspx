<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20006.aspx.vb" Inherits="intranet_rrhh_v2.PLA20006"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA20006</title>
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
			<table id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 984px; POSITION: absolute; TOP: 8px; HEIGHT: 106px"
				cellspacing="2" cellpadding="2" width="984" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Adelanto 
						de Vacaciones</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table2" style="WIDTH: 954px; HEIGHT: 26px" cellspacing="1" cellpadding="1" width="954"
							border="0">
							<tbody>
								<tr>
									<td class="Etiqueta" style="WIDTH: 37px; HEIGHT: 11px">Año</td>
									<td style="WIDTH: 92px; HEIGHT: 11px"><asp:textbox id="txtano" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="53px"
											Height="20px" MaxLength="4"></asp:textbox>&nbsp;&nbsp;</td>
									<td class="Etiqueta" style="WIDTH: 35px; HEIGHT: 11px">Mes</td>
									<td style="WIDTH: 137px; HEIGHT: 11px">
										<asp:dropdownlist id="ddlMes" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="96px"
											Height="16px">
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
										</asp:dropdownlist>
                                    </td>
									<td class="Etiqueta" style="WIDTH: 41px">Planilla</td>
									<td style="WIDTH: 149px">
										<asp:dropdownlist id="ddlPlanilla" runat="server" Height="16px" Width="106px" Font-Names="Verdana"
											Font-Size="XX-Small">
											<asp:ListItem Value="EMP">EMPLEADOS</asp:ListItem>
											<asp:ListItem Value="OBM">OBREROS</asp:ListItem>
										</asp:dropdownlist>
                                    </td>
									<td class="Etiqueta" style="WIDTH: 68px; HEIGHT: 11px">Fecha Inicial</td>
									<td style="WIDTH: 141px; HEIGHT: 11px"><asp:textbox id="txtfechaini" runat="server" 
                                            Height="18px" Width="88px" Font-Names="Verdana"
											Font-Size="XX-Small" CssClass="INPUT"></asp:textbox><img onclick="popUpCalendar(this, Form1.txtfechaini, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
											src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
									<td class="Etiqueta" style="WIDTH: 62px; HEIGHT: 11px">Fecha Final</td>
									<td style="HEIGHT: 11px"><asp:textbox id="txtfechafin" runat="server" Height="18px" 
                                            Width="88px" Font-Names="Verdana"
											Font-Size="XX-Small" CssClass="INPUT"></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, Form1.txtfechafin, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
											src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
								</tr>
							</tbody>
						</table>
						<table class="botonera" id="Table3" style="HEIGHT: 16px" cellspacing="1" cellpadding="1"
							width="100%" border="0">
							<tr>
								<td align="right"><asp:button id="btnCalcular" runat="server" Text="Cálcular" CssClass="boton"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
