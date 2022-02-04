<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ConversiondeValesAPedidos_Detalle.aspx.vb" Inherits="intranet_logi.frm_ConversiondeValeAPedidos_Detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
	<title>frm_ConversiondeValeAPedidos_Detalle</title>
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
		//funcion Seguimiento de Aprobacion
		function btnSeguimiento_Onclick()
		{
			if(document.all('txtNumeroPedido').value == '')
			{
				return false;
			}
			var z = document.all('txtNumeroPedido').value;
			var intCodigoPedido = parseInt(z.substring(6,10));
			var retorno=window.showModalDialog("frm_ListarSeguimientoPedidos.aspx?intCodigoPedido="+ intCodigoPedido,"","dialogHeight:300px;dialogWidth:800px;center:yes;help:no;status=no");
			return false;	
		}
		// funcion Solicitar Aprobacion
		function SolicitarAprobacion()
		{
			var retorno = window.showModalDialog("../../intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var Codigo = datos[0];
				document.all('txtAcepta').value = Codigo;
			}
		}
		 //funcion Buscar Pedido
		function VerConsultaPedido()
		{
			var retorno = window.showModalDialog("frm_ConsultaPedido.aspx","","dialogHeight:600px;dialogWidth:800px;center:yes;help:no;");
			if (retorno==""){
			   return false;
			}
			if(retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var NumPedido = datos[0];
				document.all('txtCodigo').value = NumPedido
				return true;
			}
			//window.open("frm_ConsultaPedido.aspx","","width=900,height=600,scrollbars=YES, status=NO");
			//window.open('','_parent','');
			//window.close();
		}
		// funcion Cerrar
		function fnc_Cerrar()
		{
			window.open('','_parent','');
			window.close();
		}
		// Funcion Editar Articulo
		function EditarItemPedido(CodItem, Cantidad, DesItem, UniItem, PreItem)
		{
			document.all('txtCodArticulo').value = CodItem;
			document.all('txtCantidad').value = parseFloat(Cantidad).toFixed(3);
			document.all('txtCanX').value = parseFloat(Cantidad).toFixed(3);
			document.all('lblPrecioArticulo').value = parseFloat(PreItem).toFixed(3);
			document.all('lblDesArticulo').value = DesItem;
			document.all('lblUniMedida').value = UniItem;
			document.all('txtCantidad').focus();
		}
		// Funcion Confirma Accion
		function Confirmacion(stMensaje)
		{
			var Mensaje = stMensaje;
			if (confirm(Mensaje) == true){	
				var strAcepto = "1";
				document.all('txtAcepta').value = strAcepto;
			}
			else{	
				var strAcepto = "0";
				document.all('txtAcepta').value = strAcepto;
			}
		}
		// Funcion Busca Centro de Costos
		function fnc_BuscarCentroCosto()
		{
			var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var Codigo = datos[0];
				var Nombre = datos[1];
				document.all('txtCodCentroCostos').value = Codigo;
				document.all('lblDesCentroCostos').value = Nombre;
			}
		}
		// Funcion Busca Cuenta de Gastos
		function fnc_BuscarCtaGasto() 
		{
			var retorno = window.showModalDialog("../../intranet/Buscadores/frmCtaGasto.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if(retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var Codigo = datos[0];
				var Nombre = datos[1];
				document.all('txtCodCuentaGastos').value = Codigo;
				document.all('lblDesCuentaGastos').value = Nombre;
			}
		}
		// Funcion Busca Empleados
		function fnc_BuscarTrabajadores()
		{
			var tipo = "EMP";
			var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo="+tipo,"","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
				var datos = retorno.split(":");
				var codigo = datos[0];
				var nombre = datos[1];
				document.all('txtCodSolicitante').value = codigo;
				document.all('lblDesSolicitante').value = nombre;
			}
		}
		// Funcion Busca Articulos
		function fnc_BuscarArticulos()
		{
			var x = document.all('cboAlmacen').value;
			if (x!=""){
					var lstrAlmacen = x.substring(0,3);
					var lpstrTipo = 9;
					var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen="+lstrAlmacen + "&pstrTipo=" + lpstrTipo,"","dialogHeight:500px;dialogWidth:800px;center:yes;help:no;");
					if (retorno!="" && retorno!=":")
					{
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
			else{
					alert("Debe elegir un almacen para oonsultar Articulos");
					document.all('cboAlmacen').focus();
			}
			
		}
		// Para Validar el ingreso de datos obligatorios
		function fnc_GrabarDatos()
		{
			var lintVerificar = 0;
			lintVerificar = fnc_VerificarDatos()
			if(lintVerificar == 0)
			{
				return false;
			}else{
				return true;
			}
		}
		// Verifica si todos los datos estan bien ingresados
		function fnc_VerificarDatos()
		{	// Solicitante
			if(document.all('txtCodSolicitante').value=="")
			{
				alert("Debe ingresar el solicitante del Vale de almacen.");
				document.all('txtSolicitante').focus();
				return 0
			}
			// Centro de Costos
			var ActivoCTC = document.all('txtOrdenServicio').value;
			var TipoActivoCTC = ActivoCTC.substring(0,1);
			if(document.all('txtCodCentroCostos').value=="" && TipoActivoCTC.value != "9" && document.all('rdbVale').checked)
			{
				alert("Debe ingresar el centro de costo del Vale de almacen.");
				document.all('txtCodCentroCostos').focus();
				return 0
			}// Almacen
			if(document.all('cboAlmacen').value=="")
			{
				alert("Debe elegir el almacen origen del Vale de almacen");
				document.all('cboAlmacen').focus();
				return 0
			}// Item
			if(document.all('txtCodArticulo').value=="")
			{
				alert("Debe ingresar el articulo a solicitar.");
				document.all('txtCodArticulo').focus();
				return 0
			}// Cuenta de Gastos
			var varActivo = document.all('txtOrdenServicio').value;
			var strActivo = parseInt(varActivo.substring(1,1));
			if(document.all('cboCuentaGastos').value=="" && document.all('txtDesCuentaGasto').value=="" && strActivo=="0")
			{
				alert("Debe elegir cuenta de gasto.");
				document.all('cboCuentaGastos').focus();
				return 0
			}//cantidad
			if(document.all('txtCantidad').value=="" || document.all('txtCantidad').value==0)
			{
				alert("Debe ingresar la cantidad para el articulo.");
				document.all('txtCantidad').focus();
				return 0
			}
			return 1
		}
		function txtCantidad_onBlur(ControlID,Constante)
		{
			var ltxtCantidad=document.all(ControlID + 'txtCantidad'+Constante);
			if(ltxtCantidad.value=='' || isVarType(ltxtCantidad.value,'NUM')==false)
			{
				alert('La cantidad ingresada no es valida.');
				ltxtCantidad.focus();
				return false;
			}
			ltxtCantidad.value=parseFloat(ltxtCantidad.value).toFixed(2);
			return true;
		}
		
		//Muestra el listado de los Activos fijos y las ordenes de Trabajo
		function BuscarOrdenServicio() 
		{
			var strCentroCostos = document.all('txtCodCentroCostos').value;
			if (strCentroCostos=="" && document.all('rdbVale').checked)
			{
				alert("Debe elegir un centro de costos");
				document.all('txtCodCentroCostos').focus();
			}
			else
			{
				var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaActivo.aspx?strCentroCostos="+ strCentroCostos,"","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
				if (retorno!="" && retorno!=":")
					{
						var datos = retorno.split(":");
						var Codigo = datos[0];
						var Nombre = datos[1];
						document.all('txtOrdenServicio').value=Codigo;
						document.all('txtDesServicio').value=Nombre;
					}
			}
		}
	</script>
  </head>
  <body>
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
		  <td class="Cabecera" style="WIDTH: 100%; HEIGHT: 20px" vAlign="middle" align="left">Registro 
			de Vale de Almacen
		  </td>
		</tr>
	  </table>
	  <table style="WIDTH: 80%; HEIGHT: 10px" cellspacing="1" cellpadding="1" border="0">
		<tr>
		  <td style="HEIGHT: 258px">
			<!-- Cabecera de Pedidos  -->
			<table style="WIDTH: 98%; HEIGHT: 160px">
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 23px" width="135">Nro Vale 
				  Mantto.:
				</td>
				<td style="WIDTH: 356px; HEIGHT: 23px"><asp:textbox id="txtValeMantto" tabIndex="2" Runat="server" Height="20px" Font-Size="9px" width="130px"
					CssClass="inputDisabled2"></asp:textbox></td>
				<td style="WIDTH: 97px; HEIGHT: 23px"></td>
				<td style="WIDTH: 384px; HEIGHT: 23px"></td>
				<td style="WIDTH: 495px; HEIGHT: 23px"></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 23px" width="135"></td>
				<td style="WIDTH: 356px; HEIGHT: 23px"><asp:radiobutton id="rdbVale" Runat="server" CssClass="" BackColor="#CDE0EF" Text="Vale" Checked="True"
					AutoPostBack="True"></asp:radiobutton><asp:radiobutton id="rdbCTC" Runat="server" CssClass="" BackColor="#CDE0EF" Text="CTC" AutoPostBack="True"></asp:radiobutton></td>
				<td style="WIDTH: 97px; HEIGHT: 23px"></td>
				<td style="WIDTH: 384px; HEIGHT: 23px"></td>
				<td style="WIDTH: 495px; HEIGHT: 23px"></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 23px" width="135">Situacion:
				</td>
				<td style="WIDTH: 356px; HEIGHT: 23px"><asp:textbox id="txtEstado" tabIndex="2" Runat="server" Height="20px" Font-Size="9px" width="130px"
					CssClass="inputDisabled2"></asp:textbox></td>
				<td style="WIDTH: 97px; HEIGHT: 23px"></td>
				<td style="WIDTH: 384px; HEIGHT: 23px"></td>
				<td style="WIDTH: 495px; HEIGHT: 23px"></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 22px" width="135"><asp:label id="lblNumeroPedido" runat="server" CssClass="etiqueta" Width="122px">Numero de Pedido:</asp:label></td>
				<td style="WIDTH: 356px; HEIGHT: 22px"><asp:textbox id="txtSeriePedido" Runat="server" Height="20px" Font-Size="9px" CssClass="inputDisabled2"
					Width="50px"></asp:textbox><asp:textbox id="txtNumeroPedido" Runat="server" Height="20px" Font-Size="9px" CssClass="inputDisabled2"
					Width="80px"></asp:textbox></td>
				<td style="WIDTH: 97px; HEIGHT: 22px">Fec. de Pedido:</td>
				<td style="WIDTH: 384px; HEIGHT: 22px">Fec. de Aprob.:</td>
				<td style="WIDTH: 495px; HEIGHT: 22px">Fec. de Atenc:</td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 22px" width="135">Solicitante:</td>
				<td style="WIDTH: 356px; HEIGHT: 22px"><asp:textbox id="txtCodSolicitante" tabIndex="2" Runat="server" Height="20px" Font-Size="9px"
					width="110px" MaxLength="20"></asp:textbox><input class="boton" id="btnSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarTrabajadores();"
					type="button" value="..." name="btnSolicitante" />
				  <asp:textbox id="lblDesSolicitante" tabIndex="2" BorderStyle="None" Runat="server" Height="20px"
					Font-Size="9px" width="200px" BackColor="#CDE0EF"></asp:textbox></td>
				<td style="WIDTH: 97px; HEIGHT: 22px"><asp:textbox id="txtFechaPedido" BorderStyle="None" Runat="server" Height="20px" Font-Size="10px"
					BackColor="#CDE0EF" Text="" Width="80px"></asp:textbox></td>
				<td style="WIDTH: 384px; HEIGHT: 22px"><asp:textbox id="txtFechaAprobacion" BorderStyle="None" Runat="server" Height="20px" Font-Size="10px"
					BackColor="#CDE0EF" Text="" Width="80px"></asp:textbox></td>
				<td style="WIDTH: 495px; HEIGHT: 22px"><asp:textbox id="txtFechaAtencion" BorderStyle="None" Runat="server" Height="20px" Font-Size="10px"
					BackColor="#CDE0EF" Text="" Width="80px"></asp:textbox></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 2px" width="135">Centro de 
				  Costos:</td>
				<td style="WIDTH: 356px; HEIGHT: 2px"><asp:textbox id="txtCodCentroCostos" tabIndex="3" Runat="server" Height="20px" Font-Size="9px"
					width="110px" MaxLength="20"></asp:textbox><input class="boton" id="btnCentroCosto" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarCentroCosto();"
					type="button" value="..." name="btnCentroCosto" />
				  <asp:textbox id="lblDesCentroCostos" tabIndex="2" BorderStyle="None" Runat="server" Height="20px"
					Font-Size="9px" width="200px" BackColor="#CDE0EF"></asp:textbox></td>
				<td style="WIDTH: 97px; HEIGHT: 2px">Psto. Inic.(S/.):</td>
				<td style="WIDTH: 384px; HEIGHT: 2px">Psto. Util.(S/.):</td>
				<td style="WIDTH: 495px; HEIGHT: 2px">Psto. Disp.(S/.):</td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 6px">Almacen Origen:</td>
				<td style="WIDTH: 356px; HEIGHT: 6px"><asp:dropdownlist id="cboAlmacen" Runat="server" Height="20px" Font-Size="9px" CssClass="cbo" Width="250px"
					Enabled="false"></asp:dropdownlist></td>
				<td style="WIDTH: 97px; HEIGHT: 6px"><asp:label id="lblPstoInicial" Runat="server" text="" Font-Bold="True" Height="20px" Font-Size="10px"
					BackColor="#CDE0EF" Width="80px"></asp:label></td>
				<td style="WIDTH: 384px; HEIGHT: 6px"><asp:label id="lblPstoUtilizado" Runat="server" text="" Font-Bold="True" Height="20px" Font-Size="10px"
					BackColor="#CDE0EF" Width="80px"></asp:label></td>
				<td style="WIDTH: 495px; HEIGHT: 6px"><asp:label id="lblPstoDisponible" Runat="server" text="" Font-Bold="True" Height="20px" Font-Size="10px"
					BackColor="#CDE0EF" Width="80px"></asp:label></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 135px; HEIGHT: 12px">Observacion:</td>
				<td style="WIDTH: 356px; HEIGHT: 12px"><asp:textbox id="txtObservaciones" tabIndex="5" Runat="server" Height="43px" Font-Size="9px"
					CssClass="input" Width="350px" MaxLength="100" TextMode="MultiLine"></asp:textbox><asp:label id="lblContadorPalabras" Runat="server" text="" Font-Bold="True" Height="8px" Font-Size="9px"
					Width="104px"></asp:label></td>
				<td style="WIDTH: 97px; HEIGHT: 12px"><asp:textbox id="txtAcepta" tabIndex="2" BorderStyle="None" Runat="server" Height="10px" Font-Size="8px"
					width="20px" BackColor="#CDE0EF" ForeColor="#CDE0EF"></asp:textbox><asp:textbox id="txtSituacion" tabIndex="2" BorderStyle="None" Runat="server" Height="10px" Font-Size="8px"
					width="20px" BackColor="#CDE0EF" ForeColor="#CDE0EF"></asp:textbox><asp:textbox id="txtCanX" tabIndex="2" BorderStyle="None" Runat="server" Height="10px" Font-Size="8px"
					width="20px" BackColor="#CDE0EF" ForeColor="#CDE0EF">0.00</asp:textbox></td>
				<td style="WIDTH: 384px; HEIGHT: 12px" align="right"><asp:textbox id="txtCodigo" tabIndex="2" BorderStyle="None" Runat="server" Height="10px" Font-Size="8px"
					width="80px" BackColor="#CDE0EF" ForeColor="#CDE0EF"></asp:textbox></td>
				<td style="WIDTH: 495px; HEIGHT: 12px"></td>
			  </tr>
			  <tr>
				<td style="WIDTH: 135px; HEIGHT: 8px" colspan="5"><asp:label id="lblError" Runat="server" text="" Font-Bold="True" Height="12px" Width="800px"
					ForeColor="Red"></asp:label></td>
			  </tr>
			</table>
			<table id="Table3" style="WIDTH: 824px; HEIGHT: 24px">
			  <tr>
				<td class="etiqueta" style="WIDTH: 150px; HEIGHT: 19px">Articulo:</td>
				<td class="etiqueta" style="WIDTH: 861px; HEIGHT: 19px" valign="middle" align="left">Cuenta 
				  de Gastos:&nbsp;
				  <asp:textbox id="txtDesCuentaGasto" tabIndex="2" BorderStyle="None" Runat="server" Height="15px"
					Font-Size="8px" width="219px" CssClass="etiqueta"></asp:textbox></td>
				<td class="etiqueta" style="WIDTH: 331px; HEIGHT: 19px">Activo /CTC:</td>
				<td class="etiqueta" style="WIDTH: 40px; HEIGHT: 19px">Cantidad:</td>
				<td class="etiqueta" style="WIDTH: 34px; HEIGHT: 19px"></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 150px; HEIGHT: 22px"><nobr><asp:textbox id="txtCodArticulo" tabIndex="6" Runat="server" Height="20px" Font-Size="10px" Width="100px"
					MaxLength="20"></asp:textbox><input class="boton" id="btnArticulos" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_BuscarArticulos();"
					type="button" value="..." name="btnArticulos" /></nobr></td>
				<td class="etiqueta" style="WIDTH: 861px; HEIGHT: 22px"><asp:dropdownlist id="cboCuentaGastos" Runat="server" Height="32px" Font-Size="9px" CssClass="cbo"
					AutoPostBack="True" Width="244px" Enabled="false"></asp:dropdownlist><asp:imagebutton id="imgBuscarCtaGastos" runat="server" ImageUrl="../../intranet/Imagenes/Buscar.gif" ToolTip="Consultar Cuenta de Gastos"></asp:imagebutton></td>
				<td class="etiqueta" style="WIDTH: 331px; HEIGHT: 22px"><asp:textbox id="txtOrdenServicio" tabIndex="4" Runat="server" Height="20px" Font-Size="10px"
					width="100px" MaxLength="20"></asp:textbox><input class="boton" id="btnServicio" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:BuscarOrdenServicio();"
					type="button" value="..." name="btnServicio" /></td>
				<td class="etiqueta" style="WIDTH: 40px; HEIGHT: 22px"><asp:textbox id="txtCantidad" tabIndex="7" runat="server" Font-Bold="True" Height="20px" Font-Size="10px"
					Text="0.00" Width="60px">0.00</asp:textbox></td>
				<td class="etiqueta" style="WIDTH: 34px; HEIGHT: 22px"><asp:imagebutton id="btnAgregar" runat="server" ImageUrl="../../intranet/Imagenes/save_16.png" ToolTip="Guardar Registro en el  Pedido"></asp:imagebutton></td>
			  </tr>
			  <tr>
				<td class="etiqueta" style="WIDTH: 993px; HEIGHT: 13px" colspan="2"><asp:textbox id="lblDesArticulo" BorderStyle="None" runat="server" Height="15px" CssClass="etiqueta"
					Width="254px"></asp:textbox><asp:textbox id="lblUniMedida" tabIndex="2" BorderStyle="None" Runat="server" Height="15px" Font-Size="8px"
					width="55px" CssClass="etiqueta"></asp:textbox><asp:textbox id="lblStockArticulo" tabIndex="2" BorderStyle="None" Runat="server" Height="15px"
					Font-Size="8px" width="71px" CssClass="etiqueta"></asp:textbox><asp:textbox id="lblPrecioArticulo" tabIndex="2" BorderStyle="None" Runat="server" Height="15px"
					Font-Size="8px" width="65px" CssClass="etiqueta"></asp:textbox></td>
				<td class="etiqueta" style="WIDTH: 331px; HEIGHT: 13px"><asp:textbox id="txtDesServicio" tabIndex="2" BorderStyle="None" Runat="server" Height="15px"
					Font-Size="8px" width="250px" CssClass="etiqueta"></asp:textbox></td>
				<td class="etiqueta" style="WIDTH: 40px; HEIGHT: 13px"></td>
				<td class="etiqueta" style="WIDTH: 34px; HEIGHT: 13px"></td>
			  </tr>
			</table>
		  </td>
		<tr>
		  <td style="HEIGHT: 1px"><asp:label id="lblItems" Runat="server" text="Numero de Items">Numero de Items:</asp:label></td>
		</tr>
		<tr>
		  <td><asp:datagrid id="dgDetallePedido" runat="server" Width="826px" AutoGenerateColumns="False">
			  <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
			  <ItemStyle CssClass="GridItem"></ItemStyle>
			  <HeaderStyle CssClass="GridHeader"></HeaderStyle>
			  <Columns>
				<asp:TemplateColumn HeaderText="Sec.">
				  <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
				  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_SECU") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Codigo">
				  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				  <ItemStyle HorizontalAlign="Left"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Descripcion">
				  <HeaderStyle HorizontalAlign="Center" Width="300px" VerticalAlign="Middle"></HeaderStyle>
				  <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Cta Gasto">
				  <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
				  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblCtagasto" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'>
					</asp:Label>
					<asp:Label id="lblDesCtaGasto" runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.DesCtaGasto") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Act. Fijo CTC">
				  <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
				  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblActivoFijo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'>
					</asp:Label>
					<asp:Label id="lblDesActivo" runat="server" visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.DesActivo") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Cantidad Solicitada">
				  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				  <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEDI") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="U.M.">
				  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				  <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblUnidaMedida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>'>
					</asp:Label>
				  </ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Precio">
				  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				  <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
				  <ItemTemplate>
					<asp:Label id="lblPrecio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PE_ITEM") %>'>
					</asp:Label>
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
					<asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/eliminar.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CO_ITEM")%>'>
					</asp:ImageButton>
				  </ItemTemplate>
				</asp:TemplateColumn>
			  </Columns>
			</asp:datagrid></td>
		</tr>
		<tr>
		  <td align="right"><asp:label id="lblMonto" Runat="server" Height="15px">Total (S/.):</asp:label><asp:label id="lblTotalPedido" Runat="server" Font-Bold="True" Height="15px"></asp:label></td>
		</tr>
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
				<td style="WIDTH: 9.35%" align="center"><asp:button id="btnNuevo" Runat="server" CssClass="boton" Text="Nuevo" ToolTip="Nuevo Pedido"></asp:button></td>
				<td style="WIDTH: 10.24%" align="center"><asp:button id="btnGuardar" Runat="server" CssClass="boton" Text="Guardar" Width="83px"></asp:button></td>
				<td style="WIDTH: 15%" align="center"><asp:button id="btnSolicitaAprobacion" Runat="server" CssClass="boton" Text="Solicitar Aprobacion"
					Width="120px"></asp:button>
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
