<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListadoPedidosDistribucion.aspx.vb" Inherits="intranet_logi.frmListadoPedidosDistribucion" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Artículos de Distribución</title>

 <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
    <%--<link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script> --%><%-- <link type="text/css" rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
 <link type="text/css" rel="stylesheet" href="/resources/demos/style.css">--%>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function JvfunonBlur() {
            var grid = document.getElementById('<%=grvListArtDist.ClientID %>');

            var col1;
            var totalcol1 = 0;
            for (i = 0; i < grid.rows.length; i++) {
                col1 = grid.rows[i].cells[9];

                for (j = 0; j < col1.childNodes.length; j++) {
                    if (col1.childNodes[j].type == "text") {
                        if (!isNaN(col1.childNodes[j].value) && col1.childNodes[j].value != "") {
                            totalcol1 += parseInt(col1.childNodes[j].value)
                        }
                    }
                }
                
            }
            document.getElementById('<%=lblSumaDistri.ClientID %>').innerHTML = totalcol1.toString();

        }
    </script>
    <style type="text/css">
        .style5
        {
            width: 1000px;
            height: 41px;
        }
        
        .style6
        {
            width: 1096px;
            height: 159px;
        }
                
        .style7
        {
            width: 1096px;
            height: 75px;
        }
        .style8
        {
            width: 1096px;
            height: 24px;
        }
                
        .style9
        {
            width: 83%;
        }
                
        .CabMain
        {
            width: 954px;
        }
                
        .style10
        {
            width: 303px;
        }
        .overlay
        {
            
            z-index:98;
            background-color: #BFFF00;
            filter:alpha(opacity=50); 
            opacity:0.5;
        }   
        </style>
</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
  </asp:ScriptManager>
    <div>
         <center>
            <asp:UpdatePanel ID="uplContenido" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="CabMain" border="1" cellpadding="0" cellspacing="0" style="border-color:Blue;">
                <tr>
                    <td style="Font-Size:14px;" valign="bottom" align="center" class="style9" >
                        <b>Listado de Pedidos para Distribución</b></td>
                </tr>
                </table>
                <br />
                <br />
                <table border="0" cellpadding="0" cellspacing="5" 
                    style="width: 959px; height: 58px;text-align:left;">
                <tr style="text-align:left;">
                    <td>
                        <table style="width: 711px; text-align:left;">
                            <tr>
                                <td>
                                    <label style="font-size:14px;">Código de Artículo: </label>
                                </td>
                                <td  colspan="3">
                                    <ig:WebDropDown ID="combobox" EnableAutoFiltering="Client" 
                                        EnableAutoCompleteFirstMatch="true" runat="server" Width="447px" 
                                        Height="19px"></ig:WebDropDown>
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Boton" Height="29px" Width="105px"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
                <div>
                        <asp:Label ID="lblStock" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="16px" Visible="True"></asp:Label>
                </div>
                <asp:UpdateProgress ID="up_Load" DisplayAfter="0" runat="server" >   
                 <ProgressTemplate>
                  <div id="divLoad" class="overlay" style="width:959px; height:20px">
                  <img src="../Imagenes/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />
                   Procesando...
                  </div>
                 </ProgressTemplate> 
                </asp:UpdateProgress>
                
                <br />
                <table>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="5" 
                                style="width: 959px; text-align:right;">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnLimpiarProgramados" Text="Limpiar programados" CssClass="Boton" runat="server" Height="30px" Width="120px"/>
                                    </td>
                                    <td class="style5" style="text-align:right;">
                                        <asp:Button ID="BtnConcluir" runat="server" Text="Concluir" CssClass="Boton" 
                                            Height="30px" Width="106px"/>
                                        <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" CssClass="Boton" 
                                            Height="30px" Width="106px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="DivTablaPrincipal" runat="server">
                        <table border="0" cellpadding="0" cellspacing="5" 
                    style="width: 946px; height: 190px">
                        <tr  style="width:1200px;">
                            <td align="right" class="style8">
                                <asp:Label runat="server" ID="lblTituloTotalDistribSinProg" Text="Total Distribuido sin programación: " Font-Size="14px"></asp:Label>
                                <asp:Label runat="server" ID="lblTotalDistribSinProg"  Font-Size="14px" ForeColor="Red">0</asp:Label>
                                <br />
                                <asp:ImageButton ID="IbtnRefrescarSvsP" ImageUrl="../images/check.jpg" ToolTip="Actualizar Stock vs Pedido" runat="server" Height="16px" Width="16px"/>
                                <asp:Label ID="lblTituloStockvsPedido" runat="server" Text="Stock Vs Pedido: " Font-Size="14px"></asp:Label>
                                <asp:Label runat="server" ID="lblStockvsPedido" Font-Size="14px" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="lblTituloCodigoArt" Text="Artículo: " Font-Size="14px"></asp:Label>
                                <asp:Label runat="server" ID="lblCodigoArt" Font-Size="14px" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr  style="width:1200px;">
                            <td class="style6">
                                <asp:GridView ID="grvListArtDist" runat="server" Width="950px" 
                                    AutoGenerateColumns="False" DataKeyNames="DATOS">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccion" runat="server" type="checkbok"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Datos" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDatos" runat="server" Text='<%# Bind("DATOS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Center" Width="100px"/>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="# Pedido">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumeroPedido" runat="server" Text='<%# Bind("NU_PEDI") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Center" Width="100px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="CLIENTE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoClie" runat="server" Text='<%# Bind("NO_CLIE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Left" Width="170px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Fecha Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFeEntr" runat="server" Text='<%# Bind("FE_ENTR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Left" Width="60px"/>
                                       </asp:TemplateField>
                                       <%--<asp:TemplateField HeaderText="Lugar Entrega">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLugEntrega" runat="server" Text='<%# Bind("LUG_ENTREGA") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Left" Width="150px"/>
                                       </asp:TemplateField>--%>
                                       <asp:TemplateField HeaderText="Articulo" Visible = "false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArticulo" runat="server" Text='<%# Bind("ARTICULO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Left" Width="450px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Cant. Disponible">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaDisp" runat="server" ForeColor="Red" Text='<%# Bind("ca_disp") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Right" Width="60px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Mts. Pedido">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaPedi" runat="server" Text='<%# Bind("CA_PEDI") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Right" Width="60px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Mts. Atendidos">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaAten" runat="server" Text='<%# Bind("CA_ATEN") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Right" Width="60px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Mts. Pendientes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaPend" runat="server" Text='<%# Bind("CA_PEND") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Right" Width="60px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Mts. Distribuidos">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaDistribuidos" runat="server" Text='<%# Bind("CA_DIST") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Right" Width="60px"/>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Mts. Repartir">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMtsRepartir" runat="server" Width="75px">0</asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Right" Width="75px"/>
                                       </asp:TemplateField>
                                   </Columns>
                                </asp:GridView> 
                            </td>
                        </tr>
                        <tr runat="server" id="EsMostrar">
                            <td style="text-align:right;font-size:14px;" class="style7">
                                <label>Total de Registros: </label><asp:Label ID="lblCantidad" runat="server" Font-Size="14px"></asp:Label>
                                <br />
                                <label>Suma mts distribuidos: </label><asp:Label ID="lblSumaDistri" runat="server" Font-Size="14px" ForeColor="Red">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div runat="server">
                        <asp:Label runat="server" ID="lblPedidoConcluido" Font-Size="14px"></asp:Label>
                    </div>
                    <br />
                    <div id="DivSinConcluir" runat="server">
                        <asp:Label runat="server" ID="lblSinConcluir" Font-Size="14px"></asp:Label>
                    </div>
                    <br />
            <div id="DistTotal" runat="server">
                <div style="white-space:nowrap;">
                    <br />
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>

