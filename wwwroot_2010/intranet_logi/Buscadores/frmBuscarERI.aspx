<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmBuscarERI.aspx.vb" Inherits="intranet_logi.frmBuscarERI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Listado de Articulos</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="javascript">
		    returnValue = "";
		    function btnEscoger_Onclick(Codigo, Almacen) {
		        returnValue = Codigo + ":" + Almacen;
		        window.close();
		    }

		    function Cerrar() {
		        window.close();
		    }
		</script>
	</head>
	<body ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellspacing="2"
				cellpadding="1" width="440" border="0">
				<tr>
					<td class="Cabecera" style="BACKGROUND-IMAGE: url(../../intranet/Imagenes/Cabecera.bmp)">Busqueda 
						de Inventarios (ERI)</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0">
							<tr>
								<td class="etiqueta" style="WIDTH: 113px" width="113">Código</td>
								<td width="300">
									<asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 113px" width="113">Estado</td>
								<td width="300">
									<asp:RadioButton ID="rdbAbierto" runat="server" Text="Abierto" Checked="True" 
                                        GroupName="Estado" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdbCerrado" runat="server" Text="Cerrado" 
                                        GroupName="Estado" />
                                </td>
							</tr>
							<tr>
								<td class="Footer" colspan="2">
									<asp:Button id="btnBuscar" runat="server" CssClass="Boton" Text="Buscar"></asp:Button>&nbsp;<input class="Boton" id="btnCerrar" onclick="Cerrar()" type="button" value="Cerrar" name="btnCerrar"/>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="panel" valign="top" height="260">
						<div id="divDatos" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px">
							<asp:DataGrid id="dgDatos" runat="server" Width="408px" 
                                AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<input class="Boton" style="width:auto" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..."/>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="vch_CodigoInventario" HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign = "Center"></asp:BoundColumn>
									<asp:BoundColumn DataField="vch_CodigoAlmacen" HeaderText="Almacen" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign = "Center"></asp:BoundColumn>
									<asp:BoundColumn DataField="dtm_FechaInicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign = "Center"></asp:BoundColumn>
								    <asp:BoundColumn DataField="dtm_FechaFin" HeaderText="Fecha Fin" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign = "Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="chr_EstadoInventario" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign = "Center"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
