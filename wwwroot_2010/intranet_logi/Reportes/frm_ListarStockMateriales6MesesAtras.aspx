<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ListarStockMateriales6MesesAtras.aspx.vb" Inherits="intranet_logi.frm_ListarStockMateriales6MesesAtras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>INVENTARIO DE RESPUESTOS 6 MESES ATRAS</title>
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
    <form id="frmData" runat="server" method="post">
    <center>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
       <table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width:700px; height:30px">
        <tr>
         <td style="width:700px" class="Cabecera">LISTADO DE MATERIALES EN STOCK 6 MESES 
             ATRAS</td>
        </tr>
       </table>
         <!-- **************  Inicio de Contenido ****************** -->          
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
           <td style="width:150px" align="left" class="Etiqueta">Año:</td>
           <td align="left" style="width:100px">
            <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="false" 
                   CssClass="input" Height="20px" Width="81px" Font-Size="10px">
            </asp:DropDownList>
           </td>
       
          </tr>
          <tr>
           <td style="width:150px" align="left" class="Etiqueta">Mes:</td>
           <td align="left" style="width:100px">
               <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="false" 
                   CssClass="input" Font-Size="10px" Height="16px" Width="82px">
                   <asp:ListItem Value="01">ENERO</asp:ListItem>
                   <asp:ListItem Value="02">FEBRERO</asp:ListItem>
                   <asp:ListItem Value="03">MARZO</asp:ListItem>
                   <asp:ListItem Value="04">ABRIL</asp:ListItem>
                   <asp:ListItem Value="05">MAYO</asp:ListItem>
                   <asp:ListItem Value="06">JUNIO</asp:ListItem>
                   <asp:ListItem Value="07">JULIO</asp:ListItem>
                   <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                   <asp:ListItem Value="09">SETIEMBRE</asp:ListItem>
                   <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                   <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                   <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
               </asp:DropDownList>
           </td>
           <td align="left" style="width:350px">
               <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                   ImageUrl="../Images/Buscar.gif" style="cursor:hand;" 
                   ToolTip="Buscar" Width="30px" />
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
          <!-- **************  Fin de tabContenido ****************** -->
    </center>
    </form>
</body>
</html>
