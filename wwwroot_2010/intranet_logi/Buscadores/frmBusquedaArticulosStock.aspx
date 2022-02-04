<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmBusquedaArticulosStock.aspx.vb" Inherits="intranet_logi.frmBusquedaArticulosStock" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
     <title>Listado de Articulos con Stock</title>
	  <base target="_self">
	  <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
      <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
      <meta content="JavaScript" name="vs_defaultClientScript"/>
      <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
      <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
      <script language="javascript" type="text/javascript">
          returnValue = "";

          function btnEscoger_Onclick(Codigo, Nombre, Unidad, stock) {
              returnValue = Codigo + ":" + Nombre + ":" + Unidad + ":" + stock;
              window.close();
          }

          function Cerrar() {
              window.close();
          }
		</script>
	</head>
	<body>
    		<form id="Form1" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" border="0" style="width:700px">
				<tr>
                	<td class="Cabecera" style="width:700px; height:30px">&nbsp;Busqueda de articulos con stock</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="0" cellpadding="0" width="700px" border="0">
							<tr>
								<td class="etiqueta" style="width:120px" width="52">&nbsp;Código:</td>
								<td style="width:580px"><asp:textbox id="txtCodigo" runat="server" CssClass="input"></asp:textbox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="width:120px">&nbsp;Nombre :</TD>
								<td style="width:580px"><asp:textbox id="txtNombre" runat="server" CssClass="input" width="256px"></asp:textbox></TD>
							</tr>
							<tr>
								<td colspan="2" align="right">
                                 &nbsp;<input id="hdnAlmacen" style="width: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAlmacen" runat="server"/>
                                 &nbsp;<input id="hdnTipoArticulo" style="width: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnTipoArticulo" runat="server"/>
                                 &nbsp;<asp:button id="btnBuscar" runat="server" CssClass="boton" Text="Buscar"></asp:button>
                                 &nbsp;<input class="boton" id="btnCerrar" onclick="Cerrar()" type="button" value="Cerrar" name="btnCerrar"/>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<div id="divDatos" style="width:700px">
							<asp:DataGrid id="dgDatos" runat="server" width="700px" AutoGenerateColumns="False">
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<Columns>
                                    <asp:TemplateColumn>
                                      <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemTemplate>
											<input runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." class="Boton" style="width:20px"/>
										</ItemTemplate>
									</asp:TemplateColumn>
									
                                    <asp:BoundColumn DataField="co_item" HeaderText="Codigo">
                                     <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
                                     <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                    </asp:BoundColumn>
									
                                    <asp:BoundColumn DataField="de_item" HeaderText="Articulo">
										 <HeaderStyle HorizontalAlign="Center" Width="330px" VerticalAlign="Middle"></HeaderStyle>
                                     <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>

									<asp:BoundColumn DataField="ca_actu" HeaderText="Stock" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>

                                    <asp:BoundColumn DataField="co_unme" HeaderText="U.M.">
                                    	<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
									</asp:BoundColumn>

									<asp:BoundColumn DataField="co_colo" HeaderText="Color">
                                    	<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundColumn>

									<asp:BoundColumn DataField="de_data" HeaderText="Colorante">
                                    	<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundColumn>

									<asp:BoundColumn DataField="co_deta_desc" HeaderText="Dise&#241;o">
                                    	<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundColumn>
								</Columns>
							</asp:DataGrid>
                        </div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
