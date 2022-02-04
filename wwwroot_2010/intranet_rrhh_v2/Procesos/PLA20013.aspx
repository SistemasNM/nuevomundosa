<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20013.aspx.vb" Inherits="intranet_rrhh_v2.PLA20013"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
		<!--<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>-->
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsDesTabla.js"></script>
		<script type="text/javascript" language="JavaScript">
<!-- Begin

		function fnc_mostrardetalle(pstrcodigo)
		{
			var retorno = window.showModalDialog("PLA20011.aspx?pstrCodigo="+pstrcodigo,"","dialogHeight:400px;dialogWidth:840px;center:yes;help:no;");
			//modalWin
			retorno.close();
			retorno=null;
			return false;
		}//end function
		
		
		function modalWin(purl) 
		{
			if (window.showModalDialog) {
				window.showModalDialog(purl,"name","dialogWidth:255px;dialogHeight:250px");
			} else {
				window.open(purl,"name","height=255,width=250,toolbar=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no ,modal=yes");
			}//end if
		}//end function
	
		function popUp(strUrl) 
		{
			var intWidth = screen.width;
			var intHeight = screen.height;
			window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		}//end funcion			
			
		function fnc_Eliminar(ControlID)
		{
			var lstrMensaje='Se eliminará el registro de -- '+document.all(ControlID+'_lblTrabDesI').innerHTML+' --.\n\n¿Esta seguro de continuar?';
			return confirm(lstrMensaje);
			//return false;
		}//end function
		
		function fnc_Update(ControlID)
		{
			var lstrMensaje='Se actualizará el reintegro de -- '+document.all(ControlID+'_lblTrabDesI').innerHTML+' --.\n\n¿Esta seguro de continuar?';
			return confirm(lstrMensaje);
			//return false;
		}//end function
				
//End-->
		</script>
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
	</head>
	<body>
		<form id="frmHESolicitud" name="frmHESolicitud" method="post" runat="Server">
			<input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server" /><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellspacing="2"
				cellpadding="2" width="1008" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 22px">&nbsp;REINTEGRO DE HORAS EXTRAS</td>
				</tr>
				<tr>
					<td class="panel">
						<table style="WIDTH: 1016px; HEIGHT: 18px" cellspacing="0" cellpadding="0">
							<tr>
								<td colspan="3">&nbsp;<asp:button id="btnConsultar" runat="server" CssClass="Boton" Text="Consultar"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:label id="lblMensaje" runat="server" CssClass="input" ForeColor="Red"></asp:label></td>
							</tr>
						</table>
						<table>
							<tr>
								<td><asp:datagrid id="DataGrid1" runat="server" Width="1008px" AutoGenerateColumns="False">
										<FooterStyle CssClass="GridFooter"></FooterStyle>
										<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem"></ItemStyle>
										<HeaderStyle CssClass="GridHeader"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn>
												<HeaderStyle Width="10px"></HeaderStyle>
												<ItemTemplate>
													<NOBR>
														<asp:Label  id="lblCodigoI" runat="server" Width="0px" Height="0px" Visible="False" text='<%# Container.DataItem("int_codigo") %>'>
														</asp:Label><input id="hdnCodigoI" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigoI"
															runat="server" />
														<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif"></asp:ImageButton>&nbsp;
													</NOBR>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:label id="lblCodigoE" runat="server" Width="0px" Height="0px" Visible="False" text='<%# Container.DataItem("int_codigo") %>'>
													</asp:label><input id="hdnCodigoE" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigoE"
														runat="server" />
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Tarea">
												<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:label id="lblTareaI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("chr_tarea") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="CTC">
												<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblCtcI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_codctc") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Fecha">
												<HeaderStyle HorizontalAlign="Center" Width="105px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblFechaI" runat="server" CssClass="Input" Width="80px" text='<%# Container.DataItem("chr_fecha_sol") %>'>
													</asp:label>
												</ItemTemplate>
												<FooterTemplate>
													&nbsp;
												</FooterTemplate>
												<EditItemTemplate>
													&nbsp;
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="C&#243;digo">
												<HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
												<HeaderTemplate>
													<table id="Table6" style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="65" border="0">
														<tr>
															<td class="GridHeader">
																<center>Código</center>
															</td>
														</tr>
													</table>
												</HeaderTemplate>
												<ItemTemplate>
													<table id="Table7" style="WIDTH: 50px; HEIGHT: 8px" cellspacing="1" cellpadding="1" width="105"
														border="0">
														<tr>
															<td>
																<asp:Panel id="Panel25" runat="server" Width="80px">
																	<asp:label id="lblTrabajadorI" runat="server" CssClass="input" Width="50px" text='<%# Container.DataItem("vch_codtrabajador") %>'>
																	</asp:label>
																</asp:Panel></td>
														</tr>
													</table>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Trabajador">
												<HeaderStyle HorizontalAlign="Center" Width="360px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblTrabDesI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_destrabajador") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Horas Solici.">
												<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblHESolI" runat="server" CssClass="InputDerecha" Width="100%" text='<%# Container.DataItem("tin_horext_sol") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Motivo">
												<HeaderStyle HorizontalAlign="Center" Width="400px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblObservacionI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_observacion") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Situ.">
												<HeaderStyle Width="30px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblSituI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("chr_estado") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="60px"></HeaderStyle>
												<ItemTemplate>
													<asp:ImageButton id="btnUpdate" runat="server" CommandName="Update" ImageUrl="../../intranet/Imagenes/Grabar.gif"></asp:ImageButton>&nbsp; 
													&nbsp;
													<asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" ImageUrl="../../intranet/Imagenes/borrador.gif"></asp:ImageButton>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
						<input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server" />
						<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server" />&nbsp;
						<input id="HDN4" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1"	name="hdnAprobacion" runat="server" />
                        <input id="HDN3" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN3" runat="server" />
                        <input id="HDN5" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN5" runat="server" /></td>
				</tr>
			</table>
		</form>
	</body>
</html>
