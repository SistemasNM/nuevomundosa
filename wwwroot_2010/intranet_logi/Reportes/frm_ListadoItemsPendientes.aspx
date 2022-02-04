<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ListadoItemsPendientes.aspx.vb" Inherits="intranet_logi.frm_ListadoItemsPendientes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
  <title>Nuevo Mundo - Listado de Articulos por Atender</title>
  <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
  <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
  <meta name="vs_defaultClientScript" content="JavaScript"/>
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
  <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
  <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
  <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
 <%-- <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>--%>
     <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
  <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
  <script language="javascript" type="text/javascript">

      // Ver reporte
      function fMostrarReporte(strUrl) {
          var intwidth = screen.width;
          var intheight = screen.height;
          window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
      }

     // Funcion Busca Centro de Costos
     function fnc_BuscarCentroCosto() {
         var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCodCentroCosto').value = Codigo;
             document.all('txtDesCentroCosto').value = Nombre;
         }
     }

     // Muestra el listado de los Activos fijos y las ordenes de trabajo
     function BuscarOrdenServicio() {
         var strCentroCostos = ""
         var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaActivo.aspx?strCentroCostos=" + strCentroCostos, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCodActivo').value = Codigo;
             document.all('txtDesActivo').value = Nombre;
         }
     }
     
     // Numero de pedido
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
 <center>
  <form id="frmListadoItemsPendientes" method="post" runat="server">
   <table border="0" cellspacing="0" cellpadding="0" style="width:800px">
    <tr>
	 <td class="Cabecera" style="width:800px; height:30px" align="center">&nbsp;LISTADO DE ARTICULOS POR ATENDER</td>
    </tr>
    <tr><td></td></tr>
   </table>

   <table border="0" cellspacing="0" cellpadding="0" style="width:800px">
    <tr>
     <td style="width: 100px; height: 20px" align="left" class="Etiqueta">&nbsp;Tipo Doc:</td>
     <td style="height: 20px" align="left" colspan="2">
      <asp:RadioButtonList runat="server" id="rdlDocumentos" CssClass="cboFormulario" Font-Size="9px" Width="300px" RepeatDirection="Horizontal">
       <asp:ListItem Selected="true" Text="PED" Value="1"></asp:ListItem>
       <asp:ListItem Selected="False" Text="RQS" Value="2"></asp:ListItem>
       <asp:ListItem Selected="False" Text="PEH" Value="3"></asp:ListItem>
       <asp:ListItem Selected="False" Text="PDE" Value="4"></asp:ListItem>
       <asp:ListItem Selected="False" Text="EPP's" Value="5"></asp:ListItem>
      </asp:RadioButtonList>
     </td>
     <td style="width: 150px; height: 20px" align="left"></td>
     <td style="width: 300px; height: 20px" align="center">
      <asp:ImageButton ID="btnBuscar" runat="server" Width="30px" Height="30px" ImageUrl="../../intranet/Imagenes/Buscar.png" ToolTip="Buscar" style="cursor:hand;"/>
     </td>
    </tr>
    <tr>
     <td style="width: 100px; height: 20px" align="left" class="Etiqueta">&nbsp;Fecha Inicio:</td>
	 <td style="width: 150px; height: 20px" align="left">
      <asp:textbox id="txtFechaIni" runat="server" width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>&nbsp;
      <img onclick="popUpCalendar(this, frmListadoItemsPendientes.txtFechaIni, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  />
     </td>
     <td style="width: 100px; height: 20px" align="left" class="Etiqueta">&nbsp;Fecha Fin:</td>
     <td style="width: 150px; height: 20px" align="left">
      <asp:textbox id="txtFechaFin" runat="server" width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>&nbsp;
      <img onclick="popUpCalendar(this, frmListadoItemsPendientes.txtFechaFin, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Final" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"/>
     </td>
     <td style="width: 300px; height: 20px" align="left">
         &nbsp;</td>
    </tr>
   </table>

   <table style="width:800px; height:20px" border="0" cellpadding="0" cellspacing="0">
     <!-- Centro Costo -->
    <tr>
	 <td style="width: 100px; height: 20px" align="left" class="Etiqueta">&nbsp;Area:</td>
	 <td style="width: 150px; height: 20px" align="left">
      <asp:textbox id="txtCodCentroCosto" runat="server" width="100px" CssClass="input" Font-Size= "10px" MaxLength = "7"></asp:textbox>
       &nbsp;<input style="width:20px; height:20px" id="btnCentroCosto" class="boton" onclick="javascript:fnc_BuscarCentroCosto();" value="..." type="button" name="btnCentroCosto" />
     </td>
     <td style="width: 550px; height: 20px" align="left">
      <asp:TextBox Id="txtDesCentroCosto" Text="" runat="server" CssClass="Etiqueta" width="550px" Font-Size="9px" BorderStyle="None"></asp:TextBox>
     </td>
    </tr>
    <!-- Activo/CTC -->
    <tr>
     <td align="left" class="Etiqueta" style="width: 100px; height: 20px">&nbsp;Activo/CTC:</td>
     <td align="left" style="width: 150px; height: 20px">
      <asp:TextBox ID="txtCodActivo" runat="server" width="100px" CssClass="input" Font-Size="10px" MaxLength="6"></asp:TextBox>
      &nbsp;<input id="btnServicio" class="boton" name="btnServicio" onclick="javascript:BuscarOrdenServicio();" style="width: 20px; height: 20px" type="button" value="..."></input></td>
     <td align="left" style="width:550px; height:20px">
      <asp:textbox id="txtDesActivo" Text="" runat="server" CssClass="Etiqueta" width="550px" Font-Size="9px" BorderStyle="None"></asp:textbox>
     </td>
    </tr>
   </table>
   <!-- Numero de documento, Prioridad -->
   <table style="width:800px;height:20px" border="0" cellpadding="0" cellspacing="0">
    <tr>
	 <td style="width: 100px; height:20px" align="left" class="Etiqueta">&nbsp;Num. Doc.:</td>
	 <td style="width: 150px; height:20px" align="left">
      <asp:textbox id="txtSerie" runat="server" width="30px" Font-Size= "10px" MaxLength = "5"  CssClass="input"></asp:textbox>
      &nbsp;<asp:TextBox ID="txtNumeroDocumento" runat="server" width="90px" Font-Size= "10px" MaxLength = "10"></asp:TextBox>
     </td>
     <td style="width:100px; height:20px" align="left"></td>
     <td style="width:150px; height:20px" align="left"></td>
     <td style="width:300px; height:20px" align="left"></td>
    </tr>
    <tr>
	 <td style="width: 100px;" align="left" class="Etiqueta">&nbsp;Prioridad:</td>
     <td style="width: 150px;" align="left">
      <asp:dropdownlist id="ddlPrioridad" runat="server" CssClass="input" Width="120px" Height="16px" AutoPostBack="false">
	   <asp:ListItem Value="" Selected="True">TODOS</asp:ListItem>
	   <asp:ListItem Value="NOR">NORMAL</asp:ListItem>
	   <asp:ListItem Value="URG">URGENTE</asp:ListItem>
	  </asp:dropdownlist>
     </td>
     <td style="width: 100px; height: 20px" align="left" class="Etiqueta">&nbsp;Fecha Inst.:</td>
     <td style="width: 150px; height: 20px" align="left">
      <asp:textbox id="txtFecIns" runat="server" width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>&nbsp;
      <img onclick="popUpCalendar(this, frmListadoItemsPendientes.txtFecIns, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Instalacion" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"/>
     </td>
     <td style="width:300px; height:20px" align="left"></td>
    </tr>
    <tr>
     <td style="width:100px; height:20px" align="left" class="Etiqueta">&nbsp;Stock:</td>
     <td style="width:150px; height:20px" align="left">
      <asp:CheckBox ID="chkStock" Text="" runat="server" Checked="false" CssClass="input" />
     </td>
     <td style="width:100px; height:20px" align="left"></td>
     <td style="width:150px; height:20px" align="left"></td>
     <td style="width:300px; height:20px" align="left"></td>
    </tr>
   </table>
   <table style="width:800px;height:20px" border="0" cellpadding="0" cellspacing="0">
    <tr>
     <td style="width:800px"><asp:Label id="lblError" runat="server" CssClass="error"></asp:Label></td>
    </tr>
   </table>
  </form>
 </center>
</body>
</html>
