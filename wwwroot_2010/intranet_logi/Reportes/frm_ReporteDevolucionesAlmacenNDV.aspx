<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ReporteDevolucionesAlmacenNDV.aspx.vb" Inherits="intranet_logi.frm_ReporteDevolucionesAlmacenNDV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
<title>Nuevo Mundo</title> 
<link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
<link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
<link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
 <%--<script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>--%>  
<script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
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
            <td style="width:100%; font-size: large; font-weight: bold;" valign="bottom" 
                align="center"">
                Reporte: Devoluciones Almacén (NDV)</td>
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
                            <td style="width: 150px">
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
											<td>&nbsp;</td>
											<td>&nbsp;</td>
											<td>
                                                <table border="0" cellpadding="0" cellspacing="0" 
                                                    style="WIDTH: 500px; HEIGHT: 20px">
                                                    <tr>
                                                        <td align="center" class="Etiqueta" style="WIDTH: 100px; HEIGHT: 20px">
                                                            &nbsp;Fecha Inicio:</td>
                                                        <td align="left" style="WIDTH: 120px; HEIGHT: 20px">
                                                            <asp:TextBox ID="txtFechaIni" runat="server" CssClass="input" Font-Size="10px" MaxLength="10" Width="80px"></asp:TextBox>&nbsp;
                                                            <img onclick="popUpCalendar(this, frmData.txtFechaIni, 'dd/mm/yyyy')" border="0" 
                                                                alt="Seleccionar fecha Inicial" src="../Imagenes/Calendario.gif" width="16px" height="16px"  /></td>                                                        
                                                        <td align="center" class="Etiqueta" style="WIDTH: 100px; HEIGHT: 20px">
                                                            &nbsp;Fecha Fin:</td>
                                                        <td align="left" style="WIDTH: 120px; HEIGHT: 20px">
                                                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="input" Font-Size="10px" MaxLength="10" Width="70px"></asp:TextBox>&nbsp;
                                                            <img onclick="popUpCalendar(this, frmData.txtFechaFin, 'dd/mm/yyyy')" border="0"		 
                                                                alt="Seleccionar fecha Final" src="../Imagenes/Calendario.gif" width="16px" height="16px"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="WIDTH: 100px; HEIGHT: 20px">
                                                            &nbsp;</td>
                                                        <td align="left" style="WIDTH: 120px; HEIGHT: 20px">
                                                            &nbsp;</td>
                                                        <td align="center" style="WIDTH: 100px; HEIGHT: 20px">
                                                            &nbsp;</td>
                                                        <td align="left" style="WIDTH: 120px; HEIGHT: 20px">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
											<td>
                                                &nbsp;</td>
											<td >
                                                <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                                    ImageUrl="../Imagenes/Buscar.png" OnClick="btnBuscar_Click" 
                                                    style="cursor:hand;" ToolTip="Buscar" Width="30px" />
                                            </td>											
											<td>
                                                &nbsp;</td>											
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