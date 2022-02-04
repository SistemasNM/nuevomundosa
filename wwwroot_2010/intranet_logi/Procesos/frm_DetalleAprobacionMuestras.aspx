<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_DetalleAprobacionMuestras.aspx.vb" Inherits="intranet_logi.frm_DetalleAprobacionMuestras"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>frm_DetalleAprobacionMuestras.aspx</title>
		<base target="_self"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
		function VerAdjunto (Orden , Articulo)
		{
			var retorno = window.showModalDialog("LOG20010.aspx?Orden="+Orden+"&Articulo="+Articulo,"","dialogHeight:300px;dialogWidth:550px;center:yes;help:no;");
		}//end function
		
		function Historial (OCOS)
		{
			var retorno = window.showModalDialog("LOG20007.aspx?&strRequisicion="+OCOS,"","dialogHeight:300px;dialogWidth:500px;center:yes;help:no;");
		}
		returnValue = "";
		function Aprobar(Valor)
		{
			returnValue = Valor;
			window.close();
		}//end function
			
		function ConfirmarEli()
		{
			if (confirm("Está seguro de aprobar la requisicion."))
			{
				return true;	
			}else{
				return false;
			}
		}//end function
			
		function popUp(strUrl) 
		{
			var intWidth = screen.width;
			var intHeight = screen.height;
			//window.open(strUrl);
			window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		}//end function
			
		function fnc_estadisticosxarticulo(pstrarticulo)
		{
			document.all('hdnarticuloestadistica').value=pstrarticulo;
			document.all('hdnposicionpanel').value=event.clientY+10;
			document.all['pnldatosestadisticos'].style.display='inline';
			//document.all['pnldatosestadisticos'].style.top=event.clientY+20;
			return true;
		}//end function
	
	
		function fnc_ocultarestadisticos()
		{
			document.all('hdnarticuloestadistica').value='';
			document.all['pnldatosestadisticos'].style.display='none';
		}//end function
		
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellspacing="2"
				cellpadding="2" width="724" align="center" border="0">
				<tr>
					<td class="Cabecera">APROBACION DE SOLICITUD DE MUESTRA</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table4" cellspacing="0" cellpadding="0" width="600" border="0">
							<tr>
								<td style="WIDTH: 300px">
									<table id="Table3" style="WIDTH: 300px" border="0">
										<tr>
											<td class="Etiqueta" style="WIDTH: 91px">Número</td>
											<td><asp:textbox id="txtDocumento" runat="server" CssClass="input" Width="214px"></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta" style="WIDTH: 91px">Fecha Solicitud</td>
											<td><asp:textbox id="TxtFecha" runat="server" CssClass="input" Width="214px"></asp:textbox></td>
										</tr>
									</table>
									<table id="tblCentroCosto" style="WIDTH: 300px" border="0" runat="server">
										<tr>
											<td class="Etiqueta" style="WIDTH: 90px">Cliente</td>
											<td><asp:textbox id="txtCliente" runat="server" CssClass="input" Width="214px"></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta" style="WIDTH: 90px">Vendedor</td>
											<td><asp:textbox id="txtVendedor" runat="server" CssClass="input" Width="214px"></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta" style="WIDTH: 90px">Almacén</td>
											<td><asp:textbox id="txtAlmacen" runat="server" CssClass="input" Width="214px"></asp:textbox></td>
										</tr>
									</table>
									<table id="Table6" style="WIDTH: 300px" border="0">
										<tr>
											<td class="Etiqueta" style="WIDTH: 79px" vAlign="top">Observaciones</td>
											<td><asp:textbox id="txtObservaciones" runat="server" CssClass="input" Width="223px"
													TextMode="MultiLine" Height="40px"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td style="WIDTH: 300px">&nbsp;&nbsp;</td>
								<td style="WIDTH: 300px" vAlign="top"><asp:label id="lblCreacion" runat="server" Width="100%">Label</asp:label><asp:label id="lblTitulo" runat="server" CssClass="Etiqueta" Width="368px" Font-Size="Small">Seguimiento</asp:label>
									<div style="WIDTH: 432px; HEIGHT: 109px">
                                        <asp:datagrid id="dtgSeguimiento" runat="server" Width="416px" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
											<ItemStyle CssClass="griditem"></ItemStyle>
											<HeaderStyle CssClass="gridheader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="var_Estado" HeaderText="Estado"></asp:BoundColumn>
												<asp:BoundColumn DataField="var_Usuario" HeaderText="Usuario"></asp:BoundColumn>
												<asp:BoundColumn DataField="var_Fecha" HeaderText="Fecha"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
                                    </div>
								</td>
							</tr>
						</table>
						<DIV style="OVERFLOW: auto; WIDTH: 734px; HEIGHT: 144px">
                            <asp:datagrid id="dtgDetalle" runat="server" Width="710px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
								<ItemStyle CssClass="griditem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3px"></HeaderStyle>
										<ItemTemplate>
											<asp:ImageButton id="ibtiestadisticas" runat="server" CommandName="cmd_estadisticas" ImageUrl="../../intranet/Imagenes/buscar16x16.gif"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Int_Secuencia" HeaderText="Item"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_CodigoArticulo"></asp:BoundColumn>
									<asp:BoundColumn DataField="de_item"></asp:BoundColumn>
									<asp:BoundColumn DataField="co_unme" HeaderText="U.M.">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ca_actu" HeaderText="Stock" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="num_CantidadSolicitud" HeaderText="Cantidad" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="num_Precio" HeaderText="Precio" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:ImageButton id="ibtAdjuntos" runat="server" CommandName="ADJUNTOS" ImageUrl="../../intranet/Imagenes/PaginasI.bmp"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
                        </DIV>
						<table id="Table1" border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 586px" vAlign="top" align="right">Cantidad Total</td>
								<td><asp:textbox id="txtTotal" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table9" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td align="right"><asp:button id="btnAnular" runat="server" CssClass="boton" Text="Anular" Visible="False"></asp:button><asp:button id="btnAprobar" runat="server" CssClass="boton" Text="Aprobar"></asp:button><asp:button id="btnDesaprobar" runat="server" CssClass="boton" Text="Desaprobar"></asp:button><asp:button id="btnRechazar" runat="server" CssClass="boton" Text="Rechazar" Visible="False"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
