<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Consulta_Escolaridad_Prestamo.aspx.vb" Inherits="intranet_logi.frm_Consulta_Escolaridad_Prestamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
  <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
  <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
  <link href="../css/EstilosWeb.css" rel="stylesheet" type="text/css" />
  <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>
  <script language = "javascript" type ="text/javascript">
      function showFormularioEscolaridad(ruta) {
          //var row = lnk.parentNode.parentNode;
          //var rowIndex = row.rowIndex - 1;

          //var codigo = row.cells[0].innerText;
          //var login = row.cells[6].innerText;
          //window.open("http://200.60.99.228/EnlaceNM_Extranet/Procesos/frmAsignacionEscolar.aspx?Usuario=" + login + "&IdFlg=" + "visitante");
          window.open(ruta);
      }

      function showFormularioPrestamo(ruta) {
//          var row = lnk.parentNode.parentNode;
//          var rowIndex = row.rowIndex - 1;

//          var codigo = row.cells[0].innerText;
//          var login = row.cells[6].innerText;
          //          window.open("http://200.60.99.228/EnlaceNM_Extranet/Procesos/frmPrestamoEscolar.aspx?Usuario=" + login + "&IdFlg=" + "visitante");
          window.open(ruta);
      }
  </script>
  <script language="javascript" type="text/jscript">


      //========================================//


      function fMostrarReporte(strURL) {

          var intWidth = screen.width;
          var intHeight = screen.height;

          window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
      }

      //===============================================================//

       
</script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
                    <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	                <tr>
	                    <td class="Cabecera" style="width: 800px; height: 30px" align="center">MAESTRO DE ORDENES DE TRABAJO</td>
	                </tr>
	            </table>
                <table style="width: 800px" align="center">
                    <tr>
                            <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Año:</td>
                            <td>
                            <asp:DropDownList ID="ddlAno" runat="server" Width="70px" AutoPostBack = "true">
                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                    <asp:ListItem Value="2022">2022</asp:ListItem>
                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                    <asp:ListItem Value="2025">2025</asp:ListItem>
                                    <asp:ListItem Value="2026">2026</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Tipo:</td>
                            <td>
                                <asp:CheckBox id="chkEscolaridad" runat="server" Checked="true" Text="Escolaridad" AutoPostBack="true"/>
                                
                            </td>
                            <td>
                                <asp:CheckBox id="chkPrestamo" runat="server" Text="Préstamo" AutoPostBack="true"/>
                            </td>
                            <td></td>
                            <td>
                                <asp:Button id="btnBuscar" runat="server" Text = "Buscar" CssClass="btnAzul"/>
                            </td>
                             <td></td>
                            <td>
                                <asp:Button id="btnReporte" runat="server" Text = "Reporte" CssClass="btnAzul"/>
                            </td>
                    </tr>
                    
                </table>
                <asp:Panel ID="pnlEscolaridad" runat="server" HorizontalAlign="Center">
                <table style="width: 1000px" align="center">
                        <tr>
                            <td>
                                <center><h1 style="color:black;background-color:Green">ESCOLARIDAD</h1></center>
                            </td>
                        </tr>
                     </table>
                <table style="width: 1000px" align="center">
                    <tr>
                        <td>
                            <asp:GridView ID="grdEscolaridad" runat="server" Width="1000px" AutoGenerateColumns="False">
                                 <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="grdRowStyle" />
                                    <AlternatingRowStyle CssClass="GridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Orden">
										<ItemTemplate>
											<asp:Label ID="lblOrden" runat="server" Width="8px" Text='<%# (Container.DataItemIndex + 1).ToString %>'></asp:Label>
										</ItemTemplate>
										<ItemStyle Width="8px"/>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Width="40px" Text='<%# Eval("CodTrabajador") %>' ClientIDMode="Static"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="40px"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Datos">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDatos" runat="server" Width="150px" Text='<%# Eval("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Área">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArea" runat="server" Width="90px" Text='<%# Eval("Area") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="90px"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Solicitud">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaSol" runat="server" Width="30px" Text='<%# Eval("FechaSol") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMontoTotal" runat="server" Width="50px" Text='<%# String.Format("{0:N}", Container.DataItem("MontoTotal"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstado" runat="server" Width="50px" Text='<%# Eval("Estado") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px"/>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <%--<asp:ImageButton  id="btnDetalle" runat="server" Width="30px" ImageUrl="~/images/Buscar.png"  OnClientClick="return showFormularioEscolaridad(this);"/>--%>
                                            <asp:ImageButton  id="btnDetalle" runat="server" Width="30px" ImageUrl="~/images/Buscar.png" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"/>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                </asp:Panel>

                <asp:Panel ID="pnlPrestamo" runat="server" HorizontalAlign="Center">
                     <table style="width: 1000px" align="center">
                        <tr>
                            <td>
                                <center><h1 style="color:black;background-color:Green">PRÉSTAMO</h1></center>
                            </td>
                        </tr>
                     </table>
                     <table style="width: 1000px" align="center">
                        <tr>
                            <td>
                                 <asp:GridView ID="grdPrestamo" runat="server" Width="1000px" AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="grdRowStyle" />
                                    <AlternatingRowStyle CssClass="GridAlternateItem" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="Orden">
										<ItemTemplate>
											<asp:Label ID="lblOrden" runat="server" Width="8px" Text='<%# (Container.DataItemIndex + 1).ToString %>'></asp:Label>
										</ItemTemplate>
										<ItemStyle Width="8px"/>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Width="40px" Text='<%# Eval("CodTrabajador") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Datos">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDatos" runat="server" Width="150px" Text='<%# Eval("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Área">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArea" runat="server" Width="90px" Text='<%# Eval("Area") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Solicitud">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaSol" runat="server" Width="30px" Text='<%# Eval("FechaSol") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monto Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMontoTotal" runat="server" Width="50px" Text='<%# String.Format("{0:N}", Container.DataItem("MontoTotal"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstado" runat="server" Width="50px" Text='<%# Eval("Estado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%--<asp:ImageButton  id="btnDetalle" runat="server" Width="30px" ImageUrl="~/images/Buscar.png" OnClientClick="return showFormularioPrestamo(this);"/>--%>
                                            <asp:ImageButton  id="btnDetalle" runat="server" Width="30px" ImageUrl="~/images/Buscar.png"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                 </asp:GridView>
                            </td>
                        </tr>
                     </table>
                </asp:Panel>
        </center>
    </form>
</body>
</html>
