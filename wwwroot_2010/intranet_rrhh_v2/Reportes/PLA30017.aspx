<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PLA30017.aspx.vb" Inherits="intranet_rrhh.PLA30017" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    	<title>PLA30008</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		  <link href="../Styles/NM0001.css" rel="stylesheet" type="text/css" /> 
		<script type="text/javascript" language="javascript">


		    function fMostrarReporte(strURL) {

		        var intWidth = screen.width;
		        var intHeight = screen.height;

		        window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }
		</script>
	    <style type="text/css">
            .style1
            {
                font-weight: bold;
                font-size: 9px;
                color: #333333;
                background-color: #BCCAE0;
                font-family: Verdana;
                text-decoration: none;
                height: 20px;
                width: 64px;
            }
            </style>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 32px"
				cellspacing="2" cellpadding="2" width="100%" border="0">
				<tbody>
					<tr>
						<td class="Cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">MINTRA - Formulario 2 
                            - Obreros </td>
					</tr>
					<tr>
						<td class="panel" rowspan="2">
							<table id="Table2" style="WIDTH: 772px; HEIGHT: 28px" cellspacing="1" cellpadding="1"
								border="0">
								<tbody>
									<tr>
										<td class="style1">Año</td>
										<td style="WIDTH: 100px; HEIGHT: 20px">
                                            <asp:textbox id="txtAnio" runat="server" 
                                                MaxLength="4" Height="16px" Width="39px" Font-Names="Verdana"
												Font-Size="XX-Small" CssClass="TextoCentrado"></asp:textbox></td>
										<td class="Etiqueta" style="WIDTH: 50px; HEIGHT: 20px">Mes</td>
                                        <td style="WIDTH: 50px; HEIGHT: 20px">
                                        <asp:dropdownlist id="ddlMes" runat="server" Height="16px" Width="150px" Font-Names="Verdana" Font-Size="XX-Small">
												<asp:ListItem Value="1">ENERO</asp:ListItem>
												<asp:ListItem Value="2">FEBRERO</asp:ListItem>
												<asp:ListItem Value="3">MARZO</asp:ListItem>
												<asp:ListItem Value="4">ABRIL</asp:ListItem>
												<asp:ListItem Value="5">MAYO</asp:ListItem>
												<asp:ListItem Value="6">JUNIO</asp:ListItem>
												<asp:ListItem Value="7">JULIO</asp:ListItem>
												<asp:ListItem Value="8">AGOSTO</asp:ListItem>
												<asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
												<asp:ListItem Value="10">OCTUBRE</asp:ListItem>
												<asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
												<asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
											</asp:dropdownlist>
                                           </td>
										<td style="WIDTH: 150px; HEIGHT: 21px">
                                            &nbsp;</td>
										<td style="WIDTH: 150px; HEIGHT: 21px">
                                            <asp:button id="btnReporte" runat="server" Text="Reporte" CssClass="boton" 
                                                Width="100px"></asp:button></td>
									</tr>
								</tbody>
							</table>
							<table class="botonera" id="Table3" style="WIDTH: 600px; HEIGHT: 28px" cellspacing="1"
								cellpadding="1" width="595" border="0">
								<tr>
                                    <td align="left" style="width:500px"><asp:Label ID = "lblMensaje" Text="" runat = "server"></asp:Label></td>
									<td align="right"  style="width:100px">&nbsp;</td>
								</tr>
							</table>
		
		</td></tr></tbody></table>
        </form>
	</body>
</html>

