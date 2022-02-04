<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptRollosReetiquetados.aspx.vb" Inherits="intranet_logi.frm_RptRollosReetiquetados" %>
<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title>Rollos Reetiquetados</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
<%-- <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
--%>
 <script language="javascript" type="text/jscript">




     function fMostrarReporte(strURL) {

         var intWidth = screen.width;
         var intHeight = screen.height;

         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }


     //===============================================================//

       
</script></head><body><form id="frmData" runat="server" method="post" >

<center>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <br />

    <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
        style="width:680px; height:30px">
        <tr>
            <td style="width:100%" valign="bottom" align="center" >
                Reporte: Rollos Reeiquetados</td>
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
                    
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:500px; height:40px">

                        <tr>
                            <td style="width: 450px">
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
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
											<td>
                                                <ig:WebDatePicker ID="TxtFechaIni" runat="server" Width="110px" CssClass="input"></ig:WebDatePicker>
											</td>
											<td>&nbsp;&nbsp;&nbsp;Hasta :
											</td>
											<td>
                                                <ig:WebDatePicker ID="TxtFechaFin" runat="server" Width="110px" CssClass="input"></ig:WebDatePicker>											
											</td>
											<td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>											
											<td>
                                        <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                        ImageUrl="../Imagenes/Buscar.png" ToolTip="Buscar" 
                                        OnClick="btnBuscar_Click" style="cursor:hand;"
                                        Width="30px" />						
                                                			
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
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Art. Destino:</td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtArticulo7" runat="server" MaxLength="7" CssClass="input"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
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
