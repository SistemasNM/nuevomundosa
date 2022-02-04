<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_RegistroPedido.aspx.vb" Inherits="intranet_logi.frm_RegistroPedido"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 transitional//EN">
<html>
 <head id="hdpagina">
 <title>Nuevo Mundo - Vale de Salida</title>
 <meta content="False" name="vs_snapToGrid"/>
 <meta content="true" name="vs_showGrid"/>
 <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
 <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
 <meta content="JavaScript" name="vs_defaultClientScript"/>
 <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
 <script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>
 <link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet"/>
 
 <script language="javascript" type="text/javascript">
    //funcion valida numero
     function ValidaNumero() {
         var tecla;
         tecla = document.all ? tecla = e.KeyCode : tecla = e.which;
         return ((tecla > 47 && tecla < 58) || tecla == 46);
     }

     //funcion Seguimiento de Aprobacion
     function btnSeguimiento_Onclick() {
         if (document.all('txtNumeroPedido').value == '') {
             return false;
         }
         var z = document.all('txtNumeroPedido').value;
         var intCodigoPedido = parseInt(z);
         alert(intCodigoPedido);
         var retorno = window.showModalDialog("frm_ListarSeguimientoPedidos.aspx?intCodigoPedido=" + intCodigoPedido, "", "dialogHeight:300px;dialogWidth:800px;center:yes;help:no;status=no");
         return false;
     }

     // funcion Solicitar Aprobacion
     function SolicitarAprobacion() {
         var retorno = window.showModalDialog("../../intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             document.all('txtAcepta').value = Codigo;
         }
     }

     //funcion Buscar Pedido
     function VerConsultaPedido() {
         var retorno = window.showModalDialog("frm_ConsultaPedido.aspx", "", "dialogHeight:600px;dialogWidth:800px;center:yes;help:no;");
         if (retorno == "") {
             return false;
         }
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var NumPedido = datos[0];
             document.all('txtCodigo').value = NumPedido
             return true;
         }
     }


     // funcion Cerrar
     function fnc_Cerrar() {
         window.open('', '_parent', '');
         window.close();
     }

     // Funcion Editar Articulo

     function EditarItemPedido(CodItem, Cantidad, DesItem, UniItem, PreItem) {
         document.all('txtCodArticulo').value = CodItem;
         document.all('txtCantidad').value = parseFloat(Cantidad).toFixed(3);
         document.all('txtCanX').value = parseFloat(Cantidad).toFixed(3);
         document.all('lblPrecioArticulo').value = parseFloat(PreItem).toFixed(3);
         document.all('lblDesArticulo').value = DesItem;
         document.all('lblUniMedida').value = UniItem;
         document.all('txtCantidad').focus();
     }

     // Funcion Confirma Accion

     function Confirmacion(stMensaje) {
         var Mensaje = stMensaje;
         if (confirm(Mensaje) == true) {
             var strAcepto = "1";
             document.all('txtAcepta').value = strAcepto;
         }
         else {
             var strAcepto = "0";
             document.all('txtAcepta').value = strAcepto;
         }
     }

     // Funcion Busca Centro de Costos

     function fnc_BuscarCentroCosto() {
         var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCodCentroCostos').value = Codigo;
             document.all('lblDesCentroCostos').value = Nombre;
         }
     }

     // Funcion Busca Cuenta de Gastos
     function fnc_BuscarCtaGasto() {
         var retorno = window.showModalDialog("../../intranet/Buscadores/frmCtaGasto.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCodCuentaGastos').value = Codigo;
             document.all('lblDesCuentaGastos').value = Nombre;
         }
     }

     // Funcion Busca Empleados

     function fnc_Buscartrabajadores() {
         var tipo = "EMP";
         var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var codigo = datos[0];
             var nombre = datos[1];
             document.all('txtCodSolicitante').value = codigo;
             document.all('lblDesSolicitante').value = nombre;
         }
     }

     // Funcion Busca Articulos

     function fnc_BuscarArticulos() {
         var x = document.all('cboAlmacen').value;
         if (x != "") {
             var lstrAlmacen = x.substring(0, 3);
             var lpstrTipo = 9;
             var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen=" + lstrAlmacen + "&pstrTipo=" + lpstrTipo, "", "dialogHeight:500px;dialogWidth:800px;center:yes;help:no;");
             if (retorno != "" && retorno != ":") {
                 var datos = retorno.split(":");
                 var Codigo = datos[0];
                 var Nombre = datos[1];
                 var Unidad = datos[2];
                 var Stock = datos[3];
                 document.all('txtCodArticulo').value = Codigo;
                 document.all('lblDesArticulo').value = "Desc: " + Nombre;
                 document.all('lblUniMedida').value = "U.M.: " + Unidad;
                 document.all('lblStockArticulo').value = "Stock: " + parseFloat(Stock).toFixed(3);
             }
         }

         else {

             alert("Debe elegir un almacen para oonsultar Articulos");
             document.all('cboAlmacen').focus();
         }
     }

     // Para Validar el ingreso de datos obligatorios

     function fnc_GrabarDatos() {
         var lintVerificar = 0;
         lintVerificar = fnc_VerificarDatos()
         if (lintVerificar == 0) {
             return false;
         } else {
             return true;
         }
     }

     // Verifica si todos los datos estan bien ingresados

     function fnc_VerificarDatos() {
         // Solicitante
         if (document.all('txtCodSolicitante').value == "") {
             alert("Debe ingresar el solicitante para el vale de almacen.");
             document.all('txtSolicitante').focus();
             return 0
         }
         // Centro de Costos
         if (document.all('txtCodCentroCostos').value == "") {
             alert("Debe ingresar el centro de costo para el vale de almacen.");
             document.all('txtCodCentroCostos').focus();
             return 0
         } 
         // Almacen
         if (document.all('cboAlmacen').value == "") {
             alert("Debe elegir el almacen origen para el vale de almacen");
             document.all('cboAlmacen').focus();
             return 0
         }
         // Fecha de instalacion
         if (document.all('txtFecInstal').value == "") {
             alert("Debe ingresar una fecha de instalacion para el pedido");
             document.all('txtFecInstal').focus();
             return 0
         }
         // Item
         if (document.all('txtCodArticulo').value == "") {
             alert("Debe ingresar el articulo a solicitar.");
             document.all('txtCodArticulo').focus();
             return 0
         } 
         // Cuenta de Gastos
         var varActivo = document.all('txtOrdenServicio').value;
         var strActivo = parseInt(varActivo.substring(1, 1));
         if (document.all('cboCuentaGastos').value == "" && document.all('txtDesCuentaGasto').value == "" && strActivo.value != "9" && document.all('rdbVale').checked == true) {
             alert("Debe elegir cuenta de gasto para el vale de almacen.");
             document.all('cboCuentaGastos').focus();
             return 0
         } 
         //cantidad
         if (document.all('txtCantidad').value == "" || document.all('txtCantidad').value == 0) {
             alert("Debe ingresar la cantidad para el articulo.");
             document.all('txtCantidad').focus();
             return 0
         }
         return 1
     }

     //txtCantidad_onBlur
     function txtCantidad_onBlur(ControlID, Constante) {
         var cantidad
         cantidad = document.getElementById('txtCantidad').value
         if (cantidad == null || isNaN(cantidad)) {
             alert('La cantidad ingresada no es valida.');
             ltxtCantidad.focus();
             return false;
         }
         else {
             cantidad = parseFloat(txtCantidad.value).toFixed(2);
             txtCantidad.value = cantidad;
             return true;
         }
     }

     //Muestra el listado de los Activos fijos y las ordenes de trabajo
     function BuscarOrdenServicio() {
         var strCentroCostos = document.all('txtCodCentroCostos').value;
         if (strCentroCostos == "") {
             alert("Debe elegir un centro de costos");
             document.all('txtCodCentroCostos').focus();
         }
         else {
             var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaActivo.aspx?strCentroCostos=" + strCentroCostos, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
             if (retorno != "" && retorno != ":") {
                 var datos = retorno.split(":");
                 var Codigo = datos[0];
                 var Nombre = datos[1];
                 document.all('txtOrdenServicio').value = Codigo;
                 document.all('txtDesServicio').value = Nombre;
             }
         }
     }
	</script>
  </head>

  <body>
  <center>
	<form id="frmRegistraPedidos" method="post" autocomplete="off" runat="server">
	  <!-- Titulo -->
	  <table style="WIDTH: 800px; HEIGHT: 20px" cellspacing="0" cellpadding="0" border="1">
		<tr>
		  <td class="Cabecera" style="WIDTH: 800px; HEIGHT: 20px" align="left">Registro de Vale de Almacen</td>
		</tr>
	  </table>
	   
      <!-- Tipo de vale  -->
	  <table style="WIDTH: 800px">
	   <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left"></td>
		<td style="WIDTH: 700px; HEIGHT: 20px" align="left">
         &nbsp;<asp:radiobutton id="rdbVale" runat="server" BackColor="#CDE0EF" Text="Vale" Checked="true" AutoPostBack="true"></asp:radiobutton>
         &nbsp;<asp:radiobutton id="rdbCTC" runat="server" BackColor="#CDE0EF" Text="CTC" AutoPostBack="true"></asp:radiobutton>
        </td>
	   </tr>
      </table>

      <!-- Situacion  -->
      <table style="WIDTH: 800px">
	   <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Situacion:</td>
		<td style="WIDTH: 400px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtEstado" runat="server" CssClass="inputDisabled2" Height="20px" Font-Size="9px" width="120px"></asp:textbox>
        </td>
        <td style="WIDTH: 100px; HEIGHT: 20px" align="left">Fec. Pedido:</td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">Fec. Aprob.:</td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">Fec. Atenc.:</td>
	   </tr>
      </table>

      <!-- Numero de pedido  -->
      <table style="WIDTH: 800px">
	   <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Num. Pedido:</td>
		<td style="WIDTH: 400px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtSeriePedido" runat="server" CssClass="inputDisabled2" Height="20px" Font-Size="9px" Width="40px"></asp:textbox>
         <asp:textbox id="txtNumeroPedido" runat="server" CssClass="inputDisabled2" Height="20px" Font-Size="9px" Width="80px"></asp:textbox>
        </td>
        <td style="WIDTH: 100px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtFechaPedido" BorderStyle="None" runat="server" BackColor="#CDE0EF" Text="" Width="70px" Height="20px" Font-Size="9px"></asp:textbox>
        </td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtFechaAprobacion" BorderStyle="None" runat="server" BackColor="#CDE0EF" Text="" Width="70px" Height="20px" Font-Size="9px"></asp:textbox>
        </td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtFechaAtencion" BorderStyle="None" runat="server" BackColor="#CDE0EF" Text="" Width="70px" Height="20px" Font-Size="9px"></asp:textbox>
        </td>
	   </tr>
      </table>

      <!-- solictante  -->
      <table style="WIDTH: 800px">
	   <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Solicitante:</td>
		<td style="WIDTH: 400px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtCodSolicitante" runat="server" width="100px" Height="20px" Font-Size="9px" MaxLength="20"></asp:textbox>
         &nbsp;<input class="boton" id="btnSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_Buscartrabajadores();" type="button" value="..." name="btnSolicitante"/>
		 &nbsp;<asp:textbox id="lblDesSolicitante" BorderStyle="None" Runat="server" BackColor="#CDE0EF" Height="20px" Font-Size="9px" width="250px"></asp:textbox>
        </td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">Psto. Inic.(S/.):</td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">Psto. Util.(S/.):</td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">Psto. Disp.(S/.):</td>
	   </tr>
      </table>

      <!-- centro costos  -->
      <table style="WIDTH: 800px">
	   <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Centro Cost.:</td>
		<td style="WIDTH: 400px; HEIGHT: 20px" align="left">
         &nbsp;<asp:textbox id="txtCodCentroCostos" runat="server" Height="20px" Font-Size="9px" width="100px" MaxLength="20"></asp:textbox>
         &nbsp;<input class="boton" id="btnCentroCosto" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarCentroCosto();" type="button" value="..." name="btnCentroCosto"/>
         &nbsp;<asp:textbox id="lblDesCentroCostos" runat="server" BorderStyle="None" BackColor="#CDE0EF" Height="20px" Font-Size="9px" width="250px"></asp:textbox>
        </td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">
         <asp:label id="lblPstoInicial" Runat="server" text="" BackColor="#CDE0EF" Height="20px" Font-Size="9px" Width="70px"></asp:label>
        </td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">
         <asp:label id="lblPstoUtilizado" Runat="server" text="" BackColor="#CDE0EF" Height="20px" Font-Size="9px" Width="70px"></asp:label>
        </td>
		<td style="WIDTH: 100px; HEIGHT: 20px" align="left">
         <asp:label id="lblPstoDisponible" Runat="server" text="" BackColor="#CDE0EF" Height="20px" Font-Size="9px" Width="70px"></asp:label>
        </td>
	   </tr>
      </table>

      <!-- almacen/prioridad/fecha  -->
      <table style="WIDTH: 800px">
       <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Prioridad:</td>
		<td style="WIDTH: 700px; HEIGHT: 20px" align="left">
		 <asp:dropdownlist id="cboPrioridad" runat="server" CssClass="cbo" Font-Size="9px" Width="120px" Height="20px">
          <asp:ListItem Value="NOR">Normal</asp:ListItem>
		  <asp:ListItem Value="URG">Urgente</asp:ListItem>
         </asp:dropdownlist>
        </td>
       </tr>
       <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Fec. Instal.:</td>
		<td style="WIDTH: 700px; HEIGHT: 20px" align="left">
         <asp:textbox id="txtFecInstal" runat="server" CssClass="input" Width="100px"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frmRegistraPedidos.txtFecInstal, 'dd/mm/yyyy')" border="0"
							alt="Seleccionar fecha de instalacion" src="../../intranet/Imagenes/Calendario.gif" width="16" height="16"/>
		 
		</td>
	   </tr>
       <tr>
		<td class="etiqueta" style="WIDTH: 100px; HEIGHT: 20px" align="left">Alm. Origen:</td>
		<td style="WIDTH: 700px; HEIGHT: 20px" align="left">
         <asp:dropdownlist id="cboAlmacen" runat="server" CssClass="cbo" Height="20px" Font-Size="9px" Width="250px" Enabled="false"></asp:dropdownlist>
        </td>
       </tr>
	  </table>

       <!-- observacion  -->
       <table style="WIDTH: 800px">
		<tr>
		 <td class="etiqueta" style="WIDTH: 100px; HEIGHT: 40px" align="left">Observacion:</td>
		 <td style="WIDTH: 700px; HEIGHT: 40px">
          <asp:textbox id="txtObservaciones" runat="server" CssClass="input" Height="40px" font-Size="9px" Width="500px" MaxLength="200" TextMode="MultiLine"></asp:textbox>
          <p><font style="BACKGROUND-COLOR: #ffff66" face="Calibri" size="1"><em>*Maximo 200 caracteres</em></font></p>
         </td>
        </tr>
       </table>
       <table>
        <tr>
         <td style="WIDTH: 800px; HEIGHT: 20px" align="left">
          <asp:label id="lblContadorPalabras" Runat="server" text="" Font-Bold="true" Height="20px" Font-Size="9px"></asp:label>
         </td>
        </tr>
       </table>
       <table style="WIDTH: 800px">
        <tr>
		 <td style="WIDTH: 800px; HEIGHT: 20px">
          <asp:textbox id="txtAcepta" BorderStyle="None" runat="server" BackColor="#CDE0EF" Height="20px" Font-Size="8px" width="20px" ForeColor="#CDE0EF"></asp:textbox>
          <asp:textbox id="txtSituacion" BorderStyle="None" runat="server" BackColor="#CDE0EF" Height="20px" Font-Size="8px" width="20px" ForeColor="#CDE0EF"></asp:textbox>
          <asp:textbox id="txtCanX" BorderStyle="None" runat="server" BackColor="#CDE0EF" Height="20px" Font-Size="8px" width="20px" ForeColor="#CDE0EF">0.00</asp:textbox>
          <asp:textbox id="txtCodigo" BorderStyle="None" Runat="server" BackColor="#CDE0EF" Height="20px" Font-Size="8px" width="80px" ForeColor="#CDE0EF"></asp:textbox>
         </td>
		</tr>
		<tr>
		 <td style="WIDTH: 800px; HEIGHT: 20px">
          <asp:label id="lblError" Runat="server" text="" Font-Bold="true" Height="20px" Width="800px" ForeColor="Red"></asp:label>
         </td>
		</tr>
	   </table>

       <!-- Articulo  -->
	   <table style="WIDTH: 800px"> 
		<tr>
		 <td style="WIDTH: 800px" align="left">
          <asp:panel id="pnlArticulo" Runat="server" Width="800px">
		   <table style="WIDTH: 800px">
			<tr>
			 <td class="etiqueta" style="WIDTH: 150px; HEIGHT: 20px">Articulo:</td>
			 <td class="etiqueta" style="WIDTH: 250px; HEIGHT: 20px" align="left">Cuenta Gastos:&nbsp;
			  <asp:textbox id="txtDesCuentaGasto" BorderStyle="None" runat="server" CssClass="etiqueta" width="100px" Font-Size="8px" Height="20px"></asp:textbox>
             </td>
			 <td class="etiqueta" style="WIDTH: 300px; HEIGHT: 20px">Activo/CTC:</td>
			 <td class="etiqueta" style="WIDTH: 70px; HEIGHT: 20px">Cantidad:</td>
			 <td class="etiqueta" style="WIDTH: 30px; HEIGHT: 20px"></td>
		    </tr>
			<tr>
			 <td class="etiqueta" style="WIDTH: 150px; HEIGHT: 20px">
			  <asp:textbox id="txtCodArticulo" runat="server" Font-Size="10px" Height="20px" Width="100px" MaxLength="20"></asp:textbox>
              &nbsp;<input class="boton" id="btnArticulos" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarArticulos();" type="button" value="..." name="btnArticulos"/>
             </td>
			 <td class="etiqueta" style="WIDTH: 250px; HEIGHT: 20px">
              <asp:dropdownlist id="cboCuentaGastos" runat="server" AutoPostBack="true" CssClass="cbo" Font-Size="9px" Height="20px" Width="200px" Enabled="false"></asp:dropdownlist>
			  &nbsp;<asp:imagebutton id="imgBuscarCtaGastos" runat="server" ImageUrl="../../intranet/Imagenes/Buscar.gif" ToolTip="Consultar Cuenta de Gastos"></asp:imagebutton>
             </td>
			 <td class="etiqueta" style="WIDTH: 300px; HEIGHT: 20px">
			  <asp:textbox id="txtOrdenServicio" runat="server" width="100px" Font-Size="10px" Height="20px" MaxLength="20"></asp:textbox>
              &nbsp;<input class="boton" id="btnServicio" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:BuscarOrdenServicio();" type="button" value="..." name="btnServicio"/>
             </td>
			 <td class="etiqueta" style="WIDTH: 70px; HEIGHT: 20px">
              <asp:textbox id="txtCantidad" runat="server" Font-Bold="true" Text="0.00" Font-Size="10px" Height="20px" Width="60px">0.00</asp:textbox>
             </td>
			 <td class="etiqueta" style="WIDTH: 30px; HEIGHT: 20px">
              <asp:imagebutton id="btnAgregar" runat="server" ImageUrl="../../intranet/Imagenes/save_16.png" ToolTip="Guardar Registro en el  Pedido"></asp:imagebutton>
             </td>
		    </tr>
		    <tr>
		     <td class="etiqueta" style="WIDTH: 400px; HEIGHT: 20px" colspan="2">
              <asp:textbox id="lblDesArticulo" BorderStyle="None" runat="server" CssClass="etiqueta" Height="20px" Width="200px"></asp:textbox>
              <asp:textbox id="lblUniMedida" BorderStyle="None" Runat="server" CssClass="etiqueta" width="50px" Font-Size="8px" Height="20px"></asp:textbox>
              <asp:textbox id="lblStockArticulo" BorderStyle="None" Runat="server" CssClass="etiqueta" width="50px" Font-Size="8px" Height="20px"></asp:textbox>
			  <asp:textbox id="lblPrecioArticulo" BorderStyle="None" Runat="server" CssClass="etiqueta" width="50px" Font-Size="8px" Height="20px"></asp:textbox>
             </td>
		     <td class="etiqueta" style="WIDTH: 300px; HEIGHT: 20px">
			  <asp:textbox id="txtDesServicio" BorderStyle="None" Runat="server" CssClass="etiqueta" width="300px" Font-Size="8px" Height="20px"></asp:textbox>
             </td>
		     <td class="etiqueta" style="WIDTH: 70px; HEIGHT: 20px"></td>
		     <td class="etiqueta" style="WIDTH: 30px; HEIGHT: 20px"></td>
            </tr>
		   </table>
		  </asp:panel>
		</tr>
        </table>
        
        <table style="WIDTH: 800px">
         <tr>
          <td><asp:label id="lblItems" Runat="server" text="Numero de Items">Numero de Items:</asp:label></td>  
         </tr>
		 <tr>
		  <td>
           <asp:datagrid id="dgDetallePedido" runat="server" Width="800px" AutoGenerateColumns="False">
		    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
			<ItemStyle CssClass="GridItem"></ItemStyle>
			<HeaderStyle CssClass="GridHeader"></HeaderStyle>
			<Columns>
			 <asp:TemplateColumn HeaderText="Sec.">
			  <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
			  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
			  <ItemTemplate>
			   <asp:Label id="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_SECU") %>'></asp:Label>
			  </ItemTemplate>
			 </asp:TemplateColumn>
			 
             <asp:TemplateColumn HeaderText="Codigo">
			  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			  <ItemStyle HorizontalAlign="Left"></ItemStyle>
			  <ItemTemplate>
			   <asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
			  </ItemTemplate>
			 </asp:TemplateColumn>
			
             <asp:TemplateColumn HeaderText="Descripcion">
			  <HeaderStyle HorizontalAlign="Center" Width="300px" VerticalAlign="Middle"></HeaderStyle>
			  <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
			  <ItemTemplate>
			   <asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'></asp:Label>
			  </ItemTemplate>
			 </asp:TemplateColumn>
			 
             <asp:TemplateColumn HeaderText="Cta Gasto">
			  <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
			  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
			  <ItemTemplate>
			   <asp:Label id="lblCtagasto" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
			   <asp:Label id="lblDesCtaGasto" runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.DesCtaGasto") %>'></asp:Label>
              </ItemTemplate>
			 </asp:TemplateColumn>
			 
             <asp:TemplateColumn HeaderText="Act. Fijo CTC">
			  <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
			  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
			  <ItemTemplate>
			   <asp:Label id="lblActivoFijo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>
               <asp:Label id="lblDesActivo" runat="server" visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.DesActivo") %>'></asp:Label>
			  </ItemTemplate>
			 </asp:TemplateColumn>

			<asp:TemplateColumn HeaderText="Cantidad Solicitada">
			 <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			 <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
			 <ItemTemplate>
			  <asp:Label id="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEDI") %>'></asp:Label>
			 </ItemTemplate>
			</asp:TemplateColumn>
			
            <asp:TemplateColumn HeaderText="U.M.">
			 <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			 <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
			 <ItemTemplate>
              <asp:Label id="lblUnidaMedida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>'></asp:Label>
             </ItemTemplate>
			</asp:TemplateColumn>
			
            <asp:TemplateColumn HeaderText="Precio">
			 <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			 <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
			 <ItemTemplate>
			  <asp:Label id="lblPrecio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PE_ITEM") %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>
			
            <asp:BoundColumn DataField="SubTotal" HeaderText="Monto (S/.)" DataFormatString="{0:#,##0.00}">
			 <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
			 <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
			</asp:BoundColumn>
			
            <asp:TemplateColumn>
			 <HeaderStyle Width="3%"></HeaderStyle>
			 <ItemStyle HorizontalAlign="Center"></ItemStyle>
			 <ItemTemplate>
			  <asp:ImageButton id="btnEditarItem" runat="server" ImageUrl="../../intranet/Imagenes/editar.gif" CommandName="Editar"></asp:ImageButton>
			 </ItemTemplate>
			</asp:TemplateColumn>
			
            <asp:TemplateColumn>
			 <HeaderStyle Width="3%"></HeaderStyle>
			 <ItemTemplate>
			  <asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/eliminar.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CO_ITEM")%>'></asp:ImageButton>
			 </ItemTemplate>
			</asp:TemplateColumn>
		   </Columns>
		  </asp:datagrid></td>
		 </tr>

		 <tr>
		  <td style="WIDTH: 800px; HEIGHT: 20px" align="right">
           <asp:label id="lblMonto" Runat="server" Height="20px">Total (S/.):</asp:label>
           <asp:label id="lblTotalPedido" Runat="server" Font-Bold="true" Height="20px"></asp:label>
          </td>
		 </tr>
		 
         <tr>
		  <td>
			<table style="WIDTH:800px; HEIGHT:20px" cellspacing="0" cellpadding="0" border="0">
			  <tr>
				<td style="WIDTH: 800px; HEIGHT:20px" align="right">
                 <asp:button id="btnNuevo" Runat="server" CssClass="boton" Text="Nuevo" ToolTip="Nuevo Pedido" Width="100px"></asp:button>
                 &nbsp;<input class="boton" id="btnBuscar" onclick="javascript:VerConsultaPedido();" type="button" value="Buscar" name="btnBuscar" runat="server" size="100px"/>
                 &nbsp;<asp:button id="btnSolicitaAprobacion" runat="server" CssClass="boton" Text="Solic. Aprobacion" Width="100px"></asp:button>
                 &nbsp;<asp:button id="btnVerSeguimiento" runat="server" CssClass="boton" Text="Ver Seguimiento" Width="100px"></asp:button>
                 &nbsp;<asp:button id="btnAnular" Runat="server" CssClass="boton" Text="Anular" Width="100px" ToolTip="Anular Pedido"></asp:button>
                 &nbsp;<input class="boton" id="btnSalir" onclick="javascript:fnc_Cerrar();" type="button" value="Salir" name="btnSalir" runat="server" size="100px"/>
                </td>
			  </tr>
			 </table>
		    </td>
		   </tr>
		   <tr>
		    <td><asp:label id="lblMsgError" Runat="server" text="" Font-Bold="true" ForeColor="Red"></asp:label></td>
		   </tr>
	      </table> 
        </form>
       </center>
      </body>
</html>
