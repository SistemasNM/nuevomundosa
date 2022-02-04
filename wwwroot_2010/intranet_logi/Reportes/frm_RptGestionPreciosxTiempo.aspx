<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptGestionPreciosxTiempo.aspx.vb" Inherits="intranet_logi.frm_RptGestionPreciosxTiempo" %>
<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >

<title>Nuevo Mundo</title>
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
            <td style="width:100%" valign="bottom" align="center" >
                Reporte: Gestión de Precios Articulos por Tiempo</td>
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
											<td colspan="6" align="left">
                                                <asp:Label ID="Label1" runat="server" Text="Modelos de Reporte" 
                                                    Font-Underline="True"></asp:Label></td>
										</tr>
										<tr>
											<td colspan="6" align="left" nowrap>
                                                <asp:RadioButtonList ID="rbListModelos" runat="server" AutoPostBack="True" 
                                                    Width="269px" Height="34px" style="text-align: left" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Selected  Title="Este Modelo lista los Articulos con Precio Inicio - Precio Fin.">Modelo 1</asp:ListItem>
                                                    <asp:ListItem Value="2" Title="Este Modelo lista los Articulos considerando el Tipo de Articulo y el Resultado de Comparacion entre Precio Inicio - Precio Fin.">Modelo 2</asp:ListItem>
                                                    <asp:ListItem Value="3" Title="Lista artículos agrupados por sus características (rubro, familia, subfamilia) y la diferencia de Precio Final - Precio Inicial.">Modelo 3</asp:ListItem>
                                                </asp:RadioButtonList>
											</td>
										</tr>	
										<tr>
											<td colspan="6" align="left">&nbsp;&nbsp;&nbsp;</td>
										</tr>
										<tr>
											<td colspan="6" align="left">
                                                <asp:Label ID="Label3" runat="server" Text="Filtros de Reporte" 
                                                    Font-Underline="True"></asp:Label></td>
										</tr>
										<tr>
											<td colspan="6" align="left">&nbsp;&nbsp;&nbsp;</td>
										</tr>																												
										<tr>
											<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Desde :</td>
											<td><ig:WebDatePicker ID="TxtFechaIni" runat="server" Width="110px"></ig:WebDatePicker>
											    </td>
											<td>&nbsp;&nbsp;&nbsp;Hasta :
											</td>
											<td>
                                                <ig:WebDatePicker ID="TxtFechaFin" runat="server" Width="110px">
                                                                        </ig:WebDatePicker>											
											</td>
											<td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>											
											<td>
                                        <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                        ImageUrl="~/Images/Buscar.png" ToolTip="Buscar" 
                                        OnClick="btnBuscar_Click" style="cursor:hand;"
                                        Width="30px" />											
											</td>											
										</tr>
										<tr id="tr_TipoArt" runat="server">
											<td nowrap><asp:Label ID="Label2" runat="server" Text="Tipo Articulo :"></asp:Label>
											</td>
											<td colspan="5" align="left">
                                                <asp:DropDownList ID="cmbTipoArt" runat="server">
                                                    <asp:ListItem Text="Todos" Value="0" Selected></asp:ListItem>
                                                    <asp:ListItem Text="Materia Prima" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="P.Q. y Colorantes" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Suministros" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Repuestos" Value="4"></asp:ListItem>
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
                            &nbsp;</td>
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
