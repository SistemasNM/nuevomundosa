<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptDiasdeStkArticulos.aspx.vb" Inherits="intranet_logi.frm_RptDiasdeStkArticulos" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >

<title>Nuevo Mundo</title>
<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
<link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
<link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
<script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>

<script language="javascript" type="text/jscript">

      
       //========================================//


         function fMostrarReporte(strURL) {
       
           var intWidth  = screen.width;
           var intHeight = screen.height;
  
           window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
       }
       
       //===============================================================//
       
</script>
    </head>

<body>

    <form id="frmData" runat="server" method="post" >

<center>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <br />

    <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
        style="width:680px; height:30px">
        <tr>
            <td style="width:100%" class="Cabecera" >
                Reporte: Días de Stock por Artículo</td>
        </tr>
    </table>
       
    <!-- **************  Inicio de Contenido ****************** -->
            
            <asp:UpdatePanel ID="panContenido" runat="server" >
            <ContentTemplate>

                    <table  border="0" cellpadding="0" cellspacing="0"   style="width:500px;">
                    <tr>
                        <td style="width:500px;height:5px"></td>
                    </tr>
                    </table>
                    <table  border="0" cellpadding="0" cellspacing="0" class="FrameSimple" style="width:680px; height:40px">
                    <tr>
                    <td align="center">
                    
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:300px; height:40px">

                        <tr>
                            <td style="width: 150px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
								
										<tr>
											<td colspan="4" align="left">
                                                <asp:Label ID="Label1" runat="server" Text="Modelos de Reporte" 
                                                    Font-Underline="True"></asp:Label></td>
										</tr>
										<tr>
											<td colspan="4" align="left" nowrap>
                                                <asp:RadioButtonList ID="rbListModelos" runat="server" AutoPostBack="True" 
                                                    RepeatDirection="Horizontal" Width="350px">
                                                    <asp:ListItem Value="1" Selected  Title="Este Modelo lista el Stock por Articulos Filtrado por Tipo de Articulo y Almacen.">Modelo 1</asp:ListItem>
                                                    <asp:ListItem Value="2" Title="Este Modelo lista el Stock por Articulos Filtrado por Tipo de Articulo, Almacen y Tipo de Modelo de Compras.">Modelo 2</asp:ListItem>
                                                </asp:RadioButtonList>
											</td>
										</tr>
										<tr>
											<td colspan="4" align="left">&nbsp;&nbsp;&nbsp;</td>
										</tr>
										<tr>
											<td colspan="4" align="left">
                                                <asp:Label ID="Label3" runat="server" Text="Filtros de Reporte" 
                                                    Font-Underline="True"></asp:Label></td>
										</tr>											
										<tr>
											<td colspan="4" align="left">&nbsp;&nbsp;&nbsp;</td>
										</tr>								
								
										<tr>
											<td align="right" nowrap>Tipo de Artículo :&nbsp;&nbsp;
											</td>
											<td align="left">
											    <asp:DropDownList ID="cmbTipoArticulo" runat="server" class="input" ></asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td align="right">&nbsp;&nbsp;&nbsp;Almacen :&nbsp;&nbsp;
											</td>
											<td align="left">
                                                <asp:DropDownList ID="cmbAlmacen" runat="server" class="input" ></asp:DropDownList>
											</td>
											<td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>											
											<td>
                                        <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                        ImageUrl="../../intranet/Imagenes/Buscar.png" ToolTip="Buscar" 
                                        style="cursor:hand;"
                                        Width="30px" />											
											</td>											
										</tr>
										
										<tr id="tr_TipoModComp" runat="server">
											<td nowrap><asp:Label ID="Label2" runat="server" Text="Tipo Mod. Compra :"></asp:Label>&nbsp;&nbsp;
											</td>
											<td colspan="3" align="left">
                                                <asp:DropDownList ID="cmbTipoModComp" runat="server" class="input">
                                                    <asp:ListItem Text="TODOS" Value="0" Selected></asp:ListItem>
                                                    <asp:ListItem Text="CONSUMO PROMEDIO" Value="R" ></asp:ListItem>
                                                    <asp:ListItem Text="DEMANDA" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="CRITICO" Value="C"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
										</tr>											
										
									</table>                            
                            </td>
                        </tr>                      
                        <tr>
                            <td valign="middle">                               
                                
                            </td>
                        </tr>                                             
                        <tr>
                            <td align="left">
                                <asp:TextBox ID="txtUrl" runat="server" CssClass="txtHid" Width="10px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    
                    </td>
                    </tr>
                    </table>
                    <br />

                    
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:680px;">
                    <tr>
                        <td style="height:20px" align="left">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="height:20px">
                            &nbsp;
                        </td>
                    </tr>
                    </table>                    
                    <br />                                        
            </ContentTemplate>
            </asp:UpdatePanel>            
            <!-- **************  Fin de tabContenido ****************** -->            
</center>

    </form>

</body>
</html>
