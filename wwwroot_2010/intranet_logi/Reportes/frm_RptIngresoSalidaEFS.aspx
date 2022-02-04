<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptIngresoSalidaEFS.aspx.vb" Inherits="intranet_logi.frm_RptIngresoSalidaEFS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Seguimiento de Documentos</title>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/> 
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
 <script language="javascript" type="text/jscript">
     function fMostrarReporte(strURL) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }
 </script>

</head>
<body>
  <form id="frm_RptIngresoSalidaHilos" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   
   <asp:UpdatePanel ID="panContenido" runat="server">
    <ContentTemplate>
 
     <!-- Cabecera -->
     <table border="0" cellspacing="1" cellpadding="1" width="100%">
      <tr>
	   <td align="center" style="HEIGHT:20px;" class="Cabecera">REPORTE&nbsp;DE&nbsp;MOVIMIENTOS - 
           EFS</td>
      </tr>
     </table>
     <table style="WIDTH:600px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0" align="center" >
        <tr>
            <td style="width:10px"></td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" CssClass="txtHid"></asp:TextBox>
            </td>
            <td style="width:10px"></td>
        </tr>
        <tr>
            <td style="width:10px"></td>
            <td>
                <table style="WIDTH:580px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0">
                    <tr>
	                    <td style="WIDTH: 100px;" align="right" class="Etiqueta">Fecha Inicio:&nbsp;</td>
	                    <td style="WIDTH: 190px;" align="left">
                            <asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                            &nbsp;<img onclick="popUpCalendar(this, frm_RptIngresoSalidaHilos.txtFechaIni, 'dd/mm/yyyy')" border="0"
		                                alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  /></td>
                        <td style="WIDTH: 100px;" align="right" class="Etiqueta">Fecha Fin:&nbsp;</td>
                        <td style="WIDTH: 190px;" align="left">
                            <asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
                            &nbsp;<img onclick="popUpCalendar(this, frm_RptIngresoSalidaHilos.txtFechaFin, 'dd/mm/yyyy')" border="0"
		                                alt="Seleccionar fecha Final" src="../images/Calendario.gif" width="16px" height="16px"/>
                        </td>
                        </tr>
                        <tr>
                            <td align="left" style="WIDTH: 100px;">&nbsp;</td>
                            <td align="left" style="WIDTH: 190px;">&nbsp;</td>
                            <td align="left" style="WIDTH: 100px;">&nbsp;</td>
                            <td align="left" style="WIDTH: 190px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" style="WIDTH: 100px;">
                            &nbsp;</td>
                        <td align="left" style="WIDTH: 190px;">
                            <asp:RadioButtonList ID="rdlReportes" runat="server" CssClass="input">
                                <asp:ListItem Selected="True" Text="Ingresos EFS" Value="1"></asp:ListItem>
                                <asp:ListItem Selected="False" Text="Consumos EFS" Value="2"></asp:ListItem>

                            </asp:RadioButtonList>
                        </td>
                        <td align="left" style="WIDTH: 100px;">
                            <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                                ImageUrl="../images/Buscar.png" style="cursor:hand;" 
                                ToolTip="Buscar" Width="30px" />
                        </td>
                        <td align="left" style="WIDTH: 190px;">
                            &nbsp;</td>
                    </tr>
                </table>
        </td>
        <td style="width:10px"></td>
    </tr>
         <tr>
             <td style="width:10px">
                 &nbsp;</td>
             <td>
                 <asp:Label ID="lblMensaje" runat="server" CssClass="error"></asp:Label>
             </td>
             <td style="width:10px">
                 &nbsp;</td>
         </tr>
    </table>    
    </ContentTemplate>
   </asp:UpdatePanel>
   
 </form>
</body>
</html>
