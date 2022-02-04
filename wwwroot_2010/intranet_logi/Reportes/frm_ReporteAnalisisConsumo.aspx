<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ReporteAnalisisConsumo.aspx.vb" Inherits="intranet_logi.frm_ReporteAnalisisConsumo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Nuevo Mundo - Reporte de Consumo</title>
		<meta name="vs_snapToGrid" content="False"/>
		<meta name="vs_showGrid" content="True"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
		<link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/EstilosWeb.css"/>
		<script language="javascript" type="text/javascript">
		// Funcion Busca Articulos
		function fnc_BuscarArticulos()
		{
			var x = "001";
			if (x!=""){
			var lstrAlmacen = x.substring(0,3);
				var lpstrTipo = 9;
				var lstrAlmacen = x.substring(0,3);
				var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen=" + lstrAlmacen + "&pstrTipo=" + lpstrTipo, "", "dialogHeight:500px;dialogWidth:800px;center:yes;help:no;");
				//var retorno = window.showModalDialog("../../../Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen="+lstrAlmacen,"","dialogHeight:500px;dialogWidth:800px;center:yes;help:no;");
				if (retorno!="" && retorno!=":"){
					var datos = retorno.split(":");
					var Codigo = datos[0];
					var Nombre = datos[1];
					var Unidad = datos[2];
					var Stock = datos[3];
					document.all('txtCodArticulo').value = Codigo;
					document.all('lblDesArticulo').value = "Desc: " + Nombre;
					document.all('lblUniMedida').value = "U.M.: " + Unidad;
					document.all('lblStockArticulo').value = "Stock: " + parseFloat(Stock).toFixed(3);
				}
			}
			else{
				alert("Debe elegir un almacen para oonsultar Articulos");
			}
			
		}
		function popUp1(strUrl) 
		{
			var intWidth = screen.width;
			var intHeight = screen.height;
			window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		}
		function fnc_BuscarProveedor()
		{
		    var retorno = window.showModalDialog("../../intranet/Buscadores/frm_QryProveedores.aspx", "dialogHeight:500px;dialogWidth:800px;center:yes;help:no;")
			var datos = retorno.split(":");
			var Codigo = datos[0];
			var Nombre = datos[1];
			document.all('txtCodProveedor').value = Codigo;
			document.all('txtDesProveedor').value = Nombre;
		}
		</script>
	</head>
	<body>
		<form id="frm_ReporteAnalisisConsumo" method="post" runat="server">
			<!-- Titulo -->
			<table style="WIDTH: 100%; HEIGHT: 10px" border="0" cellspacing="1" cellpadding="1">
				<tr>
					<td style="WIDTH: 100%; HEIGHT: 20px" class="Cabecera" valign="middle" align="left">Reporte 
						de Consumo
					</td>
				</tr>
			</table>
			<table style="WIDTH: 100%; HEIGHT: 10px" border="0" cellspacing="1" cellpadding="1">
				<tr>
					<td style="WIDTH: 100%; HEIGHT: 20px" valign="middle" align="left"><asp:radiobutton id="rdbArticulo" AutoPostBack="True" Runat="server" Text="Articulo"></asp:radiobutton><asp:radiobutton id="rdbFile" AutoPostBack="True" Runat="server" Text="Archivo"></asp:radiobutton><asp:radiobutton style="Z-INDEX: 0" id="rbdProveedor" AutoPostBack="True" Runat="server" Text="Proveedor"></asp:radiobutton></td>
				</tr>
				<tr>
					<td style="WIDTH: 100%; HEIGHT: 20px" valign="middle" align="left"><asp:checkbox id="chkMensual" runat="server" AutoPostBack="True" Text="Mensual"></asp:checkbox><asp:checkbox style="Z-INDEX: 0" id="chkSemanal" runat="server" AutoPostBack="True" Text="Semanal"></asp:checkbox></td>
				</tr>
			</table>
			<asp:panel id="pnlArticulo" Runat="server" Height="2px">
				<table style="WIDTH: 100%; HEIGHT: 10px" border="0" cellspacing="1" cellpadding="1">
					<tr>
						<td style="WIDTH: 55px" valign="middle" align="left">Producto:</td>
						<td style="WIDTH: 124px" valign="middle" align="left">
							<asp:textbox style="Z-INDEX: 0" id="txtCodArticulo" Runat="server" Height="20px" Font-Size="10px"
								MaxLength="20" Width="100px"></asp:textbox><input style="Z-INDEX: 0; WIDTH: 20px; HEIGHT: 20px" id="btnArticulos" class="boton" onclick="javascript:fnc_BuscarArticulos();"
								value="..." type="button" name="btnArticulos"></td>
						<td valign="middle" align="left">
							<asp:textbox style="Z-INDEX: 0" id="lblDesArticulo" BorderStyle="None" runat="server" Height="15px"
								Width="300px" CssClass="etiqueta"></asp:textbox>
							<asp:textbox style="Z-INDEX: 0" id="lblUniMedida" BorderStyle="None" Runat="server" Height="15px"
								Font-Size="8px" CssClass="etiqueta" width="55px"></asp:textbox>
							<asp:textbox style="Z-INDEX: 0" id="lblStockArticulo" BorderStyle="None" Runat="server" Height="15px"
								Font-Size="8px" CssClass="etiqueta" width="71px"></asp:textbox>
							<asp:textbox style="Z-INDEX: 0" id="lblPrecioArticulo" BorderStyle="None" Runat="server" Height="18px"
								Font-Size="8px" CssClass="etiqueta" width="2px"></asp:textbox>
							<asp:button style="Z-INDEX: 0" id="btnVerReporte" runat="server" Text="Ver Reporte" Height="20px"
								Width="80px" CssClass="boton" Font-Bold="false"></asp:button></td>
					</tr>
				</table>
			</asp:panel>
            <asp:panel id="pnlProveedores" Runat="server">
				<table>
					<tr>
						<td>
							<asp:Label id="lblProveedor" runat="server">Proveedor:</asp:Label></td>
						<td>
							<asp:textbox style="Z-INDEX: 0" id="txtCodProveedor" Runat="server" Height="20px" Font-Size="10px"
								MaxLength="20" Width="100px"></asp:textbox></td>
						<td><input style="Z-INDEX: 0; WIDTH: 20px; HEIGHT: 20px" id="btnProveedor" class="boton" onclick="javascript:fnc_BuscarProveedor();"
								value="..." type="button" name="btnProveedor">
						</td>
						<td>
							<asp:textbox id="txtDesProveedor" BorderStyle="None" runat="server" Height="18px" Width="424px"
								CssClass="etiqueta"></asp:textbox></td>
						<td>
							<asp:button style="Z-INDEX: 0" id="btnVerProveedores" runat="server" Text="Ver Reporte" Height="20px"
								Width="80px" CssClass="boton" Font-Bold="false"></asp:button></td>
						</td></tr>
				</table>
			</asp:panel>
			<table>
				<tr>
					<td><asp:panel id="pnlFile" Runat="server" Width="704px">
							<table id="Table1" border="0" cellspacing="0" cellpadding="0" width="432" height="36">
								<tr>
									<td class="Titulo" height="13" width="355">&nbsp; Seleccione el archivo</td>
									<td class="Titulo" height="13"></td>
								</tr>
								<tr>
									<td width="355"><input style="WIDTH: 614px; HEIGHT: 22px" id="File1" class="Boton" size="83" type="file"
											name="File1" runat="server"></td>
									<td>
										<asp:Button id="btnSubir" runat="server" Text="Ver Reporte" CssClass="Boton"></asp:Button></td>
								</tr>
							</table>
						</asp:panel></td>
				</tr>
			</table>
			<table>
				<tr>
					<td></td>
				</tr>
			</table>
			<asp:datagrid style="Z-INDEX: 101; POSITION: absolute; TOP: 225px; LEFT: 13px" id="dgDetalleConsumo"
				runat="server" AutoGenerateColumns="true">
				<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
				<ItemStyle CssClass="GridItem"></ItemStyle>
				<HeaderStyle CssClass="GridHeader"></HeaderStyle>
			</asp:datagrid>
        </form>
	</body>
</html>
