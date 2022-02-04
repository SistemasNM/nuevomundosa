<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistroDespachoPedidosDesperdicios.aspx.vb" Inherits="intranet_logi.frm_RegistroDespachoPedidosDesperdicios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <base target="_self"/>
  <title>Despacho de pedido - Hilos</title>
 	 <%--<link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>--%>
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
   <%--<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>--%>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
   <%--<link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>--%>
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
   <link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet"/>
   <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>

  <script language="javascript" type="text/javascript">
      // funciones
      function VerValesPedido(strNumeroPedido) {
          var retorno = window.showModalDialog("frm_ListadoValesPedido.aspx?&strNumeroPedido=" + strNumeroPedido, "", "dialogheight:400px;dialogwidth:750px;center:yes;help:no;");
      }
      function fMostrarReporte(strUrl) {
          var intwidth = screen.width;
          var intheight = screen.height;
          window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
      }

      function fnc_Cerrar() {
          window.open('', '_parent', '');
          window.close();
      }
   
		</script>
</head>
<body>
 <form id="frm_DespachoPedidoHilo" runat="server" autocomplete="off">
  <center>
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    	
    <%--titulo--%>
		<table id="tblCabeceraPagina" border="0" cellspacing="0" cellpadding="0" width="800px">
		 <tr>
			<td style="width:800px; height:30px" class="Cabecera">DESPACHO DE PEDIDO DE DESPERDICIOS               
		 </tr>
		</table>
    
    <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
     <ContentTemplate>
      <%--datos--%>
			<table style="width:800px" border="0" cellspacing="0" cellpadding="0" class="Panel">
			 <tr>
			  <td style="width:120px" class="Descripcion">&nbsp;Situacion:</td>
				<td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:label id="txtEstado" runat="server" Font-Bold="True" width="200px" Font-Size="8px" CssClass="txtDeshabilitado"></asp:label>
        </td>
       </tr>
       <tr>
        <td class="Descripcion" style="width:120px">&nbsp;Solicitante:</td>
        <td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:Label ID="txtCodSolicitante" runat="server" Font-Size="8px" width="30px" CssClass="txtDeshabilitado"></asp:Label>
         <asp:Label ID="txtDesSolicitante" runat="server" Font-Size="8px" width="166px" CssClass="txtDeshabilitado"></asp:Label>
        </td>
       </tr>
       <tr>
        <td class="Descripcion" style="width:120px"> &nbsp;Area:</td>
        <td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:Label ID="txtDesArea" runat="server" Font-Size="8px" width="200px" CssClass="txtDeshabilitado"></asp:Label>
        </td>
       </tr>
       <tr>
        <td class="Descripcion" style="width:120px">&nbsp;Seccion:</td>
        <td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:Label ID="txtDesSeccion" runat="server" Font-Size="8px" width="200px" CssClass="txtDeshabilitado"></asp:Label>
        </td>
       </tr>
       <tr>
        <td class="Descripcion" style="width:120px">&nbsp;Cargo:</td>
        <td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:Label ID="txtDesCargo" runat="server" Font-Size="8px" width="200px" CssClass="txtDeshabilitado"></asp:Label>
        </td>
       </tr>
       <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Num. Pedido:</td>
				<td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:label id="txtNumeroPedido" runat="server" width="200px" Font-Size="8px" CssClass="txtDeshabilitado"></asp:label>
        </td>
			 </tr>
			 <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Centro Costos:</td>
				<td style="width:680px" valign="middle" valign="middle" align="left">
         &nbsp;<asp:label id="txtDesCentroCostos" runat="server" width="200px" Font-Size="8px" CssClass="txtDeshabilitado"></asp:label>
        </td>
			 </tr>
			 <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Almacen:</td>
				<td style="width:680px" valign="middle" valign="middle" align="left">
         &nbsp;<asp:label id="txtDesAlmacen" runat="server" width="200px" Font-Size="8px" CssClass="txtDeshabilitado"></asp:label>
        </td>
			 </tr>
			 <tr>
				<td style="width:120px;" class="Descripcion">&nbsp;Fec Pedido:</td>
				<td style="width:680px;" valign="middle" valign="middle" align="left">
         &nbsp;<asp:label id="txtFechaPedido" runat="server" width="200px" Font-Size="8px" CssClass="txtDeshabilitado"></asp:label>
        </td>
			 </tr>
			 <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Fec. Aprobacion:</td>
				<td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:label id="txtFechaAprobacion" runat="server" width="200px" Font-Size="8px" CssClass="txtDeshabilitado"></asp:label>
        </td>
			 </tr>
       <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Fec. Atencion:</td>
				<td style="width:680px" valign="middle" align="left">
				 <%--&nbsp;<asp:textbox id="txtFechaDespacho" runat="server" width="80px" CssClass="txtDeshabilitado2"></asp:textbox>--%>
         &nbsp;<asp:Label ID="lblFechaDespacho" runat="server" width="200px" Font-Size="8px" Text="" CssClass="txtDeshabilitado"></asp:Label>
				</td>
				</tr>
			 <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Entregado a:</td>
				<td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:label id="lblDesRecepciona" runat="server" Font-Size="8px" Width="200px" CssClass="txtDeshabilitado"></asp:label>
        </td>
			 </tr>
			 <tr>
				<td style="width:120px" class="Descripcion">&nbsp;Obervaciones:</td>
				<td style="width:680px" valign="middle" align="left">
         &nbsp;<asp:textbox id="txtObservaciones" runat="server" width="500px" Height="30px" CssClass="txtAreaDeshabilitado"	TextMode="MultiLine"></asp:textbox></td>
			 </tr>
       <tr>
			  <td style="width:800px" colspan="2" align="left">
         <asp:label id="lblError" text="" runat="server" CssClass="mensaje"></asp:label>
        </td>
			 </tr>
		  </table>

      <%--items--%>
      <table style="width:800px">
       <tr>
				<td style="width:800px" align="right">
         <asp:label id="lblItems" text="Numero de Items" runat="server" CssClass="contador">Numero de Items:</asp:label>
        </td>
			 </tr>
			</table>
      
      <%--grilla--%>
			<table style="width:800px" border="0" cellspacing="0" cellpadding="0">
			 <tr>
				<td style="width:800px" class="Descripcion" align="left">
         <asp:datagrid id="dgDetalle" runat="server" width="800px" AutoGenerateColumns="False">
					<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
					<ItemStyle CssClass="GridItem"></ItemStyle>
					<HeaderStyle CssClass="GridHeader"></HeaderStyle>
					<Columns>
           
           <asp:BoundColumn DataField="nu_secu" HeaderText="Sec.">
						<HeaderStyle HorizontalAlign="Center" width="20px" VerticalAlign="Middle"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="Center" Width="20px"></ItemStyle>
					 </asp:BoundColumn>
					 
           <asp:TemplateColumn HeaderText="Codigo">
						<HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="left" Width="100px"></ItemStyle>
						<ItemTemplate>
						 <asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
						 <asp:Label id="lblCtagasto" runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
						 <asp:Label id="lblActivoFijo" runat="server" Visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>
						</ItemTemplate>
					 </asp:TemplateColumn>

					 <asp:TemplateColumn HeaderText="Descripcion">
						<HeaderStyle HorizontalAlign="Center" width="300px" VerticalAlign="Middle"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="left" Width="300px"></ItemStyle>
						<ItemTemplate>
						 <asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'></asp:Label>
						</ItemTemplate>
					 </asp:TemplateColumn>

					 <asp:TemplateColumn HeaderText="Can. Aprob.">
						<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="right" width="50px"></ItemStyle>
						<ItemTemplate>
						 <asp:Label id="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_APRO") %>'></asp:Label>
						</ItemTemplate>
					 </asp:TemplateColumn>

					 <asp:TemplateColumn HeaderText="Can. Aten.">
						<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="right" width="50px"></ItemStyle>
						<ItemTemplate>
						 <asp:Label id="lblCantidadAtendida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_ATEN") %>'></asp:Label>
						</ItemTemplate>
					 </asp:TemplateColumn>

					 <asp:TemplateColumn HeaderText="Can. Pend.">
						<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
						<ItemTemplate>
						 <asp:Label id="lblCantidadPendiente" runat="server" Visible = "true" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEND") %>'></asp:Label>
						</ItemTemplate>
					 </asp:TemplateColumn>

					 <asp:TemplateColumn HeaderText="Stock Kgs.">
						<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
						<ItemTemplate>
						 <asp:Label id="lblStockCon" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Stock") %>'></asp:Label>
						</ItemTemplate>
					 </asp:TemplateColumn>


           <asp:TemplateColumn HeaderText="Kgs.">
						<HeaderStyle width="50px" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
						<ItemTemplate>
						 <asp:TextBox ID="txtDespachableKgs" runat="server" width="50px" Font-Bold="true" Font-Size="8px" CssClass="inputDerecha" Text='<%# DataBinder.Eval(Container, "DataItem.CA_DESP") %>'></asp:TextBox>
						</ItemTemplate>
					 </asp:TemplateColumn>
					</Columns>
				 </asp:datagrid>
        </td>
			 </tr>
       <%--botonera--%>
       <tr>
				<td style="width:800px" align="right">
         &nbsp;<asp:button id="btnVales" runat="server" width="120px" toolTip="Ver vales generados" Text="Vales generados" CssClass="btnAzul"></asp:button>
         &nbsp;<asp:button id="btnVerVale" runat="server" width="120px" toolTip="Vista previa de pedido" Text="Vista previa vale" CssClass="btnAzul"></asp:button>
				 &nbsp;<asp:button id="btnCulminar" runat="server" width="120px" tooltip="Culminar pedido" Text="Culminar vale" CssClass="btnAzul"></asp:button>
				 &nbsp;<asp:button id="btnDespachar" runat="server" width="120px" toolTip="Despachar Pedido" Text="Despachar vale" CssClass="btnVerde"></asp:button>
         &nbsp;<input id="btnSalir" onclick="javascript:fnc_Cerrar();" value="Salir" type="button"	name="btnSalir" runat="server" size="120px" class="btnAzul"/>
        </td>
       </tr>
			</table>
     </ContentTemplate>
  
     <Triggers>
      <asp:AsyncPostBackTrigger ControlID ="btnDespachar" EventName="click" />
      <asp:AsyncPostBackTrigger ControlID ="btnVales" EventName="click" />
     </Triggers>
    </asp:UpdatePanel>
   </center>
	</form>
</body>
</html>
