<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmTiposAprobacion_2.aspx.vb" Inherits="intranet_logi.frmTiposAprobacion_2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frmTiposAprobacion</title>
		<base target="_self">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<LINK href="../Estilos/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
		    returnValue = "";
		    function btnEscoger_Onclick(Codigo, Nombre) {
		        returnValue = Codigo + ":" + Nombre;
		        window.close();
		    }

		    function Cerrar() {
		        window.close();
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="2"
				cellPadding="1" width="440" border="0">
				<TR>
					<TD class="Cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Busqueda de Tipos de Aprobación</TD>
				</TR>
				<TR>
					<TD class="panel">
						<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="etiqueta" style="WIDTH: 113px" width="113">Código</TD>
								<TD width="300">
									<asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="etiqueta" style="WIDTH: 113px" width="113">Nombre</TD>
								<TD width="300">
									<asp:TextBox id="txtNombre" runat="server" Width="296px" CssClass="input"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="Footer" colSpan="2">
									<asp:Button id="btnBuscar" runat="server" CssClass="boton" Text="Buscar"></asp:Button>&nbsp;<INPUT class="boton" id="btnCerrar" onclick="Cerrar()" type="button" value="Cerrar" name="btnCerrar">&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="panel" vAlign="top" height="260">
						<DIV id="divDatos" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px">
							<asp:DataGrid id="dgDatos" runat="server" Width="350px" AutoGenerateColumns="False">
								<FooterStyle CssClass="GridFooter"></FooterStyle>
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="GridHeader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<input class="input" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="...">
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CO_APRO" HeaderText="C&#243;digo"></asp:BoundColumn>
									<asp:BoundColumn DataField="DE_APRO" HeaderText="Descripci&#243;n"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>