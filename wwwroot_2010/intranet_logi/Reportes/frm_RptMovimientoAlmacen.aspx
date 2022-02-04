<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptMovimientoAlmacen.aspx.vb" Inherits="intranet_logi.frm_RptMovimientoAlmacen" %>
<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
<meta http-equiv="X-UA-Compatible" content="IE=8"/>
 <title>Nuevo Mundo - Movimiento de almacen</title>
 <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
 <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
 
 <script language="javascript" type="text/jscript">
     function fMostrarReporte(strURL) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }
     function FormatearBusqDoc(pTexto) {
         //numero de pedido
         if (pTexto == 2) {
             var lnume = '00000000000' + document.all["txtNumPedidoAlmacen"].value;
             lnume = lnume.substring(lnume.length, lnume.length - 10);
             if (lnume == '0000000000') {
                 document.all["txtNumPedidoAlmacen"].value = '';
             }
             else {
                 document.all["txtNumPedidoAlmacen"].value = lnume;
             }
         }
     }
 </script>
</head>
<body>
 <form id="frmData" runat="server" method="post" >
  <center>
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   <table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width:700px; height:30px">
    <tr>
     <td style="width:700px" class="Cabecera">MOVIMIENTO DE ALMACEN</td>
    </tr>
   </table>
   <asp:UpdatePanel ID="panContenido" runat="server" >
    <ContentTemplate>
     <table border="0" cellpadding="0" cellspacing="0" style="width:700px">
      <tr>
       <td style="width:700px"></td>
      </tr>
     </table>
     <%--filtro fechas --%>
     <table  border="0" cellpadding="0" cellspacing="0" style="width:700px">
      <tr>
       <td style="width:150px" align="left" class="Etiqueta" height="30px">Desde:</td>
	   <td style="width:100px" align="left">
        <ig:WebDatePicker ID="TxtFechaIni" runat="server" Width="100px" Font-Size="10px"></ig:WebDatePicker>
       </td>
       <td style="width:450px" align="center" rowspan="2">
        <asp:ImageButton ID="btnBuscar" runat="server" Width="30px" Height="30px" ImageUrl="../../intranet/Imagenes/Buscar.png" ToolTip="Buscar" OnClick="btnBuscar_Click" style="cursor:hand;"/>
       </td>
      </tr>
      <tr>
       <td style="width:150px" align="left" class="Etiqueta" height="30px">Hasta:</td>
       <td style="width:100px" align="left">
        <ig:WebDatePicker ID="TxtFechaFin" runat="server" Width="100px" Font-Size="10px"></ig:WebDatePicker>
       </td>
      </tr>
      <tr>
       <td style="width:150px" align="left" class="Etiqueta" height="30px">Tipo doc. almacen:</td>
       <td align="left" style="width:100px">
        <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="false" CssClass="input" Height="20px" Width="100px" Font-Size="10px">
         <asp:ListItem Selected="True" Value="ALL">TODOS</asp:ListItem>
         <asp:ListItem Value="VS">VS</asp:ListItem>
		 <asp:ListItem Value="VSP">VSP</asp:ListItem>
         <asp:ListItem Value="VTC">VTC</asp:ListItem>
		 <asp:ListItem Value="DPR">DPR</asp:ListItem>
         <asp:ListItem Value="DTC">DTC</asp:ListItem>
		 <asp:ListItem Value="DVT">DVT</asp:ListItem>
         <asp:ListItem Value="IPR">IPR</asp:ListItem>
		 <asp:ListItem Value="GRR">GRR</asp:ListItem>
        </asp:DropDownList>
       </td>
       <td style="width:350px" align="left">
        <asp:TextBox ID="txtNumDocAlmacen" runat="server" Text="" Width="100px" CssClass="inputCodigo" Font-Size="10px" MaxLength="15"></asp:TextBox>
       </td>
      </tr>
      <tr>
       <td style="width:150px" align="left" class="Etiqueta" height="30px">Numero de Pedido:</td>
       <td align="left" style="width:100px">
        <asp:TextBox ID="txtSeriePedidoAlmacen" runat="server" Text="" Width="90px" CssClass="txtReadOnly" Font-Size="10px"></asp:TextBox>
       </td>
       <td align="left" style="width:350px">
        <asp:TextBox ID="txtNumPedidoAlmacen" runat="server" Text="" Width="100px" CssClass="inputCodigo" Font-Size="10px" MaxLength="10"></asp:TextBox>
       </td>
      </tr>
     </table>

     <%--Numero de documento de almacen--%>
      <table border="0" cellpadding="0" cellspacing="0" style="width:700px">
       <tr>
        <td style="width:700px" align="left">
         <asp:TextBox ID="txtUrl" runat="server" CssClass="txtHid" Width="700px"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td style="width:700px" align="left">
         <asp:Label ID="lblMsg" runat="server" Width="700px" CssClass="error"></asp:Label>
        </td>
       </tr>
      </table>
    </ContentTemplate>
   </asp:UpdatePanel>
  </center>
 </form>
</body>
</html>
