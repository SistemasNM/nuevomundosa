<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30013.aspx.vb" Inherits="intranet_rrhh.PLA30013"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30013</title>
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
			}//end function
			
			function fMostrarReporte(strURL) 
			{
				var intWidth  = screen.width;
				var intHeight = screen.height;
  
                window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
            } //end function

		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 971px; POSITION: absolute; TOP: 8px; HEIGHT: 80px"
				cellspacing="2" cellpadding="2" width="971" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Listado 
						de Adelantos y Prestamos
					</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 632px; HEIGHT: 96px" cellspacing="1" cellpadding="1" width="632"
							border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 118px; HEIGHT: 11px">&nbsp;Periodo de 
									Inicio&nbsp;:</td>
								<td style="WIDTH: 308px; HEIGHT: 11px"><asp:textbox id="txtPeriodoIni" runat="server" Height="20px" Width="88px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="10" ToolTip="Formato: YYYY.MM  ejemplo 2011.02"></asp:textbox>&nbsp;<FONT style="BACKGROUND-COLOR: #ffff00"><EM>! 
											ejemplo.&nbsp;2011.01</EM></FONT></td>
								<td class="Etiqueta" style="WIDTH: 120px; HEIGHT: 11px">Seleccionar reporte...</td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 118px; HEIGHT: 23px">&nbsp;Periodo Final :</td>
								<td style="WIDTH: 308px; HEIGHT: 23px"><asp:textbox id="txtPeriodoFin" runat="server" Height="20px" Width="88px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="10" ToolTip="Formato: YYYY.MM  ejemplo 2011.02"></asp:textbox>&nbsp;<FONT style="BACKGROUND-COLOR: #ffff00"><EM>! 
											ejemplo.&nbsp;2011.05</EM></FONT></td>
								<td style="WIDTH: 90px; HEIGHT: 11px" rowspan="2"><asp:radiobutton id="optAdelantos" runat="server" GroupName="rptadepre" Checked="True" Text="Adelantos"></asp:radiobutton><asp:radiobutton id="optPrestamos" runat="server" GroupName="rptadepre" Text="Prestamos"></asp:radiobutton></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 118px; HEIGHT: 11px">&nbsp;Trabajador :</td>
								<td style="WIDTH: 308px; HEIGHT: 11px"><asp:textbox id="txtTrabajador" runat="server" Height="20px" Width="88px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="5"></asp:textbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="WIDTH: 624px; HEIGHT: 32px" cellspacing="2"
							cellpadding="2" width="624" border="0">
							<tr>
								<td style="WIDTH: 839px" align="right"><asp:button id="btnConsultar" runat="server" Text="Consultar" CssClass="boton"></asp:button>&nbsp;&nbsp;</td>
								<td align="right"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
