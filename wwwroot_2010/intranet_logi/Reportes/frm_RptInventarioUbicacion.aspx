<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RptInventarioUbicacion.aspx.vb" Inherits="intranet_logi.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>REPORTE DE INVENTARIO POR UBICACION</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet" />
        
		<script type="text/javascript" language="javascript" >
		    function popUp(strUrl) {
		        var intWidth = screen.width;
		        var intHeight = screen.height;
		        //window.open(strUrl);
		        window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }	
		</script>

	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 706px; POSITION: absolute; TOP: 8px; HEIGHT: 64px"
				cellspacing="1" cellpadding="0" width="706" border="0">
				<tr>
					<td class="Cabecera" id="colTitulo" align="left" runat="Server">&nbsp;REPORTE DE INVENTARIO POR UBICACION</td>
				</tr>
				<tr>
					<td>
						<table id="tblMain" style="LEFT: 0px; WIDTH: 544px; POSITION: static; TOP: 0px; HEIGHT: 17px"
							cellspacing="1" cellpadding="1" width="544" border="0">
							<tr id="rowPeriodo" runat="server">
								<td class="etiqueta" style="WIDTH: 82px; HEIGHT: 4px">Ubicación&nbsp;</td>
								<td style="HEIGHT: 4px">
									<asp:TextBox ID="txtUbicacion" runat="server" CssClass="Input" Width="71px"></asp:TextBox>
                                </td>
							</tr>
							</table>
						<table class="botonera" id="Table6" style="HEIGHT: 24px" cellspacing="2" cellpadding="2"
							width="100%" border="0">
							<tr>
								<td align="right">
									<asp:button id="btnReporte" runat="server" CssClass="boton" Width="120px" 
                                        Height="24px" Text="Ver reporte"
										ToolTip="Muestra el reporte según los datos ingresados"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

    