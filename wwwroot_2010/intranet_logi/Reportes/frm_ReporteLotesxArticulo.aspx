<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ReporteLotesxArticulo.aspx.vb" Inherits="intranet_logi.frm_ReporteLotesxArticulo" %>
<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
<title>Nuevo Mundo</title>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>


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
                Reporte de Lotes por Articulo</td>
        </tr>
    </table>
       
    <!-- **************  Inicio de Contenido ****************** -->
            
            <asp:UpdatePanel ID="panContenido" runat="server" >
            <ContentTemplate>

                    <table  border="0" cellpadding="0" cellspacing="0"   style="width:550px;">
                    <tr>
                        <td style="width:500px;height:5px"></td>
                    </tr>
                    </table>
                    <table  border="0" cellpadding="0" cellspacing="0" class="FrameSimple" style="width:550px; height:60px">
                    <tr>
                    <td align="center">
                    
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:500px; height:60px">

                        <tr>
                            <td style="width: 500px">  <table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="4" align="left">
                                                &nbsp;</td>
										</tr>
										    <tr>
                                                <td align="left" colspan="4">
                                                    <asp:Label ID="Label3" runat="server" Font-Underline="True" 
                                                        Text="Filtros de Reporte"></asp:Label>
                                                </td>
                                            </tr>
										<tr>
											<td colspan="4" align="left">&nbsp;&nbsp;&nbsp;</td>
										</tr>
									<tr>
										<td style="width: 150px" class="lbl">Desde:</td>
											<td align="left">
											    <ig:WebDatePicker ID="TxtFechaIni" runat="server" Width="110px"></ig:WebDatePicker>
											    </td>
											<td style="width: 100px" class="lbl">&nbsp;&nbsp;Hasta:</td>
											<td>
                                                <ig:WebDatePicker ID="TxtFechaFin" runat="server" Width="110px">
                                                                        </ig:WebDatePicker>											
											</td>
											
										</tr>
									        <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>

                                            </tr>
                                            <tr>
                                                <td class="lbl">
                                                    Articulo (4 Digitos):</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtArticulo" runat="server" MaxLength="4" Width="60px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                                        ImageUrl="~/images/Buscar.png"  style="cursor:hand;" ToolTip="Buscar" Width="30px" />
                                                </td>

                                            </tr>
									</table>
                                    	                           
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
                        <td style="height:20px" align="left" class="error">
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
