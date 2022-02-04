<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30006.aspx.vb" Inherits="intranet_rrhh.PLA30006"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30006</title>
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
				<tbody>
					<tr>
						<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Adelanto 
							de Vacaciones</td>
					</tr>
					<tr>
						<td class="panel" rowspan="2">
							<table id="Table2" style="WIDTH: 592px; HEIGHT: 16px" cellspacing="1" cellpadding="1" width="592"
								border="0">
								<tbody>
									<tr>
										<td class="Etiqueta" style="WIDTH: 74px; HEIGHT: 20px">Año</td>
										<td style="WIDTH: 103px; HEIGHT: 20px"><asp:textbox id="Txtanno" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="53px"
												Height="20px" MaxLength="4"></asp:textbox></td>
										<td class="Etiqueta" style="WIDTH: 65px; HEIGHT: 20px">Mes</td>
										<td style="WIDTH: 170px; HEIGHT: 20px">
											<asp:dropdownlist id="ddlMes" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="116px"
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
										<td class="Etiqueta" style="WIDTH: 54px; HEIGHT: 20px">Planilla</td>
										<td style="WIDTH: 149px; HEIGHT: 21px">
											<asp:dropdownlist id="ddlPlanilla" runat="server" Height="16px" Width="106px" Font-Names="Verdana"
												Font-Size="XX-Small">
												<asp:ListItem Value="EMP">EMPLEADOS</asp:ListItem>
												<asp:ListItem Value="OBM">OBREROS</asp:ListItem>
											</asp:dropdownlist></td>
									</tr>
								</tbody>
							</table>
							<table id="Table4" style="WIDTH: 592px; HEIGHT: 24px" cellspacing="1" cellpadding="1" border="0">
								<tr>
									<td class="Etiqueta" style="WIDTH: 68px" width="68">Tipo Reporte</td>
									<td class="input"><asp:radiobutton id="rbtRepBol" runat="server" CssClass="input" Checked="True" GroupName="Reporte"
											Text="Boletas"></asp:radiobutton><asp:radiobutton id="rbtRepRes" runat="server" Width="75px" CssClass="input" GroupName="Reporte"
											Text="Resumido"></asp:radiobutton>&nbsp;</td>
								</tr>
							</table>
							<table class="botonera" id="Table3" style="WIDTH: 595px; HEIGHT: 28px" cellspacing="1"
								cellpadding="1" width="595" border="0">
								<tr>
									<td align="right"><asp:button id="btnVer" runat="server" CssClass="boton" Text="Ver reporte"></asp:button></td>
								</tr>
							</table>
		
		</td></tr></tbody></table>
        </form>
	</body>
</html>
