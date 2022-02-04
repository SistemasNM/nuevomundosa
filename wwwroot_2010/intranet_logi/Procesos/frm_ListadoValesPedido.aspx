<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ListadoValesPedido.aspx.vb" Inherits="intranet_logi.frm_ListadoValesPedido"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>VALES DE SALIDA</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
		<link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/EstilosWeb.css"/>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
	</head>
	<body>
		<form id="frm_ListadoValesPedido" method="post" runat="server">
			<table id="tblCabeceraPagina" border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td style="HEIGHT: 28px" class="Cabecera">Listado de Vales de Salida</td>
				</tr>
			</table>
			<table>
				<tr>
					<td style="HEIGHT: 13px">
						<asp:Label id="lblMsgError" runat="server" ForeColor="Red"></asp:Label>
					</td>
				</tr>
				<tr>
					<td>
						<asp:datagrid id="dgListaVales" runat="server" Width="700px" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle Font-Size="12px" Font-Bold="True" Height="20px" CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderStyle Width="3%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" CommandName="Editar"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="3%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="ibtImprimir" runat="server" ImageUrl="../../intranet/Imagenes/im_printer.gif" CommandName="Imprimir"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Vale de Salida">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
									<ItemTemplate>
                                    	<asp:Label id="lblVale" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.vale") %>'></asp:Label>
                                        <asp:Label id="lblNumDocu" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_DOCU") %>' Visible="false"></asp:Label>
                                        <asp:Label id="lblTipoDocu" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TI_DOCU") %>' Visible="false"></asp:Label>
                                        <asp:Label id="lblAlmacen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ALMA") %>' Visible="false"></asp:Label>
                                        <asp:Label id="lblEmpresa" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_EMPR") %>' Visible="false"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="fe_docu" HeaderText="Fecha Atencion">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="80px"></HeaderStyle>
									<ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Atendido" HeaderText="Atendido Por">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="220px"></HeaderStyle>
									<ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Almacen" HeaderText="Almacen">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="250px"></HeaderStyle>
									<ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Imp">
									<HeaderStyle Width="3%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
                                        <asp:CheckBox ID="chkImpreso" runat="server" Checked ='<%# DataBinder.Eval(Container, "DataItem.IMPRESO") %>' Enabled="false"/>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td>
						<asp:Panel id="pnlDetalle" runat="server">
							<table style="WIDTH: 700px; HEIGHT: 8px">
								<tr>
									<td>
										<asp:Label id="lblDetalle" Runat="server">Detalle de Vale de Salida</asp:Label></td>
								</tr>
								<tr>
									<td>
										<asp:datagrid style="Z-INDEX: 0" id="dgDetalleVale" runat="server" AutoGenerateColumns="False" Width="700px">
											<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
											<ItemStyle CssClass="GridItem"></ItemStyle>
											<HeaderStyle Font-Size="12px" Font-Bold="True" Height="20px" CssClass="GridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="co_item" HeaderText="Codigo">
													<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="150px"></HeaderStyle>
													<ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>

												<asp:BoundColumn DataField="de_item" HeaderText="Descripcion">
													<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="400px"></HeaderStyle>
													<ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
                        	
                        <asp:BoundColumn DataField="ca_docu_movi" DataFormatString="{0:#,##0.00}" HeaderText="Cant.">
                         <ItemStyle Font-Size="9px" HorizontalAlign="Right" width="50px"></ItemStyle>
													<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="50px"></HeaderStyle>
												</asp:BoundColumn>
												
                        <asp:BoundColumn DataField="co_unme_movi" HeaderText="U.M">
                         <ItemStyle Font-Size="9px" HorizontalAlign="left" width="50px"></ItemStyle>
												 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="50px"></HeaderStyle>
												</asp:BoundColumn>
												
                        <asp:BoundColumn DataField="ca_docu_alte" DataFormatString="{0:#,##0.00}" HeaderText="Cant. Alter.">
                         <ItemStyle Font-Size="9px" HorizontalAlign="Right" width="50px"></ItemStyle>
												 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="50px"></HeaderStyle>
												</asp:BoundColumn>

												<asp:BoundColumn DataField="co_unme_alte" HeaderText="U.M Alter.">
													<ItemStyle Font-Size="9px" HorizontalAlign="left" width="50px"></ItemStyle>
                          <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="50px"></HeaderStyle>
												</asp:BoundColumn>

											</Columns>
										</asp:datagrid></td>
								</tr>
							</table>
						</asp:Panel>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
