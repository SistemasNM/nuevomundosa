<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Reporte_Pedidos_Ventas.aspx.vb" Inherits="intranet_logi.frm_Reporte_Pedidos_Ventas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
     <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
     <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
     <link href="../css/EstilosWeb.css" rel="stylesheet" type="text/css" />
     <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <center>
             <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	            <tr>
	             <td class="Cabecera" style="width: 800px; height: 30px" align="center">Pedidos por Clientes - Ventas</td>
	            </tr>
	           </table>
               <table style="width: 800px">
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Serie - Nro Pedido:</td>
                    <td align="left" class="style1">
                        <%-- <asp:TextBox ID="txtSerie" runat="server" class="input" Width="30px" ></asp:TextBox>--%>
                         <asp:DropDownList ID="ddserie" runat="server">
                            <asp:ListItem Value="0001">0001</asp:ListItem>
                            <asp:ListItem Value="0006" Selected="true">0006</asp:ListItem>
                         </asp:DropDownList>
                         -
                         <asp:TextBox ID="txtNumero" runat="server" class="input" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="Boton" Width="90px" BackColor="Green" />
                    </td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Cliente:</td>
                    <td align="left" class="style1">
                         <asp:label ID="lblCliente" runat="server" class="input" Width="300px"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Linea credito S/.:</td>
                    <td align="left" class="style1">
                         <asp:label ID="lblLinea" runat="server" class="input" Width="98px"></asp:label>
                    </td>
                     <td class="Descripcion" style="width:130px" align="left">Utilización:</td>
                     <td align="left" class="style1">
                         <asp:label ID="lblPorc" runat="server" class="input" Width="98px"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Fecha Programada:</td>
                    <td align="left" class="style1">
                         <asp:label ID="lblFechaEntrega" runat="server" class="input" Width="98px"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Lugar de Entrega:</td>
                    <td align="left" class="style1">
                         <asp:label ID="lblLugar" runat="server" class="input" Width="300px"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Status de Pedido:</td>
                    <td align="left" class="style1">
                         <asp:label ID="lblstatus" runat="server" class="input" Width="150px" BackColor="GreenYellow" ForeColor="Black" Font-Bold="true"></asp:label>
                    </td>
                </tr>
               </table>
               <table>
                <tr>
                    <td></td>
                </tr>
               </table>
               <table>
               <tr>
                <td><center><h1 style="color:black;background-color:Green">DETALLE DE PEDIDO</h1></center></td>
               </tr>
               
                <tr>
                    <td>
                         <asp:GridView ID="grdReporVenta" runat="server" AutoGenerateColumns="false" Width="800PX" Height="200px">
                             <AlternatingRowStyle CssClass="GridAltItem"/>
                            <RowStyle CssClass="GridAltItemAlert2"/>
                             <HeaderStyle CssClass="GridHeader"/>
                             <Columns>
                                <asp:TemplateField HeaderText="Artículo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArt" runat="server"  Text='<%#Container.DataItem("CO_ITEM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Desc. Artículo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescArt" runat="server"  Text='<%#Container.DataItem("DE_ITEM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Metraje Pedido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMetrajePed" runat="server" Text='<%# String.Format("{0:#,##0.##}", Container.DataItem("CA_PEDI"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Metraje Programado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMetrajePro" runat="server" Text='<%# String.Format("{0:#,##0.##}", Container.DataItem("CANT_PRO"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Metraje Despachado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMetrajeDes" runat="server" Text='<%# String.Format("{0:#,##0.##}", Container.DataItem("CANT_DESP"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Metraje Pendiente">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMetrajePen" runat="server" Text='<%# String.Format("{0:#,##0.##}", Container.DataItem("CANT_PEN"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                             </Columns>

                         </asp:GridView>
                    </td>
                </tr>
               </table>
               <table>
                <tr><td></td></tr>
                <tr><td></td></tr>
               </table>
               <table>
               <tr>
                <td><center><h1 style="color:black;background-color:Green;">PROGRAMACIÓN DE PEDIDO</h1></center></td>
               </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdSalidaPedido" runat="server" AutoGenerateColumns="false" Width="950PX" Height="200px">
                            <AlternatingRowStyle CssClass="GridAltItem"/>
                            <RowStyle CssClass="GridAltItemAlert2"/>
                             <HeaderStyle CssClass="GridHeader"/>
                                 <Columns>
                                    <asp:TemplateField HeaderText="Nro. Salida">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalida" runat="server"  Text='<%#Container.DataItem("SALIDA")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px"  HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cod. Artículo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodArt" runat="server"  Text='<%#Container.DataItem("ARTICULO")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px"  HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desc. Artículo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescArt" runat="server"  Text='<%#Container.DataItem("DE_ITEM")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Metraje Programado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantPro" runat="server"  Text='<%# String.Format("{0:#,##0.##}",Container.DataItem("CANT_PROGRAMADA"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px"  HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nro. Guia">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNroGuia" runat="server"  Text='<%#Container.DataItem("NU_GUIA")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="90px"  HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado Salida">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstSal" runat="server"  Text='<%#Container.DataItem("ESTO_GUIA")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px"  HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Metraje Despachado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantDes" runat="server"  Text='<%# String.Format("{0:#,##0.##}",Container.DataItem("CANT_DESPACHADA"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Programada">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecPro" runat="server"  Text='<%#Container.DataItem("FECHA_PROGRAMADA")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px"/>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
               </table>
        </center>
    </form>
</body>
</html>
