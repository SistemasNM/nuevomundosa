<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_MovimientoxUbicacion.aspx.vb" Inherits="intranet_logi.frm_MovimientoxUbicacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporte de Movimiento por Ubicación</title>
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
    <form id="frmData" runat="server" method="post" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table style="width:946px" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="Cabecera" style="width:900px; height:30px" align="center">MOVIMIENTO POR 
                        UBICACIÓN</td>
                </tr>
            </table>
            <table id="Table1" style="WIDTH: 946px; HEIGHT: 40px" cellspacing="2" cellpadding="1" width="946" class="FrameSimple" 
				    border="0">
				    <tr>
					    <td class="Descripcion" style="width:100px">&nbsp;Fecha Inicial :</td>
					    <td class="style3" style="width:150px">
						    <asp:textbox id="txtFecha_Inicio" runat="server" Width="104px" CssClass="input"></asp:textbox><IMG id="imgFechaInicio" onclick="popUpCalendar(this, frmData.txtFecha_Inicio, 'dd/mm/yyyy')"
							    alt="Seleccionar fecha" src="~/images/calendario.png" name="imgFechaInicio" runat="server"></td>
					    <td style="WIDTH: 586px; HEIGHT: 25px">&nbsp;
						    </td>
					
				    </tr>
				    <tr>
					    <td class="Descripcion" style="width:100px">&nbsp;Fecha Final :</td>
					    <td class="style4" style="width:150px">
						    <asp:textbox id="txtFecha_Final" runat="server" Width="103px" CssClass="input" 
                                MaxLength="10"></asp:textbox><IMG id="imgFechaInicio0" onclick="popUpCalendar(this, frmData.txtFecha_Final, 'dd/mm/yyyy')"
							    alt="Seleccionar fecha" src="~/images/calendario.png" name="imgFechaInicio0" 
                                runat="server"></td>
					    <td style="HEIGHT: 26px">
                            <asp:button Width="95PX" id="btnProcesar" runat="server" CssClass="btnDetalle" 
                                Text="Procesar"></asp:button>
                        </td>
				    </tr>
                    <tr>
                        <td class="Descripcion" style="width:100px">&nbsp;Ubicación :</td>
					    <td class="style4" style="width:150px">
						    <asp:DropDownList ID="ddlubicaciones" runat="server" Height="17px" 
                                Width="109px">
                            </asp:DropDownList>
                        </td>
					    <td style="HEIGHT: 26px">
                            <asp:button Width="95PX" id="btnReporte" runat="server" CssClass="btnDetalle" 
                                Text="Reporte"></asp:button>
                        </td>
                    </tr>   
			    </table>
        </center>

    </form>
</body>
</html>





