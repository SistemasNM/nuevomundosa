<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30014.aspx.vb" Inherits="intranet_rrhh.PLA30014"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30014</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="JavaScript" src="../../intranet/JS/jsDesTabla.js" ></script>
		<script type="text/javascript" language="javascript">
			//enlace directo
			g_RutaDirecto=1;
			
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
	       }//end function
		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 971px; POSITION: absolute; TOP: 8px; HEIGHT: 80px"
				cellspacing="2" cellpadding="2" width="971" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Listado 
						de Sueldos Brutos (Promedios)
					</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 720px; HEIGHT: 128px" cellspacing="1" cellpadding="1"
							width="720" border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 85px; HEIGHT: 24px">&nbsp;Puesto&nbsp;:</td>
								<td style="WIDTH: 443px; HEIGHT: 24px"><asp:textbox id="txtpuescod" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="10" ToolTip="Ejem. 2011.09" Width="82px" CssClass="input"></asp:textbox>&nbsp;<input class="boton" id="btnPuesto" style="WIDTH: 20px; HEIGHT: 20px" onclick="fdesPuestoRRHH(txtpuescod,txtpuesdes)"
										type="button" value="...">&nbsp;<asp:textbox id="txtpuesdes" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="10" ToolTip="Ejem. 2011.09" Width="279px" CssClass="input"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 120px; HEIGHT: 24px">Seleccionar reporte...</td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 85px; HEIGHT: 24px">&nbsp;Area&nbsp;:</td>
								<td style="WIDTH: 443px; HEIGHT: 24px"><asp:textbox id="txtareacod" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="10" ToolTip="Ejem. 2011.09" Width="82px" CssClass="input"></asp:textbox>&nbsp;
									<input class="boton" id="btnArea" style="WIDTH: 20px; HEIGHT: 20px" onclick="fdesAreaRRHH(txtareacod,txtareades)"
										type="button" value="...">&nbsp;<asp:textbox id="txtareades" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="10" ToolTip="Ejem. 2011.09" Width="279px" CssClass="input"></asp:textbox></td>
								<td style="WIDTH: 90px; HEIGHT: 11px" rowspan="4"><asp:radiobutton id="optresumen" runat="server" Width="139px" Text="Resumido x Trabajador" Checked="True"
										GroupName="rptreporte"></asp:radiobutton><asp:radiobutton id="optdetalle" runat="server" Width="142px" Text="Detallado x Conceptos" GroupName="rptreporte"></asp:radiobutton></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 85px; HEIGHT: 21px">&nbsp;Periodos&nbsp;:</td>
								<td style="WIDTH: 443px; HEIGHT: 21px"><asp:textbox id="txtPeriodoIni" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="10" ToolTip="Ejem. 2011.08" Width="82px" CssClass="input"></asp:textbox>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtPeriodoFin" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="10" ToolTip="Ejem. 2011.09" Width="72px" CssClass="input"></asp:textbox>&nbsp;<FONT style="BACKGROUND-COLOR: #ffff00"><EM>! 
											ejemplo. 2011.01 - 2011.05</EM></FONT></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 85px; HEIGHT: 6px">&nbsp;Trabajador :</td>
								<td style="WIDTH: 443px; HEIGHT: 6px"><asp:textbox id="txtTrabajador" runat="server" Height="20px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="5" Width="82px" CssClass="input"></asp:textbox></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 85px; HEIGHT: 11px"></td>
								<td style="WIDTH: 443px; HEIGHT: 11px"></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="WIDTH: 720px; HEIGHT: 32px" cellspacing="2"
							cellpadding="2" width="720" border="0">
							<tr>
								<td style="WIDTH: 496px" align="right">&nbsp;</td>
								<td align="right"><asp:button id="btnConsultar" runat="server" CssClass="boton" Text="Consultar"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
