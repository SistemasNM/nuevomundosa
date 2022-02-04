<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_PedidosEtiquetasPendientes.aspx.vb" Inherits="intranet_logi.frm_PedidosEtiquetasPendientes" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="../js/popcalendar.js" type="text/javascript"></script>
    <link href="../css/Styles.css" rel="Stylesheet" type="text/css"/>
    <link href="../css/NM0001.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        function BuscarCliente() {
            var retorno = window.showModalDialog("../Buscadores/frm_Clientes.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                form1.txtCodigoCliente.value = codigo;
                form1.txtNombreCliente.value = nombre;
                //form1.hdnNombreCliente.value = nombre;
            }
        }
        function fMostrarReporte(strURL) {

            var intWidth = screen.width;
            var intHeight = screen.height;

            window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
       <center>
                <table style="width:946px" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="Cabecera" style="width:900px; height:30px" align="center">REPORTE DE PEDIDOS ETIQUETAS</td>
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
                        <td class="Descripcion" style="width:100px">&nbsp;Cliente :</td>
                        <td class="style4" style="width:150px">
                            <asp:textbox id="txtCodigoCliente" runat="server" Width="106px" CssClass="inputDisabled" 
                                MaxLength="13"></asp:textbox>&nbsp;
                                <input class="boton" id="Button2" style="width:20px; height:20px" onclick="BuscarCliente()"
							    type="button" value="..." name="btnBuscar" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreCliente" runat="server" BorderStyle="None" 
                                CssClass="Etiqueta" Width="484px"></asp:TextBox>
                        </td> 
                    </tr>
                   
                            
			    </table>
                <table>
                     <tr>
                        <td>
                            *Rep. Pendientes no usa filtros
                        </td>
                        <td>
                            *Rep. Despacho usa filtros
                        </td>
                    </tr>
                </table>
                <table style="width:700px">
                    <tr>
                        <td align="right">
                            <asp:button id="btnReporte1" runat="server" CssClass="boton" Text="PENDIENTES"></asp:button>
                        </td>
                        <td>
                            <asp:button Width="95PX" id="btnReporte2" runat="server" CssClass="boton" Text="DESPACHADOS"></asp:button>
                        </td>
                    </tr>
                </table>
            </center>
    </form>
</body>
</html>
