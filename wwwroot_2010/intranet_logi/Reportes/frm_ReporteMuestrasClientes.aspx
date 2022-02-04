<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_ReporteMuestrasClientes.aspx.vb" Inherits="intranet_logi.frm_ReporteMuestrasClientes"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head  runat="server">
		<title>Reporte de Muestras x Clientes</title>
		 <meta http-equiv="X-UA-Compatible" content="IE=9"/> 
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet" />
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
		<script type="text/javascript" language="javascript" >

			function popUp(strUrl) 
			{
				var intWidth = screen.width;
				var intHeight = screen.height;
				//window.open(strUrl);
				window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
            }

            function fnc_BuscarCliente() {//begin fnc_BuscarCliente()
           // alert("sdsad");
                var retorno = window.showModalDialog("../Buscadores/frm_ClientesVendedor.aspx?", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
                if (retorno != "" && retorno != ":") {
                    var datos = retorno.split(":");
                    var codigo = datos[0];
                    var nombre = datos[1];
                    var codigoVendedor = datos[2];
                    var nombreVendedor = datos[3];

                    document.getElementById('txtCodigoCliente').value = codigo; //RucCliente
                    document.getElementById('txtNombreCliente').value = nombre; //NombreCliente




                }
            }


            function fMostrarReporte(strURL) {

                var intWidth = screen.width;
                var intHeight = screen.height;

                window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
            }

		</script>

	   

	    <style type="text/css">
            .Input
            {}
        </style>

	   

	</head>
	<body>
    
		<form id="Form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 900px; POSITION: absolute; TOP: 8px; HEIGHT: 64px"
				cellspacing="1" cellpadding="0" width="706" border="0">
				<tr>
					<td class="Cabecera" id="colTitulo" align="left" runat="Server">&nbsp;MUESTRAS POR CLIENTES</td>
				</tr>
				<tr>
					<td>
						<table id="tblMain" style="LEFT: 0px; WIDTH: 544px; POSITION: static; TOP: 0px; HEIGHT: 17px"
							cellspacing="1" cellpadding="1" width="544" border="0">
							<tr id="Tr2" runat="server">
								<td class="etiqueta" style="WIDTH: 82px; HEIGHT: 4px">&nbsp;Desde : </td>
								<td align="left" height="30px" class="style1">
                                                
                                                        <asp:TextBox ID="txtFechaIni" runat="server" CssClass="input" Font-Size="10px" MaxLength="10" Width="80px"></asp:TextBox>&nbsp;
                                                        <img onclick="popUpCalendar(this, Form1.txtFechaIni, 'dd/mm/yyyy')" border="0" 
                                                                alt="Seleccionar fecha Inicial" src="../Imagenes/Calendario.gif" width="16px" height="16px"  /></td>                                                        
                                                        
                                    <td class="etiqueta" style="WIDTH: 82px; HEIGHT: 4px">&nbsp;Hasta : </td>
                                    <td style="WIDTH: 200px;" align="left" height="30px">
                                                    
                                                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="input" Font-Size="10px" MaxLength="10" Width="70px"></asp:TextBox>&nbsp;
                                                            <img onclick="popUpCalendar(this, Form1.txtFechaFin, 'dd/mm/yyyy')" border="0"		 
                                                                alt="Seleccionar fecha Final" src="../Imagenes/Calendario.gif" width="16px" height="16px"/>
                                                           </td>
							</tr>
                            <tr id="Tr1" runat="server">
								<td class="etiqueta" style="WIDTH: 82px; HEIGHT: 4px">&nbsp;Vendedor</td>
								<td style="HEIGHT: 4px" colspan="3">
                                    <asp:dropdownlist id="ddlVendedor" runat="server" Height="20px" Width="392px" 
                                        Font-Names="Verdana" Font-Size="8px" CssClass="Input"></asp:dropdownlist>
                                 </td>
							</tr>
                            </table>
                            <table style="LEFT: 0px; WIDTH: 544px; POSITION: static; TOP: 0px; HEIGHT: 17px">
                             <tr style="height: 25px;width:600px">
                            <td class="Etiqueta" style="width:150px" align="left">&nbsp;Cliente:</td>
                            <td  align="left" style="width:800px">
                                <asp:textbox id="txtCodigoCliente" runat="server" width="90px" CssClass="inputDisabled2"></asp:textbox>&nbsp;
                                    <input class="Boton" id="btnOT" style="width:20px; height:20px" onclick="fnc_BuscarCliente();" type="button" value="..."  />
                                <asp:textbox id="txtNombreCliente" runat="server" width="300px" CssClass="inputDisabled2"></asp:textbox>
                            </td>
                            
                        </tr>

							<tr>
								<td class="etiqueta" style="WIDTH: 82px; HEIGHT: 8px" vAlign="top">Tipo</td>
								<td vAlign="middle" colspan="3">
									<asp:RadioButtonList id="rdbTipo" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="R" Selected="True">Resumido</asp:ListItem>
									</asp:RadioButtonList></td>
                                    
							</tr>
						</table>
						<table class="botonera" id="Table6" style="HEIGHT: 24px" cellspacing="2" cellpadding="2"
							width="544px" border="0">
							<tr>
								<td align="right">
									<asp:button id="btnBuscar" runat="server" CssClass="boton" Width="120px" Height="24px" Text="Ver reporte"
										ToolTip="Muestra el reporte según los datos ingresados"></asp:button></td>
							</tr>


						</table>
					</td>


				</tr>
                							<tr>
								<td align="left">
									<asp:Label ID="lblerror" runat="server" Text="Label" CssClass="error"></asp:Label>
                                </td>
							</tr>
			</table>
		</form>
	    
	</body>
</html>
