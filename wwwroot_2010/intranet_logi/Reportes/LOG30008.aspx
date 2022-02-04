<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG30008.aspx.vb" Inherits="intranet_logi.LOG30008"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG30004</title>
		<meta content="False" name="vs_showGrid"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
		
		function fMostrarReporte(strURL) 
		{
			var intWidth  = screen.width;
			var intHeight = screen.height;

	        window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
	    }//end function
	    
	    function fnc_BuscarArticulos()
		{
			var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulos.aspx","","dialogHeight:370px;dialogWidth:440px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var Codigo = datos[0];
				var Nombre = datos[1];
				document.all('txtcodarticulo').value = Codigo;
				document.all('txtdesarticulo').value = Nombre;
			}//end if	
		}//end function
	       
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 88px"
				cellspacing="0" cellpadding="0" width="98%" border="0">
				<tr>
					<td class="Cabecera" style="HEIGHT: 19px">Análisis de Consumo
					</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table2" style="WIDTH: 632px; HEIGHT: 108px" cellspacing="1" cellpadding="1"
							width="632" border="0">
							<tr>
								<td style="WIDTH: 68px" rowSpan="3"><asp:radiobutton id="rbtfiltro1" runat="server" AutoPostBack="True" GroupName="Filtro" Checked="True"
										Text="Filtro 1"></asp:radiobutton></td>
								<td class="etiqueta" style="FONT-WEIGHT: bold; WIDTH: 108px">&nbsp;Tipo de Artículo 
									:</td>
								<td><asp:dropdownlist id="ddlTipoArt" runat="server" AutoPostBack="True" Height="16px" Width="232px" Font-Names="Verdana"
										Font-Size="XX-Small"></asp:dropdownlist></td>
								<td rowSpan="3"><asp:checkbox id="chkconstocksinmov" runat="server" Text="Todos con stock mayor a cero" Width="171px"></asp:checkbox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 108px; HEIGHT: 1px">
								&nbsp;Rubro :
								<td style="HEIGHT: 1px"><asp:dropdownlist id="ddlRubro" runat="server" AutoPostBack="True" Height="24px" Width="232px" Font-Names="Verdana"
										Font-Size="XX-Small"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 108px">
								&nbsp;Familia :
								<td><asp:dropdownlist id="ddlFamilia" runat="server" Height="24px" Width="232px" Font-Names="Verdana"
										Font-Size="XX-Small"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colspan="4"></td>
							</tr>
							<tr>
								<td style="WIDTH: 68px"><asp:radiobutton id="rbtfiltro2" runat="server" AutoPostBack="True" GroupName="Filtro" Text="Filtro 2"></asp:radiobutton></td>
								<td class="etiqueta" style="WIDTH: 108px">&nbsp;Artículo :</td>
								<td colspan="2"><asp:textbox id="txtcodarticulo" runat="server" Width="120px" Enabled="False" 
										MaxLength="20" CssClass="input"></asp:textbox><asp:textbox id="txtdesarticulo" runat="server" Width="280px" Enabled="False" 
										MaxLength="20" CssClass="input"></asp:textbox>&nbsp;<input class="boton" id="btnbuscararticulo" style="WIDTH: 20px; HEIGHT: 20px" disabled
										onclick="fnc_BuscarArticulos();" type="button" value="..." name="btnbuscararticulo" runat="server"></td>
							</tr>
							<tr>
								<td colspan="4"></td>
							</tr>
							<tr>
								<td style="WIDTH: 68px" rowSpan="3"><asp:radiobutton id="rbtfiltro3" runat="server" AutoPostBack="True" GroupName="Filtro" Text="Filtro 3"></asp:radiobutton></td>
								<td class="etiqueta" style="WIDTH: 108px">&nbsp;Tipo listado :</td>
								<td>
									<P>&nbsp;Listado de artículos agrupados por&nbsp;:&nbsp;<br>
										&nbsp;1-Tipo Artículo,&nbsp;2-Rubro,&nbsp;3-Familia,&nbsp;4-SubFamilia</P>
								</td>
								<td rowSpan="2"><asp:checkbox id="chkconstocksinmov_3" runat="server" Text="Todos con stock mayor a cero" Width="171px"
										Enabled="False"></asp:checkbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="WIDTH: 632px; HEIGHT: 48px" cellspacing="2"
							cellpadding="2" width="632" align="left" border="0">
							<tr>
								<td align="left"><asp:button id="btnVerReporte" runat="server" Text="Ver reporte" Height="24px" Width="96px"
										CssClass="boton" runat="server"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
