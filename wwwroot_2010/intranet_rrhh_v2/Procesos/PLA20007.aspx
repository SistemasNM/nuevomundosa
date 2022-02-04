<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20007.aspx.vb" Inherits="intranet_rrhh_v2.PLA20007"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA20007</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href='<% =ResolveClientUrl("../../intranet/Estilos/tab.webfx.css")%>' type="text/css" rel="stylesheet" />
		<link href='<% =ResolveClientUrl("../../intranet/Estilos/NM0001.css")%>' type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript" src='<% =ResolveClientUrl("../../intranet/JS/jsCalendario_N3.js")%>'></script>
		<script type="text/javascript" language="javascript" src='<% =ResolveClientUrl("../../intranet/js/tabpane.js")%>'></script>

		<script type="text/javascript" language="javascript">
		  var tabPane;
		  function showArticleTab(sName) {
		    if (typeof tabPane != "undefined") {
		      switch (sName) {
		        case "PLANILLA":
		          tabPane.setSelectedIndex(0);
		          break;
		        case "DETALLE":
		          tabPane.setSelectedIndex(1);
		          break;
		        case "REGISTRO":
		          tabPane.setSelectedIndex(2);
		          break;
		      }
		    }
		  }
				
		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 18px">INTERFASE A BANCOS</td>
				</tr>
				<tr>
					<td>
						<div class="tab-pane" id="tab-pane-1">
							<script type="text/javascript">
							  tabPane = new WebFXTabPane(document.getElementById("tab-pane-1"), true);
							</script>
							<!-- PLANILLA -->
							<div class="tab-page" id="divPlanilla" style="HEIGHT: 250px">
								<h2 class="tab">Datos</h2>
								<table id="Table2" cellspacing="1" cellpadding="1" width="350" border="0">
									<tr>
										<td class="etiqueta" style="HEIGHT: 16px" width="90">Planilla</td>
										<td style="WIDTH: 184px; HEIGHT: 16px" width="184"><asp:dropdownlist id="ddlPlanilla" tabIndex="2" runat="server" CssClass="input" AutoPostBack="True">
												<asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
												<asp:ListItem Value="EMP">Empleados</asp:ListItem>
												<asp:ListItem Value="OBM">Obreros</asp:ListItem>
											</asp:dropdownlist></td>
										<td style="HEIGHT: 16px" width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" style="HEIGHT: 16px" width="90">Banco</td>
										<td style="WIDTH: 184px; HEIGHT: 16px" width="184"><asp:dropdownlist id="ddlBanco" tabIndex="2" runat="server" CssClass="input" AutoPostBack="True">
												<asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
												<asp:ListItem Value="02">Continental</asp:ListItem>
												<asp:ListItem Value="03">Interbank</asp:ListItem>
												<asp:ListItem Value="15">Scotiabank</asp:ListItem>
												<asp:ListItem Value="01">Credito</asp:ListItem>
											</asp:dropdownlist></td>
										<td style="HEIGHT: 16px" width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" style="HEIGHT: 3px" width="90">Moneda</td>
										<td style="WIDTH: 184px; HEIGHT: 3px" width="184"><asp:dropdownlist id="ddlMoneda" tabIndex="2" runat="server" CssClass="input" AutoPostBack="True">
												<asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
												<asp:ListItem Value="DOL">Dolares</asp:ListItem>
												<asp:ListItem Value="SOL">Soles</asp:ListItem>
											</asp:dropdownlist></td>
										<td style="HEIGHT: 3px" width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" width="90">Cuenta</td>
										<td style="WIDTH: 184px" width="184"><asp:dropdownlist id="ddlCuenta" runat="server" CssClass="Input" Width="240px"></asp:dropdownlist></td>
										<td width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" style="HEIGHT: 4px" width="90">Periodo</td>
										<td style="WIDTH: 184px; HEIGHT: 8px" width="184"><asp:textbox id="txtAnno" tabIndex="3" runat="server" CssClass="Input" Width="40px"></asp:textbox>&nbsp;&nbsp;&nbsp;
											<asp:dropdownlist id="ddlMes" tabIndex="2" runat="server" CssClass="input" AutoPostBack="True" Width="96px">
												<asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
												<asp:ListItem Value="01">Enero</asp:ListItem>
												<asp:ListItem Value="02">Febrero</asp:ListItem>
												<asp:ListItem Value="03">Marzo</asp:ListItem>
												<asp:ListItem Value="04">Abril</asp:ListItem>
												<asp:ListItem Value="05">Mayo</asp:ListItem>
												<asp:ListItem Value="06">Junio</asp:ListItem>
												<asp:ListItem Value="07">Julio</asp:ListItem>
												<asp:ListItem Value="08">Agosto</asp:ListItem>
												<asp:ListItem Value="09">Septiembre</asp:ListItem>
												<asp:ListItem Value="10">Octubre</asp:ListItem>
												<asp:ListItem Value="11">Noviembre</asp:ListItem>
												<asp:ListItem Value="12">Diciembre</asp:ListItem>
											</asp:dropdownlist></td>
										<td style="HEIGHT: 8px" width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" width="90">Fecha Abono</td>
										<td style="WIDTH: 184px" width="185"><asp:textbox id="txtFechaPago" tabIndex="3" 
                                                runat="server" CssClass="Input" AutoPostBack="True"
												Width="88px"></asp:textbox>&nbsp; <img onclick="popUpCalendar(this, Form1.txtFechaPago, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
												src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
										<td width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" width="90">Trabajadores</td>
										<td style="WIDTH: 184px" width="185"><asp:textbox id="txtCTrabI" tabIndex="3" runat="server" CssClass="Input" Width="56px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:textbox id="txtCTrabF" tabIndex="3" runat="server" CssClass="Input" Width="56px"></asp:textbox></td>
										<td width="15"></td>
									</tr>
								</table>
							</div>
							<!-- DETALLE -->
							<div class="tab-page" id="divDetalle" style="HEIGHT: 250px">
								<h2 class="tab">Datos Adicionales</h2>
								<P class="tab">
									<table id="Table3" cellspacing="1" cellpadding="1" width="350" border="0">
										<tr>
											<td class="etiqueta" style="WIDTH: 69px; HEIGHT: 16px" width="69">Archivo</td>
											<td style="WIDTH: 184px; HEIGHT: 16px" width="184"><asp:textbox id="txtArchivo" tabIndex="3" runat="server" CssClass="Input" Width="134px"></asp:textbox></td>
											<td style="HEIGHT: 16px" width="15"></td>
										</tr>
										<tr>
											<td class="etiqueta" style="WIDTH: 69px; HEIGHT: 16px" width="69">Tipo Cambio</td>
											<td style="WIDTH: 184px; HEIGHT: 16px" width="184"><asp:textbox id="txtTC" tabIndex="3" runat="server" CssClass="Input" Width="56px"></asp:textbox></td>
											<td style="HEIGHT: 16px" width="15"></td>
										</tr>
										<tr>
											<td class="etiqueta" style="WIDTH: 69px; HEIGHT: 16px" width="69">Codigo Empresa</td>
											<td style="WIDTH: 184px; HEIGHT: 25px" width="184"><asp:textbox id="txtCodEmpresa" tabIndex="3" runat="server" CssClass="Input" Width="136px"></asp:textbox></td>
											<td style="HEIGHT: 16px" width="15"></td>
										</tr>
									</table>
								</P>
							</div>
							<!-- REGISTRO -->
							<div class="tab-page" id="divRegistro" style="HEIGHT: 250px">
								<h2 class="tab">Abonos</h2>
								<table id="Table4" cellspacing="1" cellpadding="1" width="350" border="0">
									<tr>
										<td class="etiqueta" style="WIDTH: 105px; HEIGHT: 16px" width="105">Abono</td>
										<td style="WIDTH: 184px; HEIGHT: 21px" width="184"><asp:radiobuttonlist id="rblAbonos" runat="server" AutoPostBack="True" Width="112px" Height="116px">
												<asp:ListItem Value="01">Fin de Mes</asp:ListItem>
												<asp:ListItem Value="02">Por Concepto</asp:ListItem>
												<asp:ListItem Value="03">Mov.Varios</asp:ListItem>
												<asp:ListItem Value="04">C.T.S.</asp:ListItem>
												<asp:ListItem Value="05" Selected="True">Cuenta Corriente</asp:ListItem>
											</asp:radiobuttonlist></td>
										<td style="HEIGHT: 16px" width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" style="WIDTH: 105px; HEIGHT: 16px" width="105">Concepto</td>
										<td style="WIDTH: 184px; HEIGHT: 16px" width="184"><asp:textbox id="txtConcepto" tabIndex="3" runat="server" CssClass="Input" Width="56px" Enabled="False"></asp:textbox></td>
										<td style="HEIGHT: 16px" width="15"></td>
									</tr>
									<tr>
										<td class="etiqueta" style="WIDTH: 105px; HEIGHT: 16px" width="105">Filtro x Moneda</td>
										<td style="WIDTH: 184px; HEIGHT: 25px" width="184"><asp:dropdownlist id="ddlFMoneda" tabIndex="2" runat="server" CssClass="input" AutoPostBack="True"
												Width="96px">
												<asp:ListItem Value="Seleccionar" Selected="True">Seleccionar</asp:ListItem>
												<asp:ListItem Value="SOL">Soles</asp:ListItem>
												<asp:ListItem Value="DOL">Dolares</asp:ListItem>
											</asp:dropdownlist></td>
										<td style="HEIGHT: 16px" width="15"></td>
									</tr>
								</table>
							</div>
						</div>
					</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td class="pie"><asp:button id="btnGrabar" accessKey="G" runat="server" CssClass="boton" Enabled="False" Text="Generar"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</html>
