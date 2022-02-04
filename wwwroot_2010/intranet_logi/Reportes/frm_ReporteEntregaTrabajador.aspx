<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ReporteEntregaTrabajador.aspx.vb" Inherits="intranet_logi.frm_ReporteEntregaTrabajador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
<script language="javascript" type="text/jscript">
    
</script>
   <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
 <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
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
    <form id="form1" runat="server">
        <center>
            <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
                style="width:680px; height:30px">
                <tr>
                    <td style="width:100%" valign="bottom" align="center" >
                        REPORTE DE ENTREGA POR TRABAJADOR</td>
                </tr>
            </table>
            <table style="WIDTH:600px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0">
                <tr>
	               <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30">&nbsp;Fecha Inicio:</td>
	               <td style="WIDTH: 130px;" align="left" height="30">
                    <asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                     &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaIni, 'dd/mm/yyyy')" border="0"
		             alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  /></td>
       
                  </tr>
                   <tr>
	                   <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30">&nbsp;Fecha Fin:</td>
	                   <td style="WIDTH: 130px;" align="left" height="30">
                        <asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                         &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaFin, 'dd/mm/yyyy')" border="0"
		                 alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  /></td>
                       <td style="width:40px" align="center" rowspan="5">
                        <asp:ImageButton ID="btnBuscar" runat="server" Width="75px" Height="25px" ImageUrl="~/images/btn_Buscar.gif" ToolTip="Buscar" style="cursor:hand;"/>
                       </td>
                   </tr>
                   <tr>
                    <td>
                         <asp:RadioButton ID="rdbSerie3" runat="server" Text="Serie 0003" 
                                 Checked="True"  GroupName="TipoReporte" AutoPostBack="True" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdbSerie5" runat="server" Text="Serie 0005" 
                                 GroupName="TipoReporte" AutoPostBack="True" />
                    </td>
                   </tr>
            </table>
        </center>
    </form>
</body>
</html>
