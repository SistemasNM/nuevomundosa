<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListadoPedidosDistribucion_v2.aspx.vb" Inherits="intranet_logi.frmListadoPedidosDistribucion_v2" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Artículos de Distribución</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../css/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../ig_res/Default/ig_dataGrid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        tbody > tr > td.Left
        {
          text-align: left;
        }
    
        tbody > tr > td.Left
        {
          text-align: left;
        }
        tbody > tr > td.Right
        {
          text-align: right;
        }
    
        tbody > tr > td.Center
        {
          text-align: center;
        }
    
        TBODY.rowItem > TR > TD
        {
          background: #F3F3F3;
        }
    
        TBODY > TR.rowItemAlt > TD
        {
          background: #E0E0E0;
        }
    
        TBODY.RowItemChild > TR > TD
        {
          background-color: #EEF5FB;
        }
    
        TBODY > TR.RowItemAltChild > TD
        {
          background-color: #F2F2F2;
        }
    
        .Cursor
        {
          cursor: pointer;
        }
        .overlay
        {
            
            z-index:98;
            background-color: #BFFF00;
            filter:alpha(opacity=50); 
            opacity:0.5;
        }  
    </style>
  
    <script src="../js/jquery-1.11.2.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function shrinkandgrow(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "../images/More_2_16.png") {
                $("#" + displayIcon).closest("tr")
			    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
			    .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "../images/Less_2_16.png");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "../images/More_2_16.png");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <center>
            <asp:ScriptManager ID="SMG" runat="server"></asp:ScriptManager>
                <table class="Cabecera" border="1" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="Cabecera" style="text-align:center;" height="25" >Listado de Pedidos para Distribución</td>
                </tr>
                </table>
                <br />
                <br />
                <table border="0" cellpadding="0" cellspacing="5" 
                    style="width: 1100px; height: 58px;text-align:left;">
                <tr style="text-align:left;">
                    <td>
                        <table style="width: 711px; text-align:center;">
                            <tr>
                                <td>
                                    <label style="font-size:14px;">Código de Artículo: </label>
                                </td>
                                <td  colspan="3">
                                    <ig:WebDropDown ID="combobox" EnableAutoFiltering="Client" 
                                        EnableAutoCompleteFirstMatch="true" runat="server" Width="400px" 
                                        Height="19px"></ig:WebDropDown>
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Height="29px" BackColor="AliceBlue" CssClass="Boton" Width="105px"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
                <div id="idMensaje" runat="server">
                    <asp:Label ID="lblStock" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="16px" Visible="True"></asp:Label>
                </div>
                <br />
                <table id="idBotones" runat="server">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="5" 
                                style="width: 1100px; text-align:right;">
                                <tr>
                                    <td style="text-align:right;">
                                        <asp:Button ID="BtnConcluir" runat="server"  BackColor="AliceBlue" Text="Concluir" CssClass="Boton" 
                                            Height="30px" Width="106px"/>&nbsp
                                        <asp:Button ID="BtnEnviar" runat="server" BackColor="AliceBlue" Text="Enviar" CssClass="Boton" 
                                            Height="30px" Width="106px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="idDatosGenerales"  runat="server">
                    <tr  style="width:1100px;">
                        <td style="text-align:right;">
                            <asp:ImageButton ID="IbtnRefrescarSvsP" ImageUrl="../images/boton-actualizar.png" ToolTip="Actualizar Stock vs Pedido" runat="server" Height="16px" Width="16px"/>&nbsp
                            <asp:Label ID="lblTituloStockvsPedido" runat="server" Text="Stock Vs Pedido: " Font-Size="14px"></asp:Label>
                            <asp:Label runat="server" ID="lblStockvsPedido" Font-Size="14px" ForeColor="Red"></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="lblTituloCodigoArt" Text="Artículo: " Font-Size="14px"></asp:Label>
                            <asp:Label runat="server" ID="lblCodigoArt" Font-Size="14px" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr  style="width:1100px;">
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="CO_CLIE"
		                    RowDataBound="GridView1_RowDataBound" HeaderStyle-BackColor="#A52A2A" HeaderStyle-ForeColor="White" >
		                        <Columns>
		                            <asp:TemplateField ItemStyle-Width="20px">
		                                <ItemTemplate>
			                                <a href="JavaScript:shrinkandgrow('div<%# Eval("CO_CLIE") %>');">
				                                <img alt="Details" id="imgdiv<%# Eval("CO_CLIE") %>" src="../images/More_2_16.png" /> 
			                                </a>
			                                <div id="div<%# Eval("CO_CLIE") %>" style="display: none;">
				                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataKeyNames="CO_CLIE"
                                                HeaderStyle-BackColor="#FFA500" HeaderStyle-ForeColor="White">
				                                <Columns>
					                                <asp:BoundField ItemStyle-Width="150px" DataField="NU_PEDI" HeaderText="Nro. Pedido" />
					                                <asp:BoundField ItemStyle-Width="100px" DataField="FE_ENTREGA" HeaderText="Fecha Entrega" />
					                                <asp:BoundField ItemStyle-Width="100px" DataField="MTS_PEDIENTES" HeaderText="Mts. Pendientes" />                    
					                                <asp:BoundField ItemStyle-Width="100px" DataField="MTS_PROGRAMADOS" HeaderText="Mts. Programados" />
				                                </Columns>
				                                </asp:GridView>
			                                </div>
		                                </ItemTemplate>
		                            </asp:TemplateField>
			                        <asp:BoundField ItemStyle-Width="100px" DataField="CO_CLIE" HeaderText="RUC Cliente" />
			                        <asp:BoundField ItemStyle-Width="300px" DataField="NO_CLIE" HeaderText="Nombre del Cliente" />
			                        <asp:BoundField ItemStyle-Width="80px" DataField="CANT_DISPONIBLE" HeaderText="Cant. Disponible" />
			                        <asp:BoundField ItemStyle-Width="80px" DataField="MTS_TOTAL_PEDIENTES" HeaderText="Mts Pendientes" />
			                        <asp:BoundField ItemStyle-Width="90px" DataField="MTS_TOTAL_PROGRAMADOS" HeaderText="Mts Programados" />
			                        <asp:BoundField ItemStyle-Width="80px" DataField="MTS_TOTAL_DISTRIBUIDOS" HeaderText="Mts Distribuidos" />
                                    <asp:TemplateField>
                                        <HeaderStyle Width="70px"/>
                                        <HeaderTemplate>Mts Repartir</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" Width="70px">0</asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
		                        </Columns>
	                        </asp:GridView>
                            <%--<ig:WebHierarchicalDataGrid ID="igDtg_Distribucion" runat="server" 
                              ItemCssClass="rowItem" AltItemCssClass="rowItemAlt" Height="450px" Width="1100px" 
                              DataKeyFields="CO_CLIE,CO_ITEM" DataMember="Distribucion" AutoGenerateColumns="False"
                              AutoGenerateBands="False" Key="Distribucion" InitialDataBindDepth="-1" 
                              EnableDataViewState="True">
                            
                            <ExpandButton AltText="Mostrar Detalle" HoverImageUrl="~/images/More_2_16.png" ImageUrl="~/images/More-16.png" PressedImageUrl="~/images/More-16.png"></ExpandButton>
                            <CollapseButton AltText="Ocultar Detalle" HoverImageUrl="~/images/Less_2_16.png" ImageUrl="~/images/Less-16.png" PressedImageUrl="~/images/Less-16.png"></CollapseButton>
                            <GroupingSettings>
                                <RemoveButton AltText="Quitar Agrupación"></RemoveButton>
                            </GroupingSettings>
                            <ExpandCollapseAnimation SlideOpenDirection="Auto" SlideOpenDuration="50" SlideCloseDirection="Auto" SlideCloseDuration="50" />
                            <ExpandCollapseAnimation SlideOpenDirection="Auto" SlideOpenDuration="50" SlideCloseDirection="Auto" SlideCloseDuration="50" /><Columns>
                                <ig:BoundDataField DataFieldName="CO_CLIE" Key="CO_CLIE" Width="120px">
                                    <Header Text="Codigo Cliente" />
                                <Header Text="Codigo Cliente" /></ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="NO_CLIE" Key="NO_CLIE" Width="250px">
                                    <Header Text="Nombre del Cliente" />
                                <Header Text="Nombre del Cliente" /></ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="CANT_DISPONIBLE" Key="CANT_DISPONIBLE" Width="120px">
                                    <Header Text="Cant. Disponible" />
                                <Header Text="Cant. Disponible" /></ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="MTS_TOTAL_PEDIENTES" Key="MTS_TOTAL_PEDIENTES" Width="120px">
                                    <Header Text="Mts. Pendientes" />
                                <Header Text="Mts. Pendientes" /></ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="MTS_TOTAL_PROGRAMADOS" Key="MTS_TOTAL_PROGRAMADOS" Width="140px">
                                    <Header Text="Mts. Programados" />
                                <Header Text="Mts. Programados" /></ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="MTS_TOTAL_DISTRIBUIDOS" Key="MTS_TOTAL_DISTRIBUIDOS" Width="120px">
                                    <Header Text="Mts. Distribuidos" />
                                <Header Text="Mts. Distribuidos" /></ig:BoundDataField>
                                <ig:TemplateDataField  Width="120px" Key="Mts_Repartir">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" Width="100px" ID="txtMtsRepartir">0</asp:TextBox>
                                        </ItemTemplate>
                                        <Header Text="Mts. Repartir"/>
                                <Header Text="Mts. Repartir"/></ig:TemplateDataField>
                            </Columns>
                            <Bands>
                                <ig:Band DataKeyFields="CO_CLIE" DataMember="Detalle" AutoGenerateColumns="False" Key="Detalle" ItemCssClass="RowItemChild" AltItemCssClass="RowItemAltChild">
                                  <ExpandButton AltText="Mostrar Detalle" />
                                  <CollapseButton AltText="Ocultar Detalle" />
                                  <ExpandButton AltText="Mostrar Detalle" /><CollapseButton AltText="Ocultar Detalle" /><GroupingSettings>
                                    <RemoveButton AltText="Quitar Agrupación" />
                                  <RemoveButton AltText="Quitar Agrupación" /></GroupingSettings>
                                  <Columns>
                                    <ig:TemplateDataField  Width="30px" Key="bit_Seleccion">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <Header Text="Chk"/>
                                    <Header Text="Chk"/></ig:TemplateDataField>
                                    <ig:BoundDataField DataFieldName="NU_PEDI" Key="NU_PEDI" Width="120px">
                                        <Header Text="Nro. Pedido" />
                                    <Header Text="Nro. Pedido" /></ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="FE_ENTREGA" Key="FE_ENTREGA" Width="120px">
                                      <Header Text="Fecha Entrega" />
                                    <Header Text="Fecha Entrega" /></ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="MTS_PEDIENTES" Key="MTS_PEDIENTES" Width="120px">
                                      <Header Text="Mts. Pendientes" />
                                    <Header Text="Mts. Pendientes" /></ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="MTS_PROGRAMADOS" Key="MTS_PROGRAMADOS" Width="120px">
                                      <Header Text="Mts. Programados" />
                                    <Header Text="Mts. Programados" /></ig:BoundDataField>
                                  </Columns>
                                  <Behaviors>
                                    <ig:EditingCore>
                                      <Behaviors>
                                        <ig:CellEditing Enabled="false">
                                        </ig:CellEditing>
                                      </Behaviors>
                                    </ig:EditingCore>
                                  </Behaviors>
                                </ig:Band>
                            </Bands>
                            <Behaviors>
                                <ig:Activation>
                                </ig:Activation>
                                <ig:VirtualScrolling ScrollingMode="Virtual">
                                </ig:VirtualScrolling>
                            </Behaviors>
                            </ig:WebHierarchicalDataGrid>--%>
                        </td>
                    </tr>
                </table>
        </center>
    </div> 
    </form>
</body>
</html>
