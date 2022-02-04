<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptResultadoInventarioOtros.aspx.vb" Inherits="intranet_logi.frm_RptREsultadoInventarioOtros" %>
<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
 <title>INVENTARIO DE REPUESTOS Y OTROS</title>
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
     
 </script>
</head>
<body>
 <form id="frmData" runat="server" method="post" >
  <center>
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   <table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width:700px; height:30px">
    <tr>
     <td style="width:700px" class="Cabecera">RESULTADO DE INVENTARIOS</td>
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
       <td style="width:150px" align="left" class="Etiqueta">Codigo Álmacen:</td>
       <td align="left" style="width:100px">
        <asp:DropDownList ID="ddlAlmacenes" runat="server" AutoPostBack="false" 
               CssClass="input" Height="20px" Width="243px" Font-Size="10px">
        </asp:DropDownList>
       </td>
       
      </tr>
      <tr>
       <td style="width:150px" align="left" class="Etiqueta">Fecha de Inventario:</td>
       <td align="left" style="width:100px">
           <asp:DropDownList ID="ddlFechas" runat="server" AutoPostBack="false" 
               CssClass="input" Font-Size="10px" Height="19px" Width="108px">
           </asp:DropDownList>
       </td>
       <td align="left" style="width:350px">
           <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
               ImageUrl="../Images/Buscar.gif" style="cursor:hand;" 
               ToolTip="Buscar" Width="30px" />
       </td>
      </tr>
         <tr>
             <td align="left" class="Etiqueta" style="width:150px">
                 Tipo del Reporte:</td>
             <td align="left" style="width:100px">
                 <asp:CheckBoxList ID="cblReporte" runat="server" Height="16px" 
                     RepeatDirection="Horizontal" Width="358px">
                     <asp:ListItem Value="1">Faltantes</asp:ListItem>
                     <asp:ListItem Value="2">Sobrantes</asp:ListItem>
                     <asp:ListItem Value="3">Cuadrados</asp:ListItem>
                     <asp:ListItem Value="4">Total</asp:ListItem>
                 </asp:CheckBoxList>
             </td>
             <td align="left" style="width:350px">
                 &nbsp;</td>
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
