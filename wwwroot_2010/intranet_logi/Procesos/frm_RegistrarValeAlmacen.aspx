<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarValeAlmacen.aspx.vb" Inherits="intranet_logi.frm_RegistrarValeAlmacen" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">

  <meta http-equiv="X-UA-Compatible" content="IE=9"/>  
  <title>Nuevo Mundo - Registrar pedido de almacen</title>
  <%--<link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>--%>
  <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
  <%--<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>--%>
  <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
  <%--<link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>--%>
  <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
  <%--<link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet"/>--%>
  <link href="../css/EstilosWeb.css" rel="stylesheet" type="text/css" />
  <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>
  <%--<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>--%>
<%--  <script src="../js/jsCalendario_N4.js" type="text/javascript"></script>--%>
     <script src="../js/jquery-1.12.1.js.js" type="text/javascript"></script>
     <script src="../js/jquery-ui.js" type="text/javascript"></script>
     <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
     

  <script language="javascript" type="text/javascript">

    // --- Inicio: funciones generales

    //funcion valida numero
    //function txtCantidad_onBlur(ControlID, Constante) {
    //  if (txtCantidad.value == '' || isVarType(txtCantidad.value, 'NUM') == false) {
    //    alert('La cantidad ingresada no es valida.');
    //    txtCantidad.focus();
    //    return false;
    //  }
    //  txtCantidad.value = parseFloat(txtCantidad.value).toFixed(2);
    //  return true;
    //}
    // Validacion de datos
    function fnc_VerificarDatos() {

      // Solicitante
      if (document.all('txtCodSolicitante').value == "") {
        alert("Debe ingresar el solicitante para el vale de almacen.");
        document.all('txtSolicitante').focus();
        return 0
      }
      // Almacen
      if (document.all('txtCodAlmacen').value == "") {
        alert("Debe elegir el almacen origen para el vale de almacen");
        document.all('txtCodAlmacen').focus();
        return 0
      }
      // Centro de Costos
      if (document.all('txtCodCentroCostos').value == "") {
        alert("Debe ingresar el centro de costo para el vale de almacen.");
        document.all('txtCodCentroCostos').focus();
        return 0
    }

    //Prioridad
    if (document.getElementById("cboPrioridad").value == "") {
        alert("Debe seleccionar una prioridad.");
        document.getElementById("cboPrioridad").focus();
        return 0
    }
    
      // Fecha de instalacion
      if (document.getElementById("txtFecInstal").value == "") {
        alert("Debe ingresar una fecha de instalacion para el pedido");
        document.getElementById("txtFecInstal").focus();
        return 0
      }
      // Item
      if (document.all('txtCodArticulo').value == "") {
        alert("Debe ingresar el articulo a solicitar.");
        document.all('txtCodArticulo').focus();
        return 0
      }
      // Cuenta de Gastos
      var varActivo = document.all('txtCodOrdenServicio').value;
      var strActivo = parseInt(varActivo.substring(1, 1));
      if (document.all('txtCodCuentaGastos').value == "" && document.all('lblDesCuentaGasto').value == "" && strActivo.value != "9" && document.all('rdbVale').checked == true) {
        alert("Debe elegir cuenta de gasto para el vale de almacen.");
        document.all('txtCodCuentaGastos').focus();
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

    // --- Fin: funciones generales
    
    // --- Inicio: funciones para los botones

    //Buscar Pedido
    function VerConsultaPedido() {
      var retorno = window.showModalDialog("frm_ConsultaPedido.aspx", "", "dialogHeight:600px;dialogWidth:800px;center:yes;help:no;");
      if (retorno == "") {
        return false;
      }
      if (retorno != "" && retorno != ":") {
        var datos = retorno.split(":");
        var NumPedido = datos[0];
        document.all('txtCodigo').value = NumPedido;
        return true;
      }
    }

    //Seguimiento de Aprobacion
    function btnSeguimiento_Onclick() {
      if (document.all('txtNumeroPedido').value == '') {
        return false;
      }
      var z = document.all('txtNumeroPedido').value;
      var intCodigoPedido = parseInt(z, 10);
      //alert(intCodigoPedido);
      var retorno = window.showModalDialog("frm_ListarSeguimientoPedidos.aspx?intCodigoPedido=" + intCodigoPedido, "", "dialogheight:300px;dialogwidth:800px;center:yes;help:no;status=no");
      return false;
    }

    //Solicitar Aprobacion
    function SolicitarAprobacion() {
      var retorno = window.showModalDialog("../../intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
      if (retorno != "" && retorno != ":") {
        var datos = retorno.split(":");
        var Codigo = datos[0];
        document.all('txtAcepta').value = Codigo;
      }
    }

    //Cerrar
    function fnc_Cerrar() {
      window.open('', '_parent', '');
      window.close();
    }

    //Editar Articulo
    function EditarItemPedido(CodItem, Cantidad, DesItem, UniItem, PreItem) {
      document.all('txtCodArticulo').value = CodItem;
      document.all('txtCantidad').value = parseFloat(Cantidad).toFixed(3);
      document.all('txtCanX').value = parseFloat(Cantidad).toFixed(3);
      document.all('lblPrecioArticulo').value = parseFloat(PreItem).toFixed(3);
      document.all('lblDesArticulo').value = DesItem;
      document.all('lblUniMedida').value = UniItem;
      document.all('txtCantidad').focus();
    }
    // --- Fin: funciones para los botones

    // --- Inicio: Buscadores
    
    //Almacenes
    function BuscarAlmacenes() {
      var retorno = window.showModalDialog("../../intranet/Buscadores/frmAlmacenes.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
      if (retorno != "" && retorno != ":") {
        var datos = retorno.split(":");
        var Codigo = datos[0];
        var Nombre = datos[1];
        document.all('txtCodAlmacen').value = Codigo;
        document.all('lblDesAlmacen').value = Nombre;
      }
    }

    //Empleados
    function fnc_Buscartrabajadores() {
      var tipo = "EMP";
      var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
      if (retorno != "" && retorno != ":") {
        var datos = retorno.split(":");
        var codigo = datos[0];
        var nombre = datos[1];
        document.all('txtCodSolicitante').value = codigo;
        document.all('lblDesSolicitante').value = nombre;
      }
    }
    //Centro de Costos
    function fnc_BuscarCentroCosto() {
      var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
      if (retorno != "" && retorno != ":") {
        var datos = retorno.split(":");
        var Codigo = datos[0];
        var Nombre = datos[1];
        document.all('txtCodCentroCostos').value = Codigo;
        document.all('lblDesCentroCostos').value = Nombre;
      }
    }

    // Funcion Busca Articulos
    function fnc_BuscarArticulos() {
      var x = document.all('txtCodAlmacen').value;
      if (x != "") {
        var lstrAlmacen = x.substring(0, 3);
        var lpstrTipo = 9;
        var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen=" + lstrAlmacen + "&pstrTipo=" + lpstrTipo, "", "dialogheight:500px;dialogwidth:800px;center:yes;help:no;");
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
        document.all('txtCodAlmacen').focus();
      }
    }

    //Cuenta de Gastos
    function fnc_BuscarCtaGasto() {
      var strCentroCostos = document.all('txtCodCentroCostos').value;
      if (strCentroCostos == "") {
        alert("Debe elegir un centro de costos para el pedido.");
        document.all('txtCodCentroCostos').focus();
      }
      else {
        var retorno = window.showModalDialog("../../intranet/Buscadores/frmCtaGasto.aspx?strCentroCosto=" + strCentroCostos, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
        if (retorno != "" && retorno != ":") {
          var datos = retorno.split(":");
          var Codigo = datos[0];
          var Nombre = datos[1];
          document.all('txtCodCuentaGastos').value = Codigo;
          document.all('lblDesCuentaGasto').value = Nombre;
       }
      }
    }

    //Activos fijos y CTC
    function BuscarOrdenServicio() {
      var strCentroCostos = document.all('txtCodCentroCostos').value;
      if (strCentroCostos == "") {
        alert("Debe elegir un centro de costos");
        document.all('txtCodCentroCostos').focus();
      }
      else {
        var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaActivo.aspx?strCentroCostos=" + strCentroCostos, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
        if (retorno != "" && retorno != ":") {
          var datos = retorno.split(":");
          var Codigo = datos[0];
          var Nombre = datos[1];
          document.all('txtCodOrdenServicio').value = Codigo;
          document.all('lblDesServicio').value = Nombre;
        }
      }
    }
    // --- fin: Buscadores


	</script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '<Ant',
                nextText: 'Sig>',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['es']);

            if ($('#txtFecInstal').value == '') {
                $('#txtFecInstal').attr('disabled', 'disabled');
            }
            
            $('#cboPrioridad').change(function () {
                var valorPrioridad = $(this).val();
                //alert(valorPrioridad);

                var date_diff = (valorPrioridad == 'URG' ? '0d' : '2d');
                $('#txtFecInstal').val('').removeAttr('disabled').removeClass('hasDatepicker').datepicker({
                    dateFormat: 'dd/mm/yy',
                    minDate: date_diff
                });
                if (valorPrioridad == 'URG') {
                    $("#txtFecInstal").datepicker().datepicker("setDate", new Date());
                }
            });

            $('#txtFecInstal').keyup(function () {
                $(this).val('');
                alert('Por favor seleccione una fecha del calendario.');
            });


        });
  </script>
 </head>

 <body>
  <form id="frm_RegistrarValeAlmacen" method="post" autocomplete="off" runat="server">
   <center>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:UpdatePanel ID="panContenido" runat="server">
    <ContentTemplate>--%>
	   <%--Titulo--%>
	   <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	    <tr>
	     <td class="Cabecera" style="width: 800px; height: 30px" align="center">Registro de vale de almacen</td>
	    </tr>
	   </table>

     <%--Inicio: Parametros Generales--%>
      <table style="width: 800px">
       <%--Tipo de vale--%>
       <tr>
	      <td class="Descripcion" style="width:130px" align="left">Tipo:</td>
	      <td style="width: 120px" align="left">
         &nbsp;<asp:radiobutton id="rdbVale" runat="server" BackColor="#CDE0EF" Text="Vale" Checked="true" AutoPostBack="true"></asp:radiobutton>
         &nbsp;<asp:radiobutton id="rdbCTC" runat="server" BackColor="#CDE0EF" Text="CTC" AutoPostBack="true"></asp:radiobutton>
        </td>
        <td class="Descripcion" style="width:380px" align="right" colspan="3">Situacion:</td>
        <td style="width:170px" align="left"><asp:textbox id="txtEstado" runat="server" CssClass="txtDeshabilitado2" Font-Size="9px" width="120px"></asp:textbox></td>
	     </tr>
           
       <%--Fechas: pedido, aprobacion y atencion--%>
       <tr>
        <td class="Descripcion" style="width:130px" align="left">Fec. Pedido:</td>
        <td style="width:120px" align="left"><asp:textbox id="txtFechaPedido" runat="server" Text="" width="120px" Font-Size="9px" CssClass="txtDeshabilitado2"></asp:textbox></td>
        <td class="Descripcion" style="width:130px" align="left">Fec. Aprob.:</td>
        <td style="width:120px" align="left"><asp:textbox id="txtFechaAprobacion" runat="server" Text="" width="120px" Font-Size="9px" CssClass="txtDeshabilitado2"></asp:textbox></td>
        <td class="Descripcion" style="width:130px" align="left">Fec. Aten.:</td>
        <td style="width:170px" align="left"><asp:textbox id="txtFechaAtencion" runat="server" Text="" width="120px" Font-Size="9px" CssClass="txtDeshabilitado2"></asp:textbox></td>
       </tr>

       <%--Presupuesto--%>
       <tr>
        <td class="Descripcion" style="width:130px" align="left">Psto. Inic(S/.):</td>
	      <td style="width:120px" align="left">
         <asp:textbox id="txtPstoInicial" runat="server" Text="" width="120px" Font-Size="9px" CssClass="txtDeshabilitado"></asp:textbox>
        </td>
	      <td class="Descripcion" style="width:130px" align="left">Psto. Disp(S/.):</td>
        <td style="width:120px" align="left">
         <asp:textbox id="txtPstoUtilizado" runat="server" Text="" width="120px" Font-Size="9px" CssClass="txtDeshabilitado"></asp:textbox>
        </td>
	      <td class="Descripcion" style="width:130px" align="left">Psto. Util(S/.):</td>
	      <td style="width:170px" align="left">
         <asp:textbox id="txtPstoDisponible" runat="server" Text="" width="120px" Font-Size="9px" CssClass="txtDeshabilitado"></asp:textbox>
        </td>
       </tr>

       <%--Numero de pedido--%>
       <tr>
	      <td class="Descripcion" style="width: 130px" align="left">Num. Pedido:</td>
	      <td style="width: 120px" align="left">
         <asp:textbox id="txtSeriePedido" runat="server" CssClass="txtDeshabilitado2" Font-Size="9px" width="30px"></asp:textbox>
         &nbsp;<asp:textbox id="txtNumeroPedido" runat="server" CssClass="txtDeshabilitado2" Font-Size="9px" width="75px"></asp:textbox>
        </td>
        <td style="width:550px" align="left" colspan="4" class="Descripcion">&nbsp;</td>
	     </tr>

       <%--solictante--%>
       <tr>
	      <td class="Descripcion" style="width: 100px" align="left">Solicitante:</td>
	      <td style="width: 120px" align="left">
         <asp:textbox id="txtCodSolicitante" runat="server" width="90px" Font-Size="9px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
         &nbsp;<input id="btnSolicitante" style="width:20px;" onclick="javascript:fnc_Buscartrabajadores();" type="button" value="..." name="btnSolicitante" class="Boton"/>
        </td>
        <td style="width:550px" align="left" colspan="4" class="Descripcion">
         <asp:textbox id="lblDesSolicitante" BorderStyle="None" Runat="server" Font-Size="9px" width="550px" class="Descripcion"></asp:textbox>
        </td>
	     </tr>

       <%--almacen--%>
       <tr>
	      <td class="Descripcion" style="width:130px" align="left">Almacen:</td>
	      <td style="width:120px" align="left">
         <asp:textbox id="txtCodAlmacen" runat="server" CssClass="txtHabilitado" width="90px"></asp:textbox>
         &nbsp;<input id="btnAlmacen" style="width: 20px; height:20px" onclick="javascript:BuscarAlmacenes();" type="button" value="..." name="btnAlmacen" class="Boton"/>
	      </td>
        <td style="width:550px" align="left" colspan="4" class="Descripcion">
         <asp:textbox id="lblDesAlmacen" BorderStyle="None" Runat="server" Font-Size="9px" width="550px" class="Descripcion"></asp:textbox>
        </td>
	     </tr>

       <%--centro costos--%>
       <tr>
	      <td class="Descripcion" style="width: 100px" align="left">Centro Cost.:</td>
	      <td style="width: 120px;" align="left">
         <asp:textbox id="txtCodCentroCostos" runat="server" Font-Size="9px" width="90px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
         &nbsp;<input id="btnCentroCosto" style="width: 20px" onclick="javascript:fnc_BuscarCentroCosto();" type="button" value="..." name="btnCentroCosto" class="Boton"/>
        </td>
        <td style="width:550px" align="left" colspan="4" class="Descripcion">
         <asp:textbox id="lblDesCentroCostos" runat="server" BorderStyle="None" Font-Size="9px" width="550px" class="Descripcion"></asp:textbox>
        </td>
	     </tr>
       
       <%--prioridad--%>
       <tr>
	      <td class="Descripcion" style="width:130px" align="left">Prioridad:</td>
	      <td style="width: 120px" align="left">
	       <asp:dropdownlist id="cboPrioridad" runat="server" CssClass="cboFormulario" 
                  Font-Size="9px" width="120px">
           <asp:ListItem Value="">Seleccionar</asp:ListItem>
            <asp:ListItem Value="NOR">Normal - 48 Horas</asp:ListItem>
	        <asp:ListItem Value="URG">Urgente - Mismo Día</asp:ListItem>
         </asp:dropdownlist>
        </td>
        <td style="width:550px" align="left" colspan="4" class="Descripcion">&nbsp;</td>
       </tr>

       <%--fecha prioridad--%>
       <tr>
	      <td class="Descripcion" style="width:130px" align="left">Fecha Instalación:</td>
	      <td style="width:120px" align="left">              
              <asp:TextBox ID="txtFecInstal" runat="server" class="input" style="width: 110px"></asp:TextBox>
          </td>
           
        <td style="width:550px" align="left" colspan="4" class="Descripcion">&nbsp;</td>
	     </tr>
       
<%--       <tr>
	      <td class="Descripcion" style="width:130px" align="left">Fec. Instal.:</td>
	      <td style="width:120px" align="left">
         <asp:textbox id="txtFecInstal" runat="server" CssClass="txtHabilitado" width="90px" MaxLength="10"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frm_RegistrarValeAlmacen.txtFecInstal, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha de instalacion" src="../../intranet/Imagenes/Calendario.gif" width="16" height="16"/>
	      </td>
        <td style="width:550px" align="left" colspan="4" class="Descripcion">&nbsp;</td>
	     </tr>--%>


       <%--observacion--%>
       <tr>
	      <td class="Descripcion" style="width: 120px" align="left">Observacion:</td>
	      <td style="width: 680px" colspan="5"  align="left" class="Descripcion">
         <asp:textbox id="txtObservaciones" runat="server" CssClass="txtAreaHabilitado" height="30px" font-Size="9px" width="450px" MaxLength="200" TextMode="MultiLine"></asp:textbox>
         <br />&nbsp;<asp:Label ID="lblNumeroCaracteres" runat="server" Text= "*Maximo 200 caracteres" Font-Size = "8px" Font-Italic="true" BackColor = "Yellow" ForeColor = "Black"></asp:Label>
        </td>
       </tr>
      </table>
      <%--Fin: Parametros Generales--%>
       
      <%--Inicio: Error--%>
      <table style="width: 800px">
       <tr>
		    <td align="left"><asp:Label ID="lblError" runat="server" Text="" CssClass="mensaje"></asp:Label></td>
	     </tr>
      </table>
      <%--Fin: Error--%>

      <%--Inicio: datos Articulo--%>
	    <table style="width: 800px"> 
	     <tr>
	      <td style="width: 800px" align="left">
         <asp:panel id="pnlArticulo" runat="server" width="800px">
		      
          <table style="width: 800px">
           <%--titulo de cabecera--%>
		       <tr>
		        <td class="Descripcion" style="width:300px" align="left">Articulo:</td>
		        <td class="Descripcion" style="width:200px" align="left">Cuenta Gastos:</td>
		        <td class="Descripcion" style="width:200px" align="left">Activo/CTC:</td>
		        <td class="Descripcion" style="width:70px" align="left">Cantidad:</td>
		        <td class="Descripcion" style="width:30px" align="left">&nbsp;</td>
		       </tr>
         
           <%--controles de cabecera--%>
           <tr>
            <td class="Descripcion" style="width: 300px">
		         <asp:textbox id="txtCodArticulo" runat="server" Font-Size="9px" width="100px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
             &nbsp;<input id="btnArticulos" style="width: 20px" onclick="javascript:fnc_BuscarArticulos();" type="button" value="..." name="btnArticulos" class="Boton"/>
            </td>
		        <td class="Descripcion" style="width: 200px">
             <asp:textbox id="txtCodCuentaGastos" runat="server" width="100px" Font-Size="9px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
              &nbsp;<input id="btnCuentaGastos" style="width: 20px" onclick="javascript:fnc_BuscarCtaGasto('','');" type="button" value="..." name="btnCuentaGastos" class="Boton"/>
            </td>
		        <td class="Descripcion" style="width: 200px">
		         <asp:textbox id="txtCodOrdenServicio" runat="server" width="100px" Font-Size="9px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
             &nbsp;<input id="btnServicio" style="width: 20px" onclick="javascript:BuscarOrdenServicio();" type="button" value="..." name="btnServicio" class="Boton"/>
            </td>
		        <td class="Descripcion" style="width:70px">
             <asp:textbox id="txtCantidad" runat="server" Font-Bold="true" Text="0.00" Font-Size="9px" width="60px" CssClass="txtHabilitado">0.00</asp:textbox>
            </td>
		        <td class="Descripcion" style="width:30px">
             <asp:imagebutton id="btnAgregar" runat="server" 
                ImageUrl="../../intranet/Imagenes/save_16.png" 
                ToolTip="Guardar Registro en el  Pedido" Height="16px"></asp:imagebutton>
            </td>
		       </tr>
         
           <%--descripcion de cabecera--%>
		       <tr>
		        <td class="Descripcion" style="width:300px">
             <asp:textbox id="lblDesArticulo" BorderStyle="None" runat="server" Cssclass="Descripcion" width="150px" Font-Size="8px"></asp:textbox>
             <asp:textbox id="lblUniMedida" BorderStyle="None" runat="server" Cssclass="Descripcion" width="50px" Font-Size="8px"></asp:textbox>
             <asp:textbox id="lblStockArticulo" BorderStyle="None" runat="server" Cssclass="Descripcion" width="60px" Font-Size="8px"></asp:textbox>
		         <asp:textbox id="lblPrecioArticulo" BorderStyle="None" runat="server" Cssclass="Descripcion" width="40px" Font-Size="8px"></asp:textbox>
            </td>
		        <td class="Descripcion" style="width:200px">
             <asp:TextBox ID="lblDesCuentaGasto" runat="server" BorderStyle="None" Cssclass="Descripcion" Font-Size="8px" width="200px"></asp:TextBox>
            </td>
		        <td class="Descripcion" style="width:200px">
             <asp:textbox id="lblDesServicio" BorderStyle="None" runat="server" Cssclass="Descripcion" width="200px" Font-Size="8px"></asp:textbox>
            </td>
		        <td class="Descripcion" style="width:70px">&nbsp;</td>
            <td class="Descripcion" style="width:30px">&nbsp;</td>
           </tr>
          </table>
         </asp:panel>
        </td>
       </tr>
      </table>
      <%--Fin: datos Articulo--%>

      <%--Inicio: grilla--%>
      <table style="WIDTH: 800px">
       <tr>
        <td align="left"><asp:label id="lblItems" Runat="server" text="Numero de Items" CssClass="contador"></asp:label></td>  
       </tr>
	     <tr>
	      <td>
         <asp:datagrid id="dgDetallePedido" runat="server" Width="800px" AutoGenerateColumns="False">
		      <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		      <ItemStyle CssClass="GridItem"></ItemStyle>
		      <HeaderStyle CssClass="GridHeader"></HeaderStyle>
		      <Columns>
           
           <%--Secuencia--%>
           <asp:TemplateColumn HeaderText="Sec.">
		        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
		        <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_SECU") %>'></asp:Label>
		        </ItemTemplate>
		       </asp:TemplateColumn>
           
           <%--Codigo--%>
           <asp:TemplateColumn HeaderText="Codigo">
		        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
		        <ItemStyle HorizontalAlign="Left"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
		        </ItemTemplate>
		       </asp:TemplateColumn>
           
           <%--Descripcion--%>
           <asp:TemplateColumn HeaderText="Descripcion">
		        <HeaderStyle HorizontalAlign="Center" Width="300px" VerticalAlign="Middle"></HeaderStyle>
		        <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'></asp:Label>
		        </ItemTemplate>
		       </asp:TemplateColumn>

           <%--Cuenta de gasto--%>
           <asp:TemplateColumn HeaderText="Cta Gasto">
		        <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
		        <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblCtagasto" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
		         <asp:Label id="lblDesCtaGasto" runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.DesCtaGasto") %>'></asp:Label>
            </ItemTemplate>
		       </asp:TemplateColumn>
			 
           <%--Activo - CTC--%>
           <asp:TemplateColumn HeaderText="Act. Fijo CTC">
		        <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
		        <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblActivoFijo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>
             <asp:Label id="lblDesActivo" runat="server" visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.DesActivo") %>'></asp:Label>
		        </ItemTemplate>
		       </asp:TemplateColumn>

           <%--Cantidad--%>
		       <asp:TemplateColumn HeaderText="Cantidad Solicitada">
		        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
		        <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEDI") %>'></asp:Label>
		        </ItemTemplate>
		       </asp:TemplateColumn>
			     
           <%--U.M.--%>
           <asp:TemplateColumn HeaderText="U.M.">
		        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
		        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
		        <ItemTemplate>
             <asp:Label id="lblUnidaMedida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>'></asp:Label>
            </ItemTemplate>
		       </asp:TemplateColumn>
			     
           <%--Precio--%>
           <asp:TemplateColumn HeaderText="Precio">
		        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
		        <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
		        <ItemTemplate>
		         <asp:Label id="lblPrecio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PE_ITEM") %>'></asp:Label>
            </ItemTemplate>
           </asp:TemplateColumn>
			
           <%--Subtotal--%>
           <asp:BoundColumn DataField="SubTotal" HeaderText="Monto (S/.)" DataFormatString="{0:#,##0.00}">
		        <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
		        <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
		       </asp:BoundColumn>
			
           <%--edita--%>
           <asp:TemplateColumn>
		        <HeaderStyle Width="3%"></HeaderStyle>
		        <ItemStyle HorizontalAlign="Center"></ItemStyle>
		        <ItemTemplate>
		         <asp:ImageButton id="btnEditarItem" runat="server" ImageUrl="../../intranet/Imagenes/editar.gif" CommandName="Editar"></asp:ImageButton>
		        </ItemTemplate>
		       </asp:TemplateColumn>
			     
           <%--elimina--%>
           <asp:TemplateColumn>
		        <HeaderStyle Width="3%"></HeaderStyle>
		        <ItemTemplate>
		         <asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/eliminar.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CO_ITEM")%>'></asp:ImageButton>
		        </ItemTemplate>
		       </asp:TemplateColumn>
		      </Columns>
	       </asp:datagrid>
        </td>
	     </tr>
	     
       <tr>
	      <td style="WIDTH: 800px; HEIGHT: 20px" align="right">
         <asp:label id="lblMonto" Runat="server" Height="20px">Total (S/.):</asp:label>
         <asp:label id="lblTotalPedido" Runat="server" Font-Bold="true" Height="20px"></asp:label>
        </td>
	     </tr>
	    </table>
      <%--Fin: grilla--%>

    <%-- </ContentTemplate>
      <Triggers> 
       <asp:AsyncPostBackTrigger ControlID ="btnBuscar" EventName="click" />
      </Triggers>
     </asp:UpdatePanel>--%>
   
     <%--Inicio: botonera--%>
     <table style="WIDTH:800px" cellspacing="0" cellpadding="0" border="0">
	    <tr>
       <td style="WIDTH:80px" align="right"></td>
	     <td style="WIDTH:120px" align="right">
        <asp:button id="btnNuevo" Runat="server" CssClass="btnAzul" Text="Nuevo" ToolTip="Nuevo Pedido"></asp:button>
       </td>
       <td style="WIDTH:120px" align="right">
        <asp:button id="btnBuscar" runat="server" CssClass="btnAzul" Text="Buscar"></asp:button>
       </td>
       <td style="WIDTH:120px" align="right">
        <asp:button id="btnSolicitaAprobacion" runat="server" CssClass="btnAzul" Text="Solic. Aprobacion"></asp:button>
       </td>
       <td style="WIDTH:120px" align="right">
        <asp:button id="btnVerSeguimiento" runat="server" CssClass="btnAzul" Text="Ver Seguimiento" ></asp:button>
       </td>
       <td style="WIDTH:120px" align="right">
        <asp:button id="btnAnular" Runat="server" CssClass="btnAzul" Text="Anular" ToolTip="Anular Pedido"></asp:button>
       </td>
       <td style="WIDTH:120px" align="right">
        <input class="btnAzul" id="btnSalir" onclick="javascript:fnc_Cerrar();" type="button" value="Salir" name="btnSalir" runat="server"/>
       </td>
	    </tr>
	   </table>
     <%--Fin: botonera--%>

     <%--Inicio: parametros ocultos --%>
     <table style="WIDTH: 800px">
      <tr>
	     <td style="WIDTH: 800px">
        <asp:textbox id="txtAcepta" BorderStyle="None" runat="server" BackColor="#CDE0EF" Font-Size="8px" width="20px" ForeColor="#CDE0EF"></asp:textbox>
        <asp:textbox id="txtSituacion" BorderStyle="None" runat="server" BackColor="#CDE0EF" Font-Size="8px" width="20px" ForeColor="#CDE0EF"></asp:textbox>
        <asp:textbox id="txtCanX" BorderStyle="None" runat="server" BackColor="#CDE0EF" Font-Size="8px" width="20px" ForeColor="#CDE0EF">0.00</asp:textbox>
        <asp:textbox id="txtCodigo" BorderStyle="None" Runat="server" BackColor="#CDE0EF" Font-Size="8px" width="80px" ForeColor="#CDE0EF"></asp:textbox>
       </td>
      </tr>
     </table>

     <%--<asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
       <ProgressTemplate>     
        <div id="divLoad" class="CssLoadNormal">
         <br />
         <img src="../images/Loading.gif" style="vertical-align:middle; height: 20px; width:20px" alt="" />
         <br/>Procesando...
        </div>
       </ProgressTemplate> 
      </asp:UpdateProgress>--%>
     <%--Fin: parametros ocultos --%>
    </center>   
   </form>
  </body>
</html>