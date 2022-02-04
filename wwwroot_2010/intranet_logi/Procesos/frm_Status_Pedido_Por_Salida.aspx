<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Status_Pedido_Por_Salida.aspx.vb" Inherits="intranet_logi.frm_Status_Pedido_Por_Salida" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
     <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
     <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
     <link href="../css/EstilosWeb.css" rel="stylesheet" type="text/css" />
     <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .style1
        {
            width: 9px;
        }
        .style2
        {
            width: 70px;
            height: 16px;
        }
        .style3
        {
            width: 300px;
            height: 16px;
        }
        .style4
        {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
             <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	            <tr>
	             <td class="Cabecera" style="width: 800px; height: 30px" align="center">Proceso de Status Salida por Pedido - Logistica</td>
	            </tr>
	           </table>
               <table style="width: 400px">
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Nro Salida:</td>
                    <td align="left" class="style1">
                         <asp:TextBox ID="txtSalida" runat="server" class="input" Width="98px"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="Boton" Width="90px" BackColor="Green" />
                    </td>
                    
                </tr>
               </table>
               <table style="width: 600px">
                <tr><td></td></tr>
                <tr><td> <hr /></td></tr>
               </table>
               <table style="width: 600px">
                <tr>
                    <td class="Descripcion" style="width:80px" align="left">Ayudante #1:</td>
                    <td style="width:300px" align="left" >
                        <asp:DropDownList ID="ddlAyudante1" runat="server" CssClass="Input" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:80px" align="left">Ayudante #2:</td>
                    <td style="width:300px" align="left" >
                        <asp:DropDownList ID="ddlAyudante2" runat="server" CssClass="Input" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="Descripcion" style="width:80px" align="left">Ayudante #3:</td>
                    <td style="width:300px" align="left">
                        <asp:DropDownList ID="ddlAyudante3" runat="server" CssClass="Input" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button id="btnMarcar" runat="server" Text="Marcar Todo Entregado" 
                            CssClass="Boton" Width="140px" BackColor="Green" Height="24px" />
                    </td>
                </tr>
               </table>
               <table>
                <tr>                    
                    <td>&nbsp;</td>
               </tr>
               </table>
               <table style="width: 600px">
                    <tr>
                        <td>
                            <asp:DataGrid ID="dgGuia" runat="server" AutoGenerateColumns="False" Width="600px" Height="80px"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False" HorizontalAlign="Center">
                                <AlternatingItemStyle CssClass="GridAltItem" Font-Bold="False" />
                                <ItemStyle CssClass="GridItem" Font-Bold="False" Font-Italic="False" />
                                <HeaderStyle CssClass="GridHeader" Font-Bold="true"/>
                                <Columns>
                                    <asp:TemplateColumn  HeaderText="Nro Guía">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNroGuia" runat="server" Text='<%#  Container.DataItem("vch_NroGuia")%>'></asp:Label>
                                        </ItemTemplate> 
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Nro Pedido" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNroPedido" runat="server" Text='<%#  Container.DataItem("vch_NroPedido")%>'></asp:Label>
                                        </ItemTemplate> 
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Salida" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalida" runat="server" Text='<%#  Container.DataItem("int_NroSalida")%>'></asp:Label>
                                        </ItemTemplate> 
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="#Rollos">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNroRollos" runat="server" Text='<%# String.Format("{0:N}", Container.DataItem("Cant_Rollos"))%>'></asp:Label>
                                        </ItemTemplate> 
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Tot. Mts.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMtrs" runat="server" Text='<%# String.Format("{0:N}", Container.DataItem("Mts_Despachado"))%>'></asp:Label>
                                        </ItemTemplate> 
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlEstado" runat="server" SelectedValue='<%# Eval("vch_Estadoguia") %>'>
                                               <%-- <asp:ListItem Value="C1">Confirmar Salida</asp:ListItem>
                                                <asp:ListItem Value="C2">Confirmar Entrega</asp:ListItem>
                                                <asp:ListItem Value="C3">Rep. Devolucion</asp:ListItem>
                                                <asp:ListItem Value="C4">Rep. No Aceptada</asp:ListItem>--%>
                                                <asp:ListItem Value="PEN">--Seleccione---</asp:ListItem>
                                                 <asp:ListItem Value="CSA">Confirmar Salida</asp:ListItem>
                                                <asp:ListItem Value="ENT">Confirmar Entrega</asp:ListItem>
                                                <asp:ListItem Value="RED">Rep. Devolucion</asp:ListItem>
                                                <asp:ListItem Value="REN">Rep. No Aceptada</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Grabar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkAgregar" runat="server" CommandName="Grabar">
                                                <img border="0" src="../images/save.gif" alt="agregar" width="16" height="16"/>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    

                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
               </table>
               <%--<table>
                <tr>
                    <td class="Descripcion" style="width:130px" align="left">Estado Salida:</td>
                    <td align="left" class="style1">
                         <asp:DropDownList ID="ddlEstado" runat="server">
                            <asp:ListItem Value="" Selected="True">---Seleccione---</asp:ListItem>
                            <asp:ListItem Value="C1">Confirmar Salida</asp:ListItem>
                            <asp:ListItem Value="C2">Confirmar Entrega</asp:ListItem>
                            <asp:ListItem Value="C3">Rep. Devolucion</asp:ListItem>
                             <asp:ListItem Value="C4">Rep. No Aceptada</asp:ListItem>
                         </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Button id="btnGrabar" runat="server" Text="Grabar" CssClass="Boton" Width="90px" BackColor="Green" />
                    </td>
                </tr>
               </table>--%>
               <table>
                <tr>
                    <td></td>
                </tr>
               </table>
               <table>
                <tr>
                    <td>
                         <%--<asp:GridView ID="grdReporVenta" runat="server" AutoGenerateColumns="false" Width="800PX">
                             <AlternatingRowStyle CssClass="GridAltItem"/>
                            <RowStyle CssClass="GridAltItemAlert2"/>
                             <HeaderStyle CssClass="GridHeader"/>
                             <Columns>
                                <asp:TemplateField HeaderText="Cliente">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCli" runat="server"  Text='<%#Container.DataItem("NO_CLIE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod. Artículo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArti" runat="server"   Text='<%#Container.DataItem("vch_CodArticulo")%>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Desc. Artículo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesArt" runat="server"  Text='<%#Container.DataItem("DE_ITEM")%>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Met. Programado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMetraje" runat="server"  Text='<%# String.Format("{0:N}", Container.DataItem("num_CantProgramada"))%>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"/>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
				                    <asp:LinkButton ID="lnkEdit" runat="server"  CausesValidation="true" Text="<img border=0 src=../images/im_edit.gif alt=editar>" CommandName="Edit" width="20px" ></asp:LinkButton>
			                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px"/>
                            </asp:TemplateField
                             </Columns>

                         </asp:GridView>--%>
                    </td>
                </tr>
               </table>
        </center>
    </form>
</body>
</html>
