<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarPedidoHilos.aspx.vb"
  Inherits="intranet_logi.frm_RegistrarPedidoHilos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Registro de pedido de hilos</title>
  <link href="../css/sytle.css" type="text/css" rel="stylesheet" />
  <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
  <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
  <%--<link href="../css/NM0001.css" type="text/css" rel="stylesheet" />--%>    
 <%-- <script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>--%>
    
  <script language="javascript" type="text/javascript">

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
      //cantidad
      if (document.all('txtCantidad').value == "" || document.all('txtCantidad').value == 0) {
        alert("Debe ingresar la cantidad para el articulo.");
        document.all('txtCantidad').focus();
        return 0
      }
      return 1
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
        //  var x = document.all('txtCodAlmacen').value;
        var x = document.getElementById("txtCodAlmacen").value;
        var lstrAlmacen = x.substring(0, 3);
        
        if (x != "") {
            if (x == "015") {
                var retorno = window.showModalDialog("../Buscadores/frmBusqueda.aspx?TipoBusqueda=DesperdiciosAlgodon", "dialogHeight:420px;dialogWidth:450px;center:yes;help:no;");
                if (retorno != "" && retorno != ":") {
                    var datos = retorno.split(":");
                    var Codigo = datos[0];
                    var Nombre = datos[1];
                    var Titulo = "";
                    var Peso = "";                    
                    var Stock = "Stock: " + parseFloat(datos[2]).toFixed(2);
                }
                document.getElementById("lblTitulo_Cantidad").innerHTML = "Cant. Kilos:";

            } else {
                var lpstrTipo = 9;
                var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaHilos.aspx?pstrAlmacen=" + lstrAlmacen + "&pstrTipo=" + lpstrTipo, "", "dialogheight:500px;dialogwidth:800px;center:yes;help:no;");
                if (retorno != "" && retorno != ":") {
                    var datos = retorno.split(":");
                    var Codigo = datos[0];
                    var Nombre = datos[1];
                    var Titulo = "Tit.: " + parseFloat(datos[2]).toFixed(2);
                    var Peso = "Peso: " + parseFloat(datos[3]).toFixed(2);
                    var Stock = "Stock: " + parseFloat(datos[4]).toFixed(2);
                }
                document.getElementById("lblTitulo_Cantidad").innerHTML = "Cant. Conos:";
            }
            document.getElementById("txtCodArticulo").value = Codigo;
            document.getElementById("lblDesArticulo").value = Nombre;
            document.getElementById("lblTitulo").value = Titulo
            document.getElementById("lblPeso").value = Peso
            document.getElementById("lblStock").value = Stock
      }
      else {

        alert("Debe elegir un almacen para oonsultar Articulos");
        document.all('txtCodAlmacen').focus();
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

    //Solicitar Aprobacion
    function SolicitarAprobacion() {
      var retorno = window.showModalDialog("../../intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
      if (retorno != "" && retorno != ":") {
        var datos = retorno.split(":");
        var Codigo = datos[0];
        document.all('txtAcepta').value = Codigo;
      }
      return
    }

    // formatear el codigo de pedido
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

    function fMostrarReporte(strUrl) {
      var intwidth = screen.width;
      var intheight = screen.height;
      window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
    }
  </script>
    <style type="text/css">
        .style1
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 100px;
            height: 21px;
        }
        .style2
        {
            width: 120px;
            height: 21px;
        }
        .style3
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 550px;
            height: 21px;
        }
        .style4
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 130px;
            height: 26px;
        }
        .style5
        {
            width: 120px;
            height: 26px;
        }
        .style6
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 550px;
            height: 26px;
        }
        .style7
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 130px;
            height: 22px;
        }
        .style8
        {
            width: 120px;
            height: 22px;
        }
        .style9
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 550px;
            height: 22px;
        }
        .cbo
        {}
    </style>
</head>
<body>
  <form id="frm_RegistrarPedidoHilo" runat="server">
  <center>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
      <table style="width: 800px" cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td class="Cabecera" style="width: 800px; height: 30px" align="center">
            Registro pedido de hilos a almacen
          </td>
        </tr>
      </table>
    </div>
    <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
      <ContentTemplate>
        <div>
          <table style="width: 800px">
            <%--Tipo de vale--%>
            <tr>
              <td class="Descripcion" style="width: 130px" align="left">
                Tipo:
              </td>
              <td style="width: 120px" align="left">
                &nbsp;<asp:RadioButton ID="rdbVale" runat="server" BackColor="#CDE0EF" Text="Vale de Salida"
                  Checked="true" AutoPostBack="true"></asp:RadioButton>
              </td>
              <td class="Descripcion" style="width: 380px" align="right" colspan="3">
                Situacion:
              </td>
              <td style="width: 170px" align="left">
                <asp:TextBox ID="txtEstado" runat="server" CssClass="txtDeshabilitado2" Font-Size="9px"
                  Width="120px"></asp:TextBox>
              </td>
            </tr>
            <%--Fechas: pedido, aprobacion y atencion--%>
            <tr>
              <td class="Descripcion" style="width: 130px" align="left">
                Fec. Pedido:
              </td>
              <td style="width: 120px" align="left">
                <asp:TextBox ID="txtFechaPedido" runat="server" Text="" Width="120px" Font-Size="9px"
                  CssClass="txtDeshabilitado2"></asp:TextBox>
              </td>
              <td class="Descripcion" style="width: 130px" align="left">
                Fec. Aprob.:
              </td>
              <td style="width: 120px" align="left">
                <asp:TextBox ID="txtFechaAprobacion" runat="server" Text="" Width="120px" Font-Size="9px"
                  CssClass="txtDeshabilitado2"></asp:TextBox>
              </td>
              <td class="Descripcion" style="width: 130px" align="left">
                Fec. Aten.:
              </td>
              <td style="width: 170px" align="left">
                <asp:TextBox ID="txtFechaAtencion" runat="server" Text="" Width="120px" Font-Size="9px"
                  CssClass="txtDeshabilitado2"></asp:TextBox>
              </td>
            </tr>
            <%--Numero de pedido--%>
            <tr>
              <td class="Descripcion" style="width: 130px" align="left">
                Num. Pedido:
              </td>
              <td style="width: 120px" align="left">
                <asp:TextBox ID="txtSeriePedido" runat="server" CssClass="txtDeshabilitado2" Font-Size="9px"
                  Width="30px"></asp:TextBox>
                &nbsp;<asp:TextBox ID="txtNumeroPedido" runat="server" CssClass="txtHabilitado" Font-Size="9px"
                  Width="75px"></asp:TextBox>
              </td>
              <td style="width: 550px" align="left" colspan="5" class="Descripcion">
                &nbsp;
              </td>
            </tr>
            <%--solictante--%>
            <tr>
              <td class="Descripcion" style="width: 100px" align="left">
                Solicitante:
              </td>
              <td style="width: 120px" align="left">
                <asp:TextBox ID="txtCodSolicitante" runat="server" Width="90px" Font-Size="9px" MaxLength="20"
                  CssClass="txtHabilitado"></asp:TextBox>
                &nbsp;<input id="btnSolicitante" style="width: 20px;" onclick="javascript:fnc_Buscartrabajadores();"
                  type="button" value="..." name="btnSolicitante" class="Boton" />
              </td>
              <td style="width: 550px" align="left" colspan="5" class="Descripcion">
                <asp:TextBox ID="lblDesSolicitante" BorderStyle="None" runat="server" Font-Size="9px"
                  Width="550px" class="Descripcion"></asp:TextBox>
              </td>
            </tr>
            <%--almacen--%>
            <tr>
              <td class="style4" align="left">
                Almacen:
              </td>
              <td align="left" class="style5">
                <asp:TextBox ID="txtCodAlmacen" runat="server" CssClass="txtHabilitado" Width="90px"></asp:TextBox>
                &nbsp;<input id="btnAlmacen" style="width: 20px; height: 20px" onclick="javascript:BuscarAlmacenes();"
                  type="button" value="..." name="btnAlmacen" class="Boton" />
              </td>
              <td align="left" colspan="5" class="style6">
                <asp:TextBox ID="lblDesAlmacen" BorderStyle="None" runat="server" Font-Size="9px"
                  Width="550px" class="Descripcion"></asp:TextBox>
              </td>
            </tr>
            <%--centro costos--%>
            <tr>
              <td class="style1" align="left">
                Centro Cost.:
              </td>
              <td align="left" class="style2">
                &nbsp;
                  <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="cbo" Font-Size="10px"
                  Width="120px">
                      <asp:ListItem Selected="True" Value="8030102">Urdido Denim</asp:ListItem>
                      <asp:ListItem Value="8040202">Urdido Crudo</asp:ListItem>
                      <asp:ListItem Value="8040301">Adm.Telares</asp:ListItem>
                      <asp:ListItem Value="8020402">Recubridora</asp:ListItem>
                      <asp:ListItem Value="8020703">Cont. Ring Marzoli</asp:ListItem>
                      <asp:ListItem Value="8020602">Apert. Carda O.E.</asp:ListItem>
                      <asp:ListItem Value="8020701">Apert. Carda Ring</asp:ListItem>
                  </asp:DropDownList>
              </td>
              <td align="left" colspan="5" class="style3">
                <asp:TextBox ID="lblDesCentroCostos" runat="server" BorderStyle="None" Font-Size="9px"
                  Width="550px" class="Descripcion"></asp:TextBox>
              </td>
            </tr>
            <%--fecha prioridad--%>
            <tr>
              <td class="style7" align="left">
                Fec. Instal.:
              </td>
              <td align="left" class="style8">
                <asp:TextBox ID="txtFecInstal" runat="server" CssClass="txtHabilitado" Width="90px"
                  MaxLength="10"></asp:TextBox>
                &nbsp;<img onclick="popUpCalendar(this, frm_RegistrarPedidoHilo.txtFecInstal, 'dd/mm/yyyy')"
                  border="0" alt="Seleccionar fecha de instalacion" src="../../intranet/Imagenes/Calendario.gif"
                  width="16" height="16" />
              </td>
              <td align="left" colspan="5" class="style9">
                &nbsp;
              </td>
            </tr>
            <%--prioridad--%>
            <tr>
              <td class="Descripcion" style="width: 130px" align="left">
                Prioridad:
              </td>
              <td style="width: 120px" align="left">
                <asp:DropDownList ID="cboPrioridad" runat="server" CssClass="cbo" Font-Size="9px"
                  Width="120px">
                  <asp:ListItem Value="NOR">Normal</asp:ListItem>
                  <asp:ListItem Value="URG">Urgente</asp:ListItem>
                </asp:DropDownList>
              </td>
              <td style="width: 550px" align="left" colspan="1" class="Descripcion">
                &nbsp;
                <asp:DropDownList ID="ddlTurno" runat="server" CssClass="cbo" Font-Size="9px" Width="100px">
                  <asp:ListItem Value="11">11:00 horas</asp:ListItem>
                  <asp:ListItem Value="13">13:00 horas</asp:ListItem>
                  <asp:ListItem Value="19">19:00 horas</asp:ListItem>
                  <asp:ListItem Value="23">23:00 horas</asp:ListItem>
				  <asp:ListItem Value="3">03:00 horas</asp:ListItem>
                </asp:DropDownList>
              </td>
              <td colspan="1" class="Descripcion">
                Lugar de Entrega:
              </td>
              <td colspan="3" class="Descripcion">
                <asp:DropDownList ID="ddlLugarEntrega" runat="server" CssClass="cbo" 
                      Font-Size="9px" Width="160px" Height="19px">
                  <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                  <asp:ListItem Value="REC">Recubridora</asp:ListItem>
                  <asp:ListItem Value="TRT">Tramas Telares</asp:ListItem>
                  <asp:ListItem Value="URB">Urdidora Benninger</asp:ListItem>
                  <asp:ListItem Value="URH">Urdidora Hacoba</asp:ListItem>
<%--                  <asp:ListItem Value="URD">Urdidora Denim</asp:ListItem>--%>
                  <asp:ListItem Value="URC">Urdido Crudo</asp:ListItem>
                  <asp:ListItem Value="HIL">Hilandería</asp:ListItem>
                  <asp:ListItem Value="PIN">Pinzadora</asp:ListItem>
                  <asp:ListItem Value="PRE">Zona de Prensa</asp:ListItem>
                  <asp:ListItem Value="OTR">Otros</asp:ListItem>
                </asp:DropDownList>
              </td>
            </tr>
            <%--observacion--%>
            <tr>
              <td class="Descripcion" style="width: 120px" align="left">
                Observacion:
              </td>
              <td style="width: 680px" colspan="6" align="left" class="Descripcion">
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="txtAreaHabilitado" Height="30px"
                  Font-Size="9px" Width="450px" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                <br />
                &nbsp;<asp:Label ID="lblNumeroCaracteres" runat="server" Text="*Maximo 200 caracteres"
                  Font-Size="8px" Font-Italic="true" BackColor="Cyan" ForeColor="Black"></asp:Label>
              </td>
            </tr>
          </table>
          <%--Inicio: Error--%>
          <table style="width: 800px">
            <tr>
              <td align="left">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="mensaje"></asp:Label>
              </td>
            </tr>
          </table>
          <%--Fin: Error--%>
          <%--Inicio: datos Articulo--%>
          <table style="width: 800px">
            <tr>
              <td style="width: 800px" align="left">
                <asp:Panel ID="pnlArticulo" runat="server" Width="800px">
                  <table style="width: 800px">
                    <tr>
                      <td class="Descripcion" style="width: 500px" align="left">
                        Articulo:
                      </td>
                      <td class="Descripcion" style="width: 100px" align="left">
                        Activo:
                      </td>
                      <td class="Descripcion" style="width: 100px" align="left">
                        Obs:
                      </td>
                      <td class="Descripcion" style="width: 70px" align="left">                        
                        <asp:Label ID="lblTitulo_Cantidad" runat="server" Text="Label">Cant. Conos:</asp:Label>                        
                      </td>
                      <td class="Descripcion" style="width: 30px" align="left">
                        &nbsp;
                      </td>
                    </tr>
                    <%--controles de cabecera--%>
                    <tr>
                      <td class="Descripcion" style="width: 500px">
                        <asp:TextBox ID="txtCodArticulo" runat="server" Font-Size="9px" Width="200px" MaxLength="20"
                          CssClass="txtHabilitado"></asp:TextBox>
                        &nbsp;<input id="btnArticulos" style="width: 20px" onclick="javascript:fnc_BuscarArticulos();"
                          type="button" value="..." name="btnArticulos" class="Boton" />
                        &nbsp;<asp:TextBox ID="lblDesArticulo" BorderStyle="None" runat="server" Font-Size="8px"
                          Width="220px" BackColor="#BCCAE0"></asp:TextBox>
                      </td>
                      <td class="Descripcion" style="width: 100px">
                        <asp:TextBox ID="txtCodOrdenServicio" runat="server" Width="60px" Font-Size="9px"
                          MaxLength="20" CssClass="txtHabilitado"></asp:TextBox>
                        &nbsp;<input id="btnServicio" style="width: 20px" onclick="javascript:BuscarOrdenServicio();"
                          type="button" value="..." name="btnServicio" class="Boton" />
                      </td>
                      <td class="Descripcion" style="width: 100px">
                        <asp:TextBox ID="txtObsDet" runat="server" Width="100px" Font-Size="9px" MaxLength="20"
                          CssClass="txtHabilitado"></asp:TextBox>
                      </td>
                      <td class="Descripcion" style="width: 70px">
                        <asp:TextBox ID="txtCanConos" runat="server" Font-Bold="true" Text="0.00" Font-Size="9px"
                          Width="60px" CssClass="txtHabilitado">0.00</asp:TextBox>
                      </td>
                      <td class="Descripcion" style="width: 30px">
                        <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="../../intranet/Imagenes/save_16.png"
                          ToolTip="Guardar Registro en el  Pedido" style="height: 16px"></asp:ImageButton>
                      </td>
                    </tr>
                    <tr>
                      <td class="Descripcion" style="width: 500px">
                        <asp:TextBox ID="lblUniMedida" BorderStyle="None" runat="server" Font-Size="8px"
                          Width="100px" BackColor="#BCCAE0"></asp:TextBox>
                        <asp:TextBox ID="lblTitulo" BorderStyle="None" runat="server" Font-Size="9px" Width="100px"
                          BackColor="#BCCAE0"></asp:TextBox>
                        <asp:TextBox ID="lblPeso" BorderStyle="None" runat="server" Font-Size="9px" Width="100px"
                          BackColor="#BCCAE0"></asp:TextBox>
                        <asp:TextBox ID="lblStock" BorderStyle="None" runat="server" Font-Size="9px" Width="100px"
                          BackColor="#BCCAE0"></asp:TextBox>
                      </td>
                      <td class="Descripcion" style="width: 300px" colspan="4">
                        <asp:TextBox ID="lblDesServicio" BorderStyle="None" runat="server" CssClass="Descripcion"
                          Width="250px" Font-Size="8px"></asp:TextBox>
                      </td>
                    </tr>
                  </table>
                </asp:Panel>
              </td>
            </tr>
          </table>
          <%--Fin: datos Articulo--%>
          <%--Inicio: grilla--%>
          <table style="width: 800px">
            <tr>
              <td align="right">
                <asp:Label ID="lblItems" runat="server" Text="Numero de Items" CssClass="contador"></asp:Label>
              </td>
            </tr>
            <tr>
              <td>
                <asp:DataGrid ID="dgDetallePedido" runat="server" Width="800px" AutoGenerateColumns="False">
                  <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                  <ItemStyle CssClass="GridItem"></ItemStyle>
                  <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                  <Columns>
                    <%--Secuencia--%>
                    <asp:TemplateColumn HeaderText="Sec.">
                      <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_SECU") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Codigo--%>
                    <asp:TemplateColumn HeaderText="Codigo">
                      <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Descripcion--%>
                    <asp:TemplateColumn HeaderText="Descripcion">
                      <HeaderStyle HorizontalAlign="Center" Width="300px" VerticalAlign="Middle"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Activo--%>
                    <asp:TemplateColumn HeaderText="Act. Fijo">
                      <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblActivoFijo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>
                        <asp:Label ID="lblDesActivo" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DesActivo") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--titulo--%>
                    <asp:TemplateColumn HeaderText="Titulo">
                      <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblTitulo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_TituloReal") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--peso--%>
                    <asp:TemplateColumn HeaderText="Peso">
                      <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblPeso" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_PesoHilo") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Cantidad--%>
                    <asp:TemplateColumn HeaderText="Cantidad">
                      <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                      <ItemTemplate>
                        <asp:Label ID="lblCanConos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ca_pedi") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--Stock--%>
                    <asp:TemplateColumn HeaderText="Stock">
                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                        <ItemStyle Font-Size="8px" HorizontalAlign="Right" Width="50px"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStock" runat="server" Text='<%# String.Format("{0:N}", Container.DataItem("Stock"))  %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--obs--%>
                    <asp:BoundColumn DataField="Obs" HeaderText="Obs">
                      <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundColumn>
                    <%--edita--%>
                    <asp:TemplateColumn>
                      <HeaderStyle Width="3%"></HeaderStyle>
                      <ItemStyle HorizontalAlign="Center"></ItemStyle>
                      <ItemTemplate>
                        <asp:ImageButton ID="btnEditarItem" runat="server" ImageUrl="../../intranet/Imagenes/editar.gif"
                          CommandName="Editar"></asp:ImageButton>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <%--elimina--%>
                    <asp:TemplateColumn>
                      <HeaderStyle Width="3%"></HeaderStyle>
                      <ItemTemplate>
                        <asp:ImageButton ID="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/eliminar.gif"
                          CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CO_ITEM")%>'>
                        </asp:ImageButton>
                      </ItemTemplate>
                    </asp:TemplateColumn>
                  </Columns>
                </asp:DataGrid>
              </td>
            </tr>
          </table>
          <%--Fin: grilla--%>
          <%--Inicio: botonera--%>
          <table style="width: 800px; height: 20px;" cellspacing="0" cellpadding="0" border="0">
            <tr>
              <td style="width: 320px" align="right">
              </td>
              <td style="width: 120px" align="right">
                <asp:Button ID="btnNuevo" runat="server" CssClass="btnAzul" Text="Nuevo" ToolTip="Nuevo Pedido">
                </asp:Button>
              </td>
              <td style="width: 120px" align="right">
                <asp:Button ID="btnBuscar" runat="server" CssClass="btnAzul" Text="Buscar"></asp:Button>
              </td>
              <td style="width: 120px" align="right">
                <asp:Button ID="btnVisualizar" runat="server" CssClass="btnAzul" Text="Vista previa">
                </asp:Button>
              </td>
              <td style="width: 120px" align="right">
                <asp:Button ID="btnSolicitaAprobacion" runat="server" CssClass="btnVerde" Text="Aprobar">
                </asp:Button>
              </td>
              <td style="width: 120px" align="right">
                <asp:Button ID="btnAnular" runat="server" CssClass="btnRojo" Text="Anular" ToolTip="Anular Pedido">
                </asp:Button>
              </td>
            </tr>
          </table>
          <%--fin: botonera--%>
          <%--Inicio: parametros ocultos --%>
          <table style="width: 800px">
            <tr>
              <td style="width: 800px">
                <asp:TextBox ID="txtAcepta" BorderStyle="None" runat="server" BackColor="#CDE0EF"
                  Font-Size="8px" Width="20px" ForeColor="#CDE0EF"></asp:TextBox>
                <asp:TextBox ID="txtSituacion" BorderStyle="None" runat="server" BackColor="#CDE0EF"
                  Font-Size="8px" Width="20px" ForeColor="#CDE0EF"></asp:TextBox>
                <asp:TextBox ID="txtCanX" BorderStyle="None" runat="server" BackColor="#CDE0EF" Font-Size="8px"
                  Width="20px" ForeColor="#CDE0EF">0.00</asp:TextBox>
                <asp:TextBox ID="txtCodigo" BorderStyle="None" runat="server" BackColor="#CDE0EF"
                  Font-Size="8px" Width="80px" ForeColor="#CDE0EF"></asp:TextBox>
              </td>
            </tr>
          </table>
        </div>
      </ContentTemplate>
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="click" />
      </Triggers>
    </asp:UpdatePanel>
  </center>
  </form>
</body>
</html>
