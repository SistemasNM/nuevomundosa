<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ListadoDespachoMuestras.aspx.vb" Inherits="intranet_logi.frm_ListadoDespachoMuestras"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Despacho de Muestras - Pendientes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
        <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
		    function btnSeleccion_Onclick(strCodigo)
		    {
			    window.location.href = "frm_RegistrarDespachoMuestras.aspx?Codigo="+strCodigo;
			    return false;
			
		    }		
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellspacing="1" cellpadding="1" width="98%" border="0">
				<tr>
					<td class="Cabecera" style="HEIGHT: 28px">DESPACHO DE MUESTRAS Y ETIQUETAS - PENDIENTES</td>
				</tr>
				<tr>
					<td>
						<!--GridAlternateItem-->
						<DIV style="WIDTH: 100%; HEIGHT: 300px;overflow: auto;">
                            <asp:datagrid id="dtgLista" runat="server" AutoGenerateColumns="False" Width="100%">
								<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="var_GrupoAprobacionNombre" HeaderText="Grupo de aprobaci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_TipoAprobacionNombre" HeaderText="Tipo de aprobaci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_NumeroSolicitud" HeaderText="Número Solicitud"></asp:BoundColumn>
									<asp:BoundColumn DataField="num_TotalMetros" HeaderText="Metros" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="var_FechaCreacion" HeaderText="Fecha Creaci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_NombreCliente" HeaderText="Cliente"></asp:BoundColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<input class="input" id="btnSeleccion" name="btnSeleccion" runat="server" type="button"
												value="..." style="cursor:hand;" title="Presione para Registrar la Solicitud de Muestra." />
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
                        </DIV>
					</td>
				</tr>
				<tr>
					<td class="FOOTER"></td>
				</tr>
			</table>
		</form>
	</body>
</html>
