<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_VacacionesFecha.aspx.vb" Inherits="intranet_rrhh.frm_VacacionesFecha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Vacaciones pendientes a la fecha</title>
 <link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../Styles/sytle.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
 <script language="javascript" type="text/javascript">
   // Mostrar reporte
   function fMostrarReporte(strURL) {
     var intWidth = screen.width;
     var intHeight = screen.height;
     window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
   }

  </script>
</head>
<body>
  <form id="frm_VacacionesFecha" runat="server">
   <div>
    <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	   <tr>
	    <td class="titulo" style="width:800px; height:20px" align="center">LISTADO DE VACACIONES PENDIENTES</td>
	   </tr>
     <tr>
	    <td style="width:800px; height:5px"></td>
     </tr>
    </table>
   </div>
   
   <div>
    <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	   <tr>
	    <td style="width:100px" class="Etiqueta">Fecha:</td>
      <td style="width:100px">
       <asp:TextBox ID="txtFecha" runat="server" Text="" Width="70px" CssClass="input" Font-Size="9px"></asp:TextBox>
       <img id="imgFechaInicio" onclick="popUpCalendar(this, txtFecha, 'dd/mm/yyyy')" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" runat="server" />
      </td>
      <td style="width:400px" align="left"></td>
      <td style="width:200px" align="right">
       <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btnAzul" />
      </td>
	   </tr>
    </table>
   </div>
  </form>
</body>
</html>
