<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30011.aspx.vb" Inherits="intranet_rrhh.PLA30011"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30011</title>
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
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 968px; POSITION: absolute; TOP: 8px; HEIGHT: 147px"
				cellspacing="2" cellpadding="2" width="968" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Emision 
						de Boletas de Pago
					</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 976px; HEIGHT: 63px" cellspacing="1" cellpadding="1" width="976"
							border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">&nbsp;Planilla :</td>
								<td style="WIDTH: 90px; HEIGHT: 11px"><asp:dropdownlist id="ddlPlanilla" runat="server" Height="16px" Width="88px" Font-Names="Verdana"
										Font-Size="XX-Small">
										<asp:ListItem Value="EMP">Empleados</asp:ListItem>
										<asp:ListItem Value="OBM">Obreros</asp:ListItem>
									</asp:dropdownlist></td>
								<td class="Etiqueta" style="WIDTH: 5px; HEIGHT: 11px">&nbsp;Año :</td>
								<td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtAnio" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="4"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 12px; HEIGHT: 11px">&nbsp;Mes :</td>
								<td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlMes" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="1">Enero</asp:ListItem>
										<asp:ListItem Value="2">Febrero</asp:ListItem>
										<asp:ListItem Value="3">Marzo</asp:ListItem>
										<asp:ListItem Value="4">Abril</asp:ListItem>
										<asp:ListItem Value="5">Mayo</asp:ListItem>
										<asp:ListItem Value="6">Junio</asp:ListItem>
										<asp:ListItem Value="7">Julio</asp:ListItem>
										<asp:ListItem Value="8">Agosto</asp:ListItem>
										<asp:ListItem Value="9">Septiembre</asp:ListItem>
										<asp:ListItem Value="10">Octubre</asp:ListItem>
										<asp:ListItem Value="11">Noviembre</asp:ListItem>
										<asp:ListItem Value="12">Diciembre</asp:ListItem>
									</asp:dropdownlist>&nbsp;&nbsp;&nbsp;</td>
								<td class="ETiqueta" style="HEIGHT: 11px">&nbsp;Correlativo&nbsp;&nbsp;&nbsp;&nbsp; 
									:</td>
								<td style="HEIGHT: 11px"><asp:textbox id="txtCorrelativo" runat="server" Height="20px" Width="53px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="1"></asp:textbox></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">Código 
									Inicio&nbsp;&nbsp;&nbsp;&nbsp; :</td>
								<td style="WIDTH: 90px; HEIGHT: 11px"><asp:textbox id="txtCodInicio" runat="server" Height="20px" Width="53px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="5"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 5px; HEIGHT: 11px">Código Final :</td>
								<td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtCodFin" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="5"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 12px; HEIGHT: 11px">Centro&nbsp; Costo :</td>
								<td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlCCosto" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="EMP" Selected="True">Empleados</asp:ListItem>
										<asp:ListItem Value="ONM">Pta.Principal</asp:ListItem>
										<asp:ListItem Value="OPN">Pta.Hilanderia</asp:ListItem>
									</asp:dropdownlist></td>
								<td class="Etiqueta" style="HEIGHT: 11px">Observaciones:</td>
								<td style="HEIGHT: 11px"><asp:textbox id="txtObservacion" runat="server" Height="20px" Width="427px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="60"></asp:textbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="WIDTH: 958px; HEIGHT: 32px" cellspacing="2"
							cellpadding="2" width="958" border="0">
							<tr>
								<td align="right"></td>
								<td style="WIDTH: 839px" align="right"><asp:button id="btnImprimir" runat="server" CssClass="boton" Text="Imprimir"></asp:button></td>
								<td align="right"><asp:button id="btnVer" runat="server" CssClass="boton" Text="Cancelar"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
