<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptPedidoDistribucion.aspx.vb" Inherits="intranet_logi.frm_RptPedidoDistribucion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <script src="../js/popCalendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/jscript">
        function fMostrarReporte(strURL) {
            var intWidth = screen.width;
            var intHeight = screen.height;
            window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
        }
    </script>
</head>
<body>
    <form id="frm" method="post" runat="server">
    <div>
        <center>

        <asp:Panel ID="pnlCabecera" runat="server">
            <table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width:100%; height:30px">
                <tr>
                    <td style="width:100%" valign="bottom" align="center">REPORTE DE PEDIDOS POR DISTRIBUCIÓN</td>
                </tr>
            </table>
        </asp:Panel>

       <table id="Table1" cellspacing="2" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="Cabecera" colspan="2"></td>
					<td class="Cabecera">&nbsp;</td>
				</tr>
                </table>
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
											<td class="Titulo" style="width: 80px" align="right">Item:&nbsp;4&nbsp;Digitos:&nbsp;</td>
											<td style="width: 120px"  align="left">
                                                <asp:TextBox ID="txtCodigoCorto" runat="server" MaxLength="30" CssClass="input" Width="80px"></asp:TextBox>											
											</td>
											<td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>											
											<td>
                                                					
                                                			
											</td>	
											<td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>											
											<td>
                                                <asp:Button ID="btnVerReporte" runat="server" Text="Ver Reporte" Width="90px" CssClass="btnActivo"/>					
                                                			
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
                                             <td class="Titulo" style="width: 80px" align="right">Fecha&nbsp;Ini:&nbsp;</td>
					                        <td style="width: 120px" align="left">&nbsp;
						                        <asp:TextBox id="TxtFechaIni" runat="server" CssClass="input" MaxLength="10" Width="80px"></asp:TextBox>&nbsp;
                                                <asp:Image ID="Image1" runat="server" onclick="popUpCalendar(this, frm.TxtFechaIni, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha" src="../images/im_calendar.gif" width="13" border="0" />
                                            </td>
											
                                            
                                                                                     
											
                                        </tr>
                                        <tr>
                                        <td class="Titulo" style="width: 80px" align="right">Fecha&nbsp;Fin:&nbsp;</td>
					                        <td style="width: 120px" align="left">&nbsp;
						                        <asp:TextBox id="TxtFechaFin" runat="server" CssClass="input" MaxLength="10" Width="80px"></asp:TextBox>&nbsp;
                                                <asp:Image ID="Image2" runat="server" onclick="popUpCalendar(this, frm.TxtFechaFin, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha" src="../images/im_calendar.gif" width="13" border="0" />
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
        </center>
    </div>
    </form>
</body>
</html>
