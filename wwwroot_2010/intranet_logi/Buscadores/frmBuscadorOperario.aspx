<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmBuscadorOperario.aspx.vb" Inherits="intranet_logi.frmBuscadorOperario" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MAI_0001</title>
		<base target="_self"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script language="javascript" type="text/javascript">
		    returnValue = "";
		    function btnEscoger_Onclick(Codigo, Nombre, CCC, CCN) {
		        returnValue = Codigo + ":" + Nombre + ":" + CCC + ":" + CCN;

		        window.close();
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="2"
				cellPadding="1" width="440" border="0">
				<TR>
					<TD class="Cabecera"><% =Titulo%></TD>
				</TR>
				<TR>
					<TD class="panel">
						<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="etiqueta" width="80">Código</TD>
								<TD width="300"><asp:textbox id="txtCodigo" runat="server" Font-Size="XX-Small" Font-Names="Verdana" CssClass="input"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="etiqueta" width="80">Nombre</TD>
								<TD width="300"><asp:textbox id="txtNombre" runat="server" Font-Size="XX-Small" Font-Names="Verdana" CssClass="input"
										Width="296px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="Footer" colSpan="2"><asp:button id="btnBuscar" runat="server" CssClass="boton" Text="Buscar"></asp:button>&nbsp;<INPUT class="boton" id="btnCerrar" type="button" value="Cerrar" name="btnCerrar">&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="panel" vAlign="top" height="260">
						<DIV id="divDatos" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px"><asp:datagrid id="dgDatos" runat="server" Width="350px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<INPUT class="boton" id="btnEscoger" style="WIDTH: 22px; HEIGHT: 22px" type="button" value="..."
												name="btnEscoger" runat="server">
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="chr_CodigoTrabajador" HeaderText="C&#243;digo"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_NombreTrabajador" HeaderText="Nombre y Apellido / Raz&#243;n Social"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
