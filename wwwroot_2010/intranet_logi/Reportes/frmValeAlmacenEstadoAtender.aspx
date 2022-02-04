<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmValeAlmacenEstadoAtender.aspx.vb" Inherits="intranet_logi.frmValeAlmacenEstadoAtender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Seguimiento de Documentos</title>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
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
	   <td align="center" style="width:800px;HEIGHT:20px;" class="Cabecera">REPORTE&nbsp;DE&nbsp;VALES&nbsp;DE&nbsp;ALMACEN&nbsp;GENERAL</td>
      </tr>
     </table>

     <table style="WIDTH:600px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30">&nbsp;Fecha Inicio:</td>
	   <td style="WIDTH: 130px;" align="left" height="30">
        <asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frm_RptIngresoSalidaHilos.txtFechaIni, 'dd/mm/yyyy')" border="0"
		 alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  /></td>
       
      </tr>
      <tr>
	   <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30">&nbsp;Fecha Fin:</td>
	   <td style="WIDTH: 130px;" align="left" height="30">
        <asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frm_RptIngresoSalidaHilos.txtFechaFin, 'dd/mm/yyyy')" border="0"
		 alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  /></td>
       <td style="width:40px" align="center" rowspan="5">
        <asp:ImageButton ID="btnBuscar" runat="server" Width="30px" Height="30px" ImageUrl="../../intranet/Imagenes/Buscar.png" ToolTip="Buscar" style="cursor:hand;"/>
       </td>
      </tr>
     </table>

     
    </ContentTemplate>
   </asp:UpdatePanel>
 </form>
</body>
</html>
