<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ReporteEstadiosSalida.aspx.vb" Inherits="intranet_logi.frm_ReporteEstadiosSalida" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../js/popcalendar.js" type="text/javascript"></script>
    <link href="../css/Styles.css" rel="Stylesheet" type="text/css"/>
    <link href="../css/NM0001.css" type="text/css" rel="Stylesheet" />
     <script language="javascript" type="text/javascript">
         function fMostrarReporte(strURL) {

             var intWidth = screen.width;
             var intHeight = screen.height;

             window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
        <center>
            <table style="width:946px" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="Cabecera" style="width:900px; height:30px" align="center">REPORTE DE ESTADOS DE SALIDAS</td>
                    </tr>
                </table>
                <table id="Table1" style="WIDTH: 946px; HEIGHT: 40px" cellspacing="2" cellpadding="1" width="946" class="FrameSimple" 
				    border="0">
				    <tr>
					    <td class="Descripcion" style="width:100px">&nbsp;Fecha Inicial :</td>
					    <td class="style3" style="width:150px">
						    <asp:textbox id="txtFecha_Inicio" runat="server" Width="104px" CssClass="input"></asp:textbox><IMG id="imgFechaInicio" onclick="popUpCalendar(this, form1.txtFecha_Inicio, 'dd/mm/yyyy')"
							    alt="Seleccionar fecha" src="~/images/calendario.png" name="imgFechaInicio" runat="server"></td>
					    <td style="WIDTH: 586px; HEIGHT: 25px">&nbsp;
						    </td>
					
				    </tr>
				    <tr>
					    <td class="Descripcion" style="width:100px">&nbsp;Fecha Final :</td>
					    <td class="style4" style="width:150px">
						    <asp:textbox id="txtFecha_Final" runat="server" Width="103px" CssClass="input" 
                                MaxLength="10"></asp:textbox><IMG id="imgFechaInicio0" onclick="popUpCalendar(this, form1.txtFecha_Final, 'dd/mm/yyyy')"
							    alt="Seleccionar fecha" src="~/images/calendario.png" name="imgFechaInicio0" 
                                runat="server"></td>
								
					    <td style="HEIGHT: 26px"></td>
				    </tr>
                    <tr>
                        <td class="Descripcion" style="width:100px">&nbsp;#Salida :</td>
                        <td class="style4" style="width:150px">
                            <asp:textbox id="txtSalida" runat="server" Width="106px" CssClass="input" 
                                MaxLength="13"></asp:textbox>&nbsp;
                               
                        </td>
                         <td style="HEIGHT: 26px"></td>
                    </tr>    
			    </table>
                <table style="width:700px">
                    <tr>
                        <td></td><td></td>
                        <td>
                            <asp:button Width="95PX" id="btnReporte" runat="server" CssClass="boton" Text="Reporte"></asp:button>
                        </td>
                    </tr>
                </table>
        </center>
    </form>
</body>
</html>
