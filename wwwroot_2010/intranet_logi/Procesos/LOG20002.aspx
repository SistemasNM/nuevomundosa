<%@ Register TagPrefix="cc1" Namespace="NM.Posicionador" Assembly="Posicionador" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20002.aspx.vb" Inherits="intranet_logi.LOG20002"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20002</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
		<script language="javascript" type="text/javascript">
		    function BuscarRequisicion (Requisicion,Estado)
		    {
			    var retorno = window.showModalDialog("LOG20003.aspx?strEstado="+Estado+"&strRequisicion="+Requisicion,"","dialogHeight:450px;dialogWidth:790px;center:yes;help:no;");
			    if (retorno == "1") {
			        //document.getElementById("hdn1").value = "1";
			        //document.Form1.submit;
			        return true;
			    } else {
			        return false;
                }
		    }
		    function BuscarPedido(NumPedido, Estado)
		    {
			    var retorno = window.showModalDialog("frm_AprobacionPedidos.aspx?&strNumeroPedido="+NumPedido,"","dialogHeight:700px;dialogWidth:900px;center:yes;help:no;");
			    if (retorno == "1") {
			        //document.getElementById("hdn1").value = "1";
			        //Form1.submit;
			        return true;
			    } else {
			        return false;
                }
		    }
		    function BuscarOS(NumPedido, Estado)
		    {
	            var strLink ="";
	            strLink = "frmAprobacionOS.aspx?Usuario="+ document.getElementById("hdnusuencryptado").value +"&strNumeroOrdenServicio=" + NumPedido;
			    var retorno = window.showModalDialog(strLink,"","dialogHeight:700px;dialogWidth:900px;center:yes;help:no;");
			    if (retorno == "1") {
			        //document.getElementById("hdn1").value = "1";
			        //Form1.submit;
			        return true;
			    } else {
			        return false;
                }
			}

			function BuscarConformidad(NumPedido, Estado) {
			    var strLink = "";
			    strLink = "frmAprobacionConformidad.aspx?Usuario=" + document.getElementById("hdnusuencryptado").value + "&strNumeroConformidad=" + NumPedido;
			    var retorno = window.showModalDialog(strLink, "", "dialogHeight:700px;dialogWidth:900px;center:yes;help:no;");
			    if (retorno == "1") {
			        return true;
			    } else {
			        return false;
			    }
			}
		</script>
	</head>
	<body id="Cuerpo" >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; POSITION: absolute; WIDTH: 912px; HEIGHT: 387px; TOP: 8px; LEFT: 0px"
				cellspacing="2" cellpadding="2" width="912" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 8px">&nbsp;MÓDULO DE APROBACIÓN DE REQUISICIONES
                    </td>
				</tr>
				<tr>
					<td class="panel" valign="top">
						<table cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" width="90"><asp:panel id="Panel1" runat="server" Width="90px">Tipo requisición</asp:panel></td>
								<td width="100%"><asp:radiobutton id="rbtTodos" runat="server" Checked="True" CssClass="input" GroupName="Tipos" Text="Todos"></asp:radiobutton><asp:radiobutton id="rbtArticulos" runat="server" CssClass="input" GroupName="Tipos" Text="Artículos"></asp:radiobutton><asp:radiobutton id="rbtServicios" runat="server" CssClass="input" GroupName="Tipos" Text="Servicios"></asp:radiobutton></td>
							</tr>
						</table>
						<table style="WIDTH: 600px; HEIGHT: 16px" cellspacing="2" cellpadding="2" width="600" border="0">
							<tr>
								<td class="Etiqueta" width="90"><asp:panel id="Panel2" runat="server" Width="90px">Fecha de creación</asp:panel>
								</td>
								<td width="67"><asp:checkbox id="cbxDesde" runat="server" Checked="True" CssClass="Input" Text="Desde" AutoPostBack="True"></asp:checkbox></td>
								<td width="104"><asp:textbox id="txtDesde" runat="server" Width="104px" CssClass="input"></asp:textbox></td>
								<td width="13"><img id="imgDesde" onclick="popUpCalendar(this, Form1.txtDesde, 'dd/mm/yyyy')" height="15"
										alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" name="imgDesde"
										runat="server"></td>
								<td width="65"><asp:checkbox id="cbxHasta" runat="server" Checked="True" CssClass="Input" Text="Hasta" AutoPostBack="True"></asp:checkbox></td>
								<td width="104"><span style="WIDTH: 103px"><asp:textbox id="txtHasta" runat="server" Width="104px" CssClass="input"></asp:textbox></span></td>
								<td width="157"><img id="imgHasta" onclick="popUpCalendar(this, Form1.txtHasta, 'dd/mm/yyyy')" height="15"
										alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" runat="server"></td>
							</tr>
						</table>
						<table cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" width="90"><asp:panel id="Panel3" runat="server" Width="90px">Estado</asp:panel></td>
								<td width="100%"><asp:dropdownlist id="ddlEstado" runat="server" Width="184px" CssClass="input">
										<asp:ListItem Value="T">TODOS</asp:ListItem>
										<asp:ListItem Value="R">RECHAZADOS</asp:ListItem>
										<asp:ListItem Value="A">APROBADOS</asp:ListItem>
										<asp:ListItem Value="P" Selected="True">POR APROBAR</asp:ListItem>
									</asp:dropdownlist><input style="Z-INDEX: 0; BACKGROUND-COLOR: transparent" id="hdnusuencryptado" class="Input"
										type="hidden" name="hdnusuencryptado" runat="server"></td>
							</tr>
						</table>
						<table cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" width="90"><asp:panel id="Panel4" runat="server" Width="90px">Solicitada por</asp:panel>
								</td>
								<td width="112"><asp:textbox id="txtSolicitador" runat="server" Width="104px" CssClass="input"></asp:textbox></td>
								<td width="21"><input class="Boton" id="btnSolicitador" style="WIDTH: 20px; HEIGHT: 20px" type="button"
										value="..." name="btnSolicitador" runat="server"></td>
								<td width="100%"><asp:label id="lblSolicitada" runat="server" Width="100%" CssClass="Input"></asp:label></td>
							</tr>
						</table>
						<table cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="Etiqueta" vAlign="top" width="90"><asp:panel id="Panel5" runat="server" Width="90px">Observaciones</asp:panel></td>
								<td width="100%"><asp:textbox id="txtObservaciones" runat="server" Width="100%" CssClass="input"></asp:textbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" width="100%">
							<tr>
								<td align="right" height="26"><input id="hdn1" type="hidden" name="hdn1" runat="server" /><asp:button id="btnConsultar" runat="server" CssClass="boton" Text="Consultar"></asp:button></td>
							</tr>
						</table>
						<asp:datagrid id="DataGrid1" runat="server" Width="912px" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="gridheader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" ToolTip = "Consultar"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Tipo" HeaderText="Tipo"></asp:BoundColumn>
								<asp:BoundColumn DataField="DescripcionEstado" HeaderText="Estado"></asp:BoundColumn>
								<asp:BoundColumn DataField="NumeroSolicitud" HeaderText="Solic."></asp:BoundColumn>
								<asp:BoundColumn DataField="NumeroOrden" HeaderText="Orden/Compra"></asp:BoundColumn>
								<asp:BoundColumn DataField="UsuarioCreador" HeaderText="Solicitado por"></asp:BoundColumn>
								<asp:BoundColumn DataField="FechaCreacion" HeaderText="Fecha Creaci&#243;n"></asp:BoundColumn>
								<asp:BoundColumn DataField="NumeroRequisicion" HeaderText="Nro Requisici&#243;n"></asp:BoundColumn>
								<asp:BoundColumn DataField="FechaRequisicion" HeaderText="Fecha Documento"></asp:BoundColumn>
								<asp:BoundColumn DataField="UnidadMedida" HeaderText="Moneda">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Total" HeaderText="Imp. Neto" DataFormatString="{0:#,##0.00}">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Total2" HeaderText="Imp. Total" DataFormatString="{0:#,##0.00}">
									<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Observaciones" HeaderText="Observaciones"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
			<cc1:posicionador id="Posicionador1" style="Z-INDEX: 102; POSITION: absolute; TOP: 408px; LEFT: 80px" runat="server" Controlcito="Cuerpo"></cc1:posicionador>
        </form>
	</body>
</html>
