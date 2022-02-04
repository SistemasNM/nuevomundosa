<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ConversiondeValesAPedidos.aspx.vb" Inherits="intranet_logi.ConversiondeValesAPedidos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>frm_ConversiondeValesAPedidos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
		    function btnSeleccion_Onclick(strVale)
		    {
			    window.location.href = "frm_ConversiondeValesAPedidos_Detalle.aspx?NroVale="+strVale;
			    return false;
		    }					
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellspacing="1" cellpadding="1" width="98%" border="0">
				<tr>
					<td class="CABECERA">Conversion de Vales de Mantto. a Pedidos</td>
				</tr>
				<tr>
					<td><asp:datagrid id="dgDatos" runat="server" AutoGenerateColumns="False" Width="600px">
							<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="Cod_Almacen" HeaderText="Almacen">
									<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Id_Vale" HeaderText="Vale">
									<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Id_OT" HeaderText="Nro_Documento">
									<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Fe_Pedi" HeaderText="Fecha">
									<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Id_Personal" HeaderText="Codigo">
									<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Concepto" HeaderText="Concepto">
									<HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<input class="input" id="btnSeleccion" runat="server" type="button" value="..." name="btnSeleccion" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="FOOTER"></td>
				</tr>
			</table>
		</form>
	</body>
</html>
