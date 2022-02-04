<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ReporteReclamosPendienteRecojo.aspx.vb" Inherits="intranet_logi.frm_ReporteReclamosPendienteRecojo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Reporte Reclamos Pendiente de Recojo</title>
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
     <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
     <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
     <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
          <script language="javascript" type="text/jscript">
          //========================================//
          function fMostrarReporte(strURL) {

              var intWidth = screen.width;
              var intHeight = screen.height;

              window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
          }
          //========================================//
           
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
                style="width:680px; height:30px">
                <tr>
                    <td style="width:100%" class="Cabecera">Reporte: Reclamos Pendientes de Recojo</td>
                </tr>
            </table>
            <table  border="0" cellpadding="0" cellspacing="0"   style="width:500px;">
                <tr>
                    <td style="width:500px;height:5px"></td>
                </tr>
            </table>
            <table  border="0" cellpadding="0" cellspacing="0" class="FrameSimple" style="width:300px; height:40px">
             <tr>
                <td><asp:Label ID="idlabel" runat="server" Text="Reclamo"></asp:Label></td>
                <td><asp:TextBox ID="idReclamo" runat="server"></asp:TextBox></td>
                <td>
                    <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" ImageUrl="~/Imagenes/Buscar.png" ToolTip="Buscar" style="cursor:hand;" Width="30px" />											
			    </td>	
            </tr>            
            </table>
        </center>
    </form>
</body>
</html>
