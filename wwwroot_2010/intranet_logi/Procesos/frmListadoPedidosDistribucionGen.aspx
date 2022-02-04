<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListadoPedidosDistribucionGen.aspx.vb" Inherits="intranet_logi.frmListadoPedidosDistribucionGen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Artículos de Distribución</title>
 <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
 <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
    <style type="text/css">
        .style5
        {
            width: 723px;
        }
        .style6
        {
            width: 100%;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        
        <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
            style="width:1179px; height:30px">
            <tr>
                <td style="Font-Size:14px;" valign="bottom" align="center" class="style6" >
                    Listado de Pedidos Repartición - Distribución</td>
            </tr>
        </table>
        <table>
            <tr>
                <td><asp:Button
                        ID="btnAprobar" runat="server" Text="Aprobar" CssClass="Boton" Height="30px" 
                        Width="150px" Font-Size="16px"/></td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="5" 
                style="height: 200px; width: 1293px">
            <tr>
                <td class="style5">
                    
                    <asp:DataGrid runat="server" ID="grdListArtDistG" Width="1178px" 
                        AutoGenerateColumns="false">
                     <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		            <ItemStyle CssClass="GridItem"></ItemStyle>
		            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <Columns>
                               
                                <asp:BoundColumn DataField="NU_PEDIDO" HeaderText="#Pedido">
                                  <HeaderStyle HorizontalAlign="Center" Width="130px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                

                                <asp:BoundColumn DataField="NO_CLIE" HeaderText="CLIENTE">
                                  <HeaderStyle HorizontalAlign="Center" Width="260px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                               

                                <asp:BoundColumn DataField="FE_DESP" HeaderText="Fecha Despacho" DataFormatString="{0:d}">
                                  <HeaderStyle HorizontalAlign="Center" Width="85px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                <asp:BoundColumn DataField="LUG_ENTREGA" HeaderText="Lugar Entrega">
                                  <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                 <asp:BoundColumn DataField="ARTICULO" HeaderText="Articulo">
                                  <HeaderStyle HorizontalAlign="Center" Width="430px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                <asp:BoundColumn DataField="CA_REP" HeaderText="Mts. x Repartir" DataFormatString="{0:N2}">
                                  <HeaderStyle HorizontalAlign="Center" Width="75px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                 <asp:TemplateColumn >
                                     <ItemStyle Width="10px" />
                                     <ItemTemplate>
                                         <asp:ImageButton ID="imgBEliminar" runat="server" ImageUrl="~/Imagenes/Delete.png" 
                                             Width="25px" Height="25px" CommandArgument='<%# Eval("DATOS") %>' 
                                             onclick="imgBEliminar_Click"/>
                                     </ItemTemplate>
                                 </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
