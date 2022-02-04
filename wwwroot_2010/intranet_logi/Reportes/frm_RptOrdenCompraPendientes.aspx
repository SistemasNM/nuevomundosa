<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptOrdenCompraPendientes.aspx.vb" Inherits="intranet_logi.frm_RptOrdenCompraPendientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
    <%--<link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>--%>
    <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>

    <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
     <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
 <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/jscript">
        function fMostrarReporte(strURL) {

            var intWidth = screen.width;
            var intHeight = screen.height;

            window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
        }
    </script>
</head>
<body>
    <form id="frm_RptOrdenCompraPendientes" runat="server">
        <center>
             <asp:ScriptManager ID="ScriptManager1" runat="server">
             </asp:ScriptManager> 
              <br />
               <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
                    style="width:680px; height:30px">
                    <tr>
                        <td style="width:100%" valign="bottom" align="center" >
                            Reporte: Ordenes de Compras Pendientes</td>
                    </tr>
                </table>
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
                                            <td>
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
                                                            <td style="WIDTH: 200px;" align="left" height="30px">
                                                        <asp:textbox id="txtFechaIniOC" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                                                        &nbsp;<img onclick="popUpCalendar(this, frm_RptOrdenCompraPendientes.txtFechaIniOC, 'dd/mm/yyyy')" border="0"
		                                                        alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  /></td>
											            <td>&nbsp;&nbsp;&nbsp;Hasta :
											            </td>
                                                        <td style="WIDTH: 200px;" align="left" height="30px">
                                                            <asp:textbox id="txtFechaFinOC" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
                                                            &nbsp;<img onclick="popUpCalendar(this, frm_RptOrdenCompraPendientes.txtFechaFinOC, 'dd/mm/yyyy')" border="0"
	                                                        alt="Seleccionar fecha Final" src="../images/Calendario.gif" width="16px" height="16px"/>
                                                        </td>
											            <td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>											
											            <td>
                                                            <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                                                 ImageUrl="../images/Buscar.png" style="cursor:hand;" ToolTip="Buscar" 
                                                                 Width="30px" />									
											             </td>											
										            </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:600px;text-align:left">
                                                <asp:Label ID="lblMensaje" runat="server" CssClass="error"></asp:Label>
                                            </td>
                                            <td style="width:100px;text-align:center">               
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>


                    </ContentTemplate>
                </asp:UpdatePanel>
        </center>
    </form>
</body>
</html>
