<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptResumenIngresoDesperdicios.aspx.vb" Inherits="intranet_logi.frm_RptResumenIngresoDesperdicios" %>
<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >

<title>Nuevo Mundo</title> 
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" /> 
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />

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

    <form id="frmData" runat="server" method="post" >

<center>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <br />

    <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
        style="width:680px; height:30px">
        <tr>
            <td style="width:100%" valign="bottom" align="center" class="CabMain" >
                Reporte: Resumen - Ingresos de Desperdicios</td>
        </tr>
    </table>
       
    <!-- **************  Inicio de Contenido ****************** -->
            
            <asp:UpdatePanel ID="panContenido" runat="server" >
            <ContentTemplate>

                    <table  border="0" cellpadding="0" cellspacing="0"   style="width:450px;">
                    <tr>
                        <td style="width:500px;height:5px"></td>
                    </tr>
                    </table>
                    <table  border="0" cellpadding="0" cellspacing="0" class="FrameSimple" style="width:680px; height:40px">
                    <tr>
                    <td align="center">
                    
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:450px; height:40px">

                        <tr>
                            <td style="width: 450px">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="6" align="left">
                                                <asp:Label ID="Label3" runat="server" Text="Filtros de Reporte" 
                                                    Font-Underline="True"></asp:Label></td>
										</tr>
										<tr>
											<td colspan="6" align="left">&nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="rbt_opcion" 
                                                    runat="server" Width="450px">
                                                <asp:ListItem Selected="True" Value="1">Resumido</asp:ListItem>
                                                <asp:ListItem Value="2">Detallado por Documento</asp:ListItem>
                                                <asp:ListItem Value="3">Resumido por Fecha</asp:ListItem>
                                                <asp:ListItem Value="4">Desperdicios de Tejeduría</asp:ListItem>
                                                <asp:ListItem Value="5">Desperdicios de Tejeduría por Proceso</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
										</tr>																												
										<tr>
                                            <td align="left" colspan="6" ></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">&nbsp;</td>
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
                                        ImageUrl="~/images/Buscar.png" ToolTip="Buscar" 
                                        OnClick="btnBuscar_Click" style="cursor:hand;"
                                        Width="30px" />											
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
                            <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
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
