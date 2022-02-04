<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30008.aspx.vb" Inherits="intranet_rrhh.PLA30008"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30008</title>
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
						<td class="cabecera">Maestro de Trabajadores</td>
					</tr>
					<tr>
						<td class="panel" rowspan="2">
							<table id="Table2" style="WIDTH: 592px; HEIGHT: 16px" cellspacing="1" cellpadding="1" width="600"
								border="0">
								<tbody>
									<tr>
										<td class="Etiqueta" style="WIDTH: 50px; HEIGHT: 20px">Año</td>
										<td style="WIDTH: 100px; HEIGHT: 20px"><asp:textbox id="txtanio" runat="server" MaxLength="4" Height="20px" Width="100px" Font-Names="Verdana"
												Font-Size="XX-Small"></asp:textbox></td>
										<td class="Etiqueta" style="WIDTH: 50px; HEIGHT: 20px">Mes</td>
										<td style="WIDTH: 150px; HEIGHT: 20px"></td>
                                        <td style="WIDTH: 50px; HEIGHT: 20px">
                                        <asp:dropdownlist id="ddlMes" runat="server" Height="16px" Width="150px" Font-Names="Verdana" Font-Size="XX-Small">
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
										<td class="Etiqueta" style="WIDTH: 50px; HEIGHT: 20px">Estado</td>
										<td style="WIDTH: 150px; HEIGHT: 21px"><asp:dropdownlist id="ddlEstado" runat="server" Height="16px" Width="150px" Font-Names="Verdana" Font-Size="XX-Small">
												<asp:ListItem Value="ACT">ACTIVOS</asp:ListItem>
												<asp:ListItem Value="ANU">CESADOS</asp:ListItem>
											</asp:dropdownlist></td>
									</tr>
								</tbody>
							</table>
							<table class="botonera" id="Table3" style="WIDTH: 600px; HEIGHT: 28px" cellspacing="1"
								cellpadding="1" width="595" border="0">
								<tr>
                                    <td align="left" style="width:500px"><asp:Label ID = "lblMensaje" Text="" runat = "server"></asp:Label></td>
									<td align="right"  style="width:100px"><asp:button id="btnexportar" runat="server" Text="Exportar" CssClass="boton" Width="100px"></asp:button></td>
								</tr>
							</table>
		
		</td></tr></tbody></table>
        </form>
	</body>
</html>
