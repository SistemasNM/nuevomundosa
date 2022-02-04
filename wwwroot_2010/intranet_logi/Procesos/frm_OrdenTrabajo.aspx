<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_OrdenTrabajo.aspx.vb" Inherits="intranet_logi.frm_OrdenTrabajo"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>frm_OrdenTrabajo</title>
		<meta content="False" name="vs_snapToGrid"/>
		<meta content="True" name="vs_showGrid"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
		<link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet"/>
		<script language="javascript" type="text/javascript">

		
		// Funcion Busca Centro de Costos
		function fnc_BuscarMaquinas()
		{
			var retorno = window.showModalDialog("../../intranet/Buscadores/frm_GM_maquinas.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var Codigo = datos[0];
				var Nombre = datos[1];
				document.all('txtMaquina').value = Codigo;
				document.all('lblDesMaquina').value = Nombre;
			}
		}
		
		// Funcion Busca Empleados
		function fnc_BuscarPersonal()
		{
			var tipo = "EMP";
			var retorno = window.showModalDialog("../../intranet/Buscadores/frm_GM_Personal.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var codigo = datos[0];
				var nombre = datos[1];
				document.all('txtCodSolicitante').value = codigo;
				document.all('lblDesSolicitante').value = nombre;
			}
		}
		
	
		</script>
	</head>
	<body topmargin="1">
		<form id="frmRegistraPedidos" method="post" autocomplete="off" runat="server">
			<!-- Cabecera de Pagina-->
            <!--
			<table style="WIDTH: 100%; HEIGHT: 20px" cellspacing="1" cellpadding="1" border="0">
				<tr>
					<td style="WIDTH: 41.33%; HEIGHT: 17px" vAlign="middle" align="right"><asp:label id="lblUsu" Runat="server" text="Usuario:"></asp:label></td>
					<td style="WIDTH: 7.86%; HEIGHT: 17px" vAlign="middle" align="left"><asp:label id="lblUsuario" Runat="server" text="USUARIO" Font-Bold="True">USUARIO</asp:label></td>
					<td style="WIDTH: 10%; HEIGHT: 17px" vAlign="middle" align="left"></td>
				</tr>
			</table>
            -->
			<!-- Titulo -->
			<table style="WIDTH: 100%; HEIGHT: 10px" cellspacing="1" cellpadding="1" border="0">
				<tr>
					<td class="Cabecera" style="WIDTH: 100%; HEIGHT: 20px" valign="middle" align="left">Solicitud de Orden de Trabajo
					</td>
				</tr>
			</table>
			<table style="WIDTH: 58.26%; HEIGHT: 308px" cellspacing="1" cellpadding="1" border="0">
				<tr>
					<td style="HEIGHT: 255px">
						<!-- Cabecera de Pedidos  -->
						<table style="WIDTH: 0%; HEIGHT: 206px">
							<tr>
								<td class="etiqueta" style="WIDTH: 112px; HEIGHT: 23px" width="112">Solicitud OT 
									N°:
								</td>
								<td style="WIDTH: 356px; HEIGHT: 23px"><asp:textbox id="txtNumeroSoli" tabIndex="2" Runat="server" CssClass="inputDisabled2" Height="20px"
										Font-Size="9px" width="130px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 112px; HEIGHT: 22px" width="112"><asp:label id="lblNumeroPedido" runat="server" CssClass="etiqueta" Width="122px">Fecha:</asp:label></td>
								<td style="WIDTH: 356px; HEIGHT: 22px"><asp:textbox id="txtFecha" Runat="server" CssClass="inputDisabled2" Height="20px" Font-Size="9px"
										Width="80px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 112px; HEIGHT: 22px" width="112">Area:</td>
								<td style="WIDTH: 356px; HEIGHT: 22px"><asp:dropdownlist id="cboAreas" Runat="server" CssClass="cbo" Height="20px" Font-Size="9px" Width="133px"
										Enabled="false"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 112px; HEIGHT: 22px" width="112">Máquina:</td>
								<td style="WIDTH: 356px; HEIGHT: 22px"><asp:textbox id="txtMaquina" tabIndex="3" Runat="server" Height="20px" Font-Size="9px" width="70px"
										MaxLength="20"></asp:textbox><input class="boton" id="btnMaquina" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarMaquinas();"
										type="button" value="..." name="btnMaquina" />
									<asp:textbox id="lblDesMaquina" tabIndex="2" BorderStyle="None" Runat="server" Height="20px"
										Font-Size="9px" width="200px" BackColor="#CDE0EF"></asp:textbox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 112px; HEIGHT: 22px" width="112">Solicitante:</td>
								<td style="WIDTH: 356px; HEIGHT: 22px"><asp:textbox id="txtCodSolicitante" tabIndex="2" Runat="server" Height="20px" Font-Size="9px"
										width="69px" MaxLength="20"></asp:textbox><input class="boton" id="btnSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarPersonal();"
										type="button" value="..." name="btnSolicitante" />
									<asp:textbox id="lblDesSolicitante" tabIndex="2" BorderStyle="None" Runat="server" Height="20px"
										Font-Size="9px" width="200px" BackColor="#CDE0EF"></asp:textbox></td>
							</tr>
							<tr>
								<td class="etiqueta" style="WIDTH: 112px; HEIGHT: 52px">Observacion:</td>
								<td style="WIDTH: 356px; HEIGHT: 52px"><asp:textbox id="txtObservaciones" tabIndex="5" Runat="server" CssClass="input" Height="43px"
										Font-Size="9px" Width="530px" MaxLength="100" TextMode="MultiLine"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 135px; HEIGHT: 8px" colspan="5"><asp:label id="lblError" Runat="server" text="" Font-Bold="True" Height="12px" Width="672px"
										ForeColor="Red"></asp:label></td>
							</tr>
						</table>
					</td>
				<tr>
					<td>
						<table id="Table5" style="WIDTH: 99.54%; HEIGHT: 22px" cellspacing="0" cellpadding="0"
							border="0">
							<tr>
								<td style="WIDTH: 267px"></td>
								<td style="WIDTH: 10.36%" align="center"></td>
								<td style="WIDTH: 10%" align="center"></td>
								<!-- <input style="Z-INDEX: 0" id="Button1" class="boton" onclick="javascript:VerConsultaPedido();"
										value="Buscar" type="button" name="btnBuscar" runat="server"></td> -->
								<td style="WIDTH: 10%" align="center"><asp:button id="btnNuevo" Runat="server" CssClass="boton" Width="97px" Text="Nuevo" ToolTip="Nuevar OT"></asp:button></td>
								<td style="WIDTH: 2.55%" align="center"></td>
								<td style="WIDTH: 15%" align="center"><asp:button id="btnGuardar" Runat="server" CssClass="boton" Width="97px" Text="Guardar" ToolTip="Guardar OT"></asp:button>
								<td style="WIDTH: 10%" align="center"><input class="boton" id="btnSalir" onclick="javascript:fnc_Cerrar();" type="button" value="Salir"
										name="btnSalir" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><asp:label id="lblMsgError" Runat="server" text="" Font-Bold="True" ForeColor="Red"></asp:label></td>
				</tr>
			</table> <!-- fila 1  --> <!-- fila 2  --> <!-- fila 3  --> <!-- fila 4  --> <!-- Articulo  -->  <!-- Articulo  --> <!-- Detalle de Pedidos  --> <!-- Botonera --></form>
	</body>
</html>
