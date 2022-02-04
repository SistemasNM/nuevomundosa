<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_AprobacionPedidos.aspx.vb" Inherits="intranet_logi.frm_AprobacionPedidos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
  <title>Aprobacion de Pedidos</title>
  <base target="_self"/>
  <meta name="vs_showGrid" content="True"/>
  <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
  <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
  <meta name="vs_defaultClientScript" content="JavaScript"/>
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
  <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
  <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
  <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>

  <script language="javascript" type="text/javascript">
      returnValue = "";
      function Aprobar(Valor) {
          returnValue = Valor;
          window.close();
      }
      function EditarItemPedido(CodItem, Cantidad, DesItem, UniItem, PreItem) {
          document.all('lblCodArticulo').value = CodItem;
          document.all('txtCantidad').value = parseFloat(Cantidad).toFixed(3);
          document.all('txtCanX').value = Cantidad;
          document.all('lblPrecioArticulo').value = PreItem;
          document.all('lblDesArticulo').value = DesItem;
          document.all('lblUniMedida').value = UniItem;
          document.all('txtCantidad').focus();
          //REQSIS201900029 - DG - INI
          frmAprobacionPedidos.hdnFlg.value = "0";
          //REQSIS201900029 - DG - FIN
      }

      function fnc_Cerrar() {
          window.open('', '_parent', '');
          window.close();
      }

      // Funcion Confirma Accion
      function Confirmacion(stMensaje) {
          var strAcepto
          var Mensaje = stMensaje;
          if (confirm(Mensaje) == true) {
              strAcepto = "1";
              document.all('txtAcepta').value = strAcepto;
          }
          else {
              strAcepto = "0";
              document.all('txtAcepta').value = strAcepto;
          }
          //REQSIS201900029 - DG - INI
          frmAprobacionPedidos.hdnFlg.value = "0";
          //REQSIS201900029 - DG - FIN
      }

      function btnSeguimiento_Onclick() {
          if (document.all('lblNumeroPedido').value == '') {
              return false;
          }
          var z = document.all('lblNumeroPedido').value;
          var intCodigoPedido = parseInt(z.substring(1, 10));
          var retorno = window.showModalDialog("frm_ListarSeguimientoPedidos.aspx?intCodigoPedido=" + intCodigoPedido, "", "dialogheight:300px;dialogwidth:800px;center:yes;help:no;status=no");
          //REQSIS201900029 - DG - INI
          frmAprobacionPedidos.hdnFlg.value = "0";
          //REQSIS201900029 - DG - FIN
          return false;
      }
      //REQSIS201900029 - DG - INI
      function btnEditarFecha_Onclick() {
          if (document.all('lblNumeroPedido').value == '') {
              return false;
          }
          var intCodigoPedido = document.all('lblNumeroPedido').value;
          var retorno = window.showModalDialog("frm_ModificarFechaInstalacion.aspx?intCodigoPedido=" + intCodigoPedido + "&intTipo=" + 1, "", "dialogheight:500px;dialogwidth:800px;center:yes;help:no;status=no");
          frmAprobacionPedidos.hdnFlg.value = "1";
      }
      function fnc_ConfirmarOperacion() {
          if (confirm("Esta seguro de reiniciar el proceso del vale. \n Volvera a instancia del creador del vale.") == true) {
              return true;
          } else {
              return false; 
            }
      }
      //REQSIS201900029 - DG - FIN
	</script>
  </head>
  <body>
   <form id="frmAprobacionPedidos" method="post" autocomplete="off" runat="server">
   <center>
      <!-- Cabecera de Pagina-->
	  <table style="width:800px; height: 20px" border="0" cellspacing="0" cellpadding="0">
		<tr>
		  <td style="width: 700px" valign="middle" align="right">Usuario:</td>
		  <td style="width: 100px" valign="middle" align="left"><asp:label id="lblUsuario" text="USUARIO" Runat="server" Font-Bold="True">USUARIO</asp:label></td>
		</tr>
	  </table>

	  <!-- Titulo -->
	  <table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
	   <tr>
		<td style="width:800px;height:20px" class="Cabecera" valign="middle" align="center">APROBACION DE VALE DE ALMACEN</td>
	   </tr>
	  </table> 
      
      <table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
	   <tr>
		<td style="width:400px">
         <!-- Articulo  -->    
         <table style="width: 400px" border="0" cellspacing="0" cellpadding="0">
          <tr>
		   <td style="width:150px" class="etiqueta">Situacion:</td>
		   <td style="width:250px"><asp:label id="lblEstado" text="" runat="server" Font-Bold="True" Font-Size="9px" width="250px"></asp:label></td>
		  </tr>
		  <tr>
		   <td style="width:150px" class="etiqueta">Numero de Pedido:</td>
		   <td style="width:250px"><asp:label id="lblNumPedido" text="" runat="server" Font-Size="9px" width="250px"></asp:label></td>
		  </tr>
		  <tr>
		   <td style="width:150px" class="etiqueta">Solicitante:</td>
		   <td style="width:250px"><asp:label id="lblSolicitante" text="" Runat="server" Font-Size="9px" width="250px"></asp:label></td>
		  </tr>
		  <tr>
		   <td style="width:150px" class="etiqueta">Centro de Costos:</td>
		   <td style="width:250px"><asp:label id="lblCentroCostos" text="" runat="server" Font-Size="9px" width="250px"></asp:label></td>
		  </tr>
		  <tr>
		   <td style="width:250px" class="etiqueta">Almacen Origen:</td>
		   <td style="width:250px"><asp:label id="lblAlmacen" text="" runat="server" Font-Size="9px" width="250px"></asp:label></td>
		  </tr>
		  <tr>
		   <td style="width:150px" class="etiqueta">Fecha de Pedido:</td>
		   <td style="width:250px"><asp:label id="lblFechaPedido" text="" runat="server" Font-Size="9px" width="250px"></asp:label></td>
		  </tr>
		  <tr>
		   <td style="width:150px" class="etiqueta">Prioridad</td>
		   <td style="width:250px">
            <asp:textbox id="txtPrioridad" runat="server" width="80px" CssClass="inputDisabled" Enabled="False" Font-Size="10px"></asp:textbox>
            &nbsp;Fec.Instal&nbsp;
            <asp:textbox id="txtFecInstal" runat="server" width="80px" CssClass="inputDisabled" Enabled="False" Font-Size="10px"></asp:textbox>
            <asp:ImageButton id="btnEditarFecInstal" runat="server" ImageUrl="../../intranet/Imagenes/editar.gif"  Width="16px" Height="16px"></asp:ImageButton>
           </td>
		  </tr>
          </table>
          </td>
         
        <td>
         <%--Seguimiento--%>
          <table style="width: 400px" border="0" cellspacing="0" cellpadding="0">
           <tr>
            <td style="width:400px">
             <asp:datagrid id="dgSeguimiento" runat="server" width="390px" AutoGenerateColumns="False">
		      <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
		      <ItemStyle CssClass="GridItem"></ItemStyle>
		      <HeaderStyle CssClass="GridHeader"></HeaderStyle>
		       <Columns>
		   
               <asp:BoundColumn DataField="nu_paso" HeaderText="Sec.">
			    <HeaderStyle HorizontalAlign="Center" width="50px" VerticalAlign="Middle"></HeaderStyle>
                <ItemStyle Font-Size="8px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
		       </asp:BoundColumn>
		
               <asp:BoundColumn DataField="st_apro" HeaderText="Estado">
		        <HeaderStyle HorizontalAlign="Center" width="50px" VerticalAlign="Middle"></HeaderStyle>
                <ItemStyle Font-Size="8px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
		       </asp:BoundColumn>

		       <asp:BoundColumn DataField="no_usua" HeaderText="Usuario">
			    <HeaderStyle HorizontalAlign="Center" width="200px" VerticalAlign="Middle"></HeaderStyle>
                <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
		       </asp:BoundColumn>

		       <asp:BoundColumn DataField="fe_apro" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy hh:mm}">
			    <HeaderStyle HorizontalAlign="Center" width="100px" VerticalAlign="Middle"></HeaderStyle>
                <ItemStyle Font-Size="8px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
		       </asp:BoundColumn>
		      </Columns>
		     </asp:datagrid>
            </td>
	       </tr>
	      </table> 
         </td>
       </tr>
      </table>
          
      <table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
	   <tr>
		<td style="width:150px" class="etiqueta">Observacion:</td>
		<td style="width:650px"><asp:label id="lblObservaciones" text="" Runat="server" Font-Size="9px" width="650px"></asp:label></td>
	   </tr>
       <tr>
        <td style="width:150px" class="etiqueta">Activo/CTC:</td>
        <td style="width:650px"><asp:label id="lblActivoCTC" text="" Runat="server" Font-Size="9px" width="650px"></asp:label></td>
       </tr>
       <tr>
        <td style="width:150px"></td>
        <td style="width:650px">
         <asp:textbox id="txtCanX" borderStyle="None" runat="server" Font-Size="8px" Height="15px" width="15px" CssClass="etiqueta" ForeColor="#BCCAE0" BackColor="#CDE0EF"></asp:textbox>
         <asp:textbox id="txtAcepta" borderStyle="None" runat="server" Font-Size="8px" Height="15px" width="15px" CssClass="etiqueta" ForeColor="#BCCAE0" BackColor="#CDE0EF"></asp:textbox>
         <asp:textbox id="txtSituacion" borderStyle="None" runat="server" Font-Size="8px" Height="15px" width="15px" CssClass="etiqueta" ForeColor="#BCCAE0" BackColor="#CDE0EF"></asp:textbox>
         <asp:textbox id="lblNumeroPedido" borderStyle="None" runat="server" Font-Size="9pt" Height="15px" Width="15px" CssClass="etiqueta" ForeColor="#CDE0EF" BackColor="#CDE0EF"></asp:textbox>
         <asp:textbox id="txtCtaGasto" borderStyle="None" runat="server" Font-Size="9pt" Height="15px" Width="15px" CssClass="etiqueta" ForeColor="#CDE0EF" BackColor="#CDE0EF"></asp:textbox>
         <asp:textbox id="txtOrden" borderStyle="None" runat="server" Font-Size="9pt" Height="15px" Width="15px" CssClass="etiqueta" ForeColor="#CDE0EF" BackColor="#CDE0EF"></asp:textbox>
         <asp:textbox id="txtSecuencia" borderStyle="None" runat="server" Font-Size="9pt" Height="15px" Width="15px" CssClass="etiqueta" ForeColor="#CDE0EF" BackColor="#CDE0EF"></asp:textbox>
        </td>
       </tr>
	  </table>

      <%--edicion--%>
	  <asp:panel id="pnlArticulo" runat="server" width="800px">
	   <table style="width:800px" border="0" cellspacing="0" cellpadding="0">
		<tr>
		 <td class="etiqueta" style="width:100px">Codigo</td>
		 <td class="etiqueta" style="width:50px">U.M.</td>
		 <td class="etiqueta" style="width:100px">Precio</td>
		 <td class="etiqueta" style="width:400px">Descripcion</td>
		 <td class="etiqueta" style="width:100px">Cantidad</td>
		 <td class="etiqueta" style="width:50px">
		  <asp:imagebutton id="btnActualizar" runat="server" Visible="False" ImageUrl="../../intranet/Imagenes/save_16.png"
				ToolTip="Guardar Cambios en Registro"></asp:imagebutton></td>
		</tr>
		<tr>
		 <td class="etiqueta" style="width:100px">
		  <asp:textbox id="lblCodArticulo" BorderStyle="None" runat="server" width="100px" Font-Size="9px" CssClass="etiqueta"></asp:textbox>
         </td>
		 <td class="etiqueta" style="width:50px">
		  <asp:textbox id="lblUniMedida" BorderStyle="None" runat="server" width="50px" Font-Size="9px" CssClass="etiqueta"></asp:textbox>
         </td>
		 <td class="etiqueta" style="width:100px">
		  <asp:textbox id="lblPrecioArticulo" BorderStyle="None" runat="server"	Font-Size="9px" CssClass="etiqueta" width="100px"></asp:textbox>
         </td>
		 <td class="etiqueta" style="width:400px">
		  <asp:textbox id="lblDesArticulo" BorderStyle="None" runat="server" width="400px" Font-Size="9px" CssClass="etiqueta"></asp:textbox>
         </td>
		 <td class="etiqueta" style="width:100px">
		  <asp:textbox id="txtCantidad" runat="server" Font-Bold="True" width="100px" Font-Size="10px" Text="0.00"></asp:textbox></td>
		 <td class="etiqueta" style="width: 103px; height: 22px" width="103"></td>
		</tr>
	   </table>
	  </asp:panel>

	  <table style="width:800px" border="0" cellspacing="0" cellpadding="0">
	   <tr>
		<td style="width:800px" valign="middle" align="left"><asp:label id="lblMsg" text="" runat="server" Font-Bold="True" width="800px" CssClass="error"></asp:label></td>
	   </tr>
	   <tr>
		<td style="width:800px" valign="middle" align="right"><asp:label id="lblItems" text="Numero de Items:" Runat="server" width="800px"></asp:label>
        </td>
	   </tr>
       <tr>
        <td style="width:800px" valign="middle" align="right">
         <asp:label id="lblTextoTotal" text="Total (S/.):" runat="server"></asp:label>
         <asp:label id="lblTotalPedido" runat="server"></asp:label>
        </td>
       </tr>
       <!-- Grilla -->
	   <tr> 
		<td style="width:800px" align="center">
         <asp:datagrid id="dgDetallePedido" runat="server" AutoGenerateColumns="False" 
                width="800px" >
		  <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		  <ItemStyle CssClass="GridItem"></ItemStyle>
		  <HeaderStyle CssClass="GridHeader"></HeaderStyle>
		  <Columns>
		   
           <asp:TemplateColumn HeaderText="Sec.">
			<HeaderStyle HorizontalAlign="Center" width="30px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate><asp:Label id="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_SECU") %>' Width="30px"></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>

		   <asp:TemplateColumn HeaderText="Codigo">
			<HeaderStyle HorizontalAlign="Center" width="100px"></HeaderStyle>
			<ItemStyle HorizontalAlign="center" Font-Size="8px"></ItemStyle>
			<ItemTemplate>
             <asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>' Width="100px"></asp:Label>
			 <asp:Label id="lblCtagasto" runat="server" Visible ="false" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
			 <asp:Label id="lblActivoFijo" runat="server" Visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>
		    </ItemTemplate>
		   </asp:TemplateColumn>

		   <asp:TemplateColumn HeaderText="Descripcion">
		    <HeaderStyle HorizontalAlign="Center" width="200px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
			<ItemTemplate><asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>' Width="200px"></asp:Label></ItemTemplate>
           </asp:TemplateColumn>
		   
           <asp:TemplateColumn HeaderText="Cta Gasto">
			<HeaderStyle HorizontalAlign="Center" width="200px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
			<ItemTemplate><asp:Label id="lblDesCtaGasto" runat="server" Visible ="true" Text='<%# DataBinder.Eval(Container, "DataItem.DesCtaGasto") %>' Width="200px"></asp:Label></ItemTemplate>
           </asp:TemplateColumn>

           <asp:TemplateColumn HeaderText="O/T">
			<HeaderStyle HorizontalAlign="Center"  VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate><asp:Label id="lblOrderTrabajo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrdenTrabajo") %>' ></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>


           <asp:TemplateColumn HeaderText="Cant. Stock">
			<HeaderStyle HorizontalAlign="Center" width="60px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate><asp:Label id="lblCantidadStock" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Stock") %>' Width="60px"></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>

		   <asp:TemplateColumn HeaderText="Cant. Solicitada">
			<HeaderStyle HorizontalAlign="Center" width="60px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate><asp:Label id="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEDI") %>' Width="60px"></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>

		   <asp:TemplateColumn HeaderText="U.M.">
			<HeaderStyle HorizontalAlign="Center" width="50px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="center"></ItemStyle>
			<ItemTemplate><asp:Label id="lblUniMed" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>' Width="50px"></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>

		   <asp:TemplateColumn HeaderText="Precio">
			<HeaderStyle HorizontalAlign="Center" width="60px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="right"></ItemStyle>
			<ItemTemplate><asp:Label id="lblPrecio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PE_ITEM") %>' Width="60px"></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>

		   <asp:BoundColumn DataField="SubTotal" HeaderText="Monto (S/.)" DataFormatString="{0:#,##0.00}">
			<HeaderStyle HorizontalAlign="Center" width="60px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
		   </asp:BoundColumn>

		   <asp:TemplateColumn HeaderText="Cant. Aprobada">
			<HeaderStyle HorizontalAlign="Center" width="60px" VerticalAlign="Middle"></HeaderStyle>
			<ItemStyle Font-Size="8px" HorizontalAlign="right"></ItemStyle>
			<ItemTemplate><asp:Label id="lblCantidadAprobada" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_APRO") %>' Width="60px"></asp:Label></ItemTemplate>
		   </asp:TemplateColumn>

			<asp:TemplateColumn>
			 <HeaderStyle width="20px" HorizontalAlign="Center"></HeaderStyle>
			 <ItemTemplate>
              <asp:ImageButton id="btnEditarItem" runat="server" ImageUrl="../../intranet/Imagenes/editar.gif" CommandName="Editar" Width="16px" Height="16px"></asp:ImageButton>
             </ItemTemplate>
			</asp:TemplateColumn>
			
            <asp:TemplateColumn>
			 <HeaderStyle width="20px" HorizontalAlign="Center"></HeaderStyle>
			 <ItemTemplate><asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/eliminar.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CO_ITEM")%>' Width="16px" Height="16px"></asp:ImageButton></ItemTemplate>
		    </asp:TemplateColumn>
		   
           </Columns>
		  </asp:datagrid>
         </td>
		</tr>
	  </table>

	  <table style="width:800px" border="0" cellspacing="0" cellpadding="0">
	   <tr>
		<td style="width:800px" align="right">
		<asp:button id="btnVerSeguimiento" Runat="server" width="100px" CssClass="boton" Visible="False" Text="Ver Seguimiento"></asp:button>
		<asp:button id="btnAprobar" Runat="server" CssClass="boton" Text="Aprobar" width="100px"></asp:button>
        <%--REQSIS201900029 - DG - INI--%>
        <asp:button id="btnReiniciar" Runat="server" CssClass="boton" Text="Reiniciar" width="100px"></asp:button>
        <%--REQSIS201900029 - DG - FIN--%>
		<asp:button id="btnAnular" Runat="server" CssClass="boton" Text="Anular"  width="100px"></asp:button>
		<input id="btnSalir" class="boton" onclick="javascript:fnc_Cerrar();" value="Salir" type="button" name="btnSalir" runat="server" /></td>
	   </tr>
	   <tr>
		<td style="width:800px">
         <asp:label id="lblError" text="" Runat="server" Font-Bold="True" width="800px" ForeColor="Red"></asp:label></td>
	   </tr>
	  </table>
      <table>
        <tr>
		    <td>
                <input id="hdnFlg" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnFlg" runat="server" /> 
            </td>
        </tr>
      </table>
     </center>
	</form>
  </body>
</html>
