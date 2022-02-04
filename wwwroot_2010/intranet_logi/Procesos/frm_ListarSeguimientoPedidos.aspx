<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ListarSeguimientoPedidos.aspx.vb" Inherits="intranet_logi.frm_ListarSeguimientoPedidos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>frm_ListarSeguimientoPedidos</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript" >
		    function fnc_Cerrar()
		    {
			    window.open('','_parent','');
			    window.close();
		    }
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellspacing="1" cellpadding="1" width="750" border="0" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px">
				<tr>
					<td class="cabecera" style="HEIGHT: 21px">&nbsp;PEDIDOS - SEGUIMIENTO DE 
						APROBACIONES</td>
				</tr>
				<tr>
					<td align="left">
						<table width="100%">
							<tr>
								<td style="WIDTH: 590px; HEIGHT: 15px" valign="top" colspan="3"><asp:datagrid id="dgSeguimiento" runat="server" Width="740px" AutoGenerateColumns="False">
										<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem"></ItemStyle>
										<HeaderStyle CssClass="GridHeader"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="nu_paso" HeaderText="Sec.">
												<HeaderStyle Width="50px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="st_apro" HeaderText="Estado">
												<HeaderStyle Width="50px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="no_usua" HeaderText="Usuario">
												<HeaderStyle Width="200px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="fe_apro" HeaderText="Fecha">
												<HeaderStyle Width="80px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ob_0001" HeaderText="Observaciones">
												<HeaderStyle Width="200px"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td align="left">
									<input id="hdnCodigo" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
										runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
