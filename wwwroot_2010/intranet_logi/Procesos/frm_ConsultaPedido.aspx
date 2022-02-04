<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ConsultaPedido.aspx.vb" Inherits="intranet_logi.frm_ConsultaPedido"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Consulta de pedido</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
		<link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/EstilosWeb.css"/>
		<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>
		<script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		    function fnc_Cerrar()
		    {
			    window.open('','_parent','');
			    window.close();
		    }
		    // Funcion Busca Empleados
		    function fnc_BuscarTrabajadores()
		    {
			    var tipo = "EMP";
			    var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo="+tipo,"","dialogHeight:450px;dialogwidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":")
			    {
				    var datos = retorno.split(":");
				    var codigo = datos[0];
				    var nombre = datos[1];
				    document.all('txtSolicitante').value = codigo;
				    document.all('lblSolicitante').value = nombre;
			    }
		    }
		    // Funcion: Carga la ventana de Registro de Pedidos
		    function VerRegistroPedido(NumPedido)
		    {
			    //window.location.href = "frm_RegistroPedido.aspx?strNumeroPedido="+NumPedido;
			    //window.open("frm_RegistroPedido.aspx?&strNumeroPedido="+NumPedido,"","");
			    returnValue = NumPedido;
			    window.open('','_parent','');
			    window.close();
		    }
		    function FormatearBusqDoc(pTexto)
		    {
			    if(pTexto==1)//serie
			    {
				    var lserie='000000'+document.all["txtSerie"].value;
				    lserie=lserie.substring(lserie.length, lserie.length-4);
				    if (lserie=='0000')
				    {
					    document.all["txtSerie"].value='';
				    }else{
					    document.all["txtSerie"].value=lserie;
				    }//end if
				
			    }//end if
			
			    if(pTexto==2)//numero
			    {
				    var lnume='00000000000'+document.all["txtNumeroPedido"].value;
				    lnume=lnume.substring(lnume.length,lnume.length-10);
				    if (lnume=='0000000000')
				    {
					    document.all["txtNumeroPedido"].value='';
				    }else{
					    document.all["txtNumeroPedido"].value=lnume;
				    }//end if
				
			    }//end if
		    }//end function
		</script>
	</head>
	<body>
		<form id="frm_ConsultaPedidos" method="post" runat="server">
        <center>
			<!-- Titulo -->
			<table id="tblCabeceraPagina" border="0" cellspacing="1" cellpadding="1" width="800px">
				<tr>
					<td style="width:800px; height:30px" class="Cabecera">Consulta de&nbsp;Vale&nbsp;de Almacen</td>
				</tr>
			</table>
			<!-- Cabecera de Pagina -->
			<table style="width: 800px" border="0">
				<tr>
					<td style="width:120px" class="Etiqueta">Fec. inicio:</td>
					<td style="width:140px">
                     <asp:textbox id="txtFechaIni" runat="server" width="95px" CssClass="input"></asp:textbox>
                     <img onclick="popUpCalendar(this, frm_ConsultaPedidos.txtFechaIni, 'dd/mm/yyyy')" border="0"
							alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"/></td>
					<td style="width:120px" class="etiqueta" width="75">Estado:</td>
					<td style="width:140px">
                     <asp:dropdownlist id="ddlEstado" runat="server" width="120px" CssClass="input"	AutoPostBack="false">
					 <asp:ListItem Value="ALL" Selected="True">TODOS</asp:ListItem>
					 <asp:ListItem Value="ACT">ACTIVO</asp:ListItem>
					 <asp:ListItem Value="APR">APROBADO</asp:ListItem>
					 <asp:ListItem Value="ATE">ATENDIDO</asp:ListItem>
					 <asp:ListItem Value="ANU">ANULADO</asp:ListItem>
					</asp:dropdownlist></td>
					<td style="width:280px">
					 <asp:button id="btnBuscar" runat="server" Font-Bold="false" width="80px" Text="Buscar" CssClass="boton" ></asp:button>
                    </td>
				</tr>
				<tr>
					<td style="width:120px" class="etiqueta">Fec. fin :</td>
					<td style="width:140px">
                     <asp:textbox id="txtFechaFin" runat="server" width="95px" CssClass="input"></asp:textbox>
                     <img onclick="popUpCalendar(this, frm_ConsultaPedidos.txtFechaFin, 'dd/mm/yyyy')" border="0"
							alt="Seleccionar fecha Final" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px" />
					</td>
					<td style="width:120px" class="etiqueta">Solicitado por:</td>
					<td style="width:140px">
                     <asp:textbox id="txtSolicitante" runat="server" width="100px"
							CssClass="input"></asp:textbox>
						<input style="width:20px; height:20px" id="btnSolicitante" class="boton"
							onclick="javascript:fnc_BuscarTrabajadores();" value="..." type="button" name="btnSolicitante" /></td>
					<td style="width:280px">
                     <input id="btnSalir" onclick="javascript:fnc_Cerrar()" value="Salir" type="button" name="btnSalir" class="boton"/>
                    </td>
				</tr>
				<tr>
					<td style="width:120px" class="etiqueta">Num. Ped.:</td>
					<td style="width:140px">
                     <asp:textbox id="txtSerie" runat="server" width="30px" Font-Size="10px"></asp:textbox>
                     <asp:textbox id="txtNumeroPedido" runat="server" width="80px" Font-Size="10px"></asp:textbox></td>
					<td style="width:120px"></td>
					<td style="width:140px">
						<asp:label id="lblSolicitante" text="" runat="server" Font-Bold="True" width="309px" Font-Size="9px"></asp:label></td>
					<td style="width:280px"></td>
				</tr>
			</table>
            <table style="width:800px" border="0" cellspacing="0" cellpadding="0">
             <tr>
			  <td align="left"><asp:label id="lblMsg" runat="server" width="800px" CssClass="error"></asp:label></td>
			 </tr>
             <tr>
			  <td align="right"><asp:label id="lblItems" runat="server" width="800px" CssClass="txtReg"></asp:label></td>
			 </tr>
            </table>
			<table style="width:800px" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td><asp:datagrid id="dgListaPedidos" runat="server" width="800px" AutoGenerateColumns="False">
							<HeaderStyle Font-Bold="True" Font-Size="10px" CssClass="GridHeader"></HeaderStyle>
							<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<Columns>
								<asp:TemplateColumn>
                                 <HeaderStyle HorizontalAlign="Center" width="20px"></HeaderStyle>
									<ItemTemplate>
										<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" CommandName="Consulta"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Num. Pedido">
									<HeaderStyle HorizontalAlign="Center" width="80px"></HeaderStyle>
									<ItemStyle HorizontalAlign="left" Font-Size="8px" width="80px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nu_pedi") %>' Font-Size="8px"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:BoundColumn DataField="fe_pedi" HeaderText="Fec. Pedido">
									<HeaderStyle HorizontalAlign="Center" width="60px"></HeaderStyle>
									<ItemStyle HorizontalAlign="center" Font-Size="8px" width="60px"></ItemStyle>
								</asp:BoundColumn>

								<asp:BoundColumn DataField="de_alma" HeaderText="Almacen">
									<HeaderStyle HorizontalAlign="Center" width="200px"></HeaderStyle>
									<ItemStyle HorizontalAlign="left" Font-Size="8px" width="200px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ti_situ" HeaderText="Situacion">
									<HeaderStyle HorizontalAlign="Center" width="70px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Font-Size="8px" width="70px"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="de_obse" HeaderText="Observaciones">
									<HeaderStyle HorizontalAlign="Center" width="380px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Font-Size="8px" width="370px"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
            </center>
		</form>
	</body>
</html>
