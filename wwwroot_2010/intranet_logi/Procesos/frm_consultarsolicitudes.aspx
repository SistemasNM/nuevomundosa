<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_consultarsolicitudes.aspx.vb" Inherits="intranet_logi.frm_consultarsolicitudes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Despacho de Muestras - Pendientes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>
		<script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		function btnSeleccion_Onclick(strCodigo)
		{
			window.location.href = "frm_Registromuestras.aspx?pstrNumeroSolicitud="+strCodigo;
			return false;
			
		}	
		
	    function ListarClientes() /*Muestra el listado de los Clientes de la empresa*/
		{
			var retorno = window.showModalDialog("../../intranet/Buscadores/frm_QryClientes.aspx","","dialogHeight:600px;dialogWidth:550px;center:yes;help:no;");
			//alert(retorno);
			//return;
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var Codigo = datos[0];
				var Nombre = datos[1];
				document.all('TxtCodigoCliente').value = Codigo;
				document.all('txtNombreCliente').value = Nombre;
				//document.frmSolicitudMuestras.HDN1.value=Nombre;
				//break;
			}
		}		
		</script>
	</head>
	<body>
		<form id="frm_consultarsolicitudes" name="frm_consultarsolicitudes" method="post" runat="server">
			<table id="Table40" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 949px; POSITION: absolute; TOP: 0px; HEIGHT: 416px" cellspacing="2" cellpadding="2" width="949" border="0">
					<tr>
						<td class="cabecera" style="HEIGHT: 22px">&nbsp;CONSULTAR SOLICITUDES DE MUESTRA</td>
					</tr>
					<tr>
						<td class="panel">
							<table style="WIDTH: 720px; HEIGHT: 16px" border="0">
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Cliente:</td>
									<td style="WIDTH: 94px" width="94"><asp:textbox id="TxtCodigoCliente" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
									<td style="WIDTH: 49px; HEIGHT: 8px" width="49"><input class="boton" id="btnClienteSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="ListarClientes();"
											type="button" size="20" value="..." name="btnClienteSolicitante"></td>
									<td style="HEIGHT: 8px" colspan="2"><asp:textbox id="txtNombreCliente" runat="server" Width="360px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox></td>
									<td></td>
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Fecha Inicio:</td>
									<td style="WIDTH: 98px" width="98"><asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
									<td style="WIDTH: 50px">&nbsp; <img onclick="popUpCalendar(this, frm_consultarsolicitudes.txtFechaIni, 'dd/mm/yyyy')"
											height="15" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0">&nbsp;
									</td>
									<td class="etiqueta" style="WIDTH: 108px">&nbsp;Fecha Fin :</td>
									<td>&nbsp;<asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input"></asp:textbox>
										&nbsp;<img onclick="popUpCalendar(this, frm_consultarsolicitudes.txtFechaFin, 'dd/mm/yyyy')"
											height="15" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0">&nbsp;
									</td>
									<td>&nbsp;<asp:button id="btnBuscar" runat="server" Text="Buscar"></asp:button></td>
								</tr>
							</table>
							<table id="Table1" cellspacing="1" cellpadding="1" width="98%" border="0">
								<tr>
									<td>
										<!--GridAlternateItem-->
										<DIV style="WIDTH: 100%; HEIGHT: 300px;overflow: auto;"><asp:datagrid id="dtgLista" runat="server" Width="100%" AutoGenerateColumns="False">
												<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
												<ItemStyle CssClass="GridItem"></ItemStyle>
												<HeaderStyle CssClass="gridheader"></HeaderStyle>
												<Columns>
													<asp:TemplateColumn>
														<ItemTemplate>
															<input class="input" id="btnSeleccion" name="btnSeleccion" runat="server" type="button"
																value="..." style="cursor:hand;" title="Presione para Consultar la Solicitud de Muestra.">
														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="RUCCLI" HeaderText="RUC"></asp:BoundColumn>
													<asp:BoundColumn DataField="CLIENTE" HeaderText="Cliente"></asp:BoundColumn>
													<asp:BoundColumn DataField="NROSOL" HeaderText="Número Solicitud"></asp:BoundColumn>
													<asp:BoundColumn DataField="FECSOL" HeaderText="Fec. Solicitud"></asp:BoundColumn>
													<asp:BoundColumn DataField="ESTADO" HeaderText="Estado"></asp:BoundColumn>
													<asp:BoundColumn DataField="GRR" HeaderText="Número Guia"></asp:BoundColumn>
													<asp:BoundColumn DataField="UsuarioCreacion" HeaderText="Usuario"></asp:BoundColumn>
												</Columns>
											</asp:datagrid></DIV>
									</td>
								</tr>
								<tr>
									<td align="center">
										<table class="botonera7" id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center"
											border="0">
											<tr>
												<td align="left"><asp:label id="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td class="FOOTER"></td>
								</tr>
							</table>
                        </td>
                    </tr>
            </table>
		</form>
	</body>
</html>
