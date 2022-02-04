<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ReportePilotoFlow.aspx.vb" Inherits="intranet_logi.frm_ReportePilotoFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >

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
            <td style="width:100%" valign="bottom" align="center" >
                Reporte Piloto Flow</td>
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
                    <table  border="0" cellpadding="0" cellspacing="0" class="FrameSimple" style="width:680px; height:60px">
                    <tr>
                    <td align="center">
                    
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:400px; height:60px">

                        <tr>
                            <td style="width: 350px">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td colspan="3" align="left">
                                                <asp:Label ID="Label3" runat="server" Text="Filtros de Reporte" 
                                                    Font-Underline="True"></asp:Label></td>
										</tr>
										<tr>
											<td colspan="3" align="left">&nbsp;&nbsp;&nbsp;</td>
										</tr>																												
										<tr>
											<td align="right" style="font-weight: bold; width: 150px;">Año:&nbsp;&nbsp;&nbsp;</td>
											<td align="right" style="width: 100px">
                                                <asp:DropDownList ID="ddlAnhioPeriodo" runat="server" Height="21px" Width="70px">
                                                </asp:DropDownList>
											</td>
											<td >
                                        <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                        ImageUrl="~/images/Buscar.png" ToolTip="Buscar" 
                                        OnClick="btnBuscar_Click" style="cursor:hand;"
                                        Width="30px" />											
											</td>											
										</tr>
									    <tr>
                                            <td align="right" style="font-weight: bold; width: 150px;">
                                                Articulo (4 Digitos)&nbsp;&nbsp;&nbsp;</td>
                                            <td align="right" style="width: 100px">
                                                <asp:TextBox ID="txtArticulo" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
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
