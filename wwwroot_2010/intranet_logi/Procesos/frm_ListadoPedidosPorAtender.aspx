<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ListadoPedidosPorAtender.aspx.vb" Inherits="intranet_logi.frm_ListadoPedidosPorAtender"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Nuevo Mundo - Listado de Pedidos/Logistica</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
<%--		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
		<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
		<link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>--%>

        <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
        <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
        <script src="../js/jsCalendario_N4.js" type="text/javascript"></script>
        <script src="../js/functions.js" type="text/javascript"></script>

		<script language="javascript" type="text/javascript">
		    // Funcion enlace con Despacho
		    function VerAtenderPedido(NumPedido) {
                var dt_time = new Date().getTime();
                var strURL = "frm_RegistroDespachoPedidos.aspx?&strNumeroPedido=" + NumPedido + "&dtTime=" + dt_time;
                var retorno = window.showModalDialog(strURL, "", "dialogHeight:650px;dialogwidth:950px;center:yes;help:no;");		        
		      }
		      function VerAtenderHilo(NumPedido, CodAlmacen) {
                if (CodAlmacen=="015") {
		            var dt_time = new Date().getTime();
		            var strURL = "frm_RegistroDespachoPedidosDesperdicios.aspx?&strNumeroPedido=" + NumPedido + "&strCodigoAlmacen=" + CodAlmacen + "&dtTime=" + dt_time;
		            var retorno = window.showModalDialog(strURL, "", "dialogHeight:650px;dialogwidth:950px;center:yes;help:no;");
                }
		    }
		    //---------------------------------------------------------------------------------------------
		    //PEDIDOS EPPS - DG - 14/09/2018 - INI
		    //---------------------------------------------------------------------------------------------
		    function VerAtenderPedidoEPPsOtros(NumPedido) {
		        var dt_time = new Date().getTime();
		        var strURL = "frm_RegistroDespachoPedidosEPPsOtros.aspx?&strNumeroPedido=" + NumPedido + "&dtTime=" + dt_time;
		        var retorno = window.showModalDialog(strURL, "", "dialogHeight:650px;dialogwidth:950px;center:yes;help:no;");
		    }
		    //---------------------------------------------------------------------------------------------
		    //PEDIDOS EPPS - DG - 14/09/2018 - FIN
		    //---------------------------------------------------------------------------------------------

		    //Empleados
		    function fnc_Buscartrabajadores() {
		        var tipo = "EMP";
		        var dt_time = new Date().getTime();
		        var strURL = "../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo + "&dtTime=" + dt_time;
		        var retorno = window.showModalDialog(strURL, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
		        if (retorno != "" && retorno != ":") {
		           var datos = retorno.split(":");
		           var codigo = datos[0];
		           var nombre = datos[1];
		           document.all('txtSolicitante').value = codigo;
		           document.all('lblSolicitante').value = nombre;
		        }
		    }
		    function fnc_Cerrar() {
		        window.open('', '_parent', '');
		        window.close();
		    }

		    //FormatearBusqDoc
		    function FormatearBusqDoc(pTexto) {
		        //serie
		        if (pTexto == 1) {
		            var lserie = '000000' + document.all["txtSerie"].value;
		            lserie = lserie.substring(lserie.length, lserie.length - 4);
		            if (lserie == '0000') {
		                document.all["txtSerie"].value = '';
		            }
		            else {
		                document.all["txtSerie"].value = lserie;
		            }
		        }
		        //numero
		        if (pTexto == 2) {
		            var lnume = '00000000000' + document.all["txtNumeroDocumento"].value;
		            lnume = lnume.substring(lnume.length, lnume.length - 10);
		            if (lnume == '0000000000') {
		                document.all["txtNumeroDocumento"].value = '';
		            }
		            else {
		                document.all["txtNumeroDocumento"].value = lnume;
		            }
		        }
		    }
		</script>
	</head>
	<body>
		<form id="frmListadoPedidosPorAtender" method="post" autocomplete="off" runat="server">
			<!-- Cabecera de Pagina-->
			<table border="0" cellspacing="1" cellpadding="1" width="800px">
				<tr>
					<td style="HEIGHT: 20px" class="Cabecera">LISTADO DE VALES DE SALIDA - PENDIENTES DE DESPACHO</td>
				</tr>
			</table>

            <!--Filtros-->
			<table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
			    <tr>
				    <td style="width: 100px; HEIGHT: 20px" class="Etiqueta">&nbsp;Fecha Inicio:</td>
					<td style="width: 150px; HEIGHT: 20px">
                     <asp:textbox id="txtFechaIni" runat="server" width="100px" CssClass="input"></asp:textbox>
                     &nbsp;<img onclick="popUpCalendar(this, frmListadoPedidosPorAtender.txtFechaIni, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="15" height="15" />
                    </td>
					<td style="width: 100px; HEIGHT: 20px" class="etiqueta">&nbsp;Estado:</td>
					<td style="width: 350px; HEIGHT: 20px">
                     <asp:dropdownlist id="ddlEstado" runat="server" width="130px" CssClass="input" AutoPostBack="false" Height="20px">
                      <asp:ListItem Value="APR" Selected="True">PENDIENTES</asp:ListItem>
                      <asp:ListItem Value="PRE">PRE-DESPACHO</asp:ListItem>
					  <asp:ListItem Value="ATE">ATENDIDOS</asp:ListItem>
					  <asp:ListItem Value="FIN">CULMINADO</asp:ListItem>
					 </asp:dropdownlist></td>
					<td style="width: 100px; HEIGHT: 20px" valign="middle" align="right">
                     <asp:button id="btnBuscar" runat="server" Font-Bold="True" width="80px" CssClass="boton" Height="20px" Text="Buscar"></asp:button>
                    </td>
				</tr>

				<tr>
					<td style="width: 100px; HEIGHT: 20px" class="etiqueta">&nbsp;Fecha Fin:</td>
					<td style="width: 150px; HEIGHT: 20px">
                     <asp:textbox id="txtFechaFin" runat="server" width="100px" CssClass="input"></asp:textbox>
                     &nbsp;<img onclick="popUpCalendar(this, frmListadoPedidosPorAtender.txtFechaFin, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="15" height="15" />
					</td>
					<td style="width: 100px; HEIGHT: 20px" class="etiqueta">&nbsp;Solic. por:</td>
					<td style="width: 350px; HEIGHT: 20px">
                     <asp:textbox id="txtSolicitante" runat="server" Height="20px" Font-Size="10px" width="110px"></asp:textbox>
                     &nbsp;<input id="btnSolicitante" style="width:20px;" onclick="javascript:fnc_Buscartrabajadores();" type="button" value="..." name="btnSolicitante" class="Boton"/>
                     &nbsp;<asp:label id="lblSolicitante" runat="server" text="" Font-Bold="false" width="200px" Height="20px" Font-Size="8px"></asp:label>
                    </td>
					<td style="width: 100px; HEIGHT: 20px" align="right">
                    <input id="btnSalir" class="boton" onclick="javascript:fnc_Cerrar()" value="Salir" type="button" name="btnSalir" />
                    </td>
				</tr>

				<tr>
					<td style="width: 100px; HEIGHT: 20px" class="etiqueta">&nbsp;Fec. Inic. Ins.</td>
					<td style="width: 150px; HEIGHT: 20px">
                     <asp:textbox id="txtFecIniIns" runat="server" width="100px" CssClass="input"></asp:textbox>
                     &nbsp;<img onclick="popUpCalendar(this, frmListadoPedidosPorAtender.txtFecIniIns, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px" />
                    </td>
					<td style="width: 100px; HEIGHT: 20px" class="etiqueta">&nbsp;Fec. Fin. Ins.</td>
					<td style="width: 300px; HEIGHT: 20px">
                     <asp:textbox id="txtFecFinIns" runat="server" width="110px" CssClass="input"></asp:textbox>
                     &nbsp;<img onclick="popUpCalendar(this, frmListadoPedidosPorAtender.txtFecFinIns, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px" />
                    </td>
					<td style="width: 100px; HEIGHT: 20px"></td>
				</tr>
        <tr>
	       <td style="width: 100px; HEIGHT:20px" align="left" class="Etiqueta">Tipo:</td>
	       <td style="width: 150px; HEIGHT:20px" align="left">
         <%-- <asp:CheckBox ID="chkTipo" runat="server" Text="Ped. Desp. Algodon" Checked="false" AutoPostBack="true" CssClass="input"/>--%>
         <asp:DropDownList ID="ddwTipo" runat="server"  AutoPostBack="true">
            <asp:ListItem Value="1" Selected="true">Vale de Almacen</asp:ListItem>
            <asp:ListItem Value="2">Ped. Desp. Algodon</asp:ListItem>
            <asp:ListItem Value="3">Vale de EPPsOtros</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td style="width:100px; HEIGHT:20px" align="left" class="Etiqueta">&nbsp;Num. Pedido:</td>
         <td style="width:300px; HEIGHT: 20px">
         <asp:textbox id="txtSerie" runat="server" width="40px" Font-Size= "10px" MaxLength = "5" CssClass="inputDisabled"></asp:textbox>
          &nbsp;<asp:TextBox ID="txtNumeroDocumento" runat="server" width="90px" Font-Size= "9px" MaxLength = "10" CssClass="input"></asp:TextBox>
         </td>
         <td style="width:100px; HEIGHT: 20px"></td>
        </tr>
       </table>
       
       <table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
				<tr>
                 <td style="width: 800px; height:20px">
                  <asp:label id="lblMsg" runat="server" width="800px" Height="20px" CssClass="error"></asp:label>
                 </td>
				</tr>
                <tr>
                 <td style="width: 800px; height:20px" align="left">
                  <asp:label id="lblItems" runat="server" width="200px" Height="20px" Font-Size = "9px"></asp:label>
                 </td>
                </tr>
                </table>
                
                <%--grilla--%>
                <table style="width: 1200px" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td style="width: 1200px">
                     <asp:datagrid id="dgListaPedidos" runat="server" width="1200px" AutoGenerateColumns="False">
					 <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
					 <ItemStyle CssClass="GridItem"></ItemStyle>
					 <HeaderStyle Font-Size="12px" Font-Bold="True" Height="20px" CssClass="GridHeader"></HeaderStyle>
					 <Columns>

					  <asp:TemplateColumn>
            <HeaderStyle width="20px"></HeaderStyle>
					   <ItemTemplate>
						<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" Width="16px" Height="16px"></asp:ImageButton>
					   </ItemTemplate>
					  </asp:TemplateColumn>
					
                      <asp:BoundColumn DataField="nu_pedi" HeaderText="Num. Pedido">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="100px"></HeaderStyle>
					   <ItemStyle Font-Size="9px" HorizontalAlign="Left" Font-Bold="True" width="100px"></ItemStyle>
					  </asp:BoundColumn>
					  
                      <asp:BoundColumn DataField="ti_situ" HeaderText="Situacion">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="80px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Center" width="80px"></ItemStyle>
					  </asp:BoundColumn>

                      <asp:BoundColumn DataField="fe_pedi" HeaderText="Fec. Ped.">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="50px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Center" width="50px"></ItemStyle>
					  </asp:BoundColumn>

                      <asp:BoundColumn DataField="FechaInstalacion" HeaderText="Fec. Ins.">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="50px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Center" width="50px"></ItemStyle>
					  </asp:BoundColumn>
					
                      <asp:BoundColumn DataField="fe_apro" HeaderText="Fec. Apro.">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="50px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Center" width="50px"></ItemStyle>
					  </asp:BoundColumn>	

                      <asp:BoundColumn DataField="de_alma" HeaderText="Almacen">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="150px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Left" width="150px"></ItemStyle>
					  </asp:BoundColumn>
					
                      <asp:BoundColumn DataField="dias" HeaderText="Dias Pend">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="50px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Center" width="50px"></ItemStyle>
					  </asp:BoundColumn>
								
                      <asp:BoundColumn DataField="NomSolicitante" HeaderText="Solicitante">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="150px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Left" width="180px"></ItemStyle>
					  </asp:BoundColumn>
								
                      <asp:BoundColumn DataField="DesCentroCostos" HeaderText="Centro Costos">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="200px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
					  </asp:BoundColumn>
						
                      <asp:BoundColumn DataField="de_obse" HeaderText="Observacion">
					   <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="300px"></HeaderStyle>
					   <ItemStyle Font-Size="8px" HorizontalAlign="Left" Width="300px"></ItemStyle>
					  </asp:BoundColumn>
                      
					  <asp:BoundColumn DataField="vch_CodReserva" HeaderText="Cod. Reserva">
                       <HeaderStyle Font-Bold="True" HorizontalAlign="Center" width="80px"></HeaderStyle>
					   <ItemStyle Font-Size="9px" HorizontalAlign="Center" width="80px"></ItemStyle>
                      </asp:BoundColumn>
                      
					 </Columns>
					</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</html>

