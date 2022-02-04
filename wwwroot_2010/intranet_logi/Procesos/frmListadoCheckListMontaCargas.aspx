<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListadoCheckListMontaCargas.aspx.vb" Inherits="intranet_logi.frmListadoCheckListMontaCargas" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
	<title>Nuevo Mundo - Seguimiento de Pedidos</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
	<meta name="vs_defaultClientScript" content="JavaScript"/>
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
	<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
	<link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
	<link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
	<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
	<script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
	<script language="javascript" type="text/javascript">

	    // Mostrar reporte
	    function fMostrarReporte(strURL) {
	        var intWidth = screen.width;
	        var intHeight = screen.height;
	        window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
	    }

	    function fnc_BuscarTrabajadores() {
	        var tipo = "EMP";
	        var retorno = window.showModalDialog("../Buscadores/frmBuscadorOperario.aspx?strTipo=" + tipo, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
	        if (retorno != "" && retorno != ":") {
	            var datos = retorno.split(":");
	            var codigo = datos[0];
	            var nombre = datos[1];
	            document.all('txtSolicitante').value = codigo;
	            document.getElementById('lblDesSolicitante').innerHTML = nombre;
	        }
	    }

	    function fnc_Cerrar() {
	        window.open('', '_parent', '');
	        window.close();
	    }

	    function FormatearBusqDoc(pTexto) {
	        if (pTexto == 2) {
	            var lnume = '00000000000' + document.all["txtNumeroPedido"].value;
	            lnume = lnume.substring(lnume.length, lnume.length - 10);
	            if (lnume == '0000000000') {
	                document.all["txtNumeroPedido"].value = '';
	            } else {
	                document.all["txtNumeroPedido"].value = lnume;
	            }
	        }
	    }

	</script>
  </head>
  <body>
	<form id="frmListadoChkMontacarga" method="post" runat="server" autocomplete="off">
      <table border="0" cellspacing="0" cellpadding="0" width="800px">
		<tr>
		  <td style="WIDTH:800px;height:20px" class="Cabecera">CHECK LIST MONTACARGAS</td>
		</tr>
	  </table>
	  <table style="WIDTH:800px" border="0" cellspacing="0" cellpadding="0">
		<tr>
		  <td style="WIDTH: 120px" class="Etiqueta">Fecha Inicio:</td>
		  <td style="WIDTH: 150px">
           <asp:textbox id="txtFechaIni" runat="server" CssClass="input" Width="100px" MaxLength="10"></asp:textbox>
           <img onclick="popUpCalendar(this, frmListadoChkMontacarga.txtFechaIni, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  />
          </td>
		  <td style="WIDTH: 120px" class="etiqueta" valign="middle" align="left">Fecha Fin:</td>
          <td style="WIDTH: 150px">
		   <asp:textbox id="txtFechaFin" runat="server" CssClass="input" Width="100px" MaxLength="10"></asp:textbox>
           <img onclick="popUpCalendar(this, frmListadoChkMontacarga.txtFechaFin, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  />
          </td>
		  <td style="WIDTH: 260px" valign="middle" align="left">
           <asp:button id="btnBuscar" runat="server" Font-Bold="True" CssClass="boton" Width="80px" Text="Buscar"></asp:button>
          </td>
		</tr>

		<tr>
		 <td style="WIDTH:120px" class="etiqueta">Turno:</td>
		 <td style="WIDTH:150px">
          <asp:dropdownlist id="ddlEstado" runat="server" CssClass="input" Width="120px" AutoPostBack="false">
		   <asp:ListItem Value="0" Selected="True">Seleccione Turno</asp:ListItem>
		   <asp:ListItem Value="1">1er</asp:ListItem>
		   <asp:ListItem Value="2">2do</asp:ListItem>
		   <asp:ListItem Value="3">3er</asp:ListItem>
		  </asp:dropdownlist>
         </td>
		 <td style="WIDTH:120px" >&nbsp;</td>
         <td style="WIDTH:150px">
		  <%--<asp:dropdownlist id="cboPrioridad" Runat="server" Width="120px" CssClass="cbo" Font-Size="9px">
		   <asp:ListItem>TODOS</asp:ListItem>
		   <asp:ListItem Value="NOR">Normal</asp:ListItem>
		   <asp:ListItem Value="URG">Urgente</asp:ListItem>
		  </asp:dropdownlist>--%>
         </td>
		 <td style="WIDTH:260px">             
          <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Boton"/><%--<input id="btnSalir" class="boton" onclick="javascript:fnc_Cerrar()" value="Salir" type="button" name="btnSalir" />--%>
         </td>
		</tr>

        <tr>
		 <td style="WIDTH:120px" class="Etiqueta">Solicitado Por:</td>
		 <td style="WIDTH:150px">
          <asp:textbox id="txtSolicitante" runat="server" Font-Size="10px" width="100px" MaxLength="10"></asp:textbox>
          &nbsp;<input style="width:20px; height:20px" id="btnSolicitante" class="boton" onclick="javascript:fnc_BuscarTrabajadores();" value="..." type="button" name="btnSolicitante" />
         </td>
		 <td style="WIDTH:530px" valign="middle" align="left" colspan="3">
          <asp:Label id="lblDesSolicitante" Runat="server" Font-Size="9px" width="300px"></asp:Label>
         </td>
		</tr>

        <tr>
		 <td style="width:120px" class="etiqueta">Num. Maquina:</td>
		 <td style="width:150px">
          <asp:DropDownList ID="ddlNumMaquina" runat="server" Width="120px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                        <asp:ListItem Value="L8">006478 - L8</asp:ListItem>
                        <asp:ListItem Value="L10">006760 - L10</asp:ListItem>
                        <asp:ListItem Value="L11">006968 - L11</asp:ListItem>
                        <asp:ListItem Value="L12">006990 - L12</asp:ListItem>
                        <asp:ListItem Value="L13">007322 - L13</asp:ListItem>
                    </asp:DropDownList>
		 <td style="width:120px"></td>
         <td style="width:150px"></td>
         <td style="width:260px"></td>
        </tr>

        <tr>
		 <td style="WIDTH: 800px" colspan="5">
          <asp:label id="lblMsg" runat="server" Width="800px" CssClass="error"></asp:label>
         </td>
		</tr>
		
       </table>
       
       <table style="WIDTH:1000px" border="0" cellspacing="0" cellpadding="0">
        <tr>
		  <td style="WIDTH:1000px" align="right">
           <%--<asp:label id="lblItems" runat="server" Width="800px"></asp:label>--%>
          </td>
		</tr>
		<tr>
		 <td style="WIDTH:800px">
          <asp:datagrid id="dgListaCheckList" runat="server" Width="1000px" AutoGenerateColumns="False">
		   <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		   <ItemStyle CssClass="GridItem"></ItemStyle>
		   <HeaderStyle CssClass="GridHeader"></HeaderStyle>
		   <Columns>

           <asp:TemplateColumn>
             <HeaderStyle HorizontalAlign="center" Width="20px"></HeaderStyle>
			 <ItemTemplate>
			  <asp:ImageButton id="ibtEditar" runat="server" 
                     ImageUrl="../../intranet/Imagenes/buscar.gif" CommandName="Editar" 
                     CommandArgument='<%# DataBinder.Eval(Container.DataItem,"INT_ID_CHECKLIST")%>'></asp:ImageButton>
             </ItemTemplate>
			</asp:TemplateColumn>
			
			
            <asp:BoundColumn DataField="VCH_NUM_MAQUINA" HeaderText="N° Maquina">
			 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="60px"></HeaderStyle>
			 <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
			</asp:BoundColumn>

			<asp:BoundColumn DataField="FECHA_CHK" HeaderText="Fec. CheckList">
			 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="70px"></HeaderStyle>
			 <ItemStyle Font-Size="8px" HorizontalAlign="Center"></ItemStyle>
			</asp:BoundColumn>

			<asp:BoundColumn DataField="TURNO" HeaderText="Turno">
			 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="25px"></HeaderStyle>
			 <ItemStyle Font-Size="8px" HorizontalAlign="Center"></ItemStyle>
			</asp:BoundColumn>

			<asp:BoundColumn DataField="VCH_COD_EMPL" HeaderText="Solicitante">
			 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="190px"></HeaderStyle>
			 <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
			</asp:BoundColumn>

			<asp:BoundColumn DataField="VCH_HONOMETRO_E" HeaderText="Horometro">
			 <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="70px"></HeaderStyle>
			 <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
			</asp:BoundColumn>
            
		  </Columns>
		 </asp:datagrid>
        </td>
	   </tr>
	  </table>
	</form>
  </body>
</html>